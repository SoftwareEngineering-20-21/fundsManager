using System.Collections.Generic;
using BLL.Services;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class TestUserService
    {
        const string password = "fancy";
        const string passwordHash = "$2a$04$mHManSTXI9OrvFGtT3Nsz.eyyGumdhnj/oRAGINntWjZ7D/rtupx2";
        User testUser = new User
        {
            Name = "test",
            Surname = "test",
            Mail = "test@test.com",
            Phone = "0999999999",
            Password = passwordHash,
            BankAccounts = new List<UserBankAccount>()
        };
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(new List<User> { testUser });
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);
        }

        [Test]
        public void TestLogin()
        {
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(new List<User> { testUser });
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<User>()).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object);
            User testLogin = service.Login("test@test.com", password);
            Assert.AreEqual(testLogin.Name, "test");
        }
    }
}