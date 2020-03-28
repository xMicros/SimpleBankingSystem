using System;

namespace SimpleBankingSystem.Domain.Models.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
