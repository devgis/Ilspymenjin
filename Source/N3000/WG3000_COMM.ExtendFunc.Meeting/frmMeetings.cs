using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class frmMeetings : frmN3000
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private ToolStripButton btnPrint;

		private ToolStripButton btnExport;

		private ToolStripButton btnStat;

		private ToolStripButton btnRealtimeSign;

		private ToolStripButton btnExit;

		private ToolStripButton btnAddress;

		private DataGridView dgvMain;

		private DataGridViewTextBoxColumn MeetingNO;

		private DataGridViewTextBoxColumn MeetingName;

		private DataGridViewTextBoxColumn MeetingTime;

		private DataGridViewTextBoxColumn Addr;

		private DataGridViewTextBoxColumn Content;

		private DataGridViewTextBoxColumn Notes;

		private DataTable dt;

		private DataView dv;

		private dfrmFind dfrmFind1 = new dfrmFind();

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMeetings));
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.dgvMain = new DataGridView();
			this.MeetingNO = new DataGridViewTextBoxColumn();
			this.MeetingName = new DataGridViewTextBoxColumn();
			this.MeetingTime = new DataGridViewTextBoxColumn();
			this.Addr = new DataGridViewTextBoxColumn();
			this.Content = new DataGridViewTextBoxColumn();
			this.Notes = new DataGridViewTextBoxColumn();
			this.toolStrip1 = new ToolStrip();
			this.btnAddress = new ToolStripButton();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExport = new ToolStripButton();
			this.btnStat = new ToolStripButton();
			this.btnRealtimeSign = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			this.dgvMain.AllowUserToOrderColumns = true;
			this.dgvMain.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.MeetingNO,
				this.MeetingName,
				this.MeetingTime,
				this.Addr,
				this.Content,
				this.Notes
			});
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.MultiSelect = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvMain.DoubleClick += new EventHandler(this.dgvMain_DoubleClick);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.MeetingNO.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.MeetingNO, "MeetingNO");
			this.MeetingNO.Name = "MeetingNO";
			this.MeetingNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.MeetingName, "MeetingName");
			this.MeetingName.Name = "MeetingName";
			this.MeetingName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.MeetingTime, "MeetingTime");
			this.MeetingTime.Name = "MeetingTime";
			this.MeetingTime.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Addr, "Addr");
			this.Addr.Name = "Addr";
			this.Addr.ReadOnly = true;
			this.Content.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.Content, "Content");
			this.Content.Name = "Content";
			this.Content.ReadOnly = true;
			this.Notes.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.Notes, "Notes");
			this.Notes.Name = "Notes";
			this.Notes.ReadOnly = true;
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAddress,
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnPrint,
				this.btnExport,
				this.btnStat,
				this.btnRealtimeSign,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnAddress.ForeColor = Color.White;
			this.btnAddress.Image = Resources.pTools_TypeSetup;
			componentResourceManager.ApplyResources(this.btnAddress, "btnAddress");
			this.btnAddress.Name = "btnAddress";
			this.btnAddress.Click += new EventHandler(this.btnAddress_Click);
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
			this.btnExport.ForeColor = Color.White;
			this.btnExport.Image = Resources.pTools_ExportToExcel;
			componentResourceManager.ApplyResources(this.btnExport, "btnExport");
			this.btnExport.Name = "btnExport";
			this.btnExport.Click += new EventHandler(this.btnExport_Click);
			this.btnStat.ForeColor = Color.White;
			this.btnStat.Image = Resources.pTools_StatisticsReport;
			componentResourceManager.ApplyResources(this.btnStat, "btnStat");
			this.btnStat.Name = "btnStat";
			this.btnStat.Click += new EventHandler(this.btnStat_Click);
			this.btnRealtimeSign.ForeColor = Color.White;
			this.btnRealtimeSign.Image = Resources.pTools_Edit_Batch;
			componentResourceManager.ApplyResources(this.btnRealtimeSign, "btnRealtimeSign");
			this.btnRealtimeSign.Name = "btnRealtimeSign";
			this.btnRealtimeSign.Click += new EventHandler(this.btnRealtimeSign_Click);
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmMeetings";
			base.FormClosing += new FormClosingEventHandler(this.frmMeetings_FormClosing);
			base.Load += new EventHandler(this.frmMeetings_Load);
			base.KeyDown += new KeyEventHandler(this.frmMeetings_KeyDown);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public static string getStrMeetingIdentity(long id)
		{
			string result = "";
			try
			{
				long num = id;
				if (num <= 5L && num >= 0L)
				{
					switch ((int)num)
					{
					case 0:
						result = CommonStr.strMeetingIdentity0;
						goto IL_6B;
					case 1:
						result = CommonStr.strMeetingIdentity1;
						goto IL_6B;
					case 2:
						result = CommonStr.strMeetingIdentity2;
						goto IL_6B;
					case 3:
						result = CommonStr.strMeetingIdentity3;
						goto IL_6B;
					case 4:
						result = CommonStr.strMeetingIdentity4;
						goto IL_6B;
					case 5:
						result = CommonStr.strMeetingIdentity5;
						goto IL_6B;
					}
				}
				result = id.ToString();
				IL_6B:;
			}
			catch
			{
			}
			return result;
		}

		public static string getStrSignWay(long id)
		{
			string result = "";
			try
			{
				long num = id;
				if (num <= 2L && num >= 0L)
				{
					switch ((int)num)
					{
					case 0:
						result = CommonStr.strSignWay0;
						goto IL_47;
					case 1:
						result = CommonStr.strSignWay1;
						goto IL_47;
					case 2:
						result = CommonStr.strSignWay2;
						goto IL_47;
					}
				}
				result = id.ToString();
				IL_47:;
			}
			catch
			{
			}
			return result;
		}

		public frmMeetings()
		{
			this.InitializeComponent();
		}

		private void frmMeetings_Load(object sender, EventArgs e)
		{
			this.loadOperatorPrivilege();
			this.loadMeetingData();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuMeeting";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnEdit.Visible = false;
				this.btnDelete.Visible = false;
				this.btnRealtimeSign.Visible = false;
			}
		}

		private void loadMeetingData()
		{
			string cmdText = "SELECT [f_MeetingNO], [f_MeetingName], [f_MeetingDateTime], [f_MeetingAdr], [f_Content], [f_Notes] FROM t_d_Meeting ";
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dt);
						}
					}
					goto IL_CB;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dt);
					}
				}
			}
			IL_CB:
			DataGridView dataGridView = this.dgvMain;
			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = this.dv;
			for (int i = 0; i < this.dv.Table.Columns.Count; i++)
			{
				dataGridView.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
				dataGridView.Columns[i].Name = this.dv.Table.Columns[i].ColumnName;
			}
			wgAppConfig.setDisplayFormatDate(dataGridView, "f_MeetingDateTime", wgTools.DisplayFormat_DateYMDHMSWeek);
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

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmMeetingSet dfrmMeetingSet = new dfrmMeetingSet())
			{
				dfrmMeetingSet.ShowDialog(this);
				this.loadMeetingData();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
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
			using (dfrmMeetingSet dfrmMeetingSet = new dfrmMeetingSet())
			{
				dfrmMeetingSet.curMeetingNo = this.dgvMain.Rows[index].Cells[0].Value.ToString();
				if (dfrmMeetingSet.ShowDialog(this) == DialogResult.OK)
				{
					this.loadMeetingData();
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

		private void dgvMain_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
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
			string text = string.Format("{0}\r\n\r\n{1}:  {2}", this.btnDelete.Text, this.dgvMain.Columns[0].HeaderText, this.dgvMain.Rows[index].Cells[0].Value.ToString());
			text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			string strSql = " DELETE FROM t_d_Meeting WHERE [f_MeetingNO]= " + wgTools.PrepareStr(this.dgvMain.Rows[index].Cells[0].Value.ToString());
			wgAppConfig.runUpdateSql(strSql);
			strSql = " DELETE FROM t_d_MeetingConsumer WHERE [f_MeetingNO]= " + wgTools.PrepareStr(this.dgvMain.Rows[index].Cells[0].Value.ToString());
			wgAppConfig.runUpdateSql(strSql);
			this.loadMeetingData();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcel(this.dgvMain, this.Text);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvMain, this.Text);
		}

		private void btnAddress_Click(object sender, EventArgs e)
		{
			using (dfrmMeetingAdr dfrmMeetingAdr = new dfrmMeetingAdr())
			{
				dfrmMeetingAdr.ShowDialog();
			}
		}

		private void btnRealtimeSign_Click(object sender, EventArgs e)
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
			try
			{
				DataView dataView = new DataView(this.dv.Table);
				dataView.RowFilter = "f_MeetingNO = " + wgTools.PrepareStr(this.dgvMain.Rows[index].Cells[0].Value.ToString());
				if (dataView.Count > 0)
				{
					if (!(((DateTime)dataView[0]["f_MeetingDateTime"]).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")))
					{
						string text = dataView[0]["f_MeetingName"].ToString();
						text = text + "\r\n\r\n" + string.Format(CommonStr.strMeetingDate + ": ", new object[0]);
						text += ((DateTime)dataView[0]["f_MeetingDateTime"]).ToString("yyyy-MM-dd");
						text = text + ", " + CommonStr.strMeetingSystemDate + ": ";
						text += DateTime.Now.ToString("yyyy-MM-dd");
						text = text + " , " + CommonStr.strMeetingMismatch + "?";
						if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
						{
							return;
						}
					}
					dfrmMeetingSign dfrmMeetingSign = new dfrmMeetingSign();
					dfrmMeetingSign.TopMost = true;
					dfrmMeetingSign.curMeetingNo = this.dgvMain.Rows[index].Cells[0].Value.ToString();
					base.Hide();
					dfrmMeetingSign.ShowDialog(this);
					base.Close();
				}
			}
			catch
			{
			}
		}

		private void btnStat_Click(object sender, EventArgs e)
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
			try
			{
				if (new DataView(this.dv.Table)
				{
					RowFilter = "f_MeetingNO = " + wgTools.PrepareStr(this.dgvMain.Rows[index].Cells[0].Value.ToString())
				}.Count > 0)
				{
					new dfrmMeetingStatDetail
					{
						TopMost = true,
						curMeetingNo = this.dgvMain.Rows[index].Cells[0].Value.ToString()
					}.ShowDialog(this);
				}
			}
			catch
			{
			}
		}

		private void frmMeetings_KeyDown(object sender, KeyEventArgs e)
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

		private void frmMeetings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
