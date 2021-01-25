using DealerApp.Core.Common;

namespace DealerApp.Core.QueryFilters
{
    public class VehiculoQueryFilter : QueryFilter
    {
        public string Matricula { get; set; }
        public string Placa { get; set; }
        public int? Lanzamiento { get; set; }
    }
}