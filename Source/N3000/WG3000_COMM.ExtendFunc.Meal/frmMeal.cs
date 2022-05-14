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

namespace WG3000_COMM.ExtendFunc.Meal
{
	public class frmMeal : frmN3000
	{
		public class ToolStripDateTime : ToolStripControlHost
		{
			private static DateTimePicker dtp;

			public DateTimePicker DateTimeControl
			{
				get
				{
					return base.Control as DateTimePicker;
				}
			}

			public int BoxWidth
			{
				get
				{
					return (base.Control as DateTimePicker).Size.Width;
				}
				set
				{
					base.Control.Size = new Size(new Point(value, base.Control.Size.Height));
					(base.Control as DateTimePicker).Size = new Size(new Point(value, base.Control.Size.Height));
				}
			}

			public DateTime Value
			{
				get
				{
					return (base.Control as DateTimePicker).Value;
				}
				set
				{
					(base.Control as DateTimePicker).Value = value;
				}
			}

			public ToolStripDateTime() : base(frmMeal.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmMeal.ToolStripDateTime.dtp != null)
				{
					frmMeal.ToolStripDateTime.dtp.Dispose();
				}
				base.Dispose(disposing);
			}

			public void SetTimeFormat()
			{
				DateTimePicker dateTimePicker = base.Control as DateTimePicker;
				dateTimePicker.CustomFormat = "HH;mm";
				dateTimePicker.Format = DateTimePickerFormat.Custom;
				dateTimePicker.ShowUpDown = true;
			}
		}

		private IContainer components;

		private ToolStrip toolStrip1;

		private DataGridView dgvSwipeRecords;

		private ToolStripButton btnPrint;

		private BackgroundWorker backgroundWorker1;

		private ToolStripButton btnExportToExcel;

		private UserControlFind userControlFind1;

		private System.Windows.Forms.Timer timer1;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private DataGridView dgvSubtotal;

		private TabPage tabPage3;

		private DataGridView dgvStatistics;

		private ToolStripButton btnCreateReport;

		private ProgressBar ProgressBar1;

		private ToolStrip toolStrip3;

		private ToolStripLabel toolStripLabel2;

		private ToolStripLabel toolStripLabel3;

		private ToolStripButton btnMealSetup;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_ConsumerID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_ReadDate;

		private DataGridViewTextBoxColumn MealName;

		private DataGridViewTextBoxColumn f_Cost;

		private DataGridViewTextBoxColumn f_Addr;

		private DataGridViewTextBoxColumn f_ReaderID;

		private DataGridViewTextBoxColumn f_ReaderID2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn f_DepartmentName2;

		private DataGridViewTextBoxColumn f_ConsumerNO2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn Morning;

		private DataGridViewTextBoxColumn Lunch;

		private DataGridViewTextBoxColumn Evening;

		private DataGridViewTextBoxColumn Other;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		private ToolStripButton btnExit;

		private frmMeal.ToolStripDateTime dtpDateFrom;

		private frmMeal.ToolStripDateTime dtpDateTo;

		private string oldStatTitle = "";

		private DataSet dsDefaultStyle = new DataSet("DGV_STILE");

		private int recIdMin;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

		private int startRecordIndex;

		private int MaxRecord = 1000;

		private dfrmSwipeRecordsFindOption dfrmFindOption;

		public string strFindOption = "";

		private DataSet ds;

		private DataView dvReaderStatistics;

		private DataView dvConsumerStatistics = new DataView();

