using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> ProductCategories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryProduct>(x => x.HasKey(a => new { a.ProductId, a.CategoryId }));

            builder.Entity<CategoryProduct>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.Entity<CategoryProduct>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            //Many to Many
            builder.Entity<Rating>(b =>
            {
                b.HasKey(k => new { k.productId, k.userId });
                b.HasOne(r => r.product)
                    .WithMany(p => p.rate)
                    .HasForeignKey(o => o.productId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(r => r.user)
                    .WithMany(u => u.rating)
                    .HasForeignKey(o => o.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


        }
    }

}