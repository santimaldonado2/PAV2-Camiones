using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorInformeCobros
    {

        public static LinkedList<DTEInformeCobros> buscarCobros(int idCliente, int idCobro, DateTime fechaMin, DateTime fechaMax)
        {
            LinkedList<DTEInformeCobros> lista = new LinkedList<DTEInformeCobros>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();

            DateTime inicio = Convert.ToDateTime("01/01/1990");
            DateTime fin = DateTime.Today;

            String commandText = @"SELECT	cl.nombreCliente,
                                            co.montoTotal,
                                            co.idCobro,
                                            co.fechaCobro,
                                            t.nombreTipoCobro
                                    FROM    Cobro co, 
                                            Cliente cl,
                                            TipoCobro t
                                    WHERE   co.idCliente = cl.idCliente
                                    AND     co.idTipoCobro = t.idTipoCobro";

            try
            {
                if (idCliente != -1)
                {
                    commandText += " AND co.idCliente = @idCliente ";
                }

                if (idCobro != -1)
                {
                    commandText += " AND co.idCobro = @idCobro ";
                }

                if (fechaMin != inicio)
                {
                    commandText += " AND co.fechaCobro BETWEEN @inicio AND @fin ";
                }

                if (fechaMax != fin)
                {
                    commandText += " AND co.fechaCobro BETWEEN @inicio AND @fin ";
                }


                SqlCommand cmd = GestorConexion.iniciarComando(cn, commandText);

                if (idCliente != -1)
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                }

                if (idCobro != -1)
                {
                    cmd.Parameters.AddWithValue("@idCobro", idCobro);
                }

                if (fechaMin != inicio)
                {
                    inicio = fechaMin;
                    cmd.Parameters.AddWithValue("@inicio", inicio);
                }

                if (fechaMax != fin)
                {
                    fin = fechaMax;
                    cmd.Parameters.AddWithValue("@fin", fin);
                }

                cmd.Transaction = tran;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DTEInformeCobros DTEInformeCobros = new DTEInformeCobros();

                    DTEInformeCobros.idCobro = (int)dr["idCobro"];
                    DTEInformeCobros.nombreCliente = dr["nombreCliente"].ToString();
                    DTEInformeCobros.montoCobro = float.Parse(dr["montoTotal"].ToString());
                    DTEInformeCobros.fechaCobro = (DateTime)dr["fechaCobro"];
                    DTEInformeCobros.tipoCobro = dr["nombreTipoCobro"].ToString();
                 
                    lista.AddLast(DTEInformeCobros);
                                                            
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
