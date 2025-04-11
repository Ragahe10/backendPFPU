namespace backendPFPU.Models
{
    public class EstadisticasEstudiante
    {
        public int cantidadMaterias { get; set; }
        public int porcentajeAsistencia { get; set; }

        public float promedioNotas { get; set; }
        public int cantidadDeudas { get; set; }
        public int cantidadMateriasAprobadas { get; set; }
        public int cantidadMateriasDesaprobadas { get; set; }
    }
}
