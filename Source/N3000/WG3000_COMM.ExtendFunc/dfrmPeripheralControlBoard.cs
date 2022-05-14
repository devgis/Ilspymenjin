using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmPeripheralControlBoard : frmN3000
	{
		public int ControllerNO;

		public int ControllerSN;

		private int[] ext_doorSet = new int[4];

		private int[] ext_controlSet = new int[4];

		private int[] ext_warnSignalEnabledSet = new int[4];

		private int[] ext_warnSignalEnabled2Set = new int[4];

		private decimal[] ext_timeoutSet = new decimal[4];

		private int[] ext_active = new int[4];

		private int lastTabIndex;

		private int ext_AlarmControlMode;

		private int ext_SetAlarmOnDelay;

		private int ext_SetAlarmOffDelay;

		private string chkActiveDefault = "";

		private IContainer components;

		internal TextBox txtf_ControllerSN;

		internal TextBox txtf_ControllerNO;

		internal Label Label2;

		internal Label Label1;

		internal Button btnExit;

		internal Button btnOK;

		private GroupBox grpExt;

		private GroupBox groupBox5;

		private RadioButton radioButton25;

		private RadioButton radioButton13;

		private RadioButton radioButton12;

		private RadioButton radioButton11;

		private RadioButton radioButton10;

		private GroupBox grpEvent;

		private CheckBox checkBox90;

		private CheckBox checkBox89;

		private CheckBox checkBox88;

		private CheckBox checkBox87;

		private CheckBox checkBox86;

		private CheckBox checkBox85;

		private CheckBox checkBox84;

		private Label label71;

		private Label label70;

		private NumericUpDown nudDelay;

		private Button btnOption;

		private GroupBox grpSet;

		private CheckBox chkActive;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		private Label label4;

		private Label label3;

		public dfrmPeripheralControlBoard()
		{
			this.InitializeComponent();
		}

		private void dfrmPeripheralControlBoard_Load(object sender, EventArgs e)
		{
			this.txtf_ControllerSN.Text = this.ControllerSN.ToString();
			this.txtf_ControllerNO.Text = this.ControllerNO.ToString();
			this.chkActiveDefault = this.chkActive.Text;
			int controllerID = 0;
			string text = " SELECT b.f_ControllerID, b.f_PeripheralControl ";
			text = text + " FROM t_b_Controller b  WHERE  b.[f_ControllerNO] = " + this.ControllerNO.ToString() + " AND  b.f_Enabled >0 ";
			string text2 = "0";
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							text2 = wgTools.SetObjToStr(oleDbDataReader["f_PeripheralControl"]);
							controllerID = (int)oleDbDataReader[0];
						}
						oleDbDataReader.Close();
					}
					goto IL_14D;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						text2 = wgTools.SetObjToStr(sqlDataReader["f_PeripheralControl"]);
						controllerID = (int)sqlDataReader[0];
					}
					sqlDataReader.Close();
				}
			}
			IL_14D:
			string[] array = text2.Split(new char[]
			{
				','
			});
			if (array.Length != 27)
			{
				text2 = "126,30,30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,10,10,10,0,0,0,0";
				array = text2.Split(new char[]
				{
					','
				});
			}
			int num = 0;
			this.ext_AlarmControlMode = int.Parse(array[num++]);
			this.ext_SetAlarmOnDelay = int.Parse(array[num++]);
			this.ext_SetAlarmOffDelay = int.Parse(array[num++]);
			this.ext_doorSet[0] = int.Parse(array[num++]);
			this.ext_doorSet[1] = int.Parse(array[num++]);
			this.ext_doorSet[2] = int.Parse(array[num++]);
			this.ext_doorSet[3] = int.Parse(array[num++]);
			this.ext_controlSet[0] = int.Parse(array[num++]);
			this.ext_controlSet[1] = int.Parse(array[num++]);
			this.ext_controlSet[2] = int.Parse(array[num++]);
			this.ext_controlSet[3] = int.Parse(array[num++]);
			this.ext_warnSignalEnabledSet[0] = int.Parse(array[num++]);
			this.ext_warnSignalEnabledSet[1] = int.Parse(array[num++]);
			this.ext_warnSignalEnabledSet[2] = int.Parse(array[num++]);
			this.ext_warnSignalEnabledSet[3] = int.Parse(array[num++]);
			this.ext_warnSignalEnabled2Set[0] = int.Parse(array[num++]);
			this.ext_warnSignalEnabled2Set[1] = int.Parse(array[num++]);
			this.ext_warnSignalEnabled2Set[2] = int.Parse(array[num++]);
			this.ext_warnSignalEnabled2Set[3] = int.Parse(array[num++]);
			this.ext_timeoutSet[0] = decimal.Parse(array[num++]);
			this.ext_timeoutSet[1] = decimal.Parse(array[num++]);
			this.ext_timeoutSet[2] = decimal.Parse(array[num++]);
			this.ext_timeoutSet[3] = decimal.Parse(array[num++]);
			this.ext_active[0] = int.Parse(array[num++]);
			this.ext_active[1] = int.Parse(array[num++]);
			this.ext_active[2] = int.Parse(array[num++]);
			this.ext_active[3] = int.Parse(array[num++]);
			using (icController icController = new icController())
			{
				icController.GetInfoFromDBByControllerID(controllerID);
				switch (wgMjController.GetControllerType(this.ControllerSN))
				{
				case 1:
					this.radioButton10.Text = icController.GetDoorName(1);
					this.radioButton11.Visible = false;
					this.radioButton12.Visible = false;
					this.radioButton13.Visible = false;
					this.radioButton25.Location = this.radioButton11.Location;
					break;
				case 2:
					this.radioButton10.Text = icController.GetDoorName(1);
					this.radioButton11.Text = icController.GetDoorName(2);
					this.radioButton12.Visible = false;
					this.radioButton13.Visible = false;
					this.radioButton25.Location = this.radioButton12.Location;
					break;
				default:
					this.radioButton10.Text = icController.GetDoorName(1);
					this.radioButton11.Text = icController.GetDoorName(2);
					this.radioButton12.Text = icController.GetDoorName(3);
					this.radioButton13.Text = icController.GetDoorName(4);
					break;
				}
			}
			this.tabControl1.SelectedTab = this.tabPage1;
			this.chkActive.Text = this.chkActiveDefault + " " + this.tabControl1.SelectedTab.Text;
			this.updateGrpExt(this.tabControl1.SelectedIndex);
		}

		private void updateGrpExt(int doorNum)
		{
			this.lastTabIndex = doorNum;
			if (this.ext_active[doorNum] <= 0)
			{
				this.chkActive.Checked = false;
				this.grpSet.Visible = false;
				this.radioButton10.Checked = true;
				this.checkBox84.Checked = false;
				this.checkBox85.Checked = false;
				this.checkBox86.Checked = false;
				this.checkBox87.Checked = false;
				this.checkBox88.Checked = false;
				this.checkBox89.Checked = false;
				this.checkBox90.Checked = false;
				this.nudDelay.Value = 10m;
				return;
			}
			this.chkActive.Checked = true;
			this.grpSet.Visible = true;
			int num = this.ext_doorSet[doorNum];
			int num2 = num;
			switch (num2)
			{
			case 0:
				this.radioButton10.Checked = true;
				break;
			case 1:
				this.radioButton11.Checked = true;
				break;
			case 2:
				this.radioButton12.Checked = true;
				break;
			case 3:
				this.radioButton13.Checked = true;
				break;
			default:
				if (num2 == 16)
				{
					this.radioButton25.Checked = true;
				}
				break;
			}
			if (!this.radioButton25.Checked)
			{
				this.grpEvent.Visible = true;
				int num3 = this.ext_warnSignalEnabledSet[doorNum];
				this.checkBox84.Checked = ((num3 & 1) > 0);
				this.checkBox85.Checked = ((num3 & 2) > 0);
				this.checkBox86.Checked = ((num3 & 4) > 0);
				this.checkBox87.Checked = ((num3 & 8) > 0);
				this.checkBox88.Checked = ((num3 & 16) > 0);
				this.checkBox89.Checked = ((num3 & 32) > 0);
				this.checkBox90.Checked = ((num3 & 64) > 0);
			}
			else
			{
				this.grpEvent.Visible = false;
			}
			this.nudDelay.Value = this.ext_timeoutSet[doorNum];
		}

		private void updateParamExt(int doorNum)
		{
			if (!this.chkActive.Checked)
			{
				this.ext_active[doorNum] = 0;
				return;
			}
			this.ext_active[doorNum] = 1;
			int num = 0;
			if (this.radioButton10.Checked)
			{
				num = 0;
			}
			if (this.radioButton11.Checked)
			{
				num = 1;
			}
			if (this.radioButton12.Checked)
			{
				num = 2;
			}
			if (this.radioButton13.Checked)
			{
				num = 3;
			}
			if (this.radioButton25.Checked)
			{
				num = 16;
			}
			this.ext_doorSet[doorNum] = num;
			if (this.ext_controlSet[doorNum] == 0)
			{
				this.ext_controlSet[doorNum] = 1;
			}
			if (!this.radioButton25.Checked)
			{
				int num2 = 0;
				if (this.checkBox84.Checked)
				{
					num2 |= 1;
				}
				if (this.checkBox85.Checked)
				{
					num2 |= 2;
				}
				if (this.checkBox86.Checked)
				{
					num2 |= 4;
				}
				if (this.checkBox87.Checked)
				{
					num2 |= 8;
				}
				if (this.checkBox88.Checked)
				{
					num2 |= 16;
				}
				if (this.checkBox89.Checked)
				{
					num2 |= 32;
				}
				if (this.checkBox90.Checked)
				{
					num2 |= 64;
				}
				this.ext_warnSignalEnabledSet[doorNum] = num2;
			}
			this.ext_timeoutSet[doorNum] = this.nudDelay.Value;
		}

		private void saveParmExt()
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.ext_active[i] == 0)
				{
					this.ext_doorSet[i] = 0;
					this.ext_controlSet[i] = 0;
					this.ext_warnSignalEnabledSet[i] = 0;
					this.ext_warnSignalEnabled2Set[i] = 0;
					this.ext_timeoutSet[i] = 0m;
				}
			}
			string text = "";
			text += this.ext_AlarmControlMode.ToString();
			text = text + "," + this.ext_SetAlarmOnDelay.ToString();
			text = text + "," + this.ext_SetAlarmOffDelay.ToString();
			text = text + "," + this.ext_doorSet[0].ToString();
			text = text + "," + this.ext_doorSet[1].ToString();
			text = text + "," + this.ext_doorSet[2].ToString();
			text = text + "," + this.ext_doorSet[3].ToString();
			text = text + "," + this.ext_controlSet[0].ToString();
			text = text + "," + this.ext_controlSet[1].ToString();
			text = text + "," + this.ext_controlSet[2].ToString();
			text = text + "," + this.ext_controlSet[3].ToString();
			text = text + "," + this.ext_warnSignalEnabledSet[0].ToString();
			text = text + "," + this.ext_warnSignalEnabledSet[1].ToString();
			text = text + "," + this.ext_warnSignalEnabledSet[2].ToString();
			text = text + "," + this.ext_warnSignalEnabledSet[3].ToString();
			text = text + "," + this.ext_warnSignalEnabled2Set[0].ToString();
			text = text + "," + this.ext_warnSignalEnabled2Set[1].ToString();
			text = text + "," + this.ext_warnSignalEnabled2Set[2].ToString();
			text = text + "," + this.ext_warnSignalEnabled2Set[3].ToString();
			text = text + "," + this.ext_timeoutSet[0].ToString();
			text = text + "," + this.ext_timeoutSet[1].ToString();
			text = text + "," + this.ext_timeoutSet[2].ToString();
			text = text + "," + this.ext_timeoutSet[3].ToString();
			text = text + "," + this.ext_active[0].ToString();
			text = text + "," + this.ext_active[1].ToString();
			text = text + "," + this.ext_active[2].ToString();
			text = text + "," + this.ext_active[3].ToString();
			string text2 = " UPDATE t_b_Controller SET f_PeripheralControl =" + wgTools.PrepareStr(text);
			text2 = text2 + "   WHERE  [f_ControllerNO] = " + this.ControllerNO.ToString();
			wgAppConfig.runUpdateSql(text2);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < 4; j++)
			{
				if ((this.ext_warnSignalEnabledSet[j] & 4) > 0)
				{
					num = 1;
				}
				if ((this.ext_warnSignalEnabledSet[j] & 2) > 0)
				{
					num2 = 1;
				}
				if ((this.ext_warnSignalEnabledSet[j] & 1) > 0)
				{
					num3 = 1;
				}
				if ((this.ext_warnSignalEnabledSet[j] & 16) > 0)
				{
					num4 = 1;
				}
			}
			if (num == 1)
			{
				text2 = " UPDATE t_b_Controller SET f_DoorInvalidOpen =1  WHERE  [f_ControllerNO] = " + this.ControllerNO.ToString();
				wgAppConfig.runUpdateSql(text2);
			}
			if (num2 == 1)
			{
				text2 = " UPDATE t_b_Controller SET f_DoorOpenTooLong =1  WHERE  [f_ControllerNO] = " + this.ControllerNO.ToString();
				wgAppConfig.runUpdateSql(text2);
			}
			if (num3 == 1)
			{
				text2 = " UPDATE t_b_Controller SET f_ForceWarn =1  WHERE  [f_ControllerNO] = " + this.ControllerNO.ToString();
				wgAppConfig.runUpdateSql(text2);
			}
			if (num4 == 1)
			{
				text2 = " UPDATE t_b_Controller SET f_InvalidCardWarn =1  WHERE  [f_ControllerNO] = " + this.ControllerNO.ToString();
				wgAppConfig.runUpdateSql(text2);
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lastTabIndex != this.tabControl1.SelectedIndex)
			{
				this.chkActive.Text = this.chkActiveDefault + " " + this.tabControl1.SelectedTab.Text;
				this.updateParamExt(this.lastTabIndex);
				this.updateGrpExt(this.tabControl1.SelectedIndex);
			}
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			this.grpSet.Visible = this.chkActive.Checked;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.updateParamExt(this.tabControl1.SelectedIndex);
			this.saveParmExt();
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void radioButton25_CheckedChanged(object sender, EventArgs e)
		{
			this.grpEvent.Visible = !this.radioButton25.Checked;
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
			using (dfrmPeripheralControlBoardSuper dfrmPeripheralControlBoardSuper = new dfrmPeripheralControlBoardSuper())
			{
				dfrmPeripheralControlBoardSuper.extControl = this.ext_controlSet[this.tabControl1.SelectedIndex];
				dfrmPeripheralControlBoardSuper.ext_warnSignalEnabled2 = this.ext_warnSignalEnabled2Set[this.tabControl1.SelectedIndex];
				if (dfrmPeripheralControlBoardSuper.ShowDialog(this) == DialogResult.OK)
				{
					this.ext_controlSet[this.tabControl1.SelectedIndex] = dfrmPeripheralControlBoardSuper.extControl;
					this.ext_warnSignalEnabled2Set[this.tabControl1.SelectedIndex] = dfrmPeripheralControlBoardSuper.ext_warnSignalEnabled2;
				}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPeripheralControlBoard));
			this.txtf_ControllerSN = new TextBox();
			this.txtf_ControllerNO = new TextBox();
			this.Label2 = new Label();
			this.Label1 = new Label();
			this.btnExit = new Button();
			this.btnOK = new Button();
			this.grpExt = new GroupBox();
			this.grpSet = new GroupBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label71 = new Label();
			this.btnOption = new Button();
			this.groupBox5 = new GroupBox();
			this.radioButton25 = new RadioButton();
			this.radioButton13 = new RadioButton();
			this.radioButton12 = new RadioButton();
			this.radioButton11 = new RadioButton();
			this.radioButton10 = new RadioButton();
			this.grpEvent = new GroupBox();
			this.checkBox90 = new CheckBox();
			this.checkBox89 = new CheckBox();
			this.checkBox88 = new CheckBox();
			this.checkBox87 = new CheckBox();
			this.checkBox86 = new CheckBox();
			this.checkBox85 = new CheckBox();
			this.checkBox84 = new CheckBox();
			this.label70 = new Label();
			this.nudDelay = new NumericUpDown();
			this.chkActive = new CheckBox();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.tabPage2 = new TabPage();
			this.tabPage3 = new TabPage();
			this.tabPage4 = new TabPage();
			this.grpExt.SuspendLayout();
			this.grpSet.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.grpEvent.SuspendLayout();
			((ISupportInitialize)this.nudDelay).BeginInit();
			this.tabControl1.SuspendLayout();
			base.SuspendLayout();
			this.txtf_ControllerSN.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.txtf_ControllerSN, "txtf_ControllerSN");
			this.txtf_ControllerSN.Name = "txtf_ControllerSN";
			this.txtf_ControllerSN.ReadOnly = true;
			this.txtf_ControllerNO.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.txtf_ControllerNO, "txtf_ControllerNO");
			this.txtf_ControllerNO.Name = "txtf_ControllerNO";
			this.txtf_ControllerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			this.Label2.Name = "Label2";
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			this.Label1.Name = "Label1";
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.DialogResult = DialogResult.Cancel;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.grpExt.BackColor = Color.Transparent;
			this.grpExt.Controls.Add(this.grpSet);
			this.grpExt.Controls.Add(this.chkActive);
			this.grpExt.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.grpExt, "grpExt");
			this.grpExt.Name = "grpExt";
			this.grpExt.TabStop = false;
			this.grpSet.Controls.Add(this.label4);
			this.grpSet.Controls.Add(this.label3);
			this.grpSet.Controls.Add(this.label71);
			this.grpSet.Controls.Add(this.btnOption);
			this.grpSet.Controls.Add(this.groupBox5);
			this.grpSet.Controls.Add(this.grpEvent);
			this.grpSet.Controls.Add(this.label70);
			this.grpSet.Controls.Add(this.nudDelay);
			componentResourceManager.ApplyResources(this.grpSet, "grpSet");
			this.grpSet.Name = "grpSet";
			this.grpSet.TabStop = false;
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.label71, "label71");
			this.label71.Name = "label71";
			this.btnOption.BackColor = Color.Transparent;
			this.btnOption.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOption, "btnOption");
			this.btnOption.ForeColor = Color.White;
			this.btnOption.Name = "btnOption";
			this.btnOption.UseVisualStyleBackColor = false;
			this.btnOption.Click += new EventHandler(this.btnOption_Click);
			this.groupBox5.Controls.Add(this.radioButton25);
			this.groupBox5.Controls.Add(this.radioButton13);
			this.groupBox5.Controls.Add(this.radioButton12);
			this.groupBox5.Controls.Add(this.radioButton11);
			this.groupBox5.Controls.Add(this.radioButton10);
			componentResourceManager.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			componentResourceManager.ApplyResources(this.radioButton25, "radioButton25");
			this.radioButton25.Name = "radioButton25";
			this.radioButton25.UseVisualStyleBackColor = true;
			this.radioButton25.CheckedChanged += new EventHandler(this.radioButton25_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton13, "radioButton13");
			this.radioButton13.Name = "radioButton13";
			this.radioButton13.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton12, "radioButton12");
			this.radioButton12.Name = "radioButton12";
			this.radioButton12.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton11, "radioButton11");
			this.radioButton11.Name = "radioButton11";
			this.radioButton11.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton10, "radioButton10");
			this.radioButton10.Checked = true;
			this.radioButton10.Name = "radioButton10";
			this.radioButton10.TabStop = true;
			this.radioButton10.UseVisualStyleBackColor = true;
			this.grpEvent.Controls.Add(this.checkBox90);
			this.grpEvent.Controls.Add(this.checkBox89);
			this.grpEvent.Controls.Add(this.checkBox88);
			this.grpEvent.Controls.Add(this.checkBox87);
			this.grpEvent.Controls.Add(this.checkBox86);
			this.grpEvent.Controls.Add(this.checkBox85);
			this.grpEvent.Controls.Add(this.checkBox84);
			componentResourceManager.ApplyResources(this.grpEvent, "grpEvent");
			this.grpEvent.Name = "grpEvent";
			this.grpEvent.TabStop = false;
			componentResourceManager.ApplyResources(this.checkBox90, "checkBox90");
			this.checkBox90.Name = "checkBox90";
			this.checkBox90.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox89, "checkBox89");
			this.checkBox89.Name = "checkBox89";
			this.checkBox89.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox88, "checkBox88");
			this.checkBox88.Name = "checkBox88";
			this.checkBox88.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox87, "checkBox87");
			this.checkBox87.Name = "checkBox87";
			this.checkBox87.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox86, "checkBox86");
			this.checkBox86.Name = "checkBox86";
			this.checkBox86.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox85, "checkBox85");
			this.checkBox85.Name = "checkBox85";
			this.checkBox85.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox84, "checkBox84");
			this.checkBox84.Name = "checkBox84";
			this.checkBox84.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label70, "label70");
			this.label70.Name = "label70";
			componentResourceManager.ApplyResources(this.nudDelay, "nudDelay");
			NumericUpDown arg_9E5_0 = this.nudDelay;
			int[] array = new int[4];
			array[0] = 6553;
			arg_9E5_0.Maximum = new decimal(array);
			this.nudDelay.Name = "nudDelay";
			NumericUpDown arg_A11_0 = this.nudDelay;
			int[] array2 = new int[4];
			array2[0] = 3;
			arg_A11_0.Value = new decimal(array2);
			componentResourceManager.ApplyResources(this.chkActive, "chkActive");
			this.chkActive.Name = "chkActive";
			this.chkActive.UseVisualStyleBackColor = true;
			this.chkActive.CheckedChanged += new EventHandler(this.chkActive_CheckedChanged);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPage1.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage4, "tabPage4");
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.grpExt);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtf_ControllerSN);
			base.Controls.Add(this.txtf_ControllerNO);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmPeripheralControlBoard";
			base.Load += new EventHandler(this.dfrmPeripheralControlBoard_Load);
			this.grpExt.ResumeLayout(false);
			this.grpExt.PerformLayout();
			this.grpSet.ResumeLayout(false);
			this.grpSet.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.grpEvent.ResumeLayout(false);
			this.grpEvent.PerformLayout();
			((ISupportInitialize)this.nudDelay).EndInit();
			this.tabControl1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
