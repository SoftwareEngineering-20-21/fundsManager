using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}