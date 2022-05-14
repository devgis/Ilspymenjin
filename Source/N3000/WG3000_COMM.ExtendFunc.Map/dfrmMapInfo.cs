using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Map
{
	public class dfrmMapInfo : frmN3000
	{
		public string mapName;

		public string mapFile;

		private ResourceManager resStr;

		private Container components;

		internal Label lblMapName;

		internal Label Label2;

		internal TextBox txtMapName;

		internal TextBox txtMapFileName;

		internal Button btnBrowse;

		internal OpenFileDialog OpenFileDialog1;

		internal Button btnOK;

		internal Button btnCancel;

		public dfrmMapInfo()
		{
			this.InitializeComponent();
			this.resStr = new ResourceManager("WgiCCard." + base.Name + "Str", Assembly.GetExecutingAssembly());
			this.resStr.IgnoreCase = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmMapInfo));
			this.lblMapName = new Label();
			this.txtMapName = new TextBox();
			this.Label2 = new Label();
			this.txtMapFileName = new TextBox();
			this.btnBrowse = new Button();
			this.OpenFileDialog1 = new OpenFileDialog();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			base.SuspendLayout();
			this.lblMapName.BackColor = Color.Transparent;
			this.lblMapName.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.lblMapName, "lblMapName");
			this.lblMapName.Name = "lblMapName";
			componentResourceManager.ApplyResources(this.txtMapName, "txtMapName");
			this.txtMapName.Name = "txtMapName";
			this.Label2.BackColor = Color.Transparent;
			this.Label2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.Label2, "Label2");
			this.Label2.Name = "Label2";
			componentResourceManager.ApplyResources(this.txtMapFileName, "txtMapFileName");
			this.txtMapFileName.Name = "txtMapFileName";
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
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			base.AcceptButton = this.btnOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnBrowse);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.txtMapName);
			base.Controls.Add(this.lblMapName);
			base.Controls.Add(this.txtMapFileName);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmMapInfo";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					if (this.txtMapFileName.Text != "")
					{
						this.OpenFileDialog1.InitialDirectory = this.txtMapFileName.Text;
					}
				}
				catch (Exception)
				{
				}
				this.OpenFileDialog1.FilterIndex = 1;
				if (this.OpenFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					this.txtMapFileName.Text = this.OpenFileDialog1.FileName;
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

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				this.txtMapName.Text = this.txtMapName.Text.Trim();
				this.txtMapFileName.Text = this.txtMapFileName.Text.Trim();
				if (this.txtMapName.Text == "")
				{
					XMessageBox.Show(CommonStr.strMapNameNull);
				}
				else if (this.txtMapFileName.Text == "")
				{
					XMessageBox.Show(CommonStr.strMapFileNull);
				}
				else
				{
					string text = this.txtMapFileName.Text;
					FileInfo fileInfo = new FileInfo(text);
					if (!fileInfo.Exists)
					{
						fileInfo = new FileInfo(wgAppConfig.Path4PhotoDefault() + fileInfo.Name);
						if (!fileInfo.Exists)
						{
							XMessageBox.Show(CommonStr.strMapFileNotExist);
							return;
						}
					}
					FileInfo fileInfo2 = new FileInfo(wgAppConfig.Path4PhotoDefault() + fileInfo.Name);
					if (!(fileInfo2.FullName.ToUpper() == fileInfo.FullName.ToUpper()))
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
							wgTools.WgDebugWrite(ex.ToString(), new object[]
							{
								EventLogEntryType.Error
							});
						}
						fileInfo.CopyTo(wgAppConfig.Path4PhotoDefault() + fileInfo.Name, true);
					}
					this.mapName = this.txtMapName.Text;
					this.mapFile = fileInfo.Name;
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			catch (Exception ex2)
			{
				wgTools.WgDebugWrite(ex2.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
	}
}
