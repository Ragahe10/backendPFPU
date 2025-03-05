using backendPFPU.Models;
using backendPFPU.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagoController : ControllerBase
    {
        private readonly ITipoPagoRepository _tipoPagoRepository;

        public TipoPagoController(ITipoPagoRepository tipoPagoRepository)
        {
            _tipoPagoRepository = tipoPagoRepository;
        }

        [HttpGet]
        [Route("/GetAllTipoPago")]
        public IActionResult GetAllTipoPago()
        {
            var tipoPagos = _tipoPagoRepository.GetAllTipoPago();
            return Ok(tipoPagos);
        }

        [HttpGet]
        [Route("/GetTipoPago/{id}")]
        public IActionResult GetTipoPago(int id)
        {
            var tipoPago = _tipoPagoRepository.GetTipoPago(id);
            if (tipoPago == null)
            {
                return NotFound(new { msg = "No se encontró el tipo de pago." });
            }
            return Ok(tipoPago);
        }

        [HttpPost]
        [Route("/AddTipoPago")]
        public IActionResult AddTipoPago(TipoPago tipoPago)
        {
            if (tipoPago == null)
            {
                return BadRequest(new { msg = "El tipo de pago proporcionado es inválido." });
            }
            _tipoPagoRepository.AddTipoPago(tipoPago);
            return Ok(new
            {
                msg = "El tipo de pago se guardó con éxito",
            });
        }

        [HttpPut]
        [Route("/PutTipoPago")]
        public IActionResult PutTipoPago(TipoPago tipoPago)
        {
            if (tipoPago == null)
            {
                return BadRequest(new { msg = "El tipo de pago proporcionado es inválido." });
            }
            _tipoPagoRepository.UpdateTipoPago(tipoPago);
            return Ok(new
            {
                msg = "El tipo de pago se actualizó con éxito",
            });
        }

        [HttpDelete]
        [Route("/DeleteTipoPago/{id}")]
        public IActionResult DeleteTipoPago(int id)
        {
            var tipoPago = _tipoPagoRepository.GetTipoPago(id);
            if (tipoPago == null)
            {
                return NotFound(new { msg = "No se encontró el tipo de pago." });
            }
            _tipoPagoRepository.DeleteTipoPago(id);
            return Ok(new
            {
                msg = "El tipo de pago se eliminó con éxito",
            });
        }

    }
}
