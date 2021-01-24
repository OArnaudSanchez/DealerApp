using DealerApp.Core.Common;

namespace DealerApp.Core.DTOs
{
    public class MarcaDTO : ImageBase
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Lanzamiento { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}