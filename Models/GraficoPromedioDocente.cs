namespace backendPFPU.Models
{
    public class GraficoPromedioDocente
    {
        public string[] labels { get; set; }
        public float[] data { get; set; }   
        public string label { get; } = "Promedio de Notas de sus alumnos por Trimestre";

    }
}
