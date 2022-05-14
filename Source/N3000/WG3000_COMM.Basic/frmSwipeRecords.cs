using System;
using System.Collections;
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
	public class frmSwipeRecords : frmN3000
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
					(base.Control as DateTimePicker).Value = value;
				}
			}

			public ToolStripDateTime() : base(frmSwipeRecords.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmSwipeRecords.ToolStripDateTime.dtp != null)
				{
					frmSwipeRecords.ToolStripDateTime.dtp.Dispose();
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

		private DataView dvFloor;

		private frmSwipeRecords.ToolStripDateTime dtpDateFrom;

		private frmSwipeRecords.ToolStripDateTime dtpDateTo;

		private frmSwipeRecords.ToolStripDateTime dtpTimeFrom;

		private frmSwipeRecords.ToolStripDateTime dtpTimeTo;

		private DataSet dsDefaultStyle = new DataSet("DGV_STILE");

		private int recIdMin;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private dfrmSwipeRecordsFindOption dfrmFindOption;

		public string strFindOption = "";

		private IContainer components;

		private ToolStrip toolStrip1;

		private DataGridView dgvSwipeRecords;

		private ToolStripButton btnPrint;

		private BackgroundWorker backgroundWorker1;

		private ToolStripButton btnExportToExcel;

		private UserControlFind userControlFind1;

		private ToolStrip toolStrip3;

		private ToolStripLabel toolStripLabel2;

		private ToolStripComboBox cboStart;

		private ToolStripLabel toolStripLabel3;

		private ToolStripComboBox cboEnd;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripLabel toolStripLabel4;

		private ToolStripLabel toolStripLabel5;

		private System.Windows.Forms.Timer timer1;

		private ToolStripButton btnFindOption;

		private ToolStripButton btnDelete;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_CardNO;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ReadDate;

		private DataGridViewTextBoxColumn f_Addr;

		private DataGridViewCheckBoxColumn f_Pass;

		private DataGridViewTextBoxColumn f_Desc;

		private DataGridViewTextBoxColumn f_RecordAll;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem saveLayoutToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultLayoutToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem loadAllToolStripMenuItem;

		public frmSwipeRecords()
		{
			this.InitializeComponent();
		}

		private void loadFloorInfo()
		{
			string text = " SELECT a.f_floorID,  c.f_DoorName + '.' + a.f_floorName as f_floorFullName , 0 as f_Selected, b.f_ZoneID, 0 as f_TimeProfile, b.f_ControllerID, b.f_ControllerSN ";
			text += " FROM t_b_floor a, t_b_Controller b,t_b_Door c WHERE c.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID and a.f_DoorID = c.f_DoorID ";
			text += " ORDER BY  (  c.f_DoorName + '.' + a.f_floorName ) ";
			text = "  SELECT t_b_Reader.f_ReaderName, t_b_Floor.f_floorID, t_b_Door.f_DoorName + '.' + t_b_Floor.f_floorName as f_floorFullName,  ";
			text += "   t_b_Door.f_DoorName, ";
			text += "   t_b_Floor.f_floorNO, t_b_Controller.f_ZoneID, t_b_Floor.f_floorName, t_b_Door.f_ControllerID  ";
			text += "    FROM t_b_Floor , t_b_Door, t_b_Controller, t_b_Reader ";
			text += "   where t_b_Floor.f_DoorID = t_b_Door.f_DoorID and t_b_Door.f_ControllerID = t_b_Controller.f_ControllerNO and t_b_Reader.f_ControllerID = t_b_Floor.f_ControllerID ";
			DataTable dataTable = new DataTable();
			new DataView(dataTable);
			this.dvFloor = new DataView(dataTable);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(dataTable);
						}
					}
					goto IL_110;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(dataTable);
					}
				}
			}
			try
			{
				IL_110:
				dataTable.PrimaryKey = new DataColumn[]
				{
					dataTable.Columns[0]
				};
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void frmSwipeRecords_Load(object sender, EventArgs e)
		{
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.saveDefaultStyle();
			this.loadStyle();
			this.loadFloorInfo();
			this.dtpDateFrom = new frmSwipeRecords.ToolStripDateTime();
			this.dtpDateTo = new frmSwipeRecords.ToolStripDateTime();
			this.dtpTimeFrom = new frmSwipeRecords.ToolStripDateTime();
			this.dtpTimeFrom.SetTimeFormat();
			this.dtpTimeFrom.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
			this.dtpTimeTo = new frmSwipeRecords.ToolStripDateTime();
			this.dtpTimeTo.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
			this.dtpTimeTo.SetTimeFormat();
			this.toolStrip3.Items.Clear();
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.cboStart,
				this.dtpDateFrom,
				this.toolStripLabel3,
				this.cboEnd,
				this.dtpDateTo,
				this.toolStripSeparator1,
				this.toolStripLabel4,
				this.dtpTimeFrom,
				this.toolStripLabel5,
				this.dtpTimeTo
			});
			this.dtpDateFrom.BoxWidth = 120;
			this.dtpDateTo.BoxWidth = 120;
			this.dtpTimeFrom.BoxWidth = 62;
			this.dtpTimeTo.BoxWidth = 62;
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			if (this.cboStart.Items.Count > 0)
			{
				this.cboStart.SelectedIndex = 0;
			}
			this.dtpDateFrom.Enabled = false;
			if (this.cboEnd.Items.Count > 0)
			{
				this.cboEnd.SelectedIndex = 0;
			}
			this.dtpDateTo.Enabled = false;
			this.dtpDateFrom.BoxWidth = 150;
			this.dtpDateTo.BoxWidth = 150;
			wgAppConfig.setDisplayFormatDate(this.dtpDateFrom.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpDateTo.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			if (!wgAppConfig.getParamValBoolByNO(143))
			{
				Cursor.Current = Cursors.WaitCursor;
				this.timer1.Enabled = true;
				this.userControlFind1.btnQuery.PerformClick();
			}
		}

		private void saveDefaultStyle()
		{
			DataTable dataTable = new DataTable();
			this.dsDefaultStyle.Tables.Add(dataTable);
			dataTable.TableName = this.dgvSwipeRecords.Name;
			dataTable.Columns.Add("colName");
			dataTable.Columns.Add("colHeader");
			dataTable.Columns.Add("colWidth");
			dataTable.Columns.Add("colVisable");
			dataTable.Columns.Add("colDisplayIndex");
			for (int i = 0; i < this.dgvSwipeRecords.ColumnCount; i++)
			{
				DataGridViewColumn dataGridViewColumn = this.dgvSwipeRecords.Columns[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow["colName"] = dataGridViewColumn.Name;
				dataRow["colHeader"] = dataGridViewColumn.HeaderText;
				dataRow["colWidth"] = dataGridViewColumn.Width;
				dataRow["colVisable"] = dataGridViewColumn.Visible;
				dataRow["colDisplayIndex"] = dataGridViewColumn.DisplayIndex;
				dataTable.Rows.Add(dataRow);
				dataTable.AcceptChanges();
			}
		}

		private void loadDefaultStyle()
		{
			DataTable dataTable = this.dsDefaultStyle.Tables[this.dgvSwipeRecords.Name];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.dgvSwipeRecords.Columns[i].Name = dataTable.Rows[i]["colName"].ToString();
				this.dgvSwipeRecords.Columns[i].HeaderText = dataTable.Rows[i]["colHeader"].ToString();
				this.dgvSwipeRecords.Columns[i].Width = int.Parse(dataTable.Rows[i]["colWidth"].ToString());
				this.dgvSwipeRecords.Columns[i].Visible = bool.Parse(dataTable.Rows[i]["colVisable"].ToString());
				this.dgvSwipeRecords.Columns[i].DisplayIndex = int.Parse(dataTable.Rows[i]["colDisplayIndex"].ToString());
			}
		}

		private void loadStyle()
		{
			this.dgvSwipeRecords.AutoGenerateColumns = false;
			wgAppConfig.ReadGVStyle(this, this.dgvSwipeRecords);
		}

		private DataTable loadSwipeRecords(int startIndex, int maxRecords, string strSql)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("loadSwipeRecords Start");
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
					goto IL_17B;
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
			IL_17B:
			if (this.table.Rows.Count > 0)
			{
				this.recIdMin = int.Parse(this.table.Rows[this.table.Rows.Count - 1][0].ToString());
			}
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			return this.table;
		}

		private string getSqlOfDateTime()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getSqlOfDateTime_Acc();
			}
			string text = "";
			if (this.cboStart.SelectedIndex == 1)
			{
				text = "  ([f_ReadDate]>= " + wgTools.PrepareStr(this.dtpDateFrom.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 00:00:00") + ")";
			}
			if (this.cboEnd.SelectedIndex == 1)
			{
				if (text != "")
				{
					text += " AND ";
				}
				text = text + "  ([f_ReadDate]<= " + wgTools.PrepareStr(this.dtpDateTo.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59") + ")";
			}
			if (this.dtpTimeFrom.Value.ToString("HH:mm") != "00:00")
			{
				if (text != "")
				{
					text += " AND ";
				}
				if (this.dtpTimeFrom.Value.ToString("mm") == "00")
				{
					text = text + " DATEPART(hh, [f_ReadDate]) >= " + this.dtpTimeFrom.Value.ToString("HH");
				}
				else
				{
					text += " ( ";
					text = text + " DATEPART(hh, [f_ReadDate]) > " + this.dtpTimeFrom.Value.ToString("HH");
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						" OR (DATEPART(hh, [f_ReadDate]) = ",
						this.dtpTimeFrom.Value.ToString("HH"),
						" AND (DATEPART(mi, [f_ReadDate]) >= ",
						this.dtpTimeFrom.Value.ToString("mm"),
						"))"
					});
					text += " ) ";
				}
			}
			if (this.dtpTimeTo.Value.ToString("HH:mm") != "23:59")
			{
				if (text != "")
				{
					text += " AND ";
				}
				if (this.dtpTimeTo.Value.ToString("mm") == "59")
				{
					text = text + " DATEPART(hh, [f_ReadDate]) <= " + this.dtpTimeTo.Value.ToString("HH");
				}
				else
				{
					text += " ( ";
					text = text + " DATEPART(hh, [f_ReadDate]) < " + this.dtpTimeTo.Value.ToString("HH");
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						" OR (DATEPART(hh, [f_ReadDate]) = ",
						this.dtpTimeTo.Value.ToString("HH"),
						" AND (DATEPART(mi, [f_ReadDate]) <= ",
						this.dtpTimeTo.Value.ToString("mm"),
						"))"
					});
					text += " ) ";
				}
			}
			return text;
		}

		private string getSqlOfDateTime_Acc()
		{
			string text = "";
			if (this.cboStart.SelectedIndex == 1)
			{
				text = "  ([f_ReadDate]>= " + wgTools.PrepareStr(this.dtpDateFrom.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 00:00:00") + ")";
			}
			if (this.cboEnd.SelectedIndex == 1)
			{
				if (text != "")
				{
					text += " AND ";
				}
				text = text + "  ([f_ReadDate]<= " + wgTools.PrepareStr(this.dtpDateTo.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59") + ")";
			}
			if (this.dtpTimeFrom.Value.ToString("HH:mm") != "00:00")
			{
				if (text != "")
				{
					text += " AND ";
				}
				if (this.dtpTimeFrom.Value.ToString("mm") == "00")
				{
					text = text + " Hour([f_ReadDate]) >= " + this.dtpTimeFrom.Value.ToString("HH");
				}
				else
				{
					text += " ( ";
					text = text + " HOUR( [f_ReadDate]) > " + this.dtpTimeFrom.Value.ToString("HH");
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						" OR (HOUR([f_ReadDate]) = ",
						this.dtpTimeFrom.Value.ToString("HH"),
						" AND (Minute( [f_ReadDate]) >= ",
						this.dtpTimeFrom.Value.ToString("mm"),
						"))"
					});
					text += " ) ";
				}
			}
			if (this.dtpTimeTo.Value.ToString("HH:mm") != "23:59")
			{
				if (text != "")
				{
					text += " AND ";
				}
				if (this.dtpTimeTo.Value.ToString("mm") == "59")
				{
					text = text + " HOUR( [f_ReadDate]) <= " + this.dtpTimeTo.Value.ToString("HH");
				}
				else
				{
					text += " ( ";
					text = text + " HOUR([f_ReadDate]) < " + this.dtpTimeTo.Value.ToString("HH");
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						" OR (Hour( [f_ReadDate]) = ",
						this.dtpTimeTo.Value.ToString("HH"),
						" AND (Minute( [f_ReadDate]) <= ",
						this.dtpTimeTo.Value.ToString("mm"),
						"))"
					});
					text += " ) ";
				}
			}
			return text;
		}

		public void btnQuery_Click(object sender, EventArgs e)
		{
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			string arg = "";
			bool flag = false;
			if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
			{
				flag = true;
				arg = " (t_d_SwipeRecord.f_ReaderID IN ( " + this.dfrmFindOption.getStrSql() + " )) ";
			}
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
			text += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
			text += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, ";
			text += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
			string text2 = " ( 1>0 ) ";
			if (this.getSqlOfDateTime() != "")
			{
				text2 += string.Format(" AND {0} ", this.getSqlOfDateTime());
			}
			if (flag)
			{
				text2 += string.Format(" AND {0} ", arg);
			}
			string sqlFindSwipeRecord = wgAppConfig.getSqlFindSwipeRecord(text, "t_d_SwipeRecord", text2, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindSwipeRecord);
		}

		public void btnQuery_Click_Acc(object sender, EventArgs e)
		{
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			string arg = "";
			bool flag = false;
			if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
			{
				flag = true;
				arg = " (t_d_SwipeRecord.f_ReaderID IN ( " + this.dfrmFindOption.getStrSql() + " )) ";
			}
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
			text += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
			text += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, ";
			text += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
			string text2 = " ( 1>0 ) ";
			if (this.getSqlOfDateTime() != "")
			{
				text2 += string.Format(" AND {0} ", this.getSqlOfDateTime());
			}
			if (flag)
			{
				text2 += string.Format(" AND {0} ", arg);
			}
			string sqlFindSwipeRecord = wgAppConfig.getSqlFindSwipeRecord(text, "t_d_SwipeRecord", text2, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindSwipeRecord);
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
			this.dgvSwipeRecords.DataSource = null;
			this.timer1.Enabled = true;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void frmSwipeRecords_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFindOption != null)
			{
				this.dfrmFindOption.Close();
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvSwipeRecords, this.Text);
		}

		private void fillDgv(DataTable dt)
		{
			try
			{
				if (this.dgvSwipeRecords.DataSource == null)
				{
					this.dgvSwipeRecords.DataSource = dt;
					for (int i = 0; i < dt.Columns.Count; i++)
					{
						this.dgvSwipeRecords.Columns[i].DataPropertyName = dt.Columns[i].ColumnName;
						this.dgvSwipeRecords.Columns[i].Name = dt.Columns[i].ColumnName;
					}
					wgAppConfig.setDisplayFormatDate(this.dgvSwipeRecords, "f_ReadDate", wgTools.DisplayFormat_DateYMDHMSWeek);
					wgAppConfig.ReadGVStyle(this, this.dgvSwipeRecords);
					if (this.startRecordIndex == 0 && dt.Rows.Count >= this.MaxRecord)
					{
						this.startRecordIndex += this.MaxRecord;
						wgTools.WgDebugWrite("First 1000", new object[0]);
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
					int firstDisplayedScrollingRowIndex = this.dgvSwipeRecords.FirstDisplayedScrollingRowIndex;
					DataTable dataTable = this.dgvSwipeRecords.DataSource as DataTable;
					dataTable.Merge(dt);
					if (firstDisplayedScrollingRowIndex > 0)
					{
						this.dgvSwipeRecords.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
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
			e.Result = this.loadSwipeRecords(startIndex, maxRecords, strSql);
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
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvSwipeRecords.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvSwipeRecords_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > this.dgvSwipeRecords.Rows.Count || e.NewValue + this.dgvSwipeRecords.Rows.Count / 10 > this.dgvSwipeRecords.Rows.Count))
				{
					if (this.startRecordIndex <= this.dgvSwipeRecords.Rows.Count)
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
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvSwipeRecords.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void dgvSwipeRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < this.dgvSwipeRecords.Columns.Count && this.dgvSwipeRecords.Columns[e.ColumnIndex].Name.Equals("f_Desc"))
			{
				string text = e.Value as string;
				if (text != null && text != " ")
				{
					return;
				}
				DataGridViewCell dataGridViewCell = this.dgvSwipeRecords[e.ColumnIndex, e.RowIndex];
				string text2 = this.dgvSwipeRecords[e.ColumnIndex + 1, e.RowIndex].Value as string;
				if (string.IsNullOrEmpty(text2))
				{
					e.Value = "";
					dataGridViewCell.Value = "";
					return;
				}
				MjRec mjRec = new MjRec(text2.PadLeft(48, '0'));
				e.Value = mjRec.GetDetailedRecord(null, 0u);
				if (mjRec.floorNo > 0)
				{
					this.dvFloor.RowFilter = string.Format("f_ReaderName = '{0}' AND f_floorNO = {1} ", this.dgvSwipeRecords["f_ReaderName", e.RowIndex].Value, mjRec.floorNo);
					if (this.dvFloor.Count >= 1)
					{
						object value = e.Value;
						e.Value = string.Concat(new object[]
						{
							value,
							" [",
							this.dvFloor[0]["f_floorFullName"].ToString(),
							"]"
						});
					}
				}
				dataGridViewCell.Value = e.Value;
			}
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewColumn dataGridViewColumn in this.dgvSwipeRecords.Columns)
			{
				if (dataGridViewColumn.Name.Equals("f_Desc"))
				{
					foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)this.dgvSwipeRecords.Rows))
					{
						DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[dataGridViewColumn.Index];
						if (dataGridViewCell.Value != null && dataGridViewCell.Value as string == " ")
						{
							string text = dataGridViewRow.Cells[dataGridViewColumn.Index + 1].Value as string;
							MjRec mjRec = new MjRec(text.PadLeft(36, '0'));
							dataGridViewCell.Value = mjRec.GetDetailedRecord(null, 0u);
							if (mjRec.floorNo > 0)
							{
								this.dvFloor.RowFilter = string.Format("f_ReaderName = '{0}' AND f_floorNO = {1} ", dataGridViewRow.Cells["f_ReaderName"].Value, mjRec.floorNo);
								if (this.dvFloor.Count >= 1)
								{
									DataGridViewCell expr_124 = dataGridViewCell;
									object value = expr_124.Value;
									expr_124.Value = string.Concat(new object[]
									{
										value,
										" [",
										this.dvFloor[0]["f_floorFullName"].ToString(),
										"]"
									});
								}
							}
						}
					}
				}
			}
			wgAppConfig.exportToExcel(this.dgvSwipeRecords, this.Text);
		}

		private void cboStart_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cboStart.SelectedIndex == 1)
			{
				this.dtpDateFrom.Enabled = true;
				return;
			}
			this.dtpDateFrom.Enabled = false;
		}

		private void cboEnd_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cboEnd.SelectedIndex == 1)
			{
				this.dtpDateTo.Enabled = true;
				return;
			}
			this.dtpDateTo.Enabled = false;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.dgvSwipeRecords.DataSource == null)
			{
				Cursor.Current = Cursors.WaitCursor;
				return;
			}
			Cursor.Current = Cursors.Default;
			this.timer1.Enabled = false;
		}

		private void btnFindOption_Click(object sender, EventArgs e)
		{
			if (this.dfrmFindOption == null)
			{
				this.dfrmFindOption = new dfrmSwipeRecordsFindOption();
				this.dfrmFindOption.Owner = this;
			}
			this.dfrmFindOption.Show();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = (sender as ToolStripButton).Text;
				dfrmInputNewName.label1.Text = CommonStr.strSelectMaxRecID;
				if (dfrmInputNewName.ShowDialog(this) == DialogResult.OK)
				{
					int num;
					if (int.TryParse(dfrmInputNewName.strNewName, out num))
					{
						string text = "DELETE FROM t_d_SwipeRecord  WHERE f_RecID < " + num.ToString();
						int num2 = wgAppConfig.runUpdateSql(text);
						wgAppConfig.wgLog("strsql =" + text);
						wgAppConfig.wgLog("Deleted Records' Count =" + num2.ToString());
						XMessageBox.Show(CommonStr.strDeletedSwipeRecordCount + num2.ToString());
					}
					else
					{
						XMessageBox.Show(CommonStr.strNumericWrong);
					}
				}
			}
		}

		private void frmSwipeRecords_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.SaveDGVStyle(this, this.dgvSwipeRecords);
			XMessageBox.Show(sender.ToString() + " " + CommonStr.strSuccessfully);
		}

		private void restoreDefaultLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.RestoreGVStyle(this, this.dgvSwipeRecords);
			this.loadDefaultStyle();
			this.loadStyle();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			string text = "";
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = sender.ToString();
				dfrmInputNewName.label1.Text = CommonStr.strCardID;
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				text = dfrmInputNewName.strNewName;
			}
			if (!string.IsNullOrEmpty(text))
			{
				int groupMinNO = 0;
				int groupIDOfMinNO = 0;
				int groupMaxNO = 0;
				string findName = "";
				long findCard = 0L;
				int findConsumerID = 0;
				string arg = "";
				bool flag = false;
				this.userControlFind1.txtFindCardID.Text = "";
				this.userControlFind1.txtFindName.Text = "";
				if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
				{
					flag = true;
					arg = " (t_d_SwipeRecord.f_ReaderID IN ( " + this.dfrmFindOption.getStrSql() + " )) ";
				}
				this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
				string text2 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text2 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text2 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, ";
				text2 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
				string text3 = " ( 1>0 ) ";
				if (this.getSqlOfDateTime() != "")
				{
					text3 += string.Format(" AND {0} ", this.getSqlOfDateTime());
				}
				if (flag)
				{
					text3 += string.Format(" AND {0} ", arg);
				}
				if (text.IndexOf("%") < 0)
				{
					text = string.Format("%{0}%", text);
				}
				if (wgAppConfig.IsAccessDB)
				{
					text3 += string.Format(" AND CSTR(t_d_SwipeRecord.f_CardNO) like {0} ", wgTools.PrepareStr(text));
				}
				else
				{
					text3 += string.Format(" AND t_d_SwipeRecord.f_CardNO like {0} ", wgTools.PrepareStr(text));
				}
				string sqlFindSwipeRecord = wgAppConfig.getSqlFindSwipeRecord(text2, "t_d_SwipeRecord", text3, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
				this.reloadData(sqlFindSwipeRecord);
				return;
			}
		}

		private void loadAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.bLoadedFinished)
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this.startRecordIndex <= this.dgvSwipeRecords.Rows.Count)
				{
					if (this.backgroundWorker1.IsBusy)
					{
						return;
					}
					this.startRecordIndex += this.MaxRecord;
					this.bLoadedFinished = true;
					this.backgroundWorker1.RunWorkerAsync(new object[]
					{
						this.startRecordIndex,
						100000000,
						this.dgvSql
					});
					return;
				}
				else
				{
					wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvSwipeRecords.Rows.Count.ToString() + "#");
				}
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmSwipeRecords));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.dgvSwipeRecords = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_CardNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ReadDate = new DataGridViewTextBoxColumn();
			this.f_Addr = new DataGridViewTextBoxColumn();
			this.f_Pass = new DataGridViewCheckBoxColumn();
			this.f_Desc = new DataGridViewTextBoxColumn();
			this.f_RecordAll = new DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.saveLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultLayoutToolStripMenuItem = new ToolStripMenuItem();
			this.loadAllToolStripMenuItem = new ToolStripMenuItem();
			this.toolStrip3 = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.cboStart = new ToolStripComboBox();
			this.toolStripLabel3 = new ToolStripLabel();
			this.cboEnd = new ToolStripComboBox();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripLabel4 = new ToolStripLabel();
			this.toolStripLabel5 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnFindOption = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.userControlFind1 = new UserControlFind();
			((ISupportInitialize)this.dgvSwipeRecords).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.dgvSwipeRecords, "dgvSwipeRecords");
			this.dgvSwipeRecords.AllowUserToAddRows = false;
			this.dgvSwipeRecords.AllowUserToDeleteRows = false;
			this.dgvSwipeRecords.AllowUserToOrderColumns = true;
			this.dgvSwipeRecords.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSwipeRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSwipeRecords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSwipeRecords.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_RecID,
				this.f_CardNO,
				this.f_ConsumerNO,
				this.f_ConsumerName,
				this.f_DepartmentName,
				this.f_ReadDate,
				this.f_Addr,
				this.f_Pass,
				this.f_Desc,
				this.f_RecordAll
			});
			this.dgvSwipeRecords.ContextMenuStrip = this.contextMenuStrip1;
			this.dgvSwipeRecords.EnableHeadersVisualStyles = false;
			this.dgvSwipeRecords.Name = "dgvSwipeRecords";
			this.dgvSwipeRecords.ReadOnly = true;
			this.dgvSwipeRecords.RowHeadersVisible = false;
			this.dgvSwipeRecords.RowTemplate.Height = 23;
			this.dgvSwipeRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSwipeRecords.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvSwipeRecords_CellFormatting);
			this.dgvSwipeRecords.Scroll += new ScrollEventHandler(this.dgvSwipeRecords_Scroll);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_RecID.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_CardNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_CardNO, "f_CardNO");
			this.f_CardNO.Name = "f_CardNO";
			this.f_CardNO.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_ConsumerNO, "f_ConsumerNO");
			this.f_ConsumerNO.Name = "f_ConsumerNO";
			this.f_ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DepartmentName, "f_DepartmentName");
			this.f_DepartmentName.Name = "f_DepartmentName";
			this.f_DepartmentName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReadDate, "f_ReadDate");
			this.f_ReadDate.Name = "f_ReadDate";
			this.f_ReadDate.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Addr, "f_Addr");
			this.f_Addr.Name = "f_Addr";
			this.f_Addr.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Pass, "f_Pass");
			this.f_Pass.Name = "f_Pass";
			this.f_Pass.ReadOnly = true;
			this.f_Pass.Resizable = DataGridViewTriState.True;
			this.f_Pass.SortMode = DataGridViewColumnSortMode.Automatic;
			this.f_Desc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Desc, "f_Desc");
			this.f_Desc.Name = "f_Desc";
			this.f_Desc.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_RecordAll, "f_RecordAll");
			this.f_RecordAll.Name = "f_RecordAll";
			this.f_RecordAll.ReadOnly = true;
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem1,
				this.saveLayoutToolStripMenuItem,
				this.restoreDefaultLayoutToolStripMenuItem,
				this.loadAllToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
			componentResourceManager.ApplyResources(this.saveLayoutToolStripMenuItem, "saveLayoutToolStripMenuItem");
			this.saveLayoutToolStripMenuItem.Name = "saveLayoutToolStripMenuItem";
			this.saveLayoutToolStripMenuItem.Click += new EventHandler(this.saveLayoutToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.restoreDefaultLayoutToolStripMenuItem, "restoreDefaultLayoutToolStripMenuItem");
			this.restoreDefaultLayoutToolStripMenuItem.Name = "restoreDefaultLayoutToolStripMenuItem";
			this.restoreDefaultLayoutToolStripMenuItem.Click += new EventHandler(this.restoreDefaultLayoutToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.loadAllToolStripMenuItem, "loadAllToolStripMenuItem");
			this.loadAllToolStripMenuItem.Name = "loadAllToolStripMenuItem";
			this.loadAllToolStripMenuItem.Click += new EventHandler(this.loadAllToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.toolStrip3, "toolStrip3");
			this.toolStrip3.BackColor = Color.Transparent;
			this.toolStrip3.BackgroundImage = Resources.pTools_second_title;
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.cboStart,
				this.toolStripLabel3,
				this.cboEnd,
				this.toolStripSeparator1,
				this.toolStripLabel4,
				this.toolStripLabel5
			});
			this.toolStrip3.Name = "toolStrip3";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.toolStripLabel2.ForeColor = Color.FromArgb(233, 241, 255);
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.cboStart, "cboStart");
			this.cboStart.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboStart.Items"),
				componentResourceManager.GetString("cboStart.Items1")
			});
			this.cboStart.Name = "cboStart";
			this.cboStart.SelectedIndexChanged += new EventHandler(this.cboStart_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			this.toolStripLabel3.ForeColor = Color.FromArgb(233, 241, 255);
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.cboEnd, "cboEnd");
			this.cboEnd.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboEnd.Items"),
				componentResourceManager.GetString("cboEnd.Items1")
			});
			this.cboEnd.Name = "cboEnd";
			this.cboEnd.SelectedIndexChanged += new EventHandler(this.cboEnd_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			componentResourceManager.ApplyResources(this.toolStripLabel4, "toolStripLabel4");
			this.toolStripLabel4.ForeColor = Color.FromArgb(233, 241, 255);
			this.toolStripLabel4.Name = "toolStripLabel4";
			componentResourceManager.ApplyResources(this.toolStripLabel5, "toolStripLabel5");
			this.toolStripLabel5.ForeColor = Color.FromArgb(233, 241, 255);
			this.toolStripLabel5.Name = "toolStripLabel5";
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pTools_first_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnPrint,
				this.btnExportToExcel,
				this.btnFindOption,
				this.btnDelete
			});
			this.toolStrip1.Name = "toolStrip1";
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.ForeColor = Color.FromArgb(233, 241, 255);
			this.btnPrint.Image = Resources.pTools_Print;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.ForeColor = Color.FromArgb(233, 241, 255);
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			componentResourceManager.ApplyResources(this.btnFindOption, "btnFindOption");
			this.btnFindOption.ForeColor = Color.FromArgb(233, 241, 255);
			this.btnFindOption.Image = Resources.pTools_QueryOption;
			this.btnFindOption.Name = "btnFindOption";
			this.btnFindOption.Click += new EventHandler(this.btnFindOption_Click);
			componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.ForeColor = Color.White;
			this.btnDelete.Image = Resources.pTools_Del;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.ForeColor = Color.White;
			this.userControlFind1.Name = "userControlFind1";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvSwipeRecords);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmSwipeRecords";
			base.FormClosing += new FormClosingEventHandler(this.frmSwipeRecords_FormClosing);
			base.Load += new EventHandler(this.frmSwipeRecords_Load);
			base.KeyDown += new KeyEventHandler(this.frmSwipeRecords_KeyDown);
			((ISupportInitialize)this.dgvSwipeRecords).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
