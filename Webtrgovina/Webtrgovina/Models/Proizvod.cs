
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webtrgovina.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Proizvod :Entitet
    {
        /// <summary>
        /// Naziv u bazi
        /// </summary>
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Naziv{ get; set; }

        /// <summary>
        /// Vrsta u bazi
        /// </summary>
        [Required(ErrorMessage = "Vrsta obavezno")]
        public string? Vrsta{ get; set; }

        /// <summary>
        /// Cijena u bazi
        /// </summary>
        [Range(0, 10000, ErrorMessage = "Vrijednost {0} mora biti između {1} i {2}")]
        public decimal? Cijena{ get; set; }
    }
}
