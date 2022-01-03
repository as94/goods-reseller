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
            
            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Version).IsRequired().HasColumnType("integer").HasDefaultValue(1);
            builder.UseXminAsConcurrencyToken();
            
            builder.Property(x => x.SupplierInfo)
                .IsRequired()
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<SupplierInfo>(x, _jsonSerializerSettings))
                .HasDefaultValueSql("'{}'");

            builder
                .OwnsOne(x => x.TotalCost, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("TotalCostValue");
                    x.WithOwner();
                })
                .Navigation(x => x.TotalCost)
                .IsRequired();

            var navigation =
                builder.Metadata.FindNavigation(nameof(Supply.SupplyItems));
            
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
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