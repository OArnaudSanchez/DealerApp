using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.QueryFilters
{
    public class CombustibleQueryFilter : QueryFilter
    {
        public CombustibleType? TipoCombustible { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}