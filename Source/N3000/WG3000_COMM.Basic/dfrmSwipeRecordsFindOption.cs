using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmSwipeRecordsFindOption : frmN3000
	{
		private IContainer components;

		private CheckedListBox chkListDoors;

		private ComboBox cboZone;

		private Label label25;

		private Button btnQuery;

		private Button btnClose;

		private Button btnSelectAll;

		private Button btnSelectNone;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private DataView dvDoors;

		private DataView dvDoors4Watching;

		private DataTable dt;

		private int[] arrAddr;

		private CheckedListBox listViewNotDisplay = new CheckedListBox();

		private dfrmFind dfrmFind1;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmSwipeRecordsFindOption));
			this.chkListDoors = new CheckedListBox();
			this.cboZone = new ComboBox();
			this.label25 = new Label();
			this.btnQuery = new Button();
			this.btnClose = new Button();
			this.btnSelectAll = new Button();
			this.btnSelectNone = new Button();
			base.SuspendLayout();
			this.chkListDoors.CheckOnClick = true;
			this.chkListDoors.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.chkListDoors, "chkListDoors");
			this.chkListDoors.MultiColumn = true;
			this.chkListDoors.Name = "chkListDoors";
			this.cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboZone.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboZone, "cboZone");
			this.cboZone.Name = "cboZone";
			this.cboZone.SelectedIndexChanged += new EventHandler(this.cbof_Zone_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.label25, "label25");
			this.label25.BackColor = Color.Transparent;
			this.label25.ForeColor = Color.White;
			this.label25.Name = "label25";
			this.btnQuery.BackColor = Color.Transparent;
			this.btnQuery.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnQuery, "btnQuery");
			this.btnQuery.ForeColor = Color.White;
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.UseVisualStyleBackColor = false;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.btnSelectAll.BackColor = Color.Transparent;
			this.btnSelectAll.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSelectAll, "btnSelectAll");
			this.btnSelectAll.ForeColor = Color.White;
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.UseVisualStyleBackColor = false;
			this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
			this.btnSelectNone.BackColor = Color.Transparent;
			this.btnSelectNone.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSelectNone, "btnSelectNone");
			this.btnSelectNone.ForeColor = Color.White;
			this.btnSelectNone.Name = "btnSelectNone";
			this.btnSelectNone.UseVisualStyleBackColor = false;
			this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.ControlBox = false;
			base.Controls.Add(this.btnSelectNone);
			base.Controls.Add(this.btnSelectAll);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnQuery);
			base.Controls.Add(this.cboZone);
			base.Controls.Add(this.label25);
			base.Controls.Add(this.chkListDoors);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmSwipeRecordsFindOption";
			base.TopMost = true;
			base.Load += new EventHandler(this.dfrmSwipeRecordsFindOption_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmSwipeRecordsFindOption_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmSwipeRecordsFindOption()
		{
			this.InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Hide();
		}

		private void dfrmSwipeRecordsFindOption_Load(object sender, EventArgs e)
		{
			this.loadZoneInfo();
			this.loadDoorData();
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
			if (wgAppConfig.IsAccessDB)
			{
				this.loadDoorData_Acc();
				return;
			}
			string text = " SELECT c.*,b.f_ControllerSN ,  b.f_ZoneID ";
			text += " FROM t_b_Controller b, t_b_reader c WHERE c.f_ControllerID = b.f_ControllerID ";
			text += " ORDER BY  c.f_ReaderID ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						this.dt = new DataTable();
						this.dvDoors = new DataView(this.dt);
						this.dvDoors4Watching = new DataView(this.dt);
						sqlDataAdapter.Fill(this.dt);
						icControllerZone icControllerZone = new icControllerZone();
						icControllerZone.getAllowedControllers(ref this.dt);
						if (this.dvDoors.Count > 0)
						{
							this.arrAddr = new int[this.dvDoors.Count + 1];
							for (int i = 0; i < this.dvDoors.Count; i++)
							{
								string item = wgTools.SetObjToStr(this.dvDoors[i]["f_ReaderName"]);
								this.chkListDoors.Items.Add(item);
								this.arrAddr[i] = (int)this.dvDoors[i]["f_ReaderID"];
							}
						}
					}
				}
			}
		}

		private void loadDoorData_Acc()
		{
			string text = " SELECT c.*,b.f_ControllerSN ,  b.f_ZoneID ";
			text += " FROM t_b_Controller b, t_b_reader c WHERE c.f_ControllerID = b.f_ControllerID ";
			text += " ORDER BY  c.f_ReaderID ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						this.dt = new DataTable();
						this.dvDoors = new DataView(this.dt);
						this.dvDoors4Watching = new DataView(this.dt);
						oleDbDataAdapter.Fill(this.dt);
						icControllerZone icControllerZone = new icControllerZone();
						icControllerZone.getAllowedControllers(ref this.dt);
						if (this.dvDoors.Count > 0)
						{
							this.arrAddr = new int[this.dvDoors.Count + 1];
							for (int i = 0; i < this.dvDoors.Count; i++)
							{
								string item = wgTools.SetObjToStr(this.dvDoors[i]["f_ReaderName"]);
								this.chkListDoors.Items.Add(item);
								this.arrAddr[i] = (int)this.dvDoors[i]["f_ReaderID"];
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
				DataView dataView = this.dvDoors;
				if (this.cboZone.SelectedIndex < 0 || (this.cboZone.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
				{
					dataView.RowFilter = "";
				}
				else
				{
					dataView.RowFilter = "f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
					string arg = " f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
					int num = (int)this.arrZoneID[this.cboZone.SelectedIndex];
					int num2 = (int)this.arrZoneNO[this.cboZone.SelectedIndex];
					int zoneChildMaxNo = icControllerZone.getZoneChildMaxNo(this.cboZone.Text, this.arrZoneName, this.arrZoneNO);
					if (num2 > 0)
					{
						if (num2 >= zoneChildMaxNo)
						{
							dataView.RowFilter = string.Format(" f_ZoneID ={0:d} ", num);
							arg = string.Format(" f_ZoneID ={0:d} ", num);
						}
						else
						{
							dataView.RowFilter = "";
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
							dataView.RowFilter = string.Format("  {0} ", text);
							arg = string.Format("  {0} ", text);
						}
					}
					dataView.RowFilter = string.Format(" {0} ", arg);
				}
				this.chkListDoors.Items.Clear();
				if (this.dvDoors.Count > 0)
				{
					for (int j = 0; j < this.dvDoors.Count; j++)
					{
						this.arrAddr[j] = (int)this.dvDoors[j]["f_ReaderID"];
						this.chkListDoors.Items.Add(wgTools.SetObjToStr(this.dvDoors[j]["f_ReaderName"]));
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
			string text = "-1";
			if (this.chkListDoors.CheckedItems.Count != 0)
			{
				for (int i = 0; i < this.chkListDoors.Items.Count; i++)
				{
					if (this.chkListDoors.GetItemChecked(i))
					{
						text = text + "," + this.arrAddr[i].ToString();
					}
				}
			}
			return text;
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			if (base.Owner != null)
			{
				(base.Owner as frmSwipeRecords).btnQuery_Click(null, null);
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

		private void dfrmSwipeRecordsFindOption_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(this.chkListDoors, this);
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}
	}
}
