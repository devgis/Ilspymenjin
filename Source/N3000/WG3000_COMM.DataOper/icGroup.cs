using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	internal class icGroup
	{
		public int addNew(string GroupName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNew_Acc(GroupName);
			}
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					string text = " INSERT INTO t_b_Group (f_GroupName) values (";
					text += wgTools.PrepareStr(GroupName);
					text += ")";
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
						text = "SELECT f_GroupID from [t_b_Group]  ORDER BY f_GroupName ASC ";
						sqlCommand.CommandText = text;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						int num = 1;
						while (sqlDataReader.Read())
						{
							text = "UPDATE t_b_Group SET f_GroupNO= " + num.ToString() + " WHERE  f_GroupID= " + sqlDataReader[0].ToString();
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

		public int addNew_Acc(string GroupName)
		{
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					string text = " INSERT INTO t_b_Group (f_GroupName) values (";
					text += wgTools.PrepareStr(GroupName);
					text += ")";
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
						text = "SELECT f_GroupID from [t_b_Group]  ORDER BY f_GroupName ASC ";
						oleDbCommand.CommandText = text;
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						int num = 1;
						while (oleDbDataReader.Read())
						{
							text = "UPDATE t_b_Group SET f_GroupNO= " + num.ToString() + " WHERE  f_GroupID= " + oleDbDataReader[0].ToString();
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

		public int addNew4BatchExcel(string GroupName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.addNew4BatchExcel_Acc(GroupName);
			}
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					string text = " INSERT INTO t_b_Group (f_GroupName) values (";
					text += wgTools.PrepareStr(GroupName);
					text += ")";
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
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

		public int addNew4BatchExcel_Acc(string GroupName)
		{
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					string text = " INSERT INTO t_b_Group (f_GroupName) values (";
					text += wgTools.PrepareStr(GroupName);
					text += ")";
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
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

		public int updateGroupNO()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.updateGroupNO_Acc();
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
				{
					string text = "SELECT f_GroupID from [t_b_Group]  ORDER BY f_GroupName ASC ";
					sqlCommand.CommandText = text;
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					int num = 1;
					while (sqlDataReader.Read())
					{
						text = "UPDATE t_b_Group SET f_GroupNO= " + num.ToString() + " WHERE  f_GroupID= " + sqlDataReader[0].ToString();
						wgAppConfig.runUpdateSql(text);
						num++;
					}
					sqlDataReader.Close();
				}
			}
			return 1;
		}

		public int updateGroupNO_Acc()
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					string text = "SELECT f_GroupID from [t_b_Group]  ORDER BY f_GroupName ASC ";
					oleDbCommand.CommandText = text;
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					int num = 1;
					while (oleDbDataReader.Read())
					{
						text = "UPDATE t_b_Group SET f_GroupNO= " + num.ToString() + " WHERE  f_GroupID= " + oleDbDataReader[0].ToString();
						wgAppConfig.runUpdateSql(text);
						num++;
					}
					oleDbDataReader.Close();
				}
			}
			return 1;
		}

		public int delete(string GroupName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.delete_Acc(GroupName);
			}
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					string text = " DELETE FROM t_b_Group WHERE (f_GroupName = ";
					text += wgTools.PrepareStr(GroupName);
					text += " ) ";
					text = text + " or (f_GroupName like " + wgTools.PrepareStr(GroupName + "\\%");
					text += " ) ";
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
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

		public int delete_Acc(string GroupName)
		{
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					string text = " DELETE FROM t_b_Group WHERE (f_GroupName = ";
					text += wgTools.PrepareStr(GroupName);
					text += " ) ";
					text = text + " or (f_GroupName like " + wgTools.PrepareStr(GroupName + "\\%");
					text += " ) ";
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
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

		public bool checkExisted(string GroupNewName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.checkExisted_Acc(GroupNewName);
			}
			bool result = false;
			if (GroupNewName == null)
			{
				return result;
			}
			if (GroupNewName == "")
			{
				return result;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					string text = "SELECT f_GroupID,f_GroupName from [t_b_Group]  ";
					text += " WHERE (f_GroupName = ";
					text += wgTools.PrepareStr(GroupNewName);
					text += " ) ";
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
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

		public bool checkExisted_Acc(string GroupNewName)
		{
			bool result = false;
			if (GroupNewName == null)
			{
				return result;
			}
			if (GroupNewName == "")
			{
				return result;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					string text = "SELECT f_GroupID,f_GroupName from [t_b_Group]  ";
					text += " WHERE (f_GroupName = ";
					text += wgTools.PrepareStr(GroupNewName);
					text += " ) ";
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
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

		public int getGroupID(string GroupNewName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getGroupID_Acc(GroupNewName);
			}
			int result = -1;
			if (GroupNewName == null)
			{
				return result;
			}
			if (GroupNewName == "")
			{
				return result;
			}
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					string text = "SELECT f_GroupID,f_GroupName from [t_b_Group]  ";
					text += " WHERE (f_GroupName = ";
					text += wgTools.PrepareStr(GroupNewName);
					text += " ) ";
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
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

		public int getGroupID_Acc(string GroupNewName)
		{
			int result = -1;
			if (GroupNewName == null)
			{
				return result;
			}
			if (GroupNewName == "")
			{
				return result;
			}
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					string text = "SELECT f_GroupID,f_GroupName from [t_b_Group]  ";
					text += " WHERE (f_GroupName = ";
					text += wgTools.PrepareStr(GroupNewName);
					text += " ) ";
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
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

		public int Update(string GroupName, string GroupNewName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.Update_Acc(GroupName, GroupNewName);
			}
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
				{
					try
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text = "SELECT f_GroupID,f_GroupName from [t_b_Group]  ";
						text += " WHERE (f_GroupName = ";
						text += wgTools.PrepareStr(GroupName);
						text += " ) ";
						text = text + " or (f_GroupName like " + wgTools.PrepareStr(GroupName + "\\%");
						text += " )  ORDER BY f_GroupName ASC";
						sqlCommand.CommandText = text;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							text = "UPDATE t_b_Group SET f_GroupName= " + wgTools.PrepareStr(GroupNewName + sqlDataReader[1].ToString().Substring(GroupName.Length));
							text = text + " WHERE  f_GroupID= " + sqlDataReader[0].ToString();
							wgAppConfig.runUpdateSql(text);
						}
						sqlDataReader.Close();
						result = 1;
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
				}
			}
			return result;
		}

		public int Update_Acc(string GroupName, string GroupNewName)
		{
			int result = -9;
			if (GroupName == null)
			{
				return -201;
			}
			if (GroupName == "")
			{
				return -201;
			}
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					try
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						string text = "SELECT f_GroupID,f_GroupName from [t_b_Group]  ";
						text += " WHERE (f_GroupName = ";
						text += wgTools.PrepareStr(GroupName);
						text += " ) ";
						text = text + " or (f_GroupName like " + wgTools.PrepareStr(GroupName + "\\%");
						text += " )  ORDER BY f_GroupName ASC";
						oleDbCommand.CommandText = text;
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							text = "UPDATE t_b_Group SET f_GroupName= " + wgTools.PrepareStr(GroupNewName + oleDbDataReader[1].ToString().Substring(GroupName.Length));
							text = text + " WHERE  f_GroupID= " + oleDbDataReader[0].ToString();
							wgAppConfig.runUpdateSql(text);
						}
						oleDbDataReader.Close();
						result = 1;
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
				}
			}
			return result;
		}

		public int getGroup(ref ArrayList arrGroupName, ref ArrayList arrGroupID, ref ArrayList arrGroupNO)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getGroup_Acc(ref arrGroupName, ref arrGroupID, ref arrGroupNO);
			}
			int result = -9;
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					arrGroupName.Clear();
					arrGroupID.Clear();
					arrGroupNO.Clear();
					ArrayList arrayList = new ArrayList();
					using (SqlCommand sqlCommand = new SqlCommand("SELECT f_GroupName from t_b_Group,t_b_Group4Operator WHERE t_b_Group4Operator.f_GroupID = t_b_Group.f_GroupID  AND t_b_Group4Operator.f_OperatorID = " + icOperator.OperatorID.ToString() + "  order by f_GroupName ASC", sqlConnection))
					{
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							arrayList.Add(sqlDataReader[0]);
						}
						sqlDataReader.Close();
						if (arrayList.Count == 0)
						{
							arrGroupName.Add("");
							arrGroupID.Add(0);
							arrGroupNO.Add(0);
						}
						string text = "SELECT f_GroupID,f_GroupName, f_GroupNO from [t_b_Group]  ";
						text += "  ORDER BY f_GroupName ASC";
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
								arrGroupID.Add(sqlDataReader[0]);
								arrGroupName.Add(sqlDataReader[1]);
								arrGroupNO.Add(sqlDataReader[2]);
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

		public int getGroup_Acc(ref ArrayList arrGroupName, ref ArrayList arrGroupID, ref ArrayList arrGroupNO)
		{
			int result = -9;
			try
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					arrGroupName.Clear();
					arrGroupID.Clear();
					arrGroupNO.Clear();
					ArrayList arrayList = new ArrayList();
					using (OleDbCommand oleDbCommand = new OleDbCommand("SELECT f_GroupName from t_b_Group,t_b_Group4Operator WHERE t_b_Group4Operator.f_GroupID = t_b_Group.f_GroupID  AND t_b_Group4Operator.f_OperatorID = " + icOperator.OperatorID.ToString() + "  order by f_GroupName ASC", oleDbConnection))
					{
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							arrayList.Add(oleDbDataReader[0]);
						}
						oleDbDataReader.Close();
						if (arrayList.Count == 0)
						{
							arrGroupName.Add("");
							arrGroupID.Add(0);
							arrGroupNO.Add(0);
						}
						string text = "SELECT f_GroupID,f_GroupName, f_GroupNO from [t_b_Group]  ";
						text += "  ORDER BY f_GroupName ASC";
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
								arrGroupID.Add(oleDbDataReader[0]);
								arrGroupName.Add(oleDbDataReader[1]);
								arrGroupNO.Add(oleDbDataReader[2]);
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

		public static int getGroupChildMaxNo(string groupName, ArrayList arrGroupName, ArrayList arrGroupNO)
		{
			int num = 0;
			try
			{
				string value = groupName + "\\";
				for (int i = 0; i < arrGroupName.Count; i++)
				{
					if (arrGroupName[i].ToString().IndexOf(value) == 0 && int.Parse(arrGroupNO[i].ToString()) > num)
					{
						num = int.Parse(arrGroupNO[i].ToString());
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return num;
		}
	}
}
