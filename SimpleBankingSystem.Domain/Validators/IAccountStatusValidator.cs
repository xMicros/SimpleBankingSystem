using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Validators
{
    public interface IAccountStatusValidator
    {
        bool IsClosed(IAccountStatus status);
        bool IsFrozen(IAccountStatus status);
        bool IsUnverifiedOrClosed(IAccountStatus status);
    }
}