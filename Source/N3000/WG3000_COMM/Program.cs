using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM
{
	internal static class Program
	{
		private const string wgDatabaseDefaultNameOfAdroitor = "AccessData";

		private const string defaultDBFileName = "n3k_default.sql";

		private static Thread startSlowThread;

		public static int expcount = 0;

		public static string expStrDayHour = "";

		private static float dbVersionNewest = 75f;

		private static string g_cnStrAcc = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= {0}.mdb;User ID=admin;Password=;JET OLEDB:Database Password=168168", Application.StartupPath + "\\" + wgAppConfig.accessDbName);

		private static bool bSqlExress = false;

		public static void getNewSoftware()
		{
			try
			{
				comMjSpecialUpdate.updateMjSpecialSoftware();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		[STAThread]
		private static void Main(string[] cmdArgs)
		{
			wgTools.gPTC = "/jHWIsa9BCY8k9kJc+0XjQ==";
			wgAppConfig.ProductTypeOfApp = "AccessControl";
			Directory.SetCurrentDirectory(Application.StartupPath);
			wgAppConfig.gRestart = false;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new ThreadExceptionEventHandler(Program.GlobalExceptionHandler);
			Program.localize();
			if (!string.IsNullOrEmpty(wgTools.SetObjToStr(wgAppConfig.GetKeyVal("CommTimeoutMsMin"))))
			{
				long.TryParse(wgTools.SetObjToStr(wgAppConfig.GetKeyVal("CommTimeoutMsMin")), out wgUdpComm.CommTimeoutMsMin);
			}
			if (cmdArgs.Length == 1)
			{
				if (cmdArgs[0].ToUpper() == "-P")
				{
					wgAppConfig.dbConString = "";
					Application.Run(new frmProductFormat());
					return;
				}
				if (cmdArgs[0].ToUpper() == "-S" || cmdArgs[0].ToUpper() == "-WEB")
				{
					wgAppConfig.dbConString = "";
					dfrmNetControllerConfig dfrmNetControllerConfig = new dfrmNetControllerConfig();
					try
					{
						dfrmNetControllerConfig.btnAddToSystem.Visible = false;
						icOperator.login("wiegand", "168668");
						if (cmdArgs[0].ToUpper() == "-WEB")
						{
							dfrmNetControllerConfig.btnIPAndWebConfigure.Visible = true;
						}
						Application.Run(dfrmNetControllerConfig);
					}
					catch (Exception)
					{
					}
					dfrmNetControllerConfig.Dispose();
					return;
				}
				if (cmdArgs[0].Length > 2 && cmdArgs[0].ToUpper().Substring(0, 3) == "-CS")
				{
					wgAppConfig.dbConString = "";
					icOperator.login("wiegand", "168668");
					frmTestController frmTestController = new frmTestController();
					try
					{
						frmTestController.onlyProduce();
						Application.Run(frmTestController);
					}
					catch (Exception)
					{
					}
					frmTestController.Dispose();
					return;
				}
			}
			dfrmWait dfrmWait = new dfrmWait();
			dfrmWait.Show();
			dfrmWait.Refresh();
			if (Program.dbConnectionCheck() > 0)
			{
				wgAppConfig.CustConfigureInit();
				Program.UpgradeDatabase();
				try
				{
					string systemParamByNO = wgAppConfig.getSystemParamByNO(30);
					if (string.IsNullOrEmpty(systemParamByNO))
					{
						wgAppConfig.setSystemParamValue(30, "Application Version", Application.ProductVersion, "V9 当前使用的应用软件版本");
					}
					else if (systemParamByNO != Application.ProductVersion)
					{
						wgAppConfig.setSystemParamValue(30, "Application Version", Application.ProductVersion, "V9 当前使用的应用软件版本");
					}
				}
				catch (Exception)
				{
				}
				dfrmWait.Close();
				Application.Run(new frmLogin());
				if (wgAppConfig.IsLogin)
				{
					try
					{
						if (int.Parse(wgAppConfig.GetKeyVal("RunTimeAt")) >= 0)
						{
							DateTime dateTime = DateTime.Parse("2012-12-1");
							if (DateTime.Now.Date >= dateTime.Date && (int.Parse(wgAppConfig.GetKeyVal("RunTimeAt")) == 0 || DateTime.Now.Date >= dateTime.AddDays((double)int.Parse(wgAppConfig.GetKeyVal("RunTimeAt"))).Date || DateTime.Now.AddDays(32.0).Date <= dateTime.AddDays((double)int.Parse(wgAppConfig.GetKeyVal("RunTimeAt"))).Date))
							{
								Program.startSlowThread = new Thread(new ThreadStart(Program.getNewSoftware));
								Program.startSlowThread.IsBackground = true;
								Program.startSlowThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
								Program.startSlowThread.Start();
							}
						}
						else
						{
							wgAppConfig.UpdateKeyVal("RunTimeAt", "0");
						}
					}
					catch (Exception)
					{
						wgAppConfig.UpdateKeyVal("RunTimeAt", "0");
					}
					try
					{
						string systemParamByNO2 = wgAppConfig.getSystemParamByNO(49);
						if (string.IsNullOrEmpty(systemParamByNO2))
						{
							wgAppConfig.setSystemParamValue(49, "Install Time", DateTime.Now.ToString(wgTools.YMDHMSFormat), "");
						}
					}
					catch (Exception)
					{
					}
					try
					{
						string text = wgAppConfig.Path4Photo();
						if (!wgAppConfig.DirectoryIsExisted(text))
						{
							wgAppConfig.CreatePhotoDirectory(text);
						}
						if (!wgAppConfig.DirectoryIsExisted(text))
						{
							wgAppConfig.wgLog(text + " " + CommonStr.strFileDirectoryNotVisited);
						}
					}
					catch (Exception)
					{
					}
					try
					{
						string text2 = wgAppConfig.Path4PhotoDefault();
						if (!wgAppConfig.DirectoryIsExisted(text2))
						{
							Directory.CreateDirectory(text2);
						}
						if (!wgAppConfig.DirectoryIsExisted(text2))
						{
							wgAppConfig.wgLog(text2 + " " + CommonStr.strFileDirectoryNotVisited);
						}
					}
					catch (Exception)
					{
					}
				}
				if (wgAppConfig.IsLogin)
				{
					Application.Run(new frmADCT3000());
					if (wgAppConfig.IsAccessDB)
					{
						dfrmWait dfrmWait2 = new dfrmWait();
						dfrmWait2.Show();
						dfrmWait2.Refresh();
						wgAppConfig.backupBeforeExitByJustCopy();
						dfrmWait2.Hide();
						dfrmWait2.Close();
					}
					if (wgAppConfig.gRestart)
					{
						Process.Start(new ProcessStartInfo
						{
							FileName = Application.ExecutablePath,
							UseShellExecute = true
						});
					}
				}
				if (wgMail.bSendingMail)
				{
					for (int i = 0; i < 30; i++)
					{
						Thread.Sleep(1000);
						if (!wgMail.bSendingMail)
						{
							break;
						}
					}
				}
				try
				{
					Thread.Sleep(500);
					Environment.Exit(0);
				}
				catch (Exception)
				{
				}
				return;
			}
			dfrmWait.Close();
			if (wgAppConfig.IsAccessDB)
			{
				XMessageBox.Show(CommonStr.strAccessDatabaseNotConnected, wgTools.MSGTITLE, MessageBoxButtons.OK);
				return;
			}
			if (XMessageBox.Show(CommonStr.strSqlServerNotConnected, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\") + 1) + "SqlSet.exe",
					UseShellExecute = true
				});
			}
		}

		public static void GlobalExceptionHandler(object sender, ThreadExceptionEventArgs e)
		{
			wgTools.WgDebugWrite(e.Exception.ToString(), new object[0]);
			try
			{
				wgAppConfig.wgLog(e.Exception.ToString(), EventLogEntryType.Error, null);
				dfrmShowError dfrmShowError = new dfrmShowError();
				try
				{
					dfrmShowError.StartPosition = FormStartPosition.Manual;
					dfrmShowError.Location = new Point(0, 0);
					dfrmShowError.errInfo = e.Exception.ToString();
					dfrmShowError.ShowDialog();
				}
				catch (Exception)
				{
				}
				dfrmShowError.Dispose();
				if (!(Program.expStrDayHour == DateTime.Now.ToString("yyyy-MM-dd HH")))
				{
					Program.expStrDayHour = DateTime.Now.ToString("yyyy-MM-dd HH");
					Program.expcount = 1;
				}
				if (Program.expcount >= 3)
				{
					Thread.CurrentThread.Abort();
				}
				else
				{
					Program.expcount++;
				}
			}
			catch
			{
			}
		}

		public static void localize()
		{
			string keyVal = wgAppConfig.GetKeyVal("Language");
			if (keyVal != "" && !wgAppConfig.IsChineseSet(keyVal))
			{
				wgTools.DisplayFormat_DateYMDHMSWeek.Replace("dddd", "ddd");
				wgTools.DisplayFormat_DateYMDWeek.Replace("dddd", "ddd");
			}
			wgAppConfig.CultureInfoStr = keyVal;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("DisplayFormat_DateYMD")) && wgTools.IsValidDateTimeFormat(wgAppConfig.GetKeyVal("DisplayFormat_DateYMD")))
			{
				wgTools.DisplayFormat_DateYMD = wgAppConfig.GetKeyVal("DisplayFormat_DateYMD");
			}
			if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("DisplayFormat_DateYMDWeek")) && wgTools.IsValidDateTimeFormat(wgAppConfig.GetKeyVal("DisplayFormat_DateYMDWeek")))
			{
				wgTools.DisplayFormat_DateYMDWeek = wgAppConfig.GetKeyVal("DisplayFormat_DateYMDWeek");
			}
			if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMS")) && wgTools.IsValidDateTimeFormat(wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMS")))
			{
				wgTools.DisplayFormat_DateYMDHMS = wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMS");
			}
			if (!string.IsNullOrEmpty(wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMSWeek")) && wgTools.IsValidDateTimeFormat(wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMSWeek")))
			{
				wgTools.DisplayFormat_DateYMDHMSWeek = wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMSWeek");
			}
		}

		public static void UpgradeDatabase()
		{
			if (wgAppConfig.IsAccessDB)
			{
				Program.UpgradeDatabase_Acc();
				return;
			}
			SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				float num = 0f;
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				string cmdText = "SELECT f_Value FROM t_a_SystemParam WHERE f_No=9 ";
				SqlCommand sqlCommand2;
				SqlCommand sqlCommand = sqlCommand2 = new SqlCommand(cmdText, sqlConnection);
				try
				{
					float.TryParse(sqlCommand.ExecuteScalar().ToString(), out num);
				}
				finally
				{
					if (sqlCommand2 != null)
					{
						((IDisposable)sqlCommand2).Dispose();
					}
				}
				if (num > Program.dbVersionNewest)
				{
					Application.Exit();
				}
				else
				{
					Program.UpgradeDatabase_common(num);
					if (num != Program.dbVersionNewest)
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						wgAppConfig.setSystemParamValue(9, "Database Version", Program.dbVersionNewest.ToString(), string.Concat(new object[]
						{
							"V",
							num,
							" => V",
							Program.dbVersionNewest
						}));
						wgAppConfig.wgLog(string.Concat(new object[]
						{
							"V",
							num,
							" => V",
							Program.dbVersionNewest
						}), EventLogEntryType.Information, null);
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				sqlConnection.Dispose();
			}
		}

		private static string wgAppConfigGetSystemParamByNO(int ParaNo)
		{
			return wgAppConfig.getSystemParamByNO(ParaNo);
		}

		private static int wgAppConfigRunUpdateSql(string strSql)
		{
			return wgAppConfig.runUpdateSql(strSql);
		}

		private static void wgToolsWgDebugWrite(string info)
		{
			wgTools.WgDebugWrite(info, new object[0]);
		}

		private static bool wgAppConfigIsAccessDB()
		{
			return wgAppConfig.IsAccessDB;
		}

		private static string wgToolsPrepareStr(object obj)
		{
			return wgTools.PrepareStr(obj);
		}

		private static string wgToolsPrepareStr(object obj, bool bDate, string dateFormat)
		{
			return wgTools.PrepareStr(obj, bDate, dateFormat);
		}

		private static string getOperatorPrivilegeInsertSql(int functionId, string functionName, string displayName)
		{
			return string.Format("Insert Into t_s_OperatorPrivilege(f_OperatorID,f_FunctionID,f_FunctionName,f_FunctionDisplayName,f_ReadOnly,f_FullControl)  SELECT t_s_OperatorPrivilege.f_OperatorID,{0} as f_FunctionID,{1} as f_FunctionName ,{2} as f_FunctionDisplayName,0 as f_ReadOnly,1 as f_FullControl FROM  t_s_OperatorPrivilege WHERE t_s_OperatorPrivilege.f_functionID = 1  AND t_s_OperatorPrivilege.f_OperatorID NOT IN (SELECT t_s_OperatorPrivilege.f_OperatorID  FROM  t_s_OperatorPrivilege  WHERE t_s_OperatorPrivilege.f_functionID = {0} )", functionId, Program.wgToolsPrepareStr(functionName), Program.wgToolsPrepareStr(displayName));
		}

		public static void UpgradeDatabase_common(float dbversion)
		{
			if (dbversion == 73f)
			{
				if (Program.wgAppConfigGetSystemParamByNO(146) == "")
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(146,'Activate Door As Switch','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				if (Program.wgAppConfigGetSystemParamByNO(147) == "")
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(147,'Activate Valid Swipe Gap','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				if (Program.wgAppConfigGetSystemParamByNO(148) == "")
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(148,'Activate Operator Management','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
			}
			if (dbversion <= 73.1f)
			{
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(149,'Activate Meeting','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex)
				{
					Program.wgToolsWgDebugWrite(ex.ToString());
				}
				try
				{
					try
					{
						if (!Program.wgAppConfigIsAccessDB())
						{
							string text = "CREATE TABLE  [t_d_Reader4Meeting] ( ";
							text += "f_MeetingNO   [nvarchar] (15)   NOT NULL,";
							text += "[f_ReaderID] INT  NULL )";
							Program.wgAppConfigRunUpdateSql(text);
						}
						else
						{
							string text = "CREATE TABLE  [t_d_Reader4Meeting] ( ";
							text += "f_MeetingNO TEXT (15) NOT NULL,";
							text += "[f_ReaderID] INT  NULL )";
							Program.wgAppConfigRunUpdateSql(text);
						}
					}
					catch (Exception ex2)
					{
						Program.wgToolsWgDebugWrite(ex2.ToString());
					}
					try
					{
						if (!Program.wgAppConfigIsAccessDB())
						{
							string text = "CREATE TABLE t_d_Meeting ( ";
							text += "f_MeetingNO   [nvarchar] (15)   NOT NULL,";
							text += "f_MeetingName   [nvarchar] (255)  NULL,";
							text += "f_MeetingAdr   [nvarchar] (255)  NULL,";
							text += "f_MeetingDateTime   DATETIME NOT NULL,";
							text += "f_SignStartTime   DATETIME NOT NULL,";
							text += "f_SignEndTime   DATETIME NOT NULL,";
							text += "f_Content   [nvarchar] (255)  NULL,";
							text += "f_Notes      [ntext]  NULL  )";
							Program.wgAppConfigRunUpdateSql(text);
							text = " ALTER TABLE [t_d_Meeting] WITH NOCHECK ADD ";
							text += "\tCONSTRAINT [PK_t_d_Meeting] PRIMARY KEY  CLUSTERED ";
							text += " ([f_MeetingNO])  ";
							Program.wgAppConfigRunUpdateSql(text);
						}
						else
						{
							string text = "CREATE TABLE t_d_Meeting ( ";
							text += "f_MeetingNO TEXT (15) NOT NULL,";
							text += "f_MeetingName TEXT (255) NULL ,";
							text += "f_MeetingAdr TEXT (255) NULL ,";
							text += "f_MeetingDateTime   DATETIME NOT NULL,";
							text += "f_SignStartTime   DATETIME NOT NULL,";
							text += "f_SignEndTime   DATETIME NOT NULL,";
							text += "f_Content TEXT (255) NULL ,";
							text += "f_Notes MEMO ,";
							text += "CONSTRAINT PK_t_d_Meeting PRIMARY KEY  (f_MeetingNO))";
							Program.wgAppConfigRunUpdateSql(text);
						}
					}
					catch (Exception ex3)
					{
						Program.wgToolsWgDebugWrite(ex3.ToString());
					}
					try
					{
						if (!Program.wgAppConfigIsAccessDB())
						{
							string text = "CREATE TABLE t_d_MeetingAdr ( ";
							text += "f_MeetingAdr   [nvarchar] (255)  NOT NULL,";
							text += "f_ReaderID   INT   NOT NULL Default 0,";
							text += "f_Notes      [ntext]  NULL  )";
							Program.wgAppConfigRunUpdateSql(text);
						}
						else
						{
							string text = "CREATE TABLE t_d_MeetingAdr ( ";
							text += "f_MeetingAdr TEXT (255) NOT NULL ,";
							text += "f_ReaderID   INT   NOT NULL Default 0,";
							text += "f_Notes MEMO )";
							Program.wgAppConfigRunUpdateSql(text);
						}
					}
					catch (Exception ex4)
					{
						Program.wgToolsWgDebugWrite(ex4.ToString());
					}
					try
					{
						if (!Program.wgAppConfigIsAccessDB())
						{
							string text = "CREATE TABLE t_d_MeetingConsumer ( ";
							text += " f_Id        [int] IDENTITY (1, 1) NOT NULL  ,";
							text += " f_MeetingNO   [nvarchar] (15)   NOT NULL,";
							text += " f_ConsumerID   [int]  NOT NULL Default(0) ,";
							text += " f_MeetingIdentity    INT NOT NULL   DEFAULT -1,";
							text += " f_Seat   [nvarchar] (255)  NULL,";
							text += " f_SignWay   [int]  NOT NULL Default(0),";
							text += " f_SignRealTime   DATETIME NULL,";
							text += " f_RecID  INT NOT NULL   DEFAULT 0 ,";
							text += " f_Notes      [ntext]  NULL  )";
							Program.wgAppConfigRunUpdateSql(text);
						}
						else
						{
							string text = "CREATE TABLE t_d_MeetingConsumer ( ";
							text += " f_Id AUTOINCREMENT NOT NULL ,";
							text += " f_MeetingNO   TEXT (15)   NOT NULL,";
							text += " f_ConsumerID    INT NOT NULL   DEFAULT 0 ,";
							text += " f_MeetingIdentity    INT NOT NULL   DEFAULT -1,";
							text += " f_Seat   TEXT (255) NULL,";
							text += " f_SignWay   int  NOT NULL Default 0,";
							text += " f_SignRealTime  DATETIME NULL,";
							text += " f_RecID  INT NOT NULL   DEFAULT 0 ,";
							text += " f_Notes      MEMO  )";
							Program.wgAppConfigRunUpdateSql(text);
						}
					}
					catch (Exception ex5)
					{
						Program.wgToolsWgDebugWrite(ex5.ToString());
					}
				}
				catch (Exception ex6)
				{
					Program.wgToolsWgDebugWrite(ex6.ToString());
				}
				try
				{
					string text = "CREATE TABLE  [t_d_Reader4Meal] ( ";
					text += "[f_ReaderID] INT  NULL, f_CostMorning   Numeric(10,2) NOT   NULL  DEFAULT -1 , f_CostLunch   Numeric(10,2)  NOT NULL  DEFAULT -1 , f_CostEvening   Numeric(10,2) NOT  NULL   DEFAULT -1 , f_CostOther   Numeric(10,2) NOT  NULL  DEFAULT -1  )";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex7)
				{
					Program.wgToolsWgDebugWrite(ex7.ToString());
				}
				try
				{
					try
					{
						string text = "   CREATE TABLE  [t_b_MealSetup] ";
						if (!Program.wgAppConfigIsAccessDB())
						{
							text += "( [f_ID] INT NOT NULL , [f_Value] INT NULL , [f_BeginHMS] DATETIME NULL ,[f_EndHMS] DATETIME NULL , [f_ParamVal]   Numeric(10,2)   NULL , f_Notes      [ntext]  NULL  ) ";
						}
						else
						{
							text += "( [f_ID] INT NOT NULL , [f_Value] INT NULL , [f_BeginHMS] DATETIME NULL ,[f_EndHMS] DATETIME NULL , [f_ParamVal]   Numeric(10,2)   NULL ,  f_Notes      MEMO   ) ";
						}
						Program.wgAppConfigRunUpdateSql(text);
					}
					catch (Exception ex8)
					{
						Program.wgToolsWgDebugWrite(ex8.ToString());
					}
					try
					{
						string text = " INSERT INTO [t_b_MealSetup] ([f_ID], [f_Value], [f_BeginHMS] ,[f_EndHMS] , [f_ParamVal]) ";
						text += "VALUES (1, 0,NULL, NULL,60)";
						Program.wgAppConfigRunUpdateSql(text);
					}
					catch (Exception ex9)
					{
						Program.wgToolsWgDebugWrite(ex9.ToString());
					}
					try
					{
						string text = " INSERT INTO [t_b_MealSetup] ([f_ID], [f_Value], [f_BeginHMS] ,[f_EndHMS] , [f_ParamVal]) ";
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							"VALUES (2, 1,",
							Program.wgToolsPrepareStr("04:00", true, " HH:mm"),
							",",
							Program.wgToolsPrepareStr("09:59", true, " HH:mm"),
							",0)"
						});
						Program.wgAppConfigRunUpdateSql(text);
					}
					catch (Exception ex10)
					{
						Program.wgToolsWgDebugWrite(ex10.ToString());
					}
					try
					{
						string text = " INSERT INTO [t_b_MealSetup] ([f_ID], [f_Value], [f_BeginHMS] ,[f_EndHMS] , [f_ParamVal]) ";
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							"VALUES (3, 1,",
							Program.wgToolsPrepareStr("10:00", true, " HH:mm"),
							",",
							Program.wgToolsPrepareStr("15:59", true, " HH:mm"),
							",0)"
						});
						Program.wgAppConfigRunUpdateSql(text);
					}
					catch (Exception ex11)
					{
						Program.wgToolsWgDebugWrite(ex11.ToString());
					}
					try
					{
						string text = " INSERT INTO [t_b_MealSetup] ([f_ID], [f_Value], [f_BeginHMS] ,[f_EndHMS] , [f_ParamVal]) ";
						string text4 = text;
						text = string.Concat(new string[]
						{
							text4,
							"VALUES (4, 1,",
							Program.wgToolsPrepareStr("16:00", true, " HH:mm"),
							",",
							Program.wgToolsPrepareStr("21:59", true, " HH:mm"),
							",0)"
						});
						Program.wgAppConfigRunUpdateSql(text);
					}
					catch (Exception ex12)
					{
						Program.wgToolsWgDebugWrite(ex12.ToString());
					}
					try
					{
						string text = " INSERT INTO [t_b_MealSetup] ([f_ID], [f_Value], [f_BeginHMS] ,[f_EndHMS] , [f_ParamVal]) ";
						string text5 = text;
						text = string.Concat(new string[]
						{
							text5,
							"VALUES (5, 1,",
							Program.wgToolsPrepareStr("22:00", true, " HH:mm"),
							",",
							Program.wgToolsPrepareStr("03:59", true, " HH:mm"),
							",0)"
						});
						Program.wgAppConfigRunUpdateSql(text);
					}
					catch (Exception ex13)
					{
						Program.wgToolsWgDebugWrite(ex13.ToString());
					}
				}
				catch (Exception ex14)
				{
					Program.wgToolsWgDebugWrite(ex14.ToString());
				}
			}
			if (dbversion <= 73.2f)
			{
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(27,'AbsentTimeout (minute)','','30','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex15)
				{
					Program.wgToolsWgDebugWrite(ex15.ToString());
				}
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(28,'AllowTimeout (minute)','','10','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex16)
				{
					Program.wgToolsWgDebugWrite(ex16.ToString());
				}
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(29,'LogCreatePatrolReport','','','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex17)
				{
					Program.wgToolsWgDebugWrite(ex17.ToString());
				}
				try
				{
					string text = "CREATE TABLE  [t_b_Reader4Patrol] ( ";
					text += "[f_ReaderID] INT  NULL )";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex18)
				{
					Program.wgToolsWgDebugWrite(ex18.ToString());
				}
				try
				{
					string text = "CREATE TABLE  [t_d_PatrolUsers] ( ";
					text += "[f_ConsumerID] INT  NULL )";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex19)
				{
					Program.wgToolsWgDebugWrite(ex19.ToString());
				}
				try
				{
					string text = " CREATE TABLE t_d_PatrolRouteDetail (";
					if (Program.wgAppConfigIsAccessDB())
					{
						text += "f_RecId AUTOINCREMENT NOT NULL ,";
						text += " f_RouteID int   ,";
						text += " f_Sn int   ,";
						text += "f_ReaderID int , ";
						text += "f_patroltime TEXT(5)  NULL   , ";
						text += "f_NextDay int , ";
						text += "  CONSTRAINT PK_t_b_PatrolRouteDetail PRIMARY KEY ( f_RouteID,f_Sn)) ";
					}
					else
					{
						text += " f_RecId [int] IDENTITY (1, 1) NOT NULL  ,";
						text += " f_RouteID int   ,";
						text += " f_Sn int   ,";
						text += "f_ReaderID int , ";
						text += "f_patroltime  [nvarchar] (5)   NULL   , ";
						text += "f_NextDay int , ";
						text += "  CONSTRAINT PK_t_b_PatrolRouteDetail PRIMARY KEY ( f_RouteID,f_Sn)) ";
					}
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex20)
				{
					Program.wgToolsWgDebugWrite(ex20.ToString());
				}
				try
				{
					string text = " CREATE TABLE t_d_PatrolRouteList (";
					text += "f_RouteID  INT NOT NULL   ,";
					if (Program.wgAppConfigIsAccessDB())
					{
						text += " f_RouteName  TEXT (50) NOT NULL ,";
						text += " f_Description NOTE , ";
					}
					else
					{
						text += " f_RouteName   [nvarchar] (50) NOT NULL ,";
						text += " f_Description [ntext] NULL , ";
					}
					text += "  CONSTRAINT PK_t_d_PatrolRouteList PRIMARY KEY ( f_RouteID)) ";
					Program.wgAppConfigRunUpdateSql(text);
					text = "    CREATE UNIQUE INDEX idxf_RouteName_1 ";
					text += "   ON t_d_PatrolRouteList (f_RouteName)";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex21)
				{
					Program.wgToolsWgDebugWrite(ex21.ToString());
				}
				try
				{
					string text = " CREATE TABLE t_d_PatrolPlanData (";
					if (Program.wgAppConfigIsAccessDB())
					{
						text += "f_RecID AUTOINCREMENT NOT NULL ";
						text += " , f_ConsumerID INT  NULL  ";
						text += " , f_DateYM TEXT(10)  NULL  ";
					}
					else
					{
						text += " f_RecID [int] IDENTITY (1, 1) NOT NULL  ";
						text += " , f_ConsumerID INT  NULL  ";
						text += " , f_DateYM  [nvarchar](10)  NULL  ";
					}
					for (int i = 1; i <= 31; i++)
					{
						text = text + " , f_RouteID_" + i.ToString().PadLeft(2, '0') + "  INT   DEFAULT -1  ";
					}
					text += " , f_LogDate  DATETIME   NULL  ";
					if (Program.wgAppConfigIsAccessDB())
					{
						text += " , f_Notes MEMO NULL ";
					}
					else
					{
						text += " ,  f_Notes      [ntext]  NULL ";
					}
					text += " )";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex22)
				{
					Program.wgToolsWgDebugWrite(ex22.ToString());
				}
				try
				{
					string text = " CREATE TABLE t_d_PatrolDetailData (";
					if (Program.wgAppConfigIsAccessDB())
					{
						text += "f_RecId AUTOINCREMENT NOT NULL ,";
					}
					else
					{
						text += " f_RecId [int] IDENTITY (1, 1) NOT NULL  ,";
					}
					text += " f_ConsumerID int   ,";
					text += " f_PatrolDate  DATETIME NULL    ,";
					text += " f_RouteID int   ,";
					text += " f_ReaderID int   ,";
					text += " f_PlanPatrolTime DATETIME NULL ,";
					text += " f_RealPatrolTime DATETIME NULL ,";
					text += " f_EventDesc  int ,";
					text += "  CONSTRAINT PK_t_d_PatrolDetailData PRIMARY KEY ( f_RecId)) ";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex23)
				{
					Program.wgToolsWgDebugWrite(ex23.ToString());
				}
				try
				{
					string text = " CREATE TABLE t_d_PatrolStatistic (";
					if (Program.wgAppConfigIsAccessDB())
					{
						text += "f_RecId AUTOINCREMENT NOT NULL ,";
					}
					else
					{
						text += " f_RecId [int] IDENTITY (1, 1) NOT NULL  ,";
					}
					text += " f_ConsumerID int   ,";
					text += " f_PatrolDateStart  DATETIME NULL    ,";
					text += " f_PatrolDateEnd  DATETIME NULL    ,";
					text += " f_TotalLate int   ,";
					text += " f_TotalEarly int   ,";
					text += " f_TotalAbsence int   ,";
					text += " f_TotalNormal int   ,";
					text += "  CONSTRAINT PK_t_d_PatrolStatistic PRIMARY KEY ( f_RecId)) ";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex24)
				{
					Program.wgToolsWgDebugWrite(ex24.ToString());
				}
				try
				{
					if (Program.wgAppConfigGetSystemParamByNO(149) == "")
					{
						string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(149,'Activate Meeting','','0','')";
						Program.wgAppConfigRunUpdateSql(text);
					}
				}
				catch (Exception ex25)
				{
					Program.wgToolsWgDebugWrite(ex25.ToString());
				}
				try
				{
					if (Program.wgAppConfigGetSystemParamByNO(150) == "")
					{
						string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(150,'Activate Meal','','0','')";
						Program.wgAppConfigRunUpdateSql(text);
					}
				}
				catch (Exception ex26)
				{
					Program.wgToolsWgDebugWrite(ex26.ToString());
				}
				try
				{
					if (Program.wgAppConfigGetSystemParamByNO(151) == "")
					{
						string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(151,'Activate Patrol','','0','')";
						Program.wgAppConfigRunUpdateSql(text);
					}
				}
				catch (Exception ex27)
				{
					Program.wgToolsWgDebugWrite(ex27.ToString());
				}
			}
			if (dbversion <= 73.3f)
			{
				try
				{
					string text = Program.getOperatorPrivilegeInsertSql(48, "mnuPatrolDetailData", "Patrol");
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex28)
				{
					Program.wgToolsWgDebugWrite(ex28.ToString());
				}
				try
				{
					string text = Program.getOperatorPrivilegeInsertSql(49, "mnuConstMeal", "Meal");
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex29)
				{
					Program.wgToolsWgDebugWrite(ex29.ToString());
				}
				try
				{
					string text = Program.getOperatorPrivilegeInsertSql(50, "mnuMeeting", "Meeting");
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex30)
				{
					Program.wgToolsWgDebugWrite(ex30.ToString());
				}
				try
				{
					string text = Program.getOperatorPrivilegeInsertSql(51, "mnuElevator", "Elevator");
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex31)
				{
					Program.wgToolsWgDebugWrite(ex31.ToString());
				}
			}
			if (dbversion <= 73.5f)
			{
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(60,'Active Fire_Broadcast','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex32)
				{
					Program.wgToolsWgDebugWrite(ex32.ToString());
				}
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(61,'Active Interlock_Broadcast','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex33)
				{
					Program.wgToolsWgDebugWrite(ex33.ToString());
				}
				try
				{
					string text = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(62,'Active Antiback_Broadcast','','0','')";
					Program.wgAppConfigRunUpdateSql(text);
				}
				catch (Exception ex34)
				{
					Program.wgToolsWgDebugWrite(ex34.ToString());
				}
			}
		}

		public static void UpgradeDatabase_Acc()
		{
			if (!wgAppConfig.IsAccessDB)
			{
				return;
			}
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				float num = 0f;
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				string cmdText = "SELECT f_Value FROM t_a_SystemParam WHERE f_No=9 ";
				OleDbCommand oleDbCommand2;
				OleDbCommand oleDbCommand = oleDbCommand2 = new OleDbCommand(cmdText, oleDbConnection);
				try
				{
					float.TryParse(oleDbCommand.ExecuteScalar().ToString(), out num);
				}
				finally
				{
					if (oleDbCommand2 != null)
					{
						((IDisposable)oleDbCommand2).Dispose();
					}
				}
				if (num > Program.dbVersionNewest)
				{
					Application.Exit();
				}
				else
				{
					Program.UpgradeDatabase_common(num);
					if (num != Program.dbVersionNewest)
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						wgAppConfig.setSystemParamValue(9, "Database Version", Program.dbVersionNewest.ToString(), string.Concat(new object[]
						{
							"V",
							num,
							" => V",
							Program.dbVersionNewest
						}));
						wgAppConfig.wgLog(string.Concat(new object[]
						{
							"V",
							num,
							" => V",
							Program.dbVersionNewest
						}), EventLogEntryType.Information, null);
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				oleDbConnection.Dispose();
			}
		}

		public static string descDbConnection(string strDbConnection)
		{
			string result = strDbConnection;
			try
			{
				if (!string.IsNullOrEmpty(strDbConnection) && strDbConnection.Length > 3 && strDbConnection.Substring(0, 3) == "ENC")
				{
					result = WGPacket.Dpt(strDbConnection.Substring(3));
				}
			}
			catch
			{
			}
			return result;
		}

		public static int dbConnectionCheck()
		{
			int num = -1;
			string text = Program.descDbConnection(wgAppConfig.GetKeyVal("dbConnection"));
			bool flag = true;
			if (!string.IsNullOrEmpty(text))
			{
				wgAppConfig.dbConString = text;
				flag = false;
			}
			if (string.IsNullOrEmpty(text))
			{
				wgAppConfig.IsAccessDB = true;
				wgAppConfig.dbConString = Program.g_cnStrAcc;
			}
			else if (text.ToUpper().IndexOf("Data Source".ToUpper()) >= 0 && text.ToUpper().IndexOf(".OLEDB".ToUpper()) < 0)
			{
				wgAppConfig.IsAccessDB = false;
			}
			else
			{
				wgAppConfig.IsAccessDB = true;
			}
			if (wgAppConfig.IsAccessDB)
			{
				return Program.dbConnectionCheck_Acc();
			}
			SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString + ";Connection Timeout=5");
			try
			{
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				string cmdText = "SELECT * FROM t_a_SystemParam WHERE f_NO = 12";
				SqlCommand sqlCommand2;
				SqlCommand sqlCommand = sqlCommand2 = new SqlCommand(cmdText, sqlConnection);
				try
				{
					sqlCommand.CommandTimeout = 5;
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						num = 1;
					}
					sqlDataReader.Close();
				}
				finally
				{
					if (sqlCommand2 != null)
					{
						((IDisposable)sqlCommand2).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				sqlConnection.Dispose();
			}
			if (num <= 0 && flag)
			{
				if (Program.ConnectTest2010())
				{
					if (Program.createDatabase2010(wgAppConfig.dbName))
					{
						text = Program.descDbConnection(wgAppConfig.GetKeyVal("dbConnection"));
						if (!string.IsNullOrEmpty(text))
						{
							wgAppConfig.dbConString = text;
							num = 1;
						}
					}
				}
				else
				{
					Program.bSqlExress = true;
					if (Program.ConnectTest2010())
					{
						SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString.Replace(";Data Source=(local)", ";Data Source=(local)\\sqlexpress") + ";Connection Timeout=5");
						try
						{
							if (sqlConnection2.State != ConnectionState.Open)
							{
								sqlConnection2.Open();
							}
							string cmdText = "SELECT * FROM t_a_SystemParam WHERE f_NO = 12";
							SqlCommand sqlCommand3;
							SqlCommand sqlCommand = sqlCommand3 = new SqlCommand(cmdText, sqlConnection2);
							try
							{
								sqlCommand.CommandTimeout = 5;
								SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
								if (sqlDataReader2.Read())
								{
									num = 1;
								}
								sqlDataReader2.Close();
							}
							finally
							{
								if (sqlCommand3 != null)
								{
									((IDisposable)sqlCommand3).Dispose();
								}
							}
						}
						catch (Exception ex2)
						{
							wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
						}
						finally
						{
							sqlConnection2.Dispose();
						}
						if (num > 0)
						{
							wgAppConfig.UpdateKeyVal("dbConnection", Program.getConSql(wgAppConfig.dbName));
							text = Program.descDbConnection(wgAppConfig.GetKeyVal("dbConnection"));
							if (!string.IsNullOrEmpty(text))
							{
								wgAppConfig.dbConString = text;
								num = 1;
							}
							else
							{
								num = 0;
							}
						}
						else if (Program.createDatabase2010(wgAppConfig.dbName))
						{
							text = Program.descDbConnection(wgAppConfig.GetKeyVal("dbConnection"));
							if (!string.IsNullOrEmpty(text))
							{
								wgAppConfig.dbConString = text;
								num = 1;
							}
						}
					}
				}
			}
			if (num > 0)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		public static int dbConnectionCheck_Acc()
		{
			int num = -1;
			if (!wgAppConfig.IsAccessDB)
			{
				return num;
			}
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				string cmdText = "SELECT * FROM t_a_SystemParam WHERE f_NO = 12";
				OleDbCommand oleDbCommand2;
				OleDbCommand oleDbCommand = oleDbCommand2 = new OleDbCommand(cmdText, oleDbConnection);
				try
				{
					oleDbCommand.CommandTimeout = 5;
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.Read())
					{
						num = 1;
					}
					oleDbDataReader.Close();
				}
				finally
				{
					if (oleDbCommand2 != null)
					{
						((IDisposable)oleDbCommand2).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				oleDbConnection.Dispose();
			}
			if (num <= 0)
			{
				try
				{
					FileInfo fileInfo = new FileInfo(Application.StartupPath + "\\" + wgAppConfig.accessDbName + ".mdb");
					if (fileInfo.Exists)
					{
						try
						{
							if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
							{
								fileInfo.Attributes &= (FileAttributes)16777214;
							}
							goto IL_1C6;
						}
						catch (Exception)
						{
							goto IL_1C6;
						}
						goto IL_E0;
						IL_1C6:
						goto IL_1EC;
					}
					IL_E0:
					FileInfo fileInfo2 = new FileInfo(string.Format(Application.StartupPath + "\\PHOTO\\{0}.mdbAA", wgAppConfig.accessDbName));
					int result;
					if (!fileInfo2.Exists)
					{
						FileInfo fileInfo3 = new FileInfo(string.Format(Application.StartupPath + "\\{0}.mdb.gz", wgAppConfig.accessDbName));
						if (!fileInfo3.Exists)
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(fileInfo3.FullName, FileMode.Create)))
							{
								binaryWriter.Write(Resources.iCCard3000_mdbA);
							}
						}
						wgTools.Decompress(fileInfo3);
						fileInfo3.Delete();
						result = 1;
						return result;
					}
					fileInfo2.CopyTo(Application.StartupPath + "\\" + wgAppConfig.accessDbName + ".mdb", true);
					fileInfo2 = new FileInfo(Application.StartupPath + "\\" + wgAppConfig.accessDbName + ".mdb");
					fileInfo2.Attributes = FileAttributes.Archive;
					result = 1;
					return result;
				}
				catch (Exception ex2)
				{
					wgTools.WgDebugWrite(ex2.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
				}
			}
			IL_1EC:
			if (num > 0)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		private static string getConSql(string dbName)
		{
			string result = string.Format("data source={0};initial catalog={1};integrated security=SSPI;persist security info=True", "(local)", dbName);
			if (Program.bSqlExress)
			{
				result = string.Format("data source={0};initial catalog={1};integrated security=SSPI;persist security info=True", "(local)\\sqlexpress", dbName);
			}
			return result;
		}

		private static bool ConnectTest2010()
		{
			bool result = false;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				string text = Program.getConSql("master");
				text += ";Connection Timeout=5";
				SqlConnection sqlConnection = new SqlConnection(text);
				try
				{
					string cmdText = " SELECT name FROM sysdatabases ";
					bool flag = false;
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlCommand.CommandTimeout = 5;
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							flag = true;
						}
						sqlDataReader.Close();
					}
					if (!flag)
					{
						return false;
					}
					result = true;
				}
				catch (Exception)
				{
				}
				finally
				{
					sqlConnection.Dispose();
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
			return result;
		}

		private static bool createDatabase2010(string databaseName)
		{
			bool flag = false;
			if (databaseName == "")
			{
				return flag;
			}
			bool flag2 = false;
			try
			{
				string text = Program.getConSql("master");
				text += ";Connection Timeout=5";
				SqlConnection sqlConnection = new SqlConnection(text);
				try
				{
					sqlConnection.Open();
					string text2 = " SELECT name FROM sysdatabases ";
					object obj = null;
					SqlCommand sqlCommand2;
					SqlCommand sqlCommand = sqlCommand2 = new SqlCommand(text2, sqlConnection);
					try
					{
						sqlCommand.CommandTimeout = 5;
						obj = sqlCommand.ExecuteScalar();
					}
					finally
					{
						if (sqlCommand2 != null)
						{
							((IDisposable)sqlCommand2).Dispose();
						}
					}
					if (obj == null)
					{
						bool result = flag;
						return result;
					}
					text2 = " SELECT  convert( int, LEFT(convert(nvarchar,SERVERPROPERTY('ProductVersion')),CHARINDEX('.',convert(nvarchar,SERVERPROPERTY('ProductVersion')))-1)) ";
					object obj2 = null;
					SqlCommand sqlCommand3;
					sqlCommand = (sqlCommand3 = new SqlCommand(text2, sqlConnection));
					try
					{
						sqlCommand.CommandTimeout = 5;
						obj2 = sqlCommand.ExecuteScalar();
					}
					finally
					{
						if (sqlCommand3 != null)
						{
							((IDisposable)sqlCommand3).Dispose();
						}
					}
					if (obj2 == null)
					{
						bool result = flag;
						return result;
					}
					string path = "";
					string str = "n3k_default.sql";
					path = Application.StartupPath + "\\" + str;
					text2 = "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + databaseName + "')";
					text2 = text2 + "\r\n DROP DATABASE [" + databaseName + "]";
					SqlCommand sqlCommand4;
					sqlCommand = (sqlCommand4 = new SqlCommand(text2, sqlConnection));
					try
					{
						sqlCommand.CommandTimeout = 300;
						sqlCommand.ExecuteNonQuery();
					}
					finally
					{
						if (sqlCommand4 != null)
						{
							((IDisposable)sqlCommand4).Dispose();
						}
					}
					text2 = " CREATE DATABASE [" + databaseName + "] ";
					SqlCommand sqlCommand5;
					sqlCommand = (sqlCommand5 = new SqlCommand(text2, sqlConnection));
					try
					{
						sqlCommand.CommandTimeout = 300;
						sqlCommand.ExecuteNonQuery();
					}
					finally
					{
						if (sqlCommand5 != null)
						{
							((IDisposable)sqlCommand5).Dispose();
						}
					}
					text2 = wgTools.ReadTextFile(path);
					text2 = text2.Replace("ADCT3000", databaseName);
					text2 = text2.Replace("\r\nGO", "\r\n");
					text2 = text2.Replace(" COLLATE Chinese_PRC_CI_AS", " ");
					SqlCommand sqlCommand6;
					sqlCommand = (sqlCommand6 = new SqlCommand(text2, sqlConnection));
					try
					{
						sqlCommand.CommandTimeout = 300;
						sqlCommand.ExecuteNonQuery();
					}
					finally
					{
						if (sqlCommand6 != null)
						{
							((IDisposable)sqlCommand6).Dispose();
						}
					}
					bool flag3 = false;
					try
					{
						text2 = " CREATE PARTITION FUNCTION [RangePrivilegePF1](int) AS RANGE LEFT FOR VALUES (N'1',N'2',N'3',N'4',N'5',N'6',N'7',N'8',N'9',N'10',N'11',N'12',N'13',N'14',N'15',N'16',N'17',N'18',N'19',N'20',N'21',N'22',N'23',N'24',N'25',N'26',N'27',N'28',N'29',N'30',N'31',N'32',N'33',N'34',N'35',N'36',N'37',N'38',N'39',N'40',N'41',N'42',N'43',N'44',N'45',N'46',N'47',N'48',N'49',N'50',N'51',N'52',N'53',N'54',N'55',N'56',N'57',N'58',N'59',N'60',N'61',N'62',N'63',N'64',N'65',N'66',N'67',N'68',N'69',N'70',N'71',N'72',N'73',N'74',N'75',N'76',N'77',N'78',N'79',N'80',N'81',N'82',N'83',N'84',N'85',N'86',N'87',N'88',N'89',N'90',N'91',N'92',N'93',N'94',N'95',N'96',N'97',N'98',N'99',N'100',N'101',N'102',N'103',N'104',N'105',N'106',N'107',N'108',N'109',N'110',N'111',N'112',N'113',N'114',N'115',N'116',N'117',N'118',N'119',N'120',N'121',N'122',N'123',N'124',N'125',N'126',N'127',N'128',N'129',N'130',N'131',N'132',N'133',N'134',N'135',N'136',N'137',N'138',N'139',N'140',N'141',N'142',N'143',N'144',N'145',N'146',N'147',N'148',N'149',N'150',N'151',N'152',N'153',N'154',N'155',N'156',N'157',N'158',N'159',N'160',N'161',N'162',N'163',N'164',N'165',N'166',N'167',N'168',N'169',N'170',N'171',N'172',N'173',N'174',N'175',N'176',N'177',N'178',N'179',N'180',N'181',N'182',N'183',N'184',N'185',N'186',N'187',N'188',N'189',N'190',N'191',N'192',N'193',N'194',N'195',N'196',N'197',N'198',N'199',N'200',N'201',N'202',N'203',N'204',N'205',N'206',N'207',N'208',N'209',N'210',N'211',N'212',N'213',N'214',N'215',N'216',N'217',N'218',N'219',N'220',N'221',N'222',N'223',N'224',N'225',N'226',N'227',N'228',N'229',N'230',N'231',N'232',N'233',N'234',N'235',N'236',N'237',N'238',N'239',N'240',N'241',N'242',N'243',N'244',N'245',N'246',N'247',N'248',N'249',N'250',N'251',N'252',N'253',N'254',N'255',N'256',N'257',N'258',N'259',N'260',N'261',N'262',N'263',N'264',N'265',N'266',N'267',N'268',N'269',N'270',N'271',N'272',N'273',N'274',N'275',N'276',N'277',N'278',N'279',N'280',N'281',N'282',N'283',N'284',N'285',N'286',N'287',N'288',N'289',N'290',N'291',N'292',N'293',N'294',N'295',N'296',N'297',N'298',N'299',N'300',N'301',N'302',N'303',N'304',N'305',N'306',N'307',N'308',N'309',N'310',N'311',N'312',N'313',N'314',N'315',N'316',N'317',N'318',N'319',N'320',N'321',N'322',N'323',N'324',N'325',N'326',N'327',N'328',N'329',N'330',N'331',N'332',N'333',N'334',N'335',N'336',N'337',N'338',N'339',N'340',N'341',N'342',N'343',N'344',N'345',N'346',N'347',N'348',N'349',N'350',N'351',N'352',N'353',N'354',N'355',N'356',N'357',N'358',N'359',N'360',N'361',N'362',N'363',N'364',N'365',N'366',N'367',N'368',N'369',N'370',N'371',N'372',N'373',N'374',N'375',N'376',N'377',N'378',N'379',N'380',N'381',N'382',N'383',N'384',N'385',N'386',N'387',N'388',N'389',N'390',N'391',N'392',N'393',N'394',N'395',N'396',N'397',N'398',N'399',N'400',N'401',N'402',N'403',N'404',N'405',N'406',N'407',N'408',N'409',N'410',N'411',N'412',N'413',N'414',N'415',N'416',N'417',N'418',N'419',N'420',N'421',N'422',N'423',N'424',N'425',N'426',N'427',N'428',N'429',N'430',N'431',N'432',N'433',N'434',N'435',N'436',N'437',N'438',N'439',N'440',N'441',N'442',N'443',N'444',N'445',N'446',N'447',N'448',N'449',N'450',N'451',N'452',N'453',N'454',N'455',N'456',N'457',N'458',N'459',N'460',N'461',N'462',N'463',N'464',N'465',N'466',N'467',N'468',N'469',N'470',N'471',N'472',N'473',N'474',N'475',N'476',N'477',N'478',N'479',N'480',N'481',N'482',N'483',N'484',N'485',N'486',N'487',N'488',N'489',N'490',N'491',N'492',N'493',N'494',N'495',N'496',N'497',N'498',N'499',N'500',N'501',N'502',N'503',N'504',N'505',N'506',N'507',N'508',N'509',N'510',N'511',N'512',N'513',N'514',N'515',N'516',N'517',N'518',N'519',N'520',N'521',N'522',N'523',N'524',N'525',N'526',N'527',N'528',N'529',N'530',N'531',N'532',N'533',N'534',N'535',N'536',N'537',N'538',N'539',N'540',N'541',N'542',N'543',N'544',N'545',N'546',N'547',N'548',N'549',N'550',N'551',N'552',N'553',N'554',N'555',N'556',N'557',N'558',N'559',N'560',N'561',N'562',N'563',N'564',N'565',N'566',N'567',N'568',N'569',N'570',N'571',N'572',N'573',N'574',N'575',N'576',N'577',N'578',N'579',N'580',N'581',N'582',N'583',N'584',N'585',N'586',N'587',N'588',N'589',N'590',N'591',N'592',N'593',N'594',N'595',N'596',N'597',N'598',N'599',N'600',N'601',N'602',N'603',N'604',N'605',N'606',N'607',N'608',N'609',N'610',N'611',N'612',N'613',N'614',N'615',N'616',N'617',N'618',N'619',N'620',N'621',N'622',N'623',N'624',N'625',N'626',N'627',N'628',N'629',N'630',N'631',N'632',N'633',N'634',N'635',N'636',N'637',N'638',N'639',N'640',N'641',N'642',N'643',N'644',N'645',N'646',N'647',N'648',N'649',N'650',N'651',N'652',N'653',N'654',N'655',N'656',N'657',N'658',N'659',N'660',N'661',N'662',N'663',N'664',N'665',N'666',N'667',N'668',N'669',N'670',N'671',N'672',N'673',N'674',N'675',N'676',N'677',N'678',N'679',N'680',N'681',N'682',N'683',N'684',N'685',N'686',N'687',N'688',N'689',N'690',N'691',N'692',N'693',N'694',N'695',N'696',N'697',N'698',N'699',N'700',N'701',N'702',N'703',N'704',N'705',N'706',N'707',N'708',N'709',N'710',N'711',N'712',N'713',N'714',N'715',N'716',N'717',N'718',N'719',N'720',N'721',N'722',N'723',N'724',N'725',N'726',N'727',N'728',N'729',N'730',N'731',N'732',N'733',N'734',N'735',N'736',N'737',N'738',N'739',N'740',N'741',N'742',N'743',N'744',N'745',N'746',N'747',N'748',N'749',N'750',N'751',N'752',N'753',N'754',N'755',N'756',N'757',N'758',N'759',N'760',N'761',N'762',N'763',N'764',N'765',N'766',N'767',N'768',N'769',N'770',N'771',N'772',N'773',N'774',N'775',N'776',N'777',N'778',N'779',N'780',N'781',N'782',N'783',N'784',N'785',N'786',N'787',N'788',N'789',N'790',N'791',N'792',N'793',N'794',N'795',N'796',N'797',N'798',N'799',N'800',N'801',N'802',N'803',N'804',N'805',N'806',N'807',N'808',N'809',N'810',N'811',N'812',N'813',N'814',N'815',N'816',N'817',N'818',N'819',N'820',N'821',N'822',N'823',N'824',N'825',N'826',N'827',N'828',N'829',N'830',N'831',N'832',N'833',N'834',N'835',N'836',N'837',N'838',N'839',N'840',N'841',N'842',N'843',N'844',N'845',N'846',N'847',N'848',N'849',N'850',N'851',N'852',N'853',N'854',N'855',N'856',N'857',N'858',N'859',N'860',N'861',N'862',N'863',N'864',N'865',N'866',N'867',N'868',N'869',N'870',N'871',N'872',N'873',N'874',N'875',N'876',N'877',N'878',N'879',N'880',N'881',N'882',N'883',N'884',N'885',N'886',N'887',N'888',N'889',N'890',N'891',N'892',N'893',N'894',N'895',N'896',N'897',N'898',N'899',N'900',N'901',N'902',N'903',N'904',N'905',N'906',N'907',N'908',N'909',N'910',N'911',N'912',N'913',N'914',N'915',N'916',N'917',N'918',N'919',N'920',N'921',N'922',N'923',N'924',N'925',N'926',N'927',N'928',N'929',N'930',N'931',N'932',N'933',N'934',N'935',N'936',N'937',N'938',N'939',N'940',N'941',N'942',N'943',N'944',N'945',N'946',N'947',N'948',N'949',N'950',N'951',N'952',N'953',N'954',N'955',N'956',N'957',N'958',N'959',N'960',N'961',N'962',N'963',N'964',N'965',N'966',N'967',N'968',N'969',N'970',N'971',N'972',N'973',N'974',N'975',N'976',N'977',N'978',N'979',N'980',N'981',N'982',N'983',N'984',N'985',N'986',N'987',N'988',N'989',N'990',N'991',N'992',N'993',N'994',N'995',N'996',N'997',N'998',N'999') \r\n     \r\nCREATE PARTITION SCHEME [RangePrivilegePS1] AS PARTITION [RangePrivilegePF1] TO ([PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY])\r\n\r\n\r\nCREATE TABLE [dbo].[t_d_Privilege](\r\n\t[f_PrivilegeRecID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,\r\n\t[f_DoorID] [int] NOT NULL,\r\n\t[f_ControlSegID] [int] NOT NULL,\r\n\t[f_ConsumerID] [int] NOT NULL,\r\n\t[f_ControllerID] [int] NOT NULL,\r\n\t[f_DoorNO] [tinyint] NOT NULL,\r\n    CONSTRAINT [PK_t_d_Privilege] PRIMARY KEY CLUSTERED \r\n    (\r\n\t[f_ControllerID] ASC,\r\n\t[f_PrivilegeRecID] ASC\r\n    )  ON [RangePrivilegePS1](f_ControllerID) \r\n)ON [PRIMARY] \r\n\r\n";
						SqlCommand sqlCommand7;
						sqlCommand = (sqlCommand7 = new SqlCommand(text2, sqlConnection));
						try
						{
							sqlCommand.CommandTimeout = 300;
							sqlCommand.ExecuteNonQuery();
							flag3 = true;
						}
						finally
						{
							if (sqlCommand7 != null)
							{
								((IDisposable)sqlCommand7).Dispose();
							}
						}
					}
					catch (Exception)
					{
					}
					if (!flag3)
					{
						text2 = "CREATE TABLE [dbo].[t_d_Privilege](\r\n\t[f_PrivilegeRecID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,\r\n\t[f_DoorID] [int] NOT NULL,\r\n\t[f_ControlSegID] [int] NOT NULL,\r\n\t[f_ConsumerID] [int] NOT NULL,\r\n\t[f_ControllerID] [int] NOT NULL,\r\n\t[f_DoorNO] [tinyint] NOT NULL,\r\n  CONSTRAINT [PK_t_d_Privilege] PRIMARY KEY CLUSTERED \r\n  (\r\n\t[f_ControllerID] ASC,\r\n\t[f_PrivilegeRecID] ASC\r\n  )  ON [PRIMARY] \r\n)ON [PRIMARY] \r\n\r\n";
						SqlCommand sqlCommand8;
						sqlCommand = (sqlCommand8 = new SqlCommand(text2, sqlConnection));
						try
						{
							sqlCommand.CommandTimeout = 300;
							sqlCommand.ExecuteNonQuery();
						}
						finally
						{
							if (sqlCommand8 != null)
							{
								((IDisposable)sqlCommand8).Dispose();
							}
						}
					}
					if (flag3)
					{
						text2 = "\r\nCREATE NONCLUSTERED INDEX [_dta_index_t_d_Privilege_12_1810105489__K4_1_2_3_5] ON [dbo].[t_d_Privilege] \r\n(\r\n\t[f_ConsumerID] ASC\r\n)\r\nINCLUDE ( [f_PrivilegeRecID],\r\n[f_DoorID],\r\n[f_ControlSegID],\r\n[f_ControllerID]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]\r\n";
					}
					else
					{
						text2 = "\r\nCREATE NONCLUSTERED INDEX [_dta_index_t_d_Privilege_12_1810105489__K4_1_2_3_5] ON [dbo].[t_d_Privilege] \r\n(\r\n\t[f_ConsumerID] ASC\r\n) ON [PRIMARY]\r\n";
					}
					SqlCommand sqlCommand9;
					sqlCommand = (sqlCommand9 = new SqlCommand(text2, sqlConnection));
					try
					{
						sqlCommand.CommandTimeout = 300;
						sqlCommand.ExecuteNonQuery();
					}
					finally
					{
						if (sqlCommand9 != null)
						{
							((IDisposable)sqlCommand9).Dispose();
						}
					}
					if (flag3)
					{
						text2 = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(53,'Created Partition','1','1','2010-8-3 16:07:41')";
					}
					else
					{
						text2 = "Insert Into t_a_SystemParam(f_No,f_Name,f_EName,f_Value,f_Notes) Values(53,'Created Partition','0','0','2010-8-3 16:07:48')";
					}
					SqlCommand sqlCommand10;
					sqlCommand = (sqlCommand10 = new SqlCommand(text2, sqlConnection));
					try
					{
						sqlCommand.CommandTimeout = 300;
						sqlCommand.ExecuteNonQuery();
						flag2 = true;
					}
					finally
					{
						if (sqlCommand10 != null)
						{
							((IDisposable)sqlCommand10).Dispose();
						}
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					sqlConnection.Dispose();
				}
				wgAppConfig.UpdateKeyVal("dbConnection", Program.getConSql(databaseName));
				wgAppConfig.wgLogWithoutDB("Create DB " + databaseName, EventLogEntryType.Information, null);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				flag = flag2;
			}
			return flag;
		}
	}
}
