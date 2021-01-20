using System.Collections.Generic;
using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.Entities
{
    public partial class Combustible : BaseEntity
    {
        public Combustible()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }
        public CombustibleType TipoCombustible { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }


        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
