using Webtrgovina.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Webtrgovina.Data
{
    /// <summary>
    /// Ovo mi je datoteka gdje ću navoditi datasetove i načine spajanja u bazi
    /// </summary>
    public class WebtrgovinaContext : DbContext
    {
        /// <summary>
        /// Kostruktor
        /// </summary>
        /// <param name="options"></param>
        public WebtrgovinaContext(DbContextOptions<WebtrgovinaContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Narudzbe u bazi
        /// </summary>
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }

        public DbSet<Kupac> Kupci { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // implementacija veze 1:n
            modelBuilder.Entity<Narudzba>().HasOne(g => g.Proizvod);
            modelBuilder.Entity<Narudzba>().HasOne(g => g.Kupac);
           

        }
    }
}

