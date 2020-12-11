using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Enums;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    /// <summary>
    /// Statistics Service class
    /// Implement IStatisticService interface
    /// </summary>
    public class StatisticsService : IStatisticsService
    {
        /// <summary>
        /// Contains an object of IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Gets or sets statistics service current user
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">unit of work</param>
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Implementation of IStatisticService
        /// </summary>
        /// <param name="fromDate">expenses statistic from date</param>
        /// <param name="toDate">expenses statistic to date</param>
        /// <returns>statistic from fromDate to toDate</returns>
        public IEnumerable<StatisticsItem> GetExpenceStatistics(DateTime fromDate, DateTime toDate)
        {
            if (this.CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = this.unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == this.CurrentUser.Id).Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate  &&  x.BankAccountTo.Type == AccountType.Expence);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountTo
            });
        }

        /// <summary>
        /// Implementation of IStatisticService
        /// </summary>
        /// <param name="fromDate">income statistic from date</param>
        /// <param name="toDate">income statistic to date</param>
        /// <returns>income statistic from fromDate to toDate</returns>
        public IEnumerable<StatisticsItem> GetIncomeStatistics(DateTime fromDate, DateTime toDate)
        {
            if (this.CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = this.unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == this.CurrentUser.Id).Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate && x.BankAccountFrom.Type == AccountType.Income);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountFrom
            });
        }

        /// <summary>
        /// Implementation of IStatisticService
        /// </summary>
        /// <returns>expenses statistic for the whole period</returns>
        public IEnumerable<StatisticsItem> GetExpenceStatisticsFullPeriod()
        {
            if (this.CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = this.unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == this.CurrentUser.Id).Where(x => x.BankAccountTo.Type == AccountType.Expence);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountTo
            });
        }

        /// <summary>
        /// Implementation of IStatisticService
        /// </summary>
        /// <returns>income statistic for the whole period</returns>
        public IEnumerable<StatisticsItem> GetIncomeStatisticsFullPeriod()
        {
            if (this.CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = this.unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == this.CurrentUser.Id).Where(x => x.BankAccountFrom.Type == AccountType.Income);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountFrom
            });
        }
    }
}
