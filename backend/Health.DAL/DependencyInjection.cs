using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Health.DAL;

public static class DependencyInjection
{
    public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("ConnectionStrings").GetSection("PostreSQL").Value;
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }
}

