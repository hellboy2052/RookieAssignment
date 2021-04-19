using API.core;
using API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using API.Services;
using API.Services.Products;
using API.Services.Security;

namespace API.Services.Configuration
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(
                config.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000");
                });
            });
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IUserAccessor, UserAccessor>();
            return services;

        }
    }
}