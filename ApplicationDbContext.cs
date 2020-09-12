using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WhereIsMyOrderAPI.Models;

namespace WhereIsMyOrderAPI
{
    public class ApplicationDbContext : DbContext
    {
        private IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void  OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(config.GetConnectionString("SqlConnectionString"));

        public DbSet<Order> Orders { get; set; }
    }
}
