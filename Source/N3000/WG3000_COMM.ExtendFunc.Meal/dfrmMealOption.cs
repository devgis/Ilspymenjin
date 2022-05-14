using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Meal
{
	public class dfrmMealOption : frmN3000
	{
		private IContainer components;

		internal Button cmdCancel;

		internal Button cmdOK;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private Label label4;

		private NumericUpDown nudCost;

		private DataGridView dgvSelected;

		internal Label Label10;

		private DataGridView dgvOptional;

		internal Button btnDeleteAllReaders;

		internal Label Label11;

		internal Button btnDeleteOneReader;

		internal Button btnAddAllReaders;

		internal Button btnAddOneReader;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn Cost;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		private DataGridViewTextBoxColumn f_Cost;

		private string strMealCon = "";

		private DataSet ds = new DataSet("dsMeal");

		private DataView dv;

		private DataView dvSelected;

		private DataTable dt;

		public int mealNo = -1;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMealOption));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.label4 = new Label();
			this.nudCost = new NumericUpDown();
			this.dgvSelected = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.Cost = new DataGridViewTextBoxColumn();
			this.Label10 = new Label();
			this.dgvOptional = new DataGridView();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.f_Cost = new DataGridViewTextBoxColumn();
			this.btnDeleteAllReaders = new Button();
			this.Label11 = new Label();
			this.btnDeleteOneReader = new Button();
			this.btnAddAllReaders = new Button();
			this.btnAddOneReader = new Button();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.nudCost).BeginInit();
			((ISupportInitialize)this.dgvSelected).BeginInit();
			((ISupportInitialize)this.dgvOptional).BeginInit();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage1);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabPage1.BackgroundImage = Resources.pMain_content_bkg;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.nudCost);
			this.tabPage1.Controls.Add(this.dgvSelected);
			this.tabPage1.Controls.Add(this.Label10);
			this.tabPage1.Controls.Add(this.dgvOptional);
			this.tabPage1.Controls.Add(this.btnDeleteAllReaders);
			this.tabPage1.Controls.Add(this.Label11);
			this.tabPage1.Controls.Add(this.btnDeleteOneReader);
			this.tabPage1.Controls.Add(this.btnAddAllReaders);
			this.tabPage1.Controls.Add(this.btnAddOneReader);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			this.nudCost.DecimalPlaces = 2;
			componentResourceManager.ApplyResources(this.nudCost, "nudCost");
			this.nudCost.Name = "nudCost";
			NumericUpDown arg_33A_0 = this.nudCost;
			int[] array = new int[4];
			array[0] = 5;
			arg_33A_0.Value = new decimal(array);
			this.dgvSelected.AllowUserToAddRows = false;
			this.dgvSelected.AllowUserToDeleteRows = false;
			this.dgvSelected.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvSelected, "dgvSelected");
			this.dgvSelected.BackgroundColor = Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelected.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelected.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelected.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.Cost
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelected.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelected.EnableHeadersVisualStyles = false;
			this.dgvSelected.Name = "dgvSelected";
			this.dgvSelected.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvSelected.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvSelected.RowTemplate.Height = 23;
			this.dgvSelected.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSelected.DoubleClick += new EventHandler(this.btnDeleteOneReader_Click);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Cost, "Cost");
			this.Cost.Name = "Cost";
			this.Cost.ReadOnly = true;
			this.Label10.BackColor = Color.Transparent;
			this.Label10.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label10, "Label10");
			this.Label10.Name = "Label10";
			this.dgvOptional.AllowUserToAddRows = false;
			this.dgvOptional.AllowUserToDeleteRows = false;
			this.dgvOptional.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvOptional, "dgvOptional");
			this.dgvOptional.BackgroundColor = Color.White;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.dgvOptional.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvOptional.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvOptional.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.f_Selected,
				this.f_Cost
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Window;
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.dgvOptional.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvOptional.EnableHeadersVisualStyles = false;
			this.dgvOptional.Name = "dgvOptional";
			this.dgvOptional.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvOptional.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvOptional.RowTemplate.Height = 23;
			this.dgvOptional.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvOptional.DoubleClick += new EventHandler(this.btnAddOneReader_Click);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected, "f_Selected");
			this.f_Selected.Name = "f_Selected";
			this.f_Selected.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Cost, "f_Cost");
			this.f_Cost.Name = "f_Cost";
			this.f_Cost.ReadOnly = true;
			this.btnDeleteAllReaders.BackColor = Color.Transparent;
			this.btnDeleteAllReaders.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteAllReaders, "btnDeleteAllReaders");
			this.btnDeleteAllReaders.ForeColor = Color.White;
			this.btnDeleteAllReaders.Name = "btnDeleteAllReaders";
			this.btnDeleteAllReaders.UseVisualStyleBackColor = false;
			this.btnDeleteAllReaders.Click += new EventHandler(this.btnDeleteAllReaders_Click);
			this.Label11.BackColor = Color.Transparent;
			this.Label11.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.Name = "Label11";
			this.btnDeleteOneReader.BackColor = Color.Transparent;
			this.btnDeleteOneReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDeleteOneReader, "btnDeleteOneReader");
			this.btnDeleteOneReader.ForeColor = Color.White;
			this.btnDeleteOneReader.Name = "btnDeleteOneReader";
			this.btnDeleteOneReader.UseVisualStyleBackColor = false;
			this.btnDeleteOneReader.Click += new EventHandler(this.btnDeleteOneReader_Click);
			this.btnAddAllReaders.BackColor = Color.Transparent;
			this.btnAddAllReaders.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddAllReaders, "btnAddAllReaders");
			this.btnAddAllReaders.ForeColor = Color.White;
			this.btnAddAllReaders.Name = "btnAddAllReaders";
			this.btnAddAllReaders.UseVisualStyleBackColor = false;
			this.btnAddAllReaders.Click += new EventHandler(this.btnAddAllReaders_Click);
			this.btnAddOneReader.BackColor = Color.Transparent;
			this.btnAddOneReader.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddOneReader, "btnAddOneReader");
			this.btnAddOneReader.ForeColor = Color.White;
			this.btnAddOneReader.Name = "btnAddOneReader";
			this.btnAddOneReader.UseVisualStyleBackColor = false;
			this.btnAddOneReader.Click += new EventHandler(this.btnAddOneReader_Click);
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Name = "dfrmMealOption";
			base.Load += new EventHandler(this.dfrmMealOption_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((ISupportInitialize)this.nudCost).EndInit();
			((ISupportInitialize)this.dgvSelected).EndInit();
			((ISupportInitialize)this.dgvOptional).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmMealOption()
		{
			this.InitializeComponent();
		}

		private void dfrmMealOption_Load(object sender, EventArgs e)
		{
			switch (this.mealNo)
			{
			case 0:
				this.strMealCon = "f_CostMorning";
				break;
			case 1:
				this.strMealCon = "f_CostLunch";
				break;
			case 2:
				this.strMealCon = "f_CostEvening";
				break;
			case 3:
				this.strMealCon = "f_CostOther";
				break;
			default:
				return;
			}
			this.loadData();
			if (this.dgvOptional.Rows.Count == 0 && this.dgvSelected.Rows.Count == 0)
			{
				XMessageBox.Show(CommonStr.strMealPremote);
				base.Close();
			}
		}

		public void loadData_Acc()
		{
			OleDbConnection connection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				string cmdText = string.Format("Select t_d_Reader4Meal.f_ReaderID, f_ReaderName, IIF(ISNULL({0}),0, IIF({0} >=0,1,0)) as f_Selected,{0} as f_Cost from t_b_reader,t_d_Reader4Meal  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_Reader4Meal.f_ReaderID ", this.strMealCon);
				OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				oleDbDataAdapter.Fill(this.ds, "optionalReader");
				this.dv = new DataView(this.ds.Tables["optionalReader"]);
				this.dv.RowFilter = " f_Selected = 0";
				this.dvSelected = new DataView(this.ds.Tables["optionalReader"]);
				this.dvSelected.RowFilter = " f_Selected = 1";
				this.dt = this.ds.Tables["optionalReader"];
				try
				{
					DataColumn[] primaryKey = new DataColumn[]
					{
						this.dt.Columns[0]
					};
					this.dt.PrimaryKey = primaryKey;
				}
				catch (Exception)
				{
					throw;
				}
				for (int i = 0; i < this.dgvOptional.Columns.Count; i++)
				{
					this.dgvOptional.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvSelected.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
				}
				this.dv.RowFilter = "f_Selected = 0";
				this.dvSelected.RowFilter = "f_Selected > 0";
				this.dgvOptional.AutoGenerateColumns = false;
				this.dgvOptional.DataSource = this.dv;
				this.dgvSelected.AutoGenerateColumns = false;
				this.dgvSelected.DataSource = this.dvSelected;
				this.dgvOptional.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvSelected.DefaultCellStyle.ForeColor = Color.Black;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public void loadData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadData_Acc();
				return;
			}
			SqlConnection connection = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				string cmdText = string.Format("Select t_d_Reader4Meal.f_ReaderID, f_ReaderName, CASE WHEN {0}  IS NULL  THEN 0 ELSE (CASE WHEN {0} >=0 THEN 1 ELSE 0 END ) END  as f_Selected,{0} as f_Cost from t_b_reader,t_d_Reader4Meal  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND    t_b_reader.f_ReaderID  = t_d_Reader4Meal.f_ReaderID ", this.strMealCon);
				SqlCommand selectCommand = new SqlCommand(cmdText, connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
				sqlDataAdapter.Fill(this.ds, "optionalReader");
				this.dv = new DataView(this.ds.Tables["optionalReader"]);
				this.dv.RowFilter = " f_Selected = 0";
				this.dvSelected = new DataView(this.ds.Tables["optionalReader"]);
				this.dvSelected.RowFilter = " f_Selected = 1";
				this.dt = this.ds.Tables["optionalReader"];
				try
				{
					DataColumn[] primaryKey = new DataColumn[]
					{
						this.dt.Columns[0]
					};
					this.dt.PrimaryKey = primaryKey;
				}
				catch (Exception)
				{
					throw;
				}
				for (int i = 0; i < this.dgvOptional.Columns.Count; i++)
				{
					this.dgvOptional.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
					this.dgvSelected.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
				}
				this.dv.RowFilter = "f_Selected = 0";
				this.dvSelected.RowFilter = "f_Selected > 0";
				this.dgvOptional.AutoGenerateColumns = false;
				this.dgvOptional.DataSource = this.dv;
				this.dgvSelected.AutoGenerateColumns = false;
				this.dgvSelected.DataSource = this.dvSelected;
				this.dgvOptional.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvSelected.DefaultCellStyle.ForeColor = Color.Black;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.cmdOK_Click_Acc(sender, e);
		}

		private void cmdOK_Click_Acc(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				Cursor arg_10_0 = Cursor.Current;
				string strSql = string.Format(" Update t_d_Reader4Meal SET {0} = -1", this.strMealCon);
				wgAppConfig.runUpdateSql(strSql);
				if (this.dvSelected.Count > 0)
				{
					for (int i = 0; i <= this.dvSelected.Count - 1; i++)
					{
						strSql = string.Format(" Update t_d_Reader4Meal SET {0} = {1} WHERE f_ReaderID ={2}", this.strMealCon, this.dvSelected[i]["f_Cost"].ToString(), this.dvSelected[i]["f_ReaderID"].ToString());
						wgAppConfig.runUpdateSql(strSql);
					}
				}
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
			this.Cursor = Cursors.Default;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnAddAllReaders_Click(object sender, EventArgs e)
		{
			this.dgvOptional.SelectAll();
			wgAppConfig.selectObject(this.dgvOptional, "f_cost", this.nudCost.Value.ToString());
			this.dgvOptional.ClearSelection();
		}

		private void btnAddOneReader_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvOptional, "f_cost", this.nudCost.Value.ToString());
		}

		private void btnDeleteOneReader_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelected);
		}

		private void btnDeleteAllReaders_Click(object sender, EventArgs e)
		{
			this.dgvSelected.SelectAll();
			wgAppConfig.deselectObject(this.dgvSelected);
		}
	}
}
