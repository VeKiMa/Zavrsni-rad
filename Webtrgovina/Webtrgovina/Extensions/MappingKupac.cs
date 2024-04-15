using Webtrgovina.Mappers;
using Webtrgovina.Models;

namespace Webtrgovina.Extensions
{
    public static class MappingKupac
    {
        public static List<KupacDTORead> MapKupacReadList(this List<Kupac> lista)
        {
            var mapper = KupacMapper.InicijalizirajReadToDTO();
            var vrati = new List<KupacDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<KupacDTORead>(e));
            });
            return vrati;
        }

        public static KupacDTORead MapKupacReadToDTO(this Kupac entitet)
        {
            var mapper = KupacMapper.InicijalizirajReadToDTO();
            return mapper.Map<KupacDTORead>(entitet);
        }

        public static KupacDTOInsertUpdate MapKupacInsertUpdatedToDTO(this Kupac entitet)
        {
            var mapper = KupacMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<KupacDTOInsertUpdate>(entitet);
        }

        public static Kupac MapKupacInsertUpdateFromDTO(this KupacDTOInsertUpdate dto, Kupac entitet)
        {
            entitet.Ime = dto.ime;
            entitet.Prezime = dto.prezime;
            entitet.Email = dto.email;
            entitet.Adresa = dto.adresa;
            entitet.Telefon = dto.telefon;
            return entitet;
        }

    }
}
