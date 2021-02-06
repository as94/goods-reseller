using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Product = GoodsReseller.DataCatalogContext.Models.Products.Product;

namespace GoodsReseller.Infrastructure
{
    public sealed class GoodsResellerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        public GoodsResellerDbContext()
        {
            Database.EnsureCreated();
        }
    }
}