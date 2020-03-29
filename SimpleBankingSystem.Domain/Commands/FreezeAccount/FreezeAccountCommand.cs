using System;

namespace SimpleBankingSystem.Domain.Commands.FreezeAccount
{
    public class FreezeAccountCommand : ICommand
    {
        public Guid AccountId { get; set; }
    }
}
