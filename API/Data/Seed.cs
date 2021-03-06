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
                        Description = "V???i s??? m???nh m???, b???n b??? t??? thi???t k??? v??? ngo??i l???n b??n trong c???u h??nh, Asus TUF Gaming FX506LH (HN002T) s??? l?? m???t tr??? th??? ?????c l???c c???a b???n trong tr?? ch??i y??u th??ch v?? c??? nh???ng t??c v??? n???ng kh??c",
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
                        Description = "Laptop Asus VivoBook A512FL (EJ569T) l?? chi???c laptop nh??? g???n ph?? h???p v???i ?????i t?????ng h???c sinh, sinh vi??n hay nh??n vi??n v??n ph??ng. Chi???c m??y ???????c trang b??? c???u h??nh m???nh m??? ????? ????? b???n tho???i m??i s??? d???ng c??c ???ng d???ng v??n ph??ng hi???n nay v?? h??? tr??? t???t trong vi???c x??? l?? h??nh ???nh b???ng Photoshop, Ai",
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
                        Description = "Laptop Dell Vostro 5402 i5 (V4I5003W) l?? m???u m??y t??nh h?????ng ?????n ?????i t?????ng ng?????i d??ng l?? h???c sinh sinh vi??n ho???c nh??n vi??n v??n ph??ng v???i c???u h??nh m???nh m???, ????p ???ng t???t c??c nhu c???u th?????ng ng??y.",
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
                        Description = "Laptop Dell Inspiron 3505 R3 (Y1N1T1) v???i thi???t k??? trang nh??, hi???n ?????i, hi???u n??ng ????? d??ng c??ng m??n h??nh hi???n th??? s???c n??t mang ?????n cho h???c sinh, sinh vi??n v?? d??n v??n ph??ng m???t s??? l???a ch???n ph?? h???p trong ph??n kh??c gi?? r???.",
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