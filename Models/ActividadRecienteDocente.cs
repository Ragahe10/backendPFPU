namespace backendPFPU.Models
{
    public class ActividadRecienteDocente
    {
        public string Tipo { get; set; }       // Puede ser "nota" o "asistencia" 
        public DateTime Fecha { get; set; }    // Fecha de la actividad
        public string Descripcion { get; set; } // Texto con los detalles de la actividad
    }
}
