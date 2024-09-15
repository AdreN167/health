using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Health.DAL;

public static class DependencyInjection
{
    public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostreSQL");
        var redisConnectionString = configuration.GetConnectionString("Redis");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);
    }
}

