using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cobro
    {
        public int idCobro { get; set; }
        public DateTime fechaCobro { get; set; }
        public int idCliente { get; set; }
        public double montoTotal { get; set; }
        public int idTipoCobro { get; set; }


    }
}
