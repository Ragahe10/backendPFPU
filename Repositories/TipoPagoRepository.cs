using backendPFPU.Models;
using Microsoft.Data.Sqlite;

namespace backendPFPU.Repositories
{
    public class TipoPagoRepository : ITipoPagoRepository
    {
        private string _CadenaDeConexion;
        public TipoPagoRepository(string cadenaDeConexion)
        {
            _CadenaDeConexion = cadenaDeConexion;
        }
        public void AddTipoPago(TipoPago tipoPago)
        {
            var query = "INSERT INTO tipo_pago (tipo) VALUES (@tipo)";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tipo", tipoPago.tipo);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }


        public List<TipoPago> GetAllTipoPago()
        {
            var query = "SELECT * FROM tipo_pago";
            var tipoPagos = new List<TipoPago>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoPago = new TipoPago();
                            tipoPago.id_tipo = reader.GetInt32(0);
                            tipoPago.tipo = reader.GetString(1);
                            tipoPagos.Add(tipoPago);
                        }
                    }
                }
                connection.Close();
            }
            return tipoPagos;
        }

        public TipoPago GetTipoPago(int id)
        {
            var query = "SELECT * FROM tipo_pago WHERE id_tipo = @id";
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
                            var tipoPago = new TipoPago();
                            tipoPago.id_tipo = reader.GetInt32(0);
                            tipoPago.tipo = reader.GetString(1);
                            return tipoPago;
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        void ITipoPagoRepository.DeleteTipoPago(int id)
        {
            var query = "DELETE FROM tipo_pago WHERE id_tipo = @id";
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

        void ITipoPagoRepository.UpdateTipoPago(TipoPago tipoPago)
        {
            var query = "UPDATE tipo_pago SET tipo = @tipo WHERE id_tipo = @id";
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", tipoPago.id_tipo);
                    command.Parameters.AddWithValue("@tipo", tipoPago.tipo);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
