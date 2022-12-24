using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace DemoDapper
{
    public class Consulta
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();

        public IEnumerable<Habitacion> ObtenerHabitaciones()
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var habitaciones = conexion.Query<Habitacion>("SELECT * FROM habitaciones WHERE id_estado = 2").ToList();

                return habitaciones;
            }
        }
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
       /* public int Insertar(Empleados personal)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string comando = "INSERT INTO [Empleados]([nombre],[apellido],[fecha_nacimiento],[id_jefe], [id_cargo]) VALUES('{0}','{1}','{2}','{3}','{4}')";
                string sentencia = string.Format(comando, personal.nombre, personal.apellido, personal.fecha_nacimiento, personal.id_jefe, personal.id_cargo);

                return conexion.Execute(sentencia);
            }
        }
        //Parametros
        public int Insertar2(Empleados personal)
        {


            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {

                conexion.Open();
                string sentenciaSql = "INSERT INTO [Empleados]([nombre],[apellido],[fecha_nacimiento],[id_jefe], [id_cargo]) " +
                                  "VALUES(@nombre, @apellido, @fecha_nacimiento, @id_jefe, @id_cargo)";

                return conexion.Execute(sentenciaSql, new { nombre = personal.nombre, apellido = personal.apellido, fecha_nacimiento = personal.fecha_nacimiento, id_jefe = personal.id_jefe, id_cargo = personal.id_cargo });
            }
        }




        public int Actualizar(int id_empleado)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var comando = "UPDATE[Empleados] SET[id_cargo] = 1 WHERE[id_empleado] = " + id_empleado;
                return conexion.Execute(comando);
            }
        }

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

    }
}
