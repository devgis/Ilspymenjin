using System;
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
	public class dfrmSystemParam : frmN3000
	{
		private DataTable dt;

		private DataView dv;

		private IContainer components;

		private DataGridView dataGridView1;

		internal Button btnOK;

		internal Button btnCancel;

		private DataGridViewTextBoxColumn f_NO;

		private DataGridViewTextBoxColumn f_Name;

		private DataGridViewTextBoxColumn f_Value;

		private DataGridViewTextBoxColumn f_EName;

		private DataGridViewTextBoxColumn f_Notes;

		private DataGridViewTextBoxColumn f_Modified;

		private DataGridViewTextBoxColumn f_OldValue;

		public dfrmSystemParam()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			DataTable table = (this.dataGridView1.DataSource as DataView).Table;
			for (int i = 0; i <= table.Rows.Count - 1; i++)
			{
				if (wgTools.SetObjToStr(table.Rows[i]["f_Value"]) != wgTools.SetObjToStr(table.Rows[i]["f_OldValue"]))
				{
					string text = " UPDATE t_a_SystemParam SET ";
					text = text + " f_Value = " + wgTools.PrepareStr(table.Rows[i]["f_Value"].ToString());
					text = text + " , f_Modified = " + wgTools.PrepareStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					text = text + " WHERE f_NO = " + table.Rows[i]["f_NO"].ToString();
					wgAppConfig.runUpdateSql(text);
				}
			}
			if (XMessageBox.Show(CommonStr.strUpdateSuccessfully, wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.Close();
		}

		private void fillSystemParam()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.fillSystemParam_Acc();
				return;
			}
			string text = " SELECT ";
			text += " f_NO, f_Name, f_Value, f_EName, f_Notes, f_Modified, f_Value as f_OldValue ";
			text += " FROM t_a_SystemParam ";
			text += " ORDER BY [f_NO] ";
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
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.DataSource = this.dv;
			int num = 0;
			while (num < this.dv.Table.Columns.Count && num < this.dataGridView1.ColumnCount)
			{
				this.dataGridView1.Columns[num].DataPropertyName = this.dv.Table.Columns[num].ColumnName;
				num++;
			}
			this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.WindowText;
		}

		private void fillSystemParam_Acc()
		{
			string text = " SELECT ";
			text += " f_NO, f_Name, f_Value, f_EName, f_Notes, f_Modified, f_Value as f_OldValue ";
			text += " FROM t_a_SystemParam ";
			text += " ORDER BY [f_NO] ";
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
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.DataSource = this.dv;
			int num = 0;
			while (num < this.dv.Table.Columns.Count && num < this.dataGridView1.ColumnCount)
			{
				this.dataGridView1.Columns[num].DataPropertyName = this.dv.Table.Columns[num].ColumnName;
				num++;
			}
			this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.WindowText;
		}

		private void dfrmSystemParam_Load(object sender, EventArgs e)
		{
			this.fillSystemParam();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmSystemParam_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
				{
					this.dataGridView1.Columns[i].Visible = true;
				}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmSystemParam));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			this.dataGridView1 = new DataGridView();
			this.f_NO = new DataGridViewTextBoxColumn();
			this.f_Name = new DataGridViewTextBoxColumn();
			this.f_Value = new DataGridViewTextBoxColumn();
			this.f_EName = new DataGridViewTextBoxColumn();
			this.f_Notes = new DataGridViewTextBoxColumn();
			this.f_Modified = new DataGridViewTextBoxColumn();
			this.f_OldValue = new DataGridViewTextBoxColumn();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = Color.FromArgb(192, 255, 255);
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_NO,
				this.f_Name,
				this.f_Value,
				this.f_EName,
				this.f_Notes,
				this.f_Modified,
				this.f_OldValue
			});
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = Color.White;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(224, 224, 224);
			this.f_NO.DefaultCellStyle = dataGridViewCellStyle4;
			this.f_NO.Frozen = true;
			componentResourceManager.ApplyResources(this.f_NO, "f_NO");
			this.f_NO.Name = "f_NO";
			this.f_NO.ReadOnly = true;
			dataGridViewCellStyle5.BackColor = Color.FromArgb(224, 224, 224);
			this.f_Name.DefaultCellStyle = dataGridViewCellStyle5;
			componentResourceManager.ApplyResources(this.f_Name, "f_Name");
			this.f_Name.Name = "f_Name";
			this.f_Name.ReadOnly = true;
			this.f_Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Value, "f_Value");
			this.f_Value.Name = "f_Value";
			componentResourceManager.ApplyResources(this.f_EName, "f_EName");
			this.f_EName.Name = "f_EName";
			componentResourceManager.ApplyResources(this.f_Notes, "f_Notes");
			this.f_Notes.Name = "f_Notes";
			this.f_Notes.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Modified, "f_Modified");
			this.f_Modified.Name = "f_Modified";
			componentResourceManager.ApplyResources(this.f_OldValue, "f_OldValue");
			this.f_OldValue.Name = "f_OldValue";
			this.f_OldValue.ReadOnly = true;
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
			base.Controls.Add(this.dataGridView1);
			base.Name = "dfrmSystemParam";
			base.Load += new EventHandler(this.dfrmSystemParam_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmSystemParam_KeyDown);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
