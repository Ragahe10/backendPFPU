using backendPFPU.Models;

namespace backendPFPU.Respositories;

public interface IUsuarioRepository{
    public void PostAdministrador(Administrador usuario);
    public void PostProfesor(Profesor usuario);
    public void PostAlumno(Alumno usuario);
    public List<Usuario> GetUsuarios();
    public Usuario GetUsuario(int id_usuario);
    public void UpdateUsuario(Usuario usuario);
    public void DeleteUsuario(int id_usuario);
}