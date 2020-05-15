using Moq;
using NUnit.Framework;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace SimpleBankingSystem.UnitTests.RepositoriesTests
{
    [TestFixture]
    public class DummyAccountRepositoryTests
    {
        [Test]
        public async Task WhenGettingAccountById_ShouldReturnProperObject()
        {
            var accountId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var accountMock = new Mock<IAccountEntity>();
            accountMock.Setup(a => a.Id).Returns(accountId);
            var target = new DummyAccountRepository(accountMock.Object);

            var retrievedAccount = await target.GetById(accountId);

            Assert.IsNotNull(retrievedAccount);
            Assert.AreEqual(accountMock.Object.Id, retrievedAccount.Id);
        }
    }
}
