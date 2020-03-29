using Moq;
using NUnit.Framework;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Validators;

namespace SimpleBankingSystem.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class AccountStatusValidatorTests
    {
        private IAccountStatusValidator _target;
        private Mock<IAccountStatus> _statusMock;

        [SetUp]
        public void Setup()
        {
            _target = new AccountStatusValidator();
            _statusMock = new Mock<IAccountStatus>();
        }

        [Test]
        public void WhenAccountStatusIsClosed_ShouldReturnTrue()
        {
            _statusMock.Setup(s => s.Value).Returns(AccountStatusValues.Closed);

            var result = _target.IsClosed(_statusMock.Object);

            Assert.IsTrue(result);
        }

        [TestCase(AccountStatusValues.Verified)]
        [TestCase(AccountStatusValues.Unverified)]
        [TestCase(AccountStatusValues.Frozen)]
        public void WhenAccountStatusIsNotClosed_ShouldReturnFalse(AccountStatusValues statusValue)
        {
            _statusMock.Setup(s => s.Value).Returns(statusValue);

            var result = _target.IsClosed(_statusMock.Object);

            Assert.IsFalse(result);
        }

        [Test]
        public void WhenAccountStatusIsFrozen_ShouldReturnTrue()
        {
            _statusMock.Setup(s => s.Value).Returns(AccountStatusValues.Frozen);

            var result = _target.IsFrozen(_statusMock.Object);

            Assert.IsTrue(result);
        }

        [TestCase(AccountStatusValues.Verified)]
        [TestCase(AccountStatusValues.Unverified)]
        [TestCase(AccountStatusValues.Closed)]
        public void WhenAccountStatusIsNotFrozen_ShouldReturnFalse(AccountStatusValues statusValue)
        {
            _statusMock.Setup(s => s.Value).Returns(statusValue);

            var result = _target.IsFrozen(_statusMock.Object);

            Assert.IsFalse(result);
        }
        
        [TestCase(AccountStatusValues.Unverified)]
        [TestCase(AccountStatusValues.Closed)]
        public void WhenAccountStatusIsUnverifiedOrClosed_ShouldReturnTrue(AccountStatusValues statusValue)
        {
            _statusMock.Setup(s => s.Value).Returns(statusValue);

            var result = _target.IsUnverifiedOrClosed(_statusMock.Object);

            Assert.IsTrue(result);
        }

        [TestCase(AccountStatusValues.Verified)]
        [TestCase(AccountStatusValues.Frozen)]
        public void WhenAccountStatusIsNotUnverifiedNorClosed_ShouldReturnTrue(AccountStatusValues statusValue)
        {
            _statusMock.Setup(s => s.Value).Returns(statusValue);

            var result = _target.IsUnverifiedOrClosed(_statusMock.Object);

            Assert.IsFalse(result);
        }
    }
}
