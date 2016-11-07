using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Chofer
    {
        public int IdChofer { get; set; }
        public string NombreChofer { get; set; }
        public Ciudad Ciudad { get; set; }
        public int NroDoc { get; set; }
        public DateTime FechaNac { get; set; }
        public bool Activo { get; set; }

    }
}
