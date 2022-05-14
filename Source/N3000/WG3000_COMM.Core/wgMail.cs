using jmail;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.DataOper;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Core
{
	internal class wgMail
	{
		public static bool bSendingMail;

		public static void sendMailOnce()
		{
			string mailSubject;
			string strInfo = wgMail.sysInfo4Mail(out mailSubject);
			int i = 0;
			wgMail.bSendingMail = true;
			while (i < 3)
			{
				if (wgMail.sendMail(strInfo, mailSubject))
				{
					wgMail.bSendingMail = false;
					return;
				}
				Thread.Sleep(120000);
				i++;
			}
			wgMail.bSendingMail = false;
		}

		private static string sysInfo4Mail(out string subject)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return wgMail.sysInfo4Mail_Acc(out subject);
			}
			string text = "";
			string text2 = "Mail Subject";
			try
			{
				text = text + "\r\n【软件版本】：V" + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf("."));
				string systemParamByNO = wgAppConfig.getSystemParamByNO(49);
				text += "\r\n【起始日期】：";
				if (!string.IsNullOrEmpty(systemParamByNO))
				{
					text += DateTime.Parse(systemParamByNO).ToString("yyyy-MM-dd");
				}
				text += "\r\n【硬件版本】：";
				string text3;
				string text4;
				wgAppConfig.getSystemParamValue(48, out text3, out text3, out text4);
				if (!string.IsNullOrEmpty(text4) && text4.IndexOf("\r\n") >= 0)
				{
					text4 = text4.Substring(text4.IndexOf("\r\n") + "\r\n".Length);
				}
				string text5 = "";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.CommandText = "SELECT f_ControllerSN FROM t_b_Controller ";
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							text5 += wgTools.SetObjToStr(sqlDataReader[0]);
							if (text4.IndexOf(sqlDataReader[0].ToString() + ",VER=") >= 0)
							{
								string text6 = text4.Substring(text4.IndexOf(sqlDataReader[0].ToString() + ",VER=") + (sqlDataReader[0].ToString() + ",VER=").Length);
								if (text6.Length > 0)
								{
									text6 = text6.Substring(0, text6.IndexOf(","));
									text5 = text5 + "(v" + text6 + ");";
								}
								else
								{
									text5 += "(v  )";
								}
							}
							else
							{
								text5 += "(v  )";
							}
						}
						sqlDataReader.Close();
					}
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text += text5;
				}
				text += "\r\n";
				text += "\r\n【使用者公司全称】：";
				string text7;
				string str;
				wgAppConfig.getSystemParamValue(36, out text3, out text7, out str);
				if (!string.IsNullOrEmpty(text7))
				{
					text += text7;
				}
				if (icOperator.checkSoftwareRegister() > 0)
				{
					text = text + "\r\n" + CommonStr.strAlreadyRegistered;
					text2 = text7 + "[" + CommonStr.strAlreadyRegistered + "]";
					text = text + "\r\n【施工和承建公司名称】：" + str;
				}
				else
				{
					text = text + "\r\n" + CommonStr.strUnRegistered;
					text2 = text7 + "[" + CommonStr.strUnRegistered + "]";
					text = text + "\r\n【施工和承建公司名称】：" + str;
				}
				text2 = string.Concat(new string[]
				{
					wgAppConfig.ProductTypeOfApp,
					"2012_",
					text2,
					"_V",
					Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf("."))
				});
				using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand2 = new SqlCommand("", sqlConnection2))
					{
						sqlConnection2.Open();
						sqlCommand2.CommandText = " SELECT COUNT(*)  from t_b_door where f_doorEnabled=1";
						SqlDataReader sqlDataReader = sqlCommand2.ExecuteReader();
						if (sqlDataReader.Read())
						{
							text = text + "\r\n【门数】：" + sqlDataReader[0].ToString();
						}
						sqlDataReader.Close();
					}
				}
				using (SqlConnection sqlConnection3 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand3 = new SqlCommand("", sqlConnection3))
					{
						sqlConnection3.Open();
						sqlCommand3.CommandText = "SELECT f_ControllerSN FROM t_b_Controller ";
						SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
						text5 = "";
						while (sqlDataReader.Read())
						{
							text5 = text5 + "\r\n" + sqlDataReader[0].ToString();
						}
						sqlDataReader.Close();
					}
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text = text + "\r\n【控制器序列号S/N】：" + text5;
				}
				text += "\r\n【其他信息】：";
				using (SqlConnection sqlConnection4 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand4 = new SqlCommand("", sqlConnection4))
					{
						sqlConnection4.Open();
						sqlCommand4.CommandText = "SELECT count(*) FROM t_b_Consumer ";
						SqlDataReader sqlDataReader = sqlCommand4.ExecuteReader();
						if (sqlDataReader.Read())
						{
							text = text + "\r\n【注册人数】：" + sqlDataReader[0].ToString();
						}
						sqlDataReader.Close();
					}
				}
				using (SqlConnection sqlConnection5 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand5 = new SqlCommand("", sqlConnection5))
					{
						sqlConnection5.Open();
						sqlCommand5.CommandText = "SELECT f_No, f_Value FROM t_a_SystemParam WHERE f_NO>100 ORDER BY f_No Asc ";
						SqlDataReader sqlDataReader = sqlCommand5.ExecuteReader();
						if (sqlDataReader.HasRows)
						{
							string text8 = "";
							text += "\r\n【启用功能(No,Val)】：\r\n";
							while (sqlDataReader.Read())
							{
								string text9 = text;
								text = string.Concat(new string[]
								{
									text9,
									" (",
									sqlDataReader["f_No"].ToString(),
									",",
									wgTools.SetObjToStr(sqlDataReader["f_Value"]),
									")"
								});
								if (wgTools.SetObjToStr(sqlDataReader["f_Value"]) == "1")
								{
									text8 = text8 + sqlDataReader["f_No"].ToString() + ";";
								}
							}
							if (!string.IsNullOrEmpty(text8))
							{
								text = text + "\r\n【已启用】：" + text8;
							}
						}
						sqlDataReader.Close();
					}
				}
				using (SqlConnection sqlConnection6 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand6 = new SqlCommand("", sqlConnection6))
					{
						sqlConnection6.Open();
						sqlCommand6.CommandText = "SELECT f_No, f_Value FROM t_a_SystemParam  WHERE f_NO<100  ORDER BY f_No Asc ";
						SqlDataReader sqlDataReader = sqlCommand6.ExecuteReader();
						if (sqlDataReader.HasRows)
						{
							text += "\r\n【参数值(No,Val)】：\r\n";
							while (sqlDataReader.Read())
							{
								string text10 = text;
								text = string.Concat(new string[]
								{
									text10,
									" (",
									sqlDataReader["f_No"].ToString(),
									",",
									wgTools.SetObjToStr(sqlDataReader["f_Value"]),
									")"
								});
							}
						}
						sqlDataReader.Close();
					}
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text += "\r\n【DrvInfo】：\r\n";
					text += text4;
				}
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)enumerator.Current;
							text += string.Format("\r\n【{0}】： ", CommonStr.strSystem);
							text += string.Format("\r\n{0} ", managementObject["Caption"]);
							text += string.Format("\r\n{0} ", managementObject["version"].ToString());
							text += string.Format("\r\n{0} ", managementObject["CSDVersion"]);
						}
					}
				}
				text += string.Format("\r\n【数据库版本】： ", new object[0]);
				string text11 = "SELECT @@VERSION";
				using (SqlConnection sqlConnection7 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand7 = new SqlCommand("", sqlConnection7))
					{
						sqlConnection7.Open();
						sqlCommand7.CommandText = text11;
						text11 = wgTools.SetObjToStr(sqlCommand7.ExecuteScalar());
					}
				}
				if (!string.IsNullOrEmpty(text11))
				{
					text += string.Format("\r\n{0}", text11);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			text += "\r\n---------------------------------------------";
			text += "\r\n";
			text = text + "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
			subject = text2;
			return text;
		}

		private static string sysInfo4Mail_Acc(out string subject)
		{
			string text = "";
			string text2 = "Mail Subject";
			try
			{
				text = text + "\r\n【软件版本】：V" + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf("."));
				string systemParamByNO = wgAppConfig.getSystemParamByNO(49);
				text += "\r\n【起始日期】：";
				if (!string.IsNullOrEmpty(systemParamByNO))
				{
					text += DateTime.Parse(systemParamByNO).ToString("yyyy-MM-dd");
				}
				text += "\r\n【硬件版本】：";
				string text3;
				string text4;
				wgAppConfig.getSystemParamValue(48, out text3, out text3, out text4);
				if (!string.IsNullOrEmpty(text4) && text4.IndexOf("\r\n") >= 0)
				{
					text4 = text4.Substring(text4.IndexOf("\r\n") + "\r\n".Length);
				}
				string text5 = "";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
					{
						oleDbConnection.Open();
						oleDbCommand.CommandText = "SELECT f_ControllerSN FROM t_b_Controller ";
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							text5 += wgTools.SetObjToStr(oleDbDataReader[0]);
							if (text4.IndexOf(oleDbDataReader[0].ToString() + ",VER=") >= 0)
							{
								string text6 = text4.Substring(text4.IndexOf(oleDbDataReader[0].ToString() + ",VER=") + (oleDbDataReader[0].ToString() + ",VER=").Length);
								if (text6.Length > 0)
								{
									text6 = text6.Substring(0, text6.IndexOf(","));
									text5 = text5 + "(v" + text6 + ");";
								}
								else
								{
									text5 += "(v  )";
								}
							}
							else
							{
								text5 += "(v  )";
							}
						}
						oleDbDataReader.Close();
					}
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text += text5;
				}
				text += "\r\n";
				text += "\r\n【使用者公司全称】：";
				string text7;
				string str;
				wgAppConfig.getSystemParamValue(36, out text3, out text7, out str);
				if (!string.IsNullOrEmpty(text7))
				{
					text += text7;
				}
				if (icOperator.checkSoftwareRegister() > 0)
				{
					text = text + "\r\n" + CommonStr.strAlreadyRegistered;
					text2 = text7 + "[" + CommonStr.strAlreadyRegistered + "]";
					text = text + "\r\n【施工和承建公司名称】：" + str;
				}
				else
				{
					text = text + "\r\n" + CommonStr.strUnRegistered;
					text2 = text7 + "[" + CommonStr.strUnRegistered + "]";
					text = text + "\r\n【施工和承建公司名称】：" + str;
				}
				text2 = string.Concat(new string[]
				{
					wgAppConfig.ProductTypeOfApp,
					"2012_",
					text2,
					"_V",
					Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf("."))
				});
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand("", oleDbConnection2))
					{
						oleDbConnection2.Open();
						oleDbCommand2.CommandText = " SELECT COUNT(*)  from t_b_door where f_doorEnabled=1";
						OleDbDataReader oleDbDataReader = oleDbCommand2.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							text = text + "\r\n【门数】：" + oleDbDataReader[0].ToString();
						}
						oleDbDataReader.Close();
					}
				}
				using (OleDbConnection oleDbConnection3 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand3 = new OleDbCommand("", oleDbConnection3))
					{
						oleDbConnection3.Open();
						oleDbCommand3.CommandText = "SELECT f_ControllerSN FROM t_b_Controller ";
						OleDbDataReader oleDbDataReader = oleDbCommand3.ExecuteReader();
						text5 = "";
						while (oleDbDataReader.Read())
						{
							text5 = text5 + "\r\n" + oleDbDataReader[0].ToString();
						}
						oleDbDataReader.Close();
					}
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text = text + "\r\n【控制器序列号S/N】：" + text5;
				}
				text += "\r\n【其他信息】：";
				using (OleDbConnection oleDbConnection4 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand4 = new OleDbCommand("", oleDbConnection4))
					{
						oleDbConnection4.Open();
						oleDbCommand4.CommandText = "SELECT count(*) FROM t_b_Consumer ";
						OleDbDataReader oleDbDataReader = oleDbCommand4.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							text = text + "\r\n【注册人数】：" + oleDbDataReader[0].ToString();
						}
						oleDbDataReader.Close();
					}
				}
				using (OleDbConnection oleDbConnection5 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand5 = new OleDbCommand("", oleDbConnection5))
					{
						oleDbConnection5.Open();
						oleDbCommand5.CommandText = "SELECT f_No, f_Value FROM t_a_SystemParam WHERE f_NO>100 ORDER BY f_No Asc ";
						OleDbDataReader oleDbDataReader = oleDbCommand5.ExecuteReader();
						if (oleDbDataReader.HasRows)
						{
							string text8 = "";
							text += "\r\n【启用功能(No,Val)】：\r\n";
							while (oleDbDataReader.Read())
							{
								string text9 = text;
								text = string.Concat(new string[]
								{
									text9,
									" (",
									oleDbDataReader["f_No"].ToString(),
									",",
									wgTools.SetObjToStr(oleDbDataReader["f_Value"]),
									")"
								});
								if (wgTools.SetObjToStr(oleDbDataReader["f_Value"]) == "1")
								{
									text8 = text8 + oleDbDataReader["f_No"].ToString() + ";";
								}
							}
							if (!string.IsNullOrEmpty(text8))
							{
								text = text + "\r\n【已启用】：" + text8;
							}
						}
						oleDbDataReader.Close();
					}
				}
				using (OleDbConnection oleDbConnection6 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand6 = new OleDbCommand("", oleDbConnection6))
					{
						oleDbConnection6.Open();
						oleDbCommand6.CommandText = "SELECT f_No, f_Value FROM t_a_SystemParam  WHERE f_NO<100  ORDER BY f_No Asc ";
						OleDbDataReader oleDbDataReader = oleDbCommand6.ExecuteReader();
						if (oleDbDataReader.HasRows)
						{
							text += "\r\n【参数值(No,Val)】：\r\n";
							while (oleDbDataReader.Read())
							{
								string text10 = text;
								text = string.Concat(new string[]
								{
									text10,
									" (",
									oleDbDataReader["f_No"].ToString(),
									",",
									wgTools.SetObjToStr(oleDbDataReader["f_Value"]),
									")"
								});
							}
						}
						oleDbDataReader.Close();
					}
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text += "\r\n【DrvInfo】：\r\n";
					text += text4;
				}
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)enumerator.Current;
							text += string.Format("\r\n【{0}】： ", CommonStr.strSystem);
							text += string.Format("\r\n{0} ", managementObject["Caption"]);
							text += string.Format("\r\n{0} ", managementObject["version"].ToString());
							text += string.Format("\r\n{0} ", managementObject["CSDVersion"]);
						}
					}
				}
				text += string.Format("\r\n【数据库版本】： ", new object[0]);
				string text11 = "Microsoft Access";
				if (!string.IsNullOrEmpty(text11))
				{
					text += string.Format("\r\n{0}", text11);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			text += "\r\n---------------------------------------------";
			text += "\r\n";
			text = text + "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
			subject = text2;
			return text;
		}

		private static bool sendMail(string strInfo, string mailSubject)
		{
			if (wgMail.sendMail2010(strInfo, mailSubject))
			{
				return true;
			}
			try
			{
				jmail.Message message;
				try
				{
					message = new MessageClass();
				}
				catch
				{
					using (Process process = new Process())
					{
						process.StartInfo.FileName = "regsvr32";
						process.StartInfo.Arguments = "/s \"" + Application.StartupPath + "\\n3k_jm.dll\"";
						process.Start();
						process.WaitForExit();
						message = new MessageClass();
					}
				}
				message.Logging = true;
				message.Charset = "GB2312";
				message.ContentType = "text/plain";
				message.AddRecipient("mail20050530@126.com", null, null);
				message.AddRecipientBCC("ccmail20050530@126.com", null);
				message.AddRecipientBCC("ccmail20050530@21cn.com", null);
				message.AddRecipientBCC("mail20050530@21cn.com", null);
				message.From = "ccmail20050530@126.com";
				message.MailServerUserName = "ccmail20050530@126.com";
				message.MailServerPassWord = "CCMAIL20055678";
				message.Subject = mailSubject;
				message.Body = strInfo + "\r\nsendMail";
				message.Priority = 1;
				message.Send("smtp.126.com", false);
				message.Close();
				return true;
			}
			catch
			{
			}
			return wgMail.sendMailBackup(strInfo, mailSubject);
		}

		private static bool sendMailBackup(string strInfo, string mailSubject)
		{
			try
			{
				jmail.Message message;
				try
				{
					message = new MessageClass();
				}
				catch
				{
					using (Process process = new Process())
					{
						process.StartInfo.FileName = "regsvr32";
						process.StartInfo.Arguments = "/s \"" + Application.StartupPath + "\\n3k_jm.dll\"";
						process.Start();
						process.WaitForExit();
						message = new MessageClass();
					}
				}
				message.Logging = true;
				message.Charset = "GB2312";
				message.ContentType = "text/plain";
				message.Logging = true;
				message.Charset = "GB2312";
				message.ContentType = "text/plain";
				message.AddRecipient("mail20050530@126.com", null, null);
				message.AddRecipientBCC("ccmail20050530@126.com", null);
				message.AddRecipientBCC("ccmail20050530@21cn.com", null);
				message.AddRecipientBCC("mail20050530@21cn.com", null);
				message.From = "ccmail20050530@21cn.com";
				message.MailServerUserName = "ccmail20050530@21cn.com";
				message.MailServerPassWord = "CCMAIL20055678";
				message.Subject = mailSubject;
				message.Body = strInfo + "\r\nsendMailBackup";
				message.Priority = 1;
				message.Send("smtp.21cn.com", false);
				return true;
			}
			catch
			{
			}
			return false;
		}

		private static bool sendMail2010(string strInfo, string mailSubject)
		{
			try
			{
				using (MailMessage mailMessage = new MailMessage())
				{
					mailMessage.BodyEncoding = Encoding.GetEncoding("gb2312");
					mailMessage.SubjectEncoding = Encoding.UTF8;
					mailMessage.To.Add("mail20050530@126.com");
					mailMessage.Bcc.Add("ccmail20050530@126.com");
					mailMessage.Bcc.Add("ccmail20050530@21cn.com");
					mailMessage.Bcc.Add("mail20050530@21cn.com");
					mailMessage.From = new MailAddress("ccmail20050530@126.com");
					mailMessage.Priority = MailPriority.High;
					mailMessage.Subject = mailSubject;
					mailMessage.Body = strInfo + "\r\nsendMail2010";
					new SmtpClient("smtp.126.com")
					{
						Credentials = new NetworkCredential("ccmail20050530", "CCMAIL20055678")
					}.Send(mailMessage);
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}
	}
}
