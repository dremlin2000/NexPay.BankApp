using NexPay.BankApp.Core.ViewModel;
using System;
using System.Threading.Tasks;

namespace NexPay.BankApp.Core.Abstract.AppService
{
    public interface IPaymentService
    {
        Task<Guid> Submit(PaymentDetails paymentDetails);
    }
}
