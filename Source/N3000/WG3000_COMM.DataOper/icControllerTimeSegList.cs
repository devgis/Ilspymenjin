using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	public class icControllerTimeSegList : wgMjControllerTimeSegList
	{
		public icControllerTimeSegList()
		{
			base.Clear();
		}

		public void fillByDB()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.fillByDB_Acc();
				return;
			}
			base.Clear();
			string cmdText = " SELECT * FROM t_b_ControlSeg  ";
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(136);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						if ((int)sqlDataReader["f_ControlSegID"] <= 254)
						{
							MjControlTimeSeg mjControlTimeSeg = new MjControlTimeSeg();
							mjControlTimeSeg.SegIndex = (byte)((int)sqlDataReader["f_ControlSegID"]);
							mjControlTimeSeg.weekdayControl = 0;
							MjControlTimeSeg expr_8E = mjControlTimeSeg;
							expr_8E.weekdayControl += ((sqlDataReader["f_Monday"].ToString() == "1") ? 1 : 0);
							MjControlTimeSeg expr_BE = mjControlTimeSeg;
							expr_BE.weekdayControl += ((sqlDataReader["f_Tuesday"].ToString() == "1") ? 2 : 0);
							MjControlTimeSeg expr_EE = mjControlTimeSeg;
							expr_EE.weekdayControl += ((sqlDataReader["f_Wednesday"].ToString() == "1") ? 4 : 0);
							MjControlTimeSeg expr_11E = mjControlTimeSeg;
							expr_11E.weekdayControl += ((sqlDataReader["f_Thursday"].ToString() == "1") ? 8 : 0);
							MjControlTimeSeg expr_14E = mjControlTimeSeg;
							expr_14E.weekdayControl += ((sqlDataReader["f_Friday"].ToString() == "1") ? 16 : 0);
							MjControlTimeSeg expr_17F = mjControlTimeSeg;
							expr_17F.weekdayControl += ((sqlDataReader["f_Saturday"].ToString() == "1") ? 32 : 0);
							MjControlTimeSeg expr_1B0 = mjControlTimeSeg;
							expr_1B0.weekdayControl += ((sqlDataReader["f_Sunday"].ToString() == "1") ? 64 : 0);
							mjControlTimeSeg.hmsStart1 = DateTime.Parse(sqlDataReader["f_BeginHMS1"].ToString());
							mjControlTimeSeg.hmsStart2 = DateTime.Parse(sqlDataReader["f_BeginHMS2"].ToString());
							mjControlTimeSeg.hmsStart3 = DateTime.Parse(sqlDataReader["f_BeginHMS3"].ToString());
							mjControlTimeSeg.hmsEnd1 = DateTime.Parse(sqlDataReader["f_EndHMS1"].ToString());
							mjControlTimeSeg.hmsEnd2 = DateTime.Parse(sqlDataReader["f_EndHMS2"].ToString());
							mjControlTimeSeg.hmsEnd3 = DateTime.Parse(sqlDataReader["f_EndHMS3"].ToString());
							mjControlTimeSeg.ymdStart = DateTime.Parse(sqlDataReader["f_BeginYMD"].ToString());
							mjControlTimeSeg.ymdEnd = DateTime.Parse(sqlDataReader["f_EndYMD"].ToString());
							if (paramValBoolByNO)
							{
								mjControlTimeSeg.LimittedMode = int.Parse(sqlDataReader["f_ReaderCount"].ToString());
								mjControlTimeSeg.TotalLimittedAccess = ((int)sqlDataReader["f_LimitedTimesOfDay"] & 255);
								mjControlTimeSeg.MonthLimittedAccess = ((int)sqlDataReader["f_LimitedTimesOfDay"] >> 8 & 255);
								mjControlTimeSeg.LimittedAccess1 = (int)sqlDataReader["f_LimitedTimesOfHMS1"];
								mjControlTimeSeg.LimittedAccess2 = (int)sqlDataReader["f_LimitedTimesOfHMS2"];
								mjControlTimeSeg.LimittedAccess3 = (int)sqlDataReader["f_LimitedTimesOfHMS3"];
							}
							mjControlTimeSeg.nextSeg = (byte)((int)sqlDataReader["f_ControlSegIDLinked"]);
							mjControlTimeSeg.ControlByHoliday = (byte)((int)sqlDataReader["f_ControlByHoliday"]);
							if (base.AddItem(mjControlTimeSeg) != 1)
							{
								break;
							}
						}
					}
					sqlDataReader.Close();
				}
			}
		}

		public void fillByDB_Acc()
		{
			base.Clear();
			string cmdText = " SELECT * FROM t_b_ControlSeg  ";
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(136);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						if ((int)oleDbDataReader["f_ControlSegID"] <= 254)
						{
							MjControlTimeSeg mjControlTimeSeg = new MjControlTimeSeg();
							mjControlTimeSeg.SegIndex = (byte)((int)oleDbDataReader["f_ControlSegID"]);
							mjControlTimeSeg.weekdayControl = 0;
							MjControlTimeSeg expr_80 = mjControlTimeSeg;
							expr_80.weekdayControl += ((oleDbDataReader["f_Monday"].ToString() == "1") ? 1 : 0);
							MjControlTimeSeg expr_B0 = mjControlTimeSeg;
							expr_B0.weekdayControl += ((oleDbDataReader["f_Tuesday"].ToString() == "1") ? 2 : 0);
							MjControlTimeSeg expr_E0 = mjControlTimeSeg;
							expr_E0.weekdayControl += ((oleDbDataReader["f_Wednesday"].ToString() == "1") ? 4 : 0);
							MjControlTimeSeg expr_110 = mjControlTimeSeg;
							expr_110.weekdayControl += ((oleDbDataReader["f_Thursday"].ToString() == "1") ? 8 : 0);
							MjControlTimeSeg expr_140 = mjControlTimeSeg;
							expr_140.weekdayControl += ((oleDbDataReader["f_Friday"].ToString() == "1") ? 16 : 0);
							MjControlTimeSeg expr_171 = mjControlTimeSeg;
							expr_171.weekdayControl += ((oleDbDataReader["f_Saturday"].ToString() == "1") ? 32 : 0);
							MjControlTimeSeg expr_1A2 = mjControlTimeSeg;
							expr_1A2.weekdayControl += ((oleDbDataReader["f_Sunday"].ToString() == "1") ? 64 : 0);
							mjControlTimeSeg.hmsStart1 = DateTime.Parse(oleDbDataReader["f_BeginHMS1"].ToString());
							mjControlTimeSeg.hmsStart2 = DateTime.Parse(oleDbDataReader["f_BeginHMS2"].ToString());
							mjControlTimeSeg.hmsStart3 = DateTime.Parse(oleDbDataReader["f_BeginHMS3"].ToString());
							mjControlTimeSeg.hmsEnd1 = DateTime.Parse(oleDbDataReader["f_EndHMS1"].ToString());
							mjControlTimeSeg.hmsEnd2 = DateTime.Parse(oleDbDataReader["f_EndHMS2"].ToString());
							mjControlTimeSeg.hmsEnd3 = DateTime.Parse(oleDbDataReader["f_EndHMS3"].ToString());
							mjControlTimeSeg.ymdStart = DateTime.Parse(oleDbDataReader["f_BeginYMD"].ToString());
							mjControlTimeSeg.ymdEnd = DateTime.Parse(oleDbDataReader["f_EndYMD"].ToString());
							if (paramValBoolByNO)
							{
								mjControlTimeSeg.LimittedMode = int.Parse(oleDbDataReader["f_ReaderCount"].ToString());
								mjControlTimeSeg.TotalLimittedAccess = ((int)oleDbDataReader["f_LimitedTimesOfDay"] & 255);
								mjControlTimeSeg.MonthLimittedAccess = ((int)oleDbDataReader["f_LimitedTimesOfDay"] >> 8 & 255);
								mjControlTimeSeg.LimittedAccess1 = (int)oleDbDataReader["f_LimitedTimesOfHMS1"];
								mjControlTimeSeg.LimittedAccess2 = (int)oleDbDataReader["f_LimitedTimesOfHMS2"];
								mjControlTimeSeg.LimittedAccess3 = (int)oleDbDataReader["f_LimitedTimesOfHMS3"];
							}
							mjControlTimeSeg.nextSeg = (byte)((int)oleDbDataReader["f_ControlSegIDLinked"]);
							mjControlTimeSeg.ControlByHoliday = (byte)((int)oleDbDataReader["f_ControlByHoliday"]);
							if (base.AddItem(mjControlTimeSeg) != 1)
							{
								break;
							}
						}
					}
					oleDbDataReader.Close();
				}
			}
		}
	}
}
