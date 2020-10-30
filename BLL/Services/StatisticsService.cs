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
    class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;
        public User CurrentUser { get; set; }
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<StatisticsItem> GetStatistics(DateTime fromDate, DateTime toDate)
        {
           
            List<StatisticsItem> Items = new List<StatisticsItem>();
            if (CurrentUser is null)
            {
                throw new ArgumentException("User is null");
            }

            if (acctype == null)
            {
                var accounts=unitOfWork.Repository<BankAccount>().Get(x => x.Users.Select(a => a.UserId).Contains(CurrentUser.Id));
                var transaction= unitOfWork.Repository<Transaction>().Get(x => x.Users.Select(a => a.UserId).Contains(CurrentUser.Id));
          
            }
            
      
        }
        public IEnumerable<StatisticsItem> GetIncomeStatistics(DateTime fromDate, DateTime toDate, AccountType? acctype = null)



    }
}
