using ChatMangementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Mapping
{
    public class MappingimitedPeopleEntity : IEntityTypeConfiguration<LimitedPeople>
    {
        public void Configure(EntityTypeBuilder<LimitedPeople> builder)
        {
            builder.HasKey(key => key.Id);
        }
    }
}
