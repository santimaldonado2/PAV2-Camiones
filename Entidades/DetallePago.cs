using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetallePago
    {
        public int idDetallePago { get; set; }
        public PagoChofer Pago { get; set; }
        public int idViaje { get; set; }
        public double monto { get; set; }
        public double descuentoAdelanto { get; set; }
    }
}
