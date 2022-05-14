using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class frmShiftAttReport : frmN3000
	{
		public class ToolStripDateTime : ToolStripControlHost
		{
			private static DateTimePicker dtp;

			public DateTimePicker DateTimeControl
			{
				get
				{
					return base.Control as DateTimePicker;
				}
			}

			public int BoxWidth
			{
				get
				{
					return (base.Control as DateTimePicker).Size.Width;
				}
				set
				{
					base.Control.Size = new Size(new Point(value, base.Control.Size.Height));
					(base.Control as DateTimePicker).Size = new Size(new Point(value, base.Control.Size.Height));
				}
			}

			public DateTime Value
			{
				get
				{
					return (base.Control as DateTimePicker).Value;
				}
				set
				{
					DateTime dateTime;
					if (DateTime.TryParse(value.ToString(), out dateTime) && dateTime >= (base.Control as DateTimePicker).MinDate && dateTime <= (base.Control as DateTimePicker).MaxDate)
					{
						(base.Control as DateTimePicker).Value = dateTime;
					}
				}
			}

			public ToolStripDateTime() : base(frmShiftAttReport.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmShiftAttReport.ToolStripDateTime.dtp != null)
				{
					frmShiftAttReport.ToolStripDateTime.dtp.Dispose();
				}
				base.Dispose(disposing);
			}

			public void SetTimeFormat()
			{
				DateTimePicker dateTimePicker = base.Control as DateTimePicker;
				dateTimePicker.CustomFormat = "HH;mm";
				dateTimePicker.Format = DateTimePickerFormat.Custom;
				dateTimePicker.ShowUpDown = true;
			}
		}

		private IContainer components;

		private ToolStrip toolStrip1;

		private DataGridView dgvMain;

		private ToolStripButton btnPrint;

		private BackgroundWorker backgroundWorker1;

		private ToolStripButton btnExportToExcel;

		private UserControlFind userControlFind1;

		private ToolStrip toolStrip3;

		private ToolStripLabel toolStripLabel2;

		private ToolStripLabel toolStripLabel3;

		private ToolStripButton btnCreateReport;

		private ToolStripButton btnStatistics;

		private ToolStrip toolStrip2;

		private ToolStripLabel lblLog;

		private ToolStripButton btnFindOption;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem btnSelectColumns;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_ShiftDateShort;

		private DataGridViewTextBoxColumn f_Addr;

		private DataGridViewTextBoxColumn f_ReadTimes;

		private DataGridViewTextBoxColumn f_OnDuty1Short;

		private DataGridViewTextBoxColumn f_Desc1;

		private DataGridViewTextBoxColumn f_OffDuty1Short;

		private DataGridViewTextBoxColumn f_Desc2;

		private DataGridViewTextBoxColumn f_OnDuty2Short;

		private DataGridViewTextBoxColumn f_Desc3;

		private DataGridViewTextBoxColumn f_OffDuty2Short;

		private DataGridViewTextBoxColumn f_Desc4;

		private DataGridViewTextBoxColumn f_OnDuty3Short;

		private DataGridViewTextBoxColumn f_Desc5;

		private DataGridViewTextBoxColumn f_OffDuty3Short;

		private DataGridViewTextBoxColumn f_Desc6;

		private DataGridViewTextBoxColumn f_OnDuty4Short;

		private DataGridViewTextBoxColumn f_Desc7;

		private DataGridViewTextBoxColumn f_OffDuty4Short;

		private DataGridViewTextBoxColumn f_Desc8;

		private DataGridViewTextBoxColumn f_LateMinutes;

		private DataGridViewTextBoxColumn f_LeaveEarlyMinutes;

		private DataGridViewTextBoxColumn f_OvertimeHours;

		private DataGridViewTextBoxColumn f_AbsenceDays;

		private DataGridViewTextBoxColumn f_NotReadCardCount;

		private ToolStripMenuItem saveLayoutToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultLayoutToolStripMenuItem;

		private ToolStripMenuItem cmdQueryNormalShift;

		private ToolStripMenuItem cmdQueryOtherShift;

		private ToolStripMenuItem cmdCreateWithSomeConsumer;

		private ToolStripMenuItem displayAllToolStripMenuItem;

		private frmShiftAttReport.ToolStripDateTime dtpDateFrom;

		private frmShiftAttReport.ToolStripDateTime dtpDateTo;

		private bool bLogCreateReport;

		private DateTime logDateStart;

		private DateTime logDateEnd;

		private DataSet dsDefaultStyle = new DataSet("DGV_STILE");

		private ArrayList arrColsName = new ArrayList();

		private ArrayList arrColsShow = new ArrayList();

		private int recIdMax;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private dfrmShiftAttReportFindOption dfrmFindOption;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmShiftAttReport));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.btnSelectColumns = new ToolStripMenuItem();
			this.saveLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.cmdQueryNormalShift = new ToolStripMenuItem();
			this.cmdQueryOtherShift = new ToolStripMenuItem();
			this.cmdCreateWithSomeConsumer = new ToolStripMenuItem();
			this.displayAllToolStripMenuItem = new ToolStripMenuItem();
			this.dgvMain = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_ShiftDateShort = new DataGridViewTextBoxColumn();
			this.f_Addr = new DataGridViewTextBoxColumn();
			this.f_ReadTimes = new DataGridViewTextBoxColumn();
			this.f_OnDuty1Short = new DataGridViewTextBoxColumn();
			this.f_Desc1 = new DataGridViewTextBoxColumn();
			this.f_OffDuty1Short = new DataGridViewTextBoxColumn();
			this.f_Desc2 = new DataGridViewTextBoxColumn();
			this.f_OnDuty2Short = new DataGridViewTextBoxColumn();
			this.f_Desc3 = new DataGridViewTextBoxColumn();
			this.f_OffDuty2Short = new DataGridViewTextBoxColumn();
			this.f_Desc4 = new DataGridViewTextBoxColumn();
			this.f_OnDuty3Short = new DataGridViewTextBoxColumn();
			this.f_Desc5 = new DataGridViewTextBoxColumn();
			this.f_OffDuty3Short = new DataGridViewTextBoxColumn();
			this.f_Desc6 = new DataGridViewTextBoxColumn();
			this.f_OnDuty4Short = new DataGridViewTextBoxColumn();
			this.f_Desc7 = new DataGridViewTextBoxColumn();
			this.f_OffDuty4Short = new DataGridViewTextBoxColumn();
			this.f_Desc8 = new DataGridViewTextBoxColumn();
			this.f_LateMinutes = new DataGridViewTextBoxColumn();
			this.f_LeaveEarlyMinutes = new DataGridViewTextBoxColumn();
			this.f_OvertimeHours = new DataGridViewTextBoxColumn();
			this.f_AbsenceDays = new DataGridViewTextBoxColumn();
			this.f_NotReadCardCount = new DataGridViewTextBoxColumn();
			this.toolStrip2 = new ToolStrip();
			this.lblLog = new ToolStripLabel();
			this.userControlFind1 = new UserControlFind();
			this.toolStrip3 = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.toolStripLabel3 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnStatistics = new ToolStripButton();
			this.btnCreateReport = new ToolStripButton();
			this.btnFindOption = new ToolStripButton();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip2.SuspendLayout();
			this.toolStrip3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnSelectColumns,
				this.saveLayoutToolStripMenuItem,
				this.restoreDefaultLayoutToolStripMenuItem,
				this.cmdQueryNormalShift,
				this.cmdQueryOtherShift,
				this.cmdCreateWithSomeConsumer,
				this.displayAllToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.btnSelectColumns, "btnSelectColumns");
			this.btnSelectColumns.Name = "btnSelectColumns";
			this.btnSelectColumns.Click += new EventHandler(this.btnSelectColumns_Click);
			componentResourceManager.ApplyResources(this.saveLayoutToolStripMenuItem, "saveLayoutToolStripMenuItem");
			this.saveLayoutToolStripMenuItem.Name = "saveLayoutToolStripMenuItem";
			this.saveLayoutToolStripMenuItem.Click += new EventHandler(this.saveLayoutToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.restoreDefaultLayoutToolStripMenuItem, "restoreDefaultLayoutToolStripMenuItem");
			this.restoreDefaultLayoutToolStripMenuItem.Name = "restoreDefaultLayoutToolStripMenuItem";
			this.restoreDefaultLayoutToolStripMenuItem.Click += new EventHandler(this.restoreDefaultLayoutToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.cmdQueryNormalShift, "cmdQueryNormalShift");
			this.cmdQueryNormalShift.Name = "cmdQueryNormalShift";
			this.cmdQueryNormalShift.Click += new EventHandler(this.btnQuery_Click);
			componentResourceManager.ApplyResources(this.cmdQueryOtherShift, "cmdQueryOtherShift");
			this.cmdQueryOtherShift.Name = "cmdQueryOtherShift";
			this.cmdQueryOtherShift.Click += new EventHandler(this.btnQuery_Click);
			componentResourceManager.ApplyResources(this.cmdCreateWithSomeConsumer, "cmdCreateWithSomeConsumer");
			this.cmdCreateWithSomeConsumer.Name = "cmdCreateWithSomeConsumer";
			this.cmdCreateWithSomeConsumer.Click += new EventHandler(this.cmdCreateWithSomeConsumer_Click);
			componentResourceManager.ApplyResources(this.displayAllToolStripMenuItem, "displayAllToolStripMenuItem");
			this.displayAllToolStripMenuItem.Name = "displayAllToolStripMenuItem";
			this.displayAllToolStripMenuItem.Click += new EventHandler(this.displayAllToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			this.dgvMain.AllowUserToOrderColumns = true;
			this.dgvMain.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_RecID,
				this.f_DepartmentName,
				this.f_ConsumerNO,
				this.f_ConsumerName,
				this.f_ShiftDateShort,
				this.f_Addr,
				this.f_ReadTimes,
				this.f_OnDuty1Short,
				this.f_Desc1,
				this.f_OffDuty1Short,
				this.f_Desc2,
				this.f_OnDuty2Short,
				this.f_Desc3,
				this.f_OffDuty2Short,
				this.f_Desc4,
				this.f_OnDuty3Short,
				this.f_Desc5,
				this.f_OffDuty3Short,
				this.f_Desc6,
				this.f_OnDuty4Short,
				this.f_Desc7,
				this.f_OffDuty4Short,
				this.f_Desc8,
				this.f_LateMinutes,
				this.f_LeaveEarlyMinutes,
				this.f_OvertimeHours,
				this.f_AbsenceDays,
				this.f_NotReadCardCount
			});
			this.dgvMain.ContextMenuStrip = this.contextMenuStrip1;
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvMain.Scroll += new ScrollEventHandler(this.dgvMain_Scroll);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_RecID.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DepartmentName, "f_DepartmentName");
			this.f_DepartmentName.Name = "f_DepartmentName";
			this.f_DepartmentName.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_ConsumerNO, "f_ConsumerNO");
			this.f_ConsumerNO.Name = "f_ConsumerNO";
			this.f_ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftDateShort, "f_ShiftDateShort");
			this.f_ShiftDateShort.Name = "f_ShiftDateShort";
			this.f_ShiftDateShort.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Addr, "f_Addr");
			this.f_Addr.Name = "f_Addr";
			this.f_Addr.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReadTimes, "f_ReadTimes");
			this.f_ReadTimes.Name = "f_ReadTimes";
			this.f_ReadTimes.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty1Short, "f_OnDuty1Short");
			this.f_OnDuty1Short.Name = "f_OnDuty1Short";
			this.f_OnDuty1Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc1, "f_Desc1");
			this.f_Desc1.Name = "f_Desc1";
			this.f_Desc1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OffDuty1Short, "f_OffDuty1Short");
			this.f_OffDuty1Short.Name = "f_OffDuty1Short";
			this.f_OffDuty1Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc2, "f_Desc2");
			this.f_Desc2.Name = "f_Desc2";
			this.f_Desc2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty2Short, "f_OnDuty2Short");
			this.f_OnDuty2Short.Name = "f_OnDuty2Short";
			this.f_OnDuty2Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc3, "f_Desc3");
			this.f_Desc3.Name = "f_Desc3";
			this.f_Desc3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OffDuty2Short, "f_OffDuty2Short");
			this.f_OffDuty2Short.Name = "f_OffDuty2Short";
			this.f_OffDuty2Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc4, "f_Desc4");
			this.f_Desc4.Name = "f_Desc4";
			this.f_Desc4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty3Short, "f_OnDuty3Short");
			this.f_OnDuty3Short.Name = "f_OnDuty3Short";
			this.f_OnDuty3Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc5, "f_Desc5");
			this.f_Desc5.Name = "f_Desc5";
			this.f_Desc5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OffDuty3Short, "f_OffDuty3Short");
			this.f_OffDuty3Short.Name = "f_OffDuty3Short";
			this.f_OffDuty3Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc6, "f_Desc6");
			this.f_Desc6.Name = "f_Desc6";
			this.f_Desc6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty4Short, "f_OnDuty4Short");
			this.f_OnDuty4Short.Name = "f_OnDuty4Short";
			this.f_OnDuty4Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc7, "f_Desc7");
			this.f_Desc7.Name = "f_Desc7";
			this.f_Desc7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OffDuty4Short, "f_OffDuty4Short");
			this.f_OffDuty4Short.Name = "f_OffDuty4Short";
			this.f_OffDuty4Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc8, "f_Desc8");
			this.f_Desc8.Name = "f_Desc8";
			this.f_Desc8.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LateMinutes, "f_LateMinutes");
			this.f_LateMinutes.Name = "f_LateMinutes";
			this.f_LateMinutes.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LeaveEarlyMinutes, "f_LeaveEarlyMinutes");
			this.f_LeaveEarlyMinutes.Name = "f_LeaveEarlyMinutes";
			this.f_LeaveEarlyMinutes.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OvertimeHours, "f_OvertimeHours");
			this.f_OvertimeHours.Name = "f_OvertimeHours";
			this.f_OvertimeHours.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_AbsenceDays, "f_AbsenceDays");
			this.f_AbsenceDays.Name = "f_AbsenceDays";
			this.f_AbsenceDays.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_NotReadCardCount, "f_NotReadCardCount");
			this.f_NotReadCardCount.Name = "f_NotReadCardCount";
			this.f_NotReadCardCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.BackColor = Color.Transparent;
			this.toolStrip2.BackgroundImage = Resources.pTools_third_title;
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.lblLog
			});
			this.toolStrip2.Name = "toolStrip2";
			componentResourceManager.ApplyResources(this.lblLog, "lblLog");
			this.lblLog.ForeColor = Color.White;
			this.lblLog.Name = "lblLog";
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.Name = "userControlFind1";
			componentResourceManager.ApplyResources(this.toolStrip3, "toolStrip3");
			this.toolStrip3.BackColor = Color.Transparent;
			this.toolStrip3.BackgroundImage = Resources.pTools_second_title;
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.toolStripLabel3
			});
			this.toolStrip3.Name = "toolStrip3";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.toolStripLabel2.ForeColor = Color.White;
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			this.toolStripLabel3.ForeColor = Color.White;
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnPrint,
				this.btnExportToExcel,
				this.btnStatistics,
				this.btnCreateReport,
				this.btnFindOption
			});
			this.toolStrip1.Name = "toolStrip1";
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			componentResourceManager.ApplyResources(this.btnStatistics, "btnStatistics");
			this.btnStatistics.ForeColor = Color.White;
			this.btnStatistics.Image = Resources.pTools_StatisticsReport;
			this.btnStatistics.Name = "btnStatistics";
			this.btnStatistics.Click += new EventHandler(this.btnStatistics_Click);
			componentResourceManager.ApplyResources(this.btnCreateReport, "btnCreateReport");
			this.btnCreateReport.ForeColor = Color.White;
			this.btnCreateReport.Image = Resources.pTools_CreateShiftReport;
			this.btnCreateReport.Name = "btnCreateReport";
			this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
			componentResourceManager.ApplyResources(this.btnFindOption, "btnFindOption");
			this.btnFindOption.ForeColor = Color.White;
			this.btnFindOption.Image = Resources.pTools_QueryOption;
			this.btnFindOption.Name = "btnFindOption";
			this.btnFindOption.Click += new EventHandler(this.btnFindOption_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmShiftAttReport";
			base.FormClosing += new FormClosingEventHandler(this.frmShiftAttReport_FormClosing);
			base.Load += new EventHandler(this.frmSwipeRecords_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmShiftAttReport()
		{
			this.InitializeComponent();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuAttendenceData";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnCreateReport.Visible = false;
			}
		}

		private void frmSwipeRecords_Load(object sender, EventArgs e)
		{
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.loadOperatorPrivilege();
			this.dtpDateFrom = new frmShiftAttReport.ToolStripDateTime();
			this.dtpDateTo = new frmShiftAttReport.ToolStripDateTime();
			this.toolStrip3.Items.Clear();
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.dtpDateFrom,
				this.toolStripLabel3,
				this.dtpDateTo
			});
			this.dtpDateFrom.BoxWidth = 120;
			this.dtpDateTo.BoxWidth = 120;
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.dtpDateFrom.Enabled = true;
			this.dtpDateTo.Enabled = true;
			this.userControlFind1.toolStripLabel2.Visible = false;
			this.userControlFind1.txtFindCardID.Visible = false;
			this.saveDefaultStyle();
			this.loadStyle();
			Cursor.Current = Cursors.WaitCursor;
			this.getLogCreateReport();
			if (this.bLogCreateReport)
			{
				this.dtpDateFrom.Value = this.logDateStart;
				this.dtpDateTo.Value = this.logDateEnd;
			}
			else
			{
				this.dtpDateTo.Value = DateTime.Now.Date;
				this.dtpDateFrom.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01"));
			}
			this.dtpDateFrom.BoxWidth = 150;
			this.dtpDateTo.BoxWidth = 150;
			wgAppConfig.setDisplayFormatDate(this.dtpDateFrom.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpDateTo.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.cmdQueryNormalShift.Visible = true;
				this.cmdQueryOtherShift.Visible = true;
			}
			this.Refresh();
			this.userControlFind1.btnQuery.PerformClick();
		}

		public void getLogCreateReport()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.getLogCreateReport_Acc();
				return;
			}
			this.bLogCreateReport = false;
			string cmdText = "SELECT * FROM t_a_Attendence WHERE [f_NO]=15 ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read() && wgTools.SetObjToStr(sqlDataReader["f_Notes"]) != "")
					{
						this.bLogCreateReport = true;
						this.logDateStart = DateTime.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"]).Substring(0, 10));
						this.logDateEnd = DateTime.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"]).Substring(12, 10));
						this.lblLog.Text = sqlDataReader["f_Notes"].ToString();
					}
					sqlDataReader.Close();
				}
			}
		}

		public void getLogCreateReport_Acc()
		{
			this.bLogCreateReport = false;
			string cmdText = "SELECT * FROM t_a_Attendence WHERE [f_NO]=15 ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read() && wgTools.SetObjToStr(oleDbDataReader["f_Notes"]) != "")
					{
						this.bLogCreateReport = true;
						this.logDateStart = DateTime.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"]).Substring(0, 10));
						this.logDateEnd = DateTime.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"]).Substring(12, 10));
						this.lblLog.Text = oleDbDataReader["f_Notes"].ToString();
					}
					oleDbDataReader.Close();
				}
			}
		}

		private bool OnlyTwoTimesSpecial()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.OnlyTwoTimesSpecial_Acc();
			}
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				return false;
			}
			string cmdText = "SELECT * FROM t_a_Attendence";
			bool result;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					int num = 2;
					while (sqlDataReader.Read())
					{
						if ((int)sqlDataReader["f_No"] == 14)
						{
							num = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
					}
					sqlDataReader.Close();
					if (num == 4)
					{
						result = false;
					}
					else
					{
						result = (wgAppConfig.getSystemParamByNO(57).ToString() == "1");
					}
				}
			}
			return result;
		}

		private bool OnlyTwoTimesSpecial_Acc()
		{
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				return false;
			}
			string cmdText = "SELECT * FROM t_a_Attendence";
			bool result;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					int num = 2;
					while (oleDbDataReader.Read())
					{
						if ((int)oleDbDataReader["f_No"] == 14)
						{
							num = Convert.ToInt32(oleDbDataReader["f_Value"]);
						}
					}
					oleDbDataReader.Close();
					if (num == 4)
					{
						result = false;
					}
					else
					{
						result = (wgAppConfig.getSystemParamByNO(57).ToString() == "1");
					}
				}
			}
			return result;
		}

		private bool OnlyOnDutySpecial()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.OnlyOnDutySpecial_Acc();
			}
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				return false;
			}
			string cmdText = "SELECT * FROM t_a_Attendence";
			bool result;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						if ((int)sqlDataReader["f_No"] == 14)
						{
							Convert.ToInt32(sqlDataReader["f_Value"]);
						}
					}
					sqlDataReader.Close();
					result = (wgAppConfig.getSystemParamByNO(59).ToString() == "1");
				}
			}
			return result;
		}

		private bool OnlyOnDutySpecial_Acc()
		{
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				return false;
			}
			string cmdText = "SELECT * FROM t_a_Attendence";
			bool result;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						if ((int)oleDbDataReader["f_No"] == 14)
						{
							Convert.ToInt32(oleDbDataReader["f_Value"]);
						}
					}
					oleDbDataReader.Close();
					result = (wgAppConfig.getSystemParamByNO(59).ToString() == "1");
				}
			}
			return result;
		}

		private void saveDefaultStyle()
		{
			DataTable dataTable = new DataTable();
			this.dsDefaultStyle.Tables.Add(dataTable);
			dataTable.TableName = this.dgvMain.Name;
			dataTable.Columns.Add("colName");
			dataTable.Columns.Add("colHeader");
			dataTable.Columns.Add("colWidth");
			dataTable.Columns.Add("colVisable");
			dataTable.Columns.Add("colDisplayIndex");
			for (int i = 0; i < this.dgvMain.ColumnCount; i++)
			{
				DataGridViewColumn dataGridViewColumn = this.dgvMain.Columns[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow["colName"] = dataGridViewColumn.Name;
				dataRow["colHeader"] = dataGridViewColumn.HeaderText;
				dataRow["colWidth"] = dataGridViewColumn.Width;
				dataRow["colVisable"] = dataGridViewColumn.Visible;
				dataRow["colDisplayIndex"] = dataGridViewColumn.DisplayIndex;
				dataTable.Rows.Add(dataRow);
				dataTable.AcceptChanges();
			}
		}

		private void loadDefaultStyle()
		{
			DataTable dataTable = this.dsDefaultStyle.Tables[this.dgvMain.Name];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.dgvMain.Columns[i].Name = dataTable.Rows[i]["colName"].ToString();
				this.dgvMain.Columns[i].HeaderText = dataTable.Rows[i]["colHeader"].ToString();
				this.dgvMain.Columns[i].Width = int.Parse(dataTable.Rows[i]["colWidth"].ToString());
				this.dgvMain.Columns[i].Visible = bool.Parse(dataTable.Rows[i]["colVisable"].ToString());
				this.dgvMain.Columns[i].DisplayIndex = int.Parse(dataTable.Rows[i]["colDisplayIndex"].ToString());
			}
		}

		private void loadStyle()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadStyle_Acc();
				return;
			}
			if (this.OnlyTwoTimesSpecial())
			{
				this.dgvMain.Columns[25].HeaderText = CommonStr.strWorkHour;
			}
			if (!wgAppConfig.getParamValBoolByNO(113))
			{
				int num = 2;
				string cmdText = "SELECT f_Value FROM t_a_Attendence WHERE f_No =14";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							int.TryParse(wgTools.SetObjToStr(sqlDataReader[0]), out num);
						}
						sqlDataReader.Close();
					}
				}
				this.dgvMain.Columns[5].Visible = false;
				this.dgvMain.Columns[6].Visible = false;
				if (num == 4)
				{
					this.dgvMain.Columns[7].HeaderText = CommonStr.strAMOnDuty;
					this.dgvMain.Columns[8].HeaderText = CommonStr.strDutyDesc1;
					this.dgvMain.Columns[9].HeaderText = CommonStr.strAMOffDuty;
					this.dgvMain.Columns[10].HeaderText = CommonStr.strDutyDesc2;
					this.dgvMain.Columns[11].HeaderText = CommonStr.strPMOnDuty;
					this.dgvMain.Columns[12].HeaderText = CommonStr.strDutyDesc3;
					this.dgvMain.Columns[13].HeaderText = CommonStr.strPMOffDuty;
					this.dgvMain.Columns[14].HeaderText = CommonStr.strDutyDesc4;
					for (int i = 15; i < 23; i++)
					{
						this.dgvMain.Columns[i].Visible = false;
					}
					if (this.OnlyOnDutySpecial())
					{
						this.dgvMain.Columns[9].Visible = false;
						this.dgvMain.Columns[10].Visible = false;
						this.dgvMain.Columns[13].Visible = false;
						this.dgvMain.Columns[14].Visible = false;
						this.dgvMain.Columns[24].Visible = false;
						this.dgvMain.Columns[25].Visible = false;
						this.dgvMain.Columns[27].Visible = false;
					}
				}
				else
				{
					this.dgvMain.Columns[7].HeaderText = CommonStr.strAMOnDuty;
					this.dgvMain.Columns[8].HeaderText = CommonStr.strOnDutyDesc;
					this.dgvMain.Columns[9].HeaderText = CommonStr.strPMOffDuty;
					this.dgvMain.Columns[10].HeaderText = CommonStr.strOffDutyDesc;
					for (int j = 11; j < 23; j++)
					{
						this.dgvMain.Columns[j].Visible = false;
					}
					if (this.OnlyOnDutySpecial())
					{
						this.dgvMain.Columns[9].Visible = false;
						this.dgvMain.Columns[10].Visible = false;
						this.dgvMain.Columns[24].Visible = false;
						this.dgvMain.Columns[25].Visible = false;
						this.dgvMain.Columns[27].Visible = false;
					}
				}
			}
			this.dgvMain.AutoGenerateColumns = false;
			this.arrColsName.Clear();
			this.arrColsShow.Clear();
			for (int k = 0; k < this.dgvMain.ColumnCount; k++)
			{
				this.arrColsName.Add(this.dgvMain.Columns[k].HeaderText);
				this.arrColsShow.Add(this.dgvMain.Columns[k].Visible);
			}
			string text = "";
			string text2 = "";
			for (int l = 0; l < this.arrColsName.Count; l++)
			{
				if (text != "")
				{
					text += ",";
					text2 += ",";
				}
				text += this.arrColsName[l];
				text2 += this.arrColsShow[l].ToString();
			}
			string keyVal = wgAppConfig.GetKeyVal(base.Name + "-" + this.dgvMain.Tag);
			if (keyVal != "")
			{
				string[] array = keyVal.Split(new char[]
				{
					';'
				});
				if (array.Length == 2 && text == array[0] && text2 != array[1])
				{
					string[] array2 = array[1].Split(new char[]
					{
						','
					});
					if (array2.Length == this.arrColsName.Count)
					{
						this.arrColsShow.Clear();
						for (int m = 0; m < this.dgvMain.ColumnCount; m++)
						{
							this.dgvMain.Columns[m].Visible = bool.Parse(array2[m]);
							this.arrColsShow.Add(this.dgvMain.Columns[m].Visible);
						}
					}
				}
			}
			wgAppConfig.ReadGVStyle(this, this.dgvMain);
		}

		private void loadStyle_Acc()
		{
			if (this.OnlyTwoTimesSpecial())
			{
				this.dgvMain.Columns[25].HeaderText = CommonStr.strWorkHour;
			}
			if (!wgAppConfig.getParamValBoolByNO(113))
			{
				int num = 2;
				string cmdText = "SELECT f_Value FROM t_a_Attendence WHERE f_No =14";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							int.TryParse(wgTools.SetObjToStr(oleDbDataReader[0]), out num);
						}
						oleDbDataReader.Close();
					}
				}
				this.dgvMain.Columns[5].Visible = false;
				this.dgvMain.Columns[6].Visible = false;
				if (num == 4)
				{
					this.dgvMain.Columns[7].HeaderText = CommonStr.strAMOnDuty;
					this.dgvMain.Columns[8].HeaderText = CommonStr.strDutyDesc1;
					this.dgvMain.Columns[9].HeaderText = CommonStr.strAMOffDuty;
					this.dgvMain.Columns[10].HeaderText = CommonStr.strDutyDesc2;
					this.dgvMain.Columns[11].HeaderText = CommonStr.strPMOnDuty;
					this.dgvMain.Columns[12].HeaderText = CommonStr.strDutyDesc3;
					this.dgvMain.Columns[13].HeaderText = CommonStr.strPMOffDuty;
					this.dgvMain.Columns[14].HeaderText = CommonStr.strDutyDesc4;
					for (int i = 15; i < 23; i++)
					{
						this.dgvMain.Columns[i].Visible = false;
					}
					if (this.OnlyOnDutySpecial())
					{
						this.dgvMain.Columns[9].Visible = false;
						this.dgvMain.Columns[10].Visible = false;
						this.dgvMain.Columns[13].Visible = false;
						this.dgvMain.Columns[14].Visible = false;
						this.dgvMain.Columns[24].Visible = false;
						this.dgvMain.Columns[25].Visible = false;
						this.dgvMain.Columns[27].Visible = false;
					}
				}
				else
				{
					this.dgvMain.Columns[7].HeaderText = CommonStr.strAMOnDuty;
					this.dgvMain.Columns[8].HeaderText = CommonStr.strOnDutyDesc;
					this.dgvMain.Columns[9].HeaderText = CommonStr.strPMOffDuty;
					this.dgvMain.Columns[10].HeaderText = CommonStr.strOffDutyDesc;
					for (int j = 11; j < 23; j++)
					{
						this.dgvMain.Columns[j].Visible = false;
					}
					if (this.OnlyOnDutySpecial())
					{
						this.dgvMain.Columns[9].Visible = false;
						this.dgvMain.Columns[10].Visible = false;
						this.dgvMain.Columns[24].Visible = false;
						this.dgvMain.Columns[25].Visible = false;
						this.dgvMain.Columns[27].Visible = false;
					}
				}
			}
			this.dgvMain.AutoGenerateColumns = false;
			this.arrColsName.Clear();
			this.arrColsShow.Clear();
			for (int k = 0; k < this.dgvMain.ColumnCount; k++)
			{
				this.arrColsName.Add(this.dgvMain.Columns[k].HeaderText);
				this.arrColsShow.Add(this.dgvMain.Columns[k].Visible);
			}
			string text = "";
			string text2 = "";
			for (int l = 0; l < this.arrColsName.Count; l++)
			{
				if (text != "")
				{
					text += ",";
					text2 += ",";
				}
				text += this.arrColsName[l];
				text2 += this.arrColsShow[l].ToString();
			}
			string keyVal = wgAppConfig.GetKeyVal(base.Name + "-" + this.dgvMain.Tag);
			if (keyVal != "")
			{
				string[] array = keyVal.Split(new char[]
				{
					';'
				});
				if (array.Length == 2 && text == array[0] && text2 != array[1])
				{
					string[] array2 = array[1].Split(new char[]
					{
						','
					});
					if (array2.Length == this.arrColsName.Count)
					{
						this.arrColsShow.Clear();
						for (int m = 0; m < this.dgvMain.ColumnCount; m++)
						{
							this.dgvMain.Columns[m].Visible = bool.Parse(array2[m]);
							this.arrColsShow.Add(this.dgvMain.Columns[m].Visible);
						}
					}
				}
			}
			wgAppConfig.ReadGVStyle(this, this.dgvMain);
		}

		private DataTable loadDataRecords(int startIndex, int maxRecords, string strSql)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine(this.Text + " loadDataRecords Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recIdMax = -2147483648;
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND t_d_shift_AttReport.f_RecID > {0:d}", this.recIdMax);
			}
			else
			{
				strSql += string.Format(" WHERE t_d_shift_AttReport.f_RecID > {0:d}", this.recIdMax);
			}
			strSql += " ORDER BY t_d_shift_AttReport.f_RecID ";
			this.table = new DataTable();
			wgTools.WriteLine("da.Fill start");
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.table);
						}
					}
					goto IL_190;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.table);
					}
				}
			}
			IL_190:
			if (this.table.Rows.Count > 0)
			{
				this.recIdMax = int.Parse(this.table.Rows[this.table.Rows.Count - 1][0].ToString());
			}
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			wgTools.WriteLine(this.Text + "  loadRecords End");
			return this.table;
		}

		private string getSqlOfDateTime(string colNameOfDate)
		{
			string text = string.Concat(new string[]
			{
				"  (",
				colNameOfDate,
				" >= ",
				wgTools.PrepareStr(this.dtpDateFrom.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 00:00:00"),
				")"
			});
			if (text != "")
			{
				text += " AND ";
			}
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				"  (",
				colNameOfDate,
				" <= ",
				wgTools.PrepareStr(this.dtpDateTo.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59"),
				")"
			});
		}

		public void btnQuery_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnQuery_Click_Acc(sender, e);
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			this.getLogCreateReport();
			if (!this.bLogCreateReport)
			{
				XMessageBox.Show(this, CommonStr.strGetRecordsBeforeCreateReport + "\r\n\r\n" + CommonStr.strCreateInAdvance, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			string str = "";
			bool flag = false;
			if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
			{
				flag = true;
				str = this.dfrmFindOption.getStrSql();
			}
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_shift_AttReport.f_RecID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
			text += " t_d_shift_AttReport.f_ShiftDate AS f_ShiftDateShort, ";
			text += " t_d_shift_AttReport.f_ShiftID, ";
			text += " t_d_shift_AttReport.f_ReadTimes, ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OnDuty1,108) , '') AS f_OnDuty1Short,  ";
			text += "       ISNULL(t_d_shift_AttReport.f_OnDuty1AttDesc, '') + ISNULL(t_d_shift_AttReport.f_OnDuty1CardRecordDesc, '') AS f_Desc1,  ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OffDuty1,108) , '') AS f_OffDuty1Short,            ";
			text += "       ISNULL(t_d_shift_AttReport.f_OffDuty1AttDesc, '') + ISNULL(t_d_shift_AttReport.f_OffDuty1CardRecordDesc, '') AS f_Desc2,           ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OnDuty2,108) , '') AS f_OnDuty2Short,        ";
			text += "       ISNULL(t_d_shift_AttReport.f_OnDuty2AttDesc, '') + ISNULL(t_d_shift_AttReport.f_OnDuty2CardRecordDesc, '') AS f_Desc3,            ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OffDuty2,108) , '') AS f_OffDuty2Short, ";
			text += "       ISNULL(t_d_shift_AttReport.f_OffDuty2AttDesc, '')+ ISNULL(t_d_shift_AttReport.f_OffDuty2CardRecordDesc, '') AS f_Desc4,           ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OnDuty3,108) , '') AS f_OnDuty3Short,              ";
			text += "       ISNULL(t_d_shift_AttReport.f_OnDuty3AttDesc, '') + ISNULL(t_d_shift_AttReport.f_OnDuty3CardRecordDesc, '') AS f_Desc5,            ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OffDuty3,108) , '') AS f_OffDuty3Short,       ";
			text += "       ISNULL(t_d_shift_AttReport.f_OffDuty3AttDesc, '')+ ISNULL(t_d_shift_AttReport.f_OffDuty3CardRecordDesc, '') AS f_Desc6,           ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OnDuty4,108) , '') AS f_OnDuty4Short,   ";
			text += "       ISNULL(t_d_shift_AttReport.f_OnDuty4AttDesc, '') + ISNULL(t_d_shift_AttReport.f_OnDuty4CardRecordDesc, '') AS f_Desc7,            ";
			text += "       ISNULL(CONVERT(char(8), t_d_shift_AttReport.f_OffDuty4,108) , '') AS f_OffDuty4Short,  ";
			text += "       ISNULL(t_d_shift_AttReport.f_OffDuty4AttDesc, '')+ ISNULL(t_d_shift_AttReport.f_OffDuty4CardRecordDesc, '') AS f_Desc8, ";
			text += "CASE WHEN [f_LateMinutes]>0 THEN (CASE WHEN [f_LateMinutes]<1 THEN '0.5' ELSE CONVERT(varchar(6),[f_LateMinutes]) END ) ELSE ' ' END [f_LateMinutes], ";
			text += "CASE WHEN [f_LeaveEarlyMinutes]>0 THEN (CASE WHEN [f_LeaveEarlyMinutes]<1 THEN '0.5' ELSE CONVERT(varchar(6),[f_LeaveEarlyMinutes]) END ) ELSE ' ' END [f_LeaveEarlyMinutes], ";
			text += "CASE WHEN [f_OvertimeHours]>0 THEN (CASE WHEN [f_OvertimeHours]<1 THEN '0.5' ELSE CONVERT(varchar(6),[f_OvertimeHours]) END ) ELSE ' ' END [f_OvertimeHours], ";
			text += "CASE WHEN [f_AbsenceDays]>0 THEN (CASE WHEN [f_AbsenceDays]<1 THEN '0.5' ELSE CONVERT(varchar(6),[f_AbsenceDays]) END ) ELSE ' ' END [f_AbsenceDays], ";
			text += "CASE WHEN [f_NotReadCardCount]>0 THEN (CASE WHEN [f_NotReadCardCount]<1 THEN '0.5' ELSE CONVERT(varchar(6),[f_NotReadCardCount]) END ) ELSE ' ' END [f_NotReadCardCount] ";
			string text2 = wgAppConfig.getSqlFindNormal(text, "t_d_shift_AttReport", this.getSqlOfDateTime("t_d_shift_AttReport.f_ShiftDate"), groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			if (flag)
			{
				text2 = text2 + " WHERE " + str;
			}
			string text3 = "";
			if (sender == this.cmdQueryNormalShift)
			{
				text3 = " AND t_d_shift_AttReport.f_ShiftID IS NULL ";
			}
			else if (sender == this.cmdQueryOtherShift)
			{
				text3 = " AND t_d_shift_AttReport.f_ShiftID IS NOT NULL ";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				if (text2.IndexOf(" WHERE ") > 0)
				{
					text2 += text3;
				}
				else
				{
					text2 = text2 + " WHERE (1>0) " + text3;
				}
			}
			this.reloadData(text2);
		}

		public void btnQuery_Click_Acc(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			this.getLogCreateReport();
			if (!this.bLogCreateReport)
			{
				XMessageBox.Show(this, CommonStr.strGetRecordsBeforeCreateReport + "\r\n\r\n" + CommonStr.strCreateInAdvance, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			string str = "";
			bool flag = false;
			if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
			{
				flag = true;
				str = this.dfrmFindOption.getStrSql();
			}
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_shift_AttReport.f_RecID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
			text += " t_d_shift_AttReport.f_ShiftDate AS f_ShiftDateShort, ";
			text += " t_d_shift_AttReport.f_ShiftID, ";
			text += " t_d_shift_AttReport.f_ReadTimes, ";
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OnDuty1", "f_OnDuty1Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OnDuty1AttDesc", "f_OnDuty1CardRecordDesc", "f_Desc1");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OffDuty1", "f_OffDuty1Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OffDuty1AttDesc", "f_OffDuty1CardRecordDesc", "f_Desc2");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OnDuty2", "f_OnDuty2Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OnDuty2AttDesc", "f_OnDuty2CardRecordDesc", "f_Desc3");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OffDuty2", "f_OffDuty2Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OffDuty2AttDesc", "f_OffDuty2CardRecordDesc", "f_Desc4");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OnDuty3", "f_OnDuty3Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OnDuty3AttDesc", "f_OnDuty3CardRecordDesc", "f_Desc5");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OffDuty3", "f_OffDuty3Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OffDuty3AttDesc", "f_OffDuty3CardRecordDesc", "f_Desc6");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OnDuty4", "f_OnDuty4Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OnDuty4AttDesc", "f_OnDuty4CardRecordDesc", "f_Desc7");
			text += string.Format("       IIF(IsDate(t_d_shift_AttReport.{0}),Format(t_d_shift_AttReport.{0},'hh:nn:ss'),'')  AS {1},  ", "f_OffDuty4", "f_OffDuty4Short");
			text += string.Format("       IIF(ISNULL(t_d_shift_AttReport.{0}), '',t_d_shift_AttReport.{0}) + IIF(ISNULL(t_d_shift_AttReport.{1}), '',t_d_shift_AttReport.{1}) AS {2},  ", "f_OffDuty4AttDesc", "f_OffDuty4CardRecordDesc", "f_Desc8");
			text += "IIF ([f_LateMinutes]>0, IIF ([f_LateMinutes]<1, '0.5', CSTR([f_LateMinutes])), ' ') AS [f_LateMinutes], ";
			text += "IIF ([f_LeaveEarlyMinutes]>0 ,  IIF ([f_LeaveEarlyMinutes]<1 , '0.5', CSTR([f_LeaveEarlyMinutes])), ' ') AS [f_LeaveEarlyMinutes], ";
			text += "IIF ([f_OvertimeHours]>0,  IIF ([f_OvertimeHours]<1,  '0.5', CSTR([f_OvertimeHours])), ' ') AS [f_OvertimeHours], ";
			text += "IIF ([f_AbsenceDays]>0,  IIF ([f_AbsenceDays]<1, '0.5', CSTR([f_AbsenceDays])), ' ') AS [f_AbsenceDays], ";
			text += "IIF ([f_NotReadCardCount]>0, IIF ([f_NotReadCardCount]<1, '0.5', CSTR([f_NotReadCardCount])), ' ') AS [f_NotReadCardCount] ";
			string text2 = wgAppConfig.getSqlFindNormal(text, "t_d_shift_AttReport", this.getSqlOfDateTime("t_d_shift_AttReport.f_ShiftDate"), groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			if (flag)
			{
				text2 = text2 + " WHERE " + str;
			}
			string text3 = "";
			if (sender == this.cmdQueryNormalShift)
			{
				text3 = " AND t_d_shift_AttReport.f_ShiftID IS NULL ";
			}
			else if (sender == this.cmdQueryOtherShift)
			{
				text3 = " AND t_d_shift_AttReport.f_ShiftID IS NOT NULL ";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				if (text2.IndexOf(" WHERE ") > 0)
				{
					text2 += text3;
				}
				else
				{
					text2 = text2 + " WHERE (1>0) " + text3;
				}
			}
			this.reloadData(text2);
		}

		private void reloadData(string strsql)
		{
			if (this.backgroundWorker1.IsBusy)
			{
				return;
			}
			this.bLoadedFinished = false;
			this.startRecordIndex = 0;
			this.MaxRecord = 1000;
			if (!string.IsNullOrEmpty(strsql))
			{
				this.dgvSql = strsql;
			}
			this.dgvMain.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void frmShiftAttReport_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFindOption != null)
			{
				this.dfrmFindOption.Close();
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvMain, this.Text);
		}

		private void fillDgv(DataTable dt)
		{
			try
			{
				if (this.dgvMain.DataSource == null)
				{
					this.dgvMain.DataSource = dt;
					for (int i = 0; i < dt.Columns.Count; i++)
					{
						this.dgvMain.Columns[i].DataPropertyName = dt.Columns[i].ColumnName;
						this.dgvMain.Columns[i].Name = dt.Columns[i].ColumnName;
					}
					wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_ShiftDateShort", wgTools.DisplayFormat_DateYMDWeek);
					wgAppConfig.ReadGVStyle(this, this.dgvMain);
					if (this.startRecordIndex == 0 && dt.Rows.Count >= this.MaxRecord)
					{
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
					}
				}
				else if (dt.Rows.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = this.dgvMain.FirstDisplayedScrollingRowIndex;
					DataTable dataTable = this.dgvMain.DataSource as DataTable;
					dataTable.Merge(dt);
					if (firstDisplayedScrollingRowIndex >= 0)
					{
						this.dgvMain.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			Cursor.Current = Cursors.Default;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			int startIndex = (int)((object[])e.Argument)[0];
			int maxRecords = (int)((object[])e.Argument)[1];
			string strSql = (string)((object[])e.Argument)[2];
			e.Result = this.loadDataRecords(startIndex, maxRecords, strSql);
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				XMessageBox.Show(CommonStr.strOperationCanceled);
				return;
			}
			if (e.Error != null)
			{
				string text = string.Format("An error occurred: {0}", e.Error.Message);
				XMessageBox.Show(text);
				return;
			}
			if ((e.Result as DataTable).Rows.Count < this.MaxRecord)
			{
				this.bLoadedFinished = true;
			}
			this.fillDgv(e.Result as DataTable);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvMain_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > this.dgvMain.Rows.Count || e.NewValue + this.dgvMain.Rows.Count / 10 > this.dgvMain.Rows.Count))
				{
					if (this.startRecordIndex <= this.dgvMain.Rows.Count)
					{
						if (this.backgroundWorker1.IsBusy)
						{
							return;
						}
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
						return;
					}
					else
					{
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcelSpecial(ref this.dgvMain, this.Text, this.bLoadedFinished, ref this.backgroundWorker1, ref this.startRecordIndex, this.MaxRecord, this.dgvSql);
		}

		private void btnCreateReport_Click(object sender, EventArgs e)
		{
			if (this.dtpDateFrom.Value > this.dtpDateTo.Value)
			{
				return;
			}
			string text = string.Format("{0}\r\n{1} {2} {3} {4}", new object[]
			{
				this.btnCreateReport.Text,
				this.toolStripLabel2.Text,
				this.dtpDateFrom.Value.ToString(wgTools.DisplayFormat_DateYMD),
				this.toolStripLabel3.Text,
				this.dtpDateTo.Value.ToString(wgTools.DisplayFormat_DateYMD)
			});
			text = string.Format(CommonStr.strGetRecordsBeforeCreateReport + "\r\n\r\n" + CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text2 = "";
			long num4 = 0L;
			int num5 = 0;
			this.userControlFind1.getSqlInfo(ref num, ref num2, ref num3, ref text2, ref num4, ref num5);
			string text3 = " SELECT   f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled ";
			string text4 = text3;
			text4 += " FROM t_b_Consumer WHERE (f_AttendEnabled >0 ) ";
			if (num5 > 0)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer WHERE (f_AttendEnabled >0 ) ";
				text4 += string.Format(" AND t_b_Consumer.f_ConsumerID ={0:d} ", num5);
			}
			else if (num > 0)
			{
				text4 = text3;
				if (num >= num3)
				{
					text4 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text4 += string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", num2);
				}
				else
				{
					text4 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text4 += string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", num);
					text4 += string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", num3);
				}
				if (text2 != "")
				{
					text4 += string.Format(" AND f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				}
				else if (num4 > 0L)
				{
					text4 += string.Format(" AND f_CardNO ={0:d} ", num4);
				}
				text4 += " AND (f_AttendEnabled >0 ) ";
			}
			else if (text2 != "")
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				text4 += " AND (f_AttendEnabled >0 ) ";
			}
			else if (num4 > 0L)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
				text4 += " AND (f_AttendEnabled >0 ) ";
			}
			bool flag = false;
			int totalConsumer = 0;
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text4.Replace(text3, " SELECT  COUNT(*) "), oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							flag = true;
							totalConsumer = Convert.ToInt32(oleDbDataReader[0]);
						}
						oleDbDataReader.Close();
					}
					goto IL_3A6;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text4.Replace(text3, " SELECT  COUNT(*) "), sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						flag = true;
						totalConsumer = Convert.ToInt32(sqlDataReader[0]);
					}
					sqlDataReader.Close();
				}
			}
			IL_3A6:
			if (flag)
			{
				if (base.OwnedForms.Length > 0)
				{
					Form[] ownedForms = base.OwnedForms;
					for (int i = 0; i < ownedForms.Length; i++)
					{
						Form form = ownedForms[i];
						if (form.Name == "dfrmShiftAttReportCreate")
						{
							return;
						}
					}
				}
				using (dfrmShiftAttReportCreate dfrmShiftAttReportCreate = new dfrmShiftAttReportCreate())
				{
					dfrmShiftAttReportCreate.totalConsumer = totalConsumer;
					dfrmShiftAttReportCreate.dtBegin = this.dtpDateFrom.Value;
					dfrmShiftAttReportCreate.dtEnd = this.dtpDateTo.Value;
					dfrmShiftAttReportCreate.strConsumerSql = text4;
					dfrmShiftAttReportCreate.groupName = this.userControlFind1.cboFindDept.Text;
					dfrmShiftAttReportCreate.TopMost = true;
					dfrmShiftAttReportCreate.ShowDialog(this);
					this.btnQuery_Click(null, null);
				}
			}
		}

		private void btnStatistics_Click(object sender, EventArgs e)
		{
			using (frmShiftAttStatistics frmShiftAttStatistics = new frmShiftAttStatistics())
			{
				frmShiftAttStatistics.ShowDialog(this);
			}
		}

		private void btnFindOption_Click(object sender, EventArgs e)
		{
			if (this.dfrmFindOption == null)
			{
				this.dfrmFindOption = new dfrmShiftAttReportFindOption();
				this.dfrmFindOption.Owner = this;
			}
			this.dfrmFindOption.Show();
		}

		private void btnSelectColumns_Click(object sender, EventArgs e)
		{
			using (dfrmSelectColumnsShow dfrmSelectColumnsShow = new dfrmSelectColumnsShow())
			{
				for (int i = 1; i < this.arrColsName.Count; i++)
				{
					dfrmSelectColumnsShow.chkListColumns.Items.Add(this.arrColsName[i]);
					dfrmSelectColumnsShow.chkListColumns.SetItemChecked(i - 1, bool.Parse(this.arrColsShow[i].ToString()));
				}
				if (dfrmSelectColumnsShow.ShowDialog(this) == DialogResult.OK)
				{
					this.arrColsShow.Clear();
					this.arrColsShow.Add(this.dgvMain.Columns[0].Visible);
					for (int j = 1; j < this.dgvMain.ColumnCount; j++)
					{
						this.dgvMain.Columns[j].Visible = dfrmSelectColumnsShow.chkListColumns.GetItemChecked(j - 1);
						this.arrColsShow.Add(this.dgvMain.Columns[j].Visible);
					}
					this.saveColumns();
				}
			}
		}

		private void saveColumns()
		{
			string text = "";
			string text2 = "";
			for (int i = 0; i < this.arrColsName.Count; i++)
			{
				if (text != "")
				{
					text += ",";
					text2 += ",";
				}
				text += this.arrColsName[i];
				text2 += this.arrColsShow[i].ToString();
			}
			wgAppConfig.InsertKeyVal(base.Name + "-" + this.dgvMain.Tag, text + ";" + text2);
			wgAppConfig.UpdateKeyVal(base.Name + "-" + this.dgvMain.Tag, text + ";" + text2);
		}

		private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.SaveDGVStyle(this, this.dgvMain);
			XMessageBox.Show(sender.ToString() + " " + CommonStr.strSuccessfully);
		}

		private void restoreDefaultLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.RestoreGVStyle(this, this.dgvMain);
			wgAppConfig.UpdateKeyVal(base.Name + "-" + this.dgvMain.Tag, "");
			this.loadDefaultStyle();
			this.loadStyle();
		}

		private void cmdCreateWithSomeConsumer_Click(object sender, EventArgs e)
		{
			if (this.dtpDateFrom.Value > this.dtpDateTo.Value)
			{
				return;
			}
			string text = string.Format("{0}\r\n{1} {2} {3} {4}", new object[]
			{
				this.btnCreateReport.Text,
				this.toolStripLabel2.Text,
				this.dtpDateFrom.Value.ToString(wgTools.DisplayFormat_DateYMD),
				this.toolStripLabel3.Text,
				this.dtpDateTo.Value.ToString(wgTools.DisplayFormat_DateYMD)
			});
			text = string.Format(CommonStr.strGetRecordsBeforeCreateReport + "\r\n\r\n" + CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text2 = "";
			long num4 = 0L;
			int num5 = 0;
			this.userControlFind1.getSqlInfo(ref num, ref num2, ref num3, ref text2, ref num4, ref num5);
			string text3 = " SELECT   f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled ";
			string text4 = text3;
			text4 += " FROM t_b_Consumer WHERE (f_AttendEnabled >0 ) ";
			if (num5 > 0)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer WHERE (f_AttendEnabled >0 ) ";
				text4 += string.Format(" AND t_b_Consumer.f_ConsumerID ={0:d} ", num5);
			}
			else if (num > 0)
			{
				text4 = text3;
				if (num >= num3)
				{
					text4 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text4 += string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", num2);
				}
				else
				{
					text4 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text4 += string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", num);
					text4 += string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", num3);
				}
				if (text2 != "")
				{
					text4 += string.Format(" AND f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				}
				else if (num4 > 0L)
				{
					text4 += string.Format(" AND f_CardNO ={0:d} ", num4);
				}
				text4 += " AND (f_AttendEnabled >0 ) ";
			}
			else if (text2 != "")
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				text4 += " AND (f_AttendEnabled >0 ) ";
			}
			else if (num4 > 0L)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
				text4 += " AND (f_AttendEnabled >0 ) ";
			}
			dfrmUserSelected dfrmUserSelected = new dfrmUserSelected();
			if (dfrmUserSelected.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			if (string.IsNullOrEmpty(dfrmUserSelected.selectedUsers))
			{
				return;
			}
			text4 = text3;
			text4 += " FROM t_b_Consumer  ";
			text4 += string.Format(" WHERE f_ConsumerID IN ({0}) AND {1} ", dfrmUserSelected.selectedUsers, "   (f_AttendEnabled >0 ) ");
			bool flag = false;
			int totalConsumer = 0;
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text4.Replace(text3, " SELECT  COUNT(*) "), oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							flag = true;
							totalConsumer = Convert.ToInt32(oleDbDataReader[0]);
						}
						oleDbDataReader.Close();
					}
					goto IL_3FB;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text4.Replace(text3, " SELECT  COUNT(*) "), sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						flag = true;
						totalConsumer = Convert.ToInt32(sqlDataReader[0]);
					}
					sqlDataReader.Close();
				}
			}
			IL_3FB:
			if (flag)
			{
				if (base.OwnedForms.Length > 0)
				{
					Form[] ownedForms = base.OwnedForms;
					for (int i = 0; i < ownedForms.Length; i++)
					{
						Form form = ownedForms[i];
						if (form.Name == "dfrmShiftAttReportCreate")
						{
							return;
						}
					}
				}
				using (dfrmShiftAttReportCreate dfrmShiftAttReportCreate = new dfrmShiftAttReportCreate())
				{
					dfrmShiftAttReportCreate.totalConsumer = totalConsumer;
					dfrmShiftAttReportCreate.dtBegin = this.dtpDateFrom.Value;
					dfrmShiftAttReportCreate.dtEnd = this.dtpDateTo.Value;
					dfrmShiftAttReportCreate.strConsumerSql = text4;
					dfrmShiftAttReportCreate.groupName = this.userControlFind1.cboFindDept.Text;
					dfrmShiftAttReportCreate.TopMost = true;
					dfrmShiftAttReportCreate.ShowDialog(this);
					this.btnQuery_Click(null, null);
				}
			}
		}

		private void displayAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.bLoadedFinished)
			{
				if (!this.bLogCreateReport)
				{
					XMessageBox.Show(this, CommonStr.strGetRecordsBeforeCreateReport + "\r\n\r\n" + CommonStr.strCreateInAdvance, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				Cursor.Current = Cursors.WaitCursor;
				if (this.startRecordIndex <= this.dgvMain.Rows.Count)
				{
					if (this.backgroundWorker1.IsBusy)
					{
						return;
					}
					this.startRecordIndex += this.MaxRecord;
					this.bLoadedFinished = true;
					this.backgroundWorker1.RunWorkerAsync(new object[]
					{
						this.startRecordIndex,
						100000000,
						this.dgvSql
					});
					return;
				}
				else
				{
					wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + "#");
				}
			}
		}
	}
}
