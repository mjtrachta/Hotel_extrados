using DemoDapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_extrados.DTO
{
    internal class HabitacionesVIP: Habitacion
    {
        public bool servicio_habitacion { get; set; }

        public bool hidromasajes { get; set; }
    }
}
