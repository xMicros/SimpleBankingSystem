namespace SimpleBankingSystem.Domain.Queries
{
    public interface IQuery<out TResponse> where TResponse : IQueryResponse
    {
    }
}
