using FasterCrmApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FasterCrmApp.DataAccess.Context.EntityFramework.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
    }
}
