using System.Text;
using EmployeeIdentity.Persistence.Context;
using EmployeeIdentity.Persistence.IdentityModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeIdentity.Persistence;

public static class PersistenceServiceRegisteration
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration config)
    {
        var secretKey = config.GetSection("JwtSettings").GetValue<string>("Key");

        services.AddDbContext<ApplicationDbContext>(option => {
            option.UseNpgsql(config.GetConnectionString("Default"), b => b.MigrationsAssembly("EmployeeIdentity.Persistence"));
        });

        services
            .AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            // .AddBearerToken(IdentityConstants.BearerScheme);
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {

                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey ?? string.Empty)),
                    ValidateLifetime =  true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };

            });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        return services;
    }
}