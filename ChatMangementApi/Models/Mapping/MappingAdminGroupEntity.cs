using ChatMangementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Mapping
{
    public class MappingAdminGroupEntity : IEntityTypeConfiguration<AdminGroup>
    {
        public void Configure(EntityTypeBuilder<AdminGroup> builder)
        {
            builder.HasKey(key => new { key.AdminId, key.GroupId });
        }
    }
}
