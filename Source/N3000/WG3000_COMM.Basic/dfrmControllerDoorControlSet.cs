using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmControllerDoorControlSet : frmN3000
	{
		public int doorControl = -1;

		private IContainer components;

		public Button btnNormalClose;

		private Button btnNormalOpen;

		private Button btnOnline;

		public Button btnCancel;

		private Label label1;

		public dfrmControllerDoorControlSet()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnOnline_Click(object sender, EventArgs e)
		{
			if (sender == this.btnOnline)
			{
				this.doorControl = 3;
			}
			else if (sender == this.btnNormalClose)
			{
				this.doorControl = 2;
			}
			else
			{
				if (sender != this.btnNormalOpen)
				{
					this.btnCancel.PerformClick();
					return;
				}
				this.doorControl = 1;
			}
			base.DialogResult = DialogResult.OK;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerDoorControlSet));
			this.btnNormalClose = new Button();
			this.btnNormalOpen = new Button();
			this.btnOnline = new Button();
			this.btnCancel = new Button();
			this.label1 = new Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnNormalClose, "btnNormalClose");
			this.btnNormalClose.BackColor = Color.Transparent;
			this.btnNormalClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnNormalClose.ForeColor = Color.White;
			this.btnNormalClose.Name = "btnNormalClose";
			this.btnNormalClose.UseVisualStyleBackColor = false;
			this.btnNormalClose.Click += new EventHandler(this.btnOnline_Click);
			componentResourceManager.ApplyResources(this.btnNormalOpen, "btnNormalOpen");
			this.btnNormalOpen.BackColor = Color.Transparent;
			this.btnNormalOpen.BackgroundImage = Resources.pMain_button_normal;
			this.btnNormalOpen.ForeColor = Color.White;
			this.btnNormalOpen.Name = "btnNormalOpen";
			this.btnNormalOpen.UseVisualStyleBackColor = false;
			this.btnNormalOpen.Click += new EventHandler(this.btnOnline_Click);
			componentResourceManager.ApplyResources(this.btnOnline, "btnOnline");
			this.btnOnline.BackColor = Color.Transparent;
			this.btnOnline.BackgroundImage = Resources.pMain_button_normal;
			this.btnOnline.ForeColor = Color.White;
			this.btnOnline.Name = "btnOnline";
			this.btnOnline.UseVisualStyleBackColor = false;
			this.btnOnline.Click += new EventHandler(this.btnOnline_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.Red;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOnline);
			base.Controls.Add(this.btnNormalClose);
			base.Controls.Add(this.btnNormalOpen);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmControllerDoorControlSet";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
