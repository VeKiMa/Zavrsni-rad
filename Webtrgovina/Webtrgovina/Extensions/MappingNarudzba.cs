
using System.Text.RegularExpressions;
using Webtrgovina.Mappers;
using Webtrgovina.Models;

namespace Webtrgovina.Extensions
{
    public static class MappingNarudzba
    {

        public static List<NarudzbaDTORead> MapNarudzbaReadList(this List<Narudzba> lista)
        {
            var mapper = NarudzbaMapper.InicijalizirajReadToDTO();
            var vrati = new List<NarudzbaDTORead>();
            lista.ForEach(e =>
            {
                vrati.Add(mapper.Map<NarudzbaDTORead>(e));
            });
            return vrati;
        }

        public static NarudzbaDTORead MapNarudzbaReadToDTO(this Narudzba e)
        {
            var mapper = NarudzbaMapper.InicijalizirajReadToDTO();
            return mapper.Map<NarudzbaDTORead>(e);
        }

        public static NarudzbaDTOInsertUpdate MapNarudzbaInsertUpdatedToDTO(this Narudzba e)
        {
            var mapper = NarudzbaMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<NarudzbaDTOInsertUpdate>(e);


        }

    }

}
