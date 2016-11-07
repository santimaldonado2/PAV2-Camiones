using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DTEDetalleViaje
    {
        public int idDetalleViaje { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public double Kilogramos { get; set; }
        public double Distancia { get; set; }
        public double PrecioUnitario { get; set; }
        public string Cliente { get; set; }

        public DTEDetalleViaje(DetalleViaje detalle)
        {
            this.idDetalleViaje = detalle.IdDetalleViaje;
            this.FechaSalida = detalle.FechaSalida;
            this.FechaLlegada = detalle.FechaLlegada;
            this.CiudadDestino = detalle.CiudadDestino.NombreCiudad;
            this.CiudadOrigen = detalle.CiudadOrigen.NombreCiudad;
            this.Kilogramos = detalle.Kilogramos;
            this.Distancia = detalle.Distancia;
            this.PrecioUnitario = detalle.PrecioUnitario;
            this.Cliente = detalle.Cliente.NombreCliente;

        }
    }
}
