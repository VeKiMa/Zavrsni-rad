using Webtrgovina.Data;
using Webtrgovina.Extensions;
using Webtrgovina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Webtrgovina.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]



    public class NarudzbaController : ControllerBase
    {

        private readonly WebtrgovinaContext _context;

        public NarudzbaController(WebtrgovinaContext context)
        {
            _context = context;
        }


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
                var lista = _context.Narudzbe
                    .Include(g => g.Kupac)
                    .Include(g => g.Proizvod)
                    .ToList();
                if (lista == null || lista.Count == 0)
                {
                    return BadRequest("Ne postoje narudžbe u bazi");
                }
                /*
                Console.WriteLine("=========================");
                foreach (var item in lista)
                {
                    Console.WriteLine(item.Smjer!.Naziv);
                    Console.WriteLine(item.Predavac!.Ime);
                }
                Console.WriteLine("=========================");
                */
                return new JsonResult(lista.MapNarudzbaReadList());
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
                var p = _context.Narudzbe.Include(i => i.Proizvod).Include(i => i.Kupac);

                if (p == null)
                {
                    return BadRequest("Ne postoji narudžba s šifrom " + sifra + " u bazi");
                }
                return new JsonResult(MapNarudzbaInsertUpdatedToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        private object? MapNarudzbaInsertUpdatedToDTO()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(NarudzbaDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }

            var kupac = _context.Kupci.Find(dto.kupacSifra);

            if (kupac == null)
            {
                return BadRequest("Ne postoji kupac s šifrom " + dto.kupacSifra + " u bazi");
            }

            var proizvod = _context.Proizvodi.Find(dto.proizvodSifra);

            if (proizvod == null)
            {
                return BadRequest("Ne postoji proizvod s šifrom " + dto.proizvodSifra + " u bazi");
            }


            var entitet = dto.MapNarudzbaInsertUpdateFromDTO(new Narudzba());

            entitet.Kupac = kupac;
            entitet.Proizvod = proizvod;


            try
            {
                _context.Narudzbe.Add(entitet);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, entitet.MapNarudzbaReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, NarudzbaDTOInsertUpdate dto)
        {
            if (sifra <= 0 || !ModelState.IsValid || dto == null)
            {
                return BadRequest(ModelState);
            }


            try
            {


                var entitet = _context.Narudzbe.Include(i => i.Kupac).Include(i => i.Proizvod)
                    .FirstOrDefault(x => x.Sifra == sifra);

                if (entitet == null)
                {
                    return BadRequest("Ne postoji narudzba s šifrom " + sifra + " u bazi");
                }

                var kupac = _context.Kupci.Find(dto.kupacSifra);

                if (kupac == null)
                {
                    return BadRequest("Ne postoji kupac s šifrom " + dto.kupacSifra + " u bazi");
                }

                var proizvod = _context.Proizvodi.Find(dto.proizvodSifra);

                if (proizvod == null)
                {
                    return BadRequest("Ne postoji proizvod s šifrom " + dto.proizvodSifra + " u bazi");
                }


                entitet = dto.MapNarudzbaInsertUpdateFromDTO(entitet);

                entitet.Kupac = kupac;
                entitet.Proizvod = proizvod;


                _context.Narudzbe.Update(entitet);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, entitet.MapNarudzbaReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }





        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entitetIzbaze = _context.Narudzbe.Find(sifra);

                if (entitetIzbaze == null)
                {
                    return BadRequest("Ne postoji narudzba s šifrom " + sifra + " u bazi");
                }

                _context.Narudzbe.Remove(entitetIzbaze);
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