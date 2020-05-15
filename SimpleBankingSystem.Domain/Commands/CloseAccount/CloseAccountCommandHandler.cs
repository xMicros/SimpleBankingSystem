using MediatR;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Domain.Commands.CloseAccount
{
    public class CloseAccountCommandHandler : AsyncRequestHandler<CloseAccountCommand>
    {
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountRepository _accountRepository;

        public CloseAccountCommandHandler(IAccountStatusValidator statusValidator, IAccountRepository accountRepository)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        protected async override Task Handle(CloseAccountCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var account = await _accountRepository.GetById(command.AccountId);

            if (_statusValidator.IsUnverifiedOrClosed(account.Status))
            {
                throw new ForbiddenCommandException();
            }
            account.Status.ChangeStatus(AccountStatusValues.Closed);
        }
    }
}
