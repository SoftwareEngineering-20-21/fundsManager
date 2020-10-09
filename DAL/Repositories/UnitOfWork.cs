using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork :  IUnitOfWork
    {
   
        private readonly FundsContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;
        public UnitOfWork()
        {
            this.context = new FundsContext();
        }
        public UnitOfWork(FundsContext context)
        {
            this.context = context;
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            repositories ??= new Dictionary<string, object>();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }


        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                context.Dispose();
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
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}