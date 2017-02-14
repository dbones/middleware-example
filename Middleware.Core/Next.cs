namespace Middleware.Core
{
    using System.Threading.Tasks;

    public delegate Task Next<in T>(T context);
}