using backendPFPU.Models;
using Microsoft.Data.Sqlite;

namespace backendPFPU.Respositories;

public class UsuarioRepository : IUsuarioRepository{
    private string _CadenaDeConexion;

    public UsuarioRepository(string cadenaDeConexion)
    {
        _CadenaDeConexion = cadenaDeConexion;
    }

    public void PostUsuario(Usuario usuario){
        var query = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido) VALUES (@dni, @contrasenia, @correo, @nombre, @apellido)";
        using(SqliteConnection connection = new SqliteConnection(_CadenaDeConexion)){
            var command = new SqliteCommand(query,connection);
            command.Parameters.Add(new SqliteParameter("@dni", usuario.Dni.ToString()));
            command.Parameters.Add(new SqliteParameter("@contrasenia", usuario.Contrasenia));
            command.Parameters.Add(new SqliteParameter("@correo", usuario.Correo));
            command.Parameters.Add(new SqliteParameter("@nombre", usuario.Nombre));
            command.Parameters.Add(new SqliteParameter("@apellido", usuario.Apellido));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<Usuario> GetUsuarios(){
        return null;
    }
    public Usuario GetUsuario(int id_usuario){
        return null;
    }
    public void UpdateUsuario(Usuario usuario){}
    public void DeleteUsuario(int id_usuario){}
}