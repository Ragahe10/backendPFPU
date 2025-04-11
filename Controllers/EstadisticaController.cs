using backendPFPU.Models;
using backendPFPU.Repositories;
using backendPFPU.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
         private readonly ICursoRepository _cursoRepository;
       private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IDeudaRepository _deudaRepository;
        private readonly INotaRepository _notaRepository;

        public EstadisticaController(IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository, IMateriaRepository materiaRepository, IAsistenciaRepository asistenciaRepository, IDeudaRepository deudaRepository, INotaRepository notaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _cursoRepository = cursoRepository;
            _asistenciaRepository = asistenciaRepository;
            _materiaRepository = materiaRepository;
            _deudaRepository = deudaRepository;
            _notaRepository = notaRepository;
        }

        [HttpGet]
        [Route("/estadisticasAdmin")]
        public IActionResult GetEstadisticasAdmin()
        {
            var statsAdmin = new EstadisticasAdmin();
            statsAdmin.cantidadAlumnos = _usuarioRepository.GetAlumnos().Count;
            statsAdmin.cantidadDocentes = _usuarioRepository.GetDocentes().Count;
            statsAdmin.cantidadMaterias = _materiaRepository.GetMaterias().Count;
            statsAdmin.cantidadCursos = _cursoRepository.GetAll().Count;
            statsAdmin.PorcentajeAsistenciasGlobal = _asistenciaRepository.GetPorcentajeAsistenciasGlobal();
            statsAdmin.AlumnosConDeuda = _deudaRepository.GetCantidadAlumnosConDeuda();

            return Ok(statsAdmin);
        }

        [HttpGet]
        [Route("/estadisticasDocente/{id}")]
        public IActionResult GetEstadisticasDocente(int id)
        {
            var statsDocente = new EstadisticasDocente();
            statsDocente.cantidadAlumnos = _usuarioRepository.GetAlumnosByDocente(id).Count;
            statsDocente.cantidadMaterias = _materiaRepository.GetMateriasByDocente(id).Count;
            statsDocente.porcentajeAsistenciaPromedio = _asistenciaRepository.GetPorcentajeAsistenciasByDocente(id);

            return Ok(statsDocente);
        }

        [HttpGet]
        [Route("/estadisticasAlumno/{id}")]
        public IActionResult GetEstadisticasAlumno(int id)
        {
            var statsAlumno = new EstadisticasEstudiante();
          
            statsAlumno.cantidadMaterias = _materiaRepository.GetCantidadMateriasByAlumno(id);
            statsAlumno.porcentajeAsistencia = _asistenciaRepository.GetPorcentajeAsistenciasByAlumno(id);
            statsAlumno.promedioNotas = _notaRepository.GetPromedioByAlumno(id);
            statsAlumno.cantidadDeudas = _deudaRepository.GetDeudasPendientesByAlumno(id).Count;
            statsAlumno.cantidadMateriasAprobadas = _materiaRepository.GetCantidadMateriasAprobadasByAlumno(id);
            statsAlumno.cantidadMateriasDesaprobadas = _materiaRepository.GetCantidadMateriasDesaprobadasByAlumno(id);
            return Ok(statsAlumno);
        }

        [HttpGet]
        [Route("/graficosAdmin/asistencia")]
        public IActionResult GetGraficoAsistenciaAdmin()
        {
            return Ok(_asistenciaRepository.GetGraficoAsistenciaAdmin());
        }

        [HttpGet]
        [Route("/graficosAdmin/deuda")]
        public IActionResult GetGraficoDeudaAdmin()
        {
            return Ok(_deudaRepository.GetGraficoEstadosDePagoAdmin());
        }

        [HttpGet]
        [Route("/actividadRecienteAdmin")]
        public IActionResult GetActividadRecienteAdmin()
        {
            return Ok(_deudaRepository.GetUltimasActividades());
        }

        [HttpGet]
        [Route("/graficoPromedioDocente/{id}")]
        public IActionResult GetGraficoPromedioDocente(int id)
        {
            return Ok(_notaRepository.GetGraficoPromedioDocente(id));
        }

        [HttpGet]
        [Route("/graficoAsistenciasAlumnoByDocente/{id}")]
        public IActionResult GetGraficoAsistenciasAlumnoByDocente(int id)
        {
            return Ok(_asistenciaRepository.GetGraficoAsistenciasAlumnoByDocente(id));
        }

        [HttpGet]
        [Route("/actividadRecienteDocente/{id}")]
        public IActionResult GetActividadRecienteDocente(int id)
        {
            return Ok(_deudaRepository.GetUltimasActividadesDocente(id));
        }

        [HttpGet]
        [Route("/graficoNotasAlumno/{id}")]
        public IActionResult GetGraficoNotasAlumno(int id)
        {
            return Ok(_notaRepository.GetGraficoNotasAlumno(id));
        }

        [HttpGet]
        [Route("/graficoAsistenciaTotalAlumno/{id}")]
        public IActionResult GetGraficoAsistenciaTotalAlumno(int id)
        {
            return Ok(_asistenciaRepository.GetGraficoAsistenciaTotalALumno(id));
        }

        [HttpGet]
        [Route("/actividadRecienteAlumno/{id}")]
        public IActionResult GetActividadRecienteAlumno(int id)
        {
            return Ok(_deudaRepository.GetUltimasActividadesAlumno(id));
        }
    }
}
