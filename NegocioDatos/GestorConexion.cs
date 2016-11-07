using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioDatos
{
    class GestorConexion
    {
        static string cadenaConex = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\USER\\Documents\\GitHub\\PAV2-Camiones\\Camiones\\App_Data\\Camiones.mdf;Integrated Security=True";
            public static SqlConnection abrirConexion()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = cadenaConex;
            cn.Open();
            return cn;
        }

        public static void cerrarConexion(SqlConnection cn)
        {
            cn.Close();
        }

        public static SqlCommand iniciarComando(SqlConnection cn, string cmdTxt)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = cmdTxt;
            return cmd;
        }
    }
}
