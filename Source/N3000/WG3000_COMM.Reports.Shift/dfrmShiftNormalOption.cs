using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftNormalOption : frmN3000
	{
		private IContainer components;

		internal DateTimePicker dtpOffduty0;

		internal Label Label7;

		private CheckBox chkEarliest;

		private CheckBox chkOnlyTwoTimes;

		private GroupBox groupBox1;

		internal Label label1;

		internal Button btnOK;

		internal Button btnCancel;

		private CheckBox chkInvalidSwipe;

		private ComboBox cboLeaveAbsenceTimeout;

		private CheckBox chkOnlyOnDuty;

		public dfrmShiftNormalOption()
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

		private void dfrmShiftNormalOption_Load(object sender, EventArgs e)
		{
			this.dtpOffduty0.CustomFormat = "HH:mm";
			this.dtpOffduty0.Format = DateTimePickerFormat.Custom;
			this.dtpOffduty0.Value = DateTime.Parse("00:00:00");
			this.cboLeaveAbsenceTimeout.Items.Clear();
			double num = 0.0;
			for (int i = 0; i < 48; i++)
			{
				this.cboLeaveAbsenceTimeout.Items.Add(num.ToString("F1", CultureInfo.InvariantCulture));
				num += 0.5;
			}
			this.cboLeaveAbsenceTimeout.Text = "8.0";
			try
			{
				this.dtpOffduty0.Value = DateTime.Parse("2011-1-1 " + wgAppConfig.getSystemParamByNO(55).ToString());
			}
			catch
			{
			}
			try
			{
				this.chkEarliest.Checked = (wgAppConfig.getSystemParamByNO(56).ToString() == "1");
			}
			catch
			{
			}
			try
			{
				this.chkOnlyTwoTimes.Checked = (wgAppConfig.getSystemParamByNO(57).ToString() == "1");
			}
			catch
			{
			}
			try
			{
				this.cboLeaveAbsenceTimeout.Text = wgAppConfig.getSystemParamByNO(58).ToString();
			}
			catch
			{
			}
			try
			{
				this.chkInvalidSwipe.Checked = (wgAppConfig.getSystemParamByNO(54).ToString() == "1");
			}
			catch
			{
			}
			try
			{
				this.chkOnlyOnDuty.Checked = (wgAppConfig.getSystemParamByNO(59).ToString() == "1");
			}
			catch
			{
			}
			this.loadOperatorPrivilege();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				wgAppConfig.setSystemParamValue(55, "", this.dtpOffduty0.Value.ToString("HH:mm"), "");
				wgAppConfig.setSystemParamValueBool(56, this.chkEarliest.Checked);
				wgAppConfig.setSystemParamValueBool(57, this.chkOnlyTwoTimes.Checked);
				wgAppConfig.setSystemParamValue(58, "", this.cboLeaveAbsenceTimeout.Text, "");
				wgAppConfig.setSystemParamValueBool(54, this.chkInvalidSwipe.Checked);
				wgAppConfig.setSystemParamValueBool(59, this.chkOnlyOnDuty.Checked);
			}
			catch
			{
			}
			base.Close();
		}

		private void chkOnlyOnDuty_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkOnlyOnDuty.Checked)
			{
				this.chkOnlyTwoTimes.Checked = false;
			}
		}

		private void chkOnlyTwoTimes_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkOnlyTwoTimes.Checked)
			{
				this.chkOnlyOnDuty.Checked = false;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftNormalOption));
			this.btnCancel = new Button();
			this.chkInvalidSwipe = new CheckBox();
			this.btnOK = new Button();
			this.groupBox1 = new GroupBox();
			this.cboLeaveAbsenceTimeout = new ComboBox();
			this.label1 = new Label();
			this.chkOnlyTwoTimes = new CheckBox();
			this.chkOnlyOnDuty = new CheckBox();
			this.dtpOffduty0 = new DateTimePicker();
			this.Label7 = new Label();
			this.chkEarliest = new CheckBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.chkInvalidSwipe, "chkInvalidSwipe");
			this.chkInvalidSwipe.ForeColor = Color.White;
			this.chkInvalidSwipe.Name = "chkInvalidSwipe";
			this.chkInvalidSwipe.UseVisualStyleBackColor = true;
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.groupBox1.Controls.Add(this.cboLeaveAbsenceTimeout);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.chkOnlyTwoTimes);
			this.groupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.cboLeaveAbsenceTimeout.DisplayMember = "8.0";
			this.cboLeaveAbsenceTimeout.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboLeaveAbsenceTimeout.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboLeaveAbsenceTimeout, "cboLeaveAbsenceTimeout");
			this.cboLeaveAbsenceTimeout.Name = "cboLeaveAbsenceTimeout";
			this.cboLeaveAbsenceTimeout.ValueMember = "8.0";
			this.label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.chkOnlyTwoTimes, "chkOnlyTwoTimes");
			this.chkOnlyTwoTimes.ForeColor = Color.White;
			this.chkOnlyTwoTimes.Name = "chkOnlyTwoTimes";
			this.chkOnlyTwoTimes.UseVisualStyleBackColor = true;
			this.chkOnlyTwoTimes.CheckedChanged += new EventHandler(this.chkOnlyTwoTimes_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkOnlyOnDuty, "chkOnlyOnDuty");
			this.chkOnlyOnDuty.ForeColor = Color.White;
			this.chkOnlyOnDuty.Name = "chkOnlyOnDuty";
			this.chkOnlyOnDuty.UseVisualStyleBackColor = true;
			this.chkOnlyOnDuty.CheckedChanged += new EventHandler(this.chkOnlyOnDuty_CheckedChanged);
			componentResourceManager.ApplyResources(this.dtpOffduty0, "dtpOffduty0");
			this.dtpOffduty0.Name = "dtpOffduty0";
			this.dtpOffduty0.ShowUpDown = true;
			this.dtpOffduty0.Value = new DateTime(2004, 7, 18, 0, 0, 0, 0);
			this.Label7.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.Name = "Label7";
			componentResourceManager.ApplyResources(this.chkEarliest, "chkEarliest");
			this.chkEarliest.ForeColor = Color.White;
			this.chkEarliest.Name = "chkEarliest";
			this.chkEarliest.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.chkOnlyOnDuty);
			base.Controls.Add(this.chkInvalidSwipe);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.chkEarliest);
			base.Controls.Add(this.dtpOffduty0);
			base.Controls.Add(this.Label7);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftNormalOption";
			base.Load += new EventHandler(this.dfrmShiftNormalOption_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
