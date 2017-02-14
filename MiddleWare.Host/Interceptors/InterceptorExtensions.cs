namespace MiddleWare.Host.Interceptors
{
    using System;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;

    public static class InterceptorExtensions 
    {
        public static void After(this IInvocation invocation, Action action)
        {
            var task = invocation.ReturnValue as Task;

            if (task == null)
            {
                action();
            }
            else
            {
                task.ContinueWith(t =>
                {
                    action();
                });    
            }
        }
    
    }
}