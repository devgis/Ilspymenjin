using System;

namespace WG3000_COMM.Core
{
	internal class wgAppRunInfo
	{
		public delegate void appRunInfoLoadNumHandler(string strNum);

		public delegate void appRunInfoCommStatusHandler(string strCommStatus);

		public delegate void appRunInfoMonitorHandler(string strNum);

		private static event wgAppRunInfo.appRunInfoLoadNumHandler appRunInfoLoadNums;

		public static event wgAppRunInfo.appRunInfoLoadNumHandler evAppRunInfoLoadNum
		{
			add
			{
				wgAppRunInfo.appRunInfoLoadNums += value;
			}
			remove
			{
				wgAppRunInfo.appRunInfoLoadNums -= value;
			}
		}

		private static event wgAppRunInfo.appRunInfoCommStatusHandler appRunInfoCommStatus;

		public static event wgAppRunInfo.appRunInfoCommStatusHandler evAppRunInfoCommStatus
		{
			add
			{
				wgAppRunInfo.appRunInfoCommStatus += value;
			}
			remove
			{
				wgAppRunInfo.appRunInfoCommStatus -= value;
			}
		}

		private static event wgAppRunInfo.appRunInfoMonitorHandler appRunInfoMonitors;

		public static event wgAppRunInfo.appRunInfoMonitorHandler evAppRunInfoMonitor
		{
			add
			{
				wgAppRunInfo.appRunInfoMonitors += value;
			}
			remove
			{
				wgAppRunInfo.appRunInfoMonitors -= value;
			}
		}

		public static void ClearAllDisplayedInfo()
		{
			wgAppRunInfo.raiseAppRunInfoLoadNums("");
			wgAppRunInfo.raiseAppRunInfoCommStatus("");
		}

		public static void raiseAppRunInfoLoadNums(string info)
		{
			if (wgAppRunInfo.appRunInfoLoadNums != null)
			{
				wgAppRunInfo.appRunInfoLoadNums(info);
			}
		}

		public static void raiseAppRunInfoCommStatus(string info)
		{
			if (wgAppRunInfo.appRunInfoCommStatus != null)
			{
				wgAppRunInfo.appRunInfoCommStatus(info);
			}
		}

		public static void raiseAppRunInfoMonitors(string info)
		{
			if (wgAppRunInfo.appRunInfoMonitors != null)
			{
				wgAppRunInfo.appRunInfoMonitors(info);
			}
		}
	}
}
