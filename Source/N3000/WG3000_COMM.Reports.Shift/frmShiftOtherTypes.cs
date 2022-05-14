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

namespace WG3000_COMM.Reports.Shift
{
	public class frmShiftOtherTypes : frmN3000
	{
		private DataTable dt;

		private DataView dv;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private ToolStripButton btnPrint;

		private ToolStripButton btnExportToExcel;

		private ToolStrip toolStrip2;

		private ToolStripLabel toolStripLabel1;

		private DataGridView dgvMain;

		private DataGridViewTextBoxColumn f_ShiftID;

		private DataGridViewTextBoxColumn f_ShiftName;

		private DataGridViewTextBoxColumn f_ReadTimes;

		private DataGridViewCheckBoxColumn f_bOvertimeShift;

		private DataGridViewTextBoxColumn f_OnDuty1t;

		private DataGridViewTextBoxColumn f_OffDuty1t;

		private DataGridViewTextBoxColumn f_OnDuty2t;

		private DataGridViewTextBoxColumn f_OffDuty2t;

		private DataGridViewTextBoxColumn f_OnDuty3t;

		private DataGridViewTextBoxColumn f_OffDuty3t;

		private DataGridViewTextBoxColumn f_OnDuty4t;

		private DataGridViewTextBoxColumn f_OffDuty4t;

