using NexPay.BankApp.Core.ViewModel;
using System;
using System.Threading.Tasks;

namespace NexPay.BankApp.Core.Abstract.Repository
{
    public interface IPaymentRepository
    {
        Task<Guid> Add(PaymentDetails paymentDetails);
    }
}