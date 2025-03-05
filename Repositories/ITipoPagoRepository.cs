using backendPFPU.Models;

namespace backendPFPU.Repositories
{
    public interface ITipoPagoRepository
    {
        List<TipoPago> GetAllTipoPago();
        TipoPago GetTipoPago(int id);
        void AddTipoPago(TipoPago tipoPago);
        void UpdateTipoPago(TipoPago tipoPago);
        void DeleteTipoPago(int id);
    }
}
