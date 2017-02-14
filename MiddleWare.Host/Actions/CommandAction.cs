namespace MiddleWare.Host.Actions
{
    using System.Threading.Tasks;
    using Middleware.Core;

    public class CommandAction : IAction<Transfer>
    {
        private readonly TransferCommand _command;

        public CommandAction(TransferCommand command)
        {
            _command = command;
        }
        
        public async Task Execute(Transfer context, Next<Transfer> next)
        {
            await _command.Execute(context);
            await next(context);
        }
    }
}