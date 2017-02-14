namespace MiddleWare.Host.Interceptors
{
    using System;
    using System.Diagnostics;
    using Castle.DynamicProxy;

    public class TimerInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            invocation.Proceed();

            invocation.After(() =>
            {
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedTicks);
            });
        }
    }
}