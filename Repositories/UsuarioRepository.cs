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
        var usuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo, telefono) 
                         VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo, @telefono)";

        var lastIdQuery = "SELECT last_insert_rowid();";

        var administradorQuery = @"INSERT INTO administrativo (id_admin) VALUES (@id_admin)";

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
                        usuarioCommand.Parameters.AddWithValue("@telefono", usuario.Telefono);
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


    public void PostDocente(Docente usuario)
    {
        var insertUsuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo, telefono) 
                           VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo, @telefono);";

        var lastIdQuery = "SELECT last_insert_rowid();";

        var insertProfesorQuery = @"INSERT INTO docente (id_docente) VALUES (@id_docente)";

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
                        insertCommand.Parameters.AddWithValue("@tipo", "docente");
                        insertCommand.Parameters.AddWithValue("@telefono", usuario.Telefono);

                        insertCommand.ExecuteNonQuery();
                    }

                    // Obtener el último ID insertado
                    int id_docente;
                    using (var lastIdCommand = new SqliteCommand(lastIdQuery, connection, transaction))
                    {
                        id_docente = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    }

                    // Insertar en la tabla profesor
                    using (var profesorCommand = new SqliteCommand(insertProfesorQuery, connection, transaction))
                    {
                        profesorCommand.Parameters.AddWithValue("@id_docente", id_docente);
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
    var usuarioQuery = @"INSERT INTO usuario (dni, contrasenia, correo, nombre, apellido, tipo, telefono) 
                         VALUES (@dni, @contrasenia, @correo, @nombre, @apellido, @tipo, @telefono)";

    var lastIdQuery = "SELECT last_insert_rowid();";

    var alumnoQuery = @"INSERT INTO alumno (id_alumno, direccion, matricula, fecha_nac, id_curso) 
                        VALUES (@id_alumno, @direccion, @matricula, @fecha_nac, @id_curso)";

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
                        usuarioCommand.Parameters.AddWithValue("@tipo", "Alumno");
                        usuarioCommand.Parameters.AddWithValue("@telefono", usuario.Telefono);
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
                        alumnoCommand.Parameters.AddWithValue("@id_curso", usuario.Id_curso);
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

    public void PutAlumno(Alumno usuario)
    {
        var usuarioQuery = @"UPDATE usuario 
                         SET dni = @dni, 
                             correo = @correo, 
                             nombre = @nombre, 
                             apellido = @apellido, 
                             telefono = @telefono 
                         WHERE id_usuario = @id_usuario";

        var alumnoQuery = @"UPDATE alumno 
                        SET direccion = @direccion, 
                            matricula = @matricula, 
                            fecha_nac = @fecha_nac, 
                            id_curso = @id_curso 
                        WHERE id_alumno = @id_alumno";

        using (var connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Actualizar en la tabla usuario
                    using (var usuarioCommand = new SqliteCommand(usuarioQuery, connection, transaction))
                    {
                        usuarioCommand.Parameters.AddWithValue("@dni", usuario.Dni.ToString());
                        usuarioCommand.Parameters.AddWithValue("@correo", usuario.Correo);
                        usuarioCommand.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        usuarioCommand.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        usuarioCommand.Parameters.AddWithValue("@telefono", usuario.Telefono);
                        usuarioCommand.Parameters.AddWithValue("@id_usuario", usuario.Id_usuario);
                        usuarioCommand.ExecuteNonQuery();
                    }

                    // Actualizar en la tabla alumno
                    using (var alumnoCommand = new SqliteCommand(alumnoQuery, connection, transaction))
                    {
                        alumnoCommand.Parameters.AddWithValue("@direccion", usuario.Direccion);
                        alumnoCommand.Parameters.AddWithValue("@matricula", usuario.Matricula);
                        alumnoCommand.Parameters.AddWithValue("@fecha_nac", usuario.Fecha_nac);
                        alumnoCommand.Parameters.AddWithValue("@id_curso", usuario.Id_curso);
                        alumnoCommand.Parameters.AddWithValue("@id_alumno", usuario.Id_usuario);
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

    public Usuario GetDocente(int id_usuario)
    {
        var docenteQuery = "SELECT * FROM usuario WHERE id_usuario = @id_usuario AND tipo = 'Docente'";
        var docente = new Usuario();
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(docenteQuery, connection);
            command.Parameters.Add(new SqliteParameter("@id_usuario", id_usuario));
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                docente.Id_usuario = reader.GetInt32(0);
                docente.Dni = reader.GetInt32(1);
                docente.Contrasenia = reader.GetString(2);
                docente.Correo = reader.GetString(3);
                docente.Nombre = reader.GetString(4);
                docente.Apellido = reader.GetString(5);
                docente.Tipo = reader.GetString(6);
            }
            connection.Close();
        }
        return docente;
    }

    public void DeleteAlumno(int id_usuario)
    {
        var alumnoQuery = "DELETE FROM alumno WHERE id_alumno = @id_usuario";
        var usuarioQuery = "DELETE FROM usuario WHERE id_usuario = @id_usuario";

        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Primero elimina de la tabla alumno
                    using (var alumnoCommand = new SqliteCommand(alumnoQuery, connection, transaction))
                    {
                        alumnoCommand.Parameters.AddWithValue("@id_usuario", id_usuario);
                        alumnoCommand.ExecuteNonQuery();
                    }

                    // Luego elimina de la tabla usuario
                    using (var usuarioCommand = new SqliteCommand(usuarioQuery, connection, transaction))
                    {
                        usuarioCommand.Parameters.AddWithValue("@id_usuario", id_usuario);
                        usuarioCommand.ExecuteNonQuery();
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


    public List<Usuario> GetDocentes()
    {
        var profesores = new List<Usuario>();
        var profesorQuery = "Select * from usuario where tipo = 'Docente'";
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
                profesor.Telefono = reader.GetString(8);
                profesores.Add(profesor);
            }
            connection.Close();
        }
        return profesores;
    }

    public List<Alumno> GetAlumnos()
    {
        var alumnos = new List<Alumno>();
        var alumnoQuery = "SELECT id_usuario, dni, correo, nombre, apellido, direccion, matricula, fecha_nac, telefono, id_curso FROM usuario INNER JOIN alumno ON usuario.id_usuario = alumno.id_alumno;";
        using (SqliteConnection connection = new SqliteConnection(_CadenaDeConexion))
        {
            connection.Open();
            var command = new SqliteCommand(alumnoQuery, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var alumno = new Alumno();
                alumno.Id_usuario = reader.GetInt32(0);
                alumno.Dni = reader.GetInt32(1);
                alumno.Correo = reader.GetString(2); 
                alumno.Nombre = reader.GetString(3); 
                alumno.Apellido = reader.GetString(4);
                alumno.Direccion = reader.GetString(5);
                alumno.Matricula = reader.GetInt32(6); 
                alumno.Fecha_nac = reader.GetDateTime(7);
                alumno.Telefono = reader.GetString(8);
                alumno.Id_curso = reader.GetInt32(9);

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
                administrador.Telefono = reader.GetString(8);
                administradores.Add(administrador);
            }
            connection.Close();
        }
        return administradores;


    }

    public void UpdateUsuario(Usuario usuario) { }
    public void DeleteUsuario(int id_usuario) { }
}