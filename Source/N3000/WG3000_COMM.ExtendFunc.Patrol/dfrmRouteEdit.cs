using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class dfrmRouteEdit : frmN3000
	{
		public int currentRouteID;

		private DataSet ds = new DataSet("dsMeal");

		private DataView dv;

		private DataView dvSelected;

		private DataTable dt;

		private int routeSn = -1;

		private int colf_ReaderID = 3;

		private int colf_Sn = 2;

		private string datetimeFirstPatrol;

		private IContainer components;

		internal Button cmdCancel;

		internal Button cmdOK;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private NumericUpDown nudMinute;

		private DataGridView dgvSelected;

		internal Label Label10;

		private DataGridView dgvOptional;

		internal Button btnDeleteAllReaders;

		internal Label Label11;

		internal Button btnDeleteOneReader;

		internal Button btnAddAllReaders;

		internal Button btnAddOneReader;

		private DateTimePicker dateBeginHMS1;

		private DateTimePicker dtpTime;

		private Label label45;

		private CheckBox chkAutoAdd;

		private Label label1;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private DataGridViewTextBoxColumn f_NextDay1;

		private DataGridViewTextBoxColumn f_patroltime1;

		private DataGridViewTextBoxColumn f_Sn;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		private Label label2;

		internal TextBox txtName;

		internal ComboBox cbof_RouteID;

		internal Label Label8;

		internal Button btnStartTimeUpdate;

		internal Button btnCopyFromOtherRoute;

		private DataGridViewCheckBoxColumn NextDay;

		private DataGridViewTextBoxColumn Cost;

		private DataGridViewTextBoxColumn f_SN2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		internal Label label3;

		public dfrmRouteEdit()
		{
			this.InitializeComponent();
		}

		private void dfrmMealOption_Load(object sender, EventArgs e)
		{
			this.dtpTime.CustomFormat = "HH:mm";
			this.dtpTime.Format = DateTimePickerFormat.Custom;
			this.dtpTime.Value = DateTime.Parse("00:00:00");
			this.loadData();
			this.dgvOptional.AutoGenerateColumns = false;
			this.dgvOptional.DataSource = this.dv;
			this.dgvSelected.AutoGenerateColumns = false;
			this.dgvSelected.DataSource = this.dvSelected;
			this.dvSelected.Sort = "f_NextDay ASC, f_patroltime asc, f_Sn asc";
			this.dgvOptional.DefaultCellStyle.ForeColor = Color.Black;
			this.dgvSelected.DefaultCellStyle.ForeColor = Color.Black;
			this.dt = this.ds.Tables["optionalReader"];
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dt.Columns[this.colf_ReaderID]
				};
				this.dt.PrimaryKey = primaryKey;
			}
			catch (Exception)
			{
				throw;
			}
			this.dt = this.ds.Tables["selectedReader"];
			try
			{
				DataColumn[] primaryKey2 = new DataColumn[]
				{
					this.dt.Columns[this.colf_Sn]
				};
				this.dt.PrimaryKey = primaryKey2;
			}
			catch (Exception)
			{
				throw;
			}
			for (int i = 0; i < this.dgvOptional.Columns.Count; i++)
			{
				this.dgvOptional.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
				this.dgvSelected.Columns[i].DataPropertyName = this.dvSelected.Table.Columns[i].ColumnName;
			}
			this.cbof_RouteID.Items.Clear();
			for (int j = 1; j <= 99; j++)
			{
				this.cbof_RouteID.Items.Add(j);
			}
			string a = "";
			if (this.currentRouteID <= 0)
			{
				a = "New";
			}
			if (a == "New")
			{
				this.ds.Tables["selectedReader"].Clear();
				this.cbof_RouteID.Enabled = true;
				string cmdText = "SELECT f_RouteID FROM t_d_PatrolRouteList  ORDER BY [f_RouteID] ASC ";
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
						{
							oleDbConnection.Open();
							OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
							while (oleDbDataReader.Read())
							{
								int num = this.cbof_RouteID.Items.IndexOf((int)oleDbDataReader[0]);
								if (num >= 0)
								{
									this.cbof_RouteID.Items.RemoveAt(num);
								}
							}
							oleDbDataReader.Close();
						}
						goto IL_376;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num = this.cbof_RouteID.Items.IndexOf((int)sqlDataReader[0]);
							if (num >= 0)
							{
								this.cbof_RouteID.Items.RemoveAt(num);
							}
						}
						sqlDataReader.Close();
					}
				}
				IL_376:
				if (this.cbof_RouteID.Items.Count == 0)
				{
					base.Close();
				}
				this.cbof_RouteID.Text = this.cbof_RouteID.Items[0].ToString();
			}
			else
			{
				this.cbof_RouteID.Enabled = false;
				this.cbof_RouteID.Text = this.currentRouteID.ToString();
				string cmdText2 = " SELECT * FROM t_d_PatrolRouteList WHERE [f_RouteID]= " + this.currentRouteID.ToString();
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand2 = new OleDbCommand(cmdText2, oleDbConnection2))
						{
							oleDbConnection2.Open();
							OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader();
							if (oleDbDataReader2.Read())
							{
								this.txtName.Text = wgTools.SetObjToStr(oleDbDataReader2["f_RouteName"].ToString());
							}
							oleDbDataReader2.Close();
						}
						goto IL_4DF;
					}
				}
				using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection2))
					{
						sqlConnection2.Open();
						SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
						if (sqlDataReader2.Read())
						{
							this.txtName.Text = wgTools.SetObjToStr(sqlDataReader2["f_RouteName"].ToString());
						}
						sqlDataReader2.Close();
					}
				}
			}
			IL_4DF:
			if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("RouteEditAutoIncrease")))
			{
				if (wgAppConfig.GetKeyVal("RouteEditAutoIncrease") == "0")
				{
					this.chkAutoAdd.Checked = false;
				}
				else
				{
					this.chkAutoAdd.Checked = true;
					try
					{
						this.nudMinute.Value = decimal.Parse(wgAppConfig.GetKeyVal("RouteEditAutoIncrease"));
					}
					catch (Exception)
					{
					}
				}
			}
			if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("RouteEditStartTime")))
			{
				try
				{
					this.dtpTime.Value = DateTime.Parse(wgAppConfig.GetKeyVal("RouteEditStartTime"));
				}
				catch (Exception)
				{
				}
			}
		}

		public void loadData_Acc()
		{
			OleDbConnection connection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				OleDbCommand selectCommand = new OleDbCommand("Select  0 as f_NextDay,'' as f_patroltime, -1 as f_Sn, t_b_reader.f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader, t_b_Reader4Patrol  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  t_b_Reader4Patrol.f_ReaderID = t_b_Reader.f_ReaderID  ", connection);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "optionalReader");
				selectCommand = new OleDbCommand("Select   f_NextDay, f_patroltime, f_Sn, t_b_reader.f_ReaderID, t_b_reader.f_ReaderName, 0 as f_Selected from t_d_PatrolRouteDetail,t_b_reader, t_b_Reader4Patrol  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_PatrolRouteDetail.f_ReaderID  AND t_b_Reader.f_ReaderID = t_b_Reader4Patrol.f_ReaderID  and t_d_PatrolRouteDetail.f_RouteID = " + this.currentRouteID.ToString(), connection);
				oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "selectedReader");
				this.dv = new DataView(this.ds.Tables["optionalReader"]);
				this.dvSelected = new DataView(this.ds.Tables["selectedReader"]);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public void loadData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadData_Acc();
				return;
			}
			SqlConnection connection = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				SqlCommand selectCommand = new SqlCommand("Select  0 as f_NextDay,'' as f_patroltime, -1 as f_Sn, t_b_reader.f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader, t_b_Reader4Patrol  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  t_b_Reader4Patrol.f_ReaderID = t_b_Reader.f_ReaderID  ", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
				sqlDataAdapter.Fill(this.ds, "optionalReader");
				selectCommand = new SqlCommand("Select   f_NextDay, f_patroltime, f_Sn, t_b_reader.f_ReaderID, t_b_reader.f_ReaderName, 0 as f_Selected from t_d_PatrolRouteDetail,t_b_reader,t_b_Reader4Patrol   , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_PatrolRouteDetail.f_ReaderID  AND t_b_Reader.f_ReaderID = t_b_Reader4Patrol.f_ReaderID  and t_d_PatrolRouteDetail.f_RouteID = " + this.currentRouteID.ToString(), connection);
				sqlDataAdapter = new SqlDataAdapter(selectCommand);
				sqlDataAdapter.Fill(this.ds, "selectedReader");
				this.dv = new DataView(this.ds.Tables["optionalReader"]);
				this.dvSelected = new DataView(this.ds.Tables["selectedReader"]);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtName.Text))
			{
				XMessageBox.Show(CommonStr.strNameNotEmpty);
				return;
			}
			if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
			{
				XMessageBox.Show(CommonStr.strNameNotEmpty);
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			try
			{
				Cursor arg_51_0 = Cursor.Current;
				string text;
				if (this.currentRouteID <= 0)
				{
					this.currentRouteID = int.Parse(this.cbof_RouteID.Text);
					text = string.Format(" INSERT INTO t_d_PatrolRouteList(f_RouteID, f_RouteName) VALUES({0},{1})", this.currentRouteID.ToString(), wgTools.PrepareStr(this.txtName.Text));
					wgAppConfig.runUpdateSql(text);
				}
				text = " DELETE FROM  t_d_PatrolRouteDetail WHERE f_RouteID = " + this.currentRouteID.ToString();
				wgAppConfig.runUpdateSql(text);
				if (this.dvSelected.Count > 0)
				{
					for (int i = 0; i <= this.dvSelected.Count - 1; i++)
					{
						text = "INSERT INTO t_d_PatrolRouteDetail (f_RouteID, f_Sn, f_ReaderID, f_patroltime, f_NextDay) VALUES( ";
						text += this.currentRouteID.ToString();
						text = text + "," + (i + 1).ToString();
						text = text + "," + this.dvSelected[i]["f_ReaderID"].ToString();
						text = text + "," + wgTools.PrepareStr(this.dvSelected[i]["f_patroltime"].ToString());
						text = text + "," + this.dvSelected[i]["f_NextDay"].ToString();
						text += ") ";
						wgAppConfig.runUpdateSql(text);
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void saveDefaultValue()
		{
			if (this.dgvSelected.Rows.Count == 0)
			{
				wgAppConfig.UpdateKeyVal("RouteEditStartTime", this.dtpTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				if (this.chkAutoAdd.Checked)
				{
					wgAppConfig.UpdateKeyVal("RouteEditAutoIncrease", this.nudMinute.Value.ToString());
					return;
				}
				wgAppConfig.UpdateKeyVal("RouteEditAutoIncrease", "0");
			}
		}

		private void btnAddAllReaders_Click(object sender, EventArgs e)
		{
			this.dgvOptional.SelectAll();
			this.btnAddOneReader_Click(sender, e);
			this.dgvOptional.ClearSelection();
		}

		private void btnAddOneReader_Click(object sender, EventArgs e)
		{
			this.saveDefaultValue();
			int arg_16_0 = this.dgvSelected.Rows.Count;
			if (this.routeSn < 0 && this.dgvSelected.Rows.Count > 0)
			{
				foreach (DataRowView dataRowView in (this.dgvSelected.DataSource as DataView))
				{
					if (this.routeSn < (int)dataRowView["f_Sn"])
					{
						this.routeSn = (int)dataRowView["f_Sn"];
					}
				}
				this.routeSn++;
			}
			if (this.routeSn <= 0)
			{
				this.routeSn = 1;
			}
			if (this.dgvSelected.Rows.Count <= 0)
			{
				this.datetimeFirstPatrol = this.dtpTime.Value.ToString("HH:mm");
				this.routeSn = 1;
				this.radioButton2.Checked = false;
				this.radioButton1.Checked = true;
			}
			else
			{
				this.datetimeFirstPatrol = (this.dgvSelected.DataSource as DataView)[0]["f_patroltime"].ToString();
				if ((this.dgvSelected.DataSource as DataView)[0]["f_NextDay"].ToString() == "1")
				{
					XMessageBox.Show(CommonStr.strPatrolPointFirstTimeNextDay);
					return;
				}
				if (string.Compare(this.datetimeFirstPatrol, this.dtpTime.Value.ToString("HH:mm")) > 0)
				{
					this.radioButton2.Checked = true;
				}
				else
				{
					if (string.Compare(this.datetimeFirstPatrol, this.dtpTime.Value.ToString("HH:mm")) == 0 && this.dtpTime.Value.ToString("HH:mm") == "00:00")
					{
						XMessageBox.Show(CommonStr.strPatrolPointAddFailed);
						return;
					}
					this.radioButton2.Checked = false;
				}
			}
			DataGridView dataGridView = this.dgvOptional;
			int index;
			if (dataGridView.SelectedRows.Count <= 0)
			{
				if (dataGridView.SelectedCells.Count <= 0)
				{
					return;
				}
				index = dataGridView.SelectedCells[0].RowIndex;
			}
			else
			{
				index = dataGridView.SelectedRows[0].Index;
			}
			DataTable table = ((DataView)this.dgvSelected.DataSource).Table;
			using (DataTable table2 = ((DataView)dataGridView.DataSource).Table)
			{
				if (dataGridView.SelectedRows.Count > 0)
				{
					int count = dataGridView.SelectedRows.Count;
					int[] array = new int[count];
					int num = 0;
					for (int i = 0; i < dataGridView.Rows.Count; i++)
					{
						if (dataGridView.Rows[i].Selected)
						{
							array[num] = (int)dataGridView.Rows[i].Cells[this.colf_ReaderID].Value;
							num++;
						}
					}
					for (int j = 0; j < count; j++)
					{
						int num2 = array[j];
						DataRow dataRow = table2.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_NextDay"] = (this.radioButton2.Checked ? 1 : 0);
							dataRow["f_patroltime"] = this.dtpTime.Value.ToString("HH:mm");
							DataRow dataRow2 = table.NewRow();
							for (int k = 0; k < table2.Columns.Count; k++)
							{
								dataRow2[k] = dataRow[k];
							}
							dataRow2["f_Sn"] = this.routeSn;
							this.routeSn++;
							table.Rows.Add(dataRow2);
							if (this.chkAutoAdd.Checked)
							{
								if (this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value).Date == this.dtpTime.Value.Date)
								{
									if (this.radioButton2.Checked && string.Compare(this.datetimeFirstPatrol, this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value).ToString("HH:mm")) <= 0)
									{
										XMessageBox.Show(CommonStr.strPatrolPointAddFailed);
										break;
									}
									this.dtpTime.Value = this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value);
								}
								else
								{
									if (this.radioButton2.Checked)
									{
										XMessageBox.Show(CommonStr.strPatrolPointAddFailed);
										break;
									}
									this.dtpTime.Value = this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value);
									this.radioButton2.Checked = true;
								}
							}
						}
					}
				}
				else
				{
					int num3 = (int)dataGridView.Rows[index].Cells[this.colf_ReaderID].Value;
					DataRow dataRow = table2.Rows.Find(num3);
					if (dataRow != null)
					{
						dataRow["f_NextDay"] = (this.radioButton2.Checked ? 1 : 0);
						dataRow["f_patroltime"] = this.dtpTime.Value.ToString("HH:mm");
						DataRow dataRow3 = table.NewRow();
						for (int l = 0; l < table2.Columns.Count; l++)
						{
							dataRow3[l] = dataRow[l];
						}
						dataRow3["f_Sn"] = this.routeSn;
						this.routeSn++;
						table.Rows.Add(dataRow3);
						if (this.chkAutoAdd.Checked)
						{
							if (this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value).Date == this.dtpTime.Value.Date)
							{
								if (this.radioButton2.Checked && string.Compare(this.datetimeFirstPatrol, this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value).ToString("HH:mm")) <= 0)
								{
									XMessageBox.Show(CommonStr.strPatrolPointAddFailed);
									table.AcceptChanges();
									return;
								}
								this.dtpTime.Value = this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value);
							}
							else if (!this.radioButton2.Checked)
							{
								this.dtpTime.Value = this.dtpTime.Value.AddMinutes((double)this.nudMinute.Value);
								this.radioButton2.Checked = true;
							}
						}
					}
				}
			}
			table.AcceptChanges();
		}

		private void btnDeleteOneReader_Click(object sender, EventArgs e)
		{
			DataGridView dataGridView = this.dgvSelected;
			try
			{
				int index;
				if (dataGridView.SelectedRows.Count <= 0)
				{
					if (dataGridView.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dataGridView.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dataGridView.SelectedRows[0].Index;
				}
				using (DataTable table = ((DataView)dataGridView.DataSource).Table)
				{
					if (dataGridView.SelectedRows.Count > 0)
					{
						int count = dataGridView.SelectedRows.Count;
						int[] array = new int[count];
						for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
						{
							array[i] = (int)dataGridView.SelectedRows[i].Cells[this.colf_Sn].Value;
						}
						for (int j = 0; j < count; j++)
						{
							int num = array[j];
							DataRow dataRow = table.Rows.Find(num);
							if (dataRow != null)
							{
								dataRow.Delete();
							}
						}
						table.AcceptChanges();
					}
					else
					{
						int num2 = (int)dataGridView.Rows[index].Cells[this.colf_Sn].Value;
						DataRow dataRow = table.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = 0;
							dataRow.Delete();
						}
						table.AcceptChanges();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void btnDeleteAllReaders_Click(object sender, EventArgs e)
		{
			this.dgvSelected.SelectAll();
			this.btnDeleteOneReader_Click(sender, e);
		}

		private void btnStartTimeUpdate_Click(object sender, EventArgs e)
		{
			DataTable table = ((DataView)this.dgvSelected.DataSource).Table;
			DataView dataView = (DataView)this.dgvSelected.DataSource;
			if (dataView.Count > 0)
			{
				if ((this.dgvSelected.DataSource as DataView)[0]["f_NextDay"].ToString() == "1")
				{
					XMessageBox.Show(CommonStr.strPatrolPointFirstTimeNextDay);
					return;
				}
				string str = dataView[0]["f_patroltime"].ToString();
				DateTime value = DateTime.Parse(this.dtpTime.Value.ToString("yyyy-MM-dd ") + str + ":00");
				TimeSpan timeSpan = this.dtpTime.Value.Subtract(value);
				dataView[0]["f_patroltime"] = this.dtpTime.Value.ToString("HH:mm");
				value = DateTime.Parse(this.dtpTime.Value.ToString("yyyy-MM-dd HH:mm:00"));
				DateTime t = value.AddDays(1.0);
				for (int i = 1; i < table.Rows.Count; i++)
				{
					DateTime t2 = DateTime.Parse(this.dtpTime.Value.AddDays((double)((int)table.Rows[i]["f_NextDay"])).ToString("yyyy-MM-dd ") + table.Rows[i]["f_patroltime"].ToString() + ":00").AddMinutes(timeSpan.TotalMinutes);
					if (t2 >= t)
					{
						XMessageBox.Show(CommonStr.strPatrolErrPatrolTime);
						table.AcceptChanges();
						return;
					}
					table.Rows[i]["f_patroltime"] = t2.ToString("HH:mm");
					table.Rows[i]["f_NextDay"] = ((t2.Date != value.Date) ? 1 : 0);
				}
				table.AcceptChanges();
			}
		}

		private void btnCopyFromOtherRoute_Click(object sender, EventArgs e)
		{
			using (dfrmPatrolTaskEdit dfrmPatrolTaskEdit = new dfrmPatrolTaskEdit())
			{
				if (dfrmPatrolTaskEdit.ShowDialog() == DialogResult.OK)
				{
					int routeID = dfrmPatrolTaskEdit.routeID;
					if (routeID != 0)
					{
						this.ds.Tables["selectedReader"].Clear();
						string cmdText = "Select   f_NextDay, f_patroltime, f_Sn, t_b_reader.f_ReaderID, t_b_reader.f_ReaderName, 0 as f_Selected from t_d_PatrolRouteDetail,t_b_reader  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_PatrolRouteDetail.f_ReaderID and t_d_PatrolRouteDetail.f_RouteID = " + routeID.ToString();
						if (wgAppConfig.IsAccessDB)
						{
							using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
							{
								using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
								{
									using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
									{
										oleDbDataAdapter.Fill(this.ds, "selectedReader");
									}
								}
								goto IL_106;
							}
						}
						using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
						{
							using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
							{
								using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
								{
									sqlDataAdapter.Fill(this.ds, "selectedReader");
								}
							}
						}
					}
				}
				IL_106:;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmRouteEdit));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.txtName = new TextBox();
			this.cbof_RouteID = new ComboBox();
			this.Label8 = new Label();
			this.label2 = new Label();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.label3 = new Label();
			this.btnCopyFromOtherRoute = new Button();
			this.btnStartTimeUpdate = new Button();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.label1 = new Label();
			this.chkAutoAdd = new CheckBox();
			this.dtpTime = new DateTimePicker();
			this.label45 = new Label();
			this.dateBeginHMS1 = new DateTimePicker();
			this.nudMinute = new NumericUpDown();
			this.dgvSelected = new DataGridView();
			this.NextDay = new DataGridViewCheckBoxColumn();
			this.Cost = new DataGridViewTextBoxColumn();
			this.f_SN2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.Label10 = new Label();
			this.dgvOptional = new DataGridView();
			this.f_NextDay1 = new DataGridViewTextBoxColumn();
			this.f_patroltime1 = new DataGridViewTextBoxColumn();
			this.f_Sn = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.btnDeleteAllReaders = new Button();
			this.Label11 = new Label();
			this.btnDeleteOneReader = new Button();
			this.btnAddAllReaders = new Button();
			this.btnAddOneReader = new Button();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.nudMinute).BeginInit();
			((ISupportInitialize)this.dgvSelected).BeginInit();
			((ISupportInitialize)this.dgvOptional).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.txtName, "txtName");
			this.txtName.Name = "txtName";
			this.cbof_RouteID.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_RouteID, "cbof_RouteID");
			this.cbof_RouteID.Name = "cbof_RouteID";
			this.Label8.BackColor = Color.Transparent;
			this.Label8.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label8, "Label8");
			this.Label8.Name = "Label8";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabPage1.BackgroundImage = Resources.pMain_content_bkg;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.btnCopyFromOtherRoute);
			this.tabPage1.Controls.Add(this.btnStartTimeUpdate);
			this.tabPage1.Controls.Add(this.radioButton2);
			this.tabPage1.Controls.Add(this.radioButton1);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.chkAutoAdd);
			this.tabPage1.Controls.Add(this.dtpTime);
			this.tabPage1.Controls.Add(this.label45);
			this.tabPage1.Controls.Add(this.dateBeginHMS1);
			this.tabPage1.Controls.Add(this.nudMinute);
			this.tabPage1.Controls.Add(this.dgvSelected);
			this.tabPage1.Controls.Add(this.Label10);
			this.tabPage1.Controls.Add(this.dgvOptional);
			this.tabPage1.Controls.Add(this.btnDeleteAllReaders);
			this.tabPage1.Controls.Add(this.Label11);
			this.tabPage1.Controls.Add(this.btnDeleteOneReader);
			this.tabPage1.Controls.Add(this.btnAddAllReaders);
			this.tabPage1.Controls.Add(this.btnAddOneReader);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			this.btnCopyFromOtherRoute.BackColor = Color.Transparent;
			this.btnCopyFromOtherRoute.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCopyFromOtherRoute, "btnCopyFromOtherRoute");
			this.btnCopyFromOtherRoute.ForeColor = Color.White;
			this.btnCopyFromOtherRoute.Name = "btnCopyFromOtherRoute";
			this.btnCopyFromOtherRoute.UseVisualStyleBackColor = false;
			this.btnCopyFromOtherRoute.Click += new EventHandler(this.btnCopyFromOtherRoute_Click);
			this.btnStartTimeUpdate.BackColor = Color.Transparent;
			this.btnStartTimeUpdate.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnStartTimeUpdate, "btnStartTimeUpdate");
			this.btnStartTimeUpdate.ForeColor = Color.White;
			this.btnStartTimeUpdate.Name = "btnStartTimeUpdate";
			this.btnStartTimeUpdate.UseVisualStyleBackColor = false;
			this.btnStartTimeUpdate.Click += new EventHandler(this.btnStartTimeUpdate_Click);
			componentResourceManager.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.ForeColor = Color.White;
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.Checked = true;
			this.radioButton1.ForeColor = Color.White;
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.chkAutoAdd, "chkAutoAdd");
			this.chkAutoAdd.Checked = true;
			this.chkAutoAdd.CheckState = CheckState.Checked;
			this.chkAutoAdd.ForeColor = Color.White;
			this.chkAutoAdd.Name = "chkAutoAdd";
			this.chkAutoAdd.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.dtpTime, "dtpTime");
			this.dtpTime.Name = "dtpTime";
			this.dtpTime.ShowUpDown = true;
			this.dtpTime.Value = new DateTime(2012, 6, 12, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label45, "label45");
			this.label45.BackColor = Color.Transparent;
			this.label45.ForeColor = Color.White;
			this.label45.Name = "label45";
			componentResourceManager.ApplyResources(this.dateBeginHMS1, "dateBeginHMS1");
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.nudMinute, "nudMinute");
			NumericUpDown arg_865_0 = this.nudMinute;
			int[] array = new int[4];
			array[0] = 2400;
			arg_865_0.Maximum = new decimal(array);
			this.nudMinute.Name = "nudMinute";
			NumericUpDown arg_895_0 = this.nudMinute;
			int[] array2 = new int[4];
			array2[0] = 30;
			arg_895_0.Value = new decimal(array2);
			this.dgvSelected.AllowUserToAddRows = false;
			this.dgvSelected.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dgvSelected, "dgvSelected");
			this.dgvSelected.BackgroundColor = Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelected.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelected.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelected.Columns.AddRange(new DataGridViewColumn[]
			{
				this.NextDay,
				this.Cost,
				this.f_SN2,
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelected.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelected.EnableHeadersVisualStyles = false;
			this.dgvSelected.Name = "dgvSelected";
			this.dgvSelected.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvSelected.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvSelected.RowTemplate.Height = 23;
			this.dgvSelected.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSelected.DoubleClick += new EventHandler(this.btnDeleteOneReader_Click);
			this.NextDay.Frozen = true;
			componentResourceManager.ApplyResources(this.NextDay, "NextDay");
			this.NextDay.Name = "NextDay";
			this.NextDay.ReadOnly = true;
			this.NextDay.Resizable = DataGridViewTriState.True;
			componentResourceManager.ApplyResources(this.Cost, "Cost");
			this.Cost.Name = "Cost";
			this.Cost.ReadOnly = true;
			this.Cost.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_SN2, "f_SN2");
			this.f_SN2.Name = "f_SN2";
			this.f_SN2.ReadOnly = true;
			this.f_SN2.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Label10.BackColor = Color.Transparent;
			this.Label10.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label10, "Label10");
			this.Label10.Name = "Label10";
			this.dgvOptional.AllowUserToAddRows = false;
			this.dgvOptional.AllowUserToDeleteRows = false;
			this.dgvOptional.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvOptional, "dgvOptional");
			this.dgvOptional.BackgroundColor = Color.White;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.dgvOptional.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvOptional.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvOptional.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_NextDay1,
				this.f_patroltime1,
				this.f_Sn,
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.f_Selected
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Window;
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.dgvOptional.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvOptional.EnableHeadersVisualStyles = false;
			this.dgvOptional.Name = "dgvOptional";
			this.dgvOptional.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvOptional.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvOptional.RowTemplate.Height = 23;
			this.dgvOptional.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvOptional.DoubleClick += new EventHandler(this.btnAddOneReader_Click);
			componentResourceManager.ApplyResources(this.f_NextDay1, "f_NextDay1");
			this.f_NextDay1.Name = "f_NextDay1";
			this.f_NextDay1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_patroltime1, "f_patroltime1");
			this.f_patroltime1.Name = "f_patroltime1";
			this.f_patroltime1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Sn, "f_Sn");
			this.f_Sn.Name = "f_Sn";
			this.f_Sn.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected, "f_Selected");
			this.f_Selected.Name = "f_Selected";
			this.f_Selected.ReadOnly = true;
			this.btnDeleteAllReaders.BackColor = Color.Transparent;
			this.btnDeleteAllReaders.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteAllReaders, "btnDeleteAllReaders");
			this.btnDeleteAllReaders.ForeColor = Color.White;
			this.btnDeleteAllReaders.Name = "btnDeleteAllReaders";
			this.btnDeleteAllReaders.UseVisualStyleBackColor = false;
			this.btnDeleteAllReaders.Click += new EventHandler(this.btnDeleteAllReaders_Click);
			this.Label11.BackColor = Color.Transparent;
			this.Label11.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.Name = "Label11";
			this.btnDeleteOneReader.BackColor = Color.Transparent;
			this.btnDeleteOneReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteOneReader, "btnDeleteOneReader");
			this.btnDeleteOneReader.ForeColor = Color.White;
			this.btnDeleteOneReader.Name = "btnDeleteOneReader";
			this.btnDeleteOneReader.UseVisualStyleBackColor = false;
			this.btnDeleteOneReader.Click += new EventHandler(this.btnDeleteOneReader_Click);
			this.btnAddAllReaders.BackColor = Color.Transparent;
			this.btnAddAllReaders.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddAllReaders, "btnAddAllReaders");
			this.btnAddAllReaders.ForeColor = Color.White;
			this.btnAddAllReaders.Name = "btnAddAllReaders";
			this.btnAddAllReaders.UseVisualStyleBackColor = false;
			this.btnAddAllReaders.Click += new EventHandler(this.btnAddAllReaders_Click);
			this.btnAddOneReader.BackColor = Color.Transparent;
			this.btnAddOneReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddOneReader, "btnAddOneReader");
			this.btnAddOneReader.ForeColor = Color.White;
			this.btnAddOneReader.Name = "btnAddOneReader";
			this.btnAddOneReader.UseVisualStyleBackColor = false;
			this.btnAddOneReader.Click += new EventHandler(this.btnAddOneReader_Click);
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtName);
			base.Controls.Add(this.cbof_RouteID);
			base.Controls.Add(this.Label8);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Name = "dfrmRouteEdit";
			base.Load += new EventHandler(this.dfrmMealOption_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((ISupportInitialize)this.nudMinute).EndInit();
			((ISupportInitialize)this.dgvSelected).EndInit();
			((ISupportInitialize)this.dgvOptional).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
