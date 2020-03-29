using System;

namespace SimpleBankingSystem.Domain.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommand : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal MoneyAmount { get; set; }
    }
}
