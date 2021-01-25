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
            CreateMap<Contrato, ContratoDTO>().ReverseMap();
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<Modelo, ModeloDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<SangreClienteDTO, SangreCliente>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Vehiculo, VehiculoDTO>().ReverseMap();
        }
    }
}