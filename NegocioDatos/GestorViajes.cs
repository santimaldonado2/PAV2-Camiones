using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;


namespace NegocioDatos
{
    public class GestorViajes
    {
        public static string RegistrarViaje(Viaje viaje, LinkedList<DetalleViaje> detalles)
        {
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            string mensaje="";
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"INSERT INTO Viaje (idChofer,idViaje)
                                                                     VALUES(@idChofer,@idViaje); SELECT @@IDENTITY");

                cmd.Parameters.AddWithValue("@idChofer", viaje.Chofer.IdChofer);
                cmd.Parameters.AddWithValue("@idViaje", viaje.IdViaje);
                cmd.Transaction = tran;
                viaje.IdViaje = Convert.ToInt32(cmd.ExecuteScalar());
                
                foreach(DetalleViaje detalle in detalles){
                    SqlCommand cmdDetalle = GestorConexion.iniciarComando(cn, @"INSERT INTO DetalleViaje (idViaje,
                                                                                                   fechaSalida,
                                                                                                   fechaLlegada,
                                                                                                   CiudadOrigen,
                                                                                                   CiudadDestino,
                                                                                                   kilogramos,
                                                                                                   distancia,
                                                                                                   precioUnitario,
                                                                                                   idCliente)
                                                                     VALUES(@idViaje,
                                                                            @FechaSalida,
                                                                            @FechaLlegada,
                                                                            @CiudadOrigen,
                                                                            @CiudadDestino,
                                                                            @Kilogramos,
                                                                            @Distancia,
                                                                            @PrecioUnitario,
                                                                            @Cliente)");

                    cmdDetalle.Parameters.AddWithValue("idViaje", viaje.IdViaje);
                    cmdDetalle.Parameters.AddWithValue("FechaSalida", detalle.FechaSalida);
                    cmdDetalle.Parameters.AddWithValue("FechaLlegada", detalle.FechaLlegada);
                    cmdDetalle.Parameters.AddWithValue("CiudadOrigen", detalle.CiudadOrigen.IdCiudad);
                    cmdDetalle.Parameters.AddWithValue("CiudadDestino", detalle.CiudadDestino.IdCiudad);
                    cmdDetalle.Parameters.AddWithValue("Kilogramos", detalle.Kilogramos);
                    cmdDetalle.Parameters.AddWithValue("Distancia", detalle.Distancia);
                    cmdDetalle.Parameters.AddWithValue("PrecioUnitario", detalle.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("Cliente", detalle.Cliente.IdCliente);

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

        public static LinkedList<Viaje> listarViajes()
        {
            LinkedList<Viaje> lista = new LinkedList<Viaje>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT v.idViaje,
                                                                            v.idChofer,
                                                                            v.idCamion
                                                                      FROM  Viaje v");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    

                    Viaje viaje = new Viaje();
                    viaje.IdViaje = (int)dr["idViaje"];
                    viaje.Chofer = GestorChoferes.buscarChofer((int)dr["idChofer"]);
                    viaje.Camion = GestorCamiones.buscarCamion((int)dr["idCamion"]);
                    lista.AddLast(viaje);
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
