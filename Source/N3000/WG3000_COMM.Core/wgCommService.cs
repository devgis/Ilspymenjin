using System;
using System.Collections.Generic;
using System.Threading;
using WG3000_COMM.DataOper;

namespace WG3000_COMM.Core
{
	public class wgCommService : MarshalByRefObject, IDisposable
	{
		private Dictionary<int, icController> m_NowWatching;

		private Dictionary<int, icController> m_WantWatching;

		private DateTime m_ControllerUpdateTime = DateTime.Now;

		private int updateCnt;

		private DateTime m_lastGetInfoDateTime = DateTime.Now;

		private int m_bHaveServer;

		private wgCommServer commServer;

		private int m_bStopWatch;

		private static int m_watching_cycle_ms = 400;

		private static int m_unconnect_timeout_sec = 6;

		public event OnCommEventHandler EventHandler;

		public DateTime lastGetInfoDateTime
		{
			get
			{
				return this.m_lastGetInfoDateTime;
			}
		}

		public Dictionary<int, icController> WatchingController
		{
			get
			{
				return this.m_NowWatching;
			}
			set
			{
				if (this.m_WantWatching != null)
				{
					this.m_WantWatching = null;
				}
				if (value != null)
				{
					Dictionary<int, icController> value2 = new Dictionary<int, icController>(value);
					Interlocked.Exchange<Dictionary<int, icController>>(ref this.m_WantWatching, value2);
				}
				this.m_ControllerUpdateTime = DateTime.Now;
				if (this.updateCnt == 2147483647)
				{
					Interlocked.Exchange(ref this.updateCnt, 0);
				}
				Interlocked.Increment(ref this.updateCnt);
			}
		}

		public static int Watching_Cycle_ms
		{
			get
			{
				return wgCommService.m_watching_cycle_ms;
			}
			set
			{
				if (value > 0 && value < 3600000)
				{
					wgCommService.m_watching_cycle_ms = value;
				}
			}
		}

		public static int unconnect_timeout_sec
		{
			get
			{
				if (wgCommService.Watching_Cycle_ms > wgCommService.m_unconnect_timeout_sec * 1000)
				{
					return wgCommService.Watching_Cycle_ms / 1000 + 1;
				}
				return wgCommService.m_unconnect_timeout_sec;
			}
			set
			{
				if (value > 0 && value < 3600)
				{
					wgCommService.m_unconnect_timeout_sec = value;
				}
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.commServer != null)
			{
				this.commServer.Close();
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public ControllerRunInformation GetRunInfo(int ControllerSN)
		{
			return this.commServer.GetRunInfo(ControllerSN);
		}

		public wgCommService()
		{
			new Thread(new ThreadStart(this.WatchController))
			{
				Name = "Comm Service"
			}.Start();
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		private void PublishEvent(string message)
		{
			wgTools.WgDebugWrite("Publishing \"{0}\"...", new object[]
			{
				message
			});
			if (this.EventHandler != null)
			{
				this.EventHandler(message);
			}
		}

		public void StopWatch()
		{
			Interlocked.Exchange(ref this.m_bStopWatch, 1);
		}

		private void WatchController()
		{
			wgTools.WgDebugWrite("watchController= {0:d}", new object[]
			{
				111111111
			});
			if (this.m_bHaveServer > 0)
			{
				return;
			}
			Interlocked.Increment(ref this.m_bHaveServer);
			WGPacketBasicRunInformation4ServerToSend wGPacketBasicRunInformation4ServerToSend = new WGPacketBasicRunInformation4ServerToSend();
			wGPacketBasicRunInformation4ServerToSend.type = 32;
			wGPacketBasicRunInformation4ServerToSend.code = 32;
			wGPacketBasicRunInformation4ServerToSend.iDevSnFrom = 0u;
			wGPacketBasicRunInformation4ServerToSend.iDevSnTo = 0u;
			wGPacketBasicRunInformation4ServerToSend.iCallReturn = 0;
			this.commServer = new wgCommServer();
			this.commServer.evNewRecord += new wgCommServer.newRecordHandler(this.udpserver_evNewRecord);
			byte[] array = null;
			wgTools.WgDebugWrite("m_bStopWatch= {0:d}", new object[]
			{
				this.m_bStopWatch
			});
			DateTime arg_AF_0 = DateTime.Now;
			int num = -1;
			int num2 = 0;
			while (this.m_bStopWatch < 1)
			{
				if (num != this.updateCnt)
				{
					this.m_NowWatching = null;
					if (this.m_WantWatching != null)
					{
						Interlocked.Exchange<Dictionary<int, icController>>(ref this.m_NowWatching, this.m_WantWatching);
					}
					Interlocked.Exchange(ref num, this.updateCnt);
					num2 = 3;
				}
				else if (this.m_NowWatching == null)
				{
					Thread.Sleep(100);
				}
				else if (num2 >= 0)
				{
					num2--;
					long ticks = DateTime.Now.Ticks;
					foreach (KeyValuePair<int, icController> current in this.m_NowWatching)
					{
						wGPacketBasicRunInformation4ServerToSend.iDevSnTo = (uint)current.Value.ControllerSN;
						if (array != null)
						{
							wGPacketBasicRunInformation4ServerToSend.GetNewXid();
						}
						array = wGPacketBasicRunInformation4ServerToSend.ToBytes();
						this.commServer.UDP_OnlySend(array, 300, current.Value.IP, current.Value.PORT);
						Thread.Sleep(1);
					}
					long ticks2 = DateTime.Now.Ticks;
					this.m_lastGetInfoDateTime = DateTime.Now;
					if (ticks2 > ticks && ticks2 - ticks < (long)(wgCommService.m_watching_cycle_ms * 1000 * 10))
					{
						Thread.Sleep(wgCommService.m_watching_cycle_ms - (int)(ticks2 - ticks) / 10000);
					}
				}
			}
			this.commServer.evNewRecord -= new wgCommServer.newRecordHandler(this.udpserver_evNewRecord);
			this.commServer.Dispose();
		}

		private void udpserver_evNewRecord(string info)
		{
			if (this.EventHandler != null)
			{
				OnCommEventHandler onCommEventHandler = null;
				int num = 1;
				Delegate[] invocationList = this.EventHandler.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Delegate @delegate = invocationList[i];
					try
					{
						onCommEventHandler = (OnCommEventHandler)@delegate;
						onCommEventHandler(info);
					}
					catch (Exception ex)
					{
						wgTools.WriteLine(ex.ToString());
						wgTools.WgDebugWrite("事件订阅者" + num.ToString() + "发生错误,系统将取消事件订阅!", new object[0]);
						this.EventHandler -= onCommEventHandler;
					}
					num++;
				}
			}
		}
	}
}
