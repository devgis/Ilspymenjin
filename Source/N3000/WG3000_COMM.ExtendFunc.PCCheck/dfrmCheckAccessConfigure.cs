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

namespace WG3000_COMM.ExtendFunc.PCCheck
{
	public class dfrmCheckAccessConfigure : frmN3000
	{
		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private DataSet dsCheckAccess = new DataSet();

		private DataView dvCheckAccess;

		private string strSelectedDoors = "";

		private DataView dv;

		private DataView dvSelected;

		private DataTable dt;

		private DataView dvtmp;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private string strZoneFilter = "";

		private dfrmFind dfrmFind1;

		private IContainer components;

		private ComboBox cbof_GroupID;

		private Label label4;

		private DataGridView dgvGroups;

		private Label label1;

		private Button btnCancel;

		private Button btnEdit;

		private Button btnOK;

		private DataGridViewTextBoxColumn f_GroupID;

		private DataGridViewTextBoxColumn GroupName;

		private DataGridViewCheckBoxColumn f_active;

		private DataGridViewTextBoxColumn MoreCards;

		private DataGridViewTextBoxColumn f_SoundFile;

		private GroupBox groupBox2;

		private ComboBox cbof_ZoneID;

		private Label label25;

		private DataGridView dgvSelectedDoors;

		private DataGridView dgvDoors;

		private Button btnDelAllDoors;

		private Button btnDelOneDoor;

		private Button btnAddOneDoor;

		private Button btnAddAllDoors;

		private Label label2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn f_Selected2;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn f_Selected;

		private DataGridViewTextBoxColumn f_ZoneID;

		private Button btnOption;

		public dfrmCheckAccessConfigure()
		{
			this.InitializeComponent();
		}

