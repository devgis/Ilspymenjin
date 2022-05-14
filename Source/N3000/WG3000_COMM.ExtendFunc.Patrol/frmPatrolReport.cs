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

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class frmPatrolReport : frmN3000
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

			public ToolStripDateTime() : base(frmPatrolReport.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmPatrolReport.ToolStripDateTime.dtp != null)
				{
					frmPatrolReport.ToolStripDateTime.dtp.Dispose();
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

		private ToolStripMenuItem saveLayoutToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultLayoutToolStripMenuItem;

		private ToolStripMenuItem cmdCreateWithSomeConsumer;

		private ToolStripButton btnPatrolSetup;

		private ToolStripButton btnPatrolRoute;

		private ToolStripButton btnPatrolTask;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_ShiftDateShort;

		private DataGridViewTextBoxColumn f_OnDuty1Short;

		private DataGridViewTextBoxColumn f_OffDuty1Short;

		private DataGridViewTextBoxColumn f_Desc1;

		private DataGridViewTextBoxColumn f_Desc2;

		private DataGridViewTextBoxColumn f_Desc3;

		private ToolStripButton btnExit;

		private frmPatrolReport.ToolStripDateTime dtpDateFrom;

		private frmPatrolReport.ToolStripDateTime dtpDateTo;

		private bool bLogCreateReport;

		private DateTime logDateStart;

		private DateTime logDateEnd;

		private DataSet dsDefaultStyle = new DataSet("DGV_STILE");

		private ArrayList arrColsName = new ArrayList();

		private ArrayList arrColsShow = new ArrayList();

		private int recIdMax;

		private DataTable table;

		private bool bFirstQuery = true;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private dfrmPatrolReportFindOption dfrmFindOption;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPatrolReport));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.btnSelectColumns = new ToolStripMenuItem();
			this.saveLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.cmdCreateWithSomeConsumer = new ToolStripMenuItem();
			this.dgvMain = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_ShiftDateShort = new DataGridViewTextBoxColumn();
			this.f_OnDuty1Short = new DataGridViewTextBoxColumn();
			this.f_OffDuty1Short = new DataGridViewTextBoxColumn();
			this.f_Desc1 = new DataGridViewTextBoxColumn();
			this.f_Desc2 = new DataGridViewTextBoxColumn();
			this.f_Desc3 = new DataGridViewTextBoxColumn();
			this.toolStrip2 = new ToolStrip();
			this.lblLog = new ToolStripLabel();
			this.userControlFind1 = new UserControlFind();
			this.toolStrip3 = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.toolStripLabel3 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnPatrolSetup = new ToolStripButton();
			this.btnPatrolRoute = new ToolStripButton();
			this.btnPatrolTask = new ToolStripButton();
			this.btnCreateReport = new ToolStripButton();
			this.btnStatistics = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnFindOption = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip2.SuspendLayout();
			this.toolStrip3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnSelectColumns,
				this.saveLayoutToolStripMenuItem,
				this.restoreDefaultLayoutToolStripMenuItem,
				this.cmdCreateWithSomeConsumer
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.btnSelectColumns.Name = "btnSelectColumns";
			componentResourceManager.ApplyResources(this.btnSelectColumns, "btnSelectColumns");
			this.btnSelectColumns.Click += new EventHandler(this.btnSelectColumns_Click);
			this.saveLayoutToolStripMenuItem.Name = "saveLayoutToolStripMenuItem";
			componentResourceManager.ApplyResources(this.saveLayoutToolStripMenuItem, "saveLayoutToolStripMenuItem");
			this.saveLayoutToolStripMenuItem.Click += new EventHandler(this.saveLayoutToolStripMenuItem_Click);
			this.restoreDefaultLayoutToolStripMenuItem.Name = "restoreDefaultLayoutToolStripMenuItem";
			componentResourceManager.ApplyResources(this.restoreDefaultLayoutToolStripMenuItem, "restoreDefaultLayoutToolStripMenuItem");
			this.restoreDefaultLayoutToolStripMenuItem.Click += new EventHandler(this.restoreDefaultLayoutToolStripMenuItem_Click);
			this.cmdCreateWithSomeConsumer.Name = "cmdCreateWithSomeConsumer";
			componentResourceManager.ApplyResources(this.cmdCreateWithSomeConsumer, "cmdCreateWithSomeConsumer");
			this.cmdCreateWithSomeConsumer.Click += new EventHandler(this.cmdCreateWithSomeConsumer_Click);
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
				this.f_OnDuty1Short,
				this.f_OffDuty1Short,
				this.f_Desc1,
				this.f_Desc2,
				this.f_Desc3
			});
			this.dgvMain.ContextMenuStrip = this.contextMenuStrip1;
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
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
			componentResourceManager.ApplyResources(this.f_OnDuty1Short, "f_OnDuty1Short");
			this.f_OnDuty1Short.Name = "f_OnDuty1Short";
			this.f_OnDuty1Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OffDuty1Short, "f_OffDuty1Short");
			this.f_OffDuty1Short.Name = "f_OffDuty1Short";
			this.f_OffDuty1Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc1, "f_Desc1");
			this.f_Desc1.Name = "f_Desc1";
			this.f_Desc1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Desc2, "f_Desc2");
			this.f_Desc2.Name = "f_Desc2";
			this.f_Desc2.ReadOnly = true;
			this.f_Desc3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Desc3, "f_Desc3");
			this.f_Desc3.Name = "f_Desc3";
			this.f_Desc3.ReadOnly = true;
			this.toolStrip2.BackColor = Color.Transparent;
			this.toolStrip2.BackgroundImage = Resources.pTools_third_title;
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.lblLog
			});
			this.toolStrip2.Name = "toolStrip2";
			this.lblLog.ForeColor = Color.White;
			this.lblLog.Name = "lblLog";
			componentResourceManager.ApplyResources(this.lblLog, "lblLog");
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.Name = "userControlFind1";
			this.toolStrip3.BackColor = Color.Transparent;
			this.toolStrip3.BackgroundImage = Resources.pTools_second_title;
			componentResourceManager.ApplyResources(this.toolStrip3, "toolStrip3");
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.toolStripLabel3
			});
			this.toolStrip3.Name = "toolStrip3";
			this.toolStripLabel2.ForeColor = Color.White;
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.toolStripLabel3.ForeColor = Color.White;
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnPatrolSetup,
				this.btnPatrolRoute,
				this.btnPatrolTask,
				this.btnCreateReport,
				this.btnStatistics,
				this.btnPrint,
				this.btnExportToExcel,
				this.btnFindOption,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnPatrolSetup.ForeColor = Color.White;
			this.btnPatrolSetup.Image = Resources.pTools_TypeSetup;
			componentResourceManager.ApplyResources(this.btnPatrolSetup, "btnPatrolSetup");
			this.btnPatrolSetup.Name = "btnPatrolSetup";
			this.btnPatrolSetup.Click += new EventHandler(this.btnPatrolSetup_Click);
			this.btnPatrolRoute.ForeColor = Color.White;
			this.btnPatrolRoute.Image = Resources.pTools_Edit_Batch;
			componentResourceManager.ApplyResources(this.btnPatrolRoute, "btnPatrolRoute");
			this.btnPatrolRoute.Name = "btnPatrolRoute";
			this.btnPatrolRoute.Click += new EventHandler(this.btnPatrolRoute_Click);
			this.btnPatrolTask.ForeColor = Color.White;
			this.btnPatrolTask.Image = Resources.pTools_EditPrivielge;
			componentResourceManager.ApplyResources(this.btnPatrolTask, "btnPatrolTask");
			this.btnPatrolTask.Name = "btnPatrolTask";
			this.btnPatrolTask.Click += new EventHandler(this.btnPatrolTask_Click);
			this.btnCreateReport.ForeColor = Color.White;
			this.btnCreateReport.Image = Resources.pTools_CreateShiftReport;
			componentResourceManager.ApplyResources(this.btnCreateReport, "btnCreateReport");
			this.btnCreateReport.Name = "btnCreateReport";
			this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
			this.btnStatistics.ForeColor = Color.White;
			this.btnStatistics.Image = Resources.pTools_StatisticsReport;
			componentResourceManager.ApplyResources(this.btnStatistics, "btnStatistics");
			this.btnStatistics.Name = "btnStatistics";
			this.btnStatistics.Click += new EventHandler(this.btnStatistics_Click);
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			this.btnFindOption.ForeColor = Color.White;
			this.btnFindOption.Image = Resources.pTools_QueryOption;
			componentResourceManager.ApplyResources(this.btnFindOption, "btnFindOption");
			this.btnFindOption.Name = "btnFindOption";
			this.btnFindOption.Click += new EventHandler(this.btnFindOption_Click);
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmPatrolReport";
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

		public frmPatrolReport()
		{
			this.InitializeComponent();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuPatrolDetailData";
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
			this.dtpDateFrom = new frmPatrolReport.ToolStripDateTime();
			this.dtpDateTo = new frmPatrolReport.ToolStripDateTime();
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
			string cmdText = "SELECT * FROM  t_a_SystemParam WHERE [f_NO]=29 ";
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
			string cmdText = "SELECT * FROM  t_a_SystemParam WHERE [f_NO]=29 ";
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
			this.dgvMain.AutoGenerateColumns = false;
			this.arrColsName.Clear();
			this.arrColsShow.Clear();
			for (int i = 0; i < this.dgvMain.ColumnCount; i++)
			{
				this.arrColsName.Add(this.dgvMain.Columns[i].HeaderText);
				this.arrColsShow.Add(this.dgvMain.Columns[i].Visible);
			}
			string text = "";
			string text2 = "";
			for (int j = 0; j < this.arrColsName.Count; j++)
			{
				if (text != "")
				{
					text += ",";
					text2 += ",";
				}
				text += this.arrColsName[j];
				text2 += this.arrColsShow[j].ToString();
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
						for (int k = 0; k < this.dgvMain.ColumnCount; k++)
						{
							this.dgvMain.Columns[k].Visible = bool.Parse(array2[k]);
							this.arrColsShow.Add(this.dgvMain.Columns[k].Visible);
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
				strSql += string.Format(" AND t_d_PatrolDetailData.f_RecID > {0:d}", this.recIdMax);
			}
			else
			{
				strSql += string.Format(" WHERE t_d_PatrolDetailData.f_RecID > {0:d}", this.recIdMax);
			}
			strSql += " ORDER BY t_d_PatrolDetailData.f_RecID ";
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

		private string getSqlFindNormal(string strBaseInfo, string fromMainDt, string strTimeCon, int groupMinNO, int groupIDOfMinNO, int groupMaxNO, string findName, long findCard, int findConsumerID)
		{
			string result = "";
			try
			{
				string text = "";
				if (!string.IsNullOrEmpty(strTimeCon))
				{
					text += string.Format("AND {0}", strTimeCon);
				}
				if (findConsumerID > 0)
				{
					text += string.Format("AND   t_b_Consumer.f_ConsumerID ={0:d} ", findConsumerID);
					result = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  LEFT JOIN t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID)) LEFT JOIN t_d_PatrolRouteList on ( t_d_PatrolRouteList.f_RouteID = {0}.f_RouteID))  LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  )  ", fromMainDt, text);
					return result;
				}
				if (!string.IsNullOrEmpty(findName))
				{
					text += string.Format(" AND t_b_Consumer.f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", findName)));
				}
				if (findCard > 0L)
				{
					text += string.Format(" AND t_b_Consumer.f_CardNO ={0:d} ", findCard);
				}
				if (groupMinNO > 0)
				{
					if (groupMinNO >= groupMaxNO)
					{
						result = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) LEFT JOIN t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID)) LEFT JOIN t_d_PatrolRouteList on ( t_d_PatrolRouteList.f_RouteID = {0}.f_RouteID)) INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text, string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", groupIDOfMinNO));
					}
					else
					{
						result = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) LEFT JOIN t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID)) LEFT JOIN t_d_PatrolRouteList on ( t_d_PatrolRouteList.f_RouteID = {0}.f_RouteID)) INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text, string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", groupMinNO) + string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", groupMaxNO));
					}
				}
				else
				{
					result = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) LEFT JOIN t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID)) LEFT JOIN t_d_PatrolRouteList on ( t_d_PatrolRouteList.f_RouteID = {0}.f_RouteID)) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public string getEventDescStr(int code)
		{
			string result = "";
			switch (code)
			{
			case 0:
				result = CommonStr.strPatrolEventRest;
				break;
			case 1:
				result = CommonStr.strPatrolEventNormal;
				break;
			case 2:
				result = CommonStr.strPatrolEventEarly;
				break;
			case 3:
				result = CommonStr.strPatrolEventLate;
				break;
			case 4:
				result = CommonStr.strPatrolEventAbsence;
				break;
			}
			return result;
		}

		public void btnQuery_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			this.getLogCreateReport();
			if (!this.bFirstQuery && !this.bLogCreateReport)
			{
				XMessageBox.Show(this, CommonStr.strGetRecordsBeforeCreateReport + "\r\n\r\n" + CommonStr.strCreateInAdvance, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.bFirstQuery = false;
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
			string text = " SELECT t_d_PatrolDetailData.f_RecID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
			text += " t_d_PatrolDetailData.f_PlanPatrolTime AS f_patroldate, ";
			text += " t_d_PatrolDetailData.f_PlanPatrolTime, ";
			text += " t_d_PatrolDetailData.f_RealPatrolTime, ";
			if (wgAppConfig.IsAccessDB)
			{
				text += string.Format("IIF(f_EventDesc=0, {0} ,IIF(f_EventDesc=1,{1}, IIF(f_EventDesc=2, {2}, IIF(f_EventDesc=3, {3}, IIF(f_EventDesc=4, {4},''))))) AS  [f_EventDesc] ,  ", new object[]
				{
					wgTools.PrepareStr(this.getEventDescStr(0)),
					wgTools.PrepareStr(this.getEventDescStr(1)),
					wgTools.PrepareStr(this.getEventDescStr(2)),
					wgTools.PrepareStr(this.getEventDescStr(3)),
					wgTools.PrepareStr(this.getEventDescStr(4))
				});
			}
			else
			{
				text += string.Format("CASE WHEN f_EventDesc=0 THEN {0} ELSE ( CASE WHEN f_EventDesc=1 THEN {1} ELSE ( CASE WHEN f_EventDesc=2 THEN {2} ELSE (CASE WHEN f_EventDesc=3 THEN {3} ELSE (CASE WHEN f_EventDesc=4 THEN {4} ELSE '' END) END) END) END) END AS  [f_EventDesc] ,  ", new object[]
				{
					wgTools.PrepareStr(this.getEventDescStr(0)),
					wgTools.PrepareStr(this.getEventDescStr(1)),
					wgTools.PrepareStr(this.getEventDescStr(2)),
					wgTools.PrepareStr(this.getEventDescStr(3)),
					wgTools.PrepareStr(this.getEventDescStr(4))
				});
			}
			text += " f_RouteName, ";
			text += " f_ReaderName, ";
			text += " '' as f_Description";
			string text2 = this.getSqlFindNormal(text, "t_d_PatrolDetailData", this.getSqlOfDateTime("t_d_PatrolDetailData.f_patroldate"), groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			if (flag)
			{
				text2 = text2 + " WHERE " + str;
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
					int num = 0;
					while (num < dt.Columns.Count && num < this.dgvMain.Columns.Count)
					{
						this.dgvMain.Columns[num].DataPropertyName = dt.Columns[num].ColumnName;
						this.dgvMain.Columns[num].Name = dt.Columns[num].ColumnName;
						num++;
					}
					wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_PatrolDate", wgTools.DisplayFormat_DateYMDWeek);
					wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_PlanPatrolTime", "HH:mm");
					wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_RealPatrolTime", "HH:mm:ss");
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
			string text3 = " SELECT   f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO ";
			string text4 = text3;
			text4 += " FROM t_b_Consumer WHERE (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers )) ";
			if (num5 > 0)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer WHERE ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers ))  ) ";
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
				text4 += " AND ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers )) ) ";
			}
			else if (text2 != "")
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				text4 += " AND ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers )) ) ";
			}
			else if (num4 > 0L)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
				text4 += " AND ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers ))  ) ";
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
						if (form.Name == "dfrmPatrolReportCreate")
						{
							return;
						}
					}
				}
				using (dfrmPatrolReportCreate dfrmPatrolReportCreate = new dfrmPatrolReportCreate())
				{
					dfrmPatrolReportCreate.totalConsumer = totalConsumer;
					dfrmPatrolReportCreate.dtBegin = this.dtpDateFrom.Value;
					dfrmPatrolReportCreate.dtEnd = this.dtpDateTo.Value;
					dfrmPatrolReportCreate.strConsumerSql = text4;
					dfrmPatrolReportCreate.groupName = this.userControlFind1.cboFindDept.Text;
					dfrmPatrolReportCreate.TopMost = true;
					dfrmPatrolReportCreate.ShowDialog(this);
					this.btnQuery_Click(null, null);
				}
			}
		}

		private void btnStatistics_Click(object sender, EventArgs e)
		{
			using (frmPatrolStatistics frmPatrolStatistics = new frmPatrolStatistics())
			{
				frmPatrolStatistics.ShowDialog(this);
			}
		}

		private void btnFindOption_Click(object sender, EventArgs e)
		{
			if (this.dfrmFindOption == null)
			{
				this.dfrmFindOption = new dfrmPatrolReportFindOption();
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
			string text3 = " SELECT   f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO ";
			string text4 = text3;
			text4 += " FROM t_b_Consumer WHERE ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers ))  ) ";
			if (num5 > 0)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer WHERE ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers )) ) ";
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
				text4 += " AND ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers )) ) ";
			}
			else if (text2 != "")
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				text4 += " AND ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers ))  ) ";
			}
			else if (num4 > 0L)
			{
				text4 = text3;
				text4 += " FROM t_b_Consumer  ";
				text4 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
				text4 += " AND ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers ))  ) ";
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
			text4 += string.Format(" WHERE f_ConsumerID IN ({0}) AND {1} ", dfrmUserSelected.selectedUsers, "   ( (t_b_Consumer.f_ConsumerID IN (SELECT t_d_PatrolUsers.f_ConsumerID FROM t_d_PatrolUsers ))  ) ");
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
						if (form.Name == "dfrmPatrolReportCreate")
						{
							return;
						}
					}
				}
				using (dfrmPatrolReportCreate dfrmPatrolReportCreate = new dfrmPatrolReportCreate())
				{
					dfrmPatrolReportCreate.totalConsumer = totalConsumer;
					dfrmPatrolReportCreate.dtBegin = this.dtpDateFrom.Value;
					dfrmPatrolReportCreate.dtEnd = this.dtpDateTo.Value;
					dfrmPatrolReportCreate.strConsumerSql = text4;
					dfrmPatrolReportCreate.groupName = this.userControlFind1.cboFindDept.Text;
					dfrmPatrolReportCreate.TopMost = true;
					dfrmPatrolReportCreate.ShowDialog(this);
					this.btnQuery_Click(null, null);
				}
			}
		}

		private void btnPatrolSetup_Click(object sender, EventArgs e)
		{
			using (dfrmPatrolSetup dfrmPatrolSetup = new dfrmPatrolSetup())
			{
				dfrmPatrolSetup.ShowDialog();
			}
		}

		private void btnPatrolRoute_Click(object sender, EventArgs e)
		{
			using (frmPatrolRoute frmPatrolRoute = new frmPatrolRoute())
			{
				frmPatrolRoute.ShowDialog();
			}
		}

		private void btnPatrolTask_Click(object sender, EventArgs e)
		{
			using (frmPatrolTaskData frmPatrolTaskData = new frmPatrolTaskData())
			{
				frmPatrolTaskData.ShowDialog();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
