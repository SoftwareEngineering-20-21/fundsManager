using System;
using System.Threading.Tasks;
using DAL.Domain;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface IBankAccountService
    {
        Task<Transaction> MakeTransaction(BankAccount from, BankAccount to, decimal amount, DateTime date, string description);
        Task<bool> ShareAccount(BankAccount account, string email);
        Task<BankAccount> CreateAccount(AccountType type, string name, Currency currency);
        Task DeleteAccount(BankAccount account);
    }
}