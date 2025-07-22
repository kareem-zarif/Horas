using Horas.Data.Repos;
using Horas.Domain.Interfaces;
using Horas.Domain.Interfaces.IRepos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Horas.Data.IOC
{
    public static class Extensions
    {
        public static IServiceCollection ConfigData(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("kareemConn");
            services.AddDbContext<HorasDBContext>(x => x.UseSqlServer(conn));

            //addscoped => Repos/UOW
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>)); ////resolve and inject all repo automatically when create dbcontext
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ISubCategoryRepo, SubCategoryRepo>();

            services.AddScoped<IUOW, UOW>();


            return services;
        }
    }
}
