using System;
using System.Collections.Generic;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {

        private readonly FundsContext context;
        private bool disposed;
        private BankAccountRepository bankAccountRepository;
        private UserRepository userRepository;
        private TransactionRepository transactionRepository;
        public UnitOfWork()
        {
            this.context = new FundsContext();
        }
        public UnitOfWork(FundsContext context)
        {
            this.context = context;
        }
        public TransactionRepository GeTransactionRepository
        {
            get
            {
                if (transactionRepository is null)
                {
                    transactionRepository = new TransactionRepository(context);
                }

                return transactionRepository;
            }
        }

        public BankAccountRepository GetBankAccountRepository
        {
            get
            {
                if (bankAccountRepository is null)
                {
                    bankAccountRepository = new BankAccountRepository(context);
                }

                return bankAccountRepository;
            }
        }

        public UserRepository GetUserRepository
        {
            get
            {
                if (userRepository is null)
                {
                    return  new UserRepository(context);
                }

                return userRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}