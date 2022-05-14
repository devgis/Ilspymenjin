using System;
using System.ComponentModel;
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
	public class dfrmInterfaceLock : frmN3000
	{
		public string newPassword;

		public int operatorID;

		private Container components;

		public TextBox txtOperatorName;

		internal Label Label2;

		internal Label Label3;

		internal TextBox txtPassword;

		internal Button btnOk;

		public dfrmInterfaceLock()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmInterfaceLock));
			this.btnOk = new Button();
			this.txtOperatorName = new TextBox();
			this.txtPassword = new TextBox();
			this.Label2 = new Label();
			this.Label3 = new Label();
			base.SuspendLayout();
			this.btnOk.BackColor = Color.Transparent;
			this.btnOk.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOk, "btnOk");
			this.btnOk.ForeColor = Color.White;
			this.btnOk.Name = "btnOk";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			componentResourceManager.ApplyResources(this.txtOperatorName, "txtOperatorName");
			this.txtOperatorName.Name = "txtOperatorName";
			this.txtOperatorName.ReadOnly = true;
			this.txtOperatorName.TabStop = false;
			componentResourceManager.ApplyResources(this.txtPassword, "txtPassword");
			this.txtPassword.Name = "txtPassword";
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.Name = "Label2";
			this.Label3.BackColor = Color.Transparent;
			this.Label3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			base.AcceptButton = this.btnOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.ControlBox = false;
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtOperatorName);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label3);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmInterfaceLock";
			base.Load += new EventHandler(this.dfrmSetPassword_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (icOperator.login(this.txtOperatorName.Text, this.txtPassword.Text))
			{
				base.DialogResult = DialogResult.OK;
				wgAppConfig.IsLogin = true;
				wgAppConfig.wgLog(this.Text, EventLogEntryType.Information, null);
				base.Close();
				return;
			}
			SystemSounds.Beep.Play();
			XMessageBox.Show(this, CommonStr.strErrPwdOrName, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void dfrmSetPassword_Load(object sender, EventArgs e)
		{
			this.txtOperatorName.CharacterCasing = CharacterCasing.Lower;
			this.txtPassword.CharacterCasing = CharacterCasing.Lower;
		}
	}
}
