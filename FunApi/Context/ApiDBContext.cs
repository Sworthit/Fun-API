using FunApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Context
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Name> Names { get; set; }

        public DbSet<GeneratedName> GeneratedNames { get; set; }
    }
}
