using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleViaje
    {
        public int IdDetalleViaje { get; set; }
        public Viaje Viaje { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public Ciudad CiudadOrigen { get; set; }
        public Ciudad CiudadDestino { get; set; }
        public double Kilogramos { get; set;}
        public double Distancia { get; set; }
        public double PrecioUnitario { get; set; }
        public Cliente Cliente { get; set; }
    }
}
