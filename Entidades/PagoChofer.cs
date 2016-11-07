using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PagoChofer
    {
        public int IdPago { get; set; }
        public Chofer Chofer { get; set; }
        public double MontoTotal { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
