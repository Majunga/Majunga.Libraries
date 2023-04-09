using AutoMapper;
using System;

namespace Majunga.Libraries.Infrastructure.Services
{
    public interface IConversionService
    {
        T? Convert<T>(object source) where T : class;

        void Map(object source, object target);
    }

    public class ConversionService : IConversionService
    {
        private readonly IMapper _mapper;

        public ConversionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T? Convert<T>(object source) where T : class
        {
            return (T?)Convert(source, typeof(T));
        }

        private object? Convert(object? source, Type targetType)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            if (source == null) return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            try
            {
                return _mapper.Map(source, source.GetType(), targetType);
            }
            catch (AutoMapperConfigurationException amce)
            {
                throw new Exception(amce.Message, amce);
            }
            catch (AutoMapperMappingException amme)
            {
                if (amme.MemberMap == null)
                {
                    throw new Exception(amme.Message, amme);
                }
                if (amme.InnerException != null)
                {
                    // this is not ideal as we lose the call stack in the exception, but we need to preserve the original 
                    // exception to prevent AutoMapper from leaking into the application.  We could create a new instance 
                    // of the original exception, and throw that, with the AutoMapper as the inner exception, but it's
                    // difficult to preserve the data when constructing a new instance (e.g. as discovered with 
                    // DataValidationException)
                    throw amme.InnerException;
                }
                throw;
            }
        }

        public void Map(object source, object target)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            try
            {
                _mapper.Map(source, target, source.GetType(), target.GetType());
            }
            catch (AutoMapperMappingException amme)
            {
                if (amme.MemberMap == null)
                {
                    throw new Exception(amme.Message, amme);
                }
                if (amme.InnerException != null)
                {
                    // this is not ideal as we lose the call stack in the exception, but we need to preserve the original 
                    // exception to prevent AutoMapper from leaking into the application.  We could create a new instance 
                    // of the original exception, and throw that, with the AutoMapper as the inner exception, but it's
                    // difficult to preserve the data when constructing a new instance (e.g. as discovered with 
                    // DataValidationException)
                    throw amme.InnerException;
                }
                throw;
            }
        }
    }
}
