using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc.Map
{
	public class dfrmSelectMapDoor : frmN3000
	{
		public string doorName;

		public bool bAddDoor = true;

		private Container components;

		internal Button btnOK;

		internal Button btnCancel;

		internal ListBox lstMappedDoors;

		internal Label Label1;

		internal Label Label2;

		internal ListBox lstUnMappedDoors;

		private dfrmFind dfrmFind1 = new dfrmFind();

		public dfrmSelectMapDoor()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmSelectMapDoor));
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.lstUnMappedDoors = new ListBox();
			this.lstMappedDoors = new ListBox();
			this.Label1 = new Label();
			this.Label2 = new Label();
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
			componentResourceManager.ApplyResources(this.lstUnMappedDoors, "lstUnMappedDoors");
			this.lstUnMappedDoors.Name = "lstUnMappedDoors";
			this.lstUnMappedDoors.SelectedIndexChanged += new EventHandler(this.lstUnMappedDoors_SelectedIndexChanged);
			this.lstUnMappedDoors.DoubleClick += new EventHandler(this.lstUnMappedDoors_DoubleClick);
			this.lstUnMappedDoors.MouseDoubleClick += new MouseEventHandler(this.lstUnMappedDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.lstMappedDoors, "lstMappedDoors");
			this.lstMappedDoors.Name = "lstMappedDoors";
			this.lstMappedDoors.SelectedIndexChanged += new EventHandler(this.lstMappedDoors_SelectedIndexChanged);
			this.lstMappedDoors.DoubleClick += new EventHandler(this.lstMappedDoors_DoubleClick);
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.Name = "Label2";
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.lstUnMappedDoors);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.lstMappedDoors);
			base.Controls.Add(this.Label2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmSelectMapDoor";
			base.FormClosing += new FormClosingEventHandler(this.dfrmSelectMapDoor_FormClosing);
			base.Load += new EventHandler(this.dfrmSelectMapDoor_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmSelectMapDoor_KeyDown);
			base.ResumeLayout(false);
		}

		private void lstUnMappedDoors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lstUnMappedDoors.SelectedItems.Count > 0)
			{
				this.lstMappedDoors.SelectedIndex = -1;
			}
		}

		private void lstMappedDoors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lstMappedDoors.SelectedItems.Count > 0)
			{
				this.lstUnMappedDoors.SelectedIndex = -1;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.lstUnMappedDoors.SelectedItems.Count > 0)
			{
				this.doorName = this.lstUnMappedDoors.SelectedItem.ToString();
				base.DialogResult = DialogResult.OK;
				return;
			}
			if (this.lstMappedDoors.SelectedItems.Count > 0)
			{
				this.doorName = this.lstMappedDoors.SelectedItem.ToString();
				this.bAddDoor = false;
				base.DialogResult = DialogResult.OK;
				return;
			}
			base.DialogResult = DialogResult.Cancel;
		}

		private void lstUnMappedDoors_DoubleClick(object sender, EventArgs e)
		{
			this.btnOK.PerformClick();
		}

		private void lstMappedDoors_DoubleClick(object sender, EventArgs e)
		{
			this.btnOK.PerformClick();
		}

		private void dfrmSelectMapDoor_Load(object sender, EventArgs e)
		{
			base.KeyPreview = true;
		}

		private void dfrmSelectMapDoor_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
					this.dfrmFind1.Focus();
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmSelectMapDoor_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void lstUnMappedDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnOK.PerformClick();
		}
	}
}
