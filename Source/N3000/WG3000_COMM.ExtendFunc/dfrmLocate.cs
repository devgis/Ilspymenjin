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
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmLocate : frmN3000
	{
		private IContainer components;

		private Button btnExit;

		private Label lblWait;

		private DataGridView dgvUsers;

		private Button button1;

		private ComboBox cbof_GroupID;

		private Label label4;

		private Button btnQuery;

		private BackgroundWorker backgroundWorker1;

		private System.Windows.Forms.Timer timer1;

		private ToolTip toolTip1;

		private ProgressBar progressBar1;

		private RichTextBox txtLocate;

		private BackgroundWorker backgroundWorker2;

		private Label label1;

		private DataGridViewTextBoxColumn ConsumerID;

		private DataGridViewTextBoxColumn UserID;

		private DataGridViewTextBoxColumn ConsumerName;

		private DataGridViewTextBoxColumn CardNO;

		private DataGridViewTextBoxColumn f_GroupID;

		private DataGridViewCheckBoxColumn f_SelectedUsers;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private DataView dv;

		private DataView dvSelected;

		private dfrmFind dfrmFind1;

		private bool bStarting = true;

		private string strGroupFilter = "";

		private bool bEdit;

		private string m_strGroupName;

		private string m_strUsers;

		private string strInOutInfo;

		private string strUserId = "000";

		private DataSet ds = new DataSet("ReaderAndCardRecordtable");

		private icController controller4Locate = new icController();

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.controller4Locate != null)
			{
				this.controller4Locate.Dispose();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmLocate));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.progressBar1 = new ProgressBar();
			this.btnQuery = new Button();
			this.lblWait = new Label();
			this.dgvUsers = new DataGridView();
			this.ConsumerID = new DataGridViewTextBoxColumn();
			this.UserID = new DataGridViewTextBoxColumn();
			this.ConsumerName = new DataGridViewTextBoxColumn();
			this.CardNO = new DataGridViewTextBoxColumn();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.f_SelectedUsers = new DataGridViewCheckBoxColumn();
			this.button1 = new Button();
			this.cbof_GroupID = new ComboBox();
			this.label4 = new Label();
			this.btnExit = new Button();
			this.txtLocate = new RichTextBox();
			this.label1 = new Label();
			this.backgroundWorker2 = new BackgroundWorker();
			((ISupportInitialize)this.dgvUsers).BeginInit();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Name = "progressBar1";
			this.btnQuery.BackColor = Color.Transparent;
			this.btnQuery.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnQuery, "btnQuery");
			this.btnQuery.ForeColor = Color.White;
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.UseVisualStyleBackColor = false;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			componentResourceManager.ApplyResources(this.lblWait, "lblWait");
			this.lblWait.BorderStyle = BorderStyle.FixedSingle;
			this.lblWait.ForeColor = Color.White;
			this.lblWait.Name = "lblWait";
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AllowUserToDeleteRows = false;
			this.dgvUsers.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvUsers, "dgvUsers");
			this.dgvUsers.BackgroundColor = Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ConsumerID,
				this.UserID,
				this.ConsumerName,
				this.CardNO,
				this.f_GroupID,
				this.f_SelectedUsers
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.MultiSelect = false;
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvUsers.RowTemplate.Height = 23;
			this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvUsers.KeyDown += new KeyEventHandler(this.dgvUsers_KeyDown);
			componentResourceManager.ApplyResources(this.ConsumerID, "ConsumerID");
			this.ConsumerID.Name = "ConsumerID";
			this.ConsumerID.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.UserID.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.UserID, "UserID");
			this.UserID.Name = "UserID";
			this.UserID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ConsumerName, "ConsumerName");
			this.ConsumerName.Name = "ConsumerName";
			this.ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.CardNO, "CardNO");
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_GroupID, "f_GroupID");
			this.f_GroupID.Name = "f_GroupID";
			this.f_GroupID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedUsers, "f_SelectedUsers");
			this.f_SelectedUsers.Name = "f_SelectedUsers";
			this.f_SelectedUsers.ReadOnly = true;
			this.button1.BackColor = Color.Transparent;
			this.button1.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.button1, "button1");
			this.button1.ForeColor = Color.White;
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.btnExit_Click);
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.FormattingEnabled = true;
			this.cbof_GroupID.Name = "cbof_GroupID";
			this.cbof_GroupID.SelectedIndexChanged += new EventHandler(this.cbof_GroupID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.BackColor = Color.Transparent;
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.txtLocate, "txtLocate");
			this.txtLocate.BackColor = Color.White;
			this.txtLocate.BorderStyle = BorderStyle.None;
			this.txtLocate.ForeColor = Color.Black;
			this.txtLocate.Name = "txtLocate";
			this.txtLocate.TextChanged += new EventHandler(this.txtLocate_TextChanged);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.Yellow;
			this.label1.Name = "label1";
			this.backgroundWorker2.DoWork += new DoWorkEventHandler(this.backgroundWorker2_DoWork);
			this.backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtLocate);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.btnQuery);
			base.Controls.Add(this.lblWait);
			base.Controls.Add(this.dgvUsers);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.cbof_GroupID);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.btnExit);
			base.Name = "dfrmLocate";
			base.FormClosing += new FormClosingEventHandler(this.dfrmPrivilegeCopy_FormClosing);
			base.Load += new EventHandler(this.dfrmPrivilegeCopy_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmLocate_KeyDown);
			((ISupportInitialize)this.dgvUsers).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmLocate()
		{
			this.InitializeComponent();
		}

		private void dfrmPrivilegeCopy_Load(object sender, EventArgs e)
		{
			try
			{
				icGroup icGroup = new icGroup();
				icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
				for (int i = 0; i < this.arrGroupID.Count; i++)
				{
					if (i == 0 && string.IsNullOrEmpty(this.arrGroupName[i].ToString()))
					{
						this.cbof_GroupID.Items.Add(CommonStr.strAll);
					}
					else
					{
						this.cbof_GroupID.Items.Add(this.arrGroupName[i].ToString());
					}
				}
				if (this.cbof_GroupID.Items.Count > 0)
				{
					this.cbof_GroupID.SelectedIndex = 0;
				}
				this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
				this.UserID.HeaderText = wgAppConfig.ReplaceWorkNO(this.UserID.HeaderText);
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			if (!this.backgroundWorker1.IsBusy)
			{
				this.backgroundWorker1.RunWorkerAsync();
			}
			this.dgvUsers.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			Cursor.Current = Cursors.WaitCursor;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			e.Result = this.loadUserData4BackWork();
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
			this.loadUserData4BackWorkComplete(e.Result as DataTable);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvUsers.Rows.Count.ToString());
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				if (!this.bStarting)
				{
					if (this.backgroundWorker2.IsBusy)
					{
						Cursor.Current = Cursors.WaitCursor;
					}
					else if (this.progressBar1.Value != 0 && this.progressBar1.Value != this.progressBar1.Maximum)
					{
						Cursor.Current = Cursors.WaitCursor;
					}
				}
				else if (this.dgvUsers.DataSource == null)
				{
					Cursor.Current = Cursors.WaitCursor;
				}
				else
				{
					this.timer1.Enabled = false;
					Cursor.Current = Cursors.Default;
					this.lblWait.Visible = false;
					this.btnQuery.Enabled = true;
					this.cbof_GroupID.Enabled = true;
					this.bStarting = false;
				}
			}
			catch (Exception)
			{
			}
		}

		private DataTable loadUserData4BackWork()
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("loadUserData Start");
			icConsumerShare.loadUserData();
			this.ds = new DataSet("ReaderAndCardRecordtable");
			string cmdText = " SELECT * FROM t_d_SwipeRecord WHERE 1<0 ";
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.ds, "cardrecord");
						}
					}
					goto IL_E2;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.ds, "cardrecord");
					}
				}
			}
			IL_E2:
			return icConsumerShare.getDt();
		}

		private void loadUserData4BackWorkComplete(DataTable dtUser)
		{
			this.dv = new DataView(dtUser);
			this.dvSelected = new DataView(dtUser);
			this.dv.RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
			this.dvSelected.RowFilter = string.Format("f_DoorEnabled > 0 AND {0}", icConsumerShare.getSelectedRowfilter());
			this.dgvUsers.AutoGenerateColumns = false;
			this.dgvUsers.DataSource = this.dv;
			int num = 0;
			while (num < this.dv.Table.Columns.Count && num < this.dgvUsers.ColumnCount)
			{
				this.dgvUsers.Columns[num].DataPropertyName = dtUser.Columns[num].ColumnName;
				num++;
			}
			this.cbof_GroupID_SelectedIndexChanged(null, null);
			wgTools.WriteLine("loadUserData End");
			Cursor.Current = Cursors.Default;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (this.bEdit)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.Close();
		}

		private void cbof_GroupID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvUsers.DataSource != null)
			{
				DataView dataView = (DataView)this.dgvUsers.DataSource;
				if (this.cbof_GroupID.SelectedIndex < 0 || (this.cbof_GroupID.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
				{
					dataView.RowFilter = icConsumerShare.getOptionalRowfilter();
					this.strGroupFilter = "";
				}
				else
				{
					dataView.RowFilter = "f_Selected = 0 AND f_GroupID =" + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					this.strGroupFilter = " f_GroupID =" + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					int num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					int num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex];
					int groupChildMaxNo = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
					if (num2 > 0)
					{
						if (num2 >= groupChildMaxNo)
						{
							dataView.RowFilter = string.Format("f_Selected = 0 AND f_GroupID ={0:d} ", num);
							this.strGroupFilter = string.Format(" f_GroupID ={0:d} ", num);
						}
						else
						{
							dataView.RowFilter = "f_Selected = 0 ";
							string text = "";
							for (int i = 0; i < this.arrGroupNO.Count; i++)
							{
								if ((int)this.arrGroupNO[i] <= groupChildMaxNo && (int)this.arrGroupNO[i] >= num2)
								{
									if (text == "")
									{
										text += string.Format(" f_GroupID ={0:d} ", (int)this.arrGroupID[i]);
									}
									else
									{
										text += string.Format(" OR f_GroupID ={0:d} ", (int)this.arrGroupID[i]);
									}
								}
							}
							dataView.RowFilter = string.Format("f_Selected = 0 AND ( {0} )", text);
							this.strGroupFilter = string.Format("  {0} ", text);
						}
					}
					dataView.RowFilter = string.Format("(f_DoorEnabled > 0) AND {0} AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
				}
				if (string.IsNullOrEmpty(this.strGroupFilter))
				{
					((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  {0}", icConsumerShare.getOptionalRowfilter());
					return;
				}
				((DataView)this.dgvUsers.DataSource).RowFilter = string.Format("f_DoorEnabled > 0 AND  ({0}) AND ({1})", icConsumerShare.getOptionalRowfilter(), this.strGroupFilter);
			}
		}

		private void dfrmPrivilegeCopy_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
		{
			this.dfrmLocate_KeyDown(this.dgvUsers, e);
		}

		private void dfrmLocate_KeyDown(object sender, KeyEventArgs e)
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

		private void btnQuery_Click(object sender, EventArgs e)
		{
			DataGridView dataGridView = this.dgvUsers;
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
			DataTable table = ((DataView)dataGridView.DataSource).Table;
			int num = (int)dataGridView.Rows[index].Cells[0].Value;
			DataRow dataRow = table.Rows.Find(num);
			if (dataRow != null)
			{
				this.strUserId = dataRow["f_ConsumerID"].ToString();
				this.m_strGroupName = this.cbof_GroupID.Text;
				if (this.m_strGroupName == CommonStr.strAll)
				{
					this.m_strGroupName = "";
				}
				this.m_strUsers = dataRow["f_ConsumerName"].ToString();
				if (this.backgroundWorker2.IsBusy)
				{
					return;
				}
				this.backgroundWorker2.RunWorkerAsync();
				this.timer1.Enabled = true;
			}
		}

		private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			this.strInOutInfo = "";
			try
			{
				int num = 0;
				DataRow[] array = new DataRow[]
				{
					this.ds.Tables["cardrecord"].NewRow(),
					this.ds.Tables["cardrecord"].NewRow()
				};
				string cmdText = " SELECT * FROM t_d_SwipeRecord WHERE f_Character >0 AND f_ConsumerID = " + this.strUserId + " ORDER BY f_ReadDate DESC ";
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
						{
							oleDbConnection.Open();
							oleDbCommand.CommandTimeout = 180;
							OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
							while (oleDbDataReader.Read())
							{
								if (!((DateTime)oleDbDataReader["f_ReadDate"] > DateTime.Now.AddDays(2.0)))
								{
									for (int i = 0; i <= oleDbDataReader.FieldCount - 1; i++)
									{
										array[num][i] = oleDbDataReader[i];
									}
									num++;
									if (num >= 2)
									{
										break;
									}
								}
							}
							oleDbDataReader.Close();
						}
						goto IL_1FE;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.CommandTimeout = 180;
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							if (!((DateTime)sqlDataReader["f_ReadDate"] > DateTime.Now.AddDays(2.0)))
							{
								for (int i = 0; i <= sqlDataReader.FieldCount - 1; i++)
								{
									array[num][i] = sqlDataReader[i];
								}
								num++;
								if (num >= 2)
								{
									break;
								}
							}
						}
						sqlDataReader.Close();
					}
				}
				IL_1FE:
				string text = this.m_strUsers;
				text += "\r\n";
				if (num > 0)
				{
					if (num == 2 && (int)array[0]["f_ControllerSN"] == (int)array[1]["f_ControllerSN"] && (DateTime)array[0]["f_ReadDate"] > (DateTime)array[1]["f_ReadDate"] && array[0]["f_InOut"].ToString() == "0" && array[1]["f_InOut"].ToString() == "1")
					{
						this.controller4Locate.GetInfoFromDBByControllerSN((int)array[1]["f_ControllerSN"]);
						text = text + ((DateTime)array[1]["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek) + "   {0}  " + this.controller4Locate.GetDoorNameByReaderNO((int)((byte)array[1]["f_ReaderNO"]));
						text += "\r\n";
						this.controller4Locate.GetInfoFromDBByControllerSN((int)array[0]["f_ControllerSN"]);
						text = text + ((DateTime)array[0]["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek) + "   {1}  " + this.controller4Locate.GetDoorNameByReaderNO((int)((byte)array[0]["f_ReaderNO"]));
						text += "\r\n";
						text += "{2}:  ";
						TimeSpan timeSpan = ((DateTime)array[0]["f_ReadDate"]).Subtract((DateTime)array[1]["f_ReadDate"]);
						if (timeSpan.TotalDays >= 1.0)
						{
							text = text + (int)timeSpan.TotalDays + " {9}, ";
						}
						if (timeSpan.Hours > 0)
						{
							text = text + timeSpan.Hours + " {3}, ";
						}
						if (timeSpan.Minutes > 0)
						{
							text = text + timeSpan.Minutes + " {4} ";
						}
					}
					else
					{
						this.controller4Locate.GetInfoFromDBByControllerSN((int)array[0]["f_ControllerSN"]);
						if (array[0]["f_InOut"].ToString() == "0")
						{
							text += "{5}";
							text += "\r\n";
							text = text + ((DateTime)array[0]["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek) + "   {1}    " + this.controller4Locate.GetDoorNameByReaderNO((int)((byte)array[0]["f_ReaderNO"]));
							text += "\r\n";
							text += "{2}:  ";
						}
						else
						{
							text = text + ((DateTime)array[0]["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek) + "   {0}    " + this.controller4Locate.GetDoorNameByReaderNO((int)((byte)array[0]["f_ReaderNO"]));
							text += "\r\n";
							text += "{6}";
							text += "\r\n";
							text += "{2}:  ";
							TimeSpan timeSpan = DateTime.Now.Subtract((DateTime)array[0]["f_ReadDate"]);
							if (timeSpan.TotalSeconds < 0.0)
							{
								text += "[{7}] ";
							}
							else
							{
								if (timeSpan.TotalDays >= 1.0)
								{
									text = text + (int)timeSpan.TotalDays + " {9}, ";
								}
								if (timeSpan.Hours > 0)
								{
									text = text + timeSpan.Hours + " {3}, ";
								}
								if (timeSpan.Minutes > 0)
								{
									text = text + timeSpan.Minutes + " {4} ";
								}
							}
						}
					}
				}
				else
				{
					text += "{8}";
				}
				this.strInOutInfo = text;
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.txtLocate.Text = string.Format(this.strInOutInfo, new object[]
			{
				CommonStr.strEnterInto,
				CommonStr.strGoOff,
				CommonStr.strStay,
				CommonStr.strHour,
				CommonStr.strMinutes,
				CommonStr.strEnterWithoutSwiping,
				CommonStr.strDontGoOff,
				CommonStr.strLaterThanNow,
				CommonStr.strNoSwiping,
				CommonStr.strDay
			});
			this.timer1.Enabled = false;
			Cursor.Current = Cursors.Default;
		}

		private void txtLocate_TextChanged(object sender, EventArgs e)
		{
		}
	}
}
