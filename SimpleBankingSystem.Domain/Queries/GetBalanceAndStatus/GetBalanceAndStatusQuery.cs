using MediatR;
using System;

namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQuery : IRequest<GetBalanceAndStatusQueryResponse>
    {
        public Guid AccountId { get; set; }
    }
}
