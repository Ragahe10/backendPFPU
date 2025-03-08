namespace backendPFPU.Models
{
    public class Asistencia
    {
        public int id_asistencia { get; set; }
        public int id_alumno { get; set; }
        public int id_materia { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
    }
}
