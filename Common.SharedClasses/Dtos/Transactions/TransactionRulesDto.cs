namespace Common.SharedClasses.Dtos.Transactions
{
    public class TransactionRulesDto
    {
        public int Id { get; set; }
        public string HandlerName { get; set; } = string.Empty;
        public int? MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

