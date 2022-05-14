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
	public class dfrmPrivilegeCopy : frmN3000
	{
		private IContainer components;

		private Label lblWait;

		private DataGridView dgvSelectedUsers;

		private DataGridView dgvUsers;

		private Button btnDelAllUsers;

		private Button btnDelOneUser;

		private Button btnAddOneUser;

		private Button button1;

		private Button btnAddAllUsers;

		private ComboBox cbof_GroupID;

		private Label label4;

		private DataGridView dgvSelectedUsers4Copy;

		private Button btnAddOneUser4Copy;

		private Button btnDeleteOneUser4Copy;

		private Label label1;

		private Label label2;

		private Button btnAddPass;

		private BackgroundWorker backgroundWorker1;

		private System.Windows.Forms.Timer timer1;

		private ToolTip toolTip1;

		private ProgressBar progressBar1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn f_SelectedGroup;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn UserID;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewTextBoxColumn f_GroupID;

		private DataGridViewCheckBoxColumn f_SelectedUsers;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private SqlConnection cn;

		private SqlCommand cm;

		private DataView dv1;

		private DataView dv2;

		private DataView dv;

		private DataView dvSelected;

		private DataTable dt;

		private dfrmFind dfrmFind1;

		private bool bStarting = true;

		private string strGroupFilter = "";

		private bool bEdit;

		private DataTable dt4copy;

		private dfrmWait dfrmWait1 = new dfrmWait();

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPrivilegeCopy));
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
			this.backgroundWorker1 = new BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.progressBar1 = new ProgressBar();
			this.btnAddPass = new Button();
			this.label2 = new Label();
			this.label1 = new Label();
			this.btnDeleteOneUser4Copy = new Button();
			this.btnAddOneUser4Copy = new Button();
			this.dgvSelectedUsers4Copy = new DataGridView();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
			this.lblWait = new Label();
			this.dgvSelectedUsers = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.f_SelectedGroup = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.dgvUsers = new DataGridView();
			this.btnDelAllUsers = new Button();
			this.btnDelOneUser = new Button();
			this.btnAddOneUser = new Button();
			this.button1 = new Button();
			this.btnAddAllUsers = new Button();
			this.cbof_GroupID = new ComboBox();
			this.label4 = new Label();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			((ISupportInitialize)this.dgvSelectedUsers4Copy).BeginInit();
			((ISupportInitialize)this.dgvSelectedUsers).BeginInit();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Name = "progressBar1";
			this.toolTip1.SetToolTip(this.progressBar1, componentResourceManager.GetString("progressBar1.ToolTip"));
			componentResourceManager.ApplyResources(this.btnAddPass, "btnAddPass");
			this.btnAddPass.BackColor = Color.Transparent;
			this.btnAddPass.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddPass.ForeColor = Color.White;
			this.btnAddPass.Image = Resources.Rec1Pass;
			this.btnAddPass.Name = "btnAddPass";
			this.toolTip1.SetToolTip(this.btnAddPass, componentResourceManager.GetString("btnAddPass.ToolTip"));
			this.btnAddPass.UseVisualStyleBackColor = false;
			this.btnAddPass.Click += new EventHandler(this.btnAddPass_Click);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			this.toolTip1.SetToolTip(this.label2, componentResourceManager.GetString("label2.ToolTip"));
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.toolTip1.SetToolTip(this.label1, componentResourceManager.GetString("label1.ToolTip"));
			componentResourceManager.ApplyResources(this.btnDeleteOneUser4Copy, "btnDeleteOneUser4Copy");
			this.btnDeleteOneUser4Copy.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeleteOneUser4Copy.ForeColor = Color.White;
			this.btnDeleteOneUser4Copy.Name = "btnDeleteOneUser4Copy";
			this.toolTip1.SetToolTip(this.btnDeleteOneUser4Copy, componentResourceManager.GetString("btnDeleteOneUser4Copy.ToolTip"));
			this.btnDeleteOneUser4Copy.UseVisualStyleBackColor = true;
			this.btnDeleteOneUser4Copy.Click += new EventHandler(this.btnDeleteOneUser4Copy_Click);
			componentResourceManager.ApplyResources(this.btnAddOneUser4Copy, "btnAddOneUser4Copy");
			this.btnAddOneUser4Copy.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneUser4Copy.ForeColor = Color.White;
			this.btnAddOneUser4Copy.Name = "btnAddOneUser4Copy";
			this.toolTip1.SetToolTip(this.btnAddOneUser4Copy, componentResourceManager.GetString("btnAddOneUser4Copy.ToolTip"));
			this.btnAddOneUser4Copy.UseVisualStyleBackColor = true;
			this.btnAddOneUser4Copy.Click += new EventHandler(this.btnAddOneUser4Copy_Click);
			componentResourceManager.ApplyResources(this.dgvSelectedUsers4Copy, "dgvSelectedUsers4Copy");
			this.dgvSelectedUsers4Copy.AllowUserToAddRows = false;
			this.dgvSelectedUsers4Copy.AllowUserToDeleteRows = false;
			this.dgvSelectedUsers4Copy.AllowUserToOrderColumns = true;
			this.dgvSelectedUsers4Copy.BackgroundColor = Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers4Copy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelectedUsers4Copy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelectedUsers4Copy.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn5,
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.dataGridViewTextBoxColumn8,
				this.dataGridViewTextBoxColumn9,
				this.dataGridViewCheckBoxColumn2
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedUsers4Copy.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelectedUsers4Copy.EnableHeadersVisualStyles = false;
			this.dgvSelectedUsers4Copy.Name = "dgvSelectedUsers4Copy";
			this.dgvSelectedUsers4Copy.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers4Copy.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvSelectedUsers4Copy.RowTemplate.Height = 23;
			this.dgvSelectedUsers4Copy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvSelectedUsers4Copy, componentResourceManager.GetString("dgvSelectedUsers4Copy.ToolTip"));
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn2, "dataGridViewCheckBoxColumn2");
			this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			this.dataGridViewCheckBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.lblWait, "lblWait");
			this.lblWait.BorderStyle = BorderStyle.FixedSingle;
			this.lblWait.ForeColor = Color.White;
			this.lblWait.Name = "lblWait";
			this.toolTip1.SetToolTip(this.lblWait, componentResourceManager.GetString("lblWait.ToolTip"));
			componentResourceManager.ApplyResources(this.dgvSelectedUsers, "dgvSelectedUsers");
			this.dgvSelectedUsers.AllowUserToAddRows = false;
			this.dgvSelectedUsers.AllowUserToDeleteRows = false;
			this.dgvSelectedUsers.AllowUserToOrderColumns = true;
			this.dgvSelectedUsers.BackgroundColor = Color.White;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
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
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Window;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = Color.White;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedUsers.DefaultCellStyle = dataGridViewCellStyle6;
			this.dgvSelectedUsers.EnableHeadersVisualStyles = false;
			this.dgvSelectedUsers.Name = "dgvSelectedUsers";
			this.dgvSelectedUsers.ReadOnly = true;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = SystemColors.Control;
			dataGridViewCellStyle7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgvSelectedUsers.RowTemplate.Height = 23;
			this.dgvSelectedUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvSelectedUsers, componentResourceManager.GetString("dgvSelectedUsers.ToolTip"));
			this.dgvSelectedUsers.MouseDoubleClick += new MouseEventHandler(this.dgvSelectedUsers_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
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
			dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle9.ForeColor = Color.White;
			dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ConsumerID,
				this.UserID,
				this.ConsumerName,
				this.CardNO,
				this.f_GroupID,
				this.f_SelectedUsers
			});
			dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = SystemColors.Window;
			dataGridViewCellStyle10.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle10.ForeColor = Color.White;
			dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
			this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle10;
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = SystemColors.Control;
			dataGridViewCellStyle11.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvUsers, componentResourceManager.GetString("dgvUsers.ToolTip"));
			this.dgvUsers.KeyDown += new KeyEventHandler(this.dgvUsers_KeyDown);
			this.dgvUsers.MouseDoubleClick += new MouseEventHandler(this.dgvUsers_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.btnDelAllUsers, "btnDelAllUsers");
			this.btnDelAllUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelAllUsers.ForeColor = Color.White;
			this.btnDelAllUsers.Name = "btnDelAllUsers";
			this.toolTip1.SetToolTip(this.btnDelAllUsers, componentResourceManager.GetString("btnDelAllUsers.ToolTip"));
			this.btnDelAllUsers.UseVisualStyleBackColor = true;
			this.btnDelAllUsers.Click += new EventHandler(this.btnDelAllUsers_Click);
			componentResourceManager.ApplyResources(this.btnDelOneUser, "btnDelOneUser");
			this.btnDelOneUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelOneUser.ForeColor = Color.White;
			this.btnDelOneUser.Name = "btnDelOneUser";
			this.toolTip1.SetToolTip(this.btnDelOneUser, componentResourceManager.GetString("btnDelOneUser.ToolTip"));
			this.btnDelOneUser.UseVisualStyleBackColor = true;
			this.btnDelOneUser.Click += new EventHandler(this.btnDelOneUser_Click);
			componentResourceManager.ApplyResources(this.btnAddOneUser, "btnAddOneUser");
			this.btnAddOneUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneUser.ForeColor = Color.White;
			this.btnAddOneUser.Name = "btnAddOneUser";
			this.toolTip1.SetToolTip(this.btnAddOneUser, componentResourceManager.GetString("btnAddOneUser.ToolTip"));
			this.btnAddOneUser.UseVisualStyleBackColor = true;
			this.btnAddOneUser.Click += new EventHandler(this.btnAddOneUser_Click);
			componentResourceManager.ApplyResources(this.button1, "button1");
			this.button1.BackColor = Color.Transparent;
			this.button1.BackgroundImage = Resources.pMain_button_normal;
			this.button1.ForeColor = Color.White;
			this.button1.Name = "button1";
			this.toolTip1.SetToolTip(this.button1, componentResourceManager.GetString("button1.ToolTip"));
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this.btnAddAllUsers, "btnAddAllUsers");
			this.btnAddAllUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllUsers.ForeColor = Color.White;
			this.btnAddAllUsers.Name = "btnAddAllUsers";
			this.toolTip1.SetToolTip(this.btnAddAllUsers, componentResourceManager.GetString("btnAddAllUsers.ToolTip"));
			this.btnAddAllUsers.UseVisualStyleBackColor = true;
			this.btnAddAllUsers.Click += new EventHandler(this.btnAddAllUsers_Click);
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			this.cbof_GroupID.Name = "cbof_GroupID";
			this.toolTip1.SetToolTip(this.cbof_GroupID, componentResourceManager.GetString("cbof_GroupID.ToolTip"));
			this.cbof_GroupID.SelectedIndexChanged += new EventHandler(this.cbof_GroupID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.BackColor = Color.Transparent;
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			this.toolTip1.SetToolTip(this.label4, componentResourceManager.GetString("label4.ToolTip"));
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.UserID.DefaultCellStyle = dataGridViewCellStyle12;
			componentResourceManager.ApplyResources(this.UserID, "UserID");
			this.UserID.Name = "UserID";
			this.UserID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ConsumerName, "ConsumerName");
			this.ConsumerName.Name = "ConsumerName";
			this.ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.CardNO, "CardNO");
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_GroupID, "f_GroupID");
			this.f_GroupID.Name = "f_GroupID";
			this.f_GroupID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedUsers, "f_SelectedUsers");
			this.f_SelectedUsers.Name = "f_SelectedUsers";
			this.f_SelectedUsers.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.btnAddPass);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnDeleteOneUser4Copy);
			base.Controls.Add(this.btnAddOneUser4Copy);
			base.Controls.Add(this.dgvSelectedUsers4Copy);
			base.Controls.Add(this.lblWait);
			base.Controls.Add(this.dgvSelectedUsers);
			base.Controls.Add(this.dgvUsers);
			base.Controls.Add(this.btnDelAllUsers);
			base.Controls.Add(this.btnDelOneUser);
			base.Controls.Add(this.btnAddOneUser);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.btnAddAllUsers);
			base.Controls.Add(this.cbof_GroupID);
			base.Controls.Add(this.label4);
			base.Name = "dfrmPrivilegeCopy";
			this.toolTip1.SetToolTip(this, componentResourceManager.GetString("$this.ToolTip"));
			base.FormClosing += new FormClosingEventHandler(this.dfrmPrivilegeCopy_FormClosing);
			base.Load += new EventHandler(this.dfrmPrivilegeCopy_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmPrivilegeCopy_KeyDown);
			((ISupportInitialize)this.dgvSelectedUsers4Copy).EndInit();
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			((ISupportInitialize)this.dgvUsers).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmPrivilegeCopy()
		{
			this.InitializeComponent();
		}

		private void dfrmPrivilegeCopy_Load(object sender, EventArgs e)
		{
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.dataGridViewTextBoxColumn6.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn6.HeaderText);
			this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
			this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			try
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
			this.dgvSelectedUsers4Copy.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			Cursor.Current = Cursors.WaitCursor;
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
					this.timer1.Enabled = false;
					Cursor.Current = Cursors.Default;
					this.lblWait.Visible = false;
					this.btnAddAllUsers.Enabled = true;
					this.btnAddOneUser.Enabled = true;
					this.btnAddOneUser4Copy.Enabled = true;
					this.btnAddPass.Enabled = true;
					this.btnDelAllUsers.Enabled = true;
					this.btnDeleteOneUser4Copy.Enabled = true;
					this.btnDelOneUser.Enabled = true;
					this.cbof_GroupID.Enabled = true;
					this.bStarting = false;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
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
			this.dv.RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
			this.dvSelected.RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
			this.dgvUsers.AutoGenerateColumns = false;
			this.dgvUsers.DataSource = this.dv;
			this.dgvSelectedUsers.AutoGenerateColumns = false;
			this.dgvSelectedUsers.DataSource = this.dvSelected;
			this.dt4copy = dtUser.Clone();
			this.dgvSelectedUsers4Copy.AutoGenerateColumns = false;
			this.dgvSelectedUsers4Copy.DataSource = new DataView(this.dt4copy);
			int num = 0;
			while (num < this.dv.Table.Columns.Count && num < this.dgvUsers.ColumnCount)
			{
				this.dgvUsers.Columns[num].DataPropertyName = dtUser.Columns[num].ColumnName;
				this.dgvSelectedUsers.Columns[num].DataPropertyName = dtUser.Columns[num].ColumnName;
				this.dgvSelectedUsers4Copy.Columns[num].DataPropertyName = this.dt4copy.Columns[num].ColumnName;
				num++;
			}
			this.cbof_GroupID_SelectedIndexChanged(null, null);
			wgTools.WriteLine("loadUserData End");
			Cursor.Current = Cursors.Default;
		}

		private void btnAddAllUsers_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			if (this.strGroupFilter == "")
			{
				icConsumerShare.selectAllUsers();
				((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
				((DataView)this.dgvSelectedUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
				int arg_7A_0 = this.dgvSelectedUsers.RowCount;
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
			int arg_219_0 = this.dv2.Count;
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
			int arg_21_0 = this.dgvSelectedUsers.RowCount;
		}

		private void btnDelOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedUsers, icConsumerShare.iSelectedCurrentNoneMax);
		}

		private void dgvUsers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.dgvSelectedUsers4Copy.Rows.Count == 0)
			{
				this.btnAddOneUser4Copy.PerformClick();
				return;
			}
			this.btnAddOneUser.PerformClick();
		}

		private void dgvSelectedUsers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnDelOneUser.PerformClick();
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

		private void dfrmPrivilegeCopy_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
			try
			{
				if (this.dfrmWait1 != null)
				{
					this.dfrmWait1.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
		{
			this.dfrmPrivilegeCopy_KeyDown(this.dgvUsers, e);
		}

		private void dfrmPrivilegeCopy_KeyDown(object sender, KeyEventArgs e)
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

		private void btnAddOneUser4Copy_Click(object sender, EventArgs e)
		{
			if (this.dt4copy.Rows.Count > 0)
			{
				return;
			}
			try
			{
				DataGridView dataGridView = this.dgvUsers;
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
				DataTable table = ((DataView)dataGridView.DataSource).Table;
				int num = (int)dataGridView.Rows[index].Cells[0].Value;
				DataRow dataRow = table.Rows.Find(num);
				if (dataRow != null)
				{
					DataRow dataRow2 = this.dt4copy.NewRow();
					for (int i = 0; i < table.Columns.Count; i++)
					{
						dataRow2[i] = dataRow[i];
					}
					table.Rows.Remove(dataRow);
					table.AcceptChanges();
					this.dt4copy.Rows.Add(dataRow2);
					this.dt4copy.AcceptChanges();
				}
				icConsumerShare.setUpdateLog();
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void btnDeleteOneUser4Copy_Click(object sender, EventArgs e)
		{
			if (this.dt4copy.Rows.Count == 0)
			{
				return;
			}
			try
			{
				DataGridView dataGridView = this.dgvUsers;
				DataTable table = ((DataView)dataGridView.DataSource).Table;
				DataRow dataRow = table.NewRow();
				if (dataRow != null)
				{
					DataRow dataRow2 = this.dt4copy.Rows[0];
					for (int i = 0; i < table.Columns.Count; i++)
					{
						dataRow[i] = dataRow2[i];
					}
					dataRow["f_Selected"] = icConsumerShare.iSelectedCurrentNoneMax;
					table.Rows.Add(dataRow);
					table.AcceptChanges();
					this.dt4copy.Rows.Remove(dataRow2);
					this.dt4copy.AcceptChanges();
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
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
			wgAppConfig.wgLog(string.Format("{0}: {1} => {2}", this.Text.Replace("\r\n", ""), ((DataView)this.dgvSelectedUsers4Copy.DataSource)[0]["f_ConsumerName"].ToString(), text), EventLogEntryType.Information, null);
		}

		private void btnAddPass_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnAddPass_Click_Acc(sender, e);
				return;
			}
			if (this.dgvSelectedUsers.RowCount <= 0 || this.dgvSelectedUsers4Copy.RowCount <= 0)
			{
				return;
			}
			if (XMessageBox.Show(this.Text + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				return;
			}
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.cm = new SqlCommand("", this.cn);
			bool flag = false;
			this.timer1.Enabled = true;
			if (this.dgvSelectedUsers.Rows.Count > 1000)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			try
			{
				DataView arg_C3_0 = (DataView)this.dgvSelectedUsers.DataSource;
				DataView dataView = (DataView)this.dgvSelectedUsers4Copy.DataSource;
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				try
				{
					int num = 2000;
					int i = 0;
					this.progressBar1.Maximum = 2 * this.dgvSelectedUsers.RowCount;
					string text2;
					while (i < this.dgvSelectedUsers.Rows.Count)
					{
						string text = "";
						while (i < this.dgvSelectedUsers.Rows.Count)
						{
							text = text + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerID"] + ",";
							i++;
							if (text.Length > num)
							{
								break;
							}
						}
						text += "0";
						text2 = "DELETE FROM  [t_d_Privilege] ";
						text2 += " WHERE [f_ConsumerID] IN (";
						text2 += text;
						text2 += " ) ";
						this.cm.CommandText = text2;
						this.cm.ExecuteNonQuery();
						ProgressBar arg_1D1_0 = this.progressBar1;
						int arg_1D1_1 = i;
						int arg_1D0_0 = this.dgvSelectedUsers.Rows.Count;
						arg_1D1_0.Value = arg_1D1_1;
						Application.DoEvents();
					}
					i = 0;
					while (i < this.dgvSelectedUsers.Rows.Count)
					{
						string text = "";
						while (i < this.dgvSelectedUsers.Rows.Count)
						{
							text = text + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerID"] + ",";
							i++;
							if (text.Length > num)
							{
								break;
							}
						}
						text += "0";
						text2 = "INSERT INTO [t_d_Privilege] ( [f_ConsumerID],[f_DoorID], [f_ControlSegID], [f_ControllerID], [f_DoorNO])";
						text2 += " SELECT t_b_Consumer.f_ConsumerID, t_d_Privilege.f_DoorID,t_d_Privilege.[f_ControlSegID] , t_d_Privilege.[f_ControllerID], t_d_Privilege.[f_DoorNO] ";
						text2 += " FROM t_d_Privilege, t_b_Consumer ";
						text2 += " WHERE [t_b_Consumer].[f_ConsumerID] IN (";
						text2 += text;
						text2 += " ) ";
						text2 = text2 + " AND (t_d_Privilege.f_ConsumerID)= " + dataView[0]["f_ConsumerID"];
						this.cm.CommandText = text2;
						this.cm.ExecuteNonQuery();
						this.progressBar1.Value = i + this.dgvSelectedUsers.Rows.Count;
						Application.DoEvents();
					}
					flag = true;
					string format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} ";
					text2 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount);
					this.cm.CommandText = text2;
					this.cm.ExecuteNonQuery();
					this.logOperate(null);
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
				XMessageBox.Show(ex2.Message);
			}
			finally
			{
				if (this.cm != null)
				{
					this.cm.Dispose();
				}
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				this.dfrmWait1.Hide();
			}
			this.progressBar1.Value = this.progressBar1.Maximum;
			Cursor.Current = Cursors.Default;
			if (flag)
			{
				wgAppConfig.wgLog(this.Text + CommonStr.strSuccessfully);
				XMessageBox.Show(this.Text + CommonStr.strSuccessfully);
				base.DialogResult = DialogResult.OK;
				this.bEdit = true;
				base.Close();
				return;
			}
			this.progressBar1.Value = 0;
			this.bEdit = true;
			wgAppConfig.wgLog(this.Text + CommonStr.strOperateFailed);
			XMessageBox.Show(this.Text + CommonStr.strOperateFailed);
		}

		private void btnAddPass_Click_Acc(object sender, EventArgs e)
		{
			OleDbConnection oleDbConnection = null;
			OleDbCommand oleDbCommand = null;
			if (this.dgvSelectedUsers.RowCount <= 0 || this.dgvSelectedUsers4Copy.RowCount <= 0)
			{
				return;
			}
			if (XMessageBox.Show(this.Text + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				return;
			}
			oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			oleDbCommand = new OleDbCommand("", oleDbConnection);
			bool flag = false;
			this.timer1.Enabled = true;
			if (this.dgvSelectedUsers.Rows.Count > 1000)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			try
			{
				DataView arg_A9_0 = (DataView)this.dgvSelectedUsers.DataSource;
				DataView dataView = (DataView)this.dgvSelectedUsers4Copy.DataSource;
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				try
				{
					int num = 2000;
					int i = 0;
					this.progressBar1.Maximum = 2 * this.dgvSelectedUsers.RowCount;
					string text2;
					while (i < this.dgvSelectedUsers.Rows.Count)
					{
						string text = "";
						while (i < this.dgvSelectedUsers.Rows.Count)
						{
							text = text + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerID"] + ",";
							i++;
							if (text.Length > num)
							{
								break;
							}
						}
						text += "0";
						text2 = "DELETE FROM  [t_d_Privilege] ";
						text2 += " WHERE [f_ConsumerID] IN (";
						text2 += text;
						text2 += " ) ";
						oleDbCommand.CommandText = text2;
						oleDbCommand.ExecuteNonQuery();
						ProgressBar arg_1AB_0 = this.progressBar1;
						int arg_1AB_1 = i;
						int arg_1AA_0 = this.dgvSelectedUsers.Rows.Count;
						arg_1AB_0.Value = arg_1AB_1;
						Application.DoEvents();
					}
					i = 0;
					while (i < this.dgvSelectedUsers.Rows.Count)
					{
						string text = "";
						while (i < this.dgvSelectedUsers.Rows.Count)
						{
							text = text + ((DataView)this.dgvSelectedUsers.DataSource)[i]["f_ConsumerID"] + ",";
							i++;
							if (text.Length > num)
							{
								break;
							}
						}
						text += "0";
						text2 = "INSERT INTO [t_d_Privilege] ( [f_ConsumerID],[f_DoorID], [f_ControlSegID], [f_ControllerID], [f_DoorNO])";
						text2 += " SELECT t_b_Consumer.f_ConsumerID, t_d_Privilege.f_DoorID,t_d_Privilege.[f_ControlSegID] , t_d_Privilege.[f_ControllerID], t_d_Privilege.[f_DoorNO] ";
						text2 += " FROM t_d_Privilege, t_b_Consumer ";
						text2 += " WHERE [t_b_Consumer].[f_ConsumerID] IN (";
						text2 += text;
						text2 += " ) ";
						text2 = text2 + " AND (t_d_Privilege.f_ConsumerID)= " + dataView[0]["f_ConsumerID"];
						oleDbCommand.CommandText = text2;
						oleDbCommand.ExecuteNonQuery();
						this.progressBar1.Value = i + this.dgvSelectedUsers.Rows.Count;
						Application.DoEvents();
					}
					flag = true;
					string format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} ";
					text2 = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), this.dgvSelectedUsers.RowCount);
					oleDbCommand.CommandText = text2;
					oleDbCommand.ExecuteNonQuery();
					this.logOperate(null);
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
				XMessageBox.Show(ex2.Message);
			}
			finally
			{
				if (oleDbCommand != null)
				{
					oleDbCommand.Dispose();
				}
				if (oleDbConnection.State != ConnectionState.Closed)
				{
					oleDbConnection.Close();
				}
				this.dfrmWait1.Hide();
			}
			this.progressBar1.Value = this.progressBar1.Maximum;
			Cursor.Current = Cursors.Default;
			if (flag)
			{
				wgAppConfig.wgLog(this.Text + CommonStr.strSuccessfully);
				XMessageBox.Show(this.Text + CommonStr.strSuccessfully);
				base.DialogResult = DialogResult.OK;
				this.bEdit = true;
				base.Close();
				return;
			}
			this.progressBar1.Value = 0;
			this.bEdit = true;
			wgAppConfig.wgLog(this.Text + CommonStr.strOperateFailed);
			XMessageBox.Show(this.Text + CommonStr.strOperateFailed);
		}
	}
}
