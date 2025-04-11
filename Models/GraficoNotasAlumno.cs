namespace backendPFPU.Models
{
    public class GraficoNotasAlumno
    {

        public string[] labels { get; set; }
        public float[] data { get; set; }

        private const string titulo = "Promedio Notas Por Materia";
        public string label { get; } = titulo;
    }
}
