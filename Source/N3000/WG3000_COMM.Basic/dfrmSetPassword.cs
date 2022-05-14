using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmSetPassword : frmN3000
	{
		public string newPassword;

		public int operatorID;

		private Container components;

		internal TextBox txtPasswordNew;

		internal Label Label2;

		internal Label Label3;

		internal TextBox txtPasswordNewConfirm;

		internal Button btnOk;

		internal Button btnCancel;

		public dfrmSetPassword()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmSetPassword));
			this.txtPasswordNew = new TextBox();
			this.Label2 = new Label();
			this.Label3 = new Label();
			this.txtPasswordNewConfirm = new TextBox();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.txtPasswordNew, "txtPasswordNew");
			this.txtPasswordNew.Name = "txtPasswordNew";
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.Name = "Label2";
			this.Label3.BackColor = Color.Transparent;
			this.Label3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.txtPasswordNewConfirm, "txtPasswordNewConfirm");
			this.txtPasswordNewConfirm.Name = "txtPasswordNewConfirm";
			this.btnOk.BackColor = Color.Transparent;
			this.btnOk.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOk, "btnOk");
			this.btnOk.ForeColor = Color.White;
			this.btnOk.Name = "btnOk";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			base.AcceptButton = this.btnOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtPasswordNew);
			base.Controls.Add(this.txtPasswordNewConfirm);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.btnCancel);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmSetPassword";
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
			if (this.txtPasswordNew.Text != this.txtPasswordNewConfirm.Text)
			{
				XMessageBox.Show(this, CommonStr.strPwdNotSame, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.operatorID != 0)
			{
				try
				{
					this.newPassword = this.txtPasswordNew.Text;
					string text = " UPDATE [t_s_Operator] ";
					text = text + "SET [f_Password]=" + wgTools.PrepareStr(this.txtPasswordNew.Text);
					text = text + " WHERE [f_OperatorID]=" + this.operatorID;
					if (wgAppConfig.runUpdateSql(text) >= 0)
					{
						base.DialogResult = DialogResult.OK;
						base.Close();
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
					XMessageBox.Show(ex.Message);
				}
				return;
			}
			if (this.txtPasswordNew.Text == "")
			{
				this.txtPasswordNew.Text = "0";
				this.txtPasswordNewConfirm.Text = "0";
			}
			long num = -1L;
			if (!long.TryParse(this.txtPasswordNew.Text, out num) || this.txtPasswordNew.Text.Length > 6)
			{
				XMessageBox.Show(this, CommonStr.strPasswordWarn, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (num <= 0L)
			{
				XMessageBox.Show(this, CommonStr.strPasswordWarn, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.newPassword = this.txtPasswordNew.Text;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void dfrmSetPassword_Load(object sender, EventArgs e)
		{
			this.txtPasswordNew.CharacterCasing = CharacterCasing.Lower;
			this.txtPasswordNewConfirm.CharacterCasing = CharacterCasing.Lower;
		}
	}
}
