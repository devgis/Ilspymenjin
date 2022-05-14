using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class dfrmPatrolTaskEdit : frmN3000
	{
		public int routeID = -1;

		private Container components;

		internal Label Label3;

		internal Button btnOK;

		internal Button btnCancel;

		internal ComboBox cbof_route;

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrConsumerCMIndex = new ArrayList();

		private ArrayList arrRouteID = new ArrayList();

		private ArrayList arrSelectedRouteID = new ArrayList();

		private DataSet dsConsumers;

		private DataTable dtOptionalShift;

		public dfrmPatrolTaskEdit()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPatrolTaskEdit));
			this.cbof_route = new ComboBox();
			this.Label3 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.cbof_route, "cbof_route");
			this.cbof_route.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_route.Name = "cbof_route";
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.BackColor = Color.Transparent;
			this.Label3.ForeColor = Color.White;
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.cbof_route);
			base.Controls.Add(this.Label3);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmPatrolTaskEdit";
			base.Load += new EventHandler(this.dfrmShiftEdit_Load);
			base.ResumeLayout(false);
		}

		private void dfrmShiftEdit_Load(object sender, EventArgs e)
		{
			this.dsConsumers = new DataSet("Users");
			string text = "";
			if (wgAppConfig.IsAccessDB)
			{
				text += " SELECT    IIF( [f_RouteName] IS NULL , CSTR([f_RouteID]) ";
				text += "    , CSTR([f_RouteID]) + '-' + [f_RouteName] ";
				text += "    ) AS f_RouteFullName, [f_RouteID] from t_d_PatrolRouteList order by f_RouteID ASC  ";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dsConsumers, "OptionalShift");
						}
					}
					goto IL_114;
				}
			}
			text += " SELECT    CASE WHEN [f_RouteName] IS NULL THEN CONVERT(nvarchar(50),[f_RouteID]) ";
			text += "    ELSE CONVERT(nvarchar(50),[f_RouteID]) + '-' + [f_RouteName] ";
			text += "    END AS f_RouteFullName, [f_RouteID] from t_d_PatrolRouteList order by f_RouteID ASC  ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dsConsumers, "OptionalShift");
					}
				}
			}
			IL_114:
			this.dtOptionalShift = this.dsConsumers.Tables["OptionalShift"];
			this.arrRouteID.Clear();
			this.cbof_route.Items.Clear();
			this.arrRouteID.Add("0");
			this.cbof_route.Items.Add("0*-" + CommonStr.strPatrolEventRest);
			if (this.dtOptionalShift.Rows.Count > 0)
			{
				for (int i = 0; i <= this.dtOptionalShift.Rows.Count - 1; i++)
				{
					this.cbof_route.Items.Add(this.dtOptionalShift.Rows[i][0]);
					this.arrRouteID.Add(this.dtOptionalShift.Rows[i][1]);
				}
			}
			if (this.cbof_route.Items.Count > 0)
			{
				this.cbof_route.SelectedIndex = 0;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.routeID = int.Parse(this.arrRouteID[this.cbof_route.SelectedIndex].ToString());
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
	}
}
