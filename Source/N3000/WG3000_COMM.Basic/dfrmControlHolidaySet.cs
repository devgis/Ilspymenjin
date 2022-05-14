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

namespace WG3000_COMM.Basic
{
	public class dfrmControlHolidaySet : frmN3000
	{
		private IContainer components;

		internal Button btnAddHoliday;

		internal Button btnDelHoliday;

		internal Button btnCancel;

		private DataGridView dgvMain;

		private Label label1;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private DataGridView dgvNeedWork;

		internal Button btnAddNeedWorkDay;

		internal Button btnDelNeedWorkDay;

		private Label label2;

		private DataGridViewTextBoxColumn f_No;

		private DataGridViewTextBoxColumn f_from;

		private DataGridViewTextBoxColumn f_to;

		private DataGridViewTextBoxColumn f_Note;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataView dvHolidays;

		private DataView dvNeedWork;

		private DataTable dt;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControlHolidaySet));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.groupBox2 = new GroupBox();
			this.label2 = new Label();
			this.dgvNeedWork = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.btnAddNeedWorkDay = new Button();
			this.btnDelNeedWorkDay = new Button();
			this.groupBox1 = new GroupBox();
			this.dgvMain = new DataGridView();
			this.f_No = new DataGridViewTextBoxColumn();
			this.f_from = new DataGridViewTextBoxColumn();
			this.f_to = new DataGridViewTextBoxColumn();
			this.f_Note = new DataGridViewTextBoxColumn();
			this.btnAddHoliday = new Button();
			this.btnDelHoliday = new Button();
			this.label1 = new Label();
			this.btnCancel = new Button();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.dgvNeedWork).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dgvMain).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.dgvNeedWork);
			this.groupBox2.Controls.Add(this.btnAddNeedWorkDay);
			this.groupBox2.Controls.Add(this.btnDelNeedWorkDay);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			this.dgvNeedWork.AllowUserToAddRows = false;
			this.dgvNeedWork.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dgvNeedWork, "dgvNeedWork");
			this.dgvNeedWork.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvNeedWork.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvNeedWork.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvNeedWork.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4
			});
			this.dgvNeedWork.EnableHeadersVisualStyles = false;
			this.dgvNeedWork.Name = "dgvNeedWork";
			this.dgvNeedWork.ReadOnly = true;
			this.dgvNeedWork.RowTemplate.Height = 23;
			this.dgvNeedWork.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.btnAddNeedWorkDay.BackColor = Color.Transparent;
			this.btnAddNeedWorkDay.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddNeedWorkDay, "btnAddNeedWorkDay");
			this.btnAddNeedWorkDay.ForeColor = Color.White;
			this.btnAddNeedWorkDay.Name = "btnAddNeedWorkDay";
			this.btnAddNeedWorkDay.UseVisualStyleBackColor = false;
			this.btnAddNeedWorkDay.Click += new EventHandler(this.btnAddNeedWorkDay_Click);
			this.btnDelNeedWorkDay.BackColor = Color.Transparent;
			this.btnDelNeedWorkDay.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDelNeedWorkDay, "btnDelNeedWorkDay");
			this.btnDelNeedWorkDay.ForeColor = Color.White;
			this.btnDelNeedWorkDay.Name = "btnDelNeedWorkDay";
			this.btnDelNeedWorkDay.UseVisualStyleBackColor = false;
			this.btnDelNeedWorkDay.Click += new EventHandler(this.btnDelNeedWorkDay_Click);
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this.dgvMain);
			this.groupBox1.Controls.Add(this.btnAddHoliday);
			this.groupBox1.Controls.Add(this.btnDelHoliday);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
			this.dgvMain.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvMain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_No,
				this.f_from,
				this.f_to,
				this.f_Note
			});
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.f_No, "f_No");
			this.f_No.Name = "f_No";
			this.f_No.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_from, "f_from");
			this.f_from.Name = "f_from";
			this.f_from.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_to, "f_to");
			this.f_to.Name = "f_to";
			this.f_to.ReadOnly = true;
			this.f_Note.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Note, "f_Note");
			this.f_Note.Name = "f_Note";
			this.f_Note.ReadOnly = true;
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
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmControlHolidaySet";
			base.Load += new EventHandler(this.dfrmHolidaySet_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.dgvNeedWork).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dgvMain).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmControlHolidaySet()
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
				this.btnDelHoliday.Visible = false;
			}
		}

		private void _dataTableLoad()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this._dataTableLoad_Acc();
				return;
			}
			SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				string cmdText = "SELECT f_Id, f_BeginYMDHMS, f_EndYMDHMS, f_Notes,f_forcework From t_b_ControlHolidays ";
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						this.dt = new DataTable();
						sqlDataAdapter.Fill(this.dt);
						this.dvHolidays = new DataView(this.dt);
						this.dvHolidays.RowFilter = " f_forcework <> 1";
						this.dgvMain.AutoGenerateColumns = false;
						this.dgvMain.DataSource = this.dvHolidays;
						this.dvHolidays.Sort = "f_BeginYMDHMS ASC";
						for (int i = 0; i < this.dvHolidays.Table.Columns.Count; i++)
						{
							this.dgvMain.Columns[i].DataPropertyName = this.dvHolidays.Table.Columns[i].ColumnName;
							this.dgvMain.Columns[i].Name = this.dvHolidays.Table.Columns[i].ColumnName;
							if (this.dgvMain.ColumnCount == i + 1)
							{
								break;
							}
						}
						this.dvNeedWork = new DataView(this.dt);
						this.dvNeedWork.RowFilter = " f_forcework = 1";
						this.dgvNeedWork.AutoGenerateColumns = false;
						this.dgvNeedWork.DataSource = this.dvNeedWork;
						this.dvNeedWork.Sort = "f_BeginYMDHMS ASC";
						for (int j = 0; j < this.dvNeedWork.Table.Columns.Count; j++)
						{
							this.dgvNeedWork.Columns[j].DataPropertyName = this.dvNeedWork.Table.Columns[j].ColumnName;
							this.dgvNeedWork.Columns[j].Name = this.dvNeedWork.Table.Columns[j].ColumnName;
							if (this.dgvNeedWork.ColumnCount == j + 1)
							{
								break;
							}
						}
					}
				}
				wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_BeginYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_EndYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				wgAppConfig.setDisplayFormatDate(this.dgvNeedWork, "f_BeginYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				wgAppConfig.setDisplayFormatDate(this.dgvNeedWork, "f_EndYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				this.dgvMain.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvNeedWork.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvMain.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvNeedWork.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				sqlConnection.Dispose();
			}
		}

		private void _dataTableLoad_Acc()
		{
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				string cmdText = "SELECT f_Id, f_BeginYMDHMS, f_EndYMDHMS, f_Notes,f_forcework From t_b_ControlHolidays ";
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						this.dt = new DataTable();
						oleDbDataAdapter.Fill(this.dt);
						this.dvHolidays = new DataView(this.dt);
						this.dvHolidays.RowFilter = " f_forcework <> 1";
						this.dgvMain.AutoGenerateColumns = false;
						this.dgvMain.DataSource = this.dvHolidays;
						this.dvHolidays.Sort = "f_BeginYMDHMS ASC";
						for (int i = 0; i < this.dvHolidays.Table.Columns.Count; i++)
						{
							this.dgvMain.Columns[i].DataPropertyName = this.dvHolidays.Table.Columns[i].ColumnName;
							this.dgvMain.Columns[i].Name = this.dvHolidays.Table.Columns[i].ColumnName;
							if (this.dgvMain.ColumnCount == i + 1)
							{
								break;
							}
						}
						this.dvNeedWork = new DataView(this.dt);
						this.dvNeedWork.RowFilter = " f_forcework = 1";
						this.dgvNeedWork.AutoGenerateColumns = false;
						this.dgvNeedWork.DataSource = this.dvNeedWork;
						this.dvNeedWork.Sort = "f_BeginYMDHMS ASC";
						for (int j = 0; j < this.dvNeedWork.Table.Columns.Count; j++)
						{
							this.dgvNeedWork.Columns[j].DataPropertyName = this.dvNeedWork.Table.Columns[j].ColumnName;
							this.dgvNeedWork.Columns[j].Name = this.dvNeedWork.Table.Columns[j].ColumnName;
							if (this.dgvNeedWork.ColumnCount == j + 1)
							{
								break;
							}
						}
					}
				}
				wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_BeginYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				wgAppConfig.setDisplayFormatDate(this.dgvMain, "f_EndYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				wgAppConfig.setDisplayFormatDate(this.dgvNeedWork, "f_BeginYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				wgAppConfig.setDisplayFormatDate(this.dgvNeedWork, "f_EndYMDHMS", wgTools.DisplayFormat_DateYMDHMSWeek);
				this.dgvMain.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvNeedWork.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvMain.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				this.dgvNeedWork.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				oleDbConnection.Dispose();
			}
		}

		private void btnAddHoliday_Click(object sender, EventArgs e)
		{
			using (dfrmControlHolidayAdd dfrmControlHolidayAdd = new dfrmControlHolidayAdd())
			{
				dfrmControlHolidayAdd.ShowDialog(this);
			}
			this._dataTableLoad();
		}

		private void btnDelHoliday_Click(object sender, EventArgs e)
		{
			if (this.dgvMain.Rows.Count <= 0)
			{
				return;
			}
			int arg_2A_0 = this.dgvMain.SelectedRows[0].Index;
			string strSql = " DELETE FROM t_b_ControlHolidays WHERE [f_Id]= " + this.dgvMain.SelectedRows[0].Cells["f_Id"].Value.ToString();
			wgAppConfig.runUpdateSql(strSql);
			this._dataTableLoad();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnDelNeedWorkDay_Click(object sender, EventArgs e)
		{
			if (this.dgvNeedWork.Rows.Count <= 0)
			{
				return;
			}
			int arg_2A_0 = this.dgvNeedWork.SelectedRows[0].Index;
			string strSql = " DELETE FROM t_b_ControlHolidays WHERE [f_Id]= " + this.dgvNeedWork.SelectedRows[0].Cells["f_Id"].Value.ToString();
			wgAppConfig.runUpdateSql(strSql);
			this._dataTableLoad();
		}

		private void btnAddNeedWorkDay_Click(object sender, EventArgs e)
		{
			using (dfrmControlHolidayAdd dfrmControlHolidayAdd = new dfrmControlHolidayAdd())
			{
				dfrmControlHolidayAdd.Text = this.groupBox2.Text;
				dfrmControlHolidayAdd.bHoliday = false;
				dfrmControlHolidayAdd.ShowDialog(this);
			}
			this._dataTableLoad();
		}
	}
}
