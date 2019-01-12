using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Booking2.Models;

namespace Booking2.Models
{
    public class Booking2Context : DbContext
    {
        public Booking2Context (DbContextOptions<Booking2Context> options)
            : base(options)
        {
        }

        public DbSet<Booking2.Models.User> User { get; set; }

        public DbSet<Booking2.Models.Building> Building { get; set; }

        public DbSet<Booking2.Models.Image> Image { get; set; }

        public DbSet<Booking2.Models.Rent> Rent { get; set; }
    }
}
