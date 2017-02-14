namespace MiddleWare.Host.Infrastructure
{
    using System;
    using Autofac;
    using Middleware.Core;

    public class AutofacScopedResolver : IScopedResolver
    {
        private readonly ILifetimeScope _scope;

        public AutofacScopedResolver(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public object Resolve(Type type)
        {
            return _scope.Resolve(type);
        }
    }
}