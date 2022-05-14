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

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class frmPatrolStatistics : frmN3000
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

			public ToolStripDateTime() : base(frmPatrolStatistics.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmPatrolStatistics.ToolStripDateTime.dtp != null)
				{
					frmPatrolStatistics.ToolStripDateTime.dtp.Dispose();
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

		private bool bLogCreateReport;

		private DateTime logDateStart;

		private DateTime logDateEnd;

		private DataSet dsDefaultStyle = new DataSet("DGV_STILE");

		private int recIdMax;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

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

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem saveLayoutToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultLayoutToolStripMenuItem;

		private ToolStripMenuItem cmdQueryNormalShift;

		private ToolStripMenuItem cmdQueryOtherShift;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_DayRealWork;

		private DataGridViewTextBoxColumn f_LeaveEarlyCount;

		private DataGridViewTextBoxColumn f_LateCount;

		private DataGridViewTextBoxColumn f_AbsenceDays;

		private DataGridViewTextBoxColumn f_ManualReadTimesCount;

		public frmPatrolStatistics()
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
			string cmdText = "SELECT * FROM  t_a_SystemParam WHERE [f_NO]=29 ";
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
				strSql += string.Format(" AND t_d_PatrolStatistic.f_RecID > {0:d}", this.recIdMax);
			}
			else
			{
				strSql += string.Format(" WHERE t_d_PatrolStatistic.f_RecID > {0:d}", this.recIdMax);
			}
			strSql += " ORDER BY t_d_PatrolStatistic.f_RecID ";
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

		private void btnQuery_Click(object sender, EventArgs e)
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
			string text = " SELECT t_d_PatrolStatistic.f_RecID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
			if (wgAppConfig.IsAccessDB)
			{
				text += "IIF((IIF(ISNULL([f_TotalNormal]           ),0,[f_TotalNormal]            )) >0 ,IIF((IIF(ISNULL([f_TotalNormal]           ),0,[f_TotalNormal]            )) <1 , '0.5', CSTR(t_d_PatrolStatistic.[f_TotalNormal]            ) ) , ' ') AS  [f_TotalNormal]           ,  ";
				text += "IIF((IIF(ISNULL([f_TotalEarly]           ),0,[f_TotalEarly]            )) >0 ,IIF((IIF(ISNULL([f_TotalEarly]           ),0,[f_TotalEarly]            )) <1 , '0.5', CSTR(t_d_PatrolStatistic.[f_TotalEarly]            ) ) , ' ') AS  [f_TotalEarly]           ,  ";
				text += "IIF((IIF(ISNULL([f_TotalLate]           ),0,[f_TotalLate]            )) >0 ,IIF((IIF(ISNULL([f_TotalLate]           ),0,[f_TotalLate]            )) <1 , '0.5', CSTR(t_d_PatrolStatistic.[f_TotalLate]            ) ) , ' ') AS  [f_TotalLate]           ,  ";
				text += "IIF((IIF(ISNULL([f_TotalAbsence]           ),0,[f_TotalAbsence]            )) >0 ,IIF((IIF(ISNULL([f_TotalAbsence]           ),0,[f_TotalAbsence]            )) <1 , '0.5', CSTR(t_d_PatrolStatistic.[f_TotalAbsence]            ) ) , ' ') AS  [f_TotalAbsence]           ,  ";
			}
			else
			{
				text += "CASE WHEN CONVERT(decimal(10,1),[f_TotalNormal]     ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_TotalNormal]     ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_TotalNormal]      ) END) ELSE ' ' END  [f_TotalNormal]     ,  ";
				text += "CASE WHEN CONVERT(decimal(10,1),[f_TotalEarly]     ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_TotalEarly]     ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_TotalEarly]      ) END) ELSE ' ' END  [f_TotalEarly]     ,  ";
				text += "CASE WHEN CONVERT(decimal(10,1),[f_TotalLate]     ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_TotalLate]     ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_TotalLate]      ) END) ELSE ' ' END  [f_TotalLate]     ,  ";
				text += "CASE WHEN CONVERT(decimal(10,1),[f_TotalAbsence]     ) >0 THEN (CASE WHEN CONVERT(decimal(10,1),[f_TotalAbsence]     ) <1 THEN  '0.5' ELSE  CONVERT(varchar(6),[f_TotalAbsence]      ) END) ELSE ' ' END  [f_TotalAbsence]     ,  ";
			}
			text += " f_PatrolDateStart, ";
			text += " f_PatrolDateEnd ";
			string sqlFindNormal = wgAppConfig.getSqlFindNormal(text, "t_d_PatrolStatistic", "", groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindNormal);
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
						num++;
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPatrolStatistics));
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.dgvMain = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_DayRealWork = new DataGridViewTextBoxColumn();
			this.f_LeaveEarlyCount = new DataGridViewTextBoxColumn();
			this.f_LateCount = new DataGridViewTextBoxColumn();
			this.f_AbsenceDays = new DataGridViewTextBoxColumn();
			this.f_ManualReadTimesCount = new DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.saveLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.cmdQueryNormalShift = new ToolStripMenuItem();
			this.cmdQueryOtherShift = new ToolStripMenuItem();
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
				this.f_DayRealWork,
				this.f_LeaveEarlyCount,
				this.f_LateCount,
				this.f_AbsenceDays,
				this.f_ManualReadTimesCount
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
			componentResourceManager.ApplyResources(this.f_DayRealWork, "f_DayRealWork");
			this.f_DayRealWork.Name = "f_DayRealWork";
			this.f_DayRealWork.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LeaveEarlyCount, "f_LeaveEarlyCount");
			this.f_LeaveEarlyCount.Name = "f_LeaveEarlyCount";
			this.f_LeaveEarlyCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LateCount, "f_LateCount");
			this.f_LateCount.Name = "f_LateCount";
			this.f_LateCount.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_AbsenceDays, "f_AbsenceDays");
			this.f_AbsenceDays.Name = "f_AbsenceDays";
			this.f_AbsenceDays.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ManualReadTimesCount, "f_ManualReadTimesCount");
			this.f_ManualReadTimesCount.Name = "f_ManualReadTimesCount";
			this.f_ManualReadTimesCount.ReadOnly = true;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.saveLayoutToolStripMenuItem,
				this.restoreDefaultLayoutToolStripMenuItem,
				this.cmdQueryNormalShift,
				this.cmdQueryOtherShift
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.saveLayoutToolStripMenuItem.Name = "saveLayoutToolStripMenuItem";
			componentResourceManager.ApplyResources(this.saveLayoutToolStripMenuItem, "saveLayoutToolStripMenuItem");
			this.saveLayoutToolStripMenuItem.Click += new EventHandler(this.saveLayoutToolStripMenuItem_Click);
			this.restoreDefaultLayoutToolStripMenuItem.Name = "restoreDefaultLayoutToolStripMenuItem";
			componentResourceManager.ApplyResources(this.restoreDefaultLayoutToolStripMenuItem, "restoreDefaultLayoutToolStripMenuItem");
			this.restoreDefaultLayoutToolStripMenuItem.Click += new EventHandler(this.restoreDefaultLayoutToolStripMenuItem_Click);
			this.cmdQueryNormalShift.Name = "cmdQueryNormalShift";
			componentResourceManager.ApplyResources(this.cmdQueryNormalShift, "cmdQueryNormalShift");
			this.cmdQueryNormalShift.Click += new EventHandler(this.btnQuery_Click);
			this.cmdQueryOtherShift.Name = "cmdQueryOtherShift";
			componentResourceManager.ApplyResources(this.cmdQueryOtherShift, "cmdQueryOtherShift");
			this.cmdQueryOtherShift.Click += new EventHandler(this.btnQuery_Click);
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
			this.userControlFind1.ForeColor = Color.White;
			this.userControlFind1.Name = "userControlFind1";
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnPrint,
				this.btnExportToExcel,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
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
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmPatrolStatistics";
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
	}
}
