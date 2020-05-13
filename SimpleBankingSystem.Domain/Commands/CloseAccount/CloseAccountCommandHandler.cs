using MediatR;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;

namespace SimpleBankingSystem.Domain.Commands.CloseAccount
{
    public class CloseAccountCommandHandler : RequestHandler<CloseAccountCommand>
    {
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountEntity _account;

        public CloseAccountCommandHandler(IAccountStatusValidator statusValidator, IAccountEntity account)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected override void Handle(CloseAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            if (_statusValidator.IsUnverifiedOrClosed(_account.Status))
            {
                throw new ForbiddenCommandException();
            }
            _account.Status.ChangeStatus(AccountStatusValues.Closed);
        }
    }
}
