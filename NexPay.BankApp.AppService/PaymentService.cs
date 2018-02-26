using NexPay.BankApp.Core.Abstract.AppService;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.Validator;
using NexPay.BankApp.Core.ViewModel;
using NexPay.Utils.Abstract;
using System;
using System.Threading.Tasks;

namespace NexPay.BankApp.AppService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IModelValidator _modelValidator;

        public PaymentService(IPaymentRepository paymentRepository, IModelValidator modelValidator)
        {
            _paymentRepository = paymentRepository;
            _modelValidator = modelValidator;
        }

        public async Task<Guid> Submit(PaymentDetails paymentDetails)
        {
            await _modelValidator.ValidateAsync<PaymentDetails, PaymentDetailsValidator>(paymentDetails);

            return await _paymentRepository.Add(paymentDetails);
        }
    }
}
