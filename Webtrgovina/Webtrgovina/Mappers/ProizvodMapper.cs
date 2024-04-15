using AutoMapper;
using System.Net.NetworkInformation;
using Webtrgovina.Models;
using Microsoft.Extensions.Configuration;

namespace Webtrgovina.Mappers
{
    public class ProizvodMapper
    {
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.AllowNullDestinationValues = true;
                    c.CreateMap<Proizvod, ProizvodDTORead>();
                })
                );
        }

        public static Mapper InicijalizirajReadFromDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<ProizvodDTORead, Proizvod>();
                })
                );
        }

        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Proizvod, ProizvodDTOInsertUpdate>();
                })
                );
        }

    }
}
