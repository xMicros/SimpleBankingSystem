using System;

namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQuery : IQuery<GetBalanceAndStatusQueryResponse>
    {
        public Guid AccountId { get; set; }
    }
}
