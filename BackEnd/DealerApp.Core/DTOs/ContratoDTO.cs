using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.DTOs
{
    public class ContratoDTO : ImageBase
    {
        public int? Id { get; set; }
        public string Concepto { get; set; }
        public string? Fecha { get; set; }
        public string Descripcion { get; set; }
        public EstatusContratoType? Estatus { get; set; }
        public int IdVehiculo { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
    }
}