using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Chofer
    {
        public int idChofer { get; set; }
        public string nombreChofer { get; set; }
        public int idCiudad { get; set; }
        public int nroDoc { get; set; }
        public DateTime FechaNac { get; set; }
        public bool activo { get; set; }

    }
}
