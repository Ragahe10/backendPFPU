namespace backendPFPU.Models
{
    public class GraficoAsistenciasAlumnoByDocente
    {
        public string[] labels { get; set; }
        public int[] data { get; set; }
        public string[] backgroundColor { get; set; } = ["#ef4444", "#22c55e" , "#eab308"];
    }
}
