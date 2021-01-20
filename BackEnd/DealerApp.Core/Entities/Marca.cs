using System.Collections.Generic;
using DealerApp.Core.Common;

namespace DealerApp.Core.Entities
{
    public partial class Marca : BaseEntity
    {
        public Marca()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public string Nombre { get; set; }
        public string Lanzamiento { get; set; }
        public string Foto { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
