using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webtrgovina.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Kupac : Entitet
    {
        /// <summary>
        /// Ime u bazi
        /// </summary>
        [Required(ErrorMessage = "Ime obavezno")]
        public string? Ime{ get; set; }
        /// <summary>
        /// Prezime u bazi
        /// </summary>
        [Required(ErrorMessage = "Prezime obavezno")]
        public string? Prezime { get; set; }
        /// <summary>
        /// Email u bazi
        /// </summary>
        [Required(ErrorMessage = "Prezime obavezno")]
        public string? Email { get; set; }
        /// <summary>
        /// Adresa u bazi
        /// </summary>
        [Required(ErrorMessage = "Adresa obavezno")]
        public string? Adresa{ get; set; }
        /// <summary>
        /// Adresa u bazi
        /// </summary>
        [Required(ErrorMessage = "Telefon obavezno")]
        public string? Telefon{ get; set; }
    }
}
