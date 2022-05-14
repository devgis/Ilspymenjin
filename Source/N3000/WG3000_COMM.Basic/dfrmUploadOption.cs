using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmUploadOption : frmN3000
	{
		private IContainer components;

		private CheckBox chkBasicConfiguration;

		private CheckBox chkAccessPrivilege;

		private Button btnCancel;

		private Button btnOK;

		public int checkVal;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmUploadOption));
			this.chkBasicConfiguration = new CheckBox();
			this.chkAccessPrivilege = new CheckBox();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.chkBasicConfiguration, "chkBasicConfiguration");
			this.chkBasicConfiguration.BackColor = Color.Transparent;
			this.chkBasicConfiguration.Checked = true;
			this.chkBasicConfiguration.CheckState = CheckState.Checked;
			this.chkBasicConfiguration.ForeColor = Color.White;
			this.chkBasicConfiguration.Name = "chkBasicConfiguration";
			this.chkBasicConfiguration.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.chkAccessPrivilege, "chkAccessPrivilege");
			this.chkAccessPrivilege.BackColor = Color.Transparent;
			this.chkAccessPrivilege.Checked = true;
			this.chkAccessPrivilege.CheckState = CheckState.Checked;
			this.chkAccessPrivilege.ForeColor = Color.White;
			this.chkAccessPrivilege.Name = "chkAccessPrivilege";
			this.chkAccessPrivilege.UseVisualStyleBackColor = false;
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.chkAccessPrivilege);
			base.Controls.Add(this.chkBasicConfiguration);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmUploadOption";
			base.Load += new EventHandler(this.dfrmUploadOption_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmUploadOption()
		{
			this.InitializeComponent();
		}

		private void dfrmUploadOption_Load(object sender, EventArgs e)
		{
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.checkVal = 0;
			if (this.chkBasicConfiguration.Checked)
			{
				this.checkVal++;
			}
			if (this.chkAccessPrivilege.Checked)
			{
				this.checkVal += 2;
			}
			base.Close();
		}
	}
}
