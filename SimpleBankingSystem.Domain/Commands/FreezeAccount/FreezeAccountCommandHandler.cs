using MediatR;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Domain.Commands.FreezeAccount
{
    public class FreezeAccountCommandHandler : AsyncRequestHandler<FreezeAccountCommand>
    {
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountRepository _accountRepository;

        public FreezeAccountCommandHandler(IAccountStatusValidator statusValidator, IAccountRepository accountRepository)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        protected async override Task Handle(FreezeAccountCommand command, CancellationToken cancellationToken)
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
            account.Status.ChangeStatus(AccountStatusValues.Frozen);
        }
    }
}
