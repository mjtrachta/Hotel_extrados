using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Hotel_extrados;

namespace DemoDapper
{
    public class Consulta
    {
        
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();


        // LOGIN
        public int Login(string username, string password)
        {
            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                string sql = "SELECT * FROM Usuarios WHERE mail = @Username AND pass = @Password";
                var parameters = new { Username = username, Password = password };
                Usuario user = db.QueryFirstOrDefault<Usuario>(sql, parameters);

                if (user != null && user.id_rol == 6)
                {
                    return 1;
                }
                else if (user != null && user.id_rol == 7)
                {
                    return 2;
                }
                else
                {
                    return 4;
                }
            }
        }


        // ADMIN 1
        public int CargarHabitacion(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "INSERT INTO [Habitaciones]([piso],[numero_habitacion],[id_tipo],[camas],[cochera],[precio],[tv],[desayuno],[id_estado],[servicio_habitacion],[hidromasajes]) " +
                                  "VALUES(@piso, @numero_habitacion, @id_tipo, @camas, @cochera, @precio, @tv, @desayuno, @id_estado, @servicio_habitacion, @hidromasajes)";

                return conexion.Execute(sentenciaSql, new { id_habitacion = habitacion.id_habitacion, piso = habitacion.piso, numero_habitacion = habitacion.numero_habitacion, id_tipo = habitacion.id_tipo, camas = habitacion.camas, cochera = habitacion.cochera, precio = habitacion.precio, tv = habitacion.tv, desayuno = habitacion.desayuno, id_estado = habitacion.id_estado, servicio_habitacion = habitacion.servicio_habitacion, hidromasajes = habitacion.hidromasajes }); ;
            }
        }
        //ADMIN 2
        public int ObtenerHabitacionesDisponiblesParametro(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<int>("SELECT id_estado FROM habitaciones WHERE id_habitacion = @id_habitacion", new { id_habitacion = habitacion.id_habitacion });
                return dias;
            }
        }
        public int ActualizarEstadoDisponibleALimpieza(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '2' THEN '3' " +
                    "WHEN '1' THEN '1' " +
                    "WHEN '3' THEN '3' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }
        public int ActualizarEstadoDisponibleARenovacion(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '2' THEN '4' " +
                    "WHEN '1' THEN '1' " +
                    "WHEN '3' THEN '3' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }
        public int ActualizarEstadoOcupadoALimpieza(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '1' THEN '3' " +
                    "WHEN '2' THEN '2' " +
                    "WHEN '3' THEN '3' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }
        public int ActualizarEstadoOcupadoARenovacion(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '1' THEN '4' " +
                    "WHEN '2' THEN '2' " +
                    "WHEN '3' THEN '3' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });
            }
        }
        public int ActualizarBandera(Reserva actualizarbandera)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE ClienteXHabitacion " +
                    "SET bandera = CASE bandera " +
                    "WHEN '1' THEN '0' " +
                    "WHEN '0' THEN '0' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion AND " +
                    "GETDATE() >= (fecha_desde) AND " +
                    "GETDATE() <= (fecha_hasta)";
                return conexion.Execute(comando, new { id_habitacion = actualizarbandera.id_habitacion });

            }
        }
        // ADMIN 3
        public int EstadoHabitacionHoy(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<int>("SELECT COUNT (*) AS registros " +
                    "FROM ClienteXHabitacion c " +
                    "WHERE c.id_habitacion = @id_habitacion AND " +
                    "GETDATE() >= (fecha_desde) AND " +
                    "GETDATE() <= (fecha_hasta)", new { id_habitacion = habitacion.id_habitacion });

                return dias;
            }
        }
        public long Cliente(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<long>("SELECT cuil_cliente " +
                    "FROM ClienteXHabitacion c " +
                    "WHERE c.id_habitacion = @id_habitacion AND " +
                    "GETDATE() >= (fecha_desde) AND " +
                    "GETDATE() <= (fecha_hasta)", new { id_habitacion = habitacion.id_habitacion });

                return dias;
            }
        }
        public int ActualizarEstadoLimpiezaADisponible(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '3' THEN '2' " +
                    "WHEN '1' THEN '1' " +
                    "WHEN '2' THEN '2' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }
        public int ActualizarEstadoLimpiezaAOcupado(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '3' THEN '1' " +
                    "WHEN '1' THEN '1' " +
                    "WHEN '2' THEN '2' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }
        //ADMIN 4
        public int ActualizarEstadoRenovacionADisponible(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '4' THEN '2'  " + "" +
                    "WHEN '1' THEN '1'  " + "" +
                    "WHEN '2' THEN '2'  " + "" +
                    "WHEN '3' THEN '3'  " + "" +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }
        public IEnumerable<Habitacion> ObtenerHabitacionesRenovacion()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT h.id_habitacion, e.descripcion_estado, t.descripcion,h.piso, h.numero_habitacion, h.camas, h.cochera, h.precio, h.tv, h.desayuno, h.servicio_habitacion, h.hidromasajes " +
                                                                "FROM habitaciones h " +
                                                                "JOIN Estado_habitaciones e " +
                                                                "ON h.id_estado = e.id_estado " +
                                                                "JOIN Tipo_habitaciones t " +
                                                                "ON h.id_tipo = t.id_tipo " +
                                                                "WHERE h.id_estado = 4 ").ToList();
                return habitaciones;

            }
        }  //PRINT


        // AT CLIENTE 1
        public IEnumerable<Habitacion> ObtenerHabitacionesComunes()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT * " +
                                                              "FROM habitaciones h " +
                                                              "JOIN Estado_habitaciones e " +
                                                              "ON h.id_estado = e.id_estado " +
                                                              "WHERE h.id_tipo = 1").ToList();
                return habitaciones;
            }
        }
        public IEnumerable<Habitacion> ObtenerHabitacionesVip()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT * " +
                                                                  "FROM habitaciones h " +
                                                                  "JOIN Estado_habitaciones e " +
                                                                  "ON h.id_estado = e.id_estado " +
                                                                  "WHERE h.id_tipo = 2").ToList();
                return habitaciones;

            }
        }
        public DateTime DiasOcupados(int id_habitacion) // si devuelve 1 esta ocupado si es 0 disponible
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<DateTime>("SELECT fecha_hasta " +
                                                   "FROM ClienteXHabitacion " +
                                                   "WHERE  id_habitacion = @id_habitacion AND " +
                                                   "GETDATE() >= (fecha_desde) AND  " +
                                                   "GETDATE() <= (fecha_hasta)",
                                                    new { id_habitacion = id_habitacion });
                return dias;
            }
        }
        // AT CLIENTE 2
        public IEnumerable<Habitacion> ObtenerHabitacionesDisponibles()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT h.id_habitacion, e.descripcion_estado, t.descripcion,h.piso, h.numero_habitacion, h.camas, h.cochera, h.precio, h.tv, h.desayuno, h.servicio_habitacion, h.hidromasajes " +
                                                                "FROM habitaciones h " +
                                                                "JOIN Estado_habitaciones e " +
                                                                "ON h.id_estado = e.id_estado " +
                                                                "JOIN Tipo_habitaciones t " +
                                                                "ON h.id_tipo = t.id_tipo " +
                                                                "WHERE h.id_estado = 2").ToList();
                return habitaciones;

            }
        }
        // AT CLIENTE 3
        public int CargarCliente(Cliente cliente)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "INSERT INTO [Clientes]([cuil],[nombre],[apellido],[mail]) " +
                                      "VALUES(@cuil, @nombre, @apellido, @mail)";

                return conexion.Execute(sentenciaSql, new { cuil = cliente.cuil, nombre = cliente.nombre, apellido = cliente.apellido, mail = cliente.mail }); ;
            }
        }
        // AT CLIENTE 4
        public int CargarReserva(Reserva reserva)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "INSERT INTO [ClienteXHabitacion]([cuil_cliente],[id_habitacion],[fecha_desde],[fecha_hasta],[bandera]) " +
                                  "VALUES(@cuil_cliente, @id_habitacion, @fecha_desde, @fecha_hasta, @bandera)";

                return conexion.Execute(sentenciaSql, new { cuil_cliente = reserva.cuil_cliente, id_habitacion = reserva.id_habitacion, fecha_desde = reserva.fecha_desde, fecha_hasta = reserva.fecha_hasta, bandera = reserva.bandera }); ;
            }
        }
        public int ColisionDeFechas(Reserva habitacion, DateTime fecha1)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<int>("SELECT COUNT (*) AS registros " +
                    "FROM ClienteXHabitacion c " +
                    "WHERE c.id_habitacion = @id_habitacion AND " +
                    "@fecha1 >= (fecha_desde) AND " +
                    "@fecha1 <= (fecha_hasta)", new { id_habitacion = habitacion.id_habitacion, fecha1 = fecha1 });

                return dias;
            }
        }
        //AT CLIENTE 5
        public IEnumerable<Habitacion> ObtenerHabitacionesDisponiblesLimpieza()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT h.id_habitacion, e.descripcion_estado, t.descripcion,h.piso, h.numero_habitacion, h.camas, h.cochera, h.precio, h.tv, h.desayuno, h.servicio_habitacion, h.hidromasajes " +
                                                                "FROM habitaciones h " +
                                                                "JOIN Estado_habitaciones e " +
                                                                "ON h.id_estado = e.id_estado " +
                                                                "JOIN Tipo_habitaciones t " +
                                                                "ON h.id_tipo = t.id_tipo " +
                                                                "WHERE h.id_estado = 2 or h.id_estado = 3").ToList();
                return habitaciones;

            }
        }
        public int ActualizarEstado(Habitacion actualizarEstado)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones " +
                              "SET id_estado = CASE id_estado " +
                              "WHEN '2' THEN '3'  " +
                              "WHEN '3' THEN '2'  " +
                              "ELSE NULL  " +
                              "END " +
                              "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstado.id_habitacion });

            }
        }


        // TEST
        public int ActualizarEstadoLimpiezaAlEstadoAnterior(Habitacion actualizarEstadoRAD)
        {

            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "" /*"UPDATE Habitaciones " +
                    "SET id_estado = CASE id_estado " +
                    "WHEN '2' THEN '4' " +
                    "WHEN '1' THEN '1' " +
                    "WHEN '3' THEN '3' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion"*/;
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }

        }

        public IEnumerable<Habitacion> ObtenerHabitacionesLimpieza()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT h.id_habitacion, h.piso, h.numero_habitacion " +
                                                                "FROM habitaciones h " +
                                                                "WHERE h.id_estado = 3").ToList();
                return habitaciones;

            }
        }

        public int ExisteHabitacionLimpia(int habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "SELECT COUNT(*)  " +
                    "FROM Habitaciones " +
                    "WHERE EXISTS (SELECT id_estado " +
                    "              FROM Habitaciones " +
                    "              WHERE id_habitacion = @id_habitacion) " +
                    "              and id_habitacion = @id_habitacion and id_estado = 3;";
                return conexion.QueryFirstOrDefault<int>(comando, new { id_habitacion = habitacion });

            }
        }

        public int ExisteHabitacionRenovacion(int habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "SELECT COUNT(*)  " +
                    "FROM Habitaciones " +
                    "WHERE EXISTS (SELECT id_habitacion " +
                    "              FROM Habitaciones " +
                    "              WHERE id_habitacion = @id_habitacion) " +
                    "              and id_habitacion = @id_habitacion and id_estado = 4;";
                return conexion.QueryFirstOrDefault<int>(comando, new { id_habitacion = habitacion });

            }
        }



    }
}
