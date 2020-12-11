using BLL.Models;
using DAL.Domain;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    /// <summary>
    /// Statistics Service interface
    /// Contains method to GetExpenceStatistic, getIncomeStatistic
    /// GetExpenseStatisticFullPerion, getIncomeStatisticFullPeriod
    /// </summary>
    
    public interface IStatisticsService
    {
        User CurrentUser { get; set; }

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <param name="fromDate">expence statistic from date</param>
        /// <param name="toDate">expence statistic to date</param>
        /// <returns>statistic from fromDate to toDate</returns>
        public IEnumerable<StatisticsItem> GetExpenceStatistics(DateTime fromDate,DateTime toDate);

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <param name="fromDate">income statistic from date</param>
        /// <param name="toDate">income statistioc to date</param>
        /// <returns>income statistic from fromDate to toDate</returns>
        public IEnumerable<StatisticsItem> GetIncomeStatistics(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <returns>expence statistic for the whole period</returns>
        public IEnumerable<StatisticsItem> GetExpenceStatisticsFullPeriod();

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <returns>income statistic for the whole period</returns>
        public IEnumerable<StatisticsItem> GetIncomeStatisticsFullPeriod();
    }
}
