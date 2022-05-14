using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class comPatrol_Acc : Component
	{
		private const int ERR_NONE = 0;

		private const int ERR_FAIL = -1;

		private const int EER_SQL_RUNFAIL = -999;

		private IContainer components;

		public bool bStopCreate;

		public string errInfo = "";

		private int tTwoReadMintime = 60;

		private int tPatrolEventDescNormal = 1;

		private int tPatrolEventDescEarly = 2;

		private int tPatrolEventDescLate = 3;

		private int tPatrolEventDescAbsence = 4;

		private short tNotPatrolTimeout = 30;

		private int tOnTimePatrolTimeout = 10;

		private DataTable dtShiftWork;

		private DataColumn dc;

		private DataTable dtCardRecord;

		private DataTable dtValidCardRecord;

		private OleDbDataAdapter daCardRecord;

		private DataSet dsAtt = new DataSet();

		private OleDbConnection cn;

		private OleDbCommand cmd;

		private DataTable dtReport;

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
		}

		public comPatrol_Acc()
		{
			this.InitializeComponent();
		}

		public comPatrol_Acc(IContainer container)
		{
			container.Add(this);
			this.InitializeComponent();
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

		public string errDesc(int errno)
		{
			string result;
			if (errno != -999)
			{
				if (errno == -1)
				{
					result = CommonStr.strFailed;
				}
				else
				{
					result = CommonStr.strUnknown;
				}
			}
			else
			{
				result = CommonStr.strSqlRunFail;
			}
			return result;
		}

		public void getPatrolParam()
		{
			this.tOnTimePatrolTimeout = (int)short.Parse(wgAppConfig.getSystemParamByNO(28));
			this.tNotPatrolTimeout = short.Parse(wgAppConfig.getSystemParamByNO(27));
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

		public int shift_arrangeByRule(int consumerId, DateTime dateStart, DateTime dateEnd, int ruleLen, int[] shiftRule)
		{
			this.cn = new OleDbConnection(wgAppConfig.dbConString);
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
						text2 = " SELECT * FROM t_d_PatrolPlanData ";
						text2 = text2 + " WHERE f_ConsumerID = " + consumerId;
						text2 = text2 + " AND f_DateYM = " + this.PrepareStr(text);
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new OleDbCommand(text2, this.cn);
						OleDbDataReader oleDbDataReader = this.cmd.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							flag = true;
							for (int i = 0; i <= oleDbDataReader.FieldCount - 1; i++)
							{
								array[i] = oleDbDataReader[i];
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
						oleDbDataReader.Close();
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
						text2 = "  UPDATE t_d_PatrolPlanData SET ";
						int k = 1;
						object obj = text2;
						text2 = string.Concat(new object[]
						{
							obj,
							" f_RouteID_",
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
								" , f_RouteID_",
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
						text2 = "  INSERT INTO t_d_PatrolPlanData  ";
						text2 += " ( f_ConsumerID , f_DateYM  ";
						for (int l = 1; l <= 31; l++)
						{
							text2 = text2 + " , f_RouteID_" + l.ToString().PadLeft(2, '0');
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
					this.cmd = new OleDbCommand(text2, this.cn);
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
			this.cn = new OleDbConnection(wgAppConfig.dbConString);
			bool flag = false;
			object[] array = new object[37];
			this.errInfo = "";
			int result = -1;
			if (consumerId == 0)
			{
				string text = " DELETE FROM t_d_PatrolPlanData ";
				if (this.cn.State != ConnectionState.Open)
				{
					this.cn.Open();
				}
				this.cmd = new OleDbCommand(text, this.cn);
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
						text = " SELECT * FROM t_d_PatrolPlanData ";
						text = text + " WHERE f_ConsumerID = " + consumerId;
						text = text + " AND f_DateYM = " + this.PrepareStr(text2);
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new OleDbCommand(text, this.cn);
						OleDbDataReader oleDbDataReader = this.cmd.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							flag = true;
							for (int i = 0; i <= oleDbDataReader.FieldCount - 1; i++)
							{
								array[i] = oleDbDataReader[i];
							}
						}
						else
						{
							flag = false;
						}
						oleDbDataReader.Close();
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
							text = "  DELETE FROM t_d_PatrolPlanData ";
							text = text + " WHERE f_RecID = " + array[0];
						}
						else
						{
							text = "  UPDATE t_d_PatrolPlanData SET ";
							int j = 1;
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								" f_RouteID_",
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
									" , f_RouteID_",
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
						this.cmd = new OleDbCommand(text, this.cn);
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
			this.dtShiftWork = new DataTable("t_d_PatrolPlanWork");
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
				this.dc.ColumnName = "f_PatrolDate";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_RouteID";
				this.dc.DefaultValue = -1;
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_ReaderID";
				this.dc.DefaultValue = 0;
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_PlanPatrolTime";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_RealPatrolTime";
				this.dtShiftWork.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_EventDesc";
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
			this.cn = new OleDbConnection(wgAppConfig.dbConString);
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
						string text2 = " SELECT * FROM t_d_PatrolPlanData ";
						text2 = text2 + " WHERE f_ConsumerID = " + consumerid;
						text2 = text2 + " AND f_DateYM = " + this.PrepareStr(text);
						if (this.cn.State != ConnectionState.Open)
						{
							this.cn.Open();
						}
						this.cmd = new OleDbCommand(text2, this.cn);
						OleDbDataReader oleDbDataReader = this.cmd.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							flag = true;
							for (int i = 0; i <= oleDbDataReader.FieldCount - 1; i++)
							{
								array[i] = oleDbDataReader[i];
							}
						}
						else
						{
							bNotArranged |= 1;
							flag = false;
							array[0] = -1;
						}
						oleDbDataReader.Close();
					}
					do
					{
						if (flag)
						{
							int num = Convert.ToInt32(array[2 + dateTime2.Day]);
							if (num > 0)
							{
								string text2 = "SELECT * FROM t_d_PatrolRouteDetail WHERE f_RouteID = " + num + " ORDER BY f_Sn ";
								this.cmd = new OleDbCommand(text2, this.cn);
								if (this.cn.State != ConnectionState.Open)
								{
									this.cn.Open();
								}
								OleDbDataReader oleDbDataReader = this.cmd.ExecuteReader();
								while (oleDbDataReader.Read())
								{
									DataRow dataRow = dtShiftWorkSchedule.NewRow();
									dataRow[0] = consumerid;
									dataRow[1] = dateTime2;
									dataRow["f_RouteID"] = oleDbDataReader["f_RouteID"];
									dataRow["f_ReaderID"] = oleDbDataReader["f_ReaderID"];
									if ((int)oleDbDataReader["f_NextDay"] > 0)
									{
										dataRow["f_PlanPatrolTime"] = Convert.ToDateTime(string.Concat(new object[]
										{
											Strings.Format(dateTime2.AddDays(1.0), "yyyy-MM-dd"),
											" ",
											oleDbDataReader["f_patroltime"],
											":00"
										}));
									}
									else
									{
										dataRow["f_PlanPatrolTime"] = Convert.ToDateTime(string.Concat(new object[]
										{
											Strings.Format(dateTime2, "yyyy-MM-dd"),
											" ",
											oleDbDataReader["f_patroltime"],
											":00"
										}));
									}
									dtShiftWorkSchedule.Rows.Add(dataRow);
								}
								oleDbDataReader.Close();
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
			this.getPatrolParam();
			this.cn = new OleDbConnection(wgAppConfig.dbConString);
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
				string text = "SELECT t_d_SwipeRecord.f_ConsumerID, t_d_SwipeRecord.f_ReaderID, t_d_SwipeRecord.f_ReadDate, 0 as f_used  FROM t_d_SwipeRecord, t_b_Reader,t_b_Reader4Patrol ";
				text = text + " , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  f_ConsumerID = " + consumerid;
				text = text + " AND ([f_ReadDate]>= " + this.PrepareStr(dateStart, true, "yyyy-MM-dd 00:00:00") + ") ";
				text = text + " AND ([f_ReadDate]<= " + this.PrepareStr(dateEnd.AddDays(1.0), true, "yyyy-MM-dd 23:59:59") + ") ";
				text += " AND (t_d_SwipeRecord.f_ReaderID = t_b_Reader.f_ReaderID) ";
				text += " AND t_b_Reader.f_ReaderID = t_b_Reader4Patrol.f_ReaderID ";
				text += " ORDER BY f_ReadDate ASC, f_RecID ASC ";
				this.cmd = new OleDbCommand();
				this.cmd.CommandTimeout = wgAppConfig.dbCommandTimeout;
				this.cmd.Connection = this.cn;
				this.cmd.CommandText = text;
				this.cmd.CommandType = CommandType.Text;
				this.daCardRecord = new OleDbDataAdapter(this.cmd);
				this.daCardRecord.Fill(this.dsAtt, "CardRecord");
				this.dtCardRecord = this.dsAtt.Tables["CardRecord"];
				this.daCardRecord = new OleDbDataAdapter("SELECT t_d_SwipeRecord.f_ConsumerID, t_d_SwipeRecord.f_ReaderID, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_DutyOnOff,0 as f_used FROM t_d_SwipeRecord, t_b_Reader , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  1<0  ", this.cn);
				this.daCardRecord.Fill(this.dsAtt, "ValidCardRecord");
				this.dtValidCardRecord = this.dsAtt.Tables["ValidCardRecord"];
				if (this.dtCardRecord.Rows.Count > 0)
				{
					object[] array = new object[this.dtCardRecord.Columns.Count - 1 + 1];
					DateTime value = Convert.ToDateTime(this.dtCardRecord.Rows[0]["f_ReadDate"]);
					int num2 = Convert.ToInt32(this.dtCardRecord.Rows[0]["f_ReaderID"]);
					DataRow dataRow = this.dtValidCardRecord.NewRow();
					this.dtCardRecord.Rows[0].ItemArray.CopyTo(array, 0);
					dataRow.ItemArray = array;
					this.dtValidCardRecord.Rows.Add(dataRow);
					value = Convert.ToDateTime(dataRow["f_ReadDate"]);
					num2 = Convert.ToInt32(dataRow["f_ReaderID"]);
					for (int i = 1; i <= this.dtCardRecord.Rows.Count - 1; i++)
					{
						if (this.bStopCreate)
						{
							int result = num;
							return result;
						}
						DateTime dateTime = Convert.ToDateTime(this.dtCardRecord.Rows[i]["f_ReadDate"]);
						TimeSpan timeSpan = dateTime.Subtract(value);
						int num3 = Convert.ToInt32(this.dtCardRecord.Rows[i]["f_ReaderID"]);
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
					object obj = dtShiftWorkSchedule.Rows[k]["f_PlanPatrolTime"];
					if (!Information.IsDBNull(obj))
					{
						bool flag = false;
						int num4 = j;
						while (j < this.dtValidCardRecord.Rows.Count)
						{
							object value2 = this.dtValidCardRecord.Rows[j]["f_ReadDate"];
							TimeSpan timeSpan2 = Convert.ToDateTime(obj).Subtract(Convert.ToDateTime(value2));
							if (timeSpan2.TotalMinutes > (double)this.tNotPatrolTimeout)
							{
								j++;
								num4 = j;
							}
							else
							{
								if (timeSpan2.TotalMinutes < (double)(-(double)this.tNotPatrolTimeout))
								{
									break;
								}
								if (wgTools.SetObjToStr(this.dtValidCardRecord.Rows[j]["f_used"]) != "1" && (int)dtShiftWorkSchedule.Rows[k]["f_ReaderID"] == (int)this.dtValidCardRecord.Rows[j]["f_ReaderID"])
								{
									dtShiftWorkSchedule.Rows[k]["f_RealPatrolTime"] = value2;
									flag = true;
									this.dtValidCardRecord.Rows[j]["f_used"] = 1;
									break;
								}
								j++;
							}
						}
						if (!flag)
						{
							j = num4;
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
			this.getPatrolParam();
			this.errInfo = "";
			int result = -1;
			try
			{
				for (int i = 0; i <= dtShiftWorkSchedule.Rows.Count - 1; i++)
				{
					if (this.bStopCreate)
					{
						return result;
					}
					object obj = dtShiftWorkSchedule.Rows[i]["f_PlanPatrolTime"];
					if (!Information.IsDBNull(obj))
					{
						if (Information.IsDBNull(dtShiftWorkSchedule.Rows[i]["f_RealPatrolTime"]))
						{
							dtShiftWorkSchedule.Rows[i]["f_EventDesc"] = this.tPatrolEventDescAbsence;
						}
						else
						{
							object value = dtShiftWorkSchedule.Rows[i]["f_RealPatrolTime"];
							TimeSpan timeSpan = Convert.ToDateTime(obj).Subtract(Convert.ToDateTime(value));
							if (Math.Abs(timeSpan.TotalMinutes) <= (double)this.tOnTimePatrolTimeout)
							{
								dtShiftWorkSchedule.Rows[i]["f_EventDesc"] = this.tPatrolEventDescNormal;
							}
							else if (timeSpan.TotalMinutes > (double)this.tOnTimePatrolTimeout)
							{
								dtShiftWorkSchedule.Rows[i]["f_EventDesc"] = this.tPatrolEventDescEarly;
							}
							else if (timeSpan.TotalMinutes < (double)(-(double)this.tOnTimePatrolTimeout))
							{
								dtShiftWorkSchedule.Rows[i]["f_EventDesc"] = this.tPatrolEventDescLate;
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
			this.cn = new OleDbConnection(wgAppConfig.dbConString);
			this.cmd = new OleDbCommand();
			string text = "";
			bool flag = true;
			this.errInfo = "";
			int result = -1;
			try
			{
				if (dtShiftWorkSchedule.Rows.Count > 0)
				{
					using (this.cmd = new OleDbCommand())
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
							text = " INSERT INTO t_d_PatrolDetailData ";
							text += " ( f_ConsumerID, f_PatrolDate, f_RouteID, f_ReaderID, f_PlanPatrolTime, f_RealPatrolTime, f_EventDesc";
							text += " ) ";
							text = text + " Values ( " + dataRow["f_ConsumerID"];
							text = text + "," + this.PrepareStr(dataRow["f_PatrolDate"], true, "yyyy-MM-dd");
							text = text + "," + dataRow["f_RouteID"];
							text = text + "," + dataRow["f_ReaderID"];
							text = text + "," + this.PrepareStr(dataRow["f_PlanPatrolTime"], true, "yyyy-MM-dd HH:mm:ss");
							text = text + "," + this.PrepareStr(dataRow["f_RealPatrolTime"], true, "yyyy-MM-dd HH:mm:ss");
							text = text + "," + this.PrepareStr(dataRow["f_EventDesc"]);
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
				if (wgAppConfig.runUpdateSql("Delete From t_d_PatrolDetailData") >= 0)
				{
					result = 0;
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
				this.dc.ColumnName = "f_PatrolDateStart";
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_PatrolDateEnd";
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_TotalLate";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_LateMinutes";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_TotalEarly";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Decimal");
				this.dc.ColumnName = "f_TotalAbsence";
				this.dc.DefaultValue = 0;
				this.dtReport.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Int32");
				this.dc.ColumnName = "f_TotalNormal";
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
				int num = -1;
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
					dataRow["f_ConsumerID"] = Convert.ToInt32(dataRow2["f_ConsumerID"]);
					if (this.SetObjToStr(dataRow2["f_EventDesc"]) == this.tPatrolEventDescEarly.ToString())
					{
						dataRow["f_TotalEarly"] = Convert.ToInt32(dataRow["f_TotalEarly"]) + 1;
					}
					else if (this.SetObjToStr(dataRow2["f_EventDesc"]) == this.tPatrolEventDescLate.ToString())
					{
						dataRow["f_TotalLate"] = Convert.ToInt32(dataRow["f_TotalLate"]) + 1;
					}
					else if (this.SetObjToStr(dataRow2["f_EventDesc"]) == this.tPatrolEventDescAbsence.ToString())
					{
						dataRow["f_TotalAbsence"] = Convert.ToInt32(dataRow["f_TotalAbsence"]) + 1;
					}
					else if (this.SetObjToStr(dataRow2["f_EventDesc"]) == this.tPatrolEventDescNormal.ToString())
					{
						dataRow["f_TotalNormal"] = Convert.ToInt32(dataRow["f_TotalNormal"]) + 1;
					}
					if (num < 0)
					{
						num = (int)dataRow2["f_ConsumerID"];
					}
					if (num != (int)dataRow2["f_ConsumerID"])
					{
						dtAttReport.Rows.Add(dataRow);
						dataRow = dtAttReport.NewRow();
						num = (int)dataRow2["f_ConsumerID"];
					}
				}
				if (num > 0)
				{
					dtAttReport.Rows.Add(dataRow);
					dataRow = dtAttReport.NewRow();
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

		public int shift_AttReport_writetodb(DataTable dtAttReport)
		{
			this.cn = new OleDbConnection(wgAppConfig.dbConString);
			string text = "";
			this.cmd = new OleDbCommand();
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
						text = " INSERT INTO t_d_PatrolStatistic ";
						text += " ( f_ConsumerID, f_PatrolDateStart, f_PatrolDateEnd ";
						text += " , f_TotalLate, f_TotalEarly, f_TotalAbsence, f_TotalNormal ";
						text += " ) ";
						text = text + " Values ( " + dataRow["f_ConsumerID"];
						text = text + "," + this.PrepareStr(dataRow["f_PatrolDateStart"], true, "yyyy-MM-dd");
						text = text + "," + this.PrepareStr(dataRow["f_PatrolDateEnd"], true, "yyyy-MM-dd");
						text = text + "," + dataRow["f_TotalLate"];
						text = text + "," + dataRow["f_TotalEarly"];
						text = text + "," + dataRow["f_TotalAbsence"];
						text = text + "," + dataRow["f_TotalNormal"];
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
					CommonStr.strPatrolCreateLog,
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
				string text4 = "UPDATE t_a_SystemParam ";
				text4 = text4 + " SET [f_Value]=" + this.PrepareStr(obj);
				text4 = text4 + " , [f_Notes] = " + this.PrepareStr(text);
				text4 += " WHERE [f_NO]= 29 ";
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
				if (wgAppConfig.runUpdateSql("Delete From t_d_PatrolStatistic") >= 0)
				{
					result = 0;
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
	}
}
