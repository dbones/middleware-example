namespace Middleware.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GetNextFactory<TContext>
    {
        private readonly IEnumerator<PipeItem> _enumerator;
        private readonly IScopedResolver _scope;

        public GetNextFactory(IEnumerator<PipeItem> enumerator, IScopedResolver scope)
        {
            _enumerator = enumerator;
            _scope = scope;
        }


        public Next<TContext> GetNext()
        {
            if (!_enumerator.MoveNext())
            {
                return ctx => Task.CompletedTask;
            }

            var pipedType = _enumerator.Current;

            Next<TContext> result = ctx =>
            {
                IAction<TContext> middleware = null;

                if (pipedType.PipeItemType == PipeItemType.Type)
                {
                    middleware = (IAction<TContext>) _scope.Resolve(pipedType.Type);
                }
                else
                {
                    middleware = (IAction<TContext>) pipedType.GivenInstance;
                }

                return middleware.Execute(ctx, GetNext());

            };

            return result;

        }
    }
}