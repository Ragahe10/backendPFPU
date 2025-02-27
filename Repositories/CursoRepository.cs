using backendPFPU.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace backendPFPU.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private string _CadenaDeConexion;
   
        public CursoRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }
        void ICursoRepository.AddCurso(Curso curso)
        {
            var query = "INSERT INTO curso (division, cupo_restante, id_anio) VALUES (@division, @cupo_restante, @id_anio)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@division", DbType.String) { Value = curso.division });
                    command.Parameters.Add(new SqliteParameter("@cupo_restante", DbType.Int32) { Value = curso.cupo_restante });
                    command.Parameters.Add(new SqliteParameter("@id_anio", DbType.Int32) { Value = curso.id_anio });
                    command.ExecuteNonQuery();
                }
            }
        }
        void ICursoRepository.DeleteCurso(int id)
        {
            var query = "DELETE FROM curso WHERE id_curso = @id";
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
        List<Curso> ICursoRepository.GetAll()
        {
            var cursos = new List<Curso>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                var query = "SELECT * FROM curso";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var curso = new Curso();
                            curso.id_curso = reader.GetInt32(0);
                            curso.division = reader.GetString(1);
                            curso.cupo_restante = reader.GetInt32(2);
                            curso.id_anio = reader.GetInt32(3);
                            cursos.Add(curso);
                        }
                    }
                }
                connection.Close();
                return cursos;
            }
        }

        Curso ICursoRepository.GetCurso(int id)
        {
            Curso curso = null;
            var query = "SELECT * FROM curso WHERE id_curso = @id";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@id", DbType.Int32) { Value = id });
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            curso = new Curso();
                            curso.id_curso = reader.GetInt32(0);
                            curso.division = reader.GetString(1);
                            curso.cupo_restante = reader.GetInt32(2);
                            curso.id_anio = reader.GetInt32(3);
                        }
                    }
                }
                connection.Close();
            }
            return curso;
        }

        void ICursoRepository.UpdateCurso(Curso curso)
        {
            var query = "UPDATE curso SET division = @division, cupo_restante = @cupo_restante, id_anio = @id_anio WHERE id_curso = @id_curso";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.Add(new SqliteParameter("@division", DbType.String) { Value = curso.division });
                    command.Parameters.Add(new SqliteParameter("@cupo_restante", DbType.Int32) { Value = curso.cupo_restante });
                    command.Parameters.Add(new SqliteParameter("@id_anio", DbType.Int32) { Value = curso.id_anio });
                    command.Parameters.Add(new SqliteParameter("@id_curso", DbType.Int32) { Value = curso.id_curso });
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró un registro con ese ID.");
                    }
                }
            }
        }
    }
}
