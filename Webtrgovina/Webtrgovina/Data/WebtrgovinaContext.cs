using Webtrgovina.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        /// Kupci u bazi
        /// </summary>
        public DbSet<Kupac> kupci { get; set; }

        /// <summary>
        /// Proizvodi u bazi
        /// </summary>

        public DbSet<Proizvod> Proizvodi { get; set; }

        ///// <summary>
        ///// Narudzbe u bazi
        ///// </summary>

        //public DbSet<Narudzba> Narudzbe { get; set; }

    }  
}
