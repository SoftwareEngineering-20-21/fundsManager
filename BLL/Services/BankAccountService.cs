using System;
using System.Linq;
using BLL.Interfaces;
using DAL.Domain;
using DAL.Enums;
using DAL.Interfaces;

namespace BLL.Services
{
    public class BankAccountService:IBankAccountService
    {
        public User CurrentUser { get; set; }
        private readonly IUnitOfWork unitOfWork;
        public BankAccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool MakeTransaction(BankAccount from, BankAccount to, decimal amount, DateTime date)
        {
            throw new NotImplementedException();
        }
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

        public bool CreateAccount(AccountType type, string name, Currency currency)
        {
            if (CurrentUser != null)
            {
                CurrentUser.BankAccounts.Add(
                    new UserBankAccount
                    {
                        BankAccount = new BankAccount
                        {
                            CurrencyType = currency,
                            Name = name,
                            Type = type
                        }
                    });
                unitOfWork.Repository<User>().Update(CurrentUser);
                unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
        public bool DeleteAccount(BankAccount account)
        {
            try
            {
                unitOfWork.Repository<BankAccount>().Delete(account);
                unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}