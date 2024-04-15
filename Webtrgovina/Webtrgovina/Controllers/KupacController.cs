using Webtrgovina.Data;
using Webtrgovina.Models;
using Microsoft.AspNetCore.Mvc;
using Webtrgovina.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Webtrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom kupac u bazi
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
        /// Konstruktor klase koja prima Edunova kontext
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
        /// <returns>Kupci u bazi</returns>
        /// <response code="200">Sve OK, ako nema podataka content-length: 0 </response>
        /// <response code="400">Zahtjev nije valjan</response>
        /// <response code="503">Baza na koju se spajam nije dostupna</response

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
                var kupci = _context.Kupci.ToList();
                if (kupci == null || kupci.Count == 0)
                {
                    return BadRequest("Ne postoje smjerovi u bazi");
                }
                return new JsonResult(kupci.MapKupacReadList());
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
                var kupac = _context.Kupci.Find(sifra);
                    
                if (kupac == null)
                {
                    return BadRequest("Kupac s šifrom" + " ne postoji");
                }
                return new JsonResult(kupac.MapKupacInsertUpdatedToDTO());
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
        ///     {ime: "Primjer imena"}
        /// </remarks>
        /// <param name="kupac">Kupac za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Kupac s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(KupacDTOInsertUpdate kupacDTO)
        {
            if (!ModelState.IsValid || kupacDTO == null)
            {
                return BadRequest();
            }
            try
            {
                var kupac = kupacDTO.MapKupacInsertUpdateFromDTO(new Kupac());                   
                _context.Kupci.Add(kupac);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, 
                    kupac.MapKupacReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }
        /// <summary>
        /// Mijenja podatke postojećeg smjera u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/smjer/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "naziv": "Novi naziv",
        ///  "trajanje": 120,
        ///  "cijena": 890.22,
        ///  "upisnina": 0,
        ///  "verificiran": true
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra smjera koji se mijenja</param>  
        /// <param name="smjer">Smjer za unijeti u JSON formatu</param>  
        /// <returns>Svi poslani podaci od smjera koji su spremljeni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi smjera kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response>
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, KupacDTOInsertUpdate kupacDTO)
        {
            if (sifra <= 0 || !ModelState.IsValid || kupacDTO == null)
            {
                return BadRequest();
            }


            try
            {


                var kupacIzBaze = _context.Kupci.Find(sifra);

                if (kupacIzBaze == null)
                {
                    return BadRequest("Ne postoji kupac s šifrom" + sifra + " u bazi");
                }

                var kupac = kupacDTO.MapKupacInsertUpdateFromDTO(kupacIzBaze);

                _context.Kupci.Update(kupac);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, kupac.MapKupacInsertUpdatedToDTO());
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
        /// <param name="sifra">Šifra kupca koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi skupca kojeg želimo obrisati</response>
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
                var entitetIzbaze = _context.Kupci.Find(sifra);

                if (entitetIzbaze == null)
                {
                    return BadRequest("Ne postoji kupac s šifrom " + sifra + " u bazi");
                }

                var lista = _context.Narudzbe.Include(x => x.Kupac).Where(x => x.Kupac.Sifra == sifra).ToList();

                if (lista != null && lista.Count() > 0) 
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Kupac se ne može obrisati jer je postavljen u narudzbama: ");
                    foreach (var e in lista) 
                    {
                        sb.Append(e.Sifra).Append(", ");
                    }
                
                    return BadRequest(sb.ToString().Substring(0, sb.ToString().Length - 2));
                
                }

                _context.Kupci.Remove(entitetIzbaze);
                _context.SaveChanges();

                return Ok("Obrisano"); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }



    }

}
