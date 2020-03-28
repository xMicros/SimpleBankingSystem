using NUnit.Framework;
using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.UnitTests.ModelsTests
{
    [TestFixture]
    public class AccountBalanceTests
    {
        [Test]
        public void WhenCreatingBalanceWithZeroAmount_ShouldHaveProperAmount()
        {
            var expectedBalanceAmount = 0;

            var target = AccountBalance.CreateWithZeroAmount();

            Assert.AreEqual(expectedBalanceAmount, target.Amount);
        }

        [Test]
        public void WhenAddingMoney_ShouldUpdateBalanceWithProperAmount()
        {
            var expectedBalanceAmount = 100.50m;
            var target = AccountBalance.CreateWithZeroAmount();

            target.AddMoney(expectedBalanceAmount);

            Assert.AreEqual(expectedBalanceAmount, target.Amount);
        }

        [Test]
        public void WhenSubtractingMoney_ShouldUpdateBalanceWithProperAmount()
        {
            var moneyToBeAdded = 100.50m;
            var moneyToBeSubtracted = 40.20m;
            var expectedBalanceAmount = 60.30m;
            var target = AccountBalance.CreateWithZeroAmount();
            target.AddMoney(moneyToBeAdded);

            target.SubtractMoney(moneyToBeSubtracted);

            Assert.AreEqual(expectedBalanceAmount, target.Amount);
        }
    }
}
