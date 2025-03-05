using backendPFPU.Models;
using Microsoft.Data.Sqlite;

namespace backendPFPU.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private string _CadenaDeConexion;
        public PagoRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }
        public void AddPago(Pago pago)
        {
            var query = "INSERT INTO pago (monto, fecha, id_tipo, estado, id_alumno) VALUES (@monto, @fecha, @id_tipo, @estado, @id_alumno)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@monto", pago.monto);
                    command.Parameters.AddWithValue("@fecha", pago.fecha);
                    command.Parameters.AddWithValue("@id_tipo", pago.id_tipo);
                    command.Parameters.AddWithValue("@estado", pago.estado);
                    command.Parameters.AddWithValue("@id_alumno", pago.id_alumno);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void DeletePago(int id)
        {
            var query = "DELETE FROM pago WHERE id_pago = @id";
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

        public List<Pago> GetAllPago()
        {
           var query = "SELECT * FROM pago";
            var pagos = new List<Pago>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pago = new Pago();
                            pago.id_pago = reader.GetInt32(0);
                            pago.monto = reader.GetFloat(5);
                            pago.fecha = reader.GetString(2);
                            pago.id_tipo = reader.GetInt32(4);
                            pago.estado = reader.GetString(3);
                            pago.id_alumno = reader.GetInt32(1);
                            pagos.Add(pago);
                        }
                    }
                }
                connection.Close();
            }
            return pagos;
        }

        public Pago GetPago(int id)
        {
            var query = "SELECT * FROM pago WHERE id_pago = @id";
            var pago = new Pago();
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
                            pago.id_pago = reader.GetInt32(0);
                            pago.monto = reader.GetFloat(5);
                            pago.fecha = reader.GetString(2);
                            pago.id_tipo = reader.GetInt32(4);
                            pago.estado = reader.GetString(3);
                            pago.id_alumno = reader.GetInt32(1);
                        }
                    }
                }
                connection.Close();
            }
            return pago;

        }

        public List<Pago> GetPagosByAlumno(int id)
        {
            var query = "SELECT * FROM pago WHERE id_alumno = @id";
            var pagos = new List<Pago>();
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
                            var pago = new Pago();
                            pago.id_pago = reader.GetInt32(0);
                            pago.monto = reader.GetFloat(5);
                            pago.fecha = reader.GetString(2);
                            pago.id_tipo = reader.GetInt32(4);
                            pago.estado = reader.GetString(3);
                            pago.id_alumno = reader.GetInt32(1);
                            pagos.Add(pago);
                        }
                    }
                }
                connection.Close();
            }
            return pagos;
        }

        public void UpdatePago(Pago pago)
        {
            var query = "UPDATE pago SET monto = @monto, fecha = @fecha, id_tipo = @id_tipo, estado = @estado, id_alumno = @id_alumno WHERE id_pago = @id_pago";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@monto", pago.monto);
                    command.Parameters.AddWithValue("@fecha", pago.fecha);
                    command.Parameters.AddWithValue("@id_tipo", pago.id_tipo);
                    command.Parameters.AddWithValue("@estado", pago.estado);
                    command.Parameters.AddWithValue("@id_alumno", pago.id_alumno);
                    command.Parameters.AddWithValue("@id_pago", pago.id_pago);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }
    }
}
