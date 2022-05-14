using System;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmController : frmN3000
	{
		private bool m_OperateNew = true;

		private int m_ControllerID;

		private icController m_Controller;

		private bool m_ControllerTypeChanged;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		public bool bEditZone;

		private IContainer components;

		private GroupBox grpbController;

		private Label label1;

		private Label label2;

		private Label label4;

		private Label label3;

		private RadioButton optIPSmall;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage3;

		private TabPage tabPage2;

		private GroupBox groupBox15;

		private GroupBox groupBox16;

		private RadioButton optDutyOff4B;

		private RadioButton optDutyOn4B;

		private RadioButton optDutyOnOff4B;

		private CheckBox chkAttend4B;

		private TextBox txtReaderName4B;

		private Label label22;

		private GroupBox groupBox17;

		private RadioButton optDutyOff3B;

		private RadioButton optDutyOn3B;

		private RadioButton optDutyOnOff3B;

		private CheckBox chkAttend3B;

		private TextBox txtReaderName3B;

		private Label label23;

		private GroupBox groupBox18;

		private RadioButton optDutyOff2B;

		private RadioButton optDutyOn2B;

		private RadioButton optDutyOnOff2B;

		private CheckBox chkAttend2B;

		private TextBox txtReaderName2B;

		private Label label24;

		private CheckBox chkDoorActive2B;

		private TextBox txtDoorName2B;

		private Label label27;

		private GroupBox groupBox21;

		private RadioButton optNC2B;

		private RadioButton optNO2B;

		private RadioButton optOnline2B;

		private GroupBox gpbAttend1B;

		private RadioButton optDutyOff1B;

		private RadioButton optDutyOn1B;

		private RadioButton optDutyOnOff1B;

		private CheckBox chkAttend1B;

		private TextBox txtReaderName1B;

		private Label label28;

		private Label label29;

		private Label label30;

		private CheckBox chkDoorActive1B;

		private TextBox txtDoorName1B;

		private Label label31;

		private Label label32;

		private GroupBox groupBox23;

		private RadioButton optNC1B;

		private RadioButton optNO1B;

		private RadioButton optOnline1B;

		private GroupBox groupBox19;

		private Button btnCancel;

		private Label label26;

		private TextBox txtNote;

		private CheckBox chkControllerActive;

		private GroupBox grpbDoorReader;

		private GroupBox grpbIP;

		private Label label34;

		private Label label39;

		private GroupBox groupBox1;

		private CheckBox chkDoorActive4D;

		private TextBox txtDoorName4D;

		private Label label11;

		private GroupBox groupBox6;

		private RadioButton optNC4D;

		private RadioButton optNO4D;

		private RadioButton optOnline4D;

		private CheckBox chkDoorActive3D;

		private TextBox txtDoorName3D;

		private Label label12;

		private GroupBox groupBox7;

		private RadioButton optNC3D;

		private RadioButton optNO3D;

		private RadioButton optOnline3D;

		private Label label13;

		private Label label14;

		private GroupBox groupBox8;

		private RadioButton optDutyOff4D;

		private RadioButton optDutyOn4D;

		private RadioButton optDutyOnOff4D;

		private CheckBox chkAttend4D;

		private TextBox txtReaderName4D;

		private Label label15;

		private GroupBox groupBox9;

		private RadioButton optDutyOff3D;

		private RadioButton optDutyOn3D;

		private RadioButton optDutyOnOff3D;

		private CheckBox chkAttend3D;

		private TextBox txtReaderName3D;

		private Label label16;

		private GroupBox groupBox10;

		private RadioButton optDutyOff2D;

		private RadioButton optDutyOn2D;

		private RadioButton optDutyOnOff2D;

		private CheckBox chkAttend2D;

		private TextBox txtReaderName2D;

		private Label label17;

		private CheckBox chkDoorActive2D;

		private TextBox txtDoorName2D;

		private Label label18;

		private GroupBox groupBox11;

		private RadioButton optNC2D;

		private RadioButton optNO2D;

		private RadioButton optOnline2D;

		private GroupBox groupBox12;

		private RadioButton optDutyOff1D;

		private RadioButton optDutyOn1D;

		private RadioButton optDutyOnOff1D;

		private CheckBox chkAttend1D;

		private TextBox txtReaderName1D;

		private Label label19;

		private Label label20;

		private Label label21;

		private CheckBox chkDoorActive1D;

		private TextBox txtDoorName1D;

		private Label label40;

		private Label label41;

		private GroupBox groupBox13;

		private RadioButton optNC1D;

		private RadioButton optNO1D;

		private RadioButton optOnline1D;

		private Label label33;

		private Label label35;

		private GroupBox groupBox14;

		private RadioButton optDutyOff2A;

		private RadioButton optDutyOn2A;

		private RadioButton optDutyOnOff2A;

		private CheckBox chkAttend2A;

		private TextBox txtReaderName2A;

		private Label label36;

		private GroupBox groupBox20;

		private RadioButton optDutyOff1A;

		private RadioButton optDutyOn1A;

		private RadioButton optDutyOnOff1A;

		private CheckBox chkAttend1A;

		private TextBox txtReaderName1A;

		private Label label37;

		private Label label38;

		private Label label42;

		private CheckBox chkDoorActive1A;

		private TextBox txtDoorName1A;

		private Label label43;

		private Label label44;

		private GroupBox groupBox22;

		private RadioButton optNC1A;

		private RadioButton optNO1A;

		private RadioButton optOnline1A;

		private Button btnCancel2;

		private NumericUpDown nudDoorDelay1D;

		private NumericUpDown nudPort;

		private NumericUpDown nudDoorDelay4D;

		private NumericUpDown nudDoorDelay3D;

		private NumericUpDown nudDoorDelay2D;

		private NumericUpDown nudDoorDelay2B;

		private NumericUpDown nudDoorDelay1B;

		private NumericUpDown nudDoorDelay1A;

		private ComboBox cbof_Zone;

		private Label label25;

		private Button btnZoneManage;

		public MaskedTextBox mtxtbControllerSN;

		public Button btnOK;

		public Button btnNext;

		public RadioButton optIPLarge;

		public TextBox txtControllerIP;

		public MaskedTextBox mtxtbControllerNO;

		private Label label8;

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

		public int ControllerID
		{
			get
			{
				return this.m_ControllerID;
			}
			set
			{
				this.m_ControllerID = value;
			}
		}

		public dfrmController()
		{
			this.InitializeComponent();
		}

		private void dfrmController_Load(object sender, EventArgs e)
		{
			base.Visible = false;
			this.grpbDoorReader.Visible = false;
			this.loadZoneInfo();
			this.mtxtbControllerNO.Mask = "99990";
			this.mtxtbControllerSN.Mask = "000000000";
			if (this.m_OperateNew)
			{
				this.mtxtbControllerNO.Text = (icController.GetMaxControllerNO() + 1).ToString();
			}
			else
			{
				this.m_Controller = new icController();
				this.m_Controller.GetInfoFromDBByControllerID(this.m_ControllerID);
				this.m_Controller.ControllerID = this.m_ControllerID;
				this.mtxtbControllerNO.Text = this.m_Controller.ControllerNO.ToString();
				this.mtxtbControllerSN.Text = this.m_Controller.ControllerSN.ToString();
				this.txtNote.Text = this.m_Controller.Note.ToString();
				this.chkControllerActive.Checked = this.m_Controller.Active;
				if (this.m_Controller.IP == "")
				{
					this.optIPSmall.Checked = true;
				}
				else
				{
					this.optIPLarge.Checked = true;
					this.txtControllerIP.Text = this.m_Controller.IP;
					this.nudPort.Value = this.m_Controller.PORT;
				}
				if (this.m_Controller.ZoneID > 0)
				{
					if (this.cbof_Zone.Items.Count > 0)
					{
						this.cbof_Zone.SelectedIndex = 0;
					}
					for (int i = 0; i < this.cbof_Zone.Items.Count; i++)
					{
						if ((int)this.arrZoneID[i] == this.m_Controller.ZoneID)
						{
							this.cbof_Zone.SelectedIndex = i;
							break;
						}
					}
				}
			}
			this.grpbIP.Visible = this.optIPLarge.Checked;
			base.Visible = true;
			this.mtxtbControllerSN.Focus();
			this.btnZoneManage.Visible = false;
			bool flag = false;
			string funName = "btnZoneManage";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && !flag)
			{
				this.btnZoneManage.Visible = true;
			}
			this.tabPage1.BackColor = this.BackColor;
			this.tabPage2.BackColor = this.BackColor;
			this.tabPage3.BackColor = this.BackColor;
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cbof_Zone.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				this.cbof_Zone.Items.Add(this.arrZoneName[i].ToString());
			}
			if (this.cbof_Zone.Items.Count > 0)
			{
				this.cbof_Zone.SelectedIndex = 0;
			}
			bool visible = true;
			this.label25.Visible = visible;
			this.cbof_Zone.Visible = visible;
			this.btnZoneManage.Visible = visible;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.mtxtbControllerNO.Text = this.mtxtbControllerNO.Text.Replace(" ", "");
			this.mtxtbControllerSN.Text = this.mtxtbControllerSN.Text.Replace(" ", "");
			int num;
			if (!int.TryParse(this.mtxtbControllerNO.Text, out num))
			{
				XMessageBox.Show(this, CommonStr.strIDWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (int.Parse(this.mtxtbControllerNO.Text) > 100000 || int.Parse(this.mtxtbControllerNO.Text) < 0)
			{
				XMessageBox.Show(this, CommonStr.strIDWrong + ", <1000000 , >0", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (!int.TryParse(this.mtxtbControllerSN.Text, out num))
			{
				XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (wgMjController.GetControllerType(int.Parse(this.mtxtbControllerSN.Text)) == 0)
			{
				XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.optIPLarge.Checked && string.IsNullOrEmpty(this.txtControllerIP.Text))
			{
				XMessageBox.Show(this, CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.m_OperateNew)
			{
				if (icController.IsExisted2SN(int.Parse(this.mtxtbControllerSN.Text), 0))
				{
					XMessageBox.Show(this, CommonStr.strSNAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (icController.IsExisted2NO(int.Parse(this.mtxtbControllerNO.Text), 0))
				{
					XMessageBox.Show(this, CommonStr.strControllerNOAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			else
			{
				if (icController.IsExisted2SN(int.Parse(this.mtxtbControllerSN.Text), this.m_ControllerID))
				{
					XMessageBox.Show(this, CommonStr.strSNAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (icController.IsExisted2NO(int.Parse(this.mtxtbControllerNO.Text), this.m_ControllerID))
				{
					XMessageBox.Show(this, CommonStr.strControllerNOAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			if (this.optIPLarge.Checked)
			{
				this.txtControllerIP.Text = this.txtControllerIP.Text.Replace(" ", "");
				if (string.IsNullOrEmpty(this.txtControllerIP.Text))
				{
					XMessageBox.Show(this, CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			int controllerType = wgMjController.GetControllerType(int.Parse(this.mtxtbControllerSN.Text));
			if (controllerType > 0)
			{
				switch (controllerType)
				{
				case 1:
					this.tabControl1.Controls.Remove(this.tabPage1);
					this.tabPage1.Dispose();
					this.tabControl1.Controls.Remove(this.tabPage2);
					this.tabPage2.Dispose();
					if (int.Parse(this.mtxtbControllerSN.Text) >= 170000000 && int.Parse(this.mtxtbControllerSN.Text) <= 179999999)
					{
						this.label43.Text = CommonStr.strElevatorName;
						this.tabPage3.Text = CommonStr.strElevatorController;
						this.txtReaderName1A.Text = CommonStr.strElevator;
						if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 2)
						{
							this.tabPage3.Text = CommonStr.strElevatorController2;
							this.txtReaderName1A.Text = CommonStr.strElevator2;
						}
						else if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 3)
						{
							this.tabPage3.Text = CommonStr.strElevatorController3;
							this.txtReaderName1A.Text = CommonStr.strElevator3;
						}
						this.label36.Visible = false;
						this.label37.Visible = false;
						this.label38.Visible = false;
						this.label42.Visible = false;
						this.label44.Visible = false;
						this.groupBox22.Visible = false;
						this.nudDoorDelay1A.Visible = false;
						this.txtReaderName2A.Visible = false;
						this.chkAttend2A.Visible = false;
						goto IL_50C;
					}
					goto IL_50C;
				case 2:
					this.tabControl1.Controls.Remove(this.tabPage1);
					this.tabPage1.Dispose();
					this.tabControl1.Controls.Remove(this.tabPage3);
					this.tabPage3.Dispose();
					goto IL_50C;
				case 4:
					this.tabControl1.Controls.Remove(this.tabPage2);
					this.tabPage2.Dispose();
					this.tabControl1.Controls.Remove(this.tabPage3);
					this.tabPage3.Dispose();
					goto IL_50C;
				}
				this.tabControl1.Controls.Remove(this.tabPage2);
				this.tabPage2.Dispose();
				this.tabControl1.Controls.Remove(this.tabPage3);
				this.tabPage3.Dispose();
			}
			IL_50C:
			if (!this.m_OperateNew && wgMjController.GetControllerType(this.m_Controller.ControllerSN) == wgMjController.GetControllerType(int.Parse(this.mtxtbControllerSN.Text)))
			{
				this.m_ControllerTypeChanged = false;
				switch (controllerType)
				{
				case 1:
					this.chkDoorActive1A.Checked = this.m_Controller.GetDoorActive(1);
					this.optOnline1A.Checked = (this.m_Controller.GetDoorControl(1) == 3);
					this.optNO1A.Checked = (this.m_Controller.GetDoorControl(1) == 1);
					this.optNC1A.Checked = (this.m_Controller.GetDoorControl(1) == 2);
					this.nudDoorDelay1A.Value = this.m_Controller.GetDoorDelay(1);
					this.txtReaderName1A.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(1), this.m_Controller.GetDoorName(1) + "-");
					this.chkAttend1A.Checked = this.m_Controller.GetReaderAsAttendActive(1);
					this.optDutyOnOff1A.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 3);
					this.optDutyOn1A.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 2);
					this.optDutyOff1A.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 1);
					this.txtReaderName2A.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(2), this.m_Controller.GetDoorName(1) + "-");
					this.chkAttend2A.Checked = this.m_Controller.GetReaderAsAttendActive(2);
					this.optDutyOnOff2A.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 3);
					this.optDutyOn2A.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 2);
					this.optDutyOff2A.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 1);
					if (this.m_Controller.ControllerSN != int.Parse(this.mtxtbControllerSN.Text))
					{
						this.txtDoorName1A.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(1), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
						goto IL_13D5;
					}
					this.txtDoorName1A.Text = this.m_Controller.GetDoorName(1);
					goto IL_13D5;
				case 2:
					this.chkDoorActive1B.Checked = this.m_Controller.GetDoorActive(1);
					this.optOnline1B.Checked = (this.m_Controller.GetDoorControl(1) == 3);
					this.optNO1B.Checked = (this.m_Controller.GetDoorControl(1) == 1);
					this.optNC1B.Checked = (this.m_Controller.GetDoorControl(1) == 2);
					this.nudDoorDelay1B.Value = this.m_Controller.GetDoorDelay(1);
					this.txtReaderName1B.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(1), this.m_Controller.GetDoorName(1) + "-");
					this.chkAttend1B.Checked = this.m_Controller.GetReaderAsAttendActive(1);
					this.optDutyOnOff1B.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 3);
					this.optDutyOn1B.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 2);
					this.optDutyOff1B.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 1);
					this.txtReaderName2B.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(2), this.m_Controller.GetDoorName(1) + "-");
					this.chkAttend2B.Checked = this.m_Controller.GetReaderAsAttendActive(2);
					this.optDutyOnOff2B.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 3);
					this.optDutyOn2B.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 2);
					this.optDutyOff2B.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 1);
					this.chkDoorActive2B.Checked = this.m_Controller.GetDoorActive(2);
					this.optOnline2B.Checked = (this.m_Controller.GetDoorControl(2) == 3);
					this.optNO2B.Checked = (this.m_Controller.GetDoorControl(2) == 1);
					this.optNC2B.Checked = (this.m_Controller.GetDoorControl(2) == 2);
					this.nudDoorDelay2B.Value = this.m_Controller.GetDoorDelay(2);
					this.txtReaderName3B.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(3), this.m_Controller.GetDoorName(2) + "-");
					this.chkAttend3B.Checked = this.m_Controller.GetReaderAsAttendActive(3);
					this.optDutyOnOff3B.Checked = (this.m_Controller.GetReaderAsAttendControl(3) == 3);
					this.optDutyOn3B.Checked = (this.m_Controller.GetReaderAsAttendControl(3) == 2);
					this.optDutyOff3B.Checked = (this.m_Controller.GetReaderAsAttendControl(3) == 1);
					this.txtReaderName4B.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(4), this.m_Controller.GetDoorName(2) + "-");
					this.chkAttend4B.Checked = this.m_Controller.GetReaderAsAttendActive(4);
					this.optDutyOnOff4B.Checked = (this.m_Controller.GetReaderAsAttendControl(4) == 3);
					this.optDutyOn4B.Checked = (this.m_Controller.GetReaderAsAttendControl(4) == 2);
					this.optDutyOff4B.Checked = (this.m_Controller.GetReaderAsAttendControl(4) == 1);
					if (this.m_Controller.ControllerNO != int.Parse(this.mtxtbControllerNO.Text))
					{
						this.txtDoorName1B.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(1), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
						this.txtDoorName2B.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(2), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
						goto IL_13D5;
					}
					this.txtDoorName1B.Text = this.m_Controller.GetDoorName(1);
					this.txtDoorName2B.Text = this.m_Controller.GetDoorName(2);
					goto IL_13D5;
				}
				this.chkDoorActive1D.Checked = this.m_Controller.GetDoorActive(1);
				this.optOnline1D.Checked = (this.m_Controller.GetDoorControl(1) == 3);
				this.optNO1D.Checked = (this.m_Controller.GetDoorControl(1) == 1);
				this.optNC1D.Checked = (this.m_Controller.GetDoorControl(1) == 2);
				this.nudDoorDelay1D.Value = this.m_Controller.GetDoorDelay(1);
				this.chkDoorActive2D.Checked = this.m_Controller.GetDoorActive(2);
				this.optOnline2D.Checked = (this.m_Controller.GetDoorControl(2) == 3);
				this.optNO2D.Checked = (this.m_Controller.GetDoorControl(2) == 1);
				this.optNC2D.Checked = (this.m_Controller.GetDoorControl(2) == 2);
				this.nudDoorDelay2D.Value = this.m_Controller.GetDoorDelay(2);
				this.chkDoorActive3D.Checked = this.m_Controller.GetDoorActive(3);
				this.optOnline3D.Checked = (this.m_Controller.GetDoorControl(3) == 3);
				this.optNO3D.Checked = (this.m_Controller.GetDoorControl(3) == 1);
				this.optNC3D.Checked = (this.m_Controller.GetDoorControl(3) == 2);
				this.nudDoorDelay3D.Value = this.m_Controller.GetDoorDelay(3);
				this.chkDoorActive4D.Checked = this.m_Controller.GetDoorActive(4);
				this.optOnline4D.Checked = (this.m_Controller.GetDoorControl(4) == 3);
				this.optNO4D.Checked = (this.m_Controller.GetDoorControl(4) == 1);
				this.optNC4D.Checked = (this.m_Controller.GetDoorControl(4) == 2);
				this.nudDoorDelay4D.Value = this.m_Controller.GetDoorDelay(4);
				this.txtReaderName1D.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(1), this.m_Controller.GetDoorName(1) + "-");
				this.chkAttend1D.Checked = this.m_Controller.GetReaderAsAttendActive(1);
				this.optDutyOnOff1D.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 3);
				this.optDutyOn1D.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 2);
				this.optDutyOff1D.Checked = (this.m_Controller.GetReaderAsAttendControl(1) == 1);
				this.txtReaderName2D.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(2), this.m_Controller.GetDoorName(2) + "-");
				this.chkAttend2D.Checked = this.m_Controller.GetReaderAsAttendActive(2);
				this.optDutyOnOff2D.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 3);
				this.optDutyOn2D.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 2);
				this.optDutyOff2D.Checked = (this.m_Controller.GetReaderAsAttendControl(2) == 1);
				this.txtReaderName3D.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(3), this.m_Controller.GetDoorName(3) + "-");
				this.chkAttend3D.Checked = this.m_Controller.GetReaderAsAttendActive(3);
				this.optDutyOnOff3D.Checked = (this.m_Controller.GetReaderAsAttendControl(3) == 3);
				this.optDutyOn3D.Checked = (this.m_Controller.GetReaderAsAttendControl(3) == 2);
				this.optDutyOff3D.Checked = (this.m_Controller.GetReaderAsAttendControl(3) == 1);
				this.txtReaderName4D.Text = icController.StrDelFirstSame(this.m_Controller.GetReaderName(4), this.m_Controller.GetDoorName(4) + "-");
				this.chkAttend4D.Checked = this.m_Controller.GetReaderAsAttendActive(4);
				this.optDutyOnOff4D.Checked = (this.m_Controller.GetReaderAsAttendControl(4) == 3);
				this.optDutyOn4D.Checked = (this.m_Controller.GetReaderAsAttendControl(4) == 2);
				this.optDutyOff4D.Checked = (this.m_Controller.GetReaderAsAttendControl(4) == 1);
				if (this.m_Controller.ControllerNO != int.Parse(this.mtxtbControllerNO.Text))
				{
					this.txtDoorName1D.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(1), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
					this.txtDoorName2D.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(2), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
					this.txtDoorName3D.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(3), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
					this.txtDoorName4D.Text = icController.StrReplaceFirstSame(this.m_Controller.GetDoorName(4), this.m_Controller.ControllerNO.ToString() + "-", this.mtxtbControllerNO.Text + "-");
				}
				else
				{
					this.txtDoorName1D.Text = this.m_Controller.GetDoorName(1);
					this.txtDoorName2D.Text = this.m_Controller.GetDoorName(2);
					this.txtDoorName3D.Text = this.m_Controller.GetDoorName(3);
					this.txtDoorName4D.Text = this.m_Controller.GetDoorName(4);
				}
			}
			else
			{
				this.m_ControllerTypeChanged = true;
				switch (controllerType)
				{
				case 1:
					this.txtDoorName1A.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName1A.Text;
					goto IL_13D5;
				case 2:
					this.txtDoorName1B.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName1B.Text;
					this.txtDoorName2B.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName2B.Text;
					goto IL_13D5;
				}
				this.txtDoorName1D.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName1D.Text;
				this.txtDoorName2D.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName2D.Text;
				this.txtDoorName3D.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName3D.Text;
				this.txtDoorName4D.Text = "m" + this.mtxtbControllerNO.Text.PadLeft(3, '0') + "-" + this.txtDoorName4D.Text;
			}
			IL_13D5:
			string cmdText = "Select * from  [t_b_Reader] where NOT (f_DutyOnOff =3)";
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						bool flag = false;
						if (oleDbDataReader.Read())
						{
							flag = true;
						}
						oleDbDataReader.Close();
						if (flag && controllerType > 0)
						{
							switch (controllerType)
							{
							case 1:
								this.label33.Visible = true;
								this.groupBox20.Visible = true;
								this.groupBox14.Visible = true;
								break;
							case 2:
								this.label39.Visible = true;
								this.groupBox16.Visible = true;
								this.groupBox17.Visible = true;
								this.groupBox18.Visible = true;
								this.gpbAttend1B.Visible = true;
								break;
							case 4:
								this.label13.Visible = true;
								this.groupBox8.Visible = true;
								this.groupBox9.Visible = true;
								this.groupBox10.Visible = true;
								this.groupBox12.Visible = true;
								break;
							}
						}
					}
					goto IL_1631;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					bool flag2 = false;
					if (sqlDataReader.Read())
					{
						flag2 = true;
					}
					sqlDataReader.Close();
					if (flag2 && controllerType > 0)
					{
						switch (controllerType)
						{
						case 1:
							this.label33.Visible = true;
							this.groupBox20.Visible = true;
							this.groupBox14.Visible = true;
							break;
						case 2:
							this.label39.Visible = true;
							this.groupBox16.Visible = true;
							this.groupBox17.Visible = true;
							this.groupBox18.Visible = true;
							this.gpbAttend1B.Visible = true;
							break;
						case 4:
							this.label13.Visible = true;
							this.groupBox8.Visible = true;
							this.groupBox9.Visible = true;
							this.groupBox10.Visible = true;
							this.groupBox12.Visible = true;
							break;
						}
					}
				}
			}
			IL_1631:
			this.grpbDoorReader.Location = new Point(2, 5);
			base.Size = new Size(base.Size.Width, this.grpbDoorReader.Height + 40);
			base.AcceptButton = this.btnOK;
			this.grpbDoorReader.Visible = true;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		public void btnOK_Click(object sender, EventArgs e)
		{
			this.mtxtbControllerNO.Text = this.mtxtbControllerNO.Text.Replace(" ", "");
			this.mtxtbControllerSN.Text = this.mtxtbControllerSN.Text.Replace(" ", "");
			int num;
			if (!int.TryParse(this.mtxtbControllerNO.Text, out num))
			{
				XMessageBox.Show(this, CommonStr.strIDWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (int.Parse(this.mtxtbControllerNO.Text) > 100000 || int.Parse(this.mtxtbControllerNO.Text) < 0)
			{
				XMessageBox.Show(this, CommonStr.strIDWrong + ", <=100000 , >0", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (!int.TryParse(this.mtxtbControllerSN.Text, out num))
			{
				XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (wgMjController.GetControllerType(int.Parse(this.mtxtbControllerSN.Text)) == 0)
			{
				XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.optIPLarge.Checked && string.IsNullOrEmpty(this.txtControllerIP.Text))
			{
				XMessageBox.Show(this, CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.m_OperateNew)
			{
				if (icController.IsExisted2SN(int.Parse(this.mtxtbControllerSN.Text), 0))
				{
					XMessageBox.Show(this, CommonStr.strSNAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			else if (icController.IsExisted2SN(int.Parse(this.mtxtbControllerSN.Text), this.m_ControllerID))
			{
				XMessageBox.Show(this, CommonStr.strSNAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.m_OperateNew && this.m_Controller == null)
			{
				this.m_Controller = new icController();
			}
			icController controller = this.m_Controller;
			controller.ControllerNO = int.Parse(this.mtxtbControllerNO.Text);
			controller.ControllerSN = int.Parse(this.mtxtbControllerSN.Text);
			controller.Note = this.txtNote.Text.ToString();
			controller.Active = this.chkControllerActive.Checked;
			controller.IP = "";
			controller.PORT = 60000;
			if (this.cbof_Zone.SelectedIndex < 0)
			{
				controller.ZoneID = 0;
			}
			else
			{
				controller.ZoneID = (int)this.arrZoneID[this.cbof_Zone.SelectedIndex];
			}
			if (this.optIPLarge.Checked)
			{
				controller.IP = this.txtControllerIP.Text;
				controller.PORT = (int)this.nudPort.Value;
			}
			switch (wgMjController.GetControllerType(controller.ControllerSN))
			{
			case 1:
				controller.SetDoorName(1, this.txtDoorName1A.Text);
				controller.SetDoorActive(1, this.chkDoorActive1A.Checked);
				if (this.optOnline1A.Checked)
				{
					controller.SetDoorControl(1, 3);
				}
				else if (this.optNO1A.Checked)
				{
					controller.SetDoorControl(1, 1);
				}
				else if (this.optNC1A.Checked)
				{
					controller.SetDoorControl(1, 2);
				}
				controller.SetDoorDelay(1, (int)this.nudDoorDelay1A.Value);
				controller.SetReaderName(1, string.Format("{0}-{1}", this.txtDoorName1A.Text, this.txtReaderName1A.Text));
				controller.SetReaderName(2, string.Format("{0}-{1}", this.txtDoorName1A.Text, this.txtReaderName2A.Text));
				controller.SetReaderAsAttendActive(1, this.chkAttend1A.Checked);
				controller.SetReaderAsAttendActive(2, this.chkAttend2A.Checked);
				if (this.optDutyOnOff1A.Checked)
				{
					controller.SetReaderAsAttendControl(1, 3);
				}
				else if (this.optDutyOn1A.Checked)
				{
					controller.SetReaderAsAttendControl(1, 2);
				}
				else if (this.optDutyOff1A.Checked)
				{
					controller.SetReaderAsAttendControl(1, 1);
				}
				if (this.optDutyOnOff2A.Checked)
				{
					controller.SetReaderAsAttendControl(2, 3);
					goto IL_B34;
				}
				if (this.optDutyOn2A.Checked)
				{
					controller.SetReaderAsAttendControl(2, 2);
					goto IL_B34;
				}
				if (this.optDutyOff2A.Checked)
				{
					controller.SetReaderAsAttendControl(2, 1);
					goto IL_B34;
				}
				goto IL_B34;
			case 2:
				controller.SetDoorName(1, this.txtDoorName1B.Text);
				controller.SetDoorName(2, this.txtDoorName2B.Text);
				controller.SetDoorActive(1, this.chkDoorActive1B.Checked);
				controller.SetDoorActive(2, this.chkDoorActive2B.Checked);
				if (this.optOnline1B.Checked)
				{
					controller.SetDoorControl(1, 3);
				}
				else if (this.optNO1B.Checked)
				{
					controller.SetDoorControl(1, 1);
				}
				else if (this.optNC1B.Checked)
				{
					controller.SetDoorControl(1, 2);
				}
				if (this.optOnline2B.Checked)
				{
					controller.SetDoorControl(2, 3);
				}
				else if (this.optNO2B.Checked)
				{
					controller.SetDoorControl(2, 1);
				}
				else if (this.optNC2B.Checked)
				{
					controller.SetDoorControl(2, 2);
				}
				controller.SetDoorDelay(1, (int)this.nudDoorDelay1B.Value);
				controller.SetDoorDelay(2, (int)this.nudDoorDelay2B.Value);
				controller.SetReaderName(1, string.Format("{0}-{1}", this.txtDoorName1B.Text, this.txtReaderName1B.Text));
				controller.SetReaderName(2, string.Format("{0}-{1}", this.txtDoorName1B.Text, this.txtReaderName2B.Text));
				controller.SetReaderName(3, string.Format("{0}-{1}", this.txtDoorName2B.Text, this.txtReaderName3B.Text));
				controller.SetReaderName(4, string.Format("{0}-{1}", this.txtDoorName2B.Text, this.txtReaderName4B.Text));
				controller.SetReaderAsAttendActive(1, this.chkAttend1B.Checked);
				controller.SetReaderAsAttendActive(2, this.chkAttend2B.Checked);
				controller.SetReaderAsAttendActive(3, this.chkAttend3B.Checked);
				controller.SetReaderAsAttendActive(4, this.chkAttend4B.Checked);
				if (this.optDutyOnOff1B.Checked)
				{
					controller.SetReaderAsAttendControl(1, 3);
				}
				else if (this.optDutyOn1B.Checked)
				{
					controller.SetReaderAsAttendControl(1, 2);
				}
				else if (this.optDutyOff1B.Checked)
				{
					controller.SetReaderAsAttendControl(1, 1);
				}
				if (this.optDutyOnOff2B.Checked)
				{
					controller.SetReaderAsAttendControl(2, 3);
				}
				else if (this.optDutyOn2B.Checked)
				{
					controller.SetReaderAsAttendControl(2, 2);
				}
				else if (this.optDutyOff2B.Checked)
				{
					controller.SetReaderAsAttendControl(2, 1);
				}
				if (this.optDutyOnOff3B.Checked)
				{
					controller.SetReaderAsAttendControl(3, 3);
				}
				else if (this.optDutyOn3B.Checked)
				{
					controller.SetReaderAsAttendControl(3, 2);
				}
				else if (this.optDutyOff3B.Checked)
				{
					controller.SetReaderAsAttendControl(3, 1);
				}
				if (this.optDutyOnOff4B.Checked)
				{
					controller.SetReaderAsAttendControl(4, 3);
					goto IL_B34;
				}
				if (this.optDutyOn4B.Checked)
				{
					controller.SetReaderAsAttendControl(4, 2);
					goto IL_B34;
				}
				if (this.optDutyOff4B.Checked)
				{
					controller.SetReaderAsAttendControl(4, 1);
					goto IL_B34;
				}
				goto IL_B34;
			}
			controller.SetDoorName(1, this.txtDoorName1D.Text);
			controller.SetDoorName(2, this.txtDoorName2D.Text);
			controller.SetDoorName(3, this.txtDoorName3D.Text);
			controller.SetDoorName(4, this.txtDoorName4D.Text);
			controller.SetDoorActive(1, this.chkDoorActive1D.Checked);
			controller.SetDoorActive(2, this.chkDoorActive2D.Checked);
			controller.SetDoorActive(3, this.chkDoorActive3D.Checked);
			controller.SetDoorActive(4, this.chkDoorActive4D.Checked);
			if (this.optOnline1D.Checked)
			{
				controller.SetDoorControl(1, 3);
			}
			else if (this.optNO1D.Checked)
			{
				controller.SetDoorControl(1, 1);
			}
			else if (this.optNC1D.Checked)
			{
				controller.SetDoorControl(1, 2);
			}
			if (this.optOnline2D.Checked)
			{
				controller.SetDoorControl(2, 3);
			}
			else if (this.optNO2D.Checked)
			{
				controller.SetDoorControl(2, 1);
			}
			else if (this.optNC2D.Checked)
			{
				controller.SetDoorControl(2, 2);
			}
			if (this.optOnline3D.Checked)
			{
				controller.SetDoorControl(3, 3);
			}
			else if (this.optNO3D.Checked)
			{
				controller.SetDoorControl(3, 1);
			}
			else if (this.optNC3D.Checked)
			{
				controller.SetDoorControl(3, 2);
			}
			if (this.optOnline4D.Checked)
			{
				controller.SetDoorControl(4, 3);
			}
			else if (this.optNO4D.Checked)
			{
				controller.SetDoorControl(4, 1);
			}
			else if (this.optNC4D.Checked)
			{
				controller.SetDoorControl(4, 2);
			}
			controller.SetDoorDelay(1, (int)this.nudDoorDelay1D.Value);
			controller.SetDoorDelay(2, (int)this.nudDoorDelay2D.Value);
			controller.SetDoorDelay(3, (int)this.nudDoorDelay3D.Value);
			controller.SetDoorDelay(4, (int)this.nudDoorDelay4D.Value);
			controller.SetReaderName(1, string.Format("{0}-{1}", this.txtDoorName1D.Text, this.txtReaderName1D.Text));
			controller.SetReaderName(2, string.Format("{0}-{1}", this.txtDoorName2D.Text, this.txtReaderName2D.Text));
			controller.SetReaderName(3, string.Format("{0}-{1}", this.txtDoorName3D.Text, this.txtReaderName3D.Text));
			controller.SetReaderName(4, string.Format("{0}-{1}", this.txtDoorName4D.Text, this.txtReaderName4D.Text));
			controller.SetReaderAsAttendActive(1, this.chkAttend1D.Checked);
			controller.SetReaderAsAttendActive(2, this.chkAttend2D.Checked);
			controller.SetReaderAsAttendActive(3, this.chkAttend3D.Checked);
			controller.SetReaderAsAttendActive(4, this.chkAttend4D.Checked);
			if (this.optDutyOnOff1D.Checked)
			{
				controller.SetReaderAsAttendControl(1, 3);
			}
			else if (this.optDutyOn1D.Checked)
			{
				controller.SetReaderAsAttendControl(1, 2);
			}
			else if (this.optDutyOff1D.Checked)
			{
				controller.SetReaderAsAttendControl(1, 1);
			}
			if (this.optDutyOnOff2D.Checked)
			{
				controller.SetReaderAsAttendControl(2, 3);
			}
			else if (this.optDutyOn2D.Checked)
			{
				controller.SetReaderAsAttendControl(2, 2);
			}
			else if (this.optDutyOff2D.Checked)
			{
				controller.SetReaderAsAttendControl(2, 1);
			}
			if (this.optDutyOnOff3D.Checked)
			{
				controller.SetReaderAsAttendControl(3, 3);
			}
			else if (this.optDutyOn3D.Checked)
			{
				controller.SetReaderAsAttendControl(3, 2);
			}
			else if (this.optDutyOff3D.Checked)
			{
				controller.SetReaderAsAttendControl(3, 1);
			}
			if (this.optDutyOnOff4D.Checked)
			{
				controller.SetReaderAsAttendControl(4, 3);
			}
			else if (this.optDutyOn4D.Checked)
			{
				controller.SetReaderAsAttendControl(4, 2);
			}
			else if (this.optDutyOff4D.Checked)
			{
				controller.SetReaderAsAttendControl(4, 1);
			}
			IL_B34:
			int num2;
			string strMsg;
			if (this.m_OperateNew)
			{
				num2 = controller.AddIntoDB();
				strMsg = string.Concat(new string[]
				{
					CommonStr.strAddController,
					":(",
					controller.ControllerNO.ToString(),
					")",
					controller.ControllerSN.ToString()
				});
			}
			else
			{
				num2 = controller.UpdateIntoDB(this.m_ControllerTypeChanged);
				strMsg = string.Concat(new string[]
				{
					CommonStr.strUpdateController,
					":(",
					controller.ControllerNO.ToString(),
					")",
					controller.ControllerSN.ToString()
				});
			}
			if (num2 < 0)
			{
				XMessageBox.Show(this, CommonStr.strValWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				wgTools.WgDebugWrite("Controller Ret=" + num2.ToString(), new object[0]);
				return;
			}
			if (sender != null)
			{
				wgAppConfig.wgLog(strMsg);
				base.Close();
				return;
			}
			this.txtDoorName1A.Text = "1" + CommonStr.strDoorNO;
			this.txtDoorName1B.Text = "1" + CommonStr.strDoorNO;
			this.txtDoorName2B.Text = "2" + CommonStr.strDoorNO;
			this.txtDoorName1D.Text = "1" + CommonStr.strDoorNO;
			this.txtDoorName2D.Text = "2" + CommonStr.strDoorNO;
			this.txtDoorName3D.Text = "3" + CommonStr.strDoorNO;
			this.txtDoorName4D.Text = "4" + CommonStr.strDoorNO;
		}

		private void optIPLarge_CheckedChanged(object sender, EventArgs e)
		{
			this.grpbIP.Visible = this.optIPLarge.Checked;
		}

		private void btnCancel2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnZoneManage_Click(object sender, EventArgs e)
		{
			using (frmZones frmZones = new frmZones())
			{
				frmZones.ShowDialog(this);
			}
			this.bEditZone = true;
			this.loadZoneInfo();
			if (this.m_Controller == null)
			{
				if (this.cbof_Zone.Items.Count > 0)
				{
					this.cbof_Zone.SelectedIndex = 0;
					return;
				}
			}
			else if (this.m_Controller.ZoneID > 0)
			{
				if (this.cbof_Zone.Items.Count > 0)
				{
					this.cbof_Zone.SelectedIndex = 0;
				}
				for (int i = 0; i < this.cbof_Zone.Items.Count; i++)
				{
					if ((int)this.arrZoneID[i] == this.m_Controller.ZoneID)
					{
						this.cbof_Zone.SelectedIndex = i;
						return;
					}
				}
			}
		}

		private void dfrmController_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				int controllerType = wgMjController.GetControllerType(int.Parse(this.mtxtbControllerSN.Text));
				if (controllerType > 0)
				{
					switch (controllerType)
					{
					case 1:
						this.label33.Visible = true;
						this.groupBox20.Visible = true;
						this.groupBox14.Visible = true;
						return;
					case 2:
						this.label39.Visible = true;
						this.groupBox16.Visible = true;
						this.groupBox17.Visible = true;
						this.groupBox18.Visible = true;
						this.gpbAttend1B.Visible = true;
						return;
					case 3:
						break;
					case 4:
						this.label13.Visible = true;
						this.groupBox8.Visible = true;
						this.groupBox9.Visible = true;
						this.groupBox10.Visible = true;
						this.groupBox12.Visible = true;
						break;
					default:
						return;
					}
				}
			}
		}

		private void dfrmController_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		public static void SNInput(ref MaskedTextBox mtb)
		{
			if (mtb.Text.Length != mtb.Text.Trim().Length)
			{
				mtb.Text = mtb.Text.Trim();
			}
			else if (mtb.Text.Length == 0 && mtb.SelectionStart != 0)
			{
				mtb.SelectionStart = 0;
			}
			if (mtb.Text.Length > 0)
			{
				if (mtb.Text.IndexOf(" ") > 0)
				{
					mtb.Text = mtb.Text.Replace(" ", "");
				}
				if (mtb.Text.Length > 9 && long.Parse(mtb.Text) >= (long)((ulong)-1))
				{
					mtb.Text = mtb.Text.Substring(0, mtb.Text.Length - 1);
				}
			}
		}

		private void mtxtbControllerSN_KeyPress(object sender, KeyPressEventArgs e)
		{
			dfrmController.SNInput(ref this.mtxtbControllerSN);
		}

		private void mtxtbControllerSN_KeyUp(object sender, KeyEventArgs e)
		{
			dfrmController.SNInput(ref this.mtxtbControllerSN);
		}

		private void mtxtbControllerNO_KeyPress(object sender, KeyPressEventArgs e)
		{
			dfrmController.SNInput(ref this.mtxtbControllerNO);
		}

		private void mtxtbControllerNO_KeyUp(object sender, KeyEventArgs e)
		{
			dfrmController.SNInput(ref this.mtxtbControllerNO);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_Controller != null)
			{
				this.m_Controller.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmController));
			this.btnNext = new Button();
			this.btnCancel = new Button();
			this.grpbDoorReader = new GroupBox();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.groupBox1 = new GroupBox();
			this.nudDoorDelay4D = new NumericUpDown();
			this.nudDoorDelay3D = new NumericUpDown();
			this.nudDoorDelay2D = new NumericUpDown();
			this.nudDoorDelay1D = new NumericUpDown();
			this.chkDoorActive4D = new CheckBox();
			this.txtDoorName4D = new TextBox();
			this.label11 = new Label();
			this.groupBox6 = new GroupBox();
			this.optNC4D = new RadioButton();
			this.optNO4D = new RadioButton();
			this.optOnline4D = new RadioButton();
			this.chkDoorActive3D = new CheckBox();
			this.txtDoorName3D = new TextBox();
			this.label12 = new Label();
			this.groupBox7 = new GroupBox();
			this.optNC3D = new RadioButton();
			this.optNO3D = new RadioButton();
			this.optOnline3D = new RadioButton();
			this.label13 = new Label();
			this.label14 = new Label();
			this.groupBox8 = new GroupBox();
			this.optDutyOff4D = new RadioButton();
			this.optDutyOn4D = new RadioButton();
			this.optDutyOnOff4D = new RadioButton();
			this.chkAttend4D = new CheckBox();
			this.txtReaderName4D = new TextBox();
			this.label15 = new Label();
			this.groupBox9 = new GroupBox();
			this.optDutyOff3D = new RadioButton();
			this.optDutyOn3D = new RadioButton();
			this.optDutyOnOff3D = new RadioButton();
			this.chkAttend3D = new CheckBox();
			this.txtReaderName3D = new TextBox();
			this.label16 = new Label();
			this.groupBox10 = new GroupBox();
			this.optDutyOff2D = new RadioButton();
			this.optDutyOn2D = new RadioButton();
			this.optDutyOnOff2D = new RadioButton();
			this.chkAttend2D = new CheckBox();
			this.txtReaderName2D = new TextBox();
			this.label17 = new Label();
			this.chkDoorActive2D = new CheckBox();
			this.txtDoorName2D = new TextBox();
			this.label18 = new Label();
			this.groupBox11 = new GroupBox();
			this.optNC2D = new RadioButton();
			this.optNO2D = new RadioButton();
			this.optOnline2D = new RadioButton();
			this.groupBox12 = new GroupBox();
			this.optDutyOff1D = new RadioButton();
			this.optDutyOn1D = new RadioButton();
			this.optDutyOnOff1D = new RadioButton();
			this.chkAttend1D = new CheckBox();
			this.txtReaderName1D = new TextBox();
			this.label19 = new Label();
			this.label20 = new Label();
			this.label21 = new Label();
			this.chkDoorActive1D = new CheckBox();
			this.txtDoorName1D = new TextBox();
			this.label40 = new Label();
			this.label41 = new Label();
			this.groupBox13 = new GroupBox();
			this.optNC1D = new RadioButton();
			this.optNO1D = new RadioButton();
			this.optOnline1D = new RadioButton();
			this.tabPage2 = new TabPage();
			this.groupBox15 = new GroupBox();
			this.nudDoorDelay2B = new NumericUpDown();
			this.nudDoorDelay1B = new NumericUpDown();
			this.label39 = new Label();
			this.label34 = new Label();
			this.groupBox16 = new GroupBox();
			this.optDutyOff4B = new RadioButton();
			this.optDutyOn4B = new RadioButton();
			this.optDutyOnOff4B = new RadioButton();
			this.chkAttend4B = new CheckBox();
			this.txtReaderName4B = new TextBox();
			this.label22 = new Label();
			this.groupBox17 = new GroupBox();
			this.optDutyOff3B = new RadioButton();
			this.optDutyOn3B = new RadioButton();
			this.optDutyOnOff3B = new RadioButton();
			this.chkAttend3B = new CheckBox();
			this.txtReaderName3B = new TextBox();
			this.label23 = new Label();
			this.groupBox18 = new GroupBox();
			this.optDutyOff2B = new RadioButton();
			this.optDutyOn2B = new RadioButton();
			this.optDutyOnOff2B = new RadioButton();
			this.chkAttend2B = new CheckBox();
			this.txtReaderName2B = new TextBox();
			this.label24 = new Label();
			this.chkDoorActive2B = new CheckBox();
			this.txtDoorName2B = new TextBox();
			this.label27 = new Label();
			this.groupBox21 = new GroupBox();
			this.optNC2B = new RadioButton();
			this.optNO2B = new RadioButton();
			this.optOnline2B = new RadioButton();
			this.gpbAttend1B = new GroupBox();
			this.optDutyOff1B = new RadioButton();
			this.optDutyOn1B = new RadioButton();
			this.optDutyOnOff1B = new RadioButton();
			this.chkAttend1B = new CheckBox();
			this.txtReaderName1B = new TextBox();
			this.label28 = new Label();
			this.label29 = new Label();
			this.label30 = new Label();
			this.chkDoorActive1B = new CheckBox();
			this.txtDoorName1B = new TextBox();
			this.label31 = new Label();
			this.label32 = new Label();
			this.groupBox23 = new GroupBox();
			this.optNC1B = new RadioButton();
			this.optNO1B = new RadioButton();
			this.optOnline1B = new RadioButton();
			this.tabPage3 = new TabPage();
			this.groupBox19 = new GroupBox();
			this.nudDoorDelay1A = new NumericUpDown();
			this.label33 = new Label();
			this.label35 = new Label();
			this.groupBox14 = new GroupBox();
			this.optDutyOff2A = new RadioButton();
			this.optDutyOn2A = new RadioButton();
			this.optDutyOnOff2A = new RadioButton();
			this.chkAttend2A = new CheckBox();
			this.txtReaderName2A = new TextBox();
			this.label36 = new Label();
			this.groupBox20 = new GroupBox();
			this.optDutyOff1A = new RadioButton();
			this.optDutyOn1A = new RadioButton();
			this.optDutyOnOff1A = new RadioButton();
			this.chkAttend1A = new CheckBox();
			this.txtReaderName1A = new TextBox();
			this.label37 = new Label();
			this.label38 = new Label();
			this.label42 = new Label();
			this.chkDoorActive1A = new CheckBox();
			this.txtDoorName1A = new TextBox();
			this.label43 = new Label();
			this.label44 = new Label();
			this.groupBox22 = new GroupBox();
			this.optNC1A = new RadioButton();
			this.optNO1A = new RadioButton();
			this.optOnline1A = new RadioButton();
			this.btnOK = new Button();
			this.grpbController = new GroupBox();
			this.label8 = new Label();
			this.btnZoneManage = new Button();
			this.cbof_Zone = new ComboBox();
			this.label25 = new Label();
			this.grpbIP = new GroupBox();
			this.nudPort = new NumericUpDown();
			this.txtControllerIP = new TextBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.chkControllerActive = new CheckBox();
			this.label26 = new Label();
			this.txtNote = new TextBox();
			this.mtxtbControllerNO = new MaskedTextBox();
			this.mtxtbControllerSN = new MaskedTextBox();
			this.optIPLarge = new RadioButton();
			this.optIPSmall = new RadioButton();
			this.label2 = new Label();
			this.label1 = new Label();
			this.btnCancel2 = new Button();
			this.grpbDoorReader.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.nudDoorDelay4D).BeginInit();
			((ISupportInitialize)this.nudDoorDelay3D).BeginInit();
			((ISupportInitialize)this.nudDoorDelay2D).BeginInit();
			((ISupportInitialize)this.nudDoorDelay1D).BeginInit();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox12.SuspendLayout();
			this.groupBox13.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox15.SuspendLayout();
			((ISupportInitialize)this.nudDoorDelay2B).BeginInit();
			((ISupportInitialize)this.nudDoorDelay1B).BeginInit();
			this.groupBox16.SuspendLayout();
			this.groupBox17.SuspendLayout();
			this.groupBox18.SuspendLayout();
			this.groupBox21.SuspendLayout();
			this.gpbAttend1B.SuspendLayout();
			this.groupBox23.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox19.SuspendLayout();
			((ISupportInitialize)this.nudDoorDelay1A).BeginInit();
			this.groupBox14.SuspendLayout();
			this.groupBox20.SuspendLayout();
			this.groupBox22.SuspendLayout();
			this.grpbController.SuspendLayout();
			this.grpbIP.SuspendLayout();
			((ISupportInitialize)this.nudPort).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnNext, "btnNext");
			this.btnNext.BackColor = Color.Transparent;
			this.btnNext.BackgroundImage = Resources.pMain_button_normal;
			this.btnNext.ForeColor = Color.White;
			this.btnNext.Name = "btnNext";
			this.btnNext.UseVisualStyleBackColor = false;
			this.btnNext.Click += new EventHandler(this.btnNext_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.grpbDoorReader, "grpbDoorReader");
			this.grpbDoorReader.BackColor = Color.Transparent;
			this.grpbDoorReader.Controls.Add(this.btnCancel);
			this.grpbDoorReader.Controls.Add(this.tabControl1);
			this.grpbDoorReader.Controls.Add(this.btnOK);
			this.grpbDoorReader.Name = "grpbDoorReader";
			this.grpbDoorReader.TabStop = false;
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.BackColor = Color.Transparent;
			this.tabPage1.BackgroundImage = Resources.pMain_content_bkg;
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.ForeColor = Color.White;
			this.tabPage1.Name = "tabPage1";
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this.nudDoorDelay4D);
			this.groupBox1.Controls.Add(this.nudDoorDelay3D);
			this.groupBox1.Controls.Add(this.nudDoorDelay2D);
			this.groupBox1.Controls.Add(this.nudDoorDelay1D);
			this.groupBox1.Controls.Add(this.chkDoorActive4D);
			this.groupBox1.Controls.Add(this.txtDoorName4D);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.groupBox6);
			this.groupBox1.Controls.Add(this.chkDoorActive3D);
			this.groupBox1.Controls.Add(this.txtDoorName3D);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.groupBox7);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.groupBox8);
			this.groupBox1.Controls.Add(this.chkAttend4D);
			this.groupBox1.Controls.Add(this.txtReaderName4D);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.groupBox9);
			this.groupBox1.Controls.Add(this.chkAttend3D);
			this.groupBox1.Controls.Add(this.txtReaderName3D);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.groupBox10);
			this.groupBox1.Controls.Add(this.chkAttend2D);
			this.groupBox1.Controls.Add(this.txtReaderName2D);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.chkDoorActive2D);
			this.groupBox1.Controls.Add(this.txtDoorName2D);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.groupBox11);
			this.groupBox1.Controls.Add(this.groupBox12);
			this.groupBox1.Controls.Add(this.chkAttend1D);
			this.groupBox1.Controls.Add(this.txtReaderName1D);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.label20);
			this.groupBox1.Controls.Add(this.label21);
			this.groupBox1.Controls.Add(this.chkDoorActive1D);
			this.groupBox1.Controls.Add(this.txtDoorName1D);
			this.groupBox1.Controls.Add(this.label40);
			this.groupBox1.Controls.Add(this.label41);
			this.groupBox1.Controls.Add(this.groupBox13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.nudDoorDelay4D, "nudDoorDelay4D");
			NumericUpDown arg_F23_0 = this.nudDoorDelay4D;
			int[] array = new int[4];
			array[0] = 6000;
			arg_F23_0.Maximum = new decimal(array);
			this.nudDoorDelay4D.Name = "nudDoorDelay4D";
			NumericUpDown arg_F4F_0 = this.nudDoorDelay4D;
			int[] array2 = new int[4];
			array2[0] = 3;
			arg_F4F_0.Value = new decimal(array2);
			componentResourceManager.ApplyResources(this.nudDoorDelay3D, "nudDoorDelay3D");
			NumericUpDown arg_F80_0 = this.nudDoorDelay3D;
			int[] array3 = new int[4];
			array3[0] = 6000;
			arg_F80_0.Maximum = new decimal(array3);
			this.nudDoorDelay3D.Name = "nudDoorDelay3D";
			NumericUpDown arg_FAF_0 = this.nudDoorDelay3D;
			int[] array4 = new int[4];
			array4[0] = 3;
			arg_FAF_0.Value = new decimal(array4);
			componentResourceManager.ApplyResources(this.nudDoorDelay2D, "nudDoorDelay2D");
			NumericUpDown arg_FE3_0 = this.nudDoorDelay2D;
			int[] array5 = new int[4];
			array5[0] = 6000;
			arg_FE3_0.Maximum = new decimal(array5);
			this.nudDoorDelay2D.Name = "nudDoorDelay2D";
			NumericUpDown arg_1012_0 = this.nudDoorDelay2D;
			int[] array6 = new int[4];
			array6[0] = 3;
			arg_1012_0.Value = new decimal(array6);
			componentResourceManager.ApplyResources(this.nudDoorDelay1D, "nudDoorDelay1D");
			NumericUpDown arg_1046_0 = this.nudDoorDelay1D;
			int[] array7 = new int[4];
			array7[0] = 6000;
			arg_1046_0.Maximum = new decimal(array7);
			this.nudDoorDelay1D.Name = "nudDoorDelay1D";
			NumericUpDown arg_1075_0 = this.nudDoorDelay1D;
			int[] array8 = new int[4];
			array8[0] = 3;
			arg_1075_0.Value = new decimal(array8);
			componentResourceManager.ApplyResources(this.chkDoorActive4D, "chkDoorActive4D");
			this.chkDoorActive4D.Checked = true;
			this.chkDoorActive4D.CheckState = CheckState.Checked;
			this.chkDoorActive4D.Name = "chkDoorActive4D";
			this.chkDoorActive4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName4D, "txtDoorName4D");
			this.txtDoorName4D.Name = "txtDoorName4D";
			componentResourceManager.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			componentResourceManager.ApplyResources(this.groupBox6, "groupBox6");
			this.groupBox6.Controls.Add(this.optNC4D);
			this.groupBox6.Controls.Add(this.optNO4D);
			this.groupBox6.Controls.Add(this.optOnline4D);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC4D, "optNC4D");
			this.optNC4D.Name = "optNC4D";
			this.optNC4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO4D, "optNO4D");
			this.optNO4D.Name = "optNO4D";
			this.optNO4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline4D, "optOnline4D");
			this.optOnline4D.Checked = true;
			this.optOnline4D.Name = "optOnline4D";
			this.optOnline4D.TabStop = true;
			this.optOnline4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkDoorActive3D, "chkDoorActive3D");
			this.chkDoorActive3D.Checked = true;
			this.chkDoorActive3D.CheckState = CheckState.Checked;
			this.chkDoorActive3D.Name = "chkDoorActive3D";
			this.chkDoorActive3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName3D, "txtDoorName3D");
			this.txtDoorName3D.Name = "txtDoorName3D";
			componentResourceManager.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			componentResourceManager.ApplyResources(this.groupBox7, "groupBox7");
			this.groupBox7.Controls.Add(this.optNC3D);
			this.groupBox7.Controls.Add(this.optNO3D);
			this.groupBox7.Controls.Add(this.optOnline3D);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC3D, "optNC3D");
			this.optNC3D.Name = "optNC3D";
			this.optNC3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO3D, "optNO3D");
			this.optNO3D.Name = "optNO3D";
			this.optNO3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline3D, "optOnline3D");
			this.optOnline3D.Checked = true;
			this.optOnline3D.Name = "optOnline3D";
			this.optOnline3D.TabStop = true;
			this.optOnline3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			componentResourceManager.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			componentResourceManager.ApplyResources(this.groupBox8, "groupBox8");
			this.groupBox8.Controls.Add(this.optDutyOff4D);
			this.groupBox8.Controls.Add(this.optDutyOn4D);
			this.groupBox8.Controls.Add(this.optDutyOnOff4D);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff4D, "optDutyOff4D");
			this.optDutyOff4D.Name = "optDutyOff4D";
			this.optDutyOff4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn4D, "optDutyOn4D");
			this.optDutyOn4D.Name = "optDutyOn4D";
			this.optDutyOn4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff4D, "optDutyOnOff4D");
			this.optDutyOnOff4D.Checked = true;
			this.optDutyOnOff4D.Name = "optDutyOnOff4D";
			this.optDutyOnOff4D.TabStop = true;
			this.optDutyOnOff4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend4D, "chkAttend4D");
			this.chkAttend4D.Checked = true;
			this.chkAttend4D.CheckState = CheckState.Checked;
			this.chkAttend4D.Name = "chkAttend4D";
			this.chkAttend4D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName4D, "txtReaderName4D");
			this.txtReaderName4D.Name = "txtReaderName4D";
			componentResourceManager.ApplyResources(this.label15, "label15");
			this.label15.Name = "label15";
			componentResourceManager.ApplyResources(this.groupBox9, "groupBox9");
			this.groupBox9.Controls.Add(this.optDutyOff3D);
			this.groupBox9.Controls.Add(this.optDutyOn3D);
			this.groupBox9.Controls.Add(this.optDutyOnOff3D);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff3D, "optDutyOff3D");
			this.optDutyOff3D.Name = "optDutyOff3D";
			this.optDutyOff3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn3D, "optDutyOn3D");
			this.optDutyOn3D.Name = "optDutyOn3D";
			this.optDutyOn3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff3D, "optDutyOnOff3D");
			this.optDutyOnOff3D.Checked = true;
			this.optDutyOnOff3D.Name = "optDutyOnOff3D";
			this.optDutyOnOff3D.TabStop = true;
			this.optDutyOnOff3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend3D, "chkAttend3D");
			this.chkAttend3D.Checked = true;
			this.chkAttend3D.CheckState = CheckState.Checked;
			this.chkAttend3D.Name = "chkAttend3D";
			this.chkAttend3D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName3D, "txtReaderName3D");
			this.txtReaderName3D.Name = "txtReaderName3D";
			componentResourceManager.ApplyResources(this.label16, "label16");
			this.label16.Name = "label16";
			componentResourceManager.ApplyResources(this.groupBox10, "groupBox10");
			this.groupBox10.Controls.Add(this.optDutyOff2D);
			this.groupBox10.Controls.Add(this.optDutyOn2D);
			this.groupBox10.Controls.Add(this.optDutyOnOff2D);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff2D, "optDutyOff2D");
			this.optDutyOff2D.Name = "optDutyOff2D";
			this.optDutyOff2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn2D, "optDutyOn2D");
			this.optDutyOn2D.Name = "optDutyOn2D";
			this.optDutyOn2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff2D, "optDutyOnOff2D");
			this.optDutyOnOff2D.Checked = true;
			this.optDutyOnOff2D.Name = "optDutyOnOff2D";
			this.optDutyOnOff2D.TabStop = true;
			this.optDutyOnOff2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend2D, "chkAttend2D");
			this.chkAttend2D.Checked = true;
			this.chkAttend2D.CheckState = CheckState.Checked;
			this.chkAttend2D.Name = "chkAttend2D";
			this.chkAttend2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName2D, "txtReaderName2D");
			this.txtReaderName2D.Name = "txtReaderName2D";
			componentResourceManager.ApplyResources(this.label17, "label17");
			this.label17.Name = "label17";
			componentResourceManager.ApplyResources(this.chkDoorActive2D, "chkDoorActive2D");
			this.chkDoorActive2D.Checked = true;
			this.chkDoorActive2D.CheckState = CheckState.Checked;
			this.chkDoorActive2D.Name = "chkDoorActive2D";
			this.chkDoorActive2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName2D, "txtDoorName2D");
			this.txtDoorName2D.Name = "txtDoorName2D";
			componentResourceManager.ApplyResources(this.label18, "label18");
			this.label18.Name = "label18";
			componentResourceManager.ApplyResources(this.groupBox11, "groupBox11");
			this.groupBox11.Controls.Add(this.optNC2D);
			this.groupBox11.Controls.Add(this.optNO2D);
			this.groupBox11.Controls.Add(this.optOnline2D);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC2D, "optNC2D");
			this.optNC2D.Name = "optNC2D";
			this.optNC2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO2D, "optNO2D");
			this.optNO2D.Name = "optNO2D";
			this.optNO2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline2D, "optOnline2D");
			this.optOnline2D.Checked = true;
			this.optOnline2D.Name = "optOnline2D";
			this.optOnline2D.TabStop = true;
			this.optOnline2D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.groupBox12, "groupBox12");
			this.groupBox12.Controls.Add(this.optDutyOff1D);
			this.groupBox12.Controls.Add(this.optDutyOn1D);
			this.groupBox12.Controls.Add(this.optDutyOnOff1D);
			this.groupBox12.Name = "groupBox12";
			this.groupBox12.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff1D, "optDutyOff1D");
			this.optDutyOff1D.Name = "optDutyOff1D";
			this.optDutyOff1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn1D, "optDutyOn1D");
			this.optDutyOn1D.Name = "optDutyOn1D";
			this.optDutyOn1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff1D, "optDutyOnOff1D");
			this.optDutyOnOff1D.Checked = true;
			this.optDutyOnOff1D.Name = "optDutyOnOff1D";
			this.optDutyOnOff1D.TabStop = true;
			this.optDutyOnOff1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend1D, "chkAttend1D");
			this.chkAttend1D.Checked = true;
			this.chkAttend1D.CheckState = CheckState.Checked;
			this.chkAttend1D.Name = "chkAttend1D";
			this.chkAttend1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName1D, "txtReaderName1D");
			this.txtReaderName1D.Name = "txtReaderName1D";
			componentResourceManager.ApplyResources(this.label19, "label19");
			this.label19.Name = "label19";
			componentResourceManager.ApplyResources(this.label20, "label20");
			this.label20.Name = "label20";
			componentResourceManager.ApplyResources(this.label21, "label21");
			this.label21.Name = "label21";
			componentResourceManager.ApplyResources(this.chkDoorActive1D, "chkDoorActive1D");
			this.chkDoorActive1D.Checked = true;
			this.chkDoorActive1D.CheckState = CheckState.Checked;
			this.chkDoorActive1D.Name = "chkDoorActive1D";
			this.chkDoorActive1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName1D, "txtDoorName1D");
			this.txtDoorName1D.Name = "txtDoorName1D";
			componentResourceManager.ApplyResources(this.label40, "label40");
			this.label40.Name = "label40";
			componentResourceManager.ApplyResources(this.label41, "label41");
			this.label41.Name = "label41";
			componentResourceManager.ApplyResources(this.groupBox13, "groupBox13");
			this.groupBox13.Controls.Add(this.optNC1D);
			this.groupBox13.Controls.Add(this.optNO1D);
			this.groupBox13.Controls.Add(this.optOnline1D);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC1D, "optNC1D");
			this.optNC1D.Name = "optNC1D";
			this.optNC1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO1D, "optNO1D");
			this.optNO1D.Name = "optNO1D";
			this.optNO1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline1D, "optOnline1D");
			this.optOnline1D.Checked = true;
			this.optOnline1D.Name = "optOnline1D";
			this.optOnline1D.TabStop = true;
			this.optOnline1D.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.BackgroundImage = Resources.pMain_content_bkg;
			this.tabPage2.Controls.Add(this.groupBox15);
			this.tabPage2.ForeColor = Color.White;
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.groupBox15, "groupBox15");
			this.groupBox15.BackColor = Color.Transparent;
			this.groupBox15.Controls.Add(this.nudDoorDelay2B);
			this.groupBox15.Controls.Add(this.nudDoorDelay1B);
			this.groupBox15.Controls.Add(this.label39);
			this.groupBox15.Controls.Add(this.label34);
			this.groupBox15.Controls.Add(this.groupBox16);
			this.groupBox15.Controls.Add(this.chkAttend4B);
			this.groupBox15.Controls.Add(this.txtReaderName4B);
			this.groupBox15.Controls.Add(this.label22);
			this.groupBox15.Controls.Add(this.groupBox17);
			this.groupBox15.Controls.Add(this.chkAttend3B);
			this.groupBox15.Controls.Add(this.txtReaderName3B);
			this.groupBox15.Controls.Add(this.label23);
			this.groupBox15.Controls.Add(this.groupBox18);
			this.groupBox15.Controls.Add(this.chkAttend2B);
			this.groupBox15.Controls.Add(this.txtReaderName2B);
			this.groupBox15.Controls.Add(this.label24);
			this.groupBox15.Controls.Add(this.chkDoorActive2B);
			this.groupBox15.Controls.Add(this.txtDoorName2B);
			this.groupBox15.Controls.Add(this.label27);
			this.groupBox15.Controls.Add(this.groupBox21);
			this.groupBox15.Controls.Add(this.gpbAttend1B);
			this.groupBox15.Controls.Add(this.chkAttend1B);
			this.groupBox15.Controls.Add(this.txtReaderName1B);
			this.groupBox15.Controls.Add(this.label28);
			this.groupBox15.Controls.Add(this.label29);
			this.groupBox15.Controls.Add(this.label30);
			this.groupBox15.Controls.Add(this.chkDoorActive1B);
			this.groupBox15.Controls.Add(this.txtDoorName1B);
			this.groupBox15.Controls.Add(this.label31);
			this.groupBox15.Controls.Add(this.label32);
			this.groupBox15.Controls.Add(this.groupBox23);
			this.groupBox15.Name = "groupBox15";
			this.groupBox15.TabStop = false;
			componentResourceManager.ApplyResources(this.nudDoorDelay2B, "nudDoorDelay2B");
			NumericUpDown arg_2140_0 = this.nudDoorDelay2B;
			int[] array9 = new int[4];
			array9[0] = 6000;
			arg_2140_0.Maximum = new decimal(array9);
			this.nudDoorDelay2B.Name = "nudDoorDelay2B";
			NumericUpDown arg_216F_0 = this.nudDoorDelay2B;
			int[] array10 = new int[4];
			array10[0] = 3;
			arg_216F_0.Value = new decimal(array10);
			componentResourceManager.ApplyResources(this.nudDoorDelay1B, "nudDoorDelay1B");
			NumericUpDown arg_21A3_0 = this.nudDoorDelay1B;
			int[] array11 = new int[4];
			array11[0] = 6000;
			arg_21A3_0.Maximum = new decimal(array11);
			this.nudDoorDelay1B.Name = "nudDoorDelay1B";
			NumericUpDown arg_21D2_0 = this.nudDoorDelay1B;
			int[] array12 = new int[4];
			array12[0] = 3;
			arg_21D2_0.Value = new decimal(array12);
			componentResourceManager.ApplyResources(this.label39, "label39");
			this.label39.Name = "label39";
			componentResourceManager.ApplyResources(this.label34, "label34");
			this.label34.Name = "label34";
			componentResourceManager.ApplyResources(this.groupBox16, "groupBox16");
			this.groupBox16.Controls.Add(this.optDutyOff4B);
			this.groupBox16.Controls.Add(this.optDutyOn4B);
			this.groupBox16.Controls.Add(this.optDutyOnOff4B);
			this.groupBox16.Name = "groupBox16";
			this.groupBox16.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff4B, "optDutyOff4B");
			this.optDutyOff4B.Name = "optDutyOff4B";
			this.optDutyOff4B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn4B, "optDutyOn4B");
			this.optDutyOn4B.Name = "optDutyOn4B";
			this.optDutyOn4B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff4B, "optDutyOnOff4B");
			this.optDutyOnOff4B.Checked = true;
			this.optDutyOnOff4B.Name = "optDutyOnOff4B";
			this.optDutyOnOff4B.TabStop = true;
			this.optDutyOnOff4B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend4B, "chkAttend4B");
			this.chkAttend4B.Checked = true;
			this.chkAttend4B.CheckState = CheckState.Checked;
			this.chkAttend4B.Name = "chkAttend4B";
			this.chkAttend4B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName4B, "txtReaderName4B");
			this.txtReaderName4B.Name = "txtReaderName4B";
			componentResourceManager.ApplyResources(this.label22, "label22");
			this.label22.Name = "label22";
			componentResourceManager.ApplyResources(this.groupBox17, "groupBox17");
			this.groupBox17.Controls.Add(this.optDutyOff3B);
			this.groupBox17.Controls.Add(this.optDutyOn3B);
			this.groupBox17.Controls.Add(this.optDutyOnOff3B);
			this.groupBox17.Name = "groupBox17";
			this.groupBox17.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff3B, "optDutyOff3B");
			this.optDutyOff3B.Name = "optDutyOff3B";
			this.optDutyOff3B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn3B, "optDutyOn3B");
			this.optDutyOn3B.Name = "optDutyOn3B";
			this.optDutyOn3B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff3B, "optDutyOnOff3B");
			this.optDutyOnOff3B.Checked = true;
			this.optDutyOnOff3B.Name = "optDutyOnOff3B";
			this.optDutyOnOff3B.TabStop = true;
			this.optDutyOnOff3B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend3B, "chkAttend3B");
			this.chkAttend3B.Checked = true;
			this.chkAttend3B.CheckState = CheckState.Checked;
			this.chkAttend3B.Name = "chkAttend3B";
			this.chkAttend3B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName3B, "txtReaderName3B");
			this.txtReaderName3B.Name = "txtReaderName3B";
			componentResourceManager.ApplyResources(this.label23, "label23");
			this.label23.Name = "label23";
			componentResourceManager.ApplyResources(this.groupBox18, "groupBox18");
			this.groupBox18.Controls.Add(this.optDutyOff2B);
			this.groupBox18.Controls.Add(this.optDutyOn2B);
			this.groupBox18.Controls.Add(this.optDutyOnOff2B);
			this.groupBox18.Name = "groupBox18";
			this.groupBox18.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff2B, "optDutyOff2B");
			this.optDutyOff2B.Name = "optDutyOff2B";
			this.optDutyOff2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn2B, "optDutyOn2B");
			this.optDutyOn2B.Name = "optDutyOn2B";
			this.optDutyOn2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff2B, "optDutyOnOff2B");
			this.optDutyOnOff2B.Checked = true;
			this.optDutyOnOff2B.Name = "optDutyOnOff2B";
			this.optDutyOnOff2B.TabStop = true;
			this.optDutyOnOff2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend2B, "chkAttend2B");
			this.chkAttend2B.Checked = true;
			this.chkAttend2B.CheckState = CheckState.Checked;
			this.chkAttend2B.Name = "chkAttend2B";
			this.chkAttend2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName2B, "txtReaderName2B");
			this.txtReaderName2B.Name = "txtReaderName2B";
			componentResourceManager.ApplyResources(this.label24, "label24");
			this.label24.Name = "label24";
			componentResourceManager.ApplyResources(this.chkDoorActive2B, "chkDoorActive2B");
			this.chkDoorActive2B.Checked = true;
			this.chkDoorActive2B.CheckState = CheckState.Checked;
			this.chkDoorActive2B.Name = "chkDoorActive2B";
			this.chkDoorActive2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName2B, "txtDoorName2B");
			this.txtDoorName2B.Name = "txtDoorName2B";
			componentResourceManager.ApplyResources(this.label27, "label27");
			this.label27.Name = "label27";
			componentResourceManager.ApplyResources(this.groupBox21, "groupBox21");
			this.groupBox21.Controls.Add(this.optNC2B);
			this.groupBox21.Controls.Add(this.optNO2B);
			this.groupBox21.Controls.Add(this.optOnline2B);
			this.groupBox21.Name = "groupBox21";
			this.groupBox21.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC2B, "optNC2B");
			this.optNC2B.Name = "optNC2B";
			this.optNC2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO2B, "optNO2B");
			this.optNO2B.Name = "optNO2B";
			this.optNO2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline2B, "optOnline2B");
			this.optOnline2B.Checked = true;
			this.optOnline2B.Name = "optOnline2B";
			this.optOnline2B.TabStop = true;
			this.optOnline2B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.gpbAttend1B, "gpbAttend1B");
			this.gpbAttend1B.Controls.Add(this.optDutyOff1B);
			this.gpbAttend1B.Controls.Add(this.optDutyOn1B);
			this.gpbAttend1B.Controls.Add(this.optDutyOnOff1B);
			this.gpbAttend1B.Name = "gpbAttend1B";
			this.gpbAttend1B.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff1B, "optDutyOff1B");
			this.optDutyOff1B.Name = "optDutyOff1B";
			this.optDutyOff1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn1B, "optDutyOn1B");
			this.optDutyOn1B.Name = "optDutyOn1B";
			this.optDutyOn1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff1B, "optDutyOnOff1B");
			this.optDutyOnOff1B.Checked = true;
			this.optDutyOnOff1B.Name = "optDutyOnOff1B";
			this.optDutyOnOff1B.TabStop = true;
			this.optDutyOnOff1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend1B, "chkAttend1B");
			this.chkAttend1B.Checked = true;
			this.chkAttend1B.CheckState = CheckState.Checked;
			this.chkAttend1B.Name = "chkAttend1B";
			this.chkAttend1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName1B, "txtReaderName1B");
			this.txtReaderName1B.Name = "txtReaderName1B";
			componentResourceManager.ApplyResources(this.label28, "label28");
			this.label28.Name = "label28";
			componentResourceManager.ApplyResources(this.label29, "label29");
			this.label29.Name = "label29";
			componentResourceManager.ApplyResources(this.label30, "label30");
			this.label30.Name = "label30";
			componentResourceManager.ApplyResources(this.chkDoorActive1B, "chkDoorActive1B");
			this.chkDoorActive1B.Checked = true;
			this.chkDoorActive1B.CheckState = CheckState.Checked;
			this.chkDoorActive1B.Name = "chkDoorActive1B";
			this.chkDoorActive1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName1B, "txtDoorName1B");
			this.txtDoorName1B.Name = "txtDoorName1B";
			componentResourceManager.ApplyResources(this.label31, "label31");
			this.label31.Name = "label31";
			componentResourceManager.ApplyResources(this.label32, "label32");
			this.label32.Name = "label32";
			componentResourceManager.ApplyResources(this.groupBox23, "groupBox23");
			this.groupBox23.Controls.Add(this.optNC1B);
			this.groupBox23.Controls.Add(this.optNO1B);
			this.groupBox23.Controls.Add(this.optOnline1B);
			this.groupBox23.Name = "groupBox23";
			this.groupBox23.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC1B, "optNC1B");
			this.optNC1B.Name = "optNC1B";
			this.optNC1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO1B, "optNO1B");
			this.optNO1B.Name = "optNO1B";
			this.optNO1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline1B, "optOnline1B");
			this.optOnline1B.Checked = true;
			this.optOnline1B.Name = "optOnline1B";
			this.optOnline1B.TabStop = true;
			this.optOnline1B.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.BackColor = Color.Transparent;
			this.tabPage3.BackgroundImage = Resources.pMain_content_bkg;
			this.tabPage3.Controls.Add(this.groupBox19);
			this.tabPage3.ForeColor = Color.White;
			this.tabPage3.Name = "tabPage3";
			componentResourceManager.ApplyResources(this.groupBox19, "groupBox19");
			this.groupBox19.Controls.Add(this.nudDoorDelay1A);
			this.groupBox19.Controls.Add(this.label33);
			this.groupBox19.Controls.Add(this.label35);
			this.groupBox19.Controls.Add(this.groupBox14);
			this.groupBox19.Controls.Add(this.chkAttend2A);
			this.groupBox19.Controls.Add(this.txtReaderName2A);
			this.groupBox19.Controls.Add(this.label36);
			this.groupBox19.Controls.Add(this.groupBox20);
			this.groupBox19.Controls.Add(this.chkAttend1A);
			this.groupBox19.Controls.Add(this.txtReaderName1A);
			this.groupBox19.Controls.Add(this.label37);
			this.groupBox19.Controls.Add(this.label38);
			this.groupBox19.Controls.Add(this.label42);
			this.groupBox19.Controls.Add(this.chkDoorActive1A);
			this.groupBox19.Controls.Add(this.txtDoorName1A);
			this.groupBox19.Controls.Add(this.label43);
			this.groupBox19.Controls.Add(this.label44);
			this.groupBox19.Controls.Add(this.groupBox22);
			this.groupBox19.Name = "groupBox19";
			this.groupBox19.TabStop = false;
			componentResourceManager.ApplyResources(this.nudDoorDelay1A, "nudDoorDelay1A");
			NumericUpDown arg_2E49_0 = this.nudDoorDelay1A;
			int[] array13 = new int[4];
			array13[0] = 6000;
			arg_2E49_0.Maximum = new decimal(array13);
			this.nudDoorDelay1A.Name = "nudDoorDelay1A";
			NumericUpDown arg_2E78_0 = this.nudDoorDelay1A;
			int[] array14 = new int[4];
			array14[0] = 3;
			arg_2E78_0.Value = new decimal(array14);
			componentResourceManager.ApplyResources(this.label33, "label33");
			this.label33.Name = "label33";
			componentResourceManager.ApplyResources(this.label35, "label35");
			this.label35.Name = "label35";
			componentResourceManager.ApplyResources(this.groupBox14, "groupBox14");
			this.groupBox14.Controls.Add(this.optDutyOff2A);
			this.groupBox14.Controls.Add(this.optDutyOn2A);
			this.groupBox14.Controls.Add(this.optDutyOnOff2A);
			this.groupBox14.Name = "groupBox14";
			this.groupBox14.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff2A, "optDutyOff2A");
			this.optDutyOff2A.Name = "optDutyOff2A";
			this.optDutyOff2A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn2A, "optDutyOn2A");
			this.optDutyOn2A.Name = "optDutyOn2A";
			this.optDutyOn2A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff2A, "optDutyOnOff2A");
			this.optDutyOnOff2A.Checked = true;
			this.optDutyOnOff2A.Name = "optDutyOnOff2A";
			this.optDutyOnOff2A.TabStop = true;
			this.optDutyOnOff2A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend2A, "chkAttend2A");
			this.chkAttend2A.Checked = true;
			this.chkAttend2A.CheckState = CheckState.Checked;
			this.chkAttend2A.Name = "chkAttend2A";
			this.chkAttend2A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName2A, "txtReaderName2A");
			this.txtReaderName2A.Name = "txtReaderName2A";
			componentResourceManager.ApplyResources(this.label36, "label36");
			this.label36.Name = "label36";
			componentResourceManager.ApplyResources(this.groupBox20, "groupBox20");
			this.groupBox20.Controls.Add(this.optDutyOff1A);
			this.groupBox20.Controls.Add(this.optDutyOn1A);
			this.groupBox20.Controls.Add(this.optDutyOnOff1A);
			this.groupBox20.Name = "groupBox20";
			this.groupBox20.TabStop = false;
			componentResourceManager.ApplyResources(this.optDutyOff1A, "optDutyOff1A");
			this.optDutyOff1A.Name = "optDutyOff1A";
			this.optDutyOff1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOn1A, "optDutyOn1A");
			this.optDutyOn1A.Name = "optDutyOn1A";
			this.optDutyOn1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optDutyOnOff1A, "optDutyOnOff1A");
			this.optDutyOnOff1A.Checked = true;
			this.optDutyOnOff1A.Name = "optDutyOnOff1A";
			this.optDutyOnOff1A.TabStop = true;
			this.optDutyOnOff1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAttend1A, "chkAttend1A");
			this.chkAttend1A.Checked = true;
			this.chkAttend1A.CheckState = CheckState.Checked;
			this.chkAttend1A.Name = "chkAttend1A";
			this.chkAttend1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtReaderName1A, "txtReaderName1A");
			this.txtReaderName1A.Name = "txtReaderName1A";
			componentResourceManager.ApplyResources(this.label37, "label37");
			this.label37.Name = "label37";
			componentResourceManager.ApplyResources(this.label38, "label38");
			this.label38.Name = "label38";
			componentResourceManager.ApplyResources(this.label42, "label42");
			this.label42.Name = "label42";
			componentResourceManager.ApplyResources(this.chkDoorActive1A, "chkDoorActive1A");
			this.chkDoorActive1A.Checked = true;
			this.chkDoorActive1A.CheckState = CheckState.Checked;
			this.chkDoorActive1A.Name = "chkDoorActive1A";
			this.chkDoorActive1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.txtDoorName1A, "txtDoorName1A");
			this.txtDoorName1A.Name = "txtDoorName1A";
			componentResourceManager.ApplyResources(this.label43, "label43");
			this.label43.Name = "label43";
			componentResourceManager.ApplyResources(this.label44, "label44");
			this.label44.Name = "label44";
			componentResourceManager.ApplyResources(this.groupBox22, "groupBox22");
			this.groupBox22.Controls.Add(this.optNC1A);
			this.groupBox22.Controls.Add(this.optNO1A);
			this.groupBox22.Controls.Add(this.optOnline1A);
			this.groupBox22.Name = "groupBox22";
			this.groupBox22.TabStop = false;
			componentResourceManager.ApplyResources(this.optNC1A, "optNC1A");
			this.optNC1A.Name = "optNC1A";
			this.optNC1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optNO1A, "optNO1A");
			this.optNO1A.Name = "optNO1A";
			this.optNO1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optOnline1A, "optOnline1A");
			this.optOnline1A.Checked = true;
			this.optOnline1A.Name = "optOnline1A";
			this.optOnline1A.TabStop = true;
			this.optOnline1A.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.grpbController, "grpbController");
			this.grpbController.BackColor = Color.Transparent;
			this.grpbController.Controls.Add(this.label8);
			this.grpbController.Controls.Add(this.btnZoneManage);
			this.grpbController.Controls.Add(this.cbof_Zone);
			this.grpbController.Controls.Add(this.label25);
			this.grpbController.Controls.Add(this.grpbIP);
			this.grpbController.Controls.Add(this.chkControllerActive);
			this.grpbController.Controls.Add(this.label26);
			this.grpbController.Controls.Add(this.txtNote);
			this.grpbController.Controls.Add(this.mtxtbControllerNO);
			this.grpbController.Controls.Add(this.mtxtbControllerSN);
			this.grpbController.Controls.Add(this.optIPLarge);
			this.grpbController.Controls.Add(this.optIPSmall);
			this.grpbController.Controls.Add(this.label2);
			this.grpbController.Controls.Add(this.label1);
			this.grpbController.ForeColor = Color.White;
			this.grpbController.Name = "grpbController";
			this.grpbController.TabStop = false;
			componentResourceManager.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			componentResourceManager.ApplyResources(this.btnZoneManage, "btnZoneManage");
			this.btnZoneManage.BackColor = Color.Transparent;
			this.btnZoneManage.BackgroundImage = Resources.pMain_button_normal;
			this.btnZoneManage.ForeColor = Color.White;
			this.btnZoneManage.Name = "btnZoneManage";
			this.btnZoneManage.UseVisualStyleBackColor = false;
			this.btnZoneManage.Click += new EventHandler(this.btnZoneManage_Click);
			componentResourceManager.ApplyResources(this.cbof_Zone, "cbof_Zone");
			this.cbof_Zone.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_Zone.FormattingEnabled = true;
			this.cbof_Zone.Name = "cbof_Zone";
			componentResourceManager.ApplyResources(this.label25, "label25");
			this.label25.Name = "label25";
			componentResourceManager.ApplyResources(this.grpbIP, "grpbIP");
			this.grpbIP.Controls.Add(this.nudPort);
			this.grpbIP.Controls.Add(this.txtControllerIP);
			this.grpbIP.Controls.Add(this.label4);
			this.grpbIP.Controls.Add(this.label3);
			this.grpbIP.Name = "grpbIP";
			this.grpbIP.TabStop = false;
			componentResourceManager.ApplyResources(this.nudPort, "nudPort");
			NumericUpDown arg_3779_0 = this.nudPort;
			int[] array15 = new int[4];
			array15[0] = 65534;
			arg_3779_0.Maximum = new decimal(array15);
			NumericUpDown arg_379C_0 = this.nudPort;
			int[] array16 = new int[4];
			array16[0] = 1024;
			arg_379C_0.Minimum = new decimal(array16);
			this.nudPort.Name = "nudPort";
			NumericUpDown arg_37CF_0 = this.nudPort;
			int[] array17 = new int[4];
			array17[0] = 60000;
			arg_37CF_0.Value = new decimal(array17);
			componentResourceManager.ApplyResources(this.txtControllerIP, "txtControllerIP");
			this.txtControllerIP.Name = "txtControllerIP";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.chkControllerActive, "chkControllerActive");
			this.chkControllerActive.Checked = true;
			this.chkControllerActive.CheckState = CheckState.Checked;
			this.chkControllerActive.Name = "chkControllerActive";
			this.chkControllerActive.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label26, "label26");
			this.label26.Name = "label26";
			componentResourceManager.ApplyResources(this.txtNote, "txtNote");
			this.txtNote.Name = "txtNote";
			componentResourceManager.ApplyResources(this.mtxtbControllerNO, "mtxtbControllerNO");
			this.mtxtbControllerNO.Name = "mtxtbControllerNO";
			this.mtxtbControllerNO.ValidatingType = typeof(int);
			this.mtxtbControllerNO.KeyPress += new KeyPressEventHandler(this.mtxtbControllerNO_KeyPress);
			this.mtxtbControllerNO.KeyUp += new KeyEventHandler(this.mtxtbControllerNO_KeyUp);
			componentResourceManager.ApplyResources(this.mtxtbControllerSN, "mtxtbControllerSN");
			this.mtxtbControllerSN.Name = "mtxtbControllerSN";
			this.mtxtbControllerSN.RejectInputOnFirstFailure = true;
			this.mtxtbControllerSN.ResetOnSpace = false;
			this.mtxtbControllerSN.KeyPress += new KeyPressEventHandler(this.mtxtbControllerSN_KeyPress);
			this.mtxtbControllerSN.KeyUp += new KeyEventHandler(this.mtxtbControllerSN_KeyUp);
			componentResourceManager.ApplyResources(this.optIPLarge, "optIPLarge");
			this.optIPLarge.Name = "optIPLarge";
			this.optIPLarge.UseVisualStyleBackColor = true;
			this.optIPLarge.CheckedChanged += new EventHandler(this.optIPLarge_CheckedChanged);
			componentResourceManager.ApplyResources(this.optIPSmall, "optIPSmall");
			this.optIPSmall.Checked = true;
			this.optIPSmall.Name = "optIPSmall";
			this.optIPSmall.TabStop = true;
			this.optIPSmall.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.btnCancel2, "btnCancel2");
			this.btnCancel2.BackColor = Color.Transparent;
			this.btnCancel2.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel2.DialogResult = DialogResult.Cancel;
			this.btnCancel2.ForeColor = Color.White;
			this.btnCancel2.Name = "btnCancel2";
			this.btnCancel2.UseVisualStyleBackColor = false;
			this.btnCancel2.Click += new EventHandler(this.btnCancel2_Click);
			base.AcceptButton = this.btnNext;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.grpbDoorReader);
			base.Controls.Add(this.btnNext);
			base.Controls.Add(this.grpbController);
			base.Controls.Add(this.btnCancel2);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmController";
			base.Load += new EventHandler(this.dfrmController_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmController_KeyDown);
			base.KeyPress += new KeyPressEventHandler(this.dfrmController_KeyPress);
			this.grpbDoorReader.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.nudDoorDelay4D).EndInit();
			((ISupportInitialize)this.nudDoorDelay3D).EndInit();
			((ISupportInitialize)this.nudDoorDelay2D).EndInit();
			((ISupportInitialize)this.nudDoorDelay1D).EndInit();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.groupBox11.ResumeLayout(false);
			this.groupBox11.PerformLayout();
			this.groupBox12.ResumeLayout(false);
			this.groupBox12.PerformLayout();
			this.groupBox13.ResumeLayout(false);
			this.groupBox13.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.groupBox15.ResumeLayout(false);
			this.groupBox15.PerformLayout();
			((ISupportInitialize)this.nudDoorDelay2B).EndInit();
			((ISupportInitialize)this.nudDoorDelay1B).EndInit();
			this.groupBox16.ResumeLayout(false);
			this.groupBox16.PerformLayout();
			this.groupBox17.ResumeLayout(false);
			this.groupBox17.PerformLayout();
			this.groupBox18.ResumeLayout(false);
			this.groupBox18.PerformLayout();
			this.groupBox21.ResumeLayout(false);
			this.groupBox21.PerformLayout();
			this.gpbAttend1B.ResumeLayout(false);
			this.gpbAttend1B.PerformLayout();
			this.groupBox23.ResumeLayout(false);
			this.groupBox23.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.groupBox19.ResumeLayout(false);
			this.groupBox19.PerformLayout();
			((ISupportInitialize)this.nudDoorDelay1A).EndInit();
			this.groupBox14.ResumeLayout(false);
			this.groupBox14.PerformLayout();
			this.groupBox20.ResumeLayout(false);
			this.groupBox20.PerformLayout();
			this.groupBox22.ResumeLayout(false);
			this.groupBox22.PerformLayout();
			this.grpbController.ResumeLayout(false);
			this.grpbController.PerformLayout();
			this.grpbIP.ResumeLayout(false);
			this.grpbIP.PerformLayout();
			((ISupportInitialize)this.nudPort).EndInit();
			base.ResumeLayout(false);
		}
	}
}
