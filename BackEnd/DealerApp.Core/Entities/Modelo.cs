using System.Collections.Generic;
using DealerApp.Core.Common;

namespace DealerApp.Core.Entities
{
    public partial class Modelo : BaseEntity
    {
        public Modelo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
