using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmControllerAntiPassback : frmN3000
	{
		private IContainer components;

		private DataGridView dataGridView1;

		private Button btnEdit;

		private Button btnClose;

		private DataGridViewTextBoxColumn f_ControllerID;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewCheckBoxColumn f_AntiBack;

		private DataGridViewTextBoxColumn f_DoorNames;

		internal CheckBox chkGrouped;

		internal CheckBox chkActiveAntibackShare;

		private bool bLoad = true;

		private dfrmFind dfrmFind1 = new dfrmFind();

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dfrmFind1 != null)
			{
				this.dfrmFind1.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerAntiPassback));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.dataGridView1 = new DataGridView();
			this.f_ControllerID = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_AntiBack = new DataGridViewCheckBoxColumn();
			this.f_DoorNames = new DataGridViewTextBoxColumn();
			this.btnEdit = new Button();
			this.btnClose = new Button();
			this.chkGrouped = new CheckBox();
			this.chkActiveAntibackShare = new CheckBox();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ControllerID,
				this.f_ControllerSN,
				this.f_AntiBack,
				this.f_DoorNames
			});
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Click += new EventHandler(this.dataGridView1_DoubleClick);
			componentResourceManager.ApplyResources(this.f_ControllerID, "f_ControllerID");
			this.f_ControllerID.Name = "f_ControllerID";
			this.f_ControllerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_AntiBack, "f_AntiBack");
			this.f_AntiBack.Name = "f_AntiBack";
			this.f_AntiBack.ReadOnly = true;
			this.f_AntiBack.Resizable = DataGridViewTriState.True;
			this.f_AntiBack.SortMode = DataGridViewColumnSortMode.Automatic;
			this.f_DoorNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_DoorNames, "f_DoorNames");
			this.f_DoorNames.Name = "f_DoorNames";
			this.f_DoorNames.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.BackColor = Color.Transparent;
			this.btnEdit.BackgroundImage = Resources.pMain_button_normal;
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.chkGrouped, "chkGrouped");
			this.chkGrouped.BackColor = Color.Transparent;
			this.chkGrouped.ForeColor = Color.White;
			this.chkGrouped.Name = "chkGrouped";
			this.chkGrouped.UseVisualStyleBackColor = false;
			this.chkGrouped.CheckedChanged += new EventHandler(this.chkGrouped_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkActiveAntibackShare, "chkActiveAntibackShare");
			this.chkActiveAntibackShare.BackColor = Color.Transparent;
			this.chkActiveAntibackShare.ForeColor = Color.White;
			this.chkActiveAntibackShare.Name = "chkActiveAntibackShare";
			this.chkActiveAntibackShare.UseVisualStyleBackColor = false;
			this.chkActiveAntibackShare.CheckedChanged += new EventHandler(this.chkActiveAntibackShare_CheckedChanged);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.chkGrouped);
			base.Controls.Add(this.chkActiveAntibackShare);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnEdit);
			base.Controls.Add(this.dataGridView1);
			base.Name = "dfrmControllerAntiPassback";
			base.FormClosing += new FormClosingEventHandler(this.dfrmControllerAntiPassback_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerAntiPassback_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControllerAntiPassback_KeyDown);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmControllerAntiPassback()
		{
			this.InitializeComponent();
		}

		private void dfrmControllerAntiPassback_Load(object sender, EventArgs e)
		{
			this.chkActiveAntibackShare.Checked = wgAppConfig.getParamValBoolByNO(62);
			this.chkActiveAntibackShare.Visible = this.chkActiveAntibackShare.Checked;
			if (this.chkActiveAntibackShare.Visible)
			{
				this.dataGridView1.Location = new Point(8, 40);
				if (wgAppConfig.getSystemParamByNO(62) == "2")
				{
					this.chkGrouped.Checked = true;
					this.chkGrouped.Visible = true;
					this.dataGridView1.Location = new Point(8, 72);
				}
				this.dataGridView1.Size = new Size(this.dataGridView1.Size.Width, this.dataGridView1.Size.Height + 8 - this.dataGridView1.Location.Y);
			}
			string text = " SELECT ";
			text += " f_ControllerNO ";
			text += ", f_ControllerSN ";
			text += ", f_AntiBack ";
			text += ", f_DoorNames ";
			text += ", t_b_Controller.f_ZoneID ";
			text += "  from t_b_Controller ORDER BY f_ControllerNO ";
			wgAppConfig.fillDGVData(ref this.dataGridView1, text);
			DataTable table = ((DataView)this.dataGridView1.DataSource).Table;
			DataView dataView = new DataView(table);
			if (dataView.Count > 0)
			{
				dataView.RowFilter = " f_AntiBack > 10";
				if (dataView.Count > 0)
				{
					dfrmAntiBack.bDisplayIndoorPersonMax = true;
				}
			}
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref table);
			this.loadOperatorPrivilege();
			this.bLoad = false;
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuAntiBack";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnEdit.Visible = false;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.SelectedRows.Count <= 0)
			{
				if (this.dataGridView1.SelectedCells.Count <= 0)
				{
					return;
				}
				int arg_3D_0 = this.dataGridView1.SelectedCells[0].RowIndex;
			}
			else
			{
				int arg_56_0 = this.dataGridView1.SelectedRows[0].Index;
			}
			int index = 0;
			DataGridView dataGridView = this.dataGridView1;
			if (dataGridView.Rows.Count > 0)
			{
				index = dataGridView.CurrentCell.RowIndex;
			}
			using (dfrmAntiBack dfrmAntiBack = new dfrmAntiBack())
			{
				dfrmAntiBack.ControllerSN = dataGridView.Rows[index].Cells[1].Value.ToString();
				dfrmAntiBack.Text = dfrmAntiBack.Text + "[" + dataGridView.Rows[index].Cells[1].Value.ToString() + "]";
				if (dfrmAntiBack.ShowDialog(this) == DialogResult.OK)
				{
					int retValue = dfrmAntiBack.retValue;
					string strSql = "UPDATE t_b_Controller SET f_AntiBack =" + retValue.ToString() + " Where f_ControllerSN = " + dataGridView.Rows[index].Cells[1].Value.ToString();
					if (wgAppConfig.runUpdateSql(strSql) > 0)
					{
						if (retValue == 0)
						{
							dataGridView.Rows[index].Cells["f_AntiBack"].Value = 0;
						}
						else
						{
							dataGridView.Rows[index].Cells["f_AntiBack"].Value = 1;
						}
					}
				}
			}
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}

		private void dfrmControllerAntiPassback_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Control && e.KeyValue == 81 && e.Shift)
				{
					if (!this.chkGrouped.Visible)
					{
						if (this.chkActiveAntibackShare.Visible)
						{
							this.chkGrouped.Visible = true;
							this.dataGridView1.Location = new Point(8, 72);
							this.dataGridView1.Size = new Size(this.dataGridView1.Size.Width, this.dataGridView1.Size.Height + 40 - this.dataGridView1.Location.Y);
						}
						else
						{
							this.chkActiveAntibackShare.Visible = true;
							this.dataGridView1.Location = new Point(8, 40);
							this.dataGridView1.Size = new Size(this.dataGridView1.Size.Width, this.dataGridView1.Size.Height + 8 - this.dataGridView1.Location.Y);
						}
					}
					this.chkActiveAntibackShare_CheckedChanged(null, null);
				}
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmControllerAntiPassback_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void chkActiveAntibackShare_CheckedChanged(object sender, EventArgs e)
		{
			if (this.bLoad)
			{
				return;
			}
			wgAppConfig.setSystemParamValue(62, this.chkActiveAntibackShare.Checked ? (this.chkGrouped.Checked ? "2" : "1") : "0");
			if (this.chkActiveAntibackShare.Checked)
			{
				this.chkGrouped.Enabled = true;
				return;
			}
			this.chkGrouped.Enabled = false;
			this.chkGrouped.Checked = false;
		}

		private void chkGrouped_CheckedChanged(object sender, EventArgs e)
		{
			if (this.bLoad)
			{
				return;
			}
			wgAppConfig.setSystemParamValue(62, this.chkActiveAntibackShare.Checked ? (this.chkGrouped.Checked ? "2" : "1") : "0");
		}
	}
}
