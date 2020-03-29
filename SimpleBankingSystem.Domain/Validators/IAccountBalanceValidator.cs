using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Validators
{
    public interface IAccountBalanceValidator
    {
        bool CanWithdrawMoney(IAccountBalance balance, decimal moneyAmount);
    }
}