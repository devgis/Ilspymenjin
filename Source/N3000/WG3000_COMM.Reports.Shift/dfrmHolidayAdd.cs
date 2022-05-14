using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmHolidayAdd : frmN3000
	{
		public string holidayType = "2";

		internal Label Label4;

		internal DateTimePicker dtpStartDate;

		internal Label Label5;

		internal Label Label6;

		internal DateTimePicker dtpEndDate;

		internal ComboBox cboStart;

		internal TextBox txtf_Notes;

		internal Label Label7;

		internal ComboBox cboEnd;

		internal TextBox txtHolidayName;

		internal Button btnCancel;

		internal Button btnOK;

		private ArrayList arrGroupID = new ArrayList();

		public dfrmHolidayAdd()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmHolidayAdd));
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.Label4 = new Label();
			this.dtpStartDate = new DateTimePicker();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.dtpEndDate = new DateTimePicker();
			this.cboStart = new ComboBox();
			this.cboEnd = new ComboBox();
			this.txtf_Notes = new TextBox();
			this.Label7 = new Label();
			this.txtHolidayName = new TextBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.BackColor = Color.Transparent;
			this.Label4.ForeColor = Color.White;
			this.Label4.Name = "Label4";
			componentResourceManager.ApplyResources(this.dtpStartDate, "dtpStartDate");
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpStartDate.ValueChanged += new EventHandler(this.dtpStartDate_ValueChanged);
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.BackColor = Color.Transparent;
			this.Label5.ForeColor = Color.White;
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.BackColor = Color.Transparent;
			this.Label6.ForeColor = Color.White;
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.dtpEndDate, "dtpEndDate");
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.cboStart, "cboStart");
			this.cboStart.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboStart.Name = "cboStart";
			componentResourceManager.ApplyResources(this.cboEnd, "cboEnd");
			this.cboEnd.DisplayMember = "f_GroupName";
			this.cboEnd.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboEnd.Name = "cboEnd";
			this.cboEnd.ValueMember = "f_GroupID";
			componentResourceManager.ApplyResources(this.txtf_Notes, "txtf_Notes");
			this.txtf_Notes.Name = "txtf_Notes";
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.BackColor = Color.Transparent;
			this.Label7.ForeColor = Color.White;
			this.Label7.Name = "Label7";
			componentResourceManager.ApplyResources(this.txtHolidayName, "txtHolidayName");
			this.txtHolidayName.Name = "txtHolidayName";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtHolidayName);
			base.Controls.Add(this.txtf_Notes);
			base.Controls.Add(this.dtpStartDate);
			base.Controls.Add(this.Label5);
			base.Controls.Add(this.Label6);
			base.Controls.Add(this.dtpEndDate);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.cboStart);
			base.Controls.Add(this.cboEnd);
			base.Controls.Add(this.Label7);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmHolidayAdd";
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.Load += new EventHandler(this.dfrmLeave_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void dfrmLeave_Load(object sender, EventArgs e)
		{
			try
			{
				this.dtpStartDate.Value = DateTime.Now.Date;
				this.dtpEndDate.Value = DateTime.Now.Date;
				this.cboStart.Items.Clear();
				this.cboStart.Items.AddRange(new string[]
				{
					CommonStr.strAM,
					CommonStr.strPM
				});
				this.cboEnd.Items.Clear();
				this.cboEnd.Items.AddRange(new string[]
				{
					CommonStr.strAM,
					CommonStr.strPM
				});
				if (this.cboStart.Items.Count > 0)
				{
					this.cboStart.SelectedIndex = 0;
				}
				if (this.cboEnd.Items.Count > 1)
				{
					this.cboEnd.SelectedIndex = 1;
				}
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			wgAppConfig.setDisplayFormatDate(this.dtpStartDate, wgTools.DisplayFormat_DateYMDWeek);
			wgAppConfig.setDisplayFormatDate(this.dtpEndDate, wgTools.DisplayFormat_DateYMDWeek);
		}

		private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dtpStartDate.Value > this.dtpEndDate.Value)
				{
					this.cboEnd.Text = CommonStr.strPM;
				}
				this.dtpEndDate.MinDate = this.dtpStartDate.Value;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.txtHolidayName.Text = this.txtHolidayName.Text.Trim();
			if (this.txtHolidayName.Text == "")
			{
				XMessageBox.Show(this, CommonStr.strNameNotEmpty, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			try
			{
				string text = "INSERT INTO [t_a_Holiday] ([f_Name], [f_Value], [f_Value1], [f_Value2],[f_Value3], [f_Type], [f_Note])";
				text = text + " Values( " + wgTools.PrepareStr(this.txtHolidayName.Text);
				text = text + ", " + wgTools.PrepareStr(this.dtpStartDate.Value.ToString("yyyy-MM-dd"));
				text = text + ", " + wgTools.PrepareStr(this.cboStart.Text);
				text = text + ", " + wgTools.PrepareStr(this.dtpEndDate.Value.ToString("yyyy-MM-dd"));
				text = text + ", " + wgTools.PrepareStr(this.cboEnd.Text);
				text = text + ", " + wgTools.PrepareStr(this.holidayType);
				text = text + ", " + wgTools.PrepareStr(this.txtf_Notes.Text);
				text += " )";
				wgAppConfig.runUpdateSql(text);
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
	}
}
