using ChatMangementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Mapping
{
    public class MappingBotSettingEntity:IEntityTypeConfiguration<botSetting>
    {
        public void Configure (EntityTypeBuilder<botSetting> builder)
        {
            builder.HasKey(key => key.SettingId);

            builder.HasOne(one => one.Group)
                .WithOne(one => one.BotSetting)
                .HasForeignKey<botSetting>(f => f.Group_Id);

        }
    }
}
