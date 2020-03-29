using System;

namespace SimpleBankingSystem.Domain.Models.Entities
{
    public class Entity : IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
