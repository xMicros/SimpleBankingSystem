using NUnit.Framework;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus;
using System;

namespace SimpleBankingSystem.UnitTests.QueriesTests
{
    [TestFixture]
    public class GetBalanceAndStatusQueryTests
    {
        private IAccountEntity _account;

        [SetUp]
        public void Setup()
        {
            _account = new AccountEntity();
        }

        [Test]
        public void WhenQueryIsValid_ShouldExecuteAndReturnProperData()
        {
            var expectedBalanceAmount = 0;
            var expectedStatusValue = "Unverified";
            var query = new GetBalanceAndStatusQuery();
            var queryHandler = new GetBalanceAndStatusQueryHandler(_account);

            var queryResponse = queryHandler.Execute(query);
            
            Assert.AreEqual(expectedBalanceAmount, queryResponse.Balance);
            Assert.AreEqual(expectedStatusValue, queryResponse.Status);
        }

        [Test]
        public void WhenQueryIsNull_ShouldThrowProperExceptionWhileExecuting()
        {
            var queryHandler = new GetBalanceAndStatusQueryHandler(_account);

            Assert.Throws<ArgumentNullException>(() => queryHandler.Execute(null));
        }
    }
}
