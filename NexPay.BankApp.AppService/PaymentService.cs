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
        private readonly IAppRepository _appRepository;
        private readonly IModelValidator _modelValidator;
        private readonly IObjectMapper _objectMapper;

        public PaymentService(IAppRepository appRepository, IModelValidator modelValidator, IObjectMapper objectMapper)
        {
            _appRepository = appRepository;
            _modelValidator = modelValidator;
            _objectMapper = objectMapper;
        }

        public async Task<Guid> Submit(PaymentDetails paymentDetails)
        {
            await _modelValidator.ValidateAsync<PaymentDetails, PaymentDetailsValidator>(paymentDetails);

            var model = _objectMapper.Map<PaymentDetails, Core.Model.PaymentDetails>(paymentDetails);

            _appRepository.Create(model);
            await _appRepository.SaveAsync();

            return model.Id;
        }
    }
}
