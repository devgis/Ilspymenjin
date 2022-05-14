using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class frmShiftAttStatistics : frmN3000
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

			public ToolStripDateTime() : base(frmShiftAttStatistics.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmShiftAttStatistics.ToolStripDateTime.dtp != null)
				{
					frmShiftAttStatistics.ToolStripDateTime.dtp.Dispose();
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

		private UserControlFindSecond userControlFind1;

		private ToolStripButton btnExit;

		private ToolStrip toolStrip2;

		private ToolStripLabel lblLog;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_DayShouldWork;

		private DataGridViewTextBoxColumn f_DayRealWork;

		private DataGridViewTextBoxColumn f_LateMinutes;

		private DataGridViewTextBoxColumn f_LateCount;

		private DataGridViewTextBoxColumn f_LeaveEarlyMinutes;

		private DataGridViewTextBoxColumn f_LeaveEarlyCount;

		private DataGridViewTextBoxColumn f_OvertimeHours;

		private DataGridViewTextBoxColumn f_AbsenceDays;

		private DataGridViewTextBoxColumn f_NotReadCardCount;

		private DataGridViewTextBoxColumn f_ManualReadTimesCount;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem saveLayoutToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultLayoutToolStripMenuItem;

		private ToolStripMenuItem cmdQueryNormalShift;

		private ToolStripMenuItem cmdQueryOtherShift;

		private ToolStripMenuItem displayAllToolStripMenuItem;

		private bool bLogCreateReport;

		private DateTime logDateStart;

		private DateTime logDateEnd;

		private DataSet dsDefaultStyle = new DataSet("DGV_STILE");

		private DataGridViewTextBoxColumn dc;

		private int recIdMax;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmShiftAttStatistics));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.dgvMain = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_DayShouldWork = new DataGridViewTextBoxColumn();
			this.f_DayRealWork = new DataGridViewTextBoxColumn();
			this.f_LateMinutes = new DataGridViewTextBoxColumn();
			this.f_LateCount = new DataGridViewTextBoxColumn();
			this.f_LeaveEarlyMinutes = new DataGridViewTextBoxColumn();
			this.f_LeaveEarlyCount = new DataGridViewTextBoxColumn();
			this.f_OvertimeHours = new DataGridViewTextBoxColumn();
			this.f_AbsenceDays = new DataGridViewTextBoxColumn();
			this.f_NotReadCardCount = new DataGridViewTextBoxColumn();
			this.f_ManualReadTimesCount = new DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.saveLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.cmdQueryNormalShift = new ToolStripMenuItem();
			this.cmdQueryOtherShift = new ToolStripMenuItem();
			this.displayAllToolStripMenuItem = new ToolStripMenuItem();
			this.toolStrip2 = new ToolStrip();
			this.lblLog = new ToolStripLabel();
			this.userControlFind1 = new UserControlFindSecond();
			this.toolStrip1 = new ToolStrip();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
				this.f_DayShouldWork,
				this.f_DayRealWork,
				this.f_LateMinutes,
				this.f_LateCount,
				this.f_LeaveEarlyMinutes,
				this.f_LeaveEarlyCount,
				this.f_OvertimeHours,
				this.f_AbsenceDays,
				this.f_NotReadCardCount,
				this.f_ManualReadTimesCount
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
			dataGridViewCellStyle4.NullValue = null;
			this.f_DayShouldWork.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_DayShouldWork, "f_DayShouldWork");
			this.f_DayShouldWork.Name = "f_DayShouldWork";
			this.f_DayShouldWork.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DayRealWork, "f_DayRealWork");
			this.f_DayRealWork.Name = "f_DayRealWork";
			this.f_DayRealWork.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LateMinutes, "f_LateMinutes");
			this.f_LateMinutes.Name = "f_LateMinutes";
			this.f_LateMinutes.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LateCount, "f_LateCount");
			this.f_LateCount.Name = "f_LateCount";
			this.f_LateCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LeaveEarlyMinutes, "f_LeaveEarlyMinutes");
			this.f_LeaveEarlyMinutes.Name = "f_LeaveEarlyMinutes";
			this.f_LeaveEarlyMinutes.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LeaveEarlyCount, "f_LeaveEarlyCount");
			this.f_LeaveEarlyCount.Name = "f_LeaveEarlyCount";
			this.f_LeaveEarlyCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OvertimeHours, "f_OvertimeHours");
			this.f_OvertimeHours.Name = "f_OvertimeHours";
			this.f_OvertimeHours.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_AbsenceDays, "f_AbsenceDays");
			this.f_AbsenceDays.Name = "f_AbsenceDays";
			this.f_AbsenceDays.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_NotReadCardCount, "f_NotReadCardCount");
			this.f_NotReadCardCount.Name = "f_NotReadCardCount";
			this.f_NotReadCardCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ManualReadTimesCount, "f_ManualReadTimesCount");
			this.f_ManualReadTimesCount.Name = "f_ManualReadTimesCount";
			this.f_ManualReadTimesCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.saveLayoutToolStripMenuItem,
				this.restoreDefaultLayoutToolStripMenuItem,
				this.cmdQueryNormalShift,
				this.cmdQueryOtherShift,
				this.displayAllToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
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
			componentResourceManager.ApplyResources(this.displayAllToolStripMenuItem, "displayAllToolStripMenuItem");
			this.displayAllToolStripMenuItem.Name = "displayAllToolStripMenuItem";
			this.displayAllToolStripMenuItem.Click += new EventHandler(this.displayAllToolStripMenuItem_Click);
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
			this.userControlFind1.ForeColor = Color.White;
			this.userControlFind1.Name = "userControlFind1";
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnPrint,
				this.btnExportToExcel,
				this.btnExit
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
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmShiftAttStatistics";
			base.FormClosing += new FormClosingEventHandler(this.frmShiftAttReport_FormClosing);
			base.Load += new EventHandler(this.frmShiftAttStatistics_Load);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmShiftAttStatistics()
		{
			this.InitializeComponent();
		}

		private void frmShiftAttStatistics_Load(object sender, EventArgs e)
		{
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.userControlFind1.toolStripLabel2.Visible = false;
			this.userControlFind1.txtFindCardID.Visible = false;
			this.saveDefaultStyle();
			this.loadStyle();
			Cursor.Current = Cursors.WaitCursor;
			this.getLogCreateReport();
			bool arg_96_0 = this.bLogCreateReport;
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
					using (new SqlDataAdapter(sqlCommand))
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
		}

		public void getLogCreateReport_Acc()
		{
			this.bLogCreateReport = false;
			string cmdText = "SELECT * FROM t_a_Attendence WHERE [f_NO]=15 ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					using (new OleDbDataAdapter(oleDbCommand))
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
		}

		public string getlocalizedHolidayType(string type)
		{
			string text = "";
			try
			{
				string result;
				if (string.IsNullOrEmpty(type))
				{
					result = type;
					return result;
				}
				text = type;
				if (type == "出差" || type == "出差" || type == "Business Trip")
				{
					text = CommonStr.strBusinessTrip;
				}
				if (type == "病假" || type == "病假" || type == "Sick Leave")
				{
					text = CommonStr.strSickLeave;
				}
				if (type == "事假" || type == "事假" || type == "Private Leave")
				{
					text = CommonStr.strPrivateLeave;
				}
				result = text;
				return result;
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
			return text;
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
				this.dgvMain.Columns[10].HeaderText = CommonStr.strWorkHour;
			}
			if (this.OnlyOnDutySpecial())
			{
				this.dgvMain.Columns[8].Visible = false;
				this.dgvMain.Columns[9].Visible = false;
				this.dgvMain.Columns[10].Visible = false;
				this.dgvMain.Columns[12].Visible = false;
			}
			this.dgvMain.AutoGenerateColumns = false;
			string cmdText = " SELECT * FROM t_a_HolidayType ORDER BY f_NO ";
			int i = 0;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						string text = "f_SpecialType" + (i + 1);
						this.dc = new DataGridViewTextBoxColumn();
						this.dc.HeaderText = this.getlocalizedHolidayType(sqlDataReader["f_HolidayType"].ToString()) + "\r\n(" + CommonStr.strDay + ")";
						this.dc.DataPropertyName = text;
						this.dc.Width = 45;
						this.dc.ReadOnly = true;
						this.dc.Visible = true;
						this.dgvMain.Columns.Add(this.dc);
						i++;
					}
					sqlDataReader.Close();
					while (i < 32)
					{
						string text = "f_SpecialType" + (i + 1);
						this.dc = new DataGridViewTextBoxColumn();
						this.dc.HeaderText = text;
						this.dc.DataPropertyName = text;
						this.dc.Width = 45;
						this.dc.ReadOnly = true;
						this.dc.Visible = false;
						this.dgvMain.Columns.Add(this.dc);
						i++;
					}
					wgAppConfig.ReadGVStyle(this, this.dgvMain);
				}
			}
		}

		private void loadStyle_Acc()
		{
			if (this.OnlyTwoTimesSpecial())
			{
				this.dgvMain.Columns[10].HeaderText = CommonStr.strWorkHour;
			}
			if (this.OnlyOnDutySpecial())
			{
				this.dgvMain.Columns[8].Visible = false;
				this.dgvMain.Columns[9].Visible = false;
				this.dgvMain.Columns[10].Visible = false;
				this.dgvMain.Columns[12].Visible = false;
			}
			this.dgvMain.AutoGenerateColumns = false;
			string cmdText = " SELECT * FROM t_a_HolidayType ORDER BY f_NO ";
			int i = 0;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						string text = "f_SpecialType" + (i + 1);
						this.dc = new DataGridViewTextBoxColumn();
						this.dc.HeaderText = this.getlocalizedHolidayType(oleDbDataReader["f_HolidayType"].ToString()) + "\r\n(" + CommonStr.strDay + ")";
						this.dc.DataPropertyName = text;
						this.dc.Width = 45;
						this.dc.ReadOnly = true;
						this.dc.Visible = true;
						this.dgvMain.Columns.Add(this.dc);
						i++;
					}
					oleDbDataReader.Close();
					while (i < 32)
					{
						string text = "f_SpecialType" + (i + 1);
						this.dc = new DataGridViewTextBoxColumn();
						this.dc.HeaderText = text;
						this.dc.DataPropertyName = text;
						this.dc.Width = 45;
						this.dc.ReadOnly = true;
						this.dc.Visible = false;
						this.dgvMain.Columns.Add(this.dc);
						i++;
					}
					wgAppConfig.ReadGVStyle(this, this.dgvMain);
				}
			}
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
				strSql += string.Format(" AND t_d_shift_AttStatistic.f_RecID > {0:d}", this.recIdMax);
			}
			else
			{
				strSql += string.Format(" WHERE t_d_shift_AttStatistic.f_RecID > {0:d}", this.recIdMax);
			}
			strSql += " ORDER BY t_d_shift_AttStatistic.f_RecID ";
			this.table = new DataTable();
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
					goto IL_186;
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
			IL_186:
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
			return " 1>0 ";
		}

		private void btnQuery_Click(object sender, EventArgs e)
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
				XMessageBox.Show(this, CommonStr.strCreateInAdvance, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_shift_AttStatistic.f_RecID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_DayShouldWork]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_DayShouldWork]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_DayShouldWork]        ) END) ELSE ' ' END  [f_DayShouldWork]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_DayRealWork]         ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_DayRealWork]         ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_DayRealWork]          ) END) ELSE ' ' END  [f_DayRealWork]         ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_LateMinutes]         ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_LateMinutes]         ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_LateMinutes]          ) END) ELSE ' ' END  [f_LateMinutes]         ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_LateCount]           ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_LateCount]           ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_LateCount]            ) END) ELSE ' ' END  [f_LateCount]           ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_LeaveEarlyMinutes]   ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_LeaveEarlyMinutes]   ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_LeaveEarlyMinutes]    ) END) ELSE ' ' END  [f_LeaveEarlyMinutes]   ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_LeaveEarlyCount]     ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_LeaveEarlyCount]     ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_LeaveEarlyCount]      ) END) ELSE ' ' END  [f_LeaveEarlyCount]     ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_OvertimeHours]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_OvertimeHours]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_OvertimeHours]        ) END) ELSE ' ' END  [f_OvertimeHours]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_AbsenceDays]         ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_AbsenceDays]         ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_AbsenceDays]          ) END) ELSE ' ' END  [f_AbsenceDays]         ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_NotReadCardCount]    ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_NotReadCardCount]    ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_NotReadCardCount]     ) END) ELSE ' ' END  [f_NotReadCardCount]    ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_ManualReadTimesCount]) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_ManualReadTimesCount]) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_ManualReadTimesCount] ) END) ELSE ' ' END  [f_ManualReadTimesCount],  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType1]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType1]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType1]         ) END) ELSE ' ' END  [f_SpecialType1]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType2]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType2]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType2]         ) END) ELSE ' ' END  [f_SpecialType2]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType3]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType3]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType3]         ) END) ELSE ' ' END  [f_SpecialType3]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType4]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType4]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType4]         ) END) ELSE ' ' END  [f_SpecialType4]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType5]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType5]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType5]         ) END) ELSE ' ' END  [f_SpecialType5]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType6]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType6]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType6]         ) END) ELSE ' ' END  [f_SpecialType6]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType7]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType7]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType7]         ) END) ELSE ' ' END  [f_SpecialType7]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType8]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType8]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType8]         ) END) ELSE ' ' END  [f_SpecialType8]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType9]        ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType9]        ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType9]         ) END) ELSE ' ' END  [f_SpecialType9]        ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType10]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType10]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType10]        ) END) ELSE ' ' END  [f_SpecialType10]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType11]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType11]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType11]        ) END) ELSE ' ' END  [f_SpecialType11]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType12]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType12]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType12]        ) END) ELSE ' ' END  [f_SpecialType12]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType13]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType13]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType13]        ) END) ELSE ' ' END  [f_SpecialType13]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType14]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType14]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType14]        ) END) ELSE ' ' END  [f_SpecialType14]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType15]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType15]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType15]        ) END) ELSE ' ' END  [f_SpecialType15]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType16]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType16]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType16]        ) END) ELSE ' ' END  [f_SpecialType16]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType17]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType17]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType17]        ) END) ELSE ' ' END  [f_SpecialType17]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType18]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType18]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType18]        ) END) ELSE ' ' END  [f_SpecialType18]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType19]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType19]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType19]        ) END) ELSE ' ' END  [f_SpecialType19]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType20]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType20]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType20]        ) END) ELSE ' ' END  [f_SpecialType20]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType21]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType21]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType21]        ) END) ELSE ' ' END  [f_SpecialType21]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType22]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType22]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType22]        ) END) ELSE ' ' END  [f_SpecialType22]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType23]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType23]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType23]        ) END) ELSE ' ' END  [f_SpecialType23]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType24]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType24]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType24]        ) END) ELSE ' ' END  [f_SpecialType24]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType25]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType25]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType25]        ) END) ELSE ' ' END  [f_SpecialType25]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType26]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType26]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType26]        ) END) ELSE ' ' END  [f_SpecialType26]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType27]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType27]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType27]        ) END) ELSE ' ' END  [f_SpecialType27]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType28]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType28]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType28]        ) END) ELSE ' ' END  [f_SpecialType28]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType29]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType29]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType29]        ) END) ELSE ' ' END  [f_SpecialType29]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType30]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType30]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType30]        ) END) ELSE ' ' END  [f_SpecialType30]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType31]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType31]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType31]        ) END) ELSE ' ' END  [f_SpecialType31]       ,  ";
			text += "CASE WHEN CONVERT(decimal(10,1),[f_SpecialType32]       ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_SpecialType32]       ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_SpecialType32]        ) END) ELSE ' ' END  [f_SpecialType32]          ";
			string text2 = wgAppConfig.getSqlFindNormal(text, "t_d_shift_AttStatistic", "", groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			string text3 = "";
			if (sender == this.cmdQueryNormalShift)
			{
				text3 = " AND t_d_shift_AttStatistic.f_ConsumerID IN (SELECT aaa.f_ConsumerID FROM t_b_Consumer aaa WHERE aaa.f_ShiftEnabled =0) ";
			}
			else if (sender == this.cmdQueryOtherShift)
			{
				text3 = " AND t_d_shift_AttStatistic.f_ConsumerID IN (SELECT aaa.f_ConsumerID FROM t_b_Consumer aaa WHERE aaa.f_ShiftEnabled =1) ";
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

		private void btnQuery_Click_Acc(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			this.getLogCreateReport();
			if (!this.bLogCreateReport)
			{
				XMessageBox.Show(this, CommonStr.strCreateInAdvance, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_shift_AttStatistic.f_RecID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
			text += "IIF((IIF(ISNULL([f_DayShouldWork]),0,[f_DayShouldWork])       ) >0 ,CSTR(t_d_shift_AttStatistic.[f_DayShouldWork]        ) , ' ') AS  [f_DayShouldWork]       ,  ";
			text += "IIF((IIF(ISNULL([f_DayRealWork]         ),0,[f_DayRealWork] )         ) >0 ,IIF((IIF(ISNULL([f_DayRealWork]         ),0,[f_DayRealWork]          )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_DayRealWork]          ) ) , ' ') AS  [f_DayRealWork]         ,  ";
			text += "IIF((IIF(ISNULL([f_LateMinutes]         ),0,[f_LateMinutes]          )) >0 ,IIF((IIF(ISNULL([f_LateMinutes]         ),0,[f_LateMinutes]          )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_LateMinutes]          ) ) , ' ') AS  [f_LateMinutes]         ,  ";
			text += "IIF((IIF(ISNULL([f_LateCount]           ),0,[f_LateCount]            )) >0 ,IIF((IIF(ISNULL([f_LateCount]           ),0,[f_LateCount]            )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_LateCount]            ) ) , ' ') AS  [f_LateCount]           ,  ";
			text += "IIF((IIF(ISNULL([f_LeaveEarlyMinutes]   ),0,[f_LeaveEarlyMinutes]    )) >0 ,IIF((IIF(ISNULL([f_LeaveEarlyMinutes]   ),0,[f_LeaveEarlyMinutes]    )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_LeaveEarlyMinutes]    ) ) , ' ') AS  [f_LeaveEarlyMinutes]   ,  ";
			text += "IIF((IIF(ISNULL([f_LeaveEarlyCount]     ),0,[f_LeaveEarlyCount]      )) >0 ,IIF((IIF(ISNULL([f_LeaveEarlyCount]     ),0,[f_LeaveEarlyCount]      )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_LeaveEarlyCount]      ) ) , ' ') AS  [f_LeaveEarlyCount]     ,  ";
			text += "IIF((IIF(ISNULL([f_OvertimeHours]       ),0,[f_OvertimeHours]        )) >0 ,IIF((IIF(ISNULL([f_OvertimeHours]       ),0,[f_OvertimeHours]        )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_OvertimeHours]        ) ) , ' ') AS  [f_OvertimeHours]       ,  ";
			text += "IIF((IIF(ISNULL([f_AbsenceDays]         ),0,[f_AbsenceDays]          )) >0 ,IIF((IIF(ISNULL([f_AbsenceDays]         ),0,[f_AbsenceDays]          )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_AbsenceDays]          ) ) , ' ') AS  [f_AbsenceDays]         ,  ";
			text += "IIF((IIF(ISNULL([f_NotReadCardCount]    ),0,[f_NotReadCardCount]     )) >0 ,IIF((IIF(ISNULL([f_NotReadCardCount]    ),0,[f_NotReadCardCount]     )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_NotReadCardCount]     ) ) , ' ') AS  [f_NotReadCardCount]    ,  ";
			text += "IIF((IIF(ISNULL([f_ManualReadTimesCount]),0,[f_ManualReadTimesCount] )) >0 ,IIF((IIF(ISNULL([f_ManualReadTimesCount]),0,[f_ManualReadTimesCount] )) <1 , '0.5', CSTR(t_d_shift_AttStatistic.[f_ManualReadTimesCount] ) ) , ' ') AS  [f_ManualReadTimesCount],  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType1]    ),0, [f_SpecialType1] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType1]    ),0, [f_SpecialType1] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType1]         ) )) , ' ') AS  [f_SpecialType1]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType2]    ),0, [f_SpecialType2] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType2]    ),0, [f_SpecialType2] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType2]         ) )) , ' ') AS  [f_SpecialType2]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType3]    ),0, [f_SpecialType3] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType3]    ),0, [f_SpecialType3] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType3]         ) )) , ' ') AS  [f_SpecialType3]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType4]    ),0, [f_SpecialType4] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType4]    ),0, [f_SpecialType4] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType4]         ) )) , ' ') AS  [f_SpecialType4]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType5]    ),0, [f_SpecialType5] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType5]    ),0, [f_SpecialType5] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType5]         ) )) , ' ') AS  [f_SpecialType5]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType6]    ),0, [f_SpecialType6] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType6]    ),0, [f_SpecialType6] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType6]         ) )) , ' ') AS  [f_SpecialType6]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType7]    ),0, [f_SpecialType7] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType7]    ),0, [f_SpecialType7] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType7]         ) )) , ' ') AS  [f_SpecialType7]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType8]    ),0, [f_SpecialType8] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType8]    ),0, [f_SpecialType8] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType8]         ) )) , ' ') AS  [f_SpecialType8]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType9]    ),0, [f_SpecialType9] )    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType9]    ),0, [f_SpecialType9] )    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType9]         ) )) , ' ') AS  [f_SpecialType9]        ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType10]   ),0, [f_SpecialType10])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType10]   ),0, [f_SpecialType10])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType10]        ) )) , ' ') AS  [f_SpecialType10]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType11]   ),0, [f_SpecialType11])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType11]   ),0, [f_SpecialType11])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType11]        ) )) , ' ') AS  [f_SpecialType11]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType12]   ),0, [f_SpecialType12])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType12]   ),0, [f_SpecialType12])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType12]        ) )) , ' ') AS  [f_SpecialType12]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType13]   ),0, [f_SpecialType13])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType13]   ),0, [f_SpecialType13])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType13]        ) )) , ' ') AS  [f_SpecialType13]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType14]   ),0, [f_SpecialType14])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType14]   ),0, [f_SpecialType14])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType14]        ) )) , ' ') AS  [f_SpecialType14]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType15]   ),0, [f_SpecialType15])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType15]   ),0, [f_SpecialType15])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType15]        ) )) , ' ') AS  [f_SpecialType15]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType16]   ),0, [f_SpecialType16])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType16]   ),0, [f_SpecialType16])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType16]        ) )) , ' ') AS  [f_SpecialType16]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType17]   ),0, [f_SpecialType17])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType17]   ),0, [f_SpecialType17])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType17]        ) )) , ' ') AS  [f_SpecialType17]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType18]   ),0, [f_SpecialType18])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType18]   ),0, [f_SpecialType18])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType18]        ) )) , ' ') AS  [f_SpecialType18]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType19]   ),0, [f_SpecialType19])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType19]   ),0, [f_SpecialType19])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType19]        ) )) , ' ') AS  [f_SpecialType19]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType20]   ),0, [f_SpecialType20])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType20]   ),0, [f_SpecialType20])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType20]        ) )) , ' ') AS  [f_SpecialType20]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType21]   ),0, [f_SpecialType21])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType21]   ),0, [f_SpecialType21])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType21]        ) )) , ' ') AS  [f_SpecialType21]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType22]   ),0, [f_SpecialType22])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType22]   ),0, [f_SpecialType22])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType22]        ) )) , ' ') AS  [f_SpecialType22]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType23]   ),0, [f_SpecialType23])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType23]   ),0, [f_SpecialType23])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType23]        ) )) , ' ') AS  [f_SpecialType23]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType24]   ),0, [f_SpecialType24])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType24]   ),0, [f_SpecialType24])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType24]        ) )) , ' ') AS  [f_SpecialType24]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType25]   ),0, [f_SpecialType25])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType25]   ),0, [f_SpecialType25])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType25]        ) )) , ' ') AS  [f_SpecialType25]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType26]   ),0, [f_SpecialType26])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType26]   ),0, [f_SpecialType26])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType26]        ) )) , ' ') AS  [f_SpecialType26]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType27]   ),0, [f_SpecialType27])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType27]   ),0, [f_SpecialType27])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType27]        ) )) , ' ') AS  [f_SpecialType27]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType28]   ),0, [f_SpecialType28])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType28]   ),0, [f_SpecialType28])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType28]        ) )) , ' ') AS  [f_SpecialType28]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType29]   ),0, [f_SpecialType29])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType29]   ),0, [f_SpecialType29])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType29]        ) )) , ' ') AS  [f_SpecialType29]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType30]   ),0, [f_SpecialType30])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType30]   ),0, [f_SpecialType30])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType30]        ) )) , ' ') AS  [f_SpecialType30]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType31]   ),0, [f_SpecialType31])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType31]   ),0, [f_SpecialType31])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType31]        ) )) , ' ') AS  [f_SpecialType31]       ,  ";
			text += "IIF(CDbl(IIF(ISNULL([f_SpecialType32]   ),0, [f_SpecialType32])    ) >0 ,(IIF(CDbl(IIF(ISNULL([f_SpecialType32]   ),0, [f_SpecialType32])    ) <1 , '0.5', (t_d_shift_AttStatistic.[f_SpecialType32]        ) )) , ' ') AS  [f_SpecialType32]          ";
			string text2 = wgAppConfig.getSqlFindNormal(text, "t_d_shift_AttStatistic", "", groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			string text3 = "";
			if (sender == this.cmdQueryNormalShift)
			{
				text3 = " AND t_d_shift_AttStatistic.f_ConsumerID IN (SELECT aaa.f_ConsumerID FROM t_b_Consumer aaa WHERE aaa.f_ShiftEnabled =0) ";
			}
			else if (sender == this.cmdQueryOtherShift)
			{
				text3 = " AND t_d_shift_AttStatistic.f_ConsumerID IN (SELECT aaa.f_ConsumerID FROM t_b_Consumer aaa WHERE aaa.f_ShiftEnabled =1) ";
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
					}
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

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.SaveDGVStyle(this, this.dgvMain);
			XMessageBox.Show(sender.ToString() + " " + CommonStr.strSuccessfully);
		}

		private void restoreDefaultLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.RestoreGVStyle(this, this.dgvMain);
			this.loadDefaultStyle();
			this.loadStyle();
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
