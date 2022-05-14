using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.Map
{
	public class frmMaps : frmN3000
	{
		public ListView lstDoors = new ListView();

		public ContextMenuStrip contextMenuStrip1Doors;

		public ToolStripButton btnMonitor;

		public ToolStripButton btnStop;

		private string strcmdWatchCurrentMapBk = "";

		private string strcmdWatchAllMapsBk = "";

		private DataView dvDoors;

		private DataView dvDoors4Watching;

		private DataView dvSelected;

		private ImageList imgDoor2;

		private Dictionary<string, string> ReaderName = new Dictionary<string, string>();

		private DataTable dtReader;

		private TabPage mapPage;

		private Panel mapPanel;

		private PictureBox mapPicture;

		private ucMapDoor uc1door;

		private DataTable dt;

		private DataView dvMapDoors;

		private DataView dvMap;

		private DataSet dstemp;

		private SqlDataAdapter da;

		private SqlConnection cn;

		private MemoryStream photoMemoryStream;

		private byte[] photoImageData;

		private int t;

		private int l;

		private ucMapDoor currentUcMapDoor;

		private ArrayList arrZoomScale = new ArrayList();

		private ArrayList arrZoomScaleTabpageName = new ArrayList();

		private bool bEditing;

		private DataView dvMapDoor;

		private Point lastMouseP;

		private IContainer components;

		private ToolStrip C1ToolBar4MapOperate;

		private ToolStripButton cmdCloseMaps;

		private ToolStripButton cmdZoomIn;

		private ToolStripButton cmdZoomOut;

		private ToolStripButton cmdEditMap;

		private ToolStripButton cmdWatchCurrentMap;

		private ToolStripButton cmdWatchAllMaps;

		private TabControl c1tabMaps;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private ContextMenuStrip C1CmnuMap;

		private ContextMenuStrip C1CmnuDoor;

		private ToolStripMenuItem cmdAddDoorByLoc;

		private ToolStrip C1ToolBar4MapEdit;

		private ToolStripButton cmdAddMap;

		private ToolStripButton cmdDeleteMap;

		private ToolStripButton cmdChangeMapName;

		private ToolStripButton cmdAddDoor;

		private ToolStripButton cmdDeleteDoor;

		private ToolStripButton cmdSaveMap;

		private ToolStripButton cmdCancelAndExit;

		private Timer Timer2;

		private ToolStripMenuItem openDoorToolStripMenuItem;

		private ToolStripButton btnStopOthers;

		public frmMaps()
		{
			this.InitializeComponent();
		}

		private void frmMaps_Load(object sender, EventArgs e)
		{
			try
			{
				this.C1ToolBar4MapEdit.Visible = false;
				this.cmdAddDoorByLoc.Visible = false;
				this.bEditing = false;
				this.c1tabMaps.TabPages.Clear();
				this.loadDoorData();
				this.strcmdWatchCurrentMapBk = this.cmdWatchCurrentMap.Text;
				this.strcmdWatchAllMapsBk = this.cmdWatchAllMaps.Text;
				this.loadmapFromDB();
				bool flag = false;
				string funName = "btnMaps";
				if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
				{
					this.cmdEditMap.Visible = false;
				}
				this.Timer2.Enabled = true;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			this.dt = new DataTable();
			this.dvDoors = new DataView(this.dt);
			this.dvDoors4Watching = new DataView(this.dt);
			this.dvSelected = new DataView(this.dt);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dt);
						}
					}
					goto IL_105;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dt);
					}
				}
			}
			IL_105:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dt);
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					this.dt.Columns[0]
				};
				this.dt.PrimaryKey = primaryKey;
			}
			catch (Exception)
			{
				throw;
			}
			this.imgDoor2 = new ImageList();
			this.imgDoor2.ImageSize = new Size(24, 32);
			this.imgDoor2.TransparentColor = SystemColors.Window;
			string systemParamByNO = wgAppConfig.getSystemParamByNO(22);
			if (!string.IsNullOrEmpty(systemParamByNO))
			{
				decimal num = decimal.Parse(systemParamByNO, CultureInfo.InvariantCulture);
				if (num != 1m && num > 0m && num < 100m)
				{
					this.imgDoor2.ImageSize = new Size((int)(24m * num), (int)(32m * num));
				}
			}
			text = " SELECT a.f_ReaderNO, a.f_ReaderName , b.f_ControllerSN ";
			text += " FROM t_b_Reader a, t_b_Controller b WHERE  b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			this.dtReader = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection2 = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand2 = new OleDbCommand(text, oleDbConnection2))
					{
						using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(oleDbCommand2))
						{
							oleDbDataAdapter2.Fill(this.dtReader);
						}
					}
					goto IL_2D5;
				}
			}
			using (SqlConnection sqlConnection2 = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand2 = new SqlCommand(text, sqlConnection2))
				{
					using (SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2))
					{
						sqlDataAdapter2.Fill(this.dtReader);
					}
				}
			}
			IL_2D5:
			if (this.dtReader.Rows.Count > 0)
			{
				for (int i = 0; i < this.dtReader.Rows.Count; i++)
				{
					this.ReaderName.Add(string.Format("{0}-{1}", this.dtReader.Rows[i]["f_ControllerSN"].ToString(), this.dtReader.Rows[i]["f_ReaderNO"].ToString()), this.dtReader.Rows[i]["f_ReaderName"].ToString());
				}
			}
		}

		private void loadmapFromDB()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadmapFromDB_Acc();
				return;
			}
			this.c1tabMaps.Visible = false;
			float doorScale = 1f;
			float.TryParse(wgAppConfig.getSystemParamByNO(22), out doorScale);
			try
			{
				this.dstemp = new DataSet("mapInfo");
				this.cn = new SqlConnection(wgAppConfig.dbConString);
				string text = "SELECT * FROM  t_d_maps ORDER BY f_MapPageIndex";
				this.da = new SqlDataAdapter(text, this.cn);
				this.da.Fill(this.dstemp, "t_d_maps");
				this.dvMap = new DataView(this.dstemp.Tables["t_d_maps"]);
				string text2 = "";
				text = " SELECT t_d_mapdoors.*, t_d_maps.f_MapName, t_d_maps.f_MapPageIndex, t_b_Door.f_DoorName, t_b_Controller.f_ZoneID ";
				if (text2 == "")
				{
					text += " FROM ((t_d_mapdoors INNER JOIN t_d_maps ON t_d_mapdoors.f_MapId = t_d_maps.f_MapId) INNER JOIN t_b_Door ON t_d_mapdoors.f_DoorID = t_b_Door.f_DoorID) INNER JOIN t_b_Controller ON t_b_Door.f_ControllerID = t_b_Controller.f_ControllerID ";
				}
				else
				{
					text += " FROM ((t_d_mapdoors INNER JOIN t_d_maps ON t_d_mapdoors.f_MapId = t_d_maps.f_MapId) INNER JOIN t_b_Door ON t_d_mapdoors.f_DoorID = t_b_Door.f_DoorID) INNER JOIN t_b_Controller ON t_b_Door.f_ControllerID = t_b_Controller.f_ControllerID ";
					text = text + " WHERE t_b_door.f_DoorID IN " + text2;
				}
				this.da = new SqlDataAdapter(text, this.cn);
				this.da.Fill(this.dstemp, "v_d_mapdoors");
				this.dt = this.dstemp.Tables["v_d_mapdoors"];
				icControllerZone icControllerZone = new icControllerZone();
				icControllerZone.getAllowedControllers(ref this.dt);
				this.dvMapDoors = new DataView(this.dstemp.Tables["v_d_mapdoors"]);
				this.c1tabMaps.TabPages.Clear();
				if (this.dvMap.Count > 0)
				{
					this.cmdZoomIn.Enabled = true;
					this.cmdZoomOut.Enabled = true;
					this.cmdWatchCurrentMap.Enabled = true;
					this.cmdWatchAllMaps.Enabled = true;
					for (int i = 0; i <= this.dvMap.Count - 1; i++)
					{
						this.mapPage = new TabPage();
						this.mapPage.Text = this.dvMap[i]["f_MapName"].ToString();
						this.mapPage.Tag = this.dvMap[i]["f_MapFile"];
						this.c1tabMaps.TabPages.Add(this.mapPage);
						this.c1tabMaps.SelectedTab = this.mapPage;
						this.mapPanel = new Panel();
						this.mapPicture = new PictureBox();
						this.mapPanel.Dock = DockStyle.Fill;
						this.mapPanel.BackColor = Color.White;
						this.mapPanel.AutoScroll = true;
						this.mapPicture.SizeMode = PictureBoxSizeMode.AutoSize;
						this.ShowMap(this.mapPage.Tag.ToString(), this.mapPicture);
						this.mapPicture.SizeMode = PictureBoxSizeMode.StretchImage;
						this.mapPicture.ContextMenuStrip = this.C1CmnuMap;
						this.mapPicture.MouseDown += new MouseEventHandler(this.mapPicture_MouseDown);
						this.mapPanel.Controls.Add(this.mapPicture);
						this.mapPage.Controls.Add(this.mapPanel);
						this.dvMapDoors.RowFilter = " f_MapID= " + this.dvMap[i]["f_MapID"];
						for (int j = 0; j <= this.dvMapDoors.Count - 1; j++)
						{
							this.uc1door = new ucMapDoor();
							this.uc1door.doorName = this.dvMapDoors[j]["f_DoorName"].ToString();
							this.uc1door.doorScale = doorScale;
							this.uc1door.bindSource = this.mapPicture;
							this.uc1door.doorLocation = new Point(int.Parse(this.dvMapDoors[j]["f_DoorLocationX"].ToString()), int.Parse(this.dvMapDoors[j]["f_DoorLocationY"].ToString()));
							this.uc1door.MouseDown += new MouseEventHandler(this.UcMapDoor_MouseDown);
							this.uc1door.MouseMove += new MouseEventHandler(this.UcMapDoor_MouseMove);
							this.uc1door.MouseUp += new MouseEventHandler(this.UcMapDoor_MouseUp);
							this.uc1door.Click += new EventHandler(this.ucMapDoor_Click);
							this.uc1door.imgDoor = this.imgDoor2;
							this.uc1door.ContextMenuStrip = this.contextMenuStrip1Doors;
							this.uc1door.picDoorState.ContextMenuStrip = this.C1CmnuDoor;
							this.mapPicture.Controls.Add(this.uc1door);
						}
					}
				}
				else
				{
					this.cmdZoomIn.Enabled = false;
					this.cmdZoomOut.Enabled = false;
					this.cmdWatchCurrentMap.Enabled = false;
					this.cmdWatchAllMaps.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			this.loadEmapInfoLocation();
			this.c1tabMaps.Visible = true;
		}

		private void loadmapFromDB_Acc()
		{
			this.c1tabMaps.Visible = false;
			float doorScale = 1f;
			float.TryParse(wgAppConfig.getSystemParamByNO(22), out doorScale);
			try
			{
				this.dstemp = new DataSet("mapInfo");
				OleDbConnection selectConnection = new OleDbConnection(wgAppConfig.dbConString);
				string text = "SELECT * FROM  t_d_maps ORDER BY f_MapPageIndex";
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(text, selectConnection);
				oleDbDataAdapter.Fill(this.dstemp, "t_d_maps");
				this.dvMap = new DataView(this.dstemp.Tables["t_d_maps"]);
				string text2 = "";
				text = " SELECT t_d_mapdoors.*, t_d_maps.f_MapName, t_d_maps.f_MapPageIndex, t_b_Door.f_DoorName, t_b_Controller.f_ZoneID ";
				if (text2 == "")
				{
					text += " FROM ((t_d_mapdoors INNER JOIN t_d_maps ON t_d_mapdoors.f_MapId = t_d_maps.f_MapId) INNER JOIN t_b_Door ON t_d_mapdoors.f_DoorID = t_b_Door.f_DoorID) INNER JOIN t_b_Controller ON t_b_Door.f_ControllerID = t_b_Controller.f_ControllerID ";
				}
				else
				{
					text += " FROM ((t_d_mapdoors INNER JOIN t_d_maps ON t_d_mapdoors.f_MapId = t_d_maps.f_MapId) INNER JOIN t_b_Door ON t_d_mapdoors.f_DoorID = t_b_Door.f_DoorID) INNER JOIN t_b_Controller ON t_b_Door.f_ControllerID = t_b_Controller.f_ControllerID ";
					text = text + " WHERE t_b_door.f_DoorID IN " + text2;
				}
				oleDbDataAdapter = new OleDbDataAdapter(text, selectConnection);
				oleDbDataAdapter.Fill(this.dstemp, "v_d_mapdoors");
				this.dt = this.dstemp.Tables["v_d_mapdoors"];
				icControllerZone icControllerZone = new icControllerZone();
				icControllerZone.getAllowedControllers(ref this.dt);
				this.dvMapDoors = new DataView(this.dstemp.Tables["v_d_mapdoors"]);
				this.c1tabMaps.TabPages.Clear();
				if (this.dvMap.Count > 0)
				{
					this.cmdZoomIn.Enabled = true;
					this.cmdZoomOut.Enabled = true;
					this.cmdWatchCurrentMap.Enabled = true;
					this.cmdWatchAllMaps.Enabled = true;
					for (int i = 0; i <= this.dvMap.Count - 1; i++)
					{
						this.mapPage = new TabPage();
						this.mapPage.Text = this.dvMap[i]["f_MapName"].ToString();
						this.mapPage.Tag = this.dvMap[i]["f_MapFile"];
						this.c1tabMaps.TabPages.Add(this.mapPage);
						this.c1tabMaps.SelectedTab = this.mapPage;
						this.mapPanel = new Panel();
						this.mapPicture = new PictureBox();
						this.mapPanel.Dock = DockStyle.Fill;
						this.mapPanel.BackColor = Color.White;
						this.mapPanel.AutoScroll = true;
						this.mapPicture.SizeMode = PictureBoxSizeMode.AutoSize;
						this.ShowMap(this.mapPage.Tag.ToString(), this.mapPicture);
						this.mapPicture.SizeMode = PictureBoxSizeMode.StretchImage;
						this.mapPicture.ContextMenuStrip = this.C1CmnuMap;
						this.mapPicture.MouseDown += new MouseEventHandler(this.mapPicture_MouseDown);
						this.mapPanel.Controls.Add(this.mapPicture);
						this.mapPage.Controls.Add(this.mapPanel);
						this.dvMapDoors.RowFilter = " f_MapID= " + this.dvMap[i]["f_MapID"];
						for (int j = 0; j <= this.dvMapDoors.Count - 1; j++)
						{
							this.uc1door = new ucMapDoor();
							this.uc1door.doorName = this.dvMapDoors[j]["f_DoorName"].ToString();
							this.uc1door.doorScale = doorScale;
							this.uc1door.bindSource = this.mapPicture;
							this.uc1door.doorLocation = new Point(int.Parse(this.dvMapDoors[j]["f_DoorLocationX"].ToString()), int.Parse(this.dvMapDoors[j]["f_DoorLocationY"].ToString()));
							this.uc1door.MouseDown += new MouseEventHandler(this.UcMapDoor_MouseDown);
							this.uc1door.MouseMove += new MouseEventHandler(this.UcMapDoor_MouseMove);
							this.uc1door.MouseUp += new MouseEventHandler(this.UcMapDoor_MouseUp);
							this.uc1door.Click += new EventHandler(this.ucMapDoor_Click);
							this.uc1door.imgDoor = this.imgDoor2;
							this.uc1door.ContextMenuStrip = this.contextMenuStrip1Doors;
							this.uc1door.picDoorState.ContextMenuStrip = this.C1CmnuDoor;
							this.mapPicture.Controls.Add(this.uc1door);
						}
					}
				}
				else
				{
					this.cmdZoomIn.Enabled = false;
					this.cmdZoomOut.Enabled = false;
					this.cmdWatchCurrentMap.Enabled = false;
					this.cmdWatchAllMaps.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			this.loadEmapInfoLocation();
			this.c1tabMaps.Visible = true;
		}

		private void UcMapDoor_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				PictureBox bindSource = ((ucMapDoor)sender).bindSource;
				this.t = bindSource.PointToClient(Control.MousePosition).Y - (sender as Control).Top;
				this.l = bindSource.PointToClient(Control.MousePosition).X - (sender as Control).Left;
				ucMapDoor ucMapDoor = (ucMapDoor)sender;
				this.currentUcMapDoor = ucMapDoor;
				for (int i = 0; i <= this.lstDoors.Items.Count - 1; i++)
				{
					if (this.lstDoors.Items[i].Text == ucMapDoor.doorName)
					{
						this.lstDoors.SelectedItems.Clear();
						this.lstDoors.Items[i].Selected = true;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void UcMapDoor_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && this.cmdAddMap.Visible)
			{
				try
				{
					PictureBox bindSource = ((ucMapDoor)sender).bindSource;
					int top = bindSource.PointToClient(Control.MousePosition).Y - this.t;
					int left = bindSource.PointToClient(Control.MousePosition).X - this.l;
					(sender as Control).Top = top;
					(sender as Control).Left = left;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
		}

		private void UcMapDoor_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && this.cmdAddMap.Visible)
			{
				try
				{
					((ucMapDoor)sender).doorLocation = ((ucMapDoor)sender).Location;
				}
				catch (Exception ex)
				{
					wgTools.WgDebugWrite(ex.ToString(), new object[0]);
				}
			}
		}

		public PictureBox getPicture(TabPage tabpage)
		{
			PictureBox result = null;
			try
			{
				foreach (object current in tabpage.Controls)
				{
					if (current is Panel)
					{
						foreach (object current2 in ((Panel)current).Controls)
						{
							if (current2 is PictureBox)
							{
								result = (PictureBox)current2;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			return result;
		}

		private void mapZoom(float zoomScale)
		{
			try
			{
				if (this.c1tabMaps.SelectedTab != null)
				{
					PictureBox picture = this.getPicture(this.c1tabMaps.SelectedTab);
					if (picture != null)
					{
						float num = 1f;
						int num2 = this.arrZoomScaleTabpageName.IndexOf(this.c1tabMaps.SelectedTab.Text);
						if (num2 < 0)
						{
							this.arrZoomScaleTabpageName.Add(this.c1tabMaps.SelectedTab.Text);
							this.arrZoomScale.Add(1.0);
						}
						num2 = this.arrZoomScaleTabpageName.IndexOf(this.c1tabMaps.SelectedTab.Text);
						if (num2 >= 0)
						{
							float.TryParse(this.arrZoomScale[num2].ToString(), out num);
							if ((float)picture.Width * zoomScale >= 10f && (float)picture.Width * zoomScale <= 10000f && (float)picture.Height * zoomScale >= 10f && (float)picture.Height * zoomScale <= 10000f)
							{
								num *= zoomScale;
								this.arrZoomScale[num2] = num;
								picture.Size = new Size(new Point((int)((float)picture.Width * zoomScale), (int)((float)picture.Height * zoomScale)));
								foreach (object current in picture.Controls)
								{
									if (current is ucMapDoor)
									{
										((ucMapDoor)current).mapScale = ((ucMapDoor)current).mapScale * zoomScale;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public void ShowMap(string fileToDisplay, PictureBox obj)
		{
			try
			{
				obj.Visible = false;
				if (fileToDisplay != null)
				{
					FileInfo fileInfo = new FileInfo(wgAppConfig.Path4PhotoDefault() + fileToDisplay);
					if (fileInfo.Exists)
					{
						using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
						{
							this.photoImageData = new byte[fileStream.Length + 1L];
							fileStream.Read(this.photoImageData, 0, (int)fileStream.Length);
						}
						if (this.photoMemoryStream != null)
						{
							try
							{
								this.photoMemoryStream.Close();
							}
							catch (Exception)
							{
							}
							this.photoMemoryStream = null;
						}
						this.photoMemoryStream = new MemoryStream(this.photoImageData);
						try
						{
							if (obj.Image != null)
							{
								obj.Image.Dispose();
							}
						}
						catch (Exception)
						{
						}
						obj.Image = Image.FromStream(this.photoMemoryStream);
						obj.Visible = true;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public void loadEmapInfoLocation()
		{
			try
			{
				string keyVal = wgAppConfig.GetKeyVal("EMapLocInfo");
				string keyVal2 = wgAppConfig.GetKeyVal("EMapZoomInfo");
				if (!(keyVal == "") && !(keyVal2 == ""))
				{
					string[] array = keyVal.Split(new char[]
					{
						','
					});
					string[] array2 = keyVal2.Split(new char[]
					{
						','
					});
					if (array.Length * 2 == array2.Length * 3)
					{
						for (int i = 0; i <= array.Length / 3 - 1; i++)
						{
							if (array[i * 3] != array2[i * 2])
							{
								return;
							}
						}
						foreach (TabPage tabPage in this.c1tabMaps.TabPages)
						{
							for (int i = 0; i <= array.Length / 3 - 1; i++)
							{
								if (array[i * 3] == tabPage.Text)
								{
									this.c1tabMaps.SelectedTab = tabPage;
									this.mapZoom(float.Parse(array2[i * 2 + 1]));
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		public void saveEmapInfoLocation()
		{
			try
			{
				string text = "";
				string text2 = "";
				foreach (TabPage tabPage in this.c1tabMaps.TabPages)
				{
					int num = this.arrZoomScaleTabpageName.IndexOf(tabPage.Text);
					float num2 = 1f;
					if (num >= 0)
					{
						num2 = (float)this.arrZoomScale[num];
					}
					if (text2 != "")
					{
						text2 += ",";
					}
					object obj = text2;
					text2 = string.Concat(new object[]
					{
						obj,
						tabPage.Text,
						",",
						num2
					});
					PictureBox picture = this.getPicture(tabPage);
					if (text != "")
					{
						text += ",";
					}
					if (picture != null)
					{
						object obj2 = text;
						text = string.Concat(new object[]
						{
							obj2,
							tabPage.Text,
							",",
							picture.Location.X,
							",",
							picture.Location.Y
						});
					}
					else
					{
						text = text + tabPage.Text + ",0,0";
					}
				}
				wgAppConfig.UpdateKeyVal("EMapZoomInfo", text2);
				wgAppConfig.UpdateKeyVal("EMapLocInfo", text);
				this.arrZoomScaleTabpageName.Clear();
				this.arrZoomScale.Clear();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void Timer2_Tick(object sender, EventArgs e)
		{
			if (this.bEditing)
			{
				return;
			}
			this.Timer2.Enabled = false;
			try
			{
				bool flag = false;
				for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
				{
					foreach (object current in this.c1tabMaps.TabPages[i].Controls)
					{
						if (current is Panel)
						{
							foreach (object current2 in ((Panel)current).Controls)
							{
								if (current2 is PictureBox)
								{
									foreach (ucMapDoor ucMapDoor in ((PictureBox)current2).Controls)
									{
										for (int j = 0; j <= this.lstDoors.Items.Count - 1; j++)
										{
											if (this.lstDoors.Items[j].Text == ucMapDoor.doorName && this.lstDoors.Items[j].ImageIndex != ucMapDoor.doorStatus)
											{
												ucMapDoor.doorStatus = this.lstDoors.Items[j].ImageIndex;
												if (!flag)
												{
													if (ucMapDoor.doorStatus >= 4)
													{
														this.c1tabMaps.SelectedTab = this.c1tabMaps.TabPages[i];
													}
													flag = true;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
			this.Timer2.Enabled = true;
		}

		private void cmdEditMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.C1ToolBar4MapEdit.Visible = true;
				this.bEditing = true;
				this.C1ToolBar4MapOperate.Visible = false;
				this.cmdAddDoorByLoc.Visible = true;
				this.Timer2.Enabled = false;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdCloseMaps_Click(object sender, EventArgs e)
		{
			try
			{
				this.saveEmapInfoLocation();
				for (int i = 0; i < this.c1tabMaps.TabPages.Count; i++)
				{
					foreach (object current in this.c1tabMaps.TabPages[i].Controls)
					{
						if (current is Panel)
						{
							foreach (object current2 in ((Panel)current).Controls)
							{
								if (current2 is PictureBox)
								{
									wgAppConfig.DisposeImage((current2 as PictureBox).Image);
								}
							}
						}
					}
					wgAppConfig.DisposeImage(this.c1tabMaps.TabPages[i].BackgroundImage);
				}
				base.Close();
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdZoomIn_Click(object sender, EventArgs e)
		{
			this.mapZoom(1.25f);
		}

		private void cmdZoomOut_Click(object sender, EventArgs e)
		{
			this.mapZoom(0.8f);
		}

		private void cmdWatchCurrentMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.lstDoors.SelectedItems.Clear();
				int tabIndex = this.c1tabMaps.SelectedTab.TabIndex;
				foreach (object current in this.c1tabMaps.TabPages[tabIndex].Controls)
				{
					if (current is Panel)
					{
						foreach (object current2 in ((Panel)current).Controls)
						{
							if (current2 is PictureBox)
							{
								foreach (ucMapDoor ucMapDoor in ((PictureBox)current2).Controls)
								{
									for (int i = 0; i <= this.lstDoors.Items.Count - 1; i++)
									{
										if (this.lstDoors.Items[i].Text == ucMapDoor.doorName)
										{
											this.lstDoors.Items[i].Selected = true;
										}
									}
								}
							}
						}
					}
				}
				if (this.btnMonitor != null)
				{
					this.btnMonitor.PerformClick();
					this.cmdWatchCurrentMap.Text = this.strcmdWatchCurrentMapBk;
					this.cmdWatchAllMaps.Text = this.strcmdWatchAllMapsBk;
					this.cmdWatchCurrentMap.BackColor = Color.Transparent;
					this.cmdWatchAllMaps.BackColor = Color.Transparent;
					if (this.lstDoors.SelectedItems.Count > 0)
					{
						(sender as ToolStripButton).BackColor = Color.Green;
						this.btnStopOthers.BackColor = Color.Red;
						(sender as ToolStripButton).Text = CommonStr.strMonitoring;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdWatchAllMaps_Click(object sender, EventArgs e)
		{
			try
			{
				this.lstDoors.SelectedItems.Clear();
				for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
				{
					foreach (object current in this.c1tabMaps.TabPages[i].Controls)
					{
						if (current is Panel)
						{
							foreach (object current2 in ((Panel)current).Controls)
							{
								if (current2 is PictureBox)
								{
									foreach (ucMapDoor ucMapDoor in ((PictureBox)current2).Controls)
									{
										for (int j = 0; j <= this.lstDoors.Items.Count - 1; j++)
										{
											if (this.lstDoors.Items[j].Text == ucMapDoor.doorName)
											{
												this.lstDoors.Items[j].Selected = true;
											}
										}
									}
								}
							}
						}
					}
				}
				if (this.btnMonitor != null)
				{
					this.btnMonitor.PerformClick();
					this.cmdWatchCurrentMap.Text = this.strcmdWatchCurrentMapBk;
					this.cmdWatchAllMaps.Text = this.strcmdWatchAllMapsBk;
					this.cmdWatchCurrentMap.BackColor = Color.Transparent;
					this.cmdWatchAllMaps.BackColor = Color.Transparent;
					if (this.lstDoors.SelectedItems.Count > 0)
					{
						(sender as ToolStripButton).BackColor = Color.Green;
						this.btnStopOthers.BackColor = Color.Red;
						(sender as ToolStripButton).Text = CommonStr.strMonitoring;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdAddMap_Click(object sender, EventArgs e)
		{
			try
			{
				using (dfrmMapInfo dfrmMapInfo = new dfrmMapInfo())
				{
					if (dfrmMapInfo.ShowDialog(this) == DialogResult.OK)
					{
						string mapName = dfrmMapInfo.mapName;
						string mapFile = dfrmMapInfo.mapFile;
						for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
						{
							if (this.c1tabMaps.TabPages[i].Text == mapName)
							{
								XMessageBox.Show(CommonStr.strMapNameDuplicated);
								return;
							}
						}
						this.mapPage = new TabPage();
						this.mapPage.Text = mapName;
						this.mapPage.Tag = mapFile;
						this.c1tabMaps.TabPages.Add(this.mapPage);
						this.c1tabMaps.SelectedTab = this.mapPage;
						this.mapPanel = new Panel();
						this.mapPicture = new PictureBox();
						this.mapPanel.Dock = DockStyle.Fill;
						this.mapPanel.BackColor = Color.White;
						this.mapPanel.AutoScroll = true;
						this.mapPicture.SizeMode = PictureBoxSizeMode.AutoSize;
						this.ShowMap(mapFile, this.mapPicture);
						this.mapPicture.SizeMode = PictureBoxSizeMode.StretchImage;
						this.mapPicture.ContextMenuStrip = this.C1CmnuMap;
						this.mapPanel.Controls.Add(this.mapPicture);
						this.mapPage.Controls.Add(this.mapPanel);
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdDeleteMap_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.c1tabMaps.SelectedTab != null)
				{
					if (XMessageBox.Show(sender.ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) != DialogResult.Cancel)
					{
						this.c1tabMaps.TabPages.Remove(this.c1tabMaps.SelectedTab);
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdChangeMapName_Click(object sender, EventArgs e)
		{
			try
			{
				using (dfrmMapInfo dfrmMapInfo = new dfrmMapInfo())
				{
					dfrmMapInfo.txtMapName.Text = this.c1tabMaps.SelectedTab.Text;
					dfrmMapInfo.txtMapFileName.Text = this.c1tabMaps.SelectedTab.Tag.ToString();
					if (dfrmMapInfo.ShowDialog(this) == DialogResult.OK)
					{
						string mapName = dfrmMapInfo.mapName;
						string mapFile = dfrmMapInfo.mapFile;
						for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
						{
							if (this.c1tabMaps.TabPages[i].Text == mapName && i != this.c1tabMaps.SelectedTab.TabIndex)
							{
								XMessageBox.Show(CommonStr.strMapNameDuplicated4Edit);
								return;
							}
						}
						TabPage selectedTab = this.c1tabMaps.SelectedTab;
						selectedTab.Text = mapName;
						selectedTab.Tag = mapFile;
						foreach (object current in selectedTab.Controls)
						{
							if (current is Panel)
							{
								foreach (object current2 in ((Panel)current).Controls)
								{
									if (current2 is PictureBox)
									{
										((PictureBox)current2).SizeMode = PictureBoxSizeMode.AutoSize;
										this.ShowMap(mapFile, (PictureBox)current2);
										((PictureBox)current2).SizeMode = PictureBoxSizeMode.StretchImage;
										return;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdAddDoor_Click(object sender, EventArgs e)
		{
			float doorScale = 1f;
			float.TryParse(wgAppConfig.getSystemParamByNO(22), out doorScale);
			try
			{
				if (sender == this.cmdAddDoorByLoc)
				{
					object obj = this.lastMouseP;
				}
				this.dvMapDoor = new DataView(this.dvDoors.Table);
				if (this.dvMapDoor.Count > 0 && this.c1tabMaps.TabPages.Count > 0)
				{
					using (dfrmSelectMapDoor dfrmSelectMapDoor = new dfrmSelectMapDoor())
					{
						for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
						{
							foreach (object current in this.c1tabMaps.TabPages[i].Controls)
							{
								if (current is Panel)
								{
									foreach (object current2 in ((Panel)current).Controls)
									{
										if (current2 is PictureBox)
										{
											foreach (ucMapDoor ucMapDoor in ((PictureBox)current2).Controls)
											{
												dfrmSelectMapDoor.lstMappedDoors.Items.Add(ucMapDoor.doorName);
											}
										}
									}
								}
							}
						}
						for (int j = 0; j <= this.dvMapDoor.Count - 1; j++)
						{
							if (dfrmSelectMapDoor.lstMappedDoors.FindString(this.dvMapDoor[j]["f_DoorName"].ToString()) == -1)
							{
								dfrmSelectMapDoor.lstUnMappedDoors.Items.Add(this.dvMapDoor[j]["f_DoorName"]);
							}
						}
						if (dfrmSelectMapDoor.ShowDialog(this) == DialogResult.OK)
						{
							string doorName = dfrmSelectMapDoor.doorName;
							if (!dfrmSelectMapDoor.bAddDoor)
							{
								for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
								{
									foreach (object current3 in this.c1tabMaps.TabPages[i].Controls)
									{
										if (current3 is Panel)
										{
											foreach (object current4 in ((Panel)current3).Controls)
											{
												if (current4 is PictureBox)
												{
													foreach (ucMapDoor ucMapDoor2 in ((PictureBox)current4).Controls)
													{
														if (ucMapDoor2.doorName == doorName)
														{
															ucMapDoor2.Dispose();
															((PictureBox)current4).Controls.Remove(ucMapDoor2);
															break;
														}
													}
												}
											}
										}
									}
								}
							}
							this.uc1door = new ucMapDoor();
							this.uc1door.doorName = doorName;
							this.uc1door.doorScale = doorScale;
							int num = this.arrZoomScaleTabpageName.IndexOf(this.c1tabMaps.SelectedTab.Text);
							if (num < 0)
							{
								this.uc1door.mapScale = 1f;
							}
							else
							{
								this.uc1door.mapScale = (float)this.arrZoomScale[num];
							}
							this.uc1door.MouseDown += new MouseEventHandler(this.UcMapDoor_MouseDown);
							this.uc1door.MouseMove += new MouseEventHandler(this.UcMapDoor_MouseMove);
							this.uc1door.MouseUp += new MouseEventHandler(this.UcMapDoor_MouseUp);
							this.uc1door.Click += new EventHandler(this.ucMapDoor_Click);
							this.uc1door.imgDoor = this.imgDoor2;
							this.uc1door.picDoorState.ContextMenuStrip = this.C1CmnuDoor;
							this.uc1door.ContextMenuStrip = this.contextMenuStrip1Doors;
							foreach (object current5 in this.c1tabMaps.SelectedTab.Controls)
							{
								if (current5 is Panel)
								{
									foreach (object current6 in ((Panel)current5).Controls)
									{
										if (current6 is PictureBox)
										{
											this.uc1door.bindSource = (PictureBox)current6;
											((PictureBox)current6).Controls.Add(this.uc1door);
											if (sender == this.cmdAddDoorByLoc)
											{
												this.uc1door.Location = this.lastMouseP;
											}
											else
											{
												this.uc1door.Location = new Point(-((PictureBox)current6).Location.X, -((PictureBox)current6).Location.Y);
											}
											return;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdDeleteDoor_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = false;
				foreach (object current in this.c1tabMaps.SelectedTab.Controls)
				{
					if (current is Panel)
					{
						foreach (object current2 in ((Panel)current).Controls)
						{
							if (current2 is PictureBox)
							{
								foreach (ucMapDoor ucMapDoor in ((PictureBox)current2).Controls)
								{
									if (ucMapDoor.txtDoorName == ucMapDoor.ActiveControl)
									{
										if (!flag)
										{
											if (XMessageBox.Show(sender.ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
											{
												return;
											}
											flag = true;
										}
										ucMapDoor.Dispose();
										((PictureBox)current2).Controls.Remove(ucMapDoor);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdSaveMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.dvMapDoor = new DataView(this.dvDoors.Table);
				string text = " DELETE FROM t_d_maps ";
				wgAppConfig.runUpdateSql(text);
				text = " DELETE FROM t_d_mapdoors ";
				wgAppConfig.runUpdateSql(text);
				this.cmdZoomIn.Enabled = false;
				this.cmdZoomOut.Enabled = false;
				this.cmdWatchCurrentMap.Enabled = false;
				this.cmdWatchAllMaps.Enabled = false;
				if (this.dvMapDoor.Count > 0 && this.c1tabMaps.TabPages.Count > 0)
				{
					this.cmdZoomIn.Enabled = true;
					this.cmdZoomOut.Enabled = true;
					this.cmdWatchCurrentMap.Enabled = true;
					this.cmdWatchAllMaps.Enabled = true;
					for (int i = 0; i <= this.c1tabMaps.TabPages.Count - 1; i++)
					{
						foreach (object current in this.c1tabMaps.TabPages[i].Controls)
						{
							if (current is Panel)
							{
								foreach (object current2 in ((Panel)current).Controls)
								{
									if (current2 is PictureBox)
									{
										text = " INSERT INTO t_d_maps";
										text += " (f_MapName, f_MapPageIndex, f_MapFile) ";
										text = text + " Values(" + wgTools.PrepareStr(this.c1tabMaps.TabPages[i].Text);
										text = text + " ," + i;
										text = text + " ," + wgTools.PrepareStr(this.c1tabMaps.TabPages[i].Tag);
										text += " )";
										wgAppConfig.runUpdateSql(text);
										text = "SELECT f_MapID from t_d_maps where f_MapName = " + wgTools.PrepareStr(this.c1tabMaps.TabPages[i].Text);
										long num = (long)int.Parse("0" + wgTools.SetObjToStr(wgAppConfig.getValBySql(text)));
										foreach (ucMapDoor ucMapDoor in ((PictureBox)current2).Controls)
										{
											this.dvMapDoor.RowFilter = " f_DoorName = " + wgTools.PrepareStr(ucMapDoor.doorName);
											text = " INSERT INTO t_d_mapdoors";
											text += " (f_DoorID, f_MapID, f_DoorLocationX, f_DoorLocationY) ";
											text = text + " Values(" + this.dvMapDoor[0]["f_DoorID"];
											text = text + " ," + num;
											text = text + " ," + ucMapDoor.doorLocation.X;
											text = text + "," + ucMapDoor.doorLocation.Y;
											text += " )";
											wgAppConfig.runUpdateSql(text);
										}
									}
								}
							}
						}
					}
				}
				this.C1ToolBar4MapEdit.Visible = false;
				this.bEditing = false;
				this.C1ToolBar4MapOperate.Visible = true;
				this.cmdAddDoorByLoc.Visible = false;
				this.Timer2.Enabled = true;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void cmdCancelAndExit_Click(object sender, EventArgs e)
		{
			try
			{
				this.c1tabMaps.Visible = false;
				this.C1ToolBar4MapEdit.Visible = false;
				this.bEditing = false;
				this.C1ToolBar4MapOperate.Visible = true;
				this.cmdAddDoorByLoc.Visible = false;
				this.loadmapFromDB();
				this.Timer2.Enabled = true;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void ucMapDoor_Click(object sender, EventArgs e)
		{
			try
			{
				ucMapDoor ucMapDoor = (ucMapDoor)sender;
				for (int i = 0; i <= this.lstDoors.Items.Count - 1; i++)
				{
					if (this.lstDoors.Items[i].Text == ucMapDoor.doorName)
					{
						this.lstDoors.SelectedItems.Clear();
						this.lstDoors.Items[i].Selected = true;
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void mapPicture_MouseDown(object sender, MouseEventArgs e)
		{
			this.lastMouseP = e.Location;
		}

		private void btnStopOthers_Click(object sender, EventArgs e)
		{
			if (this.btnStop != null)
			{
				this.btnStop.PerformClick();
			}
			this.cmdWatchCurrentMap.Text = this.strcmdWatchCurrentMapBk;
			this.cmdWatchAllMaps.Text = this.strcmdWatchAllMapsBk;
			this.cmdWatchCurrentMap.BackColor = Color.Transparent;
			this.cmdWatchAllMaps.BackColor = Color.Transparent;
			this.btnStopOthers.BackColor = Color.Transparent;
		}

		private void frmMaps_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				for (int i = 0; i < this.c1tabMaps.TabPages.Count; i++)
				{
					foreach (object current in this.c1tabMaps.TabPages[i].Controls)
					{
						if (current is Panel)
						{
							foreach (object current2 in ((Panel)current).Controls)
							{
								if (current2 is PictureBox)
								{
									wgAppConfig.DisposeImage((current2 as PictureBox).Image);
								}
							}
						}
					}
					wgAppConfig.DisposeImage(this.c1tabMaps.TabPages[i].BackgroundImage);
				}
			}
			catch
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.photoMemoryStream != null)
			{
				this.photoMemoryStream.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMaps));
			this.c1tabMaps = new TabControl();
			this.tabPage2 = new TabPage();
			this.tabPage1 = new TabPage();
			this.C1CmnuMap = new ContextMenuStrip(this.components);
			this.cmdAddDoorByLoc = new ToolStripMenuItem();
			this.C1CmnuDoor = new ContextMenuStrip(this.components);
			this.openDoorToolStripMenuItem = new ToolStripMenuItem();
			this.Timer2 = new Timer(this.components);
			this.C1ToolBar4MapEdit = new ToolStrip();
			this.cmdAddMap = new ToolStripButton();
			this.cmdDeleteMap = new ToolStripButton();
			this.cmdChangeMapName = new ToolStripButton();
			this.cmdAddDoor = new ToolStripButton();
			this.cmdDeleteDoor = new ToolStripButton();
			this.cmdSaveMap = new ToolStripButton();
			this.cmdCancelAndExit = new ToolStripButton();
			this.C1ToolBar4MapOperate = new ToolStrip();
			this.cmdCloseMaps = new ToolStripButton();
			this.cmdZoomIn = new ToolStripButton();
			this.cmdZoomOut = new ToolStripButton();
			this.cmdEditMap = new ToolStripButton();
			this.cmdWatchCurrentMap = new ToolStripButton();
			this.cmdWatchAllMaps = new ToolStripButton();
			this.btnStopOthers = new ToolStripButton();
			this.c1tabMaps.SuspendLayout();
			this.C1CmnuMap.SuspendLayout();
			this.C1CmnuDoor.SuspendLayout();
			this.C1ToolBar4MapEdit.SuspendLayout();
			this.C1ToolBar4MapOperate.SuspendLayout();
			base.SuspendLayout();
			this.c1tabMaps.Controls.Add(this.tabPage2);
			this.c1tabMaps.Controls.Add(this.tabPage1);
			componentResourceManager.ApplyResources(this.c1tabMaps, "c1tabMaps");
			this.c1tabMaps.Name = "c1tabMaps";
			this.c1tabMaps.SelectedIndex = 0;
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.C1CmnuMap.Items.AddRange(new ToolStripItem[]
			{
				this.cmdAddDoorByLoc
			});
			this.C1CmnuMap.Name = "C1CmnuMap";
			componentResourceManager.ApplyResources(this.C1CmnuMap, "C1CmnuMap");
			this.cmdAddDoorByLoc.Name = "cmdAddDoorByLoc";
			componentResourceManager.ApplyResources(this.cmdAddDoorByLoc, "cmdAddDoorByLoc");
			this.cmdAddDoorByLoc.Click += new EventHandler(this.cmdAddDoor_Click);
			this.C1CmnuDoor.Items.AddRange(new ToolStripItem[]
			{
				this.openDoorToolStripMenuItem
			});
			this.C1CmnuDoor.Name = "C1CmnuMap";
			componentResourceManager.ApplyResources(this.C1CmnuDoor, "C1CmnuDoor");
			this.openDoorToolStripMenuItem.Name = "openDoorToolStripMenuItem";
			componentResourceManager.ApplyResources(this.openDoorToolStripMenuItem, "openDoorToolStripMenuItem");
			this.Timer2.Tick += new EventHandler(this.Timer2_Tick);
			this.C1ToolBar4MapEdit.BackColor = Color.Transparent;
			this.C1ToolBar4MapEdit.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.C1ToolBar4MapEdit, "C1ToolBar4MapEdit");
			this.C1ToolBar4MapEdit.Items.AddRange(new ToolStripItem[]
			{
				this.cmdAddMap,
				this.cmdDeleteMap,
				this.cmdChangeMapName,
				this.cmdAddDoor,
				this.cmdDeleteDoor,
				this.cmdSaveMap,
				this.cmdCancelAndExit
			});
			this.C1ToolBar4MapEdit.Name = "C1ToolBar4MapEdit";
			this.cmdAddMap.ForeColor = Color.White;
			this.cmdAddMap.Image = Resources.pTools_Add_Auto;
			componentResourceManager.ApplyResources(this.cmdAddMap, "cmdAddMap");
			this.cmdAddMap.Name = "cmdAddMap";
			this.cmdAddMap.Click += new EventHandler(this.cmdAddMap_Click);
			this.cmdDeleteMap.ForeColor = Color.White;
			this.cmdDeleteMap.Image = Resources.pTools_CardLost;
			componentResourceManager.ApplyResources(this.cmdDeleteMap, "cmdDeleteMap");
			this.cmdDeleteMap.Name = "cmdDeleteMap";
			this.cmdDeleteMap.Click += new EventHandler(this.cmdDeleteMap_Click);
			this.cmdChangeMapName.ForeColor = Color.White;
			this.cmdChangeMapName.Image = Resources.pTools_Edit_Batch;
			componentResourceManager.ApplyResources(this.cmdChangeMapName, "cmdChangeMapName");
			this.cmdChangeMapName.Name = "cmdChangeMapName";
			this.cmdChangeMapName.Click += new EventHandler(this.cmdChangeMapName_Click);
			this.cmdAddDoor.ForeColor = Color.White;
			this.cmdAddDoor.Image = Resources.pTools_Add;
			componentResourceManager.ApplyResources(this.cmdAddDoor, "cmdAddDoor");
			this.cmdAddDoor.Name = "cmdAddDoor";
			this.cmdAddDoor.Click += new EventHandler(this.cmdAddDoor_Click);
			this.cmdDeleteDoor.ForeColor = Color.White;
			this.cmdDeleteDoor.Image = Resources.pTools_Del;
			componentResourceManager.ApplyResources(this.cmdDeleteDoor, "cmdDeleteDoor");
			this.cmdDeleteDoor.Name = "cmdDeleteDoor";
			this.cmdDeleteDoor.Click += new EventHandler(this.cmdDeleteDoor_Click);
			this.cmdSaveMap.ForeColor = Color.White;
			this.cmdSaveMap.Image = Resources.pTools_Maps_Save;
			componentResourceManager.ApplyResources(this.cmdSaveMap, "cmdSaveMap");
			this.cmdSaveMap.Name = "cmdSaveMap";
			this.cmdSaveMap.Click += new EventHandler(this.cmdSaveMap_Click);
			this.cmdCancelAndExit.ForeColor = Color.White;
			this.cmdCancelAndExit.Image = Resources.pTools_Maps_Cancel;
			componentResourceManager.ApplyResources(this.cmdCancelAndExit, "cmdCancelAndExit");
			this.cmdCancelAndExit.Name = "cmdCancelAndExit";
			this.cmdCancelAndExit.Click += new EventHandler(this.cmdCancelAndExit_Click);
			this.C1ToolBar4MapOperate.BackColor = Color.Transparent;
			this.C1ToolBar4MapOperate.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.C1ToolBar4MapOperate, "C1ToolBar4MapOperate");
			this.C1ToolBar4MapOperate.Items.AddRange(new ToolStripItem[]
			{
				this.cmdCloseMaps,
				this.cmdZoomIn,
				this.cmdZoomOut,
				this.cmdEditMap,
				this.cmdWatchCurrentMap,
				this.cmdWatchAllMaps,
				this.btnStopOthers
			});
			this.C1ToolBar4MapOperate.Name = "C1ToolBar4MapOperate";
			this.cmdCloseMaps.ForeColor = Color.White;
			this.cmdCloseMaps.Image = Resources.pTools_Maps_Close;
			componentResourceManager.ApplyResources(this.cmdCloseMaps, "cmdCloseMaps");
			this.cmdCloseMaps.Name = "cmdCloseMaps";
			this.cmdCloseMaps.Click += new EventHandler(this.cmdCloseMaps_Click);
			this.cmdZoomIn.ForeColor = Color.White;
			this.cmdZoomIn.Image = Resources.pTools_Maps_ZoomLarge;
			componentResourceManager.ApplyResources(this.cmdZoomIn, "cmdZoomIn");
			this.cmdZoomIn.Name = "cmdZoomIn";
			this.cmdZoomIn.Click += new EventHandler(this.cmdZoomIn_Click);
			this.cmdZoomOut.ForeColor = Color.White;
			this.cmdZoomOut.Image = Resources.pTools_Maps_ZoomSmall;
			componentResourceManager.ApplyResources(this.cmdZoomOut, "cmdZoomOut");
			this.cmdZoomOut.Name = "cmdZoomOut";
			this.cmdZoomOut.Click += new EventHandler(this.cmdZoomOut_Click);
			this.cmdEditMap.ForeColor = Color.White;
			this.cmdEditMap.Image = Resources.pTools_Edit;
			componentResourceManager.ApplyResources(this.cmdEditMap, "cmdEditMap");
			this.cmdEditMap.Name = "cmdEditMap";
			this.cmdEditMap.Click += new EventHandler(this.cmdEditMap_Click);
			this.cmdWatchCurrentMap.ForeColor = Color.White;
			this.cmdWatchCurrentMap.Image = Resources.pConsole_Monitor;
			componentResourceManager.ApplyResources(this.cmdWatchCurrentMap, "cmdWatchCurrentMap");
			this.cmdWatchCurrentMap.Name = "cmdWatchCurrentMap";
			this.cmdWatchCurrentMap.Click += new EventHandler(this.cmdWatchCurrentMap_Click);
			this.cmdWatchAllMaps.ForeColor = Color.White;
			this.cmdWatchAllMaps.Image = Resources.pTools_Maps_SelectAll;
			componentResourceManager.ApplyResources(this.cmdWatchAllMaps, "cmdWatchAllMaps");
			this.cmdWatchAllMaps.Name = "cmdWatchAllMaps";
			this.cmdWatchAllMaps.Click += new EventHandler(this.cmdWatchAllMaps_Click);
			this.btnStopOthers.ForeColor = Color.White;
			this.btnStopOthers.Image = Resources.pConsole_Stop;
			componentResourceManager.ApplyResources(this.btnStopOthers, "btnStopOthers");
			this.btnStopOthers.Name = "btnStopOthers";
			this.btnStopOthers.Click += new EventHandler(this.btnStopOthers_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.c1tabMaps);
			base.Controls.Add(this.C1ToolBar4MapEdit);
			base.Controls.Add(this.C1ToolBar4MapOperate);
			base.Name = "frmMaps";
			base.FormClosing += new FormClosingEventHandler(this.frmMaps_FormClosing);
			base.Load += new EventHandler(this.frmMaps_Load);
			this.c1tabMaps.ResumeLayout(false);
			this.C1CmnuMap.ResumeLayout(false);
			this.C1CmnuDoor.ResumeLayout(false);
			this.C1ToolBar4MapEdit.ResumeLayout(false);
			this.C1ToolBar4MapEdit.PerformLayout();
			this.C1ToolBar4MapOperate.ResumeLayout(false);
			this.C1ToolBar4MapOperate.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
