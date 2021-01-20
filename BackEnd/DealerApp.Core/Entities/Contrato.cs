using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.Entities
{
    public partial class Contrato : BaseEntity
    {
        public string Concepto { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public EstatusContratoType Estatus { get; set; }
        public int IdVehiculo { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual Vehiculo IdVehiculoNavigation { get; set; }
    }
}
