namespace backendPFPU.Models
{
    public class ActividadRecienteAdmin
    {
        public string Tipo { get; set; }       // Puede ser "nota", "asistencia" o "pago"
        public DateTime Fecha { get; set; }    // Fecha de la actividad
        public string Descripcion { get; set; } // Texto con los detalles de la actividad
    }

}
