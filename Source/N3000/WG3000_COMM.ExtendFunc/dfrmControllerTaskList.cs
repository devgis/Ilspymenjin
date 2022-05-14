using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmControllerTaskList : frmN3000
	{
		private IContainer components;

		private GroupBox groupBox2;

		private CheckBox checkBox49;

		private CheckBox checkBox48;

		private CheckBox checkBox47;

		private CheckBox checkBox46;

		private CheckBox checkBox45;

		private CheckBox checkBox44;

		private CheckBox checkBox43;

		private Label label45;

		private DateTimePicker dtpTime;

		private Label label43;

		private Label label44;

		private DateTimePicker dtpEnd;

		private DateTimePicker dtpBegin;

		private ComboBox cboDoors;

		private Label label1;

		private ComboBox cboAccessMethod;

		private Label label2;

		private Button btnAdd;

		private DataGridView dgvTaskList;

		private Button btnDel;

		private Button btnClose;

		private Label label3;

		private TextBox textBox1;

		private DataGridViewTextBoxColumn f_ID;

		private DataGridViewTextBoxColumn f_From;

		private DataGridViewTextBoxColumn f_To;

		private DataGridViewTextBoxColumn f_OperateHMS1A;

		private DataGridViewCheckBoxColumn f_Monday;

		private DataGridViewCheckBoxColumn f_Tuesday;

		private DataGridViewCheckBoxColumn f_Wednesday;

		private DataGridViewCheckBoxColumn f_Thursday;

		private DataGridViewCheckBoxColumn f_Friday;

		private DataGridViewCheckBoxColumn f_Saturday;

		private DataGridViewCheckBoxColumn f_Sunday;

		private DataGridViewTextBoxColumn f_AdaptTo;

		private DataGridViewTextBoxColumn f_DoorControlDesc;

		private DataGridViewTextBoxColumn f_Note;

		private DataGridViewTextBoxColumn f_DoorControl;

		private DataGridViewTextBoxColumn f_DoorID;

		private Button btnEdit;

		private DataTable dt;

		private DataView dv;

		private DataTable dtDoors;

		private DataView dvDoors;

		private ArrayList arrDoorID = new ArrayList();

		private dfrmFind dfrmFind1 = new dfrmFind();

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dfrmFind1 != null)
			{
				this.dfrmFind1.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerTaskList));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.textBox1 = new TextBox();
			this.label3 = new Label();
			this.btnClose = new Button();
			this.btnDel = new Button();
			this.dgvTaskList = new DataGridView();
			this.f_ID = new DataGridViewTextBoxColumn();
			this.f_From = new DataGridViewTextBoxColumn();
			this.f_To = new DataGridViewTextBoxColumn();
			this.f_OperateHMS1A = new DataGridViewTextBoxColumn();
			this.f_Monday = new DataGridViewCheckBoxColumn();
			this.f_Tuesday = new DataGridViewCheckBoxColumn();
			this.f_Wednesday = new DataGridViewCheckBoxColumn();
			this.f_Thursday = new DataGridViewCheckBoxColumn();
			this.f_Friday = new DataGridViewCheckBoxColumn();
			this.f_Saturday = new DataGridViewCheckBoxColumn();
			this.f_Sunday = new DataGridViewCheckBoxColumn();
			this.f_AdaptTo = new DataGridViewTextBoxColumn();
			this.f_DoorControlDesc = new DataGridViewTextBoxColumn();
			this.f_Note = new DataGridViewTextBoxColumn();
			this.f_DoorControl = new DataGridViewTextBoxColumn();
			this.f_DoorID = new DataGridViewTextBoxColumn();
			this.btnAdd = new Button();
			this.label2 = new Label();
			this.cboAccessMethod = new ComboBox();
			this.label1 = new Label();
			this.cboDoors = new ComboBox();
			this.groupBox2 = new GroupBox();
			this.checkBox49 = new CheckBox();
			this.checkBox48 = new CheckBox();
			this.checkBox47 = new CheckBox();
			this.checkBox46 = new CheckBox();
			this.checkBox45 = new CheckBox();
			this.checkBox44 = new CheckBox();
			this.checkBox43 = new CheckBox();
			this.label45 = new Label();
			this.dtpTime = new DateTimePicker();
			this.label43 = new Label();
			this.label44 = new Label();
			this.dtpEnd = new DateTimePicker();
			this.dtpBegin = new DateTimePicker();
			this.btnEdit = new Button();
			((ISupportInitialize)this.dgvTaskList).BeginInit();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.btnDel, "btnDel");
			this.btnDel.BackColor = Color.Transparent;
			this.btnDel.BackgroundImage = Resources.pMain_button_normal;
			this.btnDel.ForeColor = Color.White;
			this.btnDel.Name = "btnDel";
			this.btnDel.UseVisualStyleBackColor = false;
			this.btnDel.Click += new EventHandler(this.btnDel_Click);
			componentResourceManager.ApplyResources(this.dgvTaskList, "dgvTaskList");
			this.dgvTaskList.AllowUserToAddRows = false;
			this.dgvTaskList.AllowUserToDeleteRows = false;
			this.dgvTaskList.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvTaskList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvTaskList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvTaskList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ID,
				this.f_From,
				this.f_To,
				this.f_OperateHMS1A,
				this.f_Monday,
				this.f_Tuesday,
				this.f_Wednesday,
				this.f_Thursday,
				this.f_Friday,
				this.f_Saturday,
				this.f_Sunday,
				this.f_AdaptTo,
				this.f_DoorControlDesc,
				this.f_Note,
				this.f_DoorControl,
				this.f_DoorID
			});
			this.dgvTaskList.EnableHeadersVisualStyles = false;
			this.dgvTaskList.Name = "dgvTaskList";
			this.dgvTaskList.ReadOnly = true;
			this.dgvTaskList.RowTemplate.Height = 23;
			this.dgvTaskList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvTaskList.DoubleClick += new EventHandler(this.dgvTaskList_DoubleClick);
			componentResourceManager.ApplyResources(this.f_ID, "f_ID");
			this.f_ID.Name = "f_ID";
			this.f_ID.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_From.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_From, "f_From");
			this.f_From.Name = "f_From";
			this.f_From.ReadOnly = true;
			this.f_From.Resizable = DataGridViewTriState.True;
			this.f_From.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_To, "f_To");
			this.f_To.Name = "f_To";
			this.f_To.ReadOnly = true;
			this.f_To.Resizable = DataGridViewTriState.True;
			this.f_To.SortMode = DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_OperateHMS1A.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_OperateHMS1A, "f_OperateHMS1A");
			this.f_OperateHMS1A.Name = "f_OperateHMS1A";
			this.f_OperateHMS1A.ReadOnly = true;
			this.f_OperateHMS1A.Resizable = DataGridViewTriState.True;
			this.f_OperateHMS1A.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_Monday, "f_Monday");
			this.f_Monday.Name = "f_Monday";
			this.f_Monday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Tuesday, "f_Tuesday");
			this.f_Tuesday.Name = "f_Tuesday";
			this.f_Tuesday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Wednesday, "f_Wednesday");
			this.f_Wednesday.Name = "f_Wednesday";
			this.f_Wednesday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Thursday, "f_Thursday");
			this.f_Thursday.Name = "f_Thursday";
			this.f_Thursday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Friday, "f_Friday");
			this.f_Friday.Name = "f_Friday";
			this.f_Friday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Saturday, "f_Saturday");
			this.f_Saturday.Name = "f_Saturday";
			this.f_Saturday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Sunday, "f_Sunday");
			this.f_Sunday.Name = "f_Sunday";
			this.f_Sunday.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_AdaptTo, "f_AdaptTo");
			this.f_AdaptTo.Name = "f_AdaptTo";
			this.f_AdaptTo.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DoorControlDesc, "f_DoorControlDesc");
			this.f_DoorControlDesc.Name = "f_DoorControlDesc";
			this.f_DoorControlDesc.ReadOnly = true;
			this.f_Note.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Note, "f_Note");
			this.f_Note.Name = "f_Note";
			this.f_Note.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DoorControl, "f_DoorControl");
			this.f_DoorControl.Name = "f_DoorControl";
			this.f_DoorControl.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_DoorID, "f_DoorID");
			this.f_DoorID.Name = "f_DoorID";
			this.f_DoorID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.BackColor = Color.Transparent;
			this.btnAdd.BackgroundImage = Resources.pMain_button_normal;
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = false;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.cboAccessMethod, "cboAccessMethod");
			this.cboAccessMethod.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboAccessMethod.FormattingEnabled = true;
			this.cboAccessMethod.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboAccessMethod.Items"),
				componentResourceManager.GetString("cboAccessMethod.Items1"),
				componentResourceManager.GetString("cboAccessMethod.Items2"),
				componentResourceManager.GetString("cboAccessMethod.Items3"),
				componentResourceManager.GetString("cboAccessMethod.Items4"),
				componentResourceManager.GetString("cboAccessMethod.Items5"),
				componentResourceManager.GetString("cboAccessMethod.Items6"),
				componentResourceManager.GetString("cboAccessMethod.Items7"),
				componentResourceManager.GetString("cboAccessMethod.Items8"),
				componentResourceManager.GetString("cboAccessMethod.Items9"),
				componentResourceManager.GetString("cboAccessMethod.Items10")
			});
			this.cboAccessMethod.Name = "cboAccessMethod";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.cboDoors, "cboDoors");
			this.cboDoors.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboDoors.FormattingEnabled = true;
			this.cboDoors.Name = "cboDoors";
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.checkBox49);
			this.groupBox2.Controls.Add(this.checkBox48);
			this.groupBox2.Controls.Add(this.checkBox47);
			this.groupBox2.Controls.Add(this.checkBox46);
			this.groupBox2.Controls.Add(this.checkBox45);
			this.groupBox2.Controls.Add(this.checkBox44);
			this.groupBox2.Controls.Add(this.checkBox43);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.checkBox49, "checkBox49");
			this.checkBox49.Checked = true;
			this.checkBox49.CheckState = CheckState.Checked;
			this.checkBox49.Name = "checkBox49";
			this.checkBox49.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox48, "checkBox48");
			this.checkBox48.Checked = true;
			this.checkBox48.CheckState = CheckState.Checked;
			this.checkBox48.Name = "checkBox48";
			this.checkBox48.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox47, "checkBox47");
			this.checkBox47.Checked = true;
			this.checkBox47.CheckState = CheckState.Checked;
			this.checkBox47.Name = "checkBox47";
			this.checkBox47.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox46, "checkBox46");
			this.checkBox46.Checked = true;
			this.checkBox46.CheckState = CheckState.Checked;
			this.checkBox46.Name = "checkBox46";
			this.checkBox46.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox45, "checkBox45");
			this.checkBox45.Checked = true;
			this.checkBox45.CheckState = CheckState.Checked;
			this.checkBox45.Name = "checkBox45";
			this.checkBox45.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox44, "checkBox44");
			this.checkBox44.Checked = true;
			this.checkBox44.CheckState = CheckState.Checked;
			this.checkBox44.Name = "checkBox44";
			this.checkBox44.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox43, "checkBox43");
			this.checkBox43.Checked = true;
			this.checkBox43.CheckState = CheckState.Checked;
			this.checkBox43.Name = "checkBox43";
			this.checkBox43.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label45, "label45");
			this.label45.BackColor = Color.Transparent;
			this.label45.ForeColor = Color.White;
			this.label45.Name = "label45";
			componentResourceManager.ApplyResources(this.dtpTime, "dtpTime");
			this.dtpTime.Name = "dtpTime";
			this.dtpTime.ShowUpDown = true;
			this.dtpTime.Value = new DateTime(2011, 11, 30, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label43, "label43");
			this.label43.BackColor = Color.Transparent;
			this.label43.ForeColor = Color.White;
			this.label43.Name = "label43";
			componentResourceManager.ApplyResources(this.label44, "label44");
			this.label44.BackColor = Color.Transparent;
			this.label44.ForeColor = Color.White;
			this.label44.Name = "label44";
			componentResourceManager.ApplyResources(this.dtpEnd, "dtpEnd");
			this.dtpEnd.Name = "dtpEnd";
			this.dtpEnd.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			componentResourceManager.ApplyResources(this.dtpBegin, "dtpBegin");
			this.dtpBegin.Name = "dtpBegin";
			this.dtpBegin.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.BackColor = Color.Transparent;
			this.btnEdit.BackgroundImage = Resources.pMain_button_normal;
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnEdit);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnDel);
			base.Controls.Add(this.dgvTaskList);
			base.Controls.Add(this.btnAdd);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cboAccessMethod);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cboDoors);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.label45);
			base.Controls.Add(this.dtpTime);
			base.Controls.Add(this.label43);
			base.Controls.Add(this.label44);
			base.Controls.Add(this.dtpEnd);
			base.Controls.Add(this.dtpBegin);
			base.Name = "dfrmControllerTaskList";
			base.FormClosing += new FormClosingEventHandler(this.dfrmControllerTaskList_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerTaskList_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControllerTaskList_KeyDown);
			((ISupportInitialize)this.dgvTaskList).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmControllerTaskList()
		{
			this.InitializeComponent();
		}

		private void dfrmControllerTaskList_Load(object sender, EventArgs e)
		{
			this.dtpBegin.Value = DateTime.Now.Date;
			this.dtpTime.CustomFormat = "HH:mm";
			this.dtpTime.Format = DateTimePickerFormat.Custom;
			this.dtpTime.Value = DateTime.Parse("00:00:00");
			this.loadDoorData();
			if (this.cboAccessMethod.Items.Count > 0)
			{
				this.cboAccessMethod.SelectedIndex = 0;
			}
			this.LoadTaskListData();
			wgAppConfig.setDisplayFormatDate(this.dtpBegin, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpEnd, wgTools.DisplayFormat_DateYMDWeek);
			this.loadOperatorPrivilege();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuTaskList";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnDel.Visible = false;
				this.dgvTaskList.ReadOnly = true;
			}
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			this.dtDoors = new DataTable();
			this.dvDoors = new DataView(this.dtDoors);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtDoors);
						}
					}
					goto IL_E3;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtDoors);
					}
				}
			}
			IL_E3:
			int count = this.dtDoors.Rows.Count;
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtDoors);
			this.cboDoors.Items.Clear();
			if (count == this.dtDoors.Rows.Count)
			{
				this.cboDoors.Items.Add(CommonStr.strAll);
				this.arrDoorID.Add(0);
			}
			if (this.dvDoors.Count > 0)
			{
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					this.cboDoors.Items.Add(wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]));
					this.arrDoorID.Add(this.dvDoors[i]["f_DoorID"]);
				}
			}
			if (this.cboDoors.Items.Count > 0)
			{
				this.cboDoors.SelectedIndex = 0;
			}
		}

		private void LoadTaskListData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.LoadTaskListData_Acc();
				return;
			}
			string text = "  SELECT f_Id,  ";
			text += "   f_BeginYMD, ";
			text += "   f_EndYMD,  ";
			text += "  ISNULL(CONVERT(char(5), f_OperateTime,108) , '00:00') AS [f_Time], ";
			text += "  [f_Monday], [f_Tuesday], [f_Wednesday], [f_Thursday], ";
			text += "   [f_Friday], [f_Saturday], [f_Sunday], ";
			text = text + "  CASE WHEN a.f_DoorID=0 THEN  " + wgTools.PrepareStr(CommonStr.strAll) + " ELSE b.f_DoorName END AS f_AdaptTo, ";
			text += " ' ' AS f_DoorControlDesc, ";
			text += " f_Notes, ";
			text += " a.f_DoorID, ";
			text += " a.f_DoorControl ";
			text += ", c.f_ZoneID ";
			text += " FROM t_b_ControllerTaskList a LEFT JOIN t_b_Door b ON a.f_DoorID = b.f_DoorID  LEFT JOIN  t_b_Controller c on b.f_ControllerID = c.f_ControllerID ";
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
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
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			this.dgvTaskList.AutoGenerateColumns = false;
			this.dgvTaskList.DataSource = this.dv;
			int i;
			for (i = 0; i < this.dt.Rows.Count; i++)
			{
				if ((int)this.dt.Rows[i]["f_DoorControl"] < this.cboAccessMethod.Items.Count)
				{
					this.dt.Rows[i]["f_DoorControlDesc"] = this.cboAccessMethod.Items[(int)this.dt.Rows[i]["f_DoorControl"]].ToString();
				}
			}
			i = 0;
			while (i < this.dv.Table.Columns.Count && i < this.dgvTaskList.ColumnCount)
			{
				this.dgvTaskList.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
				this.dgvTaskList.Columns[i].Name = this.dv.Table.Columns[i].ColumnName;
				i++;
			}
			wgAppConfig.setDisplayFormatDate(this.dgvTaskList, "f_BeginYMD", wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dgvTaskList, "f_EndYMD", wgTools.DisplayFormat_DateYMDWeek);
		}

		private void LoadTaskListData_Acc()
		{
			string text = "  SELECT f_Id,  ";
			text += "   f_BeginYMD, ";
			text += "   f_EndYMD,  ";
			text += "  IIF(f_OperateTime IS NULL , '00:00',  Format([f_OperateTime],'Short Time') ) AS [f_Time], ";
			text += "  [f_Monday], [f_Tuesday], [f_Wednesday], [f_Thursday], ";
			text += "   [f_Friday], [f_Saturday], [f_Sunday], ";
			text = text + "  IIF ( a.f_DoorID=0 ,  " + wgTools.PrepareStr(CommonStr.strAll) + " , b.f_DoorName ) AS f_AdaptTo, ";
			text += " ' ' AS f_DoorControlDesc, ";
			text += " f_Notes, ";
			text += " a.f_DoorID, ";
			text += " a.f_DoorControl ";
			text += ", c.f_ZoneID ";
			text += " FROM (( t_b_ControllerTaskList a LEFT JOIN t_b_Door b ON a.f_DoorID = b.f_DoorID ) LEFT JOIN  t_b_Controller c on b.f_ControllerID = c.f_ControllerID ) ";
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						oleDbDataAdapter.Fill(this.dt);
					}
				}
			}
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			this.dgvTaskList.AutoGenerateColumns = false;
			this.dgvTaskList.DataSource = this.dv;
			int i;
			for (i = 0; i < this.dt.Rows.Count; i++)
			{
				if ((int)this.dt.Rows[i]["f_DoorControl"] < this.cboAccessMethod.Items.Count)
				{
					this.dt.Rows[i]["f_DoorControlDesc"] = this.cboAccessMethod.Items[(int)this.dt.Rows[i]["f_DoorControl"]].ToString();
				}
			}
			i = 0;
			while (i < this.dv.Table.Columns.Count && i < this.dgvTaskList.ColumnCount)
			{
				this.dgvTaskList.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
				this.dgvTaskList.Columns[i].Name = this.dv.Table.Columns[i].ColumnName;
				i++;
			}
			wgAppConfig.setDisplayFormatDate(this.dgvTaskList, "f_BeginYMD", wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dgvTaskList, "f_EndYMD", wgTools.DisplayFormat_DateYMDWeek);
		}

		private string getDateString(DateTimePicker dtp)
		{
			if (dtp == null)
			{
				return wgTools.PrepareStr("");
			}
			return wgTools.PrepareStr(dtp.Value.ToString(wgTools.YMDHMSFormat), true, wgTools.YMDHMSFormat);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (this.dtpBegin.Value.Date > this.dtpEnd.Value.Date)
			{
				string strTimeInvalidParm = CommonStr.strTimeInvalidParm;
				XMessageBox.Show(strTimeInvalidParm, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (!this.checkBox43.Checked && !this.checkBox44.Checked && !this.checkBox45.Checked && !this.checkBox46.Checked && !this.checkBox47.Checked && !this.checkBox48.Checked && !this.checkBox49.Checked)
			{
				string strTimeInvalidParm2 = CommonStr.strTimeInvalidParm;
				XMessageBox.Show(strTimeInvalidParm2, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.cboDoors.SelectedIndex < 0)
			{
				return;
			}
			string text = " INSERT INTO t_b_ControllerTaskList(f_BeginYMD,[f_EndYMD],  [f_OperateTime] ,";
			text += "  [f_Monday], [f_Tuesday], [f_Wednesday], [f_Thursday], [f_Friday], [f_Saturday], [f_Sunday], [f_DoorID],";
			text += "  [f_DoorControl], [f_Notes]";
			text += ") ";
			text += " VALUES ( ";
			text += this.getDateString(this.dtpBegin);
			text = text + " , " + this.getDateString(this.dtpEnd);
			text = text + " , " + this.getDateString(this.dtpTime);
			text = text + " , " + (this.checkBox43.Checked ? "1" : "0");
			text = text + " , " + (this.checkBox44.Checked ? "1" : "0");
			text = text + " , " + (this.checkBox45.Checked ? "1" : "0");
			text = text + " , " + (this.checkBox46.Checked ? "1" : "0");
			text = text + " , " + (this.checkBox47.Checked ? "1" : "0");
			text = text + " , " + (this.checkBox48.Checked ? "1" : "0");
			text = text + " , " + (this.checkBox49.Checked ? "1" : "0");
			text = text + " , " + this.arrDoorID[this.cboDoors.SelectedIndex];
			if (this.cboAccessMethod.SelectedIndex < 0)
			{
				this.cboAccessMethod.SelectedIndex = 0;
			}
			text = text + " , " + this.cboAccessMethod.SelectedIndex;
			text = text + " , " + wgTools.PrepareStr(this.textBox1.Text.Trim());
			text += " )";
			int num = wgAppConfig.runUpdateSql(text);
			if (num > 0)
			{
				wgAppConfig.wgLog(string.Format("{0} {1} [{2}]", this.Text, this.btnAdd.Text, text));
				this.LoadTaskListData();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.dgvTaskList.Rows.Count > 0)
			{
				num = this.dgvTaskList.CurrentCell.RowIndex;
			}
			int index;
			if (this.dgvTaskList.SelectedRows.Count <= 0)
			{
				if (this.dgvTaskList.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvTaskList.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvTaskList.SelectedRows[0].Index;
			}
			string text = "";
			if (this.dgvTaskList.SelectedRows.Count > 1)
			{
				int num2 = 0;
				for (int i = 0; i < this.dgvTaskList.SelectedRows.Count; i++)
				{
					int num3 = 2147483647;
					for (int j = 0; j < this.dgvTaskList.SelectedRows.Count; j++)
					{
						index = this.dgvTaskList.SelectedRows[j].Index;
						int num4 = int.Parse(this.dgvTaskList.Rows[index].Cells[0].Value.ToString());
						if (num4 > num2 && num4 < num3)
						{
							num3 = num4;
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += num3.ToString();
					num2 = num3;
				}
			}
			string text2;
			if (this.dgvTaskList.SelectedRows.Count <= 1)
			{
				text2 = string.Format("{0}\r\n{1}:  {2}", this.btnDel.Text, this.dgvTaskList.Columns[0].HeaderText, this.dgvTaskList.Rows[index].Cells[0].Value.ToString());
				text2 = string.Format(CommonStr.strAreYouSure + " {0} ?", text2);
			}
			else
			{
				text2 = string.Format("{0}\r\n{1}=  {2}", this.btnDel.Text, CommonStr.strTaskNum, this.dgvTaskList.SelectedRows.Count.ToString());
				text2 = string.Format(CommonStr.strAreYouSure + " {0} ?", text2);
			}
			if (XMessageBox.Show(text2, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			string text3;
			if (string.IsNullOrEmpty(text))
			{
				text3 = " DELETE FROM t_b_ControllerTaskList WHERE [f_Id]= " + this.dgvTaskList.Rows[index].Cells[0].Value.ToString();
			}
			else
			{
				text3 = string.Format(" DELETE FROM t_b_ControllerTaskList WHERE [f_Id] IN ({0}) ", text);
			}
			wgAppConfig.runUpdateSql(text3);
			wgAppConfig.wgLog(string.Format("{0} {1} [{2}]", this.Text, this.btnDel.Text, text3));
			this.LoadTaskListData();
			if (this.dgvTaskList.RowCount > 0)
			{
				if (this.dgvTaskList.RowCount > num)
				{
					this.dgvTaskList.CurrentCell = this.dgvTaskList[1, num];
					return;
				}
				this.dgvTaskList.CurrentCell = this.dgvTaskList[1, this.dgvTaskList.RowCount - 1];
			}
		}

		private void dfrmControllerTaskList_KeyDown(object sender, KeyEventArgs e)
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

		private void dfrmControllerTaskList_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.dgvTaskList.Rows.Count > 0)
			{
				num = this.dgvTaskList.CurrentCell.RowIndex;
			}
			int index;
			if (this.dgvTaskList.SelectedRows.Count <= 0)
			{
				if (this.dgvTaskList.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvTaskList.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvTaskList.SelectedRows[0].Index;
			}
			string text = "";
			if (this.dgvTaskList.SelectedRows.Count > 1)
			{
				int num2 = 0;
				for (int i = 0; i < this.dgvTaskList.SelectedRows.Count; i++)
				{
					int num3 = 2147483647;
					for (int j = 0; j < this.dgvTaskList.SelectedRows.Count; j++)
					{
						index = this.dgvTaskList.SelectedRows[j].Index;
						int num4 = int.Parse(this.dgvTaskList.Rows[index].Cells[0].Value.ToString());
						if (num4 > num2 && num4 < num3)
						{
							num3 = num4;
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += num3.ToString();
					num2 = num3;
				}
			}
			bool flag = false;
			using (dfrmControllerTask dfrmControllerTask = new dfrmControllerTask())
			{
				if (this.dgvTaskList.SelectedRows.Count > 1)
				{
					dfrmControllerTask.Text = string.Format("{0}: [{1}]", this.btnEdit.Text, this.dgvTaskList.SelectedRows.Count.ToString());
					dfrmControllerTask.txtTaskIDs.Text = string.Format("({0})", text);
				}
				else
				{
					dfrmControllerTask.txtTaskIDs.Text = this.dgvTaskList.Rows[index].Cells[0].Value.ToString();
				}
				if (dfrmControllerTask.ShowDialog() == DialogResult.OK)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.LoadTaskListData();
			}
			if (this.dgvTaskList.RowCount > 0)
			{
				if (this.dgvTaskList.RowCount > num)
				{
					this.dgvTaskList.CurrentCell = this.dgvTaskList[1, num];
					return;
				}
				this.dgvTaskList.CurrentCell = this.dgvTaskList[1, this.dgvTaskList.RowCount - 1];
			}
		}

		private void dgvTaskList_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}
	}
}
