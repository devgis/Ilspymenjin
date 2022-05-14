using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmPrivilege : frmN3000
	{
		private IContainer components;

		private ComboBox cbof_GroupID;

		private Label label4;

		private Label label1;

		private ComboBox cbof_ControlSegID;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Button btnDelAllUsers;

		private Button btnDelOneUser;

		private Button btnAddOneUser;

		private Button btnAddAllUsers;

		private Button btnDelAllDoors;

		private Button btnDelOneDoor;

		private Button btnAddOneDoor;

		private Button btnAddAllDoors;

		private Button btnAddPassAndUpload;

		private Button btnDeletePassAndUpload;

		private DataGridView dgvUsers;

		private DataGridView dgvSelectedUsers;

		private Label label3;

		private DataGridView dgvDoors;

		private DataGridView dgvSelectedDoors;

		private Button btnExit;

		private Button btnAddPass;

		private Button btnDeletePass;

		private ComboBox cbof_ZoneID;

		private Label label25;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn f_Selected2;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		private DataGridViewTextBoxColumn f_ZoneID;

		private ProgressBar progressBar1;

		private BackgroundWorker backgroundWorker1;

		private System.Windows.Forms.Timer timer1;

		private Label lblWait;

		private ToolTip toolTip1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn f_SelectedGroup;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn UserID;

		private DataGridViewTextBoxColumn UserName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewTextBoxColumn f_GroupID;

		private DataGridViewCheckBoxColumn f_SelectedUsers;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private dfrmFind dfrmFind1;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private int[] controlSegIDList = new int[256];

		private string strGroupFilter = "";

		private DataView dv1;

		private DataView dv2;

		private DataView dv;

		private DataView dvSelected;

		private DataTable dt;

		private DataView dvtmp;

		private DataTable dtDoorTmpSelected;

		private DataView dvDoorTmpSelected;

		private DataView dvSelectedControllerID;

		private SqlCommand cmd;

		private SqlConnection cn;

		private dfrmWait dfrmWait1 = new dfrmWait();

		private bool bEdit;

		private string strZoneFilter = "";

		private bool bStarting = true;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dfrmWait1 != null)
			{
				this.dfrmWait1.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPrivilege));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.btnDeletePass = new Button();
			this.btnAddPass = new Button();
			this.btnExit = new Button();
			this.btnDeletePassAndUpload = new Button();
			this.btnAddPassAndUpload = new Button();
			this.groupBox2 = new GroupBox();
			this.cbof_ZoneID = new ComboBox();
			this.label25 = new Label();
			this.dgvSelectedDoors = new DataGridView();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.f_Selected2 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.dgvDoors = new DataGridView();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.f_ZoneID = new DataGridViewTextBoxColumn();
			this.btnDelAllDoors = new Button();
			this.btnDelOneDoor = new Button();
			this.btnAddOneDoor = new Button();
			this.btnAddAllDoors = new Button();
			this.groupBox1 = new GroupBox();
			this.cbof_ControlSegID = new ComboBox();
			this.cbof_GroupID = new ComboBox();
			this.lblWait = new Label();
			this.label3 = new Label();
			this.dgvSelectedUsers = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.f_SelectedGroup = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.dgvUsers = new DataGridView();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.UserName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			this.btnDelAllUsers = new Button();
			this.btnDelOneUser = new Button();
			this.btnAddOneUser = new Button();
			this.btnAddAllUsers = new Button();
			this.label1 = new Label();
			this.label4 = new Label();
			this.progressBar1 = new ProgressBar();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.dgvSelectedDoors).BeginInit();
			((ISupportInitialize)this.dgvDoors).BeginInit();
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
			componentResourceManager.ApplyResources(this.btnDeletePass, "btnDeletePass");
			this.btnDeletePass.BackColor = Color.Transparent;
			this.btnDeletePass.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeletePass.ForeColor = Color.White;
			this.btnDeletePass.Image = Resources.Rec2NoPass;
			this.btnDeletePass.Name = "btnDeletePass";
			this.toolTip1.SetToolTip(this.btnDeletePass, componentResourceManager.GetString("btnDeletePass.ToolTip"));
			this.btnDeletePass.UseVisualStyleBackColor = false;
			this.btnDeletePass.Click += new EventHandler(this.btnDeletePass_Click);
			componentResourceManager.ApplyResources(this.btnAddPass, "btnAddPass");
			this.btnAddPass.BackColor = Color.Transparent;
			this.btnAddPass.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddPass.ForeColor = Color.White;
			this.btnAddPass.Image = Resources.Rec1Pass;
			this.btnAddPass.Name = "btnAddPass";
			this.toolTip1.SetToolTip(this.btnAddPass, componentResourceManager.GetString("btnAddPass.ToolTip"));
			this.btnAddPass.UseVisualStyleBackColor = false;
			this.btnAddPass.Click += new EventHandler(this.btnAddPass_Click);
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.toolTip1.SetToolTip(this.btnExit, componentResourceManager.GetString("btnExit.ToolTip"));
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this.btnDeletePassAndUpload, "btnDeletePassAndUpload");
			this.btnDeletePassAndUpload.BackColor = Color.Transparent;
			this.btnDeletePassAndUpload.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeletePassAndUpload.ForeColor = Color.White;
			this.btnDeletePassAndUpload.Image = Resources.wg16UploadNoPass;
			this.btnDeletePassAndUpload.Name = "btnDeletePassAndUpload";
			this.toolTip1.SetToolTip(this.btnDeletePassAndUpload, componentResourceManager.GetString("btnDeletePassAndUpload.ToolTip"));
			this.btnDeletePassAndUpload.UseVisualStyleBackColor = false;
			this.btnDeletePassAndUpload.Click += new EventHandler(this.btnDeletePassAndUpload_Click);
			componentResourceManager.ApplyResources(this.btnAddPassAndUpload, "btnAddPassAndUpload");
			this.btnAddPassAndUpload.BackColor = Color.Transparent;
			this.btnAddPassAndUpload.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddPassAndUpload.ForeColor = Color.White;
			this.btnAddPassAndUpload.Image = Resources.wg16UploadPass;
			this.btnAddPassAndUpload.Name = "btnAddPassAndUpload";
			this.toolTip1.SetToolTip(this.btnAddPassAndUpload, componentResourceManager.GetString("btnAddPassAndUpload.ToolTip"));
			this.btnAddPassAndUpload.UseVisualStyleBackColor = false;
			this.btnAddPassAndUpload.Click += new EventHandler(this.btnAddPassAndUpload_Click);
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.cbof_ZoneID);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.dgvSelectedDoors);
			this.groupBox2.Controls.Add(this.dgvDoors);
			this.groupBox2.Controls.Add(this.btnDelAllDoors);
			this.groupBox2.Controls.Add(this.btnDelOneDoor);
			this.groupBox2.Controls.Add(this.btnAddOneDoor);
			this.groupBox2.Controls.Add(this.btnAddAllDoors);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			this.toolTip1.SetToolTip(this.groupBox2, componentResourceManager.GetString("groupBox2.ToolTip"));
			componentResourceManager.ApplyResources(this.cbof_ZoneID, "cbof_ZoneID");
			this.cbof_ZoneID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ZoneID.FormattingEnabled = true;
			this.cbof_ZoneID.Name = "cbof_ZoneID";
			this.toolTip1.SetToolTip(this.cbof_ZoneID, componentResourceManager.GetString("cbof_ZoneID.ToolTip"));
			this.cbof_ZoneID.SelectedIndexChanged += new EventHandler(this.cbof_Zone_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label25, "label25");
			this.label25.Name = "label25";
			this.toolTip1.SetToolTip(this.label25, componentResourceManager.GetString("label25.ToolTip"));
			componentResourceManager.ApplyResources(this.dgvSelectedDoors, "dgvSelectedDoors");
			this.dgvSelectedDoors.AllowUserToAddRows = false;
			this.dgvSelectedDoors.AllowUserToDeleteRows = false;
			this.dgvSelectedDoors.AllowUserToOrderColumns = true;
			this.dgvSelectedDoors.BackgroundColor = Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedDoors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelectedDoors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelectedDoors.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn8,
				this.dataGridViewTextBoxColumn9,
				this.f_Selected2,
				this.Column1
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedDoors.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelectedDoors.EnableHeadersVisualStyles = false;
			this.dgvSelectedDoors.Name = "dgvSelectedDoors";
			this.dgvSelectedDoors.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedDoors.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvSelectedDoors.RowTemplate.Height = 23;
			this.dgvSelectedDoors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvSelectedDoors, componentResourceManager.GetString("dgvSelectedDoors.ToolTip"));
			this.dgvSelectedDoors.MouseDoubleClick += new MouseEventHandler(this.dgvSelectedDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected2, "f_Selected2");
			this.f_Selected2.Name = "f_Selected2";
			this.f_Selected2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column1, "Column1");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dgvDoors, "dgvDoors");
			this.dgvDoors.AllowUserToAddRows = false;
			this.dgvDoors.AllowUserToDeleteRows = false;
			this.dgvDoors.AllowUserToOrderColumns = true;
			this.dgvDoors.BackgroundColor = Color.White;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.dgvDoors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvDoors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvDoors.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.f_Selected,
				this.f_ZoneID
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Window;
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.dgvDoors.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvDoors.EnableHeadersVisualStyles = false;
			this.dgvDoors.Name = "dgvDoors";
			this.dgvDoors.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvDoors.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvDoors.RowTemplate.Height = 23;
			this.dgvDoors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvDoors, componentResourceManager.GetString("dgvDoors.ToolTip"));
			this.dgvDoors.KeyDown += new KeyEventHandler(this.dgvDoors_KeyDown);
			this.dgvDoors.MouseClick += new MouseEventHandler(this.dgvDoors_MouseClick);
			this.dgvDoors.MouseDoubleClick += new MouseEventHandler(this.dgvDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected, "f_Selected");
			this.f_Selected.Name = "f_Selected";
			this.f_Selected.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ZoneID, "f_ZoneID");
			this.f_ZoneID.Name = "f_ZoneID";
			this.f_ZoneID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnDelAllDoors, "btnDelAllDoors");
			this.btnDelAllDoors.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelAllDoors.Name = "btnDelAllDoors";
			this.toolTip1.SetToolTip(this.btnDelAllDoors, componentResourceManager.GetString("btnDelAllDoors.ToolTip"));
			this.btnDelAllDoors.UseVisualStyleBackColor = true;
			this.btnDelAllDoors.Click += new EventHandler(this.btnDelAllDoors_Click);
			componentResourceManager.ApplyResources(this.btnDelOneDoor, "btnDelOneDoor");
			this.btnDelOneDoor.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelOneDoor.Name = "btnDelOneDoor";
			this.toolTip1.SetToolTip(this.btnDelOneDoor, componentResourceManager.GetString("btnDelOneDoor.ToolTip"));
			this.btnDelOneDoor.UseVisualStyleBackColor = true;
			this.btnDelOneDoor.Click += new EventHandler(this.btnDelOneDoor_Click);
			componentResourceManager.ApplyResources(this.btnAddOneDoor, "btnAddOneDoor");
			this.btnAddOneDoor.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneDoor.Name = "btnAddOneDoor";
			this.toolTip1.SetToolTip(this.btnAddOneDoor, componentResourceManager.GetString("btnAddOneDoor.ToolTip"));
			this.btnAddOneDoor.UseVisualStyleBackColor = true;
			this.btnAddOneDoor.Click += new EventHandler(this.btnAddOneDoor_Click);
			componentResourceManager.ApplyResources(this.btnAddAllDoors, "btnAddAllDoors");
			this.btnAddAllDoors.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllDoors.Name = "btnAddAllDoors";
			this.toolTip1.SetToolTip(this.btnAddAllDoors, componentResourceManager.GetString("btnAddAllDoors.ToolTip"));
			this.btnAddAllDoors.UseVisualStyleBackColor = true;
			this.btnAddAllDoors.Click += new EventHandler(this.btnAddAllDoors_Click);
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.cbof_ControlSegID);
			this.groupBox1.Controls.Add(this.cbof_GroupID);
			this.groupBox1.Controls.Add(this.lblWait);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.dgvSelectedUsers);
			this.groupBox1.Controls.Add(this.dgvUsers);
			this.groupBox1.Controls.Add(this.btnDelAllUsers);
			this.groupBox1.Controls.Add(this.btnDelOneUser);
			this.groupBox1.Controls.Add(this.btnAddOneUser);
			this.groupBox1.Controls.Add(this.btnAddAllUsers);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.toolTip1.SetToolTip(this.groupBox1, componentResourceManager.GetString("groupBox1.ToolTip"));
			componentResourceManager.ApplyResources(this.cbof_ControlSegID, "cbof_ControlSegID");
			this.cbof_ControlSegID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ControlSegID.FormattingEnabled = true;
			this.cbof_ControlSegID.Name = "cbof_ControlSegID";
			this.toolTip1.SetToolTip(this.cbof_ControlSegID, componentResourceManager.GetString("cbof_ControlSegID.ToolTip"));
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			this.cbof_GroupID.Name = "cbof_GroupID";
			this.toolTip1.SetToolTip(this.cbof_GroupID, componentResourceManager.GetString("cbof_GroupID.ToolTip"));
			this.cbof_GroupID.SelectedIndexChanged += new EventHandler(this.cbof_GroupID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.lblWait, "lblWait");
			this.lblWait.BorderStyle = BorderStyle.FixedSingle;
			this.lblWait.ForeColor = Color.White;
			this.lblWait.Name = "lblWait";
			this.toolTip1.SetToolTip(this.lblWait, componentResourceManager.GetString("lblWait.ToolTip"));
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.toolTip1.SetToolTip(this.label3, componentResourceManager.GetString("label3.ToolTip"));
			componentResourceManager.ApplyResources(this.dgvSelectedUsers, "dgvSelectedUsers");
			this.dgvSelectedUsers.AllowUserToAddRows = false;
			this.dgvSelectedUsers.AllowUserToDeleteRows = false;
			this.dgvSelectedUsers.AllowUserToOrderColumns = true;
			this.dgvSelectedUsers.BackgroundColor = Color.White;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle7.ForeColor = Color.White;
			dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgvSelectedUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelectedUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.f_SelectedGroup,
				this.dataGridViewCheckBoxColumn1
			});
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = SystemColors.Window;
			dataGridViewCellStyle8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle8.ForeColor = Color.White;
			dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedUsers.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgvSelectedUsers.EnableHeadersVisualStyles = false;
			this.dgvSelectedUsers.Name = "dgvSelectedUsers";
			this.dgvSelectedUsers.ReadOnly = true;
			dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = SystemColors.Control;
			dataGridViewCellStyle9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvSelectedUsers.RowTemplate.Height = 23;
			this.dgvSelectedUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvSelectedUsers, componentResourceManager.GetString("dgvSelectedUsers.ToolTip"));
			this.dgvSelectedUsers.MouseDoubleClick += new MouseEventHandler(this.dgvSelectedUsers_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle10;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedGroup, "f_SelectedGroup");
			this.f_SelectedGroup.Name = "f_SelectedGroup";
			this.f_SelectedGroup.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dgvUsers, "dgvUsers");
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AllowUserToDeleteRows = false;
			this.dgvUsers.AllowUserToOrderColumns = true;
			this.dgvUsers.BackgroundColor = Color.White;
			dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle11.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle11.ForeColor = Color.White;
			dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
			this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ConsumerID,
				this.UserID,
				this.UserName,
				this.CardNO,
				this.f_GroupID,
				this.f_SelectedUsers
			});
			dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = SystemColors.Window;
			dataGridViewCellStyle12.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle12.ForeColor = Color.White;
			dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = DataGridViewTriState.False;
			this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle12;
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle13.BackColor = SystemColors.Control;
			dataGridViewCellStyle13.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle13.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle13.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvUsers, componentResourceManager.GetString("dgvUsers.ToolTip"));
			this.dgvUsers.KeyDown += new KeyEventHandler(this.dgvUsers_KeyDown);
			this.dgvUsers.MouseDoubleClick += new MouseEventHandler(this.dgvUsers_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.UserID.DefaultCellStyle = dataGridViewCellStyle14;
			componentResourceManager.ApplyResources(this.UserID, "UserID");
			this.UserID.Name = "UserID";
			this.UserID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.UserName, "UserName");
			this.UserName.Name = "UserName";
			this.UserName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.CardNO, "CardNO");
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_GroupID, "f_GroupID");
			this.f_GroupID.Name = "f_GroupID";
			this.f_GroupID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedUsers, "f_SelectedUsers");
			this.f_SelectedUsers.Name = "f_SelectedUsers";
			this.f_SelectedUsers.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnDelAllUsers, "btnDelAllUsers");
			this.btnDelAllUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelAllUsers.Name = "btnDelAllUsers";
			this.toolTip1.SetToolTip(this.btnDelAllUsers, componentResourceManager.GetString("btnDelAllUsers.ToolTip"));
			this.btnDelAllUsers.UseVisualStyleBackColor = true;
			this.btnDelAllUsers.Click += new EventHandler(this.btnDelAllUsers_Click);
			componentResourceManager.ApplyResources(this.btnDelOneUser, "btnDelOneUser");
			this.btnDelOneUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelOneUser.Name = "btnDelOneUser";
			this.toolTip1.SetToolTip(this.btnDelOneUser, componentResourceManager.GetString("btnDelOneUser.ToolTip"));
			this.btnDelOneUser.UseVisualStyleBackColor = true;
			this.btnDelOneUser.Click += new EventHandler(this.btnDelOneUser_Click);
			componentResourceManager.ApplyResources(this.btnAddOneUser, "btnAddOneUser");
			this.btnAddOneUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneUser.Name = "btnAddOneUser";
			this.toolTip1.SetToolTip(this.btnAddOneUser, componentResourceManager.GetString("btnAddOneUser.ToolTip"));
			this.btnAddOneUser.UseVisualStyleBackColor = true;
			this.btnAddOneUser.Click += new EventHandler(this.btnAddOneUser_Click);
			componentResourceManager.ApplyResources(this.btnAddAllUsers, "btnAddAllUsers");
			this.btnAddAllUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllUsers.Name = "btnAddAllUsers";
			this.toolTip1.SetToolTip(this.btnAddAllUsers, componentResourceManager.GetString("btnAddAllUsers.ToolTip"));
			this.btnAddAllUsers.UseVisualStyleBackColor = true;
			this.btnAddAllUsers.Click += new EventHandler(this.btnAddAllUsers_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.toolTip1.SetToolTip(this.label1, componentResourceManager.GetString("label1.ToolTip"));
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			this.toolTip1.SetToolTip(this.label4, componentResourceManager.GetString("label4.ToolTip"));
			componentResourceManager.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Name = "progressBar1";
			this.toolTip1.SetToolTip(this.progressBar1, componentResourceManager.GetString("progressBar1.ToolTip"));
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.btnDeletePass);
			base.Controls.Add(this.btnAddPass);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnDeletePassAndUpload);
			base.Controls.Add(this.btnAddPassAndUpload);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.progressBar1);
			base.Name = "dfrmPrivilege";
			this.toolTip1.SetToolTip(this, componentResourceManager.GetString("$this.ToolTip"));
			base.FormClosing += new FormClosingEventHandler(this.dfrmPrivilege_FormClosing);
			base.Load += new EventHandler(this.dfrmPrivilege_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmPrivilege_KeyDown);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.dgvSelectedDoors).EndInit();
			((ISupportInitialize)this.dgvDoors).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			((ISupportInitialize)this.dgvUsers).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmPrivilege()
		{
			this.InitializeComponent();
		}

		private void dfrmPrivilege_KeyDown(object sender, KeyEventArgs e)
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
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cbof_ZoneID.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrZoneName[i].ToString()))
				{
					this.cbof_ZoneID.Items.Add(CommonStr.strAllZones);
				}
				else
				{
					this.cbof_ZoneID.Items.Add(this.arrZoneName[i].ToString());
				}
			}
			if (this.cbof_ZoneID.Items.Count > 0)
			{
				this.cbof_ZoneID.SelectedIndex = 0;
			}
			bool visible = true;
			this.label25.Visible = visible;
			this.cbof_ZoneID.Visible = visible;
		}

		private void dfrmPrivilege_Load(object sender, EventArgs e)
		{
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
			this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			try
			{
				this.label1.Visible = wgAppConfig.getParamValBoolByNO(121);
				this.cbof_ControlSegID.Visible = wgAppConfig.getParamValBoolByNO(121);
				this.loadControlSegData();
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
				this.loadZoneInfo();
				this.loadDoorData();
				this.cbof_Zone_SelectedIndexChanged(null, null);
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			if (!this.backgroundWorker1.IsBusy)
			{
				this.backgroundWorker1.RunWorkerAsync();
			}
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvDoors.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedDoors.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			Cursor.Current = Cursors.WaitCursor;
		}

		private void loadControlSegData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadControlSegData_Acc();
				return;
			}
			this.cbof_ControlSegID.Items.Clear();
			this.cbof_ControlSegID.Items.Add(CommonStr.strFreeTime);
			this.controlSegIDList[0] = 1;
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				string text = " SELECT ";
				text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
				text += "   CASE WHEN [f_ControlSegName] IS NULL THEN CONVERT(nvarchar(50),  ";
				text += "     ([t_b_ControlSeg].[f_ControlSegID])) ELSE (CONVERT(nvarchar(50), ";
				text += "     ([t_b_ControlSeg].[f_ControlSegID])) + ' [' + [f_ControlSegName] + ']') ";
				text += "    END AS f_ControlSegID  ";
				text += "  FROM [t_b_ControlSeg] WHERE  [t_b_ControlSeg].[f_ControlSegID]>1 ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
				using (SqlCommand sqlCommand = new SqlCommand(text, this.cn))
				{
					this.cn.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					int num = 1;
					while (sqlDataReader.Read())
					{
						this.cbof_ControlSegID.Items.Add(sqlDataReader["f_ControlSegID"]);
						this.controlSegIDList[num] = (int)sqlDataReader["f_ControlSegIDBak"];
						num++;
					}
					sqlDataReader.Close();
					if (this.cbof_ControlSegID.Items.Count > 0)
					{
						this.cbof_ControlSegID.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				if (this.cn != null)
				{
					this.cn.Dispose();
				}
			}
		}

		private void loadControlSegData_Acc()
		{
			this.cbof_ControlSegID.Items.Clear();
			this.cbof_ControlSegID.Items.Add(CommonStr.strFreeTime);
			this.controlSegIDList[0] = 1;
			OleDbConnection oleDbConnection = null;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				try
				{
					string text = " SELECT ";
					text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
					text += "  IIF(ISNULL([f_ControlSegName]), CSTR([t_b_ControlSeg].[f_ControlSegID]), CSTR([t_b_ControlSeg].[f_ControlSegID]) & ' [' & [f_ControlSegName] & ']') AS f_ControlSegID ";
					text += "  FROM [t_b_ControlSeg]  WHERE  [t_b_ControlSeg].[f_ControlSegID]>1  ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						int num = 1;
						while (oleDbDataReader.Read())
						{
							this.cbof_ControlSegID.Items.Add(oleDbDataReader["f_ControlSegID"]);
							this.controlSegIDList[num] = (int)oleDbDataReader["f_ControlSegIDBak"];
							num++;
						}
						oleDbDataReader.Close();
						if (this.cbof_ControlSegID.Items.Count > 0)
						{
							this.cbof_ControlSegID.SelectedIndex = 0;
						}
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
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
			try
			{
				this.dv.RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
				this.dvSelected.RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
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
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			wgTools.WriteLine("loadUserData End");
			this.cbof_GroupID_SelectedIndexChanged(null, null);
			Cursor.Current = Cursors.Default;
		}

		private void loadDoorData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadDoorData_Acc();
				return;
			}
			string text = " SELECT a.f_DoorID, a.f_DoorName , 0 as f_Selected, b.f_ZoneID, a.f_ControllerID, a.f_DoorNO,b.f_ControllerSN ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						this.dt = new DataTable();
						this.dv = new DataView(this.dt);
						this.dvSelected = new DataView(this.dt);
						sqlDataAdapter.Fill(this.dt);
						try
						{
							DataColumn[] primaryKey = new DataColumn[]
							{
								this.dt.Columns[0]
							};
							this.dt.PrimaryKey = primaryKey;
						}
						catch (Exception ex)
						{
							wgAppConfig.wgLog(ex.ToString());
						}
						this.dv.RowFilter = "f_Selected = 0";
						this.dvSelected.RowFilter = "f_Selected > 0";
						this.dgvDoors.AutoGenerateColumns = false;
						this.dgvDoors.DataSource = this.dv;
						this.dgvSelectedDoors.AutoGenerateColumns = false;
						this.dgvSelectedDoors.DataSource = this.dvSelected;
						for (int i = 0; i < this.dgvDoors.Columns.Count; i++)
						{
							this.dgvDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
							this.dgvSelectedDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
						}
					}
				}
			}
		}

		private void loadDoorData_Acc()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , 0 as f_Selected, b.f_ZoneID, a.f_ControllerID, a.f_DoorNO,b.f_ControllerSN ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						this.dt = new DataTable();
						this.dv = new DataView(this.dt);
						this.dvSelected = new DataView(this.dt);
						oleDbDataAdapter.Fill(this.dt);
						try
						{
							DataColumn[] primaryKey = new DataColumn[]
							{
								this.dt.Columns[0]
							};
							this.dt.PrimaryKey = primaryKey;
						}
						catch (Exception ex)
						{
							wgAppConfig.wgLog(ex.ToString());
						}
						this.dv.RowFilter = "f_Selected = 0";
						this.dvSelected.RowFilter = "f_Selected > 0";
						this.dgvDoors.AutoGenerateColumns = false;
						this.dgvDoors.DataSource = this.dv;
						this.dgvSelectedDoors.AutoGenerateColumns = false;
						this.dgvSelectedDoors.DataSource = this.dvSelected;
						for (int i = 0; i < this.dgvDoors.Columns.Count; i++)
						{
							this.dgvDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
							this.dgvSelectedDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
						}
					}
				}
			}
		}

		private void btnAddAllUsers_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			if (this.strGroupFilter == "")
			{
				icConsumerShare.selectAllUsers();
				((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
				if (this.dgvSelectedUsers.RowCount > 1000)
				{
					this.btnAddPassAndUpload.Enabled = false;
					this.btnDeletePassAndUpload.Enabled = false;
				}
				return;
			}
			wgTools.WriteLine("btnAddAllUsers_Click Start");
			this.dt = ((DataView)this.dgvUsers.DataSource).Table;
			this.dv1 = (DataView)this.dgvUsers.DataSource;
			this.dv2 = (DataView)this.dgvSelectedUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvSelectedUsers.DataSource = null;
			if (this.strGroupFilter == "")
			{
				return;
			}
			string rowFilter = this.dv1.RowFilter;
			string rowFilter2 = this.dv2.RowFilter;
			this.dv1.Dispose();
			this.dv2.Dispose();
			this.dv1 = null;
			this.dv2 = null;
			this.dt.BeginLoadData();
			this.dv = new DataView(this.dt);
			this.dv.RowFilter = this.strGroupFilter;
			for (int i = 0; i < this.dv.Count; i++)
			{
				this.dv[i]["f_Selected"] = icConsumerShare.getSelectedValue();
			}
			this.dt.EndLoadData();
			this.dv1 = new DataView(this.dt);
			this.dv1.RowFilter = rowFilter;
			this.dv2 = new DataView(this.dt);
			this.dv2.RowFilter = rowFilter2;
			this.dgvUsers.DataSource = this.dv1;
			this.dgvSelectedUsers.DataSource = this.dv2;
			wgTools.WriteLine("btnAddAllUsers_Click End");
			if (this.dv2.Count > 1000)
			{
				this.btnAddPassAndUpload.Enabled = false;
				this.btnDeletePassAndUpload.Enabled = false;
			}
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
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
					((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
					return;
				}
				((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  ({0}) AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
			}
		}

		private void btnAddOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvUsers, icConsumerShare.iSelectedCurrentNoneMax);
			if (this.dgvSelectedUsers.RowCount > 1000)
			{
				this.btnAddPassAndUpload.Enabled = false;
				this.btnDeletePassAndUpload.Enabled = false;
			}
		}

		private void btnDelOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedUsers, icConsumerShare.iSelectedCurrentNoneMax);
		}

		private void btnAddOneDoor_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvDoors);
		}

		private void btnDelOneDoor_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedDoors);
		}

		private void btnAddAllDoors_Click(object sender, EventArgs e)
		{
			this.dt = ((DataView)this.dgvDoors.DataSource).Table;
			if (this.cbof_ZoneID.SelectedIndex <= 0 && this.cbof_ZoneID.Text == CommonStr.strAllZones)
			{
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 1;
				}
				return;
			}
			if (this.cbof_ZoneID.SelectedIndex >= 0)
			{
				this.dvtmp = new DataView((this.dgvDoors.DataSource as DataView).Table);
				this.dvtmp.RowFilter = string.Format("  {0} ", this.strZoneFilter);
				for (int j = 0; j < this.dvtmp.Count; j++)
				{
					this.dvtmp[j]["f_Selected"] = 1;
				}
			}
		}

		private void btnDelAllDoors_Click(object sender, EventArgs e)
		{
			this.dt = ((DataView)this.dgvSelectedDoors.DataSource).Table;
			for (int i = 0; i < this.dt.Rows.Count; i++)
			{
				this.dt.Rows[i]["f_Selected"] = 0;
			}
		}

		private void logOperate(object sender)
		{
			string text = "";
			for (int i = 0; i <= Math.Min(wgAppConfig.LogEventMaxCount, this.dgvSelectedUsers.RowCount) - 1; i++)
			{
				text = text + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerName"] + ",";
			}
			if (this.dgvSelectedUsers.RowCount > wgAppConfig.LogEventMaxCount)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"......(",
					this.dgvSelectedUsers.RowCount,
					")"
				});
			}
			else
			{
				object obj2 = text;
				text = string.Concat(new object[]
				{
					obj2,
					"(",
					this.dgvSelectedUsers.RowCount,
					")"
				});
			}
			string text2 = "";
			for (int i = 0; i <= Math.Min(wgAppConfig.LogEventMaxCount, this.dgvSelectedDoors.RowCount) - 1; i++)
			{
				text2 = text2 + ((DataView)this.dgvSelectedDoors.DataSource)[i]["f_DoorName"] + ",";
			}
			if (this.dgvSelectedDoors.RowCount > wgAppConfig.LogEventMaxCount)
			{
				object obj3 = text2;
				text2 = string.Concat(new object[]
				{
					obj3,
					"......(",
					this.dgvSelectedDoors.RowCount,
					")"
				});
			}
			else
			{
				object obj4 = text2;
				text2 = string.Concat(new object[]
				{
					obj4,
					"(",
					this.dgvSelectedDoors.RowCount,
					")"
				});
			}
			wgAppConfig.wgLog(string.Format("{0}:[{1} => {2}]:{3} => {4}", new object[]
			{
				(sender as Button).Text.Replace("\r\n", ""),
				this.dgvSelectedUsers.RowCount.ToString(),
				this.dgvSelectedDoors.RowCount.ToString(),
				text,
				text2
			}), EventLogEntryType.Information, null);
		}

		private void btnAddPassAndUpload_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnAddPassAndUpload_Click_Acc(sender, e);
				return;
			}
			if (XMessageBox.Show(string.Concat(new string[]
			{
				(sender as Button).Text,
				" \r\n\r\n",
				CommonStr.strUsersNum,
				" = ",
				this.dgvSelectedUsers.RowCount.ToString(),
				"\r\n\r\n",
				CommonStr.strDoorsNum,
				" = ",
				this.dgvSelectedDoors.RowCount.ToString(),
				"? "
			}), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			if (this.dgvSelectedDoors.Rows.Count <= 0)
			{
				return;
			}
			if (this.dgvSelectedUsers.Rows.Count <= 0)
			{
				return;
			}
			this.bEdit = true;
			if (this.dgvSelectedUsers.Rows.Count > 1000 || this.dgvSelectedDoors.Rows.Count > 100)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("btnAddPass_Click Start");
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				this.cn.Open();
				this.cmd = new SqlCommand("", this.cn);
				try
				{
					int i = 0;
					i = 0;
					this.progressBar1.Maximum = this.dgvSelectedDoors.RowCount * this.dgvSelectedUsers.RowCount;
					bool flag = true;
					flag = true;
					this.dtDoorTmpSelected = ((DataView)this.dgvSelectedDoors.DataSource).Table.Copy();
					this.dvDoorTmpSelected = new DataView(this.dtDoorTmpSelected);
					this.dvSelectedControllerID = new DataView(this.dtDoorTmpSelected);
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					this.dvDoorTmpSelected.RowFilter = "f_Selected > 0";
					this.dvSelectedControllerID.RowFilter = "f_Selected = 2";
					foreach (DataRowView dataRowView in this.dvDoorTmpSelected)
					{
						this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", dataRowView["f_ControllerID"].ToString());
						if (this.dvSelectedControllerID.Count == wgMjController.GetControllerType(int.Parse(dataRowView["f_ControllerSN"].ToString())))
						{
							if (arrayList.IndexOf(int.Parse(dataRowView["f_ControllerID"].ToString())) < 0)
							{
								arrayList.Add(int.Parse(dataRowView["f_ControllerID"].ToString()));
								arrayList2.Add(int.Parse(dataRowView["f_ControllerSN"].ToString()));
							}
						}
						else
						{
							dataRowView["f_Selected"] = 2;
						}
					}
					this.dvDoorTmpSelected.RowFilter = "f_Selected = 2";
					int num = 0;
					int num2 = 0;
					while (i < this.dgvSelectedDoors.Rows.Count)
					{
						string text = "";
						int num3;
						if (arrayList.Count > 0 && num < arrayList.Count)
						{
							text = text + " [f_ControllerID] = ( " + arrayList[num].ToString() + ")";
							this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", arrayList[num].ToString());
							num++;
							num3 = this.dvSelectedControllerID.Count;
							i += this.dvSelectedControllerID.Count;
						}
						else
						{
							if (this.dvDoorTmpSelected.Count <= num2)
							{
								break;
							}
							text = text + " [f_DoorID] = ( " + this.dvDoorTmpSelected[num2]["f_DoorID"].ToString() + ")";
							num2++;
							num3 = 1;
							i++;
						}
						int num4 = 2000;
						int j = 0;
						while (j < this.dgvSelectedUsers.Rows.Count)
						{
							string text2 = "";
							if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
							{
								while (j < this.dgvSelectedUsers.Rows.Count)
								{
									text2 = text2 + ((DataView)this.dgvSelectedUsers.DataSource)[j]["f_ConsumerID"] + ",";
									j++;
									if (text2.Length > num4)
									{
										break;
									}
								}
								text2 += "0";
							}
							else
							{
								j = this.dgvSelectedUsers.Rows.Count;
							}
							if (flag)
							{
								string text3 = "DELETE FROM  [t_d_Privilege]  WHERE  ";
								text3 = text3 + "  ( " + text + ")";
								if (text2 != "")
								{
									text3 = text3 + " AND [f_ConsumerID] IN (" + text2 + " ) ";
								}
								this.cmd.CommandText = text3;
								wgTools.WriteLine(text3);
								this.cmd.ExecuteNonQuery();
								wgTools.WriteLine("DELETE FROM  [t_d_Privilege] End");
							}
							this.progressBar1.Value = j * num3 + this.dgvSelectedUsers.Rows.Count * (i - num3);
							Application.DoEvents();
						}
					}
					flag = true;
					i = 0;
					num = 0;
					num2 = 0;
					while (i < this.dgvSelectedDoors.Rows.Count)
					{
						string text = "";
						int num5;
						if (arrayList.Count > 0 && num < arrayList.Count)
						{
							text = text + " [f_ControllerID] = ( " + arrayList[num].ToString() + ")";
							this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", arrayList[num].ToString());
							num++;
							num5 = this.dvSelectedControllerID.Count;
							i += this.dvSelectedControllerID.Count;
						}
						else
						{
							if (this.dvDoorTmpSelected.Count <= num2)
							{
								break;
							}
							text = text + " [f_DoorID] = ( " + this.dvDoorTmpSelected[num2]["f_DoorID"].ToString() + ")";
							num2++;
							num5 = 1;
							i++;
						}
						int num6 = 2000;
						int k = 0;
						while (k < this.dgvSelectedUsers.Rows.Count)
						{
							string text4 = "";
							if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
							{
								while (k < this.dgvSelectedUsers.Rows.Count)
								{
									text4 = text4 + ((DataView)this.dgvSelectedUsers.DataSource)[k]["f_ConsumerID"] + ",";
									k++;
									if (text4.Length > num6)
									{
										break;
									}
								}
								text4 += "0";
							}
							else
							{
								k = this.dgvSelectedUsers.Rows.Count;
							}
							string text3 = "INSERT INTO [t_d_Privilege] (f_ConsumerID, f_DoorID ,f_ControllerID, f_DoorNO, f_ControlSegID)";
							object obj = text3;
							text3 = string.Concat(new object[]
							{
								obj,
								" SELECT t_b_Consumer.f_ConsumerID, t_b_Door.f_DoorID, t_b_Door.f_ControllerID ,t_b_Door.f_DoorNO, ",
								this.controlSegIDList[this.cbof_ControlSegID.SelectedIndex],
								" AS [f_ControlSegID]  "
							});
							text3 += " FROM t_b_Consumer, t_b_Door ";
							text3 += " WHERE  ((t_b_Consumer.f_DoorEnabled)=1) ";
							if (text4 != "")
							{
								text3 = text3 + " AND [f_ConsumerID] IN (" + text4 + " ) ";
							}
							text3 = text3 + " AND  ( " + text + ")";
							this.cmd.CommandText = text3;
							wgTools.WriteLine(text3);
							this.cmd.ExecuteNonQuery();
							wgTools.WriteLine("INSERT INTO [t_d_Privilege] End");
							this.progressBar1.Value = k * num5 + this.dgvSelectedUsers.Rows.Count * (i - num5);
							Application.DoEvents();
						}
					}
					string format;
					if (sender.Equals(this.btnAddPass))
					{
						format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
					}
					else
					{
						format = "UPDATE t_b_Controller SET f_lastDelAddAndUploadDateTime ={0}, f_lastDelAddAndUploadConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
					}
					for (int l = 0; l < this.dgvSelectedDoors.Rows.Count; l++)
					{
						string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, (int)((DataView)this.dgvSelectedDoors.DataSource)[l]["f_ControllerID"]);
						this.cmd.CommandText = text3;
						this.cmd.ExecuteNonQuery();
					}
					wgTools.WriteLine("btnAddPass_Click End");
					Cursor.Current = Cursors.Default;
					this.progressBar1.Value = this.progressBar1.Maximum;
					if (sender.Equals(this.btnAddPass))
					{
						this.logOperate(this.btnAddPass);
						XMessageBox.Show(string.Concat(new string[]
						{
							(sender as Button).Text,
							" \r\n\r\n",
							CommonStr.strUsersNum,
							" = ",
							this.dgvSelectedUsers.RowCount.ToString(),
							"\r\n\r\n",
							CommonStr.strDoorsNum,
							" = ",
							this.dgvSelectedDoors.RowCount.ToString(),
							"\r\n\r\n",
							CommonStr.strSuccessfully
						}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						this.progressBar1.Value = 0;
					}
					else
					{
						this.logOperate(this.btnAddPassAndUpload);
						this.progressBar1.Value = 0;
						ArrayList arrayList3 = new ArrayList();
						this.progressBar1.Maximum = this.dgvSelectedDoors.Rows.Count;
						if (this.dgvSelectedUsers.Rows.Count > 0)
						{
							using (icPrivilege icPrivilege = new icPrivilege())
							{
								using (icController icController = new icController())
								{
									for (int m = 0; m < this.dgvSelectedDoors.Rows.Count; m++)
									{
										int num7 = (int)((DataView)this.dgvSelectedDoors.DataSource)[m]["f_ControllerID"];
										if (arrayList3.IndexOf(num7) < 0)
										{
											icController.GetInfoFromDBByControllerID(num7);
											int controllerRunInformationIP = icController.GetControllerRunInformationIP();
											if (controllerRunInformationIP <= 0)
											{
												XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
												this.progressBar1.Value = 0;
												return;
											}
											if (icController.runinfo.registerCardNum == 0u && icPrivilege.ClearAllPrivilegeIP(icController.ControllerSN, icController.IP, icController.PORT) < 0)
											{
												XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
												this.progressBar1.Value = 0;
												return;
											}
											for (int n = 0; n < this.dgvSelectedUsers.Rows.Count; n++)
											{
												if (icPrivilege.AddPrivilegeOfOneCardByDB(num7, (int)((DataView)this.dgvSelectedUsers.DataSource)[n]["f_ConsumerID"]) < 0)
												{
													format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
													if (this.cn.State != ConnectionState.Open)
													{
														this.cn.Open();
													}
													for (int num8 = 0; num8 < this.dgvSelectedDoors.Rows.Count; num8++)
													{
														num7 = (int)((DataView)this.dgvSelectedDoors.DataSource)[num8]["f_ControllerID"];
														if (arrayList3.IndexOf(num7) < 0)
														{
															string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, num7);
															this.cmd.CommandText = text3;
															this.cmd.ExecuteNonQuery();
														}
													}
													XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
													this.progressBar1.Value = 0;
													return;
												}
											}
											arrayList3.Add(num7);
										}
										this.progressBar1.Value = m + 1;
									}
								}
							}
						}
						wgAppConfig.wgLog(string.Concat(new string[]
						{
							(sender as Button).Text.Replace("\r\n", ""),
							" ,",
							CommonStr.strUsersNum,
							" = ",
							this.dgvSelectedUsers.RowCount.ToString(),
							",",
							CommonStr.strDoorsNum,
							" = ",
							this.dgvSelectedDoors.RowCount.ToString(),
							",",
							CommonStr.strSuccessfully
						}), EventLogEntryType.Information, null);
						Cursor.Current = Cursors.Default;
						this.progressBar1.Value = this.progressBar1.Maximum;
						XMessageBox.Show(string.Concat(new string[]
						{
							(sender as Button).Text,
							" \r\n\r\n",
							CommonStr.strUsersNum,
							" = ",
							this.dgvSelectedUsers.RowCount.ToString(),
							"\r\n\r\n",
							CommonStr.strDoorsNum,
							" = ",
							this.dgvSelectedDoors.RowCount.ToString(),
							"\r\n\r\n",
							CommonStr.strSuccessfully
						}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						this.progressBar1.Value = 0;
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				finally
				{
					if (this.cmd != null)
					{
						this.cmd.Dispose();
					}
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			finally
			{
				if (this.cn != null)
				{
					this.cn.Dispose();
				}
				this.dfrmWait1.Hide();
			}
		}

		private void btnAddPassAndUpload_Click_Acc(object sender, EventArgs e)
		{
			OleDbCommand oleDbCommand = null;
			OleDbConnection oleDbConnection = null;
			if (XMessageBox.Show(string.Concat(new string[]
			{
				(sender as Button).Text,
				" \r\n\r\n",
				CommonStr.strUsersNum,
				" = ",
				this.dgvSelectedUsers.RowCount.ToString(),
				"\r\n\r\n",
				CommonStr.strDoorsNum,
				" = ",
				this.dgvSelectedDoors.RowCount.ToString(),
				"? "
			}), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			if (this.dgvSelectedDoors.Rows.Count <= 0)
			{
				return;
			}
			if (this.dgvSelectedUsers.Rows.Count <= 0)
			{
				return;
			}
			this.bEdit = true;
			if (this.dgvSelectedUsers.Rows.Count > 1000 || this.dgvSelectedDoors.Rows.Count > 100)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("btnAddPass_Click Start");
			oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				oleDbConnection.Open();
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					try
					{
						int i = 0;
						i = 0;
						this.progressBar1.Maximum = this.dgvSelectedDoors.RowCount * this.dgvSelectedUsers.RowCount;
						bool flag = true;
						flag = true;
						this.dtDoorTmpSelected = ((DataView)this.dgvSelectedDoors.DataSource).Table.Copy();
						this.dvDoorTmpSelected = new DataView(this.dtDoorTmpSelected);
						this.dvSelectedControllerID = new DataView(this.dtDoorTmpSelected);
						ArrayList arrayList = new ArrayList();
						ArrayList arrayList2 = new ArrayList();
						this.dvDoorTmpSelected.RowFilter = "f_Selected > 0";
						this.dvSelectedControllerID.RowFilter = "f_Selected = 2";
						foreach (DataRowView dataRowView in this.dvDoorTmpSelected)
						{
							this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", dataRowView["f_ControllerID"].ToString());
							if (this.dvSelectedControllerID.Count == wgMjController.GetControllerType(int.Parse(dataRowView["f_ControllerSN"].ToString())))
							{
								if (arrayList.IndexOf(int.Parse(dataRowView["f_ControllerID"].ToString())) < 0)
								{
									arrayList.Add(int.Parse(dataRowView["f_ControllerID"].ToString()));
									arrayList2.Add(int.Parse(dataRowView["f_ControllerSN"].ToString()));
								}
							}
							else
							{
								dataRowView["f_Selected"] = 2;
							}
						}
						this.dvDoorTmpSelected.RowFilter = "f_Selected = 2";
						int num = 0;
						int num2 = 0;
						while (i < this.dgvSelectedDoors.Rows.Count)
						{
							string text = "";
							int num3;
							if (arrayList.Count > 0 && num < arrayList.Count)
							{
								text = text + " [f_ControllerID] = ( " + arrayList[num].ToString() + ")";
								this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", arrayList[num].ToString());
								num++;
								num3 = this.dvSelectedControllerID.Count;
								i += this.dvSelectedControllerID.Count;
							}
							else
							{
								if (this.dvDoorTmpSelected.Count <= num2)
								{
									break;
								}
								text = text + " [f_DoorID] = ( " + this.dvDoorTmpSelected[num2]["f_DoorID"].ToString() + ")";
								num2++;
								num3 = 1;
								i++;
							}
							int num4 = 2000;
							int j = 0;
							while (j < this.dgvSelectedUsers.Rows.Count)
							{
								string text2 = "";
								if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
								{
									while (j < this.dgvSelectedUsers.Rows.Count)
									{
										text2 = text2 + ((DataView)this.dgvSelectedUsers.DataSource)[j]["f_ConsumerID"] + ",";
										j++;
										if (text2.Length > num4)
										{
											break;
										}
									}
									text2 += "0";
								}
								else
								{
									j = this.dgvSelectedUsers.Rows.Count;
								}
								if (flag)
								{
									string text3 = "DELETE FROM  [t_d_Privilege]  WHERE  ";
									text3 = text3 + "  ( " + text + ")";
									if (text2 != "")
									{
										text3 = text3 + " AND [f_ConsumerID] IN (" + text2 + " ) ";
									}
									oleDbCommand.CommandText = text3;
									wgTools.WriteLine(text3);
									oleDbCommand.ExecuteNonQuery();
									wgTools.WriteLine("DELETE FROM  [t_d_Privilege] End");
								}
								this.progressBar1.Value = j * num3 + this.dgvSelectedUsers.Rows.Count * (i - num3);
								Application.DoEvents();
							}
						}
						flag = true;
						i = 0;
						num = 0;
						num2 = 0;
						while (i < this.dgvSelectedDoors.Rows.Count)
						{
							string text = "";
							int num5;
							if (arrayList.Count > 0 && num < arrayList.Count)
							{
								text = text + " [f_ControllerID] = ( " + arrayList[num].ToString() + ")";
								this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", arrayList[num].ToString());
								num++;
								num5 = this.dvSelectedControllerID.Count;
								i += this.dvSelectedControllerID.Count;
							}
							else
							{
								if (this.dvDoorTmpSelected.Count <= num2)
								{
									break;
								}
								text = text + " [f_DoorID] = ( " + this.dvDoorTmpSelected[num2]["f_DoorID"].ToString() + ")";
								num2++;
								num5 = 1;
								i++;
							}
							int num6 = 2000;
							int k = 0;
							while (k < this.dgvSelectedUsers.Rows.Count)
							{
								string text4 = "";
								if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
								{
									while (k < this.dgvSelectedUsers.Rows.Count)
									{
										text4 = text4 + ((DataView)this.dgvSelectedUsers.DataSource)[k]["f_ConsumerID"] + ",";
										k++;
										if (text4.Length > num6)
										{
											break;
										}
									}
									text4 += "0";
								}
								else
								{
									k = this.dgvSelectedUsers.Rows.Count;
								}
								string text3 = "INSERT INTO [t_d_Privilege] (f_ConsumerID, f_DoorID ,f_ControllerID, f_DoorNO, f_ControlSegID)";
								object obj = text3;
								text3 = string.Concat(new object[]
								{
									obj,
									" SELECT t_b_Consumer.f_ConsumerID, t_b_Door.f_DoorID, t_b_Door.f_ControllerID ,t_b_Door.f_DoorNO, ",
									this.controlSegIDList[this.cbof_ControlSegID.SelectedIndex],
									" AS [f_ControlSegID]  "
								});
								text3 += " FROM t_b_Consumer, t_b_Door ";
								text3 += " WHERE  ((t_b_Consumer.f_DoorEnabled)=1) ";
								if (text4 != "")
								{
									text3 = text3 + " AND [f_ConsumerID] IN (" + text4 + " ) ";
								}
								text3 = text3 + " AND  ( " + text + ")";
								oleDbCommand.CommandText = text3;
								wgTools.WriteLine(text3);
								oleDbCommand.ExecuteNonQuery();
								wgTools.WriteLine("INSERT INTO [t_d_Privilege] End");
								this.progressBar1.Value = k * num5 + this.dgvSelectedUsers.Rows.Count * (i - num5);
								Application.DoEvents();
							}
						}
						string format;
						if (sender.Equals(this.btnAddPass))
						{
							format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
						}
						else
						{
							format = "UPDATE t_b_Controller SET f_lastDelAddAndUploadDateTime ={0}, f_lastDelAddAndUploadConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
						}
						for (int l = 0; l < this.dgvSelectedDoors.Rows.Count; l++)
						{
							string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, (int)((DataView)this.dgvSelectedDoors.DataSource)[l]["f_ControllerID"]);
							oleDbCommand.CommandText = text3;
							oleDbCommand.ExecuteNonQuery();
						}
						wgTools.WriteLine("btnAddPass_Click End");
						Cursor.Current = Cursors.Default;
						this.progressBar1.Value = this.progressBar1.Maximum;
						if (sender.Equals(this.btnAddPass))
						{
							this.logOperate(this.btnAddPass);
							XMessageBox.Show(string.Concat(new string[]
							{
								(sender as Button).Text,
								" \r\n\r\n",
								CommonStr.strUsersNum,
								" = ",
								this.dgvSelectedUsers.RowCount.ToString(),
								"\r\n\r\n",
								CommonStr.strDoorsNum,
								" = ",
								this.dgvSelectedDoors.RowCount.ToString(),
								"\r\n\r\n",
								CommonStr.strSuccessfully
							}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							this.progressBar1.Value = 0;
						}
						else
						{
							this.logOperate(this.btnAddPassAndUpload);
							this.progressBar1.Value = 0;
							ArrayList arrayList3 = new ArrayList();
							this.progressBar1.Maximum = this.dgvSelectedDoors.Rows.Count;
							if (this.dgvSelectedUsers.Rows.Count > 0)
							{
								using (icPrivilege icPrivilege = new icPrivilege())
								{
									using (icController icController = new icController())
									{
										for (int m = 0; m < this.dgvSelectedDoors.Rows.Count; m++)
										{
											int num7 = (int)((DataView)this.dgvSelectedDoors.DataSource)[m]["f_ControllerID"];
											if (arrayList3.IndexOf(num7) < 0)
											{
												icController.GetInfoFromDBByControllerID(num7);
												int controllerRunInformationIP = icController.GetControllerRunInformationIP();
												if (controllerRunInformationIP <= 0)
												{
													XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
													this.progressBar1.Value = 0;
													return;
												}
												if (icController.runinfo.registerCardNum == 0u && icPrivilege.ClearAllPrivilegeIP(icController.ControllerSN, icController.IP, icController.PORT) < 0)
												{
													XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
													this.progressBar1.Value = 0;
													return;
												}
												for (int n = 0; n < this.dgvSelectedUsers.Rows.Count; n++)
												{
													if (icPrivilege.AddPrivilegeOfOneCardByDB(num7, (int)((DataView)this.dgvSelectedUsers.DataSource)[n]["f_ConsumerID"]) < 0)
													{
														format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
														if (oleDbConnection.State != ConnectionState.Open)
														{
															oleDbConnection.Open();
														}
														for (int num8 = 0; num8 < this.dgvSelectedDoors.Rows.Count; num8++)
														{
															num7 = (int)((DataView)this.dgvSelectedDoors.DataSource)[num8]["f_ControllerID"];
															if (arrayList3.IndexOf(num7) < 0)
															{
																string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, num7);
																oleDbCommand.CommandText = text3;
																oleDbCommand.ExecuteNonQuery();
															}
														}
														XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
														this.progressBar1.Value = 0;
														return;
													}
												}
												arrayList3.Add(num7);
											}
											this.progressBar1.Value = m + 1;
										}
									}
								}
							}
							wgAppConfig.wgLog(string.Concat(new string[]
							{
								(sender as Button).Text.Replace("\r\n", ""),
								" ,",
								CommonStr.strUsersNum,
								" = ",
								this.dgvSelectedUsers.RowCount.ToString(),
								",",
								CommonStr.strDoorsNum,
								" = ",
								this.dgvSelectedDoors.RowCount.ToString(),
								",",
								CommonStr.strSuccessfully
							}), EventLogEntryType.Information, null);
							Cursor.Current = Cursors.Default;
							this.progressBar1.Value = this.progressBar1.Maximum;
							XMessageBox.Show(string.Concat(new string[]
							{
								(sender as Button).Text,
								" \r\n\r\n",
								CommonStr.strUsersNum,
								" = ",
								this.dgvSelectedUsers.RowCount.ToString(),
								"\r\n\r\n",
								CommonStr.strDoorsNum,
								" = ",
								this.dgvSelectedDoors.RowCount.ToString(),
								"\r\n\r\n",
								CommonStr.strSuccessfully
							}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							this.progressBar1.Value = 0;
						}
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			finally
			{
				if (oleDbConnection != null)
				{
					oleDbConnection.Dispose();
				}
				this.dfrmWait1.Hide();
			}
		}

		private void btnDeletePassAndUpload_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnDeletePassAndUpload_Click_Acc(sender, e);
				return;
			}
			if (XMessageBox.Show(string.Concat(new string[]
			{
				(sender as Button).Text,
				" \r\n\r\n",
				CommonStr.strUsersNum,
				" = ",
				this.dgvSelectedUsers.RowCount.ToString(),
				"\r\n\r\n",
				CommonStr.strDoorsNum,
				" = ",
				this.dgvSelectedDoors.RowCount.ToString(),
				"? "
			}), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			if (this.dgvSelectedDoors.Rows.Count <= 0)
			{
				return;
			}
			if (this.dgvSelectedUsers.Rows.Count <= 0)
			{
				return;
			}
			this.bEdit = true;
			Cursor.Current = Cursors.WaitCursor;
			if (this.dgvSelectedUsers.Rows.Count > 1000)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			wgTools.WriteLine("btnDelete_Click Start");
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.cmd = new SqlCommand("", this.cn);
			try
			{
				this.cn.Open();
				this.dtDoorTmpSelected = ((DataView)this.dgvSelectedDoors.DataSource).Table.Copy();
				this.dvDoorTmpSelected = new DataView(this.dtDoorTmpSelected);
				this.dvSelectedControllerID = new DataView(this.dtDoorTmpSelected);
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				this.dvDoorTmpSelected.RowFilter = "f_Selected > 0";
				this.dvSelectedControllerID.RowFilter = "f_Selected = 2";
				foreach (DataRowView dataRowView in this.dvDoorTmpSelected)
				{
					this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", dataRowView["f_ControllerID"].ToString());
					if (this.dvSelectedControllerID.Count == wgMjController.GetControllerType(int.Parse(dataRowView["f_ControllerSN"].ToString())))
					{
						if (arrayList.IndexOf(int.Parse(dataRowView["f_ControllerID"].ToString())) < 0)
						{
							arrayList.Add(int.Parse(dataRowView["f_ControllerID"].ToString()));
							arrayList2.Add(int.Parse(dataRowView["f_ControllerSN"].ToString()));
						}
					}
					else
					{
						dataRowView["f_Selected"] = 2;
					}
				}
				this.dvDoorTmpSelected.RowFilter = "f_Selected = 2";
				int i = 0;
				this.progressBar1.Maximum = this.dgvSelectedDoors.RowCount * this.dgvSelectedUsers.RowCount;
				int num = 0;
				int num2 = 0;
				while (i < this.dgvSelectedDoors.Rows.Count)
				{
					string text = "";
					int num3;
					if (arrayList.Count > 0 && num < arrayList.Count)
					{
						text = text + " [f_ControllerID] = ( " + arrayList[num].ToString() + ")";
						this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", arrayList[num].ToString());
						num++;
						num3 = this.dvSelectedControllerID.Count;
						i += this.dvSelectedControllerID.Count;
					}
					else
					{
						if (this.dvDoorTmpSelected.Count <= num2)
						{
							break;
						}
						text = text + " [f_DoorID] = ( " + this.dvDoorTmpSelected[num2]["f_DoorID"].ToString() + ")";
						num2++;
						num3 = 1;
						i++;
					}
					int num4 = 2000;
					int j = 0;
					while (j < this.dgvSelectedUsers.Rows.Count)
					{
						string text2 = "";
						if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
						{
							while (j < this.dgvSelectedUsers.Rows.Count)
							{
								text2 = text2 + ((DataView)this.dgvSelectedUsers.DataSource)[j]["f_ConsumerID"] + ",";
								if (text2.Length > num4)
								{
									break;
								}
								j++;
							}
							text2 += "0";
						}
						else
						{
							j = this.dgvSelectedUsers.Rows.Count;
						}
						string text3 = "DELETE FROM  [t_d_Privilege]  WHERE  ";
						text3 = text3 + "  ( " + text + ")";
						if (text2 != "")
						{
							text3 = text3 + " AND [f_ConsumerID] IN (" + text2 + " ) ";
						}
						this.cmd.CommandText = text3;
						wgTools.WriteLine(text3);
						this.cmd.ExecuteNonQuery();
						wgTools.WriteLine("DELETE FROM  [t_d_Privilege] End");
						this.progressBar1.Value = j * num3 + this.dgvSelectedUsers.Rows.Count * (i - num3);
						Application.DoEvents();
					}
				}
				string format;
				if (sender.Equals(this.btnDeletePass))
				{
					format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
				}
				else
				{
					format = "UPDATE t_b_Controller SET f_lastDelAddAndUploadDateTime ={0}, f_lastDelAddAndUploadConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
				}
				for (int k = 0; k < this.dgvSelectedDoors.Rows.Count; k++)
				{
					string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, (int)((DataView)this.dgvSelectedDoors.DataSource)[k]["f_ControllerID"]);
					this.cmd.CommandText = text3;
					this.cmd.ExecuteNonQuery();
				}
				wgTools.WriteLine("btnDelete_Click End");
				this.progressBar1.Value = this.progressBar1.Maximum;
				Cursor.Current = Cursors.Default;
				if (sender.Equals(this.btnDeletePass))
				{
					this.logOperate(this.btnDeletePass);
					XMessageBox.Show(string.Concat(new string[]
					{
						(sender as Button).Text,
						" \r\n\r\n",
						CommonStr.strUsersNum,
						" = ",
						this.dgvSelectedUsers.RowCount.ToString(),
						"\r\n\r\n",
						CommonStr.strDoorsNum,
						" = ",
						this.dgvSelectedDoors.RowCount.ToString(),
						"\r\n\r\n",
						CommonStr.strSuccessfully
					}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.progressBar1.Value = 0;
				}
				else
				{
					this.logOperate(this.btnDeletePassAndUpload);
					ArrayList arrayList3 = new ArrayList();
					if (this.dgvSelectedUsers.Rows.Count > 0)
					{
						using (icPrivilege icPrivilege = new icPrivilege())
						{
							for (int l = 0; l < this.dgvSelectedDoors.Rows.Count; l++)
							{
								int num5 = (int)((DataView)this.dgvSelectedDoors.DataSource)[l]["f_ControllerID"];
								if (arrayList3.IndexOf(num5) < 0)
								{
									for (int m = 0; m < this.dgvSelectedUsers.Rows.Count; m++)
									{
										if (icPrivilege.DelPrivilegeOfOneCardByDB(num5, (int)((DataView)this.dgvSelectedUsers.DataSource)[m]["f_ConsumerID"]) < 0)
										{
											format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
											for (int n = 0; n < this.dgvSelectedDoors.Rows.Count; n++)
											{
												num5 = (int)((DataView)this.dgvSelectedDoors.DataSource)[n]["f_ControllerID"];
												if (arrayList3.IndexOf(num5) < 0)
												{
													string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, num5);
													this.cmd.CommandText = text3;
													this.cmd.ExecuteNonQuery();
												}
											}
											XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
											this.progressBar1.Value = 0;
											return;
										}
									}
									arrayList3.Add(num5);
								}
							}
						}
					}
					wgAppConfig.wgLog(string.Concat(new string[]
					{
						(sender as Button).Text.Replace("\r\n", ""),
						" ,",
						CommonStr.strUsersNum,
						" = ",
						this.dgvSelectedUsers.RowCount.ToString(),
						",",
						CommonStr.strDoorsNum,
						" = ",
						this.dgvSelectedDoors.RowCount.ToString(),
						",",
						CommonStr.strSuccessfully
					}), EventLogEntryType.Information, null);
					this.progressBar1.Value = this.progressBar1.Maximum;
					Cursor.Current = Cursors.Default;
					XMessageBox.Show((sender as Button).Text + " " + CommonStr.strSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.progressBar1.Value = 0;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				if (this.cmd != null)
				{
					this.cmd.Dispose();
				}
				if (this.cn != null)
				{
					this.cn.Dispose();
				}
				this.dfrmWait1.Hide();
			}
		}

		private void btnDeletePassAndUpload_Click_Acc(object sender, EventArgs e)
		{
			OleDbCommand oleDbCommand = null;
			OleDbConnection oleDbConnection = null;
			if (XMessageBox.Show(string.Concat(new string[]
			{
				(sender as Button).Text,
				" \r\n\r\n",
				CommonStr.strUsersNum,
				" = ",
				this.dgvSelectedUsers.RowCount.ToString(),
				"\r\n\r\n",
				CommonStr.strDoorsNum,
				" = ",
				this.dgvSelectedDoors.RowCount.ToString(),
				"? "
			}), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			if (this.dgvSelectedDoors.Rows.Count <= 0)
			{
				return;
			}
			if (this.dgvSelectedUsers.Rows.Count <= 0)
			{
				return;
			}
			this.bEdit = true;
			Cursor.Current = Cursors.WaitCursor;
			if (this.dgvSelectedUsers.Rows.Count > 1000)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			wgTools.WriteLine("btnDelete_Click Start");
			oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			oleDbCommand = new OleDbCommand("", oleDbConnection);
			try
			{
				oleDbConnection.Open();
				this.dtDoorTmpSelected = ((DataView)this.dgvSelectedDoors.DataSource).Table.Copy();
				this.dvDoorTmpSelected = new DataView(this.dtDoorTmpSelected);
				this.dvSelectedControllerID = new DataView(this.dtDoorTmpSelected);
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				this.dvDoorTmpSelected.RowFilter = "f_Selected > 0";
				this.dvSelectedControllerID.RowFilter = "f_Selected = 2";
				foreach (DataRowView dataRowView in this.dvDoorTmpSelected)
				{
					this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", dataRowView["f_ControllerID"].ToString());
					if (this.dvSelectedControllerID.Count == wgMjController.GetControllerType(int.Parse(dataRowView["f_ControllerSN"].ToString())))
					{
						if (arrayList.IndexOf(int.Parse(dataRowView["f_ControllerID"].ToString())) < 0)
						{
							arrayList.Add(int.Parse(dataRowView["f_ControllerID"].ToString()));
							arrayList2.Add(int.Parse(dataRowView["f_ControllerSN"].ToString()));
						}
					}
					else
					{
						dataRowView["f_Selected"] = 2;
					}
				}
				this.dvDoorTmpSelected.RowFilter = "f_Selected = 2";
				int i = 0;
				this.progressBar1.Maximum = this.dgvSelectedDoors.RowCount * this.dgvSelectedUsers.RowCount;
				int num = 0;
				int num2 = 0;
				while (i < this.dgvSelectedDoors.Rows.Count)
				{
					string text = "";
					int num3;
					if (arrayList.Count > 0 && num < arrayList.Count)
					{
						text = text + " [f_ControllerID] = ( " + arrayList[num].ToString() + ")";
						this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", arrayList[num].ToString());
						num++;
						num3 = this.dvSelectedControllerID.Count;
						i += this.dvSelectedControllerID.Count;
					}
					else
					{
						if (this.dvDoorTmpSelected.Count <= num2)
						{
							break;
						}
						text = text + " [f_DoorID] = ( " + this.dvDoorTmpSelected[num2]["f_DoorID"].ToString() + ")";
						num2++;
						num3 = 1;
						i++;
					}
					int num4 = 2000;
					int j = 0;
					while (j < this.dgvSelectedUsers.Rows.Count)
					{
						string text2 = "";
						if (((DataView)this.dgvSelectedUsers.DataSource).Table.Rows.Count > this.dgvSelectedUsers.Rows.Count)
						{
							while (j < this.dgvSelectedUsers.Rows.Count)
							{
								text2 = text2 + ((DataView)this.dgvSelectedUsers.DataSource)[j]["f_ConsumerID"] + ",";
								if (text2.Length > num4)
								{
									break;
								}
								j++;
							}
							text2 += "0";
						}
						else
						{
							j = this.dgvSelectedUsers.Rows.Count;
						}
						string text3 = "DELETE FROM  [t_d_Privilege]  WHERE  ";
						text3 = text3 + "  ( " + text + ")";
						if (text2 != "")
						{
							text3 = text3 + " AND [f_ConsumerID] IN (" + text2 + " ) ";
						}
						oleDbCommand.CommandText = text3;
						wgTools.WriteLine(text3);
						oleDbCommand.ExecuteNonQuery();
						wgTools.WriteLine("DELETE FROM  [t_d_Privilege] End");
						this.progressBar1.Value = j * num3 + this.dgvSelectedUsers.Rows.Count * (i - num3);
						Application.DoEvents();
					}
				}
				string format;
				if (sender.Equals(this.btnDeletePass))
				{
					format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
				}
				else
				{
					format = "UPDATE t_b_Controller SET f_lastDelAddAndUploadDateTime ={0}, f_lastDelAddAndUploadConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
				}
				for (int k = 0; k < this.dgvSelectedDoors.Rows.Count; k++)
				{
					string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, (int)((DataView)this.dgvSelectedDoors.DataSource)[k]["f_ControllerID"]);
					oleDbCommand.CommandText = text3;
					oleDbCommand.ExecuteNonQuery();
				}
				wgTools.WriteLine("btnDelete_Click End");
				this.progressBar1.Value = this.progressBar1.Maximum;
				Cursor.Current = Cursors.Default;
				if (sender.Equals(this.btnDeletePass))
				{
					this.logOperate(this.btnDeletePass);
					XMessageBox.Show(string.Concat(new string[]
					{
						(sender as Button).Text,
						" \r\n\r\n",
						CommonStr.strUsersNum,
						" = ",
						this.dgvSelectedUsers.RowCount.ToString(),
						"\r\n\r\n",
						CommonStr.strDoorsNum,
						" = ",
						this.dgvSelectedDoors.RowCount.ToString(),
						"\r\n\r\n",
						CommonStr.strSuccessfully
					}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.progressBar1.Value = 0;
				}
				else
				{
					this.logOperate(this.btnDeletePassAndUpload);
					ArrayList arrayList3 = new ArrayList();
					if (this.dgvSelectedUsers.Rows.Count > 0)
					{
						using (icPrivilege icPrivilege = new icPrivilege())
						{
							for (int l = 0; l < this.dgvSelectedDoors.Rows.Count; l++)
							{
								int num5 = (int)((DataView)this.dgvSelectedDoors.DataSource)[l]["f_ControllerID"];
								if (arrayList3.IndexOf(num5) < 0)
								{
									for (int m = 0; m < this.dgvSelectedUsers.Rows.Count; m++)
									{
										if (icPrivilege.DelPrivilegeOfOneCardByDB(num5, (int)((DataView)this.dgvSelectedUsers.DataSource)[m]["f_ConsumerID"]) < 0)
										{
											format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
											for (int n = 0; n < this.dgvSelectedDoors.Rows.Count; n++)
											{
												num5 = (int)((DataView)this.dgvSelectedDoors.DataSource)[n]["f_ControllerID"];
												if (arrayList3.IndexOf(num5) < 0)
												{
													string text3 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount, num5);
													oleDbCommand.CommandText = text3;
													oleDbCommand.ExecuteNonQuery();
												}
											}
											XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
											this.progressBar1.Value = 0;
											return;
										}
									}
									arrayList3.Add(num5);
								}
							}
						}
					}
					wgAppConfig.wgLog(string.Concat(new string[]
					{
						(sender as Button).Text.Replace("\r\n", ""),
						" ,",
						CommonStr.strUsersNum,
						" = ",
						this.dgvSelectedUsers.RowCount.ToString(),
						",",
						CommonStr.strDoorsNum,
						" = ",
						this.dgvSelectedDoors.RowCount.ToString(),
						",",
						CommonStr.strSuccessfully
					}), EventLogEntryType.Information, null);
					this.progressBar1.Value = this.progressBar1.Maximum;
					Cursor.Current = Cursors.Default;
					XMessageBox.Show((sender as Button).Text + " " + CommonStr.strSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.progressBar1.Value = 0;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				if (oleDbCommand != null)
				{
					oleDbCommand.Dispose();
				}
				if (oleDbConnection != null)
				{
					oleDbConnection.Dispose();
				}
				this.dfrmWait1.Hide();
			}
		}

		private void dgvUsers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnAddOneUser.PerformClick();
		}

		private void dgvSelectedUsers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnDelOneUser.PerformClick();
		}

		private void dgvDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnAddOneDoor.PerformClick();
		}

		private void dgvSelectedDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnDelOneDoor.PerformClick();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (this.bEdit)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.Close();
		}

		private void btnDeletePass_Click(object sender, EventArgs e)
		{
			this.btnDeletePassAndUpload_Click(sender, e);
			this.bEdit = true;
		}

		private void btnAddPass_Click(object sender, EventArgs e)
		{
			this.btnAddPassAndUpload_Click(sender, e);
			this.bEdit = true;
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
						}
						else
						{
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
					dataView.RowFilter = string.Format("(f_DoorEnabled > 0) AND {0} AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				}
				if (string.IsNullOrEmpty(this.strGroupFilter))
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
				}
				else
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  ({0}) AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				}
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
			}
		}

		private void cbof_Zone_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvDoors.DataSource != null)
			{
				DataView dataView = (DataView)this.dgvDoors.DataSource;
				if (this.cbof_ZoneID.SelectedIndex < 0 || (this.cbof_ZoneID.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
				{
					dataView.RowFilter = "f_Selected = 0";
					this.strZoneFilter = "";
					return;
				}
				dataView.RowFilter = "f_Selected = 0 AND f_ZoneID =" + this.arrZoneID[this.cbof_ZoneID.SelectedIndex];
				this.strZoneFilter = " f_ZoneID =" + this.arrZoneID[this.cbof_ZoneID.SelectedIndex];
				int num = (int)this.arrZoneID[this.cbof_ZoneID.SelectedIndex];
				int num2 = (int)this.arrZoneNO[this.cbof_ZoneID.SelectedIndex];
				int zoneChildMaxNo = icControllerZone.getZoneChildMaxNo(this.cbof_ZoneID.Text, this.arrZoneName, this.arrZoneNO);
				if (num2 > 0)
				{
					if (num2 >= zoneChildMaxNo)
					{
						dataView.RowFilter = string.Format("f_Selected = 0 AND f_ZoneID ={0:d} ", num);
						this.strZoneFilter = string.Format(" f_ZoneID ={0:d} ", num);
					}
					else
					{
						dataView.RowFilter = "f_Selected = 0 ";
						string text = "";
						for (int i = 0; i < this.arrZoneNO.Count; i++)
						{
							if ((int)this.arrZoneNO[i] <= zoneChildMaxNo && (int)this.arrZoneNO[i] >= num2)
							{
								if (text == "")
								{
									text += string.Format(" f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
								}
								else
								{
									text += string.Format(" OR f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
								}
							}
						}
						dataView.RowFilter = string.Format("f_Selected = 0 AND ( {0} )", text);
						this.strZoneFilter = string.Format("  {0} ", text);
					}
				}
				dataView.RowFilter = string.Format("f_Selected = 0 AND ( {0} )", this.strZoneFilter);
			}
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
			try
			{
				if (!this.bStarting)
				{
					if (this.progressBar1.Value != 0 && this.progressBar1.Value != this.progressBar1.Maximum)
					{
						Cursor.Current = Cursors.WaitCursor;
					}
				}
				else if (this.dgvUsers.DataSource == null)
				{
					Cursor.Current = Cursors.WaitCursor;
				}
				else
				{
					Cursor.Current = Cursors.Default;
					this.lblWait.Visible = false;
					this.groupBox1.Enabled = true;
					this.bStarting = false;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{
		}

		private void dfrmPrivilege_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
			try
			{
				if (this.dfrmWait1 != null)
				{
					this.dfrmWait1.Dispose();
				}
			}
			catch (Exception)
			{
			}
		}

		private void dgvDoors_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void dgvDoors_MouseClick(object sender, MouseEventArgs e)
		{
		}
	}
}
