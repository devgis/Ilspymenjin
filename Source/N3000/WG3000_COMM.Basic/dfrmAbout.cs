using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmAbout : frmN3000
	{
		private bool bDispSpecial;

		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private TextBox textBoxDescription;

		internal Button btnRegister;

		internal Button btnClose;

		private Label label5;

		public string AssemblyTitle
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (customAttributes.Length > 0)
				{
					AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
					if (assemblyTitleAttribute.Title != "")
					{
						return assemblyTitleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)customAttributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
			}
		}

		public dfrmAbout()
		{
			this.InitializeComponent();
			this.label1.Text = this.AssemblyProduct;
			this.label2.Text = string.Format("Version {0}", this.AssemblyVersion);
			this.label3.Text = this.AssemblyCopyright;
			this.label4.Text = "";
			this.textBoxDescription.Text = this.AssemblyDescription;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void dfrmAbout_Load(object sender, EventArgs e)
		{
			this.textBoxDescription.ForeColor = Color.White;
			this.label1.Text = base.Owner.Text;
			this.label2.Text = string.Format("Software Version: {0}", this.AssemblyVersion);
			this.label3.Text = "Database Version: " + wgAppConfig.getSystemParamByNO(9) + (wgAppConfig.IsAccessDB ? "  [Microsoft Access]" : "[MS Sql Server]");
			this.label4.Text = string.Format(".Net Framework {0} ", Environment.Version.ToString());
			this.label5.Text = "";
			try
			{
				if (!string.IsNullOrEmpty(wgAppConfig.getSystemParamByNO(12)) && wgAppConfig.getSystemParamByNO(12) == "200405")
				{
					TextBox expr_CC = this.textBoxDescription;
					expr_CC.Text = expr_CC.Text + "\r\n" + CommonStr.strRegisterAlready;
					if (!string.IsNullOrEmpty(wgAppConfig.getSystemParamByNO(36)))
					{
						TextBox expr_FA = this.textBoxDescription;
						expr_FA.Text = expr_FA.Text + "\r\n" + wgAppConfig.getSystemParamByNO(36);
					}
					TextBox expr_11C = this.textBoxDescription;
					expr_11C.Text = expr_11C.Text + "\r\n" + CommonStr.strWelcomeToUse;
					this.label5.Text = this.textBoxDescription.Text.Replace("\r\n", "\r\n\r\n");
					this.btnRegister.Text = CommonStr.strRegisterAgain;
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnRegister_Click(object sender, EventArgs e)
		{
			using (dfrmRegister dfrmRegister = new dfrmRegister())
			{
				if (dfrmRegister.ShowDialog(this) == DialogResult.OK)
				{
					this.textBoxDescription.Text = "";
					this.dfrmAbout_Load(null, null);
				}
			}
		}

		private void dfrmAbout_KeyDown(object sender, KeyEventArgs e)
		{
			if (!this.bDispSpecial && e.Control && e.Shift && e.KeyValue == 87)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.bDispSpecial = true;
				TextBox expr_4C = this.textBoxDescription;
				expr_4C.Text += "\r\n2010 版权所有(C) 深圳市微耕实业有限公司\r\n保留所有权利";
				this.label5.Text = this.textBoxDescription.Text.Replace("\r\n", "\r\n\r\n");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmAbout));
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.textBoxDescription = new TextBox();
			this.btnRegister = new Button();
			this.btnClose = new Button();
			this.label5 = new Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			this.textBoxDescription.BackColor = Color.FromArgb(128, 131, 156);
			this.textBoxDescription.BorderStyle = BorderStyle.None;
			componentResourceManager.ApplyResources(this.textBoxDescription, "textBoxDescription");
			this.textBoxDescription.ForeColor = Color.White;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnRegister, "btnRegister");
			this.btnRegister.BackColor = Color.Transparent;
			this.btnRegister.BackgroundImage = Resources.pMain_button_normal;
			this.btnRegister.ForeColor = Color.White;
			this.btnRegister.Name = "btnRegister";
			this.btnRegister.UseVisualStyleBackColor = false;
			this.btnRegister.Click += new EventHandler(this.btnRegister_Click);
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.ForeColor = Color.White;
			this.label5.Name = "label5";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.label5);
			base.Controls.Add(this.btnRegister);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.textBoxDescription);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmAbout";
			base.Load += new EventHandler(this.dfrmAbout_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmAbout_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
