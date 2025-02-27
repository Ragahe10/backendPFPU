using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface IAnioRepository
    {
        List<Anio> GetAll();
        Anio GetById(int id);
        void PostAnio(Anio anio);
        void PutAnio(Anio anio);
        void DeleteAnio(int id);
    }
}
