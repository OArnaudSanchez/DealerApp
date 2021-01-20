using System.Collections.Generic;
using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.Entities
{
    public partial class Usuario : BaseEntity
    {
        public Usuario()
        {
            Contrato = new HashSet<Contrato>();
        }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Foto { get; set; }
        public string Creacion { get; set; }
        public bool? Estatus { get; set; }
        public int IdSangre { get; set; }
        public RolType IdRol { get; set; }
        public virtual SangreCliente IdSangreNavigation { get; set; }
        public virtual ICollection<Contrato> Contrato { get; set; }
    }
}
