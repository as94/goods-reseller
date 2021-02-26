using GoodsReseller.SupplyContext.Domain.Supplies.Entities;
using GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class SupplyEntityTypeConfiguration : IEntityTypeConfiguration<Supply>
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.ToTable("supplies");
            
            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            
            builder.Property(e => e.SupplierInfo)
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<SupplierInfo>(x, _jsonSerializerSettings));

            builder
                .OwnsOne(o => o.TotalCost, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("TotalCostValue");
                    x.WithOwner();
                });
            
            
            var navigation =
                builder.Metadata.FindNavigation(nameof(Supply.SupplyItems));
            
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
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