using MediatR;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Domain.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommandHandler : AsyncRequestHandler<WithdrawMoneyCommand>
    {
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountBalanceValidator _balanceValidator;
        private readonly IAccountRepository _accountRepository;

        public WithdrawMoneyCommandHandler(IAccountStatusValidator statusValidator, IAccountBalanceValidator balanceValidator, IAccountRepository accountRepository)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _balanceValidator = balanceValidator ?? throw new ArgumentNullException(nameof(balanceValidator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        protected async override Task Handle(WithdrawMoneyCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var account = await _accountRepository.GetById(command.AccountId);

            if (_statusValidator.IsUnverifiedOrClosed(account.Status) ||
                !_balanceValidator.CanWithdrawMoney(account.Balance, command.MoneyAmount))
            {
                throw new ForbiddenCommandException();
            }
            account.Balance.SubtractMoney(command.MoneyAmount);

            if (_statusValidator.IsFrozen(account.Status))
            {
                account.Status.ChangeStatus(AccountStatusValues.Verified);
            }
        }
    }
}
