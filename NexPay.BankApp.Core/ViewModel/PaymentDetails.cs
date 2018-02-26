using System;

namespace NexPay.BankApp.Core.ViewModel
{
    public class PaymentDetails
    {
        public uint Bsb { get; set; }
        public uint AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
    }
}
