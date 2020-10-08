using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class BankAccountRepository:IRepository<BankAccount>
    {
        private FundsContext context;
        public BankAccountRepository(FundsContext context)
        {
            this.context = context;
        }

        public IEnumerable<BankAccount> Get()
        {
            throw new NotImplementedException();
        }

        public BankAccount Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
