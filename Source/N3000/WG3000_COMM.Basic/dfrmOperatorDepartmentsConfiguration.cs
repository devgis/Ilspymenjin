using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmOperatorDepartmentsConfiguration : frmN3000
	{
		private Container components;

		internal Label Label11;

		internal Label Label10;

		internal ListBox lstSelectedGroups;

		private Button btnDeleteAllGroups;

		private Button btnDeleteOneGroup;

		private Button btnAddOneGroup;

		private Button btnAddAllGroups;

		internal Button button1;

		internal Button button2;

		internal ListBox lstOptionalGroups;

		private DataTable dtOptionalGroups;

		private DataTable dtSelectedGroups;

		private DataSet ds;

		private SqlDataAdapter daOptionalGroup;

		private SqlDataAdapter daSelectedGroup;

		public int operatorId;

		public dfrmOperatorDepartmentsConfiguration()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOperatorDepartmentsConfiguration));
			this.button1 = new Button();
			this.button2 = new Button();
			this.btnDeleteAllGroups = new Button();
			this.btnDeleteOneGroup = new Button();
			this.btnAddOneGroup = new Button();
			this.btnAddAllGroups = new Button();
			this.Label11 = new Label();
			this.Label10 = new Label();
			this.lstSelectedGroups = new ListBox();
			this.lstOptionalGroups = new ListBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.button1, "button1");
			this.button1.BackColor = Color.Transparent;
			this.button1.BackgroundImage = Resources.pMain_button_normal;
			this.button1.ForeColor = Color.White;
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.btnOk_Click);
			componentResourceManager.ApplyResources(this.button2, "button2");
			this.button2.BackColor = Color.Transparent;
			this.button2.BackgroundImage = Resources.pMain_button_normal;
			this.button2.DialogResult = DialogResult.Cancel;
			this.button2.ForeColor = Color.White;
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.btnDeleteAllGroups, "btnDeleteAllGroups");
			this.btnDeleteAllGroups.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeleteAllGroups.ForeColor = Color.White;
			this.btnDeleteAllGroups.Name = "btnDeleteAllGroups";
			this.btnDeleteAllGroups.UseVisualStyleBackColor = true;
			this.btnDeleteAllGroups.Click += new EventHandler(this.btnDeleteAllGroups_Click);
			componentResourceManager.ApplyResources(this.btnDeleteOneGroup, "btnDeleteOneGroup");
			this.btnDeleteOneGroup.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeleteOneGroup.ForeColor = Color.White;
			this.btnDeleteOneGroup.Name = "btnDeleteOneGroup";
			this.btnDeleteOneGroup.UseVisualStyleBackColor = true;
			this.btnDeleteOneGroup.Click += new EventHandler(this.btnDeleteOneGroup_Click);
			componentResourceManager.ApplyResources(this.btnAddOneGroup, "btnAddOneGroup");
			this.btnAddOneGroup.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddOneGroup.ForeColor = Color.White;
			this.btnAddOneGroup.Name = "btnAddOneGroup";
			this.btnAddOneGroup.UseVisualStyleBackColor = true;
			this.btnAddOneGroup.Click += new EventHandler(this.btnAddOneGroup_Click);
			componentResourceManager.ApplyResources(this.btnAddAllGroups, "btnAddAllGroups");
			this.btnAddAllGroups.BackgroundImage = Resources.pMain_button_normal;
			this.btnAddAllGroups.ForeColor = Color.White;
			this.btnAddAllGroups.Name = "btnAddAllGroups";
			this.btnAddAllGroups.UseVisualStyleBackColor = true;
			this.btnAddAllGroups.Click += new EventHandler(this.btnAddAllGroups_Click);
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.ForeColor = Color.White;
			this.Label11.Name = "Label11";
			componentResourceManager.ApplyResources(this.Label10, "Label10");
			this.Label10.ForeColor = Color.White;
			this.Label10.Name = "Label10";
			componentResourceManager.ApplyResources(this.lstSelectedGroups, "lstSelectedGroups");
			this.lstSelectedGroups.Name = "lstSelectedGroups";
			this.lstSelectedGroups.DoubleClick += new EventHandler(this.btnDeleteOneGroup_Click);
			componentResourceManager.ApplyResources(this.lstOptionalGroups, "lstOptionalGroups");
			this.lstOptionalGroups.Name = "lstOptionalGroups";
			this.lstOptionalGroups.DoubleClick += new EventHandler(this.btnAddOneGroup_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.btnDeleteAllGroups);
			base.Controls.Add(this.btnDeleteOneGroup);
			base.Controls.Add(this.btnAddOneGroup);
			base.Controls.Add(this.btnAddAllGroups);
			base.Controls.Add(this.Label11);
			base.Controls.Add(this.Label10);
			base.Controls.Add(this.lstSelectedGroups);
			base.Controls.Add(this.lstOptionalGroups);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmOperatorDepartmentsConfiguration";
			base.Load += new EventHandler(this.dfrmSwitchGroupsConfiguration_Load);
			base.ResumeLayout(false);
		}

		private void _dataTableLoad()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this._dataTableLoad_Acc();
				return;
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("Select * from t_b_Group where f_GroupID IN (SELECT f_GroupID FROM t_b_Group4Operator Where f_OperatorId = " + this.operatorId.ToString() + " ) ", sqlConnection))
				{
					using (SqlCommand sqlCommand2 = new SqlCommand("Select * from t_b_Group where f_GroupID NOT IN (SELECT f_GroupID FROM  t_b_Group4Operator Where f_OperatorId = " + this.operatorId.ToString() + " ) ", sqlConnection))
					{
						this.ds = new DataSet("Users-Doors");
						this.daSelectedGroup = new SqlDataAdapter(sqlCommand);
						this.daOptionalGroup = new SqlDataAdapter(sqlCommand2);
						try
						{
							this.ds.Clear();
							this.daOptionalGroup.Fill(this.ds, "OptionalGroups");
							this.daSelectedGroup.Fill(this.ds, "SelectedGroups");
							this.dtOptionalGroups = new DataTable();
							this.dtOptionalGroups = this.ds.Tables["OptionalGroups"].Copy();
							this.dtSelectedGroups = new DataTable();
							this.dtSelectedGroups = this.ds.Tables["SelectedGroups"].Copy();
							this.lstOptionalGroups.DataSource = this.dtOptionalGroups;
							this.lstOptionalGroups.DisplayMember = "f_GroupName";
							this.lstSelectedGroups.DataSource = this.dtSelectedGroups;
							this.lstSelectedGroups.DisplayMember = "f_GroupName";
							this.dtSelectedGroups.AcceptChanges();
							this.dtOptionalGroups.AcceptChanges();
						}
						catch (Exception ex)
						{
							wgTools.WgDebugWrite(ex.ToString(), new object[]
							{
								EventLogEntryType.Error
							});
						}
					}
				}
			}
		}

		private void _dataTableLoad_Acc()
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("Select * from t_b_Group where f_GroupID IN (SELECT f_GroupID FROM t_b_Group4Operator Where f_OperatorId = " + this.operatorId.ToString() + " ) ", oleDbConnection))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand("Select * from t_b_Group where f_GroupID NOT IN (SELECT f_GroupID FROM  t_b_Group4Operator Where f_OperatorId = " + this.operatorId.ToString() + " ) ", oleDbConnection))
					{
						this.ds = new DataSet("Users-Doors");
						OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
						OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(oleDbCommand2);
						try
						{
							this.ds.Clear();
							oleDbDataAdapter2.Fill(this.ds, "OptionalGroups");
							oleDbDataAdapter.Fill(this.ds, "SelectedGroups");
							this.dtOptionalGroups = new DataTable();
							this.dtOptionalGroups = this.ds.Tables["OptionalGroups"].Copy();
							this.dtSelectedGroups = new DataTable();
							this.dtSelectedGroups = this.ds.Tables["SelectedGroups"].Copy();
							this.lstOptionalGroups.DataSource = this.dtOptionalGroups;
							this.lstOptionalGroups.DisplayMember = "f_GroupName";
							this.lstSelectedGroups.DataSource = this.dtSelectedGroups;
							this.lstSelectedGroups.DisplayMember = "f_GroupName";
							this.dtSelectedGroups.AcceptChanges();
							this.dtOptionalGroups.AcceptChanges();
						}
						catch (Exception ex)
						{
							wgTools.WgDebugWrite(ex.ToString(), new object[]
							{
								EventLogEntryType.Error
							});
						}
					}
				}
			}
		}

		private void _bindGroup()
		{
			try
			{
				this.lstOptionalGroups.DisplayMember = "f_GroupName";
				this.lstOptionalGroups.DataSource = this.dtOptionalGroups;
				this.lstSelectedGroups.DisplayMember = "f_GroupName";
				this.lstSelectedGroups.DataSource = this.dtSelectedGroups;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void _unbindGroup()
		{
			try
			{
				this.lstOptionalGroups.DataSource = null;
				this.lstOptionalGroups.DisplayMember = null;
				this.lstSelectedGroups.DataSource = null;
				this.lstSelectedGroups.DisplayMember = null;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnAddAllGroups_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor current = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				this._unbindGroup();
				DataTable dataTable = this.dtOptionalGroups;
				DataTable dataTable2 = this.dtSelectedGroups;
				for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
				{
					dataTable2.ImportRow(dataTable.Rows[i]);
				}
				dataTable.Clear();
				dataTable2.AcceptChanges();
				dataTable.AcceptChanges();
				this.lstSelectedGroups.Refresh();
				this.lstOptionalGroups.Refresh();
				this._bindGroup();
				Cursor.Current = current;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		public static void lst_UpdateOne(DataTable dtSource, DataTable dtDestine, ListBox lstSrc, ListBox lstDest)
		{
			try
			{
				object dataSource = lstDest.DataSource;
				string displayMember = lstDest.DisplayMember;
				lstDest.DisplayMember = null;
				lstDest.DataSource = null;
				Cursor current = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					if (lstSrc.SelectedIndices.Count > 0)
					{
						DataTable dataTable = dtDestine.Copy();
						dataTable.Rows.Clear();
						int num = lstSrc.SelectedIndices.Count - 1;
						int[] array = new int[num + 1];
						for (int i = 0; i <= num; i++)
						{
							array[i] = lstSrc.SelectedIndices[num - i];
						}
						for (int i = 0; i <= num; i++)
						{
							int num2 = array[i];
							if (num2 >= 0)
							{
								DataRow row = dtSource.Rows[num2];
								dataTable.ImportRow(row);
								dtSource.Rows.Remove(row);
								dtSource.AcceptChanges();
							}
						}
						dataTable.AcceptChanges();
						for (int i = 0; i <= num; i++)
						{
							dtDestine.ImportRow(dataTable.Rows[num - i]);
						}
						dtSource.AcceptChanges();
						dtDestine.AcceptChanges();
						lstSrc.Refresh();
						lstDest.Refresh();
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[]
					{
						EventLogEntryType.Error
					});
				}
				lstDest.DisplayMember = displayMember;
				lstDest.DataSource = dataSource;
				Cursor.Current = current;
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnAddOneGroup_Click(object sender, EventArgs e)
		{
			dfrmOperatorDepartmentsConfiguration.lst_UpdateOne(this.dtOptionalGroups, this.dtSelectedGroups, this.lstOptionalGroups, this.lstSelectedGroups);
		}

		private void btnDeleteOneGroup_Click(object sender, EventArgs e)
		{
			dfrmOperatorDepartmentsConfiguration.lst_UpdateOne(this.dtSelectedGroups, this.dtOptionalGroups, this.lstSelectedGroups, this.lstOptionalGroups);
		}

		private void btnDeleteAllGroups_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor current = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				this._unbindGroup();
				DataTable dataTable = this.dtSelectedGroups;
				DataTable dataTable2 = this.dtOptionalGroups;
				for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
				{
					dataTable2.ImportRow(dataTable.Rows[i]);
				}
				dataTable.Clear();
				dataTable.AcceptChanges();
				dataTable2.AcceptChanges();
				this.lstSelectedGroups.Refresh();
				this.lstOptionalGroups.Refresh();
				this._bindGroup();
				Cursor.Current = current;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				Cursor arg_10_0 = Cursor.Current;
				DataTable dataTable = this.dtSelectedGroups;
				string text = " DELETE FROM t_b_Group4Operator Where f_OperatorId = " + this.operatorId;
				wgAppConfig.runUpdateSql(text);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
					{
						text = " INSERT INTO t_b_Group4Operator";
						text += " (f_GroupID, f_OperatorID) ";
						text = text + " Values(" + dataTable.Rows[i]["f_GroupID"];
						text = text + " ," + this.operatorId;
						text += " )";
						wgAppConfig.runUpdateSql(text);
					}
				}
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			this.Cursor = Cursors.Default;
		}

		private void dfrmSwitchGroupsConfiguration_Load(object sender, EventArgs e)
		{
			this.Text = wgAppConfig.ReplaceFloorRomm(this.Text);
			this.Label10.Text = wgAppConfig.ReplaceFloorRomm(this.Label10.Text);
			this.Label11.Text = wgAppConfig.ReplaceFloorRomm(this.Label11.Text);
			this._dataTableLoad();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
