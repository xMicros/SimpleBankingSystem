﻿using NUnit.Framework;
using SimpleBankingSystem.Domain.Commands.CloseAccount;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;

namespace SimpleBankingSystem.UnitTests.CommandsTests
{
    [TestFixture]
    public class CloseAccountCommandTests
    {
        private IAccountStatusValidator _statusValidator;
        private IAccountEntity _account;

        [SetUp]
        public void Setup()
        {
            _statusValidator = new AccountStatusValidator();
            _account = new AccountEntity();
        }

        [Test]
        public void WhenCommandIsValid_ShouldNotThrowExceptionAndExecuteProperly()
        {
            _account.Status.ChangeStatus(AccountStatusValues.Verified);
            var expectedAccountStatusValue = AccountStatusValues.Closed;
            var command = new CloseAccountCommand();
            var commandHandler = new CloseAccountCommandHandler(_statusValidator, _account);

            Assert.DoesNotThrow(() => commandHandler.Execute(command));
            Assert.AreEqual(expectedAccountStatusValue, _account.Status.Value);
        }

        [Test]
        public void WhenCommandIsNull_ShouldThrowProperExceptionWhileExecuting()
        {
            var commandHandler = new CloseAccountCommandHandler(_statusValidator, _account);

            Assert.Throws<ArgumentNullException>(() => commandHandler.Execute(null));
        }

        [TestCase(AccountStatusValues.Unverified)]
        [TestCase(AccountStatusValues.Closed)]
        public void WhenValidationFails_ShouldThrowProperExceptionWhileExecuting(AccountStatusValues invalidAccountStatusValue)
        {
            _account.Status.ChangeStatus(invalidAccountStatusValue);
            var command = new CloseAccountCommand();
            var commandHandler = new CloseAccountCommandHandler(_statusValidator, _account);

            Assert.Throws<ForbiddenCommandException>(() => commandHandler.Execute(command));
        }
    }
}