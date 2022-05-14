using System;
using System.Data;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	internal class icPrivilegeShare
	{
		private static int m_privilegeTotal = -1;

		private static DataView m_dvPrivilegeCount = null;

		public static bool bNeedRefresh = true;

		public static int privilegeTotal
		{
			get
			{
				return icPrivilegeShare.m_privilegeTotal;
			}
		}

		public static DataView dvPrivilegeCount
		{
			get
			{
				return icPrivilegeShare.m_dvPrivilegeCount;
			}
		}

		public static void setNeedRefresh()
		{
			icPrivilegeShare.m_privilegeTotal = -1;
			icPrivilegeShare.m_dvPrivilegeCount = null;
			wgAppConfig.setSystemParamValue(52, null, null, null);
			wgAppConfig.setSystemParamValue(51, null, null, null);
			icPrivilegeShare.bNeedRefresh = true;
		}
	}
}
