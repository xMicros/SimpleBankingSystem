using SimpleBankingSystem.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IAccountEntity> GetById(Guid accountId);
    }
}
