using System;

namespace SimpleBankingSystem.Domain.Models.Entities
{
    public class Entity : IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
