using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.DTOs
{
    public class UsuarioDTO : ImageBase
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Creacion { get; set; }
        public bool? Estatus { get; set; }
        public int IdSangre { get; set; }
        public RolType? IdRol { get; set; }

    }
}