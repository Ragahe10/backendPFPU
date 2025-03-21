namespace backendPFPU.Models
{
    public class GraficoAsistenciaAdmin
    {
        public string[] labels { get; set; }
        public float[] data { get; set; }

        private const string titulo = "Promedio de Asistencia por Año";
        public string label { get; } = titulo;

    }
}
