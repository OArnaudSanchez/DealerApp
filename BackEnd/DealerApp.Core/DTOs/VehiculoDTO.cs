using DealerApp.Core.Common;

namespace DealerApp.Core.DTOs
{
    public class VehiculoDTO : ImageBase
    {
        public int? Id { get; set; }
        public string Matricula { get; set; }
        public string Placa { get; set; }
        public decimal Precio { get; set; }
        public string Puertas { get; set; }
        public string Lanzamiento { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int IdCombustible { get; set; }
        public int IdColor { get; set; }
    }
}