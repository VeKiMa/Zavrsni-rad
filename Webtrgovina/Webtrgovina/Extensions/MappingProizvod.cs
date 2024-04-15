
using Webtrgovina.Mappers;
using Webtrgovina.Models;

namespace Webtrgovina.Extensions
{
    public static class MappingProizvod
    {
        public static List<ProizvodDTORead> MapProizvodReadList(this List<Proizvod> lista)
        {
            var mapper = ProizvodMapper.InicijalizirajReadToDTO();
            var vrati = new List<ProizvodDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<ProizvodDTORead>(e));
            });
            return vrati;
        }

        public static ProizvodDTORead MapProizvodReadToDTO(this Proizvod entitet)
        {
            var mapper = ProizvodMapper.InicijalizirajReadToDTO();
            return mapper.Map<ProizvodDTORead>(entitet);
        }

        public static ProizvodDTOInsertUpdate MapProizvodInsertUpdatedToDTO(this Proizvod entitet)
        {
            var mapper = ProizvodMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<ProizvodDTOInsertUpdate>(entitet);
        }

        public static Proizvod MapProizvodInsertUpdateFromDTO(this ProizvodDTOInsertUpdate dto, Proizvod entitet)
        {
            entitet.Naziv = dto.naziv;
            entitet.Vrsta = dto.vrsta;
            entitet.Cijena = dto.cijena;           
            return entitet;
        }
    }
}
