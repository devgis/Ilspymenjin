using System;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Properties;

namespace WG3000_COMM.Core
{
	public class InfoRow
	{
		public string desc = "";

		public string information = "";

		public string detail = "";

		public int category = 100;

		public string MjRecStr = "";

		private static DataGridViewCellStyle styleRed = null;

		private static DataGridViewCellStyle styleGreen = new DataGridViewCellStyle();

		private static DataGridViewCellStyle styleYellow = new DataGridViewCellStyle();

		private static DataGridViewCellStyle styleOrange = new DataGridViewCellStyle();

		private static void loadStyle()
		{
			InfoRow.styleRed = new DataGridViewCellStyle
			{
				Alignment = DataGridViewContentAlignment.MiddleLeft,
				BackColor = Color.Red,
				Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 134),
				ForeColor = Color.White,
				SelectionBackColor = SystemColors.Highlight,
				SelectionForeColor = SystemColors.HighlightText,
				WrapMode = DataGridViewTriState.False
			};
			InfoRow.styleGreen = new DataGridViewCellStyle
			{
				Alignment = DataGridViewContentAlignment.MiddleLeft,
				BackColor = Color.Green,
				Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 134),
				ForeColor = Color.White,
				SelectionBackColor = SystemColors.Highlight,
				SelectionForeColor = SystemColors.HighlightText,
				WrapMode = DataGridViewTriState.False
			};
			InfoRow.styleYellow = new DataGridViewCellStyle
			{
				Alignment = DataGridViewContentAlignment.MiddleLeft,
				BackColor = Color.Yellow,
				Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 134),
				ForeColor = Color.Blue,
				SelectionBackColor = SystemColors.Highlight,
				SelectionForeColor = SystemColors.HighlightText,
				WrapMode = DataGridViewTriState.False
			};
			InfoRow.styleOrange = new DataGridViewCellStyle
			{
				Alignment = DataGridViewContentAlignment.MiddleLeft,
				BackColor = Color.Orange,
				Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 134),
				ForeColor = Color.Blue,
				SelectionBackColor = SystemColors.Highlight,
				SelectionForeColor = SystemColors.HighlightText,
				WrapMode = DataGridViewTriState.False
			};
		}

		public static object getImage(string stringValue, ref DataGridViewRow dgvr)
		{
			if (InfoRow.styleRed == null)
			{
				InfoRow.loadStyle();
			}
			object result;
			switch (stringValue)
			{
			case "0":
			case "2":
				result = Resources.Rec1Pass;
				dgvr.DefaultCellStyle = InfoRow.styleGreen;
				return result;
			case "1":
			case "3":
				result = Resources.Rec2NoPass;
				dgvr.DefaultCellStyle = InfoRow.styleOrange;
				return result;
			case "4":
			case "6":
				result = Resources.Rec3Warn;
				dgvr.DefaultCellStyle = InfoRow.styleYellow;
				return result;
			case "5":
				result = Resources.Rec4Falt;
				dgvr.DefaultCellStyle = InfoRow.styleRed;
				return result;
			case "101":
				result = Resources.Rec4Falt;
				return result;
			case "501":
				result = Resources.Rec3Warn;
				dgvr.DefaultCellStyle = InfoRow.styleYellow;
				return result;
			}
			result = Resources.eventlogInfo;
			return result;
		}
	}
}
