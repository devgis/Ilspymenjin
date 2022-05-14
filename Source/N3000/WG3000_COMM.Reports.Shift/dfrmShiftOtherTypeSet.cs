using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftOtherTypeSet : frmN3000
	{
		private IContainer components;

		internal CheckBox chkBOvertimeShift;

		internal TextBox txtName;

		internal ComboBox cbof_ShiftID;

		internal Label Label1;

		internal Label Label8;

		internal ComboBox cbof_Readtimes;

		internal Label Label11;

		internal CheckBox chkBOvertimeShift1;

		internal GroupBox groupBox1;

		internal Label Label6;

		internal DateTimePicker dateBeginHMS1;

		internal DateTimePicker dateEndHMS1;

		internal Label Label7;

		internal GroupBox groupBox2;

		internal Label label2;

		internal CheckBox chkBOvertimeShift2;

		internal DateTimePicker dateBeginHMS2;

		internal DateTimePicker dateEndHMS2;

		internal Label label3;

		internal GroupBox groupBox3;

		internal Label label4;

		internal CheckBox chkBOvertimeShift3;

		internal DateTimePicker dateBeginHMS3;

		internal DateTimePicker dateEndHMS3;

		internal Label label5;

		internal GroupBox groupBox4;

		internal Label label9;

		internal CheckBox chkBOvertimeShift4;

		internal DateTimePicker dateBeginHMS4;

		internal DateTimePicker dateEndHMS4;

		internal Label label10;

		internal Button cmdCancel;

		internal Button cmdOK;

		public string operateMode = "";

		public int curShiftID;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftOtherTypeSet));
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.groupBox1 = new GroupBox();
			this.Label6 = new Label();
			this.chkBOvertimeShift1 = new CheckBox();
			this.dateBeginHMS1 = new DateTimePicker();
			this.dateEndHMS1 = new DateTimePicker();
			this.Label7 = new Label();
			this.chkBOvertimeShift = new CheckBox();
			this.txtName = new TextBox();
			this.cbof_ShiftID = new ComboBox();
			this.Label1 = new Label();
			this.Label8 = new Label();
			this.cbof_Readtimes = new ComboBox();
			this.Label11 = new Label();
			this.groupBox2 = new GroupBox();
			this.label2 = new Label();
			this.chkBOvertimeShift2 = new CheckBox();
			this.dateBeginHMS2 = new DateTimePicker();
			this.dateEndHMS2 = new DateTimePicker();
			this.label3 = new Label();
			this.groupBox3 = new GroupBox();
			this.label4 = new Label();
			this.chkBOvertimeShift3 = new CheckBox();
			this.dateBeginHMS3 = new DateTimePicker();
			this.dateEndHMS3 = new DateTimePicker();
			this.label5 = new Label();
			this.groupBox4 = new GroupBox();
			this.label9 = new Label();
			this.chkBOvertimeShift4 = new CheckBox();
			this.dateBeginHMS4 = new DateTimePicker();
			this.dateEndHMS4 = new DateTimePicker();
			this.label10 = new Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			base.SuspendLayout();
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.Label6);
			this.groupBox1.Controls.Add(this.chkBOvertimeShift1);
			this.groupBox1.Controls.Add(this.dateBeginHMS1);
			this.groupBox1.Controls.Add(this.dateEndHMS1);
			this.groupBox1.Controls.Add(this.Label7);
			this.groupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.chkBOvertimeShift1, "chkBOvertimeShift1");
			this.chkBOvertimeShift1.Name = "chkBOvertimeShift1";
			componentResourceManager.ApplyResources(this.dateBeginHMS1, "dateBeginHMS1");
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS1, "dateEndHMS1");
			this.dateEndHMS1.Name = "dateEndHMS1";
			this.dateEndHMS1.ShowUpDown = true;
			this.dateEndHMS1.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.Name = "Label7";
			this.chkBOvertimeShift.BackColor = Color.Transparent;
			this.chkBOvertimeShift.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chkBOvertimeShift, "chkBOvertimeShift");
			this.chkBOvertimeShift.Name = "chkBOvertimeShift";
			this.chkBOvertimeShift.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.txtName, "txtName");
			this.txtName.Name = "txtName";
			this.cbof_ShiftID.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_ShiftID, "cbof_ShiftID");
			this.cbof_ShiftID.Name = "cbof_ShiftID";
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			this.Label8.BackColor = Color.Transparent;
			this.Label8.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label8, "Label8");
			this.Label8.Name = "Label8";
			this.cbof_Readtimes.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_Readtimes, "cbof_Readtimes");
			this.cbof_Readtimes.Name = "cbof_Readtimes";
			this.cbof_Readtimes.SelectedIndexChanged += new EventHandler(this.cbof_Readtimes_SelectedIndexChanged);
			this.Label11.BackColor = Color.Transparent;
			this.Label11.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label11, "Label11");
			this.Label11.Name = "Label11";
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.chkBOvertimeShift2);
			this.groupBox2.Controls.Add(this.dateBeginHMS2);
			this.groupBox2.Controls.Add(this.dateEndHMS2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.chkBOvertimeShift2, "chkBOvertimeShift2");
			this.chkBOvertimeShift2.Name = "chkBOvertimeShift2";
			componentResourceManager.ApplyResources(this.dateBeginHMS2, "dateBeginHMS2");
			this.dateBeginHMS2.Name = "dateBeginHMS2";
			this.dateBeginHMS2.ShowUpDown = true;
			this.dateBeginHMS2.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS2, "dateEndHMS2");
			this.dateEndHMS2.Name = "dateEndHMS2";
			this.dateEndHMS2.ShowUpDown = true;
			this.dateEndHMS2.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.groupBox3.BackColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.chkBOvertimeShift3);
			this.groupBox3.Controls.Add(this.dateBeginHMS3);
			this.groupBox3.Controls.Add(this.dateEndHMS3);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.chkBOvertimeShift3, "chkBOvertimeShift3");
			this.chkBOvertimeShift3.Name = "chkBOvertimeShift3";
			componentResourceManager.ApplyResources(this.dateBeginHMS3, "dateBeginHMS3");
			this.dateBeginHMS3.Name = "dateBeginHMS3";
			this.dateBeginHMS3.ShowUpDown = true;
			this.dateBeginHMS3.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS3, "dateEndHMS3");
			this.dateEndHMS3.Name = "dateEndHMS3";
			this.dateEndHMS3.ShowUpDown = true;
			this.dateEndHMS3.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			this.groupBox4.BackColor = Color.Transparent;
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.chkBOvertimeShift4);
			this.groupBox4.Controls.Add(this.dateBeginHMS4);
			this.groupBox4.Controls.Add(this.dateEndHMS4);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			componentResourceManager.ApplyResources(this.chkBOvertimeShift4, "chkBOvertimeShift4");
			this.chkBOvertimeShift4.Name = "chkBOvertimeShift4";
			componentResourceManager.ApplyResources(this.dateBeginHMS4, "dateBeginHMS4");
			this.dateBeginHMS4.Name = "dateBeginHMS4";
			this.dateBeginHMS4.ShowUpDown = true;
			this.dateBeginHMS4.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS4, "dateEndHMS4");
			this.dateEndHMS4.Name = "dateEndHMS4";
			this.dateEndHMS4.ShowUpDown = true;
			this.dateEndHMS4.Value = new DateTime(2010, 2, 28, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.chkBOvertimeShift);
			base.Controls.Add(this.txtName);
			base.Controls.Add(this.cbof_ShiftID);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.Label8);
			base.Controls.Add(this.cbof_Readtimes);
			base.Controls.Add(this.Label11);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftOtherTypeSet";
			base.Load += new EventHandler(this.dfrmShiftOtherTypeSet_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmShiftOtherTypeSet()
		{
			this.InitializeComponent();
		}

		private void dfrmShiftOtherTypeSet_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmShiftOtherTypeSet_Load_Acc(sender, e);
				return;
			}
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("00:00:00");
			this.dateBeginHMS2.CustomFormat = "HH:mm";
			this.dateBeginHMS2.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS2.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS2.CustomFormat = "HH:mm";
			this.dateEndHMS2.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS2.Value = DateTime.Parse("00:00:00");
			this.dateBeginHMS3.CustomFormat = "HH:mm";
			this.dateBeginHMS3.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS3.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS3.CustomFormat = "HH:mm";
			this.dateEndHMS3.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS3.Value = DateTime.Parse("00:00:00");
			this.dateBeginHMS4.CustomFormat = "HH:mm";
			this.dateBeginHMS4.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS4.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS4.CustomFormat = "HH:mm";
			this.dateEndHMS4.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS4.Value = DateTime.Parse("00:00:00");
			this.cbof_ShiftID.Items.Clear();
			for (int i = 1; i <= 99; i++)
			{
				this.cbof_ShiftID.Items.Add(i);
			}
			this.cbof_Readtimes.Items.Clear();
			this.cbof_Readtimes.Items.Add(2);
			this.cbof_Readtimes.Items.Add(4);
			this.cbof_Readtimes.Items.Add(6);
			this.cbof_Readtimes.Items.Add(8);
			if (this.operateMode == "New")
			{
				this.cbof_ShiftID.Enabled = true;
				string cmdText = "SELECT f_ShiftID FROM t_b_ShiftSet  ORDER BY [f_ShiftID] ASC ";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						while (sqlDataReader.Read())
						{
							int num = this.cbof_ShiftID.Items.IndexOf((int)sqlDataReader[0]);
							if (num >= 0)
							{
								this.cbof_ShiftID.Items.RemoveAt(num);
							}
						}
						sqlDataReader.Close();
					}
				}
				if (this.cbof_ShiftID.Items.Count == 0)
				{
					base.Close();
				}
				this.cbof_ShiftID.Text = this.cbof_ShiftID.Items[0].ToString();
				this.curShiftID = int.Parse(this.cbof_ShiftID.Text);
				this.cbof_Readtimes.Text = this.cbof_Readtimes.Items[0].ToString();
			}
			else
			{
				this.cbof_ShiftID.Enabled = false;
				this.cbof_ShiftID.Text = this.curShiftID.ToString();
				string cmdText = " SELECT * FROM t_b_ShiftSet WHERE [f_ShiftID]= " + this.curShiftID.ToString();
				using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand2 = new SqlCommand(cmdText, sqlConnection2))
					{
						sqlConnection2.Open();
						SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
						if (sqlDataReader2.Read())
						{
							DateTime value = DateTime.Parse("2010-3-10 00:00:00");
							if (DateTime.TryParse(sqlDataReader2["f_OnDuty1"].ToString(), out value))
							{
								this.dateBeginHMS1.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OnDuty2"].ToString(), out value))
							{
								this.dateBeginHMS2.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OnDuty3"].ToString(), out value))
							{
								this.dateBeginHMS3.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OnDuty4"].ToString(), out value))
							{
								this.dateBeginHMS4.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OffDuty1"].ToString(), out value))
							{
								this.dateEndHMS1.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OffDuty2"].ToString(), out value))
							{
								this.dateEndHMS2.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OffDuty3"].ToString(), out value))
							{
								this.dateEndHMS3.Value = value;
							}
							if (DateTime.TryParse(sqlDataReader2["f_OffDuty4"].ToString(), out value))
							{
								this.dateEndHMS4.Value = value;
							}
							this.cbof_Readtimes.Text = sqlDataReader2["f_Readtimes"].ToString();
							this.txtName.Text = wgTools.SetObjToStr(sqlDataReader2["f_ShiftName"].ToString());
							this.chkBOvertimeShift.Checked = (int.Parse(sqlDataReader2["f_bOvertimeShift"].ToString()) == 1);
							this.chkBOvertimeShift1.Checked = (int.Parse(sqlDataReader2["f_bOvertimeShift"].ToString()) == 2);
							this.chkBOvertimeShift2.Checked = (int.Parse(sqlDataReader2["f_bOvertimeShift"].ToString()) == 2);
							this.chkBOvertimeShift3.Checked = (int.Parse(sqlDataReader2["f_bOvertimeShift"].ToString()) == 2);
							this.chkBOvertimeShift4.Checked = (int.Parse(sqlDataReader2["f_bOvertimeShift"].ToString()) == 2);
						}
						sqlDataReader2.Close();
					}
				}
			}
			this.cbof_Readtimes_SelectedIndexChanged(null, null);
		}

		private void dfrmShiftOtherTypeSet_Load_Acc(object sender, EventArgs e)
		{
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("00:00:00");
			this.dateBeginHMS2.CustomFormat = "HH:mm";
			this.dateBeginHMS2.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS2.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS2.CustomFormat = "HH:mm";
			this.dateEndHMS2.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS2.Value = DateTime.Parse("00:00:00");
			this.dateBeginHMS3.CustomFormat = "HH:mm";
			this.dateBeginHMS3.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS3.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS3.CustomFormat = "HH:mm";
			this.dateEndHMS3.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS3.Value = DateTime.Parse("00:00:00");
			this.dateBeginHMS4.CustomFormat = "HH:mm";
			this.dateBeginHMS4.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS4.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS4.CustomFormat = "HH:mm";
			this.dateEndHMS4.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS4.Value = DateTime.Parse("00:00:00");
			this.cbof_ShiftID.Items.Clear();
			for (int i = 1; i <= 99; i++)
			{
				this.cbof_ShiftID.Items.Add(i);
			}
			this.cbof_Readtimes.Items.Clear();
			this.cbof_Readtimes.Items.Add(2);
			this.cbof_Readtimes.Items.Add(4);
			this.cbof_Readtimes.Items.Add(6);
			this.cbof_Readtimes.Items.Add(8);
			if (this.operateMode == "New")
			{
				this.cbof_ShiftID.Enabled = true;
				string cmdText = "SELECT f_ShiftID FROM t_b_ShiftSet  ORDER BY [f_ShiftID] ASC ";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						oleDbConnection.Open();
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						while (oleDbDataReader.Read())
						{
							int num = this.cbof_ShiftID.Items.IndexOf((int)oleDbDataReader[0]);
							if (num >= 0)
							{
								this.cbof_ShiftID.Items.RemoveAt(num);
							}
						}
						oleDbDataReader.Close();
					}
				}
				if (this.cbof_ShiftID.Items.Count == 0)
				{
					base.Close();
				}
				this.cbof_ShiftID.Text = this.cbof_ShiftID.Items[0].ToString();
				this.curShiftID = int.Parse(this.cbof_ShiftID.Text);
				this.cbof_Readtimes.Text = this.cbof_Readtimes.Items[0].ToString();
			}
			else
			{
				this.cbof_ShiftID.Enabled = false;
				this.cbof_ShiftID.Text = this.curShiftID.ToString();
				string cmdText = " SELECT * FROM t_b_ShiftSet WHERE [f_ShiftID]= " + this.curShiftID.ToString();
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(cmdText, oleDbConnection2))
					{
						oleDbConnection2.Open();
						OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader();
						if (oleDbDataReader2.Read())
						{
							DateTime value = DateTime.Parse("2010-3-10 00:00:00");
							if (DateTime.TryParse(oleDbDataReader2["f_OnDuty1"].ToString(), out value))
							{
								this.dateBeginHMS1.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OnDuty2"].ToString(), out value))
							{
								this.dateBeginHMS2.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OnDuty3"].ToString(), out value))
							{
								this.dateBeginHMS3.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OnDuty4"].ToString(), out value))
							{
								this.dateBeginHMS4.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OffDuty1"].ToString(), out value))
							{
								this.dateEndHMS1.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OffDuty2"].ToString(), out value))
							{
								this.dateEndHMS2.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OffDuty3"].ToString(), out value))
							{
								this.dateEndHMS3.Value = value;
							}
							if (DateTime.TryParse(oleDbDataReader2["f_OffDuty4"].ToString(), out value))
							{
								this.dateEndHMS4.Value = value;
							}
							this.cbof_Readtimes.Text = oleDbDataReader2["f_Readtimes"].ToString();
							this.txtName.Text = wgTools.SetObjToStr(oleDbDataReader2["f_ShiftName"].ToString());
							this.chkBOvertimeShift.Checked = (int.Parse(oleDbDataReader2["f_bOvertimeShift"].ToString()) == 1);
							this.chkBOvertimeShift1.Checked = (int.Parse(oleDbDataReader2["f_bOvertimeShift"].ToString()) == 2);
							this.chkBOvertimeShift2.Checked = (int.Parse(oleDbDataReader2["f_bOvertimeShift"].ToString()) == 2);
							this.chkBOvertimeShift3.Checked = (int.Parse(oleDbDataReader2["f_bOvertimeShift"].ToString()) == 2);
							this.chkBOvertimeShift4.Checked = (int.Parse(oleDbDataReader2["f_bOvertimeShift"].ToString()) == 2);
						}
						oleDbDataReader2.Close();
					}
				}
			}
			this.cbof_Readtimes_SelectedIndexChanged(null, null);
		}

		private void cbof_Readtimes_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = this.chkBOvertimeShift1;
			this.groupBox1.Visible = false;
			this.groupBox2.Visible = false;
			this.groupBox3.Visible = false;
			this.groupBox4.Visible = false;
			this.chkBOvertimeShift1.Visible = false;
			this.chkBOvertimeShift2.Visible = false;
			this.chkBOvertimeShift3.Visible = false;
			this.chkBOvertimeShift4.Visible = false;
			if (!string.IsNullOrEmpty(this.cbof_Readtimes.Text))
			{
				if (int.Parse(this.cbof_Readtimes.Text) >= 2)
				{
					this.groupBox1.Visible = true;
					checkBox = this.chkBOvertimeShift1;
				}
				if (int.Parse(this.cbof_Readtimes.Text) >= 4)
				{
					this.groupBox2.Visible = true;
					checkBox = this.chkBOvertimeShift2;
				}
				if (int.Parse(this.cbof_Readtimes.Text) >= 6)
				{
					this.groupBox3.Visible = true;
					checkBox = this.chkBOvertimeShift3;
				}
				if (int.Parse(this.cbof_Readtimes.Text) >= 8)
				{
					this.groupBox4.Visible = true;
					checkBox = this.chkBOvertimeShift4;
				}
			}
			checkBox.Visible = true;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.cmdOK_Click_Acc(sender, e);
				return;
			}
			int bOvertimeShift = 0;
			if (this.chkBOvertimeShift.Checked)
			{
				bOvertimeShift = 1;
			}
			else if (this.chkBOvertimeShift1.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift1.Checked ? 2 : 0);
			}
			else if (this.chkBOvertimeShift2.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift2.Checked ? 2 : 0);
			}
			else if (this.chkBOvertimeShift3.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift3.Checked ? 2 : 0);
			}
			else if (this.chkBOvertimeShift4.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift4.Checked ? 2 : 0);
			}
			if (this.operateMode == "New")
			{
				using (comShift comShift = new comShift())
				{
					int num = comShift.shift_add(int.Parse(this.cbof_ShiftID.Text), this.txtName.Text.Trim(), int.Parse(this.cbof_Readtimes.Text), this.dateBeginHMS1.Value, this.dateEndHMS1.Value, this.dateBeginHMS2.Value, this.dateEndHMS2.Value, this.dateBeginHMS3.Value, this.dateEndHMS3.Value, this.dateBeginHMS4.Value, this.dateEndHMS4.Value, bOvertimeShift);
					if (num != 0)
					{
						XMessageBox.Show(this, comShift.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						base.DialogResult = DialogResult.OK;
						base.Close();
					}
					return;
				}
			}
			using (comShift comShift2 = new comShift())
			{
				int num = comShift2.shift_update(int.Parse(this.cbof_ShiftID.Text), this.txtName.Text.Trim(), int.Parse(this.cbof_Readtimes.Text), this.dateBeginHMS1.Value, this.dateEndHMS1.Value, this.dateBeginHMS2.Value, this.dateEndHMS2.Value, this.dateBeginHMS3.Value, this.dateEndHMS3.Value, this.dateBeginHMS4.Value, this.dateEndHMS4.Value, bOvertimeShift);
				if (num != 0)
				{
					XMessageBox.Show(this, comShift2.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
		}

		private void cmdOK_Click_Acc(object sender, EventArgs e)
		{
			int bOvertimeShift = 0;
			if (this.chkBOvertimeShift.Checked)
			{
				bOvertimeShift = 1;
			}
			else if (this.chkBOvertimeShift1.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift1.Checked ? 2 : 0);
			}
			else if (this.chkBOvertimeShift2.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift2.Checked ? 2 : 0);
			}
			else if (this.chkBOvertimeShift3.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift3.Checked ? 2 : 0);
			}
			else if (this.chkBOvertimeShift4.Visible)
			{
				bOvertimeShift = (this.chkBOvertimeShift4.Checked ? 2 : 0);
			}
			if (this.operateMode == "New")
			{
				using (comShift_Acc comShift_Acc = new comShift_Acc())
				{
					int num = comShift_Acc.shift_add(int.Parse(this.cbof_ShiftID.Text), this.txtName.Text.Trim(), int.Parse(this.cbof_Readtimes.Text), this.dateBeginHMS1.Value, this.dateEndHMS1.Value, this.dateBeginHMS2.Value, this.dateEndHMS2.Value, this.dateBeginHMS3.Value, this.dateEndHMS3.Value, this.dateBeginHMS4.Value, this.dateEndHMS4.Value, bOvertimeShift);
					if (num != 0)
					{
						XMessageBox.Show(this, comShift_Acc.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						base.DialogResult = DialogResult.OK;
						base.Close();
					}
					return;
				}
			}
			using (comShift_Acc comShift_Acc2 = new comShift_Acc())
			{
				int num = comShift_Acc2.shift_update(int.Parse(this.cbof_ShiftID.Text), this.txtName.Text.Trim(), int.Parse(this.cbof_Readtimes.Text), this.dateBeginHMS1.Value, this.dateEndHMS1.Value, this.dateBeginHMS2.Value, this.dateEndHMS2.Value, this.dateBeginHMS3.Value, this.dateEndHMS3.Value, this.dateBeginHMS4.Value, this.dateEndHMS4.Value, bOvertimeShift);
				if (num != 0)
				{
					XMessageBox.Show(this, comShift_Acc2.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
