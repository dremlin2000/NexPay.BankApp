using Moq;
using NexPay.BankApp.AppService;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.ViewModel;
using NUnit.Framework;
using System;
using Shouldly;
using System.Threading.Tasks;

namespace NexPay.BankApp.Test
{
    [TestFixture]
    public class PaymentServiceTest
    {
        [Test]
        public async Task GivenPaymentService_WhenValidBankPaymentSubmitted_ThenViewModelValidated_And_TransactionSaved_And_ReceiptNumberRetrievedBack()
        {
            //Arrange
            var expectedReceiptNum = Guid.NewGuid();
            var mockPaymentRepo = new Mock<IPaymentRepository>();
            mockPaymentRepo.Setup(x => x.Add(It.IsAny<PaymentDetails>())).ReturnsAsync(expectedReceiptNum);
            var paymentService = new PaymentService(mockPaymentRepo.Object);

            //Act
            var receiptNum = await paymentService.Submit(new PaymentDetails());

            //Assert
            receiptNum.ShouldBe(expectedReceiptNum);
            mockPaymentRepo.Verify(x => x.Add(It.IsAny<PaymentDetails>()), Times.Once);
        }
    }
}
