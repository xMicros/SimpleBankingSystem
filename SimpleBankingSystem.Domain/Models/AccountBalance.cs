namespace SimpleBankingSystem.Domain.Models
{
    public class AccountBalance : IAccountBalance
    {
        private readonly object _padlock = new object();

        private AccountBalance()
        {
            Amount = 0;
        }

        public decimal Amount { get; private set; }

        public static IAccountBalance CreateWithZeroAmount()
        {
            return new AccountBalance();
        }

        public void AddMoney(decimal money)
        {
            lock (_padlock)
            {
                Amount += money;
            }
        }

        public void SubtractMoney(decimal money)
        {
            lock (_padlock)
            {
                Amount -= money;
            }
        }
    }
}
