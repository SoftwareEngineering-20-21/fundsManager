using DAL.Domain;
using DAL.Enums;
using System;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        bool Login(string email, string password);
        bool SignUp(string firstName, string lastName, string email, string phoneNumber, string password);
        bool ChangePassword(string oldPassword, string newPassword);
        bool ChangeMail(string newMail);
        bool ChangePhoneNumber(string number);
    }
}