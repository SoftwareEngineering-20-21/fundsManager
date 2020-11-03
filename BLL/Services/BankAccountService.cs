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
    public class BankAccountService : IBankAccountService
    {
        public User CurrentUser { get; set; }
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrencyService currencyService;
        public BankAccountService(IUnitOfWork unitOfWork, ICurrencyService currencyService)
        {
            this.unitOfWork = unitOfWork;
            this.currencyService = currencyService;
        }

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

        public IEnumerable<Transaction> GetAllUserTransactions()
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            return unitOfWork.Repository<Transaction>().Get(x => x.UserId == CurrentUser.Id);

        }

        public IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id && x.BankAccountFrom.Id == fromAccount.Id);
        }

        public IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id && x.BankAccountFrom.Id == toAccount.Id);

        }

        public IEnumerable<Transaction> GetAllUserTransactions(DateTime dateFrom, DateTime dateTo)
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }
            return unitOfWork.Repository<Transaction>()
                .Get(x => x.UserId == CurrentUser.Id && x.TransactionDate >= dateFrom && x.TransactionDate <= dateTo);
        }

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

        public IEnumerable<BankAccount> GetAllUserAccounts()
        {
            if (CurrentUser is null)
            {
                throw new ArgumentException("Current user is null");
            }

            return unitOfWork.Repository<BankAccount>()
                .Get(x => x.Users.Select(x => x.UserId).Contains(CurrentUser.Id));
        }

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
        public async Task<bool> ShareAccount(BankAccount account, string email)
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
            await unitOfWork.SaveAsync();
            return true;
        }

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

        public async Task DeleteAccount(BankAccount account)
        {
            unitOfWork.Repository<BankAccount>().Delete(account);
            await unitOfWork.SaveAsync();
        }
    }
}