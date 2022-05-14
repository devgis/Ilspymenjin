using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmOperatorUpdate : frmN3000
	{
		private IContainer components;

		private Label label7;

		private TextBox txtName;

		private Label label2;

		private TextBox txtPassword;

		private Button btnCancel;

		private Button btnOK;

		private TextBox txtConfirmedPassword;

		private Label label1;

		public int operateMode;

		public int operatorID = -1;

		public string operatorName = "";

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOperatorUpdate));
			this.label7 = new Label();
			this.txtName = new TextBox();
			this.label2 = new Label();
			this.txtPassword = new TextBox();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.txtConfirmedPassword = new TextBox();
			this.label1 = new Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.label7, "label7");
			this.label7.BackColor = Color.Transparent;
			this.label7.ForeColor = Color.White;
			this.label7.Name = "label7";
			componentResourceManager.ApplyResources(this.txtName, "txtName");
			this.txtName.Name = "txtName";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.txtPassword, "txtPassword");
			this.txtPassword.Name = "txtPassword";
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.txtConfirmedPassword, "txtConfirmedPassword");
			this.txtConfirmedPassword.Name = "txtConfirmedPassword";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtConfirmedPassword);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.txtName);
			base.Controls.Add(this.label2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmOperatorUpdate";
			base.Load += new EventHandler(this.dfrmOperatorUpdate_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmOperatorUpdate()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
			{
				XMessageBox.Show(this, CommonStr.strPersonNameNotEmpty, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.txtPassword.Text.Trim() != this.txtConfirmedPassword.Text.Trim())
			{
				XMessageBox.Show(this, CommonStr.strPasswordNotSame, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.operateMode == 1 || this.operateMode == 2)
			{
				if (this.EditOperator() >= 0)
				{
					base.DialogResult = DialogResult.OK;
					base.Close();
					return;
				}
			}
			else if (this.AddOperator() >= 0)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private int EditOperator()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.EditOperator_Acc();
			}
			int result = -1;
			try
			{
				string text = " SELECT f_OperatorID FROM [t_s_Operator] ";
				text = text + "WHERE [f_OperatorName]=" + wgTools.PrepareStr(this.txtName.Text);
				text = text + " AND  NOT [f_OperatorID]=" + this.operatorID.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						object obj = sqlCommand.ExecuteScalar();
						if (obj != null)
						{
							XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return result;
						}
						text = " UPDATE [t_s_Operator] ";
						text = text + "SET [f_OperatorName]=" + wgTools.PrepareStr(this.txtName.Text);
						text = text + " , [f_Password]= " + wgTools.PrepareStr(this.txtPassword.Text.Trim());
						text = text + " WHERE [f_OperatorID]=" + this.operatorID.ToString();
						using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection))
						{
							sqlCommand2.ExecuteNonQuery();
							result = 1;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		private int EditOperator_Acc()
		{
			int result = -1;
			try
			{
				string text = " SELECT f_OperatorID FROM [t_s_Operator] ";
				text = text + "WHERE [f_OperatorName]=" + wgTools.PrepareStr(this.txtName.Text);
				text = text + " AND NOT [f_OperatorID]=" + this.operatorID.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						object obj = oleDbCommand.ExecuteScalar();
						if (obj != null)
						{
							XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return result;
						}
						text = " UPDATE [t_s_Operator] ";
						text = text + "SET [f_OperatorName]=" + wgTools.PrepareStr(this.txtName.Text);
						text = text + " , [f_Password]= " + wgTools.PrepareStr(this.txtPassword.Text.Trim());
						text = text + " WHERE [f_OperatorID]=" + this.operatorID.ToString();
						using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection))
						{
							oleDbCommand2.ExecuteNonQuery();
							result = 1;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		private int AddOperator()
		{
			if (wgAppConfig.IsAccessDB)
			{
				return this.AddOperator_Acc();
			}
			int result = -1;
			try
			{
				string text = " SELECT f_OperatorID FROM [t_s_Operator] ";
				text = text + "WHERE [f_OperatorName]=" + wgTools.PrepareStr(this.txtName.Text);
				text = text + " AND NOT [f_OperatorID]=" + this.operatorID.ToString();
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						sqlConnection.Open();
						object obj = sqlCommand.ExecuteScalar();
						if (obj != null)
						{
							XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return result;
						}
						text = " INSERT INTO [t_s_Operator] ";
						text += "([f_OperatorName],  [f_Password])";
						text = text + " Values(" + wgTools.PrepareStr(this.txtName.Text);
						text = text + " , " + wgTools.PrepareStr(this.txtPassword.Text.Trim());
						text += ")";
						using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection))
						{
							sqlCommand2.ExecuteNonQuery();
							result = 1;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		private int AddOperator_Acc()
		{
			int result = -1;
			try
			{
				string text = " SELECT f_OperatorID FROM [t_s_Operator] ";
				text = text + "WHERE [f_OperatorName]=" + wgTools.PrepareStr(this.txtName.Text);
				text = text + " AND NOT [f_OperatorID]=" + this.operatorID.ToString();
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						oleDbConnection.Open();
						object obj = oleDbCommand.ExecuteScalar();
						if (obj != null)
						{
							XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return result;
						}
						text = " INSERT INTO [t_s_Operator] ";
						text += "([f_OperatorName],  [f_Password])";
						text = text + " Values(" + wgTools.PrepareStr(this.txtName.Text);
						text = text + " , " + wgTools.PrepareStr(this.txtPassword.Text.Trim());
						text += ")";
						using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection))
						{
							oleDbCommand2.ExecuteNonQuery();
							result = 1;
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		private void dfrmOperatorUpdate_Load(object sender, EventArgs e)
		{
			if (this.operateMode == 2)
			{
				this.txtName.ReadOnly = true;
				this.txtName.TabStop = false;
			}
			this.txtName.Text = this.operatorName;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
	}
}
