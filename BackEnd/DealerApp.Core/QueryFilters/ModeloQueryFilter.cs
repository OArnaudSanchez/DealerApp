using DealerApp.Core.Common;

namespace DealerApp.Core.QueryFilters
{
    public class ModeloQueryFilter : QueryFilter
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}