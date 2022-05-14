using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmTCPIPWEBConfigure : frmN3000
	{
		private const int webStringCount = 154;

		private IContainer components;

		private Label label1;

		private Label label2;

		private TextBox txtf_ControllerSN;

		private TextBox txtf_MACAddr;

		private Label label3;

		private TextBox txtf_IP;

		private Label label4;

		private TextBox txtf_mask;

		private Label label5;

		private TextBox txtf_gateway;

		private Button btnOK;

		private Button btnCancel;

		public CheckBox chkEditIP;

		public NumericUpDown nudHttpPort;

		public ComboBox cboLanguage;

		private Label label8;

		private Button btnSelectFile;

		private Button btnOtherLanguage;

		public TextBox txtSelectedFileName;

		public CheckBox chkUpdateWebSet;

		public CheckBox chkUpdateSuperCard;

		private Label label9;

		public MaskedTextBox txtSuperCard2;

		private Label label10;

		public MaskedTextBox txtSuperCard1;

		private Label label11;

		private Button btnuploadUser;

		private GroupBox grpWEBUsers;

		public ComboBox cboLanguage2;

		private Label label12;

		private Button btnEditUsers;

		private Button btnDownloadUsers;

		private Button btnSelectUserFile;

		public TextBox txtUsersFile;

		public CheckBox chkAutoUploadWEBUsers;

		public GroupBox grpIP;

		public GroupBox grpWEB;

		public GroupBox grpSuperCards;

		public Button btnOption;

		public Button btnOptionWEB;

		public Label lblPort;

		public NumericUpDown nudPort;

		public Label lblHttpPort;

		public RadioButton optWEBEnabled;

		public RadioButton optWEBDisable;

		public GroupBox grpWEBEnabled;

		private OpenFileDialog openFileDialog1;

		private DataGridView dataGridView3;

		public CheckBox chkAdjustTime;

		private Button btnTryWEB;

		public ComboBox cboDateFormat;

		public Label label6;

		private Button btnRestoreNameAndPassword;

		public CheckBox chkUpdateSpecialCard;

		public GroupBox grpSpecialCards;

		public MaskedTextBox txtSpecialCard2;

		private Label label13;

		public MaskedTextBox txtSpecialCard1;

		private Label label14;

		public CheckBox chkWebOnlyQuery;

		public string strSN = "";

		public string strMac = "";

		public string strIP = "";

		public string strMask = "";

		public string strGateway = "";

		public string strTCPPort = "";

		public string strPCAddress = "";

		public string strSearchedIP = "";

		public string strSearchedMask = "";

		public DataTable dtWebString;

		public DataTable dtUsers;

		private DataTable dtPrivilege;

		private DataView dv;

		private DataTable tb;

		private DataTable tb1;

		private DataTable tb2;

		private DataTable tb3;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmTCPIPWEBConfigure));
			this.openFileDialog1 = new OpenFileDialog();
			this.btnCancel = new Button();
			this.grpWEBEnabled = new GroupBox();
			this.optWEBEnabled = new RadioButton();
			this.optWEBDisable = new RadioButton();
			this.grpWEBUsers = new GroupBox();
			this.txtUsersFile = new TextBox();
			this.dataGridView3 = new DataGridView();
			this.chkAutoUploadWEBUsers = new CheckBox();
			this.cboLanguage2 = new ComboBox();
			this.label12 = new Label();
			this.btnEditUsers = new Button();
			this.btnDownloadUsers = new Button();
			this.btnSelectUserFile = new Button();
			this.btnuploadUser = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.txtf_ControllerSN = new TextBox();
			this.txtf_MACAddr = new TextBox();
			this.label3 = new Label();
			this.txtf_IP = new TextBox();
			this.label4 = new Label();
			this.txtf_mask = new TextBox();
			this.label5 = new Label();
			this.txtf_gateway = new TextBox();
			this.btnOK = new Button();
			this.btnOption = new Button();
			this.lblPort = new Label();
			this.grpIP = new GroupBox();
			this.nudPort = new NumericUpDown();
			this.chkEditIP = new CheckBox();
			this.grpWEB = new GroupBox();
			this.chkWebOnlyQuery = new CheckBox();
			this.cboDateFormat = new ComboBox();
			this.label6 = new Label();
			this.btnOptionWEB = new Button();
			this.lblHttpPort = new Label();
			this.nudHttpPort = new NumericUpDown();
			this.cboLanguage = new ComboBox();
			this.label8 = new Label();
			this.btnSelectFile = new Button();
			this.btnOtherLanguage = new Button();
			this.txtSelectedFileName = new TextBox();
			this.chkUpdateWebSet = new CheckBox();
			this.chkUpdateSuperCard = new CheckBox();
			this.grpSuperCards = new GroupBox();
			this.label9 = new Label();
			this.txtSuperCard2 = new MaskedTextBox();
			this.label10 = new Label();
			this.txtSuperCard1 = new MaskedTextBox();
			this.label11 = new Label();
			this.chkAdjustTime = new CheckBox();
			this.btnTryWEB = new Button();
			this.btnRestoreNameAndPassword = new Button();
			this.chkUpdateSpecialCard = new CheckBox();
			this.grpSpecialCards = new GroupBox();
			this.txtSpecialCard2 = new MaskedTextBox();
			this.label13 = new Label();
			this.txtSpecialCard1 = new MaskedTextBox();
			this.label14 = new Label();
			this.grpWEBEnabled.SuspendLayout();
			this.grpWEBUsers.SuspendLayout();
			((ISupportInitialize)this.dataGridView3).BeginInit();
			this.grpIP.SuspendLayout();
			((ISupportInitialize)this.nudPort).BeginInit();
			this.grpWEB.SuspendLayout();
			((ISupportInitialize)this.nudHttpPort).BeginInit();
			this.grpSuperCards.SuspendLayout();
			this.grpSpecialCards.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.openFileDialog1, "openFileDialog1");
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.grpWEBEnabled, "grpWEBEnabled");
			this.grpWEBEnabled.Controls.Add(this.optWEBEnabled);
			this.grpWEBEnabled.Controls.Add(this.optWEBDisable);
			this.grpWEBEnabled.ForeColor = Color.White;
			this.grpWEBEnabled.Name = "grpWEBEnabled";
			this.grpWEBEnabled.TabStop = false;
			componentResourceManager.ApplyResources(this.optWEBEnabled, "optWEBEnabled");
			this.optWEBEnabled.Checked = true;
			this.optWEBEnabled.ForeColor = Color.White;
			this.optWEBEnabled.Name = "optWEBEnabled";
			this.optWEBEnabled.TabStop = true;
			this.optWEBEnabled.UseVisualStyleBackColor = true;
			this.optWEBEnabled.CheckedChanged += new EventHandler(this.optWEBEnabled_CheckedChanged);
			componentResourceManager.ApplyResources(this.optWEBDisable, "optWEBDisable");
			this.optWEBDisable.ForeColor = Color.White;
			this.optWEBDisable.Name = "optWEBDisable";
			this.optWEBDisable.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.grpWEBUsers, "grpWEBUsers");
			this.grpWEBUsers.Controls.Add(this.txtUsersFile);
			this.grpWEBUsers.Controls.Add(this.dataGridView3);
			this.grpWEBUsers.Controls.Add(this.chkAutoUploadWEBUsers);
			this.grpWEBUsers.Controls.Add(this.cboLanguage2);
			this.grpWEBUsers.Controls.Add(this.label12);
			this.grpWEBUsers.Controls.Add(this.btnEditUsers);
			this.grpWEBUsers.Controls.Add(this.btnDownloadUsers);
			this.grpWEBUsers.Controls.Add(this.btnSelectUserFile);
			this.grpWEBUsers.Controls.Add(this.btnuploadUser);
			this.grpWEBUsers.ForeColor = Color.White;
			this.grpWEBUsers.Name = "grpWEBUsers";
			this.grpWEBUsers.TabStop = false;
			componentResourceManager.ApplyResources(this.txtUsersFile, "txtUsersFile");
			this.txtUsersFile.Name = "txtUsersFile";
			this.txtUsersFile.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridView3, "dataGridView3");
			this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.RowTemplate.Height = 23;
			componentResourceManager.ApplyResources(this.chkAutoUploadWEBUsers, "chkAutoUploadWEBUsers");
			this.chkAutoUploadWEBUsers.ForeColor = Color.White;
			this.chkAutoUploadWEBUsers.Name = "chkAutoUploadWEBUsers";
			this.chkAutoUploadWEBUsers.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.cboLanguage2, "cboLanguage2");
			this.cboLanguage2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboLanguage2.FormattingEnabled = true;
			this.cboLanguage2.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboLanguage2.Items"),
				componentResourceManager.GetString("cboLanguage2.Items1"),
				componentResourceManager.GetString("cboLanguage2.Items2")
			});
			this.cboLanguage2.Name = "cboLanguage2";
			componentResourceManager.ApplyResources(this.label12, "label12");
			this.label12.BackColor = Color.Transparent;
			this.label12.ForeColor = Color.White;
			this.label12.Name = "label12";
			componentResourceManager.ApplyResources(this.btnEditUsers, "btnEditUsers");
			this.btnEditUsers.BackColor = Color.Transparent;
			this.btnEditUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnEditUsers.ForeColor = Color.White;
			this.btnEditUsers.Name = "btnEditUsers";
			this.btnEditUsers.UseVisualStyleBackColor = false;
			this.btnEditUsers.Click += new EventHandler(this.btnEditUsers_Click);
			componentResourceManager.ApplyResources(this.btnDownloadUsers, "btnDownloadUsers");
			this.btnDownloadUsers.BackColor = Color.Transparent;
			this.btnDownloadUsers.BackgroundImage = Resources.pMain_button_normal;
			this.btnDownloadUsers.ForeColor = Color.White;
			this.btnDownloadUsers.Name = "btnDownloadUsers";
			this.btnDownloadUsers.UseVisualStyleBackColor = false;
			this.btnDownloadUsers.Click += new EventHandler(this.btnDownloadUsers_Click);
			componentResourceManager.ApplyResources(this.btnSelectUserFile, "btnSelectUserFile");
			this.btnSelectUserFile.BackColor = Color.Transparent;
			this.btnSelectUserFile.BackgroundImage = Resources.pMain_button_normal;
			this.btnSelectUserFile.ForeColor = Color.White;
			this.btnSelectUserFile.Name = "btnSelectUserFile";
			this.btnSelectUserFile.UseVisualStyleBackColor = false;
			this.btnSelectUserFile.Click += new EventHandler(this.btnSelectUserFile_Click);
			componentResourceManager.ApplyResources(this.btnuploadUser, "btnuploadUser");
			this.btnuploadUser.BackColor = Color.Transparent;
			this.btnuploadUser.BackgroundImage = Resources.pMain_button_normal;
			this.btnuploadUser.ForeColor = Color.White;
			this.btnuploadUser.Name = "btnuploadUser";
			this.btnuploadUser.UseVisualStyleBackColor = false;
			this.btnuploadUser.Click += new EventHandler(this.btnuploadUser_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.txtf_ControllerSN, "txtf_ControllerSN");
			this.txtf_ControllerSN.Name = "txtf_ControllerSN";
			this.txtf_ControllerSN.ReadOnly = true;
			this.txtf_ControllerSN.TabStop = false;
			componentResourceManager.ApplyResources(this.txtf_MACAddr, "txtf_MACAddr");
			this.txtf_MACAddr.Name = "txtf_MACAddr";
			this.txtf_MACAddr.ReadOnly = true;
			this.txtf_MACAddr.TabStop = false;
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.txtf_IP, "txtf_IP");
			this.txtf_IP.Name = "txtf_IP";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.txtf_mask, "txtf_mask");
			this.txtf_mask.Name = "txtf_mask";
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.ForeColor = Color.White;
			this.label5.Name = "label5";
			componentResourceManager.ApplyResources(this.txtf_gateway, "txtf_gateway");
			this.txtf_gateway.Name = "txtf_gateway";
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnOption, "btnOption");
			this.btnOption.BackColor = Color.Transparent;
			this.btnOption.BackgroundImage = Resources.pMain_button_normal;
			this.btnOption.ForeColor = Color.White;
			this.btnOption.Name = "btnOption";
			this.btnOption.TabStop = false;
			this.btnOption.UseVisualStyleBackColor = false;
			this.btnOption.Click += new EventHandler(this.btnOption_Click);
			componentResourceManager.ApplyResources(this.lblPort, "lblPort");
			this.lblPort.ForeColor = Color.White;
			this.lblPort.Name = "lblPort";
			componentResourceManager.ApplyResources(this.grpIP, "grpIP");
			this.grpIP.BackColor = Color.Transparent;
			this.grpIP.Controls.Add(this.nudPort);
			this.grpIP.Controls.Add(this.lblPort);
			this.grpIP.Controls.Add(this.label3);
			this.grpIP.Controls.Add(this.txtf_gateway);
			this.grpIP.Controls.Add(this.label5);
			this.grpIP.Controls.Add(this.txtf_IP);
			this.grpIP.Controls.Add(this.txtf_mask);
			this.grpIP.Controls.Add(this.label4);
			this.grpIP.Controls.Add(this.btnOption);
			this.grpIP.Name = "grpIP";
			this.grpIP.TabStop = false;
			componentResourceManager.ApplyResources(this.nudPort, "nudPort");
			NumericUpDown arg_CEF_0 = this.nudPort;
			int[] array = new int[4];
			array[0] = 65534;
			arg_CEF_0.Maximum = new decimal(array);
			NumericUpDown arg_D0F_0 = this.nudPort;
			int[] array2 = new int[4];
			array2[0] = 1024;
			arg_D0F_0.Minimum = new decimal(array2);
			this.nudPort.Name = "nudPort";
			this.nudPort.TabStop = false;
			NumericUpDown arg_D4E_0 = this.nudPort;
			int[] array3 = new int[4];
			array3[0] = 60000;
			arg_D4E_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this.chkEditIP, "chkEditIP");
			this.chkEditIP.ForeColor = Color.White;
			this.chkEditIP.Name = "chkEditIP";
			this.chkEditIP.UseVisualStyleBackColor = true;
			this.chkEditIP.CheckedChanged += new EventHandler(this.chkEditIP_CheckedChanged);
			componentResourceManager.ApplyResources(this.grpWEB, "grpWEB");
			this.grpWEB.Controls.Add(this.chkWebOnlyQuery);
			this.grpWEB.Controls.Add(this.cboDateFormat);
			this.grpWEB.Controls.Add(this.label6);
			this.grpWEB.Controls.Add(this.btnOptionWEB);
			this.grpWEB.Controls.Add(this.lblHttpPort);
			this.grpWEB.Controls.Add(this.nudHttpPort);
			this.grpWEB.Controls.Add(this.cboLanguage);
			this.grpWEB.Controls.Add(this.label8);
			this.grpWEB.Controls.Add(this.btnSelectFile);
			this.grpWEB.Controls.Add(this.btnOtherLanguage);
			this.grpWEB.Controls.Add(this.txtSelectedFileName);
			this.grpWEB.Name = "grpWEB";
			this.grpWEB.TabStop = false;
			componentResourceManager.ApplyResources(this.chkWebOnlyQuery, "chkWebOnlyQuery");
			this.chkWebOnlyQuery.ForeColor = Color.White;
			this.chkWebOnlyQuery.Name = "chkWebOnlyQuery";
			this.chkWebOnlyQuery.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.cboDateFormat, "cboDateFormat");
			this.cboDateFormat.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboDateFormat.FormattingEnabled = true;
			this.cboDateFormat.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboDateFormat.Items"),
				componentResourceManager.GetString("cboDateFormat.Items1"),
				componentResourceManager.GetString("cboDateFormat.Items2"),
				componentResourceManager.GetString("cboDateFormat.Items3"),
				componentResourceManager.GetString("cboDateFormat.Items4"),
				componentResourceManager.GetString("cboDateFormat.Items5")
			});
			this.cboDateFormat.Name = "cboDateFormat";
			componentResourceManager.ApplyResources(this.label6, "label6");
			this.label6.BackColor = Color.Transparent;
			this.label6.ForeColor = Color.White;
			this.label6.Name = "label6";
			componentResourceManager.ApplyResources(this.btnOptionWEB, "btnOptionWEB");
			this.btnOptionWEB.BackColor = Color.Transparent;
			this.btnOptionWEB.BackgroundImage = Resources.pMain_button_normal;
			this.btnOptionWEB.ForeColor = Color.White;
			this.btnOptionWEB.Name = "btnOptionWEB";
			this.btnOptionWEB.TabStop = false;
			this.btnOptionWEB.UseVisualStyleBackColor = false;
			this.btnOptionWEB.Click += new EventHandler(this.btnOptionWEB_Click);
			componentResourceManager.ApplyResources(this.lblHttpPort, "lblHttpPort");
			this.lblHttpPort.BackColor = Color.Transparent;
			this.lblHttpPort.ForeColor = Color.White;
			this.lblHttpPort.Name = "lblHttpPort";
			componentResourceManager.ApplyResources(this.nudHttpPort, "nudHttpPort");
			NumericUpDown arg_10E1_0 = this.nudHttpPort;
			int[] array4 = new int[4];
			array4[0] = 65535;
			arg_10E1_0.Maximum = new decimal(array4);
			this.nudHttpPort.Name = "nudHttpPort";
			NumericUpDown arg_1111_0 = this.nudHttpPort;
			int[] array5 = new int[4];
			array5[0] = 80;
			arg_1111_0.Value = new decimal(array5);
			componentResourceManager.ApplyResources(this.cboLanguage, "cboLanguage");
			this.cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboLanguage.FormattingEnabled = true;
			this.cboLanguage.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboLanguage.Items"),
				componentResourceManager.GetString("cboLanguage.Items1"),
				componentResourceManager.GetString("cboLanguage.Items2")
			});
			this.cboLanguage.Name = "cboLanguage";
			this.cboLanguage.SelectedIndexChanged += new EventHandler(this.cboLanguage_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label8, "label8");
			this.label8.BackColor = Color.Transparent;
			this.label8.ForeColor = Color.White;
			this.label8.Name = "label8";
			componentResourceManager.ApplyResources(this.btnSelectFile, "btnSelectFile");
			this.btnSelectFile.BackColor = Color.Transparent;
			this.btnSelectFile.BackgroundImage = Resources.pMain_button_normal;
			this.btnSelectFile.ForeColor = Color.White;
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.UseVisualStyleBackColor = false;
			this.btnSelectFile.Click += new EventHandler(this.btnSelectFile_Click);
			componentResourceManager.ApplyResources(this.btnOtherLanguage, "btnOtherLanguage");
			this.btnOtherLanguage.BackColor = Color.Transparent;
			this.btnOtherLanguage.BackgroundImage = Resources.pMain_button_normal;
			this.btnOtherLanguage.ForeColor = Color.White;
			this.btnOtherLanguage.Name = "btnOtherLanguage";
			this.btnOtherLanguage.UseVisualStyleBackColor = false;
			this.btnOtherLanguage.Click += new EventHandler(this.btnOtherLanguage_Click);
			componentResourceManager.ApplyResources(this.txtSelectedFileName, "txtSelectedFileName");
			this.txtSelectedFileName.Name = "txtSelectedFileName";
			this.txtSelectedFileName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.chkUpdateWebSet, "chkUpdateWebSet");
			this.chkUpdateWebSet.ForeColor = Color.White;
			this.chkUpdateWebSet.Name = "chkUpdateWebSet";
			this.chkUpdateWebSet.UseVisualStyleBackColor = true;
			this.chkUpdateWebSet.CheckedChanged += new EventHandler(this.chkUpdateWebSet_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkUpdateSuperCard, "chkUpdateSuperCard");
			this.chkUpdateSuperCard.ForeColor = Color.White;
			this.chkUpdateSuperCard.Name = "chkUpdateSuperCard";
			this.chkUpdateSuperCard.UseVisualStyleBackColor = true;
			this.chkUpdateSuperCard.CheckedChanged += new EventHandler(this.chkUpdateSuperCard_CheckedChanged);
			componentResourceManager.ApplyResources(this.grpSuperCards, "grpSuperCards");
			this.grpSuperCards.Controls.Add(this.label9);
			this.grpSuperCards.Controls.Add(this.txtSuperCard2);
			this.grpSuperCards.Controls.Add(this.label10);
			this.grpSuperCards.Controls.Add(this.txtSuperCard1);
			this.grpSuperCards.Controls.Add(this.label11);
			this.grpSuperCards.ForeColor = Color.White;
			this.grpSuperCards.Name = "grpSuperCards";
			this.grpSuperCards.TabStop = false;
			componentResourceManager.ApplyResources(this.label9, "label9");
			this.label9.BackColor = Color.Transparent;
			this.label9.ForeColor = Color.White;
			this.label9.Name = "label9";
			componentResourceManager.ApplyResources(this.txtSuperCard2, "txtSuperCard2");
			this.txtSuperCard2.Name = "txtSuperCard2";
			this.txtSuperCard2.KeyPress += new KeyPressEventHandler(this.txtSuperCard2_KeyPress);
			this.txtSuperCard2.KeyUp += new KeyEventHandler(this.txtSuperCard2_KeyUp);
			componentResourceManager.ApplyResources(this.label10, "label10");
			this.label10.BackColor = Color.Transparent;
			this.label10.ForeColor = Color.White;
			this.label10.Name = "label10";
			componentResourceManager.ApplyResources(this.txtSuperCard1, "txtSuperCard1");
			this.txtSuperCard1.Name = "txtSuperCard1";
			this.txtSuperCard1.KeyPress += new KeyPressEventHandler(this.txtSuperCard1_KeyPress);
			this.txtSuperCard1.KeyUp += new KeyEventHandler(this.txtSuperCard1_KeyUp);
			componentResourceManager.ApplyResources(this.label11, "label11");
			this.label11.BackColor = Color.Transparent;
			this.label11.ForeColor = Color.White;
			this.label11.Name = "label11";
			componentResourceManager.ApplyResources(this.chkAdjustTime, "chkAdjustTime");
			this.chkAdjustTime.ForeColor = Color.White;
			this.chkAdjustTime.Name = "chkAdjustTime";
			this.chkAdjustTime.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnTryWEB, "btnTryWEB");
			this.btnTryWEB.BackColor = Color.Transparent;
			this.btnTryWEB.BackgroundImage = Resources.pMain_button_normal;
			this.btnTryWEB.ForeColor = Color.White;
			this.btnTryWEB.Image = Resources.web;
			this.btnTryWEB.Name = "btnTryWEB";
			this.btnTryWEB.UseVisualStyleBackColor = false;
			this.btnTryWEB.Click += new EventHandler(this.btnTryWEB_Click);
			componentResourceManager.ApplyResources(this.btnRestoreNameAndPassword, "btnRestoreNameAndPassword");
			this.btnRestoreNameAndPassword.BackColor = Color.Transparent;
			this.btnRestoreNameAndPassword.BackgroundImage = Resources.pMain_button_normal;
			this.btnRestoreNameAndPassword.ForeColor = Color.White;
			this.btnRestoreNameAndPassword.Name = "btnRestoreNameAndPassword";
			this.btnRestoreNameAndPassword.UseVisualStyleBackColor = false;
			this.btnRestoreNameAndPassword.Click += new EventHandler(this.btnRestoreNameAndPassword_Click);
			componentResourceManager.ApplyResources(this.chkUpdateSpecialCard, "chkUpdateSpecialCard");
			this.chkUpdateSpecialCard.ForeColor = Color.White;
			this.chkUpdateSpecialCard.Name = "chkUpdateSpecialCard";
			this.chkUpdateSpecialCard.UseVisualStyleBackColor = true;
			this.chkUpdateSpecialCard.CheckedChanged += new EventHandler(this.chkUpdateSpecialCard_CheckedChanged);
			componentResourceManager.ApplyResources(this.grpSpecialCards, "grpSpecialCards");
			this.grpSpecialCards.Controls.Add(this.txtSpecialCard2);
			this.grpSpecialCards.Controls.Add(this.label13);
			this.grpSpecialCards.Controls.Add(this.txtSpecialCard1);
			this.grpSpecialCards.Controls.Add(this.label14);
			this.grpSpecialCards.ForeColor = Color.White;
			this.grpSpecialCards.Name = "grpSpecialCards";
			this.grpSpecialCards.TabStop = false;
			componentResourceManager.ApplyResources(this.txtSpecialCard2, "txtSpecialCard2");
			this.txtSpecialCard2.Name = "txtSpecialCard2";
			componentResourceManager.ApplyResources(this.label13, "label13");
			this.label13.BackColor = Color.Transparent;
			this.label13.ForeColor = Color.White;
			this.label13.Name = "label13";
			componentResourceManager.ApplyResources(this.txtSpecialCard1, "txtSpecialCard1");
			this.txtSpecialCard1.Name = "txtSpecialCard1";
			componentResourceManager.ApplyResources(this.label14, "label14");
			this.label14.BackColor = Color.Transparent;
			this.label14.ForeColor = Color.White;
			this.label14.Name = "label14";
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.chkUpdateSpecialCard);
			base.Controls.Add(this.grpSpecialCards);
			base.Controls.Add(this.btnRestoreNameAndPassword);
			base.Controls.Add(this.btnTryWEB);
			base.Controls.Add(this.chkAdjustTime);
			base.Controls.Add(this.grpWEBEnabled);
			base.Controls.Add(this.grpWEBUsers);
			base.Controls.Add(this.chkUpdateSuperCard);
			base.Controls.Add(this.grpSuperCards);
			base.Controls.Add(this.chkUpdateWebSet);
			base.Controls.Add(this.grpWEB);
			base.Controls.Add(this.chkEditIP);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.grpIP);
			base.Controls.Add(this.txtf_ControllerSN);
			base.Controls.Add(this.txtf_MACAddr);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmTCPIPWEBConfigure";
			base.Load += new EventHandler(this.dfrmTCPIPConfigure_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmTCPIPWEBConfigure_KeyDown);
			this.grpWEBEnabled.ResumeLayout(false);
			this.grpWEBEnabled.PerformLayout();
			this.grpWEBUsers.ResumeLayout(false);
			this.grpWEBUsers.PerformLayout();
			((ISupportInitialize)this.dataGridView3).EndInit();
			this.grpIP.ResumeLayout(false);
			this.grpIP.PerformLayout();
			((ISupportInitialize)this.nudPort).EndInit();
			this.grpWEB.ResumeLayout(false);
			this.grpWEB.PerformLayout();
			((ISupportInitialize)this.nudHttpPort).EndInit();
			this.grpSuperCards.ResumeLayout(false);
			this.grpSuperCards.PerformLayout();
			this.grpSpecialCards.ResumeLayout(false);
			this.grpSpecialCards.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmTCPIPWEBConfigure()
		{
			this.InitializeComponent();
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
			this.btnOption.Enabled = false;
			this.lblPort.Visible = true;
			this.nudPort.Visible = true;
		}

		public bool isIPAddress(string ipstr)
		{
			bool result = false;
			try
			{
				if (!string.IsNullOrEmpty(ipstr))
				{
					string[] array = ipstr.Split(new char[]
					{
						'.'
					});
					if (array.Length == 4)
					{
						result = true;
						for (int i = 0; i <= 3; i++)
						{
							int num;
							if (!int.TryParse(array[i], out num))
							{
								result = false;
								break;
							}
							if (num < 0 || num > 255)
							{
								result = false;
								break;
							}
						}
						if (int.Parse(array[0]) == 0)
						{
							result = false;
						}
						else if (int.Parse(array[3]) == 255)
						{
							result = false;
						}
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!this.txtf_ControllerSN.ReadOnly)
			{
				this.txtf_ControllerSN.Text = this.txtf_ControllerSN.Text.Trim();
				int num;
				if (!int.TryParse(this.txtf_ControllerSN.Text, out num))
				{
					XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (wgMjController.GetControllerType(int.Parse(this.txtf_ControllerSN.Text)) == 0)
				{
					XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			if (this.chkEditIP.Checked)
			{
				if (string.IsNullOrEmpty(this.txtf_IP.Text))
				{
					XMessageBox.Show(this, CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.txtf_IP.Text = this.txtf_IP.Text.Replace(" ", "");
				if (!this.isIPAddress(this.txtf_IP.Text))
				{
					XMessageBox.Show(this, this.txtf_IP.Text + "  " + CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.txtf_mask.Text = this.txtf_mask.Text.Replace(" ", "");
				if (!this.isIPAddress(this.txtf_mask.Text))
				{
					XMessageBox.Show(this, this.txtf_mask.Text + "  " + CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.txtf_gateway.Text = this.txtf_gateway.Text.Replace(" ", "");
				if (!string.IsNullOrEmpty(this.txtf_gateway.Text) && !this.isIPAddress(this.txtf_gateway.Text))
				{
					XMessageBox.Show(this, this.txtf_gateway.Text + "  " + CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			if (this.chkUpdateWebSet.Checked && (this.nudHttpPort.Value == 60000m || this.nudHttpPort.Value == this.nudPort.Value))
			{
				XMessageBox.Show(this, this.lblHttpPort.Text + "  " + CommonStr.strHttpWEBWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.chkUpdateWebSet.Checked && this.cboLanguage.SelectedIndex == 2)
			{
				if (string.IsNullOrEmpty(this.txtSelectedFileName.Text))
				{
					XMessageBox.Show(CommonStr.strTranslateFileSelect);
					return;
				}
				if (this.dtWebString == null)
				{
					bool flag = false;
					string text = this.txtSelectedFileName.Text;
					if (File.Exists(text))
					{
						try
						{
							this.tb1 = new DataTable();
							this.tb1.TableName = "WEBString";
							this.tb1.Columns.Add("f_NO");
							this.tb1.Columns.Add("f_Name");
							this.tb1.Columns.Add("f_Value");
							this.tb1.Columns.Add("f_CName");
							this.tb1.ReadXml(text);
							this.tb1.AcceptChanges();
							if (this.tb1.Rows.Count == 154)
							{
								bool flag2 = true;
								for (int i = 0; i < this.tb1.Rows.Count; i++)
								{
									if (string.IsNullOrEmpty(this.tb1.Rows[i]["f_Value"].ToString()))
									{
										XMessageBox.Show(string.Format("{0} {1}", this.tb1.Rows[i]["f_NO"].ToString(), CommonStr.strTranslateValueInvavid));
										return;
									}
								}
								if (flag2)
								{
									flag = true;
									this.dtWebString = this.tb1.Copy();
								}
							}
						}
						catch
						{
						}
					}
					if (flag)
					{
						goto IL_411;
					}
					XMessageBox.Show(CommonStr.strTranslateFileInvalid);
					return;
				}
			}
			IL_411:
			if (this.chkAutoUploadWEBUsers.Checked)
			{
				if (string.IsNullOrEmpty(this.txtUsersFile.Text))
				{
					XMessageBox.Show(CommonStr.strUserFileSelect);
					return;
				}
				if (!File.Exists(this.txtUsersFile.Text))
				{
					XMessageBox.Show(CommonStr.strUserFileSelect);
					return;
				}
			}
			this.strSN = this.txtf_ControllerSN.Text;
			this.strMac = this.txtf_MACAddr.Text;
			this.strIP = this.txtf_IP.Text;
			this.strMask = this.txtf_mask.Text;
			this.strGateway = this.txtf_gateway.Text;
			this.strTCPPort = this.nudPort.Value.ToString();
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void dfrmTCPIPConfigure_Load(object sender, EventArgs e)
		{
			this.txtSuperCard1.Mask = "9999999999";
			this.txtSuperCard2.Mask = "9999999999";
			this.txtf_ControllerSN.Text = this.strSN;
			this.txtf_MACAddr.Text = this.strMac;
			this.txtf_IP.Text = this.strIP;
			this.txtf_mask.Text = this.strMask;
			this.txtf_gateway.Text = this.strGateway;
			if (string.IsNullOrEmpty(this.strTCPPort))
			{
				this.strTCPPort = 60000.ToString();
			}
			else if (int.Parse(this.strTCPPort) < this.nudPort.Minimum || int.Parse(this.strTCPPort) >= 65535)
			{
				this.strTCPPort = 60000.ToString();
			}
			this.nudPort.Value = int.Parse(this.strTCPPort);
			if (this.txtf_IP.Text == "255.255.255.255")
			{
				this.txtf_IP.Text = "192.168.0.0";
			}
			if (this.txtf_mask.Text == "255.255.255.255")
			{
				this.txtf_mask.Text = "255.255.255.0";
			}
			if (this.txtf_gateway.Text == "255.255.255.255")
			{
				this.txtf_gateway.Text = "";
			}
			if (this.txtf_gateway.Text == "0.0.0.0")
			{
				this.txtf_gateway.Text = "";
			}
		}

		private void btnOptionWEB_Click(object sender, EventArgs e)
		{
			this.btnOptionWEB.Enabled = false;
			this.lblHttpPort.Visible = true;
			this.nudHttpPort.Visible = true;
			this.label6.Visible = true;
			this.cboDateFormat.Visible = true;
		}

		private void optWEBEnabled_CheckedChanged(object sender, EventArgs e)
		{
			this.grpWEB.Enabled = this.optWEBEnabled.Checked;
		}

		private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.btnSelectFile.Visible = (this.cboLanguage.SelectedIndex == 2);
			this.btnOtherLanguage.Visible = (this.cboLanguage.SelectedIndex == 2);
			this.txtSelectedFileName.Visible = (this.cboLanguage.SelectedIndex == 2);
		}

		private void chkEditIP_CheckedChanged(object sender, EventArgs e)
		{
			this.grpIP.Enabled = this.chkEditIP.Checked;
		}

		private void chkUpdateWebSet_CheckedChanged(object sender, EventArgs e)
		{
			this.grpWEBEnabled.Enabled = this.chkUpdateWebSet.Checked;
			this.grpWEB.Enabled = (this.grpWEBEnabled.Enabled && this.optWEBEnabled.Checked);
		}

		private void chkUpdateSuperCard_CheckedChanged(object sender, EventArgs e)
		{
			this.grpSuperCards.Enabled = this.chkUpdateSuperCard.Checked;
		}

		private void btnSelectFile_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Filter = " (*.xml)|*.xml| (*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				this.openFileDialog1.Title = (sender as Button).Text;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = this.openFileDialog1.FileName;
					bool flag = false;
					string text = fileName;
					try
					{
						if (File.Exists(text))
						{
							this.tb2 = new DataTable();
							this.tb2.TableName = "WEBString";
							this.tb2.Columns.Add("f_NO");
							this.tb2.Columns.Add("f_Name");
							this.tb2.Columns.Add("f_Value");
							this.tb2.Columns.Add("f_CName");
							this.tb2.ReadXml(text);
							this.tb2.AcceptChanges();
							if (this.tb2.Rows.Count == 154)
							{
								bool flag2 = true;
								for (int i = 0; i < this.tb2.Rows.Count; i++)
								{
									if (string.IsNullOrEmpty(this.tb2.Rows[i]["f_Value"].ToString()))
									{
										XMessageBox.Show(string.Format(CommonStr.strTranslateValueInvavid, this.tb2.Rows[i]["f_NO"].ToString()));
										return;
									}
								}
								if (flag2)
								{
									flag = true;
									this.dtWebString = this.tb2.Copy();
								}
							}
						}
						if (!flag)
						{
							XMessageBox.Show(CommonStr.strTranslateFileInvalid);
							return;
						}
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
					this.txtSelectedFileName.Text = fileName;
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
		}

		private void btnSelectUserFile_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Filter = " (*.xml)|*.xml| (*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				this.openFileDialog1.Title = (sender as Button).Text;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = this.openFileDialog1.FileName;
					bool flag = false;
					string text = fileName;
					try
					{
						if (File.Exists(text))
						{
							this.tb = new DataTable();
							this.tb.TableName = wgAppConfig.dbWEBUserName;
							this.tb.Columns.Add("f_CardNO");
							this.tb.Columns.Add("f_ConsumerName");
							this.tb.ReadXml(text);
							this.tb.AcceptChanges();
							flag = true;
							this.dtUsers = this.tb.Copy();
						}
						if (!flag)
						{
							XMessageBox.Show(CommonStr.strUsersFileInvalid);
							return;
						}
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
					this.txtUsersFile.Text = fileName;
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
		}

		private void btnOtherLanguage_Click(object sender, EventArgs e)
		{
			using (dfrmTranslate dfrmTranslate = new dfrmTranslate())
			{
				dfrmTranslate.ShowDialog();
			}
		}

		private void btnEditUsers_Click(object sender, EventArgs e)
		{
			using (dfrmEditUserFile dfrmEditUserFile = new dfrmEditUserFile())
			{
				dfrmEditUserFile.ShowDialog();
			}
		}

		private void txtSuperCard1_KeyPress(object sender, KeyPressEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtSuperCard1);
		}

		private void txtSuperCard1_KeyUp(object sender, KeyEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtSuperCard1);
		}

		private void txtSuperCard2_KeyPress(object sender, KeyPressEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtSuperCard2);
		}

		private void txtSuperCard2_KeyUp(object sender, KeyEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtSuperCard2);
		}

		private void btnDownloadUsers_Click(object sender, EventArgs e)
		{
			try
			{
				int controllerSN = int.Parse(this.txtf_ControllerSN.Text);
				Cursor.Current = Cursors.WaitCursor;
				using (wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege())
				{
					wgMjControllerPrivilege.AllowDownload();
					if (this.dtPrivilege != null)
					{
						this.dtPrivilege.Rows.Clear();
						this.dtPrivilege.Dispose();
						this.dtPrivilege = null;
						GC.Collect();
					}
					if (this.dtPrivilege == null)
					{
						this.dtPrivilege = new DataTable(wgAppConfig.dbWEBUserName);
						this.dtPrivilege.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
						this.dtPrivilege.Columns.Add("f_BeginYMD", Type.GetType("System.DateTime"));
						this.dtPrivilege.Columns.Add("f_EndYMD", Type.GetType("System.DateTime"));
						this.dtPrivilege.Columns.Add("f_PIN", Type.GetType("System.String"));
						this.dtPrivilege.Columns.Add("f_ControlSegID1", Type.GetType("System.Byte"));
						this.dtPrivilege.Columns["f_ControlSegID1"].DefaultValue = 0;
						this.dtPrivilege.Columns.Add("f_ControlSegID2", Type.GetType("System.Byte"));
						this.dtPrivilege.Columns["f_ControlSegID2"].DefaultValue = 0;
						this.dtPrivilege.Columns.Add("f_ControlSegID3", Type.GetType("System.Byte"));
						this.dtPrivilege.Columns["f_ControlSegID3"].DefaultValue = 0;
						this.dtPrivilege.Columns.Add("f_ControlSegID4", Type.GetType("System.Byte"));
						this.dtPrivilege.Columns["f_ControlSegID4"].DefaultValue = 0;
						this.dtPrivilege.Columns.Add("f_ConsumerName", Type.GetType("System.String"));
						this.dtPrivilege.Columns.Add("f_IsDeleted", Type.GetType("System.UInt32"));
					}
					if (wgMjControllerPrivilege.DownloadIP(controllerSN, null, 60000, "INCLUDEDELETED", ref this.dtPrivilege, this.strPCAddress) > 0)
					{
						if (this.dtPrivilege.Rows.Count >= 0)
						{
							this.dtPrivilege.Columns.Remove("f_BeginYMD");
							this.dtPrivilege.Columns.Remove("f_EndYMD");
							this.dtPrivilege.Columns.Remove("f_PIN");
							this.dtPrivilege.Columns.Remove("f_ControlSegID1");
							this.dtPrivilege.Columns.Remove("f_ControlSegID2");
							this.dtPrivilege.Columns.Remove("f_ControlSegID3");
							this.dtPrivilege.Columns.Remove("f_ControlSegID4");
							this.dtPrivilege.AcceptChanges();
							this.dv = new DataView(this.dtPrivilege);
							this.dv.RowFilter = "f_IsDeleted = 1";
							if (this.dv.Count > 0)
							{
								for (int i = this.dv.Count - 1; i >= 0; i--)
								{
									this.dv.Delete(i);
								}
							}
							this.dtPrivilege.AcceptChanges();
							this.dtPrivilege.Columns.Remove("f_IsDeleted");
							this.dtPrivilege.AcceptChanges();
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
								this.dtPrivilege.WriteXml(stringWriter, XmlWriteMode.WriteSchema, true);
								using (StreamWriter streamWriter = new StreamWriter(text, false))
								{
									streamWriter.Write(stringWriter.ToString());
								}
							}
							XMessageBox.Show((sender as Button).Text + "\r\n\r\n" + text);
						}
						else
						{
							XMessageBox.Show(string.Concat(new string[]
							{
								(sender as Button).Text,
								" ",
								controllerSN.ToString(),
								" ",
								CommonStr.strFailed
							}));
						}
					}
					else
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							(sender as Button).Text,
							" ",
							controllerSN.ToString(),
							" ",
							CommonStr.strFailed
						}));
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			Cursor.Current = Cursors.Default;
		}

		private int ipweb_uploadusers(string userFile, int controllerSN, string lang)
		{
			if (!File.Exists(userFile))
			{
				return 0;
			}
			this.tb3 = new DataTable();
			this.tb3.TableName = wgAppConfig.dbWEBUserName;
			this.tb3.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
			this.tb3.Columns.Add("f_ConsumerName");
			this.tb3.ReadXml(userFile);
			this.tb3.AcceptChanges();
			this.dv = new DataView(this.tb3);
			this.dv.Sort = "f_CardNO ASC";
			try
			{
				using (wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege())
				{
					wgMjControllerPrivilege.AllowUpload();
					if (this.dtPrivilege != null)
					{
						this.dtPrivilege.Rows.Clear();
						this.dtPrivilege.Dispose();
						this.dtPrivilege = null;
						GC.Collect();
					}
					this.dtPrivilege = new DataTable("Privilege");
					this.dtPrivilege.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
					this.dtPrivilege.Columns.Add("f_ConsumerName", Type.GetType("System.String"));
					this.dtPrivilege.Columns.Add("f_BeginYMD", Type.GetType("System.DateTime"));
					this.dtPrivilege.Columns.Add("f_EndYMD", Type.GetType("System.DateTime"));
					this.dtPrivilege.Columns.Add("f_PIN", Type.GetType("System.String"));
					this.dtPrivilege.Columns.Add("f_ControlSegID1", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID1"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID2", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID2"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID3", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID3"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID4", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID4"].DefaultValue = 0;
					uint num = 0u;
					int result;
					for (int i = 0; i < this.dv.Count; i++)
					{
						DataRow dataRow = this.dtPrivilege.NewRow();
						dataRow["f_CardNO"] = (uint)this.dv[i]["f_CardNO"];
						dataRow["f_ConsumerName"] = this.dv[i]["f_ConsumerName"];
						dataRow["f_BeginYMD"] = DateTime.Parse("2011-1-1");
						dataRow["f_EndYMD"] = DateTime.Parse("2029-12-31");
						dataRow["f_PIN"] = 0;
						dataRow["f_ControlSegID1"] = 1;
						dataRow["f_ControlSegID2"] = 1;
						dataRow["f_ControlSegID3"] = 1;
						dataRow["f_ControlSegID4"] = 1;
						dataRow["f_ConsumerName"] = this.dv[i]["f_ConsumerName"];
						if ((uint)dataRow["f_CardNO"] <= num)
						{
							XMessageBox.Show(CommonStr.strFailed);
							result = 0;
							return result;
						}
						num = (uint)dataRow["f_CardNO"];
						this.dtPrivilege.Rows.Add(dataRow);
					}
					this.dtPrivilege.AcceptChanges();
					wgMjControllerPrivilege.bAllowUploadUserName = true;
					if (wgMjControllerPrivilege.UploadIP(controllerSN, null, 60000, "DOOR NAME", this.dtPrivilege, this.strPCAddress) < 0)
					{
						XMessageBox.Show(CommonStr.strFailed);
						result = 0;
						return result;
					}
					result = 1;
					return result;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return 0;
		}

		private void btnuploadUser_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtUsersFile.Text))
			{
				XMessageBox.Show(CommonStr.strUserFileSelect);
				return;
			}
			if (!File.Exists(this.txtUsersFile.Text))
			{
				XMessageBox.Show(CommonStr.strUserFileSelect);
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			string lang = "utf-8";
			if (this.ipweb_uploadusers(this.txtUsersFile.Text, int.Parse(this.txtf_ControllerSN.Text), lang) > 0)
			{
				Cursor.Current = Cursors.Default;
				wgAppConfig.wgLog((sender as Button).Text + "  SN=" + this.strSN);
				XMessageBox.Show(string.Concat(new string[]
				{
					(sender as Button).Text,
					"  SN=",
					this.strSN,
					" ",
					CommonStr.strSuccessfully
				}));
			}
			Cursor.Current = Cursors.Default;
		}

		private void funCtrlShiftQ()
		{
			if (this.btnRestoreNameAndPassword.Visible)
			{
				this.chkUpdateSpecialCard.Visible = true;
			}
			this.chkAutoUploadWEBUsers.Visible = true;
			this.btnRestoreNameAndPassword.Visible = true;
		}

		private void dfrmTCPIPWEBConfigure_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Control && e.Shift && e.KeyValue == 81)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						this.funCtrlShiftQ();
					}
				}
			}
			catch
			{
			}
		}

		private void getMaskGateway(string pcIPAddress, ref string mask, ref string gateway)
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			NetworkInterface[] array = allNetworkInterfaces;
			for (int i = 0; i < array.Length; i++)
			{
				NetworkInterface networkInterface = array[i];
				IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
				UnicastIPAddressInformationCollection unicastAddresses = iPProperties.UnicastAddresses;
				if (unicastAddresses.Count > 0)
				{
					Console.WriteLine(networkInterface.Description);
					foreach (UnicastIPAddressInformation current in unicastAddresses)
					{
						if (!current.Address.IsIPv6LinkLocal && !(current.Address.ToString() == "127.0.0.1") && current.Address.ToString() == pcIPAddress)
						{
							mask = current.IPv4Mask.ToString();
							if (iPProperties.GatewayAddresses.Count > 0)
							{
								gateway = iPProperties.GatewayAddresses[0].Address.ToString();
							}
							return;
						}
					}
					Console.WriteLine();
				}
			}
		}

		private bool CommunicteSocketTcpIsValid(string ipdest, int port)
		{
			Socket socket = null;
			bool result = false;
			try
			{
				IPAddress address = IPAddress.Parse(ipdest);
				IPEndPoint remoteEP = new IPEndPoint(address, port);
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.SendTimeout = 1000;
				socket.ReceiveTimeout = 1000;
				socket.Connect(remoteEP);
				if (socket.Connected)
				{
					result = true;
				}
				socket.Close();
				socket = null;
			}
			catch
			{
				if (socket != null)
				{
					socket.Close();
				}
				return result;
			}
			return result;
		}

		private void btnTryWEB_Click(object sender, EventArgs e)
		{
			this.tryWEB_ByARP();
		}

		private int IPLng(IPAddress ip)
		{
			byte[] array = new byte[4];
			array = ip.GetAddressBytes();
			return ((int)array[3] << 24) + ((int)array[2] << 16) + ((int)array[1] << 8) + (int)array[0];
		}

		private void tryWEB_ByARP()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.btnTryWEB.Enabled = false;
			icController icController = new icController();
			try
			{
				string text = "";
				icController.ControllerSN = int.Parse(this.strSN);
				if (icController.GetControllerRunInformationIP(this.strPCAddress) <= 0)
				{
					XMessageBox.Show(string.Format("{0} {1} {2}", CommonStr.strController, this.strSN, CommonStr.strCommFail));
				}
				else
				{
					wgMjControllerConfigure wgMjControllerConfigure = null;
					icController.GetConfigureIP(ref wgMjControllerConfigure);
					if (wgMjControllerConfigure != null)
					{
						text = wgMjControllerConfigure.ip.ToString();
					}
					bool flag = false;
					if (text != "192.168.0.0" && text != "192.168.168.0" && text != "255.255.255.255" && text != "")
					{
						icController.IP = text;
						if (icController.GetControllerRunInformationIP(this.strPCAddress) > 0)
						{
							flag = true;
						}
						icController.IP = "";
					}
					if (!flag)
					{
						IPAddress iPAddress = IPAddress.Parse(this.strPCAddress);
						byte[] array = new byte[4];
						array = iPAddress.GetAddressBytes();
						if (array[3] != 123)
						{
							array[3] = 123;
							iPAddress = new IPAddress(array);
						}
						byte[] array2 = new byte[6];
						uint num = (uint)array2.Length;
						int num2 = wgGlobal.SafeNativeMethods.SendARP(this.IPLng(iPAddress), this.IPLng(IPAddress.Parse(this.strPCAddress)), array2, ref num);
						if (num2 == 0)
						{
							byte[] expr_159_cp_0 = array;
							int expr_159_cp_1 = 3;
							expr_159_cp_0[expr_159_cp_1] += 1;
							while (array[3] != 123)
							{
								if (array[3] == 0 || array[3] == 255)
								{
									byte[] expr_182_cp_0 = array;
									int expr_182_cp_1 = 3;
									expr_182_cp_0[expr_182_cp_1] += 1;
								}
								else
								{
									iPAddress = new IPAddress(array);
									num = (uint)array2.Length;
									num2 = wgGlobal.SafeNativeMethods.SendARP(this.IPLng(iPAddress), this.IPLng(IPAddress.Parse(this.strPCAddress)), array2, ref num);
									if (num2 != 0)
									{
										break;
									}
									byte[] expr_1D1_cp_0 = array;
									int expr_1D1_cp_1 = 3;
									expr_1D1_cp_0[expr_1D1_cp_1] += 1;
								}
							}
						}
						if (num2 != 0)
						{
							byte[] array3 = new byte[1152];
							for (int i = 0; i < array3.Length; i++)
							{
								array3[i] = 0;
							}
							int num3 = 80;
							int num4 = 12288;
							if (wgAppConfig.IsChineseSet(wgAppConfig.CultureInfoStr))
							{
								num4 = 8192;
							}
							int num5 = 100;
							array3[num5] = (byte)(num4 & 255);
							byte[] expr_254_cp_0 = array3;
							int expr_254_cp_1 = 1024 + (num5 >> 3);
							expr_254_cp_0[expr_254_cp_1] |= (byte)(1 << (num5 & 7));
							num5++;
							array3[num5] = (byte)(num4 >> 8);
							byte[] expr_28C_cp_0 = array3;
							int expr_28C_cp_1 = 1024 + (num5 >> 3);
							expr_28C_cp_0[expr_28C_cp_1] |= (byte)(1 << (num5 & 7));
							num5++;
							array3[num5] = (byte)(num4 >> 16);
							byte[] expr_2C5_cp_0 = array3;
							int expr_2C5_cp_1 = 1024 + (num5 >> 3);
							expr_2C5_cp_0[expr_2C5_cp_1] |= (byte)(1 << (num5 & 7));
							num5++;
							array3[num5] = (byte)(num4 >> 24);
							byte[] expr_2FE_cp_0 = array3;
							int expr_2FE_cp_1 = 1024 + (num5 >> 3);
							expr_2FE_cp_0[expr_2FE_cp_1] |= (byte)(1 << (num5 & 7));
							num5 = 96;
							array3[num5] = (byte)(num3 & 255);
							byte[] expr_338_cp_0 = array3;
							int expr_338_cp_1 = 1024 + (num5 >> 3);
							expr_338_cp_0[expr_338_cp_1] |= (byte)(1 << (num5 & 7));
							num5++;
							array3[num5] = (byte)(num3 >> 8);
							byte[] expr_370_cp_0 = array3;
							int expr_370_cp_1 = 1024 + (num5 >> 3);
							expr_370_cp_0[expr_370_cp_1] |= (byte)(1 << (num5 & 7));
							icController.UpdateConfigureCPUSuperIP(array3, "", this.strPCAddress);
							string text2 = "";
							string text3 = "";
							this.getMaskGateway(this.strPCAddress, ref text2, ref text3);
							icController.NetIPConfigure(icController.ControllerSN.ToString(), wgMjControllerConfigure.MACAddr, iPAddress.ToString(), text2, text3, 60000.ToString(), this.strPCAddress);
							Thread.Sleep(2000);
						}
						int num6 = 3;
						num = (uint)array2.Length;
						num2 = wgGlobal.SafeNativeMethods.SendARP(this.IPLng(iPAddress), this.IPLng(IPAddress.Parse(this.strPCAddress)), array2, ref num);
						while (num2 != 0 && num6-- > 0)
						{
							Thread.Sleep(500);
							num = (uint)array2.Length;
							num2 = wgGlobal.SafeNativeMethods.SendARP(this.IPLng(iPAddress), this.IPLng(IPAddress.Parse(this.strPCAddress)), array2, ref num);
						}
						if (num2 == 0)
						{
							text = iPAddress.ToString();
							flag = true;
						}
					}
					if (flag && !this.CommunicteSocketTcpIsValid(text, 80))
					{
						byte[] array4 = new byte[1152];
						for (int j = 0; j < array4.Length; j++)
						{
							array4[j] = 0;
						}
						int num7 = 80;
						int num8 = 12288;
						if (wgAppConfig.IsChineseSet(wgAppConfig.CultureInfoStr))
						{
							num8 = 8192;
						}
						int num9 = 100;
						array4[num9] = (byte)(num8 & 255);
						byte[] expr_4F6_cp_0 = array4;
						int expr_4F6_cp_1 = 1024 + (num9 >> 3);
						expr_4F6_cp_0[expr_4F6_cp_1] |= (byte)(1 << (num9 & 7));
						num9++;
						array4[num9] = (byte)(num8 >> 8);
						byte[] expr_52E_cp_0 = array4;
						int expr_52E_cp_1 = 1024 + (num9 >> 3);
						expr_52E_cp_0[expr_52E_cp_1] |= (byte)(1 << (num9 & 7));
						num9++;
						array4[num9] = (byte)(num8 >> 16);
						byte[] expr_567_cp_0 = array4;
						int expr_567_cp_1 = 1024 + (num9 >> 3);
						expr_567_cp_0[expr_567_cp_1] |= (byte)(1 << (num9 & 7));
						num9++;
						array4[num9] = (byte)(num8 >> 24);
						byte[] expr_5A0_cp_0 = array4;
						int expr_5A0_cp_1 = 1024 + (num9 >> 3);
						expr_5A0_cp_0[expr_5A0_cp_1] |= (byte)(1 << (num9 & 7));
						num9 = 96;
						array4[num9] = (byte)(num7 & 255);
						byte[] expr_5DA_cp_0 = array4;
						int expr_5DA_cp_1 = 1024 + (num9 >> 3);
						expr_5DA_cp_0[expr_5DA_cp_1] |= (byte)(1 << (num9 & 7));
						num9++;
						array4[num9] = (byte)(num7 >> 8);
						byte[] expr_612_cp_0 = array4;
						int expr_612_cp_1 = 1024 + (num9 >> 3);
						expr_612_cp_0[expr_612_cp_1] |= (byte)(1 << (num9 & 7));
						icController.UpdateConfigureCPUSuperIP(array4, "", this.strPCAddress);
						icController.RebootControllerIP(this.strPCAddress);
						Thread.Sleep(2000);
					}
					if (flag)
					{
						Process.Start(new ProcessStartInfo
						{
							FileName = "HTTP://" + text,
							UseShellExecute = true
						});
					}
					else
					{
						XMessageBox.Show(string.Format("{0} {1} {2}", CommonStr.strController, this.strSN, CommonStr.strFailed));
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				if (icController != null)
				{
					icController.Dispose();
				}
				Cursor.Current = Cursors.Default;
				this.btnTryWEB.Enabled = true;
			}
		}

		private void btnRestoreNameAndPassword_Click(object sender, EventArgs e)
		{
			try
			{
				if (XMessageBox.Show(CommonStr.strRebootController4Restore, (sender as Button).Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					using (icController icController = new icController())
					{
						icController.ControllerSN = int.Parse(this.txtf_ControllerSN.Text);
						icController.UpdateFRamIP(268435457u, 0u);
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			Cursor.Current = Cursors.Default;
		}

		private void chkUpdateSpecialCard_CheckedChanged(object sender, EventArgs e)
		{
			this.grpSpecialCards.Visible = this.chkUpdateSpecialCard.Checked;
			this.grpSpecialCards.Enabled = this.chkUpdateSpecialCard.Checked;
		}
	}
}
