using System;

namespace SimpleBankingSystem.Domain.Commands.DepositMoney
{
    public class DepositMoneyCommand : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal MoneyAmount { get; set; }
    }
}
