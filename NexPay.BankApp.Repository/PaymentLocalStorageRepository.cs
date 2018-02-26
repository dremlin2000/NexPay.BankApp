using Microsoft.Extensions.Options;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.Configuration;
using NexPay.BankApp.Core.ViewModel;
using NexPay.Utils.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NexPay.BankApp.Repository
{
    public class PaymentLocalStorageRepository : IPaymentRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IJsonSerializer _serializer;

        public PaymentLocalStorageRepository(IOptions<AppSettings> appSettings, IJsonSerializer serializer)
        {
            _appSettings = appSettings.Value;
            _serializer = serializer;
        }

        public async Task<Guid> Add(PaymentDetails paymentDetails)
        {
            var id = Guid.NewGuid();
            using (StreamWriter outputFile = new StreamWriter(_appSettings.TransactionFilePath, true))
            {
                await outputFile.WriteLineAsync(_serializer.Serialize(new { Id = id, LogTime = DateTime.Now, Payment = paymentDetails }));
            }

            return id;
        }
    }
}
