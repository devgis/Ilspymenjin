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
	public class dfrmShiftAttReportCreate : frmN3000
	{
		private IContainer components;

		private Label lblInfo;

		private ProgressBar progressBar1;

		private Button btnStop;

		private BackgroundWorker backgroundWorker1;

		private Label label1;

		private Label lblRuntime;

		private System.Windows.Forms.Timer timer1;

		public int totalConsumer;

		public string groupName = "";

		public DateTime dtBegin;

		public DateTime dtEnd;

		public string strConsumerSql = "";

		private comShift comShiftWork = new comShift();

		private comShift_Acc comShiftWork_Acc = new comShift_Acc();

		private DataTable dtShiftWorkSchedule;

		private DataTable dtAttReport;

		private DataTable dtAttStatistic;

		private DateTime startTime = DateTime.Now;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftAttReportCreate));
			this.lblInfo = new Label();
			this.progressBar1 = new ProgressBar();
			this.btnStop = new Button();
			this.backgroundWorker1 = new BackgroundWorker();
			this.label1 = new Label();
			this.lblRuntime = new Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.lblInfo, "lblInfo");
			this.lblInfo.BackColor = Color.Transparent;
			this.lblInfo.ForeColor = Color.White;
			this.lblInfo.Name = "lblInfo";
			componentResourceManager.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Name = "progressBar1";
			this.btnStop.BackColor = Color.Transparent;
			this.btnStop.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnStop, "btnStop");
			this.btnStop.ForeColor = Color.White;
			this.btnStop.Name = "btnStop";
			this.btnStop.UseVisualStyleBackColor = false;
			this.btnStop.Click += new EventHandler(this.btnStop_Click);
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.lblRuntime, "lblRuntime");
			this.lblRuntime.BackColor = Color.Transparent;
			this.lblRuntime.ForeColor = Color.White;
			this.lblRuntime.Name = "lblRuntime";
			this.timer1.Enabled = true;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.lblRuntime);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnStop);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.lblInfo);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftAttReportCreate";
			base.FormClosing += new FormClosingEventHandler(this.dfrmShiftAttReportCreate_FormClosing);
			base.Load += new EventHandler(this.dfrmShiftAttReportCreate_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmShiftAttReportCreate()
		{
			this.InitializeComponent();
		}

		private void dfrmShiftAttReportCreate_Load(object sender, EventArgs e)
		{
			this.progressBar1.Maximum = this.totalConsumer;
			this.label1.Text = ("[ " + this.totalConsumer.ToString() + " ]").PadLeft("[ 200000 ]".Length, ' ');
			this.StartCreate();
		}

		private void StartCreate()
		{
			if (this.backgroundWorker1.IsBusy)
			{
				return;
			}
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.dtBegin,
				this.dtEnd,
				this.strConsumerSql
			});
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			DateTime dateStart = (DateTime)((object[])e.Argument)[0];
			DateTime dateEnd = (DateTime)((object[])e.Argument)[1];
			string strSql = (string)((object[])e.Argument)[2];
			e.Result = this.ReportCreate(dateStart, dateEnd, strSql);
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				wgTools.WgDebugWrite("Users Operation was canceled", new object[0]);
				return;
			}
			if (e.Error != null)
			{
				string info = string.Format("An error occurred: {0}", e.Error.Message);
				wgTools.WgDebugWrite(info, new object[0]);
				return;
			}
			wgAppRunInfo.raiseAppRunInfoLoadNums(CommonStr.strSuccessfully);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private int ReportCreate(DateTime dateStart, DateTime dateEnd, string strSql)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.ReportCreate_Acc(dateStart, dateEnd, strSql);
			}
			int num = -1;
			num = 0;
			try
			{
				if (num == 0)
				{
					num = this.comShiftWork.shift_work_schedule_cleardb();
				}
				if (num == 0)
				{
					num = this.comShiftWork.shift_AttReport_cleardb();
				}
				if (num == 0)
				{
					num = this.comShiftWork.shift_AttStatistic_cleardb();
				}
				if (num == 0)
				{
					this.comShiftWork.getAttendenceParam();
				}
				int num2 = 0;
				int num3 = 0;
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						Cursor.Current = Cursors.WaitCursor;
						bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
						while (sqlDataReader.Read())
						{
							if (this.comShiftWork.bStopCreate)
							{
								return num;
							}
							int currentConsumerID = (int)sqlDataReader["f_ConsumerID"];
							num3++;
							this.backgroundWorker1.ReportProgress(num3);
							if (paramValBoolByNO && (byte)sqlDataReader["f_ShiftEnabled"] > 0)
							{
								num = this.ShiftOtherDeal(currentConsumerID, this.comShiftWork, dateStart, dateEnd, ref num2);
								if (num != 0)
								{
									if ((num2 & 1) > 0)
									{
										XMessageBox.Show(string.Concat(new object[]
										{
											sqlDataReader["f_ConsumerName"],
											"\r\n\r\n",
											CommonStr.strNotArrange,
											"\r\n",
											CommonStr.strReArrange
										}));
										break;
									}
									if ((num2 & 2) > 0)
									{
										XMessageBox.Show(string.Concat(new object[]
										{
											sqlDataReader["f_ConsumerName"],
											"\r\n\r\n",
											CommonStr.strNotArrange,
											"\r\n",
											CommonStr.strReArrange
										}));
										break;
									}
									if ((num2 & 4) > 0)
									{
										XMessageBox.Show(string.Concat(new object[]
										{
											sqlDataReader["f_ConsumerName"],
											"\r\n\r\n",
											CommonStr.strArrangeShiftIDNotExisted,
											"\r\n",
											CommonStr.strReArrange
										}));
										break;
									}
									break;
								}
							}
							else
							{
								using (comCreateAttendenceData comCreateAttendenceData = new comCreateAttendenceData())
								{
									comCreateAttendenceData.startDateTime = dateStart;
									comCreateAttendenceData.endDateTime = dateEnd;
									comCreateAttendenceData.strConsumerSql = strSql + "AND f_ConsumerID = " + currentConsumerID.ToString();
									comCreateAttendenceData.make();
								}
							}
						}
						sqlDataReader.Close();
						if (num == 0)
						{
							num = this.comShiftWork.logCreateReport(dateStart, dateEnd, this.groupName, this.totalConsumer.ToString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return num;
		}

		private int ReportCreate_Acc(DateTime dateStart, DateTime dateEnd, string strSql)
		{
			int num = -1;
			num = 0;
			try
			{
				if (num == 0)
				{
					num = this.comShiftWork_Acc.shift_work_schedule_cleardb();
				}
				if (num == 0)
				{
					num = this.comShiftWork_Acc.shift_AttReport_cleardb();
				}
				if (num == 0)
				{
					num = this.comShiftWork_Acc.shift_AttStatistic_cleardb();
				}
				if (num == 0)
				{
					this.comShiftWork_Acc.getAttendenceParam();
				}
				int num2 = 0;
				int num3 = 0;
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						Cursor.Current = Cursors.WaitCursor;
						bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
						while (oleDbDataReader.Read())
						{
							if (this.comShiftWork_Acc.bStopCreate)
							{
								return num;
							}
							int currentConsumerID = (int)oleDbDataReader["f_ConsumerID"];
							num3++;
							this.backgroundWorker1.ReportProgress(num3);
							if (paramValBoolByNO && (byte)oleDbDataReader["f_ShiftEnabled"] > 0)
							{
								num = this.ShiftOtherDeal_Acc(currentConsumerID, this.comShiftWork_Acc, dateStart, dateEnd, ref num2);
								if (num != 0)
								{
									if ((num2 & 1) > 0)
									{
										XMessageBox.Show(string.Concat(new object[]
										{
											oleDbDataReader["f_ConsumerName"],
											"\r\n\r\n",
											CommonStr.strNotArrange,
											"\r\n",
											CommonStr.strReArrange
										}));
										break;
									}
									if ((num2 & 2) > 0)
									{
										XMessageBox.Show(string.Concat(new object[]
										{
											oleDbDataReader["f_ConsumerName"],
											"\r\n\r\n",
											CommonStr.strNotArrange,
											"\r\n",
											CommonStr.strReArrange
										}));
										break;
									}
									if ((num2 & 4) > 0)
									{
										XMessageBox.Show(string.Concat(new object[]
										{
											oleDbDataReader["f_ConsumerName"],
											"\r\n\r\n",
											CommonStr.strArrangeShiftIDNotExisted,
											"\r\n",
											CommonStr.strReArrange
										}));
										break;
									}
									break;
								}
							}
							else
							{
								using (comCreateAttendenceData_Acc comCreateAttendenceData_Acc = new comCreateAttendenceData_Acc())
								{
									comCreateAttendenceData_Acc.startDateTime = dateStart;
									comCreateAttendenceData_Acc.endDateTime = dateEnd;
									comCreateAttendenceData_Acc.strConsumerSql = strSql + "AND f_ConsumerID = " + currentConsumerID.ToString();
									comCreateAttendenceData_Acc.make();
								}
							}
						}
						oleDbDataReader.Close();
						if (num == 0)
						{
							num = this.comShiftWork_Acc.logCreateReport(dateStart, dateEnd, this.groupName, this.totalConsumer.ToString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return num;
		}

		private int ShiftOtherDeal(int currentConsumerID, comShift comShiftWork, DateTime startDate, DateTime endDate, ref int bNotArrange)
		{
			int num = 0;
			int num2 = 1;
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				if (this.dtShiftWorkSchedule == null)
				{
					num = comShiftWork.shift_work_schedule_create(out this.dtShiftWorkSchedule);
				}
				else
				{
					this.dtShiftWorkSchedule.Rows.Clear();
				}
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_fill(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate, ref bNotArrange);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0 && bNotArrange != 0)
			{
				int arg_8A_0 = bNotArrange & 1;
				int arg_92_0 = bNotArrange & 2;
				int arg_9A_0 = bNotArrange & 4;
				num = -1;
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_updatebyReadcard(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_updatebyManualReadcard(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_updatebyLeave(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_analyst(this.dtShiftWorkSchedule);
			}
			wgTools.WriteLine(num2++.ToString());
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				if (this.dtAttReport == null)
				{
					num = comShiftWork.shift_AttReport_Create(out this.dtAttReport);
				}
				else
				{
					this.dtAttReport.Rows.Clear();
				}
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttReport_Fill(this.dtAttReport, this.dtShiftWorkSchedule);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttReport_writetodb(this.dtAttReport);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				if (this.dtAttStatistic == null)
				{
					num = comShiftWork.shift_AttStatistic_Create(out this.dtAttStatistic);
				}
				else
				{
					this.dtAttStatistic.Rows.Clear();
				}
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttStatistic_Fill(this.dtAttStatistic, this.dtAttReport);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttStatistic_writetodb(this.dtAttStatistic);
			}
			wgTools.WriteLine(num2++.ToString());
			wgTools.WriteLine(num2++.ToString());
			return num;
		}

		private int ShiftOtherDeal_Acc(int currentConsumerID, comShift_Acc comShiftWork, DateTime startDate, DateTime endDate, ref int bNotArrange)
		{
			int num = 0;
			int num2 = 1;
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				if (this.dtShiftWorkSchedule == null)
				{
					num = comShiftWork.shift_work_schedule_create(out this.dtShiftWorkSchedule);
				}
				else
				{
					this.dtShiftWorkSchedule.Rows.Clear();
				}
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_fill(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate, ref bNotArrange);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0 && bNotArrange != 0)
			{
				int arg_8A_0 = bNotArrange & 1;
				int arg_92_0 = bNotArrange & 2;
				int arg_9A_0 = bNotArrange & 4;
				num = -1;
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_updatebyReadcard(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_updatebyManualReadcard(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_updatebyLeave(currentConsumerID, this.dtShiftWorkSchedule, startDate, endDate);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_work_schedule_analyst(this.dtShiftWorkSchedule);
			}
			wgTools.WriteLine(num2++.ToString());
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				if (this.dtAttReport == null)
				{
					num = comShiftWork.shift_AttReport_Create(out this.dtAttReport);
				}
				else
				{
					this.dtAttReport.Rows.Clear();
				}
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttReport_Fill(this.dtAttReport, this.dtShiftWorkSchedule);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttReport_writetodb(this.dtAttReport);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				if (this.dtAttStatistic == null)
				{
					num = comShiftWork.shift_AttStatistic_Create(out this.dtAttStatistic);
				}
				else
				{
					this.dtAttStatistic.Rows.Clear();
				}
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttStatistic_Fill(this.dtAttStatistic, this.dtAttReport);
			}
			wgTools.WriteLine(num2++.ToString());
			if (num == 0)
			{
				num = comShiftWork.shift_AttStatistic_writetodb(this.dtAttStatistic);
			}
			wgTools.WriteLine(num2++.ToString());
			wgTools.WriteLine(num2++.ToString());
			return num;
		}

		private int ShiftNornalDeal(int ConsumerID, DateTime dateStart, DateTime dateEnd)
		{
			return -1;
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBar1.Value = e.ProgressPercentage;
			this.lblInfo.Text = e.ProgressPercentage.ToString();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			if (this.comShiftWork != null)
			{
				this.comShiftWork.bStopCreate = true;
			}
			if (this.comShiftWork_Acc != null)
			{
				this.comShiftWork_Acc.bStopCreate = true;
			}
			this.backgroundWorker1.CancelAsync();
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				TimeSpan timeSpan = DateTime.Now.Subtract(this.startTime);
				string text = string.Concat(new object[]
				{
					timeSpan.Hours,
					":",
					timeSpan.Minutes,
					":",
					timeSpan.Seconds
				});
				this.lblRuntime.Text = text;
			}
			catch
			{
			}
		}

		private void dfrmShiftAttReportCreate_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.backgroundWorker1.IsBusy)
			{
				if (this.comShiftWork != null)
				{
					this.comShiftWork.bStopCreate = true;
				}
				if (this.comShiftWork_Acc != null)
				{
					this.comShiftWork_Acc.bStopCreate = true;
				}
				this.backgroundWorker1.CancelAsync();
			}
		}
	}
}
