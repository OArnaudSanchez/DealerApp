using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.QueryFilters
{
    public class ContratoQueryFilter : QueryFilter
    {
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public EstatusContratoType? Estatus { get; set; }
        public int? Vehiculo { get; set; }
        public int? Cliente { get; set; }
        public int? Usuario { get; set; }
    }
}