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

namespace WG3000_COMM.ExtendFunc.Elevator
{
	public class dfrmFloors : frmN3000
	{
		private DataTable dt;

		private DataTable dtDoors;

		private DataView dv;

		private DataView dvDoors;

		private string newFloorNameShort = "";

		private ArrayList arrDoorID = new ArrayList();

		private ArrayList arrControllerID = new ArrayList();

		private DataView dvFloorList;

		private dfrmFind dfrmFind1;

		private IContainer components;

		private ComboBox cboElevator;

		private Label label1;

		private ComboBox cboFloorNO;

		private Label label2;

		private Button btnAdd;

		private DataGridView dgvFloorList;

		private Button btnDel;

		private Button btnClose;

		private Label label3;

		private TextBox textBox1;

		private Button btnChange;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem btnRemoteControl;

		private ToolStripMenuItem btnRemoteControlNC;

		private DataGridViewTextBoxColumn f_floorID;

		private DataGridViewTextBoxColumn f_floorFullName;

		private DataGridViewTextBoxColumn f_DoorName;

		private DataGridViewTextBoxColumn f_floorNO;

		private DataGridViewTextBoxColumn f_ZoneID;

		private DataGridViewTextBoxColumn f_floorName;

		public dfrmFloors()
		{
			this.InitializeComponent();
		}

