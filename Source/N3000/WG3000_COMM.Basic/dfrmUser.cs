using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmUser : frmN3000
	{
		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private Label label1;

		private TabPage tabPage2;

		private Label label3;

		private Label label2;

		private GroupBox grpbAttendance;

		private RadioButton optShift;

		private RadioButton optNormal;

		private CheckBox chkDoorEnabled;

		private CheckBox chkAttendance;

		private Label label6;

		private Label label5;

		private Label label4;

		private GroupBox grpbMainInfo;

		private GroupBox grpbAccessControl;

		private Label label7;

		private TextBox txtf_ConsumerName;

		private TextBox txtf_ConsumerNO;

		private ComboBox cbof_GroupID;

		private PictureBox pictureBox1;

		private Button btnSelectPhoto;

		private DateTimePicker dtpDeactivate;

		private DateTimePicker dtpActivate;

		private Button btnAddNext;

		private Button btnOK;

		private Button btnCancel;

		private MaskedTextBox txtf_CardNO;

		private MaskedTextBox txtf_PIN;

		private Label label8;

		private Timer timer1;

		internal ComboBox txtf_Sex;

		internal TextBox txtf_Addr;

		internal Label Label18;

		internal Label Label16;

		internal Label Label20;

		internal Label Label21;

		internal TextBox txtf_Postcode;

		internal TextBox txtf_JoinDate;

		internal TextBox txtf_LeaveDate;

		internal Label Label13;

		internal TextBox txtf_Political;

		internal Label Label14;

		internal TextBox txtf_CertificateType;

		internal Label Label15;

		internal TextBox txtf_CertificateID;

		internal Label Label17;

		internal TextBox txtf_Telephone;

		internal Label Label19;

		internal TextBox txtf_Mobile;

		internal Label Label22;

		internal TextBox txtf_Email;

		internal Label Label23;

		internal Label Label24;

		internal TextBox txtf_Culture;

		internal TextBox txtf_TechGrade;

		internal Label Label25;

		internal Label Label26;

		internal Label Label27;

		internal TextBox txtf_Hometown;

		internal TextBox txtf_Title;

		internal TextBox txtf_CorporationName;

		internal Label Label28;

		internal TextBox txtf_Birthday;

		internal Label Label30;

		internal TextBox txtf_Marriage;

		internal Label Label31;

		internal TextBox txtf_SocialInsuranceNo;

		internal Label Label32;

		internal Label Label33;

		internal TextBox txtf_HomePhone;

		internal Label Label34;

		internal TextBox txtf_Nationality;

		internal Label Label35;

		internal TextBox txtf_Religion;

		internal TextBox txtf_EnglishName;

		internal Label Label36;

		internal TextBox txtf_Note;

		internal Label Label37;

		private OpenFileDialog openFileDialog1;

		private bool m_OperateNew = true;

		private int m_consumerID;

		private string m_curGroup = "";

		private bool bContinued;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private string strUserAutoAddSet;

		private string strStartCaption = "";

		private int userIDlen;

		private string photoFileName = "";

		public bool OperateNew
		{
			get
			{
				return this.m_OperateNew;
			}
			set
			{
				this.m_OperateNew = value;
			}
		}

		public int consumerID
		{
			get
			{
				return this.m_consumerID;
			}
			set
			{
				this.m_consumerID = value;
			}
		}

		public string curGroup
		{
			get
			{
				return this.m_curGroup;
			}
			set
			{
				this.m_curGroup = value;
			}
		}

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmUser));
			this.timer1 = new Timer(this.components);
			this.openFileDialog1 = new OpenFileDialog();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.btnAddNext = new Button();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.grpbMainInfo = new GroupBox();
			this.label8 = new Label();
			this.txtf_CardNO = new MaskedTextBox();
			this.btnSelectPhoto = new Button();
			this.txtf_ConsumerName = new TextBox();
			this.txtf_ConsumerNO = new TextBox();
			this.cbof_GroupID = new ComboBox();
			this.pictureBox1 = new PictureBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.grpbAccessControl = new GroupBox();
			this.txtf_PIN = new MaskedTextBox();
			this.dtpDeactivate = new DateTimePicker();
			this.dtpActivate = new DateTimePicker();
			this.label5 = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.grpbAttendance = new GroupBox();
			this.optShift = new RadioButton();
			this.optNormal = new RadioButton();
			this.chkDoorEnabled = new CheckBox();
			this.chkAttendance = new CheckBox();
			this.tabPage2 = new TabPage();
			this.txtf_Sex = new ComboBox();
			this.txtf_Addr = new TextBox();
			this.Label18 = new Label();
			this.Label16 = new Label();
			this.Label20 = new Label();
			this.Label21 = new Label();
			this.txtf_Postcode = new TextBox();
			this.txtf_JoinDate = new TextBox();
			this.txtf_LeaveDate = new TextBox();
			this.Label13 = new Label();
			this.txtf_Political = new TextBox();
			this.Label14 = new Label();
			this.txtf_CertificateType = new TextBox();
			this.Label15 = new Label();
			this.txtf_CertificateID = new TextBox();
			this.Label17 = new Label();
			this.txtf_Telephone = new TextBox();
			this.Label19 = new Label();
			this.txtf_Mobile = new TextBox();
			this.Label22 = new Label();
			this.txtf_Email = new TextBox();
			this.Label23 = new Label();
			this.Label24 = new Label();
			this.txtf_Culture = new TextBox();
			this.txtf_TechGrade = new TextBox();
			this.Label25 = new Label();
			this.Label26 = new Label();
			this.Label27 = new Label();
			this.txtf_Hometown = new TextBox();
			this.txtf_Title = new TextBox();
			this.txtf_CorporationName = new TextBox();
			this.Label28 = new Label();
			this.txtf_Birthday = new TextBox();
			this.Label30 = new Label();
			this.txtf_Marriage = new TextBox();
			this.Label31 = new Label();
			this.txtf_SocialInsuranceNo = new TextBox();
			this.Label32 = new Label();
			this.Label33 = new Label();
			this.txtf_HomePhone = new TextBox();
			this.Label34 = new Label();
			this.txtf_Nationality = new TextBox();
			this.Label35 = new Label();
			this.txtf_Religion = new TextBox();
			this.txtf_EnglishName = new TextBox();
			this.Label36 = new Label();
			this.txtf_Note = new TextBox();
			this.Label37 = new Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.grpbMainInfo.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.grpbAccessControl.SuspendLayout();
			this.grpbAttendance.SuspendLayout();
			this.tabPage2.SuspendLayout();
			base.SuspendLayout();
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnAddNext.BackColor = Color.Transparent;
			this.btnAddNext.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddNext, "btnAddNext");
			this.btnAddNext.ForeColor = Color.White;
			this.btnAddNext.Name = "btnAddNext";
			this.btnAddNext.UseVisualStyleBackColor = false;
			this.btnAddNext.Click += new EventHandler(this.btnAddNext_Click);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabPage1.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.grpbMainInfo);
			this.tabPage1.Controls.Add(this.grpbAccessControl);
			this.tabPage1.Controls.Add(this.grpbAttendance);
			this.tabPage1.Controls.Add(this.chkDoorEnabled);
			this.tabPage1.Controls.Add(this.chkAttendance);
			this.tabPage1.ForeColor = Color.White;
			this.tabPage1.Name = "tabPage1";
			this.grpbMainInfo.Controls.Add(this.label8);
			this.grpbMainInfo.Controls.Add(this.txtf_CardNO);
			this.grpbMainInfo.Controls.Add(this.btnSelectPhoto);
			this.grpbMainInfo.Controls.Add(this.txtf_ConsumerName);
			this.grpbMainInfo.Controls.Add(this.txtf_ConsumerNO);
			this.grpbMainInfo.Controls.Add(this.cbof_GroupID);
			this.grpbMainInfo.Controls.Add(this.pictureBox1);
			this.grpbMainInfo.Controls.Add(this.label1);
			this.grpbMainInfo.Controls.Add(this.label2);
			this.grpbMainInfo.Controls.Add(this.label3);
			this.grpbMainInfo.Controls.Add(this.label4);
			componentResourceManager.ApplyResources(this.grpbMainInfo, "grpbMainInfo");
			this.grpbMainInfo.Name = "grpbMainInfo";
			this.grpbMainInfo.TabStop = false;
			componentResourceManager.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			componentResourceManager.ApplyResources(this.txtf_CardNO, "txtf_CardNO");
			this.txtf_CardNO.Name = "txtf_CardNO";
			this.txtf_CardNO.TextChanged += new EventHandler(this.txtf_CardNO_TextChanged);
			this.txtf_CardNO.KeyPress += new KeyPressEventHandler(this.txtf_CardNO_KeyPress);
			this.txtf_CardNO.KeyUp += new KeyEventHandler(this.txtf_CardNO_KeyUp);
			this.btnSelectPhoto.BackColor = Color.Transparent;
			this.btnSelectPhoto.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSelectPhoto, "btnSelectPhoto");
			this.btnSelectPhoto.ForeColor = Color.White;
			this.btnSelectPhoto.Name = "btnSelectPhoto";
			this.btnSelectPhoto.UseVisualStyleBackColor = false;
			this.btnSelectPhoto.Click += new EventHandler(this.btnSelectPhoto_Click);
			componentResourceManager.ApplyResources(this.txtf_ConsumerName, "txtf_ConsumerName");
			this.txtf_ConsumerName.Name = "txtf_ConsumerName";
			componentResourceManager.ApplyResources(this.txtf_ConsumerNO, "txtf_ConsumerNO");
			this.txtf_ConsumerNO.Name = "txtf_ConsumerNO";
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.Name = "cbof_GroupID";
			componentResourceManager.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			this.grpbAccessControl.Controls.Add(this.txtf_PIN);
			this.grpbAccessControl.Controls.Add(this.dtpDeactivate);
			this.grpbAccessControl.Controls.Add(this.dtpActivate);
			this.grpbAccessControl.Controls.Add(this.label5);
			this.grpbAccessControl.Controls.Add(this.label7);
			this.grpbAccessControl.Controls.Add(this.label6);
			componentResourceManager.ApplyResources(this.grpbAccessControl, "grpbAccessControl");
			this.grpbAccessControl.Name = "grpbAccessControl";
			this.grpbAccessControl.TabStop = false;
			componentResourceManager.ApplyResources(this.txtf_PIN, "txtf_PIN");
			this.txtf_PIN.Name = "txtf_PIN";
			this.txtf_PIN.PasswordChar = '*';
			componentResourceManager.ApplyResources(this.dtpDeactivate, "dtpDeactivate");
			this.dtpDeactivate.Name = "dtpDeactivate";
			this.dtpDeactivate.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			componentResourceManager.ApplyResources(this.dtpActivate, "dtpActivate");
			this.dtpActivate.Name = "dtpActivate";
			this.dtpActivate.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			componentResourceManager.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			componentResourceManager.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			this.grpbAttendance.Controls.Add(this.optShift);
			this.grpbAttendance.Controls.Add(this.optNormal);
			componentResourceManager.ApplyResources(this.grpbAttendance, "grpbAttendance");
			this.grpbAttendance.Name = "grpbAttendance";
			this.grpbAttendance.TabStop = false;
			componentResourceManager.ApplyResources(this.optShift, "optShift");
			this.optShift.Name = "optShift";
			this.optShift.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNormal, "optNormal");
			this.optNormal.Checked = true;
			this.optNormal.Name = "optNormal";
			this.optNormal.TabStop = true;
			this.optNormal.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkDoorEnabled, "chkDoorEnabled");
			this.chkDoorEnabled.Checked = true;
			this.chkDoorEnabled.CheckState = CheckState.Checked;
			this.chkDoorEnabled.Name = "chkDoorEnabled";
			this.chkDoorEnabled.UseVisualStyleBackColor = true;
			this.chkDoorEnabled.CheckedChanged += new EventHandler(this.chkDoorEnabled_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkAttendance, "chkAttendance");
			this.chkAttendance.Checked = true;
			this.chkAttendance.CheckState = CheckState.Checked;
			this.chkAttendance.Name = "chkAttendance";
			this.chkAttendance.UseVisualStyleBackColor = true;
			this.chkAttendance.CheckedChanged += new EventHandler(this.chkAttendance_CheckedChanged);
			this.tabPage2.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Controls.Add(this.txtf_Sex);
			this.tabPage2.Controls.Add(this.txtf_Addr);
			this.tabPage2.Controls.Add(this.Label18);
			this.tabPage2.Controls.Add(this.Label16);
			this.tabPage2.Controls.Add(this.Label20);
			this.tabPage2.Controls.Add(this.Label21);
			this.tabPage2.Controls.Add(this.txtf_Postcode);
			this.tabPage2.Controls.Add(this.txtf_JoinDate);
			this.tabPage2.Controls.Add(this.txtf_LeaveDate);
			this.tabPage2.Controls.Add(this.Label13);
			this.tabPage2.Controls.Add(this.txtf_Political);
			this.tabPage2.Controls.Add(this.Label14);
			this.tabPage2.Controls.Add(this.txtf_CertificateType);
			this.tabPage2.Controls.Add(this.Label15);
			this.tabPage2.Controls.Add(this.txtf_CertificateID);
			this.tabPage2.Controls.Add(this.Label17);
			this.tabPage2.Controls.Add(this.txtf_Telephone);
			this.tabPage2.Controls.Add(this.Label19);
			this.tabPage2.Controls.Add(this.txtf_Mobile);
			this.tabPage2.Controls.Add(this.Label22);
			this.tabPage2.Controls.Add(this.txtf_Email);
			this.tabPage2.Controls.Add(this.Label23);
			this.tabPage2.Controls.Add(this.Label24);
			this.tabPage2.Controls.Add(this.txtf_Culture);
			this.tabPage2.Controls.Add(this.txtf_TechGrade);
			this.tabPage2.Controls.Add(this.Label25);
			this.tabPage2.Controls.Add(this.Label26);
			this.tabPage2.Controls.Add(this.Label27);
			this.tabPage2.Controls.Add(this.txtf_Hometown);
			this.tabPage2.Controls.Add(this.txtf_Title);
			this.tabPage2.Controls.Add(this.txtf_CorporationName);
			this.tabPage2.Controls.Add(this.Label28);
			this.tabPage2.Controls.Add(this.txtf_Birthday);
			this.tabPage2.Controls.Add(this.Label30);
			this.tabPage2.Controls.Add(this.txtf_Marriage);
			this.tabPage2.Controls.Add(this.Label31);
			this.tabPage2.Controls.Add(this.txtf_SocialInsuranceNo);
			this.tabPage2.Controls.Add(this.Label32);
			this.tabPage2.Controls.Add(this.Label33);
			this.tabPage2.Controls.Add(this.txtf_HomePhone);
			this.tabPage2.Controls.Add(this.Label34);
			this.tabPage2.Controls.Add(this.txtf_Nationality);
			this.tabPage2.Controls.Add(this.Label35);
			this.tabPage2.Controls.Add(this.txtf_Religion);
			this.tabPage2.Controls.Add(this.txtf_EnglishName);
			this.tabPage2.Controls.Add(this.Label36);
			this.tabPage2.Controls.Add(this.txtf_Note);
			this.tabPage2.Controls.Add(this.Label37);
			this.tabPage2.ForeColor = Color.White;
			this.tabPage2.Name = "tabPage2";
			componentResourceManager.ApplyResources(this.txtf_Sex, "txtf_Sex");
			this.txtf_Sex.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("txtf_Sex.Items"),
				componentResourceManager.GetString("txtf_Sex.Items1")
			});
			this.txtf_Sex.Name = "txtf_Sex";
			componentResourceManager.ApplyResources(this.txtf_Addr, "txtf_Addr");
			this.txtf_Addr.Name = "txtf_Addr";
			componentResourceManager.ApplyResources(this.Label18, "Label18");
			this.Label18.Name = "Label18";
			componentResourceManager.ApplyResources(this.Label16, "Label16");
			this.Label16.Name = "Label16";
			componentResourceManager.ApplyResources(this.Label20, "Label20");
			this.Label20.Name = "Label20";
			componentResourceManager.ApplyResources(this.Label21, "Label21");
			this.Label21.Name = "Label21";
			componentResourceManager.ApplyResources(this.txtf_Postcode, "txtf_Postcode");
			this.txtf_Postcode.Name = "txtf_Postcode";
			componentResourceManager.ApplyResources(this.txtf_JoinDate, "txtf_JoinDate");
			this.txtf_JoinDate.Name = "txtf_JoinDate";
			componentResourceManager.ApplyResources(this.txtf_LeaveDate, "txtf_LeaveDate");
			this.txtf_LeaveDate.Name = "txtf_LeaveDate";
			componentResourceManager.ApplyResources(this.Label13, "Label13");
			this.Label13.Name = "Label13";
			componentResourceManager.ApplyResources(this.txtf_Political, "txtf_Political");
			this.txtf_Political.Name = "txtf_Political";
			componentResourceManager.ApplyResources(this.Label14, "Label14");
			this.Label14.Name = "Label14";
			componentResourceManager.ApplyResources(this.txtf_CertificateType, "txtf_CertificateType");
			this.txtf_CertificateType.Name = "txtf_CertificateType";
			componentResourceManager.ApplyResources(this.Label15, "Label15");
			this.Label15.Name = "Label15";
			componentResourceManager.ApplyResources(this.txtf_CertificateID, "txtf_CertificateID");
			this.txtf_CertificateID.Name = "txtf_CertificateID";
			componentResourceManager.ApplyResources(this.Label17, "Label17");
			this.Label17.Name = "Label17";
			componentResourceManager.ApplyResources(this.txtf_Telephone, "txtf_Telephone");
			this.txtf_Telephone.Name = "txtf_Telephone";
			componentResourceManager.ApplyResources(this.Label19, "Label19");
			this.Label19.Name = "Label19";
			componentResourceManager.ApplyResources(this.txtf_Mobile, "txtf_Mobile");
			this.txtf_Mobile.Name = "txtf_Mobile";
			componentResourceManager.ApplyResources(this.Label22, "Label22");
			this.Label22.Name = "Label22";
			componentResourceManager.ApplyResources(this.txtf_Email, "txtf_Email");
			this.txtf_Email.Name = "txtf_Email";
			componentResourceManager.ApplyResources(this.Label23, "Label23");
			this.Label23.Name = "Label23";
			componentResourceManager.ApplyResources(this.Label24, "Label24");
			this.Label24.Name = "Label24";
			componentResourceManager.ApplyResources(this.txtf_Culture, "txtf_Culture");
			this.txtf_Culture.Name = "txtf_Culture";
			componentResourceManager.ApplyResources(this.txtf_TechGrade, "txtf_TechGrade");
			this.txtf_TechGrade.Name = "txtf_TechGrade";
			componentResourceManager.ApplyResources(this.Label25, "Label25");
			this.Label25.Name = "Label25";
			componentResourceManager.ApplyResources(this.Label26, "Label26");
			this.Label26.Name = "Label26";
			componentResourceManager.ApplyResources(this.Label27, "Label27");
			this.Label27.Name = "Label27";
			componentResourceManager.ApplyResources(this.txtf_Hometown, "txtf_Hometown");
			this.txtf_Hometown.Name = "txtf_Hometown";
			componentResourceManager.ApplyResources(this.txtf_Title, "txtf_Title");
			this.txtf_Title.Name = "txtf_Title";
			componentResourceManager.ApplyResources(this.txtf_CorporationName, "txtf_CorporationName");
			this.txtf_CorporationName.Name = "txtf_CorporationName";
			componentResourceManager.ApplyResources(this.Label28, "Label28");
			this.Label28.Name = "Label28";
			componentResourceManager.ApplyResources(this.txtf_Birthday, "txtf_Birthday");
			this.txtf_Birthday.Name = "txtf_Birthday";
			componentResourceManager.ApplyResources(this.Label30, "Label30");
			this.Label30.Name = "Label30";
			componentResourceManager.ApplyResources(this.txtf_Marriage, "txtf_Marriage");
			this.txtf_Marriage.Name = "txtf_Marriage";
			componentResourceManager.ApplyResources(this.Label31, "Label31");
			this.Label31.Name = "Label31";
			componentResourceManager.ApplyResources(this.txtf_SocialInsuranceNo, "txtf_SocialInsuranceNo");
			this.txtf_SocialInsuranceNo.Name = "txtf_SocialInsuranceNo";
			componentResourceManager.ApplyResources(this.Label32, "Label32");
			this.Label32.Name = "Label32";
			componentResourceManager.ApplyResources(this.Label33, "Label33");
			this.Label33.Name = "Label33";
			componentResourceManager.ApplyResources(this.txtf_HomePhone, "txtf_HomePhone");
			this.txtf_HomePhone.Name = "txtf_HomePhone";
			componentResourceManager.ApplyResources(this.Label34, "Label34");
			this.Label34.Name = "Label34";
			componentResourceManager.ApplyResources(this.txtf_Nationality, "txtf_Nationality");
			this.txtf_Nationality.Name = "txtf_Nationality";
			componentResourceManager.ApplyResources(this.Label35, "Label35");
			this.Label35.Name = "Label35";
			componentResourceManager.ApplyResources(this.txtf_Religion, "txtf_Religion");
			this.txtf_Religion.Name = "txtf_Religion";
			componentResourceManager.ApplyResources(this.txtf_EnglishName, "txtf_EnglishName");
			this.txtf_EnglishName.Name = "txtf_EnglishName";
			componentResourceManager.ApplyResources(this.Label36, "Label36");
			this.Label36.Name = "Label36";
			componentResourceManager.ApplyResources(this.txtf_Note, "txtf_Note");
			this.txtf_Note.Name = "txtf_Note";
			componentResourceManager.ApplyResources(this.Label37, "Label37");
			this.Label37.Name = "Label37";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnAddNext);
			base.Controls.Add(this.tabControl1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmUser";
			base.FormClosing += new FormClosingEventHandler(this.dfrmUser_FormClosing);
			base.Load += new EventHandler(this.dfrmUser_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.grpbMainInfo.ResumeLayout(false);
			this.grpbMainInfo.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.grpbAccessControl.ResumeLayout(false);
			this.grpbAccessControl.PerformLayout();
			this.grpbAttendance.ResumeLayout(false);
			this.grpbAttendance.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			base.ResumeLayout(false);
		}

		public dfrmUser()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				int num;
				if (this.m_OperateNew)
				{
					num = this.AddUser();
				}
				else
				{
					num = this.EditUser();
				}
				if (num < 0)
				{
					XMessageBox.Show(this, icConsumer.getErrInfo(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					this.savePhoto();
					icConsumerShare.setUpdateLog();
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnAddNext_Click(object sender, EventArgs e)
		{
			int num = this.AddUser();
			if (num < 0)
			{
				XMessageBox.Show(this, icConsumer.getErrInfo(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.savePhoto();
			icConsumerShare.setUpdateLog();
			long num2 = 0L;
			long.TryParse(this.txtf_ConsumerNO.Text, out num2);
			if (num2 > 0L)
			{
				this.txtf_ConsumerNO.Text = (num2 + 1L).ToString();
			}
			else if (!string.IsNullOrEmpty(this.strStartCaption) && this.txtf_ConsumerNO.Text.StartsWith(this.strStartCaption))
			{
				string s = this.txtf_ConsumerNO.Text.Substring(this.txtf_ConsumerNO.Text.IndexOf(this.strStartCaption) + this.strStartCaption.Length);
				long num3;
				if (long.TryParse(s, out num3))
				{
					num3 += 1L;
					string text;
					if (string.IsNullOrEmpty(this.strStartCaption))
					{
						text = num3.ToString();
					}
					else if (this.userIDlen - this.strStartCaption.Length > 0)
					{
						text = string.Format("{0}{1}", this.strStartCaption, num3.ToString().PadLeft(this.userIDlen - this.strStartCaption.Length, '0'));
					}
					else
					{
						text = string.Format("{0}{1}", this.strStartCaption, num3.ToString());
					}
					this.txtf_ConsumerNO.Text = text;
				}
				else
				{
					this.txtf_ConsumerNO.Text = "";
				}
			}
			else
			{
				this.txtf_ConsumerNO.Text = "";
			}
			this.txtf_ConsumerName.Text = "";
			this.txtf_CardNO.Text = "";
			this.txtf_CardNO.Focus();
			this.bContinued = true;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (this.bContinued)
			{
				base.DialogResult = DialogResult.OK;
			}
			base.Close();
		}

		private void loadUserAutoAdd()
		{
			try
			{
				this.strUserAutoAddSet = wgAppConfig.GetKeyVal("UserAutoAddSet");
				if (!string.IsNullOrEmpty(this.strUserAutoAddSet) && this.strUserAutoAddSet.IndexOf(",") > 0)
				{
					string s = this.strUserAutoAddSet.Substring(0, this.strUserAutoAddSet.IndexOf(","));
					string objToStr = this.strUserAutoAddSet.Substring(this.strUserAutoAddSet.IndexOf(",") + 1);
					if (int.Parse(s) > 0)
					{
						this.userIDlen = int.Parse(s);
					}
					this.strStartCaption = wgTools.SetObjToStr(objToStr);
				}
			}
			catch (Exception)
			{
			}
		}

		private void dfrmUser_Load(object sender, EventArgs e)
		{
			this.txtf_CardNO.Mask = "9999999999";
			this.txtf_PIN.Mask = "999999";
			this.txtf_PIN.Text = 345678.ToString();
			this.dtpActivate.Value = DateTime.Now.Date;
			this.tabPage1.BackColor = this.BackColor;
			this.tabPage2.BackColor = this.BackColor;
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.label1.Text = wgAppConfig.ReplaceWorkNO(this.label1.Text);
			icGroup icGroup = new icGroup();
			icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
			int i = this.arrGroupID.Count;
			for (i = 0; i < this.arrGroupID.Count; i++)
			{
				this.cbof_GroupID.Items.Add(this.arrGroupName[i].ToString());
			}
			if (this.cbof_GroupID.Items.Count > 0)
			{
				this.cbof_GroupID.SelectedIndex = 0;
			}
			this.loadUserAutoAdd();
			if (this.m_OperateNew)
			{
				this.loadData4New();
				this.btnAddNext.Visible = true;
			}
			else
			{
				this.loadData4Edit();
				this.btnAddNext.Visible = false;
			}
			this.chkAttendance_CheckedChanged(null, null);
			this.chkDoorEnabled_CheckedChanged(null, null);
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.grpbAttendance.Visible = true;
			}
			else
			{
				this.optNormal.Checked = true;
				this.grpbAttendance.Visible = false;
			}
			this.label7.Visible = wgAppConfig.getParamValBoolByNO(123);
			this.txtf_PIN.Visible = wgAppConfig.getParamValBoolByNO(123);
			wgAppConfig.setDisplayFormatDate(this.dtpActivate, wgTools.DisplayFormat_DateYMD);
			wgAppConfig.setDisplayFormatDate(this.dtpDeactivate, wgTools.DisplayFormat_DateYMD);
		}

		private void loadData4New()
		{
			try
			{
				icConsumer icConsumer = new icConsumer();
				long num = icConsumer.ConsumerNONext(this.strStartCaption);
				if (num < 0L)
				{
					num = 1L;
				}
				string text;
				if (string.IsNullOrEmpty(this.strStartCaption))
				{
					text = num.ToString();
				}
				else if (this.userIDlen - this.strStartCaption.Length > 0)
				{
					text = string.Format("{0}{1}", this.strStartCaption, num.ToString().PadLeft(this.userIDlen - this.strStartCaption.Length, '0'));
				}
				else
				{
					text = string.Format("{0}{1}", this.strStartCaption, num.ToString());
				}
				this.txtf_ConsumerNO.Text = text;
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void loadData4Edit()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadData4Edit_Acc();
				return;
			}
			SqlConnection sqlConnection = null;
			SqlCommand sqlCommand = null;
			try
			{
				sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				sqlCommand = new SqlCommand("", sqlConnection);
				try
				{
					string text = " SELECT  t_b_Consumer.*,  f_GroupName ";
					text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
					text = text + " where [f_ConsumerID]= " + this.m_consumerID.ToString();
					sqlCommand.CommandText = text;
					sqlCommand.Connection = sqlConnection;
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);
					if (sqlDataReader.Read())
					{
						this.chkDoorEnabled.Checked = ((byte)sqlDataReader["f_DoorEnabled"] > 0);
						this.chkAttendance.Checked = ((byte)sqlDataReader["f_AttendEnabled"] > 0);
						this.txtf_ConsumerNO.Text = wgTools.SetObjToStr(sqlDataReader["f_ConsumerNO"]);
						this.txtf_ConsumerName.Text = wgTools.SetObjToStr(sqlDataReader["f_ConsumerName"]);
						this.txtf_CardNO.Text = wgTools.SetObjToStr(sqlDataReader["f_CardNO"]);
						if (this.txtf_CardNO.Text != "")
						{
							this.txtf_CardNO.ReadOnly = true;
							this.txtf_CardNO.Cursor = Cursors.Arrow;
						}
						this.txtf_PIN.Text = wgTools.SetObjToStr(sqlDataReader["f_PIN"]);
						this.dtpActivate.Value = (DateTime)sqlDataReader["f_BeginYMD"];
						this.dtpDeactivate.Value = (DateTime)sqlDataReader["f_EndYMD"];
						this.m_curGroup = wgTools.SetObjToStr(sqlDataReader["f_GroupName"]);
						this.cbof_GroupID.Text = this.m_curGroup;
						this.optNormal.Checked = true;
						this.optShift.Checked = ((byte)sqlDataReader["f_ShiftEnabled"] > 0);
					}
					sqlDataReader.Close();
					text = " SELECT  * ";
					text += " FROM t_b_Consumer_Other  ";
					text = text + " where [f_ConsumerID]= " + this.m_consumerID.ToString();
					sqlCommand.CommandText = text;
					sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);
					if (sqlDataReader.Read())
					{
						this.txtf_Title.Text = wgTools.SetObjToStr(sqlDataReader["f_Title"]);
						this.txtf_Culture.Text = wgTools.SetObjToStr(sqlDataReader["f_Culture"]);
						this.txtf_Hometown.Text = wgTools.SetObjToStr(sqlDataReader["f_Hometown"]);
						this.txtf_Birthday.Text = wgTools.SetObjToStr(sqlDataReader["f_Birthday"]);
						this.txtf_Marriage.Text = wgTools.SetObjToStr(sqlDataReader["f_Marriage"]);
						this.txtf_JoinDate.Text = wgTools.SetObjToStr(sqlDataReader["f_JoinDate"]);
						this.txtf_LeaveDate.Text = wgTools.SetObjToStr(sqlDataReader["f_LeaveDate"]);
						this.txtf_CertificateType.Text = wgTools.SetObjToStr(sqlDataReader["f_CertificateType"]);
						this.txtf_CertificateID.Text = wgTools.SetObjToStr(sqlDataReader["f_CertificateID"]);
						this.txtf_SocialInsuranceNo.Text = wgTools.SetObjToStr(sqlDataReader["f_SocialInsuranceNo"]);
						this.txtf_Addr.Text = wgTools.SetObjToStr(sqlDataReader["f_Addr"]);
						this.txtf_Postcode.Text = wgTools.SetObjToStr(sqlDataReader["f_Postcode"]);
						this.txtf_Sex.Text = wgTools.SetObjToStr(sqlDataReader["f_Sex"]);
						this.txtf_Nationality.Text = wgTools.SetObjToStr(sqlDataReader["f_Nationality"]);
						this.txtf_Religion.Text = wgTools.SetObjToStr(sqlDataReader["f_Religion"]);
						this.txtf_EnglishName.Text = wgTools.SetObjToStr(sqlDataReader["f_EnglishName"]);
						this.txtf_Mobile.Text = wgTools.SetObjToStr(sqlDataReader["f_Mobile"]);
						this.txtf_HomePhone.Text = wgTools.SetObjToStr(sqlDataReader["f_HomePhone"]);
						this.txtf_Telephone.Text = wgTools.SetObjToStr(sqlDataReader["f_Telephone"]);
						this.txtf_Email.Text = wgTools.SetObjToStr(sqlDataReader["f_Email"]);
						this.txtf_Political.Text = wgTools.SetObjToStr(sqlDataReader["f_Political"]);
						this.txtf_CorporationName.Text = wgTools.SetObjToStr(sqlDataReader["f_CorporationName"]);
						this.txtf_TechGrade.Text = wgTools.SetObjToStr(sqlDataReader["f_TechGrade"]);
						this.txtf_Note.Text = wgTools.SetObjToStr(sqlDataReader["f_Note"]);
					}
					sqlDataReader.Close();
					this.loadPhoto();
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
				}
				finally
				{
					if (sqlCommand != null)
					{
						sqlCommand.Dispose();
					}
					if (sqlConnection.State != ConnectionState.Closed)
					{
						sqlConnection.Close();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void loadData4Edit_Acc()
		{
			OleDbConnection oleDbConnection = null;
			OleDbCommand oleDbCommand = null;
			try
			{
				oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				oleDbCommand = new OleDbCommand("", oleDbConnection);
				try
				{
					string text = " SELECT  t_b_Consumer.*,  f_GroupName ";
					text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
					text = text + " where [f_ConsumerID]= " + this.m_consumerID.ToString();
					oleDbCommand.CommandText = text;
					oleDbCommand.Connection = oleDbConnection;
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.Default);
					if (oleDbDataReader.Read())
					{
						this.chkDoorEnabled.Checked = ((byte)oleDbDataReader["f_DoorEnabled"] > 0);
						this.chkAttendance.Checked = ((byte)oleDbDataReader["f_AttendEnabled"] > 0);
						this.txtf_ConsumerNO.Text = wgTools.SetObjToStr(oleDbDataReader["f_ConsumerNO"]);
						this.txtf_ConsumerName.Text = wgTools.SetObjToStr(oleDbDataReader["f_ConsumerName"]);
						this.txtf_CardNO.Text = wgTools.SetObjToStr(oleDbDataReader["f_CardNO"]);
						if (this.txtf_CardNO.Text != "")
						{
							this.txtf_CardNO.ReadOnly = true;
						}
						this.txtf_PIN.Text = wgTools.SetObjToStr(oleDbDataReader["f_PIN"]);
						this.dtpActivate.Value = (DateTime)oleDbDataReader["f_BeginYMD"];
						this.dtpDeactivate.Value = (DateTime)oleDbDataReader["f_EndYMD"];
						this.m_curGroup = wgTools.SetObjToStr(oleDbDataReader["f_GroupName"]);
						this.cbof_GroupID.Text = this.m_curGroup;
						this.optNormal.Checked = true;
						this.optShift.Checked = ((byte)oleDbDataReader["f_ShiftEnabled"] > 0);
					}
					oleDbDataReader.Close();
					text = " SELECT  * ";
					text += " FROM t_b_Consumer_Other  ";
					text = text + " where [f_ConsumerID]= " + this.m_consumerID.ToString();
					oleDbCommand.CommandText = text;
					oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.Default);
					if (oleDbDataReader.Read())
					{
						this.txtf_Title.Text = wgTools.SetObjToStr(oleDbDataReader["f_Title"]);
						this.txtf_Culture.Text = wgTools.SetObjToStr(oleDbDataReader["f_Culture"]);
						this.txtf_Hometown.Text = wgTools.SetObjToStr(oleDbDataReader["f_Hometown"]);
						this.txtf_Birthday.Text = wgTools.SetObjToStr(oleDbDataReader["f_Birthday"]);
						this.txtf_Marriage.Text = wgTools.SetObjToStr(oleDbDataReader["f_Marriage"]);
						this.txtf_JoinDate.Text = wgTools.SetObjToStr(oleDbDataReader["f_JoinDate"]);
						this.txtf_LeaveDate.Text = wgTools.SetObjToStr(oleDbDataReader["f_LeaveDate"]);
						this.txtf_CertificateType.Text = wgTools.SetObjToStr(oleDbDataReader["f_CertificateType"]);
						this.txtf_CertificateID.Text = wgTools.SetObjToStr(oleDbDataReader["f_CertificateID"]);
						this.txtf_SocialInsuranceNo.Text = wgTools.SetObjToStr(oleDbDataReader["f_SocialInsuranceNo"]);
						this.txtf_Addr.Text = wgTools.SetObjToStr(oleDbDataReader["f_Addr"]);
						this.txtf_Postcode.Text = wgTools.SetObjToStr(oleDbDataReader["f_Postcode"]);
						this.txtf_Sex.Text = wgTools.SetObjToStr(oleDbDataReader["f_Sex"]);
						this.txtf_Nationality.Text = wgTools.SetObjToStr(oleDbDataReader["f_Nationality"]);
						this.txtf_Religion.Text = wgTools.SetObjToStr(oleDbDataReader["f_Religion"]);
						this.txtf_EnglishName.Text = wgTools.SetObjToStr(oleDbDataReader["f_EnglishName"]);
						this.txtf_Mobile.Text = wgTools.SetObjToStr(oleDbDataReader["f_Mobile"]);
						this.txtf_HomePhone.Text = wgTools.SetObjToStr(oleDbDataReader["f_HomePhone"]);
						this.txtf_Telephone.Text = wgTools.SetObjToStr(oleDbDataReader["f_Telephone"]);
						this.txtf_Email.Text = wgTools.SetObjToStr(oleDbDataReader["f_Email"]);
						this.txtf_Political.Text = wgTools.SetObjToStr(oleDbDataReader["f_Political"]);
						this.txtf_CorporationName.Text = wgTools.SetObjToStr(oleDbDataReader["f_CorporationName"]);
						this.txtf_TechGrade.Text = wgTools.SetObjToStr(oleDbDataReader["f_TechGrade"]);
						this.txtf_Note.Text = wgTools.SetObjToStr(oleDbDataReader["f_Note"]);
					}
					oleDbDataReader.Close();
					this.loadPhoto();
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
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
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void loadPhoto()
		{
			try
			{
				string fileToDisplay;
				if (this.txtf_CardNO.Text.Trim() == "")
				{
					fileToDisplay = null;
				}
				else
				{
					fileToDisplay = wgAppConfig.getPhotoFileName(long.Parse(this.txtf_CardNO.Text));
				}
				Image image = this.pictureBox1.Image;
				wgAppConfig.ShowMyImage(fileToDisplay, ref image);
				this.pictureBox1.Image = image;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private int AddUser()
		{
			icConsumer icConsumer = new icConsumer();
			int num = icConsumer.addNew(this.txtf_ConsumerNO.Text, this.txtf_ConsumerName.Text, int.Parse(this.arrGroupID[this.cbof_GroupID.SelectedIndex].ToString()), this.chkAttendance.Checked ? 1 : 0, this.optNormal.Checked ? 0 : 1, this.chkDoorEnabled.Checked ? 1 : 0, this.dtpActivate.Value, this.dtpDeactivate.Value, (this.txtf_PIN.Text == "") ? 0 : int.Parse(this.txtf_PIN.Text), (this.txtf_CardNO.Text == "") ? 0L : long.Parse(this.txtf_CardNO.Text));
			if (num >= 0)
			{
				icConsumer.editUserOtherInfo(icConsumer.gConsumerID, this.txtf_Title.Text, this.txtf_Culture.Text, this.txtf_Hometown.Text, this.txtf_Birthday.Text, this.txtf_Marriage.Text, this.txtf_JoinDate.Text, this.txtf_LeaveDate.Text, this.txtf_CertificateType.Text, this.txtf_CertificateID.Text, this.txtf_SocialInsuranceNo.Text, this.txtf_Addr.Text, this.txtf_Postcode.Text, this.txtf_Sex.Text, this.txtf_Nationality.Text, this.txtf_Religion.Text, this.txtf_EnglishName.Text, this.txtf_Mobile.Text, this.txtf_HomePhone.Text, this.txtf_Telephone.Text, this.txtf_Email.Text, this.txtf_Political.Text, this.txtf_CorporationName.Text, this.txtf_TechGrade.Text, this.txtf_Note.Text);
				wgAppConfig.wgLog(string.Format("{0}:{1} [{2}]", CommonStr.strAddUsers, this.txtf_ConsumerName.Text, this.txtf_CardNO.Text), EventLogEntryType.Information, null);
			}
			return num;
		}

		private int EditUser()
		{
			icConsumer icConsumer = new icConsumer();
			int num = icConsumer.editUser(this.m_consumerID, this.txtf_ConsumerNO.Text, this.txtf_ConsumerName.Text, int.Parse(this.arrGroupID[this.cbof_GroupID.SelectedIndex].ToString()), this.chkAttendance.Checked ? 1 : 0, this.optNormal.Checked ? 0 : 1, this.chkDoorEnabled.Checked ? 1 : 0, this.dtpActivate.Value, this.dtpDeactivate.Value, (this.txtf_PIN.Text == "") ? 0 : int.Parse(this.txtf_PIN.Text), (this.txtf_CardNO.Text == "") ? 0L : long.Parse(this.txtf_CardNO.Text));
			if (num >= 0)
			{
				icConsumer.editUserOtherInfo(this.m_consumerID, this.txtf_Title.Text, this.txtf_Culture.Text, this.txtf_Hometown.Text, this.txtf_Birthday.Text, this.txtf_Marriage.Text, this.txtf_JoinDate.Text, this.txtf_LeaveDate.Text, this.txtf_CertificateType.Text, this.txtf_CertificateID.Text, this.txtf_SocialInsuranceNo.Text, this.txtf_Addr.Text, this.txtf_Postcode.Text, this.txtf_Sex.Text, this.txtf_Nationality.Text, this.txtf_Religion.Text, this.txtf_EnglishName.Text, this.txtf_Mobile.Text, this.txtf_HomePhone.Text, this.txtf_Telephone.Text, this.txtf_Email.Text, this.txtf_Political.Text, this.txtf_CorporationName.Text, this.txtf_TechGrade.Text, this.txtf_Note.Text);
			}
			return num;
		}

		private void chkAttendance_CheckedChanged(object sender, EventArgs e)
		{
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.grpbAttendance.Visible = this.chkAttendance.Checked;
			}
		}

		private void chkDoorEnabled_CheckedChanged(object sender, EventArgs e)
		{
			this.grpbAccessControl.Visible = this.chkDoorEnabled.Checked;
		}

		private void txtf_CardNO_TextChanged(object sender, EventArgs e)
		{
			if (this.txtf_CardNO.Text.Length == 1)
			{
				this.timer1.Interval = 500;
				this.timer1.Enabled = true;
			}
			if (this.txtf_CardNO.Text.Length == 0)
			{
				this.btnSelectPhoto.Enabled = false;
				return;
			}
			this.btnSelectPhoto.Enabled = true;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			try
			{
				if (this.txtf_CardNO.Text.Length >= 8)
				{
					this.cbof_GroupID.Focus();
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void txtf_CardNO_KeyPress(object sender, KeyPressEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtf_CardNO);
		}

		private void txtf_CardNO_KeyUp(object sender, KeyEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtf_CardNO);
		}

		private void btnSelectPhoto_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.photoFileName = "";
				}
				catch (Exception ex)
				{
					wgTools.WriteLine(ex.ToString());
				}
				this.openFileDialog1.Filter = " (*.jpg)|*.jpg|(*.bmp)|*.bmp";
				this.openFileDialog1.FilterIndex = 1;
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					this.photoFileName = this.openFileDialog1.FileName;
					Image image = this.pictureBox1.Image;
					wgAppConfig.ShowMyImage(this.photoFileName, ref image);
					this.pictureBox1.Image = image;
				}
			}
			catch (Exception ex2)
			{
				wgTools.WriteLine(ex2.ToString());
				XMessageBox.Show(ex2.ToString());
			}
			Directory.SetCurrentDirectory(Application.StartupPath);
		}

		private void savePhoto()
		{
			if (this.photoFileName == "")
			{
				return;
			}
			if (string.IsNullOrEmpty(this.txtf_CardNO.Text))
			{
				return;
			}
			try
			{
				wgAppConfig.photoDirectoryLastWriteTime = DateTime.Parse("2012-6-12 18:57:08.531");
				if (wgAppConfig.DirectoryIsExisted(wgAppConfig.Path4Photo()))
				{
					string fileName = this.photoFileName;
					FileInfo fileInfo = new FileInfo(fileName);
					FileInfo fileInfo2 = new FileInfo(wgAppConfig.Path4Photo() + this.txtf_CardNO.Text + fileInfo.Extension);
					if (!(fileInfo2.FullName.ToUpper() == this.photoFileName.ToUpper()))
					{
						try
						{
							if (fileInfo2.Exists)
							{
								fileInfo2.Delete();
							}
						}
						catch (Exception ex)
						{
							wgTools.WgDebugWrite(ex.ToString(), new object[0]);
						}
						fileInfo.CopyTo(wgAppConfig.Path4Photo() + this.txtf_CardNO.Text + ".jpg", true);
					}
					this.photoFileName = "";
					this.pictureBox1.Image = null;
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
		}

		private void dfrmUser_FormClosing(object sender, FormClosingEventArgs e)
		{
			wgAppConfig.DisposeImage(this.pictureBox1.Image);
		}
	}
}
