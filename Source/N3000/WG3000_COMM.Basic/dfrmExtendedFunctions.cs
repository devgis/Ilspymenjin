using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.ExtendFunc.Elevator;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmExtendedFunctions : frmN3000
	{
		private IContainer components;

		private GroupBox groupBox1;

		private CheckBox chkRecordDoorStatusEvent;

		private CheckBox chkActiveLogQuery;

		private CheckBox chkRecordButtonEvent;

		private GroupBox groupBox2;

		private GroupBox groupBox3;

		private GroupBox groupBox4;

		private GroupBox groupBox5;

		private Button btnOK;

		private Button btnCancel;

		private CheckBox chkActivateDontDisplayAccessControl;

		private CheckBox chkActivateDontDisplayAttendance;

		private CheckBox chkActivateOtherShiftSchedule;

		private CheckBox chkActivateTimeProfile;

		private CheckBox chkActivateRemoteOpenDoor;

		private CheckBox chkActivateAccessKeypad;

		private CheckBox chkActivatePeripheralControl;

		private CheckBox chkActivateControllerTaskList;

		private CheckBox chkActivateAntiPassBack;

		private CheckBox chkActivateInterLock;

		private CheckBox chkActivateMultiCardAccess;

		private CheckBox chkActivateFirstCardOpen;

		private CheckBox chkActivateWarnForceWithCard;

		private CheckBox chkActivateTimeSegLimittedAccess;

		private CheckBox chkActivateDontAutoLoadPrivileges;

		private CheckBox chkActivateDontAutoLoadSwipeRecords;

		private CheckBox chkActivateMaps;

		private CheckBox chkActivatePCCheckAccess;

		private CheckBox chkActivateElevator;

		private CheckBox chkActivateDoorAsSwitch;

		private CheckBox chkActivateOperatorManagement;

		private CheckBox chkActivatePatrol;

		private CheckBox chkActivateMeal;

		private CheckBox chkActivateMeeting;

		private Button btnSetup;

		private int OneToMoreSelect;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmExtendedFunctions));
			this.groupBox1 = new GroupBox();
			this.chkRecordDoorStatusEvent = new CheckBox();
			this.chkActiveLogQuery = new CheckBox();
			this.chkRecordButtonEvent = new CheckBox();
			this.groupBox2 = new GroupBox();
			this.btnSetup = new Button();
			this.chkActivatePatrol = new CheckBox();
			this.chkActivateMeal = new CheckBox();
			this.chkActivateMeeting = new CheckBox();
			this.chkActivateElevator = new CheckBox();
			this.chkActivateMaps = new CheckBox();
			this.chkActivateOtherShiftSchedule = new CheckBox();
			this.chkActivateDontDisplayAttendance = new CheckBox();
			this.chkActivateDontDisplayAccessControl = new CheckBox();
			this.groupBox3 = new GroupBox();
			this.chkActivatePeripheralControl = new CheckBox();
			this.chkActivateAccessKeypad = new CheckBox();
			this.chkActivateRemoteOpenDoor = new CheckBox();
			this.chkActivateTimeProfile = new CheckBox();
			this.groupBox4 = new GroupBox();
			this.chkActivateOperatorManagement = new CheckBox();
			this.chkActivateDoorAsSwitch = new CheckBox();
			this.chkActivateTimeSegLimittedAccess = new CheckBox();
			this.chkActivatePCCheckAccess = new CheckBox();
			this.chkActivateFirstCardOpen = new CheckBox();
			this.chkActivateMultiCardAccess = new CheckBox();
			this.chkActivateInterLock = new CheckBox();
			this.chkActivateAntiPassBack = new CheckBox();
			this.chkActivateControllerTaskList = new CheckBox();
			this.groupBox5 = new GroupBox();
			this.chkActivateDontAutoLoadSwipeRecords = new CheckBox();
			this.chkActivateDontAutoLoadPrivileges = new CheckBox();
			this.chkActivateWarnForceWithCard = new CheckBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.chkRecordDoorStatusEvent);
			this.groupBox1.Controls.Add(this.chkActiveLogQuery);
			this.groupBox1.Controls.Add(this.chkRecordButtonEvent);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.chkRecordDoorStatusEvent, "chkRecordDoorStatusEvent");
			this.chkRecordDoorStatusEvent.Name = "chkRecordDoorStatusEvent";
			this.chkRecordDoorStatusEvent.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActiveLogQuery, "chkActiveLogQuery");
			this.chkActiveLogQuery.Name = "chkActiveLogQuery";
			this.chkActiveLogQuery.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkRecordButtonEvent, "chkRecordButtonEvent");
			this.chkRecordButtonEvent.Name = "chkRecordButtonEvent";
			this.chkRecordButtonEvent.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.btnSetup);
			this.groupBox2.Controls.Add(this.chkActivatePatrol);
			this.groupBox2.Controls.Add(this.chkActivateMeal);
			this.groupBox2.Controls.Add(this.chkActivateMeeting);
			this.groupBox2.Controls.Add(this.chkActivateElevator);
			this.groupBox2.Controls.Add(this.chkActivateMaps);
			this.groupBox2.Controls.Add(this.chkActivateOtherShiftSchedule);
			this.groupBox2.Controls.Add(this.chkActivateDontDisplayAttendance);
			this.groupBox2.Controls.Add(this.chkActivateDontDisplayAccessControl);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.btnSetup, "btnSetup");
			this.btnSetup.BackColor = Color.Transparent;
			this.btnSetup.BackgroundImage = Resources.pMain_button_normal;
			this.btnSetup.ForeColor = Color.White;
			this.btnSetup.Name = "btnSetup";
			this.btnSetup.UseVisualStyleBackColor = false;
			this.btnSetup.Click += new EventHandler(this.btnSetup_Click);
			componentResourceManager.ApplyResources(this.chkActivatePatrol, "chkActivatePatrol");
			this.chkActivatePatrol.Name = "chkActivatePatrol";
			this.chkActivatePatrol.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateMeal, "chkActivateMeal");
			this.chkActivateMeal.Name = "chkActivateMeal";
			this.chkActivateMeal.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateMeeting, "chkActivateMeeting");
			this.chkActivateMeeting.Name = "chkActivateMeeting";
			this.chkActivateMeeting.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateElevator, "chkActivateElevator");
			this.chkActivateElevator.Name = "chkActivateElevator";
			this.chkActivateElevator.UseVisualStyleBackColor = true;
			this.chkActivateElevator.CheckedChanged += new EventHandler(this.chkActivateElevator_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkActivateMaps, "chkActivateMaps");
			this.chkActivateMaps.Name = "chkActivateMaps";
			this.chkActivateMaps.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateOtherShiftSchedule, "chkActivateOtherShiftSchedule");
			this.chkActivateOtherShiftSchedule.Name = "chkActivateOtherShiftSchedule";
			this.chkActivateOtherShiftSchedule.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateDontDisplayAttendance, "chkActivateDontDisplayAttendance");
			this.chkActivateDontDisplayAttendance.Name = "chkActivateDontDisplayAttendance";
			this.chkActivateDontDisplayAttendance.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateDontDisplayAccessControl, "chkActivateDontDisplayAccessControl");
			this.chkActivateDontDisplayAccessControl.Name = "chkActivateDontDisplayAccessControl";
			this.chkActivateDontDisplayAccessControl.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.BackColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.chkActivatePeripheralControl);
			this.groupBox3.Controls.Add(this.chkActivateAccessKeypad);
			this.groupBox3.Controls.Add(this.chkActivateRemoteOpenDoor);
			this.groupBox3.Controls.Add(this.chkActivateTimeProfile);
			this.groupBox3.ForeColor = Color.White;
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.chkActivatePeripheralControl, "chkActivatePeripheralControl");
			this.chkActivatePeripheralControl.Name = "chkActivatePeripheralControl";
			this.chkActivatePeripheralControl.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateAccessKeypad, "chkActivateAccessKeypad");
			this.chkActivateAccessKeypad.Name = "chkActivateAccessKeypad";
			this.chkActivateAccessKeypad.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateRemoteOpenDoor, "chkActivateRemoteOpenDoor");
			this.chkActivateRemoteOpenDoor.Name = "chkActivateRemoteOpenDoor";
			this.chkActivateRemoteOpenDoor.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateTimeProfile, "chkActivateTimeProfile");
			this.chkActivateTimeProfile.Name = "chkActivateTimeProfile";
			this.chkActivateTimeProfile.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.BackColor = Color.Transparent;
			this.groupBox4.Controls.Add(this.chkActivateOperatorManagement);
			this.groupBox4.Controls.Add(this.chkActivateDoorAsSwitch);
			this.groupBox4.Controls.Add(this.chkActivateTimeSegLimittedAccess);
			this.groupBox4.Controls.Add(this.chkActivatePCCheckAccess);
			this.groupBox4.Controls.Add(this.chkActivateFirstCardOpen);
			this.groupBox4.Controls.Add(this.chkActivateMultiCardAccess);
			this.groupBox4.Controls.Add(this.chkActivateInterLock);
			this.groupBox4.Controls.Add(this.chkActivateAntiPassBack);
			this.groupBox4.Controls.Add(this.chkActivateControllerTaskList);
			this.groupBox4.ForeColor = Color.White;
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.chkActivateOperatorManagement, "chkActivateOperatorManagement");
			this.chkActivateOperatorManagement.Name = "chkActivateOperatorManagement";
			this.chkActivateOperatorManagement.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateDoorAsSwitch, "chkActivateDoorAsSwitch");
			this.chkActivateDoorAsSwitch.BackColor = Color.Red;
			this.chkActivateDoorAsSwitch.Name = "chkActivateDoorAsSwitch";
			this.chkActivateDoorAsSwitch.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.chkActivateTimeSegLimittedAccess, "chkActivateTimeSegLimittedAccess");
			this.chkActivateTimeSegLimittedAccess.BackColor = Color.Red;
			this.chkActivateTimeSegLimittedAccess.Name = "chkActivateTimeSegLimittedAccess";
			this.chkActivateTimeSegLimittedAccess.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.chkActivatePCCheckAccess, "chkActivatePCCheckAccess");
			this.chkActivatePCCheckAccess.Name = "chkActivatePCCheckAccess";
			this.chkActivatePCCheckAccess.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateFirstCardOpen, "chkActivateFirstCardOpen");
			this.chkActivateFirstCardOpen.Name = "chkActivateFirstCardOpen";
			this.chkActivateFirstCardOpen.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateMultiCardAccess, "chkActivateMultiCardAccess");
			this.chkActivateMultiCardAccess.Name = "chkActivateMultiCardAccess";
			this.chkActivateMultiCardAccess.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateInterLock, "chkActivateInterLock");
			this.chkActivateInterLock.Name = "chkActivateInterLock";
			this.chkActivateInterLock.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateAntiPassBack, "chkActivateAntiPassBack");
			this.chkActivateAntiPassBack.Name = "chkActivateAntiPassBack";
			this.chkActivateAntiPassBack.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateControllerTaskList, "chkActivateControllerTaskList");
			this.chkActivateControllerTaskList.Name = "chkActivateControllerTaskList";
			this.chkActivateControllerTaskList.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.BackColor = Color.Transparent;
			this.groupBox5.Controls.Add(this.chkActivateDontAutoLoadSwipeRecords);
			this.groupBox5.Controls.Add(this.chkActivateDontAutoLoadPrivileges);
			this.groupBox5.Controls.Add(this.chkActivateWarnForceWithCard);
			this.groupBox5.ForeColor = Color.White;
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			componentResourceManager.ApplyResources(this.chkActivateDontAutoLoadSwipeRecords, "chkActivateDontAutoLoadSwipeRecords");
			this.chkActivateDontAutoLoadSwipeRecords.Name = "chkActivateDontAutoLoadSwipeRecords";
			this.chkActivateDontAutoLoadSwipeRecords.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateDontAutoLoadPrivileges, "chkActivateDontAutoLoadPrivileges");
			this.chkActivateDontAutoLoadPrivileges.Name = "chkActivateDontAutoLoadPrivileges";
			this.chkActivateDontAutoLoadPrivileges.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkActivateWarnForceWithCard, "chkActivateWarnForceWithCard");
			this.chkActivateWarnForceWithCard.Name = "chkActivateWarnForceWithCard";
			this.chkActivateWarnForceWithCard.UseVisualStyleBackColor = true;
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
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.groupBox5);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmExtendedFunctions";
			base.Load += new EventHandler(this.dfrmExtendedFunctions_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmExtendedFunctions_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			base.ResumeLayout(false);
		}

		public dfrmExtendedFunctions()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			wgAppConfig.setSystemParamValueBool(101, this.chkRecordButtonEvent.Checked);
			wgAppConfig.setSystemParamValueBool(102, this.chkRecordDoorStatusEvent.Checked);
			wgAppConfig.setSystemParamValueBool(103, this.chkActiveLogQuery.Checked);
			wgAppConfig.setSystemParamValueBool(111, this.chkActivateDontDisplayAccessControl.Checked);
			wgAppConfig.setSystemParamValueBool(112, this.chkActivateDontDisplayAttendance.Checked);
			wgAppConfig.setSystemParamValueBool(113, this.chkActivateOtherShiftSchedule.Checked);
			wgAppConfig.setSystemParamValueBool(114, this.chkActivateMaps.Checked);
			wgAppConfig.setSystemParamValueBool(121, this.chkActivateTimeProfile.Checked);
			wgAppConfig.setSystemParamValueBool(122, this.chkActivateRemoteOpenDoor.Checked);
			wgAppConfig.setSystemParamValueBool(123, this.chkActivateAccessKeypad.Checked);
			wgAppConfig.setSystemParamValueBool(124, this.chkActivatePeripheralControl.Checked);
			wgAppConfig.setSystemParamValueBool(148, this.chkActivateOperatorManagement.Checked);
			wgAppConfig.setSystemParamValueBool(131, this.chkActivateControllerTaskList.Checked);
			wgAppConfig.setSystemParamValueBool(132, this.chkActivateAntiPassBack.Checked);
			wgAppConfig.setSystemParamValueBool(133, this.chkActivateInterLock.Checked);
			wgAppConfig.setSystemParamValueBool(134, this.chkActivateMultiCardAccess.Checked);
			wgAppConfig.setSystemParamValueBool(135, this.chkActivateFirstCardOpen.Checked);
			wgAppConfig.setSystemParamValueBool(137, this.chkActivatePCCheckAccess.Checked);
			wgAppConfig.setSystemParamValueBool(136, this.chkActivateTimeSegLimittedAccess.Checked);
			wgAppConfig.setSystemParamValueBool(146, this.chkActivateDoorAsSwitch.Checked);
			wgAppConfig.setSystemParamValueBool(141, this.chkActivateWarnForceWithCard.Checked);
			wgAppConfig.setSystemParamValueBool(142, this.chkActivateDontAutoLoadPrivileges.Checked);
			wgAppConfig.setSystemParamValueBool(143, this.chkActivateDontAutoLoadSwipeRecords.Checked);
			if (!this.chkActivateElevator.Checked)
			{
				wgAppConfig.setSystemParamValueBool(144, false);
			}
			else
			{
				if (this.OneToMoreSelect == 0)
				{
					this.OneToMoreSelect = 1;
				}
				wgAppConfig.setSystemParamValue(144, this.OneToMoreSelect.ToString());
			}
			wgAppConfig.setSystemParamValueBool(149, this.chkActivateMeeting.Checked);
			wgAppConfig.setSystemParamValueBool(150, this.chkActivateMeal.Checked);
			wgAppConfig.setSystemParamValueBool(151, this.chkActivatePatrol.Checked);
			base.DialogResult = DialogResult.Cancel;
			if (XMessageBox.Show(this, CommonStr.strUpdateSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				base.DialogResult = DialogResult.OK;
			}
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmExtendedFunctions_Load(object sender, EventArgs e)
		{
			this.chkRecordButtonEvent.Checked = wgAppConfig.getParamValBoolByNO(101);
			this.chkRecordDoorStatusEvent.Checked = wgAppConfig.getParamValBoolByNO(102);
			this.chkActiveLogQuery.Checked = wgAppConfig.getParamValBoolByNO(103);
			this.chkActivateDontDisplayAccessControl.Checked = wgAppConfig.getParamValBoolByNO(111);
			this.chkActivateDontDisplayAttendance.Checked = wgAppConfig.getParamValBoolByNO(112);
			this.chkActivateOtherShiftSchedule.Checked = wgAppConfig.getParamValBoolByNO(113);
			this.chkActivateMaps.Checked = wgAppConfig.getParamValBoolByNO(114);
			this.chkActivateTimeProfile.Checked = wgAppConfig.getParamValBoolByNO(121);
			this.chkActivateRemoteOpenDoor.Checked = wgAppConfig.getParamValBoolByNO(122);
			this.chkActivateAccessKeypad.Checked = wgAppConfig.getParamValBoolByNO(123);
			this.chkActivatePeripheralControl.Checked = wgAppConfig.getParamValBoolByNO(124);
			this.chkActivateOperatorManagement.Checked = wgAppConfig.getParamValBoolByNO(148);
			this.chkActivateControllerTaskList.Checked = wgAppConfig.getParamValBoolByNO(131);
			this.chkActivateAntiPassBack.Checked = wgAppConfig.getParamValBoolByNO(132);
			this.chkActivateInterLock.Checked = wgAppConfig.getParamValBoolByNO(133);
			this.chkActivateMultiCardAccess.Checked = wgAppConfig.getParamValBoolByNO(134);
			this.chkActivateFirstCardOpen.Checked = wgAppConfig.getParamValBoolByNO(135);
			this.chkActivatePCCheckAccess.Checked = wgAppConfig.getParamValBoolByNO(137);
			this.chkActivateTimeSegLimittedAccess.Checked = wgAppConfig.getParamValBoolByNO(136);
			this.chkActivateDoorAsSwitch.Checked = wgAppConfig.getParamValBoolByNO(146);
			this.chkActivateWarnForceWithCard.Checked = wgAppConfig.getParamValBoolByNO(141);
			this.chkActivateDontAutoLoadPrivileges.Checked = wgAppConfig.getParamValBoolByNO(142);
			this.chkActivateDontAutoLoadSwipeRecords.Checked = wgAppConfig.getParamValBoolByNO(143);
			this.chkActivateElevator.Checked = wgAppConfig.getParamValBoolByNO(144);
			this.OneToMoreSelect = int.Parse("0" + wgAppConfig.getSystemParamByNO(144));
			this.chkActivateMeeting.Checked = wgAppConfig.getParamValBoolByNO(149);
			this.chkActivateMeal.Checked = wgAppConfig.getParamValBoolByNO(150);
			this.chkActivatePatrol.Checked = wgAppConfig.getParamValBoolByNO(151);
			if (!this.chkActivateTimeSegLimittedAccess.Checked)
			{
				this.chkActivateTimeSegLimittedAccess.Visible = false;
			}
			if (!this.chkActivateDoorAsSwitch.Checked)
			{
				this.chkActivateDoorAsSwitch.Visible = false;
			}
		}

		private void funcCtrlShiftQ()
		{
			this.chkActivateTimeSegLimittedAccess.Visible = true;
			this.chkActivateDoorAsSwitch.Visible = true;
		}

		private void dfrmExtendedFunctions_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.funcCtrlShiftQ();
			}
		}

		private void chkActivateElevator_CheckedChanged(object sender, EventArgs e)
		{
			this.btnSetup.Visible = this.chkActivateElevator.Checked;
		}

		private void btnSetup_Click(object sender, EventArgs e)
		{
			using (dfrmOneToMoreSetup dfrmOneToMoreSetup = new dfrmOneToMoreSetup())
			{
				dfrmOneToMoreSetup.radioButton0.Checked = true;
				dfrmOneToMoreSetup.radioButton1.Checked = ((this.OneToMoreSelect & 255) == 2);
				dfrmOneToMoreSetup.radioButton2.Checked = ((this.OneToMoreSelect & 255) == 3);
				if (this.OneToMoreSelect > 3)
				{
					try
					{
						dfrmOneToMoreSetup.numericUpDown21.Value = (this.OneToMoreSelect >> 8 & 255) / 10m;
						dfrmOneToMoreSetup.numericUpDown20.Value = (this.OneToMoreSelect >> 16 & 255) / 10m;
						dfrmOneToMoreSetup.Size = new Size(554, 259);
					}
					catch (Exception)
					{
					}
				}
				if (dfrmOneToMoreSetup.ShowDialog() == DialogResult.OK)
				{
					this.OneToMoreSelect = 1;
					if (dfrmOneToMoreSetup.radioButton1.Checked)
					{
						this.OneToMoreSelect = 2;
					}
					if (dfrmOneToMoreSetup.radioButton2.Checked)
					{
						this.OneToMoreSelect = 3;
					}
					if (!(dfrmOneToMoreSetup.numericUpDown21.Value == 0.4m) || !(dfrmOneToMoreSetup.numericUpDown20.Value == 5m))
					{
						this.OneToMoreSelect += (int)(dfrmOneToMoreSetup.numericUpDown21.Value * 10m) << 8;
						this.OneToMoreSelect += (int)(dfrmOneToMoreSetup.numericUpDown20.Value * 10m) << 16;
					}
				}
			}
		}
	}
}
