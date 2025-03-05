using backendPFPU.Models;
using backendPFPU.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        [HttpGet]
        [Route("/GetAllPago")]
        public IActionResult GetAllPago()
        {
            var pagos = _pagoRepository.GetAllPago();
            return Ok(pagos);
        }

        [HttpGet]
        [Route("/GetPago/{id}")]
        public IActionResult GetPago(int id)
        {
            var pago = _pagoRepository.GetPago(id);
            if (pago.id_pago == 0)
            {
                return NotFound(new { msg = "No se encontró el pago." });
            }
            return Ok(pago);
        }
        [HttpGet]
        [Route("/GetPagosAlumno/{id}")]
        public IActionResult GetPagosAlumno(int id)
        {
            var pagos = _pagoRepository.GetPagosByAlumno(id);
            if (pagos == null)
            {
                return NotFound(new { msg = "No se encontraron pagos para el alumno." });
            }
            return Ok(pagos);
        }

        [HttpPost]
        [Route("/AddPago")]
        public IActionResult AddPago(Pago pago)
        {
            if (pago == null)
            {
                return BadRequest(new { msg = "El pago proporcionado es inválido." });
            }
            _pagoRepository.AddPago(pago);
            return Ok(new
            {
                msg = "El pago se guardó con éxito",
            });
        }

        [HttpPut]
        [Route("/UpdatePago")]
        public IActionResult UpdatePago(Pago pago)
        {
            if (pago == null)
            {
                return BadRequest(new { msg = "El pago proporcionado es inválido." });
            }
            _pagoRepository.UpdatePago(pago);
            return Ok(new
            {
                msg = "El pago se actualizó con éxito",
            });
        }

        [HttpDelete]
        [Route("/DeletePago/{id}")]
        public IActionResult DeletePago(int id)
        {
            var pago = _pagoRepository.GetPago(id);
            if (pago == null)
            {
                return NotFound(new { msg = "No se encontró el pago." });
            }
            _pagoRepository.DeletePago(id);
            return Ok(new
            {
                msg = "El pago se eliminó con éxito",
            });
        }
    }
}
