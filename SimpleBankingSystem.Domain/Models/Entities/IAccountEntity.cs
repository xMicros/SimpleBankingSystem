namespace SimpleBankingSystem.Domain.Models.Entities
{
    public interface IAccountEntity : IEntity
    {
        IAccountBalance Balance { get; }
        IAccountStatus Status { get; }
    }
}