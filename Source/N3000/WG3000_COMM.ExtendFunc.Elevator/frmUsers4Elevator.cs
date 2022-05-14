using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Elevator
{
	public class frmUsers4Elevator : Form
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private DataGridView dgvUsers;

		private ToolStripButton btnExport;

		private BackgroundWorker backgroundWorker1;

		private ImageList imageList1;

		private ToolStripButton btnPrint;

		private UserControlFind userControlFind1;

		private ToolStripButton btnAutoAdd;

		private ToolStripButton btnBatchUpdate;

		private ToolStripButton btnEditPrivilege;

		private OpenFileDialog openFileDialog1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem batchUpdateSelectToolStripMenuItem;

		private ToolStripButton btnExit;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn ConsumerNO;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewCheckBoxColumn Attend;

		private DataGridViewCheckBoxColumn Shift;

		private DataGridViewCheckBoxColumn Door;

		private DataGridViewTextBoxColumn Start;

		private DataGridViewTextBoxColumn End;

		private DataGridViewTextBoxColumn Deptname;

		private DataGridViewTextBoxColumn floorName;

		private DataGridViewTextBoxColumn TimeProfile;

		private DataGridViewTextBoxColumn MoreFloor;

		private DataGridViewTextBoxColumn FloorID;

		private DataTable dtUserFloor;

		private DataView dvUserFloor;

		private string dgvSql;

		private bool bLoadedFinished;

		private string recNOMax = "";

		private DataTable tb;

		private DataView dv;

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private bool firstShow = true;

		private int currentRowIndex;

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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmUsers4Elevator));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.dgvUsers = new DataGridView();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.ConsumerNO = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.Attend = new DataGridViewCheckBoxColumn();
			this.Shift = new DataGridViewCheckBoxColumn();
			this.Door = new DataGridViewCheckBoxColumn();
			this.Start = new DataGridViewTextBoxColumn();
			this.End = new DataGridViewTextBoxColumn();
			this.Deptname = new DataGridViewTextBoxColumn();
			this.floorName = new DataGridViewTextBoxColumn();
			this.TimeProfile = new DataGridViewTextBoxColumn();
			this.MoreFloor = new DataGridViewTextBoxColumn();
			this.FloorID = new DataGridViewTextBoxColumn();
			this.backgroundWorker1 = new BackgroundWorker();
			this.imageList1 = new ImageList(this.components);
			this.toolStrip1 = new ToolStrip();
			this.btnBatchUpdate = new ToolStripButton();
			this.btnEditPrivilege = new ToolStripButton();
			this.btnAutoAdd = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExport = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			this.openFileDialog1 = new OpenFileDialog();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.batchUpdateSelectToolStripMenuItem = new ToolStripMenuItem();
			this.userControlFind1 = new UserControlFind();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.dgvUsers, "dgvUsers");
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AllowUserToDeleteRows = false;
			this.dgvUsers.AllowUserToOrderColumns = true;
			this.dgvUsers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ConsumerID,
				this.ConsumerNO,
				this.ConsumerName,
				this.CardNO,
				this.Attend,
				this.Shift,
				this.Door,
				this.Start,
				this.End,
				this.Deptname,
				this.floorName,
				this.TimeProfile,
				this.MoreFloor,
				this.FloorID
			});
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvUsers.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvUsers_CellFormatting);
			this.dgvUsers.Scroll += new ScrollEventHandler(this.dgvUsers_Scroll);
			this.dgvUsers.DoubleClick += new EventHandler(this.dgvUsers_DoubleClick);
			this.dgvUsers.KeyDown += new KeyEventHandler(this.frmUsers_KeyDown);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.ConsumerNO.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.ConsumerNO, "ConsumerNO");
			this.ConsumerNO.Name = "ConsumerNO";
			this.ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ConsumerName, "ConsumerName");
			this.ConsumerName.Name = "ConsumerName";
			this.ConsumerName.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.CardNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.CardNO, "CardNO");
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Attend, "Attend");
			this.Attend.Name = "Attend";
			this.Attend.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Shift, "Shift");
			this.Shift.Name = "Shift";
			this.Shift.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Door, "Door");
			this.Door.Name = "Door";
			this.Door.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Start, "Start");
			this.Start.Name = "Start";
			this.Start.ReadOnly = true;
			componentResourceManager.ApplyResources(this.End, "End");
			this.End.Name = "End";
			this.End.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Deptname, "Deptname");
			this.Deptname.Name = "Deptname";
			this.Deptname.ReadOnly = true;
			this.floorName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.floorName, "floorName");
			this.floorName.Name = "floorName";
			this.floorName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.TimeProfile, "TimeProfile");
			this.TimeProfile.Name = "TimeProfile";
			this.TimeProfile.ReadOnly = true;
			componentResourceManager.ApplyResources(this.MoreFloor, "MoreFloor");
			this.MoreFloor.Name = "MoreFloor";
			this.MoreFloor.ReadOnly = true;
			componentResourceManager.ApplyResources(this.FloorID, "FloorID");
			this.FloorID.Name = "FloorID";
			this.FloorID.ReadOnly = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "add.png");
			this.imageList1.Images.SetKeyName(1, "edit.png");
			this.imageList1.Images.SetKeyName(2, "delete.png");
			this.imageList1.Images.SetKeyName(3, "cancel.png");
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnBatchUpdate,
				this.btnEditPrivilege,
				this.btnAutoAdd,
				this.btnPrint,
				this.btnExport,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
			componentResourceManager.ApplyResources(this.btnBatchUpdate, "btnBatchUpdate");
			this.btnBatchUpdate.ForeColor = Color.White;
			this.btnBatchUpdate.Image = Resources.pTools_Edit_Batch;
			this.btnBatchUpdate.Name = "btnBatchUpdate";
			this.btnBatchUpdate.Click += new EventHandler(this.btnBatchUpdate_Click);
			componentResourceManager.ApplyResources(this.btnEditPrivilege, "btnEditPrivilege");
			this.btnEditPrivilege.ForeColor = Color.White;
			this.btnEditPrivilege.Image = Resources.pTools_EditPrivielge;
			this.btnEditPrivilege.Name = "btnEditPrivilege";
			this.btnEditPrivilege.Click += new EventHandler(this.btnEditPrivilege_Click);
			componentResourceManager.ApplyResources(this.btnAutoAdd, "btnAutoAdd");
			this.btnAutoAdd.ForeColor = Color.White;
			this.btnAutoAdd.Image = Resources.pTools_Add_Auto;
			this.btnAutoAdd.Name = "btnAutoAdd";
			this.btnAutoAdd.Click += new EventHandler(this.btnAutoAdd_Click);
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			componentResourceManager.ApplyResources(this.btnExport, "btnExport");
			this.btnExport.ForeColor = Color.White;
			this.btnExport.Image = Resources.pTools_ExportToExcel;
			this.btnExport.Name = "btnExport";
			this.btnExport.Click += new EventHandler(this.btnExport_Click);
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.openFileDialog1.FileName = "openFileDialog1";
			componentResourceManager.ApplyResources(this.openFileDialog1, "openFileDialog1");
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.batchUpdateSelectToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.batchUpdateSelectToolStripMenuItem, "batchUpdateSelectToolStripMenuItem");
			this.batchUpdateSelectToolStripMenuItem.Name = "batchUpdateSelectToolStripMenuItem";
			this.batchUpdateSelectToolStripMenuItem.Click += new EventHandler(this.batchUpdateSelectToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.Name = "userControlFind1";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvUsers);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip1);
			this.DoubleBuffered = true;
			base.KeyPreview = true;
			base.Name = "frmUsers4Elevator";
			base.FormClosing += new FormClosingEventHandler(this.frmUsers4Elevator_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.frmUsers_FormClosed);
			base.Load += new EventHandler(this.frmUsers_Load);
			base.KeyDown += new KeyEventHandler(this.frmUsers_KeyDown);
			((ISupportInitialize)this.dgvUsers).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmUsers4Elevator()
		{
			this.InitializeComponent();
		}

		private void loadUserFloor()
		{
			string cmdText = " SELECT  t_b_UserFloor .*,  t_b_Door.f_DoorName + '.' +  t_b_floor.f_floorName as f_floorName  FROM (t_b_UserFloor INNER JOIN t_b_Floor ON t_b_UserFloor.f_floorID = t_b_Floor.f_floorID) LEFT JOIN (t_b_Controller RIGHT JOIN t_b_Door ON t_b_Controller.f_ControllerID = t_b_Door.f_ControllerID) ON t_b_Floor.f_DoorID = t_b_Door.f_DoorID ";
			this.dtUserFloor = new DataTable();
			this.dvUserFloor = new DataView(this.dtUserFloor);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtUserFloor);
						}
					}
					return;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtUserFloor);
					}
				}
			}
		}

		private void frmUsers_Load(object sender, EventArgs e)
		{
			if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 2)
			{
				this.btnEditPrivilege.Text = CommonStr.strFloorPrivilege2;
				this.btnBatchUpdate.Text = CommonStr.strFloorConfigure2;
			}
			else if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 3)
			{
				this.btnEditPrivilege.Text = CommonStr.strFloorPrivilege3;
				this.btnBatchUpdate.Text = CommonStr.strFloorConfigure3;
			}
			this.ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.ConsumerNO.HeaderText);
			Icon icon = base.Icon;
			wgAppConfig.GetAppIcon(ref icon);
			base.Icon = icon;
			this.Deptname.HeaderText = wgAppConfig.ReplaceFloorRomm(this.Deptname.HeaderText);
			this.loadOperatorPrivilege();
			this.loadUserFloor();
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.loadStyle();
			Cursor.Current = Cursors.WaitCursor;
			this.btnAutoAdd.Visible = (wgAppConfig.GetKeyVal("ElevatorGroupVisible") == "1");
			this.dgvUsers.ContextMenuStrip = this.contextMenuStrip1;
			this.userControlFind1.btnQuery.PerformClick();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuConsumers";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag))
			{
				if (flag)
				{
					this.btnAutoAdd.Visible = false;
					this.btnBatchUpdate.Visible = false;
					return;
				}
			}
			else
			{
				base.Close();
			}
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			this.loadUserFloor();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text = "";
			long num4 = 0L;
			int num5 = 0;
			this.userControlFind1.getSqlInfo(ref num, ref num2, ref num3, ref text, ref num4, ref num5);
			string text2 = " SELECT    t_b_Consumer.f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName, ' ' as f_FloorNameDesc,' ' as f_ControlSegID,' ' as f_MoreFloorNum, ' ' as f_FloorID  ";
			text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
			if (num5 > 0)
			{
				text2 = " SELECT    t_b_Consumer.f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName, ' ' as f_FloorNameDesc,' ' as f_ControlSegID,' ' as f_MoreFloorNum, ' ' as f_FloorID  ";
				text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				text2 = text2 + " WHERE  t_b_Consumer.f_ConsumerID = " + num5.ToString();
			}
			else if (num > 0)
			{
				text2 = " SELECT    t_b_Consumer.f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName, ' ' as f_FloorNameDesc,' ' as f_ControlSegID,' ' as f_MoreFloorNum, ' ' as f_FloorID  ";
				if (num >= num3)
				{
					text2 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text2 += string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", num2);
				}
				else
				{
					text2 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text2 += string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", num);
					text2 += string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", num3);
				}
				if (text != "")
				{
					text2 += string.Format(" AND f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text)));
				}
				else if (num4 > 0L)
				{
					text2 += string.Format(" AND f_CardNO ={0:d} ", num4);
				}
			}
			else if (text != "")
			{
				text2 = " SELECT    t_b_Consumer.f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName, ' ' as f_FloorNameDesc,' ' as f_ControlSegID,' ' as f_MoreFloorNum, ' ' as f_FloorID  ";
				text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				text2 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text)));
			}
			else if (num4 > 0L)
			{
				text2 = " SELECT    t_b_Consumer.f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName, ' ' as f_FloorNameDesc,' ' as f_ControlSegID,' ' as f_MoreFloorNum, ' ' as f_FloorID  ";
				text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				text2 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
			}
			this.reloadUserData(text2);
		}

		private void loadStyle()
		{
			this.dgvUsers.AutoGenerateColumns = false;
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(121);
			this.dgvUsers.Columns[11].Visible = paramValBoolByNO;
			wgAppConfig.ReadGVStyle(this, this.dgvUsers);
		}

		private void reloadUserData(string strsql)
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
			this.dgvUsers.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private DataView loadUserData(int startIndex, int maxRecords, string strSql)
		{
			wgTools.WriteLine("loadUserData Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recNOMax = "";
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND f_ConsumerNO > {0}", wgTools.PrepareStr(this.recNOMax));
			}
			else
			{
				strSql += string.Format(" WHERE f_ConsumerNO > {0}", wgTools.PrepareStr(this.recNOMax));
			}
			strSql += " ORDER BY f_ConsumerNO ";
			this.tb = new DataTable("users");
			this.dv = new DataView(this.tb);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.tb);
						}
					}
					goto IL_187;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.tb);
					}
				}
			}
			IL_187:
			if (this.tb.Rows.Count > 0)
			{
				this.recNOMax = this.tb.Rows[this.tb.Rows.Count - 1]["f_ConsumerNO"].ToString();
			}
			wgTools.WriteLine("loadUserData End");
			return this.dv;
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				using (dfrmElevatorGroup dfrmElevatorGroup = new dfrmElevatorGroup())
				{
					dfrmElevatorGroup.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void dgvUsers_DoubleClick(object sender, EventArgs e)
		{
			this.btnEditPrivilege.PerformClick();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcelSpecial(ref this.dgvUsers, this.Text, this.bLoadedFinished, ref this.backgroundWorker1, ref this.startRecordIndex, this.MaxRecord, this.dgvSql);
		}

		private void fillDgv(DataView dvUser4Elevator)
		{
			try
			{
				DataGridView dataGridView = this.dgvUsers;
				if (dataGridView.DataSource == null)
				{
					dataGridView.DataSource = dvUser4Elevator;
					for (int i = 0; i < dvUser4Elevator.Table.Columns.Count; i++)
					{
						dataGridView.Columns[i].DataPropertyName = dvUser4Elevator.Table.Columns[i].ColumnName;
						dataGridView.Columns[i].Name = dvUser4Elevator.Table.Columns[i].ColumnName;
					}
					wgAppConfig.setDisplayFormatDate(dataGridView, "f_BeginYMD", wgTools.DisplayFormat_DateYMD);
					wgAppConfig.setDisplayFormatDate(dataGridView, "f_EndYMD", wgTools.DisplayFormat_DateYMD);
					wgAppConfig.ReadGVStyle(this, dataGridView);
					if (this.startRecordIndex == 0 && dvUser4Elevator.Count >= this.MaxRecord)
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
				else if (dvUser4Elevator.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = dataGridView.FirstDisplayedScrollingRowIndex;
					DataView dataView = dataGridView.DataSource as DataView;
					dataView.Table.Merge(dvUser4Elevator.Table);
					if (firstDisplayedScrollingRowIndex >= 0)
					{
						dataGridView.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
				}
				if (this.dgvUsers.RowCount > 0 && this.dgvUsers.RowCount > this.currentRowIndex)
				{
					this.dgvUsers.CurrentCell = this.dgvUsers[1, this.currentRowIndex];
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
			e.Result = this.loadUserData(startIndex, maxRecords, strSql);
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				wgTools.WgDebugWrite("Users Operation was canceled", new object[0]);
				return;
			}
			if (e.Error != null)
			{
				string info = string.Format("An error occurred: {0}", e.Error.Message);
				wgTools.WgDebugWrite(info, new object[0]);
				return;
			}
			if ((e.Result as DataView).Count < this.MaxRecord)
			{
				this.bLoadedFinished = true;
			}
			this.fillDgv(e.Result as DataView);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvUsers_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				DataGridView dataGridView = this.dgvUsers;
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > dataGridView.Rows.Count || e.NewValue + dataGridView.Rows.Count / 10 > dataGridView.Rows.Count))
				{
					if (this.startRecordIndex <= dataGridView.Rows.Count)
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
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvUsers, this.Text);
		}

		private void btnAutoAdd_Click(object sender, EventArgs e)
		{
			try
			{
				using (dfrmElevatorGroup dfrmElevatorGroup = new dfrmElevatorGroup())
				{
					dfrmElevatorGroup.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void frmUsers_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void showUpload()
		{
			if (this.firstShow)
			{
				this.firstShow = false;
				XMessageBox.Show(CommonStr.strNeedUploadFloor);
			}
		}

		private void btnEditPrivilege_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvUsers.SelectedRows.Count <= 0)
				{
					if (this.dgvUsers.SelectedCells.Count <= 0)
					{
						XMessageBox.Show(this, CommonStr.strSelectUser, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					index = this.dgvUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvUsers.SelectedRows[0].Index;
				}
				if (this.dgvUsers.Rows.Count > 0)
				{
					this.currentRowIndex = this.dgvUsers.CurrentCell.RowIndex;
				}
				using (dfrmUserFloor dfrmUserFloor = new dfrmUserFloor())
				{
					dfrmUserFloor.consumerID = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
					dfrmUserFloor.Text = string.Concat(new string[]
					{
						this.dgvUsers.Rows[index].Cells[1].Value.ToString().Trim(),
						".",
						this.dgvUsers.Rows[index].Cells[2].Value.ToString().Trim(),
						" -- ",
						dfrmUserFloor.Text
					});
					if (dfrmUserFloor.ShowDialog(this) == DialogResult.OK)
					{
						this.showUpload();
						this.btnQuery_Click(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void funcCtrlShiftQ()
		{
			this.btnAutoAdd.Visible = true;
			wgAppConfig.UpdateKeyVal("ElevatorGroupVisible", "1");
		}

		private void frmUsers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
				{
					dfrmInputNewName.setPasswordChar('*');
					if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
					{
						return;
					}
					if (dfrmInputNewName.strNewName != "5678")
					{
						return;
					}
					this.funcCtrlShiftQ();
				}
			}
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

		private void frmUsers_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < this.dgvUsers.Columns.Count && this.dgvUsers.Columns[e.ColumnIndex].Name.Equals("f_FloorNameDesc"))
			{
				string text = e.Value as string;
				if (text != null && text != " ")
				{
					return;
				}
				DataGridViewCell dataGridViewCell = this.dgvUsers[e.ColumnIndex, e.RowIndex];
				string text2 = this.dgvUsers[0, e.RowIndex].Value.ToString();
				if (this.dvUserFloor == null || string.IsNullOrEmpty(text2))
				{
					e.Value = "";
					dataGridViewCell.Value = "";
					return;
				}
				this.dvUserFloor.RowFilter = "f_ConsumerID = " + text2;
				if (this.dvUserFloor.Count == 0)
				{
					e.Value = "";
					dataGridViewCell.Value = "";
					return;
				}
				if (this.dvUserFloor.Count == 1)
				{
					e.Value = this.dvUserFloor[0]["f_floorName"];
					dataGridViewCell.Value = this.dvUserFloor[0]["f_floorName"];
					this.dgvUsers[e.ColumnIndex + 1, e.RowIndex].Value = this.dvUserFloor[0]["f_ControlSegID"];
					return;
				}
				if (this.dvUserFloor.Count > 1)
				{
					e.Value = CommonStr.strElevatorMoreFloors + string.Format("({0})", this.dvUserFloor.Count.ToString());
					dataGridViewCell.Value = CommonStr.strElevatorMoreFloors + string.Format("({0})", this.dvUserFloor.Count.ToString());
					this.dgvUsers[e.ColumnIndex + 1, e.RowIndex].Value = this.dvUserFloor[0]["f_ControlSegID"];
					return;
				}
				e.Value = "";
				dataGridViewCell.Value = "";
			}
		}

		private void btnBatchUpdate_Click(object sender, EventArgs e)
		{
			using (dfrmFloors dfrmFloors = new dfrmFloors())
			{
				dfrmFloors.Text = this.btnBatchUpdate.Text;
				if (dfrmFloors.ShowDialog(this) == DialogResult.OK)
				{
					this.reloadUserData("");
				}
			}
		}

		private void batchUpdateSelectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvUsers.SelectedRows.Count <= 0)
			{
				return;
			}
			using (dfrmUserFloor dfrmUserFloor = new dfrmUserFloor())
			{
				string text = "";
				for (int i = 0; i < this.dgvUsers.SelectedRows.Count; i++)
				{
					int index = this.dgvUsers.SelectedRows[i].Index;
					int num = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += num.ToString();
				}
				dfrmUserFloor.strSqlSelected = text;
				dfrmUserFloor.Text = string.Format("{0}: [{1}]", sender.ToString(), this.dgvUsers.SelectedRows.Count.ToString());
				if (dfrmUserFloor.ShowDialog(this) == DialogResult.OK)
				{
					this.btnQuery_Click(null, null);
				}
			}
		}

		private void frmUsers4Elevator_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
