
//not move to global using
using Horas.Api.Hubs;
using Person = Horas.Domain.Person;
namespace Horas.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

            // Add services to the container.
            builder.Services.ConfigData(builder.Configuration);
            builder.Services.AddAutoMapper(op => op.AddMaps(typeof(Program).Assembly));
            builder.Services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssemblies(
             typeof(ReviewCreatedEventHandler).Assembly
         ));




            #region Auth

            builder.Services.AddScoped<ITokenService, TokenSevice>();

            builder.Services.AddIdentityCore<Person>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<HorasDBContext>()
                .AddSignInManager<SignInManager<Person>>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]!)),
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization();

            #endregion

            builder.Services.AddMemoryCache();


            #region Stripe Payment
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSetting"));
            StripeConfiguration.ApiKey = builder.Configuration["StripeSetting:SecretKey"];
            #endregion


            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy => policy
                         //.AllowAnyOrigin()
                         //.WithOrigins("") // حطي هنا عنوان Angular 4200
                         .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                           .AllowCredentials() // مهم جداً عشان SignalR
                );
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("AllowAngular");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<HorasDBContext>();
                var userManager = services.GetRequiredService<UserManager<Person>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                await StoreContextSeed.SeedAsync(context, userManager, roleManager);
            }

            app.MapHub<ChatHub>("/hub/chat");

            app.Run();
        }
    }
}
