using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmUserBatchUpdate : frmN3000
	{
		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupNameWithSpace = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		public string strSqlSelected = "";

		private bool bInsertNullDepartment;

		private IContainer components;

		internal GroupBox GroupBox1;

		internal RadioButton opt1a;

		internal RadioButton opt1b;

		internal CheckBox chk1;

		internal Button btnOK;

		internal Button btnCancel;

		internal ComboBox cbof_GroupID;

		internal Label Label3;

		internal CheckBox chk2;

		internal CheckBox chk3;

		internal CheckBox chk4;

		internal GroupBox GroupBox2;

		internal RadioButton opt2a;

		internal RadioButton opt2b;

		internal GroupBox GroupBox3;

		internal RadioButton opt3a;

		internal RadioButton opt3b;

		internal ComboBox cbof_GroupNew;

		internal CheckBox chk5;

		internal DateTimePicker dtpEnd;

		internal Label Label1;

		internal GroupBox GroupBox4;

		internal DateTimePicker dtpBegin;

		internal Label Label5;

		private MaskedTextBox txtf_PIN;

		internal CheckBox chk6;

		internal CheckBox chkIncludeAllBranch;

		public dfrmUserBatchUpdate()
		{
			this.InitializeComponent();
		}

		private void dfrmUserBatchUpdate_Load(object sender, EventArgs e)
		{
			this.txtf_PIN.Mask = "999999";
			this.txtf_PIN.Text = 345678.ToString();
			this.Label3.Text = wgAppConfig.ReplaceFloorRomm(this.Label3.Text);
			this.chk4.Text = wgAppConfig.ReplaceFloorRomm(this.chk4.Text);
			this.chkIncludeAllBranch.Text = wgAppConfig.ReplaceFloorRomm(this.chkIncludeAllBranch.Text);
			try
			{
				icGroup icGroup = new icGroup();
				icGroup.getGroup(ref this.arrGroupNameWithSpace, ref this.arrGroupID, ref this.arrGroupNO);
				int i = this.arrGroupID.Count;
				for (i = 0; i < this.arrGroupID.Count; i++)
				{
					if (i == 0 && string.IsNullOrEmpty(this.arrGroupNameWithSpace[i].ToString()))
					{
						this.arrGroupName.Add(CommonStr.strAll);
					}
					else
					{
						this.arrGroupName.Add(this.arrGroupNameWithSpace[i].ToString());
					}
					this.cbof_GroupID.Items.Add(this.arrGroupName[i].ToString());
					this.cbof_GroupNew.Items.Add(this.arrGroupNameWithSpace[i].ToString());
				}
				if ((int)this.arrGroupID[0] == 0)
				{
					this.cbof_GroupID.Items.Insert(1, wgAppConfig.ReplaceFloorRomm(CommonStr.strDepartmentIsEmpty));
					this.bInsertNullDepartment = true;
				}
				if (this.cbof_GroupID.Items.Count > 0)
				{
					this.cbof_GroupID.SelectedIndex = 0;
				}
				if (this.cbof_GroupNew.Items.Count > 0)
				{
					this.cbof_GroupNew.SelectedIndex = 0;
				}
				if (!string.IsNullOrEmpty(this.strSqlSelected))
				{
					this.cbof_GroupID.Visible = false;
					this.Label3.Visible = false;
					this.chkIncludeAllBranch.Visible = false;
				}
				bool paramValBoolByNO = wgAppConfig.getParamValBoolByNO(113);
				if (paramValBoolByNO)
				{
					this.chk3.Visible = true;
					this.GroupBox3.Visible = true;
				}
				else
				{
					this.chk3.Visible = false;
					this.GroupBox3.Visible = false;
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
			wgAppConfig.setDisplayFormatDate(this.dtpBegin, wgTools.DisplayFormat_DateYMD);
			wgAppConfig.setDisplayFormatDate(this.dtpEnd, wgTools.DisplayFormat_DateYMD);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			int num = 0;
			string text = "  ";
			int num2;
			int num3;
			if (this.cbof_GroupID.SelectedIndex < 0 || (this.cbof_GroupID.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
			{
				num2 = 0;
				num3 = 0;
			}
			else if (this.bInsertNullDepartment && this.cbof_GroupID.Text == wgAppConfig.ReplaceFloorRomm(CommonStr.strDepartmentIsEmpty))
			{
				num2 = 0;
				num3 = 0;
				text = " WHERE f_GroupID = 0   ";
			}
			else
			{
				if (this.bInsertNullDepartment)
				{
					num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex - 1];
					num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex - 1];
					num3 = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
				}
				else
				{
					num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex];
					num3 = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
				}
				if (!this.chkIncludeAllBranch.Checked)
				{
					num3 = num2;
				}
			}
			if (num2 > 0)
			{
				if (num2 >= num3)
				{
					text = " FROM   t_b_Consumer,t_b_Group WHERE  t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text += string.Format(" AND  t_b_Group.f_GroupID ={0:d} ", num);
				}
				else
				{
					text = " FROM   t_b_Consumer,t_b_Group   WHERE t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text += string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", num2);
					text += string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ", num3);
				}
			}
			if (!string.IsNullOrEmpty(this.strSqlSelected))
			{
				text = string.Format(" WHERE f_ConsumerID IN ({0}) ", this.strSqlSelected);
			}
			if (this.chk1.Checked)
			{
				string text2 = "UPDATE t_b_Consumer   SET ";
				text2 = text2 + "  t_b_Consumer.[f_DoorEnabled]=" + (this.opt1a.Checked ? "1" : "0");
				text2 += text;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk3.Checked)
			{
				string text2 = "UPDATE t_b_Consumer SET ";
				text2 = text2 + "  t_b_Consumer.[f_ShiftEnabled]=" + (this.opt3b.Checked ? "1" : "0");
				text2 += text;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk2.Checked)
			{
				string text2 = "UPDATE t_b_Consumer SET ";
				text2 = text2 + "  t_b_Consumer.[f_AttendEnabled]=" + (this.opt2a.Checked ? "1" : "0");
				text2 += text;
				wgAppConfig.runUpdateSql(text2);
				if (!this.opt2a.Checked)
				{
					text2 = "UPDATE t_b_Consumer SET ";
					text2 += "  t_b_Consumer.[f_ShiftEnabled]=0";
					text2 += text;
					wgAppConfig.runUpdateSql(text2);
				}
			}
			if (this.chk5.Checked)
			{
				string text2 = "UPDATE t_b_Consumer SET ";
				text2 = text2 + " t_b_Consumer.[f_BeginYMD]=" + wgTools.PrepareStr(this.dtpBegin.Value, true, "yyyy-MM-dd");
				text2 = text2 + "  ,t_b_Consumer.[f_EndYMD]=" + wgTools.PrepareStr(this.dtpEnd.Value, true, "yyyy-MM-dd");
				text2 += text;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk6.Checked)
			{
				string text2 = "UPDATE t_b_Consumer SET ";
				text2 = text2 + " t_b_Consumer.[f_PIN] = " + ((this.txtf_PIN.Text == "") ? "0" : this.txtf_PIN.Text);
				text2 += text;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk4.Checked)
			{
				string text2 = "UPDATE t_b_Consumer SET ";
				if (this.cbof_GroupNew.SelectedIndex == -1)
				{
					text2 += "  t_b_Consumer.[f_GroupID]=0";
				}
				else
				{
					text2 = text2 + "  t_b_Consumer.[f_GroupID]=" + wgTools.PrepareStr(this.arrGroupID[this.cbof_GroupNew.SelectedIndex]);
				}
				text2 += text;
				wgAppConfig.runUpdateSql(text2);
			}
			base.DialogResult = DialogResult.OK;
			icConsumerShare.setUpdateLog();
			base.Close();
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			int num = 0;
			string text = "";
			string str = "  ";
			int num2;
			int num3;
			if (this.cbof_GroupID.SelectedIndex < 0 || (this.cbof_GroupID.SelectedIndex == 0 && (int)this.arrGroupID[0] == 0))
			{
				num2 = 0;
				num3 = 0;
			}
			else if (this.bInsertNullDepartment && this.cbof_GroupID.Text == wgAppConfig.ReplaceFloorRomm(CommonStr.strDepartmentIsEmpty))
			{
				num2 = 0;
				num3 = 0;
				str = " WHERE f_GroupID = 0   ";
			}
			else
			{
				if (this.bInsertNullDepartment)
				{
					num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex - 1];
					num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex - 1];
					num3 = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
				}
				else
				{
					num = (int)this.arrGroupID[this.cbof_GroupID.SelectedIndex];
					num2 = (int)this.arrGroupNO[this.cbof_GroupID.SelectedIndex];
					num3 = icGroup.getGroupChildMaxNo(this.cbof_GroupID.Text, this.arrGroupName, this.arrGroupNO);
				}
				if (!this.chkIncludeAllBranch.Checked)
				{
					num3 = num2;
				}
			}
			if (num2 > 0)
			{
				if (num2 >= num3)
				{
					text = "    INNER JOIN t_b_Group ON (  t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text += string.Format(" AND  t_b_Group.f_GroupID ={0:d} ) ", num);
				}
				else
				{
					text = "    INNER JOIN t_b_Group ON  ( t_b_Consumer.f_GroupID = t_b_Group.f_GroupID  ";
					text += string.Format(" AND  t_b_Group.f_GroupNO >={0:d} ", num2);
					text += string.Format(" AND  t_b_Group.f_GroupNO <={0:d} ) ", num3);
				}
			}
			if (!string.IsNullOrEmpty(this.strSqlSelected))
			{
				str = string.Format(" WHERE f_ConsumerID IN ({0}) ", this.strSqlSelected);
				text = " ";
			}
			if (this.chk1.Checked)
			{
				string text2 = "UPDATE t_b_Consumer  ";
				text2 += text;
				text2 = text2 + "  SET  t_b_Consumer.[f_DoorEnabled]=" + (this.opt1a.Checked ? "1" : "0");
				text2 += str;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk3.Checked)
			{
				string text2 = "UPDATE t_b_Consumer ";
				text2 += text;
				text2 = text2 + " SET  t_b_Consumer.[f_ShiftEnabled]=" + (this.opt3b.Checked ? "1" : "0");
				text2 += str;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk2.Checked)
			{
				string text2 = "UPDATE t_b_Consumer ";
				text2 += text;
				text2 = text2 + " SET  t_b_Consumer.[f_AttendEnabled]=" + (this.opt2a.Checked ? "1" : "0");
				text2 += str;
				wgAppConfig.runUpdateSql(text2);
				if (!this.opt2a.Checked)
				{
					text2 = "UPDATE t_b_Consumer  ";
					text2 += text;
					text2 += " SET t_b_Consumer.[f_ShiftEnabled]=0";
					text2 += str;
					wgAppConfig.runUpdateSql(text2);
				}
			}
			if (this.chk5.Checked)
			{
				string text2 = "UPDATE t_b_Consumer ";
				text2 += text;
				text2 = text2 + " SET t_b_Consumer.[f_BeginYMD]=" + wgTools.PrepareStr(this.dtpBegin.Value, true, "yyyy-MM-dd");
				text2 = text2 + "  ,t_b_Consumer.[f_EndYMD]=" + wgTools.PrepareStr(this.dtpEnd.Value, true, "yyyy-MM-dd");
				text2 += str;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk6.Checked)
			{
				string text2 = "UPDATE t_b_Consumer ";
				text2 += text;
				text2 = text2 + " SET  t_b_Consumer.[f_PIN] = " + ((this.txtf_PIN.Text == "") ? "0" : this.txtf_PIN.Text);
				text2 += str;
				wgAppConfig.runUpdateSql(text2);
			}
			if (this.chk4.Checked)
			{
				string text2 = "UPDATE t_b_Consumer ";
				text2 += text;
				if (this.cbof_GroupNew.SelectedIndex == -1)
				{
					text2 += " SET  t_b_Consumer.[f_GroupID]=0";
				}
				else
				{
					text2 = text2 + " SET  t_b_Consumer.[f_GroupID]=" + wgTools.PrepareStr(this.arrGroupID[this.cbof_GroupNew.SelectedIndex]);
				}
				text2 += str;
				wgAppConfig.runUpdateSql(text2);
			}
			base.DialogResult = DialogResult.OK;
			icConsumerShare.setUpdateLog();
			base.Close();
		}

		private void chk1_CheckedChanged(object sender, EventArgs e)
		{
			this.GroupBox1.Enabled = this.chk1.Checked;
		}

		private void chk2_CheckedChanged(object sender, EventArgs e)
		{
			this.GroupBox2.Enabled = this.chk2.Checked;
		}

		private void chk4_CheckedChanged(object sender, EventArgs e)
		{
			this.cbof_GroupNew.Enabled = this.chk4.Checked;
		}

		private void chk3_CheckedChanged(object sender, EventArgs e)
		{
			this.GroupBox3.Enabled = this.chk3.Checked;
		}

		private void chk5_CheckedChanged(object sender, EventArgs e)
		{
			this.GroupBox4.Enabled = this.chk5.Checked;
		}

		private void funcCtrlShiftQ()
		{
			this.chk6.Visible = true;
			this.txtf_PIN.Visible = true;
		}

		private void dfrmUserBatchUpdate_KeyDown(object sender, KeyEventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmUserBatchUpdate));
			this.GroupBox1 = new GroupBox();
			this.opt1a = new RadioButton();
			this.opt1b = new RadioButton();
			this.chk1 = new CheckBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.cbof_GroupID = new ComboBox();
			this.Label3 = new Label();
			this.chk2 = new CheckBox();
			this.chk3 = new CheckBox();
			this.chk4 = new CheckBox();
			this.GroupBox2 = new GroupBox();
			this.opt2a = new RadioButton();
			this.opt2b = new RadioButton();
			this.GroupBox3 = new GroupBox();
			this.opt3a = new RadioButton();
			this.opt3b = new RadioButton();
			this.cbof_GroupNew = new ComboBox();
			this.chk5 = new CheckBox();
			this.dtpEnd = new DateTimePicker();
			this.Label1 = new Label();
			this.GroupBox4 = new GroupBox();
			this.dtpBegin = new DateTimePicker();
			this.Label5 = new Label();
			this.txtf_PIN = new MaskedTextBox();
			this.chk6 = new CheckBox();
			this.chkIncludeAllBranch = new CheckBox();
			this.GroupBox1.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			this.GroupBox4.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.opt1a);
			this.GroupBox1.Controls.Add(this.opt1b);
			this.GroupBox1.ForeColor = Color.White;
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.opt1a, "opt1a");
			this.opt1a.Checked = true;
			this.opt1a.Name = "opt1a";
			this.opt1a.TabStop = true;
			componentResourceManager.ApplyResources(this.opt1b, "opt1b");
			this.opt1b.Name = "opt1b";
			componentResourceManager.ApplyResources(this.chk1, "chk1");
			this.chk1.BackColor = Color.Transparent;
			this.chk1.ForeColor = Color.White;
			this.chk1.Name = "chk1";
			this.chk1.UseVisualStyleBackColor = false;
			this.chk1.CheckedChanged += new EventHandler(this.chk1_CheckedChanged);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.Name = "cbof_GroupID";
			componentResourceManager.ApplyResources(this.Label3, "Label3");
			this.Label3.BackColor = Color.Transparent;
			this.Label3.ForeColor = Color.White;
			this.Label3.Name = "Label3";
			componentResourceManager.ApplyResources(this.chk2, "chk2");
			this.chk2.BackColor = Color.Transparent;
			this.chk2.ForeColor = Color.White;
			this.chk2.Name = "chk2";
			this.chk2.UseVisualStyleBackColor = false;
			this.chk2.CheckedChanged += new EventHandler(this.chk2_CheckedChanged);
			componentResourceManager.ApplyResources(this.chk3, "chk3");
			this.chk3.BackColor = Color.Transparent;
			this.chk3.ForeColor = Color.White;
			this.chk3.Name = "chk3";
			this.chk3.UseVisualStyleBackColor = false;
			this.chk3.CheckedChanged += new EventHandler(this.chk3_CheckedChanged);
			componentResourceManager.ApplyResources(this.chk4, "chk4");
			this.chk4.BackColor = Color.Transparent;
			this.chk4.ForeColor = Color.White;
			this.chk4.Name = "chk4";
			this.chk4.UseVisualStyleBackColor = false;
			this.chk4.CheckedChanged += new EventHandler(this.chk4_CheckedChanged);
			componentResourceManager.ApplyResources(this.GroupBox2, "GroupBox2");
			this.GroupBox2.BackColor = Color.Transparent;
			this.GroupBox2.Controls.Add(this.opt2a);
			this.GroupBox2.Controls.Add(this.opt2b);
			this.GroupBox2.ForeColor = Color.White;
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.opt2a, "opt2a");
			this.opt2a.Checked = true;
			this.opt2a.Name = "opt2a";
			this.opt2a.TabStop = true;
			componentResourceManager.ApplyResources(this.opt2b, "opt2b");
			this.opt2b.Name = "opt2b";
			componentResourceManager.ApplyResources(this.GroupBox3, "GroupBox3");
			this.GroupBox3.BackColor = Color.Transparent;
			this.GroupBox3.Controls.Add(this.opt3a);
			this.GroupBox3.Controls.Add(this.opt3b);
			this.GroupBox3.ForeColor = Color.White;
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.opt3a, "opt3a");
			this.opt3a.Checked = true;
			this.opt3a.Name = "opt3a";
			this.opt3a.TabStop = true;
			componentResourceManager.ApplyResources(this.opt3b, "opt3b");
			this.opt3b.Name = "opt3b";
			componentResourceManager.ApplyResources(this.cbof_GroupNew, "cbof_GroupNew");
			this.cbof_GroupNew.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupNew.Name = "cbof_GroupNew";
			componentResourceManager.ApplyResources(this.chk5, "chk5");
			this.chk5.BackColor = Color.Transparent;
			this.chk5.ForeColor = Color.White;
			this.chk5.Name = "chk5";
			this.chk5.UseVisualStyleBackColor = false;
			this.chk5.CheckedChanged += new EventHandler(this.chk5_CheckedChanged);
			componentResourceManager.ApplyResources(this.dtpEnd, "dtpEnd");
			this.dtpEnd.Name = "dtpEnd";
			this.dtpEnd.Value = new DateTime(2029, 12, 31, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			componentResourceManager.ApplyResources(this.GroupBox4, "GroupBox4");
			this.GroupBox4.BackColor = Color.Transparent;
			this.GroupBox4.Controls.Add(this.dtpBegin);
			this.GroupBox4.Controls.Add(this.Label5);
			this.GroupBox4.Controls.Add(this.dtpEnd);
			this.GroupBox4.Controls.Add(this.Label1);
			this.GroupBox4.ForeColor = Color.White;
			this.GroupBox4.Name = "GroupBox4";
			this.GroupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.dtpBegin, "dtpBegin");
			this.dtpBegin.Name = "dtpBegin";
			this.dtpBegin.Value = new DateTime(2010, 1, 1, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.Label5, "Label5");
			this.Label5.Name = "Label5";
			componentResourceManager.ApplyResources(this.txtf_PIN, "txtf_PIN");
			this.txtf_PIN.Name = "txtf_PIN";
			componentResourceManager.ApplyResources(this.chk6, "chk6");
			this.chk6.BackColor = Color.Transparent;
			this.chk6.ForeColor = Color.White;
			this.chk6.Name = "chk6";
			this.chk6.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.chkIncludeAllBranch, "chkIncludeAllBranch");
			this.chkIncludeAllBranch.BackColor = Color.Transparent;
			this.chkIncludeAllBranch.Checked = true;
			this.chkIncludeAllBranch.CheckState = CheckState.Checked;
			this.chkIncludeAllBranch.ForeColor = Color.White;
			this.chkIncludeAllBranch.Name = "chkIncludeAllBranch";
			this.chkIncludeAllBranch.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.chkIncludeAllBranch);
			base.Controls.Add(this.chk6);
			base.Controls.Add(this.txtf_PIN);
			base.Controls.Add(this.cbof_GroupID);
			base.Controls.Add(this.GroupBox4);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.chk1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.chk3);
			base.Controls.Add(this.chk4);
			base.Controls.Add(this.GroupBox2);
			base.Controls.Add(this.GroupBox3);
			base.Controls.Add(this.cbof_GroupNew);
			base.Controls.Add(this.chk5);
			base.Controls.Add(this.chk2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmUserBatchUpdate";
			base.Load += new EventHandler(this.dfrmUserBatchUpdate_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmUserBatchUpdate_KeyDown);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox3.ResumeLayout(false);
			this.GroupBox4.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
