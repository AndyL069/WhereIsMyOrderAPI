using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WhereIsMyOrderAPI.Repositories;
using Microsoft.Extensions.Configuration;
using OidcApiAuthorization;

[assembly: FunctionsStartup(typeof(WhereIsMyOrderAPI.Startup))]
namespace WhereIsMyOrderAPI
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            string SqlConnection = config.GetConnectionString("SqlConnectionString");
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(SqlConnection));

            // Repositories
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            // OIDC Authorization
            builder.Services.AddOidcApiAuthorization();
        }
    }
}
