using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorTipoCobros
    {
        public static LinkedList<TipoCobro> listarTiposCobros()
        {
            LinkedList<TipoCobro> lista = new LinkedList<TipoCobro>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idTipoCobro,
                                                                            c.nombreTipoCobro
                                                                      FROM  TipoCobro c
                                                                      ORDER BY nombreTipoCobro");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoCobro tipoCobro = new TipoCobro();
                    tipoCobro.idTipoCobro = (int)dr["idTipoCobro"];
                   tipoCobro.nombreTipoCobro = dr["noombreTipoCobro"].ToString();

                    lista.AddLast(tipoCobro);
                }
                dr.Close();
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
