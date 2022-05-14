using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Elevator
{
	public class dfrmOneToMoreSetup : frmN3000
	{
		private IContainer components;

		private Button btnCancel;

		private Button btnOK;

		internal TextBox textBox0;

		internal Label Label1;

		internal TextBox textBox1;

		internal Label label2;

		public RadioButton radioButton0;

		public RadioButton radioButton2;

		public RadioButton radioButton1;

		internal TextBox textBox3;

		internal TextBox textBox2;

		internal TextBox textBox5;

		internal TextBox textBox4;

		private Label label141;

		private Label label142;

		public NumericUpDown numericUpDown20;

		public NumericUpDown numericUpDown21;

		public dfrmOneToMoreSetup()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void radioButton0_CheckedChanged(object sender, EventArgs e)
		{
			this.textBox0.Visible = false;
			this.textBox1.Visible = false;
			this.textBox2.Visible = false;
			this.textBox3.Visible = false;
			this.textBox4.Visible = false;
			this.textBox5.Visible = false;
			if (this.radioButton1.Checked)
			{
				this.textBox2.Visible = true;
				this.textBox3.Visible = true;
				return;
			}
			if (this.radioButton2.Checked)
			{
				this.textBox4.Visible = true;
				this.textBox5.Visible = true;
				return;
			}
			this.textBox0.Visible = true;
			this.textBox1.Visible = true;
		}

		private void dfrmOneToMoreSetup_Load(object sender, EventArgs e)
		{
			this.radioButton0_CheckedChanged(null, null);
		}

		private void funcCtrlShiftQ()
		{
			base.Size = new Size(554, 259);
		}

		private void dfrmOneToMoreSetup_KeyDown(object sender, KeyEventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOneToMoreSetup));
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.radioButton0 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.textBox0 = new TextBox();
			this.Label1 = new Label();
			this.textBox1 = new TextBox();
			this.label2 = new Label();
			this.textBox3 = new TextBox();
			this.textBox2 = new TextBox();
			this.textBox5 = new TextBox();
			this.textBox4 = new TextBox();
			this.numericUpDown20 = new NumericUpDown();
			this.label141 = new Label();
			this.numericUpDown21 = new NumericUpDown();
			this.label142 = new Label();
			((ISupportInitialize)this.numericUpDown20).BeginInit();
			((ISupportInitialize)this.numericUpDown21).BeginInit();
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
			componentResourceManager.ApplyResources(this.radioButton0, "radioButton0");
			this.radioButton0.BackColor = Color.Transparent;
			this.radioButton0.Checked = true;
			this.radioButton0.ForeColor = Color.White;
			this.radioButton0.Name = "radioButton0";
			this.radioButton0.TabStop = true;
			this.radioButton0.UseVisualStyleBackColor = false;
			this.radioButton0.CheckedChanged += new EventHandler(this.radioButton0_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.BackColor = Color.Transparent;
			this.radioButton2.ForeColor = Color.White;
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.UseVisualStyleBackColor = false;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton0_CheckedChanged);
			componentResourceManager.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.BackColor = Color.Transparent;
			this.radioButton1.ForeColor = Color.White;
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.UseVisualStyleBackColor = false;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton0_CheckedChanged);
			componentResourceManager.ApplyResources(this.textBox0, "textBox0");
			this.textBox0.Name = "textBox0";
			this.textBox0.ReadOnly = true;
			this.Label1.BackColor = Color.Transparent;
			this.Label1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			componentResourceManager.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.textBox3, "textBox3");
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.textBox2, "textBox2");
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.textBox5, "textBox5");
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			componentResourceManager.ApplyResources(this.textBox4, "textBox4");
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.numericUpDown20, "numericUpDown20");
			NumericUpDown arg_4CC_0 = this.numericUpDown20;
			int[] array = new int[4];
			array[0] = 25;
			arg_4CC_0.Maximum = new decimal(array);
			this.numericUpDown20.Name = "numericUpDown20";
			this.numericUpDown20.ReadOnly = true;
			NumericUpDown arg_504_0 = this.numericUpDown20;
			int[] array2 = new int[4];
			array2[0] = 5;
			arg_504_0.Value = new decimal(array2);
			componentResourceManager.ApplyResources(this.label141, "label141");
			this.label141.ForeColor = Color.White;
			this.label141.Name = "label141";
			this.numericUpDown21.DecimalPlaces = 1;
			this.numericUpDown21.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				65536
			});
			componentResourceManager.ApplyResources(this.numericUpDown21, "numericUpDown21");
			NumericUpDown arg_596_0 = this.numericUpDown21;
			int[] array3 = new int[4];
			array3[0] = 25;
			arg_596_0.Maximum = new decimal(array3);
			this.numericUpDown21.Minimum = new decimal(new int[]
			{
				3,
				0,
				0,
				65536
			});
			this.numericUpDown21.Name = "numericUpDown21";
			this.numericUpDown21.ReadOnly = true;
			this.numericUpDown21.Value = new decimal(new int[]
			{
				4,
				0,
				0,
				65536
			});
			componentResourceManager.ApplyResources(this.label142, "label142");
			this.label142.ForeColor = Color.White;
			this.label142.Name = "label142";
			componentResourceManager.ApplyResources(this, "$this");
			base.ControlBox = false;
			base.Controls.Add(this.numericUpDown20);
			base.Controls.Add(this.label141);
			base.Controls.Add(this.numericUpDown21);
			base.Controls.Add(this.label142);
			base.Controls.Add(this.textBox5);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.textBox3);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox0);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.radioButton0);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Name = "dfrmOneToMoreSetup";
			base.Load += new EventHandler(this.dfrmOneToMoreSetup_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmOneToMoreSetup_KeyDown);
			((ISupportInitialize)this.numericUpDown20).EndInit();
			((ISupportInitialize)this.numericUpDown21).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
