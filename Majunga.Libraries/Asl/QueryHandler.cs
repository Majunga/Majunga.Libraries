using Majunga.Libraries.Infrastructure.IAsl;
using Majunga.Libraries.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Asl
{
    public abstract class QueryHandler<TQuery, TResult> : HandlerBase, IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        public QueryHandler(IConversionService conversionService) : base(conversionService)
        {
        }

        public virtual async Task<TResult?> Query(TQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var rawResult = await RunQuery(query);

            if (rawResult == null) return default;

            return Convert<TResult>(rawResult);
        }

        protected abstract Task<object?> RunQuery(TQuery query);
    }
}
