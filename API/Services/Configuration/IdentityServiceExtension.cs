using System.Text;
using API.Data;
using API.Services.Security;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Configuration
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config){
            services.AddIdentityCore<User>(option =>
            {
                option.SignIn.RequireConfirmedEmail = false;
                option.Password.RequireNonAlphanumeric = false;
            }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MyDbContext>()
            .AddSignInManager<SignInManager<User>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            services.AddAuthorization(option =>
            {

                //Register IsPermitRequirement policy to service

                option.AddPolicy("IsPermitRequire", policy =>
                {
                    policy.Requirements.Add(new IsPermitRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsPermitRequirementHandler>();
            services.AddScoped<TokenService>();
            return services;
        }
        
    }
}