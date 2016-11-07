using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DTEDetalleCobro
    {
        public string nombreCliente { get; set; }
        public string tipoCobro { get; set; }
        public DateTime fechaCobro { get; set; }
        public DateTime fechaSalida { get; set; }
        public DateTime fechaLlegada { get; set; }
        public double kilogramos { get; set; }
        public double distancia { get; set; }
        public double precioUnitario { get; set; }
        public double subtotal { get; set; }      

    }
}

