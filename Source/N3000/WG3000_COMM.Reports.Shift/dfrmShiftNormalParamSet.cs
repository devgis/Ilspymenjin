using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftNormalParamSet : frmN3000
	{
		private IContainer components;

		internal GroupBox GroupBox1;

		internal ComboBox cboLeaveAbsenceDay;

		internal Label Label16;

		internal Label Label15;

		internal Label Label13;

		internal Label Label12;

		internal NumericUpDown nudOvertimeTimeout;

		internal ComboBox cboLateAbsenceDay;

		internal NumericUpDown nudLeaveAbsenceTimeout;

		internal NumericUpDown nudLeaveTimeout;

		internal NumericUpDown nudLateAbsenceTimeout;

		internal NumericUpDown nudLateTimeout;

		internal Label Label1;

		internal Label Label2;

		internal Label Label3;

		internal Label Label4;

		internal Label Label5;

		internal Label Label14;

		internal Label Label17;

		internal DateTimePicker dtpOffduty0;

		internal DateTimePicker dtpOnduty0;

		internal Label Label7;

		internal Label Label6;

		internal GroupBox grpbTwoTimes;

		internal RadioButton optReadCardTwoTimes;

		internal RadioButton optReadCardFourTimes;

		internal Button btnOK;

		internal DateTimePicker dtpOnduty2;

		internal Label Label11;

		internal GroupBox grpbFourtimes;

		internal Label Label8;

		internal DateTimePicker dtpOnduty1;

		internal DateTimePicker dtpOffduty1;

		internal Label Label9;

		internal DateTimePicker dtpOffduty2;

		internal Label Label10;

		internal GroupBox GroupBox2;

		internal Button btnCancel;

		internal Button btnOption;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftNormalParamSet));
			this.btnOption = new Button();
			this.GroupBox1 = new GroupBox();
			this.cboLeaveAbsenceDay = new ComboBox();
			this.Label16 = new Label();
			this.Label15 = new Label();
			this.Label13 = new Label();
			this.Label12 = new Label();
			this.nudOvertimeTimeout = new NumericUpDown();
			this.cboLateAbsenceDay = new ComboBox();
			this.nudLeaveAbsenceTimeout = new NumericUpDown();
			this.nudLeaveTimeout = new NumericUpDown();
			this.nudLateAbsenceTimeout = new NumericUpDown();
			this.nudLateTimeout = new NumericUpDown();
			this.Label1 = new Label();
			this.Label2 = new Label();
			this.Label3 = new Label();
			this.Label4 = new Label();
			this.Label5 = new Label();
			this.Label14 = new Label();
			this.Label17 = new Label();
			this.dtpOffduty0 = new DateTimePicker();
			this.dtpOnduty0 = new DateTimePicker();
			this.Label7 = new Label();
			this.Label6 = new Label();
			this.grpbTwoTimes = new GroupBox();
			this.optReadCardTwoTimes = new RadioButton();
			this.optReadCardFourTimes = new RadioButton();
			this.btnOK = new Button();
			this.dtpOnduty2 = new DateTimePicker();
			this.Label11 = new Label();
			this.grpbFourtimes = new GroupBox();
			this.Label8 = new Label();
			this.dtpOnduty1 = new DateTimePicker();
			this.dtpOffduty1 = new DateTimePicker();
			this.Label9 = new Label();
			this.dtpOffduty2 = new DateTimePicker();
			this.Label10 = new Label();
			this.GroupBox2 = new GroupBox();
			this.btnCancel = new Button();
			this.GroupBox1.SuspendLayout();
			((ISupportInitialize)this.nudOvertimeTimeout).BeginInit();
			((ISupportInitialize)this.nudLeaveAbsenceTimeout).BeginInit();
			((ISupportInitialize)this.nudLeaveTimeout).BeginInit();
			((ISupportInitialize)this.nudLateAbsenceTimeout).BeginInit();
			((ISupportInitialize)this.nudLateTimeout).BeginInit();
			this.grpbTwoTimes.SuspendLayout();
			this.grpbFourtimes.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			base.SuspendLayout();
			this.btnOption.BackColor = Color.Transparent;
			this.btnOption.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOption, "btnOption");
			this.btnOption.ForeColor = Color.White;
			this.btnOption.Name = "btnOption";
			this.btnOption.UseVisualStyleBackColor = false;
			this.btnOption.Click += new EventHandler(this.btnOption_Click);
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.cboLeaveAbsenceDay);
			this.GroupBox1.Controls.Add(this.Label16);
			this.GroupBox1.Controls.Add(this.Label15);
			this.GroupBox1.Controls.Add(this.Label13);
			this.GroupBox1.Controls.Add(this.Label12);
			this.GroupBox1.Controls.Add(this.nudOvertimeTimeout);
			this.GroupBox1.Controls.Add(this.cboLateAbsenceDay);
			this.GroupBox1.Controls.Add(this.nudLeaveAbsenceTimeout);
			this.GroupBox1.Controls.Add(this.nudLeaveTimeout);
			this.GroupBox1.Controls.Add(this.nudLateAbsenceTimeout);
			this.GroupBox1.Controls.Add(this.nudLateTimeout);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.Label14);
			this.GroupBox1.Controls.Add(this.Label17);
			this.GroupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			this.cboLeaveAbsenceDay.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cboLeaveAbsenceDay, "cboLeaveAbsenceDay");
			this.cboLeaveAbsenceDay.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboLeaveAbsenceDay.Items"),
				componentResourceManager.GetString("cboLeaveAbsenceDay.Items1"),
				componentResourceManager.GetString("cboLeaveAbsenceDay.Items2")
			});
			this.cboLeaveAbsenceDay.Name = "cboLeaveAbsenceDay";
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
			NumericUpDown arg_5A2_0 = this.nudOvertimeTimeout;
			int[] array = new int[4];
			array[0] = 600;
			arg_5A2_0.Maximum = new decimal(array);
			this.nudOvertimeTimeout.Name = "nudOvertimeTimeout";
			this.nudOvertimeTimeout.ReadOnly = true;
			NumericUpDown arg_5DB_0 = this.nudOvertimeTimeout;
			int[] array2 = new int[4];
			array2[0] = 60;
			arg_5DB_0.Value = new decimal(array2);
			this.cboLateAbsenceDay.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cboLateAbsenceDay, "cboLateAbsenceDay");
			this.cboLateAbsenceDay.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboLateAbsenceDay.Items"),
				componentResourceManager.GetString("cboLateAbsenceDay.Items1"),
				componentResourceManager.GetString("cboLateAbsenceDay.Items2")
			});
			this.cboLateAbsenceDay.Name = "cboLateAbsenceDay";
			this.nudLeaveAbsenceTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudLeaveAbsenceTimeout, "nudLeaveAbsenceTimeout");
			NumericUpDown arg_693_0 = this.nudLeaveAbsenceTimeout;
			int[] array3 = new int[4];
			array3[0] = 600;
			arg_693_0.Maximum = new decimal(array3);
			this.nudLeaveAbsenceTimeout.Name = "nudLeaveAbsenceTimeout";
			this.nudLeaveAbsenceTimeout.ReadOnly = true;
			NumericUpDown arg_6CF_0 = this.nudLeaveAbsenceTimeout;
			int[] array4 = new int[4];
			array4[0] = 120;
			arg_6CF_0.Value = new decimal(array4);
			this.nudLeaveTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudLeaveTimeout, "nudLeaveTimeout");
			NumericUpDown arg_713_0 = this.nudLeaveTimeout;
			int[] array5 = new int[4];
			array5[0] = 600;
			arg_713_0.Maximum = new decimal(array5);
			this.nudLeaveTimeout.Name = "nudLeaveTimeout";
			this.nudLeaveTimeout.ReadOnly = true;
			NumericUpDown arg_74E_0 = this.nudLeaveTimeout;
			int[] array6 = new int[4];
			array6[0] = 5;
			arg_74E_0.Value = new decimal(array6);
			this.nudLateAbsenceTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudLateAbsenceTimeout, "nudLateAbsenceTimeout");
			NumericUpDown arg_792_0 = this.nudLateAbsenceTimeout;
			int[] array7 = new int[4];
			array7[0] = 600;
			arg_792_0.Maximum = new decimal(array7);
			this.nudLateAbsenceTimeout.Name = "nudLateAbsenceTimeout";
			this.nudLateAbsenceTimeout.ReadOnly = true;
			NumericUpDown arg_7CE_0 = this.nudLateAbsenceTimeout;
			int[] array8 = new int[4];
			array8[0] = 120;
			arg_7CE_0.Value = new decimal(array8);
			this.nudLateTimeout.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudLateTimeout, "nudLateTimeout");
			NumericUpDown arg_812_0 = this.nudLateTimeout;
			int[] array9 = new int[4];
			array9[0] = 600;
			arg_812_0.Maximum = new decimal(array9);
			this.nudLateTimeout.Name = "nudLateTimeout";
			this.nudLateTimeout.ReadOnly = true;
			NumericUpDown arg_84D_0 = this.nudLateTimeout;
			int[] array10 = new int[4];
			array10[0] = 5;
			arg_84D_0.Value = new decimal(array10);
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.Name = "Label2";
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.Name = "Label4";
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.Label14, "Label14");
			this.Label14.Name = "Label14";
			componentResourceManager.ApplyResources(this.Label17, "Label17");
			this.Label17.Name = "Label17";
			componentResourceManager.ApplyResources(this.dtpOffduty0, "dtpOffduty0");
			this.dtpOffduty0.Name = "dtpOffduty0";
			this.dtpOffduty0.ShowUpDown = true;
			this.dtpOffduty0.Value = new DateTime(2004, 7, 18, 17, 30, 0, 0);
			componentResourceManager.ApplyResources(this.dtpOnduty0, "dtpOnduty0");
			this.dtpOnduty0.Name = "dtpOnduty0";
			this.dtpOnduty0.ShowUpDown = true;
			this.dtpOnduty0.Value = new DateTime(2004, 7, 18, 8, 30, 0, 0);
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.Name = "Label7";
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.Name = "Label6";
			this.grpbTwoTimes.Controls.Add(this.Label6);
			this.grpbTwoTimes.Controls.Add(this.dtpOnduty0);
			this.grpbTwoTimes.Controls.Add(this.dtpOffduty0);
			this.grpbTwoTimes.Controls.Add(this.Label7);
			componentResourceManager.ApplyResources(this.grpbTwoTimes, "grpbTwoTimes");
			this.grpbTwoTimes.Name = "grpbTwoTimes";
			this.grpbTwoTimes.TabStop = false;
			this.optReadCardTwoTimes.Checked = true;
			componentResourceManager.ApplyResources(this.optReadCardTwoTimes, "optReadCardTwoTimes");
			this.optReadCardTwoTimes.Name = "optReadCardTwoTimes";
			this.optReadCardTwoTimes.TabStop = true;
			this.optReadCardTwoTimes.CheckedChanged += new EventHandler(this.optReadCardTwoTimes_CheckedChanged);
			componentResourceManager.ApplyResources(this.optReadCardFourTimes, "optReadCardFourTimes");
			this.optReadCardFourTimes.Name = "optReadCardFourTimes";
			this.optReadCardFourTimes.CheckedChanged += new EventHandler(this.optReadCardFourTimes_CheckedChanged);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.dtpOnduty2, "dtpOnduty2");
			this.dtpOnduty2.Name = "dtpOnduty2";
			this.dtpOnduty2.ShowUpDown = true;
			this.dtpOnduty2.Value = new DateTime(2004, 7, 18, 13, 30, 0, 0);
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.Name = "Label11";
			this.grpbFourtimes.Controls.Add(this.Label8);
			this.grpbFourtimes.Controls.Add(this.dtpOnduty1);
			this.grpbFourtimes.Controls.Add(this.dtpOffduty1);
			this.grpbFourtimes.Controls.Add(this.Label9);
			this.grpbFourtimes.Controls.Add(this.dtpOffduty2);
			this.grpbFourtimes.Controls.Add(this.Label10);
			this.grpbFourtimes.Controls.Add(this.Label11);
			this.grpbFourtimes.Controls.Add(this.dtpOnduty2);
			componentResourceManager.ApplyResources(this.grpbFourtimes, "grpbFourtimes");
			this.grpbFourtimes.Name = "grpbFourtimes";
			this.grpbFourtimes.TabStop = false;
			componentResourceManager.ApplyResources(this.Label8, "Label8");
			this.Label8.Name = "Label8";
			componentResourceManager.ApplyResources(this.dtpOnduty1, "dtpOnduty1");
			this.dtpOnduty1.Name = "dtpOnduty1";
			this.dtpOnduty1.ShowUpDown = true;
			this.dtpOnduty1.Value = new DateTime(2004, 7, 18, 8, 30, 0, 0);
			componentResourceManager.ApplyResources(this.dtpOffduty1, "dtpOffduty1");
			this.dtpOffduty1.Name = "dtpOffduty1";
			this.dtpOffduty1.ShowUpDown = true;
			this.dtpOffduty1.Value = new DateTime(2004, 7, 18, 12, 0, 0, 0);
			componentResourceManager.ApplyResources(this.Label9, "Label9");
			this.Label9.Name = "Label9";
			componentResourceManager.ApplyResources(this.dtpOffduty2, "dtpOffduty2");
			this.dtpOffduty2.Name = "dtpOffduty2";
			this.dtpOffduty2.ShowUpDown = true;
			this.dtpOffduty2.Value = new DateTime(2004, 7, 18, 17, 30, 0, 0);
			componentResourceManager.ApplyResources(this.Label10, "Label10");
			this.Label10.Name = "Label10";
			this.GroupBox2.BackColor = Color.Transparent;
			this.GroupBox2.Controls.Add(this.grpbFourtimes);
			this.GroupBox2.Controls.Add(this.grpbTwoTimes);
			this.GroupBox2.Controls.Add(this.optReadCardTwoTimes);
			this.GroupBox2.Controls.Add(this.optReadCardFourTimes);
			this.GroupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox2, "GroupBox2");
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.TabStop = false;
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnOption);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.GroupBox2);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftNormalParamSet";
			base.Load += new EventHandler(this.dfrmShiftNormalParamSet_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmShiftNormalParamSet_KeyDown);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			((ISupportInitialize)this.nudOvertimeTimeout).EndInit();
			((ISupportInitialize)this.nudLeaveAbsenceTimeout).EndInit();
			((ISupportInitialize)this.nudLeaveTimeout).EndInit();
			((ISupportInitialize)this.nudLateAbsenceTimeout).EndInit();
			((ISupportInitialize)this.nudLateTimeout).EndInit();
			this.grpbTwoTimes.ResumeLayout(false);
			this.grpbFourtimes.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public dfrmShiftNormalParamSet()
		{
			this.InitializeComponent();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuShiftNormalConfigure";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnOK.Visible = false;
			}
		}

		private void dfrmShiftNormalParamSet_Load(object sender, EventArgs e)
		{
			this.dtpOnduty0.CustomFormat = "HH:mm";
			this.dtpOnduty0.Format = DateTimePickerFormat.Custom;
			this.dtpOnduty0.Value = DateTime.Parse("08:30:00");
			this.dtpOffduty0.CustomFormat = "HH:mm";
			this.dtpOffduty0.Format = DateTimePickerFormat.Custom;
			this.dtpOffduty0.Value = DateTime.Parse("17:30:00");
			this.dtpOnduty1.CustomFormat = "HH:mm";
			this.dtpOnduty1.Format = DateTimePickerFormat.Custom;
			this.dtpOnduty1.Value = DateTime.Parse("08:30:00");
			this.dtpOffduty1.CustomFormat = "HH:mm";
			this.dtpOffduty1.Format = DateTimePickerFormat.Custom;
			this.dtpOffduty1.Value = DateTime.Parse("12:00:00");
			this.dtpOnduty2.CustomFormat = "HH:mm";
			this.dtpOnduty2.Format = DateTimePickerFormat.Custom;
			this.dtpOnduty2.Value = DateTime.Parse("13:30:00");
			this.dtpOffduty2.CustomFormat = "HH:mm";
			this.dtpOffduty2.Format = DateTimePickerFormat.Custom;
			this.dtpOffduty2.Value = DateTime.Parse("17:30:00");
			this.loadOperatorPrivilege();
			this.getAttendanceParam();
			try
			{
				if (!(wgAppConfig.getSystemParamByNO(55).ToString() == "00:00") && !(wgAppConfig.getSystemParamByNO(55).ToString() == "00:00:00"))
				{
					this.btnOption.Visible = true;
				}
				if (wgAppConfig.getSystemParamByNO(56).ToString() == "1")
				{
					this.btnOption.Visible = true;
				}
				if (wgAppConfig.getSystemParamByNO(57).ToString() == "1")
				{
					this.btnOption.Visible = true;
				}
				if (wgAppConfig.getSystemParamByNO(54).ToString() == "1")
				{
					this.btnOption.Visible = true;
				}
				if (wgAppConfig.getSystemParamByNO(59).ToString() == "1")
				{
					this.btnOption.Visible = true;
				}
			}
			catch
			{
			}
		}

		private void getAttendanceParam()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.getAttendanceParam_Acc();
				return;
			}
			string cmdText = "SELECT * FROM t_a_Attendence";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						switch ((int)sqlDataReader["f_No"])
						{
						case 1:
							this.nudLateTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 2:
							this.nudLateAbsenceTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 3:
							this.cboLateAbsenceDay.SelectedIndex = (int)(decimal.Parse(sqlDataReader["f_Value"].ToString(), CultureInfo.InvariantCulture) * 2m);
							break;
						case 4:
							this.nudLeaveTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 5:
							this.nudLeaveAbsenceTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 6:
							this.cboLeaveAbsenceDay.SelectedIndex = (int)(decimal.Parse(sqlDataReader["f_Value"].ToString(), CultureInfo.InvariantCulture) * 2m);
							break;
						case 7:
							this.nudOvertimeTimeout.Value = int.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 8:
							this.dtpOnduty0.Value = DateTime.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 9:
							this.dtpOffduty0.Value = DateTime.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 10:
							this.dtpOnduty1.Value = DateTime.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 11:
							this.dtpOffduty1.Value = DateTime.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 12:
							this.dtpOnduty2.Value = DateTime.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 13:
							this.dtpOffduty2.Value = DateTime.Parse((string)sqlDataReader["f_Value"]);
							break;
						case 14:
							if (int.Parse((string)sqlDataReader["f_Value"]) == 4)
							{
								this.optReadCardFourTimes.Checked = true;
								this.grpbFourtimes.Visible = true;
								this.grpbTwoTimes.Visible = false;
							}
							else
							{
								this.optReadCardTwoTimes.Checked = true;
								this.grpbFourtimes.Visible = false;
								this.grpbTwoTimes.Visible = true;
							}
							break;
						}
					}
					sqlDataReader.Close();
				}
			}
		}

		private void getAttendanceParam_Acc()
		{
			string cmdText = "SELECT * FROM t_a_Attendence";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
					{
						switch ((int)oleDbDataReader["f_No"])
						{
						case 1:
							this.nudLateTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 2:
							this.nudLateAbsenceTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 3:
							this.cboLateAbsenceDay.SelectedIndex = (int)(decimal.Parse(oleDbDataReader["f_Value"].ToString(), CultureInfo.InvariantCulture) * 2m);
							break;
						case 4:
							this.nudLeaveTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 5:
							this.nudLeaveAbsenceTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 6:
							this.cboLeaveAbsenceDay.SelectedIndex = (int)(decimal.Parse(oleDbDataReader["f_Value"].ToString(), CultureInfo.InvariantCulture) * 2m);
							break;
						case 7:
							this.nudOvertimeTimeout.Value = int.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 8:
							this.dtpOnduty0.Value = DateTime.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 9:
							this.dtpOffduty0.Value = DateTime.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 10:
							this.dtpOnduty1.Value = DateTime.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 11:
							this.dtpOffduty1.Value = DateTime.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 12:
							this.dtpOnduty2.Value = DateTime.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 13:
							this.dtpOffduty2.Value = DateTime.Parse((string)oleDbDataReader["f_Value"]);
							break;
						case 14:
							if (int.Parse((string)oleDbDataReader["f_Value"]) == 4)
							{
								this.optReadCardFourTimes.Checked = true;
								this.grpbFourtimes.Visible = true;
								this.grpbTwoTimes.Visible = false;
							}
							else
							{
								this.optReadCardTwoTimes.Checked = true;
								this.grpbFourtimes.Visible = false;
								this.grpbTwoTimes.Visible = true;
							}
							break;
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
			if (this.nudLateTimeout.Value >= this.nudLateAbsenceTimeout.Value)
			{
				XMessageBox.Show(this, CommonStr.strShiftNormalParamSet1, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.optReadCardTwoTimes.Checked && this.dtpOffduty0.Value <= this.dtpOnduty0.Value)
			{
				XMessageBox.Show(this, CommonStr.strShiftNormalParamSet2, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.optReadCardFourTimes.Checked)
			{
				if (this.dtpOffduty1.Value <= this.dtpOnduty1.Value)
				{
					XMessageBox.Show(this, CommonStr.strShiftNormalParamSet2, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.dtpOffduty2.Value <= this.dtpOnduty2.Value)
				{
					XMessageBox.Show(this, CommonStr.strShiftNormalParamSet2, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.dtpOffduty2.Value <= this.dtpOnduty1.Value)
				{
					XMessageBox.Show(this, CommonStr.strShiftNormalParamSet2, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			for (int i = 1; i <= 14; i++)
			{
				switch (i)
				{
				case 1:
					this.setAttendanceParam(i, this.nudLateTimeout.Value.ToString());
					break;
				case 2:
					this.setAttendanceParam(i, this.nudLateAbsenceTimeout.Value.ToString());
					break;
				case 3:
					this.setAttendanceParam(i, (this.cboLateAbsenceDay.SelectedIndex / 2m).ToString(CultureInfo.InvariantCulture));
					break;
				case 4:
					this.setAttendanceParam(i, this.nudLeaveTimeout.Value.ToString());
					break;
				case 5:
					this.setAttendanceParam(i, this.nudLeaveAbsenceTimeout.Value.ToString());
					break;
				case 6:
					this.setAttendanceParam(i, (this.cboLeaveAbsenceDay.SelectedIndex / 2m).ToString(CultureInfo.InvariantCulture));
					break;
				case 7:
					this.setAttendanceParam(i, this.nudOvertimeTimeout.Value.ToString());
					break;
				case 8:
					this.setAttendanceParam(i, this.dtpOnduty0.Value.ToString(wgTools.YMDHMSFormat));
					break;
				case 9:
					this.setAttendanceParam(i, this.dtpOffduty0.Value.ToString(wgTools.YMDHMSFormat));
					break;
				case 10:
					this.setAttendanceParam(i, this.dtpOnduty1.Value.ToString(wgTools.YMDHMSFormat));
					break;
				case 11:
					this.setAttendanceParam(i, this.dtpOffduty1.Value.ToString(wgTools.YMDHMSFormat));
					break;
				case 12:
					this.setAttendanceParam(i, this.dtpOnduty2.Value.ToString(wgTools.YMDHMSFormat));
					break;
				case 13:
					this.setAttendanceParam(i, this.dtpOffduty2.Value.ToString(wgTools.YMDHMSFormat));
					break;
				case 14:
					if (this.optReadCardTwoTimes.Checked)
					{
						this.setAttendanceParam(i, "2");
					}
					else
					{
						this.setAttendanceParam(i, "4");
					}
					break;
				}
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void setAttendanceParam(int no, string val)
		{
			string text = "UPDATE t_a_Attendence ";
			text = text + " SET [f_value]=" + wgTools.PrepareStr(val);
			text = text + " WHERE [f_NO]= " + no.ToString();
			wgAppConfig.runUpdateSql(text);
		}

		private void optReadCardTwoTimes_CheckedChanged(object sender, EventArgs e)
		{
			this.grpbTwoTimes.Visible = this.optReadCardTwoTimes.Checked;
			this.grpbFourtimes.Visible = this.optReadCardFourTimes.Checked;
		}

		private void optReadCardFourTimes_CheckedChanged(object sender, EventArgs e)
		{
			this.grpbTwoTimes.Visible = this.optReadCardTwoTimes.Checked;
			this.grpbFourtimes.Visible = this.optReadCardFourTimes.Checked;
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
			using (dfrmShiftNormalOption dfrmShiftNormalOption = new dfrmShiftNormalOption())
			{
				dfrmShiftNormalOption.ShowDialog();
			}
		}

		private void funcCtrlShiftQ()
		{
			this.btnOption.Visible = true;
		}

		private void dfrmShiftNormalParamSet_KeyDown(object sender, KeyEventArgs e)
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
	}
}
