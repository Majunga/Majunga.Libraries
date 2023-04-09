using System.Threading.Tasks;

namespace Majunga.Libraries.Infrastructure.IAsl
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command);
    }
}
