using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorClientes
    {

        public static string insertarCliente(Cliente cliente)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "INSERT INTO Cliente (nombreCliente,nroCuit,clienteFijo,fechaInscripcion,idCiudad) VALUES (@nombreCliente,@nroCuit,@clienteFijo,@fechaInscripcion,@ciudad);select Scope_Identity() as ID");
                cmd.Parameters.AddWithValue("@nombreCliente", cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@nroCuit", cliente.NroCuit);
                cmd.Parameters.AddWithValue("@clienteFijo", (cliente.ClienteFijo) ? 1 : 0);
                cmd.Parameters.AddWithValue("@fechaInscripcion", cliente.FechaInscripcion);

                cmd.Parameters.AddWithValue("@ciudad", cliente.Ciudad.IdCiudad);

                cmd.Transaction = tran;
                cliente.IdCliente = Convert.ToInt32(cmd.ExecuteScalar());
                tran.Commit();
                mensaje = "Cliente insertado!";
                return mensaje;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                mensaje = ex.Message;
                if (mensaje.Contains("UK_Cliente_nroCuit"))
                {
                    throw new Exception("El CUIT que ha ingresado ya se encuentra registrado, modifique los datos y vuelva a intentarlo");
                }
                else
                    throw;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }

        }



        public static string modificarCliente(Cliente cliente)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"UPDATE Cliente 
                                                                        SET nombreCliente = @nombreCliente, 
                                                                            nroCuit = @nroCuit,
                                                                            clienteFijo = @clienteFijo,
                                                                            fechaInscripcion = @fechaInscripcion,
                                                                            idCiudad = @ciudad
                                                                      WHERE idCliente = @id");
                cmd.Parameters.AddWithValue("@nombreCliente", cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@nroCuit", cliente.NroCuit);
                cmd.Parameters.AddWithValue("@clienteFijo", (cliente.ClienteFijo) ? 1 : 0);
                cmd.Parameters.AddWithValue("@fechaInscripcion", cliente.FechaInscripcion);
                cmd.Parameters.AddWithValue("@ciudad", cliente.Ciudad.IdCiudad);

                cmd.Parameters.AddWithValue("@id", cliente.IdCliente);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                mensaje = "Cliente actualizado!";
                return mensaje;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw;
                //mensaje = ex.Message;
                //return mensaje;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }

        }




        public static string eliminarCliente(int idCliente)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"DELETE
                                                                     FROM   Cliente 
                                                                     WHERE  idCliente = @id");
                cmd.Parameters.AddWithValue("@id", idCliente);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                mensaje = "Cliente eliminado!";
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


        public static LinkedList<Cliente> listarClientes()
        {
            LinkedList<Cliente> listaClientes = new LinkedList<Cliente>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCliente,
                                                                            c.nombreCliente,
                                                                            c.nroCuit,
                                                                            c.clienteFijo,
                                                                            c.fechaInscripcion,
                                                                            c.idCiudad,
                                                                            u.nombre
                                                                      FROM  Cliente c JOIN Ciudad u ON u.idCiudad = c.idCiudad ORDER BY fechaInscripcion");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Int32.Parse(dr["idCiudad"].ToString());
                    ciudad.NombreCiudad = dr["nombre"].ToString();

                    Cliente cliente = new Cliente();
                    cliente.IdCliente = (int)dr["idCliente"];
                    cliente.NombreCliente = dr["nombreCliente"].ToString();
                    cliente.NroCuit = (int)dr["nroCuit"];
                    cliente.ClienteFijo = Boolean.Parse(dr["clienteFijo"].ToString());
                    cliente.FechaInscripcion = (DateTime)dr["fechaInscripcion"];

                    cliente.Ciudad = ciudad;
                    listaClientes.AddLast(cliente);
                }
                dr.Close();
                return listaClientes;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return listaClientes;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }


        public static LinkedList<Cliente> buscarClientePorNombre(string nombre)
        {
            LinkedList<Cliente> listaClientes = new LinkedList<Cliente>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCliente,
                                                                            c.nombreCliente,
                                                                            c.nroCuit,
                                                                            c.clienteFijo,
                                                                            c.fechaInscripcion,
                                                                            c.idCiudad,
                                                                            u.nombre
                                                                      FROM  Cliente c JOIN Ciudad u ON u.idCiudad = c.idCiudad 
                                                                     WHERE  c.nombreCliente LIKE @nombreCliente");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@nombreCliente", "%" + nombre + "%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Int32.Parse(dr["idCiudad"].ToString());
                    ciudad.NombreCiudad = dr["nombre"].ToString();

                    Cliente cliente = new Cliente();
                    cliente.IdCliente = (int)dr["idCliente"];
                    cliente.NombreCliente = dr["nombreCliente"].ToString();
                    cliente.NroCuit = (int)dr["nroCuit"];
                    cliente.ClienteFijo = Boolean.Parse(dr["clienteFijo"].ToString());
                    cliente.FechaInscripcion = (DateTime)dr["fechaInscripcion"];

                    cliente.Ciudad = ciudad;
                    listaClientes.AddLast(cliente);
                }
                dr.Close();
                return listaClientes;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return listaClientes;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }



        public static Cliente buscarCliente(int idCliente)
        {
            Cliente cliente = new Cliente();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCliente,
                                                                            c.nombreCliente,
                                                                            c.nroCuit,
                                                                            c.clienteFijo,
                                                                            c.fechaInscripcion,
                                                                            c.idCiudad,
                                                                            u.nombre
                                                                       FROM  Cliente c JOIN Ciudad u ON u.idCiudad = c.idCiudad 
                                                                     WHERE  c.idCliente = @idCliente");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Int32.Parse(dr["idCiudad"].ToString());
                    ciudad.NombreCiudad = dr["nombre"].ToString();

                    cliente.IdCliente = (int)dr["idCliente"];
                    cliente.NombreCliente = dr["nombreCliente"].ToString();
                    cliente.NroCuit = (int)dr["nroCuit"];
                    cliente.ClienteFijo = Boolean.Parse(dr["clienteFijo"].ToString());
                    cliente.FechaInscripcion = (DateTime)dr["fechaInscripcion"];

                    cliente.Ciudad = ciudad;
                }
                dr.Close();
                return cliente;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return cliente;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }




    }
}
