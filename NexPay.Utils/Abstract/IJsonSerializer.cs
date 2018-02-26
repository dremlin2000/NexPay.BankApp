using System;

namespace NexPay.Utils.Abstract
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string jsonString);
        string Serialize<T>(T obj);
    }
}
