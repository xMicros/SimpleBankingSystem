using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Validators
{
    public class AccountStatusValidator : IAccountStatusValidator
    {
        public bool IsClosed(IAccountStatus status)
        {
            return status.Value == AccountStatusValues.Closed;
        }

        public bool IsFrozen(IAccountStatus status)
        {
            return status.Value == AccountStatusValues.Frozen;
        }

        public bool IsUnverifiedOrClosed(IAccountStatus status)
        {
            return status.Value == AccountStatusValues.Unverified || IsClosed(status);
        }
    }
}
