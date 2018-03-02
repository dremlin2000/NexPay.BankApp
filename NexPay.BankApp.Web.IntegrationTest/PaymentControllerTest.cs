using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NexPay.BankApp.Core.ViewModel;
using NexPay.BankApp.Repository;
using NUnit.Framework;
using Shouldly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NexPay.BankApp.Web.IntegrationTest
{
    [TestFixture]
    public class PaymentControllerTest
    {
        private TestServer _server;
        private HttpClient _client;
        private AppDbContext _dbCtx;
        //private Core.Model.PaymentDetails localModel;

        [SetUp]
        public void Init()
        {
            _server = new TestServer(new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>());
            _client = _server.CreateClient();
            _dbCtx = _server.Host.Services.GetService(typeof(AppDbContext)) as AppDbContext;
            //localModel = new Core.Model.PaymentDetails { Bsb = 12345678 };
            //_dbCtx.Set<Core.Model.PaymentDetails>().Add(localModel);
            //_dbCtx.SaveChanges();
            //_dbCtx.Database.OpenConnection();
            //_dbCtx.Database.EnsureCreated();
            //_dbCtx.Database.Migrate();
        }

        [Test]
        public async Task GivenIHavePaymentApi_WhenISubmitValidPayment_ThenThePaymentIsSavedIntoDatabaseAndReceiptNumberRetrieved()
        {
            //Arrange
            var viewModel = new PaymentDetails() { Bsb = 123456, AccountNumber = 12345678, AccountName = "AccountName", Amount = 100 };
            var httpContent = new StringContent(JsonConvert.SerializeObject(viewModel));
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            // Act
            var response = await _client.PostAsync("/api/Payment", httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Guid receiptNum;
            Guid.TryParse(JsonConvert.DeserializeObject<string>(responseString), out receiptNum).ShouldBeTrue();
            //var a = _dbCtx.Database.GetDbConnection();
            //a.State.ShouldBe(System.Data.ConnectionState.Open);
            //a.ShouldNotBeNull();
            (await _dbCtx.Set<Core.Model.PaymentDetails>().FindAsync(receiptNum)).ShouldNotBeNull();
        }
    }
}
