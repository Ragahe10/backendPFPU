using backendPFPU.Models;

namespace backendPFPU.Respositories;

public interface IUsuarioRepository{
    public void PostUsuario(Usuario usuario);
    public List<Usuario> GetUsuarios();
    public Usuario GetUsuario(int id_usuario);
    public void UpdateUsuario(Usuario usuario);
    public void DeleteUsuario(int id_usuario);
}