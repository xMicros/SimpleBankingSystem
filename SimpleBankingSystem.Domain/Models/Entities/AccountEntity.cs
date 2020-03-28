using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain.Models.Entities
{
    public class AccountEntity : Entity, IAccountEntity
    {
        public AccountEntity() : base()
        {
            Balance = AccountBalance.CreateWithZeroAmount();
            Status = AccountStatus.CreateWithUnverifiedValue();
        }

        public IAccountBalance Balance { get; private set; }
        public IAccountStatus Status { get; private set; }
        
        public void DepositMoney(decimal money)
        {
            if (Status.Value == AccountStatusValues.Closed)
            {
                return;
            }
            Balance.AddMoney(money);
            UnfreezeAccount();
        }

        public void WithdrawMoney(decimal money)
        {
            if (Status.Value == AccountStatusValues.Unverified ||
                Status.Value == AccountStatusValues.Closed)
            {
                return;
            }
            Balance.SubtractMoney(money);
            UnfreezeAccount();
        }

        public void CloseAccount()
        {
            if (Status.Value == AccountStatusValues.Unverified ||
                Status.Value == AccountStatusValues.Closed)
            {
                return;
            }
            Status.ChangeStatus(AccountStatusValues.Closed);
        }

        public void FreezeAccount()
        {
            if (Status.Value == AccountStatusValues.Unverified ||
                Status.Value == AccountStatusValues.Closed)
            {
                return;
            }
            Status.ChangeStatus(AccountStatusValues.Frozen);
        }

        private void UnfreezeAccount()
        {
            if (Status.Value != AccountStatusValues.Frozen)
            {
                return;
            }
            Status.ChangeStatus(AccountStatusValues.Verified);
        }
    }
}
