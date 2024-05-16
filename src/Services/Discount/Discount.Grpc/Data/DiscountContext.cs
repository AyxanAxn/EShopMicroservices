using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options)
            : base(options)
        { }

        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 3,
                    Amount = 4,
                    Description = "Iphone is the best phone",
                    ProductName = "Iphone X"
                },
                new Coupon
                {
                    Id = 4,
                    Amount = 4,
                    Description = "Samsung is the worst phone",
                    ProductName = "Samsung S 20"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}