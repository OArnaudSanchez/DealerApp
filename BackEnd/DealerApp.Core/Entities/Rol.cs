using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.Entities
{
    public partial class Rol : BaseEntity
    {
        public RolType TipoRol { get; set; }
        public string? Creacion { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}
