using NexPay.BankApp.Core.Abstract.AppService;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.ViewModel;
using System;
using System.Threading.Tasks;

namespace NexPay.BankApp.AppService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> Submit(PaymentDetails paymentDetails)
        {
            return await _paymentRepository.Add(paymentDetails);
        }
    }
}
