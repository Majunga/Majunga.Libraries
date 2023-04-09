using Majunga.Libraries.Infrastructure.IAsl;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Asl
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public async virtual Task Handle(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            await RunCommand(command);
        }

        protected abstract Task RunCommand(TCommand command);
    }
}
