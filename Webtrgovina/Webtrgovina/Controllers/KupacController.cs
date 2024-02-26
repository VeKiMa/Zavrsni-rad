using Webtrgovina.Data;
using Webtrgovina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace Webtrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom osoba u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KupacController : ControllerBase
    {
        /// <summary>
        /// Kontest za rad s bazom koji će biti postavljen s pomoću Dependecy Injection-om
        /// </summary>
        private readonly WebtrgovinaContext _context;
        /// <summary>
        /// Konstruktor klase koja prima Webtrgovina kontext
        /// pomoću DI principa
        /// </summary>
        /// <param name="context"></param>
        public KupacController(WebtrgovinaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve kupce iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Kupac
        ///    
        /// </remarks>
        /// <returns>Kupce u bazi</returns>
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
                var kupci = _context.kupci.ToList();
                if (kupci == null || kupci.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(kupci);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }
        /// <summary>
        /// Dodaje novog kupca u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/Kupac
        ///     {naziv: "Primjer naziva"}
        /// </remarks>
        /// <param name="kupac">Kupac za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Kupac s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(Kupac kupac)
        {
            if (!ModelState.IsValid || kupac == null)
            {
                return BadRequest();
            }
            try
            {
                _context.kupci.Add(kupac);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, kupac);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojeće Kupca u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/kupac/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "Ime": "Novo ime",
        ///  "Prezime": "Novo prezime",
        ///  "Email": "Novi Email",
        ///  "Adresa": "Nova adresa",
        ///  "Telefon":"Novi broj telefona"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra kupca koji se mijenja</param>  
        /// <param name="kupac">Kupac za unijeti u JSON formatu</param>  
        /// <returns>Svi poslani podaci od kupca koji su spremljeni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi kupca kojeu želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra,Kupac kupac)
        {
            if (sifra <= 0 || !ModelState.IsValid || kupac == null)
            {
                return BadRequest();
            }


            try
            {


                var kupacIzBaze = _context.kupci.Find(sifra);

                if (kupacIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }


                // inače ovo rade mapperi
                // za sada ručno
                kupacIzBaze.Ime = kupac.Ime;
                kupacIzBaze.Prezime = kupac.Prezime;
                kupacIzBaze.Email = kupac.Email;
                kupacIzBaze.Adresa = kupac.Adresa;
                kupacIzBaze.Telefon =  kupac.Telefon;


                _context.kupci.Update(kupacIzBaze);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, kupacIzBaze);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

        /// <summary>
        /// Briše kupca iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/kupac/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra kupca koja se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi kupca kojeu želimo obrisati</response>
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
                var kupacIzBaze = _context.kupci.Find(sifra);

                if (kupacIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.kupci.Remove(kupacIzBaze);
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
