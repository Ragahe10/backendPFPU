namespace backendPFPU.Models
{
    public class GraficoAsistenciaTotalALumno
    {
        public string[] labels { get; set; }
        public int[] data { get; set; }
        public string[] backgroundColor { get; set; } = ["#22c55e", "#eab308", "#ef4444"];
    }
}
