using System;

namespace SimpleBankingSystem.Domain.Exceptions
{
    public class ForbiddenCommandException : Exception
    {
        private const string ForbiddenMessageText = "Command is forbidden!";

        public override string Message => ForbiddenMessageText;
    }
}
