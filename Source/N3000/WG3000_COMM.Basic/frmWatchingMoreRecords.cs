using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;

namespace WG3000_COMM.Basic
{
	public class frmWatchingMoreRecords : frmN3000
	{
		public DataTable tbRunInfoLog;

		public int groupMax = 3;

		public float InfoFontSize = 15f;

		private int lastCnt = -1;

		private GroupBox[] grp;

		private RichTextBox[] txtB;

		private PictureBox[] picBox;

		private IContainer components;

		private FlowLayoutPanel flowLayoutPanel1;

		private GroupBox groupBox1;

		private PictureBox pictureBox1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripMenuItem toolStripMenuItem3;

		private ToolStripMenuItem toolStripMenuItem4;

		private ToolStripMenuItem toolStripMenuItem5;

		private ToolStripMenuItem toolStripMenuItem6;

		private ToolStripMenuItem enlargeInfoDisplayToolStripMenuItem;

		private ToolStripMenuItem saveDisplayStyleToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultToolStripMenuItem;

		private RichTextBox richTextBox1;

		private GroupBox groupBox2;

		private PictureBox pictureBox2;

		private RichTextBox richTextBox2;

		private GroupBox groupBox3;

		private PictureBox pictureBox3;

		private RichTextBox richTextBox3;

		private GroupBox groupBox4;

		private PictureBox pictureBox4;

		private RichTextBox richTextBox4;

		private GroupBox groupBox5;

		private PictureBox pictureBox5;

		private RichTextBox richTextBox5;

		private ToolStripMenuItem enlargeFontToolStripMenuItem;

		private ToolStripMenuItem ReduceFontToolStripMenuItem;

		private ToolStripMenuItem ReduceInfoDisplaytoolStripMenuItem;

		private ToolTip toolTip1;

		private Timer timer1;

		public frmWatchingMoreRecords()
		{
			this.InitializeComponent();
		}

		private void frmWatchingMoreRecords_Load(object sender, EventArgs e)
		{
			if (this.tbRunInfoLog != null)
			{
				string keyVal = wgAppConfig.GetKeyVal("WatchingMoreRecords_Display");
				if (!string.IsNullOrEmpty(keyVal))
				{
					try
					{
						string[] array = keyVal.Split(new char[]
						{
							','
						});
						RichTextBox[] array2 = new RichTextBox[]
						{
							this.richTextBox1,
							this.richTextBox2,
							this.richTextBox3,
							this.richTextBox4,
							this.richTextBox5
						};
						base.Size = new Size(int.Parse(array[0]), int.Parse(array[1]));
						base.Location = new Point(int.Parse(array[2]), int.Parse(array[3]));
						float.TryParse(array[6], out this.InfoFontSize);
						for (int i = 0; i < 5; i++)
						{
							array2[i].Size = new Size(array2[0].Size.Width, int.Parse(array[4]));
							array2[i].Font = new Font("宋体", this.InfoFontSize, FontStyle.Bold, array2[i].Font.Unit);
						}
						this.groupMax = int.Parse(array[5]);
					}
					catch (Exception ex)
					{
						wgAppConfig.wgLog(ex.ToString());
					}
				}
				GroupBox[] array3 = new GroupBox[]
				{
					this.groupBox1,
					this.groupBox2,
					this.groupBox3,
					this.groupBox4,
					this.groupBox5
				};
				for (int j = 0; j < 5; j++)
				{
					if (j >= this.groupMax)
					{
						array3[j].Visible = false;
					}
					else
					{
						array3[j].Visible = true;
					}
				}
				this.lstSwipes_RowsAdded(null, null);
				this.frmWatchingMoreRecords_SizeChanged(null, null);
			}
			this.timer1.Enabled = true;
		}

		private void lstSwipes_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			this.richTextBox1.Text = "";
			this.richTextBox2.Text = "";
			this.richTextBox3.Text = "";
			this.richTextBox4.Text = "";
			this.richTextBox5.Text = "";
			this.richTextBox1.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox2.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox3.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox4.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox5.BackColor = Color.FromArgb(128, 131, 156);
			this.groupBox1.Text = "";
			this.groupBox2.Text = "";
			this.groupBox3.Text = "";
			this.groupBox4.Text = "";
			this.groupBox5.Text = "";
			this.pictureBox1.Image = null;
			this.pictureBox2.Image = null;
			this.pictureBox3.Image = null;
			this.pictureBox4.Image = null;
			this.pictureBox5.Image = null;
			this.lastCnt = 0;
		}

