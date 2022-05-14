using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class dfrmMeetingSet : frmN3000
	{
		public string curMeetingNo = "";

		private GroupBox groupBox1;

		private Label lblWait;

		private Label label3;

		private DataGridView dgvSelectedUsers;

		private DataGridView dgvUsers;

		private Button btnDelAllUsers;

		private Button btnDelOneUser;

		private Button btnAdd;

		private Button btnAddAll;

		private ComboBox cbof_GroupID;

		private Label label4;

		private BackgroundWorker backgroundWorker1;

		private System.Windows.Forms.Timer timer1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn Identity;

		private DataGridViewTextBoxColumn IdentityStr2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn f_MoreCards_GrpID;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn f_SelectedGroup;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn Identity1;

		private DataGridViewTextBoxColumn IdentityStr;

		private DataGridViewTextBoxColumn UserID;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewTextBoxColumn SeatNO1;

		private DataGridViewCheckBoxColumn f_SelectedUsers;

		private DataGridViewTextBoxColumn f_GroupID;

		private ResourceManager resStr;

		private IContainer components;

		internal Button btnOK;

		internal Button btnCancel;

		internal Label Label1;

		internal TextBox txtf_MeetingNo;

		internal TextBox txtf_MeetingName;

		internal DateTimePicker dtpMeetingDate;

		internal DateTimePicker dtpMeetingTime;

		internal DateTimePicker dtpStartTime;

		internal DateTimePicker dtpEndTime;

		internal TextBox txtf_Content;

		internal TextBox txtf_Notes;

		internal Label lblControlTimeSeg;

		internal Label Label9;

		internal ComboBox cboIdentity;

		internal TextBox txtSeat;

		internal ComboBox cbof_MeetingAdr;

		internal Button btnAddMeetingAdr;

		internal Button btnCreateInfo;

		internal Label lblMeetingName;

		internal Label lblMeetingAddr;

		internal Label lblMeetingDateTime;

		internal Label lblSignBegin;

		internal Label lblSignEnd;

		internal Label lblContent;

		internal Label lblNotes;

		private DataSet ds = new DataSet();

		private DataView dvGroupedConsumers;

		private bool bNeedUpdateMeetingConsumer;

		private DataTable dt;

		private DataTable dtUser1;

		private DataView dv;

		private DataView dvSelected;

		private DataView dv1;

		private DataView dv2;

		private string strGroupFilter = "";

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private dfrmFind dfrmFind1 = new dfrmFind();

		private static string lastLoadUsers = "";

		private static DataTable dtLastLoad;

		public dfrmMeetingSet()
		{
			this.InitializeComponent();
			this.resStr = new ResourceManager("WgiCCard." + base.Name + "Str", Assembly.GetExecutingAssembly());
			this.resStr.IgnoreCase = true;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMeetingSet));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.txtf_Notes = new TextBox();
			this.lblNotes = new Label();
			this.txtf_Content = new TextBox();
			this.cbof_MeetingAdr = new ComboBox();
			this.lblContent = new Label();
			this.btnAddMeetingAdr = new Button();
			this.groupBox1 = new GroupBox();
			this.txtSeat = new TextBox();
			this.lblWait = new Label();
			this.cboIdentity = new ComboBox();
			this.lblControlTimeSeg = new Label();
			this.label3 = new Label();
			this.dgvSelectedUsers = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.Identity = new DataGridViewTextBoxColumn();
			this.IdentityStr2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.f_MoreCards_GrpID = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.f_SelectedGroup = new DataGridViewTextBoxColumn();
			this.dgvUsers = new DataGridView();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.Identity1 = new DataGridViewTextBoxColumn();
			this.IdentityStr = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.SeatNO1 = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.btnDelAllUsers = new Button();
			this.btnDelOneUser = new Button();
			this.btnAdd = new Button();
			this.btnAddAll = new Button();
			this.Label9 = new Label();
			this.cbof_GroupID = new ComboBox();
			this.label4 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.Label1 = new Label();
			this.txtf_MeetingNo = new TextBox();
			this.lblMeetingName = new Label();
			this.txtf_MeetingName = new TextBox();
			this.lblMeetingAddr = new Label();
			this.lblMeetingDateTime = new Label();
			this.dtpMeetingDate = new DateTimePicker();
			this.dtpMeetingTime = new DateTimePicker();
			this.lblSignBegin = new Label();
			this.lblSignEnd = new Label();
			this.dtpStartTime = new DateTimePicker();
			this.dtpEndTime = new DateTimePicker();
			this.btnCreateInfo = new Button();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dgvSelectedUsers).BeginInit();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.txtf_Notes, "txtf_Notes");
			this.txtf_Notes.Name = "txtf_Notes";
			this.lblNotes.BackColor = Color.Transparent;
			this.lblNotes.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblNotes, "lblNotes");
			this.lblNotes.Name = "lblNotes";
			componentResourceManager.ApplyResources(this.txtf_Content, "txtf_Content");
			this.txtf_Content.Name = "txtf_Content";
			this.cbof_MeetingAdr.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_MeetingAdr, "cbof_MeetingAdr");
			this.cbof_MeetingAdr.Name = "cbof_MeetingAdr";
			this.lblContent.BackColor = Color.Transparent;
			this.lblContent.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblContent, "lblContent");
			this.lblContent.Name = "lblContent";
			this.btnAddMeetingAdr.BackColor = Color.Transparent;
			this.btnAddMeetingAdr.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddMeetingAdr, "btnAddMeetingAdr");
			this.btnAddMeetingAdr.ForeColor = Color.White;
			this.btnAddMeetingAdr.Name = "btnAddMeetingAdr";
			this.btnAddMeetingAdr.UseVisualStyleBackColor = false;
			this.btnAddMeetingAdr.Click += new EventHandler(this.btnAddMeetingAdr_Click);
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.txtSeat);
			this.groupBox1.Controls.Add(this.lblWait);
			this.groupBox1.Controls.Add(this.cboIdentity);
			this.groupBox1.Controls.Add(this.lblControlTimeSeg);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.dgvSelectedUsers);
			this.groupBox1.Controls.Add(this.dgvUsers);
			this.groupBox1.Controls.Add(this.btnDelAllUsers);
			this.groupBox1.Controls.Add(this.btnDelOneUser);
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Controls.Add(this.btnAddAll);
			this.groupBox1.Controls.Add(this.Label9);
			this.groupBox1.Controls.Add(this.cbof_GroupID);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.txtSeat.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.txtSeat, "txtSeat");
			this.txtSeat.Name = "txtSeat";
			componentResourceManager.ApplyResources(this.lblWait, "lblWait");
			this.lblWait.BorderStyle = BorderStyle.FixedSingle;
			this.lblWait.Name = "lblWait";
			this.cboIdentity.BackColor = Color.White;
			this.cboIdentity.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cboIdentity, "cboIdentity");
			this.cboIdentity.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboIdentity.Items"),
				componentResourceManager.GetString("cboIdentity.Items1"),
				componentResourceManager.GetString("cboIdentity.Items2"),
				componentResourceManager.GetString("cboIdentity.Items3"),
				componentResourceManager.GetString("cboIdentity.Items4"),
				componentResourceManager.GetString("cboIdentity.Items5")
			});
			this.cboIdentity.Name = "cboIdentity";
			this.lblControlTimeSeg.BackColor = Color.Transparent;
			this.lblControlTimeSeg.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblControlTimeSeg, "lblControlTimeSeg");
			this.lblControlTimeSeg.Name = "lblControlTimeSeg";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.dgvSelectedUsers.AllowUserToAddRows = false;
			this.dgvSelectedUsers.AllowUserToDeleteRows = false;
			this.dgvSelectedUsers.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvSelectedUsers, "dgvSelectedUsers");
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
				this.Identity,
				this.IdentityStr2,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.f_MoreCards_GrpID,
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
			componentResourceManager.ApplyResources(this.Identity, "Identity");
			this.Identity.Name = "Identity";
			this.Identity.ReadOnly = true;
			componentResourceManager.ApplyResources(this.IdentityStr2, "IdentityStr2");
			this.IdentityStr2.Name = "IdentityStr2";
			this.IdentityStr2.ReadOnly = true;
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
			componentResourceManager.ApplyResources(this.f_MoreCards_GrpID, "f_MoreCards_GrpID");
			this.f_MoreCards_GrpID.Name = "f_MoreCards_GrpID";
			this.f_MoreCards_GrpID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedGroup, "f_SelectedGroup");
			this.f_SelectedGroup.Name = "f_SelectedGroup";
			this.f_SelectedGroup.ReadOnly = true;
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AllowUserToDeleteRows = false;
			this.dgvUsers.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvUsers, "dgvUsers");
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
				this.Identity1,
				this.IdentityStr,
				this.UserID,
				this.ConsumerName,
				this.CardNO,
				this.SeatNO1,
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
			this.dgvUsers.DoubleClick += new EventHandler(this.btnAdd_Click);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Identity1, "Identity1");
			this.Identity1.Name = "Identity1";
			this.Identity1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.IdentityStr, "IdentityStr");
			this.IdentityStr.Name = "IdentityStr";
			this.IdentityStr.ReadOnly = true;
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
			componentResourceManager.ApplyResources(this.SeatNO1, "SeatNO1");
			this.SeatNO1.Name = "SeatNO1";
			this.SeatNO1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedUsers, "f_SelectedUsers");
			this.f_SelectedUsers.Name = "f_SelectedUsers";
			this.f_SelectedUsers.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_GroupID, "f_GroupID");
			this.f_GroupID.Name = "f_GroupID";
			this.f_GroupID.ReadOnly = true;
			this.btnDelAllUsers.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDelAllUsers, "btnDelAllUsers");
			this.btnDelAllUsers.Name = "btnDelAllUsers";
			this.btnDelAllUsers.UseVisualStyleBackColor = true;
			this.btnDelAllUsers.Click += new EventHandler(this.btnDelAllUsers_Click);
			this.btnDelOneUser.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDelOneUser, "btnDelOneUser");
			this.btnDelOneUser.Name = "btnDelOneUser";
			this.btnDelOneUser.UseVisualStyleBackColor = true;
			this.btnDelOneUser.Click += new EventHandler(this.btnDelOneUser_Click);
			this.btnAdd.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.btnAddAll.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddAll, "btnAddAll");
			this.btnAddAll.Name = "btnAddAll";
			this.btnAddAll.UseVisualStyleBackColor = true;
			this.btnAddAll.Click += new EventHandler(this.btnAddAll_Click);
			this.Label9.BackColor = Color.Transparent;
			this.Label9.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label9, "Label9");
			this.Label9.Name = "Label9";
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
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
			this.btnOK.Click += new EventHandler(this.btnOk_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			componentResourceManager.ApplyResources(this.txtf_MeetingNo, "txtf_MeetingNo");
			this.txtf_MeetingNo.Name = "txtf_MeetingNo";
			this.txtf_MeetingNo.ReadOnly = true;
			this.lblMeetingName.BackColor = Color.Transparent;
			this.lblMeetingName.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblMeetingName, "lblMeetingName");
			this.lblMeetingName.Name = "lblMeetingName";
			componentResourceManager.ApplyResources(this.txtf_MeetingName, "txtf_MeetingName");
			this.txtf_MeetingName.Name = "txtf_MeetingName";
			this.lblMeetingAddr.BackColor = Color.Transparent;
			this.lblMeetingAddr.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblMeetingAddr, "lblMeetingAddr");
			this.lblMeetingAddr.Name = "lblMeetingAddr";
			this.lblMeetingDateTime.BackColor = Color.Transparent;
			this.lblMeetingDateTime.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblMeetingDateTime, "lblMeetingDateTime");
			this.lblMeetingDateTime.Name = "lblMeetingDateTime";
			componentResourceManager.ApplyResources(this.dtpMeetingDate, "dtpMeetingDate");
			this.dtpMeetingDate.Name = "dtpMeetingDate";
			this.dtpMeetingDate.Value = new DateTime(2008, 2, 21, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dtpMeetingTime, "dtpMeetingTime");
			this.dtpMeetingTime.Format = DateTimePickerFormat.Time;
			this.dtpMeetingTime.Name = "dtpMeetingTime";
			this.dtpMeetingTime.ShowUpDown = true;
			this.dtpMeetingTime.Value = new DateTime(2008, 2, 21, 0, 0, 0, 0);
			this.lblSignBegin.BackColor = Color.Transparent;
			this.lblSignBegin.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblSignBegin, "lblSignBegin");
			this.lblSignBegin.Name = "lblSignBegin";
			this.lblSignEnd.BackColor = Color.Transparent;
			this.lblSignEnd.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblSignEnd, "lblSignEnd");
			this.lblSignEnd.Name = "lblSignEnd";
			componentResourceManager.ApplyResources(this.dtpStartTime, "dtpStartTime");
			this.dtpStartTime.Format = DateTimePickerFormat.Time;
			this.dtpStartTime.Name = "dtpStartTime";
			this.dtpStartTime.ShowUpDown = true;
			this.dtpStartTime.Value = new DateTime(2003, 3, 10, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dtpEndTime, "dtpEndTime");
			this.dtpEndTime.Format = DateTimePickerFormat.Time;
			this.dtpEndTime.Name = "dtpEndTime";
			this.dtpEndTime.ShowUpDown = true;
			this.dtpEndTime.Value = new DateTime(2003, 3, 10, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.btnCreateInfo, "btnCreateInfo");
			this.btnCreateInfo.BackColor = Color.Transparent;
			this.btnCreateInfo.BackgroundImage = Resources.pMain_button_normal;
			this.btnCreateInfo.ForeColor = Color.White;
			this.btnCreateInfo.Name = "btnCreateInfo";
			this.btnCreateInfo.UseVisualStyleBackColor = false;
			this.btnCreateInfo.Click += new EventHandler(this.btnCreateInfo_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtf_Notes);
			base.Controls.Add(this.lblNotes);
			base.Controls.Add(this.txtf_Content);
			base.Controls.Add(this.cbof_MeetingAdr);
			base.Controls.Add(this.lblContent);
			base.Controls.Add(this.btnAddMeetingAdr);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.lblMeetingName);
			base.Controls.Add(this.txtf_MeetingName);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.lblMeetingAddr);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.lblMeetingDateTime);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.dtpMeetingDate);
			base.Controls.Add(this.txtf_MeetingNo);
			base.Controls.Add(this.dtpMeetingTime);
			base.Controls.Add(this.btnCreateInfo);
			base.Controls.Add(this.lblSignBegin);
			base.Controls.Add(this.dtpEndTime);
			base.Controls.Add(this.lblSignEnd);
			base.Controls.Add(this.dtpStartTime);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmMeetingSet";
			base.FormClosing += new FormClosingEventHandler(this.dfrmMeetingSet_FormClosing);
			base.Load += new EventHandler(this.dfrmMeetingSet_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmMeetingSet_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			((ISupportInitialize)this.dgvUsers).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
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

		private void dfrmMeetingSet_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmMeetingSet_Load_Acc(sender, e);
				return;
			}
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			base.KeyPreview = true;
			try
			{
				this.cboIdentity.SelectedIndex = 0;
				this.loadGroupData();
				this.loadMeetingAdr();
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				if (this.curMeetingNo == "")
				{
					this.txtf_MeetingNo.Text = Strings.Format(DateTime.Now, "yyyyMMdd_HHmmss");
					this.dtpMeetingDate.Value = DateTime.Now.Date;
					this.dtpMeetingTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd 9:00:00"));
					this.dtpStartTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd 8:00:00"));
					this.dtpEndTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd 17:30:00"));
				}
				else
				{
					this.txtf_MeetingNo.Text = this.curMeetingNo;
					string cmdText = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						this.txtf_MeetingName.Text = wgTools.SetObjToStr(sqlDataReader["f_MeetingName"]);
						this.cbof_MeetingAdr.Text = wgTools.SetObjToStr(sqlDataReader["f_MeetingAdr"]);
						this.dtpMeetingDate.Value = DateTime.Parse(Strings.Format(sqlDataReader["f_MeetingDateTime"], "yyyy-MM-dd"));
						this.dtpMeetingTime.Value = DateTime.Parse(Strings.Format(sqlDataReader["f_MeetingDateTime"], "yyyy-MM-dd HH:mm:ss"));
						this.dtpStartTime.Value = DateTime.Parse(Strings.Format(sqlDataReader["f_SignStartTime"], "yyyy-MM-dd HH:mm:ss"));
						this.dtpEndTime.Value = DateTime.Parse(Strings.Format(sqlDataReader["f_SignEndTime"], "yyyy-MM-dd HH:mm:ss"));
						this.txtf_Content.Text = wgTools.SetObjToStr(sqlDataReader["f_Content"]);
						this.txtf_Notes.Text = wgTools.SetObjToStr(sqlDataReader["f_Notes"]);
					}
					sqlDataReader.Close();
				}
				this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
				this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
				this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
				this.dtpMeetingTime.CustomFormat = "HH:mm";
				this.dtpMeetingTime.Format = DateTimePickerFormat.Custom;
				this.dtpStartTime.CustomFormat = "HH:mm";
				this.dtpStartTime.Format = DateTimePickerFormat.Custom;
				this.dtpEndTime.CustomFormat = "HH:mm";
				this.dtpEndTime.Format = DateTimePickerFormat.Custom;
				wgAppConfig.setDisplayFormatDate(this.dtpMeetingDate, wgTools.DisplayFormat_DateYMDWeek);
				this.backgroundWorker1.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			Cursor.Current = current;
		}

		private void dfrmMeetingSet_Load_Acc(object sender, EventArgs e)
		{
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			base.KeyPreview = true;
			try
			{
				this.cboIdentity.SelectedIndex = 0;
				this.loadGroupData();
				this.loadMeetingAdr();
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				if (this.curMeetingNo == "")
				{
					this.txtf_MeetingNo.Text = Strings.Format(DateTime.Now, "yyyyMMdd_HHmmss");
					this.dtpMeetingDate.Value = DateTime.Now.Date;
					this.dtpMeetingTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd 9:00:00"));
					this.dtpStartTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd 8:00:00"));
					this.dtpEndTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd 17:30:00"));
				}
				else
				{
					this.txtf_MeetingNo.Text = this.curMeetingNo;
					string cmdText = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection);
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						this.txtf_MeetingName.Text = wgTools.SetObjToStr(oleDbDataReader["f_MeetingName"]);
						this.cbof_MeetingAdr.Text = wgTools.SetObjToStr(oleDbDataReader["f_MeetingAdr"]);
						this.dtpMeetingDate.Value = DateTime.Parse(Strings.Format(oleDbDataReader["f_MeetingDateTime"], "yyyy-MM-dd"));
						this.dtpMeetingTime.Value = DateTime.Parse(Strings.Format(oleDbDataReader["f_MeetingDateTime"], "yyyy-MM-dd HH:mm:ss"));
						this.dtpStartTime.Value = DateTime.Parse(Strings.Format(oleDbDataReader["f_SignStartTime"], "yyyy-MM-dd HH:mm:ss"));
						this.dtpEndTime.Value = DateTime.Parse(Strings.Format(oleDbDataReader["f_SignEndTime"], "yyyy-MM-dd HH:mm:ss"));
						this.txtf_Content.Text = wgTools.SetObjToStr(oleDbDataReader["f_Content"]);
						this.txtf_Notes.Text = wgTools.SetObjToStr(oleDbDataReader["f_Notes"]);
					}
					oleDbDataReader.Close();
				}
				this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
				this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
				this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
				this.dtpMeetingTime.CustomFormat = "HH:mm";
				this.dtpMeetingTime.Format = DateTimePickerFormat.Custom;
				this.dtpStartTime.CustomFormat = "HH:mm";
				this.dtpStartTime.Format = DateTimePickerFormat.Custom;
				this.dtpEndTime.CustomFormat = "HH:mm";
				this.dtpEndTime.Format = DateTimePickerFormat.Custom;
				wgAppConfig.setDisplayFormatDate(this.dtpMeetingDate, wgTools.DisplayFormat_DateYMDWeek);
				this.backgroundWorker1.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			Cursor.Current = current;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOk_Click_Acc(sender, e);
				return;
			}
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				if (this.txtf_MeetingName.Text.Trim() == "")
				{
					XMessageBox.Show(CommonStr.strMeetingNameIsEmpty);
					return;
				}
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				if (this.curMeetingNo == "")
				{
					string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						this.curMeetingNo = this.txtf_MeetingNo.Text;
					}
					sqlDataReader.Close();
				}
				if (this.curMeetingNo == "")
				{
					string text = "INSERT INTO t_d_Meeting ([f_MeetingNO], [f_MeetingName], [f_MeetingAdr], [f_MeetingDateTime], [f_SignStartTime], [f_SignEndTime], [f_Content], [f_Notes]) ";
					text = text + "VALUES(" + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					text = text + " , " + wgTools.PrepareStr(this.txtf_MeetingName.Text);
					text = text + " , " + wgTools.PrepareStr(this.cbof_MeetingAdr.Text);
					text = text + " , " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpMeetingTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpStartTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpEndTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + wgTools.PrepareStr(this.txtf_Content.Text);
					text = text + " , " + wgTools.PrepareStr(this.txtf_Notes.Text);
					text += ")";
					SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
					int num = sqlCommand.ExecuteNonQuery();
					if (num > 0)
					{
					}
				}
				else
				{
					string text = "   Update [t_d_Meeting] ";
					text = text + " SET [f_MeetingName]=" + wgTools.PrepareStr(this.txtf_MeetingName.Text);
					text = text + " , [f_MeetingAdr]=" + wgTools.PrepareStr(this.cbof_MeetingAdr.Text);
					text = text + " , [f_MeetingDateTime]=" + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpMeetingTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , [f_SignStartTime]=" + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpStartTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , [f_SignEndTime]=" + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpEndTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , [f_Content]=" + wgTools.PrepareStr(this.txtf_Content.Text);
					text = text + " , [f_Notes]=" + wgTools.PrepareStr(this.txtf_Notes.Text);
					text = text + " WHERE  f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
					int num = sqlCommand.ExecuteNonQuery();
				}
				if (this.bNeedUpdateMeetingConsumer)
				{
					string text2 = "";
					this.dvGroupedConsumers = (DataView)this.dgvSelectedUsers.DataSource;
					if (this.dvGroupedConsumers.Count > 0)
					{
						for (int i = 0; i <= this.dvGroupedConsumers.Count - 1; i++)
						{
							text2 = text2 + this.dvGroupedConsumers[i]["f_ConsumerID"] + ",";
						}
						text2 += 0;
						string text = " Delete  FROM t_d_MeetingConsumer  WHERE f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
						if (text2 != "")
						{
							text = text + " AND f_ConsumerID NOT IN (" + text2 + ")";
						}
						SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
						sqlCommand.ExecuteNonQuery();
						sqlCommand = new SqlCommand(text, sqlConnection);
						text = " SELECT COUNT(*) FROM t_d_MeetingConsumer ";
						text = text + " WHERE  f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
						sqlCommand.CommandText = text;
						int num2 = (int)sqlCommand.ExecuteScalar();
						int num = 0;
						for (int i = 0; i <= this.dvGroupedConsumers.Count - 1; i++)
						{
							if (num2 > 0)
							{
								text = " Update t_d_MeetingConsumer ";
								text = text + " SET f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
								text = text + ", f_MeetingIdentity = " + this.dvGroupedConsumers[i]["f_MeetingIdentity"];
								text = text + ", f_Seat = " + wgTools.PrepareStr(this.dvGroupedConsumers[i]["f_Seat"]);
								text = text + " WHERE  f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
								text = text + " AND f_ConsumerID = " + this.dvGroupedConsumers[i]["f_ConsumerID"];
								sqlCommand.CommandText = text;
								num = sqlCommand.ExecuteNonQuery();
							}
							if (num <= 0)
							{
								text = " INSERT INTO t_d_MeetingConsumer ( [f_MeetingNO], [f_ConsumerID], [f_MeetingIdentity], [f_Seat])";
								text = text + " VALUES( " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
								text = text + ",  " + this.dvGroupedConsumers[i]["f_ConsumerID"];
								text = text + ", " + this.dvGroupedConsumers[i]["f_MeetingIdentity"];
								text = text + ", " + wgTools.PrepareStr(this.dvGroupedConsumers[i]["f_Seat"]);
								text += " ) ";
								sqlCommand.CommandText = text;
								sqlCommand.ExecuteNonQuery();
							}
						}
					}
					else
					{
						string text = " Delete  FROM t_d_MeetingConsumer  WHERE f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
						if (text2 != "")
						{
							text = text + " AND f_ConsumerID NOT IN (" + text2 + ")";
						}
						SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
						sqlCommand.ExecuteNonQuery();
					}
				}
				if (sqlConnection.State == ConnectionState.Open)
				{
					sqlConnection.Close();
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			Cursor.Current = current;
		}

		private void btnOk_Click_Acc(object sender, EventArgs e)
		{
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				if (this.txtf_MeetingName.Text.Trim() == "")
				{
					XMessageBox.Show(CommonStr.strMeetingNameIsEmpty);
					return;
				}
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				if (this.curMeetingNo == "")
				{
					string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						this.curMeetingNo = this.txtf_MeetingNo.Text;
					}
					oleDbDataReader.Close();
				}
				if (this.curMeetingNo == "")
				{
					string text = "INSERT INTO t_d_Meeting ([f_MeetingNO], [f_MeetingName], [f_MeetingAdr], [f_MeetingDateTime], [f_SignStartTime], [f_SignEndTime], [f_Content], [f_Notes]) ";
					text = text + "VALUES(" + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					text = text + " , " + wgTools.PrepareStr(this.txtf_MeetingName.Text);
					text = text + " , " + wgTools.PrepareStr(this.cbof_MeetingAdr.Text);
					text = text + " , " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpMeetingTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpStartTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpEndTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + wgTools.PrepareStr(this.txtf_Content.Text);
					text = text + " , " + wgTools.PrepareStr(this.txtf_Notes.Text);
					text += ")";
					OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
					int num = oleDbCommand.ExecuteNonQuery();
					if (num > 0)
					{
					}
				}
				else
				{
					string text = "   Update [t_d_Meeting] ";
					text = text + " SET [f_MeetingName]=" + wgTools.PrepareStr(this.txtf_MeetingName.Text);
					text = text + " , [f_MeetingAdr]=" + wgTools.PrepareStr(this.cbof_MeetingAdr.Text);
					text = text + " , [f_MeetingDateTime]=" + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpMeetingTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , [f_SignStartTime]=" + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpStartTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , [f_SignEndTime]=" + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpEndTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , [f_Content]=" + wgTools.PrepareStr(this.txtf_Content.Text);
					text = text + " , [f_Notes]=" + wgTools.PrepareStr(this.txtf_Notes.Text);
					text = text + " WHERE  f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
					OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
					int num = oleDbCommand.ExecuteNonQuery();
				}
				if (this.bNeedUpdateMeetingConsumer)
				{
					string text2 = "";
					this.dvGroupedConsumers = (DataView)this.dgvSelectedUsers.DataSource;
					if (this.dvGroupedConsumers.Count > 0)
					{
						for (int i = 0; i <= this.dvGroupedConsumers.Count - 1; i++)
						{
							text2 = text2 + this.dvGroupedConsumers[i]["f_ConsumerID"] + ",";
						}
						text2 += 0;
						string text = " Delete  FROM t_d_MeetingConsumer  WHERE f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
						if (text2 != "")
						{
							text = text + " AND f_ConsumerID NOT IN (" + text2 + ")";
						}
						OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
						oleDbCommand.ExecuteNonQuery();
						oleDbCommand = new OleDbCommand(text, oleDbConnection);
						text = " SELECT COUNT(*) FROM t_d_MeetingConsumer ";
						text = text + " WHERE  f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
						oleDbCommand.CommandText = text;
						int num2 = (int)oleDbCommand.ExecuteScalar();
						int num = 0;
						for (int i = 0; i <= this.dvGroupedConsumers.Count - 1; i++)
						{
							if (num2 > 0)
							{
								text = " Update t_d_MeetingConsumer ";
								text = text + " SET f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
								text = text + ", f_MeetingIdentity = " + this.dvGroupedConsumers[i]["f_MeetingIdentity"];
								text = text + ", f_Seat = " + wgTools.PrepareStr(this.dvGroupedConsumers[i]["f_Seat"]);
								text = text + " WHERE  f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
								text = text + " AND f_ConsumerID = " + this.dvGroupedConsumers[i]["f_ConsumerID"];
								oleDbCommand.CommandText = text;
								num = oleDbCommand.ExecuteNonQuery();
							}
							if (num <= 0)
							{
								text = " INSERT INTO t_d_MeetingConsumer ( [f_MeetingNO], [f_ConsumerID], [f_MeetingIdentity], [f_Seat])";
								text = text + " VALUES( " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
								text = text + ",  " + this.dvGroupedConsumers[i]["f_ConsumerID"];
								text = text + ", " + this.dvGroupedConsumers[i]["f_MeetingIdentity"];
								text = text + ", " + wgTools.PrepareStr(this.dvGroupedConsumers[i]["f_Seat"]);
								text += " ) ";
								oleDbCommand.CommandText = text;
								oleDbCommand.ExecuteNonQuery();
							}
						}
					}
					else
					{
						string text = " Delete  FROM t_d_MeetingConsumer  WHERE f_MeetingNO= " + wgTools.PrepareStr(this.txtf_MeetingNo.Text);
						if (text2 != "")
						{
							text = text + " AND f_ConsumerID NOT IN (" + text2 + ")";
						}
						OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
						oleDbCommand.ExecuteNonQuery();
					}
				}
				if (oleDbConnection.State == ConnectionState.Open)
				{
					oleDbConnection.Close();
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			Cursor.Current = current;
		}

		private void dfrmMeetingSet_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
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

		public void loadMeetingAdr()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadMeetingAdr_Acc();
				return;
			}
			try
			{
				this.cbof_MeetingAdr.Items.Clear();
				DataSet dataSet = new DataSet();
				SqlConnection connection = new SqlConnection(wgAppConfig.dbConString);
				try
				{
					dataSet.Clear();
					SqlCommand selectCommand = new SqlCommand("Select DISTINCT f_MeetingAdr  from t_d_MeetingAdr ", connection);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
					sqlDataAdapter.Fill(dataSet, "t_d_MeetingAdr");
					if (dataSet.Tables["t_d_MeetingAdr"].Rows.Count > 0)
					{
						for (int i = 0; i <= dataSet.Tables["t_d_MeetingAdr"].Rows.Count - 1; i++)
						{
							this.cbof_MeetingAdr.Items.Add(dataSet.Tables["t_d_MeetingAdr"].Rows[i][0]);
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
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void loadMeetingAdr_Acc()
		{
			try
			{
				this.cbof_MeetingAdr.Items.Clear();
				DataSet dataSet = new DataSet();
				OleDbConnection connection = new OleDbConnection(wgAppConfig.dbConString);
				try
				{
					dataSet.Clear();
					OleDbCommand selectCommand = new OleDbCommand("Select DISTINCT f_MeetingAdr  from t_d_MeetingAdr ", connection);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
					oleDbDataAdapter.Fill(dataSet, "t_d_MeetingAdr");
					if (dataSet.Tables["t_d_MeetingAdr"].Rows.Count > 0)
					{
						for (int i = 0; i <= dataSet.Tables["t_d_MeetingAdr"].Rows.Count - 1; i++)
						{
							this.cbof_MeetingAdr.Items.Add(dataSet.Tables["t_d_MeetingAdr"].Rows[i][0]);
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
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnAddMeetingAdr_Click(object sender, EventArgs e)
		{
			try
			{
				string text = this.cbof_MeetingAdr.Text;
				using (dfrmMeetingAdr dfrmMeetingAdr = new dfrmMeetingAdr())
				{
					dfrmMeetingAdr.ShowDialog();
				}
				this.loadMeetingAdr();
				if (string.IsNullOrEmpty(text))
				{
					this.cbof_MeetingAdr.Text = text;
				}
				else
				{
					foreach (object current in this.cbof_MeetingAdr.Items)
					{
						if (current.ToString() == text)
						{
							this.cbof_MeetingAdr.Text = text;
						}
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

		private void btnCreateInfo_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				string text = "";
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					this.lblMeetingName.Text,
					"\t",
					this.txtf_MeetingName.Text,
					"\r\n"
				});
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					this.lblMeetingAddr.Text,
					"\t",
					this.cbof_MeetingAdr.Text,
					"\r\n"
				});
				string text4 = text;
				text = string.Concat(new string[]
				{
					text4,
					this.lblMeetingDateTime.Text,
					"\t",
					this.dtpMeetingDate.Text,
					" ",
					this.dtpMeetingTime.Text,
					"\r\n"
				});
				string text5 = text;
				text = string.Concat(new string[]
				{
					text5,
					this.lblSignBegin.Text,
					"\t",
					this.dtpStartTime.Text,
					"\r\n"
				});
				string text6 = text;
				text = string.Concat(new string[]
				{
					text6,
					this.lblSignEnd.Text,
					"\t",
					this.dtpEndTime.Text,
					"\r\n"
				});
				if (this.txtf_Content.Text.Length > 0)
				{
					text = text + this.lblContent.Text + "\r\n";
					text = text + this.txtf_Content.Text + "\r\n";
				}
				if (this.txtf_Notes.Text.Length > 0)
				{
					text = text + this.lblNotes.Text + "\r\n";
					text = text + this.txtf_Notes.Text + "\r\n";
				}
				text += "\r\n";
				text += "\r\n";
				text += Strings.Format(DateTime.Now, "yyyy-MM-dd");
				string fileName = this.txtf_MeetingNo.Text + "-" + Strings.Format(DateTime.Now, "yyyy-MM-dd_HHmmss_ff") + ".txt";
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.FileName = fileName;
					saveFileDialog.Filter = " (*.txt)|*.txt";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						string fileName2 = saveFileDialog.FileName;
						using (StreamWriter streamWriter = new StreamWriter(fileName2, true))
						{
							streamWriter.WriteLine(text);
						}
						fileName = fileName2;
					}
				}
				Process.Start(new ProcessStartInfo
				{
					FileName = fileName,
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			this.Cursor = Cursors.Default;
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

		private DataTable loadUserData4BackWork()
		{
			Thread.Sleep(100);
			wgTools.WriteLine("loadUserData Start");
			this.dtUser1 = new DataTable();
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT  t_b_Consumer.f_ConsumerID ";
				text += " , f_MeetingIdentity,' ' as  f_MeetingIdentityStr, f_ConsumerNO, f_ConsumerName, f_CardNO ";
				text += " , f_Seat ";
				text += " ,IIF (t_d_MeetingConsumer.f_MeetingIdentity IS NULL, 0,  IIF (  t_d_MeetingConsumer.f_MeetingIdentity <0 , 0 , 1 )) AS f_Selected ";
				text += " , f_GroupID ";
				text += " FROM t_b_Consumer ";
				text = text + " LEFT OUTER JOIN t_d_MeetingConsumer ON ( t_b_Consumer.f_ConsumerID = t_d_MeetingConsumer.f_ConsumerID AND f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text) + ")";
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
					goto IL_1A9;
				}
			}
			text = " SELECT  t_b_Consumer.f_ConsumerID ";
			text += " , f_MeetingIdentity,' ' as f_MeetingIdentityStr, f_ConsumerNO, f_ConsumerName, f_CardNO ";
			text += " , f_Seat ";
			text += " , CASE WHEN t_d_MeetingConsumer.f_MeetingIdentity IS NULL THEN 0 ELSE CASE WHEN t_d_MeetingConsumer.f_MeetingIdentity < 0 THEN 0 ELSE 1 END END AS f_Selected ";
			text += " , f_GroupID ";
			text += " FROM t_b_Consumer ";
			text = text + " LEFT OUTER JOIN t_d_MeetingConsumer ON ( t_b_Consumer.f_ConsumerID = t_d_MeetingConsumer.f_ConsumerID AND f_MeetingNO = " + wgTools.PrepareStr(this.txtf_MeetingNo.Text) + ")";
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
			IL_1A9:
			for (int i = 0; i < this.dtUser1.Rows.Count; i++)
			{
				DataRow dataRow = this.dtUser1.Rows[i];
				if (!string.IsNullOrEmpty(dataRow["f_MeetingIdentity"].ToString()) && (int)dataRow["f_MeetingIdentity"] >= 0)
				{
					dataRow["f_MeetingIdentityStr"] = this.cboIdentity.Items[(int)dataRow["f_MeetingIdentity"]].ToString();
				}
			}
			this.dtUser1.AcceptChanges();
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
			dfrmMeetingSet.lastLoadUsers = icConsumerShare.getUpdateLog();
			dfrmMeetingSet.dtLastLoad = this.dtUser1;
			return this.dtUser1;
		}

		private void loadUserData4BackWorkComplete(DataTable dtUser)
		{
			this.dv = new DataView(dtUser);
			this.dvSelected = new DataView(dtUser);
			this.dv.RowFilter = "f_Selected = 0";
			this.dvSelected.RowFilter = "f_Selected > 0";
			this.dvSelected.Sort = "f_MeetingIdentity ASC, f_ConsumerNo ASC ";
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

		private void btnAddAll_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("btnAddAllUsers_Click Start");
			DataTable table = ((DataView)this.dgvUsers.DataSource).Table;
			DataView dataSource = (DataView)this.dgvUsers.DataSource;
			DataView dataSource2 = (DataView)this.dgvSelectedUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvSelectedUsers.DataSource = null;
			int selectedIndex = this.cboIdentity.SelectedIndex;
			if (this.strGroupFilter == "")
			{
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if ((int)table.Rows[i]["f_Selected"] != 1)
					{
						table.Rows[i]["f_Selected"] = 1;
						table.Rows[i]["f_Seat"] = this.txtSeat.Text;
						table.Rows[i]["f_MeetingIdentity"] = selectedIndex;
						table.Rows[i]["f_MeetingIdentityStr"] = this.cboIdentity.Items[selectedIndex];
					}
				}
			}
			else
			{
				this.dv = new DataView(table);
				this.dv.RowFilter = this.strGroupFilter;
				for (int j = 0; j < this.dv.Count; j++)
				{
					if ((int)this.dv[j]["f_Selected"] != 1)
					{
						this.dv[j]["f_Selected"] = 1;
						this.dv[j]["f_Seat"] = this.txtSeat.Text;
						this.dv[j]["f_MeetingIdentity"] = selectedIndex;
						this.dv[j]["f_MeetingIdentityStr"] = this.cboIdentity.Items[selectedIndex];
					}
				}
			}
			this.dgvUsers.DataSource = dataSource;
			this.dgvSelectedUsers.DataSource = dataSource2;
			this.bNeedUpdateMeetingConsumer = true;
			Cursor.Current = Cursors.Default;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.cboIdentity.SelectedIndex;
			dfrmMeetingSet.selectObject(this.dgvUsers, "f_MeetingIdentity", selectedIndex, "f_Seat", this.txtSeat.Text.ToString(), "f_MeetingIdentityStr", this.cboIdentity.Items[selectedIndex].ToString());
			this.bNeedUpdateMeetingConsumer = true;
		}

		public static void selectObject(DataGridView dgv, string secondField, int val, string secondField2, string val2, string secondField3, string val3)
		{
			try
			{
				int index;
				if (dgv.SelectedRows.Count <= 0)
				{
					if (dgv.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dgv.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dgv.SelectedRows[0].Index;
				}
				using (DataTable table = ((DataView)dgv.DataSource).Table)
				{
					if (dgv.SelectedRows.Count > 0)
					{
						int count = dgv.SelectedRows.Count;
						int[] array = new int[count];
						for (int i = 0; i < dgv.SelectedRows.Count; i++)
						{
							array[i] = (int)dgv.SelectedRows[i].Cells[0].Value;
						}
						for (int j = 0; j < count; j++)
						{
							int num = array[j];
							DataRow dataRow = table.Rows.Find(num);
							if (dataRow != null)
							{
								dataRow["f_Selected"] = 1;
								if (secondField != "")
								{
									dataRow[secondField] = val;
								}
								if (secondField2 != "")
								{
									dataRow[secondField2] = val2;
								}
								if (secondField3 != "")
								{
									dataRow[secondField3] = val3;
								}
							}
						}
					}
					else
					{
						int num2 = (int)dgv.Rows[index].Cells[0].Value;
						DataRow dataRow = table.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = 1;
							if (secondField != "")
							{
								dataRow[secondField] = val;
							}
							if (secondField2 != "")
							{
								dataRow[secondField2] = val2;
							}
							if (secondField3 != "")
							{
								dataRow[secondField3] = val3;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void btnDelOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedUsers);
			this.bNeedUpdateMeetingConsumer = true;
		}

		private void btnDelAllUsers_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			this.dt = ((DataView)this.dgvUsers.DataSource).Table;
			this.dv1 = (DataView)this.dgvUsers.DataSource;
			this.dv2 = (DataView)this.dgvSelectedUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvSelectedUsers.DataSource = null;
			for (int i = 0; i < this.dt.Rows.Count; i++)
			{
				this.dt.Rows[i]["f_Selected"] = 0;
			}
			this.dgvUsers.DataSource = this.dv1;
			this.dgvSelectedUsers.DataSource = this.dv2;
			this.bNeedUpdateMeetingConsumer = true;
			Cursor.Current = Cursors.Default;
		}

		private void dfrmMeetingSet_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
