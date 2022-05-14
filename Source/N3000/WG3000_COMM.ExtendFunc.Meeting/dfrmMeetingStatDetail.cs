using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class dfrmMeetingStatDetail : frmN3000
	{
		public string curMeetingNo = "";

		public TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		private TabPage tabPage5;

		private TabPage tabPage6;

		private GroupBox grpExt;

		private DataGridView dgvMain;

		private DataGridView dgvStat;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn InFact;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn f_ManualCardRecordID;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_Identity;

		private DataGridViewTextBoxColumn f_SeatNO;

		private DataGridViewTextBoxColumn f_SignRealTime;

		private DataGridViewTextBoxColumn f_Notes;

		public int selectedPage = -1;

		private Container components;

		internal Button btnLeave;

		internal Button btnManualSign;

		internal Button btnRecreate;

		internal Button btnPrint;

		internal Button btnExport;

		internal Button btnRefresh;

		internal Button btnExit;

		private DataSet ds = new DataSet();

		private long[,] arrMeetingNum = new long[7, 6];

		private DateTime signStarttime;

		private DateTime signEndtime;

		private string meetingAdr = "";

		private ArrayList arrControllerID = new ArrayList();

		private ArrayList arrSignedUser = new ArrayList();

		private ArrayList arrSignedSeat = new ArrayList();

		private ArrayList arrSignedCardNo = new ArrayList();

		private DataView dvShould;

		private DataView dvInFact;

		private DataView dvLeave;

		private DataView dvAbsent;

		private DataView dvLate;

		private long lngDealtRecordID = -1L;

		private string queryReaderStr = "";

		private string meetingName = "";

		private dfrmFind dfrmFind1 = new dfrmFind();

		public dfrmMeetingStatDetail()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMeetingStatDetail));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.tabPage2 = new TabPage();
			this.tabPage3 = new TabPage();
			this.tabPage4 = new TabPage();
			this.tabPage5 = new TabPage();
			this.tabPage6 = new TabPage();
			this.btnLeave = new Button();
			this.btnExit = new Button();
			this.btnManualSign = new Button();
			this.btnRecreate = new Button();
			this.btnPrint = new Button();
			this.btnExport = new Button();
			this.btnRefresh = new Button();
			this.grpExt = new GroupBox();
			this.dgvStat = new DataGridView();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.InFact = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.dgvMain = new DataGridView();
			this.f_ManualCardRecordID = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_Identity = new DataGridViewTextBoxColumn();
			this.f_SeatNO = new DataGridViewTextBoxColumn();
			this.f_SignRealTime = new DataGridViewTextBoxColumn();
			this.f_Notes = new DataGridViewTextBoxColumn();
			this.tabControl1.SuspendLayout();
			this.grpExt.SuspendLayout();
			((ISupportInitialize)this.dgvStat).BeginInit();
			((ISupportInitialize)this.dgvMain).BeginInit();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPage1.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage4, "tabPage4");
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage5, "tabPage5");
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage6, "tabPage6");
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.UseVisualStyleBackColor = true;
			this.btnLeave.BackColor = Color.Transparent;
			this.btnLeave.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnLeave, "btnLeave");
			this.btnLeave.ForeColor = Color.White;
			this.btnLeave.Name = "btnLeave";
			this.btnLeave.UseVisualStyleBackColor = false;
			this.btnLeave.Click += new EventHandler(this.btnLeave_Click);
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnOk_Click);
			this.btnManualSign.BackColor = Color.Transparent;
			this.btnManualSign.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnManualSign, "btnManualSign");
			this.btnManualSign.ForeColor = Color.White;
			this.btnManualSign.Name = "btnManualSign";
			this.btnManualSign.UseVisualStyleBackColor = false;
			this.btnManualSign.Click += new EventHandler(this.btnManualSign_Click);
			this.btnRecreate.BackColor = Color.Transparent;
			this.btnRecreate.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnRecreate, "btnRecreate");
			this.btnRecreate.ForeColor = Color.White;
			this.btnRecreate.Name = "btnRecreate";
			this.btnRecreate.UseVisualStyleBackColor = false;
			this.btnRecreate.Click += new EventHandler(this.btnRecreate_Click);
			this.btnPrint.BackColor = Color.Transparent;
			this.btnPrint.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.UseVisualStyleBackColor = false;
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnExport.BackColor = Color.Transparent;
			this.btnExport.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnExport, "btnExport");
			this.btnExport.ForeColor = Color.White;
			this.btnExport.Name = "btnExport";
			this.btnExport.UseVisualStyleBackColor = false;
			this.btnExport.Click += new EventHandler(this.btnExport_Click);
			this.btnRefresh.BackColor = Color.Transparent;
			this.btnRefresh.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnRefresh, "btnRefresh");
			this.btnRefresh.ForeColor = Color.White;
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
			componentResourceManager.ApplyResources(this.grpExt, "grpExt");
			this.grpExt.BackColor = Color.Transparent;
			this.grpExt.Controls.Add(this.dgvStat);
			this.grpExt.Controls.Add(this.dgvMain);
			this.grpExt.ForeColor = Color.White;
			this.grpExt.Name = "grpExt";
			this.grpExt.TabStop = false;
			this.dgvStat.AllowUserToAddRows = false;
			this.dgvStat.AllowUserToDeleteRows = false;
			this.dgvStat.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvStat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvStat.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.InFact,
				this.Column1,
				this.Column2,
				this.Column3,
				this.Column4
			});
			this.dgvStat.EnableHeadersVisualStyles = false;
			componentResourceManager.ApplyResources(this.dgvStat, "dgvStat");
			this.dgvStat.Name = "dgvStat";
			this.dgvStat.ReadOnly = true;
			this.dgvStat.RowHeadersVisible = false;
			this.dgvStat.RowTemplate.Height = 23;
			this.dgvStat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.InFact, "InFact");
			this.InFact.Name = "InFact";
			this.InFact.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column1, "Column1");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column2, "Column2");
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column3, "Column3");
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column4, "Column4");
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			this.dgvMain.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ManualCardRecordID,
				this.f_ConsumerName,
				this.f_Identity,
				this.f_SeatNO,
				this.f_SignRealTime,
				this.f_Notes
			});
			this.dgvMain.EnableHeadersVisualStyles = false;
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowHeadersVisible = false;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ManualCardRecordID.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_ManualCardRecordID, "f_ManualCardRecordID");
			this.f_ManualCardRecordID.Name = "f_ManualCardRecordID";
			this.f_ManualCardRecordID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Identity, "f_Identity");
			this.f_Identity.Name = "f_Identity";
			this.f_Identity.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_SeatNO.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_SeatNO, "f_SeatNO");
			this.f_SeatNO.Name = "f_SeatNO";
			this.f_SeatNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SignRealTime, "f_SignRealTime");
			this.f_SignRealTime.Name = "f_SignRealTime";
			this.f_SignRealTime.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Notes, "f_Notes");
			this.f_Notes.Name = "f_Notes";
			this.f_Notes.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.btnLeave);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnManualSign);
			base.Controls.Add(this.btnRecreate);
			base.Controls.Add(this.btnPrint);
			base.Controls.Add(this.btnExport);
			base.Controls.Add(this.btnRefresh);
			base.Controls.Add(this.grpExt);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmMeetingStatDetail";
			base.FormClosing += new FormClosingEventHandler(this.dfrmMeetingStatDetail_FormClosing);
			base.Load += new EventHandler(this.dfrmStd_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmMeetingStatDetail_KeyDown);
			this.tabControl1.ResumeLayout(false);
			this.grpExt.ResumeLayout(false);
			((ISupportInitialize)this.dgvStat).EndInit();
			((ISupportInitialize)this.dgvMain).EndInit();
			base.ResumeLayout(false);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		public void fillMeetingRecord(string MeetingNo)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.fillMeetingRecord_Acc(MeetingNo);
				return;
			}
			try
			{
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(MeetingNo);
				SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					this.signStarttime = (DateTime)sqlDataReader["f_SignStartTime"];
					this.signEndtime = (DateTime)sqlDataReader["f_SignEndTime"];
					this.meetingAdr = wgTools.SetObjToStr(sqlDataReader["f_MeetingAdr"]);
				}
				sqlDataReader.Close();
				if (this.lngDealtRecordID == -1L && this.meetingAdr != "")
				{
					this.queryReaderStr = "";
					text = "Select t_b_reader.* from t_b_reader,t_d_MeetingAdr  ";
					text += " , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  t_b_Reader.f_ReaderID  = t_d_MeetingAdr.f_ReaderID ";
					text = text + " AND t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.meetingAdr);
					sqlCommand = new SqlCommand(text, sqlConnection);
					sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.HasRows)
					{
						while (sqlDataReader.Read())
						{
							if (this.queryReaderStr == "")
							{
								this.queryReaderStr = " f_ReaderID IN ( " + sqlDataReader["f_ReaderID"];
							}
							else
							{
								this.queryReaderStr = this.queryReaderStr + " , " + sqlDataReader["f_ReaderID"];
							}
							if (this.arrControllerID.IndexOf(sqlDataReader["f_ControllerID"]) < 0)
							{
								this.arrControllerID.Add(sqlDataReader["f_ControllerID"]);
							}
						}
						this.queryReaderStr += ")";
					}
					sqlDataReader.Close();
				}
				if (this.lngDealtRecordID == -1L)
				{
					this.lngDealtRecordID = 0L;
				}
				string text2 = "";
				text2 = text2 + " ([f_ReadDate]>= " + wgTools.PrepareStr(this.signStarttime, true, "yyyy-MM-dd H:mm:ss") + ")";
				text2 = text2 + " AND ([f_ReadDate]<= " + wgTools.PrepareStr(this.signEndtime, true, "yyyy-MM-dd H:mm:ss") + ")";
				string text3 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text3 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text3 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, ";
				text3 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll, t_d_MeetingConsumer.f_Seat, t_d_MeetingConsumer.f_MeetingIdentity, t_d_SwipeRecord.f_ConsumerID ";
				text3 += string.Format(" FROM ((t_b_Consumer INNER JOIN t_d_SwipeRecord ON ( t_b_Consumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID)) LEFT JOIN  t_d_MeetingConsumer on ( t_d_MeetingConsumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + ") ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", new object[0]);
				text3 = text3 + " WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID.ToString();
				text3 = text3 + " AND  t_d_SwipeRecord.f_ConsumerID IN (SELECT f_ConsumerID FROM t_d_MeetingConsumer WHERE f_SignWay=0 AND f_RecID =0 AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + " )  ";
				if (this.queryReaderStr != "")
				{
					text3 = text3 + " AND " + this.queryReaderStr;
				}
				text3 = text3 + " AND " + text2;
				text = text3;
				sqlCommand = new SqlCommand(text, sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				if (sqlDataReader.HasRows)
				{
					while (sqlDataReader.Read())
					{
						int num = arrayList.IndexOf(sqlDataReader["f_ConsumerID"]);
						if (num < 0)
						{
							arrayList.Add(sqlDataReader["f_ConsumerID"]);
							arrayList2.Add((DateTime)sqlDataReader["f_ReadDate"]);
							arrayList3.Add(sqlDataReader["f_RecID"]);
						}
						else if ((DateTime)arrayList2[num] > (DateTime)sqlDataReader["f_ReadDate"])
						{
							arrayList2[num] = (DateTime)sqlDataReader["f_ReadDate"];
							arrayList3[num] = sqlDataReader["f_RecID"];
						}
					}
				}
				sqlDataReader.Close();
				if (arrayList.Count > 0)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						text = " UPDATE t_d_MeetingConsumer ";
						text = text + " SET [f_SignRealTime] = " + wgTools.PrepareStr((DateTime)arrayList2[i], true, "yyyy-MM-dd H:mm:ss");
						text = text + " ,[f_RecID] = " + arrayList3[i];
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" WHERE f_ConsumerID = ",
							arrayList[i],
							" AND  f_MeetingNO = ",
							wgTools.PrepareStr(MeetingNo)
						});
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
				text = "SELECT f_RecID from t_d_SwipeRecord ORDER BY f_RecID DESC ";
				sqlCommand = new SqlCommand(text, sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.HasRows && sqlDataReader.Read())
				{
					this.lngDealtRecordID = long.Parse(sqlDataReader["f_RecID"].ToString());
				}
				sqlDataReader.Close();
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void fillMeetingRecord_Acc(string MeetingNo)
		{
			try
			{
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(MeetingNo);
				OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.Read())
				{
					this.signStarttime = (DateTime)oleDbDataReader["f_SignStartTime"];
					this.signEndtime = (DateTime)oleDbDataReader["f_SignEndTime"];
					this.meetingAdr = wgTools.SetObjToStr(oleDbDataReader["f_MeetingAdr"]);
				}
				oleDbDataReader.Close();
				if (this.lngDealtRecordID == -1L && this.meetingAdr != "")
				{
					this.queryReaderStr = "";
					text = "Select t_b_reader.* from t_b_reader,t_d_MeetingAdr  ";
					text += " , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND  t_b_Reader.f_ReaderID  = t_d_MeetingAdr.f_ReaderID ";
					text = text + " AND t_d_MeetingAdr.f_MeetingAdr = " + wgTools.PrepareStr(this.meetingAdr);
					oleDbCommand = new OleDbCommand(text, oleDbConnection);
					oleDbDataReader = oleDbCommand.ExecuteReader();
					if (oleDbDataReader.HasRows)
					{
						while (oleDbDataReader.Read())
						{
							if (this.queryReaderStr == "")
							{
								this.queryReaderStr = " f_ReaderID IN ( " + oleDbDataReader["f_ReaderID"];
							}
							else
							{
								this.queryReaderStr = this.queryReaderStr + " , " + oleDbDataReader["f_ReaderID"];
							}
							if (this.arrControllerID.IndexOf(oleDbDataReader["f_ControllerID"]) < 0)
							{
								this.arrControllerID.Add(oleDbDataReader["f_ControllerID"]);
							}
						}
						this.queryReaderStr += ")";
					}
					oleDbDataReader.Close();
				}
				if (this.lngDealtRecordID == -1L)
				{
					this.lngDealtRecordID = 0L;
				}
				string text2 = "";
				text2 = text2 + " ([f_ReadDate]>= " + wgTools.PrepareStr(this.signStarttime, true, "yyyy-MM-dd H:mm:ss") + ")";
				text2 = text2 + " AND ([f_ReadDate]<= " + wgTools.PrepareStr(this.signEndtime, true, "yyyy-MM-dd H:mm:ss") + ")";
				string text3 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text3 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text3 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, ";
				text3 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll, t_d_MeetingConsumer.f_Seat, t_d_MeetingConsumer.f_MeetingIdentity, t_d_SwipeRecord.f_ConsumerID ";
				text3 += string.Format(" FROM ((t_b_Consumer INNER JOIN t_d_SwipeRecord ON ( t_b_Consumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID)) LEFT JOIN  t_d_MeetingConsumer on ( t_d_MeetingConsumer.f_ConsumerID = t_d_SwipeRecord.f_ConsumerID AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + ") ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", new object[0]);
				text3 = text3 + " WHERE t_d_SwipeRecord.f_RecID > " + this.lngDealtRecordID.ToString();
				text3 = text3 + " AND  t_d_SwipeRecord.f_ConsumerID IN (SELECT f_ConsumerID FROM t_d_MeetingConsumer WHERE f_SignWay=0 AND f_RecID =0 AND  f_MeetingNO = " + wgTools.PrepareStr(MeetingNo) + " )  ";
				if (this.queryReaderStr != "")
				{
					text3 = text3 + " AND " + this.queryReaderStr;
				}
				text3 = text3 + " AND " + text2;
				text = text3;
				oleDbCommand = new OleDbCommand(text, oleDbConnection);
				oleDbDataReader = oleDbCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				if (oleDbDataReader.HasRows)
				{
					while (oleDbDataReader.Read())
					{
						int num = arrayList.IndexOf(oleDbDataReader["f_ConsumerID"]);
						if (num < 0)
						{
							arrayList.Add(oleDbDataReader["f_ConsumerID"]);
							arrayList2.Add((DateTime)oleDbDataReader["f_ReadDate"]);
							arrayList3.Add(oleDbDataReader["f_RecID"]);
						}
						else if ((DateTime)arrayList2[num] > (DateTime)oleDbDataReader["f_ReadDate"])
						{
							arrayList2[num] = (DateTime)oleDbDataReader["f_ReadDate"];
							arrayList3[num] = oleDbDataReader["f_RecID"];
						}
					}
				}
				oleDbDataReader.Close();
				if (arrayList.Count > 0)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						text = " UPDATE t_d_MeetingConsumer ";
						text = text + " SET [f_SignRealTime] = " + wgTools.PrepareStr((DateTime)arrayList2[i], true, "yyyy-MM-dd H:mm:ss");
						text = text + " ,[f_RecID] = " + arrayList3[i];
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" WHERE f_ConsumerID = ",
							arrayList[i],
							" AND  f_MeetingNO = ",
							wgTools.PrepareStr(MeetingNo)
						});
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
				text = "SELECT f_RecID from t_d_SwipeRecord ORDER BY f_RecID DESC ";
				oleDbCommand = new OleDbCommand(text, oleDbConnection);
				oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.HasRows && oleDbDataReader.Read())
				{
					this.lngDealtRecordID = long.Parse(oleDbDataReader["f_RecID"].ToString());
				}
				oleDbDataReader.Close();
				oleDbConnection.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void fillMeetingNum()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.fillMeetingNum_Acc();
				return;
			}
			try
			{
				SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
				if (sqlConnection.State == ConnectionState.Closed)
				{
					sqlConnection.Open();
				}
				string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				DateTime dateTime = DateTime.Now;
				if (sqlDataReader.Read())
				{
					dateTime = (DateTime)sqlDataReader["f_MeetingDateTime"];
					this.meetingName = sqlDataReader["f_MeetingName"].ToString();
				}
				sqlDataReader.Close();
				int i;
				for (i = 0; i <= 5; i++)
				{
					this.arrMeetingNum[0, i] = 0L;
					this.arrMeetingNum[1, i] = 0L;
					this.arrMeetingNum[2, i] = 0L;
					this.arrMeetingNum[3, i] = 0L;
					this.arrMeetingNum[4, i] = 0L;
					this.arrMeetingNum[5, i] = 0L;
					this.arrMeetingNum[6, i] = 0L;
				}
				text = "SELECT  a.f_RecID,b.f_ConsumerName, '' as f_MeetingIdentityStr, a.f_Seat, a.f_SignRealTime,'' as f_SignWayStr, a.f_SignWay, a.f_MeetingIdentity  FROM t_d_MeetingConsumer a, t_b_Consumer b WHERE a.f_ConsumerID=b.f_ConsumerID and a.f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(text, sqlConnection);
				this.ds.Clear();
				sqlDataAdapter.Fill(this.ds, "t_d_MeetingConsumer");
				DataView dataView = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				for (int j = 0; j <= dataView.Count - 1; j++)
				{
					this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_MeetingIdentityStr"] = frmMeetings.getStrMeetingIdentity(long.Parse(this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_MeetingIdentity"].ToString()));
					this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_SignWayStr"] = frmMeetings.getStrSignWay(long.Parse(this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_SignWay"].ToString()));
				}
				for (int j = 0; j <= 5; j++)
				{
					dataView.RowFilter = " f_MeetingIdentity = " + j;
					if (dataView.Count > 0)
					{
						this.arrMeetingNum[j, 0] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1)) ";
						this.arrMeetingNum[j, 1] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND (f_SignWay = 2) ";
						this.arrMeetingNum[j, 2] = (long)dataView.Count;
						this.arrMeetingNum[j, 3] = Math.Max(0L, this.arrMeetingNum[j, 0] - this.arrMeetingNum[j, 1] - this.arrMeetingNum[j, 2]);
						dataView.RowFilter = string.Concat(new object[]
						{
							" f_MeetingIdentity = ",
							j,
							" AND  ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1))  AND f_SignRealTime > ",
							Strings.Format(dateTime, "#yyyy-MM-dd HH:mm:ss#")
						});
						this.arrMeetingNum[j, 4] = (long)dataView.Count;
						if (this.arrMeetingNum[j, 0] > 0L)
						{
							this.arrMeetingNum[j, 5] = this.arrMeetingNum[j, 1] * 1000L / this.arrMeetingNum[j, 0];
						}
					}
					this.arrMeetingNum[6, 0] += this.arrMeetingNum[j, 0];
					this.arrMeetingNum[6, 1] += this.arrMeetingNum[j, 1];
					this.arrMeetingNum[6, 2] += this.arrMeetingNum[j, 2];
					this.arrMeetingNum[6, 3] += this.arrMeetingNum[j, 3];
					this.arrMeetingNum[6, 4] += this.arrMeetingNum[j, 4];
					this.arrMeetingNum[6, 5] += this.arrMeetingNum[j, 5];
				}
				if (this.arrMeetingNum[6, 0] > 0L)
				{
					this.arrMeetingNum[6, 5] = this.arrMeetingNum[6, 1] * 1000L / this.arrMeetingNum[6, 0];
				}
				this.dvShould = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvInFact = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvLeave = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvAbsent = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvLate = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvShould.RowFilter = "";
				this.dvInFact.RowFilter = "( f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1) ";
				this.dvLeave.RowFilter = " f_SignWay = 2 ";
				this.dvAbsent.RowFilter = " f_SignWay =0 AND f_RecID <=0  ";
				this.dvLate.RowFilter = " ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1)) AND f_SignRealTime > " + Strings.Format(dateTime, "#yyyy-MM-dd HH:mm:ss#");
				DataTable dataTable = new DataTable("Stat");
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingIdentity";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingShould";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingInFact";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingLeave";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingAbsent";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingLate";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingRatio";
				dataTable.Columns.Add(dataColumn);
				for (int j = 0; j <= 6; j++)
				{
					DataRow dataRow = dataTable.NewRow();
					if (j == 6)
					{
						dataRow[0] = CommonStr.strMeetingSubTotal;
					}
					else
					{
						dataRow[0] = frmMeetings.getStrMeetingIdentity((long)j);
					}
					for (i = 0; i <= 4; i++)
					{
						dataRow[i + 1] = this.arrMeetingNum[j, i];
						if (dataRow[i + 1].ToString() == "0")
						{
							dataRow[i + 1] = "";
						}
					}
					dataRow[6] = this.arrMeetingNum[j, 5] / 10L + "%";
					if (string.IsNullOrEmpty(dataRow[1].ToString()))
					{
						dataRow[6] = "";
					}
					dataTable.Rows.Add(dataRow);
				}
				DataTable dataTable2 = this.ds.Tables["t_d_MeetingConsumer"];
				this.dgvMain.AutoGenerateColumns = false;
				i = 0;
				while (i < dataTable2.Columns.Count && i < this.dgvMain.ColumnCount)
				{
					this.dgvMain.Columns[i].DataPropertyName = dataTable2.Columns[i].ColumnName;
					i++;
				}
				this.dgvMain.DataSource = this.dvShould;
				this.dgvMain.DefaultCellStyle.ForeColor = Color.Black;
				wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_SignRealTime", wgTools.DisplayFormat_DateYMDHMSWeek);
				this.dgvStat.AutoGenerateColumns = false;
				i = 0;
				while (i < dataTable.Columns.Count && i < this.dgvStat.ColumnCount)
				{
					this.dgvStat.Columns[i].DataPropertyName = dataTable.Columns[i].ColumnName;
					i++;
				}
				this.dgvStat.DataSource = dataTable;
				this.dgvStat.DefaultCellStyle.ForeColor = Color.Black;
				this.tabControl1.SelectedIndex = 5;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public void fillMeetingNum_Acc()
		{
			try
			{
				OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
				if (oleDbConnection.State == ConnectionState.Closed)
				{
					oleDbConnection.Open();
				}
				string text = "SELECT * FROM t_d_Meeting WHERE f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				DateTime dateTime = DateTime.Now;
				if (oleDbDataReader.Read())
				{
					dateTime = (DateTime)oleDbDataReader["f_MeetingDateTime"];
					this.meetingName = oleDbDataReader["f_MeetingName"].ToString();
				}
				oleDbDataReader.Close();
				int i;
				for (i = 0; i <= 5; i++)
				{
					this.arrMeetingNum[0, i] = 0L;
					this.arrMeetingNum[1, i] = 0L;
					this.arrMeetingNum[2, i] = 0L;
					this.arrMeetingNum[3, i] = 0L;
					this.arrMeetingNum[4, i] = 0L;
					this.arrMeetingNum[5, i] = 0L;
					this.arrMeetingNum[6, i] = 0L;
				}
				text = "SELECT  a.f_RecID,b.f_ConsumerName, '' as f_MeetingIdentityStr, a.f_Seat, a.f_SignRealTime,'' as f_SignWayStr, a.f_SignWay, a.f_MeetingIdentity  FROM t_d_MeetingConsumer a, t_b_Consumer b WHERE a.f_ConsumerID=b.f_ConsumerID and a.f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(text, oleDbConnection);
				this.ds.Clear();
				oleDbDataAdapter.Fill(this.ds, "t_d_MeetingConsumer");
				DataView dataView = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				for (int j = 0; j <= dataView.Count - 1; j++)
				{
					this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_MeetingIdentityStr"] = frmMeetings.getStrMeetingIdentity(long.Parse(this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_MeetingIdentity"].ToString()));
					this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_SignWayStr"] = frmMeetings.getStrSignWay(long.Parse(this.ds.Tables["t_d_MeetingConsumer"].Rows[j]["f_SignWay"].ToString()));
				}
				for (int j = 0; j <= 5; j++)
				{
					dataView.RowFilter = " f_MeetingIdentity = " + j;
					if (dataView.Count > 0)
					{
						this.arrMeetingNum[j, 0] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1)) ";
						this.arrMeetingNum[j, 1] = (long)dataView.Count;
						dataView.RowFilter = " f_MeetingIdentity = " + j + " AND (f_SignWay = 2) ";
						this.arrMeetingNum[j, 2] = (long)dataView.Count;
						this.arrMeetingNum[j, 3] = Math.Max(0L, this.arrMeetingNum[j, 0] - this.arrMeetingNum[j, 1] - this.arrMeetingNum[j, 2]);
						dataView.RowFilter = string.Concat(new object[]
						{
							" f_MeetingIdentity = ",
							j,
							" AND  ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1))  AND f_SignRealTime > ",
							Strings.Format(dateTime, "#yyyy-MM-dd HH:mm:ss#")
						});
						this.arrMeetingNum[j, 4] = (long)dataView.Count;
						if (this.arrMeetingNum[j, 0] > 0L)
						{
							this.arrMeetingNum[j, 5] = this.arrMeetingNum[j, 1] * 1000L / this.arrMeetingNum[j, 0];
						}
					}
					this.arrMeetingNum[6, 0] += this.arrMeetingNum[j, 0];
					this.arrMeetingNum[6, 1] += this.arrMeetingNum[j, 1];
					this.arrMeetingNum[6, 2] += this.arrMeetingNum[j, 2];
					this.arrMeetingNum[6, 3] += this.arrMeetingNum[j, 3];
					this.arrMeetingNum[6, 4] += this.arrMeetingNum[j, 4];
					this.arrMeetingNum[6, 5] += this.arrMeetingNum[j, 5];
				}
				if (this.arrMeetingNum[6, 0] > 0L)
				{
					this.arrMeetingNum[6, 5] = this.arrMeetingNum[6, 1] * 1000L / this.arrMeetingNum[6, 0];
				}
				this.dvShould = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvInFact = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvLeave = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvAbsent = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvLate = new DataView(this.ds.Tables["t_d_MeetingConsumer"]);
				this.dvShould.RowFilter = "";
				this.dvInFact.RowFilter = "( f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1) ";
				this.dvLeave.RowFilter = " f_SignWay = 2 ";
				this.dvAbsent.RowFilter = " f_SignWay =0 AND f_RecID <=0  ";
				this.dvLate.RowFilter = " ((f_SignWay =0 AND f_RecID >0 ) OR (f_SignWay = 1)) AND f_SignRealTime > " + Strings.Format(dateTime, "#yyyy-MM-dd HH:mm:ss#");
				DataTable dataTable = new DataTable("Stat");
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingIdentity";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingShould";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingInFact";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingLeave";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingAbsent";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingLate";
				dataTable.Columns.Add(dataColumn);
				dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "strMeetingRatio";
				dataTable.Columns.Add(dataColumn);
				for (int j = 0; j <= 6; j++)
				{
					DataRow dataRow = dataTable.NewRow();
					if (j == 6)
					{
						dataRow[0] = CommonStr.strMeetingSubTotal;
					}
					else
					{
						dataRow[0] = frmMeetings.getStrMeetingIdentity((long)j);
					}
					for (i = 0; i <= 4; i++)
					{
						dataRow[i + 1] = this.arrMeetingNum[j, i];
						if (dataRow[i + 1].ToString() == "0")
						{
							dataRow[i + 1] = "";
						}
					}
					dataRow[6] = this.arrMeetingNum[j, 5] / 10L + "%";
					if (string.IsNullOrEmpty(dataRow[1].ToString()))
					{
						dataRow[6] = "";
					}
					dataTable.Rows.Add(dataRow);
				}
				DataTable dataTable2 = this.ds.Tables["t_d_MeetingConsumer"];
				this.dgvMain.AutoGenerateColumns = false;
				i = 0;
				while (i < dataTable2.Columns.Count && i < this.dgvMain.ColumnCount)
				{
					this.dgvMain.Columns[i].DataPropertyName = dataTable2.Columns[i].ColumnName;
					i++;
				}
				this.dgvMain.DataSource = this.dvShould;
				this.dgvMain.DefaultCellStyle.ForeColor = Color.Black;
				wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_SignRealTime", wgTools.DisplayFormat_DateYMDHMSWeek);
				this.dgvStat.AutoGenerateColumns = false;
				i = 0;
				while (i < dataTable.Columns.Count && i < this.dgvStat.ColumnCount)
				{
					this.dgvStat.Columns[i].DataPropertyName = dataTable.Columns[i].ColumnName;
					i++;
				}
				this.dgvStat.DataSource = dataTable;
				this.dgvStat.DefaultCellStyle.ForeColor = Color.Black;
				this.tabControl1.SelectedIndex = 5;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dfrmStd_Load(object sender, EventArgs e)
		{
			bool flag = false;
			string funName = "mnuMeeting";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnManualSign.Enabled = false;
				this.btnLeave.Enabled = false;
			}
			try
			{
				if (this.curMeetingNo == "")
				{
					base.DialogResult = DialogResult.Cancel;
					base.Close();
				}
				TabPage selectedTab = this.tabControl1.SelectedTab;
				this.fillMeetingNum();
				if (!string.IsNullOrEmpty(this.meetingName))
				{
					this.Text = this.Text + "[" + this.meetingName + "]";
				}
				this.tabControl1.SelectedTab = selectedTab;
				this.tabControl1_SelectedIndexChanged(null, null);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnManualSign_Click(object sender, EventArgs e)
		{
			try
			{
				using (dfrmManualSign dfrmManualSign = new dfrmManualSign())
				{
					dfrmManualSign.curMeetingNo = this.curMeetingNo;
					dfrmManualSign.Text = this.btnManualSign.Text;
					if (dfrmManualSign.ShowDialog(this) == DialogResult.OK)
					{
						int selectedIndex = this.tabControl1.SelectedIndex;
						this.fillMeetingNum();
						this.tabControl1.SelectedIndex = selectedIndex;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnRecreate_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				string text = " UPDATE t_d_MeetingConsumer ";
				text += " SET [f_SignRealTime] = NULL ";
				text += " ,[f_RecID] = 0 ";
				text = text + " WHERE f_SignWay = 0 AND  f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				wgAppConfig.runUpdateSql(text);
				this.lngDealtRecordID = -1L;
				this.fillMeetingRecord(this.curMeetingNo);
				int selectedIndex = this.tabControl1.SelectedIndex;
				this.fillMeetingNum();
				this.tabControl1.SelectedIndex = selectedIndex;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			this.Cursor = Cursors.Default;
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.tabControl1.SelectedIndex;
			this.fillMeetingNum();
			this.tabControl1.SelectedIndex = selectedIndex;
		}

		private void btnLeave_Click(object sender, EventArgs e)
		{
			try
			{
				using (dfrmManualSign dfrmManualSign = new dfrmManualSign())
				{
					dfrmManualSign.curMeetingNo = this.curMeetingNo;
					dfrmManualSign.curMode = "Leave";
					dfrmManualSign.Text = this.btnLeave.Text;
					if (dfrmManualSign.ShowDialog(this) == DialogResult.OK)
					{
						int arg_43_0 = this.tabControl1.SelectedIndex;
						this.fillMeetingNum();
						this.tabControl1.SelectedIndex = 2;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dgvMain.Visible)
				{
					wgAppConfig.printdgv(this.dgvMain, this.Text + " ( " + this.tabControl1.SelectedTab.Text + " )");
				}
				else
				{
					wgAppConfig.printdgv(this.dgvStat, this.Text + " ( " + this.tabControl1.SelectedTab.Text + " )");
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (this.dgvMain.Visible)
			{
				wgAppConfig.exportToExcel(this.dgvMain, this.Text + " ( " + this.tabControl1.SelectedTab.Text + " )");
				return;
			}
			wgAppConfig.exportToExcel(this.dgvStat, this.Text + " ( " + this.tabControl1.SelectedTab.Text + " )");
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
		}

		private void radioButton25_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				this.dgvMain.DataSource = this.dvShould;
				break;
			case 1:
				this.dgvMain.DataSource = this.dvInFact;
				break;
			case 2:
				this.dgvMain.DataSource = this.dvLeave;
				break;
			case 3:
				this.dgvMain.DataSource = this.dvAbsent;
				break;
			case 4:
				this.dgvMain.DataSource = this.dvLate;
				break;
			}
			if (this.tabControl1.SelectedIndex >= 0 && this.tabControl1.SelectedIndex <= 4)
			{
				this.dgvStat.Visible = false;
				this.dgvMain.Visible = true;
			}
			else
			{
				this.dgvStat.Visible = true;
				this.dgvMain.Visible = false;
			}
			this.dgvMain.Dock = DockStyle.Fill;
			this.dgvStat.Dock = DockStyle.Fill;
		}

		private void dfrmMeetingStatDetail_KeyDown(object sender, KeyEventArgs e)
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

		private void dfrmMeetingStatDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
