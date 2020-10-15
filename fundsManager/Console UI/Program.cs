using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DAL.Context;
using DAL.Domain;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Console_UI
{
    class Program
    {
        static void Main(string[] args)
        {   
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                //seed(unitOfWork);
                //unitOfWork.Save();
                //Console.WriteLine(unitOfWork.Repository<BankAccount>().Get(3).CurrencyType);

                Console.WriteLine("Users");
                foreach (var i in unitOfWork.Repository<User>().Get())
                {
                    Console.Write(i.Id + "\t");
                    Console.Write(i.Login + "\t");
                    Console.Write(i.Password + "\t");
                    Console.Write(i.Mail + "\t");
                   Console.Write(i.Name + "\t");
                   Console.Write(i.Surname + "\t");
                   Console.Write(i.Phone.ToString() + "\t");
                   Console.WriteLine();
                }
                
                Console.WriteLine("BankAccounts");
                foreach (var i in unitOfWork.Repository<BankAccount>().Get())
                {
                    Console.WriteLine(i.CurrencyType.Code);
                    Console.WriteLine(i.Users.Count);

                    Console.Write(i.Id + "\t");
                    Console.Write(i.CurrencyTypeId + "\t");
                    Console.Write(i.Name + "\t");
                    Console.Write(i.Type.ToString() + "\t");
                    Console.Write(i.Name + "\t");
                    Console.WriteLine();
                }
                Console.WriteLine("Transaction");
                foreach (var i in unitOfWork.Repository<Transaction>().Get())
                {
                    Console.Write(i.Id.ToString() + "\t");
                    Console.Write(i.AmountFrom.ToString() + "\t");
                    Console.Write(i.AmountTo.ToString() + "\t");
                    Console.Write(i.BankAccountFrom.Id.ToString() + "\t");
                    Console.Write(i.BankAccountTo.Id.ToString() + "\t");
                    Console.Write(i.TransactionDate.ToString() + "\t");
                    Console.WriteLine();
                }
                Console.WriteLine("Currency");
                foreach (var i in unitOfWork.Repository<Currency>().Get())
                {
                    Console.Write(i.Id.ToString() + "\t");
                    Console.Write(i.Code.ToString() + "\t");
                    Console.WriteLine();
                }
               
               
            }
        }
        public static void seed(IUnitOfWork unitOfWork)
        {
            List<Currency> cur = new List<Currency>
            {
                new Currency{Code = "USD"},
                new Currency{Code = "EUR"},
                new Currency{Code = "RUB"},
                new Currency{Code = "ZAR"},
                new Currency{Code = "GBP"}
            };
            List<User> users = new List<User>
            {
                new User{Surname = "Surname3",Mail = "mail3",Name = "Name3",Phone = "0684565728",Login ="Login3",Password = "Password3"},
                new User{Surname = "Surname4",Mail = "mail4",Name = "Name4",Phone = "0684565738",Login ="Login4",Password = "Password4"},
                new User{Surname = "Surname1",Mail = "mail1",Name = "Name1",Phone = "0684565708",Login ="Login1",Password = "Password1"},
                new User{Surname = "Surname5",Mail = "mail5",Name = "Name5",Phone = "0684565748",Login ="Login5",Password = "Password5"},
                new User{Surname = "Surname6",Mail = "mail6",Name = "Name6",Phone = "0684565758",Login ="Login6",Password = "Password6"},
                new User{Surname = "Surname7",Mail = "mail7",Name = "Name7",Phone = "0684565768",Login ="Login7",Password = "Password7"},
                new User{Surname = "Surname2",Mail = "mail2",Name = "Name2",Phone = "0684565701",Login ="Login2",Password = "Password2"}

            };

            List<BankAccount> accounts = new List<BankAccount>
            {
                new BankAccount{CurrencyType = cur[0],Name="Banck1",Type = AccountType.Current,Users 
                    = new List<UserBankAccount>
                    {
                        new UserBankAccount{User = users[0]}
                    }},
                new BankAccount{CurrencyType = cur[1],Name="Banck2",Type = AccountType.Income,Users
                    = new List<UserBankAccount>
                    {
                        new UserBankAccount{User = users[0]}
                    }},
                new BankAccount{CurrencyType = cur[2],Name="Banck3",Type = AccountType.Expence,Users
                    = new List<UserBankAccount>
                    {
                        new UserBankAccount{User = users[3]}
                    }},
                new BankAccount{CurrencyType = cur[1],Name="Banck4",Type = AccountType.Current,Users
                    = new List<UserBankAccount>
                    {
                        new UserBankAccount{User = users[2]}
                    }},
                new BankAccount{CurrencyType = cur[0],Name="Banck5",Type = AccountType.Income,Users
                    = new List<UserBankAccount>
                    {
                        new UserBankAccount{User = users[1]}
                    }}
            };
            List<Transaction> transactions =new  List<Transaction>
            {
                new Transaction{AmountFrom = 12121,AmountTo = 4343,BankAccountFrom = accounts[0],BankAccountTo = accounts[1],Description = "description1",TransactionDate = DateTime.Now},
                new Transaction{AmountFrom = 3213,AmountTo = 111,BankAccountFrom = accounts[1],BankAccountTo = accounts[0],Description = "description2",TransactionDate = DateTime.Now},
                new Transaction{AmountFrom = 5435,AmountTo = 22,BankAccountFrom = accounts[2],BankAccountTo = accounts[2],Description = "description3",TransactionDate = DateTime.Now},
                new Transaction{AmountFrom = 7676,AmountTo = 333,BankAccountFrom = accounts[3],BankAccountTo = accounts[3],Description = "description4",TransactionDate = DateTime.Now},
                new Transaction{AmountFrom = 87687,AmountTo = 444,BankAccountFrom = accounts[0],BankAccountTo = accounts[0],Description = "description5",TransactionDate = DateTime.Now},
                new Transaction{AmountFrom = 889,AmountTo = 555,BankAccountFrom = accounts[1],BankAccountTo = accounts[2],Description = "description5",TransactionDate = DateTime.Now}
            };
            foreach (var i in cur)
            {
                unitOfWork.Repository<Currency>().Update(i);
            }
            foreach (var i in users)
            {
                unitOfWork.Repository<User>().Update(i);
            }
            foreach (var i in accounts)
            {
                unitOfWork.Repository<BankAccount>().Update(i);
            }
            foreach (var i in transactions)
            {
                unitOfWork.Repository<Transaction>().Update(i);
            }
        }
    }
}
