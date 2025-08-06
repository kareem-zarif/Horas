namespace Horas.Data.IOC
{
    public static class Extensions
    {
        public static IServiceCollection ConfigData(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("kareemConn");
            services.AddDbContext<HorasDBContext>(x =>
                x.UseSqlServer(conn, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()
                )
            );

            //addscoped => Repos/UOW
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>)); ////resolve and inject all repo automatically when create dbcontext
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ISubCategoryRepo, SubCategoryRepo>();
            services.AddScoped<ICustomerRepo,CustomerRepo>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<ICartItemRepo, CartItemRepo>();
            services.AddScoped<ISupplierRepo, SupplierRepo>();
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IProductSupplierRepo, ProductSupplierRepo>();
            services.AddScoped<IPersonNotificationRepo,PersonNotificationRepo>();

            services.AddScoped<IUOW, UOW>();

            return services;
        }
    }
}
