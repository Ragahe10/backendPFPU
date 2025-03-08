namespace backendPFPU.Models
{
    public class Nota
    {
        public int id_nota { get; set; }
        public int id_alumno { get; set; }
        public int id_materia { get; set; }
        public string fecha { get; set; }
        public float nota { get; set; }
        public string descripcion { get; set; }
        public int trimestre { get; set; }

    }
}
