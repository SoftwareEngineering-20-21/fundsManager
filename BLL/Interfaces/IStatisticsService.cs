using BLL.Models;
using DAL.Domain;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IStatisticsService
    {
        User CurrentUser { get; set; }
        public IEnumerable<StatisticsItem> GetExpenceStatistics(DateTime fromDate,DateTime toDate);
        public IEnumerable<StatisticsItem> GetIncomeStatistics(DateTime fromDate, DateTime toDate);
        public IEnumerable<StatisticsItem> GetExpenceStatisticsFullPeriod();
        public IEnumerable<StatisticsItem> GetIncomeStatisticsFullPeriod();
    }
}
