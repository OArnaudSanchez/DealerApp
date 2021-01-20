using System.Collections.Generic;
using DealerApp.Core.Common;

namespace DealerApp.Core.Entities
{
    public partial class SangreCliente : BaseEntity
    {
        public SangreCliente()
        {
            Cliente = new HashSet<Cliente>();
            Usuario = new HashSet<Usuario>();
        }

        public string TipoSangre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
