using MediatR;
using System;

namespace SimpleBankingSystem.Domain.Commands.FreezeAccount
{
    public class FreezeAccountCommand : IRequest
    {
        public Guid AccountId { get; set; }
    }
}
