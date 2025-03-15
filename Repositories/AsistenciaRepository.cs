using backendPFPU.Models;
using Microsoft.Data.Sqlite;

namespace backendPFPU.Repositories
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private string _CadenaDeConexion;

        public AsistenciaRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }
        public void AddAsistencia(Asistencia item)
        {
            var query = "INSERT INTO asistencia (id_alumno, id_materia, fecha, estado) VALUES (@id_alumno, @id_materia, @fecha, @estado)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_alumno", item.id_alumno);
                    command.Parameters.AddWithValue("@id_materia", item.id_materia);
                    command.Parameters.AddWithValue("@fecha", item.fecha);
                    command.Parameters.AddWithValue("@estado", item.estado);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void DeleteAsistencia(int id, int id_alumno, int id_materia, string fecha)
        {
            var query = "DELETE FROM asistencia WHERE id_asistencia = @id AND id_alumno = @id_alumno AND id_materia = @id_materia AND fecha = @fecha";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new System.Exception("No se encontró un registro con ese ID, alumno, materia o fecha.");
                    }
                }
                connection.Close();
            }
        }

        public Asistencia GetAsistencia(int id, int id_alumno, int id_materia, string fecha)
        {
            var query = "SELECT * FROM asistencia WHERE id_asistencia = @id AND id_alumno = @id_alumno AND id_materia = @id_materia AND fecha = @fecha";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var asistencia = new Asistencia();
                            asistencia.id_asistencia = reader.GetInt32(0);
                            asistencia.id_alumno = reader.GetInt32(1);
                            asistencia.id_materia = reader.GetInt32(2);
                            asistencia.fecha = reader.GetString(3);
                            asistencia.estado = reader.GetString(4);
                            return asistencia;
                        }
                        else
                        {
                            throw new System.Exception("No se encontró un registro con ese ID, alumno, materia o fecha.");
                        }
                    }
                }
            }
        }

        public List<Asistencia> GetAsistencias()
        {
            var query = "SELECT * FROM asistencia";
            var asistencias = new List<Asistencia>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var asistencia = new Asistencia();
                            asistencia.id_asistencia = reader.GetInt32(0);
                            asistencia.id_alumno = reader.GetInt32(1);
                            asistencia.id_materia = reader.GetInt32(2);
                            asistencia.fecha = reader.GetString(3);
                            asistencia.estado = reader.GetString(4);
                            asistencias.Add(asistencia);
                        }
                    }
                }
                connection.Close();
            }
            return asistencias;
        }

        public void UpdateAsistencia(Asistencia item)
        {
            var query = "UPDATE asistencia SET estado = @estado WHERE id_asistencia = @id";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", item.id_asistencia);
                    command.Parameters.AddWithValue("@estado", item.estado);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new System.Exception("No se encontró un registro con ese ID.");
                    }
                }
                connection.Close();
            }
        }

        public int GetPorcentajeAsistenciasByAlumno(int id_alumno)
        {
            var query = "SELECT COUNT(*) FROM asistencia WHERE id_alumno = @id_alumno AND estado = 'P'";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    int asistencias = Convert.ToInt32(command.ExecuteScalar());
                    query = "SELECT COUNT(*) FROM asistencia WHERE id_alumno = @id_alumno";
                    command.CommandText = query;
                    int total = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    if (total == 0)
                    {
                        return 0;
                    }
                    return (asistencias * 100) / total;
                }
            }
        }

        public int GetPorcentajeAsistenciasByMateriaAlumno(int id_materia, int id_alumno)
        {
            var queryAsistencias = "SELECT COUNT(*) FROM asistencia WHERE id_materia = @id_materia AND id_alumno = @id_alumno AND (estado = 'P' OR estado = 'T')";
            var queryTotal = "SELECT COUNT(*) FROM asistencia WHERE id_materia = @id_materia AND id_alumno = @id_alumno";

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(queryAsistencias, connection))
                {
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    int asistencias = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = queryTotal;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    int total = Convert.ToInt32(command.ExecuteScalar());

                    return total == 0 ? 0 : (asistencias * 100) / total;
                }
            }
        }

        public int GetPresentesByMateriaAlumno(int id_materia, int id_alumno)
        {
            var query = "SELECT COUNT(*) FROM asistencia WHERE id_materia = @id_materia AND id_alumno = @id_alumno AND estado = 'P'";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int GetAusentesByMateriaAlumno(int id_materia, int id_alumno)
        {
            var query = "SELECT COUNT(*) FROM asistencia WHERE id_materia = @id_materia AND id_alumno = @id_alumno AND estado = 'A'";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int GetTardesByMateriaAlumno(int id_materia, int id_alumno)
        {
            var query = "SELECT COUNT(*) FROM asistencia WHERE id_materia = @id_materia AND id_alumno = @id_alumno AND estado = 'T'";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_materia", id_materia);
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

    }
}
