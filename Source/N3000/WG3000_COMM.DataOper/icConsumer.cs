using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.DataOper
{
	internal class icConsumer
	{
		private const int ConsumerNOMinLen = 10;

		public string gYMDFormat = "yyyy-MM-dd";

		public int gConsumerID;

		public void wgDebugWrite(string info)
		{
		}

		public int addNew(string newConsumerNO, string ConsumerName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNew_Acc(newConsumerNO, ConsumerName);
			}
			int result = -9;
			if (newConsumerNO == null)
			{
				return -401;
			}
			if (newConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string obj = newConsumerNO.PadLeft(10, ' ');
			try
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
							text = " INSERT INTO t_b_Consumer (f_ConsumerNO, f_ConsumerName, f_BeginYMD ) values (";
							text += wgTools.PrepareStr(obj);
							text += ",";
							text += wgTools.PrepareStr(ConsumerName);
							text += ",";
							text += wgTools.PrepareStr("2012-01-01", true, this.gYMDFormat);
							text += ")";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "SELECT f_ConsumerID from [t_b_Consumer] where f_ConsumerNo =" + wgTools.PrepareStr(obj);
							sqlCommand.CommandText = text;
							int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
							this.gConsumerID = num;
							text = " INSERT INTO t_b_Consumer_Other (f_ConsumerID) values (";
							text += num;
							text += ")";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "COMMIT TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int addNew_Acc(string newConsumerNO, string ConsumerName)
		{
			int result = -9;
			if (newConsumerNO == null)
			{
				return -401;
			}
			if (newConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string obj = newConsumerNO.PadLeft(10, ' ');
			try
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
							text = " INSERT INTO t_b_Consumer (f_ConsumerNO, f_ConsumerName, f_BeginYMD ) values (";
							text += wgTools.PrepareStr(obj);
							text += ",";
							text += wgTools.PrepareStr(ConsumerName);
							text += ",";
							text += wgTools.PrepareStr("2012-01-01", true, this.gYMDFormat);
							text += ")";
							oleDbCommand.CommandText = text;
							text = "SELECT f_ConsumerID from [t_b_Consumer] where f_ConsumerNo =" + wgTools.PrepareStr(obj);
							oleDbCommand.CommandText = text;
							int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
							this.gConsumerID = num;
							text = " INSERT INTO t_b_Consumer_Other (f_ConsumerID) values (";
							text += num;
							text += ")";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = "COMMIT TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int addNew(string newConsumerNO, string ConsumerName, long CardNO, int deptID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNew_Acc(newConsumerNO, ConsumerName, CardNO, deptID);
			}
			int result = -9;
			if (newConsumerNO == null)
			{
				return -401;
			}
			if (newConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string obj = newConsumerNO.PadLeft(10, ' ');
			try
			{
				string text;
				if (CardNO > 0L)
				{
					text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
						{
							sqlConnection.Open();
							int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
							if (num > 0)
							{
								result = -103;
								return result;
							}
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
							sqlCommand.CommandText = text;
							num = sqlCommand.ExecuteNonQuery();
						}
					}
				}
				text = "BEGIN TRANSACTION";
				using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
					{
						sqlConnection2.Open();
						sqlCommand2.ExecuteNonQuery();
						try
						{
							text = " INSERT INTO t_b_Consumer (f_ConsumerNO, f_ConsumerName, f_GroupID, f_CardNO, f_BeginYMD) values (";
							text += wgTools.PrepareStr(obj);
							text += ",";
							text += wgTools.PrepareStr(ConsumerName);
							text += ",";
							text += wgTools.PrepareStr(deptID);
							text += ",";
							text += ((CardNO > 0L) ? CardNO.ToString() : "NULL");
							text += ",";
							text += wgTools.PrepareStr("2012-01-01", true, this.gYMDFormat);
							text += ")";
							sqlCommand2.CommandText = text;
							int num = sqlCommand2.ExecuteNonQuery();
							text = "SELECT f_ConsumerID from [t_b_Consumer] where f_ConsumerNo =" + wgTools.PrepareStr(obj);
							sqlCommand2.CommandText = text;
							int num2 = int.Parse("0" + wgTools.SetObjToStr(sqlCommand2.ExecuteScalar()));
							this.gConsumerID = num2;
							text = " INSERT INTO t_b_Consumer_Other (f_ConsumerID) values (";
							text += num2;
							text += ")";
							sqlCommand2.CommandText = text;
							num = sqlCommand2.ExecuteNonQuery();
							int num3 = 0;
							if (CardNO > 0L)
							{
								text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
								sqlCommand2.CommandText = text;
								SqlDataReader sqlDataReader = sqlCommand2.ExecuteReader();
								while (sqlDataReader.Read())
								{
									num3++;
									if (num3 > 1)
									{
										break;
									}
								}
								sqlDataReader.Close();
								if (num3 <= 1)
								{
									text = "SELECT f_ConsumerID FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
									sqlCommand2.CommandText = text;
									sqlDataReader = sqlCommand2.ExecuteReader();
									while (sqlDataReader.Read())
									{
										num3++;
										if (num3 > 1)
										{
											break;
										}
									}
									sqlDataReader.Close();
								}
							}
							if (num3 > 1)
							{
								text = "ROLLBACK TRANSACTION";
								sqlCommand2.CommandText = text;
								sqlCommand2.ExecuteNonQuery();
							}
							else
							{
								text = "COMMIT TRANSACTION";
								sqlCommand2.CommandText = text;
								sqlCommand2.ExecuteNonQuery();
								result = 1;
							}
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							sqlCommand2.CommandText = text;
							sqlCommand2.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int addNew_Acc(string newConsumerNO, string ConsumerName, long CardNO, int deptID)
		{
			int result = -9;
			if (newConsumerNO == null)
			{
				return -401;
			}
			if (newConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string obj = newConsumerNO.PadLeft(10, ' ');
			try
			{
				string text;
				if (CardNO > 0L)
				{
					text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
						{
							oleDbConnection.Open();
							int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
							if (num > 0)
							{
								result = -103;
								return result;
							}
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
							oleDbCommand.CommandText = text;
							num = oleDbCommand.ExecuteNonQuery();
						}
					}
				}
				text = "BEGIN TRANSACTION";
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
					{
						oleDbConnection2.Open();
						oleDbCommand2.ExecuteNonQuery();
						try
						{
							text = " INSERT INTO t_b_Consumer (f_ConsumerNO, f_ConsumerName, f_GroupID, f_CardNO, f_BeginYMD) values (";
							text += wgTools.PrepareStr(obj);
							text += ",";
							text += wgTools.PrepareStr(ConsumerName);
							text += ",";
							text += wgTools.PrepareStr(deptID);
							text += ",";
							text += ((CardNO > 0L) ? CardNO.ToString() : "NULL");
							text += ",";
							text += wgTools.PrepareStr("2012-01-01", true, this.gYMDFormat);
							text += ")";
							oleDbCommand2.CommandText = text;
							int num = oleDbCommand2.ExecuteNonQuery();
							text = "SELECT f_ConsumerID from [t_b_Consumer] where f_ConsumerNo =" + wgTools.PrepareStr(obj);
							oleDbCommand2.CommandText = text;
							int num2 = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand2.ExecuteScalar()));
							this.gConsumerID = num2;
							text = " INSERT INTO t_b_Consumer_Other (f_ConsumerID) values (";
							text += num2;
							text += ")";
							oleDbCommand2.CommandText = text;
							num = oleDbCommand2.ExecuteNonQuery();
							int num3 = 0;
							if (CardNO > 0L)
							{
								text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
								oleDbCommand2.CommandText = text;
								OleDbDataReader oleDbDataReader = oleDbCommand2.ExecuteReader();
								while (oleDbDataReader.Read())
								{
									num3++;
									if (num3 > 1)
									{
										break;
									}
								}
								oleDbDataReader.Close();
								if (num3 <= 1)
								{
									text = "SELECT f_ConsumerID FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
									oleDbCommand2.CommandText = text;
									oleDbDataReader = oleDbCommand2.ExecuteReader();
									while (oleDbDataReader.Read())
									{
										num3++;
										if (num3 > 1)
										{
											break;
										}
									}
									oleDbDataReader.Close();
								}
							}
							if (num3 > 1)
							{
								text = "ROLLBACK TRANSACTION";
								oleDbCommand2.CommandText = text;
								oleDbCommand2.ExecuteNonQuery();
							}
							else
							{
								text = "COMMIT TRANSACTION";
								oleDbCommand2.CommandText = text;
								oleDbCommand2.ExecuteNonQuery();
								result = 1;
							}
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							oleDbCommand2.CommandText = text;
							oleDbCommand2.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int addNew(string newConsumerNO, string ConsumerName, int GroupID, byte AttendEnabled, byte ShiftEnabled, byte DoorEnabled, DateTime BeginYMD, DateTime EndYMD, int PIN, long CardNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNew_Acc(newConsumerNO, ConsumerName, GroupID, AttendEnabled, ShiftEnabled, DoorEnabled, BeginYMD, EndYMD, PIN, CardNO);
			}
			int result = -9;
			if (newConsumerNO == null)
			{
				return -401;
			}
			if (newConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string text = "";
			string obj = newConsumerNO.PadLeft(10, ' ');
			try
			{
				byte b = ShiftEnabled;
				if (AttendEnabled == 0)
				{
					b = 0;
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						if (CardNO > 0L)
						{
							text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
							sqlCommand.CommandText = text;
							int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
							if (num > 0)
							{
								result = -103;
								return result;
							}
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
							sqlCommand.CommandText = text;
							num = sqlCommand.ExecuteNonQuery();
						}
						text = "BEGIN TRANSACTION";
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
						wgTools.WriteLine("BEGIN TRANSACTION End: ");
						try
						{
							text = " INSERT INTO t_b_Consumer (f_ConsumerNO, f_ConsumerName, f_GroupID, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled,f_BeginYMD,f_EndYMD,f_PIN, f_CardNO) values (";
							text += wgTools.PrepareStr(obj);
							text = text + "," + wgTools.PrepareStr(ConsumerName);
							text = text + "," + GroupID.ToString();
							text = text + "," + AttendEnabled.ToString();
							text = text + "," + b.ToString();
							text = text + "," + DoorEnabled.ToString();
							text = text + "," + wgTools.PrepareStr(BeginYMD, true, this.gYMDFormat);
							text = text + "," + wgTools.PrepareStr(EndYMD, true, this.gYMDFormat);
							text = text + "," + PIN.ToString();
							text = text + "," + ((CardNO > 0L) ? CardNO.ToString() : "NULL");
							text += ")";
							sqlCommand.CommandText = text;
							int num = sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine("INSERT INTO t_b_Consumer End: ");
							text = "SELECT f_ConsumerID from [t_b_Consumer] where f_ConsumerNo =" + wgTools.PrepareStr(obj);
							sqlCommand.CommandText = text;
							int num2 = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
							wgTools.WriteLine("SELECT f_ConsumerID End: ");
							this.gConsumerID = num2;
							text = " INSERT INTO t_b_Consumer_Other (f_ConsumerID) values (";
							text += num2;
							text += ")";
							sqlCommand.CommandText = text;
							num = sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine("INSERT INTO t_b_Consumer_Other End: ");
							int num3 = 0;
							if (CardNO > 0L)
							{
								text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
								sqlCommand.CommandText = text;
								SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
								while (sqlDataReader.Read())
								{
									num3++;
									if (num3 > 1)
									{
										break;
									}
								}
								sqlDataReader.Close();
								if (num3 <= 1)
								{
									text = "SELECT f_ConsumerID FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
									sqlCommand.CommandText = text;
									sqlDataReader = sqlCommand.ExecuteReader();
									while (sqlDataReader.Read())
									{
										num3++;
										if (num3 > 1)
										{
											break;
										}
									}
									sqlDataReader.Close();
								}
							}
							if (num3 > 1)
							{
								text = "ROLLBACK TRANSACTION";
								sqlCommand.CommandText = text;
								sqlCommand.ExecuteNonQuery();
							}
							else
							{
								text = "COMMIT TRANSACTION";
								sqlCommand.CommandText = text;
								sqlCommand.ExecuteNonQuery();
								result = 1;
							}
							wgTools.WriteLine("COMMIT TRANSACTION End: ");
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine("ROLLBACK TRANSACTION End: ");
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int addNew_Acc(string newConsumerNO, string ConsumerName, int GroupID, byte AttendEnabled, byte ShiftEnabled, byte DoorEnabled, DateTime BeginYMD, DateTime EndYMD, int PIN, long CardNO)
		{
			int result = -9;
			if (newConsumerNO == null)
			{
				return -401;
			}
			if (newConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string text = "";
			string obj = newConsumerNO.PadLeft(10, ' ');
			try
			{
				byte b = ShiftEnabled;
				if (AttendEnabled == 0)
				{
					b = 0;
				}
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						if (CardNO > 0L)
						{
							text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
							oleDbCommand.CommandText = text;
							int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
							if (num > 0)
							{
								result = -103;
								return result;
							}
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
							oleDbCommand.CommandText = text;
							num = oleDbCommand.ExecuteNonQuery();
						}
						text = "BEGIN TRANSACTION";
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
						wgTools.WriteLine("BEGIN TRANSACTION End: ");
						try
						{
							text = " INSERT INTO t_b_Consumer (f_ConsumerNO, f_ConsumerName, f_GroupID, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled,f_BeginYMD,f_EndYMD,f_PIN, f_CardNO) values (";
							text += wgTools.PrepareStr(obj);
							text = text + "," + wgTools.PrepareStr(ConsumerName);
							text = text + "," + GroupID.ToString();
							text = text + "," + AttendEnabled.ToString();
							text = text + "," + b.ToString();
							text = text + "," + DoorEnabled.ToString();
							text = text + "," + wgTools.PrepareStr(BeginYMD, true, this.gYMDFormat);
							text = text + "," + wgTools.PrepareStr(EndYMD, true, this.gYMDFormat);
							text = text + "," + PIN.ToString();
							text = text + "," + ((CardNO > 0L) ? CardNO.ToString() : "NULL");
							text += ")";
							oleDbCommand.CommandText = text;
							int num = oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine("INSERT INTO t_b_Consumer End: ");
							text = "SELECT f_ConsumerID from [t_b_Consumer] where f_ConsumerNo =" + wgTools.PrepareStr(obj);
							oleDbCommand.CommandText = text;
							int num2 = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
							wgTools.WriteLine("SELECT f_ConsumerID End: ");
							this.gConsumerID = num2;
							text = " INSERT INTO t_b_Consumer_Other (f_ConsumerID) values (";
							text += num2;
							text += ")";
							oleDbCommand.CommandText = text;
							num = oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine("INSERT INTO t_b_Consumer_Other End: ");
							int num3 = 0;
							if (CardNO > 0L)
							{
								text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
								oleDbCommand.CommandText = text;
								OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
								while (oleDbDataReader.Read())
								{
									num3++;
									if (num3 > 1)
									{
										break;
									}
								}
								oleDbDataReader.Close();
								if (num3 <= 1)
								{
									text = "SELECT f_ConsumerID FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
									oleDbCommand.CommandText = text;
									oleDbDataReader = oleDbCommand.ExecuteReader();
									while (oleDbDataReader.Read())
									{
										num3++;
										if (num3 > 1)
										{
											break;
										}
									}
									oleDbDataReader.Close();
								}
							}
							if (num3 > 1)
							{
								text = "ROLLBACK TRANSACTION";
								oleDbCommand.CommandText = text;
								oleDbCommand.ExecuteNonQuery();
							}
							else
							{
								text = "COMMIT TRANSACTION";
								oleDbCommand.CommandText = text;
								oleDbCommand.ExecuteNonQuery();
								result = 1;
							}
							wgTools.WriteLine("COMMIT TRANSACTION End: ");
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine("ROLLBACK TRANSACTION End: ");
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int editUser(int ConsumerID, string ConsumerNO, string ConsumerName, int GroupID, byte AttendEnabled, byte ShiftEnabled, byte DoorEnabled, DateTime BeginYMD, DateTime EndYMD, int PIN, long CardNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.editUser_Acc(ConsumerID, ConsumerNO, ConsumerName, GroupID, AttendEnabled, ShiftEnabled, DoorEnabled, BeginYMD, EndYMD, PIN, CardNO);
			}
			int result = -9;
			if (ConsumerNO == null)
			{
				return -401;
			}
			if (ConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string text = "";
			string obj = ConsumerNO.PadLeft(10, ' ');
			try
			{
				byte b = ShiftEnabled;
				if (AttendEnabled == 0)
				{
					b = 0;
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						if (CardNO > 0L)
						{
							text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
							sqlCommand.CommandText = text;
							int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
							if (num > 0)
							{
								if (ConsumerID != num)
								{
									result = -103;
									return result;
								}
							}
							else
							{
								text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
								sqlCommand.CommandText = text;
								num = sqlCommand.ExecuteNonQuery();
							}
						}
						text = "BEGIN TRANSACTION";
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
						try
						{
							text = " UPDATE t_b_Consumer  SET  ";
							text = text + " f_ConsumerNO =" + wgTools.PrepareStr(obj);
							text = text + ",f_ConsumerName =" + wgTools.PrepareStr(ConsumerName);
							text = text + ",f_GroupID = " + GroupID.ToString();
							text = text + ",f_AttendEnabled = " + AttendEnabled.ToString();
							text = text + ",f_ShiftEnabled = " + b.ToString();
							text = text + ",f_DoorEnabled= " + DoorEnabled.ToString();
							text = text + ",f_BeginYMD=" + wgTools.PrepareStr(BeginYMD, true, this.gYMDFormat);
							text = text + ",f_EndYMD=" + wgTools.PrepareStr(EndYMD, true, this.gYMDFormat);
							text = text + ",f_PIN=" + PIN.ToString();
							text = text + ",f_CardNO = " + ((CardNO > 0L) ? CardNO.ToString() : "NULL");
							text = text + " WHERE f_ConsumerID =" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							int num = sqlCommand.ExecuteNonQuery();
							int num2 = 0;
							if (CardNO > 0L)
							{
								text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
								sqlCommand.CommandText = text;
								SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
								while (sqlDataReader.Read())
								{
									num2++;
									if (num2 > 1)
									{
										break;
									}
								}
								sqlDataReader.Close();
								if (num2 <= 1)
								{
									text = "SELECT f_ConsumerID FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
									sqlCommand.CommandText = text;
									sqlDataReader = sqlCommand.ExecuteReader();
									while (sqlDataReader.Read())
									{
										num2++;
										if (num2 > 1)
										{
											break;
										}
									}
									sqlDataReader.Close();
								}
							}
							if (num2 > 1)
							{
								text = "ROLLBACK TRANSACTION";
								sqlCommand.CommandText = text;
								sqlCommand.ExecuteNonQuery();
							}
							else
							{
								text = "COMMIT TRANSACTION";
								sqlCommand.CommandText = text;
								sqlCommand.ExecuteNonQuery();
								result = 1;
							}
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int editUser_Acc(int ConsumerID, string ConsumerNO, string ConsumerName, int GroupID, byte AttendEnabled, byte ShiftEnabled, byte DoorEnabled, DateTime BeginYMD, DateTime EndYMD, int PIN, long CardNO)
		{
			int result = -9;
			if (ConsumerNO == null)
			{
				return -401;
			}
			if (ConsumerNO == "")
			{
				return -401;
			}
			if (ConsumerName == null)
			{
				return -201;
			}
			if (ConsumerName == "")
			{
				return -201;
			}
			string text = "";
			string obj = ConsumerNO.PadLeft(10, ' ');
			try
			{
				byte b = ShiftEnabled;
				if (AttendEnabled == 0)
				{
					b = 0;
				}
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						if (CardNO > 0L)
						{
							text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
							oleDbCommand.CommandText = text;
							int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
							if (num > 0)
							{
								if (ConsumerID != num)
								{
									result = -103;
									return result;
								}
							}
							else
							{
								text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
								oleDbCommand.CommandText = text;
								num = oleDbCommand.ExecuteNonQuery();
							}
						}
						text = "BEGIN TRANSACTION";
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
						try
						{
							text = " UPDATE t_b_Consumer  SET  ";
							text = text + " f_ConsumerNO =" + wgTools.PrepareStr(obj);
							text = text + ",f_ConsumerName =" + wgTools.PrepareStr(ConsumerName);
							text = text + ",f_GroupID = " + GroupID.ToString();
							text = text + ",f_AttendEnabled = " + AttendEnabled.ToString();
							text = text + ",f_ShiftEnabled = " + b.ToString();
							text = text + ",f_DoorEnabled= " + DoorEnabled.ToString();
							text = text + ",f_BeginYMD=" + wgTools.PrepareStr(BeginYMD, true, this.gYMDFormat);
							text = text + ",f_EndYMD=" + wgTools.PrepareStr(EndYMD, true, this.gYMDFormat);
							text = text + ",f_PIN=" + PIN.ToString();
							text = text + ",f_CardNO = " + ((CardNO > 0L) ? CardNO.ToString() : "NULL");
							text = text + " WHERE f_ConsumerID =" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							int num = oleDbCommand.ExecuteNonQuery();
							int num2 = 0;
							if (CardNO > 0L)
							{
								text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
								oleDbCommand.CommandText = text;
								OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
								while (oleDbDataReader.Read())
								{
									num2++;
									if (num2 > 1)
									{
										break;
									}
								}
								oleDbDataReader.Close();
								if (num2 <= 1)
								{
									text = "SELECT f_ConsumerID FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
									oleDbCommand.CommandText = text;
									oleDbDataReader = oleDbCommand.ExecuteReader();
									while (oleDbDataReader.Read())
									{
										num2++;
										if (num2 > 1)
										{
											break;
										}
									}
									oleDbDataReader.Close();
								}
							}
							if (num2 > 1)
							{
								text = "ROLLBACK TRANSACTION";
								oleDbCommand.CommandText = text;
								oleDbCommand.ExecuteNonQuery();
							}
							else
							{
								text = "COMMIT TRANSACTION";
								oleDbCommand.CommandText = text;
								oleDbCommand.ExecuteNonQuery();
								result = 1;
							}
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int editUserOtherInfo(int ConsumerID, string txtf_Title, string txtf_Culture, string txtf_Hometown, string txtf_Birthday, string txtf_Marriage, string txtf_JoinDate, string txtf_LeaveDate, string txtf_CertificateType, string txtf_CertificateID, string txtf_SocialInsuranceNo, string txtf_Addr, string txtf_Postcode, string txtf_Sex, string txtf_Nationality, string txtf_Religion, string txtf_EnglishName, string txtf_Mobile, string txtf_HomePhone, string txtf_Telephone, string txtf_Email, string txtf_Political, string txtf_CorporationName, string txtf_TechGrade, string txtf_Note)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.editUserOtherInfo_Acc(ConsumerID, txtf_Title, txtf_Culture, txtf_Hometown, txtf_Birthday, txtf_Marriage, txtf_JoinDate, txtf_LeaveDate, txtf_CertificateType, txtf_CertificateID, txtf_SocialInsuranceNo, txtf_Addr, txtf_Postcode, txtf_Sex, txtf_Nationality, txtf_Religion, txtf_EnglishName, txtf_Mobile, txtf_HomePhone, txtf_Telephone, txtf_Email, txtf_Political, txtf_CorporationName, txtf_TechGrade, txtf_Note);
			}
			int result = -9;
			try
			{
				string text = " UPDATE t_b_Consumer_Other  SET  ";
				text = text + "   f_Title                 = " + wgTools.PrepareStr(txtf_Title);
				text = text + "  , f_Culture               = " + wgTools.PrepareStr(txtf_Culture);
				text = text + "  , f_Hometown              = " + wgTools.PrepareStr(txtf_Hometown);
				text = text + "  , f_Birthday              = " + wgTools.PrepareStr(txtf_Birthday);
				text = text + "  , f_Marriage              = " + wgTools.PrepareStr(txtf_Marriage);
				text = text + "  , f_JoinDate              = " + wgTools.PrepareStr(txtf_JoinDate);
				text = text + "  , f_LeaveDate             = " + wgTools.PrepareStr(txtf_LeaveDate);
				text = text + "  , f_CertificateType       = " + wgTools.PrepareStr(txtf_CertificateType);
				text = text + "  , f_CertificateID         = " + wgTools.PrepareStr(txtf_CertificateID);
				text = text + "  , f_SocialInsuranceNo     = " + wgTools.PrepareStr(txtf_SocialInsuranceNo);
				text = text + "  , f_Addr                  = " + wgTools.PrepareStr(txtf_Addr);
				text = text + "  , f_Postcode              = " + wgTools.PrepareStr(txtf_Postcode);
				text = text + "  , f_Sex                   = " + wgTools.PrepareStr(txtf_Sex);
				text = text + "  , f_Nationality           = " + wgTools.PrepareStr(txtf_Nationality);
				text = text + "  , f_Religion              = " + wgTools.PrepareStr(txtf_Religion);
				text = text + "  , f_EnglishName           = " + wgTools.PrepareStr(txtf_EnglishName);
				text = text + "  , f_Mobile                = " + wgTools.PrepareStr(txtf_Mobile);
				text = text + "  , f_HomePhone             = " + wgTools.PrepareStr(txtf_HomePhone);
				text = text + "  , f_Telephone             = " + wgTools.PrepareStr(txtf_Telephone);
				text = text + "  , f_Email                 = " + wgTools.PrepareStr(txtf_Email);
				text = text + "  , f_Political             = " + wgTools.PrepareStr(txtf_Political);
				text = text + "  , f_CorporationName       = " + wgTools.PrepareStr(txtf_CorporationName);
				text = text + "  , f_TechGrade             = " + wgTools.PrepareStr(txtf_TechGrade);
				text = text + "  , f_Note                  = " + wgTools.PrepareStr(txtf_Note);
				text = text + " WHERE f_ConsumerID =" + ConsumerID.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int editUserOtherInfo_Acc(int ConsumerID, string txtf_Title, string txtf_Culture, string txtf_Hometown, string txtf_Birthday, string txtf_Marriage, string txtf_JoinDate, string txtf_LeaveDate, string txtf_CertificateType, string txtf_CertificateID, string txtf_SocialInsuranceNo, string txtf_Addr, string txtf_Postcode, string txtf_Sex, string txtf_Nationality, string txtf_Religion, string txtf_EnglishName, string txtf_Mobile, string txtf_HomePhone, string txtf_Telephone, string txtf_Email, string txtf_Political, string txtf_CorporationName, string txtf_TechGrade, string txtf_Note)
		{
			int result = -9;
			try
			{
				string text = " UPDATE t_b_Consumer_Other  SET  ";
				text = text + "   f_Title                 = " + wgTools.PrepareStr(txtf_Title);
				text = text + "  , f_Culture               = " + wgTools.PrepareStr(txtf_Culture);
				text = text + "  , f_Hometown              = " + wgTools.PrepareStr(txtf_Hometown);
				text = text + "  , f_Birthday              = " + wgTools.PrepareStr(txtf_Birthday);
				text = text + "  , f_Marriage              = " + wgTools.PrepareStr(txtf_Marriage);
				text = text + "  , f_JoinDate              = " + wgTools.PrepareStr(txtf_JoinDate);
				text = text + "  , f_LeaveDate             = " + wgTools.PrepareStr(txtf_LeaveDate);
				text = text + "  , f_CertificateType       = " + wgTools.PrepareStr(txtf_CertificateType);
				text = text + "  , f_CertificateID         = " + wgTools.PrepareStr(txtf_CertificateID);
				text = text + "  , f_SocialInsuranceNo     = " + wgTools.PrepareStr(txtf_SocialInsuranceNo);
				text = text + "  , f_Addr                  = " + wgTools.PrepareStr(txtf_Addr);
				text = text + "  , f_Postcode              = " + wgTools.PrepareStr(txtf_Postcode);
				text = text + "  , f_Sex                   = " + wgTools.PrepareStr(txtf_Sex);
				text = text + "  , f_Nationality           = " + wgTools.PrepareStr(txtf_Nationality);
				text = text + "  , f_Religion              = " + wgTools.PrepareStr(txtf_Religion);
				text = text + "  , f_EnglishName           = " + wgTools.PrepareStr(txtf_EnglishName);
				text = text + "  , f_Mobile                = " + wgTools.PrepareStr(txtf_Mobile);
				text = text + "  , f_HomePhone             = " + wgTools.PrepareStr(txtf_HomePhone);
				text = text + "  , f_Telephone             = " + wgTools.PrepareStr(txtf_Telephone);
				text = text + "  , f_Email                 = " + wgTools.PrepareStr(txtf_Email);
				text = text + "  , f_Political             = " + wgTools.PrepareStr(txtf_Political);
				text = text + "  , f_CorporationName       = " + wgTools.PrepareStr(txtf_CorporationName);
				text = text + "  , f_TechGrade             = " + wgTools.PrepareStr(txtf_TechGrade);
				text = text + "  , f_Note                  = " + wgTools.PrepareStr(txtf_Note);
				text = text + " WHERE f_ConsumerID =" + ConsumerID.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.ExecuteNonQuery();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int deleteUser(int ConsumerID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.deleteUser_Acc(ConsumerID);
			}
			int result = -9;
			try
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
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_b_UserFloor ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_ShiftData ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_Leave ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_ManualCardRecord ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_d_Privilege] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_b_IDCard_Lost] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_b_Consumer_Other] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_b_Consumer] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = "COMMIT TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							result = 1;
						}
						catch (Exception ex)
						{
							this.wgDebugWrite(ex.ToString());
							this.wgDebugWrite(text);
							text = "ROLLBACK TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				this.wgDebugWrite(ex2.ToString());
			}
			return result;
		}

		public int deleteUser_Acc(int ConsumerID)
		{
			int result = -9;
			try
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
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_b_UserFloor ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_ShiftData ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_Leave ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_ManualCardRecord ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_d_Privilege] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_b_IDCard_Lost] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_b_Consumer_Other] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE FROM [t_b_Consumer] ";
							text = text + "  WHERE [f_ConsumerID]=" + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = "COMMIT TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							result = 1;
						}
						catch (Exception ex)
						{
							this.wgDebugWrite(ex.ToString());
							this.wgDebugWrite(text);
							text = "ROLLBACK TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				this.wgDebugWrite(ex2.ToString());
			}
			return result;
		}

		public int addNewCard(int ConsumerID, long CardNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNewCard_Acc(ConsumerID, CardNO);
			}
			int result = -9;
			try
			{
				string text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
						if (num > 0)
						{
							result = -103;
							return result;
						}
						text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
						sqlCommand.CommandText = text;
						num = sqlCommand.ExecuteNonQuery();
						text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
						sqlCommand.CommandText = text;
						num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
						if (num > 0)
						{
							result = -103;
						}
						else
						{
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
							sqlCommand.CommandText = text;
							num = sqlCommand.ExecuteNonQuery();
							text = "UPDATE t_b_Consumer SET  [f_CardNO]= " + CardNO.ToString();
							text = text + " WHERE f_ConsumerID = " + ConsumerID.ToString();
							sqlCommand.CommandText = text;
							num = sqlCommand.ExecuteNonQuery();
							if (num == 1)
							{
								result = 1;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int addNewCard_Acc(int ConsumerID, long CardNO)
		{
			int result = -9;
			try
			{
				string text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
						if (num > 0)
						{
							result = -103;
							return result;
						}
						text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
						oleDbCommand.CommandText = text;
						num = oleDbCommand.ExecuteNonQuery();
						text = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
						oleDbCommand.CommandText = text;
						num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
						if (num > 0)
						{
							result = -103;
						}
						else
						{
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + CardNO.ToString();
							oleDbCommand.CommandText = text;
							num = oleDbCommand.ExecuteNonQuery();
							text = "UPDATE t_b_Consumer SET  [f_CardNO]= " + CardNO.ToString();
							text = text + " WHERE f_ConsumerID = " + ConsumerID.ToString();
							oleDbCommand.CommandText = text;
							num = oleDbCommand.ExecuteNonQuery();
							if (num == 1)
							{
								result = 1;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int registerLostCard(int ConsumerID, long NewCardNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.registerLostCard_Acc(ConsumerID, NewCardNO);
			}
			int result = -9;
			try
			{
				string text = "SELECT f_CardNO FROM t_b_Consumer WHERE [f_ConsumerID]= " + ConsumerID.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.CommandText = text;
						long num = long.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
						if (num <= 0L)
						{
							result = -104;
						}
						else
						{
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + num.ToString();
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "INSERT INTO t_b_IDCard_Lost ([f_ConsumerID], [f_CardNO]) VALUES( ";
							text = text + ConsumerID.ToString() + "," + num.ToString();
							text += ")";
							sqlCommand.CommandText = text;
							num = (long)sqlCommand.ExecuteNonQuery();
						}
						if (NewCardNO <= 0L)
						{
							text = "Update t_b_Consumer SET f_CardNO = NULL WHERE [f_ConsumerID]= " + ConsumerID.ToString();
						}
						else
						{
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + NewCardNO.ToString();
							sqlCommand.CommandText = text;
							num = (long)sqlCommand.ExecuteNonQuery();
							text = "Update t_b_Consumer SET f_CardNO = " + NewCardNO.ToString() + " WHERE [f_ConsumerID]= " + ConsumerID.ToString();
						}
						sqlCommand.CommandText = text;
						num = (long)sqlCommand.ExecuteNonQuery();
						if (num >= 0L)
						{
							result = (int)num;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int registerLostCard_Acc(int ConsumerID, long NewCardNO)
		{
			int result = -9;
			try
			{
				string text = "SELECT f_CardNO FROM t_b_Consumer WHERE [f_ConsumerID]= " + ConsumerID.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.CommandText = text;
						long num = long.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
						if (num <= 0L)
						{
							result = -104;
						}
						else
						{
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + num.ToString();
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = "INSERT INTO t_b_IDCard_Lost ([f_ConsumerID], [f_CardNO]) VALUES( ";
							text = text + ConsumerID.ToString() + "," + num.ToString();
							text += ")";
							oleDbCommand.CommandText = text;
							num = (long)oleDbCommand.ExecuteNonQuery();
						}
						if (NewCardNO <= 0L)
						{
							text = "Update t_b_Consumer SET f_CardNO = NULL WHERE [f_ConsumerID]= " + ConsumerID.ToString();
						}
						else
						{
							text = "DELETE FROM t_b_IDCard_Lost WHERE [f_CardNO]= " + NewCardNO.ToString();
							oleDbCommand.CommandText = text;
							num = (long)oleDbCommand.ExecuteNonQuery();
							text = "Update t_b_Consumer SET f_CardNO = " + NewCardNO.ToString() + " WHERE [f_ConsumerID]= " + ConsumerID.ToString();
						}
						oleDbCommand.CommandText = text;
						num = (long)oleDbCommand.ExecuteNonQuery();
						if (num >= 0L)
						{
							result = (int)num;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int deleteAllUser()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.deleteAllUser_Acc();
			}
			int result = -9;
			try
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
							text = " DELETE  FROM t_b_UserFloor ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE  FROM t_d_ShiftData ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_Leave ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_ManualCardRecord ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_d_Privilege] ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_b_IDCard_Lost] ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_b_Consumer_Other] ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_b_Consumer] ";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							text = "COMMIT TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							sqlCommand.CommandText = text;
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public int deleteAllUser_Acc()
		{
			int result = -9;
			try
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
							text = " DELETE  FROM t_b_UserFloor ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE  FROM t_d_ShiftData ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_Leave ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							wgTools.WriteLine(text);
							text = " DELETE  FROM t_d_ManualCardRecord ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_d_Privilege] ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_b_IDCard_Lost] ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_b_Consumer_Other] ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = " DELETE FROM [t_b_Consumer] ";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							text = "COMMIT TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
							result = 1;
						}
						catch (Exception)
						{
							text = "ROLLBACK TRANSACTION";
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public long ConsumerNONext()
		{
			return this.ConsumerNONext("");
		}

		public long ConsumerNONext(string startcaption)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.ConsumerNONext_Acc(startcaption);
			}
			long num = 0L;
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.CommandText = "SELECT [f_ConsumerNO] FROM [t_b_Consumer] ";
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							long num2 = -1L;
							string text = wgTools.SetObjToStr(sqlDataReader[0]);
							if (text == "")
							{
								num2 = 0L;
							}
							else
							{
								long.TryParse(text, out num2);
								if (num2 <= 0L && !string.IsNullOrEmpty(startcaption) && text.IndexOf(startcaption) == 0 && text.StartsWith(startcaption))
								{
									text = text.Substring(startcaption.Length);
									if (text == "")
									{
										num2 = 0L;
									}
									else
									{
										long.TryParse(text, out num2);
									}
								}
							}
							if (num < num2)
							{
								num = num2;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			num += 1L;
			return num;
		}

		public long ConsumerNONext_Acc(string startcaption)
		{
			long num = 0L;
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.CommandText = "SELECT [f_ConsumerNO] FROM [t_b_Consumer] ";
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							long num2 = -1L;
							string text = wgTools.SetObjToStr(oleDbDataReader[0]);
							if (text == "")
							{
								num2 = 0L;
							}
							else
							{
								long.TryParse(text, out num2);
								if (num2 <= 0L && !string.IsNullOrEmpty(startcaption) && text.IndexOf(startcaption) == 0 && text.StartsWith(startcaption))
								{
									text = text.Substring(startcaption.Length);
									if (text == "")
									{
										num2 = 0L;
									}
									else
									{
										long.TryParse(text, out num2);
									}
								}
							}
							if (num < num2)
							{
								num = num2;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			num += 1L;
			return num;
		}

		public long ConsumerNONextWithSpace()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.ConsumerNONextWithSpace_Acc();
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.CommandText = "SELECT max(Right('                    ' + [f_ConsumerNO],20)) FROM [t_b_Consumer] ";
						long num = -1L;
						string text = wgTools.SetObjToStr(sqlCommand.ExecuteScalar());
						if (text == "")
						{
							num = 1L;
						}
						else
						{
							long.TryParse(text, out num);
							if (num > 0L)
							{
								num += 1L;
							}
						}
						if (num > 0L)
						{
							sqlCommand.CommandText = "SELECT ([f_ConsumerNO]) FROM [t_b_Consumer] WHERE f_ConsumerNO=" + wgTools.PrepareStr(num.ToString());
							text = wgTools.SetObjToStr(sqlCommand.ExecuteScalar());
							if (text == "")
							{
								return num;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return -1L;
		}

		public long ConsumerNONextWithSpace_Acc()
		{
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.CommandText = "SELECT max(Right('                    ' + [f_ConsumerNO],20)) FROM [t_b_Consumer] ";
						long num = -1L;
						string text = wgTools.SetObjToStr(oleDbCommand.ExecuteScalar());
						if (text == "")
						{
							num = 1L;
						}
						else
						{
							long.TryParse(text, out num);
							if (num > 0L)
							{
								num += 1L;
							}
						}
						if (num > 0L)
						{
							oleDbCommand.CommandText = "SELECT ([f_ConsumerNO]) FROM [t_b_Consumer] WHERE f_ConsumerNO=" + wgTools.PrepareStr(num.ToString());
							text = wgTools.SetObjToStr(oleDbCommand.ExecuteScalar());
							if (text == "")
							{
								return num;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return -1L;
		}

		public long ConsumerNONextWithZero()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.ConsumerNONextWithZero_Acc();
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.CommandText = "SELECT max(Right('00000000000000000000' + RTRIM(LTRIM([f_ConsumerNO])),20)) FROM [t_b_Consumer] ";
						long num = -1L;
						string text = wgTools.SetObjToStr(sqlCommand.ExecuteScalar());
						if (text == "")
						{
							num = 1L;
						}
						else
						{
							long.TryParse(text, out num);
							if (num > 0L)
							{
								num += 1L;
							}
						}
						if (num > 0L)
						{
							sqlCommand.CommandText = "SELECT ([f_ConsumerNO]) FROM [t_b_Consumer] WHERE f_ConsumerNO=" + wgTools.PrepareStr(num.ToString());
							text = wgTools.SetObjToStr(sqlCommand.ExecuteScalar());
							if (text == "")
							{
								return num;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return -1L;
		}

		public long ConsumerNONextWithZero_Acc()
		{
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.CommandText = "SELECT max(Right('00000000000000000000' + RTRIM(LTRIM([f_ConsumerNO])),20)) FROM [t_b_Consumer] ";
						long num = -1L;
						string text = wgTools.SetObjToStr(oleDbCommand.ExecuteScalar());
						if (text == "")
						{
							num = 1L;
						}
						else
						{
							long.TryParse(text, out num);
							if (num > 0L)
							{
								num += 1L;
							}
						}
						if (num > 0L)
						{
							oleDbCommand.CommandText = "SELECT ([f_ConsumerNO]) FROM [t_b_Consumer] WHERE f_ConsumerNO=" + wgTools.PrepareStr(num.ToString());
							text = wgTools.SetObjToStr(oleDbCommand.ExecuteScalar());
							if (text == "")
							{
								return num;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return -1L;
		}

		public static string getErrInfo(int ErrNO)
		{
			string result = "";
			int num = ErrNO;
			if (num <= -201)
			{
				if (num == -901)
				{
					result = CommonStr.strDBConNotCreate;
					return result;
				}
				if (num == -401)
				{
					result = CommonStr.strConsumerNOWrong;
					return result;
				}
				if (num == -201)
				{
					result = CommonStr.strConsumerNameWrong;
					return result;
				}
			}
			else
			{
				switch (num)
				{
				case -104:
					result = CommonStr.strCardNotExisted;
					return result;
				case -103:
					result = CommonStr.strCardAlreadyUsed;
					return result;
				default:
					if (num == -9)
					{
						result = CommonStr.strOperateFailed + "  (E=" + ErrNO.ToString() + ")";
						return result;
					}
					if (num == 0)
					{
						return result;
					}
					break;
				}
			}
			if (ErrNO < 0)
			{
				result = CommonStr.strOperateFailed + "  (E=" + ErrNO.ToString() + ")";
			}
			return result;
		}

		public bool isExisted(long CardNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.isExisted_Acc(CardNO);
			}
			bool result = false;
			try
			{
				string cmdText = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						int num = int.Parse("0" + wgTools.SetObjToStr(sqlCommand.ExecuteScalar()));
						if (num > 0)
						{
							result = true;
							return result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public bool isExisted_Acc(long CardNO)
		{
			bool result = false;
			try
			{
				string cmdText = "SELECT f_ConsumerID From t_b_Consumer WHERE  [f_CardNO]= " + CardNO.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						int num = int.Parse("0" + wgTools.SetObjToStr(oleDbCommand.ExecuteScalar()));
						if (num > 0)
						{
							result = true;
							return result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.wgDebugWrite(ex.ToString());
			}
			return result;
		}
	}
}
