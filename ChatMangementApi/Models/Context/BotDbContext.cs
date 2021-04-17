using ChatMangementApi.Models.Entities;
using ChatMangementApi.Models.Extenstions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Context
{
    public class BotDbContext:DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
    : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CustomMapper();
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Group> Groups { get; set; }
        public DbSet<AdminGroup> AdminGroups { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<botSetting> BotSettings { get; set; }
        public DbSet<LimitedPeople> LimitedPeoples { get; set; }
    }
}
