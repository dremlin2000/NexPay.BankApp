using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NexPay.BankApp.Core.Abstract.AppService;
using NexPay.BankApp.Core.ViewModel;
using NexPay.BankApp.Web.Infrastructure.ActionFilters;
using System.Linq;
using System.Threading.Tasks;

namespace NexPay.BankApp.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(UnitOfWorkActionFilter))]

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]PaymentDetails paymentDetails)
        {
            _logger.LogInformation("Begin payment processing");
            return await ExecuteAsync(async () =>
            {
                var result = await _paymentService.Submit(paymentDetails);
                _logger.LogInformation($"Payment {result} processed successfully");
                return result;
            });
        }
    }
}
