using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmEditUserFile : frmN3000
	{
		private IContainer components;

		private DataGridView dataGridView1;

		internal Button btnOK;

		internal Button btnCancel;

		internal Button btnLoad;

		private OpenFileDialog openFileDialog1;

		internal Button btnFind;

		internal Button btnLoadFromDB;

		private DataGridViewTextBoxColumn f_CardNO;

		private DataGridViewTextBoxColumn f_UserName;

		private DataTable tb;

		private DataView dv;

		private dfrmFind dfrmFind1;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmEditUserFile));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.openFileDialog1 = new OpenFileDialog();
			this.btnLoadFromDB = new Button();
			this.btnFind = new Button();
			this.btnLoad = new Button();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.dataGridView1 = new DataGridView();
			this.f_CardNO = new DataGridViewTextBoxColumn();
			this.f_UserName = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.openFileDialog1.FileName = "openFileDialog1";
			componentResourceManager.ApplyResources(this.btnLoadFromDB, "btnLoadFromDB");
			this.btnLoadFromDB.BackColor = Color.Transparent;
			this.btnLoadFromDB.BackgroundImage = Resources.pMain_button_normal;
			this.btnLoadFromDB.ForeColor = Color.White;
			this.btnLoadFromDB.Name = "btnLoadFromDB";
			this.btnLoadFromDB.UseVisualStyleBackColor = false;
			this.btnLoadFromDB.Click += new EventHandler(this.btnLoadFromDB_Click);
			componentResourceManager.ApplyResources(this.btnFind, "btnFind");
			this.btnFind.BackColor = Color.Transparent;
			this.btnFind.BackgroundImage = Resources.pMain_button_normal;
			this.btnFind.ForeColor = Color.White;
			this.btnFind.Name = "btnFind";
			this.btnFind.UseVisualStyleBackColor = false;
			this.btnFind.Click += new EventHandler(this.btnFind_Click);
			componentResourceManager.ApplyResources(this.btnLoad, "btnLoad");
			this.btnLoad.BackColor = Color.Transparent;
			this.btnLoad.BackgroundImage = Resources.pMain_button_normal;
			this.btnLoad.ForeColor = Color.White;
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.UseVisualStyleBackColor = false;
			this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			dataGridViewCellStyle.BackColor = Color.FromArgb(192, 255, 255);
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.BackgroundColor = Color.White;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_CardNO,
				this.f_UserName
			});
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			componentResourceManager.ApplyResources(this.f_CardNO, "f_CardNO");
			this.f_CardNO.MaxInputLength = 10;
			this.f_CardNO.Name = "f_CardNO";
			this.f_UserName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_UserName, "f_UserName");
			this.f_UserName.MaxInputLength = 32;
			this.f_UserName.Name = "f_UserName";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnLoadFromDB);
			base.Controls.Add(this.btnFind);
			base.Controls.Add(this.btnLoad);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.dataGridView1);
			base.Name = "dfrmEditUserFile";
			base.FormClosing += new FormClosingEventHandler(this.dfrmEditUserFile_FormClosing);
			base.Load += new EventHandler(this.dfrmSystemParam_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmSystemParam_KeyDown);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmEditUserFile()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				using (DataView dataView = new DataView((this.dataGridView1.DataSource as DataView).Table))
				{
					dataView.Sort = "f_CardNO ASC";
					if (dataView.Count > 0)
					{
						uint num = 0u;
						string objToStr = null;
						for (int i = 0; i < dataView.Count; i++)
						{
							if (dataView[i]["f_CardNO"] == null)
							{
								XMessageBox.Show(CommonStr.strCheckCard);
								return;
							}
							if (string.IsNullOrEmpty(dataView[i]["f_CardNO"].ToString()))
							{
								XMessageBox.Show(CommonStr.strCheckCard);
								return;
							}
							if (uint.Parse(dataView[i]["f_CardNO"].ToString()) == 0u)
							{
								XMessageBox.Show(CommonStr.strCheckCard);
								return;
							}
							if (num == uint.Parse(dataView[i]["f_CardNO"].ToString()))
							{
								XMessageBox.Show(string.Format("{0}:{1}\r\n{2}\r\n{3}", new object[]
								{
									CommonStr.strCheckCard,
									num.ToString(),
									wgTools.SetObjToStr(objToStr),
									wgTools.SetObjToStr(dataView[i]["f_ConsumerName"])
								}));
								return;
							}
							num = uint.Parse(dataView[i]["f_CardNO"].ToString());
							objToStr = wgTools.SetObjToStr(dataView[i]["f_ConsumerName"]);
						}
					}
				}
				string text = string.Concat(new string[]
				{
					wgAppConfig.Path4Doc(),
					wgAppConfig.dbWEBUserName,
					"_",
					DateTime.Now.ToString("yyyyMMddHHmmss"),
					".xml"
				});
				using (StringWriter stringWriter = new StringWriter())
				{
					(this.dataGridView1.DataSource as DataView).Table.WriteXml(stringWriter, XmlWriteMode.WriteSchema, true);
					using (StreamWriter streamWriter = new StreamWriter(text, false))
					{
						streamWriter.Write(stringWriter.ToString());
					}
				}
				XMessageBox.Show((sender as Button).Text + "\r\n\r\n" + text);
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmSystemParam_Load(object sender, EventArgs e)
		{
			this.btnLoadFromDB.Visible = !string.IsNullOrEmpty(wgAppConfig.dbConString);
			try
			{
				if (this.tb != null)
				{
					this.tb.Dispose();
				}
				this.tb = new DataTable();
				this.tb.TableName = wgAppConfig.dbWEBUserName;
				this.tb.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
				this.tb.Columns.Add("f_ConsumerName");
				this.tb.AcceptChanges();
				this.dv = new DataView(this.tb);
				this.dv.Sort = "f_CardNO ASC";
				this.dataGridView1.AutoGenerateColumns = false;
				this.dataGridView1.DataSource = this.dv;
				int num = 0;
				while (num < this.dv.Table.Columns.Count && num < this.dataGridView1.ColumnCount)
				{
					this.dataGridView1.Columns[num].DataPropertyName = this.dv.Table.Columns[num].ColumnName;
					num++;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.WindowText;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmSystemParam_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
				{
					this.dataGridView1.Columns[i].Visible = true;
				}
				(this.dataGridView1.DataSource as DataView).RowFilter = "";
			}
			if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
			{
				if (this.dfrmFind1 == null)
				{
					this.dfrmFind1 = new dfrmFind();
				}
				this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
			}
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Filter = " (*.xml)|*.xml| (*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				this.openFileDialog1.Title = (sender as Button).Text;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				string fileName = this.openFileDialog1.FileName;
				string text = fileName;
				if (File.Exists(text))
				{
					if (this.tb != null)
					{
						this.tb.Dispose();
					}
					this.tb = new DataTable();
					this.tb.TableName = wgAppConfig.dbWEBUserName;
					this.tb.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
					this.tb.Columns.Add("f_ConsumerName");
					this.tb.ReadXml(text);
					this.tb.AcceptChanges();
					if (this.dv != null)
					{
						this.dv.Dispose();
					}
					this.dv = new DataView(this.tb);
					this.dv.Sort = "f_CardNO ASC";
					this.dataGridView1.AutoGenerateColumns = false;
					this.dataGridView1.DataSource = this.dv;
					int num = 0;
					while (num < this.dv.Table.Columns.Count && num < this.dataGridView1.ColumnCount)
					{
						this.dataGridView1.Columns[num].DataPropertyName = this.dv.Table.Columns[num].ColumnName;
						num++;
					}
					this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.WindowText;
					return;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			XMessageBox.Show((sender as Button).Text + " " + CommonStr.strFailed);
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			if (this.dfrmFind1 == null)
			{
				this.dfrmFind1 = new dfrmFind();
			}
			this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
		}

		private void dfrmEditUserFile_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void btnLoadFromDB_Click(object sender, EventArgs e)
		{
			try
			{
				string text = string.Format(" SELECT  f_CardNO, f_ConsumerName   ", new object[0]);
				text += " FROM t_b_Consumer ";
				text += " WHERE f_CardNO > 0 ";
				text += " ORDER BY f_CardNO ASC ";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							this.tb = new DataTable(wgAppConfig.dbWEBUserName);
							sqlDataAdapter.Fill(this.tb);
							this.dv = new DataView(this.tb);
							this.dv.Sort = "f_CardNO ASC";
							this.dataGridView1.AutoGenerateColumns = false;
							this.dataGridView1.DataSource = this.dv;
							int num = 0;
							while (num < this.dv.Table.Columns.Count && num < this.dataGridView1.ColumnCount)
							{
								this.dataGridView1.Columns[num].DataPropertyName = this.dv.Table.Columns[num].ColumnName;
								num++;
							}
						}
					}
				}
				this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}
	}
}
