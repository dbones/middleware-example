namespace MiddleWare.Host.Actions
{
    using System;
    using System.Threading.Tasks;
    using Middleware.Core;

    public class RandomWait : IAction<Transfer>
    {
        readonly Random _random = new Random();

        public async Task Execute(Transfer context, Next<Transfer> next)
        {
            var mseconds= _random.Next(3, 10) * 1000;
            System.Threading.Thread.Sleep(mseconds);
            await next(context);
        }
    }
}