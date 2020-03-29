using Moq;
using NUnit.Framework;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Validators;

namespace SimpleBankingSystem.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class AccountBalanceValidatorTests
    {
        private IAccountBalanceValidator _target;
        private Mock<IAccountBalance> _balanceMock;

        [SetUp]
        public void Setup()
        {
            _target = new AccountBalanceValidator();
            _balanceMock = new Mock<IAccountBalance>();
        }

        [Test]
        public void WhenBalanceAmountIsSufficient_ShouldAllowWithdrawingMoney()
        {
            _balanceMock.Setup(b => b.Amount).Returns(20.50m);
            var moneyToWithdraw = 10.99m;

            var result = _target.CanWithdrawMoney(_balanceMock.Object, moneyToWithdraw);

            Assert.IsTrue(result);
        }

        [TestCase(0)]
        [TestCase(10)]
        public void WhenBalanceAmountIsInsufficient_ShouldNotAllowWithdrawingMoney(decimal balanceAmountValue)
        {
            _balanceMock.Setup(b => b.Amount).Returns(balanceAmountValue);
            var moneyToWithdraw = 10.99m;

            var result = _target.CanWithdrawMoney(_balanceMock.Object, moneyToWithdraw);

            Assert.IsFalse(result);
        }
    }
}
