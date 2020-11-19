using System.Collections.Generic;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class TestUserService
    {
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(new List<User>());
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}