using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorInformeViajes
    {
        public static LinkedList<DTEInformeViajes> buscarViajes(int chofer_id, int camion_id, double distancia_min, double distancia_max)
        {
            LinkedList<DTEInformeViajes> lista = new LinkedList<DTEInformeViajes>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            String commandText = @"SELECT	v.idViaje,
                                            ch.nombreChofer,
		                                    c.nroVehiculo,
		                                    SUM(dv.distancia) distanciaTotal
                                    FROM    Viaje v,
                                            DetalleViaje dv,
		                                    Chofer ch,
                                            Camion c
                                    WHERE   dv.idViaje = v.idViaje
                                    AND     v.idChofer = ch.idChofer
                                    AND     v.idCamion = c.idCamion ";
            try
            {
                if(chofer_id != -1)
                {
                    commandText += "AND v.idChofer = @idChofer ";
                }

                if (camion_id != -1)
                {
                    commandText += "AND v.idCamion = @idCamion ";
                }

                commandText += @" GROUP BY v.idViaje,ch.nombreChofer,c.nroVehiculo 
                                 HAVING 1 = 1 ";

                if(distancia_min != -1)
                {
                    commandText += "AND SUM(dv.distancia) > @distancia_min ";
                }

                if (distancia_max != -1)
                {
                    commandText += "AND SUM(dv.distancia) < @distancia_max ";
                }
                SqlCommand cmd = GestorConexion.iniciarComando(cn, commandText);

                if (chofer_id != -1)
                {
                    cmd.Parameters.AddWithValue("@idChofer", chofer_id);
                }

                if (camion_id != -1)
                {
                    cmd.Parameters.AddWithValue("@idCamion", camion_id);
                }

                if (distancia_min != -1)
                {
                    cmd.Parameters.AddWithValue("@distancia_min", distancia_min);
                }

                if (distancia_max != -1)
                {
                    cmd.Parameters.AddWithValue("@distancia_max", distancia_max);
                }

                cmd.Transaction = tran;
                //cmd.Parameters.AddWithValue("@patente", "%" + patente + "%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DTEInformeViajes DTEInformeViajes = new DTEInformeViajes();
                    DTEInformeViajes.distanciaTotal = float.Parse(dr["distanciaTotal"].ToString());
                    DTEInformeViajes.idViaje = (int)dr["idViaje"];
                    DTEInformeViajes.nombreChofer = dr["nombreChofer"].ToString();
                    DTEInformeViajes.numeroCamion = Int32.Parse(dr["nroVehiculo"].ToString());
                    lista.AddLast(DTEInformeViajes);
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
