using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorViaje
    {
        public static LinkedList<Viaje> listarChoferes()
        {
            LinkedList<Viaje> lista = new LinkedList<Viaje>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT idChofer,
                                                                            idViaje,
                                                                            idCamion
                                                                      FROM  Viaje 
                                                                      ORDER BY idViaje");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Viaje viaje = new Viaje();
                    viaje.idChofer = (int)dr["idChofer"];
                    viaje.idCamion = (int)dr["idCamion"];
                    viaje.idViaje = (int)dr["idViaje"];
                    lista.AddLast(viaje);
                }
                dr.Close();
                return lista;
            }
            catch (SqlException)
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
