using backendPFPU.Models;
using backendPFPU.Respositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace backendPFPU.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    [Route("/PostProfesor")]
    public IActionResult PostProfesor(Profesor usuario)
    {
        if (usuario == null)
        {
            return BadRequest(new { msg = "El Profesor proporcionado es inválido." });
        }

        // Guardar el usuario utilizando el repositorio
        _usuarioRepository.PostProfesor(usuario);

        return Ok(new
        {
            msg = "El Profesor se guardó con éxito",
            usuario
        });
    }
}
