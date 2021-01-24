using DealerApp.Core.Common;

namespace DealerApp.Core.QueryFilters
{
    public class SangreClienteQueryFilter : QueryFilter
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}