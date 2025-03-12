namespace backendPFPU.Models
{
    public class Deuda
    {
        public int id_deuda { get; set; }
        public string fecha_vencimiento { get; set; }
        public int id_alumno { get; set; }
        public string estado { get; set; }
        public float monto { get; set; }

        public int id_tipo { get; set; }
    }
}
