using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZhiDaCore.Entity.Identity;
using ZhiDaCore.Entity.Trades;

namespace ZhiDaCore.EFCore
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Trade> Trade { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>().ToTable("Trade");
            modelBuilder.Entity<Trade>().HasKey(c=>c.SequenceID);
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
