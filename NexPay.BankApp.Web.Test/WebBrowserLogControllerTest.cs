using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NexPay.BankApp.Core.Extensions;
using NexPay.BankApp.Core.ViewModel;
using NexPay.BankApp.Web.Controllers;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexPay.BankApp.Web.Test
{
    [TestFixture]
    public class WebBrowserLogControllerTest
    {
        [Test]
        public void GivenLogger_WhenLogRequestSent_ThenOkResultReturnedBack()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<WebBrowserLogController>>();
            var controller = new WebBrowserLogController(mockLogger.Object);

            //Act
            var result = controller.Post(new LogMessage() { Message = "message", Severity = Severity.Info });

            //Assert
            result.ShouldBeOfType<OkResult>();
        }
    }
}
