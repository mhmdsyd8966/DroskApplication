using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Client.Helpers.ServicesConfigurationHelpers;

public static class ContextConfiguration
{
    public static IServiceCollection AddContextDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TheLayerContext>(options => options.UseSqlServer(configuration.GetConnectionString("default")));
        return services;
    }
}