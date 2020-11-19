using System;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BLL.Interfaces;
namespace BLL.Services
{
    public class CurrencyService : ICurrencyService
    {
        XmlDocument xml;

        public CurrencyService()
        {
            xml = new XmlDocument();
            UpdateCurrency();
        }
        public Decimal GetRate(string code)
        {
            UpdateCurrency();
            XmlNodeList xnList = xml.SelectNodes("/exchange/currency");
            foreach (XmlNode node in xnList)
            {
                if (node["cc"].InnerText == code)
                {
                    return Decimal.Parse(node["rate"].InnerText);
                }
            }
            throw new Exception();
        }

        public decimal GetRateByDate(string code, DateTime date)
        {
            string convertedDate = date.ToString("yyyyMMdd");
            string url = $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={code}&date={convertedDate}";
            string content = new WebClient().DownloadString(url);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            XmlNodeList xmlNode = xmlDocument.SelectNodes("/exchange/currency");
            return Decimal.Parse(xmlNode[0]["rate"].InnerText);
        }

        public void UpdateCurrency()
        {
            string content = new WebClient().DownloadString("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange");
            xml.LoadXml(content);
            string fileName = "currency.xml";
            FileInfo fi = new FileInfo(fileName);
            bool exists = fi.Exists;
            if (!fi.Exists)
            {
                xml.Save("currency.xml");
            }
            else
            {
                DateTime today = DateTime.Now;
                DateTime updatedTimeFile = fi.LastWriteTime;

                DateTime datetoday = new DateTime(today.Year, today.Month, today.Day);
                DateTime dateUpdateFile = new DateTime(updatedTimeFile.Year, updatedTimeFile.Month, updatedTimeFile.Day);

                DateTime hourCurrenyUpdate = new DateTime(2015, 1, 1, 10, 0, 1);
                DateTime hourToday = new DateTime(2015, 1, 1, today.Hour, today.Minute, today.Second);

                if (datetoday != dateUpdateFile && hourToday > hourCurrenyUpdate) xml.Save("currency.xml");
            }

        }

    }
}