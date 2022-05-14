using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class frmUsers : Form
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private DataGridView dgvUsers;

		private ToolStripButton btnRegisterLostCard;

		private ToolStripButton btnExport;

		private BackgroundWorker backgroundWorker1;

		private ImageList imageList1;

		private ToolStripButton btnPrint;

		private UserControlFind userControlFind1;

		private ToolStripButton btnImportFromExcel;

		private ToolStripButton btnAutoAdd;

		private ToolStripButton btnBatchUpdate;

		private ToolStripButton btnEditPrivilege;

		private OpenFileDialog openFileDialog1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem batchUpdateSelectToolStripMenuItem;

		private ToolStripMenuItem importFromExcelToolStripMenuItem;

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

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem displayAllToolStripMenuItem;

		private string dgvSql;

		private bool bLoadedFinished;

		private string recNOMax = "";

		private DataTable tb4loadUserData;

		private DataView dv4loadUserData;

		private int deletedUserCnt;

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private DataSet DS;

		private OleDbDataAdapter MyCommand;

		private OleDbConnection MyConnection;

		private DataView dv;

		private dfrmWait dfrmWait1 = new dfrmWait();

		public WatchingService watching;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dv != null)
				{
					this.dv.Dispose();
				}
				if (this.dv4loadUserData != null)
				{
					this.dv4loadUserData.Dispose();
				}
				if (this.tb4loadUserData != null)
				{
					this.tb4loadUserData.Dispose();
				}
				if (this.userControlFind1 != null)
				{
					this.userControlFind1.Dispose();
				}
				if (disposing && this.dfrmWait1 != null)
				{
					this.dfrmWait1.Dispose();
				}
			}
			if (disposing && this.watching != null)
			{
				this.watching.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmUsers));
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
			this.backgroundWorker1 = new BackgroundWorker();
			this.imageList1 = new ImageList(this.components);
			this.toolStrip1 = new ToolStrip();
			this.btnAutoAdd = new ToolStripButton();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExport = new ToolStripButton();
			this.btnImportFromExcel = new ToolStripButton();
			this.btnRegisterLostCard = new ToolStripButton();
			this.btnBatchUpdate = new ToolStripButton();
			this.btnEditPrivilege = new ToolStripButton();
			this.openFileDialog1 = new OpenFileDialog();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.batchUpdateSelectToolStripMenuItem = new ToolStripMenuItem();
			this.importFromExcelToolStripMenuItem = new ToolStripMenuItem();
			this.displayAllToolStripMenuItem = new ToolStripMenuItem();
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
				this.Deptname
			});
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvUsers.Scroll += new ScrollEventHandler(this.dgvUsers_Scroll);
			this.dgvUsers.DoubleClick += new EventHandler(this.dgvUsers_DoubleClick);
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
			this.Deptname.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.Deptname, "Deptname");
			this.Deptname.Name = "Deptname";
			this.Deptname.ReadOnly = true;
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
				this.btnAutoAdd,
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnPrint,
				this.btnExport,
				this.btnImportFromExcel,
				this.btnRegisterLostCard,
				this.btnBatchUpdate,
				this.btnEditPrivilege
			});
			this.toolStrip1.Name = "toolStrip1";
			componentResourceManager.ApplyResources(this.btnAutoAdd, "btnAutoAdd");
			this.btnAutoAdd.ForeColor = Color.White;
			this.btnAutoAdd.Image = Resources.pTools_Add_Auto;
			this.btnAutoAdd.Name = "btnAutoAdd";
			this.btnAutoAdd.Click += new EventHandler(this.btnAutoAdd_Click);
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
			componentResourceManager.ApplyResources(this.btnExport, "btnExport");
			this.btnExport.ForeColor = Color.White;
			this.btnExport.Image = Resources.pTools_ExportToExcel;
			this.btnExport.Name = "btnExport";
			this.btnExport.Click += new EventHandler(this.btnExport_Click);
			componentResourceManager.ApplyResources(this.btnImportFromExcel, "btnImportFromExcel");
			this.btnImportFromExcel.ForeColor = Color.White;
			this.btnImportFromExcel.Image = Resources.pTools_ImportFromExcel;
			this.btnImportFromExcel.Name = "btnImportFromExcel";
			this.btnImportFromExcel.Click += new EventHandler(this.btnImportFromExcel_Click);
			componentResourceManager.ApplyResources(this.btnRegisterLostCard, "btnRegisterLostCard");
			this.btnRegisterLostCard.ForeColor = Color.White;
			this.btnRegisterLostCard.Image = Resources.pTools_CardLost;
			this.btnRegisterLostCard.Name = "btnRegisterLostCard";
			this.btnRegisterLostCard.Click += new EventHandler(this.btnRegisterLostCard_Click);
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
			this.openFileDialog1.FileName = "openFileDialog1";
			componentResourceManager.ApplyResources(this.openFileDialog1, "openFileDialog1");
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem1,
				this.batchUpdateSelectToolStripMenuItem,
				this.importFromExcelToolStripMenuItem,
				this.displayAllToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
			componentResourceManager.ApplyResources(this.batchUpdateSelectToolStripMenuItem, "batchUpdateSelectToolStripMenuItem");
			this.batchUpdateSelectToolStripMenuItem.Name = "batchUpdateSelectToolStripMenuItem";
			this.batchUpdateSelectToolStripMenuItem.Click += new EventHandler(this.batchUpdateSelectToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.importFromExcelToolStripMenuItem, "importFromExcelToolStripMenuItem");
			this.importFromExcelToolStripMenuItem.Name = "importFromExcelToolStripMenuItem";
			this.importFromExcelToolStripMenuItem.Click += new EventHandler(this.btnImportFromExcel_Click);
			componentResourceManager.ApplyResources(this.displayAllToolStripMenuItem, "displayAllToolStripMenuItem");
			this.displayAllToolStripMenuItem.Name = "displayAllToolStripMenuItem";
			this.displayAllToolStripMenuItem.Click += new EventHandler(this.displayAllToolStripMenuItem_Click);
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
			base.Name = "frmUsers";
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

		public frmUsers()
		{
			this.InitializeComponent();
		}

		private void frmUsers_Load(object sender, EventArgs e)
		{
			this.Deptname.HeaderText = wgAppConfig.ReplaceFloorRomm(this.Deptname.HeaderText);
			this.ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.ConsumerNO.HeaderText);
			this.loadOperatorPrivilege();
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.loadStyle();
			Cursor.Current = Cursors.WaitCursor;
			string str = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
			str += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
			this.userControlFind1.btnQuery.PerformClick();
			bool flag;
			bool flag2;
			icOperator.getFrmOperatorPrivilege("mnuCardLost", out flag, out flag2);
			this.btnRegisterLostCard.Visible = flag2;
			bool flag3 = false;
			string funName = "mnuCardLost";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag3))
			{
				this.btnRegisterLostCard.Visible = !flag3;
			}
			else
			{
				this.btnRegisterLostCard.Visible = false;
			}
			this.btnEditPrivilege.Visible = false;
			flag2 = !wgAppConfig.getParamValBoolByNO(111);
			if (flag2)
			{
				flag3 = false;
				funName = "mnu1DoorControl";
				if (icOperator.OperatePrivilegeVisible(funName, ref flag3) && !flag3)
				{
					funName = "mnuPrivilege";
					if (icOperator.OperatePrivilegeVisible(funName, ref flag3) && !flag3)
					{
						this.btnEditPrivilege.Visible = true;
					}
				}
			}
			icControllerZone icControllerZone = new icControllerZone();
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			icControllerZone.getZone(ref arrayList, ref arrayList2, ref arrayList3);
			if (arrayList2.Count > 0 && (int)arrayList2[0] != 0)
			{
				this.btnEditPrivilege.Enabled = false;
			}
			this.dgvUsers.ContextMenuStrip = this.contextMenuStrip1;
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
					this.btnAdd.Visible = false;
					this.btnEdit.Visible = false;
					this.btnDelete.Visible = false;
					this.btnImportFromExcel.Visible = false;
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
			this.deletedUserCnt = 0;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text = "";
			long num4 = 0L;
			int num5 = 0;
			this.userControlFind1.getSqlInfo(ref num, ref num2, ref num3, ref text, ref num4, ref num5);
			string text2 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
			text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
			if (num5 > 0)
			{
				text2 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
				text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				text2 = text2 + " WHERE  t_b_Consumer.f_ConsumerID = " + num5.ToString();
			}
			else if (num > 0)
			{
				text2 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
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
				text2 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
				text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				text2 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text)));
			}
			else if (num4 > 0L)
			{
				text2 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
				text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				text2 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
			}
			this.reloadUserData(text2);
		}

		private void loadStyle()
		{
			this.dgvUsers.AutoGenerateColumns = false;
			bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
			this.dgvUsers.Columns[5].Visible = paramValBoolByNO;
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
			this.tb4loadUserData = new DataTable("users");
			this.dv4loadUserData = new DataView(this.tb4loadUserData);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.tb4loadUserData);
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
						sqlDataAdapter.Fill(this.tb4loadUserData);
					}
				}
			}
			IL_187:
			if (this.tb4loadUserData.Rows.Count > 0)
			{
				this.recNOMax = this.tb4loadUserData.Rows[this.tb4loadUserData.Rows.Count - 1]["f_ConsumerNO"].ToString();
			}
			wgTools.WriteLine("loadUserData End");
			return this.dv4loadUserData;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			DataGridView dataGridView = this.dgvUsers;
			DataGridViewColumn sortedColumn = dataGridView.SortedColumn;
			ListSortDirection direction = ListSortDirection.Ascending;
			if (sortedColumn != null && dataGridView.SortOrder == System.Windows.Forms.SortOrder.Descending)
			{
				direction = ListSortDirection.Descending;
			}
			if (dataGridView.Rows.Count > 0)
			{
				num = dataGridView.CurrentCell.RowIndex;
			}
			using (dfrmUser dfrmUser = new dfrmUser())
			{
				if (dfrmUser.ShowDialog(this) == DialogResult.OK)
				{
					this.reloadUserData("");
					if (dataGridView.RowCount > 0)
					{
						if (dataGridView.RowCount > num)
						{
							dataGridView.CurrentCell = dataGridView[1, num];
						}
						else
						{
							dataGridView.CurrentCell = dataGridView[1, dataGridView.RowCount - 1];
						}
					}
					if (sortedColumn != null)
					{
						dataGridView.Sort(sortedColumn, direction);
					}
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvUsers.SelectedRows.Count <= 0)
				{
					if (this.dgvUsers.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvUsers.SelectedRows[0].Index;
				}
				using (dfrmUser dfrmUser = new dfrmUser())
				{
					dfrmUser.consumerID = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
					dfrmUser.OperateNew = false;
					if (dfrmUser.ShowDialog(this) == DialogResult.OK)
					{
						DataGridViewRow dataGridViewRow = this.dgvUsers.Rows[index];
						string text = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
						text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
						text = text + " WHERE f_ConsumerID= " + dfrmUser.consumerID.ToString();
						if (wgAppConfig.IsAccessDB)
						{
							using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
							{
								using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
								{
									oleDbConnection.Open();
									OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
									if (oleDbDataReader.Read())
									{
										for (int i = 1; i < this.dgvUsers.Columns.Count; i++)
										{
											dataGridViewRow.Cells[i].Value = oleDbDataReader[i];
										}
									}
									oleDbDataReader.Close();
								}
								goto IL_20A;
							}
						}
						using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
						{
							using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
							{
								sqlConnection.Open();
								SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
								if (sqlDataReader.Read())
								{
									for (int j = 1; j < this.dgvUsers.Columns.Count; j++)
									{
										dataGridViewRow.Cells[j].Value = sqlDataReader[j];
									}
								}
								sqlDataReader.Close();
							}
						}
					}
					IL_20A:;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void dgvUsers_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvUsers.SelectedRows.Count <= 0)
				{
					if (this.dgvUsers.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvUsers.SelectedRows[0].Index;
				}
				int num = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
				if (num > 0)
				{
					string text;
					if (this.dgvUsers.SelectedRows.Count == 1)
					{
						text = string.Format("{0}\r\n\r\n{1}:  {2}", this.btnDelete.Text, this.dgvUsers.Columns[2].HeaderText, this.dgvUsers.Rows[index].Cells[2].Value.ToString());
					}
					else
					{
						text = string.Format("{0}\r\n\r\n{1}=  {2}", this.btnDelete.Text, CommonStr.strUsersNum, this.dgvUsers.SelectedRows.Count);
					}
					text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
					if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.OK)
					{
						icConsumer icConsumer = new icConsumer();
						if (this.dgvUsers.SelectedRows.Count == 1)
						{
							text = string.Format("{0} {1}:  {2}", this.btnDelete.Text, this.dgvUsers.Columns[2].HeaderText, this.dgvUsers.Rows[index].Cells[2].Value.ToString());
						}
						else
						{
							text = string.Format("{0} {1}=  {2}", this.btnDelete.Text, CommonStr.strUsersNum, this.dgvUsers.SelectedRows.Count);
							text += string.Format("From {0}...", this.dgvUsers.Rows[index].Cells[2].Value.ToString());
						}
						int count = this.dgvUsers.SelectedRows.Count;
						if (this.dgvUsers.SelectedRows.Count == 1)
						{
							icConsumer.deleteUser(num);
						}
						else
						{
							for (int i = 0; i < this.dgvUsers.SelectedRows.Count; i++)
							{
								index = this.dgvUsers.SelectedRows[i].Index;
								num = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
								icConsumer.deleteUser(num);
							}
						}
						foreach (DataGridViewRow dataGridViewRow in this.dgvUsers.SelectedRows)
						{
							this.dgvUsers.Rows.Remove(dataGridViewRow);
						}
						this.deletedUserCnt += count;
						wgAppConfig.wgLog(text, EventLogEntryType.Information, null);
						if (this.bLoadedFinished)
						{
							wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString() + "#");
						}
					}
					icConsumerShare.setUpdateLog();
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnRegisterLostCard_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvUsers.SelectedRows.Count <= 0)
				{
					if (this.dgvUsers.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvUsers.SelectedRows[0].Index;
				}
				int num = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
				if (num > 0)
				{
					using (dfrmUsersCardLost dfrmUsersCardLost = new dfrmUsersCardLost())
					{
						dfrmUsersCardLost.txtf_ConsumerName.Text = this.dgvUsers.Rows[index].Cells[2].Value.ToString();
						dfrmUsersCardLost.txtf_CardNO.Text = this.dgvUsers.Rows[index].Cells[3].Value.ToString();
						string text = this.dgvUsers.Rows[index].Cells[3].Value.ToString();
						string value = "";
						if (dfrmUsersCardLost.ShowDialog(this) == DialogResult.OK)
						{
							icConsumer icConsumer = new icConsumer();
							value = dfrmUsersCardLost.txtf_CardNONew.Text;
							if (string.IsNullOrEmpty(dfrmUsersCardLost.txtf_CardNONew.Text))
							{
								icConsumer.registerLostCard(num, 0L);
							}
							else
							{
								icConsumer.registerLostCard(num, long.Parse(dfrmUsersCardLost.txtf_CardNONew.Text.Trim()));
							}
							icConsumerShare.setUpdateLog();
							wgAppConfig.wgLog(string.Format("{0}:{1} [{2} => {3}]", new object[]
							{
								sender.ToString(),
								this.dgvUsers.Rows[index].Cells[2].Value.ToString(),
								this.dgvUsers.Rows[index].Cells[3].Value.ToString(),
								dfrmUsersCardLost.txtf_CardNONew.Text
							}), EventLogEntryType.Information, null);
							DataGridViewRow dataGridViewRow = this.dgvUsers.Rows[index];
							string text2 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
							text2 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
							text2 = text2 + " WHERE f_ConsumerID= " + num.ToString();
							if (wgAppConfig.IsAccessDB)
							{
								using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
								{
									using (OleDbCommand oleDbCommand = new OleDbCommand(text2, oleDbConnection))
									{
										oleDbConnection.Open();
										OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
										bool flag = false;
										if (oleDbDataReader.Read())
										{
											for (int i = 1; i < this.dgvUsers.Columns.Count; i++)
											{
												dataGridViewRow.Cells[i].Value = oleDbDataReader[i];
											}
											if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_DoorEnabled"])) > 0)
											{
												flag = true;
											}
										}
										oleDbDataReader.Close();
										if (flag)
										{
											text2 = " SELECT a.* ";
											text2 += " FROM t_b_Controller a, t_d_Privilege b";
											text2 = text2 + " WHERE b.f_ConsumerID= " + num.ToString();
											text2 += " AND  a.f_ControllerID = b.f_ControllerID  ";
											icPrivilege icPrivilege = new icPrivilege();
											try
											{
												using (OleDbCommand oleDbCommand2 = new OleDbCommand(text2, oleDbConnection))
												{
													oleDbDataReader = oleDbCommand2.ExecuteReader();
													ArrayList arrayList = new ArrayList();
													while (oleDbDataReader.Read())
													{
														if (arrayList.IndexOf((int)oleDbDataReader["f_ControllerID"]) < 0)
														{
															arrayList.Add((int)oleDbDataReader["f_ControllerID"]);
															if (!wgMjController.IsElevator((int)oleDbDataReader["f_ControllerSN"]))
															{
																if (!string.IsNullOrEmpty(text) && icPrivilege.DelPrivilegeOfOneCardIP((int)oleDbDataReader["f_ControllerSN"], wgTools.SetObjToStr(oleDbDataReader["f_IP"]), (int)oleDbDataReader["f_PORT"], uint.Parse(text)) < 0)
																{
																	XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																	break;
																}
																if (!string.IsNullOrEmpty(value))
																{
																	using (icController icController = new icController())
																	{
																		icController.GetInfoFromDBByControllerID((int)oleDbDataReader["f_ControllerID"]);
																		int controllerRunInformationIP = icController.GetControllerRunInformationIP();
																		if (controllerRunInformationIP <= 0)
																		{
																			XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																			return;
																		}
																		if (icController.runinfo.registerCardNum == 0u && icPrivilege.ClearAllPrivilegeIP(icController.ControllerSN, icController.IP, icController.PORT) < 0)
																		{
																			XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																			return;
																		}
																	}
																	if (icPrivilege.AddPrivilegeOfOneCardByDB((int)oleDbDataReader["f_ControllerID"], num) < 0)
																	{
																		XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																		break;
																	}
																}
															}
														}
													}
													oleDbDataReader.Close();
												}
											}
											catch (Exception ex)
											{
												wgTools.WgDebugWrite(ex.ToString(), new object[0]);
											}
											finally
											{
												icPrivilege.Dispose();
											}
										}
									}
									goto IL_835;
								}
							}
							using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
							{
								using (SqlCommand sqlCommand = new SqlCommand(text2, sqlConnection))
								{
									sqlConnection.Open();
									SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
									bool flag2 = false;
									if (sqlDataReader.Read())
									{
										for (int j = 1; j < this.dgvUsers.Columns.Count; j++)
										{
											dataGridViewRow.Cells[j].Value = sqlDataReader[j];
										}
										if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_DoorEnabled"])) > 0)
										{
											flag2 = true;
										}
									}
									sqlDataReader.Close();
									if (flag2)
									{
										text2 = " SELECT a.* ";
										text2 += " FROM t_b_Controller a, t_d_Privilege b";
										text2 = text2 + " WHERE b.f_ConsumerID= " + num.ToString();
										text2 += " AND  a.f_ControllerID = b.f_ControllerID  ";
										icPrivilege icPrivilege2 = new icPrivilege();
										try
										{
											using (SqlCommand sqlCommand2 = new SqlCommand(text2, sqlConnection))
											{
												sqlDataReader = sqlCommand2.ExecuteReader();
												ArrayList arrayList2 = new ArrayList();
												while (sqlDataReader.Read())
												{
													if (arrayList2.IndexOf((int)sqlDataReader["f_ControllerID"]) < 0)
													{
														arrayList2.Add((int)sqlDataReader["f_ControllerID"]);
														if (!wgMjController.IsElevator((int)sqlDataReader["f_ControllerSN"]))
														{
															if (!string.IsNullOrEmpty(text) && icPrivilege2.DelPrivilegeOfOneCardIP((int)sqlDataReader["f_ControllerSN"], wgTools.SetObjToStr(sqlDataReader["f_IP"]), (int)sqlDataReader["f_PORT"], uint.Parse(text)) < 0)
															{
																XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																break;
															}
															if (!string.IsNullOrEmpty(value))
															{
																using (icController icController2 = new icController())
																{
																	icController2.GetInfoFromDBByControllerID((int)sqlDataReader["f_ControllerID"]);
																	int controllerRunInformationIP2 = icController2.GetControllerRunInformationIP();
																	if (controllerRunInformationIP2 <= 0)
																	{
																		XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																		return;
																	}
																	if (icController2.runinfo.registerCardNum == 0u && icPrivilege2.ClearAllPrivilegeIP(icController2.ControllerSN, icController2.IP, icController2.PORT) < 0)
																	{
																		XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																		return;
																	}
																}
																if (icPrivilege2.AddPrivilegeOfOneCardByDB((int)sqlDataReader["f_ControllerID"], num) < 0)
																{
																	XMessageBox.Show(CommonStr.strDelAddAndUploadFail, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																	break;
																}
															}
														}
													}
												}
												sqlDataReader.Close();
											}
										}
										catch (Exception ex2)
										{
											wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
										}
										finally
										{
											icPrivilege2.Dispose();
										}
									}
								}
							}
						}
						IL_835:;
					}
				}
			}
			catch (Exception ex3)
			{
				wgAppConfig.wgLog(ex3.ToString());
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcelSpecial(ref this.dgvUsers, this.Text, this.bLoadedFinished, ref this.backgroundWorker1, ref this.startRecordIndex, this.MaxRecord, this.dgvSql);
		}

		private void fillDgv(DataView dv)
		{
			try
			{
				DataGridView dataGridView = this.dgvUsers;
				if (dataGridView.DataSource == null)
				{
					dataGridView.DataSource = dv;
					for (int i = 0; i < dv.Table.Columns.Count; i++)
					{
						dataGridView.Columns[i].DataPropertyName = dv.Table.Columns[i].ColumnName;
						dataGridView.Columns[i].Name = dv.Table.Columns[i].ColumnName;
					}
					wgAppConfig.setDisplayFormatDate(dataGridView, "f_BeginYMD", wgTools.DisplayFormat_DateYMD);
					wgAppConfig.setDisplayFormatDate(dataGridView, "f_EndYMD", wgTools.DisplayFormat_DateYMD);
					wgAppConfig.ReadGVStyle(this, dataGridView);
					if (this.startRecordIndex == 0 && dv.Count >= this.MaxRecord)
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
				else if (dv.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = dataGridView.FirstDisplayedScrollingRowIndex;
					DataView dataView = dataGridView.DataSource as DataView;
					dataView.Table.Merge(dv.Table);
					if (firstDisplayedScrollingRowIndex >= 0)
					{
						dataGridView.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
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
				if (e.NewValue > e.OldValue && (e.NewValue + 100 + this.deletedUserCnt > dataGridView.Rows.Count + this.deletedUserCnt || e.NewValue + this.deletedUserCnt + (dataGridView.Rows.Count + this.deletedUserCnt) / 10 > dataGridView.Rows.Count + this.deletedUserCnt))
				{
					if (this.startRecordIndex <= dataGridView.Rows.Count + this.deletedUserCnt)
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

		private void btnImportFromExcel_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				XMessageBox.Show(wgAppConfig.ReplaceWorkNO(wgAppConfig.ReplaceFloorRomm(CommonStr.strImportInformation)));
				this.openFileDialog1.Filter = " (*.xls)|*.xls| (*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				try
				{
					this.openFileDialog1.InitialDirectory = ".\\REPORT";
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
				}
				this.openFileDialog1.Title = this.btnImportFromExcel.Text;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = this.openFileDialog1.FileName;
					this.dfrmWait1.Show();
					this.dfrmWait1.Refresh();
					wgTools.WriteLine("start");
					int num = 0;
					int num2 = 1;
					int num3 = 2;
					int num4 = 3;
					int num5 = -1;
					this.MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source= " + fileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE'");
					string format = "Provider=Microsoft.{0}.OLEDB.{1};Data Source={2};Extended Properties=\"Excel {3};HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"";
					if (!(fileName == string.Empty))
					{
						FileInfo fileInfo = new FileInfo(fileName);
						if (fileInfo.Extension.Equals(".xls"))
						{
							this.MyConnection = new OleDbConnection(string.Format(format, new object[]
							{
								"Jet",
								"4.0",
								fileName,
								"8.0"
							}));
						}
						else if (fileInfo.Extension.Equals(".xlsx"))
						{
							this.MyConnection = new OleDbConnection(string.Format(format, new object[]
							{
								"Ace",
								"12.0",
								fileName,
								"12.0"
							}));
						}
						else
						{
							this.MyConnection = new OleDbConnection(string.Format(format, new object[]
							{
								"Jet",
								"4.0",
								fileName,
								"8.0"
							}));
						}
					}
					this.DS = new DataSet();
					this.MyConnection.Open();
					DataTable oleDbSchemaTable = this.MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					this.MyConnection.Close();
					string text = "";
					if (oleDbSchemaTable.Rows.Count <= 0)
					{
						XMessageBox.Show(this.btnImportFromExcel.Text + ": " + 0);
					}
					else
					{
						text = wgTools.SetObjToStr(oleDbSchemaTable.Rows[0][2]);
						for (int i = 0; i <= oleDbSchemaTable.Rows.Count - 1; i++)
						{
							if (wgTools.SetObjToStr(oleDbSchemaTable.Rows[i][2]) == "用户" || wgTools.SetObjToStr(oleDbSchemaTable.Rows[i][2]) == "用戶" || wgTools.SetObjToStr(oleDbSchemaTable.Rows[i][2]) == "Users")
							{
								text = wgTools.SetObjToStr(oleDbSchemaTable.Rows[i][2]);
								break;
							}
						}
						num = -1;
						num2 = -1;
						num3 = -1;
						num4 = -1;
						num5 = -1;
						if (text.IndexOf("$") <= 0)
						{
							text += "$";
						}
						try
						{
							this.MyCommand = new OleDbDataAdapter("select * from [" + text + "A1:Z1]", this.MyConnection);
							this.MyCommand.Fill(this.DS, "userInfoTitle");
							string arg_3B8_0 = this.DS.Tables["userInfoTitle"].Columns[0].ColumnName;
							for (int j = 0; j <= this.DS.Tables["userInfoTitle"].Columns.Count - 1; j++)
							{
								object columnName = this.DS.Tables["userInfoTitle"].Columns[j].ColumnName;
								if (wgTools.SetObjToStr(columnName) != "")
								{
									string key;
									if (wgTools.SetObjToStr(columnName).ToUpper() == "User ID".ToUpper())
									{
										num = j;
									}
									else if (wgTools.SetObjToStr(columnName).ToUpper() == "User Name".ToUpper())
									{
										num2 = j;
									}
									else if (wgTools.SetObjToStr(columnName).ToUpper() == "Card NO".ToUpper())
									{
										num3 = j;
									}
									else if (wgTools.SetObjToStr(columnName).ToUpper() == "Department".ToUpper() || wgTools.SetObjToStr(columnName).ToUpper() == CommonStr.strReplaceFloorRoom.ToUpper())
									{
										num4 = j;
									}
									else
										switch (key = wgTools.SetObjToStr(columnName).ToUpper().Substring(0, 2))
										{
										case "NO":
										case "用户":
										case "用戶":
										case "编号":
										case "編號":
										case "WO":
										case "工号":
										case "工號":
											num = j;
											break;
										case "NA":
										case "姓名":
											num2 = j;
											break;
										case "CA":
										case "卡号":
										case "卡號":
											num3 = j;
											break;
										case "DE":
										case "部门":
										case "部門":
											num4 = j;
											break;
										}
								}
							}
						}
						catch (Exception ex2)
						{
							wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
							wgAppConfig.wgLog(ex2.ToString());
						}
						if (num2 < 0)
						{
							XMessageBox.Show(CommonStr.strWrongUsersFile);
						}
						else
						{
							string text2 = "";
							int num7 = 0;
							try
							{
								int num8 = Math.Max(num, num2);
								num8 = Math.Max(num8, num3);
								num8 = Math.Max(num8, num4);
								string text3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
								if (num8 < text3.Length)
								{
									this.MyCommand = new OleDbDataAdapter(string.Concat(new string[]
									{
										"select * from [",
										text,
										"A1:",
										text3.Substring(num8, 1),
										"65535]"
									}), this.MyConnection);
									this.MyCommand.Fill(this.DS, "userInfo");
								}
							}
							catch (Exception ex3)
							{
								wgTools.WgDebugWrite(ex3.ToString(), new object[0]);
								wgAppConfig.wgLog(ex3.ToString());
							}
							this.dv = new DataView(this.DS.Tables["userInfo"]);
							int num9 = 0;
							icConsumer icConsumer = new icConsumer();
							long num10 = icConsumer.ConsumerNONext();
							long num11 = num10;
							if (num10 < 0L)
							{
								num11 = 1L;
							}
							string text4 = "";
							new icConsumer();
							for (int k = 0; k <= this.dv.Count - 1; k++)
							{
								string text5 = wgTools.SetObjToStr(this.dv[k][num2]).Trim();
								int num12;
								if (text5 != "" && !(text4 == "") && int.TryParse(text4, out num12))
								{
									num11 = Math.Max(num11, (long)(int.Parse(text4) + 1));
								}
							}
							for (int l = 0; l <= this.dv.Count - 1; l++)
							{
								string text6 = "";
								text4 = "";
								string a = "";
								string text7 = "";
								string text5 = wgTools.SetObjToStr(this.dv[l][num2]).Trim();
								if (num >= 0)
								{
									text4 = wgTools.SetObjToStr(this.dv[l][num]).Trim();
								}
								if (num5 >= 0)
								{
									a = wgTools.SetObjToStr(this.dv[l][num5]).Trim();
								}
								if (num3 >= 0)
								{
									text6 = wgTools.SetObjToStr(this.dv[l][num3]).Trim();
								}
								if (num4 >= 0)
								{
									text7 = wgTools.SetObjToStr(this.dv[l][num4]).Trim();
								}
								if (text5 != "")
								{
									int num13;
									if (text4 == "")
									{
										text4 = num11.ToString();
									}
									else if (int.TryParse(text4, out num13))
									{
										num11 = Math.Max(num11, (long)int.Parse(text4));
									}
									if (this.addConsumerNew(text4, text5, text6, text7) > 0)
									{
										num9++;
										num11 += 1L;
									}
									else
									{
										text2 = text2 + text5 + ",";
										num7++;
									}
								}
								else
								{
									if (!(text4 == "") || !(text5 == "") || !(text6 == "") || !(text7 == "") || !(a == ""))
									{
										text2 = string.Concat(new object[]
										{
											text2,
											"L",
											l + 1,
											","
										});
										num7++;
									}
									if (text2.Length > 500)
									{
										break;
									}
								}
								if (l >= 65535)
								{
									break;
								}
								wgAppRunInfo.raiseAppRunInfoLoadNums((num7 + num9).ToString() + " / " + this.dv.Count.ToString());
								this.dfrmWait1.Text = (num7 + num9).ToString() + " / " + this.dv.Count.ToString();
								Application.DoEvents();
							}
							wgAppRunInfo.raiseAppRunInfoLoadNums((num7 + num9).ToString() + " / " + this.dv.Count.ToString());
							this.dfrmWait1.Text = (num7 + num9).ToString() + " / " + this.dv.Count.ToString();
							icGroup icGroup = new icGroup();
							icGroup.updateGroupNO();
							wgTools.WriteLine("Import end");
							if (!(text2 == ""))
							{
								this.dfrmWait1.Hide();
								wgTools.WgDebugWrite(CommonStr.strNotImportedUsers + num7.ToString() + "\r\n" + text2, new object[0]);
								XMessageBox.Show(CommonStr.strNotImportedUsers + num7.ToString() + "\r\n\r\n" + text2);
								wgAppConfig.wgLog(CommonStr.strNotImportedUsers + num7.ToString() + "\r\n" + text2);
							}
							wgAppConfig.wgLog(this.btnImportFromExcel.Text + ": " + num9);
							icConsumerShare.setUpdateLog();
							XMessageBox.Show(this.btnImportFromExcel.Text + ": " + num9);
							this.reloadUserData("");
						}
					}
				}
			}
			catch (Exception ex4)
			{
				wgTools.WgDebugWrite(ex4.ToString(), new object[0]);
				wgAppConfig.wgLog(ex4.ToString());
			}
			finally
			{
				Directory.SetCurrentDirectory(Application.StartupPath);
				Cursor.Current = Cursors.Default;
				this.dfrmWait1.Hide();
			}
		}

		public int addConsumerNew(string no, string name, string strCard, string dept)
		{
			icConsumer icConsumer = new icConsumer();
			if (strCard != "")
			{
				bool flag = false;
				long num;
				if (long.TryParse(strCard, out num))
				{
					if (strCard.ToUpper().IndexOf("E") >= 0)
					{
						return -1;
					}
					long num2 = long.Parse(strCard);
					strCard = num2.ToString();
					if (num2 <= 0L)
					{
						return -1;
					}
					if (icConsumer.isExisted(long.Parse(strCard)))
					{
						return -1;
					}
					flag = true;
				}
				if (!flag)
				{
					return -1;
				}
			}
			int result;
			if (this._addConsumer4Import(no, name, strCard, dept))
			{
				result = 1;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		private bool _addConsumer4Import(string no, string name, string strCard, string dept)
		{
			long cardNO = 0L;
			icConsumer icConsumer = new icConsumer();
			if (!string.IsNullOrEmpty(strCard))
			{
				long.TryParse(strCard, out cardNO);
			}
			bool result;
			if (string.IsNullOrEmpty(dept))
			{
				result = (icConsumer.addNew(no, name, cardNO, 0) >= 0);
			}
			else
			{
				result = (icConsumer.addNew(no, name, cardNO, this.getDeptId(dept)) >= 0);
			}
			return result;
		}

		private int getDeptId(string deptName)
		{
			icGroup icGroup = new icGroup();
			int groupID = icGroup.getGroupID(deptName);
			if (groupID > 0)
			{
				return groupID;
			}
			string[] array = deptName.Split(new char[]
			{
				'\\'
			});
			string text = "";
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (text == "")
				{
					text = array[i];
				}
				else
				{
					text = text + "\\" + array[i];
				}
				if (flag || !icGroup.checkExisted(text))
				{
					flag = true;
					icGroup.addNew4BatchExcel(text);
				}
			}
			return icGroup.getGroupID(deptName);
		}

		public void startWatch()
		{
			if (this.watching == null)
			{
				this.watching = new WatchingService();
			}
		}

		public void stopWatch()
		{
			if (this.watching != null)
			{
				this.watching.StopWatch();
			}
		}

		private void btnAutoAdd_Click(object sender, EventArgs e)
		{
			using (dfrmUserAutoAdd dfrmUserAutoAdd = new dfrmUserAutoAdd())
			{
				dfrmUserAutoAdd.watching = this.watching;
				dfrmUserAutoAdd.frmCall = this;
				if (dfrmUserAutoAdd.ShowDialog(this) == DialogResult.OK)
				{
					this.reloadUserData("");
				}
			}
		}

		private void btnBatchUpdate_Click(object sender, EventArgs e)
		{
			using (dfrmUserBatchUpdate dfrmUserBatchUpdate = new dfrmUserBatchUpdate())
			{
				if (dfrmUserBatchUpdate.ShowDialog(this) == DialogResult.OK)
				{
					this.reloadUserData("");
				}
			}
		}

		private void frmUsers_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.watching != null)
			{
				this.watching.StopWatch();
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
						return;
					}
					index = this.dgvUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvUsers.SelectedRows[0].Index;
				}
				using (dfrmPrivilegeSingle dfrmPrivilegeSingle = new dfrmPrivilegeSingle())
				{
					dfrmPrivilegeSingle.consumerID = int.Parse(this.dgvUsers.Rows[index].Cells[0].Value.ToString());
					dfrmPrivilegeSingle.Text = string.Concat(new string[]
					{
						this.dgvUsers.Rows[index].Cells[1].Value.ToString().Trim(),
						".",
						this.dgvUsers.Rows[index].Cells[2].Value.ToString().Trim(),
						" -- ",
						dfrmPrivilegeSingle.Text
					});
					dfrmPrivilegeSingle.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void funcCtrlShiftQ()
		{
			this.btnImportFromExcel.Visible = true;
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
				this.funcCtrlShiftQ();
			}
		}

		private void frmUsers_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.stopWatch();
		}

		private void batchUpdateSelectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvUsers.SelectedRows.Count <= 0)
			{
				return;
			}
			using (dfrmUserBatchUpdate dfrmUserBatchUpdate = new dfrmUserBatchUpdate())
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
				dfrmUserBatchUpdate.strSqlSelected = text;
				dfrmUserBatchUpdate.Text = string.Format("{0}: [{1}]", this.batchUpdateSelectToolStripMenuItem.Text, this.dgvUsers.SelectedRows.Count.ToString());
				if (dfrmUserBatchUpdate.ShowDialog(this) == DialogResult.OK)
				{
					this.reloadUserData("");
				}
			}
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
				this.deletedUserCnt = 0;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				string text2 = "";
				long num4 = 0L;
				int num5 = 0;
				this.userControlFind1.txtFindCardID.Text = "";
				this.userControlFind1.txtFindName.Text = "";
				this.userControlFind1.getSqlInfo(ref num, ref num2, ref num3, ref text2, ref num4, ref num5);
				string text3 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
				text3 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
				if (num5 > 0)
				{
					text3 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
					text3 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
					text3 = text3 + " WHERE  t_b_Consumer.f_ConsumerID = " + num5.ToString();
				}
				else if (num > 0)
				{
					text3 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
					if (num >= num3)
					{
						text3 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
						text3 += string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", num2);
					}
					else
					{
						text3 += " FROM t_b_Consumer,t_b_Group  WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
						text3 += string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", num);
						text3 += string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", num3);
					}
					if (text2 != "")
					{
						text3 += string.Format(" AND f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
					}
					else if (num4 > 0L)
					{
						text3 += string.Format(" AND f_CardNO ={0:d} ", num4);
					}
				}
				else if (text2 != "")
				{
					text3 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
					text3 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
					text3 += string.Format(" WHERE f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", text2)));
				}
				else if (num4 > 0L)
				{
					text3 = " SELECT    f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, f_AttendEnabled, f_ShiftEnabled, f_DoorEnabled, f_BeginYMD, f_EndYMD, f_GroupName ";
					text3 += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ";
					text3 += string.Format(" WHERE f_CardNO ={0:d} ", num4);
				}
				string text4 = " ( 1>0 ) ";
				if (text.IndexOf("%") < 0)
				{
					text = string.Format("%{0}%", text);
				}
				if (wgAppConfig.IsAccessDB)
				{
					text4 += string.Format(" AND CSTR(f_CardNO) like {0} ", wgTools.PrepareStr(text));
				}
				else
				{
					text4 += string.Format(" AND f_CardNO like {0} ", wgTools.PrepareStr(text));
				}
				if (text3.ToUpper().IndexOf("WHERE") > 0)
				{
					text3 += string.Format(" AND {0} ", text4);
				}
				else
				{
					text3 += string.Format(" WHERE {0} ", text4);
				}
				this.reloadUserData(text3);
				return;
			}
		}

		private void displayAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.bLoadedFinished)
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this.startRecordIndex <= this.dgvUsers.Rows.Count)
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
					wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString() + "#");
				}
			}
		}
	}
}
