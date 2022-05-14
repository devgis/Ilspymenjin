using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Basic
{
	public class dfrmControlHolidayAdd : frmN3000
	{
		public bool bHoliday = true;

		internal Label Label4;

		internal DateTimePicker dtpStartDate;

		internal Label Label5;

		internal Label Label6;

		internal DateTimePicker dtpEndDate;

		internal TextBox txtf_Notes;

		internal Label Label7;

		internal TextBox txtHolidayName;

		internal Button btnCancel;

		private DateTimePicker dateBeginHMS1;

		private DateTimePicker dateEndHMS1;

		internal Button btnOK;

		private ArrayList arrGroupID = new ArrayList();

		public dfrmControlHolidayAdd()
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControlHolidayAdd));
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.Label4 = new Label();
			this.dtpStartDate = new DateTimePicker();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.dtpEndDate = new DateTimePicker();
			this.txtf_Notes = new TextBox();
			this.Label7 = new Label();
			this.txtHolidayName = new TextBox();
			this.dateBeginHMS1 = new DateTimePicker();
			this.dateEndHMS1 = new DateTimePicker();
			base.SuspendLayout();
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
			this.Label4.BackColor = Color.Transparent;
			this.Label4.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.Name = "Label4";
			componentResourceManager.ApplyResources(this.dtpStartDate, "dtpStartDate");
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpStartDate.ValueChanged += new EventHandler(this.dtpStartDate_ValueChanged);
			this.Label5.BackColor = Color.Transparent;
			this.Label5.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
			this.Label6.BackColor = Color.Transparent;
			this.Label6.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.dtpEndDate, "dtpEndDate");
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.txtf_Notes, "txtf_Notes");
			this.txtf_Notes.Name = "txtf_Notes";
			this.Label7.BackColor = Color.Transparent;
			this.Label7.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label7, "Label7");
			this.Label7.Name = "Label7";
			componentResourceManager.ApplyResources(this.txtHolidayName, "txtHolidayName");
			this.txtHolidayName.Name = "txtHolidayName";
			componentResourceManager.ApplyResources(this.dateBeginHMS1, "dateBeginHMS1");
			this.dateBeginHMS1.Name = "dateBeginHMS1";
			this.dateBeginHMS1.ShowUpDown = true;
			this.dateBeginHMS1.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dateEndHMS1, "dateEndHMS1");
			this.dateEndHMS1.Name = "dateEndHMS1";
			this.dateEndHMS1.ShowUpDown = true;
			this.dateEndHMS1.Value = new DateTime(2010, 1, 1, 23, 59, 0, 0);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.dateEndHMS1);
			base.Controls.Add(this.dateBeginHMS1);
			base.Controls.Add(this.txtHolidayName);
			base.Controls.Add(this.txtf_Notes);
			base.Controls.Add(this.dtpStartDate);
			base.Controls.Add(this.Label5);
			base.Controls.Add(this.Label6);
			base.Controls.Add(this.dtpEndDate);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.Label7);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmControlHolidayAdd";
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
				this.dateBeginHMS1.CustomFormat = "HH:mm";
				this.dateBeginHMS1.Format = DateTimePickerFormat.Custom;
				this.dateBeginHMS1.Value = DateTime.Parse("00:00:00");
				this.dateEndHMS1.CustomFormat = "HH:mm";
				this.dateEndHMS1.Format = DateTimePickerFormat.Custom;
				this.dateEndHMS1.Value = DateTime.Parse("23:59:59");
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
				this.dtpEndDate.MinDate = this.dtpStartDate.Value;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "INSERT INTO [t_b_ControlHolidays] ([f_BeginYMDHMS], [f_EndYMDHMS], [f_Notes], [f_forceWork])";
				text += " Values( ";
				text = text + " " + wgTools.PrepareStr(DateTime.Parse(this.dtpStartDate.Value.ToString("yyyy-MM-dd ") + this.dateBeginHMS1.Value.ToString("HH:mm")), true, wgTools.YMDHMSFormat);
				text = text + ", " + wgTools.PrepareStr(DateTime.Parse(this.dtpEndDate.Value.ToString("yyyy-MM-dd ") + this.dateEndHMS1.Value.ToString("HH:mm:59")), true, wgTools.YMDHMSFormat);
				text = text + ", " + wgTools.PrepareStr(this.txtf_Notes.Text);
				text = text + ", " + (this.bHoliday ? "0" : "1");
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
