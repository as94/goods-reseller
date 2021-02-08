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

            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Version).IsRequired().HasColumnType("integer");

            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(255)");
            
            builder
                .OwnsOne(o => o.PasswordHash, x =>
                {
                    x.Property(x => x.Value).IsRequired().HasColumnType("varchar(1024)");
                    x.WithOwner();
                });

            builder
                .OwnsOne(o => o.Role, x =>
                {
                    x.Property(x => x.Id).IsRequired();
                    x.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");
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