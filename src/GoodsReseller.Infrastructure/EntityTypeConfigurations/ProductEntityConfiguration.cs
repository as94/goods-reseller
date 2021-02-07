using System;
using GoodsReseller.DataCatalogContext.Models.Products;
using Microsoft.EntityFrameworkCore;
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
            // .HasColumnType("binary(16)");
            builder.HasKey(x => x.Id);
            
            builder.Property(e => e.Version).IsRequired();
            // .HasColumnType("int(11)");
            
            builder.Property(x => x.Label).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(1024)");
            builder.Property(e => e.UnitPrice.Value).IsRequired().HasColumnName("UnitPrice");
            builder.Property(e => e.DiscountPerUnit.Value).IsRequired().HasColumnName("DiscountPerUnit");
            
            builder.Property(e => e.ProductIds)
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<Guid[]>(x, _jsonSerializerSettings));
            
            builder.Property(x => x.CreationDate.Date).IsRequired().HasColumnName("CreationDate").HasColumnType("datetime");
            builder.Property(x => x.CreationDate.DateUtc).IsRequired().HasColumnName("CreationDateUtc").HasColumnType("datetime");
            builder.Property(x => x.LastUpdateDate.Date).HasColumnName("LastUpdateDate").HasColumnType("datetime");
            builder.Property(x => x.LastUpdateDate.DateUtc).HasColumnName("LastUpdateDateUtc").HasColumnType("datetime");
        }
    }
}