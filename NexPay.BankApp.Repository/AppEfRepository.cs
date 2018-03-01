using System;
using System.Threading.Tasks;
using Data.EFRepository.Base;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.ViewModel;

namespace NexPay.BankApp.Repository
{
    public partial class AppEfRepository : EntityFrameworkRepository<AppDbContext>, IAppRepository
    {
        public AppEfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Guid> Add(PaymentDetails paymentDetails)
        {
            throw new NotImplementedException();
        }
    }
}
