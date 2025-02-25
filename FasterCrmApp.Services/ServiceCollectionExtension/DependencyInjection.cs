using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FasterCrmApp.Services.ServiceCollectionExtension
{
    //Bu kod, Services katmanında kullanılan servislerin Dependency Injection(DI) container'a kaydedilmesini sağlayan bir yardımcı sınıftır. Yani, bu sınıf üzerinden MediatR, AutoMapper ve diğer Services katmanı ile ilgili bağımlılıkların DI sistemine kaydedilmesini sağlıyoruz. Ancak bu sınıf, Program.cs veya Startup.cs dosyasından çağrılmadığı sürece herhangi bir etkisi olmaz. ( builder.Services.AddApplicationServices(); )
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            /*
             
            IServiceCollection bir interface'tir ve ASP.NET Core'un Dependency Injection (DI) mekanizmasında kullanılan temel yapı taşlarından biridir. Bu interface, uygulamadaki servislerin (bağımlılıkların) tanımlanmasını ve DI container'a eklenmesini sağlar. Ancak, ASP.NET Core çalışma zamanında bu interface'in bir somut implementasyonu (concrete implementation) olan ServiceCollection sınıfı oluşturulur ve kullanılır. Bu somut sınıf, servis tanımlarını bir liste olarak tutar ve DI container'ın nasıl oluşturulacağını belirler.

           *************************************************************

           IServiceCollection Interface'i:
            ASP.NET Core uygulamalarında, servisleri DI container'a eklemek için kullanılan temel bir interface'tir demiştik.
            Kendisi bir koleksiyon olarak çalışır ve servis tanımlarını temsil eden ServiceDescriptor nesnelerini tutar.
        
            public interface IServiceCollection : IList<ServiceDescriptor>
            {
            // Servis tanımları için kullanılan bir koleksiyon
            }

            ServiceCollection Sınıfı:
            IServiceCollection interface'ini implement eden somut bir sınıftır.
            Servis tanımlarını bir liste (List<ServiceDescriptor>) olarak saklar.
            Çalışma zamanında DI container'ı oluşturmak için bu listeyi kullanır.
            
            public class ServiceCollection : List<ServiceDescriptor>, IServiceCollection
            {
            // IServiceCollection'ın somut implementasyonu
            }

           Çalışma zamanında ASP.NET Core, ServiceCollection sınıfını kullanarak bir DI container (ServiceProvider) oluşturur ve bu container uygulama boyunca servislerin çözülmesini sağlar.

            *************************************************************
            AddApplicationServices Metodu:
            AddApplicationServices metodu, IServiceCollection interface'ini genişleten bir extension metottur. Bu metodun amacı, Application katmanına ait bağımlılıkların (örneğin, MediatR ve AutoMapper gibi) DI container'a eklenmesini sağlamaktır.

            Metodun Çalışma Mantığı
            AddApplicationServices, kendisine geçirilen IServiceCollection üzerinde çalışır.
            Çalışma zamanında IServiceCollection, somut bir sınıf olan ServiceCollection örneği tarafından implement edilir.
            Bu nedenle, AddApplicationServices çağrıldığında aslında ServiceCollection sınıfının bir örneği üzerinde çalışır.

             */

            // AutoMapper Entegrasyonu
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Services kayıtları
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
