using backendPFPU.Models;
using Microsoft.Data.Sqlite;
using BCrypt.Net;
using backendPFPU.Helpers;

namespace backendPFPU.Respositories;

public class UsuarioRepository : IUsuarioRepository
{
    private string _CadenaDeConexion;
    private PasswordService _service;

    public UsuarioRepository(string cadenaDeConexion)
    {
        _CadenaDeConexion = cadenaDeConexion;
        _service = new PasswordService();
    }

    public void PostAdministrador(Administrador usuario)
    {
        var query = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido) VALUES (@dni, @contrasenia, @correo, @nombre, @apellido)";
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@dni", usuario.Dni.ToString()));
            command.Parameters.Add(new SqliteParameter("@contrasenia", _service.Encriptar(usuario.Contrasenia)));
            command.Parameters.Add(new SqliteParameter("@correo", usuario.Correo));
            command.Parameters.Add(new SqliteParameter("@nombre", usuario.Nombre));
            command.Parameters.Add(new SqliteParameter("@apellido", usuario.Apellido));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void PostProfesor(Profesor usuario)
    {
        var usuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo) 
                         VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo);
                         SELECT last_insert_rowid();";

        var profesorQuery = @"INSERT INTO profesor (id_profesor) VALUES (@id_profesor)";

        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Insertar en la tabla usuario y obtener el ID generado
                    var usuarioCommand = new SqliteCommand(usuarioQuery, connection, transaction);
                    usuarioCommand.Parameters.AddWithValue("@dni", usuario.Dni.ToString());
                    usuarioCommand.Parameters.AddWithValue("@contrasenia", _service.Encriptar(usuario.Contrasenia));
                    usuarioCommand.Parameters.AddWithValue("@correo", usuario.Correo);
                    usuarioCommand.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    usuarioCommand.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    usuarioCommand.Parameters.AddWithValue("@tipo", usuario.Tipo);

                    var id_profesor = Convert.ToInt32(usuarioCommand.ExecuteScalar());

                    // Insertar en la tabla profesor
                    var profesorCommand = new SqliteCommand(profesorQuery, connection, transaction);
                    profesorCommand.Parameters.AddWithValue("@id_profesor", id_profesor);
                    profesorCommand.ExecuteNonQuery();

                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch
                {
                    // Revertir la transacción en caso de error
                    transaction.Rollback();
                    throw;
                }
            }

            connection.Close();
        }
    }


    public void PostAlumno(Alumno usuario) { }
    public List<Usuario> GetUsuarios()
    {
        return null;
    }
    public Usuario GetUsuario(int id_usuario)
    {
        return null;
    }
    public void UpdateUsuario(Usuario usuario) { }
    public void DeleteUsuario(int id_usuario) { }
}