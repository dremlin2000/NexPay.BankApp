using ExpectedObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NexPay.BankApp.Core.Abstract.AppService;
using NexPay.BankApp.Core.ViewModel;
using NexPay.BankApp.Web.Controllers;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexPay.BankApp.Test
{
    [TestFixture]
    public class PaymentControllerTest
    {
        [Test]
        public async Task GivenPaymentApiController_WhenValidPaymentSubmitted_ThenSubmitMethodCalled_And_OkResultReturned()
        {
            //Arrange
            var expectedReceiptNum = Guid.NewGuid();
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.Submit(It.IsAny<PaymentDetails>())).ReturnsAsync(expectedReceiptNum);
            var controller = new PaymentController(mockPaymentService.Object);

            //Act
            var result = await controller.Post(new PaymentDetails());

            //Assert
            result.ShouldBeOfType<OkObjectResult>();
            mockPaymentService.Verify(x => x.Submit(It.IsAny<PaymentDetails>()), Times.Once);
            ((OkObjectResult)result).Value.ShouldBe(expectedReceiptNum);
        }

        [Test]
        public async Task GivenPaymentApiController_WhenInValidPaymentSubmitted_ThenSubmitMethodCalled_And_BadRequestResultReturned()
        {
            //Arrange
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService
                .Setup(x => x.Submit(It.IsAny<PaymentDetails>()))
                .ThrowsAsync(new ValidationException(new List<ValidationFailure> { new ValidationFailure("propName", "error") }));
            var controller = new PaymentController(mockPaymentService.Object);
            //Act
            var result = await controller.Post(new PaymentDetails());

            //Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
            mockPaymentService.Verify(x => x.Submit(It.IsAny<PaymentDetails>()), Times.Once);
            var response = ((BadRequestObjectResult)result).Value as Error;
            (new ErrorMessage { PropertyName = "propName", Message = "error" }).ToExpectedObject()
                .ShouldEqual(response.Errors.SingleOrDefault());
        }
    }
}
