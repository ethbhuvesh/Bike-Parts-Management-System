using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScaffoldingCheckApp.Models;

namespace ScaffoldingCheckApp.Data
{
    public class ScaffoldingCheckAppContext : DbContext
    {
        public ScaffoldingCheckAppContext (DbContextOptions<ScaffoldingCheckAppContext> options)
            : base(options)
        {
        }

        public DbSet<ScaffoldingCheckApp.Models.Users> Users { get; set; } = default!;

        public DbSet<ScaffoldingCheckApp.Models.Roles> Roles { get; set; }

        public DbSet<ScaffoldingCheckApp.Models.PurchaseRecord> PurchaseRecord { get; set; }

        public DbSet<ScaffoldingCheckApp.Models.PartsAndAccessoriesInventory> PartsAndAccessoriesInventory { get; set; }

        public DbSet<ScaffoldingCheckApp.Models.BikesInventory> BikesInventory { get; set; }
    }
}

