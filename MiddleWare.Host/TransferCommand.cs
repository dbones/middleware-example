namespace MiddleWare.Host
{
    using System;
    using System.Threading.Tasks;

    public class TransferCommand
    {
        public virtual Task Execute(Transfer transfer)
        {
            //do some logic here.
            Console.WriteLine($"Transfering {transfer.Amount}, "+
                              $"from: {transfer.SourceAccount} "+
                              $"to {transfer.DestinationAccount}");

            return Task.CompletedTask;
        }
    }
}