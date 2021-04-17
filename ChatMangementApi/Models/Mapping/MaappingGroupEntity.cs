using ChatMangementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Mapping
{
    public class MaappingGroupEntity : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(Key => Key.Id);

            builder.HasMany(many => many.AdminGroups)
                .WithOne(one => one.Group)
                .HasForeignKey(f => f.GroupId);

            builder.HasMany(m => m.LimitedPeople)
                .WithOne(one => one.Group)
                .HasForeignKey(f => f.Group_Id);
        }
    }
}
