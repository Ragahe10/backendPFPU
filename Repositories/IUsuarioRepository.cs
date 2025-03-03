using backendPFPU.Models;

namespace backendPFPU.Respositories;

public interface IUsuarioRepository{
    public void PostAdministrador(Administrador usuario);
    public void PostDocente(Docente usuario);
    public void PostAlumno(Alumno usuario);
    public void PutAlumno(Alumno usuario);

    public void DeleteAlumno(int id_usuario);
    public List<Usuario> GetUsuarios();

    public List<Usuario> GetDocentes();

    public List<Alumno> GetAlumnos();

    public List<Usuario> GetAdministradores();
    public Usuario GetUsuario(int id_usuario);

    public Usuario GetDocente(int id_usuario);
    public void UpdateUsuario(Usuario usuario);
    public void DeleteUsuario(int id_usuario);
}