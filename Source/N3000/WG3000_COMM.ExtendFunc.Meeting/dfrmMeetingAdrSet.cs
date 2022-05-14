using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class dfrmMeetingAdrSet : frmN3000
	{
		public string curMeetingAdr = "";

		private Container components;

		internal Button btnCancel;

		internal GroupBox GroupBox1;

		internal Label Label11;

		internal Button btnAddAllReaders;

		internal Label Label10;

		internal Button btnAddOneReader;

		internal Button btnDeleteOneReader;

		internal Button btnDeleteAllReaders;

		internal Button btnOK;

		internal Label Label1;

		private DataGridView dgvSelected;

		private DataGridView dgvOptional;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		internal TextBox txtMeetingAdr;

		private DataSet ds = new DataSet("dsMeetingAdr");

		private DataView dv;

		private DataView dvSelected;

		private DataTable dt;

		public dfrmMeetingAdrSet()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMeetingAdrSet));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.btnCancel = new Button();
			this.GroupBox1 = new GroupBox();
			this.dgvSelected = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dgvOptional = new DataGridView();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.Label11 = new Label();
			this.btnAddAllReaders = new Button();
			this.Label10 = new Label();
			this.btnAddOneReader = new Button();
			this.btnDeleteOneReader = new Button();
			this.btnDeleteAllReaders = new Button();
			this.btnOK = new Button();
			this.Label1 = new Label();
			this.txtMeetingAdr = new TextBox();
			this.GroupBox1.SuspendLayout();
			((ISupportInitialize)this.dgvSelected).BeginInit();
			((ISupportInitialize)this.dgvOptional).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.GroupBox1.Controls.Add(this.dgvSelected);
			this.GroupBox1.Controls.Add(this.dgvOptional);
			this.GroupBox1.Controls.Add(this.Label11);
			this.GroupBox1.Controls.Add(this.btnAddAllReaders);
			this.GroupBox1.Controls.Add(this.Label10);
			this.GroupBox1.Controls.Add(this.btnAddOneReader);
			this.GroupBox1.Controls.Add(this.btnDeleteOneReader);
			this.GroupBox1.Controls.Add(this.btnDeleteAllReaders);
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			this.dgvSelected.AllowUserToAddRows = false;
			this.dgvSelected.AllowUserToDeleteRows = false;
			this.dgvSelected.AllowUserToOrderColumns = true;
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
			this.dgvSelected.DoubleClick += new EventHandler(this.dgvSelected_DoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
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
			this.dgvOptional.DoubleClick += new EventHandler(this.dgvOptional_DoubleClick);
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
			this.Label11.BackColor = Color.Transparent;
			this.Label11.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.Name = "Label11";
			this.btnAddAllReaders.BackColor = Color.Transparent;
			this.btnAddAllReaders.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddAllReaders, "btnAddAllReaders");
			this.btnAddAllReaders.ForeColor = Color.White;
			this.btnAddAllReaders.Name = "btnAddAllReaders";
			this.btnAddAllReaders.UseVisualStyleBackColor = false;
			this.btnAddAllReaders.Click += new EventHandler(this.btnAddAllReaders_Click);
			this.Label10.BackColor = Color.Transparent;
			this.Label10.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label10, "Label10");
			this.Label10.Name = "Label10";
			this.btnAddOneReader.BackColor = Color.Transparent;
			this.btnAddOneReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddOneReader, "btnAddOneReader");
			this.btnAddOneReader.ForeColor = Color.White;
			this.btnAddOneReader.Name = "btnAddOneReader";
			this.btnAddOneReader.UseVisualStyleBackColor = false;
			this.btnAddOneReader.Click += new EventHandler(this.btnAddOneReader_Click);
			this.btnDeleteOneReader.BackColor = Color.Transparent;
			this.btnDeleteOneReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteOneReader, "btnDeleteOneReader");
			this.btnDeleteOneReader.ForeColor = Color.White;
			this.btnDeleteOneReader.Name = "btnDeleteOneReader";
			this.btnDeleteOneReader.UseVisualStyleBackColor = false;
			this.btnDeleteOneReader.Click += new EventHandler(this.btnDeleteOneReader_Click);
			this.btnDeleteAllReaders.BackColor = Color.Transparent;
			this.btnDeleteAllReaders.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteAllReaders, "btnDeleteAllReaders");
			this.btnDeleteAllReaders.ForeColor = Color.White;
			this.btnDeleteAllReaders.Name = "btnDeleteAllReaders";
			this.btnDeleteAllReaders.UseVisualStyleBackColor = false;
			this.btnDeleteAllReaders.Click += new EventHandler(this.btnDeleteAllReaders_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			componentResourceManager.ApplyResources(this.txtMeetingAdr, "txtMeetingAdr");
			this.txtMeetingAdr.Name = "txtMeetingAdr";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtMeetingAdr);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmMeetingAdrSet";
			base.Load += new EventHandler(this.dfrmMeetingAdr_Load);
			this.GroupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dgvSelected).EndInit();
			((ISupportInitialize)this.dgvOptional).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmMeetingAdr_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmMeetingAdr_Load_Acc(sender, e);
				return;
			}
			SqlConnection connection = new SqlConnection(wgAppConfig.dbConString);
			SqlCommand selectCommand = new SqlCommand();
			try
			{
				if (this.curMeetingAdr == "")
				{
					selectCommand = new SqlCommand("Select f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )     ", connection);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
					sqlDataAdapter.Fill(this.ds, "optionalReader");
				}
				else
				{
					this.txtMeetingAdr.Text = this.curMeetingAdr;
					this.txtMeetingAdr.ReadOnly = true;
					selectCommand = new SqlCommand("Select f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID NOT IN (SELECT t_d_MeetingAdr.f_ReaderID FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.curMeetingAdr) + ") ", connection);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
					sqlDataAdapter.Fill(this.ds, "optionalReader");
					selectCommand = new SqlCommand("Select f_ReaderID, f_ReaderName, 1 as f_Selected from t_b_reader  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  IN (SELECT t_d_MeetingAdr.f_ReaderID FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.curMeetingAdr) + ") ", connection);
					sqlDataAdapter = new SqlDataAdapter(selectCommand);
					sqlDataAdapter.Fill(this.ds, "optionalReader");
				}
				this.dv = new DataView(this.ds.Tables["optionalReader"]);
				this.dv.RowFilter = " f_Selected = 0";
				this.dvSelected = new DataView(this.ds.Tables["optionalReader"]);
				this.dvSelected.RowFilter = " f_Selected = 1";
				this.dt = this.ds.Tables["optionalReader"];
				try
				{
					DataColumn[] primaryKey = new DataColumn[]
					{
						this.dt.Columns[0]
					};
					this.dt.PrimaryKey = primaryKey;
				}
				catch (Exception)
				{
					throw;
				}
				for (int i = 0; i < this.dgvOptional.Columns.Count; i++)
				{
					this.dgvOptional.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvSelected.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
				}
				this.dv.RowFilter = "f_Selected = 0";
				this.dvSelected.RowFilter = "f_Selected > 0";
				this.dgvOptional.AutoGenerateColumns = false;
				this.dgvOptional.DataSource = this.dv;
				this.dgvSelected.AutoGenerateColumns = false;
				this.dgvSelected.DataSource = this.dvSelected;
				this.dgvOptional.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvSelected.DefaultCellStyle.ForeColor = Color.Black;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dfrmMeetingAdr_Load_Acc(object sender, EventArgs e)
		{
			OleDbConnection connection = new OleDbConnection(wgAppConfig.dbConString);
			OleDbCommand selectCommand = new OleDbCommand();
			try
			{
				if (this.curMeetingAdr == "")
				{
					selectCommand = new OleDbCommand("Select f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )   ", connection);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
					oleDbDataAdapter.Fill(this.ds, "optionalReader");
				}
				else
				{
					this.txtMeetingAdr.Text = this.curMeetingAdr;
					this.txtMeetingAdr.ReadOnly = true;
					selectCommand = new OleDbCommand("Select f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID NOT IN (SELECT t_d_MeetingAdr.f_ReaderID FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.curMeetingAdr) + ") ", connection);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
					oleDbDataAdapter.Fill(this.ds, "optionalReader");
					selectCommand = new OleDbCommand("Select f_ReaderID, f_ReaderName, 1 as f_Selected from t_b_reader  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  IN (SELECT t_d_MeetingAdr.f_ReaderID FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.curMeetingAdr) + ") ", connection);
					oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
					oleDbDataAdapter.Fill(this.ds, "optionalReader");
				}
				this.dv = new DataView(this.ds.Tables["optionalReader"]);
				this.dv.RowFilter = " f_Selected = 0";
				this.dvSelected = new DataView(this.ds.Tables["optionalReader"]);
				this.dvSelected.RowFilter = " f_Selected = 1";
				this.dt = this.ds.Tables["optionalReader"];
				try
				{
					DataColumn[] primaryKey = new DataColumn[]
					{
						this.dt.Columns[0]
					};
					this.dt.PrimaryKey = primaryKey;
				}
				catch (Exception)
				{
					throw;
				}
				for (int i = 0; i < this.dgvOptional.Columns.Count; i++)
				{
					this.dgvOptional.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvSelected.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
				}
				this.dv.RowFilter = "f_Selected = 0";
				this.dvSelected.RowFilter = "f_Selected > 0";
				this.dgvOptional.AutoGenerateColumns = false;
				this.dgvOptional.DataSource = this.dv;
				this.dgvSelected.AutoGenerateColumns = false;
				this.dgvSelected.DataSource = this.dvSelected;
				this.dgvOptional.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvSelected.DefaultCellStyle.ForeColor = Color.Black;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnAddAllReaders_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 1;
				}
				this.dt.AcceptChanges();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnAddOneReader_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvOptional);
		}

		private void btnDeleteOneReader_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelected);
		}

		private void btnDeleteAllReaders_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 0;
				}
				this.dt.AcceptChanges();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			try
			{
				this.txtMeetingAdr.Text = this.txtMeetingAdr.Text.Trim();
				if (this.txtMeetingAdr.Text == "")
				{
					XMessageBox.Show(CommonStr.strMeetingNameIsEmpty);
				}
				else if (this.dvSelected.Count <= 0)
				{
					XMessageBox.Show(CommonStr.strMeetingSelectReaderAsSign);
				}
				else
				{
					string text;
					if (!this.txtMeetingAdr.ReadOnly)
					{
						SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
						SqlCommand sqlCommand = new SqlCommand();
						text = " SELECT * FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.txtMeetingAdr.Text);
						sqlCommand = new SqlCommand(text, sqlConnection);
						if (sqlConnection.State == ConnectionState.Closed)
						{
							sqlConnection.Open();
						}
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							sqlDataReader.Close();
							XMessageBox.Show(CommonStr.strMeetingNameIsDupliated);
							return;
						}
						sqlDataReader.Close();
					}
					Cursor arg_EF_0 = Cursor.Current;
					text = " DELETE FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.txtMeetingAdr.Text);
					wgAppConfig.runUpdateSql(text);
					if (this.dvSelected.Count > 0)
					{
						for (int i = 0; i <= this.dvSelected.Count - 1; i++)
						{
							text = " INSERT INTO t_d_MeetingAdr";
							text += " (f_MeetingAdr, f_ReaderID) ";
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								" Values(",
								wgTools.PrepareStr(this.txtMeetingAdr.Text),
								",",
								this.dvSelected[i]["f_ReaderID"]
							});
							text += " )";
							wgAppConfig.runUpdateSql(text);
						}
					}
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			try
			{
				this.txtMeetingAdr.Text = this.txtMeetingAdr.Text.Trim();
				if (this.txtMeetingAdr.Text == "")
				{
					XMessageBox.Show(CommonStr.strMeetingNameIsEmpty);
				}
				else if (this.dvSelected.Count <= 0)
				{
					XMessageBox.Show(CommonStr.strMeetingSelectReaderAsSign);
				}
				else
				{
					string text;
					if (!this.txtMeetingAdr.ReadOnly)
					{
						OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
						OleDbCommand oleDbCommand = new OleDbCommand();
						text = " SELECT * FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.txtMeetingAdr.Text);
						oleDbCommand = new OleDbCommand(text, oleDbConnection);
						if (oleDbConnection.State == ConnectionState.Closed)
						{
							oleDbConnection.Open();
						}
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							oleDbDataReader.Close();
							XMessageBox.Show(CommonStr.strMeetingNameIsDupliated);
							return;
						}
						oleDbDataReader.Close();
					}
					Cursor arg_DF_0 = Cursor.Current;
					text = " DELETE FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.txtMeetingAdr.Text);
					wgAppConfig.runUpdateSql(text);
					if (this.dvSelected.Count > 0)
					{
						for (int i = 0; i <= this.dvSelected.Count - 1; i++)
						{
							text = " INSERT INTO t_d_MeetingAdr";
							text += " (f_MeetingAdr, f_ReaderID) ";
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								" Values(",
								wgTools.PrepareStr(this.txtMeetingAdr.Text),
								",",
								this.dvSelected[i]["f_ReaderID"]
							});
							text += " )";
							wgAppConfig.runUpdateSql(text);
						}
					}
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dgvOptional_DoubleClick(object sender, EventArgs e)
		{
			this.btnAddOneReader.PerformClick();
		}

		private void dgvSelected_DoubleClick(object sender, EventArgs e)
		{
			this.btnDeleteOneReader.PerformClick();
		}
	}
}
