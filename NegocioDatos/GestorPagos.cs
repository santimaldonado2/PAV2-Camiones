using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using NegocioDatos;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorPagos
    {
        public static LinkedList<PagoChofer> listarPagos()
        {
            LinkedList<PagoChofer> lista = new LinkedList<PagoChofer>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT p.idPago,
                                                                            p.idChofer,
                                                                            p.fechaPago,
                                                                            p.montoTotal,
                                                                            p.idChofer,
                                                                            c.nombreChofer
                                                                      FROM  PagoChofer p JOIN Chofer c ON(p.idChofer=c.idChofer)
                                                                      ORDER BY idPago");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Chofer chofer = new Chofer();
                    chofer.IdChofer = Int32.Parse(dr["idChofer"].ToString());
                    chofer.NombreChofer = dr["nombreChofer"].ToString();

                    PagoChofer pago = new PagoChofer();
                    pago.IdPago = (int)dr["idPago"];
                    pago.MontoTotal = Convert.ToDouble(dr["montoTotal"].ToString());
                    pago.FechaPago = (DateTime)dr["fechaPago"];
                    lista.AddLast(pago);
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

        public static string insertarPagoChofer(PagoChofer pago, LinkedList<DetallePago> detalles)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "INSERT INTO PagoChofer (idChofer,fechaPago,montoTotal) VALUES (@idChofer,@fechaPago,@montoTotal);select Scope_Identity() as ID");
                cmd.Parameters.AddWithValue("@idChofer", pago.Chofer.IdChofer);
                cmd.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                cmd.Parameters.AddWithValue("@montoTotal", pago.MontoTotal);
                cmd.Transaction = tran;
                pago.IdPago = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (DetallePago detallePago in detalles)
                {
                    SqlCommand cmdDetalle = GestorConexion.iniciarComando(cn, "INSERT INTO DetallePago (idPago,idViaje,monto,descuentoAdelanto) VALUES (@idPago,@idViaje,@monto,@descuentoAdelanto);select Scope_Identity() as ID");
                    cmdDetalle.Parameters.AddWithValue("@idPago", pago.IdPago);
                    cmdDetalle.Parameters.AddWithValue("@idViaje", detallePago.idViaje);
                    cmdDetalle.Parameters.AddWithValue("@monto", detallePago.monto);
                    cmdDetalle.Parameters.AddWithValue("@descuentoAdelanto", detallePago.descuentoAdelanto);

                    cmdDetalle.Transaction = tran;
                    cmdDetalle.ExecuteNonQuery();
                }


                tran.Commit();
                mensaje = "Pago registrado!";
                return mensaje;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                mensaje = ex.Message;
                return mensaje;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }

        }

    }
}
