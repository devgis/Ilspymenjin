using System;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.DataOper
{
	public class icDesc
	{
		public const int ERRBIT_PARAM = 0;

		public const int ERRBIT_DATAFLASH = 1;

		public const int ERRBIT_REALCLOCK = 2;

		public const int WARNBIT_FORCE = 0;

		public const int WARNBIT_DOOROPENTOOLONG = 1;

		public const int WARNBIT_DOORINVALIDOPEN = 2;

		public const int WARNBIT_FORCECLOSE = 3;

		public const int WARNBIT_DOORINVALIDREAD = 4;

		public const int WARNBIT_FIRELINK = 5;

		public const int WARNBIT_ALARM = 6;

		public const int WARNBIT_FORCE_WITHCARD = 7;

		private static string[] allPinDesc = new string[]
		{
			"1",
			"读卡器的4号指示灯",
			"2",
			"读卡器的3号指示灯",
			"5",
			"读卡器的2号指示灯",
			"6",
			"读卡器的1号指示灯",
			"10",
			"1号读卡器的D0",
			"11",
			"1号读卡器的D1",
			"12",
			"2号读卡器的D0",
			"13",
			"2号读卡器的D1",
			"18",
			"DF的写保护控制",
			"19",
			"DF的复位控制",
			"22",
			"2号门磁",
			"23",
			"刷卡或按键信号指示灯",
			"24",
			"故障指示灯",
			"25",
			"运行指示灯",
			"26",
			"强制锁门(紧急双闭)",
			"27",
			"作为3.3电源使能控制 2010-8-24 13:45:23 ",
			"28",
			"DF的sck",
			"29",
			"DF的片选",
			"30",
			"DF的miso",
			"31",
			"DF的mosi",
			"34",
			"SD2405API的scl",
			"35",
			"SD2405API的sda",
			"47",
			"3号开门按钮",
			"59",
			"网络连接指示灯绿色(由网络控制)",
			"60",
			"网络通信指示灯(由网络控制)",
			"61",
			"4号开门按钮",
			"66",
			"1号开门按钮",
			"67",
			"2号开门按钮",
			"70",
			"扩展板的scl",
			"71",
			"扩展板的sda",
			"72",
			"3号锁",
			"73",
			"4号锁",
			"74",
			"3号门磁",
			"75",
			"4号门磁",
			"77",
			"JTAG 5",
			"78",
			"JTAG 1",
			"79",
			"JTAG 3",
			"80",
			"JTAG 4",
			"89",
			"JTAG 只接了上拉3.3V",
			"90",
			"2号锁",
			"91",
			"1号门磁",
			"92",
			"1号锁",
			"95",
			"3号读卡器的D0",
			"96",
			"3号读卡器的D1",
			"99",
			"4号读卡器的D0",
			"100",
			"4号读卡器的D1"
		};

		public static string doorControlDesc(int doorControl)
		{
			string result;
			switch (doorControl)
			{
			case 1:
				result = CommonStr.strDoorControl_NO;
				break;
			case 2:
				result = CommonStr.strDoorControl_NC;
				break;
			case 3:
				result = CommonStr.strDoorControl_OnLine;
				break;
			default:
				result = doorControl.ToString();
				break;
			}
			return result;
		}

		public static string ErrorDetail(int errNo)
		{
			string text = "";
			if ((errNo & 1) > 0)
			{
				text += CommonStr.strErrParam;
			}
			if ((errNo & 2) > 0)
			{
				text = text + " " + CommonStr.strErrDataFlash;
			}
			if ((errNo & 4) > 0)
			{
				text = text + " " + CommonStr.strErrRealClock;
			}
			return text;
		}

		public static string WarnDetail(int warnNo)
		{
			string text = "";
			if ((warnNo & 1) > 0)
			{
				text = text + "-" + CommonStr.strWarnThreateCode;
			}
			if ((warnNo & 2) > 0)
			{
				text = text + "-" + CommonStr.strWarnOpenTooLong;
			}
			if ((warnNo & 4) > 0)
			{
				text = text + "-" + CommonStr.strWarnForcedOpen;
			}
			if ((warnNo & 8) > 0)
			{
				text = text + "-" + CommonStr.strWarnForcedLock;
			}
			if ((warnNo & 16) > 0)
			{
				text = text + "-" + CommonStr.strWarnInvalidCardSwiping;
			}
			if ((warnNo & 32) > 0)
			{
				text = text + "-" + CommonStr.strWarnFireAlarm;
			}
			if ((warnNo & 64) > 0)
			{
				text = text + "-" + CommonStr.strWarnARM;
			}
			return text;
		}

		public static string failedPinDesc(int failedPin)
		{
			string text = "";
			int num = -1;
			if (failedPin == 104)
			{
				text = "时钟问题";
			}
			else if (failedPin == 103)
			{
				num = 100;
				text = "时钟问题, ";
			}
			else if (failedPin > 100)
			{
				num = failedPin - 100;
				text = "时钟问题, ";
			}
			else if (failedPin > 0)
			{
				num = failedPin;
			}
			if (num > 0)
			{
				for (int i = 0; i < icDesc.allPinDesc.Length; i += 2)
				{
					if (string.Compare(num.ToString(), icDesc.allPinDesc[i]) == 0)
					{
						text += icDesc.allPinDesc[i + 1];
					}
				}
			}
			return text;
		}
	}
}
