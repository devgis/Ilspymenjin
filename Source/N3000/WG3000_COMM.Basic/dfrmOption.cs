using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmOption : frmN3000
	{
		private IContainer components;

		private GroupBox groupBox1;

		private CheckBox chkAutoLoginOnly;

		internal Button cmdCancel;

		internal Button cmdOK;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private ComboBox cboOnlyDate;

		private Label label2;

		private Label label1;

		private TextBox txtOnlyDate;

		private Button btnRefreshOnlyDate;

		private Button btnRefreshDateTimeWeek;

		private Button btnRefreshDateWeek;

		private TextBox txtDateTimeWeek;

		private ComboBox cboDateTimeWeek;

		private TextBox txtDateWeek;

		private ComboBox cboDateWeek;

		private Label label3;

		private Button btnRefreshDateTime;

		private TextBox txtDateTime;

		private ComboBox cboDateTime;

		private Label label4;

		private CheckBox chkHouse;

		private ComboBox cboLanguage;

		private Label label5;

		internal Button btnOption;

		private CheckBox chkHideLogin;

		public dfrmOption()
		{
			this.InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (this.cboLanguage.SelectedIndex == 0)
			{
				wgAppConfig.UpdateKeyVal("Language", "");
			}
			else if (this.cboLanguage.SelectedIndex == 1)
			{
				wgAppConfig.UpdateKeyVal("Language", "zh-CHS");
			}
			else if (this.cboLanguage.SelectedIndex >= 2)
			{
				wgAppConfig.UpdateKeyVal("Language", this.cboLanguage.Items[this.cboLanguage.SelectedIndex].ToString());
			}
			if (this.chkAutoLoginOnly.Checked)
			{
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM t_s_Operator WHERE f_OperatorID= " + icOperator.OperatorID, oleDbConnection))
						{
							oleDbConnection.Open();
							OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
							if (oleDbDataReader.Read())
							{
								wgAppConfig.UpdateKeyVal("autologinName", wgTools.SetObjToStr(oleDbDataReader["f_OperatorName"]));
								wgAppConfig.UpdateKeyVal("autologinPassword", wgTools.SetObjToStr(oleDbDataReader["f_Password"]));
							}
							oleDbDataReader.Close();
						}
						goto IL_1D3;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM t_s_Operator WHERE f_OperatorID= " + icOperator.OperatorID, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							wgAppConfig.UpdateKeyVal("autologinName", wgTools.SetObjToStr(sqlDataReader["f_OperatorName"]));
							wgAppConfig.UpdateKeyVal("autologinPassword", wgTools.SetObjToStr(sqlDataReader["f_Password"]));
						}
						sqlDataReader.Close();
					}
					goto IL_1D3;
				}
			}
			wgAppConfig.UpdateKeyVal("autologinName", "");
			wgAppConfig.UpdateKeyVal("autologinPassword", "");
			IL_1D3:
			wgAppConfig.setSystemParamValueBool(145, this.chkHouse.Checked);
			wgAppConfig.UpdateKeyVal("HideGettingStartedWhenLogin", this.chkHideLogin.Checked ? "0" : "1");
			if (this.tabControl1.Visible)
			{
				if (wgTools.IsValidDateTimeFormat(this.cboOnlyDate.Text))
				{
					wgAppConfig.UpdateKeyVal("DisplayFormat_DateYMD", this.cboOnlyDate.Text);
				}
				if (wgTools.IsValidDateTimeFormat(this.cboDateWeek.Text))
				{
					wgAppConfig.UpdateKeyVal("DisplayFormat_DateYMDWeek", this.cboDateWeek.Text);
				}
				if (wgTools.IsValidDateTimeFormat(this.cboDateTime.Text))
				{
					wgAppConfig.UpdateKeyVal("DisplayFormat_DateYMDHMS", this.cboDateTime.Text);
				}
				if (wgTools.IsValidDateTimeFormat(this.cboDateTimeWeek.Text))
				{
					wgAppConfig.UpdateKeyVal("DisplayFormat_DateYMDHMSWeek", this.cboDateTimeWeek.Text);
				}
			}
			if (XMessageBox.Show(CommonStr.strUpdateSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void software_language_check()
		{
			this.cboLanguage.Items.Clear();
			this.cboLanguage.Items.Add("English");
			this.cboLanguage.SelectedIndex = 0;
			this.cboLanguage.Items.Add("简体中文[zh-CHS]");
			if (wgAppConfig.GetKeyVal("Language") == "zh-CHS")
			{
				this.cboLanguage.SelectedIndex = 1;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo directoryInfo2 = directories[i];
				FileInfo[] files = directoryInfo2.GetFiles();
				for (int j = 0; j < files.Length; j++)
				{
					FileInfo fileInfo = files[j];
					if (fileInfo.Name == "N3000.resources.dll")
					{
						wgTools.WriteLine(fileInfo.FullName);
						if (directoryInfo2.Name != "zh-CHS")
						{
							this.cboLanguage.Items.Add(directoryInfo2.Name);
							if (wgAppConfig.GetKeyVal("Language") == directoryInfo2.Name)
							{
								this.cboLanguage.SelectedIndex = this.cboLanguage.Items.Count - 1;
							}
						}
					}
				}
			}
		}

		private void dfrmOption_Load(object sender, EventArgs e)
		{
			this.chkHideLogin.Checked = (wgAppConfig.GetKeyVal("HideGettingStartedWhenLogin") != "1");
			this.chkAutoLoginOnly.Checked = (wgAppConfig.GetKeyVal("autologinName") != "");
			this.software_language_check();
			this.tabPage1.BackColor = this.BackColor;
			this.tabPage2.BackColor = this.BackColor;
			this.chkHouse.Checked = wgAppConfig.bFloorRoomManager;
			if (icOperator.OperatorID == 1 && (wgAppConfig.GetKeyVal("AllowUploadUserName") == "1" || !string.IsNullOrEmpty(wgAppConfig.getSystemParamByNO(41)) || wgAppConfig.getParamValBoolByNO(147)))
			{
				this.btnOption.Visible = true;
			}
			this.cboDateTime.Items.Clear();
			this.cboDateTime.Items.AddRange(new string[]
			{
				"yyyy-MM-dd HH:mm:ss",
				"yyyy-MM-dd HH:mm:ss",
				"yyyy-M-d HH:mm:ss",
				"yy-M-d HH:mm:ss",
				"yy-MM-dd HH:mm:ss",
				"HH:mm:ss dd-MMM-yy",
				"HH:mm:ss d/M/yyyy",
				"HH:mm:ss d/M/yy",
				"HH:mm:ss dd/MM/yy",
				"yy/M/d HH:mm:ss",
				"yy/MM/dd HH:mm:ss",
				"yyyy/MM/dd HH:mm:ss",
				"HH:mm:ss M/d/yyyy",
				"HH:mm:ss M/d/yy",
				"HH:mm:ss MM/dd/yyyy"
			});
			this.cboDateTimeWeek.Items.Clear();
			this.cboDateTimeWeek.Items.AddRange(new string[]
			{
				"yyyy-MM-dd HH:mm:ss dddd",
				"yyyy-MM-dd HH:mm:ss ddd",
				"yyyy-M-d HH:mm:ss ddd",
				"yy-M-d HH:mm:ss ddd",
				"yy-MM-dd HH:mm:ss ddd",
				"HH:mm:ss dd-MMM-yy ddd",
				"HH:mm:ss d/M/yyyy ddd",
				"HH:mm:ss d/M/yy ddd",
				"HH:mm:ss dd/MM/yy ddd",
				"yy/M/d HH:mm:ss ddd",
				"yy/MM/dd HH:mm:ss ddd",
				"yyyy/MM/dd HH:mm:ss ddd",
				"HH:mm:ss M/d/yyyy ddd",
				"HH:mm:ss M/d/yy ddd",
				"HH:mm:ss MM/dd/yyyy ddd"
			});
			this.cboDateWeek.Items.Clear();
			this.cboDateWeek.Items.AddRange(new string[]
			{
				"yyyy-MM-dd dddd",
				"yyyy-MM-dd ddd",
				"yyyy-M-d ddd",
				"yy-M-d ddd",
				"yy-MM-dd ddd",
				"dd-MMM-yy ddd",
				"d/M/yyyy ddd",
				"d/M/yy ddd",
				"dd/MM/yy ddd",
				"yy/M/d ddd",
				"yy/MM/dd ddd",
				"yyyy/MM/dd ddd",
				"M/d/yyyy ddd",
				"M/d/yy ddd",
				"MM/dd/yyyy ddd"
			});
			this.cboOnlyDate.Items.Clear();
			this.cboOnlyDate.Items.AddRange(new string[]
			{
				"yyyy-MM-dd",
				"yyyy-M-d",
				"yy-M-d",
				"yy-MM-dd",
				"dd-MMM-yy",
				"d/M/yyyy",
				"d/M/yy",
				"dd/MM/yy",
				"yy/M/d",
				"yy/MM/dd",
				"yyyy/MM/dd",
				"M/d/yyyy",
				"M/d/yy",
				"MM/dd/yyyy"
			});
		}

		private void btnRefreshOnlyDate_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cboOnlyDate.Text))
				{
					this.txtOnlyDate.Text = "";
				}
				else
				{
					this.txtOnlyDate.Text = DateTime.Now.ToString(this.cboOnlyDate.Text);
					DateTime dateTime;
					if (!DateTime.TryParse(this.txtOnlyDate.Text, out dateTime))
					{
						this.txtOnlyDate.Text = CommonStr.strDateTimeFormatErr;
					}
				}
			}
			catch (Exception)
			{
				this.txtOnlyDate.Text = CommonStr.strDateTimeFormatErr;
			}
		}

		private void btnRefreshDateWeek_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cboDateWeek.Text))
				{
					this.txtDateWeek.Text = "";
				}
				else
				{
					this.txtDateWeek.Text = DateTime.Now.ToString(this.cboDateWeek.Text);
					DateTime dateTime;
					if (!DateTime.TryParse(this.txtDateWeek.Text, out dateTime))
					{
						this.txtDateWeek.Text = CommonStr.strDateTimeFormatErr;
					}
				}
			}
			catch (Exception)
			{
				this.txtDateWeek.Text = CommonStr.strDateTimeFormatErr;
			}
		}

		private void btnRefreshDateTime_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cboDateTime.Text))
				{
					this.txtDateTime.Text = "";
				}
				else
				{
					this.txtDateTime.Text = DateTime.Now.ToString(this.cboDateTime.Text);
					DateTime dateTime;
					if (!DateTime.TryParse(this.txtDateTime.Text, out dateTime))
					{
						this.txtDateTime.Text = CommonStr.strDateTimeFormatErr;
					}
				}
			}
			catch (Exception)
			{
				this.txtDateTime.Text = CommonStr.strDateTimeFormatErr;
			}
		}

		private void btnRefreshDateTimeWeek_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cboDateTimeWeek.Text))
				{
					this.txtDateTimeWeek.Text = "";
				}
				else
				{
					this.txtDateTimeWeek.Text = DateTime.Now.ToString(this.cboDateTimeWeek.Text);
					DateTime dateTime;
					if (!DateTime.TryParse(this.txtDateTimeWeek.Text, out dateTime))
					{
						this.txtDateTimeWeek.Text = CommonStr.strDateTimeFormatErr;
					}
				}
			}
			catch (Exception)
			{
				this.txtDateTimeWeek.Text = CommonStr.strDateTimeFormatErr;
			}
		}

		private void funcCtrlShiftQ()
		{
			if (!this.btnOption.Visible)
			{
				this.btnOption.Visible = true;
				return;
			}
			this.tabControl1.Visible = true;
			base.Size = new Size(640, 480);
			try
			{
				wgAppConfig.GetKeyVal("Language");
				this.cboOnlyDate.Text = wgAppConfig.GetKeyVal("DisplayFormat_DateYMD");
				this.cboDateWeek.Text = wgAppConfig.GetKeyVal("DisplayFormat_DateYMDWeek");
				this.cboDateTime.Text = wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMS");
				this.cboDateTimeWeek.Text = wgAppConfig.GetKeyVal("DisplayFormat_DateYMDHMSWeek");
			}
			catch (Exception)
			{
			}
		}

		private void dfrmOption_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.funcCtrlShiftQ();
			}
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
			using (dfrmOptionAdvanced dfrmOptionAdvanced = new dfrmOptionAdvanced())
			{
				dfrmOptionAdvanced.ShowDialog();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOption));
			this.cmdOK = new Button();
			this.cmdCancel = new Button();
			this.chkHideLogin = new CheckBox();
			this.btnOption = new Button();
			this.chkHouse = new CheckBox();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.btnRefreshDateTime = new Button();
			this.btnRefreshDateTimeWeek = new Button();
			this.btnRefreshDateWeek = new Button();
			this.btnRefreshOnlyDate = new Button();
			this.txtDateTime = new TextBox();
			this.txtDateTimeWeek = new TextBox();
			this.cboDateTime = new ComboBox();
			this.cboDateTimeWeek = new ComboBox();
			this.txtDateWeek = new TextBox();
			this.cboDateWeek = new ComboBox();
			this.label4 = new Label();
			this.txtOnlyDate = new TextBox();
			this.label3 = new Label();
			this.cboOnlyDate = new ComboBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.tabPage2 = new TabPage();
			this.chkAutoLoginOnly = new CheckBox();
			this.groupBox1 = new GroupBox();
			this.label5 = new Label();
			this.cboLanguage = new ComboBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			componentResourceManager.ApplyResources(this.chkHideLogin, "chkHideLogin");
			this.chkHideLogin.ForeColor = Color.White;
			this.chkHideLogin.Name = "chkHideLogin";
			this.chkHideLogin.UseVisualStyleBackColor = true;
			this.btnOption.BackColor = Color.Transparent;
			this.btnOption.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOption, "btnOption");
			this.btnOption.ForeColor = Color.White;
			this.btnOption.Name = "btnOption";
			this.btnOption.UseVisualStyleBackColor = false;
			this.btnOption.Click += new EventHandler(this.btnOption_Click);
			componentResourceManager.ApplyResources(this.chkHouse, "chkHouse");
			this.chkHouse.BackColor = Color.Transparent;
			this.chkHouse.ForeColor = Color.White;
			this.chkHouse.Name = "chkHouse";
			this.chkHouse.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.btnRefreshDateTime);
			this.tabPage1.Controls.Add(this.btnRefreshDateTimeWeek);
			this.tabPage1.Controls.Add(this.btnRefreshDateWeek);
			this.tabPage1.Controls.Add(this.btnRefreshOnlyDate);
			this.tabPage1.Controls.Add(this.txtDateTime);
			this.tabPage1.Controls.Add(this.txtDateTimeWeek);
			this.tabPage1.Controls.Add(this.cboDateTime);
			this.tabPage1.Controls.Add(this.cboDateTimeWeek);
			this.tabPage1.Controls.Add(this.txtDateWeek);
			this.tabPage1.Controls.Add(this.cboDateWeek);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.txtOnlyDate);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.cboOnlyDate);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.ForeColor = Color.White;
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.btnRefreshDateTime.BackgroundImage = Resources.pMain_button_normal;
			this.btnRefreshDateTime.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnRefreshDateTime, "btnRefreshDateTime");
			this.btnRefreshDateTime.Name = "btnRefreshDateTime";
			this.btnRefreshDateTime.UseVisualStyleBackColor = true;
			this.btnRefreshDateTime.Click += new EventHandler(this.btnRefreshDateTime_Click);
			this.btnRefreshDateTimeWeek.BackgroundImage = Resources.pMain_button_normal;
			this.btnRefreshDateTimeWeek.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnRefreshDateTimeWeek, "btnRefreshDateTimeWeek");
			this.btnRefreshDateTimeWeek.Name = "btnRefreshDateTimeWeek";
			this.btnRefreshDateTimeWeek.UseVisualStyleBackColor = true;
			this.btnRefreshDateTimeWeek.Click += new EventHandler(this.btnRefreshDateTimeWeek_Click);
			this.btnRefreshDateWeek.BackgroundImage = Resources.pMain_button_normal;
			this.btnRefreshDateWeek.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnRefreshDateWeek, "btnRefreshDateWeek");
			this.btnRefreshDateWeek.Name = "btnRefreshDateWeek";
			this.btnRefreshDateWeek.UseVisualStyleBackColor = true;
			this.btnRefreshDateWeek.Click += new EventHandler(this.btnRefreshDateWeek_Click);
			this.btnRefreshOnlyDate.BackgroundImage = Resources.pMain_button_normal;
			this.btnRefreshOnlyDate.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnRefreshOnlyDate, "btnRefreshOnlyDate");
			this.btnRefreshOnlyDate.Name = "btnRefreshOnlyDate";
			this.btnRefreshOnlyDate.UseVisualStyleBackColor = true;
			this.btnRefreshOnlyDate.Click += new EventHandler(this.btnRefreshOnlyDate_Click);
			componentResourceManager.ApplyResources(this.txtDateTime, "txtDateTime");
			this.txtDateTime.Name = "txtDateTime";
			componentResourceManager.ApplyResources(this.txtDateTimeWeek, "txtDateTimeWeek");
			this.txtDateTimeWeek.Name = "txtDateTimeWeek";
			this.cboDateTime.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboDateTime, "cboDateTime");
			this.cboDateTime.Name = "cboDateTime";
			this.cboDateTime.SelectedIndexChanged += new EventHandler(this.btnRefreshDateTime_Click);
			this.cboDateTimeWeek.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboDateTimeWeek, "cboDateTimeWeek");
			this.cboDateTimeWeek.Name = "cboDateTimeWeek";
			this.cboDateTimeWeek.SelectedIndexChanged += new EventHandler(this.btnRefreshDateTimeWeek_Click);
			componentResourceManager.ApplyResources(this.txtDateWeek, "txtDateWeek");
			this.txtDateWeek.Name = "txtDateWeek";
			this.cboDateWeek.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboDateWeek, "cboDateWeek");
			this.cboDateWeek.Name = "cboDateWeek";
			this.cboDateWeek.SelectedIndexChanged += new EventHandler(this.btnRefreshDateWeek_Click);
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.txtOnlyDate, "txtOnlyDate");
			this.txtOnlyDate.Name = "txtOnlyDate";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.cboOnlyDate.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboOnlyDate, "cboOnlyDate");
			this.cboOnlyDate.Name = "cboOnlyDate";
			this.cboOnlyDate.SelectedIndexChanged += new EventHandler(this.btnRefreshOnlyDate_Click);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAutoLoginOnly, "chkAutoLoginOnly");
			this.chkAutoLoginOnly.BackColor = Color.Transparent;
			this.chkAutoLoginOnly.ForeColor = Color.White;
			this.chkAutoLoginOnly.Name = "chkAutoLoginOnly";
			this.chkAutoLoginOnly.UseVisualStyleBackColor = false;
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cboLanguage);
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.ForeColor = Color.White;
			this.label5.Name = "label5";
			this.cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboLanguage.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboLanguage, "cboLanguage");
			this.cboLanguage.Name = "cboLanguage";
			base.AcceptButton = this.cmdOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.cmdCancel;
			base.Controls.Add(this.chkHideLogin);
			base.Controls.Add(this.btnOption);
			base.Controls.Add(this.chkHouse);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.chkAutoLoginOnly);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmOption";
			base.Load += new EventHandler(this.dfrmOption_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmOption_KeyDown);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
