using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmOptionAdvanced : frmN3000
	{
		private IContainer components;

		internal Button cmdCancel;

		internal Button cmdOK;

		private CheckBox chkAllowUploadUserName;

		private GroupBox groupBox1;

		private FolderBrowserDialog folderBrowserDialog1;

		private Button btnBrowse;

		private TextBox txtPhotoDirectory;

		private Label label2;

		private NumericUpDown nudValidSwipeGap;

		private CheckBox chkValidSwipeGap;

		private GroupBox groupBox2;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOptionAdvanced));
			this.folderBrowserDialog1 = new FolderBrowserDialog();
			this.groupBox2 = new GroupBox();
			this.chkAllowUploadUserName = new CheckBox();
			this.groupBox1 = new GroupBox();
			this.nudValidSwipeGap = new NumericUpDown();
			this.chkValidSwipeGap = new CheckBox();
			this.btnBrowse = new Button();
			this.txtPhotoDirectory = new TextBox();
			this.label2 = new Label();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.nudValidSwipeGap).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.chkAllowUploadUserName);
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.chkAllowUploadUserName, "chkAllowUploadUserName");
			this.chkAllowUploadUserName.BackColor = Color.Transparent;
			this.chkAllowUploadUserName.ForeColor = Color.White;
			this.chkAllowUploadUserName.Name = "chkAllowUploadUserName";
			this.chkAllowUploadUserName.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.nudValidSwipeGap);
			this.groupBox1.Controls.Add(this.chkValidSwipeGap);
			this.groupBox1.Controls.Add(this.btnBrowse);
			this.groupBox1.Controls.Add(this.txtPhotoDirectory);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.nudValidSwipeGap, "nudValidSwipeGap");
			NumericUpDown arg_213_0 = this.nudValidSwipeGap;
			int[] array = new int[4];
			array[0] = 2;
			arg_213_0.Increment = new decimal(array);
			NumericUpDown arg_233_0 = this.nudValidSwipeGap;
			int[] array2 = new int[4];
			array2[0] = 86400;
			arg_233_0.Maximum = new decimal(array2);
			NumericUpDown arg_24F_0 = this.nudValidSwipeGap;
			int[] array3 = new int[4];
			array3[0] = 6;
			arg_24F_0.Minimum = new decimal(array3);
			this.nudValidSwipeGap.Name = "nudValidSwipeGap";
			this.nudValidSwipeGap.ReadOnly = true;
			NumericUpDown arg_28B_0 = this.nudValidSwipeGap;
			int[] array4 = new int[4];
			array4[0] = 30;
			arg_28B_0.Value = new decimal(array4);
			componentResourceManager.ApplyResources(this.chkValidSwipeGap, "chkValidSwipeGap");
			this.chkValidSwipeGap.BackColor = Color.Transparent;
			this.chkValidSwipeGap.ForeColor = Color.White;
			this.chkValidSwipeGap.Name = "chkValidSwipeGap";
			this.chkValidSwipeGap.UseVisualStyleBackColor = false;
			this.chkValidSwipeGap.CheckedChanged += new EventHandler(this.chkValidSwipeGap_CheckedChanged);
			this.btnBrowse.BackColor = Color.Transparent;
			this.btnBrowse.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnBrowse, "btnBrowse");
			this.btnBrowse.ForeColor = Color.White;
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.UseVisualStyleBackColor = false;
			this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
			componentResourceManager.ApplyResources(this.txtPhotoDirectory, "txtPhotoDirectory");
			this.txtPhotoDirectory.Name = "txtPhotoDirectory";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Name = "dfrmOptionAdvanced";
			base.Load += new EventHandler(this.dfrmOptionAdvanced_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmOptionAdvanced_KeyDown);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.nudValidSwipeGap).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmOptionAdvanced()
		{
			this.InitializeComponent();
		}

		private void dfrmOptionAdvanced_Load(object sender, EventArgs e)
		{
			if (icOperator.OperatorID != 1)
			{
				XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.chkAllowUploadUserName.Checked = (wgAppConfig.GetKeyVal("AllowUploadUserName") == "1");
			this.txtPhotoDirectory.Text = wgAppConfig.getSystemParamByNO(41);
			this.chkValidSwipeGap.Visible = wgAppConfig.getParamValBoolByNO(147);
			this.chkValidSwipeGap.Checked = wgAppConfig.getParamValBoolByNO(147);
			this.nudValidSwipeGap.Visible = wgAppConfig.getParamValBoolByNO(147);
			this.nudValidSwipeGap.Enabled = false;
			if (this.chkValidSwipeGap.Checked)
			{
				this.nudValidSwipeGap.Value = int.Parse(wgAppConfig.getSystemParamByNO(147));
				this.nudValidSwipeGap.Enabled = true;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.txtPhotoDirectory.Text.Trim()) && this.txtPhotoDirectory.Text.Trim().Length != Encoding.GetEncoding("utf-8").GetBytes(this.txtPhotoDirectory.Text.Trim()).Length)
			{
				XMessageBox.Show(this, CommonStr.strInvalidPhotoDirectory, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			wgAppConfig.UpdateKeyVal("AllowUploadUserName", this.chkAllowUploadUserName.Checked ? "1" : "0");
			wgAppConfig.setSystemParamValue(41, this.txtPhotoDirectory.Text.Trim());
			if (this.chkValidSwipeGap.Visible)
			{
				int num = 0;
				if (this.chkValidSwipeGap.Checked)
				{
					num = (int)this.nudValidSwipeGap.Value;
				}
				if ((num & 1) > 0)
				{
					num++;
				}
				wgAppConfig.setSystemParamValue(147, num.ToString());
			}
			base.Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.folderBrowserDialog1.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.txtPhotoDirectory.Text = this.folderBrowserDialog1.SelectedPath;
			}
		}

		private void funcCtrlShiftQ()
		{
			this.txtPhotoDirectory.ReadOnly = false;
			this.chkValidSwipeGap.Visible = true;
			this.nudValidSwipeGap.Visible = true;
		}

		private void dfrmOptionAdvanced_KeyDown(object sender, KeyEventArgs e)
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

		private void chkValidSwipeGap_CheckedChanged(object sender, EventArgs e)
		{
			this.nudValidSwipeGap.Enabled = this.chkValidSwipeGap.Checked;
		}
	}
}
