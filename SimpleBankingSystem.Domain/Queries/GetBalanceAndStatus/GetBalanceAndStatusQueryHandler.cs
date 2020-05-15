using AutoMapper;
using MediatR;
using SimpleBankingSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQueryHandler : IRequestHandler<GetBalanceAndStatusQuery, GetBalanceAndStatusQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public GetBalanceAndStatusQueryHandler(IMapper mapper, IAccountRepository accountRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<GetBalanceAndStatusQueryResponse> Handle(GetBalanceAndStatusQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var account = await _accountRepository.GetById(query.AccountId);
            var queryResponse = _mapper.Map<GetBalanceAndStatusQueryResponse>(account);
            return queryResponse;
        }
    }
}
