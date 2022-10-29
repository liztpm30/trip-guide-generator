using System;
using Microsoft.EntityFrameworkCore;
using trip_guide_generator.Model;

namespace trip_guide_generator.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){}

        public DbSet<User> User { get; set; }
    }
}

