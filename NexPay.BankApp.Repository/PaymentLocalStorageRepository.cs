using Microsoft.Extensions.Options;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.Configuration;
using NexPay.BankApp.Core.ViewModel;
using NexPay.Utils.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NexPay.BankApp.Repository
{
    public class PaymentLocalStorageRepository : IAppRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IJsonSerializer _serializer;

        public PaymentLocalStorageRepository(IOptions<AppSettings> appSettings, IJsonSerializer serializer)
        {
            _appSettings = appSettings.Value;
            _serializer = serializer;
        }

        public async Task<Guid> Add(PaymentDetails paymentDetails)
        {
            var id = Guid.NewGuid();
            using (StreamWriter outputFile = new StreamWriter(_appSettings.TransactionFilePath, true))
            {
                await outputFile.WriteLineAsync(_serializer.Serialize(new { Id = id, LogTime = DateTime.Now, Payment = paymentDetails }));
            }

            return id;
        }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void CreateList<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, int? skip = null, int? take = null, bool isDistinct = false) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDto> Get<TEntity, TDto>(Expression<Func<TEntity, TDto>> selectProperties, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, int? skip = null, int? take = null, bool isDistinct = false)
            where TEntity : class
            where TDto : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, int? skip = null, int? take = null, bool isDistinct = false) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> selectProperties, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, int? skip = null, int? take = null, bool isDistinct = false)
            where TEntity : class
            where TDto : class
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ResetDataFromDb<TEntity>(IEnumerable<TEntity> list, string entityPkName) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update<TEntity>(TEntity entity, Expression<Func<TEntity, object[]>> excludeProperties = null, Expression<Func<TEntity, object[]>> includeProperties = null) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
