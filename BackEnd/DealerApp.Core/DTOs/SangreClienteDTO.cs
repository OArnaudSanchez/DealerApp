namespace DealerApp.Core.DTOs
{
    public class SangreClienteDTO
    {
        public int? Id { get; set; }
        public string TipoSangre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
    }
}