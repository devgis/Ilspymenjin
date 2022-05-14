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

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftEdit : frmN3000
	{
		public int shiftid = -1;

		private Container components;

		internal Label Label3;

		internal Button btnOK;

		internal Button btnCancel;

		internal ComboBox cbof_shift;

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrConsumerCMIndex = new ArrayList();

		private ArrayList arrShiftID = new ArrayList();

		private ArrayList arrSelectedShiftID = new ArrayList();

		private DataSet dsConsumers;

		private DataTable dtOptionalShift;

		public dfrmShiftEdit()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftEdit));
			this.cbof_shift = new ComboBox();
			this.Label3 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			base.SuspendLayout();
			this.cbof_shift.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_shift, "cbof_shift");
			this.cbof_shift.Name = "cbof_shift";
			this.Label3.BackColor = Color.Transparent;
			this.Label3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.cbof_shift);
			base.Controls.Add(this.Label3);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftEdit";
			base.Load += new EventHandler(this.dfrmShiftEdit_Load);
			base.ResumeLayout(false);
		}

		private void dfrmShiftEdit_Load(object sender, EventArgs e)
		{
			this.dsConsumers = new DataSet("Users");
			string text = "";
			if (wgAppConfig.IsAccessDB)
			{
				text += " SELECT    IIF( [f_ShiftName] IS NULL , CSTR([f_ShiftID]) ";
				text += "    , CSTR([f_ShiftID]) + '-' + [f_ShiftName] ";
				text += "    ) AS f_ShiftFullName, [f_ShiftID] from t_b_ShiftSet order by f_ShiftID ASC  ";
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
			text += " SELECT    CASE WHEN [f_ShiftName] IS NULL THEN CONVERT(nvarchar(50),[f_ShiftID]) ";
			text += "    ELSE CONVERT(nvarchar(50),[f_ShiftID]) + '-' + [f_ShiftName] ";
			text += "    END AS f_ShiftFullName, [f_ShiftID] from t_b_ShiftSet order by f_ShiftID ASC  ";
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
			this.arrShiftID.Clear();
			this.cbof_shift.Items.Clear();
			this.arrShiftID.Add("0");
			this.cbof_shift.Items.Add("0*-" + CommonStr.strRest);
			if (this.dtOptionalShift.Rows.Count > 0)
			{
				for (int i = 0; i <= this.dtOptionalShift.Rows.Count - 1; i++)
				{
					this.cbof_shift.Items.Add(this.dtOptionalShift.Rows[i][0]);
					this.arrShiftID.Add(this.dtOptionalShift.Rows[i][1]);
				}
			}
			if (this.cbof_shift.Items.Count > 0)
			{
				this.cbof_shift.SelectedIndex = 0;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.shiftid = int.Parse(this.arrShiftID[this.cbof_shift.SelectedIndex].ToString());
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
