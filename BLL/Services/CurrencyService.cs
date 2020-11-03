using System;
using System.Data;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BLL.Interfaces;
namespace BLL.Services
{
    class CurrencyService : ICurrencyService
    {
        XmlDocument xml;

        public CurrencyService()
        {
            xml = new XmlDocument();
            UpdateCurrency();
        }
        public Decimal GetRate(string code)
        {
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
        }
    }
}