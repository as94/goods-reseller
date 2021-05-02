using System;
using System.Linq;
using GoodsReseller.DataCatalogContext.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Version).IsRequired().HasColumnType("integer");

            builder.Property(x => x.Label).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(1024)");

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
                .OwnsOne(o => o.AddedCost, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("AddedCostValue").HasDefaultValue(0);
                    x.WithOwner();
                });
            
            var valueComparer = new ValueComparer<Guid[]>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray());

            builder.Property(e => e.ProductIds)
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<Guid[]>(x, _jsonSerializerSettings))
                .Metadata
                .SetValueComparer(valueComparer);

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