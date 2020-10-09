using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public sealed class FundsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public FundsContext()
            : base(new DbContextOptionsBuilder<FundsContext>().UseSqlServer(
                @"Data Source=DESKTOP-1CLE678\SQLEXPRESS;Initial Catalog=FundsDb;Integrated Security=True").Options)
        { 
            Database.EnsureCreated();
        }
        public FundsContext(DbContextOptions<FundsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
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


            modelBuilder.Entity<BankAccount>()
                .HasOne(p => p.CurrencyType)
                .WithMany(b => b.BankAccounts)
                .HasForeignKey(x => x.CurrencyTypeId);


            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.Login).IsUnique();
            });
        }
    }
}