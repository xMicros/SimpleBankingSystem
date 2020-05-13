using MediatR;
using System;

namespace SimpleBankingSystem.Domain.Commands.CloseAccount
{
    public class CloseAccountCommand : IRequest
    {
        public Guid AccountId { get; set; }
    }
}
