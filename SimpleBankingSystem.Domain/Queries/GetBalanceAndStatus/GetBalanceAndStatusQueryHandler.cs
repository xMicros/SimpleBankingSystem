using MediatR;
using SimpleBankingSystem.Domain.Models.Entities;
using System;

namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQueryHandler : RequestHandler<GetBalanceAndStatusQuery, GetBalanceAndStatusQueryResponse>
    {
        private readonly IAccountEntity _account;

        public GetBalanceAndStatusQueryHandler(IAccountEntity account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected override GetBalanceAndStatusQueryResponse Handle(GetBalanceAndStatusQuery query)
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
