using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Core
{
	public class dfrmSelectColumnsShow : frmN3000
	{
		private IContainer components;

		internal Button btnOK;

		internal Button btnCancel;

		private Label label1;

		public CheckedListBox chkListColumns;

		public dfrmSelectColumnsShow()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmSelectColumnsShow));
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.label1 = new Label();
			this.chkListColumns = new CheckedListBox();
			base.SuspendLayout();
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.chkListColumns.CheckOnClick = true;
			this.chkListColumns.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.chkListColumns, "chkListColumns");
			this.chkListColumns.Name = "chkListColumns";
			this.chkListColumns.ThreeDCheckBoxes = true;
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.chkListColumns);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "dfrmSelectColumnsShow";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
