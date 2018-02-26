using FluentValidation;
using NexPay.BankApp.Core.ViewModel;
using System;

namespace NexPay.BankApp.Core.Validator
{
    public class PaymentDetailsValidator : AbstractValidator<PaymentDetails>
    {
        public PaymentDetailsValidator()
        {
            RuleFor(x => x.Bsb)
                .NotEmpty()
                .WithMessage(x => $"[{nameof(x.Bsb)}]: value is required")
                .Must(x => x >= 100000 && x <= 999999)
                .WithMessage(x => $"[{nameof(x.Bsb)}]: 6 digits only");
            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .WithMessage(x => $"[{nameof(x.AccountNumber)}]: value is required")
                .Must(x => x >= 10000000 && x <= 99999999)
                .WithMessage(x => $"[{nameof(x.AccountNumber)}]: 8 digits only");
            RuleFor(x => x.AccountName)
                .NotEmpty()
                .WithMessage(x => $"[{nameof(x.AccountName)}]: value is required")
                .Matches("^[a-zA-Z0-9]{1,32}$")
                .WithMessage(x => $"[{nameof(x.AccountName)}]: invalid format");
            RuleFor(x => x.Amount)
               .NotEmpty()
               .WithMessage(x => $"[{nameof(x.Amount)}]: value is required")
               .GreaterThan(0)
               .WithMessage(x => $"[{nameof(x.Amount)}]: value should be positive");
        }
    }
}
