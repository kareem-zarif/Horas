<<<<<<< HEAD
using System.Text;
using Horas.Data;
using Horas.Data.DataAccess;
using Horas.Data.Services;
using Horas.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Person = Horas.Domain.Person;
=======
using Horas.Application.Handlers;
using Horas.Data;
using Horas.Data.DataAccess;
using Horas.Data.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
>>>>>>> origin/menna2


namespace Horas.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
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

            app.Run();
        }
    }
}
