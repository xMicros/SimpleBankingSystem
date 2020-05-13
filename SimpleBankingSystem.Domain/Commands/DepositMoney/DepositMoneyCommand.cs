using MediatR;
using System;

namespace SimpleBankingSystem.Domain.Commands.DepositMoney
{
    public class DepositMoneyCommand : IRequest
    {
        public Guid AccountId { get; set; }
        public decimal MoneyAmount { get; set; }
    }
}
