using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int NroCuit { get; set; }
        public bool ClienteFijo { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public Ciudad Ciudad { get; set; }

    }
}
