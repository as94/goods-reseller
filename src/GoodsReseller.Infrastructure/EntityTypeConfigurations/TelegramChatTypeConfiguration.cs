using GoodsReseller.NotificationContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsReseller.Infrastructure.EntityTypeConfigurations
{
    internal sealed class TelegramChatTypeConfiguration : IEntityTypeConfiguration<TelegramChat>
    {
        public void Configure(EntityTypeBuilder<TelegramChat> builder)
        {
            builder.ToTable("telegram_chats");

            builder.Property(x => x.ChatId).IsRequired();
            builder.HasKey(x => x.ChatId);
            
            builder.Property(x => x.UserName).IsRequired().HasColumnType("varchar(255)");
        }
    }
}