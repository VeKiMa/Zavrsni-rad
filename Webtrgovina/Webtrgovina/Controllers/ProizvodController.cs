using Webtrgovina.Data;
using Webtrgovina.Extensions;
using Microsoft.AspNetCore.Mvc;
using Webtrgovina.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Webtrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom proizvod u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProizvodController : ControllerBase
    {
        /// <summary>
        /// Kontest za rad s bazom koji će biti postavljen s pomoću Dependecy Injection-om
        /// </summary>
        private readonly WebtrgovinaContext _context;
        /// <summary>
        /// Konstruktor klase koja prima Skladiste kontext
        /// pomoću DI principa
        /// </summary>
        /// <param name="context"></param>
        public ProizvodController(WebtrgovinaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve proizvode iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Proizvod
        ///    
        /// </remarks>
        /// <returns>Proizvodi u bazi</returns>
        /// <response code="200">Sve OK, ako nema podataka content-length: 0 </response>
        /// <response code="400">Zahtjev nije valjan</response>
        /// <response code="503">Baza na koju se spajam nije dostupna</response>
        [HttpGet]
        public IActionResult Get()
        {
            // kontrola ukoliko upit nije valjan
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lista = _context.Proizvodi.ToList();
                if (lista == null || lista.Count == 0)
                {
                    return BadRequest("Ne postoje proizvodi u bazi");
                }
                return new JsonResult(lista.MapProizvodReadList());
            }
            catch (Exception ex)

            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }
        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {
            // kontrola ukoliko upit nije valjan
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var p = _context.Proizvodi.Find(sifra);
                if (p == null)
                {
                    return BadRequest("Proizvod s šifrom " + sifra + " ne postoji");
                }
                return new JsonResult(p.MapProizvodInsertUpdatedToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novi proizvod u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/Proizvod
        ///     {naziv: "Primjer proizvoda"}
        /// </remarks>
        /// <param name="proizvod">Proizvod za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Smjer s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(ProizvodDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var entitet = dto.MapProizvodInsertUpdateFromDTO(new Proizvod());
                _context.Proizvodi.Add(entitet);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created,
                    entitet.MapProizvodReadToDTO());
            }
            catch (Exception ex)

            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg proizvoda u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/proizvod/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "naziv": "Novi naziv",
        ///  "vrsta proizvoda": "Nova vrsta proizvpoda",
        ///  "cijena": 0
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra proizvoda koji se mijenja</param>  
        /// <param name="proizvod">Proizvod za unijeti u JSON formatu</param>  
        /// <returns>Svi poslani podaci od proizvoda koji su spremljeni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi proizvoda kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, ProizvodDTOInsertUpdate dto
            )
        {
            if (sifra <= 0 || !ModelState.IsValid || dto == null)
            {
                return BadRequest(ModelState);
            }


            try
            {


                var entitetIzBaze = _context.Proizvodi.Find(sifra);

                if (entitetIzBaze == null)
                {
                    return BadRequest("Ne postoje proizvod s šifrom " + sifra + " u bazi");
                }
                var entitet = dto.MapProizvodInsertUpdateFromDTO(entitetIzBaze);



                _context.Proizvodi.Update(entitetIzBaze);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, entitetIzBaze.MapProizvodReadToDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

        /// <summary>
        /// Briše proizvod iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Proizvod/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra proizvoda koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi smjera kojeg želimo obrisati</response>
        /// <response code="503">Problem s bazom</response> 
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var entitetIzBaze = _context.Proizvodi.Find(sifra);

                if (entitetIzBaze == null)
                {
                    return BadRequest("Ne postoji proizvod s šifrom " + sifra + " u bazi");
                }
                var lista = _context.Narudzbe.Include(x => x.Proizvod).Where(x => x.Proizvod.Sifra == sifra).ToList();

                if (lista != null && lista.Count() > 0)

                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Proizvod se ne može obrisati jer je postavljen u narudžbama: ");

                    foreach (var e in lista)
                    {
                        sb.Append(e.Naziv).Append(", ");
                    }

                    return BadRequest(sb.ToString().Substring(0, sb.ToString().Length - 2));
                }



                _context.Proizvodi.Remove(entitetIzBaze);
                _context.SaveChanges();

                return Ok("Obrisano");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

        [HttpPatch]
        public async Task<ActionResult> Patch(int sifraProizvod, IFormFile datoteka)
        {
            if (datoteka == null)
            {
                return BadRequest("Datoteka nije postavljena");
            }

            var entitetIzbaze = _context.Proizvodi.Find(sifraProizvod);

            if (entitetIzbaze == null)
            {
                return BadRequest("Ne postoji proizvod s šifrom " + sifraProizvod + " u bazi");
            }
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "datoteke" + ds + "proizvodi");
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                var putanja = Path.Combine(dir + ds + sifraProizvod + "_" + System.IO.Path.GetExtension(datoteka.FileName));
                Stream fileStream = new FileStream(putanja, FileMode.Create);
                await datoteka.CopyToAsync(fileStream);
                return new JsonResult(new { poruka = "Datoteka pohranjena" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
        }
    }
}
