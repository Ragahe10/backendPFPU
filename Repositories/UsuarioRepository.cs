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
        var usuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo) 
                         VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo)";

        var lastIdQuery = "SELECT last_insert_rowid();";

        var administradorQuery = @"INSERT INTO administrativo (id_admin, rango) VALUES (@id_admin, @rango)";

        using (var connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    int id_administrador;

                    // Insertar en la tabla usuario
                    using (var usuarioCommand = new SqliteCommand(usuarioQuery, connection, transaction))
                    {
                        usuarioCommand.Parameters.AddWithValue("@dni", usuario.Dni.ToString());
                        usuarioCommand.Parameters.AddWithValue("@contrasenia", _service.Encriptar(usuario.Contrasenia));
                        usuarioCommand.Parameters.AddWithValue("@correo", usuario.Correo);
                        usuarioCommand.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        usuarioCommand.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        usuarioCommand.Parameters.AddWithValue("@tipo", usuario.Tipo);
                        usuarioCommand.ExecuteNonQuery();
                    }

                    // Obtener el último ID insertado
                    using (var lastIdCommand = new SqliteCommand(lastIdQuery, connection, transaction))
                    {
                        id_administrador = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    }

                    // Insertar en la tabla administrativo
                    using (var administradorCommand = new SqliteCommand(administradorQuery, connection, transaction))
                    {
                        administradorCommand.Parameters.AddWithValue("@id_admin", id_administrador);
                        administradorCommand.Parameters.AddWithValue("@rango", usuario.Rango);
                        administradorCommand.ExecuteNonQuery();
                    }

                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }


    public void PostProfesor(Profesor usuario)
    {
        var insertUsuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo) 
                           VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo);";

        var lastIdQuery = "SELECT last_insert_rowid();";

        var insertProfesorQuery = @"INSERT INTO profesor (id_profesor) VALUES (@id_profesor)";

        using (var connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Insertar usuario
                    using (var insertCommand = new SqliteCommand(insertUsuarioQuery, connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@dni", usuario.Dni.ToString());
                        insertCommand.Parameters.AddWithValue("@contrasenia", _service.Encriptar(usuario.Contrasenia));
                        insertCommand.Parameters.AddWithValue("@correo", usuario.Correo);
                        insertCommand.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        insertCommand.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        insertCommand.Parameters.AddWithValue("@tipo", usuario.Tipo);

                        insertCommand.ExecuteNonQuery();
                    }

                    // Obtener el último ID insertado
                    int id_profesor;
                    using (var lastIdCommand = new SqliteCommand(lastIdQuery, connection, transaction))
                    {
                        id_profesor = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    }

                    // Insertar en la tabla profesor
                    using (var profesorCommand = new SqliteCommand(insertProfesorQuery, connection, transaction))
                    {
                        profesorCommand.Parameters.AddWithValue("@id_profesor", id_profesor);
                        profesorCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

            }
            connection.Close();

        }

    }


    public void PostAlumno(Alumno usuario) { 
    var usuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo) 
                         VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo)";

    var lastIdQuery = "SELECT last_insert_rowid();";

    var alumnoQuery = @"INSERT INTO alumno (id_alumno, direccion, matricula, fecha_nac) 
                        VALUES (@id_alumno, @direccion, @matricula, @fecha_nac)";

        using (var connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    int id_alumno;
                    // Insertar en la tabla usuario
                    using (var usuarioCommand = new SqliteCommand(usuarioQuery, connection, transaction))
                    {
                        usuarioCommand.Parameters.AddWithValue("@dni", usuario.Dni.ToString());
                        usuarioCommand.Parameters.AddWithValue("@contrasenia", _service.Encriptar(usuario.Contrasenia));
                        usuarioCommand.Parameters.AddWithValue("@correo", usuario.Correo);
                        usuarioCommand.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        usuarioCommand.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        usuarioCommand.Parameters.AddWithValue("@tipo", usuario.Tipo);
                        usuarioCommand.ExecuteNonQuery();
                    }
                    // Obtener el último ID insertado
                    using (var lastIdCommand = new SqliteCommand(lastIdQuery, connection, transaction))
                    {
                        id_alumno = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    }
                    // Insertar en la tabla alumno
                    using (var alumnoCommand = new SqliteCommand(alumnoQuery, connection, transaction))
                    {
                        alumnoCommand.Parameters.AddWithValue("@id_alumno", id_alumno);
                        alumnoCommand.Parameters.AddWithValue("@direccion", usuario.Direccion);
                        alumnoCommand.Parameters.AddWithValue("@matricula", usuario.Matricula);
                        alumnoCommand.Parameters.AddWithValue("@fecha_nac", usuario.Fecha_nac);
                        alumnoCommand.ExecuteNonQuery();
                    }
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            connection.Close();
        }
       
    }
    public List<Usuario> GetUsuarios()
    {
        var query = "Select * from usuario";
        var usuarios = new List<Usuario>();
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var usuario = new Usuario();
                usuario.Id_usuario = reader.GetInt32(0);
                usuario.Dni = reader.GetInt32(1);
                usuario.Contrasenia = reader.GetString(2);
                usuario.Correo = reader.GetString(3);
                usuario.Nombre = reader.GetString(4);
                usuario.Apellido = reader.GetString(5);
                usuario.Tipo = reader.GetString(6);
                usuarios.Add(usuario);
            }
            connection.Close();
        }
        return usuarios;

    }
    public Usuario GetUsuario(int id_usuario)
    {
        var usuarioQuery = "Select * from usuario where id_usuario = @id_usuario";
        var usuario = new Usuario();

        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(usuarioQuery, connection);
            command.Parameters.Add(new SqliteParameter("@id_usuario", id_usuario));
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                usuario.Id_usuario = reader.GetInt32(0);
                usuario.Dni = reader.GetInt32(1);
                usuario.Contrasenia = reader.GetString(2);
                usuario.Correo = reader.GetString(3);
                usuario.Nombre = reader.GetString(4);
                usuario.Apellido = reader.GetString(5);
                usuario.Tipo = reader.GetString(6);
            }
            connection.Close();
        }
        return usuario;

    }

    public List<Usuario> GetProfesores()
    {
        var profesores = new List<Usuario>();
        var profesorQuery = "Select * from usuario where tipo = 'Profesor'";
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(profesorQuery, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var profesor = new Usuario();
                profesor.Id_usuario = reader.GetInt32(0);
                profesor.Dni = reader.GetInt32(1);
                profesor.Contrasenia = reader.GetString(2);
                profesor.Correo = reader.GetString(3);
                profesor.Nombre = reader.GetString(4);
                profesor.Apellido = reader.GetString(5);
                profesor.Tipo = reader.GetString(6);
                profesores.Add(profesor);
            }
            connection.Close();
        }
        return profesores;
    }

    public List<Usuario> GetAlumnos() {
        var alumnos = new List<Usuario>();
        var alumnoQuery = "Select * from usuario where tipo = 'Alumno'";
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(alumnoQuery, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var alumno = new Usuario();
                alumno.Id_usuario = reader.GetInt32(0);
                alumno.Dni = reader.GetInt32(1);
                alumno.Contrasenia = reader.GetString(2);
                alumno.Correo = reader.GetString(3);
                alumno.Nombre = reader.GetString(4);
                alumno.Apellido = reader.GetString(5);
                alumno.Tipo = reader.GetString(6);
                alumnos.Add(alumno);
            }
            connection.Close();
        }
        return alumnos;

    }
    public List<Usuario> GetAdministradores() {
        var query = "Select * from usuario where tipo = 'Administrativo'";
        var administradores = new List<Usuario>();
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var administrador = new Usuario();
                administrador.Id_usuario = reader.GetInt32(0);
                administrador.Dni = reader.GetInt32(1);
                administrador.Contrasenia = reader.GetString(2);
                administrador.Correo = reader.GetString(3);
                administrador.Nombre = reader.GetString(4);
                administrador.Apellido = reader.GetString(5);
                administrador.Tipo = reader.GetString(6);
                administradores.Add(administrador);
            }
            connection.Close();
        }
        return administradores;


    }

    public void UpdateUsuario(Usuario usuario) { }
    public void DeleteUsuario(int id_usuario) { }
}