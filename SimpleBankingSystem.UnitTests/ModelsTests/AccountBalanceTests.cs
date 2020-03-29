using NUnit.Framework;
using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.UnitTests.ModelsTests
{
    [TestFixture]
    public class AccountBalanceTests
    {
        private IAccountBalance _target;

        [SetUp]
        public void Setup()
        {
            _target = AccountBalance.CreateWithZeroAmount();
        }

        [Test]
        public void WhenCreatingBalanceWithZeroAmount_ShouldHaveProperAmount()
        {
            var expectedBalanceAmount = 0;

            Assert.AreEqual(expectedBalanceAmount, _target.Amount);
        }

        [Test]
        public void WhenAddingMoney_ShouldUpdateBalanceWithProperAmount()
        {
            var expectedBalanceAmount = 100.50m;

            _target.AddMoney(expectedBalanceAmount);

            Assert.AreEqual(expectedBalanceAmount, _target.Amount);
        }

        [Test]
        public void WhenSubtractingMoney_ShouldUpdateBalanceWithProperAmount()
        {
            var moneyToBeAdded = 100.50m;
            var moneyToBeSubtracted = 40.20m;
            var expectedBalanceAmount = 60.30m;
            _target.AddMoney(moneyToBeAdded);

            _target.SubtractMoney(moneyToBeSubtracted);

            Assert.AreEqual(expectedBalanceAmount, _target.Amount);
        }
    }
}
