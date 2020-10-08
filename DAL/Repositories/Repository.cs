using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class Repository<T>: IRepository<T> where T:BaseEntity
    {
        private FundsContext context;
        private DbSet<T> dbSet;
        public Repository()
        {
            this.context = new FundsContext();
            dbSet = context.Set<T>();
        }
        public Repository(FundsContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public IEnumerable<T> Get()
        {
            return dbSet;
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            T elem = dbSet.Find(id);
            if (elem != null)
            {
                dbSet.Remove(elem);
            }
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
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
