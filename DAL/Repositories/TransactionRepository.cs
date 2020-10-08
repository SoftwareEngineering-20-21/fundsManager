using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class TransactionRepository:IRepository<Transaction>
    {
        private FundsContext context;
        public TransactionRepository(FundsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Transaction> Get()
        {
            throw new NotImplementedException();
        }

        public Transaction Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
