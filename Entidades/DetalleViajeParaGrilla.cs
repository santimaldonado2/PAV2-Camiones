using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleViajeParaGrilla
    {
        public int IdDetalleViaje { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public double Kilogramos { get; set; }
        public double Distancia { get; set; }
        public double PrecioUnitario { get; set; }
        public string NombreCliente { get; set; }
        public double Subtotal { get; set; }


    }
}
