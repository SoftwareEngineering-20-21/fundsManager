using System;
using DAL.Domain;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface IBankAccountService
    {
        bool MakeTransaction(BankAccount from, BankAccount to, decimal amount, DateTime date);
        bool ShareAccount(BankAccount account, string email);
        bool CreateAccount(AccountType type, string name, Currency currency);
        bool DeleteAccount(BankAccount account);
    }
}