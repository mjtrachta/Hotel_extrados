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
        public IEnumerable<Habitacion> ObtenerHabitacionesComunes()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT e.descripcion_estado, h.piso, h.numero_habitacion, h.camas, h.cochera, h.precio, h.tv, h.desayuno " +
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
                var habitaciones = conexion.Query<Habitacion>("SELECT e.descripcion_estado, h.piso, h.numero_habitacion, h.camas ,h.cochera, h.precio, h.servicio_habitacion, h.hidromasajes " +
                                                                  "FROM habitaciones h " +
                                                                  "JOIN Estado_habitaciones e " +
                                                                  "ON h.id_estado = e.id_estado " +
                                                                  "WHERE h.id_tipo = 2").ToList();
                return habitaciones;
                
            }
        }

        public IEnumerable<Habitacion> ObtenerHabitacionesDisponibles()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT h.id_habitacion, e.descripcion_estado, t.descripcion,h.piso, h.numero_habitacion, h.camas, h.cochera, h.precio, h.tv, h.desayuno, h.servicio_habitacion, h.hidromasajes "+
                                                                "FROM habitaciones h "+
                                                                "JOIN Estado_habitaciones e "+
                                                                "ON h.id_estado = e.id_estado "+
                                                                "JOIN Tipo_habitaciones t "+
                                                                "ON h.id_tipo = t.id_tipo "+
                                                                "WHERE h.id_estado = 2").ToList();
                return habitaciones;

            }
        }

        public int ObtenerHabitacionesDisponiblesParametro(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<int>("SELECT id_estado FROM habitaciones WHERE id_habitacion = @id_habitacion", new { id_habitacion = habitacion.id_habitacion });
                return dias;
            }
        }

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
        }
        //Parametros
        public int CargarCliente(Cliente cliente)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "INSERT INTO [Clientes]([cuil],[nombre],[apellido],[mail]) " +
                                      "VALUES(@cuil, @nombre, @apellido, @mail)";

                return conexion.Execute(sentenciaSql, new { cuil = cliente.cuil, nombre = cliente.nombre, apellido = cliente.apellido, mail = cliente.mail });;
            }
        }

        public int CargarReserva(Reserva reserva)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "INSERT INTO [ClienteXHabitacion]([cuil_cliente],[id_habitacion],[fecha_desde],[fecha_hasta]) " +
                                  "VALUES(@cuil_cliente, @id_habitacion, @fecha_desde, @fecha_hasta)";

                return conexion.Execute(sentenciaSql, new { cuil_cliente = reserva.cuil_cliente, id_habitacion = reserva.id_habitacion, fecha_desde = reserva.fecha_desde, fecha_hasta = reserva.fecha_hasta }); ;
            }
        }

        public int ActualizarEstado(Habitacion actualizarEstado)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones "+
                              " SET id_estado = CASE id_estado "+
                              "WHEN '2' THEN '3'  "+
                              "WHEN '3' THEN '2'  "+
                              "ELSE NULL  "+
                              "END "+
                              "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstado.id_habitacion });
                
            }
        }

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

        public IEnumerable<Reserva> DiasOcupados()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.Query<Reserva>("SELECT cuil_cliente, id_habitacion, fecha_desde, DATEDIFF(DAY, fecha_desde, fecha_hasta) AS 'dias_ocupados' \r\nFROM ClienteXHabitacion\r\nWHERE cuil_cliente = 20422895460 and id_habitacion = 2 and fecha_desde = '02-02-2023'").ToList();
                return dias;
            }
        }

   
        public IEnumerable<Reserva> EstadoHabitacionHoy(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.Query<Reserva>("SELECT CASE COUNT (*) " +
                    "WHEN 0 THEN 'False' " +
                    "WHEN 1 THEN 'True' " +
                    "ELSE 'ERROR' " +
                    "END AS registros " +
                    "FROM ClienteXHabitacion c " +
                    "WHERE c.id_habitacion = @id_habitacion AND " +
                    "GETDATE() >= (fecha_desde) AND " +
                    "GETDATE() <= (fecha_hasta)", new { id_habitacion = habitacion.id_habitacion }) ;
                return dias;
            }
        }



        public IEnumerable<Reserva> EstadoHabitacionHoy2(int id_habitacion)
        {

            var query = "SELECT CASE COUNT (*) " +
                    "WHEN 0 THEN 'False' " +
                    "WHEN 1 THEN 'True' " +
                    "ELSE 'ERROR' " +
                    "END AS registros " +
                    "FROM ClienteXHabitacion c " +
                    "WHERE c.id_habitacion = @id_habitacion AND " +
                    "GETDATE() >= (fecha_desde) AND " +
                    "GETDATE() <= (fecha_hasta)";

            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var emp = conexion.Query<Reserva>(query, new { id_habitacion });
                return emp;
            }
        }
        
