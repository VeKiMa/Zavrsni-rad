using AutoMapper;
using Webtrgovina.Models;

namespace Webtrgovina.Mappers
{
    public class KupacMapper
    {
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Kupac, KupacDTORead>();
                })
                );
        }

        public static Mapper InicijalizirajReadFromDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<KupacDTORead, Kupac>();
                })
                );
        }

        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Kupac, KupacDTOInsertUpdate>();
                })
                );
        }
    }
}
