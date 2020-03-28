using NUnit.Framework;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.UnitTests.ModelsTests
{
    [TestFixture]
    public class AccountStatusTests
    {
        [Test]
        public void WhenCreatingStatusWithUnverifiedValue_ShouldHaveProperValue()
        {
            var expectedStatusValue = AccountStatusValues.Unverified;

            var target = AccountStatus.CreateWithUnverifiedValue();

            Assert.AreEqual(expectedStatusValue, target.Value);
        }

        [TestCase(AccountStatusValues.Verified)]
        [TestCase(AccountStatusValues.Closed)]
        [TestCase(AccountStatusValues.Frozen)]
        public void WhenChangingStatus_ShouldUpdateItWithProperValue(AccountStatusValues expectedValue)
        {
            var target = AccountStatus.CreateWithUnverifiedValue();

            target.ChangeStatus(expectedValue);

            Assert.AreEqual(expectedValue, target.Value);
        }
    }
}
