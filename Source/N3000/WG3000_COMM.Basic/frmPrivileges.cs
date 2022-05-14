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
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class frmPrivileges : Form
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private DataGridView dgvPrivileges;

		private ToolStripButton btnEdit;

		private ToolStripButton btnPrint;

		private ToolStripButton btnExport;

		private ToolStrip toolStrip2;

		private ToolStripLabel toolStripLabel1;

		private ToolStripComboBox cboDoor;

		private UserControlFind userControlFind1;

		private BackgroundWorker backgroundWorker1;

		private ToolStripButton btnEditSinglePrivilege;

		private ToolStripButton btnPrivilegeCopy;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_DoorName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_CardNO;

		private DataGridViewTextBoxColumn f_ControlSeg;

		private DataGridViewTextBoxColumn f_ControlSegName;

		private DataGridViewTextBoxColumn f_ConsumerID;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem cardNOFuzzyQueryToolStripMenuItem;

		private ToolStripMenuItem displayAllToolStripMenuItem;

		private int[] controlSegIDList = new int[256];

		private string[] controlSegNameList = new string[256];

		private ArrayList arrControllerAllEnabled;

		private ArrayList arrController;

		private string strSqlAllPrivileg = " SELECT  t_d_Privilege.f_PrivilegeRecID,d.f_DoorName, c.f_ConsumerNO, c.f_ConsumerName, c.f_CardNO,  t_d_Privilege.f_ControlSegID,' ' as  f_ControlSegName, t_d_Privilege.f_ConsumerID  FROM ((t_d_Privilege  INNER JOIN t_b_Consumer c ON t_d_Privilege.f_ConsumerID=c.f_ConsumerID)   INNER JOIN t_b_Door d ON t_d_Privilege.f_DoorID=d.f_DoorID) ";

		private string strAllPrivilegsNum = "";

		private int iAllPrivilegsNum;

		private DataView dvDoors;

		private DataView dvDoors4Watching;

		private DataTable dt;

		private bool bLoadedFinished;

		private int recIdMax;

		private int iSelectedControllerIndex = -1;

		private int iSelectedControllerIndexLast = -1;

		private DataTable dtData;

		private SqlConnection cn;

		private SqlCommand cmd;

		private SqlDataAdapter da;

		private OleDbConnection cn_Acc;

		private OleDbCommand cmd_Acc;

		private OleDbDataAdapter da_Acc;

		private string dgvSql;

		private int startRecordIndex;

		private int MaxRecord = 1000;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPrivileges));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.dgvPrivileges = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_DoorName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_CardNO = new DataGridViewTextBoxColumn();
			this.f_ControlSeg = new DataGridViewTextBoxColumn();
			this.f_ControlSegName = new DataGridViewTextBoxColumn();
			this.f_ConsumerID = new DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.cardNOFuzzyQueryToolStripMenuItem = new ToolStripMenuItem();
			this.displayAllToolStripMenuItem = new ToolStripMenuItem();
			this.backgroundWorker1 = new BackgroundWorker();
			this.toolStrip2 = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.cboDoor = new ToolStripComboBox();
			this.toolStrip1 = new ToolStrip();
			this.btnEdit = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExport = new ToolStripButton();
			this.btnPrivilegeCopy = new ToolStripButton();
			this.btnEditSinglePrivilege = new ToolStripButton();
			this.userControlFind1 = new UserControlFind();
			((ISupportInitialize)this.dgvPrivileges).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.dgvPrivileges, "dgvPrivileges");
			this.dgvPrivileges.AllowUserToAddRows = false;
			this.dgvPrivileges.AllowUserToDeleteRows = false;
			this.dgvPrivileges.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvPrivileges.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvPrivileges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvPrivileges.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_RecID,
				this.f_DoorName,
				this.f_ConsumerNO,
				this.f_ConsumerName,
				this.f_CardNO,
				this.f_ControlSeg,
				this.f_ControlSegName,
				this.f_ConsumerID
			});
			this.dgvPrivileges.ContextMenuStrip = this.contextMenuStrip1;
			this.dgvPrivileges.EnableHeadersVisualStyles = false;
			this.dgvPrivileges.Name = "dgvPrivileges";
			this.dgvPrivileges.ReadOnly = true;
			this.dgvPrivileges.RowTemplate.Height = 23;
			this.dgvPrivileges.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvPrivileges.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvPrivileges_CellFormatting);
			this.dgvPrivileges.Scroll += new ScrollEventHandler(this.dgvPrivileges_Scroll);
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DoorName, "f_DoorName");
			this.f_DoorName.Name = "f_DoorName";
			this.f_DoorName.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_ConsumerNO, "f_ConsumerNO");
			this.f_ConsumerNO.Name = "f_ConsumerNO";
			this.f_ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_CardNO, "f_CardNO");
			this.f_CardNO.Name = "f_CardNO";
			this.f_CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControlSeg, "f_ControlSeg");
			this.f_ControlSeg.Name = "f_ControlSeg";
			this.f_ControlSeg.ReadOnly = true;
			this.f_ControlSegName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_ControlSegName, "f_ControlSegName");
			this.f_ControlSegName.Name = "f_ControlSegName";
			this.f_ControlSegName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerID, "f_ConsumerID");
			this.f_ConsumerID.Name = "f_ConsumerID";
			this.f_ConsumerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.cardNOFuzzyQueryToolStripMenuItem,
				this.displayAllToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.cardNOFuzzyQueryToolStripMenuItem, "cardNOFuzzyQueryToolStripMenuItem");
			this.cardNOFuzzyQueryToolStripMenuItem.Name = "cardNOFuzzyQueryToolStripMenuItem";
			this.cardNOFuzzyQueryToolStripMenuItem.Click += new EventHandler(this.cardNOFuzzyQueryToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.displayAllToolStripMenuItem, "displayAllToolStripMenuItem");
			this.displayAllToolStripMenuItem.Name = "displayAllToolStripMenuItem";
			this.displayAllToolStripMenuItem.Click += new EventHandler(this.displayAllToolStripMenuItem_Click);
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.BackColor = Color.Transparent;
			this.toolStrip2.BackgroundImage = Resources.pTools_second_title;
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.cboDoor
			});
			this.toolStrip2.Name = "toolStrip2";
			componentResourceManager.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			this.toolStripLabel1.ForeColor = Color.White;
			this.toolStripLabel1.Name = "toolStripLabel1";
			componentResourceManager.ApplyResources(this.cboDoor, "cboDoor");
			this.cboDoor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboDoor.Name = "cboDoor";
			this.cboDoor.KeyPress += new KeyPressEventHandler(this.cboDoor_KeyPress);
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnEdit,
				this.btnPrint,
				this.btnExport,
				this.btnPrivilegeCopy,
				this.btnEditSinglePrivilege
			});
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Image = Resources.pTools_ChangePrivilege;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
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
			componentResourceManager.ApplyResources(this.btnPrivilegeCopy, "btnPrivilegeCopy");
			this.btnPrivilegeCopy.ForeColor = Color.White;
			this.btnPrivilegeCopy.Image = Resources.pTools_CopyPrivilege;
			this.btnPrivilegeCopy.Name = "btnPrivilegeCopy";
			this.btnPrivilegeCopy.Click += new EventHandler(this.btnPrivilegeCopy_Click);
			componentResourceManager.ApplyResources(this.btnEditSinglePrivilege, "btnEditSinglePrivilege");
			this.btnEditSinglePrivilege.ForeColor = Color.White;
			this.btnEditSinglePrivilege.Image = Resources.pTools_EditPrivielge;
			this.btnEditSinglePrivilege.Name = "btnEditSinglePrivilege";
			this.btnEditSinglePrivilege.Click += new EventHandler(this.btnEditSinglePrivilege_Click);
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.Name = "userControlFind1";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvPrivileges);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			this.DoubleBuffered = true;
			base.Name = "frmPrivileges";
			base.FormClosing += new FormClosingEventHandler(this.frmPrivileges_FormClosing);
			base.Load += new EventHandler(this.frmPrivileges_Load);
			((ISupportInitialize)this.dgvPrivileges).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmPrivileges()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void loadControlSegData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadControlSegData_Acc();
				return;
			}
			this.controlSegNameList[0] = CommonStr.strFreeTime;
			this.controlSegIDList[0] = 1;
			string text = " SELECT ";
			text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
			text += "   CASE WHEN [f_ControlSegName] IS NULL THEN CONVERT(nvarchar(50),  ";
			text += "     ([t_b_ControlSeg].[f_ControlSegID])) ELSE (CONVERT(nvarchar(50), ";
			text += "     ([t_b_ControlSeg].[f_ControlSegID])) + ' [' + [f_ControlSegName] + ']') ";
			text += "    END AS f_ControlSegID  ";
			text += "  FROM [t_b_ControlSeg] WHERE  [t_b_ControlSeg].[f_ControlSegID]>1 ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					int num = 1;
					while (sqlDataReader.Read())
					{
						this.controlSegNameList[num] = (string)sqlDataReader["f_ControlSegID"];
						this.controlSegIDList[num] = (int)sqlDataReader["f_ControlSegIDBak"];
						num++;
					}
					sqlDataReader.Close();
				}
			}
		}

		private void loadControlSegData_Acc()
		{
			this.controlSegNameList[0] = CommonStr.strFreeTime;
			this.controlSegIDList[0] = 1;
			string text = " SELECT ";
			text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
			text += "  IIF(ISNULL([f_ControlSegName]), CSTR([t_b_ControlSeg].[f_ControlSegID]), CSTR([t_b_ControlSeg].[f_ControlSegID]) & ' [' & [f_ControlSegName] & ']') AS f_ControlSegID ";
			text += "  FROM [t_b_ControlSeg]  WHERE  [t_b_ControlSeg].[f_ControlSegID]>1  ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					int num = 1;
					while (oleDbDataReader.Read())
					{
						this.controlSegNameList[num] = (string)oleDbDataReader["f_ControlSegID"];
						this.controlSegIDList[num] = (int)oleDbDataReader["f_ControlSegIDBak"];
						num++;
					}
					oleDbDataReader.Close();
				}
			}
		}

		private void loadControllerData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadControllerData_Acc();
				return;
			}
			string cmdText = " SELECT f_ControllerID   FROM [t_b_Controller] WHERE f_Enabled > 0";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					this.arrControllerAllEnabled = new ArrayList();
					this.arrController = new ArrayList();
					while (sqlDataReader.Read())
					{
						this.arrControllerAllEnabled.Add(sqlDataReader[0]);
						this.arrController.Add(sqlDataReader[0]);
					}
					sqlDataReader.Close();
				}
			}
		}

		private void loadControllerData_Acc()
		{
			string cmdText = " SELECT f_ControllerID   FROM [t_b_Controller] WHERE f_Enabled > 0";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					this.arrControllerAllEnabled = new ArrayList();
					this.arrController = new ArrayList();
					while (oleDbDataReader.Read())
					{
						this.arrControllerAllEnabled.Add(oleDbDataReader[0]);
						this.arrController.Add(oleDbDataReader[0]);
					}
					oleDbDataReader.Close();
				}
			}
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuPrivilege";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnEditSinglePrivilege.Visible = false;
				this.btnEdit.Visible = false;
				this.btnPrivilegeCopy.Visible = false;
			}
		}

		private void frmPrivileges_Load(object sender, EventArgs e)
		{
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.loadOperatorPrivilege();
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.userControlFind1.btnClear.Click += new EventHandler(this.btnClear_Click);
			this.loadStyle();
			this.loadControlSegData();
			this.loadDoorData();
			this.loadControllerData();
			icControllerZone icControllerZone = new icControllerZone();
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			icControllerZone.getZone(ref arrayList, ref arrayList2, ref arrayList3);
			if (arrayList2.Count > 0 && (int)arrayList2[0] != 0)
			{
				this.btnEditSinglePrivilege.Enabled = false;
			}
			if (!wgAppConfig.getParamValBoolByNO(142))
			{
				this.userControlFind1.btnQuery.PerformClick();
			}
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, a.f_ControllerID , b.f_ZoneID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			this.dt = new DataTable();
			this.dvDoors = new DataView(this.dt);
			this.dvDoors4Watching = new DataView(this.dt);
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
					goto IL_F4;
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
			IL_F4:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dt.Columns[0]
				};
				this.dt.PrimaryKey = primaryKey;
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			this.cboDoor.Items.Clear();
			this.cboDoor.Items.Add("");
			if (this.dvDoors.Count > 0)
			{
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					this.cboDoor.Items.Add(wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]));
				}
			}
		}

		private void loadStyle()
		{
			this.dgvPrivileges.AutoGenerateColumns = false;
			this.dgvPrivileges.Columns[5].Visible = wgAppConfig.getParamValBoolByNO(121);
			this.dgvPrivileges.Columns[6].Visible = wgAppConfig.getParamValBoolByNO(121);
			wgAppConfig.ReadGVStyle(this, this.dgvPrivileges);
		}

		private void reloadData(string strsql)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.reloadData_Acc(strsql);
				return;
			}
			if (this.backgroundWorker1.IsBusy)
			{
				return;
			}
			if (this.arrController.Count <= 0)
			{
				return;
			}
			this.bLoadedFinished = false;
			this.iAllPrivilegsNum = 0;
			this.iSelectedControllerIndex = 0;
			this.strAllPrivilegsNum = "";
			if (this.strSqlAllPrivileg == strsql)
			{
				try
				{
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						if (sqlConnection.State != ConnectionState.Open)
						{
							sqlConnection.Open();
						}
						SqlCommand sqlCommand = null;
						try
						{
							if (wgAppConfig.getSystemParamByNO(53) == "1")
							{
								sqlCommand = new SqlCommand("SELECT SUM(row_count)  FROM sys.dm_db_partition_stats WHERE object_id = OBJECT_ID('t_d_privilege') AND index_id =1 ", sqlConnection);
								this.iAllPrivilegsNum = int.Parse(sqlCommand.ExecuteScalar().ToString());
								this.strAllPrivilegsNum = "/" + this.iAllPrivilegsNum;
							}
							else
							{
								sqlCommand = new SqlCommand("select rowcnt from sysindexes where id=object_id(N't_d_Privilege') and name = N'PK_t_d_Privilege'", sqlConnection);
								this.iAllPrivilegsNum = int.Parse(sqlCommand.ExecuteScalar().ToString());
								if (this.iAllPrivilegsNum <= 2000000)
								{
									using (SqlCommand sqlCommand2 = new SqlCommand("select count(1) from t_d_Privilege", sqlConnection))
									{
										this.iAllPrivilegsNum = int.Parse(sqlCommand2.ExecuteScalar().ToString());
										this.strAllPrivilegsNum = "/" + this.iAllPrivilegsNum;
									}
								}
							}
						}
						catch (Exception)
						{
						}
						finally
						{
							if (sqlCommand != null)
							{
								sqlCommand.Dispose();
							}
						}
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.strAllPrivilegsNum);
			this.startRecordIndex = 0;
			this.MaxRecord = 1000;
			if (!string.IsNullOrEmpty(strsql))
			{
				this.dgvSql = strsql;
			}
			this.dgvPrivileges.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void reloadData_Acc(string strsql)
		{
			if (this.backgroundWorker1.IsBusy)
			{
				return;
			}
			if (this.arrController.Count <= 0)
			{
				return;
			}
			this.bLoadedFinished = false;
			this.iAllPrivilegsNum = 0;
			this.iSelectedControllerIndex = 0;
			this.strAllPrivilegsNum = "";
			if (this.strSqlAllPrivileg == strsql)
			{
				try
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						if (oleDbConnection.State != ConnectionState.Open)
						{
							oleDbConnection.Open();
						}
						using (null)
						{
							try
							{
								using (OleDbCommand oleDbCommand2 = new OleDbCommand("select count(1) from t_d_Privilege", oleDbConnection))
								{
									this.iAllPrivilegsNum = int.Parse(oleDbCommand2.ExecuteScalar().ToString());
									this.strAllPrivilegsNum = "/" + this.iAllPrivilegsNum;
								}
							}
							catch (Exception)
							{
							}
						}
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.strAllPrivilegsNum);
			this.startRecordIndex = 0;
			this.MaxRecord = 1000;
			if (!string.IsNullOrEmpty(strsql))
			{
				this.dgvSql = strsql;
			}
			this.dgvPrivileges.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private DataView loadData(int startIndex, int maxRecords, string strSqlpar)
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.loadData_Acc(startIndex, maxRecords, strSqlpar);
			}
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("load Privileges Data Start");
			int num = this.iSelectedControllerIndex;
			if (this.cn != null)
			{
				this.cn.Dispose();
			}
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			if (this.cmd != null)
			{
				this.cmd.Dispose();
			}
			this.cmd = new SqlCommand("", this.cn);
			if (this.da != null)
			{
				this.da.Dispose();
			}
			this.da = new SqlDataAdapter();
			this.cmd.CommandTimeout = wgAppConfig.dbCommandTimeout;
			this.dtData = new DataTable();
			while (true)
			{
				string text;
				if (this.iSelectedControllerIndex == 0 && this.arrController.Count > 1)
				{
					text = "SELECT TOP 1 f_ControllerID FROM t_d_Privilege order by f_ControllerID";
					this.cmd.CommandText = text;
					if (this.cn.State != ConnectionState.Open)
					{
						this.cn.Open();
					}
					object obj = this.cmd.ExecuteScalar();
					if (obj == null)
					{
						break;
					}
					this.iSelectedControllerIndex = this.arrController.IndexOf((int)obj);
					if (this.iSelectedControllerIndex < 0)
					{
						this.iSelectedControllerIndex = 0;
					}
				}
				text = strSqlpar;
				if (text.ToUpper().IndexOf("SELECT ") > 0)
				{
					text = string.Format("SELECT TOP {0:d} ", maxRecords) + text.Substring(text.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
				}
				if (this.iSelectedControllerIndexLast != this.iSelectedControllerIndex)
				{
					this.recIdMax = -2147483648;
					this.iSelectedControllerIndexLast = this.iSelectedControllerIndex;
				}
				else if (startIndex == 0)
				{
					this.recIdMax = -2147483648;
				}
				else if (text.ToUpper().IndexOf(" WHERE ") > 0)
				{
					text += string.Format(" AND f_PrivilegeRecID > {0:d}", this.recIdMax);
				}
				else
				{
					text += string.Format(" WHERE f_PrivilegeRecID > {0:d}", this.recIdMax);
				}
				if (text.ToUpper().IndexOf(" WHERE ") > 0)
				{
					text += string.Format(" AND t_d_Privilege.f_ControllerID = {0:d}", (int)this.arrController[this.iSelectedControllerIndex]);
				}
				else
				{
					text += string.Format(" WHERE t_d_Privilege.f_ControllerID = {0:d}", (int)this.arrController[this.iSelectedControllerIndex]);
				}
				text += " ORDER BY f_PrivilegeRecID ";
				this.cmd.CommandText = text;
				this.da.SelectCommand = this.cmd;
				wgTools.WriteLine("da.Fill start");
				this.da.Fill(this.dtData);
				if (this.dtData.Rows.Count > 0 && (num != 0 || this.dtData.Rows.Count >= 100))
				{
					goto IL_300;
				}
				this.iSelectedControllerIndex++;
				if (this.iSelectedControllerIndex >= this.arrController.Count)
				{
					goto IL_378;
				}
			}
			this.iSelectedControllerIndex = this.arrController.Count - 1;
			return new DataView(this.dtData);
			IL_300:
			this.recIdMax = int.Parse(this.dtData.Rows[this.dtData.Rows.Count - 1][0].ToString());
			wgTools.WriteLine(string.Format("recIdMax = {0:d}", this.recIdMax));
			IL_378:
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			wgTools.WriteLine("load Privileges Data End");
			return new DataView(this.dtData);
		}

		private DataView loadData_Acc(int startIndex, int maxRecords, string strSqlpar)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("load Privileges Data Start");
			int num = this.iSelectedControllerIndex;
			if (this.cn_Acc != null)
			{
				this.cn_Acc.Dispose();
			}
			this.cn_Acc = new OleDbConnection(wgAppConfig.dbConString);
			if (this.cmd_Acc != null)
			{
				this.cmd_Acc.Dispose();
			}
			this.cmd_Acc = new OleDbCommand("", this.cn_Acc);
			if (this.da_Acc != null)
			{
				this.da_Acc.Dispose();
			}
			this.da_Acc = new OleDbDataAdapter();
			this.cmd_Acc.CommandTimeout = wgAppConfig.dbCommandTimeout;
			this.dtData = new DataTable();
			while (true)
			{
				string text;
				if (this.iSelectedControllerIndex == 0 && this.arrController.Count > 1)
				{
					text = "SELECT TOP 1 f_ControllerID FROM t_d_Privilege order by f_ControllerID";
					this.cmd_Acc.CommandText = text;
					if (this.cn_Acc.State != ConnectionState.Open)
					{
						this.cn_Acc.Open();
					}
					object obj = this.cmd_Acc.ExecuteScalar();
					if (obj == null)
					{
						break;
					}
					this.iSelectedControllerIndex = this.arrController.IndexOf((int)obj);
					if (this.iSelectedControllerIndex < 0)
					{
						this.iSelectedControllerIndex = 0;
					}
				}
				text = strSqlpar;
				if (text.ToUpper().IndexOf("SELECT ") > 0)
				{
					text = string.Format("SELECT TOP {0:d} ", maxRecords) + text.Substring(text.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
				}
				if (this.iSelectedControllerIndexLast != this.iSelectedControllerIndex)
				{
					this.recIdMax = -2147483648;
					this.iSelectedControllerIndexLast = this.iSelectedControllerIndex;
				}
				else if (startIndex == 0)
				{
					this.recIdMax = -2147483648;
				}
				else if (text.ToUpper().IndexOf(" WHERE ") > 0)
				{
					text += string.Format(" AND f_PrivilegeRecID > {0:d}", this.recIdMax);
				}
				else
				{
					text += string.Format(" WHERE f_PrivilegeRecID > {0:d}", this.recIdMax);
				}
				if (text.ToUpper().IndexOf(" WHERE ") > 0)
				{
					text += string.Format(" AND t_d_Privilege.f_ControllerID = {0:d}", (int)this.arrController[this.iSelectedControllerIndex]);
				}
				else
				{
					text += string.Format(" WHERE t_d_Privilege.f_ControllerID = {0:d}", (int)this.arrController[this.iSelectedControllerIndex]);
				}
				text += " ORDER BY f_PrivilegeRecID ";
				this.cmd_Acc.CommandText = text;
				this.da_Acc.SelectCommand = this.cmd_Acc;
				wgTools.WriteLine("da_Acc.Fill start");
				this.da_Acc.Fill(this.dtData);
				if (this.dtData.Rows.Count > 0 && (num != 0 || this.dtData.Rows.Count >= 100))
				{
					goto IL_2EF;
				}
				this.iSelectedControllerIndex++;
				if (this.iSelectedControllerIndex >= this.arrController.Count)
				{
					goto IL_367;
				}
			}
			this.iSelectedControllerIndex = this.arrController.Count - 1;
			return new DataView(this.dtData);
			IL_2EF:
			this.recIdMax = int.Parse(this.dtData.Rows[this.dtData.Rows.Count - 1][0].ToString());
			wgTools.WriteLine(string.Format("recIdMax = {0:d}", this.recIdMax));
			IL_367:
			wgTools.WriteLine("da_Acc.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			wgTools.WriteLine("load Privileges Data End");
			return new DataView(this.dtData);
		}

		private void fillDgv(DataView dv)
		{
			try
			{
				DataGridView dataGridView = this.dgvPrivileges;
				if (dataGridView.DataSource == null)
				{
					dataGridView.DataSource = dv;
					for (int i = 0; i < dv.Table.Columns.Count; i++)
					{
						dataGridView.Columns[i].DataPropertyName = dv.Table.Columns[i].ColumnName;
					}
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
			string strSqlpar = (string)((object[])e.Argument)[2];
			e.Result = this.loadData(startIndex, maxRecords, strSqlpar);
			if (backgroundWorker.CancellationPending)
			{
				wgTools.WriteLine("bw.CancellationPending");
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
				if (this.iSelectedControllerIndex + 1 < this.arrController.Count)
				{
					this.iSelectedControllerIndex++;
					this.startRecordIndex = 0;
				}
				else
				{
					this.bLoadedFinished = true;
				}
			}
			this.fillDgv(e.Result as DataView);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvPrivileges.Rows.Count.ToString() + this.strAllPrivilegsNum + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvPrivileges_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				DataGridView dataGridView = this.dgvPrivileges;
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > dataGridView.Rows.Count || e.NewValue + dataGridView.Rows.Count / 10 > dataGridView.Rows.Count))
				{
					if (this.iSelectedControllerIndex + 1 < this.arrController.Count || this.startRecordIndex <= dataGridView.Rows.Count)
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
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvPrivileges.Rows.Count.ToString() + "/" + this.dgvPrivileges.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string strBaseInfo = " SELECT  t_d_Privilege.f_PrivilegeRecID,t_b_Door.f_DoorName, t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, t_b_Consumer.f_CardNO,  t_d_Privilege.f_ControlSegID,' ' as  f_ControlSegName, t_b_Consumer.f_ConsumerID ";
			string text = wgAppConfig.getSqlFindPrilivege(strBaseInfo, "t_d_Privilege", "", groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			if (this.cboDoor.Text != "")
			{
				this.dvDoors4Watching.RowFilter = "f_DoorName = " + wgTools.PrepareStr(this.cboDoor.Text);
				if (text.ToUpper().IndexOf(" WHERE ") > 0)
				{
					text = text + " AND t_d_Privilege.f_DoorID =" + this.dvDoors4Watching[0]["f_DoorID"].ToString();
				}
				else
				{
					text = text + " WHERE t_d_Privilege.f_DoorID =" + this.dvDoors4Watching[0]["f_DoorID"].ToString();
				}
				this.arrController.Clear();
				this.arrController.Add(this.dvDoors4Watching[0]["f_ControllerID"]);
			}
			else
			{
				this.arrController.Clear();
				for (int i = 0; i < this.arrControllerAllEnabled.Count; i++)
				{
					this.arrController.Add(this.arrControllerAllEnabled[i]);
				}
			}
			this.reloadData(text);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			this.cboDoor.SelectedIndex = -1;
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			bool flag = false;
			if (this.backgroundWorker1.IsBusy)
			{
				flag = true;
				this.backgroundWorker1.CancelAsync();
			}
			using (dfrmPrivilege dfrmPrivilege = new dfrmPrivilege())
			{
				if (dfrmPrivilege.ShowDialog(this) != DialogResult.OK)
				{
					if (flag)
					{
						this.userControlFind1.btnQuery.PerformClick();
					}
					return;
				}
			}
			icPrivilegeShare.setNeedRefresh();
			this.userControlFind1.btnQuery.PerformClick();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvPrivileges, this.Text);
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			DataGridView dataGridView = this.dgvPrivileges;
			if (dataGridView.Rows.Count <= 65535 && !this.bLoadedFinished)
			{
				using (dfrmWait dfrmWait = new dfrmWait())
				{
					dfrmWait.Show();
					dfrmWait.Refresh();
					while (this.backgroundWorker1.IsBusy)
					{
						Thread.Sleep(500);
						Application.DoEvents();
					}
					while (this.iSelectedControllerIndex + 1 < this.arrController.Count || this.startRecordIndex <= dataGridView.Rows.Count)
					{
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							66000 - dataGridView.Rows.Count,
							this.dgvSql
						});
						while (this.backgroundWorker1.IsBusy)
						{
							Thread.Sleep(500);
							Application.DoEvents();
						}
						this.startRecordIndex = this.startRecordIndex + 66000 - dataGridView.Rows.Count - this.MaxRecord;
						if (dataGridView.Rows.Count > 65535)
						{
							IL_156:
							dfrmWait.Hide();
							goto IL_168;
						}
					}
					wgAppRunInfo.raiseAppRunInfoLoadNums(dataGridView.Rows.Count.ToString() + "#");
					goto IL_156;
				}
			}
			IL_168:
			wgAppConfig.exportToExcel(dataGridView, this.Text);
		}

		private void dgvPrivileges_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < this.dgvPrivileges.Columns.Count && this.dgvPrivileges.Columns[e.ColumnIndex].Name.Equals("f_ControlSegName"))
			{
				string text = e.Value as string;
				if (text != null && text != " ")
				{
					return;
				}
				DataGridViewCell dataGridViewCell = this.dgvPrivileges[e.ColumnIndex, e.RowIndex];
				int num = (int)this.dgvPrivileges[e.ColumnIndex - 1, e.RowIndex].Value;
				for (int i = 0; i < this.controlSegIDList.Length; i++)
				{
					if (this.controlSegIDList[i] == num)
					{
						e.Value = this.controlSegNameList[i].ToString();
						dataGridViewCell.Value = e.Value;
						return;
					}
				}
			}
		}

		private void frmPrivileges_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.backgroundWorker1.CancelAsync();
		}

		private void btnEditSinglePrivilege_Click(object sender, EventArgs e)
		{
			try
			{
				DataGridView dataGridView = this.dgvPrivileges;
				int index;
				if (dataGridView.SelectedRows.Count <= 0)
				{
					if (dataGridView.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dataGridView.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dataGridView.SelectedRows[0].Index;
				}
				bool flag = false;
				if (this.backgroundWorker1.IsBusy)
				{
					flag = true;
					this.backgroundWorker1.CancelAsync();
				}
				using (dfrmPrivilegeSingle dfrmPrivilegeSingle = new dfrmPrivilegeSingle())
				{
					dfrmPrivilegeSingle.consumerID = int.Parse(dataGridView.Rows[index].Cells[7].Value.ToString());
					dfrmPrivilegeSingle.Text = string.Concat(new string[]
					{
						dataGridView.Rows[index].Cells[2].Value.ToString().Trim(),
						".",
						dataGridView.Rows[index].Cells[3].Value.ToString().Trim(),
						" -- ",
						dfrmPrivilegeSingle.Text
					});
					if (dfrmPrivilegeSingle.ShowDialog(this) != DialogResult.OK)
					{
						if (flag)
						{
							this.userControlFind1.btnQuery.PerformClick();
						}
						return;
					}
				}
				icPrivilegeShare.setNeedRefresh();
				this.userControlFind1.btnQuery.PerformClick();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cboDoor_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.userControlFind1.btnQuery.PerformClick();
			}
		}

		private void btnPrivilegeCopy_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = false;
				if (this.backgroundWorker1.IsBusy)
				{
					flag = true;
					this.backgroundWorker1.CancelAsync();
				}
				using (dfrmPrivilegeCopy dfrmPrivilegeCopy = new dfrmPrivilegeCopy())
				{
					if (dfrmPrivilegeCopy.ShowDialog(this) != DialogResult.OK)
					{
						if (flag)
						{
							this.userControlFind1.btnQuery.PerformClick();
						}
						return;
					}
				}
				icPrivilegeShare.setNeedRefresh();
				this.userControlFind1.btnQuery.PerformClick();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cardNOFuzzyQueryToolStripMenuItem_Click(object sender, EventArgs e)
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
				this.userControlFind1.txtFindCardID.Text = "";
				this.userControlFind1.txtFindName.Text = "";
				this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
				string strBaseInfo = " SELECT  t_d_Privilege.f_PrivilegeRecID,t_b_Door.f_DoorName, t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, t_b_Consumer.f_CardNO,  t_d_Privilege.f_ControlSegID,' ' as  f_ControlSegName, t_b_Consumer.f_ConsumerID ";
				string text2 = wgAppConfig.getSqlFindPrilivege(strBaseInfo, "t_d_Privilege", "", groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
				if (this.cboDoor.Text != "")
				{
					this.dvDoors4Watching.RowFilter = "f_DoorName = " + wgTools.PrepareStr(this.cboDoor.Text);
					if (text2.ToUpper().IndexOf(" WHERE ") > 0)
					{
						text2 = text2 + " AND t_d_Privilege.f_DoorID =" + this.dvDoors4Watching[0]["f_DoorID"].ToString();
					}
					else
					{
						text2 = text2 + " WHERE t_d_Privilege.f_DoorID =" + this.dvDoors4Watching[0]["f_DoorID"].ToString();
					}
					this.arrController.Clear();
					this.arrController.Add(this.dvDoors4Watching[0]["f_ControllerID"]);
				}
				else
				{
					this.arrController.Clear();
					for (int i = 0; i < this.arrControllerAllEnabled.Count; i++)
					{
						this.arrController.Add(this.arrControllerAllEnabled[i]);
					}
				}
				string text3 = " ( 1>0 ) ";
				if (text.IndexOf("%") < 0)
				{
					text = string.Format("%{0}%", text);
				}
				if (wgAppConfig.IsAccessDB)
				{
					text3 += string.Format(" AND CSTR(f_CardNO) like {0} ", wgTools.PrepareStr(text));
				}
				else
				{
					text3 += string.Format(" AND f_CardNO like {0} ", wgTools.PrepareStr(text));
				}
				if (text2.ToUpper().IndexOf("WHERE") > 0)
				{
					text2 += string.Format(" AND {0} ", text3);
				}
				else
				{
					text2 += string.Format(" WHERE {0} ", text3);
				}
				this.reloadData(text2);
				return;
			}
		}

		private void displayAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			while (!this.bLoadedFinished)
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this.startRecordIndex <= this.dgvPrivileges.Rows.Count)
				{
					if (!this.backgroundWorker1.IsBusy)
					{
						this.startRecordIndex += this.MaxRecord;
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							100000000,
							this.dgvSql
						});
					}
				}
				else
				{
					wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvPrivileges.Rows.Count.ToString() + "#");
				}
				Thread.Sleep(2000);
				Application.DoEvents();
				Cursor.Current = Cursors.WaitCursor;
			}
			Cursor.Current = Cursors.Default;
		}
	}
}
