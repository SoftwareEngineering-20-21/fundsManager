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
    /// <summary>
    /// User Service class
    /// Implement IUserService
    /// </summary>
    
    public class UserService : IUserService
    {
        private readonly Regex phoneRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
        
        /// <summary>
        /// User service current user
        /// </summary>
        public User CurrentUser { get; private set; }
        
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="emailaddress">select email</param>
        /// <returns>if emailaddress valid</returns>
        public bool IsValidMail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Constructor with paramethr
        /// </summary>
        /// <param name="unitOfWork">unit of work</param>
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CurrentUser = null;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="newMail">new email</param>
        /// <returns>if email changed</returns>
        public bool ChangeMail(string newMail)
        {
            bool changed = false;
            var emails = unitOfWork.Repository<User>().Get().Select(x => x.Mail);
            if (!emails.Contains(newMail) && IsValidMail(newMail))
            {
                CurrentUser.Mail = newMail;
                unitOfWork.Repository<User>().Update(CurrentUser);
                unitOfWork.Save();
                changed = true;
            }
            return changed;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="oldPassword">password to change</param>
        /// <param name="newPassword">new password</param>
        /// <returns>if password changed</returns>
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

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="number">new phone number</param>
        /// <returns>if phone number changed</returns>
        public bool ChangePhoneNumber(string number)
        {
            bool changed = false;
            var numbers = unitOfWork.Repository<User>().Get().Select(x => x.Phone);
            if (phoneRegex.IsMatch(number) && !numbers.Contains(number))
            {
                CurrentUser.Phone = number;
                unitOfWork.Repository<User>().Update(CurrentUser);
                unitOfWork.Save();
                changed = true;
            }
            return changed;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <returns>logined user</returns>
        public User Login(string email, string password)
        {
            User user = unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                CurrentUser = user;
            }
            else
            {
                throw new ArgumentException("The email or password is incorrect.");
            }
            return CurrentUser;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="firstName">user name</param>
        /// <param name="lastName">user last name</param>
        /// <param name="email">user email</param>
        /// <param name="phoneNumber">user phone number</param>
        /// <param name="password">user password</param>
        /// <returns>registred user</returns>
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
            }
            else
            {
                throw new ArgumentException("Phone or mail incorrect");
            }
            return CurrentUser;
        }
    }
}