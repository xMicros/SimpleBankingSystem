using System;

namespace SimpleBankingSystem.Domain.Commands.CloseAccount
{
    public class CloseAccountCommand : ICommand
    {
        public Guid AccountId { get; set; }
    }
}