		public frmShiftOtherTypes()
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
			string funName = "mnuShiftSet";
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
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT ";
				text += " [f_ShiftID], [f_ShiftName], [f_ReadTimes], [f_bOvertimeShift]";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OnDuty1],'Short Time') )   AS [f_OnDuty1t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OffDuty1],'Short Time') )  AS [f_OffDuty1t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OnDuty2],'Short Time') )   AS [f_OnDuty2t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OffDuty2],'Short Time') )  AS [f_OffDuty2t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OnDuty3],'Short Time') )   AS [f_OnDuty3t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OffDuty3],'Short Time') )  AS [f_OffDuty3t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OnDuty4],'Short Time') )   AS [f_OnDuty4t] ";
				text += " ,  IIF(f_OnDuty1 IS NULL , ' ',  Format([f_OffDuty4],'Short Time') )  AS [f_OffDuty4t] ";
				text += "  FROM [t_b_ShiftSet] ORDER BY [f_ShiftID] ASC  ";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dt);
						}
					}
					goto IL_1CD;
				}
			}
			text = " SELECT ";
			text += " [f_ShiftID], [f_ShiftName], [f_ReadTimes], [f_bOvertimeShift]";
			text += " ,ISNULL(CONVERT(char(5), f_OnDuty1,108) , ' ') AS [f_OnDuty1t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OffDuty1,108) , ' ') AS [f_OffDuty1t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OnDuty2,108) , ' ') AS [f_OnDuty2t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OffDuty2,108) , ' ') AS [f_OffDuty2t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OnDuty3,108) , ' ') AS [f_OnDuty3t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OffDuty3,108) , ' ') AS [f_OffDuty3t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OnDuty4,108) , ' ') AS [f_OnDuty4t] ";
			text += " ,ISNULL(CONVERT(char(5), f_OffDuty4,108) , ' ') AS [f_OffDuty4t] ";
			text += "  FROM [t_b_ShiftSet] ORDER BY [f_ShiftID] ASC  ";
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
			IL_1CD:
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
			using (dfrmShiftOtherTypeSet dfrmShiftOtherTypeSet = new dfrmShiftOtherTypeSet())
			{
				dfrmShiftOtherTypeSet.operateMode = "New";
				if (dfrmShiftOtherTypeSet.ShowDialog(this) == DialogResult.OK)
				{
					this.loadData();
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
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
			using (dfrmShiftOtherTypeSet dfrmShiftOtherTypeSet = new dfrmShiftOtherTypeSet())
			{
				dfrmShiftOtherTypeSet.curShiftID = int.Parse(this.dgvMain.Rows[index].Cells[0].Value.ToString());
				if (dfrmShiftOtherTypeSet.ShowDialog(this) == DialogResult.OK)
				{
					this.loadData();
				}
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
			if (wgAppConfig.IsAccessDB)
			{
				using (comShift_Acc comShift_Acc = new comShift_Acc())
				{
					comShift_Acc.shift_delete(int.Parse(this.dgvMain.Rows[index].Cells[0].Value.ToString()));
					goto IL_15F;
				}
			}
			using (comShift comShift = new comShift())
			{
				comShift.shift_delete(int.Parse(this.dgvMain.Rows[index].Cells[0].Value.ToString()));
			}
			IL_15F:
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmShiftOtherTypes));
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.dgvMain = new DataGridView();
			this.f_ShiftID = new DataGridViewTextBoxColumn();
			this.f_ShiftName = new DataGridViewTextBoxColumn();
			this.f_ReadTimes = new DataGridViewTextBoxColumn();
			this.f_bOvertimeShift = new DataGridViewCheckBoxColumn();
			this.f_OnDuty1t = new DataGridViewTextBoxColumn();
			this.f_OffDuty1t = new DataGridViewTextBoxColumn();
			this.f_OnDuty2t = new DataGridViewTextBoxColumn();
			this.f_OffDuty2t = new DataGridViewTextBoxColumn();
			this.f_OnDuty3t = new DataGridViewTextBoxColumn();
			this.f_OffDuty3t = new DataGridViewTextBoxColumn();
			this.f_OnDuty4t = new DataGridViewTextBoxColumn();
			this.f_OffDuty4t = new DataGridViewTextBoxColumn();
			this.toolStrip2 = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip2.SuspendLayout();
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
				this.f_ShiftName,
				this.f_ReadTimes,
				this.f_bOvertimeShift,
				this.f_OnDuty1t,
				this.f_OffDuty1t,
				this.f_OnDuty2t,
				this.f_OffDuty2t,
				this.f_OnDuty3t,
				this.f_OffDuty3t,
				this.f_OnDuty4t,
				this.f_OffDuty4t
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
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ShiftName.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_ShiftName, "f_ShiftName");
			this.f_ShiftName.Name = "f_ShiftName";
			this.f_ShiftName.ReadOnly = true;
			this.f_ShiftName.Resizable = DataGridViewTriState.True;
			this.f_ShiftName.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_ReadTimes, "f_ReadTimes");
			this.f_ReadTimes.Name = "f_ReadTimes";
			this.f_ReadTimes.ReadOnly = true;
			this.f_ReadTimes.Resizable = DataGridViewTriState.True;
			this.f_ReadTimes.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_bOvertimeShift, "f_bOvertimeShift");
			this.f_bOvertimeShift.Name = "f_bOvertimeShift";
			this.f_bOvertimeShift.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_OnDuty1t.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_OnDuty1t, "f_OnDuty1t");
			this.f_OnDuty1t.Name = "f_OnDuty1t";
			this.f_OnDuty1t.ReadOnly = true;
			this.f_OnDuty1t.Resizable = DataGridViewTriState.True;
			this.f_OnDuty1t.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_OffDuty1t, "f_OffDuty1t");
			this.f_OffDuty1t.Name = "f_OffDuty1t";
			this.f_OffDuty1t.ReadOnly = true;
			this.f_OffDuty1t.Resizable = DataGridViewTriState.True;
			this.f_OffDuty1t.SortMode = DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_OnDuty2t.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_OnDuty2t, "f_OnDuty2t");
			this.f_OnDuty2t.Name = "f_OnDuty2t";
			this.f_OnDuty2t.ReadOnly = true;
			this.f_OnDuty2t.Resizable = DataGridViewTriState.True;
			this.f_OnDuty2t.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_OffDuty2t, "f_OffDuty2t");
			this.f_OffDuty2t.Name = "f_OffDuty2t";
			this.f_OffDuty2t.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty3t, "f_OnDuty3t");
			this.f_OnDuty3t.Name = "f_OnDuty3t";
			this.f_OnDuty3t.ReadOnly = true;
			this.f_OnDuty3t.Resizable = DataGridViewTriState.True;
			this.f_OnDuty3t.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_OffDuty3t, "f_OffDuty3t");
			this.f_OffDuty3t.Name = "f_OffDuty3t";
			this.f_OffDuty3t.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty4t, "f_OnDuty4t");
			this.f_OnDuty4t.Name = "f_OnDuty4t";
			this.f_OnDuty4t.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OffDuty4t, "f_OffDuty4t");
			this.f_OffDuty4t.Name = "f_OffDuty4t";
			this.f_OffDuty4t.ReadOnly = true;
			this.toolStrip2.BackColor = Color.Transparent;
			this.toolStrip2.BackgroundImage = Resources.pTools_second_title;
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1
			});
			this.toolStrip2.Name = "toolStrip2";
			this.toolStripLabel1.ForeColor = Color.White;
			this.toolStripLabel1.Name = "toolStripLabel1";
			componentResourceManager.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnPrint,
				this.btnExportToExcel
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
			componentResourceManager.ApplyResources(this, "$this");
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmShiftOtherTypes";
			base.Load += new EventHandler(this.frmShiftOtherTypes_Load);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
