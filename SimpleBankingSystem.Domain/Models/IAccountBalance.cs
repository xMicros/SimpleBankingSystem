namespace SimpleBankingSystem.Domain.Models
{
    public interface IAccountBalance
    {
        decimal Amount { get; }

        void AddMoney(decimal money);
        void SubtractMoney(decimal money);
    }
}