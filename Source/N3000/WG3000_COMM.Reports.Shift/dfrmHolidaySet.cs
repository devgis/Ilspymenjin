using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmHolidaySet : frmN3000
	{
		private SqlConnection con;

		private SqlCommand cmd;

		private SqlDataAdapter da;

		private DataSet ds;

		private DataTable dt;

		private IContainer components;

		internal Button btnAddHoliday;

		internal Button btnDelHoliday;

		internal Button btnAddNeedWork;

		internal Button btnDelNeedWork;

		internal Button btnOK;

		internal RadioButton optSunWork2;

		internal RadioButton optSunWork0;

		internal RadioButton optSunWork1;

		internal GroupBox GroupBox1;

		internal RadioButton optSatWork2;

		internal RadioButton optSatWork0;

		internal RadioButton optSatWork1;

		internal GroupBox GroupBox2;

		internal Button btnCancel;

		private DataGridView dgvMain;

		private Label label1;

		private Label label2;

		private DataGridView dgvMain2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn From2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn To2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn f_Name;

		private DataGridViewTextBoxColumn f_from;

		private DataGridViewTextBoxColumn From1;

		private DataGridViewTextBoxColumn f_to;

		private DataGridViewTextBoxColumn To1;

		private DataGridViewTextBoxColumn f_Note;

		private DataGridViewTextBoxColumn f_No;

		private DataGridViewTextBoxColumn f_Value;

		public dfrmHolidaySet()
		{
			this.InitializeComponent();
		}

		private void dfrmHolidaySet_Load(object sender, EventArgs e)
		{
			this._dataTableLoad();
			bool flag = false;
			string funName = "mnuHolidaySet";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAddHoliday.Visible = false;
				this.btnAddNeedWork.Visible = false;
				this.btnDelHoliday.Visible = false;
				this.btnDelNeedWork.Visible = false;
				this.btnOK.Visible = false;
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
			this.ds = new DataSet("Holiday");
			try
			{
				this.ds.Clear();
				string str = "";
				str = " SELECT  t_a_Holiday.f_Name,";
				str += " [f_Value] AS f_from, ";
				str += " [f_Value1] AS f_from1, ";
				str += " [f_Value2] AS f_to, ";
				str += " [f_Value3] AS f_to1, ";
				str += " t_a_Holiday.f_Note, t_a_Holiday.f_No,t_a_Holiday.f_Value,f_EName,f_Value1,f_Value3";
				str += " FROM t_a_Holiday";
				string cmdText = str + " WHERE [f_Type]='1'";
				this.cmd = new SqlCommand(cmdText, this.con);
				this.da = new SqlDataAdapter(this.cmd);
				this.da.Fill(this.ds, "Holiday1");
				this.dt = this.ds.Tables["Holiday1"];
				using (comShift comShift = new comShift())
				{
					comShift.localizedHoliday(this.ds.Tables["Holiday1"]);
				}
				cmdText = str + " WHERE [f_Type]='2' ORDER BY [f_from] ASC ";
				this.cmd = new SqlCommand(cmdText, this.con);
				this.da = new SqlDataAdapter(this.cmd);
				this.da.Fill(this.ds, "Holiday2");
				using (comShift comShift2 = new comShift())
				{
					comShift2.localizedHoliday(this.ds.Tables["Holiday2"]);
				}
				cmdText = str + " WHERE [f_Type]='3' ORDER BY [f_from] ASC ";
				this.cmd = new SqlCommand(cmdText, this.con);
				this.da = new SqlDataAdapter(this.cmd);
				this.da.Fill(this.ds, "NeedWork");
				using (comShift comShift3 = new comShift())
				{
					comShift3.localizedHoliday(this.ds.Tables["NeedWork"]);
				}
				this.dgvMain.AutoGenerateColumns = false;
				this.dgvMain2.AutoGenerateColumns = false;
				this.dgvMain.DataSource = this.ds.Tables["Holiday2"];
				this.dgvMain2.DataSource = this.ds.Tables["NeedWork"];
				for (int i = 0; i < this.dgvMain.Columns.Count; i++)
				{
					this.dgvMain.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvMain2.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvMain.Columns[i].Name = this.dt.Columns[i].ColumnName;
					this.dgvMain2.Columns[i].Name = this.dt.Columns[i].ColumnName + "_NeedWork";
				}
				for (int i = 0; i < this.dgvMain.Rows.Count; i++)
				{
					this.dgvMain.Rows[i].Cells["f_from"].Value = DateTime.Parse(this.dgvMain.Rows[i].Cells["f_from"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
					this.dgvMain.Rows[i].Cells["f_to"].Value = DateTime.Parse(this.dgvMain.Rows[i].Cells["f_to"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
				}
				for (int i = 0; i < this.dgvMain2.Rows.Count; i++)
				{
					this.dgvMain2.Rows[i].Cells["f_from_NeedWork"].Value = DateTime.Parse(this.dgvMain2.Rows[i].Cells["f_from_NeedWork"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
					this.dgvMain2.Rows[i].Cells["f_to_NeedWork"].Value = DateTime.Parse(this.dgvMain2.Rows[i].Cells["f_to_NeedWork"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
				}
				for (int j = 0; j <= this.dt.Rows.Count - 1; j++)
				{
					DataRow dataRow = this.dt.Rows[j];
					if (Convert.ToInt32(dataRow["f_NO"]) == 1)
					{
						if (Convert.ToString(dataRow["f_Value"]) == "0")
						{
							this.optSatWork0.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "1")
						{
							this.optSatWork1.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "3")
						{
							this.optSatWork2.Checked = true;
						}
						else
						{
							this.optSatWork0.Checked = true;
						}
					}
					else if (Convert.ToInt32(dataRow["f_NO"]) == 2)
					{
						if (Convert.ToString(dataRow["f_Value"]) == "0")
						{
							this.optSunWork0.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "1")
						{
							this.optSunWork1.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "3")
						{
							this.optSunWork2.Checked = true;
						}
						else
						{
							this.optSunWork0.Checked = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void _dataTableLoad_Acc()
		{
			OleDbConnection connection = new OleDbConnection(wgAppConfig.dbConString);
			this.ds = new DataSet("Holiday");
			try
			{
				this.ds.Clear();
				string str = "";
				str = " SELECT  t_a_Holiday.f_Name,";
				str += " [f_Value] AS f_from, ";
				str += " [f_Value1] AS f_from1, ";
				str += " [f_Value2] AS f_to, ";
				str += " [f_Value3] AS f_to1, ";
				str += " t_a_Holiday.f_Note, t_a_Holiday.f_No,t_a_Holiday.f_Value,f_EName,f_Value1,f_Value3";
				str += " FROM t_a_Holiday";
				string cmdText = str + " WHERE [f_Type]='1'";
				OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "Holiday1");
				this.dt = this.ds.Tables["Holiday1"];
				using (comShift_Acc comShift_Acc = new comShift_Acc())
				{
					comShift_Acc.localizedHoliday(this.ds.Tables["Holiday1"]);
				}
				cmdText = str + " WHERE [f_Type]='2' ORDER BY [f_Value] ASC ";
				selectCommand = new OleDbCommand(cmdText, connection);
				oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "Holiday2");
				using (comShift_Acc comShift_Acc2 = new comShift_Acc())
				{
					comShift_Acc2.localizedHoliday(this.ds.Tables["Holiday2"]);
				}
				cmdText = str + " WHERE [f_Type]='3' ORDER BY [f_Value] ASC ";
				selectCommand = new OleDbCommand(cmdText, connection);
				oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "NeedWork");
				using (comShift_Acc comShift_Acc3 = new comShift_Acc())
				{
					comShift_Acc3.localizedHoliday(this.ds.Tables["NeedWork"]);
				}
				this.dgvMain.AutoGenerateColumns = false;
				this.dgvMain2.AutoGenerateColumns = false;
				this.dgvMain.DataSource = this.ds.Tables["Holiday2"];
				this.dgvMain2.DataSource = this.ds.Tables["NeedWork"];
				for (int i = 0; i < this.dgvMain.Columns.Count; i++)
				{
					this.dgvMain.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvMain2.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvMain.Columns[i].Name = this.dt.Columns[i].ColumnName;
					this.dgvMain2.Columns[i].Name = this.dt.Columns[i].ColumnName + "_NeedWork";
				}
				for (int i = 0; i < this.dgvMain.Rows.Count; i++)
				{
					this.dgvMain.Rows[i].Cells["f_from"].Value = DateTime.Parse(this.dgvMain.Rows[i].Cells["f_from"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
					this.dgvMain.Rows[i].Cells["f_to"].Value = DateTime.Parse(this.dgvMain.Rows[i].Cells["f_to"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
				}
				for (int i = 0; i < this.dgvMain2.Rows.Count; i++)
				{
					this.dgvMain2.Rows[i].Cells["f_from_NeedWork"].Value = DateTime.Parse(this.dgvMain2.Rows[i].Cells["f_from_NeedWork"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
					this.dgvMain2.Rows[i].Cells["f_to_NeedWork"].Value = DateTime.Parse(this.dgvMain2.Rows[i].Cells["f_to_NeedWork"].Value.ToString()).ToString(wgTools.DisplayFormat_DateYMDWeek);
				}
				for (int j = 0; j <= this.dt.Rows.Count - 1; j++)
				{
					DataRow dataRow = this.dt.Rows[j];
					if (Convert.ToInt32(dataRow["f_NO"]) == 1)
					{
						if (Convert.ToString(dataRow["f_Value"]) == "0")
						{
							this.optSatWork0.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "1")
						{
							this.optSatWork1.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "3")
						{
							this.optSatWork2.Checked = true;
						}
						else
						{
							this.optSatWork0.Checked = true;
						}
					}
					else if (Convert.ToInt32(dataRow["f_NO"]) == 2)
					{
						if (Convert.ToString(dataRow["f_Value"]) == "0")
						{
							this.optSunWork0.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "1")
						{
							this.optSunWork1.Checked = true;
						}
						else if (Convert.ToString(dataRow["f_Value"]) == "3")
						{
							this.optSunWork2.Checked = true;
						}
						else
						{
							this.optSunWork0.Checked = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnAddHoliday_Click(object sender, EventArgs e)
		{
			using (dfrmHolidayAdd dfrmHolidayAdd = new dfrmHolidayAdd())
			{
				dfrmHolidayAdd.ShowDialog(this);
			}
			this._dataTableLoad();
		}

		private void btnDelHoliday_Click(object sender, EventArgs e)
		{
			if (this.dgvMain.Rows.Count <= 0)
			{
				return;
			}
			int index = this.dgvMain.SelectedRows[0].Index;
			string strSql = " DELETE FROM t_a_Holiday WHERE [f_NO]= " + (this.dgvMain.DataSource as DataTable).Rows[index]["f_NO"].ToString();
			wgAppConfig.runUpdateSql(strSql);
			this._dataTableLoad();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string text = " UPDATE t_a_Holiday ";
			text += " SET [f_Value]=";
			if (this.optSatWork0.Checked)
			{
				text += wgTools.PrepareStr(0);
			}
			else if (this.optSatWork1.Checked)
			{
				text += wgTools.PrepareStr(1);
			}
			else if (this.optSatWork2.Checked)
			{
				text += wgTools.PrepareStr(3);
			}
			else
			{
				text += wgTools.PrepareStr(0);
			}
			text += " WHERE [f_NO]=1";
			wgAppConfig.runUpdateSql(text);
			text = " UPDATE t_a_Holiday ";
			text += " SET [f_Value]=";
			if (this.optSunWork0.Checked)
			{
				text += wgTools.PrepareStr(0);
			}
			else if (this.optSunWork1.Checked)
			{
				text += wgTools.PrepareStr(1);
			}
			else if (this.optSunWork2.Checked)
			{
				text += wgTools.PrepareStr(3);
			}
			else
			{
				text += wgTools.PrepareStr(0);
			}
			text += " WHERE [f_NO]=2";
			wgAppConfig.runUpdateSql(text);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnAddNeedWork_Click(object sender, EventArgs e)
		{
			using (dfrmHolidayAdd dfrmHolidayAdd = new dfrmHolidayAdd())
			{
				dfrmHolidayAdd.holidayType = "3";
				dfrmHolidayAdd.Text = CommonStr.strNeedToWork;
				dfrmHolidayAdd.ShowDialog(this);
			}
			this._dataTableLoad();
		}

		private void btnDelNeedWork_Click(object sender, EventArgs e)
		{
			if (this.dgvMain2.Rows.Count <= 0)
			{
				return;
			}
			int index = this.dgvMain2.SelectedRows[0].Index;
			string strSql = " DELETE FROM t_a_Holiday WHERE [f_NO]= " + (this.dgvMain2.DataSource as DataTable).Rows[index]["f_NO"].ToString();
			wgAppConfig.runUpdateSql(strSql);
			this._dataTableLoad();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmHolidaySet));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.btnAddHoliday = new Button();
			this.btnDelHoliday = new Button();
			this.btnAddNeedWork = new Button();
			this.btnDelNeedWork = new Button();
			this.btnOK = new Button();
			this.optSunWork2 = new RadioButton();
			this.optSunWork0 = new RadioButton();
			this.optSunWork1 = new RadioButton();
			this.GroupBox1 = new GroupBox();
			this.optSatWork2 = new RadioButton();
			this.optSatWork0 = new RadioButton();
			this.optSatWork1 = new RadioButton();
			this.GroupBox2 = new GroupBox();
			this.btnCancel = new Button();
			this.dgvMain = new DataGridView();
			this.f_Name = new DataGridViewTextBoxColumn();
			this.f_from = new DataGridViewTextBoxColumn();
			this.From1 = new DataGridViewTextBoxColumn();
			this.f_to = new DataGridViewTextBoxColumn();
			this.To1 = new DataGridViewTextBoxColumn();
			this.f_Note = new DataGridViewTextBoxColumn();
			this.f_No = new DataGridViewTextBoxColumn();
			this.f_Value = new DataGridViewTextBoxColumn();
			this.label1 = new Label();
			this.label2 = new Label();
			this.dgvMain2 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.From2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.To2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.GroupBox1.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			((ISupportInitialize)this.dgvMain).BeginInit();
			((ISupportInitialize)this.dgvMain2).BeginInit();
			base.SuspendLayout();
			this.btnAddHoliday.BackColor = Color.Transparent;
			this.btnAddHoliday.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddHoliday, "btnAddHoliday");
			this.btnAddHoliday.ForeColor = Color.White;
			this.btnAddHoliday.Name = "btnAddHoliday";
			this.btnAddHoliday.UseVisualStyleBackColor = false;
			this.btnAddHoliday.Click += new EventHandler(this.btnAddHoliday_Click);
			this.btnDelHoliday.BackColor = Color.Transparent;
			this.btnDelHoliday.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDelHoliday, "btnDelHoliday");
			this.btnDelHoliday.ForeColor = Color.White;
			this.btnDelHoliday.Name = "btnDelHoliday";
			this.btnDelHoliday.UseVisualStyleBackColor = false;
			this.btnDelHoliday.Click += new EventHandler(this.btnDelHoliday_Click);
			this.btnAddNeedWork.BackColor = Color.Transparent;
			this.btnAddNeedWork.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddNeedWork, "btnAddNeedWork");
			this.btnAddNeedWork.ForeColor = Color.White;
			this.btnAddNeedWork.Name = "btnAddNeedWork";
			this.btnAddNeedWork.UseVisualStyleBackColor = false;
			this.btnAddNeedWork.Click += new EventHandler(this.btnAddNeedWork_Click);
			this.btnDelNeedWork.BackColor = Color.Transparent;
			this.btnDelNeedWork.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDelNeedWork, "btnDelNeedWork");
			this.btnDelNeedWork.ForeColor = Color.White;
			this.btnDelNeedWork.Name = "btnDelNeedWork";
			this.btnDelNeedWork.UseVisualStyleBackColor = false;
			this.btnDelNeedWork.Click += new EventHandler(this.btnDelNeedWork_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.optSunWork2, "optSunWork2");
			this.optSunWork2.Name = "optSunWork2";
			this.optSunWork0.Checked = true;
			componentResourceManager.ApplyResources(this.optSunWork0, "optSunWork0");
			this.optSunWork0.Name = "optSunWork0";
			this.optSunWork0.TabStop = true;
			componentResourceManager.ApplyResources(this.optSunWork1, "optSunWork1");
			this.optSunWork1.Name = "optSunWork1";
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.optSatWork2);
			this.GroupBox1.Controls.Add(this.optSatWork0);
			this.GroupBox1.Controls.Add(this.optSatWork1);
			this.GroupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.optSatWork2, "optSatWork2");
			this.optSatWork2.Name = "optSatWork2";
			this.optSatWork0.Checked = true;
			componentResourceManager.ApplyResources(this.optSatWork0, "optSatWork0");
			this.optSatWork0.Name = "optSatWork0";
			this.optSatWork0.TabStop = true;
			componentResourceManager.ApplyResources(this.optSatWork1, "optSatWork1");
			this.optSatWork1.Name = "optSatWork1";
			this.GroupBox2.BackColor = Color.Transparent;
			this.GroupBox2.Controls.Add(this.optSunWork2);
			this.GroupBox2.Controls.Add(this.optSunWork0);
			this.GroupBox2.Controls.Add(this.optSunWork1);
			this.GroupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox2, "GroupBox2");
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.TabStop = false;
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
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
			this.dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_Name,
				this.f_from,
				this.From1,
				this.f_to,
				this.To1,
				this.f_Note,
				this.f_No,
				this.f_Value
			});
			this.dgvMain.EnableHeadersVisualStyles = false;
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.f_Name, "f_Name");
			this.f_Name.Name = "f_Name";
			this.f_Name.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_from, "f_from");
			this.f_from.Name = "f_from";
			this.f_from.ReadOnly = true;
			componentResourceManager.ApplyResources(this.From1, "From1");
			this.From1.Name = "From1";
			this.From1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_to, "f_to");
			this.f_to.Name = "f_to";
			this.f_to.ReadOnly = true;
			componentResourceManager.ApplyResources(this.To1, "To1");
			this.To1.Name = "To1";
			this.To1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Note, "f_Note");
			this.f_Note.Name = "f_Note";
			this.f_Note.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_No, "f_No");
			this.f_No.Name = "f_No";
			this.f_No.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Value, "f_Value");
			this.f_Value.Name = "f_Value";
			this.f_Value.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			this.dgvMain2.AllowUserToAddRows = false;
			this.dgvMain2.AllowUserToDeleteRows = false;
			this.dgvMain2.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dgvMain2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvMain2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvMain2.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.From2,
				this.dataGridViewTextBoxColumn3,
				this.To2,
				this.dataGridViewTextBoxColumn4,
				this.dataGridViewTextBoxColumn5,
				this.dataGridViewTextBoxColumn6
			});
			this.dgvMain2.EnableHeadersVisualStyles = false;
			componentResourceManager.ApplyResources(this.dgvMain2, "dgvMain2");
			this.dgvMain2.Name = "dgvMain2";
			this.dgvMain2.ReadOnly = true;
			this.dgvMain2.RowTemplate.Height = 23;
			this.dgvMain2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.From2, "From2");
			this.From2.Name = "From2";
			this.From2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.To2, "To2");
			this.To2.Name = "To2";
			this.To2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.dgvMain2);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.btnAddHoliday);
			base.Controls.Add(this.btnDelHoliday);
			base.Controls.Add(this.btnAddNeedWork);
			base.Controls.Add(this.btnDelNeedWork);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.GroupBox2);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmHolidaySet";
			base.Load += new EventHandler(this.dfrmHolidaySet_Load);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			((ISupportInitialize)this.dgvMain).EndInit();
			((ISupportInitialize)this.dgvMain2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
