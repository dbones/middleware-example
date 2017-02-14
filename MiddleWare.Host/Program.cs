namespace MiddleWare.Host
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Actions;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using Infrastructure;
    using Interceptors;
    using Middleware.Core;

    internal class Program
    {
        public static void Main(string[] args)
        {
            //UseMiddleare();
            UseAop();
        }


        private static void UseAop()
        {
            //setup the container
            var builder = new ContainerBuilder();

            builder.RegisterType<TimerInterceptor>().InstancePerDependency();
            builder.RegisterType<LogInterceptor>().InstancePerLifetimeScope();

            //note the AOP is setup here.
            builder.RegisterType<TransferCommand>().InstancePerLifetimeScope()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(TimerInterceptor))
                .InterceptedBy(typeof(LogInterceptor));

            using (var container = builder.Build())
            {
                var cmd = container.Resolve<TransferCommand>();

                //app is now running.
                var task1 = cmd.Execute(new Transfer("A", "B", 100));
                Task.WaitAll(task1);
                Console.WriteLine("complete");
            }
        }

        private static void UseMiddleare()
        {
            //setup the container
            var builder = new ContainerBuilder();
            builder.RegisterType<TransferCommand>().InstancePerLifetimeScope();
            builder.RegisterType<CommandAction>().InstancePerLifetimeScope();
            builder.RegisterType<LogAction>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            {
                //setup the app, which is based on middleware
                //regsiter middleware actions with the 'Use' and 'Add' functions
                //the order will be the order actions are executed in
                var app = new Middleware<Transfer>(new AutofacResolver(container));

                //using a lambda function, because we can.
                app.Use(async (ctx, next) =>
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    await next(ctx);
                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.ElapsedTicks);
                });

                app.Add<LogAction>();
                app.Add<CommandAction>(); //this calls the command.

                //app is now running.
                var task1 = app.Execute(new Transfer("A", "B", 100));
                Task.WaitAll(task1);
                Console.WriteLine("complete");
            }
        }
    }
}