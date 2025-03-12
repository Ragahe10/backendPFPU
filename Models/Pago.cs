namespace backendPFPU.Models
{
    public class Pago
    {
        public int id_pago { get; set; }
        public float monto { get; set; }
        public string fecha { get; set; }
    
        public int id_alumno { get; set; }

        public int id_deuda { get; set; }
    }
}