		private void dfrmControllerTaskList_Load(object sender, EventArgs e)
		{
			string newValue = CommonStr.strFloor;
			this.newFloorNameShort = CommonStr.strFloorShort;
			string text = "";
			if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 2)
			{
				newValue = CommonStr.strFloor2;
				text = CommonStr.strFloorController2;
				this.newFloorNameShort = CommonStr.strFloorShort2;
			}
			else if ((int.Parse("0" + wgAppConfig.getSystemParamByNO(144)) & 255) == 3)
			{
				newValue = CommonStr.strFloor3;
				text = CommonStr.strFloorController3;
				this.newFloorNameShort = CommonStr.strFloorShort3;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.label1.Text = this.label1.Text.Replace(CommonStr.strFloor, newValue);
				this.label3.Text = this.label3.Text.Replace(CommonStr.strFloor, newValue);
				this.btnChange.Text = this.btnChange.Text.Replace(CommonStr.strFloor, newValue);
				this.f_floorFullName.HeaderText = this.f_floorFullName.HeaderText.Replace(CommonStr.strFloor, newValue);
				this.f_floorNO.HeaderText = this.f_floorNO.HeaderText.Replace(CommonStr.strFloor, newValue);
				this.label2.Text = this.label2.Text.Replace(CommonStr.strFloorController, text);
				this.f_DoorName.HeaderText = this.f_DoorName.HeaderText.Replace(CommonStr.strFloorController, text);
			}
			this.loadDoorData();
			this.LoadFloorData();
			if (this.cboFloorNO.Items.Count > 0)
			{
				this.cboFloorNO.SelectedIndex = 0;
			}
			try
			{
				this.textBox1.Text = ((this.cboFloorNO.Text.Length == 1) ? "_" : "") + this.cboFloorNO.Text + this.newFloorNameShort;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			bool flag = false;
			string funName = "mnuElevator";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnChange.Visible = false;
				this.btnDel.Visible = false;
				this.cboElevator.Enabled = false;
				this.cboFloorNO.Enabled = false;
				this.textBox1.Enabled = false;
			}
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID,a.f_ControllerID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " AND b.f_ControllerSN >= 170000000 AND b.f_ControllerSN <= 179999999 ";
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
					goto IL_EF;
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
			IL_EF:
			int arg_FF_0 = this.dtDoors.Rows.Count;
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtDoors);
			this.cboElevator.Items.Clear();
			if (this.dvDoors.Count > 0)
			{
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					this.cboElevator.Items.Add(wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]));
					this.arrDoorID.Add(this.dvDoors[i]["f_DoorID"]);
					this.arrControllerID.Add(this.dvDoors[i]["f_ControllerID"]);
				}
			}
			if (this.cboElevator.Items.Count > 0)
			{
				this.cboElevator.SelectedIndex = 0;
				this.textBox1.Focus();
			}
		}

		private void updateOptionFloors()
		{
			if (this.dvFloorList != null)
			{
				if (this.cboElevator.SelectedIndex < 0)
				{
					this.cboFloorNO.Items.Clear();
					return;
				}
				this.dvFloorList.RowFilter = "f_DoorName = " + wgTools.PrepareStr(this.cboElevator.Text);
				this.cboFloorNO.Items.Clear();
				for (int i = 1; i <= 40; i++)
				{
					this.dvFloorList.RowFilter = "f_DoorName = " + wgTools.PrepareStr(this.cboElevator.Text) + "AND f_floorNO = " + i.ToString();
					if (this.dvFloorList.Count == 0)
					{
						this.cboFloorNO.Items.Add(i.ToString());
					}
				}
				if (this.cboFloorNO.Items.Count > 0)
				{
					this.cboFloorNO.SelectedIndex = 0;
				}
			}
		}

		private void LoadFloorData()
		{
			string text = "  SELECT t_b_Floor.f_floorID, t_b_Door.f_DoorName + '.' + t_b_Floor.f_floorName as f_floorFullName,  ";
			text += "   t_b_Door.f_DoorName, ";
			text += "   t_b_Floor.f_floorNO, t_b_Controller.f_ZoneID, t_b_Floor.f_floorName  ";
			text += "FROM (t_b_Floor LEFT JOIN t_b_Door ON t_b_Floor.f_DoorID = t_b_Door.f_DoorID) LEFT JOIN t_b_Controller ON t_b_Door.f_ControllerID = t_b_Controller.f_ControllerNO";
			text += " ORDER BY  (  t_b_Door.f_DoorName + '.' + t_b_Floor.f_floorName ) ";
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
			this.dvFloorList = new DataView(this.dt);
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
					goto IL_10C;
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
			IL_10C:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			this.dgvFloorList.AutoGenerateColumns = false;
			this.dgvFloorList.DataSource = this.dv;
			this.updateOptionFloors();
			int num = 0;
			while (num < this.dv.Table.Columns.Count && num < this.dgvFloorList.ColumnCount)
			{
				this.dgvFloorList.Columns[num].DataPropertyName = this.dv.Table.Columns[num].ColumnName;
				this.dgvFloorList.Columns[num].Name = this.dv.Table.Columns[num].ColumnName;
				num++;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (this.cboElevator.SelectedIndex < 0)
			{
				return;
			}
			if (this.cboFloorNO.SelectedIndex < 0)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.textBox1.Text))
			{
				return;
			}
			this.textBox1.Text = this.textBox1.Text.Trim();
			this.dvFloorList.RowFilter = "f_floorName = " + wgTools.PrepareStr(this.textBox1.Text) + " AND f_DoorName = " + wgTools.PrepareStr(this.cboElevator.Text);
			if (this.dvFloorList.Count > 0)
			{
				XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string text = " INSERT INTO t_b_floor(";
			text += "  f_floorName, f_DoorID, f_ControllerID, f_floorNO ";
			text += ") ";
			text += " VALUES ( ";
			text = text + wgTools.PrepareStr(this.textBox1.Text.Trim()) + " , ";
			text = text + this.arrDoorID[this.cboElevator.SelectedIndex] + " , ";
			text = text + this.arrControllerID[this.cboElevator.SelectedIndex] + " , ";
			text = text + this.cboFloorNO.Text + ") ";
			int num = wgAppConfig.runUpdateSql(text);
			if (num > 0)
			{
				this.LoadFloorData();
				try
				{
					this.textBox1.Text = ((this.cboFloorNO.Text.Length == 1) ? "_" : "") + this.cboFloorNO.Text + this.newFloorNameShort;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				this.textBox1.Focus();
				this.textBox1.SelectAll();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			int index;
			if (this.dgvFloorList.SelectedRows.Count <= 0)
			{
				if (this.dgvFloorList.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvFloorList.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvFloorList.SelectedRows[0].Index;
			}
			string text = string.Format("{0}\r\n{1}:  {2}", this.btnDel.Text, this.dgvFloorList.Columns[1].HeaderText, this.dgvFloorList.Rows[index].Cells[1].Value.ToString());
			text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			string strSql = " DELETE FROM t_b_floor WHERE [f_floorId]= " + this.dgvFloorList.Rows[index].Cells[0].Value.ToString();
			wgAppConfig.runUpdateSql(strSql);
			this.LoadFloorData();
		}

		private void cboElevator_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.updateOptionFloors();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.textBox1.Text))
			{
				this.btnAdd.Enabled = false;
				return;
			}
			this.btnAdd.Enabled = true;
		}

		private void btnChange_Click(object sender, EventArgs e)
		{
			int index;
			if (this.dgvFloorList.SelectedRows.Count <= 0)
			{
				if (this.dgvFloorList.SelectedCells.Count <= 0)
				{
					return;
				}
				index = this.dgvFloorList.SelectedCells[0].RowIndex;
			}
			else
			{
				index = this.dgvFloorList.SelectedRows[0].Index;
			}
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = (sender as Button).Text + ":  " + this.dgvFloorList.Rows[index].Cells[1].Value.ToString();
				if (dfrmInputNewName.ShowDialog(this) == DialogResult.OK)
				{
					string text = dfrmInputNewName.strNewName.Trim();
					if (!string.IsNullOrEmpty(text))
					{
						if (!(text == this.dgvFloorList.Rows[index].Cells[1].Value.ToString()))
						{
							this.dvFloorList.RowFilter = "f_floorName = " + wgTools.PrepareStr(text);
							if (this.dvFloorList.Count > 0)
							{
								XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
							else
							{
								string strSql = string.Format(" UPDATE t_b_floor SET f_floorName={0}  WHERE [f_floorId]={1} ", wgTools.PrepareStr(text), this.dgvFloorList.Rows[index].Cells[0].Value.ToString());
								wgAppConfig.runUpdateSql(strSql);
								this.LoadFloorData();
							}
						}
					}
				}
			}
		}

		private void dfrmFloors_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyValue == 81 && e.Control)
				{
					this.btnRemoteControlNC.Visible = true;
				}
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

		private void btnRemoteControl_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvFloorList.SelectedRows.Count <= 0)
				{
					if (this.dgvFloorList.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvFloorList.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvFloorList.SelectedRows[0].Index;
				}
				using (icController icController = new icController())
				{
					icController.GetInfoFromDBByDoorName(this.dgvFloorList.Rows[index].Cells["f_DoorName"].Value.ToString());
					if (icController.RemoteOpenFoorIP(int.Parse(this.dgvFloorList.Rows[index].Cells["f_floorNO"].Value.ToString()), (uint)icOperator.OperatorID, 18446744073709551615uL) > 0)
					{
						string text = string.Concat(new string[]
						{
							this.btnRemoteControl.Text,
							" ",
							this.dgvFloorList.Rows[index].Cells["f_floorName"].Value.ToString(),
							" ",
							CommonStr.strSuccessfully
						});
						wgAppConfig.wgLog(text);
						XMessageBox.Show(text);
					}
					else
					{
						string text = string.Concat(new string[]
						{
							this.btnRemoteControl.Text,
							"  ",
							this.dgvFloorList.Rows[index].Cells["f_floorName"].Value.ToString(),
							" ",
							CommonStr.strFailed
						});
						wgAppConfig.wgLog(text);
						XMessageBox.Show(this, text, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnRemoteControlNC_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvFloorList.SelectedRows.Count <= 0)
				{
					if (this.dgvFloorList.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvFloorList.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvFloorList.SelectedRows[0].Index;
				}
				using (icController icController = new icController())
				{
					icController.GetInfoFromDBByDoorName(this.dgvFloorList.Rows[index].Cells["f_DoorName"].Value.ToString());
					if (icController.RemoteOpenFoorIP(int.Parse(this.dgvFloorList.Rows[index].Cells["f_floorNO"].Value.ToString()) + 40, (uint)icOperator.OperatorID, 18446744073709551615uL) > 0)
					{
						string text = string.Concat(new string[]
						{
							this.btnRemoteControlNC.Text,
							" ",
							this.dgvFloorList.Rows[index].Cells["f_floorName"].Value.ToString(),
							" ",
							CommonStr.strSuccessfully
						});
						wgAppConfig.wgLog(text);
						XMessageBox.Show(text);
					}
					else
					{
						string text = string.Concat(new string[]
						{
							this.btnRemoteControlNC.Text,
							"  ",
							this.dgvFloorList.Rows[index].Cells["f_floorName"].Value.ToString(),
							" ",
							CommonStr.strFailed
						});
						wgAppConfig.wgLog(text);
						XMessageBox.Show(this, text, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void dfrmFloors_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmFloors));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.btnRemoteControl = new ToolStripMenuItem();
			this.btnRemoteControlNC = new ToolStripMenuItem();
			this.btnChange = new Button();
			this.textBox1 = new TextBox();
			this.label3 = new Label();
			this.btnClose = new Button();
			this.btnDel = new Button();
			this.dgvFloorList = new DataGridView();
			this.f_floorID = new DataGridViewTextBoxColumn();
			this.f_floorFullName = new DataGridViewTextBoxColumn();
			this.f_DoorName = new DataGridViewTextBoxColumn();
			this.f_floorNO = new DataGridViewTextBoxColumn();
			this.f_ZoneID = new DataGridViewTextBoxColumn();
			this.f_floorName = new DataGridViewTextBoxColumn();
			this.btnAdd = new Button();
			this.label2 = new Label();
			this.cboFloorNO = new ComboBox();
			this.label1 = new Label();
			this.cboElevator = new ComboBox();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.dgvFloorList).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnRemoteControl,
				this.btnRemoteControlNC
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.btnRemoteControl, "btnRemoteControl");
			this.btnRemoteControl.Name = "btnRemoteControl";
			this.btnRemoteControl.Click += new EventHandler(this.btnRemoteControl_Click);
			componentResourceManager.ApplyResources(this.btnRemoteControlNC, "btnRemoteControlNC");
			this.btnRemoteControlNC.Name = "btnRemoteControlNC";
			this.btnRemoteControlNC.Click += new EventHandler(this.btnRemoteControlNC_Click);
			componentResourceManager.ApplyResources(this.btnChange, "btnChange");
			this.btnChange.BackColor = Color.Transparent;
			this.btnChange.BackgroundImage = Resources.pMain_button_normal;
			this.btnChange.ForeColor = Color.White;
			this.btnChange.Name = "btnChange";
			this.btnChange.UseVisualStyleBackColor = false;
			this.btnChange.Click += new EventHandler(this.btnChange_Click);
			componentResourceManager.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
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
			componentResourceManager.ApplyResources(this.dgvFloorList, "dgvFloorList");
			this.dgvFloorList.AllowUserToAddRows = false;
			this.dgvFloorList.AllowUserToDeleteRows = false;
			this.dgvFloorList.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvFloorList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvFloorList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvFloorList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_floorID,
				this.f_floorFullName,
				this.f_DoorName,
				this.f_floorNO,
				this.f_ZoneID,
				this.f_floorName
			});
			this.dgvFloorList.ContextMenuStrip = this.contextMenuStrip1;
			this.dgvFloorList.EnableHeadersVisualStyles = false;
			this.dgvFloorList.Name = "dgvFloorList";
			this.dgvFloorList.ReadOnly = true;
			this.dgvFloorList.RowTemplate.Height = 23;
			this.dgvFloorList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvFloorList.KeyDown += new KeyEventHandler(this.dfrmFloors_KeyDown);
			componentResourceManager.ApplyResources(this.f_floorID, "f_floorID");
			this.f_floorID.Name = "f_floorID";
			this.f_floorID.ReadOnly = true;
			this.f_floorID.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_floorFullName, "f_floorFullName");
			this.f_floorFullName.Name = "f_floorFullName";
			this.f_floorFullName.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_DoorName.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_DoorName, "f_DoorName");
			this.f_DoorName.Name = "f_DoorName";
			this.f_DoorName.ReadOnly = true;
			this.f_DoorName.Resizable = DataGridViewTriState.True;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_floorNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_floorNO, "f_floorNO");
			this.f_floorNO.Name = "f_floorNO";
			this.f_floorNO.ReadOnly = true;
			this.f_floorNO.Resizable = DataGridViewTriState.True;
			componentResourceManager.ApplyResources(this.f_ZoneID, "f_ZoneID");
			this.f_ZoneID.Name = "f_ZoneID";
			this.f_ZoneID.ReadOnly = true;
			this.f_ZoneID.SortMode = DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_floorName.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_floorName, "f_floorName");
			this.f_floorName.Name = "f_floorName";
			this.f_floorName.ReadOnly = true;
			this.f_floorName.Resizable = DataGridViewTriState.True;
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
			componentResourceManager.ApplyResources(this.cboFloorNO, "cboFloorNO");
			this.cboFloorNO.DropDownHeight = 300;
			this.cboFloorNO.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboFloorNO.FormattingEnabled = true;
			this.cboFloorNO.Name = "cboFloorNO";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.cboElevator, "cboElevator");
			this.cboElevator.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboElevator.FormattingEnabled = true;
			this.cboElevator.Name = "cboElevator";
			this.cboElevator.SelectedIndexChanged += new EventHandler(this.cboElevator_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnChange);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnDel);
			base.Controls.Add(this.dgvFloorList);
			base.Controls.Add(this.btnAdd);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cboFloorNO);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cboElevator);
			base.Name = "dfrmFloors";
			base.FormClosing += new FormClosingEventHandler(this.dfrmFloors_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerTaskList_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmFloors_KeyDown);
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.dgvFloorList).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
