using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstAttempt.Models;

namespace FirstAttempt.Data
{
    public class FirstAttemptContext : DbContext
    {
        public FirstAttemptContext (DbContextOptions<FirstAttemptContext> options)
            : base(options)
        {
        }

        public DbSet<FirstAttempt.Models.Bikes> Bikes { get; set; } = default!;
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
