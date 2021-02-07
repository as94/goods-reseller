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
            // .HasColumnType("binary(16)");
            builder.HasKey(x => x.Id);
            
            builder.Property(e => e.Version).IsRequired();
            // .HasColumnType("int(11)");
            
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.PasswordHash).IsRequired().HasColumnType("varchar(1024)");
            
            builder.Property(x => x.Role.Name).IsRequired().HasColumnName("Role").HasColumnType("varchar(255)");
            
            builder.Property(x => x.CreationDate.Date).IsRequired().HasColumnName("CreationDate").HasColumnType("datetime");
            builder.Property(x => x.CreationDate.DateUtc).IsRequired().HasColumnName("CreationDateUtc").HasColumnType("datetime");
            builder.Property(x => x.LastUpdateDate.Date).HasColumnName("LastUpdateDate").HasColumnType("datetime");
            builder.Property(x => x.LastUpdateDate.DateUtc).HasColumnName("LastUpdateDateUtc").HasColumnType("datetime");
        }
    }
}