using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.EFRepository.Base;
using Data.Repository.Base.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NexPay.BankApp.AppService;
using NexPay.BankApp.Core.Abstract.AppService;
using NexPay.BankApp.Core.Abstract.Repository;
using NexPay.BankApp.Core.Configuration;
using NexPay.BankApp.Core.ViewModel;
using NexPay.BankApp.Repository;
using NexPay.BankApp.Web.Infrastructure.ActionFilters;
using NexPay.Utils.Abstract;
using NexPay.Utils.Concrete;

namespace NexPay.BankApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddAutoMapper();

            // Add application services.
            services.AddScoped<IJsonSerializer, NewtonsoftJsonSerializer>();
            services.AddScoped<Utils.Abstract.IObjectMapper, ObjectMapper>();
            services.AddScoped<IAppRepository, AppEfRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IModelValidator, ModelValidator>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<UnitOfWorkActionFilter>();

            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            Action<DbContextOptionsBuilder> dbCtxOptions = null;
            if (CurrentEnvironment.IsEnvironment("Testing"))
            {
                dbCtxOptions = (options) =>
                {
                    //There are two options to use in-memory database
                    //First option is to use build in one but it does not support transactions
                    options.UseInMemoryDatabase("TestingDB");
                    options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

                    //Second option is to use SQLite in-memory
                    //var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
                    //inMemorySqlite.Open();
                    //options.UseSqlite(inMemorySqlite);
                };
            }
            else
            {
                dbCtxOptions = (options) => options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            }

            services.AddDbContext<AppDbContext>(dbCtxOptions);
            services.AddScoped(provider => (DbContext)provider.GetService(typeof(AppDbContext)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseExceptionHandler(
                 options => {
                     options.Run(
                     async context =>
                     {
                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType = "application/json";
                         var ex = context.Features.Get<IExceptionHandlerFeature>();
                         if (ex != null)
                         {
                             var errMessage = new ErrorMessage();
                             if (env.IsDevelopment())
                             {
                                 errMessage.Message = $"Error Message: { ex.Error.Message}\r\nStackTrace: {ex.Error.StackTrace}";
                                 errMessage.ErrorCode = ex.Error.GetType().Name;
                             }
                             else
                             {
                                 errMessage.Message = "Internal server error occurred!";
                             }

                             await context.Response.WriteAsync(JsonConvert.SerializeObject(
                                 new Error { Errors = new[] { errMessage } },
                                 new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })).ConfigureAwait(false);
                         }
                     });
                 });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
