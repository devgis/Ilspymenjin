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

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmFirstCard : frmN3000
	{
		private const int MoreCardGroupMaxLen = 9;

		private IContainer components;

		private GroupBox grpUsers;

		private Label label3;

		private DataGridView dgvSelectedUsers;

		private DataGridView dgvUsers;

		private Button btnDelAllUsers;

		private Button btnDelOneUser;

		private Button btnAddOneUser;

		private Button btnAddAllUsers;

		private ComboBox cbof_GroupID;

		private Label label4;

		internal Button btnOK;

		internal Button btnCancel;

		internal CheckBox chkActive;

		private GroupBox grpBegin;

		private ComboBox cboBeginControlStatus;

		private Label label1;

		private Label label7;

		private DateTimePicker dateBeginHMS1;

		internal Label Label5;

		private GroupBox grpEnd;

		private ComboBox cboEndControlStatus;

		private Label label2;

		private Label label6;

		private DateTimePicker dateEndHMS1;

		internal Label label8;

		private GroupBox grpWeekdayControl;

		private CheckBox chkMonday;

		private CheckBox chkSunday;

		private CheckBox chkTuesday;

		private CheckBox chkSaturday;

		private CheckBox chkWednesday;

		private CheckBox chkFriday;

		private CheckBox chkThursday;

		private System.Windows.Forms.Timer timer1;

		private BackgroundWorker backgroundWorker1;

		private Label lblWait;

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

		private DataTable dt;

		private DataTable dtUser1;

		private DataView dv;

		private DataView dvSelected;

		private DataView dv1;

		private DataView dv2;

		public int DoorID;

		public string retValue = "0";

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private int controllerID;

		private int doorNo;

		private string strGroupFilter = "";

		private static string lastLoadUsers = "";

		private static DataTable dtLastLoad;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmFirstCard));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.grpWeekdayControl = new GroupBox();
			this.chkMonday = new CheckBox();
			this.chkSunday = new CheckBox();
			this.chkTuesday = new CheckBox();
			this.chkSaturday = new CheckBox();
			this.chkWednesday = new CheckBox();
			this.chkFriday = new CheckBox();
			this.chkThursday = new CheckBox();
			this.grpEnd = new GroupBox();
			this.cboEndControlStatus = new ComboBox();
			this.label2 = new Label();
			this.label6 = new Label();
			this.dateEndHMS1 = new DateTimePicker();
			this.label8 = new Label();
			this.grpUsers = new GroupBox();
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
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.chkActive = new CheckBox();
			this.grpBegin = new GroupBox();
			this.cboBeginControlStatus = new ComboBox();
			this.label1 = new Label();
			this.label7 = new Label();
			this.dateBeginHMS1 = new DateTimePicker();
			this.Label5 = new Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.backgroundWorker1 = new BackgroundWorker();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.grpWeekdayControl.SuspendLayout();
			this.grpEnd.SuspendLayout();
			this.grpUsers.SuspendLayout();
			((ISupportInitialize)this.dgvSelectedUsers).BeginInit();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			this.grpBegin.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.grpWeekdayControl, "grpWeekdayControl");
			this.grpWeekdayControl.BackColor = Color.Transparent;
			this.grpWeekdayControl.Controls.Add(this.chkMonday);
			this.grpWeekdayControl.Controls.Add(this.chkSunday);
			this.grpWeekdayControl.Controls.Add(this.chkTuesday);
			this.grpWeekdayControl.Controls.Add(this.chkSaturday);
			this.grpWeekdayControl.Controls.Add(this.chkWednesday);
			this.grpWeekdayControl.Controls.Add(this.chkFriday);
			this.grpWeekdayControl.Controls.Add(this.chkThursday);
			this.grpWeekdayControl.ForeColor = Color.White;
			this.grpWeekdayControl.Name = "grpWeekdayControl";
			this.grpWeekdayControl.TabStop = false;
			componentResourceManager.ApplyResources(this.chkMonday, "chkMonday");
			this.chkMonday.Checked = true;
			this.chkMonday.CheckState = CheckState.Checked;
			this.chkMonday.Name = "chkMonday";
			this.chkMonday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkSunday, "chkSunday");
			this.chkSunday.Checked = true;
			this.chkSunday.CheckState = CheckState.Checked;
			this.chkSunday.Name = "chkSunday";
			this.chkSunday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkTuesday, "chkTuesday");
			this.chkTuesday.Checked = true;
			this.chkTuesday.CheckState = CheckState.Checked;
			this.chkTuesday.Name = "chkTuesday";
			this.chkTuesday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkSaturday, "chkSaturday");
			this.chkSaturday.Checked = true;
			this.chkSaturday.CheckState = CheckState.Checked;
			this.chkSaturday.Name = "chkSaturday";
			this.chkSaturday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkWednesday, "chkWednesday");
			this.chkWednesday.Checked = true;
			this.chkWednesday.CheckState = CheckState.Checked;
			this.chkWednesday.Name = "chkWednesday";
			this.chkWednesday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkFriday, "chkFriday");
			this.chkFriday.Checked = true;
			this.chkFriday.CheckState = CheckState.Checked;
			this.chkFriday.Name = "chkFriday";
			this.chkFriday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkThursday, "chkThursday");
			this.chkThursday.Checked = true;
			this.chkThursday.CheckState = CheckState.Checked;
			this.chkThursday.Name = "chkThursday";
			this.chkThursday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.grpEnd, "grpEnd");
			this.grpEnd.BackColor = Color.Transparent;
			this.grpEnd.Controls.Add(this.cboEndControlStatus);
			this.grpEnd.Controls.Add(this.label2);
			this.grpEnd.Controls.Add(this.label6);
			this.grpEnd.Controls.Add(this.dateEndHMS1);
			this.grpEnd.Controls.Add(this.label8);
			this.grpEnd.ForeColor = Color.White;
			this.grpEnd.Name = "grpEnd";
			this.grpEnd.TabStop = false;
			componentResourceManager.ApplyResources(this.cboEndControlStatus, "cboEndControlStatus");
			this.cboEndControlStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				componentResourceManager.GetString("cboEndControlStatus.AutoCompleteCustomSource"),
				componentResourceManager.GetString("cboEndControlStatus.AutoCompleteCustomSource1"),
				componentResourceManager.GetString("cboEndControlStatus.AutoCompleteCustomSource2")
			});
			this.cboEndControlStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboEndControlStatus.FormattingEnabled = true;
			this.cboEndControlStatus.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboEndControlStatus.Items"),
				componentResourceManager.GetString("cboEndControlStatus.Items1"),
				componentResourceManager.GetString("cboEndControlStatus.Items2"),
				componentResourceManager.GetString("cboEndControlStatus.Items3")
			});
			this.cboEndControlStatus.Name = "cboEndControlStatus";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			componentResourceManager.ApplyResources(this.dateEndHMS1, "dateEndHMS1");
			this.dateEndHMS1.Name = "dateEndHMS1";
			this.dateEndHMS1.ShowUpDown = true;
			this.dateEndHMS1.Value = new DateTime(2010, 1, 1, 8, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			componentResourceManager.ApplyResources(this.grpUsers, "grpUsers");
			this.grpUsers.BackColor = Color.Transparent;
			this.grpUsers.Controls.Add(this.lblWait);
			this.grpUsers.Controls.Add(this.label3);
			this.grpUsers.Controls.Add(this.dgvSelectedUsers);
			this.grpUsers.Controls.Add(this.dgvUsers);
			this.grpUsers.Controls.Add(this.btnDelAllUsers);
			this.grpUsers.Controls.Add(this.btnDelOneUser);
			this.grpUsers.Controls.Add(this.btnAddOneUser);
			this.grpUsers.Controls.Add(this.btnAddAllUsers);
			this.grpUsers.Controls.Add(this.cbof_GroupID);
			this.grpUsers.Controls.Add(this.label4);
			this.grpUsers.ForeColor = Color.White;
			this.grpUsers.Name = "grpUsers";
			this.grpUsers.TabStop = false;
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
			this.btnCancel.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.chkActive, "chkActive");
			this.chkActive.BackColor = Color.Transparent;
			this.chkActive.ForeColor = Color.White;
			this.chkActive.Name = "chkActive";
			this.chkActive.UseVisualStyleBackColor = false;
			this.chkActive.CheckedChanged += new EventHandler(this.chkActive_CheckedChanged);
			componentResourceManager.ApplyResources(this.grpBegin, "grpBegin");
			this.grpBegin.BackColor = Color.Transparent;
			this.grpBegin.Controls.Add(this.cboBeginControlStatus);
			this.grpBegin.Controls.Add(this.label1);
			this.grpBegin.Controls.Add(this.label7);
			this.grpBegin.Controls.Add(this.dateBeginHMS1);
			this.grpBegin.Controls.Add(this.Label5);
			this.grpBegin.ForeColor = Color.White;
			this.grpBegin.Name = "grpBegin";
			this.grpBegin.TabStop = false;
			componentResourceManager.ApplyResources(this.cboBeginControlStatus, "cboBeginControlStatus");
			this.cboBeginControlStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				componentResourceManager.GetString("cboBeginControlStatus.AutoCompleteCustomSource"),
				componentResourceManager.GetString("cboBeginControlStatus.AutoCompleteCustomSource1"),
				componentResourceManager.GetString("cboBeginControlStatus.AutoCompleteCustomSource2")
			});
			this.cboBeginControlStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboBeginControlStatus.FormattingEnabled = true;
			this.cboBeginControlStatus.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboBeginControlStatus.Items"),
				componentResourceManager.GetString("cboBeginControlStatus.Items1"),
				componentResourceManager.GetString("cboBeginControlStatus.Items2")
			});
			this.cboBeginControlStatus.Name = "cboBeginControlStatus";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			componentResourceManager.ApplyResources(this.dateBeginHMS1, "dateBeginHMS1");
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Value = new DateTime(2010, 1, 1, 8, 0, 0, 0);
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
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
			base.Controls.Add(this.grpWeekdayControl);
			base.Controls.Add(this.grpEnd);
			base.Controls.Add(this.grpBegin);
			base.Controls.Add(this.chkActive);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.grpUsers);
			base.Name = "dfrmFirstCard";
			base.Load += new EventHandler(this.dfrmFirstCard_Load);
			this.grpWeekdayControl.ResumeLayout(false);
			this.grpWeekdayControl.PerformLayout();
			this.grpEnd.ResumeLayout(false);
			this.grpEnd.PerformLayout();
			this.grpUsers.ResumeLayout(false);
			this.grpUsers.PerformLayout();
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			((ISupportInitialize)this.dgvUsers).EndInit();
			this.grpBegin.ResumeLayout(false);
			this.grpBegin.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmFirstCard()
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

		private void dfrmFirstCard_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmFirstCard_Load_Acc(sender, e);
				return;
			}
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("08:00:00");
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("08:00:00");
			this.loadGroupData();
			string text = "SELECT t_b_Door.*  FROM  t_b_Door  ";
			text = text + " Where  f_DoorID = " + this.DoorID;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						this.controllerID = (int)sqlDataReader["f_ControllerID"];
						this.doorNo = int.Parse(sqlDataReader["f_DoorNo"].ToString());
						this.dateBeginHMS1.Value = DateTime.Parse(sqlDataReader["f_FirstCard_BeginHMS"].ToString());
						this.dateEndHMS1.Value = DateTime.Parse(sqlDataReader["f_FirstCard_EndHMS"].ToString());
						try
						{
							this.cboBeginControlStatus.SelectedIndex = (((int)sqlDataReader["f_FirstCard_BeginControl"] >= 0 && (int)sqlDataReader["f_FirstCard_BeginControl"] < 4) ? ((int)sqlDataReader["f_FirstCard_BeginControl"]) : 0);
							this.cboEndControlStatus.SelectedIndex = (((int)sqlDataReader["f_FirstCard_EndControl"] >= 0 && (int)sqlDataReader["f_FirstCard_EndControl"] < 4) ? ((int)sqlDataReader["f_FirstCard_EndControl"]) : 0);
						}
						catch (Exception)
						{
							if (this.cboBeginControlStatus.Items.Count > 0)
							{
								this.cboBeginControlStatus.SelectedIndex = 0;
							}
							if (this.cboEndControlStatus.Items.Count > 0)
							{
								this.cboEndControlStatus.SelectedIndex = 0;
							}
						}
						if (wgTools.SetObjToStr(sqlDataReader["f_FirstCard_Weekday"]) != "")
						{
							this.chkMonday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 1) > 0);
							this.chkTuesday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 2) > 0);
							this.chkWednesday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 4) > 0);
							this.chkThursday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 8) > 0);
							this.chkFriday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 16) > 0);
							this.chkSaturday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 32) > 0);
							this.chkSunday.Checked = (((int)sqlDataReader["f_FirstCard_Weekday"] & 64) > 0);
						}
						if ((int)sqlDataReader["f_FirstCard_Enabled"] > 0)
						{
							this.chkActive.Checked = true;
						}
						else
						{
							this.chkActive.Checked = false;
						}
						this.chkActive_CheckedChanged(null, null);
					}
					sqlDataReader.Close();
				}
			}
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void dfrmFirstCard_Load_Acc(object sender, EventArgs e)
		{
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("08:00:00");
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("08:00:00");
			this.loadGroupData();
			string text = "SELECT t_b_Door.*  FROM  t_b_Door  ";
			text = text + " Where  f_DoorID = " + this.DoorID;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						this.controllerID = (int)oleDbDataReader["f_ControllerID"];
						this.doorNo = int.Parse(oleDbDataReader["f_DoorNo"].ToString());
						this.dateBeginHMS1.Value = DateTime.Parse(oleDbDataReader["f_FirstCard_BeginHMS"].ToString());
						this.dateEndHMS1.Value = DateTime.Parse(oleDbDataReader["f_FirstCard_EndHMS"].ToString());
						try
						{
							this.cboBeginControlStatus.SelectedIndex = (((int)oleDbDataReader["f_FirstCard_BeginControl"] >= 0 && (int)oleDbDataReader["f_FirstCard_BeginControl"] < 4) ? ((int)oleDbDataReader["f_FirstCard_BeginControl"]) : 0);
							this.cboEndControlStatus.SelectedIndex = (((int)oleDbDataReader["f_FirstCard_EndControl"] >= 0 && (int)oleDbDataReader["f_FirstCard_EndControl"] < 4) ? ((int)oleDbDataReader["f_FirstCard_EndControl"]) : 0);
						}
						catch (Exception)
						{
							if (this.cboBeginControlStatus.Items.Count > 0)
							{
								this.cboBeginControlStatus.SelectedIndex = 0;
							}
							if (this.cboEndControlStatus.Items.Count > 0)
							{
								this.cboEndControlStatus.SelectedIndex = 0;
							}
						}
						if (wgTools.SetObjToStr(oleDbDataReader["f_FirstCard_Weekday"]) != "")
						{
							this.chkMonday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 1) > 0);
							this.chkTuesday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 2) > 0);
							this.chkWednesday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 4) > 0);
							this.chkThursday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 8) > 0);
							this.chkFriday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 16) > 0);
							this.chkSaturday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 32) > 0);
							this.chkSunday.Checked = (((int)oleDbDataReader["f_FirstCard_Weekday"] & 64) > 0);
						}
						if ((int)oleDbDataReader["f_FirstCard_Enabled"] > 0)
						{
							this.chkActive.Checked = true;
						}
						else
						{
							this.chkActive.Checked = false;
						}
						this.chkActive_CheckedChanged(null, null);
					}
					oleDbDataReader.Close();
				}
			}
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void btnAddAllUsers_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("btnAddAllUsers_Click Start");
			this.dt = ((DataView)this.dgvUsers.DataSource).Table;
			this.dv1 = (DataView)this.dgvUsers.DataSource;
			this.dv2 = (DataView)this.dgvSelectedUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvSelectedUsers.DataSource = null;
			if (this.strGroupFilter == "")
			{
				string rowFilter = this.dv1.RowFilter;
				string rowFilter2 = this.dv2.RowFilter;
				this.dv1.Dispose();
				this.dv2.Dispose();
				this.dv1 = null;
				this.dv2 = null;
				this.dt.BeginLoadData();
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 1;
				}
				this.dt.EndLoadData();
				this.dv1 = new DataView(this.dt);
				this.dv1.RowFilter = rowFilter;
				this.dv2 = new DataView(this.dt);
				this.dv2.RowFilter = rowFilter2;
			}
			else
			{
				this.dv = new DataView(this.dt);
				this.dv.RowFilter = this.strGroupFilter;
				for (int j = 0; j < this.dv.Count; j++)
				{
					this.dv[j]["f_Selected"] = 1;
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
				this.dt = ((DataView)this.dgvUsers.DataSource).Table;
				this.dv1 = (DataView)this.dgvUsers.DataSource;
				this.dv2 = (DataView)this.dgvSelectedUsers.DataSource;
				this.dgvUsers.DataSource = null;
				this.dgvSelectedUsers.DataSource = null;
				string rowFilter = this.dv1.RowFilter;
				string rowFilter2 = this.dv2.RowFilter;
				this.dv1.Dispose();
				this.dv2.Dispose();
				this.dv1 = null;
				this.dv2 = null;
				this.dt.BeginLoadData();
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 0;
				}
				this.dt.EndLoadData();
				this.dv1 = new DataView(this.dt);
				this.dv1.RowFilter = rowFilter;
				this.dv2 = new DataView(this.dt);
				this.dv2.RowFilter = rowFilter2;
				this.dgvUsers.DataSource = this.dv1;
				this.dgvSelectedUsers.DataSource = this.dv2;
				wgTools.WriteLine("btnDelAllUsers_Click End");
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnAddOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvUsers);
		}

		private void btnDelOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedUsers);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			int num = 0;
			if (this.chkMonday.Checked)
			{
				num |= 1;
			}
			if (this.chkTuesday.Checked)
			{
				num |= 2;
			}
			if (this.chkWednesday.Checked)
			{
				num |= 4;
			}
			if (this.chkThursday.Checked)
			{
				num |= 8;
			}
			if (this.chkFriday.Checked)
			{
				num |= 16;
			}
			if (this.chkSaturday.Checked)
			{
				num |= 32;
			}
			if (this.chkSunday.Checked)
			{
				num |= 64;
			}
			string text = "update t_b_door set f_FirstCard_Enabled =" + (this.chkActive.Checked ? 1 : 0);
			text = text + ", f_FirstCard_BeginHMS=" + wgTools.PrepareStr(this.dateBeginHMS1.Value.ToString(wgTools.YMDHMSFormat), true, wgTools.YMDHMSFormat);
			text = text + ", f_FirstCard_BeginControl =" + this.cboBeginControlStatus.SelectedIndex;
			text = text + ", f_FirstCard_EndHMS=" + wgTools.PrepareStr(this.dateEndHMS1.Value.ToString(wgTools.YMDHMSFormat), true, wgTools.YMDHMSFormat);
			text = text + ", f_FirstCard_EndControl=" + this.cboEndControlStatus.SelectedIndex;
			text = text + ", f_FirstCard_Weekday=" + num;
			text = text + " Where f_DoorID = " + this.DoorID;
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.ExecuteNonQuery();
					}
					goto IL_1F2;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
			}
			IL_1F2:
			text = " Delete  FROM t_d_doorFirstCardUsers  WHERE f_DoorID= " + this.DoorID;
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
					{
						oleDbConnection2.Open();
						oleDbCommand2.ExecuteNonQuery();
					}
					goto IL_291;
				}
			}
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
				{
					sqlConnection2.Open();
					sqlCommand2.ExecuteNonQuery();
				}
			}
			IL_291:
			if (this.chkActive.Checked)
			{
				if (this.dgvSelectedUsers.DataSource != null)
				{
					using (DataView dataView = this.dgvSelectedUsers.DataSource as DataView)
					{
						if (dataView.Count > 0)
						{
							int i = 0;
							while (i <= dataView.Count - 1)
							{
								text = "INSERT INTO [t_d_doorFirstCardUsers](f_ConsumerID, f_DoorID )";
								text += " VALUES( ";
								text += dataView[i]["f_ConsumerID"].ToString();
								text = text + ", " + this.DoorID.ToString() + ")";
								if (wgAppConfig.IsAccessDB)
								{
									using (OleDbConnection oleDbConnection3 = new OleDbConnection(wgAppConfig.dbConString))
									{
										using (OleDbCommand oleDbCommand3 = new OleDbCommand(text, oleDbConnection3))
										{
											oleDbConnection3.Open();
											oleDbCommand3.ExecuteNonQuery();
										}
										goto IL_3AE;
									}
									goto IL_36D;
								}
								goto IL_36D;
								IL_3AE:
								i++;
								continue;
								IL_36D:
								using (SqlConnection sqlConnection3 = new SqlConnection(wgAppConfig.dbConString))
								{
									using (SqlCommand sqlCommand3 = new SqlCommand(text, sqlConnection3))
									{
										sqlConnection3.Open();
										sqlCommand3.ExecuteNonQuery();
									}
								}
								goto IL_3AE;
							}
						}
					}
				}
				this.retValue = "1";
			}
			else
			{
				this.retValue = "0";
			}
			base.DialogResult = DialogResult.OK;
			this.Cursor = current;
			base.Close();
		}

		private void cbof_GroupID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvUsers.DataSource != null)
			{
				DataView dataView = (DataView)this.dgvUsers.DataSource;
				if (this.cbof_GroupID.SelectedIndex < 0 || (this.cbof_GroupID.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
				{
					dataView.RowFilter = "f_Selected = 0";
					this.strGroupFilter = "";
					return;
				}
				dataView.RowFilter = "f_Selected = 0 AND f_GroupID =" + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
				this.strGroupFilter = " f_GroupID =" + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
				int num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex];
				int num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex];
				int groupChildMaxNo = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
				if (num2 > 0)
				{
					if (num2 >= groupChildMaxNo)
					{
						dataView.RowFilter = string.Format("f_Selected = 0 AND f_GroupID ={0:d} ", num);
						this.strGroupFilter = string.Format(" f_GroupID ={0:d} ", num);
						return;
					}
					dataView.RowFilter = "f_Selected = 0 ";
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
					dataView.RowFilter = string.Format("f_Selected = 0 AND ( {0} )", text);
					this.strGroupFilter = string.Format("  {0} ", text);
				}
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkActive.Checked)
			{
				this.grpBegin.Visible = false;
				this.grpEnd.Visible = false;
				this.grpUsers.Visible = false;
				this.grpWeekdayControl.Visible = false;
				return;
			}
			this.grpBegin.Visible = true;
			this.grpEnd.Visible = true;
			this.grpUsers.Visible = true;
			this.grpWeekdayControl.Visible = true;
			if (this.dgvUsers.DataSource == null)
			{
				return;
			}
			DataView dataSource = (DataView)this.dgvUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvUsers.DataSource = dataSource;
		}

		private DataTable loadUserData4BackWork()
		{
			Thread.Sleep(100);
			wgTools.WriteLine("loadUserData Start");
			this.dtUser1 = new DataTable();
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT  t_b_Consumer.f_ConsumerID ";
				text += " , f_ConsumerNO, f_ConsumerName, f_CardNO ";
				text += " , IIF ( f_doorFirstCardUsersId IS NULL , 0 , 1 ) AS f_Selected ";
				text += " , f_GroupID ";
				text += " FROM t_b_Consumer ";
				text += string.Format(" LEFT OUTER JOIN t_d_doorFirstCardUsers ON ( t_b_Consumer.f_ConsumerID=t_d_doorFirstCardUsers.f_ConsumerID AND f_DoorID= {0})", this.DoorID.ToString());
				text += " WHERE f_DoorEnabled > 0";
				text += " ORDER BY f_ConsumerNO ASC ";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtUser1);
						}
					}
					goto IL_19A;
				}
			}
			text = " SELECT  t_b_Consumer.f_ConsumerID ";
			text += " , f_ConsumerNO, f_ConsumerName, f_CardNO ";
			text += " , CASE WHEN f_doorFirstCardUsersId IS NULL THEN 0 ELSE 1 END AS f_Selected ";
			text += " , f_GroupID ";
			text += " FROM t_b_Consumer ";
			text = text + " LEFT OUTER JOIN t_d_doorFirstCardUsers ON t_b_Consumer.f_ConsumerID=t_d_doorFirstCardUsers.f_ConsumerID AND f_DoorID= " + this.DoorID;
			text += " WHERE f_DoorEnabled > 0";
			text += " ORDER BY f_ConsumerNO ASC ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtUser1);
					}
				}
			}
			IL_19A:
			wgTools.WriteLine("da.Fill End");
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dtUser1.Columns[0]
				};
				this.dtUser1.PrimaryKey = primaryKey;
			}
			catch (Exception)
			{
				throw;
			}
			dfrmFirstCard.lastLoadUsers = icConsumerShare.getUpdateLog();
			dfrmFirstCard.dtLastLoad = this.dtUser1;
			return this.dtUser1;
		}

		private void loadUserData4BackWorkComplete(DataTable dtUser)
		{
			this.dv = new DataView(dtUser);
			this.dvSelected = new DataView(dtUser);
			this.dv.RowFilter = "f_Selected = 0";
			this.dvSelected.RowFilter = "f_Selected > 0";
			this.dvSelected.Sort = " f_ConsumerNo ASC ";
			this.dgvUsers.AutoGenerateColumns = false;
			this.dgvUsers.DataSource = this.dv;
			this.dgvSelectedUsers.AutoGenerateColumns = false;
			this.dgvSelectedUsers.DataSource = this.dvSelected;
			for (int i = 0; i < this.dv.Table.Columns.Count; i++)
			{
				this.dgvUsers.Columns[i].DataPropertyName = dtUser.Columns[i].ColumnName;
				this.dgvSelectedUsers.Columns[i].DataPropertyName = dtUser.Columns[i].ColumnName;
			}
			this.cbof_GroupID_SelectedIndexChanged(null, null);
			wgTools.WriteLine("loadUserData End");
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
			this.grpUsers.Enabled = true;
			this.btnOK.Enabled = true;
		}
	}
}
