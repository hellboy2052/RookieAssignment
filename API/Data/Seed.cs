using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(MyDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default user and product
            if (!userManager.Users.Any() && !context.Products.Any())
            {
                //Seed Role
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                    await roleManager.CreateAsync(new IdentityRole("superadmin"));
                    await roleManager.CreateAsync(new IdentityRole("member"));
                    await roleManager.CreateAsync(new IdentityRole("customer"));
                }
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
                    switch (user.UserName)
                    {
                        case "henry":
                            await userManager.AddToRoleAsync(user, "member");
                            break;
                        case "bob":
                            await userManager.AddToRoleAsync(user, "customer");
                            break;
                        case "tri":
                            await userManager.AddToRoleAsync(user, "superadmin");
                            break;
                        default:
                            break;
                    }

                }
                //Seed brand


                var brands = new List<Brand>
                {
                    new Brand
                    {
                        Name = "Asus"
                    },
                    new Brand
                    {
                        Name = "Dell"
                    },
                    new Brand
                    {
                        Name = "Nike"
                    },
                    new Brand
                    {
                        Name = "Adidas"
                    }
                };


                if (!context.Brands.Any()) await context.Brands.AddRangeAsync(brands);

                //Seed Category
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Laptop"
                    },
                    new Category
                    {
                        Name = "Office"
                    },
                    new Category
                    {
                        Name = "Gaming"
                    },
                    new Category
                    {
                        Name = "Graphic"
                    },
                };

                if (!context.Categories.Any()) await context.Categories.AddRangeAsync(categories);




                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Laptop Asus TUF Gaming FX506LH",
                        Price = 20000000,
                        Description = "Với sự mạnh mẽ, bền bỉ từ thiết kế vẻ ngoài lẫn bên trong cấu hình, Asus TUF Gaming FX506LH (HN002T) sẽ là một trợ thủ đắc lực của bạn trong trò chơi yêu thích và cả những tác vụ nặng khác",
                        Brand = brands[0],
                        CreatedDate = DateTime.Now,
                        ProductCategories = new List<CategoryProduct>
                        {
                            new CategoryProduct
                            {
                                Category = categories[0]
                            },
                            new CategoryProduct
                            {
                                Category = categories[2]
                            },
                        },
                    },
                    new Product
                    {
                        Name = "Asus VivoBook A512FL i5",
                        Price = 18090000,
                        Description = "Laptop Asus VivoBook A512FL (EJ569T) là chiếc laptop nhỏ gọn phù hợp với đối tượng học sinh, sinh viên hay nhân viên văn phòng. Chiếc máy được trang bị cấu hình mạnh mẽ đủ để bạn thoải mái sử dụng các ứng dụng văn phòng hiện nay và hỗ trợ tốt trong việc xử lí hình ảnh bằng Photoshop, Ai",
                        Brand = brands[0],
                        CreatedDate = DateTime.Now,
                        ProductCategories = new List<CategoryProduct>
                        {
                            new CategoryProduct
                            {
                                Category = categories[0]
                            },
                            new CategoryProduct
                            {
                                Category = categories[1]
                            },
                        },
                    },
                    new Product
                    {
                        Name = "Laptop Dell Vostro 5402",
                        Price = 18890000,
                        Description = "Laptop Dell Vostro 5402 i5 (V4I5003W) là mẫu máy tính hướng đến đối tượng người dùng là học sinh sinh viên hoặc nhân viên văn phòng với cấu hình mạnh mẽ, đáp ứng tốt các nhu cầu thường ngày.",
                        Brand = brands[1],
                        CreatedDate = DateTime.Now,
                        ProductCategories = new List<CategoryProduct>
                        {
                            new CategoryProduct
                            {
                                Category = categories[0]
                            },
                            new CategoryProduct
                            {
                                Category = categories[1]
                            },
                            new CategoryProduct
                            {
                                Category = categories[2]
                            },
                        },
                    },
                    new Product
                    {
                        Name = "Laptop Dell Inspiron 3505 R3",
                        Price = 11990000,
                        Description = "Laptop Dell Inspiron 3505 R3 (Y1N1T1) với thiết kế trang nhã, hiện đại, hiệu năng đủ dùng cùng màn hình hiển thị sắc nét mang đến cho học sinh, sinh viên và dân văn phòng một sự lựa chọn phù hợp trong phân khúc giá rẻ.",
                        Brand = brands[1],
                        CreatedDate = DateTime.Now,
                        ProductCategories = new List<CategoryProduct>
                        {
                            new CategoryProduct
                            {
                                Category = categories[0]
                            },
                            new CategoryProduct
                            {
                                Category = categories[1]
                            },
                        },
                    },
                };



                if (!context.Brands.Any() && !context.Categories.Any())
                {
                    await context.Products.AddRangeAsync(products);
                }





                await context.SaveChangesAsync();
            }
        }
    }
}