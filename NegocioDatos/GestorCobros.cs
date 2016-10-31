using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorCobros
    {
        public static LinkedList<Cobro> listarCobros()
        {
            LinkedList<Cobro> lista = new LinkedList<Cobro>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCobro,
                                                                            c.fechaCobro,
                                                                            c.idCliente,
                                                                            c.montoTotal,
                                                                            c.tipoCobro,
                                                                      FROM  Cobro c
                                                                      ORDER BY fechaCobro");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cobro cobro = new Cobro();
                    cobro.idCobro = (int)dr["idCobro"];
                    cobro.fechaCobro = (DateTime)dr["fechaCobro"];
                    cobro.idCliente = (int)dr["idCliente"];
                    cobro.montoTotal = (double)dr["montoTotal"];
                    cobro.tipoCobro = dr["tipoCobro"].ToString();

                    lista.AddLast(cobro);
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