/*
        public int EstadoHabitacionHoy(Reserva reserva)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "SELECT CASE COUNT (*)" +
                    "WHEN 0 THEN 'False'" +
                    "WHEN 1 THEN 'True' " +
                    "ELSE 'ERROR' " +
                    "END AS registros " +
                    "FROM ClienteXHabitacion c " +
                    "WHERE c.id_habitacion = @id_habitacion AND  " +
                    "GETDATE() >= (fecha_desde) AND " +
                    "GETDATE() <= (fecha_hasta)";

                return conexion.Execute(sentenciaSql, new { id_habitacion =  reserva.id_habitacion}); ;
            }
        }
*/


        public int ActualizarEstadoRenovacionADisponible(Habitacion actualizarEstadoRAD)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones "+
                    "SET id_estado = CASE id_estado "+
                    "WHEN '4' THEN '2'  "+"" +
                    "WHEN '1' THEN '1'  "+"" +
                    "WHEN '2' THEN '2'  "+"" +
                    "WHEN '3' THEN '3'  "+"" +
                    "END "+
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion});

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
                    "WHEN '2' THEN '4' " +
                    "WHEN '1' THEN '1' " +
                    "WHEN '3' THEN '3' " +
                    "WHEN '4' THEN '4' " +
                    "END " +
                    "WHERE id_habitacion = @id_habitacion";
                return conexion.Execute(comando, new { id_habitacion = actualizarEstadoRAD.id_habitacion });

            }
        }




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
        /*
         public int ActualizarEstado(Habitacion actualizarEstado)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE Habitaciones "+
                              " SET id_estado = CASE id_estado "+
                              "WHEN '2' THEN '3'  "+
                              "WHEN '3' THEN '2'  "+
                              "ELSE NULL  "+
                              "END "+
                              "WHERE id_habitacion = @id_habitacion";

                var a = conexion.Execute(comando, new { id_habitacion = actualizarEstado.id_habitacion });
                conexion.Close();
                return a;
                
            }
        }
         */





















        /*
        //Parametros
        public IEnumerable<Empleados> ObtenerSupervisores()
        {


            string sentencia = "SELECT e.id_empleado, e.nombre, e.apellido, e.fecha_nacimiento, c.descripcion, id_jefe " +
                                                            "FROM Empleados e " +
                                                            "JOIN Cargos c " +
                                                            "ON e.id_cargo = c.id_cargo " +
                                                            "WHERE e.id_cargo = @id_cargo_supervisores";
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var empleados = conexion.Query<Empleados>(sentencia, new { id_cargo_supervisores = 1 }).ToList();

                return empleados;
            }
        }
        */
        /* 

         //Parametros

         public int Actualizar2(Empleados personal)
         {
             using (IDbConnection conexion = new SqlConnection(cadenaConexion))
             {
                 conexion.Open();
                 var comando = "UPDATE[Empleados] SET[id_cargo] = 1 WHERE[id_empleado] = @id_empleado_asciende";
                 return conexion.Execute(comando, new { id_empleado_asciende = personal.id_empleado });
             }
         }



         //public void Ascender(Personal personal)
         //{
         //    using (IDbConnection conexion = new SqlConnection(cadena))
         //    {
         //        conexion.Open();
         //        var comando = "UPDATE [Personal] SET [Id_Cargo= @Id_Cargo]";
         //        var sentencia = new { Id_Cargo = 2 }; / string.Format(comando, personal.Id_Cargo);/

         //        conexion.Execute(comando, sentencia);
         //    }

         //}
        */



        /* }
                else if(user != null && user.id_tipo == 2)
                {
                    var habitaciones = conexion.Query<Habitacion>("SELECT e.descripcion ,h.piso, h.numero_habitacion, h.camas, h.cochera, h.precio, h.tv, h.desayuno " +
                                                                  "FROM habitaciones h " +
                                                                  "JOIN Estado_habitaciones e " +
                                                                  "on h.id_estado = e.id_estado").ToList();
                    return habitaciones;
                }
                else
                {
                    return null;
                }   */
    }
}
