using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface IPagoRepository
    {
        List<Pago> GetAllPago();
        Pago GetPago(int id);
        void AddPago(Pago pago);
        void UpdatePago(Pago pago);
        void DeletePago(int id);
        List<Pago> GetPagosByAlumno(int id);
    }
}
