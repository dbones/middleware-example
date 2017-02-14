namespace MiddleWare.Host.Actions
{
    using System;
    using System.Threading.Tasks;
    using Middleware.Core;

    public class LogAction : IAction<Transfer>
    {
        public async Task Execute(Transfer context, Next<Transfer> next)
        {
            Console.WriteLine($"before {context}");
            await next(context);
            Console.WriteLine($"after {context}");
        }
    }
}