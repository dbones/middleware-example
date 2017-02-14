namespace Middleware.Core
{
    using System;

    public interface IResolverBase : IDisposable
    {
        object Resolve(Type type);
    }
}