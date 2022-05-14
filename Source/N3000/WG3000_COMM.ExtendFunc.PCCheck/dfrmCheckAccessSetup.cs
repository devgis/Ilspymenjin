using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc.PCCheck
{
	public class dfrmCheckAccessSetup : frmN3000
	{
		public string groupname;

		public int active;

		public int morecards = 1;

		public string soundfilename;

		private string newSoundFile = "";

		private IContainer components;

		private OpenFileDialog openFileDialog1;

		internal CheckBox chkActive;

		internal GroupBox GroupBox1;

		internal NumericUpDown nudMoreCards;

		internal Label label14;

		internal Button btnOK;

		internal Button btnCancel;

		private Label label4;

		private TextBox txtGroupName;

		private TextBox txtFileName;

		internal Label label1;

		internal Button btnBrowse;

		public dfrmCheckAccessSetup()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.newSoundFile))
				{
					string fileName = this.newSoundFile;
					FileInfo fileInfo = new FileInfo(fileName);
					FileInfo fileInfo2 = new FileInfo(wgAppConfig.Path4PhotoDefault() + this.txtFileName.Text);
					if (!(fileInfo2.FullName.ToUpper() == this.newSoundFile.ToUpper()))
					{
						try
						{
							if (fileInfo2.Exists)
							{
								fileInfo2.Delete();
							}
						}
						catch (Exception ex)
						{
							wgTools.WgDebugWrite(ex.ToString(), new object[0]);
						}
						fileInfo.CopyTo(wgAppConfig.Path4PhotoDefault() + this.txtFileName.Text, true);
					}
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[0]);
			}
			try
			{
				if (this.chkActive.Checked)
				{
					this.active = 1;
					this.morecards = (int)this.nudMoreCards.Value;
					this.soundfilename = this.txtFileName.Text;
				}
				else
				{
					this.active = 0;
					this.morecards = 1;
					this.soundfilename = "";
				}
				base.DialogResult = DialogResult.OK;
			}
			catch (Exception ex3)
			{
				wgTools.WgDebugWrite(ex3.ToString(), new object[0]);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.openFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\\Photo\\";
				}
				catch (Exception)
				{
				}
				this.openFileDialog1.Filter = "(*.wav)|*.wav|(*.*)|*.*";
				this.openFileDialog1.FilterIndex = 1;
				this.openFileDialog1.RestoreDirectory = true;
				if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					this.newSoundFile = this.openFileDialog1.FileName;
					FileInfo fileInfo = new FileInfo(this.newSoundFile);
					this.txtFileName.Text = fileInfo.Name;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				Directory.SetCurrentDirectory(Application.StartupPath);
			}
		}

		private void dfrmCheckAccessSetup_Load(object sender, EventArgs e)
		{
			try
			{
				this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
				this.txtGroupName.Text = this.groupname;
				if (this.active > 0)
				{
					this.chkActive.Checked = true;
					this.GroupBox1.Enabled = true;
				}
				else
				{
					this.chkActive.Checked = false;
					this.GroupBox1.Enabled = false;
				}
				this.nudMoreCards.Value = this.morecards;
				this.txtFileName.Text = this.soundfilename;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			this.GroupBox1.Enabled = this.chkActive.Checked;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmCheckAccessSetup));
			this.openFileDialog1 = new OpenFileDialog();
			this.txtGroupName = new TextBox();
			this.label4 = new Label();
			this.chkActive = new CheckBox();
			this.GroupBox1 = new GroupBox();
			this.txtFileName = new TextBox();
			this.nudMoreCards = new NumericUpDown();
			this.label1 = new Label();
			this.label14 = new Label();
			this.btnBrowse = new Button();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.GroupBox1.SuspendLayout();
			((ISupportInitialize)this.nudMoreCards).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.txtGroupName, "txtGroupName");
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.chkActive, "chkActive");
			this.chkActive.BackColor = Color.Transparent;
			this.chkActive.ForeColor = Color.White;
			this.chkActive.Name = "chkActive";
			this.chkActive.UseVisualStyleBackColor = false;
			this.chkActive.CheckedChanged += new EventHandler(this.chkActive_CheckedChanged);
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.txtFileName);
			this.GroupBox1.Controls.Add(this.nudMoreCards);
			this.GroupBox1.Controls.Add(this.label1);
			this.GroupBox1.Controls.Add(this.label14);
			this.GroupBox1.Controls.Add(this.btnBrowse);
			this.GroupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.txtFileName, "txtFileName");
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.ReadOnly = true;
			this.nudMoreCards.BackColor = Color.White;
			componentResourceManager.ApplyResources(this.nudMoreCards, "nudMoreCards");
			this.nudMoreCards.Name = "nudMoreCards";
			this.nudMoreCards.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			this.btnBrowse.BackColor = Color.Transparent;
			this.btnBrowse.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnBrowse, "btnBrowse");
			this.btnBrowse.ForeColor = Color.White;
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.UseVisualStyleBackColor = false;
			this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
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
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtGroupName);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.chkActive);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmCheckAccessSetup";
			base.Load += new EventHandler(this.dfrmCheckAccessSetup_Load);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			((ISupportInitialize)this.nudMoreCards).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
