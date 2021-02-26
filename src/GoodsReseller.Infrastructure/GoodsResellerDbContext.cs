using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.EntityTypeConfigurations;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure
{
    public sealed class GoodsResellerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItem> SupplyItems { get; set; }
        
        public GoodsResellerDbContext(DbContextOptions<GoodsResellerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplyItemEntityTypeConfiguration());
        }
    }
}