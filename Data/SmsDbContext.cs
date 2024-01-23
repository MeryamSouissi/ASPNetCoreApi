using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZoneFranche.Data
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Entreprise> Entreprises { get; set; }
    }
}
