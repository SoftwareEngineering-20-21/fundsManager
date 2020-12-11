using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Domain;
using DAL.Enums;
using DAL.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// The Bank Account Service class
    /// Implement IBankAccountService interface
    /// </summary>
    public class BankAccountService : IBankAccountService
    {
        
        /// <summary>
        /// Bank accounte service current user
        /// </summary>
        public User CurrentUser { get; set; }
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrencyService currencyService;
        
        /// <summary>
        /// Constructor with paramethr
        /// </summary>
        /// <param name="unitOfWork">unit of work</param>
        /// <param name="currencyService">currency service</param>
        public BankAccountService(IUnitOfWork unitOfWork, ICurrencyService currencyService)
        {
            this.unitOfWork = unitOfWork;
            this.currencyService = currencyService;
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="account">The Bank account</param>
        /// <returns>account score</returns>
        public decimal GetAccountScore(BankAccount account)
        {
            if (account.Type == AccountType.Income)
            {
                return unitOfWork.Repository<Transaction>().Get(x => x.BankAccountFrom.Id == account.Id)
                    .Select(x => x.AmountFrom).Sum();
            }
            else if (account.Type == AccountType.Expence)
            {
                return unitOfWork.Repository<Transaction>().Get(x => x.BankAccountTo.Id == account.Id)
                    .Select(x => x.AmountTo).Sum();
            }
            else
            {
                return unitOfWork.Repository<Transaction>().Get(x => x.BankAccountTo.Id == account.Id)
                           .Select(x => x.AmountTo).Sum() -
                       unitOfWork.Repository<Transaction>().Get(x => x.BankAccountFrom.Id == account.Id)
                           .Select(x => x.AmountFrom).Sum();
            }
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <returns>all user transaction</returns>
        public IEnumerable<Transaction> GetAllUserTransactions()
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            return unitOfWork.Repository<Transaction>().Get(x => x.UserId == CurrentUser.Id);

        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="fromAccount">user transaction from this account</param>
        /// <returns>user transaction from account</returns>
        public IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id && x.BankAccountFrom.Id == fromAccount.Id);
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="toAccount">The bank account that get user transaction</param>
        /// <returns>user transaction to account</returns>
        public IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id && x.BankAccountFrom.Id == toAccount.Id);

        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="dateFrom">transaction from date</param>
        /// <param name="dateTo">transaction to date</param>
        /// <returns>user transaction from dateFrom to dateTo</returns>
        public IEnumerable<Transaction> GetAllUserTransactions(DateTime dateFrom, DateTime dateTo)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id && x.TransactionDate >= dateFrom && x.TransactionDate <= dateTo);
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="fromAccount">user transaction from this account</param>
        /// <param name="dateFrom">user transaction from date</param>
        /// <param name="dateTo">user transaction to date</param>
        /// <returns>user transaction account from dateFrom to dateTo </returns>
        public IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount, DateTime dateFrom, DateTime dateTo)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id &&
                          x.BankAccountFrom.Id == fromAccount.Id).Where(x => x.TransactionDate >= dateFrom && x.TransactionDate <= dateTo);
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="toAccount">The bank account that get user transaction</param>
        /// <param name="dateFrom">user transaction from date</param>
        /// <param name="dateTo">user transaction to date</param>
        /// <returns>user transaction to account from dateFrom to dateTo</returns>
        public IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount, DateTime dateFrom, DateTime dateTo)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id &&
                          x.BankAccountTo.Id == toAccount.Id).Where(x => x.TransactionDate >= dateFrom && x.TransactionDate <= dateTo);
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <returns>user accounts</returns>
        public IEnumerable<BankAccount> GetAllUserAccounts()
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            return unitOfWork.Repository<BankAccount>()
                .Get(x => x.Users.Select(x => x.UserId).Contains(CurrentUser.Id));
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="from">The Bank account that send transaction</param>
        /// <param name="to">The bank account that get transaction</param>
        /// <param name="amount">amount of money</param>
        /// <param name="date">Date creation of transaction</param>
        /// <param name="description">description to transaction</param>
        /// <returns>transaction</returns>
        public async Task<Transaction> MakeTransaction(BankAccount from, BankAccount to, decimal amount, DateTime date, string description)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            if (from is null || to is null)
                throw new ArgumentException("Bank fromAccount is null");

            if (from.Type == AccountType.Income && to.Type == AccountType.Expence)
                throw new ArgumentException("Cannot create transaction from income to expence fromAccount");

            if (from.Type == AccountType.Expence)
                throw new ArgumentException("Cannot create transaction from expence");

            if (from.Type == AccountType.Current && to.Type == AccountType.Income)
                throw new ArgumentException("Cannot create transaction from current to income fromAccount");

            var transaction = new Transaction
            {
                AmountFrom = amount,
                AmountTo = amount * currencyService.GetRate(to.CurrencyType.Code) /
                           currencyService.GetRate(from.CurrencyType.Code),
                BankAccountFrom = from,
                BankAccountTo = to,
                Description = description,
                TransactionDate = date,
                UserId = CurrentUser.Id,
                User = CurrentUser
            };
            await unitOfWork.Repository<Transaction>().AddAsync(transaction);
            await unitOfWork.SaveAsync();
            return transaction;
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="account">The bank account to share</param>
        /// <param name="email">Email to share account</param>
        /// <returns>if account shared</returns>
        public bool ShareAccount(BankAccount account, string email)
        {
            var user = unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email);
            if (user == null || account == null)
            {
                return false;
            }
            user.BankAccounts.Add(new UserBankAccount
            {
                BankAccount = account
            });
            unitOfWork.Repository<User>().Update(user);
            unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="type">Select account type</param>
        /// <param name="name">name of account</param>
        /// <param name="currency">select currency</param>
        /// <returns>created account</returns>
        public async Task<BankAccount> CreateAccount(AccountType type, string name, Currency currency)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            if (currency is null)
            {
                throw new ArgumentException("Currency is null");
            }

            var account = new BankAccount
            {
                CurrencyType = currency,
                Name = name,
                Type = type
            };
            CurrentUser.BankAccounts.Add(
                new UserBankAccount
                {
                    BankAccount = account
                });
            unitOfWork.Repository<User>().Update(CurrentUser);
            await unitOfWork.SaveAsync();
            return account;
        }

        /// <summary>
        /// Implementation of IBankAccountService
        /// </summary>
        /// <param name="account">The bank account to delete</param>
        /// <returns>if account deleted</returns>
        public async Task DeleteAccount(BankAccount account)
        {
            unitOfWork.Repository<BankAccount>().Delete(account);
            await unitOfWork.SaveAsync();
        }
    }
}