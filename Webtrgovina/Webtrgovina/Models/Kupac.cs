using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Webtrgovina.Models
{
    public class Kupac : Entitet
    {

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Ime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Prezime { get; set; }
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Adresa { get; set; }
        
        public string? Telefon { get; set; }


    }
}