		private void lstSwipes_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (this.tbRunInfoLog == null)
			{
				return;
			}
			this.lastCnt = this.tbRunInfoLog.Rows.Count;
			if (this.tbRunInfoLog.Rows.Count == 0)
			{
				this.lstSwipes_RowsRemoved(null, null);
				return;
			}
			base.SuspendLayout();
			if (this.tbRunInfoLog.Rows.Count > 0)
			{
				int num = 0;
				if (this.grp == null)
				{
					this.grp = new GroupBox[]
					{
						this.groupBox1,
						this.groupBox2,
						this.groupBox3,
						this.groupBox4,
						this.groupBox5
					};
					this.txtB = new RichTextBox[]
					{
						this.richTextBox1,
						this.richTextBox2,
						this.richTextBox3,
						this.richTextBox4,
						this.richTextBox5
					};
					this.picBox = new PictureBox[]
					{
						this.pictureBox1,
						this.pictureBox2,
						this.pictureBox3,
						this.pictureBox4,
						this.pictureBox5
					};
				}
				for (int i = this.tbRunInfoLog.Rows.Count - 1; i >= 0; i--)
				{
					string text = this.tbRunInfoLog.Rows[i]["f_Detail"] as string;
					string value = this.tbRunInfoLog.Rows[i]["f_MjRecStr"] as string;
					if (!string.IsNullOrEmpty(value))
					{
						MjRec mjRec = new MjRec(this.tbRunInfoLog.Rows[i]["f_MjRecStr"] as string);
						if (mjRec.IsSwipeRecord)
						{
							this.loadPhoto((long)((ulong)mjRec.CardID), ref this.picBox[num]);
							this.txtB[num].Text = text;
							this.txtB[num].Font = new Font("宋体", this.InfoFontSize, FontStyle.Bold, this.txtB[num].Font.Unit);
							this.grp[num].Text = this.tbRunInfoLog.Rows[i]["f_RecID"].ToString();
							this.grp[num].Visible = true;
							if (mjRec.IsPassed)
							{
								this.txtB[num].BackColor = Color.FromArgb(128, 131, 156);
							}
							else
							{
								this.txtB[num].BackColor = Color.Orange;
							}
							num++;
							if (num >= this.groupMax)
							{
								break;
							}
						}
					}
				}
				this.richTextBox1.Text = this.txtB[0].Text;
			}
			base.ResumeLayout();
		}

		private void loadPhoto(long cardno, ref PictureBox box)
		{
			try
			{
				box.Visible = false;
				string photoFileName = wgAppConfig.getPhotoFileName(cardno);
				Image image = box.Image;
				wgAppConfig.ShowMyImage(photoFileName, ref image);
				if (image != null)
				{
					box.Image = image;
					box.Visible = true;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public void ReallyCloseForm()
		{
			wgAppConfig.DisposeImage(this.pictureBox1.Image);
			wgAppConfig.DisposeImage(this.pictureBox2.Image);
			wgAppConfig.DisposeImage(this.pictureBox3.Image);
			wgAppConfig.DisposeImage(this.pictureBox4.Image);
			wgAppConfig.DisposeImage(this.pictureBox5.Image);
			base.Close();
		}

		private void frmWatchingMoreRecords_FormClosing(object sender, FormClosingEventArgs e)
		{
			wgAppConfig.DisposeImage(this.pictureBox1.Image);
			wgAppConfig.DisposeImage(this.pictureBox2.Image);
			wgAppConfig.DisposeImage(this.pictureBox3.Image);
			wgAppConfig.DisposeImage(this.pictureBox4.Image);
			wgAppConfig.DisposeImage(this.pictureBox5.Image);
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			int num;
			if (sender == this.toolStripMenuItem2)
			{
				num = 5;
			}
			else if (sender == this.toolStripMenuItem3)
			{
				num = 4;
			}
			else if (sender == this.toolStripMenuItem4)
			{
				num = 3;
			}
			else if (sender == this.toolStripMenuItem5)
			{
				num = 2;
			}
			else
			{
				if (sender != this.toolStripMenuItem6)
				{
					return;
				}
				num = 1;
			}
			GroupBox[] array = new GroupBox[]
			{
				this.groupBox1,
				this.groupBox2,
				this.groupBox3,
				this.groupBox4,
				this.groupBox5
			};
			for (int i = 0; i < 5; i++)
			{
				if (i >= num)
				{
					array[i].Visible = false;
				}
				else
				{
					array[i].Visible = true;
				}
			}
			this.groupMax = num;
			this.frmWatchingMoreRecords_SizeChanged(null, null);
		}

		private void frmWatchingMoreRecords_SizeChanged(object sender, EventArgs e)
		{
			GroupBox[] array = new GroupBox[]
			{
				this.groupBox1,
				this.groupBox2,
				this.groupBox3,
				this.groupBox4,
				this.groupBox5
			};
			for (int i = 0; i < this.groupMax; i++)
			{
				array[i].Size = new Size(this.flowLayoutPanel1.Width / this.groupMax - 8, this.flowLayoutPanel1.Height - 18);
			}
		}

		private void enlargeInfoDisplayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				RichTextBox[] array = new RichTextBox[]
				{
					this.richTextBox1,
					this.richTextBox2,
					this.richTextBox3,
					this.richTextBox4,
					this.richTextBox5
				};
				PictureBox[] array2 = new PictureBox[]
				{
					this.pictureBox1,
					this.pictureBox2,
					this.pictureBox3,
					this.pictureBox4,
					this.pictureBox5
				};
				for (int i = 0; i < 5; i++)
				{
					if (array2[i].Height > 26)
					{
						array[i].Size = new Size(array[i].Width, array[i].Height + 26);
						array2[i].Location = new Point(array2[i].Location.X, array2[i].Location.Y + 26);
						array2[i].Size = new Size(array2[i].Width, array2[i].Height - 26);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void saveDisplayStyleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = base.Size.Width.ToString() + "," + base.Size.Height.ToString() + ",";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				base.Location.X.ToString(),
				",",
				base.Location.Y.ToString(),
				","
			});
			text = text + this.richTextBox1.Height.ToString() + "," + this.groupMax.ToString();
			text = text + "," + this.InfoFontSize.ToString();
			wgAppConfig.UpdateKeyVal("WatchingMoreRecords_Display", text);
		}

		private void restoreDefaultToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wgAppConfig.UpdateKeyVal("WatchingMoreRecords_Display", "");
			base.Close();
		}

		private void enlargeFontToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				this.InfoFontSize += 1f;
				RichTextBox[] array = new RichTextBox[]
				{
					this.richTextBox1,
					this.richTextBox2,
					this.richTextBox3,
					this.richTextBox4,
					this.richTextBox5
				};
				for (int i = 0; i < 5; i++)
				{
					array[i].Font = new Font("宋体", this.InfoFontSize, FontStyle.Bold, array[i].Font.Unit);
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void ReduceFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.InfoFontSize > 9f)
				{
					this.InfoFontSize -= 1f;
					RichTextBox[] array = new RichTextBox[]
					{
						this.richTextBox1,
						this.richTextBox2,
						this.richTextBox3,
						this.richTextBox4,
						this.richTextBox5
					};
					for (int i = 0; i < 5; i++)
					{
						array[i].Font = new Font("宋体", this.InfoFontSize, FontStyle.Bold, array[i].Font.Unit);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void ReduceInfoDisplaytoolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.richTextBox1.Height >= 26)
				{
					RichTextBox[] array = new RichTextBox[]
					{
						this.richTextBox1,
						this.richTextBox2,
						this.richTextBox3,
						this.richTextBox4,
						this.richTextBox5
					};
					PictureBox[] array2 = new PictureBox[]
					{
						this.pictureBox1,
						this.pictureBox2,
						this.pictureBox3,
						this.pictureBox4,
						this.pictureBox5
					};
					for (int i = 0; i < 5; i++)
					{
						array[i].Size = new Size(array[i].Width, array[i].Height - 26);
						array2[i].Location = new Point(array2[i].Location.X, array2[i].Location.Y - 26);
						array2[i].Size = new Size(array2[i].Width, array2[i].Height + 26);
					}
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			try
			{
				if (this.tbRunInfoLog != null && this.lastCnt != this.tbRunInfoLog.Rows.Count)
				{
					this.lstSwipes_RowsAdded(null, null);
				}
			}
			catch (Exception)
			{
			}
			this.timer1.Enabled = true;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmWatchingMoreRecords));
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.toolStripMenuItem2 = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripMenuItem();
			this.toolStripMenuItem6 = new ToolStripMenuItem();
			this.enlargeInfoDisplayToolStripMenuItem = new ToolStripMenuItem();
			this.ReduceInfoDisplaytoolStripMenuItem = new ToolStripMenuItem();
			this.enlargeFontToolStripMenuItem = new ToolStripMenuItem();
			this.ReduceFontToolStripMenuItem = new ToolStripMenuItem();
			this.saveDisplayStyleToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultToolStripMenuItem = new ToolStripMenuItem();
			this.toolTip1 = new ToolTip(this.components);
			this.timer1 = new Timer(this.components);
			this.flowLayoutPanel1 = new FlowLayoutPanel();
			this.groupBox1 = new GroupBox();
			this.pictureBox1 = new PictureBox();
			this.richTextBox1 = new RichTextBox();
			this.groupBox2 = new GroupBox();
			this.pictureBox2 = new PictureBox();
			this.richTextBox2 = new RichTextBox();
			this.groupBox3 = new GroupBox();
			this.pictureBox3 = new PictureBox();
			this.richTextBox3 = new RichTextBox();
			this.groupBox4 = new GroupBox();
			this.pictureBox4 = new PictureBox();
			this.richTextBox4 = new RichTextBox();
			this.groupBox5 = new GroupBox();
			this.pictureBox5 = new PictureBox();
			this.richTextBox5 = new RichTextBox();
			this.contextMenuStrip1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.pictureBox3).BeginInit();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.pictureBox4).BeginInit();
			this.groupBox5.SuspendLayout();
			((ISupportInitialize)this.pictureBox5).BeginInit();
			base.SuspendLayout();
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem2,
				this.toolStripMenuItem3,
				this.toolStripMenuItem4,
				this.toolStripMenuItem5,
				this.toolStripMenuItem6,
				this.enlargeInfoDisplayToolStripMenuItem,
				this.ReduceInfoDisplaytoolStripMenuItem,
				this.enlargeFontToolStripMenuItem,
				this.ReduceFontToolStripMenuItem,
				this.saveDisplayStyleToolStripMenuItem,
				this.restoreDefaultToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			componentResourceManager.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			this.toolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			componentResourceManager.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			this.toolStripMenuItem3.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			componentResourceManager.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			this.toolStripMenuItem4.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			componentResourceManager.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			this.toolStripMenuItem5.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			componentResourceManager.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			this.toolStripMenuItem6.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.enlargeInfoDisplayToolStripMenuItem.Name = "enlargeInfoDisplayToolStripMenuItem";
			componentResourceManager.ApplyResources(this.enlargeInfoDisplayToolStripMenuItem, "enlargeInfoDisplayToolStripMenuItem");
			this.enlargeInfoDisplayToolStripMenuItem.Click += new EventHandler(this.enlargeInfoDisplayToolStripMenuItem_Click);
			this.ReduceInfoDisplaytoolStripMenuItem.Name = "ReduceInfoDisplaytoolStripMenuItem";
			componentResourceManager.ApplyResources(this.ReduceInfoDisplaytoolStripMenuItem, "ReduceInfoDisplaytoolStripMenuItem");
			this.ReduceInfoDisplaytoolStripMenuItem.Click += new EventHandler(this.ReduceInfoDisplaytoolStripMenuItem_Click);
			this.enlargeFontToolStripMenuItem.Name = "enlargeFontToolStripMenuItem";
			componentResourceManager.ApplyResources(this.enlargeFontToolStripMenuItem, "enlargeFontToolStripMenuItem");
			this.enlargeFontToolStripMenuItem.Click += new EventHandler(this.enlargeFontToolStripMenuItem1_Click);
			this.ReduceFontToolStripMenuItem.Name = "ReduceFontToolStripMenuItem";
			componentResourceManager.ApplyResources(this.ReduceFontToolStripMenuItem, "ReduceFontToolStripMenuItem");
			this.ReduceFontToolStripMenuItem.Click += new EventHandler(this.ReduceFontToolStripMenuItem_Click);
			this.saveDisplayStyleToolStripMenuItem.Name = "saveDisplayStyleToolStripMenuItem";
			componentResourceManager.ApplyResources(this.saveDisplayStyleToolStripMenuItem, "saveDisplayStyleToolStripMenuItem");
			this.saveDisplayStyleToolStripMenuItem.Click += new EventHandler(this.saveDisplayStyleToolStripMenuItem_Click);
			this.restoreDefaultToolStripMenuItem.Name = "restoreDefaultToolStripMenuItem";
			componentResourceManager.ApplyResources(this.restoreDefaultToolStripMenuItem, "restoreDefaultToolStripMenuItem");
			this.restoreDefaultToolStripMenuItem.Click += new EventHandler(this.restoreDefaultToolStripMenuItem_Click);
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.flowLayoutPanel1.Controls.Add(this.groupBox1);
			this.flowLayoutPanel1.Controls.Add(this.groupBox2);
			this.flowLayoutPanel1.Controls.Add(this.groupBox3);
			this.flowLayoutPanel1.Controls.Add(this.groupBox4);
			this.flowLayoutPanel1.Controls.Add(this.groupBox5);
			componentResourceManager.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Controls.Add(this.richTextBox1);
			this.groupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.BackColor = Color.FromArgb(128, 131, 156);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.richTextBox1, "richTextBox1");
			this.richTextBox1.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox1.BorderStyle = BorderStyle.None;
			this.richTextBox1.ForeColor = Color.White;
			this.richTextBox1.Name = "richTextBox1";
			this.groupBox2.Controls.Add(this.pictureBox2);
			this.groupBox2.Controls.Add(this.richTextBox2);
			this.groupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.pictureBox2, "pictureBox2");
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.richTextBox2, "richTextBox2");
			this.richTextBox2.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox2.BorderStyle = BorderStyle.None;
			this.richTextBox2.ForeColor = Color.White;
			this.richTextBox2.Name = "richTextBox2";
			this.groupBox3.Controls.Add(this.pictureBox3);
			this.groupBox3.Controls.Add(this.richTextBox3);
			this.groupBox3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.pictureBox3, "pictureBox3");
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.richTextBox3, "richTextBox3");
			this.richTextBox3.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox3.BorderStyle = BorderStyle.None;
			this.richTextBox3.ForeColor = Color.White;
			this.richTextBox3.Name = "richTextBox3";
			this.groupBox4.Controls.Add(this.pictureBox4);
			this.groupBox4.Controls.Add(this.richTextBox4);
			this.groupBox4.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.pictureBox4, "pictureBox4");
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.richTextBox4, "richTextBox4");
			this.richTextBox4.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox4.BorderStyle = BorderStyle.None;
			this.richTextBox4.ForeColor = Color.White;
			this.richTextBox4.Name = "richTextBox4";
			this.groupBox5.Controls.Add(this.pictureBox5);
			this.groupBox5.Controls.Add(this.richTextBox5);
			this.groupBox5.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			componentResourceManager.ApplyResources(this.pictureBox5, "pictureBox5");
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.TabStop = false;
			componentResourceManager.ApplyResources(this.richTextBox5, "richTextBox5");
			this.richTextBox5.BackColor = Color.FromArgb(128, 131, 156);
			this.richTextBox5.BorderStyle = BorderStyle.None;
			this.richTextBox5.ForeColor = Color.White;
			this.richTextBox5.Name = "richTextBox5";
			componentResourceManager.ApplyResources(this, "$this");
			this.ContextMenuStrip = this.contextMenuStrip1;
			base.Controls.Add(this.flowLayoutPanel1);
			base.MinimizeBox = false;
			base.Name = "frmWatchingMoreRecords";
			this.toolTip1.SetToolTip(this, componentResourceManager.GetString("$this.ToolTip"));
			base.FormClosing += new FormClosingEventHandler(this.frmWatchingMoreRecords_FormClosing);
			base.Load += new EventHandler(this.frmWatchingMoreRecords_Load);
			base.SizeChanged += new EventHandler(this.frmWatchingMoreRecords_SizeChanged);
			this.contextMenuStrip1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox2).EndInit();
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox3).EndInit();
			this.groupBox4.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox4).EndInit();
			this.groupBox5.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox5).EndInit();
			base.ResumeLayout(false);
		}
	}
}
