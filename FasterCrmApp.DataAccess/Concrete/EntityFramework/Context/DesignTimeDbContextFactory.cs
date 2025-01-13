using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FasterCrmApp.DataAccess.Context.EntityFramework.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-CS2RH3I;Database=FasterCrmAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}

// Migration Çalışma Mantığı (Kısa Özet)

// Komut Başlatılır:
// dotnet ef migrations add gibi bir komut çalıştırılır.
// EF Core, tasarım zamanında (design-time) bir DbContext oluşturmak için çalışır.

// Tasarım Zamanında DbContext Oluşturulur:
// EF Core, önce Startup.cs veya Program.cs içindeki AddDbContext yapılandırmasını kullanarak DbContext oluşturmayı dener.
// Eğer bu mümkün değilse, IDesignTimeDbContextFactory adlı bir sınıf kullanılarak manuel olarak DbContext oluşturulur.

// Model Çıkarılır:
// DbContext içindeki DbSet tanımları ve yapılandırmalar incelenerek veritabanı şeması oluşturulur.

// Şema Karşılaştırılır:
// Mevcut veritabanı ile çıkarılan model arasındaki farklar belirlenir.

// Migration Dosyası Oluşturulur:
// Farklar SQL komutlarına dönüştürülerek migration dosyasında (Up ve Down metotları) kaydedilir.

// Not:
// Migration sırasında proje tam anlamıyla çalıştırılmaz, sadece DbContext'in bir örneği oluşturulur.
// DI çalışmazsa IDesignTimeDbContextFactory devreye girer.