		private void dfrmCheckAccessSetup_Load(object sender, EventArgs e)
		{
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.GroupName.HeaderText = wgAppConfig.ReplaceFloorRomm(this.GroupName.HeaderText);
			this.label1.Text = wgAppConfig.ReplaceFloorRomm(this.label1.Text);
			try
			{
				icGroup icGroup = new icGroup();
				icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
				for (int i = 0; i < this.arrGroupID.Count; i++)
				{
					if (i == 0 && string.IsNullOrEmpty(this.arrGroupName[i].ToString()))
					{
						this.cbof_GroupID.Items.Add("");
					}
					else
					{
						this.cbof_GroupID.Items.Add(this.arrGroupName[i].ToString());
					}
				}
				if (this.cbof_GroupID.Items.Count > 0)
				{
					this.cbof_GroupID.SelectedIndex = 0;
				}
				string cmdText = " SELECT a.f_GroupID,a.f_GroupName,b.f_CheckAccessActive,b.f_MoreCards, b.f_SoundFileName,b.f_GroupType from t_b_Group a LEFT JOIN t_b_group4PCCheckAccess b ON a.f_GroupID = b.f_GroupID order by f_GroupName ASC";
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
						{
							using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
							{
								oleDbDataAdapter.Fill(this.dsCheckAccess, "groups");
							}
						}
						goto IL_1BB;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlDataAdapter.Fill(this.dsCheckAccess, "groups");
						}
					}
				}
				IL_1BB:
				this.dvCheckAccess = new DataView(this.dsCheckAccess.Tables["groups"]);
				for (int j = 0; j <= this.dvCheckAccess.Count - 1; j++)
				{
					if (string.IsNullOrEmpty(this.dvCheckAccess[j]["f_GroupType"].ToString()))
					{
						this.dvCheckAccess[j]["f_GroupType"] = 0;
						this.dvCheckAccess[j]["f_CheckAccessActive"] = 0;
						this.dvCheckAccess[j]["f_MoreCards"] = 1;
						this.dvCheckAccess[j]["f_SoundFileName"] = "";
					}
				}
				this.dvCheckAccess.RowFilter = "f_GroupType = 1";
				if (this.dvCheckAccess.Count > 0)
				{
					int num = this.cbof_GroupID.Items.IndexOf(this.dvCheckAccess[0]["f_GroupName"].ToString());
					if (num > 0)
					{
						this.strSelectedDoors = wgTools.SetObjToStr(this.dvCheckAccess[0]["f_SoundFileName"]);
						this.dvCheckAccess[0]["f_SoundFileName"] = "";
						this.cbof_GroupID.Text = this.dvCheckAccess[0]["f_GroupName"].ToString();
						if (!string.IsNullOrEmpty(this.strSelectedDoors))
						{
							this.btnOption.Enabled = false;
							base.Size = new Size(808, 604);
						}
					}
				}
				try
				{
					DataColumn[] primaryKey = new DataColumn[]
					{
						this.dsCheckAccess.Tables["groups"].Columns[0]
					};
					this.dsCheckAccess.Tables["groups"].PrimaryKey = primaryKey;
				}
				catch (Exception ex)
				{
					wgAppConfig.wgLog(ex.ToString());
				}
				this.cbof_GroupID_SelectedIndexChanged(null, null);
				this.dgvGroups.AutoGenerateColumns = false;
				this.dgvGroups.DataSource = this.dvCheckAccess;
				int num2 = 0;
				while (num2 < this.dvCheckAccess.Table.Columns.Count && num2 < this.dgvGroups.ColumnCount)
				{
					this.dgvGroups.Columns[num2].DataPropertyName = this.dvCheckAccess.Table.Columns[num2].ColumnName;
					num2++;
				}
			}
			catch (Exception ex2)
			{
				wgAppConfig.wgLog(ex2.ToString());
			}
			this.dgvGroups.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.loadZoneInfo();
			this.loadDoorData();
			this.loadPrivilegeData();
			this.dgvDoors.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.dgvSelectedDoors.DefaultCellStyle.ForeColor = SystemColors.WindowText;
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
			if (wgAppConfig.IsAccessDB)
			{
				this.loadDoorData_Acc();
				return;
			}
			string text = " SELECT a.f_DoorID, a.f_DoorName , 0 as f_Selected, b.f_ZoneID, 1 as f_ControlSegID,' ' as f_ControlSegName, a.f_ControllerID, a.f_DoorNO,b.f_ControllerSN ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						this.dt = new DataTable();
						this.dv = new DataView(this.dt);
						this.dvSelected = new DataView(this.dt);
						sqlDataAdapter.Fill(this.dt);
						try
						{
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
						this.dgvDoors.AutoGenerateColumns = false;
						this.dgvDoors.DataSource = this.dv;
						this.dgvSelectedDoors.AutoGenerateColumns = false;
						this.dgvSelectedDoors.DataSource = this.dvSelected;
						for (int i = 0; i < this.dgvDoors.Columns.Count; i++)
						{
							this.dgvDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
							this.dgvSelectedDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
						}
					}
				}
			}
		}

		private void loadDoorData_Acc()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , 0 as f_Selected, b.f_ZoneID, 1 as f_ControlSegID,' ' as f_ControlSegName, a.f_ControllerID, a.f_DoorNO,b.f_ControllerSN ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						this.dt = new DataTable();
						this.dv = new DataView(this.dt);
						this.dvSelected = new DataView(this.dt);
						oleDbDataAdapter.Fill(this.dt);
						try
						{
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
						this.dgvDoors.AutoGenerateColumns = false;
						this.dgvDoors.DataSource = this.dv;
						this.dgvSelectedDoors.AutoGenerateColumns = false;
						this.dgvSelectedDoors.DataSource = this.dvSelected;
						for (int i = 0; i < this.dgvDoors.Columns.Count; i++)
						{
							this.dgvDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
							this.dgvSelectedDoors.Columns[i].DataPropertyName = this.dt.Columns[i].ColumnName;
						}
					}
				}
			}
		}

		private void loadPrivilegeData()
		{
			Cursor.Current = Cursors.WaitCursor;
			wgTools.WriteLine("loadPrivilegeData Start");
			if (string.IsNullOrEmpty(this.strSelectedDoors))
			{
				return;
			}
			string[] array = this.strSelectedDoors.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				DataTable table = ((DataView)this.dgvDoors.DataSource).Table;
				for (int i = 0; i < array.Length; i++)
				{
					for (int j = 0; j < table.Rows.Count; j++)
					{
						if (int.Parse(array[i]) == (int)table.Rows[j]["f_DoorID"])
						{
							table.Rows[j]["f_Selected"] = 1;
							break;
						}
					}
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private void cbof_GroupID_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dvCheckAccess != null)
				{
					if (string.IsNullOrEmpty(this.cbof_GroupID.Text))
					{
						this.dvCheckAccess.RowFilter = "";
					}
					else
					{
						this.dvCheckAccess.RowFilter = string.Format("f_GroupName <> '{0}'", this.cbof_GroupID.Text);
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				if (this.dvSelected.Count > 0)
				{
					for (int i = 0; i < this.dvSelected.Count; i++)
					{
						if (i != 0)
						{
							text += ",";
						}
						text += string.Format("{0:d}", this.dvSelected[i]["f_DoorID"]);
					}
				}
				string text2 = "DELETE from t_b_group4PCCheckAccess";
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(text2, oleDbConnection))
						{
							oleDbConnection.Open();
							oleDbCommand.ExecuteNonQuery();
							for (int j = 0; j <= this.dvCheckAccess.Count - 1; j++)
							{
								text2 = "INSERT INTO t_b_group4PCCheckAccess (f_GroupID,f_GroupType,f_CheckAccessActive,f_MoreCards,f_SoundFileName)";
								text2 = text2 + " VALUES( " + this.dvCheckAccess[j]["f_GroupID"].ToString();
								text2 = text2 + " ," + 0;
								text2 = text2 + " ," + this.dvCheckAccess[j]["f_CheckAccessActive"].ToString();
								text2 = text2 + " ," + this.dvCheckAccess[j]["f_MoreCards"].ToString();
								text2 = text2 + " ," + wgTools.PrepareStr(this.dvCheckAccess[j]["f_SoundFileName"].ToString());
								text2 += ")";
								oleDbCommand.CommandText = text2;
								oleDbCommand.ExecuteNonQuery();
							}
							if (!string.IsNullOrEmpty(this.cbof_GroupID.Text))
							{
								text2 = "INSERT INTO t_b_group4PCCheckAccess (f_GroupID,f_GroupType,f_CheckAccessActive,f_MoreCards,f_SoundFileName)";
								text2 = text2 + " VALUES( " + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
								text2 = text2 + " ," + 1;
								text2 = text2 + " ," + 0;
								text2 = text2 + " ," + 1;
								text2 = text2 + " ," + wgTools.PrepareStr(text);
								text2 += ")";
								oleDbCommand.CommandText = text2;
								oleDbCommand.ExecuteNonQuery();
							}
						}
						goto IL_428;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text2, sqlConnection))
					{
						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
						for (int k = 0; k <= this.dvCheckAccess.Count - 1; k++)
						{
							text2 = "INSERT INTO t_b_group4PCCheckAccess (f_GroupID,f_GroupType,f_CheckAccessActive,f_MoreCards,f_SoundFileName)";
							text2 = text2 + " VALUES( " + this.dvCheckAccess[k]["f_GroupID"].ToString();
							text2 = text2 + " ," + 0;
							text2 = text2 + " ," + this.dvCheckAccess[k]["f_CheckAccessActive"].ToString();
							text2 = text2 + " ," + this.dvCheckAccess[k]["f_MoreCards"].ToString();
							text2 = text2 + " ," + wgTools.PrepareStr(this.dvCheckAccess[k]["f_SoundFileName"].ToString());
							text2 += ")";
							sqlCommand.CommandText = text2;
							sqlCommand.ExecuteNonQuery();
						}
						if (!string.IsNullOrEmpty(this.cbof_GroupID.Text))
						{
							text2 = "INSERT INTO t_b_group4PCCheckAccess (f_GroupID,f_GroupType,f_CheckAccessActive,f_MoreCards,f_SoundFileName)";
							text2 = text2 + " VALUES( " + this.arrGroupID[this.cbof_GroupID.SelectedIndex];
							text2 = text2 + " ," + 1;
							text2 = text2 + " ," + 0;
							text2 = text2 + " ," + 1;
							text2 = text2 + " ," + wgTools.PrepareStr(text);
							text2 += ")";
							sqlCommand.CommandText = text2;
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
				IL_428:
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				DataGridView dataGridView = this.dgvGroups;
				int index;
				if (dataGridView.SelectedRows.Count <= 0)
				{
					if (dataGridView.SelectedCells.Count <= 0)
					{
						return;
					}
					index = dataGridView.SelectedCells[0].RowIndex;
				}
				else
				{
					index = dataGridView.SelectedRows[0].Index;
				}
				DataTable table = ((DataView)dataGridView.DataSource).Table;
				int num = (int)dataGridView.Rows[index].Cells[0].Value;
				DataRow dataRow = table.Rows.Find(num);
				if (dataRow != null)
				{
					using (dfrmCheckAccessSetup dfrmCheckAccessSetup = new dfrmCheckAccessSetup())
					{
						dfrmCheckAccessSetup.groupname = dataRow["f_GroupName"].ToString();
						dfrmCheckAccessSetup.soundfilename = dataRow["f_SoundFileName"].ToString();
						dfrmCheckAccessSetup.active = (int)dataRow["f_CheckAccessActive"];
						dfrmCheckAccessSetup.morecards = (int)dataRow["f_MoreCards"];
						if (dfrmCheckAccessSetup.ShowDialog(this) == DialogResult.OK)
						{
							dataRow["f_SoundFileName"] = dfrmCheckAccessSetup.soundfilename;
							dataRow["f_CheckAccessActive"] = dfrmCheckAccessSetup.active;
							dataRow["f_MoreCards"] = dfrmCheckAccessSetup.morecards;
							this.dsCheckAccess.Tables["groups"].AcceptChanges();
							this.Refresh();
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dgvGroups_DoubleClick(object sender, EventArgs e)
		{
			this.btnEdit.PerformClick();
		}

		private void btnAddAllDoors_Click(object sender, EventArgs e)
		{
			this.dt = ((DataView)this.dgvDoors.DataSource).Table;
			if (this.cbof_ZoneID.SelectedIndex <= 0 && this.cbof_ZoneID.Text == CommonStr.strAllZones)
			{
				for (int i = 0; i < this.dt.Rows.Count; i++)
				{
					this.dt.Rows[i]["f_Selected"] = 1;
				}
				return;
			}
			if (this.cbof_ZoneID.SelectedIndex >= 0)
			{
				this.dvtmp = new DataView((this.dgvDoors.DataSource as DataView).Table);
				this.dvtmp.RowFilter = string.Format("  {0} ", this.strZoneFilter);
				for (int j = 0; j < this.dvtmp.Count; j++)
				{
					this.dvtmp[j]["f_Selected"] = 1;
				}
			}
		}

		private void btnAddOneDoor_Click(object sender, EventArgs e)
		{
			wgAppConfig.selectObject(this.dgvDoors);
		}

		private void btnDelOneDoor_Click(object sender, EventArgs e)
		{
			wgAppConfig.deselectObject(this.dgvSelectedDoors);
		}

		private void btnDelAllDoors_Click(object sender, EventArgs e)
		{
			this.dt = ((DataView)this.dgvSelectedDoors.DataSource).Table;
			for (int i = 0; i < this.dt.Rows.Count; i++)
			{
				this.dt.Rows[i]["f_Selected"] = 0;
			}
		}

		private void cbof_Zone_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dgvDoors.DataSource != null)
			{
				DataView dataView = (DataView)this.dgvDoors.DataSource;
				if (this.cbof_ZoneID.SelectedIndex < 0 || (this.cbof_ZoneID.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
				{
					dataView.RowFilter = "f_Selected = 0";
					this.strZoneFilter = "";
					return;
				}
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
		}

		private void dgvDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnAddOneDoor.PerformClick();
		}

		private void dgvSelectedDoors_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.btnDelOneDoor.PerformClick();
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
			base.Size = new Size(808, 604);
			this.btnOption.Enabled = false;
		}

		private void dfrmCheckAccessConfigure_KeyDown(object sender, KeyEventArgs e)
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

		private void dfrmCheckAccessConfigure_FormClosing(object sender, FormClosingEventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmCheckAccessConfigure));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
			this.btnCancel = new Button();
			this.groupBox2 = new GroupBox();
			this.label2 = new Label();
			this.cbof_ZoneID = new ComboBox();
			this.label25 = new Label();
			this.dgvSelectedDoors = new DataGridView();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.f_Selected2 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.dgvDoors = new DataGridView();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.f_Selected = new DataGridViewTextBoxColumn();
			this.f_ZoneID = new DataGridViewTextBoxColumn();
			this.btnDelAllDoors = new Button();
			this.btnDelOneDoor = new Button();
			this.btnAddOneDoor = new Button();
			this.btnAddAllDoors = new Button();
			this.btnOK = new Button();
			this.btnOption = new Button();
			this.btnEdit = new Button();
			this.dgvGroups = new DataGridView();
			this.f_GroupID = new DataGridViewTextBoxColumn();
			this.GroupName = new DataGridViewTextBoxColumn();
			this.f_active = new DataGridViewCheckBoxColumn();
			this.MoreCards = new DataGridViewTextBoxColumn();
			this.f_SoundFile = new DataGridViewTextBoxColumn();
			this.cbof_GroupID = new ComboBox();
			this.label1 = new Label();
			this.label4 = new Label();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.dgvSelectedDoors).BeginInit();
			((ISupportInitialize)this.dgvDoors).BeginInit();
			((ISupportInitialize)this.dgvGroups).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.cbof_ZoneID);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.dgvSelectedDoors);
			this.groupBox2.Controls.Add(this.dgvDoors);
			this.groupBox2.Controls.Add(this.btnDelAllDoors);
			this.groupBox2.Controls.Add(this.btnDelOneDoor);
			this.groupBox2.Controls.Add(this.btnAddOneDoor);
			this.groupBox2.Controls.Add(this.btnAddAllDoors);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.cbof_ZoneID, "cbof_ZoneID");
			this.cbof_ZoneID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ZoneID.FormattingEnabled = true;
			this.cbof_ZoneID.Name = "cbof_ZoneID";
			this.cbof_ZoneID.SelectedIndexChanged += new EventHandler(this.cbof_Zone_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label25, "label25");
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
				this.Column1
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
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
			this.dgvSelectedDoors.MouseDoubleClick += new MouseEventHandler(this.dgvSelectedDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected2, "f_Selected2");
			this.f_Selected2.Name = "f_Selected2";
			this.f_Selected2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Column1, "Column1");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dgvDoors, "dgvDoors");
			this.dgvDoors.AllowUserToAddRows = false;
			this.dgvDoors.AllowUserToDeleteRows = false;
			this.dgvDoors.AllowUserToOrderColumns = true;
			this.dgvDoors.BackgroundColor = Color.White;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = Color.White;
			dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.dgvDoors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvDoors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvDoors.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn6,
				this.dataGridViewTextBoxColumn7,
				this.f_Selected,
				this.f_ZoneID
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Window;
			dataGridViewCellStyle5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.dgvDoors.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvDoors.EnableHeadersVisualStyles = false;
			this.dgvDoors.Name = "dgvDoors";
			this.dgvDoors.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvDoors.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvDoors.RowTemplate.Height = 23;
			this.dgvDoors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvDoors.MouseDoubleClick += new MouseEventHandler(this.dgvDoors_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Selected, "f_Selected");
			this.f_Selected.Name = "f_Selected";
			this.f_Selected.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ZoneID, "f_ZoneID");
			this.f_ZoneID.Name = "f_ZoneID";
			this.f_ZoneID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnDelAllDoors, "btnDelAllDoors");
			this.btnDelAllDoors.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelAllDoors.Name = "btnDelAllDoors";
			this.btnDelAllDoors.UseVisualStyleBackColor = true;
			this.btnDelAllDoors.Click += new EventHandler(this.btnDelAllDoors_Click);
			componentResourceManager.ApplyResources(this.btnDelOneDoor, "btnDelOneDoor");
			this.btnDelOneDoor.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelOneDoor.Name = "btnDelOneDoor";
			this.btnDelOneDoor.UseVisualStyleBackColor = true;
			this.btnDelOneDoor.Click += new EventHandler(this.btnDelOneDoor_Click);
			componentResourceManager.ApplyResources(this.btnAddOneDoor, "btnAddOneDoor");
			this.btnAddOneDoor.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneDoor.Name = "btnAddOneDoor";
			this.btnAddOneDoor.UseVisualStyleBackColor = true;
			this.btnAddOneDoor.Click += new EventHandler(this.btnAddOneDoor_Click);
			componentResourceManager.ApplyResources(this.btnAddAllDoors, "btnAddAllDoors");
			this.btnAddAllDoors.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllDoors.Name = "btnAddAllDoors";
			this.btnAddAllDoors.UseVisualStyleBackColor = true;
			this.btnAddAllDoors.Click += new EventHandler(this.btnAddAllDoors_Click);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnOption, "btnOption");
			this.btnOption.BackColor = Color.Transparent;
			this.btnOption.BackgroundImage = Resources.pMain_button_normal;
			this.btnOption.ForeColor = Color.White;
			this.btnOption.Name = "btnOption";
			this.btnOption.UseVisualStyleBackColor = false;
			this.btnOption.Click += new EventHandler(this.btnOption_Click);
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.BackColor = Color.Transparent;
			this.btnEdit.BackgroundImage = Resources.pMain_button_normal;
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			componentResourceManager.ApplyResources(this.dgvGroups, "dgvGroups");
			this.dgvGroups.AllowUserToAddRows = false;
			this.dgvGroups.AllowUserToDeleteRows = false;
			this.dgvGroups.AllowUserToOrderColumns = true;
			this.dgvGroups.BackgroundColor = Color.White;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle7.ForeColor = Color.White;
			dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
			this.dgvGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvGroups.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_GroupID,
				this.GroupName,
				this.f_active,
				this.MoreCards,
				this.f_SoundFile
			});
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = SystemColors.Window;
			dataGridViewCellStyle8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle8.ForeColor = Color.White;
			dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
			this.dgvGroups.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgvGroups.EnableHeadersVisualStyles = false;
			this.dgvGroups.MultiSelect = false;
			this.dgvGroups.Name = "dgvGroups";
			this.dgvGroups.ReadOnly = true;
			dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = SystemColors.Control;
			dataGridViewCellStyle9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
			this.dgvGroups.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvGroups.RowTemplate.Height = 23;
			this.dgvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvGroups.DoubleClick += new EventHandler(this.dgvGroups_DoubleClick);
			componentResourceManager.ApplyResources(this.f_GroupID, "f_GroupID");
			this.f_GroupID.Name = "f_GroupID";
			this.f_GroupID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.GroupName, "GroupName");
			this.GroupName.Name = "GroupName";
			this.GroupName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_active, "f_active");
			this.f_active.Name = "f_active";
			this.f_active.ReadOnly = true;
			componentResourceManager.ApplyResources(this.MoreCards, "MoreCards");
			this.MoreCards.Name = "MoreCards";
			this.MoreCards.ReadOnly = true;
			this.f_SoundFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_SoundFile, "f_SoundFile");
			this.f_SoundFile.Name = "f_SoundFile";
			this.f_SoundFile.ReadOnly = true;
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			this.cbof_GroupID.Name = "cbof_GroupID";
			this.cbof_GroupID.SelectedIndexChanged += new EventHandler(this.cbof_GroupID_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.BackColor = Color.Transparent;
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnOption);
			base.Controls.Add(this.btnEdit);
			base.Controls.Add(this.dgvGroups);
			base.Controls.Add(this.cbof_GroupID);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label4);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmCheckAccessConfigure";
			base.FormClosing += new FormClosingEventHandler(this.dfrmCheckAccessConfigure_FormClosing);
			base.Load += new EventHandler(this.dfrmCheckAccessSetup_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmCheckAccessConfigure_KeyDown);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.dgvSelectedDoors).EndInit();
			((ISupportInitialize)this.dgvDoors).EndInit();
			((ISupportInitialize)this.dgvGroups).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
