using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");
                
            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(e => e.ProductId).IsRequired();
                
            builder.Property(e => e.Quantity.Value).IsRequired().HasColumnName("Quantity");
            builder.Property(e => e.UnitPrice.Value).IsRequired().HasColumnName("UnitPrice");
            builder.Property(e => e.DiscountPerUnit.Value).IsRequired().HasColumnName("DiscountPerUnit");
        }
    }
}