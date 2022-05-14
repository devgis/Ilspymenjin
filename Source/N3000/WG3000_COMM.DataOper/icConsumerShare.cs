using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading;
using WG3000_COMM.Core;

namespace WG3000_COMM.DataOper
{
	internal class icConsumerShare
	{
		private static string lastLoadUsers = "";

		private static DataTable dtLastLoad;

		private static int iSelectedMin = 1879048192;

		private static int iSelectedMax = 1879048192;

		private static int m_iSelectedCurrentNoneMax = 1879048192;

		private static DataTable dtUser = null;

		public static int iSelectedCurrentNoneMax
		{
			get
			{
				return icConsumerShare.m_iSelectedCurrentNoneMax;
			}
		}

		public static string getUpdateLog()
		{
			return wgTools.SetObjToStr(wgAppConfig.getSystemParamByNO(50));
		}

		public static void setUpdateLog()
		{
			wgAppConfig.setSystemParamValue(50, null, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), null);
		}

		public static void loadUserData()
		{
			wgTools.WriteLine("loadUserData Start");
			Thread.Sleep(100);
			if (!string.IsNullOrEmpty(icConsumerShare.lastLoadUsers) && icConsumerShare.lastLoadUsers == icConsumerShare.getUpdateLog() && icConsumerShare.dtLastLoad != null && icConsumerShare.iSelectedMax + 1000 < 2147483647)
			{
				icConsumerShare.selectNoneUsers();
				icConsumerShare.dtLastLoad.AcceptChanges();
				wgTools.WriteLine("return dtLastLoad");
				return;
			}
			icConsumerShare.iSelectedMin = 1879048192;
			icConsumerShare.iSelectedMax = 1879048192;
			icConsumerShare.m_iSelectedCurrentNoneMax = 1879048192;
			string text = string.Format(" SELECT f_ConsumerID, f_ConsumerNO, f_ConsumerName, f_CardNO, {0:d} as f_Selected, f_GroupID, f_DoorEnabled  ", icConsumerShare.iSelectedMin);
			text += " FROM t_b_Consumer ";
			text += " ORDER BY f_ConsumerNO ASC ";
			icConsumerShare.dtUser = new DataTable();
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
							oleDbDataAdapter.Fill(icConsumerShare.dtUser);
						}
					}
					goto IL_17A;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						sqlDataAdapter.Fill(icConsumerShare.dtUser);
					}
				}
			}
			IL_17A:
			wgTools.WriteLine("da.Fill End");
			try
			{
				DataColumn[] primaryKey = new DataColumn[]
				{
					icConsumerShare.dtUser.Columns[0]
				};
				icConsumerShare.dtUser.PrimaryKey = primaryKey;
			}
			catch (Exception)
			{
				throw;
			}
			icConsumerShare.lastLoadUsers = icConsumerShare.getUpdateLog();
			icConsumerShare.dtLastLoad = icConsumerShare.dtUser;
		}

		public static void selectAllUsers()
		{
			icConsumerShare.iSelectedMin--;
			icConsumerShare.m_iSelectedCurrentNoneMax = icConsumerShare.iSelectedMin;
		}

		public static void selectNoneUsers()
		{
			icConsumerShare.iSelectedMax++;
			icConsumerShare.m_iSelectedCurrentNoneMax = icConsumerShare.iSelectedMax;
		}

		public static DataTable getDt()
		{
			return icConsumerShare.dtLastLoad;
		}

		public static string getOptionalRowfilter()
		{
			return string.Format("( f_Selected <={0:d} )", icConsumerShare.m_iSelectedCurrentNoneMax);
		}

		public static string getSelectedRowfilter()
		{
			return string.Format(" ( f_Selected >{0:d} )", icConsumerShare.m_iSelectedCurrentNoneMax);
		}

		public static int getSelectedValue()
		{
			return icConsumerShare.m_iSelectedCurrentNoneMax + 1;
		}
	}
}
