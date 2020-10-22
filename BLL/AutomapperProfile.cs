using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BLL.DTO;
using DAL.Domain;

namespace BLL
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {

            CreateMap<Currency, CurrencyDTO>()
                .ReverseMap();
            CreateMap<Transaction, TransactionDTO>()
                .ReverseMap();

            CreateMap<BankAccount, BankAccountDTO>()
                .ForMember(x => x.Users, a => a.MapFrom(z => z.Users.Select(a => a.User)));
            CreateMap<BankAccountDTO, BankAccount>()
                .ForMember(e => e.Users, o => o.MapFrom(m => m.Users))
                .AfterMap((m, e) =>
                {
                    foreach (var u in e.Users)
                    {
                        u.BankAccount = e;
                    }
                });


            CreateMap<User, UserDTO>()
                .ForMember(x => x.BankAccounts
                    , a => a.MapFrom(z => z.BankAccounts.Select(u => u.BankAccount)));

            CreateMap<UserDTO, User>()
                .ForMember(ent => ent.BankAccounts, o => o.MapFrom(m => m.BankAccounts))
                .AfterMap((m, e) =>
                {
                    foreach (var ba in e.BankAccounts)
                    {
                        ba.User = e;
                    }
                });
            CreateMap<BankAccountDTO, UserBankAccount>()
                .ForMember(e => e.BankAccount, m => m.MapFrom(mod => mod));
            CreateMap<UserDTO, UserBankAccount>()
                .ForMember(e => e.User, m => m.MapFrom(mod => mod));

        }
    }
}