using backendPFPU.Helpers;
using backendPFPU.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace backendPFPU.Repositories
{
    public class AnioRepository : IAnioRepository
    {
        private string _CadenaDeConexion;

        public AnioRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
         
        }

        void IAnioRepository.DeleteAnio(int id)
        {
            var query = "DELETE FROM anio WHERE id_anio = @id";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id", DbType.Int32) { Value = id });
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró un registro con ese ID.");
                    }
                }
            }
        }

        List<Anio> IAnioRepository.GetAll()
        {
            var anios = new List<Anio>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
               connection.Open();
                var query = "SELECT * FROM anio";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var anio = new Anio();
                            anio.id_anio = reader.GetInt32(0);
                            anio.anio = reader.GetString(1);
                            anios.Add(anio);
                        }
                    }
                }
                connection.Close();
            }
            return anios;

        }

        Anio IAnioRepository.GetById(int id)
        {
            var anio = new Anio();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                var query = "SELECT * FROM anio WHERE id_anio = @id";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            anio.id_anio = reader.GetInt32(0);
                            anio.anio = reader.GetString(1);
                        }
                    }
                }
                connection.Close();
            }
            return anio;
        }

        void IAnioRepository.PostAnio(Anio anio)
        {
            var query = "INSERT INTO anio (anio) VALUES (@anio)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@anio", anio.anio);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }

        void IAnioRepository.PutAnio(Anio anio)
        {
            var query = "UPDATE anio SET anio = @anio WHERE id_anio = @id";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@anio", anio.anio);
                    command.Parameters.AddWithValue("@id", anio.id_anio);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        string IAnioRepository.GetAnioByAlumno(int id_alumno)
        {
            string anio = "";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                var query = "SELECT CONCAT(anio, ' ' , division) FROM anio INNER JOIN curso USING(id_anio) INNER JOIN alumno USING(id_curso) WHERE id_alumno = @id_alumno";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_alumno", id_alumno);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            anio = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            return anio;
        }


    }
}
