using NUnit.Framework;
using SimpleBankingSystem.Domain.Commands.DepositMoney;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;

namespace SimpleBankingSystem.UnitTests.CommandsTests
{
    [TestFixture]
    public class DepositMoneyCommandTests
    {
        private IAccountStatusValidator _statusValidator;
        private IAccountEntity _account;

        [SetUp]
        public void Setup()
        {
            _statusValidator = new AccountStatusValidator();
            _account = new AccountEntity();
        }

        [TestCase(AccountStatusValues.Verified)]
        [TestCase(AccountStatusValues.Unverified)]
        public void WhenCommandIsValid_ShouldNotThrowExceptionAndExecuteProperly(AccountStatusValues expectedAccountStatusValue)
        {
            _account.Status.ChangeStatus(expectedAccountStatusValue);
            var expectedAccountBalanceAmount = 50.99m;
            var command = new DepositMoneyCommand
            {
                MoneyAmount = expectedAccountBalanceAmount
            };
            var commandHandler = new DepositMoneyCommandHandler(_statusValidator, _account);

            Assert.DoesNotThrow(() => commandHandler.Execute(command));
            Assert.AreEqual(expectedAccountStatusValue, _account.Status.Value);
            Assert.AreEqual(expectedAccountBalanceAmount, _account.Balance.Amount);
        }

        [Test]
        public void WhenCommandIsValidAndAccountFrozen_ShouldNotThrowExceptionAndExecuteProperly()
        {
            _account.Status.ChangeStatus(AccountStatusValues.Frozen);
            var expectedAccountStatusValue = AccountStatusValues.Verified;
            var expectedAccountBalanceAmount = 50.99m;
            var command = new DepositMoneyCommand
            {
                MoneyAmount = expectedAccountBalanceAmount
            };
            var commandHandler = new DepositMoneyCommandHandler(_statusValidator, _account);

            Assert.DoesNotThrow(() => commandHandler.Execute(command));
            Assert.AreEqual(expectedAccountStatusValue, _account.Status.Value);
            Assert.AreEqual(expectedAccountBalanceAmount, _account.Balance.Amount);
        }

        [Test]
        public void WhenCommandIsNull_ShouldThrowProperExceptionWhileExecuting()
        {
            var commandHandler = new DepositMoneyCommandHandler(_statusValidator, _account);

            Assert.Throws<ArgumentNullException>(() => commandHandler.Execute(null));
        }

        [Test]
        public void WhenValidationFails_ShouldThrowProperExceptionWhileExecuting()
        {
            _account.Status.ChangeStatus(AccountStatusValues.Closed);
            var command = new DepositMoneyCommand
            {
                MoneyAmount = 10.99m
            };
            var commandHandler = new DepositMoneyCommandHandler(_statusValidator, _account);

            Assert.Throws<ForbiddenCommandException>(() => commandHandler.Execute(command));
        }
    }
}
