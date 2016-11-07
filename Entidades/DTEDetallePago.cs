using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DTEDetallePago
    {
        public int idDetallePago { get; set; }
        public int idPago { get; set; }
        public int idViaje { get; set; }
        public double monto { get; set; }
        public double descuentoAdelanto { get; set; }

        public DTEDetallePago(DetallePago detalle)
        {
            this.idDetallePago = detalle.idDetallePago;
            this.idPago = detalle.Pago.IdPago;
            this.idViaje = detalle.idViaje;
            this.monto = detalle.monto;
            this.descuentoAdelanto = detalle.descuentoAdelanto;
        }
    }
}
