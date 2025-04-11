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

        List<Deuda> GetDeudasPendientesByAlumno(int id_alumno);
        public List<Deuda> ObtenerDeudasVencidas();

        public int GetCantidadAlumnosConDeuda();

        public GraficoEstadosDePagoAdmin GetGraficoEstadosDePagoAdmin();

        public List<ActividadRecienteAdmin> GetUltimasActividades();

        public List<ActividadRecienteDocente> GetUltimasActividadesDocente(int id);

        public List<ActividadRecienteAlumno> GetUltimasActividadesAlumno(int id);

        public Deuda GetResumenDeudaByAlumno(int id);
    }
}
