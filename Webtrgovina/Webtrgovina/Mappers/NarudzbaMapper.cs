using AutoMapper;
using System.Text.RegularExpressions;
using Webtrgovina.Controllers;
using Webtrgovina.Models;

namespace Webtrgovina.Mappers
{
    public class NarudzbaMapper
    {
        public static Mapper InicijalizirajReadToDTO() => new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Narudzba, NarudzbaDTORead>();
                })
                );

        public static Mapper InicijalizirajReadFromDTO()
        {
            Mapper mapper = new Mapper(
                            new MapperConfiguration(c =>
                            {
                                c.CreateMap<NarudzbaDTORead, Narudzba>();
                            })
                            );
            return mapper;
        }

        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Narudzba, NarudzbaDTOInsertUpdate>();
                })
                );
        }
    }
}