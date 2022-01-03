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

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Version).IsRequired().HasColumnType("integer");
            builder.UseXminAsConcurrencyToken();
            
            builder
                .OwnsOne(x => x.Status, x =>
                {
                    x.Property(x => x.Id).IsRequired();
                    x.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");
                    x.WithOwner();
                })
                .Navigation(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<Address>(x, _jsonSerializerSettings))
                .HasDefaultValueSql("'{}'");
                
            builder.Property(x => x.CustomerInfo)
                .IsRequired()
                .HasColumnType("json")
                .HasConversion(
                    x => JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    x => JsonConvert.DeserializeObject<CustomerInfo>(x, _jsonSerializerSettings))
                .HasDefaultValueSql("'{}'");

            builder
                .OwnsOne(x => x.DeliveryCost, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("DeliveryCostValue").HasDefaultValue(0);
                    x.WithOwner();
                })
                .Navigation(x => x.DeliveryCost)
                .IsRequired();
                
            builder
                .OwnsOne(x => x.AddedCost, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("AddedCostValue").HasDefaultValue(0);
                    x.WithOwner();
                })
                .Navigation(x => x.AddedCost)
                .IsRequired();
            
            builder
                .OwnsOne(x => x.TotalCost, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnName("TotalCostValue");
                    x.WithOwner();
                })
                .Navigation(x => x.TotalCost)
                .IsRequired();
                
            var navigation =
                builder.Metadata.FindNavigation(nameof(Order.OrderItems));

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