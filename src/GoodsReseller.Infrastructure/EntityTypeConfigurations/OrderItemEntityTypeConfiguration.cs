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
            
            builder
                .OwnsOne(o => o.Quantity, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("QuantityValue");
                    x.WithOwner();
                });
            
            builder
                .OwnsOne(o => o.UnitPrice, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("UnitPriceValue");
                    x.WithOwner();
                });
            
            builder
                .OwnsOne(o => o.DiscountPerUnit, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("DiscountPerUnitValue");
                    x.WithOwner();
                });
        }
    }
}