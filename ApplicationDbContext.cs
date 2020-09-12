using Microsoft.EntityFrameworkCore;
using WhereIsMyOrderAPI.Models;

namespace WhereIsMyOrderAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
