using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BPMS1.Models;

namespace BPMS1.Data
{
    public class BPMS1Context : DbContext
    {
        public BPMS1Context (DbContextOptions<BPMS1Context> options)
            : base(options)
        {
        }

        public DbSet<BPMS1.Models.BikesModel> BikesModel { get; set; } = default!;

        public DbSet<BPMS1.Models.UsersModel> UsersModel { get; set; }

        public DbSet<BPMS1.Models.PartsModel> PartsModel { get; set; }

        public DbSet<BPMS1.Models.PurchaseRecordsModel> PurchaseRecordsModel { get; set; }

        public DbSet<BPMS1.Models.OrderDetailsModel> OrderDetailsModel { get; set; }

        public DbSet<BPMS1.Models.Cart> Cart { get; set; }

    }
}
