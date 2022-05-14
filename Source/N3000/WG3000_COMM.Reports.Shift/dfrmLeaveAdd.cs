using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmLeaveAdd : frmN3000
	{
		private IContainer components;

		private GroupBox groupBox1;

		private Label label3;

		private DataGridView dgvSelectedUsers;

		private DataGridView dgvUsers;

		private Button btnDelAllUsers;

		private Button btnDelOneUser;

		private Button btnAddOneUser;

		private Button btnAddAllUsers;

		private ComboBox cbof_GroupID;

		private Label label4;

		internal TextBox txtf_Notes;

		internal DateTimePicker dtpStartDate;

		internal Label Label5;

		internal Label Label6;

		internal DateTimePicker dtpEndDate;

		internal Label label2;

		internal ComboBox cboHolidayType;

		internal ComboBox cboStart;

		internal ComboBox cboEnd;

		internal Label Label7;

		internal Button btnOK;

		internal Button btnClose;

		private Label lblWait;

		private System.Windows.Forms.Timer timer1;

		private BackgroundWorker backgroundWorker1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn f_SelectedGroup;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn UserID;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewCheckBoxColumn f_SelectedUsers;

		private DataGridViewTextBoxColumn f_GroupID;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private DataTable dt;

		private DataView dv;

		private DataView dvSelected;

		private DataView dv1;

		private DataView dv2;

		private DataSet ds;

		private DataTable dtUser;

		private string strGroupFilter = "";

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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmLeaveAdd));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.groupBox1 = new GroupBox();
			this.lblWait = new Label();
			this.label3 = new Label();
			this.dgvSelectedUsers = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.f_SelectedGroup = new DataGridViewTextBoxColumn();
			this.dgvUsers = new DataGridView();
			this.btnDelAllUsers = new Button();
			this.btnDelOneUser = new Button();
			this.btnAddOneUser = new Button();
			this.btnAddAllUsers = new Button();
			this.cbof_GroupID = new ComboBox();
			this.label4 = new Label();
			this.txtf_Notes = new TextBox();
			this.dtpStartDate = new DateTimePicker();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.dtpEndDate = new DateTimePicker();
			this.label2 = new Label();
			this.cboHolidayType = new ComboBox();
			this.cboStart = new ComboBox();
			this.cboEnd = new ComboBox();
			this.Label7 = new Label();
			this.btnOK = new Button();
			this.btnClose = new Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.backgroundWorker1 = new BackgroundWorker();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dgvSelectedUsers).BeginInit();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.lblWait);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.dgvSelectedUsers);
			this.groupBox1.Controls.Add(this.dgvUsers);
			this.groupBox1.Controls.Add(this.btnDelAllUsers);
			this.groupBox1.Controls.Add(this.btnDelOneUser);
			this.groupBox1.Controls.Add(this.btnAddOneUser);
			this.groupBox1.Controls.Add(this.btnAddAllUsers);
			this.groupBox1.Controls.Add(this.cbof_GroupID);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.lblWait, "lblWait");
			this.lblWait.BorderStyle = BorderStyle.FixedSingle;
			this.lblWait.Name = "lblWait";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.dgvSelectedUsers, "dgvSelectedUsers");
			this.dgvSelectedUsers.AllowUserToAddRows = false;
			this.dgvSelectedUsers.AllowUserToDeleteRows = false;
			this.dgvSelectedUsers.AllowUserToOrderColumns = true;
			this.dgvSelectedUsers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelectedUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelectedUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.dataGridViewCheckBoxColumn1,
				this.f_SelectedGroup
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedUsers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelectedUsers.EnableHeadersVisualStyles = false;
			this.dgvSelectedUsers.Name = "dgvSelectedUsers";
			this.dgvSelectedUsers.ReadOnly = true;
			this.dgvSelectedUsers.RowTemplate.Height = 23;
			this.dgvSelectedUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSelectedUsers.DoubleClick += new EventHandler(this.btnDelOneUser_Click);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedGroup, "f_SelectedGroup");
			this.f_SelectedGroup.Name = "f_SelectedGroup";
			this.f_SelectedGroup.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dgvUsers, "dgvUsers");
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AllowUserToDeleteRows = false;
			this.dgvUsers.AllowUserToOrderColumns = true;
			this.dgvUsers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ConsumerID,
				this.UserID,
				this.ConsumerName,
				this.CardNO,
				this.f_SelectedUsers,
				this.f_GroupID
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Window;
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvUsers.DoubleClick += new EventHandler(this.btnAddOneUser_Click);
			componentResourceManager.ApplyResources(this.btnDelAllUsers, "btnDelAllUsers");
			this.btnDelAllUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelAllUsers.Name = "btnDelAllUsers";
			this.btnDelAllUsers.UseVisualStyleBackColor = true;
			this.btnDelAllUsers.Click += new EventHandler(this.btnDelAllUsers_Click);
			componentResourceManager.ApplyResources(this.btnDelOneUser, "btnDelOneUser");
			this.btnDelOneUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelOneUser.Name = "btnDelOneUser";
			this.btnDelOneUser.UseVisualStyleBackColor = true;
			this.btnDelOneUser.Click += new EventHandler(this.btnDelOneUser_Click);
			componentResourceManager.ApplyResources(this.btnAddOneUser, "btnAddOneUser");
			this.btnAddOneUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneUser.Name = "btnAddOneUser";
			this.btnAddOneUser.UseVisualStyleBackColor = true;
			this.btnAddOneUser.Click += new EventHandler(this.btnAddOneUser_Click);
			componentResourceManager.ApplyResources(this.btnAddAllUsers, "btnAddAllUsers");
			this.btnAddAllUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllUsers.Name = "btnAddAllUsers";
			this.btnAddAllUsers.UseVisualStyleBackColor = true;
			this.btnAddAllUsers.Click += new EventHandler(this.btnAddAllUsers_Click);
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			this.cbof_GroupID.Name = "cbof_GroupID";
			this.cbof_GroupID.SelectedIndexChanged += new EventHandler(this.cbof_GroupID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.txtf_Notes, "txtf_Notes");
			this.txtf_Notes.Name = "txtf_Notes";
			componentResourceManager.ApplyResources(this.dtpStartDate, "dtpStartDate");
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpStartDate.ValueChanged += new EventHandler(this.dtpStartDate_ValueChanged);
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.BackColor = Color.Transparent;
			this.Label5.ForeColor = Color.White;
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.BackColor = Color.Transparent;
			this.Label6.ForeColor = Color.White;
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.dtpEndDate, "dtpEndDate");
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.cboHolidayType, "cboHolidayType");
			this.cboHolidayType.DisplayMember = "f_GroupName";
			this.cboHolidayType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboHolidayType.Name = "cboHolidayType";
			this.cboHolidayType.ValueMember = "f_GroupID";
			componentResourceManager.ApplyResources(this.cboStart, "cboStart");
			this.cboStart.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboStart.Name = "cboStart";
			componentResourceManager.ApplyResources(this.cboEnd, "cboEnd");
			this.cboEnd.DisplayMember = "f_GroupName";
			this.cboEnd.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboEnd.Name = "cboEnd";
			this.cboEnd.ValueMember = "f_GroupID";
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.BackColor = Color.Transparent;
			this.Label7.ForeColor = Color.White;
			this.Label7.Name = "Label7";
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.UserID.DefaultCellStyle = dataGridViewCellStyle6;
			componentResourceManager.ApplyResources(this.UserID, "UserID");
			this.UserID.Name = "UserID";
			this.UserID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ConsumerName, "ConsumerName");
			this.ConsumerName.Name = "ConsumerName";
			this.ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.CardNO, "CardNO");
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedUsers, "f_SelectedUsers");
			this.f_SelectedUsers.Name = "f_SelectedUsers";
			this.f_SelectedUsers.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_GroupID, "f_GroupID");
			this.f_GroupID.Name = "f_GroupID";
			this.f_GroupID.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.txtf_Notes);
			base.Controls.Add(this.dtpStartDate);
			base.Controls.Add(this.Label5);
			base.Controls.Add(this.Label6);
			base.Controls.Add(this.dtpEndDate);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cboHolidayType);
			base.Controls.Add(this.cboStart);
			base.Controls.Add(this.cboEnd);
			base.Controls.Add(this.Label7);
			base.Controls.Add(this.groupBox1);
			base.Name = "dfrmLeaveAdd";
			base.Load += new EventHandler(this.dfrmLeaveAdd_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			((ISupportInitialize)this.dgvUsers).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmLeaveAdd()
		{
			this.InitializeComponent();
		}

		private void loadGroupData()
		{
			icGroup icGroup = new icGroup();
			icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
			for (int i = 0; i < this.arrGroupID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrGroupName[i].ToString()))
				{
					this.cbof_GroupID.Items.Add(CommonStr.strAll);
				}
				else
				{
					this.cbof_GroupID.Items.Add(this.arrGroupName[i].ToString());
				}
			}
			if (this.cbof_GroupID.Items.Count > 0)
			{
				this.cbof_GroupID.SelectedIndex = 0;
			}
		}

		private void dfrmLeaveAdd_Load(object sender, EventArgs e)
		{
			this.cboStart.Items.Clear();
			this.cboStart.Items.AddRange(new string[]
			{
				CommonStr.strAM,
				CommonStr.strPM
			});
			this.cboEnd.Items.Clear();
			this.cboEnd.Items.AddRange(new string[]
			{
				CommonStr.strAM,
				CommonStr.strPM
			});
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
			this.loadGroupData();
			this.ds = new DataSet();
			string cmdText = " SELECT *  FROM [t_a_HolidayType]";
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.ds, "t_a_HolidayType");
						}
					}
				}
				comShift_Acc.localizedHolidayType(this.ds.Tables["t_a_HolidayType"]);
			}
			else
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlDataAdapter.Fill(this.ds, "t_a_HolidayType");
						}
					}
				}
				comShift.localizedHolidayType(this.ds.Tables["t_a_HolidayType"]);
			}
			this.cboHolidayType.Items.Clear();
			for (int i = 0; i <= this.ds.Tables["t_a_HolidayType"].Rows.Count - 1; i++)
			{
				this.cboHolidayType.Items.Add(this.ds.Tables["t_a_HolidayType"].Rows[i]["f_HolidayType"]);
			}
			if (this.cboHolidayType.Items.Count >= 0)
			{
				this.cboHolidayType.SelectedIndex = 0;
			}
			this.dtpStartDate.Value = DateTime.Today;
			this.dtpEndDate.Value = DateTime.Today;
			if (this.cboStart.Items.Count > 0)
			{
				this.cboStart.SelectedIndex = 0;
			}
			if (this.cboEnd.Items.Count > 1)
			{
				this.cboEnd.SelectedIndex = 1;
			}
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			wgAppConfig.setDisplayFormatDate(this.dtpStartDate, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpEndDate, wgTools.DisplayFormat_DateYMDWeek);
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void loadUserData()
		{
			wgTools.WriteLine("loadUserData Start");
			string text = " SELECT f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, 0 as f_Selected, f_GroupID ";
			text += " FROM t_b_Consumer WHERE f_DoorEnabled > 0";
			text += " ORDER BY f_ConsumerNO ASC ";
			this.dtUser = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtUser);
						}
					}
					goto IL_DC;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtUser);
					}
				}
			}
			IL_DC:
			wgTools.WriteLine("da.Fill End");
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dtUser.Columns[0]
				};
				this.dtUser.PrimaryKey = primaryKey;
			}
			catch (Exception)
			{
				throw;
			}
			this.dv = new DataView(this.dtUser);
			this.dvSelected = new DataView(this.dtUser);
			this.dv.RowFilter = "f_Selected = 0";
			this.dvSelected.RowFilter = "f_Selected > 0";
			this.dgvUsers.AutoGenerateColumns = false;
			this.dgvUsers.DataSource = this.dv;
			this.dgvSelectedUsers.AutoGenerateColumns = false;
			this.dgvSelectedUsers.DataSource = this.dvSelected;
			for (int i = 0; i < this.dv.Table.Columns.Count; i++)
			{
				this.dgvUsers.Columns[i].DataPropertyName = this.dtUser.Columns[i].ColumnName;
				this.dgvSelectedUsers.Columns[i].DataPropertyName = this.dtUser.Columns[i].ColumnName;
			}
			wgTools.WriteLine("loadUserData End");
		}

		private void btnAddAllUsers_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			if (this.strGroupFilter == "")
			{
				icConsumerShare.selectAllUsers();
				((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("{0}", icConsumerShare.getOptionalRowfilter());
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("{0}", icConsumerShare.getSelectedRowfilter());
				return;
			}
			wgTools.WriteLine("btnAddAllUsers_Click Start");
			this.dt = ((DataView)this.dgvUsers.DataSource).Table;
			this.dv1 = (DataView)this.dgvUsers.DataSource;
			this.dv2 = (DataView)this.dgvSelectedUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvSelectedUsers.DataSource = null;
			if (!(this.strGroupFilter == ""))
			{
				this.dv = new DataView(this.dt);
				this.dv.RowFilter = this.strGroupFilter;
				for (int i = 0; i < this.dv.Count; i++)
				{
					this.dv[i]["f_Selected"] = icConsumerShare.getSelectedValue();
				}
			}
			this.dgvUsers.DataSource = this.dv1;
			this.dgvSelectedUsers.DataSource = this.dv2;
			wgTools.WriteLine("btnAddAllUsers_Click End");
			Cursor.Current = Cursors.Default;
		}

		private void btnDelAllUsers_Click(object sender, EventArgs e)
		{
			if (this.dgvSelectedUsers.Rows.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				wgTools.WriteLine("btnDelAllUsers_Click Start");
				icConsumerShare.selectNoneUsers();
				if (string.IsNullOrEmpty(this.strGroupFilter))
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("{0}", icConsumerShare.getOptionalRowfilter());
				}
				else
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("({0}) AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				}
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("{0}", icConsumerShare.getSelectedRowfilter());
			}
		}

		private void btnAddOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvUsers, icConsumerShare.iSelectedCurrentNoneMax);
		}

		private void btnDelOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedUsers, icConsumerShare.iSelectedCurrentNoneMax);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			if (this.dgvSelectedUsers.Rows.Count <= 0)
			{
				return;
			}
			if (this.cboHolidayType.Items.Count == 0)
			{
				return;
			}
			int num = 0;
			bool flag = false;
			int i = 0;
			string text = "";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					while (i < this.dgvSelectedUsers.Rows.Count)
					{
						string text2 = "";
						if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
						{
							while (i < this.dgvSelectedUsers.Rows.Count)
							{
								text2 = text2 + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerID"] + ",";
								if (text2.Length > 2000)
								{
									break;
								}
								i++;
							}
							text2 += "0";
						}
						else
						{
							i = this.dgvSelectedUsers.Rows.Count;
						}
						text = "INSERT INTO [t_d_Leave] (f_ConsumerID, f_Value ,f_Value1,f_Value2,f_Value3,f_HolidayType,f_Notes)";
						text += " SELECT t_b_Consumer.f_ConsumerID ";
						text = text + ", " + wgTools.PrepareStr(this.dtpStartDate.Value.ToString("yyyy-MM-dd")) + " AS [f_Value] ";
						text = text + ", " + wgTools.PrepareStr(this.cboStart.Text) + " AS [f_Value1] ";
						text = text + ", " + wgTools.PrepareStr(this.dtpEndDate.Value.ToString("yyyy-MM-dd")) + " AS [f_Value2] ";
						text = text + ", " + wgTools.PrepareStr(this.cboEnd.Text) + " AS [f_Value3] ";
						text = text + ", " + wgTools.PrepareStr(this.cboHolidayType.Text) + " AS [f_HolidayType] ";
						text = text + ", " + wgTools.PrepareStr(this.txtf_Notes.Text) + " AS [f_Notes] ";
						text += " FROM t_b_Consumer";
						if (text2 != "")
						{
							text = text + " WHERE [f_ConsumerID] IN (" + text2 + " ) ";
						}
						sqlCommand.CommandText = text;
						num = sqlCommand.ExecuteNonQuery();
						if (num <= 0)
						{
							flag = false;
							break;
						}
						flag = true;
						wgTools.WriteLine("INSERT INTO [t_d_Leave] End");
					}
				}
			}
			if (flag)
			{
				XMessageBox.Show(string.Concat(new object[]
				{
					(sender as Button).Text,
					"  ",
					num,
					"  ",
					CommonStr.strUnit,
					"  ",
					CommonStr.strSuccessfully
				}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			XMessageBox.Show((sender as Button).Text + " " + CommonStr.strFailed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			if (this.dgvSelectedUsers.Rows.Count <= 0)
			{
				return;
			}
			if (this.cboHolidayType.Items.Count == 0)
			{
				return;
			}
			int num = 0;
			bool flag = false;
			int i = 0;
			string text = "";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					while (i < this.dgvSelectedUsers.Rows.Count)
					{
						string text2 = "";
						if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
						{
							while (i < this.dgvSelectedUsers.Rows.Count)
							{
								text2 = text2 + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerID"] + ",";
								if (text2.Length > 2000)
								{
									break;
								}
								i++;
							}
							text2 += "0";
						}
						else
						{
							i = this.dgvSelectedUsers.Rows.Count;
						}
						text = "INSERT INTO [t_d_Leave] (f_ConsumerID, f_Value ,f_Value1,f_Value2,f_Value3,f_HolidayType,f_Notes)";
						text += " SELECT t_b_Consumer.f_ConsumerID ";
						text = text + ", " + wgTools.PrepareStr(this.dtpStartDate.Value.ToString("yyyy-MM-dd")) + " AS [f_Value] ";
						text = text + ", " + wgTools.PrepareStr(this.cboStart.Text) + " AS [f_Value1] ";
						text = text + ", " + wgTools.PrepareStr(this.dtpEndDate.Value.ToString("yyyy-MM-dd")) + " AS [f_Value2] ";
						text = text + ", " + wgTools.PrepareStr(this.cboEnd.Text) + " AS [f_Value3] ";
						text = text + ", " + wgTools.PrepareStr(this.cboHolidayType.Text) + " AS [f_HolidayType] ";
						text = text + ", " + wgTools.PrepareStr(this.txtf_Notes.Text) + " AS [f_Notes] ";
						text += " FROM t_b_Consumer";
						if (text2 != "")
						{
							text = text + " WHERE [f_ConsumerID] IN (" + text2 + " ) ";
						}
						oleDbCommand.CommandText = text;
						num = oleDbCommand.ExecuteNonQuery();
						if (num <= 0)
						{
							flag = false;
							break;
						}
						flag = true;
						wgTools.WriteLine("INSERT INTO [t_d_Leave] End");
					}
				}
			}
			if (flag)
			{
				XMessageBox.Show(string.Concat(new object[]
				{
					(sender as Button).Text,
					"  ",
					num,
					"  ",
					CommonStr.strUnit,
					"  ",
					CommonStr.strSuccessfully
				}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			XMessageBox.Show((sender as Button).Text + " " + CommonStr.strFailed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void cbof_GroupID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvUsers.DataSource != null)
			{
				DataView dataView = (DataView)this.dgvUsers.DataSource;
				if (this.cbof_GroupID.SelectedIndex < 0 || (this.cbof_GroupID.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
				{
					dataView.RowFilter = icConsumerShare.getOptionalRowfilter();
					this.strGroupFilter = "";
				}
				else
				{
					this.strGroupFilter = " f_GroupID =" + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					int num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					int num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex];
					int groupChildMaxNo = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
					if (num2 > 0)
					{
						if (num2 >= groupChildMaxNo)
						{
							this.strGroupFilter = string.Format(" f_GroupID ={0:d} ", num);
						}
						else
						{
							string text = "";
							for (int i = 0; i < this.arrGroupNO.Count; i++)
							{
								if ((int)this.arrGroupNO[i] <= groupChildMaxNo && (int)this.arrGroupNO[i] >= num2)
								{
									if (text == "")
									{
										text += string.Format(" f_GroupID ={0:d} ", (int)this.arrGroupID[i]);
									}
									else
									{
										text += string.Format(" OR f_GroupID ={0:d} ", (int)this.arrGroupID[i]);
									}
								}
							}
							this.strGroupFilter = string.Format("  {0} ", text);
						}
					}
					dataView.RowFilter = string.Format("{0} AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				}
				if (string.IsNullOrEmpty(this.strGroupFilter))
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("{0}", icConsumerShare.getOptionalRowfilter());
				}
				else
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("  ({0}) AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				}
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format(" {0}", icConsumerShare.getSelectedRowfilter());
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private DataTable loadUserData4BackWork()
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("loadUserData Start");
			icConsumerShare.loadUserData();
			return icConsumerShare.getDt();
		}

		private void loadUserData4BackWorkComplete(DataTable dtUser)
		{
			this.dv = new DataView(dtUser);
			this.dvSelected = new DataView(dtUser);
			this.dv.RowFilter = icConsumerShare.getOptionalRowfilter();
			this.dvSelected.RowFilter = icConsumerShare.getSelectedRowfilter();
			this.dgvUsers.AutoGenerateColumns = false;
			this.dgvUsers.DataSource = this.dv;
			this.dgvSelectedUsers.AutoGenerateColumns = false;
			this.dgvSelectedUsers.DataSource = this.dvSelected;
			int num = 0;
			while (num < this.dv.Table.Columns.Count && num < this.dgvUsers.ColumnCount)
			{
				this.dgvUsers.Columns[num].DataPropertyName = dtUser.Columns[num].ColumnName;
				this.dgvSelectedUsers.Columns[num].DataPropertyName = dtUser.Columns[num].ColumnName;
				num++;
			}
			this.cbof_GroupID_SelectedIndexChanged(null, null);
			wgTools.WriteLine("loadUserData End");
			Cursor.Current = Cursors.Default;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			e.Result = this.loadUserData4BackWork();
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				wgTools.WgDebugWrite("Users Operation was canceled", new object[0]);
				return;
			}
			if (e.Error != null)
			{
				string info = string.Format("An error occurred: {0}", e.Error.Message);
				wgTools.WgDebugWrite(info, new object[0]);
				return;
			}
			this.loadUserData4BackWorkComplete(e.Result as DataTable);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString());
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.dgvUsers.DataSource == null)
			{
				Cursor.Current = Cursors.WaitCursor;
				return;
			}
			this.timer1.Enabled = false;
			Cursor.Current = Cursors.Default;
			this.lblWait.Visible = false;
			this.groupBox1.Enabled = true;
			this.btnOK.Enabled = true;
		}

		private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dtpStartDate.Value > this.dtpEndDate.Value)
				{
					this.cboEnd.Text = CommonStr.strPM;
				}
				this.dtpEndDate.MinDate = this.dtpStartDate.Value;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}
	}
}
