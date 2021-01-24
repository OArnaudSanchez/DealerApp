using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.QueryFilters
{
    public class RolQueryFilter : QueryFilter
    {
        public RolType? TipoRol { get; set; }
        public string Descripcion { get; set; }
    }
}