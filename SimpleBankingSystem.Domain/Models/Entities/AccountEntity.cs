﻿using System;

namespace SimpleBankingSystem.Domain.Models.Entities
{
    public class AccountEntity : Entity, IAccountEntity
    {
        public AccountEntity() : base()
        {
            Balance = AccountBalance.CreateWithZeroAmount();
            Status = AccountStatus.CreateWithUnverifiedValue();
        }

        public AccountEntity(Guid id) : base(id)
        {
            Balance = AccountBalance.CreateWithZeroAmount();
            Status = AccountStatus.CreateWithUnverifiedValue();
        }

        public IAccountBalance Balance { get; }
        public IAccountStatus Status { get; }
    }
}
