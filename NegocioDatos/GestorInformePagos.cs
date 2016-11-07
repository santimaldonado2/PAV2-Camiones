using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorInformePagos
    {
        public static LinkedList<DTEInformePagos> buscarPagos(int chofer_id, double montoMin, double montoMax, DateTime fecha_min, DateTime fecha_max)
        {
            DateTime fechaMinLocal = DateTime.Parse("01/01/2000");
            DateTime fechaMaxLocal = DateTime.Parse("01/01/2000");

            LinkedList<DTEInformePagos> lista = new LinkedList<DTEInformePagos>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            String commandText = @"SELECT	p.idPago,
                                            c.nombreChofer,
                                            c.nroDoc,
		                                    p.montoTotal,
		                                    p.fechaPago
                                    FROM    PagoChofer p,
		                                    Chofer c
                                    WHERE   p.idChofer = c.idChofer";
            try
            {
                if (chofer_id != -1)
                {
                    commandText += " AND p.idChofer = @idChofer";
                }
                
                if (montoMin != -1)
                {
                    commandText += " AND p.montoTotal > @montoMin";
                }
                if(montoMax != -1)
                {
                    commandText += " AND p.montoTotal < @montoMax";
                }

                commandText += @" GROUP BY p.idPago,c.nroDoc,c.nombreChofer,p.montoTotal,p.fechaPago 
                                 HAVING 1 = 1 ";
/*
                if (fecha_min != fechaMinLocal)
                {
                    commandText += "AND SUM(dv.distancia) > @distancia_min ";
                }

                if (fecha_max != fechaMaxLocal)
                {
                    commandText += "AND SUM(dv.distancia) < @distancia_max ";
                }*/
                SqlCommand cmd = GestorConexion.iniciarComando(cn, commandText);

                if (chofer_id != -1)
                {
                    cmd.Parameters.AddWithValue("@idChofer", chofer_id);
                }
                
                if (montoMin != -1)
                {
                    cmd.Parameters.AddWithValue("@montoMin", montoMin);
                }
                if (montoMax != -1)
                {
                    cmd.Parameters.AddWithValue("@montoMax", montoMax);
                }
                /*
                if (fecha_min != -1)
                {
                    cmd.Parameters.AddWithValue("@distancia_min", fecha_min);
                }

                if (fecha_max != -1)
                {
                    cmd.Parameters.AddWithValue("@distancia_max", fecha_max);
                }*/

                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DTEInformePagos DTEInformePagos = new DTEInformePagos();
                    DTEInformePagos.montoTotal = float.Parse(dr["montoTotal"].ToString());
                    DTEInformePagos.idPago = (int)dr["idPago"];
                    DTEInformePagos.nombreChofer = dr["nombreChofer"].ToString();
                    DTEInformePagos.nroDoc = Int32.Parse(dr["nroDoc"].ToString());
                    DTEInformePagos.fechaPago = DateTime.Parse(dr["fechaPago"].ToString());
                    lista.AddLast(DTEInformePagos);
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
