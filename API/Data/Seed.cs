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
                        Name = "Shoe"
                    },
                    new Category
                    {
                        Name = "Sport"
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
                        Image = "asus-uf-gaming-fx506lh-i5-hn002t-15-600x600.jpg",
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
                        }
                    },
                    new Product
                    {
                        Name = "Asus VivoBook A512FL i5",
                        Price = 18090000,
                        Description = "Laptop Asus VivoBook A512FL (EJ569T) là chiếc laptop nhỏ gọn phù hợp với đối tượng học sinh, sinh viên hay nhân viên văn phòng. Chiếc máy được trang bị cấu hình mạnh mẽ đủ để bạn thoải mái sử dụng các ứng dụng văn phòng hiện nay và hỗ trợ tốt trong việc xử lí hình ảnh bằng Photoshop, Ai",
                        Image = "asus-vivobook-a512fl-i5-10210u-8gb-512gb-2gb-mx250-9-217320-600x600.jpg",
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
                        }
                    },
                    new Product
                    {
                        Name = "Laptop Dell Vostro 5402",
                        Price = 18890000,
                        Description = "Laptop Dell Vostro 5402 i5 (V4I5003W) là mẫu máy tính hướng đến đối tượng người dùng là học sinh sinh viên hoặc nhân viên văn phòng với cấu hình mạnh mẽ, đáp ứng tốt các nhu cầu thường ngày.",
                        Image = "dell-vostro-5402-i5-v4i5003w-222320-102344-600x600.jpg",
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
                        }
                    },
                    new Product
                    {
                        Name = "Laptop Dell Inspiron 3505 R3",
                        Price = 11990000,
                        Description = "Laptop Dell Inspiron 3505 R3 (Y1N1T1) với thiết kế trang nhã, hiện đại, hiệu năng đủ dùng cùng màn hình hiển thị sắc nét mang đến cho học sinh, sinh viên và dân văn phòng một sự lựa chọn phù hợp trong phân khúc giá rẻ.",
                        Image = "dell-inspiron-3505-r3-y1n1t1-600x600.jpg",
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
                        }
                    },
                    new Product
                    {
                        Name = "GIÀY NIKE AIR ZOOM STRUCTURE 23 NAM - ĐEN TRẮNG",
                        Price = 3490000,
                        Description = "Giày Nike Air Zoom Structure 23 là mẫu giày được nâng cấp rất nhiều so với phiên bản trước đó: nhẹ hơn, khỏe khoắn hơn, mang lại trải nghiệm tuyệt vời cho người dùng",
                        Image = "giay-nike-air-zoom-structure-23-nam-den-trang-01-800x800_0.jpg",
                        Brand = brands[2],
                        CreatedDate = DateTime.Now,
                        ProductCategories = new List<CategoryProduct>
                        {
                            new CategoryProduct
                            {
                                Category = categories[3]
                            },
                            new CategoryProduct
                            {
                                Category = categories[4]
                            },
                        }
                    },
                    new Product
                    {
                        Name = "GIÀY NIKE AIR ZOOM STRUCTURE 23 NAM - ĐEN TRẮNG",
                        Price = 3490000,
                        Description = "Được thừa hưởng kiểu dáng thiết kế với đàn anh Boston 8 m trước- song ở phiên bản 9 m, các nhà sản xuất đã thêm vào công nghệ đế giữa LightStrike kết hợp với bộ đế Boost huyền thoại của Adidas, làm tăng gấp đôi khả năng hỗ trợ lực cho bàn chân so với phiên bản cũ",
                        Image = "giay-adidas-adizero-boston-9-m-nam-den-cam-01-800x800_0.jpg",
                        Brand = brands[3],
                        CreatedDate = DateTime.Now,
                        ProductCategories = new List<CategoryProduct>
                        {
                            new CategoryProduct
                            {
                                Category = categories[3]
                            },
                            new CategoryProduct
                            {
                                Category = categories[4]
                            },
                        }
                    }
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