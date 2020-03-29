using NUnit.Framework;
using SimpleBankingSystem.Domain.Commands.WithdrawMoney;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;

namespace SimpleBankingSystem.UnitTests.CommandsTests
{
    [TestFixture]
    public class WithdrawMoneyCommandTests
    {
        private IAccountStatusValidator _statusValidator;
        private IAccountBalanceValidator _balanceValidator;
        private IAccountEntity _account;

        [SetUp]
        public void Setup()
        {
            _statusValidator = new AccountStatusValidator();
            _balanceValidator = new AccountBalanceValidator();
            _account = new AccountEntity();
        }

        [Test]
        public void WhenCommandIsValid_ShouldNotThrowExceptionAndExecuteProperly()
        {
            var expectedAccountStatusValue = AccountStatusValues.Verified;
            _account.Status.ChangeStatus(expectedAccountStatusValue);
            _account.Balance.AddMoney(100);
            var moneyToWithdraw = 49.01m;
            var expectedAccountBalanceAmount = 50.99m;
            var command = new WithdrawMoneyCommand
            {
                MoneyAmount = moneyToWithdraw
            };
            var commandHandler = new WithdrawMoneyCommandHandler(_statusValidator, _balanceValidator, _account);

            Assert.DoesNotThrow(() => commandHandler.Execute(command));
            Assert.AreEqual(expectedAccountStatusValue, _account.Status.Value);
            Assert.AreEqual(expectedAccountBalanceAmount, _account.Balance.Amount);
        }

        [Test]
        public void WhenCommandIsValidAndAccountFrozen_ShouldNotThrowExceptionAndExecuteProperly()
        {
            _account.Status.ChangeStatus(AccountStatusValues.Frozen);
            _account.Balance.AddMoney(100);
            var expectedAccountStatusValue = AccountStatusValues.Verified;
            var moneyToWithdraw = 49.01m;
            var expectedAccountBalanceAmount = 50.99m;
            var command = new WithdrawMoneyCommand
            {
                MoneyAmount = moneyToWithdraw
            };
            var commandHandler = new WithdrawMoneyCommandHandler(_statusValidator, _balanceValidator, _account);

            Assert.DoesNotThrow(() => commandHandler.Execute(command));
            Assert.AreEqual(expectedAccountStatusValue, _account.Status.Value);
            Assert.AreEqual(expectedAccountBalanceAmount, _account.Balance.Amount);
        }

        [Test]
        public void WhenCommandIsNull_ShouldThrowProperExceptionWhileExecuting()
        {
            var commandHandler = new WithdrawMoneyCommandHandler(_statusValidator, _balanceValidator, _account);

            Assert.Throws<ArgumentNullException>(() => commandHandler.Execute(null));
        }

        [TestCase(AccountStatusValues.Unverified)]
        [TestCase(AccountStatusValues.Closed)]
        public void WhenStatusValidationFails_ShouldThrowProperExceptionWhileExecuting(AccountStatusValues invalidAccountStatusValue)
        {
            _account.Status.ChangeStatus(invalidAccountStatusValue);
            var command = new WithdrawMoneyCommand
            {
                MoneyAmount = 10.99m
            };
            var commandHandler = new WithdrawMoneyCommandHandler(_statusValidator, _balanceValidator, _account);

            Assert.Throws<ForbiddenCommandException>(() => commandHandler.Execute(command));
        }

        [TestCase(0)]
        [TestCase(10)]
        public void WhenStatusValidationFails_ShouldThrowProperExceptionWhileExecuting(decimal insufficientAccountBalanceAmount)
        {
            _account.Status.ChangeStatus(AccountStatusValues.Verified);
            _account.Balance.AddMoney(insufficientAccountBalanceAmount);
            var command = new WithdrawMoneyCommand
            {
                MoneyAmount = 10.99m
            };
            var commandHandler = new WithdrawMoneyCommandHandler(_statusValidator, _balanceValidator, _account);

            Assert.Throws<ForbiddenCommandException>(() => commandHandler.Execute(command));
        }
    }
}
