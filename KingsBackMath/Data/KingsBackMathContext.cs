using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KingsBackMath.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KingsBackMath.Data
{
    public class KingsBackMathContext : IdentityDbContext<GameUser>
    {
        public KingsBackMathContext(DbContextOptions<KingsBackMathContext> options) : base(options)
        {
            
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<GameDefinition> GameDefinitions { get; set; }
    }
}
