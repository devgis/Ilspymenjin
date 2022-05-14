using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmTCPIPConfigure : frmN3000
	{
		private IContainer components;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label1;

		private Label label2;

		private TextBox txtf_ControllerSN;

		private TextBox txtf_MACAddr;

		private Label label3;

		private TextBox txtf_IP;

		private Label label4;

		private TextBox txtf_mask;

		private Label label5;

		private TextBox txtf_gateway;

		private Button btnOK;

		private Button btnCancel;

		private Button btnOption;

		private Label label6;

		private GroupBox grpPort;

		private NumericUpDown nudPort;

		public string strSN = "";

		public string strMac = "";

		public string strIP = "";

		public string strMask = "";

		public string strGateway = "";

		public string strTCPPort = "";

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmTCPIPConfigure));
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label1 = new Label();
			this.label2 = new Label();
			this.txtf_ControllerSN = new TextBox();
			this.txtf_MACAddr = new TextBox();
			this.label3 = new Label();
			this.txtf_IP = new TextBox();
			this.label4 = new Label();
			this.txtf_mask = new TextBox();
			this.label5 = new Label();
			this.txtf_gateway = new TextBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.btnOption = new Button();
			this.label6 = new Label();
			this.grpPort = new GroupBox();
			this.nudPort = new NumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			this.grpPort.SuspendLayout();
			((ISupportInitialize)this.nudPort).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.BackColor = Color.Transparent;
			componentResourceManager.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtf_ControllerSN, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtf_MACAddr, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtf_IP, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtf_mask, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.txtf_gateway, 1, 4);
			this.tableLayoutPanel1.ForeColor = Color.White;
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.txtf_ControllerSN, "txtf_ControllerSN");
			this.txtf_ControllerSN.Name = "txtf_ControllerSN";
			this.txtf_ControllerSN.ReadOnly = true;
			this.txtf_ControllerSN.TabStop = false;
			componentResourceManager.ApplyResources(this.txtf_MACAddr, "txtf_MACAddr");
			this.txtf_MACAddr.Name = "txtf_MACAddr";
			this.txtf_MACAddr.ReadOnly = true;
			this.txtf_MACAddr.TabStop = false;
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.txtf_IP, "txtf_IP");
			this.txtf_IP.Name = "txtf_IP";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.txtf_mask, "txtf_mask");
			this.txtf_mask.Name = "txtf_mask";
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			componentResourceManager.ApplyResources(this.txtf_gateway, "txtf_gateway");
			this.txtf_gateway.Name = "txtf_gateway";
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnOption.BackColor = Color.Transparent;
			this.btnOption.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOption, "btnOption");
			this.btnOption.ForeColor = Color.White;
			this.btnOption.Name = "btnOption";
			this.btnOption.TabStop = false;
			this.btnOption.UseVisualStyleBackColor = false;
			this.btnOption.Click += new EventHandler(this.btnOption_Click);
			componentResourceManager.ApplyResources(this.label6, "label6");
			this.label6.ForeColor = Color.White;
			this.label6.Name = "label6";
			this.grpPort.BackColor = Color.Transparent;
			this.grpPort.Controls.Add(this.nudPort);
			this.grpPort.Controls.Add(this.label6);
			componentResourceManager.ApplyResources(this.grpPort, "grpPort");
			this.grpPort.Name = "grpPort";
			this.grpPort.TabStop = false;
			componentResourceManager.ApplyResources(this.nudPort, "nudPort");
			NumericUpDown arg_5C0_0 = this.nudPort;
			int[] array = new int[4];
			array[0] = 65534;
			arg_5C0_0.Maximum = new decimal(array);
			NumericUpDown arg_5E0_0 = this.nudPort;
			int[] array2 = new int[4];
			array2[0] = 1024;
			arg_5E0_0.Minimum = new decimal(array2);
			this.nudPort.Name = "nudPort";
			this.nudPort.TabStop = false;
			NumericUpDown arg_61C_0 = this.nudPort;
			int[] array3 = new int[4];
			array3[0] = 60000;
			arg_61C_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.grpPort);
			base.Controls.Add(this.btnOption);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.tableLayoutPanel1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmTCPIPConfigure";
			base.Load += new EventHandler(this.dfrmTCPIPConfigure_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.grpPort.ResumeLayout(false);
			this.grpPort.PerformLayout();
			((ISupportInitialize)this.nudPort).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmTCPIPConfigure()
		{
			this.InitializeComponent();
		}

		private void btnOption_Click(object sender, EventArgs e)
		{
			base.Size = new Size(460, 380);
			this.btnOption.Enabled = false;
		}

		public bool isIPAddress(string ipstr)
		{
			bool result = false;
			try
			{
				if (!string.IsNullOrEmpty(ipstr))
				{
					string[] array = ipstr.Split(new char[]
					{
						'.'
					});
					if (array.Length == 4)
					{
						result = true;
						for (int i = 0; i <= 3; i++)
						{
							int num;
							if (!int.TryParse(array[i], out num))
							{
								result = false;
								break;
							}
							if (num < 0 || num > 255)
							{
								result = false;
								break;
							}
						}
						if (int.Parse(array[0]) == 0)
						{
							result = false;
						}
						else if (int.Parse(array[3]) == 255)
						{
							result = false;
						}
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!this.txtf_ControllerSN.ReadOnly)
			{
				this.txtf_ControllerSN.Text = this.txtf_ControllerSN.Text.Trim();
				int num;
				if (!int.TryParse(this.txtf_ControllerSN.Text, out num))
				{
					XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (wgMjController.GetControllerType(int.Parse(this.txtf_ControllerSN.Text)) == 0)
				{
					XMessageBox.Show(this, CommonStr.strSNWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			if (string.IsNullOrEmpty(this.txtf_IP.Text))
			{
				XMessageBox.Show(this, CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.txtf_IP.Text = this.txtf_IP.Text.Replace(" ", "");
			if (!this.isIPAddress(this.txtf_IP.Text))
			{
				XMessageBox.Show(this, this.txtf_IP.Text + "  " + CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.txtf_mask.Text = this.txtf_mask.Text.Replace(" ", "");
			if (!this.isIPAddress(this.txtf_mask.Text))
			{
				XMessageBox.Show(this, this.txtf_mask.Text + "  " + CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.txtf_gateway.Text = this.txtf_gateway.Text.Replace(" ", "");
			if (!string.IsNullOrEmpty(this.txtf_gateway.Text) && !this.isIPAddress(this.txtf_gateway.Text))
			{
				XMessageBox.Show(this, this.txtf_gateway.Text + "  " + CommonStr.strIPAddrWrong, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.strSN = this.txtf_ControllerSN.Text;
			this.strMac = this.txtf_MACAddr.Text;
			this.strIP = this.txtf_IP.Text;
			this.strMask = this.txtf_mask.Text;
			this.strGateway = this.txtf_gateway.Text;
			this.strTCPPort = this.nudPort.Value.ToString();
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void dfrmTCPIPConfigure_Load(object sender, EventArgs e)
		{
			this.txtf_ControllerSN.Text = this.strSN;
			this.txtf_MACAddr.Text = this.strMac;
			this.txtf_IP.Text = this.strIP;
			this.txtf_mask.Text = this.strMask;
			this.txtf_gateway.Text = this.strGateway;
			if (int.Parse(this.strTCPPort) < this.nudPort.Minimum || int.Parse(this.strTCPPort) >= 65535)
			{
				this.strTCPPort = 60000.ToString();
			}
			this.nudPort.Value = int.Parse(this.strTCPPort);
			if (this.txtf_IP.Text == "255.255.255.255")
			{
				this.txtf_IP.Text = "192.168.0.0";
			}
			if (this.txtf_mask.Text == "255.255.255.255")
			{
				this.txtf_mask.Text = "255.255.255.0";
			}
			if (this.txtf_gateway.Text == "255.255.255.255")
			{
				this.txtf_gateway.Text = "";
			}
			if (this.txtf_gateway.Text == "0.0.0.0")
			{
				this.txtf_gateway.Text = "";
			}
		}
	}
}
