using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;

namespace WG3000_COMM.Basic
{
	public class dfrmWait : Form
	{
		private IContainer components;

		private Label label1;

		public dfrmWait()
		{
			this.InitializeComponent();
			this.BackColor = Color.FromArgb(128, 131, 156);
		}

		private void dfrmWait_Load(object sender, EventArgs e)
		{
			Icon icon = base.Icon;
			wgAppConfig.GetAppIcon(ref icon);
			base.Icon = icon;
			this.Refresh();
			Cursor.Current = Cursors.WaitCursor;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmWait));
			this.label1 = new Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BorderStyle = BorderStyle.Fixed3D;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.label1.UseWaitCursor = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(128, 131, 156);
			base.Controls.Add(this.label1);
			this.ForeColor = Color.White;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmWait";
			base.TopMost = true;
			base.UseWaitCursor = true;
			base.Load += new EventHandler(this.dfrmWait_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
