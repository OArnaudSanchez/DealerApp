using DealerApp.Core.Enumerations;

namespace DealerApp.Core.DTOs
{
    public class CombustibleDTO
    {
        public int? Id { get; set; }
        public CombustibleType? TipoCombustible { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}