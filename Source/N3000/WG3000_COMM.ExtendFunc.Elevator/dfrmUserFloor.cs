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

namespace WG3000_COMM.ExtendFunc.Elevator
{
	public class dfrmUserFloor : frmN3000
	{
		private DataTable dt;

		private DataView dvSelected;

		private int m_consumerID;

		public string strSqlSelected = "";

		private int[] controlSegIDList = new int[256];

		private string[] controlSegNameList = new string[256];

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private DataTable oldTbPrivilege;

		private DataTable tbPrivilege;

		private DataView dv;

		private bool bEdit;

		private SqlCommand cmd;

		private SqlConnection cn;

		private DataTable dtDoorTmpSelected;

		private DataView dvDoorTmpSelected;

		private DataView dvSelectedControllerID;

		private string strZoneFilter = "";

		private dfrmFind dfrmFind1;

		private IContainer components;

		private Button btnOK;

		private Button btnExit;

		private ComboBox cbof_ZoneID;

		private Label label25;

		private DataGridView dgvSelectedDoors;

		private DataGridView dgvFloors;

		private Button btnDelAllDoors;

		private Button btnDelOneDoor;

		private Button btnAddOneDoor;

		private Button btnAddAllDoors;

		private ComboBox cbof_ControlSegID;

		private Label label1;

		private Label lblOptional;

		private Label lblSeleted;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn f_Selected2;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn TimeProfile;

		private DataGridViewTextBoxColumn f_ControlSegName;

		private DataGridViewTextBoxColumn ControllerSN2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		private DataGridViewTextBoxColumn f_ZoneID;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn f_ControlSegName1;

		private DataGridViewTextBoxColumn ControllerSN;

		public int consumerID
		{
			get
			{
				return this.m_consumerID;
			}
			set
			{
				this.m_consumerID = value;
			}
		}

		public dfrmUserFloor()
		{
			this.InitializeComponent();
		}

