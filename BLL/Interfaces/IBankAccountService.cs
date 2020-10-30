using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Domain;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface IBankAccountService
    {
        IEnumerable<Transaction> GetAllUserTransactions();
        IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount);
        IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount);
        IEnumerable<Transaction> GetAllUserTransactions(DateTime dateFrom);
        IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount, DateTime dateFrom);
        IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount, DateTime dateFrom);
        IEnumerable<BankAccount> GetAllUserAccounts();
        Task<Transaction> MakeTransaction(BankAccount from, BankAccount to, decimal amount, DateTime date, string description);
        Task<bool> ShareAccount(BankAccount account, string email);
        Task<BankAccount> CreateAccount(AccountType type, string name, Currency currency);
        Task DeleteAccount(BankAccount account);
    }
}