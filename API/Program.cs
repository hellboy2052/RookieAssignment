using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var service = scope.ServiceProvider;
            try
            {
                var context = service.GetRequiredService<MyDbContext>();

                var UserManager = service.GetRequiredService<UserManager<User>>();

                var RoleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.MigrateAsync();
                await Seed.SeedData(context, UserManager, RoleManager);
                
            }
            catch (Exception e)
            {
                var logger = service.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, "An error occured during migration");
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
