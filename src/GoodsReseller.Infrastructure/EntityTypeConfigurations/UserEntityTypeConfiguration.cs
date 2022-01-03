using GoodsReseller.AuthContext.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Version).IsRequired().HasColumnType("integer");
            builder.UseXminAsConcurrencyToken();

            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(255)");
            builder.HasIndex(x => x.Email).IsUnique();
            
            builder
                .OwnsOne(x => x.PasswordHash, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnType("varchar(1024)");
                    x.WithOwner();
                })
                .Navigation(x => x.PasswordHash)
                .IsRequired();

            builder
                .OwnsOne(x => x.Role, x =>
                {
                    x.Property(x => x.Id).IsRequired();
                    x.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");
                    x.WithOwner();
                })
                .Navigation(x => x.Role)
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