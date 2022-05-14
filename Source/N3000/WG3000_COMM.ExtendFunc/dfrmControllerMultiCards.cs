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
	public class dfrmControllerMultiCards : frmN3000
	{
		private IContainer components;

		private DataGridView dataGridView1;

		private Button btnClose;

		private Button btnEdit;

		private DataGridViewTextBoxColumn f_DoorID;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewTextBoxColumn f_DoorNo;

		private DataGridViewTextBoxColumn f_DoorName;

		private DataGridViewTextBoxColumn f_MoreCards_Total;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerMultiCards));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.btnClose = new Button();
			this.btnEdit = new Button();
			this.dataGridView1 = new DataGridView();
			this.f_DoorID = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_DoorNo = new DataGridViewTextBoxColumn();
			this.f_DoorName = new DataGridViewTextBoxColumn();
			this.f_MoreCards_Total = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.BackColor = Color.Transparent;
			this.btnEdit.BackgroundImage = Resources.pMain_button_normal;
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
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
				this.f_DoorID,
				this.f_ControllerSN,
				this.f_DoorNo,
				this.f_DoorName,
				this.f_MoreCards_Total
			});
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Click += new EventHandler(this.dataGridView1_DoubleClick);
			componentResourceManager.ApplyResources(this.f_DoorID, "f_DoorID");
			this.f_DoorID.Name = "f_DoorID";
			this.f_DoorID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DoorNo, "f_DoorNo");
			this.f_DoorNo.Name = "f_DoorNo";
			this.f_DoorNo.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DoorName, "f_DoorName");
			this.f_DoorName.Name = "f_DoorName";
			this.f_DoorName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_MoreCards_Total, "f_MoreCards_Total");
			this.f_MoreCards_Total.Name = "f_MoreCards_Total";
			this.f_MoreCards_Total.ReadOnly = true;
			this.f_MoreCards_Total.Resizable = DataGridViewTriState.True;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnEdit);
			base.Controls.Add(this.dataGridView1);
			base.Name = "dfrmControllerMultiCards";
			base.FormClosing += new FormClosingEventHandler(this.dfrmControllerMultiCards_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerMultiCards_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControllerMultiCards_KeyDown);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmControllerMultiCards()
		{
			this.InitializeComponent();
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dfrmControllerMultiCards_Load(object sender, EventArgs e)
		{
			string text = " SELECT ";
			text += " t_b_Door.f_DoorID ";
			text += ", t_b_Controller.f_ControllerSN ";
			text += ", t_b_Door.f_DoorNO ";
			text += ", t_b_Door.f_DoorName ";
			text += ", t_b_Door.f_MoreCards_Total ";
			text += ", t_b_Controller.f_ZoneID ";
			text += " from t_b_Controller,t_b_Door WHERE t_b_Controller.f_ControllerID=t_b_Door.f_ControllerID ";
			text += " ORDER BY t_b_Door.f_DoorID ";
			wgAppConfig.fillDGVData(ref this.dataGridView1, text);
			DataTable table = ((DataView)this.dataGridView1.DataSource).Table;
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref table);
			this.loadOperatorPrivilege();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuMoreCards";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnEdit.Visible = false;
				this.dataGridView1.ReadOnly = true;
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
			using (dfrmMultiCards dfrmMultiCards = new dfrmMultiCards())
			{
				dfrmMultiCards.DoorID = int.Parse(dataGridView.Rows[index].Cells[0].Value.ToString());
				dfrmMultiCards.Text = string.Concat(new string[]
				{
					dfrmMultiCards.Text,
					"[",
					dataGridView.Rows[index].Cells[2].Value.ToString(),
					"   ",
					dataGridView.Rows[index].Cells[3].Value.ToString(),
					"]"
				});
				if (dfrmMultiCards.ShowDialog(this) == DialogResult.OK)
				{
					dataGridView.Rows[index].Cells["f_MoreCards_Total"].Value = dfrmMultiCards.retValue;
				}
			}
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}

		private void dfrmControllerMultiCards_KeyDown(object sender, KeyEventArgs e)
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
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmControllerMultiCards_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
