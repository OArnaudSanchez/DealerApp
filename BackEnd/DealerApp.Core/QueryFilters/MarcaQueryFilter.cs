using DealerApp.Core.Common;

namespace DealerApp.Core.QueryFilters
{
    public class MarcaQueryFilter : QueryFilter
    {
        public string Nombre { get; set; }
        public int? Lanzamiento { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}