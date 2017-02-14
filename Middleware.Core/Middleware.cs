namespace Middleware.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Middleware<TContext>
    {
        private readonly IResolver _resolver;
        private readonly List<PipeItem> _pipedTypes = new List<PipeItem>();


        public Middleware(IResolver resolver)
        {
            _resolver = resolver;
        }



        public void Add<T>() where T : IAction<TContext>
        {
            _pipedTypes.Add(new PipeItem(typeof(T)));
        }

        public void Use(Func<TContext, Next<TContext>, Task> action)
        {
            _pipedTypes.Add(new PipeItem(new ActionWrapper<TContext>(action)));
        }


        public async Task Execute(TContext context)
        {
            var enumerator = _pipedTypes.GetEnumerator();

            using (var scope = _resolver.BeginScope())
            {
                var factory = new GetNextFactory<TContext>(enumerator, scope);
                await factory.GetNext()(context);
            }

        }
    }

}