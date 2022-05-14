using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WG3000_COMM.Core
{
	public sealed class ExcelObject : IDisposable
	{
		public delegate void ProgressWork(float percentage);

		private string excelObject = "Provider=Microsoft.{0}.OLEDB.{1};Data Source={2};Extended Properties=\"Excel {3};HDR=YES\"";

		private string filepath = string.Empty;

		private OleDbConnection con;

		private DataSet ds;

		private event ExcelObject.ProgressWork Reading;

		private event ExcelObject.ProgressWork Writing;

		private event EventHandler connectionStringChange;

		public event ExcelObject.ProgressWork ReadProgress
		{
			add
			{
				this.Reading += value;
			}
			remove
			{
				this.Reading -= value;
			}
		}

		public event ExcelObject.ProgressWork WriteProgress
		{
			add
			{
				this.Writing += value;
			}
			remove
			{
				this.Writing -= value;
			}
		}

		public event EventHandler ConnectionStringChanged
		{
			add
			{
				this.connectionStringChange += value;
			}
			remove
			{
				this.connectionStringChange -= value;
			}
		}

		public string ConnectionString
		{
			get
			{
				if (this.filepath == string.Empty)
				{
					return string.Empty;
				}
				FileInfo fileInfo = new FileInfo(this.filepath);
				if (fileInfo.Extension.Equals(".xls"))
				{
					return string.Format(this.excelObject, new object[]
					{
						"Jet",
						"4.0",
						this.filepath,
						"8.0"
					});
				}
				if (fileInfo.Extension.Equals(".xlsx"))
				{
					return string.Format(this.excelObject, new object[]
					{
						"Ace",
						"12.0",
						this.filepath,
						"12.0"
					});
				}
				return string.Format(this.excelObject, new object[]
				{
					"Jet",
					"4.0",
					this.filepath,
					"8.0"
				});
			}
		}

		public OleDbConnection Connection
		{
			get
			{
				if (this.con == null)
				{
					OleDbConnection oleDbConnection = new OleDbConnection(this.ConnectionString);
					this.con = oleDbConnection;
				}
				return this.con;
			}
		}

		public void onReadProgress(float percentage)
		{
			if (this.Reading != null)
			{
				this.Reading(percentage);
			}
		}

		public void onWriteProgress(float percentage)
		{
			if (this.Writing != null)
			{
				this.Writing(percentage);
			}
		}

		public void onConnectionStringChanged()
		{
			if (this.Connection != null && !this.Connection.ConnectionString.Equals(this.ConnectionString))
			{
				if (this.Connection.State == ConnectionState.Open)
				{
					this.Connection.Close();
				}
				this.Connection.Dispose();
				this.con = null;
			}
			if (this.connectionStringChange != null)
			{
				this.connectionStringChange(this, new EventArgs());
			}
		}

		public ExcelObject(string path)
		{
			this.filepath = path;
			this.onConnectionStringChanged();
		}

		public DataTable GetSchema()
		{
			if (this.Connection.State != ConnectionState.Open)
			{
				this.Connection.Open();
			}
			return this.Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
			{
				null,
				null,
				null,
				"TABLE"
			});
		}

		public DataTable ReadTable(string tableName)
		{
			return this.ReadTable(tableName, "");
		}

		public DataTable ReadTable(string tableName, string criteria)
		{
			DataTable result;
			try
			{
				if (this.Connection.State != ConnectionState.Open)
				{
					this.Connection.Open();
					this.onReadProgress(10f);
				}
				string text = "Select * from [{0}]";
				if (!string.IsNullOrEmpty(criteria))
				{
					text = text + " Where " + criteria;
				}
				using (OleDbCommand oleDbCommand = new OleDbCommand(string.Format(text, tableName)))
				{
					oleDbCommand.Connection = this.Connection;
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						this.onReadProgress(30f);
						this.ds = new DataSet();
						this.onReadProgress(50f);
						oleDbDataAdapter.Fill(this.ds, tableName);
						this.onReadProgress(100f);
						if (this.ds.Tables.Count == 1)
						{
							result = this.ds.Tables[0];
						}
						else
						{
							result = null;
						}
					}
				}
			}
			catch
			{
				XMessageBox.Show("Table Cannot be read");
				result = null;
			}
			return result;
		}

		public bool DropTable(string tablename)
		{
			bool result;
			try
			{
				if (this.Connection.State != ConnectionState.Open)
				{
					this.Connection.Open();
					this.onWriteProgress(10f);
				}
				string format = "Drop Table [{0}]";
				using (OleDbCommand oleDbCommand = new OleDbCommand(string.Format(format, tablename), this.Connection))
				{
					this.onWriteProgress(30f);
					oleDbCommand.ExecuteNonQuery();
					this.onWriteProgress(80f);
				}
				this.Connection.Close();
				this.onWriteProgress(100f);
				result = true;
			}
			catch (Exception ex)
			{
				this.onWriteProgress(0f);
				XMessageBox.Show(ex.Message);
				result = false;
			}
			return result;
		}

		public bool WriteTable(string tableName, Dictionary<string, string> tableDefination)
		{
			bool result;
			try
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(this.GenerateCreateTable(tableName, tableDefination), this.Connection))
				{
					if (this.Connection.State != ConnectionState.Open)
					{
						this.Connection.Open();
					}
					oleDbCommand.ExecuteNonQuery();
					result = true;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public bool test_WriteTable()
		{
			bool result;
			try
			{
				string cmdText = "CREATE TABLE [users](f_ConsumerID Int,编号 CHAR(50),姓名 CHAR(100),卡号 Int,部门 String,考勤 Byte,倒班 Byte,门禁 Byte,起始日期 DateTime,截止日期 DateTime)";
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, this.Connection))
				{
					if (this.Connection.State != ConnectionState.Open)
					{
						this.Connection.Open();
					}
					oleDbCommand.ExecuteNonQuery();
					result = true;
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
				result = false;
			}
			return result;
		}

		public bool WriteTable(DataView dv)
		{
			bool result;
			try
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(this.GenerateCreateTable(dv), this.Connection))
				{
					if (this.Connection.State != ConnectionState.Open)
					{
						this.Connection.Open();
					}
					oleDbCommand.ExecuteNonQuery();
					result = true;
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
				result = false;
			}
			return result;
		}

		public bool WriteTable(DataGridView dgv)
		{
			bool result;
			try
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(this.GenerateCreateTable(dgv), this.Connection))
				{
					if (this.Connection.State != ConnectionState.Open)
					{
						this.Connection.Open();
					}
					oleDbCommand.ExecuteNonQuery();
					result = true;
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
				result = false;
			}
			return result;
		}

		private string GenerateCreateTable(DataView dv)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("CREATE TABLE [{0}](", dv.Table.TableName);
			bool flag = true;
			foreach (DataColumn dataColumn in dv.Table.Columns)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				flag = false;
				if (dataColumn.DataType.ToString().IndexOf("System.Int") >= 0)
				{
					stringBuilder.AppendFormat("{0} {1}", dataColumn.ColumnName.ToString(), "Int");
				}
				else
				{
					stringBuilder.AppendFormat("{0} {1}", dataColumn.ColumnName.ToString(), dataColumn.DataType.ToString().Replace("System.", ""));
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString().Replace("\r\n", " ");
		}

		private string GenerateCreateTable(DataGridView dgv)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("CREATE TABLE [{0}](", "ExcelData");
			bool flag = true;
			foreach (DataGridViewColumn dataGridViewColumn in dgv.Columns)
			{
				if (dataGridViewColumn.Visible)
				{
					string text = dataGridViewColumn.HeaderText.ToString().Replace("[", "(");
					text = text.Replace("]", ")");
					text = text.Replace(".", " ");
					text = text.Replace("\r\n", " ");
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					flag = false;
					if (dataGridViewColumn.ValueType.Name.ToString().IndexOf("Int") >= 0)
					{
						if (stringBuilder.ToString().IndexOf(string.Format("[{0}]", text)) >= 0)
						{
							stringBuilder.AppendFormat("[{0}] {1}", text + dataGridViewColumn.Index.ToString(), "Int");
						}
						else
						{
							stringBuilder.AppendFormat("[{0}] {1}", text, "Int");
						}
					}
					else if (dataGridViewColumn.ValueType.Name.ToString().IndexOf("DateTime") >= 0)
					{
						if (stringBuilder.ToString().IndexOf(string.Format("[{0}]", text)) >= 0)
						{
							stringBuilder.AppendFormat("[{0}] {1}", text + dataGridViewColumn.Index.ToString(), "String");
						}
						else
						{
							stringBuilder.AppendFormat("[{0}] {1}", text, "String");
						}
					}
					else if (stringBuilder.ToString().IndexOf(string.Format("[{0}]", text)) >= 0)
					{
						stringBuilder.AppendFormat("[{0}] {1}", text + dataGridViewColumn.Index.ToString(), dataGridViewColumn.ValueType.Name.ToString());
					}
					else
					{
						stringBuilder.AppendFormat("[{0}] {1}", text, dataGridViewColumn.ValueType.Name.ToString());
					}
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString().Replace("\r\n", " ").Replace(".", " ");
		}

		public bool AddNewRow(DataRow dr)
		{
			using (OleDbCommand oleDbCommand = new OleDbCommand(this.GenerateInsertStatement(dr), this.Connection))
			{
				oleDbCommand.ExecuteNonQuery();
			}
			return true;
		}

		public bool AddNewRow(DataGridViewRow dgvdr, DataGridView dgv)
		{
			using (OleDbCommand oleDbCommand = new OleDbCommand(this.GenerateInsertStatement(dgvdr, dgv), this.Connection))
			{
				oleDbCommand.ExecuteNonQuery();
			}
			return true;
		}

		private string GenerateInsertStatement(DataGridViewRow dgvdr, DataGridView dgv)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("INSERT INTO [{0}](", "ExcelData");
			bool flag = true;
			foreach (DataGridViewColumn dataGridViewColumn in dgv.Columns)
			{
				if (dataGridViewColumn.Visible)
				{
					string text = dataGridViewColumn.HeaderText.ToString().Replace("[", "(");
					text = text.Replace("]", ")");
					text = text.Replace(".", " ");
					text = text.Replace("\r\n", " ");
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					flag = false;
					if (stringBuilder.ToString().IndexOf(string.Format("[{0}]", text)) >= 0)
					{
						stringBuilder.AppendFormat(string.Format("[{0}]", text + dataGridViewColumn.Index.ToString()), new object[0]);
					}
					else
					{
						stringBuilder.AppendFormat(string.Format("[{0}]", text), new object[0]);
					}
				}
			}
			stringBuilder.Append(") VALUES(");
			string value = stringBuilder.ToString().Replace("\r\n", " ").Replace(".", " ");
			stringBuilder = null;
			stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			flag = true;
			for (int i = 0; i <= dgv.Columns.Count - 1; i++)
			{
				if (dgv.Columns[i].Visible)
				{
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					flag = false;
					if (dgvdr.Cells[i].Value == null)
					{
						stringBuilder.Append("NULL");
					}
					else if (dgvdr.Cells[i].Value == DBNull.Value)
					{
						stringBuilder.Append("NULL");
					}
					else if (dgvdr.Cells[i].Value.ToString().Trim() == "")
					{
						stringBuilder.Append("NULL");
					}
					else if (dgv.Columns[i].ValueType.Name.ToString().IndexOf("Int") < 0)
					{
						if (dgv.Columns[i].ValueType.Name.ToString().IndexOf("DateTime") >= 0)
						{
							stringBuilder.Append("'");
							if (string.IsNullOrEmpty(dgv.Columns[i].DefaultCellStyle.Format))
							{
								stringBuilder.Append(dgvdr.Cells[i].Value.ToString().Replace("'", "''"));
							}
							else
							{
								stringBuilder.Append(((DateTime)dgvdr.Cells[i].Value).ToString(dgv.Columns[i].DefaultCellStyle.Format).Replace("'", "''"));
							}
							stringBuilder.Append("'");
						}
						else
						{
							stringBuilder.Append("'");
							stringBuilder.Append(dgvdr.Cells[i].Value.ToString().Replace("'", "''"));
							stringBuilder.Append("'");
						}
					}
					else
					{
						stringBuilder.Append(dgvdr.Cells[i].Value.ToString().Replace("'", "''"));
					}
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString().Replace("\r\n", " ");
		}

		private string GenerateCreateTable(string tableName, Dictionary<string, string> tableDefination)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("CREATE TABLE [{0}](", tableName);
			bool flag = true;
			foreach (KeyValuePair<string, string> current in tableDefination)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				flag = false;
				stringBuilder.AppendFormat("{0} {1}", current.Key, current.Value);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString().Replace("\r\n", " ");
		}

		private string GenerateInsertStatement(DataRow dr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			stringBuilder.AppendFormat("INSERT INTO [{0}](", dr.Table.TableName);
			foreach (DataColumn dataColumn in dr.Table.Columns)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				flag = false;
				stringBuilder.Append(dataColumn.Caption);
			}
			stringBuilder.Append(") VALUES(");
			for (int i = 0; i <= dr.Table.Columns.Count - 1; i++)
			{
				if (!object.ReferenceEquals(dr.Table.Columns[i].DataType, typeof(int)))
				{
					stringBuilder.Append("'");
					stringBuilder.Append(dr[i].ToString().Replace("'", "''"));
					stringBuilder.Append("'");
				}
				else
				{
					stringBuilder.Append(dr[i].ToString().Replace("'", "''"));
				}
				if (i != dr.Table.Columns.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString().Replace("\r\n", " ");
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.con != null && this.con.State == ConnectionState.Open)
				{
					this.con.Close();
				}
				if (this.con != null)
				{
					this.con.Dispose();
					this.con = null;
				}
				if (this.filepath != null)
				{
					this.filepath = string.Empty;
				}
				if (this.ds != null)
				{
					this.ds.Dispose();
				}
			}
		}
	}
}
