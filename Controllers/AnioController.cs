using backendPFPU.Models;
using backendPFPU.Repositories;
using backendPFPU.Respositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace backendPFPU.Controllers;

[ApiController]
[Route("[controller]")]
public class AnioController : ControllerBase
{
     private readonly ILogger<AnioController> _logger;
    private IAnioRepository _anioRepository;

    public AnioController(ILogger<AnioController> logger, IAnioRepository anioRepository)
    {
        _logger = logger;
        _anioRepository = anioRepository;
    }

    [HttpGet]
    [Route("/GetAllAnios")]
    public IActionResult GetAllAnios()
    {
        var anios = _anioRepository.GetAll();
        return Ok(anios);
    }

    [HttpGet]
    [Route("/GetAnio/{id}")]
    public IActionResult GetAnio(int id)
    {
        var anio = _anioRepository.GetById(id);
        if (anio == null)
        {
            return NotFound(new { msg = "No se encontró el año." });
        }
        return Ok(anio);
    }

    [HttpPost]
    [Route("/PostAnio")]
    public IActionResult PostAnio(Anio anio)
    {
        if (anio == null)
        {
            return BadRequest(new { msg = "El año proporcionado es inválido." });
        }
        _anioRepository.PostAnio(anio);
        return Ok(new
        {
            msg = "El año se guardó con éxito",
        });
    }

    [HttpPut]
    [Route("/PutAnio")]
    public IActionResult PutAnio(Anio anio)
    {
        if (anio == null)
        {
            return BadRequest(new { msg = "El año proporcionado es inválido." });
        }
        _anioRepository.PutAnio(anio);
        return Ok(new
        {
            msg = "El año se actualizó con éxito",
        });
    }

    [HttpDelete]
    [Route("/DeleteAnio/{id}")]
    public IActionResult DeleteAnio(int id)
    {
        _anioRepository.DeleteAnio(id);
        return Ok(new
        {
            msg = "El año se eliminó con éxito",
        });
    }


}
