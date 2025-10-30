namespace Phanteon.Models
{
    public class Dispositivo
    {
        public int IdDispositivo { get; set; }
        public string SerialDispositivo { get; set; } = null!;
        public string MAC { get; set; } = null!;
        public string Firmware { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime Registro { get; set; }
        public string Activo { get; set; } = null!;
        public DateTime UltimaVista { get; set; }
    }
}
