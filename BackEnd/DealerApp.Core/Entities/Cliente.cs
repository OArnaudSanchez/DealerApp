using System.Collections.Generic;
using DealerApp.Core.Common;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.Entities
{
    public partial class Cliente : BaseEntity
    {
        public Cliente()
        {
            Contrato = new HashSet<Contrato>();
        }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public SexoEnum Sexo { get; set; }
        public string Nacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Foto { get; set; }
        public bool? Estatus { get; set; }
        public int IdSangre { get; set; }
        public RolType IdRol { get; set; }
        public virtual SangreCliente IdSangreNavigation { get; set; }
        public virtual ICollection<Contrato> Contrato { get; set; }

    }
}
