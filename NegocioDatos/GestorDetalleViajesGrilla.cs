using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorDetalleViajesGrilla
    {

        public static LinkedList<DetalleViajeParaGrilla> listarDetallesViajes()
        {
            LinkedList<DetalleViajeParaGrilla> lista = new LinkedList<DetalleViajeParaGrilla>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT d.idDetalleViaje, d.fechaSalida, d.fechaLLegada, d.kilogramos, c.nombreCliente, d.distancia, d.precioUnitario FROM DetalleViaje d JOIN Cliente c ON d.idCliente = c.idCliente ");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    DetalleViajeParaGrilla det = new DetalleViajeParaGrilla();
                    det.IdDetalleViaje = int.Parse(dr["idDetalleViaje"].ToString());
                    det.FechaSalida = (DateTime)dr["fechaSalida"];
                    det.FechaLlegada = (DateTime)dr["fechaLLegada"];
                    det.Kilogramos = double.Parse(dr["kilogramos"].ToString());
                    det.NombreCliente = dr["nombreCliente"].ToString();
                    det.Distancia = double.Parse(dr["distancia"].ToString());
                    det.PrecioUnitario = double.Parse(dr["precioUnitario"].ToString());
                    det.Subtotal = det.PrecioUnitario * det.Distancia;

                    lista.AddLast(det);
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




        public static double obtenerMonto(int idDetalleViaje)
        {
            double monto = 0.0;

            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT distancia, precioUnitario FROM DetalleViaje WHERE idDetalleViaje = @idDetalleViaje ");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@idDetalleViaje", idDetalleViaje);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {                   
                   double distancia = double.Parse(dr["distancia"].ToString());
                    double precioUnitario = double.Parse(dr["precioUnitario"].ToString());
                    monto = distancia * precioUnitario;
                }
                return monto;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return monto;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }





        public static DetalleViajeParaGrilla obtenerDetalleViaje(int idDetalleViaje)
        {
            DetalleViajeParaGrilla viaje = new DetalleViajeParaGrilla();

            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT idDetalleViaje, fechaSalida, fechaLLegada, kilogramos, distancia, precioUnitario FROM DetalleViaje WHERE idDetalleViaje = @idDetalleViaje ");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@idDetalleViaje", idDetalleViaje);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viaje.IdDetalleViaje = int.Parse(dr["idDetalleViaje"].ToString());
                    viaje.FechaSalida = (DateTime)dr["fechaSalida"];
                    viaje.FechaLlegada = (DateTime)dr["fechaLLegada"];
                    viaje.Kilogramos = double.Parse(dr["kilogramos"].ToString());
                    viaje.Distancia = double.Parse(dr["distancia"].ToString());
                    viaje.PrecioUnitario = double.Parse(dr["precioUnitario"].ToString());
                    viaje.Subtotal = viaje.PrecioUnitario * viaje.Distancia;

                }
                return viaje;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return viaje;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }








    }
}
