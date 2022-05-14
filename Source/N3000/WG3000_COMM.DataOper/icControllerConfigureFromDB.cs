using System;
using System.Collections;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	internal class icControllerConfigureFromDB
	{
		public static int getControllerConfigureFromDBByControllerID(int ControllerID, ref wgMjControllerConfigure controlConfigure, ref wgMjControllerTaskList controlTaskList, ref wgMjControllerHolidaysList controlHolidayList)
		{
			DbConnection dbConnection;
			DbCommand dbCommand;
			if (wgAppConfig.IsAccessDB)
			{
				dbConnection = new OleDbConnection(wgAppConfig.dbConString);
				dbCommand = new OleDbCommand("", dbConnection as OleDbConnection);
			}
			else
			{
				dbConnection = new SqlConnection(wgAppConfig.dbConString);
				dbCommand = new SqlCommand("", dbConnection as SqlConnection);
			}
			int num = ControllerID;
			if (num <= 0)
			{
				return -1;
			}
			int controllerSN = 0;
			int num2 = 0;
			dbConnection.Open();
			string text = " SELECT *, f_ZoneNO  FROM t_b_Controller LEFT JOIN t_b_Controller_Zone ON t_b_Controller_Zone.f_ZoneID = t_b_Controller.f_ZoneID WHERE f_ControllerID =  " + num.ToString();
			dbCommand.CommandText = text;
			dbCommand.CommandText = text;
			DbDataReader dbDataReader = dbCommand.ExecuteReader();
			if (dbDataReader.Read())
			{
				controllerSN = (int)dbDataReader["f_ControllerSN"];
				if (!string.IsNullOrEmpty(wgTools.SetObjToStr(dbDataReader["f_ZoneNO"])))
				{
					num2 = int.Parse(wgTools.SetObjToStr(dbDataReader["f_ZoneNO"]));
				}
			}
			dbDataReader.Close();
			text = " SELECT * from t_b_door where [f_controllerID]= " + ControllerID.ToString() + " order by [f_DoorNO] ASC";
			dbCommand.CommandText = text;
			dbDataReader = dbCommand.ExecuteReader();
			int num3 = 0;
			while (dbDataReader.Read())
			{
				num3++;
				controlConfigure.DoorControlSet(num3, (int)dbDataReader["f_DoorControl"]);
				controlConfigure.DoorDelaySet(num3, (int)dbDataReader["f_DoorDelay"]);
				if (wgAppConfig.getParamValBoolByNO(134))
				{
					controlConfigure.MorecardNeedCardsSet(num3, (int)dbDataReader["f_MoreCards_Total"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 1, (int)dbDataReader["f_MoreCards_Grp1"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 2, (int)dbDataReader["f_MoreCards_Grp2"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 3, (int)dbDataReader["f_MoreCards_Grp3"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 4, (int)dbDataReader["f_MoreCards_Grp4"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 5, (int)dbDataReader["f_MoreCards_Grp5"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 6, (int)dbDataReader["f_MoreCards_Grp6"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 7, (int)dbDataReader["f_MoreCards_Grp7"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 8, (int)dbDataReader["f_MoreCards_Grp8"]);
				}
				else
				{
					controlConfigure.MorecardNeedCardsSet(num3, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 1, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 2, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 3, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 4, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 5, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 6, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 7, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 8, 0);
				}
				if ((int)dbDataReader["f_MoreCards_Grp1"] > 0 || (int)dbDataReader["f_MoreCards_Grp2"] > 0 || (int)dbDataReader["f_MoreCards_Grp3"] > 0 || (int)dbDataReader["f_MoreCards_Grp4"] > 0 || (int)dbDataReader["f_MoreCards_Grp5"] > 0 || (int)dbDataReader["f_MoreCards_Grp6"] > 0 || (int)dbDataReader["f_MoreCards_Grp7"] > 0 || (int)dbDataReader["f_MoreCards_Grp8"] > 0)
				{
					controlConfigure.MorecardSequenceInputSet(num3, ((int)dbDataReader["f_MoreCards_Option"] & 16) > 0);
				}
				else
				{
					controlConfigure.MorecardSequenceInputSet(num3, false);
				}
				controlConfigure.MorecardSingleGroupEnableSet(num3, ((int)dbDataReader["f_MoreCards_Option"] & 8) > 0);
				controlConfigure.MorecardSingleGroupStartNOSet(num3, ((int)dbDataReader["f_MoreCards_Option"] & 7) + 1);
				controlConfigure.DoorDisableTimesegMinSet(num3, 0);
			}
			dbDataReader.Close();
			text = " SELECT * from t_b_reader where [f_controllerID]= " + ControllerID.ToString() + " order by [f_ReaderNO] ASC";
			dbCommand.CommandText = text;
			dbDataReader = dbCommand.ExecuteReader();
			int num4 = 0;
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(123);
			while (dbDataReader.Read())
			{
				num4++;
				if (paramValBoolByNO)
				{
					controlConfigure.ReaderPasswordSet(num4, (int)dbDataReader["f_PasswordEnabled"]);
					controlConfigure.InputCardNOOpenSet(num4, (int)dbDataReader["f_InputCardno_Enabled"]);
				}
				else
				{
					controlConfigure.ReaderPasswordSet(num4, 0);
					controlConfigure.InputCardNOOpenSet(num4, 0);
				}
			}
			dbDataReader.Close();
			int num5 = 0;
			if (wgAppConfig.getParamValBoolByNO(146))
			{
				string text2 = "";
				string text3 = "";
				string text4 = "";
				wgAppConfig.getSystemParamValue(146, out text2, out text3, out text4);
				if (!string.IsNullOrEmpty(wgTools.SetObjToStr(text4)))
				{
					text = string.Format(" SELECT * from t_b_door where [f_controllerID]= {0} AND [f_DoorID] IN ({1}) order by [f_DoorNO] ASC", ControllerID.ToString(), text4);
					dbCommand.CommandText = text;
					dbDataReader = dbCommand.ExecuteReader();
					while (dbDataReader.Read())
					{
						num5 |= 1 << int.Parse(wgTools.SetObjToStr(dbDataReader["f_DoorNO"])) - 1;
					}
					dbDataReader.Close();
				}
			}
			controlConfigure.lockSwitchOption = num5;
			controlConfigure.swipeGap = int.Parse("0" + wgAppConfig.getSystemParamByNO(147));
			text = " SELECT * from t_b_Controller where [f_controllerID]= " + ControllerID;
			dbCommand.CommandText = text;
			dbDataReader = dbCommand.ExecuteReader();
			if (dbDataReader.Read())
			{
				controlConfigure.DoorInterlockSet(1, 0);
				controlConfigure.DoorInterlockSet(2, 0);
				controlConfigure.DoorInterlockSet(3, 0);
				controlConfigure.DoorInterlockSet(4, 0);
				if (wgAppConfig.getParamValBoolByNO(133))
				{
					int num6 = (int)dbDataReader["f_InterLock"];
					if (num6 == 1)
					{
						controlConfigure.DoorInterlockSet(1, 49);
						controlConfigure.DoorInterlockSet(2, 50);
					}
					else if (num6 == 2)
					{
						controlConfigure.DoorInterlockSet(3, 196);
						controlConfigure.DoorInterlockSet(4, 200);
					}
					else if (num6 == 3)
					{
						controlConfigure.DoorInterlockSet(1, 49);
						controlConfigure.DoorInterlockSet(2, 50);
						controlConfigure.DoorInterlockSet(3, 196);
						controlConfigure.DoorInterlockSet(4, 200);
					}
					else if (num6 == 4)
					{
						controlConfigure.DoorInterlockSet(1, 113);
						controlConfigure.DoorInterlockSet(2, 114);
						controlConfigure.DoorInterlockSet(3, 116);
					}
					else if (num6 == 8)
					{
						controlConfigure.DoorInterlockSet(1, 241);
						controlConfigure.DoorInterlockSet(2, 242);
						controlConfigure.DoorInterlockSet(3, 244);
						controlConfigure.DoorInterlockSet(4, 248);
					}
				}
				if (wgAppConfig.getParamValBoolByNO(132))
				{
					controlConfigure.antiback = (int)dbDataReader["f_AntiBack"] % 10;
					controlConfigure.indoorPersonsMax = ((int)dbDataReader["f_AntiBack"] - controlConfigure.antiback) / 10;
				}
				else
				{
					controlConfigure.antiback = 0;
					controlConfigure.indoorPersonsMax = 0;
				}
				controlConfigure.moreCardRead4Reader = (int)dbDataReader["f_MoreCards_GoInOut"];
				int doorOpenTimeout = int.Parse(wgAppConfig.getSystemParamByNO(40));
				controlConfigure.doorOpenTimeout = doorOpenTimeout;
				string text5 = wgTools.SetObjToStr(dbDataReader["f_PeripheralControl"]);
				string[] array = text5.Split(new char[]
				{
					','
				});
				if (!wgAppConfig.getParamValBoolByNO(124) || array.Length != 27)
				{
					text5 = "126,30,30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,10,10,10,0,0,0,0";
					array = text5.Split(new char[]
					{
						','
					});
				}
				int[] array2 = new int[4];
				int[] array3 = new int[4];
				int[] array4 = new int[4];
				int[] array5 = new int[4];
				decimal[] array6 = new decimal[4];
				int[] array7 = new int[4];
				int i = 0;
				int ext_AlarmControlMode = int.Parse(array[i++]);
				int ext_SetAlarmOnDelay = int.Parse(array[i++]);
				int ext_SetAlarmOffDelay = int.Parse(array[i++]);
				array2[0] = int.Parse(array[i++]);
				array2[1] = int.Parse(array[i++]);
				array2[2] = int.Parse(array[i++]);
				array2[3] = int.Parse(array[i++]);
				array3[0] = int.Parse(array[i++]);
				array3[1] = int.Parse(array[i++]);
				array3[2] = int.Parse(array[i++]);
				array3[3] = int.Parse(array[i++]);
				array4[0] = int.Parse(array[i++]);
				array4[1] = int.Parse(array[i++]);
				array4[2] = int.Parse(array[i++]);
				array4[3] = int.Parse(array[i++]);
				array5[0] = int.Parse(array[i++]);
				array5[1] = int.Parse(array[i++]);
				array5[2] = int.Parse(array[i++]);
				array5[3] = int.Parse(array[i++]);
				array6[0] = decimal.Parse(array[i++]);
				array6[1] = decimal.Parse(array[i++]);
				array6[2] = decimal.Parse(array[i++]);
				array6[3] = decimal.Parse(array[i++]);
				array7[0] = int.Parse(array[i++]);
				array7[1] = int.Parse(array[i++]);
				array7[2] = int.Parse(array[i++]);
				array7[3] = int.Parse(array[i++]);
				controlConfigure.ext_AlarmControlMode = ext_AlarmControlMode;
				controlConfigure.ext_SetAlarmOnDelay = ext_SetAlarmOnDelay;
				controlConfigure.ext_SetAlarmOffDelay = ext_SetAlarmOffDelay;
				for (i = 0; i < 4; i++)
				{
					if (array7[i] > 0)
					{
						controlConfigure.Ext_doorSet(i, array2[i]);
						controlConfigure.Ext_controlSet(i, array3[i]);
						controlConfigure.Ext_warnSignalEnabledSet(i, array4[i]);
						controlConfigure.Ext_warnSignalEnabled2Set(i, array5[i]);
						controlConfigure.Ext_timeoutSet(i, (int)array6[i]);
					}
					else
					{
						controlConfigure.Ext_doorSet(i, 0);
						controlConfigure.Ext_controlSet(i, 0);
						controlConfigure.Ext_warnSignalEnabledSet(i, 0);
						controlConfigure.Ext_warnSignalEnabled2Set(i, 0);
						controlConfigure.Ext_timeoutSet(i, 0);
					}
				}
				int num7 = 0;
				num7 += (((int)dbDataReader["f_ForceWarn"] > 0) ? 1 : 0);
				num7 += (((int)dbDataReader["f_DoorOpenTooLong"] > 0) ? 2 : 0);
				num7 += (((int)dbDataReader["f_DoorInvalidOpen"] > 0) ? 4 : 0);
				num7 += 8;
				num7 += (((int)dbDataReader["f_InvalidCardWarn"] > 0) ? 16 : 0);
				num7 += 32;
				if ((array2[0] == 16 && array7[0] > 0) || (array2[1] == 16 && array7[1] > 0) || (array2[2] == 16 && array7[2] > 0) || (array2[3] == 16 && array7[3] > 0))
				{
					num7 += 64;
				}
				else
				{
					controlConfigure.ext_Alarm_Status = 0;
				}
				if (wgAppConfig.getParamValBoolByNO(141))
				{
					num7 += 128;
				}
				if (!wgAppConfig.getParamValBoolByNO(124))
				{
					num7 = 0;
				}
				controlConfigure.warnSetup = num7;
				controlConfigure.xpPassword = int.Parse(wgAppConfig.getSystemParamByNO(24));
				if (!wgAppConfig.getParamValBoolByNO(124) || !wgAppConfig.getParamValBoolByNO(60))
				{
					controlConfigure.fire_broadcast_receive = 0;
					controlConfigure.fire_broadcast_send = 0;
				}
				else
				{
					controlConfigure.fire_broadcast_receive = 15;
					controlConfigure.fire_broadcast_send = 1;
					if (wgAppConfig.getSystemParamByNO(60) == "2" && (num2 > 0 & num2 < 253))
					{
						controlConfigure.fire_broadcast_send = num2 + 1;
					}
				}
				if (!wgAppConfig.getParamValBoolByNO(133) || !wgAppConfig.getParamValBoolByNO(61) || wgMjController.GetControllerType(controllerSN) == 1)
				{
					controlConfigure.interlock_broadcast_receive = 0;
					controlConfigure.interlock_broadcast_send = 0;
				}
				else
				{
					controlConfigure.interlock_broadcast_receive = 5;
					controlConfigure.interlock_broadcast_send = 1;
					if (wgAppConfig.getSystemParamByNO(61) == "2" && (num2 > 0 & num2 < 253))
					{
						controlConfigure.interlock_broadcast_send = num2 + 1;
					}
				}
				if (!wgAppConfig.getParamValBoolByNO(132) || !wgAppConfig.getParamValBoolByNO(62))
				{
					controlConfigure.antiback_broadcast_send = 0;
					if (controlConfigure.indoorPersonsMax > 0)
					{
						controlConfigure.antiback_broadcast_send = 254;
					}
				}
				else
				{
					controlConfigure.antiback_broadcast_send = 1;
					if (wgAppConfig.getSystemParamByNO(62) == "2" && (num2 > 0 & num2 < 253))
					{
						controlConfigure.antiback_broadcast_send = num2 + 1;
					}
				}
				controlConfigure.receventWarn = ((num7 > 0) ? 1 : 0);
				controlConfigure.receventPB = (wgAppConfig.getParamValBoolByNO(101) ? 1 : 0);
				controlConfigure.receventDS = (wgAppConfig.getParamValBoolByNO(102) ? 1 : 0);
			}
			dbDataReader.Close();
			int j = 0;
			while (j < 16)
			{
				j++;
				controlConfigure.SuperpasswordSet(j, 65535);
			}
			if (paramValBoolByNO)
			{
				text = " SELECT f_ReaderNO  from t_b_Reader  ";
				text = text + " where [t_b_Reader].[f_ControllerID] = " + ControllerID.ToString();
				text += " order by [f_ReaderNO] ASC";
				dbCommand.CommandText = text;
				ArrayList arrayList = new ArrayList();
				dbDataReader = dbCommand.ExecuteReader();
				while (dbDataReader.Read())
				{
					arrayList.Add(dbDataReader["f_ReaderNO"]);
				}
				dbDataReader.Close();
				text = " SELECT f_Password,t_b_Reader.f_ReaderNO,t_b_ReaderPassword.f_BAll,t_b_ReaderPassword.f_ReaderID   from t_b_ReaderPassword LEFT JOIN  t_b_Reader ON t_b_ReaderPassword.f_ReaderID = t_b_Reader.f_ReaderID ";
				text = text + " where [f_BAll] = 1 Or [t_b_Reader].[f_ControllerID] = " + ControllerID.ToString();
				dbCommand.CommandText = text;
				dbDataReader = dbCommand.ExecuteReader();
				int[] array8 = new int[]
				{
					1,
					1,
					1,
					1
				};
				while (dbDataReader.Read())
				{
					if ((int)dbDataReader["f_BAll"] == 1)
					{
						if (array8[0] <= 4)
						{
							controlConfigure.SuperpasswordSet(array8[0]++, (int)dbDataReader["f_Password"]);
						}
						if (array8[1] <= 4)
						{
							controlConfigure.SuperpasswordSet(4 + array8[1]++, (int)dbDataReader["f_Password"]);
						}
						if (array8[2] <= 4)
						{
							controlConfigure.SuperpasswordSet(8 + array8[2]++, (int)dbDataReader["f_Password"]);
						}
						if (array8[3] <= 4)
						{
							controlConfigure.SuperpasswordSet(12 + array8[3]++, (int)dbDataReader["f_Password"]);
						}
					}
					else
					{
						j = arrayList.IndexOf(dbDataReader["f_ReaderNO"]);
						if (array8[j] <= 4)
						{
							controlConfigure.SuperpasswordSet(array8[j] + j * 4, (int)dbDataReader["f_Password"]);
							array8[j]++;
						}
					}
				}
				dbDataReader.Close();
			}
			controlConfigure.FirstCardInfoSet(1, 0);
			controlConfigure.FirstCardInfoSet(2, 0);
			controlConfigure.FirstCardInfoSet(3, 0);
			controlConfigure.FirstCardInfoSet(4, 0);
			controlTaskList = new wgMjControllerTaskList();
			if (wgAppConfig.getParamValBoolByNO(135))
			{
				text = " SELECT  f_FirstCard_Enabled,f_DoorNO ";
				text += ", f_FirstCard_BeginHMS";
				text += ", f_FirstCard_BeginControl ";
				text += ", f_FirstCard_EndHMS ";
				text += ", f_FirstCard_EndControl";
				text += ", f_FirstCard_Weekday ";
				text = text + " FROM  t_b_door Where f_FirstCard_Enabled> 0 AND [f_ControllerID] = " + ControllerID.ToString();
				text += " ORDER BY f_DoorNO ";
				dbCommand.CommandText = text;
				dbDataReader = dbCommand.ExecuteReader();
				while (dbDataReader.Read())
				{
					MjControlTaskItem mjControlTaskItem = new MjControlTaskItem();
					mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
					mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
					mjControlTaskItem.hms = DateTime.Parse(dbDataReader["f_FirstCard_BeginHMS"].ToString());
					mjControlTaskItem.weekdayControl = (byte)((int)dbDataReader["f_FirstCard_Weekday"]);
					switch ((int)dbDataReader["f_FirstCard_BeginControl"])
					{
					case 0:
						mjControlTaskItem.paramValue = 19;
						break;
					case 1:
						mjControlTaskItem.paramValue = 17;
						break;
					case 2:
						mjControlTaskItem.paramValue = 18;
						break;
					case 3:
						mjControlTaskItem.paramValue = 20;
						break;
					default:
						mjControlTaskItem.paramValue = 0;
						break;
					}
					mjControlTaskItem.paramLoc = (int)(180 + (byte)dbDataReader["f_DoorNO"] - 1);
					if (controlTaskList.AddItem(mjControlTaskItem) < 0)
					{
						wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
					}
					mjControlTaskItem = new MjControlTaskItem();
					mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
					mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
					mjControlTaskItem.hms = DateTime.Parse(dbDataReader["f_FirstCard_EndHMS"].ToString());
					mjControlTaskItem.weekdayControl = (byte)((int)dbDataReader["f_FirstCard_Weekday"]);
					switch ((int)dbDataReader["f_FirstCard_EndControl"])
					{
					case 0:
						mjControlTaskItem.paramValue = 0;
						break;
					case 1:
						mjControlTaskItem.paramValue = 0;
						break;
					case 2:
						mjControlTaskItem.paramValue = 0;
						break;
					case 3:
					{
						mjControlTaskItem.paramValue = 4;
						MjControlTaskItem expr_1163 = mjControlTaskItem;
						expr_1163.paramValue += 16;
						break;
					}
					default:
						mjControlTaskItem.paramValue = 0;
						break;
					}
					mjControlTaskItem.paramLoc = (int)(180 + (byte)dbDataReader["f_DoorNO"] - 1);
					if (controlTaskList.AddItem(mjControlTaskItem) < 0)
					{
						wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
					}
					mjControlTaskItem = new MjControlTaskItem();
					mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
					mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
					mjControlTaskItem.hms = DateTime.Parse(dbDataReader["f_FirstCard_EndHMS"].ToString());
					mjControlTaskItem.weekdayControl = (byte)((int)dbDataReader["f_FirstCard_Weekday"]);
					switch ((int)dbDataReader["f_FirstCard_EndControl"])
					{
					case 0:
						mjControlTaskItem.paramValue = 3;
						break;
					case 1:
						mjControlTaskItem.paramValue = 1;
						break;
					case 2:
						mjControlTaskItem.paramValue = 2;
						break;
					case 3:
						mjControlTaskItem.paramValue = 3;
						break;
					default:
						mjControlTaskItem.paramValue = 3;
						break;
					}
					mjControlTaskItem.paramLoc = (int)(26 + (byte)dbDataReader["f_DoorNO"] - 1);
					if (controlTaskList.AddItem(mjControlTaskItem) < 0)
					{
						wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
					}
				}
				dbDataReader.Close();
			}
			if (wgAppConfig.getParamValBoolByNO(131))
			{
				text = " SELECT t_b_ControllerTaskList.*,t_b_Door.f_DoorNO, t_b_Door.f_ControllerID FROM t_b_ControllerTaskList ";
				text = text + " LEFT JOIN t_b_Door ON t_b_ControllerTaskList.f_DoorID = t_b_Door.f_DoorID  where t_b_ControllerTaskList.[f_DoorID]=0 OR [f_controllerID]= " + ControllerID.ToString();
				dbCommand.CommandText = text;
				dbDataReader = dbCommand.ExecuteReader();
				while (dbDataReader.Read())
				{
					MjControlTaskItem mjControlTaskItem2 = new MjControlTaskItem();
					mjControlTaskItem2.ymdStart = (DateTime)dbDataReader["f_BeginYMD"];
					mjControlTaskItem2.ymdEnd = (DateTime)dbDataReader["f_EndYMD"];
					mjControlTaskItem2.hms = (DateTime)dbDataReader["f_OperateTime"];
					int num8 = 0;
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Sunday"]);
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Saturday"]);
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Friday"]);
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Thursday"]);
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Wednesday"]);
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Tuesday"]);
					num8 = num8 * 2 + (int)((byte)dbDataReader["f_Monday"]);
					mjControlTaskItem2.weekdayControl = (byte)num8;
					mjControlTaskItem2.paramLoc = 0;
					if ((int)dbDataReader["f_DoorID"] == 0)
					{
						switch ((int)dbDataReader["f_DoorControl"])
						{
						case 0:
							mjControlTaskItem2.paramValue = 3;
							for (int k = 0; k < wgMjController.GetControllerType(controllerSN); k++)
							{
								MjControlTaskItem mjControlTaskItem3 = new MjControlTaskItem();
								mjControlTaskItem3.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem3.paramLoc = 26 + k;
								controlTaskList.AddItem(mjControlTaskItem3);
							}
							break;
						case 1:
							mjControlTaskItem2.paramValue = 1;
							for (int l = 0; l < wgMjController.GetControllerType(controllerSN); l++)
							{
								MjControlTaskItem mjControlTaskItem4 = new MjControlTaskItem();
								mjControlTaskItem4.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem4.paramLoc = 26 + l;
								controlTaskList.AddItem(mjControlTaskItem4);
							}
							break;
						case 2:
							mjControlTaskItem2.paramValue = 2;
							for (int m = 0; m < wgMjController.GetControllerType(controllerSN); m++)
							{
								MjControlTaskItem mjControlTaskItem5 = new MjControlTaskItem();
								mjControlTaskItem5.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem5.paramLoc = 26 + m;
								controlTaskList.AddItem(mjControlTaskItem5);
							}
							break;
						case 3:
						case 4:
							mjControlTaskItem2.paramValue = 0;
							if ((int)dbDataReader["f_DoorControl"] == 3)
							{
								mjControlTaskItem2.paramValue = 2;
							}
							for (int n = 0; n < wgMjController.GetControllerType(controllerSN); n++)
							{
								MjControlTaskItem mjControlTaskItem6 = new MjControlTaskItem();
								mjControlTaskItem6.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem6.paramLoc = 256 + n;
								controlTaskList.AddItem(mjControlTaskItem6);
							}
							break;
						case 5:
						case 6:
						case 7:
							mjControlTaskItem2.paramValue = 0;
							if ((int)dbDataReader["f_DoorControl"] == 7 || (int)dbDataReader["f_DoorControl"] == 6)
							{
								mjControlTaskItem2.paramValue = 1;
							}
							for (int num9 = 0; num9 < 4; num9++)
							{
								MjControlTaskItem mjControlTaskItem7 = new MjControlTaskItem();
								mjControlTaskItem7.CopyFrom(mjControlTaskItem2);
								if (wgMjController.GetControllerType(controllerSN) != 4 && (int)dbDataReader["f_DoorControl"] == 6 && (num9 == 1 || num9 == 3))
								{
									mjControlTaskItem7.paramValue = 0;
								}
								mjControlTaskItem7.paramLoc = 38 + num9;
								controlTaskList.AddItem(mjControlTaskItem7);
							}
							break;
						case 8:
						case 9:
							mjControlTaskItem2.paramValue = 0;
							for (int num10 = 0; num10 < wgMjController.GetControllerType(controllerSN); num10++)
							{
								if ((int)dbDataReader["f_DoorControl"] == 8)
								{
									mjControlTaskItem2.paramValue = (byte)controlConfigure.MorecardNeedCardsGet(num10 + 1);
								}
								MjControlTaskItem mjControlTaskItem8 = new MjControlTaskItem();
								mjControlTaskItem8.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem8.paramLoc = 184 + num10;
								controlTaskList.AddItem(mjControlTaskItem8);
							}
							break;
						case 10:
							mjControlTaskItem2.paramValue = 0;
							for (int num11 = 0; num11 < wgMjController.GetControllerType(controllerSN); num11++)
							{
								MjControlTaskItem expr_16BB = mjControlTaskItem2;
								expr_16BB.paramValue += (byte)(1 << num11);
							}
							mjControlTaskItem2.paramLoc = 55;
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						default:
							mjControlTaskItem2.paramValue = 0;
							mjControlTaskItem2.paramLoc = 0;
							break;
						}
					}
					else
					{
						switch ((int)dbDataReader["f_DoorControl"])
						{
						case 0:
							mjControlTaskItem2.paramValue = 3;
							mjControlTaskItem2.paramLoc = (int)(26 + (byte)dbDataReader["f_DoorNO"] - 1);
							if (controlTaskList.AddItem(mjControlTaskItem2) < 0)
							{
								wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
							}
							break;
						case 1:
							mjControlTaskItem2.paramValue = 1;
							mjControlTaskItem2.paramLoc = (int)(26 + (byte)dbDataReader["f_DoorNO"] - 1);
							if (controlTaskList.AddItem(mjControlTaskItem2) < 0)
							{
								wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
							}
							break;
						case 2:
							mjControlTaskItem2.paramValue = 2;
							mjControlTaskItem2.paramLoc = (int)(26 + (byte)dbDataReader["f_DoorNO"] - 1);
							if (controlTaskList.AddItem(mjControlTaskItem2) < 0)
							{
								wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
							}
							break;
						case 3:
						case 4:
							mjControlTaskItem2.paramValue = 0;
							if ((int)dbDataReader["f_DoorControl"] == 3)
							{
								mjControlTaskItem2.paramValue = 2;
							}
							mjControlTaskItem2.paramLoc = 256 + (int)((byte)dbDataReader["f_DoorNO"]) - 1;
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						case 5:
						case 6:
						case 7:
							mjControlTaskItem2.paramValue = 0;
							if ((int)dbDataReader["f_DoorControl"] == 7 || (int)dbDataReader["f_DoorControl"] == 6)
							{
								mjControlTaskItem2.paramValue = 1;
							}
							if (wgMjController.GetControllerType(controllerSN) == 4)
							{
								mjControlTaskItem2.paramLoc = (int)(38 + (byte)dbDataReader["f_DoorNO"] - 1);
								controlTaskList.AddItem(mjControlTaskItem2);
							}
							else if ((byte)dbDataReader["f_DoorNO"] <= 2)
							{
								mjControlTaskItem2.paramLoc = (int)(38 + ((byte)dbDataReader["f_DoorNO"] - 1) * 2);
								MjControlTaskItem mjControlTaskItem9 = new MjControlTaskItem();
								mjControlTaskItem9.CopyFrom(mjControlTaskItem2);
								controlTaskList.AddItem(mjControlTaskItem9);
								if ((int)dbDataReader["f_DoorControl"] == 6)
								{
									mjControlTaskItem2.paramValue = 0;
								}
								mjControlTaskItem2.paramLoc = (int)(38 + ((byte)dbDataReader["f_DoorNO"] - 1) * 2 + 1);
								controlTaskList.AddItem(mjControlTaskItem2);
							}
							break;
						case 8:
						case 9:
							mjControlTaskItem2.paramValue = 0;
							if ((int)dbDataReader["f_DoorControl"] == 8)
							{
								mjControlTaskItem2.paramValue = (byte)controlConfigure.MorecardNeedCardsGet((int)((byte)dbDataReader["f_DoorNO"]));
							}
							mjControlTaskItem2.paramLoc = (int)(184 + (byte)dbDataReader["f_DoorNO"] - 1);
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						case 10:
							mjControlTaskItem2.paramValue = (byte)(1 << (int)((byte)dbDataReader["f_DoorNO"] - 1));
							mjControlTaskItem2.paramLoc = 55;
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						default:
							mjControlTaskItem2.paramValue = 0;
							mjControlTaskItem2.paramLoc = 0;
							break;
						}
					}
				}
				dbDataReader.Close();
			}
			controlConfigure.controlTaskList_enabled = ((controlTaskList.taskCount > 0) ? 1 : 0);
			if (wgAppConfig.getParamValBoolByNO(121))
			{
				text = " SELECT * FROM t_b_ControlHolidays ";
				dbCommand.CommandText = text;
				dbDataReader = dbCommand.ExecuteReader();
				while (dbDataReader.Read())
				{
					MjControlHolidayTime mjControlHolidayTime = new MjControlHolidayTime();
					mjControlHolidayTime.dtStart = (DateTime)dbDataReader["f_BeginYMDHMS"];
					mjControlHolidayTime.dtEnd = (DateTime)dbDataReader["f_EndYMDHMS"];
					mjControlHolidayTime.bForceWork = ((int)dbDataReader["f_forceWork"] == 1);
					controlHolidayList.AddItem(mjControlHolidayTime);
				}
				dbDataReader.Close();
			}
			if (controlHolidayList.holidayCount > 0)
			{
				controlConfigure.holidayControl = 1;
			}
			else
			{
				controlConfigure.holidayControl = 0;
			}
			if (wgAppConfig.getParamValBoolByNO(144) && wgMjController.IsElevator(controllerSN))
			{
				int num12 = 0;
				text = " SELECT * FROM t_b_Floor WHERE [f_ControllerID] = " + ControllerID.ToString();
				dbCommand.CommandText = text;
				dbDataReader = dbCommand.ExecuteReader();
				while (dbDataReader.Read())
				{
					if ((int)dbDataReader["f_FloorNO"] > 0)
					{
						if ((int)dbDataReader["f_FloorNO"] <= 20)
						{
							num12 |= 1;
						}
						else if ((int)dbDataReader["f_FloorNO"] <= 40)
						{
							num12 |= 2;
						}
					}
				}
				dbDataReader.Close();
				try
				{
					int num13 = int.Parse("0" + wgAppConfig.getSystemParamByNO(144));
					if (num13 > 3)
					{
						controlConfigure.elevatorSingleDelay = (float)((num13 >> 8 & 255) / 10m);
						controlConfigure.elevatorMultioutputDelay = (float)((num13 >> 16 & 255) / 10m);
					}
					else
					{
						controlConfigure.elevatorSingleDelay = 0.4f;
						controlConfigure.elevatorMultioutputDelay = 5f;
					}
				}
				catch (Exception)
				{
				}
			}
			return 1;
		}

		private static int getControllerConfigureFromDBByControllerID_Acc(int ControllerID, ref wgMjControllerConfigure controlConfigure, ref wgMjControllerTaskList controlTaskList, ref wgMjControllerHolidaysList controlHolidayList)
		{
			int num = ControllerID;
			if (num <= 0)
			{
				return -1;
			}
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			int controllerSN = 0;
			int num2 = 0;
			oleDbConnection.Open();
			string text = " SELECT *, f_ZoneNO  FROM t_b_Controller LEFT JOIN t_b_Controller_Zone ON t_b_Controller_Zone.f_ZoneID = t_b_Controller.f_ZoneID WHERE f_ControllerID =  " + num.ToString();
			OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
			oleDbCommand.CommandText = text;
			OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
			if (oleDbDataReader.Read())
			{
				controllerSN = (int)oleDbDataReader["f_ControllerSN"];
				if (!string.IsNullOrEmpty(wgTools.SetObjToStr(oleDbDataReader["f_ZoneNO"])))
				{
					num2 = int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ZoneNO"]));
				}
			}
			oleDbDataReader.Close();
			text = " SELECT * from t_b_door where [f_controllerID]= " + ControllerID.ToString() + " order by [f_DoorNO] ASC";
			oleDbCommand.CommandText = text;
			oleDbDataReader = oleDbCommand.ExecuteReader();
			int num3 = 0;
			while (oleDbDataReader.Read())
			{
				num3++;
				controlConfigure.DoorControlSet(num3, (int)oleDbDataReader["f_DoorControl"]);
				controlConfigure.DoorDelaySet(num3, (int)oleDbDataReader["f_DoorDelay"]);
				if (wgAppConfig.getParamValBoolByNO(134))
				{
					controlConfigure.MorecardNeedCardsSet(num3, (int)oleDbDataReader["f_MoreCards_Total"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 1, (int)oleDbDataReader["f_MoreCards_Grp1"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 2, (int)oleDbDataReader["f_MoreCards_Grp2"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 3, (int)oleDbDataReader["f_MoreCards_Grp3"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 4, (int)oleDbDataReader["f_MoreCards_Grp4"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 5, (int)oleDbDataReader["f_MoreCards_Grp5"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 6, (int)oleDbDataReader["f_MoreCards_Grp6"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 7, (int)oleDbDataReader["f_MoreCards_Grp7"]);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 8, (int)oleDbDataReader["f_MoreCards_Grp8"]);
				}
				else
				{
					controlConfigure.MorecardNeedCardsSet(num3, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 1, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 2, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 3, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 4, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 5, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 6, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 7, 0);
					controlConfigure.MorecardGroupNeedCardsSet(num3, 8, 0);
				}
				if ((int)oleDbDataReader["f_MoreCards_Grp1"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp2"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp3"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp4"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp5"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp6"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp7"] > 0 || (int)oleDbDataReader["f_MoreCards_Grp8"] > 0)
				{
					controlConfigure.MorecardSequenceInputSet(num3, ((int)oleDbDataReader["f_MoreCards_Option"] & 16) > 0);
				}
				else
				{
					controlConfigure.MorecardSequenceInputSet(num3, false);
				}
				controlConfigure.MorecardSingleGroupEnableSet(num3, ((int)oleDbDataReader["f_MoreCards_Option"] & 8) > 0);
				controlConfigure.MorecardSingleGroupStartNOSet(num3, ((int)oleDbDataReader["f_MoreCards_Option"] & 7) + 1);
				controlConfigure.DoorDisableTimesegMinSet(num3, 0);
			}
			oleDbDataReader.Close();
			text = " SELECT * from t_b_reader where [f_controllerID]= " + ControllerID.ToString() + " order by [f_ReaderNO] ASC";
			oleDbCommand.CommandText = text;
			oleDbDataReader = oleDbCommand.ExecuteReader();
			int num4 = 0;
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(123);
			while (oleDbDataReader.Read())
			{
				num4++;
				if (paramValBoolByNO)
				{
					controlConfigure.ReaderPasswordSet(num4, (int)oleDbDataReader["f_PasswordEnabled"]);
					controlConfigure.InputCardNOOpenSet(num4, (int)oleDbDataReader["f_InputCardno_Enabled"]);
				}
				else
				{
					controlConfigure.ReaderPasswordSet(num4, 0);
					controlConfigure.InputCardNOOpenSet(num4, 0);
				}
			}
			oleDbDataReader.Close();
			int num5 = 0;
			if (wgAppConfig.getParamValBoolByNO(146))
			{
				string text2 = "";
				string text3 = "";
				string text4 = "";
				wgAppConfig.getSystemParamValue(146, out text2, out text3, out text4);
				if (!string.IsNullOrEmpty(wgTools.SetObjToStr(text4)))
				{
					text = string.Format(" SELECT * from t_b_door where [f_controllerID]= {0} AND [f_DoorID] IN ({1}) order by [f_DoorNO] ASC", ControllerID.ToString(), text4);
					oleDbCommand.CommandText = text;
					oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						num5 |= 1 << int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_DoorNO"])) - 1;
					}
					oleDbDataReader.Close();
				}
			}
			controlConfigure.lockSwitchOption = num5;
			controlConfigure.swipeGap = int.Parse("0" + wgAppConfig.getSystemParamByNO(147));
			text = " SELECT * from t_b_Controller where [f_controllerID]= " + ControllerID;
			oleDbCommand.CommandText = text;
			oleDbDataReader = oleDbCommand.ExecuteReader();
			if (oleDbDataReader.Read())
			{
				controlConfigure.DoorInterlockSet(1, 0);
				controlConfigure.DoorInterlockSet(2, 0);
				controlConfigure.DoorInterlockSet(3, 0);
				controlConfigure.DoorInterlockSet(4, 0);
				if (wgAppConfig.getParamValBoolByNO(133))
				{
					int num6 = (int)oleDbDataReader["f_InterLock"];
					if (num6 == 1)
					{
						controlConfigure.DoorInterlockSet(1, 49);
						controlConfigure.DoorInterlockSet(2, 50);
					}
					else if (num6 == 2)
					{
						controlConfigure.DoorInterlockSet(3, 196);
						controlConfigure.DoorInterlockSet(4, 200);
					}
					else if (num6 == 3)
					{
						controlConfigure.DoorInterlockSet(1, 49);
						controlConfigure.DoorInterlockSet(2, 50);
						controlConfigure.DoorInterlockSet(3, 196);
						controlConfigure.DoorInterlockSet(4, 200);
					}
					else if (num6 == 4)
					{
						controlConfigure.DoorInterlockSet(1, 113);
						controlConfigure.DoorInterlockSet(2, 114);
						controlConfigure.DoorInterlockSet(3, 116);
					}
					else if (num6 == 8)
					{
						controlConfigure.DoorInterlockSet(1, 241);
						controlConfigure.DoorInterlockSet(2, 242);
						controlConfigure.DoorInterlockSet(3, 244);
						controlConfigure.DoorInterlockSet(4, 248);
					}
				}
				if (wgAppConfig.getParamValBoolByNO(132))
				{
					controlConfigure.antiback = (int)oleDbDataReader["f_AntiBack"] % 10;
					controlConfigure.indoorPersonsMax = ((int)oleDbDataReader["f_AntiBack"] - controlConfigure.antiback) / 10;
				}
				else
				{
					controlConfigure.antiback = 0;
					controlConfigure.indoorPersonsMax = 0;
				}
				controlConfigure.moreCardRead4Reader = (int)oleDbDataReader["f_MoreCards_GoInOut"];
				int doorOpenTimeout = int.Parse(wgAppConfig.getSystemParamByNO(40));
				controlConfigure.doorOpenTimeout = doorOpenTimeout;
				string text5 = wgTools.SetObjToStr(oleDbDataReader["f_PeripheralControl"]);
				string[] array = text5.Split(new char[]
				{
					','
				});
				if (!wgAppConfig.getParamValBoolByNO(124) || array.Length != 27)
				{
					text5 = "126,30,30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,10,10,10,0,0,0,0";
					array = text5.Split(new char[]
					{
						','
					});
				}
				int[] array2 = new int[4];
				int[] array3 = new int[4];
				int[] array4 = new int[4];
				int[] array5 = new int[4];
				decimal[] array6 = new decimal[4];
				int[] array7 = new int[4];
				int i = 0;
				int ext_AlarmControlMode = int.Parse(array[i++]);
				int ext_SetAlarmOnDelay = int.Parse(array[i++]);
				int ext_SetAlarmOffDelay = int.Parse(array[i++]);
				array2[0] = int.Parse(array[i++]);
				array2[1] = int.Parse(array[i++]);
				array2[2] = int.Parse(array[i++]);
				array2[3] = int.Parse(array[i++]);
				array3[0] = int.Parse(array[i++]);
				array3[1] = int.Parse(array[i++]);
				array3[2] = int.Parse(array[i++]);
				array3[3] = int.Parse(array[i++]);
				array4[0] = int.Parse(array[i++]);
				array4[1] = int.Parse(array[i++]);
				array4[2] = int.Parse(array[i++]);
				array4[3] = int.Parse(array[i++]);
				array5[0] = int.Parse(array[i++]);
				array5[1] = int.Parse(array[i++]);
				array5[2] = int.Parse(array[i++]);
				array5[3] = int.Parse(array[i++]);
				array6[0] = decimal.Parse(array[i++]);
				array6[1] = decimal.Parse(array[i++]);
				array6[2] = decimal.Parse(array[i++]);
				array6[3] = decimal.Parse(array[i++]);
				array7[0] = int.Parse(array[i++]);
				array7[1] = int.Parse(array[i++]);
				array7[2] = int.Parse(array[i++]);
				array7[3] = int.Parse(array[i++]);
				controlConfigure.ext_AlarmControlMode = ext_AlarmControlMode;
				controlConfigure.ext_SetAlarmOnDelay = ext_SetAlarmOnDelay;
				controlConfigure.ext_SetAlarmOffDelay = ext_SetAlarmOffDelay;
				for (i = 0; i < 4; i++)
				{
					if (array7[i] > 0)
					{
						controlConfigure.Ext_doorSet(i, array2[i]);
						controlConfigure.Ext_controlSet(i, array3[i]);
						controlConfigure.Ext_warnSignalEnabledSet(i, array4[i]);
						controlConfigure.Ext_warnSignalEnabled2Set(i, array5[i]);
						controlConfigure.Ext_timeoutSet(i, (int)array6[i]);
					}
					else
					{
						controlConfigure.Ext_doorSet(i, 0);
						controlConfigure.Ext_controlSet(i, 0);
						controlConfigure.Ext_warnSignalEnabledSet(i, 0);
						controlConfigure.Ext_warnSignalEnabled2Set(i, 0);
						controlConfigure.Ext_timeoutSet(i, 0);
					}
				}
				int num7 = 0;
				num7 += (((int)oleDbDataReader["f_ForceWarn"] > 0) ? 1 : 0);
				num7 += (((int)oleDbDataReader["f_DoorOpenTooLong"] > 0) ? 2 : 0);
				num7 += (((int)oleDbDataReader["f_DoorInvalidOpen"] > 0) ? 4 : 0);
				num7 += 8;
				num7 += (((int)oleDbDataReader["f_InvalidCardWarn"] > 0) ? 16 : 0);
				num7 += 32;
				if ((array2[0] == 16 && array7[0] > 0) || (array2[1] == 16 && array7[1] > 0) || (array2[2] == 16 && array7[2] > 0) || (array2[3] == 16 && array7[3] > 0))
				{
					num7 += 64;
				}
				else
				{
					controlConfigure.ext_Alarm_Status = 0;
				}
				if (wgAppConfig.getParamValBoolByNO(141))
				{
					num7 += 128;
				}
				if (!wgAppConfig.getParamValBoolByNO(124))
				{
					num7 = 0;
				}
				controlConfigure.warnSetup = num7;
				controlConfigure.xpPassword = int.Parse(wgAppConfig.getSystemParamByNO(24));
				if (!wgAppConfig.getParamValBoolByNO(124) || !wgAppConfig.getParamValBoolByNO(60))
				{
					controlConfigure.fire_broadcast_receive = 0;
					controlConfigure.fire_broadcast_send = 0;
				}
				else
				{
					controlConfigure.fire_broadcast_receive = 15;
					controlConfigure.fire_broadcast_send = 1;
					if (wgAppConfig.getSystemParamByNO(60) == "2" && (num2 > 0 & num2 < 253))
					{
						controlConfigure.fire_broadcast_send = num2 + 1;
					}
				}
				if (!wgAppConfig.getParamValBoolByNO(133) || !wgAppConfig.getParamValBoolByNO(61))
				{
					controlConfigure.interlock_broadcast_receive = 0;
					controlConfigure.interlock_broadcast_send = 0;
				}
				else
				{
					controlConfigure.interlock_broadcast_receive = 5;
					controlConfigure.interlock_broadcast_send = 1;
					if (wgAppConfig.getSystemParamByNO(61) == "2" && (num2 > 0 & num2 < 253))
					{
						controlConfigure.interlock_broadcast_send = num2 + 1;
					}
				}
				if (!wgAppConfig.getParamValBoolByNO(132) || !wgAppConfig.getParamValBoolByNO(62))
				{
					controlConfigure.antiback_broadcast_send = 0;
				}
				else
				{
					controlConfigure.antiback_broadcast_send = 1;
					if (wgAppConfig.getSystemParamByNO(62) == "2" && (num2 > 0 & num2 < 253))
					{
						controlConfigure.antiback_broadcast_send = num2 + 1;
					}
				}
				controlConfigure.receventWarn = ((num7 > 0) ? 1 : 0);
				controlConfigure.receventPB = (wgAppConfig.getParamValBoolByNO(101) ? 1 : 0);
				controlConfigure.receventDS = (wgAppConfig.getParamValBoolByNO(102) ? 1 : 0);
			}
			oleDbDataReader.Close();
			int j = 0;
			while (j < 16)
			{
				j++;
				controlConfigure.SuperpasswordSet(j, 65535);
			}
			if (paramValBoolByNO)
			{
				text = " SELECT f_ReaderNO  from t_b_Reader  ";
				text = text + " where [t_b_Reader].[f_ControllerID] = " + ControllerID.ToString();
				text += " order by [f_ReaderNO] ASC";
				oleDbCommand.CommandText = text;
				ArrayList arrayList = new ArrayList();
				oleDbDataReader = oleDbCommand.ExecuteReader();
				while (oleDbDataReader.Read())
				{
					arrayList.Add(oleDbDataReader["f_ReaderNO"]);
				}
				oleDbDataReader.Close();
				text = " SELECT f_Password,t_b_Reader.f_ReaderNO,t_b_ReaderPassword.f_BAll,t_b_ReaderPassword.f_ReaderID   from t_b_ReaderPassword LEFT JOIN  t_b_Reader ON t_b_ReaderPassword.f_ReaderID = t_b_Reader.f_ReaderID ";
				text = text + " where [f_BAll] = 1 Or [t_b_Reader].[f_ControllerID] = " + ControllerID.ToString();
				oleDbCommand.CommandText = text;
				oleDbDataReader = oleDbCommand.ExecuteReader();
				int[] array8 = new int[]
				{
					1,
					1,
					1,
					1
				};
				while (oleDbDataReader.Read())
				{
					if ((int)oleDbDataReader["f_BAll"] == 1)
					{
						if (array8[0] <= 4)
						{
							controlConfigure.SuperpasswordSet(array8[0]++, (int)oleDbDataReader["f_Password"]);
						}
						if (array8[1] <= 4)
						{
							controlConfigure.SuperpasswordSet(4 + array8[1]++, (int)oleDbDataReader["f_Password"]);
						}
						if (array8[2] <= 4)
						{
							controlConfigure.SuperpasswordSet(8 + array8[2]++, (int)oleDbDataReader["f_Password"]);
						}
						if (array8[3] <= 4)
						{
							controlConfigure.SuperpasswordSet(12 + array8[3]++, (int)oleDbDataReader["f_Password"]);
						}
					}
					else
					{
						j = arrayList.IndexOf(oleDbDataReader["f_ReaderNO"]);
						if (array8[j] <= 4)
						{
							controlConfigure.SuperpasswordSet(array8[j] + j * 4, (int)oleDbDataReader["f_Password"]);
							array8[j]++;
						}
					}
				}
				oleDbDataReader.Close();
			}
			controlConfigure.FirstCardInfoSet(1, 0);
			controlConfigure.FirstCardInfoSet(2, 0);
			controlConfigure.FirstCardInfoSet(3, 0);
			controlConfigure.FirstCardInfoSet(4, 0);
			controlTaskList = new wgMjControllerTaskList();
			if (wgAppConfig.getParamValBoolByNO(135))
			{
				text = " SELECT  f_FirstCard_Enabled,f_DoorNO ";
				text += ", f_FirstCard_BeginHMS";
				text += ", f_FirstCard_BeginControl ";
				text += ", f_FirstCard_EndHMS ";
				text += ", f_FirstCard_EndControl";
				text += ", f_FirstCard_Weekday ";
				text = text + " FROM  t_b_door Where f_FirstCard_Enabled> 0 AND [f_ControllerID] = " + ControllerID.ToString();
				text += " ORDER BY f_DoorNO ";
				oleDbCommand.CommandText = text;
				oleDbDataReader = oleDbCommand.ExecuteReader();
				while (oleDbDataReader.Read())
				{
					MjControlTaskItem mjControlTaskItem = new MjControlTaskItem();
					mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
					mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
					mjControlTaskItem.hms = DateTime.Parse(oleDbDataReader["f_FirstCard_BeginHMS"].ToString());
					mjControlTaskItem.weekdayControl = (byte)((int)oleDbDataReader["f_FirstCard_Weekday"]);
					switch ((int)oleDbDataReader["f_FirstCard_BeginControl"])
					{
					case 0:
						mjControlTaskItem.paramValue = 19;
						break;
					case 1:
						mjControlTaskItem.paramValue = 17;
						break;
					case 2:
						mjControlTaskItem.paramValue = 18;
						break;
					case 3:
						mjControlTaskItem.paramValue = 20;
						break;
					default:
						mjControlTaskItem.paramValue = 0;
						break;
					}
					mjControlTaskItem.paramLoc = (int)(180 + (byte)oleDbDataReader["f_DoorNO"] - 1);
					if (controlTaskList.AddItem(mjControlTaskItem) < 0)
					{
						wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
					}
					mjControlTaskItem = new MjControlTaskItem();
					mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
					mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
					mjControlTaskItem.hms = DateTime.Parse(oleDbDataReader["f_FirstCard_EndHMS"].ToString());
					mjControlTaskItem.weekdayControl = (byte)((int)oleDbDataReader["f_FirstCard_Weekday"]);
					switch ((int)oleDbDataReader["f_FirstCard_EndControl"])
					{
					case 0:
						mjControlTaskItem.paramValue = 0;
						break;
					case 1:
						mjControlTaskItem.paramValue = 0;
						break;
					case 2:
						mjControlTaskItem.paramValue = 0;
						break;
					case 3:
					{
						mjControlTaskItem.paramValue = 4;
						MjControlTaskItem expr_1112 = mjControlTaskItem;
						expr_1112.paramValue += 16;
						break;
					}
					default:
						mjControlTaskItem.paramValue = 0;
						break;
					}
					mjControlTaskItem.paramLoc = (int)(180 + (byte)oleDbDataReader["f_DoorNO"] - 1);
					if (controlTaskList.AddItem(mjControlTaskItem) < 0)
					{
						wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
					}
					mjControlTaskItem = new MjControlTaskItem();
					mjControlTaskItem.ymdStart = DateTime.Parse("2010-1-1");
					mjControlTaskItem.ymdEnd = DateTime.Parse("2029-12-31");
					mjControlTaskItem.hms = DateTime.Parse(oleDbDataReader["f_FirstCard_EndHMS"].ToString());
					mjControlTaskItem.weekdayControl = (byte)((int)oleDbDataReader["f_FirstCard_Weekday"]);
					switch ((int)oleDbDataReader["f_FirstCard_EndControl"])
					{
					case 0:
						mjControlTaskItem.paramValue = 3;
						break;
					case 1:
						mjControlTaskItem.paramValue = 1;
						break;
					case 2:
						mjControlTaskItem.paramValue = 2;
						break;
					case 3:
						mjControlTaskItem.paramValue = 3;
						break;
					default:
						mjControlTaskItem.paramValue = 3;
						break;
					}
					mjControlTaskItem.paramLoc = (int)(26 + (byte)oleDbDataReader["f_DoorNO"] - 1);
					if (controlTaskList.AddItem(mjControlTaskItem) < 0)
					{
						wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
					}
				}
				oleDbDataReader.Close();
			}
			if (wgAppConfig.getParamValBoolByNO(131))
			{
				text = " SELECT t_b_ControllerTaskList.*,t_b_Door.f_DoorNO, t_b_Door.f_ControllerID FROM t_b_ControllerTaskList ";
				text = text + " LEFT JOIN t_b_Door ON t_b_ControllerTaskList.f_DoorID = t_b_Door.f_DoorID  where t_b_ControllerTaskList.[f_DoorID]=0 OR [f_controllerID]= " + ControllerID.ToString();
				oleDbCommand.CommandText = text;
				oleDbDataReader = oleDbCommand.ExecuteReader();
				while (oleDbDataReader.Read())
				{
					MjControlTaskItem mjControlTaskItem2 = new MjControlTaskItem();
					mjControlTaskItem2.ymdStart = (DateTime)oleDbDataReader["f_BeginYMD"];
					mjControlTaskItem2.ymdEnd = (DateTime)oleDbDataReader["f_EndYMD"];
					mjControlTaskItem2.hms = (DateTime)oleDbDataReader["f_OperateTime"];
					int num8 = 0;
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Sunday"]);
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Saturday"]);
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Friday"]);
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Thursday"]);
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Wednesday"]);
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Tuesday"]);
					num8 = num8 * 2 + (int)((byte)oleDbDataReader["f_Monday"]);
					mjControlTaskItem2.weekdayControl = (byte)num8;
					mjControlTaskItem2.paramLoc = 0;
					if ((int)oleDbDataReader["f_DoorID"] == 0)
					{
						switch ((int)oleDbDataReader["f_DoorControl"])
						{
						case 0:
							mjControlTaskItem2.paramValue = 3;
							for (int k = 0; k < wgMjController.GetControllerType(controllerSN); k++)
							{
								MjControlTaskItem mjControlTaskItem3 = new MjControlTaskItem();
								mjControlTaskItem3.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem3.paramLoc = 26 + k;
								controlTaskList.AddItem(mjControlTaskItem3);
							}
							break;
						case 1:
							mjControlTaskItem2.paramValue = 1;
							for (int l = 0; l < wgMjController.GetControllerType(controllerSN); l++)
							{
								MjControlTaskItem mjControlTaskItem4 = new MjControlTaskItem();
								mjControlTaskItem4.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem4.paramLoc = 26 + l;
								controlTaskList.AddItem(mjControlTaskItem4);
							}
							break;
						case 2:
							mjControlTaskItem2.paramValue = 2;
							for (int m = 0; m < wgMjController.GetControllerType(controllerSN); m++)
							{
								MjControlTaskItem mjControlTaskItem5 = new MjControlTaskItem();
								mjControlTaskItem5.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem5.paramLoc = 26 + m;
								controlTaskList.AddItem(mjControlTaskItem5);
							}
							break;
						case 3:
						case 4:
							mjControlTaskItem2.paramValue = 0;
							if ((int)oleDbDataReader["f_DoorControl"] == 3)
							{
								mjControlTaskItem2.paramValue = 2;
							}
							for (int n = 0; n < wgMjController.GetControllerType(controllerSN); n++)
							{
								MjControlTaskItem mjControlTaskItem6 = new MjControlTaskItem();
								mjControlTaskItem6.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem6.paramLoc = 256 + n;
								controlTaskList.AddItem(mjControlTaskItem6);
							}
							break;
						case 5:
						case 6:
						case 7:
							mjControlTaskItem2.paramValue = 0;
							if ((int)oleDbDataReader["f_DoorControl"] == 7 || (int)oleDbDataReader["f_DoorControl"] == 6)
							{
								mjControlTaskItem2.paramValue = 1;
							}
							for (int num9 = 0; num9 < 4; num9++)
							{
								MjControlTaskItem mjControlTaskItem7 = new MjControlTaskItem();
								mjControlTaskItem7.CopyFrom(mjControlTaskItem2);
								if (wgMjController.GetControllerType(controllerSN) != 4 && (int)oleDbDataReader["f_DoorControl"] == 6 && (num9 == 1 || num9 == 3))
								{
									mjControlTaskItem7.paramValue = 0;
								}
								mjControlTaskItem7.paramLoc = 38 + num9;
								controlTaskList.AddItem(mjControlTaskItem7);
							}
							break;
						case 8:
						case 9:
							mjControlTaskItem2.paramValue = 0;
							for (int num10 = 0; num10 < wgMjController.GetControllerType(controllerSN); num10++)
							{
								if ((int)oleDbDataReader["f_DoorControl"] == 8)
								{
									mjControlTaskItem2.paramValue = (byte)controlConfigure.MorecardNeedCardsGet(num10 + 1);
								}
								MjControlTaskItem mjControlTaskItem8 = new MjControlTaskItem();
								mjControlTaskItem8.CopyFrom(mjControlTaskItem2);
								mjControlTaskItem8.paramLoc = 184 + num10;
								controlTaskList.AddItem(mjControlTaskItem8);
							}
							break;
						case 10:
							mjControlTaskItem2.paramValue = 0;
							for (int num11 = 0; num11 < wgMjController.GetControllerType(controllerSN); num11++)
							{
								MjControlTaskItem expr_166A = mjControlTaskItem2;
								expr_166A.paramValue += (byte)(1 << num11);
							}
							mjControlTaskItem2.paramLoc = 55;
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						default:
							mjControlTaskItem2.paramValue = 0;
							mjControlTaskItem2.paramLoc = 0;
							break;
						}
					}
					else
					{
						switch ((int)oleDbDataReader["f_DoorControl"])
						{
						case 0:
							mjControlTaskItem2.paramValue = 3;
							mjControlTaskItem2.paramLoc = (int)(26 + (byte)oleDbDataReader["f_DoorNO"] - 1);
							if (controlTaskList.AddItem(mjControlTaskItem2) < 0)
							{
								wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
							}
							break;
						case 1:
							mjControlTaskItem2.paramValue = 1;
							mjControlTaskItem2.paramLoc = (int)(26 + (byte)oleDbDataReader["f_DoorNO"] - 1);
							if (controlTaskList.AddItem(mjControlTaskItem2) < 0)
							{
								wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
							}
							break;
						case 2:
							mjControlTaskItem2.paramValue = 2;
							mjControlTaskItem2.paramLoc = (int)(26 + (byte)oleDbDataReader["f_DoorNO"] - 1);
							if (controlTaskList.AddItem(mjControlTaskItem2) < 0)
							{
								wgTools.WgDebugWrite("controlTaskList.add(mjCI)", new object[0]);
							}
							break;
						case 3:
						case 4:
							mjControlTaskItem2.paramValue = 0;
							if ((int)oleDbDataReader["f_DoorControl"] == 3)
							{
								mjControlTaskItem2.paramValue = 2;
							}
							mjControlTaskItem2.paramLoc = 256 + (int)((byte)oleDbDataReader["f_DoorNO"]) - 1;
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						case 5:
						case 6:
						case 7:
							mjControlTaskItem2.paramValue = 0;
							if ((int)oleDbDataReader["f_DoorControl"] == 7 || (int)oleDbDataReader["f_DoorControl"] == 6)
							{
								mjControlTaskItem2.paramValue = 1;
							}
							if (wgMjController.GetControllerType(controllerSN) == 4)
							{
								mjControlTaskItem2.paramLoc = (int)(38 + (byte)oleDbDataReader["f_DoorNO"] - 1);
								controlTaskList.AddItem(mjControlTaskItem2);
							}
							else if ((byte)oleDbDataReader["f_DoorNO"] <= 2)
							{
								mjControlTaskItem2.paramLoc = (int)(38 + ((byte)oleDbDataReader["f_DoorNO"] - 1) * 2);
								MjControlTaskItem mjControlTaskItem9 = new MjControlTaskItem();
								mjControlTaskItem9.CopyFrom(mjControlTaskItem2);
								controlTaskList.AddItem(mjControlTaskItem9);
								if ((int)oleDbDataReader["f_DoorControl"] == 6)
								{
									mjControlTaskItem2.paramValue = 0;
								}
								mjControlTaskItem2.paramLoc = (int)(38 + ((byte)oleDbDataReader["f_DoorNO"] - 1) * 2 + 1);
								controlTaskList.AddItem(mjControlTaskItem2);
							}
							break;
						case 8:
						case 9:
							mjControlTaskItem2.paramValue = 0;
							if ((int)oleDbDataReader["f_DoorControl"] == 8)
							{
								mjControlTaskItem2.paramValue = (byte)controlConfigure.MorecardNeedCardsGet((int)((byte)oleDbDataReader["f_DoorNO"]));
							}
							mjControlTaskItem2.paramLoc = (int)(184 + (byte)oleDbDataReader["f_DoorNO"] - 1);
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						case 10:
							mjControlTaskItem2.paramValue = (byte)(1 << (int)((byte)oleDbDataReader["f_DoorNO"] - 1));
							mjControlTaskItem2.paramLoc = 55;
							controlTaskList.AddItem(mjControlTaskItem2);
							break;
						default:
							mjControlTaskItem2.paramValue = 0;
							mjControlTaskItem2.paramLoc = 0;
							break;
						}
					}
				}
				oleDbDataReader.Close();
			}
			controlConfigure.controlTaskList_enabled = ((controlTaskList.taskCount > 0) ? 1 : 0);
			if (wgAppConfig.getParamValBoolByNO(121))
			{
				text = " SELECT * FROM t_b_ControlHolidays ";
				oleDbCommand.CommandText = text;
				oleDbDataReader = oleDbCommand.ExecuteReader();
				while (oleDbDataReader.Read())
				{
					MjControlHolidayTime mjControlHolidayTime = new MjControlHolidayTime();
					mjControlHolidayTime.dtStart = (DateTime)oleDbDataReader["f_BeginYMDHMS"];
					mjControlHolidayTime.dtEnd = (DateTime)oleDbDataReader["f_EndYMDHMS"];
					mjControlHolidayTime.bForceWork = ((int)oleDbDataReader["f_forceWork"] == 1);
					controlHolidayList.AddItem(mjControlHolidayTime);
				}
				oleDbDataReader.Close();
			}
			if (controlHolidayList.holidayCount > 0)
			{
				controlConfigure.holidayControl = 1;
			}
			else
			{
				controlConfigure.holidayControl = 0;
			}
			if (wgAppConfig.getParamValBoolByNO(144) && wgMjController.IsElevator(controllerSN))
			{
				int num12 = 0;
				text = " SELECT * FROM t_b_Floor WHERE [f_ControllerID] = " + ControllerID.ToString();
				oleDbCommand.CommandText = text;
				oleDbDataReader = oleDbCommand.ExecuteReader();
				while (oleDbDataReader.Read())
				{
					if ((int)oleDbDataReader["f_FloorNO"] > 0)
					{
						if ((int)oleDbDataReader["f_FloorNO"] <= 20)
						{
							num12 |= 1;
						}
						else if ((int)oleDbDataReader["f_FloorNO"] <= 40)
						{
							num12 |= 2;
						}
					}
				}
				oleDbDataReader.Close();
				try
				{
					int num13 = int.Parse("0" + wgAppConfig.getSystemParamByNO(144));
					if (num13 > 3)
					{
						controlConfigure.elevatorSingleDelay = (float)((num13 >> 8 & 255) / 10m);
						controlConfigure.elevatorMultioutputDelay = (float)((num13 >> 16 & 255) / 10m);
					}
					else
					{
						controlConfigure.elevatorSingleDelay = 0.4f;
						controlConfigure.elevatorMultioutputDelay = 5f;
					}
				}
				catch (Exception)
				{
				}
			}
			return 1;
		}
	}
}
