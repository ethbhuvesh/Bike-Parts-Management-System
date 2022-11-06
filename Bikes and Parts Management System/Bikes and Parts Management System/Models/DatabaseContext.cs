using Microsoft.EntityFrameworkCore;

namespace Bikes_and_Parts_Management_System.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BikesInventory> Inventory { get; set; }
        public DbSet<PartsAndAccessoriesInventory> PartsAndAccessories { get; set; }
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=D:\Programs(Coding)\.Net c#\ENPM809WFall2022Project-bgupta1\Bikes and Parts Management System\Bikes and Parts Management System\bpms.db");
    }
}
