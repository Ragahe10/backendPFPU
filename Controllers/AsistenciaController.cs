using backendPFPU.Models;
using backendPFPU.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaRepository _asistenciaRepository;

        public AsistenciaController(IAsistenciaRepository asistenciaRepository)
        {
            _asistenciaRepository = asistenciaRepository;
        }

        [HttpPost]
        [Route("/CreateAsistencia")]
        public IActionResult CreateAsistencia(Asistencia asistencia)
        {
            try
            {
                _asistenciaRepository.AddAsistencia(asistencia);
                return Ok(new { msg = "Asistencia creada con éxito" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo crear la asistencia" });
            }
        }

        [HttpPut]
        [Route("/UpdateAsistencia")]
        public IActionResult UpdateAsistencia(Asistencia asistencia)
        {
            try
            {
                _asistenciaRepository.UpdateAsistencia(asistencia);
                return Ok(new { msg = "Asistencia actualizada con éxito" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo actualizar la asistencia" });
            }
        }

        [HttpDelete]
        [Route("/DeleteAsistencia/{id_asistencia}/{id_alumno}/{id_materia}/{fecha}")]
        public IActionResult DeleteAsistencia(int id_asistencia, int id_alumno, int id_materia, string fecha)
        {
            try
            {
                _asistenciaRepository.DeleteAsistencia(id_asistencia, id_alumno, id_materia, fecha);
                return Ok(new { msg = "Asistencia eliminada con éxito" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo eliminar la asistencia" });
            }
        }

        [HttpGet]
        [Route("/GetAsistencia/{id_asistencia}/{id_alumno}/{id_materia}/{fecha}")]
        public IActionResult GetAsistencia(int id_asistencia, int id_alumno, int id_materia, string fecha)
        {
            try
            {
                var asistencia = _asistenciaRepository.GetAsistencia(id_asistencia, id_alumno, id_materia, fecha);
                return Ok(asistencia);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo obtener la asistencia" });
            }
        }

        [HttpGet]
        [Route("/GetAllAsistencias")]
        public IActionResult GetAllAsistencias()
        {
            try
            {
                var asistencias = _asistenciaRepository.GetAsistencias();
                return Ok(asistencias);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudieron obtener las asistencias" });
            }
        }

        [HttpGet]
        [Route("/GetPorcentajeAsistenciasByAlumno/{id_alumno}")]
        public IActionResult GetPorcentajeAsistenciasByAlumno(int id_alumno)
        {
            try
            {
                var porcentaje = _asistenciaRepository.GetPorcentajeAsistenciasByAlumno(id_alumno);
                return Ok(porcentaje);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo obtener el porcentaje de asistencias" });
            }
        }

        [HttpGet]
        [Route("/GetPorcentajeAsistenciasByMateriaAlumno/{id_materia}/{id_alumno}")]
        public IActionResult GetPorcentajeAsistenciasByMateriaAlumno(int id_materia, int id_alumno)
        {
            try
            {
                var porcentaje = _asistenciaRepository.GetPorcentajeAsistenciasByMateriaAlumno(id_materia, id_alumno);
                return Ok(porcentaje);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo obtener el porcentaje de asistencias" });
            }
        }

        [HttpGet]
        [Route("/GetPresentesByMateriaAlumno/{id_materia}/{id_alumno}")]
        public IActionResult GetPresentesByMateriaAlumno(int id_materia, int id_alumno)
        {
            try
            {
                var presentes = _asistenciaRepository.GetPresentesByMateriaAlumno(id_materia, id_alumno);
                return Ok(presentes);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo obtener la cantidad de presentes" });
            }
        }

        [HttpGet]
        [Route("/GetAusentesByMateriaAlumno/{id_materia}/{id_alumno}")]
        public IActionResult GetAusentesByMateriaAlumno(int id_materia, int id_alumno)
        {
            try
            {
                var ausentes = _asistenciaRepository.GetAusentesByMateriaAlumno(id_materia, id_alumno);
                return Ok(ausentes);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo obtener la cantidad de ausentes" });
            }
        }

        [HttpGet]
        [Route("/GetTardesByMateriaAlumno/{id_materia}/{id_alumno}")]
        public IActionResult GetTardesByMateriaAlumno(int id_materia, int id_alumno)
        {
            try
            {
                var tardes = _asistenciaRepository.GetTardesByMateriaAlumno(id_materia, id_alumno);
                return Ok(tardes);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo obtener la cantidad de tardes" });
            }
        }
    }
}
