namespace Middleware.Core
{
    public interface IResolver : IResolverBase
    {
        IScopedResolver BeginScope();
    }
}