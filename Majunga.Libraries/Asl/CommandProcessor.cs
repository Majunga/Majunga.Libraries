using Majunga.Libraries.Infrastructure.IAsl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Asl
{
    /// <summary>
    /// Processes Commands
    /// </summary>
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceProvider _services;

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="CommandProcessor"/>
        /// </summary>
        public CommandProcessor(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        #endregion

        [System.Diagnostics.DebuggerStepThrough]
        public async Task Execute(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var commandHandler = GetCommandHandler(command);

            await commandHandler.Handle((dynamic)command);
        }

        private dynamic GetCommandHandler(ICommand command)
        {
            var commandHandlerType = GetRequiredHandlerType(command);

            var commandHandler = GetCommandHandler(command, commandHandlerType);
            return commandHandler;
        }

        private Type GetRequiredHandlerType(ICommand command)
        {
            var commandHandlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());
            return commandHandlerType;
        }

        private dynamic GetCommandHandler(ICommand command, Type commandHandlerType)
        {
            dynamic commandHandler;
            try
            {
                commandHandler = _services.GetRequiredService(commandHandlerType);
            }
            catch (Exception ae)
            {
                var message = string.Format("A command handler has not been registered for the type: {0}", command.GetType());
                throw new Exception(message, ae);
            }

            return commandHandler;
        }
    }
}
