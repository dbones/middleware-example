namespace MiddleWare.Host
{
    public class Transfer
    {
        public Transfer(string sourceAccount, string destinationAccount, decimal amount)
        {
            SourceAccount = sourceAccount;
            DestinationAccount = destinationAccount;
            Amount = amount;
        }


        public string SourceAccount { get; private set; }
        public string DestinationAccount { get; private set; }
        public decimal Amount { get; private set; }
    }
}