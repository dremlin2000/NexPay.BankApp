using Data.EFRepository.Base.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexPay.BankApp.Core.Model;
using System;

namespace NexPay.BankApp.Repository.EntityTypeConfiguration
{
    public class PaymentDetailsEntityConfig: EntityBaseConfiguration<PaymentDetails, Guid>
    {
        public override void Configure(EntityTypeBuilder<PaymentDetails> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(PaymentDetails));
        }
    }
}
