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
            
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).IsRequired();
            
            builder
                .OwnsOne(x => x.Quantity, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("QuantityValue");
                    x.WithOwner();
                })
                .Navigation(x => x.Quantity)
                .IsRequired();
            
            builder
                .OwnsOne(x => x.UnitPrice, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("UnitPriceValue");
                    x.WithOwner();
                })
                .Navigation(x => x.UnitPrice)
                .IsRequired();
            
            builder
                .OwnsOne(x => x.DiscountPerUnit, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("DiscountPerUnitValue");
                    x.WithOwner();
                })
                .Navigation(x => x.DiscountPerUnit)
                .IsRequired();
            
            builder
                .OwnsOne(x => x.CreationDate, x =>
                {
                    x.Property(x => x.Date).IsRequired().HasColumnName("CreationDate");
                    x.Property(x => x.DateUtc).IsRequired().HasColumnName("CreationDateUtc");
                    x.WithOwner();
                })
                .Navigation(x => x.CreationDate)
                .IsRequired();

            builder
                .OwnsOne(x => x.LastUpdateDate, x =>
                {
                    x.Property(x => x.Date).HasColumnName("LastUpdateDate");
                    x.Property(x => x.DateUtc).HasColumnName("LastUpdateDateUtc");
                    x.WithOwner();
                });

            builder.Property(x => x.IsRemoved).IsRequired().HasColumnType("boolean");
        }
    }
}