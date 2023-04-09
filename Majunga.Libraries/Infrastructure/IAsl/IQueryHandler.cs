using System.Threading.Tasks;

namespace Majunga.Libraries.Infrastructure.IAsl
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        Task<TResult?> Query(TQuery query);
    }
}
