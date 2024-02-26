using Webtrgovina.Data;
using Microsoft.AspNetCore.Mvc;
using Webtrgovina.Models;
using Microsoft.Data.SqlClient;


namespace Webtrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom proizvod u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProizvodController :ControllerBase
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
                var proizvodi = _context.Proizvodi.ToList();
                if (proizvodi == null || proizvodi.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(proizvodi);
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
        public IActionResult Post(Proizvod proizvod)
        {
            if (!ModelState.IsValid || proizvod == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Proizvodi.Add(proizvod);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, proizvod);
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
        public IActionResult Put(int sifra, Proizvod proizvod)
        {
            if (sifra <= 0 || !ModelState.IsValid || proizvod == null)
            {
                return BadRequest();
            }


            try
            {


                var proizvodIzBaze = _context.Proizvodi.Find(sifra);

                if (proizvodIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }


                // inače ovo rade mapperi
                // za sada ručno
                proizvodIzBaze.Naziv = proizvod.Naziv;
                proizvodIzBaze.Vrsta = proizvod.Vrsta;
                proizvodIzBaze.Cijena = proizvod.Cijena;


                _context.Proizvodi.Update(proizvodIzBaze);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, proizvodIzBaze);
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
                var proizvodIzBaze = _context.Proizvodi.Find(sifra);

                if (proizvodIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Proizvodi.Remove(proizvodIzBaze);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\": \"Obrisano\"}"); // ovo nije baš najbolja praksa ali da znake kako i to može

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }
    }
}
