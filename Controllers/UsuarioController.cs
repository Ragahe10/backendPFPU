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
    [Route("/PostDocente")]
    public IActionResult PostDocente(Docente usuario)
    {
        if (usuario == null)
        {
            return BadRequest(new { msg = "El Profesor proporcionado es inválido." });
        }

        // Guardar el usuario utilizando el repositorio
        _usuarioRepository.PostDocente(usuario);

        return Ok(new
        {
            msg = "El Profesor se guardó con éxito",
        });
    }


    [HttpPost]
    [Route("/PostAdministrador")]
    public IActionResult PostAdministrador(Administrador usuario)
    {
        if (usuario == null)
        {
            return BadRequest(new { msg = "El Administrador proporcionado es inválido." });
        }
        // Guardar el usuario utilizando el repositorio
        _usuarioRepository.PostAdministrador(usuario);
        return Ok(new
        {
            msg = "El Administrador se guardó con éxito",
        });
    }

    [HttpPost]
    [Route("/PostAlumno")]
    public IActionResult PostAlumno(Alumno usuario)
    {
        if (usuario == null)
        {
            return BadRequest(new { msg = "El Alumno proporcionado es inválido." });
        }
        // Guardar el usuario utilizando el repositorio
        _usuarioRepository.PostAlumno(usuario);
        return Ok(new
        {
            msg = "El Alumno se guardó con éxito",
        });
    }

    [HttpGet]
    [Route("/GetDocentes")]
    public IActionResult GetProfesores()
    {
        var profesores = _usuarioRepository.GetDocentes();
        if (profesores.Count == 0)
        {
            return NotFound(new { msg = "No se encontraron profesores." });
        }
        return Ok(profesores);
    }

    [HttpGet]
    [Route("/GetDocente")]
    public IActionResult GetProfesor(int id_usuario)
    {
        var profesor = _usuarioRepository.GetDocente(id_usuario);
        if (profesor.Id_usuario == 0)
        {
            return NotFound(new { msg = "El profesor no se encontró." });
        }
        return Ok(profesor);
    }

    [HttpGet]
    [Route("/GetAlumnos")]
    public IActionResult GetAlumnos()
    {
        var alumnos = _usuarioRepository.GetAlumnos();
        if (alumnos.Count == 0)
        {
            return NotFound(new { msg = "No se encontraron alumnos." });
        }
        return Ok(alumnos);
    }

    [HttpGet]
    [Route("/GetAdministradores")]
    public IActionResult GetAdministradores()
    {
        var administradores = _usuarioRepository.GetAdministradores();
        if (administradores.Count == 0)
        {
            return NotFound(new { msg = "No se encontraron administradores." });
        }
        return Ok(administradores);
    }

    [HttpGet]
    [Route("/GetUsuario")]
    public IActionResult GetUsuario(int id_usuario)
    {
        var usuario = _usuarioRepository.GetUsuario(id_usuario);
        if (usuario.Id_usuario == 0)
        {
            return NotFound(new { msg = "El usuario no se encontró." });
        }
        return Ok(usuario);
    }

    [HttpGet]
    [Route("/GetAlumno")]
    public IActionResult GetAlumno(int id_usuario)
    {
        var alumno = _usuarioRepository.GetAlumno(id_usuario);
        if (alumno.Id_usuario == 0)
        {
            return NotFound(new { msg = "El alumno no se encontró." });
        }
        return Ok(alumno);
    }

    [HttpGet]
    [Route("/GetAlumnosByMateria")]
    public IActionResult GetAlumnosByMateria(int id_materia)
    {
        var alumnos = _usuarioRepository.GetAlumnosByMateria(id_materia);
        if (alumnos.Count == 0)
        {
            return NotFound(new { msg = "No se encontraron alumnos." });
        }
        return Ok(alumnos);
    }

    [HttpGet]
    [Route("/GetAlumnosByCurso")]
    public IActionResult GetAlumnosByCurso(int id_curso)
    {
        var alumnos = _usuarioRepository.GetAlumnosByCurso(id_curso);
        if (alumnos.Count == 0)
        {
            return NotFound(new { msg = "No se encontraron alumnos." });
        }
        return Ok(alumnos);
    }

    [HttpPut]
    [Route("/UpdateAlumno")]
    public IActionResult UpdateUsuario(Alumno usuario)
    {
        if (usuario.Id_usuario == 0)
        {
            return BadRequest(new { msg = "El usuario proporcionado es inválido." });
        }
        _usuarioRepository.PutAlumno(usuario);
        return Ok(new
        {
            msg = "El usuario se actualizó con éxito",
        });
    }

    [HttpPut]
    [Route("/UpdateDocente")]
    public IActionResult UpdateUsuario(Docente usuario)
    {
        if (usuario.Id_usuario == 0)
        {
            return BadRequest(new { msg = "El usuario proporcionado es inválido." });
        }
        _usuarioRepository.UpdateDocente(usuario);
        return Ok(new
        {
            msg = "El usuario se actualizó con éxito",
        });
    }

    [HttpPut]
    [Route("/UpdateAdministrador")]
    public IActionResult UpdateUsuario(Administrador usuario)
    {
        if (usuario.Id_usuario == 0)
        {
            return BadRequest(new { msg = "El usuario proporcionado es inválido." });
        }
        _usuarioRepository.UpdateAdministrador(usuario);
        return Ok(new
        {
            msg = "El usuario se actualizó con éxito",
        });
    }

    [HttpDelete]
    [Route("/DeleteAlumno")]
    public IActionResult DeleteUsuario(int id_usuario)
    {
        var usuario = _usuarioRepository.GetUsuario(id_usuario);
        if (usuario.Id_usuario == 0)
        {
            return BadRequest(new { msg = "El id proporcionado es inválido." });
        }
        _usuarioRepository.DeleteAlumno(id_usuario);
        return Ok(new
        {
            msg = "El alumno se eliminó con éxito",
        });
    }

    [HttpDelete]
    [Route("/DeleteDocente")]
    public IActionResult DeleteDocente(int id_usuario)
    {
        var usuario = _usuarioRepository.GetUsuario(id_usuario);
        if (usuario.Id_usuario == 0)
        {
            return BadRequest(new { msg = "El id proporcionado es inválido." });
        }
        _usuarioRepository.DeleteDocente(id_usuario);
        return Ok(new
        {
            msg = "El docente se eliminó con éxito",
        });
    }

    [HttpDelete]
    [Route("/DeleteAdministrador")]
    public IActionResult DeleteAdministrador(int id_usuario)
    {
        var usuario = _usuarioRepository.GetUsuario(id_usuario);
        if (usuario.Id_usuario == 0)
        {
            return BadRequest(new { msg = "El id proporcionado es inválido." });
        }
        _usuarioRepository.DeleteAdministrador(id_usuario);
        return Ok(new
        {
            msg = "El administrador se eliminó con éxito",
        });
    }

}
