using ChatMangementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Mapping
{
    public class MappingAdminEntity : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(key => key.Id);

            builder.HasMany(many => many.AdminGroups)
                .WithOne(one => one.Admin)
                .HasForeignKey(f => f.AdminId);

        }
    }
}
