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

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class frmPatrolRoute : frmN3000
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private ToolStripButton btnPrint;

		private ToolStripButton btnExportToExcel;

		private DataGridView dgvMain;

		private DataGridViewTextBoxColumn f_ShiftID;

		private DataGridViewTextBoxColumn f_ReadTimes;

		private ToolStripButton btnExit;

		private DataTable dt;

		private DataView dv;

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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPatrolRoute));
			this.dgvMain = new DataGridView();
			this.f_ShiftID = new DataGridViewTextBoxColumn();
			this.f_ReadTimes = new DataGridViewTextBoxColumn();
			this.toolStrip1 = new ToolStrip();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			this.dgvMain.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ShiftID,
				this.f_ReadTimes
			});
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvMain.DoubleClick += new EventHandler(this.dgvControlSegs_DoubleClick);
			componentResourceManager.ApplyResources(this.f_ShiftID, "f_ShiftID");
			this.f_ShiftID.Name = "f_ShiftID";
			this.f_ShiftID.ReadOnly = true;
			this.f_ReadTimes.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_ReadTimes, "f_ReadTimes");
			this.f_ReadTimes.Name = "f_ReadTimes";
			this.f_ReadTimes.ReadOnly = true;
			this.f_ReadTimes.Resizable = DataGridViewTriState.True;
			this.f_ReadTimes.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnPrint,
				this.btnExportToExcel,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Image = Resources.pTools_Add;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Image = Resources.pTools_Edit;
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			this.btnDelete.ForeColor = Color.White;
			this.btnDelete.Image = Resources.pTools_Del;
			componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this, "$this");
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmPatrolRoute";
			base.Load += new EventHandler(this.frmShiftOtherTypes_Load);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmPatrolRoute()
		{
			this.InitializeComponent();
		}

		private void frmShiftOtherTypes_Load(object sender, EventArgs e)
		{
			this.Refresh();
			this.loadOperatorPrivilege();
			this.loadData();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuPatrolDetailData";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnEdit.Visible = false;
				this.btnDelete.Visible = false;
			}
		}

		private void loadData()
		{
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
			string text = " SELECT ";
			text += " [f_RouteID], [f_RouteName] ";
			text += "  FROM [t_d_PatrolRouteList] ORDER BY [f_RouteID] ASC  ";
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dt);
						}
					}
					goto IL_E9;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dt);
					}
				}
			}
			IL_E9:
			DataGridView dataGridView = this.dgvMain;
			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = this.dv;
			for (int i = 0; i < this.dv.Table.Columns.Count; i++)
			{
				dataGridView.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
			}
			if (this.dv.Count > 0)
			{
				for (int i = 0; i < this.dv.Count; i++)
				{
				}
				this.btnAdd.Enabled = true;
				this.btnEdit.Enabled = true;
				this.btnDelete.Enabled = true;
				this.btnPrint.Enabled = true;
				return;
			}
			this.btnAdd.Enabled = true;
			this.btnEdit.Enabled = false;
			this.btnDelete.Enabled = false;
			this.btnPrint.Enabled = false;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmRouteEdit dfrmRouteEdit = new dfrmRouteEdit())
			{
				if (dfrmRouteEdit.ShowDialog(this) == DialogResult.OK)
				{
					this.loadData();
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.dgvMain.Rows.Count > 0)
			{
				num = this.dgvMain.CurrentCell.RowIndex;
			}
			int index;
			if (this.dgvMain.SelectedRows.Count <= 0)
			{
				if (this.dgvMain.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvMain.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvMain.SelectedRows[0].Index;
			}
			using (dfrmRouteEdit dfrmRouteEdit = new dfrmRouteEdit())
			{
				dfrmRouteEdit.currentRouteID = int.Parse(this.dgvMain.Rows[index].Cells[0].Value.ToString());
				if (dfrmRouteEdit.ShowDialog(this) == DialogResult.OK)
				{
					this.loadData();
				}
			}
			if (this.dgvMain.RowCount > 0)
			{
				if (this.dgvMain.RowCount > num)
				{
					this.dgvMain.CurrentCell = this.dgvMain[1, num];
					return;
				}
				this.dgvMain.CurrentCell = this.dgvMain[1, this.dgvMain.RowCount - 1];
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int index;
			if (this.dgvMain.SelectedRows.Count <= 0)
			{
				if (this.dgvMain.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvMain.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvMain.SelectedRows[0].Index;
			}
			string text = string.Format("{0}\r\n{1}:  {2}", this.btnDelete.Text, this.dgvMain.Columns[0].HeaderText, this.dgvMain.Rows[index].Cells[0].Value.ToString());
			text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(this, text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			int num = int.Parse(this.dgvMain.Rows[index].Cells[0].Value.ToString());
			string strSql = " DELETE FROM t_d_PatrolRouteList WHERE f_RouteID = " + num.ToString();
			wgAppConfig.runUpdateSql(strSql);
			strSql = " DELETE FROM t_d_PatrolRouteDetail WHERE f_RouteID = " + num.ToString();
			wgAppConfig.runUpdateSql(strSql);
			this.loadData();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvMain, this.Text);
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcel(this.dgvMain, this.Text);
		}

		private void dgvControlSegs_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
