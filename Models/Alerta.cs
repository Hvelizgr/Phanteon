namespace Phanteon.Models
{
    /// <summary>
    /// Modelo de alerta del sistema
    /// Representa eventos generados por los dispositivos IoT
    /// </summary>
    public class Alerta
    {
        public int IdAlert { get; set; }
        public string TipoEvento { get; set; } = null!;
        public DateTime MarcaTiempo { get; set; }
        public DateTime HoraAlmacenado { get; set; }
        public bool Procesado { get; set; }
        public string Severidad { get; set; } = null!; // "Crítica", "Advertencia", "Info"
        public string CargaUtil { get; set; } = null!;
        public int IdDispositivo { get; set; }
    }
}
