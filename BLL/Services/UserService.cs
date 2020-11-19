using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System;
using System.Runtime.InteropServices;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private readonly Regex phoneRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
        public User CurrentUser { get; private set; }
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
            CurrentUser = null;
        }

        public bool ChangeMail(string newMail)
        {
            bool changed = false;
            var emails = unitOfWork.Repository<User>().Get().Select(x => x.Mail);
            if (!emails.Contains(newMail) || IsValidMail(newMail))
            {
                CurrentUser.Mail = newMail;
                unitOfWork.Repository<User>().Update(CurrentUser);
                unitOfWork.Save();
                changed = true;
            }
            return changed;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            bool changed = false;
            string currentPassword = CurrentUser.Password;
            bool validPassword = BCrypt.Net.BCrypt.Verify(oldPassword, currentPassword);
            if (validPassword)
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                CurrentUser.Password = hashPassword;
                unitOfWork.Repository<User>().Update(CurrentUser);
                unitOfWork.Save();
                changed = true;
            }
            return changed;
        }

        public bool ChangePhoneNumber(string number)
        {
            bool changed = false;
            if (phoneRegex.IsMatch(number))
            {
                CurrentUser.Phone = number;
                unitOfWork.Repository<User>().Update(CurrentUser);
                unitOfWork.Save();
                changed = true;
            }
            return changed;
        }

        public User Login(string email, string password)
        {
            User user = unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                CurrentUser = user;
            }
            return CurrentUser;
        }

        public User SignUp(string firstName, string lastName, string email, string phoneNumber, string password)
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
                CurrentUser = user;
                return CurrentUser;
            }
            else
            {
                return CurrentUser;
            }
        }
    }
}