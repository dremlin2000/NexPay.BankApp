using Moq;
using NexPay.BankApp.AppService;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.Validator;
using NexPay.BankApp.Core.ViewModel;
using NexPay.Utils.Abstract;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Shouldly;
using System.Threading.Tasks;
using AutoMapper;
using NexPay.BankApp.Core.Automapper.Profiles;
using NexPay.Utils.Concrete;
using System.Reflection;
using NexPay.BankApp.Repository;
using Microsoft.EntityFrameworkCore;

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
            var mockAppRepo = new Mock<IAppRepository>();
            Core.Model.PaymentDetails model = null;
            mockAppRepo.Setup(x => x.Create(It.IsAny<Core.Model.PaymentDetails>())).Callback((Core.Model.PaymentDetails x ) => { model = x; }).Verifiable();
            mockAppRepo.Setup(x => x.SaveAsync()).Callback(() => { model.Id = expectedReceiptNum; }).Returns(Task.FromResult<object>(null)).Verifiable();
            var mockModelValidator = new Mock<IModelValidator>();
            var mockPaymentValidator = new Mock<PaymentDetailsValidator>();
            var autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AppProfile()));
            var objectMapper = new ObjectMapper(autoMapperConfig.CreateMapper());
            var paymentService = new PaymentService(mockAppRepo.Object, mockModelValidator.Object, objectMapper);        

            //Act
            var receiptNum = await paymentService.Submit(new PaymentDetails());

            //Assert
            receiptNum.ShouldBe(expectedReceiptNum);
            mockModelValidator.Verify(x => x.ValidateAsync<PaymentDetails, PaymentDetailsValidator>(It.IsAny<PaymentDetails>()), Times.Once);
            mockAppRepo.Verify(x => x.SaveAsync(), Times.Once);
            mockAppRepo.Verify(x => x.Create(It.IsAny<Core.Model.PaymentDetails>()), Times.Once);
        }

        //The difference between using in-memory database and mocking up the repository is significant
        //Therefore, if in-memory database is used, it is better to implement integration test
        [Test]
        public async Task GivenPaymentService_And_InMemoryDatabase_WhenValidBankPaymentSubmitted_ThenViewModelValidated_And_TransactionSaved_And_ReceiptNumberRetrievedBack()
        {
            //Arrange
            var expectedReceiptNum = Guid.NewGuid();
            var mockModelValidator = new Mock<IModelValidator>();
            var mockPaymentValidator = new Mock<PaymentDetailsValidator>();
            var autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AppProfile()));
            var objectMapper = new ObjectMapper(autoMapperConfig.CreateMapper());
            Mock<AppEfRepository> mockAppRepo = null;
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;

            Guid receiptNum;
            // Run the test against one instance of the context
            using (var context = new AppDbContext(options))
            {
                mockAppRepo = new Mock<AppEfRepository>(context);
                mockAppRepo.CallBase = true;
                var paymentService = new PaymentService(mockAppRepo.Object, mockModelValidator.Object, objectMapper);

                //Act
                receiptNum = await paymentService.Submit(new PaymentDetails());
            }

            //Assert
            receiptNum.ShouldNotBe(default(Guid));
            mockModelValidator.Verify(x => x.ValidateAsync<PaymentDetails, PaymentDetailsValidator>(It.IsAny<PaymentDetails>()), Times.Once);
            mockAppRepo.Verify(x => x.SaveAsync(), Times.Once);
            mockAppRepo.Verify(x => x.Create(It.IsAny<Core.Model.PaymentDetails>()), Times.Once);
        }
    }
}