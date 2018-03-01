using System.Collections.Generic;
using AutoMapper;
using IObjectMapper = NexPay.Utils.Abstract.IObjectMapper;

namespace NexPay.Utils.Concrete
{
    public class ObjectMapper: IObjectMapper
    {
        private readonly IMapper _mapper;
        public ObjectMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public TDestination Map<TSource, TDestination>(TSource source, IDictionary<string, object> options = null)
        {
            return _mapper.Map<TDestination>(source, opts =>
            {
                if (options != null)
                    foreach (var option in options)
                    {
                        opts.Items[option.Key] = option.Value;
                    }
            });
        }
    }
}
