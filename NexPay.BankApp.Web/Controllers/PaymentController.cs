using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NexPay.BankApp.Core.Abstract.AppService;
using NexPay.BankApp.Core.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NexPay.BankApp.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]PaymentDetails paymentDetails)
        {
            return await ExecuteAsync(async () => 
            {
                var result = await _paymentService.Submit(paymentDetails);
                return result;
            });
        }
    }
}
