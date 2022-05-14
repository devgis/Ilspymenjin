using System;
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

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class frmPatrolTaskData : frmN3000
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
					DateTime dateTime;
					if (DateTime.TryParse(value.ToString(), out dateTime) && dateTime >= (base.Control as DateTimePicker).MinDate && dateTime <= (base.Control as DateTimePicker).MaxDate)
					{
						(base.Control as DateTimePicker).Value = dateTime;
					}
				}
			}

			public ToolStripDateTime() : base(frmPatrolTaskData.ToolStripDateTime.dtp = new DateTimePicker())
			{
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && frmPatrolTaskData.ToolStripDateTime.dtp != null)
				{
					frmPatrolTaskData.ToolStripDateTime.dtp.Dispose();
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

		private DataGridView dgvMain;

		private ToolStripButton btnPrint;

		private BackgroundWorker backgroundWorker1;

		private ToolStripButton btnExportToExcel;

		private UserControlFindSecond userControlFind1;

		private ToolStrip toolStrip3;

		private ToolStripLabel toolStripLabel2;

		private ToolStripLabel toolStripLabel3;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private ToolStripButton btnClear;

		private DataGridViewTextBoxColumn f_RecID;

		private DataGridViewTextBoxColumn f_DepartmentName;

		private DataGridViewTextBoxColumn f_ConsumerNO;

		private DataGridViewTextBoxColumn f_ConsumerName;

		private DataGridViewTextBoxColumn f_DateYM;

		private DataGridViewTextBoxColumn f_ShiftID_01;

		private DataGridViewTextBoxColumn f_ShiftID_02;

		private DataGridViewTextBoxColumn f_OnDuty1Short;

		private DataGridViewTextBoxColumn f_ShiftID_04;

		private DataGridViewTextBoxColumn f_ShiftID_05;

		private DataGridViewTextBoxColumn f_ShiftID_06;

		private DataGridViewTextBoxColumn f_ShiftID_07;

		private DataGridViewTextBoxColumn f_ShiftID_08;

		private DataGridViewTextBoxColumn f_ShiftID_09;

		private DataGridViewTextBoxColumn f_ShiftID_10;

		private DataGridViewTextBoxColumn f_ShiftID_11;

		private DataGridViewTextBoxColumn f_ShiftID_12;

		private DataGridViewTextBoxColumn f_ShiftID_13;

		private DataGridViewTextBoxColumn f_ShiftID_14;

		private DataGridViewTextBoxColumn f_ShiftID_15;

		private DataGridViewTextBoxColumn f_ShiftID_16;

		private DataGridViewTextBoxColumn f_ShiftID_17;

		private DataGridViewTextBoxColumn f_ShiftID_18;

		private DataGridViewTextBoxColumn f_ShiftID_19;

		private DataGridViewTextBoxColumn f_ShiftID_20;

		private DataGridViewTextBoxColumn f_ShiftID_21;

		private DataGridViewTextBoxColumn f_ShiftID_22;

		private DataGridViewTextBoxColumn f_ShiftID_23;

		private DataGridViewTextBoxColumn f_ShiftID_24;

		private DataGridViewTextBoxColumn f_ShiftID_25;

		private DataGridViewTextBoxColumn f_ShiftID_26;

		private DataGridViewTextBoxColumn f_ShiftID_27;

		private DataGridViewTextBoxColumn f_ShiftID_28;

		private DataGridViewTextBoxColumn f_ShiftID_29;

		private DataGridViewTextBoxColumn f_ShiftID_30;

		private DataGridViewTextBoxColumn f_ShiftID_31;

		private DataGridViewTextBoxColumn f_ConsumerID;

		private ToolStripButton btnExit;

		private frmPatrolTaskData.ToolStripDateTime dtpDateFrom;

		private frmPatrolTaskData.ToolStripDateTime dtpDateTo;

		private int recIdMax;

		private DataTable table;

		private bool bLoadedFinished;

		private string dgvSql = "";

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPatrolTaskData));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.backgroundWorker1 = new BackgroundWorker();
			this.dgvMain = new DataGridView();
			this.f_RecID = new DataGridViewTextBoxColumn();
			this.f_DepartmentName = new DataGridViewTextBoxColumn();
			this.f_ConsumerNO = new DataGridViewTextBoxColumn();
			this.f_ConsumerName = new DataGridViewTextBoxColumn();
			this.f_DateYM = new DataGridViewTextBoxColumn();
			this.f_ShiftID_01 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_02 = new DataGridViewTextBoxColumn();
			this.f_OnDuty1Short = new DataGridViewTextBoxColumn();
			this.f_ShiftID_04 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_05 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_06 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_07 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_08 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_09 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_10 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_11 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_12 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_13 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_14 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_15 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_16 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_17 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_18 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_19 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_20 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_21 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_22 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_23 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_24 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_25 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_26 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_27 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_28 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_29 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_30 = new DataGridViewTextBoxColumn();
			this.f_ShiftID_31 = new DataGridViewTextBoxColumn();
			this.f_ConsumerID = new DataGridViewTextBoxColumn();
			this.toolStrip3 = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.toolStripLabel3 = new ToolStripLabel();
			this.toolStrip1 = new ToolStrip();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnClear = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnExit = new ToolStripButton();
			this.userControlFind1 = new UserControlFindSecond();
			((ISupportInitialize)this.dgvMain).BeginInit();
			this.toolStrip3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			componentResourceManager.ApplyResources(this.dgvMain, "dgvMain");
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
				this.f_RecID,
				this.f_DepartmentName,
				this.f_ConsumerNO,
				this.f_ConsumerName,
				this.f_DateYM,
				this.f_ShiftID_01,
				this.f_ShiftID_02,
				this.f_OnDuty1Short,
				this.f_ShiftID_04,
				this.f_ShiftID_05,
				this.f_ShiftID_06,
				this.f_ShiftID_07,
				this.f_ShiftID_08,
				this.f_ShiftID_09,
				this.f_ShiftID_10,
				this.f_ShiftID_11,
				this.f_ShiftID_12,
				this.f_ShiftID_13,
				this.f_ShiftID_14,
				this.f_ShiftID_15,
				this.f_ShiftID_16,
				this.f_ShiftID_17,
				this.f_ShiftID_18,
				this.f_ShiftID_19,
				this.f_ShiftID_20,
				this.f_ShiftID_21,
				this.f_ShiftID_22,
				this.f_ShiftID_23,
				this.f_ShiftID_24,
				this.f_ShiftID_25,
				this.f_ShiftID_26,
				this.f_ShiftID_27,
				this.f_ShiftID_28,
				this.f_ShiftID_29,
				this.f_ShiftID_30,
				this.f_ShiftID_31,
				this.f_ConsumerID
			});
			this.dgvMain.EnableHeadersVisualStyles = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 23;
			this.dgvMain.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.dgvMain.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvMain_CellDoubleClick);
			this.dgvMain.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvMain_CellFormatting);
			this.dgvMain.Scroll += new ScrollEventHandler(this.dgvMain_Scroll);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_RecID.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_RecID, "f_RecID");
			this.f_RecID.Name = "f_RecID";
			this.f_RecID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DepartmentName, "f_DepartmentName");
			this.f_DepartmentName.Name = "f_DepartmentName";
			this.f_DepartmentName.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ConsumerNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_ConsumerNO, "f_ConsumerNO");
			this.f_ConsumerNO.Name = "f_ConsumerNO";
			this.f_ConsumerNO.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerName, "f_ConsumerName");
			this.f_ConsumerName.Name = "f_ConsumerName";
			this.f_ConsumerName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DateYM, "f_DateYM");
			this.f_DateYM.Name = "f_DateYM";
			this.f_DateYM.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_01, "f_ShiftID_01");
			this.f_ShiftID_01.Name = "f_ShiftID_01";
			this.f_ShiftID_01.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_02, "f_ShiftID_02");
			this.f_ShiftID_02.Name = "f_ShiftID_02";
			this.f_ShiftID_02.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_OnDuty1Short, "f_OnDuty1Short");
			this.f_OnDuty1Short.Name = "f_OnDuty1Short";
			this.f_OnDuty1Short.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_04, "f_ShiftID_04");
			this.f_ShiftID_04.Name = "f_ShiftID_04";
			this.f_ShiftID_04.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_05, "f_ShiftID_05");
			this.f_ShiftID_05.Name = "f_ShiftID_05";
			this.f_ShiftID_05.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_06, "f_ShiftID_06");
			this.f_ShiftID_06.Name = "f_ShiftID_06";
			this.f_ShiftID_06.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_07, "f_ShiftID_07");
			this.f_ShiftID_07.Name = "f_ShiftID_07";
			this.f_ShiftID_07.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_08, "f_ShiftID_08");
			this.f_ShiftID_08.Name = "f_ShiftID_08";
			this.f_ShiftID_08.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_09, "f_ShiftID_09");
			this.f_ShiftID_09.Name = "f_ShiftID_09";
			this.f_ShiftID_09.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_10, "f_ShiftID_10");
			this.f_ShiftID_10.Name = "f_ShiftID_10";
			this.f_ShiftID_10.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_11, "f_ShiftID_11");
			this.f_ShiftID_11.Name = "f_ShiftID_11";
			this.f_ShiftID_11.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_12, "f_ShiftID_12");
			this.f_ShiftID_12.Name = "f_ShiftID_12";
			this.f_ShiftID_12.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_13, "f_ShiftID_13");
			this.f_ShiftID_13.Name = "f_ShiftID_13";
			this.f_ShiftID_13.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_14, "f_ShiftID_14");
			this.f_ShiftID_14.Name = "f_ShiftID_14";
			this.f_ShiftID_14.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_15, "f_ShiftID_15");
			this.f_ShiftID_15.Name = "f_ShiftID_15";
			this.f_ShiftID_15.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_16, "f_ShiftID_16");
			this.f_ShiftID_16.Name = "f_ShiftID_16";
			this.f_ShiftID_16.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_17, "f_ShiftID_17");
			this.f_ShiftID_17.Name = "f_ShiftID_17";
			this.f_ShiftID_17.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_18, "f_ShiftID_18");
			this.f_ShiftID_18.Name = "f_ShiftID_18";
			this.f_ShiftID_18.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_19, "f_ShiftID_19");
			this.f_ShiftID_19.Name = "f_ShiftID_19";
			this.f_ShiftID_19.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_20, "f_ShiftID_20");
			this.f_ShiftID_20.Name = "f_ShiftID_20";
			this.f_ShiftID_20.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_21, "f_ShiftID_21");
			this.f_ShiftID_21.Name = "f_ShiftID_21";
			this.f_ShiftID_21.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_22, "f_ShiftID_22");
			this.f_ShiftID_22.Name = "f_ShiftID_22";
			this.f_ShiftID_22.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_23, "f_ShiftID_23");
			this.f_ShiftID_23.Name = "f_ShiftID_23";
			this.f_ShiftID_23.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_24, "f_ShiftID_24");
			this.f_ShiftID_24.Name = "f_ShiftID_24";
			this.f_ShiftID_24.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_25, "f_ShiftID_25");
			this.f_ShiftID_25.Name = "f_ShiftID_25";
			this.f_ShiftID_25.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_26, "f_ShiftID_26");
			this.f_ShiftID_26.Name = "f_ShiftID_26";
			this.f_ShiftID_26.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_27, "f_ShiftID_27");
			this.f_ShiftID_27.Name = "f_ShiftID_27";
			this.f_ShiftID_27.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_28, "f_ShiftID_28");
			this.f_ShiftID_28.Name = "f_ShiftID_28";
			this.f_ShiftID_28.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_29, "f_ShiftID_29");
			this.f_ShiftID_29.Name = "f_ShiftID_29";
			this.f_ShiftID_29.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_30, "f_ShiftID_30");
			this.f_ShiftID_30.Name = "f_ShiftID_30";
			this.f_ShiftID_30.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ShiftID_31, "f_ShiftID_31");
			this.f_ShiftID_31.Name = "f_ShiftID_31";
			this.f_ShiftID_31.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ConsumerID, "f_ConsumerID");
			this.f_ConsumerID.Name = "f_ConsumerID";
			this.f_ConsumerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.toolStrip3, "toolStrip3");
			this.toolStrip3.BackColor = Color.Transparent;
			this.toolStrip3.BackgroundImage = Resources.pTools_second_title;
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.toolStripLabel3
			});
			this.toolStrip3.Name = "toolStrip3";
			componentResourceManager.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
			this.toolStripLabel2.ForeColor = Color.White;
			this.toolStripLabel2.Name = "toolStripLabel2";
			componentResourceManager.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			this.toolStripLabel3.ForeColor = Color.White;
			this.toolStripLabel3.Name = "toolStripLabel3";
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnClear,
				this.btnPrint,
				this.btnExportToExcel,
				this.btnExit
			});
			this.toolStrip1.Name = "toolStrip1";
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
			componentResourceManager.ApplyResources(this.btnClear, "btnClear");
			this.btnClear.ForeColor = Color.White;
			this.btnClear.Image = Resources.pTools_Clear_Condition;
			this.btnClear.Name = "btnClear";
			this.btnClear.Click += new EventHandler(this.btnClear_Click);
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Image = Resources.pTools_Maps_Close;
			this.btnExit.Name = "btnExit";
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this.userControlFind1, "userControlFind1");
			this.userControlFind1.BackColor = Color.Transparent;
			this.userControlFind1.BackgroundImage = Resources.pTools_second_title;
			this.userControlFind1.ForeColor = Color.White;
			this.userControlFind1.Name = "userControlFind1";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvMain);
			base.Controls.Add(this.userControlFind1);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmPatrolTaskData";
			base.FormClosing += new FormClosingEventHandler(this.frmShiftAttReport_FormClosing);
			base.Load += new EventHandler(this.frmShiftOtherData_Load);
			((ISupportInitialize)this.dgvMain).EndInit();
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmPatrolTaskData()
		{
			this.InitializeComponent();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuPatrolDetailData";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnClear.Visible = false;
				this.btnDelete.Visible = false;
				this.btnEdit.Visible = false;
			}
		}

		private void frmShiftOtherData_Load(object sender, EventArgs e)
		{
			this.f_DepartmentName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.f_DepartmentName.HeaderText);
			this.f_ConsumerNO.HeaderText = wgAppConfig.ReplaceWorkNO(this.f_ConsumerNO.HeaderText);
			this.loadOperatorPrivilege();
			this.dtpDateFrom = new frmPatrolTaskData.ToolStripDateTime();
			this.dtpDateTo = new frmPatrolTaskData.ToolStripDateTime();
			this.toolStrip3.Items.Clear();
			this.toolStrip3.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2,
				this.dtpDateFrom,
				this.toolStripLabel3,
				this.dtpDateTo
			});
			this.dtpDateFrom.BoxWidth = 120;
			this.dtpDateTo.BoxWidth = 120;
			this.userControlFind1.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.userControlFind1.toolStripLabel2.Visible = false;
			this.userControlFind1.txtFindCardID.Visible = false;
			this.dtpDateFrom.Enabled = true;
			this.dtpDateTo.Enabled = true;
			this.loadStyle();
			Cursor.Current = Cursors.WaitCursor;
			this.dtpDateTo.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-12-31"));
			this.dtpDateFrom.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01"));
			this.dtpDateFrom.BoxWidth = 150;
			this.dtpDateTo.BoxWidth = 150;
			wgAppConfig.setDisplayFormatDate(this.dtpDateFrom.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpDateTo.DateTimeControl, wgTools.DisplayFormat_DateYMDWeek);
			this.Refresh();
			this.userControlFind1.btnQuery.PerformClick();
		}

		private void loadStyle()
		{
			this.dgvMain.AutoGenerateColumns = false;
			wgAppConfig.ReadGVStyle(this, this.dgvMain);
		}

		private DataTable loadDataRecords(int startIndex, int maxRecords, string strSql)
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine(this.Text + " loadDataRecords Start");
			if (strSql.ToUpper().IndexOf("SELECT ") > 0)
			{
				strSql = string.Format("SELECT TOP {0:d} ", maxRecords) + strSql.Substring(strSql.ToUpper().IndexOf("SELECT ") + "SELECT ".Length);
			}
			if (startIndex == 0)
			{
				this.recIdMax = -2147483648;
			}
			else if (strSql.ToUpper().IndexOf(" WHERE ") > 0)
			{
				strSql += string.Format(" AND t_d_PatrolPlanData.f_RecID > {0:d}", this.recIdMax);
			}
			else
			{
				strSql += string.Format(" WHERE t_d_PatrolPlanData.f_RecID > {0:d}", this.recIdMax);
			}
			strSql += " ORDER BY t_d_PatrolPlanData.f_RecID ";
			this.table = new DataTable();
			wgTools.WriteLine("da.Fill start");
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
					goto IL_190;
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
			IL_190:
			if (this.table.Rows.Count > 0)
			{
				this.recIdMax = int.Parse(this.table.Rows[this.table.Rows.Count - 1][0].ToString());
			}
			wgTools.WriteLine("da.Fill End " + startIndex.ToString());
			Cursor.Current = Cursors.Default;
			wgTools.WriteLine(this.Text + "  loadRecords End");
			return this.table;
		}

		private string getSqlOfDateTime(string colNameOfDate)
		{
			string text = string.Concat(new string[]
			{
				"  (",
				colNameOfDate,
				" >= ",
				wgTools.PrepareStr(this.dtpDateFrom.Value.ToString("yyyy-MM")),
				")"
			});
			if (text != "")
			{
				text += " AND ";
			}
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				"  (",
				colNameOfDate,
				" < ",
				wgTools.PrepareStr(this.dtpDateTo.Value.AddMonths(1).ToString("yyyy-MM")),
				")"
			});
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			int groupMinNO = 0;
			int groupIDOfMinNO = 0;
			int groupMaxNO = 0;
			string findName = "";
			long findCard = 0L;
			int findConsumerID = 0;
			this.userControlFind1.getSqlInfo(ref groupMinNO, ref groupIDOfMinNO, ref groupMaxNO, ref findName, ref findCard, ref findConsumerID);
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT t_d_PatrolPlanData.f_RecID, t_b_Group.f_GroupName, ";
				text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
				text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
				text += " t_d_PatrolPlanData.f_DateYM, ";
				for (int i = 1; i < 31; i++)
				{
					text += string.Format(" CSTR(t_d_PatrolPlanData.f_RouteID_{0:d2}) as f_RouteID_{0:d2}, ", i);
				}
				text += "CSTR(t_d_PatrolPlanData.f_RouteID_31) as f_RouteID_31 ";
				text += " ,t_b_Consumer.f_ConsumerID  ";
			}
			else
			{
				text = " SELECT t_d_PatrolPlanData.f_RecID, t_b_Group.f_GroupName, ";
				text += "       t_b_Consumer.f_ConsumerNO AS f_ConsumerNO, ";
				text += " t_b_Consumer.f_ConsumerName AS f_ConsumerName, ";
				text += " t_d_PatrolPlanData.f_DateYM, ";
				for (int j = 1; j < 31; j++)
				{
					text += string.Format(" CONVERT(nvarchar(3),  t_d_PatrolPlanData.f_RouteID_{0:d2}) as f_RouteID_{0:d2}, ", j);
				}
				text += "CONVERT(nvarchar(3),  t_d_PatrolPlanData.f_RouteID_31) as f_RouteID_31 ";
				text += " ,t_b_Consumer.f_ConsumerID  ";
			}
			string sqlFindNormal = wgAppConfig.getSqlFindNormal(text, "t_d_PatrolPlanData", this.getSqlOfDateTime("t_d_PatrolPlanData.f_DateYM"), groupMinNO, groupIDOfMinNO, groupMaxNO, findName, findCard, findConsumerID);
			this.reloadData(sqlFindNormal);
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
			this.dgvMain.DataSource = null;
			this.backgroundWorker1.RunWorkerAsync(new object[]
			{
				this.startRecordIndex,
				this.MaxRecord,
				this.dgvSql
			});
		}

		private void frmShiftAttReport_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvMain, this.Text);
		}

		private void fillDgv(DataTable dt)
		{
			try
			{
				if (this.dgvMain.DataSource == null)
				{
					this.dgvMain.DataSource = dt;
					for (int i = 0; i < dt.Columns.Count; i++)
					{
						this.dgvMain.Columns[i].DataPropertyName = dt.Columns[i].ColumnName;
					}
					wgAppConfig.ReadGVStyle(this, this.dgvMain);
					if (this.startRecordIndex == 0 && dt.Rows.Count >= this.MaxRecord)
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
				else if (dt.Rows.Count > 0)
				{
					int firstDisplayedScrollingRowIndex = this.dgvMain.FirstDisplayedScrollingRowIndex;
					DataTable dataTable = this.dgvMain.DataSource as DataTable;
					dataTable.Merge(dt);
					if (firstDisplayedScrollingRowIndex >= 0)
					{
						this.dgvMain.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
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
			e.Result = this.loadDataRecords(startIndex, maxRecords, strSql);
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
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + (this.bLoadedFinished ? "#" : "..."));
		}

		private void dgvMain_Scroll(object sender, ScrollEventArgs e)
		{
			if (!this.bLoadedFinished && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				wgTools.WriteLine(e.OldValue.ToString());
				wgTools.WriteLine(e.NewValue.ToString());
				if (e.NewValue > e.OldValue && (e.NewValue + 100 > this.dgvMain.Rows.Count || e.NewValue + this.dgvMain.Rows.Count / 10 > this.dgvMain.Rows.Count))
				{
					if (this.startRecordIndex <= this.dgvMain.Rows.Count)
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
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dgvMain.Rows.Count.ToString() + "#");
					}
				}
			}
		}

		private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex >= 5 && e.ColumnIndex < this.dgvMain.Columns.Count)
			{
				object arg_2D_0 = e.Value;
				DataGridViewCell dataGridViewCell = this.dgvMain[e.ColumnIndex, e.RowIndex];
				string text = this.dgvMain[e.ColumnIndex, e.RowIndex].Value.ToString();
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				if (text == "0")
				{
					text = "*";
					e.Value = text;
					dataGridViewCell.Value = e.Value;
					return;
				}
				if (text == "-1")
				{
					e.Value = "-";
					dataGridViewCell.Value = e.Value;
					return;
				}
				if (text == "-2")
				{
					e.Value = DBNull.Value;
					dataGridViewCell.Value = e.Value;
					dataGridViewCell.ReadOnly = true;
					dataGridViewCell.Style.BackColor = SystemPens.InactiveBorder.Color;
				}
			}
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcelSpecial(ref this.dgvMain, this.Text, this.bLoadedFinished, ref this.backgroundWorker1, ref this.startRecordIndex, this.MaxRecord, this.dgvSql);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			using (dfrmPatrolTaskDelete dfrmPatrolTaskDelete = new dfrmPatrolTaskDelete())
			{
				dfrmPatrolTaskDelete.ShowDialog(this);
				this.btnQuery_Click(sender, null);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmPatrolTaskAutoPlan dfrmPatrolTaskAutoPlan = new dfrmPatrolTaskAutoPlan())
			{
				dfrmPatrolTaskAutoPlan.ShowDialog();
				this.btnQuery_Click(sender, null);
			}
		}

		private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (this.btnEdit.Visible)
			{
				this.btnEdit.PerformClick();
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dgvMain.SelectedCells.Count > 0 && this.dgvMain.SelectedCells[0].ColumnIndex >= 5 && this.dgvMain.SelectedCells[0].RowIndex >= 0)
				{
					DataGridViewCell dataGridViewCell = this.dgvMain.SelectedCells[0];
					DataGridViewRow dataGridViewRow = this.dgvMain.Rows[this.dgvMain.SelectedCells[0].RowIndex];
					if (dataGridViewCell.Value != DBNull.Value)
					{
						using (dfrmPatrolTaskEdit dfrmPatrolTaskEdit = new dfrmPatrolTaskEdit())
						{
							if (dfrmPatrolTaskEdit.ShowDialog() == DialogResult.OK)
							{
								int routeID = dfrmPatrolTaskEdit.routeID;
								if (wgAppConfig.IsAccessDB)
								{
									using (comPatrol_Acc comPatrol_Acc = new comPatrol_Acc())
									{
										DateTime dateShift = Convert.ToDateTime(string.Concat(new object[]
										{
											dataGridViewRow.Cells["f_DateYM"].Value,
											"-",
											dataGridViewCell.ColumnIndex - 4,
											" 12:00:00"
										}));
										long num = (long)comPatrol_Acc.shift_arrange_update(Convert.ToInt32(dataGridViewRow.Cells["f_ConsumerID"].Value), dateShift, routeID);
										if (num == 0L)
										{
											if (routeID == 0)
											{
												dataGridViewCell.Value = "*";
											}
											else
											{
												dataGridViewCell.Value = routeID;
											}
										}
										goto IL_228;
									}
								}
								using (comPatrol comPatrol = new comPatrol())
								{
									DateTime dateShift2 = Convert.ToDateTime(string.Concat(new object[]
									{
										dataGridViewRow.Cells["f_DateYM"].Value,
										"-",
										dataGridViewCell.ColumnIndex - 4,
										" 12:00:00"
									}));
									long num2 = (long)comPatrol.shift_arrange_update(Convert.ToInt32(dataGridViewRow.Cells["f_ConsumerID"].Value), dateShift2, routeID);
									if (num2 == 0L)
									{
										if (routeID == 0)
										{
											dataGridViewCell.Value = "*";
										}
										else
										{
											dataGridViewCell.Value = routeID;
										}
									}
								}
							}
							IL_228:;
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			string text = string.Format("{0}", this.btnClear.Text);
			text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			if (wgAppConfig.IsAccessDB)
			{
				using (comPatrol_Acc comPatrol_Acc = new comPatrol_Acc())
				{
					int num = comPatrol_Acc.shift_arrange_delete(0, DateTime.Parse("2000-1-1"), DateTime.Parse("2050-12-31"));
					if (num == 0)
					{
						XMessageBox.Show(this, "OK!", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						this.btnQuery_Click(sender, null);
					}
					else
					{
						XMessageBox.Show(this, comPatrol_Acc.errDesc(num) + "\r\n\r\n" + comPatrol_Acc.errInfo, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					return;
				}
			}
			using (comPatrol comPatrol = new comPatrol())
			{
				int num = comPatrol.shift_arrange_delete(0, DateTime.Parse("2000-1-1"), DateTime.Parse("2050-12-31"));
				if (num == 0)
				{
					XMessageBox.Show(this, "OK!", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.btnQuery_Click(sender, null);
				}
				else
				{
					XMessageBox.Show(this, comPatrol.errDesc(num) + "\r\n\r\n" + comPatrol.errInfo, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
