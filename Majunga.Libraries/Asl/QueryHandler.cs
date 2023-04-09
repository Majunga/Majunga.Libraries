using Majunga.Libraries.Infrastructure.IAsl;
using Majunga.Libraries.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Majunga.Auth.Asl.Queries
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        private readonly IConversionService _conversionService;

        public QueryHandler(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }
        
        public virtual async Task<TResult?> Query(TQuery query)
        {
            if(query == null)
                throw new ArgumentNullException(nameof(query));
            
            var rawResult = await RunQuery(query);

            if (rawResult == null) return default;

            return this.Convert<TResult>(rawResult);
        }

        protected abstract Task<object?> RunQuery(TQuery query);

        #region Conversion

        protected T? Convert<T>(object source)
            where T : class
        {
            return _conversionService.Convert<T>(source);
        }

        protected void Map(object source, object target)
        {
            this._conversionService.Map(source, target);
        }

        #endregion
    }
}
