using System.Linq;
using NexPay.BankApp.Core.Validator;
using NexPay.BankApp.Core.ViewModel;
using NUnit.Framework;
using Shouldly;

namespace NexPay.BankApp.Test
{
    [TestFixture]
    public class PaymentDetailsValidatorTest
    {
        PaymentDetailsValidator validator;

        [SetUp]
        public void Init()
        {
            validator = new PaymentDetailsValidator();
        }

        [Test]
        public void GivenPaymentDetailsValidator_WhenValidViewModelSent_ThenValidResultReturned()
        {
            //Arrange
            var viewModel = new PaymentDetails
            {
                Bsb = 123456,
                AccountNumber = 12345678,
                AccountName = "MyAccountName",
                Amount = 500
            };

            //Act
            var result = validator.Validate(viewModel);

            //Assert
            result.IsValid.ShouldBeTrue();
        }

        [TestCase(default(uint), default(uint), default(string), 0)]
        [TestCase((uint)1234567, (uint)123456789, "_", -100)]
        public void GivenPaymentDetailsValidator_WhenInValidViewModelSent_ThenInValidResultReturned(uint bsb, uint accountNumber, string accountName, decimal amount)
        {
            //Arrange
            var viewModel = new PaymentDetails
            {
                Bsb = bsb,
                AccountNumber = accountNumber,
                AccountName = accountName,
                Amount = amount
            };

            //Act
            var result = validator.Validate(viewModel);

            //Assert
            result.IsValid.ShouldBeFalse();
            PropertyShouldBeInvalid(result, nameof(viewModel.Bsb));
            PropertyShouldBeInvalid(result, nameof(viewModel.AccountNumber));
            PropertyShouldBeInvalid(result, nameof(viewModel.AccountName));
            PropertyShouldBeInvalid(result, nameof(viewModel.Amount));
        }

        private void PropertyShouldBeInvalid(FluentValidation.Results.ValidationResult result, string propertyName)
        {
            result.Errors.Where(x => x.PropertyName.Equals(propertyName)).Count().ShouldBeGreaterThan(0);
        }
    }
}