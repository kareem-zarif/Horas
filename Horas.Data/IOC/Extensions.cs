using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Horas.Data.IOC
{
    public static class Extensions
    {
        public static IServiceCollection ConfigData(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("ignoreConn");
            services.AddDbContext<HorasDBContext>(x => x.UseSqlServer(conn));

            //addscoped => Repos/UOW
            return services;
        }
    }
}
