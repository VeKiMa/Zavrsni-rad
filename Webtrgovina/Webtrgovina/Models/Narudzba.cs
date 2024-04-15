
using System.ComponentModel.DataAnnotations.Schema;

namespace Webtrgovina.Models
{

    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Narudzba : Entitet
    {
        
        public string? NarudzbaSifra { get; set; }
        // ovo pod navodnicima je naziv kolone u tablici narudzbe
        [ForeignKey("kupac")]
        public Kupac? Kupac { get; set; }

        [ForeignKey("proizvod")]
        public Proizvod? Proizvod { get; set; }
        public DateTime? Datumnarudzbe { get; set; }
        public string? Placanje { get; set; }
        public float? Ukupaniznos { get; set; }
        //public bool Naziv { get; internal set; }
    }
}

    

