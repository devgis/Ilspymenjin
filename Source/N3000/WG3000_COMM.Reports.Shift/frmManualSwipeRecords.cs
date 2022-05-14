using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class frmManualSwipeRecords : frmN3000
	{
		public class ToolStripDateTime : ToolStripControlHost
		{
			private static DateTimePicker dtp;

			public DateTimePicker DateTimeControl
			{
				get
				{
					return base.Control as DateTimePicker;
				}
			}

			public int BoxWidth
			{
				get
				{
					return (base.Control as DateTimePicker).Size.Width;
				}
				set
				{
					base.Control.Size = new Size(new Point(value, base.Control.Size.Height));
					(base.Control as DateTimePicker).Size = new Size(new Point(value, base.Control.Size.Height));
				}
			}

			public DateTime Value
			{
				get
				{
					return (base.Control as DateTimePicker).Value;
				}
				set
				{
					DateTime dateTime;
					if (DateTime.TryParse(value.ToString(), out dateTime) && dateTime >= (base.Control as DateTimePicker).MinDate && dateTime <= (base.Control as DateTimePicker).MaxDate)
					{
						(base.Control as DateTimePicker).Value = dateTime;
					}
				}
			}

			public ToolStripDateTime() : base(frmManualSwipeRecords.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmManualSwipeRecords.ToolStripDateTime.dtp != null)
				{
					frmManualSwipeRecords.ToolStripDateTime.dtp.Dispose();
				}
				base.Dispose(disposing);
			}

			public void SetTimeFormat()
			{
				DateTimePicker dateTimePicker = base.Control as DateTimePicker;
				dateTimePicker.CustomFormat = "HH;mm";
				dateTimePicker.Format = DateTimePickerFormat.Custom;
				dateTimePicker.ShowUpDown = true;
			}
		}

		private frmManualSwipeRecords.ToolStripDateTime dtpDateFrom;

		private frmManualSwipeRecords.ToolStripDateTime dtpDateTo;

		private int recIdMax;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private IContainer components;

		private ToolStrip toolStrip1;

		private DataGridView dgvMain;

		private ToolStripButton btnPrint;

		private BackgroundWorker backgroundWorker1;

		private ToolStripButton btnExportToExcel;

		private UserControlFind userControlFind1;

		private ToolStrip toolStrip3;

		private ToolStripLabel toolStripLabel2;

		private ToolStripLabel toolStripLabel3;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private DataGridViewTextBoxColumn f_ManualCardRecordID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_ReadDate;

		private DataGridViewTextBoxColumn f_Notes;

		public frmManualSwipeRecords()
		{
			this.InitializeComponent();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuManualCardRecord";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnDelete.Visible = false;
				this.btnEdit.Visible = false;
			}
		}

		private void frmSwipeRecords_Load(object sender, EventArgs e)
		{
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.loadOperatorPrivilege();
			this.dtpDateFrom = new frmManualSwipeRecords.ToolStripDateTime();
			this.dtpDateTo = new frmManualSwipeRecords.ToolStripDateTime();
			this.toolStrip3.Items.Clear();
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.dtpDateFrom,
				this.toolStripLabel3,
				this.dtpDateTo
			});
			this.dtpDateFrom.BoxWidth = 120;
			this.dtpDateTo.BoxWidth = 120;
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.userControlFind1.toolStripLabel2.Visible = false;
			this.userControlFind1.txtFindCardID.Visible = false;
			this.dtpDateFrom.Enabled = true;
			this.dtpDateTo.Enabled = true;
			this.loadStyle();
			Cursor.Current = Cursors.WaitCursor;
			this.dtpDateTo.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-12-31"));
			this.dtpDateFrom.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01"));
			this.dtpDateFrom.BoxWidth = 150;
			this.dtpDateTo.BoxWidth = 150;
			wgAppConfig.setDisplayFormatDate(this.dtpDateFrom.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpDateTo.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			this.Refresh();
			this.userControlFind1.btnQuery.PerformClick();
		}

		private void loadStyle()
		{
			this.dgvMain.AutoGenerateColumns = false;
			wgAppConfig.ReadGVStyle(this, this.dgvMain);
		}

		private DataTable loadDataRecords(int startIndex, int maxRecords, string strSql)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine(this.Text + " loadDataRecords Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recIdMax = -2147483648;
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND f_ManualCardRecordID > {0:d}", this.recIdMax);
			}
			else
			{
				strSql += string.Format(" WHERE f_ManualCardRecordID > {0:d}", this.recIdMax);
			}
			strSql += " ORDER BY f_ManualCardRecordID ";
			this.table = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.table);
						}
					}
					goto IL_186;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.table);
					}
				}
			}
			IL_186:
			if (this.table.Rows.Count > 0)
			{
				this.recIdMax = int.Parse(this.table.Rows[this.table.Rows.Count - 1][0].ToString());
			}
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			wgTools.WriteLine(this.Text + "  loadRecords End");
			return this.table;
		}

		private string getSqlOfDateTime(string colNameOfDate)
		{
			string text = string.Concat(new string[]
			{
				"  (",
				colNameOfDate,
				" >= ",
				wgTools.PrepareStr(this.dtpDateFrom.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 00:00:00"),
				")"
			});
			if (text != "")
			{
				text += " AND ";
			}
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				"  (",
				colNameOfDate,
				" <= ",
				wgTools.PrepareStr(this.dtpDateTo.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59"),
				")"
			});
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnQuery_Click_Acc(sender, e);
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_ManualCardRecord.f_ManualCardRecordID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, f_ReadDate,";
			text += " t_d_ManualCardRecord.f_Note, ";
			text += " t_b_Consumer.f_ConsumerID  ";
			string sqlFindNormal = wgAppConfig.getSqlFindNormal(text, "t_d_ManualCardRecord", this.getSqlOfDateTime("t_d_ManualCardRecord.f_ReadDate"), groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindNormal);
		}

		private void btnQuery_Click_Acc(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_ManualCardRecord.f_ManualCardRecordID, t_b_Group.f_GroupName, ";
			text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
			text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, f_ReadDate,";
			text += " t_d_ManualCardRecord.f_Note, ";
			text += " t_b_Consumer.f_ConsumerID  ";
			string sqlFindNormal = wgAppConfig.getSqlFindNormal(text, "t_d_ManualCardRecord", this.getSqlOfDateTime("t_d_ManualCardRecord.f_ReadDate"), groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindNormal);
		}

		private void reloadData(string strsql)
		{
			if (this.backgroundWorker1.IsBusy)
			{
				return;
			}
			this.bLoadedFinished = false;
			this.startRecordIndex = 0;
			this.MaxRecord = 1000;
			if (!string.IsNullOrEmpty(strsql))
			{
				this.dgvSql = strsql;
			}
			this.dgvMain.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void frmShiftAttReport_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvMain, this.Text);
		}

		private void fillDgv(DataTable dt)
		{
			try
			{
				if (this.dgvMain.DataSource == null)
				{
					this.dgvMain.DataSource = dt;
					for (int i = 0; i < this.dgvMain.ColumnCount; i++)
					{
						this.dgvMain.Columns[i].DataPropertyName = dt.Columns[i].ColumnName;
						this.dgvMain.Columns[i].Name = dt.Columns[i].ColumnName;
					}
					wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_ReadDate", wgTools.DisplayFormat_DateYMDHMSWeek);
					wgAppConfig.ReadGVStyle(this, this.dgvMain);
					if (this.startRecordIndex == 0 && dt.Rows.Count >= this.MaxRecord)
					{
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
					}
				}
				else if (dt.Rows.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = this.dgvMain.FirstDisplayedScrollingRowIndex;
					DataTable dataTable = this.dgvMain.DataSource as DataTable;
					dataTable.Merge(dt);
					if (firstDisplayedScrollingRowIndex >= 0)
					{
						this.dgvMain.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			Cursor.Current = Cursors.Default;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			int startIndex = (int)((object[])e.Argument)[0];
			int maxRecords = (int)((object[])e.Argument)[1];
			string strSql = (string)((object[])e.Argument)[2];
			e.Result = this.loadDataRecords(startIndex, maxRecords, strSql);
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				XMessageBox.Show(CommonStr.strOperationCanceled);
				return;
			}
			if (e.Error != null)
			{
				string text = string.Format("An error occurred: {0}", e.Error.Message);
				XMessageBox.Show(text);
				return;
			}
			if ((e.Result as DataTable).Rows.Count < this.MaxRecord)
			{
				this.bLoadedFinished = true;
			}
			this.fillDgv(e.Result as DataTable);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvMain_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > this.dgvMain.Rows.Count || e.NewValue + this.dgvMain.Rows.Count / 10 > this.dgvMain.Rows.Count))
				{
					if (this.startRecordIndex <= this.dgvMain.Rows.Count)
					{
						if (this.backgroundWorker1.IsBusy)
						{
							return;
						}
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
						return;
					}
					else
					{
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex >= 5 && e.ColumnIndex < this.dgvMain.Columns.Count)
			{
				object arg_2D_0 = e.Value;
				DataGridViewCell dataGridViewCell = this.dgvMain[e.ColumnIndex, e.RowIndex];
				string text = this.dgvMain[e.ColumnIndex, e.RowIndex].Value.ToString();
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				if (text == "0")
				{
					text = "*";
					e.Value = text;
					dataGridViewCell.Value = e.Value;
					return;
				}
				if (text == "-1")
				{
					e.Value = "-";
					dataGridViewCell.Value = e.Value;
					return;
				}
				if (text == "-2")
				{
					e.Value = DBNull.Value;
					dataGridViewCell.Value = e.Value;
				}
			}
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcelSpecial(ref this.dgvMain, this.Text, this.bLoadedFinished, ref this.backgroundWorker1, ref this.startRecordIndex, this.MaxRecord, this.dgvSql);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.dgvMain.RowCount <= 0)
			{
				return;
			}
			if (this.dgvMain.SelectedRows.Count <= 1)
			{
				int index = this.dgvMain.SelectedRows[0].Index;
				if (XMessageBox.Show(this, CommonStr.strDelete + " " + this.dgvMain[0, index].Value.ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
				{
					return;
				}
			}
			else if (XMessageBox.Show(this, CommonStr.strDeleteSelected + " " + this.dgvMain.SelectedRows.Count.ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
			{
				return;
			}
			int arg_BF_0 = this.dgvMain.FirstDisplayedScrollingRowIndex;
			if (this.dgvMain.SelectedRows.Count <= 1)
			{
				int index = this.dgvMain.SelectedRows[0].Index;
				string strSql = " DELETE FROM t_d_ManualCardRecord WHERE [f_ManualCardRecordID]= " + this.dgvMain[0, index].Value.ToString();
				wgAppConfig.runUpdateSql(strSql);
			}
			else
			{
				foreach (DataGridViewRow dataGridViewRow in this.dgvMain.SelectedRows)
				{
					string strSql = " DELETE FROM t_d_ManualCardRecord WHERE [f_ManualCardRecordID]= " + dataGridViewRow.Cells[0].Value.ToString();
					wgAppConfig.runUpdateSql(strSql);
				}
			}
			this.btnQuery_Click(sender, null);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmManualSwipeRecordsAdd dfrmManualSwipeRecordsAdd = new dfrmManualSwipeRecordsAdd())
			{
				dfrmManualSwipeRecordsAdd.ShowDialog();
				this.btnQuery_Click(sender, null);
			}
		}

		private void btnTypeSetup_Click(object sender, EventArgs e)
		{
			using (dfrmHolidayType dfrmHolidayType = new dfrmHolidayType())
			{
				dfrmHolidayType.ShowDialog(this);
				this.btnQuery_Click(sender, null);
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmManualSwipeRecords));
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.dgvMain = new DataGridView();
			this.userControlFind1 = new UserControlFind();
			this.toolStrip3 = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.toolStripLabel3 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.f_ManualCardRecordID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_ReadDate = new DataGridViewTextBoxColumn();
			this.f_Notes = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ManualCardRecordID,
				this.f_DepartmentName,
				this.f_ConsumerNO,
				this.f_ConsumerName,
				this.f_ReadDate,
				this.f_Notes
			});
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowHeadersVisible = false;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvMain.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvMain_CellFormatting);
			this.dgvMain.Scroll += new ScrollEventHandler(this.dgvMain_Scroll);
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.Name = "userControlFind1";
			this.toolStrip3.BackColor = Color.Transparent;
			this.toolStrip3.BackgroundImage = Resources.pTools_second_title;
			componentResourceManager.ApplyResources(this.toolStrip3, "toolStrip3");
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.toolStripLabel3
			});
			this.toolStrip3.Name = "toolStrip3";
			this.toolStripLabel2.ForeColor = Color.White;
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.toolStripLabel3.ForeColor = Color.White;
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
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
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ManualCardRecordID.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_ManualCardRecordID, "f_ManualCardRecordID");
			this.f_ManualCardRecordID.Name = "f_ManualCardRecordID";
			this.f_ManualCardRecordID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DepartmentName, "f_DepartmentName");
			this.f_DepartmentName.Name = "f_DepartmentName";
			this.f_DepartmentName.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_ConsumerNO, "f_ConsumerNO");
			this.f_ConsumerNO.Name = "f_ConsumerNO";
			this.f_ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReadDate, "f_ReadDate");
			this.f_ReadDate.Name = "f_ReadDate";
			this.f_ReadDate.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Notes, "f_Notes");
			this.f_Notes.Name = "f_Notes";
			this.f_Notes.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmManualSwipeRecords";
			base.FormClosing += new FormClosingEventHandler(this.frmShiftAttReport_FormClosing);
			base.Load += new EventHandler(this.frmSwipeRecords_Load);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
