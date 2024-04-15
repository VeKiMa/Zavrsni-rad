using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace Webtrgovina.Models
{
    public record ProizvodDTORead(int sifra, string naziv, string vrsta,
        decimal? cijena);

    public record ProizvodDTOInsertUpdate(string naziv, string vrsta,
        decimal cijena);




    public record KupacDTORead(int sifra, string ime, string prezime,
        string email, string adresa, string telefon);

    public record KupacDTOInsertUpdate(string ime, string prezime,
        string email, string adresa, string telefon);




    public record NarudzbaDTORead(int sifra, int? kupacsifra, int? proizvodsifra,
       DateTime? datumnarudzbe, string? placanje, float? ukupaniznos);

    public record NarudzbaDTOInsertUpdate(int? kupacSifra, int? proizvodSifra,
       DateTime? datumnarudzbe, string? placanje, float? ukupaniznos)
    {
        //internal Narudzba MapNarudzbaInsertUpdateFromDTO(Narudzba entitet)
        //{
        //    throw new NotImplementedException();
        //}
        internal Narudzba MapNarudzbaInsertUpdateFromDTO(Narudzba entitet)
        {
            throw new NotImplementedException();
        }
    }
}
