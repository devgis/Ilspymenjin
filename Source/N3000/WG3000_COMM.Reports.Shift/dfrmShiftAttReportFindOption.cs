using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftAttReportFindOption : frmN3000
	{
		private IContainer components;

		private CheckedListBox checkedListBox1;

		private Button btnClose;

		private Button btnQuery;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftAttReportFindOption));
			this.btnClose = new Button();
			this.btnQuery = new Button();
			this.checkedListBox1 = new CheckedListBox();
			base.SuspendLayout();
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.btnQuery.BackColor = Color.Transparent;
			this.btnQuery.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnQuery, "btnQuery");
			this.btnQuery.ForeColor = Color.White;
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.UseVisualStyleBackColor = false;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.checkedListBox1, "checkedListBox1");
			this.checkedListBox1.MultiColumn = true;
			this.checkedListBox1.Name = "checkedListBox1";
			componentResourceManager.ApplyResources(this, "$this");
			base.ControlBox = false;
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnQuery);
			base.Controls.Add(this.checkedListBox1);
			base.Name = "dfrmShiftAttReportFindOption";
			base.Load += new EventHandler(this.dfrmShiftAttReportFindOption_Load);
			base.ResumeLayout(false);
		}

		public dfrmShiftAttReportFindOption()
		{
			this.InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Hide();
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			if (base.Owner != null)
			{
				(base.Owner as frmShiftAttReport).btnQuery_Click(null, null);
			}
		}

		public string getStrSql()
		{
			string result = " (1 < 0) ";
			if (this.checkedListBox1.CheckedItems.Count != 0)
			{
				string text = "";
				for (int i = 0; i <= this.checkedListBox1.CheckedItems.Count - 1; i++)
				{
					if (text == "")
					{
						text = text + " t_d_shift_AttReport.[f_OnDuty1AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					}
					else
					{
						text = text + " OR t_d_shift_AttReport.[f_OnDuty1AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					}
					text = text + " OR t_d_shift_AttReport.[f_OnDuty1CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty1AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty1CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OnDuty2AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OnDuty2CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty2AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty2CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OnDuty3AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OnDuty3CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty3AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty3CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OnDuty4AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OnDuty4CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty4AttDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
					text = text + " OR t_d_shift_AttReport.[f_OffDuty4CardRecordDesc]= " + wgTools.PrepareStr(this.checkedListBox1.CheckedItems[i]);
				}
				result = " (" + text + " )  ";
			}
			return result;
		}

		private void dfrmShiftAttReportFindOption_Load(object sender, EventArgs e)
		{
			this.checkedListBox1.Items.Clear();
			this.checkedListBox1.Items.Add(CommonStr.strLateness, false);
			this.checkedListBox1.Items.Add(CommonStr.strLeaveEarly, false);
			this.checkedListBox1.Items.Add(CommonStr.strAbsence, false);
			this.checkedListBox1.Items.Add(CommonStr.strSignIn, false);
			this.checkedListBox1.Items.Add(CommonStr.strNotReadCard, false);
			this.checkedListBox1.Items.Add(CommonStr.strOvertime, false);
		}
	}
}
