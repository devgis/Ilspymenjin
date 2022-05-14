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
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Core
{
	public class UserControlFindSecond : UserControl
	{
		private int SelectedConsumerID;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private string dgvSql;

		private static string lastLoadUsers = "";

		private static DataView dvLastLoad;

		public static bool blogin = false;

		private string recNOMax = "";

		private DataTable tb;

		private DataView dv;

		private int startRecordIndex;

		private int MaxRecord = 20000;

		private string strGroupFilter = "";

		private IContainer components;

		private ToolStrip toolFindUsers;

		private ToolStripLabel toolStripLabel1;

		private ToolStripLabel toolStripLabel3;

		public ToolStripButton btnQuery;

		public ToolStripTextBox txtFindName;

		public ToolStripTextBox txtFindCardID;

		public ToolStripComboBox cboFindDept;

		public ToolStripButton btnClear;

		public ToolStripLabel toolStripLabel2;

		private BackgroundWorker backgroundWorker1;

		private ComboBox cboUsers;

		private System.Windows.Forms.Timer timer1;

		public UserControlFindSecond()
		{
			this.InitializeComponent();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			this.txtFindCardID.Text = "";
			this.txtFindName.Text = "";
			this.cboUsers.Text = "";
			if (this.cboFindDept.Items.Count > 0)
			{
				this.cboFindDept.SelectedIndex = 0;
			}
			this.cboUsers.Text = "";
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			this.SelectedConsumerID = 0;
			if (string.IsNullOrEmpty(this.cboUsers.Text))
			{
				this.txtFindName.Text = "";
				return;
			}
			if (this.cboUsers.SelectedIndex < 0)
			{
				this.txtFindName.Text = this.cboUsers.Text;
				return;
			}
			this.txtFindName.Text = ((DataRowView)this.cboUsers.SelectedItem).Row["f_ConsumerName"].ToString();
			this.SelectedConsumerID = (int)((DataRowView)this.cboUsers.SelectedItem).Row["f_ConsumerID"];
		}

		private void UserControlFind_Load(object sender, EventArgs e)
		{
			if (!UserControlFindSecond.blogin)
			{
				return;
			}
			try
			{
				this.cboFindDept.Items.Clear();
				icGroup icGroup = new icGroup();
				icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
				int i = this.arrGroupID.Count;
				for (i = 0; i < this.arrGroupID.Count; i++)
				{
					this.cboFindDept.Items.Add(this.arrGroupName[i].ToString());
				}
				if (this.cboFindDept.Items.Count > 0)
				{
					this.cboFindDept.SelectedIndex = 0;
				}
				this.toolStripLabel3.Text = wgAppConfig.ReplaceFloorRomm(this.toolStripLabel3.Text);
				this.timer1.Enabled = true;
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		public void getSqlInfo(ref int groupMinNO, ref int groupIDOfMinNO, ref int groupMaxNO, ref string findName, ref long findCard, ref int findConsumerID)
		{
			try
			{
				this.btnQuery_Click(null, null);
				findConsumerID = this.SelectedConsumerID;
				if (this.cboFindDept.SelectedIndex < 0 || (this.cboFindDept.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
				{
					groupMinNO = 0;
					groupMaxNO = 0;
				}
				else
				{
					groupIDOfMinNO = (int)this.arrGroupID[this.cboFindDept.SelectedIndex];
					groupMinNO = (int)this.arrGroupNO[this.cboFindDept.SelectedIndex];
					groupMaxNO = icGroup.getGroupChildMaxNo(this.cboFindDept.Text, this.arrGroupName, this.arrGroupNO);
				}
				if (string.IsNullOrEmpty(this.txtFindName.Text))
				{
					findName = "";
				}
				else
				{
					findName = this.txtFindName.Text.Trim();
				}
				findCard = 0L;
				if (long.TryParse(this.txtFindCardID.Text, out findCard) && findCard < 0L)
				{
					findCard = 0L;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		public string getSqlOfGroup(string fieldName)
		{
			string text = "";
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text2 = "";
			long num4 = 0L;
			int num5 = 0;
			this.getSqlInfo(ref num, ref num2, ref num3, ref text2, ref num4, ref num5);
			if (num >= num3)
			{
				text += string.Format(" {0} ={1:d} ", fieldName, num2);
			}
			else
			{
				for (int i = 0; i < this.arrGroupNO.Count; i++)
				{
					if ((int)this.arrGroupNO[i] <= num3 && (int)this.arrGroupNO[i] >= num)
					{
						if (text == "")
						{
							text += string.Format(" {0} ={1:d} ", fieldName, (int)this.arrGroupID[i]);
						}
						else
						{
							text += string.Format(" OR {0} ={1:d} ", fieldName, (int)this.arrGroupID[i]);
						}
					}
				}
			}
			return text;
		}

		private void txtFindCardID_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btnQuery.PerformClick();
				return;
			}
			if (this.txtFindCardID.Text.Length == 0)
			{
				if (e.KeyChar == '\u0016')
				{
					return;
				}
			}
			else
			{
				if (e.KeyChar == '\u0003')
				{
					return;
				}
				if (e.KeyChar == '\u0018')
				{
					return;
				}
			}
			if (e.KeyChar == '\b')
			{
				return;
			}
			int num;
			if (int.TryParse(e.KeyChar.ToString(), out num))
			{
				if (this.txtFindCardID.Text.Length > 10)
				{
					e.Handled = true;
					return;
				}
				if (this.txtFindCardID.Text.Length == 10 && this.txtFindCardID.SelectionLength == 0)
				{
					e.Handled = true;
					return;
				}
			}
			else
			{
				e.Handled = true;
			}
		}

		private DataView loadUserData4BackWork(int startIndex, int maxRecords, string strSql)
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
			DataView result;
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.tb);
							if (this.tb.Rows.Count > 0)
							{
								this.recNOMax = this.tb.Rows[this.tb.Rows.Count - 1]["f_ConsumerNO"].ToString();
							}
							wgTools.WriteLine("loadUserData End");
							result = this.dv;
							return result;
						}
					}
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.tb);
						if (this.tb.Rows.Count > 0)
						{
							this.recNOMax = this.tb.Rows[this.tb.Rows.Count - 1]["f_ConsumerNO"].ToString();
						}
						wgTools.WriteLine("loadUserData End");
						result = this.dv;
					}
				}
			}
			return result;
		}

		private void loadUserData4BackWorkComplete(DataView dv)
		{
			if (this.cboUsers.DataSource == null)
			{
				this.cboUsers.BeginUpdate();
				this.cboUsers.DisplayMember = "f_ConsumerFull";
				this.cboUsers.ValueMember = "f_ConsumerID";
				this.cboUsers.DataSource = dv;
				UserControlFindSecond.dvLastLoad = dv;
				this.cboUsers.EndUpdate();
				this.cboUsers.Text = "";
				this.cboFindDept_SelectedIndexChanged(null, null);
				return;
			}
			if (dv.Count > 0)
			{
				DataView dataView = this.cboUsers.DataSource as DataView;
				dataView.Table.Merge(dv.Table);
				if (dv.Count >= this.MaxRecord)
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
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			wgTools.WriteLine("DoWork Starting ...");
			if (UserControlFindSecond.lastLoadUsers == icConsumerShare.getUpdateLog() && UserControlFindSecond.dvLastLoad != null)
			{
				Thread.Sleep(100);
				UserControlFindSecond.dvLastLoad.RowFilter = "";
				e.Result = UserControlFindSecond.dvLastLoad;
				return;
			}
			UserControlFindSecond.lastLoadUsers = icConsumerShare.getUpdateLog();
			int startIndex = (int)((object[])e.Argument)[0];
			int maxRecords = (int)((object[])e.Argument)[1];
			string strSql = (string)((object[])e.Argument)[2];
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			e.Result = this.loadUserData4BackWork(startIndex, maxRecords, strSql);
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
			this.loadUserData4BackWorkComplete(e.Result as DataView);
			wgTools.WriteLine("backgroundWorker1_RunWorkerCompleted");
		}

		private void cboUsers_DropDown(object sender, EventArgs e)
		{
		}

		private void cboUsers_DropDownClosed(object sender, EventArgs e)
		{
		}

		private void cboUsers_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btnQuery.PerformClick();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			this.startRecordIndex = 0;
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, 0 as f_Selected, f_GroupID, '(' + LTRIM(f_ConsumerNO) + ')-' +  f_ConsumerName + '-' + IIF(ISNULL( f_CardNO),'-',CSTR(f_CardNO))  As f_ConsumerFull ";
				text += " FROM t_b_Consumer ";
			}
			else
			{
				text = " SELECT f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, 0 as f_Selected, f_GroupID, '(' + LTRIM(f_ConsumerNO) + ')-' +  f_ConsumerName + '-' + CASE WHEN f_CardNO IS NULL THEN '-' ELSE CONVERT(nvarchar(50),f_CardNO) END  As f_ConsumerFull ";
				text += " FROM t_b_Consumer ";
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.dgvSql = text;
			}
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void cboFindDept_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cboUsers.DataSource != null)
				{
					DataView dataView = (DataView)this.cboUsers.DataSource;
					if (this.cboFindDept.SelectedIndex < 0)
					{
						dataView.RowFilter = "";
						this.strGroupFilter = "";
					}
					if (this.cboFindDept.SelectedIndex == 0 && this.cboFindDept.Text == "")
					{
						dataView.RowFilter = "";
						this.strGroupFilter = "";
					}
					else
					{
						this.strGroupFilter = " f_GroupID =" + this.arrGroupID[this.cboFindDept.SelectedIndex];
						int num = (int)this.arrGroupID[this.cboFindDept.SelectedIndex];
						int num2 = (int)this.arrGroupNO[this.cboFindDept.SelectedIndex];
						int groupChildMaxNo = icGroup.getGroupChildMaxNo(this.cboFindDept.Text, this.arrGroupName, this.arrGroupNO);
						if (num2 > 0)
						{
							if (num2 >= groupChildMaxNo)
							{
								this.strGroupFilter = string.Format(" f_GroupID ={0:d} ", num);
							}
							else
							{
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
								this.strGroupFilter = string.Format("  {0} ", text);
							}
						}
						dataView.RowFilter = string.Format("{0}", this.strGroupFilter);
					}
					this.cboUsers.Text = "";
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UserControlFindSecond));
			this.toolFindUsers = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.txtFindName = new ToolStripTextBox();
			this.toolStripLabel2 = new ToolStripLabel();
			this.txtFindCardID = new ToolStripTextBox();
			this.toolStripLabel3 = new ToolStripLabel();
			this.cboFindDept = new ToolStripComboBox();
			this.btnQuery = new ToolStripButton();
			this.btnClear = new ToolStripButton();
			this.backgroundWorker1 = new BackgroundWorker();
			this.cboUsers = new ComboBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolFindUsers.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.toolFindUsers, "toolFindUsers");
			this.toolFindUsers.BackgroundImage = Resources.pTools_third_title;
			this.toolFindUsers.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.txtFindName,
				this.toolStripLabel2,
				this.txtFindCardID,
				this.toolStripLabel3,
				this.cboFindDept,
				this.btnQuery,
				this.btnClear
			});
			this.toolFindUsers.Name = "toolFindUsers";
			componentResourceManager.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			this.toolStripLabel1.ForeColor = Color.White;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.txtFindName.BorderStyle = BorderStyle.FixedSingle;
			this.txtFindName.Name = "txtFindName";
			componentResourceManager.ApplyResources(this.txtFindName, "txtFindName");
			this.toolStripLabel2.ForeColor = Color.White;
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.txtFindCardID.Name = "txtFindCardID";
			componentResourceManager.ApplyResources(this.txtFindCardID, "txtFindCardID");
			this.txtFindCardID.KeyPress += new KeyPressEventHandler(this.txtFindCardID_KeyPress);
			this.toolStripLabel3.ForeColor = Color.White;
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			this.cboFindDept.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboFindDept.Name = "cboFindDept";
			componentResourceManager.ApplyResources(this.cboFindDept, "cboFindDept");
			this.cboFindDept.SelectedIndexChanged += new EventHandler(this.cboFindDept_SelectedIndexChanged);
			this.btnQuery.ForeColor = Color.White;
			this.btnQuery.Image = Resources.pTools_Query;
			componentResourceManager.ApplyResources(this.btnQuery, "btnQuery");
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.btnClear.ForeColor = Color.White;
			this.btnClear.Image = Resources.pTools_Clear_Condition;
			componentResourceManager.ApplyResources(this.btnClear, "btnClear");
			this.btnClear.Name = "btnClear";
			this.btnClear.Click += new EventHandler(this.btnClear_Click);
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.cboUsers.DropDownWidth = 200;
			this.cboUsers.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboUsers, "cboUsers");
			this.cboUsers.Name = "cboUsers";
			this.cboUsers.DropDown += new EventHandler(this.cboUsers_DropDown);
			this.cboUsers.DropDownClosed += new EventHandler(this.cboUsers_DropDownClosed);
			this.cboUsers.KeyPress += new KeyPressEventHandler(this.cboUsers_KeyPress);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			this.BackgroundImage = Resources.pTools_third_title;
			base.Controls.Add(this.cboUsers);
			base.Controls.Add(this.toolFindUsers);
			this.DoubleBuffered = true;
			base.Name = "UserControlFindSecond";
			base.Load += new EventHandler(this.UserControlFind_Load);
			this.toolFindUsers.ResumeLayout(false);
			this.toolFindUsers.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
