using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmOperator : frmN3000
	{
		private DataTable table;

		private DataView dv;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private ToolStripButton btnSetPassword;

		private ToolStripButton btnEditPrivilege;

		private DataGridView dgvOperators;

		private DataGridViewTextBoxColumn f_OperatorID;

		private DataGridViewTextBoxColumn f_OperatorName;

		private ToolStripButton btnEditDepartment;

		private ToolStripButton btnEditZones;

		public dfrmOperator()
		{
			this.InitializeComponent();
		}

		private void dfrmOperator_Load(object sender, EventArgs e)
		{
			this.btnEditDepartment.Text = wgAppConfig.ReplaceFloorRomm(this.btnEditDepartment.Text);
			this.loadOperatorPrivilege();
			this.loadOperatorData();
		}

		private void loadOperatorPrivilege()
		{
			bool flag;
			bool flag2;
			icOperator.getFrmOperatorPrivilege(base.Name.ToString(), out flag, out flag2);
			if (flag || flag2)
			{
				if (flag2)
				{
					return;
				}
				if (flag)
				{
					this.btnAdd.Visible = false;
					this.btnEdit.Visible = false;
					this.btnDelete.Visible = false;
					this.btnSetPassword.Visible = false;
					this.btnEditPrivilege.Visible = false;
					this.toolStrip1.Visible = false;
					return;
				}
			}
			else
			{
				base.Close();
			}
		}

		private void loadOperatorData()
		{
			string text = " SELECT f_OperatorID, f_OperatorName";
			text += " FROM t_s_Operator ";
			text += "  ORDER BY f_OperatorID ";
			this.table = new DataTable();
			this.dv = new DataView(this.table);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.table);
						}
					}
					goto IL_E3;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.table);
					}
				}
			}
			IL_E3:
			this.dgvOperators.AutoGenerateColumns = false;
			this.dgvOperators.DataSource = this.dv;
			for (int i = 0; i < this.dv.Table.Columns.Count; i++)
			{
				this.dgvOperators.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
			}
			if (this.dv.Count > 0)
			{
				this.btnAdd.Enabled = true;
				this.btnEdit.Enabled = true;
				this.btnDelete.Enabled = true;
				return;
			}
			this.btnAdd.Enabled = true;
			this.btnEdit.Enabled = false;
			this.btnDelete.Enabled = false;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmOperatorUpdate dfrmOperatorUpdate = new dfrmOperatorUpdate())
			{
				dfrmOperatorUpdate.operateMode = 0;
				dfrmOperatorUpdate.Text = this.btnAdd.Text + " " + dfrmOperatorUpdate.Text;
				if (dfrmOperatorUpdate.ShowDialog(this) == DialogResult.OK)
				{
					this.loadOperatorData();
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (this.dgvOperators.Rows.Count <= 0)
			{
				return;
			}
			int index = 0;
			if (this.dgvOperators.Rows.Count > 0)
			{
				index = this.dgvOperators.CurrentCell.RowIndex;
			}
			using (dfrmOperatorUpdate dfrmOperatorUpdate = new dfrmOperatorUpdate())
			{
				dfrmOperatorUpdate.Text = this.btnEdit.Text + " " + dfrmOperatorUpdate.Text;
				dfrmOperatorUpdate.operateMode = 1;
				dfrmOperatorUpdate.operatorID = int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString());
				dfrmOperatorUpdate.operatorName = this.dgvOperators.Rows[index].Cells[1].Value.ToString();
				if (dfrmOperatorUpdate.ShowDialog(this) == DialogResult.OK)
				{
					this.loadOperatorData();
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.dgvOperators.Rows.Count <= 0)
			{
				return;
			}
			int num = 0;
			if (this.dgvOperators.Rows.Count > 0)
			{
				num = this.dgvOperators.CurrentCell.RowIndex;
			}
			if (int.Parse(this.dgvOperators.Rows[num].Cells[0].Value.ToString()) == 1)
			{
				XMessageBox.Show(this, CommonStr.strDeleteForbidden, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (XMessageBox.Show(this, CommonStr.strDelete + " " + this.dgvOperators[1, num].Value.ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
			{
				return;
			}
			string text = " DELETE FROM [t_s_Operator] ";
			text = text + "WHERE  [f_OperatorID]=" + this.dgvOperators.Rows[num].Cells[0].Value.ToString();
			wgAppConfig.runUpdateSql(text);
			this.loadOperatorData();
		}

		private void btnSetPassword_Click(object sender, EventArgs e)
		{
			if (this.dgvOperators.Rows.Count <= 0)
			{
				return;
			}
			int index = 0;
			if (this.dgvOperators.Rows.Count > 0)
			{
				index = this.dgvOperators.CurrentCell.RowIndex;
			}
			using (dfrmOperatorUpdate dfrmOperatorUpdate = new dfrmOperatorUpdate())
			{
				dfrmOperatorUpdate.Text = this.btnSetPassword.Text;
				dfrmOperatorUpdate.operateMode = 2;
				dfrmOperatorUpdate.operatorID = int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString());
				dfrmOperatorUpdate.operatorName = this.dgvOperators.Rows[index].Cells[1].Value.ToString();
				dfrmOperatorUpdate.ShowDialog(this);
			}
		}

		private void btnEditPrivilege_Click(object sender, EventArgs e)
		{
			if (this.dgvOperators.Rows.Count <= 0)
			{
				return;
			}
			int index = 0;
			if (this.dgvOperators.Rows.Count > 0)
			{
				index = this.dgvOperators.CurrentCell.RowIndex;
			}
			if (int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString()) == 1)
			{
				XMessageBox.Show(this, CommonStr.strEditOperatePrivilegeForbidden, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmOperatePrivilege dfrmOperatePrivilege = new dfrmOperatePrivilege())
			{
				dfrmOperatePrivilege.Text = this.dgvOperators.Rows[index].Cells[1].Value.ToString() + "--" + dfrmOperatePrivilege.Text;
				dfrmOperatePrivilege.operatorID = int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString());
				dfrmOperatePrivilege.ShowDialog(this);
			}
		}

		private void btnEditDepartment_Click(object sender, EventArgs e)
		{
			if (this.dgvOperators.Rows.Count <= 0)
			{
				return;
			}
			int index = 0;
			if (this.dgvOperators.Rows.Count > 0)
			{
				index = this.dgvOperators.CurrentCell.RowIndex;
			}
			if (int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString()) == 1)
			{
				XMessageBox.Show(this, CommonStr.strEditOperatePrivilegeForbidden, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmOperatorDepartmentsConfiguration dfrmOperatorDepartmentsConfiguration = new dfrmOperatorDepartmentsConfiguration())
			{
				dfrmOperatorDepartmentsConfiguration.operatorId = int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString());
				dfrmOperatorDepartmentsConfiguration.ShowDialog(this);
			}
		}

		private void btnEditZones_Click(object sender, EventArgs e)
		{
			if (this.dgvOperators.Rows.Count <= 0)
			{
				return;
			}
			int index = 0;
			if (this.dgvOperators.Rows.Count > 0)
			{
				index = this.dgvOperators.CurrentCell.RowIndex;
			}
			if (int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString()) == 1)
			{
				XMessageBox.Show(this, CommonStr.strEditOperatePrivilegeForbidden, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmOperatorZonesConfiguration dfrmOperatorZonesConfiguration = new dfrmOperatorZonesConfiguration())
			{
				dfrmOperatorZonesConfiguration.operatorId = int.Parse(this.dgvOperators.Rows[index].Cells[0].Value.ToString());
				dfrmOperatorZonesConfiguration.ShowDialog(this);
			}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOperator));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.toolStrip1 = new ToolStrip();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnSetPassword = new ToolStripButton();
			this.btnEditPrivilege = new ToolStripButton();
			this.btnEditDepartment = new ToolStripButton();
			this.btnEditZones = new ToolStripButton();
			this.dgvOperators = new DataGridView();
			this.f_OperatorID = new DataGridViewTextBoxColumn();
			this.f_OperatorName = new DataGridViewTextBoxColumn();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.dgvOperators).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnSetPassword,
				this.btnEditPrivilege,
				this.btnEditDepartment,
				this.btnEditZones
			});
			this.toolStrip1.Name = "toolStrip1";
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Image = Resources.pTools_Add;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Image = Resources.pTools_Edit;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.ForeColor = Color.White;
			this.btnDelete.Image = Resources.pTools_Del;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			componentResourceManager.ApplyResources(this.btnSetPassword, "btnSetPassword");
			this.btnSetPassword.ForeColor = Color.White;
			this.btnSetPassword.Image = Resources.pTools_SetPwd;
			this.btnSetPassword.Name = "btnSetPassword";
			this.btnSetPassword.Click += new EventHandler(this.btnSetPassword_Click);
			componentResourceManager.ApplyResources(this.btnEditPrivilege, "btnEditPrivilege");
			this.btnEditPrivilege.ForeColor = Color.White;
			this.btnEditPrivilege.Image = Resources.pTools_EditPrivielge;
			this.btnEditPrivilege.Name = "btnEditPrivilege";
			this.btnEditPrivilege.Click += new EventHandler(this.btnEditPrivilege_Click);
			componentResourceManager.ApplyResources(this.btnEditDepartment, "btnEditDepartment");
			this.btnEditDepartment.ForeColor = Color.White;
			this.btnEditDepartment.Image = Resources.pTools_Operator_Group;
			this.btnEditDepartment.Name = "btnEditDepartment";
			this.btnEditDepartment.Click += new EventHandler(this.btnEditDepartment_Click);
			componentResourceManager.ApplyResources(this.btnEditZones, "btnEditZones");
			this.btnEditZones.ForeColor = Color.White;
			this.btnEditZones.Image = Resources.pTools_Operator_Zone;
			this.btnEditZones.Name = "btnEditZones";
			this.btnEditZones.Click += new EventHandler(this.btnEditZones_Click);
			componentResourceManager.ApplyResources(this.dgvOperators, "dgvOperators");
			this.dgvOperators.AllowUserToAddRows = false;
			this.dgvOperators.AllowUserToDeleteRows = false;
			this.dgvOperators.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvOperators.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvOperators.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvOperators.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_OperatorID,
				this.f_OperatorName
			});
			this.dgvOperators.EnableHeadersVisualStyles = false;
			this.dgvOperators.MultiSelect = false;
			this.dgvOperators.Name = "dgvOperators";
			this.dgvOperators.ReadOnly = true;
			this.dgvOperators.RowTemplate.Height = 23;
			this.dgvOperators.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.f_OperatorID, "f_OperatorID");
			this.f_OperatorID.Name = "f_OperatorID";
			this.f_OperatorID.ReadOnly = true;
			this.f_OperatorName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_OperatorName, "f_OperatorName");
			this.f_OperatorName.Name = "f_OperatorName";
			this.f_OperatorName.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.dgvOperators);
			base.Controls.Add(this.toolStrip1);
			base.Name = "dfrmOperator";
			base.Load += new EventHandler(this.dfrmOperator_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.dgvOperators).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
