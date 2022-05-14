using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmPeripheralControlBoardSuper : frmN3000
	{
		private IContainer components;

		private CheckBox checkBox83;

		private CheckBox checkBox82;

		private CheckBox checkBox81;

		private CheckBox checkBox80;

		private CheckBox checkBox79;

		private CheckBox checkBox78;

		private CheckBox checkBox77;

		private CheckBox checkBox76;

		private RadioButton radioButton18;

		private RadioButton radioButton17;

		private RadioButton radioButton16;

		private RadioButton radioButton15;

		private RadioButton radioButton14;

		private Label label1;

		internal Button btnOK;

		internal Button btnCancel;

		private CheckBox chkForceOutputTimeRemains;

		public int extControl;

		public int ext_warnSignalEnabled2;

		private bool bVisibleForce;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPeripheralControlBoardSuper));
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.chkForceOutputTimeRemains = new CheckBox();
			this.label1 = new Label();
			this.checkBox83 = new CheckBox();
			this.checkBox82 = new CheckBox();
			this.checkBox81 = new CheckBox();
			this.checkBox80 = new CheckBox();
			this.checkBox79 = new CheckBox();
			this.checkBox78 = new CheckBox();
			this.checkBox77 = new CheckBox();
			this.checkBox76 = new CheckBox();
			this.radioButton18 = new RadioButton();
			this.radioButton17 = new RadioButton();
			this.radioButton16 = new RadioButton();
			this.radioButton15 = new RadioButton();
			this.radioButton14 = new RadioButton();
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
			componentResourceManager.ApplyResources(this.chkForceOutputTimeRemains, "chkForceOutputTimeRemains");
			this.chkForceOutputTimeRemains.ForeColor = Color.White;
			this.chkForceOutputTimeRemains.Name = "chkForceOutputTimeRemains";
			this.chkForceOutputTimeRemains.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.checkBox83, "checkBox83");
			this.checkBox83.ForeColor = Color.White;
			this.checkBox83.Name = "checkBox83";
			this.checkBox83.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox82, "checkBox82");
			this.checkBox82.ForeColor = Color.White;
			this.checkBox82.Name = "checkBox82";
			this.checkBox82.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox81, "checkBox81");
			this.checkBox81.ForeColor = Color.White;
			this.checkBox81.Name = "checkBox81";
			this.checkBox81.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox80, "checkBox80");
			this.checkBox80.ForeColor = Color.White;
			this.checkBox80.Name = "checkBox80";
			this.checkBox80.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox79, "checkBox79");
			this.checkBox79.ForeColor = Color.White;
			this.checkBox79.Name = "checkBox79";
			this.checkBox79.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox78, "checkBox78");
			this.checkBox78.ForeColor = Color.White;
			this.checkBox78.Name = "checkBox78";
			this.checkBox78.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox77, "checkBox77");
			this.checkBox77.ForeColor = Color.White;
			this.checkBox77.Name = "checkBox77";
			this.checkBox77.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox76, "checkBox76");
			this.checkBox76.ForeColor = Color.White;
			this.checkBox76.Name = "checkBox76";
			this.checkBox76.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton18, "radioButton18");
			this.radioButton18.ForeColor = Color.White;
			this.radioButton18.Name = "radioButton18";
			this.radioButton18.UseVisualStyleBackColor = true;
			this.radioButton18.CheckedChanged += new EventHandler(this.radioButton14_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton17, "radioButton17");
			this.radioButton17.ForeColor = Color.White;
			this.radioButton17.Name = "radioButton17";
			this.radioButton17.UseVisualStyleBackColor = true;
			this.radioButton17.CheckedChanged += new EventHandler(this.radioButton14_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton16, "radioButton16");
			this.radioButton16.ForeColor = Color.White;
			this.radioButton16.Name = "radioButton16";
			this.radioButton16.UseVisualStyleBackColor = true;
			this.radioButton16.CheckedChanged += new EventHandler(this.radioButton14_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton15, "radioButton15");
			this.radioButton15.ForeColor = Color.White;
			this.radioButton15.Name = "radioButton15";
			this.radioButton15.UseVisualStyleBackColor = true;
			this.radioButton15.CheckedChanged += new EventHandler(this.radioButton14_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton14, "radioButton14");
			this.radioButton14.ForeColor = Color.White;
			this.radioButton14.Name = "radioButton14";
			this.radioButton14.UseVisualStyleBackColor = true;
			this.radioButton14.CheckedChanged += new EventHandler(this.radioButton14_CheckedChanged);
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.chkForceOutputTimeRemains);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.checkBox83);
			base.Controls.Add(this.checkBox82);
			base.Controls.Add(this.checkBox81);
			base.Controls.Add(this.checkBox80);
			base.Controls.Add(this.checkBox79);
			base.Controls.Add(this.checkBox78);
			base.Controls.Add(this.checkBox77);
			base.Controls.Add(this.checkBox76);
			base.Controls.Add(this.radioButton18);
			base.Controls.Add(this.radioButton17);
			base.Controls.Add(this.radioButton16);
			base.Controls.Add(this.radioButton15);
			base.Controls.Add(this.radioButton14);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "dfrmPeripheralControlBoardSuper";
			base.Load += new EventHandler(this.dfrmPeripheralControlBoardSuper_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmPeripheralControlBoardSuper_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmPeripheralControlBoardSuper()
		{
			this.InitializeComponent();
		}

		private void dfrmPeripheralControlBoardSuper_Load(object sender, EventArgs e)
		{
			this.radioButton14.Checked = (this.extControl == 1 || this.extControl == 0);
			this.radioButton15.Checked = (this.extControl == 2);
			this.radioButton16.Checked = (this.extControl == 3);
			this.radioButton17.Checked = (this.extControl == 4 || this.extControl == 6);
			this.radioButton18.Checked = (this.extControl == 5 || this.extControl == 7);
			this.bVisibleForce = (this.extControl == 7 || this.extControl == 6);
			this.chkForceOutputTimeRemains.Visible = this.bVisibleForce;
			this.chkForceOutputTimeRemains.Checked = this.bVisibleForce;
			this.checkBox76.Checked = ((this.ext_warnSignalEnabled2 & 1) > 0);
			this.checkBox77.Checked = ((this.ext_warnSignalEnabled2 & 2) > 0);
			this.checkBox78.Checked = ((this.ext_warnSignalEnabled2 & 4) > 0);
			this.checkBox79.Checked = ((this.ext_warnSignalEnabled2 & 8) > 0);
			this.checkBox80.Checked = ((this.ext_warnSignalEnabled2 & 16) > 0);
			this.checkBox81.Checked = ((this.ext_warnSignalEnabled2 & 32) > 0);
			this.checkBox82.Checked = ((this.ext_warnSignalEnabled2 & 64) > 0);
			this.checkBox83.Checked = ((this.ext_warnSignalEnabled2 & 128) > 0);
			if (this.radioButton17.Checked || this.radioButton18.Checked)
			{
				this.diplayChkbox();
				return;
			}
			this.hideChkbox();
		}

		private void diplayChkbox()
		{
			this.checkBox76.Visible = true;
			this.checkBox77.Visible = true;
			this.checkBox78.Visible = true;
			this.checkBox79.Visible = true;
			this.checkBox80.Visible = true;
			this.checkBox81.Visible = true;
			this.checkBox82.Visible = true;
			this.checkBox83.Visible = true;
			this.chkForceOutputTimeRemains.Visible = this.bVisibleForce;
		}

		private void hideChkbox()
		{
			this.checkBox76.Visible = false;
			this.checkBox77.Visible = false;
			this.checkBox78.Visible = false;
			this.checkBox79.Visible = false;
			this.checkBox80.Visible = false;
			this.checkBox81.Visible = false;
			this.checkBox82.Visible = false;
			this.checkBox83.Visible = false;
			this.chkForceOutputTimeRemains.Visible = false;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			this.extControl = 0;
			if (this.radioButton14.Checked)
			{
				this.extControl = 1;
			}
			if (this.radioButton15.Checked)
			{
				this.extControl = 2;
			}
			if (this.radioButton16.Checked)
			{
				this.extControl = 3;
			}
			if (this.radioButton17.Checked)
			{
				this.extControl = (this.chkForceOutputTimeRemains.Checked ? 6 : 4);
			}
			if (this.radioButton18.Checked)
			{
				this.extControl = (this.chkForceOutputTimeRemains.Checked ? 7 : 5);
			}
			this.ext_warnSignalEnabled2 = 0;
			if (this.checkBox76.Checked)
			{
				this.ext_warnSignalEnabled2 |= 1;
			}
			if (this.checkBox77.Checked)
			{
				this.ext_warnSignalEnabled2 |= 2;
			}
			if (this.checkBox78.Checked)
			{
				this.ext_warnSignalEnabled2 |= 4;
			}
			if (this.checkBox79.Checked)
			{
				this.ext_warnSignalEnabled2 |= 8;
			}
			if (this.checkBox80.Checked)
			{
				this.ext_warnSignalEnabled2 |= 16;
			}
			if (this.checkBox81.Checked)
			{
				this.ext_warnSignalEnabled2 |= 32;
			}
			if (this.checkBox82.Checked)
			{
				this.ext_warnSignalEnabled2 |= 64;
			}
			if (this.checkBox83.Checked)
			{
				this.ext_warnSignalEnabled2 |= 128;
			}
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void radioButton14_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButton17.Checked || this.radioButton18.Checked)
			{
				this.diplayChkbox();
				return;
			}
			this.hideChkbox();
		}

		private void dfrmPeripheralControlBoardSuper_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				this.bVisibleForce = true;
				this.radioButton14_CheckedChanged(null, null);
			}
		}
	}
}
