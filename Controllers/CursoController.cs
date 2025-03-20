using backendPFPU.Models;
using backendPFPU.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backendPFPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ILogger<CursoController> _logger;
        private ICursoRepository _cursoRepository;

        public CursoController(ILogger<CursoController> logger, ICursoRepository cursoRepository)
        {
            _logger = logger;
            _cursoRepository = cursoRepository;
        }

        [HttpGet]
        [Route("/GetAllCursos")]
        public IActionResult GetAllCursos()
        {
            var cursos = _cursoRepository.GetAll();
            return Ok(cursos);
        }

        [HttpGet]
        [Route("/GetCurso/{id}")]
        public IActionResult GetCurso(int id)
        {
            var curso = _cursoRepository.GetCurso(id);
            if (curso == null)
            {
                return NotFound(new { msg = "No se encontró el curso." });
            }
            return Ok(curso);
        }

        [HttpPost]
        [Route("/PostCurso")]
        public IActionResult PostCurso(Curso curso)
        {
            if (curso == null)
            {
                return BadRequest(new { msg = "El curso proporcionado es inválido." });
            }
            _cursoRepository.AddCurso(curso);
            return Ok(new
            {
                msg = "El curso se guardó con éxito",
            });
        }

        [HttpPut]
        [Route("/PutCurso")]
        public IActionResult PutCurso(Curso curso)
        {
            if (curso == null)
            {
                return BadRequest(new { msg = "El curso proporcionado es inválido." });
            }
            _cursoRepository.UpdateCurso(curso);
            return Ok(new
            {
                msg = "El curso se actualizó con éxito",
            });
        }

        [HttpDelete]
        [Route("/DeleteCurso/{id}")]
        public IActionResult DeleteCurso(int id)
        {
            _cursoRepository.DeleteCurso(id);
            return Ok(new
            {
                msg = "El curso se eliminó con éxito",
            });
        }

    }
}
