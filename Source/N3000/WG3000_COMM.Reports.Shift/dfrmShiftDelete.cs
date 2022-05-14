using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Reports.Shift
{
	public class dfrmShiftDelete : frmN3000
	{
		private Container components;

		internal ComboBox cbof_Group;

		internal Label Label3;

		internal Label Label4;

		internal ComboBox cbof_ConsumerName;

		internal DateTimePicker dtpStartDate;

		internal Label Label5;

		internal Label Label6;

		internal DateTimePicker dtpEndDate;

		internal Label lblStartWeekday;

		internal Label lblEndWeekday;

		internal GroupBox GroupBox1;

		internal Button btnCancel;

		internal Button btnOK;

		internal ProgressBar ProgressBar1;

		private ArrayList arrConsumerCMIndex = new ArrayList();

		private ArrayList arrShiftID = new ArrayList();

		private ArrayList arrSelectedShiftID = new ArrayList();

		private DataSet dsConsumers;

		private DataView dvConsumers;

		private DataTable dtConsumers;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private dfrmFind dfrmFind1;

		public dfrmShiftDelete()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmShiftDelete));
			this.cbof_Group = new ComboBox();
			this.Label3 = new Label();
			this.Label4 = new Label();
			this.cbof_ConsumerName = new ComboBox();
			this.dtpStartDate = new DateTimePicker();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.dtpEndDate = new DateTimePicker();
			this.lblStartWeekday = new Label();
			this.lblEndWeekday = new Label();
			this.GroupBox1 = new GroupBox();
			this.ProgressBar1 = new ProgressBar();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.GroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.cbof_Group.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_Group, "cbof_Group");
			this.cbof_Group.Name = "cbof_Group";
			this.cbof_Group.SelectedIndexChanged += new EventHandler(this.cbof_Group_SelectedIndexChanged);
			this.cbof_Group.KeyDown += new KeyEventHandler(this.dfrmAutoShift_KeyDown);
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.Name = "Label4";
			this.cbof_ConsumerName.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbof_ConsumerName, "cbof_ConsumerName");
			this.cbof_ConsumerName.Name = "cbof_ConsumerName";
			this.cbof_ConsumerName.KeyDown += new KeyEventHandler(this.dfrmAutoShift_KeyDown);
			this.cbof_ConsumerName.Leave += new EventHandler(this.cbof_ConsumerName_Leave);
			componentResourceManager.ApplyResources(this.dtpStartDate, "dtpStartDate");
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpStartDate.ValueChanged += new EventHandler(this.dtpStartDate_ValueChanged);
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.Label6, "Label6");
			this.Label6.Name = "Label6";
			componentResourceManager.ApplyResources(this.dtpEndDate, "dtpEndDate");
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Value = new DateTime(2004, 7, 19, 0, 0, 0, 0);
			this.dtpEndDate.ValueChanged += new EventHandler(this.dtpEndDate_ValueChanged);
			componentResourceManager.ApplyResources(this.lblStartWeekday, "lblStartWeekday");
			this.lblStartWeekday.Name = "lblStartWeekday";
			componentResourceManager.ApplyResources(this.lblEndWeekday, "lblEndWeekday");
			this.lblEndWeekday.Name = "lblEndWeekday";
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.dtpEndDate);
			this.GroupBox1.Controls.Add(this.dtpStartDate);
			this.GroupBox1.Controls.Add(this.cbof_Group);
			this.GroupBox1.Controls.Add(this.lblEndWeekday);
			this.GroupBox1.Controls.Add(this.lblStartWeekday);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.cbof_ConsumerName);
			this.GroupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.ProgressBar1, "ProgressBar1");
			this.ProgressBar1.Name = "ProgressBar1";
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
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.ProgressBar1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.GroupBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmShiftDelete";
			base.FormClosing += new FormClosingEventHandler(this.dfrmShiftDelete_FormClosing);
			base.Load += new EventHandler(this.dfrmAutoShift_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmAutoShift_KeyDown);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void loadGroupData()
		{
			icGroup icGroup = new icGroup();
			icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
			for (int i = 0; i < this.arrGroupID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrGroupName[i].ToString()))
				{
					this.cbof_Group.Items.Add(CommonStr.strAll);
				}
				else
				{
					this.cbof_Group.Items.Add(this.arrGroupName[i].ToString());
				}
			}
			if (this.cbof_Group.Items.Count > 0)
			{
				this.cbof_Group.SelectedIndex = 0;
			}
		}

		private void _dataTableLoad()
		{
			this.dsConsumers = new DataSet("Users");
			string text = " SELECT t_b_Group.f_GroupName,t_b_Consumer.f_ConsumerID, t_b_Consumer.f_ConsumerName, LTRIM(([f_ConsumerNo]) +'- '+ [f_ConsumerName]) as [f_UserFullName]  FROM [t_b_Consumer]  LEFT OUTER JOIN t_b_Group ON ( t_b_Group.f_GroupID = t_b_Consumer.f_GroupID ) WHERE f_AttendEnabled = 1 ";
			text += " AND f_ShiftEnabled > 0 ";
			this.dsConsumers.Clear();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dsConsumers, "Consumers");
						}
					}
					goto IL_E6;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dsConsumers, "Consumers");
					}
				}
			}
			IL_E6:
			this.dtConsumers = this.dsConsumers.Tables["Consumers"];
			this.dvConsumers = new DataView(this.dtConsumers);
			this.dvConsumers.RowFilter = "";
			try
			{
				DataColumn[] array = new DataColumn[2];
				array[0] = this.dtConsumers.Columns["f_UserFullName"];
				this.dtConsumers.PrimaryKey = array;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			this.dtConsumers.AcceptChanges();
			this.loadGroupData();
		}

		private void lblShiftWeekday_update(int weekdayStart)
		{
			try
			{
				if (weekdayStart >= 7)
				{
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmAutoShift_Load(object sender, EventArgs e)
		{
			try
			{
				this.Label3.Text = wgAppConfig.ReplaceFloorRomm(this.Label3.Text);
				base.KeyPreview = true;
				this._dataTableLoad();
				this.dtpStartDate.Value = DateTime.Now.Date;
				this.dtpEndDate.Value = DateTime.Now.Date;
				if (this.cbof_Group.Items.Count > 0)
				{
					this.cbof_Group.SelectedIndex = 0;
				}
				if (this.cbof_ConsumerName.Items.Count > 0)
				{
					this.cbof_ConsumerName.SelectedIndex = 0;
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

		private void cbof_Group_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbof_Group.SelectedIndex == 0 && this.arrGroupID[0].ToString() == "0")
				{
					this.dvConsumers.RowFilter = "";
				}
				else
				{
					this.dvConsumers.RowFilter = string.Format(" (f_GroupName = {0} ) OR (f_GroupName like {1})", wgTools.PrepareStr(this.cbof_Group.Text), wgTools.PrepareStr(string.Format("{0}\\%", this.cbof_Group.Text)));
				}
				this.cbof_ConsumerName.Items.Clear();
				this.cbof_ConsumerName.Items.Add(CommonStr.strAll);
				this.arrConsumerCMIndex.Add("");
				for (int i = 0; i <= this.dvConsumers.Count - 1; i++)
				{
					this.cbof_ConsumerName.Items.Add(this.dvConsumers[i]["f_UserFullName"]);
					this.arrConsumerCMIndex.Add(i);
				}
				if (this.cbof_ConsumerName.Items.Count > 0)
				{
					this.cbof_ConsumerName.SelectedIndex = 0;
				}
				int arg_12F_0 = this.dvConsumers.Count;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.dtpEndDate.MinDate = this.dtpStartDate.Value;
				this.lblStartWeekday.Text = CommonStr.strWeekday + wgAppConfig.weekdayToChsName((int)this.dtpStartDate.Value.DayOfWeek);
				this.lblShiftWeekday_update((int)this.dtpStartDate.Value.DayOfWeek);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void dtpEndDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.lblEndWeekday.Text = CommonStr.strWeekday + wgAppConfig.weekdayToChsName((int)this.dtpEndDate.Value.DayOfWeek);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			using (comShift comShift = new comShift())
			{
				int num = 0;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					int[] array;
					if (this.cbof_ConsumerName.Text == CommonStr.strAll)
					{
						if (this.dvConsumers.Count <= 0)
						{
							XMessageBox.Show(this, CommonStr.strSelectUser, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						array = new int[this.dvConsumers.Count - 1 + 1];
						for (int i = 0; i <= this.dvConsumers.Count - 1; i++)
						{
							array[i] = (int)this.dvConsumers[i]["f_ConsumerID"];
						}
					}
					else
					{
						array = new int[]
						{
							(int)this.dvConsumers[this.cbof_ConsumerName.SelectedIndex - 1]["f_ConsumerID"]
						};
					}
					DateTime value = this.dtpStartDate.Value;
					DateTime value2 = this.dtpEndDate.Value;
					if (num == 0)
					{
						this.ProgressBar1.Maximum = array.Length;
						for (int i = 0; i <= array.Length - 1; i++)
						{
							this.ProgressBar1.Value = i;
							num = comShift.shift_arrange_delete(array[i], value, value2);
							if (num != 0)
							{
								XMessageBox.Show(this, comShift.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								break;
							}
						}
						if (num == 0)
						{
							this.ProgressBar1.Value = this.ProgressBar1.Maximum;
							XMessageBox.Show(this, "OK!", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						XMessageBox.Show(this, comShift.errDesc(num) + "\r\n\r\n" + comShift.errInfo, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				this.ProgressBar1.Value = 0;
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			using (comShift_Acc comShift_Acc = new comShift_Acc())
			{
				int num = 0;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					int[] array;
					if (this.cbof_ConsumerName.Text == CommonStr.strAll)
					{
						if (this.dvConsumers.Count <= 0)
						{
							XMessageBox.Show(this, CommonStr.strSelectUser, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						array = new int[this.dvConsumers.Count - 1 + 1];
						for (int i = 0; i <= this.dvConsumers.Count - 1; i++)
						{
							array[i] = (int)this.dvConsumers[i]["f_ConsumerID"];
						}
					}
					else
					{
						array = new int[]
						{
							(int)this.dvConsumers[this.cbof_ConsumerName.SelectedIndex - 1]["f_ConsumerID"]
						};
					}
					DateTime value = this.dtpStartDate.Value;
					DateTime value2 = this.dtpEndDate.Value;
					if (num == 0)
					{
						this.ProgressBar1.Maximum = array.Length;
						for (int i = 0; i <= array.Length - 1; i++)
						{
							this.ProgressBar1.Value = i;
							num = comShift_Acc.shift_arrange_delete(array[i], value, value2);
							if (num != 0)
							{
								XMessageBox.Show(this, comShift_Acc.errDesc(num), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								break;
							}
						}
						if (num == 0)
						{
							this.ProgressBar1.Value = this.ProgressBar1.Maximum;
							XMessageBox.Show(this, "OK!", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						XMessageBox.Show(this, comShift_Acc.errDesc(num) + "\r\n\r\n" + comShift_Acc.errInfo, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
				this.ProgressBar1.Value = 0;
				Cursor.Current = Cursors.Default;
			}
		}

		private void dfrmAutoShift_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cbof_ConsumerName_Leave(object sender, EventArgs e)
		{
			this.checkUserValid(this.cbof_ConsumerName);
		}

		public bool checkUserValid(ComboBox cbo)
		{
			try
			{
				string value = cbo.Text.ToUpper();
				int num = cbo.SelectedIndex;
				bool result;
				if (num >= 0 && cbo.Text == cbo.Items[num].ToString())
				{
					result = true;
					return result;
				}
				num = -1;
				for (int i = 0; i < cbo.Items.Count; i++)
				{
					object objToStr = cbo.Items[i];
					if (Strings.UCase(wgTools.SetObjToStr(objToStr)).IndexOf(value) >= 0)
					{
						cbo.SelectedItem = cbo.Items[i];
						cbo.SelectedIndex = i;
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					cbo.SelectedIndex = num;
					result = true;
					return result;
				}
				XMessageBox.Show(this, CommonStr.strUserNonexisted, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				result = false;
				return result;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			return false;
		}

		private void dfrmShiftDelete_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
