namespace Phanteon.Models
{
    public class HistorialDispositivo
    {
        public int IdHistorialDispositivo { get; set; }
        public DateTime MarcaTiempo { get; set; }
        public DateTime HoraAlmacenado { get; set; }
        public double ValorProximidad { get; set; }
        public bool MovimientoDetectado { get; set; }
        public string Estado { get; set; } = null!;
        public int IdDispositivo { get; set; }
    }
}
