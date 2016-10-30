using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace NegocioDatos
{
    public class GestorSeguridad
    {
        public static bool TienePermisos(Usuario user,int permisoPagina)
        {
            return user != null && user.Rol >= permisoPagina;
        }
       
    }
}
