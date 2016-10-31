using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DTEInformeCobros
    {
        public int idCobro { get; set; }
        public string nombreCliente { get; set; }
        public string tipoCobro { get; set; }
        public double montoCobro { get; set; }
        public DateTime fechaCobro { get; set; }

    }
}
