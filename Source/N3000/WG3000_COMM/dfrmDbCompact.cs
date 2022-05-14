using JRO;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
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
	public class dfrmDbCompact : frmN3000
	{
		private Container components;

		internal Button cmdCompactDatabase;

		private TextBox txtDirectory;

		internal Button btnCancel;

		public dfrmDbCompact()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmDbCompact));
			this.cmdCompactDatabase = new Button();
			this.btnCancel = new Button();
			this.txtDirectory = new TextBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.cmdCompactDatabase, "cmdCompactDatabase");
			this.cmdCompactDatabase.BackColor = Color.Transparent;
			this.cmdCompactDatabase.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCompactDatabase.ForeColor = Color.White;
			this.cmdCompactDatabase.Name = "cmdCompactDatabase";
			this.cmdCompactDatabase.UseVisualStyleBackColor = false;
			this.cmdCompactDatabase.Click += new EventHandler(this.cmdCompactDatabase_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.txtDirectory, "txtDirectory");
			this.txtDirectory.Name = "txtDirectory";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtDirectory);
			base.Controls.Add(this.cmdCompactDatabase);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmDbCompact";
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.Load += new EventHandler(this.dfrmDbCompact_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmDbCompact_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private bool sqlBackup2010()
		{
			string text = null;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				SqlConnection.ClearAllPools();
				string connectionString = wgAppConfig.dbConString;
				SqlConnection sqlConnection = new SqlConnection(connectionString);
				try
				{
					text = sqlConnection.Database;
				}
				catch (Exception)
				{
				}
				sqlConnection.Close();
				if (text == null)
				{
					bool result = false;
					return result;
				}
				string text2;
				if (this.txtDirectory.Visible)
				{
					if (this.txtDirectory.Text.Length <= 0 || !(this.txtDirectory.Text.Substring(0, 2) == "\\\\"))
					{
						bool result = false;
						return result;
					}
					if (this.txtDirectory.Text.Substring(this.txtDirectory.Text.Length - 1, 1) == "\\")
					{
						text2 = string.Format("{0}{1}_sql_{2}.bak", this.txtDirectory.Text, text, DateAndTime.Now.ToString("yyyyMMdd_HHmmss"));
					}
					else
					{
						text2 = string.Format("{0}\\{1}_sql_{2}.bak", this.txtDirectory.Text, text, DateAndTime.Now.ToString("yyyyMMdd_HHmmss"));
					}
				}
				else
				{
					text2 = string.Format("{0}_sql_{1}.bak", text, DateAndTime.Now.ToString("yyyyMMdd_HHmmss"));
				}
				connectionString = wgAppConfig.dbConString.Replace(string.Format("initial catalog={0}", text), string.Format("initial catalog={0}", "master"));
				SqlConnection sqlConnection2 = new SqlConnection(connectionString);
				try
				{
					sqlConnection2.Open();
					string cmdText = "SELECT  SERVERPROPERTY('productversion'), SERVERPROPERTY ('productlevel'), SERVERPROPERTY ('edition')";
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection2))
					{
						sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						object obj = sqlCommand.ExecuteScalar();
						if (obj != null)
						{
							if (this.txtDirectory.Visible)
							{
								if (this.txtDirectory.Text.Length <= 0 || !(this.txtDirectory.Text.Substring(0, 2) == "\\\\"))
								{
									bool result = false;
									return result;
								}
								if (this.txtDirectory.Text.Substring(this.txtDirectory.Text.Length - 1, 1) == "\\")
								{
									text2 = string.Format("{0}{1}_sql_{2}_{3}.bak", new object[]
									{
										this.txtDirectory.Text,
										text,
										wgTools.SetObjToStr(obj),
										DateAndTime.Now.ToString("yyyyMMdd_HHmmss")
									});
								}
								else
								{
									text2 = string.Format("{0}\\{1}_sql_{2}_{3}.bak", new object[]
									{
										this.txtDirectory.Text,
										text,
										wgTools.SetObjToStr(obj),
										DateAndTime.Now.ToString("yyyyMMdd_HHmmss")
									});
								}
							}
							else
							{
								text2 = string.Format("{0}_sql_{1}_{2}.bak", text, wgTools.SetObjToStr(obj), DateAndTime.Now.ToString("yyyyMMdd_HHmmss"));
							}
						}
					}
					cmdText = string.Format("BACKUP DATABASE [{0}] TO DISK = {1}", text, wgTools.PrepareStr(text2));
					using (SqlCommand sqlCommand2 = new SqlCommand(cmdText, sqlConnection2))
					{
						sqlCommand2.CommandTimeout = wgAppConfig.dbCommandTimeout;
						sqlCommand2.ExecuteNonQuery();
					}
					XMessageBox.Show("OK!\r\n\r\n" + text2);
					wgAppConfig.wgLog(this.Text + " OK :  " + text2);
					base.Close();
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				finally
				{
					sqlConnection2.Dispose();
				}
			}
			catch (Exception ex2)
			{
				string cultureInfoStr;
				if ((cultureInfoStr = wgAppConfig.CultureInfoStr) != null)
				{
					if (cultureInfoStr == "zh-CHS")
					{
						XMessageBox.Show("失败.\r\n\r\n" + ex2.ToString());
						goto IL_3E4;
					}
					if (cultureInfoStr == "zh-CHT")
					{
						XMessageBox.Show("失敗.\r\n\r\n" + ex2.ToString());
						goto IL_3E4;
					}
				}
				XMessageBox.Show("Failed.  \r\n\r\n" + ex2.ToString());
				IL_3E4:
				wgAppConfig.wgLog(this.Text + "  Failed. :  \r\n\r\n" + ex2.ToString());
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
			return true;
		}

		public void cmdCompactDatabase_Click(object sender, EventArgs e)
		{
			wgAppConfig.wgLog(this.Text + " ......");
			if (wgAppConfig.IsAccessDB)
			{
				this.cmdCompactDatabase_Click_Acc(sender, e);
				return;
			}
			this.sqlBackup2010();
		}

		public void cmdCompactDatabase_Click_Acc(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				JetEngine jetEngine;
				try
				{
					jetEngine = new JetEngineClass();
				}
				catch
				{
					using (Process process = new Process())
					{
						process.StartInfo.FileName = "regsvr32";
						process.StartInfo.Arguments = "/s \"" + Application.StartupPath + "\\msjro.dll\"";
						process.Start();
						process.WaitForExit();
					}
					jetEngine = new JetEngineClass();
				}
				Thread.Sleep(500);
				string text = "";
				string accessDbName = wgAppConfig.accessDbName;
				string fileName;
				if (string.IsNullOrEmpty(accessDbName))
				{
					fileName = DateTime.Now.ToString("yyyy-MM-dd_HHmmss_ff") + ".mdb";
				}
				else
				{
					fileName = accessDbName + DateTime.Now.ToString("-yyyy-MM-dd_HHmmss_ff") + ".mdb";
				}
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.FileName = fileName;
					saveFileDialog.Filter = " (*.mdb)|*.mdb";
					saveFileDialog.InitialDirectory = Application.StartupPath + ".\\BACKUP";
					string keyVal = wgAppConfig.GetKeyVal("BackupPathOfAccessDB");
					if (!string.IsNullOrEmpty(keyVal))
					{
						try
						{
							saveFileDialog.InitialDirectory = keyVal;
						}
						catch
						{
						}
					}
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						text = saveFileDialog.FileName;
						wgAppConfig.UpdateKeyVal("BackupPathOfAccessDB", text);
						using (dfrmWait dfrmWait = new dfrmWait())
						{
							dfrmWait.Show();
							dfrmWait.Refresh();
							wgAppConfig.backupBeforeExitByJustCopy();
							jetEngine.CompactDatabase(wgAppConfig.dbConString, string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=admin;Password=;JET OLEDB:Database Password=168168;Jet OLEDB:Engine Type=5", text));
							FileInfo fileInfo = new FileInfo(text);
							fileInfo.CopyTo(Application.StartupPath + string.Format("\\{0}.mdb", wgAppConfig.accessDbName), true);
							dfrmWait.Hide();
						}
						XMessageBox.Show(string.Concat(new string[]
						{
							this.Text,
							"  ",
							CommonStr.strSuccessfully,
							"\r\n\r\n",
							text
						}));
						base.Close();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine("Backup Access: " + ex.ToString());
				wgAppConfig.wgLog("Backup Access: " + ex.ToString());
				XMessageBox.Show(string.Concat(new string[]
				{
					this.Text,
					" ",
					CommonStr.strFailed,
					" ",
					ex.ToString()
				}));
			}
			finally
			{
				Directory.SetCurrentDirectory(Application.StartupPath);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void dfrmDbCompact_Load(object sender, EventArgs e)
		{
		}

		private void dfrmDbCompact_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.txtDirectory.Visible = true;
			}
		}
	}
}
