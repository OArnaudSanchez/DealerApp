using DealerApp.Core.Enumerations;

namespace DealerApp.Core.DTOs
{
    public class RolDTO
    {
        public int? Id { get; set; }
        public RolType? TipoRol { get; set; }
        public string? Creacion { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}