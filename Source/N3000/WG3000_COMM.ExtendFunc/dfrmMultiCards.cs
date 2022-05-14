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
	public class dfrmMultiCards : frmN3000
	{
		private const int MoreCardGroupMaxLen = 9;

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

		internal Button btnOK;

		internal Button btnCancel;

		internal NumericUpDown nudGroupToAdd;

		internal Label lblControlTimeSeg;

		internal GroupBox grpNeeded;

		internal NumericUpDown nudGrp3;

		internal Label Label10;

		internal NumericUpDown nudGrp8;

		internal Label Label9;

		internal NumericUpDown nudGrp6;

		internal Label Label8;

		internal NumericUpDown nudGrp5;

		internal Label label1;

		internal NumericUpDown nudGrp4;

		internal Label Label6;

		internal NumericUpDown nudGrp2;

		internal Label label2;

		internal NumericUpDown nudGrp1;

		internal Label label11;

		internal Label label12;

		internal NumericUpDown nudGrp7;

		internal Label label13;

		internal NumericUpDown nudTotal;

		internal Label label14;

		internal CheckBox chkActive;

		internal GroupBox grpOption;

		internal NumericUpDown nudGrpStartOfSingle;

		internal CheckBox chkReadByOrder;

		internal CheckBox chkSingleGroup;

		internal GroupBox grpOptInOut;

		internal CheckBox chkReaderIn;

		internal CheckBox chkReaderOut;

		private Label lblWait;

		private System.Windows.Forms.Timer timer1;

		private BackgroundWorker backgroundWorker1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn f_MoreCards_GrpID;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn f_SelectedGroup;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn f_MoreCards_GrpID_1;

		private DataGridViewTextBoxColumn UserID;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewCheckBoxColumn f_SelectedUsers;

		private DataGridViewTextBoxColumn f_GroupID;

		public int DoorID;

		public string retValue = "0";

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private DataTable dt;

		private DataTable dtUser1;

		private DataView dv;

		private DataView dvSelected;

		private DataView dv1;

		private DataView dv2;

		private int controllerID;

		private int controllerSN;

		private int moreCards_GoInOut;

		private int doorNo;

		private string strGroupFilter = "";

		private SqlConnection cn;

		private SqlCommand cmd;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMultiCards));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.grpOptInOut = new GroupBox();
			this.chkReaderIn = new CheckBox();
			this.chkReaderOut = new CheckBox();
			this.grpOption = new GroupBox();
			this.nudGrpStartOfSingle = new NumericUpDown();
			this.chkReadByOrder = new CheckBox();
			this.chkSingleGroup = new CheckBox();
			this.chkActive = new CheckBox();
			this.grpNeeded = new GroupBox();
			this.nudGrp3 = new NumericUpDown();
			this.Label10 = new Label();
			this.nudGrp8 = new NumericUpDown();
			this.Label9 = new Label();
			this.nudGrp6 = new NumericUpDown();
			this.Label8 = new Label();
			this.nudGrp5 = new NumericUpDown();
			this.label1 = new Label();
			this.nudGrp4 = new NumericUpDown();
			this.Label6 = new Label();
			this.nudGrp2 = new NumericUpDown();
			this.label2 = new Label();
			this.nudGrp1 = new NumericUpDown();
			this.label11 = new Label();
			this.label12 = new Label();
			this.nudGrp7 = new NumericUpDown();
			this.label13 = new Label();
			this.nudTotal = new NumericUpDown();
			this.label14 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.groupBox1 = new GroupBox();
			this.lblWait = new Label();
			this.nudGroupToAdd = new NumericUpDown();
			this.lblControlTimeSeg = new Label();
			this.label3 = new Label();
			this.dgvSelectedUsers = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.f_MoreCards_GrpID = new DataGridViewTextBoxColumn();
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
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.backgroundWorker1 = new BackgroundWorker();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.f_MoreCards_GrpID_1 = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.grpOptInOut.SuspendLayout();
			this.grpOption.SuspendLayout();
			((ISupportInitialize)this.nudGrpStartOfSingle).BeginInit();
			this.grpNeeded.SuspendLayout();
			((ISupportInitialize)this.nudGrp3).BeginInit();
			((ISupportInitialize)this.nudGrp8).BeginInit();
			((ISupportInitialize)this.nudGrp6).BeginInit();
			((ISupportInitialize)this.nudGrp5).BeginInit();
			((ISupportInitialize)this.nudGrp4).BeginInit();
			((ISupportInitialize)this.nudGrp2).BeginInit();
			((ISupportInitialize)this.nudGrp1).BeginInit();
			((ISupportInitialize)this.nudGrp7).BeginInit();
			((ISupportInitialize)this.nudTotal).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.nudGroupToAdd).BeginInit();
			((ISupportInitialize)this.dgvSelectedUsers).BeginInit();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.grpOptInOut, "grpOptInOut");
			this.grpOptInOut.BackColor = Color.Transparent;
			this.grpOptInOut.Controls.Add(this.chkReaderIn);
			this.grpOptInOut.Controls.Add(this.chkReaderOut);
			this.grpOptInOut.ForeColor = Color.White;
			this.grpOptInOut.Name = "grpOptInOut";
			this.grpOptInOut.TabStop = false;
			componentResourceManager.ApplyResources(this.chkReaderIn, "chkReaderIn");
			this.chkReaderIn.Checked = true;
			this.chkReaderIn.CheckState = CheckState.Checked;
			this.chkReaderIn.Name = "chkReaderIn";
			componentResourceManager.ApplyResources(this.chkReaderOut, "chkReaderOut");
			this.chkReaderOut.Name = "chkReaderOut";
			componentResourceManager.ApplyResources(this.grpOption, "grpOption");
			this.grpOption.BackColor = Color.Transparent;
			this.grpOption.Controls.Add(this.nudGrpStartOfSingle);
			this.grpOption.Controls.Add(this.chkReadByOrder);
			this.grpOption.Controls.Add(this.chkSingleGroup);
			this.grpOption.ForeColor = Color.White;
			this.grpOption.Name = "grpOption";
			this.grpOption.TabStop = false;
			componentResourceManager.ApplyResources(this.nudGrpStartOfSingle, "nudGrpStartOfSingle");
			this.nudGrpStartOfSingle.BackColor = Color.White;
			NumericUpDown arg_52F_0 = this.nudGrpStartOfSingle;
			int[] array = new int[4];
			array[0] = 8;
			arg_52F_0.Maximum = new decimal(array);
			NumericUpDown arg_54E_0 = this.nudGrpStartOfSingle;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_54E_0.Minimum = new decimal(array2);
			this.nudGrpStartOfSingle.Name = "nudGrpStartOfSingle";
			this.nudGrpStartOfSingle.ReadOnly = true;
			NumericUpDown arg_589_0 = this.nudGrpStartOfSingle;
			int[] array3 = new int[4];
			array3[0] = 8;
			arg_589_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this.chkReadByOrder, "chkReadByOrder");
			this.chkReadByOrder.Name = "chkReadByOrder";
			componentResourceManager.ApplyResources(this.chkSingleGroup, "chkSingleGroup");
			this.chkSingleGroup.Name = "chkSingleGroup";
			this.chkSingleGroup.CheckedChanged += new EventHandler(this.chkSingleGroup_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkActive, "chkActive");
			this.chkActive.BackColor = Color.Transparent;
			this.chkActive.ForeColor = Color.White;
			this.chkActive.Name = "chkActive";
			this.chkActive.UseVisualStyleBackColor = false;
			this.chkActive.CheckedChanged += new EventHandler(this.chkActive_CheckedChanged);
			componentResourceManager.ApplyResources(this.grpNeeded, "grpNeeded");
			this.grpNeeded.BackColor = Color.Transparent;
			this.grpNeeded.Controls.Add(this.nudGrp3);
			this.grpNeeded.Controls.Add(this.Label10);
			this.grpNeeded.Controls.Add(this.nudGrp8);
			this.grpNeeded.Controls.Add(this.Label9);
			this.grpNeeded.Controls.Add(this.nudGrp6);
			this.grpNeeded.Controls.Add(this.Label8);
			this.grpNeeded.Controls.Add(this.nudGrp5);
			this.grpNeeded.Controls.Add(this.label1);
			this.grpNeeded.Controls.Add(this.nudGrp4);
			this.grpNeeded.Controls.Add(this.Label6);
			this.grpNeeded.Controls.Add(this.nudGrp2);
			this.grpNeeded.Controls.Add(this.label2);
			this.grpNeeded.Controls.Add(this.nudGrp1);
			this.grpNeeded.Controls.Add(this.label11);
			this.grpNeeded.Controls.Add(this.label12);
			this.grpNeeded.Controls.Add(this.nudGrp7);
			this.grpNeeded.Controls.Add(this.label13);
			this.grpNeeded.Controls.Add(this.nudTotal);
			this.grpNeeded.Controls.Add(this.label14);
			this.grpNeeded.ForeColor = Color.White;
			this.grpNeeded.Name = "grpNeeded";
			this.grpNeeded.TabStop = false;
			componentResourceManager.ApplyResources(this.nudGrp3, "nudGrp3");
			this.nudGrp3.BackColor = Color.White;
			this.nudGrp3.Name = "nudGrp3";
			this.nudGrp3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Label10, "Label10");
			this.Label10.Name = "Label10";
			componentResourceManager.ApplyResources(this.nudGrp8, "nudGrp8");
			this.nudGrp8.BackColor = Color.White;
			this.nudGrp8.Name = "nudGrp8";
			this.nudGrp8.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Label9, "Label9");
			this.Label9.Name = "Label9";
			componentResourceManager.ApplyResources(this.nudGrp6, "nudGrp6");
			this.nudGrp6.BackColor = Color.White;
			this.nudGrp6.Name = "nudGrp6";
			this.nudGrp6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Label8, "Label8");
			this.Label8.Name = "Label8";
			componentResourceManager.ApplyResources(this.nudGrp5, "nudGrp5");
			this.nudGrp5.BackColor = Color.White;
			this.nudGrp5.Name = "nudGrp5";
			this.nudGrp5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.nudGrp4, "nudGrp4");
			this.nudGrp4.BackColor = Color.White;
			this.nudGrp4.Name = "nudGrp4";
			this.nudGrp4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.nudGrp2, "nudGrp2");
			this.nudGrp2.BackColor = Color.White;
			this.nudGrp2.Name = "nudGrp2";
			this.nudGrp2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.nudGrp1, "nudGrp1");
			this.nudGrp1.BackColor = Color.White;
			this.nudGrp1.Name = "nudGrp1";
			this.nudGrp1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			componentResourceManager.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			componentResourceManager.ApplyResources(this.nudGrp7, "nudGrp7");
			this.nudGrp7.BackColor = Color.White;
			this.nudGrp7.Name = "nudGrp7";
			this.nudGrp7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			componentResourceManager.ApplyResources(this.nudTotal, "nudTotal");
			this.nudTotal.BackColor = Color.White;
			NumericUpDown arg_B86_0 = this.nudTotal;
			int[] array4 = new int[4];
			array4[0] = 2;
			arg_B86_0.Minimum = new decimal(array4);
			this.nudTotal.Name = "nudTotal";
			this.nudTotal.ReadOnly = true;
			NumericUpDown arg_BC1_0 = this.nudTotal;
			int[] array5 = new int[4];
			array5[0] = 2;
			arg_BC1_0.Value = new decimal(array5);
			componentResourceManager.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
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
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.lblWait);
			this.groupBox1.Controls.Add(this.nudGroupToAdd);
			this.groupBox1.Controls.Add(this.lblControlTimeSeg);
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
			componentResourceManager.ApplyResources(this.nudGroupToAdd, "nudGroupToAdd");
			this.nudGroupToAdd.BackColor = Color.White;
			NumericUpDown arg_E8D_0 = this.nudGroupToAdd;
			int[] array6 = new int[4];
			array6[0] = 9;
			arg_E8D_0.Maximum = new decimal(array6);
			NumericUpDown arg_EAC_0 = this.nudGroupToAdd;
			int[] array7 = new int[4];
			array7[0] = 1;
			arg_EAC_0.Minimum = new decimal(array7);
			this.nudGroupToAdd.Name = "nudGroupToAdd";
			this.nudGroupToAdd.ReadOnly = true;
			NumericUpDown arg_EE7_0 = this.nudGroupToAdd;
			int[] array8 = new int[4];
			array8[0] = 1;
			arg_EE7_0.Value = new decimal(array8);
			componentResourceManager.ApplyResources(this.lblControlTimeSeg, "lblControlTimeSeg");
			this.lblControlTimeSeg.Name = "lblControlTimeSeg";
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
				this.f_MoreCards_GrpID,
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
			componentResourceManager.ApplyResources(this.f_MoreCards_GrpID, "f_MoreCards_GrpID");
			this.f_MoreCards_GrpID.Name = "f_MoreCards_GrpID";
			this.f_MoreCards_GrpID.ReadOnly = true;
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
				this.f_MoreCards_GrpID_1,
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
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_MoreCards_GrpID_1, "f_MoreCards_GrpID_1");
			this.f_MoreCards_GrpID_1.Name = "f_MoreCards_GrpID_1";
			this.f_MoreCards_GrpID_1.ReadOnly = true;
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
			base.Controls.Add(this.grpOptInOut);
			base.Controls.Add(this.grpOption);
			base.Controls.Add(this.chkActive);
			base.Controls.Add(this.grpNeeded);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.groupBox1);
			base.Name = "dfrmMultiCards";
			base.Load += new EventHandler(this.dfrmMultiCards_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmMultiCards_KeyDown);
			this.grpOptInOut.ResumeLayout(false);
			this.grpOption.ResumeLayout(false);
			((ISupportInitialize)this.nudGrpStartOfSingle).EndInit();
			this.grpNeeded.ResumeLayout(false);
			((ISupportInitialize)this.nudGrp3).EndInit();
			((ISupportInitialize)this.nudGrp8).EndInit();
			((ISupportInitialize)this.nudGrp6).EndInit();
			((ISupportInitialize)this.nudGrp5).EndInit();
			((ISupportInitialize)this.nudGrp4).EndInit();
			((ISupportInitialize)this.nudGrp2).EndInit();
			((ISupportInitialize)this.nudGrp1).EndInit();
			((ISupportInitialize)this.nudGrp7).EndInit();
			((ISupportInitialize)this.nudTotal).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.nudGroupToAdd).EndInit();
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			((ISupportInitialize)this.dgvUsers).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmMultiCards()
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

		private void dfrmMultiCards_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmMultiCards_Load_Acc(sender, e);
				return;
			}
			this.loadGroupData();
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			string text = "SELECT t_b_Door.*,t_b_Controller.f_ControllerSN, t_b_Controller.f_MoreCards_GoInOut  FROM  t_b_Controller,t_b_Door  ";
			text = text + " Where  t_b_Controller.f_ControllerID=t_b_Door.f_ControllerID and t_b_door.f_DoorID = " + this.DoorID;
			if (this.cn.State == ConnectionState.Closed)
			{
				this.cn.Open();
			}
			this.cmd = new SqlCommand(text, this.cn);
			SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
			if (sqlDataReader.Read())
			{
				this.controllerID = (int)sqlDataReader["f_ControllerID"];
				this.controllerSN = (int)sqlDataReader["f_ControllerSN"];
				this.moreCards_GoInOut = (int)sqlDataReader["f_MoreCards_GoInOut"];
				this.doorNo = int.Parse(sqlDataReader["f_DoorNo"].ToString());
				if (wgMjController.GetControllerType(this.controllerSN) == 1 || wgMjController.GetControllerType(this.controllerSN) == 2)
				{
					this.grpOptInOut.Visible = true;
					int num = this.moreCards_GoInOut >> (this.doorNo - 1) * 2 & 3;
					this.chkReaderIn.Checked = ((num & 1) > 0);
					this.chkReaderOut.Checked = ((num & 2) > 0);
				}
				else if (wgMjController.GetControllerType(this.controllerSN) == 4)
				{
					this.grpOptInOut.Visible = false;
					this.chkReaderIn.Checked = true;
					this.chkReaderIn.Enabled = false;
					this.chkReaderOut.Visible = false;
					this.grpOptInOut.Enabled = false;
					this.grpOptInOut.Visible = false;
				}
				else
				{
					this.grpOptInOut.Visible = false;
					this.chkReaderIn.Checked = true;
					this.grpOptInOut.Enabled = false;
					this.grpOptInOut.Visible = false;
				}
				if ((int)sqlDataReader["f_MoreCards_Total"] > 0)
				{
					this.chkActive.Checked = true;
					this.chkActive_CheckedChanged(null, null);
				}
				else
				{
					this.chkActive.Checked = false;
					this.chkActive_CheckedChanged(null, null);
				}
				if ((int)sqlDataReader["f_MoreCards_Total"] > 1)
				{
					this.nudTotal.Value = (int)sqlDataReader["f_MoreCards_Total"];
				}
				this.nudGrp1.Value = (int)sqlDataReader["f_MoreCards_Grp1"];
				this.nudGrp2.Value = (int)sqlDataReader["f_MoreCards_Grp2"];
				this.nudGrp3.Value = (int)sqlDataReader["f_MoreCards_Grp3"];
				this.nudGrp4.Value = (int)sqlDataReader["f_MoreCards_Grp4"];
				this.nudGrp5.Value = (int)sqlDataReader["f_MoreCards_Grp5"];
				this.nudGrp6.Value = (int)sqlDataReader["f_MoreCards_Grp6"];
				this.nudGrp7.Value = (int)sqlDataReader["f_MoreCards_Grp7"];
				this.nudGrp8.Value = (int)sqlDataReader["f_MoreCards_Grp8"];
				int num2 = (int)sqlDataReader["f_MoreCards_Option"];
				if ((num2 & 16) > 0)
				{
					this.chkReadByOrder.Checked = true;
				}
				if ((num2 & 8) > 0)
				{
					this.chkSingleGroup.Checked = true;
					this.nudGrpStartOfSingle.Value = 1 + (num2 & 7);
					this.nudGrpStartOfSingle.Visible = true;
				}
				else
				{
					this.chkSingleGroup.Checked = false;
					this.nudGrpStartOfSingle.Visible = false;
				}
				if (this.chkReadByOrder.Checked || this.chkSingleGroup.Checked)
				{
					this.grpOption.Visible = true;
				}
			}
			sqlDataReader.Close();
			this.cn.Close();
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			this.dataGridViewTextBoxColumn2.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn2.HeaderText);
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void dfrmMultiCards_Load_Acc(object sender, EventArgs e)
		{
			this.loadGroupData();
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			string text = "SELECT t_b_Door.*,t_b_Controller.f_ControllerSN, t_b_Controller.f_MoreCards_GoInOut  FROM  t_b_Controller,t_b_Door  ";
			text = text + " Where  t_b_Controller.f_ControllerID=t_b_Door.f_ControllerID and t_b_door.f_DoorID = " + this.DoorID;
			if (oleDbConnection.State == ConnectionState.Closed)
			{
				oleDbConnection.Open();
			}
			OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
			OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
			if (oleDbDataReader.Read())
			{
				this.controllerID = (int)oleDbDataReader["f_ControllerID"];
				this.controllerSN = (int)oleDbDataReader["f_ControllerSN"];
				this.moreCards_GoInOut = (int)oleDbDataReader["f_MoreCards_GoInOut"];
				this.doorNo = int.Parse(oleDbDataReader["f_DoorNo"].ToString());
				if (wgMjController.GetControllerType(this.controllerSN) == 1 || wgMjController.GetControllerType(this.controllerSN) == 2)
				{
					this.grpOptInOut.Visible = true;
					int num = this.moreCards_GoInOut >> (this.doorNo - 1) * 2 & 3;
					this.chkReaderIn.Checked = ((num & 1) > 0);
					this.chkReaderOut.Checked = ((num & 2) > 0);
				}
				else if (wgMjController.GetControllerType(this.controllerSN) == 4)
				{
					this.grpOptInOut.Visible = false;
					this.chkReaderIn.Checked = true;
					this.chkReaderIn.Enabled = false;
					this.chkReaderOut.Visible = false;
					this.grpOptInOut.Enabled = false;
					this.grpOptInOut.Visible = false;
				}
				else
				{
					this.grpOptInOut.Visible = false;
					this.chkReaderIn.Checked = true;
					this.grpOptInOut.Enabled = false;
					this.grpOptInOut.Visible = false;
				}
				if ((int)oleDbDataReader["f_MoreCards_Total"] > 0)
				{
					this.chkActive.Checked = true;
					this.chkActive_CheckedChanged(null, null);
				}
				else
				{
					this.chkActive.Checked = false;
					this.chkActive_CheckedChanged(null, null);
				}
				if ((int)oleDbDataReader["f_MoreCards_Total"] > 1)
				{
					this.nudTotal.Value = (int)oleDbDataReader["f_MoreCards_Total"];
				}
				this.nudGrp1.Value = (int)oleDbDataReader["f_MoreCards_Grp1"];
				this.nudGrp2.Value = (int)oleDbDataReader["f_MoreCards_Grp2"];
				this.nudGrp3.Value = (int)oleDbDataReader["f_MoreCards_Grp3"];
				this.nudGrp4.Value = (int)oleDbDataReader["f_MoreCards_Grp4"];
				this.nudGrp5.Value = (int)oleDbDataReader["f_MoreCards_Grp5"];
				this.nudGrp6.Value = (int)oleDbDataReader["f_MoreCards_Grp6"];
				this.nudGrp7.Value = (int)oleDbDataReader["f_MoreCards_Grp7"];
				this.nudGrp8.Value = (int)oleDbDataReader["f_MoreCards_Grp8"];
				int num2 = (int)oleDbDataReader["f_MoreCards_Option"];
				if ((num2 & 16) > 0)
				{
					this.chkReadByOrder.Checked = true;
				}
				if ((num2 & 8) > 0)
				{
					this.chkSingleGroup.Checked = true;
					this.nudGrpStartOfSingle.Value = 1 + (num2 & 7);
					this.nudGrpStartOfSingle.Visible = true;
				}
				else
				{
					this.chkSingleGroup.Checked = false;
					this.nudGrpStartOfSingle.Visible = false;
				}
				if (this.chkReadByOrder.Checked || this.chkSingleGroup.Checked)
				{
					this.grpOption.Visible = true;
				}
			}
			oleDbDataReader.Close();
			oleDbConnection.Close();
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
			DataTable table = ((DataView)this.dgvUsers.DataSource).Table;
			DataView dataSource = (DataView)this.dgvUsers.DataSource;
			DataView dataSource2 = (DataView)this.dgvSelectedUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvSelectedUsers.DataSource = null;
			if (this.strGroupFilter == "")
			{
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if ((int)table.Rows[i]["f_Selected"] != 1)
					{
						table.Rows[i]["f_Selected"] = 1;
						table.Rows[i]["f_MoreCards_GrpID"] = this.nudGroupToAdd.Value;
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
						this.dv[j]["f_MoreCards_GrpID"] = this.nudGroupToAdd.Value;
					}
				}
			}
			this.dgvUsers.DataSource = dataSource;
			this.dgvSelectedUsers.DataSource = dataSource2;
			wgTools.WriteLine("btnAddAllUsers_Click End");
			Cursor.Current = Cursors.Default;
		}

		private void btnDelAllUsers_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("btnDelAllUsers_Click Start");
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
			wgTools.WriteLine("btnDelAllUsers_Click End");
			Cursor.Current = Cursors.Default;
		}

		private void btnAddOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvUsers, "f_MoreCards_GrpID", this.nudGroupToAdd.Value.ToString());
		}

		private void btnDelOneUser_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedUsers);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			if (this.cn.State == ConnectionState.Closed)
			{
				this.cn.Open();
			}
			if (wgMjController.GetControllerType(this.controllerSN) == 1 || wgMjController.GetControllerType(this.controllerSN) == 2)
			{
				if (this.chkReaderIn.Checked && this.chkActive.Checked)
				{
					this.moreCards_GoInOut |= 1 << (this.doorNo - 1) * 2;
				}
				else
				{
					this.moreCards_GoInOut &= 255 - (1 << (this.doorNo - 1) * 2);
				}
				if (this.chkReaderOut.Checked && this.chkActive.Checked)
				{
					this.moreCards_GoInOut |= 1 << (this.doorNo - 1) * 2 + 1;
				}
				else
				{
					this.moreCards_GoInOut &= 255 - (1 << (this.doorNo - 1) * 2 + 1);
				}
			}
			else if (wgMjController.GetControllerType(this.controllerSN) == 4)
			{
				if (this.chkActive.Checked)
				{
					this.moreCards_GoInOut |= 1 << this.doorNo - 1;
				}
				else
				{
					this.moreCards_GoInOut &= 255 - (1 << this.doorNo - 1);
				}
			}
			else if (this.chkActive.Checked)
			{
				this.moreCards_GoInOut |= 1 << this.doorNo - 1;
			}
			else
			{
				this.moreCards_GoInOut &= 255 - (1 << this.doorNo - 1);
			}
			string text = string.Concat(new object[]
			{
				"update t_b_Controller set f_MoreCards_GoInOut =",
				this.moreCards_GoInOut,
				" Where f_ControllerID = ",
				this.controllerID
			});
			this.cmd = new SqlCommand(text, this.cn);
			this.cmd.ExecuteNonQuery();
			int num = 0;
			if (this.chkReadByOrder.Checked)
			{
				num += 16;
			}
			if (this.chkSingleGroup.Checked)
			{
				num += 8;
				num += (int)(--this.nudGrpStartOfSingle.Value);
			}
			if (this.chkActive.Checked)
			{
				text = "update t_b_door set f_MoreCards_Total =" + this.nudTotal.Value;
				text = text + ", f_MoreCards_Grp1=" + this.nudGrp1.Value;
				text = text + ", f_MoreCards_Grp2=" + this.nudGrp2.Value;
				text = text + ", f_MoreCards_Grp3=" + this.nudGrp3.Value;
				text = text + ", f_MoreCards_Grp4=" + this.nudGrp4.Value;
				text = text + ", f_MoreCards_Grp5=" + this.nudGrp5.Value;
				text = text + ", f_MoreCards_Grp6=" + this.nudGrp6.Value;
				text = text + ", f_MoreCards_Grp7=" + this.nudGrp7.Value;
				text = text + ", f_MoreCards_Grp8=" + this.nudGrp8.Value;
				text = text + ", f_MoreCards_Option=" + num;
				text = text + " Where f_DoorID = " + this.DoorID;
			}
			else
			{
				text = "update t_b_door set f_MoreCards_Total =" + 0;
				text = text + ", f_MoreCards_Grp1=" + 0;
				text = text + ", f_MoreCards_Grp2=" + 0;
				text = text + ", f_MoreCards_Grp3=" + 0;
				text = text + ", f_MoreCards_Grp4=" + 0;
				text = text + ", f_MoreCards_Grp5=" + 0;
				text = text + ", f_MoreCards_Grp6=" + 0;
				text = text + ", f_MoreCards_Grp7=" + 0;
				text = text + ", f_MoreCards_Grp8=" + 0;
				text = text + ", f_MoreCards_Option=" + 0;
				text = text + " Where f_DoorID = " + this.DoorID;
			}
			this.cmd = new SqlCommand(text, this.cn);
			this.cmd.ExecuteNonQuery();
			text = " Delete  FROM t_d_doorMoreCardsUsers  WHERE f_DoorID= " + this.DoorID;
			this.cmd = new SqlCommand(text, this.cn);
			this.cmd.ExecuteNonQuery();
			this.dvSelected = (this.dgvSelectedUsers.DataSource as DataView);
			if (this.chkActive.Checked && this.nudTotal.Value > 0m && this.dvSelected != null)
			{
				for (int i = 1; i <= 9; i++)
				{
					this.dvSelected.RowFilter = "f_Selected > 0 AND f_MoreCards_GrpID = " + i;
					if (this.dvSelected.Count > 0)
					{
						for (int j = 0; j <= this.dvSelected.Count - 1; j++)
						{
							text = "INSERT INTO [t_d_doorMoreCardsUsers] (f_ConsumerID, f_DoorID ,f_MoreCards_GrpID)";
							text += " VALUES( ";
							text += this.dvSelected[j]["f_ConsumerID"].ToString();
							string text2 = text;
							text = string.Concat(new string[]
							{
								text2,
								", ",
								this.DoorID.ToString(),
								",",
								i.ToString(),
								")"
							});
							this.cmd.CommandText = text;
							this.cmd.ExecuteNonQuery();
						}
					}
				}
			}
			if (this.chkActive.Checked)
			{
				this.retValue = this.nudTotal.Value.ToString();
			}
			else
			{
				this.retValue = "0";
			}
			if (this.cn.State == ConnectionState.Open)
			{
				this.cn.Close();
			}
			base.DialogResult = DialogResult.OK;
			this.Cursor = current;
			base.Close();
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			if (oleDbConnection.State == ConnectionState.Closed)
			{
				oleDbConnection.Open();
			}
			if (wgMjController.GetControllerType(this.controllerSN) == 1 || wgMjController.GetControllerType(this.controllerSN) == 2)
			{
				if (this.chkReaderIn.Checked && this.chkActive.Checked)
				{
					this.moreCards_GoInOut |= 1 << (this.doorNo - 1) * 2;
				}
				else
				{
					this.moreCards_GoInOut &= 255 - (1 << (this.doorNo - 1) * 2);
				}
				if (this.chkReaderOut.Checked && this.chkActive.Checked)
				{
					this.moreCards_GoInOut |= 1 << (this.doorNo - 1) * 2 + 1;
				}
				else
				{
					this.moreCards_GoInOut &= 255 - (1 << (this.doorNo - 1) * 2 + 1);
				}
			}
			else if (wgMjController.GetControllerType(this.controllerSN) == 4)
			{
				if (this.chkActive.Checked)
				{
					this.moreCards_GoInOut |= 1 << this.doorNo - 1;
				}
				else
				{
					this.moreCards_GoInOut &= 255 - (1 << this.doorNo - 1);
				}
			}
			else if (this.chkActive.Checked)
			{
				this.moreCards_GoInOut |= 1 << this.doorNo - 1;
			}
			else
			{
				this.moreCards_GoInOut &= 255 - (1 << this.doorNo - 1);
			}
			string text = string.Concat(new object[]
			{
				"update t_b_Controller set f_MoreCards_GoInOut =",
				this.moreCards_GoInOut,
				" Where f_ControllerID = ",
				this.controllerID
			});
			OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
			oleDbCommand.ExecuteNonQuery();
			int num = 0;
			if (this.chkReadByOrder.Checked)
			{
				num += 16;
			}
			if (this.chkSingleGroup.Checked)
			{
				num += 8;
				num += (int)(--this.nudGrpStartOfSingle.Value);
			}
			if (this.chkActive.Checked)
			{
				text = "update t_b_door set f_MoreCards_Total =" + this.nudTotal.Value;
				text = text + ", f_MoreCards_Grp1=" + this.nudGrp1.Value;
				text = text + ", f_MoreCards_Grp2=" + this.nudGrp2.Value;
				text = text + ", f_MoreCards_Grp3=" + this.nudGrp3.Value;
				text = text + ", f_MoreCards_Grp4=" + this.nudGrp4.Value;
				text = text + ", f_MoreCards_Grp5=" + this.nudGrp5.Value;
				text = text + ", f_MoreCards_Grp6=" + this.nudGrp6.Value;
				text = text + ", f_MoreCards_Grp7=" + this.nudGrp7.Value;
				text = text + ", f_MoreCards_Grp8=" + this.nudGrp8.Value;
				text = text + ", f_MoreCards_Option=" + num;
				text = text + " Where f_DoorID = " + this.DoorID;
			}
			else
			{
				text = "update t_b_door set f_MoreCards_Total =" + 0;
				text = text + ", f_MoreCards_Grp1=" + 0;
				text = text + ", f_MoreCards_Grp2=" + 0;
				text = text + ", f_MoreCards_Grp3=" + 0;
				text = text + ", f_MoreCards_Grp4=" + 0;
				text = text + ", f_MoreCards_Grp5=" + 0;
				text = text + ", f_MoreCards_Grp6=" + 0;
				text = text + ", f_MoreCards_Grp7=" + 0;
				text = text + ", f_MoreCards_Grp8=" + 0;
				text = text + ", f_MoreCards_Option=" + 0;
				text = text + " Where f_DoorID = " + this.DoorID;
			}
			oleDbCommand = new OleDbCommand(text, oleDbConnection);
			oleDbCommand.ExecuteNonQuery();
			text = " Delete  FROM t_d_doorMoreCardsUsers  WHERE f_DoorID= " + this.DoorID;
			oleDbCommand = new OleDbCommand(text, oleDbConnection);
			oleDbCommand.ExecuteNonQuery();
			this.dvSelected = (this.dgvSelectedUsers.DataSource as DataView);
			if (this.chkActive.Checked && this.nudTotal.Value > 0m && this.dvSelected != null)
			{
				for (int i = 1; i <= 9; i++)
				{
					this.dvSelected.RowFilter = "f_Selected > 0 AND f_MoreCards_GrpID = " + i;
					if (this.dvSelected.Count > 0)
					{
						for (int j = 0; j <= this.dvSelected.Count - 1; j++)
						{
							text = "INSERT INTO [t_d_doorMoreCardsUsers] (f_ConsumerID, f_DoorID ,f_MoreCards_GrpID)";
							text += " VALUES( ";
							text += this.dvSelected[j]["f_ConsumerID"].ToString();
							string text2 = text;
							text = string.Concat(new string[]
							{
								text2,
								", ",
								this.DoorID.ToString(),
								",",
								i.ToString(),
								")"
							});
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
						}
					}
				}
			}
			if (this.chkActive.Checked)
			{
				this.retValue = this.nudTotal.Value.ToString();
			}
			else
			{
				this.retValue = "0";
			}
			if (oleDbConnection.State == ConnectionState.Open)
			{
				oleDbConnection.Close();
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
			base.Close();
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkActive.Checked)
			{
				this.grpNeeded.Visible = false;
				this.grpOptInOut.Visible = false;
				this.groupBox1.Visible = false;
				return;
			}
			this.grpNeeded.Visible = true;
			if (this.grpOptInOut.Enabled)
			{
				this.grpOptInOut.Visible = true;
			}
			this.groupBox1.Visible = true;
			if (this.dgvUsers.DataSource == null)
			{
				return;
			}
			DataView dataSource = (DataView)this.dgvUsers.DataSource;
			this.dgvUsers.DataSource = null;
			this.dgvUsers.DataSource = dataSource;
		}

		private void dfrmMultiCards_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.grpOption.Visible = true;
			}
		}

		private void chkSingleGroup_CheckedChanged(object sender, EventArgs e)
		{
			this.nudGrpStartOfSingle.Visible = this.chkSingleGroup.Checked;
		}

		private DataTable loadUserData4BackWork()
		{
			Thread.Sleep(100);
			wgTools.WriteLine("loadUserData Start");
			this.dtUser1 = new DataTable();
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT  t_b_Consumer.f_ConsumerID, ";
				text += " IIF ( f_MoreCards_GrpID IS NULL , 0 , f_MoreCards_GrpID ) AS f_MoreCards_GrpID ";
				text += " , f_ConsumerNO, f_ConsumerName, f_CardNO ";
				text += " , IIF (  f_MoreCards_GrpID IS NULL , 0 , 1 ) AS f_Selected ";
				text += " , f_GroupID ";
				text += " FROM t_b_Consumer ";
				text += string.Format(" LEFT OUTER JOIN t_d_doorMoreCardsUsers ON ( t_b_Consumer.f_ConsumerID=t_d_doorMoreCardsUsers.f_ConsumerID AND f_DoorID= {0} )", this.DoorID.ToString());
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
					goto IL_1B2;
				}
			}
			text = " SELECT  t_b_Consumer.f_ConsumerID, ";
			text += " CASE WHEN f_MoreCards_GrpID IS NULL THEN 0 ELSE f_MoreCards_GrpID END AS f_MoreCards_GrpID ";
			text += " , f_ConsumerNO, f_ConsumerName, f_CardNO ";
			text += " , CASE WHEN f_MoreCards_GrpID IS NULL THEN 0 ELSE 1 END AS f_Selected ";
			text += " , f_GroupID ";
			text += " FROM t_b_Consumer ";
			text = text + " LEFT OUTER JOIN t_d_doorMoreCardsUsers ON t_b_Consumer.f_ConsumerID=t_d_doorMoreCardsUsers.f_ConsumerID AND f_DoorID= " + this.DoorID;
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
			IL_1B2:
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
			dfrmMultiCards.lastLoadUsers = icConsumerShare.getUpdateLog();
			dfrmMultiCards.dtLastLoad = this.dtUser1;
			return this.dtUser1;
		}

		private void loadUserData4BackWorkComplete(DataTable dtUser)
		{
			this.dv = new DataView(dtUser);
			this.dvSelected = new DataView(dtUser);
			this.dv.RowFilter = "f_Selected = 0";
			this.dvSelected.RowFilter = "f_Selected > 0";
			this.dvSelected.Sort = "f_MoreCards_GrpID ASC, f_ConsumerNo ASC ";
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
			this.groupBox1.Enabled = true;
			this.btnOK.Enabled = true;
		}
	}
}
