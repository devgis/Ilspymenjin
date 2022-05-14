using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	public class icController : wgMjController
	{
		private const int DoorMax = 4;

		public const int DUTY_ONOFF = 3;

		public const int DUTY_ON = 2;

		public const int DUTY_OFF = 1;

		private const int ReaderMax = 4;

		private ControllerRunInformation m_runinfo = new ControllerRunInformation();

		private int m_ControllerID;

		private int m_ControllerNO;

		private bool m_Active = true;

		private string m_Note = "";

		private int m_ZoneID;

		private bool[] m_doorActive = new bool[]
		{
			true,
			true,
			true,
			true
		};

		private int[] m_doorDelay = new int[]
		{
			3,
			3,
			3,
			3
		};

		private int[] m_doorControl = new int[]
		{
			3,
			3,
			3,
			3
		};

		private string[] m_doorName = new string[]
		{
			"",
			"",
			"",
			""
		};

		private string[] m_readerName = new string[]
		{
			"",
			"",
			"",
			""
		};

		private bool[] m_readerPasswordActive;

		private bool[] m_readerAsAttendActive;

		private int[] m_readerAsAttendControl;

		private string[] m_floorName;

		private SqlCommand cm;

		private OleDbCommand cm_Acc;

		public ControllerRunInformation runinfo
		{
			get
			{
				return this.m_runinfo;
			}
		}

		public int ControllerID
		{
			get
			{
				return this.m_ControllerID;
			}
			set
			{
				if (this.m_ControllerID >= 0)
				{
					this.m_ControllerID = value;
				}
			}
		}

		public int ControllerNO
		{
			get
			{
				return this.m_ControllerNO;
			}
			set
			{
				if (this.m_ControllerNO >= 0)
				{
					this.m_ControllerNO = value;
				}
			}
		}

		public bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
			}
		}

		public string Note
		{
			get
			{
				return this.m_Note;
			}
			set
			{
				this.m_Note = value;
			}
		}

		public int ZoneID
		{
			get
			{
				return this.m_ZoneID;
			}
			set
			{
				this.m_ZoneID = value;
			}
		}

		public int GetControllerRunInformationIP()
		{
			return this.GetControllerRunInformationIP("");
		}

		public int GetControllerRunInformationIP(string PCIPAddr)
		{
			byte[] array = null;
			if (base.GetMjControllerRunInformationIP(ref array, PCIPAddr) == 1 && array != null)
			{
				if (base.ControllerSN != -1)
				{
					this.m_runinfo.update(array, 20, (uint)base.ControllerSN);
				}
				else
				{
					uint controllerSN = (uint)((int)array[8] + ((int)array[9] << 8) + ((int)array[10] << 16) + ((int)array[11] << 24));
					this.m_runinfo.update(array, 20, controllerSN);
				}
				int num = 0;
				while (num < 10 && this.m_runinfo.newSwipes[num].IndexInDataFlash != 4294967295u)
				{
					num++;
				}
				return 1;
			}
			return -1;
		}

		public int GetControllerRunInformationIPNoTries()
		{
			byte[] array = null;
			if (base.GetMjControllerRunInformationIPNoTries(ref array) == 1 && array != null)
			{
				if (base.ControllerSN != -1)
				{
					this.m_runinfo.update(array, 20, (uint)base.ControllerSN);
				}
				else
				{
					uint controllerSN = (uint)((int)array[8] + ((int)array[9] << 8) + ((int)array[10] << 16) + ((int)array[11] << 24));
					this.m_runinfo.update(array, 20, controllerSN);
				}
				int num = 0;
				while (num < 10 && this.m_runinfo.newSwipes[num].IndexInDataFlash != 4294967295u)
				{
					num++;
				}
				return 1;
			}
			return -1;
		}

		public int GetControllerRunInformationIP_TCP(string strIP)
		{
			byte[] array = null;
			if (base.GetMjControllerRunInformationIP_TCP(strIP, ref array) == 1 && array != null)
			{
				this.m_runinfo.update(array, 20, (uint)base.ControllerSN);
				int num = 0;
				while (num < 10 && this.m_runinfo.newSwipes[num].IndexInDataFlash != 4294967295u)
				{
					num++;
				}
				return 1;
			}
			return -1;
		}

		public bool GetDoorActive(int doorNO)
		{
			return doorNO > 0 && doorNO <= 4 && this.m_doorActive[doorNO - 1];
		}

		public void SetDoorActive(int doorNO, bool active)
		{
			if (doorNO > 0 && doorNO <= 4)
			{
				this.m_doorActive[doorNO - 1] = active;
			}
		}

		public int GetDoorDelay(int doorNO)
		{
			if (doorNO > 0 && doorNO <= 4)
			{
				return this.m_doorDelay[doorNO - 1];
			}
			return 0;
		}

		public void SetDoorDelay(int doorNO, int doorDelay)
		{
			if (doorNO > 0 && doorNO <= 4)
			{
				this.m_doorDelay[doorNO - 1] = doorDelay;
			}
		}

		public int GetDoorControl(int doorNO)
		{
			if (doorNO > 0 && doorNO <= 4)
			{
				return this.m_doorControl[doorNO - 1];
			}
			return 0;
		}

		public void SetDoorControl(int doorNO, int doorControl)
		{
			if (doorNO > 0 && doorNO <= 4 && doorControl >= 0 && doorControl <= 3)
			{
				this.m_doorControl[doorNO - 1] = doorControl;
			}
		}

		public string GetDoorName(int doorNO)
		{
			if (doorNO > 0 && doorNO <= 4)
			{
				return wgTools.SetObjToStr(this.m_doorName[doorNO - 1]);
			}
			return "";
		}

		public string GetDoorNameByReaderNO(int readerNO)
		{
			int num;
			if (wgMjController.GetControllerType(base.ControllerSN) == 4)
			{
				num = readerNO;
			}
			else
			{
				num = readerNO + 1 >> 1;
			}
			if (num > 0 && num <= 4)
			{
				return wgTools.SetObjToStr(this.m_doorName[num - 1]);
			}
			return "";
		}

		public int GetDoorNO(string doorName)
		{
			int num = 0;
			while (num < 4 && !(this.m_doorName[num] == doorName))
			{
				num++;
			}
			if (num == 4)
			{
				num = 1;
			}
			else
			{
				num++;
			}
			return num;
		}

		public void SetDoorName(int doorNO, string doorName)
		{
			if (doorNO > 0 && doorNO <= 4)
			{
				this.m_doorName[doorNO - 1] = doorName;
			}
		}

		public string GetReaderName(int readerNO)
		{
			if (readerNO > 0 && readerNO <= 4)
			{
				return wgTools.SetObjToStr(this.m_readerName[readerNO - 1]);
			}
			return "";
		}

		public void SetReaderName(int readerNO, string readerName)
		{
			if (readerNO > 0 && readerNO <= 4)
			{
				this.m_readerName[readerNO - 1] = readerName;
			}
		}

		public string GetFloorName(int floorNO)
		{
			if (floorNO > 0 && floorNO <= this.m_floorName.Length)
			{
				return wgTools.SetObjToStr(this.m_floorName[floorNO - 1]);
			}
			return "";
		}

		public bool GetReaderAsAttendActive(int readerNO)
		{
			return readerNO > 0 && readerNO <= 4 && this.m_readerAsAttendActive[readerNO - 1];
		}

		public void SetReaderAsAttendActive(int readerNO, bool active)
		{
			if (readerNO > 0 && readerNO <= 4)
			{
				this.m_readerAsAttendActive[readerNO - 1] = active;
			}
		}

		public int GetReaderAsAttendControl(int readerNO)
		{
			if (readerNO > 0 && readerNO <= 4)
			{
				return this.m_readerAsAttendControl[readerNO - 1];
			}
			return 0;
		}

		public void SetReaderAsAttendControl(int readerNO, int AttendControl)
		{
			if (readerNO > 0 && readerNO <= 4 && AttendControl >= 0 && AttendControl <= 3)
			{
				this.m_readerAsAttendControl[readerNO - 1] = AttendControl;
			}
		}

		public static bool IsExisted2SN(int SN, int ControllerIDExclude)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icController.IsExisted2SN_Acc(SN, ControllerIDExclude);
			}
			bool result = false;
			try
			{
				string cmdText = string.Format("SELECT count(*) from [t_b_Controller] WHERE [f_ControllerID]<> {0:d} AND [f_ControllerSN] ={1:d} ", ControllerIDExclude, SN);
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						int num = int.Parse(sqlCommand.ExecuteScalar().ToString());
						if (num > 0)
						{
							result = true;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		public static bool IsExisted2SN_Acc(int SN, int ControllerIDExclude)
		{
			bool result = false;
			try
			{
				string cmdText = string.Format("SELECT count(*) from [t_b_Controller] WHERE [f_ControllerID]<> {0:d} AND [f_ControllerSN] ={1:d} ", ControllerIDExclude, SN);
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						int num = int.Parse(oleDbCommand.ExecuteScalar().ToString());
						if (num > 0)
						{
							result = true;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		public static bool IsExisted2NO(int ControllerNO, int ControllerIDExclude)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icController.IsExisted2NO_Acc(ControllerNO, ControllerIDExclude);
			}
			bool result = false;
			try
			{
				string cmdText = string.Format("SELECT count(*) from [t_b_Controller] WHERE   [f_ControllerID]<> {0:d} AND [f_ControllerNO] ={1:d} ", ControllerIDExclude, ControllerNO);
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						int num = int.Parse(sqlCommand.ExecuteScalar().ToString());
						if (num > 0)
						{
							result = true;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		public static bool IsExisted2NO_Acc(int ControllerNO, int ControllerIDExclude)
		{
			bool result = false;
			try
			{
				string cmdText = string.Format("SELECT count(*) from [t_b_Controller] WHERE   [f_ControllerID]<> {0:d} AND [f_ControllerNO] ={1:d} ", ControllerIDExclude, ControllerNO);
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						int num = int.Parse(oleDbCommand.ExecuteScalar().ToString());
						if (num > 0)
						{
							result = true;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		public static string StrDelFirstSame(string mainInfo, string deletedInfo)
		{
			if (string.IsNullOrEmpty(mainInfo) || string.IsNullOrEmpty(deletedInfo))
			{
				return mainInfo;
			}
			if (mainInfo.IndexOf(deletedInfo) == 0)
			{
				return mainInfo.Substring(deletedInfo.Length);
			}
			return mainInfo;
		}

		public static string StrReplaceFirstSame(string mainInfo, string oldInfo, string newInfo)
		{
			if (string.IsNullOrEmpty(mainInfo) || string.IsNullOrEmpty(oldInfo) || string.IsNullOrEmpty(newInfo))
			{
				return mainInfo;
			}
			if (mainInfo.IndexOf(oldInfo) == 0)
			{
				return newInfo + mainInfo.Substring(oldInfo.Length);
			}
			return mainInfo;
		}

		public static int GetMaxControllerNO()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icController.GetMaxControllerNO_Acc();
			}
			int result = 0;
			try
			{
				string cmdText = "SELECT MAX(f_ControllerNO) from t_b_Controller";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						result = int.Parse(sqlCommand.ExecuteScalar().ToString());
					}
				}
			}
			catch
			{
			}
			return result;
		}

		public static int GetMaxControllerNO_Acc()
		{
			int result = 0;
			try
			{
				string cmdText = "select max(CLNG(0 & [f_ControllerNO])) from t_b_Controller";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						result = int.Parse(oleDbCommand.ExecuteScalar().ToString());
					}
				}
			}
			catch
			{
			}
			return result;
		}

		public int AddIntoDB()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.AddIntoDB_Acc();
			}
			int result = -9;
			try
			{
				string text = "BEGIN TRANSACTION";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (this.cm = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						this.cm.ExecuteNonQuery();
						try
						{
							string text2 = "";
							for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
							{
								text2 = text2 + this.m_doorName[i] + ";   ";
							}
							text = " INSERT INTO t_b_Controller (f_ControllerNO, f_ControllerSN, f_Enabled, f_IP, f_PORT, f_Note, f_DoorNames,f_ZoneID) values (";
							text += this.m_ControllerNO.ToString();
							text = text + " , " + base.ControllerSN.ToString();
							text = text + " , " + (this.m_Active ? "1" : "0");
							text = text + " , " + wgTools.PrepareStr(base.IP);
							text = text + " , " + base.PORT.ToString();
							text = text + " , " + wgTools.PrepareStr(this.m_Note);
							text = text + " , " + wgTools.PrepareStr(text2);
							text = text + " , " + this.m_ZoneID.ToString();
							text += ")";
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
							text = "SELECT f_ControllerID from [t_b_Controller] where f_ControllerNo =" + this.m_ControllerNO.ToString();
							this.cm.CommandText = text;
							this.m_ControllerID = int.Parse("0" + wgTools.SetObjToStr(this.cm.ExecuteScalar()));
							text = " DELETE FROM [t_b_Door] ";
							text = text + " WHERE [f_ControllerID] = " + this.m_ControllerID.ToString();
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
							for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
							{
								text = " DELETE FROM [t_b_Door] ";
								text = text + " WHERE [f_DoorName] = " + wgTools.PrepareStr(this.m_doorName[i]);
								this.cm.CommandText = text;
								this.cm.ExecuteNonQuery();
								text = " INSERT INTO [t_b_Door] ";
								text += "([f_ControllerID], [f_DoorNO], [f_DoorName], [f_DoorControl], [f_DoorDelay], [f_DoorEnabled])";
								text = text + " Values(" + this.m_ControllerID.ToString();
								text = text + " , " + (i + 1).ToString();
								text = text + " , " + wgTools.PrepareStr(this.m_doorName[i]);
								text = text + " , " + this.m_doorControl[i].ToString();
								text = text + " , " + this.m_doorDelay[i].ToString();
								text = text + " , " + (this.m_doorActive[i] ? "1" : "0");
								text += ")";
								this.cm.CommandText = text;
								this.cm.ExecuteNonQuery();
							}
							text = " DELETE FROM [t_b_Reader] ";
							text = text + " WHERE [f_ControllerID] = " + this.m_ControllerID.ToString();
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
							for (int i = 0; i < wgMjController.GetControllerReaderNum(base.ControllerSN); i++)
							{
								text = " INSERT INTO [t_b_Reader] ";
								text += "([f_ControllerID], [f_ReaderNo], [f_ReaderName], [f_PasswordEnabled], [f_Attend],[f_DutyOnOff])";
								text = text + " Values(" + this.m_ControllerID.ToString();
								text = text + " , " + (i + 1).ToString();
								text = text + " , " + wgTools.PrepareStr(this.m_readerName[i]);
								text += " , 0";
								text = text + " , " + (this.m_readerAsAttendActive[i] ? "1" : "0");
								text = text + " , " + this.m_readerAsAttendControl[i].ToString();
								text += ")";
								this.cm.CommandText = text;
								this.cm.ExecuteNonQuery();
								if (wgMjController.IsElevator(base.ControllerSN))
								{
									break;
								}
							}
							text = "COMMIT TRANSACTION";
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public int AddIntoDB_Acc()
		{
			int result = -9;
			try
			{
				string text = "BEGIN TRANSACTION";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (this.cm_Acc = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						this.cm_Acc.ExecuteNonQuery();
						try
						{
							string text2 = "";
							for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
							{
								text2 = text2 + this.m_doorName[i] + ";   ";
							}
							text = " INSERT INTO t_b_Controller (f_ControllerNO, f_ControllerSN, f_Enabled, f_IP, f_PORT, f_Note, f_DoorNames,f_ZoneID) values (";
							text += this.m_ControllerNO.ToString();
							text = text + " , " + base.ControllerSN.ToString();
							text = text + " , " + (this.m_Active ? "1" : "0");
							text = text + " , " + wgTools.PrepareStr(base.IP);
							text = text + " , " + base.PORT.ToString();
							text = text + " , " + wgTools.PrepareStr(this.m_Note);
							text = text + " , " + wgTools.PrepareStr(text2);
							text = text + " , " + this.m_ZoneID.ToString();
							text += ")";
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
							text = "SELECT f_ControllerID from [t_b_Controller] where f_ControllerNo =" + this.m_ControllerNO.ToString();
							this.cm_Acc.CommandText = text;
							this.m_ControllerID = int.Parse("0" + wgTools.SetObjToStr(this.cm_Acc.ExecuteScalar()));
							text = " DELETE FROM [t_b_Door] ";
							text = text + " WHERE [f_ControllerID] = " + this.m_ControllerID.ToString();
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
							for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
							{
								text = " DELETE FROM [t_b_Door] ";
								text = text + " WHERE [f_DoorName] = " + wgTools.PrepareStr(this.m_doorName[i]);
								this.cm_Acc.CommandText = text;
								this.cm_Acc.ExecuteNonQuery();
								text = " INSERT INTO [t_b_Door] ";
								text += "([f_ControllerID], [f_DoorNO], [f_DoorName], [f_DoorControl], [f_DoorDelay], [f_DoorEnabled])";
								text = text + " Values(" + this.m_ControllerID.ToString();
								text = text + " , " + (i + 1).ToString();
								text = text + " , " + wgTools.PrepareStr(this.m_doorName[i]);
								text = text + " , " + this.m_doorControl[i].ToString();
								text = text + " , " + this.m_doorDelay[i].ToString();
								text = text + " , " + (this.m_doorActive[i] ? "1" : "0");
								text += ")";
								this.cm_Acc.CommandText = text;
								this.cm_Acc.ExecuteNonQuery();
							}
							text = " DELETE FROM [t_b_Reader] ";
							text = text + " WHERE [f_ControllerID] = " + this.m_ControllerID.ToString();
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
							for (int i = 0; i < wgMjController.GetControllerReaderNum(base.ControllerSN); i++)
							{
								text = " INSERT INTO [t_b_Reader] ";
								text += "([f_ControllerID], [f_ReaderNo], [f_ReaderName], [f_PasswordEnabled], [f_Attend],[f_DutyOnOff])";
								text = text + " Values(" + this.m_ControllerID.ToString();
								text = text + " , " + (i + 1).ToString();
								text = text + " , " + wgTools.PrepareStr(this.m_readerName[i]);
								text += " , 0";
								text = text + " , " + (this.m_readerAsAttendActive[i] ? "1" : "0");
								text = text + " , " + this.m_readerAsAttendControl[i].ToString();
								text += ")";
								this.cm_Acc.CommandText = text;
								this.cm_Acc.ExecuteNonQuery();
								if (wgMjController.IsElevator(base.ControllerSN))
								{
									break;
								}
							}
							text = "COMMIT TRANSACTION";
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public int UpdateIntoDB(bool ControllerTypeChanged)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.UpdateIntoDB_Acc(ControllerTypeChanged);
			}
			int result = -9;
			try
			{
				string text = "BEGIN TRANSACTION";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (this.cm = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						this.cm.ExecuteNonQuery();
						try
						{
							string text2 = "";
							for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
							{
								text2 = text2 + this.m_doorName[i] + ";   ";
							}
							text = " UPDATE t_b_Controller ";
							text += string.Format(" SET  f_ControllerNO={0}, f_ControllerSN={1}, f_Enabled={2}, f_IP={3}, f_PORT={4}, f_Note={5}, f_DoorNames={6}, f_ZoneID={7} ", new object[]
							{
								this.m_ControllerNO.ToString(),
								base.ControllerSN.ToString(),
								this.m_Active ? "1" : "0",
								wgTools.PrepareStr(base.IP),
								base.PORT.ToString(),
								wgTools.PrepareStr(this.m_Note),
								wgTools.PrepareStr(text2),
								this.m_ZoneID.ToString()
							});
							text = text + "  WHERE [f_ControllerID]=" + this.m_ControllerID.ToString();
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
							if (ControllerTypeChanged)
							{
								text = "DELETE FROM t_b_Reader WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
								this.cm.CommandText = text;
								this.cm.ExecuteNonQuery();
								text = "DELETE  FROM t_b_Door WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
								this.cm.CommandText = text;
								this.cm.ExecuteNonQuery();
								for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
								{
									text = " INSERT INTO [t_b_Door] ";
									text += "([f_ControllerID], [f_DoorNO], [f_DoorName], [f_DoorControl], [f_DoorDelay], [f_DoorEnabled])";
									text = text + " Values(" + this.m_ControllerID.ToString();
									text = text + " , " + (i + 1).ToString();
									text = text + " , " + wgTools.PrepareStr(this.m_doorName[i]);
									text = text + " , " + this.m_doorControl[i].ToString();
									text = text + " , " + this.m_doorDelay[i].ToString();
									text = text + " , " + (this.m_doorActive[i] ? "1" : "0");
									text += ")";
									this.cm.CommandText = text;
									this.cm.ExecuteNonQuery();
								}
								for (int i = 0; i < wgMjController.GetControllerReaderNum(base.ControllerSN); i++)
								{
									text = " INSERT INTO [t_b_Reader] ";
									text += "([f_ControllerID], [f_ReaderNo], [f_ReaderName], [f_PasswordEnabled], [f_Attend],[f_DutyOnOff])";
									text = text + " Values(" + this.m_ControllerID.ToString();
									text = text + " , " + (i + 1).ToString();
									text = text + " , " + wgTools.PrepareStr(this.m_readerName[i]);
									text += " , 0";
									text = text + " , " + (this.m_readerAsAttendActive[i] ? "1" : "0");
									text = text + " , " + this.m_readerAsAttendControl[i].ToString();
									text += ")";
									this.cm.CommandText = text;
									this.cm.ExecuteNonQuery();
								}
							}
							else
							{
								for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
								{
									text = " UPDATE [t_b_Door] SET ";
									text += string.Format(" [f_DoorName]={0}, [f_DoorControl]={1}, [f_DoorDelay]={2}, [f_DoorEnabled]={3} ", new object[]
									{
										wgTools.PrepareStr(this.m_doorName[i]),
										this.m_doorControl[i].ToString(),
										this.m_doorDelay[i].ToString(),
										this.m_doorActive[i] ? "1" : "0"
									});
									text = text + "  WHERE [f_ControllerID]=" + this.m_ControllerID.ToString();
									text = text + "  AND [f_DoorNO]=" + (i + 1).ToString();
									this.cm.CommandText = text;
									this.cm.ExecuteNonQuery();
								}
								for (int i = 0; i < wgMjController.GetControllerReaderNum(base.ControllerSN); i++)
								{
									text = " UPDATE [t_b_Reader] SET ";
									text += string.Format(" [f_ReaderName]={0}, [f_Attend]={1},[f_DutyOnOff]={2}", wgTools.PrepareStr(this.m_readerName[i]), this.m_readerAsAttendActive[i] ? "1" : "0", this.m_readerAsAttendControl[i].ToString());
									text = text + "  WHERE [f_ControllerID]=" + this.m_ControllerID.ToString();
									text = text + "  AND [f_ReaderNo]=" + (i + 1).ToString();
									this.cm.CommandText = text;
									this.cm.ExecuteNonQuery();
								}
							}
							text = "COMMIT TRANSACTION";
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							this.cm.CommandText = text;
							this.cm.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public int UpdateIntoDB_Acc(bool ControllerTypeChanged)
		{
			int result = -9;
			try
			{
				string text = "BEGIN TRANSACTION";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (this.cm_Acc = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						this.cm_Acc.ExecuteNonQuery();
						try
						{
							string text2 = "";
							for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
							{
								text2 = text2 + this.m_doorName[i] + ";   ";
							}
							text = " UPDATE t_b_Controller ";
							text += string.Format(" SET  f_ControllerNO={0}, f_ControllerSN={1}, f_Enabled={2}, f_IP={3}, f_PORT={4}, f_Note={5}, f_DoorNames={6}, f_ZoneID={7} ", new object[]
							{
								this.m_ControllerNO.ToString(),
								base.ControllerSN.ToString(),
								this.m_Active ? "1" : "0",
								wgTools.PrepareStr(base.IP),
								base.PORT.ToString(),
								wgTools.PrepareStr(this.m_Note),
								wgTools.PrepareStr(text2),
								this.m_ZoneID.ToString()
							});
							text = text + "  WHERE [f_ControllerID]=" + this.m_ControllerID.ToString();
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
							if (ControllerTypeChanged)
							{
								text = "DELETE FROM t_b_Reader WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
								this.cm_Acc.CommandText = text;
								this.cm_Acc.ExecuteNonQuery();
								text = "DELETE  FROM t_b_Door WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
								this.cm_Acc.CommandText = text;
								this.cm_Acc.ExecuteNonQuery();
								for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
								{
									text = " INSERT INTO [t_b_Door] ";
									text += "([f_ControllerID], [f_DoorNO], [f_DoorName], [f_DoorControl], [f_DoorDelay], [f_DoorEnabled])";
									text = text + " Values(" + this.m_ControllerID.ToString();
									text = text + " , " + (i + 1).ToString();
									text = text + " , " + wgTools.PrepareStr(this.m_doorName[i]);
									text = text + " , " + this.m_doorControl[i].ToString();
									text = text + " , " + this.m_doorDelay[i].ToString();
									text = text + " , " + (this.m_doorActive[i] ? "1" : "0");
									text += ")";
									this.cm_Acc.CommandText = text;
									this.cm_Acc.ExecuteNonQuery();
								}
								for (int i = 0; i < wgMjController.GetControllerReaderNum(base.ControllerSN); i++)
								{
									text = " INSERT INTO [t_b_Reader] ";
									text += "([f_ControllerID], [f_ReaderNo], [f_ReaderName], [f_PasswordEnabled], [f_Attend],[f_DutyOnOff])";
									text = text + " Values(" + this.m_ControllerID.ToString();
									text = text + " , " + (i + 1).ToString();
									text = text + " , " + wgTools.PrepareStr(this.m_readerName[i]);
									text += " , 0";
									text = text + " , " + (this.m_readerAsAttendActive[i] ? "1" : "0");
									text = text + " , " + this.m_readerAsAttendControl[i].ToString();
									text += ")";
									this.cm_Acc.CommandText = text;
									this.cm_Acc.ExecuteNonQuery();
								}
							}
							else
							{
								for (int i = 0; i < wgMjController.GetControllerType(base.ControllerSN); i++)
								{
									text = " UPDATE [t_b_Door] SET ";
									text += string.Format(" [f_DoorName]={0}, [f_DoorControl]={1}, [f_DoorDelay]={2}, [f_DoorEnabled]={3} ", new object[]
									{
										wgTools.PrepareStr(this.m_doorName[i]),
										this.m_doorControl[i].ToString(),
										this.m_doorDelay[i].ToString(),
										this.m_doorActive[i] ? "1" : "0"
									});
									text = text + "  WHERE [f_ControllerID]=" + this.m_ControllerID.ToString();
									text = text + "  AND [f_DoorNO]=" + (i + 1).ToString();
									this.cm_Acc.CommandText = text;
									this.cm_Acc.ExecuteNonQuery();
								}
								for (int i = 0; i < wgMjController.GetControllerReaderNum(base.ControllerSN); i++)
								{
									text = " UPDATE [t_b_Reader] SET ";
									text += string.Format(" [f_ReaderName]={0}, [f_Attend]={1},[f_DutyOnOff]={2}", wgTools.PrepareStr(this.m_readerName[i]), this.m_readerAsAttendActive[i] ? "1" : "0", this.m_readerAsAttendControl[i].ToString());
									text = text + "  WHERE [f_ControllerID]=" + this.m_ControllerID.ToString();
									text = text + "  AND [f_ReaderNo]=" + (i + 1).ToString();
									this.cm_Acc.CommandText = text;
									this.cm_Acc.ExecuteNonQuery();
								}
							}
							text = "COMMIT TRANSACTION";
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							this.cm_Acc.CommandText = text;
							this.cm_Acc.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public static int DeleteControllerFromDB(int ControllerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icController.DeleteControllerFromDB_Acc(ControllerID);
			}
			int num = ControllerID;
			if (num > 0)
			{
				string text = "BEGIN TRANSACTION";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
						try
						{
							text = " DELETE FROM t_b_ElevatorGroup ";
							text += " WHERE ";
							text = text + " t_b_ElevatorGroup.f_ControllerID =  " + num.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE  FROM t_b_UserFloor ";
							text += " WHERE f_floorID IN  ";
							text = text + " (SELECT f_floorID FROM t_b_Floor WHERE t_b_Floor.f_ControllerID =  " + num.ToString() + ")";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE FROM t_b_Floor ";
							text = text + " WHERE t_b_Floor.f_ControllerID =  " + num.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "DELETE FROM t_b_Controller WHERE f_ControllerID =  " + num.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "DELETE  FROM t_b_Door WHERE f_ControllerID =  " + num.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "DELETE  FROM t_d_Privilege WHERE f_ControllerID =  " + num.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							sqlCommand.ExecuteNonQuery();
							text = "COMMIT TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
						}
						catch (Exception ex)
						{
							wgAppConfig.wgLog(ex.ToString());
							text = "ROLLBACK TRANSACTION";
							if (sqlCommand.Connection.State != ConnectionState.Open)
							{
								sqlCommand.Connection.Open();
							}
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							throw;
						}
					}
				}
			}
			return 1;
		}

		public static int DeleteControllerFromDB_Acc(int ControllerID)
		{
			int num = ControllerID;
			if (num > 0)
			{
				string text = "BEGIN TRANSACTION";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.ExecuteNonQuery();
						try
						{
							text = " DELETE FROM t_b_ElevatorGroup ";
							text += " WHERE ";
							text = text + " t_b_ElevatorGroup.f_ControllerID =  " + num.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE  FROM t_b_UserFloor ";
							text += " WHERE f_floorID IN  ";
							text = text + " (SELECT f_floorID FROM t_b_Floor WHERE t_b_Floor.f_ControllerID =  " + num.ToString() + ")";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE FROM t_b_Floor ";
							text = text + " WHERE t_b_Floor.f_ControllerID =  " + num.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = "DELETE FROM t_b_Controller WHERE f_ControllerID =  " + num.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = "DELETE  FROM t_b_Door WHERE f_ControllerID =  " + num.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = "DELETE  FROM t_d_Privilege WHERE f_ControllerID =  " + num.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							oleDbCommand.ExecuteNonQuery();
							text = "COMMIT TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
						}
						catch (Exception ex)
						{
							wgAppConfig.wgLog(ex.ToString());
							text = "ROLLBACK TRANSACTION";
							if (oleDbCommand.Connection.State != ConnectionState.Open)
							{
								oleDbCommand.Connection.Open();
							}
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							throw;
						}
					}
				}
			}
			return 1;
		}

		public int GetInfoFromDBByDoorName(string DoorName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.GetInfoFromDBByDoorName_Acc(DoorName);
			}
			int num = 0;
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Door WHERE f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
				}
			}
			if (num > 0)
			{
				this.GetInfoFromDBByControllerID(num);
			}
			return 1;
		}

		public int GetInfoFromDBByDoorName_Acc(string DoorName)
		{
			int num = 0;
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Door WHERE f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
				}
			}
			if (num > 0)
			{
				this.GetInfoFromDBByControllerID(num);
			}
			return 1;
		}

		public int GetInfoFromDBByControllerID(int ControllerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.GetInfoFromDBByControllerID_Acc(ControllerID);
			}
			int num = ControllerID;
			if (num > 0)
			{
				this.m_ControllerID = num;
				string text = " SELECT * ";
				text = text + " FROM t_b_Controller WHERE f_ControllerID =  " + num.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							this.m_ControllerNO = (int)sqlDataReader["f_ControllerNO"];
							base.ControllerSN = (int)sqlDataReader["f_ControllerSN"];
							this.m_Active = (int.Parse(sqlDataReader["f_Enabled"].ToString()) > 0);
							base.IP = wgTools.SetObjToStr(sqlDataReader["f_IP"]);
							base.PORT = (int)sqlDataReader["f_PORT"];
							this.m_Note = wgTools.SetObjToStr(sqlDataReader["f_Note"]);
							this.m_ZoneID = (int)sqlDataReader["f_ZoneID"];
						}
						sqlDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Door WHERE f_ControllerID =  " + num.ToString();
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num2 = int.Parse(sqlDataReader["f_DoorNO"].ToString()) - 1;
							this.m_doorName[num2] = (string)sqlDataReader["f_DoorName"];
							this.m_doorControl[num2] = int.Parse(sqlDataReader["f_DoorControl"].ToString());
							this.m_doorDelay[num2] = int.Parse(sqlDataReader["f_DoorDelay"].ToString());
							this.m_doorActive[num2] = (int.Parse(sqlDataReader["f_DoorEnabled"].ToString()) > 0);
						}
						sqlDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Reader WHERE f_ControllerID =  " + num.ToString();
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num3 = int.Parse(sqlDataReader["f_ReaderNo"].ToString()) - 1;
							this.m_readerName[num3] = (string)sqlDataReader["f_ReaderName"];
							this.m_readerPasswordActive[num3] = (int.Parse(sqlDataReader["f_PasswordEnabled"].ToString()) > 0);
							this.m_readerAsAttendActive[num3] = (int.Parse(sqlDataReader["f_Attend"].ToString()) > 0);
							this.m_readerAsAttendControl[num3] = int.Parse(sqlDataReader["f_DutyOnOff"].ToString());
						}
						sqlDataReader.Close();
						text = "  SELECT t_b_Reader.f_ReaderName, t_b_Floor.f_floorID, t_b_Door.f_DoorName + '.' + t_b_Floor.f_floorName as f_floorFullName,  ";
						text += "   t_b_Door.f_DoorName, ";
						text += "   t_b_Floor.f_floorNO, t_b_Controller.f_ZoneID, t_b_Floor.f_floorName, t_b_Door.f_ControllerID  ";
						text += "    FROM t_b_Floor , t_b_Door, t_b_Controller, t_b_Reader ";
						text += "   where t_b_Floor.f_DoorID = t_b_Door.f_DoorID and t_b_Door.f_ControllerID = t_b_Controller.f_ControllerNO and t_b_Reader.f_ControllerID = t_b_Floor.f_ControllerID ";
						text = text + " and  t_b_Floor.f_ControllerID =  " + num.ToString();
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num4 = int.Parse(sqlDataReader["f_floorNO"].ToString()) - 1;
							this.m_floorName[num4] = (string)sqlDataReader["f_floorFullName"];
						}
						sqlDataReader.Close();
					}
				}
			}
			return 1;
		}

		public int GetInfoFromDBByControllerID_Acc(int ControllerID)
		{
			int num = ControllerID;
			if (num > 0)
			{
				this.m_ControllerID = num;
				string text = " SELECT * ";
				text = text + " FROM t_b_Controller WHERE f_ControllerID =  " + num.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							this.m_ControllerNO = (int)oleDbDataReader["f_ControllerNO"];
							base.ControllerSN = (int)oleDbDataReader["f_ControllerSN"];
							this.m_Active = (int.Parse(oleDbDataReader["f_Enabled"].ToString()) > 0);
							base.IP = wgTools.SetObjToStr(oleDbDataReader["f_IP"]);
							base.PORT = (int)oleDbDataReader["f_PORT"];
							this.m_Note = wgTools.SetObjToStr(oleDbDataReader["f_Note"]);
							this.m_ZoneID = (int)oleDbDataReader["f_ZoneID"];
						}
						oleDbDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Door WHERE f_ControllerID =  " + num.ToString();
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							int num2 = int.Parse(oleDbDataReader["f_DoorNO"].ToString()) - 1;
							this.m_doorName[num2] = (string)oleDbDataReader["f_DoorName"];
							this.m_doorControl[num2] = int.Parse(oleDbDataReader["f_DoorControl"].ToString());
							this.m_doorDelay[num2] = int.Parse(oleDbDataReader["f_DoorDelay"].ToString());
							this.m_doorActive[num2] = (int.Parse(oleDbDataReader["f_DoorEnabled"].ToString()) > 0);
						}
						oleDbDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Reader WHERE f_ControllerID =  " + num.ToString();
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							int num3 = int.Parse(oleDbDataReader["f_ReaderNo"].ToString()) - 1;
							this.m_readerName[num3] = (string)oleDbDataReader["f_ReaderName"];
							this.m_readerPasswordActive[num3] = (int.Parse(oleDbDataReader["f_PasswordEnabled"].ToString()) > 0);
							this.m_readerAsAttendActive[num3] = (int.Parse(oleDbDataReader["f_Attend"].ToString()) > 0);
							this.m_readerAsAttendControl[num3] = int.Parse(oleDbDataReader["f_DutyOnOff"].ToString());
						}
						oleDbDataReader.Close();
					}
				}
			}
			return 1;
		}

		public int GetInfoFromDBByControllerSN(int ControllerSN)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.GetInfoFromDBByControllerSN_Acc(ControllerSN);
			}
			int num = ControllerSN;
			if (num > 0)
			{
				ControllerSN = num;
				string text = " SELECT * ";
				text = text + " FROM t_b_Controller WHERE f_ControllerSN =  " + num.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							this.m_ControllerNO = (int)sqlDataReader["f_ControllerNO"];
							ControllerSN = (int)sqlDataReader["f_ControllerSN"];
							this.m_ControllerID = (int)sqlDataReader["f_ControllerID"];
							this.m_Active = (int.Parse(sqlDataReader["f_Enabled"].ToString()) > 0);
							base.IP = wgTools.SetObjToStr(sqlDataReader["f_IP"]);
							base.PORT = (int)sqlDataReader["f_PORT"];
							this.m_Note = wgTools.SetObjToStr(sqlDataReader["f_Note"]);
							this.m_ZoneID = (int)sqlDataReader["f_ZoneID"];
						}
						sqlDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Door WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num2 = int.Parse(sqlDataReader["f_DoorNO"].ToString()) - 1;
							this.m_doorName[num2] = (string)sqlDataReader["f_DoorName"];
							this.m_doorControl[num2] = int.Parse(sqlDataReader["f_DoorControl"].ToString());
							this.m_doorDelay[num2] = int.Parse(sqlDataReader["f_DoorDelay"].ToString());
							this.m_doorActive[num2] = (int.Parse(sqlDataReader["f_DoorEnabled"].ToString()) > 0);
						}
						sqlDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Reader WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num3 = int.Parse(sqlDataReader["f_ReaderNo"].ToString()) - 1;
							this.m_readerName[num3] = (string)sqlDataReader["f_ReaderName"];
							this.m_readerPasswordActive[num3] = (int.Parse(sqlDataReader["f_PasswordEnabled"].ToString()) > 0);
							this.m_readerAsAttendActive[num3] = (int.Parse(sqlDataReader["f_Attend"].ToString()) > 0);
							this.m_readerAsAttendControl[num3] = int.Parse(sqlDataReader["f_DutyOnOff"].ToString());
						}
						sqlDataReader.Close();
					}
				}
			}
			return 1;
		}

		public int GetInfoFromDBByControllerSN_Acc(int ControllerSN)
		{
			int num = ControllerSN;
			if (num > 0)
			{
				ControllerSN = num;
				string text = " SELECT * ";
				text = text + " FROM t_b_Controller WHERE f_ControllerSN =  " + num.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							this.m_ControllerNO = (int)oleDbDataReader["f_ControllerNO"];
							ControllerSN = (int)oleDbDataReader["f_ControllerSN"];
							this.m_ControllerID = (int)oleDbDataReader["f_ControllerID"];
							this.m_Active = (int.Parse(oleDbDataReader["f_Enabled"].ToString()) > 0);
							base.IP = wgTools.SetObjToStr(oleDbDataReader["f_IP"]);
							base.PORT = (int)oleDbDataReader["f_PORT"];
							this.m_Note = wgTools.SetObjToStr(oleDbDataReader["f_Note"]);
							this.m_ZoneID = (int)oleDbDataReader["f_ZoneID"];
						}
						oleDbDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Door WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							int num2 = int.Parse(oleDbDataReader["f_DoorNO"].ToString()) - 1;
							this.m_doorName[num2] = (string)oleDbDataReader["f_DoorName"];
							this.m_doorControl[num2] = int.Parse(oleDbDataReader["f_DoorControl"].ToString());
							this.m_doorDelay[num2] = int.Parse(oleDbDataReader["f_DoorDelay"].ToString());
							this.m_doorActive[num2] = (int.Parse(oleDbDataReader["f_DoorEnabled"].ToString()) > 0);
						}
						oleDbDataReader.Close();
						text = " SELECT * ";
						text = text + " FROM t_b_Reader WHERE f_ControllerID =  " + this.m_ControllerID.ToString();
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							int num3 = int.Parse(oleDbDataReader["f_ReaderNo"].ToString()) - 1;
							this.m_readerName[num3] = (string)oleDbDataReader["f_ReaderName"];
							this.m_readerPasswordActive[num3] = (int.Parse(oleDbDataReader["f_PasswordEnabled"].ToString()) > 0);
							this.m_readerAsAttendActive[num3] = (int.Parse(oleDbDataReader["f_Attend"].ToString()) > 0);
							this.m_readerAsAttendControl[num3] = int.Parse(oleDbDataReader["f_DutyOnOff"].ToString());
						}
						oleDbDataReader.Close();
					}
				}
			}
			return 1;
		}

		public int RemoteOpenDoorIP(string DoorName)
		{
			int num = 0;
			while (num < 4 && !(this.m_doorName[num] == DoorName))
			{
				num++;
			}
			if (num >= 4)
			{
				return -1;
			}
			return base.RemoteOpenDoorIP(num + 1, (uint)icOperator.OperatorID, 18446744073709551615uL);
		}

		public int RemoteOpenDoorIP(string DoorName, uint operatorId, ulong operatorCardNO)
		{
			int num = 0;
			while (num < 4 && !(this.m_doorName[num] == DoorName))
			{
				num++;
			}
			if (num >= 4)
			{
				return -1;
			}
			return base.RemoteOpenDoorIP(num + 1, operatorId, operatorCardNO);
		}

		public int DirectSetDoorControlIP(string DoorName, int doorControl)
		{
			int num = 0;
			while (num < 4 && !(this.m_doorName[num] == DoorName))
			{
				num++;
			}
			if (num >= 4)
			{
				return -1;
			}
			wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
			wgMjControllerConfigure.DoorControlSet(num + 1, doorControl);
			return base.UpdateConfigureIP(wgMjControllerConfigure);
		}

		public int UpdateControlTimeSegListIP(icControllerTimeSegList controlTimeSegList)
		{
			return base.UpdateControlTimeSegListIP(controlTimeSegList.ToByte());
		}

		public int UpdateControlTaskListIP(wgMjControllerTaskList controlTaskList)
		{
			return base.UpdateControlTaskListIP(controlTaskList.ToByte());
		}

		public int GetControlTaskListIP(ref wgMjControllerTaskList controlTaskList)
		{
			try
			{
				byte[] array = null;
				if (base.GetControlTaskListIP(ref array) == 1 && array != null)
				{
					controlTaskList = new wgMjControllerTaskList(array);
					return 1;
				}
			}
			catch (Exception)
			{
			}
			return -1;
		}

		public icController()
		{
			bool[] readerPasswordActive = new bool[4];
			this.m_readerPasswordActive = readerPasswordActive;
			this.m_readerAsAttendActive = new bool[]
			{
				true,
				true,
				true,
				true
			};
			this.m_readerAsAttendControl = new int[]
			{
				3,
				3,
				3,
				3
			};
			this.m_floorName = new string[]
			{
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};
			//base..ctor();
		}
	}
}
