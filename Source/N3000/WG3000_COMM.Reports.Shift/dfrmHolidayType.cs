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
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmHolidayType : frmN3000
	{
		private IContainer components;

		private ListBox lstHolidayType;

		private Button btnAdd;

		private Button btnEdit;

		private Button btnDel;

		private Button btnExit;

		private ArrayList arrDefaultList = new ArrayList();

		private DataTable dt;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmHolidayType));
			this.btnExit = new Button();
			this.btnDel = new Button();
			this.btnEdit = new Button();
			this.btnAdd = new Button();
			this.lstHolidayType = new ListBox();
			base.SuspendLayout();
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.btnDel.BackColor = Color.Transparent;
			this.btnDel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDel, "btnDel");
			this.btnDel.ForeColor = Color.White;
			this.btnDel.Name = "btnDel";
			this.btnDel.UseVisualStyleBackColor = false;
			this.btnDel.Click += new EventHandler(this.btnDel_Click);
			this.btnEdit.BackColor = Color.Transparent;
			this.btnEdit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			this.btnAdd.BackColor = Color.Transparent;
			this.btnAdd.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = false;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.lstHolidayType.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.lstHolidayType, "lstHolidayType");
			this.lstHolidayType.Name = "lstHolidayType";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnDel);
			base.Controls.Add(this.btnEdit);
			base.Controls.Add(this.btnAdd);
			base.Controls.Add(this.lstHolidayType);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmHolidayType";
			base.Load += new EventHandler(this.dfrmHolidayType_Load);
			base.ResumeLayout(false);
		}

		public dfrmHolidayType()
		{
			this.InitializeComponent();
		}

		private void dfrmHolidayType_Load(object sender, EventArgs e)
		{
			this._loadData();
		}

		private void _loadData()
		{
			string cmdText = "SELECT * FROM t_a_HolidayType ORDER BY f_NO ";
			this.dt = new DataTable("t_a_HolidayType");
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dt);
						}
					}
				}
				comShift_Acc.localizedHolidayType(this.dt);
			}
			else
			{
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlDataAdapter.Fill(this.dt);
						}
					}
				}
				comShift.localizedHolidayType(this.dt);
			}
			this.arrDefaultList.Clear();
			this.arrDefaultList.Add("出差");
			this.arrDefaultList.Add("Business Trip");
			this.arrDefaultList.Add("病假");
			this.arrDefaultList.Add("Sick Leave");
			this.arrDefaultList.Add("事假");
			this.arrDefaultList.Add("Private Leave");
			this.arrDefaultList.Add(CommonStr.strAbsence);
			this.arrDefaultList.Add(CommonStr.strLateness);
			this.arrDefaultList.Add(CommonStr.strNotReadCard);
			this.arrDefaultList.Add(CommonStr.strLeaveEarly);
			this.arrDefaultList.Add(CommonStr.strRest);
			this.arrDefaultList.Add(CommonStr.strOvertime);
			this.arrDefaultList.Add(CommonStr.strSignIn);
			this.arrDefaultList.Add(CommonStr.strPrivateLeave);
			this.arrDefaultList.Add(CommonStr.strSickLeave);
			this.arrDefaultList.Add(CommonStr.strBusinessTrip);
			this.arrDefaultList.Add(CommonStr.strPatrolEventAbsence);
			this.arrDefaultList.Add(CommonStr.strPatrolEventEarly);
			this.arrDefaultList.Add(CommonStr.strPatrolEventLate);
			this.arrDefaultList.Add(CommonStr.strPatrolEventNormal);
			this.arrDefaultList.Add(CommonStr.strPatrolEventRest);
			this.arrDefaultList.Add(CommonStr.strPatrolEventLate);
			if (this.dt.Rows.Count > 0)
			{
				this.btnDel.Enabled = true;
				this.btnEdit.Enabled = true;
			}
			else
			{
				this.btnDel.Enabled = false;
				this.btnEdit.Enabled = false;
			}
			if (this.dt.Rows.Count >= 32)
			{
				this.btnAdd.Enabled = false;
			}
			else
			{
				this.btnAdd.Enabled = true;
			}
			this.lstHolidayType.DataSource = this.dt;
			this.lstHolidayType.DisplayMember = "f_HolidayType";
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				if (dfrmInputNewName.ShowDialog(this) == DialogResult.OK)
				{
					string strNewName = dfrmInputNewName.strNewName;
					if (this.arrDefaultList.IndexOf(strNewName) >= 0)
					{
						XMessageBox.Show(this, CommonStr.strDefaultType, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						using (DataView dataView = new DataView(this.lstHolidayType.DataSource as DataTable))
						{
							dataView.RowFilter = " f_HolidayType= " + wgTools.PrepareStr(strNewName);
							if (dataView.Count > 0)
							{
								XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
						string text = " INSERT INTO  t_a_HolidayType (f_HolidayType) VALUES(";
						text += wgTools.PrepareStr(strNewName.ToString());
						text += ")";
						wgAppConfig.runUpdateSql(text);
						this._loadData();
					}
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (this.arrDefaultList.IndexOf(this.lstHolidayType.Text) >= 0)
			{
				XMessageBox.Show(this, CommonStr.strDefaultType, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				if (dfrmInputNewName.ShowDialog(this) == DialogResult.OK)
				{
					string strNewName = dfrmInputNewName.strNewName;
					if (!(this.lstHolidayType.Text == strNewName))
					{
						if (this.arrDefaultList.IndexOf(strNewName) >= 0)
						{
							XMessageBox.Show(this, CommonStr.strDefaultType, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						else
						{
							string text = " UPDATE t_a_HolidayType SET f_HolidayType = ";
							text += wgTools.PrepareStr(strNewName.ToString());
							text = text + " WHERE f_HolidayType = " + wgTools.PrepareStr(this.lstHolidayType.Text);
							wgAppConfig.runUpdateSql(text);
							this._loadData();
						}
					}
				}
			}
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			if (this.arrDefaultList.IndexOf(this.lstHolidayType.Text) >= 0)
			{
				XMessageBox.Show(this, CommonStr.strDefaultType, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string text = string.Format("{0}", this.btnDel.Text);
			text = string.Format(CommonStr.strAreYouSure + " {0} ?", text);
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			string text2 = " DELETE FROM t_a_HolidayType ";
			text2 = text2 + " WHERE f_HolidayType = " + wgTools.PrepareStr(this.lstHolidayType.Text);
			wgAppConfig.runUpdateSql(text2);
			this._loadData();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
