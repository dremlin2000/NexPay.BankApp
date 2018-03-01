using Data.Repository.Base;
using System;

namespace NexPay.BankApp.Core.Model
{
    public class PaymentDetails: EntityBase<Guid>
    {
        public int Bsb { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
    }
}
