using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.API.Configurations;

public static class DbConfiguration
{
    public static void ConfigureDbContext(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var connection = configuration.GetConnectionString("ConnectionString");
        services.AddDbContext<AppDbContext>((opt) => opt.UseSqlServer(connection), ServiceLifetime.Scoped);

    }
}