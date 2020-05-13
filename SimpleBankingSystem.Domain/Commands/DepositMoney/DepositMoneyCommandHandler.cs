using MediatR;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;

namespace SimpleBankingSystem.Domain.Commands.DepositMoney
{
    public class DepositMoneyCommandHandler : RequestHandler<DepositMoneyCommand>
    {
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountEntity _account;

        public DepositMoneyCommandHandler(IAccountStatusValidator statusValidator, IAccountEntity account)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected override void Handle(DepositMoneyCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            if (_statusValidator.IsClosed(_account.Status))
            {
                throw new ForbiddenCommandException();
            }
            _account.Balance.AddMoney(command.MoneyAmount);

            if (_statusValidator.IsFrozen(_account.Status))
            {
                _account.Status.ChangeStatus(AccountStatusValues.Verified);
            }
        }
    }
}
