using MediatR;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;

namespace SimpleBankingSystem.Domain.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommandHandler : RequestHandler<WithdrawMoneyCommand>
    {
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountBalanceValidator _balanceValidator;
        private readonly IAccountEntity _account;

        public WithdrawMoneyCommandHandler(IAccountStatusValidator statusValidator, IAccountBalanceValidator balanceValidator, IAccountEntity account)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _balanceValidator = balanceValidator ?? throw new ArgumentNullException(nameof(balanceValidator));
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected override void Handle(WithdrawMoneyCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            if (_statusValidator.IsUnverifiedOrClosed(_account.Status) ||
                !_balanceValidator.CanWithdrawMoney(_account.Balance, command.MoneyAmount))
            {
                throw new ForbiddenCommandException();
            }
            _account.Balance.SubtractMoney(command.MoneyAmount);

            if (_statusValidator.IsFrozen(_account.Status))
            {
                _account.Status.ChangeStatus(AccountStatusValues.Verified);
            }
        }
    }
}
