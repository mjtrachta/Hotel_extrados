using Hotel_extrados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoDapper
{

    class Program
    {

        public static void LogoExtrados()
        {
            Console.WriteLine("+-----------------------------------------------------------------------------------+");
            Console.WriteLine("            █░█ █▀█ ▀█▀ █▀▀ █░░   █▀▀ ▀▄▀ ▀█▀ █▀█ ▄▀█ █▀▄ █▀█ █▀");
            Console.WriteLine("            █▀█ █▄█ ░█░ ██▄ █▄▄   ██▄ █░█ ░█░ █▀▄ █▀█ █▄▀ █▄█ ▄█");
            Console.WriteLine("+-----------------------------------------------------------------------------------+");

            Console.WriteLine("");
        }
        public static void MenuAtencion()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine("|       1. Listar todas las habitaciones    (ocupadas x cuantos dias)         ac |");
            Console.WriteLine("|       2. Listar todas las habitaciones disponibles                          ac |");
            Console.WriteLine("|       3. Cargar cliente                                                     ac |");
            Console.WriteLine("|       4. Reservar habitacion                                                ac |");
            Console.WriteLine("|       5. Cambiar estado del habitacion limpieza-disponible                  ac |");
            Console.WriteLine("|       6. Salir                                                                 |");
            Console.WriteLine("+--------------------------------------------------------------------------------+");
        }
        public static void MenuAdmin()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine("|       1. Agregar una habitacion                                            adm |");
            Console.WriteLine("|       2. Cambiar estado del habitacion limpieza-disponible-ocupado-remode  adm |");
            Console.WriteLine("|       3. Cambiar estado del habitacion limpieza-estado anterior            adm |");
            Console.WriteLine("|       4. Cambiar estado del habitacion renovacion-disponible               adm |");
            Console.WriteLine("|       5. Salir                                                                |");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("|   Elige una de las opciones:                                                   |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+--------------------------------------------------------------------------------+");

        }



        public static int Login()
        {
            Consulta c1 = new Consulta();

            Console.WriteLine("INGRESE SU EMAIL: ");
            string mail = Console.ReadLine();
            Console.WriteLine("INGRESE SU CONTRASEÑA: ");
            string pass = Console.ReadLine();
            Console.WriteLine("+-----------------------------------------------------------------------------------+");
            int a = c1.Login(mail, pass);
            return a;
        }
        public static void ListarHabitaciones()
        {
            Consulta consultaBD = new Consulta();

            

            IEnumerable<Habitacion> habitacionComun = consultaBD.ObtenerHabitacionesComunes();
            IEnumerable<Habitacion> habitacionVIP = consultaBD.ObtenerHabitacionesVip();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("---------------------LISTA DE HABITACIONES----------------------");
            Console.ForegroundColor = ConsoleColor.White;

          
                foreach (var item in habitacionComun)
                {
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("ESTADO: " + item.descripcion_estado);
                    Console.WriteLine("PISO: " + item.piso);
                    Console.WriteLine("HABITACION: " + item.numero_habitacion);
                    Console.WriteLine("N DE CAMAS: " + item.camas);
                    Console.WriteLine("COCHERA: " + item.cochera);
                    Console.WriteLine("PRECIO: " + item.precio);
                    Console.WriteLine("TV: " + item.tv);
                    Console.WriteLine("DESAYUNO: " + item.desayuno);
                    Console.WriteLine("-------------------------------------------------------------");
                    continue;
                }
                foreach (var item in habitacionVIP)
                {
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("ESTADO: " + item.descripcion_estado);
                    Console.WriteLine("PISO: " + item.piso);
                    Console.WriteLine("HABITACION: " + item.numero_habitacion);
                    Console.WriteLine("N DE CAMAS: " + item.camas);
                    Console.WriteLine("COCHERA: " + item.cochera);
                    Console.WriteLine("PRECIO: " + item.precio);
                    Console.WriteLine("TV: " + item.tv);
                    Console.WriteLine("SERVICIO HABITACION: " + item.servio_habitacion);
                    Console.WriteLine("HIDRO: " + item.hidromasajes);
                    Console.WriteLine("-------------------------------------------------------------");

                } 
        }
        public static void ListarHabitacionesDisponibles()
        {
            Consulta consultaBD = new Consulta();

            IEnumerable<Habitacion> habitacionDisponible = consultaBD.ObtenerHabitacionesDisponibles();
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("---------------------LISTA DE HABITACIONES----------------------");
            Console.ForegroundColor = ConsoleColor.White;


            foreach (var item in habitacionDisponible)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("ESTADO: " + item.descripcion_estado);
                Console.WriteLine("TIPO: " + item.descripcion);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine("HABITACION: " + item.numero_habitacion);
                Console.WriteLine("N DE CAMAS: " + item.camas);
                Console.WriteLine("COCHERA: " + item.cochera);
                Console.WriteLine("PRECIO: " + item.precio);
                Console.WriteLine("TV: " + item.tv);
                Console.WriteLine("DESAYUNO: " + item.desayuno);
                Console.WriteLine("SERVICIO HABITACION: " + item.servio_habitacion);
                Console.WriteLine("HIDRO: " + item.hidromasajes);
                Console.WriteLine("----------------------------------------------------------------");

            }
        }
        public static void AgregarCliente()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción 3  AGREGAR CLIENTE            |");
            Console.WriteLine("--------------------------------------------------------------");

            Cliente cliente = new Cliente();
            Consulta insertar = new Consulta();

            Console.WriteLine("Ingrese cuil del cliente: ");
            cliente.cuil = Int64.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese nombre del cliente: ");
            cliente.nombre = Console.ReadLine();
            Console.WriteLine("Ingrese apellido del empleado: ");
            cliente.apellido = Console.ReadLine();
            Console.WriteLine("Ingrese mail del empleado ");
            cliente.mail = Console.ReadLine();

            insertar.CargarCliente(cliente);
        }

        public static void AgregarReserva()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción  3 AGREGAR RESERVA            |");
            Console.WriteLine("--------------------------------------------------------------");

            Reserva reserva = new Reserva();
            Consulta insertar = new Consulta();

            Console.WriteLine("Ingrese cuil del cliente: ");
            reserva.cuil_cliente = Int64.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el id de la habitacion: ");
            reserva.id_habitacion = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la fecha de ingreso: ");
            reserva.fecha_desde = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la fecha de salida: ");
            reserva.fecha_hasta = DateTime.Parse(Console.ReadLine());

            insertar.CargarReserva(reserva);
        }

        static void Main(string[] args)
        {

            bool salir = false;
            

            while (!salir)
            {
                LogoExtrados();
                //try
                //{
                int opcion = Login();

                switch (opcion)
                {
                  
                    case 1:
                        Console.WriteLine("----------------------------------------------------------------");
                        Console.WriteLine("|        BIENVIENDO  AL PANEL DE ADMINISTRADOR                 |");
                        Console.WriteLine("----------------------------------------------------------------");

                        bool salir1 = false;

                        while (!salir1)
                        {
                            MenuAdmin();
                            int opcion1 = Int32.Parse(Console.ReadLine());
                            switch (opcion1)
                            {
                                case 1:
                                    ListarHabitaciones();
                                    break;
                                case 2:
                                    
                                case 5:

                                    Console.WriteLine("SALIR");
                                    salir1 = true;
                                    break;

                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("-------------------------------------------");
                                    Console.WriteLine("|  ERROR !! Elige una opcion entre 1 y 5  |");
                                    Console.WriteLine("-------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.White;

                                    break;
                            }
                        }

                        break;

                    case 2:
                        Console.WriteLine("----------------------------------------------------------------");
                        Console.WriteLine("|      BIENVIENDO  AL PANEL DE ATENCION AL CLIENTE             |");
                        Console.WriteLine("----------------------------------------------------------------");

                       
                        bool salir2 = false;

                        while (!salir2)
                        {
                            MenuAtencion();
                            int opcion2 = Int32.Parse(Console.ReadLine());
                            switch (opcion2)
                            {
                                case 1:
                                    ListarHabitaciones();
                                    break;
                                case 2:
                                    ListarHabitacionesDisponibles();
                                    break;
                                case 3:
                                    AgregarCliente();                                    
                                    break;
                                case 4:
                                    AgregarReserva();
                                    break;
                                case 5:
                                    Console.WriteLine("--------------------------------------------------------------");
                                    Console.WriteLine("|      Has elegido la opción 5  CAMBIAR DE ESTADO            |");
                                    Console.WriteLine("--------------------------------------------------------------");

                                    Consulta consultaBD = new Consulta();

                                    IEnumerable<Habitacion> habitacionDisponibleLimpieza = consultaBD.ObtenerHabitacionesDisponiblesLimpieza();

                                    foreach (var item in habitacionDisponibleLimpieza)
                                    {
                                        Console.WriteLine("-----------------------------------------------------------------");
                                        Console.WriteLine("ID HABITACION: " + item.id_habitacion);
                                        Console.WriteLine("ESTADO: " + item.descripcion_estado);
                                        Console.WriteLine("PISO: " + item.piso);
                                        Console.WriteLine("HABITACION: " + item.numero_habitacion);
                                        Console.WriteLine("----------------------------------------------------------------");

                                    }

                                    Consulta actualizar = new Consulta();
                                    Habitacion habitacion = new Habitacion();


                                    Console.WriteLine("Ingrese el id del de la habitacion que desea actualizar: ");
                                    habitacion.id_estado = int.Parse(Console.ReadLine());
                                    actualizar.ActualizarEstado(habitacion);

                                    break;

                                case 6:

                                    Console.WriteLine("SALIR");
                                    salir2 = true;
                                    break;

                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("-------------------------------------------");
                                    Console.WriteLine("|  ERROR !! Elige una opcion entre 1 y 6  |");
                                    Console.WriteLine("-------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.White;

                                    break;
                            }
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
                    
                    */
                    case 3:

                        Console.WriteLine("SALIR");
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