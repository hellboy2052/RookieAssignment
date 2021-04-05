using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class Seed
    {
        public static async  Task SeedData(MyDbContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any() && !context.Products.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        UserName= "tri",
                        Email = "tri@test.com",
                        FullName = "Pham Quang Cao Tri"
                    },
                    new User
                    {
                        UserName= "bob",
                        Email = "bob@test.com",
                        FullName = "Bob"
                    },
                    new User
                    {
                        UserName= "henry",
                        Email = "henry@test.com",
                        FullName = "Henry"
                    },

                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                // var products = new List<Product>
                // {
                //     new Product{}
                // };

                await context.SaveChangesAsync();
            }
        }
    }
}