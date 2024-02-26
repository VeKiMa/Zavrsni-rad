using System.ComponentModel.DataAnnotations;

namespace Webtrgovina.Models
{
    /// <summary>
    /// Ovo je vršna nad klasa koja služi za osnovne atribute
    /// tipa sifra, operater, datum unosa, promjene, itd.
    /// </summary>
    public abstract class Entitet
    {
        /// <summary>
        /// Ovo svojstvo mi služi kao primarni ključ u bazi s
        /// generiranjem vrijednosti identiti(1,1)
        /// </summary>
        [Key]
        public int Sifra { get; set; }
    }
}
