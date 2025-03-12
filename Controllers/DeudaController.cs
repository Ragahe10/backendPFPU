using backendPFPU.Models;
using backendPFPU.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeudaController : ControllerBase
    {
        private readonly IDeudaRepository _deudaRepository;

        public DeudaController(IDeudaRepository deudaRepository)
        {
            _deudaRepository = deudaRepository;
        }

        [HttpPost]
        [Route("/AddDeuda")]
        public IActionResult AddDeuda(Deuda deuda)
        {
            try
            {
                _deudaRepository.AddDeuda(deuda);
                return Ok(new
                {
                    msg = "La deuda se guardó con éxito",
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    msg = "Ocurrió un error al guardar la deuda",
                    error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("/DeleteDeuda/{id}")]
        public IActionResult DeleteDeuda(int id)
        {
            try
            {
                _deudaRepository.DeleteDeuda(id);
                return Ok(new
                {
                    msg = "La deuda se eliminó con éxito",
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    msg = "Ocurrió un error al eliminar la deuda",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("/GetAllDeuda")]
        public IActionResult GetAllDeuda()
        {
            try
            {
                var deudas = _deudaRepository.GetAllDeuda();
                return Ok(deudas);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    msg = "Ocurrió un error al obtener las deudas",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("/GetDeuda/{id}")]
        public IActionResult GetDeuda(int id)
        {
            try
            {
                var deuda = _deudaRepository.GetDeuda(id);
                if (deuda.id_deuda == 0)
                {
                    return NotFound(new { msg = "No se encontró la deuda." });
                }
                return Ok(deuda);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    msg = "Ocurrió un error al obtener la deuda",
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("/UpdateDeuda")]
        public IActionResult UpdateDeuda(Deuda deuda)
        {
            try
            {
                _deudaRepository.UpdateDeuda(deuda);
                return Ok(new
                {
                    msg = "La deuda se actualizó con éxito",
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    msg = "Ocurrió un error al actualizar la deuda",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("/GetDeudasAlumno/{id}")]
        public IActionResult GetDeudasAlumno(int id)
        {
            try
            {
                var deudas = _deudaRepository.GetDeudasByAlumno(id);
                
                return Ok(deudas);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    msg = "Ocurrió un error al obtener las deudas del alumno",
                    error = ex.Message
                });
            }
        }
    }
}
