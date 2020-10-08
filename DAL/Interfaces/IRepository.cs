using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Domain;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Delete(T entity);
        void Delete(int id);
        void Update(T entity);
        void Save();
        Task SaveAsync();


    }
}