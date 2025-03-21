using backendPFPU.Models;
using Microsoft.Data.Sqlite;

namespace backendPFPU.Repositories
{
    public class DeudaRepository : IDeudaRepository
    {
        private string _CadenaDeConexion;
        public DeudaRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }

        public void AddDeuda(Deuda deuda)
        {
            var query = "INSERT INTO deuda (monto, fecha_vencimiento, id_alumno, estado, id_tipo) VALUES (@monto, @fecha_vencimiento, @id_alumno, @estado, @id_tipo)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@monto", deuda.monto);
                    command.Parameters.AddWithValue("@fecha_vencimiento", deuda.fecha_vencimiento);
                    command.Parameters.AddWithValue("@id_alumno", deuda.id_alumno);
                    command.Parameters.AddWithValue("@id_tipo", deuda.id_tipo);
                    command.Parameters.AddWithValue("@estado", deuda.estado);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void DeleteDeuda(int id)
        {
            var query = "DELETE FROM deuda WHERE id_deuda = @id";
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

        public List<Deuda> GetAllDeuda()
        {
            var query = "SELECT * FROM deuda";
            var deudas = new List<Deuda>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var deuda = new Deuda();
                            deuda.id_deuda = reader.GetInt32(0);
                          
                            deuda.fecha_vencimiento = reader.GetString(1);
                            deuda.id_alumno = reader.GetInt32(2);
                            deuda.estado = reader.GetString(3);
                            deuda.monto = reader.GetFloat(4);
                            deuda.id_tipo = reader.GetInt32(5);

                            deudas.Add(deuda);
                        }
                    }
                }
                connection.Close();
            }
            return deudas;
        }

        public Deuda GetDeuda(int id)
        {
            var query = "SELECT * FROM deuda WHERE id_deuda = @id";
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
                            var deuda = new Deuda();
                            deuda.id_deuda = reader.GetInt32(0);

                            deuda.fecha_vencimiento = reader.GetString(1);
                            deuda.id_alumno = reader.GetInt32(2);
                            deuda.estado = reader.GetString(3);
                            deuda.monto = reader.GetFloat(4);
                            deuda.id_tipo = reader.GetInt32(5);
                            return deuda;
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public void UpdateDeuda(Deuda deuda)
        {
            var query = "UPDATE deuda SET monto = @monto, fecha_vencimiento = @fecha_vencimiento, estado = @estado ,id_alumno = @id_alumno, id_tipo = @id_tipo WHERE id_deuda = @id_deuda";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@monto", deuda.monto);
                    command.Parameters.AddWithValue("@fecha_vencimiento", deuda.fecha_vencimiento);
                    command.Parameters.AddWithValue("@id_alumno", deuda.id_alumno);
                    command.Parameters.AddWithValue("@id_tipo", deuda.id_tipo);
                    command.Parameters.AddWithValue("@estado", deuda.estado);
                    command.Parameters.AddWithValue("@id_deuda", deuda.id_deuda);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public int GetCantidadAlumnosConDeuda()
        {
            var query = "SELECT COUNT(DISTINCT id_alumno) FROM deuda WHERE monto > 0";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    return (int)(long)command.ExecuteScalar();
                }
            }
        }

        public List<Deuda> GetDeudasByAlumno(int id)
        {
            var query = "SELECT * FROM deuda WHERE id_alumno = @id";
            var deudas = new List<Deuda>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var deuda = new Deuda();
                            deuda.id_deuda = reader.GetInt32(0);
                        
                            deuda.fecha_vencimiento = reader.GetString(1);
                            deuda.id_alumno = reader.GetInt32(2);
                            deuda.estado = reader.GetString(3);
                            deuda.monto = reader.GetFloat(4);
                            deuda.id_tipo = reader.GetInt32(5);
                            deudas.Add(deuda);
                        }
                    }
                }
                connection.Close();
            }
            return deudas;
        }

        public List<Deuda> ObtenerDeudasVencidas()
        {
            var query = "SELECT * FROM deuda WHERE estado = 'Vencido'";
            var deudas = new List<Deuda>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var deuda = new Deuda();
                            deuda.id_deuda = reader.GetInt32(0);
                            deuda.fecha_vencimiento = reader.GetString(1);
                            deuda.id_alumno = reader.GetInt32(2);
                            deuda.estado = reader.GetString(3);
                            deuda.monto = reader.GetFloat(4);
                            deuda.id_tipo = reader.GetInt32(5);
                            deudas.Add(deuda);
                        }
                    }
                }
                connection.Close();
            }
            return deudas;
        }

        public Deuda GetResumenDeudaByAlumno(int id)
        {
            var query = @"
        WITH deuda_vencida AS (
            SELECT fecha_vencimiento, 'Vencido' AS estado
            FROM deuda
            WHERE id_alumno = @id AND fecha_vencimiento < DATE('now') AND monto > 0
            ORDER BY fecha_vencimiento ASC
            LIMIT 1
        ),
        deuda_no_vencida AS (
            SELECT fecha_vencimiento, 'Pendiente' AS estado
            FROM deuda
            WHERE id_alumno = @id AND fecha_vencimiento >= DATE('now') AND monto > 0
            ORDER BY fecha_vencimiento ASC
            LIMIT 1
        )
        SELECT 
            COALESCE(SUM(monto), 0) AS total_deuda,
            CASE 
                WHEN EXISTS (SELECT 1 FROM deuda_vencida) THEN (SELECT fecha_vencimiento FROM deuda_vencida)
                ELSE (SELECT fecha_vencimiento FROM deuda_no_vencida)
            END AS fecha_vencimiento,
            CASE 
                WHEN EXISTS (SELECT 1 FROM deuda_vencida) THEN 'Vencido'
                WHEN EXISTS (SELECT 1 FROM deuda_no_vencida) THEN 'Pendiente'
                ELSE 'Sin Deuda'
            END AS estado
        FROM deuda
        WHERE id_alumno = @id AND monto > 0";

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
                            return new Deuda
                            {
                                id_deuda = id,
                                monto = reader.IsDBNull(0) ? 0 : reader.GetFloat(0),
                                fecha_vencimiento = reader.IsDBNull(1) ? null : reader.GetString(1),
                                estado = reader.IsDBNull(2) ? "Sin Deuda" : reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return null; // No hay deudas registradas
        }



    }
}
