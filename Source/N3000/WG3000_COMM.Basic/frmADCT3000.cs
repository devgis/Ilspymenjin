using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.ExtendFunc;
using WG3000_COMM.ExtendFunc.Elevator;
using WG3000_COMM.ExtendFunc.Meal;
using WG3000_COMM.ExtendFunc.Meeting;
using WG3000_COMM.ExtendFunc.Patrol;
using WG3000_COMM.ExtendFunc.PCCheck;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class frmADCT3000 : Form
	{
		private const int funNameLoc = 1;

		private const int funTagLoc = 2;

		private const int funItemLen = 3;

		private string[,] functionNameBasicConfigure;

		private string[,] functionNameAccessControl;

		private string[,] functionNameBasicOperate;

		private string[,] functionNameAttendence;

		private string[,] functionNameTool;

		private Button btnIconSelected;

		private ToolStripButton btnBookmarkSelected;

		private string oldTitle;

		private string defaultTitle;

		private dfrmSetPassword dfrmSetPassword1;

		private dfrmOperator dfrmOperator1;

		private dfrmDbCompact dfrmDbCompact1;

		private dfrmControllerTaskList dfrmControllerTaskList1;

		private dfrmLogQuery dfrmLogQuery1;

		private dfrmAbout dfrmAbout1;

		private bool bConfirmClose;

		private dfrmNetControllerConfig dfrmNetControllerConfig1;

		private frmTestController frmTestController1;

		private dfrmCheckAccessConfigure dfrmCheckAccessConfigure1;

		private bool bDisplayHideLogin;

		private IContainer components;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripMenuItem toolStripMenuItem3;

		private ToolStripMenuItem toolStripMenuItem4;

		private ToolStripMenuItem toolStripMenuItem5;

		private ToolStripMenuItem toolStripMenuItem6;

		private ToolStripMenuItem toolStripMenuItem7;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem toolStripMenuItem8;

		private ToolStripMenuItem toolStripMenuItem9;

		private ToolStripMenuItem toolStripMenuItem10;

		private ToolStripMenuItem toolStripMenuItem11;

		private ToolStripMenuItem toolStripMenuItem12;

		private ToolStripMenuItem toolStripMenuItem13;

		private ToolStripMenuItem toolStripMenuItem14;

		private ToolStripMenuItem toolStripMenuItem15;

		private ToolStripMenuItem toolStripMenuItem16;

		private ToolStripMenuItem toolStripMenuItem17;

		private ToolStripMenuItem toolStripMenuItem18;

		private ToolStripMenuItem toolsFormToolStripMenuItem;

		private FlowLayoutPanel flowLayoutPanel1ICon;

		private ToolStrip toolStrip1BookMark;

		private ToolStripButton toolStripButtonBookmark3;

		private Panel panel2Content;

		private Button btnIconBasicConfig;

		private Button btnIconAccessControl;

		private Button btnIconBasicOperate;

		private Button btnIconAttendance;

		private ToolStripButton toolStripButtonBookmark1;

		private ToolStripButton toolStripButtonBookmark2;

		private PictureBox panel4Form;

		private ToolStripMenuItem toolStripMenuItem19;

		private ToolStripMenuItem cmdChangePasswor;

		private ToolStripMenuItem cmdOperatorManage;

		private ToolStripMenuItem mnuDBBackup;

		private ToolStripMenuItem toolStripMenuItem23;

		private ToolStripMenuItem mnuExtendedFunction;

		private ToolStripMenuItem mnuOption;

		private ToolStripMenuItem mnuPCCheckAccessConfigure;

		private ToolStripMenuItem mnuTaskList;

		private ToolStripMenuItem mnuLogQuery;

		private ToolStripMenuItem mnuDeleteOldRecords;

		private ToolStripMenuItem mnuAbout;

		private ToolStripMenuItem mnuManual;

		private ToolStripMenuItem mnuSystemCharacteristic;

		private ContextMenuStrip contextMenuStrip1Tools;

		private ContextMenuStrip contextMenuStrip2Help;

		private ToolStripButton toolStripButton4;

		private ToolStripButton toolStripButton3;

		private ToolStripButton toolStripButton2;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButton5;

		private StatusStrip stbRunInfo;

		private ToolStripStatusLabel statOperator;

		private ToolStripStatusLabel statSoftwareVer;

		private ToolStripStatusLabel statCOM;

		private ToolStripStatusLabel statRuninfo1;

		private ToolStripStatusLabel statRuninfo2;

		private ToolStripStatusLabel statRuninfo3;

		private ToolStripStatusLabel statRuninfoLoadedNum;

		private ToolStripStatusLabel statTimeDate;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripDropDownButton mnu1Help;

		private System.Windows.Forms.Timer timer1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem mnuExit;

		private ToolStripButton toolStripButton7;

		private ToolStripButton toolStripButton6;

		private ToolStripMenuItem mnu1Tool;

		private ContextMenuStrip contextMenuStrip3Normal;

		private ToolStripMenuItem shortcutPersonnel;

		private ToolStripMenuItem shortcutPrivilege;

		private ToolStripMenuItem shortcutConsole;

		private ToolStripMenuItem shortcutSwipe;

		private ToolStripMenuItem shortcutAttendance;

		private ToolStripMenuItem shortcutControllers;

		private Panel panel1;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem mnuInterfaceLock;

		private ToolStripMenuItem systemParamsToolStripMenuItem;

		private ToolStripMenuItem mnuElevator;

		private GroupBox grpGettingStarted;

		private Button btnHideGettingStarted;

		private ToolTip toolTip1;

		private CheckBox chkHideLogin;

		private Button btnAddPrivilege;

		private Button btnAutoAddCardBySwiping;

		private Button btnAddController;

		private Label label1;

		private ToolStripMenuItem toolStripMenuItem20;

		private ToolStripMenuItem mnuDoorAsSwitch;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem cmdEditOperator;

		private ToolStripMenuItem mnuMeetingSign;

		private ToolStripMenuItem mnuMeal;

		private ToolStripMenuItem mnuPatrol;

		public frmADCT3000()
		{
			string[,] array = new string[3, 3];
			array[0, 0] = "控制器";
			array[0, 1] = "mnuControllers";
			array[0, 2] = "Basic.frmControllers";
			array[1, 0] = "部门班组";
			array[1, 1] = "mnuGroups";
			array[1, 2] = "Basic.frmDepartments";
			array[2, 0] = "用户";
			array[2, 1] = "mnuConsumers";
			array[2, 2] = "Basic.frmUsers";
			this.functionNameBasicConfigure = array;
			string[,] array2 = new string[9, 3];
			array2[0, 0] = "权限";
			array2[0, 1] = "mnuPrivilege";
			array2[0, 2] = "Basic.frmPrivileges";
			array2[1, 0] = "时段";
			array2[1, 1] = "mnuControlSeg";
			array2[1, 2] = "Basic.frmControlSegs";
			array2[2, 0] = "报警.消防.防盗.联动";
			array2[2, 1] = "mnuPeripheral";
			array2[2, 2] = "ExtendFunc.dfrmControllerWarnSet";
			array2[3, 0] = "密码管理";
			array2[3, 1] = "mnuPasswordManagement";
			array2[3, 2] = "ExtendFunc.dfrmControllerExtendFuncPasswordManage";
			array2[4, 0] = "反潜回";
			array2[4, 1] = "mnuAntiBack";
			array2[4, 2] = "ExtendFunc.dfrmControllerAntiPassback";
			array2[5, 0] = "多门互锁";
			array2[5, 1] = "mnuInterLock";
			array2[5, 2] = "ExtendFunc.dfrmControllerInterLock";
			array2[6, 0] = "多卡开门";
			array2[6, 1] = "mnuMoreCards";
			array2[6, 2] = "ExtendFunc.dfrmControllerMultiCards";
			array2[7, 0] = "首卡开门";
			array2[7, 1] = "mnuFirstCard";
			array2[7, 2] = "ExtendFunc.dfrmControllerFirstCard";
			array2[8, 0] = "定时任务";
			array2[8, 1] = "mnuTaskList";
			array2[8, 2] = "ExtendFunc.dfrmControllerTaskList";
			this.functionNameAccessControl = array2;
			string[,] array3 = new string[2, 3];
			array3[0, 0] = "总控制台";
			array3[0, 1] = "mnuTotalControl";
			array3[0, 2] = "Basic.frmConsole";
			array3[1, 0] = "查询原始记录";
			array3[1, 1] = "mnuCardRecords";
			array3[1, 2] = "Basic.frmSwipeRecords";
			this.functionNameBasicOperate = array3;
			string[,] array4 = new string[8, 3];
			array4[0, 0] = "考勤报表";
			array4[0, 1] = "mnuAttendenceData";
			array4[0, 2] = "Reports.Shift.frmShiftAttReport";
			array4[1, 0] = "正常班设置";
			array4[1, 1] = "mnuShiftNormalConfigure";
			array4[1, 2] = "Reports.Shift.dfrmShiftNormalParamSet";
			array4[2, 0] = "倒班设置";
			array4[2, 1] = "mnuShiftRule";
			array4[2, 2] = "Reports.Shift.dfrmShiftOtherParamSet";
			array4[3, 0] = "倒班班次";
			array4[3, 1] = "mnuShiftSet";
			array4[3, 2] = "Reports.Shift.frmShiftOtherTypes";
			array4[4, 0] = "倒班排班";
			array4[4, 1] = "mnuShiftArrange";
			array4[4, 2] = "Reports.Shift.frmShiftOtherData";
			array4[5, 0] = "正常班节假日";
			array4[5, 1] = "mnuHolidaySet";
			array4[5, 2] = "Reports.Shift.dfrmHolidaySet";
			array4[6, 0] = "请假出差";
			array4[6, 1] = "mnuLeave";
			array4[6, 2] = "Reports.Shift.frmLeave";
			array4[7, 0] = "签到";
			array4[7, 1] = "mnuManualCardRecord";
			array4[7, 2] = "Reports.Shift.frmManualSwipeRecords";
			this.functionNameAttendence = array4;
			string[,] array5 = new string[14, 3];
			array5[0, 0] = "工具";
			array5[0, 1] = "mnu1Tool";
			array5[0, 2] = "";
			array5[1, 0] = "修改密码";
			array5[1, 1] = "cmdChangePasswor";
			array5[1, 2] = "Basic.dfrmSetPassword";
			array5[2, 0] = "操作员管理";
			array5[2, 1] = "cmdOperatorManage";
			array5[2, 2] = "Basic.dfrmOperator";
			array5[3, 0] = "数据库备份";
			array5[3, 1] = "mnuDBBackup";
			array5[3, 2] = "Basic.dfrmDbCompact";
			array5[4, 0] = "控制器通信密码";
			array5[4, 1] = "mnuControllerCommPasswordSet";
			array5[4, 2] = "Basic.";
			array5[5, 0] = "扩展功能";
			array5[5, 1] = "mnuExtendedFunction";
			array5[5, 2] = "Basic.dfrmExtendedFunctions";
			array5[6, 0] = "选项";
			array5[6, 1] = "mnuOption";
			array5[6, 2] = "";
			array5[7, 0] = "操作日志";
			array5[7, 1] = "mnuLogQuery";
			array5[7, 2] = "Basic.dfrmLogQuery";
			array5[8, 0] = "帮助";
			array5[8, 1] = "mnu1Help";
			array5[8, 2] = "";
			array5[9, 0] = "关于";
			array5[9, 1] = "mnuAbout";
			array5[9, 2] = "Basic.dfrmAbout";
			array5[10, 0] = "入门指南";
			array5[10, 1] = "mnuBeginner";
			array5[10, 2] = "Basic.";
			array5[11, 0] = "使用说明书";
			array5[11, 1] = "mnuManual";
			array5[11, 2] = "";
			array5[12, 0] = "系统特性";
			array5[12, 1] = "mnuSystemCharacteristic";
			array5[12, 2] = "";
			array5[13, 0] = "远程开门";
			array5[13, 1] = "TotalControl_RemoteOpen";
			array5[13, 2] = "";
			this.functionNameTool = array5;
			this.oldTitle = "";
			this.defaultTitle = "";
			this.bDisplayHideLogin = true;
			//base.ctor();
			this.InitializeComponent();
			MdiClient value = new MdiClient();
			base.Controls.Add(value);
		}

		private void btnIconBasicConfig_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (this.btnIconSelected != null && button == this.btnIconSelected)
			{
				return;
			}
			this.btnIconSelected = button;
			foreach (object current in this.flowLayoutPanel1ICon.Controls)
			{
				if (current is Button)
				{
					(current as Button).BackgroundImage = null;
					(current as Button).BackColor = Color.FromArgb(147, 150, 177);
				}
			}
			button.BackgroundImage = Resources.pMain_icon_focus02;
			button.BackColor = Color.Transparent;
			this.closeChildForm();
			foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
			{
				toolStripButton.BackgroundImage = Resources.pMain_Bookmark_normal;
			}
			if (icOperator.PCSysInfo(false).IndexOf(": \r\nMicrosoft Windows 7 ") <= 0)
			{
				foreach (ToolStripButton toolStripButton2 in this.toolStrip1BookMark.Items)
				{
					toolStripButton2.TextAlign = ContentAlignment.MiddleCenter;
				}
			}
			this.btnBookmarkSelected = null;
			string[,] array = null;
			if (wgTools.SetObjToStr(button.Tag) == "BasciConfig")
			{
				array = this.functionNameBasicConfigure;
			}
			if (wgTools.SetObjToStr(button.Tag) == "BasicOperate")
			{
				array = this.functionNameBasicOperate;
			}
			if (button.Tag.ToString() == "AccessControl")
			{
				array = this.functionNameAccessControl;
			}
			if (button.Tag.ToString() == "Attendance")
			{
				array = this.functionNameAttendence;
			}
			foreach (object current2 in this.toolStrip1BookMark.Items)
			{
				(current2 as ToolStripButton).Visible = false;
			}
			if (array != null)
			{
				int num = 0;
				int num2 = 0;
				while (num2 < array.Length / 3 && num2 < this.toolStrip1BookMark.Items.Count)
				{
					if (!string.IsNullOrEmpty(array[num2, 1]))
					{
						this.toolStrip1BookMark.Items[num].Text = CommonStr.ResourceManager.GetString("strFunctionDisplayName_" + array[num2, 1]);
						this.toolStrip1BookMark.Items[num].Text = wgAppConfig.ReplaceFloorRomm(this.toolStrip1BookMark.Items[num].Text);
						this.toolStrip1BookMark.Items[num].Tag = "WG3000_COMM." + array[num2, 2];
						this.toolStrip1BookMark.Items[num].Visible = true;
						num++;
					}
					num2++;
				}
			}
			if (wgTools.SetObjToStr(button.Tag) == "BasicOperate" && this.shortcutConsole.Enabled)
			{
				this.shortcutConsole.PerformClick();
			}
		}

		private void toolStrip1BookMark_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripButton toolStripButton = e.ClickedItem as ToolStripButton;
			if (this.btnBookmarkSelected != null && toolStripButton == this.btnBookmarkSelected)
			{
				return;
			}
			this.btnBookmarkSelected = toolStripButton;
			foreach (ToolStripButton toolStripButton2 in this.toolStrip1BookMark.Items)
			{
				toolStripButton2.BackgroundImage = Resources.pMain_Bookmark_normal;
			}
			toolStripButton.BackgroundImage = Resources.pMain_Bookmark_focus;
			this.closeChildForm();
			Form form = null;
			if (!string.IsNullOrEmpty(wgTools.SetObjToStr(toolStripButton.Tag)))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Type type = executingAssembly.GetType(toolStripButton.Tag.ToString());
				object obj = Activator.CreateInstance(type);
				form = (Form)obj;
			}
			if (form != null)
			{
				if (toolStripButton.Tag.ToString().IndexOf(".dfrm") >= 0)
				{
					form.ShowDialog(this);
					this.btnBookmarkSelected = null;
					toolStripButton.BackgroundImage = Resources.pMain_Bookmark_normal;
					return;
				}
				form.Location = new Point(-4, -32);
				form.ControlBox = false;
				form.WindowState = FormWindowState.Normal;
				form.MdiParent = this;
				Cursor.Current = Cursors.WaitCursor;
				form.FormBorderStyle = FormBorderStyle.None;
				form.StartPosition = FormStartPosition.Manual;
				form.Show();
				form.Dock = DockStyle.Fill;
				Cursor.Current = Cursors.Default;
				this.panel4Form.Controls.Add(form);
				if (this.btnIconSelected != null)
				{
					this.btnIconSelected.Select();
				}
			}
		}

		private void toolStripButtonBookmark1_Click(object sender, EventArgs e)
		{
		}

		private void frmADCT3000_Load(object sender, EventArgs e)
		{
			wgAppConfig.bFloorRoomManager = wgAppConfig.getParamValBoolByNO(145);
			this.toolStripButtonBookmark2.Text = wgAppConfig.ReplaceFloorRomm(this.toolStripButtonBookmark2.Text);
			Icon icon = base.Icon;
			wgAppConfig.GetAppIcon(ref icon);
			base.Icon = icon;
			base.KeyPreview = true;
			Application.ThreadException += new ThreadExceptionEventHandler(Program.GlobalExceptionHandler);
			UserControlFind.blogin = true;
			UserControlFindSecond.blogin = true;
			Application.ThreadException += new ThreadExceptionEventHandler(Program.GlobalExceptionHandler);
			this.hideMenuBySystemConfig();
			this.hideMenuByUserPrivilege();
			bool flag = true;
			foreach (object current in this.flowLayoutPanel1ICon.Controls)
			{
				if (current is Button && (current as Button).Visible)
				{
					flag = false;
					(current as Button).PerformClick();
					break;
				}
			}
			if (flag)
			{
				XMessageBox.Show(CommonStr.strOperatorHaveNoPrivilege);
				this.bConfirmClose = true;
				this.mnuExit.PerformClick();
				return;
			}
			UserControlFind.blogin = true;
			UserControlFindSecond.blogin = true;
			if (wgAppConfig.getParamValBoolByNO(137) && icOperator.OperatorID == 1)
			{
				this.mnuPCCheckAccessConfigure.Visible = true;
			}
			if (!wgAppConfig.getParamValBoolByNO(144))
			{
				this.mnuElevator.Visible = false;
			}
			else
			{
				using (dfrmOneToMoreSetup dfrmOneToMoreSetup = new dfrmOneToMoreSetup())
				{
					if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 2)
					{
						this.mnuElevator.Text = dfrmOneToMoreSetup.radioButton1.Text;
					}
					else if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 3)
					{
						this.mnuElevator.Text = dfrmOneToMoreSetup.radioButton2.Text;
					}
					else
					{
						this.mnuElevator.Text = dfrmOneToMoreSetup.radioButton0.Text;
					}
				}
			}
			if (!wgAppConfig.getParamValBoolByNO(149))
			{
				this.mnuMeetingSign.Visible = false;
			}
			if (!wgAppConfig.getParamValBoolByNO(150))
			{
				this.mnuMeal.Visible = false;
			}
			if (!wgAppConfig.getParamValBoolByNO(151))
			{
				this.mnuPatrol.Visible = false;
			}
			if (wgAppConfig.getParamValBoolByNO(146) && icOperator.OperatorID == 1)
			{
				this.mnuDoorAsSwitch.Visible = true;
			}
			if (!wgAppConfig.getParamValBoolByNO(148))
			{
				this.cmdOperatorManage.Visible = false;
			}
			this.Text = wgAppConfig.LoginTitle;
			this.loadStbRunInfo();
			wgAppRunInfo.evAppRunInfoLoadNum += new wgAppRunInfo.appRunInfoLoadNumHandler(this.statRunInfo_Num_Update);
			wgAppRunInfo.evAppRunInfoCommStatus += new wgAppRunInfo.appRunInfoCommStatusHandler(this.statRunInfo_CommStatus_Update);
			wgAppRunInfo.evAppRunInfoMonitor += new wgAppRunInfo.appRunInfoMonitorHandler(this.statRunInfo_Monitor_Update);
			base.WindowState = FormWindowState.Maximized;
			this.timer1.Enabled = true;
			if (icOperator.OperatorID != 1 || wgAppConfig.GetKeyVal("HideGettingStartedWhenLogin") == "1")
			{
				this.grpGettingStarted.Visible = false;
				this.chkHideLogin.Checked = (wgAppConfig.GetKeyVal("HideGettingStartedWhenLogin") == "1");
				this.flowLayoutPanel1ICon.Size = new Size(127, 338);
				if (icOperator.OperatorID != 1)
				{
					this.toolStripMenuItem20.Visible = false;
				}
			}
			string strSql = "SELECT COUNT(*) from t_s_Operator ";
			if (int.Parse("0" + wgTools.SetObjToStr(wgAppConfig.getValBySql(strSql))) > 1)
			{
				this.cmdEditOperator.Visible = false;
			}
			else
			{
				this.cmdChangePasswor.Visible = false;
				this.cmdEditOperator.Visible = true;
			}
			this.flowLayoutPanel1ICon.Size = new Size(this.flowLayoutPanel1ICon.Size.Width, 0);
		}

		private void hideMenuByUserPrivilege()
		{
			icOperator.OperatePrivilegeLoad(ref this.functionNameBasicConfigure, 3, 1);
			icOperator.OperatePrivilegeLoad(ref this.functionNameAccessControl, 3, 1);
			icOperator.OperatePrivilegeLoad(ref this.functionNameBasicOperate, 3, 1);
			icOperator.OperatePrivilegeLoad(ref this.functionNameAttendence, 3, 1);
			using (ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem())
			{
				foreach (object current in this.contextMenuStrip1Tools.Items)
				{
					if (object.ReferenceEquals(current.GetType(), toolStripMenuItem.GetType()) && !icOperator.OperatePrivilegeVisible((current as ToolStripMenuItem).Name))
					{
						(current as ToolStripMenuItem).Visible = false;
					}
				}
				foreach (object current2 in this.contextMenuStrip2Help.Items)
				{
					if (object.ReferenceEquals(current2.GetType(), toolStripMenuItem.GetType()) && !icOperator.OperatePrivilegeVisible((current2 as ToolStripMenuItem).Name))
					{
						(current2 as ToolStripMenuItem).Visible = false;
					}
				}
			}
			if (!icOperator.OperatePrivilegeVisible("mnu1BasicConfigure"))
			{
				this.btnIconBasicConfig.Visible = false;
			}
			if (!icOperator.OperatePrivilegeVisible("mnu1DoorControl"))
			{
				this.btnIconAccessControl.Visible = false;
			}
			if (!icOperator.OperatePrivilegeVisible("mnu1BasicOperate"))
			{
				this.btnIconBasicOperate.Visible = false;
			}
			if (!icOperator.OperatePrivilegeVisible("mnu1Attendence"))
			{
				this.btnIconAttendance.Visible = false;
			}
			if (!this.btnIconBasicConfig.Visible)
			{
				this.flowLayoutPanel1ICon.Controls.Remove(this.btnIconBasicConfig);
				this.btnIconBasicConfig.Dispose();
				this.shortcutControllers.Visible = false;
				this.shortcutPersonnel.Visible = false;
			}
			if (!this.btnIconAccessControl.Visible)
			{
				this.flowLayoutPanel1ICon.Controls.Remove(this.btnIconAccessControl);
				this.btnIconAccessControl.Dispose();
				this.shortcutPrivilege.Visible = false;
			}
			if (!this.btnIconBasicOperate.Visible)
			{
				this.flowLayoutPanel1ICon.Controls.Remove(this.btnIconBasicOperate);
				this.btnIconBasicOperate.Dispose();
				this.shortcutConsole.Visible = false;
				this.shortcutSwipe.Visible = false;
			}
			if (!this.btnIconAttendance.Visible)
			{
				this.flowLayoutPanel1ICon.Controls.Remove(this.btnIconAttendance);
				this.btnIconAttendance.Dispose();
				this.shortcutAttendance.Visible = false;
			}
			this.flowLayoutPanel1ICon.Size = new Size(this.flowLayoutPanel1ICon.Width, 88 * this.flowLayoutPanel1ICon.Controls.Count);
			this.mnu1Tool.Visible = icOperator.OperatePrivilegeFullControl("mnu1Tool");
			this.cmdChangePasswor.Visible = icOperator.OperatePrivilegeFullControl("cmdChangePasswor");
			this.cmdOperatorManage.Visible = icOperator.OperatePrivilegeFullControl("cmdOperatorManage");
			this.mnuDBBackup.Visible = icOperator.OperatePrivilegeFullControl("mnuDBBackup");
			this.mnuExtendedFunction.Visible = icOperator.OperatePrivilegeFullControl("mnuExtendedFunction");
			this.mnuOption.Visible = icOperator.OperatePrivilegeFullControl("mnuOption");
			this.mnuLogQuery.Visible = (icOperator.OperatePrivilegeFullControl("mnuLogQuery") && wgAppConfig.getParamValBoolByNO(103));
			this.mnuPatrol.Visible = icOperator.OperatePrivilegeVisible("mnuPatrolDetailData");
			this.mnuMeal.Visible = icOperator.OperatePrivilegeVisible("mnuConstMeal");
			this.mnuMeetingSign.Visible = icOperator.OperatePrivilegeVisible("mnuMeeting");
			this.mnuElevator.Visible = icOperator.OperatePrivilegeVisible("mnuElevator");
			this.mnu1Help.Visible = icOperator.OperatePrivilegeVisible("mnu1Help");
			this.mnuAbout.Visible = icOperator.OperatePrivilegeVisible("mnuAbout");
			this.mnuManual.Visible = icOperator.OperatePrivilegeVisible("mnuManual");
			this.mnuSystemCharacteristic.Visible = icOperator.OperatePrivilegeVisible("mnuSystemCharacteristic");
			if (this.functionNameBasicConfigure[0, 1] == null)
			{
				this.shortcutControllers.Visible = false;
			}
			if (this.functionNameBasicConfigure[2, 1] == null)
			{
				this.shortcutPersonnel.Visible = false;
			}
			if (this.functionNameAccessControl[0, 1] == null)
			{
				this.shortcutPrivilege.Visible = false;
			}
			if (this.functionNameBasicOperate[0, 1] == null)
			{
				this.shortcutConsole.Visible = false;
			}
			if (this.functionNameBasicOperate[1, 1] == null)
			{
				this.shortcutSwipe.Visible = false;
			}
			if (this.functionNameAttendence[0, 1] == null)
			{
				this.shortcutAttendance.Visible = false;
			}
		}

		private void hideFuncItem(ref string[,] func, string funcName, bool bNotHide)
		{
			if (bNotHide)
			{
				return;
			}
			for (int i = 0; i < func.Length / 3; i++)
			{
				if (!string.IsNullOrEmpty(func[i, 1]) && func[i, 1] == funcName)
				{
					func[i, 1] = null;
					return;
				}
			}
		}

		private void hideMenuBySystemConfig()
		{
			this.mnuLogQuery.Visible = wgAppConfig.getParamValBoolByNO(103);
			this.btnIconAccessControl.Visible = !wgAppConfig.getParamValBoolByNO(111);
			if (this.btnIconAccessControl.Visible)
			{
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuControlSeg", wgAppConfig.getParamValBoolByNO(121));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuPasswordManagement", wgAppConfig.getParamValBoolByNO(123));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuPeripheral", wgAppConfig.getParamValBoolByNO(124));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuAntiBack", wgAppConfig.getParamValBoolByNO(132));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuInterLock", wgAppConfig.getParamValBoolByNO(133));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuMoreCards", wgAppConfig.getParamValBoolByNO(134));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuFirstCard", wgAppConfig.getParamValBoolByNO(135));
				this.hideFuncItem(ref this.functionNameAccessControl, "mnuTaskList", wgAppConfig.getParamValBoolByNO(131));
			}
			this.btnIconAttendance.Visible = !wgAppConfig.getParamValBoolByNO(112);
			if (this.btnIconAttendance.Visible)
			{
				this.hideFuncItem(ref this.functionNameAttendence, "mnuShiftArrange", wgAppConfig.getParamValBoolByNO(113));
				this.hideFuncItem(ref this.functionNameAttendence, "mnuShiftRule", wgAppConfig.getParamValBoolByNO(113));
				this.hideFuncItem(ref this.functionNameAttendence, "mnuShiftSet", wgAppConfig.getParamValBoolByNO(113));
			}
		}

		private void closeChildForm()
		{
			if (this.panel4Form.Controls.Count > 0)
			{
				(this.panel4Form.Controls[0] as Form).Close();
			}
			this.statRunInfo_Num_Update("");
			this.statRunInfo_CommStatus_Update("");
			this.statRunInfo_Monitor_Update("");
		}

		private void dispDfrm(Form dfrm)
		{
			this.closeChildForm();
			foreach (object current in this.flowLayoutPanel1ICon.Controls)
			{
				if (current is Button)
				{
					(current as Button).BackgroundImage = null;
					(current as Button).BackColor = Color.FromArgb(147, 150, 177);
				}
			}
			foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
			{
				toolStripButton.BackgroundImage = Resources.pMain_Bookmark_normal;
				toolStripButton.Visible = false;
			}
			this.btnIconSelected = null;
			this.btnBookmarkSelected = null;
			wgAppRunInfo.ClearAllDisplayedInfo();
			if (dfrm != null)
			{
				dfrm.ShowDialog(this);
			}
		}

		private void cmdChangePasswor_Click(object sender, EventArgs e)
		{
			this.dfrmSetPassword1 = new dfrmSetPassword();
			this.dfrmSetPassword1.Text = this.cmdChangePasswor.Text.Replace('&', ' ');
			this.dfrmSetPassword1.operatorID = icOperator.OperatorID;
			this.dispDfrm(this.dfrmSetPassword1);
		}

		private void cmdOperatorManage_Click(object sender, EventArgs e)
		{
			this.dfrmOperator1 = new dfrmOperator();
			this.dispDfrm(this.dfrmOperator1);
		}

		private void mnuDBBackup_Click(object sender, EventArgs e)
		{
			this.dfrmDbCompact1 = new dfrmDbCompact();
			this.dispDfrm(this.dfrmDbCompact1);
		}

		private void mnuExtendedFunction_Click(object sender, EventArgs e)
		{
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.setPasswordChar('*');
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				if (dfrmInputNewName.strNewName != "5678")
				{
					return;
				}
			}
			this.dispDfrm(null);
			using (dfrmExtendedFunctions dfrmExtendedFunctions = new dfrmExtendedFunctions())
			{
				if (dfrmExtendedFunctions.ShowDialog(this) == DialogResult.OK)
				{
					wgAppConfig.gRestart = true;
					this.mnuExit.PerformClick();
				}
			}
		}

		private void mnuTaskList_Click(object sender, EventArgs e)
		{
			this.dfrmControllerTaskList1 = new dfrmControllerTaskList();
			this.dispDfrm(this.dfrmControllerTaskList1);
		}

		private void mnuLogQuery_Click(object sender, EventArgs e)
		{
			this.dfrmLogQuery1 = new dfrmLogQuery();
			this.dispDfrm(this.dfrmLogQuery1);
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			this.dfrmAbout1 = new dfrmAbout();
			this.dfrmAbout1.Owner = this;
			this.dispDfrm(this.dfrmAbout1);
		}

		private void mnuManual_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = Environment.CurrentDirectory + "\\Readme.doc",
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void mnuSystemCharacteristic_Click(object sender, EventArgs e)
		{
			try
			{
				this.dispDfrm(null);
				string text = icOperator.PCSysInfo(false);
				XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				string eName = "";
				string value = "";
				wgAppConfig.setSystemParamValue(38, eName, value, text);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void frmADCT3000_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.bConfirmClose)
			{
				if (XMessageBox.Show(CommonStr.strExit + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				{
					e.Cancel = true;
					return;
				}
				this.closeChildForm();
				wgAppConfig.wgLog(this.mnuExit.Text, EventLogEntryType.Information, null);
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timer1.Enabled = false;
				this.statTimeDate.Text = DateTime.Now.ToString(wgTools.DisplayFormat_DateYMDHMSWeek);
			}
			catch (Exception)
			{
			}
			finally
			{
				this.timer1.Enabled = true;
			}
		}

		private void loadStbRunInfo()
		{
			if (icOperator.OperatorID == 1)
			{
				this.statOperator.Text = string.Format("{0}:{1}", CommonStr.strSuper, icOperator.OperatorName);
			}
			else
			{
				this.statOperator.Text = string.Format("{0}", icOperator.OperatorName);
			}
			if (wgAppConfig.IsAccessDB)
			{
				this.statSoftwareVer.Text = string.Format("{0} - Ver: {1}", "MsAccess", Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")));
			}
			else
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					this.statSoftwareVer.Text = string.Format("SQL: {0} - Ver: {1}", sqlConnection.Database, Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")));
				}
			}
			string arg = "BLUE";
			string productTypeOfApp;
			if ((productTypeOfApp = wgAppConfig.ProductTypeOfApp) != null)
			{
				if (!(productTypeOfApp == "Adroitor"))
				{
					if (!(productTypeOfApp == "WGACCESS"))
					{
						if (productTypeOfApp == "ADCT")
						{
							arg = "ADCT";
						}
					}
					else
					{
						arg = "WG";
					}
				}
				else
				{
					arg = "AT";
				}
			}
			this.statSoftwareVer.Text = this.statSoftwareVer.Text.Replace(" - Ver: ", string.Format(" -{0}- Ver: ", arg));
			string[] array = Application.ProductVersion.Split(new char[]
			{
				'.'
			});
			if (array.Length >= 4 && int.Parse(array[1]) % 2 == 0)
			{
				ToolStripStatusLabel expr_178 = this.statSoftwareVer;
				expr_178.Text = expr_178.Text + "." + array[3].ToString();
			}
			wgTools.CommPStr = wgTools.SetObjToStr(wgAppConfig.GetKeyVal("CommCurrent"));
			if (!string.IsNullOrEmpty(wgTools.CommPStr))
			{
				ToolStripStatusLabel expr_1BB = this.statSoftwareVer;
				expr_1BB.Text += ":!s";
			}
			this.statCOM.Text = "";
			this.statRuninfo1.Text = "";
			this.statRuninfo1.Spring = true;
			this.statRuninfo2.Text = "";
			this.statRuninfo3.Text = "";
			this.statRuninfo3.AutoSize = false;
			this.statRuninfo3.Width = 48;
			this.statRuninfoLoadedNum.Text = "";
			this.statRuninfoLoadedNum.AutoSize = false;
			this.statRuninfoLoadedNum.Width = 137;
			this.statTimeDate.Text = DateTime.Now.ToString(wgTools.YMDHMSFormat);
		}

		private void statRunInfo_Num_Update(string strLoadNum)
		{
			try
			{
				this.statRuninfoLoadedNum.Text = strLoadNum;
			}
			catch (Exception)
			{
			}
		}

		private void statRunInfo_CommStatus_Update(string strCommStatus)
		{
			try
			{
				this.statRuninfo1.Text = strCommStatus;
			}
			catch (Exception)
			{
			}
		}

		private void statRunInfo_Monitor_Update(string strMonitor)
		{
			try
			{
				if (strMonitor != null)
				{
					if (strMonitor == "0")
					{
						this.statRuninfo2.BackColor = Color.Transparent;
						this.statRuninfo2.Text = CommonStr.strMonitorStop;
						goto IL_80;
					}
					if (strMonitor == "1")
					{
						this.statRuninfo2.Text = CommonStr.strMonitoring;
						goto IL_80;
					}
					if (strMonitor == "2")
					{
						this.statRuninfo2.Text = CommonStr.strRealtimeGetting;
						goto IL_80;
					}
				}
				this.statRuninfo2.Text = strMonitor;
				IL_80:;
			}
			catch (Exception)
			{
			}
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			this.bConfirmClose = true;
			wgAppConfig.wgLog(this.mnuExit.Text, EventLogEntryType.Information, null);
			base.Close();
		}

		private void frmADCT3000_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Shift && e.KeyValue == 84)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.frmTestController1 = new frmTestController();
				this.frmTestController1.Owner = this;
				this.frmTestController1.Show();
			}
			if (!e.Control && !e.Shift && e.KeyValue == 112)
			{
				this.mnuManual.PerformClick();
			}
			if (e.Control && e.Shift && e.KeyValue == 78)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.dfrmNetControllerConfig1 = new dfrmNetControllerConfig();
				this.dfrmNetControllerConfig1.Show();
			}
			if (!e.Control && e.Shift && e.KeyValue == 118)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.mnuDeleteOldRecords_Click(null, null);
			}
			if (!e.Control && e.Shift && e.KeyValue == 119)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.systemParamsToolStripMenuItem_Click(null, null);
			}
			if (!e.Control && e.Shift && e.KeyValue == 123)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.systemParamsCustomTitle();
			}
			if (((e.Control && e.KeyValue == 70) || e.KeyValue == 114) && this.panel4Form.Controls.Count > 0)
			{
				try
				{
					Form form = this.panel4Form.Controls[0] as Form;
					if (this.btnBookmarkSelected != null)
					{
						if (this.btnBookmarkSelected.Tag.ToString().IndexOf(".frmControllers") > 0)
						{
							(form as frmControllers).frmControllers_KeyDown(form, e);
						}
						if (this.btnBookmarkSelected.Tag.ToString().IndexOf(".frmConsole") > 0)
						{
							(form as frmConsole).frmConsole_KeyDown(form, e);
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void mnuOption_Click(object sender, EventArgs e)
		{
			this.dispDfrm(null);
			using (dfrmOption dfrmOption = new dfrmOption())
			{
				if (dfrmOption.ShowDialog(this) == DialogResult.OK)
				{
					wgAppConfig.gRestart = true;
					this.mnuExit.PerformClick();
				}
			}
		}

		private void shortcutControllers_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnIconBasicConfig.PerformClick();
				foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
				{
					if (string.Compare(toolStripButton.Tag.ToString(), "WG3000_COMM.Basic.frmControllers") == 0)
					{
						toolStripButton.PerformClick();
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void shortcutPersonnel_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnIconBasicConfig.PerformClick();
				foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
				{
					if (string.Compare(toolStripButton.Tag.ToString(), "WG3000_COMM.Basic.frmUsers") == 0)
					{
						toolStripButton.PerformClick();
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void shortcutPrivilege_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnIconAccessControl.PerformClick();
				foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
				{
					if (string.Compare(toolStripButton.Tag.ToString(), "WG3000_COMM.Basic.frmPrivileges") == 0)
					{
						toolStripButton.PerformClick();
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void shortcutConsole_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnIconBasicOperate.PerformClick();
				foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
				{
					if (string.Compare(toolStripButton.Tag.ToString(), "WG3000_COMM.Basic.frmConsole") == 0)
					{
						toolStripButton.PerformClick();
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void shortcutSwipe_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnIconBasicOperate.PerformClick();
				foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
				{
					if (string.Compare(toolStripButton.Tag.ToString(), "WG3000_COMM.Basic.frmSwipeRecords") == 0)
					{
						toolStripButton.PerformClick();
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void shortcutAttendance_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnIconAttendance.PerformClick();
				foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
				{
					if (string.Compare(toolStripButton.Tag.ToString(), "WG3000_COMM.Reports.Shift.frmShiftAttReport") == 0)
					{
						toolStripButton.PerformClick();
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void mnuInterfaceLock_Click(object sender, EventArgs e)
		{
			using (dfrmInterfaceLock dfrmInterfaceLock = new dfrmInterfaceLock())
			{
				dfrmInterfaceLock.txtOperatorName.Text = icOperator.OperatorName;
				dfrmInterfaceLock.StartPosition = FormStartPosition.CenterScreen;
				dfrmInterfaceLock.ShowDialog(this);
			}
		}

		private void mnuDeleteOldRecords_Click(object sender, EventArgs e)
		{
			if (icOperator.OperatorID != 1)
			{
				XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.setPasswordChar('*');
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				if (dfrmInputNewName.strNewName != "5678")
				{
					return;
				}
			}
			using (dfrmDeleteRecords dfrmDeleteRecords = new dfrmDeleteRecords())
			{
				dfrmDeleteRecords.ShowDialog(this);
			}
		}

		private void mnuPCCheckAccessConfigure_Click(object sender, EventArgs e)
		{
			this.dfrmCheckAccessConfigure1 = new dfrmCheckAccessConfigure();
			this.dispDfrm(this.dfrmCheckAccessConfigure1);
		}

		private void systemParamsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (icOperator.OperatorID != 1)
			{
				XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.setPasswordChar('*');
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				if (dfrmInputNewName.strNewName != "5678")
				{
					return;
				}
			}
			using (dfrmSystemParam dfrmSystemParam = new dfrmSystemParam())
			{
				if (dfrmSystemParam.ShowDialog(this) == DialogResult.OK)
				{
					wgAppConfig.gRestart = true;
					this.mnuExit.PerformClick();
				}
			}
		}

		private void mnuElevator_Click(object sender, EventArgs e)
		{
			this.dispInPanel4(new frmUsers4Elevator
			{
				Text = this.mnuElevator.Text
			});
		}

		private void systemParamsCustomTitle()
		{
			if (icOperator.OperatorID != 1)
			{
				XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.setPasswordChar('*');
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				if (dfrmInputNewName.strNewName != "5678")
				{
					return;
				}
			}
			using (dfrmInputNewName dfrmInputNewName2 = new dfrmInputNewName())
			{
				dfrmInputNewName2.Text = CommonStr.strNewTitle;
				dfrmInputNewName2.bNotAllowNull = false;
				if (dfrmInputNewName2.ShowDialog(this) == DialogResult.OK)
				{
					if (wgAppConfig.setSystemParamValue(17, "", wgTools.SetObjToStr(dfrmInputNewName2.strNewName).Trim(), "") > 0)
					{
						if (wgAppConfig.getSystemParamByName("Custom Title") != "")
						{
							this.Text = wgAppConfig.getSystemParamByName("Custom Title");
							this.oldTitle = this.Text;
						}
						else
						{
							this.Text = this.defaultTitle;
						}
					}
				}
			}
		}

		private void btnAddController_Click(object sender, EventArgs e)
		{
			this.dispDfrm(null);
			using (dfrmNetControllerConfig dfrmNetControllerConfig = new dfrmNetControllerConfig())
			{
				dfrmNetControllerConfig.ShowDialog(this);
			}
		}

		private void btnAutoAddCardBySwiping_Click(object sender, EventArgs e)
		{
			this.dispDfrm(null);
			try
			{
				using (dfrmUserAutoAdd dfrmUserAutoAdd = new dfrmUserAutoAdd())
				{
					dfrmUserAutoAdd.bAutoAddBySwiping = true;
					dfrmUserAutoAdd.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnAddPrivilege_Click(object sender, EventArgs e)
		{
			this.dispDfrm(null);
			using (dfrmPrivilege dfrmPrivilege = new dfrmPrivilege())
			{
				dfrmPrivilege.ShowDialog(this);
			}
		}

		private void btnHideGettingStarted_Click(object sender, EventArgs e)
		{
			this.grpGettingStarted.Visible = false;
			this.flowLayoutPanel1ICon.Size = new Size(127, 338);
		}

		private void toolStripMenuItem20_Click(object sender, EventArgs e)
		{
			this.grpGettingStarted.Visible = true;
			this.flowLayoutPanel1ICon.Size = new Size(127, 338);
		}

		private void chkHideLogin_CheckedChanged(object sender, EventArgs e)
		{
			if (wgAppConfig.GetKeyVal("HideGettingStartedWhenLogin") != "1")
			{
				if (this.chkHideLogin.Checked && this.bDisplayHideLogin)
				{
					XMessageBox.Show(CommonStr.strDisplayHideLogin);
				}
				this.bDisplayHideLogin = false;
			}
			wgAppConfig.UpdateKeyVal("HideGettingStartedWhenLogin", this.chkHideLogin.Checked ? "1" : "0");
		}

		private void mnuDoorAsSwitch_Click(object sender, EventArgs e)
		{
			this.dispDfrm(null);
			using (dfrmDoorAsSwitch dfrmDoorAsSwitch = new dfrmDoorAsSwitch())
			{
				dfrmDoorAsSwitch.ShowDialog();
			}
		}

		private void cmdEditOperator_Click(object sender, EventArgs e)
		{
			using (dfrmOperatorUpdate dfrmOperatorUpdate = new dfrmOperatorUpdate())
			{
				dfrmOperatorUpdate.operateMode = 1;
				dfrmOperatorUpdate.operatorID = icOperator.OperatorID;
				dfrmOperatorUpdate.operatorName = icOperator.OperatorName;
				dfrmOperatorUpdate.ShowDialog(this);
			}
		}

		private void mnuMeetingSign_Click(object sender, EventArgs e)
		{
			this.dispDfrm(null);
			using (frmMeetings frmMeetings = new frmMeetings())
			{
				frmMeetings.ShowDialog();
			}
		}

		private void mnuMeal_Click(object sender, EventArgs e)
		{
			this.dispInPanel4(new frmMeal());
		}

		private void mnuPatrol_Click(object sender, EventArgs e)
		{
			this.dispInPanel4(new frmPatrolReport());
		}

		private void dispInPanel4(Form frm)
		{
			this.closeChildForm();
			foreach (object current in this.flowLayoutPanel1ICon.Controls)
			{
				if (current is Button)
				{
					(current as Button).BackgroundImage = null;
					(current as Button).BackColor = Color.FromArgb(147, 150, 177);
				}
			}
			foreach (ToolStripButton toolStripButton in this.toolStrip1BookMark.Items)
			{
				toolStripButton.BackgroundImage = Resources.pMain_Bookmark_normal;
				toolStripButton.Visible = false;
			}
			this.btnIconSelected = null;
			this.btnBookmarkSelected = this.toolStripButtonBookmark1;
			frm.ShowDialog();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmADCT3000));
			this.contextMenuStrip1Tools = new ContextMenuStrip(this.components);
			this.cmdChangePasswor = new ToolStripMenuItem();
			this.cmdEditOperator = new ToolStripMenuItem();
			this.cmdOperatorManage = new ToolStripMenuItem();
			this.mnuDBBackup = new ToolStripMenuItem();
			this.mnuOption = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.mnuExtendedFunction = new ToolStripMenuItem();
			this.toolStripMenuItem23 = new ToolStripMenuItem();
			this.mnuElevator = new ToolStripMenuItem();
			this.mnuMeetingSign = new ToolStripMenuItem();
			this.mnuMeal = new ToolStripMenuItem();
			this.mnuPatrol = new ToolStripMenuItem();
			this.mnuPCCheckAccessConfigure = new ToolStripMenuItem();
			this.mnuTaskList = new ToolStripMenuItem();
			this.mnuDoorAsSwitch = new ToolStripMenuItem();
			this.mnuLogQuery = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.mnuInterfaceLock = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.mnuExit = new ToolStripMenuItem();
			this.toolStripDropDownButton1 = new ToolStripDropDownButton();
			this.mnu1Tool = new ToolStripMenuItem();
			this.mnuDeleteOldRecords = new ToolStripMenuItem();
			this.systemParamsToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem20 = new ToolStripMenuItem();
			this.toolStripMenuItem19 = new ToolStripMenuItem();
			this.mnuAbout = new ToolStripMenuItem();
			this.mnuManual = new ToolStripMenuItem();
			this.mnuSystemCharacteristic = new ToolStripMenuItem();
			this.contextMenuStrip2Help = new ContextMenuStrip(this.components);
			this.mnu1Help = new ToolStripDropDownButton();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripMenuItem();
			this.toolStripMenuItem6 = new ToolStripMenuItem();
			this.toolStripMenuItem7 = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.toolStripMenuItem8 = new ToolStripMenuItem();
			this.toolStripMenuItem9 = new ToolStripMenuItem();
			this.toolStripMenuItem10 = new ToolStripMenuItem();
			this.toolStripMenuItem11 = new ToolStripMenuItem();
			this.toolStripMenuItem12 = new ToolStripMenuItem();
			this.toolStripMenuItem13 = new ToolStripMenuItem();
			this.toolStripMenuItem14 = new ToolStripMenuItem();
			this.toolStripMenuItem15 = new ToolStripMenuItem();
			this.toolStripMenuItem16 = new ToolStripMenuItem();
			this.toolStripMenuItem17 = new ToolStripMenuItem();
			this.toolStripMenuItem18 = new ToolStripMenuItem();
			this.toolsFormToolStripMenuItem = new ToolStripMenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolStrip1BookMark = new ToolStrip();
			this.contextMenuStrip3Normal = new ContextMenuStrip(this.components);
			this.shortcutControllers = new ToolStripMenuItem();
			this.shortcutPersonnel = new ToolStripMenuItem();
			this.shortcutPrivilege = new ToolStripMenuItem();
			this.shortcutConsole = new ToolStripMenuItem();
			this.shortcutSwipe = new ToolStripMenuItem();
			this.shortcutAttendance = new ToolStripMenuItem();
			this.toolStripButtonBookmark1 = new ToolStripButton();
			this.toolStripButtonBookmark2 = new ToolStripButton();
			this.toolStripButton4 = new ToolStripButton();
			this.toolStripButton3 = new ToolStripButton();
			this.toolStripButton2 = new ToolStripButton();
			this.toolStripButton1 = new ToolStripButton();
			this.toolStripButton5 = new ToolStripButton();
			this.toolStripButton7 = new ToolStripButton();
			this.toolStripButton6 = new ToolStripButton();
			this.toolStripButtonBookmark3 = new ToolStripButton();
			this.flowLayoutPanel1ICon = new FlowLayoutPanel();
			this.grpGettingStarted = new GroupBox();
			this.btnHideGettingStarted = new Button();
			this.label1 = new Label();
			this.btnAddPrivilege = new Button();
			this.btnAutoAddCardBySwiping = new Button();
			this.btnAddController = new Button();
			this.chkHideLogin = new CheckBox();
			this.btnIconBasicConfig = new Button();
			this.btnIconAccessControl = new Button();
			this.btnIconBasicOperate = new Button();
			this.btnIconAttendance = new Button();
			this.panel2Content = new Panel();
			this.stbRunInfo = new StatusStrip();
			this.statOperator = new ToolStripStatusLabel();
			this.statSoftwareVer = new ToolStripStatusLabel();
			this.statCOM = new ToolStripStatusLabel();
			this.statRuninfo1 = new ToolStripStatusLabel();
			this.statRuninfo2 = new ToolStripStatusLabel();
			this.statRuninfo3 = new ToolStripStatusLabel();
			this.statRuninfoLoadedNum = new ToolStripStatusLabel();
			this.statTimeDate = new ToolStripStatusLabel();
			this.panel1 = new Panel();
			this.panel4Form = new PictureBox();
			this.toolTip1 = new ToolTip(this.components);
			this.contextMenuStrip1Tools.SuspendLayout();
			this.contextMenuStrip2Help.SuspendLayout();
			this.toolStrip1BookMark.SuspendLayout();
			this.contextMenuStrip3Normal.SuspendLayout();
			this.flowLayoutPanel1ICon.SuspendLayout();
			this.grpGettingStarted.SuspendLayout();
			this.panel2Content.SuspendLayout();
			this.stbRunInfo.SuspendLayout();
			((ISupportInitialize)this.panel4Form).BeginInit();
			base.SuspendLayout();
			this.contextMenuStrip1Tools.Items.AddRange(new ToolStripItem[]
			{
				this.cmdChangePasswor,
				this.cmdEditOperator,
				this.cmdOperatorManage,
				this.mnuDBBackup,
				this.mnuOption,
				this.toolStripSeparator1,
				this.mnuExtendedFunction,
				this.toolStripMenuItem23,
				this.mnuElevator,
				this.mnuMeetingSign,
				this.mnuMeal,
				this.mnuPatrol,
				this.mnuPCCheckAccessConfigure,
				this.mnuTaskList,
				this.mnuDoorAsSwitch,
				this.mnuLogQuery,
				this.toolStripSeparator4,
				this.mnuInterfaceLock,
				this.toolStripSeparator2,
				this.mnuExit
			});
			this.contextMenuStrip1Tools.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1Tools, "contextMenuStrip1Tools");
			this.cmdChangePasswor.Name = "cmdChangePasswor";
			componentResourceManager.ApplyResources(this.cmdChangePasswor, "cmdChangePasswor");
			this.cmdChangePasswor.Click += new EventHandler(this.cmdChangePasswor_Click);
			this.cmdEditOperator.Name = "cmdEditOperator";
			componentResourceManager.ApplyResources(this.cmdEditOperator, "cmdEditOperator");
			this.cmdEditOperator.Click += new EventHandler(this.cmdEditOperator_Click);
			this.cmdOperatorManage.Name = "cmdOperatorManage";
			componentResourceManager.ApplyResources(this.cmdOperatorManage, "cmdOperatorManage");
			this.cmdOperatorManage.Click += new EventHandler(this.cmdOperatorManage_Click);
			this.mnuDBBackup.Name = "mnuDBBackup";
			componentResourceManager.ApplyResources(this.mnuDBBackup, "mnuDBBackup");
			this.mnuDBBackup.Click += new EventHandler(this.mnuDBBackup_Click);
			this.mnuOption.Name = "mnuOption";
			componentResourceManager.ApplyResources(this.mnuOption, "mnuOption");
			this.mnuOption.Click += new EventHandler(this.mnuOption_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			componentResourceManager.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.mnuExtendedFunction.Name = "mnuExtendedFunction";
			componentResourceManager.ApplyResources(this.mnuExtendedFunction, "mnuExtendedFunction");
			this.mnuExtendedFunction.Click += new EventHandler(this.mnuExtendedFunction_Click);
			this.toolStripMenuItem23.Name = "toolStripMenuItem23";
			componentResourceManager.ApplyResources(this.toolStripMenuItem23, "toolStripMenuItem23");
			this.mnuElevator.Name = "mnuElevator";
			componentResourceManager.ApplyResources(this.mnuElevator, "mnuElevator");
			this.mnuElevator.Click += new EventHandler(this.mnuElevator_Click);
			this.mnuMeetingSign.Name = "mnuMeetingSign";
			componentResourceManager.ApplyResources(this.mnuMeetingSign, "mnuMeetingSign");
			this.mnuMeetingSign.Click += new EventHandler(this.mnuMeetingSign_Click);
			this.mnuMeal.Name = "mnuMeal";
			componentResourceManager.ApplyResources(this.mnuMeal, "mnuMeal");
			this.mnuMeal.Click += new EventHandler(this.mnuMeal_Click);
			this.mnuPatrol.Name = "mnuPatrol";
			componentResourceManager.ApplyResources(this.mnuPatrol, "mnuPatrol");
			this.mnuPatrol.Click += new EventHandler(this.mnuPatrol_Click);
			this.mnuPCCheckAccessConfigure.Name = "mnuPCCheckAccessConfigure";
			componentResourceManager.ApplyResources(this.mnuPCCheckAccessConfigure, "mnuPCCheckAccessConfigure");
			this.mnuPCCheckAccessConfigure.Click += new EventHandler(this.mnuPCCheckAccessConfigure_Click);
			this.mnuTaskList.Name = "mnuTaskList";
			componentResourceManager.ApplyResources(this.mnuTaskList, "mnuTaskList");
			this.mnuTaskList.Click += new EventHandler(this.mnuTaskList_Click);
			this.mnuDoorAsSwitch.Name = "mnuDoorAsSwitch";
			componentResourceManager.ApplyResources(this.mnuDoorAsSwitch, "mnuDoorAsSwitch");
			this.mnuDoorAsSwitch.Click += new EventHandler(this.mnuDoorAsSwitch_Click);
			this.mnuLogQuery.Name = "mnuLogQuery";
			componentResourceManager.ApplyResources(this.mnuLogQuery, "mnuLogQuery");
			this.mnuLogQuery.Click += new EventHandler(this.mnuLogQuery_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			componentResourceManager.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			this.mnuInterfaceLock.Name = "mnuInterfaceLock";
			componentResourceManager.ApplyResources(this.mnuInterfaceLock, "mnuInterfaceLock");
			this.mnuInterfaceLock.Click += new EventHandler(this.mnuInterfaceLock_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			componentResourceManager.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			this.mnuExit.Name = "mnuExit";
			componentResourceManager.ApplyResources(this.mnuExit, "mnuExit");
			this.mnuExit.Click += new EventHandler(this.mnuExit_Click);
			this.toolStripDropDownButton1.BackColor = Color.Transparent;
			this.toolStripDropDownButton1.DropDown = this.contextMenuStrip1Tools;
			this.toolStripDropDownButton1.ForeColor = Color.White;
			this.toolStripDropDownButton1.Image = Resources.pMain_tool;
			componentResourceManager.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
			this.toolStripDropDownButton1.Margin = new Padding(21, 2, 0, 0);
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Padding = new Padding(15, 0, 0, 0);
			this.mnu1Tool.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.mnuDeleteOldRecords,
				this.systemParamsToolStripMenuItem
			});
			this.mnu1Tool.Name = "mnu1Tool";
			componentResourceManager.ApplyResources(this.mnu1Tool, "mnu1Tool");
			this.mnuDeleteOldRecords.Name = "mnuDeleteOldRecords";
			componentResourceManager.ApplyResources(this.mnuDeleteOldRecords, "mnuDeleteOldRecords");
			this.mnuDeleteOldRecords.Click += new EventHandler(this.mnuDeleteOldRecords_Click);
			this.systemParamsToolStripMenuItem.Name = "systemParamsToolStripMenuItem";
			componentResourceManager.ApplyResources(this.systemParamsToolStripMenuItem, "systemParamsToolStripMenuItem");
			this.systemParamsToolStripMenuItem.Click += new EventHandler(this.systemParamsToolStripMenuItem_Click);
			this.toolStripMenuItem20.Name = "toolStripMenuItem20";
			componentResourceManager.ApplyResources(this.toolStripMenuItem20, "toolStripMenuItem20");
			this.toolStripMenuItem20.Click += new EventHandler(this.toolStripMenuItem20_Click);
			this.toolStripMenuItem19.Name = "toolStripMenuItem19";
			componentResourceManager.ApplyResources(this.toolStripMenuItem19, "toolStripMenuItem19");
			this.mnuAbout.Name = "mnuAbout";
			componentResourceManager.ApplyResources(this.mnuAbout, "mnuAbout");
			this.mnuAbout.Click += new EventHandler(this.mnuAbout_Click);
			this.mnuManual.Name = "mnuManual";
			componentResourceManager.ApplyResources(this.mnuManual, "mnuManual");
			this.mnuManual.Click += new EventHandler(this.mnuManual_Click);
			this.mnuSystemCharacteristic.Name = "mnuSystemCharacteristic";
			componentResourceManager.ApplyResources(this.mnuSystemCharacteristic, "mnuSystemCharacteristic");
			this.mnuSystemCharacteristic.Click += new EventHandler(this.mnuSystemCharacteristic_Click);
			this.contextMenuStrip2Help.Items.AddRange(new ToolStripItem[]
			{
				this.mnuAbout,
				this.mnuManual,
				this.mnuSystemCharacteristic,
				this.toolStripMenuItem20
			});
			this.contextMenuStrip2Help.Name = "contextMenuStrip2Help";
			this.contextMenuStrip2Help.OwnerItem = this.mnu1Help;
			componentResourceManager.ApplyResources(this.contextMenuStrip2Help, "contextMenuStrip2Help");
			this.mnu1Help.BackColor = Color.Transparent;
			this.mnu1Help.DropDown = this.contextMenuStrip2Help;
			this.mnu1Help.ForeColor = Color.White;
			this.mnu1Help.Image = Resources.pMain_help;
			componentResourceManager.ApplyResources(this.mnu1Help, "mnu1Help");
			this.mnu1Help.Name = "mnu1Help";
			this.toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem2,
				this.toolStripMenuItem3,
				this.toolStripMenuItem4,
				this.toolStripMenuItem5,
				this.toolStripMenuItem6,
				this.toolStripMenuItem7,
				this.toolStripSeparator5,
				this.toolStripMenuItem8,
				this.toolStripMenuItem9,
				this.toolStripMenuItem10,
				this.toolStripMenuItem11,
				this.toolStripMenuItem12,
				this.toolStripMenuItem13
			});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			componentResourceManager.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			componentResourceManager.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			componentResourceManager.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			componentResourceManager.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			componentResourceManager.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			componentResourceManager.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			componentResourceManager.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			componentResourceManager.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			componentResourceManager.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			componentResourceManager.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			componentResourceManager.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			componentResourceManager.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			componentResourceManager.ApplyResources(this.toolStripMenuItem12, "toolStripMenuItem12");
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			componentResourceManager.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
			this.toolStripMenuItem14.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem15,
				this.toolStripMenuItem16,
				this.toolStripMenuItem17,
				this.toolStripMenuItem18,
				this.toolsFormToolStripMenuItem
			});
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			componentResourceManager.ApplyResources(this.toolStripMenuItem14, "toolStripMenuItem14");
			this.toolStripMenuItem15.Name = "toolStripMenuItem15";
			componentResourceManager.ApplyResources(this.toolStripMenuItem15, "toolStripMenuItem15");
			this.toolStripMenuItem16.Name = "toolStripMenuItem16";
			componentResourceManager.ApplyResources(this.toolStripMenuItem16, "toolStripMenuItem16");
			this.toolStripMenuItem17.Name = "toolStripMenuItem17";
			componentResourceManager.ApplyResources(this.toolStripMenuItem17, "toolStripMenuItem17");
			this.toolStripMenuItem18.Name = "toolStripMenuItem18";
			componentResourceManager.ApplyResources(this.toolStripMenuItem18, "toolStripMenuItem18");
			this.toolsFormToolStripMenuItem.Name = "toolsFormToolStripMenuItem";
			componentResourceManager.ApplyResources(this.toolsFormToolStripMenuItem, "toolsFormToolStripMenuItem");
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.toolStrip1BookMark, "toolStrip1BookMark");
			this.toolStrip1BookMark.BackColor = Color.Transparent;
			this.toolStrip1BookMark.BackgroundImage = Resources.pMain_Bookmark_bkg;
			this.toolStrip1BookMark.ContextMenuStrip = this.contextMenuStrip3Normal;
			this.toolStrip1BookMark.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1BookMark.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripButtonBookmark1,
				this.toolStripButtonBookmark2,
				this.toolStripButton4,
				this.toolStripButton3,
				this.toolStripButton2,
				this.toolStripButton1,
				this.toolStripButton5,
				this.toolStripButton7,
				this.toolStripButton6,
				this.toolStripButtonBookmark3
			});
			this.toolStrip1BookMark.Name = "toolStrip1BookMark";
			this.toolStrip1BookMark.RenderMode = ToolStripRenderMode.Professional;
			this.toolTip1.SetToolTip(this.toolStrip1BookMark, componentResourceManager.GetString("toolStrip1BookMark.ToolTip"));
			this.toolStrip1BookMark.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1BookMark_ItemClicked);
			this.contextMenuStrip3Normal.Items.AddRange(new ToolStripItem[]
			{
				this.shortcutControllers,
				this.shortcutPersonnel,
				this.shortcutPrivilege,
				this.shortcutConsole,
				this.shortcutSwipe,
				this.shortcutAttendance
			});
			this.contextMenuStrip3Normal.Name = "contextMenuStrip3Normal";
			componentResourceManager.ApplyResources(this.contextMenuStrip3Normal, "contextMenuStrip3Normal");
			this.shortcutControllers.Name = "shortcutControllers";
			componentResourceManager.ApplyResources(this.shortcutControllers, "shortcutControllers");
			this.shortcutControllers.Click += new EventHandler(this.shortcutControllers_Click);
			this.shortcutPersonnel.Name = "shortcutPersonnel";
			componentResourceManager.ApplyResources(this.shortcutPersonnel, "shortcutPersonnel");
			this.shortcutPersonnel.Click += new EventHandler(this.shortcutPersonnel_Click);
			this.shortcutPrivilege.Name = "shortcutPrivilege";
			componentResourceManager.ApplyResources(this.shortcutPrivilege, "shortcutPrivilege");
			this.shortcutPrivilege.Click += new EventHandler(this.shortcutPrivilege_Click);
			this.shortcutConsole.Name = "shortcutConsole";
			componentResourceManager.ApplyResources(this.shortcutConsole, "shortcutConsole");
			this.shortcutConsole.Click += new EventHandler(this.shortcutConsole_Click);
			this.shortcutSwipe.Name = "shortcutSwipe";
			componentResourceManager.ApplyResources(this.shortcutSwipe, "shortcutSwipe");
			this.shortcutSwipe.Click += new EventHandler(this.shortcutSwipe_Click);
			this.shortcutAttendance.Name = "shortcutAttendance";
			componentResourceManager.ApplyResources(this.shortcutAttendance, "shortcutAttendance");
			this.shortcutAttendance.Click += new EventHandler(this.shortcutAttendance_Click);
			this.toolStripButtonBookmark1.BackColor = Color.Transparent;
			this.toolStripButtonBookmark1.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButtonBookmark1, "toolStripButtonBookmark1");
			this.toolStripButtonBookmark1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButtonBookmark1.ForeColor = Color.White;
			this.toolStripButtonBookmark1.Margin = new Padding(15, 6, 0, 6);
			this.toolStripButtonBookmark1.Name = "toolStripButtonBookmark1";
			this.toolStripButtonBookmark1.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButtonBookmark1.Tag = "WG3000_COMM.Basic.frmControllers";
			this.toolStripButtonBookmark1.Click += new EventHandler(this.toolStripButtonBookmark1_Click);
			this.toolStripButtonBookmark2.BackColor = Color.Transparent;
			this.toolStripButtonBookmark2.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButtonBookmark2, "toolStripButtonBookmark2");
			this.toolStripButtonBookmark2.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButtonBookmark2.ForeColor = Color.White;
			this.toolStripButtonBookmark2.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButtonBookmark2.Name = "toolStripButtonBookmark2";
			this.toolStripButtonBookmark2.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButtonBookmark2.Tag = "WG3000_COMM.Basic.frmDepartments";
			this.toolStripButton4.BackColor = Color.Transparent;
			this.toolStripButton4.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton4, "toolStripButton4");
			this.toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton4.ForeColor = Color.White;
			this.toolStripButton4.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton4.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButton3.BackColor = Color.Transparent;
			this.toolStripButton3.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton3, "toolStripButton3");
			this.toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton3.ForeColor = Color.White;
			this.toolStripButton3.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton3.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButton2.BackColor = Color.Transparent;
			this.toolStripButton2.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton2, "toolStripButton2");
			this.toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton2.ForeColor = Color.White;
			this.toolStripButton2.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton2.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButton1.BackColor = Color.Transparent;
			this.toolStripButton1.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton1, "toolStripButton1");
			this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.ForeColor = Color.White;
			this.toolStripButton1.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton1.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButton5.BackColor = Color.Transparent;
			this.toolStripButton5.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton5, "toolStripButton5");
			this.toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton5.ForeColor = Color.White;
			this.toolStripButton5.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton5.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButton7.BackColor = Color.Transparent;
			this.toolStripButton7.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton7, "toolStripButton7");
			this.toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton7.ForeColor = Color.White;
			this.toolStripButton7.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton7.Name = "toolStripButton7";
			this.toolStripButton7.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton7.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButton6.BackColor = Color.Transparent;
			this.toolStripButton6.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButton6, "toolStripButton6");
			this.toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton6.ForeColor = Color.White;
			this.toolStripButton6.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButton6.Tag = "WG3000_COMM.Basic.frmUsers";
			this.toolStripButtonBookmark3.BackColor = Color.Transparent;
			this.toolStripButtonBookmark3.BackgroundImage = Resources.pMain_Bookmark_normal;
			componentResourceManager.ApplyResources(this.toolStripButtonBookmark3, "toolStripButtonBookmark3");
			this.toolStripButtonBookmark3.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButtonBookmark3.ForeColor = Color.White;
			this.toolStripButtonBookmark3.Margin = new Padding(14, 6, 0, 6);
			this.toolStripButtonBookmark3.Name = "toolStripButtonBookmark3";
			this.toolStripButtonBookmark3.Padding = new Padding(8, 0, 8, 0);
			this.toolStripButtonBookmark3.Tag = "WG3000_COMM.Basic.frmUsers";
			componentResourceManager.ApplyResources(this.flowLayoutPanel1ICon, "flowLayoutPanel1ICon");
			this.flowLayoutPanel1ICon.BackColor = Color.Transparent;
			this.flowLayoutPanel1ICon.BackgroundImage = Resources.pMain_icon_bkg;
			this.flowLayoutPanel1ICon.Controls.Add(this.grpGettingStarted);
			this.flowLayoutPanel1ICon.Controls.Add(this.btnIconBasicConfig);
			this.flowLayoutPanel1ICon.Controls.Add(this.btnIconAccessControl);
			this.flowLayoutPanel1ICon.Controls.Add(this.btnIconBasicOperate);
			this.flowLayoutPanel1ICon.Controls.Add(this.btnIconAttendance);
			this.flowLayoutPanel1ICon.Name = "flowLayoutPanel1ICon";
			this.grpGettingStarted.BackColor = Color.FromArgb(147, 150, 177);
			this.grpGettingStarted.Controls.Add(this.btnHideGettingStarted);
			this.grpGettingStarted.Controls.Add(this.label1);
			this.grpGettingStarted.Controls.Add(this.btnAddPrivilege);
			this.grpGettingStarted.Controls.Add(this.btnAutoAddCardBySwiping);
			this.grpGettingStarted.Controls.Add(this.btnAddController);
			this.grpGettingStarted.Controls.Add(this.chkHideLogin);
			this.grpGettingStarted.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.grpGettingStarted, "grpGettingStarted");
			this.grpGettingStarted.Name = "grpGettingStarted";
			this.grpGettingStarted.TabStop = false;
			this.btnHideGettingStarted.BackColor = Color.FromArgb(117, 121, 155);
			componentResourceManager.ApplyResources(this.btnHideGettingStarted, "btnHideGettingStarted");
			this.btnHideGettingStarted.Name = "btnHideGettingStarted";
			this.toolTip1.SetToolTip(this.btnHideGettingStarted, componentResourceManager.GetString("btnHideGettingStarted.ToolTip"));
			this.btnHideGettingStarted.UseVisualStyleBackColor = false;
			this.btnHideGettingStarted.Click += new EventHandler(this.btnHideGettingStarted_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.btnAddPrivilege, "btnAddPrivilege");
			this.btnAddPrivilege.BackColor = Color.FromArgb(117, 121, 155);
			this.btnAddPrivilege.Name = "btnAddPrivilege";
			this.toolTip1.SetToolTip(this.btnAddPrivilege, componentResourceManager.GetString("btnAddPrivilege.ToolTip"));
			this.btnAddPrivilege.UseVisualStyleBackColor = false;
			this.btnAddPrivilege.Click += new EventHandler(this.btnAddPrivilege_Click);
			componentResourceManager.ApplyResources(this.btnAutoAddCardBySwiping, "btnAutoAddCardBySwiping");
			this.btnAutoAddCardBySwiping.BackColor = Color.FromArgb(117, 121, 155);
			this.btnAutoAddCardBySwiping.Name = "btnAutoAddCardBySwiping";
			this.toolTip1.SetToolTip(this.btnAutoAddCardBySwiping, componentResourceManager.GetString("btnAutoAddCardBySwiping.ToolTip"));
			this.btnAutoAddCardBySwiping.UseVisualStyleBackColor = false;
			this.btnAutoAddCardBySwiping.Click += new EventHandler(this.btnAutoAddCardBySwiping_Click);
			componentResourceManager.ApplyResources(this.btnAddController, "btnAddController");
			this.btnAddController.BackColor = Color.FromArgb(117, 121, 155);
			this.btnAddController.Name = "btnAddController";
			this.toolTip1.SetToolTip(this.btnAddController, componentResourceManager.GetString("btnAddController.ToolTip"));
			this.btnAddController.UseVisualStyleBackColor = false;
			this.btnAddController.Click += new EventHandler(this.btnAddController_Click);
			componentResourceManager.ApplyResources(this.chkHideLogin, "chkHideLogin");
			this.chkHideLogin.Name = "chkHideLogin";
			this.chkHideLogin.UseVisualStyleBackColor = true;
			this.chkHideLogin.CheckedChanged += new EventHandler(this.chkHideLogin_CheckedChanged);
			this.btnIconBasicConfig.BackgroundImage = Resources.pMain_icon_focus02;
			componentResourceManager.ApplyResources(this.btnIconBasicConfig, "btnIconBasicConfig");
			this.btnIconBasicConfig.ForeColor = Color.White;
			this.btnIconBasicConfig.Image = Resources.pMain_BasicConfigure;
			this.btnIconBasicConfig.Name = "btnIconBasicConfig";
			this.btnIconBasicConfig.Tag = "BasciConfig";
			this.btnIconBasicConfig.UseVisualStyleBackColor = false;
			this.btnIconBasicConfig.Click += new EventHandler(this.btnIconBasicConfig_Click);
			this.btnIconAccessControl.BackColor = Color.FromArgb(147, 150, 177);
			componentResourceManager.ApplyResources(this.btnIconAccessControl, "btnIconAccessControl");
			this.btnIconAccessControl.ForeColor = Color.White;
			this.btnIconAccessControl.Image = Resources.pMain_AccessControl;
			this.btnIconAccessControl.Name = "btnIconAccessControl";
			this.btnIconAccessControl.Tag = "AccessControl";
			this.btnIconAccessControl.UseVisualStyleBackColor = false;
			this.btnIconAccessControl.Click += new EventHandler(this.btnIconBasicConfig_Click);
			this.btnIconBasicOperate.BackColor = Color.FromArgb(147, 150, 177);
			componentResourceManager.ApplyResources(this.btnIconBasicOperate, "btnIconBasicOperate");
			this.btnIconBasicOperate.ForeColor = Color.White;
			this.btnIconBasicOperate.Image = Resources.pMain_BasicOperate;
			this.btnIconBasicOperate.Name = "btnIconBasicOperate";
			this.btnIconBasicOperate.Tag = "BasicOperate";
			this.btnIconBasicOperate.UseVisualStyleBackColor = false;
			this.btnIconBasicOperate.Click += new EventHandler(this.btnIconBasicConfig_Click);
			this.btnIconAttendance.BackColor = Color.FromArgb(147, 150, 177);
			componentResourceManager.ApplyResources(this.btnIconAttendance, "btnIconAttendance");
			this.btnIconAttendance.ForeColor = Color.White;
			this.btnIconAttendance.Image = Resources.pMain_Attendance;
			this.btnIconAttendance.Name = "btnIconAttendance";
			this.btnIconAttendance.Tag = "Attendance";
			this.btnIconAttendance.UseVisualStyleBackColor = false;
			this.btnIconAttendance.Click += new EventHandler(this.btnIconBasicConfig_Click);
			this.panel2Content.BackColor = Color.FromArgb(91, 92, 120);
			componentResourceManager.ApplyResources(this.panel2Content, "panel2Content");
			this.panel2Content.Controls.Add(this.stbRunInfo);
			this.panel2Content.Controls.Add(this.panel1);
			this.panel2Content.Controls.Add(this.panel4Form);
			this.panel2Content.Name = "panel2Content";
			this.stbRunInfo.BackgroundImage = Resources.pMain_bottom;
			componentResourceManager.ApplyResources(this.stbRunInfo, "stbRunInfo");
			this.stbRunInfo.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripDropDownButton1,
				this.mnu1Help,
				this.statOperator,
				this.statSoftwareVer,
				this.statCOM,
				this.statRuninfo1,
				this.statRuninfo2,
				this.statRuninfo3,
				this.statRuninfoLoadedNum,
				this.statTimeDate
			});
			this.stbRunInfo.Name = "stbRunInfo";
			this.statOperator.BackColor = Color.Transparent;
			this.statOperator.BorderSides = ToolStripStatusLabelBorderSides.Right;
			this.statOperator.ForeColor = Color.White;
			this.statOperator.Margin = new Padding(10, 3, 0, 2);
			this.statOperator.Name = "statOperator";
			componentResourceManager.ApplyResources(this.statOperator, "statOperator");
			this.statSoftwareVer.BackColor = Color.Transparent;
			this.statSoftwareVer.ForeColor = Color.White;
			this.statSoftwareVer.Name = "statSoftwareVer";
			componentResourceManager.ApplyResources(this.statSoftwareVer, "statSoftwareVer");
			this.statCOM.BackColor = Color.Transparent;
			this.statCOM.ForeColor = Color.White;
			this.statCOM.Name = "statCOM";
			componentResourceManager.ApplyResources(this.statCOM, "statCOM");
			this.statRuninfo1.BackColor = Color.Transparent;
			this.statRuninfo1.ForeColor = Color.White;
			this.statRuninfo1.Name = "statRuninfo1";
			componentResourceManager.ApplyResources(this.statRuninfo1, "statRuninfo1");
			this.statRuninfo1.Spring = true;
			this.statRuninfo2.BackColor = Color.Transparent;
			this.statRuninfo2.ForeColor = Color.White;
			this.statRuninfo2.Name = "statRuninfo2";
			componentResourceManager.ApplyResources(this.statRuninfo2, "statRuninfo2");
			this.statRuninfo3.BackColor = Color.Transparent;
			this.statRuninfo3.ForeColor = Color.White;
			this.statRuninfo3.Name = "statRuninfo3";
			componentResourceManager.ApplyResources(this.statRuninfo3, "statRuninfo3");
			this.statRuninfoLoadedNum.BackColor = Color.Transparent;
			this.statRuninfoLoadedNum.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.statRuninfoLoadedNum.ForeColor = Color.White;
			this.statRuninfoLoadedNum.Name = "statRuninfoLoadedNum";
			componentResourceManager.ApplyResources(this.statRuninfoLoadedNum, "statRuninfoLoadedNum");
			this.statTimeDate.BackColor = Color.Transparent;
			this.statTimeDate.ForeColor = Color.White;
			this.statTimeDate.Image = Resources.timequery;
			this.statTimeDate.Name = "statTimeDate";
			componentResourceManager.ApplyResources(this.statTimeDate, "statTimeDate");
			componentResourceManager.ApplyResources(this.panel1, "panel1");
			this.panel1.BackColor = Color.Transparent;
			this.panel1.ContextMenuStrip = this.contextMenuStrip3Normal;
			this.panel1.Name = "panel1";
			this.toolTip1.SetToolTip(this.panel1, componentResourceManager.GetString("panel1.ToolTip"));
			componentResourceManager.ApplyResources(this.panel4Form, "panel4Form");
			this.panel4Form.BackColor = Color.FromArgb(128, 131, 156);
			this.panel4Form.Name = "panel4Form";
			this.panel4Form.TabStop = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(91, 92, 120);
			base.Controls.Add(this.toolStrip1BookMark);
			base.Controls.Add(this.flowLayoutPanel1ICon);
			base.Controls.Add(this.panel2Content);
			base.Name = "frmADCT3000";
			base.FormClosing += new FormClosingEventHandler(this.frmADCT3000_FormClosing);
			base.Load += new EventHandler(this.frmADCT3000_Load);
			base.KeyDown += new KeyEventHandler(this.frmADCT3000_KeyDown);
			this.contextMenuStrip1Tools.ResumeLayout(false);
			this.contextMenuStrip2Help.ResumeLayout(false);
			this.toolStrip1BookMark.ResumeLayout(false);
			this.toolStrip1BookMark.PerformLayout();
			this.contextMenuStrip3Normal.ResumeLayout(false);
			this.flowLayoutPanel1ICon.ResumeLayout(false);
			this.grpGettingStarted.ResumeLayout(false);
			this.grpGettingStarted.PerformLayout();
			this.panel2Content.ResumeLayout(false);
			this.panel2Content.PerformLayout();
			this.stbRunInfo.ResumeLayout(false);
			this.stbRunInfo.PerformLayout();
			((ISupportInitialize)this.panel4Form).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