		private void loadControlSegData()
		{
			this.cbof_ControlSegID.Items.Clear();
			this.cbof_ControlSegID.Items.Add(CommonStr.strFreeTime);
			this.controlSegNameList[0] = CommonStr.strFreeTime;
			this.controlSegIDList[0] = 1;
			int num = 1;
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT ";
				text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
				text += "  IIF(ISNULL([f_ControlSegName]), CSTR([t_b_ControlSeg].[f_ControlSegID]), CSTR([t_b_ControlSeg].[f_ControlSegID]) & ' [' & [f_ControlSegName] & ']') AS f_ControlSegID ";
				text += "  FROM [t_b_ControlSeg]  WHERE  [t_b_ControlSeg].[f_ControlSegID]>1  ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							this.cbof_ControlSegID.Items.Add(oleDbDataReader["f_ControlSegID"]);
							this.controlSegNameList[num] = (string)oleDbDataReader["f_ControlSegID"];
							this.controlSegIDList[num] = (int)oleDbDataReader["f_ControlSegIDBak"];
							num++;
						}
						oleDbDataReader.Close();
					}
					goto IL_208;
				}
			}
			text = " SELECT ";
			text += " [t_b_ControlSeg].[f_ControlSegID] as f_ControlSegIDBak, ";
			text += "   CASE WHEN [f_ControlSegName] IS NULL THEN CONVERT(nvarchar(50),  ";
			text += "     ([t_b_ControlSeg].[f_ControlSegID])) ELSE (CONVERT(nvarchar(50), ";
			text += "     ([t_b_ControlSeg].[f_ControlSegID])) + ' [' + [f_ControlSegName] + ']') ";
			text += "    END AS f_ControlSegID  ";
			text += "  FROM [t_b_ControlSeg] WHERE  [t_b_ControlSeg].[f_ControlSegID]>1 ORDER BY [t_b_ControlSeg].[f_ControlSegID] ASC  ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						this.cbof_ControlSegID.Items.Add(sqlDataReader["f_ControlSegID"]);
						this.controlSegNameList[num] = (string)sqlDataReader["f_ControlSegID"];
						this.controlSegIDList[num] = (int)sqlDataReader["f_ControlSegIDBak"];
						num++;
					}
					sqlDataReader.Close();
				}
			}
			IL_208:
			if (this.cbof_ControlSegID.Items.Count > 0)
			{
				this.cbof_ControlSegID.SelectedIndex = 0;
			}
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cbof_ZoneID.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrZoneName[i].ToString()))
				{
					this.cbof_ZoneID.Items.Add(CommonStr.strAllZones);
				}
				else
				{
					this.cbof_ZoneID.Items.Add(this.arrZoneName[i].ToString());
				}
			}
			if (this.cbof_ZoneID.Items.Count > 0)
			{
				this.cbof_ZoneID.SelectedIndex = 0;
			}
			bool visible = true;
			this.label25.Visible = visible;
			this.cbof_ZoneID.Visible = visible;
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_floorID,  c.f_DoorName + '.' + a.f_floorName as f_floorFullName , 0 as f_Selected, b.f_ZoneID, 0 as f_TimeProfile, b.f_ControllerID, b.f_ControllerSN ";
			text += " FROM t_b_floor a, t_b_Controller b,t_b_Door c WHERE c.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID and a.f_DoorID = c.f_DoorID ";
			text += " ORDER BY  (  c.f_DoorName + '.' + a.f_floorName ) ";
			this.dt = new DataTable();
			this.dv = new DataView(this.dt);
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
					goto IL_F4;
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
			try
			{
				IL_F4:
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dt.Columns[0]
				};
				this.dt.PrimaryKey = primaryKey;
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			this.dv.RowFilter = "f_Selected = 0";
			this.dvSelected.RowFilter = "f_Selected > 0";
			this.dgvFloors.AutoGenerateColumns = false;
			this.dgvFloors.DataSource = this.dv;
			this.dgvSelectedDoors.AutoGenerateColumns = false;
			this.dgvSelectedDoors.DataSource = this.dvSelected;
			for (int i = 0; i < this.dgvFloors.Columns.Count; i++)
			{
				this.dgvFloors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
				this.dgvSelectedDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
			}
		}

		private void loadPrivilegeData()
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("loadPrivilegeData Start");
			string text = " SELECT f_RecID, f_FloorId, f_ControlSegID ";
			text = text + " FROM t_b_UserFloor  WHERE f_ConsumerID=  " + this.m_consumerID.ToString();
			this.tbPrivilege = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							oleDbDataAdapter.Fill(this.tbPrivilege);
						}
					}
					goto IL_FC;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						sqlDataAdapter.Fill(this.tbPrivilege);
					}
				}
			}
			IL_FC:
			wgTools.WriteLine("da.Fill End");
			this.dv = new DataView(this.tbPrivilege);
			this.oldTbPrivilege = this.tbPrivilege;
			int num = 1;
			if (this.dv.Count > 0)
			{
				DataTable table = ((DataView)this.dgvFloors.DataSource).Table;
				for (int i = 0; i < this.dv.Count; i++)
				{
					for (int j = 0; j < table.Rows.Count; j++)
					{
						if ((int)this.dv[i]["f_floorID"] == (int)table.Rows[j]["f_floorID"])
						{
							table.Rows[j]["f_Selected"] = 1;
							num = int.Parse(this.dv[i]["f_ControlSegID"].ToString());
							break;
						}
					}
				}
			}
			for (int k = 0; k < this.controlSegIDList.Length; k++)
			{
				if (this.controlSegIDList[k] == num)
				{
					this.cbof_ControlSegID.SelectedIndex = k;
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private void updateCount()
		{
			this.lblOptional.Text = this.dgvFloors.RowCount.ToString();
			this.lblSeleted.Text = this.dgvSelectedDoors.RowCount.ToString();
		}

		private void dfrmPrivilegeSingle_Load(object sender, EventArgs e)
		{
			try
			{
				this.label1.Visible = wgAppConfig.getParamValBoolByNO(121);
				this.cbof_ControlSegID.Visible = wgAppConfig.getParamValBoolByNO(121);
				this.loadControlSegData();
				this.loadZoneInfo();
				this.loadDoorData();
				this.loadPrivilegeData();
				this.updateCount();
				bool flag = false;
				string funName = "mnuElevator";
				if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
				{
					this.btnAddAllDoors.Visible = false;
					this.btnAddOneDoor.Visible = false;
					this.btnDelAllDoors.Visible = false;
					this.btnDelOneDoor.Visible = false;
					this.btnOK.Visible = false;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			Cursor.Current = Cursors.WaitCursor;
		}

		private void selectObject(DataGridView dgv)
		{
			try
			{
				int index;
				if (dgv.SelectedRows.Count <= 0)
				{
					if (dgv.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dgv.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dgv.SelectedRows[0].Index;
				}
				DataTable table = ((DataView)dgv.DataSource).Table;
				if (dgv.SelectedRows.Count > 0)
				{
					int count = dgv.SelectedRows.Count;
					int[] array = new int[count];
					for (int i = 0; i < dgv.SelectedRows.Count; i++)
					{
						array[i] = (int)dgv.SelectedRows[i].Cells[0].Value;
					}
					for (int j = 0; j < count; j++)
					{
						int num = array[j];
						DataRow dataRow = table.Rows.Find(num);
						if (dataRow != null)
						{
							dataRow["f_Selected"] = 1;
						}
					}
				}
				else
				{
					int num2 = (int)dgv.Rows[index].Cells[0].Value;
					DataRow dataRow = table.Rows.Find(num2);
					if (dataRow != null)
					{
						dataRow["f_Selected"] = 1;
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void btnAddOneDoor_Click(object sender, EventArgs e)
		{
			this.selectObject(this.dgvFloors);
			this.updateCount();
		}

		private void btnDelOneDoor_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedDoors);
			this.updateCount();
		}

		private void btnAddAllDoors_Click(object sender, EventArgs e)
		{
			DataTable table = ((DataView)this.dgvFloors.DataSource).Table;
			if (this.cbof_ZoneID.SelectedIndex <= 0 && this.cbof_ZoneID.Text == CommonStr.strAllZones)
			{
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if ((int)table.Rows[i]["f_Selected"] != 1)
					{
						table.Rows[i]["f_Selected"] = 1;
					}
				}
			}
			else
			{
				using (DataView dataView = new DataView((this.dgvFloors.DataSource as DataView).Table))
				{
					dataView.RowFilter = string.Format("  {0} ", this.strZoneFilter);
					for (int j = 0; j < dataView.Count; j++)
					{
						dataView[j]["f_Selected"] = 1;
					}
				}
			}
			this.updateCount();
		}

		private void btnDelAllDoors_Click(object sender, EventArgs e)
		{
			DataTable table = ((DataView)this.dgvSelectedDoors.DataSource).Table;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				table.Rows[i]["f_Selected"] = 0;
			}
			this.updateCount();
		}

		private void logOperate(object sender)
		{
			string text = this.Text;
			string text2 = "";
			for (int i = 0; i <= Math.Min(10, this.dgvSelectedDoors.RowCount) - 1; i++)
			{
				text2 = text2 + ((DataView)this.dgvSelectedDoors.DataSource)[i]["f_floorFullName"] + ",";
			}
			if (this.dgvSelectedDoors.RowCount > 10)
			{
				object obj = text2;
				text2 = string.Concat(new object[]
				{
					obj,
					"......(",
					this.dgvSelectedDoors.RowCount,
					")"
				});
			}
			else
			{
				object obj2 = text2;
				text2 = string.Concat(new object[]
				{
					obj2,
					"(",
					this.dgvSelectedDoors.RowCount,
					")"
				});
			}
			wgAppConfig.wgLog(string.Format("{0}:[{1} => {2}]:{3} => {4}", new object[]
			{
				(sender as Button).Text.Replace("\r\n", ""),
				1,
				this.dgvSelectedDoors.RowCount.ToString(),
				text,
				text2
			}), EventLogEntryType.Information, null);
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (this.bEdit)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.Close();
		}

		private void dgvDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnAddOneDoor.PerformClick();
		}

		private void dgvSelectedDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnDelOneDoor.PerformClick();
		}

		private void dgvSelectedDoors_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			this.bEdit = true;
			Cursor.Current = Cursors.WaitCursor;
			this.cn = new SqlConnection(wgAppConfig.dbConString);
			this.dtDoorTmpSelected = ((DataView)this.dgvSelectedDoors.DataSource).Table.Copy();
			this.dvDoorTmpSelected = new DataView(this.dtDoorTmpSelected);
			this.dvSelectedControllerID = new DataView(this.dtDoorTmpSelected);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			this.dvDoorTmpSelected.RowFilter = "f_Selected > 0";
			this.dvSelectedControllerID.RowFilter = "f_Selected = 2";
			foreach (DataRowView dataRowView in this.dvDoorTmpSelected)
			{
				this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", dataRowView["f_ControllerID"].ToString());
				if (this.dvSelectedControllerID.Count == wgMjController.GetControllerType(int.Parse(dataRowView["f_ControllerSN"].ToString())))
				{
					if (arrayList.IndexOf(int.Parse(dataRowView["f_ControllerID"].ToString())) < 0)
					{
						arrayList.Add(int.Parse(dataRowView["f_ControllerID"].ToString()));
						arrayList2.Add(int.Parse(dataRowView["f_ControllerSN"].ToString()));
					}
				}
				else
				{
					dataRowView["f_Selected"] = 2;
				}
			}
			this.dvDoorTmpSelected.RowFilter = "f_Selected = 2";
			this.cn.Open();
			this.cmd = new SqlCommand("", this.cn);
			this.cmd.CommandTimeout = wgAppConfig.dbCommandTimeout;
			string text = "DELETE FROM  [t_b_UserFloor]    ";
			if (string.IsNullOrEmpty(this.strSqlSelected))
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"WHERE [f_ConsumerID] = (",
					this.consumerID,
					" ) "
				});
			}
			else
			{
				text += string.Format(" WHERE f_ConsumerID IN ({0}) ", this.strSqlSelected);
			}
			this.cmd.CommandText = text;
			wgTools.WriteLine(text);
			this.cmd.ExecuteNonQuery();
			wgTools.WriteLine("DELETE FROM  [t_b_UserFloor] End");
			int i = 0;
			if (string.IsNullOrEmpty(this.strSqlSelected))
			{
				while (i < this.dgvSelectedDoors.Rows.Count)
				{
					text = "INSERT INTO [t_b_UserFloor] (f_ConsumerID, f_floorID , f_ControlSegID, f_MoreFloorNum)";
					text += " VALUES ( ";
					text = text + this.consumerID.ToString() + ",";
					text = text + this.dgvSelectedDoors.Rows[i].Cells[0].Value.ToString() + ",";
					text = text + this.controlSegIDList[this.cbof_ControlSegID.SelectedIndex].ToString() + ",";
					text = text + this.dgvSelectedDoors.Rows.Count.ToString() + ")";
					this.cmd.CommandText = text;
					this.cmd.ExecuteNonQuery();
					i++;
				}
			}
			else
			{
				while (i < this.dgvSelectedDoors.Rows.Count)
				{
					text = "INSERT INTO [t_b_UserFloor] (f_ConsumerID, f_floorID , f_ControlSegID, f_MoreFloorNum)";
					text += " SELECT  f_ConsumerID,  ";
					text = text + this.dgvSelectedDoors.Rows[i].Cells[0].Value.ToString() + " AS f_floorID,";
					text = text + this.controlSegIDList[this.cbof_ControlSegID.SelectedIndex].ToString() + " AS f_ControlSegID,";
					text = text + this.dgvSelectedDoors.Rows.Count.ToString() + " AS f_MoreFloorNum ";
					text += " from t_b_consumer ";
					text += string.Format(" WHERE f_ConsumerID IN ({0}) ", this.strSqlSelected);
					this.cmd.CommandText = text;
					this.cmd.ExecuteNonQuery();
					i++;
				}
			}
			wgTools.WriteLine("INSERT INTO [t_b_UserFloor] End");
			string format;
			if (sender.Equals(this.btnOK))
			{
				format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
			}
			else
			{
				format = "UPDATE t_b_Controller SET f_lastDelAddAndUploadDateTime ={0}, f_lastDelAddAndUploadConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
			}
			for (int j = 0; j < this.dgvSelectedDoors.Rows.Count; j++)
			{
				text = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), 1, (int)((DataView)this.dgvSelectedDoors.DataSource)[j]["f_ControllerID"]);
				this.cmd.CommandText = text;
				this.cmd.ExecuteNonQuery();
			}
			this.cn.Close();
			Cursor.Current = Cursors.Default;
			this.logOperate(this.btnOK);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			OleDbConnection oleDbConnection = null;
			this.bEdit = true;
			Cursor.Current = Cursors.WaitCursor;
			oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			this.dtDoorTmpSelected = ((DataView)this.dgvSelectedDoors.DataSource).Table.Copy();
			this.dvDoorTmpSelected = new DataView(this.dtDoorTmpSelected);
			this.dvSelectedControllerID = new DataView(this.dtDoorTmpSelected);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			this.dvDoorTmpSelected.RowFilter = "f_Selected > 0";
			this.dvSelectedControllerID.RowFilter = "f_Selected = 2";
			foreach (DataRowView dataRowView in this.dvDoorTmpSelected)
			{
				this.dvSelectedControllerID.RowFilter = string.Format("f_Selected > 0  AND f_ControllerID ={0} ", dataRowView["f_ControllerID"].ToString());
				if (this.dvSelectedControllerID.Count == wgMjController.GetControllerType(int.Parse(dataRowView["f_ControllerSN"].ToString())))
				{
					if (arrayList.IndexOf(int.Parse(dataRowView["f_ControllerID"].ToString())) < 0)
					{
						arrayList.Add(int.Parse(dataRowView["f_ControllerID"].ToString()));
						arrayList2.Add(int.Parse(dataRowView["f_ControllerSN"].ToString()));
					}
				}
				else
				{
					dataRowView["f_Selected"] = 2;
				}
			}
			this.dvDoorTmpSelected.RowFilter = "f_Selected = 2";
			oleDbConnection.Open();
			OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection);
			oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
			string text = "DELETE FROM  [t_b_UserFloor]    ";
			if (string.IsNullOrEmpty(this.strSqlSelected))
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"WHERE [f_ConsumerID] = (",
					this.consumerID,
					" ) "
				});
			}
			else
			{
				text += string.Format(" WHERE f_ConsumerID IN ({0}) ", this.strSqlSelected);
			}
			oleDbCommand.CommandText = text;
			wgTools.WriteLine(text);
			oleDbCommand.ExecuteNonQuery();
			wgTools.WriteLine("DELETE FROM  [t_b_UserFloor] End");
			int i = 0;
			if (string.IsNullOrEmpty(this.strSqlSelected))
			{
				while (i < this.dgvSelectedDoors.Rows.Count)
				{
					text = "INSERT INTO [t_b_UserFloor] (f_ConsumerID, f_floorID , f_ControlSegID, f_MoreFloorNum)";
					text += " VALUES ( ";
					text = text + this.consumerID.ToString() + ",";
					text = text + this.dgvSelectedDoors.Rows[i].Cells[0].Value.ToString() + ",";
					text = text + this.controlSegIDList[this.cbof_ControlSegID.SelectedIndex].ToString() + ",";
					text = text + this.dgvSelectedDoors.Rows.Count.ToString() + ")";
					oleDbCommand.CommandText = text;
					oleDbCommand.ExecuteNonQuery();
					i++;
				}
			}
			else
			{
				while (i < this.dgvSelectedDoors.Rows.Count)
				{
					text = "INSERT INTO [t_b_UserFloor] (f_ConsumerID, f_floorID , f_ControlSegID, f_MoreFloorNum)";
					text += " SELECT  f_ConsumerID,  ";
					text = text + this.dgvSelectedDoors.Rows[i].Cells[0].Value.ToString() + " AS f_floorID,";
					text = text + this.controlSegIDList[this.cbof_ControlSegID.SelectedIndex].ToString() + " AS f_ControlSegID,";
					text = text + this.dgvSelectedDoors.Rows.Count.ToString() + " AS f_MoreFloorNum ";
					text += " from t_b_consumer ";
					text += string.Format(" WHERE f_ConsumerID IN ({0}) ", this.strSqlSelected);
					oleDbCommand.CommandText = text;
					oleDbCommand.ExecuteNonQuery();
					i++;
				}
			}
			wgTools.WriteLine("INSERT INTO [t_b_UserFloor] End");
			string format;
			if (sender.Equals(this.btnOK))
			{
				format = "UPDATE t_b_Controller SET f_lastDelAddDateTime ={0}, f_lastDelAddConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
			}
			else
			{
				format = "UPDATE t_b_Controller SET f_lastDelAddAndUploadDateTime ={0}, f_lastDelAddAndUploadConsuemrsTotal ={1:d} WHERE f_ControllerID ={2:d}";
			}
			for (int j = 0; j < this.dgvSelectedDoors.Rows.Count; j++)
			{
				text = string.Format(format, wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")), 1, (int)((DataView)this.dgvSelectedDoors.DataSource)[j]["f_ControllerID"]);
				oleDbCommand.CommandText = text;
				oleDbCommand.ExecuteNonQuery();
			}
			oleDbConnection.Close();
			Cursor.Current = Cursors.Default;
			this.logOperate(this.btnOK);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void cbof_Zone_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvFloors.DataSource != null)
			{
				DataView dataView = (DataView)this.dgvFloors.DataSource;
				if (this.cbof_ZoneID.SelectedIndex < 0 || (this.cbof_ZoneID.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
				{
					dataView.RowFilter = "f_Selected = 0";
					this.strZoneFilter = "";
				}
				else
				{
					dataView.RowFilter = "f_Selected = 0 AND f_ZoneID =" + this.arrZoneID[this.cbof_ZoneID.SelectedIndex];
					this.strZoneFilter = " f_ZoneID =" + this.arrZoneID[this.cbof_ZoneID.SelectedIndex];
					int num = (int)this.arrZoneID[this.cbof_ZoneID.SelectedIndex];
					int num2 = (int)this.arrZoneNO[this.cbof_ZoneID.SelectedIndex];
					int zoneChildMaxNo = icControllerZone.getZoneChildMaxNo(this.cbof_ZoneID.Text, this.arrZoneName, this.arrZoneNO);
					if (num2 > 0)
					{
						if (num2 >= zoneChildMaxNo)
						{
							dataView.RowFilter = string.Format("f_Selected = 0 AND f_ZoneID ={0:d} ", num);
							this.strZoneFilter = string.Format(" f_ZoneID ={0:d} ", num);
						}
						else
						{
							dataView.RowFilter = "f_Selected = 0 ";
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
							dataView.RowFilter = string.Format("f_Selected = 0 AND ( {0} )", text);
							this.strZoneFilter = string.Format("  {0} ", text);
						}
					}
					dataView.RowFilter = string.Format("f_Selected = 0 AND ( {0} )", this.strZoneFilter);
				}
				this.updateCount();
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{
		}

		private void cbof_ControlSegID_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dfrmUserFloor_KeyDown(object sender, KeyEventArgs e)
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

		private void dfrmUserFloor_FormClosing(object sender, FormClosingEventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmUserFloor));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.btnOK = new Button();
			this.btnExit = new Button();
			this.cbof_ControlSegID = new ComboBox();
			this.label1 = new Label();
			this.cbof_ZoneID = new ComboBox();
			this.label25 = new Label();
			this.dgvSelectedDoors = new DataGridView();
			this.dgvFloors = new DataGridView();
			this.btnDelAllDoors = new Button();
			this.btnDelOneDoor = new Button();
			this.btnAddOneDoor = new Button();
			this.btnAddAllDoors = new Button();
			this.lblOptional = new Label();
			this.lblSeleted = new Label();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.f_ZoneID = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.f_ControlSegName1 = new DataGridViewTextBoxColumn();
			this.ControllerSN = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.f_Selected2 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.TimeProfile = new DataGridViewTextBoxColumn();
			this.f_ControlSegName = new DataGridViewTextBoxColumn();
			this.ControllerSN2 = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dgvSelectedDoors).BeginInit();
			((ISupportInitialize)this.dgvFloors).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this.cbof_ControlSegID, "cbof_ControlSegID");
			this.cbof_ControlSegID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ControlSegID.FormattingEnabled = true;
			this.cbof_ControlSegID.Name = "cbof_ControlSegID";
			this.cbof_ControlSegID.SelectedIndexChanged += new EventHandler(this.cbof_ControlSegID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.label1.Click += new EventHandler(this.label1_Click);
			componentResourceManager.ApplyResources(this.cbof_ZoneID, "cbof_ZoneID");
			this.cbof_ZoneID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ZoneID.FormattingEnabled = true;
			this.cbof_ZoneID.Name = "cbof_ZoneID";
			this.cbof_ZoneID.SelectedIndexChanged += new EventHandler(this.cbof_Zone_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label25, "label25");
			this.label25.BackColor = Color.Transparent;
			this.label25.ForeColor = Color.White;
			this.label25.Name = "label25";
			componentResourceManager.ApplyResources(this.dgvSelectedDoors, "dgvSelectedDoors");
			this.dgvSelectedDoors.AllowUserToAddRows = false;
			this.dgvSelectedDoors.AllowUserToDeleteRows = false;
			this.dgvSelectedDoors.AllowUserToOrderColumns = true;
			this.dgvSelectedDoors.BackgroundColor = Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedDoors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelectedDoors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelectedDoors.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn8,
				this.dataGridViewTextBoxColumn9,
				this.f_Selected2,
				this.Column1,
				this.TimeProfile,
				this.f_ControlSegName,
				this.ControllerSN2
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedDoors.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelectedDoors.EnableHeadersVisualStyles = false;
			this.dgvSelectedDoors.Name = "dgvSelectedDoors";
			this.dgvSelectedDoors.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedDoors.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvSelectedDoors.RowTemplate.Height = 23;
			this.dgvSelectedDoors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSelectedDoors.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvSelectedDoors_CellFormatting);
			this.dgvSelectedDoors.MouseDoubleClick += new MouseEventHandler(this.dgvSelectedDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dgvFloors, "dgvFloors");
			this.dgvFloors.AllowUserToAddRows = false;
			this.dgvFloors.AllowUserToDeleteRows = false;
			this.dgvFloors.AllowUserToOrderColumns = true;
			this.dgvFloors.BackgroundColor = Color.White;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.dgvFloors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvFloors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvFloors.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.f_Selected,
				this.f_ZoneID,
				this.Column2,
				this.f_ControlSegName1,
				this.ControllerSN
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Window;
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.dgvFloors.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvFloors.EnableHeadersVisualStyles = false;
			this.dgvFloors.Name = "dgvFloors";
			this.dgvFloors.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvFloors.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvFloors.RowTemplate.Height = 23;
			this.dgvFloors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvFloors.KeyDown += new KeyEventHandler(this.dfrmUserFloor_KeyDown);
			this.dgvFloors.MouseDoubleClick += new MouseEventHandler(this.dgvDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.btnDelAllDoors, "btnDelAllDoors");
			this.btnDelAllDoors.BackColor = Color.Transparent;
			this.btnDelAllDoors.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelAllDoors.ForeColor = Color.White;
			this.btnDelAllDoors.Name = "btnDelAllDoors";
			this.btnDelAllDoors.UseVisualStyleBackColor = false;
			this.btnDelAllDoors.Click += new EventHandler(this.btnDelAllDoors_Click);
			componentResourceManager.ApplyResources(this.btnDelOneDoor, "btnDelOneDoor");
			this.btnDelOneDoor.BackColor = Color.Transparent;
			this.btnDelOneDoor.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelOneDoor.ForeColor = Color.White;
			this.btnDelOneDoor.Name = "btnDelOneDoor";
			this.btnDelOneDoor.UseVisualStyleBackColor = false;
			this.btnDelOneDoor.Click += new EventHandler(this.btnDelOneDoor_Click);
			componentResourceManager.ApplyResources(this.btnAddOneDoor, "btnAddOneDoor");
			this.btnAddOneDoor.BackColor = Color.Transparent;
			this.btnAddOneDoor.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneDoor.ForeColor = Color.White;
			this.btnAddOneDoor.Name = "btnAddOneDoor";
			this.btnAddOneDoor.UseVisualStyleBackColor = false;
			this.btnAddOneDoor.Click += new EventHandler(this.btnAddOneDoor_Click);
			componentResourceManager.ApplyResources(this.btnAddAllDoors, "btnAddAllDoors");
			this.btnAddAllDoors.BackColor = Color.Transparent;
			this.btnAddAllDoors.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllDoors.ForeColor = Color.White;
			this.btnAddAllDoors.Name = "btnAddAllDoors";
			this.btnAddAllDoors.UseVisualStyleBackColor = false;
			this.btnAddAllDoors.Click += new EventHandler(this.btnAddAllDoors_Click);
			componentResourceManager.ApplyResources(this.lblOptional, "lblOptional");
			this.lblOptional.BackColor = Color.Transparent;
			this.lblOptional.ForeColor = Color.White;
			this.lblOptional.Name = "lblOptional";
			componentResourceManager.ApplyResources(this.lblSeleted, "lblSeleted");
			this.lblSeleted.BackColor = Color.Transparent;
			this.lblSeleted.ForeColor = Color.White;
			this.lblSeleted.Name = "lblSeleted";
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
			componentResourceManager.ApplyResources(this.f_ZoneID, "f_ZoneID");
			this.f_ZoneID.Name = "f_ZoneID";
			this.f_ZoneID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column2, "Column2");
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControlSegName1, "f_ControlSegName1");
			this.f_ControlSegName1.Name = "f_ControlSegName1";
			this.f_ControlSegName1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ControllerSN, "ControllerSN");
			this.ControllerSN.Name = "ControllerSN";
			this.ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected2, "f_Selected2");
			this.f_Selected2.Name = "f_Selected2";
			this.f_Selected2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column1, "Column1");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.TimeProfile, "TimeProfile");
			this.TimeProfile.Name = "TimeProfile";
			this.TimeProfile.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControlSegName, "f_ControlSegName");
			this.f_ControlSegName.Name = "f_ControlSegName";
			this.f_ControlSegName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.ControllerSN2, "ControllerSN2");
			this.ControllerSN2.Name = "ControllerSN2";
			this.ControllerSN2.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.lblSeleted);
			base.Controls.Add(this.lblOptional);
			base.Controls.Add(this.cbof_ControlSegID);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.cbof_ZoneID);
			base.Controls.Add(this.label25);
			base.Controls.Add(this.dgvSelectedDoors);
			base.Controls.Add(this.dgvFloors);
			base.Controls.Add(this.btnAddAllDoors);
			base.Controls.Add(this.btnDelAllDoors);
			base.Controls.Add(this.btnAddOneDoor);
			base.Controls.Add(this.btnDelOneDoor);
			base.Name = "dfrmUserFloor";
			base.FormClosing += new FormClosingEventHandler(this.dfrmUserFloor_FormClosing);
			base.Load += new EventHandler(this.dfrmPrivilegeSingle_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmUserFloor_KeyDown);
			((ISupportInitialize)this.dgvSelectedDoors).EndInit();
			((ISupportInitialize)this.dgvFloors).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
