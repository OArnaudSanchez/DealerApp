using AutoMapper;
using DealerApp.Core.DTOs;
using DealerApp.Core.Entities;

namespace DealerApp.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Color, ColorDTO>().ReverseMap();
            CreateMap<Combustible, CombustibleDTO>().ReverseMap();
        }
    }
}