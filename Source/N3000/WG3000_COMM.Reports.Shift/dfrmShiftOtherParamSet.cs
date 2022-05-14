using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftOtherParamSet : frmN3000
	{
		private IContainer components;

		internal GroupBox GroupBox1;

		internal Label Label16;

		internal Label Label15;

		internal Label Label13;

		internal Label Label12;

		internal NumericUpDown nudOvertimeTimeout;

		internal NumericUpDown nudAheadMinutes;

		internal NumericUpDown nudLeaveTimeout;

		internal NumericUpDown nudOvertimeMinutes;

		internal NumericUpDown nudLateTimeout;

		internal Label Label1;

		internal Label Label2;

		internal Label Label3;

		internal Label Label4;

		internal Label Label5;

		internal Label Label14;

		internal Label Label17;

		internal Button btnOK;

		internal Button btnCancel;

		public dfrmShiftOtherParamSet()
		{
			this.InitializeComponent();
		}

		private void getShiftOtherAttendanceParam()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.getShiftOtherAttendanceParam_Acc();
				return;
			}
			string cmdText = "SELECT * FROM t_a_Shift_Attendence";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						int num = (int)sqlDataReader["f_No"];
						if (num <= 4)
						{
							if (num != 1)
							{
								if (num == 4)
								{
									this.nudLeaveTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
								}
							}
							else
							{
								this.nudLateTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
							}
						}
						else if (num != 7)
						{
							switch (num)
							{
							case 18:
								this.nudAheadMinutes.Value = int.Parse((string)sqlDataReader["f_Value"]);
								break;
							case 20:
								this.nudOvertimeMinutes.Value = int.Parse((string)sqlDataReader["f_Value"]);
								break;
							}
						}
						else
						{
							this.nudOvertimeTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
						}
					}
					sqlDataReader.Close();
				}
			}
		}

		private void getShiftOtherAttendanceParam_Acc()
		{
			string cmdText = "SELECT * FROM t_a_Shift_Attendence";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						int num = (int)oleDbDataReader["f_No"];
						if (num <= 4)
						{
							if (num != 1)
							{
								if (num == 4)
								{
									this.nudLeaveTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
								}
							}
							else
							{
								this.nudLateTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
							}
						}
						else if (num != 7)
						{
							switch (num)
							{
							case 18:
								this.nudAheadMinutes.Value = int.Parse((string)oleDbDataReader["f_Value"]);
								break;
							case 20:
								this.nudOvertimeMinutes.Value = int.Parse((string)oleDbDataReader["f_Value"]);
								break;
							}
						}
						else
						{
							this.nudOvertimeTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
						}
					}
					oleDbDataReader.Close();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.setShiftOtherAttendanceParam(1, this.nudLateTimeout.Value.ToString());
			this.setShiftOtherAttendanceParam(4, this.nudLeaveTimeout.Value.ToString());
			this.setShiftOtherAttendanceParam(7, this.nudOvertimeTimeout.Value.ToString());
			this.setShiftOtherAttendanceParam(17, this.nudAheadMinutes.Value.ToString());
			this.setShiftOtherAttendanceParam(18, this.nudAheadMinutes.Value.ToString());
			this.setShiftOtherAttendanceParam(19, this.nudAheadMinutes.Value.ToString());
			this.setShiftOtherAttendanceParam(20, this.nudOvertimeMinutes.Value.ToString());
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void setShiftOtherAttendanceParam(int no, string val)
		{
			string text = "UPDATE t_a_Shift_Attendence ";
			text = text + " SET [f_value]=" + wgTools.PrepareStr(val);
			text = text + " WHERE [f_NO]= " + no.ToString();
			wgAppConfig.runUpdateSql(text);
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuShiftRule";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnOK.Visible = false;
			}
		}

		private void dfrmShiftOtherParamSet_Load(object sender, EventArgs e)
		{
			this.loadOperatorPrivilege();
			this.getShiftOtherAttendanceParam();
		}

		private void funcCtrlShiftQ()
		{
			this.Label2.Visible = true;
			this.Label14.Visible = true;
			this.nudOvertimeMinutes.Visible = true;
		}

		private void dfrmShiftOtherParamSet_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.funcCtrlShiftQ();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftOtherParamSet));
			this.GroupBox1 = new GroupBox();
			this.nudAheadMinutes = new NumericUpDown();
			this.Label16 = new Label();
			this.Label15 = new Label();
			this.Label13 = new Label();
			this.Label12 = new Label();
			this.nudOvertimeTimeout = new NumericUpDown();
			this.nudLeaveTimeout = new NumericUpDown();
			this.nudLateTimeout = new NumericUpDown();
			this.Label1 = new Label();
			this.Label3 = new Label();
			this.Label4 = new Label();
			this.Label5 = new Label();
			this.Label17 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.Label2 = new Label();
			this.nudOvertimeMinutes = new NumericUpDown();
			this.Label14 = new Label();
			this.GroupBox1.SuspendLayout();
			((ISupportInitialize)this.nudAheadMinutes).BeginInit();
			((ISupportInitialize)this.nudOvertimeTimeout).BeginInit();
			((ISupportInitialize)this.nudLeaveTimeout).BeginInit();
			((ISupportInitialize)this.nudLateTimeout).BeginInit();
			((ISupportInitialize)this.nudOvertimeMinutes).BeginInit();
			base.SuspendLayout();
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.nudAheadMinutes);
			this.GroupBox1.Controls.Add(this.Label16);
			this.GroupBox1.Controls.Add(this.Label15);
			this.GroupBox1.Controls.Add(this.Label13);
			this.GroupBox1.Controls.Add(this.Label12);
			this.GroupBox1.Controls.Add(this.nudOvertimeTimeout);
			this.GroupBox1.Controls.Add(this.nudLeaveTimeout);
			this.GroupBox1.Controls.Add(this.nudLateTimeout);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.Label17);
			this.GroupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			this.nudAheadMinutes.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudAheadMinutes, "nudAheadMinutes");
			NumericUpDown arg_2D0_0 = this.nudAheadMinutes;
			int[] array = new int[4];
			array[0] = 600;
			arg_2D0_0.Maximum = new decimal(array);
			this.nudAheadMinutes.Name = "nudAheadMinutes";
			this.nudAheadMinutes.ReadOnly = true;
			NumericUpDown arg_309_0 = this.nudAheadMinutes;
			int[] array2 = new int[4];
			array2[0] = 60;
			arg_309_0.Value = new decimal(array2);
			componentResourceManager.ApplyResources(this.Label16, "Label16");
			this.Label16.Name = "Label16";
			componentResourceManager.ApplyResources(this.Label15, "Label15");
			this.Label15.Name = "Label15";
			componentResourceManager.ApplyResources(this.Label13, "Label13");
			this.Label13.Name = "Label13";
			componentResourceManager.ApplyResources(this.Label12, "Label12");
			this.Label12.Name = "Label12";
			this.nudOvertimeTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudOvertimeTimeout, "nudOvertimeTimeout");
			NumericUpDown arg_3CE_0 = this.nudOvertimeTimeout;
			int[] array3 = new int[4];
			array3[0] = 600;
			arg_3CE_0.Maximum = new decimal(array3);
			this.nudOvertimeTimeout.Name = "nudOvertimeTimeout";
			this.nudOvertimeTimeout.ReadOnly = true;
			NumericUpDown arg_40A_0 = this.nudOvertimeTimeout;
			int[] array4 = new int[4];
			array4[0] = 60;
			arg_40A_0.Value = new decimal(array4);
			this.nudLeaveTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudLeaveTimeout, "nudLeaveTimeout");
			NumericUpDown arg_44E_0 = this.nudLeaveTimeout;
			int[] array5 = new int[4];
			array5[0] = 600;
			arg_44E_0.Maximum = new decimal(array5);
			this.nudLeaveTimeout.Name = "nudLeaveTimeout";
			this.nudLeaveTimeout.ReadOnly = true;
			NumericUpDown arg_489_0 = this.nudLeaveTimeout;
			int[] array6 = new int[4];
			array6[0] = 5;
			arg_489_0.Value = new decimal(array6);
			this.nudLateTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudLateTimeout, "nudLateTimeout");
			NumericUpDown arg_4CD_0 = this.nudLateTimeout;
			int[] array7 = new int[4];
			array7[0] = 600;
			arg_4CD_0.Maximum = new decimal(array7);
			this.nudLateTimeout.Name = "nudLateTimeout";
			this.nudLateTimeout.ReadOnly = true;
			NumericUpDown arg_508_0 = this.nudLateTimeout;
			int[] array8 = new int[4];
			array8[0] = 5;
			arg_508_0.Value = new decimal(array8);
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.Name = "Label4";
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.Label17, "Label17");
			this.Label17.Name = "Label17";
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
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			this.Label2.Name = "Label2";
			componentResourceManager.ApplyResources(this.nudOvertimeMinutes, "nudOvertimeMinutes");
			NumericUpDown arg_70A_0 = this.nudOvertimeMinutes;
			int[] array9 = new int[4];
			array9[0] = 600;
			arg_70A_0.Maximum = new decimal(array9);
			this.nudOvertimeMinutes.Name = "nudOvertimeMinutes";
			NumericUpDown arg_73D_0 = this.nudOvertimeMinutes;
			int[] array10 = new int[4];
			array10[0] = 360;
			arg_73D_0.Value = new decimal(array10);
			componentResourceManager.ApplyResources(this.Label14, "Label14");
			this.Label14.BackColor = Color.Transparent;
			this.Label14.ForeColor = Color.White;
			this.Label14.Name = "Label14";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.nudOvertimeMinutes);
			base.Controls.Add(this.Label14);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftOtherParamSet";
			base.Load += new EventHandler(this.dfrmShiftOtherParamSet_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmShiftOtherParamSet_KeyDown);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			((ISupportInitialize)this.nudAheadMinutes).EndInit();
			((ISupportInitialize)this.nudOvertimeTimeout).EndInit();
			((ISupportInitialize)this.nudLeaveTimeout).EndInit();
			((ISupportInitialize)this.nudLateTimeout).EndInit();
			((ISupportInitialize)this.nudOvertimeMinutes).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
