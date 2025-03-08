using backendPFPU.Models;
using backendPFPU.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotaController : ControllerBase
    {
        private readonly INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        [HttpPost]
        [Route("/CreateNota")]
        public IActionResult CreateNota(Nota nota)
        {
            try
            {
                _notaRepository.CreateNota(nota);
                return Ok(new { msg = "Nota creada con éxito" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo crear la nota" });
            }
        }

        [HttpPut]
        [Route("/UpdateNota")]
        public IActionResult UpdateNota(Nota nota)
        {
            try
            {
                _notaRepository.UpdateNota(nota);
                return Ok(new { msg = "Nota actualizada con éxito" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo actualizar la nota" });
            }
        }

        [HttpDelete]
        [Route("/DeleteNota/{id_nota}/{id_alumno}/{id_materia}/{fecha}")]
        public IActionResult DeleteNota(int id_nota, int id_alumno, int id_materia, string fecha)
        {
            try
            {
                _notaRepository.DeleteNota(id_nota, id_alumno, id_materia, fecha);
                return Ok(new { msg = "Nota eliminada con éxito" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo eliminar la nota" });
            }
        }

        [HttpGet]
        [Route("/GetNotas")]
        public IActionResult GetNotas()
        {
            var notas = _notaRepository.GetNotas();
            return Ok(notas);
        }

        [HttpGet]
        [Route("/GetNota/{id_nota}/{id_alumno}/{id_materia}/{fecha}")]
        public IActionResult GetNota(int id_nota, int id_alumno, int id_materia, string fecha)
        {
            var nota = _notaRepository.GetNota(id_nota, id_alumno, id_materia, fecha);
            if (nota.id_nota == 0)
            {
                return NotFound(new { msg = "No se encontró la nota" });
            }
            return Ok(nota);
        }

        [HttpGet]
        [Route("/GetNotasByAlumno/{id_alumno}")]
        public IActionResult GetNotasByAlumno(int id_alumno)
        {
            try
            {
                var notas = _notaRepository.GetNotasByAlumno(id_alumno);
                return Ok(notas);
            }
            catch
            {
                return BadRequest(new { msg = "No se encontraron notas para el alumno" });
            }
        }

        [HttpGet]
        [Route("/GetNotasByMateria/{id_materia}")]
        public IActionResult GetNotasByMateria(int id_materia)
        {
            try
            {
                var notas = _notaRepository.GetNotasByMateria(id_materia);
                return Ok(notas);
            }
            catch
            {
                return BadRequest(new { msg = "No se encontraron notas para la materia" });
            }
        }
    }
}
