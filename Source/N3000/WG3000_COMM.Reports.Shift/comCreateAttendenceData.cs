using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class comCreateAttendenceData : Component
	{
		public delegate void CreateCompleteEventHandler(bool bCreated, string strDesc);

		public delegate void DealingNumEventHandler(int num);

		private const string DEFAULT_ALLOWONDUTYTIME = "00:00:00";

		private const string DEFAULT_ALLOWONDUTYTIME2 = "00:00";

		private const string DEFAULT_ALLOWOFFDUTYTIME = "23:59:59";

		private const int REST_ONE_DAY = 0;

		private const int REST_AM = 2;

		private const int REST_PM = 1;

		private const int WORK_ONE_DAY = 3;

		private const int WORK_AM = 1;

		private const int WORK_PM = 2;

		private DataSet dsAtt;

		private comCreateAttendenceData.CreateCompleteEventHandler CreateCompleteEvent;

		private comCreateAttendenceData.DealingNumEventHandler DealingNumEvent;

		public DateTime startDateTime;

		public DateTime endDateTime;

		public string strConsumerSql;

		public string groupName;

		public string consumerName;

		public int userId;

		public bool bStopCreate;

		public string strAllowOndutyTime = "00:00:00";

		private string strAllowOffdutyTime = "23:59:59";

		public bool bEarliestAsOnDuty;

		public bool bChooseTwoTimes;

		public decimal needDutyHour = 8.0m;

		public bool bChooseOnlyOnDuty;

		private Container components;

		private Thread mainThread;

		private string strTemp = "";

		private int gProcVal;

		private int normalDay;

		private SqlCommand cmdConsumer;

		private SqlConnection cnConsumer;

		private SqlConnection cn;

		private DataTable dtCardRecord;

		private DataTable dtCardRecord1;

		private DataTable dtCardRecord2;

		private DataTable dtAttendenceData;

		private DataTable dtHoliday;

		private DataTable dtHolidayType;

		private DataTable dtLeave;

		private DataView dvCardRecord;

		private DataView dvHoliday;

		private DataView dvLeave;

		private SqlDataAdapter daAttendenceData;

		private SqlDataAdapter daHoliday;

		private SqlDataAdapter daHolidayType;

		private SqlDataAdapter daLeave;

		private SqlDataAdapter daNoCardRecord;

		private SqlDataAdapter daManualCardRecord;

		private SqlDataAdapter daCardRecord;

		private int tLateTimeout;

		private int tlateAbsenceTimeout;

		private decimal tLateAbsenceDay;

		private int tLeaveTimeout;

		private int tLeaveAbsenceTimeout;

		private decimal tLeaveAbsenceDay;

		private int tOvertimeTimeout;

		private DateTime tOnduty0;

		private DateTime tOffduty0;

		private DateTime tOnduty1;

		private DateTime tOffduty1;

		private DateTime tOnduty2;

		private DateTime tOffduty2;

		private int tReadCardTimes = 2;

		private int tTwoReadMintime = 60;

		private SqlCommand cmd;

		public event comCreateAttendenceData.CreateCompleteEventHandler CreateComplete
		{
			add
			{
				this.CreateCompleteEvent = (comCreateAttendenceData.CreateCompleteEventHandler)Delegate.Combine(this.CreateCompleteEvent, value);
			}
			remove
			{
				this.CreateCompleteEvent = (comCreateAttendenceData.CreateCompleteEventHandler)Delegate.Remove(this.CreateCompleteEvent, value);
			}
		}

		public event comCreateAttendenceData.DealingNumEventHandler DealingNum
		{
			add
			{
				this.DealingNumEvent = (comCreateAttendenceData.DealingNumEventHandler)Delegate.Combine(this.DealingNumEvent, value);
			}
			remove
			{
				this.DealingNumEvent = (comCreateAttendenceData.DealingNumEventHandler)Delegate.Remove(this.DealingNumEvent, value);
			}
		}

		public string getDecimalStr(object obj)
		{
			string result = "";
			try
			{
				result = ((decimal)obj).ToString("0.0", CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public comCreateAttendenceData(IContainer Container) : this()
		{
			Container.Add(this);
		}

		public comCreateAttendenceData()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		public void startCreate()
		{
			this.mainThread = new Thread(new ThreadStart(this.make));
			this.mainThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			this.mainThread.Start();
		}

		private bool InformationIsDate(string str)
		{
			DateTime dateTime;
			return DateTime.TryParse(str, out dateTime);
		}

		private string SetObjToStr(object obj)
		{
			return wgTools.SetObjToStr(obj);
		}

		private string PrepareStr(object obj)
		{
			return wgTools.PrepareStr(obj);
		}

		private string PrepareStr(object obj, bool bDate, string dateFormat)
		{
			return wgTools.PrepareStr(obj, bDate, dateFormat);
		}

		public void localizedHolidayType(DataTable dt)
		{
			try
			{
				for (int i = 0; i <= dt.Rows.Count - 1; i++)
				{
					if (dt.Rows[i]["f_HolidayType"].ToString() == "出差" || dt.Rows[i]["f_HolidayType"].ToString() == "出差" || dt.Rows[i]["f_HolidayType"].ToString() == "Business Trip")
					{
						dt.Rows[i]["f_HolidayType"] = CommonStr.strBusinessTrip;
					}
					if (dt.Rows[i]["f_HolidayType"].ToString() == "病假" || dt.Rows[i]["f_HolidayType"].ToString() == "病假" || dt.Rows[i]["f_HolidayType"].ToString() == "Sick Leave")
					{
						dt.Rows[i]["f_HolidayType"] = CommonStr.strSickLeave;
					}
					if (dt.Rows[i]["f_HolidayType"].ToString() == "事假" || dt.Rows[i]["f_HolidayType"].ToString() == "事假" || dt.Rows[i]["f_HolidayType"].ToString() == "Private Leave")
					{
						dt.Rows[i]["f_HolidayType"] = CommonStr.strPrivateLeave;
					}
				}
				dt.AcceptChanges();
			}
			catch (Exception)
			{
			}
		}

		public void localizedHoliday(DataTable dt)
		{
			try
			{
				for (int i = 0; i <= dt.Rows.Count - 1; i++)
				{
					if (string.Compare("strHoliday_" + dt.Rows[i]["f_EName"], "strHoliday_Saturday") == 0)
					{
						dt.Rows[i]["f_Name"] = CommonStr.strHoliday_Saturday;
					}
					else if (string.Compare("strHoliday_" + dt.Rows[i]["f_EName"], "strHoliday_Sunday") == 0)
					{
						dt.Rows[i]["f_Name"] = CommonStr.strHoliday_Sunday;
					}
					else if (string.Compare("strHoliday_" + dt.Rows[i]["f_EName"], "strHoliday_AM") == 0)
					{
						dt.Rows[i]["f_Name"] = CommonStr.strHoliday_AM;
					}
					else if (string.Compare("strHoliday_" + dt.Rows[i]["f_EName"], "strHoliday_PM") == 0)
					{
						dt.Rows[i]["f_Name"] = CommonStr.strHoliday_PM;
					}
					if (!Information.IsDBNull(dt.Rows[i]["f_Value1"]) && (dt.Rows[i]["f_Value1"].ToString() == "A.M." || dt.Rows[i]["f_Value1"].ToString() == "上午" || dt.Rows[i]["f_Value1"].ToString() == "上午"))
					{
						dt.Rows[i]["f_Value1"] = CommonStr.strHoliday_AM;
					}
					if (!Information.IsDBNull(dt.Rows[i]["f_Value3"]) && (dt.Rows[i]["f_Value3"].ToString() == "A.M." || dt.Rows[i]["f_Value3"].ToString() == "上午" || dt.Rows[i]["f_Value3"].ToString() == "上午"))
					{
						dt.Rows[i]["f_Value3"] = CommonStr.strHoliday_AM;
					}
					if (!Information.IsDBNull(dt.Rows[i]["f_Value1"]) && (dt.Rows[i]["f_Value1"].ToString() == "P.M." || dt.Rows[i]["f_Value1"].ToString() == "下午" || dt.Rows[i]["f_Value1"].ToString() == "下午"))
					{
						dt.Rows[i]["f_Value1"] = CommonStr.strHoliday_PM;
					}
					if (!Information.IsDBNull(dt.Rows[i]["f_Value3"]) && (dt.Rows[i]["f_Value3"].ToString() == "P.M." || dt.Rows[i]["f_Value3"].ToString() == "下午" || dt.Rows[i]["f_Value3"].ToString() == "下午"))
					{
						dt.Rows[i]["f_Value3"] = CommonStr.strHoliday_PM;
					}
				}
				dt.AcceptChanges();
			}
			catch (Exception)
			{
			}
		}

		public void make()
		{
			this.getAttendenceParam();
			if (!("00:00:00" == this.strAllowOndutyTime) && !("00:00" == this.strAllowOndutyTime))
			{
				if (this.InformationIsDate("2000-1-1 " + this.strAllowOndutyTime))
				{
					this.strAllowOndutyTime = Strings.Format(DateTime.Parse("2000-1-1 " + this.strAllowOndutyTime), "H:mm:ss");
					this.normalDay = 1;
					DateTime dateTime = Convert.ToDateTime(Strings.Format(DateTime.Now, "yyyy-MM-dd") + " " + this.strAllowOndutyTime).AddMilliseconds(-1.0);
					this.strAllowOffdutyTime = Strings.Format(dateTime, "H:mm:ss");
				}
				else
				{
					this.strAllowOndutyTime = "00:00:00";
				}
			}
			if (this.tReadCardTimes != 4)
			{
				if (this.bChooseOnlyOnDuty)
				{
					this.make4OneTime();
					return;
				}
				this.make4TwoTimes();
				return;
			}
			else
			{
				if (this.bChooseOnlyOnDuty)
				{
					this.make4FourTimesOnlyDuty();
					return;
				}
				this.make4FourTimes();
				return;
			}
		}

		private void make4TwoTimes()
		{
			this.cnConsumer = new SqlConnection(wgAppConfig.dbConString);
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dtCardRecord = new DataTable();
			this.dsAtt = new DataSet("Attendance");
			this.dsAtt.Clear();
			this.daAttendenceData = new SqlDataAdapter("SELECT * FROM t_d_AttendenceData WHERE 1<0", this.cn);
			this.daHoliday = new SqlDataAdapter("SELECT * FROM t_a_Holiday ORDER BY  f_NO ASC", this.cn);
			this.daHolidayType = new SqlDataAdapter("SELECT * FROM t_a_HolidayType", this.cn);
			this.daLeave = new SqlDataAdapter("SELECT * FROM t_d_Leave", this.cn);
			this.daNoCardRecord = new SqlDataAdapter("SELECT f_ReadDate,f_Character,'' as f_Type  FROM t_d_ManualCardRecord Where 1<0 ", this.cn);
			this.daNoCardRecord.Fill(this.dsAtt, "AllCardRecords");
			this.dtCardRecord = this.dsAtt.Tables["AllCardRecords"];
			this.dtCardRecord.Clear();
			this.daAttendenceData.Fill(this.dsAtt, "AttendenceData");
			this.dtAttendenceData = this.dsAtt.Tables["AttendenceData"];
			this.getAttendenceParam();
			this._clearAttendenceData();
			this._clearAttStatistic();
			this.daHoliday.Fill(this.dsAtt, "Holiday");
			this.dtHoliday = this.dsAtt.Tables["Holiday"];
			this.localizedHoliday(this.dtHoliday);
			this.dvHoliday = new DataView(this.dtHoliday);
			this.dvHoliday.RowFilter = "";
			this.dvHoliday.Sort = " f_NO ASC ";
			this.daLeave.Fill(this.dsAtt, "Leave");
			this.dtLeave = this.dsAtt.Tables["Leave"];
			this.dvLeave = new DataView(this.dtLeave);
			this.dvLeave.RowFilter = "";
			this.dvLeave.Sort = " f_NO ASC ";
			this.daHolidayType.Fill(this.dsAtt, "HolidayType");
			this.dtHolidayType = this.dsAtt.Tables["HolidayType"];
			this.localizedHolidayType(this.dtHolidayType);
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 AND f_ShiftEnabled =0) ", this.cnConsumer);
			}
			else
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 ) ", this.cnConsumer);
			}
			this.cnConsumer.Open();
			SqlDataReader sqlDataReader = this.cmdConsumer.ExecuteReader();
			int num = 0;
			try
			{
				int num2 = 0;
				while (sqlDataReader.Read())
				{
					num = (int)sqlDataReader["f_ConsumerID"];
					num2++;
					string text = "SELECT f_ReadDate,f_Character,'' as f_Type  ";
					text += " FROM t_d_SwipeRecord INNER JOIN t_b_Reader ON t_b_Reader.f_Attend=1 AND t_d_SwipeRecord.f_ReaderID =t_b_Reader.f_ReaderID ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " AND t_b_Reader.f_Attend = 1 ";
					if (wgAppConfig.getSystemParamByNO(54) == "1")
					{
						text += " AND f_Character >= 1 ";
					}
					text += " ORDER BY f_ReadDate ASC ";
					this.daCardRecord = new SqlDataAdapter(text, this.cn);
					text = "SELECT f_ReadDate,f_Character ";
					text += string.Format(", {0} as f_Type", wgTools.PrepareStr(CommonStr.strSignIn));
					text += " FROM t_d_ManualCardRecord  ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " ORDER BY f_ReadDate ASC ";
					this.daManualCardRecord = new SqlDataAdapter(text, this.cn);
					decimal[] array = new decimal[32];
					DataRow dataRow = null;
					if (this.DealingNumEvent != null)
					{
						this.DealingNumEvent(num2);
					}
					this.gProcVal = num2 + 1;
					if (this.bStopCreate)
					{
						return;
					}
					DateTime dateTime = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					DateTime dateTime2 = DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime));
					DateTime dateTime3 = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					decimal num7 = 0m;
					decimal num8 = 0m;
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					for (int i = 0; i <= array.Length - 1; i++)
					{
						array[i] = 0m;
					}
					this.dtCardRecord = this.dsAtt.Tables["AllCardRecords"];
					this.dsAtt.Tables["AllCardRecords"].Clear();
					this.daCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.daManualCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.dvCardRecord = new DataView(this.dtCardRecord);
					this.dvCardRecord.RowFilter = "";
					this.dvCardRecord.Sort = " f_ReadDate ASC ";
					int j = 0;
					while (this.dvCardRecord.Count > j + 1)
					{
						if (((DateTime)this.dvCardRecord[j + 1][0]).Subtract((DateTime)this.dvCardRecord[j][0]).TotalSeconds < (double)this.tTwoReadMintime)
						{
							this.dvCardRecord[j + 1].Delete();
						}
						else
						{
							j++;
						}
					}
					while (dateTime3 <= DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime)))
					{
						dataRow = this.dtAttendenceData.NewRow();
						dataRow["f_ConsumerID"] = num;
						dataRow["f_AttDate"] = dateTime3;
						dataRow["f_LateTime"] = 0;
						dataRow["f_LeaveEarlyTime"] = 0;
						dataRow["f_OvertimeTime"] = 0;
						dataRow["f_AbsenceDay"] = 0;
						bool flag = true;
						bool flag2 = true;
						bool flag3 = false;
						bool flag4 = false;
						this.dvCardRecord.RowFilter = "  f_ReadDate >= #" + dateTime3.ToString("yyyy-MM-dd HH:mm:ss") + "# and f_ReadDate<= " + Strings.Format(dateTime3.AddDays((double)this.normalDay), "#yyyy-MM-dd " + this.strAllowOffdutyTime + "#");
						this.dvLeave.RowFilter = " f_ConsumerID = " + num.ToString();
						if (this.dvCardRecord.Count > 0)
						{
							int k = 0;
							while (k <= this.dvCardRecord.Count - 1)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (string.Compare(Strings.Format(dateTime4, "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty0, "HH:mm:ss")) <= 0)
									{
										if (this.bEarliestAsOnDuty || this.bChooseTwoTimes)
										{
											if (this.SetObjToStr(dataRow["f_Onduty1"]) == "")
											{
												dataRow["f_Onduty1"] = dateTime4;
												dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
										}
										else
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
										}
										k++;
										continue;
									}
									if (!(this.SetObjToStr(dataRow["f_Onduty1"]) != ""))
									{
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty0.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
										}
										else if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty0.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
										}
										else if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty0.AddMinutes((double)(-(double)this.tLeaveTimeout)), "HH:mm:ss")) > 0)
										{
											dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
										}
										else
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
										}
									}
								}
								else if (!(this.SetObjToStr(dataRow["f_Onduty1"]) != ""))
								{
									dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
								}
								IL_C31:
								while (k <= this.dvCardRecord.Count - 1)
								{
									dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
									if (string.Compare(Strings.Format(dateTime4, "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
									{
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty0, "HH:mm:ss")) <= 0)
										{
											dataRow["f_Offduty1"] = dateTime4;
											dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											if (string.Compare(Strings.Format(dateTime4.AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty0, "HH:mm:ss")) < 0)
											{
												if (string.Compare(Strings.Format(dateTime4.AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty0, "HH:mm:ss")) < 0)
												{
													dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
												}
												else
												{
													dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
												}
											}
										}
										else
										{
											dataRow["f_Offduty1"] = dateTime4;
											dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
										}
									}
									else
									{
										dataRow["f_Offduty1"] = dateTime4;
										dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
									}
									k++;
								}
								if (this.SetObjToStr(dataRow["f_Offduty1"]) == this.SetObjToStr(dataRow["f_Onduty1"]) && (this.SetObjToStr(dataRow["f_Offduty1Desc"]).IndexOf(CommonStr.strLeaveEarly) >= 0 || this.SetObjToStr(dataRow["f_Offduty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0))
								{
									dataRow["f_Offduty1"] = DBNull.Value;
									dataRow["f_Offduty1Desc"] = CommonStr.strNotReadCard;
								}
								if (this.SetObjToStr(dataRow["f_Offduty1"]) == "" && this.SetObjToStr(dataRow["f_Onduty1"]) == "")
								{
									dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
									dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
								}
								if (this.SetObjToStr(dataRow["f_Offduty1"]) == "" && this.SetObjToStr(dataRow["f_Offduty1Desc"]) == "")
								{
									dataRow["f_Offduty1Desc"] = CommonStr.strNotReadCard;
								}
								if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty1Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
								{
									dataRow["f_OnDuty1Desc"] = "";
									goto IL_DEE;
								}
								goto IL_DEE;
							}
							goto IL_C31;
						}
						dataRow["f_OnDuty1Desc"] = CommonStr.strAbsence;
						dataRow["f_OffDuty1Desc"] = CommonStr.strAbsence;
						IL_DEE:
						int num12 = 3;
						this.dvHoliday.RowFilter = " f_NO =1 ";
						if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
						{
							num12 = 0;
						}
						else
						{
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num12 = 1;
							}
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num12 = 2;
							}
							this.dvHoliday.RowFilter = " f_NO =2 ";
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
							{
								num12 = 0;
							}
							else
							{
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num12 = 1;
								}
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num12 = 2;
								}
								this.dvHoliday.RowFilter = " f_TYPE =2 ";
								for (int l = 0; l <= this.dvHoliday.Count - 1; l++)
								{
									this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
									DateTime t;
									DateTime.TryParse(this.strTemp, out t);
									this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value2"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
									DateTime t2;
									DateTime.TryParse(this.strTemp, out t2);
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num12 = 0;
										break;
									}
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t2)
									{
										num12 = 2;
									}
									if (t <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num12 = 1;
									}
								}
							}
						}
						if (num12 != 3)
						{
							this.dvHoliday.RowFilter = " f_TYPE =3 ";
							for (int m = 0; m <= this.dvHoliday.Count - 1; m++)
							{
								this.strTemp = Convert.ToString(this.dvHoliday[m]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[m]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime t3;
								DateTime.TryParse(this.strTemp, out t3);
								this.strTemp = Convert.ToString(this.dvHoliday[m]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[m]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime t4;
								DateTime.TryParse(this.strTemp, out t4);
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									num12 = 3;
									break;
								}
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t4)
								{
									if (num12 == 2)
									{
										num12 = 3;
									}
									else
									{
										num12 = 1;
									}
								}
								if (t3 <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									if (num12 == 1)
									{
										num12 = 3;
									}
									else
									{
										num12 = 2;
									}
								}
							}
						}
						if (num12 == 0)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
							dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strOvertime;
								dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
								if (string.Compare(Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
								{
									dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-1 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
								else
								{
									dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							flag = false;
							flag2 = false;
						}
						else if (num12 == 1)
						{
							if (this.SetObjToStr(dataRow["f_Offduty1"]) != "")
							{
								if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Offduty1"]).AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), "12:00:00") < 0)
								{
									if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Offduty1"]).AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), "12:00:00") < 0)
									{
										flag4 = true;
										dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
									}
									else
									{
										flag4 = true;
										dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
									}
								}
								else
								{
									dataRow["f_Offduty1Desc"] = "";
								}
							}
							if (this.SetObjToStr(dataRow["f_Offduty1"]) != "")
							{
								if (Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Offduty1"]).AddMinutes((double)(-(double)this.tOvertimeTimeout)), "HH:mm:ss"), "12:00:00") >= 0)
									{
										dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
										dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("2000-1-1 12:00:00"), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-1 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
								}
								else
								{
									dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
									dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty0, "2000-1-1 12:00:00")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
						}
						else if (num12 == 2)
						{
							if (this.SetObjToStr(dataRow["f_Onduty1"]) != "")
							{
								if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Onduty1"]).AddMinutes((double)(-(double)this.tlateAbsenceTimeout)), "HH:mm:ss"), "13:30:00") > 0)
								{
									flag3 = true;
									dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
								}
								else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Onduty1"]).AddMinutes((double)(-(double)this.tLateTimeout)), "HH:mm:ss"), "13:30:00") > 0)
								{
									flag3 = true;
									dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
								}
								else
								{
									dataRow["f_Onduty1Desc"] = "";
								}
								if (this.SetObjToStr(dataRow["f_Offduty1"]) != "")
								{
									if (string.Compare(Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
									{
										if (string.Compare(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss"), Strings.Format(this.tOffduty0.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss")) >= 0 && string.Compare(Strings.Format(this.tOffduty0.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty0, "HH:mm:ss")) >= 0)
										{
											dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
											dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty0, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
										}
									}
									else
									{
										dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
										dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty0, "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
								}
							}
						}
						else if (num12 == 3 && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
						{
							if (string.Compare(Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
							{
								if (string.Compare(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss"), Strings.Format(this.tOffduty0.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss")) >= 0 && string.Compare(Strings.Format(this.tOffduty0.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty0, "HH:mm:ss")) >= 0)
								{
									dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
									dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty0, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
								dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty0, "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
							}
						}
						if (this.dvLeave.Count > 0)
						{
							DateTime now = DateTime.Now;
							DateTime now2 = DateTime.Now;
							string value = "";
							string value2 = "";
							num12 = 3;
							for (int n = 0; n <= this.dvLeave.Count - 1; n++)
							{
								this.strTemp = Convert.ToString(this.dvLeave[n]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[n]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime.TryParse(this.strTemp, out now);
								this.strTemp = Convert.ToString(this.dvLeave[n]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[n]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime.TryParse(this.strTemp, out now2);
								string text2 = Convert.ToString(this.dvLeave[n]["f_HolidayType"]);
								if (now <= dateTime3 && Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= now2)
								{
									value = text2;
									value2 = text2;
									num12 = 0;
									break;
								}
								if (now <= dateTime3 && Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= now2)
								{
									value = text2;
									if (num12 == 1)
									{
										num12 = 0;
										break;
									}
									num12 = 2;
								}
								if (now <= Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= now2)
								{
									value2 = text2;
									if (num12 == 2)
									{
										num12 = 0;
										break;
									}
									num12 = 1;
								}
							}
							if (num12 == 0)
							{
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OffDuty1Desc"] = value2;
								dataRow["f_OnDuty1"] = DBNull.Value;
								dataRow["f_OffDuty1"] = DBNull.Value;
							}
							else if (num12 == 1)
							{
								dataRow["f_OffDuty1Desc"] = value2;
								dataRow["f_OffDuty1"] = DBNull.Value;
							}
							else if (num12 == 2)
							{
								if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard)
								{
									if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "")
									{
										dataRow["f_OnDuty2"] = dataRow["f_OnDuty1"];
										dataRow["f_OnDuty1"] = DBNull.Value;
										dataRow["f_OffDuty1"] = DBNull.Value;
									}
									else if (this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
									{
										dataRow["f_OnDuty2"] = dataRow["f_OffDuty1"];
										dataRow["f_OffDuty1"] = DBNull.Value;
									}
								}
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OnDuty1"] = DBNull.Value;
							}
						}
						if (this.bChooseTwoTimes)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strLateness)
							{
								dataRow["f_OnDuty1Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly)
							{
								dataRow["f_OffDuty1Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strOvertime)
							{
								dataRow["f_OnDuty1Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strOvertime)
							{
								dataRow["f_OffDuty1Desc"] = "";
							}
							dataRow["f_OvertimeTime"] = 0;
							if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "" && this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
							{
								dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "yyyy-MM-dd HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
							}
							flag2 = ((decimal)dataRow["f_OvertimeTime"] >= this.needDutyHour);
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strLateness)
						{
							if (flag3)
							{
								dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty0, "13:30:00")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
							}
							else
							{
								dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty0, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
							}
						}
						if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly)
						{
							if (flag4)
							{
								dataRow["f_LeaveEarlyTime"] = (long)Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OffDuty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(this.tOffduty0, "12:00:00")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
							}
							else
							{
								dataRow["f_LeaveEarlyTime"] = (long)Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OffDuty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(this.tOffduty0, "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0)
						{
							dataRow["f_AbsenceDay"] = this.tLateAbsenceDay + this.tLeaveAbsenceDay;
							flag2 = false;
						}
						else
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
								flag2 = false;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLeaveAbsenceDay;
								flag2 = false;
							}
						}
						if (this.SetObjToStr(dataRow["f_OffDuty1"]) == "")
						{
							dataRow["f_OvertimeTime"] = 0;
						}
						if (Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) >= 1m && num12 != 3)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) / 2.0m;
						}
						text = " INSERT INTO t_d_AttendenceData ";
						text += " ([f_ConsumerID], [f_AttDate], ";
						text += "[f_Onduty1],[f_Onduty1Desc], [f_Offduty1], [f_Offduty1Desc]";
						text += ", [f_LateTime], [f_LeaveEarlyTime],[f_OvertimeTime], [f_AbsenceDay]  ";
						text += " ) ";
						text = text + " VALUES ( " + dataRow["f_ConsumerID"];
						text = text + " , " + this.PrepareStr(dataRow["f_AttDate"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1Desc"]);
						text = text + " , " + dataRow["f_LateTime"];
						text = text + " , " + dataRow["f_LeaveEarlyTime"];
						text = text + " , " + this.getDecimalStr(dataRow["f_OvertimeTime"]);
						text = text + " , " + this.getDecimalStr(dataRow["f_AbsenceDay"]);
						text += " ) ";
						using (SqlCommand sqlCommand = new SqlCommand(text, this.cn))
						{
							if (this.cn.State == ConnectionState.Closed)
							{
								this.cn.Open();
							}
							sqlCommand.ExecuteNonQuery();
						}
						if (flag)
						{
							num3++;
						}
						string a = "";
						for (j = 0; j <= 1; j++)
						{
							if (j == 0)
							{
								a = this.SetObjToStr(dataRow["f_OnDuty1Desc"]);
							}
							else if (j == 1)
							{
								a = this.SetObjToStr(dataRow["f_OffDuty1Desc"]);
							}
							if (a == CommonStr.strLateness)
							{
								num5++;
								flag2 = false;
							}
							else if (a == CommonStr.strLeaveEarly)
							{
								num6++;
								flag2 = false;
							}
							else if (a == CommonStr.strNotReadCard)
							{
								num9++;
								flag2 = false;
							}
							else
							{
								int i = 0;
								while (i <= this.dtHolidayType.Rows.Count - 1 && i < array.Length)
								{
									if (a == Convert.ToString(this.dtHolidayType.Rows[i][1]))
									{
										flag2 = false;
										array[i] += 0.5m;
										break;
									}
									i++;
								}
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "" && this.SetObjToStr(dataRow["f_OffDuty1"]) == "")
						{
							flag2 = false;
						}
						num7 += Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture);
						num8 += Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture);
						num10 += Convert.ToInt32(dataRow["f_LateTime"]);
						num11 += Convert.ToInt32(dataRow["f_LeaveEarlyTime"]);
						if (Convert.ToInt32(dataRow["f_LateTime"]) != 0 || Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) != 0 || !(Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) == 0m))
						{
							flag2 = false;
						}
						if (flag2)
						{
							num4++;
						}
						dateTime3 = dateTime3.AddDays(1.0);
						Application.DoEvents();
					}
					this.dvCardRecord.RowFilter = string.Format("f_Type ={0}", this.PrepareStr(CommonStr.strSignIn));
					text = " Insert Into t_d_AttStatistic ";
					text += " ( [f_ConsumerID], [f_AttDateStart], [f_AttDateEnd] ";
					text += " , [f_DayShouldWork],  [f_DayRealWork]";
					text += " , [f_TotalLate],  [f_TotalLeaveEarly],[f_TotalOvertime], [f_TotalAbsenceDay], [f_TotalNotReadCard]";
					for (int i = 1; i <= 32; i++)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" , [f_SpecialType",
							i,
							"]"
						});
					}
					text += ", f_LateMinutes";
					text += ", f_LeaveEarlyMinutes";
					text += ", f_ManualReadTimesCount";
					text += " ) ";
					text = text + " Values( " + dataRow["f_ConsumerID"];
					text = text + " , " + this.PrepareStr(dateTime, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(dateTime2, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + num3;
					text = text + " , " + num4;
					text = text + " , " + num5;
					text = text + " , " + num6;
					text = text + " , " + this.getDecimalStr(num8);
					text = text + " , " + this.getDecimalStr(num7);
					text = text + " , " + num9;
					for (int i = 0; i <= 31; i++)
					{
						text = text + " , " + this.PrepareStr(this.getDecimalStr(array[i]));
					}
					text = text + ", " + num10;
					text = text + ", " + num11;
					text = text + ", " + this.dvCardRecord.Count;
					text += " )";
					using (SqlCommand sqlCommand2 = new SqlCommand(text, this.cn))
					{
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						sqlCommand2.ExecuteNonQuery();
					}
				}
				sqlDataReader.Close();
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				this.shiftAttReportImportFromAttendenceData();
				this.shiftAttStatisticImportFromAttStatistic();
				this.logCreateReport();
				if (this.CreateCompleteEvent != null)
				{
					this.CreateCompleteEvent(true, "");
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
				try
				{
					if (this.CreateCompleteEvent != null)
					{
						this.CreateCompleteEvent(false, ex.ToString());
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void make4FourTimes()
		{
			this.cnConsumer = new SqlConnection(wgAppConfig.dbConString);
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dtCardRecord1 = new DataTable();
			this.dsAtt = new DataSet("Attendance");
			this.dsAtt.Clear();
			this.daAttendenceData = new SqlDataAdapter("SELECT * FROM t_d_AttendenceData WHERE 1<0", this.cn);
			this.daHoliday = new SqlDataAdapter("SELECT * FROM t_a_Holiday ORDER BY  f_NO ASC", this.cn);
			this.daHolidayType = new SqlDataAdapter("SELECT * FROM t_a_HolidayType", this.cn);
			this.daLeave = new SqlDataAdapter("SELECT * FROM t_d_Leave", this.cn);
			this.daNoCardRecord = new SqlDataAdapter("SELECT f_ReadDate,f_Character,'' as f_Type  FROM t_d_ManualCardRecord Where 1<0 ", this.cn);
			this.daNoCardRecord.Fill(this.dsAtt, "AllCardRecords");
			this.dtCardRecord1 = this.dsAtt.Tables["AllCardRecords"];
			this.dtCardRecord1.Clear();
			this.daAttendenceData.Fill(this.dsAtt, "AttendenceData");
			this.dtAttendenceData = this.dsAtt.Tables["AttendenceData"];
			this.getAttendenceParam();
			this._clearAttendenceData();
			this._clearAttStatistic();
			this.daHoliday.Fill(this.dsAtt, "Holiday");
			this.dtHoliday = this.dsAtt.Tables["Holiday"];
			this.localizedHoliday(this.dtHoliday);
			this.dvHoliday = new DataView(this.dtHoliday);
			this.dvHoliday.RowFilter = "";
			this.dvHoliday.Sort = " f_NO ASC ";
			this.daLeave.Fill(this.dsAtt, "Leave");
			this.dtLeave = this.dsAtt.Tables["Leave"];
			this.dvLeave = new DataView(this.dtLeave);
			this.dvLeave.RowFilter = "";
			this.dvLeave.Sort = " f_NO ASC ";
			this.daHolidayType.Fill(this.dsAtt, "HolidayType");
			this.dtHolidayType = this.dsAtt.Tables["HolidayType"];
			this.localizedHolidayType(this.dtHolidayType);
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 AND f_ShiftEnabled =0) ", this.cnConsumer);
			}
			else
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 ) ", this.cnConsumer);
			}
			this.cnConsumer.Open();
			SqlDataReader sqlDataReader = this.cmdConsumer.ExecuteReader();
			int num = 0;
			try
			{
				int num2 = 0;
				while (sqlDataReader.Read())
				{
					num = (int)sqlDataReader["f_ConsumerID"];
					num2++;
					string text = "SELECT f_ReadDate,f_Character,'' as f_Type  ";
					text += " FROM t_d_SwipeRecord INNER JOIN t_b_Reader ON ( t_b_Reader.f_Attend=1 AND t_d_SwipeRecord.f_ReaderID =t_b_Reader.f_ReaderID ) ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " AND t_b_Reader.f_Attend = 1 ";
					if (wgAppConfig.getSystemParamByNO(54) == "1")
					{
						text += " AND f_Character >= 1 ";
					}
					text += " ORDER BY f_ReadDate ASC ";
					this.daCardRecord = new SqlDataAdapter(text, this.cn);
					text = "SELECT f_ReadDate,f_Character ";
					text += string.Format(",{0} as f_Type", this.PrepareStr(CommonStr.strSignIn));
					text += " FROM t_d_ManualCardRecord  ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " ORDER BY f_ReadDate ASC ";
					this.daManualCardRecord = new SqlDataAdapter(text, this.cn);
					decimal[] array = new decimal[32];
					DataRow dataRow = null;
					if (this.DealingNumEvent != null)
					{
						this.DealingNumEvent(num2);
					}
					this.gProcVal = num2 + 1;
					if (this.bStopCreate)
					{
						return;
					}
					DateTime dateTime = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					DateTime dateTime2 = DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime));
					DateTime dateTime3 = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					decimal num7 = 0m;
					decimal num8 = 0m;
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					for (int i = 0; i <= array.Length - 1; i++)
					{
						array[i] = 0m;
					}
					this.dtCardRecord1 = this.dsAtt.Tables["AllCardRecords"];
					this.dsAtt.Tables["AllCardRecords"].Clear();
					this.daCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.daManualCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.dvCardRecord = new DataView(this.dtCardRecord1);
					this.dvCardRecord.RowFilter = "";
					this.dvCardRecord.Sort = " f_ReadDate ASC ";
					int j = 0;
					while (this.dvCardRecord.Count > j + 1)
					{
						if (((DateTime)this.dvCardRecord[j + 1][0]).Subtract((DateTime)this.dvCardRecord[j][0]).TotalSeconds < (double)this.tTwoReadMintime)
						{
							this.dvCardRecord[j + 1].Delete();
						}
						else
						{
							j++;
						}
					}
					while (dateTime3 <= DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime)))
					{
						dataRow = this.dtAttendenceData.NewRow();
						dataRow["f_ConsumerID"] = num;
						dataRow["f_AttDate"] = dateTime3;
						dataRow["f_LateTime"] = 0;
						dataRow["f_LeaveEarlyTime"] = 0;
						dataRow["f_OvertimeTime"] = 0;
						dataRow["f_AbsenceDay"] = 0;
						bool flag = true;
						bool flag2 = true;
						this.dvCardRecord.RowFilter = " f_ReadDate >= #" + dateTime3.ToString("yyyy-MM-dd HH:mm:ss") + "# and f_ReadDate<= " + Strings.Format(dateTime3.AddDays((double)this.normalDay), "#yyyy-MM-dd " + this.strAllowOffdutyTime + "#");
						this.dvLeave.RowFilter = " f_ConsumerID = " + num.ToString();
						if (this.dvCardRecord.Count > 0)
						{
							int k = 0;
							while (k <= this.dvCardRecord.Count - 1)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (string.Compare(Strings.Format(dateTime4, "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty1, "HH:mm:ss")) <= 0)
									{
										if (this.bEarliestAsOnDuty)
										{
											if (this.SetObjToStr(dataRow["f_Onduty1"]) == "")
											{
												dataRow["f_Onduty1"] = dateTime4;
												dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
										}
										else
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
										}
										if (this.dvCardRecord.Count == 4)
										{
											k++;
											break;
										}
										k++;
									}
									else
									{
										if (this.SetObjToStr(dataRow["f_Onduty1"]) != "")
										{
											break;
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty1.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											k++;
											break;
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty1.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
											k++;
											break;
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty1.AddMinutes((double)(-(double)this.tLeaveTimeout)), "HH:mm:ss")) > 0)
										{
											dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
										}
										else
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
										}
										if (this.dvCardRecord.Count == 4)
										{
											k++;
											break;
										}
										break;
									}
								}
								else
								{
									if (!(this.SetObjToStr(dataRow["f_Onduty1"]) != ""))
									{
										dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
										break;
									}
									break;
								}
							}
							int num12 = k;
							k = num12;
							while (k <= this.dvCardRecord.Count - 1)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (string.Compare(Strings.Format(dateTime4, "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) <= 0 || this.dvCardRecord.Count == 4)
									{
										dataRow["f_Offduty1"] = dateTime4;
										dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
										if (string.Compare(Strings.Format(dateTime4.AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
										{
											if (string.Compare(Strings.Format(dateTime4.AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
											}
											else
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
											}
										}
										if (this.dvCardRecord.Count == 4)
										{
											k++;
											break;
										}
										k++;
									}
									else
									{
										if (this.SetObjToStr(dataRow["f_Offduty1"]) == "")
										{
											if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2, "HH:mm:ss")) >= 0)
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strNotReadCard;
											}
											else if (k + 1 <= this.dvCardRecord.Count - 1 && string.Compare(Strings.Format(this.dvCardRecord[k + 1]["f_ReadDate"], "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
											{
												dataRow["f_Offduty1"] = dateTime4;
												dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
											else if (this.SetObjToStr(dataRow["f_Onduty1"]) == "")
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strNotReadCard;
											}
											else
											{
												dataRow["f_Offduty1"] = dateTime4;
												dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
										}
										else if (k + 1 <= this.dvCardRecord.Count - 1 && string.Compare(Strings.Format(this.dvCardRecord[k + 1]["f_ReadDate"], "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
										{
											if (this.SetObjToStr(dataRow["f_Offduty1Desc"]).IndexOf(CommonStr.strLeaveEarly) >= 0)
											{
												dataRow["f_Offduty1"] = dateTime4;
												dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
											else if (this.SetObjToStr(dataRow["f_Offduty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
											{
												dataRow["f_Offduty1"] = dateTime4;
												dataRow["f_Offduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
										}
										if (this.dvCardRecord.Count == 4)
										{
											k++;
											break;
										}
										break;
									}
								}
								else
								{
									if (this.SetObjToStr(dataRow["f_Offduty1"]) == "")
									{
										dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
										break;
									}
									break;
								}
							}
							if (this.SetObjToStr(dataRow["f_Offduty1"]) == this.SetObjToStr(dataRow["f_Onduty1"]) && (this.SetObjToStr(dataRow["f_Offduty1Desc"]).IndexOf(CommonStr.strLeaveEarly) >= 0 || this.SetObjToStr(dataRow["f_Offduty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0))
							{
								dataRow["f_Offduty1"] = DBNull.Value;
								dataRow["f_Offduty1Desc"] = CommonStr.strNotReadCard;
							}
							if (this.SetObjToStr(dataRow["f_Offduty1"]) == "" && this.SetObjToStr(dataRow["f_Onduty1"]) == "")
							{
								dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
								dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
							}
							if (this.SetObjToStr(dataRow["f_Offduty1"]) == "" && this.SetObjToStr(dataRow["f_Offduty1Desc"]) == "")
							{
								dataRow["f_Offduty1Desc"] = CommonStr.strNotReadCard;
							}
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty1Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
							{
								dataRow["f_OnDuty1Desc"] = "";
							}
							num12 = k;
							k = num12;
							while (k <= this.dvCardRecord.Count - 1)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (Strings.Format(dateTime4, "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2, "HH:mm:ss")) <= 0)
									{
										dataRow["f_Onduty2"] = dateTime4;
										dataRow["f_Onduty2Desc"] = this.dvCardRecord[k]["f_Type"];
										if (this.dvCardRecord.Count == 4)
										{
											k++;
											break;
										}
										k++;
									}
									else
									{
										if (this.SetObjToStr(dataRow["f_Onduty2"]) != "")
										{
											if (!(this.SetObjToStr(dataRow["f_Offduty1"]) == this.SetObjToStr(dataRow["f_Onduty2"])))
											{
												break;
											}
											dataRow["f_Onduty2"] = DBNull.Value;
											dataRow["f_Onduty2Desc"] = "";
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
										{
											dataRow["f_Onduty2"] = dateTime4;
											dataRow["f_Onduty2Desc"] = this.dvCardRecord[k]["f_Type"];
										}
										else if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
										{
											dataRow["f_Onduty2"] = dateTime4;
											dataRow["f_Onduty2Desc"] = CommonStr.strLateness;
										}
										else if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty2.AddMinutes((double)(-(double)this.tLeaveTimeout)), "HH:mm:ss")) > 0)
										{
											dataRow["f_Onduty2Desc"] = CommonStr.strNotReadCard;
										}
										else
										{
											dataRow["f_Onduty2"] = dateTime4;
											dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
										}
										if (this.dvCardRecord.Count == 4)
										{
											k++;
											break;
										}
										break;
									}
								}
								else
								{
									if (this.SetObjToStr(dataRow["f_Onduty2Desc"]) == "" && this.SetObjToStr(dataRow["f_Onduty2"]) == "")
									{
										dataRow["f_Onduty2Desc"] = CommonStr.strNotReadCard;
										break;
									}
									break;
								}
							}
							num12 = k;
							for (k = num12; k <= this.dvCardRecord.Count - 1; k++)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (Strings.Format(dateTime4, "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty2, "HH:mm:ss")) <= 0)
									{
										dataRow["f_Offduty2"] = dateTime4;
										dataRow["f_Offduty2Desc"] = this.dvCardRecord[k]["f_Type"];
										if (string.Compare(Strings.Format(dateTime4.AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty2, "HH:mm:ss")) < 0)
										{
											if (string.Compare(Strings.Format(dateTime4.AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty2, "HH:mm:ss")) < 0)
											{
												dataRow["f_Offduty2Desc"] = CommonStr.strAbsence;
											}
											else
											{
												dataRow["f_Offduty2Desc"] = CommonStr.strLeaveEarly;
											}
										}
									}
									else
									{
										dataRow["f_Offduty2"] = dateTime4;
										dataRow["f_Offduty2Desc"] = this.dvCardRecord[k]["f_Type"];
									}
								}
								else
								{
									dataRow["f_Offduty2"] = dateTime4;
									dataRow["f_Offduty2Desc"] = this.dvCardRecord[k]["f_Type"];
								}
							}
							if (this.SetObjToStr(dataRow["f_Offduty1"]) == this.SetObjToStr(dataRow["f_Onduty2"]) && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
							{
								dataRow["f_Onduty2"] = DBNull.Value;
								dataRow["f_Onduty2Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_Offduty2"]) == this.SetObjToStr(dataRow["f_Onduty2"]) && (this.SetObjToStr(dataRow["f_Offduty2Desc"]).IndexOf(CommonStr.strLeaveEarly) >= 0 || this.SetObjToStr(dataRow["f_Offduty2Desc"]).IndexOf(CommonStr.strAbsence) >= 0))
							{
								dataRow["f_Offduty2"] = DBNull.Value;
								dataRow["f_Offduty2Desc"] = CommonStr.strNotReadCard;
							}
							if (this.SetObjToStr(dataRow["f_Offduty2"]) == "" && this.SetObjToStr(dataRow["f_Onduty2"]) == "")
							{
								dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
								dataRow["f_Offduty2Desc"] = CommonStr.strAbsence;
							}
							if (this.SetObjToStr(dataRow["f_Offduty2"]) == "" && this.SetObjToStr(dataRow["f_Offduty2Desc"]) == "")
							{
								dataRow["f_Offduty2Desc"] = CommonStr.strNotReadCard;
							}
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty2Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
							{
								dataRow["f_OnDuty2Desc"] = "";
							}
						}
						else
						{
							dataRow["f_OnDuty1Desc"] = CommonStr.strAbsence;
							dataRow["f_OffDuty1Desc"] = CommonStr.strAbsence;
							dataRow["f_OnDuty2Desc"] = CommonStr.strAbsence;
							dataRow["f_OffDuty2Desc"] = CommonStr.strAbsence;
						}
						int num13 = 3;
						this.dvHoliday.RowFilter = " f_NO =1 ";
						if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
						{
							num13 = 0;
						}
						else
						{
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num13 = 1;
							}
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num13 = 2;
							}
							this.dvHoliday.RowFilter = " f_NO =2 ";
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
							{
								num13 = 0;
							}
							else
							{
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num13 = 1;
								}
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num13 = 2;
								}
								this.dvHoliday.RowFilter = " f_TYPE =2 ";
								for (int l = 0; l <= this.dvHoliday.Count - 1; l++)
								{
									this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
									DateTime t = DateTime.Parse(this.strTemp);
									this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value2"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
									DateTime t2 = DateTime.Parse(this.strTemp);
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num13 = 0;
										break;
									}
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t2)
									{
										num13 = 2;
									}
									if (t <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num13 = 1;
									}
								}
							}
						}
						if (num13 != 3)
						{
							this.dvHoliday.RowFilter = " f_TYPE =3 ";
							for (int m = 0; m <= this.dvHoliday.Count - 1; m++)
							{
								this.strTemp = Convert.ToString(this.dvHoliday[m]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[m]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime t3 = DateTime.Parse(this.strTemp);
								this.strTemp = Convert.ToString(this.dvHoliday[m]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[m]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime t4 = DateTime.Parse(this.strTemp);
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									num13 = 3;
									break;
								}
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t4)
								{
									if (num13 == 2)
									{
										num13 = 3;
									}
									else
									{
										num13 = 1;
									}
								}
								if (t3 <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									if (num13 == 1)
									{
										num13 = 3;
									}
									else
									{
										num13 = 2;
									}
								}
							}
						}
						if (num13 == 0)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							}
							dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" || this.SetObjToStr(dataRow["f_Offduty1"]) != "" || this.SetObjToStr(dataRow["f_Onduty2"]) != "" || this.SetObjToStr(dataRow["f_Offduty2"]) != "")
							{
								if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
								{
									dataRow["f_OnDuty1Desc"] = CommonStr.strOvertime;
									dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
									if (Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
									else
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
								}
								if (this.SetObjToStr(dataRow["f_Onduty2"]) != "" && this.SetObjToStr(dataRow["f_Offduty2"]) != "")
								{
									dataRow["f_OnDuty2Desc"] = CommonStr.strOvertime;
									dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
									if (Strings.Format(dataRow["f_Offduty2"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
									else
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
								}
							}
							flag = false;
							flag2 = false;
						}
						else if (num13 == 1)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							}
							bool flag3 = false;
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								if (this.SetObjToStr(dataRow["f_OffDuty2"]) != "")
								{
									dataRow["f_OffDuty1"] = dataRow["f_OffDuty2"];
									dataRow["f_OffDuty1Desc"] = "";
									dataRow["f_OffDuty2"] = DBNull.Value;
									dataRow["f_OnDuty2"] = DBNull.Value;
									flag3 = true;
								}
								else if (this.SetObjToStr(dataRow["f_OnDuty2"]) != "")
								{
									dataRow["f_OffDuty1"] = dataRow["f_OnDuty2"];
									dataRow["f_OffDuty1Desc"] = "";
									dataRow["f_OnDuty2"] = DBNull.Value;
									dataRow["f_OffDuty2"] = DBNull.Value;
									flag3 = true;
								}
								if (flag3)
								{
									if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "")
									{
										dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
										dataRow["f_Offduty1Desc"] = DBNull.Value;
									}
									else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
									{
										if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
										{
											dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
										}
										else
										{
											dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
										}
									}
									else
									{
										dataRow["f_Offduty1Desc"] = DBNull.Value;
									}
								}
							}
							dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_Onduty2"]) != "" && this.SetObjToStr(dataRow["f_Offduty2"]) != "")
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strOvertime;
								dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
								if (Strings.Format(dataRow["f_Offduty2"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
								else
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
								dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							}
						}
						else if (num13 == 2)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
							dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "")
								{
									dataRow["f_OnDuty2"] = dataRow["f_OnDuty1"];
									dataRow["f_OnDuty1"] = DBNull.Value;
									dataRow["f_OffDuty1"] = DBNull.Value;
								}
								else if (this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
								{
									dataRow["f_OnDuty2"] = dataRow["f_OffDuty1"];
									dataRow["f_OffDuty1"] = DBNull.Value;
								}
							}
							if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strOvertime;
								dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
								if (Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
								else
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
						}
						else if (num13 == 3 && this.SetObjToStr(dataRow["f_Onduty2"]) != "" && this.SetObjToStr(dataRow["f_Offduty2"]) != "")
						{
							if (string.Compare(Strings.Format(dataRow["f_Offduty2"], "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
							{
								if (string.Compare(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss"), Strings.Format(this.tOffduty2.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss")) >= 0 && string.Compare(Strings.Format(this.tOffduty2.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty2, "HH:mm:ss")) >= 0)
								{
									dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
									dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty2, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
								dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty2, "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
							}
						}
						if (this.dvLeave.Count > 0)
						{
							string value = "";
							string value2 = "";
							num13 = 3;
							for (int n = 0; n <= this.dvLeave.Count - 1; n++)
							{
								this.strTemp = Convert.ToString(this.dvLeave[n]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[n]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime t5 = DateTime.Parse(this.strTemp);
								this.strTemp = Convert.ToString(this.dvLeave[n]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[n]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime t6 = DateTime.Parse(this.strTemp);
								string text2 = Convert.ToString(this.dvLeave[n]["f_HolidayType"]);
								if (t5 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t6)
								{
									value = text2;
									value2 = text2;
									num13 = 0;
									break;
								}
								if (t5 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t6)
								{
									value = text2;
									if (num13 == 1)
									{
										num13 = 0;
										break;
									}
									num13 = 2;
								}
								if (t5 <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t6)
								{
									value2 = text2;
									if (num13 == 2)
									{
										num13 = 0;
										break;
									}
									num13 = 1;
								}
							}
							bool flag4 = false;
							if (num13 == 0)
							{
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OnDuty2Desc"] = value2;
								dataRow["f_OffDuty1Desc"] = value;
								dataRow["f_OffDuty2Desc"] = value2;
								dataRow["f_OnDuty1"] = DBNull.Value;
								dataRow["f_OnDuty2"] = DBNull.Value;
								dataRow["f_OffDuty1"] = DBNull.Value;
								dataRow["f_OffDuty2"] = DBNull.Value;
							}
							else if (num13 == 1)
							{
								if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
								{
									if (this.SetObjToStr(dataRow["f_OffDuty2"]) != "")
									{
										dataRow["f_OffDuty1"] = dataRow["f_OffDuty2"];
										flag4 = true;
										dataRow["f_OffDuty2"] = DBNull.Value;
										dataRow["f_OnDuty2"] = DBNull.Value;
									}
									else if (this.SetObjToStr(dataRow["f_OnDuty2"]) != "")
									{
										dataRow["f_OffDuty1"] = dataRow["f_OnDuty2"];
										flag4 = true;
										dataRow["f_OnDuty2"] = DBNull.Value;
									}
									if (flag4)
									{
										if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "")
										{
											dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
											dataRow["f_Offduty1Desc"] = DBNull.Value;
										}
										else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
										{
											if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
											}
											else
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
											}
										}
										else
										{
											dataRow["f_Offduty1Desc"] = DBNull.Value;
										}
									}
								}
								dataRow["f_OnDuty2Desc"] = value2;
								dataRow["f_OffDuty2Desc"] = value2;
								dataRow["f_OnDuty2"] = DBNull.Value;
								dataRow["f_OffDuty2"] = DBNull.Value;
							}
							else if (num13 == 2)
							{
								if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strLateness || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
								{
									if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "")
									{
										dataRow["f_OnDuty2"] = dataRow["f_OnDuty1"];
										flag4 = true;
										dataRow["f_OnDuty1"] = DBNull.Value;
										dataRow["f_OffDuty1"] = DBNull.Value;
									}
									else if (this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
									{
										flag4 = true;
										dataRow["f_OnDuty2"] = dataRow["f_OffDuty1"];
										dataRow["f_OffDuty1"] = DBNull.Value;
									}
									if (flag4)
									{
										if (this.SetObjToStr(dataRow["f_OffDuty2"]) == "")
										{
											dataRow["f_Offduty2Desc"] = CommonStr.strNotReadCard;
											dataRow["f_Onduty2Desc"] = DBNull.Value;
										}
										else
										{
											DateTime dateTime4 = Convert.ToDateTime(dataRow["f_OnDuty2"]);
											if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = DBNull.Value;
											}
											else if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = CommonStr.strLateness;
											}
											else
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
											}
										}
									}
								}
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OffDuty1Desc"] = value;
								dataRow["f_OnDuty1"] = DBNull.Value;
								dataRow["f_OffDuty1"] = DBNull.Value;
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strLateness)
						{
							dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty1, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strLateness)
						{
							dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty2, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly)
						{
							dataRow["f_LeaveEarlyTime"] = (long)Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OffDuty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(this.tOffduty1, "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strLeaveEarly)
						{
							dataRow["f_LeaveEarlyTime"] = (long)Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OffDuty2"], "HH:mm:ss")), DateTime.Parse(Strings.Format(this.tOffduty2, "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
							flag2 = false;
						}
						else
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
								flag2 = false;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLeaveAbsenceDay;
								flag2 = false;
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty2Desc"]).IndexOf(CommonStr.strAbsence) >= 0)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
							flag2 = false;
						}
						else
						{
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
								flag2 = false;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLeaveAbsenceDay;
								flag2 = false;
							}
						}
						if (Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) >= 1m && num13 != 3)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) / 2.0m;
						}
						text = " INSERT INTO t_d_AttendenceData ";
						text += " ([f_ConsumerID], [f_AttDate], ";
						text += "[f_Onduty1],[f_Onduty1Desc], [f_Offduty1], [f_Offduty1Desc], ";
						text += "[f_Onduty2], [f_Onduty2Desc],[f_Offduty2], [f_Offduty2Desc]  ";
						text += ", [f_LateTime], [f_LeaveEarlyTime],[f_OvertimeTime], [f_AbsenceDay]  ";
						text += " ) ";
						text = text + " VALUES ( " + dataRow["f_ConsumerID"];
						text = text + " , " + this.PrepareStr(dataRow["f_AttDate"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty2Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty2Desc"]);
						text = text + " , " + dataRow["f_LateTime"];
						text = text + " , " + dataRow["f_LeaveEarlyTime"];
						text = text + " , " + this.getDecimalStr(dataRow["f_OvertimeTime"]);
						text = text + " , " + this.getDecimalStr(dataRow["f_AbsenceDay"]);
						text += " ) ";
						using (SqlCommand sqlCommand = new SqlCommand(text, this.cn))
						{
							if (this.cn.State == ConnectionState.Closed)
							{
								this.cn.Open();
							}
							sqlCommand.ExecuteNonQuery();
						}
						if (flag)
						{
							num3++;
						}
						string text3 = "";
						for (j = 0; j <= 3; j++)
						{
							if (j == 0)
							{
								text3 = this.SetObjToStr(dataRow["f_OnDuty1Desc"]);
							}
							else if (j == 1)
							{
								text3 = this.SetObjToStr(dataRow["f_OnDuty2Desc"]);
							}
							else if (j == 2)
							{
								text3 = this.SetObjToStr(dataRow["f_OffDuty1Desc"]);
							}
							else if (j == 3)
							{
								text3 = this.SetObjToStr(dataRow["f_OffDuty2Desc"]);
							}
							if (text3 == CommonStr.strLateness)
							{
								num5++;
								flag2 = false;
							}
							else if (text3 == CommonStr.strLeaveEarly)
							{
								num6++;
								flag2 = false;
							}
							else if (text3 == CommonStr.strNotReadCard)
							{
								num9++;
								flag2 = false;
							}
							else
							{
								text3.IndexOf(CommonStr.strNotReadCard);
								int i = 0;
								while (i <= this.dtHolidayType.Rows.Count - 1 && i < array.Length)
								{
									if (text3 == this.dtHolidayType.Rows[i][1].ToString())
									{
										flag2 = false;
										array[i] += 0.25m;
										break;
									}
									i++;
								}
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "" && this.SetObjToStr(dataRow["f_OffDuty1"]) == "" && this.SetObjToStr(dataRow["f_OnDuty2"]) == "" && this.SetObjToStr(dataRow["f_OffDuty2"]) == "")
						{
							flag2 = false;
						}
						num7 += Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture);
						num8 += Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture);
						num10 += Convert.ToInt32(dataRow["f_LateTime"]);
						num11 += Convert.ToInt32(dataRow["f_LeaveEarlyTime"]);
						if (Convert.ToInt32(dataRow["f_LateTime"]) != 0 || Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) != 0 || !(Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) == 0m))
						{
							flag2 = false;
						}
						if (flag2)
						{
							num4++;
						}
						dateTime3 = dateTime3.AddDays(1.0);
						Application.DoEvents();
					}
					this.dvCardRecord.RowFilter = string.Format("f_Type ={0}", this.PrepareStr(CommonStr.strSignIn));
					text = " Insert Into t_d_AttStatistic ";
					text += " ( [f_ConsumerID], [f_AttDateStart], [f_AttDateEnd] ";
					text += " , [f_DayShouldWork],  [f_DayRealWork]";
					text += " , [f_TotalLate],  [f_TotalLeaveEarly],[f_TotalOvertime], [f_TotalAbsenceDay], [f_TotalNotReadCard]";
					for (int i = 1; i <= 32; i++)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" , [f_SpecialType",
							i,
							"]"
						});
					}
					text += ", f_LateMinutes";
					text += ", f_LeaveEarlyMinutes";
					text += ", f_ManualReadTimesCount";
					text += " ) ";
					text = text + " Values( " + dataRow["f_ConsumerID"];
					text = text + " , " + this.PrepareStr(dateTime, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(dateTime2, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + num3;
					text = text + " , " + num4;
					text = text + " , " + num5;
					text = text + " , " + num6;
					text = text + " , " + this.getDecimalStr(num8);
					text = text + " , " + this.getDecimalStr(num7);
					text = text + " , " + num9;
					for (int i = 0; i <= 31; i++)
					{
						text = text + " , " + this.PrepareStr(this.getDecimalStr(array[i]));
					}
					text = text + ", " + num10;
					text = text + ", " + num11;
					text = text + ", " + this.dvCardRecord.Count;
					text += " )";
					using (SqlCommand sqlCommand2 = new SqlCommand(text, this.cn))
					{
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						sqlCommand2.ExecuteNonQuery();
					}
				}
				sqlDataReader.Close();
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				this.shiftAttReportImportFromAttendenceData();
				this.shiftAttStatisticImportFromAttStatistic();
				this.logCreateReport();
				if (this.CreateCompleteEvent != null)
				{
					this.CreateCompleteEvent(true, "");
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
				try
				{
					if (this.CreateCompleteEvent != null)
					{
						this.CreateCompleteEvent(false, ex.ToString());
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void getAttendenceParam()
		{
			string cmdText = "SELECT * FROM t_a_Attendence";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						if ((int)sqlDataReader["f_No"] == 1)
						{
							this.tLateTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 2)
						{
							this.tlateAbsenceTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 3)
						{
							this.tLateAbsenceDay = Convert.ToDecimal(sqlDataReader["f_Value"], CultureInfo.InvariantCulture);
						}
						else if ((int)sqlDataReader["f_No"] == 4)
						{
							this.tLeaveTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 5)
						{
							this.tLeaveAbsenceTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 6)
						{
							this.tLeaveAbsenceDay = Convert.ToDecimal(sqlDataReader["f_Value"], CultureInfo.InvariantCulture);
						}
						else if ((int)sqlDataReader["f_No"] == 7)
						{
							this.tOvertimeTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 8)
						{
							this.tOnduty0 = Convert.ToDateTime(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 9)
						{
							this.tOffduty0 = Convert.ToDateTime(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 10)
						{
							this.tOnduty1 = Convert.ToDateTime(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 11)
						{
							this.tOffduty1 = Convert.ToDateTime(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 12)
						{
							this.tOnduty2 = Convert.ToDateTime(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 13)
						{
							this.tOffduty2 = Convert.ToDateTime(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 14)
						{
							this.tReadCardTimes = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
						else if ((int)sqlDataReader["f_No"] == 16)
						{
							this.tTwoReadMintime = Convert.ToInt32(sqlDataReader["f_Value"]);
						}
					}
					sqlDataReader.Close();
					this.strAllowOndutyTime = wgAppConfig.getSystemParamByNO(55).ToString();
					this.bEarliestAsOnDuty = (wgAppConfig.getSystemParamByNO(56).ToString() == "1");
					this.bChooseTwoTimes = (wgAppConfig.getSystemParamByNO(57).ToString() == "1");
					this.needDutyHour = decimal.Parse(wgAppConfig.getSystemParamByNO(58).ToString());
					this.bChooseOnlyOnDuty = (wgAppConfig.getSystemParamByNO(59).ToString() == "1");
				}
			}
		}

		public void _clearAttendenceData()
		{
			string cmdText = "delete from t_d_AttendenceData";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
			}
		}

		public void _clearAttStatistic()
		{
			string cmdText = "delete from t_d_AttStatistic";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
			}
		}

		public int shiftAttReportImportFromAttendenceData()
		{
			int result = 0;
			string text = "SELECT * FROM t_d_AttendenceData  ORDER BY f_RecID ";
			this.dtAttendenceData = new DataTable("AttendenceData");
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtAttendenceData);
					}
				}
			}
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.cmd = new SqlCommand(text, this.cn);
			try
			{
				if (this.dtAttendenceData.Rows.Count > 0)
				{
					for (int i = 0; i <= this.dtAttendenceData.Rows.Count - 1; i++)
					{
						if (this.DealingNumEvent != null)
						{
							this.DealingNumEvent(i);
						}
						int num = 0;
						DataRow dataRow = this.dtAttendenceData.Rows[i];
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
						{
							num++;
						}
						if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
						{
							num++;
						}
						if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
						{
							num++;
						}
						if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]).IndexOf(CommonStr.strNotReadCard) >= 0)
						{
							num++;
						}
						text = " INSERT INTO t_d_Shift_AttReport ";
						text += " ( f_ConsumerID, f_shiftDate, f_ShiftID, f_ReadTimes ";
						text += " , f_OnDuty1, f_OnDuty1AttDesc, f_OnDuty1CardRecordDesc ";
						text += " , f_OffDuty1, f_OffDuty1AttDesc, f_OffDuty1CardRecordDesc ";
						text += " , f_OnDuty2, f_OnDuty2AttDesc, f_OnDuty2CardRecordDesc ";
						text += " , f_OffDuty2, f_OffDuty2AttDesc, f_OffDuty2CardRecordDesc ";
						text += " , f_OnDuty3, f_OnDuty3AttDesc, f_OnDuty3CardRecordDesc ";
						text += " , f_OffDuty3, f_OffDuty3AttDesc, f_OffDuty3CardRecordDesc ";
						text += " , f_OnDuty4, f_OnDuty4AttDesc, f_OnDuty4CardRecordDesc ";
						text += " , f_OffDuty4, f_OffDuty4AttDesc, f_OffDuty4CardRecordDesc ";
						text += " , f_LateMinutes, f_LeaveEarlyMinutes, f_OvertimeHours, f_AbsenceDays ";
						text += " , f_NotReadCardCount, f_bOvertimeShift ";
						text += " ) ";
						text = text + " Values ( " + dataRow["f_ConsumerID"];
						text = text + "," + this.PrepareStr(dataRow["f_AttDate"], true, "yyyy-MM-dd");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.tReadCardTimes;
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty1Desc"]);
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty1Desc"]);
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty2Desc"]);
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty2Desc"]);
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + this.PrepareStr("");
						text = text + "," + dataRow["f_LateTime"];
						text = text + "," + dataRow["f_LeaveEarlyTime"];
						text = text + "," + this.getDecimalStr(dataRow["f_OvertimeTime"]);
						text = text + "," + this.getDecimalStr(dataRow["f_AbsenceDay"]);
						text = text + "," + num;
						text = text + "," + this.PrepareStr("");
						text += ") ";
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						this.cmd.CommandText = text;
						int num2 = this.cmd.ExecuteNonQuery();
						if (num2 <= 0)
						{
							break;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				if (this.cn != null)
				{
					this.cn.Dispose();
				}
				if (this.cmd != null)
				{
					this.cmd.Dispose();
				}
			}
			return result;
		}

		public int shiftAttStatisticImportFromAttStatistic()
		{
			int result = 0;
			string text = "SELECT * FROM t_d_AttStatistic  ORDER BY f_RecID ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						using (DataTable dataTable = new DataTable("AttStatistic"))
						{
							sqlDataAdapter.Fill(dataTable);
							if (dataTable.Rows.Count > 0)
							{
								sqlCommand.Connection = sqlConnection;
								sqlCommand.CommandType = CommandType.Text;
								for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
								{
									if (this.DealingNumEvent != null)
									{
										this.DealingNumEvent(i);
									}
									DataRow dataRow = dataTable.Rows[i];
									text = " INSERT INTO t_d_Shift_AttStatistic ";
									text += " ( f_ConsumerID ";
									text += " , f_AttDateStart, f_AttDateEnd, f_DayShouldWork ";
									text += " , f_DayRealWork";
									text += " , f_LateMinutes,f_LateCount ";
									text += " , f_LeaveEarlyMinutes,f_LeaveEarlyCount ";
									text += " , f_OvertimeHours ";
									text += " , f_AbsenceDays ";
									text += " , f_NotReadCardCount, f_ManualReadTimesCount ";
									for (int j = 1; j <= 32; j++)
									{
										text = text + " , f_SpecialType" + j.ToString();
									}
									text += " )";
									text = text + " Values ( " + dataRow["f_ConsumerID"];
									text = text + "," + this.PrepareStr(dataRow["f_AttDateStart"], true, "yyyy-MM-dd HH:mm:ss");
									text = text + "," + this.PrepareStr(dataRow["f_AttDateEnd"], true, "yyyy-MM-dd HH:mm:ss");
									text = text + "," + dataRow["f_DayShouldWork"];
									text = text + "," + dataRow["f_DayRealWork"];
									text = text + "," + dataRow["f_LateMinutes"];
									text = text + "," + dataRow["f_TotalLate"];
									text = text + "," + dataRow["f_LeaveEarlyMinutes"];
									text = text + "," + dataRow["f_TotalLeaveEarly"];
									text = text + "," + this.getDecimalStr(dataRow["f_TotalOvertime"]);
									text = text + "," + this.getDecimalStr(dataRow["f_TotalAbsenceDay"]);
									text = text + "," + dataRow["f_TotalNotReadCard"];
									text = text + "," + dataRow["f_ManualReadTimesCount"];
									for (int k = 1; k <= 32; k++)
									{
										text = text + " ," + this.PrepareStr(dataRow["f_SpecialType" + k.ToString()]);
									}
									text += ") ";
									if (sqlConnection.State == ConnectionState.Closed)
									{
										sqlConnection.Open();
									}
									sqlCommand.CommandText = text;
									int num = sqlCommand.ExecuteNonQuery();
									if (num <= 0)
									{
										break;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		private void logCreateReport()
		{
		}

		private void make4OneTime()
		{
			this.cnConsumer = new SqlConnection(wgAppConfig.dbConString);
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dtCardRecord2 = new DataTable();
			this.dsAtt = new DataSet("Attendance");
			this.dsAtt.Clear();
			this.daAttendenceData = new SqlDataAdapter("SELECT * FROM t_d_AttendenceData WHERE 1<0", this.cn);
			this.daHoliday = new SqlDataAdapter("SELECT * FROM t_a_Holiday ORDER BY  f_NO ASC", this.cn);
			this.daHolidayType = new SqlDataAdapter("SELECT * FROM t_a_HolidayType", this.cn);
			this.daLeave = new SqlDataAdapter("SELECT * FROM t_d_Leave", this.cn);
			this.daNoCardRecord = new SqlDataAdapter("SELECT f_ReadDate,f_Character,'' as f_Type  FROM t_d_ManualCardRecord Where 1<0 ", this.cn);
			this.daNoCardRecord.Fill(this.dsAtt, "AllCardRecords");
			this.dtCardRecord2 = this.dsAtt.Tables["AllCardRecords"];
			this.dtCardRecord2.Clear();
			this.daAttendenceData.Fill(this.dsAtt, "AttendenceData");
			this.dtAttendenceData = this.dsAtt.Tables["AttendenceData"];
			this.getAttendenceParam();
			this._clearAttendenceData();
			this._clearAttStatistic();
			this.daHoliday.Fill(this.dsAtt, "Holiday");
			this.dtHoliday = this.dsAtt.Tables["Holiday"];
			this.localizedHoliday(this.dtHoliday);
			this.dvHoliday = new DataView(this.dtHoliday);
			this.dvHoliday.RowFilter = "";
			this.dvHoliday.Sort = " f_NO ASC ";
			this.daLeave.Fill(this.dsAtt, "Leave");
			this.dtLeave = this.dsAtt.Tables["Leave"];
			this.dvLeave = new DataView(this.dtLeave);
			this.dvLeave.RowFilter = "";
			this.dvLeave.Sort = " f_NO ASC ";
			this.daHolidayType.Fill(this.dsAtt, "HolidayType");
			this.dtHolidayType = this.dsAtt.Tables["HolidayType"];
			this.localizedHolidayType(this.dtHolidayType);
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 AND f_ShiftEnabled =0) ", this.cnConsumer);
			}
			else
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 ) ", this.cnConsumer);
			}
			this.cnConsumer.Open();
			SqlDataReader sqlDataReader = this.cmdConsumer.ExecuteReader();
			int num = 0;
			try
			{
				int num2 = 0;
				while (sqlDataReader.Read())
				{
					num = (int)sqlDataReader["f_ConsumerID"];
					num2++;
					string text = "SELECT f_ReadDate,f_Character,'' as f_Type  ";
					text += " FROM t_d_SwipeRecord INNER JOIN t_b_Reader ON ( t_b_Reader.f_Attend=1 AND t_d_SwipeRecord.f_ReaderID =t_b_Reader.f_ReaderID ) ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " AND t_b_Reader.f_Attend = 1 ";
					if (wgAppConfig.getSystemParamByNO(54) == "1")
					{
						text += " AND f_Character >= 1 ";
					}
					text += " ORDER BY f_ReadDate ASC ";
					this.daCardRecord = new SqlDataAdapter(text, this.cn);
					text = "SELECT f_ReadDate,f_Character ";
					text += string.Format(", {0} as f_Type", wgTools.PrepareStr(CommonStr.strSignIn));
					text += " FROM t_d_ManualCardRecord  ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " ORDER BY f_ReadDate ASC ";
					this.daManualCardRecord = new SqlDataAdapter(text, this.cn);
					decimal[] array = new decimal[32];
					DataRow dataRow = null;
					if (this.DealingNumEvent != null)
					{
						this.DealingNumEvent(num2);
					}
					this.gProcVal = num2 + 1;
					if (this.bStopCreate)
					{
						return;
					}
					DateTime dateTime = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					DateTime dateTime2 = DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime));
					DateTime dateTime3 = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					decimal num7 = 0m;
					decimal num8 = 0m;
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					for (int i = 0; i <= array.Length - 1; i++)
					{
						array[i] = 0m;
					}
					this.dtCardRecord2 = this.dsAtt.Tables["AllCardRecords"];
					this.dsAtt.Tables["AllCardRecords"].Clear();
					this.daCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.daManualCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.dvCardRecord = new DataView(this.dtCardRecord2);
					this.dvCardRecord.RowFilter = "";
					this.dvCardRecord.Sort = " f_ReadDate ASC ";
					int j = 0;
					while (this.dvCardRecord.Count > j + 1)
					{
						if (((DateTime)this.dvCardRecord[j + 1][0]).Subtract((DateTime)this.dvCardRecord[j][0]).TotalSeconds < (double)this.tTwoReadMintime)
						{
							this.dvCardRecord[j + 1].Delete();
						}
						else
						{
							j++;
						}
					}
					while (dateTime3 <= DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime)))
					{
						dataRow = this.dtAttendenceData.NewRow();
						dataRow["f_ConsumerID"] = num;
						dataRow["f_AttDate"] = dateTime3;
						dataRow["f_LateTime"] = 0;
						dataRow["f_LeaveEarlyTime"] = 0;
						dataRow["f_OvertimeTime"] = 0;
						dataRow["f_AbsenceDay"] = 0;
						bool flag = true;
						bool flag2 = true;
						bool flag3 = false;
						this.dvCardRecord.RowFilter = "  f_ReadDate >= #" + dateTime3.ToString("yyyy-MM-dd HH:mm:ss") + "# and f_ReadDate<= " + Strings.Format(dateTime3.AddDays((double)this.normalDay), "#yyyy-MM-dd " + this.strAllowOffdutyTime + "#");
						this.dvLeave.RowFilter = " f_ConsumerID = " + num.ToString();
						if (this.dvCardRecord.Count > 0)
						{
							DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[0]["f_ReadDate"]);
							dataRow["f_Onduty1"] = dateTime4;
							dataRow["f_Onduty1Desc"] = this.dvCardRecord[0]["f_Type"];
							if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty0.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) > 0)
							{
								if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty0.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
								{
									dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
								}
								else
								{
									dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
								}
							}
						}
						else
						{
							dataRow["f_OnDuty1Desc"] = CommonStr.strAbsence;
						}
						int num12 = 3;
						this.dvHoliday.RowFilter = " f_NO =1 ";
						if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
						{
							num12 = 0;
						}
						else
						{
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num12 = 1;
							}
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num12 = 2;
							}
							this.dvHoliday.RowFilter = " f_NO =2 ";
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
							{
								num12 = 0;
							}
							else
							{
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num12 = 1;
								}
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num12 = 2;
								}
								this.dvHoliday.RowFilter = " f_TYPE =2 ";
								for (int k = 0; k <= this.dvHoliday.Count - 1; k++)
								{
									this.strTemp = Convert.ToString(this.dvHoliday[k]["f_Value"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[k]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
									DateTime t;
									DateTime.TryParse(this.strTemp, out t);
									this.strTemp = Convert.ToString(this.dvHoliday[k]["f_Value2"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[k]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
									DateTime t2;
									DateTime.TryParse(this.strTemp, out t2);
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num12 = 0;
										break;
									}
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t2)
									{
										num12 = 2;
									}
									if (t <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num12 = 1;
									}
								}
							}
						}
						if (num12 != 3)
						{
							this.dvHoliday.RowFilter = " f_TYPE =3 ";
							for (int l = 0; l <= this.dvHoliday.Count - 1; l++)
							{
								this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime t3;
								DateTime.TryParse(this.strTemp, out t3);
								this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime t4;
								DateTime.TryParse(this.strTemp, out t4);
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									num12 = 3;
									break;
								}
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t4)
								{
									if (num12 == 2)
									{
										num12 = 3;
									}
									else
									{
										num12 = 1;
									}
								}
								if (t3 <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									if (num12 == 1)
									{
										num12 = 3;
									}
									else
									{
										num12 = 2;
									}
								}
							}
						}
						if (num12 == 0)
						{
							dataRow["f_Onduty1"] = DBNull.Value;
							dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							flag = false;
							flag2 = false;
						}
						else if (num12 != 1)
						{
							if (num12 == 2)
							{
								if (this.SetObjToStr(dataRow["f_Onduty1"]) != "")
								{
									if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Onduty1"]).AddMinutes((double)(-(double)this.tlateAbsenceTimeout)), "HH:mm:ss"), "13:30:00") > 0)
									{
										flag3 = true;
										dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
									}
									else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Onduty1"]).AddMinutes((double)(-(double)this.tLateTimeout)), "HH:mm:ss"), "13:30:00") > 0)
									{
										flag3 = true;
										dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
									}
									else
									{
										dataRow["f_Onduty1Desc"] = "";
									}
								}
							}
						}
						if (this.dvLeave.Count > 0)
						{
							DateTime now = DateTime.Now;
							DateTime now2 = DateTime.Now;
							string value = "";
							num12 = 3;
							for (int m = 0; m <= this.dvLeave.Count - 1; m++)
							{
								this.strTemp = Convert.ToString(this.dvLeave[m]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[m]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime.TryParse(this.strTemp, out now);
								this.strTemp = Convert.ToString(this.dvLeave[m]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[m]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime.TryParse(this.strTemp, out now2);
								string text2 = Convert.ToString(this.dvLeave[m]["f_HolidayType"]);
								if (now <= dateTime3 && Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= now2)
								{
									value = text2;
									num12 = 0;
									break;
								}
								if (now <= dateTime3 && Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= now2)
								{
									value = text2;
									if (num12 == 1)
									{
										num12 = 0;
										break;
									}
									num12 = 2;
								}
								if (now <= Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && Convert.ToDateTime(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= now2)
								{
									if (num12 == 2)
									{
										num12 = 0;
										break;
									}
									num12 = 1;
								}
							}
							if (num12 == 0)
							{
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OnDuty1"] = DBNull.Value;
							}
							else if (num12 != 1)
							{
								if (num12 == 2)
								{
									if (this.SetObjToStr(dataRow["f_Onduty1"]) != "")
									{
										dataRow["f_OnDuty1Desc"] = value;
										if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Onduty1"]).AddMinutes((double)(-(double)this.tlateAbsenceTimeout)), "HH:mm:ss"), "13:30:00") > 0)
										{
											flag3 = true;
											dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
										}
										else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_Onduty1"]).AddMinutes((double)(-(double)this.tLateTimeout)), "HH:mm:ss"), "13:30:00") > 0)
										{
											flag3 = true;
											dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
										}
									}
								}
							}
						}
						if (this.bChooseTwoTimes)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strLateness)
							{
								dataRow["f_OnDuty1Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly)
							{
								dataRow["f_OffDuty1Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strOvertime)
							{
								dataRow["f_OnDuty1Desc"] = "";
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strOvertime)
							{
								dataRow["f_OffDuty1Desc"] = "";
							}
							dataRow["f_OvertimeTime"] = 0;
							if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "" && this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
							{
								dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "yyyy-MM-dd HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
							}
							flag2 = ((decimal)dataRow["f_OvertimeTime"] >= this.needDutyHour);
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strLateness)
						{
							if (flag3)
							{
								dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty0, "13:30:00")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
							}
							else
							{
								dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty0, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0)
						{
							dataRow["f_AbsenceDay"] = this.tLateAbsenceDay + this.tLeaveAbsenceDay;
							flag2 = false;
						}
						if (Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) >= 1m && num12 != 3)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) / 2.0m;
						}
						text = " INSERT INTO t_d_AttendenceData ";
						text += " ([f_ConsumerID], [f_AttDate], ";
						text += "[f_Onduty1],[f_Onduty1Desc], [f_Offduty1], [f_Offduty1Desc]";
						text += ", [f_LateTime], [f_LeaveEarlyTime],[f_OvertimeTime], [f_AbsenceDay]  ";
						text += " ) ";
						text = text + " VALUES ( " + dataRow["f_ConsumerID"];
						text = text + " , " + this.PrepareStr(dataRow["f_AttDate"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1Desc"]);
						text = text + " , " + dataRow["f_LateTime"];
						text = text + " , " + dataRow["f_LeaveEarlyTime"];
						text = text + " , " + this.getDecimalStr(dataRow["f_OvertimeTime"]);
						text = text + " , " + this.getDecimalStr(dataRow["f_AbsenceDay"]);
						text += " ) ";
						using (SqlCommand sqlCommand = new SqlCommand(text, this.cn))
						{
							if (this.cn.State == ConnectionState.Closed)
							{
								this.cn.Open();
							}
							sqlCommand.ExecuteNonQuery();
						}
						if (flag)
						{
							num3++;
						}
						string a = "";
						for (j = 0; j <= 1; j++)
						{
							if (j == 0)
							{
								a = this.SetObjToStr(dataRow["f_OnDuty1Desc"]);
							}
							else if (j == 1)
							{
								a = this.SetObjToStr(dataRow["f_OffDuty1Desc"]);
							}
							if (a == CommonStr.strLateness)
							{
								num5++;
								flag2 = false;
							}
							else if (a == CommonStr.strLeaveEarly)
							{
								num6++;
								flag2 = false;
							}
							else if (a == CommonStr.strNotReadCard)
							{
								num9++;
								flag2 = false;
							}
							else
							{
								int i = 0;
								while (i <= this.dtHolidayType.Rows.Count - 1 && i < array.Length)
								{
									if (a == Convert.ToString(this.dtHolidayType.Rows[i][1]))
									{
										flag2 = false;
										if (this.bChooseOnlyOnDuty)
										{
											array[i] += 1.0m;
											break;
										}
										array[i] += 0.5m;
										break;
									}
									else
									{
										i++;
									}
								}
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "" && this.SetObjToStr(dataRow["f_OffDuty1"]) == "")
						{
							flag2 = false;
						}
						num7 += Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture);
						num8 += Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture);
						num10 += Convert.ToInt32(dataRow["f_LateTime"]);
						num11 += Convert.ToInt32(dataRow["f_LeaveEarlyTime"]);
						if (Convert.ToInt32(dataRow["f_LateTime"]) != 0 || Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) != 0 || !(Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) == 0m))
						{
							flag2 = false;
						}
						if (flag2)
						{
							num4++;
						}
						dateTime3 = dateTime3.AddDays(1.0);
						Application.DoEvents();
					}
					this.dvCardRecord.RowFilter = string.Format("f_Type ={0}", this.PrepareStr(CommonStr.strSignIn));
					text = " Insert Into t_d_AttStatistic ";
					text += " ( [f_ConsumerID], [f_AttDateStart], [f_AttDateEnd] ";
					text += " , [f_DayShouldWork],  [f_DayRealWork]";
					text += " , [f_TotalLate],  [f_TotalLeaveEarly],[f_TotalOvertime], [f_TotalAbsenceDay], [f_TotalNotReadCard]";
					for (int i = 1; i <= 32; i++)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" , [f_SpecialType",
							i,
							"]"
						});
					}
					text += ", f_LateMinutes";
					text += ", f_LeaveEarlyMinutes";
					text += ", f_ManualReadTimesCount";
					text += " ) ";
					text = text + " Values( " + dataRow["f_ConsumerID"];
					text = text + " , " + this.PrepareStr(dateTime, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(dateTime2, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + num3;
					text = text + " , " + num4;
					text = text + " , " + num5;
					text = text + " , " + num6;
					text = text + " , " + this.getDecimalStr(num8);
					text = text + " , " + this.getDecimalStr(num7);
					text = text + " , " + num9;
					for (int i = 0; i <= 31; i++)
					{
						text = text + " , " + this.PrepareStr(this.getDecimalStr(array[i]));
					}
					text = text + ", " + num10;
					text = text + ", " + num11;
					text = text + ", " + this.dvCardRecord.Count;
					text += " )";
					using (SqlCommand sqlCommand2 = new SqlCommand(text, this.cn))
					{
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						sqlCommand2.ExecuteNonQuery();
					}
				}
				sqlDataReader.Close();
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				this.shiftAttReportImportFromAttendenceData();
				this.shiftAttStatisticImportFromAttStatistic();
				this.logCreateReport();
				if (this.CreateCompleteEvent != null)
				{
					this.CreateCompleteEvent(true, "");
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
				try
				{
					if (this.CreateCompleteEvent != null)
					{
						this.CreateCompleteEvent(false, ex.ToString());
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void make4FourTimesOnlyDuty()
		{
			this.cnConsumer = new SqlConnection(wgAppConfig.dbConString);
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dtCardRecord1 = new DataTable();
			this.dsAtt = new DataSet("Attendance");
			this.dsAtt.Clear();
			this.daAttendenceData = new SqlDataAdapter("SELECT * FROM t_d_AttendenceData WHERE 1<0", this.cn);
			this.daHoliday = new SqlDataAdapter("SELECT * FROM t_a_Holiday ORDER BY  f_NO ASC", this.cn);
			this.daHolidayType = new SqlDataAdapter("SELECT * FROM t_a_HolidayType", this.cn);
			this.daLeave = new SqlDataAdapter("SELECT * FROM t_d_Leave", this.cn);
			this.daNoCardRecord = new SqlDataAdapter("SELECT f_ReadDate,f_Character,'' as f_Type  FROM t_d_ManualCardRecord Where 1<0 ", this.cn);
			this.daNoCardRecord.Fill(this.dsAtt, "AllCardRecords");
			this.dtCardRecord1 = this.dsAtt.Tables["AllCardRecords"];
			this.dtCardRecord1.Clear();
			this.daAttendenceData.Fill(this.dsAtt, "AttendenceData");
			this.dtAttendenceData = this.dsAtt.Tables["AttendenceData"];
			this.getAttendenceParam();
			this._clearAttendenceData();
			this._clearAttStatistic();
			this.daHoliday.Fill(this.dsAtt, "Holiday");
			this.dtHoliday = this.dsAtt.Tables["Holiday"];
			this.localizedHoliday(this.dtHoliday);
			this.dvHoliday = new DataView(this.dtHoliday);
			this.dvHoliday.RowFilter = "";
			this.dvHoliday.Sort = " f_NO ASC ";
			this.daLeave.Fill(this.dsAtt, "Leave");
			this.dtLeave = this.dsAtt.Tables["Leave"];
			this.dvLeave = new DataView(this.dtLeave);
			this.dvLeave.RowFilter = "";
			this.dvLeave.Sort = " f_NO ASC ";
			this.daHolidayType.Fill(this.dsAtt, "HolidayType");
			this.dtHolidayType = this.dsAtt.Tables["HolidayType"];
			this.localizedHolidayType(this.dtHolidayType);
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			if (paramValBoolByNO)
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 AND f_ShiftEnabled =0) ", this.cnConsumer);
			}
			else
			{
				this.cmdConsumer = new SqlCommand(this.strConsumerSql + "AND (f_AttendEnabled >0 ) ", this.cnConsumer);
			}
			this.cnConsumer.Open();
			SqlDataReader sqlDataReader = this.cmdConsumer.ExecuteReader();
			int num = 0;
			try
			{
				int num2 = 0;
				while (sqlDataReader.Read())
				{
					num = (int)sqlDataReader["f_ConsumerID"];
					num2++;
					string text = "SELECT f_ReadDate,f_Character,'' as f_Type  ";
					text += " FROM t_d_SwipeRecord INNER JOIN t_b_Reader ON ( t_b_Reader.f_Attend=1 AND t_d_SwipeRecord.f_ReaderID =t_b_Reader.f_ReaderID ) ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " AND t_b_Reader.f_Attend = 1 ";
					if (wgAppConfig.getSystemParamByNO(54) == "1")
					{
						text += " AND f_Character >= 1 ";
					}
					text += " ORDER BY f_ReadDate ASC ";
					this.daCardRecord = new SqlDataAdapter(text, this.cn);
					text = "SELECT f_ReadDate,f_Character ";
					text += string.Format(",{0} as f_Type", this.PrepareStr(CommonStr.strSignIn));
					text += " FROM t_d_ManualCardRecord  ";
					text = text + " WHERE f_ConsumerID=" + num.ToString();
					text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(this.startDateTime, true, "yyyy-MM-dd 00:00:00") + ") ";
					text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(this.endDateTime.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
					text += " ORDER BY f_ReadDate ASC ";
					this.daManualCardRecord = new SqlDataAdapter(text, this.cn);
					decimal[] array = new decimal[32];
					DataRow dataRow = null;
					if (this.DealingNumEvent != null)
					{
						this.DealingNumEvent(num2);
					}
					this.gProcVal = num2 + 1;
					if (this.bStopCreate)
					{
						return;
					}
					DateTime dateTime = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					DateTime dateTime2 = DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime));
					DateTime dateTime3 = DateTime.Parse(Strings.Format(this.startDateTime, "yyyy-MM-dd " + this.strAllowOndutyTime));
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					decimal num7 = 0m;
					decimal num8 = 0m;
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					for (int i = 0; i <= array.Length - 1; i++)
					{
						array[i] = 0m;
					}
					this.dtCardRecord1 = this.dsAtt.Tables["AllCardRecords"];
					this.dsAtt.Tables["AllCardRecords"].Clear();
					this.daCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.daManualCardRecord.Fill(this.dsAtt, "AllCardRecords");
					this.dvCardRecord = new DataView(this.dtCardRecord1);
					this.dvCardRecord.RowFilter = "";
					this.dvCardRecord.Sort = " f_ReadDate ASC ";
					int j = 0;
					while (this.dvCardRecord.Count > j + 1)
					{
						if (((DateTime)this.dvCardRecord[j + 1][0]).Subtract((DateTime)this.dvCardRecord[j][0]).TotalSeconds < (double)this.tTwoReadMintime)
						{
							this.dvCardRecord[j + 1].Delete();
						}
						else
						{
							j++;
						}
					}
					while (dateTime3 <= DateTime.Parse(Strings.Format(this.endDateTime.AddDays((double)this.normalDay), "yyyy-MM-dd " + this.strAllowOffdutyTime)))
					{
						dataRow = this.dtAttendenceData.NewRow();
						dataRow["f_ConsumerID"] = num;
						dataRow["f_AttDate"] = dateTime3;
						dataRow["f_LateTime"] = 0;
						dataRow["f_LeaveEarlyTime"] = 0;
						dataRow["f_OvertimeTime"] = 0;
						dataRow["f_AbsenceDay"] = 0;
						bool flag = true;
						bool flag2 = true;
						this.dvCardRecord.RowFilter = " f_ReadDate >= #" + dateTime3.ToString("yyyy-MM-dd HH:mm:ss") + "# and f_ReadDate<= " + Strings.Format(dateTime3.AddDays((double)this.normalDay), "#yyyy-MM-dd " + this.strAllowOffdutyTime + "#");
						this.dvLeave.RowFilter = " f_ConsumerID = " + num.ToString();
						if (this.dvCardRecord.Count > 0)
						{
							int k = 0;
							while (k <= this.dvCardRecord.Count - 1)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (string.Compare(Strings.Format(dateTime4, "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty1, "HH:mm:ss")) <= 0)
									{
										if (this.bEarliestAsOnDuty)
										{
											if (this.SetObjToStr(dataRow["f_Onduty1"]) == "")
											{
												dataRow["f_Onduty1"] = dateTime4;
												dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											}
										}
										else
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
										}
										k++;
									}
									else
									{
										if (this.SetObjToStr(dataRow["f_Onduty1"]) != "")
										{
											break;
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty1.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = this.dvCardRecord[k]["f_Type"];
											k++;
											break;
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty1.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
										{
											dataRow["f_Onduty1"] = dateTime4;
											dataRow["f_Onduty1Desc"] = CommonStr.strLateness;
											k++;
											break;
										}
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty1.AddMinutes((double)(-(double)this.tLeaveTimeout)), "HH:mm:ss")) > 0)
										{
											dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
											break;
										}
										dataRow["f_Onduty1"] = dateTime4;
										dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
										break;
									}
								}
								else
								{
									if (!(this.SetObjToStr(dataRow["f_Onduty1"]) != ""))
									{
										dataRow["f_Onduty1"] = dateTime4;
										dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
										break;
									}
									break;
								}
							}
							if (this.SetObjToStr(dataRow["f_Offduty1"]) == "" && this.SetObjToStr(dataRow["f_Onduty1"]) == "")
							{
								dataRow["f_Onduty1Desc"] = CommonStr.strAbsence;
								dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
							}
							int num12 = k;
							k = num12;
							while (k <= this.dvCardRecord.Count - 1)
							{
								DateTime dateTime4 = Convert.ToDateTime(this.dvCardRecord[k]["f_ReadDate"]);
								if (Strings.Format(dateTime4, "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) > 0)
									{
										if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2, "HH:mm:ss")) <= 0)
										{
											dataRow["f_Onduty2"] = dateTime4;
											dataRow["f_Onduty2Desc"] = this.dvCardRecord[k]["f_Type"];
										}
										else
										{
											if (this.SetObjToStr(dataRow["f_Onduty2"]) != "")
											{
												break;
											}
											if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = this.dvCardRecord[k]["f_Type"];
												break;
											}
											if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = CommonStr.strLateness;
												break;
											}
											dataRow["f_Onduty2"] = dateTime4;
											dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
											break;
										}
									}
									k++;
								}
								else
								{
									if (!(this.SetObjToStr(dataRow["f_Onduty2"]) != ""))
									{
										dataRow["f_Onduty2"] = dateTime4;
										dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
										break;
									}
									break;
								}
							}
							if (this.SetObjToStr(dataRow["f_Offduty2"]) == "" && this.SetObjToStr(dataRow["f_Onduty2"]) == "")
							{
								dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
								dataRow["f_Offduty2Desc"] = CommonStr.strAbsence;
							}
						}
						else
						{
							dataRow["f_OnDuty1Desc"] = CommonStr.strAbsence;
							dataRow["f_OffDuty1Desc"] = CommonStr.strAbsence;
							dataRow["f_OnDuty2Desc"] = CommonStr.strAbsence;
							dataRow["f_OffDuty2Desc"] = CommonStr.strAbsence;
						}
						int num13 = 3;
						this.dvHoliday.RowFilter = " f_NO =1 ";
						if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
						{
							num13 = 0;
						}
						else
						{
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num13 = 1;
							}
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Saturday)
							{
								num13 = 2;
							}
							this.dvHoliday.RowFilter = " f_NO =2 ";
							if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 0 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
							{
								num13 = 0;
							}
							else
							{
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 1 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num13 = 1;
								}
								if (Convert.ToInt32(this.dvHoliday[0]["f_Value"]) == 2 && dateTime3.DayOfWeek == DayOfWeek.Sunday)
								{
									num13 = 2;
								}
								this.dvHoliday.RowFilter = " f_TYPE =2 ";
								for (int l = 0; l <= this.dvHoliday.Count - 1; l++)
								{
									this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
									DateTime t = DateTime.Parse(this.strTemp);
									this.strTemp = Convert.ToString(this.dvHoliday[l]["f_Value2"]);
									this.strTemp = this.strTemp + " " + ((this.dvHoliday[l]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
									DateTime t2 = DateTime.Parse(this.strTemp);
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num13 = 0;
										break;
									}
									if (t <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t2)
									{
										num13 = 2;
									}
									if (t <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t2)
									{
										num13 = 1;
									}
								}
							}
						}
						if (num13 != 3)
						{
							this.dvHoliday.RowFilter = " f_TYPE =3 ";
							for (int m = 0; m <= this.dvHoliday.Count - 1; m++)
							{
								this.strTemp = Convert.ToString(this.dvHoliday[m]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[m]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime t3 = DateTime.Parse(this.strTemp);
								this.strTemp = Convert.ToString(this.dvHoliday[m]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvHoliday[m]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime t4 = DateTime.Parse(this.strTemp);
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									num13 = 3;
									break;
								}
								if (t3 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t4)
								{
									if (num13 == 2)
									{
										num13 = 3;
									}
									else
									{
										num13 = 1;
									}
								}
								if (t3 <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t4)
								{
									if (num13 == 1)
									{
										num13 = 3;
									}
									else
									{
										num13 = 2;
									}
								}
							}
						}
						if (num13 == 0)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							}
							dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" || this.SetObjToStr(dataRow["f_Offduty1"]) != "" || this.SetObjToStr(dataRow["f_Onduty2"]) != "" || this.SetObjToStr(dataRow["f_Offduty2"]) != "")
							{
								if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
								{
									dataRow["f_OnDuty1Desc"] = CommonStr.strOvertime;
									dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
									if (Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
									else
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
								}
								if (this.SetObjToStr(dataRow["f_Onduty2"]) != "" && this.SetObjToStr(dataRow["f_Offduty2"]) != "")
								{
									dataRow["f_OnDuty2Desc"] = CommonStr.strOvertime;
									dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
									if (Strings.Format(dataRow["f_Offduty2"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
									else
									{
										dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
									}
								}
							}
							flag = false;
							flag2 = false;
						}
						else if (num13 == 1)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							}
							bool flag3 = false;
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								if (this.SetObjToStr(dataRow["f_OffDuty2"]) != "")
								{
									dataRow["f_OffDuty1"] = dataRow["f_OffDuty2"];
									dataRow["f_OffDuty1Desc"] = "";
									dataRow["f_OffDuty2"] = DBNull.Value;
									dataRow["f_OnDuty2"] = DBNull.Value;
									flag3 = true;
								}
								else if (this.SetObjToStr(dataRow["f_OnDuty2"]) != "")
								{
									dataRow["f_OffDuty1"] = dataRow["f_OnDuty2"];
									dataRow["f_OffDuty1Desc"] = "";
									dataRow["f_OnDuty2"] = DBNull.Value;
									dataRow["f_OffDuty2"] = DBNull.Value;
									flag3 = true;
								}
								if (flag3)
								{
									if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "")
									{
										dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
										dataRow["f_Offduty1Desc"] = DBNull.Value;
									}
									else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
									{
										if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
										{
											dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
										}
										else
										{
											dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
										}
									}
									else
									{
										dataRow["f_Offduty1Desc"] = DBNull.Value;
									}
								}
							}
							dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_Onduty2"]) != "" && this.SetObjToStr(dataRow["f_Offduty2"]) != "")
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strOvertime;
								dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
								if (Strings.Format(dataRow["f_Offduty2"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
								else
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty2"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OnDuty2Desc"] = CommonStr.strRest;
								dataRow["f_OffDuty2Desc"] = CommonStr.strRest;
							}
						}
						else if (num13 == 2)
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
							dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
							dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "")
								{
									dataRow["f_OnDuty2"] = dataRow["f_OnDuty1"];
									dataRow["f_OnDuty1"] = DBNull.Value;
									dataRow["f_OffDuty1"] = DBNull.Value;
								}
								else if (this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
								{
									dataRow["f_OnDuty2"] = dataRow["f_OffDuty1"];
									dataRow["f_OffDuty1"] = DBNull.Value;
								}
							}
							if (this.SetObjToStr(dataRow["f_Onduty1"]) != "" && this.SetObjToStr(dataRow["f_Offduty1"]) != "")
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strOvertime;
								dataRow["f_OffDuty1Desc"] = CommonStr.strOvertime;
								if (Strings.Format(dataRow["f_Offduty1"], "yyyy-MM-dd") == Strings.Format(dateTime3, "yyyy-MM-dd"))
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
								else
								{
									dataRow["f_OvertimeTime"] = Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture) + Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_Onduty1"], "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty1"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OnDuty1Desc"] = CommonStr.strRest;
								dataRow["f_OffDuty1Desc"] = CommonStr.strRest;
							}
						}
						else if (num13 == 3 && this.SetObjToStr(dataRow["f_Onduty2"]) != "" && this.SetObjToStr(dataRow["f_Offduty2"]) != "")
						{
							if (string.Compare(Strings.Format(dataRow["f_Offduty2"], "yyyy-MM-dd"), Strings.Format(dateTime3, "yyyy-MM-dd")) == 0)
							{
								if (string.Compare(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss"), Strings.Format(this.tOffduty2.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss")) >= 0 && string.Compare(Strings.Format(this.tOffduty2.AddMinutes((double)this.tOvertimeTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty2, "HH:mm:ss")) >= 0)
								{
									dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
									dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty2, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
								}
							}
							else
							{
								dataRow["f_OffDuty2Desc"] = CommonStr.strOvertime;
								dataRow["f_OvertimeTime"] = Conversion.Int(DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOffduty2, "2000-1-1 HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_Offduty2"], "2000-1-2 HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) / 30L) / 2.0m;
							}
						}
						if (this.dvLeave.Count > 0)
						{
							string value = "";
							string value2 = "";
							num13 = 3;
							for (int n = 0; n <= this.dvLeave.Count - 1; n++)
							{
								this.strTemp = Convert.ToString(this.dvLeave[n]["f_Value"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[n]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
								DateTime t5 = DateTime.Parse(this.strTemp);
								this.strTemp = Convert.ToString(this.dvLeave[n]["f_Value2"]);
								this.strTemp = this.strTemp + " " + ((this.dvLeave[n]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
								DateTime t6 = DateTime.Parse(this.strTemp);
								string text2 = Convert.ToString(this.dvLeave[n]["f_HolidayType"]);
								if (t5 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t6)
								{
									value = text2;
									value2 = text2;
									num13 = 0;
									break;
								}
								if (t5 <= dateTime3 && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:00")) <= t6)
								{
									value = text2;
									if (num13 == 1)
									{
										num13 = 0;
										break;
									}
									num13 = 2;
								}
								if (t5 <= DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 12:00:01")) && DateTime.Parse(Strings.Format(dateTime3, "yyyy-MM-dd 23:59:59")) <= t6)
								{
									value2 = text2;
									if (num13 == 2)
									{
										num13 = 0;
										break;
									}
									num13 = 1;
								}
							}
							bool flag4 = false;
							if (num13 == 0)
							{
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OnDuty2Desc"] = value2;
								dataRow["f_OffDuty1Desc"] = value;
								dataRow["f_OffDuty2Desc"] = value2;
								dataRow["f_OnDuty1"] = DBNull.Value;
								dataRow["f_OnDuty2"] = DBNull.Value;
								dataRow["f_OffDuty1"] = DBNull.Value;
								dataRow["f_OffDuty2"] = DBNull.Value;
							}
							else if (num13 == 1)
							{
								if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly || this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
								{
									if (this.SetObjToStr(dataRow["f_OffDuty2"]) != "")
									{
										dataRow["f_OffDuty1"] = dataRow["f_OffDuty2"];
										flag4 = true;
										dataRow["f_OffDuty2"] = DBNull.Value;
										dataRow["f_OnDuty2"] = DBNull.Value;
									}
									else if (this.SetObjToStr(dataRow["f_OnDuty2"]) != "")
									{
										dataRow["f_OffDuty1"] = dataRow["f_OnDuty2"];
										flag4 = true;
										dataRow["f_OnDuty2"] = DBNull.Value;
									}
									if (flag4)
									{
										if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "")
										{
											dataRow["f_Onduty1Desc"] = CommonStr.strNotReadCard;
											dataRow["f_Offduty1Desc"] = DBNull.Value;
										}
										else if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
										{
											if (string.Compare(Strings.Format(Convert.ToDateTime(dataRow["f_OffDuty1"]).AddMinutes((double)this.tLeaveAbsenceTimeout), "HH:mm:ss"), Strings.Format(this.tOffduty1, "HH:mm:ss")) < 0)
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strAbsence;
											}
											else
											{
												dataRow["f_Offduty1Desc"] = CommonStr.strLeaveEarly;
											}
										}
										else
										{
											dataRow["f_Offduty1Desc"] = DBNull.Value;
										}
									}
								}
								dataRow["f_OnDuty2Desc"] = value2;
								dataRow["f_OffDuty2Desc"] = value2;
								dataRow["f_OnDuty2"] = DBNull.Value;
								dataRow["f_OffDuty2"] = DBNull.Value;
							}
							else if (num13 == 2)
							{
								if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strNotReadCard || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strLateness || this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
								{
									if (this.SetObjToStr(dataRow["f_OnDuty1"]) != "")
									{
										dataRow["f_OnDuty2"] = dataRow["f_OnDuty1"];
										flag4 = true;
										dataRow["f_OnDuty1"] = DBNull.Value;
										dataRow["f_OffDuty1"] = DBNull.Value;
									}
									else if (this.SetObjToStr(dataRow["f_OffDuty1"]) != "")
									{
										flag4 = true;
										dataRow["f_OnDuty2"] = dataRow["f_OffDuty1"];
										dataRow["f_OffDuty1"] = DBNull.Value;
									}
									if (flag4)
									{
										if (this.SetObjToStr(dataRow["f_OffDuty2"]) == "")
										{
											dataRow["f_Offduty2Desc"] = CommonStr.strNotReadCard;
											dataRow["f_Onduty2Desc"] = DBNull.Value;
										}
										else
										{
											DateTime dateTime4 = Convert.ToDateTime(dataRow["f_OnDuty2"]);
											if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tLateTimeout), "HH:mm:ss")) <= 0)
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = DBNull.Value;
											}
											else if (string.Compare(Strings.Format(dateTime4, "HH:mm:ss"), Strings.Format(this.tOnduty2.AddMinutes((double)this.tlateAbsenceTimeout), "HH:mm:ss")) < 0)
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = CommonStr.strLateness;
											}
											else
											{
												dataRow["f_Onduty2"] = dateTime4;
												dataRow["f_Onduty2Desc"] = CommonStr.strAbsence;
											}
										}
									}
								}
								dataRow["f_OnDuty1Desc"] = value;
								dataRow["f_OffDuty1Desc"] = value;
								dataRow["f_OnDuty1"] = DBNull.Value;
								dataRow["f_OffDuty1"] = DBNull.Value;
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strLateness)
						{
							dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty1, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty1"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strLateness)
						{
							dataRow["f_LateTime"] = (long)Convert.ToInt32(dataRow["f_LateTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(this.tOnduty2, "HH:mm:ss")), DateTime.Parse(Strings.Format(dataRow["f_OnDuty2"], "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strLeaveEarly)
						{
							dataRow["f_LeaveEarlyTime"] = (long)Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OffDuty1"], "HH:mm:ss")), DateTime.Parse(Strings.Format(this.tOffduty1, "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strLeaveEarly)
						{
							dataRow["f_LeaveEarlyTime"] = (long)Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) + DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse(Strings.Format(dataRow["f_OffDuty2"], "HH:mm:ss")), DateTime.Parse(Strings.Format(this.tOffduty2, "HH:mm:ss")), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty1Desc"]).IndexOf(CommonStr.strAbsence) >= 0)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
							flag2 = false;
						}
						else
						{
							if (this.SetObjToStr(dataRow["f_OnDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
								flag2 = false;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty1Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLeaveAbsenceDay;
								flag2 = false;
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]).IndexOf(CommonStr.strAbsence) >= 0 && this.SetObjToStr(dataRow["f_OffDuty2Desc"]).IndexOf(CommonStr.strAbsence) >= 0)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
							flag2 = false;
						}
						else
						{
							if (this.SetObjToStr(dataRow["f_OnDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
								flag2 = false;
							}
							if (this.SetObjToStr(dataRow["f_OffDuty2Desc"]) == CommonStr.strAbsence)
							{
								dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) + this.tLeaveAbsenceDay;
								flag2 = false;
							}
						}
						if (Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) >= 1m && num13 != 3)
						{
							dataRow["f_AbsenceDay"] = Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) / 2.0m;
						}
						text = " INSERT INTO t_d_AttendenceData ";
						text += " ([f_ConsumerID], [f_AttDate], ";
						text += "[f_Onduty1],[f_Onduty1Desc], [f_Offduty1], [f_Offduty1Desc], ";
						text += "[f_Onduty2], [f_Onduty2Desc],[f_Offduty2], [f_Offduty2Desc]  ";
						text += ", [f_LateTime], [f_LeaveEarlyTime],[f_OvertimeTime], [f_AbsenceDay]  ";
						text += " ) ";
						text = text + " VALUES ( " + dataRow["f_ConsumerID"];
						text = text + " , " + this.PrepareStr(dataRow["f_AttDate"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty1Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty1Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Onduty2Desc"]);
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + " , " + this.PrepareStr(dataRow["f_Offduty2Desc"]);
						text = text + " , " + dataRow["f_LateTime"];
						text = text + " , " + dataRow["f_LeaveEarlyTime"];
						text = text + " , " + this.getDecimalStr(dataRow["f_OvertimeTime"]);
						text = text + " , " + this.getDecimalStr(dataRow["f_AbsenceDay"]);
						text += " ) ";
						using (SqlCommand sqlCommand = new SqlCommand(text, this.cn))
						{
							if (this.cn.State == ConnectionState.Closed)
							{
								this.cn.Open();
							}
							sqlCommand.ExecuteNonQuery();
						}
						if (flag)
						{
							num3++;
						}
						string text3 = "";
						for (j = 0; j <= 3; j++)
						{
							if (j == 0)
							{
								text3 = this.SetObjToStr(dataRow["f_OnDuty1Desc"]);
							}
							else if (j == 1)
							{
								text3 = this.SetObjToStr(dataRow["f_OnDuty2Desc"]);
							}
							else if (j == 2)
							{
								text3 = this.SetObjToStr(dataRow["f_OffDuty1Desc"]);
							}
							else if (j == 3)
							{
								text3 = this.SetObjToStr(dataRow["f_OffDuty2Desc"]);
							}
							if (text3 == CommonStr.strLateness)
							{
								num5++;
								flag2 = false;
							}
							else if (text3 == CommonStr.strLeaveEarly)
							{
								num6++;
								flag2 = false;
							}
							else if (text3 == CommonStr.strNotReadCard)
							{
								num9++;
								flag2 = false;
							}
							else
							{
								text3.IndexOf(CommonStr.strNotReadCard);
								int i = 0;
								while (i <= this.dtHolidayType.Rows.Count - 1 && i < array.Length)
								{
									if (text3 == this.dtHolidayType.Rows[i][1].ToString())
									{
										flag2 = false;
										array[i] += 0.25m;
										break;
									}
									i++;
								}
							}
						}
						if (this.SetObjToStr(dataRow["f_OnDuty1"]) == "" && this.SetObjToStr(dataRow["f_OffDuty1"]) == "" && this.SetObjToStr(dataRow["f_OnDuty2"]) == "" && this.SetObjToStr(dataRow["f_OffDuty2"]) == "")
						{
							flag2 = false;
						}
						num7 += Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture);
						num8 += Convert.ToDecimal(dataRow["f_OvertimeTime"], CultureInfo.InvariantCulture);
						num10 += Convert.ToInt32(dataRow["f_LateTime"]);
						num11 += Convert.ToInt32(dataRow["f_LeaveEarlyTime"]);
						if (Convert.ToInt32(dataRow["f_LateTime"]) != 0 || Convert.ToInt32(dataRow["f_LeaveEarlyTime"]) != 0 || !(Convert.ToDecimal(dataRow["f_AbsenceDay"], CultureInfo.InvariantCulture) == 0m))
						{
							flag2 = false;
						}
						if (flag2)
						{
							num4++;
						}
						dateTime3 = dateTime3.AddDays(1.0);
						Application.DoEvents();
					}
					this.dvCardRecord.RowFilter = string.Format("f_Type ={0}", this.PrepareStr(CommonStr.strSignIn));
					text = " Insert Into t_d_AttStatistic ";
					text += " ( [f_ConsumerID], [f_AttDateStart], [f_AttDateEnd] ";
					text += " , [f_DayShouldWork],  [f_DayRealWork]";
					text += " , [f_TotalLate],  [f_TotalLeaveEarly],[f_TotalOvertime], [f_TotalAbsenceDay], [f_TotalNotReadCard]";
					for (int i = 1; i <= 32; i++)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" , [f_SpecialType",
							i,
							"]"
						});
					}
					text += ", f_LateMinutes";
					text += ", f_LeaveEarlyMinutes";
					text += ", f_ManualReadTimesCount";
					text += " ) ";
					text = text + " Values( " + dataRow["f_ConsumerID"];
					text = text + " , " + this.PrepareStr(dateTime, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(dateTime2, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + num3;
					text = text + " , " + num4;
					text = text + " , " + num5;
					text = text + " , " + num6;
					text = text + " , " + this.getDecimalStr(num8);
					text = text + " , " + this.getDecimalStr(num7);
					text = text + " , " + num9;
					for (int i = 0; i <= 31; i++)
					{
						text = text + " , " + this.PrepareStr(this.getDecimalStr(array[i]));
					}
					text = text + ", " + num10;
					text = text + ", " + num11;
					text = text + ", " + this.dvCardRecord.Count;
					text += " )";
					using (SqlCommand sqlCommand2 = new SqlCommand(text, this.cn))
					{
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						sqlCommand2.ExecuteNonQuery();
					}
				}
				sqlDataReader.Close();
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				this.shiftAttReportImportFromAttendenceData();
				this.shiftAttStatisticImportFromAttStatistic();
				this.logCreateReport();
				if (this.CreateCompleteEvent != null)
				{
					this.CreateCompleteEvent(true, "");
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
				try
				{
					if (this.CreateCompleteEvent != null)
					{
						this.CreateCompleteEvent(false, ex.ToString());
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
