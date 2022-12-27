using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoDapper
{
    public class Habitacion
    {
        public int id_habitacion { get; set; }

        public int piso { get; set; }

        public int numero_habitacion { get; set; }

        public int id_tipo { get; set; }

        public int camas { get; set; }

        public bool cochera { get; set; }

        public float precio { get; set; }

        public bool tv { get; set; }

        public bool desayuno { get; set; }

        public int id_estado { get; set; }

        public bool servio_habitacion { get; set; }

        public bool hidromasajes { get; set; }

        public string descripcion { get; set; }

        public string descripcion_estado { get; set; }
    }
}