		private DataView dv;

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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMeal));
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.dgvSwipeRecords = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_ConsumerID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_ReadDate = new DataGridViewTextBoxColumn();
			this.MealName = new DataGridViewTextBoxColumn();
			this.f_Cost = new DataGridViewTextBoxColumn();
			this.f_Addr = new DataGridViewTextBoxColumn();
			this.f_ReaderID = new DataGridViewTextBoxColumn();
			this.toolStrip1 = new ToolStrip();
			this.btnMealSetup = new ToolStripButton();
			this.btnCreateReport = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			this.userControlFind1 = new UserControlFind();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.tabPage2 = new TabPage();
			this.dgvSubtotal = new DataGridView();
			this.f_ReaderID2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.tabPage3 = new TabPage();
			this.dgvStatistics = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.f_DepartmentName2 = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.Morning = new DataGridViewTextBoxColumn();
			this.Lunch = new DataGridViewTextBoxColumn();
			this.Evening = new DataGridViewTextBoxColumn();
			this.Other = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
			this.ProgressBar1 = new ProgressBar();
			this.toolStrip3 = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.toolStripLabel3 = new ToolStripLabel();
			((ISupportInitialize)this.dgvSwipeRecords).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.dgvSubtotal).BeginInit();
			this.tabPage3.SuspendLayout();
			((ISupportInitialize)this.dgvStatistics).BeginInit();
			this.toolStrip3.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.dgvSwipeRecords.AllowUserToAddRows = false;
			this.dgvSwipeRecords.AllowUserToDeleteRows = false;
			this.dgvSwipeRecords.AllowUserToOrderColumns = true;
			this.dgvSwipeRecords.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSwipeRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSwipeRecords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSwipeRecords.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_RecID,
				this.f_ConsumerID,
				this.f_DepartmentName,
				this.f_ConsumerNO,
				this.f_ConsumerName,
				this.f_ReadDate,
				this.MealName,
				this.f_Cost,
				this.f_Addr,
				this.f_ReaderID
			});
			componentResourceManager.ApplyResources(this.dgvSwipeRecords, "dgvSwipeRecords");
			this.dgvSwipeRecords.EnableHeadersVisualStyles = false;
			this.dgvSwipeRecords.Name = "dgvSwipeRecords";
			this.dgvSwipeRecords.ReadOnly = true;
			this.dgvSwipeRecords.RowHeadersVisible = false;
			this.dgvSwipeRecords.RowTemplate.Height = 23;
			this.dgvSwipeRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSwipeRecords.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvSwipeRecords_CellFormatting);
			this.dgvSwipeRecords.Scroll += new ScrollEventHandler(this.dgvSwipeRecords_Scroll);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_RecID.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerID.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_ConsumerID, "f_ConsumerID");
			this.f_ConsumerID.Name = "f_ConsumerID";
			this.f_ConsumerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DepartmentName, "f_DepartmentName");
			this.f_DepartmentName.Name = "f_DepartmentName";
			this.f_DepartmentName.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_ConsumerNO, "f_ConsumerNO");
			this.f_ConsumerNO.Name = "f_ConsumerNO";
			this.f_ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReadDate, "f_ReadDate");
			this.f_ReadDate.Name = "f_ReadDate";
			this.f_ReadDate.ReadOnly = true;
			componentResourceManager.ApplyResources(this.MealName, "MealName");
			this.MealName.Name = "MealName";
			this.MealName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Cost, "f_Cost");
			this.f_Cost.Name = "f_Cost";
			this.f_Cost.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Addr, "f_Addr");
			this.f_Addr.Name = "f_Addr";
			this.f_Addr.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ReaderID, "f_ReaderID");
			this.f_ReaderID.Name = "f_ReaderID";
			this.f_ReaderID.ReadOnly = true;
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pTools_first_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnMealSetup,
				this.btnCreateReport,
				this.btnPrint,
				this.btnExportToExcel,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnMealSetup.ForeColor = Color.White;
			this.btnMealSetup.Image = Resources.pTools_TypeSetup;
			componentResourceManager.ApplyResources(this.btnMealSetup, "btnMealSetup");
			this.btnMealSetup.Name = "btnMealSetup";
			this.btnMealSetup.Click += new EventHandler(this.btnMealSetup_Click);
			this.btnCreateReport.ForeColor = Color.White;
			this.btnCreateReport.Image = Resources.pTools_CreateShiftReport;
			componentResourceManager.ApplyResources(this.btnCreateReport, "btnCreateReport");
			this.btnCreateReport.Name = "btnCreateReport";
			this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
			this.btnPrint.ForeColor = Color.FromArgb(233, 241, 255);
			this.btnPrint.Image = Resources.pTools_Print;
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnExportToExcel.ForeColor = Color.FromArgb(233, 241, 255);
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.ForeColor = Color.White;
			this.userControlFind1.Name = "userControlFind1";
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabPage1.BackgroundImage = Resources.pMain_content_bkg;
			this.tabPage1.Controls.Add(this.dgvSwipeRecords);
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage2.BackgroundImage = Resources.pMain_content_bkg;
			this.tabPage2.Controls.Add(this.dgvSubtotal);
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.dgvSubtotal.AllowUserToAddRows = false;
			this.dgvSubtotal.AllowUserToDeleteRows = false;
			this.dgvSubtotal.AllowUserToOrderColumns = true;
			this.dgvSubtotal.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			this.dgvSubtotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dgvSubtotal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSubtotal.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ReaderID2,
				this.dataGridViewTextBoxColumn9,
				this.dataGridViewTextBoxColumn4,
				this.dataGridViewTextBoxColumn5
			});
			componentResourceManager.ApplyResources(this.dgvSubtotal, "dgvSubtotal");
			this.dgvSubtotal.EnableHeadersVisualStyles = false;
			this.dgvSubtotal.Name = "dgvSubtotal";
			this.dgvSubtotal.ReadOnly = true;
			this.dgvSubtotal.RowHeadersVisible = false;
			this.dgvSubtotal.RowTemplate.Height = 23;
			this.dgvSubtotal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.f_ReaderID2, "f_ReaderID2");
			this.f_ReaderID2.Name = "f_ReaderID2";
			this.f_ReaderID2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.tabPage3.BackgroundImage = Resources.pMain_content_bkg;
			this.tabPage3.Controls.Add(this.dgvStatistics);
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.dgvStatistics.AllowUserToAddRows = false;
			this.dgvStatistics.AllowUserToDeleteRows = false;
			this.dgvStatistics.AllowUserToOrderColumns = true;
			this.dgvStatistics.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = Color.White;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvStatistics.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvStatistics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvStatistics.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.f_DepartmentName2,
				this.f_ConsumerNO2,
				this.dataGridViewTextBoxColumn6,
				this.Morning,
				this.Lunch,
				this.Evening,
				this.Other,
				this.dataGridViewTextBoxColumn10,
				this.dataGridViewTextBoxColumn11
			});
			componentResourceManager.ApplyResources(this.dgvStatistics, "dgvStatistics");
			this.dgvStatistics.EnableHeadersVisualStyles = false;
			this.dgvStatistics.Name = "dgvStatistics";
			this.dgvStatistics.ReadOnly = true;
			this.dgvStatistics.RowHeadersVisible = false;
			this.dgvStatistics.RowTemplate.Height = 23;
			this.dgvStatistics.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DepartmentName2, "f_DepartmentName2");
			this.f_DepartmentName2.Name = "f_DepartmentName2";
			this.f_DepartmentName2.ReadOnly = true;
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO2.DefaultCellStyle = dataGridViewCellStyle8;
			componentResourceManager.ApplyResources(this.f_ConsumerNO2, "f_ConsumerNO2");
			this.f_ConsumerNO2.Name = "f_ConsumerNO2";
			this.f_ConsumerNO2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Morning, "Morning");
			this.Morning.Name = "Morning";
			this.Morning.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Lunch, "Lunch");
			this.Lunch.Name = "Lunch";
			this.Lunch.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Evening, "Evening");
			this.Evening.Name = "Evening";
			this.Evening.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Other, "Other");
			this.Other.Name = "Other";
			this.Other.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ProgressBar1, "ProgressBar1");
			this.ProgressBar1.Name = "ProgressBar1";
			this.toolStrip3.BackColor = Color.Transparent;
			this.toolStrip3.BackgroundImage = Resources.pTools_second_title;
			componentResourceManager.ApplyResources(this.toolStrip3, "toolStrip3");
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.toolStripLabel3
			});
			this.toolStrip3.Name = "toolStrip3";
			this.toolStripLabel2.ForeColor = Color.White;
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.toolStripLabel3.ForeColor = Color.White;
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.ProgressBar1);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmMeal";
			base.FormClosing += new FormClosingEventHandler(this.frmSwipeRecords_FormClosing);
			base.Load += new EventHandler(this.frmSwipeRecords_Load);
			base.KeyDown += new KeyEventHandler(this.frmSwipeRecords_KeyDown);
			((ISupportInitialize)this.dgvSwipeRecords).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)this.dgvSubtotal).EndInit();
			this.tabPage3.ResumeLayout(false);
			((ISupportInitialize)this.dgvStatistics).EndInit();
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmMeal()
		{
			this.InitializeComponent();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuConstMeal";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnMealSetup.Visible = false;
			}
		}

		private void frmSwipeRecords_Load(object sender, EventArgs e)
		{
			this.oldStatTitle = this.tabPage3.Text;
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.f_DepartmentName2.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName2.HeaderText);
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.f_ConsumerNO2.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO2.HeaderText);
			this.loadOperatorPrivilege();
			this.saveDefaultStyle();
			this.loadStyle();
			this.dtpDateFrom = new frmMeal.ToolStripDateTime();
			this.dtpDateTo = new frmMeal.ToolStripDateTime();
			this.dtpDateTo.Value = DateTime.Now.Date;
			this.dtpDateFrom.Value = DateTime.Now.Date;
			this.toolStrip3.Items.Clear();
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.dtpDateFrom,
				this.toolStripLabel3,
				this.dtpDateTo
			});
			this.userControlFind1.toolStripLabel2.Visible = false;
			this.userControlFind1.txtFindCardID.Visible = false;
			this.dtpDateFrom.BoxWidth = 120;
			this.dtpDateTo.BoxWidth = 120;
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.dtpDateFrom.BoxWidth = 150;
			this.dtpDateTo.BoxWidth = 150;
			wgAppConfig.setDisplayFormatDate(this.dtpDateFrom.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpDateTo.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			this.userControlFind1.btnQuery.Visible = false;
			this.bLoadedFinished = true;
		}

		private void saveDefaultStyle()
		{
			DataTable dataTable = new DataTable();
			this.dsDefaultStyle.Tables.Add(dataTable);
			dataTable.TableName = this.dgvSwipeRecords.Name;
			dataTable.Columns.Add("colName");
			dataTable.Columns.Add("colHeader");
			dataTable.Columns.Add("colWidth");
			dataTable.Columns.Add("colVisable");
			dataTable.Columns.Add("colDisplayIndex");
			for (int i = 0; i < this.dgvSwipeRecords.ColumnCount; i++)
			{
				DataGridViewColumn dataGridViewColumn = this.dgvSwipeRecords.Columns[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow["colName"] = dataGridViewColumn.Name;
				dataRow["colHeader"] = dataGridViewColumn.HeaderText;
				dataRow["colWidth"] = dataGridViewColumn.Width;
				dataRow["colVisable"] = dataGridViewColumn.Visible;
				dataRow["colDisplayIndex"] = dataGridViewColumn.DisplayIndex;
				dataTable.Rows.Add(dataRow);
				dataTable.AcceptChanges();
			}
		}

		private void loadDefaultStyle()
		{
			DataTable dataTable = this.dsDefaultStyle.Tables[this.dgvSwipeRecords.Name];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.dgvSwipeRecords.Columns[i].Name = dataTable.Rows[i]["colName"].ToString();
				this.dgvSwipeRecords.Columns[i].HeaderText = dataTable.Rows[i]["colHeader"].ToString();
				this.dgvSwipeRecords.Columns[i].Width = int.Parse(dataTable.Rows[i]["colWidth"].ToString());
				this.dgvSwipeRecords.Columns[i].Visible = bool.Parse(dataTable.Rows[i]["colVisable"].ToString());
				this.dgvSwipeRecords.Columns[i].DisplayIndex = int.Parse(dataTable.Rows[i]["colDisplayIndex"].ToString());
			}
		}

		private void loadStyle()
		{
			this.dgvSwipeRecords.AutoGenerateColumns = false;
			wgAppConfig.ReadGVStyle(this, this.dgvSwipeRecords);
		}

		private DataTable loadSwipeRecords(int startIndex, int maxRecords, string strSql)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("loadSwipeRecords Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recIdMin = 2147483647;
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND f_RecID < {0:d}", this.recIdMin);
			}
			else
			{
				strSql += string.Format(" WHERE f_RecID < {0:d}", this.recIdMin);
			}
			strSql += " ORDER BY f_RecID DESC ";
			this.table = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(strSql, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.table);
						}
					}
					goto IL_17B;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.table);
					}
				}
			}
			IL_17B:
			if (this.table.Rows.Count > 0)
			{
				this.recIdMin = int.Parse(this.table.Rows[this.table.Rows.Count - 1][0].ToString());
			}
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			return this.table;
		}

		private string getSqlOfDateTime()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.getSqlOfDateTime_Acc();
			}
			string text = "  ([f_ReadDate]>= " + wgTools.PrepareStr(this.dtpDateFrom.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 00:00:00") + ")";
			if (text != "")
			{
				text += " AND ";
			}
			return text + "  ([f_ReadDate]<= " + wgTools.PrepareStr(this.dtpDateTo.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59") + ")";
		}

		private string getSqlOfDateTime_Acc()
		{
			string text = "  ([f_ReadDate]>= " + wgTools.PrepareStr(this.dtpDateFrom.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 00:00:00") + ")";
			if (text != "")
			{
				text += " AND ";
			}
			return text + "  ([f_ReadDate]<= " + wgTools.PrepareStr(this.dtpDateTo.Value.ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59") + ")";
		}

		public void btnQuery_Click(object sender, EventArgs e)
		{
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			string arg = "";
			bool flag = false;
			if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
			{
				flag = true;
				arg = " (t_d_SwipeRecord.f_ReaderID IN ( " + this.dfrmFindOption.getStrSql() + " )) ";
			}
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
			text += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
			text += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, ";
			text += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
			string text2 = " ( 1>0 ) ";
			if (this.getSqlOfDateTime() != "")
			{
				text2 += string.Format(" AND {0} ", this.getSqlOfDateTime());
			}
			if (flag)
			{
				text2 += string.Format(" AND {0} ", arg);
			}
			string sqlFindSwipeRecord = wgAppConfig.getSqlFindSwipeRecord(text, "t_d_SwipeRecord", text2, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindSwipeRecord);
		}

		public void btnQuery_Click_Acc(object sender, EventArgs e)
		{
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			string arg = "";
			bool flag = false;
			if (this.dfrmFindOption != null && this.dfrmFindOption.Visible)
			{
				flag = true;
				arg = " (t_d_SwipeRecord.f_ReaderID IN ( " + this.dfrmFindOption.getStrSql() + " )) ";
			}
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
			text += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
			text += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, ";
			text += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
			string text2 = " ( 1>0 ) ";
			if (this.getSqlOfDateTime() != "")
			{
				text2 += string.Format(" AND {0} ", this.getSqlOfDateTime());
			}
			if (flag)
			{
				text2 += string.Format(" AND {0} ", arg);
			}
			string sqlFindSwipeRecord = wgAppConfig.getSqlFindSwipeRecord(text, "t_d_SwipeRecord", text2, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindSwipeRecord);
		}

		private void reloadData(string strsql)
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
			this.dgvSwipeRecords.DataSource = null;
			this.timer1.Enabled = true;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void frmSwipeRecords_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFindOption != null)
			{
				this.dfrmFindOption.Close();
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedIndex == 1)
			{
				wgAppConfig.printdgv(this.dgvSubtotal, this.Text + " [" + this.tabPage2.Text + "]");
				return;
			}
			if (this.tabControl1.SelectedIndex == 2)
			{
				wgAppConfig.printdgv(this.dgvStatistics, this.Text + " [" + this.tabPage3.Text + "]");
				return;
			}
			wgAppConfig.printdgv(this.dgvSwipeRecords, this.Text + " [" + this.tabPage1.Text + "]");
		}

		private void fillDgv(DataTable dt)
		{
			try
			{
				if (this.dgvSwipeRecords.DataSource == null)
				{
					this.dgvSwipeRecords.DataSource = dt;
					for (int i = 0; i < dt.Columns.Count; i++)
					{
						this.dgvSwipeRecords.Columns[i].DataPropertyName = dt.Columns[i].ColumnName;
						this.dgvSwipeRecords.Columns[i].Name = dt.Columns[i].ColumnName;
					}
					wgAppConfig.setDisplayFormatDate(this.dgvSwipeRecords, "f_ReadDate", wgTools.DisplayFormat_DateYMDHMSWeek);
					wgAppConfig.ReadGVStyle(this, this.dgvSwipeRecords);
					if (this.startRecordIndex == 0 && dt.Rows.Count >= this.MaxRecord)
					{
						this.startRecordIndex += this.MaxRecord;
						wgTools.WgDebugWrite("First 1000", new object[0]);
						this.backgroundWorker1.RunWorkerAsync(new object[]
						{
							this.startRecordIndex,
							this.MaxRecord,
							this.dgvSql
						});
					}
				}
				else if (dt.Rows.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = this.dgvSwipeRecords.FirstDisplayedScrollingRowIndex;
					DataTable dataTable = this.dgvSwipeRecords.DataSource as DataTable;
					dataTable.Merge(dt);
					if (firstDisplayedScrollingRowIndex > 0)
					{
						this.dgvSwipeRecords.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
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
			e.Result = this.loadSwipeRecords(startIndex, maxRecords, strSql);
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				XMessageBox.Show(CommonStr.strOperationCanceled);
				return;
			}
			if (e.Error != null)
			{
				string text = string.Format("An error occurred: {0}", e.Error.Message);
				XMessageBox.Show(text);
				return;
			}
			if ((e.Result as DataTable).Rows.Count < this.MaxRecord)
			{
				this.bLoadedFinished = true;
			}
			this.fillDgv(e.Result as DataTable);
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvSwipeRecords.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvSwipeRecords_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > this.dgvSwipeRecords.Rows.Count || e.NewValue + this.dgvSwipeRecords.Rows.Count / 10 > this.dgvSwipeRecords.Rows.Count))
				{
					if (this.startRecordIndex <= this.dgvSwipeRecords.Rows.Count)
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
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvSwipeRecords.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void dgvSwipeRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < this.dgvSwipeRecords.Columns.Count && this.dgvSwipeRecords.Columns[e.ColumnIndex].Name.Equals("f_Desc"))
			{
				string text = e.Value as string;
				if (text != null && text != " ")
				{
					return;
				}
				DataGridViewCell dataGridViewCell = this.dgvSwipeRecords[e.ColumnIndex, e.RowIndex];
				string text2 = this.dgvSwipeRecords[e.ColumnIndex + 1, e.RowIndex].Value as string;
				if (string.IsNullOrEmpty(text2))
				{
					e.Value = "";
					dataGridViewCell.Value = "";
					return;
				}
				MjRec mjRec = new MjRec(text2.PadLeft(48, '0'));
				e.Value = mjRec.GetDetailedRecord(null, 0u);
				dataGridViewCell.Value = e.Value;
			}
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedIndex == 1)
			{
				wgAppConfig.exportToExcel(this.dgvSubtotal, this.Text + " [" + this.tabPage2.Text + "]");
				return;
			}
			if (this.tabControl1.SelectedIndex == 2)
			{
				wgAppConfig.exportToExcel(this.dgvStatistics, this.Text + " [" + this.tabPage3.Text + "]");
				return;
			}
			wgAppConfig.exportToExcel(this.dgvSwipeRecords, this.Text + " [" + this.tabPage1.Text + "]");
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.dgvSwipeRecords.DataSource == null)
			{
				Cursor.Current = Cursors.WaitCursor;
				return;
			}
			Cursor.Current = Cursors.Default;
			this.timer1.Enabled = false;
		}

		private void btnFindOption_Click(object sender, EventArgs e)
		{
			if (this.dfrmFindOption == null)
			{
				this.dfrmFindOption = new dfrmSwipeRecordsFindOption();
				this.dfrmFindOption.Owner = this;
			}
			this.dfrmFindOption.Show();
		}

		private void frmSwipeRecords_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.SaveDGVStyle(this, this.dgvSwipeRecords);
			XMessageBox.Show(sender.ToString() + " " + CommonStr.strSuccessfully);
		}

		private void restoreDefaultLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.RestoreGVStyle(this, this.dgvSwipeRecords);
			this.loadDefaultStyle();
			this.loadStyle();
		}

		private void btnCreateReport_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			this.startDeal();
			Cursor.Current = Cursors.Default;
		}

		public void startDeal()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.startDeal_Acc();
				return;
			}
			this.btnCreateReport.Enabled = false;
			Cursor current = Cursor.Current;
			string text = "";
			SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
			Cursor.Current = Cursors.WaitCursor;
			this.btnCreateReport.Enabled = false;
			try
			{
				int groupMinNO = 0;
				int groupIDOfMinNO = 0;
				int groupMaxNO = 0;
				string findName = "";
				long findCard = 0L;
				int findConsumerID = 0;
				this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
				string text2 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text2 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text2 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, t_d_SwipeRecord.f_ReaderID, t_b_Consumer.f_ConsumerID, ";
				text2 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
				string text3 = " ( 1>0 ) ";
				if (this.getSqlOfDateTime() != "")
				{
					text3 += string.Format(" AND {0} ", this.getSqlOfDateTime());
				}
				text3 = text3 + " AND  ([f_ReadDate]<= " + wgTools.PrepareStr(DateTime.Now.AddDays(1.0).ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59") + ")";
				string text4 = this.getSqlFindSwipeRecord4Meal(text2, "t_d_SwipeRecord", text3, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
				this.tabPage3.Text = this.oldStatTitle + string.Format("({0} {1} {2})", this.dtpDateFrom.Value.ToString(wgTools.DisplayFormat_DateYMD), this.toolStripLabel3.Text.Replace(":", ""), this.dtpDateTo.Value.ToString(wgTools.DisplayFormat_DateYMD));
				this.ProgressBar1.Value = 0;
				this.ProgressBar1.Maximum = 100;
				this.ProgressBar1.Value = 30;
				this.ds = new DataSet("inout");
				SqlCommand sqlCommand = new SqlCommand(text4);
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandTimeout = 180;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				this.ProgressBar1.Value = 40;
				sqlDataAdapter.Fill(this.ds, "t_d_SwipeRecord");
				this.ProgressBar1.Value = 60;
				text4 = " SELECT  f_RecID, 0 as f_ConsumerID, '' AS f_GroupName, '' as f_ConsumerNO,  '' AS f_ConsumerName,t_d_SwipeRecord.f_ReadDate , '' as f_MealName, 0.01 as f_Cost, '' as [f_ReaderName],f_ReaderID  ";
				text4 += " FROM t_d_SwipeRecord ";
				text4 += " WHERE 1<0 ";
				SqlCommand selectCommand = new SqlCommand(text4, sqlConnection);
				sqlDataAdapter.SelectCommand = selectCommand;
				sqlDataAdapter.Fill(this.ds, "MealReport");
				text4 = "  SELECT  t_d_Reader4Meal.*  ";
				text4 += " FROM  t_b_Reader,t_d_Reader4Meal, t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  AND t_b_Reader.f_ReaderID = t_d_Reader4Meal.f_ReaderID ";
				text4 += " ORDER BY  t_b_Reader.f_ReaderID  ";
				selectCommand = new SqlCommand(text4, sqlConnection);
				sqlDataAdapter.SelectCommand = selectCommand;
				sqlDataAdapter.Fill(this.ds, "t_d_Reader4Meal");
				DataView dataView = new DataView(this.ds.Tables["t_d_Reader4Meal"]);
				text4 = "  SELECT  t_b_Reader.f_ReaderID , t_b_Reader.[f_ReaderName], 0 as f_CostCount, 0.01 as f_CostTotal4Reader  ";
				text4 += " FROM  t_b_Reader,t_d_Reader4Meal  , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  AND t_b_Reader.f_ReaderID = t_d_Reader4Meal.f_ReaderID ";
				text4 += " ORDER BY  t_b_Reader.f_ReaderID  ";
				selectCommand = new SqlCommand(text4, sqlConnection);
				sqlDataAdapter.SelectCommand = selectCommand;
				sqlDataAdapter.Fill(this.ds, "ReaderStatistics");
				DataTable dataTable = this.ds.Tables["ReaderStatistics"];
				this.dvReaderStatistics = new DataView(this.ds.Tables["ReaderStatistics"]);
				int i;
				for (i = 0; i <= this.dvReaderStatistics.Count - 1; i++)
				{
					this.dvReaderStatistics[i]["f_CostTotal4Reader"] = 0;
				}
				dataTable.AcceptChanges();
				this.ProgressBar1.Value = 70;
				if (this.dvReaderStatistics.Count > 0)
				{
					text = text + "  f_ReaderID IN ( " + this.dvReaderStatistics[0]["f_ReaderID"];
					for (int j = 1; j <= this.dvReaderStatistics.Count - 1; j++)
					{
						text = text + "," + this.dvReaderStatistics[j]["f_ReaderID"];
					}
					text += ")";
				}
				else
				{
					text += " 1<0 ";
				}
				text2 = " SELECT    f_ConsumerID, f_GroupName,  f_ConsumerNO, f_ConsumerName ";
				text2 += " , 0 as f_CostMorningCount, 0 as f_CostLunchCount, 0 as f_CostEveningCount ,0 as f_CostOtherCount, 0 as f_CostTotalCount, 0.01 as f_CostTotal,  0.01 as f_CostMorning, 0.01 as f_CostLunch, 0.01 as f_CostEvening ,0.01 as f_CostOther ";
				text4 = this.getQueryConsumerConditionStr(text2, "", text3, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
				selectCommand = new SqlCommand(text4, sqlConnection);
				sqlDataAdapter.SelectCommand = selectCommand;
				sqlDataAdapter.Fill(this.ds, "ConsumerStatistics");
				DataTable dataTable2 = this.ds.Tables["ConsumerStatistics"];
				this.dvConsumerStatistics = new DataView(this.ds.Tables["ConsumerStatistics"]);
				for (i = 0; i <= this.dvConsumerStatistics.Count - 1; i++)
				{
					dataTable2.Rows[i]["f_CostMorning"] = 0;
					dataTable2.Rows[i]["f_CostLunch"] = 0;
					dataTable2.Rows[i]["f_CostEvening"] = 0;
					dataTable2.Rows[i]["f_CostOther"] = 0;
					dataTable2.Rows[i]["f_CostTotal"] = 0;
				}
				dataTable2.AcceptChanges();
				this.ProgressBar1.Value = 80;
				DataTable dataTable3 = this.ds.Tables["MealReport"];
				DataView dataView2 = new DataView(this.ds.Tables["t_d_SwipeRecord"]);
				sqlDataAdapter = new SqlDataAdapter("SELECT * from t_b_reader  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) ", sqlConnection);
				sqlDataAdapter.Fill(this.ds, "t_b_reader");
				DataTable dataTable4 = this.ds.Tables["t_b_reader"];
				new DataView(dataTable4);
				int num = 0;
				int num2 = 0;
				SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString);
				sqlCommand = new SqlCommand("SELECT * from t_b_MealSetup WHERE f_ID=1 ", sqlConnection2);
				if (sqlConnection2.State != ConnectionState.Open)
				{
					sqlConnection2.Open();
				}
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) == 1)
					{
						num2 = 1;
					}
					else
					{
						if (int.Parse(wgTools.SetObjToStr(sqlDataReader["f_Value"])) == 2)
						{
							num2 = 2;
							try
							{
								num = (int)decimal.Parse(wgTools.SetObjToStr(sqlDataReader["f_ParamVal"]));
								goto IL_6A0;
							}
							catch (Exception ex)
							{
								wgTools.WgDebugWrite(ex.ToString(), new object[0]);
								goto IL_6A0;
							}
						}
						num2 = 0;
						num = 0;
					}
				}
				IL_6A0:
				sqlDataReader.Close();
				sqlCommand.CommandText = "SELECT * from t_b_MealSetup WHERE f_ID > 1 ORDER BY f_ID ASC";
				if (sqlConnection2.State != ConnectionState.Open)
				{
					sqlConnection2.Open();
				}
				sqlDataReader = sqlCommand.ExecuteReader();
				int[] array = new int[4];
				string[] array2 = new string[4];
				string[] array3 = new string[4];
				DateTime[] array4 = new DateTime[4];
				DateTime[] array5 = new DateTime[4];
				decimal[] array6 = new decimal[4];
				for (int k = 0; k <= array.Length - 1; k++)
				{
					array[k] = 0;
					array4[k] = DateTime.Parse("00:00");
					array5[k] = DateTime.Parse("00:00");
					array6[k] = 0m;
				}
				array2[0] = CommonStr.strMealName0;
				array2[1] = CommonStr.strMealName1;
				array2[2] = CommonStr.strMealName2;
				array2[3] = CommonStr.strMealName3;
				array3[0] = "f_CostMorning";
				array3[1] = "f_CostLunch";
				array3[2] = "f_CostEvening";
				array3[3] = "f_CostOther";
				while (sqlDataReader.Read())
				{
					if ((int)sqlDataReader["f_ID"] == 2 || (int)sqlDataReader["f_ID"] == 3 || (int)sqlDataReader["f_ID"] == 4 || (int)sqlDataReader["f_ID"] == 5)
					{
						if ((int)sqlDataReader["f_Value"] > 0)
						{
							int k = (int)sqlDataReader["f_ID"] - 2;
							array[k] = 1;
							array4[k] = (DateTime)sqlDataReader["f_BeginHMS"];
							array4[k] = array4[k].AddSeconds((double)(-(double)array4[k].Second));
							array5[k] = (DateTime)sqlDataReader["f_EndHMS"];
							array5[k] = array5[k].AddSeconds((double)(59 - array5[k].Second));
							array6[k] = decimal.Parse(wgTools.SetObjToStr(sqlDataReader["f_ParamVal"]));
						}
					}
					else if ((int)sqlDataReader["f_ID"] == 6 && (int)sqlDataReader["f_Value"] > 0)
					{
						text += " AND f_Character=1 ";
					}
				}
				sqlDataReader.Close();
				dataView2.Sort = "f_ReadDate ASC";
				this.ProgressBar1.Value = 0;
				this.dvConsumerStatistics = new DataView(this.ds.Tables["ConsumerStatistics"]);
				this.ProgressBar1.Maximum = Math.Max(0, this.dvConsumerStatistics.Count);
				for (int l = 0; l <= this.dvConsumerStatistics.Count - 1; l++)
				{
					this.ProgressBar1.Value = l;
					dataView2.RowFilter = text + " AND  f_ConsumerID = " + this.dvConsumerStatistics[l]["f_ConsumerID"];
					if (dataView2.Count > 0)
					{
						string text5 = "";
						DateTime value = DateTime.Parse("2100-1-1");
						string a = "";
						int num3 = -1;
						string text6 = "";
						for (int m = 0; m <= dataView2.Count - 1; m++)
						{
							DateTime dateTime = (DateTime)dataView2[m]["f_ReadDate"];
							bool flag = false;
							int k;
							for (k = 0; k <= array.Length - 1; k++)
							{
								if (array[k] > 0)
								{
									if (k < 3)
									{
										if (string.Compare(dateTime.ToString("HH:mm"), array4[k].ToString("HH:mm")) >= 0 && string.Compare(dateTime.ToString("HH:mm"), array5[k].ToString("HH:mm")) <= 0)
										{
											flag = true;
											text5 = array2[k];
											text6 = array3[k];
											break;
										}
									}
									else if (string.Compare(array4[k].ToString("HH:mm"), array5[k].ToString("HH:mm")) < 0)
									{
										if (string.Compare(dateTime.ToString("HH:mm"), array4[k].ToString("HH:mm")) >= 0 && string.Compare(dateTime.ToString("HH:mm"), array5[k].ToString("HH:mm")) <= 0)
										{
											flag = true;
											text5 = array2[k];
											text6 = array3[k];
											break;
										}
									}
									else if (string.Compare(dateTime.ToString("HH:mm"), array4[k].ToString("HH:mm")) >= 0 || string.Compare(dateTime.ToString("HH:mm"), array5[k].ToString("HH:mm")) <= 0)
									{
										flag = true;
										text5 = array2[k];
										text6 = array3[k];
										break;
									}
								}
							}
							if (flag)
							{
								bool flag2 = true;
								TimeSpan timeSpan = Convert.ToDateTime(dataView2[m]["f_ReadDate"]).Subtract(value);
								if (num2 == 1 && a == text5 && Math.Abs(timeSpan.TotalHours) < 12.0)
								{
									flag2 = false;
								}
								if (num2 == 2 && a == text5 && Math.Abs(timeSpan.TotalSeconds) < (double)num && num3 == (int)dataView2[m]["f_ReaderID"])
								{
									flag2 = false;
								}
								if (flag2)
								{
									a = text5;
									value = (DateTime)dataView2[m]["f_ReadDate"];
									num3 = (int)dataView2[m]["f_ReaderID"];
									DataRow dataRow = dataTable3.NewRow();
									dataRow["f_RecID"] = dataView2[m]["f_RecID"];
									dataRow["f_GroupName"] = dataView2[m]["f_GroupName"];
									dataRow["f_ConsumerNO"] = dataView2[m]["f_ConsumerNO"];
									dataRow["f_ConsumerID"] = dataView2[m]["f_ConsumerID"];
									dataRow["f_ConsumerName"] = dataView2[m]["f_ConsumerName"];
									dataRow["f_ReaderName"] = dataView2[m]["f_ReaderName"];
									dataRow["f_ReaderID"] = dataView2[m]["f_ReaderID"];
									dataRow["f_ReadDate"] = dataView2[m]["f_ReadDate"];
									dataRow["f_MealName"] = text5;
									dataRow["f_Cost"] = array6[k];
									if (!string.IsNullOrEmpty(text6))
									{
										dataView.RowFilter = string.Format("{0}>=0 AND f_ReaderID ={1} ", text6, dataRow["f_ReaderID"].ToString());
										if (dataView.Count > 0)
										{
											dataRow["f_Cost"] = decimal.Parse(wgTools.SetObjToStr(dataView[0][text6]));
										}
									}
									dataTable3.Rows.Add(dataRow);
								}
							}
						}
						dataTable3.AcceptChanges();
					}
				}
				this.dv = new DataView(this.ds.Tables["MealReport"]);
				int num4 = 0;
				decimal num5 = 0m;
				this.dv.RowFilter = "";
				if (this.dv.Count > 0)
				{
					this.ProgressBar1.Value = 0;
					this.ProgressBar1.Maximum = Math.Max(0, dataTable.Rows.Count);
					for (int n = 0; n <= dataTable.Rows.Count - 1; n++)
					{
						this.ProgressBar1.Value = n;
						string text7 = "f_ReaderID = " + (int)dataTable.Rows[n]["f_ReaderID"];
						this.dv.RowFilter = text7;
						if (this.dv.Count > 0)
						{
							if (this.dv.Count > 0)
							{
								dataTable.Rows[n]["f_CostCount"] = (int)dataTable.Rows[n]["f_CostCount"] + this.dv.Count;
								num4 += this.dv.Count;
							}
							for (int num6 = 0; num6 <= this.dv.Count - 1; num6++)
							{
								dataTable.Rows[n]["f_CostTotal4Reader"] = (decimal)dataTable.Rows[n]["f_CostTotal4Reader"] + (decimal)this.dv[num6]["f_Cost"];
								num5 += (decimal)this.dv[num6]["f_Cost"];
							}
						}
					}
				}
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["f_ReaderName"] = CommonStr.strMealTotal;
				dataRow2["f_CostCount"] = num4;
				dataRow2["f_CostTotal4Reader"] = num5;
				dataTable.Rows.Add(dataRow2);
				dataTable.AcceptChanges();
				this.dv.RowFilter = "";
				this.dv.RowFilter = "";
				if (this.dv.Count > 0)
				{
					this.ProgressBar1.Value = 0;
					this.ProgressBar1.Maximum = Math.Max(0, dataTable2.Rows.Count);
					for (int l = 0; l <= dataTable2.Rows.Count - 1; l++)
					{
						this.ProgressBar1.Value = l;
						string text7 = "f_ConsumerID = " + dataTable2.Rows[l]["f_ConsumerID"];
						this.dv.RowFilter = text7;
						if (this.dv.Count > 0)
						{
							for (int k = 0; k <= array.Length - 1; k++)
							{
								if (array[k] > 0)
								{
									this.dv.RowFilter = text7 + " AND f_MealName= " + wgTools.PrepareStr(array2[k]);
									dataTable2.Rows[l][array3[k] + "Count"] = (int)dataTable2.Rows[l][array3[k] + "Count"] + this.dv.Count;
									dataTable2.Rows[l]["f_CostTotalCount"] = (int)dataTable2.Rows[l]["f_CostTotalCount"] + this.dv.Count;
									for (int num7 = 0; num7 <= this.dv.Count - 1; num7++)
									{
										dataTable2.Rows[l][array3[k]] = (decimal)dataTable2.Rows[l][array3[k]] + (decimal)this.dv[num7]["f_Cost"];
										dataTable2.Rows[l]["f_CostTotal"] = (decimal)dataTable2.Rows[l]["f_CostTotal"] + (decimal)this.dv[num7]["f_Cost"];
									}
								}
							}
						}
					}
				}
				dataTable2.AcceptChanges();
				DataRow dataRow3 = dataTable2.NewRow();
				dataRow3["f_GroupName"] = "==========";
				dataRow3["f_ConsumerNO"] = "==========";
				dataRow3["f_ConsumerName"] = CommonStr.strMealTotal;
				for (int k = 4; k <= 13; k++)
				{
					dataRow3[k] = 0;
				}
				for (int l = 0; l <= dataTable2.Rows.Count - 1; l++)
				{
					this.ProgressBar1.Value = l;
					for (int k = 4; k <= 13; k++)
					{
						if (dataRow3[k].GetType().Name.ToString() == "Decimal")
						{
							dataRow3[k] = (decimal)dataRow3[k] + (decimal)dataTable2.Rows[l][k];
						}
						else
						{
							dataRow3[k] = (int)dataRow3[k] + (int)dataTable2.Rows[l][k];
						}
					}
				}
				dataTable2.Rows.Add(dataRow3);
				this.ProgressBar1.Value = 0;
				this.dgvSwipeRecords.AutoGenerateColumns = false;
				this.dgvSubtotal.AutoGenerateColumns = false;
				this.dgvStatistics.AutoGenerateColumns = false;
				this.dgvSwipeRecords.DataSource = this.ds.Tables["MealReport"];
				DataTable dataTable5 = this.ds.Tables["MealReport"];
				i = 0;
				while (i < this.dgvSwipeRecords.ColumnCount && i < dataTable5.Columns.Count)
				{
					this.dgvSwipeRecords.Columns[i].DataPropertyName = dataTable5.Columns[i].ColumnName;
					i++;
				}
				wgAppConfig.setDisplayFormatDate(this.dgvSwipeRecords, "f_ReadDate", wgTools.DisplayFormat_DateYMDHMSWeek);
				this.dgvSubtotal.DataSource = dataTable;
				dataTable5 = dataTable;
				i = 0;
				while (i < this.dgvSubtotal.ColumnCount && i < dataTable5.Columns.Count)
				{
					this.dgvSubtotal.Columns[i].DataPropertyName = dataTable5.Columns[i].ColumnName;
					i++;
				}
				this.dgvStatistics.DataSource = dataTable2;
				dataTable5 = dataTable2;
				i = 0;
				while (i < this.dgvStatistics.ColumnCount && i < dataTable5.Columns.Count)
				{
					this.dgvStatistics.Columns[i].DataPropertyName = dataTable5.Columns[i].ColumnName;
					i++;
				}
				this.dgvSwipeRecords.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvSubtotal.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvStatistics.DefaultCellStyle.ForeColor = Color.Black;
				if (this.dgvSwipeRecords.Rows.Count <= 0)
				{
					this.btnPrint.Enabled = false;
					this.btnExportToExcel.Enabled = false;
					XMessageBox.Show(CommonStr.strMealNoRecords);
				}
				else
				{
					this.btnPrint.Enabled = true;
					this.btnExportToExcel.Enabled = true;
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			finally
			{
				this.btnCreateReport.Enabled = true;
				Cursor.Current = current;
			}
		}

		public void startDeal_Acc()
		{
			this.btnCreateReport.Enabled = false;
			Cursor current = Cursor.Current;
			string text = "";
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			Cursor.Current = Cursors.WaitCursor;
			this.btnCreateReport.Enabled = false;
			try
			{
				int groupMinNO = 0;
				int groupIDOfMinNO = 0;
				int groupMaxNO = 0;
				string findName = "";
				long findCard = 0L;
				int findConsumerID = 0;
				this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
				string text2 = " SELECT t_d_SwipeRecord.f_RecID, t_d_SwipeRecord.f_CardNO,  ";
				text2 += "        t_b_Consumer.f_ConsumerNO, t_b_Consumer.f_ConsumerName, ";
				text2 += "        t_b_Group.f_GroupName, t_d_SwipeRecord.f_ReadDate, t_b_Reader.f_ReaderName, t_d_SwipeRecord.f_ReaderID, t_b_Consumer.f_ConsumerID, ";
				text2 += "        t_d_SwipeRecord.f_Character, ' ' as f_Desc, t_d_SwipeRecord.f_RecordAll ";
				string text3 = " ( 1>0 ) ";
				if (this.getSqlOfDateTime() != "")
				{
					text3 += string.Format(" AND {0} ", this.getSqlOfDateTime());
				}
				text3 = text3 + " AND  ([f_ReadDate]<= " + wgTools.PrepareStr(DateTime.Now.AddDays(1.0).ToString(wgTools.YMDHMSFormat), true, "yyyy-MM-dd 23:59:59") + ")";
				string text4 = this.getSqlFindSwipeRecord4Meal(text2, "t_d_SwipeRecord", text3, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
				this.tabPage3.Text = this.oldStatTitle + string.Format("({0} {1} {2})", this.dtpDateFrom.Value.ToString(wgTools.DisplayFormat_DateYMD), this.toolStripLabel3.Text.Replace(":", ""), this.dtpDateTo.Value.ToString(wgTools.DisplayFormat_DateYMD));
				this.ProgressBar1.Value = 0;
				this.ProgressBar1.Maximum = 100;
				this.ProgressBar1.Value = 30;
				this.ds = new DataSet("inout");
				OleDbCommand oleDbCommand = new OleDbCommand(text4);
				oleDbCommand.Connection = oleDbConnection;
				oleDbCommand.CommandTimeout = 180;
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
				this.ProgressBar1.Value = 40;
				oleDbDataAdapter.Fill(this.ds, "t_d_SwipeRecord");
				this.ProgressBar1.Value = 60;
				text4 = " SELECT  f_RecID, 0 as f_ConsumerID, '' AS f_GroupName, '' as f_ConsumerNO,  '' AS f_ConsumerName,t_d_SwipeRecord.f_ReadDate , '' as f_MealName, 0.01 as f_Cost, '' as [f_ReaderName],f_ReaderID  ";
				text4 += " FROM t_d_SwipeRecord ";
				text4 += " WHERE 1<0 ";
				OleDbCommand selectCommand = new OleDbCommand(text4, oleDbConnection);
				oleDbDataAdapter.SelectCommand = selectCommand;
				oleDbDataAdapter.Fill(this.ds, "MealReport");
				text4 = "  SELECT  t_d_Reader4Meal.*  ";
				text4 += " FROM  t_b_Reader,t_d_Reader4Meal  , t_b_Controller WHERE  ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID )  AND t_b_Reader.f_ReaderID = t_d_Reader4Meal.f_ReaderID ";
				text4 += " ORDER BY  t_b_Reader.f_ReaderID  ";
				selectCommand = new OleDbCommand(text4, oleDbConnection);
				oleDbDataAdapter.SelectCommand = selectCommand;
				oleDbDataAdapter.Fill(this.ds, "t_d_Reader4Meal");
				DataView dataView = new DataView(this.ds.Tables["t_d_Reader4Meal"]);
				text4 = "  SELECT  t_b_Reader.f_ReaderID , t_b_Reader.[f_ReaderName], 0 as f_CostCount, 0.01 as f_CostTotal4Reader  ";
				text4 += " FROM  t_b_Reader,t_d_Reader4Meal , t_b_Controller WHERE ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) AND t_b_Reader.f_ReaderID = t_d_Reader4Meal.f_ReaderID ";
				text4 += " ORDER BY  t_b_Reader.f_ReaderID  ";
				selectCommand = new OleDbCommand(text4, oleDbConnection);
				oleDbDataAdapter.SelectCommand = selectCommand;
				oleDbDataAdapter.Fill(this.ds, "ReaderStatistics");
				DataTable dataTable = this.ds.Tables["ReaderStatistics"];
				this.dvReaderStatistics = new DataView(this.ds.Tables["ReaderStatistics"]);
				int i;
				for (i = 0; i <= this.dvReaderStatistics.Count - 1; i++)
				{
					this.dvReaderStatistics[i]["f_CostTotal4Reader"] = 0;
				}
				dataTable.AcceptChanges();
				this.ProgressBar1.Value = 70;
				if (this.dvReaderStatistics.Count > 0)
				{
					text = text + "  f_ReaderID IN ( " + this.dvReaderStatistics[0]["f_ReaderID"];
					for (int j = 1; j <= this.dvReaderStatistics.Count - 1; j++)
					{
						text = text + "," + this.dvReaderStatistics[j]["f_ReaderID"];
					}
					text += ")";
				}
				else
				{
					text += " 1<0 ";
				}
				text2 = " SELECT    f_ConsumerID, f_GroupName,  f_ConsumerNO, f_ConsumerName ";
				text2 += " , 0 as f_CostMorningCount, 0 as f_CostLunchCount, 0 as f_CostEveningCount ,0 as f_CostOtherCount, 0 as f_CostTotalCount, 0.01 as f_CostTotal,  0.01 as f_CostMorning, 0.01 as f_CostLunch, 0.01 as f_CostEvening ,0.01 as f_CostOther ";
				text4 = this.getQueryConsumerConditionStr(text2, "", text3, groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
				selectCommand = new OleDbCommand(text4, oleDbConnection);
				oleDbDataAdapter.SelectCommand = selectCommand;
				oleDbDataAdapter.Fill(this.ds, "ConsumerStatistics");
				DataTable dataTable2 = this.ds.Tables["ConsumerStatistics"];
				this.dvConsumerStatistics = new DataView(this.ds.Tables["ConsumerStatistics"]);
				for (i = 0; i <= this.dvConsumerStatistics.Count - 1; i++)
				{
					dataTable2.Rows[i]["f_CostMorning"] = 0;
					dataTable2.Rows[i]["f_CostLunch"] = 0;
					dataTable2.Rows[i]["f_CostEvening"] = 0;
					dataTable2.Rows[i]["f_CostOther"] = 0;
					dataTable2.Rows[i]["f_CostTotal"] = 0;
				}
				dataTable2.AcceptChanges();
				this.ProgressBar1.Value = 80;
				DataTable dataTable3 = this.ds.Tables["MealReport"];
				DataView dataView2 = new DataView(this.ds.Tables["t_d_SwipeRecord"]);
				oleDbDataAdapter = new OleDbDataAdapter("SELECT * from t_b_reader  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) ", oleDbConnection);
				oleDbDataAdapter.Fill(this.ds, "t_b_reader");
				DataTable dataTable4 = this.ds.Tables["t_b_reader"];
				new DataView(dataTable4);
				int num = 0;
				int num2 = 0;
				OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString);
				oleDbCommand = new OleDbCommand("SELECT * from t_b_MealSetup WHERE f_ID=1 ", oleDbConnection2);
				if (oleDbConnection2.State != ConnectionState.Open)
				{
					oleDbConnection2.Open();
				}
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.Read())
				{
					if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) == 1)
					{
						num2 = 1;
					}
					else
					{
						if (int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_Value"])) == 2)
						{
							num2 = 2;
							try
							{
								num = (int)decimal.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ParamVal"]));
								goto IL_692;
							}
							catch (Exception ex)
							{
								wgTools.WgDebugWrite(ex.ToString(), new object[0]);
								goto IL_692;
							}
						}
						num2 = 0;
						num = 0;
					}
				}
				IL_692:
				oleDbDataReader.Close();
				oleDbCommand.CommandText = "SELECT * from t_b_MealSetup WHERE f_ID > 1 ORDER BY f_ID ASC";
				if (oleDbConnection2.State != ConnectionState.Open)
				{
					oleDbConnection2.Open();
				}
				oleDbDataReader = oleDbCommand.ExecuteReader();
				int[] array = new int[4];
				string[] array2 = new string[4];
				string[] array3 = new string[4];
				DateTime[] array4 = new DateTime[4];
				DateTime[] array5 = new DateTime[4];
				decimal[] array6 = new decimal[4];
				for (int k = 0; k <= array.Length - 1; k++)
				{
					array[k] = 0;
					array4[k] = DateTime.Parse("00:00");
					array5[k] = DateTime.Parse("00:00");
					array6[k] = 0m;
				}
				array2[0] = CommonStr.strMealName0;
				array2[1] = CommonStr.strMealName1;
				array2[2] = CommonStr.strMealName2;
				array2[3] = CommonStr.strMealName3;
				array3[0] = "f_CostMorning";
				array3[1] = "f_CostLunch";
				array3[2] = "f_CostEvening";
				array3[3] = "f_CostOther";
				while (oleDbDataReader.Read())
				{
					if ((int)oleDbDataReader["f_ID"] == 2 || (int)oleDbDataReader["f_ID"] == 3 || (int)oleDbDataReader["f_ID"] == 4 || (int)oleDbDataReader["f_ID"] == 5)
					{
						if ((int)oleDbDataReader["f_Value"] > 0)
						{
							int k = (int)oleDbDataReader["f_ID"] - 2;
							array[k] = 1;
							array4[k] = (DateTime)oleDbDataReader["f_BeginHMS"];
							array4[k] = array4[k].AddSeconds((double)(-(double)array4[k].Second));
							array5[k] = (DateTime)oleDbDataReader["f_EndHMS"];
							array5[k] = array5[k].AddSeconds((double)(59 - array5[k].Second));
							array6[k] = decimal.Parse(wgTools.SetObjToStr(oleDbDataReader["f_ParamVal"]));
						}
					}
					else if ((int)oleDbDataReader["f_ID"] == 6 && (int)oleDbDataReader["f_Value"] > 0)
					{
						text += " AND f_Character=1 ";
					}
				}
				dataView2.Sort = "f_ReadDate ASC";
				this.ProgressBar1.Value = 0;
				this.dvConsumerStatistics = new DataView(this.ds.Tables["ConsumerStatistics"]);
				this.ProgressBar1.Maximum = Math.Max(0, this.dvConsumerStatistics.Count);
				for (int l = 0; l <= this.dvConsumerStatistics.Count - 1; l++)
				{
					this.ProgressBar1.Value = l;
					dataView2.RowFilter = text + " AND  f_ConsumerID = " + this.dvConsumerStatistics[l]["f_ConsumerID"];
					if (dataView2.Count > 0)
					{
						string text5 = "";
						DateTime value = DateTime.Parse("2100-1-1");
						string a = "";
						int num3 = -1;
						string text6 = "";
						for (int m = 0; m <= dataView2.Count - 1; m++)
						{
							DateTime dateTime = (DateTime)dataView2[m]["f_ReadDate"];
							bool flag = false;
							int k;
							for (k = 0; k <= array.Length - 1; k++)
							{
								if (array[k] > 0)
								{
									if (k < 3)
									{
										if (string.Compare(dateTime.ToString("HH:mm"), array4[k].ToString("HH:mm")) >= 0 && string.Compare(dateTime.ToString("HH:mm"), array5[k].ToString("HH:mm")) <= 0)
										{
											flag = true;
											text5 = array2[k];
											text6 = array3[k];
											break;
										}
									}
									else if (string.Compare(array4[k].ToString("HH:mm"), array5[k].ToString("HH:mm")) < 0)
									{
										if (string.Compare(dateTime.ToString("HH:mm"), array4[k].ToString("HH:mm")) >= 0 && string.Compare(dateTime.ToString("HH:mm"), array5[k].ToString("HH:mm")) <= 0)
										{
											flag = true;
											text5 = array2[k];
											text6 = array3[k];
											break;
										}
									}
									else if (string.Compare(dateTime.ToString("HH:mm"), array4[k].ToString("HH:mm")) >= 0 || string.Compare(dateTime.ToString("HH:mm"), array5[k].ToString("HH:mm")) <= 0)
									{
										flag = true;
										text5 = array2[k];
										text6 = array3[k];
										break;
									}
								}
							}
							if (flag)
							{
								bool flag2 = true;
								TimeSpan timeSpan = Convert.ToDateTime(dataView2[m]["f_ReadDate"]).Subtract(value);
								if (num2 == 1 && a == text5 && Math.Abs(timeSpan.TotalHours) < 12.0)
								{
									flag2 = false;
								}
								if (num2 == 2 && a == text5 && Math.Abs(timeSpan.TotalSeconds) < (double)num && num3 == (int)dataView2[m]["f_ReaderID"])
								{
									flag2 = false;
								}
								if (flag2)
								{
									a = text5;
									value = (DateTime)dataView2[m]["f_ReadDate"];
									num3 = (int)dataView2[m]["f_ReaderID"];
									DataRow dataRow = dataTable3.NewRow();
									dataRow["f_RecID"] = dataView2[m]["f_RecID"];
									dataRow["f_GroupName"] = dataView2[m]["f_GroupName"];
									dataRow["f_ConsumerNO"] = dataView2[m]["f_ConsumerNO"];
									dataRow["f_ConsumerID"] = dataView2[m]["f_ConsumerID"];
									dataRow["f_ConsumerName"] = dataView2[m]["f_ConsumerName"];
									dataRow["f_ReaderName"] = dataView2[m]["f_ReaderName"];
									dataRow["f_ReaderID"] = dataView2[m]["f_ReaderID"];
									dataRow["f_ReadDate"] = dataView2[m]["f_ReadDate"];
									dataRow["f_MealName"] = text5;
									dataRow["f_Cost"] = array6[k];
									if (!string.IsNullOrEmpty(text6))
									{
										dataView.RowFilter = string.Format("{0}>=0 AND f_ReaderID ={1} ", text6, dataRow["f_ReaderID"].ToString());
										if (dataView.Count > 0)
										{
											dataRow["f_Cost"] = decimal.Parse(wgTools.SetObjToStr(dataView[0][text6]));
										}
									}
									dataTable3.Rows.Add(dataRow);
								}
							}
						}
						dataTable3.AcceptChanges();
					}
				}
				this.dv = new DataView(this.ds.Tables["MealReport"]);
				int num4 = 0;
				decimal num5 = 0m;
				this.dv.RowFilter = "";
				if (this.dv.Count > 0)
				{
					this.ProgressBar1.Value = 0;
					this.ProgressBar1.Maximum = Math.Max(0, dataTable.Rows.Count);
					for (int n = 0; n <= dataTable.Rows.Count - 1; n++)
					{
						this.ProgressBar1.Value = n;
						string text7 = "f_ReaderID = " + (int)dataTable.Rows[n]["f_ReaderID"];
						this.dv.RowFilter = text7;
						if (this.dv.Count > 0)
						{
							if (this.dv.Count > 0)
							{
								dataTable.Rows[n]["f_CostCount"] = (int)dataTable.Rows[n]["f_CostCount"] + this.dv.Count;
								num4 += this.dv.Count;
							}
							for (int num6 = 0; num6 <= this.dv.Count - 1; num6++)
							{
								dataTable.Rows[n]["f_CostTotal4Reader"] = (decimal)dataTable.Rows[n]["f_CostTotal4Reader"] + (decimal)this.dv[num6]["f_Cost"];
								num5 += (decimal)this.dv[num6]["f_Cost"];
							}
						}
					}
				}
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["f_ReaderName"] = CommonStr.strMealTotal;
				dataRow2["f_CostCount"] = num4;
				dataRow2["f_CostTotal4Reader"] = num5;
				dataTable.Rows.Add(dataRow2);
				dataTable.AcceptChanges();
				this.dv.RowFilter = "";
				this.dv.RowFilter = "";
				if (this.dv.Count > 0)
				{
					this.ProgressBar1.Value = 0;
					this.ProgressBar1.Maximum = Math.Max(0, dataTable2.Rows.Count);
					for (int l = 0; l <= dataTable2.Rows.Count - 1; l++)
					{
						this.ProgressBar1.Value = l;
						string text7 = "f_ConsumerID = " + dataTable2.Rows[l]["f_ConsumerID"];
						this.dv.RowFilter = text7;
						if (this.dv.Count > 0)
						{
							for (int k = 0; k <= array.Length - 1; k++)
							{
								if (array[k] > 0)
								{
									this.dv.RowFilter = text7 + " AND f_MealName= " + wgTools.PrepareStr(array2[k]);
									dataTable2.Rows[l][array3[k] + "Count"] = (int)dataTable2.Rows[l][array3[k] + "Count"] + this.dv.Count;
									dataTable2.Rows[l]["f_CostTotalCount"] = (int)dataTable2.Rows[l]["f_CostTotalCount"] + this.dv.Count;
									for (int num7 = 0; num7 <= this.dv.Count - 1; num7++)
									{
										dataTable2.Rows[l][array3[k]] = (decimal)dataTable2.Rows[l][array3[k]] + (decimal)this.dv[num7]["f_Cost"];
										dataTable2.Rows[l]["f_CostTotal"] = (decimal)dataTable2.Rows[l]["f_CostTotal"] + (decimal)this.dv[num7]["f_Cost"];
									}
								}
							}
						}
					}
				}
				dataTable2.AcceptChanges();
				DataRow dataRow3 = dataTable2.NewRow();
				dataRow3["f_GroupName"] = "==========";
				dataRow3["f_ConsumerNO"] = "==========";
				dataRow3["f_ConsumerName"] = CommonStr.strMealTotal;
				for (int k = 4; k <= 13; k++)
				{
					dataRow3[k] = 0;
				}
				for (int l = 0; l <= dataTable2.Rows.Count - 1; l++)
				{
					this.ProgressBar1.Value = l;
					for (int k = 4; k <= 13; k++)
					{
						if (dataRow3[k].GetType().Name.ToString() == "Decimal")
						{
							dataRow3[k] = (decimal)dataRow3[k] + (decimal)dataTable2.Rows[l][k];
						}
						else
						{
							dataRow3[k] = (int)dataRow3[k] + (int)dataTable2.Rows[l][k];
						}
					}
				}
				dataTable2.Rows.Add(dataRow3);
				this.ProgressBar1.Value = 0;
				this.dgvSwipeRecords.AutoGenerateColumns = false;
				this.dgvSubtotal.AutoGenerateColumns = false;
				this.dgvStatistics.AutoGenerateColumns = false;
				this.dgvSwipeRecords.DataSource = this.ds.Tables["MealReport"];
				DataTable dataTable5 = this.ds.Tables["MealReport"];
				i = 0;
				while (i < this.dgvSwipeRecords.ColumnCount && i < dataTable5.Columns.Count)
				{
					this.dgvSwipeRecords.Columns[i].DataPropertyName = dataTable5.Columns[i].ColumnName;
					i++;
				}
				wgAppConfig.setDisplayFormatDate(this.dgvSwipeRecords, "f_ReadDate", wgTools.DisplayFormat_DateYMDHMSWeek);
				this.dgvSubtotal.DataSource = dataTable;
				dataTable5 = dataTable;
				i = 0;
				while (i < this.dgvSubtotal.ColumnCount && i < dataTable5.Columns.Count)
				{
					this.dgvSubtotal.Columns[i].DataPropertyName = dataTable5.Columns[i].ColumnName;
					i++;
				}
				this.dgvStatistics.DataSource = dataTable2;
				dataTable5 = dataTable2;
				i = 0;
				while (i < this.dgvStatistics.ColumnCount && i < dataTable5.Columns.Count)
				{
					this.dgvStatistics.Columns[i].DataPropertyName = dataTable5.Columns[i].ColumnName;
					i++;
				}
				this.dgvSwipeRecords.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvSubtotal.DefaultCellStyle.ForeColor = Color.Black;
				this.dgvStatistics.DefaultCellStyle.ForeColor = Color.Black;
				if (this.dgvSwipeRecords.Rows.Count <= 0)
				{
					this.btnPrint.Enabled = false;
					this.btnExportToExcel.Enabled = false;
					XMessageBox.Show(CommonStr.strMealNoRecords);
				}
				else
				{
					this.btnPrint.Enabled = true;
					this.btnExportToExcel.Enabled = true;
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			finally
			{
				this.btnCreateReport.Enabled = true;
				Cursor.Current = current;
			}
		}

		private string getSqlFindSwipeRecord4Meal(string strBaseInfo, string fromMainDt, string strTimeCon, int groupMinNO, int groupIDOfMinNO, int groupMaxNO, string findName, long findCard, int findConsumerID)
		{
			string text = "";
			try
			{
				string text2 = "";
				string text3 = " WHERE (1>0) ";
				if (!string.IsNullOrEmpty(strTimeCon))
				{
					text3 += string.Format("AND {0}", strTimeCon);
				}
				if (findConsumerID > 0)
				{
					text2 += string.Format("AND   t_b_Consumer.f_ConsumerID ={0:d} ", findConsumerID);
					text = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1})) INNER JOIN  t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID))  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) ) LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text2);
					text += text3;
					return text;
				}
				if (!string.IsNullOrEmpty(findName))
				{
					text3 += string.Format(" AND t_b_Consumer.f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", findName)));
				}
				if (findCard > 0L)
				{
					text3 += string.Format(" AND {0}.f_CardNO ={1:d} ", fromMainDt, findCard);
				}
				if (groupMinNO > 0)
				{
					if (groupMinNO >= groupMaxNO)
					{
						text = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  INNER JOIN   t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID))  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) )  INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text2, string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", groupIDOfMinNO));
					}
					else
					{
						text = strBaseInfo + string.Format(" FROM (((t_b_Consumer INNER JOIN {0} ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  INNER JOIN   t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID))  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) )  INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {2} ) ", fromMainDt, text2, string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", groupMinNO) + string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", groupMaxNO));
					}
				}
				else
				{
					text = strBaseInfo + string.Format(" FROM ((({0} INNER JOIN t_b_Consumer ON ( t_b_Consumer.f_ConsumerID = {0}.f_ConsumerID {1}))  INNER JOIN   t_b_Reader on ( t_b_Reader.f_ReaderID = {0}.f_ReaderID))  INNER JOIN t_b_Controller ON ( t_b_Reader.f_ControllerID = t_b_Controller.f_ControllerID ) )  LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) ", fromMainDt, text2);
				}
				text += text3;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return text;
		}

		private string getQueryConsumerConditionStr(string strBaseInfo, string fromMainDt, string strTimeCon, int groupMinNO, int groupIDOfMinNO, int groupMaxNO, string findName, long findCard, int findConsumerID)
		{
			string text = "";
			try
			{
				string text2 = " WHERE (1>0) ";
				if (findConsumerID > 0)
				{
					text = strBaseInfo + string.Format("  FROM t_b_Consumer  LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ) WHERE   t_b_Consumer.f_ConsumerID ={0:d} ", findConsumerID);
					return text;
				}
				if (!string.IsNullOrEmpty(findName))
				{
					text2 += string.Format(" AND t_b_Consumer.f_ConsumerName like {0} ", wgTools.PrepareStr(string.Format("%{0}%", findName)));
				}
				if (findCard > 0L)
				{
					text2 += string.Format(" AND t_b_Consumer.f_CardNO ={0:d} ", findCard);
				}
				if (groupMinNO > 0)
				{
					if (groupMinNO >= groupMaxNO)
					{
						text = strBaseInfo + string.Format("  FROM t_b_Consumer  INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {0} )  ", string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", groupIDOfMinNO));
					}
					else
					{
						text = strBaseInfo + string.Format("  FROM t_b_Consumer  INNER JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID {0} )  ", string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", groupMinNO) + string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", groupMaxNO));
					}
				}
				else
				{
					text = strBaseInfo + string.Format("  FROM t_b_Consumer  LEFT JOIN t_b_Group ON (t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  )  ", new object[0]);
				}
				text += text2;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return text;
		}

		private void btnMealSetup_Click(object sender, EventArgs e)
		{
			using (dfrmMealSetup dfrmMealSetup = new dfrmMealSetup())
			{
				dfrmMealSetup.ShowDialog();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
