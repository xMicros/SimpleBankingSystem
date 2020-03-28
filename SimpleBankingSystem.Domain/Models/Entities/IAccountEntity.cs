namespace SimpleBankingSystem.Domain.Models.Entities
{
    public interface IAccountEntity : IEntity
    {
        IAccountBalance Balance { get; }
        IAccountStatus Status { get; }
        
        void DepositMoney(decimal money);
        void WithdrawMoney(decimal money);
        void CloseAccount();
        void FreezeAccount();
    }
}