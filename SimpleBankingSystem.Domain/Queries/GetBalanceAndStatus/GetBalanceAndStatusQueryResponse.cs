namespace SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus
{
    public class GetBalanceAndStatusQueryResponse : IQueryResponse
    {
        public decimal Balance { get; set; }
        public string Status { get; set; }
    }
}
