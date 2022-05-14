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
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class dfrmMeetingSign : frmN3000
	{
		public string curMeetingNo = "";

		private IContainer components;

		internal Button btnCancel;

		internal Button Button1;

		internal Button Button2;

		internal Button Button3;

		internal Button Button4;

		internal Button Button5;

		internal GroupBox GroupBox1;

		internal Label Label2;

		internal Label Label3;

		internal Label Label4;

		internal Label Label5;

		internal Label Label6;

		internal GroupBox GroupBox2;

		internal Button Button6;

		internal Button Button7;

		internal GroupBox GroupBox5;

		internal GroupBox GroupBox3;

		internal GroupBox GroupBox4;

		internal GroupBox GroupBox6;

		internal GroupBox GroupBox7;

		internal GroupBox GroupBox8;

		internal Label lblMeetingName;

		internal System.Windows.Forms.Timer Timer1;

		internal Label lblTime;

		internal TextBox txtA0;

		internal TextBox txtA1;

		internal TextBox txtA2;

		internal TextBox txtA3;

		internal TextBox txtA4;

		internal TextBox txtB0;

		internal TextBox txtB1;

		internal TextBox txtB2;

		internal TextBox txtB3;

		internal TextBox txtB4;

		internal TextBox txtC0;

		internal TextBox txtC1;

		internal TextBox txtC2;

		internal TextBox txtC3;

		internal TextBox txtC4;

		internal TextBox txtD0;

		internal TextBox txtD1;

		internal TextBox txtE3;

		internal TextBox txtE4;

		internal TextBox txtE2;

		internal TextBox txtE1;

		internal TextBox txtE0;

		internal TextBox txtD2;

		internal TextBox txtD3;

		internal TextBox txtD4;

		internal System.Windows.Forms.Timer Timer2;

		internal PictureBox picSwipe1;

		internal PictureBox picSwipe6;

		internal TextBox txtSwipeUser6;

		internal TextBox txtSwipeSeat6;

		internal PictureBox picSwipe5;

		internal TextBox txtSwipeUser5;

		internal TextBox txtSwipeSeat5;

		internal PictureBox picSwipe4;

		internal TextBox txtSwipeUser4;

		internal TextBox txtSwipeSeat4;

		internal PictureBox picSwipe3;

		internal TextBox txtSwipeUser3;

		internal TextBox txtSwipeSeat3;

		internal PictureBox picSwipe2;

		internal TextBox txtSwipeUser2;

		internal TextBox txtSwipeSeat2;

		internal TextBox txtSwipeUser1;

		internal TextBox txtSwipeSeat1;

		internal Button btnErrConnect;

		private FlowLayoutPanel flowLayoutPanel1;

		internal Button button8;

		internal System.Windows.Forms.Timer TimerStartSlow;

		private DataSet ds = new DataSet();

		private long[,] arrMeetingNum = new long[5, 5];

		private DateTime signStarttime;

		private DateTime signEndtime;

		private string meetingAdr = "";

		private ArrayList arrControllerID = new ArrayList();

		private ArrayList arrSignedUser = new ArrayList();

		private ArrayList arrSignedSeat = new ArrayList();

		private ArrayList arrSignedCardNo = new ArrayList();

		public long lngDealtRecordID = -1L;

		private string queryReaderStr = "";

		private Thread startSlowThread;

		private frmConsole frmWatch;

		private int lastinfoRowsCount;

		private int cntTimer2;

		public dfrmMeetingSign()
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMeetingSign));
			this.Timer1 = new System.Windows.Forms.Timer(this.components);
			this.Timer2 = new System.Windows.Forms.Timer(this.components);
			this.TimerStartSlow = new System.Windows.Forms.Timer(this.components);
			this.flowLayoutPanel1 = new FlowLayoutPanel();
			this.GroupBox2 = new GroupBox();
			this.picSwipe1 = new PictureBox();
			this.txtSwipeUser1 = new TextBox();
			this.txtSwipeSeat1 = new TextBox();
			this.GroupBox3 = new GroupBox();
			this.picSwipe2 = new PictureBox();
			this.txtSwipeUser2 = new TextBox();
			this.txtSwipeSeat2 = new TextBox();
			this.GroupBox4 = new GroupBox();
			this.picSwipe3 = new PictureBox();
			this.txtSwipeUser3 = new TextBox();
			this.txtSwipeSeat3 = new TextBox();
			this.GroupBox6 = new GroupBox();
			this.picSwipe4 = new PictureBox();
			this.txtSwipeUser4 = new TextBox();
			this.txtSwipeSeat4 = new TextBox();
			this.GroupBox7 = new GroupBox();
			this.picSwipe5 = new PictureBox();
			this.txtSwipeUser5 = new TextBox();
			this.txtSwipeSeat5 = new TextBox();
			this.lblMeetingName = new Label();
			this.GroupBox5 = new GroupBox();
			this.button8 = new Button();
			this.btnErrConnect = new Button();
			this.btnCancel = new Button();
			this.Button6 = new Button();
			this.Button7 = new Button();
			this.lblTime = new Label();
			this.Button3 = new Button();
			this.Button2 = new Button();
			this.Button1 = new Button();
			this.Button4 = new Button();
			this.Button5 = new Button();
			this.txtA0 = new TextBox();
			this.txtA1 = new TextBox();
			this.txtA2 = new TextBox();
			this.txtA3 = new TextBox();
			this.txtA4 = new TextBox();
			this.Label2 = new Label();
			this.Label3 = new Label();
			this.txtB0 = new TextBox();
			this.txtB1 = new TextBox();
			this.txtB2 = new TextBox();
			this.txtB3 = new TextBox();
			this.txtB4 = new TextBox();
			this.Label4 = new Label();
			this.txtC0 = new TextBox();
			this.txtC1 = new TextBox();
			this.txtC2 = new TextBox();
			this.txtC3 = new TextBox();
			this.txtC4 = new TextBox();
			this.txtD0 = new TextBox();
			this.txtD1 = new TextBox();
			this.txtD2 = new TextBox();
			this.Label5 = new Label();
			this.txtD3 = new TextBox();
			this.txtD4 = new TextBox();
			this.Label6 = new Label();
			this.txtE3 = new TextBox();
			this.txtE4 = new TextBox();
			this.txtE2 = new TextBox();
			this.txtE1 = new TextBox();
			this.txtE0 = new TextBox();
			this.GroupBox1 = new GroupBox();
			this.GroupBox8 = new GroupBox();
			this.picSwipe6 = new PictureBox();
			this.txtSwipeUser6 = new TextBox();
			this.txtSwipeSeat6 = new TextBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			((ISupportInitialize)this.picSwipe1).BeginInit();
			this.GroupBox3.SuspendLayout();
			((ISupportInitialize)this.picSwipe2).BeginInit();
			this.GroupBox4.SuspendLayout();
			((ISupportInitialize)this.picSwipe3).BeginInit();
			this.GroupBox6.SuspendLayout();
			((ISupportInitialize)this.picSwipe4).BeginInit();
			this.GroupBox7.SuspendLayout();
			((ISupportInitialize)this.picSwipe5).BeginInit();
			this.GroupBox5.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.GroupBox8.SuspendLayout();
			((ISupportInitialize)this.picSwipe6).BeginInit();
			base.SuspendLayout();
			this.Timer1.Enabled = true;
			this.Timer1.Interval = 500;
			this.Timer1.Tick += new EventHandler(this.Timer1_Tick);
			this.Timer2.Interval = 50;
			this.Timer2.Tick += new EventHandler(this.Timer2_Tick);
			this.TimerStartSlow.Enabled = true;
			this.TimerStartSlow.Interval = 300;
			this.TimerStartSlow.Tick += new EventHandler(this.TimerStartSlow_Tick);
			componentResourceManager.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Controls.Add(this.GroupBox2);
			this.flowLayoutPanel1.Controls.Add(this.GroupBox3);
			this.flowLayoutPanel1.Controls.Add(this.GroupBox4);
			this.flowLayoutPanel1.Controls.Add(this.GroupBox6);
			this.flowLayoutPanel1.Controls.Add(this.GroupBox7);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.GroupBox2.BackColor = Color.Transparent;
			this.GroupBox2.Controls.Add(this.picSwipe1);
			this.GroupBox2.Controls.Add(this.txtSwipeUser1);
			this.GroupBox2.Controls.Add(this.txtSwipeSeat1);
			this.GroupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox2, "GroupBox2");
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.picSwipe1, "picSwipe1");
			this.picSwipe1.Name = "picSwipe1";
			this.picSwipe1.TabStop = false;
			componentResourceManager.ApplyResources(this.txtSwipeUser1, "txtSwipeUser1");
			this.txtSwipeUser1.BackColor = Color.FromArgb(255, 224, 192);
			this.txtSwipeUser1.BorderStyle = BorderStyle.None;
			this.txtSwipeUser1.ForeColor = SystemColors.WindowText;
			this.txtSwipeUser1.Name = "txtSwipeUser1";
			this.txtSwipeUser1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtSwipeSeat1, "txtSwipeSeat1");
			this.txtSwipeSeat1.BackColor = Color.FromArgb(255, 224, 192);
			this.txtSwipeSeat1.BorderStyle = BorderStyle.None;
			this.txtSwipeSeat1.Name = "txtSwipeSeat1";
			this.txtSwipeSeat1.ReadOnly = true;
			this.GroupBox3.BackColor = Color.Transparent;
			this.GroupBox3.Controls.Add(this.picSwipe2);
			this.GroupBox3.Controls.Add(this.txtSwipeUser2);
			this.GroupBox3.Controls.Add(this.txtSwipeSeat2);
			this.GroupBox3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox3, "GroupBox3");
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.picSwipe2, "picSwipe2");
			this.picSwipe2.Name = "picSwipe2";
			this.picSwipe2.TabStop = false;
			componentResourceManager.ApplyResources(this.txtSwipeUser2, "txtSwipeUser2");
			this.txtSwipeUser2.BackColor = Color.White;
			this.txtSwipeUser2.BorderStyle = BorderStyle.None;
			this.txtSwipeUser2.ForeColor = Color.Black;
			this.txtSwipeUser2.Name = "txtSwipeUser2";
			this.txtSwipeUser2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtSwipeSeat2, "txtSwipeSeat2");
			this.txtSwipeSeat2.BackColor = Color.White;
			this.txtSwipeSeat2.BorderStyle = BorderStyle.None;
			this.txtSwipeSeat2.ForeColor = Color.Black;
			this.txtSwipeSeat2.Name = "txtSwipeSeat2";
			this.txtSwipeSeat2.ReadOnly = true;
			this.GroupBox4.BackColor = Color.Transparent;
			this.GroupBox4.Controls.Add(this.picSwipe3);
			this.GroupBox4.Controls.Add(this.txtSwipeUser3);
			this.GroupBox4.Controls.Add(this.txtSwipeSeat3);
			this.GroupBox4.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox4, "GroupBox4");
			this.GroupBox4.Name = "GroupBox4";
			this.GroupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.picSwipe3, "picSwipe3");
			this.picSwipe3.Name = "picSwipe3";
			this.picSwipe3.TabStop = false;
			componentResourceManager.ApplyResources(this.txtSwipeUser3, "txtSwipeUser3");
			this.txtSwipeUser3.BackColor = Color.White;
			this.txtSwipeUser3.BorderStyle = BorderStyle.None;
			this.txtSwipeUser3.ForeColor = Color.Black;
			this.txtSwipeUser3.Name = "txtSwipeUser3";
			this.txtSwipeUser3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtSwipeSeat3, "txtSwipeSeat3");
			this.txtSwipeSeat3.BackColor = Color.White;
			this.txtSwipeSeat3.BorderStyle = BorderStyle.None;
			this.txtSwipeSeat3.ForeColor = Color.Black;
			this.txtSwipeSeat3.Name = "txtSwipeSeat3";
			this.txtSwipeSeat3.ReadOnly = true;
			this.GroupBox6.BackColor = Color.Transparent;
			this.GroupBox6.Controls.Add(this.picSwipe4);
			this.GroupBox6.Controls.Add(this.txtSwipeUser4);
			this.GroupBox6.Controls.Add(this.txtSwipeSeat4);
			this.GroupBox6.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox6, "GroupBox6");
			this.GroupBox6.Name = "GroupBox6";
			this.GroupBox6.TabStop = false;
			componentResourceManager.ApplyResources(this.picSwipe4, "picSwipe4");
			this.picSwipe4.Name = "picSwipe4";
			this.picSwipe4.TabStop = false;
			componentResourceManager.ApplyResources(this.txtSwipeUser4, "txtSwipeUser4");
			this.txtSwipeUser4.BackColor = Color.White;
			this.txtSwipeUser4.BorderStyle = BorderStyle.None;
			this.txtSwipeUser4.ForeColor = Color.Black;
			this.txtSwipeUser4.Name = "txtSwipeUser4";
			this.txtSwipeUser4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtSwipeSeat4, "txtSwipeSeat4");
			this.txtSwipeSeat4.BackColor = Color.White;
			this.txtSwipeSeat4.BorderStyle = BorderStyle.None;
			this.txtSwipeSeat4.ForeColor = Color.Black;
			this.txtSwipeSeat4.Name = "txtSwipeSeat4";
			this.txtSwipeSeat4.ReadOnly = true;
			this.GroupBox7.BackColor = Color.Transparent;
			this.GroupBox7.Controls.Add(this.picSwipe5);
			this.GroupBox7.Controls.Add(this.txtSwipeUser5);
			this.GroupBox7.Controls.Add(this.txtSwipeSeat5);
			this.GroupBox7.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox7, "GroupBox7");
			this.GroupBox7.Name = "GroupBox7";
			this.GroupBox7.TabStop = false;
			componentResourceManager.ApplyResources(this.picSwipe5, "picSwipe5");
			this.picSwipe5.Name = "picSwipe5";
			this.picSwipe5.TabStop = false;
			componentResourceManager.ApplyResources(this.txtSwipeUser5, "txtSwipeUser5");
			this.txtSwipeUser5.BackColor = Color.White;
			this.txtSwipeUser5.BorderStyle = BorderStyle.None;
			this.txtSwipeUser5.ForeColor = Color.Black;
			this.txtSwipeUser5.Name = "txtSwipeUser5";
			this.txtSwipeUser5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtSwipeSeat5, "txtSwipeSeat5");
			this.txtSwipeSeat5.BackColor = Color.White;
			this.txtSwipeSeat5.BorderStyle = BorderStyle.None;
			this.txtSwipeSeat5.ForeColor = Color.Black;
			this.txtSwipeSeat5.Name = "txtSwipeSeat5";
			this.txtSwipeSeat5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.lblMeetingName, "lblMeetingName");
			this.lblMeetingName.BackColor = Color.Transparent;
			this.lblMeetingName.ForeColor = Color.White;
			this.lblMeetingName.Name = "lblMeetingName";
			componentResourceManager.ApplyResources(this.GroupBox5, "GroupBox5");
			this.GroupBox5.Controls.Add(this.button8);
			this.GroupBox5.Controls.Add(this.btnErrConnect);
			this.GroupBox5.Controls.Add(this.btnCancel);
			this.GroupBox5.Controls.Add(this.Button6);
			this.GroupBox5.Controls.Add(this.Button7);
			this.GroupBox5.Controls.Add(this.lblTime);
			this.GroupBox5.Controls.Add(this.Button3);
			this.GroupBox5.Controls.Add(this.Button2);
			this.GroupBox5.Controls.Add(this.Button1);
			this.GroupBox5.Controls.Add(this.Button4);
			this.GroupBox5.Controls.Add(this.Button5);
			this.GroupBox5.Controls.Add(this.txtA0);
			this.GroupBox5.Controls.Add(this.txtA1);
			this.GroupBox5.Controls.Add(this.txtA2);
			this.GroupBox5.Controls.Add(this.txtA3);
			this.GroupBox5.Controls.Add(this.txtA4);
			this.GroupBox5.Controls.Add(this.Label2);
			this.GroupBox5.Controls.Add(this.Label3);
			this.GroupBox5.Controls.Add(this.txtB0);
			this.GroupBox5.Controls.Add(this.txtB1);
			this.GroupBox5.Controls.Add(this.txtB2);
			this.GroupBox5.Controls.Add(this.txtB3);
			this.GroupBox5.Controls.Add(this.txtB4);
			this.GroupBox5.Controls.Add(this.Label4);
			this.GroupBox5.Controls.Add(this.txtC0);
			this.GroupBox5.Controls.Add(this.txtC1);
			this.GroupBox5.Controls.Add(this.txtC2);
			this.GroupBox5.Controls.Add(this.txtC3);
			this.GroupBox5.Controls.Add(this.txtC4);
			this.GroupBox5.Controls.Add(this.txtD0);
			this.GroupBox5.Controls.Add(this.txtD1);
			this.GroupBox5.Controls.Add(this.txtD2);
			this.GroupBox5.Controls.Add(this.Label5);
			this.GroupBox5.Controls.Add(this.txtD3);
			this.GroupBox5.Controls.Add(this.txtD4);
			this.GroupBox5.Controls.Add(this.Label6);
			this.GroupBox5.Controls.Add(this.txtE3);
			this.GroupBox5.Controls.Add(this.txtE4);
			this.GroupBox5.Controls.Add(this.txtE2);
			this.GroupBox5.Controls.Add(this.txtE1);
			this.GroupBox5.Controls.Add(this.txtE0);
			this.GroupBox5.Name = "GroupBox5";
			this.GroupBox5.TabStop = false;
			this.button8.BackColor = Color.Transparent;
			this.button8.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.button8, "button8");
			this.button8.ForeColor = Color.White;
			this.button8.Name = "button8";
			this.button8.UseVisualStyleBackColor = false;
			this.button8.Click += new EventHandler(this.button8_Click);
			this.btnErrConnect.BackgroundImage = Resources.eventlogError;
			componentResourceManager.ApplyResources(this.btnErrConnect, "btnErrConnect");
			this.btnErrConnect.FlatAppearance.BorderSize = 0;
			this.btnErrConnect.Name = "btnErrConnect";
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.Button6.BackColor = Color.Transparent;
			this.Button6.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button6, "Button6");
			this.Button6.ForeColor = Color.White;
			this.Button6.Name = "Button6";
			this.Button6.UseVisualStyleBackColor = false;
			this.Button6.Click += new EventHandler(this.Button6_Click);
			this.Button7.BackColor = Color.Transparent;
			this.Button7.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button7, "Button7");
			this.Button7.ForeColor = Color.White;
			this.Button7.Name = "Button7";
			this.Button7.UseVisualStyleBackColor = false;
			this.Button7.Click += new EventHandler(this.Button7_Click);
			this.lblTime.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.lblTime, "lblTime");
			this.lblTime.ForeColor = Color.White;
			this.lblTime.Name = "lblTime";
			this.Button3.BackColor = Color.Transparent;
			this.Button3.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button3, "Button3");
			this.Button3.ForeColor = Color.White;
			this.Button3.Name = "Button3";
			this.Button3.UseVisualStyleBackColor = false;
			this.Button3.Click += new EventHandler(this.Button7_Click);
			this.Button2.BackColor = Color.Transparent;
			this.Button2.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button2, "Button2");
			this.Button2.ForeColor = Color.White;
			this.Button2.Name = "Button2";
			this.Button2.UseVisualStyleBackColor = false;
			this.Button2.Click += new EventHandler(this.Button7_Click);
			this.Button1.BackColor = Color.Transparent;
			this.Button1.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button1, "Button1");
			this.Button1.ForeColor = Color.White;
			this.Button1.Name = "Button1";
			this.Button1.UseVisualStyleBackColor = false;
			this.Button1.Click += new EventHandler(this.Button7_Click);
			this.Button4.BackColor = Color.Transparent;
			this.Button4.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button4, "Button4");
			this.Button4.ForeColor = Color.White;
			this.Button4.Name = "Button4";
			this.Button4.UseVisualStyleBackColor = false;
			this.Button4.Click += new EventHandler(this.Button7_Click);
			this.Button5.BackColor = Color.Transparent;
			this.Button5.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Button5, "Button5");
			this.Button5.ForeColor = Color.White;
			this.Button5.Name = "Button5";
			this.Button5.UseVisualStyleBackColor = false;
			this.Button5.Click += new EventHandler(this.Button7_Click);
			this.txtA0.BackColor = Color.White;
			this.txtA0.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtA0, "txtA0");
			this.txtA0.ForeColor = Color.Black;
			this.txtA0.Name = "txtA0";
			this.txtA0.ReadOnly = true;
			this.txtA1.BackColor = Color.White;
			this.txtA1.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtA1, "txtA1");
			this.txtA1.ForeColor = Color.Black;
			this.txtA1.Name = "txtA1";
			this.txtA1.ReadOnly = true;
			this.txtA2.BackColor = Color.White;
			this.txtA2.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtA2, "txtA2");
			this.txtA2.ForeColor = Color.Black;
			this.txtA2.Name = "txtA2";
			this.txtA2.ReadOnly = true;
			this.txtA3.BackColor = Color.White;
			this.txtA3.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtA3, "txtA3");
			this.txtA3.ForeColor = Color.Black;
			this.txtA3.Name = "txtA3";
			this.txtA3.ReadOnly = true;
			this.txtA4.BackColor = Color.White;
			this.txtA4.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtA4, "txtA4");
			this.txtA4.ForeColor = Color.Black;
			this.txtA4.Name = "txtA4";
			this.txtA4.ReadOnly = true;
			this.Label2.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.ForeColor = Color.White;
			this.Label2.Name = "Label2";
			this.Label3.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.ForeColor = Color.White;
			this.Label3.Name = "Label3";
			this.txtB0.BackColor = Color.White;
			this.txtB0.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtB0, "txtB0");
			this.txtB0.ForeColor = Color.Black;
			this.txtB0.Name = "txtB0";
			this.txtB0.ReadOnly = true;
			this.txtB1.BackColor = Color.White;
			this.txtB1.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtB1, "txtB1");
			this.txtB1.ForeColor = Color.Black;
			this.txtB1.Name = "txtB1";
			this.txtB1.ReadOnly = true;
			this.txtB2.BackColor = Color.White;
			this.txtB2.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtB2, "txtB2");
			this.txtB2.ForeColor = Color.Black;
			this.txtB2.Name = "txtB2";
			this.txtB2.ReadOnly = true;
			this.txtB3.BackColor = Color.White;
			this.txtB3.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtB3, "txtB3");
			this.txtB3.ForeColor = Color.Black;
			this.txtB3.Name = "txtB3";
			this.txtB3.ReadOnly = true;
			this.txtB4.BackColor = Color.White;
			this.txtB4.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtB4, "txtB4");
			this.txtB4.ForeColor = Color.Black;
			this.txtB4.Name = "txtB4";
			this.txtB4.ReadOnly = true;
			this.Label4.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.ForeColor = Color.White;
			this.Label4.Name = "Label4";
			this.txtC0.BackColor = Color.White;
			this.txtC0.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtC0, "txtC0");
			this.txtC0.ForeColor = Color.Black;
			this.txtC0.Name = "txtC0";
			this.txtC0.ReadOnly = true;
			this.txtC1.BackColor = Color.White;
			this.txtC1.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtC1, "txtC1");
			this.txtC1.ForeColor = Color.Black;
			this.txtC1.Name = "txtC1";
			this.txtC1.ReadOnly = true;
			this.txtC2.BackColor = Color.White;
			this.txtC2.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtC2, "txtC2");
			this.txtC2.ForeColor = Color.Black;
			this.txtC2.Name = "txtC2";
			this.txtC2.ReadOnly = true;
			this.txtC3.BackColor = Color.White;
			this.txtC3.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtC3, "txtC3");
			this.txtC3.ForeColor = Color.Black;
			this.txtC3.Name = "txtC3";
			this.txtC3.ReadOnly = true;
			this.txtC4.BackColor = Color.White;
			this.txtC4.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtC4, "txtC4");
			this.txtC4.ForeColor = Color.Black;
			this.txtC4.Name = "txtC4";
			this.txtC4.ReadOnly = true;
			this.txtD0.BackColor = Color.White;
			this.txtD0.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtD0, "txtD0");
			this.txtD0.ForeColor = Color.Black;
			this.txtD0.Name = "txtD0";
			this.txtD0.ReadOnly = true;
			this.txtD1.BackColor = Color.White;
			this.txtD1.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtD1, "txtD1");
			this.txtD1.ForeColor = Color.Black;
			this.txtD1.Name = "txtD1";
			this.txtD1.ReadOnly = true;
			this.txtD2.BackColor = Color.White;
			this.txtD2.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtD2, "txtD2");
			this.txtD2.ForeColor = Color.Black;
			this.txtD2.Name = "txtD2";
			this.txtD2.ReadOnly = true;
			this.Label5.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.ForeColor = Color.White;
			this.Label5.Name = "Label5";
			this.txtD3.BackColor = Color.White;
			this.txtD3.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtD3, "txtD3");
			this.txtD3.ForeColor = Color.Black;
			this.txtD3.Name = "txtD3";
			this.txtD3.ReadOnly = true;
			this.txtD4.BackColor = Color.White;
			this.txtD4.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtD4, "txtD4");
			this.txtD4.ForeColor = Color.Black;
			this.txtD4.Name = "txtD4";
			this.txtD4.ReadOnly = true;
			this.Label6.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.ForeColor = Color.White;
			this.Label6.Name = "Label6";
			this.txtE3.BackColor = Color.White;
			this.txtE3.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtE3, "txtE3");
			this.txtE3.ForeColor = Color.Black;
			this.txtE3.Name = "txtE3";
			this.txtE3.ReadOnly = true;
			this.txtE4.BackColor = Color.White;
			this.txtE4.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtE4, "txtE4");
			this.txtE4.ForeColor = Color.Black;
			this.txtE4.Name = "txtE4";
			this.txtE4.ReadOnly = true;
			this.txtE2.BackColor = Color.White;
			this.txtE2.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtE2, "txtE2");
			this.txtE2.ForeColor = Color.Black;
			this.txtE2.Name = "txtE2";
			this.txtE2.ReadOnly = true;
			this.txtE1.BackColor = Color.White;
			this.txtE1.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtE1, "txtE1");
			this.txtE1.ForeColor = Color.Black;
			this.txtE1.Name = "txtE1";
			this.txtE1.ReadOnly = true;
			this.txtE0.BackColor = Color.White;
			this.txtE0.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtE0, "txtE0");
			this.txtE0.ForeColor = Color.Black;
			this.txtE0.Name = "txtE0";
			this.txtE0.ReadOnly = true;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Controls.Add(this.GroupBox8);
			this.GroupBox1.ForeColor = Color.White;
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			this.GroupBox1.SizeChanged += new EventHandler(this.GroupBox1_SizeChanged);
			this.GroupBox8.Controls.Add(this.picSwipe6);
			this.GroupBox8.Controls.Add(this.txtSwipeUser6);
			this.GroupBox8.Controls.Add(this.txtSwipeSeat6);
			componentResourceManager.ApplyResources(this.GroupBox8, "GroupBox8");
			this.GroupBox8.Name = "GroupBox8";
			this.GroupBox8.TabStop = false;
			componentResourceManager.ApplyResources(this.picSwipe6, "picSwipe6");
			this.picSwipe6.Name = "picSwipe6";
			this.picSwipe6.TabStop = false;
			this.txtSwipeUser6.BackColor = SystemColors.ActiveCaptionText;
			this.txtSwipeUser6.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtSwipeUser6, "txtSwipeUser6");
			this.txtSwipeUser6.Name = "txtSwipeUser6";
			this.txtSwipeUser6.ReadOnly = true;
			this.txtSwipeSeat6.BackColor = SystemColors.ActiveCaptionText;
			this.txtSwipeSeat6.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtSwipeSeat6, "txtSwipeSeat6");
			this.txtSwipeSeat6.Name = "txtSwipeSeat6";
			this.txtSwipeSeat6.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.flowLayoutPanel1);
			base.Controls.Add(this.lblMeetingName);
			base.Controls.Add(this.GroupBox5);
			base.Controls.Add(this.GroupBox1);
			base.Name = "dfrmMeetingSign";
			base.TopMost = true;
			base.Closing += new CancelEventHandler(this.dfrmMeetingSign_Closing);
			base.FormClosing += new FormClosingEventHandler(this.dfrmMeetingSign_FormClosing);
			base.Load += new EventHandler(this.dfrmMeetingSign_Load);
			base.SizeChanged += new EventHandler(this.dfrmMeetingSign_SizeChanged);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox2.PerformLayout();
			((ISupportInitialize)this.picSwipe1).EndInit();
			this.GroupBox3.ResumeLayout(false);
			this.GroupBox3.PerformLayout();
			((ISupportInitialize)this.picSwipe2).EndInit();
			this.GroupBox4.ResumeLayout(false);
			this.GroupBox4.PerformLayout();
			((ISupportInitialize)this.picSwipe3).EndInit();
			this.GroupBox6.ResumeLayout(false);
			this.GroupBox6.PerformLayout();
			((ISupportInitialize)this.picSwipe4).EndInit();
			this.GroupBox7.ResumeLayout(false);
			this.GroupBox7.PerformLayout();
			((ISupportInitialize)this.picSwipe5).EndInit();
			this.GroupBox5.ResumeLayout(false);
			this.GroupBox5.PerformLayout();
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox8.ResumeLayout(false);
			this.GroupBox8.PerformLayout();
			((ISupportInitialize)this.picSwipe6).EndInit();
			base.ResumeLayout(false);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		public void fillMeetingNum()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.fillMeetingNum_Acc();
				return;
			}
			try
			{
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				for (int i = 0; i <= 4; i++)
				{
					this.arrMeetingNum[0, i] = 0L;
					this.arrMeetingNum[1, i] = 0L;
					this.arrMeetingNum[2, i] = 0L;
					this.arrMeetingNum[3, i] = 0L;
					this.arrMeetingNum[4, i] = 0L;
				}
				string selectCommandText = "SELECT  a.*  FROM t_d_MeetingConsumer a, t_b_Consumer b WHERE a.f_ConsumerID=b.f_ConsumerID and a.f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommandText, sqlConnection);
				this.ds.Clear();
				sqlDataAdapter.Fill(this.ds, "t_d_MeetingConsumer");
				DataView dataView = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				for (int j = 0; j <= 3; j++)
				{
					dataView.RowFilter = " f_MeetingIdentity = " + j;
					if (dataView.Count > 0)
					{
						this.arrMeetingNum[j, 0] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1)) ";
						this.arrMeetingNum[j, 1] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND (f_SignWay = 2) ";
						this.arrMeetingNum[j, 2] = (long)dataView.Count;
						this.arrMeetingNum[j, 3] = Math.Max(0L, this.arrMeetingNum[j, 0] - this.arrMeetingNum[j, 1] - this.arrMeetingNum[j, 2]);
						if (this.arrMeetingNum[j, 0] > 0L)
						{
							this.arrMeetingNum[j, 4] = this.arrMeetingNum[j, 1] * 1000L / this.arrMeetingNum[j, 0];
						}
					}
					this.arrMeetingNum[4, 0] += this.arrMeetingNum[j, 0];
					this.arrMeetingNum[4, 1] += this.arrMeetingNum[j, 1];
					this.arrMeetingNum[4, 2] += this.arrMeetingNum[j, 2];
					this.arrMeetingNum[4, 3] += this.arrMeetingNum[j, 3];
				}
				if (this.arrMeetingNum[4, 0] > 0L)
				{
					this.arrMeetingNum[4, 4] = this.arrMeetingNum[4, 1] * 1000L / this.arrMeetingNum[4, 0];
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

		public void fillMeetingNum_Acc()
		{
			try
			{
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				for (int i = 0; i <= 4; i++)
				{
					this.arrMeetingNum[0, i] = 0L;
					this.arrMeetingNum[1, i] = 0L;
					this.arrMeetingNum[2, i] = 0L;
					this.arrMeetingNum[3, i] = 0L;
					this.arrMeetingNum[4, i] = 0L;
				}
				string selectCommandText = "SELECT  a.*  FROM t_d_MeetingConsumer a, t_b_Consumer b WHERE a.f_ConsumerID=b.f_ConsumerID and a.f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, oleDbConnection);
				this.ds.Clear();
				oleDbDataAdapter.Fill(this.ds, "t_d_MeetingConsumer");
				DataView dataView = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				for (int j = 0; j <= 3; j++)
				{
					dataView.RowFilter = " f_MeetingIdentity = " + j;
					if (dataView.Count > 0)
					{
						this.arrMeetingNum[j, 0] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1)) ";
						this.arrMeetingNum[j, 1] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND (f_SignWay = 2) ";
						this.arrMeetingNum[j, 2] = (long)dataView.Count;
						this.arrMeetingNum[j, 3] = Math.Max(0L, this.arrMeetingNum[j, 0] - this.arrMeetingNum[j, 1] - this.arrMeetingNum[j, 2]);
						if (this.arrMeetingNum[j, 0] > 0L)
						{
							this.arrMeetingNum[j, 4] = this.arrMeetingNum[j, 1] * 1000L / this.arrMeetingNum[j, 0];
						}
					}
					this.arrMeetingNum[4, 0] += this.arrMeetingNum[j, 0];
					this.arrMeetingNum[4, 1] += this.arrMeetingNum[j, 1];
					this.arrMeetingNum[4, 2] += this.arrMeetingNum[j, 2];
					this.arrMeetingNum[4, 3] += this.arrMeetingNum[j, 3];
				}
				if (this.arrMeetingNum[4, 0] > 0L)
				{
					this.arrMeetingNum[4, 4] = this.arrMeetingNum[4, 1] * 1000L / this.arrMeetingNum[4, 0];
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

		public void fillMeetingRecord(string MeetingNo)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.fillMeetingRecord_Acc(MeetingNo);
				return;
			}
			try
			{
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(MeetingNo);
				SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					this.signStarttime = (DateTime)sqlDataReader["f_SignStartTime"];
					this.signEndtime = (DateTime)sqlDataReader["f_SignEndTime"];
					this.meetingAdr = wgTools.SetObjToStr(sqlDataReader["f_MeetingAdr"]);
				}
				sqlDataReader.Close();
				if (this.lngDealtRecordID == -1L && this.meetingAdr != "")
				{
					this.queryReaderStr = "";
					text = "Select t_b_reader.* from t_b_reader,t_d_MeetingAdr  ";
					text += " , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  t_b_Reader.f_ReaderID  = t_d_MeetingAdr.f_ReaderID ";
					text = text + " AND t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.meetingAdr);
					sqlCommand = new SqlCommand(text, sqlConnection);
					sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.HasRows)
					{
						while (sqlDataReader.Read())
						{
							if (this.queryReaderStr == "")
							{
								this.queryReaderStr = " f_ReaderID IN ( " + sqlDataReader["f_ReaderID"];
							}
							else
							{
								this.queryReaderStr = this.queryReaderStr + " , " + sqlDataReader["f_ReaderID"];
							}
							if (this.arrControllerID.IndexOf(sqlDataReader["f_ControllerID"]) < 0)
							{
								this.arrControllerID.Add(sqlDataReader["f_ControllerID"]);
							}
						}
						this.queryReaderStr += ")";
					}
					sqlDataReader.Close();
				}
				if (this.lngDealtRecordID == -1L)
				{
					this.lngDealtRecordID = 0L;
				}
				string text2 = "";
				text2 = text2 + " ([f_ReadDate]>= " + wgTools.PrepareStr(this.signStarttime, true, "yyyy-MM-dd H:mm:ss") + ")";
				text2 = text2 + " AND ([f_ReadDate]<= " + wgTools.PrepareStr(this.signEndtime, true, "yyyy-MM-dd H:mm:ss") + ")";
				string text3 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text3 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text3 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, ";
				text3 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll, t_d_MeetingConsumer.f_Seat, t_d_MeetingConsumer.f_MeetingIdentity, t_d_SwipeRecord.f_ConsumerID ";
				text3 += string.Format(" FROM ((t_b_Consumer INNER JOIN t_d_SwipeRecord ON ( t_b_Consumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID)) LEFT JOIN  t_d_MeetingConsumer on ( t_d_MeetingConsumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + ") ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", new object[0]);
				text3 = text3 + " WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID.ToString();
				text3 = text3 + " AND  t_d_SwipeRecord.f_ConsumerID IN (SELECT f_ConsumerID FROM t_d_MeetingConsumer WHERE f_SignWay=0 AND f_RecID =0 AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + " )  ";
				if (this.queryReaderStr != "")
				{
					text3 = text3 + " AND " + this.queryReaderStr;
				}
				text3 = text3 + " AND " + text2;
				text = text3;
				sqlCommand = new SqlCommand(text, sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				if (sqlDataReader.HasRows)
				{
					while (sqlDataReader.Read())
					{
						int num = arrayList.IndexOf(sqlDataReader["f_ConsumerID"]);
						if (num < 0)
						{
							arrayList.Add(sqlDataReader["f_ConsumerID"]);
							arrayList2.Add((DateTime)sqlDataReader["f_ReadDate"]);
							arrayList3.Add(sqlDataReader["f_RecID"]);
						}
						else if ((DateTime)arrayList2[num] > (DateTime)sqlDataReader["f_ReadDate"])
						{
							arrayList2[num] = (DateTime)sqlDataReader["f_ReadDate"];
							arrayList3[num] = sqlDataReader["f_RecID"];
						}
					}
				}
				sqlDataReader.Close();
				if (arrayList.Count > 0)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						text = " UPDATE t_d_MeetingConsumer ";
						text = text + " SET [f_SignRealTime] = " + wgTools.PrepareStr((DateTime)arrayList2[i], true, "yyyy-MM-dd H:mm:ss");
						text = text + " ,[f_RecID] = " + arrayList3[i];
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" WHERE f_ConsumerID = ",
							arrayList[i],
							" AND  f_MeetingNO = ",
							wgTools.PrepareStr(MeetingNo)
						});
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
				text = "SELECT f_RecID from t_d_SwipeRecord ORDER BY f_RecID DESC ";
				sqlCommand = new SqlCommand(text, sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.HasRows && sqlDataReader.Read())
				{
					this.lngDealtRecordID = long.Parse(sqlDataReader["f_RecID"].ToString());
				}
				sqlDataReader.Close();
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void fillMeetingRecord_Acc(string MeetingNo)
		{
			try
			{
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(MeetingNo);
				OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.Read())
				{
					this.signStarttime = (DateTime)oleDbDataReader["f_SignStartTime"];
					this.signEndtime = (DateTime)oleDbDataReader["f_SignEndTime"];
					this.meetingAdr = wgTools.SetObjToStr(oleDbDataReader["f_MeetingAdr"]);
				}
				oleDbDataReader.Close();
				if (this.lngDealtRecordID == -1L && this.meetingAdr != "")
				{
					this.queryReaderStr = "";
					text = "Select t_b_reader.* from t_b_reader,t_d_MeetingAdr  ";
					text += " , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  t_b_Reader.f_ReaderID  = t_d_MeetingAdr.f_ReaderID ";
					text = text + " AND t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.meetingAdr);
					oleDbCommand = new OleDbCommand(text, oleDbConnection);
					oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.HasRows)
					{
						while (oleDbDataReader.Read())
						{
							if (this.queryReaderStr == "")
							{
								this.queryReaderStr = " f_ReaderID IN ( " + oleDbDataReader["f_ReaderID"];
							}
							else
							{
								this.queryReaderStr = this.queryReaderStr + " , " + oleDbDataReader["f_ReaderID"];
							}
							if (this.arrControllerID.IndexOf(oleDbDataReader["f_ControllerID"]) < 0)
							{
								this.arrControllerID.Add(oleDbDataReader["f_ControllerID"]);
							}
						}
						this.queryReaderStr += ")";
					}
					oleDbDataReader.Close();
				}
				if (this.lngDealtRecordID == -1L)
				{
					this.lngDealtRecordID = 0L;
				}
				string text2 = "";
				text2 = text2 + " ([f_ReadDate]>= " + wgTools.PrepareStr(this.signStarttime, true, "yyyy-MM-dd H:mm:ss") + ")";
				text2 = text2 + " AND ([f_ReadDate]<= " + wgTools.PrepareStr(this.signEndtime, true, "yyyy-MM-dd H:mm:ss") + ")";
				string text3 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text3 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text3 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, ";
				text3 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll, t_d_MeetingConsumer.f_Seat, t_d_MeetingConsumer.f_MeetingIdentity, t_d_SwipeRecord.f_ConsumerID ";
				text3 += string.Format(" FROM ((t_b_Consumer INNER JOIN t_d_SwipeRecord ON ( t_b_Consumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID)) LEFT JOIN  t_d_MeetingConsumer on ( t_d_MeetingConsumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + ") ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", new object[0]);
				text3 = text3 + " WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID.ToString();
				text3 = text3 + " AND  t_d_SwipeRecord.f_ConsumerID IN (SELECT f_ConsumerID FROM t_d_MeetingConsumer WHERE f_SignWay=0 AND f_RecID =0 AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + " )  ";
				if (this.queryReaderStr != "")
				{
					text3 = text3 + " AND " + this.queryReaderStr;
				}
				text3 = text3 + " AND " + text2;
				text = text3;
				oleDbCommand = new OleDbCommand(text, oleDbConnection);
				oleDbDataReader = oleDbCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				if (oleDbDataReader.HasRows)
				{
					while (oleDbDataReader.Read())
					{
						int num = arrayList.IndexOf(oleDbDataReader["f_ConsumerID"]);
						if (num < 0)
						{
							arrayList.Add(oleDbDataReader["f_ConsumerID"]);
							arrayList2.Add((DateTime)oleDbDataReader["f_ReadDate"]);
							arrayList3.Add(oleDbDataReader["f_RecID"]);
						}
						else if ((DateTime)arrayList2[num] > (DateTime)oleDbDataReader["f_ReadDate"])
						{
							arrayList2[num] = (DateTime)oleDbDataReader["f_ReadDate"];
							arrayList3[num] = oleDbDataReader["f_RecID"];
						}
					}
				}
				oleDbDataReader.Close();
				if (arrayList.Count > 0)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						text = " UPDATE t_d_MeetingConsumer ";
						text = text + " SET [f_SignRealTime] = " + wgTools.PrepareStr((DateTime)arrayList2[i], true, "yyyy-MM-dd H:mm:ss");
						text = text + " ,[f_RecID] = " + arrayList3[i];
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" WHERE f_ConsumerID = ",
							arrayList[i],
							" AND  f_MeetingNO = ",
							wgTools.PrepareStr(MeetingNo)
						});
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
				text = "SELECT f_RecID from t_d_SwipeRecord ORDER BY f_RecID DESC ";
				oleDbCommand = new OleDbCommand(text, oleDbConnection);
				oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.HasRows && oleDbDataReader.Read())
				{
					this.lngDealtRecordID = long.Parse(oleDbDataReader["f_RecID"].ToString());
				}
				oleDbDataReader.Close();
				oleDbConnection.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void startSlow()
		{
			MethodInvoker method = new MethodInvoker(this.startSlow_Invoker);
			try
			{
				Application.DoEvents();
				Thread.Sleep(1000);
				this.fillMeetingRecord(this.curMeetingNo);
				this.fillMeetingNum();
				base.BeginInvoke(method);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void startSlow_Invoker()
		{
			try
			{
				int num = 0;
				this.txtA0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtA1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtA2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtA3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtA4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 1;
				this.txtB0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtB1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtB2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtB3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtB4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 2;
				this.txtC0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtC1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtC2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtC3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtC4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 3;
				this.txtD0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtD1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtD2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtD3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtD4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 4;
				this.txtE0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtE1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtE2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtE3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtE4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				if (this.frmWatch == null)
				{
					this.frmWatch = new frmConsole();
					this.frmWatch.arrSelectDoors4Sign = (ArrayList)this.arrControllerID.Clone();
					this.frmWatch.WindowState = FormWindowState.Minimized;
					this.frmWatch.Show();
					this.frmWatch.Visible = false;
					this.frmWatch.directToRealtimeGet();
				}
				foreach (object current in this.GroupBox5.Controls)
				{
					if (current.GetType().Name.ToString() == "TextBox" && ((TextBox)current).Text == "0")
					{
						((TextBox)current).Text = "";
					}
				}
				if (string.IsNullOrEmpty(this.txtA0.Text))
				{
					this.txtA4.Text = "";
				}
				if (string.IsNullOrEmpty(this.txtB0.Text))
				{
					this.txtB4.Text = "";
				}
				if (string.IsNullOrEmpty(this.txtC0.Text))
				{
					this.txtC4.Text = "";
				}
				if (string.IsNullOrEmpty(this.txtD0.Text))
				{
					this.txtD4.Text = "";
				}
				if (string.IsNullOrEmpty(this.txtE0.Text))
				{
					this.txtE4.Text = "";
				}
				this.Timer2.Enabled = true;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			this.TimerStartSlow.Enabled = false;
			Cursor.Current = Cursors.Default;
		}

		private void dfrmMeetingSign_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmMeetingSign_Load_Acc(sender, e);
				return;
			}
			try
			{
				if (this.curMeetingNo == "")
				{
					base.Close();
				}
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				string cmdText = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					this.lblMeetingName.Text = wgTools.SetObjToStr(sqlDataReader["f_MeetingName"]);
					this.signStarttime = DateTime.Parse(Strings.Format(sqlDataReader["f_SignStartTime"], "yyyy-MM-dd HH:mm:ss"));
					this.signEndtime = DateTime.Parse(Strings.Format(sqlDataReader["f_SignEndTime"], "yyyy-MM-dd HH:mm:ss"));
					this.meetingAdr = wgTools.SetObjToStr(sqlDataReader["f_MeetingAdr"]);
				}
				sqlDataReader.Close();
				sqlConnection.Close();
				if (this.lblMeetingName.Text == "")
				{
					base.Close();
				}
				Application.DoEvents();
				this.startSlowThread = new Thread(new ThreadStart(this.startSlow));
				this.startSlowThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
				this.startSlowThread.IsBackground = true;
				this.startSlowThread.Start();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dfrmMeetingSign_Load_Acc(object sender, EventArgs e)
		{
			try
			{
				if (this.curMeetingNo == "")
				{
					base.Close();
				}
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				string cmdText = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.Read())
				{
					this.lblMeetingName.Text = wgTools.SetObjToStr(oleDbDataReader["f_MeetingName"]);
					this.signStarttime = DateTime.Parse(Strings.Format(oleDbDataReader["f_SignStartTime"], "yyyy-MM-dd HH:mm:ss"));
					this.signEndtime = DateTime.Parse(Strings.Format(oleDbDataReader["f_SignEndTime"], "yyyy-MM-dd HH:mm:ss"));
					this.meetingAdr = wgTools.SetObjToStr(oleDbDataReader["f_MeetingAdr"]);
				}
				oleDbDataReader.Close();
				oleDbConnection.Close();
				if (this.lblMeetingName.Text == "")
				{
					base.Close();
				}
				Application.DoEvents();
				this.startSlowThread = new Thread(new ThreadStart(this.startSlow));
				this.startSlowThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
				this.startSlowThread.IsBackground = true;
				this.startSlowThread.Start();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			this.Timer1.Enabled = false;
			try
			{
				this.lblTime.Text = Strings.Format(DateTime.Now, "HH:mm:ss");
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			this.Timer1.Enabled = true;
		}

		public void updateDisplay_Invoker()
		{
			try
			{
				int num = 0;
				this.txtA0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtA1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtA2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtA3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtA4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 1;
				this.txtB0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtB1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtB2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtB3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtB4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 2;
				this.txtC0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtC1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtC2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtC3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtC4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 3;
				this.txtD0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtD1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtD2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtD3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtD4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				num = 4;
				this.txtE0.Text = this.arrMeetingNum[num, 0].ToString();
				this.txtE1.Text = this.arrMeetingNum[num, 1].ToString();
				this.txtE2.Text = this.arrMeetingNum[num, 2].ToString();
				this.txtE3.Text = this.arrMeetingNum[num, 3].ToString();
				this.txtE4.Text = (this.arrMeetingNum[num, 4] / 10L).ToString() + "%";
				foreach (object current in this.GroupBox5.Controls)
				{
					if (current.GetType().Name.ToString() == "TextBox" && ((TextBox)current).Text == "0")
					{
						((TextBox)current).Text = "";
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

		private void Timer2_Tick(object sender, EventArgs e)
		{
			this.Timer2.Enabled = false;
			try
			{
				this.cntTimer2++;
				if (this.frmWatch.getAllInfoRowsCount() != this.lastinfoRowsCount || this.cntTimer2 > 600 / this.Timer2.Interval)
				{
					this.cntTimer2 = 0;
					this.lastinfoRowsCount = this.frmWatch.getAllInfoRowsCount();
					MethodInvoker method = new MethodInvoker(this.updateDisplay_Invoker);
					object objToStr = null;
					string cmdText = "SELECT f_RecID from t_d_SwipeRecord WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID;
					if (wgAppConfig.IsAccessDB)
					{
						using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
						{
							using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
							{
								oleDbConnection.Open();
								objToStr = oleDbCommand.ExecuteScalar();
								oleDbConnection.Close();
							}
							goto IL_117;
						}
					}
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
						{
							sqlConnection.Open();
							objToStr = sqlCommand.ExecuteScalar();
							sqlConnection.Close();
						}
					}
					IL_117:
					if (wgTools.SetObjToStr(objToStr) != "")
					{
						this.getNewMeetingRecord();
						this.fillMeetingRecord(this.curMeetingNo);
						this.fillMeetingNum();
						base.BeginInvoke(method);
					}
					else
					{
						try
						{
							if (this.frmWatch != null)
							{
								ListView lstDoors = this.frmWatch.lstDoors;
								bool flag = false;
								for (int i = 0; i <= lstDoors.Items.Count - 1; i++)
								{
									if (lstDoors.Items[i].ImageIndex == 3)
									{
										flag = true;
										break;
									}
								}
								if (flag)
								{
									this.btnErrConnect.Visible = (flag ^ this.btnErrConnect.Visible);
								}
								else
								{
									this.btnErrConnect.Visible = flag;
								}
							}
						}
						catch (Exception)
						{
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
			this.Timer2.Enabled = true;
		}

		private void Timer2_Tick_Acc(object sender, EventArgs e)
		{
			this.Timer2.Enabled = false;
			try
			{
				MethodInvoker method = new MethodInvoker(this.updateDisplay_Invoker);
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				string cmdText = "SELECT f_RecID from t_d_SwipeRecord WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID;
				OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				object objToStr = oleDbCommand.ExecuteScalar();
				oleDbConnection.Close();
				if (wgTools.SetObjToStr(objToStr) != "")
				{
					this.getNewMeetingRecord();
					this.fillMeetingRecord(this.curMeetingNo);
					this.fillMeetingNum();
					base.BeginInvoke(method);
				}
				else
				{
					try
					{
						if (this.frmWatch != null)
						{
							ListView lstDoors = this.frmWatch.lstDoors;
							bool flag = false;
							for (int i = 0; i <= lstDoors.Items.Count - 1; i++)
							{
								if (lstDoors.Items[i].ImageIndex == 3)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								this.btnErrConnect.Visible = (flag ^ this.btnErrConnect.Visible);
							}
							else
							{
								this.btnErrConnect.Visible = flag;
							}
						}
					}
					catch (Exception)
					{
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
			this.Timer2.Enabled = true;
		}

		private void Button6_Click(object sender, EventArgs e)
		{
			try
			{
				dfrmManualSign dfrmManualSign = new dfrmManualSign();
				MethodInvoker method = new MethodInvoker(this.updateDisplay_Invoker);
				dfrmManualSign.curMeetingNo = this.curMeetingNo;
				if (dfrmManualSign.ShowDialog(this) == DialogResult.OK)
				{
					this.fillMeetingNum();
					base.BeginInvoke(method);
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

		public void getNewMeetingRecord()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.getNewMeetingRecord_Acc();
				return;
			}
			try
			{
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				string text = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, ";
				text += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll, t_d_MeetingConsumer.f_Seat, t_d_MeetingConsumer.f_MeetingIdentity ";
				text += string.Format(" FROM ((t_b_Consumer INNER JOIN t_d_SwipeRecord ON ( t_b_Consumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID)) LEFT JOIN  t_d_MeetingConsumer on ( t_d_MeetingConsumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID AND  f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo) + ") ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", new object[0]);
				text = text + " WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID.ToString();
				if (this.queryReaderStr != "")
				{
					text = text + " AND " + this.queryReaderStr;
				}
				string cmdText = text;
				SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				bool flag = false;
				if (sqlDataReader.HasRows)
				{
					while (sqlDataReader.Read())
					{
						string text2 = wgTools.SetObjToStr(sqlDataReader["f_ConsumerName"]);
						MjRec mjRec = new MjRec(sqlDataReader["f_RecordAll"].ToString());
						if (mjRec.IsSwipeRecord)
						{
							flag = true;
							if (text2 != "")
							{
								if (wgTools.SetObjToStr(sqlDataReader["f_MeetingIdentity"]) != "")
								{
									text2 = text2 + "." + frmMeetings.getStrMeetingIdentity(long.Parse(sqlDataReader["f_MeetingIdentity"].ToString()));
								}
								this.arrSignedUser.Add(text2);
								this.arrSignedSeat.Add(wgTools.SetObjToStr(sqlDataReader["f_Seat"]));
								this.arrSignedCardNo.Add(wgTools.SetObjToStr(sqlDataReader["f_CardNO"]));
							}
							else
							{
								text2 = wgTools.SetObjToStr(sqlDataReader["f_CardNO"]);
								this.arrSignedUser.Add(text2);
								this.arrSignedSeat.Add("!!!");
								this.arrSignedCardNo.Add(wgTools.SetObjToStr(sqlDataReader["f_CardNO"]));
							}
						}
					}
					if (this.arrSignedUser.Count > 100)
					{
						while (this.arrSignedUser.Count > 50)
						{
							this.arrSignedUser.RemoveAt(0);
							this.arrSignedSeat.RemoveAt(0);
							this.arrSignedCardNo.RemoveAt(0);
						}
					}
				}
				sqlDataReader.Close();
				sqlConnection.Close();
				if (flag)
				{
					if (this.arrSignedUser.Count > 0)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser1.Text = this.arrSignedUser[count - 1].ToString();
						this.txtSwipeSeat1.Text = this.arrSignedSeat[count - 1].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 1].ToString(), ref this.picSwipe1);
					}
					if (this.arrSignedUser.Count > 1)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser2.Text = this.arrSignedUser[count - 2].ToString();
						this.txtSwipeSeat2.Text = this.arrSignedSeat[count - 2].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 2].ToString(), ref this.picSwipe2);
					}
					if (this.arrSignedUser.Count > 2)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser3.Text = this.arrSignedUser[count - 3].ToString();
						this.txtSwipeSeat3.Text = this.arrSignedSeat[count - 3].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 3].ToString(), ref this.picSwipe3);
					}
					if (this.arrSignedUser.Count > 3)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser4.Text = this.arrSignedUser[count - 4].ToString();
						this.txtSwipeSeat4.Text = this.arrSignedSeat[count - 4].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 4].ToString(), ref this.picSwipe4);
					}
					if (this.arrSignedUser.Count > 4)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser5.Text = this.arrSignedUser[count - 5].ToString();
						this.txtSwipeSeat5.Text = this.arrSignedSeat[count - 5].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 5].ToString(), ref this.picSwipe5);
					}
					if (this.arrSignedUser.Count > 5)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser6.Text = this.arrSignedUser[count - 6].ToString();
						this.txtSwipeSeat6.Text = this.arrSignedSeat[count - 6].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 6].ToString(), ref this.picSwipe6);
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

		public void getNewMeetingRecord_Acc()
		{
			try
			{
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				string text = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, ";
				text += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll, t_d_MeetingConsumer.f_Seat, t_d_MeetingConsumer.f_MeetingIdentity ";
				text += string.Format(" FROM ((t_b_Consumer INNER JOIN t_d_SwipeRecord ON ( t_b_Consumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID)) LEFT JOIN  t_d_MeetingConsumer on ( t_d_MeetingConsumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID AND  f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo) + ") ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", new object[0]);
				text = text + " WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID.ToString();
				if (this.queryReaderStr != "")
				{
					text = text + " AND " + this.queryReaderStr;
				}
				string cmdText = text;
				OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				bool flag = false;
				if (oleDbDataReader.HasRows)
				{
					while (oleDbDataReader.Read())
					{
						string text2 = wgTools.SetObjToStr(oleDbDataReader["f_ConsumerName"]);
						MjRec mjRec = new MjRec(oleDbDataReader["f_RecordAll"].ToString());
						if (mjRec.IsSwipeRecord)
						{
							flag = true;
							if (text2 != "")
							{
								if (wgTools.SetObjToStr(oleDbDataReader["f_MeetingIdentity"]) != "")
								{
									text2 = text2 + "." + frmMeetings.getStrMeetingIdentity(long.Parse(oleDbDataReader["f_MeetingIdentity"].ToString()));
								}
								this.arrSignedUser.Add(text2);
								this.arrSignedSeat.Add(wgTools.SetObjToStr(oleDbDataReader["f_Seat"]));
								this.arrSignedCardNo.Add(wgTools.SetObjToStr(oleDbDataReader["f_CardNO"]));
							}
							else
							{
								text2 = wgTools.SetObjToStr(oleDbDataReader["f_CardNO"]);
								this.arrSignedUser.Add(text2);
								this.arrSignedSeat.Add("!!!");
								this.arrSignedCardNo.Add(wgTools.SetObjToStr(oleDbDataReader["f_CardNO"]));
							}
						}
					}
					if (this.arrSignedUser.Count > 100)
					{
						while (this.arrSignedUser.Count > 50)
						{
							this.arrSignedUser.RemoveAt(0);
							this.arrSignedSeat.RemoveAt(0);
							this.arrSignedCardNo.RemoveAt(0);
						}
					}
				}
				oleDbDataReader.Close();
				oleDbConnection.Close();
				if (flag)
				{
					if (this.arrSignedUser.Count > 0)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser1.Text = this.arrSignedUser[count - 1].ToString();
						this.txtSwipeSeat1.Text = this.arrSignedSeat[count - 1].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 1].ToString(), ref this.picSwipe1);
					}
					if (this.arrSignedUser.Count > 1)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser2.Text = this.arrSignedUser[count - 2].ToString();
						this.txtSwipeSeat2.Text = this.arrSignedSeat[count - 2].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 2].ToString(), ref this.picSwipe2);
					}
					if (this.arrSignedUser.Count > 2)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser3.Text = this.arrSignedUser[count - 3].ToString();
						this.txtSwipeSeat3.Text = this.arrSignedSeat[count - 3].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 3].ToString(), ref this.picSwipe3);
					}
					if (this.arrSignedUser.Count > 3)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser4.Text = this.arrSignedUser[count - 4].ToString();
						this.txtSwipeSeat4.Text = this.arrSignedSeat[count - 4].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 4].ToString(), ref this.picSwipe4);
					}
					if (this.arrSignedUser.Count > 4)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser5.Text = this.arrSignedUser[count - 5].ToString();
						this.txtSwipeSeat5.Text = this.arrSignedSeat[count - 5].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 5].ToString(), ref this.picSwipe5);
					}
					if (this.arrSignedUser.Count > 5)
					{
						int count = this.arrSignedUser.Count;
						this.txtSwipeUser6.Text = this.arrSignedUser[count - 6].ToString();
						this.txtSwipeSeat6.Text = this.arrSignedSeat[count - 6].ToString();
						this._loadPhoto(this.arrSignedCardNo[count - 6].ToString(), ref this.picSwipe6);
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

		private void _loadPhoto(string strCardno, ref PictureBox pic)
		{
			try
			{
				string fileToDisplay;
				if (strCardno.Trim() == "")
				{
					fileToDisplay = null;
				}
				else
				{
					fileToDisplay = wgAppConfig.getPhotoFileName(long.Parse(strCardno.Trim()));
				}
				Image image = pic.Image;
				wgAppConfig.ShowMyImage(fileToDisplay, ref image);
				pic.Image = image;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dfrmMeetingSign_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				if (this.frmWatch != null)
				{
					this.frmWatch.Close();
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

		private void Button7_Click(object sender, EventArgs e)
		{
			try
			{
				base.TopMost = false;
				dfrmMeetingStatDetail dfrmMeetingStatDetail = new dfrmMeetingStatDetail();
				MethodInvoker method = new MethodInvoker(this.updateDisplay_Invoker);
				dfrmMeetingStatDetail.curMeetingNo = this.curMeetingNo;
				if (sender == this.Button1)
				{
					dfrmMeetingStatDetail.tabControl1.SelectedTab = dfrmMeetingStatDetail.tabControl1.TabPages[0];
				}
				if (sender == this.Button2)
				{
					dfrmMeetingStatDetail.tabControl1.SelectedTab = dfrmMeetingStatDetail.tabControl1.TabPages[1];
				}
				if (sender == this.Button3)
				{
					dfrmMeetingStatDetail.tabControl1.SelectedTab = dfrmMeetingStatDetail.tabControl1.TabPages[2];
				}
				if (sender == this.Button4)
				{
					dfrmMeetingStatDetail.tabControl1.SelectedTab = dfrmMeetingStatDetail.tabControl1.TabPages[3];
				}
				if (sender == this.Button5)
				{
					dfrmMeetingStatDetail.tabControl1.SelectedTab = dfrmMeetingStatDetail.tabControl1.TabPages[5];
				}
				if (sender == this.Button7)
				{
					dfrmMeetingStatDetail.tabControl1.SelectedTab = dfrmMeetingStatDetail.tabControl1.TabPages[5];
				}
				dfrmMeetingStatDetail.ShowDialog(this);
				this.fillMeetingNum();
				base.BeginInvoke(method);
				base.TopMost = true;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void TimerStartSlow_Tick(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		}

		private void dfrmMeetingSign_SizeChanged(object sender, EventArgs e)
		{
			GroupBox[] array = new GroupBox[]
			{
				this.GroupBox2,
				this.GroupBox3,
				this.GroupBox4,
				this.GroupBox6,
				this.GroupBox7
			};
			for (int i = 0; i < 5; i++)
			{
				array[i].Size = new Size(this.flowLayoutPanel1.Width / 5 - 8, this.flowLayoutPanel1.Height - 18);
			}
		}

		private void GroupBox1_SizeChanged(object sender, EventArgs e)
		{
		}

		private void button8_Click(object sender, EventArgs e)
		{
			using (dfrmInterfaceLock dfrmInterfaceLock = new dfrmInterfaceLock())
			{
				dfrmInterfaceLock.txtOperatorName.Text = icOperator.OperatorName;
				dfrmInterfaceLock.StartPosition = FormStartPosition.CenterScreen;
				dfrmInterfaceLock.ShowDialog(this);
			}
		}

		private void dfrmMeetingSign_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (this.frmWatch != null)
				{
					this.frmWatch.Close();
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			try
			{
				if (this.startSlowThread != null && this.startSlowThread.IsAlive)
				{
					this.startSlowThread.Interrupt();
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			wgAppConfig.DisposeImage(this.picSwipe1.Image);
			wgAppConfig.DisposeImage(this.picSwipe2.Image);
			wgAppConfig.DisposeImage(this.picSwipe3.Image);
			wgAppConfig.DisposeImage(this.picSwipe4.Image);
			wgAppConfig.DisposeImage(this.picSwipe5.Image);
			wgAppConfig.DisposeImage(this.picSwipe6.Image);
		}
	}
}
