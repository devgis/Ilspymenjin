using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WG3000_COMM.Core
{
	public class wgCommServer : IDisposable
	{
		public delegate void newRecordHandler(string info);

		private class udpController
		{
			public uint ControllerSN;

			public uint lastRecordIndex = 4294967295u;

			public bool isConnected;

			public bool isFirstComm = true;

			public ControllerRunInformation runinfo;

			public udpController(uint SN)
			{
				this.ControllerSN = SN;
				this.runinfo = new ControllerRunInformation();
			}
		}

		private Queue UDPQueue = new Queue();

		private bool bUDPListenStop;

		private Socket UdpSocket;

		private Thread UDPListenThread;

		private Thread DealRuninfoPacketThread;

		private byte[] cmdtemp = new byte[1052];

		private IPEndPoint endp4broadcst;

		private ArrayList arrlstController = new ArrayList();

		private ArrayList arrlstLastRecordIndex = new ArrayList();

		private ArrayList arrlstSwipeRecord = new ArrayList();

		private Dictionary<uint, wgCommServer.udpController> watchedControllers = new Dictionary<uint, wgCommServer.udpController>();

		private long iNewRecordsCnt;

		private event wgCommServer.newRecordHandler evNewRecords;

		public event wgCommServer.newRecordHandler evNewRecord
		{
			add
			{
				this.evNewRecords += value;
			}
			remove
			{
				this.evNewRecords -= value;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void RaiseEvNewRecord(string info)
		{
			if (this.evNewRecords != null)
			{
				this.evNewRecords(info);
			}
		}

		public bool Close()
		{
			try
			{
				this.bUDPListenStop = true;
				Thread.Sleep(20);
				if (this.UDPListenThread != null)
				{
					this.UDPListenThread.Abort();
				}
				if (this.UDPQueue != null)
				{
					this.UDPQueue.Clear();
					this.UDPQueue = null;
				}
				if (this.DealRuninfoPacketThread != null)
				{
					this.DealRuninfoPacketThread.Abort();
				}
				if (this.UdpSocket != null)
				{
					this.UdpSocket.Close();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return true;
		}

		public wgCommServer()
		{
			try
			{
				this.UdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				this.UdpSocket.EnableBroadcast = true;
				this.UdpSocket.ReceiveBufferSize = 16777216;
				this.UDPListenThread = new Thread(new ThreadStart(this.UDPListenProc));
				this.UDPListenThread.Name = "wgUdpServer";
				this.UDPListenThread.IsBackground = true;
				this.UDPListenThread.Start();
				this.DealRuninfoPacketThread = new Thread(new ThreadStart(this.DealRuninfoPacketProc));
				this.DealRuninfoPacketThread.Name = "Deal Run InfoPacket";
				this.DealRuninfoPacketThread.IsBackground = true;
				this.DealRuninfoPacketThread.Start();
				Thread.Sleep(10);
			}
			catch (Exception)
			{
			}
		}

		private void UDPListenProc()
		{
			try
			{
				IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
				IPEndPoint iPEndPoint2 = new IPEndPoint(IPAddress.Broadcast, 60000);
				byte[] array = new byte[28];
				array[0] = 13;
				array[1] = 13;
				byte[] buffer = array;
				EndPoint localEP = iPEndPoint;
				EndPoint remoteEP = iPEndPoint2;
				this.UdpSocket.Bind(localEP);
				this.UdpSocket.SendTo(buffer, remoteEP);
				do
				{
					byte[] array2 = new byte[1500];
					int num = this.UdpSocket.ReceiveFrom(array2, ref localEP);
					byte[] array3 = new byte[num];
					Array.Copy(array2, 0, array3, 0, num);
					lock (this.UDPQueue.SyncRoot)
					{
						this.UDPQueue.Enqueue(array3);
					}
				}
				while (!this.bUDPListenStop);
			}
			catch (Exception)
			{
			}
		}

		public int UDP_OnlySend(byte[] cmd, int parWaitMs, string ipAddr, int ipPort)
		{
			int result = -13;
			try
			{
				IPEndPoint iPEndPoint = null;
				if (this.endp4broadcst == null)
				{
					this.endp4broadcst = new IPEndPoint(IPAddress.Broadcast, 60000);
				}
				if (string.IsNullOrEmpty(ipAddr))
				{
					iPEndPoint = this.endp4broadcst;
				}
				else
				{
					int iPEndByIPAddr = wgUdpComm.GetIPEndByIPAddr(ipAddr, ipPort, ref iPEndPoint);
					if (iPEndByIPAddr < 0)
					{
						return result;
					}
					if (iPEndByIPAddr == 2)
					{
						parWaitMs += wgUdpComm.timeourMsInternet;
					}
				}
				if (iPEndPoint != null)
				{
					this.cmdtemp = new byte[cmd.Length];
					Array.Copy(cmd, this.cmdtemp, cmd.Length);
					EndPoint remoteEP = iPEndPoint;
					this.UdpSocket.SendTo(cmd, remoteEP);
					result = 1;
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		public ControllerRunInformation GetRunInfo(int controllerSN)
		{
			if (this.watchedControllers.ContainsKey((uint)controllerSN))
			{
				return this.watchedControllers[(uint)controllerSN].runinfo;
			}
			return null;
		}

		private void DealRuninfoPacketProc()
		{
			try
			{
				do
				{
					if (this.UDPQueue.Count > 0)
					{
						byte[] array;
						lock (this.UDPQueue.SyncRoot)
						{
							array = (byte[])this.UDPQueue.Dequeue();
						}
						if (array.Length == wgMjControllerRunInformation.pktlen && array[0] == 32 && array[1] == 33)
						{
							uint num = BitConverter.ToUInt32(array, 8);
							if (!this.watchedControllers.ContainsKey(num))
							{
								this.watchedControllers.Add(num, new wgCommServer.udpController(num));
							}
							ControllerRunInformation runinfo = this.watchedControllers[num].runinfo;
							runinfo.update(array, 20, num);
							if (runinfo.newSwipes[0].IndexInDataFlash != 4294967295u && (runinfo.newSwipes[0].IndexInDataFlash > this.watchedControllers[num].lastRecordIndex || this.watchedControllers[num].lastRecordIndex == 4294967295u))
							{
								for (int i = 9; i >= 0; i--)
								{
									if (runinfo.newSwipes[i].IndexInDataFlash != 4294967295u && (runinfo.newSwipes[i].IndexInDataFlash > this.watchedControllers[num].lastRecordIndex || this.watchedControllers[num].lastRecordIndex == 4294967295u))
									{
										this.watchedControllers[num].lastRecordIndex = runinfo.newSwipes[i].IndexInDataFlash;
										this.arrlstSwipeRecord.Add(runinfo.newSwipes[i]);
										if (!this.watchedControllers[num].isFirstComm)
										{
											this.RaiseEvNewRecord(runinfo.newSwipes[i].ToStringRaw());
											this.iNewRecordsCnt += 1L;
										}
									}
								}
							}
							if (this.watchedControllers[num].isFirstComm)
							{
								this.watchedControllers[num].isFirstComm = false;
							}
						}
						Thread.Sleep(1);
					}
					else
					{
						Thread.Sleep(10);
					}
				}
				while (!this.bUDPListenStop);
			}
			catch (Exception)
			{
			}
		}
	}
}
