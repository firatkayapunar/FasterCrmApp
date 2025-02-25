using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Concrete.EntityFramework.Base;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FasterCrmApp.DataAccess.ServiceCollectionExtension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            // DbContext Entegrasyonu
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer("Data Source=;Database=FasterCrmAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            });

            // Repository kayıtları
            services.AddScoped<IClientRepository, EfClientRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IIssueRepository, EfIssueRepository>();
            services.AddScoped<ILeadRepository, EfLeadRepository>();
            services.AddScoped<INotificationRepository, EfNotificationRepository>();
            services.AddScoped<ILogRepository, EfLogRepository>();

            return services;
        }
    }
}
