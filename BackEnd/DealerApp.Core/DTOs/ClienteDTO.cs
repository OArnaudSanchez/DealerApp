using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.DTOs
{
    public class ClienteDTO : ImageBase
    {
        public int? Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public SexoEnum? Sexo { get; set; }
        public string Nacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool? Estatus { get; set; }
        public int IdSangre { get; set; }
        public RolType? IdRol { get; set; }

    }
}