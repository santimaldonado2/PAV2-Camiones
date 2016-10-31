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


            //VER SENTENCIA SQL POR EL TEMA DE LA FECHA, COBROS Y DEMAS

            String commandText = @"SELECT	cl.nombreCliente,
                                            co.tipoCobro,
		                                    co.montoCobro,
                                            co.fechaCobro
                                    FROM    Cobro co,
		                                    Cliente cl,
                                    WHERE   co.idCliente = cl.idCliente";

            try
            {
                if (idCliente != -1)
                {
                    commandText += "AND co.idCliente = @idCliente ";
                }


                SqlCommand cmd = GestorConexion.iniciarComando(cn, commandText);

                if (idCliente != -1)
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                }

                cmd.Transaction = tran;
                //cmd.Parameters.AddWithValue("@patente", "%" + patente + "%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DTEInformeCobros DTEInformeCobros = new DTEInformeCobros();
                    DTEInformeCobros.montoCobro = float.Parse(dr["montoCobro"].ToString());
                    DTEInformeCobros.idCobro = (int)dr["idCobro"];
                    DTEInformeCobros.nombreCliente = dr["nombreCliente"].ToString();

                    //Hacer lo de fecha de cobro aca
                    //DTEInformeCobros.numeroCamion = Int32.Parse(dr["nroVehiculo"].ToString());
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
