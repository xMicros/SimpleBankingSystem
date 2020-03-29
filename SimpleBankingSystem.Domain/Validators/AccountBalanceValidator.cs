using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Validators
{
    public class AccountBalanceValidator : IAccountBalanceValidator
    {
        private const decimal MinimumBalanceAmount = 0;

        public bool CanWithdrawMoney(IAccountBalance balance, decimal moneyAmount)
        {
            return IsCurrentBalanceAmountAboveMinimum(balance.Amount)
                && HasNecessaryBalanceAmountToWithdrawMoney(balance.Amount, moneyAmount);
        }

        private static bool IsCurrentBalanceAmountAboveMinimum(decimal balanceAmount)
        {
            return balanceAmount > MinimumBalanceAmount;
        }

        private static bool HasNecessaryBalanceAmountToWithdrawMoney(decimal balanceAmount, decimal moneyAmount)
        {
            return (balanceAmount - moneyAmount) >= MinimumBalanceAmount;
        }
    }
}
