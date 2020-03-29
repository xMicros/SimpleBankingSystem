using SimpleBankingSystem.Domain.Models.Entities;
using System;

namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQueryHandler : IQueryHandler<GetBalanceAndStatusQuery, GetBalanceAndStatusQueryResponse>
    {
        private readonly IAccountEntity _account;

        public GetBalanceAndStatusQueryHandler(IAccountEntity account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        public GetBalanceAndStatusQueryResponse Execute(GetBalanceAndStatusQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return new GetBalanceAndStatusQueryResponse
            {
                Balance = _account.Balance.Amount,
                Status = _account.Status.Value.ToString()
            };
        }
    }
}
