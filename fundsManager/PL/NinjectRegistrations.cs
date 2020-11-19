using System.Net.NetworkInformation;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace PL
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<FundsContext>().ToSelf();
            Bind<IRepository<BankAccount>>().To<Repository<BankAccount>>();
            Bind<IRepository<Currency>>().To<Repository<Currency>>();
            Bind<IRepository<Transaction>>().To<Repository<Transaction>>();
            Bind<IRepository<User>>().To<Repository<User>>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<IBankAccountService>().To<BankAccountService>();
            Bind<ICurrencyService>().To<CurrencyService>();
            Bind<IStatisticsService >().To<StatisticsService>();
        }
    }
}