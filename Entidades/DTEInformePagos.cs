using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DTEInformePagos
    {
        public int idPago { set; get; }
        public string nombreChofer { set; get; }
        public int nroDoc { set; get; }
        public float montoTotal { set; get; }
        public DateTime fechaPago { set; get; }
    }
}
