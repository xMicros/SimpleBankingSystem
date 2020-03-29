using NUnit.Framework;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.UnitTests.ModelsTests
{
    [TestFixture]
    public class AccountStatusTests
    {
        private IAccountStatus _target;

        [SetUp]
        public void Setup()
        {
            _target = AccountStatus.CreateWithUnverifiedValue();
        }

        [Test]
        public void WhenCreatingStatusWithUnverifiedValue_ShouldHaveProperValue()
        {
            var expectedStatusValue = AccountStatusValues.Unverified;

            Assert.AreEqual(expectedStatusValue, _target.Value);
        }

        [TestCase(AccountStatusValues.Verified)]
        [TestCase(AccountStatusValues.Closed)]
        [TestCase(AccountStatusValues.Frozen)]
        public void WhenChangingStatus_ShouldUpdateItWithProperValue(AccountStatusValues expectedValue)
        {
            _target.ChangeStatus(expectedValue);

            Assert.AreEqual(expectedValue, _target.Value);
        }
    }
}
