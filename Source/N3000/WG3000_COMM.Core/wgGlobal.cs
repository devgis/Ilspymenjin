using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WG3000_COMM.Core
{
	internal class wgGlobal
	{
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNativeMethods
		{
			[DllImport("iphlpapi.dll", ExactSpelling = true)]
			public static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);
		}

		public const int MaxPrivilegeCount_Stat = 2000000;

		public const int Param_ConsumerUpdateLog = 50;

		public const int Param_PrivilegeCountByControllerLog = 51;

		public const int Param_PrivilegeTotalLog = 52;

		public const int Param_CreatedPartition = 53;

		public const int Param_InvalidSwipeNotAsAttend = 54;

		public const int Param_OffDutyLatestTimeForNormalAttendance = 55;

		public const int Param_EarliestTimeAsOndutyForNormalAttendance = 56;

		public const int Param_OnlyTwoTimesForNormalAttendance = 57;

		public const int Param_NormalWorkTime = 58;

		public const int Param_OnlyOnDutyForNormalAttendance = 59;

		public const int Param_PatrolAllowTimeout = 28;

		public const int Param_PatrolAbsentTimeout = 27;

		public const int Param_ActiveFireSignalShare = 60;

		public const int Param_ActiveInterlockShare = 61;

		public const int Param_ActiveAntibackShare = 62;

		public const string ExtendFunction_Password = "5678";

		public const int ParamLoc_RecordButtonEvent = 101;

		public const int ParamLoc_RecordDoorStatusEvent = 102;

		public const int ParamLoc_ActivateLogQuery = 103;

		public const int ParamLoc_ActivateDontDisplayAccessControl = 111;

		public const int ParamLoc_ActivateDontDisplayAttendance = 112;

		public const int ParamLoc_ActivateOtherShiftSchedule = 113;

		public const int ParamLoc_ActivateMaps = 114;

		public const int ParamLoc_ActivateTimeProfile = 121;

		public const int ParamLoc_ActivateRemoteOpenDoor = 122;

		public const int ParamLoc_ActivateAccessKeypad = 123;

		public const int ParamLoc_ActivatePeripheralControl = 124;

		public const int ParamLoc_ActivateControllerZone = 125;

		public const int ParamLoc_ActivateControllerTaskList = 131;

		public const int ParamLoc_ActivateAntiPassBack = 132;

		public const int ParamLoc_ActivateInterLock = 133;

		public const int ParamLoc_ActivateMultiCardAccess = 134;

		public const int ParamLoc_ActivateFirstCardOpen = 135;

		public const int ParamLoc_ActivateTimeSegLimittedAccess = 136;

		public const int ParamLoc_ActivatePCCheckAccess = 137;

		public const int ParamLoc_ActivateWarnForceWithCard = 141;

		public const int ParamLoc_ActivateDontAutoLoadPrivileges = 142;

		public const int ParamLoc_ActivateDontAutoLoadSwipeRecords = 143;

		public const int ParamLoc_ActivateElevator = 144;

		public const int ParamLoc_ActivateHouse = 145;

		public const int ParamLoc_ActivateDoorAsSwitch = 146;

		public const int ParamLoc_ActivateValidSwipeGap = 147;

		public const int ParamLoc_ActivateOperatorManagement = 148;

		public const int ParamLoc_ActivateMeeting = 149;

		public const int ParamLoc_ActivateConstMeal = 150;

		public const int ParamLoc_ActivatePatrol = 151;

		public const int TIMEOUT_TWOSWIPE_FOR_CHECK_INSIDE_BY_SWIPE = 20;

		public const int CONTROLLERID_MAX_PARTITIONNUM = 999;

		public static int TRIGGER_SOURCE_4ARM = 16;

		public static int TRIGGER_EVENT_4ARM = 14336;

		public static int ERR_PRIVILEGES_OVER200K
		{
			get
			{
				return -100001;
			}
		}

		public static int ERR_PRIVILEGES_STOPUPLOAD
		{
			get
			{
				return -100002;
			}
		}

		public static int ERR_SWIPERECORD_STOPGET
		{
			get
			{
				return -200002;
			}
		}

		private wgGlobal()
		{
		}
	}
}
