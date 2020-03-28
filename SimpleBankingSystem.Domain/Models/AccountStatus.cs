using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain.Models
{
    public class AccountStatus : IAccountStatus
    {
        private AccountStatus()
        {
            Value = AccountStatusValues.Unverified;
        }

        public AccountStatusValues Value { get; private set; }

        public static IAccountStatus CreateWithUnverifiedValue()
        {
            return new AccountStatus();
        }

        public void ChangeStatus(AccountStatusValues newStatus)
        {
            Value = newStatus;
        }
    }
}
