using ChatMangementApi.Models.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Extenstions
{
    public static class CustomEntityMapper
    {
        public static void CustomMapper(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MaappingGroupEntity());
            modelBuilder.ApplyConfiguration(new MappingAdminEntity());
            modelBuilder.ApplyConfiguration(new MappingAdminGroupEntity());
            modelBuilder.ApplyConfiguration(new MappingBotSettingEntity());
            modelBuilder.ApplyConfiguration(new MappingimitedPeopleEntity());
        }
    }
}
