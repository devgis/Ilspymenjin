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
	public class dfrmPersonsInside : frmN3000
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnQuery;

		private ToolStripButton btnPrint;

		private ToolStripButton btnExportToExcel;

		private Button btnSelectNone;

		private Button btnSelectAll;

		private ComboBox cboZone;

		private Label label25;

		private CheckedListBox chkListDoors;

		internal NumericUpDown nudDays;

		private Label lblIndex;

		private Label label1;

		private Label label2;

		private Label label3;

		private TextBox txtPersons;

		private TextBox txtPersonsOutSide;

		private GroupBox groupBox2;

		internal NumericUpDown nudCycleSecs;

		private Label label4;

		private CheckBox chkAutoRefresh;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private DataGridView dgvEnterIn;

		private DataGridView dgvOutSide;

		private ProgressBar progressBar1;

		private BackgroundWorker backgroundWorker1;

		private Button btnQuery2;

		private System.Windows.Forms.Timer timer1;

		private Label label5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;

		private DataTable dt;

		private DataTable dtReader;

		private DataView dv;

		private DataView dvSelected;

		private DataView dvIn;

		private DataView dvOut;

		private dfrmFind dfrmFind1;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private DataView dvDoors;

		private DataView dvDoors4Watching;

		private DataView dvReader;

		private int[] arrAddr;

		private int[] arrAddrOut;

		private int[] arrAddrDoorID;

		private string[] arrAddrDoorName;

		private CheckedListBox listViewNotDisplay = new CheckedListBox();

		private string strSqlReaders;

		private string strSqlDoorID;

		private DataTable dtUsers;

		private DateTime tmStop;

		private DateTime NextRefreshTime;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPersonsInside));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.toolStrip1 = new ToolStrip();
			this.btnQuery = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.btnSelectNone = new Button();
			this.btnSelectAll = new Button();
			this.cboZone = new ComboBox();
			this.label25 = new Label();
			this.chkListDoors = new CheckedListBox();
			this.nudDays = new NumericUpDown();
			this.lblIndex = new Label();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.txtPersons = new TextBox();
			this.txtPersonsOutSide = new TextBox();
			this.groupBox2 = new GroupBox();
			this.chkAutoRefresh = new CheckBox();
			this.nudCycleSecs = new NumericUpDown();
			this.label4 = new Label();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.dgvEnterIn = new DataGridView();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.tabPage2 = new TabPage();
			this.dgvOutSide = new DataGridView();
			this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
			this.progressBar1 = new ProgressBar();
			this.backgroundWorker1 = new BackgroundWorker();
			this.btnQuery2 = new Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label5 = new Label();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.nudDays).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.nudCycleSecs).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.dgvEnterIn).BeginInit();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.dgvOutSide).BeginInit();
			base.SuspendLayout();
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnQuery,
				this.btnPrint,
				this.btnExportToExcel
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnQuery.ForeColor = Color.White;
			this.btnQuery.Image = Resources.pTools_Query;
			componentResourceManager.ApplyResources(this.btnQuery, "btnQuery");
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			this.btnSelectNone.BackColor = Color.Transparent;
			this.btnSelectNone.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSelectNone, "btnSelectNone");
			this.btnSelectNone.ForeColor = Color.White;
			this.btnSelectNone.Name = "btnSelectNone";
			this.btnSelectNone.UseVisualStyleBackColor = false;
			this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
			this.btnSelectAll.BackColor = Color.Transparent;
			this.btnSelectAll.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSelectAll, "btnSelectAll");
			this.btnSelectAll.ForeColor = Color.White;
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.UseVisualStyleBackColor = false;
			this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
			this.cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboZone.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboZone, "cboZone");
			this.cboZone.Name = "cboZone";
			this.cboZone.SelectedIndexChanged += new EventHandler(this.cbof_Zone_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label25, "label25");
			this.label25.BackColor = Color.Transparent;
			this.label25.ForeColor = Color.White;
			this.label25.Name = "label25";
			this.chkListDoors.CheckOnClick = true;
			this.chkListDoors.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.chkListDoors, "chkListDoors");
			this.chkListDoors.MultiColumn = true;
			this.chkListDoors.Name = "chkListDoors";
			this.chkListDoors.KeyDown += new KeyEventHandler(this.chkListDoors_KeyDown);
			this.nudDays.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudDays, "nudDays");
			NumericUpDown arg_606_0 = this.nudDays;
			int[] array = new int[4];
			array[0] = 6000;
			arg_606_0.Maximum = new decimal(array);
			NumericUpDown arg_625_0 = this.nudDays;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_625_0.Minimum = new decimal(array2);
			this.nudDays.Name = "nudDays";
			NumericUpDown arg_654_0 = this.nudDays;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_654_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this.lblIndex, "lblIndex");
			this.lblIndex.BackColor = Color.Transparent;
			this.lblIndex.ForeColor = Color.White;
			this.lblIndex.Name = "lblIndex";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			this.txtPersons.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.txtPersons, "txtPersons");
			this.txtPersons.Name = "txtPersons";
			this.txtPersons.ReadOnly = true;
			this.txtPersonsOutSide.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.txtPersonsOutSide, "txtPersonsOutSide");
			this.txtPersonsOutSide.Name = "txtPersonsOutSide";
			this.txtPersonsOutSide.ReadOnly = true;
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.chkAutoRefresh);
			this.groupBox2.Controls.Add(this.nudCycleSecs);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.chkAutoRefresh, "chkAutoRefresh");
			this.chkAutoRefresh.Name = "chkAutoRefresh";
			this.chkAutoRefresh.UseVisualStyleBackColor = true;
			this.nudCycleSecs.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudCycleSecs, "nudCycleSecs");
			NumericUpDown arg_8D2_0 = this.nudCycleSecs;
			int[] array4 = new int[4];
			array4[0] = 60000;
			arg_8D2_0.Maximum = new decimal(array4);
			this.nudCycleSecs.Name = "nudCycleSecs";
			NumericUpDown arg_902_0 = this.nudCycleSecs;
			int[] array5 = new int[4];
			array5[0] = 10;
			arg_902_0.Value = new decimal(array5);
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.dgvEnterIn);
			this.tabPage1.ForeColor = Color.White;
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.dgvEnterIn.AllowUserToAddRows = false;
			this.dgvEnterIn.AllowUserToDeleteRows = false;
			this.dgvEnterIn.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvEnterIn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvEnterIn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvEnterIn.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.dataGridViewTextBoxColumn5,
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.dataGridViewCheckBoxColumn1
			});
			componentResourceManager.ApplyResources(this.dgvEnterIn, "dgvEnterIn");
			this.dgvEnterIn.EnableHeadersVisualStyles = false;
			this.dgvEnterIn.Name = "dgvEnterIn";
			this.dgvEnterIn.ReadOnly = true;
			this.dgvEnterIn.RowHeadersVisible = false;
			this.dgvEnterIn.RowTemplate.Height = 23;
			this.dgvEnterIn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvEnterIn.KeyDown += new KeyEventHandler(this.dgvEnterIn_KeyDown);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Controls.Add(this.dgvOutSide);
			this.tabPage2.ForeColor = Color.White;
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.dgvOutSide.AllowUserToAddRows = false;
			this.dgvOutSide.AllowUserToDeleteRows = false;
			this.dgvOutSide.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = Color.White;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvOutSide.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvOutSide.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvOutSide.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn12,
				this.dataGridViewTextBoxColumn13,
				this.dataGridViewTextBoxColumn14,
				this.dataGridViewTextBoxColumn15,
				this.dataGridViewTextBoxColumn16,
				this.dataGridViewCheckBoxColumn2
			});
			componentResourceManager.ApplyResources(this.dgvOutSide, "dgvOutSide");
			this.dgvOutSide.EnableHeadersVisualStyles = false;
			this.dgvOutSide.Name = "dgvOutSide";
			this.dgvOutSide.ReadOnly = true;
			this.dgvOutSide.RowHeadersVisible = false;
			this.dgvOutSide.RowTemplate.Height = 23;
			this.dgvOutSide.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvOutSide.KeyDown += new KeyEventHandler(this.dgvOutSide_KeyDown);
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.ReadOnly = true;
			this.dataGridViewTextBoxColumn14.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			this.dataGridViewTextBoxColumn14.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			this.dataGridViewTextBoxColumn15.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
			this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
			this.dataGridViewTextBoxColumn16.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn2, "dataGridViewCheckBoxColumn2");
			this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			this.dataGridViewCheckBoxColumn2.ReadOnly = true;
			this.dataGridViewCheckBoxColumn2.Resizable = DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Name = "progressBar1";
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.btnQuery2.BackColor = Color.Transparent;
			this.btnQuery2.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnQuery2, "btnQuery2");
			this.btnQuery2.ForeColor = Color.White;
			this.btnQuery2.Name = "btnQuery2";
			this.btnQuery2.UseVisualStyleBackColor = false;
			this.btnQuery2.Click += new EventHandler(this.btnQuery_Click);
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.ForeColor = Color.Yellow;
			this.label5.Name = "label5";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.label5);
			base.Controls.Add(this.btnQuery2);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.txtPersonsOutSide);
			base.Controls.Add(this.txtPersons);
			base.Controls.Add(this.nudDays);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lblIndex);
			base.Controls.Add(this.btnSelectNone);
			base.Controls.Add(this.btnSelectAll);
			base.Controls.Add(this.cboZone);
			base.Controls.Add(this.label25);
			base.Controls.Add(this.chkListDoors);
			base.Controls.Add(this.toolStrip1);
			base.Name = "dfrmPersonsInside";
			base.FormClosing += new FormClosingEventHandler(this.dfrmPersonsInside_FormClosing);
			base.Load += new EventHandler(this.dfrmPersonsInside_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmPersonsInside_KeyDown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.nudDays).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.nudCycleSecs).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((ISupportInitialize)this.dgvEnterIn).EndInit();
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)this.dgvOutSide).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmPersonsInside()
		{
			this.InitializeComponent();
		}

		private void dfrmPersonsInside_Load(object sender, EventArgs e)
		{
			this.loadZoneInfo();
			this.tabPage1.BackColor = this.BackColor;
			this.tabPage2.BackColor = this.BackColor;
			this.loadDoorData();
			this.dgvEnterIn.AutoGenerateColumns = false;
			this.dgvOutSide.AutoGenerateColumns = false;
			this.dgvEnterIn.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvOutSide.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.NextRefreshTime = DateTime.Now.AddSeconds((double)this.nudCycleSecs.Value);
			this.dataGridViewTextBoxColumn5.HeaderText = wgAppConfig.ReplaceFloorRomm(this.dataGridViewTextBoxColumn5.HeaderText);
			this.dataGridViewTextBoxColumn14.HeaderText = wgAppConfig.ReplaceFloorRomm(this.dataGridViewTextBoxColumn14.HeaderText);
			this.dataGridViewTextBoxColumn3.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn3.HeaderText);
			this.dataGridViewTextBoxColumn12.HeaderText = wgAppConfig.ReplaceWorkNO(this.dataGridViewTextBoxColumn12.HeaderText);
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cboZone.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrZoneName[i].ToString()))
				{
					this.cboZone.Items.Add(CommonStr.strAllZones);
				}
				else
				{
					this.cboZone.Items.Add(this.arrZoneName[i].ToString());
				}
			}
			if (this.cboZone.Items.Count > 0)
			{
				this.cboZone.SelectedIndex = 0;
			}
			bool visible = true;
			this.label25.Visible = visible;
			this.cboZone.Visible = visible;
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , 0 as f_Selected, b.f_ZoneID, a.f_ControllerID, a.f_DoorNO,b.f_ControllerSN ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			this.dt = new DataTable();
			this.dvDoors = new DataView(this.dt);
			this.dvDoors4Watching = new DataView(this.dt);
			this.dvSelected = new DataView(this.dt);
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
					goto IL_105;
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
			IL_105:
			text = " SELECT c.*,b.f_ControllerSN ,  b.f_ZoneID ";
			text += " FROM t_b_Controller b, t_b_reader c WHERE c.f_ControllerID = b.f_ControllerID ";
			text += " ORDER BY  c.f_ReaderID ";
			this.dtReader = new DataTable();
			this.dvReader = new DataView(this.dtReader);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
					{
						using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(oleDbCommand2))
						{
							oleDbDataAdapter2.Fill(this.dtReader);
						}
					}
					goto IL_1F4;
				}
			}
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
				{
					using (SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2))
					{
						sqlDataAdapter2.Fill(this.dtReader);
					}
				}
			}
			IL_1F4:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			if (this.dvDoors.Count > 0)
			{
				this.arrAddr = new int[this.dvDoors.Count + 1];
				this.arrAddrOut = new int[this.dvDoors.Count + 1];
				this.arrAddrDoorID = new int[this.dvDoors.Count + 1];
				this.arrAddrDoorName = new string[this.dvDoors.Count + 1];
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					string item = wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]);
					this.chkListDoors.Items.Add(item);
					this.arrAddrDoorID[i] = (int)this.dvDoors[i]["f_DoorID"];
					this.arrAddrDoorName[i] = (string)this.dvDoors[i]["f_DoorName"];
					this.arrAddr[i] = 0;
					this.arrAddrOut[i] = 0;
					if (wgMjController.GetControllerType((int)this.dvDoors[i]["f_ControllerSN"]) == 4)
					{
						this.dvReader.RowFilter = "f_ControllerSN = " + this.dvDoors[i]["f_ControllerSN"].ToString() + " AND f_ReaderNO=" + this.dvDoors[i]["f_DoorNO"].ToString();
						if (this.dvReader.Count > 0)
						{
							this.arrAddr[i] = (int)this.dvReader[0]["f_ReaderID"];
						}
					}
					else
					{
						this.dvReader.RowFilter = string.Concat(new string[]
						{
							"f_ControllerSN = ",
							this.dvDoors[i]["f_ControllerSN"].ToString(),
							" AND (f_ReaderNO=",
							((int)(((byte)this.dvDoors[i]["f_DoorNO"] - 1) * 2 + 1)).ToString(),
							" OR f_ReaderNO=",
							((int)(((byte)this.dvDoors[i]["f_DoorNO"] - 1) * 2 + 2)).ToString(),
							" )"
						});
						this.dvReader.Sort = "f_ReaderNO ASC";
						if (this.dvReader.Count > 0)
						{
							this.arrAddr[i] = (int)this.dvReader[0]["f_ReaderID"];
							if (this.dvReader.Count > 1)
							{
								this.arrAddrOut[i] = (int)this.dvReader[1]["f_ReaderID"];
							}
						}
					}
				}
			}
		}

		private void cbof_Zone_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dvDoors != null)
			{
				this.chkListDoors.Items.Clear();
				this.dv = this.dvDoors;
				if (this.cboZone.SelectedIndex < 0 || (this.cboZone.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
				{
					this.dv.RowFilter = "";
				}
				else
				{
					this.dv.RowFilter = "f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
					string arg = " f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
					int num = (int)this.arrZoneID[this.cboZone.SelectedIndex];
					int num2 = (int)this.arrZoneNO[this.cboZone.SelectedIndex];
					int zoneChildMaxNo = icControllerZone.getZoneChildMaxNo(this.cboZone.Text, this.arrZoneName, this.arrZoneNO);
					if (num2 > 0)
					{
						if (num2 >= zoneChildMaxNo)
						{
							this.dv.RowFilter = string.Format(" f_ZoneID ={0:d} ", num);
							arg = string.Format(" f_ZoneID ={0:d} ", num);
						}
						else
						{
							this.dv.RowFilter = "";
							string text = "";
							for (int i = 0; i < this.arrZoneNO.Count; i++)
							{
								if ((int)this.arrZoneNO[i] <= zoneChildMaxNo && (int)this.arrZoneNO[i] >= num2)
								{
									if (text == "")
									{
										text += string.Format(" f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
									}
									else
									{
										text += string.Format(" OR f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
									}
								}
							}
							this.dv.RowFilter = string.Format("  {0} ", text);
							arg = string.Format("  {0} ", text);
						}
					}
					this.dv.RowFilter = string.Format(" {0} ", arg);
				}
				this.chkListDoors.Items.Clear();
				if (this.dvDoors.Count > 0)
				{
					for (int j = 0; j < this.dvDoors.Count; j++)
					{
						this.chkListDoors.Items.Add(wgTools.SetObjToStr(this.dvDoors[j]["f_DoorName"]));
						this.arrAddrDoorID[j] = (int)this.dvDoors[j]["f_DoorID"];
						this.arrAddrDoorName[j] = (string)this.dvDoors[j]["f_DoorName"];
						this.arrAddr[j] = 0;
						this.arrAddrOut[j] = 0;
						if (wgMjController.GetControllerType((int)this.dvDoors[j]["f_ControllerSN"]) == 4)
						{
							this.dvReader.RowFilter = "f_ControllerSN = " + this.dvDoors[j]["f_ControllerSN"].ToString() + " AND f_ReaderNO=" + this.dvDoors[j]["f_DoorNO"].ToString();
							if (this.dvReader.Count > 0)
							{
								this.arrAddr[j] = (int)this.dvReader[0]["f_ReaderID"];
							}
						}
						else
						{
							this.dvReader.RowFilter = string.Concat(new string[]
							{
								"f_ControllerSN = ",
								this.dvDoors[j]["f_ControllerSN"].ToString(),
								" AND (f_ReaderNO=",
								((int)(((byte)this.dvDoors[j]["f_DoorNO"] - 1) * 2 + 1)).ToString(),
								" OR f_ReaderNO=",
								((int)(((byte)this.dvDoors[j]["f_DoorNO"] - 1) * 2 + 2)).ToString(),
								" )"
							});
							if (this.dvReader.Count > 0)
							{
								this.arrAddr[j] = (int)this.dvReader[0]["f_ReaderID"];
								if (this.dvReader.Count > 1)
								{
									this.arrAddrOut[j] = (int)this.dvReader[1]["f_ReaderID"];
								}
							}
						}
					}
					return;
				}
			}
			else
			{
				this.chkListDoors.Items.Clear();
			}
		}

		public string getStrSql()
		{
			string text = "";
			this.strSqlDoorID = "";
			if (this.chkListDoors.CheckedItems.Count != 0)
			{
				for (int i = 0; i < this.chkListDoors.Items.Count; i++)
				{
					if (this.chkListDoors.GetItemChecked(i))
					{
						if (text == "")
						{
							this.strSqlDoorID += this.arrAddrDoorID[i].ToString();
							text += this.arrAddr[i].ToString();
						}
						else
						{
							this.strSqlDoorID = this.strSqlDoorID + "," + this.arrAddrDoorID[i].ToString();
							text = text + "," + this.arrAddr[i].ToString();
						}
						if (this.arrAddrOut[i] != 0)
						{
							text = text + "," + this.arrAddrOut[i].ToString();
						}
					}
				}
			}
			return text;
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			this.strSqlReaders = this.getStrSql();
			if (string.IsNullOrEmpty(this.strSqlReaders))
			{
				XMessageBox.Show(CommonStr.strSelectDoor4Query);
				return;
			}
			this.tmStop = DateTime.Now.Date.AddDays(-(double)this.nudDays.Value).Date;
			if (!this.backgroundWorker1.IsBusy)
			{
				this.btnQuery2.Enabled = false;
				this.timer1.Enabled = false;
				Cursor.Current = Cursors.WaitCursor;
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			if (this.chkListDoors.Items.Count > 0)
			{
				for (int i = 0; i < this.chkListDoors.Items.Count; i++)
				{
					this.chkListDoors.SetItemChecked(i, true);
				}
			}
		}

		private void btnSelectNone_Click(object sender, EventArgs e)
		{
			if (this.chkListDoors.Items.Count > 0)
			{
				for (int i = 0; i < this.chkListDoors.Items.Count; i++)
				{
					this.chkListDoors.SetItemChecked(i, false);
				}
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(wgAppConfig.CultureInfoStr, false);
			e.Result = this.dealPersonInside();
			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private int dealPersonInside()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.dealPersonInside_Acc();
			}
			int result = -1;
			try
			{
				string text = " SELECT  f_ConsumerNO, f_ConsumerName, f_GroupName,  '' as  f_ReadDate, '' as f_DoorName, 0 as f_bHave ";
				text += " , t_b_Consumer.f_GroupID, t_b_Consumer.f_ConsumerID  ";
				text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON ( t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ) ";
				text = text + " WHERE f_ConsumerID IN (SELECT DISTINCT f_ConsumerID FROM t_d_Privilege WHERE f_DoorID IN (" + this.strSqlDoorID + "))";
				this.dtUsers = new DataTable();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							sqlDataAdapter.Fill(this.dtUsers);
						}
					}
				}
				if (this.dtUsers.Rows.Count <= 0)
				{
					return 0;
				}
				text = " SELECT * FROM t_d_SwipeRecord WHERE 1>0 AND ";
				text = text + " f_ReaderID IN (" + this.strSqlReaders + ") ";
				text += " AND NOT ( f_ConsumerID IS NULL) ";
				text += " AND f_Character >0  ";
				text += " ORDER BY f_ReadDate DESC ";
				using (DataView dataView = new DataView(this.dtUsers))
				{
					using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
						{
							sqlCommand2.CommandTimeout = wgAppConfig.dbCommandTimeout;
							sqlConnection2.Open();
							SqlDataReader sqlDataReader = sqlCommand2.ExecuteReader();
							while (sqlDataReader.Read())
							{
								if (!((DateTime)sqlDataReader["f_ReadDate"] > DateTime.Now.AddDays(2.0)))
								{
									if (((DateTime)sqlDataReader["f_ReadDate"]).Date <= this.tmStop.Date)
									{
										break;
									}
									dataView.RowFilter = "f_ConsumerID = " + sqlDataReader["f_ConsumerID"];
									if (dataView.Count > 0 && (int)dataView[0]["f_bHave"] == 0)
									{
										dataView[0]["f_bHave"] = 1;
										if ((byte)sqlDataReader["f_InOut"] == 1)
										{
											dataView[0]["f_bHave"] = 2;
											dataView[0]["f_ReadDate"] = ((DateTime)sqlDataReader["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek);
											for (int i = 0; i < this.arrAddr.Length; i++)
											{
												if (this.arrAddr[i] == (int)sqlDataReader["f_ReaderID"])
												{
													dataView[0]["f_DoorName"] = this.arrAddrDoorName[i];
													break;
												}
											}
										}
										else
										{
											dataView[0]["f_ReadDate"] = ((DateTime)sqlDataReader["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek);
											for (int j = 0; j < this.arrAddr.Length; j++)
											{
												if (this.arrAddrOut[j] == (int)sqlDataReader["f_ReaderID"])
												{
													dataView[0]["f_DoorName"] = this.arrAddrDoorName[j];
													break;
												}
											}
										}
									}
								}
							}
							sqlDataReader.Close();
						}
					}
				}
				result = this.dtUsers.Rows.Count;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		private int dealPersonInside_Acc()
		{
			int result = -1;
			try
			{
				string text = " SELECT  f_ConsumerNO, f_ConsumerName, f_GroupName,  '' as  f_ReadDate, '' as f_DoorName, 0 as f_bHave ";
				text += " , t_b_Consumer.f_GroupID, t_b_Consumer.f_ConsumerID  ";
				text += " FROM t_b_Consumer LEFT OUTER JOIN t_b_Group ON ( t_b_Consumer.f_GroupID = t_b_Group.f_GroupID ) ";
				text = text + " WHERE f_ConsumerID IN (SELECT DISTINCT f_ConsumerID FROM t_d_Privilege WHERE f_DoorID IN (" + this.strSqlDoorID + "))";
				this.dtUsers = new DataTable();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							oleDbDataAdapter.Fill(this.dtUsers);
						}
					}
				}
				if (this.dtUsers.Rows.Count <= 0)
				{
					return 0;
				}
				text = " SELECT * FROM t_d_SwipeRecord WHERE 1>0 AND ";
				text = text + " f_ReaderID IN (" + this.strSqlReaders + ") ";
				text += " AND NOT ( f_ConsumerID IS NULL) ";
				text += " AND f_Character >0  ";
				text += " ORDER BY f_ReadDate DESC ";
				using (DataView dataView = new DataView(this.dtUsers))
				{
					using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
						{
							oleDbCommand2.CommandTimeout = wgAppConfig.dbCommandTimeout;
							oleDbConnection2.Open();
							OleDbDataReader oleDbDataReader = oleDbCommand2.ExecuteReader();
							while (oleDbDataReader.Read())
							{
								if (!((DateTime)oleDbDataReader["f_ReadDate"] > DateTime.Now.AddDays(2.0)))
								{
									if (((DateTime)oleDbDataReader["f_ReadDate"]).Date <= this.tmStop.Date)
									{
										break;
									}
									dataView.RowFilter = "f_ConsumerID = " + oleDbDataReader["f_ConsumerID"];
									if (dataView.Count > 0 && (int)dataView[0]["f_bHave"] == 0)
									{
										dataView[0]["f_bHave"] = 1;
										if ((byte)oleDbDataReader["f_InOut"] == 1)
										{
											dataView[0]["f_bHave"] = 2;
											dataView[0]["f_ReadDate"] = ((DateTime)oleDbDataReader["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek);
											for (int i = 0; i < this.arrAddr.Length; i++)
											{
												if (this.arrAddr[i] == (int)oleDbDataReader["f_ReaderID"])
												{
													dataView[0]["f_DoorName"] = this.arrAddrDoorName[i];
													break;
												}
											}
										}
										else
										{
											dataView[0]["f_ReadDate"] = ((DateTime)oleDbDataReader["f_ReadDate"]).ToString(wgTools.DisplayFormat_DateYMDHMSWeek);
											for (int j = 0; j < this.arrAddr.Length; j++)
											{
												if (this.arrAddrOut[j] == (int)oleDbDataReader["f_ReaderID"])
												{
													dataView[0]["f_DoorName"] = this.arrAddrDoorName[j];
													break;
												}
											}
										}
									}
								}
							}
							oleDbDataReader.Close();
						}
					}
				}
				result = this.dtUsers.Rows.Count;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				wgTools.WgDebugWrite("Users Operation was canceled", new object[0]);
			}
			else if (e.Error != null)
			{
				string info = string.Format("An error occurred: {0}", e.Error.Message);
				wgTools.WgDebugWrite(info, new object[0]);
			}
			else
			{
				int num = (int)e.Result;
				if (num == 0)
				{
					this.dgvEnterIn.DataSource = null;
					this.dgvOutSide.DataSource = null;
					this.txtPersons.Text = "0";
					this.txtPersonsOutSide.Text = "0";
					XMessageBox.Show(CommonStr.strNoAccessPrivilege4SelectedDoors);
				}
				else if (num > 0)
				{
					this.dvIn = new DataView(this.dtUsers);
					this.dvOut = new DataView(this.dtUsers);
					this.dvIn.RowFilter = "f_bHave =2";
					this.dvOut.RowFilter = "f_bHave < 2";
					this.dgvEnterIn.DataSource = this.dvIn;
					this.dgvOutSide.DataSource = this.dvOut;
					int num2 = 0;
					while (num2 < this.dvIn.Table.Columns.Count && num2 < this.dgvEnterIn.ColumnCount)
					{
						this.dgvEnterIn.Columns[num2].DataPropertyName = this.dvIn.Table.Columns[num2].ColumnName;
						num2++;
					}
					int num3 = 0;
					while (num3 < this.dvOut.Table.Columns.Count && num3 < this.dgvOutSide.ColumnCount)
					{
						this.dgvOutSide.Columns[num3].DataPropertyName = this.dvOut.Table.Columns[num3].ColumnName;
						num3++;
					}
					if (this.dvIn.Count == 0 && this.dvOut.Count > 0)
					{
						this.tabControl1.SelectedTab = this.tabPage2;
					}
					if (this.dvIn.Count > 0 && this.dvOut.Count == 0)
					{
						this.tabControl1.SelectedTab = this.tabPage1;
					}
					this.txtPersons.Text = this.dvIn.Count.ToString();
					this.txtPersonsOutSide.Text = this.dvOut.Count.ToString();
				}
				this.btnQuery2.Enabled = true;
				this.timer1.Enabled = true;
				this.NextRefreshTime = DateTime.Now.AddSeconds((double)this.nudCycleSecs.Value);
			}
			Cursor.Current = Cursors.Default;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.chkAutoRefresh.Checked && this.btnQuery2.Enabled && DateTime.Now > this.NextRefreshTime)
			{
				this.NextRefreshTime = DateTime.Now.AddSeconds((double)this.nudCycleSecs.Value);
				this.btnQuery2.PerformClick();
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedTab == this.tabPage1)
			{
				wgAppConfig.printdgv(this.dgvEnterIn, this.tabPage1.Text);
			}
			if (this.tabControl1.SelectedTab == this.tabPage2)
			{
				wgAppConfig.printdgv(this.dgvOutSide, this.tabPage2.Text);
			}
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedTab == this.tabPage1)
			{
				wgAppConfig.exportToExcel(this.dgvEnterIn, this.tabPage1.Text);
			}
			if (this.tabControl1.SelectedTab == this.tabPage2)
			{
				wgAppConfig.exportToExcel(this.dgvOutSide, this.tabPage2.Text);
			}
		}

		private void dfrmPersonsInside_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void dfrmPersonsInside_KeyDown(object sender, KeyEventArgs e)
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

		private void dgvEnterIn_KeyDown(object sender, KeyEventArgs e)
		{
			this.dfrmPersonsInside_KeyDown(this.dgvEnterIn, e);
		}

		private void dgvOutSide_KeyDown(object sender, KeyEventArgs e)
		{
			this.dfrmPersonsInside_KeyDown(this.dgvOutSide, e);
		}

		private void chkListDoors_KeyDown(object sender, KeyEventArgs e)
		{
			this.dfrmPersonsInside_KeyDown(this.chkListDoors, e);
		}
	}
}
