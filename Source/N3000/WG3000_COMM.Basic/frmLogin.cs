using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class frmLogin : frmN3000
	{
		private Point mouse_offset;

		private IContainer components;

		private Label label1;

		private Label label2;

		private TextBox txtOperatorName;

		private TextBox txtPassword;

		private Button btnOK;

		private Button btnExit;

		private Timer timer1;

		private Label label3;

		public frmLogin()
		{
			this.InitializeComponent();
			if (wgAppConfig.GetKeyVal("autologinName") != "")
			{
				this.txtOperatorName.Text = wgAppConfig.GetKeyVal("autologinName");
				this.txtPassword.Text = wgAppConfig.GetKeyVal("autologinPassword");
			}
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.ProductTypeOfApp != "AccessControl")
			{
				XMessageBox.Show("Wrong login file");
				this.btnExit_Click(null, null);
				return;
			}
			wgAppConfig.bFloorRoomManager = wgAppConfig.getParamValBoolByNO(145);
			if (wgAppConfig.bFloorRoomManager)
			{
				this.Text = CommonStr.strTitleHouse;
			}
			if (wgAppConfig.getSystemParamByName("Custom Title") != "")
			{
				this.Text = wgAppConfig.getSystemParamByName("Custom Title");
			}
			else if (wgAppConfig.GetKeyVal("Custom Title") != "")
			{
				this.Text = wgAppConfig.GetKeyVal("Custom Title");
			}
			wgAppConfig.IsLogin = false;
			this.timer1.Enabled = true;
		}

		private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && (sender.Equals(this.txtOperatorName) || sender.Equals(this.txtPassword) || sender.Equals(this.btnOK)))
			{
				this.btnOK_Click(sender, e);
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			if (wgAppConfig.GetKeyVal("autologinName") != "")
			{
				this.btnOK.PerformClick();
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (icOperator.checkSoftwareRegister() < 0)
			{
				using (dfrmRegister dfrmRegister = new dfrmRegister())
				{
					dfrmRegister.Text = CommonStr.strLicenseExpired;
					if (dfrmRegister.ShowDialog(this) != DialogResult.OK)
					{
						return;
					}
				}
			}
			if (icOperator.login(this.txtOperatorName.Text, this.txtPassword.Text))
			{
				base.DialogResult = DialogResult.OK;
				wgAppConfig.IsLogin = true;
				wgAppConfig.LoginTitle = this.Text;
				string text = "";
				text += string.Format("Ver: {0},", Application.ProductVersion);
				wgTools.CommPStr = wgTools.SetObjToStr(wgAppConfig.GetKeyVal("CommCurrent"));
				if (!string.IsNullOrEmpty(wgTools.CommPStr))
				{
					text += string.Format("Communication With Password,", new object[0]);
				}
				if (wgAppConfig.IsAccessDB)
				{
					if (icOperator.OperatorID == 1)
					{
						text += string.Format("{2}:{0}:{1},", icOperator.OperatorName, "MsAccess", CommonStr.strSuper);
					}
					else
					{
						text += string.Format("{0}:{1},", icOperator.OperatorName, "MsAccess");
					}
				}
				else
				{
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						if (icOperator.OperatorID == 1)
						{
							text += string.Format("{2}:{0}:{1},", icOperator.OperatorName, sqlConnection.Database, CommonStr.strSuper);
						}
						else
						{
							text += string.Format("{0}:{1},", icOperator.OperatorName, sqlConnection.Database);
						}
					}
					text += wgAppConfig.GetKeyVal("dbConnection");
				}
				wgAppConfig.wgLog(string.Format("{0},{1}", this.Text, text), EventLogEntryType.Information, null);
				base.Close();
				return;
			}
			SystemSounds.Beep.Play();
			XMessageBox.Show(this, CommonStr.strErrPwdOrName, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void frmLogin_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point mousePosition = Control.MousePosition;
				mousePosition.Offset(this.mouse_offset.X, this.mouse_offset.Y);
				base.Location = mousePosition;
			}
		}

		private void frmLogin_MouseDown(object sender, MouseEventArgs e)
		{
			this.mouse_offset = new Point(-e.X, -e.Y);
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmLogin));
			this.timer1 = new Timer(this.components);
			this.label3 = new Label();
			this.btnExit = new Button();
			this.btnOK = new Button();
			this.txtPassword = new TextBox();
			this.txtOperatorName = new TextBox();
			this.label2 = new Label();
			this.label1 = new Label();
			base.SuspendLayout();
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.Red;
			this.label3.Name = "label3";
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnOK.KeyPress += new KeyPressEventHandler(this.frmLogin_KeyPress);
			componentResourceManager.ApplyResources(this.txtPassword, "txtPassword");
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.KeyPress += new KeyPressEventHandler(this.frmLogin_KeyPress);
			componentResourceManager.ApplyResources(this.txtOperatorName, "txtOperatorName");
			this.txtOperatorName.Name = "txtOperatorName";
			this.txtOperatorName.KeyPress += new KeyPressEventHandler(this.frmLogin_KeyPress);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pLogin_bk;
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.txtOperatorName);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.Name = "frmLogin";
			base.TopMost = true;
			base.Load += new EventHandler(this.frmLogin_Load);
			base.KeyPress += new KeyPressEventHandler(this.frmLogin_KeyPress);
			base.MouseDown += new MouseEventHandler(this.frmLogin_MouseDown);
			base.MouseMove += new MouseEventHandler(this.frmLogin_MouseMove);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
