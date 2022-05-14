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
	public class dfrmMealSetup : frmN3000
	{
		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private DataGridView dgvSelected;

		private DataGridView dgvOptional;

		internal Label Label11;

		internal Button btnAddAllReaders;

		internal Label Label10;

		internal Button btnAddOneReader;

		internal Button btnDeleteOneReader;

		internal Button btnDeleteAllReaders;

		private TabPage tabPage3;

		internal Button btnOK;

		internal Button btnCancel;

		private CheckBox chkAllowableSwipe;

		private Label label1;

		private NumericUpDown nudRuleSeconds;

		private RadioButton radioButton3;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private Button btnEdit;

		private Button btnDel;

		private Button btnAdd;

		private CheckBox chkOtherMeal;

		private DateTimePicker dateBeginHMS4;

		private DateTimePicker dateEndHMS4;

		private NumericUpDown nudOther;

		private Label lblOther;

		private CheckBox chkEveningMeal;

		private DateTimePicker dateBeginHMS3;

		private DateTimePicker dateEndHMS3;

		private NumericUpDown nudEvening;

		private Label lblEvening;

		private CheckBox chkLunchMeal;

		private DateTimePicker dateBeginHMS2;

		private DateTimePicker dateEndHMS2;

		private NumericUpDown nudLunch;

		private Label lblLunch;

		private CheckBox chkMorningMeal;

		private DateTimePicker dateBeginHMS1;

		private Label label3;

		private DateTimePicker dateEndHMS1;

		private NumericUpDown nudMorning;

		private Label lblMorning;

		private Label label85;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		private Button btnOption0;

		private Button btnOption3;

		private Button btnOption2;

		private Button btnOption1;

		private DataSet ds = new DataSet("dsMeal");

		private DataView dv;

		private DataView dvSelected;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMealSetup));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.dgvSelected = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.Label10 = new Label();
			this.dgvOptional = new DataGridView();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.btnDeleteAllReaders = new Button();
			this.Label11 = new Label();
			this.btnDeleteOneReader = new Button();
			this.btnAddAllReaders = new Button();
			this.btnAddOneReader = new Button();
			this.tabPage2 = new TabPage();
			this.chkAllowableSwipe = new CheckBox();
			this.label1 = new Label();
			this.nudRuleSeconds = new NumericUpDown();
			this.radioButton3 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.tabPage3 = new TabPage();
			this.btnOption3 = new Button();
			this.btnOption2 = new Button();
			this.btnOption1 = new Button();
			this.btnOption0 = new Button();
			this.chkOtherMeal = new CheckBox();
			this.dateBeginHMS4 = new DateTimePicker();
			this.dateEndHMS4 = new DateTimePicker();
			this.nudOther = new NumericUpDown();
			this.lblOther = new Label();
			this.chkEveningMeal = new CheckBox();
			this.dateBeginHMS3 = new DateTimePicker();
			this.dateEndHMS3 = new DateTimePicker();
			this.nudEvening = new NumericUpDown();
			this.lblEvening = new Label();
			this.chkLunchMeal = new CheckBox();
			this.dateBeginHMS2 = new DateTimePicker();
			this.dateEndHMS2 = new DateTimePicker();
			this.nudLunch = new NumericUpDown();
			this.lblLunch = new Label();
			this.chkMorningMeal = new CheckBox();
			this.dateBeginHMS1 = new DateTimePicker();
			this.label3 = new Label();
			this.dateEndHMS1 = new DateTimePicker();
			this.nudMorning = new NumericUpDown();
			this.lblMorning = new Label();
			this.label85 = new Label();
			this.btnEdit = new Button();
			this.btnDel = new Button();
			this.btnAdd = new Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.dgvSelected).BeginInit();
			((ISupportInitialize)this.dgvOptional).BeginInit();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.nudRuleSeconds).BeginInit();
			this.tabPage3.SuspendLayout();
			((ISupportInitialize)this.nudOther).BeginInit();
			((ISupportInitialize)this.nudEvening).BeginInit();
			((ISupportInitialize)this.nudLunch).BeginInit();
			((ISupportInitialize)this.nudMorning).BeginInit();
			base.SuspendLayout();
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabPage1.BackgroundImage = Resources.pMain_content_bkg;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
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
				this.dataGridViewTextBoxColumn3
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
				this.f_Selected
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
			this.tabPage2.BackgroundImage = Resources.pMain_content_bkg;
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Controls.Add(this.chkAllowableSwipe);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.nudRuleSeconds);
			this.tabPage2.Controls.Add(this.radioButton3);
			this.tabPage2.Controls.Add(this.radioButton2);
			this.tabPage2.Controls.Add(this.radioButton1);
			this.tabPage2.ForeColor = Color.White;
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkAllowableSwipe, "chkAllowableSwipe");
			this.chkAllowableSwipe.Name = "chkAllowableSwipe";
			this.chkAllowableSwipe.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.nudRuleSeconds, "nudRuleSeconds");
			this.nudRuleSeconds.Name = "nudRuleSeconds";
			NumericUpDown arg_E80_0 = this.nudRuleSeconds;
			int[] array = new int[4];
			array[0] = 60;
			arg_E80_0.Value = new decimal(array);
			componentResourceManager.ApplyResources(this.radioButton3, "radioButton3");
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.Checked = true;
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.tabPage3.BackgroundImage = Resources.pMain_content_bkg;
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Controls.Add(this.btnOption3);
			this.tabPage3.Controls.Add(this.btnOption2);
			this.tabPage3.Controls.Add(this.btnOption1);
			this.tabPage3.Controls.Add(this.btnOption0);
			this.tabPage3.Controls.Add(this.chkOtherMeal);
			this.tabPage3.Controls.Add(this.dateBeginHMS4);
			this.tabPage3.Controls.Add(this.dateEndHMS4);
			this.tabPage3.Controls.Add(this.nudOther);
			this.tabPage3.Controls.Add(this.lblOther);
			this.tabPage3.Controls.Add(this.chkEveningMeal);
			this.tabPage3.Controls.Add(this.dateBeginHMS3);
			this.tabPage3.Controls.Add(this.dateEndHMS3);
			this.tabPage3.Controls.Add(this.nudEvening);
			this.tabPage3.Controls.Add(this.lblEvening);
			this.tabPage3.Controls.Add(this.chkLunchMeal);
			this.tabPage3.Controls.Add(this.dateBeginHMS2);
			this.tabPage3.Controls.Add(this.dateEndHMS2);
			this.tabPage3.Controls.Add(this.nudLunch);
			this.tabPage3.Controls.Add(this.lblLunch);
			this.tabPage3.Controls.Add(this.chkMorningMeal);
			this.tabPage3.Controls.Add(this.dateBeginHMS1);
			this.tabPage3.Controls.Add(this.label3);
			this.tabPage3.Controls.Add(this.dateEndHMS1);
			this.tabPage3.Controls.Add(this.nudMorning);
			this.tabPage3.Controls.Add(this.lblMorning);
			this.tabPage3.Controls.Add(this.label85);
			this.tabPage3.Controls.Add(this.btnEdit);
			this.tabPage3.Controls.Add(this.btnDel);
			this.tabPage3.Controls.Add(this.btnAdd);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.btnOption3.BackColor = Color.Transparent;
			this.btnOption3.BackgroundImage = Resources.pMain_button_normal;
			this.btnOption3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnOption3, "btnOption3");
			this.btnOption3.Name = "btnOption3";
			this.btnOption3.UseVisualStyleBackColor = false;
			this.btnOption3.Click += new EventHandler(this.btnOption3_Click);
			this.btnOption2.BackColor = Color.Transparent;
			this.btnOption2.BackgroundImage = Resources.pMain_button_normal;
			this.btnOption2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnOption2, "btnOption2");
			this.btnOption2.Name = "btnOption2";
			this.btnOption2.UseVisualStyleBackColor = false;
			this.btnOption2.Click += new EventHandler(this.btnOption2_Click);
			this.btnOption1.BackColor = Color.Transparent;
			this.btnOption1.BackgroundImage = Resources.pMain_button_normal;
			this.btnOption1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnOption1, "btnOption1");
			this.btnOption1.Name = "btnOption1";
			this.btnOption1.UseVisualStyleBackColor = false;
			this.btnOption1.Click += new EventHandler(this.btnOption1_Click);
			this.btnOption0.BackColor = Color.Transparent;
			this.btnOption0.BackgroundImage = Resources.pMain_button_normal;
			this.btnOption0.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.btnOption0, "btnOption0");
			this.btnOption0.Name = "btnOption0";
			this.btnOption0.UseVisualStyleBackColor = false;
			this.btnOption0.Click += new EventHandler(this.btnOption0_Click);
			componentResourceManager.ApplyResources(this.chkOtherMeal, "chkOtherMeal");
			this.chkOtherMeal.Checked = true;
			this.chkOtherMeal.CheckState = CheckState.Checked;
			this.chkOtherMeal.ForeColor = Color.White;
			this.chkOtherMeal.Name = "chkOtherMeal";
			this.chkOtherMeal.UseVisualStyleBackColor = true;
			this.chkOtherMeal.CheckedChanged += new EventHandler(this.chkMeal_CheckedChanged);
			componentResourceManager.ApplyResources(this.dateBeginHMS4, "dateBeginHMS4");
			this.dateBeginHMS4.Name = "dateBeginHMS4";
			this.dateBeginHMS4.ShowUpDown = true;
			this.dateBeginHMS4.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS4, "dateEndHMS4");
			this.dateEndHMS4.Name = "dateEndHMS4";
			this.dateEndHMS4.ShowUpDown = true;
			this.dateEndHMS4.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			this.nudOther.DecimalPlaces = 2;
			componentResourceManager.ApplyResources(this.nudOther, "nudOther");
			this.nudOther.Name = "nudOther";
			componentResourceManager.ApplyResources(this.lblOther, "lblOther");
			this.lblOther.ForeColor = Color.White;
			this.lblOther.Name = "lblOther";
			componentResourceManager.ApplyResources(this.chkEveningMeal, "chkEveningMeal");
			this.chkEveningMeal.Checked = true;
			this.chkEveningMeal.CheckState = CheckState.Checked;
			this.chkEveningMeal.ForeColor = Color.White;
			this.chkEveningMeal.Name = "chkEveningMeal";
			this.chkEveningMeal.UseVisualStyleBackColor = true;
			this.chkEveningMeal.CheckedChanged += new EventHandler(this.chkMeal_CheckedChanged);
			componentResourceManager.ApplyResources(this.dateBeginHMS3, "dateBeginHMS3");
			this.dateBeginHMS3.Name = "dateBeginHMS3";
			this.dateBeginHMS3.ShowUpDown = true;
			this.dateBeginHMS3.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS3, "dateEndHMS3");
			this.dateEndHMS3.Name = "dateEndHMS3";
			this.dateEndHMS3.ShowUpDown = true;
			this.dateEndHMS3.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			this.nudEvening.DecimalPlaces = 2;
			componentResourceManager.ApplyResources(this.nudEvening, "nudEvening");
			this.nudEvening.Name = "nudEvening";
			componentResourceManager.ApplyResources(this.lblEvening, "lblEvening");
			this.lblEvening.ForeColor = Color.White;
			this.lblEvening.Name = "lblEvening";
			componentResourceManager.ApplyResources(this.chkLunchMeal, "chkLunchMeal");
			this.chkLunchMeal.Checked = true;
			this.chkLunchMeal.CheckState = CheckState.Checked;
			this.chkLunchMeal.ForeColor = Color.White;
			this.chkLunchMeal.Name = "chkLunchMeal";
			this.chkLunchMeal.UseVisualStyleBackColor = true;
			this.chkLunchMeal.CheckedChanged += new EventHandler(this.chkMeal_CheckedChanged);
			componentResourceManager.ApplyResources(this.dateBeginHMS2, "dateBeginHMS2");
			this.dateBeginHMS2.Name = "dateBeginHMS2";
			this.dateBeginHMS2.ShowUpDown = true;
			this.dateBeginHMS2.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS2, "dateEndHMS2");
			this.dateEndHMS2.Name = "dateEndHMS2";
			this.dateEndHMS2.ShowUpDown = true;
			this.dateEndHMS2.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			this.nudLunch.DecimalPlaces = 2;
			componentResourceManager.ApplyResources(this.nudLunch, "nudLunch");
			this.nudLunch.Name = "nudLunch";
			componentResourceManager.ApplyResources(this.lblLunch, "lblLunch");
			this.lblLunch.ForeColor = Color.White;
			this.lblLunch.Name = "lblLunch";
			componentResourceManager.ApplyResources(this.chkMorningMeal, "chkMorningMeal");
			this.chkMorningMeal.Checked = true;
			this.chkMorningMeal.CheckState = CheckState.Checked;
			this.chkMorningMeal.ForeColor = Color.White;
			this.chkMorningMeal.Name = "chkMorningMeal";
			this.chkMorningMeal.UseVisualStyleBackColor = true;
			this.chkMorningMeal.CheckedChanged += new EventHandler(this.chkMeal_CheckedChanged);
			componentResourceManager.ApplyResources(this.dateBeginHMS1, "dateBeginHMS1");
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.dateEndHMS1, "dateEndHMS1");
			this.dateEndHMS1.Name = "dateEndHMS1";
			this.dateEndHMS1.ShowUpDown = true;
			this.dateEndHMS1.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			this.nudMorning.DecimalPlaces = 2;
			componentResourceManager.ApplyResources(this.nudMorning, "nudMorning");
			this.nudMorning.Name = "nudMorning";
			componentResourceManager.ApplyResources(this.lblMorning, "lblMorning");
			this.lblMorning.ForeColor = Color.White;
			this.lblMorning.Name = "lblMorning";
			this.label85.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.label85, "label85");
			this.label85.Name = "label85";
			this.btnEdit.BackColor = Color.Transparent;
			this.btnEdit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnDel.BackColor = Color.Transparent;
			this.btnDel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDel, "btnDel");
			this.btnDel.ForeColor = Color.White;
			this.btnDel.Name = "btnDel";
			this.btnDel.UseVisualStyleBackColor = false;
			this.btnAdd.BackColor = Color.Transparent;
			this.btnAdd.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.tabControl1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmMealSetup";
			base.Load += new EventHandler(this.dfrmMealSetup_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((ISupportInitialize)this.dgvSelected).EndInit();
			((ISupportInitialize)this.dgvOptional).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((ISupportInitialize)this.nudRuleSeconds).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((ISupportInitialize)this.nudOther).EndInit();
			((ISupportInitialize)this.nudEvening).EndInit();
			((ISupportInitialize)this.nudLunch).EndInit();
			((ISupportInitialize)this.nudMorning).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmMealSetup()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				string text = "";
				string a = "";
				string strB = "";
				string text2 = "";
				if (this.chkMorningMeal.Checked)
				{
					text = this.dateBeginHMS1.Value.ToString("HH:mm");
					a = this.dateEndHMS1.Value.ToString("HH:mm");
					strB = this.dateBeginHMS1.Value.ToString("HH:mm");
					text2 = this.dateEndHMS1.Value.ToString("HH:mm");
					if (this.dateBeginHMS1.Value > this.dateEndHMS1.Value)
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateBeginHMS1.Value.ToString("HH:mm"),
							"\r\n\r\n",
							this.dateEndHMS1.Value.ToString("HH:mm")
						}));
						return;
					}
				}
				if (this.chkLunchMeal.Checked)
				{
					if (this.dateBeginHMS2.Value > this.dateEndHMS2.Value)
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateBeginHMS2.Value.ToString("HH:mm"),
							"\r\n\r\n",
							this.dateEndHMS2.Value.ToString("HH:mm")
						}));
						return;
					}
					if (text2 != "" && string.Compare(this.dateBeginHMS2.Value.ToString("HH:mm"), text2) < 0)
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateBeginHMS2.Value.ToString("HH:mm"),
							"\r\n\r\n",
							text2
						}));
						return;
					}
					if (text == "")
					{
						text = this.dateBeginHMS2.Value.ToString("HH:mm");
					}
					if (a == "")
					{
						a = this.dateEndHMS2.Value.ToString("HH:mm");
					}
					strB = this.dateBeginHMS2.Value.ToString("HH:mm");
					text2 = this.dateEndHMS2.Value.ToString("HH:mm");
				}
				if (this.chkEveningMeal.Checked)
				{
					if (this.dateBeginHMS3.Value > this.dateEndHMS3.Value)
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateBeginHMS3.Value.ToString("HH:mm"),
							"\r\n\r\n",
							this.dateEndHMS3.Value.ToString("HH:mm")
						}));
						return;
					}
					if (text2 != "" && string.Compare(this.dateBeginHMS3.Value.ToString("HH:mm"), text2) < 0)
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateBeginHMS3.Value.ToString("HH:mm"),
							"\r\n\r\n",
							text2
						}));
						return;
					}
					if (text == "")
					{
						text = this.dateBeginHMS3.Value.ToString("HH:mm");
					}
					if (a == "")
					{
						a = this.dateEndHMS3.Value.ToString("HH:mm");
					}
					strB = this.dateBeginHMS3.Value.ToString("HH:mm");
					text2 = this.dateEndHMS3.Value.ToString("HH:mm");
				}
				if (this.chkOtherMeal.Checked && !(text == ""))
				{
					if (string.Compare(this.dateBeginHMS4.Value.ToString("HH:mm"), text2) > 0)
					{
						if (string.Compare(this.dateEndHMS4.Value.ToString("HH:mm"), text) >= 0 && this.dateEndHMS4.Value < this.dateBeginHMS4.Value)
						{
							XMessageBox.Show(string.Concat(new string[]
							{
								CommonStr.strWrongTimeSegment,
								"\r\n\r\n",
								this.dateEndHMS4.Value.ToString("HH:mm"),
								"\r\n\r\n",
								text
							}));
							return;
						}
					}
					else if (string.Compare(this.dateBeginHMS4.Value.ToString("HH:mm"), text) >= 0)
					{
						if (string.Compare(this.dateBeginHMS4.Value.ToString("HH:mm"), strB) >= 0)
						{
							XMessageBox.Show(string.Concat(new string[]
							{
								CommonStr.strWrongTimeSegment,
								"\r\n\r\n",
								this.dateBeginHMS4.Value.ToString("HH:mm"),
								"\r\n\r\n",
								text2
							}));
							return;
						}
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateBeginHMS4.Value.ToString("HH:mm"),
							"\r\n\r\n",
							text
						}));
						return;
					}
					else if (string.Compare(this.dateEndHMS4.Value.ToString("HH:mm"), text) >= 0)
					{
						XMessageBox.Show(string.Concat(new string[]
						{
							CommonStr.strWrongTimeSegment,
							"\r\n\r\n",
							this.dateEndHMS4.Value.ToString("HH:mm"),
							"\r\n\r\n",
							text
						}));
						return;
					}
					strB = this.dateBeginHMS4.Value.ToString("HH:mm");
					text2 = this.dateEndHMS4.Value.ToString("HH:mm");
				}
				Cursor arg_6F7_0 = Cursor.Current;
				string text4;
				if (this.dvSelected.Count > 0)
				{
					string text3 = "";
					for (int i = 0; i <= this.dvSelected.Count - 1; i++)
					{
						if (text3 == "")
						{
							text3 += this.dvSelected[i]["f_ReaderID"];
						}
						else
						{
							text3 = text3 + "," + this.dvSelected[i]["f_ReaderID"];
						}
					}
					text4 = string.Format("DELETE FROM t_d_Reader4Meal WHERE f_ReaderID NOT IN ({0})", text3);
					wgAppConfig.runUpdateSql(text4);
					text4 = string.Format("INSERT INTO t_d_Reader4Meal (f_ReaderID) SELECT f_ReaderID from t_b_Reader WHERE f_ReaderID  IN ({0}) AND f_ReaderID NOT IN (SELECT f_ReaderID From t_d_Reader4Meal)  ", text3);
					wgAppConfig.runUpdateSql(text4);
				}
				else
				{
					text4 = " DELETE FROM t_d_Reader4Meal ";
					wgAppConfig.runUpdateSql(text4);
				}
				int num = 60;
				int num2 = 0;
				if (this.radioButton1.Checked)
				{
					num2 = 0;
				}
				if (this.radioButton2.Checked)
				{
					num2 = 1;
				}
				if (this.radioButton3.Checked)
				{
					num2 = 2;
					num = (int)this.nudRuleSeconds.Value;
				}
				text4 = string.Concat(new object[]
				{
					"UPDATE t_b_MealSetup SET f_Value = ",
					num2,
					", f_ParamVal=",
					num,
					" WHERE f_ID=1 "
				});
				wgAppConfig.runUpdateSql(text4);
				text4 = "UPDATE t_b_MealSetup SET f_Value = 0 WHERE f_ID=2 ";
				if (this.chkMorningMeal.Checked)
				{
					text4 = "UPDATE t_b_MealSetup SET f_Value = 1 ";
					text4 = text4 + ",f_BeginHMS =" + wgTools.PrepareStr(this.dateBeginHMS1.Value, true, "HH:mm");
					text4 = text4 + ",f_EndHMS =" + wgTools.PrepareStr(this.dateEndHMS1.Value, true, "HH:mm");
					text4 = text4 + ", f_ParamVal= " + this.nudMorning.Value;
					text4 += " WHERE f_ID=2 ";
				}
				wgAppConfig.runUpdateSql(text4);
				text4 = "UPDATE t_b_MealSetup SET f_Value = 0 WHERE f_ID=3 ";
				if (this.chkLunchMeal.Checked)
				{
					text4 = "UPDATE t_b_MealSetup SET f_Value = 1 ";
					text4 = text4 + ",f_BeginHMS =" + wgTools.PrepareStr(this.dateBeginHMS2.Value, true, "HH:mm");
					text4 = text4 + ",f_EndHMS =" + wgTools.PrepareStr(this.dateEndHMS2.Value, true, "HH:mm");
					text4 = text4 + ", f_ParamVal= " + this.nudLunch.Value;
					text4 += " WHERE f_ID=3 ";
				}
				wgAppConfig.runUpdateSql(text4);
				text4 = "UPDATE t_b_MealSetup SET f_Value = 0 WHERE f_ID=4 ";
				if (this.chkEveningMeal.Checked)
				{
					text4 = "UPDATE t_b_MealSetup SET f_Value = 1 ";
					text4 = text4 + ",f_BeginHMS =" + wgTools.PrepareStr(this.dateBeginHMS3.Value, true, "HH:mm");
					text4 = text4 + ",f_EndHMS =" + wgTools.PrepareStr(this.dateEndHMS3.Value, true, "HH:mm");
					text4 = text4 + ", f_ParamVal= " + this.nudEvening.Value;
					text4 += " WHERE f_ID=4 ";
				}
				wgAppConfig.runUpdateSql(text4);
				text4 = "UPDATE t_b_MealSetup SET f_Value = 0 WHERE f_ID=5 ";
				if (this.chkOtherMeal.Checked)
				{
					text4 = "UPDATE t_b_MealSetup SET f_Value = 1 ";
					text4 = text4 + ",f_BeginHMS =" + wgTools.PrepareStr(this.dateBeginHMS4.Value, true, "HH:mm");
					text4 = text4 + ",f_EndHMS =" + wgTools.PrepareStr(this.dateEndHMS4.Value, true, "HH:mm");
					text4 = text4 + ", f_ParamVal= " + this.nudOther.Value;
					text4 += " WHERE f_ID=5 ";
				}
				wgAppConfig.runUpdateSql(text4);
				if (wgAppConfig.IsAccessDB)
				{
					OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
					OleDbCommand oleDbCommand = new OleDbCommand("SELECT f_ID from t_b_MealSetup WHERE f_ID=6 ", oleDbConnection);
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.Default);
					bool flag = !oleDbDataReader.HasRows;
					oleDbDataReader.Close();
					oleDbConnection.Close();
					if (flag)
					{
						text4 = "INSERT INTO t_b_MealSetup (f_ID, f_Value, f_BeginHMS, f_EndHMS, f_ParamVal) VALUES(6,0,NULL,NULL,0) ";
						wgAppConfig.runUpdateSql(text4);
					}
					text4 = "UPDATE t_b_MealSetup SET f_Value = 0 WHERE f_ID=6 ";
					if (this.chkAllowableSwipe.Checked)
					{
						text4 = "UPDATE t_b_MealSetup SET f_Value = 1 WHERE f_ID=6 ";
					}
					wgAppConfig.runUpdateSql(text4);
				}
				else
				{
					SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
					SqlCommand sqlCommand = new SqlCommand("SELECT f_ID from t_b_MealSetup WHERE f_ID=6 ", sqlConnection);
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);
					bool flag2 = !sqlDataReader.HasRows;
					sqlDataReader.Close();
					sqlConnection.Close();
					if (flag2)
					{
						text4 = "INSERT INTO t_b_MealSetup (f_ID, f_Value, f_BeginHMS, f_EndHMS, f_ParamVal) VALUES(6,0,NULL,NULL,0) ";
						wgAppConfig.runUpdateSql(text4);
					}
					text4 = "UPDATE t_b_MealSetup SET f_Value = 0 WHERE f_ID=6 ";
					if (this.chkAllowableSwipe.Checked)
					{
						text4 = "UPDATE t_b_MealSetup SET f_Value = 1 WHERE f_ID=6 ";
					}
					wgAppConfig.runUpdateSql(text4);
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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void dfrmMealSetup_Load(object sender, EventArgs e)
		{
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("04:00:00");
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("9:59:59");
			this.dateBeginHMS2.CustomFormat = "HH:mm";
			this.dateBeginHMS2.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS2.Value = DateTime.Parse("10:00:00");
			this.dateEndHMS2.CustomFormat = "HH:mm";
			this.dateEndHMS2.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS2.Value = DateTime.Parse("15:59:59");
			this.dateBeginHMS3.CustomFormat = "HH:mm";
			this.dateBeginHMS3.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS3.Value = DateTime.Parse("16:00:00");
			this.dateEndHMS3.CustomFormat = "HH:mm";
			this.dateEndHMS3.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS3.Value = DateTime.Parse("21:59:59");
			this.dateBeginHMS4.CustomFormat = "HH:mm";
			this.dateBeginHMS4.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS4.Value = DateTime.Parse("22:00:00");
			this.dateEndHMS4.CustomFormat = "HH:mm";
			this.dateEndHMS4.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS4.Value = DateTime.Parse("23:59:59");
			this.loadData();
		}

		public void loadData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadData_Acc();
				return;
			}
			SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				SqlCommand sqlCommand = new SqlCommand("Select f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  WHERE   t_b_reader.f_ReaderID NOT IN (SELECT t_d_Reader4Meal.f_ReaderID FROM t_d_Reader4Meal  ) ", sqlConnection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlDataAdapter.Fill(this.ds, "optionalReader");
				sqlCommand = new SqlCommand("Select f_ReaderID, f_ReaderName, 1 as f_Selected from t_b_reader  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  WHERE   t_b_reader.f_ReaderID  IN (SELECT t_d_Reader4Meal.f_ReaderID FROM t_d_Reader4Meal  ) ", sqlConnection);
				sqlDataAdapter = new SqlDataAdapter(sqlCommand);
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
				sqlCommand.CommandText = "SELECT * from t_b_MealSetup WHERE f_ID=1 ";
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					if (int.Parse(sqlDataReader["f_Value"].ToString()) == 1)
					{
						this.radioButton2.Checked = true;
					}
					else
					{
						if (int.Parse(sqlDataReader["f_Value"].ToString()) == 2)
						{
							this.radioButton3.Checked = true;
							try
							{
								this.nudRuleSeconds.Value = (int)decimal.Parse(sqlDataReader["f_ParamVal"].ToString());
								goto IL_2D0;
							}
							catch (Exception ex)
							{
								wgTools.WgDebugWrite(ex.ToString(), new object[0]);
								goto IL_2D0;
							}
						}
						this.radioButton1.Checked = true;
					}
				}
				IL_2D0:
				sqlDataReader.Close();
				sqlCommand.CommandText = "SELECT * from t_b_MealSetup WHERE f_ID > 1 ORDER BY f_ID ASC";
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					if (int.Parse(sqlDataReader["f_ID"].ToString()) == 2)
					{
						if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) > 0)
						{
							this.chkMorningMeal.Checked = true;
							this.dateBeginHMS1.Value = (DateTime)sqlDataReader["f_BeginHMS"];
							this.dateEndHMS1.Value = (DateTime)sqlDataReader["f_EndHMS"];
							this.nudMorning.Value = decimal.Parse(wgTools.SetObjToStr(sqlDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkMorningMeal.Checked = false;
						}
						this.dateBeginHMS1.Visible = this.chkMorningMeal.Checked;
						this.dateEndHMS1.Visible = this.chkMorningMeal.Checked;
						this.nudMorning.Visible = this.chkMorningMeal.Checked;
					}
					else if (int.Parse(sqlDataReader["f_ID"].ToString()) == 3)
					{
						if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) > 0)
						{
							this.chkLunchMeal.Checked = true;
							this.dateBeginHMS2.Value = (DateTime)sqlDataReader["f_BeginHMS"];
							this.dateEndHMS2.Value = (DateTime)sqlDataReader["f_EndHMS"];
							this.nudLunch.Value = decimal.Parse(wgTools.SetObjToStr(sqlDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkLunchMeal.Checked = false;
						}
						this.dateBeginHMS2.Visible = this.chkLunchMeal.Checked;
						this.dateEndHMS2.Visible = this.chkLunchMeal.Checked;
						this.nudLunch.Visible = this.chkLunchMeal.Checked;
					}
					else if (int.Parse(sqlDataReader["f_ID"].ToString()) == 4)
					{
						if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) > 0)
						{
							this.chkEveningMeal.Checked = true;
							this.dateBeginHMS3.Value = (DateTime)sqlDataReader["f_BeginHMS"];
							this.dateEndHMS3.Value = (DateTime)sqlDataReader["f_EndHMS"];
							this.nudEvening.Value = decimal.Parse(wgTools.SetObjToStr(sqlDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkEveningMeal.Checked = false;
						}
						this.dateBeginHMS3.Visible = this.chkEveningMeal.Checked;
						this.dateEndHMS3.Visible = this.chkEveningMeal.Checked;
						this.nudEvening.Visible = this.chkEveningMeal.Checked;
					}
					else if (int.Parse(sqlDataReader["f_ID"].ToString()) == 5)
					{
						if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) > 0)
						{
							this.chkOtherMeal.Checked = true;
							this.dateBeginHMS4.Value = (DateTime)sqlDataReader["f_BeginHMS"];
							this.dateEndHMS4.Value = (DateTime)sqlDataReader["f_EndHMS"];
							this.nudOther.Value = decimal.Parse(wgTools.SetObjToStr(sqlDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkOtherMeal.Checked = false;
						}
						this.dateBeginHMS4.Visible = this.chkOtherMeal.Checked;
						this.dateEndHMS4.Visible = this.chkOtherMeal.Checked;
						this.nudOther.Visible = this.chkOtherMeal.Checked;
					}
					else if (int.Parse(sqlDataReader["f_ID"].ToString()) == 6)
					{
						if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) > 0)
						{
							this.chkAllowableSwipe.Checked = true;
						}
						else
						{
							this.chkAllowableSwipe.Checked = false;
						}
					}
				}
				sqlDataReader.Close();
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
		}

		public void loadData_Acc()
		{
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				OleDbCommand oleDbCommand = new OleDbCommand("Select f_ReaderID, f_ReaderName, 0 as f_Selected from t_b_reader  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  WHERE   t_b_reader.f_ReaderID NOT IN (SELECT t_d_Reader4Meal.f_ReaderID FROM t_d_Reader4Meal  ) ", oleDbConnection);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
				oleDbDataAdapter.Fill(this.ds, "optionalReader");
				oleDbCommand = new OleDbCommand("Select f_ReaderID, f_ReaderName, 1 as f_Selected from t_b_reader  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  WHERE   t_b_reader.f_ReaderID  IN (SELECT t_d_Reader4Meal.f_ReaderID FROM t_d_Reader4Meal  ) ", oleDbConnection);
				oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
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
				oleDbCommand.CommandText = "SELECT * from t_b_MealSetup WHERE f_ID=1 ";
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.Read())
				{
					if (int.Parse(oleDbDataReader["f_Value"].ToString()) == 1)
					{
						this.radioButton2.Checked = true;
					}
					else
					{
						if (int.Parse(oleDbDataReader["f_Value"].ToString()) == 2)
						{
							this.radioButton3.Checked = true;
							try
							{
								this.nudRuleSeconds.Value = (int)decimal.Parse(oleDbDataReader["f_ParamVal"].ToString());
								goto IL_2C2;
							}
							catch (Exception ex)
							{
								wgTools.WgDebugWrite(ex.ToString(), new object[0]);
								goto IL_2C2;
							}
						}
						this.radioButton1.Checked = true;
					}
				}
				IL_2C2:
				oleDbDataReader.Close();
				oleDbCommand.CommandText = "SELECT * from t_b_MealSetup WHERE f_ID > 1 ORDER BY f_ID ASC";
				if (oleDbConnection.State != ConnectionState.Open)
				{
					oleDbConnection.Open();
				}
				oleDbDataReader = oleDbCommand.ExecuteReader();
				while (oleDbDataReader.Read())
				{
					if (int.Parse(oleDbDataReader["f_ID"].ToString()) == 2)
					{
						if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) > 0)
						{
							this.chkMorningMeal.Checked = true;
							this.dateBeginHMS1.Value = (DateTime)oleDbDataReader["f_BeginHMS"];
							this.dateEndHMS1.Value = (DateTime)oleDbDataReader["f_EndHMS"];
							this.nudMorning.Value = decimal.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkMorningMeal.Checked = false;
						}
						this.dateBeginHMS1.Visible = this.chkMorningMeal.Checked;
						this.dateEndHMS1.Visible = this.chkMorningMeal.Checked;
						this.nudMorning.Visible = this.chkMorningMeal.Checked;
					}
					else if (int.Parse(oleDbDataReader["f_ID"].ToString()) == 3)
					{
						if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) > 0)
						{
							this.chkLunchMeal.Checked = true;
							this.dateBeginHMS2.Value = (DateTime)oleDbDataReader["f_BeginHMS"];
							this.dateEndHMS2.Value = (DateTime)oleDbDataReader["f_EndHMS"];
							this.nudLunch.Value = decimal.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkLunchMeal.Checked = false;
						}
						this.dateBeginHMS2.Visible = this.chkLunchMeal.Checked;
						this.dateEndHMS2.Visible = this.chkLunchMeal.Checked;
						this.nudLunch.Visible = this.chkLunchMeal.Checked;
					}
					else if (int.Parse(oleDbDataReader["f_ID"].ToString()) == 4)
					{
						if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) > 0)
						{
							this.chkEveningMeal.Checked = true;
							this.dateBeginHMS3.Value = (DateTime)oleDbDataReader["f_BeginHMS"];
							this.dateEndHMS3.Value = (DateTime)oleDbDataReader["f_EndHMS"];
							this.nudEvening.Value = decimal.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkEveningMeal.Checked = false;
						}
						this.dateBeginHMS3.Visible = this.chkEveningMeal.Checked;
						this.dateEndHMS3.Visible = this.chkEveningMeal.Checked;
						this.nudEvening.Visible = this.chkEveningMeal.Checked;
					}
					else if (int.Parse(oleDbDataReader["f_ID"].ToString()) == 5)
					{
						if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) > 0)
						{
							this.chkOtherMeal.Checked = true;
							this.dateBeginHMS4.Value = (DateTime)oleDbDataReader["f_BeginHMS"];
							this.dateEndHMS4.Value = (DateTime)oleDbDataReader["f_EndHMS"];
							this.nudOther.Value = decimal.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ParamVal"]));
						}
						else
						{
							this.chkOtherMeal.Checked = false;
						}
						this.dateBeginHMS4.Visible = this.chkOtherMeal.Checked;
						this.dateEndHMS4.Visible = this.chkOtherMeal.Checked;
						this.nudOther.Visible = this.chkOtherMeal.Checked;
					}
					else if (int.Parse(oleDbDataReader["f_ID"].ToString()) == 6)
					{
						if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) > 0)
						{
							this.chkAllowableSwipe.Checked = true;
						}
						else
						{
							this.chkAllowableSwipe.Checked = false;
						}
					}
				}
				oleDbDataReader.Close();
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
		}

		private void btnAddAllReaders_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 1;
				}
				this.dt.AcceptChanges();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnAddOneReader_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvOptional);
		}

		private void btnDeleteOneReader_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelected);
		}

		private void btnDeleteAllReaders_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 0;
				}
				this.dt.AcceptChanges();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void chkMeal_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				this.dateBeginHMS1.Visible = this.chkMorningMeal.Checked;
				this.dateEndHMS1.Visible = this.chkMorningMeal.Checked;
				this.nudMorning.Visible = this.chkMorningMeal.Checked;
				this.lblMorning.Visible = this.chkMorningMeal.Checked;
				this.btnOption0.Visible = this.chkMorningMeal.Checked;
				this.dateBeginHMS2.Visible = this.chkLunchMeal.Checked;
				this.dateEndHMS2.Visible = this.chkLunchMeal.Checked;
				this.nudLunch.Visible = this.chkLunchMeal.Checked;
				this.lblLunch.Visible = this.chkLunchMeal.Checked;
				this.btnOption1.Visible = this.chkLunchMeal.Checked;
				this.dateBeginHMS3.Visible = this.chkEveningMeal.Checked;
				this.dateEndHMS3.Visible = this.chkEveningMeal.Checked;
				this.nudEvening.Visible = this.chkEveningMeal.Checked;
				this.lblEvening.Visible = this.chkEveningMeal.Checked;
				this.btnOption2.Visible = this.chkEveningMeal.Checked;
				this.dateBeginHMS4.Visible = this.chkOtherMeal.Checked;
				this.dateEndHMS4.Visible = this.chkOtherMeal.Checked;
				this.nudOther.Visible = this.chkOtherMeal.Checked;
				this.lblOther.Visible = this.chkOtherMeal.Checked;
				this.btnOption3.Visible = this.chkOtherMeal.Checked;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnOption0_Click(object sender, EventArgs e)
		{
			using (dfrmMealOption dfrmMealOption = new dfrmMealOption())
			{
				dfrmMealOption.mealNo = 0;
				dfrmMealOption expr_0E = dfrmMealOption;
				expr_0E.Text = expr_0E.Text + "--" + this.chkMorningMeal.Text;
				dfrmMealOption.ShowDialog();
			}
		}

		private void btnOption1_Click(object sender, EventArgs e)
		{
			using (dfrmMealOption dfrmMealOption = new dfrmMealOption())
			{
				dfrmMealOption.mealNo = 1;
				dfrmMealOption expr_0E = dfrmMealOption;
				expr_0E.Text = expr_0E.Text + "--" + this.chkLunchMeal.Text;
				dfrmMealOption.ShowDialog();
			}
		}

		private void btnOption2_Click(object sender, EventArgs e)
		{
			using (dfrmMealOption dfrmMealOption = new dfrmMealOption())
			{
				dfrmMealOption.mealNo = 2;
				dfrmMealOption expr_0E = dfrmMealOption;
				expr_0E.Text = expr_0E.Text + "--" + this.chkEveningMeal.Text;
				dfrmMealOption.ShowDialog();
			}
		}

		private void btnOption3_Click(object sender, EventArgs e)
		{
			using (dfrmMealOption dfrmMealOption = new dfrmMealOption())
			{
				dfrmMealOption.mealNo = 3;
				dfrmMealOption expr_0E = dfrmMealOption;
				expr_0E.Text = expr_0E.Text + "--" + this.chkOtherMeal.Text;
				dfrmMealOption.ShowDialog();
			}
		}
	}
}
