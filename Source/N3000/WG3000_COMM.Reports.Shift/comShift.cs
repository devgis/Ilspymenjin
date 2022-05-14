using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class comShift : Component
	{
		private const int ERR_NONE = 0;

		private const int ERR_FAIL = -1;

		private const int ERR_ID = -2;

		private const int ERR_READTIMES = -3;

		private const int ERR_TIMEDIFF = -4;

		private const int ERR_DAYDIFF = -5;

		private const int EER_INVILID_SHIFTID = -6;

		private const int EER_TIMEOVERLAPPED = -7;

		private const int EER_SQL_RUNFAIL = -999;

		public bool bStopCreate;

		private Container components;

		private string strTemp = "";

		public string errInfo = "";

		private int tLateTimeout;

		private decimal tLateAbsenceDay;

		private int tLeaveTimeout;

		private decimal tLeaveAbsenceDay;

		private int tOvertimeTimeout;

		private int tTwoReadMintime = 60;

		private int tAheadMinutesOnDutyFirst = 120;

		private int tAheadMinutes = 30;

		private int tDelayMinutes = 30;

		private int tOvertimeMinutes = 480;

		private int minShifDiffByMinute = 5;

		private DateTime realOnduty1;

		private DateTime realOffduty1;

		private DateTime realOnduty2;

		private DateTime realOffduty2;

		private DateTime realOnduty3;

		private DateTime realOffduty3;

		private DateTime realOnduty4;

		private DateTime realOffduty4;

		private DataTable dtShiftWork;

		private DataColumn dc;

		private DataTable dtCardRecord;

		private DataTable dtValidCardRecord;

		private SqlDataAdapter daCardRecord;

		private DataSet dsAtt = new DataSet();

		private SqlDataAdapter daLeave;

		private DataTable dtLeave;

		private SqlConnection cn;

		private SqlCommand cmd;

		private DataTable dtReport;

		private DataTable dtReport1;

		private DataTable dtHolidayType;

		private SqlDataAdapter daHolidayType;

		public comShift(IContainer Container) : this()
		{
			Container.Add(this);
		}

		public comShift()
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

		public static void localizedHolidayType(DataTable dt)
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

		public string errDesc(int errno)
		{
			string result;
			if (errno != -999)
			{
				switch (errno)
				{
				case -7:
					result = CommonStr.strTimeOverlapped;
					break;
				case -6:
					result = CommonStr.strArrangedInvalidShiftID;
					break;
				case -5:
					result = CommonStr.strShiftTimeOver24;
					break;
				case -4:
					result = CommonStr.strErrTimeDiff;
					break;
				case -3:
					result = CommonStr.strInvalidReadTimes;
					break;
				case -2:
					result = CommonStr.strInvalidShiftID;
					break;
				case -1:
					result = CommonStr.strFailed;
					break;
				default:
					result = CommonStr.strUnknown;
					break;
				}
			}
			else
			{
				result = CommonStr.strSqlRunFail;
			}
			return result;
		}

		public void getAttendenceParam()
		{
			this.tLateAbsenceDay = 0.5m;
			this.tLeaveAbsenceDay = 0.5m;
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			string cmdText = "SELECT * FROM t_a_Shift_Attendence";
			try
			{
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(cmdText, this.cn);
				SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
				while (sqlDataReader.Read())
				{
					if ((int)sqlDataReader["f_No"] == 1)
					{
						this.tLateTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
					else if ((int)sqlDataReader["f_No"] == 4)
					{
						this.tLeaveTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
					else if ((int)sqlDataReader["f_No"] == 7)
					{
						this.tOvertimeTimeout = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
					else if ((int)sqlDataReader["f_No"] == 17)
					{
						this.tAheadMinutesOnDutyFirst = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
					else if ((int)sqlDataReader["f_No"] == 18)
					{
						this.tAheadMinutes = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
					else if ((int)sqlDataReader["f_No"] == 19)
					{
						this.tDelayMinutes = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
					else if ((int)sqlDataReader["f_No"] == 20)
					{
						this.tOvertimeMinutes = Convert.ToInt32(sqlDataReader["f_Value"]);
					}
				}
				sqlDataReader.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
			}
		}

		public int tm(object dt)
		{
			int result = 0;
			try
			{
				result = int.Parse(Strings.Format((DateTime)dt, "HHmmss"));
			}
			catch (Exception)
			{
			}
			return result;
		}

		public int shift_checkvalid(int id, string name, int readtimes, object onduty1, object offduty1, object onduty2, object offduty2, object onduty3, object offduty3, object onduty4, object offduty4)
		{
			int result = -1;
			this.errInfo = "";
			try
			{
				if (id > 99)
				{
					result = -2;
					this.errInfo = id.ToString();
				}
				else if (readtimes != 2 && readtimes != 4 && readtimes != 6 && readtimes != 8)
				{
					result = -3;
					this.errInfo = -3.ToString();
				}
				else
				{
					int num = 0;
					this.realOnduty1 = DateTime.Parse(Strings.Format(onduty1, "2000-1-1 HH:mm:ss"));
					if (this.tm(onduty1) > this.tm(offduty1))
					{
						num++;
					}
					this.realOffduty1 = DateTime.Parse(Strings.Format(offduty1, "2000-1-1 HH:mm:ss")).AddDays((double)num);
					int num2 = (int)this.realOffduty1.Subtract(this.realOnduty1).TotalMinutes;
					if (num2 < this.minShifDiffByMinute)
					{
						result = -4;
					}
					else if (readtimes <= 2)
					{
						result = 0;
					}
					else
					{
						if (this.tm(offduty1) > this.tm(onduty2))
						{
							num++;
						}
						this.realOnduty2 = DateTime.Parse(Strings.Format(onduty2, "2000-1-1 HH:mm:ss")).AddDays((double)num);
						num2 = (int)this.realOnduty2.Subtract(this.realOffduty1).TotalMinutes;
						if (num2 < this.minShifDiffByMinute)
						{
							result = -4;
						}
						else
						{
							if (this.tm(onduty2) > this.tm(offduty2))
							{
								num++;
							}
							this.realOffduty2 = DateTime.Parse(Strings.Format(offduty2, "2000-1-1 HH:mm:ss")).AddDays((double)num);
							num2 = (int)this.realOffduty2.Subtract(this.realOnduty2).TotalMinutes;
							if (num2 < this.minShifDiffByMinute)
							{
								result = -4;
							}
							else
							{
								num2 = (int)this.realOffduty2.Subtract(this.realOnduty1).TotalMinutes;
								if (num2 >= 1440)
								{
									result = -5;
								}
								else if (readtimes <= 4)
								{
									result = 0;
								}
								else
								{
									if (this.tm(offduty2) > this.tm(onduty3))
									{
										num++;
									}
									this.realOnduty3 = DateTime.Parse(Strings.Format(onduty3, "2000-1-1 HH:mm:ss")).AddDays((double)num);
									num2 = (int)this.realOnduty3.Subtract(this.realOffduty2).TotalMinutes;
									if (num2 < this.minShifDiffByMinute)
									{
										result = -4;
									}
									else
									{
										if (this.tm(onduty3) > this.tm(offduty3))
										{
											num++;
										}
										this.realOffduty3 = DateTime.Parse(Strings.Format(offduty3, "2000-1-1 HH:mm:ss")).AddDays((double)num);
										num2 = (int)this.realOffduty3.Subtract(this.realOnduty3).TotalMinutes;
										if (num2 < this.minShifDiffByMinute)
										{
											result = -4;
										}
										else
										{
											num2 = (int)this.realOffduty3.Subtract(this.realOnduty1).TotalMinutes;
											if (num2 >= 1440)
											{
												result = -5;
											}
											else if (readtimes <= 6)
											{
												result = 0;
											}
											else
											{
												if (this.tm(offduty3) > this.tm(onduty4))
												{
													num++;
												}
												this.realOnduty4 = DateTime.Parse(Strings.Format(onduty4, "2000-1-1 HH:mm:ss")).AddDays((double)num);
												num2 = (int)this.realOnduty4.Subtract(this.realOffduty3).TotalMinutes;
												if (num2 < this.minShifDiffByMinute)
												{
													result = -4;
												}
												else
												{
													if (this.tm(onduty4) > this.tm(offduty4))
													{
														num++;
													}
													this.realOffduty4 = DateTime.Parse(Strings.Format(offduty4, "2000-1-1 HH:mm:ss")).AddDays((double)num);
													num2 = (int)this.realOffduty4.Subtract(this.realOnduty4).TotalMinutes;
													if (num2 < this.minShifDiffByMinute)
													{
														result = -4;
													}
													else
													{
														num2 = (int)this.realOffduty4.Subtract(this.realOnduty1).TotalMinutes;
														if (num2 >= 1440)
														{
															result = -5;
														}
														else if (readtimes <= 8)
														{
															result = 0;
														}
													}
												}
											}
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
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_add(int id, string name, int readtimes, DateTime onduty1, DateTime offduty1, DateTime onduty2, DateTime offduty2, DateTime onduty3, DateTime offduty3, DateTime onduty4, DateTime offduty4, int bOvertimeShift)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			int num = -1;
			this.errInfo = "";
			try
			{
				num = this.shift_checkvalid(id, name, readtimes, onduty1, offduty1, onduty2, offduty2, onduty3, offduty3, onduty4, offduty4);
				if (num != 0)
				{
					int result = num;
					return result;
				}
				string text = " SELECT count(*) FROM t_b_ShiftSet WHERE f_ShiftID = " + id;
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(text, this.cn);
				int num2 = Convert.ToInt32(this.cmd.ExecuteScalar());
				this.cn.Close();
				if (num2 > 0)
				{
					num = -2;
					this.errInfo = id.ToString();
					int result = num;
					return result;
				}
				text = " INSERT INTO t_b_ShiftSet ( f_ShiftID, f_ShiftName, f_ReadTimes,  f_OnDuty1, f_OffDuty1, f_OnDuty2, f_OffDuty2, f_OnDuty3, f_OffDuty3, f_OnDuty4, f_OffDuty4,f_bOvertimeShift, f_Notes)";
				text = text + " VALUES ( " + id;
				text = text + " , " + this.PrepareStr(name);
				text = text + " , " + readtimes;
				text = text + " , " + this.PrepareStr(this.realOnduty1, true, "yyyy-MM-dd HH:mm:ss");
				text = text + " , " + this.PrepareStr(this.realOffduty1, true, "yyyy-MM-dd HH:mm:ss");
				if (readtimes > 2)
				{
					text = text + " , " + this.PrepareStr(this.realOnduty2, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(this.realOffduty2, true, "yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = text + " , " + this.PrepareStr("");
					text = text + " , " + this.PrepareStr("");
				}
				if (readtimes > 4)
				{
					text = text + " , " + this.PrepareStr(this.realOnduty3, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(this.realOffduty3, true, "yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = text + " , " + this.PrepareStr("");
					text = text + " , " + this.PrepareStr("");
				}
				if (readtimes > 6)
				{
					text = text + " , " + this.PrepareStr(this.realOnduty4, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , " + this.PrepareStr(this.realOffduty4, true, "yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = text + " , " + this.PrepareStr("");
					text = text + " , " + this.PrepareStr("");
				}
				text = text + " , " + bOvertimeShift;
				text = text + " , " + this.PrepareStr("");
				text += " )";
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(text, this.cn);
				num2 = this.cmd.ExecuteNonQuery();
				this.cn.Close();
				if (num2 > 0)
				{
					num = 0;
				}
				else
				{
					num = -1;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return num;
		}

		public int shift_update(int id, string name, int readtimes, object onduty1, object offduty1, object onduty2, object offduty2, object onduty3, object offduty3, object onduty4, object offduty4, int bOvertimeShift)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			int num = -1;
			this.errInfo = "";
			try
			{
				num = this.shift_checkvalid(id, name, readtimes, onduty1, offduty1, onduty2, offduty2, onduty3, offduty3, onduty4, offduty4);
				if (num != 0)
				{
					int result = num;
					return result;
				}
				string text = " SELECT count(*) FROM t_b_ShiftSet WHERE f_ShiftID = " + id;
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(text, this.cn);
				int num2 = Convert.ToInt32(this.cmd.ExecuteScalar());
				this.cn.Close();
				if (num2 == 0)
				{
					num = -2;
					this.errInfo = id.ToString();
					int result = num;
					return result;
				}
				text = " UPDATE t_b_ShiftSet Set ";
				text = text + " f_ShiftName = " + this.PrepareStr(name);
				text = text + " , f_ReadTimes = " + readtimes;
				text = text + " , f_OnDuty1 = " + this.PrepareStr(this.realOnduty1, true, "yyyy-MM-dd HH:mm:ss");
				text = text + " , f_OffDuty1 = " + this.PrepareStr(this.realOffduty1, true, "yyyy-MM-dd HH:mm:ss");
				if (readtimes > 2)
				{
					text = text + " , f_OnDuty2 = " + this.PrepareStr(this.realOnduty2, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , f_OffDuty2 = " + this.PrepareStr(this.realOffduty2, true, "yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = text + " , f_OnDuty2 = " + this.PrepareStr("");
					text = text + " , f_OffDuty2 = " + this.PrepareStr("");
				}
				if (readtimes > 4)
				{
					text = text + " , f_OnDuty3 = " + this.PrepareStr(this.realOnduty3, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , f_OffDuty3 =" + this.PrepareStr(this.realOffduty3, true, "yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = text + " , f_OnDuty3 = " + this.PrepareStr("");
					text = text + " , f_OffDuty3 =" + this.PrepareStr("");
				}
				if (readtimes > 6)
				{
					text = text + " , f_OnDuty4  = " + this.PrepareStr(this.realOnduty4, true, "yyyy-MM-dd HH:mm:ss");
					text = text + " , f_OffDuty4 = " + this.PrepareStr(this.realOffduty4, true, "yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					text = text + " , f_OnDuty4  = " + this.PrepareStr("");
					text = text + " , f_OffDuty4 = " + this.PrepareStr("");
				}
				text = text + " ,f_bOvertimeShift = " + bOvertimeShift;
				text = text + " ,f_Notes = " + this.PrepareStr("");
				text = text + " WHERE  f_ShiftID = " + id;
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(text, this.cn);
				num2 = this.cmd.ExecuteNonQuery();
				this.cn.Close();
				if (num2 > 0)
				{
					num = 0;
				}
				else
				{
					num = -1;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return num;
		}

		public int shift_delete(int id)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			int result = -1;
			try
			{
				string cmdText = " DELETE FROM t_b_ShiftSet WHERE f_ShiftID = " + id;
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(cmdText, this.cn);
				this.cmd.ExecuteNonQuery();
				this.cn.Close();
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int shift_rule_checkValid(int ruleLen, int[] shiftRule)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.errInfo = "";
			int num = -1;
			if (ruleLen > 1)
			{
				object[,] array = new object[ruleLen + 1, 13];
				num = -1;
				try
				{
					for (int i = 0; i <= ruleLen - 1; i++)
					{
						if (this.bStopCreate)
						{
							int result = num;
							return result;
						}
						string cmdText = " SELECT * FROM t_b_ShiftSet WHERE f_ShiftID = " + shiftRule[i];
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new SqlCommand(cmdText, this.cn);
						SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
						if (sqlDataReader.Read())
						{
							for (int j = 0; j <= sqlDataReader.FieldCount - 1; j++)
							{
								array[i, j] = sqlDataReader[j];
							}
						}
						else
						{
							array[i, 0] = 0;
							if (shiftRule[i] != 0)
							{
								num = -6;
								this.errInfo = shiftRule[i].ToString();
								int result = num;
								return result;
							}
						}
						sqlDataReader.Close();
					}
					this.cn.Close();
					for (int i = 1; i <= ruleLen - 1; i++)
					{
						if (this.bStopCreate)
						{
							int result = num;
							return result;
						}
						if (Convert.ToInt32(array[i - 1, 0]) != 0 && Convert.ToInt32(array[i, 0]) != 0)
						{
							int num2 = Convert.ToInt32(array[i - 1, 2]);
							DateTime dateTime = Convert.ToDateTime(array[i - 1, 2 + num2]);
							Convert.ToInt32(array[i, 2]);
							DateTime dateTime2 = Convert.ToDateTime(array[i, 3]).AddDays(1.0);
							if (!(dateTime2 > dateTime) || dateTime2.Subtract(dateTime).TotalMinutes < (double)this.minShifDiffByMinute)
							{
								num = -7;
								this.errInfo = string.Concat(new object[]
								{
									CommonStr.strShift,
									shiftRule[i - 1],
									CommonStr.strLastOffDuty,
									Strings.Format(dateTime, "HH:mm"),
									", "
								});
								object obj = this.errInfo;
								this.errInfo = string.Concat(new object[]
								{
									obj,
									CommonStr.strShift,
									shiftRule[i],
									CommonStr.strFirstOnDuty,
									Strings.Format(dateTime2, "HH:mm"),
									CommonStr.strTimeOverlapped
								});
								int result = num;
								return result;
							}
						}
					}
					num = 0;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
				}
				finally
				{
					if (this.cn.State == ConnectionState.Open)
					{
						this.cn.Close();
					}
				}
				return num;
			}
			num = 0;
			return num;
		}

		public int shift_arrangeByRule(int consumerId, DateTime dateStart, DateTime dateEnd, int ruleLen, int[] shiftRule)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			bool flag = false;
			object[] array = new object[37];
			this.errInfo = "";
			int result = -1;
			DateTime dateTime = DateTime.Parse(Strings.Format(dateStart, "yyyy-MM-dd 12:00:00"));
			DateTime t = DateTime.Parse(Strings.Format(dateEnd, "yyyy-MM-dd 12:00:00"));
			if (dateTime > t)
			{
				return result;
			}
			try
			{
				string text = "";
				int num = 0;
				DateTime dateTime2 = dateTime;
				string text2;
				while (true)
				{
					if (text != Strings.Format(dateTime2, "yyyy-MM"))
					{
						text = Strings.Format(dateTime2, "yyyy-MM");
						text2 = " SELECT * FROM t_d_ShiftData ";
						text2 = text2 + " WHERE f_ConsumerID = " + consumerId;
						text2 = text2 + " AND f_DateYM = " + this.PrepareStr(text);
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new SqlCommand(text2, this.cn);
						SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
						if (sqlDataReader.Read())
						{
							flag = true;
							for (int i = 0; i <= sqlDataReader.FieldCount - 1; i++)
							{
								array[i] = sqlDataReader[i];
							}
						}
						else
						{
							int num2 = DateTime.DaysInMonth(dateTime2.Year, dateTime2.Month);
							flag = false;
							array[1] = consumerId;
							array[2] = this.PrepareStr(text);
							for (int j = 1; j <= 31; j++)
							{
								if (j <= num2)
								{
									array[j + 2] = -1;
								}
								else
								{
									array[j + 2] = -2;
								}
							}
						}
						sqlDataReader.Close();
					}
					do
					{
						array[2 + dateTime2.Day] = shiftRule[num];
						num++;
						if (num >= ruleLen)
						{
							num = 0;
						}
						dateTime2 = dateTime2.AddDays(1.0);
					}
					while (!(text != Strings.Format(dateTime2, "yyyy-MM")) && !(dateTime2 > t));
					if (flag)
					{
						text2 = "  UPDATE t_d_ShiftData SET ";
						int k = 1;
						object obj = text2;
						text2 = string.Concat(new object[]
						{
							obj,
							" f_ShiftID_",
							k.ToString().PadLeft(2, '0'),
							" = ",
							array[2 + k]
						});
						for (k = 2; k <= 31; k++)
						{
							object obj2 = text2;
							text2 = string.Concat(new object[]
							{
								obj2,
								" , f_ShiftID_",
								k.ToString().PadLeft(2, '0'),
								" = ",
								array[2 + k]
							});
						}
						text2 = text2 + " , f_LogDate  = " + this.PrepareStr(DateTime.Now, true, "yyyy-MM-dd HH:mm:ss");
						text2 = text2 + " , f_Notes  = " + this.PrepareStr("");
						text2 = text2 + " WHERE f_RecID = " + array[0];
					}
					else
					{
						text2 = "  INSERT INTO t_d_ShiftData  ";
						text2 += " ( f_ConsumerID , f_DateYM  ";
						for (int l = 1; l <= 31; l++)
						{
							text2 = text2 + " , f_ShiftID_" + l.ToString().PadLeft(2, '0');
						}
						text2 += " , f_Notes   ";
						text2 += " ) ";
						text2 = text2 + " Values ( " + array[1];
						text2 = text2 + " , " + array[2];
						for (int l = 1; l <= 31; l++)
						{
							text2 = text2 + " , " + array[2 + l];
						}
						text2 = text2 + "  , " + this.PrepareStr("");
						text2 += " ) ";
					}
					if (this.cn.State != ConnectionState.Open)
					{
						this.cn.Open();
					}
					this.cmd = new SqlCommand(text2, this.cn);
					int num3 = this.cmd.ExecuteNonQuery();
					if (num3 <= 0)
					{
						break;
					}
					if (dateTime2 > t)
					{
						goto Block_17;
					}
				}
				result = -999;
				this.errInfo = text2;
				return result;
				Block_17:
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int shift_arrange_update(int consumerId, DateTime dateShift, int shiftID)
		{
			int result = 0;
			int[] array = new int[1];
			try
			{
				array[0] = shiftID;
				result = this.shift_arrangeByRule(consumerId, dateShift, dateShift, 1, array);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_arrange_delete(int consumerId, DateTime dateStart, DateTime dateEnd)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			bool flag = false;
			object[] array = new object[37];
			this.errInfo = "";
			int result = -1;
			if (consumerId == 0)
			{
				string text = " DELETE FROM t_d_ShiftData ";
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new SqlCommand(text, this.cn);
				int num = this.cmd.ExecuteNonQuery();
				if (num < 0)
				{
					result = -999;
					this.errInfo = text;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			DateTime dateTime = DateTime.Parse(Strings.Format(dateStart, "yyyy-MM-dd 12:00:00"));
			DateTime t = DateTime.Parse(Strings.Format(dateEnd, "yyyy-MM-dd 12:00:00"));
			if (dateTime > t)
			{
				return result;
			}
			try
			{
				string text2 = "";
				DateTime dateTime2 = dateTime;
				string text;
				while (true)
				{
					if (text2 != Strings.Format(dateTime2, "yyyy-MM"))
					{
						text2 = Strings.Format(dateTime2, "yyyy-MM");
						text = " SELECT * FROM t_d_ShiftData ";
						text = text + " WHERE f_ConsumerID = " + consumerId;
						text = text + " AND f_DateYM = " + this.PrepareStr(text2);
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new SqlCommand(text, this.cn);
						SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
						if (sqlDataReader.Read())
						{
							flag = true;
							for (int i = 0; i <= sqlDataReader.FieldCount - 1; i++)
							{
								array[i] = sqlDataReader[i];
							}
						}
						else
						{
							flag = false;
						}
						sqlDataReader.Close();
					}
					do
					{
						array[2 + dateTime2.Day] = -1;
						dateTime2 = dateTime2.AddDays(1.0);
					}
					while (!(text2 != Strings.Format(dateTime2, "yyyy-MM")) && !(dateTime2 > t));
					if (flag)
					{
						bool flag2 = true;
						for (int j = 1; j <= 31; j++)
						{
							if (Convert.ToInt32(array[2 + j]) > -1)
							{
								flag2 = false;
								break;
							}
						}
						if (flag2)
						{
							text = "  DELETE FROM t_d_ShiftData ";
							text = text + " WHERE f_RecID = " + array[0];
						}
						else
						{
							text = "  UPDATE t_d_ShiftData SET ";
							int j = 1;
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								" f_ShiftID_",
								j.ToString().PadLeft(2, '0'),
								" = ",
								array[2 + j]
							});
							for (j = 2; j <= 31; j++)
							{
								object obj2 = text;
								text = string.Concat(new object[]
								{
									obj2,
									" , f_ShiftID_",
									j.ToString().PadLeft(2, '0'),
									" = ",
									array[2 + j]
								});
							}
							text = text + " , f_LogDate  = " + this.PrepareStr(DateTime.Now, true, "yyyy-MM-dd HH:mm:ss");
							text = text + " , f_Notes  = " + this.PrepareStr("");
							text = text + " WHERE f_RecID = " + array[0];
						}
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new SqlCommand(text, this.cn);
						int num = this.cmd.ExecuteNonQuery();
						if (num <= 0)
						{
							break;
						}
					}
					if (dateTime2 > t)
					{
						goto Block_18;
					}
				}
				result = -999;
				this.errInfo = text;
				return result;
				Block_18:
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int shift_work_schedule_create(out DataTable dtShiftWorkSchedule)
		{
			this.dtShiftWork = new DataTable("t_d_ShiftWork");
			int result = -1;
			dtShiftWorkSchedule = null;
			try
			{
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ConsumerID";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_ShiftDate";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ShiftID";
				this.dc.DefaultValue = -1;
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_Readtimes";
				this.dc.DefaultValue = 0;
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_PlanTime";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_TimeSeg";
				this.dc.DefaultValue = 0;
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_WorkTime";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.String");
				this.dc.ColumnName = "f_AttDesc";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.String");
				this.dc.ColumnName = "f_CardRecordDesc";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_Duration";
				this.dc.DefaultValue = 0;
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_bOvertimeShift";
				this.dc.DefaultValue = 0;
				this.dtShiftWork.Columns.Add(this.dc);
				dtShiftWorkSchedule = this.dtShiftWork.Copy();
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_work_schedule_fill(int consumerid, DataTable dtShiftWorkSchedule, DateTime dateStart, DateTime dateEnd, ref int bNotArranged)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			bool flag = false;
			object[] array = new object[37];
			this.errInfo = "";
			int result = -1;
			DateTime dateTime = DateTime.Parse(Strings.Format(dateStart, "yyyy-MM-dd 12:00:00"));
			DateTime t = DateTime.Parse(Strings.Format(dateEnd, "yyyy-MM-dd 12:00:00"));
			if (dateTime > t)
			{
				return result;
			}
			try
			{
				string text = "";
				DateTime dateTime2 = dateTime;
				do
				{
					if (text != Strings.Format(dateTime2, "yyyy-MM"))
					{
						text = Strings.Format(dateTime2, "yyyy-MM");
						string text2 = " SELECT * FROM t_d_ShiftData ";
						text2 = text2 + " WHERE f_ConsumerID = " + consumerid;
						text2 = text2 + " AND f_DateYM = " + this.PrepareStr(text);
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new SqlCommand(text2, this.cn);
						SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
						if (sqlDataReader.Read())
						{
							flag = true;
							for (int i = 0; i <= sqlDataReader.FieldCount - 1; i++)
							{
								array[i] = sqlDataReader[i];
							}
						}
						else
						{
							bNotArranged |= 1;
							flag = false;
							array[0] = -1;
						}
						sqlDataReader.Close();
					}
					do
					{
						if (!flag)
						{
							DataRow dataRow = dtShiftWorkSchedule.NewRow();
							dataRow[0] = consumerid;
							dataRow[1] = dateTime2;
							dataRow["f_ShiftID"] = -1;
							dataRow["f_Readtimes"] = 0;
							dataRow["f_Duration"] = 0;
							dtShiftWorkSchedule.Rows.Add(dataRow);
						}
						else
						{
							int num = Convert.ToInt32(array[2 + dateTime2.Day]);
							if (num <= 0)
							{
								DataRow dataRow = dtShiftWorkSchedule.NewRow();
								dataRow[0] = consumerid;
								dataRow[1] = dateTime2;
								dataRow[2] = num;
								dataRow["f_Readtimes"] = 0;
								dataRow["f_Duration"] = 0;
								if (num == 0)
								{
									dataRow["f_AttDesc"] = CommonStr.strRest;
								}
								if (num == -1)
								{
									bNotArranged |= 2;
								}
								dtShiftWorkSchedule.Rows.Add(dataRow);
							}
							else
							{
								string text2 = "SELECT * FROM t_b_ShiftSet WHERE f_ShiftID = " + num;
								this.cmd = new SqlCommand(text2, this.cn);
								if (this.cn.State != ConnectionState.Open)
								{
									this.cn.Open();
								}
								SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
								if (sqlDataReader.Read())
								{
									for (int j = 1; j <= Convert.ToInt32(sqlDataReader["f_ReadTimes"]); j++)
									{
										DataRow dataRow = dtShiftWorkSchedule.NewRow();
										dataRow["f_Readtimes"] = 0;
										dataRow[0] = consumerid;
										dataRow[1] = dateTime2;
										dataRow[2] = num;
										dataRow["f_Readtimes"] = sqlDataReader["f_ReadTimes"];
										dataRow["f_PlanTime"] = Convert.ToDateTime(Strings.Format(dateTime2.AddDays((double)(Convert.ToInt32(Strings.Format(sqlDataReader[j + 2], "dd")) - 1)), "yyyy-MM-dd") + Strings.Format(sqlDataReader[j + 2], " HH:mm:ss"));
										dataRow["f_TimeSeg"] = j - 1;
										dataRow["f_Duration"] = 0;
										dataRow["f_bOvertimeShift"] = sqlDataReader["f_bOvertimeShift"];
										dtShiftWorkSchedule.Rows.Add(dataRow);
									}
								}
								else
								{
									DataRow dataRow = dtShiftWorkSchedule.NewRow();
									dataRow[0] = consumerid;
									dataRow[1] = dateTime2;
									dataRow[2] = num;
									dataRow[3] = -1;
									dataRow["f_Readtimes"] = -1;
									dataRow["f_Duration"] = 0;
									dtShiftWorkSchedule.Rows.Add(dataRow);
									bNotArranged |= 4;
								}
								sqlDataReader.Close();
							}
						}
						dateTime2 = dateTime2.AddDays(1.0);
					}
					while (!(text != Strings.Format(dateTime2, "yyyy-MM")) && !(dateTime2 > t));
				}
				while (!(dateTime2 > t));
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int shift_work_schedule_updatebyReadcard(int consumerid, DataTable dtShiftWorkSchedule, DateTime dateStart, DateTime dateEnd)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dsAtt = new DataSet();
			this.errInfo = "";
			int num = -1;
			DateTime t = DateTime.Parse(Strings.Format(dateStart, "yyyy-MM-dd 12:00:00"));
			DateTime t2 = DateTime.Parse(Strings.Format(dateEnd, "yyyy-MM-dd 12:00:00"));
			if (t > t2)
			{
				return num;
			}
			try
			{
				string text = "SELECT t_d_SwipeRecord.f_ConsumerID, t_d_SwipeRecord.f_ReaderID, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_DutyOnOff  FROM t_d_SwipeRecord, t_b_Reader ";
				text = text + " WHERE f_ConsumerID = " + consumerid;
				text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(dateStart, true, "yyyy-MM-dd 00:00:00") + ") ";
				text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(dateEnd.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
				text += " AND (t_d_SwipeRecord.f_ReaderID = t_b_Reader.f_ReaderID) ";
				text += " AND t_b_Reader.f_Attend = 1 ";
				if (wgAppConfig.getSystemParamByNO(54) == "1")
				{
					text += " AND f_Character >= 1 ";
				}
				text += " ORDER BY f_ReadDate ASC, f_RecID ASC ";
				this.cmd = new SqlCommand();
				this.cmd.CommandTimeout = wgAppConfig.dbCommandTimeout;
				this.cmd.Connection = this.cn;
				this.cmd.CommandText = text;
				this.cmd.CommandType = CommandType.Text;
				this.daCardRecord = new SqlDataAdapter(this.cmd);
				this.daCardRecord.Fill(this.dsAtt, "CardRecord");
				this.dtCardRecord = this.dsAtt.Tables["CardRecord"];
				this.daCardRecord = new SqlDataAdapter("SELECT t_d_SwipeRecord.f_ConsumerID, t_d_SwipeRecord.f_ReaderID, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_DutyOnOff FROM t_d_SwipeRecord, t_b_Reader WHERE 1<0  ", this.cn);
				this.daCardRecord.Fill(this.dsAtt, "ValidCardRecord");
				this.dtValidCardRecord = this.dsAtt.Tables["ValidCardRecord"];
				if (this.dtCardRecord.Rows.Count > 0)
				{
					object[] array = new object[this.dtCardRecord.Columns.Count - 1 + 1];
					DateTime value = Convert.ToDateTime(this.dtCardRecord.Rows[0]["f_ReadDate"]);
					int num2 = Convert.ToInt32(this.dtCardRecord.Rows[0]["f_DutyOnOff"]);
					DataRow dataRow = this.dtValidCardRecord.NewRow();
					this.dtCardRecord.Rows[0].ItemArray.CopyTo(array, 0);
					dataRow.ItemArray = array;
					this.dtValidCardRecord.Rows.Add(dataRow);
					value = Convert.ToDateTime(dataRow["f_ReadDate"]);
					num2 = Convert.ToInt32(dataRow["f_DutyOnOff"]);
					for (int i = 1; i <= this.dtCardRecord.Rows.Count - 1; i++)
					{
						if (this.bStopCreate)
						{
							int result = num;
							return result;
						}
						DateTime dateTime = Convert.ToDateTime(this.dtCardRecord.Rows[i]["f_ReadDate"]);
						TimeSpan timeSpan = dateTime.Subtract(value);
						int num3 = Convert.ToInt32(this.dtCardRecord.Rows[i]["f_DutyOnOff"]);
						if (timeSpan.TotalSeconds > (double)this.tTwoReadMintime || num3 != num2)
						{
							value = dateTime;
							num2 = num3;
							dataRow = this.dtValidCardRecord.NewRow();
							this.dtCardRecord.Rows[i].ItemArray.CopyTo(array, 0);
							dataRow.ItemArray = array;
							this.dtValidCardRecord.Rows.Add(dataRow);
						}
					}
				}
				int j = 0;
				for (int k = 0; k <= dtShiftWorkSchedule.Rows.Count - 1; k++)
				{
					if (this.bStopCreate)
					{
						int result = num;
						return result;
					}
					object obj = dtShiftWorkSchedule.Rows[k]["f_PlanTime"];
					if (!Information.IsDBNull(obj))
					{
						int num4 = Convert.ToInt32(dtShiftWorkSchedule.Rows[k]["f_TimeSeg"]);
						int num5 = Convert.ToInt32(dtShiftWorkSchedule.Rows[k]["f_ReadTimes"]);
						while (j < this.dtValidCardRecord.Rows.Count)
						{
							object value2 = this.dtValidCardRecord.Rows[j]["f_ReadDate"];
							TimeSpan timeSpan2 = Convert.ToDateTime(obj).Subtract(Convert.ToDateTime(value2));
							if (num4 == 0)
							{
								if (timeSpan2.TotalMinutes > (double)this.tAheadMinutesOnDutyFirst)
								{
									j++;
								}
								else
								{
									if (timeSpan2.TotalMinutes <= (double)(-(double)this.tDelayMinutes))
									{
										break;
									}
									if ((Convert.ToInt32(this.dtValidCardRecord.Rows[j]["f_DutyOnOff"]) & 1) == 1)
									{
										dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
										break;
									}
									j++;
								}
							}
							if (num4 == num5 - 1)
							{
								if (timeSpan2.TotalMinutes < (double)(-(double)this.tOvertimeMinutes))
								{
									break;
								}
								if (timeSpan2.TotalMinutes > (double)this.tAheadMinutes)
								{
									j++;
								}
								else if (timeSpan2.TotalMinutes > (double)(-(double)this.tDelayMinutes))
								{
									if ((Convert.ToInt32(this.dtValidCardRecord.Rows[j]["f_DutyOnOff"]) & 2) == 2)
									{
										dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
										j++;
									}
									else
									{
										j++;
									}
								}
								else
								{
									if ((k + 1 < dtShiftWorkSchedule.Rows.Count - 1 && !Information.IsDBNull(dtShiftWorkSchedule.Rows[k + 1]["f_PlanTime"]) && Convert.ToDateTime(dtShiftWorkSchedule.Rows[k + 1]["f_PlanTime"]).AddMinutes((double)(-(double)this.tAheadMinutesOnDutyFirst)) < Convert.ToDateTime(value2)) || timeSpan2.TotalMinutes < (double)(-(double)this.tOvertimeMinutes))
									{
										break;
									}
									if ((Convert.ToInt32(this.dtValidCardRecord.Rows[j]["f_DutyOnOff"]) & 2) == 2)
									{
										dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
										j++;
									}
									else
									{
										j++;
									}
								}
							}
							if (num4 > 0 && num4 < num5 - 1)
							{
								if (timeSpan2.TotalMinutes <= (double)this.tAheadMinutes)
								{
									if (timeSpan2.TotalMinutes < (double)(-(double)this.tDelayMinutes))
									{
										break;
									}
									if ((num4 & 1) == 0)
									{
										if ((Convert.ToInt32(this.dtValidCardRecord.Rows[j]["f_DutyOnOff"]) & 1) == 1)
										{
											dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
											j++;
											break;
										}
										j++;
									}
									else if ((Convert.ToInt32(this.dtValidCardRecord.Rows[j]["f_DutyOnOff"]) & 2) == 2)
									{
										dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
										j++;
										if (timeSpan2.TotalMinutes <= 0.0)
										{
											break;
										}
									}
									else
									{
										j++;
									}
								}
								else
								{
									j++;
								}
							}
						}
					}
				}
				num = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return num;
		}

		public int shift_work_schedule_updatebyManualReadcard(int consumerid, DataTable dtShiftWorkSchedule, DateTime dateStart, DateTime dateEnd)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dsAtt = new DataSet();
			this.errInfo = "";
			int num = -1;
			DateTime t = DateTime.Parse(Strings.Format(dateStart, "yyyy-MM-dd 12:00:00"));
			DateTime t2 = DateTime.Parse(Strings.Format(dateEnd, "yyyy-MM-dd 12:00:00"));
			if (t > t2)
			{
				return num;
			}
			try
			{
				string text = "SELECT t_d_ManualCardRecord.f_ConsumerID,  t_d_ManualCardRecord.f_ReadDate, t_d_ManualCardRecord.f_DutyOnOff FROM t_d_ManualCardRecord ";
				text = text + " WHERE f_ConsumerID = " + consumerid;
				text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(dateStart, true, "yyyy-MM-dd 00:00:00") + ") ";
				text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(dateEnd.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
				text += " ORDER BY f_ReadDate ASC ";
				this.daCardRecord = new SqlDataAdapter(text, this.cn);
				this.daCardRecord.Fill(this.dsAtt, "CardRecord");
				DataTable dataTable = this.dsAtt.Tables["CardRecord"];
				this.daCardRecord = new SqlDataAdapter("SELECT t_d_ManualCardRecord.f_ConsumerID,  t_d_ManualCardRecord.f_ReadDate, t_d_ManualCardRecord.f_DutyOnOff FROM t_d_ManualCardRecord WHERE 1<0 ", this.cn);
				this.daCardRecord.Fill(this.dsAtt, "ValidCardRecord");
				DataTable dataTable2 = this.dsAtt.Tables["ValidCardRecord"];
				if (dataTable.Rows.Count > 0)
				{
					object[] array = new object[dataTable.Columns.Count - 1 + 1];
					DateTime value = Convert.ToDateTime(dataTable.Rows[0]["f_ReadDate"]);
					DataRow dataRow = dataTable2.NewRow();
					dataTable.Rows[0].ItemArray.CopyTo(array, 0);
					dataRow.ItemArray = array;
					dataTable2.Rows.Add(dataRow);
					value = Convert.ToDateTime(dataRow["f_ReadDate"]);
					for (int i = 1; i <= dataTable.Rows.Count - 1; i++)
					{
						if (this.bStopCreate)
						{
							int result = num;
							return result;
						}
						DateTime dateTime = Convert.ToDateTime(dataTable.Rows[i]["f_ReadDate"]);
						if (dateTime.Subtract(value).TotalSeconds > (double)this.tTwoReadMintime)
						{
							value = dateTime;
							dataRow = dataTable2.NewRow();
							dataTable.Rows[i].ItemArray.CopyTo(array, 0);
							dataRow.ItemArray = array;
							dataTable2.Rows.Add(dataRow);
						}
					}
				}
				int j = 0;
				for (int k = 0; k <= dtShiftWorkSchedule.Rows.Count - 1; k++)
				{
					if (this.bStopCreate)
					{
						int result = num;
						return result;
					}
					object obj = dtShiftWorkSchedule.Rows[k]["f_PlanTime"];
					if (!Information.IsDBNull(obj))
					{
						int num2 = Convert.ToInt32(dtShiftWorkSchedule.Rows[k]["f_TimeSeg"]);
						int num3 = Convert.ToInt32(dtShiftWorkSchedule.Rows[k]["f_ReadTimes"]);
						while (j < dataTable2.Rows.Count)
						{
							object value2 = dataTable2.Rows[j]["f_ReadDate"];
							TimeSpan timeSpan = Convert.ToDateTime(obj).Subtract(Convert.ToDateTime(value2));
							if (num2 == 0)
							{
								if (timeSpan.TotalMinutes > (double)this.tAheadMinutesOnDutyFirst)
								{
									j++;
								}
								else
								{
									if (timeSpan.TotalMinutes > (double)(-(double)this.tDelayMinutes))
									{
										dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
										dtShiftWorkSchedule.Rows[k]["f_CardRecordDesc"] = CommonStr.strSignIn;
										j++;
										break;
									}
									break;
								}
							}
							if (num2 == num3 - 1)
							{
								if (timeSpan.TotalMinutes < (double)(-(double)this.tOvertimeMinutes))
								{
									break;
								}
								if (timeSpan.TotalMinutes > (double)this.tAheadMinutes)
								{
									j++;
								}
								else if (timeSpan.TotalMinutes > (double)(-(double)this.tDelayMinutes))
								{
									dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
									dtShiftWorkSchedule.Rows[k]["f_CardRecordDesc"] = CommonStr.strSignIn;
									j++;
								}
								else
								{
									if (k + 1 < dtShiftWorkSchedule.Rows.Count - 1 && !Information.IsDBNull(dtShiftWorkSchedule.Rows[k + 1]["f_PlanTime"]) && Convert.ToDateTime(dtShiftWorkSchedule.Rows[k + 1]["f_PlanTime"]).AddMinutes((double)(-(double)this.tAheadMinutesOnDutyFirst)) < Convert.ToDateTime(value2))
									{
										break;
									}
									dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
									dtShiftWorkSchedule.Rows[k]["f_CardRecordDesc"] = CommonStr.strSignIn;
									j++;
								}
							}
							if (num2 > 0 && num2 < num3 - 1)
							{
								if (timeSpan.TotalMinutes <= (double)this.tAheadMinutes)
								{
									if (timeSpan.TotalMinutes < (double)(-(double)this.tDelayMinutes))
									{
										break;
									}
									dtShiftWorkSchedule.Rows[k]["f_WorkTime"] = value2;
									dtShiftWorkSchedule.Rows[k]["f_CardRecordDesc"] = CommonStr.strSignIn;
									j++;
									if ((num2 & 1) == 0)
									{
										break;
									}
									if (timeSpan.TotalMinutes <= 0.0)
									{
										break;
									}
								}
								else
								{
									j++;
								}
							}
						}
					}
				}
				num = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return num;
		}

		public int shift_work_schedule_updatebyLeave(int consumerid, DataTable dtShiftWorkSchedule, DateTime dateStart, DateTime dateEnd)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			DateTime arg_15_0 = DateTime.Now;
			this.dsAtt = new DataSet();
			this.errInfo = "";
			int num = -1;
			DateTime t = DateTime.Parse(Strings.Format(dateStart, "yyyy-MM-dd 12:00:00"));
			DateTime t2 = DateTime.Parse(Strings.Format(dateEnd, "yyyy-MM-dd 12:00:00"));
			if (t > t2)
			{
				return num;
			}
			try
			{
				string text = "SELECT * FROM t_d_Leave ";
				text = text + " WHERE f_ConsumerID = " + consumerid;
				text += " ORDER BY f_Value,f_Value1  ";
				this.daLeave = new SqlDataAdapter(text, this.cn);
				this.daLeave.Fill(this.dsAtt, "Leave");
				this.dtLeave = this.dsAtt.Tables["Leave"];
				if (this.dtLeave.Rows.Count <= 0)
				{
					num = 0;
					int result = num;
					return result;
				}
				for (int i = 0; i <= dtShiftWorkSchedule.Rows.Count - 1; i++)
				{
					if (this.bStopCreate)
					{
						int result = num;
						return result;
					}
					object obj = dtShiftWorkSchedule.Rows[i]["f_PlanTime"];
					if (Information.IsDBNull(obj))
					{
						int j = 0;
						obj = dtShiftWorkSchedule.Rows[i]["f_shiftDate"];
						while (j < this.dtLeave.Rows.Count)
						{
							string value = Convert.ToString(this.dtLeave.Rows[j]["f_HolidayType"]);
							this.strTemp = Convert.ToString(this.dtLeave.Rows[j]["f_Value"]);
							this.strTemp = this.strTemp + " " + ((this.dtLeave.Rows[j]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
							DateTime t3;
							DateTime.TryParse(this.strTemp, out t3);
							this.strTemp = Convert.ToString(this.dtLeave.Rows[j]["f_Value2"]);
							this.strTemp = this.strTemp + " " + ((this.dtLeave.Rows[j]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
							DateTime t4;
							DateTime.TryParse(this.strTemp, out t4);
							if (Convert.ToDateTime(obj) >= t3 && Convert.ToDateTime(obj) <= t4)
							{
								dtShiftWorkSchedule.Rows[i]["f_CardRecordDesc"] = value;
								dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = value;
								break;
							}
							j++;
						}
					}
					else
					{
						Convert.ToInt32(dtShiftWorkSchedule.Rows[i]["f_TimeSeg"]);
						Convert.ToInt32(dtShiftWorkSchedule.Rows[i]["f_ReadTimes"]);
						for (int k = 0; k < this.dtLeave.Rows.Count; k++)
						{
							string value2 = Convert.ToString(this.dtLeave.Rows[k]["f_HolidayType"]);
							this.strTemp = Convert.ToString(this.dtLeave.Rows[k]["f_Value"]);
							this.strTemp = this.strTemp + " " + ((this.dtLeave.Rows[k]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
							DateTime t5;
							DateTime.TryParse(this.strTemp, out t5);
							this.strTemp = Convert.ToString(this.dtLeave.Rows[k]["f_Value2"]);
							this.strTemp = this.strTemp + " " + ((this.dtLeave.Rows[k]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
							DateTime t6;
							DateTime.TryParse(this.strTemp, out t6);
							if (Convert.ToDateTime(obj) >= t5 && Convert.ToDateTime(obj) <= t6)
							{
								dtShiftWorkSchedule.Rows[i]["f_WorkTime"] = obj;
								dtShiftWorkSchedule.Rows[i]["f_CardRecordDesc"] = value2;
								break;
							}
						}
					}
				}
				num = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return num;
		}

		public int shift_work_schedule_analyst(DataTable dtShiftWorkSchedule)
		{
			this.errInfo = "";
			int result = -1;
			try
			{
				int num = 0;
				for (int i = 0; i <= dtShiftWorkSchedule.Rows.Count - 1; i++)
				{
					if (this.bStopCreate)
					{
						return result;
					}
					object obj = dtShiftWorkSchedule.Rows[i]["f_PlanTime"];
					if (!Information.IsDBNull(obj))
					{
						int num2 = Convert.ToInt32(dtShiftWorkSchedule.Rows[i]["f_TimeSeg"]);
						int num3 = Convert.ToInt32(dtShiftWorkSchedule.Rows[i]["f_ReadTimes"]);
						int num4 = Convert.ToInt32(dtShiftWorkSchedule.Rows[i]["f_TimeSeg"]) & 1;
						if (Information.IsDBNull(dtShiftWorkSchedule.Rows[i]["f_WorkTime"]))
						{
							dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = CommonStr.strNotReadCard;
						}
						else
						{
							object value = dtShiftWorkSchedule.Rows[i]["f_WorkTime"];
							TimeSpan timeSpan = Convert.ToDateTime(obj).Subtract(Convert.ToDateTime(value));
							dtShiftWorkSchedule.Rows[i]["f_Duration"] = timeSpan.Duration().TotalMinutes;
							if (num4 == 0 && timeSpan.TotalMinutes < (double)(-(double)this.tLateTimeout))
							{
								dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = CommonStr.strLateness;
							}
							if (num4 == 1)
							{
								if (timeSpan.TotalMinutes > (double)this.tLeaveTimeout)
								{
									dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = CommonStr.strLeaveEarly;
								}
								else if (num2 == num3 - 1 && timeSpan.TotalMinutes <= (double)(-(double)this.tOvertimeTimeout))
								{
									dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = CommonStr.strOvertime;
								}
							}
						}
						if (num2 == 0)
						{
							num = i;
						}
						if (num2 == num3 - 1)
						{
							if (this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_bOvertimeShift"]) == "1")
							{
								for (int j = num; j <= i; j++)
								{
									if (this.SetObjToStr(dtShiftWorkSchedule.Rows[j]["f_CardRecordDesc"]) == CommonStr.strSignIn || this.SetObjToStr(dtShiftWorkSchedule.Rows[j]["f_CardRecordDesc"]) == "")
									{
										dtShiftWorkSchedule.Rows[j]["f_AttDesc"] = CommonStr.strOvertime;
									}
								}
							}
							else if (this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_bOvertimeShift"]) == "2")
							{
								if (this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_AttDesc"]) == CommonStr.strNotReadCard && this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_CardRecordDesc"]) == "" && this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_AttDesc"]) == CommonStr.strNotReadCard && this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_CardRecordDesc"]) == "")
								{
									dtShiftWorkSchedule.Rows[i - 1]["f_AttDesc"] = "";
									dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = "";
								}
								else if (this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_CardRecordDesc"]) == CommonStr.strSignIn || this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_CardRecordDesc"]) == "")
								{
									if (!(this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_AttDesc"]) == CommonStr.strNotReadCard) || !(this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_CardRecordDesc"]) == ""))
									{
										dtShiftWorkSchedule.Rows[i - 1]["f_AttDesc"] = CommonStr.strOvertime;
									}
									else if (this.SetObjToStr(dtShiftWorkSchedule.Rows[i - 1]["f_AttDesc"]) == CommonStr.strNotReadCard)
									{
										dtShiftWorkSchedule.Rows[i - 1]["f_AttDesc"] = "";
									}
									if (!(this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_AttDesc"]) == CommonStr.strNotReadCard) || !(this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_CardRecordDesc"]) == ""))
									{
										dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = CommonStr.strOvertime;
									}
									else if (this.SetObjToStr(dtShiftWorkSchedule.Rows[i]["f_AttDesc"]) == CommonStr.strNotReadCard)
									{
										dtShiftWorkSchedule.Rows[i]["f_AttDesc"] = "";
									}
								}
							}
							else
							{
								bool flag = false;
								for (int j = num; j <= i; j++)
								{
									if (!(this.SetObjToStr(dtShiftWorkSchedule.Rows[j]["f_AttDesc"]) == CommonStr.strNotReadCard) || !(this.SetObjToStr(dtShiftWorkSchedule.Rows[j]["f_CardRecordDesc"]) == ""))
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									for (int j = num; j <= i; j++)
									{
										dtShiftWorkSchedule.Rows[j]["f_AttDesc"] = CommonStr.strAbsence;
									}
								}
							}
						}
					}
				}
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_work_schedule_writetodb(DataTable dtShiftWorkSchedule)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.cmd = new SqlCommand();
			string text = "";
			bool flag = true;
			this.errInfo = "";
			int result = -1;
			try
			{
				if (dtShiftWorkSchedule.Rows.Count > 0)
				{
					using (this.cmd = new SqlCommand())
					{
						this.cmd.Connection = this.cn;
						this.cmd.CommandType = CommandType.Text;
						for (int i = 0; i <= dtShiftWorkSchedule.Rows.Count - 1; i++)
						{
							if (this.bStopCreate)
							{
								return result;
							}
							DataRow dataRow = dtShiftWorkSchedule.Rows[i];
							text = " INSERT INTO t_d_Shift_Work_Schedule ";
							text += " ( f_ConsumerID, f_shiftDate, f_ShiftID, f_ReadTimes, f_PlanTime, f_TimeSeg, f_WorkTime, f_AttDesc";
							text += " , f_CardRecordDesc, f_Duration, f_bOvertimeShift";
							text += " ) ";
							text = text + " Values ( " + dataRow["f_ConsumerID"];
							text = text + "," + this.PrepareStr(dataRow["f_shiftDate"], true, "yyyy-MM-dd");
							text = text + "," + dataRow["f_ShiftID"];
							text = text + "," + dataRow["f_ReadTimes"];
							text = text + "," + this.PrepareStr(dataRow["f_PlanTime"], true, "yyyy-MM-dd HH:mm:ss");
							text = text + "," + dataRow["f_TimeSeg"];
							text = text + "," + this.PrepareStr(dataRow["f_WorkTime"], true, "yyyy-MM-dd HH:mm:ss");
							text = text + "," + this.PrepareStr(dataRow["f_AttDesc"]);
							text = text + "," + this.PrepareStr(dataRow["f_CardRecordDesc"]);
							text = text + "," + dataRow["f_Duration"];
							text = text + "," + dataRow["f_bOvertimeShift"];
							text += ") ";
							if (this.cn.State == ConnectionState.Closed)
							{
								this.cn.Open();
							}
							this.cmd.CommandText = text;
							int num = this.cmd.ExecuteNonQuery();
							if (num <= 0)
							{
								this.errInfo = text;
								flag = false;
								break;
							}
						}
					}
				}
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				if (flag)
				{
					result = 0;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString() + "\r\n" + text, new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int shift_work_schedule_cleardb()
		{
			this.errInfo = "";
			int result = -1;
			try
			{
				wgAppConfig.runUpdateSql("TRUNCATE TABLE t_d_Shift_Work_Schedule");
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_AttReport_Create(out DataTable dtAttReport)
		{
			this.dtReport = new DataTable("t_d_AttReport");
			int result = -1;
			dtAttReport = null;
			try
			{
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ConsumerID";
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_ShiftDate";
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ShiftID";
				this.dc.DefaultValue = -1;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_Readtimes";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				for (int i = 1; i <= 4; i++)
				{
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.DateTime");
					this.dc.ColumnName = "f_OnDuty" + i;
					this.dtReport.Columns.Add(this.dc);
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.String");
					this.dc.ColumnName = "f_OnDuty" + i + "AttDesc";
					this.dtReport.Columns.Add(this.dc);
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.String");
					this.dc.ColumnName = "f_OnDuty" + i + "CardRecordDesc";
					this.dtReport.Columns.Add(this.dc);
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.DateTime");
					this.dc.ColumnName = "f_OffDuty" + i;
					this.dtReport.Columns.Add(this.dc);
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.String");
					this.dc.ColumnName = "f_OffDuty" + i + "AttDesc";
					this.dtReport.Columns.Add(this.dc);
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.String");
					this.dc.ColumnName = "f_OffDuty" + i + "CardRecordDesc";
					this.dtReport.Columns.Add(this.dc);
				}
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LateMinutes";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LeaveEarlyMinutes";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Decimal");
				this.dc.ColumnName = "f_OvertimeHours";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Decimal");
				this.dc.ColumnName = "f_AbsenceDays";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_NotReadCardCount";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_bOvertimeShift";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				dtAttReport = this.dtReport.Copy();
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_AttReport_Fill(DataTable dtAttReport, DataTable dtShiftWorkSchedule)
		{
			this.errInfo = "";
			int result = -1;
			try
			{
				DataRow dataRow = null;
				for (int i = 0; i <= dtShiftWorkSchedule.Rows.Count - 1; i++)
				{
					if (this.bStopCreate)
					{
						return result;
					}
					if (dataRow == null)
					{
						dataRow = dtAttReport.NewRow();
					}
					DataRow dataRow2 = dtShiftWorkSchedule.Rows[i];
					int num = Convert.ToInt32(dataRow2["f_Readtimes"]);
					if (Convert.ToInt32(dataRow2["f_ShiftID"]) <= 0)
					{
						dataRow["f_ConsumerID"] = dataRow2["f_ConsumerID"];
						dataRow["f_ShiftDate"] = dataRow2["f_ShiftDate"];
						dataRow["f_ShiftID"] = dataRow2["f_ShiftID"];
						dataRow["f_Readtimes"] = dataRow2["f_Readtimes"];
						for (int j = 1; j <= 4; j++)
						{
							dataRow["f_OnDuty" + j + "AttDesc"] = dataRow2["f_AttDesc"];
							dataRow["f_OffDuty" + j + "AttDesc"] = dataRow2["f_AttDesc"];
						}
						dtAttReport.Rows.Add(dataRow);
						dataRow = dtAttReport.NewRow();
					}
					else
					{
						int num2 = Convert.ToInt32(dataRow2["f_Timeseg"]);
						if (num2 == 0)
						{
							dataRow["f_LateMinutes"] = 0;
							dataRow["f_LeaveEarlyMinutes"] = 0;
							dataRow["f_OvertimeHours"] = 0;
							dataRow["f_AbsenceDays"] = 0;
							dataRow["f_NotReadCardCount"] = 0;
							dataRow["f_ConsumerID"] = dataRow2["f_ConsumerID"];
							dataRow["f_ShiftDate"] = dataRow2["f_ShiftDate"];
							dataRow["f_ShiftID"] = dataRow2["f_ShiftID"];
							dataRow["f_Readtimes"] = dataRow2["f_Readtimes"];
							dataRow["f_bOvertimeShift"] = dataRow2["f_bOvertimeShift"];
						}
						if ((num2 & 1) == 0)
						{
							dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1) + "AttDesc"] = this.SetObjToStr(dataRow2["f_AttDesc"]);
							dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1) + "CardRecordDesc"] = this.SetObjToStr(dataRow2["f_CardRecordDesc"]);
							dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1)] = dataRow2["f_WorkTime"];
						}
						else
						{
							dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1) + "AttDesc"] = this.SetObjToStr(dataRow2["f_AttDesc"]);
							dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1) + "CardRecordDesc"] = this.SetObjToStr(dataRow2["f_CardRecordDesc"]);
							dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1)] = dataRow2["f_WorkTime"];
						}
						if (this.SetObjToStr(dataRow2["f_AttDesc"]) == CommonStr.strLateness)
						{
							dataRow["f_LateMinutes"] = Convert.ToDecimal(dataRow["f_LateMinutes"], CultureInfo.InvariantCulture) + Convert.ToDecimal(dataRow2["f_Duration"], CultureInfo.InvariantCulture);
						}
						else if (this.SetObjToStr(dataRow2["f_AttDesc"]) == CommonStr.strLeaveEarly)
						{
							dataRow["f_LeaveEarlyMinutes"] = Convert.ToDecimal(dataRow["f_LeaveEarlyMinutes"], CultureInfo.InvariantCulture) + Convert.ToDecimal(dataRow2["f_Duration"], CultureInfo.InvariantCulture);
						}
						else if (this.SetObjToStr(dataRow2["f_AttDesc"]) == CommonStr.strAbsence)
						{
							if ((num2 & 1) == 0)
							{
								dataRow["f_AbsenceDays"] = Convert.ToDecimal(dataRow["f_AbsenceDays"], CultureInfo.InvariantCulture) + this.tLateAbsenceDay;
							}
							else
							{
								dataRow["f_AbsenceDays"] = Convert.ToDecimal(dataRow["f_AbsenceDays"], CultureInfo.InvariantCulture) + this.tLeaveAbsenceDay;
							}
						}
						else if (this.SetObjToStr(dataRow2["f_AttDesc"]) == CommonStr.strOvertime)
						{
							if (Convert.ToInt32(dataRow["f_bOvertimeShift"]) > 0)
							{
								if (Convert.ToInt32(dataRow["f_bOvertimeShift"]) == 1)
								{
									if ((num2 & 1) != 0 && !(this.SetObjToStr(dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1)]) == "") && !(this.SetObjToStr(dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1)]) == ""))
									{
										int value = (int)(Convert.ToDateTime(dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1)]).Subtract(Convert.ToDateTime(dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1)])).TotalMinutes / 30.0);
										dataRow["f_OvertimeHours"] = Convert.ToDecimal(dataRow["f_OvertimeHours"], CultureInfo.InvariantCulture) + value / 2.0m;
									}
								}
								else if (Convert.ToInt32(dataRow["f_bOvertimeShift"]) == 2 && (num2 & 1) != 0 && !(this.SetObjToStr(dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1)]) == "") && !(this.SetObjToStr(dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1)]) == ""))
								{
									object value2 = dtShiftWorkSchedule.Rows[i - 1]["f_PlanTime"];
									int value3;
									if (Convert.ToDateTime(value2).Subtract(Convert.ToDateTime(dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1)])).TotalMinutes < 0.0)
									{
										value3 = (int)(Convert.ToDateTime(dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1)]).Subtract(Convert.ToDateTime(dataRow["f_OnDuty" + (Conversion.Int(num2 / 2) + 1)])).TotalMinutes / 30.0);
									}
									else
									{
										value3 = (int)(Convert.ToDateTime(dataRow["f_OffDuty" + (Conversion.Int(num2 / 2) + 1)]).Subtract(Convert.ToDateTime(value2)).TotalMinutes / 30.0);
									}
									dataRow["f_OvertimeHours"] = Convert.ToDecimal(dataRow["f_OvertimeHours"], CultureInfo.InvariantCulture) + value3 / 2.0m;
								}
							}
							else
							{
								dataRow["f_OvertimeHours"] = Convert.ToDecimal(dataRow["f_OvertimeHours"], CultureInfo.InvariantCulture) + Conversion.Int(Convert.ToInt32(dataRow2["f_Duration"]) / 30) / 2.0m;
							}
						}
						else if (this.SetObjToStr(dataRow2["f_AttDesc"]) == CommonStr.strNotReadCard)
						{
							dataRow["f_NotReadCardCount"] = Convert.ToInt32(dataRow["f_NotReadCardCount"]) + 1;
						}
						if (num2 + 1 == num)
						{
							if (Convert.ToDecimal(dataRow["f_AbsenceDays"], CultureInfo.InvariantCulture) > 1m)
							{
								dataRow["f_AbsenceDays"] = 1;
							}
							dtAttReport.Rows.Add(dataRow);
							dataRow = dtAttReport.NewRow();
						}
					}
				}
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_AttStatistic_Create(out DataTable dtAttStatistic)
		{
			this.dtReport1 = new DataTable("t_d_AttStatistic");
			int result = -1;
			dtAttStatistic = null;
			try
			{
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ConsumerID";
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_AttDateStart";
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_AttDateEnd";
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_DayShouldWork";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_DayRealWork";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LateMinutes";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LateCount";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LeaveEarlyMinutes";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LeaveEarlyCount";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Decimal");
				this.dc.ColumnName = "f_OvertimeHours";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Decimal");
				this.dc.ColumnName = "f_AbsenceDays";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_NotReadCardCount";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ManualReadTimesCount";
				this.dc.DefaultValue = 0;
				this.dtReport1.Columns.Add(this.dc);
				for (int i = 1; i <= 32; i++)
				{
					this.dc = new DataColumn();
					this.dc.DataType = Type.GetType("System.String");
					this.dc.ColumnName = "f_SpecialType" + i;
					this.dc.DefaultValue = "";
					this.dtReport1.Columns.Add(this.dc);
				}
				dtAttStatistic = this.dtReport1.Copy();
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_AttStatistic_Fill(DataTable dtAttStatistic, DataTable dtAttReport)
		{
			this.errInfo = "";
			int num = -1;
			try
			{
				DataRow dataRow = null;
				this.cn = new SqlConnection(wgAppConfig.dbConString);
				this.dsAtt = new DataSet();
				this.daHolidayType = new SqlDataAdapter("SELECT * FROM t_a_HolidayType", this.cn);
				this.daHolidayType.Fill(this.dsAtt, "HolidayType");
				this.dtHolidayType = this.dsAtt.Tables["HolidayType"];
				comShift.localizedHolidayType(this.dtHolidayType);
				for (int i = 0; i <= dtAttReport.Rows.Count - 1; i++)
				{
					bool flag = false;
					if (this.bStopCreate)
					{
						int result = num;
						return result;
					}
					if (dataRow == null)
					{
						dataRow = dtAttStatistic.NewRow();
					}
					DataRow dataRow2 = dtAttReport.Rows[i];
					int num2 = Convert.ToInt32(dataRow2["f_Readtimes"]);
					if (i == 0)
					{
						dataRow["f_ConsumerID"] = dataRow2["f_ConsumerID"];
						dataRow["f_AttDateStart"] = dataRow2["f_ShiftDate"];
					}
					Convert.ToDateTime(dataRow2["f_ShiftDate"]);
					dataRow["f_LateMinutes"] = Convert.ToInt32(dataRow["f_LateMinutes"], CultureInfo.InvariantCulture) + Convert.ToInt32(dataRow2["f_LateMinutes"], CultureInfo.InvariantCulture);
					dataRow["f_LeaveEarlyMinutes"] = Convert.ToInt32(dataRow["f_LeaveEarlyMinutes"], CultureInfo.InvariantCulture) + Convert.ToInt32(dataRow2["f_LeaveEarlyMinutes"], CultureInfo.InvariantCulture);
					dataRow["f_AbsenceDays"] = Convert.ToDecimal(dataRow["f_AbsenceDays"], CultureInfo.InvariantCulture) + Convert.ToDecimal(dataRow2["f_AbsenceDays"], CultureInfo.InvariantCulture);
					dataRow["f_OvertimeHours"] = Convert.ToDecimal(dataRow["f_OvertimeHours"], CultureInfo.InvariantCulture) + Convert.ToDecimal(dataRow2["f_OvertimeHours"], CultureInfo.InvariantCulture);
					dataRow["f_NotReadCardCount"] = Convert.ToInt32(dataRow["f_NotReadCardCount"]) + Convert.ToInt32(dataRow2["f_NotReadCardCount"]);
					if (Convert.ToInt32(dataRow2["f_ShiftID"]) > 0)
					{
						dataRow["f_DayShouldWork"] = Convert.ToInt32(dataRow["f_DayShouldWork"]) + 1;
						if (this.SetObjToStr(dataRow2["f_OnDuty1"]) != "" && this.SetObjToStr(dataRow2["f_OffDuty1"]) != "" && Convert.ToInt32(dataRow2["f_LateMinutes"], CultureInfo.InvariantCulture) == 0 && Convert.ToInt32(dataRow2["f_LeaveEarlyMinutes"], CultureInfo.InvariantCulture) == 0 && Convert.ToInt32(dataRow2["f_NotReadCardCount"]) == 0 && Convert.ToDecimal(dataRow2["f_AbsenceDays"], CultureInfo.InvariantCulture) == 0m)
						{
							flag = true;
						}
					}
					if (CommonStr.strOvertime == this.SetObjToStr(dataRow2["f_OnDuty" + (Conversion.Int(0) + 1) + "AttDesc"]))
					{
						for (int j = 1; j <= num2; j += 2)
						{
							string text = this.SetObjToStr(dataRow2["f_OnDuty" + (Conversion.Int(j / 2) + 1)]);
							if ("" == text)
							{
								flag = false;
								break;
							}
							text = this.SetObjToStr(dataRow2["f_OffDuty" + (Conversion.Int(j / 2) + 1)]);
							if ("" == text)
							{
								flag = false;
								break;
							}
						}
					}
					if (num2 == 0)
					{
						flag = false;
					}
					for (int j = 1; j <= num2; j += 2)
					{
						if (this.bStopCreate)
						{
							int result = num;
							return result;
						}
						string text = this.SetObjToStr(dataRow2["f_OnDuty" + (Conversion.Int(j / 2) + 1) + "CardRecordDesc"]);
						if (text != "")
						{
							if (text == CommonStr.strSignIn)
							{
								dataRow["f_ManualReadTimesCount"] = Convert.ToInt32(dataRow["f_ManualReadTimesCount"]) + 1;
							}
							else if (CommonStr.strOvertime != text)
							{
								flag = false;
							}
						}
						text = this.SetObjToStr(dataRow2["f_OffDuty" + (Conversion.Int(j / 2) + 1) + "CardRecordDesc"]);
						if (text != "")
						{
							if (text == CommonStr.strSignIn)
							{
								dataRow["f_ManualReadTimesCount"] = Convert.ToInt32(dataRow["f_ManualReadTimesCount"]) + 1;
							}
							else if (CommonStr.strOvertime != text)
							{
								flag = false;
							}
						}
						text = this.SetObjToStr(dataRow2["f_OnDuty" + (Conversion.Int(j / 2) + 1) + "AttDesc"]);
						if (text != "")
						{
							if (text == CommonStr.strLateness)
							{
								dataRow["f_LateCount"] = Convert.ToInt32(dataRow["f_LateCount"]) + 1;
							}
							else if (text == CommonStr.strLeaveEarly)
							{
								dataRow["f_LeaveEarlyCount"] = Convert.ToInt32(dataRow["f_LeaveEarlyCount"]) + 1;
							}
						}
						text = this.SetObjToStr(dataRow2["f_OffDuty" + (Conversion.Int(j / 2) + 1) + "AttDesc"]);
						if (text != "")
						{
							if (text == CommonStr.strLateness)
							{
								dataRow["f_LateCount"] = Convert.ToInt32(dataRow["f_LateCount"]) + 1;
							}
							else if (text == CommonStr.strLeaveEarly)
							{
								dataRow["f_LeaveEarlyCount"] = Convert.ToInt32(dataRow["f_LeaveEarlyCount"]) + 1;
							}
						}
					}
					if (flag)
					{
						dataRow["f_DayRealWork"] = Convert.ToInt32(dataRow["f_DayRealWork"]) + 1;
					}
					if (i == dtAttReport.Rows.Count - 1)
					{
						dataRow["f_AttDateEnd"] = dataRow2["f_ShiftDate"];
						this.shift_AttStatistic_updatebyLeave(dataRow);
						dtAttStatistic.Rows.Add(dataRow);
					}
				}
				num = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return num;
		}

		public int shift_AttStatistic_updatebyLeave(DataRow drAttStatistic)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			DateTime arg_15_0 = DateTime.Now;
			this.dsAtt = new DataSet();
			this.errInfo = "";
			int num = -1;
			try
			{
				DateTime dateTime = DateTime.Parse(Strings.Format(drAttStatistic["f_AttDateStart"], "yyyy-MM-dd 12:00:00"));
				DateTime t = DateTime.Parse(Strings.Format(drAttStatistic["f_AttDateEnd"], "yyyy-MM-dd 12:00:00"));
				int num2 = Convert.ToInt32(drAttStatistic["f_ConsumerID"]);
				if (dateTime > t)
				{
					int result = num;
					return result;
				}
				string text = "SELECT * FROM t_d_Leave ";
				text = text + " WHERE f_ConsumerID = " + num2;
				text += " ORDER BY f_Value,f_Value1  ";
				this.daLeave = new SqlDataAdapter(text, this.cn);
				this.daLeave.Fill(this.dsAtt, "Leave");
				this.dtLeave = this.dsAtt.Tables["Leave"];
				if (this.dtLeave.Rows.Count <= 0)
				{
					num = 0;
					int result = num;
					return result;
				}
				dateTime = DateTime.Parse(Strings.Format(drAttStatistic["f_AttDateStart"], "yyyy-MM-dd 08:00:00"));
				t = DateTime.Parse(Strings.Format(drAttStatistic["f_AttDateEnd"], "yyyy-MM-dd 20:00:00"));
				object value = dateTime;
				this.daHolidayType.Fill(this.dsAtt, "HolidayType");
				this.dtHolidayType = this.dsAtt.Tables["HolidayType"];
				comShift.localizedHolidayType(this.dtHolidayType);
				while (!(Convert.ToDateTime(value) > t))
				{
					int i = 0;
					while (i < this.dtLeave.Rows.Count)
					{
						string a = Convert.ToString(this.dtLeave.Rows[i]["f_HolidayType"]);
						this.strTemp = Convert.ToString(this.dtLeave.Rows[i]["f_Value"]);
						this.strTemp = this.strTemp + " " + ((this.dtLeave.Rows[i]["f_Value1"].ToString() == CommonStr.strAM) ? "00:00:00" : "12:00:00");
						DateTime t2 = DateTime.Parse(this.strTemp);
						this.strTemp = Convert.ToString(this.dtLeave.Rows[i]["f_Value2"]);
						this.strTemp = this.strTemp + " " + ((this.dtLeave.Rows[i]["f_Value3"].ToString() == CommonStr.strAM) ? "12:00:00" : "23:59:59");
						DateTime t3 = DateTime.Parse(this.strTemp);
						if (Convert.ToDateTime(value) >= t2 && Convert.ToDateTime(value) <= t3)
						{
							bool flag = false;
							for (int j = 0; j <= this.dtHolidayType.Rows.Count - 1; j++)
							{
								if (a == this.dtHolidayType.Rows[j][1].ToString())
								{
									if (drAttStatistic["f_SpecialType" + (j + 1)].ToString() == "")
									{
										drAttStatistic["f_SpecialType" + (j + 1)] = 0;
									}
									drAttStatistic["f_SpecialType" + (j + 1)] = Convert.ToDecimal(drAttStatistic["f_SpecialType" + (j + 1)], CultureInfo.InvariantCulture) + 0.5m;
									flag = true;
									break;
								}
							}
							if (flag)
							{
								break;
							}
							i++;
						}
						else
						{
							if (Convert.ToDateTime(value) < t2)
							{
								break;
							}
							i++;
						}
					}
					value = Convert.ToDateTime(value).AddHours(12.0);
				}
				num = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State == ConnectionState.Open)
				{
					this.cn.Close();
				}
			}
			return num;
		}

		public int shift_AttReport_writetodb(DataTable dtAttReport)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			string text = "";
			this.cmd = new SqlCommand();
			bool flag = true;
			this.errInfo = "";
			int result = -1;
			try
			{
				if (dtAttReport.Rows.Count > 0)
				{
					this.cmd.Connection = this.cn;
					this.cmd.CommandType = CommandType.Text;
					for (int i = 0; i <= dtAttReport.Rows.Count - 1; i++)
					{
						if (this.bStopCreate)
						{
							return result;
						}
						DataRow dataRow = dtAttReport.Rows[i];
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
						text = text + "," + this.PrepareStr(dataRow["f_shiftDate"], true, "yyyy-MM-dd");
						text = text + "," + dataRow["f_ShiftID"];
						text = text + "," + dataRow["f_ReadTimes"];
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty1AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty1CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty1"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty1AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty1CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty2AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty2CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty2"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty2AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty2CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty3"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty3AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty3CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty3"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty3AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty3CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty4"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty4AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OnDuty4CardRecordDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty4"], true, "yyyy-MM-dd HH:mm:ss");
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty4AttDesc"]);
						text = text + "," + this.PrepareStr(dataRow["f_OffDuty4CardRecordDesc"]);
						text = text + "," + dataRow["f_LateMinutes"];
						text = text + "," + dataRow["f_LeaveEarlyMinutes"];
						text = text + "," + dataRow["f_OvertimeHours"];
						text = text + "," + dataRow["f_AbsenceDays"];
						text = text + "," + dataRow["f_NotReadCardCount"];
						text = text + "," + dataRow["f_bOvertimeShift"];
						text += ") ";
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						this.cmd.CommandText = text;
						int num = this.cmd.ExecuteNonQuery();
						if (num <= 0)
						{
							this.errInfo = text;
							flag = false;
							break;
						}
					}
				}
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				if (flag)
				{
					result = 0;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString() + "\r\n" + text, new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int shift_AttReport_cleardb()
		{
			this.errInfo = "";
			int result = -1;
			try
			{
				wgAppConfig.runUpdateSql("TRUNCATE TABLE   t_d_shift_AttReport");
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_AttStatistic_writetodb(DataTable dtAttStatistic)
		{
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			string text = "";
			this.cmd = new SqlCommand();
			bool flag = true;
			this.errInfo = "";
			int result = -1;
			try
			{
				if (dtAttStatistic.Rows.Count > 0)
				{
					this.cmd.Connection = this.cn;
					this.cmd.CommandType = CommandType.Text;
					for (int i = 0; i <= dtAttStatistic.Rows.Count - 1; i++)
					{
						if (this.bStopCreate)
						{
							return result;
						}
						DataRow dataRow = dtAttStatistic.Rows[i];
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
						text = text + "," + dataRow["f_LateCount"];
						text = text + "," + dataRow["f_LeaveEarlyMinutes"];
						text = text + "," + dataRow["f_LeaveEarlyCount"];
						text = text + "," + dataRow["f_OvertimeHours"];
						text = text + "," + dataRow["f_AbsenceDays"];
						text = text + "," + dataRow["f_NotReadCardCount"];
						text = text + "," + dataRow["f_ManualReadTimesCount"];
						for (int j = 1; j <= 32; j++)
						{
							text = text + " ," + this.PrepareStr(dataRow["f_SpecialType" + j.ToString()]);
						}
						text += ") ";
						if (this.cn.State == ConnectionState.Closed)
						{
							this.cn.Open();
						}
						this.cmd.CommandText = text;
						int num = this.cmd.ExecuteNonQuery();
						if (num <= 0)
						{
							this.errInfo = text;
							flag = false;
							break;
						}
					}
				}
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
				if (flag)
				{
					result = 0;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString() + "\r\n" + text, new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				if (this.cn.State != ConnectionState.Closed)
				{
					this.cn.Close();
				}
			}
			return result;
		}

		public int logCreateReport(DateTime startDateTime, DateTime endDateTime, string groupName, string totalConsumer)
		{
			int result = -1;
			try
			{
				string text = string.Concat(new string[]
				{
					CommonStr.strCreateLog,
					"  [",
					CommonStr.strOperateDate,
					DateTime.Now.ToString(wgTools.DisplayFormat_DateYMDHMSWeek),
					"]"
				});
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					";  ",
					CommonStr.strFrom,
					Strings.Format(startDateTime, wgTools.DisplayFormat_DateYMD),
					CommonStr.strTo,
					Strings.Format(endDateTime, wgTools.DisplayFormat_DateYMD)
				});
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					";   ",
					wgAppConfig.ReplaceFloorRomm(CommonStr.strDepartment),
					":",
					groupName,
					"            ",
					CommonStr.strUser,
					" (",
					totalConsumer,
					")"
				});
				string obj = Strings.Format(startDateTime, "yyyy-MM-dd") + "--" + Strings.Format(endDateTime, "yyyy-MM-dd");
				string text4 = "UPDATE t_a_Attendence ";
				text4 = text4 + " SET [f_Value]=" + this.PrepareStr(obj);
				text4 = text4 + " , [f_Notes] = " + this.PrepareStr(text);
				text4 += " WHERE [f_NO]= 15 ";
				wgAppConfig.runUpdateSql(text4);
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}

		public int shift_AttStatistic_cleardb()
		{
			this.errInfo = "";
			int result = -1;
			try
			{
				wgAppConfig.runUpdateSql("TRUNCATE TABLE  t_d_shift_AttStatistic");
				result = 0;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return result;
		}
	}
}
