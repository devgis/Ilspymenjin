using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmShowError : frmN3000
	{
		private IContainer components;

		private Label label1;

		private Button btnCopyDetail;

		private Button btnOK;

		private Button btnDetail;

		private TextBox txtErrorDetail;

		public string errInfo = "";

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShowError));
			this.btnOK = new Button();
			this.txtErrorDetail = new TextBox();
			this.label1 = new Label();
			this.btnCopyDetail = new Button();
			this.btnDetail = new Button();
			base.SuspendLayout();
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.txtErrorDetail, "txtErrorDetail");
			this.txtErrorDetail.Name = "txtErrorDetail";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.btnCopyDetail.BackColor = Color.Transparent;
			this.btnCopyDetail.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCopyDetail, "btnCopyDetail");
			this.btnCopyDetail.ForeColor = Color.White;
			this.btnCopyDetail.Name = "btnCopyDetail";
			this.btnCopyDetail.UseVisualStyleBackColor = false;
			this.btnCopyDetail.Click += new EventHandler(this.btnCopyDetail_Click);
			componentResourceManager.ApplyResources(this.btnDetail, "btnDetail");
			this.btnDetail.Name = "btnDetail";
			this.btnDetail.UseVisualStyleBackColor = true;
			this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtErrorDetail);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnCopyDetail);
			base.Controls.Add(this.btnDetail);
			base.MinimizeBox = false;
			base.Name = "dfrmShowError";
			base.Load += new EventHandler(this.dfrmShowError_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmShowError()
		{
			this.InitializeComponent();
		}

		private void btnCopyDetail_Click(object sender, EventArgs e)
		{
			try
			{
				this.txtErrorDetail.Text = this.errInfo;
				string text = this.txtErrorDetail.Text;
				Clipboard.SetDataObject(text, false);
				this.btnDetail.Enabled = false;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.Close();
			base.DialogResult = DialogResult.OK;
		}

		private void btnDetail_Click(object sender, EventArgs e)
		{
			base.Height = 320;
			this.btnDetail.Visible = false;
			this.btnCopyDetail.Visible = true;
			try
			{
				this.txtErrorDetail.Visible = true;
				this.txtErrorDetail.Text = this.errInfo;
				string text = this.txtErrorDetail.Text;
				Clipboard.SetDataObject(text, false);
				this.btnDetail.Enabled = false;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmShowError_Load(object sender, EventArgs e)
		{
			this.errInfo != "";
		}
	}
}
