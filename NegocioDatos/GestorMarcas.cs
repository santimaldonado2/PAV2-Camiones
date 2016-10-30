using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorMarcas
    {
        public static LinkedList<Marca> listarMarcas()
        {
            LinkedList<Marca> lista = new LinkedList<Marca>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT idMarca,nombre FROM Marca ORDER BY nombre");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = int.Parse(dr["idMarca"].ToString());
                    marca.Nombre = dr["nombre"].ToString();

                    lista.AddLast(marca);
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
