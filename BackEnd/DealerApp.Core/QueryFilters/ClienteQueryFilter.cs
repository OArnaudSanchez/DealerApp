using DealerApp.Core.Common;

namespace DealerApp.Core.QueryFilters
{
    public class ClienteQueryFilter : QueryFilter
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int? Nacimiento { get; set; }

    }
}