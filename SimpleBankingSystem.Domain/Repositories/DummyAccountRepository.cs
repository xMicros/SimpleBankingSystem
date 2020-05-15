using System;
using System.Threading.Tasks;
using SimpleBankingSystem.Domain.Models.Entities;

namespace SimpleBankingSystem.Domain.Repositories
{
    public class DummyAccountRepository : IAccountRepository
    {
        private readonly IAccountEntity _dummyAccountEntity;

        public DummyAccountRepository(IAccountEntity dummyAccountEntity)
        {
            _dummyAccountEntity = dummyAccountEntity ?? throw new ArgumentNullException(nameof(dummyAccountEntity));
        }

        public async Task<IAccountEntity> GetById(Guid accountId)
        {
            // logic to retrieve account by ID from database
            return _dummyAccountEntity;
        }
    }
}
