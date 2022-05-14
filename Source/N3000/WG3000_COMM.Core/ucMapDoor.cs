using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Core
{
	public class ucMapDoor : UserControl
	{
		private const int FONTSIZE = 9;

		private const int FONT_HEIGHT = 16;

		private const int IMG_WIDTH = 24;

		private const int IMG_HEIGHT = 32;

		private const int CONTROL_HEIGHT = 50;

		public int idoorWarnSource;

		private IContainer components;

		public PictureBox picDoorState;

		public ImageList imgDoor;

		internal Label txtDoorName;

		internal Timer Timer1;

		private int m_doorStatus;

		private PictureBox m_bindSource;

		private Point m_doorLocation;

		private string m_doorName = "门名称";

		private float m_mapScale = 1f;

		private float m_doorScale = 1f;

		public int doorStatus
		{
			get
			{
				return this.m_doorStatus;
			}
			set
			{
				if (this.m_doorStatus != value)
				{
					this.m_doorStatus = 0;
					this.m_doorStatus = value;
					switch (this.m_doorStatus)
					{
					case 0:
						this.picDoorState.Image = Resources.pConsole_Door_Unknown;
						break;
					case 1:
						this.picDoorState.Image = Resources.pConsole_Door_NormalClose;
						break;
					case 2:
						this.picDoorState.Image = Resources.pConsole_Door_NormalOpen;
						break;
					case 3:
						this.picDoorState.Image = Resources.pConsole_Door_NotConnected;
						break;
					case 4:
						this.picDoorState.Image = Resources.pConsole_Door_WarnClose;
						break;
					case 5:
						this.picDoorState.Image = Resources.pConsole_Door_WarnOpen;
						break;
					case 6:
						this.picDoorState.Image = Resources.pConsole_Door_Unknown;
						break;
					case 7:
						this.picDoorState.Image = Resources.pConsole_Door_WarnClose;
						break;
					case 8:
						this.picDoorState.Image = Resources.pConsole_Door_WarnOpen;
						break;
					case 9:
						this.picDoorState.Image = Resources.pConsole_Door_NotConnected;
						break;
					default:
						this.picDoorState.Image = Resources.pConsole_Door_Unknown;
						break;
					}
					if (this.m_doorStatus == 4 || this.m_doorStatus == 5)
					{
						this.Timer1.Enabled = true;
						return;
					}
					this.Timer1.Enabled = false;
					this.picDoorState.Visible = true;
				}
			}
		}

		public PictureBox bindSource
		{
			get
			{
				return this.m_bindSource;
			}
			set
			{
				this.m_bindSource = value;
			}
		}

		public Point doorLocation
		{
			get
			{
				return this.m_doorLocation;
			}
			set
			{
				this.m_doorLocation = new Point((int)((float)value.X / this.mapScale), (int)((float)value.Y / this.mapScale));
			}
		}

		public string doorName
		{
			get
			{
				return this.m_doorName;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (this.m_doorName != value)
				{
					this.m_doorName = value;
					this.txtDoorName.Text = this.m_doorName;
					this.redraw();
				}
			}
		}

		public float mapScale
		{
			get
			{
				return this.m_mapScale;
			}
			set
			{
				if (this.m_mapScale != value)
				{
					this.m_mapScale = value;
					this.redraw();
				}
			}
		}

		public float doorScale
		{
			get
			{
				return this.m_doorScale;
			}
			set
			{
				if (this.m_doorScale != value)
				{
					this.m_doorScale = value;
					this.redraw();
				}
			}
		}

		public ucMapDoor()
		{
			this.InitializeComponent();
			this.txtDoorName.Text = this.m_doorName;
			this.doorLocation = base.Location;
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
			this.components = new Container();
			this.picDoorState = new PictureBox();
			this.imgDoor = new ImageList(this.components);
			this.txtDoorName = new Label();
			this.Timer1 = new Timer(this.components);
			((ISupportInitialize)this.picDoorState).BeginInit();
			base.SuspendLayout();
			this.picDoorState.Enabled = false;
			this.picDoorState.Image = Resources.pConsole_Door_Unknown;
			this.picDoorState.Location = new Point(0, 0);
			this.picDoorState.Name = "picDoorState";
			this.picDoorState.Size = new Size(24, 32);
			this.picDoorState.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picDoorState.TabIndex = 0;
			this.picDoorState.TabStop = false;
			this.picDoorState.Leave += new EventHandler(this.ucMapDoor_Leave);
			this.picDoorState.MouseDown += new MouseEventHandler(this.picDoorState_MouseDown);
			this.imgDoor.ColorDepth = ColorDepth.Depth16Bit;
			this.imgDoor.ImageSize = new Size(24, 32);
			this.imgDoor.TransparentColor = Color.Transparent;
			this.txtDoorName.AutoSize = true;
			this.txtDoorName.BackColor = Color.White;
			this.txtDoorName.Location = new Point(-2, 36);
			this.txtDoorName.Name = "txtDoorName";
			this.txtDoorName.Size = new Size(29, 12);
			this.txtDoorName.TabIndex = 1;
			this.txtDoorName.Text = "Name";
			this.txtDoorName.TextAlign = ContentAlignment.TopCenter;
			this.txtDoorName.Click += new EventHandler(this.ucMapDoor_Click);
			this.Timer1.Interval = 500;
			this.Timer1.Tick += new EventHandler(this.Timer1_Tick);
			this.AllowDrop = true;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.txtDoorName);
			base.Controls.Add(this.picDoorState);
			this.ForeColor = Color.Black;
			base.Name = "ucMapDoor";
			base.Size = new Size(32, 50);
			base.Load += new EventHandler(this.ucMapDoor_Load);
			base.Click += new EventHandler(this.ucMapDoor_Click);
			base.Leave += new EventHandler(this.ucMapDoor_Leave);
			((ISupportInitialize)this.picDoorState).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void redraw()
		{
			try
			{
				this.picDoorState.Size = new Size(new Point((int)(24f * this.m_mapScale * this.m_doorScale), (int)(32f * this.m_mapScale * this.m_doorScale)));
				this.txtDoorName.Text = this.m_doorName;
				this.txtDoorName.Font = new Font("Arial", 9f * this.m_mapScale);
				this.txtDoorName.Location = new Point(0, this.picDoorState.Location.Y + this.picDoorState.Size.Height + 2);
				base.Location = new Point((int)((float)this.m_doorLocation.X * this.m_mapScale), (int)((float)this.m_doorLocation.Y * this.m_mapScale));
				base.Size = new Size(new Point(Math.Max(this.txtDoorName.Width, this.picDoorState.Size.Width), (int)((32f * this.doorScale + 16f) * this.m_mapScale)));
				this.picDoorState.Location = new Point((base.Size.Width - this.picDoorState.Width) / 2, 0);
			}
			catch (Exception)
			{
			}
		}

		private void ucMapDoor_Click(object sender, EventArgs e)
		{
			this.txtDoorName.ForeColor = Color.White;
			this.txtDoorName.BackColor = Color.DodgerBlue;
			base.ActiveControl = this.txtDoorName;
		}

		private void ucMapDoor_Leave(object sender, EventArgs e)
		{
			this.txtDoorName.ForeColor = Color.Black;
			this.txtDoorName.BackColor = Color.White;
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			this.picDoorState.Visible = !this.picDoorState.Visible;
		}

		private void ucMapDoor_Load(object sender, EventArgs e)
		{
			this.redraw();
		}

		private void picDoorState_MouseDown(object sender, MouseEventArgs e)
		{
		}
	}
}
