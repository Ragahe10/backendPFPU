using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface ICursoRepository
    {
        List<Curso> GetAll();
        Curso GetCurso(int id);
        void AddCurso(Curso curso);
        void UpdateCurso(Curso curso);
        void DeleteCurso(int id);

 
    }
}
