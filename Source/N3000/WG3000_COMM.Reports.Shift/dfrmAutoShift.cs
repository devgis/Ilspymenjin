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

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmAutoShift : frmN3000
	{
		private Container components;

		internal ComboBox cbof_Group;

		internal Label Label3;

		internal Label Label4;

		internal ComboBox cbof_ConsumerName;

		internal DateTimePicker dtpStartDate;

		internal Label Label5;

		internal Label Label6;

		internal DateTimePicker dtpEndDate;

		internal Label lblStartWeekday;

		internal Label lblEndWeekday;

		internal GroupBox GroupBox1;

		internal Label Label1;

		internal Label Label2;

		internal Button btnOK;

		internal Button btnCancel;

		internal ListBox lstOptionalShifts;

		internal ListBox lstSelectedShifts;

		internal Button btnAddOne;

		internal Button btnDeleteOne;

		internal Button btnDeleteAll;

		internal Label Label7;

		internal ListBox lstShiftWeekday;

		internal Label label8;

		internal ProgressBar ProgressBar1;

		private ArrayList arrConsumerCMIndex = new ArrayList();

		private ArrayList arrShiftID = new ArrayList();

		private ArrayList arrSelectedShiftID = new ArrayList();

		private SqlDataAdapter daConsumers;

		private DataSet dsConsumers;

		private DataTable dtOptionalShift;

		private DataView dvConsumers;

		private DataTable dtConsumers;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private SqlConnection con;

		private SqlCommand cmd;

		private dfrmFind dfrmFind1;

		public dfrmAutoShift()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmAutoShift));
			this.cbof_Group = new ComboBox();
			this.Label3 = new Label();
			this.Label4 = new Label();
			this.cbof_ConsumerName = new ComboBox();
			this.dtpStartDate = new DateTimePicker();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.dtpEndDate = new DateTimePicker();
			this.lblStartWeekday = new Label();
			this.lblEndWeekday = new Label();
			this.GroupBox1 = new GroupBox();
			this.Label1 = new Label();
			this.Label2 = new Label();
			this.btnOK = new Button();
			this.btnAddOne = new Button();
			this.btnDeleteOne = new Button();
			this.btnDeleteAll = new Button();
			this.btnCancel = new Button();
			this.lstOptionalShifts = new ListBox();
			this.lstSelectedShifts = new ListBox();
			this.Label7 = new Label();
			this.lstShiftWeekday = new ListBox();
			this.ProgressBar1 = new ProgressBar();
			this.label8 = new Label();
			this.GroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.cbof_Group.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_Group, "cbof_Group");
			this.cbof_Group.Name = "cbof_Group";
			this.cbof_Group.SelectedIndexChanged += new EventHandler(this.cbof_Group_SelectedIndexChanged);
			this.cbof_Group.KeyDown += new KeyEventHandler(this.dfrmAutoShift_KeyDown);
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.Name = "Label4";
			this.cbof_ConsumerName.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_ConsumerName, "cbof_ConsumerName");
			this.cbof_ConsumerName.Name = "cbof_ConsumerName";
			this.cbof_ConsumerName.KeyDown += new KeyEventHandler(this.dfrmAutoShift_KeyDown);
			this.cbof_ConsumerName.Leave += new EventHandler(this.cbof_ConsumerName_Leave);
			componentResourceManager.ApplyResources(this.dtpStartDate, "dtpStartDate");
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpStartDate.ValueChanged += new EventHandler(this.dtpStartDate_ValueChanged);
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.dtpEndDate, "dtpEndDate");
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpEndDate.ValueChanged += new EventHandler(this.dtpEndDate_ValueChanged);
			componentResourceManager.ApplyResources(this.lblStartWeekday, "lblStartWeekday");
			this.lblStartWeekday.Name = "lblStartWeekday";
			componentResourceManager.ApplyResources(this.lblEndWeekday, "lblEndWeekday");
			this.lblEndWeekday.Name = "lblEndWeekday";
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.cbof_Group);
			this.GroupBox1.Controls.Add(this.dtpEndDate);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.dtpStartDate);
			this.GroupBox1.Controls.Add(this.cbof_ConsumerName);
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.lblEndWeekday);
			this.GroupBox1.Controls.Add(this.lblStartWeekday);
			this.GroupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.Name = "Label2";
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnAddOne.BackColor = Color.Transparent;
			this.btnAddOne.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddOne, "btnAddOne");
			this.btnAddOne.ForeColor = Color.White;
			this.btnAddOne.Name = "btnAddOne";
			this.btnAddOne.UseVisualStyleBackColor = false;
			this.btnAddOne.Click += new EventHandler(this.btnAddOne_Click);
			this.btnDeleteOne.BackColor = Color.Transparent;
			this.btnDeleteOne.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteOne, "btnDeleteOne");
			this.btnDeleteOne.ForeColor = Color.White;
			this.btnDeleteOne.Name = "btnDeleteOne";
			this.btnDeleteOne.UseVisualStyleBackColor = false;
			this.btnDeleteOne.Click += new EventHandler(this.btnDeleteOne_Click);
			this.btnDeleteAll.BackColor = Color.Transparent;
			this.btnDeleteAll.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteAll, "btnDeleteAll");
			this.btnDeleteAll.ForeColor = Color.White;
			this.btnDeleteAll.Name = "btnDeleteAll";
			this.btnDeleteAll.UseVisualStyleBackColor = false;
			this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.lstOptionalShifts, "lstOptionalShifts");
			this.lstOptionalShifts.Name = "lstOptionalShifts";
			this.lstOptionalShifts.DoubleClick += new EventHandler(this.lstOptionalShifts_DoubleClick);
			componentResourceManager.ApplyResources(this.lstSelectedShifts, "lstSelectedShifts");
			this.lstSelectedShifts.Name = "lstSelectedShifts";
			this.lstSelectedShifts.DoubleClick += new EventHandler(this.lstSelectedShifts_DoubleClick);
			this.Label7.BackColor = SystemColors.ControlLight;
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.Name = "Label7";
			componentResourceManager.ApplyResources(this.lstShiftWeekday, "lstShiftWeekday");
			this.lstShiftWeekday.BackColor = SystemColors.ControlLight;
			this.lstShiftWeekday.Name = "lstShiftWeekday";
			this.lstShiftWeekday.TabStop = false;
			componentResourceManager.ApplyResources(this.ProgressBar1, "ProgressBar1");
			this.ProgressBar1.Name = "ProgressBar1";
			this.label8.BackColor = SystemColors.ControlLight;
			componentResourceManager.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.lstSelectedShifts);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.ProgressBar1);
			base.Controls.Add(this.Label7);
			base.Controls.Add(this.lstOptionalShifts);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnAddOne);
			base.Controls.Add(this.btnDeleteOne);
			base.Controls.Add(this.btnDeleteAll);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.lstShiftWeekday);
			base.Name = "dfrmAutoShift";
			base.FormClosing += new FormClosingEventHandler(this.dfrmAutoShift_FormClosing);
			base.Load += new EventHandler(this.dfrmAutoShift_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmAutoShift_KeyDown);
			this.GroupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void loadGroupData()
		{
			icGroup icGroup = new icGroup();
			icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
			for (int i = 0; i < this.arrGroupID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrGroupName[i].ToString()))
				{
					this.cbof_Group.Items.Add(CommonStr.strAll);
				}
				else
				{
					this.cbof_Group.Items.Add(this.arrGroupName[i].ToString());
				}
			}
			if (this.cbof_Group.Items.Count > 0)
			{
				this.cbof_Group.SelectedIndex = 0;
			}
		}

		private void _dataTableLoad()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this._dataTableLoad_Acc();
				return;
			}
			this.con = new SqlConnection(wgAppConfig.dbConString);
			this.dsConsumers = new DataSet("Users");
			string text = " SELECT t_b_Group.f_GroupName,t_b_Consumer.f_ConsumerID, t_b_Consumer.f_ConsumerName, LTRIM(([f_ConsumerNo]) +'- '+ [f_ConsumerName]) as [f_UserFullName]  FROM [t_b_Consumer]  LEFT OUTER JOIN t_b_Group ON  t_b_Group.f_GroupID = t_b_Consumer.f_GroupID  WHERE f_AttendEnabled = 1 ";
			text += " AND f_ShiftEnabled > 0 ";
			this.cmd = new SqlCommand(text, this.con);
			this.daConsumers = new SqlDataAdapter(this.cmd);
			try
			{
				this.dsConsumers.Clear();
				this.daConsumers.Fill(this.dsConsumers, "Consumers");
				this.dtConsumers = this.dsConsumers.Tables["Consumers"];
				this.dvConsumers = new DataView(this.dtConsumers);
				this.dvConsumers.RowFilter = "";
				try
				{
					DataColumn[] array = new DataColumn[2];
					array[0] = this.dtConsumers.Columns["f_UserFullName"];
					this.dtConsumers.PrimaryKey = array;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				this.dtConsumers.AcceptChanges();
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			this.loadGroupData();
			try
			{
				if (wgAppConfig.IsAccessDB)
				{
					this.cmd = new SqlCommand("SELECT [f_ShiftID] & '-' & [f_ShiftName] as f_ShiftFullName, [f_ShiftID] from t_b_ShiftSet order by f_ShiftID ASC", this.con);
				}
				else
				{
					this.cmd = new SqlCommand("SELECT CONVERT(nvarchar(50),[f_ShiftID]) + case when [f_ShiftName] IS NULL Then '' ELSE   '-' + [f_ShiftName] end  as f_ShiftFullName, [f_ShiftID] from t_b_ShiftSet order by f_ShiftID ASC", this.con);
				}
				try
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(this.cmd))
					{
						sqlDataAdapter.Fill(this.dsConsumers, "OptionalShift");
					}
					this.dtOptionalShift = this.dsConsumers.Tables["OptionalShift"];
					this.arrShiftID.Clear();
					this.lstOptionalShifts.Items.Clear();
					if (this.dtOptionalShift.Rows.Count > 0)
					{
						this.arrShiftID.Add(0);
						this.lstOptionalShifts.Items.Add("0*-" + CommonStr.strRest);
						for (int i = 0; i <= this.dtOptionalShift.Rows.Count - 1; i++)
						{
							this.lstOptionalShifts.Items.Add(this.dtOptionalShift.Rows[i][0]);
							this.arrShiftID.Add(this.dtOptionalShift.Rows[i][1]);
						}
					}
				}
				catch (Exception ex3)
				{
					wgTools.WgDebugWrite(ex3.ToString(), new object[0]);
				}
				finally
				{
					this.con.Close();
				}
			}
			catch (Exception ex4)
			{
				wgTools.WgDebugWrite(ex4.ToString(), new object[0]);
			}
		}

		private void _dataTableLoad_Acc()
		{
			OleDbConnection oleDbConnection = null;
			oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			this.dsConsumers = new DataSet("Users");
			string text = " SELECT t_b_Group.f_GroupName,t_b_Consumer.f_ConsumerID, t_b_Consumer.f_ConsumerName, LTRIM(([f_ConsumerNo]) +'- '+ [f_ConsumerName]) as [f_UserFullName]  FROM [t_b_Consumer]  LEFT OUTER JOIN t_b_Group ON  ( t_b_Group.f_GroupID = t_b_Consumer.f_GroupID ) WHERE f_AttendEnabled = 1 ";
			text += " AND f_ShiftEnabled > 0 ";
			OleDbCommand selectCommand = new OleDbCommand(text, oleDbConnection);
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
			try
			{
				this.dsConsumers.Clear();
				oleDbDataAdapter.Fill(this.dsConsumers, "Consumers");
				this.dtConsumers = this.dsConsumers.Tables["Consumers"];
				this.dvConsumers = new DataView(this.dtConsumers);
				this.dvConsumers.RowFilter = "";
				try
				{
					DataColumn[] array = new DataColumn[2];
					array[0] = this.dtConsumers.Columns["f_UserFullName"];
					this.dtConsumers.PrimaryKey = array;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				this.dtConsumers.AcceptChanges();
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			this.loadGroupData();
			try
			{
				if (wgAppConfig.IsAccessDB)
				{
					selectCommand = new OleDbCommand("SELECT [f_ShiftID] & '-' & [f_ShiftName] as f_ShiftFullName, [f_ShiftID] from t_b_ShiftSet order by f_ShiftID ASC", oleDbConnection);
				}
				else
				{
					selectCommand = new OleDbCommand("SELECT CONVERT(nvarchar(50),[f_ShiftID]) + case when [f_ShiftName] IS NULL Then '' ELSE   '-' + [f_ShiftName] end  as f_ShiftFullName, [f_ShiftID] from t_b_ShiftSet order by f_ShiftID ASC", oleDbConnection);
				}
				try
				{
					using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(selectCommand))
					{
						oleDbDataAdapter2.Fill(this.dsConsumers, "OptionalShift");
					}
					this.dtOptionalShift = this.dsConsumers.Tables["OptionalShift"];
					this.arrShiftID.Clear();
					this.lstOptionalShifts.Items.Clear();
					if (this.dtOptionalShift.Rows.Count > 0)
					{
						this.arrShiftID.Add(0);
						this.lstOptionalShifts.Items.Add("0*-" + CommonStr.strRest);
						for (int i = 0; i <= this.dtOptionalShift.Rows.Count - 1; i++)
						{
							this.lstOptionalShifts.Items.Add(this.dtOptionalShift.Rows[i][0]);
							this.arrShiftID.Add(this.dtOptionalShift.Rows[i][1]);
						}
					}
				}
				catch (Exception ex3)
				{
					wgTools.WgDebugWrite(ex3.ToString(), new object[0]);
				}
				finally
				{
					oleDbConnection.Close();
				}
			}
			catch (Exception ex4)
			{
				wgTools.WgDebugWrite(ex4.ToString(), new object[0]);
			}
		}

		private void lblShiftWeekday_update(int weekdayStart)
		{
			try
			{
				int num = weekdayStart;
				if (num >= 7)
				{
					num = 0;
				}
				this.lstShiftWeekday.Items.Clear();
				for (int i = 1; i <= 14; i++)
				{
					this.lstShiftWeekday.Items.Add(wgAppConfig.weekdayToChsName(num));
					num++;
					if (num >= 7)
					{
						num = 0;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmAutoShift_Load(object sender, EventArgs e)
		{
			try
			{
				this.Label3.Text = wgAppConfig.ReplaceFloorRomm(this.Label3.Text);
				base.KeyPreview = true;
				this._dataTableLoad();
				this.dtpStartDate.Value = DateTime.Now.Date;
				this.dtpEndDate.Value = DateTime.Now.Date;
				if (this.cbof_Group.Items.Count > 0)
				{
					this.cbof_Group.SelectedIndex = 0;
				}
				if (this.cbof_ConsumerName.Items.Count > 0)
				{
					this.cbof_ConsumerName.SelectedIndex = 0;
				}
				this.btnOK.Enabled = false;
				if (this.lstOptionalShifts.Items.Count == 0)
				{
					XMessageBox.Show(this, CommonStr.strNeedShift, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			wgAppConfig.setDisplayFormatDate(this.dtpStartDate, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpEndDate, wgTools.DisplayFormat_DateYMDWeek);
		}

		private void cbof_Group_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbof_Group.SelectedIndex == 0 && this.arrGroupID[0].ToString() == "0")
				{
					this.dvConsumers.RowFilter = "";
				}
				else
				{
					this.dvConsumers.RowFilter = string.Concat(new string[]
					{
						" (f_GroupName = '",
						this.cbof_Group.Text,
						"' ) OR (f_GroupName like '",
						this.cbof_Group.Text,
						"\\%')"
					});
				}
				this.cbof_ConsumerName.Items.Clear();
				this.cbof_ConsumerName.Items.Add(CommonStr.strAll);
				this.arrConsumerCMIndex.Add("");
				for (int i = 0; i <= this.dvConsumers.Count - 1; i++)
				{
					this.cbof_ConsumerName.Items.Add(this.dvConsumers[i]["f_UserFullName"]);
					this.arrConsumerCMIndex.Add(i);
				}
				if (this.cbof_ConsumerName.Items.Count > 0)
				{
					this.cbof_ConsumerName.SelectedIndex = 0;
				}
				if (this.dvConsumers.Count <= 0)
				{
					this.btnOK.Enabled = false;
				}
				else if (this.lstSelectedShifts.Items.Count > 0)
				{
					this.btnOK.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.dtpEndDate.MinDate = this.dtpStartDate.Value;
				this.lblStartWeekday.Text = CommonStr.strWeekday + wgAppConfig.weekdayToChsName((int)this.dtpStartDate.Value.DayOfWeek);
				this.lblShiftWeekday_update((int)this.dtpStartDate.Value.DayOfWeek);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnAddOne_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.lstOptionalShifts.SelectedIndex;
			if (selectedIndex >= 0)
			{
				this.lstSelectedShifts.Items.Add(this.lstOptionalShifts.Items[selectedIndex]);
				this.arrSelectedShiftID.Add(this.arrShiftID[selectedIndex]);
				if (this.lstSelectedShifts.Items.Count == 0 || this.dvConsumers.Count <= 0)
				{
					this.btnOK.Enabled = false;
					return;
				}
				this.btnOK.Enabled = true;
			}
		}

		private void btnDeleteOne_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.lstSelectedShifts.SelectedIndex;
			if (selectedIndex >= 0)
			{
				this.lstSelectedShifts.Items.RemoveAt(selectedIndex);
				this.arrSelectedShiftID.RemoveAt(selectedIndex);
				if (this.lstSelectedShifts.Items.Count == 0)
				{
					this.btnOK.Enabled = false;
					return;
				}
				this.btnOK.Enabled = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void lstOptionalShifts_DoubleClick(object sender, EventArgs e)
		{
			this.btnAddOne.PerformClick();
		}

		private void lstSelectedShifts_DoubleClick(object sender, EventArgs e)
		{
			this.btnDeleteOne.PerformClick();
		}

		private void btnDeleteAll_Click(object sender, EventArgs e)
		{
			this.lstSelectedShifts.Items.Clear();
			this.arrSelectedShiftID.Clear();
			if (this.lstSelectedShifts.Items.Count == 0)
			{
				this.btnOK.Enabled = false;
				return;
			}
			this.btnOK.Enabled = true;
		}

		private void dtpEndDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.lblEndWeekday.Text = CommonStr.strWeekday + wgAppConfig.weekdayToChsName((int)this.dtpEndDate.Value.DayOfWeek);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				if (this.arrSelectedShiftID.Count > 0)
				{
					int[] array = new int[this.arrSelectedShiftID.Count - 1 + 1];
					int[] array2;
					if (this.cbof_ConsumerName.Text == CommonStr.strAll)
					{
						if (this.dvConsumers.Count <= 0)
						{
							XMessageBox.Show(this, CommonStr.strSelectUser, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						array2 = new int[this.dvConsumers.Count - 1 + 1];
						for (int i = 0; i <= this.dvConsumers.Count - 1; i++)
						{
							array2[i] = (int)this.dvConsumers[i]["f_ConsumerID"];
						}
					}
					else
					{
						array2 = new int[]
						{
							(int)this.dvConsumers[this.cbof_ConsumerName.SelectedIndex - 1]["f_ConsumerID"]
						};
					}
					DateTime value = this.dtpStartDate.Value;
					DateTime value2 = this.dtpEndDate.Value;
					for (int i = 0; i <= this.arrSelectedShiftID.Count - 1; i++)
					{
						array[i] = (int)this.arrSelectedShiftID[i];
					}
					if (wgAppConfig.IsAccessDB)
					{
						using (comShift_Acc comShift_Acc = new comShift_Acc())
						{
							int num = comShift_Acc.shift_rule_checkValid(array.Length, array);
							if (num == 0)
							{
								this.ProgressBar1.Maximum = array2.Length;
								for (int i = 0; i <= array2.Length - 1; i++)
								{
									this.ProgressBar1.Value = i;
									num = comShift_Acc.shift_arrangeByRule(array2[i], value, value2, array.Length, array);
									if (num != 0)
									{
										XMessageBox.Show(this, comShift_Acc.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
										break;
									}
								}
								if (num == 0)
								{
									this.ProgressBar1.Value = this.ProgressBar1.Maximum;
									XMessageBox.Show(this, "OK!", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
							}
							else
							{
								XMessageBox.Show(this, comShift_Acc.errDesc(num) + "\r\n\r\n" + comShift_Acc.errInfo, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
							goto IL_2F4;
						}
					}
					using (comShift comShift = new comShift())
					{
						int num = comShift.shift_rule_checkValid(array.Length, array);
						if (num == 0)
						{
							this.ProgressBar1.Maximum = array2.Length;
							for (int i = 0; i <= array2.Length - 1; i++)
							{
								this.ProgressBar1.Value = i;
								num = comShift.shift_arrangeByRule(array2[i], value, value2, array.Length, array);
								if (num != 0)
								{
									XMessageBox.Show(this, comShift.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									break;
								}
							}
							if (num == 0)
							{
								this.ProgressBar1.Value = this.ProgressBar1.Maximum;
								XMessageBox.Show(this, "OK!", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
						else
						{
							XMessageBox.Show(this, comShift.errDesc(num) + "\r\n\r\n" + comShift.errInfo, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
				}
				IL_2F4:;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			this.ProgressBar1.Value = 0;
			Cursor.Current = Cursors.Default;
		}

		private void dfrmAutoShift_KeyDown(object sender, KeyEventArgs e)
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

		private void cbof_ConsumerName_Leave(object sender, EventArgs e)
		{
			this.checkUserValid(this.cbof_ConsumerName);
		}

		public bool checkUserValid(ComboBox cbo)
		{
			try
			{
				string value = cbo.Text.ToUpper();
				int num = cbo.SelectedIndex;
				bool result;
				if (num >= 0 && cbo.Text == cbo.Items[num].ToString())
				{
					result = true;
					return result;
				}
				num = -1;
				for (int i = 0; i < cbo.Items.Count; i++)
				{
					object objToStr = cbo.Items[i];
					if (Strings.UCase(wgTools.SetObjToStr(objToStr)).IndexOf(value) >= 0)
					{
						cbo.SelectedItem = cbo.Items[i];
						cbo.SelectedIndex = i;
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					cbo.SelectedIndex = num;
					result = true;
					return result;
				}
				XMessageBox.Show(this, CommonStr.strUserNonexisted, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				result = false;
				return result;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return false;
		}

		private void dfrmAutoShift_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
