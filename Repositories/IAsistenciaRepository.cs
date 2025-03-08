using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface IAsistenciaRepository
    {
        List<Asistencia> GetAsistencias();
        Asistencia GetAsistencia(int id, int id_alumno, int id_materia, string fecha);
        void AddAsistencia(Asistencia item);
        public void DeleteAsistencia(int id, int id_alumno, int id_materia, string fecha);
        void UpdateAsistencia(Asistencia item);
    }
}
