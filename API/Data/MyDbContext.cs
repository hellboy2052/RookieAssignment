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

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Rate> Rates { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Picture> Pictures { get; set; }
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
            builder.Entity<Rate>(b =>
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

            builder.Entity<CartItem>(b =>
            {
                b.HasKey(k => new { k.userId, k.productId });
                b.HasOne(c => c.User)
                    .WithMany(u => u.Cart)
                    .HasForeignKey(o => o.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<OrderDetail>(b => {
                b.HasKey(k => new {k.orderId, k.productId});
                b.HasOne(o => o.Order)
                    .WithMany(s => s.orders)
                    .HasForeignKey(k => k.orderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}