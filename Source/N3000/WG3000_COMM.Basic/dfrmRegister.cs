using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmRegister : frmN3000
	{
		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private TextBox txtCompanyName;

		private TextBox txtBuildingCompanyName;

		private TextBox txtRegisterCode;

		private Button btnOK;

		private Button Exit;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmRegister));
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.txtCompanyName = new TextBox();
			this.txtBuildingCompanyName = new TextBox();
			this.txtRegisterCode = new TextBox();
			this.btnOK = new Button();
			this.Exit = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.txtCompanyName, "txtCompanyName");
			this.txtCompanyName.Name = "txtCompanyName";
			componentResourceManager.ApplyResources(this.txtBuildingCompanyName, "txtBuildingCompanyName");
			this.txtBuildingCompanyName.Name = "txtBuildingCompanyName";
			componentResourceManager.ApplyResources(this.txtRegisterCode, "txtRegisterCode");
			this.txtRegisterCode.Name = "txtRegisterCode";
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.Exit.BackColor = Color.Transparent;
			this.Exit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.Exit, "Exit");
			this.Exit.ForeColor = Color.White;
			this.Exit.Name = "Exit";
			this.Exit.UseVisualStyleBackColor = false;
			this.Exit.Click += new EventHandler(this.Exit_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.Exit);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtRegisterCode);
			base.Controls.Add(this.txtBuildingCompanyName);
			base.Controls.Add(this.txtCompanyName);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmRegister";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmRegister()
		{
			this.InitializeComponent();
		}

		private void sendRegisterInfo()
		{
			Thread thread = new Thread(new ThreadStart(wgMail.sendMailOnce));
			thread.Start();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				string text = this.txtRegisterCode.Text.Trim();
				if (string.IsNullOrEmpty(text))
				{
					XMessageBox.Show(CommonStr.strInputRegisterSN, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					string value = this.txtCompanyName.Text.Trim();
					if (string.IsNullOrEmpty(value))
					{
						XMessageBox.Show(CommonStr.strInputCompanyName, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						value = this.txtBuildingCompanyName.Text.Trim();
						if (string.IsNullOrEmpty(value))
						{
							XMessageBox.Show(CommonStr.strInputBuildingCompanyName, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						else if (text == "2004")
						{
							string eName;
							string value2;
							string notes;
							wgAppConfig.getSystemParamValue(12, out eName, out value2, out notes);
							wgAppConfig.setSystemParamValue(12, eName, "200405", notes);
							wgAppConfig.setSystemParamValue(36, "", this.txtCompanyName.Text, this.txtBuildingCompanyName.Text);
							string keyVal = wgAppConfig.GetKeyVal("rgtries");
							if (keyVal != "")
							{
								wgAppConfig.UpdateKeyVal("rgtries", 1.ToString());
							}
							this.sendRegisterInfo();
							XMessageBox.Show(CommonStr.strRegisterSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							base.DialogResult = DialogResult.OK;
							base.Close();
						}
						else
						{
							string s = "";
							if (text.Length >= 6 && text.Substring(0, 4) == "2006")
							{
								s = text.Substring(4, 2);
							}
							int num = 0;
							int.TryParse(s, out num);
							if (num >= 1)
							{
								num *= 30;
								string eName;
								string value2;
								string notes;
								wgAppConfig.getSystemParamValue(12, out eName, out value2, out notes);
								value2 = (num + 1).ToString();
								eName = DateTime.Now.ToString("yyyy-MM-dd");
								wgAppConfig.setSystemParamValue(12, eName, value2, notes);
								wgAppConfig.setSystemParamValue(36, "", this.txtCompanyName.Text, this.txtBuildingCompanyName.Text);
								string keyVal2 = wgAppConfig.GetKeyVal("rgtries");
								if (keyVal2 != "")
								{
									wgAppConfig.UpdateKeyVal("rgtries", 1.ToString());
								}
								this.sendRegisterInfo();
								XMessageBox.Show(CommonStr.strRegisterSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								base.DialogResult = DialogResult.OK;
								base.Close();
							}
							else
							{
								XMessageBox.Show(CommonStr.strRegisterSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void Exit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
