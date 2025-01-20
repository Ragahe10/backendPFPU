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

    [HttpPost(Name = "PostUsuario")]
    public IActionResult PostUsuario([FromBody] Usuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest(new { msg = "El usuario proporcionado es inválido." });
        }

        // Guardar el usuario utilizando el repositorio
        _usuarioRepository.PostUsuario(usuario);

        return Ok(new
        {
            msg = "El usuario se guardó con éxito",
            usuario
        });
    }
}
