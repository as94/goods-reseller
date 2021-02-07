using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.Property(e => e.Id).IsRequired();
            // .HasColumnType("binary(16)");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Version).IsRequired();
            // .HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<Address>(x, _jsonSerializerSettings));
                
            builder.Property(e => e.CustomerInfo)
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<CustomerInfo>(x, _jsonSerializerSettings));

            builder.Property(e => e.TotalCost.Value).IsRequired()
                .HasColumnName("TotalCost");
            // .HasColumnType("int(11)");
                
            var navigation =
                builder.Metadata.FindNavigation(nameof(Order.OrderItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            // builder.HasMany(x => x.OrderItems);

            builder.Property(x => x.IsRemoved).IsRequired();//.HasColumnType("bit(1)");
            
            builder.Property(x => x.CreationDate.Date).IsRequired().HasColumnName("CreationDate").HasColumnType("datetime");
            builder.Property(x => x.CreationDate.DateUtc).IsRequired().HasColumnName("CreationDateUtc").HasColumnType("datetime");
            builder.Property(x => x.LastUpdateDate.Date).HasColumnName("LastUpdateDate").HasColumnType("datetime");
            builder.Property(x => x.LastUpdateDate.DateUtc).HasColumnName("LastUpdateDateUtc").HasColumnType("datetime");
        }
    }
}