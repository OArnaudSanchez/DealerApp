using System.Collections.Generic;
using DealerApp.Core.Common;

namespace DealerApp.Core.Entities
{
    public partial class Vehiculo : BaseEntity
    {
        public Vehiculo()
        {
            Contrato = new HashSet<Contrato>();
        }

        public string Matricula { get; set; }
        public string Placa { get; set; }
        public string Foto { get; set; }
        public decimal Precio { get; set; }
        public string Puertas { get; set; }
        public string Lanzamiento { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int IdCombustible { get; set; }
        public int IdColor { get; set; }

        public virtual Color IdColorNavigation { get; set; }
        public virtual Combustible IdCombustibleNavigation { get; set; }
        public virtual Marca IdMarcaNavigation { get; set; }
        public virtual Modelo IdModeloNavigation { get; set; }
        public virtual ICollection<Contrato> Contrato { get; set; }
    }
}
