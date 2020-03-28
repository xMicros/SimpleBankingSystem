using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain.Models
{
    public interface IAccountStatus
    {
        AccountStatusValues Value { get; }

        void ChangeStatus(AccountStatusValues newStatus);
    }
}