using backendPFPU.Models;
using backendPFPU.Repositories;
using backendPFPU.Respositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace backendPFPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaRepository _materiaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public MateriaController(IMateriaRepository materiaRepository, IUsuarioRepository usuarioRepository)
        {
            _materiaRepository = materiaRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("/AddMateria")]
        public IActionResult AddMateria(Materia materia)
        {
            try
            {
                if (materia == null)
                    return BadRequest("Los datos de la materia son inválidos.");
                if (materia.Id_docente != null && _usuarioRepository.GetDocente((int)materia.Id_docente).Id_usuario == 0)
                    return NotFound($"No se encontró un docente con ID {materia.Id_docente}.");

                _materiaRepository.AddMateria(materia);
                return CreatedAtAction(nameof(GetMateria), new { id = materia.Id_materia }, materia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar materia: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("/DeleteMateria/{id}")]
        public IActionResult DeleteMateria(int id)
        {
            try
            {
                var materia = _materiaRepository.GetMateria(id);
                if (materia == null)
                    return NotFound($"No se encontró una materia con ID {id}.");

                _materiaRepository.DeleteMateria(id);
                return Ok("Materia eliminada con exito!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar materia: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("/GetMateria")]
        public IActionResult GetMateria(int id)
        {
            try
            {
                var materia = _materiaRepository.GetMateria(id);
                if (materia == null)
                    return NotFound($"No se encontró una materia con ID {id}.");

                return Ok(materia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener materia: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("/GetMaterias")]
        public IActionResult GetMaterias()
        {
            try
            {
                var materias = _materiaRepository.GetMaterias();
                return Ok(materias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las materias: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("/UpdateMateria")]
        public IActionResult UpdateMateria(Materia materia)
        {
            try
            {
                if (materia == null)
                    return BadRequest("Datos de materia inválidos.");

                var existingMateria = _materiaRepository.GetMateria(materia.Id_materia);
                if (existingMateria == null)
                    return NotFound($"No se encontró una materia con ID {materia.materia}.");

                _materiaRepository.UpdateMateria(materia);
                return Ok("Materia actualizada con exito!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar materia: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("/GetMateriasByAnio")]
        public IActionResult GetMateriasByAnio(int id_anio)
        {
            try
            {
                var materias = _materiaRepository.GetMateriasByAnio(id_anio);
               
                return Ok(materias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener materias: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("/GetMateriasByDocente")]
        public IActionResult GetMateriasByDocente(int id_docente)
        {
            try
            {
                var materias = _materiaRepository.GetMateriasByDocente(id_docente);
   
                return Ok(materias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener materias: {ex.Message}");
            }
        }
    }
}
