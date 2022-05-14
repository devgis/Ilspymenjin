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
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmControllerExtendFuncPasswordManage : frmN3000
	{
		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private DataGridView dataGridView1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		internal Button btnOK;

		internal Button btnCancel;

		private DataGridView dgvUsers;

		private BackgroundWorker backgroundWorker1;

		private Button btnChangePassword;

		private DataGridView dataGridView3;

		private ComboBox cboReader;

		private Label label2;

		private Label label1;

		private Button btnDel;

		private Button btnAdd;

		private Label label3;

		private DataGridView dataGridView4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		internal TextBox txtPasswordNew;

		private ComboBox cbof_GroupID;

		private Label label4;

		private DataGridViewTextBoxColumn f_ReaderID;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewTextBoxColumn f_ReaderNO;

		private DataGridViewTextBoxColumn f_ReaderName;

		private DataGridViewCheckBoxColumn f_PasswordEnabled;

		private DataGridViewTextBoxColumn f_Id;

		private DataGridViewTextBoxColumn f_Password;

		private DataGridViewTextBoxColumn f_AdaptTo;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn ConsumerNO;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewTextBoxColumn Deptname;

		private DataGridViewTextBoxColumn strPwd;

		private DataTable dtReaderPassword;

		private DataTable dtUserData;

		private DataTable dtPasswordKeypad;

		private DataTable dtReader;

		private DataView dvPasswordKeypad;

		private DataView dvReader;

		private DataView dvReaderPassword;

		private DataView dvUserData;

		private DataSet ds;

		private dfrmWait dfrmWait1 = new dfrmWait();

		private ArrayList arrReaderID = new ArrayList();

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private bool bLoadedFinished;

		private string recNOMax = "";

		private string dgvSql;

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private string strGroupFilter = "";

		private dfrmFind dfrmFind1 = new dfrmFind();

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dfrmWait1 != null)
			{
				this.dfrmWait1.Dispose();
			}
			if (disposing && this.dfrmFind1 != null)
			{
				this.dfrmFind1.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerExtendFuncPasswordManage));
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
			this.backgroundWorker1 = new BackgroundWorker();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.dataGridView1 = new DataGridView();
			this.f_ReaderID = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_ReaderNO = new DataGridViewTextBoxColumn();
			this.f_ReaderName = new DataGridViewTextBoxColumn();
			this.f_PasswordEnabled = new DataGridViewCheckBoxColumn();
			this.tabPage2 = new TabPage();
			this.cbof_GroupID = new ComboBox();
			this.label4 = new Label();
			this.btnChangePassword = new Button();
			this.dgvUsers = new DataGridView();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.ConsumerNO = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.Deptname = new DataGridViewTextBoxColumn();
			this.strPwd = new DataGridViewTextBoxColumn();
			this.tabPage3 = new TabPage();
			this.txtPasswordNew = new TextBox();
			this.btnDel = new Button();
			this.btnAdd = new Button();
			this.label3 = new Label();
			this.cboReader = new ComboBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.dataGridView3 = new DataGridView();
			this.f_Id = new DataGridViewTextBoxColumn();
			this.f_Password = new DataGridViewTextBoxColumn();
			this.f_AdaptTo = new DataGridViewTextBoxColumn();
			this.tabPage4 = new TabPage();
			this.dataGridView4 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			this.tabPage3.SuspendLayout();
			((ISupportInitialize)this.dataGridView3).BeginInit();
			this.tabPage4.SuspendLayout();
			((ISupportInitialize)this.dataGridView4).BeginInit();
			base.SuspendLayout();
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.dataGridView1);
			this.tabPage1.ForeColor = Color.White;
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ReaderID,
				this.f_ControllerSN,
				this.f_ReaderNO,
				this.f_ReaderName,
				this.f_PasswordEnabled
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			componentResourceManager.ApplyResources(this.f_ReaderID, "f_ReaderID");
			this.f_ReaderID.Name = "f_ReaderID";
			this.f_ReaderID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReaderNO, "f_ReaderNO");
			this.f_ReaderNO.Name = "f_ReaderNO";
			this.f_ReaderNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReaderName, "f_ReaderName");
			this.f_ReaderName.Name = "f_ReaderName";
			this.f_ReaderName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_PasswordEnabled, "f_PasswordEnabled");
			this.f_PasswordEnabled.Name = "f_PasswordEnabled";
			this.f_PasswordEnabled.Resizable = DataGridViewTriState.True;
			this.f_PasswordEnabled.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Controls.Add(this.cbof_GroupID);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.btnChangePassword);
			this.tabPage2.Controls.Add(this.dgvUsers);
			this.tabPage2.ForeColor = Color.White;
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.Name = "cbof_GroupID";
			this.cbof_GroupID.SelectedIndexChanged += new EventHandler(this.cbof_GroupID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.btnChangePassword, "btnChangePassword");
			this.btnChangePassword.BackgroundImage = Resources.pMain_button_normal;
			this.btnChangePassword.Name = "btnChangePassword";
			this.btnChangePassword.UseVisualStyleBackColor = true;
			this.btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AllowUserToDeleteRows = false;
			this.dgvUsers.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvUsers, "dgvUsers");
			this.dgvUsers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = Color.White;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ConsumerID,
				this.ConsumerNO,
				this.ConsumerName,
				this.CardNO,
				this.Deptname,
				this.strPwd
			});
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = SystemColors.Window;
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
			this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvUsers.Scroll += new ScrollEventHandler(this.dgvUsers_Scroll);
			this.dgvUsers.Click += new EventHandler(this.dgvUsers_DoubleClick);
			this.dgvUsers.DoubleClick += new EventHandler(this.dgvUsers_DoubleClick);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.ConsumerNO.DefaultCellStyle = dataGridViewCellStyle5;
			componentResourceManager.ApplyResources(this.ConsumerNO, "ConsumerNO");
			this.ConsumerNO.Name = "ConsumerNO";
			this.ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ConsumerName, "ConsumerName");
			this.ConsumerName.Name = "ConsumerName";
			this.ConsumerName.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.CardNO.DefaultCellStyle = dataGridViewCellStyle6;
			componentResourceManager.ApplyResources(this.CardNO, "CardNO");
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			this.Deptname.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.Deptname, "Deptname");
			this.Deptname.Name = "Deptname";
			this.Deptname.ReadOnly = true;
			componentResourceManager.ApplyResources(this.strPwd, "strPwd");
			this.strPwd.Name = "strPwd";
			this.strPwd.ReadOnly = true;
			this.tabPage3.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Controls.Add(this.txtPasswordNew);
			this.tabPage3.Controls.Add(this.btnDel);
			this.tabPage3.Controls.Add(this.btnAdd);
			this.tabPage3.Controls.Add(this.label3);
			this.tabPage3.Controls.Add(this.cboReader);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Controls.Add(this.label1);
			this.tabPage3.Controls.Add(this.dataGridView3);
			this.tabPage3.ForeColor = Color.White;
			this.tabPage3.Name = "tabPage3";
			componentResourceManager.ApplyResources(this.txtPasswordNew, "txtPasswordNew");
			this.txtPasswordNew.Name = "txtPasswordNew";
			this.txtPasswordNew.KeyPress += new KeyPressEventHandler(this.txtPasswordNew_KeyPress);
			this.btnDel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDel, "btnDel");
			this.btnDel.Name = "btnDel";
			this.btnDel.UseVisualStyleBackColor = true;
			this.btnDel.Click += new EventHandler(this.btnDel_Click);
			this.btnAdd.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.cboReader.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboReader.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboReader, "cboReader");
			this.cboReader.Name = "cboReader";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.dataGridView3.AllowUserToAddRows = false;
			this.dataGridView3.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dataGridView3, "dataGridView3");
			this.dataGridView3.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle7.ForeColor = Color.White;
			dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
			this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView3.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_Id,
				this.f_Password,
				this.f_AdaptTo
			});
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = SystemColors.Window;
			dataGridViewCellStyle8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle8.ForeColor = Color.White;
			dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
			this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle8;
			this.dataGridView3.EnableHeadersVisualStyles = false;
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.ReadOnly = true;
			this.dataGridView3.RowTemplate.Height = 23;
			this.dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.f_Id, "f_Id");
			this.f_Id.Name = "f_Id";
			this.f_Id.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Password, "f_Password");
			this.f_Password.Name = "f_Password";
			this.f_Password.ReadOnly = true;
			this.f_AdaptTo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_AdaptTo, "f_AdaptTo");
			this.f_AdaptTo.Name = "f_AdaptTo";
			this.f_AdaptTo.ReadOnly = true;
			this.tabPage4.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tabPage4, "tabPage4");
			this.tabPage4.Controls.Add(this.dataGridView4);
			this.tabPage4.ForeColor = Color.White;
			this.tabPage4.Name = "tabPage4";
			this.dataGridView4.AllowUserToAddRows = false;
			this.dataGridView4.AllowUserToDeleteRows = false;
			this.dataGridView4.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle9.ForeColor = Color.White;
			dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
			this.dataGridView4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView4.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.dataGridViewCheckBoxColumn1
			});
			dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = SystemColors.Window;
			dataGridViewCellStyle10.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle10.ForeColor = Color.White;
			dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
			this.dataGridView4.DefaultCellStyle = dataGridViewCellStyle10;
			componentResourceManager.ApplyResources(this.dataGridView4, "dataGridView4");
			this.dataGridView4.EnableHeadersVisualStyles = false;
			this.dataGridView4.Name = "dataGridView4";
			this.dataGridView4.RowTemplate.Height = 23;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
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
			this.dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.tabControl1);
			base.Name = "dfrmControllerExtendFuncPasswordManage";
			base.FormClosing += new FormClosingEventHandler(this.dfrmControllerExtendFuncPasswordManage_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerExtendFuncPasswordManage_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControllerExtendFuncPasswordManage_KeyDown);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((ISupportInitialize)this.dgvUsers).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((ISupportInitialize)this.dataGridView3).EndInit();
			this.tabPage4.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView4).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmControllerExtendFuncPasswordManage()
		{
			this.InitializeComponent();
		}

		private void fillPasswordKeypadEnableGrid()
		{
			string text = " SELECT ";
			text += " t_b_Reader.f_ReaderID ";
			text += ", t_b_Controller.f_ControllerSN ";
			text += ", t_b_Reader.f_ReaderNO ";
			text += ", t_b_Reader.f_ReaderName ";
			text += ", t_b_Reader.f_PasswordEnabled ";
			text += ", t_b_Controller.f_ZoneID ";
			text += " FROM t_b_Reader INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) ";
			text += " ORDER BY [f_ReaderID] ";
			this.dtPasswordKeypad = new DataTable();
			this.dvPasswordKeypad = new DataView(this.dtPasswordKeypad);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtPasswordKeypad);
						}
					}
					goto IL_12B;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtPasswordKeypad);
					}
				}
			}
			IL_12B:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtPasswordKeypad);
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.DataSource = this.dvPasswordKeypad;
			int num = 0;
			while (num < this.dvPasswordKeypad.Table.Columns.Count && num < this.dataGridView1.ColumnCount)
			{
				this.dataGridView1.Columns[num].DataPropertyName = this.dvPasswordKeypad.Table.Columns[num].ColumnName;
				num++;
			}
		}

		private void updatePasswordKeypadEnableGrid()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.updatePasswordKeypadEnableGrid_Acc();
				return;
			}
			this.dtPasswordKeypad = (this.dataGridView1.DataSource as DataView).Table;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
				{
					sqlConnection.Open();
					for (int i = 0; i <= this.dtPasswordKeypad.Rows.Count - 1; i++)
					{
						string text = " UPDATE t_b_Reader SET ";
						text = text + " f_PasswordEnabled = " + ((this.dtPasswordKeypad.Rows[i]["f_PasswordEnabled"].ToString() == "1") ? "1" : "0");
						text = text + " WHERE f_ReaderID = " + this.dtPasswordKeypad.Rows[i]["f_ReaderID"];
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}

		private void updatePasswordKeypadEnableGrid_Acc()
		{
			this.dtPasswordKeypad = (this.dataGridView1.DataSource as DataView).Table;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					oleDbConnection.Open();
					for (int i = 0; i <= this.dtPasswordKeypad.Rows.Count - 1; i++)
					{
						string text = " UPDATE t_b_Reader SET ";
						text = text + " f_PasswordEnabled = " + ((this.dtPasswordKeypad.Rows[i]["f_PasswordEnabled"].ToString() == "1") ? "1" : "0");
						text = text + " WHERE f_ReaderID = " + this.dtPasswordKeypad.Rows[i]["f_ReaderID"];
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
			}
		}

		private void dfrmControllerExtendFuncPasswordManage_Load(object sender, EventArgs e)
		{
			this.dfrmWait1.Show();
			this.dfrmWait1.Refresh();
			this.fillPasswordKeypadEnableGrid();
			this.fillUsersPasswordGrid();
			this.fillReaderPasswordGrid();
			this.fillManualPasswordKeypadEnableGrid();
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dataGridView3.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dataGridView4.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.tabPage1.BackColor = this.BackColor;
			this.tabPage2.BackColor = this.BackColor;
			this.tabPage3.BackColor = this.BackColor;
			this.tabPage4.BackColor = this.BackColor;
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.Deptname.HeaderText = wgAppConfig.ReplaceFloorRomm(this.Deptname.HeaderText);
			this.ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.ConsumerNO.HeaderText);
			this.loadOperatorPrivilege();
			this.dfrmWait1.Hide();
			try
			{
				base.Owner.Show();
				base.Owner.Activate();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuPasswordManagement";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnOK.Visible = false;
				this.btnChangePassword.Visible = false;
				this.btnAdd.Visible = false;
				this.btnDel.Visible = false;
				this.dataGridView1.ReadOnly = true;
				this.dataGridView3.ReadOnly = true;
				this.dataGridView4.ReadOnly = true;
				this.label1.Visible = false;
				this.label2.Visible = false;
				this.txtPasswordNew.Visible = false;
				this.cboReader.Visible = false;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.dfrmWait1.Show();
			this.dfrmWait1.Refresh();
			this.updatePasswordKeypadEnableGrid();
			this.updateManualPasswordKeypadEnableGrid();
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void fillManualPasswordKeypadEnableGrid()
		{
			string text = " SELECT ";
			text += " t_b_Reader.f_ReaderID ";
			text += ", t_b_Controller.f_ControllerSN ";
			text += ", t_b_Reader.f_ReaderNO ";
			text += ", t_b_Reader.f_ReaderName ";
			text += ", t_b_Reader.f_InputCardno_Enabled ";
			text += ", t_b_Controller.f_ZoneID ";
			text += "FROM t_b_Reader INNER JOIN t_b_Controller ON t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ";
			text += " ORDER BY [f_ReaderID] ";
			this.dtReader = new DataTable();
			this.dvReader = new DataView(this.dtReader);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtReader);
						}
					}
					goto IL_12B;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtReader);
					}
				}
			}
			IL_12B:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtReader);
			this.dataGridView4.AutoGenerateColumns = false;
			this.dataGridView4.DataSource = this.dvReader;
			int num = 0;
			while (num < this.dvReader.Table.Columns.Count && num < this.dataGridView4.ColumnCount)
			{
				this.dataGridView4.Columns[num].DataPropertyName = this.dvReader.Table.Columns[num].ColumnName;
				num++;
			}
		}

		private void updateManualPasswordKeypadEnableGrid()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.updateManualPasswordKeypadEnableGrid_Acc();
				return;
			}
			this.dtReader = (this.dataGridView4.DataSource as DataView).Table;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
				{
					sqlConnection.Open();
					for (int i = 0; i <= this.dtReader.Rows.Count - 1; i++)
					{
						string text = " UPDATE t_b_Reader SET ";
						text = text + " f_InputCardno_Enabled = " + ((this.dtReader.Rows[i]["f_InputCardno_Enabled"].ToString() == "1") ? "1" : "0");
						text = text + " WHERE f_ReaderID = " + this.dtReader.Rows[i]["f_ReaderID"];
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}

		private void updateManualPasswordKeypadEnableGrid_Acc()
		{
			this.dtReader = (this.dataGridView4.DataSource as DataView).Table;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					oleDbConnection.Open();
					for (int i = 0; i <= this.dtReader.Rows.Count - 1; i++)
					{
						string text = " UPDATE t_b_Reader SET ";
						text = text + " f_InputCardno_Enabled = " + ((this.dtReader.Rows[i]["f_InputCardno_Enabled"].ToString() == "1") ? "1" : "0");
						text = text + " WHERE f_ReaderID = " + this.dtReader.Rows[i]["f_ReaderID"];
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
			}
		}

		private void fillReaderPasswordGrid()
		{
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = string.Format(" SELECT  t_b_ReaderPassword.f_Id , t_b_ReaderPassword.f_Password , IIF ( f_BALL=1 , {0}, tt.f_ReaderName ) AS f_AdaptTo , tt.f_ZoneID  ", wgTools.PrepareStr(CommonStr.strAll));
				text += string.Format(" FROM    (SELECT t_b_Reader.f_ReaderID,t_b_Reader.f_ReaderName,  t_b_Controller.f_ZoneID  from    t_b_Controller INNER JOIN t_b_Reader ON  t_b_Controller.f_ControllerID = t_b_Reader.f_ControllerID  ) As tt  Right JOIN t_b_ReaderPassword ON ( tt.f_ReaderID = t_b_ReaderPassword.f_ReaderID) ", new object[0]);
				text += " ORDER BY [f_Id] ";
			}
			else
			{
				text = " SELECT ";
				text += " t_b_ReaderPassword.f_Id ";
				text += ", t_b_ReaderPassword.f_Password ";
				text = text + ", CASE WHEN f_BALL=1 THEN " + wgTools.PrepareStr(CommonStr.strAll);
				text += " ELSE t_b_Reader.f_ReaderName ";
				text += " END AS f_AdaptTo ";
				text += ", c.f_ZoneID ";
				text += " FROM t_b_ReaderPassword LEFT JOIN (t_b_Reader INNER JOIN t_b_Controller c ON c.f_ControllerID = t_b_Reader.f_ControllerID) ON t_b_Reader.f_ReaderID = t_b_ReaderPassword.f_ReaderID ";
				text += " ORDER BY [f_Id] ";
			}
			this.ds = new DataSet();
			this.dtReaderPassword = new DataTable();
			this.dvReaderPassword = new DataView(this.dtReaderPassword);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtReaderPassword);
						}
					}
					goto IL_181;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtReaderPassword);
					}
				}
			}
			IL_181:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtReaderPassword);
			this.dataGridView3.AutoGenerateColumns = false;
			this.dataGridView3.DataSource = this.dvReaderPassword;
			int i = 0;
			while (i < this.dvReaderPassword.Table.Columns.Count && i < this.dataGridView3.ColumnCount)
			{
				this.dataGridView3.Columns[i].DataPropertyName = this.dvReaderPassword.Table.Columns[i].ColumnName;
				i++;
			}
			if (this.cboReader.Items.Count == 0)
			{
				this.cboReader.Items.Clear();
				this.arrReaderID.Clear();
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand2 = new OleDbCommand("Select t_b_reader.*,t_b_Controller.f_ZoneID from t_b_reader inner join t_b_Controller on t_b_controller.f_controllerID = t_b_reader.f_ControllerID ", oleDbConnection2))
						{
							using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(oleDbCommand2))
							{
								oleDbDataAdapter2.Fill(this.ds, "reader");
							}
						}
						goto IL_310;
					}
				}
				using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand2 = new SqlCommand("Select t_b_reader.*,t_b_Controller.f_ZoneID from t_b_reader inner join t_b_Controller on t_b_controller.f_controllerID = t_b_reader.f_ControllerID ", sqlConnection2))
					{
						using (SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2))
						{
							sqlDataAdapter2.Fill(this.ds, "reader");
						}
					}
				}
				IL_310:
				this.dtReader = this.ds.Tables["reader"];
				int count = this.dtReader.Rows.Count;
				icControllerZone.getAllowedControllers(ref this.dtReader);
				if (count == this.dtReader.Rows.Count)
				{
					this.cboReader.Items.Add(CommonStr.strAll);
				}
				if (this.ds.Tables["reader"].Rows.Count > 0)
				{
					for (i = 0; i < this.ds.Tables["reader"].Rows.Count; i++)
					{
						string text2 = wgTools.SetObjToStr(this.ds.Tables["reader"].Rows[i]["f_ReaderName"]);
						if (this.cboReader.FindString(text2) < 0)
						{
							this.cboReader.Items.Add(text2);
							this.arrReaderID.Add(this.ds.Tables["reader"].Rows[i]["f_ReaderID"]);
						}
					}
				}
				if (this.cboReader.Items.Count > 0)
				{
					this.cboReader.SelectedIndex = 0;
				}
			}
		}

		private void fillUsersPasswordGrid()
		{
			this.loadStyle();
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
			Cursor.Current = Cursors.WaitCursor;
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = string.Format(" SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO,  f_GroupName , IIF ( f_PIN=0, {0}, IIF (f_PIN = 345678, {1},{2}))  AS strPwd, t_b_Consumer.f_GroupID  ", wgTools.PrepareStr(CommonStr.strPwdNoPassword), wgTools.PrepareStr(CommonStr.strPwdUnChanged), wgTools.PrepareStr(CommonStr.strPwdChanged));
				text += " FROM ( t_b_Consumer LEFT OUTER JOIN t_b_Group ON ( t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ) ) ";
			}
			else
			{
				text = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO,  f_GroupName ";
				text = text + " ,  CASE WHEN f_PIN=0 THEN " + wgTools.PrepareStr(CommonStr.strPwdNoPassword);
				text += " ELSE  ";
				text = text + " CASE WHEN f_PIN=345678 THEN " + wgTools.PrepareStr(CommonStr.strPwdUnChanged);
				text = text + " ELSE  " + wgTools.PrepareStr(CommonStr.strPwdChanged);
				text += " END   ";
				text += " END  AS strPwd, t_b_Consumer.f_GroupID  ";
				text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
			}
			this.reloadUserData(text);
		}

		private void loadStyle()
		{
			this.dgvUsers.AutoGenerateColumns = false;
			wgAppConfig.ReadGVStyle(this, this.dgvUsers);
		}

		private void reloadUserData(string strsql)
		{
			if (this.backgroundWorker1.IsBusy)
			{
				return;
			}
			this.bLoadedFinished = false;
			this.startRecordIndex = 0;
			this.MaxRecord = 1000;
			if (!string.IsNullOrEmpty(strsql))
			{
				this.dgvSql = strsql;
			}
			this.dgvUsers.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private DataView loadUserData(int startIndex, int maxRecords, string strSql)
		{
			wgTools.WriteLine("loadUserData Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recNOMax = "";
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND f_ConsumerNO > {0}", wgTools.PrepareStr(this.recNOMax));
			}
			else
			{
				strSql += string.Format(" WHERE f_ConsumerNO > {0}", wgTools.PrepareStr(this.recNOMax));
			}
			strSql += " ORDER BY f_ConsumerNO ";
			this.dtUserData = new DataTable("users");
			this.dvUserData = new DataView(this.dtUserData);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtUserData);
						}
					}
					goto IL_187;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtUserData);
					}
				}
			}
			IL_187:
			if (this.dtUserData.Rows.Count > 0)
			{
				this.recNOMax = this.dtUserData.Rows[this.dtUserData.Rows.Count - 1]["f_ConsumerNO"].ToString();
			}
			wgTools.WriteLine("loadUserData End");
			return this.dvUserData;
		}

		private void fillDgv(DataView dv)
		{
			try
			{
				DataGridView dataGridView = this.dgvUsers;
				if (dataGridView.DataSource == null)
				{
					this.dgvUsers.AutoGenerateColumns = false;
					dataGridView.DataSource = dv;
					int num = 0;
					while (num < dv.Table.Columns.Count && num < dataGridView.ColumnCount)
					{
						dataGridView.Columns[num].DataPropertyName = dv.Table.Columns[num].ColumnName;
						num++;
					}
					wgAppConfig.ReadGVStyle(this, dataGridView);
					dataGridView.Columns[0].Visible = false;
					if (this.startRecordIndex == 0 && dv.Count >= this.MaxRecord)
					{
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
					}
				}
				else if (dv.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = dataGridView.FirstDisplayedScrollingRowIndex;
					DataView dataView = dataGridView.DataSource as DataView;
					dataView.Table.Merge(dv.Table);
					if (firstDisplayedScrollingRowIndex >= 0)
					{
						dataGridView.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
					dataGridView.Columns[0].Visible = false;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			Cursor.Current = Cursors.Default;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			int startIndex = (int)((object[])e.Argument)[0];
			int maxRecords = (int)((object[])e.Argument)[1];
			string strSql = (string)((object[])e.Argument)[2];
			e.Result = this.loadUserData(startIndex, maxRecords, strSql);
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
			if ((e.Result as DataView).Count < this.MaxRecord)
			{
				this.bLoadedFinished = true;
			}
			this.fillDgv(e.Result as DataView);
			this.cbof_GroupID_SelectedIndexChanged(null, null);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvUsers_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				DataGridView dataGridView = this.dgvUsers;
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > dataGridView.Rows.Count || e.NewValue + dataGridView.Rows.Count / 10 > dataGridView.Rows.Count))
				{
					if (this.startRecordIndex <= dataGridView.Rows.Count)
					{
						if (this.backgroundWorker1.IsBusy)
						{
							return;
						}
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
						return;
					}
					else
					{
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void dgvUsers_DoubleClick(object sender, EventArgs e)
		{
			this.btnChangePassword.PerformClick();
		}

		private void btnChangePassword_Click(object sender, EventArgs e)
		{
			if (this.dgvUsers.SelectedRows.Count <= 0)
			{
				if (this.dgvUsers.SelectedCells.Count <= 0)
				{
					return;
				}
				int arg_3D_0 = this.dgvUsers.SelectedCells[0].RowIndex;
			}
			else
			{
				int arg_56_0 = this.dgvUsers.SelectedRows[0].Index;
			}
			int index = 0;
			DataGridView dataGridView = this.dgvUsers;
			DataGridViewColumn arg_66_0 = dataGridView.SortedColumn;
			if (dataGridView.Rows.Count > 0)
			{
				index = dataGridView.CurrentCell.RowIndex;
			}
			using (dfrmSetPassword dfrmSetPassword = new dfrmSetPassword())
			{
				dfrmSetPassword.operatorID = 0;
				dfrmSetPassword.Text = string.Concat(new object[]
				{
					this.btnChangePassword.Text,
					"  [",
					this.dgvUsers.Rows[index].Cells[2].Value,
					"] "
				});
				if (dfrmSetPassword.ShowDialog(this) == DialogResult.OK)
				{
					string text = "Update t_b_Consumer ";
					string a;
					if (wgTools.SetObjToStr(dfrmSetPassword.newPassword) == "")
					{
						text = text + "SET [f_PIN]=" + wgTools.PrepareStr(0);
						a = "0";
					}
					else
					{
						text = text + "SET [f_PIN]=" + wgTools.PrepareStr(dfrmSetPassword.newPassword);
						a = wgTools.SetObjToStr(dfrmSetPassword.newPassword);
					}
					text += "  WHERE ";
					text = text + " [f_ConsumerID]=" + this.dgvUsers.Rows[index].Cells[0].Value;
					if (wgAppConfig.runUpdateSql(text) == 1)
					{
						if (a == "0")
						{
							this.dgvUsers.Rows[index].Cells["strPwd"].Value = CommonStr.strPwdNoPassword;
						}
						else if (a == 345678.ToString())
						{
							this.dgvUsers.Rows[index].Cells["strPwd"].Value = CommonStr.strPwdUnChanged;
						}
						else
						{
							this.dgvUsers.Rows[index].Cells["strPwd"].Value = CommonStr.strPwdChanged;
						}
					}
				}
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (this.txtPasswordNew.Text.Trim() == "")
			{
				this.txtPasswordNew.Text = "";
				return;
			}
			long num;
			if (!long.TryParse(this.txtPasswordNew.Text, out num))
			{
				XMessageBox.Show(this, CommonStr.strPasswordWarn, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (num <= 0L)
			{
				XMessageBox.Show(this, CommonStr.strPasswordWarn, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.cboReader.Items.Count <= 0)
			{
				return;
			}
			long num2 = num;
			string text = "  Insert INTO t_b_ReaderPassword (f_Password, f_BAll, f_ReaderID) ";
			text = text + " Values( " + num2;
			if (this.cboReader.Items[0].ToString() == CommonStr.strAll)
			{
				if (this.cboReader.SelectedIndex == 0)
				{
					text = text + " , " + 1;
					text = text + " , " + 0;
				}
				else
				{
					text = text + " , " + 0;
					text = text + " , " + this.arrReaderID[this.cboReader.SelectedIndex - 1];
				}
			}
			else
			{
				text = text + " , " + 0;
				text = text + " , " + this.arrReaderID[this.cboReader.SelectedIndex];
			}
			text += ")";
			if (wgAppConfig.runUpdateSql(text) > 0)
			{
				this.fillReaderPasswordGrid();
			}
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			if (this.dataGridView3.SelectedRows.Count <= 0)
			{
				if (this.dataGridView3.SelectedCells.Count <= 0)
				{
					return;
				}
				int arg_3D_0 = this.dataGridView3.SelectedCells[0].RowIndex;
			}
			else
			{
				int arg_56_0 = this.dataGridView3.SelectedRows[0].Index;
			}
			int index = 0;
			DataGridView dataGridView = this.dataGridView3;
			if (dataGridView.Rows.Count > 0)
			{
				index = dataGridView.CurrentCell.RowIndex;
			}
			string strSql = "DELETE FROM t_b_ReaderPassword  WHERE f_Id =" + ((int)dataGridView.Rows[index].Cells[0].Value).ToString();
			if (wgAppConfig.runUpdateSql(strSql) > 0)
			{
				this.fillReaderPasswordGrid();
			}
		}

		private void txtPasswordNew_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\b')
			{
				return;
			}
			int num;
			if (int.TryParse(e.KeyChar.ToString(), out num))
			{
				return;
			}
			e.Handled = true;
		}

		private void cbof_GroupID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvUsers.DataSource != null)
			{
				DataView arg_20_0 = (DataView)this.dgvUsers.DataSource;
				if (this.cbof_GroupID.SelectedIndex < 0 || (this.cbof_GroupID.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
				{
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
				}
				((DataView)this.dgvUsers.DataSource).RowFilter = this.strGroupFilter;
			}
		}

		private void dfrmControllerExtendFuncPasswordManage_KeyDown(object sender, KeyEventArgs e)
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
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmControllerExtendFuncPasswordManage_FormClosing(object sender, FormClosingEventArgs e)
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
	}
}
