using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;

namespace WG3000_COMM
{
	public class frmTestController : Form
	{
		private int defaultSN = 401000002;

		private string defaultIP = "";

		private int defaultPORT = 60000;

		private wgMjControllerTaskList controlTaskList;

		private icControllerTimeSegList controlTimeSegList;

		private string newSoundFile = "invalidCard.WAV";

		private SoundPlayer player;

		private dfrmNetControllerConfig dfrmNetControllerConfig1;

		private icController control4Tcp;

		private frmTestController frmTestController1;

		private icController control4Udp;

		private string lastRecordStr = "";

		private frmProductFormat frmProductFormat1;

		private dfrmFind dfrmFind1;

		private bool bFoundController;

		private wgMjControllerHolidaysList controlHolidayList;

		private DataTable dtPrivilege;

		private DataTable dtPrivilege1;

		private byte[] webStringOther = new byte[4096];

		private DataSet DS;

		private OleDbDataAdapter MyCommand;

		private OleDbConnection MyConnection;

		private DataView dv;

		private FileStream fs;

		private FileStream fsRd;

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage2;

		private NumericUpDown txtSN;

		private Label label1;

		private TextBox txtInfo;

		private TableLayoutPanel tableLayoutPanel2;

		private Label label12;

		private TextBox txtCardNO;

		private CheckBox checkBox24;

		private CheckBox checkBox25;

		private Label label13;

		private CheckBox checkBox26;

		private CheckBox checkBox27;

		private CheckBox checkBox29;

		private CheckBox checkBox28;

		private Label label18;

		private Label label14;

		private Label label15;

		private Label label16;

		private Label label17;

		private ComboBox comboBox4;

		private ComboBox comboBox3;

		private ComboBox comboBox2;

		private Label label19;

		private ComboBox comboBox1;

		private ComboBox comboBox5;

		private Label label20;

		private ComboBox comboBox8;

		private ComboBox comboBox7;

		private ComboBox comboBox6;

		private TextBox txtPassword;

		private Label label21;

		private Label label23;

		private Label label22;

		private DateTimePicker dtpDeactivate;

		private DateTimePicker dtpActivate;

		private Button button4;

		private TabPage tabPage1;

		private TableLayoutPanel tableLayoutPanel3;

		private Label label27;

		private Label label28;

		private Label label29;

		private Label label30;

		private Label label31;

		private CheckBox checkBox31;

		private CheckBox checkBox32;

		private CheckBox checkBox33;

		private CheckBox checkBox34;

		private ComboBox comboBox10;

		private ComboBox comboBox9;

		private ComboBox comboBox11;

		private ComboBox comboBox12;

		private Label label25;

		private CheckBox checkBox30;

		private CheckBox checkBox23;

		private CheckBox checkBox22;

		private CheckBox checkBox21;

		private GroupBox groupBox1;

		private RadioButton radioButton5;

		private RadioButton radioButton4;

		private RadioButton radioButton3;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label7;

		private CheckBox checkBox19;

		private CheckBox checkBox20;

		private CheckBox checkBox18;

		private CheckBox checkBox16;

		private CheckBox checkBox17;

		private CheckBox checkBox12;

		private Label label6;

		private CheckBox checkBox15;

		private Label label5;

		private Label label4;

		private CheckBox checkBox11;

		private CheckBox checkBox14;

		private CheckBox checkBox5;

		private CheckBox checkBox9;

		private CheckBox checkBox7;

		private CheckBox checkBox13;

		private CheckBox checkBox10;

		private CheckBox checkBox6;

		private CheckBox checkBox8;

		private Label label8;

		private Label label9;

		private Label label10;

		private Label label11;

		private Label label3;

		private Label label2;

		private CheckBox checkBox4;

		private CheckBox checkBox3;

		private CheckBox checkBox2;

		private CheckBox checkBox1;

		private Button button3;

		private Button button2;

		private Button button1;

		private TabPage tabPage3;

		private ComboBox comboBox45;

		private Label label26;

		private ComboBox comboBox13;

		private ComboBox comboBox16;

		private ComboBox comboBox15;

		private ComboBox comboBox14;

		private Label label34;

		private CheckBox checkBox36;

		private ComboBox comboBox25;

		private ComboBox comboBox32;

		private CheckBox checkBox35;

		private ComboBox comboBox26;

		private ComboBox comboBox31;

		private Label label39;

		private ComboBox comboBox27;

		private ComboBox comboBox30;

		private Label label38;

		private ComboBox comboBox28;

		private ComboBox comboBox29;

		private ComboBox comboBox41;

		private Label label33;

		private Label label35;

		private ComboBox comboBox42;

		private ComboBox comboBox21;

		private ComboBox comboBox36;

		private ComboBox comboBox43;

		private ComboBox comboBox22;

		private ComboBox comboBox44;

		private ComboBox comboBox35;

		private ComboBox comboBox23;

		private Label label37;

		private ComboBox comboBox34;

		private ComboBox comboBox24;

		private ComboBox comboBox37;

		private ComboBox comboBox33;

		private ComboBox comboBox20;

		private Label label32;

		private ComboBox comboBox38;

		private Label label36;

		private ComboBox comboBox19;

		private ComboBox comboBox17;

		private ComboBox comboBox39;

		private ComboBox comboBox40;

		private ComboBox comboBox18;

		private ComboBox comboBox48;

		private ComboBox comboBox47;

		private ComboBox comboBox46;

		private CheckBox checkBox39;

		private CheckBox checkBox40;

		private CheckBox checkBox41;

		private CheckBox checkBox42;

		private CheckBox checkBox38;

		private CheckBox checkBox37;

		private Label label41;

		private Label label40;

		private Label label24;

		private TabPage tabPage4;

		private DataGridView dgvControlConfure;

		private Label label42;

		private DataGridViewTextBoxColumn f_Loc;

		private DataGridViewTextBoxColumn f_Value;

		private DataGridViewTextBoxColumn f_DefaultValue;

		private DataGridViewTextBoxColumn f_Desc;

		private DataGridViewTextBoxColumn f_Type;

		private TabPage tabPage5;

		private GroupBox groupBox2;

		private CheckBox checkBox49;

		private CheckBox checkBox48;

		private CheckBox checkBox47;

		private CheckBox checkBox46;

		private CheckBox checkBox45;

		private CheckBox checkBox44;

		private CheckBox checkBox43;

		private Label label45;

		private DateTimePicker dateTimePicker3;

		private Label label43;

		private Label label44;

		private DateTimePicker dateTimePicker2;

		private DateTimePicker dateTimePicker1;

		private Button button7;

		private Button button6;

		private Button button5;

		private Label label47;

		private NumericUpDown numericUpDown2;

		private Label label46;

		private NumericUpDown numericUpDown1;

		private Button button8;

		private Button button11;

		private Button button10;

		private Button button9;

		private ListBox listBox1;

		private GroupBox groupBox3;

		private RadioButton radioButton6;

		private RadioButton radioButton7;

		private CheckBox checkBox50;

		private Button button12;

		private TabPage tabPage6;

		private DateTimePicker dateTimePicker5;

		private DateTimePicker dateTimePicker4;

		private Button button14;

		private Button button13;

		private Label label49;

		private Label label48;

		private Button button15;

		private ComboBox comboBox49;

		private Label label51;

		private Label label52;

		private Label label53;

		private Label label54;

		private Label label50;

		private CheckBox checkBox51;

		private ComboBox comboBox52;

		private ComboBox comboBox51;

		private ComboBox comboBox50;

		private CheckBox checkBox56;

		private CheckBox checkBox55;

		private CheckBox checkBox54;

		private CheckBox checkBox53;

		private CheckBox checkBox52;

		private GroupBox groupBox4;

		private RadioButton radioButton8;

		private RadioButton radioButton9;

		private CheckBox checkBox57;

		private TabPage tabPage7;

		private CheckBox checkBox58;

		private ComboBox comboBox56;

		private ComboBox comboBox55;

		private ComboBox comboBox54;

		private ComboBox comboBox53;

		private Label label55;

		private Label label56;

		private Label label57;

		private Label label58;

		private TextBox textBox13;

		private TextBox textBox14;

		private TextBox textBox15;

		private TextBox textBox16;

		private TextBox textBox9;

		private TextBox textBox10;

		private TextBox textBox11;

		private TextBox textBox12;

		private TextBox textBox5;

		private TextBox textBox6;

		private TextBox textBox7;

		private TextBox textBox8;

		private Label label66;

		private TextBox textBox4;

		private Label label65;

		private TextBox textBox3;

		private Label label64;

		private TextBox textBox2;

		private Label label63;

		private TextBox textBox1;

		private Label label59;

		private Label label60;

		private Label label61;

		private Label label62;

		private CheckBox checkBox59;

		private CheckBox checkBox60;

		private CheckBox checkBox68;

		private CheckBox checkBox67;

		private CheckBox checkBox66;

		private CheckBox checkBox65;

		private CheckBox checkBox64;

		private CheckBox checkBox63;

		private CheckBox checkBox62;

		private CheckBox checkBox61;

		private CheckBox checkBox70;

		private CheckBox checkBox69;

		private Label label67;

		private TextBox textBox17;

		private TabPage tabPage8;

		private GroupBox groupBox7;

		private CheckBox checkBox83;

		private CheckBox checkBox82;

		private CheckBox checkBox81;

		private CheckBox checkBox80;

		private CheckBox checkBox79;

		private CheckBox checkBox78;

		private CheckBox checkBox77;

		private CheckBox checkBox76;

		private RadioButton radioButton17;

		private RadioButton radioButton16;

		private GroupBox groupBox6;

		private CheckBox checkBox90;

		private CheckBox checkBox89;

		private CheckBox checkBox88;

		private CheckBox checkBox87;

		private CheckBox checkBox86;

		private CheckBox checkBox85;

		private CheckBox checkBox84;

		private RadioButton radioButton15;

		private RadioButton radioButton14;

		private GroupBox groupBox5;

		private RadioButton radioButton13;

		private RadioButton radioButton12;

		private RadioButton radioButton11;

		private RadioButton radioButton10;

		private CheckBox checkBox72;

		private CheckBox checkBox73;

		private CheckBox checkBox74;

		private CheckBox checkBox75;

		private CheckBox checkBox71;

		private Label label69;

		private Label label68;

		private RadioButton radioButton18;

		private GroupBox groupBox8;

		private RadioButton radioButton19;

		private RadioButton radioButton20;

		private RadioButton radioButton21;

		private RadioButton radioButton22;

		private RadioButton radioButton23;

		private Label label71;

		private Label label70;

		private NumericUpDown numericUpDown3;

		private CheckBox checkBox96;

		private CheckBox checkBox95;

		private CheckBox checkBox94;

		private CheckBox checkBox91;

		private CheckBox checkBox92;

		private CheckBox checkBox93;

		private Label label75;

		private Label label76;

		private NumericUpDown numericUpDown5;

		private Label label73;

		private Label label74;

		private Label label72;

		private NumericUpDown numericUpDown4;

		private GroupBox groupBox9;

		private RadioButton radioButton25;

		private TabPage tabPage9;

		private Button button16;

		private TextBox textBox20;

		private Label label79;

		private Label label78;

		private TextBox textBox19;

		private Label label77;

		private TextBox textBox18;

		private CheckBox checkBox97;

		private Button button17;

		private NumericUpDown numericUpDown6;

		private Label label80;

		private TabPage tabPage10;

		private CheckBox checkBox104;

		private CheckBox checkBox103;

		private CheckBox checkBox102;

		private CheckBox checkBox101;

		private CheckBox checkBox100;

		private CheckBox checkBox99;

		private CheckBox checkBox98;

		private Label label84;

		private ComboBox comboBox58;

		private Label label83;

		private ComboBox comboBox57;

		private DateTimePicker dateTimePicker6;

		private DateTimePicker dateTimePicker7;

		private Label label81;

		private Label label82;

		private GroupBox groupBox11;

		private Label label89;

		private Label label90;

		private DateTimePicker dateTimePicker12;

		private DateTimePicker dateTimePicker13;

		private Label label87;

		private Label label88;

		private DateTimePicker dateTimePicker10;

		private DateTimePicker dateTimePicker11;

		private Label label86;

		private Label label85;

		private DateTimePicker dateTimePicker9;

		private DateTimePicker dateTimePicker8;

		private GroupBox groupBox10;

		private NumericUpDown numericUpDown9;

		private Label label93;

		private NumericUpDown numericUpDown8;

		private Label label92;

		private NumericUpDown numericUpDown7;

		private Label label91;

		private NumericUpDown numericUpDown10;

		private Label label94;

		private Button button18;

		private CheckBox checkBox105;

		private Button button19;

		private Button button20;

		private ListBox listBox2;

		private NumericUpDown numericUpDown12;

		private Label label96;

		private NumericUpDown numericUpDown11;

		private Label label95;

		private CheckBox checkBox109;

		private CheckBox checkBox108;

		private CheckBox checkBox107;

		private CheckBox checkBox106;

		private CheckBox checkBox110;

		private Button button21;

		private TabPage tabPage11;

		private Button button22;

		private Button button23;

		private Label label99;

		private NumericUpDown numericUpDown14;

		private Label label98;

		private Label label97;

		private NumericUpDown numericUpDown13;

		private CheckBox checkBox111;

		private Button button24;

		private TabPage tabPage12;

		private Button button25;

		private Label label100;

		private Label label101;

		private NumericUpDown nudEndPage;

		private Label label102;

		private Label label103;

		private NumericUpDown nudStartPage;

		private Label label104;

		private NumericUpDown nudDatalen;

		private NumericUpDown nudValue;

		private Label label105;

		private Label label106;

		private Label label107;

		private Button button26;

		private TabPage tabPage13;

		private Button button27;

		private Label label108;

		private NumericUpDown nudNewSN;

		private Button button28;

		private TextBox txt02e2;

		private Label label109;

		private Button button29;

		private Button button30;

		private Button button31;

		private Button button32;

		private TabPage tabPage14;

		private Button button33;

		private Button button34;

		private Button button35;

		private TextBox txtCommPassword;

		private Label label110;

		private Button button36;

		private TextBox txtOldCommPassword;

		private Label label111;

		private TabPage tabPage15;

		private Button button37;

		private Label label112;

		private TextBox textBox21;

		private Button button38;

		private Label label113;

		private System.Windows.Forms.Timer timer1;

		private Button button39;

		private Button button40;

		private Button button41;

		private Button button42;

		private TabPage tabPage16;

		private TextBox textBox23;

		private TextBox textBox22;

		private CheckBox checkBox112;

		private Button button43;

		private Label label114;

		private Button button44;

		private Button button45;

		private Button button46;

		private TabPage tabPage17;

		private Button button47;

		private Button button48;

		private Button button49;

		private TabPage tabPage18;

		private Label label116;

		private Label label115;

		private TextBox textBox25;

		private TextBox textBox24;

		private Button button50;

		private TextBox textBox26;

		private Label label117;

		private CheckBox checkBox114;

		private CheckBox checkBox113;

		private Button button51;

		private CheckBox checkBox115;

		private CheckBox checkBox117;

		private CheckBox checkBox116;

		private Button button52;

		private Label label118;

		private TextBox textBox29;

		private Label label119;

		private Label label120;

		private TextBox textBox30;

		private Button button53;

		private NumericUpDown numericUpDown15;

		private Label label121;

		private Button button54;

		private Button button55;

		private Button button56;

		private CheckBox checkBox118;

		private Button button57;

		private CheckBox checkBox119;

		private NumericUpDown numericUpDown16;

		private Label label122;

		private CheckBox checkBox120;

		private GroupBox grpbIP;

		private NumericUpDown nudPort;

		public TextBox txtControllerIP;

		private Label label123;

		private Label label124;

		private Button button60;

		private Button button59;

		private NumericUpDown numericUpDown17;

		private Label label125;

		private RichTextBox richTextBox2;

		private RichTextBox richTextBox1;

		private CheckBox checkBox121;

		private NumericUpDown numericUpDown18;

		private Label label126;

		private CheckBox checkBox122;

		private CheckBox checkBox123;

		private CheckBox checkBox124;

		private CheckBox checkBox125;

		private CheckBox checkBox126;

		private NumericUpDown numericUpDown19;

		private Label label127;

		private GroupBox groupBox12;

		private RadioButton radioButton26;

		private RadioButton radioButton24;

		private DateTimePicker dateTimePicker14;

		private Label lblWrongProductCode;

		private Button button58;

		private CheckBox checkBox127;

		private Button button62;

		private DateTimePicker dateTimePicker18;

		private Button button61;

		private ListBox listBox3;

		private DateTimePicker dateTimePicker15;

		private Label label129;

		private Label label130;

		private DateTimePicker dateTimePicker16;

		private DateTimePicker dateTimePicker17;

		private Button button63;

		private CheckBox checkBox128;

		private Label label131;

		private TabPage tabPage19;

		private Button button64;

		private Button button65;

		private Button button66;

		private Button button67;

		private ListBox listBox4;

		private GroupBox grpWeekdayControl;

		private CheckBox chkMonday;

		private CheckBox chkSunday;

		private CheckBox chkTuesday;

		private CheckBox chkSaturday;

		private CheckBox chkWednesday;

		private CheckBox chkFriday;

		private CheckBox chkThursday;

		private GroupBox grpEnd;

		private ComboBox cboEndControlStatus;

		private Label label128;

		private Label label132;

		private DateTimePicker dateEndHMS1;

		internal Label label133;

		private GroupBox grpBegin;

		private ComboBox cboBeginControlStatus;

		private Label label134;

		private Label label135;

		private DateTimePicker dateBeginHMS1;

		internal Label label136;

		private Label label137;

		private ComboBox cboDoors;

		private Label label138;

		private Button button68;

		private DataGridView dataGridView1;

		private CheckBox checkBox171;

		private CheckBox checkBox172;

		private CheckBox checkBox173;

		private CheckBox checkBox174;

		private CheckBox checkBox175;

		private CheckBox checkBox176;

		private CheckBox checkBox177;

		private CheckBox checkBox178;

		private CheckBox checkBox179;

		private CheckBox checkBox180;

		private CheckBox checkBox161;

		private CheckBox checkBox162;

		private CheckBox checkBox163;

		private CheckBox checkBox164;

		private CheckBox checkBox165;

		private CheckBox checkBox166;

		private CheckBox checkBox167;

		private CheckBox checkBox168;

		private CheckBox checkBox169;

		private CheckBox checkBox170;

		private CheckBox checkBox151;

		private CheckBox checkBox152;

		private CheckBox checkBox153;

		private CheckBox checkBox154;

		private CheckBox checkBox155;

		private CheckBox checkBox156;

		private CheckBox checkBox157;

		private CheckBox checkBox158;

		private CheckBox checkBox159;

		private CheckBox checkBox160;

		private CheckBox checkBox150;

		private CheckBox checkBox149;

		private CheckBox checkBox148;

		private CheckBox checkBox147;

		private CheckBox checkBox146;

		private CheckBox checkBox145;

		private CheckBox checkBox144;

		private CheckBox checkBox143;

		private CheckBox checkBox142;

		private CheckBox checkBox141;

		private CheckBox checkBox129;

		private TextBox textBox31;

		private Label label139;

		private Button button69;

		private Button button70;

		private CheckBox checkBox131;

		private CheckBox checkBox130;

		private Button button71;

		private Label label140;

		private TabPage tabPage20;

		private CheckBox checkBox132;

		private NumericUpDown numericUpDown20;

		private Label label141;

		private NumericUpDown numericUpDown21;

		private Label label142;

		private Button button72;

		private Label label143;

		private NumericUpDown numericUpDown22;

		private GroupBox groupBox13;

		private Button button73;

		private NumericUpDown numericUpDown23;

		private CheckBox checkBox133;

		private Button button74;

		private Button button75;

		private TabPage tabPage21;

		private DataGridView dataGridView2;

		private Button button76;

		private OpenFileDialog openFileDialog1;

		private Label label144;

		private ComboBox comboBox59;

		private TabPage tabPage22;

		private Button button77;

		private NumericUpDown numericUpDown24;

		private Label label145;

		private TextBox textBox32;

		private Label label146;

		private Button button78;

		private DataGridView dataGridView3;

		private Button button79;

		private Panel panel1;

		private Label label147;

		private Button button81;

		private Label label148;

		private Label label149;

		private Label label150;

		private TextBox textBox27;

		private TextBox textBox28;

		private TextBox textBox33;

		private CheckBox checkBox134;

		private TabPage tabPage23;

		private Label label151;

		private TextBox textBox36;

		private TextBox textBox35;

		private Button button82;

		private TextBox textBox34;

		private Button button80;

		private FolderBrowserDialog folderBrowserDialog1;

		private Button button83;

		private Button button84;

		private CheckBox checkBox135;

		private Button button85;

		private Button button86;

		private Button button87;

		private Button button88;

		private Label label154;

		private Label label155;

		private Label label152;

		private NumericUpDown numericUpDown25;

		private Label label153;

		private NumericUpDown numericUpDown26;

		private Button button89;

		private NumericUpDown numericUpDown27;

		private Label label156;

		private Button button90;

		private Label label157;

		private Label label158;

		private TextBox textBox37;

		private TextBox textBox38;

		private GroupBox groupBox14;

		private RadioButton radioButton27;

		private RadioButton radioButton28;

		private TabPage tabPage24;

		private GroupBox groupBox16;

		private RadioButton radioButton32;

		private RadioButton radioButton33;

		private GroupBox groupBox15;

		private Label label161;

		private RadioButton radioButton31;

		private RadioButton radioButton30;

		private RadioButton radioButton29;

		private NumericUpDown numericUpDown28;

		private CheckBox checkBox136;

		private NumericUpDown numericUpDown29;

		private GroupBox groupBox17;

		private NumericUpDown numericUpDown30;

		private RadioButton radioButton34;

		private RadioButton radioButton35;

		private GroupBox groupBox18;

		private Label label159;

		private RadioButton radioButton36;

		private RadioButton radioButton37;

		private NumericUpDown numericUpDown31;

		private CheckBox checkBox137;

		private GroupBox groupBox19;

		private NumericUpDown numericUpDown32;

		private RadioButton radioButton38;

		private RadioButton radioButton39;

		private CheckBox checkBox138;

		private Button button91;

		private ComboBox comboBox60;

		private TabPage tabPage25;

		private Button button92;

		private Button button93;

		private Button button94;

		private Button button95;

		public frmTestController()
		{
			this.InitializeComponent();
		}

		private void getControlSetByUser(ref icController icCon)
		{
			icCon.ControllerSN = (int)this.txtSN.Value;
			if (this.checkBox120.Checked)
			{
				icCon.IP = this.txtControllerIP.Text;
				icCon.PORT = (int)this.nudPort.Value;
				return;
			}
			icCon.IP = "";
			icCon.PORT = 60000;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
			icController icController = null;
			try
			{
				icController = new icController();
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				wgMjControllerConfigure.RestoreDefault();
				icController.UpdateConfigureIP(wgMjControllerConfigure);
			}
			catch
			{
			}
			finally
			{
				if (icController != null)
				{
					icController.Dispose();
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			WGPacket wGPacket = new WGPacket();
			wGPacket.type = 36;
			wGPacket.code = 16;
			wGPacket.iDevSnFrom = 0u;
			if (this.txtSN.Value == this.txtSN.Maximum)
			{
				wGPacket.iDevSnTo = 4294967295u;
			}
			else
			{
				wGPacket.iDevSnTo = (uint)this.txtSN.Value;
			}
			wGPacket.iCallReturn = 0;
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				byte[] cmd = wGPacket.ToBytes(wgUdpComm.udpPort);
				byte[] array = null;
				wgUdpComm.udp_get(cmd, 300, wGPacket.xid, this.defaultIP, this.defaultPORT, ref array);
				this.txtInfo.AppendText(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")));
				this.txtInfo.AppendText("\r\n");
				this.Refresh();
				if (array != null)
				{
					string text = BitConverter.ToString(array);
					this.txtInfo.AppendText(text);
					this.txtInfo.AppendText("\r\n");
					this.dgvControlConfure.Rows.Clear();
					wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
					int num = 20;
					while (num < array.Length && num - 20 < wgMjControllerConfigure.param.Length)
					{
						string[] values = new string[]
						{
							(array[num] == wgMjControllerConfigure.param[num - 20]) ? " " : "X",
							string.Format("{0:d3}[0x{1:X2}]", num - 20, num - 20 + 16),
							string.Format("0x{0:X2}", array[num]),
							(num - 20 < wgMjControllerConfigure.param.Length) ? string.Format("0x{0:X2}", wgMjControllerConfigure.param[num - 20]) : "",
							(num - 20 < wgMjControllerConfigure.paramDesc.Length) ? wgMjControllerConfigure.paramDesc[num - 20] : ""
						};
						this.dgvControlConfure.Rows.Add(values);
						num++;
					}
					for (int i = 496; i < 510; i++)
					{
						string[] values = new string[]
						{
							(array[i + 20] == 0) ? " " : "X",
							string.Format("{0:d3}[0x{1:X2}]", i, i + 16),
							string.Format("0x{0:X2}", array[i + 20]),
							string.Format("0x{0:X2}", 0),
							"--"
						};
						this.dgvControlConfure.Rows.Add(values);
					}
					for (int j = 510; j < 512; j++)
					{
						string[] values = new string[]
						{
							(array[j + 20] == 0) ? " " : "X",
							string.Format("{0:d3}[0x{1:X2}]", j, j + 16),
							string.Format("0x{0:X2}", array[j + 20]),
							string.Format("0x{0:X2}", 0),
							"CRC_CHECK"
						};
						this.dgvControlConfure.Rows.Add(values);
					}
					for (int k = 512; k < 528; k++)
					{
						string[] values = new string[]
						{
							(array[k + 20] == 255) ? " " : "X",
							string.Format("{0:d3}[0x{1:X2}]", k, k + 16),
							string.Format("0x{0:X2}", array[k + 20]),
							string.Format("0x{0:X2}", 255),
							"特殊卡_" + ((k >= 520) ? "2" : "1")
						};
						this.dgvControlConfure.Rows.Add(values);
					}
					string[] array2 = new string[]
					{
						"DHCP_IP1",
						"DHCP_IP2",
						"DHCP_IP3",
						"DHCP_IP4",
						"DHCP_Mask1",
						"DHCP_Mask2",
						"DHCP_Mask3",
						"DHCP_Mask4",
						"DHCP_Gateway1",
						"DHCP_Gateway2",
						"DHCP_Gateway3",
						"DHCP_Gateway4"
					};
					int[] array3 = new int[]
					{
						192,
						168,
						0,
						0,
						255,
						255,
						255,
						0,
						192,
						168,
						0,
						0
					};
					for (int l = 384; l < 396; l++)
					{
						string[] values = new string[]
						{
							((int)array[l + 20] == array3[l - 384]) ? " " : "X",
							string.Format("{0:d3}", l),
							string.Format("0x{0:X2}", array[l + 20]),
							string.Format("0x{0:X2}", array3[l - 384]),
							array2[l - 384]
						};
						this.dgvControlConfure.Rows.Add(values);
					}
					this.tabControl1.SelectedTab = this.tabPage4;
				}
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
			if (this.checkBox58.Checked)
			{
				wgMjControllerConfigure.DoorControlSet(1, this.comboBox53.SelectedIndex);
				wgMjControllerConfigure.DoorControlSet(2, this.comboBox54.SelectedIndex);
				wgMjControllerConfigure.DoorControlSet(3, this.comboBox55.SelectedIndex);
				wgMjControllerConfigure.DoorControlSet(4, this.comboBox56.SelectedIndex);
			}
			if (this.checkBox23.Checked)
			{
				wgMjControllerConfigure.ReaderPasswordSet(1, this.checkBox1.Checked ? 1 : 0);
				wgMjControllerConfigure.ReaderPasswordSet(2, this.checkBox2.Checked ? 1 : 0);
				wgMjControllerConfigure.ReaderPasswordSet(3, this.checkBox3.Checked ? 1 : 0);
				wgMjControllerConfigure.ReaderPasswordSet(4, this.checkBox4.Checked ? 1 : 0);
			}
			if (this.checkBox21.Checked)
			{
				int num = 0;
				wgMjControllerConfigure.DoorInterlockSet(1, num);
				wgMjControllerConfigure.DoorInterlockSet(2, num);
				wgMjControllerConfigure.DoorInterlockSet(3, num);
				wgMjControllerConfigure.DoorInterlockSet(4, num);
				if (this.checkBox5.Checked || this.checkBox6.Checked || this.checkBox7.Checked || this.checkBox8.Checked)
				{
					num = 1;
					if (this.checkBox5.Checked)
					{
						num += 16;
					}
					if (this.checkBox6.Checked)
					{
						num += 32;
					}
					if (this.checkBox7.Checked)
					{
						num += 64;
					}
					if (this.checkBox8.Checked)
					{
						num += 128;
					}
					wgMjControllerConfigure.DoorInterlockSet(1, num);
				}
				if (this.checkBox9.Checked || this.checkBox10.Checked || this.checkBox11.Checked || this.checkBox12.Checked)
				{
					num = 2;
					if (this.checkBox9.Checked)
					{
						num += 16;
					}
					if (this.checkBox10.Checked)
					{
						num += 32;
					}
					if (this.checkBox11.Checked)
					{
						num += 64;
					}
					if (this.checkBox12.Checked)
					{
						num += 128;
					}
					wgMjControllerConfigure.DoorInterlockSet(2, num);
				}
				if (this.checkBox13.Checked || this.checkBox14.Checked || this.checkBox15.Checked || this.checkBox16.Checked)
				{
					num = 4;
					if (this.checkBox13.Checked)
					{
						num += 16;
					}
					if (this.checkBox14.Checked)
					{
						num += 32;
					}
					if (this.checkBox15.Checked)
					{
						num += 64;
					}
					if (this.checkBox16.Checked)
					{
						num += 128;
					}
					wgMjControllerConfigure.DoorInterlockSet(3, num);
				}
				if (this.checkBox17.Checked || this.checkBox18.Checked || this.checkBox19.Checked || this.checkBox20.Checked)
				{
					num = 8;
					if (this.checkBox17.Checked)
					{
						num += 16;
					}
					if (this.checkBox18.Checked)
					{
						num += 32;
					}
					if (this.checkBox19.Checked)
					{
						num += 64;
					}
					if (this.checkBox20.Checked)
					{
						num += 128;
					}
					wgMjControllerConfigure.DoorInterlockSet(4, num);
				}
			}
			if (this.checkBox22.Checked)
			{
				if (this.radioButton1.Checked)
				{
					wgMjControllerConfigure.antiback = 0;
				}
				else if (this.radioButton2.Checked)
				{
					wgMjControllerConfigure.antiback = 1;
				}
				else if (this.radioButton3.Checked)
				{
					wgMjControllerConfigure.antiback = 2;
				}
				else if (this.radioButton4.Checked)
				{
					wgMjControllerConfigure.antiback = 3;
				}
				else if (this.radioButton5.Checked)
				{
					wgMjControllerConfigure.antiback = 4;
				}
				wgMjControllerConfigure.autiback_allow_firstout_enable = (this.radioButton28.Checked ? 165 : 0);
			}
			if (this.checkBox110.Checked)
			{
				wgMjControllerConfigure.indoorPersonsMax = (int)this.numericUpDown11.Value;
				wgMjControllerConfigure.indoorPersonsMin = (int)this.numericUpDown12.Value;
			}
			if (this.checkBox30.Checked)
			{
				int num2 = 0;
				num2 += (this.checkBox31.Checked ? 1 : 0);
				num2 += (this.checkBox32.Checked ? 1 : 0) << 1;
				num2 += (this.checkBox33.Checked ? 1 : 0) << 2;
				num2 += (this.checkBox34.Checked ? 1 : 0) << 3;
				wgMjControllerConfigure.moreCardRead4Reader = num2;
				wgMjControllerConfigure.MorecardNeedCardsSet(1, int.Parse(this.comboBox9.Text));
				wgMjControllerConfigure.MorecardNeedCardsSet(2, int.Parse(this.comboBox10.Text));
				wgMjControllerConfigure.MorecardNeedCardsSet(3, int.Parse(this.comboBox11.Text));
				wgMjControllerConfigure.MorecardNeedCardsSet(4, int.Parse(this.comboBox12.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 1, int.Parse(this.comboBox13.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 1, int.Parse(this.comboBox14.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 1, int.Parse(this.comboBox15.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 1, int.Parse(this.comboBox16.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 2, int.Parse(this.comboBox17.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 2, int.Parse(this.comboBox18.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 2, int.Parse(this.comboBox19.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 2, int.Parse(this.comboBox20.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 3, int.Parse(this.comboBox21.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 3, int.Parse(this.comboBox22.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 3, int.Parse(this.comboBox23.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 3, int.Parse(this.comboBox24.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 4, int.Parse(this.comboBox25.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 4, int.Parse(this.comboBox26.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 4, int.Parse(this.comboBox27.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 4, int.Parse(this.comboBox28.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 5, int.Parse(this.comboBox29.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 5, int.Parse(this.comboBox30.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 5, int.Parse(this.comboBox31.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 5, int.Parse(this.comboBox32.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 6, int.Parse(this.comboBox33.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 6, int.Parse(this.comboBox34.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 6, int.Parse(this.comboBox35.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 6, int.Parse(this.comboBox36.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 7, int.Parse(this.comboBox37.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 7, int.Parse(this.comboBox38.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 7, int.Parse(this.comboBox39.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 7, int.Parse(this.comboBox40.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(1, 8, int.Parse(this.comboBox41.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(2, 8, int.Parse(this.comboBox42.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(3, 8, int.Parse(this.comboBox43.Text));
				wgMjControllerConfigure.MorecardGroupNeedCardsSet(4, 8, int.Parse(this.comboBox44.Text));
				wgMjControllerConfigure.MorecardSequenceInputSet(1, this.checkBox35.Checked);
				wgMjControllerConfigure.MorecardSequenceInputSet(2, this.checkBox36.Checked);
				wgMjControllerConfigure.MorecardSequenceInputSet(3, this.checkBox37.Checked);
				wgMjControllerConfigure.MorecardSequenceInputSet(4, this.checkBox38.Checked);
				wgMjControllerConfigure.MorecardSingleGroupEnableSet(1, this.checkBox39.Checked);
				wgMjControllerConfigure.MorecardSingleGroupEnableSet(2, this.checkBox40.Checked);
				wgMjControllerConfigure.MorecardSingleGroupEnableSet(3, this.checkBox41.Checked);
				wgMjControllerConfigure.MorecardSingleGroupEnableSet(4, this.checkBox42.Checked);
				wgMjControllerConfigure.MorecardSingleGroupStartNOSet(1, int.Parse(this.comboBox45.Text));
				wgMjControllerConfigure.MorecardSingleGroupStartNOSet(2, int.Parse(this.comboBox46.Text));
				wgMjControllerConfigure.MorecardSingleGroupStartNOSet(3, int.Parse(this.comboBox47.Text));
				wgMjControllerConfigure.MorecardSingleGroupStartNOSet(4, int.Parse(this.comboBox48.Text));
			}
			if (this.checkBox50.Checked)
			{
				wgMjControllerConfigure.controlTaskList_enabled = (this.radioButton7.Checked ? 1 : 0);
			}
			if (this.checkBox51.Checked)
			{
				wgMjControllerConfigure.FirstCardInfoSet(1, (this.comboBox49.SelectedIndex > 0) ? (this.comboBox49.SelectedIndex + 16) : 0);
				wgMjControllerConfigure.FirstCardInfoSet(2, (this.comboBox50.SelectedIndex > 0) ? (this.comboBox50.SelectedIndex + 16) : 0);
				wgMjControllerConfigure.FirstCardInfoSet(3, (this.comboBox51.SelectedIndex > 0) ? (this.comboBox51.SelectedIndex + 16) : 0);
				wgMjControllerConfigure.FirstCardInfoSet(4, (this.comboBox52.SelectedIndex > 0) ? (this.comboBox52.SelectedIndex + 16) : 0);
			}
			if (this.checkBox52.Checked)
			{
				int num3 = 0;
				num3 += (this.checkBox53.Checked ? 1 : 0);
				num3 += (this.checkBox54.Checked ? 2 : 0);
				num3 += (this.checkBox55.Checked ? 4 : 0);
				num3 += (this.checkBox56.Checked ? 8 : 0);
				wgMjControllerConfigure.lockSwitchOption = num3;
			}
			if (this.checkBox57.Checked)
			{
				wgMjControllerConfigure.receventPB = (this.radioButton9.Checked ? 1 : 0);
				wgMjControllerConfigure.receventDS = (this.radioButton9.Checked ? 1 : 0);
			}
			if (this.checkBox59.Checked)
			{
				wgMjControllerConfigure.SuperpasswordSet(1, string.IsNullOrEmpty(this.textBox1.Text) ? 65535 : int.Parse(this.textBox1.Text));
				wgMjControllerConfigure.SuperpasswordSet(2, string.IsNullOrEmpty(this.textBox2.Text) ? 65535 : int.Parse(this.textBox2.Text));
				wgMjControllerConfigure.SuperpasswordSet(3, string.IsNullOrEmpty(this.textBox3.Text) ? 65535 : int.Parse(this.textBox3.Text));
				wgMjControllerConfigure.SuperpasswordSet(4, string.IsNullOrEmpty(this.textBox4.Text) ? 65535 : int.Parse(this.textBox4.Text));
				wgMjControllerConfigure.SuperpasswordSet(5, string.IsNullOrEmpty(this.textBox5.Text) ? 65535 : int.Parse(this.textBox5.Text));
				wgMjControllerConfigure.SuperpasswordSet(6, string.IsNullOrEmpty(this.textBox6.Text) ? 65535 : int.Parse(this.textBox6.Text));
				wgMjControllerConfigure.SuperpasswordSet(7, string.IsNullOrEmpty(this.textBox7.Text) ? 65535 : int.Parse(this.textBox7.Text));
				wgMjControllerConfigure.SuperpasswordSet(8, string.IsNullOrEmpty(this.textBox8.Text) ? 65535 : int.Parse(this.textBox8.Text));
				wgMjControllerConfigure.SuperpasswordSet(9, string.IsNullOrEmpty(this.textBox9.Text) ? 65535 : int.Parse(this.textBox9.Text));
				wgMjControllerConfigure.SuperpasswordSet(10, string.IsNullOrEmpty(this.textBox10.Text) ? 65535 : int.Parse(this.textBox10.Text));
				wgMjControllerConfigure.SuperpasswordSet(11, string.IsNullOrEmpty(this.textBox11.Text) ? 65535 : int.Parse(this.textBox11.Text));
				wgMjControllerConfigure.SuperpasswordSet(12, string.IsNullOrEmpty(this.textBox12.Text) ? 65535 : int.Parse(this.textBox12.Text));
				wgMjControllerConfigure.SuperpasswordSet(13, string.IsNullOrEmpty(this.textBox13.Text) ? 65535 : int.Parse(this.textBox13.Text));
				wgMjControllerConfigure.SuperpasswordSet(14, string.IsNullOrEmpty(this.textBox14.Text) ? 65535 : int.Parse(this.textBox14.Text));
				wgMjControllerConfigure.SuperpasswordSet(15, string.IsNullOrEmpty(this.textBox15.Text) ? 65535 : int.Parse(this.textBox15.Text));
				wgMjControllerConfigure.SuperpasswordSet(16, string.IsNullOrEmpty(this.textBox16.Text) ? 65535 : int.Parse(this.textBox16.Text));
			}
			if (this.checkBox60.Checked)
			{
				int num4 = 0;
				num4 += (this.checkBox61.Checked ? 1 : 0);
				num4 += (this.checkBox62.Checked ? 2 : 0);
				num4 += (this.checkBox63.Checked ? 4 : 0);
				num4 += (this.checkBox64.Checked ? 8 : 0);
				num4 += (this.checkBox65.Checked ? 16 : 0);
				num4 += (this.checkBox66.Checked ? 32 : 0);
				num4 += (this.checkBox67.Checked ? 64 : 0);
				num4 += (this.checkBox68.Checked ? 128 : 0);
				wgMjControllerConfigure.warnSetup = num4;
				if (!string.IsNullOrEmpty(this.textBox17.Text))
				{
					wgMjControllerConfigure.xpPassword = int.Parse(this.textBox17.Text);
				}
			}
			if (this.checkBox69.Checked)
			{
				wgMjControllerConfigure.receventWarn = (this.checkBox70.Checked ? 1 : 0);
			}
			if (this.checkBox71.Checked)
			{
				int extPortNum = 0;
				if (this.radioButton19.Checked)
				{
					extPortNum = 0;
				}
				if (this.radioButton20.Checked)
				{
					extPortNum = 1;
				}
				if (this.radioButton21.Checked)
				{
					extPortNum = 2;
				}
				if (this.radioButton22.Checked)
				{
					extPortNum = 3;
				}
				int value = 0;
				if (this.radioButton10.Checked)
				{
					value = 0;
				}
				if (this.radioButton11.Checked)
				{
					value = 1;
				}
				if (this.radioButton12.Checked)
				{
					value = 2;
				}
				if (this.radioButton13.Checked)
				{
					value = 3;
				}
				if (this.radioButton25.Checked)
				{
					value = 16;
				}
				wgMjControllerConfigure.Ext_doorSet(extPortNum, value);
				int value2 = 0;
				if (this.radioButton23.Checked)
				{
					value2 = 0;
				}
				if (this.radioButton14.Checked)
				{
					value2 = 1;
				}
				if (this.radioButton15.Checked)
				{
					value2 = 2;
				}
				if (this.radioButton16.Checked)
				{
					value2 = 3;
				}
				if (this.radioButton17.Checked)
				{
					value2 = 4;
				}
				if (this.radioButton18.Checked)
				{
					value2 = 5;
				}
				wgMjControllerConfigure.Ext_controlSet(extPortNum, value2);
				if (!this.radioButton25.Checked)
				{
					int num5 = 0;
					if (this.checkBox84.Checked)
					{
						num5 |= 1;
					}
					if (this.checkBox85.Checked)
					{
						num5 |= 2;
					}
					if (this.checkBox86.Checked)
					{
						num5 |= 4;
					}
					if (this.checkBox87.Checked)
					{
						num5 |= 8;
					}
					if (this.checkBox88.Checked)
					{
						num5 |= 16;
					}
					if (this.checkBox89.Checked)
					{
						num5 |= 32;
					}
					if (this.checkBox90.Checked)
					{
						num5 |= 64;
					}
					wgMjControllerConfigure.Ext_warnSignalEnabledSet(extPortNum, num5);
					int num6 = 0;
					if (this.checkBox76.Checked)
					{
						num6 |= 1;
					}
					if (this.checkBox77.Checked)
					{
						num6 |= 2;
					}
					if (this.checkBox78.Checked)
					{
						num6 |= 4;
					}
					if (this.checkBox79.Checked)
					{
						num6 |= 8;
					}
					if (this.checkBox80.Checked)
					{
						num6 |= 16;
					}
					if (this.checkBox81.Checked)
					{
						num6 |= 32;
					}
					if (this.checkBox82.Checked)
					{
						num6 |= 64;
					}
					if (this.checkBox83.Checked)
					{
						num6 |= 128;
					}
					wgMjControllerConfigure.Ext_warnSignalEnabled2Set(extPortNum, num6);
				}
				else
				{
					wgMjControllerConfigure.ext_SetAlarmOnDelay = (int)this.numericUpDown4.Value;
					wgMjControllerConfigure.ext_SetAlarmOffDelay = (int)this.numericUpDown5.Value;
					int num7 = 0;
					num7 += (this.checkBox91.Checked ? 2 : 0);
					num7 += (this.checkBox92.Checked ? 4 : 0);
					num7 += (this.checkBox93.Checked ? 8 : 0);
					num7 += (this.checkBox94.Checked ? 16 : 0);
					num7 += (this.checkBox95.Checked ? 32 : 0);
					num7 += (this.checkBox96.Checked ? 64 : 0);
					wgMjControllerConfigure.ext_AlarmControlMode = num7;
				}
				wgMjControllerConfigure.Ext_timeoutSet(extPortNum, (int)this.numericUpDown3.Value);
			}
			if (this.checkBox106.Checked)
			{
				wgMjControllerConfigure.swipeOrderMode = 0;
				if (!this.checkBox107.Checked)
				{
					if (this.checkBox109.Checked)
					{
						wgMjControllerConfigure.swipeOrderMode |= 2;
					}
					else
					{
						wgMjControllerConfigure.swipeOrderMode |= (this.checkBox108.Checked ? 1 : 0);
					}
				}
			}
			if (this.checkBox112.Checked)
			{
				wgMjControllerConfigure.dataServerIP = IPAddress.Parse(this.textBox22.Text);
				wgMjControllerConfigure.dataServerPort = int.Parse(this.textBox23.Text);
			}
			if (this.checkBox119.Checked)
			{
				wgMjControllerConfigure.swipeGap = (int)this.numericUpDown16.Value;
			}
			if (this.checkBox121.Checked)
			{
				wgMjControllerConfigure.pcControlSwipeTimeout = (int)this.numericUpDown18.Value;
			}
			if (this.checkBox122.Checked)
			{
				wgMjControllerConfigure.dhcpEnable = (this.checkBox123.Checked ? 165 : 0);
			}
			if (this.checkBox124.Checked)
			{
				wgMjControllerConfigure.auto_negotiation_enable = (this.checkBox125.Checked ? 165 : 0);
			}
			if (this.checkBox126.Checked)
			{
				wgMjControllerConfigure.check_controller_online_timeout = (int)this.numericUpDown19.Value;
			}
			if (this.checkBox132.Checked)
			{
				wgMjControllerConfigure.elevatorSingleDelay = (float)this.numericUpDown21.Value;
				wgMjControllerConfigure.elevatorMultioutputDelay = (float)this.numericUpDown20.Value;
			}
			if (this.checkBox134.Checked)
			{
				if (string.IsNullOrEmpty(this.textBox27.Text) || string.IsNullOrEmpty(this.textBox28.Text) || string.IsNullOrEmpty(this.textBox33.Text))
				{
					wgMjControllerConfigure.custom_cardformat_totalbits = 0;
					wgMjControllerConfigure.custom_cardformat_startloc = 0;
					wgMjControllerConfigure.custom_cardformat_validbits = 0;
					wgMjControllerConfigure.custom_cardformat_sumcheck = 0;
				}
				else
				{
					wgMjControllerConfigure.custom_cardformat_totalbits = int.Parse(this.textBox33.Text);
					wgMjControllerConfigure.custom_cardformat_startloc = int.Parse(this.textBox28.Text);
					wgMjControllerConfigure.custom_cardformat_validbits = int.Parse(this.textBox27.Text);
					wgMjControllerConfigure.custom_cardformat_sumcheck = (wgMjControllerConfigure.custom_cardformat_totalbits + wgMjControllerConfigure.custom_cardformat_startloc + wgMjControllerConfigure.custom_cardformat_validbits & 255);
				}
			}
			if (this.checkBox136.Checked)
			{
				if (this.radioButton29.Checked)
				{
					wgMjControllerConfigure.fire_broadcast_receive = 0;
				}
				else if (this.radioButton30.Checked)
				{
					wgMjControllerConfigure.fire_broadcast_receive = 128;
				}
				else if (this.radioButton31.Checked)
				{
					if (this.numericUpDown28.Value < 127m)
					{
						wgMjControllerConfigure.fire_broadcast_receive = (int)this.numericUpDown28.Value;
					}
					else
					{
						wgMjControllerConfigure.fire_broadcast_receive = 126;
					}
				}
				if (this.radioButton32.Checked)
				{
					wgMjControllerConfigure.fire_broadcast_send = 0;
				}
				else if (this.radioButton33.Checked)
				{
					wgMjControllerConfigure.fire_broadcast_send = (int)this.numericUpDown29.Value;
				}
			}
			if (this.checkBox137.Checked)
			{
				if (this.radioButton36.Checked)
				{
					wgMjControllerConfigure.interlock_broadcast_receive = 0;
				}
				else if (this.radioButton37.Checked)
				{
					if (this.numericUpDown28.Value < 253m)
					{
						wgMjControllerConfigure.interlock_broadcast_receive = (int)this.numericUpDown31.Value;
					}
					else
					{
						wgMjControllerConfigure.interlock_broadcast_send = 253;
					}
				}
				if (this.radioButton34.Checked)
				{
					wgMjControllerConfigure.interlock_broadcast_send = 0;
				}
				else if (this.radioButton35.Checked)
				{
					wgMjControllerConfigure.interlock_broadcast_send = (int)this.numericUpDown30.Value;
				}
			}
			if (this.checkBox138.Checked)
			{
				if (this.radioButton38.Checked)
				{
					wgMjControllerConfigure.antiback_broadcast_send = 0;
				}
				else if (this.radioButton39.Checked)
				{
					wgMjControllerConfigure.antiback_broadcast_send = (int)this.numericUpDown32.Value;
				}
			}
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.UpdateConfigureIP(wgMjControllerConfigure);
				if (this.checkBox50.Checked && this.radioButton7.Checked)
				{
					icController.RenewControlTaskListIP();
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void frmTestController_Load(object sender, EventArgs e)
		{
			this.player = new SoundPlayer();
			this.player.SoundLocation = this.newSoundFile;
			if (this.Text == "Test Controller Center")
			{
				dfrmInputNewName dfrmInputNewName = new dfrmInputNewName();
				try
				{
					dfrmInputNewName.setPasswordChar('@');
					if (dfrmInputNewName.ShowDialog(this) == DialogResult.OK && dfrmInputNewName.strNewName.Length >= "wg".Length + 1 && dfrmInputNewName.strNewName.Substring(0, "wg".Length) == "wg" && dfrmInputNewName.strNewName.Substring("wg".Length, DateTime.Now.Hour.ToString().Length) == DateTime.Now.Hour.ToString())
					{
						this.numericUpDown17.Value = wgUdpComm.CommTimeoutMsMin;
						this.panel1.Visible = false;
						if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("PRODUCT_SPECIAL_CARDS")))
						{
							this.textBox32.Text = wgAppConfig.GetKeyVal("PRODUCT_SPECIAL_CARDS");
						}
						return;
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					dfrmInputNewName.Dispose();
				}
				base.Close();
			}
		}

		public void onlyProduce()
		{
			foreach (TabPage tabPage in this.tabControl1.TabPages)
			{
				if (tabPage.Name != "tabPage13")
				{
					this.tabControl1.TabPages.Remove(tabPage);
				}
			}
			this.checkBox120.Visible = false;
			this.button2.Visible = false;
			this.button3.Visible = false;
			this.button49.Visible = false;
			this.button1.Visible = false;
			this.button29.Visible = false;
			this.button34.Visible = false;
			this.button31.Visible = false;
			this.button32.Visible = false;
			this.button51.Visible = false;
			this.label114.Visible = false;
			this.button43.Visible = false;
			this.button24.Visible = false;
			this.button27.Visible = false;
			this.button56.Visible = false;
			this.label111.Visible = false;
			this.txtOldCommPassword.Visible = false;
			this.label110.Visible = false;
			this.txtCommPassword.Visible = false;
			this.button35.Visible = false;
			this.button36.Visible = false;
			this.button54.Visible = false;
			this.button44.Visible = false;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			MjRegisterCard mjRegisterCard = new MjRegisterCard();
			string text = this.txtCardNO.Text;
			uint cardID;
			if (!uint.TryParse(text, NumberStyles.Integer, null, out cardID))
			{
				return;
			}
			mjRegisterCard.CardID = cardID;
			if (this.checkBox25.Checked)
			{
				mjRegisterCard.IsActivated = true;
			}
			if (this.checkBox24.Checked)
			{
				mjRegisterCard.IsDeleted = true;
			}
			if (this.checkBox111.Checked)
			{
				mjRegisterCard.IsSuperCard = true;
			}
			mjRegisterCard.Password = uint.Parse(this.txtPassword.Text);
			mjRegisterCard.ymdStart = this.dtpActivate.Value;
			mjRegisterCard.ymdEnd = this.dtpDeactivate.Value;
			mjRegisterCard.ControlSegIndexSet(1, byte.Parse(this.comboBox1.Text));
			mjRegisterCard.ControlSegIndexSet(2, byte.Parse(this.comboBox2.Text));
			mjRegisterCard.ControlSegIndexSet(3, byte.Parse(this.comboBox3.Text));
			mjRegisterCard.ControlSegIndexSet(4, byte.Parse(this.comboBox4.Text));
			mjRegisterCard.FirstCardSet(1, this.checkBox26.Checked);
			mjRegisterCard.FirstCardSet(2, this.checkBox27.Checked);
			mjRegisterCard.FirstCardSet(3, this.checkBox28.Checked);
			mjRegisterCard.FirstCardSet(4, this.checkBox29.Checked);
			mjRegisterCard.MoreCardGroupIndexSet(1, byte.Parse(this.comboBox5.Text));
			mjRegisterCard.MoreCardGroupIndexSet(2, byte.Parse(this.comboBox6.Text));
			mjRegisterCard.MoreCardGroupIndexSet(3, byte.Parse(this.comboBox7.Text));
			mjRegisterCard.MoreCardGroupIndexSet(4, byte.Parse(this.comboBox8.Text));
			if (this.radioButton24.Checked)
			{
				mjRegisterCard.maxSwipe = ((int)this.numericUpDown6.Value & 511);
			}
			else if (this.radioButton26.Checked)
			{
				mjRegisterCard.hmsEnd = this.dateTimePicker14.Value;
			}
			if (this.checkBox128.Checked && wgMjController.IsElevator((int)this.txtSN.Value))
			{
				mjRegisterCard.AllowFloors = 0uL;
				mjRegisterCard.AllowFloors |= (this.checkBox129.Checked ? 1099511627776uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox141.Checked ? 1uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox142.Checked ? 2uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox143.Checked ? 4uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox144.Checked ? 8uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox145.Checked ? 16uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox146.Checked ? 32uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox147.Checked ? 64uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox148.Checked ? 128uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox149.Checked ? 256uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox150.Checked ? 512uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox151.Checked ? 1024uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox152.Checked ? 2048uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox153.Checked ? 4096uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox154.Checked ? 8192uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox155.Checked ? 16384uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox156.Checked ? 32768uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox157.Checked ? 65536uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox158.Checked ? 131072uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox159.Checked ? 262144uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox160.Checked ? 524288uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox161.Checked ? 1048576uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox162.Checked ? 2097152uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox163.Checked ? 4194304uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox164.Checked ? 8388608uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox165.Checked ? 16777216uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox166.Checked ? 33554432uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox167.Checked ? 67108864uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox168.Checked ? 134217728uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox169.Checked ? 268435456uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox170.Checked ? 536870912uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox171.Checked ? 1073741824uL : 0uL);
                //mjRegisterCard.AllowFloors |= (this.checkBox172.Checked ? ((ulong)-2147483648) : 0uL);
                mjRegisterCard.AllowFloors |= (this.checkBox172.Checked ? 2147483648uL : 0uL);
                mjRegisterCard.AllowFloors |= (this.checkBox173.Checked ? 4294967296uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox174.Checked ? 8589934592uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox175.Checked ? 17179869184uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox176.Checked ? 34359738368uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox177.Checked ? 68719476736uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox178.Checked ? 137438953472uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox179.Checked ? 274877906944uL : 0uL);
				mjRegisterCard.AllowFloors |= (this.checkBox180.Checked ? 549755813888uL : 0uL);
				if ((mjRegisterCard.AllowFloors & 1099511627776uL) == 0uL)
				{
					bool flag = false;
					for (int i = 0; i < 40; i++)
					{
						if ((mjRegisterCard.AllowFloors & 1uL << i) > 0uL)
						{
							if (flag)
							{
								mjRegisterCard.AllowFloors |= (this.checkBox129.Checked ? 1099511627776uL : 0uL);
								break;
							}
							flag = true;
						}
					}
				}
			}
			wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege();
			try
			{
				if (this.checkBox120.Checked)
				{
					if (wgMjControllerPrivilege.AddPrivilegeOfOneCardIP((int)this.txtSN.Value, this.txtControllerIP.Text, (int)this.nudPort.Value, mjRegisterCard) < 0)
					{
						MessageBox.Show("failed\r\n");
						return;
					}
				}
				else if (wgMjControllerPrivilege.AddPrivilegeOfOneCardIP((int)this.txtSN.Value, "", 60000, mjRegisterCard) < 0)
				{
					MessageBox.Show("failed\r\n");
					return;
				}
				if (this.checkBox97.Checked)
				{
					for (int j = 0; j < 3499; j++)
					{
						mjRegisterCard.CardID += 1u;
						if (this.checkBox120.Checked)
						{
							if (wgMjControllerPrivilege.AddPrivilegeOfOneCardIP((int)this.txtSN.Value, this.txtControllerIP.Text, (int)this.nudPort.Value, mjRegisterCard) < 0)
							{
								MessageBox.Show("failed\r\n");
								break;
							}
						}
						else if (wgMjControllerPrivilege.AddPrivilegeOfOneCardIP((int)this.txtSN.Value, "", 60000, mjRegisterCard) < 0)
						{
							MessageBox.Show("failed\r\n");
							break;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				wgMjControllerPrivilege.Dispose();
			}
		}

		private void dgvControlConfure_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			string text = this.dgvControlConfure[0, e.RowIndex].Value as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.Equals("X"))
			{
				this.dgvControlConfure[0, e.RowIndex].Style.BackColor = Color.Red;
				this.dgvControlConfure[2, e.RowIndex].Style.BackColor = Color.Red;
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			this.numericUpDown1.Value = 26m;
		}

		private void button6_Click(object sender, EventArgs e)
		{
			this.numericUpDown2.Value = 1m;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			this.numericUpDown2.Value = 2m;
		}

		private void button8_Click(object sender, EventArgs e)
		{
			this.numericUpDown2.Value = 3m;
		}

		private void button9_Click(object sender, EventArgs e)
		{
			if (this.controlTaskList == null)
			{
				this.controlTaskList = new wgMjControllerTaskList();
			}
			MjControlTaskItem mjControlTaskItem = new MjControlTaskItem();
			mjControlTaskItem.ymdStart = this.dateTimePicker1.Value;
			mjControlTaskItem.ymdEnd = this.dateTimePicker2.Value;
			mjControlTaskItem.hms = this.dateTimePicker3.Value;
			mjControlTaskItem.weekdayControl = 0;
			MjControlTaskItem expr_54 = mjControlTaskItem;
			expr_54.weekdayControl += (this.checkBox43.Checked ? 1 : 0);
			MjControlTaskItem expr_74 = mjControlTaskItem;
			expr_74.weekdayControl += (this.checkBox44.Checked ? 2 : 0);
			MjControlTaskItem expr_94 = mjControlTaskItem;
			expr_94.weekdayControl += (this.checkBox45.Checked ? 4 : 0);
			MjControlTaskItem expr_B4 = mjControlTaskItem;
			expr_B4.weekdayControl += (this.checkBox46.Checked ? 8 : 0);
			MjControlTaskItem expr_D4 = mjControlTaskItem;
			expr_D4.weekdayControl += (this.checkBox47.Checked ? 16 : 0);
			MjControlTaskItem expr_F5 = mjControlTaskItem;
			expr_F5.weekdayControl += (this.checkBox48.Checked ? 32 : 0);
			MjControlTaskItem expr_116 = mjControlTaskItem;
			expr_116.weekdayControl += (this.checkBox49.Checked ? 64 : 0);
			mjControlTaskItem.paramLoc = (int)this.numericUpDown1.Value;
			mjControlTaskItem.paramValue = (byte)this.numericUpDown2.Value;
			this.listBox1.Items.Add(BitConverter.ToString(mjControlTaskItem.ToBytes()));
			this.controlTaskList.AddItem(mjControlTaskItem);
		}

		private void button10_Click(object sender, EventArgs e)
		{
			if (this.controlTaskList != null)
			{
				this.controlTaskList.Clear();
			}
			this.listBox1.Items.Clear();
		}

		private void button11_Click(object sender, EventArgs e)
		{
			if (this.controlTaskList != null)
			{
				icController icController = new icController();
				try
				{
					icController.ControllerSN = this.defaultSN;
					icController.IP = this.defaultIP;
					icController.PORT = this.defaultPORT;
					if (icController.UpdateControlTaskListIP(this.controlTaskList) > 0)
					{
						icController.RenewControlTaskListIP();
					}
				}
				catch (Exception)
				{
				}
				icController.Dispose();
			}
		}

		private void button12_Click(object sender, EventArgs e)
		{
			WGPacketSSI_FLASH_QUERY wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY();
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)this.defaultSN, (uint)MjControlTaskItem.flashStartAddr, (uint)(MjControlTaskItem.flashStartAddr + 1024 - 1));
				byte[] cmd = wGPacketSSI_FLASH_QUERY.ToBytes(wgUdpComm.udpPort);
				byte[] array = null;
				int num = wgUdpComm.udp_get(cmd, 300, wGPacketSSI_FLASH_QUERY.xid, this.defaultIP, this.defaultPORT, ref array);
				if (num >= 0)
				{
					if (array != null)
					{
						string text = BitConverter.ToString(array);
						this.txtInfo.AppendText(text);
						this.txtInfo.AppendText("\r\n");
					}
					wgTools.WriteLine(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")));
				}
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button13_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				DateTime dateTimeNew = DateTime.Parse(string.Format("{0} {1}", this.dateTimePicker4.Value.ToString("yyyy-MM-dd"), this.dateTimePicker5.Value.ToString("HH:mm:ss")));
				icController.AdjustTimeIP(dateTimeNew);
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button14_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				if (icController.AdjustTimeIP(DateTime.Now) <= 0)
				{
					this.failsound();
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button15_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				this.txtInfo.AppendText(DateTime.Now.ToString("当前电脑时间: yyyy-MM-dd HH:mm:ss.ffff\r\n"));
				icController.GetControllerRunInformationIP();
				this.txtInfo.AppendText(icController.runinfo.dtNow.ToString("控制器时间: yyyy-MM-dd HH:mm:ss\r\n") + "[星期" + icController.runinfo.weekday.ToString() + "]");
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button16_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				uint operatorId = 0u;
				ulong operatorCardNO = 18446744073709551615uL;
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				if (!string.IsNullOrEmpty(this.textBox18.Text))
				{
					operatorId = uint.Parse(this.textBox18.Text);
				}
				if (!string.IsNullOrEmpty(this.textBox19.Text))
				{
					operatorCardNO = ulong.Parse(this.textBox19.Text);
				}
				icController.RemoteOpenDoorIP(int.Parse(this.textBox20.Text), operatorId, operatorCardNO);
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button17_Click(object sender, EventArgs e)
		{
			WGPacket wGPacket = new WGPacket();
			wGPacket.type = 35;
			wGPacket.code = 16;
			wGPacket.iDevSnFrom = 0u;
			wGPacket.iDevSnTo = (uint)this.txtSN.Value;
			wGPacket.iCallReturn = 0;
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				byte[] cmd = wGPacket.ToBytes(wgUdpComm.udpPort);
				byte[] array = null;
				wgUdpComm.udp_get(cmd, 300, wGPacket.xid, this.defaultIP, this.defaultPORT, ref array);
				MjRegisterCardsParam mjRegisterCardsParam = new MjRegisterCardsParam();
				this.textBox1.AppendText(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")));
				this.textBox1.AppendText("\r\n");
				this.Refresh();
				if (array != null)
				{
					string text = BitConverter.ToString(array);
					this.txtInfo.AppendText(text);
					this.txtInfo.AppendText("\r\n");
					mjRegisterCardsParam.updateParam(array, 20);
					text = "";
					text += string.Format("权限起始页 = 0x{0:X8}[{0:d}]\r\n", mjRegisterCardsParam.iPrivilegeFirstIndexSSI_FLASH_PRIVILEGE_STARTADDR);
					text += string.Format("存储新有序权限的4K的页面 = 0x{0:X8}[{0:d}]\r\n", mjRegisterCardsParam.newPrivilegePage4KAddr);
					text += string.Format("自由的记录页面(无序的) = 0x{0:X8} [{0:d}]\r\n", mjRegisterCardsParam.freeNewPrivilegePageAddr);
					text += string.Format("是否有序的(自由页面中) = {0:d}\r\n", mjRegisterCardsParam.bOrderInfreePrivilegePage);
					text += string.Format("总权限数 = {0:d}\r\n", mjRegisterCardsParam.totalPrivilegeCount);
					text += string.Format("已删除的权限数 = {0:d}\r\n", mjRegisterCardsParam.deletedPrivilegeCount);
					this.txtInfo.AppendText(text);
				}
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button18_Click(object sender, EventArgs e)
		{
			if (this.controlTimeSegList != null)
			{
				icController icController = new icController();
				try
				{
					icController.ControllerSN = this.defaultSN;
					icController.IP = this.defaultIP;
					icController.PORT = this.defaultPORT;
					icController.UpdateControlTimeSegListIP(this.controlTimeSegList);
				}
				catch (Exception)
				{
				}
				icController.Dispose();
			}
		}

		private void button19_Click(object sender, EventArgs e)
		{
			if (this.controlTimeSegList == null)
			{
				this.controlTimeSegList = new icControllerTimeSegList();
			}
			MjControlTimeSeg mjControlTimeSeg = new MjControlTimeSeg();
			mjControlTimeSeg.ymdStart = this.dateTimePicker7.Value;
			mjControlTimeSeg.ymdEnd = this.dateTimePicker6.Value;
			mjControlTimeSeg.SegIndex = byte.Parse(this.comboBox57.Text.ToString());
			mjControlTimeSeg.TotalLimittedAccess = (int)((byte)this.numericUpDown10.Value);
			mjControlTimeSeg.LimittedMode = (this.checkBox105.Checked ? 1 : 0);
			mjControlTimeSeg.nextSeg = byte.Parse(this.comboBox58.Text.ToString());
			mjControlTimeSeg.weekdayControl = 0;
			MjControlTimeSeg expr_A6 = mjControlTimeSeg;
			expr_A6.weekdayControl += (this.checkBox98.Checked ? 1 : 0);
			MjControlTimeSeg expr_C6 = mjControlTimeSeg;
			expr_C6.weekdayControl += (this.checkBox99.Checked ? 2 : 0);
			MjControlTimeSeg expr_E6 = mjControlTimeSeg;
			expr_E6.weekdayControl += (this.checkBox100.Checked ? 4 : 0);
			MjControlTimeSeg expr_106 = mjControlTimeSeg;
			expr_106.weekdayControl += (this.checkBox101.Checked ? 8 : 0);
			MjControlTimeSeg expr_126 = mjControlTimeSeg;
			expr_126.weekdayControl += (this.checkBox102.Checked ? 16 : 0);
			MjControlTimeSeg expr_147 = mjControlTimeSeg;
			expr_147.weekdayControl += (this.checkBox103.Checked ? 32 : 0);
			MjControlTimeSeg expr_168 = mjControlTimeSeg;
			expr_168.weekdayControl += (this.checkBox104.Checked ? 64 : 0);
			mjControlTimeSeg.hmsStart1 = this.dateTimePicker8.Value;
			mjControlTimeSeg.hmsEnd1 = this.dateTimePicker9.Value;
			mjControlTimeSeg.hmsStart2 = this.dateTimePicker10.Value;
			mjControlTimeSeg.hmsEnd2 = this.dateTimePicker11.Value;
			mjControlTimeSeg.hmsStart3 = this.dateTimePicker12.Value;
			mjControlTimeSeg.hmsEnd3 = this.dateTimePicker13.Value;
			mjControlTimeSeg.LimittedAccess1 = (int)((byte)this.numericUpDown7.Value);
			mjControlTimeSeg.LimittedAccess2 = (int)((byte)this.numericUpDown8.Value);
			mjControlTimeSeg.LimittedAccess3 = (int)((byte)this.numericUpDown9.Value);
			mjControlTimeSeg.ControlByHoliday = (this.checkBox127.Checked ? 1 : 0);
			this.listBox2.Items.Add(BitConverter.ToString(mjControlTimeSeg.ToBytes()));
			this.controlTimeSegList.AddItem(mjControlTimeSeg);
		}

		private void button20_Click(object sender, EventArgs e)
		{
			if (this.controlTimeSegList != null)
			{
				this.controlTimeSegList.Clear();
				this.listBox2.Items.Clear();
			}
		}

		private void button21_Click(object sender, EventArgs e)
		{
			int gate = wgTools.gate;
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				wgTools.gate = int.Parse(WGPacket.Dpt("texCPBcVbYG8tfyU+wnXCl/Ea+TvHb316FPly/KlaYQ="));
				icController.GetControllerRunInformationIP();
				wgTools.gate = gate;
				this.lblWrongProductCode.Visible = false;
				if (icController.runinfo.wgcticks > 0u)
				{
					try
					{
						this.txtInfo.AppendText(string.Concat(new string[]
						{
							"\r\n电脑时间: ",
							DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
							"[星期",
							DateTime.Now.DayOfWeek.ToString(),
							"]"
						}));
						this.txtInfo.AppendText("\r\n控制器SN: " + icController.runinfo.CurrentControllerSN.ToString());
						this.txtInfo.AppendText("\r\n驱动版本: " + icController.runinfo.driverVersion);
						this.txtInfo.AppendText("\r\n产品类型: " + icController.ControllerProductTypeCode.ToString());
						if (wgTools.gate != icController.ControllerProductTypeCode)
						{
							this.lblWrongProductCode.Visible = true;
						}
						this.txtInfo.AppendText("\r\n控制器的系统CTICKS: " + icController.runinfo.wgcticks.ToString() + string.Format("[/20={0:d} 秒]", icController.runinfo.wgcticks / 20u));
						this.txtInfo.AppendText(string.Concat(new string[]
						{
							"\r\n控制器的实时时钟: ",
							icController.runinfo.dtNow.ToString(wgTools.YMDHMSFormat),
							"[星期",
							icController.runinfo.weekday.ToString(),
							"]"
						}));
						this.txtInfo.AppendText("\r\n控制器的上电时间: " + icController.runinfo.dtPowerOn.ToString(wgTools.YMDHMSFormat));
						this.txtInfo.AppendText(string.Format("\r\n1号读卡器上的合法刷卡次数: {0:d}\r\n", icController.runinfo.ReaderValidSwipeGet(1)));
						this.txtInfo.AppendText(string.Format("2号读卡器上的合法刷卡次数: {0:d}\r\n", icController.runinfo.ReaderValidSwipeGet(2)));
						this.txtInfo.AppendText(string.Format("3号读卡器上的合法刷卡次数: {0:d}\r\n", icController.runinfo.ReaderValidSwipeGet(3)));
						this.txtInfo.AppendText(string.Format("4号读卡器上的合法刷卡次数: {0:d}\r\n", icController.runinfo.ReaderValidSwipeGet(4)));
						this.txtInfo.AppendText(string.Format("故障号: {0:d}\r\n", icController.runinfo.appError));
						this.txtInfo.AppendText(string.Format("报警代号--1号门: {0:d}\r\n", icController.runinfo.WarnInfo(1)));
						this.txtInfo.AppendText(string.Format("报警代号--2号门: {0:d}\r\n", icController.runinfo.WarnInfo(2)));
						this.txtInfo.AppendText(string.Format("报警代号--3号门: {0:d}\r\n", icController.runinfo.WarnInfo(3)));
						this.txtInfo.AppendText(string.Format("报警代号--4号门: {0:d}\r\n", icController.runinfo.WarnInfo(4)));
						this.txtInfo.AppendText(string.Format("新记录数: {0:d}\r\n", icController.runinfo.newRecordsNum));
						this.txtInfo.AppendText(string.Format("刷卡终止索引: {0:d}\r\n", icController.runinfo.swipeEndIndex));
						this.txtInfo.AppendText(string.Format("刷卡存储的起始地址: {0:d}\r\n", icController.runinfo.swipeStartAddr));
						this.txtInfo.AppendText(string.Format("已提取记录索引: {0}\r\n", icController.runinfo.lastGetRecordIndex));
						this.txtInfo.AppendText(string.Format("注册卡数量: {0}\r\n", icController.runinfo.registerCardNumTotal));
						this.txtInfo.AppendText(string.Format("已删除注册卡数量: {0}\r\n", icController.runinfo.deletedRegisterCardNum));
						this.txtInfo.AppendText(string.Format("注册卡存储的起始地址: {0}\r\n", icController.runinfo.registerCardStartAddr));
						this.txtInfo.AppendText(string.Format("门内人数: {0}\r\n", icController.runinfo.totalPerson4AntibackShare));
						this.txtInfo.AppendText(string.Format("1号按钮状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 1) > 0) ? "     开门动作" : "--"));
						this.txtInfo.AppendText(string.Format("2号按钮状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 2) > 0) ? "     开门动作" : "--"));
						this.txtInfo.AppendText(string.Format("3号按钮状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 4) > 0) ? "     开门动作" : "--"));
						this.txtInfo.AppendText(string.Format("4号按钮状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 8) > 0) ? "     开门动作" : "--"));
						this.txtInfo.AppendText(string.Format("1号门磁状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 16) > 0) ? "     门开" : "门关"));
						this.txtInfo.AppendText(string.Format("2号门磁状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 32) > 0) ? "     门开" : "门关"));
						this.txtInfo.AppendText(string.Format("3号门磁状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 64) > 0) ? "     门开" : "门关"));
						this.txtInfo.AppendText(string.Format("4号门磁状态: {0}\r\n", ((icController.runinfo.pbdsStatus & 128) > 0) ? "     门开" : "门关"));
						this.txtInfo.AppendText(string.Format("1号  继电器: {0}\r\n", ((icController.runinfo.lockStatus & 1) > 0) ? "     动作开" : "锁门"));
						this.txtInfo.AppendText(string.Format("2号  继电器: {0}\r\n", ((icController.runinfo.lockStatus & 2) > 0) ? "     动作开" : "锁门"));
						this.txtInfo.AppendText(string.Format("3号  继电器: {0}\r\n", ((icController.runinfo.lockStatus & 4) > 0) ? "     动作开" : "锁门"));
						this.txtInfo.AppendText(string.Format("4号  继电器: {0}\r\n", ((icController.runinfo.lockStatus & 8) > 0) ? "     动作开" : "锁门"));
						if (icController.runinfo.reservedBytes[0] == 0)
						{
							this.txtInfo.AppendText(string.Format("管脚没问题\r\n", new object[0]));
						}
						else
						{
							this.txtInfo.AppendText(string.Format("failedPin 问题管脚号: {0}\r\n", icController.runinfo.reservedBytes[0]));
							this.txtInfo.AppendText(icDesc.failedPinDesc((int)icController.runinfo.reservedBytes[0]));
							if ((icController.runinfo.reservedBytes[1] & 240) == 0)
							{
								this.txtInfo.AppendText(string.Format("failedPinDesc 问题管脚PORT号: G{0:X}\r\n", icController.runinfo.reservedBytes[1]));
							}
							else
							{
								this.txtInfo.AppendText(string.Format("failedPinDesc 问题管脚PORT号: {0:X2}\r\n", icController.runinfo.reservedBytes[1]));
							}
							this.txtInfo.AppendText(string.Format("failedPinDiffPortType 问题管脚PORT类: {0:X2}\r\n", icController.runinfo.reservedBytes[2]));
							string text = "";
							switch (icController.runinfo.reservedBytes[2] >> 4)
							{
							case 1:
								text = "初始默认就有问题";
								break;
							case 2:
								text = "管脚高平设置时 就有问题";
								break;
							case 3:
								text = "管脚高平设置时 此脚 就有问题";
								break;
							case 4:
								text = "管脚低平设置时 就有问题";
								break;
							case 5:
								text = "管脚低平设置时 此脚 就有问题";
								break;
							}
							if ((icController.runinfo.reservedBytes[2] & 15) == 0)
							{
								this.txtInfo.AppendText(string.Format("产生问题的另一端口PORT= PORTG\r\n", new object[0]));
							}
							else
							{
								this.txtInfo.AppendText(string.Format("产生问题的另一端口PORT: PORT{0:X}\r\n", (int)(icController.runinfo.reservedBytes[2] & 15)));
							}
							if (text != "")
							{
								this.txtInfo.AppendText(text + "\r\n");
							}
							this.txtInfo.AppendText(string.Format("failedPinDiff 存在不同: {0:X2}\r\n", icController.runinfo.reservedBytes[3]));
						}
						this.txtInfo.AppendText("启动时的PORT值 progPORT_InputVal:\r\n");
						this.txtInfo.AppendText(string.Format("0x{0:X2},0x{1:X2},0x{2:X2},0x{3:X2},0x{4:X2},0x{5:X2},0x{6:X2}\r\n", new object[]
						{
							icController.runinfo.reservedBytes[4],
							icController.runinfo.reservedBytes[5],
							icController.runinfo.reservedBytes[6],
							icController.runinfo.reservedBytes[7],
							icController.runinfo.reservedBytes[8],
							icController.runinfo.reservedBytes[9],
							icController.runinfo.reservedBytes[10]
						}));
						this.txtInfo.AppendText(string.Format("PORTA= {0:X2}\r\n", icController.runinfo.reservedBytes[4]));
						this.txtInfo.AppendText(string.Format("PORTB= {0:X2}\r\n", icController.runinfo.reservedBytes[5]));
						this.txtInfo.AppendText(string.Format("PORTC= {0:X2}\r\n", icController.runinfo.reservedBytes[6]));
						this.txtInfo.AppendText(string.Format("PORTD= {0:X2}\r\n", icController.runinfo.reservedBytes[7]));
						this.txtInfo.AppendText(string.Format("PORTE= {0:X2}\r\n", icController.runinfo.reservedBytes[8]));
						this.txtInfo.AppendText(string.Format("PORTF= {0:X2}\r\n", icController.runinfo.reservedBytes[9]));
						this.txtInfo.AppendText(string.Format("PORTG= {0:X2}\r\n", icController.runinfo.reservedBytes[10]));
						this.txtInfo.AppendText("当前的PORT值 progPORTDATA_R:\r\n");
						this.txtInfo.AppendText(string.Format("PORTA= {0:X2}\r\n", icController.runinfo.reservedBytes[11]));
						this.txtInfo.AppendText(string.Format("PORTB= {0:X2}\r\n", icController.runinfo.reservedBytes[12]));
						this.txtInfo.AppendText(string.Format("PORTC= {0:X2}\r\n", icController.runinfo.reservedBytes[13]));
						this.txtInfo.AppendText(string.Format("PORTD= {0:X2}\r\n", icController.runinfo.reservedBytes[14]));
						this.txtInfo.AppendText(string.Format("PORTE= {0:X2}\r\n", icController.runinfo.reservedBytes[15]));
						this.txtInfo.AppendText(string.Format("PORTF= {0:X2}\r\n", icController.runinfo.reservedBytes[16]));
						this.txtInfo.AppendText(string.Format("PORTG= {0:X2}\r\n", icController.runinfo.reservedBytes[17]));
						this.txtInfo.AppendText(string.Format("门磁状态的8-15bit位[火警/强制锁门]: {0:X2}\r\n", icController.runinfo.pbdsStatusHigh));
						this.txtInfo.AppendText(string.Format("强制锁门: {0}\r\n", icController.runinfo.ForceLockIsActive ? "     动作" : "未动作"));
						this.txtInfo.AppendText(string.Format("火警: {0}\r\n", icController.runinfo.FireIsActive ? "     动作" : "未动作"));
						this.txtInfo.AppendText(string.Format("所有数据: {0}\r\n", icController.runinfo.BytesDataStr));
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
			wgTools.gate = gate;
		}

		private void button22_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.GetControllerRunInformationIP();
				if (icController.runinfo.wgcticks > 0u)
				{
					for (int i = 0; i < 16; i++)
					{
						this.txtInfo.AppendText(string.Format("FRam[{0:d}] = {1:d}\r\n", i, icController.runinfo.LngFRam(i)));
					}
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button23_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.UpdateFRamIP((uint)this.numericUpDown13.Value, (uint)this.numericUpDown14.Value);
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button24_Click(object sender, EventArgs e)
		{
		}

		private void button25_Click(object sender, EventArgs e)
		{
			WGPacketSSI_FLASH wGPacketSSI_FLASH = new WGPacketSSI_FLASH();
			wGPacketSSI_FLASH.type = 33;
			wGPacketSSI_FLASH.code = 48;
			wGPacketSSI_FLASH.iDevSnFrom = 0u;
			wGPacketSSI_FLASH.iDevSnTo = 4294967295u;
			wGPacketSSI_FLASH.iCallReturn = 0;
			wGPacketSSI_FLASH.ucData = new byte[1024];
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				wGPacketSSI_FLASH.iStartFlashAddr = (uint)(this.nudStartPage.Value * 1024m);
				wGPacketSSI_FLASH.iEndFlashAddr = wGPacketSSI_FLASH.iStartFlashAddr + 1024u - 1u;
				for (int i = 0; i < 1024; i++)
				{
					wGPacketSSI_FLASH.ucData[i] = 255;
				}
				int num = 0;
				while (num < this.nudDatalen.Value)
				{
					wGPacketSSI_FLASH.ucData[num] = (byte)(this.nudValue.Value * 17m);
					num++;
				}
				byte[] array = null;
				this.txtInfo.AppendText("button25 Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "\r\n");
				while (wGPacketSSI_FLASH.iStartFlashAddr <= (uint)(this.nudEndPage.Value * 1024m))
				{
					int num2 = wgUdpComm.udp_get(wGPacketSSI_FLASH.ToBytes(wgUdpComm.udpPort), 300, wGPacketSSI_FLASH.xid, this.defaultIP, this.defaultPORT, ref array);
					if (num2 < 0)
					{
						this.txtInfo.AppendText("button25 Err Ret=: " + num2.ToString() + "\r\n");
						break;
					}
					wGPacketSSI_FLASH.iStartFlashAddr += 1024u;
					this.label107.Text = (wGPacketSSI_FLASH.iStartFlashAddr / 1024u).ToString();
				}
				this.txtInfo.AppendText("button25 End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "\r\n");
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button26_Click(object sender, EventArgs e)
		{
			WGPacketSSI_FLASH_QUERY wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY();
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)this.txtSN.Value, (uint)(this.nudStartPage.Value * 1024m), (uint)(--(this.nudStartPage.Value * 1024m + 1024m)));
				byte[] cmd = wGPacketSSI_FLASH_QUERY.ToBytes(wgUdpComm.udpPort);
				byte[] array = null;
				int num = wgUdpComm.udp_get(cmd, 300, wGPacketSSI_FLASH_QUERY.xid, this.defaultIP, this.defaultPORT, ref array);
				if (num < 0)
				{
					wgUdpComm.Dispose();
					return;
				}
				if (array != null)
				{
					string text = BitConverter.ToString(array);
					this.txtInfo.AppendText(text);
					this.txtInfo.AppendText("\r\n");
				}
				wgTools.WriteLine(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")));
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button27_Click(object sender, EventArgs e)
		{
			MessageBox.Show("这是发行版本软件");
		}

		private void failsound()
		{
			try
			{
				this.player.Play();
			}
			catch (Exception)
			{
			}
		}

		private void button28_Click(object sender, EventArgs e)
		{
			MessageBox.Show("这是发行版本软件");
		}

		private void button29_Click(object sender, EventArgs e)
		{
			this.dfrmNetControllerConfig1 = new dfrmNetControllerConfig();
			this.dfrmNetControllerConfig1.Show();
		}

		private void button30_Click(object sender, EventArgs e)
		{
		}

		private void button31_Click(object sender, EventArgs e)
		{
		}

		private void button32_Click(object sender, EventArgs e)
		{
			Interaction.Shell("SqlSet.exe UPDATE", AppWinStyle.Hide, false, -1);
			Thread.Sleep(5000);
		}

		private void DisplayData(DataTable table)
		{
			foreach (DataRow dataRow in table.Rows)
			{
				foreach (DataColumn dataColumn in table.Columns)
				{
					wgTools.WgDebugWrite("{0} = {1}", new object[]
					{
						dataColumn.ColumnName,
						dataRow[dataColumn]
					});
				}
				wgTools.WgDebugWrite("============================", new object[0]);
			}
		}

		private void button33_Click(object sender, EventArgs e)
		{
			SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
			DataTable dataSources = instance.GetDataSources();
			this.DisplayData(dataSources);
		}

		private void button34_Click(object sender, EventArgs e)
		{
			Form arg_06_0 = base.Owner;
		}

		private void tabPage13_Click(object sender, EventArgs e)
		{
		}

		private void button35_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				byte[] array = new byte[1152];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				char[] array2 = this.txtCommPassword.Text.PadRight(16, '\0').ToCharArray();
				int num = 16;
				int num2 = 0;
				while (num2 < 16 && num2 < array2.Length)
				{
					array[num] = (byte)(array2[num2] & 'ÿ');
					byte[] expr_88_cp_0 = array;
					int expr_88_cp_1 = 1024 + (num >> 3);
					expr_88_cp_0[expr_88_cp_1] |= (byte)(1 << (num & 7));
					num++;
					num2++;
				}
				icController.UpdateConfigureCPUSuperIP(array, this.txtOldCommPassword.Text);
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button36_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtCommPassword.Text))
			{
				wgAppConfig.UpdateKeyVal("CommCurrent", "");
				return;
			}
			this.txtCommPassword.Text = this.txtCommPassword.Text.Trim();
			if (string.IsNullOrEmpty(this.txtCommPassword.Text))
			{
				wgAppConfig.UpdateKeyVal("CommCurrent", "");
				return;
			}
			wgAppConfig.UpdateKeyVal("CommCurrent", WGPacket.Ept(this.txtCommPassword.Text));
		}

		private void button37_Click(object sender, EventArgs e)
		{
			if (this.timer1.Enabled)
			{
				this.timer1.Enabled = false;
			}
			if (this.control4Tcp == null)
			{
				this.control4Tcp = new icController();
			}
			try
			{
				this.control4Tcp.ControllerSN = this.defaultSN;
				this.control4Tcp.IP = this.defaultIP;
				this.control4Tcp.PORT = this.defaultPORT;
				this.control4Tcp.GetControllerRunInformationIP_TCP(this.textBox21.Text);
				if (this.control4Tcp.runinfo.wgcticks > 0u)
				{
					this.label113.Text = this.control4Tcp.runinfo.dtNow.ToString(wgTools.YMDHMSFormat);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void button38_Click(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			if (this.control4Tcp != null)
			{
				this.control4Tcp.TCP_Close();
				this.control4Tcp = null;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			this.button43.PerformClick();
		}

		private void button39_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 10; i++)
			{
				this.frmTestController1 = new frmTestController();
				this.frmTestController1.Text = this.frmTestController1.Text + (i + 2).ToString();
				this.frmTestController1.Show();
			}
		}

		private void button40_Click(object sender, EventArgs e)
		{
			if (this.control4Tcp == null)
			{
				this.control4Tcp = new icController();
			}
			icController icController = this.control4Tcp;
			icController.ControllerSN = this.defaultSN;
			icController.IP = this.defaultIP;
			icController.PORT = this.defaultPORT;
			icController.TCP_Open(this.textBox21.Text);
			icController.AdjustTimeIP_TCP(DateTime.Now);
		}

		private void button41_Click(object sender, EventArgs e)
		{
			if (this.control4Tcp == null)
			{
				this.control4Tcp = new icController();
			}
			icController icController = this.control4Tcp;
			icController.ControllerSN = this.defaultSN;
			icController.IP = this.defaultIP;
			icController.PORT = this.defaultPORT;
			icController.TCP_Open(this.textBox21.Text);
			uint operatorId = 0u;
			uint operatorCardNO = 4294967295u;
			icController.ControllerSN = this.defaultSN;
			icController.IP = this.defaultIP;
			icController.PORT = this.defaultPORT;
			if (!string.IsNullOrEmpty(this.textBox18.Text))
			{
				operatorId = uint.Parse(this.textBox18.Text);
			}
			if (!string.IsNullOrEmpty(this.textBox19.Text))
			{
				operatorCardNO = uint.Parse(this.textBox19.Text);
			}
			icController.RemoteOpenDoorIP_TCP(int.Parse(this.textBox20.Text), operatorId, operatorCardNO);
		}

		private void button42_Click(object sender, EventArgs e)
		{
			if (this.control4Tcp == null)
			{
				this.control4Tcp = new icController();
			}
			icController icController = this.control4Tcp;
			icController.ControllerSN = this.defaultSN;
			icController.IP = this.defaultIP;
			icController.PORT = this.defaultPORT;
			icController.TCP_Open(this.textBox21.Text);
			string productInfoIP_TCP = icController.GetProductInfoIP_TCP();
			if (!string.IsNullOrEmpty(productInfoIP_TCP))
			{
				this.label113.Text = productInfoIP_TCP;
			}
		}

		private void button43_Click(object sender, EventArgs e)
		{
			if (this.timer1.Enabled)
			{
				this.timer1.Enabled = false;
			}
			if (this.control4Udp == null)
			{
				this.control4Udp = new icController();
				this.lastRecordStr = "";
			}
			icController icController = this.control4Udp;
			icController.ControllerSN = this.defaultSN;
			icController.IP = this.defaultIP;
			icController.PORT = this.defaultPORT;
			icController.GetControllerRunInformationIP();
			if (icController.runinfo.wgcticks > 0u)
			{
				this.label114.Text = icController.runinfo.dtNow.ToString(wgTools.YMDHMSFormat);
				if (icController.runinfo.newSwipes[0].ToStringRaw() != this.lastRecordStr)
				{
					this.lastRecordStr = icController.runinfo.newSwipes[0].ToStringRaw();
					wgMjControllerSwipeRecord wgMjControllerSwipeRecord = icController.runinfo.newSwipes[0];
					this.txtInfo.AppendText(wgMjControllerSwipeRecord.ToDisplaySimpleInfo(true) + "\r\n");
				}
			}
			this.timer1.Enabled = true;
		}

		private void button44_Click(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
		}

		private void button45_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				string text = "";
				string str = "";
				string productInfoIP = icController.GetProductInfoIP(ref text, ref str);
				if (!string.IsNullOrEmpty(productInfoIP))
				{
					this.txtInfo.AppendText(str + "\r\n");
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button46_Click(object sender, EventArgs e)
		{
			this.txtInfo.Text = "";
		}

		private void button47_Click(object sender, EventArgs e)
		{
			string text = "CREATE PARTITION FUNCTION [RangePrivilegePF1](int) AS RANGE LEFT FOR VALUES (";
			string text2 = "CREATE PARTITION SCHEME [RangePrivilegePS1] AS PARTITION [RangePrivilegePF1] TO (";
			for (int i = 1; i < 999; i++)
			{
				text += string.Format("N'{0:d}',", i);
				text2 += string.Format("[PRIMARY],", new object[0]);
			}
			text += string.Format("N'{0:d}')", 999);
			text2 += string.Format("[PRIMARY],[PRIMARY])", new object[0]);
			this.txtInfo.AppendText(text);
			this.txtInfo.AppendText("\r\n");
			this.txtInfo.AppendText(text2);
			this.txtInfo.AppendText("\r\n");
		}

		private void button48_Click(object sender, EventArgs e)
		{
		}

		private void button49_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.RebootControllerIP();
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button50_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				byte[] value = new byte[2048];
				long num = 0L;
				if (long.TryParse(this.textBox26.Text, NumberStyles.AllowHexSpecifier, null, out num))
				{
					int num2 = (int)(num << 16 & (long)((ulong)-65536));
					int start;
					if (int.TryParse(this.textBox24.Text, out start) || int.TryParse(this.textBox24.Text.Trim().ToUpper().Replace("0X", ""), NumberStyles.AllowHexSpecifier, null, out start))
					{
						wgTools.CommPStr = "";
						if (icController.GetControlDataIP(start, num2 + int.Parse(this.textBox25.Text), ref value) > 0)
						{
							string text = BitConverter.ToString(value);
							this.txtInfo.AppendText(text.Substring(84, int.Parse(this.textBox25.Text) * 3));
							this.txtInfo.AppendText("\r\n");
						}
						wgTools.CommPStr = wgAppConfig.GetKeyVal("CommCurrent");
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

		private void button51_Click(object sender, EventArgs e)
		{
			this.frmProductFormat1 = new frmProductFormat();
			this.frmProductFormat1.Show();
		}

		private void frmTestController_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Escape && keyCode == Keys.F12 && this.checkBox115.Checked)
			{
				this.button28_Click(null, null);
			}
			if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
			{
				if (this.dfrmFind1 == null)
				{
					this.dfrmFind1 = new dfrmFind();
				}
				this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
			}
		}

		private void button52_Click(object sender, EventArgs e)
		{
			MessageBox.Show("这是发行版本软件");
		}

		private void textBox29_TextChanged(object sender, EventArgs e)
		{
			long num;
			if (long.TryParse(this.textBox29.Text.Trim().ToUpper().Replace("0X", ""), NumberStyles.AllowHexSpecifier, null, out num))
			{
				this.textBox30.Text = num.ToString();
				return;
			}
			this.textBox30.Text = "";
		}

		private void button53_Click(object sender, EventArgs e)
		{
			WGPacket wGPacket = new WGPacket();
			wGPacket.type = 35;
			wGPacket.code = 16;
			wGPacket.iDevSnFrom = 0u;
			wGPacket.iDevSnTo = (uint)this.txtSN.Value;
			wGPacket.iCallReturn = 0;
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				byte[] array = wGPacket.ToBytes(wgUdpComm.udpPort);
				if (array != null)
				{
					byte[] array2 = null;
					int num = wgUdpComm.udp_get(array, 300, wGPacket.xid, this.defaultIP, this.defaultPORT, ref array2);
					MjRegisterCardsParam mjRegisterCardsParam = new MjRegisterCardsParam();
					this.textBox1.AppendText(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")));
					this.textBox1.AppendText("\r\n");
					this.Refresh();
					if (array2 != null)
					{
						string str = BitConverter.ToString(array2);
						mjRegisterCardsParam.updateParam(array2, 20);
						str = "";
						str += string.Format("权限起始页 = 0x{0:X8}[{0:d}]\r\n", mjRegisterCardsParam.iPrivilegeFirstIndexSSI_FLASH_PRIVILEGE_STARTADDR);
						str += string.Format("存储新有序权限的4K的页面 = 0x{0:X8}[{0:d}]\r\n", mjRegisterCardsParam.newPrivilegePage4KAddr);
						str += string.Format("自由的记录页面(无序的) = 0x{0:X8} [{0:d}]\r\n", mjRegisterCardsParam.freeNewPrivilegePageAddr);
						str += string.Format("是否有序的(自由页面中) = {0:d}\r\n", mjRegisterCardsParam.bOrderInfreePrivilegePage);
						str += string.Format("总权限数 = {0:d}\r\n", mjRegisterCardsParam.totalPrivilegeCount);
						str += string.Format("已删除的权限数 = {0:d}\r\n", mjRegisterCardsParam.deletedPrivilegeCount);
						long num2 = (long)this.numericUpDown15.Value;
						long num3;
						if ((ulong)((mjRegisterCardsParam.newPrivilegePage4KAddr - mjRegisterCardsParam.iPrivilegeFirstIndexSSI_FLASH_PRIVILEGE_STARTADDR) / 8u) > (ulong)num2)
						{
							num3 = (long)((ulong)mjRegisterCardsParam.iPrivilegeFirstIndexSSI_FLASH_PRIVILEGE_STARTADDR + (ulong)(num2 * 8L));
						}
						else
						{
							num2 -= (long)((ulong)((mjRegisterCardsParam.newPrivilegePage4KAddr - mjRegisterCardsParam.iPrivilegeFirstIndexSSI_FLASH_PRIVILEGE_STARTADDR) / 8u));
							num3 = (long)((ulong)(mjRegisterCardsParam.newPrivilegePage4KAddr + 4096u) + (ulong)(num2 * 8L));
						}
						WGPacketSSI_FLASH_QUERY wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY();
						wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)this.txtSN.Value, (uint)num3, (uint)(num3 + 1024L - 1L));
						byte[] array3 = wGPacketSSI_FLASH_QUERY.ToBytes(wgUdpComm.udpPort);
						if (array != null)
						{
							byte[] array4 = null;
							num = wgUdpComm.udp_get(array3, 300, wGPacketSSI_FLASH_QUERY.xid, this.defaultIP, this.defaultPORT, ref array4);
							if (num >= 0)
							{
								MjRegisterCard mjRegisterCard = new MjRegisterCard();
								string text = "";
								if (array4 != null)
								{
									string text2 = BitConverter.ToString(array4);
									mjRegisterCard.CardID = (uint)((int)array4[28] | (int)array4[29] << 8 | (int)array4[30] << 16 | (int)array4[31] << 24);
									text2 = BitConverter.ToString(array4).Replace("-", "").Substring(56, 16) + "[" + mjRegisterCard.CardID.ToString() + "]";
									text = BitConverter.ToString(array4).Replace("-", "").Substring(56, 16);
									this.txtInfo.AppendText(text2);
									this.txtInfo.AppendText("\r\n");
								}
								num3 = num3 * 2L + 1654784L;
								wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)this.txtSN.Value, (uint)num3, (uint)(num3 + 1024L - 1L));
								array3 = wGPacketSSI_FLASH_QUERY.ToBytes(wgUdpComm.udpPort);
								if (array3 != null)
								{
									array4 = null;
									num = wgUdpComm.udp_get(array3, 300, wGPacketSSI_FLASH_QUERY.xid, null, 60000, ref array4);
									if (num >= 0)
									{
										if (array4 != null)
										{
											try
											{
												string text3 = BitConverter.ToString(array4).Replace("-", "").Substring(56, 32);
												this.txtInfo.AppendText(text3);
												text += BitConverter.ToString(array4).Replace("-", "").Substring(56, 32);
												mjRegisterCard.Update(text);
												if (wgMjController.IsElevator((int)this.txtSN.Value))
												{
													string text4 = "允许到达的楼层: ";
													for (int i = 0; i < 40; i++)
													{
														if ((mjRegisterCard.AllowFloors & 1uL << i) > 0uL)
														{
															text4 = text4 + (i + 1).ToString() + ",";
														}
													}
													if ((mjRegisterCard.AllowFloors & 1099511627776uL) > 0uL)
													{
														text4 += "\r\n<多层...>";
													}
													this.txtInfo.AppendText("\r\n");
													this.txtInfo.AppendText(text4);
												}
												this.txtInfo.AppendText("\r\n");
											}
											catch (Exception)
											{
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				wgUdpComm.Dispose();
			}
		}

		private void button54_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				string str = "";
				string str2 = "";
				string productInfoIP = icController.GetProductInfoIP(ref str, ref str2);
				if (!string.IsNullOrEmpty(productInfoIP))
				{
					this.txtInfo.AppendText(productInfoIP + "\r\n");
					this.txtInfo.AppendText("compactInfo \r\n");
					this.txtInfo.AppendText(str + "\r\n");
					this.txtInfo.AppendText("descInfo\r\n");
					this.txtInfo.AppendText(str2 + "\r\n");
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button55_Click(object sender, EventArgs e)
		{
			int gate = wgTools.gate;
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.ControllerSN = -1;
				wgTools.gate = int.Parse(WGPacket.Dpt("SYP2ipKrbEk9QZIr9LfOoZehvmmtcOURHRu+FOBrU1Q="));
				icController.GetControllerRunInformationIP();
				wgTools.gate = gate;
				if (icController.runinfo.wgcticks <= 0u)
				{
					this.txtInfo.AppendText("???控制器未连接\r\n");
					this.txtSN.Value = 0m;
				}
				else if (icController.runinfo.CurrentControllerSN == 4294967295u)
				{
					this.txtSN.Value = 999999999m;
				}
				else
				{
					this.txtSN.Value = icController.runinfo.CurrentControllerSN;
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
			wgTools.gate = gate;
		}

		private void button56_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			this.bFoundController = false;
			try
			{
				icController.ControllerSN = -1;
				icController.GetControllerRunInformationIP();
				if (icController.runinfo.wgcticks <= 0u)
				{
					this.txtInfo.AppendText("???控制器未连接\r\n");
				}
				else
				{
					this.bFoundController = true;
					if (icController.runinfo.CurrentControllerSN != 4294967295u)
					{
						this.nudNewSN.Value = icController.runinfo.CurrentControllerSN;
					}
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
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
					this.txtInfo.AppendText("???控制器未连接\r\n");
					SystemSounds.Hand.Play();
				}
				else
				{
					if (this.checkBox116.Checked)
					{
						MjRegisterCard mjRegisterCard = new MjRegisterCard();
						mjRegisterCard.IsActivated = true;
						mjRegisterCard.Password = uint.Parse(this.txtPassword.Text);
						mjRegisterCard.ymdStart = this.dtpActivate.Value;
						mjRegisterCard.ymdEnd = this.dtpDeactivate.Value;
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
			icController.Dispose();
		}

		private void checkBox120_CheckedChanged(object sender, EventArgs e)
		{
			this.grpbIP.Visible = this.checkBox120.Checked;
			if (this.checkBox120.Checked)
			{
				this.defaultIP = this.txtControllerIP.Text;
				this.defaultPORT = (int)this.nudPort.Value;
				return;
			}
			this.defaultIP = "";
			this.defaultPORT = 60000;
		}

		private void button60_Click(object sender, EventArgs e)
		{
			this.numericUpDown17.Value = wgUdpComm.CommTimeoutMsMin;
		}

		private void button59_Click(object sender, EventArgs e)
		{
			wgUdpComm.CommTimeoutMsMin = (long)((int)this.numericUpDown17.Value);
		}

		private void button61_Click(object sender, EventArgs e)
		{
		}

		private void txtSN_ValueChanged(object sender, EventArgs e)
		{
			this.defaultSN = (int)this.txtSN.Value;
		}

		private void button58_Click(object sender, EventArgs e)
		{
			byte[] array = new byte[1152];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			int num = 32;
			for (int j = 0; j < 64; j++)
			{
				array[num] = 152;
				byte[] expr_3B_cp_0 = array;
				int expr_3B_cp_1 = 1024 + (num >> 3);
				expr_3B_cp_0[expr_3B_cp_1] |= (byte)(1 << (num & 7));
				num++;
			}
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.UpdateConfigureCPUSuperIP(array, "");
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button61_Click_1(object sender, EventArgs e)
		{
			if (this.controlHolidayList == null)
			{
				this.controlHolidayList = new wgMjControllerHolidaysList();
			}
			MjControlHolidayTime mjControlHolidayTime = new MjControlHolidayTime();
			mjControlHolidayTime.dtStart = DateTime.Parse(this.dateTimePicker17.Value.ToString("yyyy-MM-dd ") + this.dateTimePicker15.Value.ToString("HH:mm"));
			mjControlHolidayTime.dtEnd = DateTime.Parse(this.dateTimePicker16.Value.ToString("yyyy-MM-dd ") + this.dateTimePicker18.Value.ToString("HH:mm"));
			this.listBox3.Items.Add(BitConverter.ToString(mjControlHolidayTime.ToBytes()));
			this.controlHolidayList.AddItem(mjControlHolidayTime);
		}

		private void button63_Click(object sender, EventArgs e)
		{
			if (this.controlHolidayList != null)
			{
				icController icController = new icController();
				try
				{
					icController.ControllerSN = this.defaultSN;
					icController.IP = this.defaultIP;
					icController.PORT = this.defaultPORT;
					if (icController.UpdateHolidayListIP(this.controlHolidayList.ToByte()) > 0)
					{
						this.txtInfo.AppendText("上传假期 OK\r\n");
					}
					else
					{
						this.txtInfo.AppendText("上传假期 Failed\r\n");
					}
					icController.UpdateConfigureIP(new wgMjControllerConfigure
					{
						holidayControl = (int)((this.controlHolidayList.holidayCount > 0) ? 1 : 0)
					});
				}
				catch (Exception)
				{
				}
				icController.Dispose();
			}
		}

		private void button62_Click(object sender, EventArgs e)
		{
			if (this.controlHolidayList != null)
			{
				this.controlHolidayList.Clear();
				this.listBox3.Items.Clear();
			}
		}

		private void button67_Click(object sender, EventArgs e)
		{
			if (this.controlTaskList == null)
			{
				this.controlTaskList = new wgMjControllerTaskList();
			}
			if (this.cboDoors.SelectedIndex < 0 || this.cboBeginControlStatus.SelectedIndex < 0 || this.cboEndControlStatus.SelectedIndex < 0)
			{
				MessageBox.Show("请先选择各参数");
				return;
			}
			MjControlTaskItem mjControlTaskItem = new MjControlTaskItem();
			mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
			mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
			mjControlTaskItem.hms = this.dateBeginHMS1.Value;
			mjControlTaskItem.weekdayControl = 0;
			MjControlTaskItem expr_88 = mjControlTaskItem;
			expr_88.weekdayControl += (this.chkMonday.Checked ? 1 : 0);
			MjControlTaskItem expr_A8 = mjControlTaskItem;
			expr_A8.weekdayControl += (this.chkTuesday.Checked ? 2 : 0);
			MjControlTaskItem expr_C8 = mjControlTaskItem;
			expr_C8.weekdayControl += (this.chkWednesday.Checked ? 4 : 0);
			MjControlTaskItem expr_E8 = mjControlTaskItem;
			expr_E8.weekdayControl += (this.chkThursday.Checked ? 8 : 0);
			MjControlTaskItem expr_108 = mjControlTaskItem;
			expr_108.weekdayControl += (this.chkFriday.Checked ? 16 : 0);
			MjControlTaskItem expr_129 = mjControlTaskItem;
			expr_129.weekdayControl += (this.chkSaturday.Checked ? 32 : 0);
			MjControlTaskItem expr_14A = mjControlTaskItem;
			expr_14A.weekdayControl += (this.chkSunday.Checked ? 64 : 0);
			switch (this.cboBeginControlStatus.SelectedIndex)
			{
			case 0:
				mjControlTaskItem.paramValue = 19;
				break;
			case 1:
				mjControlTaskItem.paramValue = 17;
				break;
			case 2:
				mjControlTaskItem.paramValue = 18;
				break;
			case 3:
				mjControlTaskItem.paramValue = 20;
				break;
			default:
				mjControlTaskItem.paramValue = 0;
				break;
			}
			mjControlTaskItem.paramLoc = 180 + this.cboDoors.SelectedIndex;
			if (this.controlTaskList.AddItem(mjControlTaskItem) < 0)
			{
				wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
			}
			else
			{
				this.listBox4.Items.Add(BitConverter.ToString(mjControlTaskItem.ToBytes()));
			}
			mjControlTaskItem = new MjControlTaskItem();
			mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
			mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
			mjControlTaskItem.hms = this.dateEndHMS1.Value;
			mjControlTaskItem.weekdayControl = 0;
			MjControlTaskItem expr_250 = mjControlTaskItem;
			expr_250.weekdayControl += (this.chkMonday.Checked ? 1 : 0);
			MjControlTaskItem expr_270 = mjControlTaskItem;
			expr_270.weekdayControl += (this.chkTuesday.Checked ? 2 : 0);
			MjControlTaskItem expr_290 = mjControlTaskItem;
			expr_290.weekdayControl += (this.chkWednesday.Checked ? 4 : 0);
			MjControlTaskItem expr_2B0 = mjControlTaskItem;
			expr_2B0.weekdayControl += (this.chkThursday.Checked ? 8 : 0);
			MjControlTaskItem expr_2D0 = mjControlTaskItem;
			expr_2D0.weekdayControl += (this.chkFriday.Checked ? 16 : 0);
			MjControlTaskItem expr_2F1 = mjControlTaskItem;
			expr_2F1.weekdayControl += (this.chkSaturday.Checked ? 32 : 0);
			MjControlTaskItem expr_312 = mjControlTaskItem;
			expr_312.weekdayControl += (this.chkSunday.Checked ? 64 : 0);
			switch (this.cboEndControlStatus.SelectedIndex)
			{
			case 0:
				mjControlTaskItem.paramValue = 0;
				break;
			case 1:
				mjControlTaskItem.paramValue = 0;
				break;
			case 2:
				mjControlTaskItem.paramValue = 0;
				break;
			case 3:
				mjControlTaskItem.paramValue = 20;
				break;
			default:
				mjControlTaskItem.paramValue = 0;
				break;
			}
			mjControlTaskItem.paramLoc = 180 + this.cboDoors.SelectedIndex;
			if (this.controlTaskList.AddItem(mjControlTaskItem) < 0)
			{
				wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
			}
			else
			{
				this.listBox4.Items.Add(BitConverter.ToString(mjControlTaskItem.ToBytes()));
			}
			mjControlTaskItem = new MjControlTaskItem();
			mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
			mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
			mjControlTaskItem.hms = this.dateEndHMS1.Value;
			mjControlTaskItem.weekdayControl = 0;
			MjControlTaskItem expr_415 = mjControlTaskItem;
			expr_415.weekdayControl += (this.chkMonday.Checked ? 1 : 0);
			MjControlTaskItem expr_435 = mjControlTaskItem;
			expr_435.weekdayControl += (this.chkTuesday.Checked ? 2 : 0);
			MjControlTaskItem expr_455 = mjControlTaskItem;
			expr_455.weekdayControl += (this.chkWednesday.Checked ? 4 : 0);
			MjControlTaskItem expr_475 = mjControlTaskItem;
			expr_475.weekdayControl += (this.chkThursday.Checked ? 8 : 0);
			MjControlTaskItem expr_495 = mjControlTaskItem;
			expr_495.weekdayControl += (this.chkFriday.Checked ? 16 : 0);
			MjControlTaskItem expr_4B6 = mjControlTaskItem;
			expr_4B6.weekdayControl += (this.chkSaturday.Checked ? 32 : 0);
			MjControlTaskItem expr_4D7 = mjControlTaskItem;
			expr_4D7.weekdayControl += (this.chkSunday.Checked ? 64 : 0);
			switch (this.cboEndControlStatus.SelectedIndex)
			{
			case 0:
				mjControlTaskItem.paramValue = 3;
				break;
			case 1:
				mjControlTaskItem.paramValue = 1;
				break;
			case 2:
				mjControlTaskItem.paramValue = 2;
				break;
			case 3:
				mjControlTaskItem.paramValue = 3;
				break;
			default:
				mjControlTaskItem.paramValue = 3;
				break;
			}
			mjControlTaskItem.paramLoc = 26 + this.cboDoors.SelectedIndex;
			if (this.controlTaskList.AddItem(mjControlTaskItem) < 0)
			{
				wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
				return;
			}
			this.listBox4.Items.Add(BitConverter.ToString(mjControlTaskItem.ToBytes()));
		}

		private void button66_Click(object sender, EventArgs e)
		{
			this.listBox4.Items.Clear();
			if (this.controlTaskList != null)
			{
				this.controlTaskList.Clear();
			}
		}

		private void button65_Click(object sender, EventArgs e)
		{
			if (this.controlTaskList != null)
			{
				icController icController = new icController();
				try
				{
					icController.ControllerSN = this.defaultSN;
					icController.IP = this.defaultIP;
					icController.PORT = this.defaultPORT;
					wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
					wgMjControllerConfigure.FirstCardInfoSet(1, 0);
					wgMjControllerConfigure.FirstCardInfoSet(2, 0);
					wgMjControllerConfigure.FirstCardInfoSet(3, 0);
					wgMjControllerConfigure.FirstCardInfoSet(4, 0);
					if (this.controlTaskList.taskCount > 0)
					{
						wgMjControllerConfigure.controlTaskList_enabled = 1;
					}
					if (icController.UpdateConfigureIP(wgMjControllerConfigure) > 0 && icController.UpdateControlTaskListIP(this.controlTaskList) > 0)
					{
						icController.RenewControlTaskListIP();
					}
				}
				catch (Exception)
				{
				}
				icController.Dispose();
			}
		}

		private void button68_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege();
			try
			{
				wgMjControllerPrivilege.AllowDownload();
				if (this.dtPrivilege != null)
				{
					this.dtPrivilege.Rows.Clear();
					this.dtPrivilege.Dispose();
					this.dtPrivilege = null;
					GC.Collect();
				}
				this.dtPrivilege = new DataTable("Privilege");
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
				if (wgMjControllerPrivilege.DownloadIP(this.defaultSN, this.defaultIP, this.defaultPORT, "", ref this.dtPrivilege) > 0)
				{
					if (this.dtPrivilege.Rows.Count >= 0)
					{
						this.dataGridView1.DataSource = this.dtPrivilege;
					}
					else
					{
						this.txtInfo.AppendText("failed\r\n");
					}
				}
				else
				{
					this.txtInfo.AppendText("failed\r\n");
				}
			}
			catch (Exception)
			{
			}
			wgMjControllerPrivilege.Dispose();
			Cursor.Current = Cursors.Default;
		}

		private void button69_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				uint operatorId = 0u;
				ulong operatorCardNO = 18446744073709551615uL;
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				if (!string.IsNullOrEmpty(this.textBox18.Text))
				{
					operatorId = uint.Parse(this.textBox18.Text);
				}
				if (!string.IsNullOrEmpty(this.textBox19.Text))
				{
					operatorCardNO = ulong.Parse(this.textBox19.Text);
				}
				if (sender == this.button69)
				{
					icController.RemoteOpenFoorIP(int.Parse(this.textBox31.Text), operatorId, operatorCardNO);
				}
				else
				{
					icController.RemoteOpenFoorIP(int.Parse(this.textBox31.Text) + 40, operatorId, operatorCardNO);
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button70_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				uint operatorId = 0u;
				ulong operatorCardNO = 18446744073709551615uL;
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				if (!string.IsNullOrEmpty(this.textBox18.Text))
				{
					operatorId = uint.Parse(this.textBox18.Text);
				}
				if (!string.IsNullOrEmpty(this.textBox19.Text))
				{
					operatorCardNO = ulong.Parse(this.textBox19.Text);
				}
				int num = 0;
				if (sender == this.button72)
				{
					num = 20;
				}
				int millisecondsTimeout = int.Parse(this.numericUpDown22.Value.ToString());
				for (int i = 1; i <= 20; i++)
				{
					if (this.checkBox130.Checked)
					{
						icController.RemoteOpenFoorIP(i + num, operatorId, operatorCardNO);
					}
					if (this.checkBox131.Checked)
					{
						icController.RemoteOpenFoorIP(i + 40 + num, operatorId, operatorCardNO);
					}
					Thread.Sleep(millisecondsTimeout);
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button71_Click(object sender, EventArgs e)
		{
			this.txt02e2.Visible = false;
			this.button71.Visible = false;
			this.label109.Visible = false;
			this.checkBox118.Visible = false;
			this.button52.Visible = false;
			this.checkBox113.Visible = false;
			this.checkBox115.Visible = false;
			this.checkBox116.Visible = false;
			this.checkBox117.Visible = false;
			this.checkBox135.Visible = false;
			this.button57.Visible = false;
		}

		private void button73_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				byte[] array = new byte[1152];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				int num;
				if (!this.checkBox133.Checked)
				{
					num = 0;
				}
				else
				{
					num = (int)this.numericUpDown23.Value;
				}
				int num2 = 96;
				array[num2] = (byte)(num & 255);
				byte[] expr_8D_cp_0 = array;
				int expr_8D_cp_1 = 1024 + (num2 >> 3);
				expr_8D_cp_0[expr_8D_cp_1] |= (byte)(1 << (num2 & 7));
				num2++;
				array[num2] = (byte)(num >> 8);
				byte[] expr_BE_cp_0 = array;
				int expr_BE_cp_1 = 1024 + (num2 >> 3);
				expr_BE_cp_0[expr_BE_cp_1] |= (byte)(1 << (num2 & 7));
				int num3;
				if (this.comboBox59.SelectedIndex == 0)
				{
					num3 = 8192;
				}
				else if (this.comboBox59.SelectedIndex == 1)
				{
					num3 = 12288;
				}
				else if (this.comboBox59.SelectedIndex == 2)
				{
					num3 = 229376;
				}
				else
				{
					num3 = 12288;
				}
				BitConverter.GetBytes(num3);
				num2 = 100;
				array[num2] = (byte)(num3 & 255);
				byte[] expr_14C_cp_0 = array;
				int expr_14C_cp_1 = 1024 + (num2 >> 3);
				expr_14C_cp_0[expr_14C_cp_1] |= (byte)(1 << (num2 & 7));
				num2++;
				array[num2] = (byte)(num3 >> 8);
				byte[] expr_17D_cp_0 = array;
				int expr_17D_cp_1 = 1024 + (num2 >> 3);
				expr_17D_cp_0[expr_17D_cp_1] |= (byte)(1 << (num2 & 7));
				num2++;
				array[num2] = (byte)(num3 >> 16);
				byte[] expr_1AF_cp_0 = array;
				int expr_1AF_cp_1 = 1024 + (num2 >> 3);
				expr_1AF_cp_0[expr_1AF_cp_1] |= (byte)(1 << (num2 & 7));
				num2++;
				array[num2] = (byte)(num3 >> 24);
				byte[] expr_1E1_cp_0 = array;
				int expr_1E1_cp_1 = 1024 + (num2 >> 3);
				expr_1E1_cp_0[expr_1E1_cp_1] |= (byte)(1 << (num2 & 7));
				icController.UpdateConfigureCPUSuperIP(array, "");
				WGPacketSSI_FLASH wGPacketSSI_FLASH = new WGPacketSSI_FLASH();
				wGPacketSSI_FLASH.type = 33;
				wGPacketSSI_FLASH.code = 48;
				wGPacketSSI_FLASH.iDevSnFrom = 0u;
				wGPacketSSI_FLASH.iDevSnTo = (uint)this.txtSN.Value;
				wGPacketSSI_FLASH.iCallReturn = 0;
				wGPacketSSI_FLASH.ucData = new byte[1024];
				wgUdpComm wgUdpComm = new wgUdpComm();
				try
				{
					Thread.Sleep(300);
					wGPacketSSI_FLASH.iStartFlashAddr = 8331264u;
					wGPacketSSI_FLASH.iEndFlashAddr = 8335359u;
					for (int j = 0; j < 1024; j++)
					{
						wGPacketSSI_FLASH.ucData[j] = 255;
					}
					byte[] array2 = null;
					while (wGPacketSSI_FLASH.iStartFlashAddr <= wGPacketSSI_FLASH.iEndFlashAddr)
					{
						for (int k = 0; k < 1024; k++)
						{
							wGPacketSSI_FLASH.ucData[k] = this.webStringOther[(int)(checked((IntPtr)(unchecked((ulong)(wGPacketSSI_FLASH.iStartFlashAddr - 8331264u) + (ulong)((long)k)))))];
						}
						int num4 = wgUdpComm.udp_get(wGPacketSSI_FLASH.ToBytes(wgUdpComm.udpPort), 300, wGPacketSSI_FLASH.xid, this.defaultIP, this.defaultPORT, ref array2);
						if (num4 < 0)
						{
							this.txtInfo.AppendText("button25 Err Ret=: " + num4.ToString() + "\r\n");
							break;
						}
						wGPacketSSI_FLASH.iStartFlashAddr += 1024u;
						this.label107.Text = (wGPacketSSI_FLASH.iStartFlashAddr / 1024u).ToString();
					}
					this.txtInfo.AppendText("button25 End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "\r\n");
				}
				catch (Exception)
				{
				}
				finally
				{
					wgUdpComm.Dispose();
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private string ConertStr(string str, string From, string To)
		{
			byte[] bytes = Encoding.GetEncoding(From).GetBytes(str);
			bytes = Encoding.Convert(Encoding.GetEncoding(From), Encoding.GetEncoding(To), bytes);
			return Encoding.GetEncoding(To).GetString(bytes);
		}

		private void button74_Click(object sender, EventArgs e)
		{
			MessageBox.Show("此功能停用, 由软件自身完成. 2012-3-28_14:36:20");
		}

		private void button76_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				this.openFileDialog1.Filter = " (*.xls)|*.xls| (*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				try
				{
					this.openFileDialog1.InitialDirectory = ".\\REPORT";
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
				}
				this.openFileDialog1.Title = sender.ToString();
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = this.openFileDialog1.FileName;
					this.MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source= " + fileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE'");
					this.DS = new DataSet();
					this.MyConnection.Open();
					this.MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					this.MyConnection.Close();
					string str = "public$";
					try
					{
						this.MyCommand = new OleDbDataAdapter("select * from [" + str + "A1:J65535]", this.MyConnection);
						this.MyCommand.Fill(this.DS, "webInfo");
						this.dataGridView2.DataSource = this.DS.Tables["webInfo"];
						this.dv = new DataView(this.DS.Tables["webInfo"]);
						int[] array = new int[this.dv.Count];
						int num = 8192;
						string text = "";
						string text2 = "";
						for (int i = 0; i <= this.dv.Count - 1; i++)
						{
							array[i] = num;
							text += string.Format("0x{0:X},", num);
							text2 += string.Format("#define {0} ((char const*)(webStringLoc[{1:d}]))\r\n", wgTools.SetObjToStr(this.dv[i][1]).Trim(), i);
							string s = wgTools.SetObjToStr(this.dv[i][2]).Trim();
							this.txtInfo.AppendText("0x");
							byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array2 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes);
							num = num + array2.Length + 1;
							this.txtInfo.AppendText(BitConverter.ToString(array2).Replace("-", ",0x"));
							this.txtInfo.AppendText(",0x00,");
							this.txtInfo.AppendText("\r\n");
						}
						this.txtInfo.AppendText("0x00");
						this.txtInfo.AppendText("\r\n");
						this.txtInfo.AppendText(text);
						this.txtInfo.AppendText("\r\n");
						this.txtInfo.AppendText(text2);
						string path = "D:\\works2011\\ARM\\StellarisWare6852\\boards\\N3000-9506WEB\\Project\\Debug\\Exe\\N3000.bin";
						string path2 = "D:\\works2011\\ARM\\StellarisWare6852\\boards\\N3000-9506WEB\\Project\\Debug\\Exe\\N3000new.bin";
						this.fsRd = new FileStream(path, FileMode.Open);
						BinaryReader binaryReader = new BinaryReader(this.fsRd);
						this.fs = new FileStream(path2, FileMode.Create);
						BinaryWriter binaryWriter = new BinaryWriter(this.fs);
						int j;
						for (j = 0; j < 8192; j++)
						{
							binaryWriter.Write(binaryReader.ReadByte());
						}
						num = 9216;
						for (int k = 0; k <= this.dv.Count - 1; k++)
						{
							binaryWriter.Write(BitConverter.GetBytes(num));
							j += 4;
							string s = wgTools.SetObjToStr(this.dv[k][2]).Trim();
							byte[] bytes2 = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array3 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes2);
							num = num + array3.Length + 1;
						}
						byte b = 0;
						while (j < 9216)
						{
							binaryWriter.Write(b);
							j++;
						}
						for (int l = 0; l <= this.dv.Count - 1; l++)
						{
							string s = wgTools.SetObjToStr(this.dv[l][2]).Trim();
							byte[] bytes3 = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array4 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes3);
							binaryWriter.Write(array4);
							binaryWriter.Write(b);
							j = j + array4.Length + 1;
						}
						binaryReader.BaseStream.Position = (long)j;
						while (binaryReader.BaseStream.Position < 12288L)
						{
							binaryWriter.Write(binaryReader.ReadByte());
						}
						j = 12288;
						num = 13312;
						for (int m = 0; m <= this.dv.Count - 1; m++)
						{
							binaryWriter.Write(BitConverter.GetBytes(num));
							j += 4;
							string s = wgTools.SetObjToStr(this.dv[m][3]).Trim();
							byte[] bytes4 = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array5 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes4);
							num = num + array5.Length + 1;
						}
						while (j < 13312)
						{
							binaryWriter.Write(b);
							j++;
						}
						for (int n = 0; n <= this.dv.Count - 1; n++)
						{
							string s = wgTools.SetObjToStr(this.dv[n][3]).Trim();
							byte[] bytes5 = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array6 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes5);
							binaryWriter.Write(array6);
							binaryWriter.Write(b);
							j = j + array6.Length + 1;
						}
						binaryReader.BaseStream.Position = (long)j;
						while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
						{
							binaryWriter.Write(binaryReader.ReadByte());
						}
						binaryReader.Close();
						binaryWriter.Close();
						this.fs.Dispose();
						this.fsRd.Dispose();
						for (int num2 = 0; num2 < this.webStringOther.Length; num2++)
						{
							this.webStringOther[num2] = b;
						}
						j = 229376;
						num = 230400;
						for (int num3 = 0; num3 <= this.dv.Count - 1; num3++)
						{
							this.webStringOther[j - 229376] = (byte)(num & 255);
							this.webStringOther[j - 229376 + 1] = (byte)(num >> 8);
							this.webStringOther[j - 229376 + 2] = (byte)(num >> 16);
							this.webStringOther[j - 229376 + 3] = (byte)(num >> 24);
							j += 4;
							string s = wgTools.SetObjToStr(this.dv[num3][3]).Trim();
							byte[] bytes6 = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array7 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes6);
							num = num + array7.Length + 1;
						}
						j = 230400;
						for (int num4 = 0; num4 <= this.dv.Count - 1; num4++)
						{
							string s = wgTools.SetObjToStr(this.dv[num4][3]).Trim();
							byte[] bytes7 = Encoding.GetEncoding("gb2312").GetBytes(s);
							byte[] array8 = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("utf-8"), bytes7);
							for (int num5 = 0; num5 < array8.Length; num5++)
							{
								this.webStringOther[j - 229376 + num5] = array8[num5];
							}
							j = j + array8.Length + 1;
						}
						this.webStringOther[1033] = 0;
						this.dv.Dispose();
						this.MyCommand.Dispose();
					}
					catch (Exception ex2)
					{
						wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
					}
					this.DS.Dispose();
					this.MyConnection.Dispose();
				}
			}
			catch (Exception ex3)
			{
				wgTools.WgDebugWrite(ex3.ToString(), new object[0]);
			}
			finally
			{
				Directory.SetCurrentDirectory(Application.StartupPath);
				Cursor.Current = Cursors.Default;
			}
		}

		private void button77_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				wgMjControllerSwipeRecord wgMjControllerSwipeRecord = null;
				icController.GetSingleSwipeRecord((int)this.numericUpDown24.Value, ref wgMjControllerSwipeRecord);
				if (wgMjControllerSwipeRecord != null)
				{
					this.txtInfo.AppendText(wgMjControllerSwipeRecord.ToDisplaySimpleInfo(true) + "\r\n");
				}
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button78_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege();
			try
			{
				wgMjControllerPrivilege.AllowDownload();
				if (this.dtPrivilege1 != null)
				{
					this.dtPrivilege1.Rows.Clear();
					this.dtPrivilege1.Dispose();
					this.dtPrivilege1 = null;
					GC.Collect();
				}
				this.dtPrivilege1 = new DataTable("Privilege");
				this.dtPrivilege1.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
				this.dtPrivilege1.Columns.Add("f_BeginYMD", Type.GetType("System.DateTime"));
				this.dtPrivilege1.Columns.Add("f_EndYMD", Type.GetType("System.DateTime"));
				this.dtPrivilege1.Columns.Add("f_PIN", Type.GetType("System.String"));
				this.dtPrivilege1.Columns.Add("f_ControlSegID1", Type.GetType("System.Byte"));
				this.dtPrivilege1.Columns["f_ControlSegID1"].DefaultValue = 0;
				this.dtPrivilege1.Columns.Add("f_ControlSegID2", Type.GetType("System.Byte"));
				this.dtPrivilege1.Columns["f_ControlSegID2"].DefaultValue = 0;
				this.dtPrivilege1.Columns.Add("f_ControlSegID3", Type.GetType("System.Byte"));
				this.dtPrivilege1.Columns["f_ControlSegID3"].DefaultValue = 0;
				this.dtPrivilege1.Columns.Add("f_ControlSegID4", Type.GetType("System.Byte"));
				this.dtPrivilege1.Columns["f_ControlSegID4"].DefaultValue = 0;
				this.dtPrivilege1.Columns.Add("f_ConsumerName", Type.GetType("System.String"));
				this.dtPrivilege1.Columns.Add("f_IsDeleted", Type.GetType("System.UInt32"));
				if (wgMjControllerPrivilege.DownloadIP(this.defaultSN, this.defaultIP, this.defaultPORT, "INCLUDEDELETED", ref this.dtPrivilege1) <= 0)
				{
					this.txtInfo.AppendText("failed\r\n");
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				wgMjControllerPrivilege.Dispose();
			}
			Cursor.Current = Cursors.Default;
			if (this.dtPrivilege1 != null && this.dtPrivilege1.Rows.Count >= 0)
			{
				this.dtPrivilege1.Columns.Remove("f_BeginYMD");
				this.dtPrivilege1.Columns.Remove("f_EndYMD");
				this.dtPrivilege1.Columns.Remove("f_PIN");
				this.dtPrivilege1.Columns.Remove("f_ControlSegID1");
				this.dtPrivilege1.Columns.Remove("f_ControlSegID2");
				this.dtPrivilege1.Columns.Remove("f_ControlSegID3");
				this.dtPrivilege1.Columns.Remove("f_ControlSegID4");
				this.dtPrivilege1.AcceptChanges();
				DataView dataSource = new DataView(this.dtPrivilege1);
				this.dataGridView3.DataSource = dataSource;
			}
		}

		private void button79_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcel(this.dataGridView3, this.Text);
		}

		private void txtControllerIP_TextChanged(object sender, EventArgs e)
		{
			if (this.checkBox120.Checked)
			{
				this.defaultIP = this.txtControllerIP.Text;
				this.defaultPORT = (int)this.nudPort.Value;
				return;
			}
			this.defaultIP = "";
			this.defaultPORT = 60000;
		}

		private void nudPort_ValueChanged(object sender, EventArgs e)
		{
			if (this.checkBox120.Checked)
			{
				this.defaultIP = this.txtControllerIP.Text;
				this.defaultPORT = (int)this.nudPort.Value;
				return;
			}
			this.defaultIP = "";
			this.defaultPORT = 60000;
		}

		private void button80_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.folderBrowserDialog1.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.textBox34.Text = this.folderBrowserDialog1.SelectedPath;
			}
		}

		private void getResult()
		{
			string text = this.textBox34.Text;
			string text2 = this.textBox35.Text;
			string text3 = this.textBox36.Text;
			text = text.TrimEnd(new char[]
			{
				'\\'
			}) + "\\";
			if (Directory.Exists(text))
			{
				DirectoryInfo dI = new DirectoryInfo(text);
				text3 = this.result(dI, text2, text3, text);
			}
			MessageBox.Show("结果为(用|分割)：" + text3);
		}

		private string result(DirectoryInfo DI, string hasValue, string value, string path)
		{
			DirectoryInfo[] directories = DI.GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo dI = directories[i];
				value += this.result(dI, hasValue, value, path);
			}
			ArrayList arrayList = new ArrayList();
			FileInfo[] files = DI.GetFiles();
			for (int j = 0; j < files.Length; j++)
			{
				FileInfo fileInfo = files[j];
				if (fileInfo.Name.Contains(hasValue))
				{
					arrayList.Add(fileInfo.FullName);
				}
			}
			foreach (string text in arrayList)
			{
				File.Move(text, text.Replace("\\" + hasValue, "\\" + value));
			}
			return "替换文件数:" + arrayList.Count.ToString();
		}

		private void button82_Click(object sender, EventArgs e)
		{
			this.getResult();
		}

		private void button83_Click(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			this.txtInfo.AppendText(now.ToString("hh:mm:ss.ffff") + "\r\n");
			icController icController = new icController();
			try
			{
				icController.ControllerSN = -1;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
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
				string text = string.Format("SN{3} : 已发送={0}, 已接收={1}, 丢失 = {2}", new object[]
				{
					num,
					num2,
					num3,
					icController.ControllerSN
				}) + "\r\n";
				this.txtInfo.AppendText(text);
				wgUdpComm.triesTotal = 0L;
				wgTools.WriteLine("control.Test1024 Start");
				int num4 = 0;
				string text2 = "";
				int num5 = icController.test1024Write();
				if (num5 < 0)
				{
					text2 += "大数据包写入失败\r\n";
				}
				num5 = icController.test1024Read(200u, ref num4);
				if (num5 < 0)
				{
					text2 = text2 + "大数据包读取失败: " + num5.ToString() + "\r\n";
				}
				if (wgUdpComm.triesTotal > 0L)
				{
					text2 = text2 + "测试中重试次数 = " + wgUdpComm.triesTotal.ToString() + "\r\n";
				}
				if (text2 == "")
				{
					text2 = "大数据包测试成功(测试200次)";
				}
				this.txtInfo.AppendText(icController.ControllerSN.ToString() + ": " + text2 + "\r\n");
			}
			catch (Exception)
			{
			}
			icController.Dispose();
			DateTime now2 = DateTime.Now;
			this.txtInfo.AppendText(string.Format("{0} [{1}秒] \r\n", now2.ToString("hh:mm:ss.ffff"), (now2 - now).TotalSeconds));
		}

		private void button84_Click(object sender, EventArgs e)
		{
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.WarnResetIP();
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button85_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			using (wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege())
			{
				try
				{
					wgMjControllerPrivilege.AllowDownload();
					if (this.dtPrivilege != null)
					{
						this.dtPrivilege.Rows.Clear();
						this.dtPrivilege.Dispose();
						this.dtPrivilege = null;
						GC.Collect();
					}
					this.dtPrivilege = new DataTable("Privilege");
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
					this.txtInfo.AppendText("开始读取");
					string arg = DateTime.Now.ToString("yyyy-MM-dd hhmmssffff");
					for (int i = 0; i < 10; i++)
					{
						this.dtPrivilege.Clear();
						if (wgMjControllerPrivilege.DownloadIP(this.defaultSN, this.defaultIP, this.defaultPORT, "", ref this.dtPrivilege) > 0)
						{
							this.dtPrivilege.WriteXml(string.Format("logtemp\\{0}_{1}.xml", arg, i));
							if (this.dtPrivilege.Rows.Count >= 0)
							{
								this.dataGridView1.DataSource = this.dtPrivilege;
							}
							else
							{
								this.txtInfo.AppendText("failed\r\n");
							}
						}
						else
						{
							this.txtInfo.AppendText("failed\r\n");
						}
					}
					this.txtInfo.AppendText("读取完成, 开始比较");
					for (int j = 0; j < 9; j++)
					{
						if (!this.FileCompare(string.Format("logtemp\\{0}_{1}.xml", arg, j), string.Format("logtemp\\{0}_{1}.xml", arg, j + 1)))
						{
							this.txtInfo.AppendText("比较有错的第一个文件:  " + string.Format("logtemp\\{0}_{1}.xml", arg, j));
						}
					}
					this.txtInfo.AppendText("比较完成");
				}
				catch (Exception)
				{
					this.txtInfo.AppendText("读取出错");
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private bool FileCompare(string file1, string file2)
		{
			int num = 0;
			int num2 = 0;
			if (file1 == file2)
			{
				return true;
			}
			using (FileStream fileStream = new FileStream(file1, FileMode.Open))
			{
				using (FileStream fileStream2 = new FileStream(file2, FileMode.Open))
				{
					if (fileStream.Length != fileStream2.Length)
					{
						return false;
					}
					do
					{
						num = fileStream.ReadByte();
						num2 = fileStream2.ReadByte();
						if (num != num2)
						{
							break;
						}
					}
					while (num != -1);
				}
			}
			return num - num2 == 0;
		}

		private void button86_Click(object sender, EventArgs e)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this.textBox34.Text);
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fi = files[i];
				frmTestController.Compress(fi);
			}
		}

		private static void CopyStream(Stream input, Stream output)
		{
			byte[] array = new byte[32768];
			while (true)
			{
				int num = input.Read(array, 0, array.Length);
				if (num <= 0)
				{
					break;
				}
				output.Write(array, 0, num);
			}
		}

		public static void Compress(FileInfo fi)
		{
			using (FileStream fileStream = fi.OpenRead())
			{
				if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
				{
					using (FileStream fileStream2 = File.Create(fi.FullName + ".gz"))
					{
						using (GZipStream gZipStream = new GZipStream(fileStream2, CompressionMode.Compress))
						{
							frmTestController.CopyStream(fileStream, gZipStream);
						}
					}
				}
			}
		}

		public static void Decompress(FileInfo fi)
		{
			using (FileStream fileStream = fi.OpenRead())
			{
				string fullName = fi.FullName;
				string path = fullName.Remove(fullName.Length - fi.Extension.Length);
				using (FileStream fileStream2 = File.Create(path))
				{
					using (GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Decompress))
					{
						frmTestController.CopyStream(gZipStream, fileStream2);
					}
				}
			}
		}

		private void button87_Click(object sender, EventArgs e)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this.textBox34.Text);
			FileInfo[] files = directoryInfo.GetFiles("*.gz");
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fi = files[i];
				frmTestController.Decompress(fi);
			}
		}

		private void button88_Click(object sender, EventArgs e)
		{
			IPAddress iPAddress = IPAddress.Parse("192.168.168.153");
			IPAddress iPAddress2 = IPAddress.Parse("255.255.255.0");
			IPAddress iPAddress3 = IPAddress.Parse("192.168.168.254");
			int ipstart = 153;
			int ipend = 253;
			int cmdOption = 18;
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.AutoIPSetIP(cmdOption, iPAddress.ToString(), iPAddress2.ToString(), iPAddress3.ToString(), ipstart, ipend);
			}
			catch (Exception)
			{
			}
			icController.Dispose();
		}

		private void button89_Click(object sender, EventArgs e)
		{
			WGPacketSSI_FLASH_QUERY wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY();
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				Thread.Sleep(300);
				int num = (int)this.numericUpDown27.Value;
				int num2 = 0;
				while (++num2 <= num)
				{
					wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)this.txtSN.Value, (uint)(this.numericUpDown25.Value * 1024m), (uint)(--(this.numericUpDown25.Value * 1024m + 1024m)));
					byte[] cmd = wGPacketSSI_FLASH_QUERY.ToBytes(wgUdpComm.udpPort);
					byte[] array = null;
					int num3 = wgUdpComm.udp_get(cmd, 300, wGPacketSSI_FLASH_QUERY.xid, this.defaultIP, this.defaultPORT, ref array);
					this.txtInfo.AppendText(num2 + "  ----");
					if (num3 < 0)
					{
						this.txtInfo.AppendText(this.numericUpDown25.Value + "  failed????????????????");
						this.txtInfo.AppendText("\r\n");
						wgUdpComm.Dispose();
						return;
					}
					if (array != null)
					{
						BitConverter.ToString(array);
						this.txtInfo.AppendText(this.numericUpDown25.Value + "  OK");
						this.txtInfo.AppendText("\r\n");
					}
					wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)this.txtSN.Value, (uint)(this.numericUpDown26.Value * 1024m), (uint)(--(this.numericUpDown26.Value * 1024m + 1024m)));
					cmd = wGPacketSSI_FLASH_QUERY.ToBytes(wgUdpComm.udpPort);
					array = null;
					num3 = wgUdpComm.udp_get(cmd, 300, wGPacketSSI_FLASH_QUERY.xid, this.defaultIP, this.defaultPORT, ref array);
					this.txtInfo.AppendText(num2 + "  ----");
					if (num3 < 0)
					{
						this.txtInfo.AppendText(this.numericUpDown26.Value + "  failed????????????????");
						wgUdpComm.Dispose();
						return;
					}
					if (array != null)
					{
						BitConverter.ToString(array);
						this.txtInfo.AppendText(this.numericUpDown26.Value + "  OK");
						this.txtInfo.AppendText("\r\n");
					}
				}
				wgTools.WriteLine(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")));
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button90_Click(object sender, EventArgs e)
		{
			byte[] array = new byte[1152];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			ulong num = 18446744073709551615uL;
			ulong num2 = 18446744073709551615uL;
			ulong.TryParse(this.textBox37.Text, out num);
			ulong.TryParse(this.textBox38.Text, out num2);
			if (num == 0uL)
			{
				num = 18446744073709551615uL;
			}
			if (num2 == 0uL)
			{
				num2 = 18446744073709551615uL;
			}
			int num3 = 160;
			array[num3] = (byte)(num & 255uL);
			byte[] expr_7D_cp_0 = array;
			int expr_7D_cp_1 = 1024 + (num3 >> 3);
			expr_7D_cp_0[expr_7D_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 8);
			byte[] expr_AD_cp_0 = array;
			int expr_AD_cp_1 = 1024 + (num3 >> 3);
			expr_AD_cp_0[expr_AD_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 16);
			byte[] expr_DE_cp_0 = array;
			int expr_DE_cp_1 = 1024 + (num3 >> 3);
			expr_DE_cp_0[expr_DE_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 24);
			byte[] expr_10F_cp_0 = array;
			int expr_10F_cp_1 = 1024 + (num3 >> 3);
			expr_10F_cp_0[expr_10F_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 32);
			byte[] expr_140_cp_0 = array;
			int expr_140_cp_1 = 1024 + (num3 >> 3);
			expr_140_cp_0[expr_140_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 40);
			byte[] expr_171_cp_0 = array;
			int expr_171_cp_1 = 1024 + (num3 >> 3);
			expr_171_cp_0[expr_171_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 48);
			byte[] expr_1A2_cp_0 = array;
			int expr_1A2_cp_1 = 1024 + (num3 >> 3);
			expr_1A2_cp_0[expr_1A2_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num >> 56);
			byte[] expr_1D3_cp_0 = array;
			int expr_1D3_cp_1 = 1024 + (num3 >> 3);
			expr_1D3_cp_0[expr_1D3_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 & 255uL);
			byte[] expr_209_cp_0 = array;
			int expr_209_cp_1 = 1024 + (num3 >> 3);
			expr_209_cp_0[expr_209_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 8);
			byte[] expr_23A_cp_0 = array;
			int expr_23A_cp_1 = 1024 + (num3 >> 3);
			expr_23A_cp_0[expr_23A_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 16);
			byte[] expr_26C_cp_0 = array;
			int expr_26C_cp_1 = 1024 + (num3 >> 3);
			expr_26C_cp_0[expr_26C_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 24);
			byte[] expr_29E_cp_0 = array;
			int expr_29E_cp_1 = 1024 + (num3 >> 3);
			expr_29E_cp_0[expr_29E_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 32);
			byte[] expr_2D0_cp_0 = array;
			int expr_2D0_cp_1 = 1024 + (num3 >> 3);
			expr_2D0_cp_0[expr_2D0_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 40);
			byte[] expr_302_cp_0 = array;
			int expr_302_cp_1 = 1024 + (num3 >> 3);
			expr_302_cp_0[expr_302_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 48);
			byte[] expr_334_cp_0 = array;
			int expr_334_cp_1 = 1024 + (num3 >> 3);
			expr_334_cp_0[expr_334_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			array[num3] = (byte)(num2 >> 56);
			byte[] expr_366_cp_0 = array;
			int expr_366_cp_1 = 1024 + (num3 >> 3);
			expr_366_cp_0[expr_366_cp_1] |= (byte)(1 << (num3 & 7));
			num3++;
			icController icController = new icController();
			try
			{
				icController.ControllerSN = this.defaultSN;
				icController.IP = this.defaultIP;
				icController.PORT = this.defaultPORT;
				icController.UpdateConfigureCPUSuperIP(array, "");
			}
			catch (Exception)
			{
			}
			icController.Dispose();
			this.txtInfo.AppendText((sender as Button).Text + "  OK");
			this.txtInfo.AppendText("\r\n");
		}

		private void frmTestController_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
			wgAppConfig.UpdateKeyVal("PRODUCT_SPECIAL_CARDS", this.textBox32.Text);
		}

		private void numericUpDown28_ValueChanged(object sender, EventArgs e)
		{
		}

		private void button91_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				this.openFileDialog1.Filter = " (*.bin)|*.bin| (*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				try
				{
					this.openFileDialog1.InitialDirectory = ".\\REPORT";
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
				}
				this.openFileDialog1.Title = sender.ToString();
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = this.openFileDialog1.FileName;
					int num = int.Parse(this.comboBox60.Text);
					string path = fileName;
					string path2 = fileName + ".Txt";
					this.fsRd = new FileStream(path, FileMode.Open);
					BinaryReader binaryReader = new BinaryReader(this.fsRd);
					this.fs = new FileStream(path2, FileMode.Create);
					StreamWriter streamWriter = new StreamWriter(this.fs);
					int num2 = 0;
					int num3 = 0;
					long num4 = 0L;
					int num5 = 0;
					streamWriter.Write("#ifndef WEB_PAGE_0x2000 \r\n#define WEB_PAGE_0x2000 \r\n#pragma location = 0x2000\r\nconst  int chsStringLoc[]= { //实际数据 0x2400开始\r\n");
					while ((long)num2 < binaryReader.BaseStream.Length / 2L)
					{
						num4 += (long)((long)binaryReader.ReadByte() << 8 * num3);
						num3++;
						if (num3 == num)
						{
							if (num5 > 0)
							{
								streamWriter.Write(", ");
								if (num5 % 8 == 0)
								{
									streamWriter.Write("\r\n");
								}
							}
							string text = string.Format("0x{0:X2}", num4);
							if (text.Length > 10)
							{
								text = text.Replace("FFFFFFFF", "");
							}
							streamWriter.Write(text);
							num5++;
							num4 = 0L;
							num3 = 0;
						}
						num2++;
					}
					streamWriter.Write("};\r\n\r\n#pragma location = 0x3000\r\nconst  int enStringLoc[]= { //实际数据 0x3400开始\r\n");
					num5 = 0;
					while ((long)num2 < binaryReader.BaseStream.Length)
					{
						num4 += (long)((long)binaryReader.ReadByte() << 8 * num3);
						num3++;
						if (num3 == num)
						{
							if (num5 > 0)
							{
								streamWriter.Write(", ");
								if (num5 % 8 == 0)
								{
									streamWriter.Write("\r\n");
								}
							}
							string text = string.Format("0x{0:X2}", num4);
							if (text.Length > 10)
							{
								text = text.Replace("FFFFFFFF", "");
							}
							streamWriter.Write(text);
							num5++;
							num3 = 0;
							num4 = 0L;
						}
						num2++;
					}
					streamWriter.Write("};\r\n\r\n#endif\r\n");
					binaryReader.Close();
					streamWriter.Close();
					this.fs.Dispose();
					this.fsRd.Dispose();
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			finally
			{
				Directory.SetCurrentDirectory(Application.StartupPath);
				Cursor.Current = Cursors.Default;
			}
		}

		private void checkBox139_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void comboBox60_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button92_Click(object sender, EventArgs e)
		{
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				byte[] array = new byte[]
				{
					25,
					80,
					0,
					0,
					177,
					152,
					167,
					25,
					1,
					0,
					112,
					0,
					32,
					16,
					1,
					1,
					32,
					41,
					18,
					49,
					1,
					1,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				};
				byte[] array2 = null;
				Array.Copy(BitConverter.GetBytes(this.defaultSN), 0, array, 4, 4);
				DateTime now = DateTime.Now;
				DateTime now2 = DateTime.Now;
				int num = 999999;
				this.txtInfo.AppendText(string.Format("\r\n开始发出第一个:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				for (int i = 0; i < 10000; i++)
				{
					now = DateTime.Now;
					Array.Copy(BitConverter.GetBytes(num), 0, array, 8, 4);
					num--;
					wgUdpComm.udp_get(array, 300, 0u, this.defaultIP, this.defaultPORT, ref array2);
					if (array2 != null)
					{
						now2 = DateTime.Now;
					}
				}
				this.txtInfo.AppendText(string.Format("\r\n开始发出:\t{0}", now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				this.txtInfo.AppendText(string.Format("\r\n接收时间:\t{0}", now2.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button93_Click(object sender, EventArgs e)
		{
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				byte[] array = new byte[]
				{
					25,
					84,
					0,
					0,
					177,
					152,
					167,
					25,
					85,
					170,
					170,
					85,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				};
				byte[] array2 = null;
				Array.Copy(BitConverter.GetBytes(this.defaultSN), 0, array, 4, 4);
				wgUdpComm.udp_get(array, 300, 0u, this.defaultIP, this.defaultPORT, ref array2);
				this.txtInfo.AppendText(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				this.Refresh();
				if (array2 != null)
				{
					string text = BitConverter.ToString(array2);
					this.txtInfo.AppendText(text);
					this.txtInfo.AppendText("\r\n");
					this.txtInfo.AppendText(string.Format("\r\n接收时间:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				}
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button94_Click(object sender, EventArgs e)
		{
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				byte[] array = new byte[]
				{
					25,
					64,
					0,
					0,
					177,
					152,
					167,
					25,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				};
				byte[] array2 = null;
				this.txtInfo.AppendText(string.Format("\r\n远程开门:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				Array.Copy(BitConverter.GetBytes(this.defaultSN), 0, array, 4, 4);
				wgUdpComm.udp_get(array, 300, 0u, this.defaultIP, this.defaultPORT, ref array2);
				this.txtInfo.AppendText(string.Format("\r\n开始发出:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				this.Refresh();
				if (array2 != null)
				{
					string text = BitConverter.ToString(array2);
					this.txtInfo.AppendText(text);
					this.txtInfo.AppendText("\r\n");
					this.txtInfo.AppendText(string.Format("\r\n接收时间:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				}
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		private void button95_Click(object sender, EventArgs e)
		{
			wgUdpComm wgUdpComm = new wgUdpComm();
			try
			{
				byte[] array = new byte[]
				{
					25,
					86,
					0,
					0,
					177,
					152,
					167,
					25,
					1,
					0,
					112,
					0,
					32,
					16,
					1,
					1,
					32,
					41,
					18,
					49,
					1,
					1,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				};
				byte[] array2 = null;
				Array.Copy(BitConverter.GetBytes(this.defaultSN), 0, array, 4, 4);
				DateTime now = DateTime.Now;
				DateTime now2 = DateTime.Now;
				int num = 999999;
				this.txtInfo.AppendText(string.Format("\r\n开始发出第一个:\t{0}", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				for (int i = 0; i < 10000; i++)
				{
					now = DateTime.Now;
					Array.Copy(BitConverter.GetBytes(num), 0, array, 8, 4);
					num--;
					wgUdpComm.udp_get(array, 300, 0u, this.defaultIP, this.defaultPORT, ref array2);
					if (array2 != null)
					{
						now2 = DateTime.Now;
					}
				}
				this.txtInfo.AppendText(string.Format("\r\n开始发出:\t{0}", now.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
				this.txtInfo.AppendText(string.Format("\r\n接收时间:\t{0}", now2.ToString("yyyy年MM月dd日HH时mm分ss秒ffff")));
			}
			catch (Exception)
			{
			}
			wgUdpComm.Dispose();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.control4Tcp != null)
			{
				this.control4Tcp.Dispose();
			}
			if (disposing && this.control4Udp != null)
			{
				this.control4Udp.Dispose();
			}
			if (disposing && this.fs != null)
			{
				this.fs.Dispose();
			}
			if (disposing && this.fsRd != null)
			{
				this.fsRd.Dispose();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmTestController));
			this.tabControl1 = new TabControl();
			this.tabPage7 = new TabPage();
			this.label148 = new Label();
			this.label149 = new Label();
			this.label150 = new Label();
			this.textBox27 = new TextBox();
			this.textBox28 = new TextBox();
			this.textBox33 = new TextBox();
			this.checkBox134 = new CheckBox();
			this.checkBox109 = new CheckBox();
			this.checkBox108 = new CheckBox();
			this.checkBox107 = new CheckBox();
			this.checkBox106 = new CheckBox();
			this.label67 = new Label();
			this.textBox17 = new TextBox();
			this.checkBox70 = new CheckBox();
			this.checkBox69 = new CheckBox();
			this.checkBox68 = new CheckBox();
			this.checkBox67 = new CheckBox();
			this.checkBox66 = new CheckBox();
			this.checkBox65 = new CheckBox();
			this.checkBox64 = new CheckBox();
			this.checkBox63 = new CheckBox();
			this.checkBox62 = new CheckBox();
			this.checkBox61 = new CheckBox();
			this.checkBox60 = new CheckBox();
			this.textBox13 = new TextBox();
			this.textBox14 = new TextBox();
			this.textBox15 = new TextBox();
			this.textBox16 = new TextBox();
			this.textBox9 = new TextBox();
			this.textBox10 = new TextBox();
			this.textBox11 = new TextBox();
			this.textBox12 = new TextBox();
			this.textBox5 = new TextBox();
			this.textBox6 = new TextBox();
			this.textBox7 = new TextBox();
			this.textBox8 = new TextBox();
			this.label66 = new Label();
			this.textBox4 = new TextBox();
			this.label65 = new Label();
			this.textBox3 = new TextBox();
			this.label64 = new Label();
			this.textBox2 = new TextBox();
			this.label63 = new Label();
			this.textBox1 = new TextBox();
			this.label59 = new Label();
			this.label60 = new Label();
			this.label61 = new Label();
			this.label62 = new Label();
			this.checkBox59 = new CheckBox();
			this.comboBox56 = new ComboBox();
			this.comboBox55 = new ComboBox();
			this.comboBox54 = new ComboBox();
			this.comboBox53 = new ComboBox();
			this.label55 = new Label();
			this.label56 = new Label();
			this.label57 = new Label();
			this.label58 = new Label();
			this.checkBox58 = new CheckBox();
			this.tabPage1 = new TabPage();
			this.groupBox14 = new GroupBox();
			this.radioButton27 = new RadioButton();
			this.radioButton28 = new RadioButton();
			this.checkBox126 = new CheckBox();
			this.numericUpDown19 = new NumericUpDown();
			this.label127 = new Label();
			this.checkBox124 = new CheckBox();
			this.checkBox125 = new CheckBox();
			this.checkBox122 = new CheckBox();
			this.checkBox123 = new CheckBox();
			this.checkBox121 = new CheckBox();
			this.numericUpDown18 = new NumericUpDown();
			this.label126 = new Label();
			this.checkBox119 = new CheckBox();
			this.checkBox110 = new CheckBox();
			this.numericUpDown12 = new NumericUpDown();
			this.label96 = new Label();
			this.numericUpDown16 = new NumericUpDown();
			this.numericUpDown11 = new NumericUpDown();
			this.label122 = new Label();
			this.label95 = new Label();
			this.groupBox4 = new GroupBox();
			this.radioButton8 = new RadioButton();
			this.radioButton9 = new RadioButton();
			this.checkBox57 = new CheckBox();
			this.checkBox56 = new CheckBox();
			this.checkBox55 = new CheckBox();
			this.checkBox54 = new CheckBox();
			this.checkBox53 = new CheckBox();
			this.checkBox52 = new CheckBox();
			this.comboBox52 = new ComboBox();
			this.comboBox51 = new ComboBox();
			this.comboBox50 = new ComboBox();
			this.comboBox49 = new ComboBox();
			this.label51 = new Label();
			this.label52 = new Label();
			this.label53 = new Label();
			this.label54 = new Label();
			this.label50 = new Label();
			this.checkBox51 = new CheckBox();
			this.groupBox3 = new GroupBox();
			this.radioButton6 = new RadioButton();
			this.radioButton7 = new RadioButton();
			this.checkBox50 = new CheckBox();
			this.checkBox23 = new CheckBox();
			this.checkBox22 = new CheckBox();
			this.checkBox21 = new CheckBox();
			this.groupBox1 = new GroupBox();
			this.radioButton5 = new RadioButton();
			this.radioButton4 = new RadioButton();
			this.radioButton3 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label7 = new Label();
			this.checkBox19 = new CheckBox();
			this.checkBox20 = new CheckBox();
			this.checkBox18 = new CheckBox();
			this.checkBox16 = new CheckBox();
			this.checkBox17 = new CheckBox();
			this.checkBox12 = new CheckBox();
			this.label6 = new Label();
			this.checkBox15 = new CheckBox();
			this.label5 = new Label();
			this.label4 = new Label();
			this.checkBox11 = new CheckBox();
			this.checkBox14 = new CheckBox();
			this.checkBox5 = new CheckBox();
			this.checkBox9 = new CheckBox();
			this.checkBox7 = new CheckBox();
			this.checkBox13 = new CheckBox();
			this.checkBox10 = new CheckBox();
			this.checkBox6 = new CheckBox();
			this.checkBox8 = new CheckBox();
			this.label8 = new Label();
			this.label9 = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.checkBox4 = new CheckBox();
			this.checkBox3 = new CheckBox();
			this.checkBox2 = new CheckBox();
			this.checkBox1 = new CheckBox();
			this.tabPage24 = new TabPage();
			this.groupBox19 = new GroupBox();
			this.numericUpDown32 = new NumericUpDown();
			this.radioButton38 = new RadioButton();
			this.radioButton39 = new RadioButton();
			this.checkBox138 = new CheckBox();
			this.groupBox17 = new GroupBox();
			this.numericUpDown30 = new NumericUpDown();
			this.radioButton34 = new RadioButton();
			this.radioButton35 = new RadioButton();
			this.groupBox18 = new GroupBox();
			this.label159 = new Label();
			this.radioButton36 = new RadioButton();
			this.radioButton37 = new RadioButton();
			this.numericUpDown31 = new NumericUpDown();
			this.checkBox137 = new CheckBox();
			this.groupBox16 = new GroupBox();
			this.numericUpDown29 = new NumericUpDown();
			this.radioButton32 = new RadioButton();
			this.radioButton33 = new RadioButton();
			this.groupBox15 = new GroupBox();
			this.label161 = new Label();
			this.radioButton31 = new RadioButton();
			this.radioButton30 = new RadioButton();
			this.radioButton29 = new RadioButton();
			this.numericUpDown28 = new NumericUpDown();
			this.checkBox136 = new CheckBox();
			this.tabPage2 = new TabPage();
			this.button85 = new Button();
			this.checkBox171 = new CheckBox();
			this.checkBox172 = new CheckBox();
			this.checkBox173 = new CheckBox();
			this.checkBox174 = new CheckBox();
			this.checkBox175 = new CheckBox();
			this.checkBox176 = new CheckBox();
			this.checkBox177 = new CheckBox();
			this.checkBox178 = new CheckBox();
			this.checkBox179 = new CheckBox();
			this.checkBox180 = new CheckBox();
			this.checkBox161 = new CheckBox();
			this.checkBox162 = new CheckBox();
			this.checkBox163 = new CheckBox();
			this.checkBox164 = new CheckBox();
			this.checkBox165 = new CheckBox();
			this.checkBox166 = new CheckBox();
			this.checkBox167 = new CheckBox();
			this.checkBox168 = new CheckBox();
			this.checkBox169 = new CheckBox();
			this.checkBox170 = new CheckBox();
			this.checkBox151 = new CheckBox();
			this.checkBox152 = new CheckBox();
			this.checkBox153 = new CheckBox();
			this.checkBox154 = new CheckBox();
			this.checkBox155 = new CheckBox();
			this.checkBox156 = new CheckBox();
			this.checkBox157 = new CheckBox();
			this.checkBox158 = new CheckBox();
			this.checkBox159 = new CheckBox();
			this.checkBox160 = new CheckBox();
			this.checkBox150 = new CheckBox();
			this.checkBox149 = new CheckBox();
			this.checkBox148 = new CheckBox();
			this.checkBox147 = new CheckBox();
			this.checkBox146 = new CheckBox();
			this.checkBox145 = new CheckBox();
			this.checkBox144 = new CheckBox();
			this.checkBox143 = new CheckBox();
			this.checkBox142 = new CheckBox();
			this.checkBox141 = new CheckBox();
			this.checkBox129 = new CheckBox();
			this.dataGridView1 = new DataGridView();
			this.button68 = new Button();
			this.checkBox128 = new CheckBox();
			this.label131 = new Label();
			this.groupBox12 = new GroupBox();
			this.radioButton26 = new RadioButton();
			this.radioButton24 = new RadioButton();
			this.numericUpDown6 = new NumericUpDown();
			this.label80 = new Label();
			this.dateTimePicker14 = new DateTimePicker();
			this.numericUpDown15 = new NumericUpDown();
			this.checkBox111 = new CheckBox();
			this.button53 = new Button();
			this.button17 = new Button();
			this.checkBox97 = new CheckBox();
			this.button4 = new Button();
			this.dtpDeactivate = new DateTimePicker();
			this.dtpActivate = new DateTimePicker();
			this.label121 = new Label();
			this.label23 = new Label();
			this.label22 = new Label();
			this.txtPassword = new TextBox();
			this.label21 = new Label();
			this.checkBox25 = new CheckBox();
			this.checkBox24 = new CheckBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.comboBox8 = new ComboBox();
			this.comboBox7 = new ComboBox();
			this.comboBox6 = new ComboBox();
			this.comboBox5 = new ComboBox();
			this.label20 = new Label();
			this.comboBox4 = new ComboBox();
			this.comboBox3 = new ComboBox();
			this.comboBox2 = new ComboBox();
			this.label18 = new Label();
			this.label13 = new Label();
			this.checkBox26 = new CheckBox();
			this.checkBox27 = new CheckBox();
			this.checkBox29 = new CheckBox();
			this.checkBox28 = new CheckBox();
			this.label14 = new Label();
			this.label15 = new Label();
			this.label16 = new Label();
			this.label17 = new Label();
			this.label19 = new Label();
			this.comboBox1 = new ComboBox();
			this.label12 = new Label();
			this.txtCardNO = new TextBox();
			this.tabPage3 = new TabPage();
			this.label42 = new Label();
			this.comboBox48 = new ComboBox();
			this.comboBox47 = new ComboBox();
			this.comboBox46 = new ComboBox();
			this.checkBox39 = new CheckBox();
			this.checkBox40 = new CheckBox();
			this.checkBox41 = new CheckBox();
			this.checkBox42 = new CheckBox();
			this.checkBox38 = new CheckBox();
			this.checkBox37 = new CheckBox();
			this.label41 = new Label();
			this.label40 = new Label();
			this.label24 = new Label();
			this.comboBox45 = new ComboBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label27 = new Label();
			this.label28 = new Label();
			this.label29 = new Label();
			this.label30 = new Label();
			this.label31 = new Label();
			this.checkBox31 = new CheckBox();
			this.checkBox32 = new CheckBox();
			this.checkBox33 = new CheckBox();
			this.checkBox34 = new CheckBox();
			this.comboBox11 = new ComboBox();
			this.comboBox12 = new ComboBox();
			this.label25 = new Label();
			this.comboBox10 = new ComboBox();
			this.comboBox9 = new ComboBox();
			this.label34 = new Label();
			this.checkBox36 = new CheckBox();
			this.label26 = new Label();
			this.comboBox25 = new ComboBox();
			this.comboBox32 = new ComboBox();
			this.checkBox35 = new CheckBox();
			this.comboBox26 = new ComboBox();
			this.comboBox31 = new ComboBox();
			this.label39 = new Label();
			this.comboBox27 = new ComboBox();
			this.comboBox30 = new ComboBox();
			this.label38 = new Label();
			this.comboBox13 = new ComboBox();
			this.comboBox16 = new ComboBox();
			this.comboBox28 = new ComboBox();
			this.comboBox15 = new ComboBox();
			this.comboBox29 = new ComboBox();
			this.comboBox14 = new ComboBox();
			this.comboBox41 = new ComboBox();
			this.label33 = new Label();
			this.label35 = new Label();
			this.comboBox42 = new ComboBox();
			this.comboBox21 = new ComboBox();
			this.comboBox36 = new ComboBox();
			this.comboBox43 = new ComboBox();
			this.comboBox22 = new ComboBox();
			this.comboBox44 = new ComboBox();
			this.comboBox35 = new ComboBox();
			this.comboBox23 = new ComboBox();
			this.label37 = new Label();
			this.comboBox34 = new ComboBox();
			this.checkBox30 = new CheckBox();
			this.comboBox24 = new ComboBox();
			this.comboBox37 = new ComboBox();
			this.comboBox33 = new ComboBox();
			this.comboBox20 = new ComboBox();
			this.label32 = new Label();
			this.comboBox38 = new ComboBox();
			this.label36 = new Label();
			this.comboBox19 = new ComboBox();
			this.comboBox17 = new ComboBox();
			this.comboBox39 = new ComboBox();
			this.comboBox40 = new ComboBox();
			this.comboBox18 = new ComboBox();
			this.tabPage4 = new TabPage();
			this.dgvControlConfure = new DataGridView();
			this.f_Type = new DataGridViewTextBoxColumn();
			this.f_Loc = new DataGridViewTextBoxColumn();
			this.f_Value = new DataGridViewTextBoxColumn();
			this.f_DefaultValue = new DataGridViewTextBoxColumn();
			this.f_Desc = new DataGridViewTextBoxColumn();
			this.tabPage5 = new TabPage();
			this.button12 = new Button();
			this.button11 = new Button();
			this.button10 = new Button();
			this.button9 = new Button();
			this.listBox1 = new ListBox();
			this.button8 = new Button();
			this.button7 = new Button();
			this.button6 = new Button();
			this.button5 = new Button();
			this.label47 = new Label();
			this.numericUpDown2 = new NumericUpDown();
			this.label46 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.groupBox2 = new GroupBox();
			this.checkBox49 = new CheckBox();
			this.checkBox48 = new CheckBox();
			this.checkBox47 = new CheckBox();
			this.checkBox46 = new CheckBox();
			this.checkBox45 = new CheckBox();
			this.checkBox44 = new CheckBox();
			this.checkBox43 = new CheckBox();
			this.label45 = new Label();
			this.dateTimePicker3 = new DateTimePicker();
			this.label43 = new Label();
			this.label44 = new Label();
			this.dateTimePicker2 = new DateTimePicker();
			this.dateTimePicker1 = new DateTimePicker();
			this.tabPage6 = new TabPage();
			this.button15 = new Button();
			this.label49 = new Label();
			this.label48 = new Label();
			this.dateTimePicker5 = new DateTimePicker();
			this.dateTimePicker4 = new DateTimePicker();
			this.button14 = new Button();
			this.button13 = new Button();
			this.tabPage8 = new TabPage();
			this.button84 = new Button();
			this.groupBox9 = new GroupBox();
			this.checkBox96 = new CheckBox();
			this.checkBox95 = new CheckBox();
			this.checkBox92 = new CheckBox();
			this.checkBox94 = new CheckBox();
			this.checkBox93 = new CheckBox();
			this.label76 = new Label();
			this.checkBox91 = new CheckBox();
			this.numericUpDown5 = new NumericUpDown();
			this.label73 = new Label();
			this.label72 = new Label();
			this.numericUpDown4 = new NumericUpDown();
			this.label75 = new Label();
			this.label74 = new Label();
			this.label71 = new Label();
			this.label70 = new Label();
			this.numericUpDown3 = new NumericUpDown();
			this.groupBox8 = new GroupBox();
			this.radioButton19 = new RadioButton();
			this.radioButton20 = new RadioButton();
			this.radioButton21 = new RadioButton();
			this.radioButton22 = new RadioButton();
			this.groupBox7 = new GroupBox();
			this.radioButton23 = new RadioButton();
			this.checkBox83 = new CheckBox();
			this.checkBox82 = new CheckBox();
			this.checkBox81 = new CheckBox();
			this.checkBox80 = new CheckBox();
			this.checkBox79 = new CheckBox();
			this.checkBox78 = new CheckBox();
			this.checkBox77 = new CheckBox();
			this.checkBox76 = new CheckBox();
			this.radioButton18 = new RadioButton();
			this.radioButton17 = new RadioButton();
			this.radioButton16 = new RadioButton();
			this.groupBox6 = new GroupBox();
			this.checkBox90 = new CheckBox();
			this.checkBox89 = new CheckBox();
			this.checkBox88 = new CheckBox();
			this.checkBox87 = new CheckBox();
			this.checkBox86 = new CheckBox();
			this.checkBox85 = new CheckBox();
			this.checkBox84 = new CheckBox();
			this.radioButton15 = new RadioButton();
			this.radioButton14 = new RadioButton();
			this.groupBox5 = new GroupBox();
			this.radioButton25 = new RadioButton();
			this.radioButton13 = new RadioButton();
			this.radioButton12 = new RadioButton();
			this.radioButton11 = new RadioButton();
			this.radioButton10 = new RadioButton();
			this.checkBox72 = new CheckBox();
			this.checkBox73 = new CheckBox();
			this.checkBox74 = new CheckBox();
			this.checkBox75 = new CheckBox();
			this.checkBox71 = new CheckBox();
			this.label69 = new Label();
			this.label68 = new Label();
			this.tabPage9 = new TabPage();
			this.button16 = new Button();
			this.textBox20 = new TextBox();
			this.label79 = new Label();
			this.label78 = new Label();
			this.textBox19 = new TextBox();
			this.label77 = new Label();
			this.textBox18 = new TextBox();
			this.tabPage10 = new TabPage();
			this.button63 = new Button();
			this.button62 = new Button();
			this.dateTimePicker18 = new DateTimePicker();
			this.button61 = new Button();
			this.listBox3 = new ListBox();
			this.dateTimePicker15 = new DateTimePicker();
			this.label129 = new Label();
			this.label130 = new Label();
			this.dateTimePicker16 = new DateTimePicker();
			this.dateTimePicker17 = new DateTimePicker();
			this.button19 = new Button();
			this.button20 = new Button();
			this.listBox2 = new ListBox();
			this.checkBox127 = new CheckBox();
			this.checkBox105 = new CheckBox();
			this.numericUpDown10 = new NumericUpDown();
			this.label94 = new Label();
			this.button18 = new Button();
			this.groupBox11 = new GroupBox();
			this.numericUpDown9 = new NumericUpDown();
			this.label93 = new Label();
			this.numericUpDown8 = new NumericUpDown();
			this.label92 = new Label();
			this.numericUpDown7 = new NumericUpDown();
			this.label89 = new Label();
			this.label90 = new Label();
			this.label91 = new Label();
			this.dateTimePicker12 = new DateTimePicker();
			this.dateTimePicker13 = new DateTimePicker();
			this.label87 = new Label();
			this.label88 = new Label();
			this.dateTimePicker10 = new DateTimePicker();
			this.dateTimePicker11 = new DateTimePicker();
			this.label86 = new Label();
			this.label85 = new Label();
			this.dateTimePicker9 = new DateTimePicker();
			this.dateTimePicker8 = new DateTimePicker();
			this.groupBox10 = new GroupBox();
			this.checkBox98 = new CheckBox();
			this.checkBox104 = new CheckBox();
			this.checkBox99 = new CheckBox();
			this.checkBox103 = new CheckBox();
			this.checkBox100 = new CheckBox();
			this.checkBox102 = new CheckBox();
			this.checkBox101 = new CheckBox();
			this.label84 = new Label();
			this.comboBox58 = new ComboBox();
			this.label83 = new Label();
			this.comboBox57 = new ComboBox();
			this.dateTimePicker6 = new DateTimePicker();
			this.dateTimePicker7 = new DateTimePicker();
			this.label81 = new Label();
			this.label82 = new Label();
			this.tabPage11 = new TabPage();
			this.label99 = new Label();
			this.numericUpDown14 = new NumericUpDown();
			this.label98 = new Label();
			this.label97 = new Label();
			this.numericUpDown13 = new NumericUpDown();
			this.button22 = new Button();
			this.button23 = new Button();
			this.tabPage12 = new TabPage();
			this.numericUpDown27 = new NumericUpDown();
			this.label156 = new Label();
			this.button89 = new Button();
			this.label154 = new Label();
			this.label155 = new Label();
			this.label152 = new Label();
			this.numericUpDown25 = new NumericUpDown();
			this.label153 = new Label();
			this.numericUpDown26 = new NumericUpDown();
			this.button26 = new Button();
			this.label107 = new Label();
			this.label104 = new Label();
			this.nudDatalen = new NumericUpDown();
			this.nudValue = new NumericUpDown();
			this.label105 = new Label();
			this.label106 = new Label();
			this.label100 = new Label();
			this.label101 = new Label();
			this.nudEndPage = new NumericUpDown();
			this.label102 = new Label();
			this.label103 = new Label();
			this.nudStartPage = new NumericUpDown();
			this.button25 = new Button();
			this.tabPage13 = new TabPage();
			this.checkBox135 = new CheckBox();
			this.button83 = new Button();
			this.label146 = new Label();
			this.txtOldCommPassword = new TextBox();
			this.textBox32 = new TextBox();
			this.label110 = new Label();
			this.button71 = new Button();
			this.txtCommPassword = new TextBox();
			this.button57 = new Button();
			this.button35 = new Button();
			this.button56 = new Button();
			this.button54 = new Button();
			this.button52 = new Button();
			this.button36 = new Button();
			this.checkBox117 = new CheckBox();
			this.label111 = new Label();
			this.checkBox116 = new CheckBox();
			this.checkBox118 = new CheckBox();
			this.checkBox115 = new CheckBox();
			this.checkBox114 = new CheckBox();
			this.checkBox113 = new CheckBox();
			this.txt02e2 = new TextBox();
			this.label109 = new Label();
			this.label108 = new Label();
			this.nudNewSN = new NumericUpDown();
			this.button28 = new Button();
			this.button27 = new Button();
			this.tabPage14 = new TabPage();
			this.button33 = new Button();
			this.button30 = new Button();
			this.tabPage15 = new TabPage();
			this.button42 = new Button();
			this.button41 = new Button();
			this.button40 = new Button();
			this.button39 = new Button();
			this.label113 = new Label();
			this.button38 = new Button();
			this.button37 = new Button();
			this.label112 = new Label();
			this.textBox21 = new TextBox();
			this.tabPage16 = new TabPage();
			this.textBox23 = new TextBox();
			this.textBox22 = new TextBox();
			this.checkBox112 = new CheckBox();
			this.tabPage17 = new TabPage();
			this.button48 = new Button();
			this.button47 = new Button();
			this.tabPage18 = new TabPage();
			this.button90 = new Button();
			this.label157 = new Label();
			this.label158 = new Label();
			this.textBox37 = new TextBox();
			this.textBox38 = new TextBox();
			this.button58 = new Button();
			this.richTextBox2 = new RichTextBox();
			this.richTextBox1 = new RichTextBox();
			this.button60 = new Button();
			this.button59 = new Button();
			this.numericUpDown17 = new NumericUpDown();
			this.label118 = new Label();
			this.textBox26 = new TextBox();
			this.label117 = new Label();
			this.label116 = new Label();
			this.label125 = new Label();
			this.label115 = new Label();
			this.textBox25 = new TextBox();
			this.textBox24 = new TextBox();
			this.button50 = new Button();
			this.tabPage19 = new TabPage();
			this.label137 = new Label();
			this.cboDoors = new ComboBox();
			this.label138 = new Label();
			this.button64 = new Button();
			this.button65 = new Button();
			this.button66 = new Button();
			this.button67 = new Button();
			this.listBox4 = new ListBox();
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
			this.label128 = new Label();
			this.label132 = new Label();
			this.dateEndHMS1 = new DateTimePicker();
			this.label133 = new Label();
			this.grpBegin = new GroupBox();
			this.cboBeginControlStatus = new ComboBox();
			this.label134 = new Label();
			this.label135 = new Label();
			this.dateBeginHMS1 = new DateTimePicker();
			this.label136 = new Label();
			this.tabPage20 = new TabPage();
			this.label143 = new Label();
			this.numericUpDown22 = new NumericUpDown();
			this.button72 = new Button();
			this.checkBox131 = new CheckBox();
			this.checkBox132 = new CheckBox();
			this.numericUpDown20 = new NumericUpDown();
			this.checkBox130 = new CheckBox();
			this.label141 = new Label();
			this.numericUpDown21 = new NumericUpDown();
			this.button70 = new Button();
			this.label142 = new Label();
			this.label139 = new Label();
			this.textBox31 = new TextBox();
			this.button75 = new Button();
			this.button69 = new Button();
			this.tabPage21 = new TabPage();
			this.comboBox60 = new ComboBox();
			this.button91 = new Button();
			this.button88 = new Button();
			this.button79 = new Button();
			this.button78 = new Button();
			this.dataGridView3 = new DataGridView();
			this.dataGridView2 = new DataGridView();
			this.button76 = new Button();
			this.groupBox13 = new GroupBox();
			this.label144 = new Label();
			this.comboBox59 = new ComboBox();
			this.button73 = new Button();
			this.numericUpDown23 = new NumericUpDown();
			this.checkBox133 = new CheckBox();
			this.button74 = new Button();
			this.tabPage22 = new TabPage();
			this.button77 = new Button();
			this.numericUpDown24 = new NumericUpDown();
			this.label145 = new Label();
			this.tabPage23 = new TabPage();
			this.button87 = new Button();
			this.button86 = new Button();
			this.label151 = new Label();
			this.textBox36 = new TextBox();
			this.textBox35 = new TextBox();
			this.button82 = new Button();
			this.textBox34 = new TextBox();
			this.button80 = new Button();
			this.tabPage25 = new TabPage();
			this.button95 = new Button();
			this.button94 = new Button();
			this.button93 = new Button();
			this.button92 = new Button();
			this.txtSN = new NumericUpDown();
			this.label1 = new Label();
			this.txtInfo = new TextBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.button3 = new Button();
			this.button21 = new Button();
			this.button24 = new Button();
			this.button29 = new Button();
			this.button31 = new Button();
			this.button32 = new Button();
			this.button34 = new Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button43 = new Button();
			this.label114 = new Label();
			this.button44 = new Button();
			this.button45 = new Button();
			this.button46 = new Button();
			this.button49 = new Button();
			this.button51 = new Button();
			this.textBox29 = new TextBox();
			this.label119 = new Label();
			this.label120 = new Label();
			this.textBox30 = new TextBox();
			this.button55 = new Button();
			this.checkBox120 = new CheckBox();
			this.grpbIP = new GroupBox();
			this.nudPort = new NumericUpDown();
			this.txtControllerIP = new TextBox();
			this.label123 = new Label();
			this.label124 = new Label();
			this.lblWrongProductCode = new Label();
			this.label140 = new Label();
			this.openFileDialog1 = new OpenFileDialog();
			this.panel1 = new Panel();
			this.label147 = new Label();
			this.button81 = new Button();
			this.folderBrowserDialog1 = new FolderBrowserDialog();
			this.tabControl1.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox14.SuspendLayout();
			((ISupportInitialize)this.numericUpDown19).BeginInit();
			((ISupportInitialize)this.numericUpDown18).BeginInit();
			((ISupportInitialize)this.numericUpDown12).BeginInit();
			((ISupportInitialize)this.numericUpDown16).BeginInit();
			((ISupportInitialize)this.numericUpDown11).BeginInit();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabPage24.SuspendLayout();
			this.groupBox19.SuspendLayout();
			((ISupportInitialize)this.numericUpDown32).BeginInit();
			this.groupBox17.SuspendLayout();
			((ISupportInitialize)this.numericUpDown30).BeginInit();
			this.groupBox18.SuspendLayout();
			((ISupportInitialize)this.numericUpDown31).BeginInit();
			this.groupBox16.SuspendLayout();
			((ISupportInitialize)this.numericUpDown29).BeginInit();
			this.groupBox15.SuspendLayout();
			((ISupportInitialize)this.numericUpDown28).BeginInit();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.groupBox12.SuspendLayout();
			((ISupportInitialize)this.numericUpDown6).BeginInit();
			((ISupportInitialize)this.numericUpDown15).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((ISupportInitialize)this.dgvControlConfure).BeginInit();
			this.tabPage5.SuspendLayout();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage8.SuspendLayout();
			this.groupBox9.SuspendLayout();
			((ISupportInitialize)this.numericUpDown5).BeginInit();
			((ISupportInitialize)this.numericUpDown4).BeginInit();
			((ISupportInitialize)this.numericUpDown3).BeginInit();
			this.groupBox8.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.tabPage9.SuspendLayout();
			this.tabPage10.SuspendLayout();
			((ISupportInitialize)this.numericUpDown10).BeginInit();
			this.groupBox11.SuspendLayout();
			((ISupportInitialize)this.numericUpDown9).BeginInit();
			((ISupportInitialize)this.numericUpDown8).BeginInit();
			((ISupportInitialize)this.numericUpDown7).BeginInit();
			this.groupBox10.SuspendLayout();
			this.tabPage11.SuspendLayout();
			((ISupportInitialize)this.numericUpDown14).BeginInit();
			((ISupportInitialize)this.numericUpDown13).BeginInit();
			this.tabPage12.SuspendLayout();
			((ISupportInitialize)this.numericUpDown27).BeginInit();
			((ISupportInitialize)this.numericUpDown25).BeginInit();
			((ISupportInitialize)this.numericUpDown26).BeginInit();
			((ISupportInitialize)this.nudDatalen).BeginInit();
			((ISupportInitialize)this.nudValue).BeginInit();
			((ISupportInitialize)this.nudEndPage).BeginInit();
			((ISupportInitialize)this.nudStartPage).BeginInit();
			this.tabPage13.SuspendLayout();
			((ISupportInitialize)this.nudNewSN).BeginInit();
			this.tabPage14.SuspendLayout();
			this.tabPage15.SuspendLayout();
			this.tabPage16.SuspendLayout();
			this.tabPage17.SuspendLayout();
			this.tabPage18.SuspendLayout();
			((ISupportInitialize)this.numericUpDown17).BeginInit();
			this.tabPage19.SuspendLayout();
			this.grpWeekdayControl.SuspendLayout();
			this.grpEnd.SuspendLayout();
			this.grpBegin.SuspendLayout();
			this.tabPage20.SuspendLayout();
			((ISupportInitialize)this.numericUpDown22).BeginInit();
			((ISupportInitialize)this.numericUpDown20).BeginInit();
			((ISupportInitialize)this.numericUpDown21).BeginInit();
			this.tabPage21.SuspendLayout();
			((ISupportInitialize)this.dataGridView3).BeginInit();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			this.groupBox13.SuspendLayout();
			((ISupportInitialize)this.numericUpDown23).BeginInit();
			this.tabPage22.SuspendLayout();
			((ISupportInitialize)this.numericUpDown24).BeginInit();
			this.tabPage23.SuspendLayout();
			this.tabPage25.SuspendLayout();
			((ISupportInitialize)this.txtSN).BeginInit();
			this.grpbIP.SuspendLayout();
			((ISupportInitialize)this.nudPort).BeginInit();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage24);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage8);
			this.tabControl1.Controls.Add(this.tabPage9);
			this.tabControl1.Controls.Add(this.tabPage10);
			this.tabControl1.Controls.Add(this.tabPage11);
			this.tabControl1.Controls.Add(this.tabPage12);
			this.tabControl1.Controls.Add(this.tabPage13);
			this.tabControl1.Controls.Add(this.tabPage14);
			this.tabControl1.Controls.Add(this.tabPage15);
			this.tabControl1.Controls.Add(this.tabPage16);
			this.tabControl1.Controls.Add(this.tabPage17);
			this.tabControl1.Controls.Add(this.tabPage18);
			this.tabControl1.Controls.Add(this.tabPage19);
			this.tabControl1.Controls.Add(this.tabPage20);
			this.tabControl1.Controls.Add(this.tabPage21);
			this.tabControl1.Controls.Add(this.tabPage22);
			this.tabControl1.Controls.Add(this.tabPage23);
			this.tabControl1.Controls.Add(this.tabPage25);
			this.tabControl1.Location = new Point(157, 25);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(571, 708);
			this.tabControl1.TabIndex = 0;
			this.tabPage7.Controls.Add(this.label148);
			this.tabPage7.Controls.Add(this.label149);
			this.tabPage7.Controls.Add(this.label150);
			this.tabPage7.Controls.Add(this.textBox27);
			this.tabPage7.Controls.Add(this.textBox28);
			this.tabPage7.Controls.Add(this.textBox33);
			this.tabPage7.Controls.Add(this.checkBox134);
			this.tabPage7.Controls.Add(this.checkBox109);
			this.tabPage7.Controls.Add(this.checkBox108);
			this.tabPage7.Controls.Add(this.checkBox107);
			this.tabPage7.Controls.Add(this.checkBox106);
			this.tabPage7.Controls.Add(this.label67);
			this.tabPage7.Controls.Add(this.textBox17);
			this.tabPage7.Controls.Add(this.checkBox70);
			this.tabPage7.Controls.Add(this.checkBox69);
			this.tabPage7.Controls.Add(this.checkBox68);
			this.tabPage7.Controls.Add(this.checkBox67);
			this.tabPage7.Controls.Add(this.checkBox66);
			this.tabPage7.Controls.Add(this.checkBox65);
			this.tabPage7.Controls.Add(this.checkBox64);
			this.tabPage7.Controls.Add(this.checkBox63);
			this.tabPage7.Controls.Add(this.checkBox62);
			this.tabPage7.Controls.Add(this.checkBox61);
			this.tabPage7.Controls.Add(this.checkBox60);
			this.tabPage7.Controls.Add(this.textBox13);
			this.tabPage7.Controls.Add(this.textBox14);
			this.tabPage7.Controls.Add(this.textBox15);
			this.tabPage7.Controls.Add(this.textBox16);
			this.tabPage7.Controls.Add(this.textBox9);
			this.tabPage7.Controls.Add(this.textBox10);
			this.tabPage7.Controls.Add(this.textBox11);
			this.tabPage7.Controls.Add(this.textBox12);
			this.tabPage7.Controls.Add(this.textBox5);
			this.tabPage7.Controls.Add(this.textBox6);
			this.tabPage7.Controls.Add(this.textBox7);
			this.tabPage7.Controls.Add(this.textBox8);
			this.tabPage7.Controls.Add(this.label66);
			this.tabPage7.Controls.Add(this.textBox4);
			this.tabPage7.Controls.Add(this.label65);
			this.tabPage7.Controls.Add(this.textBox3);
			this.tabPage7.Controls.Add(this.label64);
			this.tabPage7.Controls.Add(this.textBox2);
			this.tabPage7.Controls.Add(this.label63);
			this.tabPage7.Controls.Add(this.textBox1);
			this.tabPage7.Controls.Add(this.label59);
			this.tabPage7.Controls.Add(this.label60);
			this.tabPage7.Controls.Add(this.label61);
			this.tabPage7.Controls.Add(this.label62);
			this.tabPage7.Controls.Add(this.checkBox59);
			this.tabPage7.Controls.Add(this.comboBox56);
			this.tabPage7.Controls.Add(this.comboBox55);
			this.tabPage7.Controls.Add(this.comboBox54);
			this.tabPage7.Controls.Add(this.comboBox53);
			this.tabPage7.Controls.Add(this.label55);
			this.tabPage7.Controls.Add(this.label56);
			this.tabPage7.Controls.Add(this.label57);
			this.tabPage7.Controls.Add(this.label58);
			this.tabPage7.Controls.Add(this.checkBox58);
			this.tabPage7.Location = new Point(4, 76);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Padding = new Padding(3);
			this.tabPage7.Size = new Size(563, 628);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "基本参数";
			this.tabPage7.UseVisualStyleBackColor = true;
			this.label148.AutoSize = true;
			this.label148.ImeMode = ImeMode.NoControl;
			this.label148.Location = new Point(56, 508);
			this.label148.Name = "label148";
			this.label148.Size = new Size(59, 12);
			this.label148.TabIndex = 98;
			this.label148.Text = "33_总位数";
			this.label149.AutoSize = true;
			this.label149.ImeMode = ImeMode.NoControl;
			this.label149.Location = new Point(162, 508);
			this.label149.Name = "label149";
			this.label149.Size = new Size(125, 12);
			this.label149.TabIndex = 99;
			this.label149.Text = "28_起始位(从1开始计)";
			this.label150.AutoSize = true;
			this.label150.ImeMode = ImeMode.NoControl;
			this.label150.Location = new Point(306, 508);
			this.label150.Name = "label150";
			this.label150.Size = new Size(71, 12);
			this.label150.TabIndex = 100;
			this.label150.Text = "27_有效长度";
			this.textBox27.Location = new Point(303, 523);
			this.textBox27.Name = "textBox27";
			this.textBox27.Size = new Size(100, 21);
			this.textBox27.TabIndex = 97;
			this.textBox28.Location = new Point(159, 523);
			this.textBox28.Name = "textBox28";
			this.textBox28.Size = new Size(100, 21);
			this.textBox28.TabIndex = 96;
			this.textBox33.Location = new Point(53, 523);
			this.textBox33.Name = "textBox33";
			this.textBox33.Size = new Size(100, 21);
			this.textBox33.TabIndex = 95;
			this.checkBox134.AutoSize = true;
			this.checkBox134.BackColor = Color.Red;
			this.checkBox134.ImeMode = ImeMode.NoControl;
			this.checkBox134.Location = new Point(16, 489);
			this.checkBox134.Name = "checkBox134";
			this.checkBox134.Size = new Size(180, 16);
			this.checkBox134.TabIndex = 94;
			this.checkBox134.Text = "134_选择要修改自定义卡格式";
			this.checkBox134.UseVisualStyleBackColor = false;
			this.checkBox109.AutoSize = true;
			this.checkBox109.Location = new Point(44, 448);
			this.checkBox109.Name = "checkBox109";
			this.checkBox109.Size = new Size(510, 16);
			this.checkBox109.TabIndex = 93;
			this.checkBox109.Text = "109_1顺序刷: 1号读卡器->3号读卡器->4号读卡器->2号读卡器->1号读卡器->3号读卡器->..";
			this.checkBox109.UseVisualStyleBackColor = true;
			this.checkBox108.AutoSize = true;
			this.checkBox108.Location = new Point(44, 426);
			this.checkBox108.Name = "checkBox108";
			this.checkBox108.Size = new Size(354, 16);
			this.checkBox108.TabIndex = 92;
			this.checkBox108.Text = "108_0顺序刷: 1号读卡器->2号读卡器->1号读卡器->2号读卡器";
			this.checkBox108.UseVisualStyleBackColor = true;
			this.checkBox107.AutoSize = true;
			this.checkBox107.Location = new Point(44, 404);
			this.checkBox107.Name = "checkBox107";
			this.checkBox107.Size = new Size(156, 16);
			this.checkBox107.TabIndex = 91;
			this.checkBox107.Text = "107_不按顺序[最高级别]";
			this.checkBox107.UseVisualStyleBackColor = true;
			this.checkBox106.AutoSize = true;
			this.checkBox106.BackColor = Color.Red;
			this.checkBox106.Location = new Point(16, 382);
			this.checkBox106.Name = "checkBox106";
			this.checkBox106.Size = new Size(204, 16);
			this.checkBox106.TabIndex = 90;
			this.checkBox106.Text = "106_选择要修改启用顺序刷卡设置";
			this.checkBox106.UseVisualStyleBackColor = false;
			this.label67.AutoSize = true;
			this.label67.Location = new Point(29, 303);
			this.label67.Name = "label67";
			this.label67.Size = new Size(83, 12);
			this.label67.TabIndex = 89;
			this.label67.Text = "新胁迫密码_17";
			this.textBox17.Location = new Point(118, 300);
			this.textBox17.Name = "textBox17";
			this.textBox17.Size = new Size(100, 21);
			this.textBox17.TabIndex = 88;
			this.checkBox70.AutoSize = true;
			this.checkBox70.Location = new Point(214, 341);
			this.checkBox70.Name = "checkBox70";
			this.checkBox70.Size = new Size(138, 16);
			this.checkBox70.TabIndex = 87;
			this.checkBox70.Text = "70_启用记录报警事件";
			this.checkBox70.UseVisualStyleBackColor = true;
			this.checkBox69.AutoSize = true;
			this.checkBox69.BackColor = Color.Red;
			this.checkBox69.Location = new Point(16, 341);
			this.checkBox69.Name = "checkBox69";
			this.checkBox69.Size = new Size(174, 16);
			this.checkBox69.TabIndex = 59;
			this.checkBox69.Text = "69_选择要修改记录报警事件";
			this.checkBox69.UseVisualStyleBackColor = false;
			this.checkBox68.AutoSize = true;
			this.checkBox68.Location = new Point(378, 278);
			this.checkBox68.Name = "checkBox68";
			this.checkBox68.Size = new Size(168, 16);
			this.checkBox68.TabIndex = 86;
			this.checkBox68.Text = "68_7胁迫报警必须刷合法卡";
			this.checkBox68.UseVisualStyleBackColor = true;
			this.checkBox67.AutoSize = true;
			this.checkBox67.Location = new Point(276, 278);
			this.checkBox67.Name = "checkBox67";
			this.checkBox67.Size = new Size(96, 16);
			this.checkBox67.TabIndex = 85;
			this.checkBox67.Text = "67_6防盗报警";
			this.checkBox67.UseVisualStyleBackColor = true;
			this.checkBox66.AutoSize = true;
			this.checkBox66.Location = new Point(142, 278);
			this.checkBox66.Name = "checkBox66";
			this.checkBox66.Size = new Size(72, 16);
			this.checkBox66.TabIndex = 84;
			this.checkBox66.Text = "66_5火警";
			this.checkBox66.UseVisualStyleBackColor = true;
			this.checkBox65.AutoSize = true;
			this.checkBox65.Location = new Point(32, 278);
			this.checkBox65.Name = "checkBox65";
			this.checkBox65.Size = new Size(108, 16);
			this.checkBox65.TabIndex = 83;
			this.checkBox65.Text = "65_4非法卡报警";
			this.checkBox65.UseVisualStyleBackColor = true;
			this.checkBox64.AutoSize = true;
			this.checkBox64.Location = new Point(378, 256);
			this.checkBox64.Name = "checkBox64";
			this.checkBox64.Size = new Size(96, 16);
			this.checkBox64.TabIndex = 82;
			this.checkBox64.Text = "64_3强制关门";
			this.checkBox64.UseVisualStyleBackColor = true;
			this.checkBox63.AutoSize = true;
			this.checkBox63.Location = new Point(276, 256);
			this.checkBox63.Name = "checkBox63";
			this.checkBox63.Size = new Size(96, 16);
			this.checkBox63.TabIndex = 81;
			this.checkBox63.Text = "63_2强行闯入";
			this.checkBox63.UseVisualStyleBackColor = true;
			this.checkBox62.AutoSize = true;
			this.checkBox62.Location = new Point(138, 256);
			this.checkBox62.Name = "checkBox62";
			this.checkBox62.Size = new Size(132, 16);
			this.checkBox62.TabIndex = 80;
			this.checkBox62.Text = "62_1门打开时间过长";
			this.checkBox62.UseVisualStyleBackColor = true;
			this.checkBox61.AutoSize = true;
			this.checkBox61.Location = new Point(31, 256);
			this.checkBox61.Name = "checkBox61";
			this.checkBox61.Size = new Size(96, 16);
			this.checkBox61.TabIndex = 79;
			this.checkBox61.Text = "61_0胁迫报警";
			this.checkBox61.UseVisualStyleBackColor = true;
			this.checkBox60.AutoSize = true;
			this.checkBox60.BackColor = Color.Red;
			this.checkBox60.Location = new Point(16, 234);
			this.checkBox60.Name = "checkBox60";
			this.checkBox60.Size = new Size(174, 16);
			this.checkBox60.TabIndex = 78;
			this.checkBox60.Text = "60_选择要修改启用报警设置";
			this.checkBox60.UseVisualStyleBackColor = false;
			this.textBox13.Location = new Point(400, 126);
			this.textBox13.Name = "textBox13";
			this.textBox13.Size = new Size(100, 21);
			this.textBox13.TabIndex = 77;
			this.textBox14.Location = new Point(400, 153);
			this.textBox14.Name = "textBox14";
			this.textBox14.Size = new Size(100, 21);
			this.textBox14.TabIndex = 76;
			this.textBox15.Location = new Point(400, 180);
			this.textBox15.Name = "textBox15";
			this.textBox15.Size = new Size(100, 21);
			this.textBox15.TabIndex = 75;
			this.textBox16.Location = new Point(400, 207);
			this.textBox16.Name = "textBox16";
			this.textBox16.Size = new Size(100, 21);
			this.textBox16.TabIndex = 74;
			this.textBox9.Location = new Point(294, 126);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new Size(100, 21);
			this.textBox9.TabIndex = 73;
			this.textBox10.Location = new Point(294, 153);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new Size(100, 21);
			this.textBox10.TabIndex = 72;
			this.textBox11.Location = new Point(294, 180);
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new Size(100, 21);
			this.textBox11.TabIndex = 71;
			this.textBox12.Location = new Point(294, 207);
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new Size(100, 21);
			this.textBox12.TabIndex = 70;
			this.textBox5.Location = new Point(188, 126);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new Size(100, 21);
			this.textBox5.TabIndex = 69;
			this.textBox6.Location = new Point(188, 153);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new Size(100, 21);
			this.textBox6.TabIndex = 68;
			this.textBox7.Location = new Point(188, 180);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new Size(100, 21);
			this.textBox7.TabIndex = 67;
			this.textBox8.Location = new Point(188, 207);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new Size(100, 21);
			this.textBox8.TabIndex = 66;
			this.label66.AutoSize = true;
			this.label66.Location = new Point(29, 210);
			this.label66.Name = "label66";
			this.label66.Size = new Size(35, 12);
			this.label66.TabIndex = 65;
			this.label66.Text = "密码4";
			this.textBox4.Location = new Point(82, 207);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new Size(100, 21);
			this.textBox4.TabIndex = 64;
			this.label65.AutoSize = true;
			this.label65.Location = new Point(29, 183);
			this.label65.Name = "label65";
			this.label65.Size = new Size(35, 12);
			this.label65.TabIndex = 63;
			this.label65.Text = "密码3";
			this.textBox3.Location = new Point(82, 180);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new Size(100, 21);
			this.textBox3.TabIndex = 62;
			this.label64.AutoSize = true;
			this.label64.Location = new Point(29, 156);
			this.label64.Name = "label64";
			this.label64.Size = new Size(35, 12);
			this.label64.TabIndex = 61;
			this.label64.Text = "密码2";
			this.textBox2.Location = new Point(82, 153);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Size(100, 21);
			this.textBox2.TabIndex = 60;
			this.label63.AutoSize = true;
			this.label63.Location = new Point(29, 129);
			this.label63.Name = "label63";
			this.label63.Size = new Size(35, 12);
			this.label63.TabIndex = 59;
			this.label63.Text = "密码1";
			this.textBox1.Location = new Point(82, 126);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(100, 21);
			this.textBox1.TabIndex = 58;
			this.label59.AutoSize = true;
			this.label59.Location = new Point(94, 111);
			this.label59.Name = "label59";
			this.label59.Size = new Size(71, 12);
			this.label59.TabIndex = 54;
			this.label59.Text = "1_1号读卡器";
			this.label60.AutoSize = true;
			this.label60.Location = new Point(198, 111);
			this.label60.Name = "label60";
			this.label60.Size = new Size(71, 12);
			this.label60.TabIndex = 55;
			this.label60.Text = "5_2号读卡器";
			this.label61.AutoSize = true;
			this.label61.Location = new Point(302, 111);
			this.label61.Name = "label61";
			this.label61.Size = new Size(71, 12);
			this.label61.TabIndex = 56;
			this.label61.Text = "9_3号读卡器";
			this.label62.AutoSize = true;
			this.label62.Location = new Point(406, 111);
			this.label62.Name = "label62";
			this.label62.Size = new Size(77, 12);
			this.label62.TabIndex = 57;
			this.label62.Text = "13_4号读卡器";
			this.checkBox59.AutoSize = true;
			this.checkBox59.BackColor = Color.Red;
			this.checkBox59.Location = new Point(16, 92);
			this.checkBox59.Name = "checkBox59";
			this.checkBox59.Size = new Size(174, 16);
			this.checkBox59.TabIndex = 53;
			this.checkBox59.Text = "59_选择要修改超级开门密码";
			this.checkBox59.UseVisualStyleBackColor = false;
			this.comboBox56.FormattingEnabled = true;
			this.comboBox56.Items.AddRange(new object[]
			{
				"00 不受控制",
				"01 常开",
				"02 常闭",
				"03 在线"
			});
			this.comboBox56.Location = new Point(413, 59);
			this.comboBox56.Name = "comboBox56";
			this.comboBox56.Size = new Size(121, 20);
			this.comboBox56.TabIndex = 52;
			this.comboBox56.Text = "00 不受控制";
			this.comboBox55.FormattingEnabled = true;
			this.comboBox55.Items.AddRange(new object[]
			{
				"00 不受控制",
				"01 常开",
				"02 常闭",
				"03 在线"
			});
			this.comboBox55.Location = new Point(286, 59);
			this.comboBox55.Name = "comboBox55";
			this.comboBox55.Size = new Size(121, 20);
			this.comboBox55.TabIndex = 51;
			this.comboBox55.Text = "00 不受控制";
			this.comboBox54.FormattingEnabled = true;
			this.comboBox54.Items.AddRange(new object[]
			{
				"00 不受控制",
				"01 常开",
				"02 常闭",
				"03 在线"
			});
			this.comboBox54.Location = new Point(159, 59);
			this.comboBox54.Name = "comboBox54";
			this.comboBox54.Size = new Size(121, 20);
			this.comboBox54.TabIndex = 50;
			this.comboBox54.Text = "00 不受控制";
			this.comboBox53.FormattingEnabled = true;
			this.comboBox53.Items.AddRange(new object[]
			{
				"00 不受控制",
				"01 常开",
				"02 常闭",
				"03 在线"
			});
			this.comboBox53.Location = new Point(32, 59);
			this.comboBox53.Name = "comboBox53";
			this.comboBox53.Size = new Size(121, 20);
			this.comboBox53.TabIndex = 49;
			this.comboBox53.Text = "00 不受控制";
			this.label55.AutoSize = true;
			this.label55.Location = new Point(419, 44);
			this.label55.Name = "label55";
			this.label55.Size = new Size(35, 12);
			this.label55.TabIndex = 45;
			this.label55.Text = "4号门";
			this.label56.AutoSize = true;
			this.label56.Location = new Point(65, 44);
			this.label56.Name = "label56";
			this.label56.Size = new Size(35, 12);
			this.label56.TabIndex = 46;
			this.label56.Text = "1号门";
			this.label57.AutoSize = true;
			this.label57.Location = new Point(180, 44);
			this.label57.Name = "label57";
			this.label57.Size = new Size(35, 12);
			this.label57.TabIndex = 47;
			this.label57.Text = "2号门";
			this.label58.AutoSize = true;
			this.label58.Location = new Point(313, 44);
			this.label58.Name = "label58";
			this.label58.Size = new Size(35, 12);
			this.label58.TabIndex = 48;
			this.label58.Text = "3号门";
			this.checkBox58.AutoSize = true;
			this.checkBox58.BackColor = Color.Red;
			this.checkBox58.Location = new Point(16, 17);
			this.checkBox58.Name = "checkBox58";
			this.checkBox58.Size = new Size(162, 16);
			this.checkBox58.TabIndex = 40;
			this.checkBox58.Text = "58_选择要修改门控制方式";
			this.checkBox58.UseVisualStyleBackColor = false;
			this.tabPage1.Controls.Add(this.groupBox14);
			this.tabPage1.Controls.Add(this.checkBox126);
			this.tabPage1.Controls.Add(this.numericUpDown19);
			this.tabPage1.Controls.Add(this.label127);
			this.tabPage1.Controls.Add(this.checkBox124);
			this.tabPage1.Controls.Add(this.checkBox125);
			this.tabPage1.Controls.Add(this.checkBox122);
			this.tabPage1.Controls.Add(this.checkBox123);
			this.tabPage1.Controls.Add(this.checkBox121);
			this.tabPage1.Controls.Add(this.numericUpDown18);
			this.tabPage1.Controls.Add(this.label126);
			this.tabPage1.Controls.Add(this.checkBox119);
			this.tabPage1.Controls.Add(this.checkBox110);
			this.tabPage1.Controls.Add(this.numericUpDown12);
			this.tabPage1.Controls.Add(this.label96);
			this.tabPage1.Controls.Add(this.numericUpDown16);
			this.tabPage1.Controls.Add(this.numericUpDown11);
			this.tabPage1.Controls.Add(this.label122);
			this.tabPage1.Controls.Add(this.label95);
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.checkBox57);
			this.tabPage1.Controls.Add(this.checkBox56);
			this.tabPage1.Controls.Add(this.checkBox55);
			this.tabPage1.Controls.Add(this.checkBox54);
			this.tabPage1.Controls.Add(this.checkBox53);
			this.tabPage1.Controls.Add(this.checkBox52);
			this.tabPage1.Controls.Add(this.comboBox52);
			this.tabPage1.Controls.Add(this.comboBox51);
			this.tabPage1.Controls.Add(this.comboBox50);
			this.tabPage1.Controls.Add(this.comboBox49);
			this.tabPage1.Controls.Add(this.label51);
			this.tabPage1.Controls.Add(this.label52);
			this.tabPage1.Controls.Add(this.label53);
			this.tabPage1.Controls.Add(this.label54);
			this.tabPage1.Controls.Add(this.label50);
			this.tabPage1.Controls.Add(this.checkBox51);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.checkBox50);
			this.tabPage1.Controls.Add(this.checkBox23);
			this.tabPage1.Controls.Add(this.checkBox22);
			this.tabPage1.Controls.Add(this.checkBox21);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.checkBox4);
			this.tabPage1.Controls.Add(this.checkBox3);
			this.tabPage1.Controls.Add(this.checkBox2);
			this.tabPage1.Controls.Add(this.checkBox1);
			this.tabPage1.Location = new Point(4, 76);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(563, 628);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "扩展参数";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.groupBox14.Controls.Add(this.radioButton27);
			this.groupBox14.Controls.Add(this.radioButton28);
			this.groupBox14.Location = new Point(176, 168);
			this.groupBox14.Name = "groupBox14";
			this.groupBox14.Size = new Size(246, 33);
			this.groupBox14.TabIndex = 74;
			this.groupBox14.TabStop = false;
			this.radioButton27.AutoSize = true;
			this.radioButton27.Checked = true;
			this.radioButton27.Location = new Point(6, 17);
			this.radioButton27.Name = "radioButton27";
			this.radioButton27.Size = new Size(101, 16);
			this.radioButton27.TabIndex = 3;
			this.radioButton27.TabStop = true;
			this.radioButton27.Text = "27_[先进后出]";
			this.radioButton27.UseVisualStyleBackColor = true;
			this.radioButton28.AutoSize = true;
			this.radioButton28.Location = new Point(115, 17);
			this.radioButton28.Name = "radioButton28";
			this.radioButton28.Size = new Size(137, 16);
			this.radioButton28.TabIndex = 2;
			this.radioButton28.Text = "28_[先进或先出都行]";
			this.radioButton28.UseVisualStyleBackColor = true;
			this.checkBox126.AutoSize = true;
			this.checkBox126.BackColor = Color.Red;
			this.checkBox126.Location = new Point(14, 612);
			this.checkBox126.Name = "checkBox126";
			this.checkBox126.Size = new Size(96, 16);
			this.checkBox126.TabIndex = 73;
			this.checkBox126.Text = "126_心跳周期";
			this.checkBox126.UseVisualStyleBackColor = false;
			NumericUpDown arg_4913_0 = this.numericUpDown19;
			int[] array = new int[4];
			array[0] = 2;
			arg_4913_0.Increment = new decimal(array);
			this.numericUpDown19.Location = new Point(159, 611);
			NumericUpDown arg_4950_0 = this.numericUpDown19;
			int[] array2 = new int[4];
			array2[0] = 65535;
			arg_4950_0.Maximum = new decimal(array2);
			this.numericUpDown19.Name = "numericUpDown19";
			this.numericUpDown19.Size = new Size(62, 21);
			this.numericUpDown19.TabIndex = 72;
			this.label127.AutoSize = true;
			this.label127.Location = new Point(226, 616);
			this.label127.Name = "label127";
			this.label127.Size = new Size(317, 12);
			this.label127.TabIndex = 71;
			this.label127.Text = "分钟(19), 设置建议不小于5分钟. 如果0分钟或表示不启用";
			this.checkBox124.AutoSize = true;
			this.checkBox124.BackColor = Color.Red;
			this.checkBox124.Location = new Point(14, 590);
			this.checkBox124.Name = "checkBox124";
			this.checkBox124.Size = new Size(180, 16);
			this.checkBox124.TabIndex = 70;
			this.checkBox124.Text = "124_选择要修改自动检测网口";
			this.checkBox124.UseVisualStyleBackColor = false;
			this.checkBox125.AutoSize = true;
			this.checkBox125.Location = new Point(248, 590);
			this.checkBox125.Name = "checkBox125";
			this.checkBox125.Size = new Size(96, 16);
			this.checkBox125.TabIndex = 69;
			this.checkBox125.Text = "125_选择启用";
			this.checkBox125.UseVisualStyleBackColor = true;
			this.checkBox122.AutoSize = true;
			this.checkBox122.BackColor = Color.Red;
			this.checkBox122.Location = new Point(14, 565);
			this.checkBox122.Name = "checkBox122";
			this.checkBox122.Size = new Size(204, 16);
			this.checkBox122.TabIndex = 68;
			this.checkBox122.Text = "122_选择要修改自动获取IP(DHCP)";
			this.checkBox122.UseVisualStyleBackColor = false;
			this.checkBox123.AutoSize = true;
			this.checkBox123.Location = new Point(248, 565);
			this.checkBox123.Name = "checkBox123";
			this.checkBox123.Size = new Size(96, 16);
			this.checkBox123.TabIndex = 67;
			this.checkBox123.Text = "123_选择启用";
			this.checkBox123.UseVisualStyleBackColor = true;
			this.checkBox121.AutoSize = true;
			this.checkBox121.BackColor = Color.Red;
			this.checkBox121.Location = new Point(14, 526);
			this.checkBox121.Name = "checkBox121";
			this.checkBox121.Size = new Size(132, 16);
			this.checkBox121.TabIndex = 66;
			this.checkBox121.Text = "121_PC控制刷卡开门";
			this.checkBox121.UseVisualStyleBackColor = false;
			NumericUpDown arg_4C79_0 = this.numericUpDown18;
			int[] array3 = new int[4];
			array3[0] = 2;
			arg_4C79_0.Increment = new decimal(array3);
			this.numericUpDown18.Location = new Point(159, 525);
			NumericUpDown arg_4CB6_0 = this.numericUpDown18;
			int[] array4 = new int[4];
			array4[0] = 65535;
			arg_4CB6_0.Maximum = new decimal(array4);
			this.numericUpDown18.Name = "numericUpDown18";
			this.numericUpDown18.Size = new Size(62, 21);
			this.numericUpDown18.TabIndex = 65;
			this.label126.AutoSize = true;
			this.label126.Location = new Point(226, 530);
			this.label126.Name = "label126";
			this.label126.Size = new Size(221, 12);
			this.label126.TabIndex = 64;
			this.label126.Text = "秒(位置18), 最大254秒, 0秒表示不启用";
			this.checkBox119.AutoSize = true;
			this.checkBox119.BackColor = Color.Red;
			this.checkBox119.Location = new Point(14, 488);
			this.checkBox119.Name = "checkBox119";
			this.checkBox119.Size = new Size(120, 16);
			this.checkBox119.TabIndex = 63;
			this.checkBox119.Text = "119_刷卡间隔约束";
			this.checkBox119.UseVisualStyleBackColor = false;
			this.checkBox110.AutoSize = true;
			this.checkBox110.BackColor = Color.Red;
			this.checkBox110.Location = new Point(14, 249);
			this.checkBox110.Name = "checkBox110";
			this.checkBox110.Size = new Size(156, 16);
			this.checkBox110.TabIndex = 63;
			this.checkBox110.Text = "110_选择要修改人数限制";
			this.checkBox110.UseVisualStyleBackColor = false;
			this.numericUpDown12.Location = new Point(498, 244);
			NumericUpDown arg_4E91_0 = this.numericUpDown12;
			int[] array5 = new int[4];
			array5[0] = 65535;
			arg_4E91_0.Maximum = new decimal(array5);
			this.numericUpDown12.Name = "numericUpDown12";
			this.numericUpDown12.Size = new Size(53, 21);
			this.numericUpDown12.TabIndex = 62;
			this.label96.AutoSize = true;
			this.label96.Location = new Point(381, 246);
			this.label96.Name = "label96";
			this.label96.Size = new Size(119, 12);
			this.label96.TabIndex = 61;
			this.label96.Text = "12_门内人数不能少于";
			NumericUpDown arg_4F48_0 = this.numericUpDown16;
			int[] array6 = new int[4];
			array6[0] = 2;
			arg_4F48_0.Increment = new decimal(array6);
			this.numericUpDown16.Location = new Point(159, 487);
			NumericUpDown arg_4F85_0 = this.numericUpDown16;
			int[] array7 = new int[4];
			array7[0] = 65535;
			arg_4F85_0.Maximum = new decimal(array7);
			this.numericUpDown16.Name = "numericUpDown16";
			this.numericUpDown16.Size = new Size(62, 21);
			this.numericUpDown16.TabIndex = 60;
			this.numericUpDown11.Location = new Point(316, 245);
			NumericUpDown arg_4FF3_0 = this.numericUpDown11;
			int[] array8 = new int[4];
			array8[0] = 65535;
			arg_4FF3_0.Maximum = new decimal(array8);
			this.numericUpDown11.Name = "numericUpDown11";
			this.numericUpDown11.Size = new Size(53, 21);
			this.numericUpDown11.TabIndex = 60;
			this.label122.AutoSize = true;
			this.label122.Location = new Point(226, 492);
			this.label122.Name = "label122";
			this.label122.Size = new Size(173, 12);
			this.label122.TabIndex = 59;
			this.label122.Text = "秒(16), 最大24*3600-1, 最小2";
			this.label95.AutoSize = true;
			this.label95.Location = new Point(181, 244);
			this.label95.Name = "label95";
			this.label95.Size = new Size(131, 12);
			this.label95.TabIndex = 59;
			this.label95.Text = "11_门内人数允许最大数";
			this.groupBox4.Controls.Add(this.radioButton8);
			this.groupBox4.Controls.Add(this.radioButton9);
			this.groupBox4.Location = new Point(195, 434);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(246, 39);
			this.groupBox4.TabIndex = 58;
			this.groupBox4.TabStop = false;
			this.radioButton8.AutoSize = true;
			this.radioButton8.Checked = true;
			this.radioButton8.Location = new Point(6, 17);
			this.radioButton8.Name = "radioButton8";
			this.radioButton8.Size = new Size(83, 16);
			this.radioButton8.TabIndex = 3;
			this.radioButton8.TabStop = true;
			this.radioButton8.Text = "8_[不启用]";
			this.radioButton8.UseVisualStyleBackColor = true;
			this.radioButton9.AutoSize = true;
			this.radioButton9.Location = new Point(119, 17);
			this.radioButton9.Name = "radioButton9";
			this.radioButton9.Size = new Size(95, 16);
			this.radioButton9.TabIndex = 2;
			this.radioButton9.Text = "9_[启用记录]";
			this.radioButton9.UseVisualStyleBackColor = true;
			this.checkBox57.AutoSize = true;
			this.checkBox57.BackColor = Color.Red;
			this.checkBox57.Location = new Point(15, 450);
			this.checkBox57.Name = "checkBox57";
			this.checkBox57.Size = new Size(174, 16);
			this.checkBox57.TabIndex = 57;
			this.checkBox57.Text = "57_选择要修改按钮门磁事件";
			this.checkBox57.UseVisualStyleBackColor = false;
			this.checkBox56.AutoSize = true;
			this.checkBox56.Location = new Point(363, 421);
			this.checkBox56.Name = "checkBox56";
			this.checkBox56.Size = new Size(96, 16);
			this.checkBox56.TabIndex = 56;
			this.checkBox56.Text = "56_4号门启用";
			this.checkBox56.UseVisualStyleBackColor = true;
			this.checkBox55.AutoSize = true;
			this.checkBox55.Location = new Point(261, 421);
			this.checkBox55.Name = "checkBox55";
			this.checkBox55.Size = new Size(96, 16);
			this.checkBox55.TabIndex = 55;
			this.checkBox55.Text = "55_3号门启用";
			this.checkBox55.UseVisualStyleBackColor = true;
			this.checkBox54.AutoSize = true;
			this.checkBox54.Location = new Point(159, 421);
			this.checkBox54.Name = "checkBox54";
			this.checkBox54.Size = new Size(96, 16);
			this.checkBox54.TabIndex = 54;
			this.checkBox54.Text = "54_2号门启用";
			this.checkBox54.UseVisualStyleBackColor = true;
			this.checkBox53.AutoSize = true;
			this.checkBox53.Location = new Point(44, 421);
			this.checkBox53.Name = "checkBox53";
			this.checkBox53.Size = new Size(96, 16);
			this.checkBox53.TabIndex = 53;
			this.checkBox53.Text = "53_1号门启用";
			this.checkBox53.UseVisualStyleBackColor = true;
			this.checkBox52.AutoSize = true;
			this.checkBox52.BackColor = Color.Red;
			this.checkBox52.Location = new Point(14, 399);
			this.checkBox52.Name = "checkBox52";
			this.checkBox52.Size = new Size(162, 16);
			this.checkBox52.TabIndex = 52;
			this.checkBox52.Text = "52_选择刷卡开, 再刷卡关";
			this.checkBox52.UseVisualStyleBackColor = false;
			this.comboBox52.FormattingEnabled = true;
			this.comboBox52.Items.AddRange(new object[]
			{
				"00按正常处理",
				"11首卡切换为常开",
				"12首卡切换为常闭",
				"13首卡切换为在线",
				"14只许首卡开门"
			});
			this.comboBox52.Location = new Point(422, 359);
			this.comboBox52.Name = "comboBox52";
			this.comboBox52.Size = new Size(120, 20);
			this.comboBox52.TabIndex = 51;
			this.comboBox52.Text = "00按正常处理";
			this.comboBox51.FormattingEnabled = true;
			this.comboBox51.Items.AddRange(new object[]
			{
				"00按正常处理",
				"11首卡切换为常开",
				"12首卡切换为常闭",
				"13首卡切换为在线",
				"14只许首卡开门"
			});
			this.comboBox51.Location = new Point(296, 359);
			this.comboBox51.Name = "comboBox51";
			this.comboBox51.Size = new Size(120, 20);
			this.comboBox51.TabIndex = 50;
			this.comboBox51.Text = "00按正常处理";
			this.comboBox50.FormattingEnabled = true;
			this.comboBox50.Items.AddRange(new object[]
			{
				"00按正常处理",
				"11首卡切换为常开",
				"12首卡切换为常闭",
				"13首卡切换为在线",
				"14只许首卡开门"
			});
			this.comboBox50.Location = new Point(170, 359);
			this.comboBox50.Name = "comboBox50";
			this.comboBox50.Size = new Size(120, 20);
			this.comboBox50.TabIndex = 49;
			this.comboBox50.Text = "00按正常处理";
			this.comboBox49.FormattingEnabled = true;
			this.comboBox49.Items.AddRange(new object[]
			{
				"00按正常处理",
				"11首卡切换为常开",
				"12首卡切换为常闭",
				"13首卡切换为在线",
				"14只许首卡开门"
			});
			this.comboBox49.Location = new Point(44, 359);
			this.comboBox49.Name = "comboBox49";
			this.comboBox49.Size = new Size(120, 20);
			this.comboBox49.TabIndex = 48;
			this.comboBox49.Text = "00按正常处理";
			this.label51.AutoSize = true;
			this.label51.Location = new Point(436, 342);
			this.label51.Name = "label51";
			this.label51.Size = new Size(35, 12);
			this.label51.TabIndex = 41;
			this.label51.Text = "4号门";
			this.label52.AutoSize = true;
			this.label52.Location = new Point(82, 342);
			this.label52.Name = "label52";
			this.label52.Size = new Size(35, 12);
			this.label52.TabIndex = 42;
			this.label52.Text = "1号门";
			this.label53.AutoSize = true;
			this.label53.Location = new Point(197, 342);
			this.label53.Name = "label53";
			this.label53.Size = new Size(35, 12);
			this.label53.TabIndex = 43;
			this.label53.Text = "2号门";
			this.label54.AutoSize = true;
			this.label54.Location = new Point(330, 342);
			this.label54.Name = "label54";
			this.label54.Size = new Size(35, 12);
			this.label54.TabIndex = 44;
			this.label54.Text = "3号门";
			this.label50.AutoSize = true;
			this.label50.Location = new Point(166, 322);
			this.label50.Name = "label50";
			this.label50.Size = new Size(101, 12);
			this.label50.TabIndex = 40;
			this.label50.Text = "刷首卡后切换到: ";
			this.checkBox51.AutoSize = true;
			this.checkBox51.BackColor = Color.Red;
			this.checkBox51.Location = new Point(14, 321);
			this.checkBox51.Name = "checkBox51";
			this.checkBox51.Size = new Size(126, 16);
			this.checkBox51.TabIndex = 39;
			this.checkBox51.Text = "51_选择要修改首卡";
			this.checkBox51.UseVisualStyleBackColor = false;
			this.groupBox3.Controls.Add(this.radioButton6);
			this.groupBox3.Controls.Add(this.radioButton7);
			this.groupBox3.Location = new Point(183, 272);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(246, 39);
			this.groupBox3.TabIndex = 38;
			this.groupBox3.TabStop = false;
			this.radioButton6.AutoSize = true;
			this.radioButton6.Checked = true;
			this.radioButton6.Location = new Point(6, 17);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new Size(83, 16);
			this.radioButton6.TabIndex = 3;
			this.radioButton6.TabStop = true;
			this.radioButton6.Text = "6_[不启用]";
			this.radioButton6.UseVisualStyleBackColor = true;
			this.radioButton7.AutoSize = true;
			this.radioButton7.Location = new Point(115, 17);
			this.radioButton7.Name = "radioButton7";
			this.radioButton7.Size = new Size(119, 16);
			this.radioButton7.TabIndex = 2;
			this.radioButton7.Text = "7_[启用定时任务]";
			this.radioButton7.UseVisualStyleBackColor = true;
			this.checkBox50.AutoSize = true;
			this.checkBox50.BackColor = Color.Red;
			this.checkBox50.Location = new Point(14, 290);
			this.checkBox50.Name = "checkBox50";
			this.checkBox50.Size = new Size(150, 16);
			this.checkBox50.TabIndex = 37;
			this.checkBox50.Text = "50_选择要修改定时任务";
			this.checkBox50.UseVisualStyleBackColor = false;
			this.checkBox23.AutoSize = true;
			this.checkBox23.BackColor = Color.Red;
			this.checkBox23.Location = new Point(14, 7);
			this.checkBox23.Name = "checkBox23";
			this.checkBox23.Size = new Size(150, 16);
			this.checkBox23.TabIndex = 36;
			this.checkBox23.Text = "23_选择要修改密码键盘";
			this.checkBox23.UseVisualStyleBackColor = false;
			this.checkBox22.AutoSize = true;
			this.checkBox22.BackColor = Color.Red;
			this.checkBox22.Location = new Point(15, 184);
			this.checkBox22.Name = "checkBox22";
			this.checkBox22.Size = new Size(138, 16);
			this.checkBox22.TabIndex = 35;
			this.checkBox22.Text = "22_选择要修改反潜回";
			this.checkBox22.UseVisualStyleBackColor = false;
			this.checkBox21.AutoSize = true;
			this.checkBox21.BackColor = Color.Red;
			this.checkBox21.Location = new Point(15, 40);
			this.checkBox21.Name = "checkBox21";
			this.checkBox21.Size = new Size(126, 16);
			this.checkBox21.TabIndex = 34;
			this.checkBox21.Text = "21_选择要修改互锁";
			this.checkBox21.UseVisualStyleBackColor = false;
			this.groupBox1.Controls.Add(this.radioButton5);
			this.groupBox1.Controls.Add(this.radioButton4);
			this.groupBox1.Controls.Add(this.radioButton3);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new Point(15, 197);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(536, 36);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.radioButton5.AutoSize = true;
			this.radioButton5.Location = new Point(420, 14);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new Size(101, 16);
			this.radioButton5.TabIndex = 4;
			this.radioButton5.Text = "5[1与234反潜]";
			this.radioButton5.UseVisualStyleBackColor = true;
			this.radioButton4.AutoSize = true;
			this.radioButton4.Location = new Point(301, 14);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new Size(113, 16);
			this.radioButton4.TabIndex = 3;
			this.radioButton4.Text = "4[1与2,3互反潜]";
			this.radioButton4.UseVisualStyleBackColor = true;
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new Point(200, 14);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new Size(95, 16);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.Text = "3[13/24反潜]";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new Point(87, 14);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(107, 16);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "2[1/2,3/4反潜]";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new Point(6, 14);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(77, 16);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "1[不反潜]";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.checkBox19, 3, 4);
			this.tableLayoutPanel1.Controls.Add(this.checkBox20, 4, 4);
			this.tableLayoutPanel1.Controls.Add(this.checkBox18, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.checkBox16, 4, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkBox17, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.checkBox12, 4, 2);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkBox15, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.checkBox11, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.checkBox14, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkBox5, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.checkBox9, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.checkBox7, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.checkBox13, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkBox10, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.checkBox6, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.checkBox8, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.label8, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label9, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label10, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label11, 4, 0);
			this.tableLayoutPanel1.Location = new Point(15, 65);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.Size = new Size(537, 110);
			this.tableLayoutPanel1.TabIndex = 31;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(3, 88);
			this.label7.Name = "label7";
			this.label7.Size = new Size(83, 12);
			this.label7.TabIndex = 26;
			this.label7.Text = "4号门开的条件";
			this.checkBox19.AutoSize = true;
			this.checkBox19.Location = new Point(324, 91);
			this.checkBox19.Name = "checkBox19";
			this.checkBox19.Size = new Size(84, 16);
			this.checkBox19.TabIndex = 28;
			this.checkBox19.Text = "19-3号门闭";
			this.checkBox19.UseVisualStyleBackColor = true;
			this.checkBox20.AutoSize = true;
			this.checkBox20.Location = new Point(431, 91);
			this.checkBox20.Name = "checkBox20";
			this.checkBox20.Size = new Size(84, 16);
			this.checkBox20.TabIndex = 27;
			this.checkBox20.Text = "20_4号门闭";
			this.checkBox20.UseVisualStyleBackColor = true;
			this.checkBox18.AutoSize = true;
			this.checkBox18.Location = new Point(217, 91);
			this.checkBox18.Name = "checkBox18";
			this.checkBox18.Size = new Size(84, 16);
			this.checkBox18.TabIndex = 29;
			this.checkBox18.Text = "18-2号门闭";
			this.checkBox18.UseVisualStyleBackColor = true;
			this.checkBox16.AutoSize = true;
			this.checkBox16.Location = new Point(431, 69);
			this.checkBox16.Name = "checkBox16";
			this.checkBox16.Size = new Size(84, 16);
			this.checkBox16.TabIndex = 22;
			this.checkBox16.Text = "16_4号门闭";
			this.checkBox16.UseVisualStyleBackColor = true;
			this.checkBox17.AutoSize = true;
			this.checkBox17.Location = new Point(110, 91);
			this.checkBox17.Name = "checkBox17";
			this.checkBox17.Size = new Size(84, 16);
			this.checkBox17.TabIndex = 30;
			this.checkBox17.Text = "17-1号门闭";
			this.checkBox17.UseVisualStyleBackColor = true;
			this.checkBox12.AutoSize = true;
			this.checkBox12.Location = new Point(431, 47);
			this.checkBox12.Name = "checkBox12";
			this.checkBox12.Size = new Size(84, 16);
			this.checkBox12.TabIndex = 17;
			this.checkBox12.Text = "12_4号门闭";
			this.checkBox12.UseVisualStyleBackColor = true;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(3, 66);
			this.label6.Name = "label6";
			this.label6.Size = new Size(83, 12);
			this.label6.TabIndex = 21;
			this.label6.Text = "3号门开的条件";
			this.checkBox15.AutoSize = true;
			this.checkBox15.Location = new Point(324, 69);
			this.checkBox15.Name = "checkBox15";
			this.checkBox15.Size = new Size(84, 16);
			this.checkBox15.TabIndex = 23;
			this.checkBox15.Text = "15-3号门闭";
			this.checkBox15.UseVisualStyleBackColor = true;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(3, 44);
			this.label5.Name = "label5";
			this.label5.Size = new Size(83, 12);
			this.label5.TabIndex = 16;
			this.label5.Text = "2号门开的条件";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(3, 22);
			this.label4.Name = "label4";
			this.label4.Size = new Size(83, 12);
			this.label4.TabIndex = 11;
			this.label4.Text = "1号门开的条件";
			this.checkBox11.AutoSize = true;
			this.checkBox11.Location = new Point(324, 47);
			this.checkBox11.Name = "checkBox11";
			this.checkBox11.Size = new Size(84, 16);
			this.checkBox11.TabIndex = 18;
			this.checkBox11.Text = "11-3号门闭";
			this.checkBox11.UseVisualStyleBackColor = true;
			this.checkBox14.AutoSize = true;
			this.checkBox14.Location = new Point(217, 69);
			this.checkBox14.Name = "checkBox14";
			this.checkBox14.Size = new Size(84, 16);
			this.checkBox14.TabIndex = 24;
			this.checkBox14.Text = "14-2号门闭";
			this.checkBox14.UseVisualStyleBackColor = true;
			this.checkBox5.AutoSize = true;
			this.checkBox5.Location = new Point(110, 25);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new Size(78, 16);
			this.checkBox5.TabIndex = 12;
			this.checkBox5.Text = "5_1号门闭";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox9.AutoSize = true;
			this.checkBox9.Location = new Point(110, 47);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new Size(78, 16);
			this.checkBox9.TabIndex = 20;
			this.checkBox9.Text = "9-1号门闭";
			this.checkBox9.UseVisualStyleBackColor = true;
			this.checkBox7.AutoSize = true;
			this.checkBox7.Location = new Point(324, 25);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new Size(78, 16);
			this.checkBox7.TabIndex = 14;
			this.checkBox7.Text = "7-3号门闭";
			this.checkBox7.UseVisualStyleBackColor = true;
			this.checkBox13.AutoSize = true;
			this.checkBox13.Location = new Point(110, 69);
			this.checkBox13.Name = "checkBox13";
			this.checkBox13.Size = new Size(84, 16);
			this.checkBox13.TabIndex = 25;
			this.checkBox13.Text = "13-1号门闭";
			this.checkBox13.UseVisualStyleBackColor = true;
			this.checkBox10.AutoSize = true;
			this.checkBox10.Location = new Point(217, 47);
			this.checkBox10.Name = "checkBox10";
			this.checkBox10.Size = new Size(84, 16);
			this.checkBox10.TabIndex = 19;
			this.checkBox10.Text = "10-2号门闭";
			this.checkBox10.UseVisualStyleBackColor = true;
			this.checkBox6.AutoSize = true;
			this.checkBox6.Location = new Point(217, 25);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new Size(78, 16);
			this.checkBox6.TabIndex = 13;
			this.checkBox6.Text = "6-2号门闭";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new Point(431, 25);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new Size(78, 16);
			this.checkBox8.TabIndex = 15;
			this.checkBox8.Text = "8-4号门闭";
			this.checkBox8.UseVisualStyleBackColor = true;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(110, 0);
			this.label8.Name = "label8";
			this.label8.Size = new Size(47, 12);
			this.label8.TabIndex = 31;
			this.label8.Text = "1号门磁";
			this.label9.AutoSize = true;
			this.label9.Location = new Point(217, 0);
			this.label9.Name = "label9";
			this.label9.Size = new Size(47, 12);
			this.label9.TabIndex = 32;
			this.label9.Text = "2号门磁";
			this.label10.AutoSize = true;
			this.label10.Location = new Point(324, 0);
			this.label10.Name = "label10";
			this.label10.Size = new Size(47, 12);
			this.label10.TabIndex = 33;
			this.label10.Text = "3号门磁";
			this.label11.AutoSize = true;
			this.label11.Location = new Point(431, 0);
			this.label11.Name = "label11";
			this.label11.Size = new Size(47, 12);
			this.label11.TabIndex = 34;
			this.label11.Text = "4号门磁";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(157, 41);
			this.label3.Name = "label3";
			this.label3.Size = new Size(317, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "互锁(要同时满足打勾的约束才行; 无约束则按正常进行)：";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(170, 8);
			this.label2.Name = "label2";
			this.label2.Size = new Size(89, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "密码键盘的启用";
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new Point(434, 7);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new Size(42, 16);
			this.checkBox4.TabIndex = 8;
			this.checkBox4.Text = "4_4";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new Point(383, 7);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new Size(42, 16);
			this.checkBox3.TabIndex = 7;
			this.checkBox3.Text = "3_3";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new Point(332, 7);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new Size(42, 16);
			this.checkBox2.TabIndex = 6;
			this.checkBox2.Text = "2_2";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new Point(281, 7);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(42, 16);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "1_1";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.tabPage24.Controls.Add(this.groupBox19);
			this.tabPage24.Controls.Add(this.checkBox138);
			this.tabPage24.Controls.Add(this.groupBox17);
			this.tabPage24.Controls.Add(this.groupBox18);
			this.tabPage24.Controls.Add(this.checkBox137);
			this.tabPage24.Controls.Add(this.groupBox16);
			this.tabPage24.Controls.Add(this.groupBox15);
			this.tabPage24.Controls.Add(this.checkBox136);
			this.tabPage24.Location = new Point(4, 76);
			this.tabPage24.Name = "tabPage24";
			this.tabPage24.Padding = new Padding(3);
			this.tabPage24.Size = new Size(563, 628);
			this.tabPage24.TabIndex = 23;
			this.tabPage24.Text = "扩展参数2";
			this.tabPage24.UseVisualStyleBackColor = true;
			this.groupBox19.Controls.Add(this.numericUpDown32);
			this.groupBox19.Controls.Add(this.radioButton38);
			this.groupBox19.Controls.Add(this.radioButton39);
			this.groupBox19.Location = new Point(55, 537);
			this.groupBox19.Name = "groupBox19";
			this.groupBox19.Size = new Size(379, 70);
			this.groupBox19.TabIndex = 111;
			this.groupBox19.TabStop = false;
			this.groupBox19.Text = "16_发送反潜回信号";
			this.numericUpDown32.Location = new Point(172, 42);
			NumericUpDown arg_732A_0 = this.numericUpDown32;
			int[] array9 = new int[4];
			array9[0] = 254;
			arg_732A_0.Maximum = new decimal(array9);
			NumericUpDown arg_7349_0 = this.numericUpDown32;
			int[] array10 = new int[4];
			array10[0] = 1;
			arg_7349_0.Minimum = new decimal(array10);
			this.numericUpDown32.Name = "numericUpDown32";
			this.numericUpDown32.Size = new Size(48, 21);
			this.numericUpDown32.TabIndex = 14;
			NumericUpDown arg_7399_0 = this.numericUpDown32;
			int[] array11 = new int[4];
			array11[0] = 1;
			arg_7399_0.Value = new decimal(array11);
			this.radioButton38.AutoSize = true;
			this.radioButton38.Checked = true;
			this.radioButton38.Location = new Point(15, 20);
			this.radioButton38.Name = "radioButton38";
			this.radioButton38.Size = new Size(77, 16);
			this.radioButton38.TabIndex = 13;
			this.radioButton38.TabStop = true;
			this.radioButton38.Text = "38_不发送";
			this.radioButton38.UseVisualStyleBackColor = true;
			this.radioButton39.AutoSize = true;
			this.radioButton39.Location = new Point(15, 42);
			this.radioButton39.Name = "radioButton39";
			this.radioButton39.Size = new Size(137, 16);
			this.radioButton39.TabIndex = 12;
			this.radioButton39.Text = "39_发送_所在的组_32";
			this.radioButton39.UseVisualStyleBackColor = true;
			this.checkBox138.AutoSize = true;
			this.checkBox138.BackColor = Color.Red;
			this.checkBox138.ImeMode = ImeMode.NoControl;
			this.checkBox138.Location = new Point(9, 512);
			this.checkBox138.Name = "checkBox138";
			this.checkBox138.Size = new Size(354, 16);
			this.checkBox138.TabIndex = 110;
			this.checkBox138.Text = "138_选择要 反潜回通信设置 [V5.25以上 2012-7-1_11:13:38]";
			this.checkBox138.UseVisualStyleBackColor = false;
			this.groupBox17.Controls.Add(this.numericUpDown30);
			this.groupBox17.Controls.Add(this.radioButton34);
			this.groupBox17.Controls.Add(this.radioButton35);
			this.groupBox17.Location = new Point(55, 436);
			this.groupBox17.Name = "groupBox17";
			this.groupBox17.Size = new Size(379, 70);
			this.groupBox17.TabIndex = 109;
			this.groupBox17.TabStop = false;
			this.groupBox17.Text = "16_发送互锁 强制锁信号(每2秒发一次)";
			this.numericUpDown30.Location = new Point(172, 42);
			NumericUpDown arg_7600_0 = this.numericUpDown30;
			int[] array12 = new int[4];
			array12[0] = 254;
			arg_7600_0.Maximum = new decimal(array12);
			NumericUpDown arg_761F_0 = this.numericUpDown30;
			int[] array13 = new int[4];
			array13[0] = 1;
			arg_761F_0.Minimum = new decimal(array13);
			this.numericUpDown30.Name = "numericUpDown30";
			this.numericUpDown30.Size = new Size(48, 21);
			this.numericUpDown30.TabIndex = 14;
			NumericUpDown arg_766F_0 = this.numericUpDown30;
			int[] array14 = new int[4];
			array14[0] = 1;
			arg_766F_0.Value = new decimal(array14);
			this.radioButton34.AutoSize = true;
			this.radioButton34.Checked = true;
			this.radioButton34.Location = new Point(15, 20);
			this.radioButton34.Name = "radioButton34";
			this.radioButton34.Size = new Size(77, 16);
			this.radioButton34.TabIndex = 13;
			this.radioButton34.TabStop = true;
			this.radioButton34.Text = "34_不发送";
			this.radioButton34.UseVisualStyleBackColor = true;
			this.radioButton35.AutoSize = true;
			this.radioButton35.Location = new Point(15, 42);
			this.radioButton35.Name = "radioButton35";
			this.radioButton35.Size = new Size(137, 16);
			this.radioButton35.TabIndex = 12;
			this.radioButton35.Text = "35_发送_所在的组_30";
			this.radioButton35.UseVisualStyleBackColor = true;
			this.groupBox18.Controls.Add(this.label159);
			this.groupBox18.Controls.Add(this.radioButton36);
			this.groupBox18.Controls.Add(this.radioButton37);
			this.groupBox18.Controls.Add(this.numericUpDown31);
			this.groupBox18.Location = new Point(55, 298);
			this.groupBox18.Name = "groupBox18";
			this.groupBox18.Size = new Size(379, 121);
			this.groupBox18.TabIndex = 108;
			this.groupBox18.TabStop = false;
			this.groupBox18.Text = "15_其他控制器 互锁 强制锁信号";
			this.label159.AutoSize = true;
			this.label159.ImeMode = ImeMode.NoControl;
			this.label159.Location = new Point(150, 91);
			this.label159.Name = "label159";
			this.label159.Size = new Size(17, 12);
			this.label159.TabIndex = 105;
			this.label159.Text = "秒";
			this.radioButton36.AutoSize = true;
			this.radioButton36.Checked = true;
			this.radioButton36.Location = new Point(15, 20);
			this.radioButton36.Name = "radioButton36";
			this.radioButton36.Size = new Size(77, 16);
			this.radioButton36.TabIndex = 12;
			this.radioButton36.TabStop = true;
			this.radioButton36.Text = "36_不接收";
			this.radioButton36.UseVisualStyleBackColor = true;
			this.radioButton37.AutoSize = true;
			this.radioButton37.Location = new Point(15, 42);
			this.radioButton37.Name = "radioButton37";
			this.radioButton37.Size = new Size(233, 28);
			this.radioButton37.TabIndex = 11;
			this.radioButton37.Text = "37_接收到_门不开至少延时\r\n(如果信号一直存在, 则一直强制锁)_31";
			this.radioButton37.UseVisualStyleBackColor = true;
			this.numericUpDown31.Location = new Point(66, 89);
			NumericUpDown arg_79BF_0 = this.numericUpDown31;
			int[] array15 = new int[4];
			array15[0] = 250;
			arg_79BF_0.Maximum = new decimal(array15);
			NumericUpDown arg_79DE_0 = this.numericUpDown31;
			int[] array16 = new int[4];
			array16[0] = 3;
			arg_79DE_0.Minimum = new decimal(array16);
			this.numericUpDown31.Name = "numericUpDown31";
			this.numericUpDown31.Size = new Size(68, 21);
			this.numericUpDown31.TabIndex = 9;
			NumericUpDown arg_7A2E_0 = this.numericUpDown31;
			int[] array17 = new int[4];
			array17[0] = 5;
			arg_7A2E_0.Value = new decimal(array17);
			this.checkBox137.AutoSize = true;
			this.checkBox137.BackColor = Color.Red;
			this.checkBox137.ImeMode = ImeMode.NoControl;
			this.checkBox137.Location = new Point(9, 276);
			this.checkBox137.Name = "checkBox137";
			this.checkBox137.Size = new Size(348, 16);
			this.checkBox137.TabIndex = 107;
			this.checkBox137.Text = "137_选择要 互锁通信设置 [V5.25以上 2012-6-15_13:08:05]";
			this.checkBox137.UseVisualStyleBackColor = false;
			this.groupBox16.Controls.Add(this.numericUpDown29);
			this.groupBox16.Controls.Add(this.radioButton32);
			this.groupBox16.Controls.Add(this.radioButton33);
			this.groupBox16.Location = new Point(55, 180);
			this.groupBox16.Name = "groupBox16";
			this.groupBox16.Size = new Size(379, 70);
			this.groupBox16.TabIndex = 106;
			this.groupBox16.TabStop = false;
			this.groupBox16.Text = "16_发送火警信息包(每2秒发一次)";
			this.numericUpDown29.Location = new Point(152, 42);
			NumericUpDown arg_7BA0_0 = this.numericUpDown29;
			int[] array18 = new int[4];
			array18[0] = 254;
			arg_7BA0_0.Maximum = new decimal(array18);
			NumericUpDown arg_7BBF_0 = this.numericUpDown29;
			int[] array19 = new int[4];
			array19[0] = 1;
			arg_7BBF_0.Minimum = new decimal(array19);
			this.numericUpDown29.Name = "numericUpDown29";
			this.numericUpDown29.Size = new Size(68, 21);
			this.numericUpDown29.TabIndex = 14;
			NumericUpDown arg_7C0F_0 = this.numericUpDown29;
			int[] array20 = new int[4];
			array20[0] = 1;
			arg_7C0F_0.Value = new decimal(array20);
			this.radioButton32.AutoSize = true;
			this.radioButton32.Checked = true;
			this.radioButton32.Location = new Point(15, 20);
			this.radioButton32.Name = "radioButton32";
			this.radioButton32.Size = new Size(77, 16);
			this.radioButton32.TabIndex = 13;
			this.radioButton32.TabStop = true;
			this.radioButton32.Text = "32_不发送";
			this.radioButton32.UseVisualStyleBackColor = true;
			this.radioButton33.AutoSize = true;
			this.radioButton33.Location = new Point(15, 42);
			this.radioButton33.Name = "radioButton33";
			this.radioButton33.Size = new Size(119, 16);
			this.radioButton33.TabIndex = 12;
			this.radioButton33.Text = "33_发送_所在的组";
			this.radioButton33.UseVisualStyleBackColor = true;
			this.groupBox15.Controls.Add(this.label161);
			this.groupBox15.Controls.Add(this.radioButton31);
			this.groupBox15.Controls.Add(this.radioButton30);
			this.groupBox15.Controls.Add(this.radioButton29);
			this.groupBox15.Controls.Add(this.numericUpDown28);
			this.groupBox15.Location = new Point(55, 42);
			this.groupBox15.Name = "groupBox15";
			this.groupBox15.Size = new Size(379, 121);
			this.groupBox15.TabIndex = 105;
			this.groupBox15.TabStop = false;
			this.groupBox15.Text = "15_其他控制器火警信号";
			this.label161.AutoSize = true;
			this.label161.ImeMode = ImeMode.NoControl;
			this.label161.Location = new Point(150, 91);
			this.label161.Name = "label161";
			this.label161.Size = new Size(17, 12);
			this.label161.TabIndex = 105;
			this.label161.Text = "秒";
			this.radioButton31.AutoSize = true;
			this.radioButton31.Location = new Point(15, 67);
			this.radioButton31.Name = "radioButton31";
			this.radioButton31.Size = new Size(353, 16);
			this.radioButton31.TabIndex = 12;
			this.radioButton31.Text = "31_接收到_开门至少延时(如果火警一直存在, 则一直打开)_28";
			this.radioButton31.UseVisualStyleBackColor = true;
			this.radioButton30.AutoSize = true;
			this.radioButton30.Location = new Point(15, 42);
			this.radioButton30.Name = "radioButton30";
			this.radioButton30.Size = new Size(227, 16);
			this.radioButton30.TabIndex = 11;
			this.radioButton30.Text = "30_接收到_门常开, 直到软件更新设置";
			this.radioButton30.UseVisualStyleBackColor = true;
			this.radioButton29.AutoSize = true;
			this.radioButton29.Checked = true;
			this.radioButton29.Location = new Point(15, 20);
			this.radioButton29.Name = "radioButton29";
			this.radioButton29.Size = new Size(77, 16);
			this.radioButton29.TabIndex = 10;
			this.radioButton29.TabStop = true;
			this.radioButton29.Text = "29_不接收";
			this.radioButton29.UseVisualStyleBackColor = true;
			NumericUpDown arg_7FC7_0 = this.numericUpDown28;
			int[] array21 = new int[4];
			array21[0] = 5;
			arg_7FC7_0.Increment = new decimal(array21);
			this.numericUpDown28.Location = new Point(66, 89);
			NumericUpDown arg_7FFE_0 = this.numericUpDown28;
			int[] array22 = new int[4];
			array22[0] = 1280;
			arg_7FFE_0.Maximum = new decimal(array22);
			NumericUpDown arg_801E_0 = this.numericUpDown28;
			int[] array23 = new int[4];
			array23[0] = 15;
			arg_801E_0.Minimum = new decimal(array23);
			this.numericUpDown28.Name = "numericUpDown28";
			this.numericUpDown28.Size = new Size(68, 21);
			this.numericUpDown28.TabIndex = 9;
			NumericUpDown arg_806F_0 = this.numericUpDown28;
			int[] array24 = new int[4];
			array24[0] = 15;
			arg_806F_0.Value = new decimal(array24);
			this.numericUpDown28.ValueChanged += new EventHandler(this.numericUpDown28_ValueChanged);
			this.checkBox136.AutoSize = true;
			this.checkBox136.BackColor = Color.Red;
			this.checkBox136.ImeMode = ImeMode.NoControl;
			this.checkBox136.Location = new Point(9, 20);
			this.checkBox136.Name = "checkBox136";
			this.checkBox136.Size = new Size(348, 16);
			this.checkBox136.TabIndex = 100;
			this.checkBox136.Text = "136_选择要 火警通信设置 [V5.25以上 2012-6-15_13:08:05]";
			this.checkBox136.UseVisualStyleBackColor = false;
			this.tabPage2.Controls.Add(this.button85);
			this.tabPage2.Controls.Add(this.checkBox171);
			this.tabPage2.Controls.Add(this.checkBox172);
			this.tabPage2.Controls.Add(this.checkBox173);
			this.tabPage2.Controls.Add(this.checkBox174);
			this.tabPage2.Controls.Add(this.checkBox175);
			this.tabPage2.Controls.Add(this.checkBox176);
			this.tabPage2.Controls.Add(this.checkBox177);
			this.tabPage2.Controls.Add(this.checkBox178);
			this.tabPage2.Controls.Add(this.checkBox179);
			this.tabPage2.Controls.Add(this.checkBox180);
			this.tabPage2.Controls.Add(this.checkBox161);
			this.tabPage2.Controls.Add(this.checkBox162);
			this.tabPage2.Controls.Add(this.checkBox163);
			this.tabPage2.Controls.Add(this.checkBox164);
			this.tabPage2.Controls.Add(this.checkBox165);
			this.tabPage2.Controls.Add(this.checkBox166);
			this.tabPage2.Controls.Add(this.checkBox167);
			this.tabPage2.Controls.Add(this.checkBox168);
			this.tabPage2.Controls.Add(this.checkBox169);
			this.tabPage2.Controls.Add(this.checkBox170);
			this.tabPage2.Controls.Add(this.checkBox151);
			this.tabPage2.Controls.Add(this.checkBox152);
			this.tabPage2.Controls.Add(this.checkBox153);
			this.tabPage2.Controls.Add(this.checkBox154);
			this.tabPage2.Controls.Add(this.checkBox155);
			this.tabPage2.Controls.Add(this.checkBox156);
			this.tabPage2.Controls.Add(this.checkBox157);
			this.tabPage2.Controls.Add(this.checkBox158);
			this.tabPage2.Controls.Add(this.checkBox159);
			this.tabPage2.Controls.Add(this.checkBox160);
			this.tabPage2.Controls.Add(this.checkBox150);
			this.tabPage2.Controls.Add(this.checkBox149);
			this.tabPage2.Controls.Add(this.checkBox148);
			this.tabPage2.Controls.Add(this.checkBox147);
			this.tabPage2.Controls.Add(this.checkBox146);
			this.tabPage2.Controls.Add(this.checkBox145);
			this.tabPage2.Controls.Add(this.checkBox144);
			this.tabPage2.Controls.Add(this.checkBox143);
			this.tabPage2.Controls.Add(this.checkBox142);
			this.tabPage2.Controls.Add(this.checkBox141);
			this.tabPage2.Controls.Add(this.checkBox129);
			this.tabPage2.Controls.Add(this.dataGridView1);
			this.tabPage2.Controls.Add(this.button68);
			this.tabPage2.Controls.Add(this.checkBox128);
			this.tabPage2.Controls.Add(this.label131);
			this.tabPage2.Controls.Add(this.groupBox12);
			this.tabPage2.Controls.Add(this.numericUpDown15);
			this.tabPage2.Controls.Add(this.checkBox111);
			this.tabPage2.Controls.Add(this.button53);
			this.tabPage2.Controls.Add(this.button17);
			this.tabPage2.Controls.Add(this.checkBox97);
			this.tabPage2.Controls.Add(this.button4);
			this.tabPage2.Controls.Add(this.dtpDeactivate);
			this.tabPage2.Controls.Add(this.dtpActivate);
			this.tabPage2.Controls.Add(this.label121);
			this.tabPage2.Controls.Add(this.label23);
			this.tabPage2.Controls.Add(this.label22);
			this.tabPage2.Controls.Add(this.txtPassword);
			this.tabPage2.Controls.Add(this.label21);
			this.tabPage2.Controls.Add(this.checkBox25);
			this.tabPage2.Controls.Add(this.checkBox24);
			this.tabPage2.Controls.Add(this.tableLayoutPanel2);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.txtCardNO);
			this.tabPage2.Location = new Point(4, 76);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(563, 628);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "卡权限";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.button85.ImeMode = ImeMode.NoControl;
			this.button85.Location = new Point(23, 594);
			this.button85.Name = "button85";
			this.button85.Size = new Size(117, 23);
			this.button85.TabIndex = 82;
			this.button85.Text = "85_读所有权限10次";
			this.button85.UseVisualStyleBackColor = true;
			this.button85.Click += new EventHandler(this.button85_Click);
			this.checkBox171.AutoSize = true;
			this.checkBox171.Location = new Point(56, 389);
			this.checkBox171.Name = "checkBox171";
			this.checkBox171.Size = new Size(36, 16);
			this.checkBox171.TabIndex = 81;
			this.checkBox171.Text = "31";
			this.checkBox171.UseVisualStyleBackColor = true;
			this.checkBox172.AutoSize = true;
			this.checkBox172.Location = new Point(99, 389);
			this.checkBox172.Name = "checkBox172";
			this.checkBox172.Size = new Size(36, 16);
			this.checkBox172.TabIndex = 80;
			this.checkBox172.Text = "32";
			this.checkBox172.UseVisualStyleBackColor = true;
			this.checkBox173.AutoSize = true;
			this.checkBox173.Location = new Point(142, 389);
			this.checkBox173.Name = "checkBox173";
			this.checkBox173.Size = new Size(36, 16);
			this.checkBox173.TabIndex = 79;
			this.checkBox173.Text = "33";
			this.checkBox173.UseVisualStyleBackColor = true;
			this.checkBox174.AutoSize = true;
			this.checkBox174.Location = new Point(185, 389);
			this.checkBox174.Name = "checkBox174";
			this.checkBox174.Size = new Size(36, 16);
			this.checkBox174.TabIndex = 78;
			this.checkBox174.Text = "34";
			this.checkBox174.UseVisualStyleBackColor = true;
			this.checkBox175.AutoSize = true;
			this.checkBox175.Location = new Point(228, 389);
			this.checkBox175.Name = "checkBox175";
			this.checkBox175.Size = new Size(36, 16);
			this.checkBox175.TabIndex = 77;
			this.checkBox175.Text = "35";
			this.checkBox175.UseVisualStyleBackColor = true;
			this.checkBox176.AutoSize = true;
			this.checkBox176.Location = new Point(271, 389);
			this.checkBox176.Name = "checkBox176";
			this.checkBox176.Size = new Size(36, 16);
			this.checkBox176.TabIndex = 76;
			this.checkBox176.Text = "36";
			this.checkBox176.UseVisualStyleBackColor = true;
			this.checkBox177.AutoSize = true;
			this.checkBox177.Location = new Point(314, 389);
			this.checkBox177.Name = "checkBox177";
			this.checkBox177.Size = new Size(36, 16);
			this.checkBox177.TabIndex = 75;
			this.checkBox177.Text = "37";
			this.checkBox177.UseVisualStyleBackColor = true;
			this.checkBox178.AutoSize = true;
			this.checkBox178.Location = new Point(357, 389);
			this.checkBox178.Name = "checkBox178";
			this.checkBox178.Size = new Size(36, 16);
			this.checkBox178.TabIndex = 74;
			this.checkBox178.Text = "38";
			this.checkBox178.UseVisualStyleBackColor = true;
			this.checkBox179.AutoSize = true;
			this.checkBox179.Location = new Point(400, 389);
			this.checkBox179.Name = "checkBox179";
			this.checkBox179.Size = new Size(36, 16);
			this.checkBox179.TabIndex = 73;
			this.checkBox179.Text = "39";
			this.checkBox179.UseVisualStyleBackColor = true;
			this.checkBox180.AutoSize = true;
			this.checkBox180.Location = new Point(443, 389);
			this.checkBox180.Name = "checkBox180";
			this.checkBox180.Size = new Size(36, 16);
			this.checkBox180.TabIndex = 72;
			this.checkBox180.Text = "40";
			this.checkBox180.UseVisualStyleBackColor = true;
			this.checkBox161.AutoSize = true;
			this.checkBox161.Location = new Point(56, 367);
			this.checkBox161.Name = "checkBox161";
			this.checkBox161.Size = new Size(36, 16);
			this.checkBox161.TabIndex = 71;
			this.checkBox161.Text = "21";
			this.checkBox161.UseVisualStyleBackColor = true;
			this.checkBox162.AutoSize = true;
			this.checkBox162.Location = new Point(99, 367);
			this.checkBox162.Name = "checkBox162";
			this.checkBox162.Size = new Size(36, 16);
			this.checkBox162.TabIndex = 70;
			this.checkBox162.Text = "22";
			this.checkBox162.UseVisualStyleBackColor = true;
			this.checkBox163.AutoSize = true;
			this.checkBox163.Location = new Point(142, 367);
			this.checkBox163.Name = "checkBox163";
			this.checkBox163.Size = new Size(36, 16);
			this.checkBox163.TabIndex = 69;
			this.checkBox163.Text = "23";
			this.checkBox163.UseVisualStyleBackColor = true;
			this.checkBox164.AutoSize = true;
			this.checkBox164.Location = new Point(185, 367);
			this.checkBox164.Name = "checkBox164";
			this.checkBox164.Size = new Size(36, 16);
			this.checkBox164.TabIndex = 68;
			this.checkBox164.Text = "24";
			this.checkBox164.UseVisualStyleBackColor = true;
			this.checkBox165.AutoSize = true;
			this.checkBox165.Location = new Point(228, 367);
			this.checkBox165.Name = "checkBox165";
			this.checkBox165.Size = new Size(36, 16);
			this.checkBox165.TabIndex = 67;
			this.checkBox165.Text = "25";
			this.checkBox165.UseVisualStyleBackColor = true;
			this.checkBox166.AutoSize = true;
			this.checkBox166.Location = new Point(271, 367);
			this.checkBox166.Name = "checkBox166";
			this.checkBox166.Size = new Size(36, 16);
			this.checkBox166.TabIndex = 66;
			this.checkBox166.Text = "26";
			this.checkBox166.UseVisualStyleBackColor = true;
			this.checkBox167.AutoSize = true;
			this.checkBox167.Location = new Point(314, 367);
			this.checkBox167.Name = "checkBox167";
			this.checkBox167.Size = new Size(36, 16);
			this.checkBox167.TabIndex = 65;
			this.checkBox167.Text = "27";
			this.checkBox167.UseVisualStyleBackColor = true;
			this.checkBox168.AutoSize = true;
			this.checkBox168.Location = new Point(357, 367);
			this.checkBox168.Name = "checkBox168";
			this.checkBox168.Size = new Size(36, 16);
			this.checkBox168.TabIndex = 64;
			this.checkBox168.Text = "28";
			this.checkBox168.UseVisualStyleBackColor = true;
			this.checkBox169.AutoSize = true;
			this.checkBox169.Location = new Point(400, 367);
			this.checkBox169.Name = "checkBox169";
			this.checkBox169.Size = new Size(36, 16);
			this.checkBox169.TabIndex = 63;
			this.checkBox169.Text = "29";
			this.checkBox169.UseVisualStyleBackColor = true;
			this.checkBox170.AutoSize = true;
			this.checkBox170.Location = new Point(443, 367);
			this.checkBox170.Name = "checkBox170";
			this.checkBox170.Size = new Size(36, 16);
			this.checkBox170.TabIndex = 62;
			this.checkBox170.Text = "30";
			this.checkBox170.UseVisualStyleBackColor = true;
			this.checkBox151.AutoSize = true;
			this.checkBox151.Location = new Point(56, 345);
			this.checkBox151.Name = "checkBox151";
			this.checkBox151.Size = new Size(36, 16);
			this.checkBox151.TabIndex = 61;
			this.checkBox151.Text = "11";
			this.checkBox151.UseVisualStyleBackColor = true;
			this.checkBox152.AutoSize = true;
			this.checkBox152.Location = new Point(99, 345);
			this.checkBox152.Name = "checkBox152";
			this.checkBox152.Size = new Size(36, 16);
			this.checkBox152.TabIndex = 60;
			this.checkBox152.Text = "12";
			this.checkBox152.UseVisualStyleBackColor = true;
			this.checkBox153.AutoSize = true;
			this.checkBox153.Location = new Point(142, 345);
			this.checkBox153.Name = "checkBox153";
			this.checkBox153.Size = new Size(36, 16);
			this.checkBox153.TabIndex = 59;
			this.checkBox153.Text = "13";
			this.checkBox153.UseVisualStyleBackColor = true;
			this.checkBox154.AutoSize = true;
			this.checkBox154.Location = new Point(185, 345);
			this.checkBox154.Name = "checkBox154";
			this.checkBox154.Size = new Size(36, 16);
			this.checkBox154.TabIndex = 58;
			this.checkBox154.Text = "14";
			this.checkBox154.UseVisualStyleBackColor = true;
			this.checkBox155.AutoSize = true;
			this.checkBox155.Location = new Point(228, 345);
			this.checkBox155.Name = "checkBox155";
			this.checkBox155.Size = new Size(36, 16);
			this.checkBox155.TabIndex = 57;
			this.checkBox155.Text = "15";
			this.checkBox155.UseVisualStyleBackColor = true;
			this.checkBox156.AutoSize = true;
			this.checkBox156.Location = new Point(271, 345);
			this.checkBox156.Name = "checkBox156";
			this.checkBox156.Size = new Size(36, 16);
			this.checkBox156.TabIndex = 56;
			this.checkBox156.Text = "16";
			this.checkBox156.UseVisualStyleBackColor = true;
			this.checkBox157.AutoSize = true;
			this.checkBox157.Location = new Point(314, 345);
			this.checkBox157.Name = "checkBox157";
			this.checkBox157.Size = new Size(36, 16);
			this.checkBox157.TabIndex = 55;
			this.checkBox157.Text = "17";
			this.checkBox157.UseVisualStyleBackColor = true;
			this.checkBox158.AutoSize = true;
			this.checkBox158.Location = new Point(357, 345);
			this.checkBox158.Name = "checkBox158";
			this.checkBox158.Size = new Size(36, 16);
			this.checkBox158.TabIndex = 54;
			this.checkBox158.Text = "18";
			this.checkBox158.UseVisualStyleBackColor = true;
			this.checkBox159.AutoSize = true;
			this.checkBox159.Location = new Point(400, 345);
			this.checkBox159.Name = "checkBox159";
			this.checkBox159.Size = new Size(36, 16);
			this.checkBox159.TabIndex = 53;
			this.checkBox159.Text = "19";
			this.checkBox159.UseVisualStyleBackColor = true;
			this.checkBox160.AutoSize = true;
			this.checkBox160.Location = new Point(443, 345);
			this.checkBox160.Name = "checkBox160";
			this.checkBox160.Size = new Size(36, 16);
			this.checkBox160.TabIndex = 52;
			this.checkBox160.Text = "20";
			this.checkBox160.UseVisualStyleBackColor = true;
			this.checkBox150.AutoSize = true;
			this.checkBox150.Location = new Point(443, 323);
			this.checkBox150.Name = "checkBox150";
			this.checkBox150.Size = new Size(36, 16);
			this.checkBox150.TabIndex = 43;
			this.checkBox150.Text = "10";
			this.checkBox150.UseVisualStyleBackColor = true;
			this.checkBox149.AutoSize = true;
			this.checkBox149.Location = new Point(400, 323);
			this.checkBox149.Name = "checkBox149";
			this.checkBox149.Size = new Size(30, 16);
			this.checkBox149.TabIndex = 42;
			this.checkBox149.Text = "9";
			this.checkBox149.UseVisualStyleBackColor = true;
			this.checkBox148.AutoSize = true;
			this.checkBox148.Location = new Point(357, 323);
			this.checkBox148.Name = "checkBox148";
			this.checkBox148.Size = new Size(30, 16);
			this.checkBox148.TabIndex = 41;
			this.checkBox148.Text = "8";
			this.checkBox148.UseVisualStyleBackColor = true;
			this.checkBox147.AutoSize = true;
			this.checkBox147.Location = new Point(314, 323);
			this.checkBox147.Name = "checkBox147";
			this.checkBox147.Size = new Size(30, 16);
			this.checkBox147.TabIndex = 40;
			this.checkBox147.Text = "7";
			this.checkBox147.UseVisualStyleBackColor = true;
			this.checkBox146.AutoSize = true;
			this.checkBox146.Location = new Point(271, 323);
			this.checkBox146.Name = "checkBox146";
			this.checkBox146.Size = new Size(30, 16);
			this.checkBox146.TabIndex = 39;
			this.checkBox146.Text = "6";
			this.checkBox146.UseVisualStyleBackColor = true;
			this.checkBox145.AutoSize = true;
			this.checkBox145.Location = new Point(228, 323);
			this.checkBox145.Name = "checkBox145";
			this.checkBox145.Size = new Size(30, 16);
			this.checkBox145.TabIndex = 38;
			this.checkBox145.Text = "5";
			this.checkBox145.UseVisualStyleBackColor = true;
			this.checkBox144.AutoSize = true;
			this.checkBox144.Location = new Point(185, 323);
			this.checkBox144.Name = "checkBox144";
			this.checkBox144.Size = new Size(30, 16);
			this.checkBox144.TabIndex = 37;
			this.checkBox144.Text = "4";
			this.checkBox144.UseVisualStyleBackColor = true;
			this.checkBox143.AutoSize = true;
			this.checkBox143.Location = new Point(142, 323);
			this.checkBox143.Name = "checkBox143";
			this.checkBox143.Size = new Size(30, 16);
			this.checkBox143.TabIndex = 36;
			this.checkBox143.Text = "3";
			this.checkBox143.UseVisualStyleBackColor = true;
			this.checkBox142.AutoSize = true;
			this.checkBox142.Location = new Point(99, 323);
			this.checkBox142.Name = "checkBox142";
			this.checkBox142.Size = new Size(30, 16);
			this.checkBox142.TabIndex = 35;
			this.checkBox142.Text = "2";
			this.checkBox142.UseVisualStyleBackColor = true;
			this.checkBox141.AutoSize = true;
			this.checkBox141.Location = new Point(56, 323);
			this.checkBox141.Name = "checkBox141";
			this.checkBox141.Size = new Size(30, 16);
			this.checkBox141.TabIndex = 34;
			this.checkBox141.Text = "1";
			this.checkBox141.UseVisualStyleBackColor = true;
			this.checkBox129.AutoSize = true;
			this.checkBox129.Location = new Point(311, 300);
			this.checkBox129.Name = "checkBox129";
			this.checkBox129.Size = new Size(108, 16);
			this.checkBox129.TabIndex = 25;
			this.checkBox129.Text = "129_强制为多层";
			this.checkBox129.UseVisualStyleBackColor = true;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new Point(205, 461);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new Size(345, 156);
			this.dataGridView1.TabIndex = 24;
			this.button68.Location = new Point(23, 556);
			this.button68.Name = "button68";
			this.button68.Size = new Size(117, 23);
			this.button68.TabIndex = 23;
			this.button68.Text = "68_取所有权限";
			this.button68.UseVisualStyleBackColor = true;
			this.button68.Click += new EventHandler(this.button68_Click);
			this.checkBox128.AutoSize = true;
			this.checkBox128.Location = new Point(18, 301);
			this.checkBox128.Name = "checkBox128";
			this.checkBox128.Size = new Size(138, 16);
			this.checkBox128.TabIndex = 22;
			this.checkBox128.Text = "128_要到楼层组合_31";
			this.checkBox128.UseVisualStyleBackColor = true;
			this.label131.AutoSize = true;
			this.label131.Location = new Point(159, 301);
			this.label131.Name = "label131";
			this.label131.Size = new Size(125, 12);
			this.label131.TabIndex = 20;
			this.label131.Text = "(只适合于电梯控制器)";
			this.groupBox12.Controls.Add(this.radioButton26);
			this.groupBox12.Controls.Add(this.radioButton24);
			this.groupBox12.Controls.Add(this.numericUpDown6);
			this.groupBox12.Controls.Add(this.label80);
			this.groupBox12.Controls.Add(this.dateTimePicker14);
			this.groupBox12.Location = new Point(211, 65);
			this.groupBox12.Name = "groupBox12";
			this.groupBox12.Size = new Size(339, 96);
			this.groupBox12.TabIndex = 19;
			this.groupBox12.TabStop = false;
			this.groupBox12.Text = "扩展字节部分";
			this.radioButton26.AutoSize = true;
			this.radioButton26.Location = new Point(11, 45);
			this.radioButton26.Name = "radioButton26";
			this.radioButton26.Size = new Size(119, 16);
			this.radioButton26.TabIndex = 18;
			this.radioButton26.Text = "26_截止时间点_14";
			this.radioButton26.UseVisualStyleBackColor = true;
			this.radioButton24.AutoSize = true;
			this.radioButton24.Checked = true;
			this.radioButton24.Location = new Point(11, 19);
			this.radioButton24.Name = "radioButton24";
			this.radioButton24.Size = new Size(269, 16);
			this.radioButton24.TabIndex = 17;
			this.radioButton24.TabStop = true;
			this.radioButton24.Text = "24_6_总次数限制[0表示不受限制, 最大510次]";
			this.radioButton24.UseVisualStyleBackColor = true;
			this.numericUpDown6.Location = new Point(280, 15);
			NumericUpDown arg_9DC0_0 = this.numericUpDown6;
			int[] array25 = new int[4];
			array25[0] = 510;
			arg_9DC0_0.Maximum = new decimal(array25);
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new Size(53, 21);
			this.numericUpDown6.TabIndex = 16;
			this.label80.AutoSize = true;
			this.label80.Location = new Point(38, 21);
			this.label80.Name = "label80";
			this.label80.Size = new Size(0, 12);
			this.label80.TabIndex = 14;
			this.dateTimePicker14.CustomFormat = "HH:mm";
			this.dateTimePicker14.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker14.Location = new Point(136, 44);
			this.dateTimePicker14.Name = "dateTimePicker14";
			this.dateTimePicker14.Size = new Size(64, 21);
			this.dateTimePicker14.TabIndex = 10;
			this.dateTimePicker14.Value = new DateTime(2029, 12, 31, 23, 59, 59, 0);
			this.numericUpDown15.Location = new Point(99, 492);
			NumericUpDown arg_9EFF_0 = this.numericUpDown15;
			int[] array26 = new int[4];
			array26[0] = 210000;
			arg_9EFF_0.Maximum = new decimal(array26);
			this.numericUpDown15.Name = "numericUpDown15";
			this.numericUpDown15.Size = new Size(100, 21);
			this.numericUpDown15.TabIndex = 18;
			this.checkBox111.AutoSize = true;
			this.checkBox111.Location = new Point(254, 44);
			this.checkBox111.Name = "checkBox111";
			this.checkBox111.Size = new Size(120, 16);
			this.checkBox111.TabIndex = 17;
			this.checkBox111.Text = "111_超级权限用户";
			this.checkBox111.UseVisualStyleBackColor = true;
			this.button53.Location = new Point(19, 519);
			this.button53.Name = "button53";
			this.button53.Size = new Size(122, 23);
			this.button53.TabIndex = 13;
			this.button53.Text = "53_读取权限[IP]";
			this.button53.UseVisualStyleBackColor = true;
			this.button53.Click += new EventHandler(this.button53_Click);
			this.button17.Location = new Point(18, 461);
			this.button17.Name = "button17";
			this.button17.Size = new Size(122, 23);
			this.button17.TabIndex = 13;
			this.button17.Text = "17_读取权限表[IP]";
			this.button17.UseVisualStyleBackColor = true;
			this.button17.Click += new EventHandler(this.button17_Click);
			this.checkBox97.AutoSize = true;
			this.checkBox97.Location = new Point(175, 427);
			this.checkBox97.Name = "checkBox97";
			this.checkBox97.Size = new Size(150, 16);
			this.checkBox97.TabIndex = 12;
			this.checkBox97.Text = "97_更新连续3500个权限";
			this.checkBox97.UseVisualStyleBackColor = true;
			this.button4.Location = new Point(20, 423);
			this.button4.Name = "button4";
			this.button4.Size = new Size(103, 23);
			this.button4.TabIndex = 11;
			this.button4.Text = "4_更新权限[IP]";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.dtpDeactivate.Location = new Point(63, 142);
			this.dtpDeactivate.Name = "dtpDeactivate";
			this.dtpDeactivate.Size = new Size(116, 21);
			this.dtpDeactivate.TabIndex = 10;
			this.dtpDeactivate.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			this.dtpActivate.CustomFormat = "yyyy-MM-dd";
			this.dtpActivate.Location = new Point(62, 115);
			this.dtpActivate.Name = "dtpActivate";
			this.dtpActivate.Size = new Size(117, 21);
			this.dtpActivate.TabIndex = 9;
			this.dtpActivate.Value = new DateTime(2009, 1, 1, 18, 18, 0, 0);
			this.label121.AutoSize = true;
			this.label121.Location = new Point(22, 494);
			this.label121.Name = "label121";
			this.label121.Size = new Size(71, 12);
			this.label121.TabIndex = 6;
			this.label121.Text = "15 权限位置";
			this.label23.AutoSize = true;
			this.label23.Location = new Point(22, 146);
			this.label23.Name = "label23";
			this.label23.Size = new Size(29, 12);
			this.label23.TabIndex = 6;
			this.label23.Text = "截止";
			this.label22.AutoSize = true;
			this.label22.Location = new Point(21, 120);
			this.label22.Name = "label22";
			this.label22.Size = new Size(29, 12);
			this.label22.TabIndex = 5;
			this.label22.Text = "起始";
			this.txtPassword.Location = new Point(62, 87);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new Size(100, 21);
			this.txtPassword.TabIndex = 4;
			this.txtPassword.Text = "345678";
			this.label21.AutoSize = true;
			this.label21.Location = new Point(21, 92);
			this.label21.Name = "label21";
			this.label21.Size = new Size(35, 12);
			this.label21.TabIndex = 3;
			this.label21.Text = "密码:";
			this.checkBox25.AutoSize = true;
			this.checkBox25.Checked = true;
			this.checkBox25.CheckState = CheckState.Checked;
			this.checkBox25.Location = new Point(62, 44);
			this.checkBox25.Name = "checkBox25";
			this.checkBox25.Size = new Size(66, 16);
			this.checkBox25.TabIndex = 2;
			this.checkBox25.Text = "25_激活";
			this.checkBox25.UseVisualStyleBackColor = true;
			this.checkBox24.AutoSize = true;
			this.checkBox24.Location = new Point(62, 65);
			this.checkBox24.Name = "checkBox24";
			this.checkBox24.Size = new Size(78, 16);
			this.checkBox24.TabIndex = 1;
			this.checkBox24.Text = "24_删除卡";
			this.checkBox24.UseVisualStyleBackColor = true;
			this.tableLayoutPanel2.ColumnCount = 5;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.00001f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel2.Controls.Add(this.comboBox8, 4, 3);
			this.tableLayoutPanel2.Controls.Add(this.comboBox7, 3, 3);
			this.tableLayoutPanel2.Controls.Add(this.comboBox6, 2, 3);
			this.tableLayoutPanel2.Controls.Add(this.comboBox5, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.label20, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.comboBox4, 4, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboBox3, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboBox2, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.label18, 4, 0);
			this.tableLayoutPanel2.Controls.Add(this.label13, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.checkBox26, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.checkBox27, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.checkBox29, 4, 2);
			this.tableLayoutPanel2.Controls.Add(this.checkBox28, 3, 2);
			this.tableLayoutPanel2.Controls.Add(this.label14, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label15, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label16, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.label17, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.label19, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboBox1, 1, 1);
			this.tableLayoutPanel2.Location = new Point(18, 177);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.Size = new Size(533, 114);
			this.tableLayoutPanel2.TabIndex = 0;
			this.comboBox8.FormattingEnabled = true;
			this.comboBox8.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox8.Location = new Point(427, 87);
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new Size(88, 20);
			this.comboBox8.TabIndex = 21;
			this.comboBox8.Text = "0";
			this.comboBox7.FormattingEnabled = true;
			this.comboBox7.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox7.Location = new Point(321, 87);
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new Size(88, 20);
			this.comboBox7.TabIndex = 20;
			this.comboBox7.Text = "0";
			this.comboBox6.FormattingEnabled = true;
			this.comboBox6.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox6.Location = new Point(215, 87);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new Size(88, 20);
			this.comboBox6.TabIndex = 19;
			this.comboBox6.Text = "0";
			this.comboBox5.FormattingEnabled = true;
			this.comboBox5.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox5.Location = new Point(109, 87);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new Size(88, 20);
			this.comboBox5.TabIndex = 18;
			this.comboBox5.Text = "0";
			this.label20.AutoSize = true;
			this.label20.Location = new Point(3, 84);
			this.label20.Name = "label20";
			this.label20.Size = new Size(65, 12);
			this.label20.TabIndex = 17;
			this.label20.Text = "对应多卡组";
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10"
			});
			this.comboBox4.Location = new Point(427, 31);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new Size(88, 20);
			this.comboBox4.TabIndex = 16;
			this.comboBox4.Text = "1";
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10"
			});
			this.comboBox3.Location = new Point(321, 31);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new Size(88, 20);
			this.comboBox3.TabIndex = 15;
			this.comboBox3.Text = "1";
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10"
			});
			this.comboBox2.Location = new Point(215, 31);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new Size(88, 20);
			this.comboBox2.TabIndex = 14;
			this.comboBox2.Text = "1";
			this.label18.AutoSize = true;
			this.label18.Location = new Point(427, 0);
			this.label18.Name = "label18";
			this.label18.Size = new Size(35, 12);
			this.label18.TabIndex = 3;
			this.label18.Text = "4号门";
			this.label13.AutoSize = true;
			this.label13.Location = new Point(3, 56);
			this.label13.Name = "label13";
			this.label13.Size = new Size(77, 12);
			this.label13.TabIndex = 3;
			this.label13.Text = "对应门的首卡";
			this.checkBox26.AutoSize = true;
			this.checkBox26.Location = new Point(109, 59);
			this.checkBox26.Name = "checkBox26";
			this.checkBox26.Size = new Size(72, 16);
			this.checkBox26.TabIndex = 4;
			this.checkBox26.Text = "26_1号门";
			this.checkBox26.UseVisualStyleBackColor = true;
			this.checkBox27.AutoSize = true;
			this.checkBox27.Location = new Point(215, 59);
			this.checkBox27.Name = "checkBox27";
			this.checkBox27.Size = new Size(72, 16);
			this.checkBox27.TabIndex = 5;
			this.checkBox27.Text = "27_2号门";
			this.checkBox27.UseVisualStyleBackColor = true;
			this.checkBox29.AutoSize = true;
			this.checkBox29.Location = new Point(427, 59);
			this.checkBox29.Name = "checkBox29";
			this.checkBox29.Size = new Size(72, 16);
			this.checkBox29.TabIndex = 7;
			this.checkBox29.Text = "29_4号门";
			this.checkBox29.UseVisualStyleBackColor = true;
			this.checkBox28.AutoSize = true;
			this.checkBox28.Location = new Point(321, 59);
			this.checkBox28.Name = "checkBox28";
			this.checkBox28.Size = new Size(72, 16);
			this.checkBox28.TabIndex = 6;
			this.checkBox28.Text = "28_3号门";
			this.checkBox28.UseVisualStyleBackColor = true;
			this.label14.AutoSize = true;
			this.label14.Location = new Point(3, 0);
			this.label14.Name = "label14";
			this.label14.Size = new Size(29, 12);
			this.label14.TabIndex = 8;
			this.label14.Text = "功能";
			this.label15.AutoSize = true;
			this.label15.Location = new Point(109, 0);
			this.label15.Name = "label15";
			this.label15.Size = new Size(35, 12);
			this.label15.TabIndex = 9;
			this.label15.Text = "1号门";
			this.label16.AutoSize = true;
			this.label16.Location = new Point(215, 0);
			this.label16.Name = "label16";
			this.label16.Size = new Size(35, 12);
			this.label16.TabIndex = 10;
			this.label16.Text = "2号门";
			this.label17.AutoSize = true;
			this.label17.Location = new Point(321, 0);
			this.label17.Name = "label17";
			this.label17.Size = new Size(35, 12);
			this.label17.TabIndex = 11;
			this.label17.Text = "3号门";
			this.label19.AutoSize = true;
			this.label19.Location = new Point(3, 28);
			this.label19.Name = "label19";
			this.label19.Size = new Size(29, 12);
			this.label19.TabIndex = 12;
			this.label19.Text = "时段";
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10"
			});
			this.comboBox1.Location = new Point(109, 31);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(100, 20);
			this.comboBox1.TabIndex = 13;
			this.comboBox1.Text = "1";
			this.label12.AutoSize = true;
			this.label12.Location = new Point(17, 14);
			this.label12.Name = "label12";
			this.label12.Size = new Size(29, 12);
			this.label12.TabIndex = 0;
			this.label12.Text = "卡号";
			this.txtCardNO.Location = new Point(62, 6);
			this.txtCardNO.Name = "txtCardNO";
			this.txtCardNO.Size = new Size(89, 21);
			this.txtCardNO.TabIndex = 1;
			this.tabPage3.Controls.Add(this.label42);
			this.tabPage3.Controls.Add(this.comboBox48);
			this.tabPage3.Controls.Add(this.comboBox47);
			this.tabPage3.Controls.Add(this.comboBox46);
			this.tabPage3.Controls.Add(this.checkBox39);
			this.tabPage3.Controls.Add(this.checkBox40);
			this.tabPage3.Controls.Add(this.checkBox41);
			this.tabPage3.Controls.Add(this.checkBox42);
			this.tabPage3.Controls.Add(this.checkBox38);
			this.tabPage3.Controls.Add(this.checkBox37);
			this.tabPage3.Controls.Add(this.label41);
			this.tabPage3.Controls.Add(this.label40);
			this.tabPage3.Controls.Add(this.label24);
			this.tabPage3.Controls.Add(this.comboBox45);
			this.tabPage3.Controls.Add(this.tableLayoutPanel3);
			this.tabPage3.Controls.Add(this.label34);
			this.tabPage3.Controls.Add(this.checkBox36);
			this.tabPage3.Controls.Add(this.label26);
			this.tabPage3.Controls.Add(this.comboBox25);
			this.tabPage3.Controls.Add(this.comboBox32);
			this.tabPage3.Controls.Add(this.checkBox35);
			this.tabPage3.Controls.Add(this.comboBox26);
			this.tabPage3.Controls.Add(this.comboBox31);
			this.tabPage3.Controls.Add(this.label39);
			this.tabPage3.Controls.Add(this.comboBox27);
			this.tabPage3.Controls.Add(this.comboBox30);
			this.tabPage3.Controls.Add(this.label38);
			this.tabPage3.Controls.Add(this.comboBox13);
			this.tabPage3.Controls.Add(this.comboBox16);
			this.tabPage3.Controls.Add(this.comboBox28);
			this.tabPage3.Controls.Add(this.comboBox15);
			this.tabPage3.Controls.Add(this.comboBox29);
			this.tabPage3.Controls.Add(this.comboBox14);
			this.tabPage3.Controls.Add(this.comboBox41);
			this.tabPage3.Controls.Add(this.label33);
			this.tabPage3.Controls.Add(this.label35);
			this.tabPage3.Controls.Add(this.comboBox42);
			this.tabPage3.Controls.Add(this.comboBox21);
			this.tabPage3.Controls.Add(this.comboBox36);
			this.tabPage3.Controls.Add(this.comboBox43);
			this.tabPage3.Controls.Add(this.comboBox22);
			this.tabPage3.Controls.Add(this.comboBox44);
			this.tabPage3.Controls.Add(this.comboBox35);
			this.tabPage3.Controls.Add(this.comboBox23);
			this.tabPage3.Controls.Add(this.label37);
			this.tabPage3.Controls.Add(this.comboBox34);
			this.tabPage3.Controls.Add(this.checkBox30);
			this.tabPage3.Controls.Add(this.comboBox24);
			this.tabPage3.Controls.Add(this.comboBox37);
			this.tabPage3.Controls.Add(this.comboBox33);
			this.tabPage3.Controls.Add(this.comboBox20);
			this.tabPage3.Controls.Add(this.label32);
			this.tabPage3.Controls.Add(this.comboBox38);
			this.tabPage3.Controls.Add(this.label36);
			this.tabPage3.Controls.Add(this.comboBox19);
			this.tabPage3.Controls.Add(this.comboBox17);
			this.tabPage3.Controls.Add(this.comboBox39);
			this.tabPage3.Controls.Add(this.comboBox40);
			this.tabPage3.Controls.Add(this.comboBox18);
			this.tabPage3.Location = new Point(4, 76);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new Padding(3);
			this.tabPage3.Size = new Size(563, 628);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "参数:多卡";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.label42.AutoSize = true;
			this.label42.Location = new Point(122, 111);
			this.label42.Name = "label42";
			this.label42.Size = new Size(269, 12);
			this.label42.TabIndex = 101;
			this.label42.Text = "每个要求人数(9)[要大于0. 否则整个多卡会失效]";
			this.comboBox48.FormattingEnabled = true;
			this.comboBox48.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8"
			});
			this.comboBox48.Location = new Point(435, 380);
			this.comboBox48.Name = "comboBox48";
			this.comboBox48.Size = new Size(88, 20);
			this.comboBox48.TabIndex = 100;
			this.comboBox48.Text = "1";
			this.comboBox47.FormattingEnabled = true;
			this.comboBox47.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8"
			});
			this.comboBox47.Location = new Point(331, 380);
			this.comboBox47.Name = "comboBox47";
			this.comboBox47.Size = new Size(88, 20);
			this.comboBox47.TabIndex = 99;
			this.comboBox47.Text = "1";
			this.comboBox46.FormattingEnabled = true;
			this.comboBox46.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8"
			});
			this.comboBox46.Location = new Point(227, 380);
			this.comboBox46.Name = "comboBox46";
			this.comboBox46.Size = new Size(88, 20);
			this.comboBox46.TabIndex = 98;
			this.comboBox46.Text = "1";
			this.checkBox39.AutoSize = true;
			this.checkBox39.Location = new Point(123, 357);
			this.checkBox39.Name = "checkBox39";
			this.checkBox39.Size = new Size(72, 16);
			this.checkBox39.TabIndex = 97;
			this.checkBox39.Text = "39_1启用";
			this.checkBox39.UseVisualStyleBackColor = true;
			this.checkBox40.AutoSize = true;
			this.checkBox40.Location = new Point(227, 358);
			this.checkBox40.Name = "checkBox40";
			this.checkBox40.Size = new Size(72, 16);
			this.checkBox40.TabIndex = 96;
			this.checkBox40.Text = "40_2启用";
			this.checkBox40.UseVisualStyleBackColor = true;
			this.checkBox41.AutoSize = true;
			this.checkBox41.Location = new Point(331, 357);
			this.checkBox41.Name = "checkBox41";
			this.checkBox41.Size = new Size(72, 16);
			this.checkBox41.TabIndex = 95;
			this.checkBox41.Text = "41_3启用";
			this.checkBox41.UseVisualStyleBackColor = true;
			this.checkBox42.AutoSize = true;
			this.checkBox42.Location = new Point(435, 358);
			this.checkBox42.Name = "checkBox42";
			this.checkBox42.Size = new Size(72, 16);
			this.checkBox42.TabIndex = 94;
			this.checkBox42.Text = "42_4启用";
			this.checkBox42.UseVisualStyleBackColor = true;
			this.checkBox38.AutoSize = true;
			this.checkBox38.Location = new Point(435, 336);
			this.checkBox38.Name = "checkBox38";
			this.checkBox38.Size = new Size(72, 16);
			this.checkBox38.TabIndex = 93;
			this.checkBox38.Text = "38_4启用";
			this.checkBox38.UseVisualStyleBackColor = true;
			this.checkBox37.AutoSize = true;
			this.checkBox37.Location = new Point(331, 336);
			this.checkBox37.Name = "checkBox37";
			this.checkBox37.Size = new Size(72, 16);
			this.checkBox37.TabIndex = 92;
			this.checkBox37.Text = "37_3启用";
			this.checkBox37.UseVisualStyleBackColor = true;
			this.label41.AutoSize = true;
			this.label41.Location = new Point(20, 379);
			this.label41.Name = "label41";
			this.label41.Size = new Size(89, 12);
			this.label41.TabIndex = 91;
			this.label41.Text = "  开始组为(45)";
			this.label40.AutoSize = true;
			this.label40.Location = new Point(20, 357);
			this.label40.Name = "label40";
			this.label40.Size = new Size(65, 12);
			this.label40.TabIndex = 90;
			this.label40.Text = "启用独立组";
			this.label24.AutoSize = true;
			this.label24.Location = new Point(20, 336);
			this.label24.Name = "label24";
			this.label24.Size = new Size(89, 12);
			this.label24.TabIndex = 89;
			this.label24.Text = "按组顺序刷才开";
			this.comboBox45.FormattingEnabled = true;
			this.comboBox45.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8"
			});
			this.comboBox45.Location = new Point(123, 380);
			this.comboBox45.Name = "comboBox45";
			this.comboBox45.Size = new Size(88, 20);
			this.comboBox45.TabIndex = 88;
			this.comboBox45.Text = "1";
			this.tableLayoutPanel3.ColumnCount = 5;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.Controls.Add(this.label27, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.label28, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.label29, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.label30, 3, 0);
			this.tableLayoutPanel3.Controls.Add(this.label31, 4, 0);
			this.tableLayoutPanel3.Controls.Add(this.checkBox31, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.checkBox32, 2, 1);
			this.tableLayoutPanel3.Controls.Add(this.checkBox33, 3, 1);
			this.tableLayoutPanel3.Controls.Add(this.checkBox34, 4, 1);
			this.tableLayoutPanel3.Controls.Add(this.comboBox11, 3, 2);
			this.tableLayoutPanel3.Controls.Add(this.comboBox12, 4, 2);
			this.tableLayoutPanel3.Controls.Add(this.label25, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.comboBox10, 2, 2);
			this.tableLayoutPanel3.Controls.Add(this.comboBox9, 1, 2);
			this.tableLayoutPanel3.Location = new Point(17, 31);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 3;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
			this.tableLayoutPanel3.Size = new Size(522, 77);
			this.tableLayoutPanel3.TabIndex = 38;
			this.label27.Anchor = AnchorStyles.Left;
			this.label27.AutoSize = true;
			this.label27.Location = new Point(3, 33);
			this.label27.Name = "label27";
			this.label27.Size = new Size(77, 12);
			this.label27.TabIndex = 11;
			this.label27.Text = "要求多卡开门";
			this.label28.AutoSize = true;
			this.label28.Location = new Point(107, 0);
			this.label28.Name = "label28";
			this.label28.Size = new Size(59, 12);
			this.label28.TabIndex = 31;
			this.label28.Text = "1号读卡器";
			this.label29.AutoSize = true;
			this.label29.Location = new Point(211, 0);
			this.label29.Name = "label29";
			this.label29.Size = new Size(59, 12);
			this.label29.TabIndex = 32;
			this.label29.Text = "2号读卡器";
			this.label30.AutoSize = true;
			this.label30.Location = new Point(315, 0);
			this.label30.Name = "label30";
			this.label30.Size = new Size(59, 12);
			this.label30.TabIndex = 33;
			this.label30.Text = "3号读卡器";
			this.label31.AutoSize = true;
			this.label31.Location = new Point(419, 0);
			this.label31.Name = "label31";
			this.label31.Size = new Size(59, 12);
			this.label31.TabIndex = 34;
			this.label31.Text = "4号读卡器";
			this.checkBox31.AutoSize = true;
			this.checkBox31.Location = new Point(107, 29);
			this.checkBox31.Name = "checkBox31";
			this.checkBox31.Size = new Size(72, 16);
			this.checkBox31.TabIndex = 35;
			this.checkBox31.Text = "31_1启用";
			this.checkBox31.UseVisualStyleBackColor = true;
			this.checkBox32.AutoSize = true;
			this.checkBox32.Location = new Point(211, 29);
			this.checkBox32.Name = "checkBox32";
			this.checkBox32.Size = new Size(72, 16);
			this.checkBox32.TabIndex = 37;
			this.checkBox32.Text = "32_2启用";
			this.checkBox32.UseVisualStyleBackColor = true;
			this.checkBox33.AutoSize = true;
			this.checkBox33.Location = new Point(315, 29);
			this.checkBox33.Name = "checkBox33";
			this.checkBox33.Size = new Size(72, 16);
			this.checkBox33.TabIndex = 38;
			this.checkBox33.Text = "33_3启用";
			this.checkBox33.UseVisualStyleBackColor = true;
			this.checkBox34.AutoSize = true;
			this.checkBox34.Location = new Point(419, 29);
			this.checkBox34.Name = "checkBox34";
			this.checkBox34.Size = new Size(72, 16);
			this.checkBox34.TabIndex = 39;
			this.checkBox34.Text = "34_4启用";
			this.checkBox34.UseVisualStyleBackColor = true;
			this.comboBox11.FormattingEnabled = true;
			this.comboBox11.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox11.Location = new Point(315, 55);
			this.comboBox11.Name = "comboBox11";
			this.comboBox11.Size = new Size(88, 20);
			this.comboBox11.TabIndex = 41;
			this.comboBox11.Text = "1";
			this.comboBox12.FormattingEnabled = true;
			this.comboBox12.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox12.Location = new Point(419, 55);
			this.comboBox12.Name = "comboBox12";
			this.comboBox12.Size = new Size(88, 20);
			this.comboBox12.TabIndex = 40;
			this.comboBox12.Text = "1";
			this.label25.Anchor = AnchorStyles.Left;
			this.label25.AutoSize = true;
			this.label25.Location = new Point(3, 58);
			this.label25.Name = "label25";
			this.label25.Size = new Size(95, 12);
			this.label25.TabIndex = 44;
			this.label25.Text = "每个要求人数(9)";
			this.comboBox10.FormattingEnabled = true;
			this.comboBox10.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox10.Location = new Point(211, 55);
			this.comboBox10.Name = "comboBox10";
			this.comboBox10.Size = new Size(88, 20);
			this.comboBox10.TabIndex = 42;
			this.comboBox10.Text = "1";
			this.comboBox9.FormattingEnabled = true;
			this.comboBox9.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox9.Location = new Point(107, 55);
			this.comboBox9.Name = "comboBox9";
			this.comboBox9.Size = new Size(88, 20);
			this.comboBox9.TabIndex = 43;
			this.comboBox9.Text = "1";
			this.label34.AutoSize = true;
			this.label34.Location = new Point(20, 224);
			this.label34.Name = "label34";
			this.label34.Size = new Size(95, 12);
			this.label34.TabIndex = 64;
			this.label34.Text = "4组要求人数(25)";
			this.checkBox36.AutoSize = true;
			this.checkBox36.Location = new Point(227, 336);
			this.checkBox36.Name = "checkBox36";
			this.checkBox36.Size = new Size(72, 16);
			this.checkBox36.TabIndex = 87;
			this.checkBox36.Text = "36_2启用";
			this.checkBox36.UseVisualStyleBackColor = true;
			this.label26.AutoSize = true;
			this.label26.Location = new Point(20, 157);
			this.label26.Name = "label26";
			this.label26.Size = new Size(95, 12);
			this.label26.TabIndex = 49;
			this.label26.Text = "1组要求人数(13)";
			this.comboBox25.FormattingEnabled = true;
			this.comboBox25.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox25.Location = new Point(123, 220);
			this.comboBox25.Name = "comboBox25";
			this.comboBox25.Size = new Size(88, 20);
			this.comboBox25.TabIndex = 62;
			this.comboBox25.Text = "0";
			this.comboBox32.FormattingEnabled = true;
			this.comboBox32.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox32.Location = new Point(435, 243);
			this.comboBox32.Name = "comboBox32";
			this.comboBox32.Size = new Size(88, 20);
			this.comboBox32.TabIndex = 68;
			this.comboBox32.Text = "0";
			this.checkBox35.AutoSize = true;
			this.checkBox35.Location = new Point(123, 336);
			this.checkBox35.Name = "checkBox35";
			this.checkBox35.Size = new Size(72, 16);
			this.checkBox35.TabIndex = 86;
			this.checkBox35.Text = "35_1启用";
			this.checkBox35.UseVisualStyleBackColor = true;
			this.comboBox26.FormattingEnabled = true;
			this.comboBox26.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox26.Location = new Point(227, 221);
			this.comboBox26.Name = "comboBox26";
			this.comboBox26.Size = new Size(88, 20);
			this.comboBox26.TabIndex = 60;
			this.comboBox26.Text = "0";
			this.comboBox31.FormattingEnabled = true;
			this.comboBox31.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox31.Location = new Point(331, 242);
			this.comboBox31.Name = "comboBox31";
			this.comboBox31.Size = new Size(88, 20);
			this.comboBox31.TabIndex = 66;
			this.comboBox31.Text = "0";
			this.label39.AutoSize = true;
			this.label39.Location = new Point(160, 10);
			this.label39.Name = "label39";
			this.label39.Size = new Size(155, 12);
			this.label39.TabIndex = 85;
			this.label39.Text = "第9组作为单卡开门的特权组";
			this.comboBox27.FormattingEnabled = true;
			this.comboBox27.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox27.Location = new Point(331, 220);
			this.comboBox27.Name = "comboBox27";
			this.comboBox27.Size = new Size(88, 20);
			this.comboBox27.TabIndex = 61;
			this.comboBox27.Text = "0";
			this.comboBox30.FormattingEnabled = true;
			this.comboBox30.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox30.Location = new Point(227, 243);
			this.comboBox30.Name = "comboBox30";
			this.comboBox30.Size = new Size(88, 20);
			this.comboBox30.TabIndex = 65;
			this.comboBox30.Text = "0";
			this.label38.AutoSize = true;
			this.label38.Location = new Point(20, 312);
			this.label38.Name = "label38";
			this.label38.Size = new Size(95, 12);
			this.label38.TabIndex = 84;
			this.label38.Text = "8组要求人数(41)";
			this.comboBox13.FormattingEnabled = true;
			this.comboBox13.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox13.Location = new Point(123, 154);
			this.comboBox13.Name = "comboBox13";
			this.comboBox13.Size = new Size(88, 20);
			this.comboBox13.TabIndex = 47;
			this.comboBox13.Text = "0";
			this.comboBox16.FormattingEnabled = true;
			this.comboBox16.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox16.Location = new Point(435, 154);
			this.comboBox16.Name = "comboBox16";
			this.comboBox16.Size = new Size(88, 20);
			this.comboBox16.TabIndex = 45;
			this.comboBox16.Text = "0";
			this.comboBox28.FormattingEnabled = true;
			this.comboBox28.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox28.Location = new Point(435, 220);
			this.comboBox28.Name = "comboBox28";
			this.comboBox28.Size = new Size(88, 20);
			this.comboBox28.TabIndex = 63;
			this.comboBox28.Text = "0";
			this.comboBox15.FormattingEnabled = true;
			this.comboBox15.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox15.Location = new Point(331, 154);
			this.comboBox15.Name = "comboBox15";
			this.comboBox15.Size = new Size(88, 20);
			this.comboBox15.TabIndex = 46;
			this.comboBox15.Text = "0";
			this.comboBox29.FormattingEnabled = true;
			this.comboBox29.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox29.Location = new Point(123, 242);
			this.comboBox29.Name = "comboBox29";
			this.comboBox29.Size = new Size(88, 20);
			this.comboBox29.TabIndex = 67;
			this.comboBox29.Text = "0";
			this.comboBox14.FormattingEnabled = true;
			this.comboBox14.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox14.Location = new Point(227, 154);
			this.comboBox14.Name = "comboBox14";
			this.comboBox14.Size = new Size(88, 20);
			this.comboBox14.TabIndex = 48;
			this.comboBox14.Text = "0";
			this.comboBox41.FormattingEnabled = true;
			this.comboBox41.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox41.Location = new Point(123, 310);
			this.comboBox41.Name = "comboBox41";
			this.comboBox41.Size = new Size(88, 20);
			this.comboBox41.TabIndex = 82;
			this.comboBox41.Text = "0";
			this.label33.AutoSize = true;
			this.label33.Location = new Point(20, 202);
			this.label33.Name = "label33";
			this.label33.Size = new Size(95, 12);
			this.label33.TabIndex = 59;
			this.label33.Text = "3组要求人数(21)";
			this.label35.AutoSize = true;
			this.label35.Location = new Point(20, 246);
			this.label35.Name = "label35";
			this.label35.Size = new Size(95, 12);
			this.label35.TabIndex = 69;
			this.label35.Text = "5组要求人数(29)";
			this.comboBox42.FormattingEnabled = true;
			this.comboBox42.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox42.Location = new Point(227, 309);
			this.comboBox42.Name = "comboBox42";
			this.comboBox42.Size = new Size(88, 20);
			this.comboBox42.TabIndex = 80;
			this.comboBox42.Text = "0";
			this.comboBox21.FormattingEnabled = true;
			this.comboBox21.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox21.Location = new Point(123, 198);
			this.comboBox21.Name = "comboBox21";
			this.comboBox21.Size = new Size(88, 20);
			this.comboBox21.TabIndex = 57;
			this.comboBox21.Text = "0";
			this.comboBox36.FormattingEnabled = true;
			this.comboBox36.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox36.Location = new Point(435, 265);
			this.comboBox36.Name = "comboBox36";
			this.comboBox36.Size = new Size(88, 20);
			this.comboBox36.TabIndex = 73;
			this.comboBox36.Text = "0";
			this.comboBox43.FormattingEnabled = true;
			this.comboBox43.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox43.Location = new Point(331, 310);
			this.comboBox43.Name = "comboBox43";
			this.comboBox43.Size = new Size(88, 20);
			this.comboBox43.TabIndex = 81;
			this.comboBox43.Text = "0";
			this.comboBox22.FormattingEnabled = true;
			this.comboBox22.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox22.Location = new Point(227, 199);
			this.comboBox22.Name = "comboBox22";
			this.comboBox22.Size = new Size(88, 20);
			this.comboBox22.TabIndex = 55;
			this.comboBox22.Text = "0";
			this.comboBox44.FormattingEnabled = true;
			this.comboBox44.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox44.Location = new Point(435, 309);
			this.comboBox44.Name = "comboBox44";
			this.comboBox44.Size = new Size(88, 20);
			this.comboBox44.TabIndex = 83;
			this.comboBox44.Text = "0";
			this.comboBox35.FormattingEnabled = true;
			this.comboBox35.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox35.Location = new Point(331, 264);
			this.comboBox35.Name = "comboBox35";
			this.comboBox35.Size = new Size(88, 20);
			this.comboBox35.TabIndex = 71;
			this.comboBox35.Text = "0";
			this.comboBox23.FormattingEnabled = true;
			this.comboBox23.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox23.Location = new Point(331, 198);
			this.comboBox23.Name = "comboBox23";
			this.comboBox23.Size = new Size(88, 20);
			this.comboBox23.TabIndex = 56;
			this.comboBox23.Text = "0";
			this.label37.AutoSize = true;
			this.label37.Location = new Point(20, 290);
			this.label37.Name = "label37";
			this.label37.Size = new Size(95, 12);
			this.label37.TabIndex = 79;
			this.label37.Text = "7组要求人数(37)";
			this.comboBox34.FormattingEnabled = true;
			this.comboBox34.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox34.Location = new Point(227, 265);
			this.comboBox34.Name = "comboBox34";
			this.comboBox34.Size = new Size(88, 20);
			this.comboBox34.TabIndex = 70;
			this.comboBox34.Text = "0";
			this.checkBox30.AutoSize = true;
			this.checkBox30.BackColor = Color.Red;
			this.checkBox30.Location = new Point(17, 9);
			this.checkBox30.Name = "checkBox30";
			this.checkBox30.Size = new Size(114, 16);
			this.checkBox30.TabIndex = 37;
			this.checkBox30.Text = "30_选择修改多卡";
			this.checkBox30.UseVisualStyleBackColor = false;
			this.comboBox24.FormattingEnabled = true;
			this.comboBox24.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox24.Location = new Point(435, 198);
			this.comboBox24.Name = "comboBox24";
			this.comboBox24.Size = new Size(88, 20);
			this.comboBox24.TabIndex = 58;
			this.comboBox24.Text = "0";
			this.comboBox37.FormattingEnabled = true;
			this.comboBox37.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox37.Location = new Point(123, 287);
			this.comboBox37.Name = "comboBox37";
			this.comboBox37.Size = new Size(88, 20);
			this.comboBox37.TabIndex = 77;
			this.comboBox37.Text = "0";
			this.comboBox33.FormattingEnabled = true;
			this.comboBox33.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox33.Location = new Point(123, 264);
			this.comboBox33.Name = "comboBox33";
			this.comboBox33.Size = new Size(88, 20);
			this.comboBox33.TabIndex = 72;
			this.comboBox33.Text = "0";
			this.comboBox20.FormattingEnabled = true;
			this.comboBox20.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox20.Location = new Point(435, 177);
			this.comboBox20.Name = "comboBox20";
			this.comboBox20.Size = new Size(88, 20);
			this.comboBox20.TabIndex = 53;
			this.comboBox20.Text = "0";
			this.label32.AutoSize = true;
			this.label32.Location = new Point(20, 180);
			this.label32.Name = "label32";
			this.label32.Size = new Size(95, 12);
			this.label32.TabIndex = 54;
			this.label32.Text = "2组要求人数(17)";
			this.comboBox38.FormattingEnabled = true;
			this.comboBox38.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox38.Location = new Point(227, 287);
			this.comboBox38.Name = "comboBox38";
			this.comboBox38.Size = new Size(88, 20);
			this.comboBox38.TabIndex = 75;
			this.comboBox38.Text = "0";
			this.label36.AutoSize = true;
			this.label36.Location = new Point(20, 268);
			this.label36.Name = "label36";
			this.label36.Size = new Size(95, 12);
			this.label36.TabIndex = 74;
			this.label36.Text = "6组要求人数(33)";
			this.comboBox19.FormattingEnabled = true;
			this.comboBox19.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox19.Location = new Point(331, 177);
			this.comboBox19.Name = "comboBox19";
			this.comboBox19.Size = new Size(88, 20);
			this.comboBox19.TabIndex = 51;
			this.comboBox19.Text = "0";
			this.comboBox17.FormattingEnabled = true;
			this.comboBox17.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox17.Location = new Point(123, 177);
			this.comboBox17.Name = "comboBox17";
			this.comboBox17.Size = new Size(88, 20);
			this.comboBox17.TabIndex = 52;
			this.comboBox17.Text = "0";
			this.comboBox39.FormattingEnabled = true;
			this.comboBox39.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox39.Location = new Point(331, 287);
			this.comboBox39.Name = "comboBox39";
			this.comboBox39.Size = new Size(88, 20);
			this.comboBox39.TabIndex = 76;
			this.comboBox39.Text = "0";
			this.comboBox40.FormattingEnabled = true;
			this.comboBox40.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox40.Location = new Point(435, 287);
			this.comboBox40.Name = "comboBox40";
			this.comboBox40.Size = new Size(88, 20);
			this.comboBox40.TabIndex = 78;
			this.comboBox40.Text = "0";
			this.comboBox18.FormattingEnabled = true;
			this.comboBox18.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.comboBox18.Location = new Point(227, 177);
			this.comboBox18.Name = "comboBox18";
			this.comboBox18.Size = new Size(88, 20);
			this.comboBox18.TabIndex = 50;
			this.comboBox18.Text = "0";
			this.tabPage4.Controls.Add(this.dgvControlConfure);
			this.tabPage4.Location = new Point(4, 76);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new Padding(3);
			this.tabPage4.Size = new Size(563, 628);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "读取到的参数信息";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.dgvControlConfure.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvControlConfure.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_Type,
				this.f_Loc,
				this.f_Value,
				this.f_DefaultValue,
				this.f_Desc
			});
			this.dgvControlConfure.Dock = DockStyle.Fill;
			this.dgvControlConfure.Location = new Point(3, 3);
			this.dgvControlConfure.Name = "dgvControlConfure";
			this.dgvControlConfure.RowTemplate.Height = 23;
			this.dgvControlConfure.Size = new Size(557, 622);
			this.dgvControlConfure.TabIndex = 0;
			this.dgvControlConfure.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvControlConfure_CellFormatting);
			this.f_Type.HeaderText = "T";
			this.f_Type.Name = "f_Type";
			this.f_Type.Width = 30;
			this.f_Loc.HeaderText = "Loc";
			this.f_Loc.Name = "f_Loc";
			this.f_Value.HeaderText = "Value";
			this.f_Value.Name = "f_Value";
			this.f_DefaultValue.HeaderText = "默认值";
			this.f_DefaultValue.Name = "f_DefaultValue";
			this.f_Desc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.f_Desc.HeaderText = "Desc";
			this.f_Desc.Name = "f_Desc";
			this.tabPage5.Controls.Add(this.button12);
			this.tabPage5.Controls.Add(this.button11);
			this.tabPage5.Controls.Add(this.button10);
			this.tabPage5.Controls.Add(this.button9);
			this.tabPage5.Controls.Add(this.listBox1);
			this.tabPage5.Controls.Add(this.button8);
			this.tabPage5.Controls.Add(this.button7);
			this.tabPage5.Controls.Add(this.button6);
			this.tabPage5.Controls.Add(this.button5);
			this.tabPage5.Controls.Add(this.label47);
			this.tabPage5.Controls.Add(this.numericUpDown2);
			this.tabPage5.Controls.Add(this.label46);
			this.tabPage5.Controls.Add(this.numericUpDown1);
			this.tabPage5.Controls.Add(this.groupBox2);
			this.tabPage5.Controls.Add(this.label45);
			this.tabPage5.Controls.Add(this.dateTimePicker3);
			this.tabPage5.Controls.Add(this.label43);
			this.tabPage5.Controls.Add(this.label44);
			this.tabPage5.Controls.Add(this.dateTimePicker2);
			this.tabPage5.Controls.Add(this.dateTimePicker1);
			this.tabPage5.Location = new Point(4, 76);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new Padding(3);
			this.tabPage5.Size = new Size(563, 628);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "定时任务";
			this.tabPage5.UseVisualStyleBackColor = true;
			this.button12.Location = new Point(424, 275);
			this.button12.Name = "button12";
			this.button12.Size = new Size(107, 23);
			this.button12.TabIndex = 24;
			this.button12.Text = "12_取定时任务页";
			this.button12.UseVisualStyleBackColor = true;
			this.button12.Click += new EventHandler(this.button12_Click);
			this.button11.Location = new Point(243, 275);
			this.button11.Name = "button11";
			this.button11.Size = new Size(151, 23);
			this.button11.TabIndex = 23;
			this.button11.Text = "11_上传定时任务[IP]";
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new EventHandler(this.button11_Click);
			this.button10.Location = new Point(129, 275);
			this.button10.Name = "button10";
			this.button10.Size = new Size(75, 23);
			this.button10.TabIndex = 22;
			this.button10.Text = "10_清空";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new EventHandler(this.button10_Click);
			this.button9.Location = new Point(24, 275);
			this.button9.Name = "button9";
			this.button9.Size = new Size(75, 23);
			this.button9.TabIndex = 21;
			this.button9.Text = "9_增加";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new EventHandler(this.button9_Click);
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new Point(18, 315);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new Size(528, 184);
			this.listBox1.TabIndex = 20;
			this.button8.Location = new Point(441, 221);
			this.button8.Name = "button8";
			this.button8.Size = new Size(75, 23);
			this.button8.TabIndex = 19;
			this.button8.Text = "8_在线";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new EventHandler(this.button8_Click);
			this.button7.Location = new Point(360, 221);
			this.button7.Name = "button7";
			this.button7.Size = new Size(75, 23);
			this.button7.TabIndex = 18;
			this.button7.Text = "7_常闭";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new EventHandler(this.button7_Click);
			this.button6.Location = new Point(279, 221);
			this.button6.Name = "button6";
			this.button6.Size = new Size(75, 23);
			this.button6.TabIndex = 17;
			this.button6.Text = "6_常开";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new EventHandler(this.button6_Click);
			this.button5.Location = new Point(279, 194);
			this.button5.Name = "button5";
			this.button5.Size = new Size(75, 23);
			this.button5.TabIndex = 16;
			this.button5.Text = "5_1号门";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new EventHandler(this.button5_Click);
			this.label47.AutoSize = true;
			this.label47.Location = new Point(34, 223);
			this.label47.Name = "label47";
			this.label47.Size = new Size(77, 12);
			this.label47.TabIndex = 15;
			this.label47.Text = "参数值(nud2)";
			this.numericUpDown2.Location = new Point(129, 221);
			NumericUpDown arg_FD2E_0 = this.numericUpDown2;
			array = new int[4];
			array[0] = 255;
			arg_FD2E_0.Maximum = new decimal(array);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new Size(120, 21);
			this.numericUpDown2.TabIndex = 14;
			this.label46.AutoSize = true;
			this.label46.Location = new Point(34, 196);
			this.label46.Name = "label46";
			this.label46.Size = new Size(89, 12);
			this.label46.TabIndex = 13;
			this.label46.Text = "参数位置(nud1)";
			this.numericUpDown1.Location = new Point(129, 194);
			NumericUpDown arg_FE00_0 = this.numericUpDown1;
			array = new int[4];
			array[0] = 1024;
			arg_FE00_0.Maximum = new decimal(array);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new Size(120, 21);
			this.numericUpDown1.TabIndex = 12;
			this.groupBox2.Controls.Add(this.checkBox49);
			this.groupBox2.Controls.Add(this.checkBox48);
			this.groupBox2.Controls.Add(this.checkBox47);
			this.groupBox2.Controls.Add(this.checkBox46);
			this.groupBox2.Controls.Add(this.checkBox45);
			this.groupBox2.Controls.Add(this.checkBox44);
			this.groupBox2.Controls.Add(this.checkBox43);
			this.groupBox2.Location = new Point(18, 129);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(537, 50);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "星期控制";
			this.checkBox49.AutoSize = true;
			this.checkBox49.Checked = true;
			this.checkBox49.CheckState = CheckState.Checked;
			this.checkBox49.Location = new Point(462, 23);
			this.checkBox49.Name = "checkBox49";
			this.checkBox49.Size = new Size(66, 16);
			this.checkBox49.TabIndex = 6;
			this.checkBox49.Text = "49_周日";
			this.checkBox49.UseVisualStyleBackColor = true;
			this.checkBox48.AutoSize = true;
			this.checkBox48.Checked = true;
			this.checkBox48.CheckState = CheckState.Checked;
			this.checkBox48.Location = new Point(386, 23);
			this.checkBox48.Name = "checkBox48";
			this.checkBox48.Size = new Size(66, 16);
			this.checkBox48.TabIndex = 5;
			this.checkBox48.Text = "48_周六";
			this.checkBox48.UseVisualStyleBackColor = true;
			this.checkBox47.AutoSize = true;
			this.checkBox47.Checked = true;
			this.checkBox47.CheckState = CheckState.Checked;
			this.checkBox47.Location = new Point(310, 23);
			this.checkBox47.Name = "checkBox47";
			this.checkBox47.Size = new Size(66, 16);
			this.checkBox47.TabIndex = 4;
			this.checkBox47.Text = "47_周五";
			this.checkBox47.UseVisualStyleBackColor = true;
			this.checkBox46.AutoSize = true;
			this.checkBox46.Checked = true;
			this.checkBox46.CheckState = CheckState.Checked;
			this.checkBox46.Location = new Point(234, 23);
			this.checkBox46.Name = "checkBox46";
			this.checkBox46.Size = new Size(66, 16);
			this.checkBox46.TabIndex = 3;
			this.checkBox46.Text = "46_周四";
			this.checkBox46.UseVisualStyleBackColor = true;
			this.checkBox45.AutoSize = true;
			this.checkBox45.Checked = true;
			this.checkBox45.CheckState = CheckState.Checked;
			this.checkBox45.Location = new Point(158, 23);
			this.checkBox45.Name = "checkBox45";
			this.checkBox45.Size = new Size(66, 16);
			this.checkBox45.TabIndex = 2;
			this.checkBox45.Text = "45_周三";
			this.checkBox45.UseVisualStyleBackColor = true;
			this.checkBox44.AutoSize = true;
			this.checkBox44.Checked = true;
			this.checkBox44.CheckState = CheckState.Checked;
			this.checkBox44.Location = new Point(82, 23);
			this.checkBox44.Name = "checkBox44";
			this.checkBox44.Size = new Size(66, 16);
			this.checkBox44.TabIndex = 1;
			this.checkBox44.Text = "44_周二";
			this.checkBox44.UseVisualStyleBackColor = true;
			this.checkBox43.AutoSize = true;
			this.checkBox43.Checked = true;
			this.checkBox43.CheckState = CheckState.Checked;
			this.checkBox43.Location = new Point(6, 23);
			this.checkBox43.Name = "checkBox43";
			this.checkBox43.Size = new Size(66, 16);
			this.checkBox43.TabIndex = 0;
			this.checkBox43.Text = "43_周一";
			this.checkBox43.UseVisualStyleBackColor = true;
			this.label45.AutoSize = true;
			this.label45.Location = new Point(52, 97);
			this.label45.Name = "label45";
			this.label45.Size = new Size(29, 12);
			this.label45.TabIndex = 10;
			this.label45.Text = "定时";
			this.dateTimePicker3.CustomFormat = "HH:mm";
			this.dateTimePicker3.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker3.Location = new Point(97, 92);
			this.dateTimePicker3.Name = "dateTimePicker3";
			this.dateTimePicker3.ShowUpDown = true;
			this.dateTimePicker3.Size = new Size(93, 21);
			this.dateTimePicker3.TabIndex = 9;
			this.dateTimePicker3.Value = new DateTime(2010, 1, 1, 8, 0, 0, 0);
			this.label43.AutoSize = true;
			this.label43.Location = new Point(52, 55);
			this.label43.Name = "label43";
			this.label43.Size = new Size(29, 12);
			this.label43.TabIndex = 8;
			this.label43.Text = "截止";
			this.label44.AutoSize = true;
			this.label44.Location = new Point(52, 33);
			this.label44.Name = "label44";
			this.label44.Size = new Size(29, 12);
			this.label44.TabIndex = 7;
			this.label44.Text = "起始";
			this.dateTimePicker2.Location = new Point(97, 51);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new Size(136, 21);
			this.dateTimePicker2.TabIndex = 3;
			this.dateTimePicker2.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Location = new Point(97, 29);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new Size(136, 21);
			this.dateTimePicker1.TabIndex = 2;
			this.dateTimePicker1.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			this.tabPage6.Controls.Add(this.button15);
			this.tabPage6.Controls.Add(this.label49);
			this.tabPage6.Controls.Add(this.label48);
			this.tabPage6.Controls.Add(this.dateTimePicker5);
			this.tabPage6.Controls.Add(this.dateTimePicker4);
			this.tabPage6.Controls.Add(this.button14);
			this.tabPage6.Controls.Add(this.button13);
			this.tabPage6.Location = new Point(4, 76);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new Padding(3);
			this.tabPage6.Size = new Size(563, 628);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "实时时钟[读写]";
			this.tabPage6.UseVisualStyleBackColor = true;
			this.button15.Location = new Point(234, 145);
			this.button15.Name = "button15";
			this.button15.Size = new Size(161, 23);
			this.button15.TabIndex = 14;
			this.button15.Text = "15_读取控制器时间";
			this.button15.UseVisualStyleBackColor = true;
			this.button15.Click += new EventHandler(this.button15_Click);
			this.label49.AutoSize = true;
			this.label49.Location = new Point(26, 58);
			this.label49.Name = "label49";
			this.label49.Size = new Size(47, 12);
			this.label49.TabIndex = 13;
			this.label49.Text = "时间(5)";
			this.label48.AutoSize = true;
			this.label48.Location = new Point(26, 31);
			this.label48.Name = "label48";
			this.label48.Size = new Size(47, 12);
			this.label48.TabIndex = 12;
			this.label48.Text = "日期(4)";
			this.dateTimePicker5.CustomFormat = "HH:mm:ss";
			this.dateTimePicker5.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker5.Location = new Point(79, 54);
			this.dateTimePicker5.Name = "dateTimePicker5";
			this.dateTimePicker5.ShowUpDown = true;
			this.dateTimePicker5.Size = new Size(93, 21);
			this.dateTimePicker5.TabIndex = 11;
			this.dateTimePicker5.Value = new DateTime(2010, 1, 1, 8, 0, 0, 0);
			this.dateTimePicker4.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker4.Location = new Point(79, 27);
			this.dateTimePicker4.Name = "dateTimePicker4";
			this.dateTimePicker4.Size = new Size(136, 21);
			this.dateTimePicker4.TabIndex = 10;
			this.dateTimePicker4.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			this.button14.Location = new Point(234, 92);
			this.button14.Name = "button14";
			this.button14.Size = new Size(161, 23);
			this.button14.TabIndex = 1;
			this.button14.Text = "14_以电脑时间设置";
			this.button14.UseVisualStyleBackColor = true;
			this.button14.Click += new EventHandler(this.button14_Click);
			this.button13.Location = new Point(54, 92);
			this.button13.Name = "button13";
			this.button13.Size = new Size(161, 23);
			this.button13.TabIndex = 0;
			this.button13.Text = "13_按指定时间设置";
			this.button13.UseVisualStyleBackColor = true;
			this.button13.Click += new EventHandler(this.button13_Click);
			this.tabPage8.Controls.Add(this.button84);
			this.tabPage8.Controls.Add(this.groupBox9);
			this.tabPage8.Controls.Add(this.label71);
			this.tabPage8.Controls.Add(this.label70);
			this.tabPage8.Controls.Add(this.numericUpDown3);
			this.tabPage8.Controls.Add(this.groupBox8);
			this.tabPage8.Controls.Add(this.groupBox7);
			this.tabPage8.Controls.Add(this.groupBox5);
			this.tabPage8.Controls.Add(this.checkBox72);
			this.tabPage8.Controls.Add(this.checkBox73);
			this.tabPage8.Controls.Add(this.checkBox74);
			this.tabPage8.Controls.Add(this.checkBox75);
			this.tabPage8.Controls.Add(this.checkBox71);
			this.tabPage8.Controls.Add(this.label69);
			this.tabPage8.Controls.Add(this.label68);
			this.tabPage8.Location = new Point(4, 76);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Padding = new Padding(3);
			this.tabPage8.Size = new Size(563, 628);
			this.tabPage8.TabIndex = 7;
			this.tabPage8.Text = "扩展板及报警设置";
			this.tabPage8.UseVisualStyleBackColor = true;
			this.button84.Location = new Point(35, 559);
			this.button84.Name = "button84";
			this.button84.Size = new Size(106, 23);
			this.button84.TabIndex = 113;
			this.button84.Text = "84 报警复位";
			this.button84.UseVisualStyleBackColor = true;
			this.button84.Click += new EventHandler(this.button84_Click);
			this.groupBox9.Controls.Add(this.checkBox96);
			this.groupBox9.Controls.Add(this.checkBox95);
			this.groupBox9.Controls.Add(this.checkBox92);
			this.groupBox9.Controls.Add(this.checkBox94);
			this.groupBox9.Controls.Add(this.checkBox93);
			this.groupBox9.Controls.Add(this.label76);
			this.groupBox9.Controls.Add(this.checkBox91);
			this.groupBox9.Controls.Add(this.numericUpDown5);
			this.groupBox9.Controls.Add(this.label73);
			this.groupBox9.Controls.Add(this.label72);
			this.groupBox9.Controls.Add(this.numericUpDown4);
			this.groupBox9.Controls.Add(this.label75);
			this.groupBox9.Controls.Add(this.label74);
			this.groupBox9.Location = new Point(124, 352);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new Size(421, 149);
			this.groupBox9.TabIndex = 112;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "防盗";
			this.checkBox96.AutoSize = true;
			this.checkBox96.Checked = true;
			this.checkBox96.CheckState = CheckState.Checked;
			this.checkBox96.Location = new Point(273, 120);
			this.checkBox96.Name = "checkBox96";
			this.checkBox96.Size = new Size(78, 16);
			this.checkBox96.TabIndex = 111;
			this.checkBox96.Text = "96_A3常闭";
			this.checkBox96.UseVisualStyleBackColor = true;
			this.checkBox95.AutoSize = true;
			this.checkBox95.Checked = true;
			this.checkBox95.CheckState = CheckState.Checked;
			this.checkBox95.Location = new Point(131, 114);
			this.checkBox95.Name = "checkBox95";
			this.checkBox95.Size = new Size(78, 16);
			this.checkBox95.TabIndex = 111;
			this.checkBox95.Text = "95_A2常闭";
			this.checkBox95.UseVisualStyleBackColor = true;
			this.checkBox92.AutoSize = true;
			this.checkBox92.Checked = true;
			this.checkBox92.CheckState = CheckState.Checked;
			this.checkBox92.Location = new Point(113, 92);
			this.checkBox92.Name = "checkBox92";
			this.checkBox92.Size = new Size(150, 16);
			this.checkBox92.TabIndex = 108;
			this.checkBox92.Text = "92_A2烟雾煤气温度报警";
			this.checkBox92.UseVisualStyleBackColor = true;
			this.checkBox94.AutoSize = true;
			this.checkBox94.Checked = true;
			this.checkBox94.CheckState = CheckState.Checked;
			this.checkBox94.Location = new Point(16, 114);
			this.checkBox94.Name = "checkBox94";
			this.checkBox94.Size = new Size(78, 16);
			this.checkBox94.TabIndex = 110;
			this.checkBox94.Text = "94_A1常闭";
			this.checkBox94.UseVisualStyleBackColor = true;
			this.checkBox93.AutoSize = true;
			this.checkBox93.Checked = true;
			this.checkBox93.CheckState = CheckState.Checked;
			this.checkBox93.Location = new Point(264, 92);
			this.checkBox93.Name = "checkBox93";
			this.checkBox93.Size = new Size(126, 16);
			this.checkBox93.TabIndex = 107;
			this.checkBox93.Text = "93_A3紧急呼救报警";
			this.checkBox93.UseVisualStyleBackColor = true;
			this.label76.AutoSize = true;
			this.label76.Location = new Point(268, 66);
			this.label76.Name = "label76";
			this.label76.Size = new Size(17, 12);
			this.label76.TabIndex = 106;
			this.label76.Text = "秒";
			this.checkBox91.AutoSize = true;
			this.checkBox91.Checked = true;
			this.checkBox91.CheckState = CheckState.Checked;
			this.checkBox91.Location = new Point(5, 92);
			this.checkBox91.Name = "checkBox91";
			this.checkBox91.Size = new Size(102, 16);
			this.checkBox91.TabIndex = 109;
			this.checkBox91.Text = "91_A1防盗报警";
			this.checkBox91.UseVisualStyleBackColor = true;
			this.numericUpDown5.Location = new Point(219, 64);
			NumericUpDown arg_11130_0 = this.numericUpDown5;
			array = new int[4];
			array[0] = 6553;
			arg_11130_0.Maximum = new decimal(array);
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new Size(48, 21);
			this.numericUpDown5.TabIndex = 105;
			NumericUpDown arg_11181_0 = this.numericUpDown5;
			array = new int[4];
			array[0] = 30;
			arg_11181_0.Value = new decimal(array);
			this.label73.AutoSize = true;
			this.label73.Location = new Point(13, 17);
			this.label73.Name = "label73";
			this.label73.Size = new Size(155, 12);
			this.label73.TabIndex = 103;
			this.label73.Text = "防盗报警延时=固定延时时长";
			this.label72.AutoSize = true;
			this.label72.Location = new Point(157, 44);
			this.label72.Name = "label72";
			this.label72.Size = new Size(17, 12);
			this.label72.TabIndex = 102;
			this.label72.Text = "秒";
			this.numericUpDown4.Location = new Point(108, 42);
			NumericUpDown arg_11280_0 = this.numericUpDown4;
			array = new int[4];
			array[0] = 6553;
			arg_11280_0.Maximum = new decimal(array);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new Size(48, 21);
			this.numericUpDown4.TabIndex = 101;
			NumericUpDown arg_112D1_0 = this.numericUpDown4;
			array = new int[4];
			array[0] = 30;
			arg_112D1_0.Value = new decimal(array);
			this.label75.AutoSize = true;
			this.label75.Location = new Point(13, 66);
			this.label75.Name = "label75";
			this.label75.Size = new Size(197, 12);
			this.label75.TabIndex = 104;
			this.label75.Text = "开门后在如下时间内撤防延时(nud5)";
			this.label74.AutoSize = true;
			this.label74.Location = new Point(13, 44);
			this.label74.Name = "label74";
			this.label74.Size = new Size(89, 12);
			this.label74.TabIndex = 98;
			this.label74.Text = "设防延时(nud4)";
			this.label71.AutoSize = true;
			this.label71.Location = new Point(101, 332);
			this.label71.Name = "label71";
			this.label71.Size = new Size(17, 12);
			this.label71.TabIndex = 99;
			this.label71.Text = "秒";
			this.label70.AutoSize = true;
			this.label70.Location = new Point(5, 307);
			this.label70.Name = "label70";
			this.label70.Size = new Size(113, 12);
			this.label70.TabIndex = 98;
			this.label70.Text = "固定延时时长(nud3)";
			this.numericUpDown3.Location = new Point(35, 327);
			NumericUpDown arg_11497_0 = this.numericUpDown3;
			array = new int[4];
			array[0] = 6553;
			arg_11497_0.Maximum = new decimal(array);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new Size(48, 21);
			this.numericUpDown3.TabIndex = 97;
			NumericUpDown arg_114E7_0 = this.numericUpDown3;
			array = new int[4];
			array[0] = 3;
			arg_114E7_0.Value = new decimal(array);
			this.groupBox8.Controls.Add(this.radioButton19);
			this.groupBox8.Controls.Add(this.radioButton20);
			this.groupBox8.Controls.Add(this.radioButton21);
			this.groupBox8.Controls.Add(this.radioButton22);
			this.groupBox8.Location = new Point(27, 85);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new Size(372, 43);
			this.groupBox8.TabIndex = 96;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "选定输出端口";
			this.radioButton19.AutoSize = true;
			this.radioButton19.Checked = true;
			this.radioButton19.Location = new Point(8, 20);
			this.radioButton19.Name = "radioButton19";
			this.radioButton19.Size = new Size(83, 16);
			this.radioButton19.TabIndex = 95;
			this.radioButton19.TabStop = true;
			this.radioButton19.Text = "rd19_1号口";
			this.radioButton19.UseVisualStyleBackColor = true;
			this.radioButton20.AutoSize = true;
			this.radioButton20.Location = new Point(97, 20);
			this.radioButton20.Name = "radioButton20";
			this.radioButton20.Size = new Size(83, 16);
			this.radioButton20.TabIndex = 94;
			this.radioButton20.Text = "rd20_2号口";
			this.radioButton20.UseVisualStyleBackColor = true;
			this.radioButton21.AutoSize = true;
			this.radioButton21.Location = new Point(186, 20);
			this.radioButton21.Name = "radioButton21";
			this.radioButton21.Size = new Size(83, 16);
			this.radioButton21.TabIndex = 93;
			this.radioButton21.Text = "rd21_3号口";
			this.radioButton21.UseVisualStyleBackColor = true;
			this.radioButton22.AutoSize = true;
			this.radioButton22.Location = new Point(275, 20);
			this.radioButton22.Name = "radioButton22";
			this.radioButton22.Size = new Size(83, 16);
			this.radioButton22.TabIndex = 92;
			this.radioButton22.Text = "rd22_4号口";
			this.radioButton22.UseVisualStyleBackColor = true;
			this.groupBox7.Controls.Add(this.radioButton23);
			this.groupBox7.Controls.Add(this.checkBox83);
			this.groupBox7.Controls.Add(this.checkBox82);
			this.groupBox7.Controls.Add(this.checkBox81);
			this.groupBox7.Controls.Add(this.checkBox80);
			this.groupBox7.Controls.Add(this.checkBox79);
			this.groupBox7.Controls.Add(this.checkBox78);
			this.groupBox7.Controls.Add(this.checkBox77);
			this.groupBox7.Controls.Add(this.checkBox76);
			this.groupBox7.Controls.Add(this.radioButton18);
			this.groupBox7.Controls.Add(this.radioButton17);
			this.groupBox7.Controls.Add(this.radioButton16);
			this.groupBox7.Controls.Add(this.groupBox6);
			this.groupBox7.Controls.Add(this.radioButton15);
			this.groupBox7.Controls.Add(this.radioButton14);
			this.groupBox7.Location = new Point(124, 131);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new Size(422, 217);
			this.groupBox7.TabIndex = 95;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "控制";
			this.radioButton23.AutoSize = true;
			this.radioButton23.Checked = true;
			this.radioButton23.Location = new Point(0, 15);
			this.radioButton23.Name = "radioButton23";
			this.radioButton23.Size = new Size(83, 16);
			this.radioButton23.TabIndex = 97;
			this.radioButton23.TabStop = true;
			this.radioButton23.Text = "23_0不控制";
			this.radioButton23.UseVisualStyleBackColor = true;
			this.checkBox83.AutoSize = true;
			this.checkBox83.Location = new Point(287, 194);
			this.checkBox83.Name = "checkBox83";
			this.checkBox83.Size = new Size(84, 16);
			this.checkBox83.TabIndex = 96;
			this.checkBox83.Text = "83_Ext火警";
			this.checkBox83.UseVisualStyleBackColor = true;
			this.checkBox82.AutoSize = true;
			this.checkBox82.Location = new Point(215, 194);
			this.checkBox82.Name = "checkBox82";
			this.checkBox82.Size = new Size(72, 16);
			this.checkBox82.TabIndex = 97;
			this.checkBox82.Text = "82_ExtA3";
			this.checkBox82.UseVisualStyleBackColor = true;
			this.checkBox81.AutoSize = true;
			this.checkBox81.Location = new Point(131, 194);
			this.checkBox81.Name = "checkBox81";
			this.checkBox81.Size = new Size(72, 16);
			this.checkBox81.TabIndex = 98;
			this.checkBox81.Text = "81_ExtA2";
			this.checkBox81.UseVisualStyleBackColor = true;
			this.checkBox80.AutoSize = true;
			this.checkBox80.Location = new Point(35, 194);
			this.checkBox80.Name = "checkBox80";
			this.checkBox80.Size = new Size(72, 16);
			this.checkBox80.TabIndex = 99;
			this.checkBox80.Text = "80_ExtA1";
			this.checkBox80.UseVisualStyleBackColor = true;
			this.checkBox79.AutoSize = true;
			this.checkBox79.Location = new Point(287, 172);
			this.checkBox79.Name = "checkBox79";
			this.checkBox79.Size = new Size(90, 16);
			this.checkBox79.TabIndex = 93;
			this.checkBox79.Text = "79_强制关门";
			this.checkBox79.UseVisualStyleBackColor = true;
			this.checkBox78.AutoSize = true;
			this.checkBox78.Location = new Point(215, 172);
			this.checkBox78.Name = "checkBox78";
			this.checkBox78.Size = new Size(66, 16);
			this.checkBox78.TabIndex = 93;
			this.checkBox78.Text = "78_按钮";
			this.checkBox78.UseVisualStyleBackColor = true;
			this.checkBox77.AutoSize = true;
			this.checkBox77.Location = new Point(131, 172);
			this.checkBox77.Name = "checkBox77";
			this.checkBox77.Size = new Size(78, 16);
			this.checkBox77.TabIndex = 93;
			this.checkBox77.Text = "77_锁门磁";
			this.checkBox77.UseVisualStyleBackColor = true;
			this.checkBox76.AutoSize = true;
			this.checkBox76.Location = new Point(35, 172);
			this.checkBox76.Name = "checkBox76";
			this.checkBox76.Size = new Size(90, 16);
			this.checkBox76.TabIndex = 93;
			this.checkBox76.Text = "76_锁继电器";
			this.checkBox76.UseVisualStyleBackColor = true;
			this.radioButton18.AutoSize = true;
			this.radioButton18.Location = new Point(0, 150);
			this.radioButton18.Name = "radioButton18";
			this.radioButton18.Size = new Size(203, 16);
			this.radioButton18.TabIndex = 3;
			this.radioButton18.Text = "18_5 IO点全部合法才输出 一致性";
			this.radioButton18.UseVisualStyleBackColor = true;
			this.radioButton17.AutoSize = true;
			this.radioButton17.Location = new Point(0, 134);
			this.radioButton17.Name = "radioButton17";
			this.radioButton17.Size = new Size(299, 16);
			this.radioButton17.TabIndex = 3;
			this.radioButton17.Text = "17_4只要IO点中有一个合法信号就输出高, 否则为低";
			this.radioButton17.UseVisualStyleBackColor = true;
			this.radioButton16.AutoSize = true;
			this.radioButton16.Location = new Point(0, 90);
			this.radioButton16.Name = "radioButton16";
			this.radioButton16.Size = new Size(131, 16);
			this.radioButton16.TabIndex = 2;
			this.radioButton16.Text = "16_3与触发源一致性";
			this.radioButton16.UseVisualStyleBackColor = true;
			this.groupBox6.Controls.Add(this.checkBox90);
			this.groupBox6.Controls.Add(this.checkBox89);
			this.groupBox6.Controls.Add(this.checkBox88);
			this.groupBox6.Controls.Add(this.checkBox87);
			this.groupBox6.Controls.Add(this.checkBox86);
			this.groupBox6.Controls.Add(this.checkBox85);
			this.groupBox6.Controls.Add(this.checkBox84);
			this.groupBox6.Location = new Point(152, 15);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new Size(224, 106);
			this.groupBox6.TabIndex = 92;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "触发事件";
			this.checkBox90.AutoSize = true;
			this.checkBox90.Location = new Point(126, 58);
			this.checkBox90.Name = "checkBox90";
			this.checkBox90.Size = new Size(66, 16);
			this.checkBox90.TabIndex = 6;
			this.checkBox90.Text = "90_防盗";
			this.checkBox90.UseVisualStyleBackColor = true;
			this.checkBox89.AutoSize = true;
			this.checkBox89.Location = new Point(126, 39);
			this.checkBox89.Name = "checkBox89";
			this.checkBox89.Size = new Size(66, 16);
			this.checkBox89.TabIndex = 5;
			this.checkBox89.Text = "89_火警";
			this.checkBox89.UseVisualStyleBackColor = true;
			this.checkBox88.AutoSize = true;
			this.checkBox88.Location = new Point(126, 20);
			this.checkBox88.Name = "checkBox88";
			this.checkBox88.Size = new Size(78, 16);
			this.checkBox88.TabIndex = 4;
			this.checkBox88.Text = "88_非法卡";
			this.checkBox88.UseVisualStyleBackColor = true;
			this.checkBox87.AutoSize = true;
			this.checkBox87.Location = new Point(6, 77);
			this.checkBox87.Name = "checkBox87";
			this.checkBox87.Size = new Size(90, 16);
			this.checkBox87.TabIndex = 3;
			this.checkBox87.Text = "87_强制关门";
			this.checkBox87.UseVisualStyleBackColor = true;
			this.checkBox86.AutoSize = true;
			this.checkBox86.Location = new Point(6, 58);
			this.checkBox86.Name = "checkBox86";
			this.checkBox86.Size = new Size(90, 16);
			this.checkBox86.TabIndex = 2;
			this.checkBox86.Text = "86_强行闯入";
			this.checkBox86.UseVisualStyleBackColor = true;
			this.checkBox85.AutoSize = true;
			this.checkBox85.Location = new Point(6, 39);
			this.checkBox85.Name = "checkBox85";
			this.checkBox85.Size = new Size(114, 16);
			this.checkBox85.TabIndex = 1;
			this.checkBox85.Text = "85_正常开门过长";
			this.checkBox85.UseVisualStyleBackColor = true;
			this.checkBox84.AutoSize = true;
			this.checkBox84.Location = new Point(6, 20);
			this.checkBox84.Name = "checkBox84";
			this.checkBox84.Size = new Size(66, 16);
			this.checkBox84.TabIndex = 0;
			this.checkBox84.Text = "84_胁迫";
			this.checkBox84.UseVisualStyleBackColor = true;
			this.radioButton15.AutoSize = true;
			this.radioButton15.Location = new Point(0, 68);
			this.radioButton15.Name = "radioButton15";
			this.radioButton15.Size = new Size(137, 16);
			this.radioButton15.TabIndex = 1;
			this.radioButton15.Text = "15_2最小延时+一致性";
			this.radioButton15.UseVisualStyleBackColor = true;
			this.radioButton14.AutoSize = true;
			this.radioButton14.Location = new Point(0, 46);
			this.radioButton14.Name = "radioButton14";
			this.radioButton14.Size = new Size(95, 16);
			this.radioButton14.TabIndex = 0;
			this.radioButton14.Text = "14_1固定延时";
			this.radioButton14.UseVisualStyleBackColor = true;
			this.groupBox5.Controls.Add(this.radioButton25);
			this.groupBox5.Controls.Add(this.radioButton13);
			this.groupBox5.Controls.Add(this.radioButton12);
			this.groupBox5.Controls.Add(this.radioButton11);
			this.groupBox5.Controls.Add(this.radioButton10);
			this.groupBox5.Location = new Point(27, 147);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new Size(91, 134);
			this.groupBox5.TabIndex = 91;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "触发源";
			this.radioButton25.AutoSize = true;
			this.radioButton25.Location = new Point(0, 106);
			this.radioButton25.Name = "radioButton25";
			this.radioButton25.Size = new Size(89, 16);
			this.radioButton25.TabIndex = 96;
			this.radioButton25.Text = "rd25_16防盗";
			this.radioButton25.UseVisualStyleBackColor = true;
			this.radioButton13.AutoSize = true;
			this.radioButton13.Location = new Point(0, 84);
			this.radioButton13.Name = "radioButton13";
			this.radioButton13.Size = new Size(83, 16);
			this.radioButton13.TabIndex = 95;
			this.radioButton13.Text = "rd13_4号门";
			this.radioButton13.UseVisualStyleBackColor = true;
			this.radioButton12.AutoSize = true;
			this.radioButton12.Location = new Point(0, 64);
			this.radioButton12.Name = "radioButton12";
			this.radioButton12.Size = new Size(83, 16);
			this.radioButton12.TabIndex = 94;
			this.radioButton12.Text = "rd12_3号门";
			this.radioButton12.UseVisualStyleBackColor = true;
			this.radioButton11.AutoSize = true;
			this.radioButton11.Location = new Point(0, 42);
			this.radioButton11.Name = "radioButton11";
			this.radioButton11.Size = new Size(83, 16);
			this.radioButton11.TabIndex = 93;
			this.radioButton11.Text = "rd11_2号门";
			this.radioButton11.UseVisualStyleBackColor = true;
			this.radioButton10.AutoSize = true;
			this.radioButton10.Checked = true;
			this.radioButton10.Location = new Point(0, 20);
			this.radioButton10.Name = "radioButton10";
			this.radioButton10.Size = new Size(83, 16);
			this.radioButton10.TabIndex = 92;
			this.radioButton10.TabStop = true;
			this.radioButton10.Text = "rd10_1号门";
			this.radioButton10.UseVisualStyleBackColor = true;
			this.checkBox72.AutoSize = true;
			this.checkBox72.Location = new Point(115, 31);
			this.checkBox72.Name = "checkBox72";
			this.checkBox72.Size = new Size(60, 16);
			this.checkBox72.TabIndex = 90;
			this.checkBox72.Text = "72_4A1";
			this.checkBox72.UseVisualStyleBackColor = true;
			this.checkBox73.AutoSize = true;
			this.checkBox73.Location = new Point(192, 31);
			this.checkBox73.Name = "checkBox73";
			this.checkBox73.Size = new Size(60, 16);
			this.checkBox73.TabIndex = 89;
			this.checkBox73.Text = "73_5A2";
			this.checkBox73.UseVisualStyleBackColor = true;
			this.checkBox74.AutoSize = true;
			this.checkBox74.Location = new Point(269, 31);
			this.checkBox74.Name = "checkBox74";
			this.checkBox74.Size = new Size(60, 16);
			this.checkBox74.TabIndex = 88;
			this.checkBox74.Text = "74_6A3";
			this.checkBox74.UseVisualStyleBackColor = true;
			this.checkBox75.AutoSize = true;
			this.checkBox75.Location = new Point(358, 31);
			this.checkBox75.Name = "checkBox75";
			this.checkBox75.Size = new Size(72, 16);
			this.checkBox75.TabIndex = 87;
			this.checkBox75.Text = "75_7火警";
			this.checkBox75.UseVisualStyleBackColor = true;
			this.checkBox71.AutoSize = true;
			this.checkBox71.BackColor = Color.Red;
			this.checkBox71.Location = new Point(27, 6);
			this.checkBox71.Name = "checkBox71";
			this.checkBox71.Size = new Size(162, 16);
			this.checkBox71.TabIndex = 41;
			this.checkBox71.Text = "71_选择要修改扩展板设置";
			this.checkBox71.UseVisualStyleBackColor = false;
			this.label69.AutoSize = true;
			this.label69.Location = new Point(25, 59);
			this.label69.Name = "label69";
			this.label69.Size = new Size(71, 12);
			this.label69.TabIndex = 0;
			this.label69.Text = "输出口(0-3)";
			this.label68.AutoSize = true;
			this.label68.Location = new Point(8, 32);
			this.label68.Name = "label68";
			this.label68.Size = new Size(107, 12);
			this.label68.TabIndex = 0;
			this.label68.Text = "输入口(4-7)[未用]";
			this.tabPage9.Controls.Add(this.button16);
			this.tabPage9.Controls.Add(this.textBox20);
			this.tabPage9.Controls.Add(this.label79);
			this.tabPage9.Controls.Add(this.label78);
			this.tabPage9.Controls.Add(this.textBox19);
			this.tabPage9.Controls.Add(this.label77);
			this.tabPage9.Controls.Add(this.textBox18);
			this.tabPage9.Location = new Point(4, 76);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Padding = new Padding(3);
			this.tabPage9.Size = new Size(563, 628);
			this.tabPage9.TabIndex = 8;
			this.tabPage9.Text = "远程开门";
			this.tabPage9.UseVisualStyleBackColor = true;
			this.button16.Location = new Point(92, 158);
			this.button16.Name = "button16";
			this.button16.Size = new Size(138, 23);
			this.button16.TabIndex = 8;
			this.button16.Text = "6远程开门IP]";
			this.button16.UseVisualStyleBackColor = true;
			this.button16.Click += new EventHandler(this.button16_Click);
			this.textBox20.Location = new Point(159, 100);
			this.textBox20.Name = "textBox20";
			this.textBox20.Size = new Size(89, 21);
			this.textBox20.TabIndex = 7;
			this.textBox20.Text = "1";
			this.label79.AutoSize = true;
			this.label79.Location = new Point(57, 109);
			this.label79.Name = "label79";
			this.label79.Size = new Size(77, 12);
			this.label79.TabIndex = 6;
			this.label79.Text = "指定门号(20)";
			this.label78.AutoSize = true;
			this.label78.Location = new Point(57, 68);
			this.label78.Name = "label78";
			this.label78.Size = new Size(77, 12);
			this.label78.TabIndex = 4;
			this.label78.Text = "所持卡号(19)";
			this.textBox19.Location = new Point(159, 65);
			this.textBox19.Name = "textBox19";
			this.textBox19.Size = new Size(89, 21);
			this.textBox19.TabIndex = 5;
			this.label77.AutoSize = true;
			this.label77.Location = new Point(57, 37);
			this.label77.Name = "label77";
			this.label77.Size = new Size(77, 12);
			this.label77.TabIndex = 2;
			this.label77.Text = "操作员ID(18)";
			this.textBox18.Location = new Point(159, 28);
			this.textBox18.Name = "textBox18";
			this.textBox18.Size = new Size(89, 21);
			this.textBox18.TabIndex = 3;
			this.textBox18.Text = "1";
			this.tabPage10.Controls.Add(this.button63);
			this.tabPage10.Controls.Add(this.button62);
			this.tabPage10.Controls.Add(this.dateTimePicker18);
			this.tabPage10.Controls.Add(this.button61);
			this.tabPage10.Controls.Add(this.listBox3);
			this.tabPage10.Controls.Add(this.dateTimePicker15);
			this.tabPage10.Controls.Add(this.label129);
			this.tabPage10.Controls.Add(this.label130);
			this.tabPage10.Controls.Add(this.dateTimePicker16);
			this.tabPage10.Controls.Add(this.dateTimePicker17);
			this.tabPage10.Controls.Add(this.button19);
			this.tabPage10.Controls.Add(this.button20);
			this.tabPage10.Controls.Add(this.listBox2);
			this.tabPage10.Controls.Add(this.checkBox127);
			this.tabPage10.Controls.Add(this.checkBox105);
			this.tabPage10.Controls.Add(this.numericUpDown10);
			this.tabPage10.Controls.Add(this.label94);
			this.tabPage10.Controls.Add(this.button18);
			this.tabPage10.Controls.Add(this.groupBox11);
			this.tabPage10.Controls.Add(this.groupBox10);
			this.tabPage10.Controls.Add(this.label84);
			this.tabPage10.Controls.Add(this.comboBox58);
			this.tabPage10.Controls.Add(this.label83);
			this.tabPage10.Controls.Add(this.comboBox57);
			this.tabPage10.Controls.Add(this.dateTimePicker6);
			this.tabPage10.Controls.Add(this.dateTimePicker7);
			this.tabPage10.Controls.Add(this.label81);
			this.tabPage10.Controls.Add(this.label82);
			this.tabPage10.Location = new Point(4, 76);
			this.tabPage10.Name = "tabPage10";
			this.tabPage10.Padding = new Padding(3);
			this.tabPage10.Size = new Size(563, 628);
			this.tabPage10.TabIndex = 9;
			this.tabPage10.Text = "时段管理";
			this.tabPage10.UseVisualStyleBackColor = true;
			this.button63.Location = new Point(449, 516);
			this.button63.Name = "button63";
			this.button63.Size = new Size(103, 23);
			this.button63.TabIndex = 46;
			this.button63.Text = "63_更新假期[IP]";
			this.button63.UseVisualStyleBackColor = true;
			this.button63.Click += new EventHandler(this.button63_Click);
			this.button62.Location = new Point(343, 486);
			this.button62.Name = "button62";
			this.button62.Size = new Size(96, 23);
			this.button62.TabIndex = 45;
			this.button62.Text = "62_清空假期";
			this.button62.UseVisualStyleBackColor = true;
			this.button62.Click += new EventHandler(this.button62_Click);
			this.dateTimePicker18.CustomFormat = "HH:mm";
			this.dateTimePicker18.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker18.Location = new Point(244, 516);
			this.dateTimePicker18.Name = "dateTimePicker18";
			this.dateTimePicker18.ShowUpDown = true;
			this.dateTimePicker18.Size = new Size(93, 21);
			this.dateTimePicker18.TabIndex = 44;
			this.dateTimePicker18.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			this.button61.Location = new Point(343, 515);
			this.button61.Name = "button61";
			this.button61.Size = new Size(96, 23);
			this.button61.TabIndex = 43;
			this.button61.Text = "61_增加 假期";
			this.button61.UseVisualStyleBackColor = true;
			this.button61.Click += new EventHandler(this.button61_Click_1);
			this.listBox3.FormattingEnabled = true;
			this.listBox3.ItemHeight = 12;
			this.listBox3.Location = new Point(24, 543);
			this.listBox3.Name = "listBox3";
			this.listBox3.Size = new Size(528, 100);
			this.listBox3.TabIndex = 42;
			this.dateTimePicker15.CustomFormat = "HH:mm";
			this.dateTimePicker15.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker15.Location = new Point(244, 491);
			this.dateTimePicker15.Name = "dateTimePicker15";
			this.dateTimePicker15.ShowUpDown = true;
			this.dateTimePicker15.Size = new Size(93, 21);
			this.dateTimePicker15.TabIndex = 40;
			this.dateTimePicker15.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.label129.AutoSize = true;
			this.label129.Location = new Point(25, 517);
			this.label129.Name = "label129";
			this.label129.Size = new Size(53, 12);
			this.label129.TabIndex = 39;
			this.label129.Text = "假期截止";
			this.label130.AutoSize = true;
			this.label130.Location = new Point(25, 495);
			this.label130.Name = "label130";
			this.label130.Size = new Size(53, 12);
			this.label130.TabIndex = 38;
			this.label130.Text = "假期起始";
			this.dateTimePicker16.Location = new Point(93, 513);
			this.dateTimePicker16.Name = "dateTimePicker16";
			this.dateTimePicker16.Size = new Size(136, 21);
			this.dateTimePicker16.TabIndex = 37;
			this.dateTimePicker16.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			this.dateTimePicker17.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker17.Location = new Point(93, 491);
			this.dateTimePicker17.Name = "dateTimePicker17";
			this.dateTimePicker17.Size = new Size(136, 21);
			this.dateTimePicker17.TabIndex = 36;
			this.dateTimePicker17.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			this.button19.Location = new Point(27, 358);
			this.button19.Name = "button19";
			this.button19.Size = new Size(75, 23);
			this.button19.TabIndex = 35;
			this.button19.Text = "19_增加";
			this.button19.UseVisualStyleBackColor = true;
			this.button19.Click += new EventHandler(this.button19_Click);
			this.button20.Location = new Point(132, 358);
			this.button20.Name = "button20";
			this.button20.Size = new Size(75, 23);
			this.button20.TabIndex = 34;
			this.button20.Text = "20_清空";
			this.button20.UseVisualStyleBackColor = true;
			this.button20.Click += new EventHandler(this.button20_Click);
			this.listBox2.FormattingEnabled = true;
			this.listBox2.ItemHeight = 12;
			this.listBox2.Location = new Point(24, 394);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new Size(528, 76);
			this.listBox2.TabIndex = 33;
			this.checkBox127.AutoSize = true;
			this.checkBox127.Location = new Point(69, 100);
			this.checkBox127.Name = "checkBox127";
			this.checkBox127.Size = new Size(120, 16);
			this.checkBox127.TabIndex = 32;
			this.checkBox127.Text = "127_假期禁止通过";
			this.checkBox127.UseVisualStyleBackColor = true;
			this.checkBox105.AutoSize = true;
			this.checkBox105.Location = new Point(266, 112);
			this.checkBox105.Name = "checkBox105";
			this.checkBox105.Size = new Size(156, 16);
			this.checkBox105.TabIndex = 32;
			this.checkBox105.Text = "105_每个读卡器独立计次";
			this.checkBox105.UseVisualStyleBackColor = true;
			this.numericUpDown10.Location = new Point(396, 80);
			NumericUpDown arg_136F7_0 = this.numericUpDown10;
			array = new int[4];
			array[0] = 30;
			arg_136F7_0.Maximum = new decimal(array);
			this.numericUpDown10.Name = "numericUpDown10";
			this.numericUpDown10.Size = new Size(60, 21);
			this.numericUpDown10.TabIndex = 31;
			this.label94.AutoSize = true;
			this.label94.Location = new Point(259, 80);
			this.label94.Name = "label94";
			this.label94.Size = new Size(107, 24);
			this.label94.TabIndex = 30;
			this.label94.Text = "10_此时段当天限次\r\n[0不限,最大30次]";
			this.button18.Location = new Point(263, 358);
			this.button18.Name = "button18";
			this.button18.Size = new Size(103, 23);
			this.button18.TabIndex = 28;
			this.button18.Text = "18_更新时段[IP]";
			this.button18.UseVisualStyleBackColor = true;
			this.button18.Click += new EventHandler(this.button18_Click);
			this.groupBox11.Controls.Add(this.numericUpDown9);
			this.groupBox11.Controls.Add(this.label93);
			this.groupBox11.Controls.Add(this.numericUpDown8);
			this.groupBox11.Controls.Add(this.label92);
			this.groupBox11.Controls.Add(this.numericUpDown7);
			this.groupBox11.Controls.Add(this.label89);
			this.groupBox11.Controls.Add(this.label90);
			this.groupBox11.Controls.Add(this.label91);
			this.groupBox11.Controls.Add(this.dateTimePicker12);
			this.groupBox11.Controls.Add(this.dateTimePicker13);
			this.groupBox11.Controls.Add(this.label87);
			this.groupBox11.Controls.Add(this.label88);
			this.groupBox11.Controls.Add(this.dateTimePicker10);
			this.groupBox11.Controls.Add(this.dateTimePicker11);
			this.groupBox11.Controls.Add(this.label86);
			this.groupBox11.Controls.Add(this.label85);
			this.groupBox11.Controls.Add(this.dateTimePicker9);
			this.groupBox11.Controls.Add(this.dateTimePicker8);
			this.groupBox11.Location = new Point(231, 147);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new Size(295, 199);
			this.groupBox11.TabIndex = 27;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "每日有效时区";
			this.numericUpDown9.Location = new Point(196, 160);
			NumericUpDown arg_13A3D_0 = this.numericUpDown9;
			array = new int[4];
			array[0] = 30;
			arg_13A3D_0.Maximum = new decimal(array);
			this.numericUpDown9.Name = "numericUpDown9";
			this.numericUpDown9.Size = new Size(53, 21);
			this.numericUpDown9.TabIndex = 29;
			this.label93.AutoSize = true;
			this.label93.Location = new Point(72, 157);
			this.label93.Name = "label93";
			this.label93.Size = new Size(101, 24);
			this.label93.TabIndex = 28;
			this.label93.Text = "9_时区3限次\r\n[0不限,最大30次]";
			this.numericUpDown8.Location = new Point(196, 106);
			NumericUpDown arg_13B09_0 = this.numericUpDown8;
			array = new int[4];
			array[0] = 30;
			arg_13B09_0.Maximum = new decimal(array);
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new Size(53, 21);
			this.numericUpDown8.TabIndex = 27;
			this.label92.AutoSize = true;
			this.label92.Location = new Point(72, 104);
			this.label92.Name = "label92";
			this.label92.Size = new Size(101, 24);
			this.label92.TabIndex = 26;
			this.label92.Text = "8_时区2限次\r\n[0不限,最大30次]";
			this.numericUpDown7.Location = new Point(196, 49);
			NumericUpDown arg_13BD2_0 = this.numericUpDown7;
			array = new int[4];
			array[0] = 30;
			arg_13BD2_0.Maximum = new decimal(array);
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new Size(53, 21);
			this.numericUpDown7.TabIndex = 25;
			this.label89.AutoSize = true;
			this.label89.Location = new Point(173, 137);
			this.label89.Name = "label89";
			this.label89.Size = new Size(17, 12);
			this.label89.TabIndex = 23;
			this.label89.Text = "--";
			this.label90.AutoSize = true;
			this.label90.Location = new Point(10, 137);
			this.label90.Name = "label90";
			this.label90.Size = new Size(53, 12);
			this.label90.TabIndex = 22;
			this.label90.Text = "时区3_12";
			this.label91.AutoSize = true;
			this.label91.Location = new Point(72, 48);
			this.label91.Name = "label91";
			this.label91.Size = new Size(107, 24);
			this.label91.TabIndex = 24;
			this.label91.Text = "7_时区1限次\r\n[0不限, 最大30次]";
			this.dateTimePicker12.CustomFormat = "HH:mm";
			this.dateTimePicker12.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker12.Location = new Point(102, 133);
			this.dateTimePicker12.Name = "dateTimePicker12";
			this.dateTimePicker12.ShowUpDown = true;
			this.dateTimePicker12.Size = new Size(62, 21);
			this.dateTimePicker12.TabIndex = 21;
			this.dateTimePicker12.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker13.CustomFormat = "HH:mm";
			this.dateTimePicker13.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker13.Location = new Point(196, 132);
			this.dateTimePicker13.Name = "dateTimePicker13";
			this.dateTimePicker13.ShowUpDown = true;
			this.dateTimePicker13.Size = new Size(62, 21);
			this.dateTimePicker13.TabIndex = 20;
			this.dateTimePicker13.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.label87.AutoSize = true;
			this.label87.Location = new Point(173, 84);
			this.label87.Name = "label87";
			this.label87.Size = new Size(17, 12);
			this.label87.TabIndex = 19;
			this.label87.Text = "--";
			this.label88.AutoSize = true;
			this.label88.Location = new Point(10, 84);
			this.label88.Name = "label88";
			this.label88.Size = new Size(53, 12);
			this.label88.TabIndex = 18;
			this.label88.Text = "时区2_10";
			this.dateTimePicker10.CustomFormat = "HH:mm";
			this.dateTimePicker10.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker10.Location = new Point(102, 80);
			this.dateTimePicker10.Name = "dateTimePicker10";
			this.dateTimePicker10.ShowUpDown = true;
			this.dateTimePicker10.Size = new Size(62, 21);
			this.dateTimePicker10.TabIndex = 17;
			this.dateTimePicker10.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker11.CustomFormat = "HH:mm";
			this.dateTimePicker11.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker11.Location = new Point(196, 80);
			this.dateTimePicker11.Name = "dateTimePicker11";
			this.dateTimePicker11.ShowUpDown = true;
			this.dateTimePicker11.Size = new Size(62, 21);
			this.dateTimePicker11.TabIndex = 16;
			this.dateTimePicker11.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.label86.AutoSize = true;
			this.label86.Location = new Point(173, 28);
			this.label86.Name = "label86";
			this.label86.Size = new Size(17, 12);
			this.label86.TabIndex = 15;
			this.label86.Text = "--";
			this.label85.AutoSize = true;
			this.label85.Location = new Point(10, 28);
			this.label85.Name = "label85";
			this.label85.Size = new Size(47, 12);
			this.label85.TabIndex = 14;
			this.label85.Text = "时区1_8";
			this.dateTimePicker9.CustomFormat = "HH:mm";
			this.dateTimePicker9.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker9.Location = new Point(196, 24);
			this.dateTimePicker9.Name = "dateTimePicker9";
			this.dateTimePicker9.ShowUpDown = true;
			this.dateTimePicker9.Size = new Size(62, 21);
			this.dateTimePicker9.TabIndex = 13;
			this.dateTimePicker9.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			this.dateTimePicker8.CustomFormat = "HH:mm";
			this.dateTimePicker8.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker8.Location = new Point(102, 24);
			this.dateTimePicker8.Name = "dateTimePicker8";
			this.dateTimePicker8.ShowUpDown = true;
			this.dateTimePicker8.Size = new Size(62, 21);
			this.dateTimePicker8.TabIndex = 12;
			this.dateTimePicker8.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.groupBox10.Controls.Add(this.checkBox98);
			this.groupBox10.Controls.Add(this.checkBox104);
			this.groupBox10.Controls.Add(this.checkBox99);
			this.groupBox10.Controls.Add(this.checkBox103);
			this.groupBox10.Controls.Add(this.checkBox100);
			this.groupBox10.Controls.Add(this.checkBox102);
			this.groupBox10.Controls.Add(this.checkBox101);
			this.groupBox10.Location = new Point(90, 145);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new Size(117, 201);
			this.groupBox10.TabIndex = 26;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "有效星期选项";
			this.checkBox98.AutoSize = true;
			this.checkBox98.Checked = true;
			this.checkBox98.CheckState = CheckState.Checked;
			this.checkBox98.Location = new Point(6, 29);
			this.checkBox98.Name = "checkBox98";
			this.checkBox98.Size = new Size(78, 16);
			this.checkBox98.TabIndex = 19;
			this.checkBox98.Text = "98_星期一";
			this.checkBox98.UseVisualStyleBackColor = true;
			this.checkBox104.AutoSize = true;
			this.checkBox104.Checked = true;
			this.checkBox104.CheckState = CheckState.Checked;
			this.checkBox104.Location = new Point(6, 161);
			this.checkBox104.Name = "checkBox104";
			this.checkBox104.Size = new Size(84, 16);
			this.checkBox104.TabIndex = 25;
			this.checkBox104.Text = "104_星期日";
			this.checkBox104.UseVisualStyleBackColor = true;
			this.checkBox99.AutoSize = true;
			this.checkBox99.Checked = true;
			this.checkBox99.CheckState = CheckState.Checked;
			this.checkBox99.Location = new Point(6, 51);
			this.checkBox99.Name = "checkBox99";
			this.checkBox99.Size = new Size(78, 16);
			this.checkBox99.TabIndex = 20;
			this.checkBox99.Text = "99_星期二";
			this.checkBox99.UseVisualStyleBackColor = true;
			this.checkBox103.AutoSize = true;
			this.checkBox103.Checked = true;
			this.checkBox103.CheckState = CheckState.Checked;
			this.checkBox103.Location = new Point(6, 139);
			this.checkBox103.Name = "checkBox103";
			this.checkBox103.Size = new Size(84, 16);
			this.checkBox103.TabIndex = 24;
			this.checkBox103.Text = "103_星期六";
			this.checkBox103.UseVisualStyleBackColor = true;
			this.checkBox100.AutoSize = true;
			this.checkBox100.Checked = true;
			this.checkBox100.CheckState = CheckState.Checked;
			this.checkBox100.Location = new Point(6, 73);
			this.checkBox100.Name = "checkBox100";
			this.checkBox100.Size = new Size(84, 16);
			this.checkBox100.TabIndex = 21;
			this.checkBox100.Text = "100_星期三";
			this.checkBox100.UseVisualStyleBackColor = true;
			this.checkBox102.AutoSize = true;
			this.checkBox102.Checked = true;
			this.checkBox102.CheckState = CheckState.Checked;
			this.checkBox102.Location = new Point(6, 117);
			this.checkBox102.Name = "checkBox102";
			this.checkBox102.Size = new Size(84, 16);
			this.checkBox102.TabIndex = 23;
			this.checkBox102.Text = "102_星期五";
			this.checkBox102.UseVisualStyleBackColor = true;
			this.checkBox101.AutoSize = true;
			this.checkBox101.Checked = true;
			this.checkBox101.CheckState = CheckState.Checked;
			this.checkBox101.Location = new Point(6, 95);
			this.checkBox101.Name = "checkBox101";
			this.checkBox101.Size = new Size(84, 16);
			this.checkBox101.TabIndex = 22;
			this.checkBox101.Text = "101_星期四";
			this.checkBox101.UseVisualStyleBackColor = true;
			this.label84.AutoSize = true;
			this.label84.Location = new Point(259, 52);
			this.label84.Name = "label84";
			this.label84.Size = new Size(107, 12);
			this.label84.TabIndex = 17;
			this.label84.Text = "58_下一个链接时段";
			this.comboBox58.FormattingEnabled = true;
			this.comboBox58.Items.AddRange(new object[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10"
			});
			this.comboBox58.Location = new Point(396, 49);
			this.comboBox58.Name = "comboBox58";
			this.comboBox58.Size = new Size(60, 20);
			this.comboBox58.TabIndex = 18;
			this.comboBox58.Text = "0";
			this.label83.AutoSize = true;
			this.label83.Location = new Point(25, 23);
			this.label83.Name = "label83";
			this.label83.Size = new Size(41, 12);
			this.label83.TabIndex = 15;
			this.label83.Text = "时段57";
			this.comboBox57.FormattingEnabled = true;
			this.comboBox57.Items.AddRange(new object[]
			{
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10"
			});
			this.comboBox57.Location = new Point(91, 20);
			this.comboBox57.Name = "comboBox57";
			this.comboBox57.Size = new Size(116, 20);
			this.comboBox57.TabIndex = 16;
			this.comboBox57.Text = "2";
			this.dateTimePicker6.Location = new Point(91, 73);
			this.dateTimePicker6.Name = "dateTimePicker6";
			this.dateTimePicker6.Size = new Size(116, 21);
			this.dateTimePicker6.TabIndex = 14;
			this.dateTimePicker6.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			this.dateTimePicker7.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker7.Location = new Point(90, 46);
			this.dateTimePicker7.Name = "dateTimePicker7";
			this.dateTimePicker7.Size = new Size(117, 21);
			this.dateTimePicker7.TabIndex = 13;
			this.dateTimePicker7.Value = new DateTime(2009, 1, 1, 18, 18, 0, 0);
			this.label81.AutoSize = true;
			this.label81.Location = new Point(50, 77);
			this.label81.Name = "label81";
			this.label81.Size = new Size(35, 12);
			this.label81.TabIndex = 12;
			this.label81.Text = "截止6";
			this.label82.AutoSize = true;
			this.label82.Location = new Point(49, 51);
			this.label82.Name = "label82";
			this.label82.Size = new Size(35, 12);
			this.label82.TabIndex = 11;
			this.label82.Text = "起始7";
			this.tabPage11.Controls.Add(this.label99);
			this.tabPage11.Controls.Add(this.numericUpDown14);
			this.tabPage11.Controls.Add(this.label98);
			this.tabPage11.Controls.Add(this.label97);
			this.tabPage11.Controls.Add(this.numericUpDown13);
			this.tabPage11.Controls.Add(this.button22);
			this.tabPage11.Controls.Add(this.button23);
			this.tabPage11.Location = new Point(4, 76);
			this.tabPage11.Name = "tabPage11";
			this.tabPage11.Padding = new Padding(3);
			this.tabPage11.Size = new Size(563, 628);
			this.tabPage11.TabIndex = 10;
			this.tabPage11.Text = "FRam管理";
			this.tabPage11.UseVisualStyleBackColor = true;
			this.label99.AutoSize = true;
			this.label99.Location = new Point(272, 79);
			this.label99.Name = "label99";
			this.label99.Size = new Size(101, 72);
			this.label99.TabIndex = 22;
			this.label99.Text = "读卡器合法卡计数\r\n4->1号读卡器\r\n5->2号读卡器\r\n6->3号读卡器\r\n7->4号读卡器\r\n\r\n";
			this.numericUpDown14.Location = new Point(131, 117);
			NumericUpDown arg_14C42_0 = this.numericUpDown14;
			array = new int[4];
			array[0] = 16777214;
			arg_14C42_0.Maximum = new decimal(array);
			this.numericUpDown14.Name = "numericUpDown14";
			this.numericUpDown14.Size = new Size(98, 21);
			this.numericUpDown14.TabIndex = 21;
			this.label98.AutoSize = true;
			this.label98.Location = new Point(45, 119);
			this.label98.Name = "label98";
			this.label98.Size = new Size(47, 12);
			this.label98.TabIndex = 20;
			this.label98.Text = "14_新值";
			this.label97.AutoSize = true;
			this.label97.Location = new Point(45, 89);
			this.label97.Name = "label97";
			this.label97.Size = new Size(71, 12);
			this.label97.TabIndex = 19;
			this.label97.Text = "13_参数索引";
			this.numericUpDown13.Location = new Point(131, 87);
			NumericUpDown arg_14D6C_0 = this.numericUpDown13;
			array = new int[4];
			array[0] = 15;
			arg_14D6C_0.Maximum = new decimal(array);
			this.numericUpDown13.Name = "numericUpDown13";
			this.numericUpDown13.Size = new Size(98, 21);
			this.numericUpDown13.TabIndex = 18;
			this.button22.Location = new Point(47, 40);
			this.button22.Name = "button22";
			this.button22.Size = new Size(161, 23);
			this.button22.TabIndex = 17;
			this.button22.Text = "22_读取FRam参数[IP]";
			this.button22.UseVisualStyleBackColor = true;
			this.button22.Click += new EventHandler(this.button22_Click);
			this.button23.Location = new Point(47, 165);
			this.button23.Name = "button23";
			this.button23.Size = new Size(161, 23);
			this.button23.TabIndex = 16;
			this.button23.Text = "23_设定新值[IP]";
			this.button23.UseVisualStyleBackColor = true;
			this.button23.Click += new EventHandler(this.button23_Click);
			this.tabPage12.Controls.Add(this.numericUpDown27);
			this.tabPage12.Controls.Add(this.label156);
			this.tabPage12.Controls.Add(this.button89);
			this.tabPage12.Controls.Add(this.label154);
			this.tabPage12.Controls.Add(this.label155);
			this.tabPage12.Controls.Add(this.label152);
			this.tabPage12.Controls.Add(this.numericUpDown25);
			this.tabPage12.Controls.Add(this.label153);
			this.tabPage12.Controls.Add(this.numericUpDown26);
			this.tabPage12.Controls.Add(this.button26);
			this.tabPage12.Controls.Add(this.label107);
			this.tabPage12.Controls.Add(this.label104);
			this.tabPage12.Controls.Add(this.nudDatalen);
			this.tabPage12.Controls.Add(this.nudValue);
			this.tabPage12.Controls.Add(this.label105);
			this.tabPage12.Controls.Add(this.label106);
			this.tabPage12.Controls.Add(this.label100);
			this.tabPage12.Controls.Add(this.label101);
			this.tabPage12.Controls.Add(this.nudEndPage);
			this.tabPage12.Controls.Add(this.label102);
			this.tabPage12.Controls.Add(this.label103);
			this.tabPage12.Controls.Add(this.nudStartPage);
			this.tabPage12.Controls.Add(this.button25);
			this.tabPage12.Location = new Point(4, 76);
			this.tabPage12.Name = "tabPage12";
			this.tabPage12.Padding = new Padding(3);
			this.tabPage12.Size = new Size(563, 628);
			this.tabPage12.TabIndex = 11;
			this.tabPage12.Text = "直接对DATAFLASH操作";
			this.tabPage12.UseVisualStyleBackColor = true;
			this.numericUpDown27.Location = new Point(70, 310);
			NumericUpDown arg_15141_0 = this.numericUpDown27;
			array = new int[4];
			array[0] = 15000;
			arg_15141_0.Maximum = new decimal(array);
			NumericUpDown arg_15160_0 = this.numericUpDown27;
			array = new int[4];
			array[0] = 1;
			arg_15160_0.Minimum = new decimal(array);
			this.numericUpDown27.Name = "numericUpDown27";
			this.numericUpDown27.Size = new Size(84, 21);
			this.numericUpDown27.TabIndex = 36;
			NumericUpDown arg_151B0_0 = this.numericUpDown27;
			array = new int[4];
			array[0] = 1;
			arg_151B0_0.Value = new decimal(array);
			this.label156.AutoSize = true;
			this.label156.Location = new Point(11, 313);
			this.label156.Name = "label156";
			this.label156.Size = new Size(59, 12);
			this.label156.TabIndex = 35;
			this.label156.Text = "循环次数:";
			this.button89.Location = new Point(248, 256);
			this.button89.Name = "button89";
			this.button89.Size = new Size(195, 23);
			this.button89.TabIndex = 34;
			this.button89.Text = "89 读取指定的2页....";
			this.button89.UseVisualStyleBackColor = true;
			this.button89.Click += new EventHandler(this.button89_Click);
			this.label154.AutoSize = true;
			this.label154.Location = new Point(11, 285);
			this.label154.Name = "label154";
			this.label154.Size = new Size(47, 12);
			this.label154.TabIndex = 33;
			this.label154.Text = "第2页面";
			this.label155.AutoSize = true;
			this.label155.Location = new Point(11, 258);
			this.label155.Name = "label155";
			this.label155.Size = new Size(47, 12);
			this.label155.TabIndex = 32;
			this.label155.Text = "第1页面";
			this.label152.AutoSize = true;
			this.label152.Location = new Point(160, 285);
			this.label152.Name = "label152";
			this.label152.Size = new Size(59, 12);
			this.label152.TabIndex = 31;
			this.label152.Text = "*1024字节";
			this.numericUpDown25.Location = new Point(70, 256);
			NumericUpDown arg_153FE_0 = this.numericUpDown25;
			array = new int[4];
			array[0] = 8192;
			arg_153FE_0.Maximum = new decimal(array);
			this.numericUpDown25.Name = "numericUpDown25";
			this.numericUpDown25.Size = new Size(84, 21);
			this.numericUpDown25.TabIndex = 30;
			this.label153.AutoSize = true;
			this.label153.Location = new Point(160, 258);
			this.label153.Name = "label153";
			this.label153.Size = new Size(59, 12);
			this.label153.TabIndex = 29;
			this.label153.Text = "*1024字节";
			this.numericUpDown26.Location = new Point(70, 283);
			NumericUpDown arg_154D0_0 = this.numericUpDown26;
			array = new int[4];
			array[0] = 8192;
			arg_154D0_0.Maximum = new decimal(array);
			this.numericUpDown26.Name = "numericUpDown26";
			this.numericUpDown26.Size = new Size(84, 21);
			this.numericUpDown26.TabIndex = 28;
			NumericUpDown arg_15524_0 = this.numericUpDown26;
			array = new int[4];
			array[0] = 1616;
			arg_15524_0.Value = new decimal(array);
			this.button26.Location = new Point(248, 185);
			this.button26.Name = "button26";
			this.button26.Size = new Size(111, 23);
			this.button26.TabIndex = 27;
			this.button26.Text = "26 读取指定开始页";
			this.button26.UseVisualStyleBackColor = true;
			this.button26.Click += new EventHandler(this.button26_Click);
			this.label107.AutoSize = true;
			this.label107.Location = new Point(68, 190);
			this.label107.Name = "label107";
			this.label107.Size = new Size(53, 12);
			this.label107.TabIndex = 26;
			this.label107.Text = "label107";
			this.label104.AutoSize = true;
			this.label104.Location = new Point(11, 110);
			this.label104.Name = "label104";
			this.label104.Size = new Size(53, 12);
			this.label104.TabIndex = 25;
			this.label104.Text = "数据长度";
			this.nudDatalen.Location = new Point(70, 108);
			NumericUpDown arg_1569E_0 = this.nudDatalen;
			array = new int[4];
			array[0] = 1024;
			arg_1569E_0.Maximum = new decimal(array);
			NumericUpDown arg_156BD_0 = this.nudDatalen;
			array = new int[4];
			array[0] = 1;
			arg_156BD_0.Minimum = new decimal(array);
			this.nudDatalen.Name = "nudDatalen";
			this.nudDatalen.Size = new Size(84, 21);
			this.nudDatalen.TabIndex = 24;
			NumericUpDown arg_1570E_0 = this.nudDatalen;
			array = new int[4];
			array[0] = 16;
			arg_1570E_0.Value = new decimal(array);
			this.nudValue.Location = new Point(70, 143);
			NumericUpDown arg_15745_0 = this.nudValue;
			array = new int[4];
			array[0] = 15;
			arg_15745_0.Maximum = new decimal(array);
			this.nudValue.Name = "nudValue";
			this.nudValue.Size = new Size(84, 21);
			this.nudValue.TabIndex = 23;
			NumericUpDown arg_15796_0 = this.nudValue;
			array = new int[4];
			array[0] = 15;
			arg_15796_0.Value = new decimal(array);
			this.label105.AutoSize = true;
			this.label105.Location = new Point(11, 146);
			this.label105.Name = "label105";
			this.label105.Size = new Size(47, 12);
			this.label105.TabIndex = 22;
			this.label105.Text = "新的值:";
			this.label106.AutoSize = true;
			this.label106.Location = new Point(160, 146);
			this.label106.Name = "label106";
			this.label106.Size = new Size(119, 12);
			this.label106.TabIndex = 21;
			this.label106.Text = "*17  (相当于是0x11)";
			this.label100.AutoSize = true;
			this.label100.Location = new Point(11, 74);
			this.label100.Name = "label100";
			this.label100.Size = new Size(53, 12);
			this.label100.TabIndex = 20;
			this.label100.Text = "结束页面";
			this.label101.AutoSize = true;
			this.label101.Location = new Point(160, 74);
			this.label101.Name = "label101";
			this.label101.Size = new Size(59, 12);
			this.label101.TabIndex = 19;
			this.label101.Text = "*1024字节";
			this.nudEndPage.Location = new Point(70, 72);
			NumericUpDown arg_1595D_0 = this.nudEndPage;
			array = new int[4];
			array[0] = 8192;
			arg_1595D_0.Maximum = new decimal(array);
			this.nudEndPage.Name = "nudEndPage";
			this.nudEndPage.Size = new Size(84, 21);
			this.nudEndPage.TabIndex = 18;
			this.label102.AutoSize = true;
			this.label102.Location = new Point(11, 47);
			this.label102.Name = "label102";
			this.label102.Size = new Size(53, 12);
			this.label102.TabIndex = 17;
			this.label102.Text = "开始页面";
			this.label103.AutoSize = true;
			this.label103.Location = new Point(160, 47);
			this.label103.Name = "label103";
			this.label103.Size = new Size(59, 12);
			this.label103.TabIndex = 16;
			this.label103.Text = "*1024字节";
			this.nudStartPage.Location = new Point(70, 45);
			NumericUpDown arg_15A8A_0 = this.nudStartPage;
			array = new int[4];
			array[0] = 8192;
			arg_15A8A_0.Maximum = new decimal(array);
			this.nudStartPage.Name = "nudStartPage";
			this.nudStartPage.Size = new Size(84, 21);
			this.nudStartPage.TabIndex = 15;
			this.button25.Location = new Point(242, 42);
			this.button25.Name = "button25";
			this.button25.Size = new Size(117, 23);
			this.button25.TabIndex = 0;
			this.button25.Text = "25 修改值";
			this.button25.UseVisualStyleBackColor = true;
			this.button25.Click += new EventHandler(this.button25_Click);
			this.tabPage13.Controls.Add(this.checkBox135);
			this.tabPage13.Controls.Add(this.button83);
			this.tabPage13.Controls.Add(this.label146);
			this.tabPage13.Controls.Add(this.txtOldCommPassword);
			this.tabPage13.Controls.Add(this.textBox32);
			this.tabPage13.Controls.Add(this.label110);
			this.tabPage13.Controls.Add(this.button71);
			this.tabPage13.Controls.Add(this.txtCommPassword);
			this.tabPage13.Controls.Add(this.button57);
			this.tabPage13.Controls.Add(this.button35);
			this.tabPage13.Controls.Add(this.button56);
			this.tabPage13.Controls.Add(this.button54);
			this.tabPage13.Controls.Add(this.button52);
			this.tabPage13.Controls.Add(this.button36);
			this.tabPage13.Controls.Add(this.checkBox117);
			this.tabPage13.Controls.Add(this.label111);
			this.tabPage13.Controls.Add(this.checkBox116);
			this.tabPage13.Controls.Add(this.checkBox118);
			this.tabPage13.Controls.Add(this.checkBox115);
			this.tabPage13.Controls.Add(this.checkBox114);
			this.tabPage13.Controls.Add(this.checkBox113);
			this.tabPage13.Controls.Add(this.txt02e2);
			this.tabPage13.Controls.Add(this.label109);
			this.tabPage13.Controls.Add(this.label108);
			this.tabPage13.Controls.Add(this.nudNewSN);
			this.tabPage13.Controls.Add(this.button28);
			this.tabPage13.Controls.Add(this.button27);
			this.tabPage13.Location = new Point(4, 76);
			this.tabPage13.Name = "tabPage13";
			this.tabPage13.Padding = new Padding(3);
			this.tabPage13.Size = new Size(563, 628);
			this.tabPage13.TabIndex = 12;
			this.tabPage13.Text = "13 Cipher";
			this.tabPage13.UseVisualStyleBackColor = true;
			this.tabPage13.Click += new EventHandler(this.tabPage13_Click);
			this.checkBox135.AutoSize = true;
			this.checkBox135.ImeMode = ImeMode.NoControl;
			this.checkBox135.Location = new Point(14, 373);
			this.checkBox135.Name = "checkBox135";
			this.checkBox135.Size = new Size(186, 16);
			this.checkBox135.TabIndex = 25;
			this.checkBox135.Text = "135 通信丢包检测(200小,2大)";
			this.checkBox135.UseVisualStyleBackColor = true;
			this.button83.Location = new Point(143, 576);
			this.button83.Name = "button83";
			this.button83.Size = new Size(114, 38);
			this.button83.TabIndex = 24;
			this.button83.Text = "83测试丢包[接单台](200小,200大)";
			this.button83.UseVisualStyleBackColor = true;
			this.button83.Click += new EventHandler(this.button83_Click);
			this.label146.AutoSize = true;
			this.label146.Location = new Point(15, 415);
			this.label146.Name = "label146";
			this.label146.Size = new Size(101, 12);
			this.label146.TabIndex = 23;
			this.label146.Text = "用 逗号 分开卡号";
			this.txtOldCommPassword.Location = new Point(396, 254);
			this.txtOldCommPassword.MaxLength = 16;
			this.txtOldCommPassword.Name = "txtOldCommPassword";
			this.txtOldCommPassword.Size = new Size(133, 21);
			this.txtOldCommPassword.TabIndex = 8;
			this.textBox32.Location = new Point(16, 430);
			this.textBox32.Multiline = true;
			this.textBox32.Name = "textBox32";
			this.textBox32.Size = new Size(187, 140);
			this.textBox32.TabIndex = 22;
			this.textBox32.Text = "7314494,  3659085, 707080, 3654261, 20760517, 3660918";
			this.label110.AutoSize = true;
			this.label110.Location = new Point(319, 285);
			this.label110.Name = "label110";
			this.label110.Size = new Size(59, 12);
			this.label110.TabIndex = 3;
			this.label110.Text = "通信密码:";
			this.button71.Location = new Point(162, 131);
			this.button71.Name = "button71";
			this.button71.Size = new Size(65, 23);
			this.button71.TabIndex = 21;
			this.button71.Text = "71_隐藏";
			this.button71.UseVisualStyleBackColor = true;
			this.button71.Click += new EventHandler(this.button71_Click);
			this.txtCommPassword.Location = new Point(396, 282);
			this.txtCommPassword.MaxLength = 16;
			this.txtCommPassword.Name = "txtCommPassword";
			this.txtCommPassword.Size = new Size(133, 21);
			this.txtCommPassword.TabIndex = 4;
			this.txtCommPassword.Text = "mypassword123456";
			this.button57.Location = new Point(11, 576);
			this.button57.Name = "button57";
			this.button57.Size = new Size(120, 38);
			this.button57.TabIndex = 20;
			this.button57.Text = "57 先作参数初始化 再作特殊设置 ";
			this.button57.UseVisualStyleBackColor = true;
			this.button57.Click += new EventHandler(this.button57_Click);
			this.button35.Location = new Point(333, 323);
			this.button35.Name = "button35";
			this.button35.Size = new Size(126, 23);
			this.button35.TabIndex = 5;
			this.button35.Text = "35设置通信密码";
			this.button35.UseVisualStyleBackColor = true;
			this.button35.Click += new EventHandler(this.button35_Click);
			this.button56.Location = new Point(432, 55);
			this.button56.Name = "button56";
			this.button56.Size = new Size(75, 23);
			this.button56.TabIndex = 19;
			this.button56.Text = "56 取原SN";
			this.button56.UseVisualStyleBackColor = true;
			this.button56.Click += new EventHandler(this.button56_Click);
			this.button54.Location = new Point(333, 485);
			this.button54.Name = "button54";
			this.button54.Size = new Size(148, 23);
			this.button54.TabIndex = 17;
			this.button54.Text = "54 产品详细信息[IP]";
			this.button54.UseVisualStyleBackColor = true;
			this.button54.Click += new EventHandler(this.button54_Click);
			this.button52.Location = new Point(6, 37);
			this.button52.Name = "button52";
			this.button52.Size = new Size(151, 54);
			this.button52.TabIndex = 16;
			this.button52.Text = "52 准备重新code";
			this.button52.UseVisualStyleBackColor = true;
			this.button52.Click += new EventHandler(this.button52_Click);
			this.button36.Location = new Point(333, 368);
			this.button36.Name = "button36";
			this.button36.Size = new Size(126, 23);
			this.button36.TabIndex = 6;
			this.button36.Text = "36保存通信密码";
			this.button36.UseVisualStyleBackColor = true;
			this.button36.Click += new EventHandler(this.button36_Click);
			this.checkBox117.AutoSize = true;
			this.checkBox117.Checked = true;
			this.checkBox117.CheckState = CheckState.Checked;
			this.checkBox117.Location = new Point(14, 350);
			this.checkBox117.Name = "checkBox117";
			this.checkBox117.Size = new Size(156, 16);
			this.checkBox117.TabIndex = 15;
			this.checkBox117.Text = "117 门磁对应扩展板输出";
			this.checkBox117.UseVisualStyleBackColor = true;
			this.label111.AutoSize = true;
			this.label111.Location = new Point(319, 257);
			this.label111.Name = "label111";
			this.label111.Size = new Size(71, 12);
			this.label111.TabIndex = 7;
			this.label111.Text = "旧通信密码:";
			this.checkBox116.AutoSize = true;
			this.checkBox116.Checked = true;
			this.checkBox116.CheckState = CheckState.Checked;
			this.checkBox116.Location = new Point(14, 395);
			this.checkBox116.Name = "checkBox116";
			this.checkBox116.Size = new Size(120, 16);
			this.checkBox116.TabIndex = 12;
			this.checkBox116.Text = "116 加入卡号权限";
			this.checkBox116.UseVisualStyleBackColor = true;
			this.checkBox118.AutoSize = true;
			this.checkBox118.Checked = true;
			this.checkBox118.CheckState = CheckState.Checked;
			this.checkBox118.Location = new Point(6, 15);
			this.checkBox118.Name = "checkBox118";
			this.checkBox118.Size = new Size(162, 16);
			this.checkBox118.TabIndex = 11;
			this.checkBox118.Text = "118 先取原SN,再准备重新";
			this.checkBox118.UseVisualStyleBackColor = true;
			this.checkBox115.AutoSize = true;
			this.checkBox115.Checked = true;
			this.checkBox115.CheckState = CheckState.Checked;
			this.checkBox115.Location = new Point(14, 257);
			this.checkBox115.Name = "checkBox115";
			this.checkBox115.Size = new Size(132, 16);
			this.checkBox115.TabIndex = 11;
			this.checkBox115.Text = "115 F12 相当于点28";
			this.checkBox115.UseVisualStyleBackColor = true;
			this.checkBox114.AutoSize = true;
			this.checkBox114.Checked = true;
			this.checkBox114.CheckState = CheckState.Checked;
			this.checkBox114.Location = new Point(14, 305);
			this.checkBox114.Name = "checkBox114";
			this.checkBox114.Size = new Size(96, 16);
			this.checkBox114.TabIndex = 10;
			this.checkBox114.Text = "114 自动递增";
			this.checkBox114.UseVisualStyleBackColor = true;
			this.checkBox113.AutoSize = true;
			this.checkBox113.Checked = true;
			this.checkBox113.CheckState = CheckState.Checked;
			this.checkBox113.Location = new Point(14, 327);
			this.checkBox113.Name = "checkBox113";
			this.checkBox113.Size = new Size(120, 16);
			this.checkBox113.TabIndex = 9;
			this.checkBox113.Text = "113 同时校准时间";
			this.checkBox113.UseVisualStyleBackColor = true;
			this.txt02e2.Location = new Point(56, 133);
			this.txt02e2.Name = "txt02e2";
			this.txt02e2.PasswordChar = 'x';
			this.txt02e2.Size = new Size(100, 21);
			this.txt02e2.TabIndex = 4;
			this.label109.AutoSize = true;
			this.label109.Location = new Point(6, 136);
			this.label109.Name = "label109";
			this.label109.Size = new Size(35, 12);
			this.label109.TabIndex = 3;
			this.label109.Text = "指令:";
			this.label108.AutoSize = true;
			this.label108.Location = new Point(6, 108);
			this.label108.Name = "label108";
			this.label108.Size = new Size(47, 12);
			this.label108.TabIndex = 3;
			this.label108.Text = "New SN:";
			this.nudNewSN.Location = new Point(56, 106);
			NumericUpDown arg_1695A_0 = this.nudNewSN;
			array = new int[4];
			array[0] = 999999999;
			arg_1695A_0.Maximum = new decimal(array);
			NumericUpDown arg_1697D_0 = this.nudNewSN;
			array = new int[4];
			array[0] = 100000000;
			arg_1697D_0.Minimum = new decimal(array);
			this.nudNewSN.Name = "nudNewSN";
			this.nudNewSN.Size = new Size(89, 21);
			this.nudNewSN.TabIndex = 2;
			NumericUpDown arg_169D0_0 = this.nudNewSN;
			array = new int[4];
			array[0] = 401000002;
			arg_169D0_0.Value = new decimal(array);
			this.button28.Location = new Point(6, 160);
			this.button28.Name = "button28";
			this.button28.Size = new Size(220, 86);
			this.button28.TabIndex = 1;
			this.button28.Text = "28 Code";
			this.button28.UseVisualStyleBackColor = true;
			this.button28.Click += new EventHandler(this.button28_Click);
			this.button27.Location = new Point(432, 23);
			this.button27.Name = "button27";
			this.button27.Size = new Size(75, 23);
			this.button27.TabIndex = 0;
			this.button27.Text = "button27";
			this.button27.UseVisualStyleBackColor = true;
			this.button27.Click += new EventHandler(this.button27_Click);
			this.tabPage14.Controls.Add(this.button33);
			this.tabPage14.Controls.Add(this.button30);
			this.tabPage14.Location = new Point(4, 76);
			this.tabPage14.Name = "tabPage14";
			this.tabPage14.Padding = new Padding(3);
			this.tabPage14.Size = new Size(563, 628);
			this.tabPage14.TabIndex = 13;
			this.tabPage14.Text = "数据库相关操作";
			this.tabPage14.UseVisualStyleBackColor = true;
			this.button33.Location = new Point(16, 76);
			this.button33.Name = "button33";
			this.button33.Size = new Size(135, 23);
			this.button33.TabIndex = 8;
			this.button33.Text = "33 列举数据库服务器";
			this.button33.UseVisualStyleBackColor = true;
			this.button33.Click += new EventHandler(this.button33_Click);
			this.button30.Location = new Point(16, 27);
			this.button30.Name = "button30";
			this.button30.Size = new Size(135, 23);
			this.button30.TabIndex = 7;
			this.button30.Text = "30 数据库设置";
			this.button30.UseVisualStyleBackColor = true;
			this.button30.Click += new EventHandler(this.button30_Click);
			this.tabPage15.Controls.Add(this.button42);
			this.tabPage15.Controls.Add(this.button41);
			this.tabPage15.Controls.Add(this.button40);
			this.tabPage15.Controls.Add(this.button39);
			this.tabPage15.Controls.Add(this.label113);
			this.tabPage15.Controls.Add(this.button38);
			this.tabPage15.Controls.Add(this.button37);
			this.tabPage15.Controls.Add(this.label112);
			this.tabPage15.Controls.Add(this.textBox21);
			this.tabPage15.Location = new Point(4, 76);
			this.tabPage15.Name = "tabPage15";
			this.tabPage15.Padding = new Padding(3);
			this.tabPage15.Size = new Size(563, 628);
			this.tabPage15.TabIndex = 14;
			this.tabPage15.Text = "TCP连接测试";
			this.tabPage15.UseVisualStyleBackColor = true;
			this.button42.Location = new Point(103, 243);
			this.button42.Name = "button42";
			this.button42.Size = new Size(155, 23);
			this.button42.TabIndex = 8;
			this.button42.Text = "42 产品信息";
			this.button42.UseVisualStyleBackColor = true;
			this.button42.Click += new EventHandler(this.button42_Click);
			this.button41.Location = new Point(103, 200);
			this.button41.Name = "button41";
			this.button41.Size = new Size(155, 23);
			this.button41.TabIndex = 7;
			this.button41.Text = "41 远程开门";
			this.button41.UseVisualStyleBackColor = true;
			this.button41.Click += new EventHandler(this.button41_Click);
			this.button40.Location = new Point(103, 161);
			this.button40.Name = "button40";
			this.button40.Size = new Size(155, 23);
			this.button40.TabIndex = 6;
			this.button40.Text = "40 校准时间";
			this.button40.UseVisualStyleBackColor = true;
			this.button40.Click += new EventHandler(this.button40_Click);
			this.button39.Location = new Point(346, 85);
			this.button39.Name = "button39";
			this.button39.Size = new Size(142, 23);
			this.button39.TabIndex = 5;
			this.button39.Text = "39 打开10个窗体";
			this.button39.UseVisualStyleBackColor = true;
			this.button39.Click += new EventHandler(this.button39_Click);
			this.label113.AutoSize = true;
			this.label113.Location = new Point(101, 61);
			this.label113.Name = "label113";
			this.label113.Size = new Size(53, 12);
			this.label113.TabIndex = 4;
			this.label113.Text = "label113";
			this.button38.Location = new Point(333, 125);
			this.button38.Name = "button38";
			this.button38.Size = new Size(155, 23);
			this.button38.TabIndex = 3;
			this.button38.Text = "38 断开连接";
			this.button38.UseVisualStyleBackColor = true;
			this.button38.Click += new EventHandler(this.button38_Click);
			this.button37.Location = new Point(103, 85);
			this.button37.Name = "button37";
			this.button37.Size = new Size(155, 23);
			this.button37.TabIndex = 2;
			this.button37.Text = "37 TCP 读取运行信息";
			this.button37.UseVisualStyleBackColor = true;
			this.button37.Click += new EventHandler(this.button37_Click);
			this.label112.AutoSize = true;
			this.label112.Location = new Point(14, 32);
			this.label112.Name = "label112";
			this.label112.Size = new Size(83, 12);
			this.label112.TabIndex = 1;
			this.label112.Text = "控制器IP:(21)";
			this.textBox21.Location = new Point(103, 29);
			this.textBox21.Name = "textBox21";
			this.textBox21.Size = new Size(166, 21);
			this.textBox21.TabIndex = 0;
			this.textBox21.Text = "192.168.168.2";
			this.tabPage16.Controls.Add(this.textBox23);
			this.tabPage16.Controls.Add(this.textBox22);
			this.tabPage16.Controls.Add(this.checkBox112);
			this.tabPage16.Location = new Point(4, 76);
			this.tabPage16.Name = "tabPage16";
			this.tabPage16.Padding = new Padding(3);
			this.tabPage16.Size = new Size(563, 628);
			this.tabPage16.TabIndex = 15;
			this.tabPage16.Text = "DataServer";
			this.tabPage16.UseVisualStyleBackColor = true;
			this.textBox23.Location = new Point(50, 76);
			this.textBox23.Name = "textBox23";
			this.textBox23.Size = new Size(53, 21);
			this.textBox23.TabIndex = 43;
			this.textBox23.Text = "61001";
			this.textBox22.Location = new Point(50, 49);
			this.textBox22.Name = "textBox22";
			this.textBox22.Size = new Size(122, 21);
			this.textBox22.TabIndex = 42;
			this.textBox22.Text = "192.168.168.153";
			this.checkBox112.AutoSize = true;
			this.checkBox112.BackColor = Color.Red;
			this.checkBox112.Location = new Point(16, 27);
			this.checkBox112.Name = "checkBox112";
			this.checkBox112.Size = new Size(246, 16);
			this.checkBox112.TabIndex = 41;
			this.checkBox112.Text = "112_选择要发送的目标IP(22 IP,23 PORT)";
			this.checkBox112.UseVisualStyleBackColor = false;
			this.tabPage17.Controls.Add(this.button48);
			this.tabPage17.Controls.Add(this.button47);
			this.tabPage17.Location = new Point(4, 76);
			this.tabPage17.Name = "tabPage17";
			this.tabPage17.Padding = new Padding(3);
			this.tabPage17.Size = new Size(563, 628);
			this.tabPage17.TabIndex = 16;
			this.tabPage17.Text = "tabPage17";
			this.tabPage17.UseVisualStyleBackColor = true;
			this.button48.Location = new Point(16, 87);
			this.button48.Name = "button48";
			this.button48.Size = new Size(156, 23);
			this.button48.TabIndex = 1;
			this.button48.Text = "48 UDP Server";
			this.button48.UseVisualStyleBackColor = true;
			this.button48.Click += new EventHandler(this.button48_Click);
			this.button47.Location = new Point(16, 27);
			this.button47.Name = "button47";
			this.button47.Size = new Size(156, 23);
			this.button47.TabIndex = 0;
			this.button47.Text = "47 Script 1000";
			this.button47.UseVisualStyleBackColor = true;
			this.button47.Click += new EventHandler(this.button47_Click);
			this.tabPage18.Controls.Add(this.button90);
			this.tabPage18.Controls.Add(this.label157);
			this.tabPage18.Controls.Add(this.label158);
			this.tabPage18.Controls.Add(this.textBox37);
			this.tabPage18.Controls.Add(this.textBox38);
			this.tabPage18.Controls.Add(this.button58);
			this.tabPage18.Controls.Add(this.richTextBox2);
			this.tabPage18.Controls.Add(this.richTextBox1);
			this.tabPage18.Controls.Add(this.button60);
			this.tabPage18.Controls.Add(this.button59);
			this.tabPage18.Controls.Add(this.numericUpDown17);
			this.tabPage18.Controls.Add(this.label118);
			this.tabPage18.Controls.Add(this.textBox26);
			this.tabPage18.Controls.Add(this.label117);
			this.tabPage18.Controls.Add(this.label116);
			this.tabPage18.Controls.Add(this.label125);
			this.tabPage18.Controls.Add(this.label115);
			this.tabPage18.Controls.Add(this.textBox25);
			this.tabPage18.Controls.Add(this.textBox24);
			this.tabPage18.Controls.Add(this.button50);
			this.tabPage18.Location = new Point(4, 76);
			this.tabPage18.Name = "tabPage18";
			this.tabPage18.Padding = new Padding(3);
			this.tabPage18.Size = new Size(563, 628);
			this.tabPage18.TabIndex = 17;
			this.tabPage18.Text = "CPU参数";
			this.tabPage18.UseVisualStyleBackColor = true;
			this.button90.Location = new Point(237, 489);
			this.button90.Name = "button90";
			this.button90.Size = new Size(120, 42);
			this.button90.TabIndex = 16;
			this.button90.Text = "90 修改特殊卡\r\n(只开门, 不发卡)";
			this.button90.UseVisualStyleBackColor = true;
			this.button90.Click += new EventHandler(this.button90_Click);
			this.label157.AutoSize = true;
			this.label157.Location = new Point(6, 530);
			this.label157.Name = "label157";
			this.label157.Size = new Size(65, 12);
			this.label157.TabIndex = 15;
			this.label157.Text = "38 特殊卡2";
			this.label158.AutoSize = true;
			this.label158.Location = new Point(6, 494);
			this.label158.Name = "label158";
			this.label158.Size = new Size(65, 12);
			this.label158.TabIndex = 14;
			this.label158.Text = "37 特殊卡1";
			this.textBox37.Location = new Point(89, 491);
			this.textBox37.Name = "textBox37";
			this.textBox37.Size = new Size(100, 21);
			this.textBox37.TabIndex = 13;
			this.textBox38.Location = new Point(89, 527);
			this.textBox38.Name = "textBox38";
			this.textBox38.Size = new Size(100, 21);
			this.textBox38.TabIndex = 12;
			this.button58.Location = new Point(371, 142);
			this.button58.Name = "button58";
			this.button58.Size = new Size(113, 42);
			this.button58.TabIndex = 11;
			this.button58.Text = "58 自定义数据修改为98";
			this.button58.UseVisualStyleBackColor = true;
			this.button58.Click += new EventHandler(this.button58_Click);
			this.richTextBox2.Location = new Point(19, 259);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new Size(162, 194);
			this.richTextBox2.TabIndex = 10;
			this.richTextBox2.Text = "";
			this.richTextBox2.ZoomFactor = 2f;
			this.richTextBox1.Location = new Point(215, 259);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new Size(162, 194);
			this.richTextBox1.TabIndex = 10;
			this.richTextBox1.Text = "";
			this.button60.Location = new Point(409, 209);
			this.button60.Name = "button60";
			this.button60.Size = new Size(75, 23);
			this.button60.TabIndex = 9;
			this.button60.Text = "60 取值";
			this.button60.UseVisualStyleBackColor = true;
			this.button60.Click += new EventHandler(this.button60_Click);
			this.button59.Location = new Point(302, 209);
			this.button59.Name = "button59";
			this.button59.Size = new Size(75, 23);
			this.button59.TabIndex = 9;
			this.button59.Text = "59 设置";
			this.button59.UseVisualStyleBackColor = true;
			this.button59.Click += new EventHandler(this.button59_Click);
			NumericUpDown arg_17B85_0 = this.numericUpDown17;
			array = new int[4];
			array[0] = 1000;
			arg_17B85_0.Increment = new decimal(array);
			this.numericUpDown17.Location = new Point(161, 209);
			NumericUpDown arg_17BC2_0 = this.numericUpDown17;
			array = new int[4];
			array[0] = 100000000;
			arg_17BC2_0.Maximum = new decimal(array);
			this.numericUpDown17.Name = "numericUpDown17";
			this.numericUpDown17.Size = new Size(120, 21);
			this.numericUpDown17.TabIndex = 8;
			this.label118.AutoSize = true;
			this.label118.Location = new Point(48, 161);
			this.label118.Name = "label118";
			this.label118.Size = new Size(77, 12);
			this.label118.TabIndex = 7;
			this.label118.Text = "1024 = 0x400";
			this.textBox26.Location = new Point(89, 114);
			this.textBox26.Name = "textBox26";
			this.textBox26.PasswordChar = 'x';
			this.textBox26.Size = new Size(100, 21);
			this.textBox26.TabIndex = 6;
			this.label117.AutoSize = true;
			this.label117.Location = new Point(39, 117);
			this.label117.Name = "label117";
			this.label117.Size = new Size(53, 12);
			this.label117.TabIndex = 5;
			this.label117.Text = "26 指令:";
			this.label116.AutoSize = true;
			this.label116.Location = new Point(6, 80);
			this.label116.Name = "label116";
			this.label116.Size = new Size(41, 12);
			this.label116.TabIndex = 2;
			this.label116.Text = "25 len";
			this.label125.AutoSize = true;
			this.label125.Location = new Point(17, 212);
			this.label125.Name = "label125";
			this.label125.Size = new Size(125, 12);
			this.label125.TabIndex = 2;
			this.label125.Text = "17 通信超时(单位:ms)";
			this.label115.AutoSize = true;
			this.label115.Location = new Point(6, 44);
			this.label115.Name = "label115";
			this.label115.Size = new Size(77, 12);
			this.label115.TabIndex = 2;
			this.label115.Text = "24 StartAddr";
			this.textBox25.Location = new Point(89, 77);
			this.textBox25.Name = "textBox25";
			this.textBox25.Size = new Size(100, 21);
			this.textBox25.TabIndex = 1;
			this.textBox25.Text = "1024";
			this.textBox24.Location = new Point(89, 41);
			this.textBox24.Name = "textBox24";
			this.textBox24.Size = new Size(100, 21);
			this.textBox24.TabIndex = 1;
			this.textBox24.Text = "196608";
			this.button50.Location = new Point(215, 40);
			this.button50.Name = "button50";
			this.button50.Size = new Size(75, 23);
			this.button50.TabIndex = 0;
			this.button50.Text = "50 读取Data";
			this.button50.UseVisualStyleBackColor = true;
			this.button50.Click += new EventHandler(this.button50_Click);
			this.tabPage19.Controls.Add(this.label137);
			this.tabPage19.Controls.Add(this.cboDoors);
			this.tabPage19.Controls.Add(this.label138);
			this.tabPage19.Controls.Add(this.button64);
			this.tabPage19.Controls.Add(this.button65);
			this.tabPage19.Controls.Add(this.button66);
			this.tabPage19.Controls.Add(this.button67);
			this.tabPage19.Controls.Add(this.listBox4);
			this.tabPage19.Controls.Add(this.grpWeekdayControl);
			this.tabPage19.Controls.Add(this.grpEnd);
			this.tabPage19.Controls.Add(this.grpBegin);
			this.tabPage19.Location = new Point(4, 76);
			this.tabPage19.Name = "tabPage19";
			this.tabPage19.Padding = new Padding(3);
			this.tabPage19.Size = new Size(563, 628);
			this.tabPage19.TabIndex = 18;
			this.tabPage19.Text = "19 首卡";
			this.tabPage19.UseVisualStyleBackColor = true;
			this.label137.AutoSize = true;
			this.label137.BackColor = Color.Transparent;
			this.label137.ForeColor = Color.Black;
			this.label137.ImeMode = ImeMode.NoControl;
			this.label137.Location = new Point(39, 16);
			this.label137.Name = "label137";
			this.label137.Size = new Size(59, 12);
			this.label137.TabIndex = 61;
			this.label137.Text = "适用于门:";
			this.cboDoors.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboDoors.ForeColor = Color.Black;
			this.cboDoors.FormattingEnabled = true;
			this.cboDoors.Items.AddRange(new object[]
			{
				"1号门",
				"2号门",
				"3号门",
				"4号门"
			});
			this.cboDoors.Location = new Point(140, 13);
			this.cboDoors.Name = "cboDoors";
			this.cboDoors.Size = new Size(144, 20);
			this.cboDoors.TabIndex = 60;
			this.label138.AutoSize = true;
			this.label138.ImeMode = ImeMode.NoControl;
			this.label138.Location = new Point(311, 62);
			this.label138.Name = "label138";
			this.label138.Size = new Size(221, 48);
			this.label138.TabIndex = 47;
			this.label138.Text = "注意: 是利用定时任务来实现的. \r\n如果有定时任务则会一同起作用\r\n一个门的首卡设置占用3个定时任务项\r\n如果要使用首卡, 必须启用定时任务功能";
			this.button64.Location = new Point(426, 419);
			this.button64.Name = "button64";
			this.button64.Size = new Size(107, 23);
			this.button64.TabIndex = 59;
			this.button64.Text = "64_取定时任务页";
			this.button64.UseVisualStyleBackColor = true;
			this.button64.Click += new EventHandler(this.button12_Click);
			this.button65.Location = new Point(245, 419);
			this.button65.Name = "button65";
			this.button65.Size = new Size(151, 23);
			this.button65.TabIndex = 58;
			this.button65.Text = "65_上传首卡[IP]";
			this.button65.UseVisualStyleBackColor = true;
			this.button65.Click += new EventHandler(this.button65_Click);
			this.button66.Location = new Point(131, 419);
			this.button66.Name = "button66";
			this.button66.Size = new Size(75, 23);
			this.button66.TabIndex = 57;
			this.button66.Text = "66_清空";
			this.button66.UseVisualStyleBackColor = true;
			this.button66.Click += new EventHandler(this.button66_Click);
			this.button67.Location = new Point(26, 419);
			this.button67.Name = "button67";
			this.button67.Size = new Size(75, 23);
			this.button67.TabIndex = 56;
			this.button67.Text = "67_增加";
			this.button67.UseVisualStyleBackColor = true;
			this.button67.Click += new EventHandler(this.button67_Click);
			this.listBox4.FormattingEnabled = true;
			this.listBox4.ItemHeight = 12;
			this.listBox4.Location = new Point(20, 459);
			this.listBox4.Name = "listBox4";
			this.listBox4.Size = new Size(528, 184);
			this.listBox4.TabIndex = 55;
			this.grpWeekdayControl.BackColor = Color.Transparent;
			this.grpWeekdayControl.Controls.Add(this.chkMonday);
			this.grpWeekdayControl.Controls.Add(this.chkSunday);
			this.grpWeekdayControl.Controls.Add(this.chkTuesday);
			this.grpWeekdayControl.Controls.Add(this.chkSaturday);
			this.grpWeekdayControl.Controls.Add(this.chkWednesday);
			this.grpWeekdayControl.Controls.Add(this.chkFriday);
			this.grpWeekdayControl.Controls.Add(this.chkThursday);
			this.grpWeekdayControl.ForeColor = Color.Black;
			this.grpWeekdayControl.Location = new Point(307, 212);
			this.grpWeekdayControl.Name = "grpWeekdayControl";
			this.grpWeekdayControl.Size = new Size(149, 186);
			this.grpWeekdayControl.TabIndex = 54;
			this.grpWeekdayControl.TabStop = false;
			this.grpWeekdayControl.Text = "星期控制";
			this.chkMonday.AutoSize = true;
			this.chkMonday.Checked = true;
			this.chkMonday.CheckState = CheckState.Checked;
			this.chkMonday.ImeMode = ImeMode.NoControl;
			this.chkMonday.Location = new Point(6, 24);
			this.chkMonday.Name = "chkMonday";
			this.chkMonday.Size = new Size(60, 16);
			this.chkMonday.TabIndex = 19;
			this.chkMonday.Text = "星期一";
			this.chkMonday.UseVisualStyleBackColor = true;
			this.chkSunday.AutoSize = true;
			this.chkSunday.Checked = true;
			this.chkSunday.CheckState = CheckState.Checked;
			this.chkSunday.ImeMode = ImeMode.NoControl;
			this.chkSunday.Location = new Point(6, 156);
			this.chkSunday.Name = "chkSunday";
			this.chkSunday.Size = new Size(60, 16);
			this.chkSunday.TabIndex = 25;
			this.chkSunday.Text = "星期日";
			this.chkSunday.UseVisualStyleBackColor = true;
			this.chkTuesday.AutoSize = true;
			this.chkTuesday.Checked = true;
			this.chkTuesday.CheckState = CheckState.Checked;
			this.chkTuesday.ImeMode = ImeMode.NoControl;
			this.chkTuesday.Location = new Point(6, 46);
			this.chkTuesday.Name = "chkTuesday";
			this.chkTuesday.Size = new Size(60, 16);
			this.chkTuesday.TabIndex = 20;
			this.chkTuesday.Text = "星期二";
			this.chkTuesday.UseVisualStyleBackColor = true;
			this.chkSaturday.AutoSize = true;
			this.chkSaturday.Checked = true;
			this.chkSaturday.CheckState = CheckState.Checked;
			this.chkSaturday.ImeMode = ImeMode.NoControl;
			this.chkSaturday.Location = new Point(6, 134);
			this.chkSaturday.Name = "chkSaturday";
			this.chkSaturday.Size = new Size(60, 16);
			this.chkSaturday.TabIndex = 24;
			this.chkSaturday.Text = "星期六";
			this.chkSaturday.UseVisualStyleBackColor = true;
			this.chkWednesday.AutoSize = true;
			this.chkWednesday.Checked = true;
			this.chkWednesday.CheckState = CheckState.Checked;
			this.chkWednesday.ImeMode = ImeMode.NoControl;
			this.chkWednesday.Location = new Point(6, 68);
			this.chkWednesday.Name = "chkWednesday";
			this.chkWednesday.Size = new Size(60, 16);
			this.chkWednesday.TabIndex = 21;
			this.chkWednesday.Text = "星期三";
			this.chkWednesday.UseVisualStyleBackColor = true;
			this.chkFriday.AutoSize = true;
			this.chkFriday.Checked = true;
			this.chkFriday.CheckState = CheckState.Checked;
			this.chkFriday.ImeMode = ImeMode.NoControl;
			this.chkFriday.Location = new Point(6, 112);
			this.chkFriday.Name = "chkFriday";
			this.chkFriday.Size = new Size(60, 16);
			this.chkFriday.TabIndex = 23;
			this.chkFriday.Text = "星期五";
			this.chkFriday.UseVisualStyleBackColor = true;
			this.chkThursday.AutoSize = true;
			this.chkThursday.Checked = true;
			this.chkThursday.CheckState = CheckState.Checked;
			this.chkThursday.ImeMode = ImeMode.NoControl;
			this.chkThursday.Location = new Point(6, 90);
			this.chkThursday.Name = "chkThursday";
			this.chkThursday.Size = new Size(60, 16);
			this.chkThursday.TabIndex = 22;
			this.chkThursday.Text = "星期四";
			this.chkThursday.UseVisualStyleBackColor = true;
			this.grpEnd.BackColor = Color.Transparent;
			this.grpEnd.Controls.Add(this.cboEndControlStatus);
			this.grpEnd.Controls.Add(this.label128);
			this.grpEnd.Controls.Add(this.label132);
			this.grpEnd.Controls.Add(this.dateEndHMS1);
			this.grpEnd.Controls.Add(this.label133);
			this.grpEnd.ForeColor = Color.Black;
			this.grpEnd.Location = new Point(55, 228);
			this.grpEnd.Name = "grpEnd";
			this.grpEnd.Size = new Size(227, 169);
			this.grpEnd.TabIndex = 53;
			this.grpEnd.TabStop = false;
			this.cboEndControlStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				"Door Open",
				"Door Closed",
				"Door Controlled"
			});
			this.cboEndControlStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboEndControlStatus.FormattingEnabled = true;
			this.cboEndControlStatus.Items.AddRange(new object[]
			{
				"0. 在线",
				"1. 常开",
				"2. 常闭",
				"3. 只许首卡开门"
			});
			this.cboEndControlStatus.Location = new Point(99, 51);
			this.cboEndControlStatus.Name = "cboEndControlStatus";
			this.cboEndControlStatus.Size = new Size(121, 20);
			this.cboEndControlStatus.TabIndex = 49;
			this.label128.AutoSize = true;
			this.label128.ImeMode = ImeMode.NoControl;
			this.label128.Location = new Point(16, 54);
			this.label128.Name = "label128";
			this.label128.Size = new Size(59, 12);
			this.label128.TabIndex = 48;
			this.label128.Text = "控制方式2";
			this.label132.AutoSize = true;
			this.label132.ImeMode = ImeMode.NoControl;
			this.label132.Location = new Point(16, 107);
			this.label132.Name = "label132";
			this.label132.Size = new Size(209, 36);
			this.label132.TabIndex = 47;
			this.label132.Text = "在停止时间后,\r\n门回到控制方式2, \r\n并且用户刷首卡不会切换到控制方式1 ";
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Location = new Point(99, 20);
			this.dateEndHMS1.Name = "dateEndHMS1";
			this.dateEndHMS1.ShowUpDown = true;
			this.dateEndHMS1.Size = new Size(85, 21);
			this.dateEndHMS1.TabIndex = 46;
			this.dateEndHMS1.Value = new DateTime(2010, 1, 1, 8, 0, 0, 0);
			this.label133.ImeMode = ImeMode.NoControl;
			this.label133.Location = new Point(16, 19);
			this.label133.Name = "label133";
			this.label133.Size = new Size(77, 27);
			this.label133.TabIndex = 45;
			this.label133.Text = "停止时间:";
			this.label133.TextAlign = ContentAlignment.MiddleLeft;
			this.grpBegin.BackColor = Color.Transparent;
			this.grpBegin.Controls.Add(this.cboBeginControlStatus);
			this.grpBegin.Controls.Add(this.label134);
			this.grpBegin.Controls.Add(this.label135);
			this.grpBegin.Controls.Add(this.dateBeginHMS1);
			this.grpBegin.Controls.Add(this.label136);
			this.grpBegin.ForeColor = Color.Black;
			this.grpBegin.Location = new Point(55, 52);
			this.grpBegin.Name = "grpBegin";
			this.grpBegin.Size = new Size(227, 169);
			this.grpBegin.TabIndex = 52;
			this.grpBegin.TabStop = false;
			this.cboBeginControlStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				"Door Open",
				"Door Closed",
				"Door Controlled"
			});
			this.cboBeginControlStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboBeginControlStatus.FormattingEnabled = true;
			this.cboBeginControlStatus.Items.AddRange(new object[]
			{
				"0. 在线",
				"1. 常开",
				"2. 常闭"
			});
			this.cboBeginControlStatus.Location = new Point(99, 51);
			this.cboBeginControlStatus.Name = "cboBeginControlStatus";
			this.cboBeginControlStatus.Size = new Size(121, 20);
			this.cboBeginControlStatus.TabIndex = 49;
			this.label134.AutoSize = true;
			this.label134.ImeMode = ImeMode.NoControl;
			this.label134.Location = new Point(16, 54);
			this.label134.Name = "label134";
			this.label134.Size = new Size(59, 12);
			this.label134.TabIndex = 48;
			this.label134.Text = "控制方式1";
			this.label135.AutoSize = true;
			this.label135.ImeMode = ImeMode.NoControl;
			this.label135.Location = new Point(16, 107);
			this.label135.Name = "label135";
			this.label135.Size = new Size(161, 36);
			this.label135.TabIndex = 47;
			this.label135.Text = "在开始时间后, \r\n用户刷首卡后,\r\n此门会自动切换到控制方式1 ";
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Location = new Point(99, 20);
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Size = new Size(85, 21);
			this.dateBeginHMS1.TabIndex = 46;
			this.dateBeginHMS1.Value = new DateTime(2010, 1, 1, 8, 0, 0, 0);
			this.label136.ImeMode = ImeMode.NoControl;
			this.label136.Location = new Point(16, 19);
			this.label136.Name = "label136";
			this.label136.Size = new Size(77, 27);
			this.label136.TabIndex = 45;
			this.label136.Text = "开始时间:";
			this.label136.TextAlign = ContentAlignment.MiddleLeft;
			this.tabPage20.Controls.Add(this.label143);
			this.tabPage20.Controls.Add(this.numericUpDown22);
			this.tabPage20.Controls.Add(this.button72);
			this.tabPage20.Controls.Add(this.checkBox131);
			this.tabPage20.Controls.Add(this.checkBox132);
			this.tabPage20.Controls.Add(this.numericUpDown20);
			this.tabPage20.Controls.Add(this.checkBox130);
			this.tabPage20.Controls.Add(this.label141);
			this.tabPage20.Controls.Add(this.numericUpDown21);
			this.tabPage20.Controls.Add(this.button70);
			this.tabPage20.Controls.Add(this.label142);
			this.tabPage20.Controls.Add(this.label139);
			this.tabPage20.Controls.Add(this.textBox31);
			this.tabPage20.Controls.Add(this.button75);
			this.tabPage20.Controls.Add(this.button69);
			this.tabPage20.Location = new Point(4, 76);
			this.tabPage20.Name = "tabPage20";
			this.tabPage20.Padding = new Padding(3);
			this.tabPage20.Size = new Size(563, 628);
			this.tabPage20.TabIndex = 19;
			this.tabPage20.Text = "20 电梯";
			this.tabPage20.UseVisualStyleBackColor = true;
			this.label143.AutoSize = true;
			this.label143.Location = new Point(201, 155);
			this.label143.Name = "label143";
			this.label143.Size = new Size(83, 12);
			this.label143.TabIndex = 94;
			this.label143.Text = "22_间隔(毫秒)";
			this.numericUpDown22.Location = new Point(286, 150);
			NumericUpDown arg_1933D_0 = this.numericUpDown22;
			array = new int[4];
			array[0] = 20000;
			arg_1933D_0.Maximum = new decimal(array);
			this.numericUpDown22.Name = "numericUpDown22";
			this.numericUpDown22.Size = new Size(53, 21);
			this.numericUpDown22.TabIndex = 93;
			NumericUpDown arg_19391_0 = this.numericUpDown22;
			array = new int[4];
			array[0] = 500;
			arg_19391_0.Value = new decimal(array);
			this.button72.Location = new Point(45, 216);
			this.button72.Name = "button72";
			this.button72.Size = new Size(192, 23);
			this.button72.TabIndex = 92;
			this.button72.Text = "72 远程到21-40楼层[IP]";
			this.button72.UseVisualStyleBackColor = true;
			this.button72.Click += new EventHandler(this.button70_Click);
			this.checkBox131.AutoSize = true;
			this.checkBox131.Checked = true;
			this.checkBox131.CheckState = CheckState.Checked;
			this.checkBox131.Location = new Point(129, 155);
			this.checkBox131.Name = "checkBox131";
			this.checkBox131.Size = new Size(60, 16);
			this.checkBox131.TabIndex = 91;
			this.checkBox131.Text = "131_NC";
			this.checkBox131.UseVisualStyleBackColor = true;
			this.checkBox132.AutoSize = true;
			this.checkBox132.BackColor = Color.Red;
			this.checkBox132.Location = new Point(15, 14);
			this.checkBox132.Name = "checkBox132";
			this.checkBox132.Size = new Size(156, 16);
			this.checkBox132.TabIndex = 68;
			this.checkBox132.Text = "132_选择要修改电梯延时";
			this.checkBox132.UseVisualStyleBackColor = false;
			this.numericUpDown20.Location = new Point(458, 11);
			NumericUpDown arg_19551_0 = this.numericUpDown20;
			array = new int[4];
			array[0] = 25;
			arg_19551_0.Maximum = new decimal(array);
			this.numericUpDown20.Name = "numericUpDown20";
			this.numericUpDown20.Size = new Size(53, 21);
			this.numericUpDown20.TabIndex = 67;
			NumericUpDown arg_195A1_0 = this.numericUpDown20;
			array = new int[4];
			array[0] = 5;
			arg_195A1_0.Value = new decimal(array);
			this.checkBox130.AutoSize = true;
			this.checkBox130.Checked = true;
			this.checkBox130.CheckState = CheckState.Checked;
			this.checkBox130.Location = new Point(50, 155);
			this.checkBox130.Name = "checkBox130";
			this.checkBox130.Size = new Size(60, 16);
			this.checkBox130.TabIndex = 90;
			this.checkBox130.Text = "130_NO";
			this.checkBox130.UseVisualStyleBackColor = true;
			this.label141.AutoSize = true;
			this.label141.Location = new Point(348, 15);
			this.label141.Name = "label141";
			this.label141.Size = new Size(95, 12);
			this.label141.TabIndex = 66;
			this.label141.Text = "20_多层延时(秒)";
			this.numericUpDown21.DecimalPlaces = 1;
			this.numericUpDown21.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				65536
			});
			this.numericUpDown21.Location = new Point(278, 11);
			this.numericUpDown21.Maximum = new decimal(new int[]
			{
				255,
				0,
				0,
				65536
			});
			this.numericUpDown21.Name = "numericUpDown21";
			this.numericUpDown21.Size = new Size(53, 21);
			this.numericUpDown21.TabIndex = 65;
			this.numericUpDown21.Value = new decimal(new int[]
			{
				4,
				0,
				0,
				65536
			});
			this.button70.Location = new Point(45, 178);
			this.button70.Name = "button70";
			this.button70.Size = new Size(192, 23);
			this.button70.TabIndex = 12;
			this.button70.Text = "70 远程到1-20楼层[IP]";
			this.button70.UseVisualStyleBackColor = true;
			this.button70.Click += new EventHandler(this.button70_Click);
			this.label142.AutoSize = true;
			this.label142.Location = new Point(177, 15);
			this.label142.Name = "label142";
			this.label142.Size = new Size(95, 12);
			this.label142.TabIndex = 64;
			this.label142.Text = "21_单层延时(秒)";
			this.label139.AutoSize = true;
			this.label139.Location = new Point(13, 65);
			this.label139.Name = "label139";
			this.label139.Size = new Size(89, 12);
			this.label139.TabIndex = 10;
			this.label139.Text = "指定楼层号(31)";
			this.textBox31.Location = new Point(115, 56);
			this.textBox31.Name = "textBox31";
			this.textBox31.Size = new Size(89, 21);
			this.textBox31.TabIndex = 11;
			this.textBox31.Text = "0";
			this.button75.Location = new Point(203, 94);
			this.button75.Name = "button75";
			this.button75.Size = new Size(138, 23);
			this.button75.TabIndex = 9;
			this.button75.Text = "75远程绿灯灭[IP]";
			this.button75.UseVisualStyleBackColor = true;
			this.button75.Click += new EventHandler(this.button69_Click);
			this.button69.Location = new Point(45, 94);
			this.button69.Name = "button69";
			this.button69.Size = new Size(138, 23);
			this.button69.TabIndex = 9;
			this.button69.Text = "69远程到楼层[IP]";
			this.button69.UseVisualStyleBackColor = true;
			this.button69.Click += new EventHandler(this.button69_Click);
			this.tabPage21.Controls.Add(this.comboBox60);
			this.tabPage21.Controls.Add(this.button91);
			this.tabPage21.Controls.Add(this.button88);
			this.tabPage21.Controls.Add(this.button79);
			this.tabPage21.Controls.Add(this.button78);
			this.tabPage21.Controls.Add(this.dataGridView3);
			this.tabPage21.Controls.Add(this.dataGridView2);
			this.tabPage21.Controls.Add(this.button76);
			this.tabPage21.Controls.Add(this.groupBox13);
			this.tabPage21.Controls.Add(this.button74);
			this.tabPage21.Location = new Point(4, 76);
			this.tabPage21.Name = "tabPage21";
			this.tabPage21.Padding = new Padding(3);
			this.tabPage21.Size = new Size(563, 628);
			this.tabPage21.TabIndex = 20;
			this.tabPage21.Text = "21 WEB";
			this.tabPage21.UseVisualStyleBackColor = true;
			this.comboBox60.FormattingEnabled = true;
			this.comboBox60.Items.AddRange(new object[]
			{
				"1",
				"2",
				"4",
				"8"
			});
			this.comboBox60.Location = new Point(23, 255);
			this.comboBox60.Name = "comboBox60";
			this.comboBox60.Size = new Size(99, 20);
			this.comboBox60.TabIndex = 91;
			this.comboBox60.Text = "4";
			this.comboBox60.SelectedIndexChanged += new EventHandler(this.comboBox60_SelectedIndexChanged);
			this.button91.Location = new Point(19, 285);
			this.button91.Name = "button91";
			this.button91.Size = new Size(112, 58);
			this.button91.TabIndex = 89;
			this.button91.Text = "91 打开bin文件, 转换为c文件";
			this.button91.UseVisualStyleBackColor = true;
			this.button91.Click += new EventHandler(this.button91_Click);
			this.button88.Location = new Point(447, 37);
			this.button88.Name = "button88";
			this.button88.Size = new Size(110, 23);
			this.button88.TabIndex = 88;
			this.button88.Text = "88 自动设IP";
			this.button88.UseVisualStyleBackColor = true;
			this.button88.Click += new EventHandler(this.button88_Click);
			this.button79.Location = new Point(197, 369);
			this.button79.Name = "button79";
			this.button79.Size = new Size(146, 23);
			this.button79.TabIndex = 87;
			this.button79.Text = "79 导出卡号和人名Excel";
			this.button79.UseVisualStyleBackColor = true;
			this.button79.Click += new EventHandler(this.button79_Click);
			this.button78.Location = new Point(19, 369);
			this.button78.Name = "button78";
			this.button78.Size = new Size(146, 23);
			this.button78.TabIndex = 86;
			this.button78.Text = "78 提取卡号和人名[IP]";
			this.button78.UseVisualStyleBackColor = true;
			this.button78.Click += new EventHandler(this.button78_Click);
			this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Location = new Point(23, 398);
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.RowTemplate.Height = 23;
			this.dataGridView3.Size = new Size(534, 189);
			this.dataGridView3.TabIndex = 85;
			this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Location = new Point(158, 158);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 23;
			this.dataGridView2.Size = new Size(375, 150);
			this.dataGridView2.TabIndex = 84;
			this.button76.Location = new Point(19, 158);
			this.button76.Name = "button76";
			this.button76.Size = new Size(103, 54);
			this.button76.TabIndex = 83;
			this.button76.Text = "76打开EXCEL并生成bin";
			this.button76.UseVisualStyleBackColor = true;
			this.button76.Click += new EventHandler(this.button76_Click);
			this.groupBox13.Controls.Add(this.label144);
			this.groupBox13.Controls.Add(this.comboBox59);
			this.groupBox13.Controls.Add(this.button73);
			this.groupBox13.Controls.Add(this.numericUpDown23);
			this.groupBox13.Controls.Add(this.checkBox133);
			this.groupBox13.Location = new Point(158, 20);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.Size = new Size(274, 115);
			this.groupBox13.TabIndex = 24;
			this.groupBox13.TabStop = false;
			this.label144.AutoSize = true;
			this.label144.Location = new Point(56, 56);
			this.label144.Name = "label144";
			this.label144.Size = new Size(71, 12);
			this.label144.TabIndex = 26;
			this.label144.Text = "语言版本_59";
			this.comboBox59.FormattingEnabled = true;
			this.comboBox59.Items.AddRange(new object[]
			{
				"中文",
				"English",
				"第三方"
			});
			this.comboBox59.Location = new Point(151, 53);
			this.comboBox59.Name = "comboBox59";
			this.comboBox59.Size = new Size(89, 20);
			this.comboBox59.TabIndex = 25;
			this.comboBox59.Text = "中文";
			this.button73.Location = new Point(22, 86);
			this.button73.Name = "button73";
			this.button73.Size = new Size(126, 23);
			this.button73.TabIndex = 24;
			this.button73.Text = "73设置WEB功能口";
			this.button73.UseVisualStyleBackColor = true;
			this.button73.Click += new EventHandler(this.button73_Click);
			this.numericUpDown23.Location = new Point(151, 20);
			NumericUpDown arg_1A1AB_0 = this.numericUpDown23;
			array = new int[4];
			array[0] = 65535;
			arg_1A1AB_0.Maximum = new decimal(array);
			this.numericUpDown23.Name = "numericUpDown23";
			this.numericUpDown23.Size = new Size(89, 21);
			this.numericUpDown23.TabIndex = 23;
			NumericUpDown arg_1A1FC_0 = this.numericUpDown23;
			array = new int[4];
			array[0] = 80;
			arg_1A1FC_0.Value = new decimal(array);
			this.checkBox133.AutoSize = true;
			this.checkBox133.Checked = true;
			this.checkBox133.CheckState = CheckState.Checked;
			this.checkBox133.Location = new Point(22, 21);
			this.checkBox133.Name = "checkBox133";
			this.checkBox133.Size = new Size(108, 16);
			this.checkBox133.TabIndex = 22;
			this.checkBox133.Text = "133_启用WEB_23";
			this.checkBox133.UseVisualStyleBackColor = true;
			this.button74.Location = new Point(19, 30);
			this.button74.Name = "button74";
			this.button74.Size = new Size(103, 23);
			this.button74.TabIndex = 82;
			this.button74.Text = "74_人名下载[IP]";
			this.button74.UseVisualStyleBackColor = true;
			this.button74.Click += new EventHandler(this.button74_Click);
			this.tabPage22.Controls.Add(this.button77);
			this.tabPage22.Controls.Add(this.numericUpDown24);
			this.tabPage22.Controls.Add(this.label145);
			this.tabPage22.Location = new Point(4, 76);
			this.tabPage22.Name = "tabPage22";
			this.tabPage22.Padding = new Padding(3);
			this.tabPage22.Size = new Size(563, 628);
			this.tabPage22.TabIndex = 21;
			this.tabPage22.Text = "22提取单个记录";
			this.tabPage22.UseVisualStyleBackColor = true;
			this.button77.Location = new Point(31, 55);
			this.button77.Name = "button77";
			this.button77.Size = new Size(102, 23);
			this.button77.TabIndex = 21;
			this.button77.Text = "77_读取记录";
			this.button77.UseVisualStyleBackColor = true;
			this.button77.Click += new EventHandler(this.button77_Click);
			this.numericUpDown24.Location = new Point(116, 22);
			NumericUpDown arg_1A461_0 = this.numericUpDown24;
			array = new int[4];
			array[0] = 210000;
			arg_1A461_0.Maximum = new decimal(array);
			NumericUpDown arg_1A480_0 = this.numericUpDown24;
			array = new int[4];
			array[0] = 1;
			arg_1A480_0.Minimum = new decimal(array);
			this.numericUpDown24.Name = "numericUpDown24";
			this.numericUpDown24.Size = new Size(100, 21);
			this.numericUpDown24.TabIndex = 20;
			NumericUpDown arg_1A4D0_0 = this.numericUpDown24;
			array = new int[4];
			array[0] = 1;
			arg_1A4D0_0.Value = new decimal(array);
			this.label145.AutoSize = true;
			this.label145.Location = new Point(29, 25);
			this.label145.Name = "label145";
			this.label145.Size = new Size(83, 12);
			this.label145.TabIndex = 19;
			this.label145.Text = "24 记录索引位";
			this.tabPage23.Controls.Add(this.button87);
			this.tabPage23.Controls.Add(this.button86);
			this.tabPage23.Controls.Add(this.label151);
			this.tabPage23.Controls.Add(this.textBox36);
			this.tabPage23.Controls.Add(this.textBox35);
			this.tabPage23.Controls.Add(this.button82);
			this.tabPage23.Controls.Add(this.textBox34);
			this.tabPage23.Controls.Add(this.button80);
			this.tabPage23.Location = new Point(4, 76);
			this.tabPage23.Name = "tabPage23";
			this.tabPage23.Padding = new Padding(3);
			this.tabPage23.Size = new Size(563, 628);
			this.tabPage23.TabIndex = 22;
			this.tabPage23.Text = "23 其他工具";
			this.tabPage23.UseVisualStyleBackColor = true;
			this.button87.Location = new Point(38, 178);
			this.button87.Name = "button87";
			this.button87.Size = new Size(75, 23);
			this.button87.TabIndex = 6;
			this.button87.Text = "87 解压";
			this.button87.UseVisualStyleBackColor = true;
			this.button87.Click += new EventHandler(this.button87_Click);
			this.button86.Location = new Point(38, 130);
			this.button86.Name = "button86";
			this.button86.Size = new Size(75, 23);
			this.button86.TabIndex = 5;
			this.button86.Text = "86 压缩";
			this.button86.UseVisualStyleBackColor = true;
			this.button86.Click += new EventHandler(this.button86_Click);
			this.label151.AutoSize = true;
			this.label151.Location = new Point(254, 55);
			this.label151.Name = "label151";
			this.label151.Size = new Size(41, 12);
			this.label151.TabIndex = 4;
			this.label151.Text = "替换为";
			this.textBox36.Location = new Point(340, 50);
			this.textBox36.Name = "textBox36";
			this.textBox36.Size = new Size(138, 21);
			this.textBox36.TabIndex = 3;
			this.textBox35.Location = new Point(72, 49);
			this.textBox35.Name = "textBox35";
			this.textBox35.Size = new Size(138, 21);
			this.textBox35.TabIndex = 3;
			this.button82.Location = new Point(38, 76);
			this.button82.Name = "button82";
			this.button82.Size = new Size(126, 23);
			this.button82.TabIndex = 2;
			this.button82.Text = "替换文件中的标识";
			this.button82.UseVisualStyleBackColor = true;
			this.button82.Click += new EventHandler(this.button82_Click);
			this.textBox34.Location = new Point(183, 23);
			this.textBox34.Name = "textBox34";
			this.textBox34.Size = new Size(295, 21);
			this.textBox34.TabIndex = 1;
			this.button80.Location = new Point(38, 21);
			this.button80.Name = "button80";
			this.button80.Size = new Size(115, 23);
			this.button80.TabIndex = 0;
			this.button80.Text = "80 指定目录";
			this.button80.UseVisualStyleBackColor = true;
			this.button80.Click += new EventHandler(this.button80_Click);
			this.tabPage25.Controls.Add(this.button95);
			this.tabPage25.Controls.Add(this.button94);
			this.tabPage25.Controls.Add(this.button93);
			this.tabPage25.Controls.Add(this.button92);
			this.tabPage25.Location = new Point(4, 76);
			this.tabPage25.Name = "tabPage25";
			this.tabPage25.Padding = new Padding(3);
			this.tabPage25.Size = new Size(563, 628);
			this.tabPage25.TabIndex = 24;
			this.tabPage25.Text = "25 短报文测试";
			this.tabPage25.UseVisualStyleBackColor = true;
			this.button95.Location = new Point(18, 98);
			this.button95.Name = "button95";
			this.button95.Size = new Size(171, 23);
			this.button95.TabIndex = 3;
			this.button95.Text = "95 尾部直接加权限[不查]";
			this.button95.UseVisualStyleBackColor = true;
			this.button95.Click += new EventHandler(this.button95_Click);
			this.button94.Location = new Point(18, 164);
			this.button94.Name = "button94";
			this.button94.Size = new Size(171, 23);
			this.button94.TabIndex = 2;
			this.button94.Text = "94 远程开门";
			this.button94.UseVisualStyleBackColor = true;
			this.button94.Click += new EventHandler(this.button94_Click);
			this.button93.Location = new Point(18, 55);
			this.button93.Name = "button93";
			this.button93.Size = new Size(171, 23);
			this.button93.TabIndex = 1;
			this.button93.Text = "93 清空权限";
			this.button93.UseVisualStyleBackColor = true;
			this.button93.Click += new EventHandler(this.button93_Click);
			this.button92.Location = new Point(18, 21);
			this.button92.Name = "button92";
			this.button92.Size = new Size(171, 23);
			this.button92.TabIndex = 0;
			this.button92.Text = "92 测试添加权限";
			this.button92.UseVisualStyleBackColor = true;
			this.button92.Click += new EventHandler(this.button92_Click);
			this.txtSN.Location = new Point(47, 33);
			NumericUpDown arg_1AC69_0 = this.txtSN;
			array = new int[4];
			array[0] = 999999999;
			arg_1AC69_0.Maximum = new decimal(array);
			this.txtSN.Name = "txtSN";
			this.txtSN.Size = new Size(89, 21);
			this.txtSN.TabIndex = 1;
			NumericUpDown arg_1ACBC_0 = this.txtSN;
			array = new int[4];
			array[0] = 401000002;
			arg_1ACBC_0.Value = new decimal(array);
			this.txtSN.ValueChanged += new EventHandler(this.txtSN_ValueChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(18, 37);
			this.label1.Name = "label1";
			this.label1.Size = new Size(23, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "SN:";
			this.txtInfo.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.txtInfo.Location = new Point(734, 8);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ScrollBars = ScrollBars.Vertical;
			this.txtInfo.Size = new Size(323, 750);
			this.txtInfo.TabIndex = 1;
			this.button1.Location = new Point(9, 429);
			this.button1.Name = "button1";
			this.button1.Size = new Size(138, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "1修复缺省参数[IP]";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new Point(9, 209);
			this.button2.Name = "button2";
			this.button2.Size = new Size(138, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "2读取参数表[IP]";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button3.Location = new Point(9, 241);
			this.button3.Name = "button3";
			this.button3.Size = new Size(138, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "3修改参数[IP]";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button21.Location = new Point(9, 156);
			this.button21.Name = "button21";
			this.button21.Size = new Size(138, 23);
			this.button21.TabIndex = 5;
			this.button21.Text = "21_读取运行信息[IP]";
			this.button21.UseVisualStyleBackColor = true;
			this.button21.Click += new EventHandler(this.button21_Click);
			this.button24.Location = new Point(20, 734);
			this.button24.Name = "button24";
			this.button24.Size = new Size(75, 23);
			this.button24.TabIndex = 6;
			this.button24.Text = "button24";
			this.button24.UseVisualStyleBackColor = true;
			this.button24.Click += new EventHandler(this.button24_Click);
			this.button29.Location = new Point(12, 487);
			this.button29.Name = "button29";
			this.button29.Size = new Size(135, 23);
			this.button29.TabIndex = 5;
			this.button29.Text = "29搜索控制器";
			this.button29.UseVisualStyleBackColor = true;
			this.button29.Click += new EventHandler(this.button29_Click);
			this.button31.Location = new Point(12, 570);
			this.button31.Name = "button31";
			this.button31.Size = new Size(135, 23);
			this.button31.TabIndex = 8;
			this.button31.Text = "31 软件在线升级";
			this.button31.UseVisualStyleBackColor = true;
			this.button31.Click += new EventHandler(this.button31_Click);
			this.button32.Location = new Point(12, 609);
			this.button32.Name = "button32";
			this.button32.Size = new Size(98, 23);
			this.button32.TabIndex = 9;
			this.button32.Text = "32 直接升";
			this.button32.UseVisualStyleBackColor = true;
			this.button32.Click += new EventHandler(this.button32_Click);
			this.button34.Location = new Point(12, 524);
			this.button34.Name = "button34";
			this.button34.Size = new Size(143, 23);
			this.button34.TabIndex = 94;
			this.button34.Text = "34 生成用户的软件权限";
			this.button34.UseVisualStyleBackColor = true;
			this.button34.Click += new EventHandler(this.button34_Click);
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.button43.Location = new Point(1, 683);
			this.button43.Name = "button43";
			this.button43.Size = new Size(146, 23);
			this.button43.TabIndex = 95;
			this.button43.Text = "43 实时监控[显示时间]";
			this.button43.UseVisualStyleBackColor = true;
			this.button43.Click += new EventHandler(this.button43_Click);
			this.label114.AutoSize = true;
			this.label114.Location = new Point(42, 665);
			this.label114.Name = "label114";
			this.label114.Size = new Size(53, 12);
			this.label114.TabIndex = 9;
			this.label114.Text = "label114";
			this.button44.Location = new Point(9, 4);
			this.button44.Name = "button44";
			this.button44.Size = new Size(75, 23);
			this.button44.TabIndex = 9;
			this.button44.Text = "44 Stop";
			this.button44.UseVisualStyleBackColor = true;
			this.button44.Click += new EventHandler(this.button44_Click);
			this.button45.Location = new Point(9, 279);
			this.button45.Name = "button45";
			this.button45.Size = new Size(138, 23);
			this.button45.TabIndex = 96;
			this.button45.Text = "45 产品信息UDP";
			this.button45.UseVisualStyleBackColor = true;
			this.button45.Click += new EventHandler(this.button45_Click);
			this.button46.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.button46.Location = new Point(653, 4);
			this.button46.Name = "button46";
			this.button46.Size = new Size(75, 23);
			this.button46.TabIndex = 97;
			this.button46.Text = "46 清空";
			this.button46.UseVisualStyleBackColor = true;
			this.button46.Click += new EventHandler(this.button46_Click);
			this.button49.Location = new Point(12, 370);
			this.button49.Name = "button49";
			this.button49.Size = new Size(143, 23);
			this.button49.TabIndex = 25;
			this.button49.Text = "49 重启控制器[Reboot]";
			this.button49.UseVisualStyleBackColor = true;
			this.button49.Click += new EventHandler(this.button49_Click);
			this.button51.Location = new Point(12, 638);
			this.button51.Name = "button51";
			this.button51.Size = new Size(110, 23);
			this.button51.TabIndex = 98;
			this.button51.Text = "51 产品生产";
			this.button51.UseVisualStyleBackColor = true;
			this.button51.Click += new EventHandler(this.button51_Click);
			this.textBox29.Location = new Point(250, 4);
			this.textBox29.Name = "textBox29";
			this.textBox29.Size = new Size(100, 21);
			this.textBox29.TabIndex = 99;
			this.textBox29.TextChanged += new EventHandler(this.textBox29_TextChanged);
			this.label119.AutoSize = true;
			this.label119.Location = new Point(197, 9);
			this.label119.Name = "label119";
			this.label119.Size = new Size(47, 12);
			this.label119.TabIndex = 5;
			this.label119.Text = "29 HEX:";
			this.label120.AutoSize = true;
			this.label120.Location = new Point(356, 9);
			this.label120.Name = "label120";
			this.label120.Size = new Size(89, 12);
			this.label120.TabIndex = 5;
			this.label120.Text = "30 转为十进制:";
			this.textBox30.Location = new Point(451, 4);
			this.textBox30.Name = "textBox30";
			this.textBox30.Size = new Size(100, 21);
			this.textBox30.TabIndex = 99;
			this.button55.Location = new Point(90, 4);
			this.button55.Name = "button55";
			this.button55.Size = new Size(75, 23);
			this.button55.TabIndex = 18;
			this.button55.Text = "55 取SN";
			this.button55.UseVisualStyleBackColor = true;
			this.button55.Click += new EventHandler(this.button55_Click);
			this.checkBox120.AutoSize = true;
			this.checkBox120.Location = new Point(9, 60);
			this.checkBox120.Name = "checkBox120";
			this.checkBox120.Size = new Size(102, 16);
			this.checkBox120.TabIndex = 100;
			this.checkBox120.Text = "120 指定IP-31";
			this.checkBox120.UseVisualStyleBackColor = true;
			this.checkBox120.CheckedChanged += new EventHandler(this.checkBox120_CheckedChanged);
			this.grpbIP.Controls.Add(this.nudPort);
			this.grpbIP.Controls.Add(this.txtControllerIP);
			this.grpbIP.Controls.Add(this.label123);
			this.grpbIP.Controls.Add(this.label124);
			this.grpbIP.Location = new Point(-14, 75);
			this.grpbIP.Name = "grpbIP";
			this.grpbIP.Size = new Size(169, 70);
			this.grpbIP.TabIndex = 101;
			this.grpbIP.TabStop = false;
			this.grpbIP.Visible = false;
			this.nudPort.Location = new Point(45, 41);
			NumericUpDown arg_1B8D7_0 = this.nudPort;
			array = new int[4];
			array[0] = 65534;
			arg_1B8D7_0.Maximum = new decimal(array);
			NumericUpDown arg_1B8FA_0 = this.nudPort;
			array = new int[4];
			array[0] = 1024;
			arg_1B8FA_0.Minimum = new decimal(array);
			this.nudPort.Name = "nudPort";
			this.nudPort.Size = new Size(57, 21);
			this.nudPort.TabIndex = 58;
			NumericUpDown arg_1B94E_0 = this.nudPort;
			array = new int[4];
			array[0] = 60000;
			arg_1B94E_0.Value = new decimal(array);
			this.nudPort.ValueChanged += new EventHandler(this.nudPort_ValueChanged);
			this.txtControllerIP.Location = new Point(45, 14);
			this.txtControllerIP.Name = "txtControllerIP";
			this.txtControllerIP.Size = new Size(116, 21);
			this.txtControllerIP.TabIndex = 0;
			this.txtControllerIP.Text = "wg3721.3322.org";
			this.txtControllerIP.TextChanged += new EventHandler(this.txtControllerIP_TextChanged);
			this.label123.AutoSize = true;
			this.label123.ImeMode = ImeMode.NoControl;
			this.label123.Location = new Point(13, 45);
			this.label123.Name = "label123";
			this.label123.Size = new Size(35, 12);
			this.label123.TabIndex = 6;
			this.label123.Text = "PORT:";
			this.label124.AutoSize = true;
			this.label124.ImeMode = ImeMode.NoControl;
			this.label124.Location = new Point(19, 18);
			this.label124.Name = "label124";
			this.label124.Size = new Size(23, 12);
			this.label124.TabIndex = 4;
			this.label124.Text = "IP:";
			this.lblWrongProductCode.AutoSize = true;
			this.lblWrongProductCode.BackColor = Color.Red;
			this.lblWrongProductCode.ForeColor = Color.White;
			this.lblWrongProductCode.Location = new Point(21, 182);
			this.lblWrongProductCode.Name = "lblWrongProductCode";
			this.lblWrongProductCode.Size = new Size(101, 12);
			this.lblWrongProductCode.TabIndex = 102;
			this.lblWrongProductCode.Text = "硬件与软件不匹配";
			this.lblWrongProductCode.Visible = false;
			this.label140.AutoSize = true;
			this.label140.BackColor = Color.Red;
			this.label140.ForeColor = Color.White;
			this.label140.Location = new Point(574, 9);
			this.label140.Name = "label140";
			this.label140.Size = new Size(77, 12);
			this.label140.TabIndex = 102;
			this.label140.Text = "V-2012.11.14";
			this.openFileDialog1.FileName = "openFileDialog1";
			this.panel1.Controls.Add(this.label147);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(1068, 769);
			this.panel1.TabIndex = 103;
			this.label147.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.label147.AutoSize = true;
			this.label147.Location = new Point(42, 18);
			this.label147.Name = "label147";
			this.label147.Size = new Size(761, 228);
			this.label147.TabIndex = 0;
			this.label147.Text = componentResourceManager.GetString("label147.Text");
			this.button81.Location = new Point(9, 328);
			this.button81.Name = "button81";
			this.button81.Size = new Size(138, 23);
			this.button81.TabIndex = 2;
			this.button81.Text = "81_以电脑时间设置";
			this.button81.UseVisualStyleBackColor = true;
			this.button81.Click += new EventHandler(this.button14_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1068, 769);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label140);
			base.Controls.Add(this.button45);
			base.Controls.Add(this.lblWrongProductCode);
			base.Controls.Add(this.grpbIP);
			base.Controls.Add(this.checkBox120);
			base.Controls.Add(this.button55);
			base.Controls.Add(this.textBox30);
			base.Controls.Add(this.textBox29);
			base.Controls.Add(this.button51);
			base.Controls.Add(this.label120);
			base.Controls.Add(this.label119);
			base.Controls.Add(this.button49);
			base.Controls.Add(this.button46);
			base.Controls.Add(this.button44);
			base.Controls.Add(this.label114);
			base.Controls.Add(this.button43);
			base.Controls.Add(this.button34);
			base.Controls.Add(this.button32);
			base.Controls.Add(this.button31);
			base.Controls.Add(this.button29);
			base.Controls.Add(this.button24);
			base.Controls.Add(this.button21);
			base.Controls.Add(this.txtSN);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.txtInfo);
			base.Controls.Add(this.button81);
			base.KeyPreview = true;
			base.Name = "frmTestController";
			this.Text = "Test Controller Center";
			base.FormClosing += new FormClosingEventHandler(this.frmTestController_FormClosing);
			base.Load += new EventHandler(this.frmTestController_Load);
			base.KeyDown += new KeyEventHandler(this.frmTestController_KeyDown);
			this.tabControl1.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			this.tabPage7.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox14.ResumeLayout(false);
			this.groupBox14.PerformLayout();
			((ISupportInitialize)this.numericUpDown19).EndInit();
			((ISupportInitialize)this.numericUpDown18).EndInit();
			((ISupportInitialize)this.numericUpDown12).EndInit();
			((ISupportInitialize)this.numericUpDown16).EndInit();
			((ISupportInitialize)this.numericUpDown11).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabPage24.ResumeLayout(false);
			this.tabPage24.PerformLayout();
			this.groupBox19.ResumeLayout(false);
			this.groupBox19.PerformLayout();
			((ISupportInitialize)this.numericUpDown32).EndInit();
			this.groupBox17.ResumeLayout(false);
			this.groupBox17.PerformLayout();
			((ISupportInitialize)this.numericUpDown30).EndInit();
			this.groupBox18.ResumeLayout(false);
			this.groupBox18.PerformLayout();
			((ISupportInitialize)this.numericUpDown31).EndInit();
			this.groupBox16.ResumeLayout(false);
			this.groupBox16.PerformLayout();
			((ISupportInitialize)this.numericUpDown29).EndInit();
			this.groupBox15.ResumeLayout(false);
			this.groupBox15.PerformLayout();
			((ISupportInitialize)this.numericUpDown28).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.groupBox12.ResumeLayout(false);
			this.groupBox12.PerformLayout();
			((ISupportInitialize)this.numericUpDown6).EndInit();
			((ISupportInitialize)this.numericUpDown15).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			((ISupportInitialize)this.dgvControlConfure).EndInit();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.tabPage8.ResumeLayout(false);
			this.tabPage8.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			((ISupportInitialize)this.numericUpDown5).EndInit();
			((ISupportInitialize)this.numericUpDown4).EndInit();
			((ISupportInitialize)this.numericUpDown3).EndInit();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.tabPage9.ResumeLayout(false);
			this.tabPage9.PerformLayout();
			this.tabPage10.ResumeLayout(false);
			this.tabPage10.PerformLayout();
			((ISupportInitialize)this.numericUpDown10).EndInit();
			this.groupBox11.ResumeLayout(false);
			this.groupBox11.PerformLayout();
			((ISupportInitialize)this.numericUpDown9).EndInit();
			((ISupportInitialize)this.numericUpDown8).EndInit();
			((ISupportInitialize)this.numericUpDown7).EndInit();
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.tabPage11.ResumeLayout(false);
			this.tabPage11.PerformLayout();
			((ISupportInitialize)this.numericUpDown14).EndInit();
			((ISupportInitialize)this.numericUpDown13).EndInit();
			this.tabPage12.ResumeLayout(false);
			this.tabPage12.PerformLayout();
			((ISupportInitialize)this.numericUpDown27).EndInit();
			((ISupportInitialize)this.numericUpDown25).EndInit();
			((ISupportInitialize)this.numericUpDown26).EndInit();
			((ISupportInitialize)this.nudDatalen).EndInit();
			((ISupportInitialize)this.nudValue).EndInit();
			((ISupportInitialize)this.nudEndPage).EndInit();
			((ISupportInitialize)this.nudStartPage).EndInit();
			this.tabPage13.ResumeLayout(false);
			this.tabPage13.PerformLayout();
			((ISupportInitialize)this.nudNewSN).EndInit();
			this.tabPage14.ResumeLayout(false);
			this.tabPage15.ResumeLayout(false);
			this.tabPage15.PerformLayout();
			this.tabPage16.ResumeLayout(false);
			this.tabPage16.PerformLayout();
			this.tabPage17.ResumeLayout(false);
			this.tabPage18.ResumeLayout(false);
			this.tabPage18.PerformLayout();
			((ISupportInitialize)this.numericUpDown17).EndInit();
			this.tabPage19.ResumeLayout(false);
			this.tabPage19.PerformLayout();
			this.grpWeekdayControl.ResumeLayout(false);
			this.grpWeekdayControl.PerformLayout();
			this.grpEnd.ResumeLayout(false);
			this.grpEnd.PerformLayout();
			this.grpBegin.ResumeLayout(false);
			this.grpBegin.PerformLayout();
			this.tabPage20.ResumeLayout(false);
			this.tabPage20.PerformLayout();
			((ISupportInitialize)this.numericUpDown22).EndInit();
			((ISupportInitialize)this.numericUpDown20).EndInit();
			((ISupportInitialize)this.numericUpDown21).EndInit();
			this.tabPage21.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView3).EndInit();
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.groupBox13.ResumeLayout(false);
			this.groupBox13.PerformLayout();
			((ISupportInitialize)this.numericUpDown23).EndInit();
			this.tabPage22.ResumeLayout(false);
			this.tabPage22.PerformLayout();
			((ISupportInitialize)this.numericUpDown24).EndInit();
			this.tabPage23.ResumeLayout(false);
			this.tabPage23.PerformLayout();
			this.tabPage25.ResumeLayout(false);
			((ISupportInitialize)this.txtSN).EndInit();
			this.grpbIP.ResumeLayout(false);
			this.grpbIP.PerformLayout();
			((ISupportInitialize)this.nudPort).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
