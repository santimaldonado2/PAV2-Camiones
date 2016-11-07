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

        public static string RegistrarCobro(Cobro cobro, LinkedList<DetalleCobro> detalles)
        {

            double montoTotalDeCobros = 0;
            foreach (DetalleCobro detalle in detalles)
            {
                montoTotalDeCobros = montoTotalDeCobros + detalle.monto;
            }


            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            string mensaje = "";
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"INSERT INTO Cobro (fechaCobro, idCliente, montoTotal, idTipoCobro)
                                                                     VALUES(@fechaCobro, @idCliente, @montoTotal, @idTipoCobro); SELECT @@IDENTITY");

                cmd.Parameters.AddWithValue("@fechaCobro", cobro.fechaCobro);
                cmd.Parameters.AddWithValue("@idCliente", cobro.idCliente);
                cmd.Parameters.AddWithValue("@montoTotal", montoTotalDeCobros);
                cmd.Parameters.AddWithValue("@idTipoCobro", cobro.idCobro);

                cmd.Transaction = tran;
                cobro.idCobro = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (DetalleCobro detalle in detalles)
                {
                    SqlCommand cmdDetalle = GestorConexion.iniciarComando(cn, @"INSERT INTO DetalleCobro (idCobro,
                                                                                                   idDetalleViaje,
                                                                                                   monto)
                                                                     VALUES(@idCobro,
                                                                            @idDetalleViaje,
                                                                            @monto)");

                    cmdDetalle.Parameters.AddWithValue("idCobro", cobro.idCobro);
                    cmdDetalle.Parameters.AddWithValue("idDetalleViaje", detalle.idDetalleViaje);
                    cmdDetalle.Parameters.AddWithValue("monto", detalle.monto);

                    cmdDetalle.Transaction = tran;

                    cmdDetalle.ExecuteNonQuery();

                }
                mensaje = "Viaje Registrado";
                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();

                throw;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);

            }
            return mensaje;
        }















    }
}
