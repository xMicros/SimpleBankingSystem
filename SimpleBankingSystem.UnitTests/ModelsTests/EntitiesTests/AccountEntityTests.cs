using NUnit.Framework;
using SimpleBankingSystem.Domain.Models.Entities;

namespace SimpleBankingSystem.UnitTests.ModelsTests.EntitiesTests
{
    [TestFixture]
    public class AccountEntityTests
    {
        [Test]
        public void WhenCreatingNewAccount_ShouldInitialiseItsProperties()
        {
            var target = new AccountEntity();

            Assert.IsNotNull(target.Id);
            Assert.IsNotNull(target.Balance);
            Assert.IsNotNull(target.Status);
        }
    }
}
