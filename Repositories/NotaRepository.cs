using backendPFPU.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace backendPFPU.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private string _CadenaDeConexion;

        public NotaRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }
        public void CreateNota(Nota nota)
        {
            var query = "INSERT INTO nota (id_alumno, id_materia, fecha, nota, descripcion, trimestre) VALUES (@id_alumno, @id_materia, @fecha, @nota, @descripcion, @trimestre)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id_alumno", DbType.Int32) { Value = nota.id_alumno });
                    command.Parameters.Add(new SqliteParameter("@id_materia", DbType.Int32) { Value = nota.id_materia });
                    command.Parameters.Add(new SqliteParameter("@fecha", DbType.String) { Value = nota.fecha });
                    command.Parameters.Add(new SqliteParameter("@nota", DbType.Int32) { Value = nota.nota });
                    command.Parameters.Add(new SqliteParameter("@descripcion", DbType.String) { Value = nota.descripcion });
                    command.Parameters.Add(new SqliteParameter("@trimestre", DbType.Int32) { Value = nota.trimestre });
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNota(int id_nota, int id_alumno, int id_materia, string fecha)
        {
            var query = "DELETE FROM nota WHERE id_nota = @id_nota AND id_alumno = @id_alumno AND id_materia = @id_materia AND fecha = @fecha";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id_nota", DbType.Int32) { Value = id_nota });
                    command.Parameters.Add(new SqliteParameter("@id_alumno", DbType.Int32) { Value = id_alumno });
                    command.Parameters.Add(new SqliteParameter("@id_materia", DbType.Int32) { Value = id_materia });
                    command.Parameters.Add(new SqliteParameter("@fecha", DbType.String) { Value = fecha });
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró un registro con ese ID.");
                    }
                }
            }

        }

        public Nota GetNota(int id_nota, int id_alumno, int id_materia, string fecha)
        {
            var query = "SELECT * FROM nota WHERE id_nota = @id_nota AND id_alumno = @id_alumno AND id_materia = @id_materia AND fecha = @fecha";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id_nota", DbType.Int32) { Value = id_nota });
                    command.Parameters.Add(new SqliteParameter("@id_alumno", DbType.Int32) { Value = id_alumno });
                    command.Parameters.Add(new SqliteParameter("@id_materia", DbType.Int32) { Value = id_materia });
                    command.Parameters.Add(new SqliteParameter("@fecha", DbType.String) { Value = fecha });
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var nota = new Nota
                            {
                                id_nota = reader.GetInt32(0),
                                id_alumno = reader.GetInt32(1),
                                id_materia = reader.GetInt32(2),
                                fecha = reader.GetString(6),
                                nota = reader.GetInt32(3),
                                descripcion = reader.GetString(4),
                                trimestre = reader.GetInt32(5)
                            };
                            return nota;
                        }
                        else
                        {
                            throw new Exception("No se encontró un registro con ese ID.");
                        }
                    }
                }
            }
        }

        public List<Nota> GetNotas()
        {
            var query = "SELECT * FROM nota";
            var notas = new List<Nota>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nota = new Nota
                            {
                                id_nota = reader.GetInt32(0),
                                id_alumno = reader.GetInt32(1),
                                id_materia = reader.GetInt32(2),
                                fecha = reader.GetString(6),
                                nota = reader.GetInt32(3),
                                descripcion = reader.GetString(4),
                                trimestre = reader.GetInt32(5)
                            };
                            notas.Add(nota);
                        }
                    }
                }
            }
            return notas;
        }

        public List<Nota> GetNotasByAlumno(int id_alumno)
        {
            var query = "SELECT * FROM nota WHERE id_alumno = @id_alumno";
            var notas = new List<Nota>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id_alumno", DbType.Int32) { Value = id_alumno });
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nota = new Nota
                            {
                                id_nota = reader.GetInt32(0),
                                id_alumno = reader.GetInt32(1),
                                id_materia = reader.GetInt32(2),
                                fecha = reader.GetString(6),
                                nota = reader.GetInt32(3),
                                descripcion = reader.GetString(4),
                                trimestre = reader.GetInt32(5)
                            };
                            notas.Add(nota);
                        }
                    }
                }
            }
            return notas;
        }

        public List<Nota> GetNotasByMateria(int id_materia)
        {
            var query = "SELECT * FROM nota WHERE id_materia = @id_materia";
            var notas = new List<Nota>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id_materia", DbType.Int32) { Value = id_materia });
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nota = new Nota
                            {
                                id_nota = reader.GetInt32(0),
                                id_alumno = reader.GetInt32(1),
                                id_materia = reader.GetInt32(2),
                                fecha = reader.GetString(6),
                                nota = reader.GetInt32(3),
                                descripcion = reader.GetString(4),
                                trimestre = reader.GetInt32(5)
                            };
                            notas.Add(nota);
                        }
                    }
                }
            }
            return notas;
        }

        public void UpdateNota(Nota nota)
        {
            var query = "UPDATE nota SET nota = @nota, descripcion = @descripcion, trimestre = @trimestre WHERE id_nota = @id_nota AND id_alumno = @id_alumno AND id_materia = @id_materia AND fecha = @fecha";
            var notas = new List<Nota>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id_nota", DbType.Int32) { Value = nota.id_nota });
                    command.Parameters.Add(new SqliteParameter("@id_alumno", DbType.Int32) { Value = nota.id_alumno });
                    command.Parameters.Add(new SqliteParameter("@id_materia", DbType.Int32) { Value = nota.id_materia });
                    command.Parameters.Add(new SqliteParameter("@fecha", DbType.String) { Value = nota.fecha });
                    command.Parameters.Add(new SqliteParameter("@nota", DbType.Int32) { Value = nota.nota });
                    command.Parameters.Add(new SqliteParameter("@descripcion", DbType.String) { Value = nota.descripcion });
                    command.Parameters.Add(new SqliteParameter("@trimestre", DbType.Int32) { Value = nota.trimestre });
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró un registro con ese ID.");
                    }
                }

            }
        }
    }
}
