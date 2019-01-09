using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MLZ_Sefer_Xheladini.Models;

namespace MLZ_Sefer_Xheladini.Models
{
    public class MLZ_Sefer_XheladiniContext : DbContext
    {
        public MLZ_Sefer_XheladiniContext (DbContextOptions<MLZ_Sefer_XheladiniContext> options)
            : base(options)
        {
        }

        public DbSet<MLZ_Sefer_Xheladini.Models.Building> Building { get; set; }

        public DbSet<MLZ_Sefer_Xheladini.Models.User> User { get; set; }

        public DbSet<MLZ_Sefer_Xheladini.Models.Rent> Rent { get; set; }
    }
}
