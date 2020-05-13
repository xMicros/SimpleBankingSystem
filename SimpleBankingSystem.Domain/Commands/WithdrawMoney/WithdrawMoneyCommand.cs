using MediatR;
using System;

namespace SimpleBankingSystem.Domain.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommand : IRequest
    {
        public Guid AccountId { get; set; }
        public decimal MoneyAmount { get; set; }
    }
}
