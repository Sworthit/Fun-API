using FunApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FunApi.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<NameModel> Names { get; set; }

        public DbSet<GeneratedName> GeneratedNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameModel>().HasData(
                new NameModel { Id = 1, Name = "Matnot" },
                new NameModel { Id = 2, Name = "Maciek" }
                );
        }
    }
}
