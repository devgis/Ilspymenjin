using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmInputNewName : frmN3000
	{
		public string strNewName = "";

		public bool bNotAllowNull = true;

		private IContainer components;

		private Button btnOK;

		private Button btnCancel;

		private TextBox txtNewName;

		private Label label2;

		public Label label1;

		public dfrmInputNewName()
		{
			this.InitializeComponent();
		}

		public void setPasswordChar(char val)
		{
			this.txtNewName.PasswordChar = val;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.bNotAllowNull && string.IsNullOrEmpty(this.txtNewName.Text))
			{
				this.label2.Visible = true;
				return;
			}
			if (string.IsNullOrEmpty(this.txtNewName.Text))
			{
				this.strNewName = "";
			}
			else
			{
				this.strNewName = this.txtNewName.Text.Trim();
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmInputNewName_Load(object sender, EventArgs e)
		{
			this.txtNewName.Text = this.strNewName;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmInputNewName));
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.label2 = new Label();
			this.txtNewName = new TextBox();
			this.label1 = new Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.txtNewName, "txtNewName");
			this.txtNewName.Name = "txtNewName";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtNewName);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmInputNewName";
			base.TopMost = true;
			base.Load += new EventHandler(this.dfrmInputNewName_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
