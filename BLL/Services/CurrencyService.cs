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
    /// <summary>
    /// The Currency Service class
    /// Implement ICurrencyService interface
    /// </summary>
    
    public class CurrencyService : ICurrencyService
    {
        XmlDocument xml;
        
        /// <summary>
        /// Constructor by default
        /// </summary>
        public CurrencyService()
        {
            xml = new XmlDocument();
            UpdateCurrency();
        }

        /// <summary>
        /// Implementation of ICurrencyService
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <returns>currency rate</returns>
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

        /// <summary>
        /// Implementation of ICurrencyService
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <param name="date">Date</param>
        /// <returns>Currency rate on the date</returns>
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

        /// <summary>
        /// Implementation of ICurrencyService
        /// Update Currency
        /// </summary>
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

                TimeSpan hourCurrenyUpdate = new TimeSpan(10, 0, 0);
                TimeSpan hourToday = new TimeSpan(today.Hour, today.Minute, today.Second);
                if (datetoday != dateUpdateFile && hourToday > hourCurrenyUpdate) xml.Save("currency.xml");
            }

        }

    }
}