using AutoMapper;
using MediatR;
using SimpleBankingSystem.Domain.Models.Entities;
using System;

namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQueryHandler : RequestHandler<GetBalanceAndStatusQuery, GetBalanceAndStatusQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountEntity _account;

        public GetBalanceAndStatusQueryHandler(IMapper mapper, IAccountEntity account)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected override GetBalanceAndStatusQueryResponse Handle(GetBalanceAndStatusQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryResponse = _mapper.Map<GetBalanceAndStatusQueryResponse>(_account);
            return queryResponse;
        }
    }
}
