using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class TestUserService
    {
        const string password = "fancy";
        const string passwordHash = "$2a$04$mHManSTXI9OrvFGtT3Nsz.eyyGumdhnj/oRAGINntWjZ7D/rtupx2";

        [Test]
        public void TestLogin()
        {
            User testUser = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test@test.com",
                Phone = "0999999999",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(new List<User> { testUser });
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);

            var exc = Assert.Throws<ArgumentException>(() => service.Login("test@test.com", "fanc"));
            Assert.AreEqual(exc.Message, "The email or password is incorrect.");
            User testLogin = service.Login("test@test.com", password);
            Assert.AreEqual(testLogin.Mail, "test@test.com");
            Assert.AreEqual(testLogin.Password, passwordHash);
        }

        [Test]
        public void TestChangePassword()
        {
            User testUser = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test@test.com",
                Phone = "0999999999",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            List<User> users = new List<User> { testUser };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(users);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);
            _ = service.Login("test@test.com", password);

            Assert.IsFalse(service.ChangePassword("fanc", "anotherone"));
            Assert.IsTrue(service.ChangePassword(password, "anotherone"));
            User user = ((List<User>)mock.Object.Get())[0];
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("anotherone", user.Password));
        }

        [Test]
        public void TestChangeEmail()
        {
            User testUser = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test@test.com",
                Phone = "0999999999",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            User testUser2 = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test2@test.com",
                Phone = "0999999999",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            List<User> users = new List<User> { testUser, testUser2 };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(users);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);
            _ = service.Login("test@test.com", password);

            Assert.IsFalse(service.ChangeMail("test2@test.com"));
            Assert.IsFalse(service.ChangeMail("test"));
            Assert.IsTrue(service.ChangeMail("test3@test.com"));
            User user = ((List<User>)mock.Object.Get())[0];
            Assert.AreEqual(user.Mail, "test3@test.com");
        }

        [Test]
        public void TestChangePhoneNumber()
        {
            User testUser = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test@test.com",
                Phone = "0999999999",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            User testUser2 = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test2@test.com",
                Phone = "0999999998",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            List<User> users = new List<User> { testUser, testUser2 };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(users);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);
            _ = service.Login("test@test.com", password);

            Assert.IsFalse(service.ChangePhoneNumber("0999999998"));
            Assert.IsFalse(service.ChangePhoneNumber("0999999"));
            Assert.IsTrue(service.ChangePhoneNumber("0999999997"));
            User user = ((List<User>)mock.Object.Get())[0];
            Assert.AreEqual(user.Phone, "0999999997");
        }

        [Test]
        public void TestSignUp()
        {
            User testUser = new User
            {
                Name = "test",
                Surname = "test",
                Mail = "test@test.com",
                Phone = "0999999999",
                Password = passwordHash,
                BankAccounts = new List<UserBankAccount>()
            };
            List<User> users = new List<User> { testUser };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(users);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);

            string message = "Phone or mail incorrect";
            var exc = Assert.Throws<ArgumentException>(() => service.SignUp("test2", "test2", "test@test.com", "0999999999", password));
            Assert.AreEqual(exc.Message, message);
            exc = Assert.Throws<ArgumentException>(() => service.SignUp("test2", "test2", "test2@test.com", "0999999999", password));
            Assert.AreEqual(exc.Message, message);
            User user = service.SignUp("test2", "test2", "test2@test.com", "0999999998", password);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Mail, "test2@test.com");
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify(password, user.Password));
        }
    }
}