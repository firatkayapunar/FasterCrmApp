using FasterCrmApp.Entities;
using FasterCrmApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FasterCrmApp.DataAccess.Context.EntityFramework.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
