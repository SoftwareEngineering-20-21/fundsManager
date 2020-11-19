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
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;
        public User CurrentUser { get; set; }
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<StatisticsItem> GetExpenceStatistics(DateTime fromDate, DateTime toDate)
        {
           
            if (CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = unitOfWork.Repository<Transaction>()
               .Get(x=>x.UserId== CurrentUser.Id).Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate  &&  x.BankAccountTo.Type==AccountType.Expence);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountTo
            });
        }   
        public IEnumerable<StatisticsItem> GetIncomeStatistics(DateTime fromDate, DateTime toDate)
        {
          
            if (CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == CurrentUser.Id).Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate && x.BankAccountFrom.Type == AccountType.Income);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountTo
            });
        }

        public IEnumerable<StatisticsItem> GetExpenceStatisticsFullPeriod()
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == CurrentUser.Id).Where(x =>x.BankAccountTo.Type == AccountType.Expence);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountTo
            });
        }

        public IEnumerable<StatisticsItem> GetIncomeStatisticsFullPeriod()
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            var transactions = unitOfWork.Repository<Transaction>()
               .Get(x => x.UserId == CurrentUser.Id).Where(x => x.BankAccountTo.Type == AccountType.Income);

            return transactions.Select(x => new StatisticsItem
            {
                Date = x.TransactionDate,
                Value = x.AmountTo
            });
        }
    }
}
