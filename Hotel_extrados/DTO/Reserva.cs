using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_extrados
{
    public class Reserva
    {

        public long cuil_cliente { get; set; }
        public int id_habitacion { get; set; }
        public DateTime fecha_desde { get; set; }
        public DateTime fecha_hasta { get; set; }

    }
}
