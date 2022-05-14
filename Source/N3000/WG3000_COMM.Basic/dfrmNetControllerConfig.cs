using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmNetControllerConfig : frmN3000
	{
		public delegate void AsyncCallback(IAsyncResult ar);

		public delegate void AddTolstDiscoveredDevices(object o, object pcIP);

		private string strControllers = "";

		private wgUdpComm wgudp;

		private bool bFirstShowInfo = true;

		private dfrmFind dfrmFind1;

		private frmTestController dfrmTest;

		private frmProductFormat frmProductFormat1;

		private bool bIPAndWEBConfigure;

		private bool bUpdateIPConfigure;

		private bool bOption;

		private int commPort = 60000;

		private bool bUpdateWEBConfigure;

		private bool bWEBEnabled;

		private string strWEBLanguage1 = "";

		private string strSelectedFile1 = "";

		private bool bOptionWeb;

		private int HttpPort = 80;

		private bool bAdjustTime;

		private int webDateFormat;

		private bool bWebOnlyQuery;

		private bool bUpdateSuperCard_IPWEB;

		private string superCard1_IPWEB = "";

		private string superCard2_IPWEB = "";

		private bool bUpdateSpecialCard_IPWEB;

		private string SpecialCard1_IPWEB = "";

		private string SpecialCard2_IPWEB = "";

		private string strWEBLanguage2 = "";

		private string strSelectedFile2 = "";

		private bool bAutoUploadUsers;

		private string strIP_IPWEB = "";

		private string strNETMASK_IPWEB = "";

		private string strGateway_IPWEB = "";

		private DataTable dtWebStringAdvanced_IPWEB;

		private DataTable dtPrivilege;

		private DataTable tb;

		private DataView dv;

		private bool bInput5678;

		private IContainer components;

		private Button btnSearch;

		private Button btnConfigure;

		private Button btnExit;

		private DataGridView dgvFoundControllers;

		private CheckBox chkSearchAgain;

		private Label label1;

		private Label lblCount;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem configureToolStripMenuItem;

		private ToolStripMenuItem findF3ToolStripMenuItem;

		private ToolStripMenuItem searchToolStripMenuItem;

		private ToolStripMenuItem clearToolStripMenuItem;

		private Label lblSearchNow;

		private Button btnDefault;

		public Button btnAddToSystem;

		private ToolStripMenuItem restoreDefaultParamToolStripMenuItem;

		private ToolStripMenuItem restoreAllSwipesToolStripMenuItem;

		private ToolStripMenuItem formatToolStripMenuItem;

		private ToolStripMenuItem searchAdvancedToolStripMenuItem;

		private ToolStripMenuItem searchSpecialSNToolStripMenuItem;

		private ToolStripMenuItem search100FromTheSpecialSNToolStripMenuItem;

		public Button btnIPAndWebConfigure;

		private DataGridViewTextBoxColumn f_ID;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewTextBoxColumn f_IP;

		private DataGridViewTextBoxColumn f_Mask;

		private DataGridViewTextBoxColumn f_Gateway;

		private DataGridViewTextBoxColumn f_PORT;

		private DataGridViewTextBoxColumn f_MACAddr;

		private DataGridViewTextBoxColumn f_PCIPAddr;

		private DataGridViewTextBoxColumn f_Note;

		private ToolStripMenuItem communicationTestToolStripMenuItem;

		private ToolStripMenuItem addSelectedToSystemToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultIPToolStripMenuItem;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private ToolStripMenuItem clearSwipesToolStripMenuItem;

		public dfrmNetControllerConfig()
		{
			this.InitializeComponent();
		}

		public void AddDiscoveryEntry(object o, object pcIP)
		{
			byte[] pkt = (byte[])o;
			string text = (string)pcIP;
			wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure(pkt, 20);
			string text2;
			if (wgMjControllerConfigure.webPort == 0 || wgMjControllerConfigure.webPort == 65535)
			{
				text2 = string.Format("{0},{1}", wgMjControllerConfigure.webDeviceName, CommonStr.strWEBDisabled);
			}
			else
			{
				text2 = string.Format("{0},{1},{2},{3}", new object[]
				{
					wgMjControllerConfigure.webDeviceName,
					wgMjControllerConfigure.webLanguage,
					(wgAppConfig.CultureInfoStr == "zh-CHS") ? wgMjControllerConfigure.webDateDisplayFormatCHS : wgMjControllerConfigure.webDateDisplayFormat,
					wgMjControllerConfigure.webPort.ToString()
				});
			}
			string[] array = new string[]
			{
				(this.dgvFoundControllers.Rows.Count + 1).ToString().PadLeft(4, '0'),
				wgMjControllerConfigure.controllerSN.ToString(),
				wgMjControllerConfigure.ip.ToString(),
				wgMjControllerConfigure.mask.ToString(),
				wgMjControllerConfigure.gateway.ToString(),
				wgMjControllerConfigure.port.ToString(),
				wgMjControllerConfigure.MACAddr,
				text,
				text2
			};
			for (int i = 0; i < array.Length; i++)
			{
				this.strControllers = this.strControllers + array[i] + ",";
			}
			this.dgvFoundControllers.Rows.Add(array);
		}

		private void dfrmNetControllerConfig_Load(object sender, EventArgs e)
		{
			this.btnConfigure.Enabled = false;
			this.btnSearch.PerformClick();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			uint iDevSnTo = 4294967295u;
			Cursor.Current = Cursors.WaitCursor;
			this.lblCount.Text = "0";
			this.toolStripStatusLabel1.Text = "0";
			this.dgvFoundControllers.Rows.Clear();
			this.btnConfigure.Enabled = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text = "";
			this.btnSearch.Enabled = false;
			Thread.Sleep(100);
			this.Refresh();
			WGPacket wGPacket = new WGPacket();
			wGPacket.type = 36;
			wGPacket.code = 16;
			wGPacket.iDevSnFrom = 0u;
			wGPacket.iDevSnTo = iDevSnTo;
			wGPacket.iCallReturn = 0;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			if (WGPacket.bCommP)
			{
				WGPacket.bCommP = false;
				string commPStr = wgTools.CommPStr;
				wgTools.CommPStr = "";
				NetworkInterface[] array = allNetworkInterfaces;
				for (int i = 0; i < array.Length; i++)
				{
					NetworkInterface networkInterface = array[i];
					if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkInterface.OperationalStatus == OperationalStatus.Up)
					{
						IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
						UnicastIPAddressInformationCollection unicastAddresses = iPProperties.UnicastAddresses;
						if (unicastAddresses.Count > 0)
						{
							Console.WriteLine(networkInterface.Description);
							foreach (UnicastIPAddressInformation current in unicastAddresses)
							{
								if (current.Address.AddressFamily == AddressFamily.InterNetwork && !current.Address.IsIPv6LinkLocal && !(current.Address.ToString() == "127.0.0.1"))
								{
									Console.WriteLine("  IP ............................. : {0}", current.Address.ToString());
									this.wgudp = new wgUdpComm(current.Address);
									Thread.Sleep(300);
									byte[] array2 = wGPacket.ToBytes(this.wgudp.udpPort);
									if (array2 == null)
									{
										return;
									}
									byte[] array3 = null;
									this.wgudp.udp_get(array2, 300, 0u, null, 60000, ref array3);
									if (array3 != null)
									{
										long ticks = DateTime.Now.Ticks;
										long num4 = ticks + 4000000L;
										this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
										{
											array3,
											current.Address.ToString()
										});
										while (DateTime.Now.Ticks < num4)
										{
											if (this.wgudp.PacketCount > 0)
											{
												while (this.wgudp.PacketCount > 0)
												{
													array3 = this.wgudp.GetPacket();
													this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
													{
														array3,
														current.Address.ToString()
													});
												}
												num4 = DateTime.Now.Ticks + 4000000L;
											}
											else
											{
												Thread.Sleep(100);
											}
										}
									}
								}
							}
							Console.WriteLine();
						}
					}
				}
				wgTools.CommPStr = commPStr;
				WGPacket.bCommP = true;
			}
			NetworkInterface[] array4 = allNetworkInterfaces;
			for (int j = 0; j < array4.Length; j++)
			{
				NetworkInterface networkInterface2 = array4[j];
				if (networkInterface2.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkInterface2.OperationalStatus == OperationalStatus.Up)
				{
					num++;
					if (networkInterface2.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
					{
						num2++;
					}
					IPInterfaceProperties iPProperties2 = networkInterface2.GetIPProperties();
					UnicastIPAddressInformationCollection unicastAddresses2 = iPProperties2.UnicastAddresses;
					if (unicastAddresses2.Count > 0)
					{
						Console.WriteLine(networkInterface2.Description);
						bool flag = true;
						foreach (UnicastIPAddressInformation current2 in unicastAddresses2)
						{
							if (current2.Address.AddressFamily == AddressFamily.InterNetwork && !current2.Address.IsIPv6LinkLocal && !(current2.Address.ToString() == "127.0.0.1"))
							{
								if (flag)
								{
									num3++;
									flag = false;
								}
								text += string.Format("{0}, ", current2.Address.ToString());
								Console.WriteLine("  IP ............................. : {0}", current2.Address.ToString());
								this.wgudp = new wgUdpComm(current2.Address);
								Thread.Sleep(300);
								byte[] array5 = wGPacket.ToBytes(this.wgudp.udpPort);
								if (array5 == null)
								{
									return;
								}
								byte[] array6 = null;
								this.wgudp.udp_get(array5, 300, 4294967295u, null, 60000, ref array6);
								if (array6 != null)
								{
									long ticks2 = DateTime.Now.Ticks;
									long num5 = ticks2 + 4000000L;
									int num6 = 0;
									this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
									{
										array6,
										current2.Address.ToString()
									});
									num6++;
									while (DateTime.Now.Ticks < num5)
									{
										if (this.wgudp.PacketCount > 0)
										{
											while (this.wgudp.PacketCount > 0)
											{
												array6 = this.wgudp.GetPacket();
												this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
												{
													array6,
													current2.Address.ToString()
												});
												num6++;
											}
											num5 = DateTime.Now.Ticks + 4000000L;
											wgTools.WgDebugWrite(string.Format("搜索到控制器数={0}:所花时间={1}ms", num6.ToString(), ((DateTime.Now.Ticks - ticks2) / 10000L).ToString()), new object[0]);
										}
										else
										{
											Thread.Sleep(100);
										}
									}
								}
							}
						}
						Console.WriteLine();
					}
				}
			}
			this.btnSearch.Enabled = true;
			wgAppConfig.wgLog(string.Format("{0} Count = {1:d}  : {2}", this.Text, this.dgvFoundControllers.Rows.Count, this.strControllers));
			wgAppConfig.wgLog(string.Format("{0}: Up Adapter Count = {1:d}; Up Adapter Wireless Count = {2:d}; Up Adapter With IPV4 Count = {3:d}; All IP : {4}", new object[]
			{
				string.Empty.PadLeft(this.Text.Length * 2, ' '),
				num,
				num2,
				num3,
				text
			}));
			if (this.dgvFoundControllers.Rows.Count > 0)
			{
				this.btnConfigure.Enabled = true;
			}
			else
			{
				string text2 = CommonStr.strNoControllerInfo2Base;
				if (num == 0)
				{
					text2 = CommonStr.strNoControllerInfo3PCNotConnected;
				}
				else if (num >= 2 && num2 >= 1)
				{
					text2 = CommonStr.strNoControllerInfo1;
				}
				if (this.bFirstShowInfo)
				{
					this.bFirstShowInfo = false;
					XMessageBox.Show(text2);
				}
			}
			this.lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString();
			this.toolStripStatusLabel1.Text = this.dgvFoundControllers.Rows.Count.ToString();
			Cursor.Current = Cursors.Default;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnConfigure_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
				return;
			}
			using (dfrmTCPIPConfigure dfrmTCPIPConfigure = new dfrmTCPIPConfigure())
			{
				DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
				dfrmTCPIPConfigure.strSN = dataGridViewRow.Cells["f_ControllerSN"].Value.ToString();
				dfrmTCPIPConfigure.strMac = dataGridViewRow.Cells["f_MACAddr"].Value.ToString();
				dfrmTCPIPConfigure.strIP = dataGridViewRow.Cells["f_IP"].Value.ToString();
				dfrmTCPIPConfigure.strMask = dataGridViewRow.Cells["f_Mask"].Value.ToString();
				dfrmTCPIPConfigure.strGateway = dataGridViewRow.Cells["f_Gateway"].Value.ToString();
				dfrmTCPIPConfigure.strTCPPort = dataGridViewRow.Cells["f_PORT"].Value.ToString();
				string text = dataGridViewRow.Cells["f_PCIPAddr"].Value.ToString();
				if (dfrmTCPIPConfigure.ShowDialog(this) == DialogResult.OK)
				{
					string strSN = dfrmTCPIPConfigure.strSN;
					string strMac = dfrmTCPIPConfigure.strMac;
					string strIP = dfrmTCPIPConfigure.strIP;
					string strMask = dfrmTCPIPConfigure.strMask;
					string strGateway = dfrmTCPIPConfigure.strGateway;
					string strTCPPort = dfrmTCPIPConfigure.strTCPPort;
					string text2 = dfrmTCPIPConfigure.Text;
					this.Refresh();
					Cursor.Current = Cursors.WaitCursor;
					this.IPConfigureCPU(strSN, strMac, strIP, strMask, strGateway, strTCPPort, text);
					wgAppConfig.wgLog(string.Concat(new string[]
					{
						text2,
						"  SN=",
						strSN,
						", Mac=",
						strMac,
						",IP =",
						strIP,
						",Mask=",
						strMask,
						",Gateway=",
						strGateway,
						", Port = ",
						strTCPPort,
						", PC IPAddr=",
						text
					}));
					if (this.chkSearchAgain.Checked)
					{
						Thread.Sleep(5000);
						this.btnSearch.PerformClick();
					}
					else
					{
						this.dgvFoundControllers.Rows.Remove(dataGridViewRow);
					}
				}
			}
		}

		private void IPConfigure(string strSN, string strMac, string strIP, string strMask, string strGateway, string strTCPPort, string PCIPAddr)
		{
			if (this.wgudp != null)
			{
				this.wgudp = null;
			}
			IPAddress iPAddress;
			if (IPAddress.TryParse(PCIPAddr, out iPAddress))
			{
				this.wgudp = new wgUdpComm(IPAddress.Parse(PCIPAddr));
			}
			else
			{
				this.wgudp = new wgUdpComm();
			}
			Thread.Sleep(300);
			WGPacketWith1152 wGPacketWith = new WGPacketWith1152();
			wGPacketWith.type = 36;
			wGPacketWith.code = 32;
			wGPacketWith.iDevSnFrom = 0u;
			if (int.Parse(strSN) == -1)
			{
				wGPacketWith.iDevSnTo = 4294967295u;
			}
			else
			{
				wGPacketWith.iDevSnTo = uint.Parse(strSN);
			}
			wGPacketWith.iCallReturn = 0;
			int num = 116;
			IPAddress.Parse(strIP).GetAddressBytes().CopyTo(wGPacketWith.ucData, num);
			byte[] expr_B3_cp_0 = wGPacketWith.ucData;
			int expr_B3_cp_1 = 1024 + (num >> 3);
			expr_B3_cp_0[expr_B3_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_E1_cp_0 = wGPacketWith.ucData;
			int expr_E1_cp_1 = 1024 + (num >> 3);
			expr_E1_cp_0[expr_E1_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_10F_cp_0 = wGPacketWith.ucData;
			int expr_10F_cp_1 = 1024 + (num >> 3);
			expr_10F_cp_0[expr_10F_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_13D_cp_0 = wGPacketWith.ucData;
			int expr_13D_cp_1 = 1024 + (num >> 3);
			expr_13D_cp_0[expr_13D_cp_1] |= (byte)(1 << (num & 7));
			num = 120;
			IPAddress.Parse(strMask).GetAddressBytes().CopyTo(wGPacketWith.ucData, num);
			byte[] expr_182_cp_0 = wGPacketWith.ucData;
			int expr_182_cp_1 = 1024 + (num >> 3);
			expr_182_cp_0[expr_182_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_1B0_cp_0 = wGPacketWith.ucData;
			int expr_1B0_cp_1 = 1024 + (num >> 3);
			expr_1B0_cp_0[expr_1B0_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_1DE_cp_0 = wGPacketWith.ucData;
			int expr_1DE_cp_1 = 1024 + (num >> 3);
			expr_1DE_cp_0[expr_1DE_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_20C_cp_0 = wGPacketWith.ucData;
			int expr_20C_cp_1 = 1024 + (num >> 3);
			expr_20C_cp_0[expr_20C_cp_1] |= (byte)(1 << (num & 7));
			num = 124;
			IPAddress.Parse(strGateway).GetAddressBytes().CopyTo(wGPacketWith.ucData, num);
			byte[] expr_251_cp_0 = wGPacketWith.ucData;
			int expr_251_cp_1 = 1024 + (num >> 3);
			expr_251_cp_0[expr_251_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_27F_cp_0 = wGPacketWith.ucData;
			int expr_27F_cp_1 = 1024 + (num >> 3);
			expr_27F_cp_0[expr_27F_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_2AD_cp_0 = wGPacketWith.ucData;
			int expr_2AD_cp_1 = 1024 + (num >> 3);
			expr_2AD_cp_0[expr_2AD_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_2DB_cp_0 = wGPacketWith.ucData;
			int expr_2DB_cp_1 = 1024 + (num >> 3);
			expr_2DB_cp_0[expr_2DB_cp_1] |= (byte)(1 << (num & 7));
			num = 128;
			wGPacketWith.ucData[num] = (byte)(int.Parse(strTCPPort) & 255);
			byte[] expr_321_cp_0 = wGPacketWith.ucData;
			int expr_321_cp_1 = 1024 + (num >> 3);
			expr_321_cp_0[expr_321_cp_1] |= (byte)(1 << (num & 7));
			num++;
			wGPacketWith.ucData[num] = (byte)(int.Parse(strTCPPort) >> 8 & 255);
			byte[] expr_367_cp_0 = wGPacketWith.ucData;
			int expr_367_cp_1 = 1024 + (num >> 3);
			expr_367_cp_0[expr_367_cp_1] |= (byte)(1 << (num & 7));
			byte[] array = wGPacketWith.ToBytes(this.wgudp.udpPort);
			if (array == null)
			{
				wgTools.WgDebugWrite("Err: IP Configure", new object[0]);
				return;
			}
			byte[] array2 = null;
			this.wgudp.udp_get(array, 300, 2147483647u, null, 60000, ref array2);
		}

		private void IPConfigureCPU(string strSN, string strMac, string strIP, string strMask, string strGateway, string strTCPPort, string PCIPAddr)
		{
			if (this.wgudp != null)
			{
				this.wgudp = null;
			}
			IPAddress iPAddress;
			if (IPAddress.TryParse(PCIPAddr, out iPAddress))
			{
				this.wgudp = new wgUdpComm(IPAddress.Parse(PCIPAddr));
			}
			else
			{
				this.wgudp = new wgUdpComm();
			}
			Thread.Sleep(300);
			WGPacketWith1152 wGPacketWith = new WGPacketWith1152();
			wGPacketWith.type = 37;
			wGPacketWith.code = 32;
			wGPacketWith.iDevSnFrom = 0u;
			wGPacketWith.iCallReturn = 0;
			if (int.Parse(strSN) == -1)
			{
				wGPacketWith.iDevSnTo = 4294967295u;
			}
			else
			{
				wGPacketWith.iDevSnTo = uint.Parse(strSN);
			}
			byte[] array = new byte[1152];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			WGPacketWith1152 wGPacketWith2 = new WGPacketWith1152();
			int num = 116;
			IPAddress.Parse(strIP).GetAddressBytes().CopyTo(wGPacketWith2.ucData, num);
			byte[] expr_DC_cp_0 = wGPacketWith2.ucData;
			int expr_DC_cp_1 = 1024 + (num >> 3);
			expr_DC_cp_0[expr_DC_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_10F_cp_0 = wGPacketWith2.ucData;
			int expr_10F_cp_1 = 1024 + (num >> 3);
			expr_10F_cp_0[expr_10F_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_142_cp_0 = wGPacketWith2.ucData;
			int expr_142_cp_1 = 1024 + (num >> 3);
			expr_142_cp_0[expr_142_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_175_cp_0 = wGPacketWith2.ucData;
			int expr_175_cp_1 = 1024 + (num >> 3);
			expr_175_cp_0[expr_175_cp_1] |= (byte)(1 << (num & 7));
			num = 120;
			IPAddress.Parse(strMask).GetAddressBytes().CopyTo(wGPacketWith2.ucData, num);
			byte[] expr_1C0_cp_0 = wGPacketWith2.ucData;
			int expr_1C0_cp_1 = 1024 + (num >> 3);
			expr_1C0_cp_0[expr_1C0_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_1F3_cp_0 = wGPacketWith2.ucData;
			int expr_1F3_cp_1 = 1024 + (num >> 3);
			expr_1F3_cp_0[expr_1F3_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_226_cp_0 = wGPacketWith2.ucData;
			int expr_226_cp_1 = 1024 + (num >> 3);
			expr_226_cp_0[expr_226_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_259_cp_0 = wGPacketWith2.ucData;
			int expr_259_cp_1 = 1024 + (num >> 3);
			expr_259_cp_0[expr_259_cp_1] |= (byte)(1 << (num & 7));
			num = 124;
			if (string.IsNullOrEmpty(strGateway))
			{
				wGPacketWith2.ucData[num] = 0;
				wGPacketWith2.ucData[num + 1] = 0;
				wGPacketWith2.ucData[num + 2] = 0;
				wGPacketWith2.ucData[num + 3] = 0;
			}
			else
			{
				IPAddress.Parse(strGateway).GetAddressBytes().CopyTo(wGPacketWith2.ucData, num);
			}
			byte[] expr_2E1_cp_0 = wGPacketWith2.ucData;
			int expr_2E1_cp_1 = 1024 + (num >> 3);
			expr_2E1_cp_0[expr_2E1_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_314_cp_0 = wGPacketWith2.ucData;
			int expr_314_cp_1 = 1024 + (num >> 3);
			expr_314_cp_0[expr_314_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_347_cp_0 = wGPacketWith2.ucData;
			int expr_347_cp_1 = 1024 + (num >> 3);
			expr_347_cp_0[expr_347_cp_1] |= (byte)(1 << (num & 7));
			num++;
			byte[] expr_37A_cp_0 = wGPacketWith2.ucData;
			int expr_37A_cp_1 = 1024 + (num >> 3);
			expr_37A_cp_0[expr_37A_cp_1] |= (byte)(1 << (num & 7));
			num = 128;
			wGPacketWith2.ucData[num] = (byte)(int.Parse(strTCPPort) & 255);
			byte[] expr_3C6_cp_0 = wGPacketWith2.ucData;
			int expr_3C6_cp_1 = 1024 + (num >> 3);
			expr_3C6_cp_0[expr_3C6_cp_1] |= (byte)(1 << (num & 7));
			num++;
			wGPacketWith2.ucData[num] = (byte)(int.Parse(strTCPPort) >> 8 & 255);
			byte[] expr_413_cp_0 = wGPacketWith2.ucData;
			int expr_413_cp_1 = 1024 + (num >> 3);
			expr_413_cp_0[expr_413_cp_1] |= (byte)(1 << (num & 7));
			num = 0;
			for (int j = 0; j < 16; j++)
			{
				array[num] = (wGPacketWith2.ucData[116 + j] & 255);
				byte[] expr_45A_cp_0 = array;
				int expr_45A_cp_1 = 1024 + (num >> 3);
				expr_45A_cp_0[expr_45A_cp_1] |= (byte)(1 << (num & 7));
				num++;
			}
			array.CopyTo(wGPacketWith.ucData, 0);
			byte[] array2 = wGPacketWith.ToBytes(this.wgudp.udpPort);
			if (array2 == null)
			{
				wgTools.WgDebugWrite("Err: IP Configure", new object[0]);
				return;
			}
			byte[] array3 = null;
			this.wgudp.udp_get(array2, 300, 2147483647u, null, 60000, ref array3);
		}

		private void dgvFoundControllers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.btnConfigure.Enabled)
			{
				this.btnConfigure.PerformClick();
			}
		}

		private void btnAddToSystem_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.Rows.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				string text = "";
				int num = 0;
				for (int i = 0; i < this.dgvFoundControllers.Rows.Count; i++)
				{
					int num2 = int.Parse(this.dgvFoundControllers.Rows[i].Cells[1].Value.ToString());
					if (num2 != -1 && !icController.IsExisted2SN(num2, 0))
					{
						text = text + num2.ToString() + ",";
						num++;
						this.lblSearchNow.Text = this.dgvFoundControllers.Rows[i].Cells[0].Value.ToString() + "-" + num2.ToString();
						this.toolStripStatusLabel2.Text = this.dgvFoundControllers.Rows[i].Cells[0].Value.ToString() + "-" + num2.ToString();
						using (dfrmController dfrmController = new dfrmController())
						{
							dfrmController.OperateNew = true;
							dfrmController.WindowState = FormWindowState.Minimized;
							dfrmController.Show();
							dfrmController.mtxtbControllerSN.Text = num2.ToString();
							dfrmController.btnNext.PerformClick();
							dfrmController.btnOK.PerformClick();
							Application.DoEvents();
						}
					}
				}
				Cursor.Current = Cursors.Default;
				XMessageBox.Show(string.Format("{0}:[{1:d}]\r\n{2}  ", CommonStr.strAutoAddController, num, text), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void dfrmNetControllerConfig_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(this.dgvFoundControllers, null);
				}
				if (e.Control && e.KeyValue == 67)
				{
					string text = "";
					for (int i = 0; i < this.dgvFoundControllers.Rows.Count; i++)
					{
						for (int j = 0; j < this.dgvFoundControllers.ColumnCount; j++)
						{
							text = text + this.dgvFoundControllers.Rows[i].Cells[j].Value.ToString() + "\t";
						}
						text += "\r\n";
					}
					Clipboard.SetDataObject(text, false);
				}
				if (e.Control && e.Shift && e.KeyValue == 81)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					this.funCtrlShiftQ();
				}
				if (e.Control && e.Shift && e.KeyValue == 84)
				{
					if (icOperator.OperatorID != 1)
					{
						XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						this.FuncControlShiftT();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void findF3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dfrmFind1 == null)
				{
					this.dfrmFind1 = new dfrmFind();
				}
				this.dfrmFind1.setObjtoFind(this.dgvFoundControllers, null);
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.dgvFoundControllers.Rows.Clear();
			this.lblCount.Text = "0";
			this.toolStripStatusLabel1.Text = "0";
		}

		public bool isExisted(string sn, string ip)
		{
			bool result = false;
			try
			{
				if (this.dgvFoundControllers.Rows.Count > 0)
				{
					for (int i = 0; i < this.dgvFoundControllers.Rows.Count; i++)
					{
						if (sn == this.dgvFoundControllers.Rows[i].Cells[1].Value.ToString() && ip == this.dgvFoundControllers.Rows[i].Cells[7].Value.ToString())
						{
							result = true;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
			return result;
		}

		private void search100FromTheSpecialSNToolStripMenuItem_Click(object sender, EventArgs e)
		{
			uint num = 0u;
			uint num2 = 0u;
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = (sender as ToolStripItem).Text;
				dfrmInputNewName.label1.Text = CommonStr.strControllerSN;
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				if (!uint.TryParse(dfrmInputNewName.strNewName, out num))
				{
					return;
				}
				if (wgMjController.GetControllerType((int)num) == 0)
				{
					return;
				}
			}
			Cursor.Current = Cursors.WaitCursor;
			this.btnConfigure.Enabled = false;
			this.btnSearch.Enabled = false;
			Thread.Sleep(100);
			this.Refresh();
			WGPacket wGPacket = new WGPacket();
			wGPacket.type = 36;
			wGPacket.code = 16;
			wGPacket.iDevSnFrom = 0u;
			wGPacket.iCallReturn = 0;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			num2 = num + 100u - 1u;
			if (sender == this.searchSpecialSNToolStripMenuItem)
			{
				num2 = num + 1u - 1u;
			}
			this.wgudp = null;
			while (num <= num2)
			{
				if ((num2 - num) % 5u == 0u)
				{
					this.lblSearchNow.Text = num.ToString();
					this.toolStripStatusLabel2.Text = num.ToString();
					this.lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString();
					this.toolStripStatusLabel1.Text = this.dgvFoundControllers.Rows.Count.ToString();
					this.Refresh();
					Application.DoEvents();
					Cursor.Current = Cursors.WaitCursor;
				}
				wGPacket.iDevSnTo = num;
				num += 1u;
				if (WGPacket.bCommP)
				{
					WGPacket.bCommP = false;
					string commPStr = wgTools.CommPStr;
					wgTools.CommPStr = "";
					NetworkInterface[] array = allNetworkInterfaces;
					for (int i = 0; i < array.Length; i++)
					{
						NetworkInterface networkInterface = array[i];
						IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
						UnicastIPAddressInformationCollection unicastAddresses = iPProperties.UnicastAddresses;
						if (unicastAddresses.Count > 0)
						{
							Console.WriteLine(networkInterface.Description);
							foreach (UnicastIPAddressInformation current in unicastAddresses)
							{
								if (!current.Address.IsIPv6LinkLocal && !(current.Address.ToString() == "127.0.0.1"))
								{
									Console.WriteLine("  IP ............................. : {0}", current.Address.ToString());
									if (this.wgudp == null)
									{
										this.wgudp = new wgUdpComm(current.Address);
										Thread.Sleep(300);
									}
									else if (this.wgudp.localIP.ToString() != current.Address.ToString())
									{
										this.wgudp = new wgUdpComm(current.Address);
										Thread.Sleep(300);
									}
									byte[] array2 = wGPacket.ToBytes(this.wgudp.udpPort);
									if (array2 == null)
									{
										return;
									}
									byte[] array3 = null;
									this.wgudp.udp_get(array2, 300, 0u, null, 60000, ref array3);
									if (array3 != null && !this.isExisted(wGPacket.iDevSnTo.ToString(), this.wgudp.localIP.ToString()))
									{
										this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
										{
											array3,
											current.Address.ToString()
										});
										long ticks = DateTime.Now.Ticks;
									}
								}
							}
							Console.WriteLine();
						}
					}
					wgTools.CommPStr = commPStr;
					WGPacket.bCommP = true;
				}
				NetworkInterface[] array4 = allNetworkInterfaces;
				for (int j = 0; j < array4.Length; j++)
				{
					NetworkInterface networkInterface2 = array4[j];
					IPInterfaceProperties iPProperties2 = networkInterface2.GetIPProperties();
					UnicastIPAddressInformationCollection unicastAddresses2 = iPProperties2.UnicastAddresses;
					if (unicastAddresses2.Count > 0)
					{
						Console.WriteLine(networkInterface2.Description);
						foreach (UnicastIPAddressInformation current2 in unicastAddresses2)
						{
							if (!current2.Address.IsIPv6LinkLocal && !(current2.Address.ToString() == "127.0.0.1"))
							{
								Console.WriteLine("  IP ............................. : {0}", current2.Address.ToString());
								if (this.wgudp == null)
								{
									this.wgudp = new wgUdpComm(current2.Address);
									Thread.Sleep(300);
								}
								else if (this.wgudp.localIP.ToString() != current2.Address.ToString())
								{
									this.wgudp = new wgUdpComm(current2.Address);
									Thread.Sleep(300);
								}
								byte[] array5 = wGPacket.ToBytes(this.wgudp.udpPort);
								if (array5 == null)
								{
									return;
								}
								byte[] array6 = null;
								this.wgudp.udp_get(array5, 300, 4294967295u, null, 60000, ref array6);
								if (array6 != null && !this.isExisted(wGPacket.iDevSnTo.ToString(), this.wgudp.localIP.ToString()))
								{
									this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
									{
										array6,
										current2.Address.ToString()
									});
								}
							}
						}
						Console.WriteLine();
					}
				}
			}
			this.btnSearch.Enabled = true;
			if (this.dgvFoundControllers.Rows.Count > 0)
			{
				this.btnConfigure.Enabled = true;
			}
			this.lblSearchNow.Text = num2.ToString();
			this.toolStripStatusLabel2.Text = num2.ToString();
			this.lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString();
			this.toolStripStatusLabel1.Text = this.dgvFoundControllers.Rows.Count.ToString();
			Cursor.Current = Cursors.Default;
		}

		private void FuncControlShiftT()
		{
			uint num = 0u;
			uint num2 = 0u;
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = "Start";
				dfrmInputNewName.label1.Text = CommonStr.strControllerSN;
				dfrmInputNewName.strNewName = "100190001";
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				if (!uint.TryParse(dfrmInputNewName.strNewName, out num))
				{
					return;
				}
				if (wgMjController.GetControllerType((int)num) == 0)
				{
					return;
				}
			}
			this.dgvFoundControllers.Rows.Clear();
			this.lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString();
			this.toolStripStatusLabel1.Text = this.dgvFoundControllers.Rows.Count.ToString();
			Cursor.Current = Cursors.WaitCursor;
			this.btnConfigure.Enabled = false;
			this.btnSearch.Enabled = false;
			Thread.Sleep(100);
			this.Refresh();
			WGPacket wGPacket = new WGPacket();
			wGPacket.type = 36;
			wGPacket.code = 16;
			wGPacket.iDevSnFrom = 0u;
			wGPacket.iCallReturn = 0;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			num2 = num + 100u - 1u;
			this.wgudp = null;
			num = num % 100000000u + 100000000u;
			uint num3 = num % 100000000u;
			num2 = num2 % 100000000u + 100000000u;
			while (num <= num2 || num2 < 400000000u)
			{
				if (num > num2)
				{
					if (num < 200000000u)
					{
						num = num3 + 200000000u;
						num2 = num2 % 100000000u + 200000000u;
					}
					else
					{
						num = num3 + 400000000u;
						num2 = num2 % 100000000u + 400000000u;
					}
				}
				if ((num2 - num) % 5u == 0u)
				{
					this.lblSearchNow.Text = num.ToString();
					this.toolStripStatusLabel2.Text = num.ToString();
					this.lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString();
					this.toolStripStatusLabel1.Text = this.dgvFoundControllers.Rows.Count.ToString();
					this.Refresh();
					Application.DoEvents();
					Cursor.Current = Cursors.WaitCursor;
				}
				wGPacket.iDevSnTo = num;
				num += 1u;
				if (WGPacket.bCommP)
				{
					WGPacket.bCommP = false;
					string commPStr = wgTools.CommPStr;
					wgTools.CommPStr = "";
					NetworkInterface[] array = allNetworkInterfaces;
					for (int i = 0; i < array.Length; i++)
					{
						NetworkInterface networkInterface = array[i];
						IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
						UnicastIPAddressInformationCollection unicastAddresses = iPProperties.UnicastAddresses;
						if (unicastAddresses.Count > 0)
						{
							Console.WriteLine(networkInterface.Description);
							foreach (UnicastIPAddressInformation current in unicastAddresses)
							{
								if (!current.Address.IsIPv6LinkLocal && !(current.Address.ToString() == "127.0.0.1"))
								{
									Console.WriteLine("  IP ............................. : {0}", current.Address.ToString());
									if (this.wgudp == null)
									{
										this.wgudp = new wgUdpComm(current.Address);
										Thread.Sleep(300);
									}
									else if (this.wgudp.localIP.ToString() != current.Address.ToString())
									{
										this.wgudp = new wgUdpComm(current.Address);
										Thread.Sleep(300);
									}
									byte[] array2 = wGPacket.ToBytes(this.wgudp.udpPort);
									if (array2 == null)
									{
										return;
									}
									byte[] array3 = null;
									this.wgudp.udp_get(array2, 300, 0u, null, 60000, ref array3);
									if (array3 != null && !this.isExisted(wGPacket.iDevSnTo.ToString(), this.wgudp.localIP.ToString()))
									{
										this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
										{
											array3,
											current.Address.ToString()
										});
										long ticks = DateTime.Now.Ticks;
									}
								}
							}
							Console.WriteLine();
						}
					}
					wgTools.CommPStr = commPStr;
					WGPacket.bCommP = true;
				}
				NetworkInterface[] array4 = allNetworkInterfaces;
				for (int j = 0; j < array4.Length; j++)
				{
					NetworkInterface networkInterface2 = array4[j];
					IPInterfaceProperties iPProperties2 = networkInterface2.GetIPProperties();
					UnicastIPAddressInformationCollection unicastAddresses2 = iPProperties2.UnicastAddresses;
					if (unicastAddresses2.Count > 0)
					{
						Console.WriteLine(networkInterface2.Description);
						foreach (UnicastIPAddressInformation current2 in unicastAddresses2)
						{
							if (!current2.Address.IsIPv6LinkLocal && !(current2.Address.ToString() == "127.0.0.1"))
							{
								Console.WriteLine("  IP ............................. : {0}", current2.Address.ToString());
								if (this.wgudp == null)
								{
									this.wgudp = new wgUdpComm(current2.Address);
									Thread.Sleep(300);
								}
								else if (this.wgudp.localIP.ToString() != current2.Address.ToString())
								{
									this.wgudp = new wgUdpComm(current2.Address);
									Thread.Sleep(300);
								}
								byte[] array5 = wGPacket.ToBytes(this.wgudp.udpPort);
								if (array5 == null)
								{
									return;
								}
								byte[] array6 = null;
								this.wgudp.udp_get(array5, 300, 4294967295u, null, 60000, ref array6);
								if (array6 != null && !this.isExisted(wGPacket.iDevSnTo.ToString(), this.wgudp.localIP.ToString()))
								{
									this.dgvFoundControllers.Invoke(new dfrmNetControllerConfig.AddTolstDiscoveredDevices(this.AddDiscoveryEntry), new object[]
									{
										array6,
										current2.Address.ToString()
									});
								}
							}
						}
						Console.WriteLine();
					}
				}
			}
			this.btnSearch.Enabled = true;
			if (this.dgvFoundControllers.Rows.Count > 0)
			{
				this.btnConfigure.Enabled = true;
			}
			this.lblSearchNow.Text = num2.ToString();
			this.toolStripStatusLabel2.Text = num2.ToString();
			this.lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString();
			this.toolStripStatusLabel1.Text = this.dgvFoundControllers.Rows.Count.ToString();
			Cursor.Current = Cursors.Default;
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			if (XMessageBox.Show(this, string.Format(CommonStr.strAreYouSure + " {0}?", this.btnDefault.Text), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
			{
				return;
			}
			string text = "-1";
			string text2 = "";
			string text3 = "192.168.0.0";
			string text4 = "255.255.255.0";
			string text5 = "";
			string text6 = "60000";
			string text7 = this.btnDefault.Text;
			this.IPConfigureCPU(text, text2, text3, text4, text5, text6, "");
			wgAppConfig.wgLog(string.Concat(new string[]
			{
				text7,
				"  SN=",
				text,
				", Mac=",
				text2,
				",IP =",
				text3,
				",Mask=",
				text4,
				",Gateway=",
				text5,
				", Port = ",
				text6,
				", PC IPAddr="
			}));
		}

		private void funCtrlShiftQ()
		{
			string text = null;
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.setPasswordChar('*');
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				text = dfrmInputNewName.strNewName;
			}
			if (!string.IsNullOrEmpty(text))
			{
				text = text.ToUpper();
				if (text == "WGTEST" + (DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour).ToString())
				{
					this.dfrmTest = new frmTestController();
					this.dfrmTest.Show();
					return;
				}
				string key;
				switch (key = text)
				{
				case "5678":
					this.btnDefault.Visible = true;
					this.restoreDefaultParamToolStripMenuItem.Visible = true;
					this.restoreAllSwipesToolStripMenuItem.Visible = true;
					this.clearSwipesToolStripMenuItem.Visible = true;
					return;
				case "IP":
					this.btnDefault.Visible = true;
					return;
				case "WEB":
					this.btnIPAndWebConfigure.Visible = true;
					return;
				case "PARAM":
					this.restoreDefaultParamToolStripMenuItem.Visible = true;
					return;
				case "RECORD":
					this.restoreAllSwipesToolStripMenuItem.Visible = true;
					return;
				case "FORMAT5678":
					this.formatToolStripMenuItem.Visible = true;
					return;
				case "CSN":
				case "CSQ":
					this.dfrmTest = new frmTestController();
					this.dfrmTest.Show();
					return;
				case "P":
					this.frmProductFormat1 = new frmProductFormat();
					this.frmProductFormat1.Show();
					return;
				case "ENC":
				{
					if (string.IsNullOrEmpty(wgAppConfig.GetKeyVal("dbConnection")))
					{
						return;
					}
					wgAppConfig.UpdateKeyVal("dbConnection", dfrmNetControllerConfig.encDbConnection(wgAppConfig.dbConString));
					wgAppConfig.runUpdateSql("Delete From t_s_wglog");
					FileInfo fileInfo = new FileInfo(Application.StartupPath + "\\n3k_log.log");
					if (fileInfo.Exists)
					{
						fileInfo.Delete();
					}
					wgAppConfig.wgLog("Encrypt DB Connection");
					XMessageBox.Show("OK");
					return;
				}
				case "DES":
					XMessageBox.Show(wgAppConfig.dbConString);
					break;

					return;
				}
				return;
			}
		}

		public static string encDbConnection(string strDbConnection)
		{
			string result = "";
			try
			{
				if (!string.IsNullOrEmpty(strDbConnection))
				{
					result = "ENC" + WGPacket.Ept(strDbConnection);
				}
			}
			catch
			{
			}
			return result;
		}

		private void restoreDefaultParamToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
				return;
			}
			using (icController icController = new icController())
			{
				DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
				icController.ControllerSN = int.Parse(dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
				wgMjControllerConfigure wgMjControllerConfigure = new wgMjControllerConfigure();
				wgMjControllerConfigure.RestoreDefault();
				icController.UpdateConfigureIP(wgMjControllerConfigure);
				wgAppConfig.wgLog(sender.ToString() + "  SN=" + icController.ControllerSN);
				XMessageBox.Show(string.Format("{0}: {1} -- {2}\r\n{3}", new object[]
				{
					icController.ControllerSN,
					sender.ToString(),
					CommonStr.strSuccessfully,
					CommonStr.strRebootController
				}));
			}
		}

		private void restoreAllSwipesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
				return;
			}
			using (icController icController = new icController())
			{
				DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
				icController.ControllerSN = int.Parse(dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
				icController.RestoreAllSwipeInTheControllersIP();
				wgAppConfig.wgLog(sender.ToString() + "  SN=" + icController.ControllerSN);
				XMessageBox.Show(string.Format("{0}: {1} -- {2}\r\n{3}", new object[]
				{
					icController.ControllerSN,
					sender.ToString(),
					CommonStr.strSuccessfully,
					CommonStr.strRebootController
				}));
			}
		}

		private void formatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
				return;
			}
			using (icController icController = new icController())
			{
				DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
				icController.ControllerSN = int.Parse(dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
				byte[] array = new byte[1152];
				array[1027] = 165;
				array[1026] = 165;
				array[1025] = 165;
				array[1024] = 165;
				icController.UpdateConfigureSuperIP(array);
				wgAppConfig.wgLog(sender.ToString() + "  SN=" + icController.ControllerSN);
				XMessageBox.Show(string.Format("{0}: {1} -- {2}\r\n{3}", new object[]
				{
					icController.ControllerSN,
					sender.ToString(),
					CommonStr.strSuccessfully,
					CommonStr.strRebootController
				}));
			}
		}

		private void btnIPAndWebConfigure_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
				return;
			}
			string text;
			string strSN;
			string strMac;
			string strIP;
			string strMask;
			string strGateway;
			string strTCPPort;
			string text2;
			using (dfrmTCPIPWEBConfigure dfrmTCPIPWEBConfigure = new dfrmTCPIPWEBConfigure())
			{
				DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
				dfrmTCPIPWEBConfigure.strSN = dataGridViewRow.Cells["f_ControllerSN"].Value.ToString();
				dfrmTCPIPWEBConfigure.strMac = dataGridViewRow.Cells["f_MACAddr"].Value.ToString();
				dfrmTCPIPWEBConfigure.strIP = dataGridViewRow.Cells["f_IP"].Value.ToString();
				dfrmTCPIPWEBConfigure.strMask = dataGridViewRow.Cells["f_Mask"].Value.ToString();
				dfrmTCPIPWEBConfigure.strGateway = dataGridViewRow.Cells["f_Gateway"].Value.ToString();
				dfrmTCPIPWEBConfigure.strTCPPort = dataGridViewRow.Cells["f_PORT"].Value.ToString();
				text = dataGridViewRow.Cells["f_PCIPAddr"].Value.ToString();
				dfrmTCPIPWEBConfigure.strPCAddress = text;
				dfrmTCPIPWEBConfigure.strSearchedIP = dataGridViewRow.Cells["f_IP"].Value.ToString();
				dfrmTCPIPWEBConfigure.strSearchedMask = dataGridViewRow.Cells["f_Mask"].Value.ToString();
				if (wgAppConfig.IsChineseSet(wgAppConfig.CultureInfoStr))
				{
					dfrmTCPIPWEBConfigure.cboLanguage.SelectedIndex = 0;
					dfrmTCPIPWEBConfigure.cboLanguage2.SelectedIndex = 0;
				}
				else
				{
					dfrmTCPIPWEBConfigure.cboLanguage.SelectedIndex = 1;
					dfrmTCPIPWEBConfigure.cboLanguage2.SelectedIndex = 1;
				}
				dfrmTCPIPWEBConfigure.cboDateFormat.SelectedIndex = 0;
				if (this.bIPAndWEBConfigure)
				{
					dfrmTCPIPWEBConfigure.chkEditIP.Checked = this.bUpdateIPConfigure;
					dfrmTCPIPWEBConfigure.grpIP.Enabled = this.bUpdateIPConfigure;
					this.bOption = (this.bOption || this.commPort != 60000);
					dfrmTCPIPWEBConfigure.btnOption.Enabled = !this.bOption;
					dfrmTCPIPWEBConfigure.lblPort.Visible = this.bOption;
					dfrmTCPIPWEBConfigure.nudPort.Visible = this.bOption;
					dfrmTCPIPWEBConfigure.nudPort.Value = decimal.Parse(this.commPort.ToString());
					dfrmTCPIPWEBConfigure.chkUpdateWebSet.Checked = this.bUpdateWEBConfigure;
					dfrmTCPIPWEBConfigure.grpWEBEnabled.Enabled = this.bUpdateWEBConfigure;
					dfrmTCPIPWEBConfigure.grpWEB.Enabled = (this.bUpdateWEBConfigure && this.bWEBEnabled);
					dfrmTCPIPWEBConfigure.optWEBEnabled.Checked = this.bWEBEnabled;
					dfrmTCPIPWEBConfigure.cboLanguage.SelectedIndex = int.Parse(this.strWEBLanguage1);
					dfrmTCPIPWEBConfigure.txtSelectedFileName.Text = this.strSelectedFile1;
					this.bOptionWeb = (this.bOptionWeb || this.HttpPort != 80);
					dfrmTCPIPWEBConfigure.btnOptionWEB.Enabled = !this.bOptionWeb;
					dfrmTCPIPWEBConfigure.lblHttpPort.Visible = this.bOptionWeb;
					dfrmTCPIPWEBConfigure.nudHttpPort.Visible = this.bOptionWeb;
					dfrmTCPIPWEBConfigure.nudHttpPort.Value = decimal.Parse(this.HttpPort.ToString());
					dfrmTCPIPWEBConfigure.cboDateFormat.SelectedIndex = this.webDateFormat;
					dfrmTCPIPWEBConfigure.chkAdjustTime.Checked = this.bAdjustTime;
					dfrmTCPIPWEBConfigure.chkWebOnlyQuery.Checked = this.bWebOnlyQuery;
					dfrmTCPIPWEBConfigure.chkUpdateSuperCard.Checked = this.bUpdateSuperCard_IPWEB;
					dfrmTCPIPWEBConfigure.grpSuperCards.Enabled = this.bUpdateSuperCard_IPWEB;
					dfrmTCPIPWEBConfigure.txtSuperCard1.Text = this.superCard1_IPWEB;
					dfrmTCPIPWEBConfigure.txtSuperCard2.Text = this.superCard2_IPWEB;
					dfrmTCPIPWEBConfigure.chkUpdateSpecialCard.Checked = this.bUpdateSpecialCard_IPWEB;
					dfrmTCPIPWEBConfigure.chkUpdateSpecialCard.Visible = this.bUpdateSpecialCard_IPWEB;
					dfrmTCPIPWEBConfigure.grpSpecialCards.Enabled = this.bUpdateSpecialCard_IPWEB;
					dfrmTCPIPWEBConfigure.grpSpecialCards.Visible = this.bUpdateSpecialCard_IPWEB;
					dfrmTCPIPWEBConfigure.txtSpecialCard1.Text = this.SpecialCard1_IPWEB;
					dfrmTCPIPWEBConfigure.txtSpecialCard2.Text = this.SpecialCard2_IPWEB;
					dfrmTCPIPWEBConfigure.cboLanguage2.SelectedIndex = int.Parse(this.strWEBLanguage2);
					dfrmTCPIPWEBConfigure.txtUsersFile.Text = this.strSelectedFile2;
					dfrmTCPIPWEBConfigure.chkAutoUploadWEBUsers.Checked = this.bAutoUploadUsers;
					dfrmTCPIPWEBConfigure.chkAutoUploadWEBUsers.Visible = this.bAutoUploadUsers;
					if (dfrmTCPIPWEBConfigure.strIP == "192.168.0.0")
					{
						dfrmTCPIPWEBConfigure.strIP = this.strIP_IPWEB;
						dfrmTCPIPWEBConfigure.strMask = this.strNETMASK_IPWEB;
						dfrmTCPIPWEBConfigure.strGateway = this.strGateway_IPWEB;
					}
				}
				if (dfrmTCPIPWEBConfigure.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				this.bIPAndWEBConfigure = true;
				this.bUpdateIPConfigure = dfrmTCPIPWEBConfigure.chkEditIP.Checked;
				this.commPort = int.Parse(dfrmTCPIPWEBConfigure.nudPort.Value.ToString());
				this.bOption = (this.commPort != 60000);
				this.bUpdateWEBConfigure = dfrmTCPIPWEBConfigure.chkUpdateWebSet.Checked;
				this.bWEBEnabled = dfrmTCPIPWEBConfigure.optWEBEnabled.Checked;
				this.strWEBLanguage1 = dfrmTCPIPWEBConfigure.cboLanguage.SelectedIndex.ToString();
				this.strSelectedFile1 = dfrmTCPIPWEBConfigure.txtSelectedFileName.Text;
				this.HttpPort = int.Parse(dfrmTCPIPWEBConfigure.nudHttpPort.Value.ToString());
				this.bOptionWeb = (this.HttpPort != 80);
				this.bAdjustTime = dfrmTCPIPWEBConfigure.chkAdjustTime.Checked;
				this.webDateFormat = dfrmTCPIPWEBConfigure.cboDateFormat.SelectedIndex;
				this.bWebOnlyQuery = dfrmTCPIPWEBConfigure.chkWebOnlyQuery.Checked;
				this.bUpdateSuperCard_IPWEB = dfrmTCPIPWEBConfigure.chkUpdateSuperCard.Checked;
				this.superCard1_IPWEB = dfrmTCPIPWEBConfigure.txtSuperCard1.Text;
				this.superCard2_IPWEB = dfrmTCPIPWEBConfigure.txtSuperCard2.Text;
				this.bUpdateSpecialCard_IPWEB = dfrmTCPIPWEBConfigure.chkUpdateSpecialCard.Checked;
				this.SpecialCard1_IPWEB = dfrmTCPIPWEBConfigure.txtSpecialCard1.Text;
				this.SpecialCard2_IPWEB = dfrmTCPIPWEBConfigure.txtSpecialCard2.Text;
				this.strWEBLanguage2 = dfrmTCPIPWEBConfigure.cboLanguage2.SelectedIndex.ToString();
				this.strSelectedFile2 = dfrmTCPIPWEBConfigure.txtUsersFile.Text;
				this.bAutoUploadUsers = dfrmTCPIPWEBConfigure.chkAutoUploadWEBUsers.Checked;
				this.strIP_IPWEB = dfrmTCPIPWEBConfigure.strIP;
				this.strNETMASK_IPWEB = dfrmTCPIPWEBConfigure.strMask;
				this.strGateway_IPWEB = dfrmTCPIPWEBConfigure.strGateway;
				if (dfrmTCPIPWEBConfigure.dtWebString != null)
				{
					this.dtWebStringAdvanced_IPWEB = dfrmTCPIPWEBConfigure.dtWebString.Copy();
				}
				else
				{
					this.dtWebStringAdvanced_IPWEB = null;
				}
				wgAppConfig.wgLog((sender as Button).Text + "  SN=" + dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
				strSN = dfrmTCPIPWEBConfigure.strSN;
				strMac = dfrmTCPIPWEBConfigure.strMac;
				strIP = dfrmTCPIPWEBConfigure.strIP;
				strMask = dfrmTCPIPWEBConfigure.strMask;
				strGateway = dfrmTCPIPWEBConfigure.strGateway;
				strTCPPort = dfrmTCPIPWEBConfigure.strTCPPort;
				text2 = dfrmTCPIPWEBConfigure.Text;
			}
			try
			{
				this.Refresh();
				if (string.IsNullOrEmpty(strSN))
				{
					return;
				}
				Cursor.Current = Cursors.WaitCursor;
				if (this.bUpdateWEBConfigure || this.bUpdateSpecialCard_IPWEB || this.bUpdateSuperCard_IPWEB)
				{
					this.ipweb_webSet();
				}
				if (this.bAdjustTime)
				{
					using (icController icController = new icController())
					{
						icController.ControllerSN = int.Parse(strSN);
						if (icController.AdjustTimeIP(DateTime.Now, text) < 0)
						{
							XMessageBox.Show(CommonStr.strAdjustTime + " " + CommonStr.strFailed);
							return;
						}
						wgAppConfig.wgLog(strSN + " " + string.Format("{0}:{1}", CommonStr.strAdjustTimeOK, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
					}
				}
				if (this.bAutoUploadUsers)
				{
					Cursor.Current = Cursors.WaitCursor;
					DataGridViewRow dataGridViewRow2 = this.dgvFoundControllers.SelectedRows[0];
					string lang = "utf-8";
					this.ipweb_uploadusers(this.strSelectedFile2, int.Parse(dataGridViewRow2.Cells["f_ControllerSN"].Value.ToString()), lang);
				}
				if (this.bUpdateIPConfigure)
				{
					Cursor.Current = Cursors.WaitCursor;
					this.IPConfigureCPU(strSN, strMac, strIP, strMask, strGateway, strTCPPort, text);
					wgAppConfig.wgLog(string.Concat(new string[]
					{
						text2,
						"  SN=",
						strSN,
						", Mac=",
						strMac,
						",IP =",
						strIP,
						",Mask=",
						strMask,
						",Gateway=",
						strGateway,
						", Port = ",
						strTCPPort,
						", PC IPAddr=",
						text
					}));
				}
				else if (this.bUpdateWEBConfigure)
				{
					using (icController icController2 = new icController())
					{
						icController2.ControllerSN = int.Parse(strSN);
						icController2.RebootControllerIP();
					}
				}
			}
			catch (Exception)
			{
			}
			Cursor.Current = Cursors.Default;
		}

		private void ipweb_webSet()
		{
			DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
			int num = int.Parse(dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
			string text = dataGridViewRow.Cells["f_PCIPAddr"].Value.ToString();
			byte[] array = new byte[1152];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			if (this.bUpdateSuperCard_IPWEB)
			{
				ulong num2 = 18446744073709551615uL;
				ulong num3 = 18446744073709551615uL;
				ulong.TryParse(this.superCard1_IPWEB, out num2);
				ulong.TryParse(this.superCard2_IPWEB, out num3);
				if (num2 == 0uL)
				{
					num2 = 18446744073709551615uL;
				}
				if (num3 == 0uL)
				{
					num3 = 18446744073709551615uL;
				}
				wgAppConfig.wgLog("  SN=" + num.ToString() + string.Format("  Super Card1={0},Card2={1}", num2.ToString(), num3.ToString()));
				int num4 = 144;
				array[num4] = (byte)(num2 & 255uL);
				byte[] expr_105_cp_0 = array;
				int expr_105_cp_1 = 1024 + (num4 >> 3);
				expr_105_cp_0[expr_105_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 8);
				byte[] expr_13B_cp_0 = array;
				int expr_13B_cp_1 = 1024 + (num4 >> 3);
				expr_13B_cp_0[expr_13B_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 16);
				byte[] expr_172_cp_0 = array;
				int expr_172_cp_1 = 1024 + (num4 >> 3);
				expr_172_cp_0[expr_172_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 24);
				byte[] expr_1A9_cp_0 = array;
				int expr_1A9_cp_1 = 1024 + (num4 >> 3);
				expr_1A9_cp_0[expr_1A9_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 32);
				byte[] expr_1E0_cp_0 = array;
				int expr_1E0_cp_1 = 1024 + (num4 >> 3);
				expr_1E0_cp_0[expr_1E0_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 40);
				byte[] expr_217_cp_0 = array;
				int expr_217_cp_1 = 1024 + (num4 >> 3);
				expr_217_cp_0[expr_217_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 48);
				byte[] expr_24E_cp_0 = array;
				int expr_24E_cp_1 = 1024 + (num4 >> 3);
				expr_24E_cp_0[expr_24E_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num2 >> 56);
				byte[] expr_285_cp_0 = array;
				int expr_285_cp_1 = 1024 + (num4 >> 3);
				expr_285_cp_0[expr_285_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 & 255uL);
				byte[] expr_2C0_cp_0 = array;
				int expr_2C0_cp_1 = 1024 + (num4 >> 3);
				expr_2C0_cp_0[expr_2C0_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 8);
				byte[] expr_2F6_cp_0 = array;
				int expr_2F6_cp_1 = 1024 + (num4 >> 3);
				expr_2F6_cp_0[expr_2F6_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 16);
				byte[] expr_32D_cp_0 = array;
				int expr_32D_cp_1 = 1024 + (num4 >> 3);
				expr_32D_cp_0[expr_32D_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 24);
				byte[] expr_364_cp_0 = array;
				int expr_364_cp_1 = 1024 + (num4 >> 3);
				expr_364_cp_0[expr_364_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 32);
				byte[] expr_39B_cp_0 = array;
				int expr_39B_cp_1 = 1024 + (num4 >> 3);
				expr_39B_cp_0[expr_39B_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 40);
				byte[] expr_3D2_cp_0 = array;
				int expr_3D2_cp_1 = 1024 + (num4 >> 3);
				expr_3D2_cp_0[expr_3D2_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 48);
				byte[] expr_409_cp_0 = array;
				int expr_409_cp_1 = 1024 + (num4 >> 3);
				expr_409_cp_0[expr_409_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num3 >> 56);
				byte[] expr_440_cp_0 = array;
				int expr_440_cp_1 = 1024 + (num4 >> 3);
				expr_440_cp_0[expr_440_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
			}
			if (this.bUpdateSpecialCard_IPWEB)
			{
				ulong num5 = 18446744073709551615uL;
				ulong num6 = 18446744073709551615uL;
				ulong.TryParse(this.SpecialCard1_IPWEB, out num5);
				ulong.TryParse(this.SpecialCard2_IPWEB, out num6);
				wgAppConfig.wgLog("  SN=" + num.ToString() + string.Format("  Special Card1={0},Card2={1}", num5.ToString(), num6.ToString()));
				if (num5 == 0uL)
				{
					num5 = 18446744073709551615uL;
				}
				if (num6 == 0uL)
				{
					num6 = 18446744073709551615uL;
				}
				int num4 = 160;
				array[num4] = (byte)(num5 & 255uL);
				byte[] expr_4F3_cp_0 = array;
				int expr_4F3_cp_1 = 1024 + (num4 >> 3);
				expr_4F3_cp_0[expr_4F3_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 8);
				byte[] expr_529_cp_0 = array;
				int expr_529_cp_1 = 1024 + (num4 >> 3);
				expr_529_cp_0[expr_529_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 16);
				byte[] expr_560_cp_0 = array;
				int expr_560_cp_1 = 1024 + (num4 >> 3);
				expr_560_cp_0[expr_560_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 24);
				byte[] expr_597_cp_0 = array;
				int expr_597_cp_1 = 1024 + (num4 >> 3);
				expr_597_cp_0[expr_597_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 32);
				byte[] expr_5CE_cp_0 = array;
				int expr_5CE_cp_1 = 1024 + (num4 >> 3);
				expr_5CE_cp_0[expr_5CE_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 40);
				byte[] expr_605_cp_0 = array;
				int expr_605_cp_1 = 1024 + (num4 >> 3);
				expr_605_cp_0[expr_605_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 48);
				byte[] expr_63C_cp_0 = array;
				int expr_63C_cp_1 = 1024 + (num4 >> 3);
				expr_63C_cp_0[expr_63C_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num5 >> 56);
				byte[] expr_673_cp_0 = array;
				int expr_673_cp_1 = 1024 + (num4 >> 3);
				expr_673_cp_0[expr_673_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 & 255uL);
				byte[] expr_6AE_cp_0 = array;
				int expr_6AE_cp_1 = 1024 + (num4 >> 3);
				expr_6AE_cp_0[expr_6AE_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 8);
				byte[] expr_6E4_cp_0 = array;
				int expr_6E4_cp_1 = 1024 + (num4 >> 3);
				expr_6E4_cp_0[expr_6E4_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 16);
				byte[] expr_71B_cp_0 = array;
				int expr_71B_cp_1 = 1024 + (num4 >> 3);
				expr_71B_cp_0[expr_71B_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 24);
				byte[] expr_752_cp_0 = array;
				int expr_752_cp_1 = 1024 + (num4 >> 3);
				expr_752_cp_0[expr_752_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 32);
				byte[] expr_789_cp_0 = array;
				int expr_789_cp_1 = 1024 + (num4 >> 3);
				expr_789_cp_0[expr_789_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 40);
				byte[] expr_7C0_cp_0 = array;
				int expr_7C0_cp_1 = 1024 + (num4 >> 3);
				expr_7C0_cp_0[expr_7C0_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 48);
				byte[] expr_7F7_cp_0 = array;
				int expr_7F7_cp_1 = 1024 + (num4 >> 3);
				expr_7F7_cp_0[expr_7F7_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num6 >> 56);
				byte[] expr_82E_cp_0 = array;
				int expr_82E_cp_1 = 1024 + (num4 >> 3);
				expr_82E_cp_0[expr_82E_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
			}
			if (this.bUpdateWEBConfigure)
			{
				int num7 = 12288;
				if (wgAppConfig.IsChineseSet(wgAppConfig.CultureInfoStr))
				{
					num7 = 8192;
				}
				int num8;
				if (!this.bWEBEnabled)
				{
					num8 = 0;
				}
				else
				{
					num8 = this.HttpPort;
					switch (int.Parse(this.strWEBLanguage1))
					{
					case 0:
						num7 = 8192;
						break;
					case 1:
						num7 = 12288;
						break;
					case 2:
						num7 = 229376;
						break;
					default:
						num7 = 12288;
						break;
					}
				}
				int num4 = 100;
				array[num4] = (byte)(num7 & 255);
				byte[] expr_8EE_cp_0 = array;
				int expr_8EE_cp_1 = 1024 + (num4 >> 3);
				expr_8EE_cp_0[expr_8EE_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num7 >> 8);
				byte[] expr_924_cp_0 = array;
				int expr_924_cp_1 = 1024 + (num4 >> 3);
				expr_924_cp_0[expr_924_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num7 >> 16);
				byte[] expr_95B_cp_0 = array;
				int expr_95B_cp_1 = 1024 + (num4 >> 3);
				expr_95B_cp_0[expr_95B_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num7 >> 24);
				byte[] expr_992_cp_0 = array;
				int expr_992_cp_1 = 1024 + (num4 >> 3);
				expr_992_cp_0[expr_992_cp_1] |= (byte)(1 << (num4 & 7));
				num4 = 96;
				array[num4] = (byte)(num8 & 255);
				byte[] expr_9CA_cp_0 = array;
				int expr_9CA_cp_1 = 1024 + (num4 >> 3);
				expr_9CA_cp_0[expr_9CA_cp_1] |= (byte)(1 << (num4 & 7));
				num4++;
				array[num4] = (byte)(num8 >> 8);
				byte[] expr_A00_cp_0 = array;
				int expr_A00_cp_1 = 1024 + (num4 >> 3);
				expr_A00_cp_0[expr_A00_cp_1] |= (byte)(1 << (num4 & 7));
				num4 = 98;
				array[num4] = (byte)(this.webDateFormat & 255);
				byte[] expr_A3C_cp_0 = array;
				int expr_A3C_cp_1 = 1024 + (num4 >> 3);
				expr_A3C_cp_0[expr_A3C_cp_1] |= (byte)(1 << (num4 & 7));
				num4 = 99;
				array[num4] = (byte)((this.bWebOnlyQuery ? 165 : 0) & 255);
				byte[] expr_A82_cp_0 = array;
				int expr_A82_cp_1 = 1024 + (num4 >> 3);
				expr_A82_cp_0[expr_A82_cp_1] |= (byte)(1 << (num4 & 7));
			}
			if (this.bUpdateSuperCard_IPWEB || this.bUpdateWEBConfigure || this.bUpdateSpecialCard_IPWEB)
			{
				using (icController icController = new icController())
				{
					icController.ControllerSN = num;
					icController.UpdateConfigureCPUSuperIP(array, "", text);
					wgAppConfig.wgLog("  SN=" + icController.ControllerSN + string.Format(" WEB Language={0}", this.strWEBLanguage1.ToString()));
				}
			}
			wgAppConfig.wgLog(this.btnIPAndWebConfigure.Text + "  SN=" + num);
			if (this.bUpdateWEBConfigure && this.bWEBEnabled && int.Parse(this.strWEBLanguage1) == 2)
			{
				byte[] array2 = new byte[4096];
				byte b = 0;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = b;
				}
				int num9 = 229376;
				int num10 = 230400;
				this.dv = new DataView(this.dtWebStringAdvanced_IPWEB);
				string name = this.dv[0][2].ToString();
				for (int k = 0; k <= this.dv.Count - 1; k++)
				{
					array2[num9 - 229376] = (byte)(num10 & 255);
					array2[num9 - 229376 + 1] = (byte)(num10 >> 8);
					array2[num9 - 229376 + 2] = (byte)(num10 >> 16);
					array2[num9 - 229376 + 3] = (byte)(num10 >> 24);
					num9 += 4;
					string s = wgTools.SetObjToStr(this.dv[k][2]).Trim();
					byte[] bytes = Encoding.GetEncoding(name).GetBytes(s);
					num10 = num10 + bytes.Length + 1;
				}
				num9 = 230400;
				for (int l = 0; l <= this.dv.Count - 1; l++)
				{
					string s = wgTools.SetObjToStr(this.dv[l][2]).Trim();
					byte[] bytes2 = Encoding.GetEncoding(name).GetBytes(s);
					for (int m = 0; m < bytes2.Length; m++)
					{
						array2[num9 - 229376 + m] = bytes2[m];
					}
					num9 = num9 + bytes2.Length + 1;
				}
				wgUdpComm wgUdpComm = null;
				try
				{
					WGPacketSSI_FLASH wGPacketSSI_FLASH = new WGPacketSSI_FLASH();
					wGPacketSSI_FLASH.type = 33;
					wGPacketSSI_FLASH.code = 48;
					wGPacketSSI_FLASH.iDevSnFrom = 0u;
					wGPacketSSI_FLASH.iDevSnTo = (uint)num;
					wGPacketSSI_FLASH.iCallReturn = 0;
					wGPacketSSI_FLASH.ucData = new byte[1024];
					IPAddress iPAddress;
					if (IPAddress.TryParse(text, out iPAddress))
					{
						wgUdpComm = new wgUdpComm(IPAddress.Parse(text));
					}
					else
					{
						wgUdpComm = new wgUdpComm();
					}
					Thread.Sleep(300);
					wGPacketSSI_FLASH.iStartFlashAddr = 8331264u;
					wGPacketSSI_FLASH.iEndFlashAddr = 8335359u;
					for (int n = 0; n < 1024; n++)
					{
						wGPacketSSI_FLASH.ucData[n] = 255;
					}
					byte[] array3 = null;
					while (wGPacketSSI_FLASH.iStartFlashAddr <= wGPacketSSI_FLASH.iEndFlashAddr)
					{
						for (int num11 = 0; num11 < 1024; num11++)
						{
							wGPacketSSI_FLASH.ucData[num11] = array2[(int)(checked((IntPtr)(unchecked((ulong)(wGPacketSSI_FLASH.iStartFlashAddr - 8331264u) + (ulong)((long)num11)))))];
						}
						wgUdpComm.udp_get_notries(wGPacketSSI_FLASH.ToBytes(wgUdpComm.udpPort), 300, wGPacketSSI_FLASH.xid, null, 60000, ref array3);
						if (array3 == null)
						{
							wgTools.WriteLine(string.Format("没有收到数据就退出={0:d}******************", 1));
						}
						wGPacketSSI_FLASH.iStartFlashAddr += 1024u;
					}
					wgUdpComm.Close();
					wgAppConfig.wgLog(this.btnIPAndWebConfigure.Text + "  SN=" + num.ToString() + "  OtherLanguage");
				}
				catch (Exception)
				{
				}
				finally
				{
					if (wgUdpComm != null)
					{
						wgUdpComm.Dispose();
					}
				}
			}
		}

		private int ipweb_uploadusers(string userFile, int controllerSN, string lang)
		{
			if (!File.Exists(userFile))
			{
				return 0;
			}
			this.tb = new DataTable();
			this.tb.TableName = wgAppConfig.dbWEBUserName;
			this.tb.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
			this.tb.Columns.Add("f_ConsumerName");
			this.tb.ReadXml(userFile);
			this.tb.AcceptChanges();
			this.dv = new DataView(this.tb);
			this.dv.Sort = "f_CardNO ASC";
			string pCIPAddr = null;
			using (wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege())
			{
				wgMjControllerPrivilege.AllowUpload();
				if (this.dtPrivilege != null)
				{
					this.dtPrivilege.Dispose();
					GC.Collect();
				}
				if (this.dtPrivilege == null)
				{
					this.dtPrivilege = new DataTable("Privilege");
					this.dtPrivilege.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
					this.dtPrivilege.Columns.Add("f_BeginYMD", Type.GetType("System.DateTime"));
					this.dtPrivilege.Columns.Add("f_EndYMD", Type.GetType("System.DateTime"));
					this.dtPrivilege.Columns.Add("f_PIN", Type.GetType("System.String"));
					this.dtPrivilege.Columns.Add("f_ControlSegID1", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID1"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID2", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID2"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID3", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID3"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID4", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID4"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ConsumerName", Type.GetType("System.String"));
				}
				uint num = 0u;
				for (int i = 0; i < this.dv.Count; i++)
				{
					DataRow dataRow = this.dtPrivilege.NewRow();
					dataRow["f_CardNO"] = (uint)this.dv[i]["f_CardNO"];
					dataRow["f_BeginYMD"] = DateTime.Parse("2011-1-1");
					dataRow["f_EndYMD"] = DateTime.Parse("2029-12-31");
					dataRow["f_PIN"] = 0;
					dataRow["f_ControlSegID1"] = 1;
					dataRow["f_ControlSegID2"] = 1;
					dataRow["f_ControlSegID3"] = 1;
					dataRow["f_ControlSegID4"] = 1;
					dataRow["f_ConsumerName"] = this.dv[i]["f_ConsumerName"];
					if ((uint)dataRow["f_CardNO"] <= num)
					{
						XMessageBox.Show(CommonStr.strFailed);
						int result = 0;
						return result;
					}
					num = (uint)dataRow["f_CardNO"];
					this.dtPrivilege.Rows.Add(dataRow);
				}
				this.dtPrivilege.AcceptChanges();
				wgMjControllerPrivilege.bAllowUploadUserName = true;
				if (wgMjControllerPrivilege.UploadIP(controllerSN, null, 60000, "DOOR NAME", this.dtPrivilege, pCIPAddr) < 0)
				{
					XMessageBox.Show(CommonStr.strFailed);
					int result = 0;
					return result;
				}
			}
			return 1;
		}

		private void dfrmNetControllerConfig_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void communicationTestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.Rows.Count > 0 && this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			using (icController icController = new icController())
			{
				icController.ControllerSN = -1;
				if (this.dgvFoundControllers.Rows.Count > 0)
				{
					DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
					icController.ControllerSN = int.Parse(dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
				}
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				wgTools.WriteLine("control.SpecialPingIP Start");
				for (int i = 0; i < 200; i++)
				{
					num++;
					if (icController.SpecialPingIP() == 1)
					{
						num2++;
					}
					else
					{
						num3++;
					}
				}
				wgTools.WriteLine("control.SpecialPingIP End");
				wgUdpComm.triesTotal = 0L;
				wgTools.WriteLine("control.Test1024 Start");
				int num4 = 0;
				string text = "";
				int num5 = icController.test1024Write();
				if (num5 < 0)
				{
					text = text + CommonStr.strCommLargePacketWriteFailed + "\r\n";
				}
				num5 = icController.test1024Read(100u, ref num4);
				if (num5 < 0)
				{
					text = text + CommonStr.strCommLargePacketReadFailed + num5.ToString() + "\r\n";
				}
				if (wgUdpComm.triesTotal > 0L)
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						CommonStr.strCommLargePacketTryTimes,
						" = ",
						wgUdpComm.triesTotal.ToString(),
						"\r\n"
					});
				}
				wgTools.WriteLine("control.Test1024 End");
				if (num3 == 0)
				{
					if (text == "")
					{
						wgAppConfig.wgLog(string.Concat(new object[]
						{
							sender.ToString(),
							"  SN=",
							icController.ControllerSN,
							" ",
							CommonStr.strCommOK
						}));
						XMessageBox.Show(string.Format("{0}: {1} -- {2}", icController.ControllerSN, sender.ToString(), CommonStr.strCommOK));
					}
					else
					{
						wgAppConfig.wgLog(string.Concat(new object[]
						{
							sender.ToString(),
							"  SN=",
							icController.ControllerSN,
							" ",
							CommonStr.strCommLose,
							" ",
							text
						}));
						XMessageBox.Show(string.Format("{0}: {1} -- {2}\r\n\r\n{3}", new object[]
						{
							icController.ControllerSN,
							sender.ToString(),
							CommonStr.strCommLose,
							text
						}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					string text3 = string.Format(" {0}: {1}={2}, {3}={4}, {5} = {6}", new object[]
					{
						CommonStr.strCommPacket,
						CommonStr.strCommPacketSent,
						num,
						CommonStr.strCommPacketReceived,
						num2,
						CommonStr.strCommPacketLost,
						num3
					}) + "\r\n";
					wgAppConfig.wgLog(string.Concat(new object[]
					{
						sender.ToString(),
						"  SN=",
						icController.ControllerSN,
						" ",
						CommonStr.strCommLose,
						" ",
						text3,
						text
					}));
					XMessageBox.Show(string.Format("{0}: {1} -- {2}\r\n\r\n{3}\r\n{4}", new object[]
					{
						icController.ControllerSN,
						sender.ToString(),
						CommonStr.strCommLose,
						text3,
						text
					}), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private void addSelectedToSystemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvFoundControllers.SelectedRows.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				string text = "";
				int num = 0;
				for (int i = 0; i < this.dgvFoundControllers.SelectedRows.Count; i++)
				{
					int num2 = int.Parse(this.dgvFoundControllers.SelectedRows[i].Cells[1].Value.ToString());
					if (num2 != -1 && !icController.IsExisted2SN(num2, 0))
					{
						text = text + num2.ToString() + ",";
						num++;
						this.lblSearchNow.Text = this.dgvFoundControllers.SelectedRows[i].Cells[0].Value.ToString() + "-" + num2.ToString();
						this.toolStripStatusLabel2.Text = this.dgvFoundControllers.SelectedRows[i].Cells[0].Value.ToString() + "-" + num2.ToString();
						using (dfrmController dfrmController = new dfrmController())
						{
							dfrmController.OperateNew = true;
							dfrmController.WindowState = FormWindowState.Minimized;
							dfrmController.Show();
							dfrmController.mtxtbControllerSN.Text = num2.ToString();
							dfrmController.btnNext.PerformClick();
							dfrmController.btnOK.PerformClick();
							Application.DoEvents();
						}
					}
				}
				Cursor.Current = Cursors.Default;
				XMessageBox.Show(string.Format("{0}:[{1:d}]\r\n{2}  ", CommonStr.strAutoAddController, num, text), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void restoreDefaultIPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.btnDefault_Click(sender, e);
		}

		private void clearSwipesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (icOperator.OperatorID != 1)
			{
				XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (!this.bInput5678)
			{
				using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
				{
					dfrmInputNewName.setPasswordChar('*');
					if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
					{
						return;
					}
					if (dfrmInputNewName.strNewName != "5678")
					{
						return;
					}
				}
			}
			this.bInput5678 = true;
			if (this.dgvFoundControllers.SelectedRows.Count <= 0)
			{
				XMessageBox.Show(CommonStr.strSelectController);
			}
			else
			{
				DataGridViewRow dataGridViewRow = this.dgvFoundControllers.SelectedRows[0];
				string text = dataGridViewRow.Cells["f_ControllerSN"].Value.ToString();
				string ipString = dataGridViewRow.Cells["f_PCIPAddr"].Value.ToString();
				string ipAddr = "";
				if (XMessageBox.Show(this, sender.ToString() + " " + text + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
				{
					return;
				}
				int ipPort = 60000;
				if (this.wgudp != null)
				{
					this.wgudp = null;
				}
				IPAddress iPAddress;
				if (IPAddress.TryParse(ipString, out iPAddress))
				{
					this.wgudp = new wgUdpComm(IPAddress.Parse(ipString));
				}
				else
				{
					this.wgudp = new wgUdpComm();
				}
				Thread.Sleep(300);
				WGPacketWith1152 wGPacketWith = new WGPacketWith1152();
				wGPacketWith.type = 37;
				wGPacketWith.code = 32;
				wGPacketWith.iDevSnFrom = 0u;
				wGPacketWith.iCallReturn = 0;
				WGPacketSSI_FLASH wGPacketSSI_FLASH = new WGPacketSSI_FLASH();
				wGPacketSSI_FLASH.type = 33;
				wGPacketSSI_FLASH.code = 48;
				wGPacketSSI_FLASH.iDevSnFrom = 0u;
				wGPacketSSI_FLASH.iDevSnTo = uint.Parse(text);
				wGPacketSSI_FLASH.iCallReturn = 0;
				wGPacketSSI_FLASH.ucData = new byte[1024];
				try
				{
					Thread.Sleep(300);
					wGPacketSSI_FLASH.iStartFlashAddr = 5017600u;
					wGPacketSSI_FLASH.iEndFlashAddr = wGPacketSSI_FLASH.iStartFlashAddr + 1024u - 1u;
					for (int i = 0; i < 1024; i++)
					{
						wGPacketSSI_FLASH.ucData[i] = 255;
					}
					byte[] array = null;
					while (wGPacketSSI_FLASH.iStartFlashAddr <= 5025792u)
					{
						int num = this.wgudp.udp_get(wGPacketSSI_FLASH.ToBytes(this.wgudp.udpPort), 300, wGPacketSSI_FLASH.xid, ipAddr, ipPort, ref array);
						if (num < 0)
						{
							break;
						}
						wGPacketSSI_FLASH.iStartFlashAddr += 1024u;
					}
					using (icController icController = new icController())
					{
						icController.ControllerSN = int.Parse(dataGridViewRow.Cells["f_ControllerSN"].Value.ToString());
						if (icController.RestoreAllSwipeInTheControllersIP() > 0)
						{
							icController.UpdateFRamIP(9u, 0u);
							icController.RebootControllerIP();
						}
					}
				}
				catch (Exception)
				{
				}
				wgAppConfig.wgLog(sender.ToString() + "  SN=" + text);
				return;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.wgudp != null)
			{
				this.wgudp.Dispose();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmNetControllerConfig));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.configureToolStripMenuItem = new ToolStripMenuItem();
			this.findF3ToolStripMenuItem = new ToolStripMenuItem();
			this.searchToolStripMenuItem = new ToolStripMenuItem();
			this.searchAdvancedToolStripMenuItem = new ToolStripMenuItem();
			this.searchSpecialSNToolStripMenuItem = new ToolStripMenuItem();
			this.search100FromTheSpecialSNToolStripMenuItem = new ToolStripMenuItem();
			this.communicationTestToolStripMenuItem = new ToolStripMenuItem();
			this.clearToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultIPToolStripMenuItem = new ToolStripMenuItem();
			this.restoreDefaultParamToolStripMenuItem = new ToolStripMenuItem();
			this.restoreAllSwipesToolStripMenuItem = new ToolStripMenuItem();
			this.formatToolStripMenuItem = new ToolStripMenuItem();
			this.addSelectedToSystemToolStripMenuItem = new ToolStripMenuItem();
			this.clearSwipesToolStripMenuItem = new ToolStripMenuItem();
			this.btnIPAndWebConfigure = new Button();
			this.lblCount = new Label();
			this.label1 = new Label();
			this.lblSearchNow = new Label();
			this.chkSearchAgain = new CheckBox();
			this.btnAddToSystem = new Button();
			this.dgvFoundControllers = new DataGridView();
			this.f_ID = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_IP = new DataGridViewTextBoxColumn();
			this.f_Mask = new DataGridViewTextBoxColumn();
			this.f_Gateway = new DataGridViewTextBoxColumn();
			this.f_PORT = new DataGridViewTextBoxColumn();
			this.f_MACAddr = new DataGridViewTextBoxColumn();
			this.f_PCIPAddr = new DataGridViewTextBoxColumn();
			this.f_Note = new DataGridViewTextBoxColumn();
			this.btnExit = new Button();
			this.btnDefault = new Button();
			this.btnConfigure = new Button();
			this.btnSearch = new Button();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new ToolStripStatusLabel();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.dgvFoundControllers).BeginInit();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.configureToolStripMenuItem,
				this.findF3ToolStripMenuItem,
				this.searchToolStripMenuItem,
				this.searchAdvancedToolStripMenuItem,
				this.communicationTestToolStripMenuItem,
				this.clearToolStripMenuItem,
				this.restoreDefaultIPToolStripMenuItem,
				this.restoreDefaultParamToolStripMenuItem,
				this.restoreAllSwipesToolStripMenuItem,
				this.formatToolStripMenuItem,
				this.addSelectedToSystemToolStripMenuItem,
				this.clearSwipesToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
			componentResourceManager.ApplyResources(this.configureToolStripMenuItem, "configureToolStripMenuItem");
			this.configureToolStripMenuItem.Click += new EventHandler(this.btnConfigure_Click);
			this.findF3ToolStripMenuItem.Name = "findF3ToolStripMenuItem";
			componentResourceManager.ApplyResources(this.findF3ToolStripMenuItem, "findF3ToolStripMenuItem");
			this.findF3ToolStripMenuItem.Click += new EventHandler(this.findF3ToolStripMenuItem_Click);
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			componentResourceManager.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
			this.searchToolStripMenuItem.Click += new EventHandler(this.btnSearch_Click);
			this.searchAdvancedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.searchSpecialSNToolStripMenuItem,
				this.search100FromTheSpecialSNToolStripMenuItem
			});
			this.searchAdvancedToolStripMenuItem.Name = "searchAdvancedToolStripMenuItem";
			componentResourceManager.ApplyResources(this.searchAdvancedToolStripMenuItem, "searchAdvancedToolStripMenuItem");
			this.searchSpecialSNToolStripMenuItem.Name = "searchSpecialSNToolStripMenuItem";
			componentResourceManager.ApplyResources(this.searchSpecialSNToolStripMenuItem, "searchSpecialSNToolStripMenuItem");
			this.searchSpecialSNToolStripMenuItem.Click += new EventHandler(this.search100FromTheSpecialSNToolStripMenuItem_Click);
			this.search100FromTheSpecialSNToolStripMenuItem.Name = "search100FromTheSpecialSNToolStripMenuItem";
			componentResourceManager.ApplyResources(this.search100FromTheSpecialSNToolStripMenuItem, "search100FromTheSpecialSNToolStripMenuItem");
			this.search100FromTheSpecialSNToolStripMenuItem.Click += new EventHandler(this.search100FromTheSpecialSNToolStripMenuItem_Click);
			this.communicationTestToolStripMenuItem.Name = "communicationTestToolStripMenuItem";
			componentResourceManager.ApplyResources(this.communicationTestToolStripMenuItem, "communicationTestToolStripMenuItem");
			this.communicationTestToolStripMenuItem.Click += new EventHandler(this.communicationTestToolStripMenuItem_Click);
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			componentResourceManager.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
			this.clearToolStripMenuItem.Click += new EventHandler(this.clearToolStripMenuItem_Click);
			this.restoreDefaultIPToolStripMenuItem.Name = "restoreDefaultIPToolStripMenuItem";
			componentResourceManager.ApplyResources(this.restoreDefaultIPToolStripMenuItem, "restoreDefaultIPToolStripMenuItem");
			this.restoreDefaultIPToolStripMenuItem.Click += new EventHandler(this.restoreDefaultIPToolStripMenuItem_Click);
			this.restoreDefaultParamToolStripMenuItem.Name = "restoreDefaultParamToolStripMenuItem";
			componentResourceManager.ApplyResources(this.restoreDefaultParamToolStripMenuItem, "restoreDefaultParamToolStripMenuItem");
			this.restoreDefaultParamToolStripMenuItem.Click += new EventHandler(this.restoreDefaultParamToolStripMenuItem_Click);
			this.restoreAllSwipesToolStripMenuItem.Name = "restoreAllSwipesToolStripMenuItem";
			componentResourceManager.ApplyResources(this.restoreAllSwipesToolStripMenuItem, "restoreAllSwipesToolStripMenuItem");
			this.restoreAllSwipesToolStripMenuItem.Click += new EventHandler(this.restoreAllSwipesToolStripMenuItem_Click);
			this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
			componentResourceManager.ApplyResources(this.formatToolStripMenuItem, "formatToolStripMenuItem");
			this.formatToolStripMenuItem.Click += new EventHandler(this.formatToolStripMenuItem_Click);
			this.addSelectedToSystemToolStripMenuItem.Name = "addSelectedToSystemToolStripMenuItem";
			componentResourceManager.ApplyResources(this.addSelectedToSystemToolStripMenuItem, "addSelectedToSystemToolStripMenuItem");
			this.addSelectedToSystemToolStripMenuItem.Click += new EventHandler(this.addSelectedToSystemToolStripMenuItem_Click);
			this.clearSwipesToolStripMenuItem.Name = "clearSwipesToolStripMenuItem";
			componentResourceManager.ApplyResources(this.clearSwipesToolStripMenuItem, "clearSwipesToolStripMenuItem");
			this.clearSwipesToolStripMenuItem.Click += new EventHandler(this.clearSwipesToolStripMenuItem_Click);
			this.btnIPAndWebConfigure.BackColor = Color.Transparent;
			this.btnIPAndWebConfigure.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnIPAndWebConfigure, "btnIPAndWebConfigure");
			this.btnIPAndWebConfigure.ForeColor = Color.White;
			this.btnIPAndWebConfigure.Name = "btnIPAndWebConfigure";
			this.btnIPAndWebConfigure.UseVisualStyleBackColor = false;
			this.btnIPAndWebConfigure.Click += new EventHandler(this.btnIPAndWebConfigure_Click);
			componentResourceManager.ApplyResources(this.lblCount, "lblCount");
			this.lblCount.BackColor = Color.Transparent;
			this.lblCount.ForeColor = Color.White;
			this.lblCount.Name = "lblCount";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.lblSearchNow, "lblSearchNow");
			this.lblSearchNow.BackColor = Color.Transparent;
			this.lblSearchNow.ForeColor = Color.White;
			this.lblSearchNow.Name = "lblSearchNow";
			componentResourceManager.ApplyResources(this.chkSearchAgain, "chkSearchAgain");
			this.chkSearchAgain.BackColor = Color.Transparent;
			this.chkSearchAgain.Checked = true;
			this.chkSearchAgain.CheckState = CheckState.Checked;
			this.chkSearchAgain.ForeColor = Color.White;
			this.chkSearchAgain.Name = "chkSearchAgain";
			this.chkSearchAgain.UseVisualStyleBackColor = false;
			this.btnAddToSystem.BackColor = Color.Transparent;
			this.btnAddToSystem.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnAddToSystem, "btnAddToSystem");
			this.btnAddToSystem.ForeColor = Color.White;
			this.btnAddToSystem.Name = "btnAddToSystem";
			this.btnAddToSystem.UseVisualStyleBackColor = false;
			this.btnAddToSystem.Click += new EventHandler(this.btnAddToSystem_Click);
			this.dgvFoundControllers.AllowUserToAddRows = false;
			this.dgvFoundControllers.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dgvFoundControllers, "dgvFoundControllers");
			this.dgvFoundControllers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvFoundControllers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvFoundControllers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvFoundControllers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ID,
				this.f_ControllerSN,
				this.f_IP,
				this.f_Mask,
				this.f_Gateway,
				this.f_PORT,
				this.f_MACAddr,
				this.f_PCIPAddr,
				this.f_Note
			});
			this.dgvFoundControllers.ContextMenuStrip = this.contextMenuStrip1;
			this.dgvFoundControllers.EnableHeadersVisualStyles = false;
			this.dgvFoundControllers.Name = "dgvFoundControllers";
			this.dgvFoundControllers.ReadOnly = true;
			this.dgvFoundControllers.RowHeadersVisible = false;
			this.dgvFoundControllers.RowTemplate.Height = 23;
			this.dgvFoundControllers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvFoundControllers.MouseDoubleClick += new MouseEventHandler(this.dgvFoundControllers_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.f_ID, "f_ID");
			this.f_ID.Name = "f_ID";
			this.f_ID.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ControllerSN.DefaultCellStyle = dataGridViewCellStyle2;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_IP, "f_IP");
			this.f_IP.Name = "f_IP";
			this.f_IP.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Mask, "f_Mask");
			this.f_Mask.Name = "f_Mask";
			this.f_Mask.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Gateway, "f_Gateway");
			this.f_Gateway.Name = "f_Gateway";
			this.f_Gateway.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_PORT.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_PORT, "f_PORT");
			this.f_PORT.Name = "f_PORT";
			this.f_PORT.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_MACAddr, "f_MACAddr");
			this.f_MACAddr.Name = "f_MACAddr";
			this.f_MACAddr.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_PCIPAddr, "f_PCIPAddr");
			this.f_PCIPAddr.Name = "f_PCIPAddr";
			this.f_PCIPAddr.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Note, "f_Note");
			this.f_Note.Name = "f_Note";
			this.f_Note.ReadOnly = true;
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.btnDefault.BackColor = Color.Transparent;
			this.btnDefault.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnDefault, "btnDefault");
			this.btnDefault.ForeColor = Color.White;
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.UseVisualStyleBackColor = false;
			this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
			this.btnConfigure.BackColor = Color.Transparent;
			this.btnConfigure.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnConfigure, "btnConfigure");
			this.btnConfigure.ForeColor = Color.White;
			this.btnConfigure.Name = "btnConfigure";
			this.btnConfigure.UseVisualStyleBackColor = false;
			this.btnConfigure.Click += new EventHandler(this.btnConfigure_Click);
			this.btnSearch.BackColor = Color.Transparent;
			this.btnSearch.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnSearch, "btnSearch");
			this.btnSearch.ForeColor = Color.White;
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.UseVisualStyleBackColor = false;
			this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
			this.statusStrip1.BackColor = Color.FromArgb(91, 92, 120);
			this.statusStrip1.BackgroundImage = Resources.pMain_bottom;
			this.statusStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripStatusLabel1,
				this.toolStripStatusLabel2
			});
			componentResourceManager.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			this.toolStripStatusLabel1.ForeColor = Color.White;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			componentResourceManager.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
			this.toolStripStatusLabel2.ForeColor = Color.White;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			componentResourceManager.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
			this.toolStripStatusLabel2.Spring = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.btnIPAndWebConfigure);
			base.Controls.Add(this.lblCount);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lblSearchNow);
			base.Controls.Add(this.chkSearchAgain);
			base.Controls.Add(this.btnAddToSystem);
			base.Controls.Add(this.dgvFoundControllers);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnConfigure);
			base.Controls.Add(this.btnDefault);
			base.Controls.Add(this.btnSearch);
			base.Name = "dfrmNetControllerConfig";
			base.FormClosing += new FormClosingEventHandler(this.dfrmNetControllerConfig_FormClosing);
			base.Load += new EventHandler(this.dfrmNetControllerConfig_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmNetControllerConfig_KeyDown);
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.dgvFoundControllers).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
