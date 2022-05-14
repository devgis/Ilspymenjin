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
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmLogQuery : frmN3000
	{
		private IContainer components;

		private DataGridView dgvMain;

		private Button btnClose;

		private BackgroundWorker backgroundWorker1;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_LogDateTime;

		private DataGridViewTextBoxColumn f_EventType;

		private DataGridViewTextBoxColumn f_EventDesc;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private bool bLoadedFinished;

		private int recIdMin;

		private DataTable dt;

		private dfrmFind dfrmFind1;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmLogQuery));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.btnClose = new Button();
			this.dgvMain = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_LogDateTime = new DataGridViewTextBoxColumn();
			this.f_EventType = new DataGridViewTextBoxColumn();
			this.f_EventDesc = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dgvMain).BeginInit();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.DialogResult = DialogResult.Cancel;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			this.dgvMain.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_RecID,
				this.f_LogDateTime,
				this.f_EventType,
				this.f_EventDesc
			});
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowHeadersVisible = false;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvMain.Scroll += new ScrollEventHandler(this.dgvMain_Scroll);
			this.dgvMain.DoubleClick += new EventHandler(this.dgvMain_DoubleClick);
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_LogDateTime, "f_LogDateTime");
			this.f_LogDateTime.Name = "f_LogDateTime";
			this.f_LogDateTime.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_EventType, "f_EventType");
			this.f_EventType.Name = "f_EventType";
			this.f_EventType.ReadOnly = true;
			this.f_EventDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_EventDesc, "f_EventDesc");
			this.f_EventDesc.Name = "f_EventDesc";
			this.f_EventDesc.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnClose;
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.dgvMain);
			base.Name = "dfrmLogQuery";
			base.FormClosing += new FormClosingEventHandler(this.dfrmLogQuery_FormClosing);
			base.Load += new EventHandler(this.dfrmLogQuery_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmLogQuery_KeyDown);
			((ISupportInitialize)this.dgvMain).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmLogQuery()
		{
			this.InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void dfrmLogQuery_Load(object sender, EventArgs e)
		{
			string strsql = " SELECT f_RecID,f_LogDateTime,  f_EventType, f_EventDesc From t_s_wgLog  ";
			this.dgvMain.AutoGenerateColumns = false;
			this.reloadData(strsql);
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
					wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_LogDateTime", wgTools.DisplayFormat_DateYMDHMSWeek);
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

		private DataTable loadDataRecords(int startIndex, int maxRecords, string strSql)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("load LogQuery Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recIdMin = 2147483647;
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND f_RecID < {0:d}", this.recIdMin);
			}
			else
			{
				strSql += string.Format(" WHERE f_RecID < {0:d}", this.recIdMin);
			}
			strSql += " ORDER BY f_RecID DESC ";
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							this.dt = new DataTable();
							wgTools.WriteLine("da.Fill start");
							oleDbDataAdapter.Fill(this.dt);
							if (this.dt.Rows.Count > 0)
							{
								this.recIdMin = int.Parse(this.dt.Rows[this.dt.Rows.Count - 1][0].ToString());
							}
						}
					}
					goto IL_236;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						this.dt = new DataTable();
						wgTools.WriteLine("da.Fill start");
						sqlDataAdapter.Fill(this.dt);
						if (this.dt.Rows.Count > 0)
						{
							this.recIdMin = int.Parse(this.dt.Rows[this.dt.Rows.Count - 1][0].ToString());
						}
					}
				}
			}
			IL_236:
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			wgTools.WriteLine(this.Text + "  load LogQuery End");
			Cursor.Current = Cursors.Default;
			return this.dt;
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

		private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dgvMain_Click(object sender, EventArgs e)
		{
		}

		private void dgvMain_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (this.dgvMain.SelectedRows.Count <= 0)
				{
					if (this.dgvMain.SelectedCells.Count <= 0)
					{
						return;
					}
					int arg_41_0 = this.dgvMain.SelectedCells[0].RowIndex;
				}
				else
				{
					int arg_5A_0 = this.dgvMain.SelectedRows[0].Index;
				}
				int index = 0;
				DataGridView dataGridView = this.dgvMain;
				if (dataGridView.Rows.Count > 0)
				{
					index = dataGridView.CurrentCell.RowIndex;
				}
				string text = dataGridView.Rows[index].Cells["f_EventDesc"].Value.ToString();
				Clipboard.SetDataObject(text, false);
				XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void dfrmLogQuery_KeyDown(object sender, KeyEventArgs e)
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
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void dfrmLogQuery_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
