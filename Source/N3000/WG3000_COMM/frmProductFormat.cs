using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;

namespace WG3000_COMM
{
	public class frmProductFormat : frmN3000
	{
		private delegate void dispDoorStatusByIPComm(icController control);

		private delegate void autoFormatLog(icController control);

		private delegate void pingErrLog(string ErrInfo);

		private IContainer components;

		private TextBox txtSN;

		private TextBox txtTime;

		private Label lblTime;

		private Label label1;

		private System.Windows.Forms.Timer timer1;

		private Button button1;

		private Button btnFormat;

		private CheckBox checkBox1;

		private Button btnConnected;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private TextBox txtRunInfo;

		private Label label9;

		private Label lblFailDetail;

		private BackgroundWorker backgroundWorker1;

		private CheckBox chkAutoFormat;

		private Button btnAdjustTime;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private Label label143;

		private NumericUpDown numericUpDown22;

		private Button button72;

		private CheckBox checkBox131;

		private CheckBox checkBox130;

		private Button button70;

		private CheckBox checkBox117;

		private CheckBox checkBox116;

		private CheckBox checkBox113;

		private Button button57;

		private Label label146;

		private TextBox textBox32;

		private Label lblFloor;

		private Panel panel1;

		private Label label147;

		private Button btnStop;

		private Label label11;

		private NumericUpDown numericUpDown2;

		private Label label10;

		private NumericUpDown numericUpDown1;

		private GroupBox groupBox1;

		private RadioButton radioButton2;

		private RadioButton optNO;

		private Label lblCommLose;

		private Button btnPing;

		private Label label12;

		private string lastControllerInfo = "";

		private long lastErrController;

