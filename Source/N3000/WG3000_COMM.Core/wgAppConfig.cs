using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.DataOper;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Core
{
	internal class wgAppConfig
	{
		public const int dbControllerUserDefaultPassword = 345678;

		public const string gc_EventSourceName = "n3k_log";

		private const string n3k_cust = "\\n3k_cust.xml";

		public static string ProductTypeOfApp = "AccessControl";

		public static bool IsLogin = false;

		public static string LoginTitle = "";

		private static string m_CultureInfoStr = "";

		private static bool m_IsAccessDB = false;

		public static bool gRestart = false;

		private static string m_dbConString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AccessData;Data Source=(local) ";

		public static int dbCommandTimeout = 600;

		private static string m_dbName = "AccessData";

		public static DateTime dtLast = DateTime.Now;

		private static Icon currenAppIcon = null;

		private static int tryCreateCnt = 0;

		private static string defaultCustConfigzhCHS = "<NewDataSet>\r\n  <xs:schema id=\"NewDataSet\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n    <xs:element name=\"NewDataSet\" msdata:IsDataSet=\"true\" msdata:MainDataTable=\"appSettings\" msdata:UseCurrentLocale=\"true\">\r\n      <xs:complexType>\r\n        <xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n          <xs:element name=\"appSettings\">\r\n            <xs:complexType>\r\n              <xs:sequence>\r\n                <xs:element name=\"key\" type=\"xs:string\" minOccurs=\"0\" />\r\n                <xs:element name=\"value\" type=\"xs:string\" minOccurs=\"0\" />\r\n              </xs:sequence>\r\n            </xs:complexType>\r\n          </xs:element>\r\n        </xs:choice>\r\n      </xs:complexType>\r\n    </xs:element>\r\n  </xs:schema>\r\n  <appSettings>\r\n    <key>dbConnection</key>\r\n    <value/>\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>Language</key>\r\n    <value>zh-CHS</value>\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>autologinName</key>\r\n    <value />\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>autologinPassword</key>\r\n    <value />\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>rgtries</key>\r\n    <value>1</value>\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>NewSoftwareVersionInfo</key>\r\n    <value>1.0.2</value>\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>RunTimes</key>\r\n    <value></value>\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>NewSoftwareSpecialVersionInfo</key>\r\n    <value>1.0.2</value>\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>CommCurrent</key>\r\n    <value />\r\n  </appSettings>\r\n  <appSettings>\r\n    <key>RunTimeAt</key>\r\n    <value>0</value>\r\n  </appSettings>\r\n</NewDataSet>";

		public static DateTime photoDirectoryLastWriteTime = default(DateTime);

		private static int photoDirectoryLastFileCount = -1;

		private static ArrayList arrPhotoFileFullNames = new ArrayList();

		private static string lastPhotoDirectoryName = "";

		private static string m_PhotoDiriectyName = "";

		private static bool m_bCreatePhotoDirectory = false;

		private static bool m_bFindDirectoryNetShare = false;

		private static string m_DirectoryNetShare = "";

		private static DataTable tb = null;

		private static DataView dv = null;

		public static bool bFloorRoomManager = false;

		public static string CultureInfoStr
		{
			get
			{
				return wgAppConfig.m_CultureInfoStr;
			}
			set
			{
				if (value != null)
				{
					wgAppConfig.m_CultureInfoStr = value;
				}
			}
		}

		public static bool IsAccessDB
		{
			get
			{
				return wgAppConfig.m_IsAccessDB;
			}
			set
			{
				wgAppConfig.m_IsAccessDB = value;
				wgTools.IsSqlServer = !value;
			}
		}

		public static string accessDbName
		{
			get
			{
				return "iCCard3000";
			}
		}

		public static string dbConString
		{
			get
			{
				return wgAppConfig.m_dbConString;
			}
			set
			{
				wgAppConfig.m_dbConString = value;
			}
		}

		public static string dbName
		{
			get
			{
				return wgAppConfig.m_dbName;
			}
			set
			{
				wgAppConfig.m_dbName = value;
			}
		}

		public static string dbWEBUserName
		{
			get
			{
				return "WEBUsers";
			}
		}

		public static int LogEventMaxCount
		{
			get
			{
				return 10000;
			}
		}

		private static string BackupDir
		{
			get
			{
				return Application.StartupPath + "\\BACKUP\\";
			}
		}

		private wgAppConfig()
		{
		}

		public static int runUpdateSql(string strSql)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.runUpdateSql_Acc(strSql);
			}
			int result = -1;
			if (string.IsNullOrEmpty(strSql))
			{
				return result;
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					result = sqlCommand.ExecuteNonQuery();
				}
			}
			return result;
		}

		public static int runUpdateSql_Acc(string strSql)
		{
			int result = -1;
			if (string.IsNullOrEmpty(strSql))
			{
				return result;
			}
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
				{
					result = oleDbCommand.ExecuteNonQuery();
				}
			}
			return result;
		}

		public static int getValBySql(string strSql)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.getValBySql_Acc(strSql);
			}
			int result = 0;
			if (string.IsNullOrEmpty(strSql))
			{
				return result;
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					int.TryParse(sqlCommand.ExecuteScalar().ToString(), out result);
				}
			}
			return result;
		}

		public static int getValBySql_Acc(string strSql)
		{
			int result = 0;
			if (string.IsNullOrEmpty(strSql))
			{
				return result;
			}
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
				{
					int.TryParse(oleDbCommand.ExecuteScalar().ToString(), out result);
				}
			}
			return result;
		}

		public static void wgLogWithoutDB(string strMsg, EventLogEntryType entryType, byte[] rawData)
		{
			try
			{
				string text = string.Concat(new object[]
				{
					icOperator.OperatorID,
					".",
					icOperator.OperatorName,
					".",
					strMsg
				});
				text = DateTime.Now.ToString("yyyy-MM-dd H-mm-ss") + "\t" + text;
				if (rawData != null)
				{
					text = text + "\t:" + Encoding.ASCII.GetString(rawData);
				}
				using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\n3k_log.log", true))
				{
					streamWriter.WriteLine(text);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void wgLog(string strMsg)
		{
			wgAppConfig.wgLog(strMsg, EventLogEntryType.Information, null);
		}

		public static void wgDBLog(string strMsg, EventLogEntryType entryType, byte[] rawData)
		{
			if (wgAppConfig.IsAccessDB)
			{
				wgAppConfig.wgDBLog_Acc(strMsg, entryType, rawData);
				return;
			}
			string text = string.Concat(new object[]
			{
				"INSERT INTO [t_s_wglog]( [f_EventType], [f_EventDesc], [f_UserID], [f_UserName])  VALUES( ",
				wgTools.PrepareStr(entryType),
				",",
				wgTools.PrepareStr(strMsg),
				",",
				icOperator.OperatorID,
				",",
				wgTools.PrepareStr(icOperator.OperatorName),
				")"
			});
			try
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (!string.IsNullOrEmpty(wgAppConfig.dbConString))
					{
						using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
						{
							using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
							{
								if (sqlConnection.State != ConnectionState.Open)
								{
									sqlConnection.Open();
								}
								sqlCommand.ExecuteNonQuery();
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public static void wgDBLog_Acc(string strMsg, EventLogEntryType entryType, byte[] rawData)
		{
			if (!wgAppConfig.IsAccessDB)
			{
				return;
			}
			string text = string.Concat(new object[]
			{
				"INSERT INTO [t_s_wglog]( [f_EventType], [f_EventDesc], [f_UserID], [f_UserName])  VALUES( ",
				wgTools.PrepareStr(entryType),
				",",
				wgTools.PrepareStr(strMsg),
				",",
				icOperator.OperatorID,
				",",
				wgTools.PrepareStr(icOperator.OperatorName),
				")"
			});
			try
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (!string.IsNullOrEmpty(wgAppConfig.dbConString))
					{
						using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
						{
							using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
							{
								if (oleDbConnection.State != ConnectionState.Open)
								{
									oleDbConnection.Open();
								}
								oleDbCommand.ExecuteNonQuery();
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public static void wgLog(string strMsg, EventLogEntryType entryType, byte[] rawData)
		{
			try
			{
				string text = string.Concat(new object[]
				{
					icOperator.OperatorID,
					".",
					icOperator.OperatorName,
					".",
					strMsg
				});
				text = DateTime.Now.ToString("yyyy-MM-dd H-mm-ss") + "\t" + text;
				if (rawData != null)
				{
					text = text + "\t:" + Encoding.ASCII.GetString(rawData);
				}
				using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\n3k_log.log", true))
				{
					streamWriter.WriteLine(text);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				string text = string.Concat(new object[]
				{
					icOperator.OperatorID,
					".",
					icOperator.OperatorName,
					".",
					strMsg
				});
				wgAppConfig.wgDBLog(text, entryType, rawData);
			}
			catch (Exception)
			{
			}
		}

		public static void wglogRecEventOfController(string strMsg)
		{
			wgAppConfig.wglogRecEventOfController(strMsg, EventLogEntryType.Information, null);
		}

		public static void wglogRecEventOfController(string strMsg, EventLogEntryType entryType, byte[] rawData)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\n3k_rec.log", true))
				{
					streamWriter.WriteLine(strMsg);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void wgDebugWrite(string info)
		{
			wgAppConfig.wgLogWithoutDB(info, EventLogEntryType.Information, null);
		}

		public static void wgDebugWrite(string strMsg, EventLogEntryType entryType)
		{
			wgAppConfig.wgLogWithoutDB(strMsg, entryType, null);
		}

		public static void writeLine(string info)
		{
			wgAppConfig.dtLast = DateTime.Now;
		}

		public static void setDisplayFormatDate(DateTimePicker dtp, string displayformat)
		{
			try
			{
				if (string.IsNullOrEmpty(displayformat))
				{
					dtp.Format = DateTimePickerFormat.Long;
				}
				else
				{
					dtp.Format = DateTimePickerFormat.Custom;
					dtp.CustomFormat = displayformat;
				}
			}
			catch (Exception)
			{
			}
		}

		public static void setDisplayFormatDate(DataGridView dgv, string columnname, string displayformat)
		{
			try
			{
				if (!string.IsNullOrEmpty(displayformat) && !string.IsNullOrEmpty(columnname))
				{
					dgv.Columns[columnname].DefaultCellStyle.Format = displayformat;
				}
			}
			catch (Exception)
			{
			}
		}

		public static void SaveDGVStyle(Form form, DataGridView dgv)
		{
			try
			{
				string text = Application.StartupPath + "\\PHOTO\\";
				string path;
				if (wgAppConfig.CultureInfoStr == "")
				{
					path = string.Format("{0}{1}_{2}.xml", text, form.Name, dgv.Name);
				}
				else
				{
					path = string.Format("{0}{1}_{2}.{3}.xml", new object[]
					{
						text,
						form.Name,
						dgv.Name,
						wgAppConfig.CultureInfoStr
					});
				}
				DataSet dataSet = new DataSet("DGV_STILE");
				DataTable dataTable = new DataTable();
				dataSet.Tables.Add(dataTable);
				dataTable.TableName = dgv.Name;
				dataTable.Columns.Add("colName");
				dataTable.Columns.Add("colHeader");
				dataTable.Columns.Add("colWidth");
				dataTable.Columns.Add("colVisable");
				dataTable.Columns.Add("colDisplayIndex");
				foreach (DataGridViewColumn dataGridViewColumn in dgv.Columns)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["colName"] = dataGridViewColumn.Name;
					dataRow["colHeader"] = dataGridViewColumn.HeaderText;
					dataRow["colWidth"] = dataGridViewColumn.Width;
					dataRow["colVisable"] = dataGridViewColumn.Visible;
					dataRow["colDisplayIndex"] = dataGridViewColumn.DisplayIndex;
					dataTable.Rows.Add(dataRow);
					dataTable.AcceptChanges();
				}
				StringWriter stringWriter = new StringWriter();
				stringWriter = new StringWriter();
				dataTable.WriteXml(stringWriter, XmlWriteMode.WriteSchema, true);
				using (StreamWriter streamWriter = new StreamWriter(path, false))
				{
					streamWriter.Write(stringWriter.ToString());
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgDebugWrite(ex.ToString());
			}
		}

		public static void ReadGVStyle(Form form, DataGridView dgv)
		{
			try
			{
				if (form != null && dgv != null)
				{
					string text = Application.StartupPath + "\\PHOTO\\";
					string text2 = string.Concat(new string[]
					{
						text,
						form.Name,
						"_",
						dgv.Name,
						".xml"
					});
					if (wgAppConfig.CultureInfoStr == "")
					{
						text2 = string.Format("{0}{1}_{2}.xml", text, form.Name, dgv.Name);
					}
					else
					{
						text2 = string.Format("{0}{1}_{2}.{3}.xml", new object[]
						{
							text,
							form.Name,
							dgv.Name,
							wgAppConfig.CultureInfoStr
						});
					}
					if (File.Exists(text2))
					{
						using (DataTable dataTable = new DataTable())
						{
							dataTable.TableName = dgv.Name;
							dataTable.Columns.Add("colName");
							dataTable.Columns.Add("colHeader");
							dataTable.Columns.Add("colWidth");
							dataTable.Columns.Add("colVisable");
							dataTable.Columns.Add("colDisplayIndex");
							dataTable.ReadXml(text2);
							foreach (DataRow dataRow in dataTable.Rows)
							{
								dgv.Columns[dataRow["colName"].ToString()].HeaderText = dataRow["colHeader"].ToString();
								dgv.Columns[dataRow["colName"].ToString()].Width = int.Parse(dataRow["colWidth"].ToString());
								dgv.Columns[dataRow["colName"].ToString()].Visible = bool.Parse(dataRow["colVisable"].ToString());
								dgv.Columns[dataRow["colName"].ToString()].DisplayIndex = int.Parse(dataRow["colDisplayIndex"].ToString());
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.writeLine(ex.ToString());
			}
		}

		public static void RestoreGVStyle(Form form, DataGridView dgv)
		{
			try
			{
				string text = Application.StartupPath + "\\PHOTO\\";
				string path = string.Concat(new string[]
				{
					text,
					form.Name,
					"_",
					dgv.Name,
					".xml"
				});
				if (wgAppConfig.CultureInfoStr == "")
				{
					path = string.Format("{0}{1}_{2}.xml", text, form.Name, dgv.Name);
				}
				else
				{
					path = string.Format("{0}{1}_{2}.{3}.xml", new object[]
					{
						text,
						form.Name,
						dgv.Name,
						wgAppConfig.CultureInfoStr
					});
				}
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgDebugWrite(ex.ToString());
			}
		}

		public static void ShowMyImage(string fileToDisplay, ref Image img)
		{
			try
			{
				wgAppConfig.DisposeImageRef(ref img);
				if (!string.IsNullOrEmpty(fileToDisplay) && wgAppConfig.FileIsExisted(fileToDisplay))
				{
					using (FileStream fileStream = new FileStream(fileToDisplay, FileMode.Open, FileAccess.Read))
					{
						byte[] buffer = new byte[fileStream.Length];
						fileStream.Read(buffer, 0, (int)fileStream.Length);
						using (MemoryStream memoryStream = new MemoryStream(buffer))
						{
							img = Image.FromStream(memoryStream);
						}
					}
				}
			}
			catch
			{
			}
		}

		public static void DisposeImageRef(ref Image img)
		{
			try
			{
				if (img != null)
				{
					img.Dispose();
					img = null;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public static void DisposeImage(Image img)
		{
			try
			{
				if (img != null)
				{
					img.Dispose();
					img = null;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public static void GetAppIcon(ref Icon appicon)
		{
			try
			{
				if (wgAppConfig.currenAppIcon == null)
				{
					wgAppConfig.currenAppIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
				}
				appicon = wgAppConfig.currenAppIcon;
			}
			catch
			{
			}
		}

		public static bool CreateCustXml()
		{
			string startupPath = Application.StartupPath;
			string path = startupPath + "\\n3k_cust.xml";
			if (File.Exists(path))
			{
				wgAppConfig.tryCreateCnt = 0;
				return true;
			}
			if (wgAppConfig.tryCreateCnt > 5)
			{
				return false;
			}
			string path2 = startupPath + "\\photo\\n3k_cust.xmlAA";
			string text = wgAppConfig.defaultCustConfigzhCHS;
			if (!wgAppConfig.IsChineseSet(Thread.CurrentThread.CurrentUICulture.Name))
			{
				text = text.Replace("zh-CHS", "en");
			}
			if (File.Exists(path2))
			{
				using (StreamReader streamReader = new StreamReader(path2))
				{
					string text2 = streamReader.ReadToEnd();
					if (text2.Length > 1000)
					{
						text = text2;
					}
				}
			}
			using (StreamWriter streamWriter = new StreamWriter(path, false))
			{
				streamWriter.WriteLine(text);
			}
			wgAppConfig.tryCreateCnt++;
			if (File.Exists(path))
			{
				wgAppConfig.tryCreateCnt = 0;
				return true;
			}
			return false;
		}

		public static string GetKeyVal(string key)
		{
			string result = "";
			try
			{
				string startupPath = Application.StartupPath;
				string text = startupPath + "\\n3k_cust.xml";
				if (!File.Exists(text))
				{
					wgAppConfig.CreateCustXml();
				}
				if (File.Exists(text))
				{
					using (DataTable dataTable = new DataTable())
					{
						dataTable.TableName = "appSettings";
						dataTable.Columns.Add("key");
						dataTable.Columns.Add("value");
						dataTable.ReadXml(text);
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (dataRow["key"].ToString() == key)
							{
								result = dataRow["value"].ToString();
								break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgDebugWrite(ex.ToString());
			}
			return result;
		}

		public static void SaveNewXmlFile(string key, string value)
		{
			string startupPath = Application.StartupPath;
			string text = startupPath + "\\n3k_cust.xmlAA";
			string path = startupPath + "\\photo\\n3k_cust.xmlAA";
			string text2 = wgAppConfig.defaultCustConfigzhCHS;
			if (!wgAppConfig.IsChineseSet(Thread.CurrentThread.CurrentUICulture.Name))
			{
				text2 = text2.Replace("zh-CHS", "en");
			}
			if (File.Exists(path))
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					string text3 = streamReader.ReadToEnd();
					if (text3.Length > 1000)
					{
						text2 = text3;
					}
				}
			}
			using (StreamWriter streamWriter = new StreamWriter(text, false))
			{
				streamWriter.WriteLine(text2);
			}
			if (File.Exists(text))
			{
				wgAppConfig.UpdateKeyVal(key, value, text);
			}
		}

		public static void UpdateKeyVal(string key, string value)
		{
			wgAppConfig.UpdateKeyVal(key, value, "");
		}

		public static void UpdateKeyVal(string key, string value, string xmlfileName)
		{
			bool flag = false;
			try
			{
				string startupPath = Application.StartupPath;
				string text = startupPath + "\\n3k_cust.xml";
				if (!string.IsNullOrEmpty(xmlfileName))
				{
					text = xmlfileName;
				}
				if (!File.Exists(text))
				{
					wgAppConfig.CreateCustXml();
				}
				if (File.Exists(text))
				{
					using (DataTable dataTable = new DataTable())
					{
						dataTable.TableName = "appSettings";
						dataTable.Columns.Add("key");
						dataTable.Columns.Add("value");
						dataTable.ReadXml(text);
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (dataRow["key"].ToString() == key)
							{
								if (value == dataRow["value"].ToString())
								{
									return;
								}
								dataRow["value"] = value;
								dataTable.AcceptChanges();
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							DataRow dataRow2 = dataTable.NewRow();
							dataRow2["key"] = key;
							dataRow2["value"] = value;
							dataTable.Rows.Add(dataRow2);
							dataTable.AcceptChanges();
						}
						using (StringWriter stringWriter = new StringWriter())
						{
							using (StreamWriter streamWriter = new StreamWriter(text, false))
							{
								dataTable.WriteXml(stringWriter, XmlWriteMode.WriteSchema, true);
								streamWriter.Write(stringWriter.ToString());
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgDebugWrite(ex.ToString());
			}
		}

		public static void InsertKeyVal(string key, string value)
		{
			bool flag = false;
			try
			{
				string startupPath = Application.StartupPath;
				string text = startupPath + "\\n3k_cust.xml";
				if (File.Exists(text))
				{
					using (DataTable dataTable = new DataTable())
					{
						dataTable.TableName = "appSettings";
						dataTable.Columns.Add("key");
						dataTable.Columns.Add("value");
						dataTable.ReadXml(text);
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (dataRow["key"].ToString() == key)
							{
								return;
							}
						}
						if (!flag)
						{
							DataRow dataRow2 = dataTable.NewRow();
							dataRow2["key"] = key;
							dataRow2["value"] = value;
							dataTable.Rows.Add(dataRow2);
							dataTable.AcceptChanges();
						}
						using (StringWriter stringWriter = new StringWriter())
						{
							using (StreamWriter streamWriter = new StreamWriter(text, false))
							{
								dataTable.WriteXml(stringWriter, XmlWriteMode.WriteSchema, true);
								streamWriter.Write(stringWriter.ToString());
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgDebugWrite(ex.ToString());
			}
		}

		public static bool FileIsExisted(string strFileName)
		{
			bool flag = false;
			try
			{
				if (string.IsNullOrEmpty(strFileName))
				{
					bool result = flag;
					return result;
				}
				if (string.IsNullOrEmpty(strFileName.Trim()))
				{
					bool result = flag;
					return result;
				}
				string text = strFileName.Trim();
				if (text.Length > 2 && !(text.Substring(0, 2) == "\\\\") && text.IndexOf(":") <= 0)
				{
					text = Application.StartupPath + "\\" + text;
				}
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Exists)
				{
					if (fileInfo.Extension.ToUpper() == ".JPG" || fileInfo.Extension.ToUpper() == ".BMP")
					{
						if (fileInfo.Length > 2048L)
						{
							flag = true;
						}
					}
					else if (fileInfo.Extension.ToUpper() == ".MP4")
					{
						if (fileInfo.Length > 10240L)
						{
							flag = true;
						}
					}
					else if (fileInfo.Length > 0L)
					{
						flag = true;
					}
				}
			}
			catch (Exception)
			{
			}
			return flag;
		}

		public static string getPhotoFileName(long cardno)
		{
			string result = "";
			try
			{
				if (cardno != 0L)
				{
					if (wgAppConfig.arrPhotoFileFullNames.Count <= 0 || wgAppConfig.lastPhotoDirectoryName != wgAppConfig.Path4Photo())
					{
						if (!wgAppConfig.DirectoryIsExisted(wgAppConfig.Path4Photo()))
						{
							return result;
						}
						wgAppConfig.lastPhotoDirectoryName = wgAppConfig.Path4Photo();
					}
					DirectoryInfo directoryInfo = new DirectoryInfo(wgAppConfig.Path4Photo());
					if (!(wgAppConfig.photoDirectoryLastWriteTime == directoryInfo.LastWriteTime) || wgAppConfig.arrPhotoFileFullNames.Count <= 0 || directoryInfo.GetFiles().Length != wgAppConfig.photoDirectoryLastFileCount)
					{
						wgAppConfig.arrPhotoFileFullNames.Clear();
						FileInfo[] files = directoryInfo.GetFiles();
						for (int i = 0; i < files.Length; i++)
						{
							FileInfo fileInfo = files[i];
							wgAppConfig.arrPhotoFileFullNames.Add(fileInfo.FullName);
						}
						wgAppConfig.photoDirectoryLastWriteTime = directoryInfo.LastWriteTime;
						wgAppConfig.photoDirectoryLastFileCount = directoryInfo.GetFiles().Length;
					}
					for (int j = cardno.ToString().Length; j <= 10; j++)
					{
						string text = wgAppConfig.Path4Photo() + cardno.ToString().PadLeft(j, '0') + ".jpg";
						if (wgAppConfig.arrPhotoFileFullNames.IndexOf(text) >= 0)
						{
							result = text;
							break;
						}
						text = text.ToLower(new CultureInfo("en-US", false)).Replace(".jpg", ".bmp");
						if (wgAppConfig.arrPhotoFileFullNames.IndexOf(text) >= 0)
						{
							result = text;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.arrPhotoFileFullNames.Clear();
				wgAppConfig.photoDirectoryLastWriteTime = DateTime.Parse("2012-4-10 09:08:50.531");
				wgAppConfig.photoDirectoryLastFileCount = -1;
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static void CreatePhotoDirectoryRealize()
		{
			try
			{
				Directory.CreateDirectory(wgAppConfig.m_PhotoDiriectyName);
				wgAppConfig.m_bCreatePhotoDirectory = true;
			}
			catch (Exception)
			{
			}
		}

		public static bool CreatePhotoDirectory(string strFileName)
		{
			try
			{
				wgAppConfig.m_PhotoDiriectyName = strFileName;
				wgAppConfig.m_bCreatePhotoDirectory = false;
				Thread thread = new Thread(new ThreadStart(wgAppConfig.CreatePhotoDirectoryRealize));
				thread.Name = "CreatePhotoDirectoryRealize";
				thread.Start();
				long ticks = DateTime.Now.Ticks;
				while (DateTime.Now.Ticks - ticks < 1000000L && !wgAppConfig.m_bCreatePhotoDirectory)
				{
				}
				if (thread.IsAlive)
				{
					thread.Abort();
				}
			}
			catch (Exception)
			{
			}
			return wgAppConfig.m_bCreatePhotoDirectory;
		}

		private static void DirectoryIsExistedNetShare()
		{
			try
			{
				if (Directory.Exists(wgAppConfig.m_DirectoryNetShare))
				{
					wgAppConfig.m_bFindDirectoryNetShare = true;
				}
			}
			catch (Exception)
			{
			}
		}

		public static bool DirectoryIsExistedWithNetShare(string strFileName)
		{
			bool flag = false;
			try
			{
				string directoryNetShare = strFileName.Trim();
				wgAppConfig.m_bFindDirectoryNetShare = false;
				wgAppConfig.m_DirectoryNetShare = directoryNetShare;
				try
				{
					Thread thread = new Thread(new ThreadStart(wgAppConfig.DirectoryIsExistedNetShare));
					thread.Name = "DirectoryIsExistedNetShare";
					thread.Start();
					long ticks = DateTime.Now.Ticks;
					while (DateTime.Now.Ticks - ticks < 1000000L)
					{
						if (wgAppConfig.m_bFindDirectoryNetShare)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						wgTools.WriteLine("DirectoryIsExistedNetShare  Not Found");
					}
					if (thread.IsAlive)
					{
						thread.Abort();
					}
				}
				catch (Exception)
				{
				}
			}
			catch (Exception)
			{
			}
			return flag;
		}

		public static bool DirectoryIsExisted(string strFileName)
		{
			bool flag = false;
			try
			{
				if (string.IsNullOrEmpty(strFileName))
				{
					bool result = flag;
					return result;
				}
				if (string.IsNullOrEmpty(strFileName.Trim()))
				{
					bool result = flag;
					return result;
				}
				string text = strFileName.Trim();
				if (text.Length > 2)
				{
					if (text.Substring(0, 2) == "\\\\")
					{
						bool result = wgAppConfig.DirectoryIsExistedWithNetShare(text);
						return result;
					}
					if (text.IndexOf(":") <= 0)
					{
						text = Application.StartupPath + "\\" + text;
					}
				}
				if (Directory.Exists(text))
				{
					flag = true;
				}
			}
			catch (Exception)
			{
			}
			return flag;
		}

		public static bool getParamValBoolByNO(int NO)
		{
			string systemParamByNO = wgAppConfig.getSystemParamByNO(NO);
			int num;
			return !string.IsNullOrEmpty(systemParamByNO) && (int.TryParse(systemParamByNO, out num) && num > 0);
		}

		public static string getSystemParamByNO(int parNo)
		{
			return wgAppConfig.getSystemParam(parNo, "");
		}

		public static string getSystemParamByName(string parName)
		{
			return wgAppConfig.getSystemParam(-1, parName);
		}

		private static string getSystemParam(int parNo, string parName)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.getSystemParam_Acc(parNo, parName);
			}
			string result = "";
			try
			{
				string cmdText;
				if (string.IsNullOrEmpty(parName))
				{
					cmdText = "SELECT f_Value FROM t_a_SystemParam WHERE f_NO=" + parNo.ToString();
				}
				else
				{
					cmdText = "SELECT f_Value FROM t_a_SystemParam WHERE f_Name=" + wgTools.PrepareStr(parName);
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						result = wgTools.SetObjToStr(sqlCommand.ExecuteScalar());
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		private static string getSystemParam_Acc(int parNo, string parName)
		{
			string result = "";
			try
			{
				string cmdText;
				if (string.IsNullOrEmpty(parName))
				{
					cmdText = "SELECT f_Value FROM t_a_SystemParam WHERE f_NO=" + parNo.ToString();
				}
				else
				{
					cmdText = "SELECT f_Value FROM t_a_SystemParam WHERE f_Name=" + wgTools.PrepareStr(parName);
				}
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						result = wgTools.SetObjToStr(oleDbCommand.ExecuteScalar());
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int getSystemParamValue(int NO, out string EName, out string value, out string notes)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.getSystemParamValue_Acc(NO, out EName, out value, out notes);
			}
			int result = -9;
			EName = null;
			value = null;
			notes = null;
			string cmdText = "SELECT * FROM t_a_SystemParam WHERE f_NO = " + NO.ToString();
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						EName = (sqlDataReader["f_EName"] as string);
						value = (sqlDataReader["f_Value"] as string);
						notes = (sqlDataReader["f_Notes"] as string);
						result = 1;
					}
					sqlDataReader.Close();
				}
			}
			return result;
		}

		public static int getSystemParamValue_Acc(int NO, out string EName, out string value, out string notes)
		{
			int result = -9;
			EName = null;
			value = null;
			notes = null;
			string cmdText = "SELECT * FROM t_a_SystemParam WHERE f_NO = " + NO.ToString();
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						EName = (oleDbDataReader["f_EName"] as string);
						value = (oleDbDataReader["f_Value"] as string);
						notes = (oleDbDataReader["f_Notes"] as string);
						result = 1;
					}
					oleDbDataReader.Close();
				}
			}
			return result;
		}

		public static int setSystemParamValue(int NO, string EName, string value, string notes)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.setSystemParamValue_Acc(NO, EName, value, notes);
			}
			int result = -9;
			try
			{
				string text = "UPDATE t_a_SystemParam SET [f_Value] = " + wgTools.PrepareStr(value);
				if (!string.IsNullOrEmpty(EName))
				{
					text = text + ", [f_EName] = " + wgTools.PrepareStr(EName);
				}
				if (!string.IsNullOrEmpty(notes))
				{
					text = text + ", [f_Notes] = " + wgTools.PrepareStr(notes);
				}
				text = text + " WHERE f_NO = " + NO.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int setSystemParamValue_Acc(int NO, string EName, string value, string notes)
		{
			int result = -9;
			if (!wgAppConfig.IsAccessDB)
			{
				return result;
			}
			try
			{
				string text = "UPDATE t_a_SystemParam SET [f_Value] = " + wgTools.PrepareStr(value);
				if (!string.IsNullOrEmpty(EName))
				{
					text = text + ", [f_EName] = " + wgTools.PrepareStr(EName);
				}
				if (!string.IsNullOrEmpty(notes))
				{
					text = text + ", [f_Notes] = " + wgTools.PrepareStr(notes);
				}
				text = text + " WHERE f_NO = " + NO.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.ExecuteNonQuery();
						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int setSystemParamValueBool(int NO, bool value)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.setSystemParamValueBool_Acc(NO, value);
			}
			int result = -9;
			try
			{
				string text = "UPDATE t_a_SystemParam SET [f_Value] = " + (value ? "1" : "0");
				text = text + " WHERE f_NO = " + NO.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int setSystemParamValueBool_Acc(int NO, bool value)
		{
			int result = -9;
			try
			{
				string text = "UPDATE t_a_SystemParam SET [f_Value] = " + (value ? "1" : "0");
				text = text + " WHERE f_NO = " + NO.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.ExecuteNonQuery();
						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int setSystemParamValue(int NO, string value)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgAppConfig.setSystemParamValue_Acc(NO, value);
			}
			int result = -9;
			try
			{
				string text = "UPDATE t_a_SystemParam SET [f_Value] = " + wgTools.PrepareStr(value);
				text = text + " WHERE f_NO = " + NO.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static int setSystemParamValue_Acc(int NO, string value)
		{
			int result = -9;
			try
			{
				string text = "UPDATE t_a_SystemParam SET [f_Value] = " + wgTools.PrepareStr(value);
				text = text + " WHERE f_NO = " + NO.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.ExecuteNonQuery();
						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static void CardIDInput(ref MaskedTextBox mtb)
		{
			if (mtb.Text.Length != mtb.Text.Trim().Length)
			{
				mtb.Text = mtb.Text.Trim();
			}
			else if (mtb.Text.Length == 0 && mtb.SelectionStart != 0)
			{
				mtb.SelectionStart = 0;
			}
			if (mtb.Text.Length > 0)
			{
				if (mtb.Text.IndexOf(" ") > 0)
				{
					mtb.Text = mtb.Text.Replace(" ", "");
				}
				if (mtb.Text.Length > 9 && long.Parse(mtb.Text) >= (long)((ulong)-1))
				{
					mtb.Text = mtb.Text.Substring(0, mtb.Text.Length - 1);
				}
			}
		}

		public static void fillDGVData(ref DataGridView dgv, string strSql)
		{
			if (wgAppConfig.IsAccessDB)
			{
				wgAppConfig.fillDGVData_Acc(ref dgv, strSql);
				return;
			}
			wgAppConfig.tb = new DataTable();
			wgAppConfig.dv = new DataView(wgAppConfig.tb);
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(wgAppConfig.tb);
					}
				}
			}
			dgv.AutoGenerateColumns = false;
			dgv.DataSource = wgAppConfig.dv;
			int num = 0;
			while (num < wgAppConfig.dv.Table.Columns.Count && num < dgv.ColumnCount)
			{
				dgv.Columns[num].DataPropertyName = wgAppConfig.dv.Table.Columns[num].ColumnName;
				num++;
			}
		}

		public static void fillDGVData_Acc(ref DataGridView dgv, string strSql)
		{
			wgAppConfig.tb = new DataTable();
			wgAppConfig.dv = new DataView(wgAppConfig.tb);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						oleDbDataAdapter.Fill(wgAppConfig.tb);
					}
				}
			}
			dgv.AutoGenerateColumns = false;
			dgv.DataSource = wgAppConfig.dv;
			int num = 0;
			while (num < wgAppConfig.dv.Table.Columns.Count && num < dgv.ColumnCount)
			{
				dgv.Columns[num].DataPropertyName = wgAppConfig.dv.Table.Columns[num].ColumnName;
				num++;
			}
		}

		public static void selectObject(DataGridView dgv)
		{
			wgAppConfig.selectObject(dgv, "", "");
		}

		public static void selectObject(DataGridView dgv, string secondField, string val)
		{
			try
			{
				int index;
				if (dgv.SelectedRows.Count <= 0)
				{
					if (dgv.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dgv.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dgv.SelectedRows[0].Index;
				}
				using (DataTable table = ((DataView)dgv.DataSource).Table)
				{
					if (dgv.SelectedRows.Count > 0)
					{
						int count = dgv.SelectedRows.Count;
						int[] array = new int[count];
						for (int i = 0; i < dgv.SelectedRows.Count; i++)
						{
							array[i] = (int)dgv.SelectedRows[i].Cells[0].Value;
						}
						for (int j = 0; j < count; j++)
						{
							int num = array[j];
							DataRow dataRow = table.Rows.Find(num);
							if (dataRow != null)
							{
								dataRow["f_Selected"] = 1;
								if (secondField != "")
								{
									dataRow[secondField] = val;
								}
							}
						}
					}
					else
					{
						int num2 = (int)dgv.Rows[index].Cells[0].Value;
						DataRow dataRow = table.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = 1;
							if (secondField != "")
							{
								dataRow[secondField] = val;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void selectObject(DataGridView dgv, int iSelectedCurrentNoneMax)
		{
			wgAppConfig.selectObject(dgv, "", "", iSelectedCurrentNoneMax);
		}

		public static void selectObject(DataGridView dgv, string secondField, string val, int iSelectedCurrentNoneMax)
		{
			try
			{
				int index;
				if (dgv.SelectedRows.Count <= 0)
				{
					if (dgv.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dgv.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dgv.SelectedRows[0].Index;
				}
				using (DataTable table = ((DataView)dgv.DataSource).Table)
				{
					if (dgv.SelectedRows.Count > 0)
					{
						int count = dgv.SelectedRows.Count;
						int[] array = new int[count];
						for (int i = 0; i < dgv.SelectedRows.Count; i++)
						{
							array[i] = (int)dgv.SelectedRows[i].Cells[0].Value;
						}
						for (int j = 0; j < count; j++)
						{
							int num = array[j];
							DataRow dataRow = table.Rows.Find(num);
							if (dataRow != null)
							{
								dataRow["f_Selected"] = iSelectedCurrentNoneMax + 1;
								if (secondField != "")
								{
									dataRow[secondField] = val;
								}
							}
						}
					}
					else
					{
						int num2 = (int)dgv.Rows[index].Cells[0].Value;
						DataRow dataRow = table.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = iSelectedCurrentNoneMax + 1;
							if (secondField != "")
							{
								dataRow[secondField] = val;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void deselectObject(DataGridView dgv)
		{
			try
			{
				int index;
				if (dgv.SelectedRows.Count <= 0)
				{
					if (dgv.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dgv.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dgv.SelectedRows[0].Index;
				}
				using (DataTable table = ((DataView)dgv.DataSource).Table)
				{
					if (dgv.SelectedRows.Count > 0)
					{
						int count = dgv.SelectedRows.Count;
						int[] array = new int[count];
						for (int i = 0; i < dgv.SelectedRows.Count; i++)
						{
							array[i] = (int)dgv.SelectedRows[i].Cells[0].Value;
						}
						for (int j = 0; j < count; j++)
						{
							int num = array[j];
							DataRow dataRow = table.Rows.Find(num);
							if (dataRow != null)
							{
								dataRow["f_Selected"] = 0;
							}
						}
					}
					else
					{
						int num2 = (int)dgv.Rows[index].Cells[0].Value;
						DataRow dataRow = table.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = 0;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void deselectObject(DataGridView dgv, int iSelectedCurrentNoneMax)
		{
			try
			{
				int index;
				if (dgv.SelectedRows.Count <= 0)
				{
					if (dgv.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dgv.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dgv.SelectedRows[0].Index;
				}
				using (DataTable table = ((DataView)dgv.DataSource).Table)
				{
					if (dgv.SelectedRows.Count > 0)
					{
						int count = dgv.SelectedRows.Count;
						int[] array = new int[count];
						for (int i = 0; i < dgv.SelectedRows.Count; i++)
						{
							array[i] = (int)dgv.SelectedRows[i].Cells[0].Value;
						}
						for (int j = 0; j < count; j++)
						{
							int num = array[j];
							DataRow dataRow = table.Rows.Find(num);
							if (dataRow != null)
							{
								dataRow["f_Selected"] = iSelectedCurrentNoneMax;
							}
						}
					}
					else
					{
						int num2 = (int)dgv.Rows[index].Cells[0].Value;
						DataRow dataRow = table.Rows.Find(num2);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = iSelectedCurrentNoneMax;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void printdgv(DataGridView dv, string Title)
		{
			using (DGVPrinter dGVPrinter = new DGVPrinter())
			{
				if (!string.IsNullOrEmpty(Title))
				{
					dGVPrinter.Title = Title;
				}
				dGVPrinter.PageNumbers = true;
				dGVPrinter.PageNumberInHeader = false;
				dGVPrinter.PorportionalColumns = true;
				dGVPrinter.HeaderCellAlignment = StringAlignment.Near;
				dGVPrinter.PrintDataGridView(dv);
			}
		}

		public static bool exportToExcel(DataGridView dgv, string formText)
		{
			string text = "";
			try
			{
				string fileName;
				if (string.IsNullOrEmpty(formText))
				{
					fileName = DateTime.Now.ToString("yyyy-MM-dd_HHmmss_ff") + ".xls";
				}
				else
				{
					fileName = formText + DateTime.Now.ToString("-yyyy-MM-dd_HHmmss_ff") + ".xls";
				}
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.FileName = fileName;
					saveFileDialog.Filter = " (*.xls)|*.xls";
					bool result;
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						text = saveFileDialog.FileName;
						using (ExcelObject excelObject = new ExcelObject(text))
						{
							int num = 0;
							using (dfrmWait dfrmWait = new dfrmWait())
							{
								dfrmWait.Show();
								dfrmWait.Refresh();
								excelObject.WriteTable(dgv);
								foreach (DataGridViewRow dgvdr in ((IEnumerable)dgv.Rows))
								{
									excelObject.AddNewRow(dgvdr, dgv);
									num++;
									if (num >= 65535)
									{
										break;
									}
									if (num % 1000 == 0)
									{
										wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1:d}", CommonStr.strExportRecords, num));
										Application.DoEvents();
									}
								}
								wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1:d}", CommonStr.strExportRecords, num));
								dfrmWait.Hide();
							}
							XMessageBox.Show(string.Concat(new string[]
							{
								CommonStr.strExportRecords,
								" = ",
								num.ToString(),
								"\t",
								(num >= 65535) ? CommonStr.strExportRecordsMax : "",
								"\r\n\r\n",
								CommonStr.strExportToExcel,
								" ",
								text
							}));
							result = true;
							return result;
						}
					}
					result = false;
					return result;
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine("ExportToExcel" + text + ex.ToString());
			}
			finally
			{
				Directory.SetCurrentDirectory(Application.StartupPath);
			}
			return false;
		}

		public static bool exportToExcelSpecial(ref DataGridView dgv, string formText, bool bLoadedFinished, ref BackgroundWorker bk1, ref int startRecordIndex, int MaxRecord, string dgvSql)
		{
			DataGridView dataGridView = dgv;
			if (dataGridView.Rows.Count <= 65535 && !bLoadedFinished)
			{
				using (dfrmWait dfrmWait = new dfrmWait())
				{
					dfrmWait.Show();
					dfrmWait.Refresh();
					while (bk1.IsBusy)
					{
						Thread.Sleep(500);
						Application.DoEvents();
					}
					while (startRecordIndex <= dataGridView.Rows.Count)
					{
						startRecordIndex += MaxRecord;
						bk1.RunWorkerAsync(new object[]
						{
							startRecordIndex,
							66000 - dataGridView.Rows.Count,
							dgvSql
						});
						while (bk1.IsBusy)
						{
							Thread.Sleep(500);
							Application.DoEvents();
						}
						startRecordIndex = startRecordIndex + 66000 - dataGridView.Rows.Count - MaxRecord;
						if (dataGridView.Rows.Count > 65535)
						{
							IL_10B:
							dfrmWait.Hide();
							goto IL_11D;
						}
					}
					wgAppRunInfo.raiseAppRunInfoLoadNums(dataGridView.Rows.Count.ToString() + "#");
					goto IL_10B;
				}
			}
			IL_11D:
			wgAppConfig.exportToExcel(dataGridView, formText);
			return true;
		}

		public static string Path4Photo()
		{
			string text = Application.StartupPath + "\\PHOTO\\";
			if (!string.IsNullOrEmpty(wgAppConfig.getSystemParamByNO(41)))
			{
				text = wgAppConfig.getSystemParamByNO(41);
				if (text.Substring(text.Length - 1, 1) != "\\")
				{
					text += "\\";
				}
			}
			return text;
		}

		public static string Path4PhotoDefault()
		{
			return Application.StartupPath + "\\PHOTO\\";
		}

		public static string Path4Doc()
		{
			string text = ".\\DOC\\";
			string startupPath = Application.StartupPath;
			text = startupPath + "\\DOC\\";
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				if (!directoryInfo.Exists)
				{
					directoryInfo.Create();
				}
			}
			catch
			{
			}
			return text;
		}

		public static string weekdayToChsName(int weekDay)
		{
			string result = "";
			try
			{
				string[] array = new string[]
				{
					CommonStr.strSunday_Short,
					CommonStr.strMonday_Short,
					CommonStr.strTuesday_Short,
					CommonStr.strWednesday_Short,
					CommonStr.strThursday_Short,
					CommonStr.strFriday_Short,
					CommonStr.strSaturday_Short
				};
				if (weekDay >= 0 && weekDay <= 6)
				{
					result = array[weekDay];
				}
			}
			catch
			{
			}
			return result;
		}

		public static void CustConfigureInit()
		{
			wgAppConfig.InsertKeyVal("autologinName", "");
			wgAppConfig.InsertKeyVal("autologinPassword", "");
			wgAppConfig.InsertKeyVal("rgtries", "1234");
			wgAppConfig.InsertKeyVal("CommCurrent", "");
			wgAppConfig.InsertKeyVal("EMapZoomInfo", "");
			wgAppConfig.InsertKeyVal("EMapLocInfo", "");
			wgAppConfig.InsertKeyVal("NewSoftwareVersionInfo", Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")));
			if (wgAppConfig.GetKeyVal("NewSoftwareVersionInfo") != Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")))
			{
				wgAppConfig.UpdateKeyVal("NewSoftwareVersionInfo", Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")));
			}
			wgAppConfig.InsertKeyVal("RunTimes", "0");
			try
			{
				wgAppConfig.UpdateKeyVal("RunTimes", wgAppConfig.GetKeyVal("RunTimes") + 1);
			}
			catch
			{
			}
			wgAppConfig.InsertKeyVal("RunTimeAt", "0");
			wgAppConfig.InsertKeyVal("NewSoftwareSpecialVersionInfo", "");
			if (wgAppConfig.GetKeyVal("NewSoftwareSpecialVersionInfo") != Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")))
			{
				wgAppConfig.UpdateKeyVal("NewSoftwareSpecialVersionInfo", Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf(".")));
			}
			if (int.Parse(wgAppConfig.GetKeyVal("RunTimeAt")) >= 0)
			{
				DateTime dateTime = DateTime.Parse("2010-5-1");
				if (DateTime.Now.Date >= dateTime.Date && int.Parse(wgAppConfig.GetKeyVal("RunTimeAt")) != 0 && !(DateTime.Now.Date >= dateTime.AddDays((double)int.Parse(wgAppConfig.GetKeyVal("RunTimeAt"))).Date) && DateTime.Now.AddDays(32.0).Date <= dateTime.AddDays((double)int.Parse(wgAppConfig.GetKeyVal("RunTimeAt"))).Date)
				{
				}
			}
			else
			{
				wgAppConfig.UpdateKeyVal("RunTimeAt", "0");
			}
			wgAppConfig.InsertKeyVal("DisplayFormat_DateYMD", "");
			wgAppConfig.InsertKeyVal("DisplayFormat_DateYMDWeek", "");
			wgAppConfig.InsertKeyVal("DisplayFormat_DateYMDHMS", "");
			wgAppConfig.InsertKeyVal("DisplayFormat_DateYMDHMSWeek", "");
		}

		public static string ReplaceFloorRomm(string info)
		{
			string text = info;
			try
			{
				if (wgAppConfig.bFloorRoomManager)
				{
					text = info.Replace(CommonStr.strReplaceDepartment, CommonStr.strReplaceFloorRoom);
					if (text == CommonStr.strReplaceDepartment2)
					{
						text = text.Replace(CommonStr.strReplaceDepartment2, CommonStr.strReplaceFloorRoom);
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
			return text;
		}

		public static string ReplaceWorkNO(string info)
		{
			string result = info;
			try
			{
				if (wgAppConfig.bFloorRoomManager)
				{
					result = info.Replace(CommonStr.strReplaceWorkNO, CommonStr.strReplaceNO);
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
			return result;
		}

		public static bool IsChineseSet(string cultureInfo)
		{
			bool result = false;
			try
			{
				if (!string.IsNullOrEmpty(cultureInfo) && (cultureInfo == "zh" || cultureInfo.IndexOf("zh-") == 0))
				{
					result = true;
				}
			}
			catch
			{
			}
			return result;
		}

		public static string getSqlFindNormal(string strBaseInfo, string fromMainDt, string strTimeCon, int groupMinNO, int groupIDOfMinNO, int groupMaxNO, string findName, long findCard, int findConsumerID)
		{
			string result = "";
			try
			{
				string text = "";
				if (!string.IsNullOrEmpty(strTimeCon))
				{
					text += string.Format("AND {0}", strTimeCon);
				}
				if (findConsumerID > 0)
				{
					text += string.Format("AND   t_b_Consumer.f_ConsumerID ={0:d} ", findConsumerID);
					result = strBaseInfo + string.Format(" FROM (t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text);
					return result;
				}
				if (!string.IsNullOrEmpty(findName))
				{
					text += string.Format(" AND t_b_Consumer.f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", findName)));
				}
				if (findCard > 0L)
				{
					text += string.Format(" AND t_b_Consumer.f_CardNO ={0:d} ", findCard);
				}
				if (groupMinNO > 0)
				{
					if (groupMinNO >= groupMaxNO)
					{
						result = strBaseInfo + string.Format(" FROM (t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text, string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", groupIDOfMinNO));
					}
					else
					{
						result = strBaseInfo + string.Format(" FROM (t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text, string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", groupMinNO) + string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", groupMaxNO));
					}
				}
				else
				{
					result = strBaseInfo + string.Format(" FROM (t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static string getSqlFindPrilivege(string strBaseInfo, string fromMainDt, string strTimeCon, int groupMinNO, int groupIDOfMinNO, int groupMaxNO, string findName, long findCard, int findConsumerID)
		{
			string result = "";
			try
			{
				string text = "";
				if (!string.IsNullOrEmpty(strTimeCon))
				{
					text += string.Format("AND {0}", strTimeCon);
				}
				if (findConsumerID > 0)
				{
					text += string.Format("AND   t_b_Consumer.f_ConsumerID ={0:d} ", findConsumerID);
					result = strBaseInfo + string.Format(" FROM (t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  INNER JOIN t_b_Door  ON {0}.f_DoorID=t_b_Door.f_DoorID ", fromMainDt, text);
					return result;
				}
				if (!string.IsNullOrEmpty(findName))
				{
					text += string.Format(" AND t_b_Consumer.f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", findName)));
				}
				if (findCard > 0L)
				{
					text += string.Format(" AND t_b_Consumer.f_CardNO ={0:d} ", findCard);
				}
				if (groupMinNO > 0)
				{
					if (groupMinNO >= groupMaxNO)
					{
						result = strBaseInfo + string.Format(" FROM ((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) INNER JOIN t_b_Door ON {0}.f_DoorID=t_b_Door.f_DoorID) INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text, string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", groupIDOfMinNO));
					}
					else
					{
						result = strBaseInfo + string.Format(" FROM ((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) INNER JOIN t_b_Door ON {0}.f_DoorID=t_b_Door.f_DoorID) INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text, string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", groupMinNO) + string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", groupMaxNO));
					}
				}
				else
				{
					result = strBaseInfo + string.Format(" FROM ((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) INNER JOIN t_b_Door ON {0}.f_DoorID=t_b_Door.f_DoorID ) ", fromMainDt, text);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		public static string getSqlFindSwipeRecord(string strBaseInfo, string fromMainDt, string strTimeCon, int groupMinNO, int groupIDOfMinNO, int groupMaxNO, string findName, long findCard, int findConsumerID)
		{
			string text = "";
			try
			{
				string text2 = "";
				string text3 = " WHERE (1>0) ";
				if (!string.IsNullOrEmpty(strTimeCon))
				{
					text3 += string.Format("AND {0}", strTimeCon);
				}
				if (findConsumerID > 0)
				{
					text2 += string.Format("AND   t_b_Consumer.f_ConsumerID ={0:d} ", findConsumerID);
					text = strBaseInfo + string.Format(" FROM ((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) LEFT JOIN  t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID) ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text2);
					text += text3;
					return text;
				}
				if (!string.IsNullOrEmpty(findName))
				{
					text3 += string.Format(" AND t_b_Consumer.f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", findName)));
				}
				if (findCard > 0L)
				{
					text3 += string.Format(" AND {0}.f_CardNO ={1:d} ", fromMainDt, findCard);
				}
				if (groupMinNO > 0)
				{
					if (groupMinNO >= groupMaxNO)
					{
						text = strBaseInfo + string.Format(" FROM ((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  LEFT JOIN   t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID) )  INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text2, string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", groupIDOfMinNO));
					}
					else
					{
						text = strBaseInfo + string.Format(" FROM ((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  LEFT JOIN   t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID) )  INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text2, string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", groupMinNO) + string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", groupMaxNO));
					}
				}
				else
				{
					text = strBaseInfo + string.Format(" FROM (({0} LEFT JOIN t_b_Consumer ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  LEFT JOIN   t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID) )  LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text2);
				}
				text += text3;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return text;
		}

		public static void backupBeforeExitByJustCopy()
		{
			try
			{
				if (wgAppConfig.IsAccessDB)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(wgAppConfig.BackupDir);
					if (!directoryInfo.Exists)
					{
						directoryInfo.Create();
						directoryInfo = new DirectoryInfo(wgAppConfig.BackupDir);
					}
					if (directoryInfo.Exists)
					{
						Cursor arg_42_0 = Cursor.Current;
						Cursor.Current = Cursors.WaitCursor;
						try
						{
							string str = wgAppConfig.accessDbName + "_000.bak";
							FileInfo fileInfo = new FileInfo(wgAppConfig.BackupDir + str);
							FileInfo fileInfo2 = new FileInfo(wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_001.bak");
							FileInfo fileInfo3 = new FileInfo(wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_LASTDAY0.bak");
							FileInfo fileInfo4 = new FileInfo(wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_LASTDAY1.bak");
							FileInfo fileInfo5 = new FileInfo(string.Format(Application.StartupPath + "\\t{0}.bak", wgAppConfig.accessDbName));
							try
							{
								if (fileInfo.Exists)
								{
									fileInfo.Attributes = FileAttributes.Archive;
								}
							}
							catch (Exception ex)
							{
								wgAppConfig.wgDebugWrite(ex.ToString(), EventLogEntryType.Error);
							}
							try
							{
								if (fileInfo2.Exists)
								{
									fileInfo2.Attributes = FileAttributes.Archive;
								}
							}
							catch (Exception ex2)
							{
								wgAppConfig.wgDebugWrite(ex2.ToString(), EventLogEntryType.Error);
							}
							try
							{
								if (fileInfo.Exists)
								{
									fileInfo.Attributes = FileAttributes.Archive;
								}
							}
							catch (Exception ex3)
							{
								wgAppConfig.wgDebugWrite(ex3.ToString(), EventLogEntryType.Error);
							}
							try
							{
								if (fileInfo3.Exists)
								{
									fileInfo3.Attributes = FileAttributes.Archive;
								}
							}
							catch (Exception ex4)
							{
								wgAppConfig.wgDebugWrite(ex4.ToString(), EventLogEntryType.Error);
							}
							try
							{
								if (fileInfo4.Exists)
								{
									fileInfo4.Attributes = FileAttributes.Archive;
								}
							}
							catch (Exception ex5)
							{
								wgAppConfig.wgDebugWrite(ex5.ToString(), EventLogEntryType.Error);
							}
							try
							{
								if (fileInfo5.Exists)
								{
									fileInfo5.Attributes = FileAttributes.Archive;
								}
							}
							catch (Exception ex6)
							{
								wgAppConfig.wgDebugWrite(ex6.ToString(), EventLogEntryType.Error);
							}
							if (fileInfo.Exists)
							{
								if (fileInfo2.Exists && !(fileInfo.LastWriteTime.ToString("yyyyMMdd") == fileInfo2.LastWriteTime.ToString("yyyyMMdd")))
								{
									if (fileInfo3.Exists)
									{
										if (fileInfo4.Exists)
										{
											fileInfo4.Delete();
										}
										fileInfo3.MoveTo(wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_LASTDAY1.bak");
									}
									fileInfo2.MoveTo(wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_LASTDAY0.bak");
								}
								if (fileInfo2.FullName == wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_001.bak" && fileInfo2.Exists)
								{
									fileInfo2.Delete();
								}
								fileInfo.MoveTo(wgAppConfig.BackupDir + wgAppConfig.accessDbName + "_001.bak");
							}
							string fileName = Application.StartupPath + string.Format("\\{0}.mdb", wgAppConfig.accessDbName);
							fileInfo = new FileInfo(fileName);
							fileInfo.CopyTo(Application.StartupPath + string.Format("\\t{0}.bak", wgAppConfig.accessDbName), true);
							fileInfo.CopyTo(wgAppConfig.BackupDir + str, true);
						}
						catch (Exception ex7)
						{
							wgAppConfig.wgDebugWrite(ex7.ToString(), EventLogEntryType.Error);
						}
					}
				}
			}
			catch (Exception ex8)
			{
				wgAppConfig.wgDebugWrite(ex8.ToString(), EventLogEntryType.Error);
			}
		}
	}
}
