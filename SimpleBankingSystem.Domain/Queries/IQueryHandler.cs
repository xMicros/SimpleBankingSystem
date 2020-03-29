namespace SimpleBankingSystem.Domain.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<IQueryResponse>
        where TResult : IQueryResponse
    {
        TResult Execute(TQuery query);
    }
}
