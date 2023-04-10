using Majunga.Libraries.Infrastructure.Services;

namespace Majunga.Libraries.Asl
{
    public class HandlerBase
    {
        private IConversionService _conversionService;

        public HandlerBase(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        #region Conversion

        protected T? Convert<T>(object source)
            where T : class
        {
            return _conversionService.Convert<T>(source);
        }

        protected void Map(object source, object target)
        {
            _conversionService.Map(source, target);
        }

        #endregion
    }
}
