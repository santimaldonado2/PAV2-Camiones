using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorCiudades
    {

        public static LinkedList<Ciudad> listarCiudades()
        {
            LinkedList<Ciudad> lista = new LinkedList<Ciudad>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT idCiudad,nombreCiudad FROM Ciudad ORDER BY nombreCiudad");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = int.Parse(dr["idCiudad"].ToString());
                   ciudad.NombreCiudad = dr["nombreCiudad"].ToString();

                    lista.AddLast(ciudad);
                }
                return lista;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return lista;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }






    }
}
