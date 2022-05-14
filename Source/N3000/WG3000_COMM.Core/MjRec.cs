using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.DataOper;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Core
{
	public class MjRec : wgMjControllerSwipeRecord
	{
		public const int RecordSizeInDb = 48;

		private string m_consumerName;

		private string m_deptName;

		private DateTime m_beginYMD = DateTime.Parse("2000-1-1");

		private DateTime m_endYMD = DateTime.Parse("2000-1-1");

		private string m_Address;

		private static icController control4GetDetailedRecord = new icController();

		public string consumerName
		{
			get
			{
				return this.m_consumerName;
			}
		}

		public string groupname
		{
			get
			{
				return this.m_deptName;
			}
		}

		public DateTime beginYMD
		{
			get
			{
				return this.m_beginYMD;
			}
		}

		public DateTime endYMD
		{
			get
			{
				return this.m_endYMD;
			}
		}

		public string address
		{
			get
			{
				return this.m_Address;
			}
			set
			{
				this.m_Address = value;
			}
		}

		public MjRec()
		{
		}

		public MjRec(byte[] rec, uint startIndex) : base(rec, startIndex)
		{
		}

		public MjRec(byte[] rec, uint startIndex, uint ControllerSN, uint loc) : base(rec, startIndex, ControllerSN, loc)
		{
		}

		public MjRec(string strRecordAll) : base(strRecordAll)
		{
		}

		public string ToDisplayDetail()
		{
			string str = "";
			if (base.IsSwipeRecord)
			{
				str += string.Format("{0}: \t{1:d}\r\n", CommonStr.strCardID, base.CardID);
				if (this.m_consumerName == null)
				{
					this.GetUserInfoFromDB();
				}
				str += string.Format("{0}: \t{1}\r\n", CommonStr.strName, this.m_consumerName);
				str += string.Format("{0}: \t{1}\r\n", wgAppConfig.ReplaceFloorRomm(CommonStr.strDepartment), this.m_deptName);
			}
			else if (base.IsRemoteOpen)
			{
				if (base.SwipeStatus < 4)
				{
					str += string.Format("{0}: \t{1:d}\r\n", CommonStr.strCardID, base.CardID);
					if (this.m_consumerName == null)
					{
						this.GetUserInfoFromDB();
					}
					str += string.Format("{0}: \t{1}\r\n", CommonStr.strName, this.m_consumerName);
					str += string.Format("{0}: \t{1}\r\n", wgAppConfig.ReplaceFloorRomm(CommonStr.strDepartment), this.m_deptName);
				}
				else if (base.SwipeStatus < 20 && base.SwipeStatus >= 16)
				{
					str += string.Format("{0}: \t{1:d}\r\n", CommonStr.strCardID, base.CardID);
				}
			}
			str += string.Format("{0}: \t{1}\r\n", CommonStr.strReadDate, base.ReadDate.ToString(wgTools.DisplayFormat_DateYMDHMSWeek));
			str += string.Format("{0}: \t{1}\r\n", CommonStr.strAddr, string.IsNullOrEmpty(this.m_Address) ? base.ControllerSN.ToString() : this.m_Address);
			return str + string.Format("{0}: \t{1}\r\n", CommonStr.strSwipeStatus, this.GetDetailedRecord(null, base.ControllerSN));
		}

		public string ToDisplayInfo()
		{
			string str = "";
			if (base.IsSwipeRecord)
			{
				str += string.Format("{0:d}-", base.CardID);
				if (this.m_consumerName == null)
				{
					this.GetUserInfoFromDB();
				}
				str += string.Format("{0}-", this.m_consumerName);
				str += string.Format("{0}-", this.m_deptName);
			}
			str += string.Format("{0}-", base.ReadDate.ToString(wgTools.DisplayFormat_DateYMDHMSWeek));
			str += string.Format("{0}-", string.IsNullOrEmpty(this.m_Address) ? base.ControllerSN.ToString() : this.m_Address);
			return str + string.Format("{0}", this.GetDetailedRecord(null, base.ControllerSN));
		}

		public void GetUserInfoFromDB()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.GetUserInfoFromDB_Acc();
				return;
			}
			string text = " SELECT  f_ConsumerName,  f_GroupName, f_BeginYMD, f_EndYMD ";
			text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
			text = text + " WHERE  t_b_Consumer.f_CardNO = " + base.CardID.ToString();
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						this.m_consumerName = (sqlDataReader["f_ConsumerName"] as string);
						this.m_deptName = (sqlDataReader["f_GroupName"] as string);
						DateTime.TryParse(sqlDataReader["f_BeginYMD"].ToString(), out this.m_beginYMD);
						DateTime.TryParse(sqlDataReader["f_EndYMD"].ToString(), out this.m_endYMD);
					}
					else
					{
						this.m_consumerName = "";
						this.m_deptName = "";
					}
					sqlDataReader.Close();
				}
			}
		}

		public void GetUserInfoFromDB_Acc()
		{
			string text = " SELECT  f_ConsumerName,  f_GroupName, f_BeginYMD, f_EndYMD ";
			text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
			text = text + " WHERE  t_b_Consumer.f_CardNO = " + base.CardID.ToString();
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						this.m_consumerName = (oleDbDataReader["f_ConsumerName"] as string);
						this.m_deptName = (oleDbDataReader["f_GroupName"] as string);
						DateTime.TryParse(oleDbDataReader["f_BeginYMD"].ToString(), out this.m_beginYMD);
						DateTime.TryParse(oleDbDataReader["f_EndYMD"].ToString(), out this.m_endYMD);
					}
					else
					{
						this.m_consumerName = "";
						this.m_deptName = "";
					}
					oleDbDataReader.Close();
				}
			}
		}

		public string GetDetailedRecord(icController current_control, uint RecControllerSN)
		{
			string text = "";
			if (base.eventCategory == 1 || base.eventCategory == 0)
			{
				byte swipeStatus = base.SwipeStatus;
				if (swipeStatus <= 19)
				{
					switch (swipeStatus)
					{
					case 0:
					case 1:
					case 2:
					case 3:
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSwipe);
						goto IL_477;
					default:
						switch (swipeStatus)
						{
						case 16:
						case 17:
						case 18:
						case 19:
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSwipeOpen);
							goto IL_477;
						}
						break;
					}
				}
				else
				{
					switch (swipeStatus)
					{
					case 32:
					case 33:
					case 34:
					case 35:
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSwipeClose);
						goto IL_477;
					default:
						switch (swipeStatus)
						{
						case 132:
						case 133:
						case 134:
						case 135:
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessPCControl);
							goto IL_477;
						default:
							switch (swipeStatus)
							{
							case 144:
							case 145:
							case 146:
							case 147:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessNOPRIVILEGE);
								goto IL_477;
							case 160:
							case 161:
							case 162:
							case 163:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessERRPASSWORD);
								goto IL_477;
							case 196:
							case 197:
							case 198:
							case 199:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_ANTIBACK);
								goto IL_477;
							case 200:
							case 201:
							case 202:
							case 203:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_MORECARD);
								goto IL_477;
							case 204:
							case 205:
							case 206:
							case 207:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_FIRSTCARD);
								goto IL_477;
							case 208:
							case 209:
							case 210:
							case 211:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessDOORNC);
								goto IL_477;
							case 212:
							case 213:
							case 214:
							case 215:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_INTERLOCK);
								goto IL_477;
							case 216:
							case 217:
							case 218:
							case 219:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_LIMITEDTIMES);
								goto IL_477;
							case 220:
							case 221:
							case 222:
							case 223:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_LIMITEDPERSONSINDOOR);
								goto IL_477;
							case 224:
							case 225:
							case 226:
							case 227:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessINVALIDTIMEZONE);
								goto IL_477;
							case 228:
							case 229:
							case 230:
							case 231:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_INORDER);
								goto IL_477;
							case 232:
							case 233:
							case 234:
							case 235:
								text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_SWIPEGAPLIMIT);
								goto IL_477;
							}
							break;
						}
						break;
					}
				}
				text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccess);
			}
			IL_477:
			if (base.eventCategory == 3 || base.eventCategory == 2)
			{
				if (base.IsPassed)
				{
					if (wgMjController.IsElevator((int)base.ControllerSN))
					{
						if (base.currentSwipeTimes >= 128)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSwipeWithCount4MultiFloor1);
						}
						else
						{
							string text2 = MjRec.control4GetDetailedRecord.GetFloorName(base.floorNo);
							if (!string.IsNullOrEmpty(text2))
							{
								text2 = " [" + text2 + "]";
							}
							text = string.Format("{0}{1}{2}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSwipeWithCount4Floor, base.currentSwipeTimes);
							text += text2;
						}
					}
					else
					{
						text = string.Format("{0}{1}{2}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSwipeWithCount, base.currentSwipeTimes);
					}
				}
				else
				{
					text = string.Format("{0}{1}{2}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordDeniedAccessSPECIAL_LIMITEDTIMES_WITHCOUNT, base.currentSwipeTimes);
				}
			}
			if (base.eventCategory == 4 || base.eventCategory == 5)
			{
				uint cardID = base.CardID;
				switch (cardID)
				{
				case 0u:
					if (base.SwipeStatus == 0)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordPowerOn);
					}
					else if ((base.SwipeStatus & 130) == 130)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordPowerOn);
					}
					else if ((base.SwipeStatus & 160) == 160)
					{
						text = string.Format("{0}{1}-{2}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordReset, "LDO");
					}
					else if ((base.SwipeStatus & 144) == 144)
					{
						text = string.Format("{0}{1}-{2}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordReset, "SW");
					}
					else if ((base.SwipeStatus & 136) == 136)
					{
						text = string.Format("{0}{1}-{2}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordReset, "WDT");
					}
					else if ((base.SwipeStatus & 132) == 132)
					{
						text = string.Format("{0}{1}-{2}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordReset, "BOR");
					}
					else if ((base.SwipeStatus & 129) == 129)
					{
						text = string.Format("{0}{1}-{2}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordReset, "EXT");
					}
					if (!string.IsNullOrEmpty(text) && RecControllerSN > 0u)
					{
						text = string.Format("{0}[{1}]", text, RecControllerSN.ToString());
					}
					break;
				case 1u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordPushButton);
					}
					break;
				case 2u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordPushButtonOpen);
					}
					break;
				case 3u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)(base.SwipeStatus + 1)), CommonStr.strRecordPushButtonClose);
					}
					break;
				case 4u:
					if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)((base.SwipeStatus & 3) + 1)), CommonStr.strRecordPushButtonInvalid_Disable);
					}
					break;
				case 5u:
					if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)((base.SwipeStatus & 3) + 1)), CommonStr.strRecordPushButtonInvalid_ForcedLock);
					}
					break;
				case 6u:
					if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)((base.SwipeStatus & 3) + 1)), CommonStr.strRecordPushButtonInvalid_NotOnLine);
					}
					break;
				case 7u:
					if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)((base.SwipeStatus & 3) + 1)), CommonStr.strRecordPushButtonInvalid_INTERLOCK);
					}
					break;
				case 8u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordDoorOpen);
					}
					break;
				case 9u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordDoorClosed);
					}
					break;
				case 10u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSuperPasswordDoorOpen);
					}
					break;
				case 11u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSuperPasswordOpen);
					}
					break;
				case 12u:
					if (base.SwipeStatus >= 0 && base.SwipeStatus <= 3)
					{
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetReaderName((int)base.ReaderNo), CommonStr.strRecordSuperPasswordClose);
					}
					break;
				default:
					switch (cardID)
					{
					case 81u:
						if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordThreat);
						}
						break;
					case 82u:
						if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordThreatOpen);
						}
						break;
					case 83u:
						if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordThreatClose);
						}
						break;
					case 84u:
						if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordWarnLeftOpen);
						}
						break;
					case 85u:
						if (base.SwipeStatus >= 128 && base.SwipeStatus <= 131)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordWarnOpenByForce);
						}
						break;
					case 86u:
						if (base.SwipeStatus == 128)
						{
							text = CommonStr.strRecordWarnFire;
							if (current_control != null)
							{
								text = MjRec.control4GetDetailedRecord.GetDoorName(1);
								if (wgMjController.GetControllerType((int)base.ControllerSN) == 2)
								{
									text = text + "," + MjRec.control4GetDetailedRecord.GetDoorName(2);
								}
								if (wgMjController.GetControllerType((int)base.ControllerSN) == 4)
								{
									text += string.Format(",{0},{1},{2}", MjRec.control4GetDetailedRecord.GetDoorName(2), MjRec.control4GetDetailedRecord.GetDoorName(3), MjRec.control4GetDetailedRecord.GetDoorName(4));
								}
								text += CommonStr.strRecordWarnFire;
							}
						}
						break;
					case 87u:
						if (base.SwipeStatus == 128)
						{
							text = CommonStr.strRecordWarnCloseByForce;
							if (current_control != null)
							{
								text = MjRec.control4GetDetailedRecord.GetDoorName(1);
								if (wgMjController.GetControllerType((int)base.ControllerSN) == 2)
								{
									text = text + "," + MjRec.control4GetDetailedRecord.GetDoorName(2);
								}
								if (wgMjController.GetControllerType((int)base.ControllerSN) == 4)
								{
									text += string.Format(",{0},{1},{2}", MjRec.control4GetDetailedRecord.GetDoorName(2), MjRec.control4GetDetailedRecord.GetDoorName(3), MjRec.control4GetDetailedRecord.GetDoorName(4));
								}
								text += CommonStr.strRecordWarnCloseByForce;
							}
						}
						break;
					case 88u:
						if (base.SwipeStatus == 128)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName(1), CommonStr.strRecordWarnGuardAgainstTheft);
						}
						break;
					case 89u:
						if (base.SwipeStatus == 128)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName(1), CommonStr.strRecordWarn24Hour);
						}
						break;
					case 90u:
						if (base.SwipeStatus == 128)
						{
							text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName(1), CommonStr.strRecordWarnEmergencyCall);
						}
						break;
					}
					break;
				}
			}
			if (base.eventCategory == 6)
			{
				byte swipeStatus2 = base.SwipeStatus;
				switch (swipeStatus2)
				{
				case 0:
				case 1:
				case 2:
				case 3:
					text = string.Format("{0}{1}{2}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordRemoteOpenDoor_ByUSBReader, base.CardID.ToString());
					break;
				default:
					switch (swipeStatus2)
					{
					case 16:
					case 17:
					case 18:
					case 19:
						text = string.Format("{0}{1}", MjRec.control4GetDetailedRecord.GetDoorName((int)base.DoorNo), CommonStr.strRecordRemoteOpenDoor);
						break;
					}
					break;
				}
			}
			return text;
		}
	}
}
