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
	public class frmControlSegs : frmN3000
	{
		private DataTable dt;

		private DataView dv;

		private bool firstShow = true;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private ToolStripButton btnPrint;

		private ToolStripButton btnExportToExcel;

		private ToolStrip toolStrip2;

		private ToolStripLabel toolStripLabel1;

		private DataGridView dgvControlSegs;

		private ToolStripButton btnHolidayControl;

		private DataGridViewTextBoxColumn f_ControlSegIDBak;

		private DataGridViewTextBoxColumn f_ControlSegID;

		private DataGridViewCheckBoxColumn f_Monday;

		private DataGridViewCheckBoxColumn f_Tuesday;

		private DataGridViewCheckBoxColumn f_Wednesday;

		private DataGridViewCheckBoxColumn f_Thursday;

		private DataGridViewCheckBoxColumn f_Friday;

		private DataGridViewCheckBoxColumn f_Saturday;

		private DataGridViewCheckBoxColumn f_Sunday;

		private DataGridViewTextBoxColumn f_BeginHMS1A;

		private DataGridViewTextBoxColumn f_EndHMS1A;

		private DataGridViewTextBoxColumn f_BeginHMS2A;

		private DataGridViewTextBoxColumn f_EndHMS2A;

		private DataGridViewTextBoxColumn f_BeginHMS3A;

		private DataGridViewTextBoxColumn f_EndHMS3A;

		private DataGridViewTextBoxColumn f_ControlSegIDLinked;

		private DataGridViewTextBoxColumn f_BeginYMD;

		private DataGridViewTextBoxColumn f_EndYMD;

		private DataGridViewCheckBoxColumn f_ControlByHoliday;

		public frmControlSegs()
		{
			this.InitializeComponent();
		}

		private void frmControlSegs_Load(object sender, EventArgs e)
		{
			this.loadOperatorPrivilege();
			this.loadControlSegData();
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
					return;
				}
			}
			else
			{
				base.Close();
			}
		}

		private void loadControlSegData()
		{
			string text = " SELECT ";
			text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
			if (wgAppConfig.IsAccessDB)
			{
				text += "  IIF(ISNULL([f_ControlSegName]), CSTR([t_b_ControlSeg].[f_ControlSegID]), CSTR([t_b_ControlSeg].[f_ControlSegID]) & ' [' & [f_ControlSegName] & ']') AS f_ControlSegID, ";
				text += " [f_Monday], [f_Tuesday], [f_Wednesday]";
				text += " , [f_Thursday], [f_Friday], [f_Saturday], [f_Sunday] ";
				text += " , Format([f_BeginHMS1],'Short Time')  as [f_BeginHMS1A]";
				text += ",Format([f_EndHMS1],'Short Time')  as [f_EndHMS1A]";
				text += ", Format([f_BeginHMS2],'Short Time')  as [f_BeginHMS2A]";
				text += ",Format([f_EndHMS2],'Short Time')  as [f_EndHMS2A]";
				text += ", Format([f_BeginHMS3],'Short Time')  as [f_BeginHMS3A]";
				text += ",Format([f_EndHMS3],'Short Time')  as [f_EndHMS3A]";
				text += "  ,f_ControlSegIDLinked,f_BeginYMD, f_EndYMD  ";
				text += ",   f_ControlByHoliday  ";
				text += " ";
				text += "  FROM [t_b_ControlSeg] ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
			}
			else
			{
				text += "   CASE WHEN [f_ControlSegName] IS NULL THEN CONVERT(nvarchar(50),  ";
				text += "     ([t_b_ControlSeg].[f_ControlSegID])) ELSE (CONVERT(nvarchar(50), ";
				text += "     ([t_b_ControlSeg].[f_ControlSegID])) + ' [' + [f_ControlSegName] + ']') ";
				text += "    END AS f_ControlSegID, [f_Monday], [f_Tuesday], [f_Wednesday], [f_Thursday], ";
				text += "   [f_Friday], [f_Saturday], [f_Sunday], ";
				text += " ISNULL(CONVERT(char(5), f_BeginHMS1,108) , '00:00') AS [f_BeginHMS1A], ";
				text += " ISNULL(CONVERT(char(5), f_EndHMS1,108) , '00:00')  AS [f_EndHMS1A], ";
				text += " ISNULL(CONVERT(char(5), f_BeginHMS2,108) , '00:00')  AS [f_BeginHMS2A], ";
				text += " ISNULL(CONVERT(char(5), f_EndHMS2,108) , '00:00')  AS [f_EndHMS2A], ";
				text += " ISNULL(CONVERT(char(5), f_BeginHMS3,108) , '00:00')  AS [f_BeginHMS3A], ";
				text += " ISNULL(CONVERT(char(5), f_EndHMS3,108) , '00:00')  AS [f_EndHMS3A] ";
				text += "  ,f_ControlSegIDLinked, ";
				text += "   f_BeginYMD, ";
				text += "   f_EndYMD  ";
				text += ",   f_ControlByHoliday  ";
				text += "  FROM [t_b_ControlSeg] ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
			}
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
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
					goto IL_242;
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
			IL_242:
			DataGridView dataGridView = this.dgvControlSegs;
			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = this.dv;
			for (int i = 0; i < this.dv.Table.Columns.Count; i++)
			{
				dataGridView.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
				dataGridView.Columns[i].Name = this.dv.Table.Columns[i].ColumnName;
			}
			wgAppConfig.setDisplayFormatDate(dataGridView, "f_BeginYMD", wgTools.DisplayFormat_DateYMD);
			wgAppConfig.setDisplayFormatDate(dataGridView, "f_EndYMD", wgTools.DisplayFormat_DateYMD);
			using (DataView dataView = new DataView(this.dt))
			{
				dataView.RowFilter = "f_ControlByHoliday = 0";
				if (dataView.Count == 0)
				{
					dataGridView.Columns["f_ControlByHoliday"].Visible = false;
				}
				else
				{
					dataGridView.Columns["f_ControlByHoliday"].Visible = true;
				}
			}
			if (this.dv.Count > 0)
			{
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

		private void showUpload()
		{
			if (this.firstShow)
			{
				this.firstShow = false;
				XMessageBox.Show(CommonStr.strNeedUploadControlTimeSeg);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmControlSeg dfrmControlSeg = new dfrmControlSeg())
			{
				dfrmControlSeg.operateMode = "New";
				if (dfrmControlSeg.ShowDialog(this) == DialogResult.OK)
				{
					this.loadControlSegData();
					this.showUpload();
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			int index;
			if (this.dgvControlSegs.SelectedRows.Count <= 0)
			{
				if (this.dgvControlSegs.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvControlSegs.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvControlSegs.SelectedRows[0].Index;
			}
			using (dfrmControlSeg dfrmControlSeg = new dfrmControlSeg())
			{
				dfrmControlSeg.curControlSegID = int.Parse(this.dgvControlSegs.Rows[index].Cells[0].Value.ToString());
				if (dfrmControlSeg.ShowDialog(this) == DialogResult.OK)
				{
					this.loadControlSegData();
					this.showUpload();
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int index;
			if (this.dgvControlSegs.SelectedRows.Count <= 0)
			{
				if (this.dgvControlSegs.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvControlSegs.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvControlSegs.SelectedRows[0].Index;
			}
			string text = string.Format("{0}\r\n\r\n{1}:  {2}", this.btnDelete.Text, this.dgvControlSegs.Columns[1].HeaderText, this.dgvControlSegs.Rows[index].Cells[0].Value.ToString());
			text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			string strSql = " DELETE FROM t_b_ControlSeg WHERE [f_ControlSegID]= " + this.dgvControlSegs.Rows[index].Cells[0].Value.ToString();
			wgAppConfig.runUpdateSql(strSql);
			this.loadControlSegData();
			this.showUpload();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvControlSegs, this.Text);
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcel(this.dgvControlSegs, this.Text);
		}

		private void dgvControlSegs_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}

		private void btnHolidayControl_Click(object sender, EventArgs e)
		{
			using (dfrmControlHolidaySet dfrmControlHolidaySet = new dfrmControlHolidaySet())
			{
				dfrmControlHolidaySet.ShowDialog();
			}
			this.showUpload();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmControlSegs));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.dgvControlSegs = new DataGridView();
			this.f_ControlSegIDBak = new DataGridViewTextBoxColumn();
			this.f_ControlSegID = new DataGridViewTextBoxColumn();
			this.f_Monday = new DataGridViewCheckBoxColumn();
			this.f_Tuesday = new DataGridViewCheckBoxColumn();
			this.f_Wednesday = new DataGridViewCheckBoxColumn();
			this.f_Thursday = new DataGridViewCheckBoxColumn();
			this.f_Friday = new DataGridViewCheckBoxColumn();
			this.f_Saturday = new DataGridViewCheckBoxColumn();
			this.f_Sunday = new DataGridViewCheckBoxColumn();
			this.f_BeginHMS1A = new DataGridViewTextBoxColumn();
			this.f_EndHMS1A = new DataGridViewTextBoxColumn();
			this.f_BeginHMS2A = new DataGridViewTextBoxColumn();
			this.f_EndHMS2A = new DataGridViewTextBoxColumn();
			this.f_BeginHMS3A = new DataGridViewTextBoxColumn();
			this.f_EndHMS3A = new DataGridViewTextBoxColumn();
			this.f_ControlSegIDLinked = new DataGridViewTextBoxColumn();
			this.f_BeginYMD = new DataGridViewTextBoxColumn();
			this.f_EndYMD = new DataGridViewTextBoxColumn();
			this.f_ControlByHoliday = new DataGridViewCheckBoxColumn();
			this.toolStrip2 = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnHolidayControl = new ToolStripButton();
			((ISupportInitialize)this.dgvControlSegs).BeginInit();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.dgvControlSegs, "dgvControlSegs");
			this.dgvControlSegs.AllowUserToAddRows = false;
			this.dgvControlSegs.AllowUserToDeleteRows = false;
			this.dgvControlSegs.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvControlSegs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvControlSegs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvControlSegs.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ControlSegIDBak,
				this.f_ControlSegID,
				this.f_Monday,
				this.f_Tuesday,
				this.f_Wednesday,
				this.f_Thursday,
				this.f_Friday,
				this.f_Saturday,
				this.f_Sunday,
				this.f_BeginHMS1A,
				this.f_EndHMS1A,
				this.f_BeginHMS2A,
				this.f_EndHMS2A,
				this.f_BeginHMS3A,
				this.f_EndHMS3A,
				this.f_ControlSegIDLinked,
				this.f_BeginYMD,
				this.f_EndYMD,
				this.f_ControlByHoliday
			});
			this.dgvControlSegs.EnableHeadersVisualStyles = false;
			this.dgvControlSegs.Name = "dgvControlSegs";
			this.dgvControlSegs.ReadOnly = true;
			this.dgvControlSegs.RowTemplate.Height = 23;
			this.dgvControlSegs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvControlSegs.DoubleClick += new EventHandler(this.dgvControlSegs_DoubleClick);
			componentResourceManager.ApplyResources(this.f_ControlSegIDBak, "f_ControlSegIDBak");
			this.f_ControlSegIDBak.Name = "f_ControlSegIDBak";
			this.f_ControlSegIDBak.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControlSegID, "f_ControlSegID");
			this.f_ControlSegID.Name = "f_ControlSegID";
			this.f_ControlSegID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Monday, "f_Monday");
			this.f_Monday.Name = "f_Monday";
			this.f_Monday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Tuesday, "f_Tuesday");
			this.f_Tuesday.Name = "f_Tuesday";
			this.f_Tuesday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Wednesday, "f_Wednesday");
			this.f_Wednesday.Name = "f_Wednesday";
			this.f_Wednesday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Thursday, "f_Thursday");
			this.f_Thursday.Name = "f_Thursday";
			this.f_Thursday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Friday, "f_Friday");
			this.f_Friday.Name = "f_Friday";
			this.f_Friday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Saturday, "f_Saturday");
			this.f_Saturday.Name = "f_Saturday";
			this.f_Saturday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Sunday, "f_Sunday");
			this.f_Sunday.Name = "f_Sunday";
			this.f_Sunday.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_BeginHMS1A.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_BeginHMS1A, "f_BeginHMS1A");
			this.f_BeginHMS1A.Name = "f_BeginHMS1A";
			this.f_BeginHMS1A.ReadOnly = true;
			this.f_BeginHMS1A.Resizable = DataGridViewTriState.True;
			this.f_BeginHMS1A.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_EndHMS1A, "f_EndHMS1A");
			this.f_EndHMS1A.Name = "f_EndHMS1A";
			this.f_EndHMS1A.ReadOnly = true;
			this.f_EndHMS1A.Resizable = DataGridViewTriState.True;
			this.f_EndHMS1A.SortMode = DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_BeginHMS2A.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_BeginHMS2A, "f_BeginHMS2A");
			this.f_BeginHMS2A.Name = "f_BeginHMS2A";
			this.f_BeginHMS2A.ReadOnly = true;
			this.f_BeginHMS2A.Resizable = DataGridViewTriState.True;
			this.f_BeginHMS2A.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_EndHMS2A, "f_EndHMS2A");
			this.f_EndHMS2A.Name = "f_EndHMS2A";
			this.f_EndHMS2A.ReadOnly = true;
			this.f_EndHMS2A.Resizable = DataGridViewTriState.True;
			this.f_EndHMS2A.SortMode = DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_BeginHMS3A.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_BeginHMS3A, "f_BeginHMS3A");
			this.f_BeginHMS3A.Name = "f_BeginHMS3A";
			this.f_BeginHMS3A.ReadOnly = true;
			this.f_BeginHMS3A.Resizable = DataGridViewTriState.True;
			this.f_BeginHMS3A.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_EndHMS3A, "f_EndHMS3A");
			this.f_EndHMS3A.Name = "f_EndHMS3A";
			this.f_EndHMS3A.ReadOnly = true;
			this.f_EndHMS3A.Resizable = DataGridViewTriState.True;
			this.f_EndHMS3A.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_ControlSegIDLinked, "f_ControlSegIDLinked");
			this.f_ControlSegIDLinked.Name = "f_ControlSegIDLinked";
			this.f_ControlSegIDLinked.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_BeginYMD, "f_BeginYMD");
			this.f_BeginYMD.Name = "f_BeginYMD";
			this.f_BeginYMD.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_EndYMD, "f_EndYMD");
			this.f_EndYMD.Name = "f_EndYMD";
			this.f_EndYMD.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControlByHoliday, "f_ControlByHoliday");
			this.f_ControlByHoliday.Name = "f_ControlByHoliday";
			this.f_ControlByHoliday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.BackColor = Color.Transparent;
			this.toolStrip2.BackgroundImage = Resources.pTools_first_title;
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1
			});
			this.toolStrip2.Name = "toolStrip2";
			componentResourceManager.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			this.toolStripLabel1.ForeColor = Color.White;
			this.toolStripLabel1.Name = "toolStripLabel1";
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnPrint,
				this.btnExportToExcel,
				this.btnHolidayControl
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
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			componentResourceManager.ApplyResources(this.btnHolidayControl, "btnHolidayControl");
			this.btnHolidayControl.ForeColor = Color.White;
			this.btnHolidayControl.Image = Resources.pTools_Add_Child;
			this.btnHolidayControl.Name = "btnHolidayControl";
			this.btnHolidayControl.Click += new EventHandler(this.btnHolidayControl_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.dgvControlSegs);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmControlSegs";
			base.Load += new EventHandler(this.frmControlSegs_Load);
			((ISupportInitialize)this.dgvControlSegs).EndInit();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
