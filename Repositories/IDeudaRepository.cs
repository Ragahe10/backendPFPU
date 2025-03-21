using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface IDeudaRepository
    {
        public void AddDeuda(Deuda deuda);
        public void DeleteDeuda(int id);
        public List<Deuda> GetAllDeuda();
        public Deuda GetDeuda(int id);
        public void UpdateDeuda(Deuda deuda);

        public List<Deuda> GetDeudasByAlumno(int id);
        public List<Deuda> ObtenerDeudasVencidas();

        public int GetCantidadAlumnosConDeuda();

        public Deuda GetResumenDeudaByAlumno(int id);
    }
}
