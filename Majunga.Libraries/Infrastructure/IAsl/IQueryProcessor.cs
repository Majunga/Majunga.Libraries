using System.Threading.Tasks;

namespace Majunga.Libraries.Infrastructure.IAsl
{
    public interface IQueryProcessor
    {
        Task<TResult?> Process<TResult>(IQuery<TResult> query)
            where TResult : class;
    }

}
