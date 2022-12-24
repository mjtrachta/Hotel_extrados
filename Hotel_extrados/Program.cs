using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoDapper
{
    class Program
    {

        public static void Menu()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine("|       0. Login                                                                 |");
            Console.WriteLine("|       1. Listar todas las habitaciones    (ocupadas x cuantos dias)         ac |");
            Console.WriteLine("|       2. Listar todas las habitaciones disponibles                          ac |");
            Console.WriteLine("|       3. Cargar cliente                                                     ac |");
            Console.WriteLine("|       4. Reservar habitacion                                                ac |");
            Console.WriteLine("|       5. Cambiar estado del habitacion limpieza(desocupado)-disponible      ac |");
            Console.WriteLine("|       6. Agregar una habitacion                                            adm |");
            Console.WriteLine("|       7. Cambiar estado del habitacion limpieza-disponible-ocupado-remode  adm |");
            Console.WriteLine("|       8. Cambiar estado del habitacion limpieza-estado anterior            adm |");
            Console.WriteLine("|       9. Cambiar estado del habitacion renovacion-disponible               adm |");
            Console.WriteLine("|       10. Salir                                                                |");
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("|   Elige una de las opciones:                                                   |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+--------------------------------------------------------------------------------+");

        }
        static void Main(string[] args)
        {

            bool salir = false;
            int a = 0;

            while (!salir)
            {
                Menu();

                //try
                //{
                int opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("----------------------------------------------------------------");
                        Console.WriteLine("|      Has elegido la opción 1  LISTAR HABITACIONES            |");
                        Console.WriteLine("----------------------------------------------------------------");

                        Consulta consultaBD = new Consulta();

                        IEnumerable<Habitacion> habitaciones = consultaBD.ObtenerHabitaciones();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("---------------------LISTA DE EMPLEADOS----------------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        foreach (var item in habitaciones)
                        {
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine("ID: " + item.id_habitacion);
                            Console.WriteLine("PISO: " + item.piso);
                            Console.WriteLine("NRO HABITACION: " + item.numero_habitacion);
                            Console.WriteLine("TIPO DE HABITACION: " + item.id_tipo);
                            Console.WriteLine("NRO CAMAS: " + item.camas);
                            Console.WriteLine("COCHERA: " + item.cochera);
                            Console.WriteLine("PRECIO: " + item.precio);
                            Console.WriteLine("TV: " + item.tv);
                            Console.WriteLine("ESTADO: " + item.id_estado);
                            Console.WriteLine("SERVICIO HABITACION: " + item.servio_habitacion);
                            Console.WriteLine("HIDROMASAJES: " + item.hidromasajes);
                            Console.WriteLine("-------------------------------------------------------------");
                        }



                        break;
                        /*
                    case 2:
                        Console.WriteLine("---------------------------------------------------------------");
                        Console.WriteLine("|      Has elegido la opción 2  LISTAR SUPERVISORES            |");
                        Console.WriteLine("---------------------------------------------------------------");

                        Consulta consultaBDsupervisores = new Consulta();

                        IEnumerable<Empleados> supervisores = consultaBDsupervisores.ObtenerSupervisores();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("---------------------LISTA DE SUPERVISORES----------------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        foreach (var item in supervisores)
                        {
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine("ID: " + item.id_empleado);
                            Console.WriteLine("NOMBRE: " + item.nombre);
                            Console.WriteLine("APELLIDO: " + item.apellido);
                            Console.WriteLine("FECHA DE NAC: " + item.fecha_nacimiento);
                            Console.WriteLine("CARGO: " + item.descripcion);
                            Console.WriteLine("JEFE: " + item.id_jefe);
                            Console.WriteLine("-------------------------------------------------------------");
                        }

                        break;

                    case 3:
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine("|      Has elegido la opción 3  AGREGAR EMPLEADOS            |");
                        Console.WriteLine("--------------------------------------------------------------");

                        Empleados personal = new Empleados();
                        Consulta insertar = new Consulta();

                        Console.WriteLine("Ingrese nombre del empleado: ");
                        personal.nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese apellido del empleado: ");
                        personal.apellido = Console.ReadLine();
                        Console.WriteLine("Ingrese la fecha de nacimiento del empleado: ");
                        personal.fecha_nacimiento = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese id de su jefe ");
                        personal.id_jefe = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese numero de cargo: (1-Jefe. 2-Sup. 3-Empl.");
                        personal.id_cargo = int.Parse(Console.ReadLine());


                        insertar.Insertar2(personal);

                        break;

                    case 4:
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine("|      Has elegido la opción 4  ASCENDER EMPLEADOS            |");
                        Console.WriteLine("--------------------------------------------------------------");

                        Consulta actualizar = new Consulta();
                        Empleados empleados1 = new Empleados();


                        Console.WriteLine("Ingrese el id del empleado que desea ascender: ");
                        empleados1.id_empleado = int.Parse(Console.ReadLine());
                        actualizar.Actualizar2(empleados1);

                        break;
                        */
                    case 2:

                        Console.WriteLine("SAlIR");
                        salir = true;

                        break;


                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine("|  ERROR !! Elige una opcion entre 1 y 5  |");
                        Console.WriteLine("-------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.White;

                        break;

                }
                //}
                //catch (Exception)
                //{
                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine("-------------------------------------------");
                //    Console.WriteLine("|     ERROR Ingrese un número valido.     |");
                //    Console.WriteLine("-------------------------------------------");
                //    Console.ForegroundColor = ConsoleColor.White;
                //}





            }
        }
    }
}