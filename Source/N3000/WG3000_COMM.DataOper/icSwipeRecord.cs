using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.DataOper
{
	internal class icSwipeRecord : wgMjControllerSwipeOperate
	{
		private static string gYMDHMSFormat = "yyyy-MM-dd HH:mm:ss";

		private icController control = new icController();

		public icSwipeRecord()
		{
			base.Clear();
		}

		public static int AddNewSwipe_SynConsumerID(MjRec mjrec)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icSwipeRecord.AddNewSwipe_SynConsumerID_Acc(mjrec);
			}
			int result = -9;
			string text = "";
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						sqlCommand.CommandType = CommandType.Text;
						sqlCommand.Connection = sqlConnection;
						text = "SELECT f_RecID FROM t_d_SwipeRecord ORDER BY f_RecID DESC";
						sqlCommand.CommandText = text;
						int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
						text = " INSERT INTO t_d_SwipeRecord (f_ReadDate, f_CardNO, f_Character, f_InOut, f_Status, f_RecOption, ";
						text += " f_ControllerSN, f_ReaderID, f_ReaderNO, f_RecordFlashLoc, f_RecordAll) values (";
						text += wgTools.PrepareStr(mjrec.ReadDate, true, icSwipeRecord.gYMDHMSFormat);
						text = text + "," + mjrec.CardID.ToString();
						text = text + "," + (mjrec.IsPassed ? "1" : "0");
						text = text + "," + (mjrec.IsEnterIn ? "1" : "0");
						text = text + "," + mjrec.bytStatus.ToString();
						text = text + "," + mjrec.bytRecOption.ToString();
						text = text + "," + mjrec.ControllerSN.ToString();
						text += ",0";
						text = text + "," + mjrec.ReaderNo.ToString();
						text = text + "," + mjrec.IndexInDataFlash.ToString();
						text = text + "," + wgTools.PrepareStr(mjrec.ToStringRaw());
						text += ")";
						text += ";";
						sqlCommand.CommandText = text;
						int num2 = sqlCommand.ExecuteNonQuery();
						if (num2 > 0)
						{
							text = " UPDATE t_d_SwipeRecord   ";
							text += " SET t_d_SwipeRecord.f_ConsumerID=t_b_Consumer.f_ConsumerID ";
							text += " FROM   t_d_SwipeRecord,t_b_Consumer ";
							text += " WHERE  t_d_SwipeRecord.f_CardNO = t_b_Consumer.f_CardNO  ";
							text = text + " AND  t_d_SwipeRecord.f_RecID >" + num.ToString();
							text += " AND (((f_RecOption & 2) =0) OR ((f_RecOption & (2+4)) = (2+4) and ((f_Status & (128+16))=0))) ";
							sqlCommand.CommandText = text;
							num2 = sqlCommand.ExecuteNonQuery();
							text = " UPDATE t_d_SwipeRecord   ";
							text += " SET t_d_SwipeRecord.f_ConsumerID=t_b_IDCard_Lost.f_ConsumerID ";
							text += " FROM   t_d_SwipeRecord,t_b_IDCard_Lost ";
							text += " WHERE  t_d_SwipeRecord.f_CardNO = t_b_IDCard_Lost.f_CardNO  ";
							text = text + " AND  t_d_SwipeRecord.f_RecID >" + num.ToString();
							text += " AND (((f_RecOption & 2) =0) OR ((f_RecOption & (2+4)) = (2+4) and ((f_Status & (128+16))=0))) ";
							sqlCommand.CommandText = text;
							num2 = sqlCommand.ExecuteNonQuery();
							text = "UPDATE a  SET a.f_ReaderID=b.f_ReaderID ";
							text += " FROM t_d_SwipeRecord a ";
							text = text + " INNER JOIN  t_b_Reader b  INNER JOIN t_b_Controller c ON c.f_ControllerID = b.f_ControllerID AND c.f_ControllerSN = " + mjrec.ControllerSN.ToString() + " ";
							text = text + " ON  a.f_ReaderNO = b.f_ReaderNO AND a.f_ReaderNO =" + mjrec.ReaderNo.ToString();
							text = text + " WHERE a.f_RecID >" + num.ToString();
							text += " AND (((f_RecOption & 2) =0) OR ((f_RecOption & (2+4)) = (2+4) and ((f_Status & (128+16))=0))) ";
							sqlCommand.CommandText = text;
							num2 = sqlCommand.ExecuteNonQuery();
							result = 1;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public static int AddNewSwipe_SynConsumerID_Acc(MjRec mjrec)
		{
			int result = -9;
			string text = "";
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						oleDbCommand.CommandType = CommandType.Text;
						oleDbCommand.Connection = oleDbConnection;
						text = "SELECT f_RecID FROM t_d_SwipeRecord ORDER BY f_RecID DESC";
						oleDbCommand.CommandText = text;
						int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
						text = " INSERT INTO t_d_SwipeRecord (f_ReadDate, f_CardNO, f_Character, f_InOut, f_Status, f_RecOption, ";
						text += " f_ControllerSN, f_ReaderID, f_ReaderNO, f_RecordFlashLoc, f_RecordAll) values (";
						text += wgTools.PrepareStr(mjrec.ReadDate, true, icSwipeRecord.gYMDHMSFormat);
						text = text + "," + mjrec.CardID.ToString();
						text = text + "," + (mjrec.IsPassed ? "1" : "0");
						text = text + "," + (mjrec.IsEnterIn ? "1" : "0");
						text = text + "," + mjrec.bytStatus.ToString();
						text = text + "," + mjrec.bytRecOption.ToString();
						text = text + "," + mjrec.ControllerSN.ToString();
						text += ",0";
						text = text + "," + mjrec.ReaderNo.ToString();
						text = text + "," + mjrec.IndexInDataFlash.ToString();
						text = text + "," + wgTools.PrepareStr(mjrec.ToStringRaw());
						text += ")";
						text += ";";
						oleDbCommand.CommandText = text;
						int num2 = oleDbCommand.ExecuteNonQuery();
						if (num2 > 0)
						{
							text = " UPDATE t_d_SwipeRecord   ";
							text += " INNER JOIN t_b_Consumer ";
							text += " ON (  t_d_SwipeRecord.f_CardNO = t_b_Consumer.f_CardNO  ";
							text = text + " AND  t_d_SwipeRecord.f_RecID >" + num.ToString();
							text += " AND (  (((f_RecOption / 2) mod 2) =0) OR ( (((f_RecOption/2) Mod 4) = 3) and ((((f_Status/128) Mod 2)=0) and (((f_Status/16) MOD 2)=0)))) ";
							text += ") SET t_d_SwipeRecord.f_ConsumerID=t_b_Consumer.f_ConsumerID ";
							oleDbCommand.CommandText = text;
							num2 = oleDbCommand.ExecuteNonQuery();
							text = " UPDATE t_d_SwipeRecord   ";
							text += " INNER JOIN t_b_IDCard_Lost ";
							text += " ON (  t_d_SwipeRecord.f_CardNO = t_b_IDCard_Lost.f_CardNO  ";
							text = text + " AND  t_d_SwipeRecord.f_RecID >" + num.ToString();
							text += " AND (  (((f_RecOption / 2) mod 2) =0) OR ( (((f_RecOption/2) Mod 4) = 3) and ((((f_Status/128) Mod 2)=0) and (((f_Status/16) MOD 2)=0)))) ";
							text += " ) SET t_d_SwipeRecord.f_ConsumerID=t_b_IDCard_Lost.f_ConsumerID ";
							oleDbCommand.CommandText = text;
							num2 = oleDbCommand.ExecuteNonQuery();
							text = "UPDATE t_d_SwipeRecord a  ";
							text += "  ";
							text = text + " INNER JOIN ( t_b_Reader b  INNER JOIN t_b_Controller c ON ( c.f_ControllerID = b.f_ControllerID AND c.f_ControllerSN = " + mjrec.ControllerSN.ToString() + " ";
							text = text + " )) ON ( a.f_ReaderNO = b.f_ReaderNO AND a.f_ReaderNO =" + mjrec.ReaderNo.ToString();
							text = text + " AND a.f_RecID >" + num.ToString();
							text += " AND (  (((f_RecOption / 2) mod 2) =0) OR ( (((f_RecOption/2) Mod 4) = 3) and ((((f_Status/128) Mod 2)=0) and (((f_Status/16) MOD 2)=0)))) ";
							text += " ) SET a.f_ReaderID=b.f_ReaderID ";
							oleDbCommand.CommandText = text;
							num2 = oleDbCommand.ExecuteNonQuery();
							result = 1;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public int ReadSwipeRecIDMax()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.ReadSwipeRecIDMax_Acc();
			}
			int result = 0;
			string cmdText = "SELECT f_RecID FROM t_d_SwipeRecord ORDER BY f_RecID DESC";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
					result = num;
				}
			}
			return result;
		}

		public int ReadSwipeRecIDMax_Acc()
		{
			int result = 0;
			string cmdText = "SELECT f_RecID FROM t_d_SwipeRecord ORDER BY f_RecID DESC";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
					result = num;
				}
			}
			return result;
		}

		public int GetSwipeRecordsByDoorName(string DoorName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.GetSwipeRecordsByDoorName_Acc(DoorName);
			}
			int result = -1;
			string text = " SELECT f_ControllerSN, f_IP, f_Port";
			text = text + " FROM t_b_Controller a, t_b_Door b WHERE a.f_ControllerID = b.f_ControllerID AND b.f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						result = this.GetSwipeRecords((int)sqlDataReader["f_ControllerSN"], wgTools.SetObjToStr(sqlDataReader["f_IP"]), (int)sqlDataReader["f_Port"], DoorName);
					}
					sqlDataReader.Close();
				}
			}
			return result;
		}

		public int GetSwipeRecordsByDoorName_Acc(string DoorName)
		{
			int result = -1;
			string text = " SELECT f_ControllerSN, f_IP, f_Port";
			text = text + " FROM t_b_Controller a, t_b_Door b WHERE a.f_ControllerID = b.f_ControllerID AND b.f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						result = this.GetSwipeRecords((int)oleDbDataReader["f_ControllerSN"], wgTools.SetObjToStr(oleDbDataReader["f_IP"]), (int)oleDbDataReader["f_Port"], DoorName);
					}
					oleDbDataReader.Close();
				}
			}
			return result;
		}

		public int GetSwipeRecords(int ControllerSN, string IP, int Port, string DoorName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.GetSwipeRecords_Acc(ControllerSN, IP, Port, DoorName);
			}
			wgTools.WriteLine("getSwipeRecords Start");
			this.control.ControllerSN = ControllerSN;
			this.control.IP = IP;
			this.control.PORT = Port;
			base.ControllerSN = ControllerSN;
			if (this.control.GetControllerRunInformationIP() < 0)
			{
				return -13;
			}
			if (this.control.runinfo.newRecordsNum == 0u)
			{
				base.lastRecordFlashIndex = (int)this.control.runinfo.lastGetRecordIndex;
				return 0;
			}
			if (base.wgudp == null)
			{
				base.wgudp = new wgUdpComm();
				Thread.Sleep(300);
			}
			byte[] array = null;
			WGPacketSSI_FLASH_QUERY wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, 5017600u, 5018623u);
			byte[] array2 = wGPacketSSI_FLASH_QUERY.ToBytes(base.wgudp.udpPort);
			if (array2 == null)
			{
				return -12;
			}
			array = null;
			int num = base.wgudp.udp_get(array2, 300, wGPacketSSI_FLASH_QUERY.xid, IP, Port, ref array);
			if (num < 0)
			{
				return -13;
			}
			wgTools.WriteLine(string.Format("\r\nBegin Sending Command:\t{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			string.Format("SSI_FLASH_{0}", DateTime.Now.ToString("yyyy-MM-dd-HHmmss"));
			int num2 = 4096;
			int num3 = 0;
			string text = "SELECT f_RecID FROM t_d_SwipeRecord ORDER BY f_RecID DESC";
			int num4;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					num4 = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
				}
			}
			int num5 = 0;
			int num6 = (int)this.control.runinfo.lastGetRecordIndex;
			int[] array3 = new int[4];
			int[] array4 = array3;
			text = " select f_ReaderID   ";
			text += " FROM   t_b_Reader, t_b_Controller ";
			text += " WHERE t_b_Controller.f_ControllerID = t_b_Reader.f_ControllerID ";
			text = text + " AND  t_b_Controller.f_ControllerSN = " + ControllerSN.ToString();
			text += " ORDER BY f_ReaderNO ASC";
			int num7 = 0;
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
				{
					sqlConnection2.Open();
					SqlDataReader sqlDataReader = sqlCommand2.ExecuteReader();
					while (sqlDataReader.Read() && num7 < 4)
					{
						array4[num7] = (int)sqlDataReader[0];
						num7++;
					}
					sqlDataReader.Close();
				}
			}
			num6 = (int)(this.control.runinfo.swipeEndIndex - this.control.runinfo.newRecordsNum);
			if (num6 > 0)
			{
				int swipeLoc = base.GetSwipeLoc(num6);
				wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, (uint)(swipeLoc - swipeLoc % 1024), (uint)(swipeLoc - swipeLoc % 1024 + 1024 - 1));
				array2 = wGPacketSSI_FLASH_QUERY.ToBytes(base.wgudp.udpPort);
				if (array2 == null)
				{
					return -12;
				}
				array = null;
				num = base.wgudp.udp_get(array2, 300, wGPacketSSI_FLASH_QUERY.xid, IP, Port, ref array);
				if (num < 0)
				{
					return -13;
				}
			}
			text = "";
			uint iStartFlashAddr = wGPacketSSI_FLASH_QUERY.iStartFlashAddr;
			bool flag = false;
			uint num8 = 0u;
			wgTools.WriteLine(string.Format("First Page:\t ={0:d}", iStartFlashAddr / 1024u));
			int num9 = num6 - num6 % 204800;
			while (!wgMjControllerSwipeOperate.bStopGetRecord && array != null)
			{
				WGPacketSSI_FLASH wGPacketSSI_FLASH = new WGPacketSSI_FLASH(array);
				uint num10 = (uint)(base.GetSwipeIndex(wGPacketSSI_FLASH.iStartFlashAddr) + num9);
				for (uint num11 = 0u; num11 < 1024u; num11 += 16u)
				{
					MjRec mjRec = new MjRec(wGPacketSSI_FLASH.ucData, num11, wGPacketSSI_FLASH.iDevSnFrom, num10);
					if (mjRec.CardID == 4294967295u || ((mjRec.bytRecOption == 0 || mjRec.bytRecOption == 255) && mjRec.CardID == 0u))
					{
						if ((long)this.control.runinfo.swipeEndIndex <= (long)((ulong)num10))
						{
							break;
						}
						num8 += 1u;
						num10 += 1u;
					}
					else
					{
						if (num3 > 0 || (ulong)mjRec.IndexInDataFlash >= (ulong)((long)num6))
						{
							text += " INSERT INTO t_d_SwipeRecord (f_ReadDate, f_CardNO, f_Character, f_InOut, f_Status, f_RecOption, ";
							text += " f_ControllerSN, f_ReaderID, f_ReaderNO, f_RecordFlashLoc, f_RecordAll) values (";
							text += wgTools.PrepareStr(mjRec.ReadDate, true, icSwipeRecord.gYMDHMSFormat);
							text = text + "," + mjRec.CardID.ToString();
							text = text + "," + (mjRec.IsPassed ? "1" : "0");
							text = text + "," + (mjRec.IsEnterIn ? "1" : "0");
							text = text + "," + mjRec.bytStatus.ToString();
							text = text + "," + mjRec.bytRecOption.ToString();
							text = text + "," + mjRec.ControllerSN.ToString();
							text = text + "," + array4[(int)(mjRec.ReaderNo - 1)].ToString();
							text = text + "," + mjRec.ReaderNo.ToString();
							text = text + "," + mjRec.IndexInDataFlash.ToString();
							text = text + "," + wgTools.PrepareStr(mjRec.ToStringRaw());
							text += ")";
							text += ";";
							num3++;
							num5 = (int)mjRec.IndexInDataFlash;
						}
						num10 += 1u;
					}
				}
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}--[{2:d}]", DoorName, CommonStr.strGotRecords, num3));
				if ((long)this.control.runinfo.swipeEndIndex <= (long)((ulong)num10))
				{
					flag = true;
					break;
				}
				if (text != "")
				{
					using (SqlConnection sqlConnection3 = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand3 = new SqlCommand(text, sqlConnection3))
						{
							sqlConnection3.Open();
							num7 = sqlCommand3.ExecuteNonQuery();
						}
					}
					text = "";
				}
				if (flag)
				{
					break;
				}
				wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, wGPacketSSI_FLASH_QUERY.iStartFlashAddr + 1024u, wGPacketSSI_FLASH_QUERY.iStartFlashAddr + 1024u + 1024u - 1u);
				if (wGPacketSSI_FLASH_QUERY.iStartFlashAddr > 8294399u)
				{
					wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, 5017600u, 5018623u);
					num9 += 204800;
				}
				if (wGPacketSSI_FLASH_QUERY.iStartFlashAddr == iStartFlashAddr)
				{
					break;
				}
				wGPacketSSI_FLASH_QUERY.GetNewXid();
				array2 = wGPacketSSI_FLASH_QUERY.ToBytes(base.wgudp.udpPort);
				if (array2 == null)
				{
					break;
				}
				num = base.wgudp.udp_get(array2, 300, wGPacketSSI_FLASH_QUERY.xid, IP, Port, ref array);
				if (num < 0 || --num2 <= 0)
				{
					break;
				}
			}
			if (text != "")
			{
				using (SqlConnection sqlConnection4 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand4 = new SqlCommand(text, sqlConnection4))
					{
						sqlConnection4.Open();
						num7 = sqlCommand4.ExecuteNonQuery();
					}
				}
			}
			wgTools.WriteLine(string.Format("Last Page:\t ={0:d}", wGPacketSSI_FLASH_QUERY.iStartFlashAddr / 1024u));
			wgTools.WriteLine(string.Format("Got Records:\t Count={0:d}", num3));
			if (num8 > 0u)
			{
				wgAppConfig.wgLog(string.Format("Got Records:\t invalidRecCount={0:d}", num8));
			}
			wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}--[{2:d}]", DoorName, CommonStr.strWritingRecordsToDB, num3));
			text = " UPDATE t_d_SwipeRecord   ";
			text += " SET t_d_SwipeRecord.f_ConsumerID=t_b_Consumer.f_ConsumerID ";
			text += " FROM   t_d_SwipeRecord,t_b_Consumer ";
			text += " WHERE  t_d_SwipeRecord.f_CardNO = t_b_Consumer.f_CardNO  ";
			text = text + " AND  t_d_SwipeRecord.f_RecID >" + num4.ToString();
			text += " AND (((f_RecOption & 2) =0) OR ((f_RecOption & (2+4)) = (2+4) and ((f_Status & (128+16))=0))) ";
			using (SqlConnection sqlConnection5 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand5 = new SqlCommand(text, sqlConnection5))
				{
					sqlConnection5.Open();
					sqlCommand5.CommandTimeout = num3 / 250 + 30;
					num7 = sqlCommand5.ExecuteNonQuery();
				}
			}
			text = " UPDATE t_d_SwipeRecord   ";
			text += " SET t_d_SwipeRecord.f_ConsumerID=t_b_IDCard_Lost.f_ConsumerID ";
			text += " FROM   t_d_SwipeRecord,t_b_IDCard_Lost ";
			text += " WHERE  t_d_SwipeRecord.f_CardNO = t_b_IDCard_Lost.f_CardNO  ";
			text = text + " AND  t_d_SwipeRecord.f_RecID >" + num4.ToString();
			text += " AND (((f_RecOption & 2) =0) OR ((f_RecOption & (2+4)) = (2+4) and ((f_Status & (128+16))=0))) ";
			using (SqlConnection sqlConnection6 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand6 = new SqlCommand(text, sqlConnection6))
				{
					sqlConnection6.Open();
					sqlCommand6.CommandTimeout = num3 / 250 + 30;
					num7 = sqlCommand6.ExecuteNonQuery();
				}
			}
			wgTools.WriteLine("Syn Data Info");
			if (num3 > 0)
			{
				if (this.control.GetControllerRunInformationIP() < 0)
				{
					return -13;
				}
				if (num5 % 204800 >= (int)(this.control.runinfo.swipeEndIndex % 204800u))
				{
					if (this.control.runinfo.swipeEndIndex > 204800u)
					{
						num6 = (int)(this.control.runinfo.swipeEndIndex - this.control.runinfo.swipeEndIndex % 204800u - 204800u + (uint)(num5 % 204800));
					}
					else
					{
						num6 = 0;
					}
				}
				else
				{
					num6 = (int)(this.control.runinfo.swipeEndIndex - this.control.runinfo.swipeEndIndex % 204800u + (uint)(num5 % 204800));
				}
				this.control.UpdateLastGetRecordLocationIP((uint)(num6 + 1));
				base.lastRecordFlashIndex = num6 + 1;
			}
			wgAppRunInfo.raiseAppRunInfoCommStatus("");
			return num3;
		}

		public int GetSwipeRecords_Acc(int ControllerSN, string IP, int Port, string DoorName)
		{
			wgTools.WriteLine("getSwipeRecords_Acc Start");
			this.control.ControllerSN = ControllerSN;
			this.control.IP = IP;
			this.control.PORT = Port;
			base.ControllerSN = ControllerSN;
			if (this.control.GetControllerRunInformationIP() < 0)
			{
				return -13;
			}
			if (this.control.runinfo.newRecordsNum == 0u)
			{
				base.lastRecordFlashIndex = (int)this.control.runinfo.lastGetRecordIndex;
				return 0;
			}
			if (base.wgudp == null)
			{
				base.wgudp = new wgUdpComm();
				Thread.Sleep(300);
			}
			byte[] array = null;
			WGPacketSSI_FLASH_QUERY wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, 5017600u, 5018623u);
			byte[] array2 = wGPacketSSI_FLASH_QUERY.ToBytes(base.wgudp.udpPort);
			if (array2 == null)
			{
				return -12;
			}
			array = null;
			int num = base.wgudp.udp_get(array2, 300, wGPacketSSI_FLASH_QUERY.xid, IP, Port, ref array);
			if (num < 0)
			{
				return -13;
			}
			wgTools.WriteLine(string.Format("\r\nBegin Sending Command:\t{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			string.Format("SSI_FLASH_{0}", DateTime.Now.ToString("yyyy-MM-dd-HHmmss"));
			int num2 = 4096;
			int num3 = 0;
			string text = "SELECT f_RecID FROM t_d_SwipeRecord ORDER BY f_RecID DESC";
			int num4;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					num4 = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
				}
			}
			int num5 = 0;
			int num6 = (int)this.control.runinfo.lastGetRecordIndex;
			int[] array3 = new int[4];
			int[] array4 = array3;
			text = " select f_ReaderID   ";
			text += " FROM   t_b_Reader, t_b_Controller ";
			text += " WHERE t_b_Controller.f_ControllerID = t_b_Reader.f_ControllerID ";
			text = text + " AND  t_b_Controller.f_ControllerSN = " + ControllerSN.ToString();
			text += " ORDER BY f_ReaderNO ASC";
			int num7 = 0;
			using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
				{
					oleDbConnection2.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand2.ExecuteReader();
					while (oleDbDataReader.Read() && num7 < 4)
					{
						array4[num7] = (int)oleDbDataReader[0];
						num7++;
					}
					oleDbDataReader.Close();
				}
			}
			num6 = (int)(this.control.runinfo.swipeEndIndex - this.control.runinfo.newRecordsNum);
			if (num6 > 0)
			{
				int swipeLoc = base.GetSwipeLoc(num6);
				wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, (uint)(swipeLoc - swipeLoc % 1024), (uint)(swipeLoc - swipeLoc % 1024 + 1024 - 1));
				array2 = wGPacketSSI_FLASH_QUERY.ToBytes(base.wgudp.udpPort);
				if (array2 == null)
				{
					return -12;
				}
				array = null;
				num = base.wgudp.udp_get(array2, 300, wGPacketSSI_FLASH_QUERY.xid, IP, Port, ref array);
				if (num < 0)
				{
					return -13;
				}
			}
			text = "";
			uint iStartFlashAddr = wGPacketSSI_FLASH_QUERY.iStartFlashAddr;
			bool flag = false;
			uint num8 = 0u;
			wgTools.WriteLine(string.Format("First Page:\t ={0:d}", iStartFlashAddr / 1024u));
			int num9 = num6 - num6 % 204800;
			OleDbConnection oleDbConnection3 = new OleDbConnection(wgAppConfig.dbConString);
			OleDbCommand oleDbCommand3 = new OleDbCommand(text, oleDbConnection3);
			oleDbConnection3.Open();
			while (!wgMjControllerSwipeOperate.bStopGetRecord && array != null)
			{
				WGPacketSSI_FLASH wGPacketSSI_FLASH = new WGPacketSSI_FLASH(array);
				uint num10 = (uint)(base.GetSwipeIndex(wGPacketSSI_FLASH.iStartFlashAddr) + num9);
				for (uint num11 = 0u; num11 < 1024u; num11 += 16u)
				{
					MjRec mjRec = new MjRec(wGPacketSSI_FLASH.ucData, num11, wGPacketSSI_FLASH.iDevSnFrom, num10);
					if (mjRec.CardID == 4294967295u || ((mjRec.bytRecOption == 0 || mjRec.bytRecOption == 255) && mjRec.CardID == 0u))
					{
						if ((long)this.control.runinfo.swipeEndIndex <= (long)((ulong)num10))
						{
							break;
						}
						num8 += 1u;
						num10 += 1u;
					}
					else
					{
						if (num3 > 0 || (ulong)mjRec.IndexInDataFlash >= (ulong)((long)num6))
						{
							text = "";
							text += " INSERT INTO t_d_SwipeRecord (f_ReadDate, f_CardNO, f_Character, f_InOut, f_Status, f_RecOption, ";
							text += " f_ControllerSN, f_ReaderID, f_ReaderNO, f_RecordFlashLoc, f_RecordAll) values (";
							text += wgTools.PrepareStr(mjRec.ReadDate, true, icSwipeRecord.gYMDHMSFormat);
							text = text + "," + mjRec.CardID.ToString();
							text = text + "," + (mjRec.IsPassed ? "1" : "0");
							text = text + "," + (mjRec.IsEnterIn ? "1" : "0");
							text = text + "," + mjRec.bytStatus.ToString();
							text = text + "," + mjRec.bytRecOption.ToString();
							text = text + "," + mjRec.ControllerSN.ToString();
							text = text + "," + array4[(int)(mjRec.ReaderNo - 1)].ToString();
							text = text + "," + mjRec.ReaderNo.ToString();
							text = text + "," + mjRec.IndexInDataFlash.ToString();
							text = text + "," + wgTools.PrepareStr(mjRec.ToStringRaw());
							text += ")";
							oleDbCommand3.CommandText = text;
							num7 = oleDbCommand3.ExecuteNonQuery();
							num3++;
							num5 = (int)mjRec.IndexInDataFlash;
						}
						num10 += 1u;
					}
				}
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}--[{2:d}]", DoorName, CommonStr.strGotRecords, num3));
				if ((long)this.control.runinfo.swipeEndIndex <= (long)((ulong)num10))
				{
					break;
				}
				if (flag)
				{
					break;
				}
				wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, wGPacketSSI_FLASH_QUERY.iStartFlashAddr + 1024u, wGPacketSSI_FLASH_QUERY.iStartFlashAddr + 1024u + 1024u - 1u);
				if (wGPacketSSI_FLASH_QUERY.iStartFlashAddr > 8294399u)
				{
					wGPacketSSI_FLASH_QUERY = new WGPacketSSI_FLASH_QUERY(33, 16, (uint)ControllerSN, 5017600u, 5018623u);
					num9 += 204800;
				}
				if (wGPacketSSI_FLASH_QUERY.iStartFlashAddr == iStartFlashAddr)
				{
					break;
				}
				wGPacketSSI_FLASH_QUERY.GetNewXid();
				array2 = wGPacketSSI_FLASH_QUERY.ToBytes(base.wgudp.udpPort);
				if (array2 == null)
				{
					break;
				}
				num = base.wgudp.udp_get(array2, 300, wGPacketSSI_FLASH_QUERY.xid, IP, Port, ref array);
				if (num < 0 || --num2 <= 0)
				{
					break;
				}
			}
			oleDbConnection3.Close();
			wgTools.WriteLine(string.Format("Last Page:\t ={0:d}", wGPacketSSI_FLASH_QUERY.iStartFlashAddr / 1024u));
			wgTools.WriteLine(string.Format("Got Records:\t Count={0:d}", num3));
			if (num8 > 0u)
			{
				wgAppConfig.wgLog(string.Format("Got Records:\t invalidRecCount={0:d}", num8));
			}
			wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}--[{2:d}]", DoorName, CommonStr.strWritingRecordsToDB, num3));
			text = " UPDATE t_d_SwipeRecord   ";
			text += " INNER JOIN   t_b_Consumer ";
			text += " ON  (t_d_SwipeRecord.f_CardNO = t_b_Consumer.f_CardNO  ";
			text = text + " AND  t_d_SwipeRecord.f_RecID >" + num4.ToString();
			text += " AND (  (((f_RecOption / 2) mod 2) =0) OR ( (((f_RecOption/2) Mod 4) = 3) and ((((f_Status/128) Mod 2)=0) and (((f_Status/16) MOD 2)=0)))) ";
			text += " )";
			text += " SET t_d_SwipeRecord.f_ConsumerID=  t_b_Consumer.f_ConsumerID ";
			using (OleDbConnection oleDbConnection4 = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand4 = new OleDbCommand(text, oleDbConnection4))
				{
					oleDbConnection4.Open();
					oleDbCommand4.CommandTimeout = num3 / 250 + 30;
					num7 = oleDbCommand4.ExecuteNonQuery();
				}
			}
			text = " UPDATE t_d_SwipeRecord   ";
			text += " INNER JOIN   t_b_IDCard_Lost ";
			text += " ON (  t_d_SwipeRecord.f_CardNO = t_b_IDCard_Lost.f_CardNO  ";
			text = text + " AND  t_d_SwipeRecord.f_RecID >" + num4.ToString();
			text += " AND (  (((f_RecOption / 2) mod 2) =0) OR ( (((f_RecOption/2) Mod 4) = 3) and ((((f_Status/128) Mod 2)=0) and (((f_Status/16) MOD 2)=0)))) ";
			text += " )";
			text += " SET t_d_SwipeRecord.f_ConsumerID=t_b_IDCard_Lost.f_ConsumerID ";
			using (OleDbConnection oleDbConnection5 = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand5 = new OleDbCommand(text, oleDbConnection5))
				{
					oleDbConnection5.Open();
					oleDbCommand5.CommandTimeout = num3 / 250 + 30;
					num7 = oleDbCommand5.ExecuteNonQuery();
				}
			}
			wgTools.WriteLine("Syn Data Info");
			if (num3 > 0)
			{
				if (this.control.GetControllerRunInformationIP() < 0)
				{
					return -13;
				}
				if (num5 % 204800 >= (int)(this.control.runinfo.swipeEndIndex % 204800u))
				{
					if (this.control.runinfo.swipeEndIndex > 204800u)
					{
						num6 = (int)(this.control.runinfo.swipeEndIndex - this.control.runinfo.swipeEndIndex % 204800u - 204800u + (uint)(num5 % 204800));
					}
					else
					{
						num6 = 0;
					}
				}
				else
				{
					num6 = (int)(this.control.runinfo.swipeEndIndex - this.control.runinfo.swipeEndIndex % 204800u + (uint)(num5 % 204800));
				}
				this.control.UpdateLastGetRecordLocationIP((uint)(num6 + 1));
				base.lastRecordFlashIndex = num6 + 1;
			}
			wgAppRunInfo.raiseAppRunInfoCommStatus("");
			return num3;
		}
	}
}
