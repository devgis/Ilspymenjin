using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmControlSeg : frmN3000
	{
		private IContainer components;

		private CheckBox chkf_ReaderCount;

		private NumericUpDown nudf_LimitedTimesOfDay;

		private Label label94;

		private GroupBox groupBox11;

		private NumericUpDown nudf_LimitedTimesOfHMS3;

		private Label label93;

		private NumericUpDown nudf_LimitedTimesOfHMS2;

		private Label label92;

		private NumericUpDown nudf_LimitedTimesOfHMS1;

		private Label label89;

		private Label label90;

		private Label label91;

		private DateTimePicker dateBeginHMS3;

		private DateTimePicker dateEndHMS3;

		private Label label87;

		private Label label88;

		private DateTimePicker dateBeginHMS2;

		private DateTimePicker dateEndHMS2;

		private Label label86;

		private Label label85;

		private DateTimePicker dateEndHMS1;

		private DateTimePicker dateBeginHMS1;

		private GroupBox groupBox10;

		private CheckBox chkMonday;

		private CheckBox chkSunday;

		private CheckBox chkTuesday;

		private CheckBox chkSaturday;

		private CheckBox chkWednesday;

		private CheckBox chkFriday;

		private CheckBox chkThursday;

		private Label label84;

		private ComboBox cbof_ControlSegIDLinked;

		private Label label83;

		private ComboBox cbof_ControlSegID;

		private DateTimePicker dtpEnd;

		private DateTimePicker dtpBegin;

		private Label label81;

		private Label label82;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private GroupBox groupBox3;

		internal TextBox txtf_ControlSegName;

		private Label label1;

		internal Button cmdCancel;

		internal Button cmdOK;

		private Label label2;

		private RadioButton optReaderCount;

		private RadioButton optControllerCount;

		private CheckBox chkNotAllowInHolidays;

		private Label label3;

		private NumericUpDown nudf_LimitedTimesOfMonth;

		public string operateMode = "";

		public int curControlSegID;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControlSeg));
			this.chkNotAllowInHolidays = new CheckBox();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label1 = new Label();
			this.txtf_ControlSegName = new TextBox();
			this.groupBox3 = new GroupBox();
			this.label3 = new Label();
			this.nudf_LimitedTimesOfMonth = new NumericUpDown();
			this.optReaderCount = new RadioButton();
			this.optControllerCount = new RadioButton();
			this.nudf_LimitedTimesOfDay = new NumericUpDown();
			this.nudf_LimitedTimesOfHMS3 = new NumericUpDown();
			this.label93 = new Label();
			this.nudf_LimitedTimesOfHMS2 = new NumericUpDown();
			this.label92 = new Label();
			this.nudf_LimitedTimesOfHMS1 = new NumericUpDown();
			this.label2 = new Label();
			this.label94 = new Label();
			this.label91 = new Label();
			this.chkf_ReaderCount = new CheckBox();
			this.groupBox2 = new GroupBox();
			this.label84 = new Label();
			this.cbof_ControlSegIDLinked = new ComboBox();
			this.groupBox11 = new GroupBox();
			this.label89 = new Label();
			this.label90 = new Label();
			this.dateBeginHMS3 = new DateTimePicker();
			this.dateEndHMS3 = new DateTimePicker();
			this.label87 = new Label();
			this.label88 = new Label();
			this.dateBeginHMS2 = new DateTimePicker();
			this.dateEndHMS2 = new DateTimePicker();
			this.label86 = new Label();
			this.label85 = new Label();
			this.dateEndHMS1 = new DateTimePicker();
			this.dateBeginHMS1 = new DateTimePicker();
			this.groupBox10 = new GroupBox();
			this.chkMonday = new CheckBox();
			this.chkSunday = new CheckBox();
			this.chkTuesday = new CheckBox();
			this.chkSaturday = new CheckBox();
			this.chkWednesday = new CheckBox();
			this.chkFriday = new CheckBox();
			this.chkThursday = new CheckBox();
			this.label83 = new Label();
			this.cbof_ControlSegID = new ComboBox();
			this.dtpEnd = new DateTimePicker();
			this.dtpBegin = new DateTimePicker();
			this.label81 = new Label();
			this.label82 = new Label();
			this.groupBox1 = new GroupBox();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.nudf_LimitedTimesOfMonth).BeginInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfDay).BeginInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfHMS3).BeginInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfHMS2).BeginInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfHMS1).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.chkNotAllowInHolidays, "chkNotAllowInHolidays");
			this.chkNotAllowInHolidays.Checked = true;
			this.chkNotAllowInHolidays.CheckState = CheckState.Checked;
			this.chkNotAllowInHolidays.ForeColor = Color.White;
			this.chkNotAllowInHolidays.Name = "chkNotAllowInHolidays";
			this.chkNotAllowInHolidays.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.txtf_ControlSegName, "txtf_ControlSegName");
			this.txtf_ControlSegName.Name = "txtf_ControlSegName";
			this.groupBox3.BackColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.nudf_LimitedTimesOfMonth);
			this.groupBox3.Controls.Add(this.optReaderCount);
			this.groupBox3.Controls.Add(this.optControllerCount);
			this.groupBox3.Controls.Add(this.nudf_LimitedTimesOfDay);
			this.groupBox3.Controls.Add(this.nudf_LimitedTimesOfHMS3);
			this.groupBox3.Controls.Add(this.label93);
			this.groupBox3.Controls.Add(this.nudf_LimitedTimesOfHMS2);
			this.groupBox3.Controls.Add(this.label92);
			this.groupBox3.Controls.Add(this.nudf_LimitedTimesOfHMS1);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label94);
			this.groupBox3.Controls.Add(this.label91);
			this.groupBox3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.nudf_LimitedTimesOfMonth, "nudf_LimitedTimesOfMonth");
			NumericUpDown arg_60C_0 = this.nudf_LimitedTimesOfMonth;
			int[] array = new int[4];
			array[0] = 254;
			arg_60C_0.Maximum = new decimal(array);
			this.nudf_LimitedTimesOfMonth.Name = "nudf_LimitedTimesOfMonth";
			this.nudf_LimitedTimesOfMonth.ReadOnly = true;
			componentResourceManager.ApplyResources(this.optReaderCount, "optReaderCount");
			this.optReaderCount.Name = "optReaderCount";
			this.optReaderCount.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.optControllerCount, "optControllerCount");
			this.optControllerCount.Checked = true;
			this.optControllerCount.Name = "optControllerCount";
			this.optControllerCount.TabStop = true;
			this.optControllerCount.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.nudf_LimitedTimesOfDay, "nudf_LimitedTimesOfDay");
			NumericUpDown arg_6CB_0 = this.nudf_LimitedTimesOfDay;
			int[] array2 = new int[4];
			array2[0] = 254;
			arg_6CB_0.Maximum = new decimal(array2);
			this.nudf_LimitedTimesOfDay.Name = "nudf_LimitedTimesOfDay";
			this.nudf_LimitedTimesOfDay.ReadOnly = true;
			componentResourceManager.ApplyResources(this.nudf_LimitedTimesOfHMS3, "nudf_LimitedTimesOfHMS3");
			NumericUpDown arg_715_0 = this.nudf_LimitedTimesOfHMS3;
			int[] array3 = new int[4];
			array3[0] = 31;
			arg_715_0.Maximum = new decimal(array3);
			this.nudf_LimitedTimesOfHMS3.Name = "nudf_LimitedTimesOfHMS3";
			this.nudf_LimitedTimesOfHMS3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label93, "label93");
			this.label93.Name = "label93";
			componentResourceManager.ApplyResources(this.nudf_LimitedTimesOfHMS2, "nudf_LimitedTimesOfHMS2");
			NumericUpDown arg_783_0 = this.nudf_LimitedTimesOfHMS2;
			int[] array4 = new int[4];
			array4[0] = 31;
			arg_783_0.Maximum = new decimal(array4);
			this.nudf_LimitedTimesOfHMS2.Name = "nudf_LimitedTimesOfHMS2";
			this.nudf_LimitedTimesOfHMS2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label92, "label92");
			this.label92.Name = "label92";
			componentResourceManager.ApplyResources(this.nudf_LimitedTimesOfHMS1, "nudf_LimitedTimesOfHMS1");
			NumericUpDown arg_7F1_0 = this.nudf_LimitedTimesOfHMS1;
			int[] array5 = new int[4];
			array5[0] = 31;
			arg_7F1_0.Maximum = new decimal(array5);
			this.nudf_LimitedTimesOfHMS1.Name = "nudf_LimitedTimesOfHMS1";
			this.nudf_LimitedTimesOfHMS1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.label94, "label94");
			this.label94.Name = "label94";
			componentResourceManager.ApplyResources(this.label91, "label91");
			this.label91.Name = "label91";
			componentResourceManager.ApplyResources(this.chkf_ReaderCount, "chkf_ReaderCount");
			this.chkf_ReaderCount.Name = "chkf_ReaderCount";
			this.chkf_ReaderCount.UseVisualStyleBackColor = true;
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.label84);
			this.groupBox2.Controls.Add(this.cbof_ControlSegIDLinked);
			this.groupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.label84, "label84");
			this.label84.Name = "label84";
			this.cbof_ControlSegIDLinked.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ControlSegIDLinked.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbof_ControlSegIDLinked, "cbof_ControlSegIDLinked");
			this.cbof_ControlSegIDLinked.Name = "cbof_ControlSegIDLinked";
			this.groupBox11.BackColor = Color.Transparent;
			this.groupBox11.Controls.Add(this.label89);
			this.groupBox11.Controls.Add(this.label90);
			this.groupBox11.Controls.Add(this.dateBeginHMS3);
			this.groupBox11.Controls.Add(this.dateEndHMS3);
			this.groupBox11.Controls.Add(this.label87);
			this.groupBox11.Controls.Add(this.label88);
			this.groupBox11.Controls.Add(this.dateBeginHMS2);
			this.groupBox11.Controls.Add(this.dateEndHMS2);
			this.groupBox11.Controls.Add(this.label86);
			this.groupBox11.Controls.Add(this.label85);
			this.groupBox11.Controls.Add(this.dateEndHMS1);
			this.groupBox11.Controls.Add(this.dateBeginHMS1);
			this.groupBox11.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox11, "groupBox11");
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.TabStop = false;
			componentResourceManager.ApplyResources(this.label89, "label89");
			this.label89.Name = "label89";
			componentResourceManager.ApplyResources(this.label90, "label90");
			this.label90.Name = "label90";
			componentResourceManager.ApplyResources(this.dateBeginHMS3, "dateBeginHMS3");
			this.dateBeginHMS3.Name = "dateBeginHMS3";
			this.dateBeginHMS3.ShowUpDown = true;
			this.dateBeginHMS3.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS3, "dateEndHMS3");
			this.dateEndHMS3.Name = "dateEndHMS3";
			this.dateEndHMS3.ShowUpDown = true;
			this.dateEndHMS3.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label87, "label87");
			this.label87.Name = "label87";
			componentResourceManager.ApplyResources(this.label88, "label88");
			this.label88.Name = "label88";
			componentResourceManager.ApplyResources(this.dateBeginHMS2, "dateBeginHMS2");
			this.dateBeginHMS2.Name = "dateBeginHMS2";
			this.dateBeginHMS2.ShowUpDown = true;
			this.dateBeginHMS2.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS2, "dateEndHMS2");
			this.dateEndHMS2.Name = "dateEndHMS2";
			this.dateEndHMS2.ShowUpDown = true;
			this.dateEndHMS2.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label86, "label86");
			this.label86.Name = "label86";
			componentResourceManager.ApplyResources(this.label85, "label85");
			this.label85.Name = "label85";
			componentResourceManager.ApplyResources(this.dateEndHMS1, "dateEndHMS1");
			this.dateEndHMS1.Name = "dateEndHMS1";
			this.dateEndHMS1.ShowUpDown = true;
			this.dateEndHMS1.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			componentResourceManager.ApplyResources(this.dateBeginHMS1, "dateBeginHMS1");
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.groupBox10.BackColor = Color.Transparent;
			this.groupBox10.Controls.Add(this.chkMonday);
			this.groupBox10.Controls.Add(this.chkSunday);
			this.groupBox10.Controls.Add(this.chkTuesday);
			this.groupBox10.Controls.Add(this.chkSaturday);
			this.groupBox10.Controls.Add(this.chkWednesday);
			this.groupBox10.Controls.Add(this.chkFriday);
			this.groupBox10.Controls.Add(this.chkThursday);
			this.groupBox10.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox10, "groupBox10");
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.TabStop = false;
			componentResourceManager.ApplyResources(this.chkMonday, "chkMonday");
			this.chkMonday.Checked = true;
			this.chkMonday.CheckState = CheckState.Checked;
			this.chkMonday.Name = "chkMonday";
			this.chkMonday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkSunday, "chkSunday");
			this.chkSunday.Checked = true;
			this.chkSunday.CheckState = CheckState.Checked;
			this.chkSunday.Name = "chkSunday";
			this.chkSunday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkTuesday, "chkTuesday");
			this.chkTuesday.Checked = true;
			this.chkTuesday.CheckState = CheckState.Checked;
			this.chkTuesday.Name = "chkTuesday";
			this.chkTuesday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkSaturday, "chkSaturday");
			this.chkSaturday.Checked = true;
			this.chkSaturday.CheckState = CheckState.Checked;
			this.chkSaturday.Name = "chkSaturday";
			this.chkSaturday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkWednesday, "chkWednesday");
			this.chkWednesday.Checked = true;
			this.chkWednesday.CheckState = CheckState.Checked;
			this.chkWednesday.Name = "chkWednesday";
			this.chkWednesday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkFriday, "chkFriday");
			this.chkFriday.Checked = true;
			this.chkFriday.CheckState = CheckState.Checked;
			this.chkFriday.Name = "chkFriday";
			this.chkFriday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.chkThursday, "chkThursday");
			this.chkThursday.Checked = true;
			this.chkThursday.CheckState = CheckState.Checked;
			this.chkThursday.Name = "chkThursday";
			this.chkThursday.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label83, "label83");
			this.label83.BackColor = Color.Transparent;
			this.label83.ForeColor = Color.White;
			this.label83.Name = "label83";
			this.cbof_ControlSegID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_ControlSegID.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbof_ControlSegID, "cbof_ControlSegID");
			this.cbof_ControlSegID.Name = "cbof_ControlSegID";
			componentResourceManager.ApplyResources(this.dtpEnd, "dtpEnd");
			this.dtpEnd.Name = "dtpEnd";
			this.dtpEnd.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			componentResourceManager.ApplyResources(this.dtpBegin, "dtpBegin");
			this.dtpBegin.Name = "dtpBegin";
			this.dtpBegin.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			componentResourceManager.ApplyResources(this.label81, "label81");
			this.label81.Name = "label81";
			componentResourceManager.ApplyResources(this.label82, "label82");
			this.label82.Name = "label82";
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.dtpBegin);
			this.groupBox1.Controls.Add(this.label82);
			this.groupBox1.Controls.Add(this.label81);
			this.groupBox1.Controls.Add(this.dtpEnd);
			this.groupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.chkNotAllowInHolidays);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtf_ControlSegName);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.chkf_ReaderCount);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox11);
			base.Controls.Add(this.groupBox10);
			base.Controls.Add(this.label83);
			base.Controls.Add(this.cbof_ControlSegID);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmControlSeg";
			base.Load += new EventHandler(this.dfrmControlSeg_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControlSeg_KeyDown);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((ISupportInitialize)this.nudf_LimitedTimesOfMonth).EndInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfDay).EndInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfHMS3).EndInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfHMS2).EndInit();
			((ISupportInitialize)this.nudf_LimitedTimesOfHMS1).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox11.ResumeLayout(false);
			this.groupBox11.PerformLayout();
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmControlSeg()
		{
			this.InitializeComponent();
		}

		private void dfrmControlSeg_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmControlSeg_Load_Acc(sender, e);
				return;
			}
			if (wgAppConfig.getParamValBoolByNO(136))
			{
				base.Size = new Size(new Point(680, base.Size.Height));
			}
			this.cbof_ControlSegID.Items.Clear();
			this.cbof_ControlSegIDLinked.Items.Clear();
			for (int i = 2; i <= 255; i++)
			{
				this.cbof_ControlSegID.Items.Add(i);
			}
			for (int i = 0; i <= 255; i++)
			{
				this.cbof_ControlSegIDLinked.Items.Add(i);
			}
			this.cbof_ControlSegIDLinked.Text = "0";
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("23:59:59");
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
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				if (this.operateMode == "New")
				{
					this.cbof_ControlSegID.Enabled = true;
					string cmdText = " SELECT * FROM t_b_ControlSeg ORDER BY [f_ControlSegID] DESC ";
					if (sqlConnection.State == ConnectionState.Closed)
					{
						sqlConnection.Open();
					}
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							this.curControlSegID = (int)sqlDataReader["f_ControlSegID"] + 1;
						}
						else
						{
							this.curControlSegID = 2;
						}
						sqlDataReader.Close();
					}
					this.cbof_ControlSegID.Text = this.curControlSegID.ToString();
				}
				else
				{
					this.cbof_ControlSegID.Enabled = false;
					this.cbof_ControlSegID.Text = this.curControlSegID.ToString();
					string cmdText2 = " SELECT * FROM t_b_ControlSeg WHERE [f_ControlSegID]= " + this.curControlSegID.ToString();
					if (sqlConnection.State == ConnectionState.Closed)
					{
						sqlConnection.Open();
					}
					using (SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection))
					{
						SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
						if (sqlDataReader2.Read())
						{
							try
							{
								this.chkMonday.Checked = (sqlDataReader2["f_Monday"].ToString() == "1");
								this.chkTuesday.Checked = (sqlDataReader2["f_Tuesday"].ToString() == "1");
								this.chkWednesday.Checked = (sqlDataReader2["f_Wednesday"].ToString() == "1");
								this.chkThursday.Checked = (sqlDataReader2["f_Thursday"].ToString() == "1");
								this.chkFriday.Checked = (sqlDataReader2["f_Friday"].ToString() == "1");
								this.chkSaturday.Checked = (sqlDataReader2["f_Saturday"].ToString() == "1");
								this.chkSunday.Checked = (sqlDataReader2["f_Sunday"].ToString() == "1");
								this.dateBeginHMS1.Value = DateTime.Parse(sqlDataReader2["f_BeginHMS1"].ToString());
								this.dateBeginHMS2.Value = DateTime.Parse(sqlDataReader2["f_BeginHMS2"].ToString());
								this.dateBeginHMS3.Value = DateTime.Parse(sqlDataReader2["f_BeginHMS3"].ToString());
								this.dateEndHMS1.Value = DateTime.Parse(sqlDataReader2["f_EndHMS1"].ToString());
								this.dateEndHMS2.Value = DateTime.Parse(sqlDataReader2["f_EndHMS2"].ToString());
								this.dateEndHMS3.Value = DateTime.Parse(sqlDataReader2["f_EndHMS3"].ToString());
								this.dtpBegin.Value = DateTime.Parse(sqlDataReader2["f_BeginYMD"].ToString());
								this.dtpEnd.Value = DateTime.Parse(sqlDataReader2["f_EndYMD"].ToString());
								this.txtf_ControlSegName.Text = wgTools.SetObjToStr(sqlDataReader2["f_ControlSegName"]);
								this.cbof_ControlSegIDLinked.Text = sqlDataReader2["f_ControlSegIDLinked"].ToString();
								this.chkf_ReaderCount.Checked = ((int.Parse(sqlDataReader2["f_ReaderCount"].ToString()) & 1) > 0);
								this.optControllerCount.Checked = ((int.Parse(sqlDataReader2["f_ReaderCount"].ToString()) & 1) == 0);
								this.optReaderCount.Checked = ((int.Parse(sqlDataReader2["f_ReaderCount"].ToString()) & 1) > 0);
								this.nudf_LimitedTimesOfDay.Value = ((int)sqlDataReader2["f_LimitedTimesOfDay"] & 255);
								this.nudf_LimitedTimesOfMonth.Value = ((int)sqlDataReader2["f_LimitedTimesOfDay"] >> 8 & 255);
								this.nudf_LimitedTimesOfHMS1.Value = (int)sqlDataReader2["f_LimitedTimesOfHMS1"];
								this.nudf_LimitedTimesOfHMS2.Value = (int)sqlDataReader2["f_LimitedTimesOfHMS2"];
								this.nudf_LimitedTimesOfHMS3.Value = (int)sqlDataReader2["f_LimitedTimesOfHMS3"];
								this.chkNotAllowInHolidays.Checked = (sqlDataReader2["f_ControlByHoliday"].ToString() == "1");
								if (!this.chkNotAllowInHolidays.Checked)
								{
									this.chkNotAllowInHolidays.Visible = true;
								}
							}
							catch (Exception)
							{
							}
						}
						sqlDataReader2.Close();
					}
				}
			}
			wgAppConfig.setDisplayFormatDate(this.dtpBegin, wgTools.DisplayFormat_DateYMD);
			wgAppConfig.setDisplayFormatDate(this.dtpEnd, wgTools.DisplayFormat_DateYMD);
		}

		private void dfrmControlSeg_Load_Acc(object sender, EventArgs e)
		{
			if (wgAppConfig.getParamValBoolByNO(136))
			{
				base.Size = new Size(new Point(680, base.Size.Height));
			}
			this.cbof_ControlSegID.Items.Clear();
			this.cbof_ControlSegIDLinked.Items.Clear();
			for (int i = 2; i <= 255; i++)
			{
				this.cbof_ControlSegID.Items.Add(i);
			}
			for (int i = 0; i <= 255; i++)
			{
				this.cbof_ControlSegIDLinked.Items.Add(i);
			}
			this.cbof_ControlSegIDLinked.Text = "0";
			this.dateBeginHMS1.CustomFormat = "HH:mm";
			this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
			this.dateBeginHMS1.Value = DateTime.Parse("00:00:00");
			this.dateEndHMS1.CustomFormat = "HH:mm";
			this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
			this.dateEndHMS1.Value = DateTime.Parse("23:59:59");
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
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				if (this.operateMode == "New")
				{
					this.cbof_ControlSegID.Enabled = true;
					string cmdText = " SELECT * FROM t_b_ControlSeg ORDER BY [f_ControlSegID] DESC ";
					if (oleDbConnection.State == ConnectionState.Closed)
					{
						oleDbConnection.Open();
					}
					using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
					{
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
						if (oleDbDataReader.Read())
						{
							this.curControlSegID = (int)oleDbDataReader["f_ControlSegID"] + 1;
						}
						else
						{
							this.curControlSegID = 2;
						}
						oleDbDataReader.Close();
					}
					this.cbof_ControlSegID.Text = this.curControlSegID.ToString();
				}
				else
				{
					this.cbof_ControlSegID.Enabled = false;
					this.cbof_ControlSegID.Text = this.curControlSegID.ToString();
					string cmdText2 = " SELECT * FROM t_b_ControlSeg WHERE [f_ControlSegID]= " + this.curControlSegID.ToString();
					if (oleDbConnection.State == ConnectionState.Closed)
					{
						oleDbConnection.Open();
					}
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(cmdText2, oleDbConnection))
					{
						OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader();
						if (oleDbDataReader2.Read())
						{
							try
							{
								this.chkMonday.Checked = (oleDbDataReader2["f_Monday"].ToString() == "1");
								this.chkTuesday.Checked = (oleDbDataReader2["f_Tuesday"].ToString() == "1");
								this.chkWednesday.Checked = (oleDbDataReader2["f_Wednesday"].ToString() == "1");
								this.chkThursday.Checked = (oleDbDataReader2["f_Thursday"].ToString() == "1");
								this.chkFriday.Checked = (oleDbDataReader2["f_Friday"].ToString() == "1");
								this.chkSaturday.Checked = (oleDbDataReader2["f_Saturday"].ToString() == "1");
								this.chkSunday.Checked = (oleDbDataReader2["f_Sunday"].ToString() == "1");
								this.dateBeginHMS1.Value = DateTime.Parse(oleDbDataReader2["f_BeginHMS1"].ToString());
								this.dateBeginHMS2.Value = DateTime.Parse(oleDbDataReader2["f_BeginHMS2"].ToString());
								this.dateBeginHMS3.Value = DateTime.Parse(oleDbDataReader2["f_BeginHMS3"].ToString());
								this.dateEndHMS1.Value = DateTime.Parse(oleDbDataReader2["f_EndHMS1"].ToString());
								this.dateEndHMS2.Value = DateTime.Parse(oleDbDataReader2["f_EndHMS2"].ToString());
								this.dateEndHMS3.Value = DateTime.Parse(oleDbDataReader2["f_EndHMS3"].ToString());
								this.dtpBegin.Value = DateTime.Parse(oleDbDataReader2["f_BeginYMD"].ToString());
								this.dtpEnd.Value = DateTime.Parse(oleDbDataReader2["f_EndYMD"].ToString());
								this.txtf_ControlSegName.Text = wgTools.SetObjToStr(oleDbDataReader2["f_ControlSegName"]);
								this.cbof_ControlSegIDLinked.Text = oleDbDataReader2["f_ControlSegIDLinked"].ToString();
								this.chkf_ReaderCount.Checked = ((int.Parse(oleDbDataReader2["f_ReaderCount"].ToString()) & 1) > 0);
								this.optControllerCount.Checked = ((int.Parse(oleDbDataReader2["f_ReaderCount"].ToString()) & 1) == 0);
								this.optReaderCount.Checked = ((int.Parse(oleDbDataReader2["f_ReaderCount"].ToString()) & 1) > 0);
								this.nudf_LimitedTimesOfDay.Value = ((int)oleDbDataReader2["f_LimitedTimesOfDay"] & 255);
								this.nudf_LimitedTimesOfMonth.Value = ((int)oleDbDataReader2["f_LimitedTimesOfDay"] >> 8 & 255);
								this.nudf_LimitedTimesOfHMS1.Value = (int)oleDbDataReader2["f_LimitedTimesOfHMS1"];
								this.nudf_LimitedTimesOfHMS2.Value = (int)oleDbDataReader2["f_LimitedTimesOfHMS2"];
								this.nudf_LimitedTimesOfHMS3.Value = (int)oleDbDataReader2["f_LimitedTimesOfHMS3"];
								this.chkNotAllowInHolidays.Checked = (oleDbDataReader2["f_ControlByHoliday"].ToString() == "1");
								if (!this.chkNotAllowInHolidays.Checked)
								{
									this.chkNotAllowInHolidays.Visible = true;
								}
							}
							catch (Exception)
							{
							}
						}
						oleDbDataReader2.Close();
					}
				}
			}
			wgAppConfig.setDisplayFormatDate(this.dtpBegin, wgTools.DisplayFormat_DateYMD);
			wgAppConfig.setDisplayFormatDate(this.dtpEnd, wgTools.DisplayFormat_DateYMD);
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private string getDateString(DateTimePicker dtp)
		{
			if (dtp == null)
			{
				return wgTools.PrepareStr("");
			}
			return wgTools.PrepareStr(dtp.Value.ToString(wgTools.YMDHMSFormat), true, wgTools.YMDHMSFormat);
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.cmdOK_Click_Acc(sender, e);
				return;
			}
			SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString);
			try
			{
				string text;
				if (this.operateMode == "New")
				{
					text = " SELECT * FROM t_b_ControlSeg WHERE [f_ControlSegID]= " + this.cbof_ControlSegID.Text;
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);
						if (sqlDataReader.Read())
						{
							sqlDataReader.Close();
							XMessageBox.Show(this, CommonStr.strIDIsDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						sqlDataReader.Close();
						text = " INSERT INTO t_b_ControlSeg([f_ControlSegID], [f_Monday], [f_Tuesday], [f_Wednesday]";
						text += " , [f_Thursday], [f_Friday], [f_Saturday], [f_Sunday] ";
						text += " , [f_BeginHMS1],[f_EndHMS1], [f_BeginHMS2], [f_EndHMS2], [f_BeginHMS3], [f_EndHMS3]";
						text += " , [f_BeginYMD],[f_EndYMD], [f_ControlSegName], [f_ControlSegIDLinked]";
						text += " , [f_ReaderCount],[f_LimitedTimesOfDay], [f_LimitedTimesOfHMS1], [f_LimitedTimesOfHMS2], [f_LimitedTimesOfHMS3]";
						text += " , [f_ControlByHoliday] ";
						text += ") ";
						text = text + " VALUES ( " + this.cbof_ControlSegID.Text;
						text = text + " , " + (this.chkMonday.Checked ? "1" : "0");
						text = text + " , " + (this.chkTuesday.Checked ? "1" : "0");
						text = text + " , " + (this.chkWednesday.Checked ? "1" : "0");
						text = text + " , " + (this.chkThursday.Checked ? "1" : "0");
						text = text + " , " + (this.chkFriday.Checked ? "1" : "0");
						text = text + " , " + (this.chkSaturday.Checked ? "1" : "0");
						text = text + " , " + (this.chkSunday.Checked ? "1" : "0");
						text = text + " , " + this.getDateString(this.dateBeginHMS1);
						text = text + " , " + this.getDateString(this.dateEndHMS1);
						text = text + " , " + this.getDateString(this.dateBeginHMS2);
						text = text + " , " + this.getDateString(this.dateEndHMS2);
						text = text + " , " + this.getDateString(this.dateBeginHMS3);
						text = text + " , " + this.getDateString(this.dateEndHMS3);
						text = text + " , " + this.getDateString(this.dtpBegin);
						text = text + " , " + this.getDateString(this.dtpEnd);
						text = text + " , " + wgTools.PrepareStr(this.txtf_ControlSegName.Text);
						text = text + " , " + this.cbof_ControlSegIDLinked.Text;
						text = text + " , " + (this.optReaderCount.Checked ? "1" : "0");
						text = text + " , " + (this.nudf_LimitedTimesOfDay.Value + ((int)this.nudf_LimitedTimesOfMonth.Value << 8)).ToString();
						text = text + " , " + this.nudf_LimitedTimesOfHMS1.Value.ToString();
						text = text + " , " + this.nudf_LimitedTimesOfHMS2.Value.ToString();
						text = text + " , " + this.nudf_LimitedTimesOfHMS3.Value.ToString();
						text = text + " , " + (this.chkNotAllowInHolidays.Checked ? "1" : "0");
						text += ")";
						using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection))
						{
							sqlCommand2.ExecuteNonQuery();
							base.DialogResult = DialogResult.OK;
							base.Close();
						}
						goto IL_7C8;
					}
				}
				sqlConnection.Open();
				text = " UPDATE t_b_ControlSeg ";
				text += " SET  [f_Monday]= ";
				text += (this.chkMonday.Checked ? "1" : "0");
				text += ", [f_Tuesday]=";
				text += (this.chkTuesday.Checked ? "1" : "0");
				text += ", [f_Wednesday]=";
				text += (this.chkWednesday.Checked ? "1" : "0");
				text += " , [f_Thursday]= ";
				text += (this.chkThursday.Checked ? "1" : "0");
				text += " ,[f_Friday]=";
				text += (this.chkFriday.Checked ? "1" : "0");
				text += " , [f_Saturday]=";
				text += (this.chkSaturday.Checked ? "1" : "0");
				text += " , [f_Sunday] =";
				text += (this.chkSunday.Checked ? "1" : "0");
				text = text + " , [f_BeginHMS1]=" + this.getDateString(this.dateBeginHMS1);
				text = text + " ,[f_EndHMS1]=" + this.getDateString(this.dateEndHMS1);
				text = text + " , [f_BeginHMS2]=" + this.getDateString(this.dateBeginHMS2);
				text = text + " ,[f_EndHMS2]=" + this.getDateString(this.dateEndHMS2);
				text = text + " , [f_BeginHMS3]=" + this.getDateString(this.dateBeginHMS3);
				text = text + " ,[f_EndHMS3]=" + this.getDateString(this.dateEndHMS3);
				text = text + " , [f_BeginYMD]=" + this.getDateString(this.dtpBegin);
				text = text + " , [f_EndYMD]=" + this.getDateString(this.dtpEnd);
				text += " , [f_ControlSegName]=";
				text = text + " " + wgTools.PrepareStr(this.txtf_ControlSegName.Text);
				text += " , [f_ControlSegIDLinked]=";
				text = text + " " + this.cbof_ControlSegIDLinked.Text;
				text += " , [f_ReaderCount]=";
				text = text + "  " + (this.optReaderCount.Checked ? "1" : "0");
				text += " , [f_LimitedTimesOfDay]=";
				text = text + "  " + (this.nudf_LimitedTimesOfDay.Value + ((int)this.nudf_LimitedTimesOfMonth.Value << 8)).ToString();
				text += " , [f_LimitedTimesOfHMS1]=";
				text = text + "  " + this.nudf_LimitedTimesOfHMS1.Value.ToString();
				text += " , [f_LimitedTimesOfHMS2]=";
				text = text + "  " + this.nudf_LimitedTimesOfHMS2.Value.ToString();
				text += " , [f_LimitedTimesOfHMS3]=";
				text = text + "  " + this.nudf_LimitedTimesOfHMS3.Value.ToString();
				text += " , [f_ControlByHoliday] =";
				text += (this.chkNotAllowInHolidays.Checked ? "1" : "0");
				text = text + " WHERE [f_ControlSegID]= " + this.cbof_ControlSegID.Text;
				using (SqlCommand sqlCommand3 = new SqlCommand(text, sqlConnection))
				{
					sqlCommand3.ExecuteNonQuery();
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
				IL_7C8:;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				sqlConnection.Dispose();
			}
		}

		private void cmdOK_Click_Acc(object sender, EventArgs e)
		{
			OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString);
			try
			{
				string text;
				if (this.operateMode == "New")
				{
					text = " SELECT * FROM t_b_ControlSeg WHERE [f_ControlSegID]= " + this.cbof_ControlSegID.Text;
					oleDbConnection.Open();
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.Default);
						if (oleDbDataReader.Read())
						{
							oleDbDataReader.Close();
							XMessageBox.Show(this, CommonStr.strIDIsDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						oleDbDataReader.Close();
						text = " INSERT INTO t_b_ControlSeg([f_ControlSegID], [f_Monday], [f_Tuesday], [f_Wednesday]";
						text += " , [f_Thursday], [f_Friday], [f_Saturday], [f_Sunday] ";
						text += " , [f_BeginHMS1],[f_EndHMS1], [f_BeginHMS2], [f_EndHMS2], [f_BeginHMS3], [f_EndHMS3]";
						text += " , [f_BeginYMD],[f_EndYMD], [f_ControlSegName], [f_ControlSegIDLinked]";
						text += " , [f_ReaderCount],[f_LimitedTimesOfDay], [f_LimitedTimesOfHMS1], [f_LimitedTimesOfHMS2], [f_LimitedTimesOfHMS3]";
						text += " , [f_ControlByHoliday] ";
						text += ") ";
						text = text + " VALUES ( " + this.cbof_ControlSegID.Text;
						text = text + " , " + (this.chkMonday.Checked ? "1" : "0");
						text = text + " , " + (this.chkTuesday.Checked ? "1" : "0");
						text = text + " , " + (this.chkWednesday.Checked ? "1" : "0");
						text = text + " , " + (this.chkThursday.Checked ? "1" : "0");
						text = text + " , " + (this.chkFriday.Checked ? "1" : "0");
						text = text + " , " + (this.chkSaturday.Checked ? "1" : "0");
						text = text + " , " + (this.chkSunday.Checked ? "1" : "0");
						text = text + " , " + this.getDateString(this.dateBeginHMS1);
						text = text + " , " + this.getDateString(this.dateEndHMS1);
						text = text + " , " + this.getDateString(this.dateBeginHMS2);
						text = text + " , " + this.getDateString(this.dateEndHMS2);
						text = text + " , " + this.getDateString(this.dateBeginHMS3);
						text = text + " , " + this.getDateString(this.dateEndHMS3);
						text = text + " , " + this.getDateString(this.dtpBegin);
						text = text + " , " + this.getDateString(this.dtpEnd);
						text = text + " , " + wgTools.PrepareStr(this.txtf_ControlSegName.Text);
						text = text + " , " + this.cbof_ControlSegIDLinked.Text;
						text = text + " , " + (this.optReaderCount.Checked ? "1" : "0");
						text = text + " , " + (this.nudf_LimitedTimesOfDay.Value + ((int)this.nudf_LimitedTimesOfMonth.Value << 8)).ToString();
						text = text + " , " + this.nudf_LimitedTimesOfHMS1.Value.ToString();
						text = text + " , " + this.nudf_LimitedTimesOfHMS2.Value.ToString();
						text = text + " , " + this.nudf_LimitedTimesOfHMS3.Value.ToString();
						text = text + " , " + (this.chkNotAllowInHolidays.Checked ? "1" : "0");
						text += ")";
						using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection))
						{
							oleDbCommand2.ExecuteNonQuery();
							base.DialogResult = DialogResult.OK;
							base.Close();
						}
						goto IL_7B8;
					}
				}
				oleDbConnection.Open();
				text = " UPDATE t_b_ControlSeg ";
				text += " SET  [f_Monday]= ";
				text += (this.chkMonday.Checked ? "1" : "0");
				text += ", [f_Tuesday]=";
				text += (this.chkTuesday.Checked ? "1" : "0");
				text += ", [f_Wednesday]=";
				text += (this.chkWednesday.Checked ? "1" : "0");
				text += " , [f_Thursday]= ";
				text += (this.chkThursday.Checked ? "1" : "0");
				text += " ,[f_Friday]=";
				text += (this.chkFriday.Checked ? "1" : "0");
				text += " , [f_Saturday]=";
				text += (this.chkSaturday.Checked ? "1" : "0");
				text += " , [f_Sunday] =";
				text += (this.chkSunday.Checked ? "1" : "0");
				text = text + " , [f_BeginHMS1]=" + this.getDateString(this.dateBeginHMS1);
				text = text + " ,[f_EndHMS1]=" + this.getDateString(this.dateEndHMS1);
				text = text + " , [f_BeginHMS2]=" + this.getDateString(this.dateBeginHMS2);
				text = text + " ,[f_EndHMS2]=" + this.getDateString(this.dateEndHMS2);
				text = text + " , [f_BeginHMS3]=" + this.getDateString(this.dateBeginHMS3);
				text = text + " ,[f_EndHMS3]=" + this.getDateString(this.dateEndHMS3);
				text = text + " , [f_BeginYMD]=" + this.getDateString(this.dtpBegin);
				text = text + " , [f_EndYMD]=" + this.getDateString(this.dtpEnd);
				text += " , [f_ControlSegName]=";
				text = text + " " + wgTools.PrepareStr(this.txtf_ControlSegName.Text);
				text += " , [f_ControlSegIDLinked]=";
				text = text + " " + this.cbof_ControlSegIDLinked.Text;
				text += " , [f_ReaderCount]=";
				text = text + "  " + (this.optReaderCount.Checked ? "1" : "0");
				text += " , [f_LimitedTimesOfDay]=";
				text = text + "  " + (this.nudf_LimitedTimesOfDay.Value + ((int)this.nudf_LimitedTimesOfMonth.Value << 8)).ToString();
				text += " , [f_LimitedTimesOfHMS1]=";
				text = text + "  " + this.nudf_LimitedTimesOfHMS1.Value.ToString();
				text += " , [f_LimitedTimesOfHMS2]=";
				text = text + "  " + this.nudf_LimitedTimesOfHMS2.Value.ToString();
				text += " , [f_LimitedTimesOfHMS3]=";
				text = text + "  " + this.nudf_LimitedTimesOfHMS3.Value.ToString();
				text += " , [f_ControlByHoliday] =";
				text += (this.chkNotAllowInHolidays.Checked ? "1" : "0");
				text = text + " WHERE [f_ControlSegID]= " + this.cbof_ControlSegID.Text;
				using (OleDbCommand oleDbCommand3 = new OleDbCommand(text, oleDbConnection))
				{
					oleDbCommand3.ExecuteNonQuery();
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
				IL_7B8:;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			finally
			{
				oleDbConnection.Dispose();
			}
		}

		private void dfrmControlSeg_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				this.chkNotAllowInHolidays.Visible = true;
			}
		}
	}
}
