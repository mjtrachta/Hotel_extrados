using Dapper;
using Hotel_extrados;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DemoDapper
{

    class Program
    {
        // MENÚ
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
            Console.WriteLine("|       1. Listar todas las habitaciones                                         |");
            Console.WriteLine("|       2. Listar todas las habitaciones disponibles                             |");
            Console.WriteLine("|       3. Cargar cliente                                                        |");
            Console.WriteLine("|       4. Reservar habitacion                                                   |");
            Console.WriteLine("|       5. Cambiar estado del habitacion limpieza-disponible                     |");
            Console.WriteLine("|       6. Salir                                                                 |");
            Console.WriteLine("+--------------------------------------------------------------------------------+");
        }
        public static void MenuAdmin()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine("|       1. Agregar una habitación                                                |");
            Console.WriteLine("|       2. Cambiar estado de la habitación a limpieza o remodelación             |");
            Console.WriteLine("|       3. Cambiar estado del habitación de limpieza al estado anterior          |");
            Console.WriteLine("|       4. Cambiar estado del habitación de renovación a disponible              |");
            Console.WriteLine("|       5. Salir                                                                 |");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("|   Elige una de las opciones:                                                   |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+--------------------------------------------------------------------------------+");

        }

        // LOGIN
       
        public static int Login()
        {
            // RESOLVER QUE NO DISTINGUE ENTRE MAYUSCULAS Y MINUSCULAS
            Consulta c1 = new Consulta();

            Console.WriteLine("INGRESE SU EMAIL: ");
            string mail = Console.ReadLine();
            Console.WriteLine("INGRESE SU CONTRASEÑA: ");
            string pass = Console.ReadLine();
            Console.WriteLine("+-----------------------------------------------------------------------------------+");
            int a = c1.Login(mail, pass);
            return a;
        }



        // ADMIN 1
        public static void AgregarHabitacion()
        {
            //validar que no ingrese otra vez si se equivoca
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción 1  AGREGAR HABITACIÓN           |");
            Console.WriteLine("--------------------------------------------------------------");

            Habitacion habitacion = new Habitacion();
            Consulta insertar = new Consulta();

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Ingrese el piso de la habitación: ");
            habitacion.piso = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Ingrese el número de la habitación: ");
            habitacion.numero_habitacion = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Común: 1 ");
            Console.WriteLine("VIP: 2");
            Console.WriteLine("Ingrese el tipo de la habitación: ");
            habitacion.id_tipo = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Ingrese el número de camas de la habitación: ");
            habitacion.camas = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Sí tiene: true ");
            Console.WriteLine("No tiene: false");
            Console.WriteLine("Ingrese si tiene cochera la habitación: ");
            habitacion.cochera = bool.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Ingrese el precio por noche de la habitación: ");
            habitacion.precio = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Sí tiene: true ");
            Console.WriteLine("No tiene: false");
            Console.WriteLine("Ingrese si tiene tv la habitación: ");
            habitacion.tv = bool.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Sí tiene: true ");
            Console.WriteLine("No tiene: false");
            Console.WriteLine("Ingrese si tiene desayuno la habitación: ");
            habitacion.desayuno = bool.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Ocupado: 1");
            Console.WriteLine("Disponible: 2");
            Console.WriteLine("Limpieza: 3");
            Console.WriteLine("Renovación: 4");
            Console.WriteLine("Ingrese el estado de la habitación: ");
            habitacion.id_estado = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Sí tiene: true ");
            Console.WriteLine("No tiene: false");
            Console.WriteLine("Ingrese si tiene servicio a la habitación: ");
            habitacion.servicio_habitacion = bool.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Sí tiene: true ");
            Console.WriteLine("No tiene: false");
            Console.WriteLine("Ingrese si tiene hidromasajes la habitación: ");
            habitacion.hidromasajes = bool.Parse(Console.ReadLine());


            insertar.CargarHabitacion(habitacion);
        }
        // ADMIN 2
        public static void ActualizarEstadoYCancelar()
        {
            Consulta consultaBD = new Consulta();

            Habitacion habitacion = new Habitacion();

            Reserva reserva = new Reserva();


            Console.WriteLine("Ingrese el id de la habitacion que desea cambiar de estado: ");
            int id_habitacion = int.Parse(Console.ReadLine());
            habitacion.id_habitacion = id_habitacion;
            reserva.id_habitacion = id_habitacion;

            Console.WriteLine("Ingrese 1 si desea poner la habitacion en estado de Limpieza");
            Console.WriteLine("Ingrese 2 si desea poner la habitacion en estado de Renovacion");

            int estado_final = int.Parse(Console.ReadLine());

            int estadoInicial = consultaBD.ObtenerHabitacionesDisponiblesParametro(habitacion);


            if (estadoInicial == 2 /*disponible*/ && estado_final == 1) //disponible a limpieza
            {
                consultaBD.ActualizarEstadoDisponibleALimpieza(habitacion);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (estadoInicial == 2 /*disponible*/ && estado_final == 2) //disponible a renovacion
            {
                consultaBD.ActualizarEstadoDisponibleARenovacion(habitacion);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (estadoInicial == 1 /*ocupado*/ && estado_final == 1) //ocupado a limpeza
            {
                consultaBD.ActualizarEstadoOcupadoALimpieza(habitacion);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente!");
                Console.ForegroundColor = ConsoleColor.White;
            }


            else if (estadoInicial == 1 /*ocupado*/ && estado_final == 2) //ocupado a renovacion
            {
                consultaBD.ActualizarEstadoOcupadoARenovacion(habitacion);
                consultaBD.ActualizarBandera(reserva);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente!");
                Console.ForegroundColor = ConsoleColor.White;
            }


            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingreso un opción incorrecta!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Intentelo nuevamente");
            }
        }
        //ADMIN 3
        public static void ConsultarEstadoHoy()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción 2  CAMBIAR DE ESTADO            |");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("");

            Consulta consultaBD = new Consulta();

            Habitacion habitacionEstado = new Habitacion();

            IEnumerable<Habitacion> habitacionLimpieza = consultaBD.ObtenerHabitacionesLimpieza();
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("               HABITACIONES EN ESTADO DE LIMPIEZA                ");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var item in habitacionLimpieza)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("ID HABITACION: " + item.id_habitacion);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine("NRO HABITACION: " + item.numero_habitacion);
                Console.WriteLine("-----------------------------------------------------------------");
            }


            Console.WriteLine("Ingrese el id de la habitacion de desea cambiar al estado anterior: ");
            int variableIntermedia = int.Parse(Console.ReadLine());

            habitacionEstado.id_habitacion = variableIntermedia;

            int serONoSer = consultaBD.ExisteHabitacionLimpia(variableIntermedia);

            int estadoHoy = consultaBD.EstadoHabitacionHoy(habitacionEstado);

            long cliente = consultaBD.Cliente(habitacionEstado);

            if (estadoHoy == 1 && serONoSer ==  1)
            {
                consultaBD.ActualizarEstadoLimpiezaAOcupado(habitacionEstado);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente a ocupado por: " + cliente);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (estadoHoy == 0 && serONoSer == 1)
            {
                consultaBD.ActualizarEstadoLimpiezaADisponible(habitacionEstado);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
        //ADMIN 4
        public static void RenovacionADisponible()
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción 4  CAMBIAR DE ESTADO R a D           |");
            Console.WriteLine("-------------------------------------------------------------------");

            Consulta consultaBD2 = new Consulta();//

            IEnumerable<Habitacion> habitacionRenovacionADisponible = consultaBD2.ObtenerHabitacionesRenovacion();



            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("               HABITACIONES EN ESTADO DE RENOVACION                ");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var item in habitacionRenovacionADisponible)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("ID HABITACION: " + item.id_habitacion);
                Console.WriteLine("ESTADO: " + item.descripcion_estado);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine(" NÚMERO HABITACION: " + item.numero_habitacion);
                Console.WriteLine("----------------------------------------------------------------");
            }


            Habitacion habitacion2 = new Habitacion();

            Console.WriteLine("Ingrese el id del de la habitacion que desea actualizar: ");
            int variableIntermedia = int.Parse(Console.ReadLine());
            habitacion2.id_habitacion = variableIntermedia;

            int serONoSer = consultaBD2.ExisteHabitacionRenovacion(variableIntermedia);

            if (serONoSer == 1)
            {
                consultaBD2.ActualizarEstadoRenovacionADisponible(habitacion2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cambio correctamente!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR");
                Console.ForegroundColor = ConsoleColor.White;
            }
           
            
        }


        // AT CLIENTE 1
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

                int idc = item.id_habitacion;
                DateTime dias = consultaBD.DiasOcupados(idc);

                int estadoc = item.id_estado;
                
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("ID COMUN: " + item.id_habitacion);
                Console.WriteLine("ID ESTADO: " + item.id_estado);
                Console.WriteLine("ESTADO: " + item.descripcion_estado);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine("HABITACION: " + item.numero_habitacion);
                Console.WriteLine("N DE CAMAS: " + item.camas);
                Console.WriteLine("COCHERA: " + item.cochera);
                Console.WriteLine("PRECIO: " + item.precio);
                Console.WriteLine("TV: " + item.tv);
                Console.WriteLine("DESAYUNO: " + item.desayuno);
                if(estadoc == 1)
                {
                    Console.WriteLine("Ocupado hasta el día: " + dias);
                    continue;
                }
                Console.WriteLine("-------------------------------------------------------------");
            }
            foreach (var item in habitacionVIP)
            {
                int estadov = item.id_estado;
                int idv = item.id_habitacion;

                DateTime dias = consultaBD.DiasOcupados(idv);

                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("ESTADO: " + item.descripcion_estado);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine("HABITACION: " + item.numero_habitacion);
                Console.WriteLine("N DE CAMAS: " + item.camas);
                Console.WriteLine("COCHERA: " + item.cochera);
                Console.WriteLine("PRECIO: " + item.precio);
                Console.WriteLine("TV: " + item.tv);
                Console.WriteLine("SERVICIO HABITACION: " + item.servicio_habitacion);
                Console.WriteLine("HIDRO: " + item.hidromasajes);
                Console.WriteLine("-------------------------------------------------------------");
                if (estadov == 1)
                {
                    Console.WriteLine("Ocupado hasta el día: " + dias);
                }
            }
        }
        // AT CLIENTE 2
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
                Console.WriteLine("ID HABITACION: " + item.id_habitacion);
                Console.WriteLine("ESTADO: " + item.descripcion_estado);
                Console.WriteLine("TIPO: " + item.descripcion);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine("HABITACION: " + item.numero_habitacion);
                Console.WriteLine("N DE CAMAS: " + item.camas);
                Console.WriteLine("COCHERA: " + item.cochera);
                Console.WriteLine("PRECIO: " + item.precio);
                Console.WriteLine("TV: " + item.tv);
                Console.WriteLine("DESAYUNO: " + item.desayuno);
                Console.WriteLine("SERVICIO HABITACION: " + item.servicio_habitacion);
                Console.WriteLine("HIDRO: " + item.hidromasajes);
                Console.WriteLine("----------------------------------------------------------------");

            }
        }
        // AT CLIENTE 3
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
        // AT CLIENTE 4
        public static void AgregarReserva()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción  3 AGREGAR RESERVA              |");
            Console.WriteLine("--------------------------------------------------------------");

            Reserva reserva = new Reserva();
            Consulta insertar = new Consulta();



            Console.WriteLine("Ingrese cuil del cliente: ");
            reserva.cuil_cliente = Int64.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el id de la habitacion: ");
            reserva.id_habitacion = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la fecha de ingreso: ");
             DateTime variable1= DateTime.Parse(Console.ReadLine());
            reserva.fecha_desde = variable1;

            Console.WriteLine("Ingrese la fecha de salida: ");
            DateTime variable2 = DateTime.Parse(Console.ReadLine());
            reserva.fecha_hasta = variable2;



            int resultado1 = insertar.ColisionDeFechas(reserva, variable1);
            int resultado2 = insertar.ColisionDeFechas(reserva, variable2);


            // dato obligatorio
            reserva.bandera = bool.Parse("True");

            if (resultado1 == 0 && resultado2 == 0)
            {
                insertar.CargarReserva(reserva);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se cargo correctamente la reserva");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error colisión de fechas");
                Console.ForegroundColor = ConsoleColor.White;
            }


            
        }
        // AT CLIENTE 5
        public static void LimpiezaADisponible()
        {
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("|      Has elegido la opción 5  CAMBIAR DE ESTADO  L a D         |");
            Console.WriteLine("------------------------------------------------------------------");

            Consulta consultaBD = new Consulta();//

            IEnumerable<Habitacion> habitacionDisponibleLimpieza = consultaBD.ObtenerHabitacionesDisponiblesLimpieza();

            foreach (var item in habitacionDisponibleLimpieza)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("ID HABITACION: " + item.id_habitacion);
                Console.WriteLine("ESTADO: " + item.descripcion_estado);
                Console.WriteLine("PISO: " + item.piso);
                Console.WriteLine("HABITACION: " + item.numero_habitacion);
                Console.WriteLine("-----------------------------------------------------------------");
            }

            Consulta actualizar = new Consulta();
            Habitacion habitacion = new Habitacion();

            Console.WriteLine("Ingrese el id del de la habitacion que desea actualizar: ");
            habitacion.id_habitacion = int.Parse(Console.ReadLine());
            actualizar.ActualizarEstado(habitacion);
        }

       

        // TEST
      /*  public static void ListarDiasOcupados()
        {
            Consulta consultaBD = new Consulta();

            IEnumerable<Reserva> diasOcupados = consultaBD.DiasOcupados();



            foreach (var item in diasOcupados)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("cuil: " + item.cuil_cliente);
                Console.WriteLine("id habitacion: " + item.id_habitacion);
                Console.WriteLine("fecha desde: " + item.fecha_desde);
                Console.WriteLine("dias: " + item.dias_ocupados);
                
                Console.WriteLine("----------------------------------------------------------------");

            }
        }
      */


        static void Main(string[] args)
        {

            bool salir = false;


            while (!salir)
            {
                LogoExtrados();
                try
                {
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
                                        AgregarHabitacion();
                                        break;
                                    case 2:
                                        ActualizarEstadoYCancelar();
                                        break;
                                    case 3:
                                        ConsultarEstadoHoy();
                                        break;
                                    case 4:
                                        RenovacionADisponible();
                                        break;

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
                                        LimpiezaADisponible();
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
                        case 3:

                            Console.WriteLine("SALIR");
                            salir = true;
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("-----------------------------------------------");
                            Console.WriteLine("|  Error verifique su usuario o contraseña!!  |");
                            Console.WriteLine("-----------------------------------------------");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;
                    }

            }
                catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("|                     Error!              |");
                Console.WriteLine("-------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
            
        }
    }
}