using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZoneFranche.Models;

namespace ZoneFranche.Data
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Demande>()
                .HasOne(d => d.Visiteur)
                .WithMany()
                .HasForeignKey(d => d.idVisiteur);

           modelBuilder.Entity<Visiteur>()
               .HasOne(d => d.Login)
               .WithMany()
               .HasForeignKey(d => d.idLogin);

            modelBuilder.Entity<Employee>()
               .HasOne(d => d.Login)
               .WithMany()
               .HasForeignKey(d => d.idLogin);

        }
        public DbSet<Models.Visiteur> Visiteurs { get; set; }
        public DbSet<Models.Demande> Demandes { get; set; }
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Entreprise> Entreprises { get; set; }
        public DbSet<Models.Login> Logins { get; set; }

    }
}
