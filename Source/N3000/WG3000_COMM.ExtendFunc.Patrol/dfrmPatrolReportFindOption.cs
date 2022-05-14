using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Patrol
{
	public class dfrmPatrolReportFindOption : frmN3000
	{
		private int[] Event = new int[]
		{
			4,
			2,
			3,
			1
		};

		private IContainer components;

		private CheckedListBox checkedListBox1;

		private Button btnClose;

		private Button btnQuery;

		public dfrmPatrolReportFindOption()
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
				(base.Owner as frmPatrolReport).btnQuery_Click(null, null);
			}
		}

		public string getStrSql()
		{
			string result = " (1 > 0) ";
			if (this.checkedListBox1.CheckedItems.Count != 0 && this.checkedListBox1.CheckedItems.Count != this.checkedListBox1.Items.Count)
			{
				string text = "";
				for (int i = 0; i <= this.checkedListBox1.CheckedItems.Count - 1; i++)
				{
					if (text == "")
					{
						text = text + "  t_d_PatrolDetailData.f_EventDesc= " + this.Event[this.checkedListBox1.CheckedIndices[i]];
					}
					else
					{
						text = text + " OR  t_d_PatrolDetailData.f_EventDesc= " + this.Event[this.checkedListBox1.CheckedIndices[i]];
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					result = " (" + text + " )  ";
				}
			}
			return result;
		}

		private void dfrmShiftAttReportFindOption_Load(object sender, EventArgs e)
		{
			this.checkedListBox1.Items.Clear();
			this.checkedListBox1.Items.Add(CommonStr.strPatrolEventAbsence, false);
			this.checkedListBox1.Items.Add(CommonStr.strPatrolEventEarly, false);
			this.checkedListBox1.Items.Add(CommonStr.strPatrolEventLate, false);
			this.checkedListBox1.Items.Add(CommonStr.strPatrolEventNormal, false);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPatrolReportFindOption));
			this.btnClose = new Button();
			this.btnQuery = new Button();
			this.checkedListBox1 = new CheckedListBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.btnQuery, "btnQuery");
			this.btnQuery.BackColor = Color.Transparent;
			this.btnQuery.BackgroundImage = Resources.pMain_button_normal;
			this.btnQuery.ForeColor = Color.White;
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.UseVisualStyleBackColor = false;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			componentResourceManager.ApplyResources(this.checkedListBox1, "checkedListBox1");
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.MultiColumn = true;
			this.checkedListBox1.Name = "checkedListBox1";
			componentResourceManager.ApplyResources(this, "$this");
			base.ControlBox = false;
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnQuery);
			base.Controls.Add(this.checkedListBox1);
			base.Name = "dfrmPatrolReportFindOption";
			base.Load += new EventHandler(this.dfrmShiftAttReportFindOption_Load);
			base.ResumeLayout(false);
		}
	}
}
