using System.Collections.Generic;

namespace NexPay.Utils.Abstract
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource source, IDictionary<string, object> options = null);
    }
}
