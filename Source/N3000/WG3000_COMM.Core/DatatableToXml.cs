using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace WG3000_COMM.Core
{
	public class DatatableToXml
	{
		private static MemoryStream ms;

		public static string CDataToXml(DataTable dt)
		{
			if (dt != null)
			{
				try
				{
					using (DatatableToXml.ms = new MemoryStream())
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(DatatableToXml.ms, Encoding.Unicode))
						{
							dt.WriteXml(xmlTextWriter);
							int num = (int)DatatableToXml.ms.Length;
							byte[] array = new byte[num];
							DatatableToXml.ms.Seek(0L, SeekOrigin.Begin);
							DatatableToXml.ms.Read(array, 0, num);
							UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
							return unicodeEncoding.GetString(array).Trim();
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
			}
			return "";
		}

		public static string CDataToXml(DataSet ds, int tableIndex)
		{
			if (tableIndex != -1)
			{
				return DatatableToXml.CDataToXml(ds.Tables[tableIndex]);
			}
			return DatatableToXml.CDataToXml(ds.Tables[0]);
		}

		public static string CDataToXml(DataSet ds)
		{
			return DatatableToXml.CDataToXml(ds, -1);
		}

		public static string CDataToXml(DataView dv)
		{
			return DatatableToXml.CDataToXml(dv.Table);
		}

		public static bool CDataToXmlFile(DataTable dt, string xmlFilePath)
		{
			if (dt != null && !string.IsNullOrEmpty(xmlFilePath))
			{
				try
				{
					using (DatatableToXml.ms = new MemoryStream())
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(DatatableToXml.ms, Encoding.Unicode))
						{
							dt.WriteXml(xmlTextWriter);
							int num = (int)DatatableToXml.ms.Length;
							byte[] array = new byte[num];
							DatatableToXml.ms.Seek(0L, SeekOrigin.Begin);
							DatatableToXml.ms.Read(array, 0, num);
							UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
							using (StreamWriter streamWriter = new StreamWriter(xmlFilePath))
							{
								streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
								streamWriter.WriteLine(unicodeEncoding.GetString(array).Trim());
								return true;
							}
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
				return false;
			}
			return false;
		}

		public static bool CDataToXmlFile(DataSet ds, int tableIndex, string xmlFilePath)
		{
			if (tableIndex != -1)
			{
				return DatatableToXml.CDataToXmlFile(ds.Tables[tableIndex], xmlFilePath);
			}
			return DatatableToXml.CDataToXmlFile(ds.Tables[0], xmlFilePath);
		}

		public static bool CDataToXmlFile(DataSet ds, string xmlFilePath)
		{
			return DatatableToXml.CDataToXmlFile(ds, -1, xmlFilePath);
		}

		public static bool CDataToXmlFile(DataView dv, string xmlFilePath)
		{
			return DatatableToXml.CDataToXmlFile(dv.Table, xmlFilePath);
		}
	}
}
