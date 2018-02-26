using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NexPay.BankApp.Core.Extensions;
using NexPay.BankApp.Core.ViewModel;

namespace NexPay.BankApp.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WebBrowserLogController : BaseController
    {
        private readonly ILogger<WebBrowserLogController> _logger;

        public WebBrowserLogController(ILogger<WebBrowserLogController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        public IActionResult Post([FromBody]LogMessage logMessage)
        {
            _logger.Log(logMessage.Message, logMessage.Severity);

            return Ok();
        }
    }
}