		private bool bStopRemoteEvalator;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmProductFormat));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.backgroundWorker1 = new BackgroundWorker();
			this.btnPing = new Button();
			this.lblCommLose = new Label();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.groupBox1 = new GroupBox();
			this.radioButton2 = new RadioButton();
			this.optNO = new RadioButton();
			this.label11 = new Label();
			this.numericUpDown2 = new NumericUpDown();
			this.label10 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.btnStop = new Button();
			this.lblFloor = new Label();
			this.label143 = new Label();
			this.numericUpDown22 = new NumericUpDown();
			this.button72 = new Button();
			this.checkBox131 = new CheckBox();
			this.checkBox130 = new CheckBox();
			this.button70 = new Button();
			this.tabPage2 = new TabPage();
			this.label146 = new Label();
			this.textBox32 = new TextBox();
			this.button57 = new Button();
			this.checkBox117 = new CheckBox();
			this.checkBox116 = new CheckBox();
			this.checkBox113 = new CheckBox();
			this.txtRunInfo = new TextBox();
			this.label4 = new Label();
			this.label7 = new Label();
			this.label3 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label9 = new Label();
			this.label8 = new Label();
			this.lblFailDetail = new Label();
			this.label2 = new Label();
			this.btnConnected = new Button();
			this.chkAutoFormat = new CheckBox();
			this.checkBox1 = new CheckBox();
			this.btnFormat = new Button();
			this.btnAdjustTime = new Button();
			this.button1 = new Button();
			this.label1 = new Label();
			this.lblTime = new Label();
			this.txtTime = new TextBox();
			this.txtSN = new TextBox();
			this.panel1 = new Panel();
			this.label147 = new Label();
			this.label12 = new Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			((ISupportInitialize)this.numericUpDown22).BeginInit();
			this.tabPage2.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.btnPing.Location = new Point(827, 97);
			this.btnPing.Name = "btnPing";
			this.btnPing.Size = new Size(75, 23);
			this.btnPing.TabIndex = 106;
			this.btnPing.Text = "检测丢包";
			this.btnPing.UseVisualStyleBackColor = true;
			this.btnPing.Click += new EventHandler(this.btnPing_Click);
			this.lblCommLose.AutoSize = true;
			this.lblCommLose.BackColor = Color.Red;
			this.lblCommLose.ForeColor = Color.White;
			this.lblCommLose.Location = new Point(323, 75);
			this.lblCommLose.Name = "lblCommLose";
			this.lblCommLose.Size = new Size(65, 12);
			this.lblCommLose.TabIndex = 105;
			this.lblCommLose.Text = "通信有丢包";
			this.lblCommLose.Visible = false;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new Point(12, 405);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(946, 204);
			this.tabControl1.TabIndex = 10;
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.numericUpDown2);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.numericUpDown1);
			this.tabPage1.Controls.Add(this.btnStop);
			this.tabPage1.Controls.Add(this.lblFloor);
			this.tabPage1.Controls.Add(this.label143);
			this.tabPage1.Controls.Add(this.numericUpDown22);
			this.tabPage1.Controls.Add(this.button72);
			this.tabPage1.Controls.Add(this.checkBox131);
			this.tabPage1.Controls.Add(this.checkBox130);
			this.tabPage1.Controls.Add(this.button70);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(938, 178);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "电梯";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.optNO);
			this.groupBox1.Location = new Point(20, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(150, 35);
			this.groupBox1.TabIndex = 107;
			this.groupBox1.TabStop = false;
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new Point(99, 12);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(35, 16);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "NC";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.optNO.AutoSize = true;
			this.optNO.Checked = true;
			this.optNO.Location = new Point(12, 12);
			this.optNO.Name = "optNO";
			this.optNO.Size = new Size(35, 16);
			this.optNO.TabIndex = 0;
			this.optNO.TabStop = true;
			this.optNO.Text = "NO";
			this.optNO.UseVisualStyleBackColor = true;
			this.label11.AutoSize = true;
			this.label11.Location = new Point(239, 111);
			this.label11.Name = "label11";
			this.label11.Size = new Size(95, 12);
			this.label11.TabIndex = 106;
			this.label11.Text = "21-40的开始楼层";
			this.numericUpDown2.Location = new Point(337, 106);
			NumericUpDown arg_821_0 = this.numericUpDown2;
			int[] array = new int[4];
			array[0] = 40;
			arg_821_0.Maximum = new decimal(array);
			NumericUpDown arg_83E_0 = this.numericUpDown2;
			int[] array2 = new int[4];
			array2[0] = 21;
			arg_83E_0.Minimum = new decimal(array2);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new Size(53, 21);
			this.numericUpDown2.TabIndex = 105;
			NumericUpDown arg_88C_0 = this.numericUpDown2;
			int[] array3 = new int[4];
			array3[0] = 21;
			arg_88C_0.Value = new decimal(array3);
			this.label10.AutoSize = true;
			this.label10.Location = new Point(239, 78);
			this.label10.Name = "label10";
			this.label10.Size = new Size(89, 12);
			this.label10.TabIndex = 104;
			this.label10.Text = "1-20的开始楼层";
			this.numericUpDown1.Location = new Point(337, 73);
			NumericUpDown arg_927_0 = this.numericUpDown1;
			int[] array4 = new int[4];
			array4[0] = 20;
			arg_927_0.Maximum = new decimal(array4);
			NumericUpDown arg_946_0 = this.numericUpDown1;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_946_0.Minimum = new decimal(array5);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new Size(53, 21);
			this.numericUpDown1.TabIndex = 103;
			NumericUpDown arg_996_0 = this.numericUpDown1;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_996_0.Value = new decimal(array6);
			this.btnStop.Location = new Point(437, 73);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new Size(192, 50);
			this.btnStop.TabIndex = 102;
			this.btnStop.Text = "停止远程电梯";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new EventHandler(this.btnStop_Click);
			this.lblFloor.AutoSize = true;
			this.lblFloor.Location = new Point(97, 51);
			this.lblFloor.Name = "lblFloor";
			this.lblFloor.Size = new Size(29, 12);
			this.lblFloor.TabIndex = 101;
			this.lblFloor.Text = "----";
			this.label143.AutoSize = true;
			this.label143.Location = new Point(176, 21);
			this.label143.Name = "label143";
			this.label143.Size = new Size(83, 12);
			this.label143.TabIndex = 100;
			this.label143.Text = "22_间隔(毫秒)";
			this.numericUpDown22.Location = new Point(261, 16);
			NumericUpDown arg_B13_0 = this.numericUpDown22;
			int[] array7 = new int[4];
			array7[0] = 20000;
			arg_B13_0.Maximum = new decimal(array7);
			this.numericUpDown22.Name = "numericUpDown22";
			this.numericUpDown22.Size = new Size(53, 21);
			this.numericUpDown22.TabIndex = 99;
			NumericUpDown arg_B67_0 = this.numericUpDown22;
			int[] array8 = new int[4];
			array8[0] = 2500;
			arg_B67_0.Value = new decimal(array8);
			this.button72.Location = new Point(20, 107);
			this.button72.Name = "button72";
			this.button72.Size = new Size(192, 20);
			this.button72.TabIndex = 98;
			this.button72.Text = "72 远程到21-40楼层[IP]";
			this.button72.UseVisualStyleBackColor = true;
			this.button72.Click += new EventHandler(this.button70_Click);
			this.checkBox131.AutoSize = true;
			this.checkBox131.Checked = true;
			this.checkBox131.CheckState = CheckState.Checked;
			this.checkBox131.Location = new Point(467, 13);
			this.checkBox131.Name = "checkBox131";
			this.checkBox131.Size = new Size(60, 16);
			this.checkBox131.TabIndex = 97;
			this.checkBox131.Text = "131_NC";
			this.checkBox131.UseVisualStyleBackColor = true;
			this.checkBox131.Visible = false;
			this.checkBox130.AutoSize = true;
			this.checkBox130.Checked = true;
			this.checkBox130.CheckState = CheckState.Checked;
			this.checkBox130.Location = new Point(388, 13);
			this.checkBox130.Name = "checkBox130";
			this.checkBox130.Size = new Size(60, 16);
			this.checkBox130.TabIndex = 96;
			this.checkBox130.Text = "130_NO";
			this.checkBox130.UseVisualStyleBackColor = true;
			this.checkBox130.Visible = false;
			this.button70.Location = new Point(20, 71);
			this.button70.Name = "button70";
			this.button70.Size = new Size(192, 20);
			this.button70.TabIndex = 95;
			this.button70.Text = "70 远程到1-20楼层[IP]";
			this.button70.UseVisualStyleBackColor = true;
			this.button70.Click += new EventHandler(this.button70_Click);
			this.tabPage2.Controls.Add(this.label146);
			this.tabPage2.Controls.Add(this.textBox32);
			this.tabPage2.Controls.Add(this.button57);
			this.tabPage2.Controls.Add(this.checkBox117);
			this.tabPage2.Controls.Add(this.checkBox116);
			this.tabPage2.Controls.Add(this.checkBox113);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(938, 178);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.label146.AutoSize = true;
			this.label146.Location = new Point(375, 12);
			this.label146.Name = "label146";
			this.label146.Size = new Size(101, 12);
			this.label146.TabIndex = 25;
			this.label146.Text = "用 逗号 分开卡号";
			this.textBox32.Location = new Point(376, 27);
			this.textBox32.Multiline = true;
			this.textBox32.Name = "textBox32";
			this.textBox32.Size = new Size(187, 140);
			this.textBox32.TabIndex = 24;
			this.textBox32.Text = "7314494,  3659085, 707080, 3654261, 20760517, 3660918";
			this.button57.Location = new Point(204, 17);
			this.button57.Name = "button57";
			this.button57.Size = new Size(126, 38);
			this.button57.TabIndex = 22;
			this.button57.Text = "57 先作参数初始化 再作特殊设置 ";
			this.button57.UseVisualStyleBackColor = true;
			this.button57.Click += new EventHandler(this.button57_Click);
			this.checkBox117.AutoSize = true;
			this.checkBox117.Checked = true;
			this.checkBox117.CheckState = CheckState.Checked;
			this.checkBox117.Location = new Point(26, 123);
			this.checkBox117.Name = "checkBox117";
			this.checkBox117.Size = new Size(258, 16);
			this.checkBox117.TabIndex = 21;
			this.checkBox117.Text = "117 (恢复默认参数后) 门磁对应扩展板输出";
			this.checkBox117.UseVisualStyleBackColor = true;
			this.checkBox116.AutoSize = true;
			this.checkBox116.Checked = true;
			this.checkBox116.CheckState = CheckState.Checked;
			this.checkBox116.Location = new Point(26, 20);
			this.checkBox116.Name = "checkBox116";
			this.checkBox116.Size = new Size(120, 16);
			this.checkBox116.TabIndex = 20;
			this.checkBox116.Text = "116 加入卡号权限";
			this.checkBox116.UseVisualStyleBackColor = true;
			this.checkBox113.AutoSize = true;
			this.checkBox113.Checked = true;
			this.checkBox113.CheckState = CheckState.Checked;
			this.checkBox113.Location = new Point(26, 101);
			this.checkBox113.Name = "checkBox113";
			this.checkBox113.Size = new Size(120, 16);
			this.checkBox113.TabIndex = 19;
			this.checkBox113.Text = "113 同时校准时间";
			this.checkBox113.UseVisualStyleBackColor = true;
			this.txtRunInfo.Location = new Point(12, 6);
			this.txtRunInfo.Multiline = true;
			this.txtRunInfo.Name = "txtRunInfo";
			this.txtRunInfo.Size = new Size(225, 255);
			this.txtRunInfo.TabIndex = 9;
			this.label4.AutoSize = true;
			this.label4.ForeColor = Color.White;
			this.label4.Location = new Point(561, 69);
			this.label4.Name = "label4";
			this.label4.Size = new Size(17, 12);
			this.label4.TabIndex = 8;
			this.label4.Text = "00";
			this.label7.AutoSize = true;
			this.label7.ForeColor = Color.White;
			this.label7.Location = new Point(562, 6);
			this.label7.Name = "label7";
			this.label7.Size = new Size(17, 12);
			this.label7.TabIndex = 8;
			this.label7.Text = "00";
			this.label3.AutoSize = true;
			this.label3.ForeColor = Color.White;
			this.label3.Location = new Point(561, 47);
			this.label3.Name = "label3";
			this.label3.Size = new Size(17, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "00";
			this.label5.AutoSize = true;
			this.label5.ForeColor = Color.White;
			this.label5.Location = new Point(481, 69);
			this.label5.Name = "label5";
			this.label5.Size = new Size(47, 12);
			this.label5.TabIndex = 8;
			this.label5.Text = "故障号:";
			this.label6.AutoSize = true;
			this.label6.ForeColor = Color.White;
			this.label6.Location = new Point(481, 6);
			this.label6.Name = "label6";
			this.label6.Size = new Size(59, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "驱动版本:";
			this.label9.AutoSize = true;
			this.label9.ForeColor = Color.White;
			this.label9.Location = new Point(735, 190);
			this.label9.Name = "label9";
			this.label9.Size = new Size(167, 48);
			this.label9.TabIndex = 8;
			this.label9.Text = "104 时表示只是时钟问题; \r\n103 时表示时钟和100脚有问题\r\n其他百位=1表示时钟问题\r\n    十个=相应管脚问题";
			this.label9.Visible = false;
			this.label8.AutoSize = true;
			this.label8.ForeColor = Color.White;
			this.label8.Location = new Point(481, 25);
			this.label8.Name = "label8";
			this.label8.Size = new Size(35, 12);
			this.label8.TabIndex = 8;
			this.label8.Text = "时钟:";
			this.lblFailDetail.AutoSize = true;
			this.lblFailDetail.ForeColor = Color.White;
			this.lblFailDetail.Location = new Point(610, 47);
			this.lblFailDetail.Name = "lblFailDetail";
			this.lblFailDetail.Size = new Size(35, 12);
			this.lblFailDetail.TabIndex = 8;
			this.lblFailDetail.Text = "说明:";
			this.label2.AutoSize = true;
			this.label2.ForeColor = Color.White;
			this.label2.Location = new Point(481, 47);
			this.label2.Name = "label2";
			this.label2.Size = new Size(83, 12);
			this.label2.TabIndex = 8;
			this.label2.Text = "有问题管脚号:";
			this.btnConnected.BackColor = Color.Red;
			this.btnConnected.Location = new Point(325, 25);
			this.btnConnected.Name = "btnConnected";
			this.btnConnected.Size = new Size(126, 42);
			this.btnConnected.TabIndex = 7;
			this.btnConnected.UseVisualStyleBackColor = false;
			this.chkAutoFormat.AutoSize = true;
			this.chkAutoFormat.ForeColor = Color.White;
			this.chkAutoFormat.Location = new Point(244, 5);
			this.chkAutoFormat.Name = "chkAutoFormat";
			this.chkAutoFormat.Size = new Size(156, 16);
			this.chkAutoFormat.TabIndex = 6;
			this.chkAutoFormat.Text = "全部通过时, 执行格式化";
			this.chkAutoFormat.UseVisualStyleBackColor = true;
			this.chkAutoFormat.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.checkBox1.AutoSize = true;
			this.checkBox1.ForeColor = Color.White;
			this.checkBox1.Location = new Point(814, 35);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(144, 16);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "停止搜索, 执行格式化";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.btnFormat.Location = new Point(827, 64);
			this.btnFormat.Name = "btnFormat";
			this.btnFormat.Size = new Size(75, 23);
			this.btnFormat.TabIndex = 5;
			this.btnFormat.Text = "格式化";
			this.btnFormat.UseVisualStyleBackColor = true;
			this.btnFormat.Visible = false;
			this.btnFormat.Click += new EventHandler(this.button2_Click);
			this.btnAdjustTime.Location = new Point(738, 64);
			this.btnAdjustTime.Name = "btnAdjustTime";
			this.btnAdjustTime.Size = new Size(75, 23);
			this.btnAdjustTime.TabIndex = 4;
			this.btnAdjustTime.Text = "校准时间";
			this.btnAdjustTime.UseVisualStyleBackColor = true;
			this.btnAdjustTime.Click += new EventHandler(this.btnAdjustTime_Click);
			this.button1.Location = new Point(243, 31);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "清空";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new Font("宋体", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(733, 97);
			this.label1.Name = "label1";
			this.label1.Size = new Size(94, 21);
			this.label1.TabIndex = 3;
			this.label1.Text = "电脑时间";
			this.lblTime.AutoSize = true;
			this.lblTime.Font = new Font("宋体", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.lblTime.ForeColor = Color.White;
			this.lblTime.Location = new Point(733, 128);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new Size(76, 21);
			this.lblTime.TabIndex = 2;
			this.lblTime.Text = "label1";
			this.txtTime.Font = new Font("宋体", 72f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.txtTime.ForeColor = Color.Black;
			this.txtTime.Location = new Point(12, 264);
			this.txtTime.Name = "txtTime";
			this.txtTime.Size = new Size(946, 117);
			this.txtTime.TabIndex = 1;
			this.txtTime.Text = "2010-10-28 12:59:59";
			this.txtTime.TextAlign = HorizontalAlignment.Center;
			this.txtSN.Font = new Font("宋体", 72f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.txtSN.ForeColor = Color.Black;
			this.txtSN.Location = new Point(243, 96);
			this.txtSN.Name = "txtSN";
			this.txtSN.Size = new Size(490, 117);
			this.txtSN.TabIndex = 0;
			this.txtSN.Text = "999999999";
			this.txtSN.TextAlign = HorizontalAlignment.Center;
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label147);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(971, 391);
			this.panel1.TabIndex = 104;
			this.label147.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.label147.AutoSize = true;
			this.label147.ForeColor = Color.White;
			this.label147.Location = new Point(42, 18);
			this.label147.Name = "label147";
			this.label147.Size = new Size(389, 60);
			this.label147.TabIndex = 0;
			this.label147.Text = "You're Welcome!\r\n支持.NET2.0 2011-12-21_08:44:32\r\n增加丢包检测 [如果存在丢包, 则格式化通不过]--2011-11-14_13:18:32\r\n增加大数据包检测 2012-3-21_14:59:57\r\n增加格式化提示  2012-6-12_14:21:41";
			this.label12.AutoSize = true;
			this.label12.BackColor = Color.Red;
			this.label12.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.label12.ForeColor = Color.White;
			this.label12.Location = new Point(239, 241);
			this.label12.Name = "label12";
			this.label12.Size = new Size(609, 20);
			this.label12.TabIndex = 107;
			this.label12.Text = "格式化时, 必须控制器格式化完成后(CPU灯正常闪烁), 才能断电!!!";
			this.label12.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.ClientSize = new Size(971, 391);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnPing);
			base.Controls.Add(this.lblCommLose);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.txtRunInfo);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.lblFailDetail);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnConnected);
			base.Controls.Add(this.chkAutoFormat);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.btnFormat);
			base.Controls.Add(this.btnAdjustTime);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lblTime);
			base.Controls.Add(this.txtTime);
			base.Controls.Add(this.txtSN);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmProductFormat";
			this.Text = "Search And Format ";
			base.FormClosing += new FormClosingEventHandler(this.frmProductFormat_FormClosing);
			base.Load += new EventHandler(this.frmProductFormat_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			((ISupportInitialize)this.numericUpDown22).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmProductFormat()
		{
			this.InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			this.lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.timer1.Enabled = true;
		}

		private void frmProductFormat_Load(object sender, EventArgs e)
		{
			this.Text = this.Text + " V" + Application.ProductVersion;
			this.panel1.Visible = false;
			if (!this.backgroundWorker1.IsBusy)
			{
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = -1;
				byte[] array = new byte[1152];
				array[1027] = 165;
				array[1026] = 165;
				array[1025] = 165;
				array[1024] = 165;
				icController.UpdateConfigureSuperIP(array);
				this.label12.Visible = true;
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.btnFormat.Visible = this.checkBox1.Checked;
			if (this.checkBox1.Checked)
			{
				this.btnConnected.BackColor = Color.Red;
				this.btnConnected.Visible = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.txtTime.Text = "";
			this.txtSN.Text = "";
			this.txtRunInfo.Text = "";
			this.lastErrController = 0L;
		}

		private void frmProductFormat_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		public static void wgLogProduct(string strMsg, string filename)
		{
			try
			{
				string text = string.Concat(new object[]
				{
					icOperator.OperatorID,
					".",
					icOperator.OperatorName,
					".",
					strMsg
				});
				text = DateTime.Now.ToString("yyyy-MM-dd H-mm-ss") + "\t" + text;
				using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\" + filename + ".log", true))
				{
					streamWriter.WriteLine(text);
				}
			}
			catch (Exception)
			{
			}
		}

		private void dispDoorStatusByIPCommEntry(icController control)
		{
			if (control.runinfo.wgcticks > 0u)
			{
				if (string.Compare(this.txtSN.Text, control.runinfo.CurrentControllerSN.ToString()) != 0)
				{
					this.txtSN.Text = control.runinfo.CurrentControllerSN.ToString();
				}
				this.txtTime.Text = control.runinfo.dtNow.ToString("yyyy-MM-dd HH:mm:ss");
				bool flag = false;
				if (Math.Abs(DateTime.Now.Subtract(control.runinfo.dtNow).TotalMinutes) >= 5.0)
				{
					this.txtTime.BackColor = Color.Red;
					this.label8.Text = "时钟: 有问题";
					flag = true;
				}
				else
				{
					this.txtTime.BackColor = Color.White;
					this.label8.Text = "时钟:";
				}
				if (control.runinfo.reservedBytes[0] > 0)
				{
					this.btnConnected.BackColor = Color.Yellow;
					if (this.label3.Text != control.runinfo.reservedBytes[0].ToString())
					{
						this.label3.Text = control.runinfo.reservedBytes[0].ToString();
						this.lblFailDetail.Text = icDesc.failedPinDesc((int)control.runinfo.reservedBytes[0]);
					}
				}
				else if (!string.IsNullOrEmpty(this.label3.Text))
				{
					this.label3.Text = "";
					this.lblFailDetail.Text = "";
				}
				if (control.runinfo.appError > 0)
				{
					this.btnConnected.BackColor = Color.Yellow;
					if (this.label4.Text != control.runinfo.appError.ToString())
					{
						this.label4.Text = control.runinfo.appError.ToString();
					}
				}
				else if (!string.IsNullOrEmpty(this.label4.Text))
				{
					this.label4.Text = "";
				}
				if (control.runinfo.reservedBytes[0] <= 0 && control.runinfo.appError == 0 && !flag)
				{
					this.btnConnected.BackColor = Color.Green;
				}
				if (control.ControllerDriverMainVer.ToString() != this.label7.Text)
				{
					this.label7.Text = control.runinfo.driverVersion;
				}
				this.btnConnected.Visible = !this.btnConnected.Visible;
				if (this.lastControllerInfo != control.runinfo.CurrentControllerSN.ToString() + control.runinfo.reservedBytes[0].ToString())
				{
					this.lastControllerInfo = control.runinfo.CurrentControllerSN.ToString() + control.runinfo.reservedBytes[0].ToString();
					string text = "";
					if (control.runinfo.reservedBytes[0] == 0)
					{
						text += string.Format("管脚没问题\t", new object[0]);
					}
					else
					{
						text += string.Format("failedPin 问题管脚号: {0}\r\n\t", control.runinfo.reservedBytes[0]);
						text += icDesc.failedPinDesc((int)control.runinfo.reservedBytes[0]);
						if ((control.runinfo.reservedBytes[1] & 240) == 0)
						{
							text += string.Format("\tfailedPinDesc 问题管脚PORT号: G{0:X}\r\n", control.runinfo.reservedBytes[1]);
						}
						else
						{
							text += string.Format("\tfailedPinDesc 问题管脚PORT号: {0:X2}\r\n", control.runinfo.reservedBytes[1]);
						}
						text += string.Format("\tfailedPinDiffPortType 问题管脚PORT类: {0:X2}\r\n", control.runinfo.reservedBytes[2]);
						string text2 = "";
						switch (control.runinfo.reservedBytes[2] >> 4)
						{
						case 1:
							text2 = "初始默认就有问题";
							break;
						case 2:
							text2 = "管脚高平设置时 就有问题";
							break;
						case 3:
							text2 = "管脚高平设置时 此脚 就有问题";
							break;
						case 4:
							text2 = "管脚低平设置时 就有问题";
							break;
						case 5:
							text2 = "管脚低平设置时 此脚 就有问题";
							break;
						}
						if ((control.runinfo.reservedBytes[2] & 15) == 0)
						{
							text += string.Format("\t产生问题的另一端口PORT= PORTG\r\n", new object[0]);
						}
						else
						{
							text += string.Format("\t产生问题的另一端口PORT: PORT{0:X}\r\n", (int)(control.runinfo.reservedBytes[2] & 15));
						}
						if (text2 != "")
						{
							text = text + text2 + "\r\n";
						}
						text += string.Format("\tfailedPinDiff 存在不同: {0:X2}\r\n", control.runinfo.reservedBytes[3]);
					}
					frmProductFormat.wgLogProduct(this.lastControllerInfo + ":" + text + string.Format("\t所有数据: {0}", control.runinfo.BytesDataStr), "n3k_Product");
					return;
				}
			}
			else
			{
				this.btnConnected.BackColor = Color.Red;
				this.btnConnected.Visible = true;
			}
		}

		private void autoFormatLogEntry(icController control)
		{
			this.txtRunInfo.AppendText("格式化: " + control.ControllerSN.ToString() + "\r\n");
			wgAppConfig.wgLogWithoutDB("格式化: " + control.ControllerSN.ToString(), EventLogEntryType.Warning, null);
			frmProductFormat.wgLogProduct("格式化: " + control.ControllerSN.ToString(), "n3k_Format");
			this.label12.Visible = true;
		}

		private void pingErrLogEntry(string ErrInfo)
		{
			if (string.IsNullOrEmpty(ErrInfo))
			{
				this.lblCommLose.Visible = false;
				return;
			}
			this.btnConnected.BackColor = Color.Yellow;
			this.lblCommLose.Visible = true;
			this.txtRunInfo.AppendText(ErrInfo);
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			icController icController = new icController();
			try
			{
				Thread.Sleep(300);
				while (true)
				{
					if (!this.checkBox1.Checked)
					{
						try
						{
							icController.ControllerSN = -1;
							icController.runinfo.Clear();
							icController.GetControllerRunInformationIPNoTries();
							base.Invoke(new frmProductFormat.dispDoorStatusByIPComm(this.dispDoorStatusByIPCommEntry), new object[]
							{
								icController
							});
							if (this.chkAutoFormat.Checked && icController.runinfo.wgcticks > 0u && Math.Abs(DateTime.Now.Subtract(icController.runinfo.dtNow).TotalMinutes) < 5.0 && icController.runinfo.reservedBytes[0] == 0 && icController.runinfo.appError == 0)
							{
								icController.ControllerSN = (int)icController.runinfo.CurrentControllerSN;
								if (this.lastErrController == (long)icController.ControllerSN)
								{
									continue;
								}
								int num = 0;
								int num2 = 0;
								int num3 = 0;
								for (int i = 0; i < 200; i++)
								{
									num++;
									if (icController.SpecialPingIP() == 1)
									{
										num2++;
									}
									else
									{
										num3++;
									}
								}
								if (num != num2)
								{
									if (this.lastErrController != (long)icController.ControllerSN)
									{
										this.lastErrController = (long)icController.ControllerSN;
										string text = string.Format("SN{3} 有故障: 通信丢包\r\n 已发送={0}, 已接收={1}, 丢失 = {2}", new object[]
										{
											num,
											num2,
											num3,
											icController.ControllerSN
										}) + "\r\n";
										base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
										{
											text
										});
									}
								}
								else
								{
									wgUdpComm.triesTotal = 0L;
									wgTools.WriteLine("control.Test1024 Start");
									int num4 = 0;
									string text2 = "";
									int num5 = icController.test1024Write();
									if (num5 < 0)
									{
										text2 += "大数据包写入失败\r\n";
									}
									num5 = icController.test1024Read(100u, ref num4);
									if (num5 < 0)
									{
										text2 = text2 + "大数据包读取失败: " + num5.ToString() + "\r\n";
									}
									if (wgUdpComm.triesTotal > 0L)
									{
										text2 = text2 + "测试中重试次数 = " + wgUdpComm.triesTotal.ToString() + "\r\n";
									}
									if (text2 != "")
									{
										if (this.lastErrController != (long)icController.ControllerSN)
										{
											this.lastErrController = (long)icController.ControllerSN;
											string text3 = string.Concat(new string[]
											{
												"SN",
												icController.ControllerSN.ToString(),
												"通信有故障: : ",
												text2,
												"\r\n"
											});
											base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
											{
												text3
											});
										}
									}
									else
									{
										string text4 = "";
										base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
										{
											text4
										});
										this.lastErrController = 0L;
										byte[] array = new byte[1152];
										array[1027] = 165;
										array[1026] = 165;
										array[1025] = 165;
										array[1024] = 165;
										icController.UpdateConfigureSuperIP(array);
										base.Invoke(new frmProductFormat.autoFormatLog(this.autoFormatLogEntry), new object[]
										{
											icController
										});
									}
								}
							}
						}
						catch (Exception ex)
						{
							wgTools.WgDebugWrite(ex.ToString(), new object[0]);
							break;
						}
					}
					Thread.Sleep(300);
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void btnAdjustTime_Click(object sender, EventArgs e)
		{
			int controllerSN;
			if (int.TryParse(this.txtSN.Text, out controllerSN))
			{
				icController icController = new icController();
				try
				{
					icController.ControllerSN = controllerSN;
					icController.AdjustTimeIP(DateTime.Now);
					frmProductFormat.wgLogProduct("校准时间: " + icController.ControllerSN.ToString(), "n3k_Format");
				}
				catch (Exception)
				{
				}
				icController.Dispose();
			}
		}

		private void button70_Click(object sender, EventArgs e)
		{
			this.button70.Enabled = false;
			this.button72.Enabled = false;
			Cursor.Current = Cursors.WaitCursor;
			icController icController = new icController();
			try
			{
				try
				{
					this.bStopRemoteEvalator = false;
					uint operatorId = 0u;
					ulong operatorCardNO = 18446744073709551615uL;
					icController.ControllerSN = int.Parse(this.txtSN.Text);
					int num = 0;
					int num2 = 1;
					if (sender == this.button72)
					{
						num = 20;
						if ((int)this.numericUpDown2.Value >= 21 && (int)this.numericUpDown2.Value <= 40)
						{
							num2 = (int)this.numericUpDown2.Value - num;
						}
					}
					else if ((int)this.numericUpDown1.Value >= 1 && (int)this.numericUpDown1.Value <= 20)
					{
						num2 = (int)this.numericUpDown1.Value - num;
					}
					int num3 = int.Parse(this.numericUpDown22.Value.ToString());
					while (num2 <= 20 && !this.bStopRemoteEvalator)
					{
						if (this.optNO.Checked)
						{
							icController.RemoteOpenFoorIP(num2 + num, operatorId, operatorCardNO);
						}
						else
						{
							icController.RemoteOpenFoorIP(num2 + 40 + num, operatorId, operatorCardNO);
						}
						this.lblFloor.Text = (num2 + num).ToString();
						Application.DoEvents();
						int num4 = 0;
						while (num4 < num3 && !this.bStopRemoteEvalator)
						{
							Application.DoEvents();
							Thread.Sleep(300);
							num4 += 300;
						}
						num2++;
					}
				}
				catch (Exception)
				{
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
				XMessageBox.Show(ex.ToString());
			}
			finally
			{
				icController.Dispose();
			}
			this.button70.Enabled = true;
			this.button72.Enabled = true;
			Cursor.Current = Cursors.Default;
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			this.bStopRemoteEvalator = true;
		}

		private void button57_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = -1;
				icController.GetControllerRunInformationIP();
				if (icController.runinfo.wgcticks <= 0u)
				{
					this.txtRunInfo.AppendText("???控制器未连接\r\n");
					SystemSounds.Hand.Play();
				}
				else
				{
					if (this.checkBox116.Checked)
					{
						MjRegisterCard mjRegisterCard = new MjRegisterCard();
						mjRegisterCard.IsActivated = true;
						mjRegisterCard.Password = uint.Parse(345678.ToString());
						mjRegisterCard.ymdStart = DateTime.Parse("2010-1-1");
						mjRegisterCard.ymdEnd = DateTime.Parse("2029-12-31");
						mjRegisterCard.ControlSegIndexSet(1, 1);
						mjRegisterCard.ControlSegIndexSet(2, 1);
						mjRegisterCard.ControlSegIndexSet(3, 1);
						mjRegisterCard.ControlSegIndexSet(4, 1);
						icPrivilege icPrivilege = new icPrivilege();
						try
						{
							string text = this.textBox32.Text;
							if (!string.IsNullOrEmpty(text))
							{
								string[] array = text.Split(new char[]
								{
									','
								});
								if (array.Length > 0)
								{
									for (int i = 0; i < array.Length; i++)
									{
										uint num;
										if (uint.TryParse(array[i].Trim(), NumberStyles.Integer, null, out num) && num > 0u)
										{
											mjRegisterCard.CardID = num;
											icPrivilege.AddPrivilegeOfOneCardIP(-1, "", 60000, mjRegisterCard);
										}
									}
								}
							}
						}
						catch (Exception)
						{
						}
						icPrivilege.Dispose();
					}
					if (this.checkBox117.Checked)
					{
						wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
						wgMjControllerConfigure.RestoreDefault();
						wgMjControllerConfigure.Ext_doorSet(0, 0);
						wgMjControllerConfigure.Ext_doorSet(1, 1);
						wgMjControllerConfigure.Ext_doorSet(2, 2);
						wgMjControllerConfigure.Ext_doorSet(3, 3);
						wgMjControllerConfigure.Ext_controlSet(0, 4);
						wgMjControllerConfigure.Ext_controlSet(1, 4);
						wgMjControllerConfigure.Ext_controlSet(2, 4);
						wgMjControllerConfigure.Ext_controlSet(3, 4);
						wgMjControllerConfigure.Ext_warnSignalEnabled2Set(0, 2);
						wgMjControllerConfigure.Ext_warnSignalEnabled2Set(1, 2);
						wgMjControllerConfigure.Ext_warnSignalEnabled2Set(2, 2);
						wgMjControllerConfigure.Ext_warnSignalEnabled2Set(3, 2);
						icController.UpdateConfigureIP(wgMjControllerConfigure);
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				icController.Dispose();
			}
		}

		private void btnPing_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			using (icController icController = new icController())
			{
				try
				{
					icController.ControllerSN = -1;
					icController.runinfo.Clear();
					icController.GetControllerRunInformationIPNoTries();
					if (icController.runinfo.wgcticks > 0u)
					{
						icController.ControllerSN = (int)icController.runinfo.CurrentControllerSN;
						int num = 0;
						int num2 = 0;
						int num3 = 0;
						for (int i = 0; i < 200; i++)
						{
							num++;
							if (icController.SpecialPingIP() == 1)
							{
								num2++;
							}
							else
							{
								num3++;
							}
						}
						if (num != num2)
						{
							string text = string.Format("SN{3} 有故障: 通信丢包\r\n 已发送={0}, 已接收={1}, 丢失 = {2}", new object[]
							{
								num,
								num2,
								num3,
								icController.ControllerSN
							}) + "\r\n";
							base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
							{
								text
							});
						}
						else
						{
							wgUdpComm.triesTotal = 0L;
							wgTools.WriteLine("control.Test1024 Start");
							int num4 = 0;
							string text2 = "";
							int num5 = icController.test1024Write();
							if (num5 < 0)
							{
								text2 += "大数据包写入失败\r\n";
							}
							num5 = icController.test1024Read(100u, ref num4);
							if (num5 < 0)
							{
								text2 = text2 + "大数据包读取失败: " + num5.ToString() + "\r\n";
							}
							if (wgUdpComm.triesTotal > 0L)
							{
								text2 = text2 + "测试中重试次数 = " + wgUdpComm.triesTotal.ToString() + "\r\n";
							}
							if (text2 != "")
							{
								string text3 = string.Concat(new string[]
								{
									"SN",
									icController.ControllerSN.ToString(),
									"通信有故障: : ",
									text2,
									"\r\n"
								});
								base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
								{
									text3
								});
							}
							else
							{
								string text4 = string.Format("SN{3} 通信正常\r\n 已发送={0}, 已接收={1}, 丢失 = {2}", new object[]
								{
									num,
									num2,
									num3,
									icController.ControllerSN
								}) + "\r\n";
								base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
								{
									text4
								});
								text4 = "";
								base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
								{
									text4
								});
								text4 = "";
								base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
								{
									text4
								});
								text2 = "大数据包测试成功(测试100次)";
								text4 = icController.ControllerSN.ToString() + ": " + text2 + "\r\n";
								base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
								{
									text4
								});
								this.lastErrController = 0L;
							}
						}
					}
					else
					{
						string text5 = "通信不上";
						base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
						{
							text5
						});
						text5 = "";
						base.Invoke(new frmProductFormat.pingErrLog(this.pingErrLogEntry), new object[]
						{
							text5
						});
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
			Cursor.Current = Cursors.Default;
		}
	}
}
