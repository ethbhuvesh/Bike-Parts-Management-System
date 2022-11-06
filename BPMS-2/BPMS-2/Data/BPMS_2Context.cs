using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BPMS_2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BPMS_2.Data
{
    public class BPMS_2Context : IdentityDbContext
    {
        public BPMS_2Context (DbContextOptions<BPMS_2Context> options)
            : base(options)
        {
        }
        
        public DbSet<BPMS_2.Models.UsersModel> UsersModel { get; set; } = default!;

        public DbSet<BPMS_2.Models.ProductsModel> ProductsModel { get; set; }
        public DbSet<BPMS_2.Models.OrderDetailsModel> OrderDetailsModel { get; set; }
        public DbSet<BPMS_2.Models.CartModel> CartModel { get; set; }
        public DbSet<BPMS_2.Models.RentBikesModel> RentBikesModel { get; set; }


    }
}
