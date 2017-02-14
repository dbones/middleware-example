namespace MiddleWare.Host.Interceptors
{
    using System;
    using Castle.DynamicProxy;

    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"before {invocation.Arguments[0]}");
            invocation.Proceed();
            invocation.After(() =>
                Console.WriteLine($"after {invocation.Arguments[0]}"));
        }
    }
}