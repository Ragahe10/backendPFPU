using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface INotaRepository
    {
        List<Nota> GetNotas();
        Nota GetNota(int id_nota, int id_alumno, int id_materia, string fecha);
        void CreateNota(Nota nota);
        void UpdateNota(Nota nota);
        void DeleteNota(int id_nota, int id_alumno, int id_materia, string fecha);

        List<Nota> GetNotasByAlumno(int id_alumno);
        List<Nota> GetNotasByMateria(int id_materia);

        List<Nota> GetNotasByMateriaAlumno(int id_materia, int id_alumno);

        float GetPromedioByAlumno(int id_alumno);

        GraficoPromedioDocente GetGraficoPromedioDocente(int id_docente); //Trae el promedio de notas de los alumnos de un docente

        GraficoNotasAlumno GetGraficoNotasAlumno(int id_alumno); //Trae el promedio de notas de un alumno de distintas materias

        float GetPromedioByMateriaAlumno(int id_materia, int id_alumno);
    }
}
