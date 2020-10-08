using System.Collections.Generic;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;

namespace DAL.Repositories
{  
    public class UserRepository:IRepository<User>
    {
        private FundsContext context;
        public UserRepository(FundsContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> Get()
        {
            throw new System.NotImplementedException();
        }

        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}