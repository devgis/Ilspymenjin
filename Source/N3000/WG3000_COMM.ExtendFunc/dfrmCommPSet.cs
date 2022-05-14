using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmCommPSet : frmN3000
	{
		private IContainer components;

		internal TextBox txtPasswordNew;

		internal TextBox txtPasswordNewConfirm;

		internal Label Label2;

		internal Label Label3;

		private GroupBox groupBox1;

		internal TextBox txtPasswordPrev;

		internal TextBox txtPasswordPrevConfirm;

		internal Label label1;

		internal Label label4;

		internal Button btnOk;

		internal Button btnCancel;

		public string CurrentPwd = "";

		public bool bChangedPwd;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmCommPSet));
			this.txtPasswordNew = new TextBox();
			this.txtPasswordNewConfirm = new TextBox();
			this.Label2 = new Label();
			this.Label3 = new Label();
			this.groupBox1 = new GroupBox();
			this.txtPasswordPrev = new TextBox();
			this.txtPasswordPrevConfirm = new TextBox();
			this.label1 = new Label();
			this.label4 = new Label();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.txtPasswordNew, "txtPasswordNew");
			this.txtPasswordNew.Name = "txtPasswordNew";
			componentResourceManager.ApplyResources(this.txtPasswordNewConfirm, "txtPasswordNewConfirm");
			this.txtPasswordNewConfirm.Name = "txtPasswordNewConfirm";
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			this.Label2.Name = "Label2";
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.BackColor = Color.Transparent;
			this.Label3.ForeColor = Color.White;
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this.txtPasswordPrev);
			this.groupBox1.Controls.Add(this.txtPasswordPrevConfirm);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.txtPasswordPrev, "txtPasswordPrev");
			this.txtPasswordPrev.Name = "txtPasswordPrev";
			componentResourceManager.ApplyResources(this.txtPasswordPrevConfirm, "txtPasswordPrevConfirm");
			this.txtPasswordPrevConfirm.Name = "txtPasswordPrevConfirm";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.BackColor = Color.Transparent;
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.btnOk, "btnOk");
			this.btnOk.BackColor = Color.Transparent;
			this.btnOk.BackgroundImage = Resources.pMain_button_normal;
			this.btnOk.ForeColor = Color.White;
			this.btnOk.Name = "btnOk";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.txtPasswordNew);
			base.Controls.Add(this.txtPasswordNewConfirm);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label3);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmCommPSet";
			base.Load += new EventHandler(this.dfrmCommPSet_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmCommPSet_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmCommPSet()
		{
			this.InitializeComponent();
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
			if (this.txtPasswordPrev.Text != this.txtPasswordPrevConfirm.Text)
			{
				XMessageBox.Show(this, this.label1.Text + "\r\n\r\n" + CommonStr.strPwdNotSame, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.CurrentPwd = this.txtPasswordNew.Text.Trim();
			if (this.bChangedPwd)
			{
				if (string.IsNullOrEmpty(this.txtPasswordPrev.Text.Trim()))
				{
					wgTools.CommPStr = "";
				}
				else
				{
					wgTools.CommPStr = WGPacket.Ept(this.txtPasswordPrev.Text.Trim());
				}
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void dfrmCommPSet_Load(object sender, EventArgs e)
		{
		}

		private void dfrmCommPSet_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Shift && e.KeyValue == 120 && !this.bChangedPwd)
			{
				base.Size = new Size(base.Size.Width, 310);
				this.groupBox1.Visible = true;
				this.bChangedPwd = true;
			}
		}
	}
}
