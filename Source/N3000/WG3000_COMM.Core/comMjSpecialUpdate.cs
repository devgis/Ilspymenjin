using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace WG3000_COMM.Core
{
	public class comMjSpecialUpdate : Component
	{
		private Container components;

		private static string downweb = "http://www.wiegand.com.cn/down/";

		public comMjSpecialUpdate(IContainer Container) : this()
		{
			Container.Add(this);
		}

		public comMjSpecialUpdate()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		private static string GetVersionFile()
		{
			string result = "";
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string text = Application.StartupPath + "\\PHOTO\\mj3kver" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".txt";
					webClient.DownloadFile(comMjSpecialUpdate.downweb + "mj3kver.txt", text);
					FileInfo fileInfo = new FileInfo(text);
					if (fileInfo.Exists)
					{
						using (StreamReader streamReader = new StreamReader(text))
						{
							result = streamReader.ReadToEnd();
						}
						fileInfo.Delete();
						return result;
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		private static string getSoftwareRar(string newfilename)
		{
			string result = "";
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string text = Application.StartupPath + "\\PHOTO\\mj3ksp" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".exe";
					webClient.DownloadFile(comMjSpecialUpdate.downweb + newfilename, text);
					FileInfo fileInfo = new FileInfo(text);
					if (fileInfo.Exists)
					{
						result = text;
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public static bool updateMjSpecialSoftware()
		{
			try
			{
				comMjSpecialUpdate.downweb = "http://www.wiegand.com.cn/down/";
				string versionFile = comMjSpecialUpdate.GetVersionFile();
				if (versionFile == "")
				{
					comMjSpecialUpdate.downweb = "http://www.wgaccess.com/down/";
					versionFile = comMjSpecialUpdate.GetVersionFile();
				}
				versionFile == "";
				DateTime value = DateTime.Parse("2011-5-1");
				wgAppConfig.UpdateKeyVal("RunTimeAt", (DateTime.Now.Subtract(value).Days + 31).ToString());
				wgAppConfig.wgLog("GetNewSpecialSoft: " + versionFile, EventLogEntryType.Information, null);
				string[] array = versionFile.Split(new char[]
				{
					';'
				});
				bool flag = false;
				if (wgTools.CmpProductVersion(array[1], wgAppConfig.GetKeyVal("NewSoftwareSpecialVersionInfo")) != 0)
				{
					flag = true;
				}
				if (!flag)
				{
					bool result = false;
					return result;
				}
				string softwareRar = comMjSpecialUpdate.getSoftwareRar(array[2]);
				if (softwareRar == "")
				{
					bool result = false;
					return result;
				}
				wgAppConfig.UpdateKeyVal("NewSoftwareSpecialVersionInfo", array[1]);
				Interaction.Shell(softwareRar, AppWinStyle.Hide, false, -1);
				Thread.Sleep(5000);
				Interaction.Shell(array[3], AppWinStyle.Hide, false, -1);
				Thread.Sleep(5000);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return false;
		}
	}
}
