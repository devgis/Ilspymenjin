using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WG3000_COMM.Core
{
	public class frmN3000 : Form
	{
		private IContainer components;

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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(18, 91, 168);
			this.BackgroundImageLayout = ImageLayout.Stretch;
			base.ClientSize = new Size(792, 566);
			this.DoubleBuffered = true;
			base.KeyPreview = true;
			base.Name = "frmN3000";
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += new EventHandler(this.frmN3000_Load);
			base.ResumeLayout(false);
		}

		public frmN3000()
		{
			this.InitializeComponent();
			this.BackColor = Color.FromArgb(128, 131, 156);
		}

		private void frmN3000_Load(object sender, EventArgs e)
		{
			Icon icon = base.Icon;
			wgAppConfig.GetAppIcon(ref icon);
			base.Icon = icon;
			if (base.IsMdiContainer)
			{
				return;
			}
			wgAppRunInfo.ClearAllDisplayedInfo();
		}
	}
}
