using AutoMapper;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus;

namespace SimpleBankingSystem.Domain.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<IAccountEntity, GetBalanceAndStatusQueryResponse>()
                .ForMember(response => response.Balance, options => options.MapFrom(account => account.Balance.Amount))
                .ForMember(response => response.Status, options => options.MapFrom(account => account.Status.Value.ToString()));
        }
    }
}
