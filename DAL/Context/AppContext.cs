using System;
using System.Collections.Generic;
using System.Text;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class AppContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<TransactionHistory> Transaction { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBankAccount>().HasKey(t => new {t.UserId, t.BankAccountId});

            modelBuilder.Entity<UserBankAccount>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.BankAccounts)
                .HasForeignKey(pt => pt.BankAccountId);

            modelBuilder.Entity<UserBankAccount>()
                .HasOne(pt => pt.BankAccount)
                .WithMany(p => p.Users)
                .HasForeignKey(pt => pt.UserId);
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}