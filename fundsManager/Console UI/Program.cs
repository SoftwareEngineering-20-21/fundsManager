using System;
using System.IO;
using DAL.Context;
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
            
            using (FundsContext context = new FundsContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            
            
                context.SaveChanges();
            }


        }
    }
}
