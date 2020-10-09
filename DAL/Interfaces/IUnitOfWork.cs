using System;
using System.Threading.Tasks;
using DAL.Domain;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<T> Repository<T>() where T : BaseEntity;
        public void Save();
        public Task SaveAsync();
    }
}