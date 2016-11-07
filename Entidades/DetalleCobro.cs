using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleCobro
    {
        public int idDetalleCobro { get; set; }
        public int idCobro { get; set; }
        public int idDetalleViaje { get; set; }
        public double monto { get; set; }

    }
}
