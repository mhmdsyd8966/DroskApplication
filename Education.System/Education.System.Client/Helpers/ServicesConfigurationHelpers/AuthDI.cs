using System.Security.Policy;
using Education.System.Core.Identity;
using Education.System.Core.Identity.Base;
using Education.System.Presentation.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Client.Helpers.ServicesConfigurationHelpers;

public static class AuthDI
{
    public static IServiceCollection AddAuthDI(this IServiceCollection services)
    {
        ConfigureIdentity<Consumer>(services);
        ConfigureIdentity<Admin>(services);
        ConfigureIdentity<Student>(services);
        ConfigureIdentity<Teacher>(services);

        return services;
    }

    private static void ConfigureIdentity<TUser>(IServiceCollection services) where TUser : Consumer
    {
        if (typeof(TUser) == typeof(Consumer))
        {
            // Use AddIdentity for one of your user types (e.g., Consumer)
            services.AddIdentity<Consumer, IdentityRole>(options => { })
                .AddEntityFrameworkStores<TheLayerContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<Consumer>>();
        }
        else
        {
            // Use AddIdentityCore for the rest
            services.AddIdentityCore<TUser>(options => { })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TheLayerContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<TUser>>();
        }
    }

    public static IServiceCollection AddAuthConfig(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
        return services;
    }
}