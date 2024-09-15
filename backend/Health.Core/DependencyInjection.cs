using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Health.Core.Mapping;
using Health.Domain.Interfaces.Services;
using Health.Core.Services;

namespace Health.Core;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddSignalR();

        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();
    }
}

