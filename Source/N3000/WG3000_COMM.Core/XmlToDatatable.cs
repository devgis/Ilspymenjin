using System;
using System.Data;
using System.IO;
using System.Xml;

namespace WG3000_COMM.Core
{
	public class XmlToDatatable
	{
		private static StringReader StrStream;

		public static DataSet CXmlToDataSet(string xmlStr)
		{
			if (!string.IsNullOrEmpty(xmlStr))
			{
				try
				{
					if (xmlStr.Substring(0, 2) == "?<")
					{
						xmlStr = xmlStr.Substring(1);
					}
					using (DataSet dataSet = new DataSet())
					{
						using (XmlToDatatable.StrStream = new StringReader(xmlStr))
						{
							using (XmlTextReader xmlTextReader = new XmlTextReader(XmlToDatatable.StrStream))
							{
								dataSet.ReadXml(xmlTextReader);
								return dataSet;
							}
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
			}
			return null;
		}

		public static DataTable CXmlToDatatTable(string xmlStr, int tableIndex)
		{
			return XmlToDatatable.CXmlToDataSet(xmlStr).Tables[tableIndex];
		}

		public static DataTable CXmlToDatatTable(string xmlStr)
		{
			return XmlToDatatable.CXmlToDataSet(xmlStr).Tables[0];
		}

		public static DataSet CXmlFileToDataSet(string xmlFilePath)
		{
			if (!string.IsNullOrEmpty(xmlFilePath))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(xmlFilePath);
					using (DataSet dataSet = new DataSet())
					{
						using (XmlToDatatable.StrStream = new StringReader(xmlDocument.InnerXml))
						{
							using (XmlTextReader xmlTextReader = new XmlTextReader(XmlToDatatable.StrStream))
							{
								dataSet.ReadXml(xmlTextReader);
								return dataSet;
							}
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
			}
			return null;
		}

		public static DataTable CXmlToDataTable(string xmlFilePath, int tableIndex)
		{
			return XmlToDatatable.CXmlFileToDataSet(xmlFilePath).Tables[tableIndex];
		}

		public static DataTable CXmlToDataTable(string xmlFilePath)
		{
			return XmlToDatatable.CXmlFileToDataSet(xmlFilePath).Tables[0];
		}
	}
}
