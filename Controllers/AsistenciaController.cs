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
    }
}
