using GoodsReseller.SupplyContext.Domain.Supplies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class SupplyItemEntityTypeConfiguration : IEntityTypeConfiguration<SupplyItem>
    {
        public void Configure(EntityTypeBuilder<SupplyItem> builder)
        {
            builder.ToTable("supply_items");
            
            builder.Property(e => e.Id).IsRequired().ValueGeneratedNever();
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
            
            builder
                .OwnsOne(o => o.CreationDate, x =>
                {
                    x.Property(x => x.Date).IsRequired().HasColumnName("CreationDate");
                    x.Property(x => x.DateUtc).IsRequired().HasColumnName("CreationDateUtc");
                    x.WithOwner();
                });

            builder
                .OwnsOne(o => o.LastUpdateDate, x =>
                {
                    x.Property(x => x.Date).HasColumnName("LastUpdateDate");
                    x.Property(x => x.DateUtc).HasColumnName("LastUpdateDateUtc");
                    x.WithOwner();
                });

            builder.Property(x => x.IsRemoved).IsRequired().HasColumnType("boolean");
        }
    }
}