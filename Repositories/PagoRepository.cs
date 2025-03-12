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
            var queryInsertPago = "INSERT INTO pago (monto, fecha, id_alumno, id_deuda) VALUES (@monto, @fecha, @id_alumno, @id_deuda)";
            var queryDeuda = "SELECT monto FROM deuda WHERE id_deuda = @id_deuda";
            var queryUpdateDeuda = "UPDATE deuda SET monto = @nuevoMonto, estado = @estado WHERE id_deuda = @id_deuda";

            float montoDeuda = 0;
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(queryDeuda, connection))
                {
                    command.Parameters.AddWithValue("@id_deuda", pago.id_deuda);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            montoDeuda = reader.GetFloat(0);
                        }
                        else
                        {
                            throw new Exception("No se encontró la deuda especificada.");
                        }
                    }
                }
            }

            if (pago.monto > montoDeuda)
            {
                throw new Exception("El monto del pago no puede ser mayor que la deuda.");
            }

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqliteCommand(queryInsertPago, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@monto", pago.monto);
                            command.Parameters.AddWithValue("@fecha", pago.fecha);
                          
                            command.Parameters.AddWithValue("@id_alumno", pago.id_alumno);
                            command.Parameters.AddWithValue("@id_deuda", pago.id_deuda);
                            command.ExecuteNonQuery();
                        }

                        float nuevoMonto = montoDeuda - pago.monto;
                        string estado = nuevoMonto == 0 ? "Pagado" : "Pendiente";

                        using (var command = new SqliteCommand(queryUpdateDeuda, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@nuevoMonto", nuevoMonto);
                            command.Parameters.AddWithValue("@estado", estado);
                            command.Parameters.AddWithValue("@id_deuda", pago.id_deuda);
                            command.ExecuteNonQuery();
                        }

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


        public void DeletePago(int id)
        {
            var querySelectPago = "SELECT monto, id_deuda FROM pago WHERE id_pago = @id";
            var queryDeletePago = "DELETE FROM pago WHERE id_pago = @id";
            var queryUpdateDeuda = "UPDATE deuda SET monto = monto + @monto, estado = 'Pendiente' WHERE id_deuda = @id_deuda";

            float montoPago = 0;
            int idDeuda = 0;

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(querySelectPago, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            montoPago = reader.GetFloat(0);
                            idDeuda = reader.GetInt32(1);
                        }
                        else
                        {
                            throw new Exception("No se encontró el pago especificado.");
                        }
                    }
                }
            }

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqliteCommand(queryDeletePago, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }

                        using (var command = new SqliteCommand(queryUpdateDeuda, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@monto", montoPago);
                            command.Parameters.AddWithValue("@id_deuda", idDeuda);
                            command.ExecuteNonQuery();
                        }

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
                            pago.monto = reader.GetFloat(3);
                            pago.fecha = reader.GetString(2);
                            
                            pago.id_deuda = reader.GetInt32(4);
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
                            pago.monto = reader.GetFloat(3);
                            pago.fecha = reader.GetString(2);

                            pago.id_deuda = reader.GetInt32(4);
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
                            pago.monto = reader.GetFloat(3);
                            pago.fecha = reader.GetString(2);

                            pago.id_deuda = reader.GetInt32(4);
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
            var queryPago = "SELECT monto, id_deuda FROM pago WHERE id_pago = @id_pago";
            var queryDeuda = "SELECT monto FROM deuda WHERE id_deuda = @id_deuda";
            var queryUpdatePago = "UPDATE pago SET monto = @monto, fecha = @fecha, id_alumno = @id_alumno WHERE id_pago = @id_pago";
            var queryUpdateDeuda = "UPDATE deuda SET monto = @nuevo_monto, estado = @estado WHERE id_deuda = @id_deuda";

            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();

                float montoAntiguoPago = 0;
                float montoDeuda = 0;
                int idDeuda = 0;

                // Obtener el monto antiguo del pago y la deuda asociada
                using (var command = new SqliteCommand(queryPago, connection))
                {
                    command.Parameters.AddWithValue("@id_pago", pago.id_pago);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            montoAntiguoPago = reader.GetFloat(0);
                            idDeuda = reader.GetInt32(1);
                        }
                    }
                }

                using (var command = new SqliteCommand(queryDeuda, connection))
                {
                    command.Parameters.AddWithValue("@id_deuda", idDeuda);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            montoDeuda = reader.GetFloat(0);
                        }
                    }
                }

                // Calcular el nuevo monto de la deuda
                float nuevoMontoDeuda = montoDeuda + montoAntiguoPago - pago.monto;

                if (pago.monto > (montoDeuda + montoAntiguoPago))
                {
                    throw new Exception("El monto del pago no puede ser mayor a la deuda actual.");
                }

                // Actualizar el pago
                using (var command = new SqliteCommand(queryUpdatePago, connection))
                {
                    command.Parameters.AddWithValue("@monto", pago.monto);
                    command.Parameters.AddWithValue("@fecha", pago.fecha);
                    command.Parameters.AddWithValue("@id_alumno", pago.id_alumno);
                    command.Parameters.AddWithValue("@id_pago", pago.id_pago);
                    command.ExecuteNonQuery();
                }

                // Determinar el nuevo estado de la deuda
                string nuevoEstado = nuevoMontoDeuda == 0 ? "Pagado" : "Pendiente";

                // Actualizar la deuda
                using (var command = new SqliteCommand(queryUpdateDeuda, connection))
                {
                    command.Parameters.AddWithValue("@nuevo_monto", nuevoMontoDeuda);
                    command.Parameters.AddWithValue("@estado", nuevoEstado);
                    command.Parameters.AddWithValue("@id_deuda", idDeuda);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

    }
}
