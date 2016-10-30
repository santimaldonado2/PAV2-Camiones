using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Camion
    {
        public int IdCamion { get; set; }
        public string Patente { get; set; }
        public bool Habilitado { get; set; }
        public DateTime FechaCompra { get; set; }
        public int NroVehiculo { get; set; }
        public Marca Marca { get; set; }
        public string Modelo { get; set; }
    }
}
