using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;

namespace Persistence.Extensions;

public static class PersistenceExtension
{
    public static void AddPersistenceService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ParkingDbContext>((service, option) =>
        {
            var configuration = service.GetService<IConfiguration>();
            var connectionString = configuration["ConnectionStrings:Default"];
            option.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
        
        // // Remote repository
        // serviceCollection.AddScoped<IClientRepository, ClientRepository>();
    }
}