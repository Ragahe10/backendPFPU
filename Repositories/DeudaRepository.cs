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

        public GraficoEstadosDePagoAdmin GetGraficoEstadosDePagoAdmin()
        {
            var query = @"
        SELECT estado, COUNT(*) as cantidad FROM (
            SELECT 
                id_alumno,
                CASE 
                    WHEN SUM(CASE WHEN estado = 'Vencido' THEN 1 ELSE 0 END) > 0 THEN 'Deuda Vencida'
                    WHEN SUM(CASE WHEN estado = 'Pendiente' THEN 1 ELSE 0 END) > 0 THEN 'Deuda Pendiente'
                    ELSE 'Al Día'
                END AS estado
            FROM deuda
            GROUP BY id_alumno
        ) AS estados_agrupados
        GROUP BY estado;
    ";

            var grafico = new GraficoEstadosDePagoAdmin();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var estados = new List<string>();
                        var cantidades = new List<int>();
                        while (reader.Read())
                        {
                            estados.Add(reader.GetString(0));
                            cantidades.Add(reader.GetInt32(1));
                        }
                        grafico.labels = estados.ToArray();
                        grafico.data = cantidades.ToArray();
                    }
                }
                connection.Close();
            }
            return grafico;
        }

        public List<ActividadRecienteAdmin> GetUltimasActividades()
        {
            var query = @"
        SELECT 'nota' AS tipo, 
               n.fecha AS fecha, 
               CONCAT('Nota registrada: ', n.nota, ' para ', a.nombre, ' ', a.apellido, ' en ', m.materia) AS descripcion
        FROM nota n
        JOIN alumno al ON n.id_alumno = al.id_alumno
        JOIN usuario a ON al.id_alumno = a.id_usuario
        JOIN materia m ON n.id_materia = m.id_materia
        UNION ALL
        SELECT 'asistencia' AS tipo,
               a.fecha AS fecha,
               CONCAT('Asistencia registrada: ', 
                      CASE a.estado 
                        WHEN 'P' THEN 'Presente' 
                        WHEN 'T' THEN 'Tarde' 
                        WHEN 'A' THEN 'Ausente' 
                      END, 
                      ' para ', u.nombre, ' ', u.apellido, ' en ', m.materia) AS descripcion
        FROM asistencia a
        JOIN alumno al ON a.id_alumno = al.id_alumno
        JOIN usuario u ON al.id_alumno = u.id_usuario
        JOIN materia m ON a.id_materia = m.id_materia
        UNION ALL
        SELECT 'pago' AS tipo,
               p.fecha AS fecha,
               CONCAT('Pago registrado: $', p.monto, ' de ', u.nombre, ' ', u.apellido) AS descripcion
        FROM pago p
        JOIN alumno al ON p.id_alumno = al.id_alumno
        JOIN usuario u ON al.id_alumno = u.id_usuario
        ORDER BY fecha DESC
        LIMIT 10;
    ";

            var actividades = new List<ActividadRecienteAdmin>();
            using (var connection = new SqliteConnection(_CadenaDeConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            actividades.Add(new ActividadRecienteAdmin
                            {
                                Tipo = reader.GetString(0),
                                Fecha = reader.GetDateTime(1),
                                Descripcion = reader.GetString(2)
                            });
                        }
                    }
                }
                connection.Close();
            }
            return actividades;
        }

        // Solo muestro carga de notas o cargas de asistencias de un docente
        public List<ActividadRecienteDocente> GetUltimasActividadesDocente(int id)
        {
            var query = @"
        SELECT 'nota' AS tipo, 
               n.fecha AS fecha, 
               CONCAT('Nota registrada: ', n.nota, ' para ', a.nombre, ' ', a.apellido, ' en ', m.materia) AS descripcion
        FROM nota n
        JOIN alumno al ON n.id_alumno = al.id_alumno
        JOIN usuario a ON al.id_alumno = a.id_usuario
        JOIN materia m ON n.id_materia = m.id_materia
        JOIN docente d ON m.id_docente = d.id_docente
        WHERE d.id_docente = @id
        UNION ALL
        SELECT 'asistencia' AS tipo,
               a.fecha AS fecha,
               CONCAT('Asistencia registrada: ', 
                      CASE a.estado 
                        WHEN 'P' THEN 'Presente' 
                        WHEN 'T' THEN 'Tarde' 
                        WHEN 'A' THEN 'Ausente' 
                      END, 
                      ' para ', u.nombre, ' ', u.apellido, ' en ', m.materia) AS descripcion
        FROM asistencia a
        JOIN alumno al ON a.id_alumno = al.id_alumno
        JOIN usuario u ON al.id_alumno = u.id_usuario
        JOIN materia m ON a.id_materia = m.id_materia
        JOIN docente d ON m.id_docente = d.id_docente
        WHERE d.id_docente = @id
        ORDER BY fecha DESC
        LIMIT 10;
    ";
            var actividades = new List<ActividadRecienteDocente>();
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
                            actividades.Add(new ActividadRecienteDocente
                            {
                                Tipo = reader.GetString(0),
                                Fecha = reader.GetDateTime(1),
                                Descripcion = reader.GetString(2)
                            });
                        }
                    }
                }
                connection.Close();
            }
            return actividades;
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

        public List<Deuda> GetDeudasPendientesByAlumno(int id_alumno)
        {
            var query = "SELECT * FROM deuda WHERE id_alumno = @id_alumno AND estado = 'Pendiente'";
            var deudas = new List<Deuda>();
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

        public List<ActividadRecienteAlumno> GetUltimasActividadesAlumno(int id)
        {
            var query = @"
        SELECT 'nota' AS tipo, 
               n.fecha AS fecha, 
               CONCAT('Nota registrada: ', n.nota, ' en ', m.materia) AS descripcion
        FROM nota n
        JOIN alumno al ON n.id_alumno = al.id_alumno
        JOIN usuario a ON al.id_alumno = a.id_usuario
        JOIN materia m ON n.id_materia = m.id_materia
        WHERE al.id_alumno = @id
        UNION ALL
        SELECT 'asistencia' AS tipo,
               a.fecha AS fecha,
               CONCAT('Asistencia registrada: ', 
                      CASE a.estado 
                        WHEN 'P' THEN 'Presente' 
                        WHEN 'T' THEN 'Tarde' 
                        WHEN 'A' THEN 'Ausente' 
                      END, 
                       ' en ', m.materia) AS descripcion
        FROM asistencia a
        JOIN alumno al ON a.id_alumno = al.id_alumno
        JOIN usuario u ON al.id_alumno = u.id_usuario
        JOIN materia m ON a.id_materia = m.id_materia
        WHERE al.id_alumno = @id
        UNION ALL
        SELECT 'pago' AS tipo,
               p.fecha AS fecha,
               CONCAT('Pago registrado: $', p.monto) AS descripcion
        FROM pago p
        JOIN alumno al ON p.id_alumno = al.id_alumno
        JOIN usuario u ON al.id_alumno = u.id_usuario
        WHERE al.id_alumno = @id
        ORDER BY fecha DESC
        LIMIT 10;
    ";
            var actividades = new List<ActividadRecienteAlumno>();
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
                            actividades.Add(new ActividadRecienteAlumno
                            {
                                Tipo = reader.GetString(0),
                                Fecha = reader.GetDateTime(1),
                                Descripcion = reader.GetString(2)
                            });
                        }
                    }
                }
                connection.Close();
            }
            return actividades;



        }
    }
}
