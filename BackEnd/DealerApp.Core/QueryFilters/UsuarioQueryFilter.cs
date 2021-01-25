using DealerApp.Core.Common;

namespace DealerApp.Core.QueryFilters
{
    public class UsuarioQueryFilter : QueryFilter
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Creacion { get; set; }
    }
}