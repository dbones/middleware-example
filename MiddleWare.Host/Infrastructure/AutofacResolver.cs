namespace MiddleWare.Host.Infrastructure
{
    using System;
    using Autofac;
    using Middleware.Core;

    public class AutofacResolver : IResolver
    {
        private readonly IContainer _container;

        public AutofacResolver(IContainer container)
        {
            _container = container;
        }


        public void Dispose()
        {
            _container.Dispose();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }


        public IScopedResolver BeginScope()
        {
            return new AutofacScopedResolver(_container.BeginLifetimeScope());
        }
    }
}