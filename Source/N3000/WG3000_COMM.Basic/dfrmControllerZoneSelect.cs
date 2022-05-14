using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmControllerZoneSelect : frmN3000
	{
		private IContainer components;

		private ComboBox cboZone;

		private Label label25;

		public Button btnCancel;

		public Button btnOK;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		public int selectZoneId = -1;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerZoneSelect));
			this.cboZone = new ComboBox();
			this.label25 = new Label();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.cboZone, "cboZone");
			this.cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboZone.FormattingEnabled = true;
			this.cboZone.Name = "cboZone";
			componentResourceManager.ApplyResources(this.label25, "label25");
			this.label25.ForeColor = Color.White;
			this.label25.Name = "label25";
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.cboZone);
			base.Controls.Add(this.label25);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmControllerZoneSelect";
			base.Load += new EventHandler(this.dfrmControllerZoneSelect_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmControllerZoneSelect()
		{
			this.InitializeComponent();
		}

		private void dfrmControllerZoneSelect_Load(object sender, EventArgs e)
		{
			this.loadZoneInfo();
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cboZone.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrZoneName[i].ToString()))
				{
					this.cboZone.Items.Add("");
				}
				else
				{
					this.cboZone.Items.Add(this.arrZoneName[i].ToString());
				}
			}
			if (this.cboZone.Items.Count > 0)
			{
				this.cboZone.SelectedIndex = 0;
			}
			bool visible = true;
			this.cboZone.Visible = visible;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.cboZone.Items.Count > 0)
			{
				this.selectZoneId = (int)this.arrZoneID[this.cboZone.SelectedIndex];
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.Close();
		}
	}
}
