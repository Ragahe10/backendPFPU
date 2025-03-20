using backendPFPU.Models;

namespace backendPFPU.Respositories;

public interface IUsuarioRepository{
    public void PostAdministrador(Administrador usuario);
    public void PostDocente(Docente usuario);
    public void UpdateDocente(Docente usuario);
    public void UpdateAdministrador(Administrador usuario);
    public void PostAlumno(Alumno usuario);
    public void PutAlumno(Alumno usuario);

    public void DeleteAlumno(int id_usuario);
    public void DeleteDocente(int id_usuario);
    public void DeleteAdministrador(int id_usuario);
    public List<Usuario> GetUsuarios();

    public List<Usuario> GetDocentes();

    public List<Alumno> GetAlumnos();

    public List<Alumno> GetAlumnosByDocente(int docente);

    public List<Alumno> GetAlumnosByMateria(int id_materia);
    public List<Alumno> GetAlumnosByCurso(int id_curso);

    public List<Usuario> GetAdministradores();
    public Usuario GetUsuario(int id_usuario);

    public Alumno GetAlumno(int id_usuario);

    public Usuario GetDocente(int id_usuario);
    public void UpdateUsuario(Usuario usuario);
    public void DeleteUsuario(int id_usuario);

    public void DesactivarUsuario(int id_usuario);
    public void ActivarUsuario(int id_usuario);
}