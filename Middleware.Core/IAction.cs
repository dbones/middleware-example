namespace Middleware.Core
{
    using System.Threading.Tasks;

    public interface IAction<T>
    {
        Task Execute(T context, Next<T> next);
    }
}