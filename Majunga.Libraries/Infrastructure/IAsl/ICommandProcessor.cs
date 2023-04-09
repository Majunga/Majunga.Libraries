using System.Threading.Tasks;

namespace Majunga.Libraries.Infrastructure.IAsl
{
    public interface ICommandProcessor
    {
        Task Execute(ICommand command);
    }
}
