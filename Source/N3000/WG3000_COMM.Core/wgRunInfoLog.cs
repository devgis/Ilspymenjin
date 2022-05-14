using System;
using System.Data;
using System.Windows.Forms;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Core
{
	public class wgRunInfoLog
	{
		public static DataTable m_dt;

		public static int logRecEventMode = 0;

		public static void init(out DataTable dt)
		{
			dt = new DataTable();
			dt.TableName = "runInfolog";
			dt.Columns.Add("f_Category");
			dt.Columns.Add("f_RecID", Type.GetType("System.UInt32"));
			dt.Columns.Add("f_Time");
			dt.Columns.Add("f_Desc");
			dt.Columns.Add("f_Info");
			dt.Columns.Add("f_Detail");
			dt.Columns.Add("f_MjRecStr");
			dt.AcceptChanges();
			wgRunInfoLog.m_dt = dt;
			try
			{
				wgRunInfoLog.logRecEventMode = 0;
				int.TryParse(wgAppConfig.GetKeyVal("logRecEventMode"), out wgRunInfoLog.logRecEventMode);
			}
			catch (Exception)
			{
			}
		}

		public static void addEvent(InfoRow newInfo)
		{
			if (wgRunInfoLog.m_dt != null)
			{
				wgRunInfoLog.m_dt.AcceptChanges();
				DataRow dataRow = wgRunInfoLog.m_dt.NewRow();
				dataRow[0] = newInfo.category;
				dataRow[1] = wgRunInfoLog.m_dt.Rows.Count + 1;
				dataRow[2] = DateTime.Now.ToString("HH:mm:ss");
				dataRow[3] = newInfo.desc;
				dataRow[4] = newInfo.information;
				dataRow[5] = newInfo.detail;
				dataRow[6] = newInfo.MjRecStr;
				wgAppConfig.wgLog(string.Format("{0},{1},{2},{3},{4}", new object[]
				{
					(wgRunInfoLog.m_dt.Rows.Count + 1).ToString(),
					newInfo.desc,
					newInfo.information,
					newInfo.detail,
					newInfo.MjRecStr
				}));
				wgRunInfoLog.m_dt.Rows.Add(dataRow);
				wgRunInfoLog.m_dt.AcceptChanges();
			}
		}

		public static void addEventSpecial1(InfoRow newInfo)
		{
			if (wgRunInfoLog.m_dt != null)
			{
				DataRow dataRow = wgRunInfoLog.m_dt.NewRow();
				dataRow[0] = newInfo.category;
				dataRow[1] = wgRunInfoLog.m_dt.Rows.Count + 1;
				dataRow[2] = DateTime.Now.ToString("HH:mm:ss");
				dataRow[3] = newInfo.desc;
				dataRow[4] = newInfo.information;
				dataRow[5] = newInfo.detail;
				dataRow[6] = newInfo.MjRecStr;
				wgTools.WriteLine("wgRunInfoLog.addEventSpecial1   dr[6]");
				wgAppConfig.wgLog(string.Format("{0},{1},{2},{3},{4}", new object[]
				{
					(wgRunInfoLog.m_dt.Rows.Count + 1).ToString(),
					newInfo.desc,
					newInfo.information,
					newInfo.detail,
					newInfo.MjRecStr
				}));
				wgRunInfoLog.m_dt.Rows.Add(dataRow);
			}
		}

		public static void addEventSpecial2()
		{
			if (wgRunInfoLog.m_dt != null)
			{
				wgRunInfoLog.m_dt.AcceptChanges();
			}
		}

		public static void addEventNotConnect(int ControllerSN, string IP, ListViewItem itm)
		{
			if (itm != null)
			{
				wgRunInfoLog.addEvent(new InfoRow
				{
					category = 101,
					desc = itm.Text,
					information = string.Format("{0}--{1}:{2:d}--IP:{3}", new object[]
					{
						CommonStr.strCommFail,
						CommonStr.strControllerSN,
						ControllerSN,
						IP
					}),
					detail = string.Format("{0}\r\n{1}\r\n{2}:\t{3:d}\r\nIP:\t{4}\r\n", new object[]
					{
						itm.Text,
						CommonStr.strCommFail,
						CommonStr.strControllerSN,
						ControllerSN,
						IP
					})
				});
				itm.ImageIndex = 3;
			}
		}
	}
}
