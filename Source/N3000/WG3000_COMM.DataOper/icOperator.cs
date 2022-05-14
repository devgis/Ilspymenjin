using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Management;
using System.Net;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.DataOper
{
	internal class icOperator
	{
		private static string m_OperatorName = "";

		private static int m_OperatorID = 0;

		private static DataTable m_dtDefaultAllPrivilege;

		private static DataTable m_dtCurrentOperatorPrivilege;

		private static DataTable dt = null;

		private static DataTable dt1 = null;

		private static ToolStripMenuItem mnuItm = null;

		private static Dictionary<string, string> dicMenuToFrm;

		public static string OperatorName
		{
			get
			{
				return icOperator.m_OperatorName;
			}
		}

		public static int OperatorID
		{
			get
			{
				return icOperator.m_OperatorID;
			}
		}

		public static DataTable dtCurrentOperatorPrivilege
		{
			get
			{
				return icOperator.m_dtCurrentOperatorPrivilege;
			}
		}

		public static bool login(string name, string pwd)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icOperator.login_Acc(name, pwd);
			}
			bool result = false;
			try
			{
				bool flag = true;
				if (!string.IsNullOrEmpty(wgAppConfig.dbConString))
				{
					try
					{
						string cmdText = "SELECT * FROM t_s_Operator WHERE f_OperatorName = " + wgTools.PrepareStr("wiegand");
						using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
						{
							using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
							{
								sqlConnection.Open();
								SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
								if (sqlDataReader.Read())
								{
									flag = false;
								}
								sqlDataReader.Close();
							}
						}
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
				}
				if (flag && name == "wiegand" && pwd == "168668")
				{
					icOperator.m_OperatorID = 1;
					icOperator.m_OperatorName = name;
					result = true;
				}
				else
				{
					string cmdText2;
					if (string.IsNullOrEmpty(pwd))
					{
						cmdText2 = "SELECT * FROM t_s_Operator WHERE f_OperatorName = " + wgTools.PrepareStr(name) + " and f_Password is NULL ";
					}
					else
					{
						cmdText2 = "SELECT * FROM t_s_Operator WHERE f_OperatorName = " + wgTools.PrepareStr(name) + " and f_Password = " + wgTools.PrepareStr(pwd);
					}
					using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection2))
						{
							sqlConnection2.Open();
							SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
							if (sqlDataReader2.Read())
							{
								icOperator.m_OperatorID = int.Parse(sqlDataReader2["f_OperatorID"].ToString());
								icOperator.m_OperatorName = name;
								result = true;
							}
							sqlDataReader2.Close();
						}
					}
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			return result;
		}

		public static bool login_Acc(string name, string pwd)
		{
			bool result = false;
			try
			{
				bool flag = true;
				if (!string.IsNullOrEmpty(wgAppConfig.dbConString))
				{
					try
					{
						string cmdText = "SELECT * FROM t_s_Operator WHERE f_OperatorName = " + wgTools.PrepareStr("wiegand");
						using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
						{
							using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
							{
								oleDbConnection.Open();
								OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
								if (oleDbDataReader.Read())
								{
									flag = false;
								}
								oleDbDataReader.Close();
							}
						}
					}
					catch (Exception ex)
					{
						wgTools.WgDebugWrite(ex.ToString(), new object[0]);
					}
				}
				if (flag && name == "wiegand" && pwd == "168668")
				{
					icOperator.m_OperatorID = 1;
					icOperator.m_OperatorName = name;
					result = true;
				}
				else
				{
					string cmdText2;
					if (string.IsNullOrEmpty(pwd))
					{
						cmdText2 = "SELECT * FROM t_s_Operator WHERE f_OperatorName = " + wgTools.PrepareStr(name) + " and f_Password is NULL ";
					}
					else
					{
						cmdText2 = "SELECT * FROM t_s_Operator WHERE f_OperatorName = " + wgTools.PrepareStr(name) + " and f_Password = " + wgTools.PrepareStr(pwd);
					}
					using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand2 = new OleDbCommand(cmdText2, oleDbConnection2))
						{
							oleDbConnection2.Open();
							OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader();
							if (oleDbDataReader2.Read())
							{
								icOperator.m_OperatorID = int.Parse(oleDbDataReader2["f_OperatorID"].ToString());
								icOperator.m_OperatorName = name;
								result = true;
							}
							oleDbDataReader2.Close();
						}
					}
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			return result;
		}

		public static string PCSysInfo(bool bSuper)
		{
			string text = "";
			try
			{
				text += string.Format("\r\n.Net Framework {0} ", Environment.Version.ToString());
			}
			catch
			{
			}
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)enumerator.Current;
							text += string.Format("\r\n{0}: ", CommonStr.strSystem);
							text += string.Format("\r\n{0} ", managementObject["Caption"]);
							text += string.Format("\r\n{0} ", managementObject["version"].ToString());
							text += string.Format("\r\n{0} ", managementObject["CSDVersion"]);
							try
							{
								RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer");
								if (registryKey != null)
								{
									text += string.Format("\r\n{0}: ", CommonStr.strIEVersion);
									text += string.Format("\r\n{0} ", registryKey.GetValue("Version"));
								}
								registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\DataAccess");
								if (registryKey != null)
								{
									text += string.Format("\r\n{0} ", "MDAC " + registryKey.GetValue("FullInstallVer"));
								}
							}
							catch
							{
							}
							text += string.Format("\r\n{0} ", "---------------------------------------------");
							text += string.Format("\r\n{0} ", CommonStr.strRegistered);
							text += string.Format("\r\n{0} ", managementObject["RegisteredUser"]);
							text += string.Format("\r\n{0} ", managementObject["Organization"].ToString());
							text += string.Format("\r\n{0} ", managementObject["SerialNumber"]);
							text += string.Format("\r\n{0} ", "---------------------------------------------");
							text += string.Format("\r\n{0} ", CommonStr.strComputer);
							text += string.Format("\r\n{0} ", managementObject["TotalVisibleMemorySize"].ToString() + " KB RAM");
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				int width = Screen.PrimaryScreen.Bounds.Width;
				int height = Screen.PrimaryScreen.Bounds.Height;
				text += string.Format("\r\n{0}:{1:d} x {2:d}: ", CommonStr.strDisplaySize, width, height);
				text += string.Format("\r\n{0} IP:", Dns.GetHostName());
				string hostName = Dns.GetHostName();
				IPAddress[] addressList = Dns.GetHostEntry(hostName).AddressList;
				for (int i = 0; i < addressList.Length; i++)
				{
					IPAddress iPAddress = addressList[i];
					text += string.Format("\r\n{0}", iPAddress.ToString());
				}
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International");
				if (registryKey != null)
				{
					text += string.Format("\r\n{0}", "---------------------------------------------");
					text += string.Format("\r\n{0}:{1}", CommonStr.strCountry, registryKey.GetValue("sCountry"));
					text += string.Format("\r\n{0}:{1}", CommonStr.strTimeFormat, registryKey.GetValue("sTimeFormat"));
					text += string.Format("\r\n{0}:{1}", CommonStr.strShortDateFormat, registryKey.GetValue("sShortDate"));
					text += string.Format("\r\n{0}:{1}", CommonStr.strLongDateFormat, registryKey.GetValue("sLongDate"));
				}
			}
			catch
			{
			}
			try
			{
				if (wgAppConfig.IsAccessDB)
				{
					string text2 = "Microsoft Access";
					if (!string.IsNullOrEmpty(text2))
					{
						text += string.Format("\r\n{0}", "---------------------------------------------");
						text += string.Format("\r\n{0}", text2);
					}
				}
				else
				{
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						string text3 = "SELECT @@VERSION";
						using (SqlCommand sqlCommand = new SqlCommand(text3, sqlConnection))
						{
							text3 = wgTools.SetObjToStr(sqlCommand.ExecuteScalar());
						}
						if (!string.IsNullOrEmpty(text3))
						{
							text += string.Format("\r\n{0}", "---------------------------------------------");
							text += string.Format("\r\n{0}", text3);
						}
					}
				}
			}
			catch (Exception)
			{
			}
			text += string.Format("\r\n{0}", "---------------------------------------------");
			text += string.Format("\r\n{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
			text = text.Replace("'", "\"");
			return text;
		}

		public static int checkSoftwareRegister()
		{
			int result = -1;
			string text = "";
			string text2 = "";
			string notes;
			if (wgAppConfig.getSystemParamValue(12, out text2, out text, out notes) > 0)
			{
				int num;
				DateTime dateTime3;
				if (text == "0")
				{
					string value = "1";
					if (!string.IsNullOrEmpty(text2) && int.TryParse(text2, out num) && num > 0)
					{
						value = (num + 1).ToString();
					}
					string eName = DateTime.Now.ToString("yyyy-MM-dd");
					notes = "";
					wgAppConfig.setSystemParamValue(12, eName, value, notes);
					notes = icOperator.PCSysInfo(false);
					eName = "";
					value = "";
					wgAppConfig.setSystemParamValue(38, eName, value, notes);
					result = 0;
				}
				else if (text == "200405")
				{
					result = 1;
				}
				else if (int.TryParse(text, out num))
				{
					DateTime dateTime2;
					if (num > 1)
					{
						DateTime dateTime;
						if (DateTime.TryParse(text2, out dateTime))
						{
							if (dateTime.AddDays((double)(num - 1)) < DateTime.Now || dateTime.AddDays((double)(-(double)(num - 1))) > DateTime.Now)
							{
								text = "";
								result = -1;
							}
							else
							{
								text = CommonStr.strEvaluation;
								result = 0;
							}
						}
					}
					else if (DateTime.TryParse(text2, out dateTime2))
					{
						if (dateTime2.AddDays(60.0) < DateTime.Now || dateTime2.AddDays(-60.0) > DateTime.Now)
						{
							text = "";
							result = -1;
						}
						else
						{
							text = CommonStr.strEvaluation;
							result = 0;
						}
					}
				}
				else if (DateTime.TryParse(text2, out dateTime3))
				{
					if (dateTime3.AddDays(60.0) < DateTime.Now || dateTime3.AddDays(-60.0) > DateTime.Now)
					{
						text = "";
						result = -1;
					}
					else
					{
						text = CommonStr.strEvaluation;
						result = 0;
					}
				}
			}
			return result;
		}

		private static bool isAllowAdd(string FunctionName)
		{
			bool result = true;
			if (!string.IsNullOrEmpty(FunctionName) && FunctionName != null && (FunctionName == "mnuExit" || FunctionName == "mnuMeetingSign"))
			{
				result = false;
			}
			return result;
		}

		private static void CheckSubMenu(ToolStripMenuItem menuItem, ref DataTable dt)
		{
			wgTools.WgDebugWrite(menuItem.Text + "--" + menuItem.Name.ToString(), new object[0]);
			if (icOperator.isAllowAdd(menuItem.Name.ToString()))
			{
				DataRow dataRow = dt.NewRow();
				dataRow[0] = dt.Rows.Count + 1;
				dataRow[1] = menuItem.Name.ToString();
				if (menuItem.Text.IndexOf("(&") > 0)
				{
					dataRow[2] = menuItem.Text.Substring(0, menuItem.Text.IndexOf("(&"));
				}
				else if (menuItem.Text.IndexOf("&") >= 0)
				{
					dataRow[2] = menuItem.Text.Replace("&", "");
				}
				else
				{
					dataRow[2] = menuItem.Text;
				}
				dataRow[3] = 0;
				dataRow[4] = 1;
				dt.Rows.Add(dataRow);
				dt.AcceptChanges();
			}
			for (int i = 0; i < menuItem.DropDownItems.Count; i++)
			{
				if (!(menuItem.DropDownItems[i] is ToolStripSeparator))
				{
					icOperator.CheckSubMenu((ToolStripMenuItem)menuItem.DropDownItems[i], ref dt);
				}
			}
		}

		private static void CheckMenu(MenuStrip Menu, ref DataTable dt)
		{
			foreach (ToolStripMenuItem menuItem in Menu.Items)
			{
				icOperator.CheckSubMenu(menuItem, ref dt);
			}
		}

		private static void IntertIntoDefaultFullFunctionDT(ref DataTable dt, string name, string display)
		{
			DataRow dataRow = dt.NewRow();
			dataRow[0] = dt.Rows.Count + 1;
			dataRow[1] = name;
			dataRow[2] = display;
			dataRow[3] = 0;
			dataRow[4] = 1;
			dt.Rows.Add(dataRow);
			dt.AcceptChanges();
		}

		public static void getDefaultFullFunction(MenuStrip mnuMain)
		{
			icOperator.dt1 = new DataTable();
			icOperator.dt1.TableName = "OperatePrivilege";
			icOperator.dt1.Columns.Add("f_FunctionID");
			icOperator.dt1.Columns.Add("f_FunctionName");
			icOperator.dt1.Columns.Add("f_FunctionDisplayName");
			icOperator.dt1.Columns.Add("f_ReadOnly");
			icOperator.dt1.Columns.Add("f_FullControl");
			icOperator.CheckMenu(mnuMain, ref icOperator.dt1);
			icOperator.IntertIntoDefaultFullFunctionDT(ref icOperator.dt1, "TotalControl_RealGetCardRecord", "Real GetCardRecord");
			icOperator.IntertIntoDefaultFullFunctionDT(ref icOperator.dt1, "TotalControl_RemoteOpen", "Remote Open");
			icOperator.IntertIntoDefaultFullFunctionDT(ref icOperator.dt1, "TotalControl_SetDoorControl", "Set Door Control");
			icOperator.IntertIntoDefaultFullFunctionDT(ref icOperator.dt1, "TotalControl_SetDoorDelay", "Set Door Delay");
			icOperator.IntertIntoDefaultFullFunctionDT(ref icOperator.dt1, "TotalControl_VideoMonitor", "Video Monitor");
			icOperator.IntertIntoDefaultFullFunctionDT(ref icOperator.dt1, "TotalControl_Map", "Map");
			icOperator.m_dtDefaultAllPrivilege = icOperator.dt1;
			string strSql = string.Format("DELETE FROM t_s_OperatorPrivilege", new object[0]);
			wgAppConfig.runUpdateSql(strSql);
			for (int i = 0; i < icOperator.dt1.Rows.Count; i++)
			{
				strSql = string.Format("INSERT INTO t_s_OperatorPrivilege ([f_OperatorID], [f_FunctionID], [f_FunctionName], [f_FunctionDisplayName], [f_ReadOnly], [f_FullControl]) VALUES(1, {0:d},{1},{2},{3:d},{4:d})", new object[]
				{
					int.Parse(icOperator.dt1.Rows[i][0].ToString()),
					wgTools.PrepareStr(icOperator.dt1.Rows[i][1].ToString()),
					wgTools.PrepareStr(icOperator.dt1.Rows[i][2].ToString()),
					int.Parse(icOperator.dt1.Rows[i][3].ToString()),
					int.Parse(icOperator.dt1.Rows[i][4].ToString())
				});
				wgAppConfig.runUpdateSql(strSql);
			}
			DatatableToXml.CDataToXmlFile(icOperator.dt1, "OperatePrivilegeCurrent.XML");
		}

		public static DataTable getOperatorPrivilege(int OperatorID)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return icOperator.getOperatorPrivilege_Acc(OperatorID);
			}
			icOperator.dt = new DataTable();
			icOperator.dt.TableName = "OperatePrivilege";
			icOperator.dt.Columns.Add("f_FunctionID");
			icOperator.dt.Columns.Add("f_FunctionName");
			icOperator.dt.Columns.Add("f_FunctionDisplayName");
			icOperator.dt.Columns.Add("f_ReadOnly");
			icOperator.dt.Columns.Add("f_FullControl");
			string text = "SELECT [f_FunctionID], [f_FunctionName], [f_FunctionDisplayName], [f_ReadOnly], [f_FullControl] FROM t_s_OperatorPrivilege WHERE f_OperatorID = " + OperatorID.ToString();
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (!sqlDataReader.HasRows)
					{
						sqlDataReader.Close();
						text = "SELECT [f_FunctionID], [f_FunctionName], [f_FunctionDisplayName], [f_ReadOnly], [f_FullControl] FROM t_s_OperatorPrivilege WHERE f_OperatorID = " + 1.ToString();
						sqlCommand.CommandText = text;
						sqlDataReader = sqlCommand.ExecuteReader();
					}
					while (sqlDataReader.Read())
					{
						DataRow dataRow = icOperator.dt.NewRow();
						dataRow[0] = sqlDataReader[0];
						dataRow[1] = sqlDataReader[1];
						dataRow[2] = sqlDataReader[2];
						dataRow[3] = (int.Parse(sqlDataReader[3].ToString()) > 0);
						dataRow[4] = (int.Parse(sqlDataReader[4].ToString()) > 0);
						icOperator.dt.Rows.Add(dataRow);
					}
					sqlDataReader.Close();
					icOperator.dt.AcceptChanges();
				}
			}
			return icOperator.dt;
		}

		public static DataTable getOperatorPrivilege_Acc(int OperatorID)
		{
			icOperator.dt = new DataTable();
			icOperator.dt.TableName = "OperatePrivilege";
			icOperator.dt.Columns.Add("f_FunctionID");
			icOperator.dt.Columns.Add("f_FunctionName");
			icOperator.dt.Columns.Add("f_FunctionDisplayName");
			icOperator.dt.Columns.Add("f_ReadOnly");
			icOperator.dt.Columns.Add("f_FullControl");
			string text = "SELECT [f_FunctionID], [f_FunctionName], [f_FunctionDisplayName], [f_ReadOnly], [f_FullControl] FROM t_s_OperatorPrivilege WHERE f_OperatorID = " + OperatorID.ToString();
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (!oleDbDataReader.HasRows)
					{
						oleDbDataReader.Close();
						text = "SELECT [f_FunctionID], [f_FunctionName], [f_FunctionDisplayName], [f_ReadOnly], [f_FullControl] FROM t_s_OperatorPrivilege WHERE f_OperatorID = " + 1.ToString();
						oleDbCommand.CommandText = text;
						oleDbDataReader = oleDbCommand.ExecuteReader();
					}
					while (oleDbDataReader.Read())
					{
						DataRow dataRow = icOperator.dt.NewRow();
						dataRow[0] = oleDbDataReader[0];
						dataRow[1] = oleDbDataReader[1];
						dataRow[2] = oleDbDataReader[2];
						dataRow[3] = (int.Parse(oleDbDataReader[3].ToString()) > 0);
						dataRow[4] = (int.Parse(oleDbDataReader[4].ToString()) > 0);
						icOperator.dt.Rows.Add(dataRow);
					}
					oleDbDataReader.Close();
					icOperator.dt.AcceptChanges();
				}
			}
			return icOperator.dt;
		}

		public static int setOperatorPrivilege(int OperatorID, DataTable dtPrivilege)
		{
			string strSql = string.Format("DELETE FROM t_s_OperatorPrivilege WHERE [f_OperatorID] =" + OperatorID.ToString(), new object[0]);
			wgAppConfig.runUpdateSql(strSql);
			for (int i = 0; i < dtPrivilege.Rows.Count; i++)
			{
				int num = 1;
				int num2 = 1;
				if (string.IsNullOrEmpty(dtPrivilege.Rows[i][3].ToString()))
				{
					num = 0;
				}
				else if (!bool.Parse(dtPrivilege.Rows[i][3].ToString()))
				{
					num = 0;
				}
				if (string.IsNullOrEmpty(dtPrivilege.Rows[i][4].ToString()))
				{
					num2 = 0;
				}
				else if (!bool.Parse(dtPrivilege.Rows[i][4].ToString()))
				{
					num2 = 0;
				}
				strSql = string.Format("INSERT INTO t_s_OperatorPrivilege ([f_OperatorID], [f_FunctionID], [f_FunctionName], [f_FunctionDisplayName], [f_ReadOnly], [f_FullControl]) VALUES({5}, {0:d},{1},{2},{3:d},{4:d})", new object[]
				{
					int.Parse(dtPrivilege.Rows[i][0].ToString()),
					wgTools.PrepareStr(dtPrivilege.Rows[i][1].ToString()),
					wgTools.PrepareStr(dtPrivilege.Rows[i][2].ToString()),
					num,
					num2,
					OperatorID.ToString()
				});
				wgAppConfig.runUpdateSql(strSql);
			}
			return 1;
		}

		private static void FindInSubMenu(ToolStripMenuItem menuItem, string MenuItemName, ref ToolStripMenuItem mnuItm)
		{
			if (mnuItm.Text != "")
			{
				return;
			}
			if (menuItem.Name.ToString().Equals(MenuItemName))
			{
				mnuItm = menuItem;
			}
			for (int i = 0; i < menuItem.DropDownItems.Count; i++)
			{
				if (!(menuItem.DropDownItems[i] is ToolStripSeparator))
				{
					icOperator.FindInSubMenu((ToolStripMenuItem)menuItem.DropDownItems[i], MenuItemName, ref mnuItm);
				}
			}
		}

		public static void FindInMenu(MenuStrip Menu, string MenuItemName, ref ToolStripMenuItem mnuItm)
		{
			foreach (ToolStripMenuItem menuItem in Menu.Items)
			{
				icOperator.FindInSubMenu(menuItem, MenuItemName, ref mnuItm);
			}
		}

		public static void OperatePrivilegeLoad(MenuStrip mnuMain)
		{
			if (icOperator.m_OperatorID == 1)
			{
				return;
			}
			if (icOperator.m_dtCurrentOperatorPrivilege == null)
			{
				icOperator.m_dtCurrentOperatorPrivilege = icOperator.getOperatorPrivilege(icOperator.m_OperatorID);
			}
			if (icOperator.m_dtCurrentOperatorPrivilege.Rows.Count > 0)
			{
				foreach (DataRow dataRow in icOperator.m_dtCurrentOperatorPrivilege.Rows)
				{
					icOperator.mnuItm = new ToolStripMenuItem();
					icOperator.mnuItm.Name = "";
					icOperator.FindInMenu(mnuMain, dataRow["f_FunctionName"].ToString(), ref icOperator.mnuItm);
					if (icOperator.mnuItm.Name.ToString() != "" && !bool.Parse(dataRow["f_ReadOnly"].ToString()) && !bool.Parse(dataRow["f_FullControl"].ToString()))
					{
						icOperator.mnuItm.Visible = false;
					}
				}
			}
		}

		public static void OperatePrivilegeLoad(ref string[,] funcList, int funItemLen, int funNameLoc)
		{
			if (icOperator.m_OperatorID == 1)
			{
				return;
			}
			if (icOperator.m_dtCurrentOperatorPrivilege == null)
			{
				icOperator.m_dtCurrentOperatorPrivilege = icOperator.getOperatorPrivilege(icOperator.m_OperatorID);
			}
			if (icOperator.m_dtCurrentOperatorPrivilege.Rows.Count > 0)
			{
				foreach (DataRow dataRow in icOperator.m_dtCurrentOperatorPrivilege.Rows)
				{
					if (!bool.Parse(dataRow["f_ReadOnly"].ToString()) && !bool.Parse(dataRow["f_FullControl"].ToString()))
					{
						for (int i = 0; i < funcList.Length / funItemLen; i++)
						{
							if (!string.IsNullOrEmpty(funcList[i, funNameLoc]) && funcList[i, funNameLoc] == dataRow["f_FunctionName"].ToString())
							{
								funcList[i, funNameLoc] = null;
								break;
							}
						}
					}
				}
			}
		}

		public static bool OperatePrivilegeFullControl(string funName)
		{
			bool flag = false;
			return icOperator.OperatePrivilegeVisible(funName, ref flag) && !flag;
		}

		public static bool OperatePrivilegeVisible(string funName)
		{
			bool flag = false;
			return icOperator.OperatePrivilegeVisible(funName, ref flag);
		}

		public static bool OperatePrivilegeVisible(string funName, ref bool bReadOnly)
		{
			if (icOperator.m_OperatorID == 1)
			{
				return true;
			}
			if (icOperator.m_dtCurrentOperatorPrivilege == null)
			{
				icOperator.m_dtCurrentOperatorPrivilege = icOperator.getOperatorPrivilege(icOperator.m_OperatorID);
			}
			if (icOperator.m_dtCurrentOperatorPrivilege.Rows.Count > 0)
			{
				foreach (DataRow dataRow in icOperator.m_dtCurrentOperatorPrivilege.Rows)
				{
					if (bool.Parse(dataRow["f_ReadOnly"].ToString()) || bool.Parse(dataRow["f_FullControl"].ToString()))
					{
						if (!bool.Parse(dataRow["f_FullControl"].ToString()) && funName == dataRow["f_FunctionName"].ToString())
						{
							bReadOnly = true;
						}
					}
					else if (funName == dataRow["f_FunctionName"].ToString())
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		public static bool OperatePrivilegeTreeDisplay(string FunctionName)
		{
			if (icOperator.m_OperatorID == 1)
			{
				return true;
			}
			if (icOperator.m_dtCurrentOperatorPrivilege == null)
			{
				icOperator.m_dtCurrentOperatorPrivilege = icOperator.getOperatorPrivilege(icOperator.m_OperatorID);
			}
			if (string.IsNullOrEmpty(FunctionName))
			{
				return false;
			}
			if (icOperator.m_dtCurrentOperatorPrivilege.Rows.Count > 0)
			{
				using (DataView dataView = new DataView(icOperator.m_dtCurrentOperatorPrivilege))
				{
					dataView.RowFilter = string.Format("f_FunctionName ={0}", wgTools.PrepareStr(FunctionName));
					if (dataView.Count > 0 && (bool.Parse(dataView[0]["f_ReadOnly"].ToString()) || bool.Parse(dataView[0]["f_FullControl"].ToString())))
					{
						return true;
					}
					return false;
				}
				return true;
			}
			return true;
		}

		private static void getDicMenuToFrm()
		{
			icOperator.dicMenuToFrm = new Dictionary<string, string>();
			icOperator.dicMenuToFrm.Add("frmControllers", "mnuControllers");
			icOperator.dicMenuToFrm.Add("frmDepartments", "mnuGroups");
			icOperator.dicMenuToFrm.Add("frmUsers", "mnuConsumers");
			icOperator.dicMenuToFrm.Add("frmControlSegs", "mnuControlSeg");
			icOperator.dicMenuToFrm.Add("frmPrivileges", "mnuPrivilege");
			icOperator.dicMenuToFrm.Add("frmConsole", "mnuTotalControl");
			icOperator.dicMenuToFrm.Add("btnCheckController", "mnuCheckController");
			icOperator.dicMenuToFrm.Add("btnAdjustTime", "mnuAdjustTime");
			icOperator.dicMenuToFrm.Add("btnUpload", "mnuUpload");
			icOperator.dicMenuToFrm.Add("btnMonitor", "mnuMonitor");
			icOperator.dicMenuToFrm.Add("btnGetRecords", "mnuGetCardRecords");
			icOperator.dicMenuToFrm.Add("btnRemoteOpen", "TotalControl_RemoteOpen");
			icOperator.dicMenuToFrm.Add("btnMaps", "btnMaps");
			icOperator.dicMenuToFrm.Add("btnRealtimeGetRecords", "mnuRealtimeGetRecords");
			icOperator.dicMenuToFrm.Add("frmSwipeRecords", "mnuCardRecords");
			icOperator.dicMenuToFrm.Add("dfrmOperator", "cmdOperatorManage");
			icOperator.dicMenuToFrm.Add("frmAbout", "mnuAbout");
		}

		public static void getFrmOperatorPrivilege(string frmName, out bool readOnly, out bool fullControl)
		{
			readOnly = true;
			fullControl = true;
			if (string.IsNullOrEmpty(frmName))
			{
				return;
			}
			if (icOperator.dicMenuToFrm == null)
			{
				icOperator.getDicMenuToFrm();
			}
			if (icOperator.dicMenuToFrm != null && icOperator.dicMenuToFrm.ContainsKey(frmName))
			{
				if (icOperator.m_dtCurrentOperatorPrivilege == null)
				{
					icOperator.m_dtCurrentOperatorPrivilege = icOperator.getOperatorPrivilege(icOperator.m_OperatorID);
				}
				if (icOperator.m_dtCurrentOperatorPrivilege.Rows.Count > 0)
				{
					using (DataView dataView = new DataView(icOperator.m_dtCurrentOperatorPrivilege))
					{
						dataView.RowFilter = string.Format("f_FunctionName ={0}", wgTools.PrepareStr(icOperator.dicMenuToFrm[frmName]));
						if (dataView.Count > 0)
						{
							readOnly = bool.Parse(dataView[0]["f_ReadOnly"].ToString());
							fullControl = bool.Parse(dataView[0]["f_FullControl"].ToString());
						}
					}
				}
			}
		}
	}
}
