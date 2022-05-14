using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	internal class icControllerZone
	{
		public int addNew(string ZoneName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNew_Acc(ZoneName);
			}
			int result = -9;
			if (ZoneName == null)
			{
				return -201;
			}
			if (ZoneName == "")
			{
				return -201;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text = " INSERT INTO t_b_Controller_Zone (f_ZoneName) values (";
						text += wgTools.PrepareStr(ZoneName);
						text += ")";
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
						text = "SELECT f_ZoneID from [t_b_Controller_Zone]  ORDER BY f_ZoneName ASC ";
						sqlCommand.CommandText = text;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						int num = 1;
						while (sqlDataReader.Read())
						{
							text = "UPDATE t_b_Controller_Zone SET f_ZoneNO= " + num.ToString() + " WHERE  f_ZoneID= " + sqlDataReader[0].ToString();
							wgAppConfig.runUpdateSql(text);
							num++;
						}
						sqlDataReader.Close();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int addNew_Acc(string ZoneName)
		{
			int result = -9;
			if (ZoneName == null)
			{
				return -201;
			}
			if (ZoneName == "")
			{
				return -201;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						string text = " INSERT INTO t_b_Controller_Zone (f_ZoneName) values (";
						text += wgTools.PrepareStr(ZoneName);
						text += ")";
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
						text = "SELECT f_ZoneID from [t_b_Controller_Zone]  ORDER BY f_ZoneName ASC ";
						oleDbCommand.CommandText = text;
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						int num = 1;
						while (oleDbDataReader.Read())
						{
							text = "UPDATE t_b_Controller_Zone SET f_ZoneNO= " + num.ToString() + " WHERE  f_ZoneID= " + oleDbDataReader[0].ToString();
							wgAppConfig.runUpdateSql(text);
							num++;
						}
						oleDbDataReader.Close();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int delete(string ZoneName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.delete_Acc(ZoneName);
			}
			int result = -9;
			if (ZoneName == null)
			{
				return -201;
			}
			if (ZoneName == "")
			{
				return -201;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text = " DELETE FROM t_b_Controller_Zone WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneName);
						text += " ) ";
						text = text + " or (f_ZoneName like " + wgTools.PrepareStr(ZoneName + "\\%");
						text += " ) ";
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int delete_Acc(string ZoneName)
		{
			int result = -9;
			if (ZoneName == null)
			{
				return -201;
			}
			if (ZoneName == "")
			{
				return -201;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						string text = " DELETE FROM t_b_Controller_Zone WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneName);
						text += " ) ";
						text = text + " or (f_ZoneName like " + wgTools.PrepareStr(ZoneName + "\\%");
						text += " ) ";
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public bool checkExisted(string ZoneNewName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.checkExisted_Acc(ZoneNewName);
			}
			bool result = false;
			if (ZoneNewName == null)
			{
				return result;
			}
			if (ZoneNewName == "")
			{
				return result;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text = "SELECT f_ZoneID,f_ZoneName from [t_b_Controller_Zone]  ";
						text += " WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneNewName);
						text += " ) ";
						sqlCommand.CommandText = text;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							result = true;
						}
						sqlDataReader.Close();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public bool checkExisted_Acc(string ZoneNewName)
		{
			bool result = false;
			if (ZoneNewName == null)
			{
				return result;
			}
			if (ZoneNewName == "")
			{
				return result;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						string text = "SELECT f_ZoneID,f_ZoneName from [t_b_Controller_Zone]  ";
						text += " WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneNewName);
						text += " ) ";
						oleDbCommand.CommandText = text;
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							result = true;
						}
						oleDbDataReader.Close();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int getZoneID(string ZoneNewName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getZoneID_Acc(ZoneNewName);
			}
			int result = -1;
			if (ZoneNewName == null)
			{
				return result;
			}
			if (ZoneNewName == "")
			{
				return result;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text = "SELECT f_ZoneID,f_ZoneName from [t_b_Controller_Zone]  ";
						text += " WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneNewName);
						text += " ) ";
						sqlCommand.CommandText = text;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							result = (int)sqlDataReader[0];
						}
						sqlDataReader.Close();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int getZoneID_Acc(string ZoneNewName)
		{
			int result = -1;
			if (ZoneNewName == null)
			{
				return result;
			}
			if (ZoneNewName == "")
			{
				return result;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						string text = "SELECT f_ZoneID,f_ZoneName from [t_b_Controller_Zone]  ";
						text += " WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneNewName);
						text += " ) ";
						oleDbCommand.CommandText = text;
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							result = (int)oleDbDataReader[0];
						}
						oleDbDataReader.Close();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int Update(string ZoneName, string ZoneNewName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.Update_Acc(ZoneName, ZoneNewName);
			}
			int result = -9;
			if (ZoneName == null)
			{
				return -201;
			}
			if (ZoneName == "")
			{
				return -201;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text = "SELECT f_ZoneID,f_ZoneName from [t_b_Controller_Zone]  ";
						text += " WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneName);
						text += " ) ";
						text = text + " or (f_ZoneName like " + wgTools.PrepareStr(ZoneName + "\\%");
						text += " )  ORDER BY f_ZoneName ASC";
						sqlCommand.CommandText = text;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							text = "UPDATE t_b_Controller_Zone SET f_ZoneName= " + wgTools.PrepareStr(ZoneNewName + sqlDataReader[1].ToString().Substring(ZoneName.Length));
							text = text + " WHERE  f_ZoneID= " + sqlDataReader[0].ToString();
							wgAppConfig.runUpdateSql(text);
						}
						sqlDataReader.Close();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int Update_Acc(string ZoneName, string ZoneNewName)
		{
			int result = -9;
			if (ZoneName == null)
			{
				return -201;
			}
			if (ZoneName == "")
			{
				return -201;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						string text = "SELECT f_ZoneID,f_ZoneName from [t_b_Controller_Zone]  ";
						text += " WHERE (f_ZoneName = ";
						text += wgTools.PrepareStr(ZoneName);
						text += " ) ";
						text = text + " or (f_ZoneName like " + wgTools.PrepareStr(ZoneName + "\\%");
						text += " )  ORDER BY f_ZoneName ASC";
						oleDbCommand.CommandText = text;
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							text = "UPDATE t_b_Controller_Zone SET f_ZoneName= " + wgTools.PrepareStr(ZoneNewName + oleDbDataReader[1].ToString().Substring(ZoneName.Length));
							text = text + " WHERE  f_ZoneID= " + oleDbDataReader[0].ToString();
							wgAppConfig.runUpdateSql(text);
						}
						oleDbDataReader.Close();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int getZone(ref ArrayList arrZoneName, ref ArrayList arrZoneID, ref ArrayList arrZoneNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getZone_Acc(ref arrZoneName, ref arrZoneID, ref arrZoneNO);
			}
			int result = -9;
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						arrZoneName.Clear();
						arrZoneID.Clear();
						arrZoneNO.Clear();
						sqlCommand.CommandText = "SELECT f_ZoneName from t_b_Controller_Zone,t_b_Controller_Zone4Operator WHERE t_b_Controller_Zone4Operator.f_ZoneID = t_b_Controller_Zone.f_ZoneID  AND t_b_Controller_Zone4Operator.f_OperatorID = " + icOperator.OperatorID.ToString() + "  order by f_ZoneName ASC";
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						ArrayList arrayList = new ArrayList();
						while (sqlDataReader.Read())
						{
							arrayList.Add(sqlDataReader[0]);
						}
						sqlDataReader.Close();
						if (arrayList.Count == 0)
						{
							arrZoneName.Add("");
							arrZoneID.Add(0);
							arrZoneNO.Add(0);
						}
						string text = "SELECT f_ZoneID,f_ZoneName, f_ZoneNO from [t_b_Controller_Zone]  ";
						text += "  ORDER BY f_ZoneName ASC";
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
						bool flag = true;
						while (sqlDataReader.Read())
						{
							if (arrayList.Count > 0)
							{
								flag = false;
							}
							for (int i = 0; i < arrayList.Count; i++)
							{
								string text2 = (string)sqlDataReader[1];
								if (text2 == arrayList[i].ToString() || text2.IndexOf(arrayList[i].ToString() + "\\") == 0)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								arrZoneID.Add(sqlDataReader[0]);
								arrZoneName.Add(sqlDataReader[1]);
								arrZoneNO.Add(sqlDataReader[2]);
							}
						}
						sqlDataReader.Close();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int getZone_Acc(ref ArrayList arrZoneName, ref ArrayList arrZoneID, ref ArrayList arrZoneNO)
		{
			int result = -9;
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						arrZoneName.Clear();
						arrZoneID.Clear();
						arrZoneNO.Clear();
						oleDbCommand.CommandText = "SELECT f_ZoneName from t_b_Controller_Zone,t_b_Controller_Zone4Operator WHERE t_b_Controller_Zone4Operator.f_ZoneID = t_b_Controller_Zone.f_ZoneID  AND t_b_Controller_Zone4Operator.f_OperatorID = " + icOperator.OperatorID.ToString() + "  order by f_ZoneName ASC";
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						ArrayList arrayList = new ArrayList();
						while (oleDbDataReader.Read())
						{
							arrayList.Add(oleDbDataReader[0]);
						}
						oleDbDataReader.Close();
						if (arrayList.Count == 0)
						{
							arrZoneName.Add("");
							arrZoneID.Add(0);
							arrZoneNO.Add(0);
						}
						string text = "SELECT f_ZoneID,f_ZoneName, f_ZoneNO from [t_b_Controller_Zone]  ";
						text += "  ORDER BY f_ZoneName ASC";
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
						bool flag = true;
						while (oleDbDataReader.Read())
						{
							if (arrayList.Count > 0)
							{
								flag = false;
							}
							for (int i = 0; i < arrayList.Count; i++)
							{
								string text2 = (string)oleDbDataReader[1];
								if (text2 == arrayList[i].ToString() || text2.IndexOf(arrayList[i].ToString() + "\\") == 0)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								arrZoneID.Add(oleDbDataReader[0]);
								arrZoneName.Add(oleDbDataReader[1]);
								arrZoneNO.Add(oleDbDataReader[2]);
							}
						}
						oleDbDataReader.Close();
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int getZoneChildMaxNo(string ZoneName, ArrayList arrZoneName, ArrayList arrZoneNO)
		{
			int num = 0;
			try
			{
				string value = ZoneName + "\\";
				for (int i = 0; i < arrZoneName.Count; i++)
				{
					if (arrZoneName[i].ToString().IndexOf(value) == 0 && int.Parse(arrZoneNO[i].ToString()) > num)
					{
						num = int.Parse(arrZoneNO[i].ToString());
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return num;
		}

		public int getAllowedControllers(ref DataTable dtController)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getAllowedControllers_Acc(ref dtController);
			}
			int result = -9;
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						sqlCommand.CommandText = "SELECT f_ZoneName from t_b_Controller_Zone,t_b_Controller_Zone4Operator WHERE t_b_Controller_Zone4Operator.f_ZoneID = t_b_Controller_Zone.f_ZoneID  AND t_b_Controller_Zone4Operator.f_OperatorID = " + icOperator.OperatorID.ToString() + "  order by f_ZoneName ASC";
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						ArrayList arrayList = new ArrayList();
						while (sqlDataReader.Read())
						{
							arrayList.Add(sqlDataReader[0]);
						}
						sqlDataReader.Close();
						if (arrayList.Count == 0)
						{
							return 1;
						}
						string text = "SELECT f_ZoneID,f_ZoneName, f_ZoneNO from [t_b_Controller_Zone]  ";
						text += "  ORDER BY f_ZoneName ASC";
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(text, sqlConnection))
						{
							using (DataTable dataTable = new DataTable("Zones"))
							{
								sqlDataAdapter.Fill(dataTable);
								using (DataView dataView = new DataView(dataTable))
								{
									int i = 0;
									int num = 0;
									while (i < dtController.Rows.Count)
									{
										DataRow dataRow = dtController.Rows[i];
										bool flag = false;
										if (int.TryParse(dataRow["f_ZoneID"].ToString(), out num))
										{
											dataView.RowFilter = "f_ZoneID = " + dataRow["f_ZoneID"].ToString();
											if (dataView.Count > 0)
											{
												string text2 = (string)dataView[0]["f_ZoneName"];
												for (int j = 0; j < arrayList.Count; j++)
												{
													if (text2 == arrayList[j].ToString() || text2.IndexOf(arrayList[j].ToString() + "\\") == 0)
													{
														flag = true;
														break;
													}
												}
											}
										}
										if (!flag)
										{
											dtController.Rows.Remove(dataRow);
											dtController.AcceptChanges();
										}
										else
										{
											i++;
										}
									}
								}
							}
						}
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public int getAllowedControllers_Acc(ref DataTable dtController)
		{
			int result = -9;
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						oleDbCommand.CommandText = "SELECT f_ZoneName from t_b_Controller_Zone,t_b_Controller_Zone4Operator WHERE t_b_Controller_Zone4Operator.f_ZoneID = t_b_Controller_Zone.f_ZoneID  AND t_b_Controller_Zone4Operator.f_OperatorID = " + icOperator.OperatorID.ToString() + "  order by f_ZoneName ASC";
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						ArrayList arrayList = new ArrayList();
						while (oleDbDataReader.Read())
						{
							arrayList.Add(oleDbDataReader[0]);
						}
						oleDbDataReader.Close();
						if (arrayList.Count == 0)
						{
							return 1;
						}
						string text = "SELECT f_ZoneID,f_ZoneName, f_ZoneNO from [t_b_Controller_Zone]  ";
						text += "  ORDER BY f_ZoneName ASC";
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(text, oleDbConnection))
						{
							using (DataTable dataTable = new DataTable("Zones"))
							{
								oleDbDataAdapter.Fill(dataTable);
								using (DataView dataView = new DataView(dataTable))
								{
									int i = 0;
									int num = 0;
									while (i < dtController.Rows.Count)
									{
										DataRow dataRow = dtController.Rows[i];
										bool flag = false;
										if (int.TryParse(dataRow["f_ZoneID"].ToString(), out num))
										{
											dataView.RowFilter = "f_ZoneID = " + dataRow["f_ZoneID"].ToString();
											if (dataView.Count > 0)
											{
												string text2 = (string)dataView[0]["f_ZoneName"];
												for (int j = 0; j < arrayList.Count; j++)
												{
													if (text2 == arrayList[j].ToString() || text2.IndexOf(arrayList[j].ToString() + "\\") == 0)
													{
														flag = true;
														break;
													}
												}
											}
										}
										if (!flag)
										{
											dtController.Rows.Remove(dataRow);
											dtController.AcceptChanges();
										}
										else
										{
											i++;
										}
									}
								}
							}
						}
					}
				}
				result = 1;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}
	}
}
