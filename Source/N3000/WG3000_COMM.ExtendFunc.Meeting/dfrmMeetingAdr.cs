using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class dfrmMeetingAdr : frmN3000
	{
		private Container components;

		internal Button btnCancel;

		internal GroupBox GroupBox1;

		internal Label Label11;

		internal ListBox lstMeetingAdr;

		internal Button btnAddMeetingAdr;

		internal Button btnDeleteMeetingAdr;

		private DataGridView dgvSelected;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		internal Button btnSelectReader;

		private DataSet ds = new DataSet("dsMeetingAdr");

		private DataView dv;

		public dfrmMeetingAdr()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMeetingAdr));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.btnCancel = new Button();
			this.GroupBox1 = new GroupBox();
			this.dgvSelected = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.btnSelectReader = new Button();
			this.Label11 = new Label();
			this.lstMeetingAdr = new ListBox();
			this.btnAddMeetingAdr = new Button();
			this.btnDeleteMeetingAdr = new Button();
			this.GroupBox1.SuspendLayout();
			((ISupportInitialize)this.dgvSelected).BeginInit();
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
			this.GroupBox1.Controls.Add(this.btnSelectReader);
			this.GroupBox1.Controls.Add(this.Label11);
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
			this.btnSelectReader.BackColor = Color.Transparent;
			this.btnSelectReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSelectReader, "btnSelectReader");
			this.btnSelectReader.ForeColor = Color.White;
			this.btnSelectReader.Name = "btnSelectReader";
			this.btnSelectReader.UseVisualStyleBackColor = false;
			this.btnSelectReader.Click += new EventHandler(this.btnSelectReader_Click);
			this.Label11.BackColor = Color.Transparent;
			this.Label11.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.Name = "Label11";
			componentResourceManager.ApplyResources(this.lstMeetingAdr, "lstMeetingAdr");
			this.lstMeetingAdr.Name = "lstMeetingAdr";
			this.lstMeetingAdr.SelectedIndexChanged += new EventHandler(this.lstMeetingAdr_SelectedIndexChanged);
			this.lstMeetingAdr.DoubleClick += new EventHandler(this.lstMeetingAdr_DoubleClick);
			this.btnAddMeetingAdr.BackColor = Color.Transparent;
			this.btnAddMeetingAdr.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddMeetingAdr, "btnAddMeetingAdr");
			this.btnAddMeetingAdr.ForeColor = Color.White;
			this.btnAddMeetingAdr.Name = "btnAddMeetingAdr";
			this.btnAddMeetingAdr.UseVisualStyleBackColor = false;
			this.btnAddMeetingAdr.Click += new EventHandler(this.btnAddMeetingAdr_Click);
			this.btnDeleteMeetingAdr.BackColor = Color.Transparent;
			this.btnDeleteMeetingAdr.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteMeetingAdr, "btnDeleteMeetingAdr");
			this.btnDeleteMeetingAdr.ForeColor = Color.White;
			this.btnDeleteMeetingAdr.Name = "btnDeleteMeetingAdr";
			this.btnDeleteMeetingAdr.UseVisualStyleBackColor = false;
			this.btnDeleteMeetingAdr.Click += new EventHandler(this.btnDeleteMeetingAdr_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnDeleteMeetingAdr);
			base.Controls.Add(this.btnAddMeetingAdr);
			base.Controls.Add(this.lstMeetingAdr);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmMeetingAdr";
			base.Load += new EventHandler(this.dfrmMeetingAdr_Load);
			this.GroupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dgvSelected).EndInit();
			base.ResumeLayout(false);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
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
				this.ds.Clear();
				SqlCommand selectCommand = new SqlCommand("Select DISTINCT f_MeetingAdr  from t_d_MeetingAdr ", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
				sqlDataAdapter.Fill(this.ds, "t_d_MeetingAdr");
				this.lstMeetingAdr.DisplayMember = "f_MeetingAdr";
				this.lstMeetingAdr.DataSource = this.ds.Tables["t_d_MeetingAdr"];
				selectCommand = new SqlCommand("Select t_b_reader.f_ReaderID, t_b_reader.f_ReaderName, 1 as f_Selected,t_d_MeetingAdr.f_MeetingAdr from t_b_reader,t_d_MeetingAdr  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_MeetingAdr.f_ReaderID ", connection);
				sqlDataAdapter = new SqlDataAdapter(selectCommand);
				sqlDataAdapter.Fill(this.ds, "t_d_MeetingAdrReader");
				this.dv = new DataView(this.ds.Tables["t_d_MeetingAdrReader"]);
				this.dv.RowFilter = "1<0";
				this.dv.Sort = "f_ReaderID ASC ";
				if (this.lstMeetingAdr.SelectedItems.Count > 0)
				{
					this.dv.RowFilter = " f_MeetingAdr = " + wgTools.PrepareStr(((DataRowView)this.lstMeetingAdr.SelectedItems[0]).Row[0]);
					this.btnSelectReader.Enabled = true;
					this.btnDeleteMeetingAdr.Enabled = true;
				}
				DataTable dataTable = this.ds.Tables["t_d_MeetingAdrReader"];
				for (int i = 0; i < this.dgvSelected.Columns.Count; i++)
				{
					this.dgvSelected.Columns[i].DataPropertyName = dataTable.Columns[i].ColumnName;
				}
				this.dgvSelected.AutoGenerateColumns = false;
				this.dgvSelected.DataSource = this.dv;
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

		public void loadData_Acc()
		{
			OleDbConnection connection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				this.ds.Clear();
				OleDbCommand selectCommand = new OleDbCommand("Select DISTINCT f_MeetingAdr  from t_d_MeetingAdr ", connection);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "t_d_MeetingAdr");
				this.lstMeetingAdr.DisplayMember = "f_MeetingAdr";
				this.lstMeetingAdr.DataSource = this.ds.Tables["t_d_MeetingAdr"];
				selectCommand = new OleDbCommand("Select t_b_reader.f_ReaderID, t_b_reader.f_ReaderName, 1 as f_Selected,t_d_MeetingAdr.f_MeetingAdr from t_b_reader,t_d_MeetingAdr  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_MeetingAdr.f_ReaderID ", connection);
				oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "t_d_MeetingAdrReader");
				this.dv = new DataView(this.ds.Tables["t_d_MeetingAdrReader"]);
				this.dv.RowFilter = "1<0";
				this.dv.Sort = "f_ReaderID ASC ";
				if (this.lstMeetingAdr.SelectedItems.Count > 0)
				{
					this.dv.RowFilter = " f_MeetingAdr = " + wgTools.PrepareStr(((DataRowView)this.lstMeetingAdr.SelectedItems[0]).Row[0]);
					this.btnSelectReader.Enabled = true;
					this.btnDeleteMeetingAdr.Enabled = true;
				}
				DataTable dataTable = this.ds.Tables["t_d_MeetingAdrReader"];
				for (int i = 0; i < this.dgvSelected.Columns.Count; i++)
				{
					this.dgvSelected.Columns[i].DataPropertyName = dataTable.Columns[i].ColumnName;
				}
				this.dgvSelected.AutoGenerateColumns = false;
				this.dgvSelected.DataSource = this.dv;
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

		private void dfrmMeetingAdr_Load(object sender, EventArgs e)
		{
			this.loadData();
			bool flag = false;
			string funName = "mnuMeeting";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAddMeetingAdr.Visible = false;
				this.btnDeleteMeetingAdr.Visible = false;
				this.btnSelectReader.Visible = false;
			}
		}

		private void btnAddMeetingAdr_Click(object sender, EventArgs e)
		{
			try
			{
				dfrmMeetingAdrSet dfrmMeetingAdrSet = new dfrmMeetingAdrSet();
				if (dfrmMeetingAdrSet.ShowDialog() == DialogResult.OK)
				{
					this.loadData();
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

		private void lstMeetingAdr_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dv != null)
				{
					if (this.lstMeetingAdr.SelectedItems.Count > 0)
					{
						this.dv.RowFilter = " f_MeetingAdr = " + wgTools.PrepareStr(((DataRowView)this.lstMeetingAdr.SelectedItems[0]).Row[0]);
						this.btnSelectReader.Enabled = true;
						this.btnDeleteMeetingAdr.Enabled = true;
					}
					else
					{
						this.dv.RowFilter = " 1<0 ";
						this.btnSelectReader.Enabled = false;
						this.btnDeleteMeetingAdr.Enabled = false;
					}
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

		private void btnSelectReader_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstMeetingAdr.SelectedItems.Count > 0 && new dfrmMeetingAdrSet
				{
					curMeetingAdr = ((DataRowView)this.lstMeetingAdr.SelectedItems[0]).Row[0].ToString()
				}.ShowDialog() == DialogResult.OK)
				{
					this.loadData();
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

		private void lstMeetingAdr_DoubleClick(object sender, EventArgs e)
		{
			this.btnSelectReader.PerformClick();
		}

		private void btnDeleteMeetingAdr_Click(object sender, EventArgs e)
		{
			try
			{
				if (XMessageBox.Show(this.btnDeleteMeetingAdr.Text + ":" + ((DataRowView)this.lstMeetingAdr.SelectedItems[0]).Row[0].ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					string strSql = " DELETE FROM t_d_MeetingAdr  WHERE t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(((DataRowView)this.lstMeetingAdr.SelectedItems[0]).Row[0]);
					wgAppConfig.runUpdateSql(strSql);
					this.loadData();
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
	}
}
