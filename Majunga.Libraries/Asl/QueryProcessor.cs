using Majunga.Libraries.Infrastructure.IAsl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Asl
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _services;

        public QueryProcessor(IServiceProvider services)
        {
            _services = services;
        }

        public async Task<TResult?> Process<TResult>(IQuery<TResult> query)
            where TResult : class
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var handler = GetQueryHandler(query);
            return await handler.Query((dynamic)query);
        }

        protected virtual dynamic GetQueryHandler<TResult>(IQuery<TResult> query)
        {
            var handlerType = GetRequiredHandlerType(query);

            dynamic handler;
            try
            {
                handler = _services.GetRequiredService(handlerType);
            }
            catch (Exception ae)
            {
                var message = $"A Query Handler could not be found for the query: {query.GetType()}";
                throw new Exception(message, ae);
            }

            return handler;
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected Type GetRequiredHandlerType<TResult>(IQuery<TResult> query)
        {
            var genericQueryHandler = typeof(IQueryHandler<,>);
            var handlerType = genericQueryHandler.MakeGenericType(query.GetType(), typeof(TResult));
            return handlerType;
        }
    }

}
