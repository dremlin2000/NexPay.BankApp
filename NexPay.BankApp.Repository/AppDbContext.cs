using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace NexPay.BankApp.Repository
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.SetInitializer<IspDbContext>(null);
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Apply all existing EntityTypeConfigurations in executing assembly
            var configTypes = 
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && 
                t.GetTypeInfo().ImplementedInterfaces.Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in configTypes)
            {
                dynamic config = Activator.CreateInstance(configType);
                modelBuilder.ApplyConfiguration(config);
            }
        }
    }
}
