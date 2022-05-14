using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.ExtendFunc;
using WG3000_COMM.ExtendFunc.Map;
using WG3000_COMM.ExtendFunc.PCCheck;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class frmConsole : Form
	{
		private class DoorSetInfo
		{
			public int DoorId;

			public string DoorName = "";

			public int DoorNO;

			public int ControllerSN;

			public string IP = "";

			public int PORT = 60000;

			public int ConnectState;

			public int ZoneID;

			public int Selected;

			public DoorSetInfo(int id, string name, int no, int sn, string ip, int port, int state, int zoneid)
			{
				this.DoorId = id;
				this.DoorName = name;
				this.DoorNO = no;
				this.ControllerSN = sn;
				this.IP = ip;
				this.PORT = port;
				this.ConnectState = state;
				this.ZoneID = zoneid;
			}
		}

		private delegate void itmDisplayStatus(ListViewItem itm, int status);

		private delegate void txtInfoHaveNewInfo();

		private enum StepOfRealtimeGetReocrds
		{
			Stop,
			GetRecordFirst,
			GetFinished,
			StartMonitoring,
			WaitGetRecord,
			DelSwipe,
			EndStep
		}

		public const int MODE_Check = 1;

		public const int MODE_SetTime = 2;

		public const int MODE_Upload = 3;

		public const int MODE_Server = 4;

		public const int MODE_GetRecords = 5;

		public const int MODE_RemoteOpen = 6;

		private IContainer components;

		private ToolStrip toolStrip1;

		private SplitContainer splitContainer1;

		private SplitContainer splitContainer2;

		private DataGridView dgvRunInfo;

		private ToolStripButton btnUpload;

		private ToolStripButton btnGetRecords;

		private ToolStripButton btnCheck;

		private ToolStripButton btnSetTime;

		private ToolStripButton btnRemoteOpen;

		private ToolStripButton btnServer;

		private GroupBox grpDetail;

		private ToolStrip toolStrip2;

		private ToolStripButton btnEventLogInfo;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton btnEventLogWarn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton btnEventLogError;

		private PictureBox pictureBox1;

		private TextBox txtInfo;

		private System.Windows.Forms.Timer timerUpdateDoorInfo;

		private ToolStripButton btnSelectAll;

		private BackgroundWorker bkUploadAndGetRecords;

		private ToolStripButton btnStopMonitor;

		private ToolStripButton btnStopOthers;

		private ToolStripComboBox cboZone;

		private BackgroundWorker bkDispDoorStatus;

		private ToolStripButton btnWarnExisted;

		private DataGridView dataGridView2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewImageColumn f_Category;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_Time;

		private DataGridViewTextBoxColumn f_Desc;

		private DataGridViewTextBoxColumn f_Info;

		private DataGridViewTextBoxColumn f_Detail;

		private DataGridViewTextBoxColumn f_MjRecStr;

		private ToolStripButton btnClearRunInfo;

		private ContextMenuStrip contextMenuStrip1Doors;

		private ToolStripMenuItem mnuCheck;

		private ToolStripMenuItem mnuWarnOutputReset;

		private System.Windows.Forms.Timer timerWarn;

		private ContextMenuStrip contextMenuStrip2RunInfo;

		private ToolStripMenuItem clearRunInfoToolStripMenuItem;

		private ToolStripMenuItem displayMoreSwipesToolStripMenuItem;

		private RichTextBox richTxtInfo;

		private ToolStripButton btnMaps;

		private ToolStripButton btnRealtimeGetRecords;

		private BackgroundWorker bkRealtimeGetRecords;

		private ToolStripMenuItem locateToolStripMenuItem;

		private ToolStripMenuItem personInsideToolStripMenuItem;

		private ToolTip toolTip1;

		public ListView lstDoors;

		private ToolStripMenuItem resetPersonInsideToolStripMenuItem;

		private GroupBox grpTool;

		private CheckBox chkNeedCheckLosePacket;

		private CheckBox chkDisplayNewestSwipe;

		private Button btnHideTools;

		public int totalConsoleMode;

		private SoundPlayer player;

		private WatchingService watching;

		private DataTable tbRunInfoLog;

		private Dictionary<string, string> ReaderName = new Dictionary<string, string>();

		private string strRealMonitor = "";

		private bool bPCCheckAccess;

		private DataView dv;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private DataView dvDoors;

		private DataView dvDoors4Watching;

		private DataView dvDoors4Check;

		private ImageList imgDoor2;

		private DataTable dt;

		private DataTable dtReader;

		private SqlCommand cm4ParamPrivilege;

		private icController control4Check;

		private DateTime watchingStartTime;

		private icController control4btnServer;

		private Queue QueRecText = new Queue();

		private static int receivedPktCount;

		private static int dealingTxt;

		private static int infoRowsCount;

		private byte[] oImage = new byte[]
		{
			66,
			77,
			198,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			118,
			0,
			0,
			0,
			40,
			0,
			0,
			0,
			11,
			0,
			0,
			0,
			10,
			0,
			0,
			0,
			1,
			0,
			4,
			0,
			0,
			0,
			0,
			0,
			80,
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
			16,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			128,
			0,
			0,
			0,
			128,
			128,
			0,
			128,
			0,
			0,
			0,
			128,
			0,
			128,
			0,
			128,
			128,
			0,
			0,
			192,
			192,
			192,
			0,
			128,
			128,
			128,
			0,
			0,
			0,
			255,
			0,
			0,
			255,
			0,
			0,
			0,
			255,
			255,
			0,
			255,
			0,
			0,
			0,
			255,
			0,
			255,
			0,
			255,
			255,
			0,
			0,
			255,
			255,
			255,
			0,
			255,
			255,
			0,
			15,
			255,
			240,
			0,
			0,
			255,
			0,
			255,
			240,
			15,
			240,
			0,
			0,
			240,
			255,
			255,
			255,
			240,
			240,
			0,
			0,
			240,
			255,
			255,
			255,
			240,
			240,
			0,
			0,
			15,
			255,
			255,
			255,
			255,
			0,
			0,
			0,
			15,
			255,
			255,
			255,
			255,
			0,
			0,
			0,
			240,
			255,
			255,
			255,
			240,
			240,
			0,
			0,
			240,
			255,
			255,
			255,
			240,
			240,
			0,
			0,
			255,
			0,
			255,
			240,
			15,
			240,
			0,
			0,
			255,
			255,
			0,
			15,
			255,
			240,
			0,
			0
		};

		private byte[] xImage = new byte[]
		{
			66,
			77,
			198,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			118,
			0,
			0,
			0,
			40,
			0,
			0,
			0,
			11,
			0,
			0,
			0,
			10,
			0,
			0,
			0,
			1,
			0,
			4,
			0,
			0,
			0,
			0,
			0,
			80,
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
			16,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			128,
			0,
			0,
			0,
			128,
			128,
			0,
			128,
			0,
			0,
			0,
			128,
			0,
			128,
			0,
			128,
			128,
			0,
			0,
			192,
			192,
			192,
			0,
			128,
			128,
			128,
			0,
			0,
			0,
			255,
			0,
			0,
			255,
			0,
			0,
			0,
			255,
			255,
			0,
			255,
			0,
			0,
			0,
			255,
			0,
			255,
			0,
			255,
			255,
			0,
			0,
			255,
			255,
			255,
			0,
			240,
			255,
			255,
			255,
			240,
			240,
			0,
			0,
			255,
			15,
			255,
			255,
			15,
			240,
			0,
			0,
			255,
			240,
			255,
			240,
			255,
			240,
			0,
			0,
			255,
			255,
			15,
			15,
			255,
			240,
			0,
			0,
			255,
			255,
			15,
			15,
			255,
			240,
			0,
			0,
			255,
			255,
			15,
			15,
			255,
			240,
			0,
			0,
			255,
			240,
			255,
			240,
			255,
			240,
			0,
			0,
			255,
			15,
			255,
			255,
			15,
			240,
			0,
			0,
			240,
			255,
			255,
			255,
			240,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0
		};

		private byte[] blankImage = new byte[]
		{
			66,
			77,
			198,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			118,
			0,
			0,
			0,
			40,
			0,
			0,
			0,
			11,
			0,
			0,
			0,
			10,
			0,
			0,
			0,
			1,
			0,
			4,
			0,
			0,
			0,
			0,
			0,
			80,
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
			16,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			128,
			0,
			0,
			0,
			128,
			128,
			0,
			128,
			0,
			0,
			0,
			128,
			0,
			128,
			0,
			128,
			128,
			0,
			0,
			192,
			192,
			192,
			0,
			128,
			128,
			128,
			0,
			0,
			0,
			255,
			0,
			0,
			255,
			0,
			0,
			0,
			255,
			255,
			0,
			255,
			0,
			0,
			0,
			255,
			0,
			255,
			0,
			255,
			255,
			0,
			0,
			255,
			255,
			255,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0,
			255,
			255,
			255,
			255,
			255,
			240,
			0,
			0
		};

		private string oldInfoTitleString;

		private ArrayList arrSelectedDoors = new ArrayList();

		private ArrayList arrSelectedDoorsItem = new ArrayList();

		private int dealtDoorIndex;

		private Dictionary<int, int> arrDealtController = new Dictionary<int, int>();

		private string CommOperate = "";

		private int CommOperateOption;

		private icController control4getRecordsFromController;

		private string strAllProductsDriversInfo;

		private icController control4uploadPrivilege = new icController();

		private wgMjControllerConfigure controlConfigure4uploadPrivilege = new wgMjControllerConfigure();

		private wgMjControllerTaskList controlTaskList4uploadPrivilege = new wgMjControllerTaskList();

		private wgMjControllerHolidaysList controlHolidayList4uploadPrivilege = new wgMjControllerHolidaysList();

		private icPrivilege pr4uploadPrivilege = new icPrivilege();

		private bool bStopComm;

		private ListView listViewNotDisplay = new ListView();

		private dfrmWait dfrmWait1 = new dfrmWait();

		private int watchingDealtDoorIndex;

		private dfrmFind dfrmFind1;

		private DateTime dtlstDoorViewChange = DateTime.Now;

		private bool bNeedCheckLosePacket;

		private frmWatchingMoreRecords frmMoreRecords;

		private frmMaps frmMaps1;

		private ArrayList doorsNeedToGetRecords = new ArrayList();

		private ArrayList selectedControllersSNOfRealtimeGetRecords = new ArrayList();

		private Dictionary<int, icController> selectedControllersOfRealtimeGetRecords;

		private Dictionary<int, int> needDelSwipeControllers;

		private Dictionary<int, int> realtimeGetRecordsSwipeIndexGot = new Dictionary<int, int>();

		private int dealtIndexOfDoorsNeedToGetRecords = -1;

		private frmConsole.StepOfRealtimeGetReocrds stepOfRealtimeGetRecords;

		private icController control4Realtime;

		private icSwipeRecord swipe4GetRecords = new icSwipeRecord();

		private dfrmLocate frm4ShowLocate;

		private dfrmPersonsInside frm4ShowPersonsInside;

		private ArrayList checkAccess_arrDoor = new ArrayList();

		private ArrayList checkAccess_arrDoorName = new ArrayList();

		private ArrayList checkAccess_arrReaderNo = new ArrayList();

		private ArrayList checkAccess_arrGroupName = new ArrayList();

		private ArrayList checkAccess_arrCardId = new ArrayList();

		private ArrayList checkAccess_arrConsumerName = new ArrayList();

		private ArrayList checkAccess_arrCheckTime = new ArrayList();

		private ArrayList checkAccess_arrCheckStartTime = new ArrayList();

		private ArrayList checkAccess_arrCount = new ArrayList();

		private ArrayList checkAccess_arrDB_GroupName = new ArrayList();

		private ArrayList checkAccess_arrDB_MoreCards = new ArrayList();

		public dfrmPCCheckAccess frm4PCCheckAccess;

		public ArrayList arrSelectDoors4Sign = new ArrayList();

		private bool bDirectToRealtimeGet;

		public bool bMainWindowDisplay = true;

		private wgCommService wgCommService1;

		private Queue wgCommQueRecText = new Queue();

		private static int wgCommReceivedPktCount;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.control4btnServer != null)
			{
				this.control4btnServer.Dispose();
			}
			if (disposing && this.control4Check != null)
			{
				this.control4Check.Dispose();
			}
			if (disposing && this.control4Realtime != null)
			{
				this.control4Realtime.Dispose();
			}
			if (disposing && this.control4uploadPrivilege != null)
			{
				this.control4uploadPrivilege.Dispose();
			}
			if (disposing && this.control4getRecordsFromController != null)
			{
				this.control4getRecordsFromController.Dispose();
			}
			if (disposing && this.pr4uploadPrivilege != null)
			{
				this.pr4uploadPrivilege.Dispose();
			}
			if (disposing && this.swipe4GetRecords != null)
			{
				this.swipe4GetRecords.Dispose();
			}
			if (disposing && this.watching != null)
			{
				this.watching.Dispose();
			}
			if (disposing && this.dfrmWait1 != null)
			{
				this.dfrmWait1.Dispose();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmConsole));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.splitContainer1 = new SplitContainer();
			this.grpTool = new GroupBox();
			this.btnHideTools = new Button();
			this.chkDisplayNewestSwipe = new CheckBox();
			this.chkNeedCheckLosePacket = new CheckBox();
			this.lstDoors = new ListView();
			this.contextMenuStrip1Doors = new ContextMenuStrip(this.components);
			this.mnuCheck = new ToolStripMenuItem();
			this.mnuWarnOutputReset = new ToolStripMenuItem();
			this.locateToolStripMenuItem = new ToolStripMenuItem();
			this.personInsideToolStripMenuItem = new ToolStripMenuItem();
			this.resetPersonInsideToolStripMenuItem = new ToolStripMenuItem();
			this.splitContainer2 = new SplitContainer();
			this.dgvRunInfo = new DataGridView();
			this.f_Category = new DataGridViewImageColumn();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_Time = new DataGridViewTextBoxColumn();
			this.f_Desc = new DataGridViewTextBoxColumn();
			this.f_Info = new DataGridViewTextBoxColumn();
			this.f_Detail = new DataGridViewTextBoxColumn();
			this.f_MjRecStr = new DataGridViewTextBoxColumn();
			this.contextMenuStrip2RunInfo = new ContextMenuStrip(this.components);
			this.clearRunInfoToolStripMenuItem = new ToolStripMenuItem();
			this.displayMoreSwipesToolStripMenuItem = new ToolStripMenuItem();
			this.dataGridView2 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.grpDetail = new GroupBox();
			this.pictureBox1 = new PictureBox();
			this.richTxtInfo = new RichTextBox();
			this.txtInfo = new TextBox();
			this.toolStrip2 = new ToolStrip();
			this.btnEventLogInfo = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.btnEventLogWarn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.btnEventLogError = new ToolStripButton();
			this.timerUpdateDoorInfo = new System.Windows.Forms.Timer(this.components);
			this.bkUploadAndGetRecords = new BackgroundWorker();
			this.bkDispDoorStatus = new BackgroundWorker();
			this.toolStrip1 = new ToolStrip();
			this.btnWarnExisted = new ToolStripButton();
			this.btnSelectAll = new ToolStripButton();
			this.btnServer = new ToolStripButton();
			this.btnStopOthers = new ToolStripButton();
			this.btnCheck = new ToolStripButton();
			this.btnSetTime = new ToolStripButton();
			this.btnUpload = new ToolStripButton();
			this.btnGetRecords = new ToolStripButton();
			this.btnRealtimeGetRecords = new ToolStripButton();
			this.btnRemoteOpen = new ToolStripButton();
			this.btnClearRunInfo = new ToolStripButton();
			this.btnMaps = new ToolStripButton();
			this.cboZone = new ToolStripComboBox();
			this.btnStopMonitor = new ToolStripButton();
			this.timerWarn = new System.Windows.Forms.Timer(this.components);
			this.bkRealtimeGetRecords = new BackgroundWorker();
			this.toolTip1 = new ToolTip(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.grpTool.SuspendLayout();
			this.contextMenuStrip1Doors.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((ISupportInitialize)this.dgvRunInfo).BeginInit();
			this.contextMenuStrip2RunInfo.SuspendLayout();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			this.grpDetail.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.BackColor = Color.FromArgb(97, 102, 131);
			componentResourceManager.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.grpTool);
			this.splitContainer1.Panel1.Controls.Add(this.lstDoors);
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.grpTool.Controls.Add(this.btnHideTools);
			this.grpTool.Controls.Add(this.chkDisplayNewestSwipe);
			this.grpTool.Controls.Add(this.chkNeedCheckLosePacket);
			this.grpTool.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.grpTool, "grpTool");
			this.grpTool.Name = "grpTool";
			this.grpTool.TabStop = false;
			this.btnHideTools.ForeColor = Color.Black;
			componentResourceManager.ApplyResources(this.btnHideTools, "btnHideTools");
			this.btnHideTools.Name = "btnHideTools";
			this.btnHideTools.UseVisualStyleBackColor = true;
			this.btnHideTools.Click += new EventHandler(this.btnHideTools_Click);
			componentResourceManager.ApplyResources(this.chkDisplayNewestSwipe, "chkDisplayNewestSwipe");
			this.chkDisplayNewestSwipe.Name = "chkDisplayNewestSwipe";
			this.chkDisplayNewestSwipe.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkNeedCheckLosePacket, "chkNeedCheckLosePacket");
			this.chkNeedCheckLosePacket.Name = "chkNeedCheckLosePacket";
			this.chkNeedCheckLosePacket.UseVisualStyleBackColor = true;
			this.lstDoors.BackColor = SystemColors.Window;
			this.lstDoors.BackgroundImageTiled = true;
			this.lstDoors.ContextMenuStrip = this.contextMenuStrip1Doors;
			componentResourceManager.ApplyResources(this.lstDoors, "lstDoors");
			this.lstDoors.ForeColor = SystemColors.WindowText;
			this.lstDoors.Name = "lstDoors";
			this.toolTip1.SetToolTip(this.lstDoors, componentResourceManager.GetString("lstDoors.ToolTip"));
			this.lstDoors.UseCompatibleStateImageBehavior = false;
			this.lstDoors.SelectedIndexChanged += new EventHandler(this.lstDoors_SelectedIndexChanged);
			this.lstDoors.KeyDown += new KeyEventHandler(this.frmConsole_KeyDown);
			this.lstDoors.MouseDown += new MouseEventHandler(this.frmConsole_MouseClick);
			this.contextMenuStrip1Doors.Items.AddRange(new ToolStripItem[]
			{
				this.mnuCheck,
				this.mnuWarnOutputReset,
				this.locateToolStripMenuItem,
				this.personInsideToolStripMenuItem,
				this.resetPersonInsideToolStripMenuItem
			});
			this.contextMenuStrip1Doors.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1Doors, "contextMenuStrip1Doors");
			this.mnuCheck.Name = "mnuCheck";
			componentResourceManager.ApplyResources(this.mnuCheck, "mnuCheck");
			this.mnuCheck.Click += new EventHandler(this.mnuCheck_Click);
			this.mnuWarnOutputReset.Name = "mnuWarnOutputReset";
			componentResourceManager.ApplyResources(this.mnuWarnOutputReset, "mnuWarnOutputReset");
			this.mnuWarnOutputReset.Click += new EventHandler(this.mnuWarnReset_Click);
			this.locateToolStripMenuItem.Name = "locateToolStripMenuItem";
			componentResourceManager.ApplyResources(this.locateToolStripMenuItem, "locateToolStripMenuItem");
			this.locateToolStripMenuItem.Click += new EventHandler(this.locateToolStripMenuItem_Click);
			this.personInsideToolStripMenuItem.Name = "personInsideToolStripMenuItem";
			componentResourceManager.ApplyResources(this.personInsideToolStripMenuItem, "personInsideToolStripMenuItem");
			this.personInsideToolStripMenuItem.Click += new EventHandler(this.personInsideToolStripMenuItem_Click);
			this.resetPersonInsideToolStripMenuItem.Name = "resetPersonInsideToolStripMenuItem";
			componentResourceManager.ApplyResources(this.resetPersonInsideToolStripMenuItem, "resetPersonInsideToolStripMenuItem");
			this.resetPersonInsideToolStripMenuItem.Click += new EventHandler(this.resetPersonInsideToolStripMenuItem_Click);
			this.splitContainer2.BackColor = Color.FromArgb(97, 102, 131);
			componentResourceManager.ApplyResources(this.splitContainer2, "splitContainer2");
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Panel1.Controls.Add(this.dgvRunInfo);
			this.splitContainer2.Panel2.BackColor = Color.FromArgb(97, 102, 131);
			this.splitContainer2.Panel2.Controls.Add(this.dataGridView2);
			this.splitContainer2.Panel2.Controls.Add(this.grpDetail);
			this.splitContainer2.Panel2.Controls.Add(this.toolStrip2);
			this.dgvRunInfo.AllowUserToAddRows = false;
			this.dgvRunInfo.AllowUserToDeleteRows = false;
			this.dgvRunInfo.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.Padding = new Padding(0, 0, 0, 2);
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvRunInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvRunInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvRunInfo.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_Category,
				this.f_RecID,
				this.f_Time,
				this.f_Desc,
				this.f_Info,
				this.f_Detail,
				this.f_MjRecStr
			});
			this.dgvRunInfo.ContextMenuStrip = this.contextMenuStrip2RunInfo;
			componentResourceManager.ApplyResources(this.dgvRunInfo, "dgvRunInfo");
			this.dgvRunInfo.EnableHeadersVisualStyles = false;
			this.dgvRunInfo.MultiSelect = false;
			this.dgvRunInfo.Name = "dgvRunInfo";
			this.dgvRunInfo.ReadOnly = true;
			this.dgvRunInfo.RowHeadersVisible = false;
			this.dgvRunInfo.RowTemplate.Height = 23;
			this.dgvRunInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.toolTip1.SetToolTip(this.dgvRunInfo, componentResourceManager.GetString("dgvRunInfo.ToolTip"));
			this.dgvRunInfo.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvRunInfo_CellFormatting);
			this.dgvRunInfo.SelectionChanged += new EventHandler(this.dgvRunInfo_SelectionChanged);
			componentResourceManager.ApplyResources(this.f_Category, "f_Category");
			this.f_Category.Name = "f_Category";
			this.f_Category.ReadOnly = true;
			this.f_Category.Resizable = DataGridViewTriState.True;
			this.f_Category.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Time, "f_Time");
			this.f_Time.Name = "f_Time";
			this.f_Time.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc, "f_Desc");
			this.f_Desc.Name = "f_Desc";
			this.f_Desc.ReadOnly = true;
			this.f_Info.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Info, "f_Info");
			this.f_Info.Name = "f_Info";
			this.f_Info.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Detail, "f_Detail");
			this.f_Detail.Name = "f_Detail";
			this.f_Detail.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_MjRecStr, "f_MjRecStr");
			this.f_MjRecStr.Name = "f_MjRecStr";
			this.f_MjRecStr.ReadOnly = true;
			this.contextMenuStrip2RunInfo.Items.AddRange(new ToolStripItem[]
			{
				this.clearRunInfoToolStripMenuItem,
				this.displayMoreSwipesToolStripMenuItem
			});
			this.contextMenuStrip2RunInfo.Name = "contextMenuStrip2RunInfo";
			componentResourceManager.ApplyResources(this.contextMenuStrip2RunInfo, "contextMenuStrip2RunInfo");
			this.clearRunInfoToolStripMenuItem.Name = "clearRunInfoToolStripMenuItem";
			componentResourceManager.ApplyResources(this.clearRunInfoToolStripMenuItem, "clearRunInfoToolStripMenuItem");
			this.clearRunInfoToolStripMenuItem.Click += new EventHandler(this.clearRunInfoToolStripMenuItem_Click);
			this.displayMoreSwipesToolStripMenuItem.Name = "displayMoreSwipesToolStripMenuItem";
			componentResourceManager.ApplyResources(this.displayMoreSwipesToolStripMenuItem, "displayMoreSwipesToolStripMenuItem");
			this.displayMoreSwipesToolStripMenuItem.Click += new EventHandler(this.displayMoreSwipesToolStripMenuItem_Click);
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToDeleteRows = false;
			this.dataGridView2.BackgroundColor = SystemColors.Window;
			this.dataGridView2.BorderStyle = BorderStyle.Fixed3D;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.Padding = new Padding(0, 0, 0, 2);
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView2.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1
			});
			componentResourceManager.ApplyResources(this.dataGridView2, "dataGridView2");
			this.dataGridView2.EnableHeadersVisualStyles = false;
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.ReadOnly = true;
			this.dataGridView2.RowHeadersVisible = false;
			this.dataGridView2.RowTemplate.Height = 23;
			this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.grpDetail, "grpDetail");
			this.grpDetail.BackColor = SystemColors.Window;
			this.grpDetail.Controls.Add(this.pictureBox1);
			this.grpDetail.Controls.Add(this.richTxtInfo);
			this.grpDetail.Controls.Add(this.txtInfo);
			this.grpDetail.ForeColor = Color.Transparent;
			this.grpDetail.Name = "grpDetail";
			this.grpDetail.TabStop = false;
			componentResourceManager.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.richTxtInfo, "richTxtInfo");
			this.richTxtInfo.BorderStyle = BorderStyle.None;
			this.richTxtInfo.Name = "richTxtInfo";
			componentResourceManager.ApplyResources(this.txtInfo, "txtInfo");
			this.txtInfo.BorderStyle = BorderStyle.None;
			this.txtInfo.ForeColor = SystemColors.WindowText;
			this.txtInfo.Name = "txtInfo";
			this.toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.btnEventLogInfo,
				this.toolStripSeparator1,
				this.btnEventLogWarn,
				this.toolStripSeparator2,
				this.btnEventLogError
			});
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.Name = "toolStrip2";
			this.btnEventLogInfo.Checked = true;
			this.btnEventLogInfo.CheckOnClick = true;
			this.btnEventLogInfo.CheckState = CheckState.Checked;
			this.btnEventLogInfo.Image = Resources.eventlogInfo;
			componentResourceManager.ApplyResources(this.btnEventLogInfo, "btnEventLogInfo");
			this.btnEventLogInfo.Name = "btnEventLogInfo";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			componentResourceManager.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.btnEventLogWarn.Checked = true;
			this.btnEventLogWarn.CheckOnClick = true;
			this.btnEventLogWarn.CheckState = CheckState.Checked;
			this.btnEventLogWarn.Image = Resources.eventlogWarn;
			componentResourceManager.ApplyResources(this.btnEventLogWarn, "btnEventLogWarn");
			this.btnEventLogWarn.Name = "btnEventLogWarn";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			componentResourceManager.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			this.btnEventLogError.Checked = true;
			this.btnEventLogError.CheckOnClick = true;
			this.btnEventLogError.CheckState = CheckState.Checked;
			this.btnEventLogError.Image = Resources.eventlogError;
			componentResourceManager.ApplyResources(this.btnEventLogError, "btnEventLogError");
			this.btnEventLogError.Name = "btnEventLogError";
			this.timerUpdateDoorInfo.Interval = 200;
			this.timerUpdateDoorInfo.Tick += new EventHandler(this.timerUpdateDoorInfo_Tick);
			this.bkUploadAndGetRecords.WorkerSupportsCancellation = true;
			this.bkUploadAndGetRecords.DoWork += new DoWorkEventHandler(this.bkUploadAndGetRecords_DoWork);
			this.bkUploadAndGetRecords.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bkUploadAndGetRecords_RunWorkerCompleted);
			this.bkDispDoorStatus.WorkerSupportsCancellation = true;
			this.bkDispDoorStatus.DoWork += new DoWorkEventHandler(this.bkDispDoorStatus_DoWork);
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnWarnExisted,
				this.btnSelectAll,
				this.btnServer,
				this.btnStopOthers,
				this.btnCheck,
				this.btnSetTime,
				this.btnUpload,
				this.btnGetRecords,
				this.btnRealtimeGetRecords,
				this.btnRemoteOpen,
				this.btnClearRunInfo,
				this.btnMaps,
				this.cboZone,
				this.btnStopMonitor
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnWarnExisted.BackColor = Color.Red;
			this.btnWarnExisted.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.btnWarnExisted.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnWarnExisted, "btnWarnExisted");
			this.btnWarnExisted.Name = "btnWarnExisted";
			this.btnWarnExisted.Click += new EventHandler(this.btnWarnExisted_Click);
			this.btnSelectAll.ForeColor = Color.White;
			this.btnSelectAll.Image = Resources.pConsole_SelectAll;
			componentResourceManager.ApplyResources(this.btnSelectAll, "btnSelectAll");
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
			this.btnServer.BackColor = Color.Transparent;
			this.btnServer.ForeColor = Color.White;
			this.btnServer.Image = Resources.pConsole_Monitor;
			componentResourceManager.ApplyResources(this.btnServer, "btnServer");
			this.btnServer.Name = "btnServer";
			this.btnServer.Click += new EventHandler(this.btnServer_Click);
			this.btnStopOthers.ForeColor = Color.White;
			this.btnStopOthers.Image = Resources.pConsole_Stop;
			componentResourceManager.ApplyResources(this.btnStopOthers, "btnStopOthers");
			this.btnStopOthers.Name = "btnStopOthers";
			this.btnStopOthers.Click += new EventHandler(this.btnStopOthers_Click);
			this.btnCheck.ForeColor = Color.White;
			this.btnCheck.Image = Resources.pConsole_CheckController;
			componentResourceManager.ApplyResources(this.btnCheck, "btnCheck");
			this.btnCheck.Name = "btnCheck";
			this.btnCheck.Click += new EventHandler(this.btnCheck_Click);
			this.btnSetTime.ForeColor = Color.White;
			this.btnSetTime.Image = Resources.pChild_AdjustTime;
			componentResourceManager.ApplyResources(this.btnSetTime, "btnSetTime");
			this.btnSetTime.Name = "btnSetTime";
			this.btnSetTime.Click += new EventHandler(this.btnSetTime_Click);
			this.btnUpload.ForeColor = Color.White;
			this.btnUpload.Image = Resources.pConsole_Upload;
			componentResourceManager.ApplyResources(this.btnUpload, "btnUpload");
			this.btnUpload.Name = "btnUpload";
			this.btnUpload.Click += new EventHandler(this.btnUpload_Click);
			this.btnGetRecords.ForeColor = Color.White;
			this.btnGetRecords.Image = Resources.pConsole_GetRecords;
			componentResourceManager.ApplyResources(this.btnGetRecords, "btnGetRecords");
			this.btnGetRecords.Name = "btnGetRecords";
			this.btnGetRecords.Click += new EventHandler(this.btnGetRecords_Click);
			this.btnRealtimeGetRecords.ForeColor = Color.White;
			this.btnRealtimeGetRecords.Image = Resources.pConsole_RealtimeGetRecords;
			componentResourceManager.ApplyResources(this.btnRealtimeGetRecords, "btnRealtimeGetRecords");
			this.btnRealtimeGetRecords.Name = "btnRealtimeGetRecords";
			this.btnRealtimeGetRecords.Click += new EventHandler(this.btnRealtimeGetRecords_Click);
			this.btnRemoteOpen.ForeColor = Color.White;
			this.btnRemoteOpen.Image = Resources.pConsole_OpenDoor;
			componentResourceManager.ApplyResources(this.btnRemoteOpen, "btnRemoteOpen");
			this.btnRemoteOpen.Name = "btnRemoteOpen";
			this.btnRemoteOpen.Click += new EventHandler(this.btnRemoteOpen_Click);
			this.btnClearRunInfo.ForeColor = Color.White;
			this.btnClearRunInfo.Image = Resources.pTools_Clear_Condition;
			componentResourceManager.ApplyResources(this.btnClearRunInfo, "btnClearRunInfo");
			this.btnClearRunInfo.Name = "btnClearRunInfo";
			this.btnClearRunInfo.Click += new EventHandler(this.btnClearRunInfo_Click);
			this.btnMaps.BackColor = Color.Transparent;
			this.btnMaps.ForeColor = Color.White;
			this.btnMaps.Image = Resources.pTools_Maps;
			componentResourceManager.ApplyResources(this.btnMaps, "btnMaps");
			this.btnMaps.Name = "btnMaps";
			this.btnMaps.Click += new EventHandler(this.btnMaps_Click);
			this.cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cboZone, "cboZone");
			this.cboZone.Name = "cboZone";
			this.cboZone.SelectedIndexChanged += new EventHandler(this.cboZone_SelectedIndexChanged);
			this.btnStopMonitor.ForeColor = Color.White;
			this.btnStopMonitor.Image = Resources.pConsole_Stop;
			componentResourceManager.ApplyResources(this.btnStopMonitor, "btnStopMonitor");
			this.btnStopMonitor.Name = "btnStopMonitor";
			this.btnStopMonitor.Click += new EventHandler(this.btnStopMonitor_Click);
			this.timerWarn.Interval = 500;
			this.timerWarn.Tick += new EventHandler(this.timerWarn_Tick);
			this.bkRealtimeGetRecords.WorkerSupportsCancellation = true;
			this.bkRealtimeGetRecords.DoWork += new DoWorkEventHandler(this.bkRealtimeGetRecords_DoWork);
			this.bkRealtimeGetRecords.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bkRealtimeGetRecords_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.White;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.toolStrip1);
			this.DoubleBuffered = true;
			base.KeyPreview = true;
			base.Name = "frmConsole";
			base.FormClosing += new FormClosingEventHandler(this.frmConsole_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.frmConsole_FormClosed);
			base.Load += new EventHandler(this.frmConsole_Load);
			base.KeyDown += new KeyEventHandler(this.frmConsole_KeyDown);
			base.MouseDown += new MouseEventHandler(this.frmConsole_MouseClick);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.grpTool.ResumeLayout(false);
			this.grpTool.PerformLayout();
			this.contextMenuStrip1Doors.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			this.splitContainer2.ResumeLayout(false);
			((ISupportInitialize)this.dgvRunInfo).EndInit();
			this.contextMenuStrip2RunInfo.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.grpDetail.ResumeLayout(false);
			this.grpDetail.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmConsole()
		{
			this.InitializeComponent();
		}

		private void frmConsole_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.FileIsExisted(wgAppConfig.Path4PhotoDefault() + "invalidCard.WAV"))
			{
				this.player = new SoundPlayer();
				this.player.SoundLocation = wgAppConfig.Path4PhotoDefault() + "invalidCard.WAV";
			}
			wgTools.WriteLine("frmConsole_Load Start");
			this.btnWarnExisted.Visible = false;
			if (this.totalConsoleMode == 0)
			{
				this.loadOperatorPrivilege();
			}
			else
			{
				this.btnCheck.Visible = false;
				this.btnSetTime.Visible = false;
				this.btnUpload.Visible = false;
				this.btnServer.Visible = false;
				this.btnGetRecords.Visible = false;
				this.btnRemoteOpen.Visible = false;
				this.mnuCheck.Visible = false;
				switch (this.totalConsoleMode)
				{
				case 1:
					this.btnCheck.Visible = true;
					this.mnuCheck.Visible = true;
					break;
				case 2:
					this.btnSetTime.Visible = true;
					break;
				case 3:
					this.btnUpload.Visible = true;
					break;
				case 4:
					this.btnServer.Visible = true;
					break;
				case 5:
					this.btnGetRecords.Visible = true;
					break;
				case 6:
					this.btnRemoteOpen.Visible = true;
					break;
				}
			}
			this.bPCCheckAccess = wgAppConfig.getParamValBoolByNO(137);
			this.loadDoorData();
			this.txtInfo.Text = "";
			this.richTxtInfo.Text = "";
			wgRunInfoLog.init(out this.tbRunInfoLog);
			this.dv = new DataView(this.tbRunInfoLog);
			this.dgvRunInfo.AutoGenerateColumns = false;
			this.dgvRunInfo.DataSource = this.dv;
			this.dgvRunInfo.Columns[0].DataPropertyName = "f_Category";
			this.dgvRunInfo.Columns[1].DataPropertyName = "f_RecID";
			this.dgvRunInfo.Columns[2].DataPropertyName = "f_Time";
			this.dgvRunInfo.Columns[3].DataPropertyName = "f_Desc";
			this.dgvRunInfo.Columns[4].DataPropertyName = "f_Info";
			this.dgvRunInfo.Columns[5].DataPropertyName = "f_Detail";
			this.dgvRunInfo.Columns[6].DataPropertyName = "f_MjRecStr";
			for (int i = 0; i < this.dgvRunInfo.ColumnCount; i++)
			{
				this.dgvRunInfo.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			this.loadZoneInfo();
			this.btnRemoteOpen.Visible = (this.btnRemoteOpen.Visible && wgAppConfig.getParamValBoolByNO(122));
			this.btnMaps.Visible = (this.btnMaps.Visible && wgAppConfig.getParamValBoolByNO(114));
			this.mnuWarnOutputReset.Visible = wgAppConfig.getParamValBoolByNO(124);
			this.resetPersonInsideToolStripMenuItem.Visible = wgAppConfig.getParamValBoolByNO(132);
			frmConsole.infoRowsCount = 0;
			this.strRealMonitor = this.btnServer.Text;
			this.oldInfoTitleString = this.dataGridView2.Columns[0].HeaderText;
			wgTools.WriteLine("frmConsole_Load End");
		}

		private void frmConsole_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (this.stepOfRealtimeGetRecords != frmConsole.StepOfRealtimeGetReocrds.Stop)
				{
					this.btnStopOthers.PerformClick();
				}
				long ticks = DateTime.Now.Ticks;
				long num = ticks + 150000000L;
				while (DateTime.Now.Ticks < num && this.stepOfRealtimeGetRecords != frmConsole.StepOfRealtimeGetReocrds.Stop)
				{
					Application.DoEvents();
				}
				this.btnStopOthers.PerformClick();
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cboZone.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrZoneName[i].ToString()))
				{
					this.cboZone.Items.Add(CommonStr.strAllZones);
				}
				else
				{
					this.cboZone.Items.Add(this.arrZoneName[i].ToString());
				}
			}
			if (this.cboZone.Items.Count > 0)
			{
				this.cboZone.SelectedIndex = 0;
			}
			bool visible = true;
			this.cboZone.Visible = visible;
		}

		private void loadOperatorPrivilege()
		{
			bool flag;
			bool flag2;
			icOperator.getFrmOperatorPrivilege(base.Name.ToString(), out flag, out flag2);
			if (flag2)
			{
				icOperator.getFrmOperatorPrivilege("btnCheckController", out flag, out flag2);
				this.btnCheck.Visible = (flag || flag2);
				this.mnuCheck.Visible = this.btnCheck.Visible;
				icOperator.getFrmOperatorPrivilege("btnAdjustTime", out flag, out flag2);
				this.btnSetTime.Visible = flag2;
				icOperator.getFrmOperatorPrivilege("btnUpload", out flag, out flag2);
				this.btnUpload.Visible = flag2;
				icOperator.getFrmOperatorPrivilege("btnMonitor", out flag, out flag2);
				this.btnServer.Visible = (flag || flag2);
				icOperator.getFrmOperatorPrivilege("btnGetRecords", out flag, out flag2);
				this.btnGetRecords.Visible = flag2;
				icOperator.getFrmOperatorPrivilege("btnRemoteOpen", out flag, out flag2);
				this.btnRemoteOpen.Visible = flag2;
				icOperator.getFrmOperatorPrivilege("btnRealtimeGetRecords", out flag, out flag2);
				this.btnRealtimeGetRecords.Visible = flag2;
				this.btnMaps.Visible = icOperator.OperatePrivilegeVisible("btnMaps");
				return;
			}
			if (flag)
			{
				icOperator.getFrmOperatorPrivilege("btnCheckController", out flag, out flag2);
				this.btnCheck.Visible = (flag || flag2);
				this.mnuCheck.Visible = this.btnCheck.Visible;
				this.btnSetTime.Visible = false;
				this.btnUpload.Visible = false;
				icOperator.getFrmOperatorPrivilege("btnMonitor", out flag, out flag2);
				this.btnServer.Visible = (flag || flag2);
				icOperator.getFrmOperatorPrivilege("btnMaps", out flag, out flag2);
				this.btnMaps.Visible = (flag2 || flag);
				this.btnGetRecords.Visible = false;
				this.btnRemoteOpen.Visible = false;
				this.btnRealtimeGetRecords.Visible = false;
				return;
			}
			base.Close();
		}

		private void displayNewestLog()
		{
			if (this.dgvRunInfo.Rows.Count > 0)
			{
				this.dgvRunInfo.FirstDisplayedScrollingRowIndex = this.dgvRunInfo.Rows.Count - 1;
				this.dgvRunInfo.Rows[this.dgvRunInfo.Rows.Count - 1].Selected = true;
				this.dgvRunInfo.Rows[this.dgvRunInfo.Rows.Count - 1].Selected = false;
				Application.DoEvents();
			}
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID ";
			text += " , a.f_ControllerID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			this.dt = new DataTable();
			this.dvDoors = new DataView(this.dt);
			this.dvDoors4Watching = new DataView(this.dt);
			this.dvDoors4Check = new DataView(this.dt);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dt);
						}
					}
					goto IL_111;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dt);
					}
				}
			}
			IL_111:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dt.Columns[0]
				};
				this.dt.PrimaryKey = primaryKey;
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			this.imgDoor2 = new ImageList();
			this.imgDoor2.ImageSize = new Size(24, 32);
			this.imgDoor2.TransparentColor = SystemColors.Window;
			string systemParamByNO = wgAppConfig.getSystemParamByNO(22);
			if (!string.IsNullOrEmpty(systemParamByNO))
			{
				decimal num = decimal.Parse(systemParamByNO, CultureInfo.InvariantCulture);
				if (num != 1m && num > 0m && num < 100m)
				{
					this.imgDoor2.ImageSize = new Size((int)(24m * num), (int)(32m * num));
				}
			}
			this.imgDoor2.Images.Add(Resources.pConsole_Door_Unknown);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_NormalClose);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_NormalOpen);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_NotConnected);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_WarnClose);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_WarnOpen);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_Unknown);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_WarnClose);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_WarnOpen);
			this.imgDoor2.Images.Add(Resources.pConsole_Door_NotConnected);
			this.lstDoors.LargeImageList = this.imgDoor2;
			this.lstDoors.SmallImageList = this.imgDoor2;
			try
			{
				if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("CONSOLE_DOORVIEW")))
				{
					string keyVal = wgAppConfig.GetKeyVal("CONSOLE_DOORVIEW");
					if (keyVal == View.Details.ToString())
					{
						this.lstDoors.View = View.LargeIcon;
					}
					else if (keyVal == View.LargeIcon.ToString())
					{
						this.lstDoors.View = View.LargeIcon;
					}
					else if (keyVal == View.List.ToString())
					{
						this.lstDoors.View = View.List;
					}
					else if (keyVal == View.SmallIcon.ToString())
					{
						this.lstDoors.View = View.SmallIcon;
					}
					else if (keyVal == View.Tile.ToString())
					{
						this.lstDoors.View = View.Tile;
					}
					else
					{
						this.lstDoors.View = View.LargeIcon;
					}
				}
			}
			catch (Exception ex2)
			{
				wgAppConfig.wgLog(ex2.ToString());
			}
			this.lstDoors.Items.Clear();
			if (this.dvDoors.Count > 0)
			{
				wgTools.WriteLine("this.lstDoors.Items.Add(itm); Start");
				this.lstDoors.BeginUpdate();
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]);
					listViewItem.ImageIndex = 0;
					listViewItem.Tag = new frmConsole.DoorSetInfo((int)this.dvDoors[i]["f_DoorID"], (string)this.dvDoors[i]["f_DoorName"], (int)((byte)this.dvDoors[i]["f_DoorNO"]), (int)this.dvDoors[i]["f_ControllerSN"], this.dvDoors[i]["f_IP"].ToString(), (int)this.dvDoors[i]["f_PORT"], (int)this.dvDoors[i]["f_ConnectState"], (int)this.dvDoors[i]["f_ZoneID"])
					{
						Selected = 0
					};
					this.lstDoors.Items.Add(listViewItem);
				}
				this.lstDoors.EndUpdate();
				wgTools.WriteLine("this.lstDoors.Items.Add(itm); End");
			}
			text = " SELECT a.f_ReaderNO, a.f_ReaderName , b.f_ControllerSN ";
			text += " FROM t_b_Reader a, t_b_Controller b WHERE  b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			this.dtReader = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
					{
						using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(oleDbCommand2))
						{
							oleDbDataAdapter2.Fill(this.dtReader);
						}
					}
					goto IL_675;
				}
			}
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
				{
					using (SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2))
					{
						sqlDataAdapter2.Fill(this.dtReader);
					}
				}
			}
			IL_675:
			if (this.dtReader.Rows.Count > 0)
			{
				for (int j = 0; j < this.dtReader.Rows.Count; j++)
				{
					this.ReaderName.Add(string.Format("{0}-{1}", this.dtReader.Rows[j]["f_ControllerSN"].ToString(), this.dtReader.Rows[j]["f_ReaderNO"].ToString()), this.dtReader.Rows[j]["f_ReaderName"].ToString());
				}
			}
			this.pcCheckAccess_Init();
		}

		private void btnUpload_Click(object sender, EventArgs e)
		{
			if (this.bkUploadAndGetRecords.IsBusy)
			{
				return;
			}
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			using (dfrmUploadOption dfrmUploadOption = new dfrmUploadOption())
			{
				dfrmUploadOption.ShowDialog(this);
				if (dfrmUploadOption.checkVal == 0)
				{
					return;
				}
				this.CommOperateOption = dfrmUploadOption.checkVal;
			}
			this.btnRealtimeGetRecords.Enabled = false;
			this.btnStopOthers.BackColor = Color.Red;
			this.btnStopMonitor.BackColor = Color.Red;
			this.btnGetRecords.Enabled = false;
			this.btnUpload.Enabled = false;
			this.arrSelectedDoors.Clear();
			this.arrSelectedDoorsItem.Clear();
			this.dealtDoorIndex = 0;
			this.arrDealtController.Clear();
			foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
			{
				this.arrSelectedDoors.Add(listViewItem.Text);
				this.arrSelectedDoorsItem.Add(listViewItem);
			}
			using (icController icController = new icController())
			{
				icController.GetInfoFromDBByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), icController.ControllerSN),
					information = string.Format("{0}", CommonStr.strUploadStart)
				});
			}
			this.displayNewestLog();
			this.CommOperate = "UPLOAD";
			this.bkUploadAndGetRecords.RunWorkerAsync();
			if (!this.bkDispDoorStatus.IsBusy)
			{
				this.bkDispDoorStatus.RunWorkerAsync();
			}
		}

		private void btnGetRecords_Click(object sender, EventArgs e)
		{
			if (this.lstDoors.SelectedItems.Count > 0 && XMessageBox.Show(sender.ToString() + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			if (this.bkUploadAndGetRecords.IsBusy)
			{
				return;
			}
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			this.btnRealtimeGetRecords.Enabled = false;
			this.btnStopOthers.BackColor = Color.Red;
			this.btnStopMonitor.BackColor = Color.Red;
			this.btnGetRecords.Enabled = false;
			this.btnUpload.Enabled = false;
			this.arrSelectedDoors.Clear();
			this.arrSelectedDoorsItem.Clear();
			this.dealtDoorIndex = 0;
			this.arrDealtController.Clear();
			foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
			{
				this.arrSelectedDoors.Add(listViewItem.Text);
				this.arrSelectedDoorsItem.Add(listViewItem);
			}
			using (icController icController = new icController())
			{
				icController.GetInfoFromDBByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), icController.ControllerSN),
					information = string.Format("{0}", CommonStr.strGetSwipeRecordStart)
				});
			}
			this.displayNewestLog();
			this.CommOperate = "GETRECORDS";
			this.bkUploadAndGetRecords.RunWorkerAsync();
			if (!this.bkDispDoorStatus.IsBusy)
			{
				this.bkDispDoorStatus.RunWorkerAsync();
			}
		}

		private void checkParam(string shouldBe, string inFact, string title, string desc, bool bEnable)
		{
			wgTools.WriteLine(title);
			if (shouldBe != inFact)
			{
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = "[" + desc + "]" + CommonStr.strNeedUpload,
					information = string.Concat(new string[]
					{
						title,
						": ",
						CommonStr.strShouldBe,
						shouldBe,
						CommonStr.strInfact,
						inFact
					}),
					category = 501
				});
			}
		}

		private void checkParamPrivileges(string doorName, icController controller, int infactPrivileges)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.checkParamPrivileges_Acc(doorName, controller, infactPrivileges);
				return;
			}
			try
			{
				if (controller.ControllerID <= 999)
				{
					if (!wgMjController.IsElevator(controller.ControllerSN))
					{
						bool flag = false;
						string text = "SELECT * FROM t_b_Controller WHERE f_ControllerID = " + controller.ControllerID.ToString();
						using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
						{
							if (sqlConnection.State != ConnectionState.Open)
							{
								sqlConnection.Open();
							}
							if (wgAppConfig.getSystemParamByNO(53) == "1")
							{
								this.cm4ParamPrivilege = new SqlCommand(text, sqlConnection);
								this.cm4ParamPrivilege.CommandText = " SELECT row_count  FROM sys.dm_db_partition_stats WHERE object_id = OBJECT_ID('t_d_privilege') AND partition_number = " + controller.ControllerID.ToString();
								string text2 = wgTools.SetObjToStr(this.cm4ParamPrivilege.ExecuteScalar());
								if (!string.IsNullOrEmpty(text2))
								{
									int num = int.Parse(text2);
									this.cm4ParamPrivilege.CommandText = text;
									SqlDataReader sqlDataReader = this.cm4ParamPrivilege.ExecuteReader();
									if (sqlDataReader.Read())
									{
										if (!flag && (int)sqlDataReader["f_lastConsoleUploadPrivilege"] != num && wgTools.SetObjToStr(sqlDataReader["f_lastDelAddDateTime"]) == wgTools.SetObjToStr(sqlDataReader["f_lastConsoleUploadDateTime"]) && string.Compare(wgTools.SetObjToStr(sqlDataReader["f_lastDelAddDateTime"]), wgTools.SetObjToStr(sqlDataReader["f_lastDelAddAndUploadDateTime"])) > 0)
										{
											flag = true;
										}
										if (!flag && !string.IsNullOrEmpty(wgTools.SetObjToStr(sqlDataReader["f_lastDelAddDateTime"])))
										{
											if (wgTools.SetObjToStr(sqlDataReader["f_lastDelAddDateTime"]) != wgTools.SetObjToStr(sqlDataReader["f_lastConsoleUploadDateTime"]))
											{
												flag = true;
											}
											else if (string.Compare(wgTools.SetObjToStr(sqlDataReader["f_lastDelAddDateTime"]), wgTools.SetObjToStr(sqlDataReader["f_lastDelAddAndUploadDateTime"])) > 0 && wgTools.SetObjToStr(sqlDataReader["f_lastConsoleUploadConsuemrsTotal"]) != wgTools.SetObjToStr(infactPrivileges))
											{
												flag = true;
											}
										}
										if (!flag && (num == 0 || infactPrivileges == 0) && num != infactPrivileges)
										{
											flag = true;
										}
										if (flag)
										{
											InfoRow infoRow = new InfoRow();
											infoRow.desc = string.Concat(new string[]
											{
												"[",
												doorName,
												"]",
												CommonStr.strPrivileges,
												CommonStr.strNeedUpload
											});
											string str = string.Concat(new string[]
											{
												"[",
												controller.ControllerSN.ToString(),
												"]",
												CommonStr.strPrivileges,
												CommonStr.strNeedUpload
											});
											infoRow.information = string.Format(str + " [{0:d}-{1:d}],[{2:d}-{3:d}-{4:d}]", new object[]
											{
												infactPrivileges,
												num,
												(int)sqlDataReader["f_lastConsoleUploadConsuemrsTotal"],
												(int)sqlDataReader["f_lastConsoleUploadPrivilege"],
												(int)sqlDataReader["f_lastConsoleUploadValidPrivilege"]
											});
											infoRow.category = 501;
											wgRunInfoLog.addEvent(infoRow);
										}
									}
									sqlDataReader.Close();
								}
							}
							else
							{
								this.cm4ParamPrivilege = new SqlCommand("select rowcnt from sysindexes where id=object_id(N't_d_Privilege') and name = N'PK_t_d_Privilege'", sqlConnection);
								int num2 = int.Parse(this.cm4ParamPrivilege.ExecuteScalar().ToString());
								if (num2 <= 2000000)
								{
									this.cm4ParamPrivilege = new SqlCommand(text, sqlConnection);
									this.cm4ParamPrivilege.CommandText = " SELECT COUNT( DISTINCT t_b_Consumer.f_CardNO) FROM t_b_Consumer ,t_d_Privilege  WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL  AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID  and f_ControllerID =" + controller.ControllerID.ToString();
									string text3 = wgTools.SetObjToStr(this.cm4ParamPrivilege.ExecuteScalar());
									if (!string.IsNullOrEmpty(text3))
									{
										int num3 = int.Parse(text3);
										if (num3 != infactPrivileges)
										{
											InfoRow infoRow2 = new InfoRow();
											infoRow2.desc = string.Concat(new string[]
											{
												"[",
												doorName,
												"]",
												CommonStr.strPrivileges,
												CommonStr.strNeedUpload
											});
											string str2 = string.Concat(new string[]
											{
												"[",
												controller.ControllerSN.ToString(),
												"]",
												CommonStr.strPrivileges,
												CommonStr.strNeedUpload
											});
											infoRow2.information = string.Format(str2 + " [{0:d}-{1:d}],[{2:d}-{3:d}-{4:d}]", new object[]
											{
												infactPrivileges,
												num3,
												9,
												9,
												9
											});
											infoRow2.category = 501;
											wgRunInfoLog.addEvent(infoRow2);
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void checkParamPrivileges_Acc(string doorName, icController controller, int infactPrivileges)
		{
			try
			{
				if (controller.ControllerID <= 999)
				{
					if (!wgMjController.IsElevator(controller.ControllerSN))
					{
						string cmdText = "SELECT * FROM t_b_Controller WHERE f_ControllerID = " + controller.ControllerID.ToString();
						using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
						{
							if (oleDbConnection.State != ConnectionState.Open)
							{
								oleDbConnection.Open();
							}
							string text = wgTools.SetObjToStr(new OleDbCommand(cmdText, oleDbConnection)
							{
								CommandText = " SELECT COUNT(*) FROM (SELECT DISTINCT t_b_Consumer.f_CardNO FROM t_b_Consumer ,t_d_Privilege  WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL  AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID  and f_ControllerID =" + controller.ControllerID.ToString() + ")"
							}.ExecuteScalar());
							if (!string.IsNullOrEmpty(text))
							{
								int num = int.Parse(text);
								if (num != infactPrivileges)
								{
									InfoRow infoRow = new InfoRow();
									infoRow.desc = string.Concat(new string[]
									{
										"[",
										doorName,
										"]",
										CommonStr.strPrivileges,
										CommonStr.strNeedUpload
									});
									string str = string.Concat(new string[]
									{
										"[",
										controller.ControllerSN.ToString(),
										"]",
										CommonStr.strPrivileges,
										CommonStr.strNeedUpload
									});
									infoRow.information = string.Format(str + " [{0:d}-{1:d}],[{2:d}-{3:d}-{4:d}]", new object[]
									{
										infactPrivileges,
										num,
										9,
										9,
										9
									});
									infoRow.category = 501;
									wgRunInfoLog.addEvent(infoRow);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnCheck_Click(object sender, EventArgs e)
		{
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			using (this.control4Check = new icController())
			{
				this.bStopComm = false;
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					if (this.bStopComm)
					{
						break;
					}
					this.control4Check.GetInfoFromDBByDoorName(listViewItem.Text);
					if (this.control4Check.GetControllerRunInformationIP() <= 0)
					{
						wgRunInfoLog.addEventNotConnect(this.control4Check.ControllerSN, this.control4Check.IP, listViewItem);
					}
					else
					{
						wgTools.WriteLine("Start");
						wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
						wgMjControllerConfigure wgMjControllerConfigure2 = new wgMjControllerConfigure();
						wgMjControllerTaskList wgMjControllerTaskList = new wgMjControllerTaskList();
						wgMjControllerHolidaysList wgMjControllerHolidaysList = new wgMjControllerHolidaysList();
						icControllerConfigureFromDB.getControllerConfigureFromDBByControllerID(this.control4Check.ControllerID, ref wgMjControllerConfigure2, ref wgMjControllerTaskList, ref wgMjControllerHolidaysList);
						wgTools.WriteLine("getControllerConfigureFromDBByControllerID");
						if (this.control4Check.GetConfigureIP(ref wgMjControllerConfigure) <= 0)
						{
							wgRunInfoLog.addEventNotConnect(this.control4Check.ControllerSN, this.control4Check.IP, listViewItem);
							continue;
						}
						wgTools.WriteLine("getConfigureIP");
						wgMjControllerTaskList wgMjControllerTaskList2 = new wgMjControllerTaskList();
						if ((this.control4Check.runinfo.appError & 2) == 0 && this.control4Check.GetControlTaskListIP(ref wgMjControllerTaskList2) <= 0)
						{
							wgRunInfoLog.addEventNotConnect(this.control4Check.ControllerSN, this.control4Check.IP, listViewItem);
							continue;
						}
						wgTools.WriteLine("getControlTaskListIP");
						new wgMjControllerHolidaysList();
						if ((this.control4Check.runinfo.appError & 2) == 0)
						{
							byte[] byt4K = null;
							if (this.control4Check.GetHolidayListIP(ref byt4K) <= 0)
							{
								wgRunInfoLog.addEventNotConnect(this.control4Check.ControllerSN, this.control4Check.IP, listViewItem);
								continue;
							}
							new wgMjControllerHolidaysList(byt4K);
						}
						wgTools.WriteLine("GetHolidayListIP");
						InfoRow infoRow = new InfoRow();
						infoRow.desc = string.Format("{0}[{1:d}]", listViewItem.Text, this.control4Check.ControllerSN);
						infoRow.information = "";
						infoRow.detail = listViewItem.Text;
						InfoRow expr_226 = infoRow;
						expr_226.detail += string.Format("\r\n{0}:\t{1}", CommonStr.strDoorStatus, this.control4Check.runinfo.IsOpen(this.control4Check.GetDoorNO(listViewItem.Text)) ? CommonStr.strDoorStatus_Open : CommonStr.strDoorStatus_Closed);
						InfoRow expr_276 = infoRow;
						expr_276.information += string.Format("{0};", this.control4Check.runinfo.IsOpen(this.control4Check.GetDoorNO(listViewItem.Text)) ? CommonStr.strDoorStatus_Open : CommonStr.strDoorStatus_Closed);
						InfoRow expr_2C1 = infoRow;
						expr_2C1.detail += string.Format("\r\n{0}:\t{1}", CommonStr.strDoorControl, icDesc.doorControlDesc(wgMjControllerConfigure.DoorControlGet(this.control4Check.GetDoorNO(listViewItem.Text))));
						InfoRow expr_2FE = infoRow;
						expr_2FE.information += string.Format("{0};", icDesc.doorControlDesc(wgMjControllerConfigure.DoorControlGet(this.control4Check.GetDoorNO(listViewItem.Text))));
						InfoRow expr_336 = infoRow;
						expr_336.detail += string.Format("\r\n{0}:\t{1:d}", CommonStr.strDoorDelay, wgMjControllerConfigure.DoorDelayGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString());
						InfoRow expr_377 = infoRow;
						expr_377.information += string.Format("{0}:{1:d};", CommonStr.strDoorDelay, wgMjControllerConfigure.DoorDelayGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString());
						InfoRow expr_3B8 = infoRow;
						expr_3B8.detail += string.Format("\r\n{0}:\t{1:d}", CommonStr.strControllerSN, this.control4Check.ControllerSN);
						InfoRow expr_3E9 = infoRow;
						expr_3E9.detail += string.Format("\r\nIP:\t{0}", this.control4Check.IP);
						InfoRow expr_410 = infoRow;
						expr_410.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strSwipes, this.control4Check.runinfo.newRecordsNum);
						InfoRow expr_446 = infoRow;
						expr_446.information += string.Format("{0}:{1};", CommonStr.strSwipes, this.control4Check.runinfo.newRecordsNum);
						InfoRow expr_47C = infoRow;
						expr_47C.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strPrivileges, this.control4Check.runinfo.registerCardNum);
						InfoRow expr_4B2 = infoRow;
						expr_4B2.information += string.Format("{0}:{1};", CommonStr.strPrivileges, this.control4Check.runinfo.registerCardNum);
						InfoRow expr_4E8 = infoRow;
						expr_4E8.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strRealClock, this.control4Check.runinfo.dtNow.ToString(wgTools.DisplayFormat_DateYMDHMSWeek));
						InfoRow expr_527 = infoRow;
						expr_527.information += string.Format("{0};", this.control4Check.runinfo.dtNow.ToString(wgTools.DisplayFormat_DateYMDHMSWeek));
						if (this.control4Check.runinfo.appError > 0)
						{
							InfoRow expr_574 = infoRow;
							expr_574.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strErr, icDesc.ErrorDetail((int)this.control4Check.runinfo.appError));
							InfoRow expr_5AA = infoRow;
							expr_5AA.information += string.Format("{0};", icDesc.ErrorDetail((int)this.control4Check.runinfo.appError));
						}
						if (this.control4Check.runinfo.WarnInfo(this.control4Check.GetDoorNO(listViewItem.Text)) > 0)
						{
							InfoRow expr_602 = infoRow;
							expr_602.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strWarnDesc, icDesc.WarnDetail((int)this.control4Check.runinfo.WarnInfo(this.control4Check.GetDoorNO(listViewItem.Text))));
							InfoRow expr_649 = infoRow;
							expr_649.information += string.Format("{0};", icDesc.WarnDetail((int)this.control4Check.runinfo.WarnInfo(this.control4Check.GetDoorNO(listViewItem.Text))));
						}
						if (this.control4Check.runinfo.FireIsActive)
						{
							InfoRow expr_69D = infoRow;
							expr_69D.detail += string.Format("\r\n--{0}", CommonStr.strFire);
							InfoRow expr_6BE = infoRow;
							expr_6BE.information += string.Format("{0};", CommonStr.strFire);
						}
						if (this.control4Check.runinfo.ForceLockIsActive)
						{
							InfoRow expr_6F1 = infoRow;
							expr_6F1.detail += string.Format("\r\n--{0}", CommonStr.strCloseByForce);
							InfoRow expr_712 = infoRow;
							expr_712.information += string.Format("{0};", CommonStr.strCloseByForce);
						}
						InfoRow expr_733 = infoRow;
						expr_733.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strFirmware, this.control4Check.runinfo.driverVersion);
						InfoRow expr_764 = infoRow;
						expr_764.information += string.Format("{0};", this.control4Check.runinfo.driverVersion);
						try
						{
							string text = "";
							string text2 = "";
							string productInfoIP = this.control4Check.GetProductInfoIP(ref text, ref text2);
							if (!string.IsNullOrEmpty(productInfoIP))
							{
								InfoRow expr_7B8 = infoRow;
								expr_7B8.detail += string.Format(" [{0}]", text.Substring(text.IndexOf("DATE=") + 5, 10));
							}
						}
						catch (Exception)
						{
						}
						InfoRow expr_7F0 = infoRow;
						expr_7F0.detail += string.Format("\r\n--{0}:\t{1}", "MAC", wgMjControllerConfigure.MACAddr);
						InfoRow expr_817 = infoRow;
						expr_817.information += string.Format("{0};", wgMjControllerConfigure.MACAddr);
						InfoRow expr_839 = infoRow;
						expr_839.detail = expr_839.detail + "\r\n---- " + CommonStr.strEnabled + " ----";
						listViewItem.ImageIndex = this.control4Check.runinfo.GetDoorImageIndex(this.control4Check.GetDoorNO(listViewItem.Text));
						if (DateTime.Now.AddMinutes(-30.0) > this.control4Check.runinfo.dtNow || DateTime.Now.AddMinutes(30.0) < this.control4Check.runinfo.dtNow)
						{
							this.checkParam(DateTime.Now.ToString(wgTools.YMDHMSFormat), this.control4Check.runinfo.dtNow.ToString(wgTools.YMDHMSFormat), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strRealClock, listViewItem.Text + " " + CommonStr.strNeedAdjustTime, false);
						}
						wgTools.WriteLine("icPrivilege.getPrivilegeNumInDBByID");
						this.checkParamPrivileges(listViewItem.Text, this.control4Check, (int)this.control4Check.runinfo.registerCardNum);
						if (wgMjControllerConfigure.controlTaskList_enabled > 0)
						{
							if (wgMjControllerTaskList2.taskCount > 0)
							{
								InfoRow expr_991 = infoRow;
								expr_991.detail += string.Format("\r\n--{0}", CommonStr.strControlTaskList);
								InfoRow expr_9B2 = infoRow;
								expr_9B2.information += string.Format("{0};", CommonStr.strControlTaskList);
							}
						}
						else
						{
							wgMjControllerTaskList2.Clear();
						}
						this.checkParam(wgMjControllerTaskList.taskCount.ToString(), wgMjControllerTaskList2.taskCount.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strControlTaskList, listViewItem.Text, false);
						if (wgMjControllerTaskList2.taskCount == 0)
						{
							this.checkParam(icDesc.doorControlDesc(wgMjControllerConfigure2.DoorControlGet(this.control4Check.GetDoorNO(listViewItem.Text))), icDesc.doorControlDesc(wgMjControllerConfigure.DoorControlGet(this.control4Check.GetDoorNO(listViewItem.Text))), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strDoorControl, listViewItem.Text, false);
						}
						this.checkParam(wgMjControllerConfigure2.DoorDelayGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), wgMjControllerConfigure.DoorDelayGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strDoorDelay, listViewItem.Text, false);
						if (wgMjControllerConfigure.DoorInterlockGet(this.control4Check.GetDoorNO(listViewItem.Text)) > 0)
						{
							InfoRow expr_B35 = infoRow;
							expr_B35.detail += string.Format("\r\n--{0}", CommonStr.strInterLock);
							InfoRow expr_B56 = infoRow;
							expr_B56.information += string.Format("{0};", CommonStr.strInterLock);
						}
						this.checkParam(wgMjControllerConfigure2.DoorInterlockGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), wgMjControllerConfigure.DoorInterlockGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strInterLock, listViewItem.Text, false);
						if (wgMjController.GetControllerType(this.control4Check.ControllerSN) == 4)
						{
							if (wgMjControllerConfigure.ReaderPasswordGet(this.control4Check.GetDoorNO(listViewItem.Text)) > 0)
							{
								InfoRow expr_C1C = infoRow;
								expr_C1C.detail += string.Format("\r\n--{0}", CommonStr.strPasswordKeypad);
								InfoRow expr_C3D = infoRow;
								expr_C3D.information += string.Format("{0};", CommonStr.strPasswordKeypad);
							}
							this.checkParam(wgMjControllerConfigure2.ReaderPasswordGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), wgMjControllerConfigure.ReaderPasswordGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strPasswordKeypad, listViewItem.Text, false);
						}
						else
						{
							if (wgMjControllerConfigure.ReaderPasswordGet((this.control4Check.GetDoorNO(listViewItem.Text) - 1) * 2 + 1) > 0)
							{
								InfoRow expr_CF8 = infoRow;
								expr_CF8.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strInDoor, CommonStr.strPasswordKeypad);
								InfoRow expr_D1E = infoRow;
								expr_D1E.information += string.Format("{0}:{1};", CommonStr.strInDoor, CommonStr.strPasswordKeypad);
							}
							if (wgMjControllerConfigure.ReaderPasswordGet((this.control4Check.GetDoorNO(listViewItem.Text) - 1) * 2 + 2) > 0)
							{
								InfoRow expr_D64 = infoRow;
								expr_D64.detail += string.Format("\r\n--{0}:\t{1}", CommonStr.strExitDoor, CommonStr.strPasswordKeypad);
								InfoRow expr_D8A = infoRow;
								expr_D8A.information += string.Format("{0}:{1};", CommonStr.strExitDoor, CommonStr.strPasswordKeypad);
							}
							this.checkParam(wgMjControllerConfigure2.ReaderPasswordGet((this.control4Check.GetDoorNO(listViewItem.Text) - 1) * 2 + 1).ToString(), wgMjControllerConfigure.ReaderPasswordGet((this.control4Check.GetDoorNO(listViewItem.Text) - 1) * 2 + 1).ToString(), string.Concat(new string[]
							{
								"[",
								this.control4Check.ControllerSN.ToString(),
								"]",
								CommonStr.strInDoor,
								" ",
								CommonStr.strPasswordKeypad
							}), listViewItem.Text, false);
							this.checkParam(wgMjControllerConfigure2.ReaderPasswordGet((this.control4Check.GetDoorNO(listViewItem.Text) - 1) * 2 + 2).ToString(), wgMjControllerConfigure.ReaderPasswordGet((this.control4Check.GetDoorNO(listViewItem.Text) - 1) * 2 + 2).ToString(), string.Concat(new string[]
							{
								"[",
								this.control4Check.ControllerSN.ToString(),
								"]",
								CommonStr.strExitDoor,
								" ",
								CommonStr.strPasswordKeypad
							}), listViewItem.Text, false);
						}
						if (wgMjControllerConfigure.receventPB > 0)
						{
							InfoRow expr_F13 = infoRow;
							expr_F13.detail += string.Format("\r\n--{0}", CommonStr.strRecordButtonEvent);
							InfoRow expr_F34 = infoRow;
							expr_F34.information += string.Format("{0};", CommonStr.strRecordButtonEvent);
						}
						this.checkParam(wgMjControllerConfigure2.receventPB.ToString(), wgMjControllerConfigure.receventPB.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strRecordButtonEvent, listViewItem.Text, true);
						if (wgMjControllerConfigure.receventDS > 0)
						{
							InfoRow expr_FB1 = infoRow;
							expr_FB1.detail += string.Format("\r\n--{0}", CommonStr.strRecordDoorStatusEvent);
							InfoRow expr_FD2 = infoRow;
							expr_FD2.information += string.Format("{0};", CommonStr.strRecordDoorStatusEvent);
						}
						this.checkParam(wgMjControllerConfigure2.receventDS.ToString(), wgMjControllerConfigure.receventDS.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strRecordDoorStatusEvent, listViewItem.Text, true);
						if (wgMjControllerConfigure.receventWarn > 0)
						{
							InfoRow expr_104F = infoRow;
							expr_104F.detail += string.Format("\r\n--{0}", CommonStr.strRecordWarnEvent);
							InfoRow expr_1070 = infoRow;
							expr_1070.information += string.Format("{0};", CommonStr.strRecordWarnEvent);
						}
						this.checkParam(wgMjControllerConfigure2.receventWarn.ToString(), wgMjControllerConfigure.receventWarn.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strRecordWarnEvent, listViewItem.Text, true);
						if (wgMjControllerConfigure.antiback > 0)
						{
							InfoRow expr_10ED = infoRow;
							expr_10ED.detail += string.Format("\r\n--{0}", CommonStr.strAntiBack);
							InfoRow expr_110E = infoRow;
							expr_110E.information += string.Format("{0};", CommonStr.strAntiBack);
						}
						this.checkParam(wgMjControllerConfigure2.antiback.ToString(), wgMjControllerConfigure.antiback.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strAntiBack, listViewItem.Text, true);
						if (wgMjControllerConfigure.DoorDisableTimesegMinGet(this.control4Check.GetDoorNO(listViewItem.Text)) > 1)
						{
							InfoRow expr_119C = infoRow;
							expr_119C.detail += string.Format("\r\n--{0}", CommonStr.strDisableControlSeg);
							InfoRow expr_11BD = infoRow;
							expr_11BD.information += string.Format("{0};", CommonStr.strDisableControlSeg);
						}
						this.checkParam(wgMjControllerConfigure2.DoorDisableTimesegMinGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), wgMjControllerConfigure.DoorDisableTimesegMinGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strDisableControlSeg, listViewItem.Text, true);
						if (wgMjControllerConfigure.indoorPersonsMax > 0)
						{
							InfoRow expr_125C = infoRow;
							expr_125C.detail += string.Format("\r\n--{0}", CommonStr.strIndoorPersonsMax);
							InfoRow expr_127D = infoRow;
							expr_127D.information += string.Format("{0};", CommonStr.strIndoorPersonsMax);
						}
						this.checkParam(wgMjControllerConfigure2.indoorPersonsMax.ToString(), wgMjControllerConfigure.indoorPersonsMax.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strIndoorPersonsMax, listViewItem.Text, true);
						if ((wgMjControllerConfigure.warnSetup & -41) > 1)
						{
							InfoRow expr_12FD = infoRow;
							expr_12FD.detail += string.Format("\r\n--{0}", icDesc.WarnDetail(wgMjControllerConfigure.warnSetup & -41));
							InfoRow expr_1327 = infoRow;
							expr_1327.information += string.Format("{0};", icDesc.WarnDetail(wgMjControllerConfigure.warnSetup & -41));
						}
						this.checkParam(wgMjControllerConfigure2.warnSetup.ToString(), wgMjControllerConfigure.warnSetup.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strWarn, listViewItem.Text, true);
						if (wgMjControllerConfigure.MorecardNeedCardsGet(this.control4Check.GetDoorNO(listViewItem.Text)) > 1)
						{
							InfoRow expr_13BE = infoRow;
							expr_13BE.detail += string.Format("\r\n--{0}", CommonStr.strMoreCards);
							InfoRow expr_13DF = infoRow;
							expr_13DF.information += string.Format("{0};", CommonStr.strMoreCards);
						}
						this.checkParam(wgMjControllerConfigure2.MorecardNeedCardsGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), wgMjControllerConfigure.MorecardNeedCardsGet(this.control4Check.GetDoorNO(listViewItem.Text)).ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strMoreCards, listViewItem.Text, true);
						if (wgMjControllerConfigure.lockSwitchOption >= 1)
						{
							string text3 = "";
							for (int i = 0; i < 4; i++)
							{
								if ((wgMjControllerConfigure.lockSwitchOption & 1 << i) > 0)
								{
									if (text3 != "")
									{
										text3 += ",";
									}
									text3 = text3 + "#" + (i + 1).ToString();
								}
							}
							InfoRow expr_14E0 = infoRow;
							expr_14E0.detail += string.Format("\r\n--{0}({1})", CommonStr.strLockSwitch, text3);
							InfoRow expr_1503 = infoRow;
							expr_1503.information += string.Format("{0}({1});", CommonStr.strLockSwitch, text3);
						}
						this.checkParam(wgMjControllerConfigure2.lockSwitchOption.ToString(), wgMjControllerConfigure.lockSwitchOption.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strLockSwitch, listViewItem.Text, true);
						if (wgMjControllerConfigure.swipeGap >= 1)
						{
							InfoRow expr_1582 = infoRow;
							expr_1582.detail += string.Format("\r\n--{0}({1}s)", CommonStr.strSwipeGap, wgMjControllerConfigure.swipeGap);
							InfoRow expr_15AE = infoRow;
							expr_15AE.information += string.Format("{0}({1}s);", CommonStr.strSwipeGap, wgMjControllerConfigure.swipeGap);
						}
						this.checkParam(wgMjControllerConfigure2.swipeGap.ToString(), wgMjControllerConfigure.swipeGap.ToString(), "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strSwipeGap, listViewItem.Text, true);
						if (wgMjControllerConfigure.webPort != 0 && wgMjControllerConfigure.webPort != 65535)
						{
							string text4 = wgMjControllerConfigure.webDeviceName;
							text4 += (string.IsNullOrEmpty(wgMjControllerConfigure.webDeviceName) ? "" : ",");
							text4 += string.Format("{0},{1},{2}", wgMjControllerConfigure.webLanguage, (wgAppConfig.CultureInfoStr == "zh-CHS") ? wgMjControllerConfigure.webDateDisplayFormatCHS : wgMjControllerConfigure.webDateDisplayFormat, wgMjControllerConfigure.webPort.ToString());
							InfoRow expr_16C0 = infoRow;
							expr_16C0.detail += string.Format("\r\n--{0}({1})", CommonStr.strWEBEnabled, text4);
							InfoRow expr_16E3 = infoRow;
							expr_16E3.information += string.Format("{0}({1});", CommonStr.strWEBEnabled, text4);
						}
						if (wgMjControllerConfigure.SpecialCard_Mother1 != 0L && wgMjControllerConfigure.SpecialCard_Mother1 != (long)((ulong)-1))
						{
							InfoRow expr_171A = infoRow;
							expr_171A.detail += string.Format("\r\n--***{0}1({1})", CommonStr.strSpecialCardMother, wgMjControllerConfigure.SpecialCard_Mother1.ToString());
							InfoRow expr_174A = infoRow;
							expr_174A.information += string.Format("***{0}1({1});", CommonStr.strSpecialCardMother, wgMjControllerConfigure.SpecialCard_Mother1.ToString());
						}
						if (wgMjControllerConfigure.SpecialCard_Mother2 != 0L && wgMjControllerConfigure.SpecialCard_Mother2 != (long)((ulong)-1))
						{
							InfoRow expr_178E = infoRow;
							expr_178E.detail += string.Format("\r\n--***{0}2({1})", CommonStr.strSpecialCardMother, wgMjControllerConfigure.SpecialCard_Mother2.ToString());
							InfoRow expr_17BE = infoRow;
							expr_17BE.information += string.Format("***{0}2({1});", CommonStr.strSpecialCardMother, wgMjControllerConfigure.SpecialCard_Mother2.ToString());
						}
						if (wgMjControllerConfigure.SpecialCard_OnlyOpen1 != 0L && wgMjControllerConfigure.SpecialCard_OnlyOpen1 != (long)((ulong)-1))
						{
							InfoRow expr_1802 = infoRow;
							expr_1802.detail += string.Format("\r\n--***{0}1({1})", CommonStr.strSpecialCardSuper, wgMjControllerConfigure.SpecialCard_OnlyOpen1.ToString());
							InfoRow expr_1832 = infoRow;
							expr_1832.information += string.Format("***{0}1({1});", CommonStr.strSpecialCardSuper, wgMjControllerConfigure.SpecialCard_OnlyOpen1.ToString());
						}
						if (wgMjControllerConfigure.SpecialCard_OnlyOpen2 != 0L && wgMjControllerConfigure.SpecialCard_OnlyOpen2 != (long)((ulong)-1))
						{
							InfoRow expr_1876 = infoRow;
							expr_1876.detail += string.Format("\r\n--***{0}2({1})", CommonStr.strSpecialCardSuper, wgMjControllerConfigure.SpecialCard_OnlyOpen2.ToString());
							InfoRow expr_18A6 = infoRow;
							expr_18A6.information += string.Format("***{0}2({1});", CommonStr.strSpecialCardSuper, wgMjControllerConfigure.SpecialCard_OnlyOpen2.ToString());
						}
						if ((wgMjControllerConfigure.fire_broadcast_receive != 0 && (long)wgMjControllerConfigure.fire_broadcast_receive != 255L) || (wgMjControllerConfigure.fire_broadcast_send != 0 && (long)wgMjControllerConfigure.fire_broadcast_send != 255L))
						{
							InfoRow expr_1907 = infoRow;
							expr_1907.detail += string.Format("\r\n--***{0}({1}s,#{2})", CommonStr.strFireSignalShare, wgMjControllerConfigure.fire_broadcast_receive.ToString(), wgMjControllerConfigure.fire_broadcast_send.ToString());
							InfoRow expr_1946 = infoRow;
							expr_1946.information += string.Format("***{0}({1}s,#{2});", CommonStr.strFireSignalShare, wgMjControllerConfigure.fire_broadcast_receive.ToString(), wgMjControllerConfigure.fire_broadcast_send.ToString());
						}
						this.checkParam((string.Format("({0}s,{1})", wgMjControllerConfigure2.fire_broadcast_receive.ToString(), wgMjControllerConfigure2.fire_broadcast_send.ToString()) != "(0s,0)") ? "1" : "0", (string.Format("({0}s,{1})", wgMjControllerConfigure.fire_broadcast_receive.ToString(), wgMjControllerConfigure.fire_broadcast_send.ToString()) != "(0s,0)") ? "1" : "0", "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strFireSignalShare, listViewItem.Text, true);
						if ((wgMjControllerConfigure.interlock_broadcast_receive != 0 && (long)wgMjControllerConfigure.interlock_broadcast_receive != 255L) || (wgMjControllerConfigure.interlock_broadcast_send != 0 && (long)wgMjControllerConfigure.interlock_broadcast_send != 255L))
						{
							InfoRow expr_1A6B = infoRow;
							expr_1A6B.detail += string.Format("\r\n--***{0}({1}s,#{2})", CommonStr.strInterLockShare, wgMjControllerConfigure.interlock_broadcast_receive.ToString(), wgMjControllerConfigure.interlock_broadcast_send.ToString());
							InfoRow expr_1AAA = infoRow;
							expr_1AAA.information += string.Format("***{0}({1}s,#{2});", CommonStr.strInterLockShare, wgMjControllerConfigure.interlock_broadcast_receive.ToString(), wgMjControllerConfigure.interlock_broadcast_send.ToString());
						}
						this.checkParam((string.Format("({0}s,{1})", wgMjControllerConfigure2.interlock_broadcast_receive.ToString(), wgMjControllerConfigure2.interlock_broadcast_send.ToString()) != "(0s,0)") ? "1" : "0", (string.Format("({0}s,{1})", wgMjControllerConfigure.interlock_broadcast_receive.ToString(), wgMjControllerConfigure.interlock_broadcast_send.ToString()) != "(0s,0)") ? "1" : "0", "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strInterLockShare, listViewItem.Text, true);
						if (wgMjControllerConfigure.antiback_broadcast_send != 0 && (long)wgMjControllerConfigure.antiback_broadcast_send != 255L)
						{
							InfoRow expr_1BB5 = infoRow;
							expr_1BB5.detail += string.Format("\r\n--***{0}(#{1})", CommonStr.strAntibackShare, wgMjControllerConfigure.antiback_broadcast_send.ToString());
							InfoRow expr_1BE5 = infoRow;
							expr_1BE5.information += string.Format("***{0}(#{1});", CommonStr.strAntibackShare, wgMjControllerConfigure.antiback_broadcast_send.ToString());
						}
						this.checkParam((string.Format("({0}s,{1})", wgMjControllerConfigure2.antiback_broadcast_send.ToString(), wgMjControllerConfigure2.antiback_broadcast_send.ToString()) != "(0s,0)") ? "1" : "0", (string.Format("({0}s,{1})", wgMjControllerConfigure.antiback_broadcast_send.ToString(), wgMjControllerConfigure.antiback_broadcast_send.ToString()) != "(0s,0)") ? "1" : "0", "[" + this.control4Check.ControllerSN.ToString() + "]" + CommonStr.strAntibackShare, listViewItem.Text, true);
						if (wgMjControllerConfigure.indoorPersonsMax > 0 || (wgMjControllerConfigure.antiback_broadcast_send != 0 && (long)wgMjControllerConfigure.antiback_broadcast_send != 255L))
						{
							InfoRow expr_1CED = infoRow;
							expr_1CED.detail += string.Format("\r\n--***{0}({1})", CommonStr.strtotalPerson4AntibackShare, this.control4Check.runinfo.totalPerson4AntibackShare.ToString());
							InfoRow expr_1D27 = infoRow;
							expr_1D27.information += string.Format("***{0}({1});", CommonStr.strtotalPerson4AntibackShare, this.control4Check.runinfo.totalPerson4AntibackShare.ToString());
						}
						if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("DISPLAY_NEWEST_SWIPE")) && this.control4Check.runinfo.newRecordsNum > 0u)
						{
							MjRec mjRec = this.control4Check.runinfo.newSwipes[0];
							if (mjRec.addressIsReader)
							{
								if (this.ReaderName.ContainsKey(string.Format("{0}-{1}", mjRec.ControllerSN.ToString(), mjRec.ReaderNo.ToString())))
								{
									wgTools.WriteLine("ReaderName.ContainsKey(string.Format(");
									mjRec.address = this.ReaderName[string.Format("{0}-{1}", mjRec.ControllerSN.ToString(), mjRec.ReaderNo.ToString())];
								}
							}
							else
							{
								this.dvDoors4Check.RowFilter = string.Format("f_ControllerSN={0}  AND f_DoorNO={1}", mjRec.ControllerSN.ToString(), mjRec.DoorNo.ToString());
								if (this.dvDoors4Check.Count > 0)
								{
									infoRow.desc = this.dvDoors4Check[0]["f_DoorName"].ToString();
									mjRec.address = (this.dvDoors4Check[0]["f_DoorName"] as string);
								}
							}
							string text5 = mjRec.ToDisplayInfo();
							int num = text5.LastIndexOf("-");
							text5 = text5.Substring(0, num) + "\r\n  " + text5.Substring(num);
							InfoRow expr_1EED = infoRow;
							expr_1EED.detail += string.Format("\r\n\r\n--{0}", text5);
						}
						if (this.bNeedCheckLosePacket)
						{
							int num2 = 0;
							int num3 = 0;
							int num4 = 0;
							for (int j = 0; j < 200; j++)
							{
								num2++;
								if (this.control4Check.SpecialPingIP() == 1)
								{
									num3++;
								}
								else
								{
									num4++;
								}
							}
							if (num4 == 0)
							{
								wgUdpComm.triesTotal = 0L;
								wgTools.WriteLine("control.Test1024 Start");
								int num5 = 0;
								string text6 = "";
								int num6 = this.control4Check.test1024Write();
								if (num6 < 0)
								{
									text6 = text6 + CommonStr.strCommLargePacketWriteFailed + "\r\n";
								}
								num6 = this.control4Check.test1024Read(100u, ref num5);
								if (num6 < 0)
								{
									text6 = text6 + CommonStr.strCommLargePacketReadFailed + num6.ToString() + "\r\n";
								}
								if (wgUdpComm.triesTotal > 0L)
								{
									string text7 = text6;
									text6 = string.Concat(new string[]
									{
										text7,
										CommonStr.strCommLargePacketTryTimes,
										" = ",
										wgUdpComm.triesTotal.ToString(),
										"\r\n"
									});
								}
								wgTools.WriteLine("control.Test1024 End");
								if (text6 != "")
								{
									string text8 = text6;
									InfoRow expr_2038 = infoRow;
									expr_2038.detail += string.Format("\r\n--{0}", CommonStr.strCommLose);
									InfoRow expr_2059 = infoRow;
									expr_2059.information += string.Format("{0};", CommonStr.strCommLose);
									InfoRow expr_207A = infoRow;
									expr_207A.detail += string.Format("\r\n--{0}", text8);
									InfoRow expr_2098 = infoRow;
									expr_2098.information += string.Format("{0};", text8);
									wgRunInfoLog.addEvent(new InfoRow
									{
										desc = "[" + listViewItem.Text + "]" + CommonStr.strCommLose,
										information = string.Concat(new string[]
										{
											"[",
											this.control4Check.ControllerSN.ToString(),
											"]",
											CommonStr.strCommLose,
											": ",
											text8
										}),
										category = 501
									});
								}
								else
								{
									InfoRow expr_214E = infoRow;
									expr_214E.detail += string.Format("\r\n--{0}", CommonStr.strCommOK);
									InfoRow expr_216F = infoRow;
									expr_216F.information += string.Format("{0};", CommonStr.strCommOK);
								}
							}
							else
							{
								string text9 = string.Format(" {0}: {1}={2}, {3}={4}, {5} = {6}", new object[]
								{
									CommonStr.strCommPacket,
									CommonStr.strCommPacketSent,
									num2,
									CommonStr.strCommPacketReceived,
									num3,
									CommonStr.strCommPacketLost,
									num4
								}) + "\r\n";
								InfoRow expr_21FA = infoRow;
								expr_21FA.detail += string.Format("\r\n--{0}", CommonStr.strCommLose);
								InfoRow expr_221B = infoRow;
								expr_221B.information += string.Format("{0};", CommonStr.strCommLose);
								InfoRow expr_223C = infoRow;
								expr_223C.detail += string.Format("\r\n--{0}", text9);
								InfoRow expr_225A = infoRow;
								expr_225A.information += string.Format("{0};", text9);
								wgRunInfoLog.addEvent(new InfoRow
								{
									desc = "[" + listViewItem.Text + "]" + CommonStr.strCommLose,
									information = string.Concat(new string[]
									{
										"[",
										this.control4Check.ControllerSN.ToString(),
										"]",
										CommonStr.strCommLose,
										": ",
										text9
									}),
									category = 501
								});
							}
						}
						wgRunInfoLog.addEvent(infoRow);
					}
					this.displayNewestLog();
					wgTools.WriteLine("displayNewestLog");
				}
			}
		}

		private void itmDisplayStatusEntry(ListViewItem itm, int status)
		{
			try
			{
				if (itm != null)
				{
					itm.ImageIndex = status;
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void dispDoorStatusByIPComm(icController control, ListViewItem itm)
		{
			try
			{
				if (control.GetControllerRunInformationIP() <= 0)
				{
					base.Invoke(new frmConsole.itmDisplayStatus(this.itmDisplayStatusEntry), new object[]
					{
						itm,
						3
					});
				}
				else
				{
					base.Invoke(new frmConsole.itmDisplayStatus(this.itmDisplayStatusEntry), new object[]
					{
						itm,
						control.runinfo.GetDoorImageIndex(control.GetDoorNO(itm.Text))
					});
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnSetTime_Click(object sender, EventArgs e)
		{
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			if (this.lstDoors.SelectedItems.Count > 0 && XMessageBox.Show(sender.ToString() + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			this.btnStopOthers.BackColor = Color.Red;
			this.btnStopMonitor.BackColor = Color.Red;
			using (icController icController = new icController())
			{
				this.bStopComm = false;
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					if (this.bStopComm)
					{
						break;
					}
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					DateTime now = DateTime.Now;
					if (icController.AdjustTimeIP(now) <= 0)
					{
						wgRunInfoLog.addEventNotConnect(icController.ControllerSN, icController.IP, listViewItem);
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
							information = string.Format("{0}:{1}", CommonStr.strAdjustTimeOK, now.ToString("yyyy-MM-dd HH:mm:ss"))
						});
						this.dispDoorStatusByIPComm(icController, listViewItem);
					}
					this.displayNewestLog();
				}
			}
			if (this.btnRealtimeGetRecords.Text != CommonStr.strRealtimeGetting && this.btnServer.Text != CommonStr.strMonitoring)
			{
				this.btnStopOthers.BackColor = Color.Transparent;
				this.btnStopMonitor.BackColor = Color.Transparent;
			}
		}

		private void btnRemoteOpen_Click(object sender, EventArgs e)
		{
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			if (this.lstDoors.SelectedItems.Count > 0 && XMessageBox.Show(sender.ToString() + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			using (icController icController = new icController())
			{
				this.bStopComm = false;
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					if (this.bStopComm)
					{
						break;
					}
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					if (icController.RemoteOpenDoorIP(listViewItem.Text) <= 0)
					{
						wgRunInfoLog.addEventNotConnect(icController.ControllerSN, icController.IP, listViewItem);
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
							information = string.Format("{0}", CommonStr.strRemoteOpenDoorOK)
						});
						this.dispDoorStatusByIPComm(icController, listViewItem);
					}
					this.displayNewestLog();
				}
			}
		}

		private void btnDirectSetDoorControl()
		{
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				this.btnSelectAll_Click(null, null);
			}
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				return;
			}
			int num = -1;
			using (dfrmControllerDoorControlSet dfrmControllerDoorControlSet = new dfrmControllerDoorControlSet())
			{
				if (this.lstDoors.SelectedItems.Count == 1)
				{
					dfrmControllerDoorControlSet.Text = dfrmControllerDoorControlSet.Text + "--" + this.lstDoors.SelectedItems[0].Text;
				}
				else
				{
					dfrmControllerDoorControlSet.Text = string.Concat(new string[]
					{
						dfrmControllerDoorControlSet.Text,
						"--",
						CommonStr.strDoorsNum,
						" = ",
						this.lstDoors.SelectedItems.Count.ToString()
					});
					if (this.lstDoors.Items.Count == this.lstDoors.SelectedItems.Count)
					{
						dfrmControllerDoorControlSet.Text += CommonStr.strAll;
					}
				}
				if (dfrmControllerDoorControlSet.ShowDialog(this) == DialogResult.OK)
				{
					num = dfrmControllerDoorControlSet.doorControl;
				}
				if (num < 0)
				{
					return;
				}
			}
			using (icController icController = new icController())
			{
				this.bStopComm = false;
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					if (this.bStopComm)
					{
						break;
					}
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					if (icController.DirectSetDoorControlIP(listViewItem.Text, num) <= 0)
					{
						wgRunInfoLog.addEventNotConnect(icController.ControllerSN, icController.IP, listViewItem);
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
							information = string.Format("{0}{1}", CommonStr.strDirectSetDoorControl, icDesc.doorControlDesc(num))
						});
						this.dispDoorStatusByIPComm(icController, listViewItem);
					}
					this.displayNewestLog();
				}
			}
		}

		private void btnServer_Click(object sender, EventArgs e)
		{
			if (!this.btnServer.Enabled)
			{
				return;
			}
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			if (this.watching == null)
			{
				this.watching = new WatchingService();
				this.watching.EventHandler += new OnEventHandler(this.evtNewInfoCallBack);
			}
			this.timerUpdateDoorInfo.Enabled = false;
			Cursor.Current = Cursors.WaitCursor;
			this.watchingStartTime = DateTime.Now;
			Dictionary<int, icController> dictionary = new Dictionary<int, icController>();
			foreach (ListViewItem listViewItem in this.lstDoors.Items)
			{
				(listViewItem.Tag as frmConsole.DoorSetInfo).Selected = 0;
			}
			foreach (ListViewItem listViewItem2 in this.lstDoors.SelectedItems)
			{
				(listViewItem2.Tag as frmConsole.DoorSetInfo).Selected = 1;
				if (!dictionary.ContainsKey((listViewItem2.Tag as frmConsole.DoorSetInfo).ControllerSN))
				{
					wgTools.WriteLine("!selectedControllers.ContainsKey(control.ControllerSN)");
					this.control4btnServer = new icController();
					this.control4btnServer.GetInfoFromDBByDoorName(listViewItem2.Text);
					dictionary.Add(this.control4btnServer.ControllerSN, this.control4btnServer);
				}
			}
			if (dictionary.Count > 0)
			{
				wgTools.WriteLine("selectedControllers.Count=" + dictionary.Count.ToString());
				this.watching.WatchingController = dictionary;
				this.timerUpdateDoorInfo.Interval = 300;
				this.timerUpdateDoorInfo.Enabled = true;
				wgAppRunInfo.raiseAppRunInfoMonitors("1");
			}
			else
			{
				wgTools.WriteLine("selectedControllers.Count=" + dictionary.Count.ToString());
				this.watching.WatchingController = null;
				this.timerUpdateDoorInfo.Enabled = false;
				wgAppRunInfo.raiseAppRunInfoMonitors("0");
			}
			(sender as ToolStripButton).BackColor = Color.Green;
			this.btnStopMonitor.BackColor = Color.Red;
			this.btnStopOthers.BackColor = Color.Red;
			(sender as ToolStripButton).Text = CommonStr.strMonitoring;
			Cursor.Current = Cursors.Default;
		}

		private void updateSelectedDoorsStatus()
		{
			if (this.watching != null)
			{
				foreach (ListViewItem listViewItem in this.lstDoors.Items)
				{
					if ((listViewItem.Tag as frmConsole.DoorSetInfo).Selected > 0 && this.watching.WatchingController != null && this.watching.WatchingController.ContainsKey((listViewItem.Tag as frmConsole.DoorSetInfo).ControllerSN))
					{
						ControllerRunInformation runInfo = this.watching.GetRunInfo((listViewItem.Tag as frmConsole.DoorSetInfo).ControllerSN);
						if (runInfo == null)
						{
							if (DateTime.Now > this.watchingStartTime.AddSeconds(3.0) && listViewItem.ImageIndex != 3)
							{
								listViewItem.ImageIndex = 3;
							}
						}
						else if (DateTime.Now > runInfo.refreshTime.AddSeconds((double)WatchingService.unconnect_timeout_sec))
						{
							if (this.watching.lastGetInfoDateTime.AddMilliseconds((double)WatchingService.Watching_Cycle_ms) > DateTime.Now && listViewItem.ImageIndex != 3)
							{
								listViewItem.ImageIndex = 3;
							}
						}
						else
						{
							int imageIndex = listViewItem.ImageIndex;
							listViewItem.ImageIndex = runInfo.GetDoorImageIndex((listViewItem.Tag as frmConsole.DoorSetInfo).DoorNO);
							if (listViewItem.ImageIndex > 2 && imageIndex != listViewItem.ImageIndex)
							{
								this.btnWarnExisted.Visible = true;
								this.btnWarnExisted.BackColor = Color.Red;
								this.timerWarn.Enabled = true;
							}
						}
					}
				}
			}
		}

		private void timerUpdateDoorInfo_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timerUpdateDoorInfo.Enabled = false;
				if (this.watching != null)
				{
					this.updateSelectedDoorsStatus();
					if (this.QueRecText.Count > 0)
					{
						base.Invoke(new frmConsole.txtInfoHaveNewInfo(this.txtInfoHaveNewInfoEntry));
					}
					wgAppRunInfo.raiseAppRunInfoLoadNums(frmConsole.infoRowsCount.ToString());
					Application.DoEvents();
					this.pcCheckAccess_DealOpen();
					this.timerUpdateDoorInfo.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void evtNewInfoCallBack(string text)
		{
			wgTools.WgDebugWrite("Got text through callback! {0}", new object[]
			{
				text
			});
			frmConsole.receivedPktCount++;
			lock (this.QueRecText.SyncRoot)
			{
				this.QueRecText.Enqueue(text);
			}
		}

		public int getAllInfoRowsCount()
		{
			return frmConsole.infoRowsCount;
		}

		private void txtInfoHaveNewInfoEntry()
		{
			if (frmConsole.dealingTxt > 0)
			{
				return;
			}
			if (this.watching.WatchingController == null)
			{
				return;
			}
			Interlocked.Exchange(ref frmConsole.dealingTxt, 1);
			int num = 0;
			long ticks = DateTime.Now.Ticks;
			long num2 = 20000000L;
			long num3 = ticks + num2;
			while (this.QueRecText.Count > 0)
			{
				object info;
				lock (this.QueRecText.SyncRoot)
				{
					info = this.QueRecText.Dequeue();
				}
				this.txtInfoUpdateEntry(info);
				frmConsole.infoRowsCount++;
				num++;
				if (DateTime.Now.Ticks > num3)
				{
					num3 = DateTime.Now.Ticks + num2;
					if (this.dgvRunInfo.Rows.Count > 0)
					{
						this.dgvRunInfo.FirstDisplayedScrollingRowIndex = this.dgvRunInfo.Rows.Count - 1;
						this.dgvRunInfo.Rows[this.dgvRunInfo.Rows.Count - 1].Selected = true;
						Application.DoEvents();
						wgRunInfoLog.addEventSpecial2();
					}
					if (this.watching.WatchingController == null)
					{
						break;
					}
				}
			}
			wgRunInfoLog.addEventSpecial2();
			this.displayNewestLog();
			Application.DoEvents();
			Interlocked.Exchange(ref frmConsole.dealingTxt, 0);
		}

		private void txtInfoUpdateEntry(object info)
		{
			MjRec mjRec = new MjRec(info as string);
			if (mjRec.ControllerSN > 0u)
			{
				try
				{
					if (!this.watching.WatchingController.ContainsKey((int)mjRec.ControllerSN))
					{
						return;
					}
				}
				catch (Exception)
				{
					return;
				}
				InfoRow infoRow = new InfoRow();
				wgTools.WriteLine("new InfoRow");
				infoRow.category = mjRec.eventCategory;
				infoRow.desc = "";
				if (mjRec.addressIsReader)
				{
					if (this.ReaderName.ContainsKey(string.Format("{0}-{1}", mjRec.ControllerSN.ToString(), mjRec.ReaderNo.ToString())))
					{
						wgTools.WriteLine("ReaderName.ContainsKey(string.Format(");
						infoRow.desc = this.ReaderName[string.Format("{0}-{1}", mjRec.ControllerSN.ToString(), mjRec.ReaderNo.ToString())];
						mjRec.address = this.ReaderName[string.Format("{0}-{1}", mjRec.ControllerSN.ToString(), mjRec.ReaderNo.ToString())];
					}
					else
					{
						infoRow.desc = "";
					}
				}
				else
				{
					this.dvDoors4Watching.RowFilter = string.Format("f_ControllerSN={0}  AND f_DoorNO={1}", mjRec.ControllerSN.ToString(), mjRec.DoorNo.ToString());
					if (this.dvDoors4Watching.Count > 0)
					{
						infoRow.desc = this.dvDoors4Watching[0]["f_DoorName"].ToString();
						mjRec.address = (this.dvDoors4Watching[0]["f_DoorName"] as string);
					}
				}
				if (this.player != null)
				{
					if (mjRec.IsPassed)
					{
						SystemSounds.Beep.Play();
					}
					else
					{
						this.player.Play();
					}
				}
				infoRow.information = mjRec.ToDisplayInfo();
				infoRow.detail = mjRec.ToDisplayDetail();
				infoRow.MjRecStr = (info as string);
				wgRunInfoLog.addEventSpecial1(infoRow);
				if (wgRunInfoLog.logRecEventMode == 1)
				{
					wgAppConfig.wglogRecEventOfController(string.Format("Rec: {0}\r\n{1}", infoRow.MjRecStr, infoRow.detail).Replace("\t", ""));
				}
				else if (wgRunInfoLog.logRecEventMode == 2)
				{
					if (mjRec.addressIsReader)
					{
						wgAppConfig.wglogRecEventOfController(string.Format("Rec: {0}\r\n{1}", infoRow.MjRecStr, infoRow.detail).Replace("\t", ""));
					}
				}
				else if (wgRunInfoLog.logRecEventMode == 3 && !mjRec.addressIsReader)
				{
					wgAppConfig.wglogRecEventOfController(string.Format("Rec: {0}\r\n{1}", infoRow.MjRecStr, infoRow.detail).Replace("\t", ""));
				}
				this.txtInfoUpdateEntry4RealtimeGetRecords(mjRec);
				this.pcCheckAccess_DealNewRecord(mjRec);
			}
		}

		private void loadPhoto(long cardno)
		{
			if (!this.bMainWindowDisplay)
			{
				return;
			}
			this.pictureBox1.Visible = false;
			try
			{
				string photoFileName = wgAppConfig.getPhotoFileName(cardno);
				Image image = this.pictureBox1.Image;
				wgAppConfig.ShowMyImage(photoFileName, ref image);
				if (image != null)
				{
					this.pictureBox1.Image = image;
					this.pictureBox1.Visible = true;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dgvRunInfo_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dgvRunInfo.SelectedRows.Count > 0)
				{
					if (string.IsNullOrEmpty(this.oldInfoTitleString))
					{
						this.oldInfoTitleString = this.dataGridView2.Columns[0].HeaderText;
					}
					this.pictureBox1.Visible = false;
					this.txtInfo.Text = (this.dgvRunInfo.SelectedRows[0].Cells["f_Detail"].Value as string);
					this.richTxtInfo.Text = (this.dgvRunInfo.SelectedRows[0].Cells["f_Detail"].Value as string);
					this.dataGridView2.Columns[0].HeaderText = string.Concat(new string[]
					{
						this.oldInfoTitleString,
						"  [",
						this.dgvRunInfo.SelectedRows[0].Cells["f_RecID"].Value.ToString(),
						"/",
						this.dgvRunInfo.RowCount.ToString(),
						"]"
					});
					if (!string.IsNullOrEmpty(this.dgvRunInfo.SelectedRows[0].Cells["f_MjRecStr"].Value as string))
					{
						MjRec mjRec = new MjRec(this.dgvRunInfo.SelectedRows[0].Cells["f_MjRecStr"].Value as string);
						if (mjRec.IsSwipeRecord)
						{
							this.loadPhoto((long)((ulong)mjRec.CardID));
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void dgvRunInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				if (this.dgvRunInfo.Columns[e.ColumnIndex].Name.Equals("f_Category"))
				{
					string text = e.Value as string;
					if (text != null)
					{
						DataGridViewCell dataGridViewCell = this.dgvRunInfo[e.ColumnIndex, e.RowIndex];
						dataGridViewCell.ToolTipText = text;
						DataGridViewRow dataGridViewRow = this.dgvRunInfo.Rows[e.RowIndex];
						e.Value = InfoRow.getImage(text, ref dataGridViewRow);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.lstDoors.Items)
			{
				listViewItem.Selected = true;
			}
			this.lstDoors.Focus();
		}

		private void frmConsole_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				if (this.watching != null)
				{
					this.watching.StopWatch();
				}
				if (this.dfrmFind1 != null)
				{
					this.dfrmFind1.ReallyCloseForm();
				}
				if (this.dfrmWait1 != null)
				{
					this.dfrmWait1.Close();
				}
				if (this.frmMoreRecords != null)
				{
					this.frmMoreRecords.ReallyCloseForm();
				}
				if (this.frmMaps1 != null)
				{
					try
					{
						this.frmMaps1.Dispose();
						this.frmMaps1 = null;
					}
					catch (Exception ex)
					{
						wgAppConfig.wgLog(ex.ToString());
					}
				}
				if (this.frm4ShowLocate != null)
				{
					try
					{
						this.frm4ShowLocate.Dispose();
						this.frm4ShowLocate = null;
					}
					catch (Exception ex2)
					{
						wgAppConfig.wgLog(ex2.ToString());
					}
				}
				if (this.frm4ShowPersonsInside != null)
				{
					try
					{
						this.frm4ShowPersonsInside.Dispose();
						this.frm4ShowPersonsInside = null;
					}
					catch (Exception ex3)
					{
						wgAppConfig.wgLog(ex3.ToString());
					}
				}
				if (this.frm4PCCheckAccess != null)
				{
					try
					{
						this.frm4PCCheckAccess.Dispose();
						this.frm4PCCheckAccess = null;
					}
					catch (Exception ex4)
					{
						wgAppConfig.wgLog(ex4.ToString());
					}
				}
				this.control4uploadPrivilege = null;
				this.controlConfigure4uploadPrivilege = null;
				this.controlTaskList4uploadPrivilege = null;
				this.swipe4GetRecords = null;
				wgAppConfig.DisposeImage(this.pictureBox1.Image);
				wgAppRunInfo.raiseAppRunInfoMonitors("");
			}
			catch (Exception ex5)
			{
				wgAppConfig.wgLog(ex5.ToString());
			}
		}

		private void getRecordsFromController(int result)
		{
			this.control4getRecordsFromController = new icController();
			this.control4getRecordsFromController.GetInfoFromDBByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
			this.arrDealtController.Add(this.control4getRecordsFromController.ControllerSN, result);
			int i;
			for (i = this.dealtDoorIndex; i < this.arrSelectedDoors.Count; i++)
			{
				this.control4getRecordsFromController.GetInfoFromDBByDoorName(this.arrSelectedDoors[i].ToString());
				if (!this.arrDealtController.ContainsKey(this.control4getRecordsFromController.ControllerSN))
				{
					break;
				}
				if (this.arrDealtController[this.control4getRecordsFromController.ControllerSN] >= 0)
				{
					InfoRow infoRow = new InfoRow();
					infoRow.desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[i].ToString(), this.control4getRecordsFromController.ControllerSN);
					if (i == this.dealtDoorIndex)
					{
						infoRow.information = string.Format("{0}--[{1:d}]", CommonStr.strGetSwipeRecordOK, this.arrDealtController[this.control4getRecordsFromController.ControllerSN]);
						wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", this.arrSelectedDoors[i].ToString(), CommonStr.strGetSwipeRecordOK, this.arrDealtController[this.control4getRecordsFromController.ControllerSN]));
					}
					else
					{
						infoRow.information = string.Format("{0}", CommonStr.strAlreadyGotSwipeRecord);
					}
					wgRunInfoLog.addEvent(infoRow);
				}
				else
				{
					foreach (ListViewItem listViewItem in this.lstDoors.Items)
					{
						if (listViewItem.Text == this.arrSelectedDoors[i].ToString())
						{
							wgRunInfoLog.addEventNotConnect(this.control4getRecordsFromController.ControllerSN, this.control4getRecordsFromController.IP, listViewItem);
							wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}", listViewItem.Text, CommonStr.strCommFail));
							break;
						}
					}
				}
			}
			if (i < this.arrSelectedDoors.Count)
			{
				this.dealtDoorIndex = i;
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), this.control4getRecordsFromController.ControllerSN),
					information = string.Format("{0}", CommonStr.strGetSwipeRecordStart)
				});
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), CommonStr.strGettingSwipeRecord));
			}
			else
			{
				this.dealtDoorIndex = i;
				if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.Stop)
				{
					this.btnRealtimeGetRecords.Enabled = true;
					if (this.btnRealtimeGetRecords.Text != CommonStr.strRealtimeGetting && this.btnServer.Text != CommonStr.strMonitoring)
					{
						this.btnStopOthers.BackColor = Color.Transparent;
						this.btnStopMonitor.BackColor = Color.Transparent;
					}
					this.btnGetRecords.Enabled = true;
					this.btnUpload.Enabled = true;
				}
			}
			this.displayNewestLog();
		}

		private int getRecordsNow()
		{
			this.swipe4GetRecords.Clear();
			return this.swipe4GetRecords.GetSwipeRecordsByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
		}

		private void uploadPrivilegeToController(int result)
		{
			this.control4uploadPrivilege.GetInfoFromDBByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
			this.arrDealtController.Add(this.control4uploadPrivilege.ControllerSN, result);
			int i;
			for (i = this.dealtDoorIndex; i < this.arrSelectedDoors.Count; i++)
			{
				this.control4uploadPrivilege.GetInfoFromDBByDoorName(this.arrSelectedDoors[i].ToString());
				if (!this.arrDealtController.ContainsKey(this.control4uploadPrivilege.ControllerSN))
				{
					break;
				}
				if (this.arrDealtController[this.control4uploadPrivilege.ControllerSN] >= 0)
				{
					InfoRow infoRow = new InfoRow();
					infoRow.desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[i].ToString(), this.control4uploadPrivilege.ControllerSN);
					if (i == this.dealtDoorIndex)
					{
						if ((this.CommOperateOption & 3) == 3)
						{
							infoRow.information = string.Format("{0}--[{1:d}]", CommonStr.strUploadAllOK, this.arrDealtController[this.control4uploadPrivilege.ControllerSN]);
							wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", this.arrSelectedDoors[i].ToString(), CommonStr.strUploadAllOK, this.arrDealtController[this.control4uploadPrivilege.ControllerSN]));
						}
						else if ((this.CommOperateOption & 1) > 0)
						{
							infoRow.information = string.Format("{0}--[{1:d}]", CommonStr.strUploadBasicConfigureOK, this.arrDealtController[this.control4uploadPrivilege.ControllerSN]);
							wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", this.arrSelectedDoors[i].ToString(), CommonStr.strUploadBasicConfigureOK, this.arrDealtController[this.control4uploadPrivilege.ControllerSN]));
						}
						else if ((this.CommOperateOption & 2) > 0)
						{
							infoRow.information = string.Format("{0}--[{1:d}]", CommonStr.strUploadPrivilegesOK, this.arrDealtController[this.control4uploadPrivilege.ControllerSN]);
							wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", this.arrSelectedDoors[i].ToString(), CommonStr.strUploadPrivilegesOK, this.arrDealtController[this.control4uploadPrivilege.ControllerSN]));
						}
					}
					else
					{
						infoRow.information = string.Format("{0}", CommonStr.strAlreadyUploadPrivileges);
					}
					wgRunInfoLog.addEvent(infoRow);
				}
				else
				{
					foreach (ListViewItem listViewItem in this.lstDoors.Items)
					{
						if (listViewItem.Text == this.arrSelectedDoors[i].ToString())
						{
							if (this.arrDealtController[this.control4uploadPrivilege.ControllerSN] == wgGlobal.ERR_PRIVILEGES_OVER200K)
							{
								InfoRow infoRow2 = new InfoRow();
								infoRow2.desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[i].ToString(), this.control4uploadPrivilege.ControllerSN);
								infoRow2.information = string.Format("{0}--[{1:d}]", wgTools.gADCT ? CommonStr.strUploadFail_200K : CommonStr.strUploadFail_40K, listViewItem.Text);
								wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}", this.arrSelectedDoors[i].ToString(), wgTools.gADCT ? CommonStr.strUploadFail_200K : CommonStr.strUploadFail_40K));
								wgRunInfoLog.addEvent(infoRow2);
								break;
							}
							wgRunInfoLog.addEventNotConnect(this.control4uploadPrivilege.ControllerSN, this.control4uploadPrivilege.IP, listViewItem);
							wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}", listViewItem.Text, CommonStr.strCommFail));
							break;
						}
					}
				}
			}
			if (i < this.arrSelectedDoors.Count)
			{
				this.dealtDoorIndex = i;
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), this.control4uploadPrivilege.ControllerSN),
					information = string.Format("{0}", CommonStr.strUploadStart)
				});
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), CommonStr.strUploadingPrivileges));
			}
			else
			{
				this.dealtDoorIndex = i;
				this.btnRealtimeGetRecords.Enabled = true;
				if (this.btnRealtimeGetRecords.Text != CommonStr.strRealtimeGetting && this.btnServer.Text != CommonStr.strMonitoring)
				{
					this.btnStopOthers.BackColor = Color.Transparent;
					this.btnStopMonitor.BackColor = Color.Transparent;
				}
				this.btnGetRecords.Enabled = true;
				this.btnUpload.Enabled = true;
			}
			this.displayNewestLog();
		}

		private int uploadPrivilegeNow(int Option)
		{
			int num = -1;
			try
			{
				this.controlConfigure4uploadPrivilege.Clear();
				this.controlTaskList4uploadPrivilege.Clear();
				this.controlHolidayList4uploadPrivilege.Clear();
				this.pr4uploadPrivilege.AllowUpload();
				string doorName = this.arrSelectedDoors[this.dealtDoorIndex].ToString();
				this.control4uploadPrivilege.GetInfoFromDBByDoorName(doorName);
				string text = "";
				string text2 = "";
				string value = null;
				int num2 = 3;
				int millisecondsTimeout = 300;
				for (int i = 0; i < num2; i++)
				{
					value = this.control4uploadPrivilege.GetProductInfoIP(ref text, ref text2);
					if (!string.IsNullOrEmpty(value))
					{
						break;
					}
					Thread.Sleep(millisecondsTimeout);
				}
				if (string.IsNullOrEmpty(value))
				{
					wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " control4uploadPrivilege.GetProductInfoIP Failed num =" + num.ToString(), new object[0]);
					num = -13;
					int result = num;
					return result;
				}
				if (string.IsNullOrEmpty(this.strAllProductsDriversInfo))
				{
					string text3;
					wgAppConfig.getSystemParamValue(48, out text3, out text3, out this.strAllProductsDriversInfo);
				}
				if (!string.IsNullOrEmpty(this.strAllProductsDriversInfo))
				{
					if (this.strAllProductsDriversInfo.IndexOf(text) < 0)
					{
						if (this.strAllProductsDriversInfo.IndexOf("SN") < 0)
						{
							this.strAllProductsDriversInfo += "\r\n";
						}
						this.strAllProductsDriversInfo += text;
						wgAppConfig.setSystemParamValue(48, "ConInfo", "", this.strAllProductsDriversInfo);
					}
				}
				else
				{
					this.strAllProductsDriversInfo = text;
					wgAppConfig.setSystemParamValue(48, "ConInfo", "", this.strAllProductsDriversInfo);
				}
				if ((Option & 1) > 0)
				{
					icControllerConfigureFromDB.getControllerConfigureFromDBByControllerID(this.control4uploadPrivilege.ControllerID, ref this.controlConfigure4uploadPrivilege, ref this.controlTaskList4uploadPrivilege, ref this.controlHolidayList4uploadPrivilege);
					if ((num = this.control4uploadPrivilege.UpdateConfigureIP(this.controlConfigure4uploadPrivilege)) <= 0)
					{
						wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " updateConfigureIP Failed num =" + num.ToString(), new object[0]);
						num = -13;
						int result = num;
						return result;
					}
					if (this.controlConfigure4uploadPrivilege.controlTaskList_enabled > 0 && (num = this.control4uploadPrivilege.UpdateControlTaskListIP(this.controlTaskList4uploadPrivilege)) <= 0)
					{
						wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " updateControlTaskListIP Failed num =" + num.ToString(), new object[0]);
						num = -13;
						int result = num;
						return result;
					}
					if (wgAppConfig.getParamValBoolByNO(121))
					{
						icControllerTimeSegList icControllerTimeSegList = new icControllerTimeSegList();
						if (wgAppConfig.getParamValBoolByNO(121))
						{
							icControllerTimeSegList.fillByDB();
						}
						if ((num = this.control4uploadPrivilege.UpdateControlTimeSegListIP(icControllerTimeSegList)) <= 0)
						{
							wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " updateControlTimeSegListIP Failed num =" + num.ToString(), new object[0]);
							num = -13;
							int result = num;
							return result;
						}
						if ((num = this.control4uploadPrivilege.UpdateHolidayListIP(this.controlHolidayList4uploadPrivilege.ToByte())) <= 0)
						{
							wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " UpdateHolidayListIP Failed num =" + num.ToString(), new object[0]);
							num = -13;
							int result = num;
							return result;
						}
					}
				}
				if ((Option & 2) > 0)
				{
					int controllerIDByDoorName = this.pr4uploadPrivilege.getControllerIDByDoorName(doorName);
					if (controllerIDByDoorName > 0)
					{
						num = this.pr4uploadPrivilege.getPrivilegeByID(controllerIDByDoorName);
						if (num < 0)
						{
							wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " pr4uploadPrivilege.getPrivilegeByID Failed num =" + num.ToString(), new object[0]);
							int result = num;
							return result;
						}
						num = this.pr4uploadPrivilege.upload(this.control4uploadPrivilege.ControllerSN, this.control4uploadPrivilege.IP, this.control4uploadPrivilege.PORT, doorName);
						if (num < 0)
						{
							wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " pr4uploadPrivilege.upload Failed num =" + num.ToString(), new object[0]);
							int result = num;
							return result;
						}
						string format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal =0,  f_lastConsoleUploadDateTime ={0}, f_lastConsoleUploadConsuemrsTotal ={1:d}, f_lastConsoleUploadPrivilege ={2:d}, f_lastConsoleUploadValidPrivilege ={3:d} WHERE f_ControllerID ={4:d}";
						string strSql = string.Format(format, new object[]
						{
							wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")),
							this.pr4uploadPrivilege.ConsumersTotal,
							this.pr4uploadPrivilege.PrivilegTotal,
							this.pr4uploadPrivilege.ValidPrivilege,
							controllerIDByDoorName
						});
						wgAppConfig.runUpdateSql(strSql);
					}
				}
				if ((Option & 1) > 0 && this.controlTaskList4uploadPrivilege.taskCount > 0)
				{
					num = this.control4uploadPrivilege.RenewControlTaskListIP();
					if (num < 0)
					{
						wgTools.WgDebugWrite(this.control4uploadPrivilege.ControllerSN.ToString() + " control4uploadPrivilege.renewControlTaskListIP Failed num =" + num.ToString(), new object[0]);
					}
				}
			}
			catch (Exception ex)
			{
				num = -1;
				wgAppConfig.wgLog(ex.ToString());
			}
			return num;
		}

		private void bkUploadAndGetRecords_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			if (this.CommOperate == "UPLOAD")
			{
				e.Result = this.uploadPrivilegeNow(this.CommOperateOption);
			}
			else if (this.CommOperate == "GETRECORDS")
			{
				e.Result = this.getRecordsNow();
			}
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void bkUploadAndGetRecords_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				XMessageBox.Show(CommonStr.strOperationCanceled);
				wgAppConfig.wgLog(CommonStr.strOperationCanceled);
				return;
			}
			if (e.Error != null)
			{
				string text = string.Format("An error occurred: {0}", e.Error.Message);
				XMessageBox.Show(text);
				return;
			}
			if (this.CommOperate == "UPLOAD")
			{
				this.uploadPrivilegeToController(int.Parse(e.Result.ToString()));
				if (this.dealtDoorIndex < this.arrSelectedDoors.Count)
				{
					this.bkUploadAndGetRecords.RunWorkerAsync();
					return;
				}
			}
			else if (this.CommOperate == "GETRECORDS")
			{
				this.getRecordsFromController(int.Parse(e.Result.ToString()));
				if (this.dealtDoorIndex < this.arrSelectedDoors.Count)
				{
					this.bkUploadAndGetRecords.RunWorkerAsync();
				}
			}
		}

		private void btnStopMonitor_Click(object sender, EventArgs e)
		{
			if (this.watching != null)
			{
				this.watching.WatchingController = null;
				this.timerUpdateDoorInfo.Enabled = false;
				wgAppRunInfo.raiseAppRunInfoMonitors("0");
			}
		}

		private void btnStopOthers_Click(object sender, EventArgs e)
		{
			if (this.watching != null)
			{
				this.watching.WatchingController = null;
				this.timerUpdateDoorInfo.Enabled = false;
				wgAppRunInfo.raiseAppRunInfoMonitors("0");
			}
			this.bStopComm = true;
			wgMjControllerPrivilege.StopUpload();
			wgMjControllerSwipeOperate.StopGetRecord();
			if (this.bkUploadAndGetRecords.IsBusy)
			{
				this.bkUploadAndGetRecords.CancelAsync();
			}
			lock (this.QueRecText.SyncRoot)
			{
				this.QueRecText.Clear();
			}
			this.btnServer.BackColor = Color.Transparent;
			this.btnServer.Text = this.strRealMonitor;
			Interlocked.Exchange(ref frmConsole.dealingTxt, 0);
			this.btnRealtimeGetRecords.Enabled = true;
			this.btnStopOthers.BackColor = Color.Transparent;
			this.btnStopMonitor.BackColor = Color.Transparent;
			this.btnGetRecords.Enabled = true;
			this.btnUpload.Enabled = true;
			this.btnServer.Enabled = true;
			this.btnStopOthers.BackColor = Color.Transparent;
			this.btnStopMonitor.BackColor = Color.Transparent;
			Cursor.Current = Cursors.Default;
		}

		private void cboZone_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dvDoors != null)
			{
				DataView dataView = this.dvDoors;
				string text;
				if (this.cboZone.SelectedIndex < 0 || (this.cboZone.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
				{
					dataView.RowFilter = "";
					text = "";
					if (this.listViewNotDisplay.Items.Count == 0)
					{
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.lstDoors.Items.Count.ToString());
						return;
					}
				}
				else
				{
					if (this.lstDoors.Items.Count + this.listViewNotDisplay.Items.Count > 100)
					{
						this.dfrmWait1.Show();
						this.dfrmWait1.Refresh();
					}
					this.lstDoors.BeginUpdate();
					this.listViewNotDisplay.BeginUpdate();
					foreach (ListViewItem listViewItem in this.lstDoors.Items)
					{
						this.lstDoors.Items.Remove(listViewItem);
						this.listViewNotDisplay.Items.Add(listViewItem);
					}
					this.listViewNotDisplay.EndUpdate();
					this.lstDoors.EndUpdate();
					dataView.RowFilter = "f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
					text = " f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
					int num = (int)this.arrZoneID[this.cboZone.SelectedIndex];
					int num2 = (int)this.arrZoneNO[this.cboZone.SelectedIndex];
					int zoneChildMaxNo = icControllerZone.getZoneChildMaxNo(this.cboZone.Text, this.arrZoneName, this.arrZoneNO);
					if (num2 > 0)
					{
						if (num2 >= zoneChildMaxNo)
						{
							dataView.RowFilter = string.Format(" f_ZoneID ={0:d} ", num);
							text = string.Format(" f_ZoneID ={0:d} ", num);
						}
						else
						{
							dataView.RowFilter = "";
							string text2 = "";
							for (int i = 0; i < this.arrZoneNO.Count; i++)
							{
								if ((int)this.arrZoneNO[i] <= zoneChildMaxNo && (int)this.arrZoneNO[i] >= num2)
								{
									if (text2 == "")
									{
										text2 += string.Format(" f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
									}
									else
									{
										text2 += string.Format(" OR f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
									}
								}
							}
							dataView.RowFilter = string.Format("  {0} ", text2);
							text = string.Format("  {0} ", text2);
						}
					}
					dataView.RowFilter = string.Format(" {0} ", text);
				}
				if (this.lstDoors.Items.Count + this.listViewNotDisplay.Items.Count > 100)
				{
					this.dfrmWait1.Show();
					this.dfrmWait1.Refresh();
				}
				this.lstDoors.BeginUpdate();
				this.listViewNotDisplay.BeginUpdate();
				foreach (ListViewItem listViewItem2 in this.listViewNotDisplay.Items)
				{
					if (text != "")
					{
						this.dvDoors.RowFilter = string.Format("({0}) AND (f_DoorName = {1})", text, wgTools.PrepareStr(listViewItem2.Text));
					}
					else
					{
						this.dvDoors.RowFilter = string.Format("f_DoorName = {0}", wgTools.PrepareStr(listViewItem2.Text));
					}
					if (this.dvDoors.Count > 0)
					{
						this.listViewNotDisplay.Items.Remove(listViewItem2);
						this.lstDoors.Items.Add(listViewItem2);
					}
				}
				this.listViewNotDisplay.EndUpdate();
				this.lstDoors.EndUpdate();
				this.dfrmWait1.Hide();
				wgTools.WriteLine("foreach (ListViewItem itm in listViewNotDisplay.Items)");
				wgAppRunInfo.raiseAppRunInfoLoadNums(this.lstDoors.Items.Count.ToString());
				return;
			}
			wgAppRunInfo.raiseAppRunInfoLoadNums("0");
		}

		private void bkDispDoorStatus_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			using (icController icController = new icController())
			{
				this.watchingDealtDoorIndex = 0;
				Thread.Sleep(100);
				while (this.watchingDealtDoorIndex > -1 && this.watchingDealtDoorIndex < this.arrSelectedDoors.Count && !this.bStopComm)
				{
					if (this.watchingDealtDoorIndex <= this.dealtDoorIndex)
					{
						icController.GetInfoFromDBByDoorName(this.arrSelectedDoors[this.watchingDealtDoorIndex].ToString());
						this.dispDoorStatusByIPComm(icController, (ListViewItem)this.arrSelectedDoorsItem[this.watchingDealtDoorIndex]);
						this.watchingDealtDoorIndex++;
					}
					else
					{
						Thread.Sleep(100);
					}
				}
			}
		}

		private void btnWarnExisted_Click(object sender, EventArgs e)
		{
			this.btnWarnExisted.Visible = false;
		}

		private void btnClearRunInfo_Click(object sender, EventArgs e)
		{
			(this.dgvRunInfo.DataSource as DataView).Table.Clear();
			this.txtInfo.Text = "";
			this.richTxtInfo.Text = "";
			this.pictureBox1.Visible = false;
			if (!string.IsNullOrEmpty(this.oldInfoTitleString))
			{
				this.dataGridView2.Columns[0].HeaderText = this.oldInfoTitleString;
			}
		}

		public void frmConsole_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (this.frm4PCCheckAccess != null && this.frm4PCCheckAccess.bDealing)
				{
					this.frm4PCCheckAccess.Focus();
				}
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
						this.dfrmFind1.StartPosition = FormStartPosition.Manual;
						this.dfrmFind1.Location = new Point(600, 8);
					}
					this.dfrmFind1.setObjtoFind(this.lstDoors, null);
				}
				if (e.Control && e.KeyValue == 65)
				{
					this.btnSelectAll.PerformClick();
				}
				if (e.Control && e.KeyValue == 48)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					if (this.btnRemoteOpen.Visible)
					{
						this.btnDirectSetDoorControl();
					}
				}
				if (e.Control && !e.Shift && e.KeyValue == 121)
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
						if (dfrmInputNewName.strNewName != "668899")
						{
							return;
						}
					}
					using (dfrmCommPSet dfrmCommPSet = new dfrmCommPSet())
					{
						dfrmCommPSet.Text = CommonStr.strSaveAsConfigureFile;
						if (dfrmCommPSet.ShowDialog(this) == DialogResult.OK)
						{
							if (string.IsNullOrEmpty(dfrmCommPSet.CurrentPwd))
							{
								wgAppConfig.UpdateKeyVal("CommCurrent", "");
							}
							else
							{
								wgAppConfig.UpdateKeyVal("CommCurrent", WGPacket.Ept(dfrmCommPSet.CurrentPwd));
								wgAppConfig.SaveNewXmlFile("CommCurrent", WGPacket.Ept(dfrmCommPSet.CurrentPwd));
							}
							wgTools.CommPStr = wgAppConfig.GetKeyVal("CommCurrent");
							wgAppConfig.wgLog(".pCurr_" + wgAppConfig.GetKeyVal("CommCurrent"));
						}
					}
				}
				if (e.Control && !e.Shift && e.KeyValue == 120)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					using (dfrmCommPSet dfrmCommPSet2 = new dfrmCommPSet())
					{
						dfrmCommPSet2.Text = CommonStr.strSetCommPassword;
						bool flag = false;
						if (dfrmCommPSet2.ShowDialog(this) == DialogResult.OK)
						{
							string keyVal = wgAppConfig.GetKeyVal("CommCurrent");
							if (string.IsNullOrEmpty(dfrmCommPSet2.CurrentPwd))
							{
								if (string.IsNullOrEmpty(keyVal))
								{
									flag = true;
								}
							}
							else if (string.Compare(WGPacket.Ept(dfrmCommPSet2.CurrentPwd), keyVal) == 0)
							{
								flag = true;
							}
							if (!flag)
							{
								XMessageBox.Show(this, CommonStr.strNewPwdNotAsSameInSystem, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
							if (flag)
							{
								this.UploadCommPassword(dfrmCommPSet2.CurrentPwd);
								wgTools.CommPStr = wgAppConfig.GetKeyVal("CommCurrent");
							}
						}
					}
				}
				if (e.Control && e.Alt && e.KeyValue == 49)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					using (dfrmInputNewName dfrmInputNewName2 = new dfrmInputNewName())
					{
						dfrmInputNewName2.Text = CommonStr.strRestorAllSwipeRecords;
						dfrmInputNewName2.setPasswordChar('*');
						if (dfrmInputNewName2.ShowDialog(this) != DialogResult.OK)
						{
							return;
						}
						if (dfrmInputNewName2.strNewName != "5678")
						{
							return;
						}
						this.RestoreAllSwipeInTheControllers();
					}
				}
				if (e.Control && e.Shift && e.KeyValue == 80)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					this.displayTools();
				}
				if (e.Control && e.Shift && e.KeyValue == 76)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					if (DateTime.Now > this.dtlstDoorViewChange.AddSeconds(3.0))
					{
						this.dtlstDoorViewChange = DateTime.Now;
						if (this.lstDoors.View == View.Details)
						{
							this.lstDoors.View = View.LargeIcon;
						}
						else if (this.lstDoors.View == View.LargeIcon)
						{
							this.lstDoors.View = View.List;
						}
						else if (this.lstDoors.View == View.List)
						{
							this.lstDoors.View = View.SmallIcon;
						}
						else if (this.lstDoors.View == View.SmallIcon)
						{
							this.lstDoors.View = View.Tile;
						}
						else if (this.lstDoors.View == View.Tile)
						{
							this.lstDoors.View = View.LargeIcon;
						}
						else
						{
							this.lstDoors.View = View.LargeIcon;
						}
						wgTools.WgDebugWrite(this.lstDoors.View.ToString(), new object[0]);
						wgAppConfig.UpdateKeyVal("CONSOLE_DOORVIEW", this.lstDoors.View.ToString());
					}
				}
				if (e.Control && e.KeyValue == 67)
				{
					string text = "";
					for (int i = 0; i < this.dgvRunInfo.Rows.Count; i++)
					{
						for (int j = 0; j < this.dgvRunInfo.ColumnCount; j++)
						{
							text = text + this.dgvRunInfo.Rows[i].Cells[j].Value.ToString().Replace("\r\n", ",") + "\t";
						}
						text += "\r\n";
					}
					Clipboard.SetDataObject(text, false);
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void RestoreAllSwipeInTheControllers()
		{
			using (icController icController = new icController())
			{
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					if (icController.RestoreAllSwipeInTheControllersIP() <= 0)
					{
						wgRunInfoLog.addEventNotConnect(icController.ControllerSN, icController.IP, listViewItem);
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
							information = string.Format("{0}", CommonStr.strRestorAllSwipeRecords)
						});
					}
					this.displayNewestLog();
				}
			}
		}

		private void UploadCommPassword(string pwd)
		{
			using (icController icController = new icController())
			{
				this.bStopComm = false;
				ArrayList arrayList = new ArrayList();
				byte[] array = new byte[1152];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				string text = "";
				if (!string.IsNullOrEmpty(pwd))
				{
					text = pwd.Substring(0, Math.Min(16, pwd.Length));
				}
				char[] array2 = text.PadRight(16, '\0').ToCharArray();
				int num = 16;
				int num2 = 0;
				while (num2 < 16 && num2 < array2.Length)
				{
					array[num] = (byte)(array2[num2] & 'ÿ');
					byte[] expr_8F_cp_0 = array;
					int expr_8F_cp_1 = 1024 + (num >> 3);
					expr_8F_cp_0[expr_8F_cp_1] |= (byte)(1 << (num & 7));
					num++;
					num2++;
				}
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					if (this.bStopComm)
					{
						break;
					}
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					if (arrayList.IndexOf(icController.ControllerSN) < 0)
					{
						arrayList.Add(icController.ControllerSN);
						if (string.IsNullOrEmpty(wgTools.CommPStr))
						{
							icController.UpdateConfigureCPUSuperIP(array, "");
						}
						else
						{
							icController.UpdateConfigureCPUSuperIP(array, WGPacket.Dpt(wgTools.CommPStr));
						}
						wgAppConfig.wgLog(".setComm_" + icController.ControllerSN.ToString());
					}
				}
			}
		}

		private void mnuCheck_Click(object sender, EventArgs e)
		{
			this.btnCheck.PerformClick();
		}

		private void mnuWarnReset_Click(object sender, EventArgs e)
		{
			using (icController icController = new icController())
			{
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					if (icController.WarnResetIP() <= 0)
					{
						wgRunInfoLog.addEventNotConnect(icController.ControllerSN, icController.IP, listViewItem);
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
							information = string.Format("{0}", sender.ToString())
						});
					}
					this.displayNewestLog();
				}
			}
		}

		private void timerWarn_Tick(object sender, EventArgs e)
		{
			this.timerWarn.Enabled = false;
			if (this.btnWarnExisted.Visible)
			{
				if (this.btnWarnExisted.BackColor == Color.Red)
				{
					this.btnWarnExisted.BackColor = Color.Transparent;
				}
				else
				{
					this.btnWarnExisted.BackColor = Color.Red;
				}
				SystemSounds.Beep.Play();
				this.timerWarn.Enabled = true;
			}
		}

		private void dgvRunInfo_KeyDown(object sender, KeyEventArgs e)
		{
			this.frmConsole_KeyDown(sender, e);
		}

		private void clearRunInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.btnClearRunInfo.PerformClick();
		}

		private void displayMoreSwipesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.frmMoreRecords != null && !this.frmMoreRecords.Visible)
				{
					this.frmMoreRecords.Close();
					this.frmMoreRecords = null;
				}
				if (this.frmMoreRecords == null)
				{
					this.frmMoreRecords = new frmWatchingMoreRecords();
					this.frmMoreRecords.tbRunInfoLog = this.tbRunInfoLog;
				}
				this.frmMoreRecords.Show(this);
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnMaps_Click(object sender, EventArgs e)
		{
			if (this.frmMaps1 != null)
			{
				try
				{
					this.frmMaps1.Dispose();
					this.frmMaps1 = null;
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
				}
			}
			this.frmMaps1 = new frmMaps();
			this.frmMaps1.lstDoors = this.lstDoors;
			this.frmMaps1.btnMonitor = this.btnServer;
			this.frmMaps1.contextMenuStrip1Doors = this.contextMenuStrip1Doors;
			this.frmMaps1.TopMost = true;
			this.frmMaps1.btnStop = this.btnStopOthers;
			this.frmMaps1.Show();
		}

		private void btnRealtimeGetRecords_Click(object sender, EventArgs e)
		{
			if (this.bkRealtimeGetRecords.IsBusy)
			{
				return;
			}
			if (!this.btnRealtimeGetRecords.Enabled)
			{
				return;
			}
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				if (!this.bDirectToRealtimeGet)
				{
					XMessageBox.Show(CommonStr.strSelectDoor);
				}
				return;
			}
			if (this.lstDoors.SelectedItems.Count > 0 && !this.bDirectToRealtimeGet && XMessageBox.Show(sender.ToString() + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			this.bDirectToRealtimeGet = false;
			this.btnStopOthers_Click(null, null);
			this.btnRealtimeGetRecords.Enabled = false;
			this.btnGetRecords.Enabled = false;
			this.btnUpload.Enabled = false;
			this.btnServer.Enabled = false;
			this.Refresh();
			this.arrSelectedDoors.Clear();
			this.arrSelectedDoorsItem.Clear();
			this.dealtDoorIndex = 0;
			this.arrDealtController.Clear();
			foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
			{
				this.arrSelectedDoors.Add(listViewItem.Text);
				this.arrSelectedDoorsItem.Add(listViewItem);
			}
			this.timerUpdateDoorInfo.Enabled = false;
			Dictionary<int, icController> dictionary = new Dictionary<int, icController>();
			this.needDelSwipeControllers = new Dictionary<int, int>();
			Cursor.Current = Cursors.WaitCursor;
			foreach (ListViewItem listViewItem2 in this.lstDoors.Items)
			{
				(listViewItem2.Tag as frmConsole.DoorSetInfo).Selected = 0;
			}
			this.realtimeGetRecordsSwipeIndexGot.Clear();
			this.selectedControllersSNOfRealtimeGetRecords.Clear();
			foreach (ListViewItem listViewItem3 in this.lstDoors.SelectedItems)
			{
				(listViewItem3.Tag as frmConsole.DoorSetInfo).Selected = 1;
				if (!dictionary.ContainsKey((listViewItem3.Tag as frmConsole.DoorSetInfo).ControllerSN))
				{
					wgTools.WriteLine("!selectedControllers.ContainsKey(control.ControllerSN)");
					this.control4Realtime = new icController();
					this.control4Realtime.GetInfoFromDBByDoorName(listViewItem3.Text);
					dictionary.Add(this.control4Realtime.ControllerSN, this.control4Realtime);
					this.realtimeGetRecordsSwipeIndexGot.Add(this.control4Realtime.ControllerSN, -1);
					this.selectedControllersSNOfRealtimeGetRecords.Add(this.control4Realtime.ControllerSN);
					this.needDelSwipeControllers.Add(this.control4Realtime.ControllerSN, 0);
				}
			}
			this.selectedControllersOfRealtimeGetRecords = dictionary;
			using (icController icController = new icController())
			{
				icController.GetInfoFromDBByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = "",
					information = string.Format("{0}", CommonStr.strRealtimeGetSwipeRecordStart)
				});
				wgRunInfoLog.addEvent(new InfoRow
				{
					desc = string.Format("{0}[{1:d}]", this.arrSelectedDoors[this.dealtDoorIndex].ToString(), icController.ControllerSN),
					information = string.Format("{0}", CommonStr.strGetSwipeRecordStart)
				});
			}
			this.displayNewestLog();
			this.bStopComm = false;
			this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.GetRecordFirst;
			this.dealtIndexOfDoorsNeedToGetRecords = -1;
			this.doorsNeedToGetRecords.Clear();
			this.bkRealtimeGetRecords.RunWorkerAsync();
			if (!this.bkDispDoorStatus.IsBusy)
			{
				this.bkDispDoorStatus.RunWorkerAsync();
			}
			(sender as ToolStripButton).BackColor = Color.LightGreen;
			(sender as ToolStripButton).Text = CommonStr.strRealtimeGetting;
			this.btnStopOthers.BackColor = Color.Red;
			this.btnStopMonitor.BackColor = Color.Red;
			Cursor.Current = Cursors.Default;
		}

		private void bkRealtimeGetRecords_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				BackgroundWorker backgroundWorker = sender as BackgroundWorker;
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
				if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.GetRecordFirst)
				{
					this.swipe4GetRecords.Clear();
					int swipeRecordsByDoorName = this.swipe4GetRecords.GetSwipeRecordsByDoorName(this.arrSelectedDoors[this.dealtDoorIndex].ToString());
					if (swipeRecordsByDoorName >= 0 && this.realtimeGetRecordsSwipeIndexGot.ContainsKey(this.swipe4GetRecords.ControllerSN))
					{
						this.realtimeGetRecordsSwipeIndexGot[this.swipe4GetRecords.ControllerSN] = this.swipe4GetRecords.lastRecordFlashIndex;
					}
					e.Result = swipeRecordsByDoorName;
				}
				else if (this.stepOfRealtimeGetRecords != frmConsole.StepOfRealtimeGetReocrds.GetFinished && this.stepOfRealtimeGetRecords != frmConsole.StepOfRealtimeGetReocrds.StartMonitoring)
				{
					if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.WaitGetRecord)
					{
						while (!this.bStopComm)
						{
							if (this.doorsNeedToGetRecords.Count > 0)
							{
								if (this.dealtIndexOfDoorsNeedToGetRecords + 1 < this.doorsNeedToGetRecords.Count)
								{
									int controllerSN;
									using (icController icController = new icController())
									{
										icController.GetInfoFromDBByDoorName(this.doorsNeedToGetRecords[this.dealtIndexOfDoorsNeedToGetRecords + 1].ToString());
										controllerSN = icController.ControllerSN;
									}
									if (this.realtimeGetRecordsSwipeIndexGot.ContainsKey(controllerSN) && this.realtimeGetRecordsSwipeIndexGot[controllerSN] > 0 && this.selectedControllersOfRealtimeGetRecords.ContainsKey(controllerSN) && this.selectedControllersOfRealtimeGetRecords[controllerSN].GetControllerRunInformationIP() > 0 && (ulong)(this.selectedControllersOfRealtimeGetRecords[controllerSN].runinfo.lastGetRecordIndex + this.selectedControllersOfRealtimeGetRecords[controllerSN].runinfo.newRecordsNum) >= (ulong)((long)this.realtimeGetRecordsSwipeIndexGot[controllerSN]))
									{
										this.selectedControllersOfRealtimeGetRecords[controllerSN].UpdateLastGetRecordLocationIP((uint)this.realtimeGetRecordsSwipeIndexGot[controllerSN]);
										this.needDelSwipeControllers[controllerSN] = 0;
									}
									this.swipe4GetRecords.Clear();
									int swipeRecordsByDoorName2 = this.swipe4GetRecords.GetSwipeRecordsByDoorName(this.doorsNeedToGetRecords[this.dealtIndexOfDoorsNeedToGetRecords + 1].ToString());
									e.Result = swipeRecordsByDoorName2;
									if (swipeRecordsByDoorName2 >= 0 && this.realtimeGetRecordsSwipeIndexGot.ContainsKey(this.swipe4GetRecords.ControllerSN))
									{
										this.realtimeGetRecordsSwipeIndexGot[this.swipe4GetRecords.ControllerSN] = this.swipe4GetRecords.lastRecordFlashIndex;
										break;
									}
									break;
								}
								else if (this.doorsNeedToGetRecords.Count > 1000)
								{
									this.doorsNeedToGetRecords.Clear();
									this.dealtIndexOfDoorsNeedToGetRecords = -1;
								}
							}
							else
							{
								Thread.Sleep(1000);
							}
						}
					}
					else if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.DelSwipe && this.realtimeGetRecordsSwipeIndexGot.Count > 0)
					{
						foreach (object current in this.selectedControllersSNOfRealtimeGetRecords)
						{
							int key = (int)current;
							if (this.realtimeGetRecordsSwipeIndexGot.ContainsKey(key) && this.realtimeGetRecordsSwipeIndexGot[key] > 0 && this.selectedControllersOfRealtimeGetRecords.ContainsKey(key) && this.needDelSwipeControllers[key] == 1 && this.selectedControllersOfRealtimeGetRecords[key].GetControllerRunInformationIP() > 0 && (ulong)(this.selectedControllersOfRealtimeGetRecords[key].runinfo.lastGetRecordIndex + this.selectedControllersOfRealtimeGetRecords[key].runinfo.newRecordsNum) >= (ulong)((long)this.realtimeGetRecordsSwipeIndexGot[key]))
							{
								this.selectedControllersOfRealtimeGetRecords[key].UpdateLastGetRecordLocationIP((uint)this.realtimeGetRecordsSwipeIndexGot[key]);
							}
						}
					}
				}
				if (backgroundWorker.CancellationPending)
				{
					e.Cancel = true;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void bkRealtimeGetRecords_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Cancelled)
				{
					wgAppConfig.wgLog(CommonStr.strOperationCanceled);
				}
				else if (e.Error != null)
				{
					string strMsg = string.Format("An error occurred: {0}", e.Error.Message);
					wgAppConfig.wgLog(strMsg);
				}
				else if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.GetRecordFirst)
				{
					this.getRecordsFromController(int.Parse(e.Result.ToString()));
					if (!this.bStopComm)
					{
						if (this.dealtDoorIndex == this.arrSelectedDoors.Count)
						{
							this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.GetFinished;
						}
						this.bkRealtimeGetRecords.RunWorkerAsync();
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = CommonStr.strStopComm,
							information = CommonStr.strStopComm
						});
						this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.Stop;
						this.btnRealtimeGetRecords.BackColor = Color.Transparent;
						this.btnRealtimeGetRecords.Text = CommonStr.strRealtimeGetRecords;
						wgAppRunInfo.raiseAppRunInfoCommStatus(CommonStr.strStopComm);
					}
				}
				else if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.GetFinished)
				{
					wgAppRunInfo.raiseAppRunInfoCommStatus("");
					if (this.watching == null)
					{
						this.watching = new WatchingService();
						this.watching.EventHandler += new OnEventHandler(this.evtNewInfoCallBack);
					}
					this.timerUpdateDoorInfo.Enabled = false;
					this.watchingStartTime = DateTime.Now;
					wgTools.WriteLine("selectedControllers.Count=" + this.selectedControllersOfRealtimeGetRecords.Count.ToString());
					this.watching.WatchingController = this.selectedControllersOfRealtimeGetRecords;
					this.timerUpdateDoorInfo.Interval = 300;
					this.timerUpdateDoorInfo.Enabled = true;
					wgAppRunInfo.raiseAppRunInfoMonitors("2");
					this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.StartMonitoring;
					this.bkRealtimeGetRecords.RunWorkerAsync();
				}
				else if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.StartMonitoring)
				{
					this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.WaitGetRecord;
					this.bkRealtimeGetRecords.RunWorkerAsync();
				}
				else if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.WaitGetRecord)
				{
					if (this.bStopComm)
					{
						this.dealtDoorIndex = 0;
						this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.DelSwipe;
						this.bkRealtimeGetRecords.RunWorkerAsync();
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}", this.doorsNeedToGetRecords[this.dealtIndexOfDoorsNeedToGetRecords + 1].ToString()),
							information = string.Format("{0}", CommonStr.strAlreadyGotSwipeRecord)
						});
						if (this.dgvRunInfo.Rows.Count > 0)
						{
							this.dgvRunInfo.FirstDisplayedScrollingRowIndex = this.dgvRunInfo.Rows.Count - 1;
							this.dgvRunInfo.Rows[this.dgvRunInfo.Rows.Count - 1].Selected = true;
							Application.DoEvents();
						}
						this.dealtIndexOfDoorsNeedToGetRecords++;
						this.bkRealtimeGetRecords.RunWorkerAsync();
					}
				}
				else if (this.stepOfRealtimeGetRecords == frmConsole.StepOfRealtimeGetReocrds.DelSwipe)
				{
					this.stepOfRealtimeGetRecords = frmConsole.StepOfRealtimeGetReocrds.Stop;
					this.btnRealtimeGetRecords.BackColor = Color.Transparent;
					this.btnRealtimeGetRecords.Text = CommonStr.strRealtimeGetRecords;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void txtInfoUpdateEntry4RealtimeGetRecords(MjRec mjrec)
		{
			if (this.stepOfRealtimeGetRecords != frmConsole.StepOfRealtimeGetReocrds.Stop && this.realtimeGetRecordsSwipeIndexGot.ContainsKey((int)mjrec.ControllerSN))
			{
				if ((ulong)mjrec.IndexInDataFlash == (ulong)((long)this.realtimeGetRecordsSwipeIndexGot[(int)mjrec.ControllerSN]))
				{
					if (icSwipeRecord.AddNewSwipe_SynConsumerID(mjrec) >= 0)
					{
						this.realtimeGetRecordsSwipeIndexGot[(int)mjrec.ControllerSN] = (int)(mjrec.IndexInDataFlash + 1u);
						this.needDelSwipeControllers[(int)mjrec.ControllerSN] = 1;
						return;
					}
				}
				else if ((ulong)mjrec.IndexInDataFlash > (ulong)((long)this.realtimeGetRecordsSwipeIndexGot[(int)mjrec.ControllerSN]))
				{
					this.dvDoors4Watching.RowFilter = string.Format("f_ControllerSN={0}  AND f_DoorNO={1}", mjrec.ControllerSN.ToString(), mjrec.DoorNo.ToString());
					if (this.dvDoors4Watching.Count > 0)
					{
						if (this.doorsNeedToGetRecords.IndexOf(this.dvDoors4Watching[0]["f_DoorName"].ToString(), Math.Max(0, this.dealtIndexOfDoorsNeedToGetRecords + 1)) >= 0)
						{
							return;
						}
						this.doorsNeedToGetRecords.Add(this.dvDoors4Watching[0]["f_DoorName"].ToString());
					}
				}
			}
		}

		private void locateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.frm4ShowLocate == null)
				{
					this.frm4ShowLocate = new dfrmLocate();
					this.frm4ShowLocate.TopMost = true;
					this.frm4ShowLocate.Show();
				}
				else
				{
					try
					{
						if (this.frm4ShowLocate.WindowState == FormWindowState.Minimized)
						{
							this.frm4ShowLocate.WindowState = FormWindowState.Normal;
						}
						this.frm4ShowLocate.Show();
					}
					catch (Exception ex)
					{
						wgAppConfig.wgLog(ex.ToString());
						this.frm4ShowLocate = null;
						this.frm4ShowLocate = new dfrmLocate();
						this.frm4ShowLocate.TopMost = true;
						this.frm4ShowLocate.Show();
					}
				}
			}
			catch (Exception ex2)
			{
				wgAppConfig.wgLog(ex2.ToString());
			}
		}

		private void personInsideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.frm4ShowPersonsInside == null)
				{
					this.frm4ShowPersonsInside = new dfrmPersonsInside();
					this.frm4ShowPersonsInside.TopMost = true;
					this.frm4ShowPersonsInside.Show();
				}
				else
				{
					try
					{
						if (this.frm4ShowPersonsInside.WindowState == FormWindowState.Minimized)
						{
							this.frm4ShowPersonsInside.WindowState = FormWindowState.Normal;
						}
						this.frm4ShowPersonsInside.Show();
					}
					catch (Exception ex)
					{
						wgAppConfig.wgLog(ex.ToString());
						this.frm4ShowPersonsInside = null;
						this.frm4ShowPersonsInside = new dfrmPersonsInside();
						this.frm4ShowPersonsInside.TopMost = true;
						this.frm4ShowPersonsInside.Show();
					}
				}
			}
			catch (Exception ex2)
			{
				wgAppConfig.wgLog(ex2.ToString());
			}
		}

		private void pcCheckAccess_Init()
		{
			if (!this.bPCCheckAccess)
			{
				return;
			}
			try
			{
				string text = "";
				try
				{
					string cmdText = " SELECT a.f_GroupID,a.f_GroupName,b.f_GroupType,b.f_MoreCards,b.f_SoundFileName  from t_b_Group a, t_b_group4PCCheckAccess b where a.f_GroupID = b.f_GroupID and b.f_GroupType=1 order by f_GroupName ASC";
					if (wgAppConfig.IsAccessDB)
					{
						using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
						{
							using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
							{
								oleDbConnection.Open();
								OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
								if (oleDbDataReader.Read())
								{
									text = wgTools.SetObjToStr(oleDbDataReader["f_SoundFileName"]);
								}
								oleDbDataReader.Close();
							}
							goto IL_DB;
						}
					}
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
						{
							sqlConnection.Open();
							SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
							if (sqlDataReader.Read())
							{
								text = wgTools.SetObjToStr(sqlDataReader["f_SoundFileName"]);
							}
							sqlDataReader.Close();
						}
					}
					IL_DB:;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
				}
				using (DataView dataView = new DataView(this.dvDoors.Table))
				{
					if (string.IsNullOrEmpty(text))
					{
						for (int i = 0; i <= dataView.Count - 1; i++)
						{
							this.checkAccess_arrDoor.Add(dataView[i]["f_DoorID"]);
							this.checkAccess_arrDoorName.Add(dataView[i]["f_DoorName"]);
							this.checkAccess_arrReaderNo.Add("");
							this.checkAccess_arrGroupName.Add("");
							this.checkAccess_arrCardId.Add("");
							this.checkAccess_arrConsumerName.Add("");
							this.checkAccess_arrCheckTime.Add("");
							this.checkAccess_arrCheckStartTime.Add(DateTime.Now);
							this.checkAccess_arrCount.Add(-1);
						}
					}
					else
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						for (int j = 0; j <= dataView.Count - 1; j++)
						{
							for (int k = 0; k < array.Length; k++)
							{
								if (int.Parse(array[k]) == (int)dataView[j]["f_DoorID"])
								{
									this.checkAccess_arrDoor.Add(dataView[j]["f_DoorID"]);
									this.checkAccess_arrDoorName.Add(dataView[j]["f_DoorName"]);
									this.checkAccess_arrReaderNo.Add("");
									this.checkAccess_arrGroupName.Add("");
									this.checkAccess_arrCardId.Add("");
									this.checkAccess_arrConsumerName.Add("");
									this.checkAccess_arrCheckTime.Add("");
									this.checkAccess_arrCheckStartTime.Add(DateTime.Now);
									this.checkAccess_arrCount.Add(-1);
								}
							}
						}
					}
				}
				try
				{
					string cmdText2 = " SELECT a.f_GroupID,a.f_GroupName,b.f_GroupType,b.f_MoreCards  from t_b_Group a, t_b_group4PCCheckAccess b where a.f_GroupID = b.f_GroupID and b.f_CheckAccessActive=1 order by f_GroupName ASC";
					if (wgAppConfig.IsAccessDB)
					{
						using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
						{
							using (OleDbCommand oleDbCommand2 = new OleDbCommand(cmdText2, oleDbConnection2))
							{
								oleDbConnection2.Open();
								OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader();
								while (oleDbDataReader2.Read())
								{
									this.checkAccess_arrDB_GroupName.Add(oleDbDataReader2["f_GroupName"]);
									this.checkAccess_arrDB_MoreCards.Add(oleDbDataReader2["f_MoreCards"]);
								}
								oleDbDataReader2.Close();
							}
							goto IL_459;
						}
					}
					using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection2))
						{
							sqlConnection2.Open();
							SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
							while (sqlDataReader2.Read())
							{
								this.checkAccess_arrDB_GroupName.Add(sqlDataReader2["f_GroupName"]);
								this.checkAccess_arrDB_MoreCards.Add(sqlDataReader2["f_MoreCards"]);
							}
							sqlDataReader2.Close();
						}
					}
					IL_459:;
				}
				catch (Exception ex2)
				{
					wgTools.WgDebugWrite(ex2.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
				}
			}
			catch (Exception ex3)
			{
				wgTools.WgDebugWrite(ex3.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void txtInfoUpdateEntry4pcCheckAccess(MjRec mjrec)
		{
			if (this.bPCCheckAccess)
			{
				try
				{
					this.pcCheckAccess_DealNewRecord(mjrec);
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
				}
			}
		}

		private void pcCheckAccess_DealNewRecord(MjRec mjrec)
		{
			if (!this.bPCCheckAccess)
			{
				return;
			}
			mjrec.GetUserInfoFromDB();
			if (string.IsNullOrEmpty(mjrec.groupname))
			{
				return;
			}
			try
			{
				if (!(mjrec.ReadDate.Date > mjrec.endYMD.Date) && !(mjrec.ReadDate.Date < mjrec.beginYMD.Date))
				{
					int num = -1;
					this.dvDoors4Watching.RowFilter = string.Format("f_ControllerSN={0}  AND f_DoorNO={1}", mjrec.ControllerSN.ToString(), mjrec.DoorNo.ToString());
					if (this.dvDoors4Watching.Count > 0)
					{
						num = this.checkAccess_arrDoorName.IndexOf(this.dvDoors4Watching[0]["f_DoorName"].ToString());
					}
					if (num >= 0)
					{
						if (mjrec.ReadDate > ((DateTime)this.checkAccess_arrCheckStartTime[num]).AddSeconds(20.0) || mjrec.ReadDate.AddSeconds(20.0) < (DateTime)this.checkAccess_arrCheckStartTime[num])
						{
							this.checkAccess_arrCount[num] = 0;
						}
						if ((int)this.checkAccess_arrCount[num] > 0 && (byte)this.checkAccess_arrReaderNo[num] == mjrec.ReaderNo && (string)this.checkAccess_arrGroupName[num] == mjrec.groupname)
						{
							if (this.checkAccess_arrCardId[num].ToString().IndexOf(mjrec.CardID.ToString().PadLeft(10, '0')) < 0)
							{
								ArrayList arrayList;
								int index;
								(arrayList = this.checkAccess_arrCardId)[index = num] = arrayList[index] + "," + mjrec.CardID.ToString().PadLeft(10, '0');
								ArrayList arrayList2;
								int index2;
								(arrayList2 = this.checkAccess_arrConsumerName)[index2 = num] = arrayList2[index2] + "\r\n" + mjrec.consumerName;
								this.checkAccess_arrCheckStartTime[num] = mjrec.ReadDate;
								this.checkAccess_arrCount[num] = (int)this.checkAccess_arrCount[num] + 1;
							}
						}
						else
						{
							this.checkAccess_arrReaderNo[num] = mjrec.ReaderNo;
							this.checkAccess_arrGroupName[num] = mjrec.groupname;
							this.checkAccess_arrCardId[num] = mjrec.CardID.ToString().PadLeft(10, '0');
							this.checkAccess_arrConsumerName[num] = mjrec.consumerName;
							this.checkAccess_arrCheckStartTime[num] = mjrec.ReadDate;
							this.checkAccess_arrCount[num] = 1;
						}
						MethodInvoker method = new MethodInvoker(this.pcCheckAccess_DealOpen);
						base.BeginInvoke(method);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void pcCheckAccess_DealOpen()
		{
			if (!this.bPCCheckAccess)
			{
				return;
			}
			try
			{
				if (this.frm4PCCheckAccess == null)
				{
					this.frm4PCCheckAccess = new dfrmPCCheckAccess();
					this.frm4PCCheckAccess.TopMost = true;
				}
				if (!this.frm4PCCheckAccess.bDealing)
				{
					for (int i = 0; i <= this.checkAccess_arrDoor.Count - 1; i++)
					{
						if ((int)this.checkAccess_arrCount[i] > 0)
						{
							int num = this.checkAccess_arrDB_GroupName.IndexOf(this.checkAccess_arrGroupName[i]);
							if (num >= 0 && (int)this.checkAccess_arrCount[i] >= (int)this.checkAccess_arrDB_MoreCards[num])
							{
								this.checkAccess_arrCount[i] = 0;
								this.frm4PCCheckAccess.bDealing = true;
								if (this.frm4PCCheckAccess.WindowState == FormWindowState.Minimized)
								{
									this.frm4PCCheckAccess.WindowState = FormWindowState.Normal;
								}
								this.frm4PCCheckAccess.strDoorId = this.checkAccess_arrDoor[i].ToString();
								this.frm4PCCheckAccess.strDoorFullName = this.checkAccess_arrDoorName[i].ToString();
								this.frm4PCCheckAccess.strGroupname = this.checkAccess_arrGroupName[i].ToString();
								this.frm4PCCheckAccess.strConsumername = this.checkAccess_arrConsumerName[i].ToString();
								this.frm4PCCheckAccess.strNow = ((DateTime)this.checkAccess_arrCheckStartTime[i]).ToString(wgTools.YMDHMSFormat);
								this.frm4PCCheckAccess.Show();
								break;
							}
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
		}

		private void lstDoors_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void frmConsole_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.toolTip1.Active)
			{
				this.toolTip1.Active = false;
			}
		}

		public void directToRealtimeGet()
		{
			Cursor.Current = Cursors.WaitCursor;
			Thread.Sleep(1000);
			if (this.arrSelectDoors4Sign.Count == 0)
			{
				this.btnSelectAll.PerformClick();
			}
			else
			{
				try
				{
					for (int i = 0; i <= this.lstDoors.Items.Count - 1; i++)
					{
						if (this.arrSelectDoors4Sign.IndexOf(this.dvDoors[i]["f_ControllerID"]) >= 0)
						{
							this.lstDoors.Items[i].Selected = true;
						}
						else
						{
							this.lstDoors.Items[i].Selected = false;
						}
					}
				}
				catch (Exception)
				{
				}
			}
			this.bMainWindowDisplay = false;
			this.bDirectToRealtimeGet = true;
			this.btnRealtimeGetRecords.PerformClick();
		}

		public void wgCommServiceStart()
		{
			if (this.wgCommService1 == null)
			{
				this.wgCommService1 = new wgCommService();
				this.wgCommService1.EventHandler += new OnCommEventHandler(this.evtCommCallBack);
			}
		}

		private void evtCommCallBack(string text)
		{
			wgTools.WgDebugWrite("Got text through callback! {0}", new object[]
			{
				text
			});
			frmConsole.wgCommReceivedPktCount++;
			lock (this.wgCommQueRecText.SyncRoot)
			{
				this.wgCommQueRecText.Enqueue(text);
			}
		}

		private void resetPersonInsideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.lstDoors.SelectedItems.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectDoor);
				return;
			}
			if (this.lstDoors.SelectedItems.Count > 0 && XMessageBox.Show(sender.ToString() + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			using (icController icController = new icController())
			{
				ArrayList arrayList = new ArrayList();
				foreach (ListViewItem listViewItem in this.lstDoors.SelectedItems)
				{
					icController.GetInfoFromDBByDoorName(listViewItem.Text);
					if (arrayList.IndexOf(icController.ControllerSN) < 0)
					{
						if (icController.UpdateFRamIP(268435458u, 0u) <= 0)
						{
							wgRunInfoLog.addEventNotConnect(icController.ControllerSN, icController.IP, listViewItem);
						}
						else
						{
							wgRunInfoLog.addEvent(new InfoRow
							{
								desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
								information = string.Format("{0}", sender.ToString())
							});
							arrayList.Add(icController.ControllerSN);
						}
					}
					else
					{
						wgRunInfoLog.addEvent(new InfoRow
						{
							desc = string.Format("{0}[{1:d}]", listViewItem.Text, icController.ControllerSN),
							information = string.Format("{0}", sender.ToString())
						});
					}
					this.displayNewestLog();
				}
			}
		}

		private void displayTools()
		{
			if (!this.grpTool.Visible)
			{
				this.chkNeedCheckLosePacket.Checked = this.bNeedCheckLosePacket;
				this.chkDisplayNewestSwipe.Checked = !string.IsNullOrEmpty(wgAppConfig.GetKeyVal("DISPLAY_NEWEST_SWIPE"));
				this.grpTool.Visible = true;
				this.grpTool.Size = new Size(310, 221);
			}
		}

		private void btnHideTools_Click(object sender, EventArgs e)
		{
			wgAppConfig.UpdateKeyVal("DISPLAY_NEWEST_SWIPE", this.chkDisplayNewestSwipe.Checked ? "1" : "");
			this.bNeedCheckLosePacket = this.chkNeedCheckLosePacket.Checked;
			this.grpTool.Visible = false;
		}
	}
}
