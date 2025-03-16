using backendPFPU.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace backendPFPU.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private string _CadenaDeConexion;

         public MateriaRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }
        public void AddMateria(Materia materia)
        {
            var query = "INSERT INTO materia (materia, id_anio, id_docente) VALUES (@materia, @id_anio, @id_docente)";

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@materia", materia.materia);
                    command.Parameters.AddWithValue("@id_anio", materia.Id_anio);

                    // Insertar NULL en id_docente si es 0 o menor
                    if (materia.Id_docente > 0)
                        command.Parameters.AddWithValue("@id_docente", materia.Id_docente);
                    else
                        command.Parameters.AddWithValue("@id_docente", DBNull.Value);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }


        public void DeleteMateria(int id)
        {
           var query = "DELETE FROM materia WHERE id_materia = @id";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public Materia GetMateria(int id)
        {
            var query = "SELECT * FROM materia WHERE id_materia = @id";
            Materia materia = null;

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            materia = new Materia
                            {
                                Id_materia = reader.GetInt32(reader.GetOrdinal("id_materia")),
                                materia = reader.GetString(reader.GetOrdinal("materia")),
                                Id_anio = reader.GetInt32(reader.GetOrdinal("id_anio")),
                                Id_docente = reader.IsDBNull(reader.GetOrdinal("id_docente")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_docente"))
                            };
                        }
                    }
                }
                connection.Close();
            }
            return materia;
        }

        public List<Materia> GetMaterias()
        {
            var query = "SELECT * FROM materia";
            var materias = new List<Materia>();

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var materia = new Materia
                        {
                            Id_materia = reader.GetInt32(reader.GetOrdinal("id_materia")),
                            materia = reader.GetString(reader.GetOrdinal("materia")),
                            Id_anio = reader.GetInt32(reader.GetOrdinal("id_anio")),
                            Id_docente = reader.IsDBNull(reader.GetOrdinal("id_docente")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_docente"))
                        };
                        materias.Add(materia);
                    }
                }
                connection.Close();
            }

            return materias;
        }


        public void UpdateMateria(Materia materia)
        {
            var query = "UPDATE materia SET materia = @materia, id_anio = @id_anio, id_docente = @id_docente WHERE id_materia = @id_materia";

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@materia", materia.materia);
                    command.Parameters.AddWithValue("@id_anio", materia.Id_anio);

                    if (materia.Id_docente.HasValue)
                        command.Parameters.AddWithValue("@id_docente", materia.Id_docente.Value);
                    else
                        command.Parameters.AddWithValue("@id_docente", DBNull.Value);

                    command.Parameters.AddWithValue("@id_materia", materia.Id_materia);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Materia> GetMateriasByAnio(int id_anio)
        {
            var query = "SELECT * FROM materia WHERE id_anio = @id_anio AND id_docente IS NULL";
            var materias = new List<Materia>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_anio", id_anio);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var materia = new Materia
                            {
                                Id_materia = reader.GetInt32(reader.GetOrdinal("id_materia")),
                                materia = reader.GetString(reader.GetOrdinal("materia")),
                                Id_anio = reader.GetInt32(reader.GetOrdinal("id_anio")),
                                Id_docente = reader.IsDBNull(reader.GetOrdinal("id_docente")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_docente"))


                            };
                            materias.Add(materia);
                        }
                    }
                }
                connection.Close();
            }
            return materias;
        }

        public List<Materia> GetMateriasByDocente(int id_docente)
        {
            var query = "SELECT * FROM materia WHERE id_docente = @id_docente";
            var materias = new List<Materia>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_docente", id_docente);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var materia = new Materia
                            {
                                Id_materia = reader.GetInt32(reader.GetOrdinal("id_materia")),
                                materia = reader.GetString(reader.GetOrdinal("materia")),
                                Id_anio = reader.GetInt32(reader.GetOrdinal("id_anio")),
                                Id_docente = reader.GetInt32(reader.GetOrdinal("id_docente"))
                            };
                            materias.Add(materia);
                        }
                    }
                }
                connection.Close();
            }
            return materias;
        }

        public List<Materia> GetMateriasByAlumno(int id_alumno)
        {
            var query = "SELECT * FROM materia INNER JOIN anio USING(id_anio) INNER JOIN curso USING(id_anio) INNER JOIN alumno USING(id_curso) WHERE id_alumno = @id_alumno";
            var materias = new List<Materia>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var materia = new Materia
                            {
                                Id_materia = reader.GetInt32(reader.GetOrdinal("id_materia")),
                                materia = reader.GetString(reader.GetOrdinal("materia")),
                                Id_anio = reader.GetInt32(reader.GetOrdinal("id_anio")),
                                Id_docente = reader.IsDBNull(reader.GetOrdinal("id_docente")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_docente"))
                            };
                            materias.Add(materia);
                        }
                    }
                }
                connection.Close();
            }
            return materias;
        }

        public int GetCantidadMateriasByAlumno(int id_alumno)
        {
            var query = "SELECT COUNT(*) FROM materia INNER JOIN anio USING(id_anio) INNER JOIN curso USING(id_anio) INNER JOIN alumno USING(id_curso) WHERE id_alumno = @id_alumno";
            int cantidad = 0;
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    cantidad = Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return cantidad;
        }

     



    }
}
