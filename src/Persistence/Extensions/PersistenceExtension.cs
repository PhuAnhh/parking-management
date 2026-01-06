using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;
using Persistence.Repositories;

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
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        // Remote repository
        serviceCollection.AddScoped<IDeviceRepository, DeviceRepository>();
        serviceCollection.AddScoped<IParkingUnitOfWork, ParkingUnitOfWork>();
    }
}