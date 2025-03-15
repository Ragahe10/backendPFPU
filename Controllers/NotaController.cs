using backendPFPU.Models;
using backendPFPU.Repositories;
using backendPFPU.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotaController : ControllerBase
    {
        private readonly INotaRepository _notaRepository;

        private readonly EmailService _emailService;

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMateriaRepository _materiaRepository;

        public NotaController(INotaRepository notaRepository, EmailService emailService, IUsuarioRepository usuarioRepository, IMateriaRepository materiaRepository)
        {
            _notaRepository = notaRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;
            _materiaRepository = materiaRepository;
        }

       
      

        [HttpPost]
        [Route("/CreateNota")]
        public async Task<IActionResult> CreateNota(Nota nota)
        {
            try
            {
                var usuario = _usuarioRepository.GetAlumno(nota.id_alumno);
                var materia = _materiaRepository.GetMateria(nota.id_materia);

                _notaRepository.CreateNota(nota);
                await _emailService.EnviarCorreoNotaActualizada(
                   usuario.Correo,
                   $"{usuario.Nombre} {usuario.Apellido}",
                   materia.materia,
                   nota.nota,
                   DateTime.Now.ToString("dd/MM/yyyy"),
                   nota.trimestre
               );
                return Ok(new { msg = "Nota creada con éxito y Alumno notificado por mail" });
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo crear la nota" });
            }
        }

        [HttpPut]
        [Route("/UpdateNota")]
        public async Task<IActionResult> UpdateNota(Nota nota)
        {
            try
            {
                var usuario = _usuarioRepository.GetAlumno(nota.id_alumno);
                var materia = _materiaRepository.GetMateria(nota.id_materia);

                _notaRepository.UpdateNota(nota);
                await _emailService.EnviarCorreoNotaActualizada(
                  usuario.Correo,
                  $"{usuario.Nombre} {usuario.Apellido}",
                  materia.materia,
                  nota.nota,
                  DateTime.Now.ToString("dd/MM/yyyy"),
                    nota.trimestre
              );
                return Ok(new { msg = "Nota Actualizada con éxito y Alumno notificado por mail" });
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

        [HttpGet]
        [Route("/GetPromedioByAlumno/{id_alumno}")]
        public IActionResult GetPromedioByAlumno(int id_alumno)
        {
            try
            {
                var promedio = _notaRepository.GetPromedioByAlumno(id_alumno);
                
                return Ok(promedio);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo calcular el promedio" });
            }
        }

        [HttpGet]
        [Route("/GetPromedioByMateriaAlumno/{id_materia}/{id_alumno}")]
        public IActionResult GetPromedioByMateriaAlumno(int id_materia, int id_alumno)
        {
            try
            {
                var promedio = _notaRepository.GetPromedioByMateriaAlumno(id_materia, id_alumno);
                return Ok(promedio);
            }
            catch
            {
                return BadRequest(new { msg = "No se pudo calcular el promedio" });
            }
        }

        [HttpGet]
        [Route("/GetNotasByMateriaAlumno/{id_materia}/{id_alumno}")]
        public IActionResult GetNotasByMateriaAlumno(int id_materia, int id_alumno)
        {
            try
            {
                var notas = _notaRepository.GetNotasByMateriaAlumno(id_materia, id_alumno);
                return Ok(notas);
            }
            catch
            {
                return BadRequest(new { msg = "No se encontraron notas para la materia y el alumno" });
            }
        }

     
    }
}
