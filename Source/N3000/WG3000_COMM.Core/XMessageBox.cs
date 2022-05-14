using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Core
{
	public class XMessageBox : Form
	{
		private const int minWidth = 180;

		private const int maxWidth = 600;

		private IContainer components;

		private Panel bottomPanel;

		private Button button2;

		private Button button3;

		private Button button1;

		private XTextBox TextBox1;

		private Panel bottomInnerPanel;

		private Panel leftPanel;

		private PictureBox pictureBox1;

		private XMessageBox()
		{
			this.InitializeComponent();
			this.TextBox1.Cursor = Cursors.Default;
		}

		private void XmessageBox(string text, string title, Size size, Font font, Color backColor, Color foreColor, bool customSize)
		{
			if (!string.IsNullOrEmpty(title))
			{
				this.Text = title;
			}
			this.TextBox1.Text = text;
			this.Font = font;
			this.BackColor = backColor;
			this.BackColor = Color.FromArgb(128, 131, 156);
			this.ForeColor = foreColor;
			Graphics graphics = base.CreateGraphics();
			SizeF sizeF = graphics.MeasureString(text, font);
			graphics.Dispose();
			int width;
			if (sizeF.Width <= 600f)
			{
				width = (int)sizeF.Width + 15;
			}
			else
			{
				width = 600;
			}
			int height = this.TextBox1.LinesCount() * this.TextBox1.Font.Height + 40 + 60;
			if (customSize)
			{
				base.Size = size;
				return;
			}
			base.Size = new Size(width, height);
		}

		private void SetButtons(XMessageBoxButtons buttons)
		{
			if (buttons == XMessageBoxButtons.OK)
			{
				this.button2.Visible = true;
				this.button2.Text = CommonStr.strMsgOK;
				this.button2.DialogResult = DialogResult.OK;
				base.ControlBox = true;
				return;
			}
			if (buttons == XMessageBoxButtons.AbortRetryIgnore)
			{
				this.button1.Visible = true;
				this.button1.Text = CommonStr.strMsgAbort;
				this.button1.DialogResult = DialogResult.Abort;
				this.button2.Visible = true;
				this.button2.Text = CommonStr.strMsgRetry;
				this.button2.DialogResult = DialogResult.Retry;
				this.button3.Visible = true;
				this.button3.Text = CommonStr.strMsgIgnore;
				this.button3.DialogResult = DialogResult.Ignore;
				if (base.Width < 180)
				{
					base.Width = 180;
					return;
				}
			}
			else if (buttons == XMessageBoxButtons.OKCancel)
			{
				this.button1.Visible = true;
				this.button1.Text = CommonStr.strMsgOK;
				this.button1.DialogResult = DialogResult.OK;
				this.button1.Location = new Point(41, 3);
				this.button2.Visible = true;
				this.button2.Text = CommonStr.strMsgCancel;
				this.button2.DialogResult = DialogResult.Cancel;
				this.button2.Location = new Point(122, 3);
				base.ControlBox = true;
				if (base.Width < 180)
				{
					base.Width = 180;
					return;
				}
			}
			else if (buttons == XMessageBoxButtons.YesNo)
			{
				this.button1.Visible = true;
				this.button1.Text = CommonStr.strMsgYes;
				this.button1.DialogResult = DialogResult.Yes;
				this.button1.Location = new Point(41, 3);
				this.button2.Visible = true;
				this.button2.Text = CommonStr.strMsgNo;
				this.button2.DialogResult = DialogResult.No;
				this.button2.Location = new Point(122, 3);
				if (base.Width < 180)
				{
					base.Width = 180;
					return;
				}
			}
			else if (buttons == XMessageBoxButtons.YesNoCancel)
			{
				this.button1.Visible = true;
				this.button1.DialogResult = DialogResult.Yes;
				this.button1.Text = CommonStr.strMsgYes;
				this.button2.Visible = true;
				this.button2.DialogResult = DialogResult.No;
				this.button2.Text = CommonStr.strMsgNo;
				this.button3.Visible = true;
				this.button3.DialogResult = DialogResult.Cancel;
				this.button3.Text = CommonStr.strMsgCancel;
				if (base.Width < 180)
				{
					base.Width = 180;
				}
			}
		}

		private void SetIcon(XMessageBoxIcon icon)
		{
			if (base.Height < 104)
			{
				base.Height = 104;
			}
			if (base.Width <= 600)
			{
				base.Width += 55;
			}
			this.leftPanel.Visible = true;
			if (icon == XMessageBoxIcon.Information)
			{
				this.pictureBox1.BackgroundImage = SystemIcons.Information.ToBitmap();
				return;
			}
			if (icon == XMessageBoxIcon.Warning)
			{
				this.pictureBox1.BackgroundImage = SystemIcons.Warning.ToBitmap();
				return;
			}
			if (icon == XMessageBoxIcon.Error)
			{
				this.pictureBox1.BackgroundImage = SystemIcons.Error.ToBitmap();
				return;
			}
			if (icon == XMessageBoxIcon.Question)
			{
				this.pictureBox1.BackgroundImage = SystemIcons.Question.ToBitmap();
				return;
			}
			if (icon == XMessageBoxIcon.Exclamation)
			{
				this.pictureBox1.BackgroundImage = SystemIcons.Exclamation.ToBitmap();
			}
		}

		private static DialogResult show(string text)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, "", xMessageBox.Size, xMessageBox.Font, xMessageBox.BackColor, xMessageBox.ForeColor, false);
				xMessageBox.SetButtons(XMessageBoxButtons.OK);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, xMessageBox.BackColor, xMessageBox.ForeColor, false);
				xMessageBox.SetButtons(XMessageBoxButtons.OK);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, XMessageBoxButtons buttons)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, xMessageBox.BackColor, xMessageBox.ForeColor, false);
				xMessageBox.SetButtons(buttons);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, XMessageBoxButtons buttons, XMessageBoxIcon icon)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, xMessageBox.BackColor, xMessageBox.ForeColor, false);
				xMessageBox.SetButtons(buttons);
				xMessageBox.SetIcon(icon);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, XMessageBoxButtons buttons, Size size)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, size, xMessageBox.Font, xMessageBox.BackColor, xMessageBox.ForeColor, true);
				xMessageBox.SetButtons(buttons);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, XMessageBoxButtons buttons, XMessageBoxIcon icon, Size size)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, size, xMessageBox.Font, xMessageBox.BackColor, xMessageBox.ForeColor, true);
				xMessageBox.SetButtons(buttons);
				xMessageBox.SetIcon(icon);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, XMessageBoxButtons buttons, Size size, Font font)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, size, font, xMessageBox.BackColor, xMessageBox.ForeColor, true);
				xMessageBox.SetButtons(buttons);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, XMessageBoxButtons buttons, XMessageBoxIcon icon, Size size, Font font)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, size, font, xMessageBox.BackColor, xMessageBox.ForeColor, true);
				xMessageBox.SetButtons(buttons);
				xMessageBox.SetIcon(icon);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		public static DialogResult Show(string text)
		{
			return XMessageBox.Show(null, text, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(string text, string title, MessageBoxButtons buttons)
		{
			return XMessageBox.Show(null, text, title, buttons, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(IWin32Window owner, string text, string title, MessageBoxButtons buttons)
		{
			return XMessageBox.Show(owner, text, title, buttons, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(string text, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return XMessageBox.Show(null, text, title, buttons, icon);
		}

		public static DialogResult Show(IWin32Window owner, string text, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				Color backColor = Color.FromArgb(147, 150, 177);
				Color white = Color.White;
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, backColor, white, false);
				if (buttons == MessageBoxButtons.OK)
				{
					xMessageBox.SetButtons(XMessageBoxButtons.OK);
				}
				if (buttons == MessageBoxButtons.OKCancel)
				{
					xMessageBox.SetButtons(XMessageBoxButtons.OKCancel);
				}
				if (icon == MessageBoxIcon.Exclamation)
				{
					xMessageBox.SetIcon(XMessageBoxIcon.Exclamation);
				}
				if (icon == MessageBoxIcon.Asterisk)
				{
					xMessageBox.SetIcon(XMessageBoxIcon.Information);
				}
				if (icon == MessageBoxIcon.Hand)
				{
					xMessageBox.SetIcon(XMessageBoxIcon.Error);
				}
				if (icon == MessageBoxIcon.Exclamation)
				{
					xMessageBox.SetIcon(XMessageBoxIcon.Warning);
				}
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, Color backColor, Color foreColor)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, backColor, foreColor, false);
				xMessageBox.SetButtons(XMessageBoxButtons.OK);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, Color backColor, Color foreColor, XMessageBoxButtons buttons)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, backColor, foreColor, false);
				xMessageBox.SetButtons(buttons);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, Color backColor, Color foreColor, XMessageBoxButtons buttons, XMessageBoxIcon icon)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, xMessageBox.Size, xMessageBox.Font, backColor, foreColor, false);
				xMessageBox.SetButtons(buttons);
				xMessageBox.SetIcon(icon);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, Color backColor, Color foreColor, XMessageBoxButtons buttons, Size size, Font font)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, size, font, backColor, foreColor, true);
				xMessageBox.SetButtons(buttons);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private static DialogResult show(string text, string title, Color backColor, Color foreColor, XMessageBoxButtons buttons, XMessageBoxIcon icon, Size size, Font font)
		{
			DialogResult result;
			using (XMessageBox xMessageBox = new XMessageBox())
			{
				xMessageBox.XmessageBox(text, title, size, font, backColor, foreColor, true);
				xMessageBox.SetButtons(buttons);
				xMessageBox.SetIcon(icon);
				result = xMessageBox.ShowDialog();
			}
			return result;
		}

		private void XMessageBox_Resize(object sender, EventArgs e)
		{
			int x = (this.bottomPanel.Width - this.bottomInnerPanel.Width) / 2;
			this.bottomInnerPanel.Location = new Point(x, this.bottomInnerPanel.Location.Y);
		}

		private void XMessageBox_ForeColorChanged(object sender, EventArgs e)
		{
			this.TextBox1.ForeColor = this.ForeColor;
		}

		private void XMessageBox_BackColorChanged(object sender, EventArgs e)
		{
			this.TextBox1.BackColor = this.BackColor;
		}

		private void XMessageBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 27)
			{
				base.DialogResult = DialogResult.Cancel;
				base.Close();
			}
		}

		private void XMessageBox_Load(object sender, EventArgs e)
		{
			Icon icon = base.Icon;
			wgAppConfig.GetAppIcon(ref icon);
			base.Icon = icon;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XMessageBox));
			this.bottomPanel = new Panel();
			this.bottomInnerPanel = new Panel();
			this.button2 = new Button();
			this.button1 = new Button();
			this.button3 = new Button();
			this.leftPanel = new Panel();
			this.pictureBox1 = new PictureBox();
			this.TextBox1 = new XTextBox();
			this.bottomPanel.SuspendLayout();
			this.bottomInnerPanel.SuspendLayout();
			this.leftPanel.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.bottomPanel.Controls.Add(this.bottomInnerPanel);
			componentResourceManager.ApplyResources(this.bottomPanel, "bottomPanel");
			this.bottomPanel.Name = "bottomPanel";
			this.bottomInnerPanel.Controls.Add(this.button2);
			this.bottomInnerPanel.Controls.Add(this.button1);
			this.bottomInnerPanel.Controls.Add(this.button3);
			componentResourceManager.ApplyResources(this.bottomInnerPanel, "bottomInnerPanel");
			this.bottomInnerPanel.Name = "bottomInnerPanel";
			this.button2.BackColor = Color.Transparent;
			this.button2.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.button2, "button2");
			this.button2.ForeColor = Color.White;
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = false;
			this.button1.BackColor = Color.Transparent;
			this.button1.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.button1, "button1");
			this.button1.ForeColor = Color.White;
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = false;
			this.button3.BackColor = Color.Transparent;
			this.button3.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.button3, "button3");
			this.button3.ForeColor = Color.White;
			this.button3.Name = "button3";
			this.button3.UseVisualStyleBackColor = false;
			this.leftPanel.Controls.Add(this.pictureBox1);
			componentResourceManager.ApplyResources(this.leftPanel, "leftPanel");
			this.leftPanel.Name = "leftPanel";
			componentResourceManager.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			this.TextBox1.BackColor = SystemColors.Control;
			this.TextBox1.BorderStyle = BorderStyle.None;
			this.TextBox1.Cursor = Cursors.IBeam;
			componentResourceManager.ApplyResources(this.TextBox1, "TextBox1");
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.ReadOnly = true;
			this.TextBox1.TabStop = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ControlBox = false;
			base.Controls.Add(this.TextBox1);
			base.Controls.Add(this.leftPanel);
			base.Controls.Add(this.bottomPanel);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "XMessageBox";
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.Load += new EventHandler(this.XMessageBox_Load);
			base.BackColorChanged += new EventHandler(this.XMessageBox_BackColorChanged);
			base.ForeColorChanged += new EventHandler(this.XMessageBox_ForeColorChanged);
			base.KeyDown += new KeyEventHandler(this.XMessageBox_KeyDown);
			base.Resize += new EventHandler(this.XMessageBox_Resize);
			this.bottomPanel.ResumeLayout(false);
			this.bottomInnerPanel.ResumeLayout(false);
			this.leftPanel.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
