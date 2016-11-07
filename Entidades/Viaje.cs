using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Viaje
    {
        public int IdViaje { get; set; }
        public Chofer Chofer { get; set; }
        public Camion Camion { get; set; }

    }
}
