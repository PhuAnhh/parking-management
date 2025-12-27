using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtension
{
    public static void AddApplicationService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // serviceCollection.AddScoped<IEventServiceHelper, EventServiceHelper>();
        //     ...
    }
}