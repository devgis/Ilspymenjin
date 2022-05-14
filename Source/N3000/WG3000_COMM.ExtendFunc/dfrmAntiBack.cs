using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmAntiBack : frmN3000
	{
		public string ControllerSN = "";

		public static bool bDisplayIndoorPersonMax;

		public int retValue;

		private IContainer components;

		private CheckBox checkBox11;

		private CheckBox checkBox21;

		private Button btnOK;

		private Button btnCancel;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private RadioButton radioButton3;

		private RadioButton radioButton4;

		private RadioButton radioButton0;

		private CheckBox checkBox22;

		internal CheckBox chkActiveAntibackShare;

		internal NumericUpDown nudTotal;

		public dfrmAntiBack()
		{
			this.InitializeComponent();
		}

		private void dfrmAntiBack_Load(object sender, EventArgs e)
		{
			string cmdText = "SELECT * FROM t_b_Controller Where f_ControllerSN = " + this.ControllerSN;
			DbConnection dbConnection;
			DbCommand dbCommand;
			if (wgAppConfig.IsAccessDB)
			{
				dbConnection = new OleDbConnection(wgAppConfig.dbConString);
				dbCommand = new OleDbCommand(cmdText, dbConnection as OleDbConnection);
			}
			else
			{
				dbConnection = new SqlConnection(wgAppConfig.dbConString);
				dbCommand = new SqlCommand(cmdText, dbConnection as SqlConnection);
			}
			dbConnection.Open();
			DbDataReader dbDataReader = dbCommand.ExecuteReader();
			if (dbDataReader.Read())
			{
				switch (wgMjController.GetControllerType(int.Parse(this.ControllerSN)))
				{
				case 1:
					this.radioButton1.Text = this.checkBox11.Text;
					this.radioButton2.Visible = false;
					this.radioButton3.Visible = false;
					this.radioButton4.Visible = false;
					break;
				case 2:
					this.radioButton1.Text = this.checkBox21.Text;
					this.radioButton2.Text = this.checkBox22.Text;
					this.radioButton3.Visible = false;
					this.radioButton4.Visible = false;
					break;
				}
				switch ((int)dbDataReader["f_AntiBack"] % 10)
				{
				case 1:
					this.radioButton1.Checked = true;
					break;
				case 2:
					this.radioButton2.Checked = true;
					break;
				case 3:
					this.radioButton3.Checked = true;
					break;
				case 4:
					this.radioButton4.Checked = true;
					break;
				default:
					this.radioButton0.Checked = true;
					break;
				}
				if ((int)dbDataReader["f_AntiBack"] > 10)
				{
					this.nudTotal.Visible = true;
					this.chkActiveAntibackShare.Visible = true;
					if (((int)dbDataReader["f_AntiBack"] - (int)dbDataReader["f_AntiBack"] % 10) / 10 > 1000)
					{
						this.nudTotal.Maximum = 4000m;
					}
					this.nudTotal.Value = ((int)dbDataReader["f_AntiBack"] - (int)dbDataReader["f_AntiBack"] % 10) / 10;
					this.chkActiveAntibackShare.Checked = true;
				}
			}
			dbDataReader.Close();
			dbConnection.Close();
			if (dfrmAntiBack.bDisplayIndoorPersonMax)
			{
				this.nudTotal.Visible = true;
				this.chkActiveAntibackShare.Visible = true;
			}
			if (wgAppConfig.getParamValBoolByNO(62))
			{
				bool arg_275_0 = this.radioButton0.Visible;
				bool arg_281_0 = this.radioButton1.Visible;
				if (this.radioButton2.Visible)
				{
					this.radioButton1.Checked = false;
					this.radioButton1.Enabled = false;
				}
				if (this.radioButton3.Visible)
				{
					this.radioButton1.Enabled = false;
					this.radioButton2.Enabled = false;
				}
				if (this.radioButton4.Visible)
				{
					this.radioButton1.Enabled = false;
					this.radioButton2.Enabled = true;
					this.radioButton3.Enabled = false;
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.retValue = 0;
			if (this.radioButton1.Checked)
			{
				this.retValue = 1;
			}
			if (this.radioButton2.Checked)
			{
				this.retValue = 2;
			}
			if (this.radioButton3.Checked)
			{
				this.retValue = 3;
			}
			if (this.radioButton4.Checked)
			{
				this.retValue = 4;
			}
			if (this.chkActiveAntibackShare.Checked)
			{
				this.retValue += (int)this.nudTotal.Value * 10;
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmAntiBack_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyValue == 81 && e.Shift)
			{
				if (this.chkActiveAntibackShare.Visible)
				{
					this.nudTotal.ReadOnly = false;
					this.nudTotal.Maximum = 4000m;
				}
				this.chkActiveAntibackShare.Visible = true;
				this.nudTotal.Visible = true;
			}
		}

		private void chkActiveAntibackShare_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkActiveAntibackShare.Checked)
			{
				if (this.radioButton4.Visible)
				{
					this.radioButton0.Enabled = false;
					this.radioButton1.Enabled = false;
					this.radioButton2.Enabled = true;
					this.radioButton3.Enabled = false;
					if (!this.radioButton4.Checked)
					{
						this.radioButton2.Checked = true;
						return;
					}
				}
				else
				{
					if (this.radioButton2.Visible)
					{
						this.radioButton0.Enabled = false;
						this.radioButton1.Checked = false;
						this.radioButton1.Enabled = false;
						this.radioButton2.Checked = true;
						return;
					}
					if (this.radioButton1.Visible)
					{
						this.radioButton0.Enabled = false;
						this.radioButton1.Checked = true;
						return;
					}
				}
			}
			else if (wgAppConfig.getParamValBoolByNO(62))
			{
				if (this.radioButton0.Visible)
				{
					this.radioButton0.Enabled = true;
				}
				bool arg_101_0 = this.radioButton1.Visible;
				if (this.radioButton2.Visible)
				{
					this.radioButton1.Checked = false;
					this.radioButton1.Enabled = false;
				}
				if (this.radioButton3.Visible)
				{
					this.radioButton1.Enabled = false;
					this.radioButton2.Enabled = false;
				}
				if (this.radioButton4.Visible)
				{
					this.radioButton1.Enabled = false;
					this.radioButton2.Enabled = true;
					this.radioButton3.Enabled = false;
					return;
				}
			}
			else
			{
				this.radioButton0.Enabled = true;
				this.radioButton1.Enabled = true;
				this.radioButton2.Enabled = true;
				this.radioButton3.Enabled = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmAntiBack));
			this.nudTotal = new NumericUpDown();
			this.chkActiveAntibackShare = new CheckBox();
			this.radioButton0 = new RadioButton();
			this.radioButton4 = new RadioButton();
			this.radioButton3 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.checkBox22 = new CheckBox();
			this.checkBox21 = new CheckBox();
			this.checkBox11 = new CheckBox();
			((ISupportInitialize)this.nudTotal).BeginInit();
			base.SuspendLayout();
			this.nudTotal.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudTotal, "nudTotal");
			NumericUpDown arg_E1_0 = this.nudTotal;
			int[] array = new int[4];
			array[0] = 1000;
			arg_E1_0.Maximum = new decimal(array);
			NumericUpDown arg_FD_0 = this.nudTotal;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_FD_0.Minimum = new decimal(array2);
			this.nudTotal.Name = "nudTotal";
			this.nudTotal.ReadOnly = true;
			NumericUpDown arg_135_0 = this.nudTotal;
			int[] array3 = new int[4];
			array3[0] = 2;
			arg_135_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this.chkActiveAntibackShare, "chkActiveAntibackShare");
			this.chkActiveAntibackShare.BackColor = Color.Transparent;
			this.chkActiveAntibackShare.ForeColor = Color.White;
			this.chkActiveAntibackShare.Name = "chkActiveAntibackShare";
			this.chkActiveAntibackShare.UseVisualStyleBackColor = false;
			this.chkActiveAntibackShare.CheckedChanged += new EventHandler(this.chkActiveAntibackShare_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton0, "radioButton0");
			this.radioButton0.BackColor = Color.Transparent;
			this.radioButton0.Checked = true;
			this.radioButton0.ForeColor = Color.White;
			this.radioButton0.Name = "radioButton0";
			this.radioButton0.TabStop = true;
			this.radioButton0.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.radioButton4, "radioButton4");
			this.radioButton4.BackColor = Color.Transparent;
			this.radioButton4.ForeColor = Color.White;
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.radioButton3, "radioButton3");
			this.radioButton3.BackColor = Color.Transparent;
			this.radioButton3.ForeColor = Color.White;
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.BackColor = Color.Transparent;
			this.radioButton2.ForeColor = Color.White;
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.BackColor = Color.Transparent;
			this.radioButton1.ForeColor = Color.White;
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.UseVisualStyleBackColor = false;
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
			componentResourceManager.ApplyResources(this.checkBox22, "checkBox22");
			this.checkBox22.BackColor = Color.Transparent;
			this.checkBox22.ForeColor = Color.White;
			this.checkBox22.Name = "checkBox22";
			this.checkBox22.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.checkBox21, "checkBox21");
			this.checkBox21.BackColor = Color.Transparent;
			this.checkBox21.ForeColor = Color.White;
			this.checkBox21.Name = "checkBox21";
			this.checkBox21.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.checkBox11, "checkBox11");
			this.checkBox11.BackColor = Color.Transparent;
			this.checkBox11.ForeColor = Color.White;
			this.checkBox11.Name = "checkBox11";
			this.checkBox11.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.nudTotal);
			base.Controls.Add(this.chkActiveAntibackShare);
			base.Controls.Add(this.radioButton0);
			base.Controls.Add(this.radioButton4);
			base.Controls.Add(this.radioButton3);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.checkBox22);
			base.Controls.Add(this.checkBox21);
			base.Controls.Add(this.checkBox11);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmAntiBack";
			base.Load += new EventHandler(this.dfrmAntiBack_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmAntiBack_KeyDown);
			((ISupportInitialize)this.nudTotal).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
