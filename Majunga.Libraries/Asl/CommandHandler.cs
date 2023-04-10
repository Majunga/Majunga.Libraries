using Majunga.Libraries.Infrastructure.IAsl;
using Majunga.Libraries.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Asl
{
    public abstract class CommandHandler<TCommand> : HandlerBase, ICommandHandler<TCommand>
    {
        public CommandHandler(IConversionService conversionService) : base(conversionService)
        {
        }
        
        public async virtual Task Handle(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            await RunCommand(command);
        }

        protected abstract Task RunCommand(TCommand command);
    }
}
