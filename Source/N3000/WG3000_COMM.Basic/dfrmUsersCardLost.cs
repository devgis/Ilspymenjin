using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmUsersCardLost : frmN3000
	{
		private IContainer components;

		private Label label2;

		private Label label3;

		private Label label1;

		private Button btnCancel;

		private Button btnOK;

		public MaskedTextBox txtf_CardNO;

		public TextBox txtf_ConsumerName;

		public MaskedTextBox txtf_CardNONew;

		public dfrmUsersCardLost()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			icConsumer icConsumer = new icConsumer();
			if (!string.IsNullOrEmpty(this.txtf_CardNONew.Text) && icConsumer.isExisted(long.Parse(this.txtf_CardNONew.Text)))
			{
				XMessageBox.Show(this, CommonStr.strCardAlreadyUsed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			icConsumerShare.setUpdateLog();
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void txtf_CardNONew_KeyPress(object sender, KeyPressEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtf_CardNONew);
		}

		private void txtf_CardNONew_KeyUp(object sender, KeyEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtf_CardNONew);
		}

		private void dfrmUsersCardLost_Load(object sender, EventArgs e)
		{
			this.txtf_CardNO.Mask = "9999999999";
			this.txtf_CardNONew.Mask = "9999999999";
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmUsersCardLost));
			this.txtf_CardNO = new MaskedTextBox();
			this.txtf_ConsumerName = new TextBox();
			this.label2 = new Label();
			this.label3 = new Label();
			this.txtf_CardNONew = new MaskedTextBox();
			this.label1 = new Label();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.txtf_CardNO, "txtf_CardNO");
			this.txtf_CardNO.Name = "txtf_CardNO";
			this.txtf_CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtf_ConsumerName, "txtf_ConsumerName");
			this.txtf_ConsumerName.Name = "txtf_ConsumerName";
			this.txtf_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.txtf_CardNONew, "txtf_CardNONew");
			this.txtf_CardNONew.Name = "txtf_CardNONew";
			this.txtf_CardNONew.KeyPress += new KeyPressEventHandler(this.txtf_CardNONew_KeyPress);
			this.txtf_CardNONew.KeyUp += new KeyEventHandler(this.txtf_CardNONew_KeyUp);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
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
			base.Controls.Add(this.txtf_CardNONew);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtf_CardNO);
			base.Controls.Add(this.txtf_ConsumerName);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label3);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmUsersCardLost";
			base.Load += new EventHandler(this.dfrmUsersCardLost_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
