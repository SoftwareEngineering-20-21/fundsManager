using System;
using System.Collections.Generic;
using System.IO;
using DAL.Context;
using DAL.Domain;
using DAL.Enums;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Console_UI
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<FundsContext> optionsBuilder = new DbContextOptionsBuilder<FundsContext>();
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-1CLE678\SQLEXPRESS;Initial Catalog=FundsDb;Integrated Security=True");
            
            
            var context = new FundsContext(optionsBuilder.Options);
           //context.Database.EnsureCreated();
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                
                unitOfWork.Repository<User>().Update(new User{Login =  "Petuh",Password = "Petuh" ,Details = new UserDetails{Name = "Nazar",Mail = "Kuchma@gmail.com"} 
                    , BankAccounts = new List<UserBankAccount>{new UserBankAccount
                    {
                       BankAccount  = new BankAccount{CurrencyType = new Currency{Code = "USD"},Name = "Na Pivo",Type = AccountType.Income}
                    }}});

                unitOfWork.Save();
            }


        }
    }
}
