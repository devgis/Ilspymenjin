using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.DataOper
{
	internal class icPrivilege : wgMjControllerPrivilege
	{
		private const long CARDNOMAX = 4294967294L;

		private DataTable dtPrivilege;

		private int m_PrivilegeTotal;

		private int m_ConsumersTotal;

		private int m_ValidPrivilegeTotal;

		private DataColumn dc;

		private DataTable dtUserFloorCnt;

		private DataView dvUserFloorCnt;

		private DataTable dtUserFloor;

		public int PrivilegTotal
		{
			get
			{
				return this.m_PrivilegeTotal;
			}
		}

		public int ValidPrivilege
		{
			get
			{
				return this.m_ValidPrivilegeTotal;
			}
		}

		public int ConsumersTotal
		{
			get
			{
				return this.m_ConsumersTotal;
			}
		}

		public icPrivilege()
		{
			base.AllowUpload();
			this.bAllowUploadUserName = false;
			int num = 0;
			try
			{
				int.TryParse(wgAppConfig.GetKeyVal("AllowUploadUserName"), out num);
			}
			catch (Exception)
			{
			}
			if (num > 0)
			{
				this.bAllowUploadUserName = true;
			}
			if (this.dtPrivilege == null)
			{
				this.dtPrivilege = new DataTable("Privilege");
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.UInt32");
				this.dc.ColumnName = "f_ConsumerID";
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.UInt32");
				this.dc.ColumnName = "f_CardNO";
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_BeginYMD";
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.DateTime");
				this.dc.ColumnName = "f_EndYMD";
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.String");
				this.dc.ColumnName = "f_PIN";
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_ControlSegID1";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_ControlSegID2";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_ControlSegID3";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_ControlSegID4";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_DoorFirstCard_1";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_DoorFirstCard_2";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_DoorFirstCard_3";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_DoorFirstCard_4";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_MoreCards_GrpID_1";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_MoreCards_GrpID_2";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_MoreCards_GrpID_3";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_MoreCards_GrpID_4";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.UInt32");
				this.dc.ColumnName = "f_MaxSwipe";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.Byte");
				this.dc.ColumnName = "f_IsSuperCard";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.UInt64");
				this.dc.ColumnName = "f_AllowFloors";
				this.dc.DefaultValue = 0;
				this.dtPrivilege.Columns.Add(this.dc);
				this.dc = new DataColumn();
				this.dc.DataType = Type.GetType("System.String");
				this.dc.ColumnName = "f_ConsumerName";
				this.dtPrivilege.Columns.Add(this.dc);
			}
		}

		public int upload(int ControllerSN, string DoorName)
		{
			return base.UploadIP(ControllerSN, "", 60000, DoorName, this.dtPrivilege);
		}

		public int upload(int ControllerSN, string IP, string DoorName)
		{
			return base.UploadIP(ControllerSN, IP, 60000, DoorName, this.dtPrivilege);
		}

		public int upload(int ControllerSN, string IP, int Port, string DoorName)
		{
			return base.UploadIP(ControllerSN, IP, Port, DoorName, this.dtPrivilege);
		}

		protected override void DisplayProcessInfo(string info, int infoCode, int specialInfo)
		{
			if (infoCode == -100001)
			{
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", info, wgTools.gADCT ? CommonStr.strUploadFail_200K : CommonStr.strUploadFail_40K, specialInfo));
				return;
			}
			switch (infoCode)
			{
			case 100001:
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}", info, CommonStr.strUploadPreparing));
				return;
			case 100002:
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", info, CommonStr.strUploadingPrivileges, specialInfo));
				return;
			case 100003:
				wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1}[{2:d}]", info, CommonStr.strUploadedPrivileges, specialInfo));
				return;
			default:
				wgAppRunInfo.raiseAppRunInfoCommStatus(info);
				return;
			}
		}

		public void getPrivilegeBySN(int ControllerSN)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.getPrivilegeBySN_Acc(ControllerSN);
				return;
			}
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Controller WHERE f_ControllerSN =  " + ControllerSN.ToString();
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
					if (num > 0)
					{
						this.getPrivilegeByID(num);
					}
				}
			}
		}

		public void getPrivilegeBySN_Acc(int ControllerSN)
		{
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Controller WHERE f_ControllerSN =  " + ControllerSN.ToString();
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
					if (num > 0)
					{
						this.getPrivilegeByID(num);
					}
				}
			}
		}

		public void getPrivilegeByDoorName(string DoorName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.getPrivilegeByDoorName_Acc(DoorName);
				return;
			}
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Door WHERE f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
					if (num > 0)
					{
						this.getPrivilegeByID(num);
					}
				}
			}
		}

		public void getPrivilegeByDoorName_Acc(string DoorName)
		{
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Door WHERE f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
					if (num > 0)
					{
						this.getPrivilegeByID(num);
					}
				}
			}
		}

		public int getControllerIDByDoorName(string DoorName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getControllerIDByDoorName_Acc(DoorName);
			}
			int result = 0;
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Door WHERE f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					result = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
				}
			}
			return result;
		}

		public int getControllerIDByDoorName_Acc(string DoorName)
		{
			int result = 0;
			string text = " SELECT f_ControllerID ";
			text = text + " FROM t_b_Door WHERE f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					result = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
				}
			}
			return result;
		}

		public int getControllerSNByDoorName(string DoorName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getControllerSNByDoorName_Acc(DoorName);
			}
			int result = 0;
			string text = " SELECT f_ControllerSN ";
			text = text + " FROM t_b_Door a,t_b_Controller b WHERE a.f_ControllerID = b.f_ControllerID AND f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					result = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
				}
			}
			return result;
		}

		public int getControllerSNByDoorName_Acc(string DoorName)
		{
			int result = 0;
			string text = " SELECT f_ControllerSN ";
			text = text + " FROM t_b_Door a,t_b_Controller b WHERE a.f_ControllerID = b.f_ControllerID AND f_DoorName =  " + wgTools.PrepareStr(DoorName);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					result = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
				}
			}
			return result;
		}

		public int getControllerSNByID(int ControllerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getControllerSNByID_Acc(ControllerID);
			}
			int result = 0;
			string text = " SELECT f_ControllerSN ";
			text = text + " FROM t_b_Controller b WHERE  b.f_ControllerID =  " + ControllerID;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					result = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
				}
			}
			return result;
		}

		public int getControllerSNByID_Acc(int ControllerID)
		{
			int result = 0;
			string text = " SELECT f_ControllerSN ";
			text = text + " FROM t_b_Controller b WHERE  b.f_ControllerID =  " + ControllerID;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					result = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
				}
			}
			return result;
		}

		public int getElevatorPrivilegeByID(int ControllerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getElevatorPrivilegeByID_Acc(ControllerID);
			}
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			wgTools.WriteLine("getPrivilegeByID Start");
			this.dtPrivilege.Rows.Clear();
			string cmdText = string.Format(" SELECT a.f_ConsumerID, COUNT(a.f_FloorID) as cnt FROM t_b_UserFloor a\r\nWHERE a.f_FloorID IN \r\n(SELECT b.f_floorid from t_b_floor b where b.[f_ControllerID]={0} or b.[f_ControllerID] in \r\n (select c.f_ControllerID from t_b_ElevatorGroup c where c.f_ElevatorGroupNO in \r\n   (select d.f_ElevatorGroupNO from t_b_ElevatorGroup d where d.f_ControllerID = {0})))\r\nGROUP BY a.f_ConsumerID ", ControllerID);
			this.dtUserFloorCnt = new DataTable();
			this.dvUserFloorCnt = new DataView(this.dtUserFloorCnt);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						sqlDataAdapter.Fill(this.dtUserFloorCnt);
					}
				}
			}
			cmdText = string.Format("SELECT b.f_CardNO, b.f_ConsumerID, b.f_BeginYMD, b.f_EndYMD, b.f_PIN, a.f_ControlSegID, b.f_DoorEnabled, c.f_floorNO\r\nFROM [t_b_UserFloor] a\r\nINNER JOIN t_b_Consumer b ON  a.f_ConsumerID = b.f_ConsumerID AND b.f_CardNO IS NOT NULL\r\nINNER JOIN t_b_Floor c ON a.f_FloorID = c.f_FloorID\r\nWHERE f_ControllerID = {0}\r\nORDER BY f_CardNO", ControllerID);
			this.dtUserFloor = new DataTable();
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(cmdText, sqlConnection2))
				{
					using (SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2))
					{
						sqlCommand2.CommandTimeout = wgAppConfig.dbCommandTimeout;
						sqlDataAdapter2.Fill(this.dtUserFloor);
					}
				}
			}
			DataRow dataRow = this.dtPrivilege.NewRow();
			dataRow["f_CardNO"] = 0;
			dataRow["f_ControlSegID1"] = 0;
			dataRow["f_ControlSegID2"] = 0;
			dataRow["f_ControlSegID3"] = 0;
			dataRow["f_ControlSegID4"] = 0;
			dataRow["f_MoreCards_GrpID_3"] = 0;
			dataRow["f_MoreCards_GrpID_4"] = 0;
			dataRow["f_MoreCards_GrpID_2"] = 0;
			dataRow["f_DoorFirstCard_2"] = 0;
			dataRow["f_DoorFirstCard_3"] = 0;
			dataRow["f_DoorFirstCard_4"] = 0;
			dataRow["f_PIN"] = 0;
			int num = 0;
			for (int i = 0; i < this.dtUserFloor.Rows.Count; i++)
			{
				if (wgMjControllerPrivilege.bStopUploadPrivilege)
				{
					return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
				}
				DataRow dataRow2 = this.dtUserFloor.Rows[i];
				if ((long)dataRow2["f_CardNO"] >= 0L && (long)dataRow2["f_CardNO"] <= (long)((ulong)-2) && int.Parse(wgTools.SetObjToStr(dataRow2["f_DoorEnabled"])) == 1)
				{
					if ((uint)dataRow["f_CardNO"] != (uint)((long)dataRow2["f_CardNO"]))
					{
						if ((uint)dataRow["f_CardNO"] > 0u)
						{
							this.dtPrivilege.Rows.Add(dataRow);
							dataRow = this.dtPrivilege.NewRow();
							dataRow["f_ControlSegID1"] = 0;
							dataRow["f_ControlSegID2"] = 0;
							dataRow["f_ControlSegID3"] = 0;
							dataRow["f_ControlSegID4"] = 0;
							dataRow["f_MoreCards_GrpID_3"] = 0;
							dataRow["f_MoreCards_GrpID_4"] = 0;
							dataRow["f_MoreCards_GrpID_2"] = 0;
							dataRow["f_DoorFirstCard_2"] = 0;
							dataRow["f_DoorFirstCard_3"] = 0;
							dataRow["f_DoorFirstCard_4"] = 0;
							dataRow["f_PIN"] = 0;
						}
						dataRow["f_CardNO"] = (uint)((long)dataRow2["f_CardNO"]);
						dataRow["f_ConsumerID"] = (uint)((int)dataRow2["f_ConsumerID"]);
						dataRow["f_BeginYMD"] = dataRow2["f_BeginYMD"];
						dataRow["f_EndYMD"] = dataRow2["f_EndYMD"];
						dataRow["f_PIN"] = dataRow2["f_PIN"];
						this.dvUserFloorCnt.RowFilter = "f_ConsumerID = " + (uint)((int)dataRow2["f_ConsumerID"]);
						if (this.dvUserFloorCnt.Count >= 1 && (int)this.dvUserFloorCnt[0]["cnt"] >= 2)
						{
							dataRow["f_AllowFloors"] = ((ulong)dataRow["f_AllowFloors"] | 1099511627776uL);
						}
						dataRow["f_ControlSegID1"] = dataRow2["f_ControlSegID"];
					}
					int num2 = int.Parse(dataRow2["f_floorNO"].ToString());
					if (num2 > 0 && num2 <= 40)
					{
						dataRow["f_AllowFloors"] = ((ulong)dataRow["f_AllowFloors"] | 1uL << num2 - 1);
					}
					num++;
				}
			}
			if ((uint)dataRow["f_CardNO"] > 0u)
			{
				this.dtPrivilege.Rows.Add(dataRow);
			}
			this.dtPrivilege.AcceptChanges();
			this.m_PrivilegeTotal = num;
			this.m_ValidPrivilegeTotal = num;
			this.m_ConsumersTotal = this.dtPrivilege.Rows.Count;
			wgTools.WriteLine("getElevatorPrivilegeByID End");
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			return 1;
		}

		public int getElevatorPrivilegeByID_Acc(int ControllerID)
		{
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			wgTools.WriteLine("getPrivilegeByID Start");
			this.dtPrivilege.Rows.Clear();
			string cmdText = string.Format(" SELECT a.f_ConsumerID, COUNT(a.f_FloorID) as cnt FROM t_b_UserFloor a\r\nWHERE a.f_FloorID IN \r\n(SELECT b.f_floorid from t_b_floor b where b.[f_ControllerID]={0} or b.[f_ControllerID] in \r\n (select c.f_ControllerID from t_b_ElevatorGroup c where c.f_ElevatorGroupNO in \r\n   (select d.f_ElevatorGroupNO from t_b_ElevatorGroup d where d.f_ControllerID = {0})))\r\nGROUP BY a.f_ConsumerID ", ControllerID);
			this.dtUserFloorCnt = new DataTable();
			this.dvUserFloorCnt = new DataView(this.dtUserFloorCnt);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						oleDbDataAdapter.Fill(this.dtUserFloorCnt);
					}
				}
			}
			cmdText = string.Format("SELECT b.f_CardNO, b.f_ConsumerID, b.f_BeginYMD, b.f_EndYMD, b.f_PIN, a.f_ControlSegID, b.f_DoorEnabled, c.f_floorNO\r\nFROM (([t_b_UserFloor] a\r\nINNER JOIN t_b_Consumer b ON ( a.f_ConsumerID = b.f_ConsumerID AND b.f_CardNO IS NOT NULL ))\r\nINNER JOIN t_b_Floor c ON a.f_FloorID = c.f_FloorID )\r\nWHERE f_ControllerID = {0}\r\nORDER BY f_CardNO", ControllerID);
			this.dtUserFloor = new DataTable();
			using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand2 = new OleDbCommand(cmdText, oleDbConnection2))
				{
					using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(oleDbCommand2))
					{
						oleDbCommand2.CommandTimeout = wgAppConfig.dbCommandTimeout;
						oleDbDataAdapter2.Fill(this.dtUserFloor);
					}
				}
			}
			DataRow dataRow = this.dtPrivilege.NewRow();
			dataRow["f_CardNO"] = 0;
			dataRow["f_ControlSegID1"] = 0;
			dataRow["f_ControlSegID2"] = 0;
			dataRow["f_ControlSegID3"] = 0;
			dataRow["f_ControlSegID4"] = 0;
			dataRow["f_MoreCards_GrpID_3"] = 0;
			dataRow["f_MoreCards_GrpID_4"] = 0;
			dataRow["f_MoreCards_GrpID_2"] = 0;
			dataRow["f_DoorFirstCard_2"] = 0;
			dataRow["f_DoorFirstCard_3"] = 0;
			dataRow["f_DoorFirstCard_4"] = 0;
			dataRow["f_PIN"] = 0;
			int num = 0;
			for (int i = 0; i < this.dtUserFloor.Rows.Count; i++)
			{
				if (wgMjControllerPrivilege.bStopUploadPrivilege)
				{
					return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
				}
				DataRow dataRow2 = this.dtUserFloor.Rows[i];
				if (long.Parse(dataRow2["f_CardNO"].ToString()) >= 0L && long.Parse(dataRow2["f_CardNO"].ToString()) <= (long)((ulong)-2) && int.Parse(wgTools.SetObjToStr(dataRow2["f_DoorEnabled"])) == 1)
				{
					if (uint.Parse(dataRow["f_CardNO"].ToString()) != (uint)long.Parse(dataRow2["f_CardNO"].ToString()))
					{
						if (uint.Parse(dataRow["f_CardNO"].ToString()) > 0u)
						{
							this.dtPrivilege.Rows.Add(dataRow);
							dataRow = this.dtPrivilege.NewRow();
							dataRow["f_ControlSegID1"] = 0;
							dataRow["f_ControlSegID2"] = 0;
							dataRow["f_ControlSegID3"] = 0;
							dataRow["f_ControlSegID4"] = 0;
							dataRow["f_MoreCards_GrpID_3"] = 0;
							dataRow["f_MoreCards_GrpID_4"] = 0;
							dataRow["f_MoreCards_GrpID_2"] = 0;
							dataRow["f_DoorFirstCard_2"] = 0;
							dataRow["f_DoorFirstCard_3"] = 0;
							dataRow["f_DoorFirstCard_4"] = 0;
							dataRow["f_PIN"] = 0;
						}
						dataRow["f_CardNO"] = (uint)long.Parse(dataRow2["f_CardNO"].ToString());
						dataRow["f_ConsumerID"] = (uint)((int)dataRow2["f_ConsumerID"]);
						dataRow["f_BeginYMD"] = dataRow2["f_BeginYMD"];
						dataRow["f_EndYMD"] = dataRow2["f_EndYMD"];
						dataRow["f_PIN"] = dataRow2["f_PIN"];
						this.dvUserFloorCnt.RowFilter = "f_ConsumerID = " + (uint)((int)dataRow2["f_ConsumerID"]);
						if (this.dvUserFloorCnt.Count >= 1 && (int)this.dvUserFloorCnt[0]["cnt"] >= 2)
						{
							dataRow["f_AllowFloors"] = ((ulong)dataRow["f_AllowFloors"] | 1099511627776uL);
						}
						dataRow["f_ControlSegID1"] = dataRow2["f_ControlSegID"];
					}
					int num2 = int.Parse(dataRow2["f_floorNO"].ToString());
					if (num2 > 0 && num2 <= 40)
					{
						dataRow["f_AllowFloors"] = ((ulong)dataRow["f_AllowFloors"] | 1uL << num2 - 1);
					}
					num++;
				}
			}
			if (uint.Parse(dataRow["f_CardNO"].ToString()) > 0u)
			{
				this.dtPrivilege.Rows.Add(dataRow);
			}
			this.dtPrivilege.AcceptChanges();
			this.m_PrivilegeTotal = num;
			this.m_ValidPrivilegeTotal = num;
			this.m_ConsumersTotal = this.dtPrivilege.Rows.Count;
			wgTools.WriteLine("getElevatorPrivilegeByID End");
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			return 1;
		}

		public int getPrivilegeByID(int ControllerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getPrivilegeByID_Acc(ControllerID);
			}
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			if (wgMjController.IsElevator(this.getControllerSNByID(ControllerID)))
			{
				return this.getElevatorPrivilegeByID(ControllerID);
			}
			wgTools.WriteLine("getPrivilegeByID Start");
			this.dtPrivilege.Rows.Clear();
			string text = " SELECT t_b_Consumer.f_CardNO, t_b_Consumer.f_ConsumerID, t_b_Consumer.f_BeginYMD, t_b_Consumer.f_EndYMD, t_b_Consumer.f_PIN, t_d_Privilege.f_ControlSegID, t_d_Privilege.f_DoorNO, t_d_Privilege.f_ControllerID,t_b_Consumer.f_DoorEnabled ";
			text += " , t_b_Consumer.f_ConsumerName ";
			text += " FROM t_d_Privilege LEFT OUTER JOIN t_b_Consumer ON  t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID";
			text = text + " WHERE  f_ControllerID =  " + ControllerID.ToString();
			text += " ORDER BY f_CardNO ";
			DataRow dataRow = this.dtPrivilege.NewRow();
			dataRow["f_CardNO"] = 0;
			int num = 0;
			int num2 = 0;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (DataView dataView = new DataView(this.dtPrivilege))
					{
						sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							if (wgMjControllerPrivilege.bStopUploadPrivilege)
							{
								int eRR_PRIVILEGES_STOPUPLOAD = wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
								return eRR_PRIVILEGES_STOPUPLOAD;
							}
							num++;
							if (!(sqlDataReader["f_CardNO"] is DBNull) && (long)sqlDataReader["f_CardNO"] >= 0L && (long)sqlDataReader["f_CardNO"] <= (long)((ulong)-2) && !(sqlDataReader["f_DoorEnabled"] is DBNull) && int.Parse(wgTools.SetObjToStr(sqlDataReader["f_DoorEnabled"])) == 1)
							{
								num2++;
								if ((uint)dataRow["f_CardNO"] != (uint)((long)sqlDataReader["f_CardNO"]))
								{
									if ((uint)dataRow["f_CardNO"] > 0u)
									{
										this.dtPrivilege.Rows.Add(dataRow);
										dataRow = this.dtPrivilege.NewRow();
									}
									dataRow["f_CardNO"] = (uint)((long)sqlDataReader["f_CardNO"]);
									dataRow["f_ConsumerID"] = (uint)((int)sqlDataReader["f_ConsumerID"]);
									dataRow["f_BeginYMD"] = sqlDataReader["f_BeginYMD"];
									dataRow["f_EndYMD"] = sqlDataReader["f_EndYMD"];
									dataRow["f_PIN"] = sqlDataReader["f_PIN"];
									dataRow["f_ConsumerName"] = sqlDataReader["f_ConsumerName"];
								}
								switch (int.Parse(sqlDataReader["f_DoorNO"].ToString()))
								{
								case 1:
									dataRow["f_ControlSegID1"] = sqlDataReader["f_ControlSegID"];
									break;
								case 2:
									dataRow["f_ControlSegID2"] = sqlDataReader["f_ControlSegID"];
									break;
								case 3:
									dataRow["f_ControlSegID3"] = sqlDataReader["f_ControlSegID"];
									break;
								case 4:
									dataRow["f_ControlSegID4"] = sqlDataReader["f_ControlSegID"];
									break;
								}
							}
						}
						if ((uint)dataRow["f_CardNO"] > 0u)
						{
							this.dtPrivilege.Rows.Add(dataRow);
						}
						sqlDataReader.Close();
						this.dtPrivilege.AcceptChanges();
						this.m_PrivilegeTotal = num;
						this.m_ValidPrivilegeTotal = num;
						this.m_ConsumersTotal = this.dtPrivilege.Rows.Count;
						text = " SELECT a.f_ConsumerID, b.f_DoorNO from t_d_doorFirstCardUsers a inner join t_b_door b on a.f_doorid = b.f_doorid and f_ControllerID =  " + ControllerID.ToString();
						text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							if (wgMjControllerPrivilege.bStopUploadPrivilege)
							{
								int eRR_PRIVILEGES_STOPUPLOAD = wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
								return eRR_PRIVILEGES_STOPUPLOAD;
							}
							dataView.RowFilter = "f_ConsumerID = " + sqlDataReader["f_ConsumerID"].ToString();
							if (dataView.Count == 1)
							{
								switch ((byte)sqlDataReader["f_DoorNO"])
								{
								case 1:
									dataView[0]["f_DoorFirstCard_1"] = 1;
									break;
								case 2:
									dataView[0]["f_DoorFirstCard_2"] = 1;
									break;
								case 3:
									dataView[0]["f_DoorFirstCard_3"] = 1;
									break;
								case 4:
									dataView[0]["f_DoorFirstCard_4"] = 1;
									break;
								}
							}
						}
						sqlDataReader.Close();
						text = " SELECT a.f_ConsumerID,a.f_MoreCards_GrpID, b.f_DoorNO from t_d_doorMoreCardsUsers a inner join t_b_door b on a.f_doorid = b.f_doorid and f_ControllerID =  " + ControllerID.ToString();
						text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							if (wgMjControllerPrivilege.bStopUploadPrivilege)
							{
								int eRR_PRIVILEGES_STOPUPLOAD = wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
								return eRR_PRIVILEGES_STOPUPLOAD;
							}
							dataView.RowFilter = "f_ConsumerID = " + sqlDataReader["f_ConsumerID"].ToString();
							if (dataView.Count == 1)
							{
								switch ((byte)sqlDataReader["f_DoorNO"])
								{
								case 1:
									dataView[0]["f_MoreCards_GrpID_1"] = sqlDataReader["f_MoreCards_GrpID"];
									break;
								case 2:
									dataView[0]["f_MoreCards_GrpID_2"] = sqlDataReader["f_MoreCards_GrpID"];
									break;
								case 3:
									dataView[0]["f_MoreCards_GrpID_3"] = sqlDataReader["f_MoreCards_GrpID"];
									break;
								case 4:
									dataView[0]["f_MoreCards_GrpID_4"] = sqlDataReader["f_MoreCards_GrpID"];
									break;
								}
							}
						}
						sqlDataReader.Close();
						this.dtPrivilege.AcceptChanges();
					}
				}
			}
			wgTools.WriteLine("getPrivilegeByID End");
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			return 1;
		}

		public int getPrivilegeByID_Acc(int ControllerID)
		{
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			if (wgMjController.IsElevator(this.getControllerSNByID(ControllerID)))
			{
				return this.getElevatorPrivilegeByID(ControllerID);
			}
			wgTools.WriteLine("getPrivilegeByID Start");
			this.dtPrivilege.Rows.Clear();
			string text = " SELECT t_b_Consumer.f_CardNO, t_b_Consumer.f_ConsumerID, t_b_Consumer.f_BeginYMD, t_b_Consumer.f_EndYMD, t_b_Consumer.f_PIN, t_d_Privilege.f_ControlSegID, t_d_Privilege.f_DoorNO, t_d_Privilege.f_ControllerID,t_b_Consumer.f_DoorEnabled ";
			text += " , t_b_Consumer.f_ConsumerName ";
			text += " FROM t_d_Privilege LEFT OUTER JOIN t_b_Consumer ON  t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID";
			text = text + " WHERE  f_ControllerID =  " + ControllerID.ToString();
			text += " ORDER BY f_CardNO ";
			DataRow dataRow = this.dtPrivilege.NewRow();
			dataRow["f_CardNO"] = 0;
			int num = 0;
			int num2 = 0;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					using (DataView dataView = new DataView(this.dtPrivilege))
					{
						oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							if (wgMjControllerPrivilege.bStopUploadPrivilege)
							{
								int eRR_PRIVILEGES_STOPUPLOAD = wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
								return eRR_PRIVILEGES_STOPUPLOAD;
							}
							num++;
							if (!(oleDbDataReader["f_CardNO"] is DBNull) && long.Parse(oleDbDataReader["f_CardNO"].ToString()) >= 0L && long.Parse(oleDbDataReader["f_CardNO"].ToString()) <= (long)((ulong)-2) && !(oleDbDataReader["f_DoorEnabled"] is DBNull) && int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_DoorEnabled"])) == 1)
							{
								num2++;
								if (uint.Parse(dataRow["f_CardNO"].ToString()) != (uint)long.Parse(oleDbDataReader["f_CardNO"].ToString()))
								{
									if (uint.Parse(dataRow["f_CardNO"].ToString()) > 0u)
									{
										this.dtPrivilege.Rows.Add(dataRow);
										dataRow = this.dtPrivilege.NewRow();
									}
									dataRow["f_CardNO"] = (uint)long.Parse(oleDbDataReader["f_CardNO"].ToString());
									dataRow["f_ConsumerID"] = (uint)((int)oleDbDataReader["f_ConsumerID"]);
									dataRow["f_BeginYMD"] = oleDbDataReader["f_BeginYMD"];
									dataRow["f_EndYMD"] = oleDbDataReader["f_EndYMD"];
									dataRow["f_PIN"] = oleDbDataReader["f_PIN"];
									dataRow["f_ConsumerName"] = oleDbDataReader["f_ConsumerName"];
								}
								switch (int.Parse(oleDbDataReader["f_DoorNO"].ToString()))
								{
								case 1:
									dataRow["f_ControlSegID1"] = oleDbDataReader["f_ControlSegID"];
									break;
								case 2:
									dataRow["f_ControlSegID2"] = oleDbDataReader["f_ControlSegID"];
									break;
								case 3:
									dataRow["f_ControlSegID3"] = oleDbDataReader["f_ControlSegID"];
									break;
								case 4:
									dataRow["f_ControlSegID4"] = oleDbDataReader["f_ControlSegID"];
									break;
								}
							}
						}
						if (uint.Parse(dataRow["f_CardNO"].ToString()) > 0u)
						{
							this.dtPrivilege.Rows.Add(dataRow);
						}
						oleDbDataReader.Close();
						this.dtPrivilege.AcceptChanges();
						this.m_PrivilegeTotal = num;
						this.m_ValidPrivilegeTotal = num;
						this.m_ConsumersTotal = this.dtPrivilege.Rows.Count;
						text = string.Format(" SELECT a.f_ConsumerID, b.f_DoorNO from ( t_d_doorFirstCardUsers a inner join t_b_door b on ( a.f_doorid = b.f_doorid and f_ControllerID = {0} )) ", ControllerID.ToString());
						text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							if (wgMjControllerPrivilege.bStopUploadPrivilege)
							{
								int eRR_PRIVILEGES_STOPUPLOAD = wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
								return eRR_PRIVILEGES_STOPUPLOAD;
							}
							dataView.RowFilter = "f_ConsumerID = " + oleDbDataReader["f_ConsumerID"].ToString();
							if (dataView.Count == 1)
							{
								switch ((byte)oleDbDataReader["f_DoorNO"])
								{
								case 1:
									dataView[0]["f_DoorFirstCard_1"] = 1;
									break;
								case 2:
									dataView[0]["f_DoorFirstCard_2"] = 1;
									break;
								case 3:
									dataView[0]["f_DoorFirstCard_3"] = 1;
									break;
								case 4:
									dataView[0]["f_DoorFirstCard_4"] = 1;
									break;
								}
							}
						}
						oleDbDataReader.Close();
						text = string.Format(" SELECT a.f_ConsumerID,a.f_MoreCards_GrpID, b.f_DoorNO from ( t_d_doorMoreCardsUsers a inner join t_b_door b on ( a.f_doorid = b.f_doorid and f_ControllerID =  {0} ))", ControllerID.ToString());
						text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							if (wgMjControllerPrivilege.bStopUploadPrivilege)
							{
								int eRR_PRIVILEGES_STOPUPLOAD = wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
								return eRR_PRIVILEGES_STOPUPLOAD;
							}
							dataView.RowFilter = "f_ConsumerID = " + oleDbDataReader["f_ConsumerID"].ToString();
							if (dataView.Count == 1)
							{
								switch ((byte)oleDbDataReader["f_DoorNO"])
								{
								case 1:
									dataView[0]["f_MoreCards_GrpID_1"] = oleDbDataReader["f_MoreCards_GrpID"];
									break;
								case 2:
									dataView[0]["f_MoreCards_GrpID_2"] = oleDbDataReader["f_MoreCards_GrpID"];
									break;
								case 3:
									dataView[0]["f_MoreCards_GrpID_3"] = oleDbDataReader["f_MoreCards_GrpID"];
									break;
								case 4:
									dataView[0]["f_MoreCards_GrpID_4"] = oleDbDataReader["f_MoreCards_GrpID"];
									break;
								}
							}
						}
						oleDbDataReader.Close();
						this.dtPrivilege.AcceptChanges();
					}
				}
			}
			wgTools.WriteLine("getPrivilegeByID End");
			if (wgMjControllerPrivilege.bStopUploadPrivilege)
			{
				return wgGlobal.ERR_PRIVILEGES_STOPUPLOAD;
			}
			return 1;
		}

		public int DelPrivilegeOfOneCardByDB(int ControllerID, int ConsumerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.DelPrivilegeOfOneCardByDB_Acc(ControllerID, ConsumerID);
			}
			int result = -1;
			string text = " SELECT f_CardNO ";
			text = text + " FROM t_b_Consumer WHERE f_ConsumerID =  " + ConsumerID.ToString();
			MjRegisterCard mjRegisterCard = new MjRegisterCard();
			long num = 0L;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						long.TryParse(sqlDataReader["f_CardNO"].ToString(), out num);
					}
					sqlDataReader.Close();
				}
			}
			if (num <= 0L)
			{
				result = 1;
				return result;
			}
			if (num > (long)((ulong)-2))
			{
				result = 1;
				return result;
			}
			text = " SELECT t_b_Consumer.f_CardNO, t_b_Consumer.f_BeginYMD, t_b_Consumer.f_EndYMD, t_b_Consumer.f_PIN, t_d_Privilege.f_ControlSegID, t_d_Privilege.f_DoorNO, t_d_Privilege.f_ControllerID ";
			text += " , t_b_Consumer.f_ConsumerName ";
			text += " FROM t_b_Consumer ,t_d_Privilege ";
			text += " WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL ";
			text = text + " AND f_ControllerID =  " + ControllerID.ToString();
			text = text + " AND f_CardNO =  " + num.ToString();
			text += " AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID ";
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
				{
					sqlConnection2.Open();
					SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
					while (sqlDataReader2.Read())
					{
						mjRegisterCard.CardID = (uint)long.Parse(sqlDataReader2["f_CardNO"].ToString());
						mjRegisterCard.Password = uint.Parse(sqlDataReader2["f_PIN"].ToString());
						mjRegisterCard.ymdStart = (DateTime)sqlDataReader2["f_BeginYMD"];
						mjRegisterCard.ymdEnd = (DateTime)sqlDataReader2["f_EndYMD"];
						switch (int.Parse(sqlDataReader2["f_DoorNO"].ToString()))
						{
						case 1:
							mjRegisterCard.ControlSegIndexSet(1, (byte)((int)sqlDataReader2["f_ControlSegID"] & 255));
							break;
						case 2:
							mjRegisterCard.ControlSegIndexSet(2, (byte)((int)sqlDataReader2["f_ControlSegID"] & 255));
							break;
						case 3:
							mjRegisterCard.ControlSegIndexSet(3, (byte)((int)sqlDataReader2["f_ControlSegID"] & 255));
							break;
						case 4:
							mjRegisterCard.ControlSegIndexSet(4, (byte)((int)sqlDataReader2["f_ControlSegID"] & 255));
							break;
						}
					}
					sqlDataReader2.Close();
					text = " SELECT a.f_ConsumerID, b.f_DoorNO from t_d_doorFirstCardUsers a inner join t_b_door b on a.f_doorid = b.f_doorid and f_ControllerID =  " + ControllerID.ToString();
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					sqlCommand2.CommandText = text;
					sqlDataReader2 = sqlCommand2.ExecuteReader();
					while (sqlDataReader2.Read())
					{
						switch ((byte)sqlDataReader2["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.FirstCardSet(1, true);
							break;
						case 2:
							mjRegisterCard.FirstCardSet(2, true);
							break;
						case 3:
							mjRegisterCard.FirstCardSet(3, true);
							break;
						case 4:
							mjRegisterCard.FirstCardSet(4, true);
							break;
						}
					}
					sqlDataReader2.Close();
					text = " SELECT a.f_ConsumerID,a.f_MoreCards_GrpID, b.f_DoorNO from t_d_doorMoreCardsUsers a inner join t_b_door b on a.f_doorid = b.f_doorid and f_ControllerID =  " + ControllerID.ToString();
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					sqlCommand2.CommandText = text;
					sqlDataReader2 = sqlCommand2.ExecuteReader();
					while (sqlDataReader2.Read())
					{
						switch ((byte)sqlDataReader2["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.MoreCardGroupIndexSet(1, (byte)((int)sqlDataReader2["f_MoreCards_GrpID"]));
							break;
						case 2:
							mjRegisterCard.MoreCardGroupIndexSet(2, (byte)((int)sqlDataReader2["f_MoreCards_GrpID"]));
							break;
						case 3:
							mjRegisterCard.MoreCardGroupIndexSet(3, (byte)((int)sqlDataReader2["f_MoreCards_GrpID"]));
							break;
						case 4:
							mjRegisterCard.MoreCardGroupIndexSet(4, (byte)((int)sqlDataReader2["f_MoreCards_GrpID"]));
							break;
						}
					}
					sqlDataReader2.Close();
					text = " SELECT * ";
					text = text + " FROM t_b_Controller WHERE f_ControllerID =  " + ControllerID.ToString();
					sqlCommand2.CommandText = text;
					sqlDataReader2 = sqlCommand2.ExecuteReader();
					this.dtPrivilege.NewRow();
					if (sqlDataReader2.Read())
					{
						if (mjRegisterCard.CardID > 0u)
						{
							result = base.AddPrivilegeOfOneCardIP((int)sqlDataReader2["f_ControllerSN"], wgTools.SetObjToStr(sqlDataReader2["f_IP"]), (int)sqlDataReader2["f_PORT"], mjRegisterCard);
						}
						else
						{
							result = base.DelPrivilegeOfOneCardIP((int)sqlDataReader2["f_ControllerSN"], wgTools.SetObjToStr(sqlDataReader2["f_IP"]), (int)sqlDataReader2["f_PORT"], (uint)num);
						}
					}
					sqlDataReader2.Close();
				}
			}
			return result;
		}

		public int DelPrivilegeOfOneCardByDB_Acc(int ControllerID, int ConsumerID)
		{
			int result = -1;
			string text = " SELECT f_CardNO ";
			text = text + " FROM t_b_Consumer WHERE f_ConsumerID =  " + ConsumerID.ToString();
			MjRegisterCard mjRegisterCard = new MjRegisterCard();
			long num = 0L;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						long.TryParse(oleDbDataReader["f_CardNO"].ToString(), out num);
					}
					oleDbDataReader.Close();
				}
			}
			if (num <= 0L)
			{
				result = 1;
				return result;
			}
			if (num > 2)//if (num > (long)((ulong)-2))
            {
				result = 1;
				return result;
			}
			text = " SELECT t_b_Consumer.f_CardNO, t_b_Consumer.f_BeginYMD, t_b_Consumer.f_EndYMD, t_b_Consumer.f_PIN, t_d_Privilege.f_ControlSegID, t_d_Privilege.f_DoorNO, t_d_Privilege.f_ControllerID ";
			text += " , t_b_Consumer.f_ConsumerName ";
			text += " FROM t_b_Consumer ,t_d_Privilege ";
			text += " WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL ";
			text = text + " AND f_ControllerID =  " + ControllerID.ToString();
			text = text + " AND f_CardNO =  " + num.ToString();
			text += " AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID ";
			using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
				{
					oleDbConnection2.Open();
					OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader();
					while (oleDbDataReader2.Read())
					{
						mjRegisterCard.CardID = (uint)long.Parse(oleDbDataReader2["f_CardNO"].ToString());
						mjRegisterCard.Password = uint.Parse(oleDbDataReader2["f_PIN"].ToString());
						mjRegisterCard.ymdStart = (DateTime)oleDbDataReader2["f_BeginYMD"];
						mjRegisterCard.ymdEnd = (DateTime)oleDbDataReader2["f_EndYMD"];
						switch (int.Parse(oleDbDataReader2["f_DoorNO"].ToString()))
						{
						case 1:
							mjRegisterCard.ControlSegIndexSet(1, (byte)((int)oleDbDataReader2["f_ControlSegID"] & 255));
							break;
						case 2:
							mjRegisterCard.ControlSegIndexSet(2, (byte)((int)oleDbDataReader2["f_ControlSegID"] & 255));
							break;
						case 3:
							mjRegisterCard.ControlSegIndexSet(3, (byte)((int)oleDbDataReader2["f_ControlSegID"] & 255));
							break;
						case 4:
							mjRegisterCard.ControlSegIndexSet(4, (byte)((int)oleDbDataReader2["f_ControlSegID"] & 255));
							break;
						}
					}
					oleDbDataReader2.Close();
					text = string.Format(" SELECT a.f_ConsumerID, b.f_DoorNO from (t_d_doorFirstCardUsers a inner join t_b_door b on ( a.f_doorid = b.f_doorid and f_ControllerID = {0} )) ", ControllerID.ToString());
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					oleDbCommand2.CommandText = text;
					oleDbDataReader2 = oleDbCommand2.ExecuteReader();
					while (oleDbDataReader2.Read())
					{
						switch ((byte)oleDbDataReader2["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.FirstCardSet(1, true);
							break;
						case 2:
							mjRegisterCard.FirstCardSet(2, true);
							break;
						case 3:
							mjRegisterCard.FirstCardSet(3, true);
							break;
						case 4:
							mjRegisterCard.FirstCardSet(4, true);
							break;
						}
					}
					oleDbDataReader2.Close();
					text = string.Format(" SELECT a.f_ConsumerID,a.f_MoreCards_GrpID, b.f_DoorNO from ( t_d_doorMoreCardsUsers a inner join t_b_door b on ( a.f_doorid = b.f_doorid and f_ControllerID = {0})) ", ControllerID.ToString());
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					oleDbCommand2.CommandText = text;
					oleDbDataReader2 = oleDbCommand2.ExecuteReader();
					while (oleDbDataReader2.Read())
					{
						switch ((byte)oleDbDataReader2["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.MoreCardGroupIndexSet(1, (byte)((int)oleDbDataReader2["f_MoreCards_GrpID"]));
							break;
						case 2:
							mjRegisterCard.MoreCardGroupIndexSet(2, (byte)((int)oleDbDataReader2["f_MoreCards_GrpID"]));
							break;
						case 3:
							mjRegisterCard.MoreCardGroupIndexSet(3, (byte)((int)oleDbDataReader2["f_MoreCards_GrpID"]));
							break;
						case 4:
							mjRegisterCard.MoreCardGroupIndexSet(4, (byte)((int)oleDbDataReader2["f_MoreCards_GrpID"]));
							break;
						}
					}
					oleDbDataReader2.Close();
					text = " SELECT * ";
					text = text + " FROM t_b_Controller WHERE f_ControllerID =  " + ControllerID.ToString();
					oleDbCommand2.CommandText = text;
					oleDbDataReader2 = oleDbCommand2.ExecuteReader();
					this.dtPrivilege.NewRow();
					if (oleDbDataReader2.Read())
					{
						if (mjRegisterCard.CardID > 0u)
						{
							result = base.AddPrivilegeOfOneCardIP((int)oleDbDataReader2["f_ControllerSN"], wgTools.SetObjToStr(oleDbDataReader2["f_IP"]), (int)oleDbDataReader2["f_PORT"], mjRegisterCard);
						}
						else
						{
							result = base.DelPrivilegeOfOneCardIP((int)oleDbDataReader2["f_ControllerSN"], wgTools.SetObjToStr(oleDbDataReader2["f_IP"]), (int)oleDbDataReader2["f_PORT"], (uint)num);
						}
					}
					oleDbDataReader2.Close();
				}
			}
			return result;
		}

		public int AddPrivilegeOfOneCardByDB(int ControllerID, int ConsumerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.AddPrivilegeOfOneCardByDB_Acc(ControllerID, ConsumerID);
			}
			int num = -1;
			string text = " SELECT f_CardNO ";
			text = text + " FROM t_b_Consumer WHERE f_ConsumerID =  " + ConsumerID.ToString();
			MjRegisterCard mjRegisterCard = new MjRegisterCard();
			long num2 = 0L;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						long.TryParse(sqlDataReader["f_CardNO"].ToString(), out num2);
					}
					sqlDataReader.Close();
					if (num2 <= 0L)
					{
						num = 1;
						int result = num;
						return result;
					}
					if (num2 > (long)((ulong)-2))
					{
						num = 1;
						int result = num;
						return result;
					}
					text = " SELECT t_b_Consumer.f_CardNO, t_b_Consumer.f_BeginYMD, t_b_Consumer.f_EndYMD, t_b_Consumer.f_PIN, t_d_Privilege.f_ControlSegID, t_d_Privilege.f_DoorNO, t_d_Privilege.f_ControllerID ";
					text += " , t_b_Consumer.f_ConsumerName ";
					text += " FROM t_b_Consumer ,t_d_Privilege ";
					text += " WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL ";
					text = text + " AND f_ControllerID =  " + ControllerID.ToString();
					text = text + " AND f_CardNO =  " + num2.ToString();
					text += " AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID ";
					sqlCommand.CommandText = text;
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						mjRegisterCard.CardID = (uint)long.Parse(sqlDataReader["f_CardNO"].ToString());
						mjRegisterCard.Password = uint.Parse(sqlDataReader["f_PIN"].ToString());
						mjRegisterCard.ymdStart = (DateTime)sqlDataReader["f_BeginYMD"];
						mjRegisterCard.ymdEnd = (DateTime)sqlDataReader["f_EndYMD"];
						switch (int.Parse(sqlDataReader["f_DoorNO"].ToString()))
						{
						case 1:
							mjRegisterCard.ControlSegIndexSet(1, (byte)((int)sqlDataReader["f_ControlSegID"] & 255));
							break;
						case 2:
							mjRegisterCard.ControlSegIndexSet(2, (byte)((int)sqlDataReader["f_ControlSegID"] & 255));
							break;
						case 3:
							mjRegisterCard.ControlSegIndexSet(3, (byte)((int)sqlDataReader["f_ControlSegID"] & 255));
							break;
						case 4:
							mjRegisterCard.ControlSegIndexSet(4, (byte)((int)sqlDataReader["f_ControlSegID"] & 255));
							break;
						}
					}
					sqlDataReader.Close();
					text = string.Format(" SELECT a.f_ConsumerID, b.f_DoorNO from ( t_d_doorFirstCardUsers a inner join t_b_door b on (a.f_doorid = b.f_doorid and f_ControllerID = {0} )) ", ControllerID.ToString());
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					sqlCommand.CommandText = text;
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						switch ((byte)sqlDataReader["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.FirstCardSet(1, true);
							break;
						case 2:
							mjRegisterCard.FirstCardSet(2, true);
							break;
						case 3:
							mjRegisterCard.FirstCardSet(3, true);
							break;
						case 4:
							mjRegisterCard.FirstCardSet(4, true);
							break;
						}
					}
					sqlDataReader.Close();
					text = " SELECT a.f_ConsumerID,a.f_MoreCards_GrpID, b.f_DoorNO from t_d_doorMoreCardsUsers a inner join t_b_door b on a.f_doorid = b.f_doorid and f_ControllerID =  " + ControllerID.ToString();
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					sqlCommand.CommandText = text;
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						switch ((byte)sqlDataReader["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.MoreCardGroupIndexSet(1, (byte)((int)sqlDataReader["f_MoreCards_GrpID"]));
							break;
						case 2:
							mjRegisterCard.MoreCardGroupIndexSet(2, (byte)((int)sqlDataReader["f_MoreCards_GrpID"]));
							break;
						case 3:
							mjRegisterCard.MoreCardGroupIndexSet(3, (byte)((int)sqlDataReader["f_MoreCards_GrpID"]));
							break;
						case 4:
							mjRegisterCard.MoreCardGroupIndexSet(4, (byte)((int)sqlDataReader["f_MoreCards_GrpID"]));
							break;
						}
					}
					sqlDataReader.Close();
				}
			}
			if (mjRegisterCard.CardID > 0u)
			{
				num = this.AddPrivilegeOfOneCardByDB(ControllerID, mjRegisterCard);
			}
			return num;
		}

		public int AddPrivilegeOfOneCardByDB_Acc(int ControllerID, int ConsumerID)
		{
			int num = -1;
			string text = " SELECT f_CardNO ";
			text = text + " FROM t_b_Consumer WHERE f_ConsumerID =  " + ConsumerID.ToString();
			MjRegisterCard mjRegisterCard = new MjRegisterCard();
			long num2 = 0L;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						long.TryParse(oleDbDataReader["f_CardNO"].ToString(), out num2);
					}
					oleDbDataReader.Close();
					if (num2 <= 0L)
					{
						num = 1;
						int result = num;
						return result;
					}
					if (num2 > (long)((ulong)-2))
					{
						num = 1;
						int result = num;
						return result;
					}
					text = " SELECT t_b_Consumer.f_CardNO, t_b_Consumer.f_BeginYMD, t_b_Consumer.f_EndYMD, t_b_Consumer.f_PIN, t_d_Privilege.f_ControlSegID, t_d_Privilege.f_DoorNO, t_d_Privilege.f_ControllerID ";
					text += " , t_b_Consumer.f_ConsumerName ";
					text += " FROM t_b_Consumer ,t_d_Privilege ";
					text += " WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL ";
					text = text + " AND f_ControllerID =  " + ControllerID.ToString();
					text = text + " AND f_CardNO =  " + num2.ToString();
					text += " AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID ";
					oleDbCommand.CommandText = text;
					oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						mjRegisterCard.CardID = (uint)long.Parse(oleDbDataReader["f_CardNO"].ToString());
						mjRegisterCard.Password = uint.Parse(oleDbDataReader["f_PIN"].ToString());
						mjRegisterCard.ymdStart = (DateTime)oleDbDataReader["f_BeginYMD"];
						mjRegisterCard.ymdEnd = (DateTime)oleDbDataReader["f_EndYMD"];
						switch (int.Parse(oleDbDataReader["f_DoorNO"].ToString()))
						{
						case 1:
							mjRegisterCard.ControlSegIndexSet(1, (byte)((int)oleDbDataReader["f_ControlSegID"] & 255));
							break;
						case 2:
							mjRegisterCard.ControlSegIndexSet(2, (byte)((int)oleDbDataReader["f_ControlSegID"] & 255));
							break;
						case 3:
							mjRegisterCard.ControlSegIndexSet(3, (byte)((int)oleDbDataReader["f_ControlSegID"] & 255));
							break;
						case 4:
							mjRegisterCard.ControlSegIndexSet(4, (byte)((int)oleDbDataReader["f_ControlSegID"] & 255));
							break;
						}
					}
					oleDbDataReader.Close();
					text = string.Format(" SELECT a.f_ConsumerID, b.f_DoorNO from (t_d_doorFirstCardUsers a inner join t_b_door b on ( a.f_doorid = b.f_doorid and f_ControllerID =  {0} )) ", ControllerID.ToString());
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					oleDbCommand.CommandText = text;
					oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						switch ((byte)oleDbDataReader["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.FirstCardSet(1, true);
							break;
						case 2:
							mjRegisterCard.FirstCardSet(2, true);
							break;
						case 3:
							mjRegisterCard.FirstCardSet(3, true);
							break;
						case 4:
							mjRegisterCard.FirstCardSet(4, true);
							break;
						}
					}
					oleDbDataReader.Close();
					text = string.Format(" SELECT a.f_ConsumerID,a.f_MoreCards_GrpID, b.f_DoorNO from (t_d_doorMoreCardsUsers a inner join t_b_door b on (a.f_doorid = b.f_doorid and f_ControllerID = {0})) ", ControllerID.ToString());
					text = text + " WHERE a.f_ConsumerID =  " + ConsumerID.ToString();
					text += " ORDER BY f_ConsumerID ASC, f_DoorNO ASC ";
					oleDbCommand.CommandText = text;
					oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						switch ((byte)oleDbDataReader["f_DoorNO"])
						{
						case 1:
							mjRegisterCard.MoreCardGroupIndexSet(1, (byte)((int)oleDbDataReader["f_MoreCards_GrpID"]));
							break;
						case 2:
							mjRegisterCard.MoreCardGroupIndexSet(2, (byte)((int)oleDbDataReader["f_MoreCards_GrpID"]));
							break;
						case 3:
							mjRegisterCard.MoreCardGroupIndexSet(3, (byte)((int)oleDbDataReader["f_MoreCards_GrpID"]));
							break;
						case 4:
							mjRegisterCard.MoreCardGroupIndexSet(4, (byte)((int)oleDbDataReader["f_MoreCards_GrpID"]));
							break;
						}
					}
					oleDbDataReader.Close();
				}
			}
			if (mjRegisterCard.CardID > 0u)
			{
				num = this.AddPrivilegeOfOneCardByDB(ControllerID, mjRegisterCard);
			}
			return num;
		}

		public int AddPrivilegeOfOneCardByDB(int ControllerID, MjRegisterCard mjrc)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.AddPrivilegeOfOneCardByDB_Acc(ControllerID, mjrc);
			}
			int result = -1;
			string text = " SELECT * ";
			text = text + " FROM t_b_Controller WHERE f_ControllerID =  " + ControllerID.ToString();
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						result = base.AddPrivilegeOfOneCardIP((int)sqlDataReader["f_ControllerSN"], wgTools.SetObjToStr(sqlDataReader["f_IP"]), (int)sqlDataReader["f_PORT"], mjrc);
					}
					sqlDataReader.Close();
				}
			}
			return result;
		}

		public int AddPrivilegeOfOneCardByDB_Acc(int ControllerID, MjRegisterCard mjrc)
		{
			int result = -1;
			string text = " SELECT * ";
			text = text + " FROM t_b_Controller WHERE f_ControllerID =  " + ControllerID.ToString();
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						result = base.AddPrivilegeOfOneCardIP((int)oleDbDataReader["f_ControllerSN"], wgTools.SetObjToStr(oleDbDataReader["f_IP"]), (int)oleDbDataReader["f_PORT"], mjrc);
					}
					oleDbDataReader.Close();
				}
			}
			return result;
		}

		public static int getPrivilegeNumInDBByID(int ControllerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icPrivilege.getPrivilegeNumInDBByID_Acc(ControllerID);
			}
			int result = 0;
			string text = " SELECT COUNT(DISTINCT t_b_Consumer.f_ConsumerID) ";
			text += " FROM t_b_Consumer ,t_d_Privilege ";
			text += " WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL ";
			text = text + " AND f_ControllerID =  " + ControllerID.ToString();
			text += " AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						result = int.Parse(sqlDataReader[0].ToString());
					}
					sqlDataReader.Close();
				}
			}
			return result;
		}

		public static int getPrivilegeNumInDBByID_Acc(int ControllerID)
		{
			int result = 0;
			string text = " SELECT COUNT(DISTINCT t_b_Consumer.f_ConsumerID) ";
			text += " FROM t_b_Consumer ,t_d_Privilege ";
			text += " WHERE t_b_Consumer.f_DoorEnabled=1 AND f_CardNO IS NOT NULL ";
			text = text + " AND f_ControllerID =  " + ControllerID.ToString();
			text += " AND t_b_Consumer.f_ConsumerID = t_d_Privilege.f_ConsumerID ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						result = int.Parse(oleDbDataReader[0].ToString());
					}
					oleDbDataReader.Close();
				}
			}
			return result;
		}
	}
}
