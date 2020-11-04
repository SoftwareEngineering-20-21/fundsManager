using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private readonly Regex phoneRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
        private User currentUser = null;
        private readonly IUnitOfWork unitOfWork;
        private bool IsValidMail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool ChangeMail(string newMail)
        {
            var emails = unitOfWork.Repository<User>().Get().Select(x => x.Mail);
            if (emails.Contains(newMail) || !IsValidMail(newMail))
            {
                return false;
            }
            currentUser.Mail = newMail;
            unitOfWork.Repository<User>().Update(currentUser);
            unitOfWork.Save();
            return true;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            string currentPassword = currentUser.Password;
            bool validPassword = BCrypt.Net.BCrypt.Verify(oldPassword, currentPassword);
            if (validPassword)
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                currentUser.Password = hashPassword;
                unitOfWork.Repository<User>().Update(currentUser);
                unitOfWork.Save();
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool ChangePhoneNumber(string number)
        {
            if (phoneRegex.IsMatch(number))
            {
                currentUser.Phone = number;
                unitOfWork.Repository<User>().Update(currentUser);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Login(string email, string password)
        {
            User user = unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                currentUser = user;
                return true;
            }
            {
                return false;
            }
        }

        public bool SignUp(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            var existUser = unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email || x.Phone == phoneNumber);
            if (existUser == null && phoneRegex.IsMatch(phoneNumber) && IsValidMail(email)) 
            {
                User user = new User
                {
                    Name = firstName,
                    Surname = lastName,
                    Mail = email,
                    Phone = phoneNumber,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    BankAccounts = new List<UserBankAccount>()
                };
                unitOfWork.Repository<User>().Update(user);
                unitOfWork.Save();
                currentUser = user;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}