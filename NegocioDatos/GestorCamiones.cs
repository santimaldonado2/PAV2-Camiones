using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorCamiones
    {


        public static string insertarCamion(Camion camion)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "INSERT INTO Camion (patente,habilitado,fechaCompra,nroVehiculo,marca,modelo) VALUES (@patente,@habilitado,@fechaCompra,@nroVehiculo,@marca,@modelo);select Scope_Identity() as ID");
                cmd.Parameters.AddWithValue("@patente", camion.Patente);
                cmd.Parameters.AddWithValue("@habilitado", (camion.Habilitado) ? 1 : 0);
                cmd.Parameters.AddWithValue("@fechaCompra", camion.FechaCompra);
                cmd.Parameters.AddWithValue("@nroVehiculo", camion.NroVehiculo);
                cmd.Parameters.AddWithValue("@marca", camion.Marca.IdMarca);
                cmd.Parameters.AddWithValue("@modelo", camion.Modelo);

                cmd.Transaction = tran;
                camion.IdCamion = Convert.ToInt32(cmd.ExecuteScalar());
                tran.Commit();
                mensaje = "Camion insertado!";
                return mensaje;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                mensaje = ex.Message;
                if (mensaje.Contains("UK_Camion_patente"))
                {
                   throw new Exception("La patente que ha ingresado ya se encuentra registrada, modifique los datos y vuelva a intentarlo");
                }
                if (mensaje.Contains("UK_Camion_nroVehiculo"))
                {
                    throw new Exception("El numero de Vehiculo ya se encuentra registrado, modifique los datos y vuelva a intentarlo");
                }
                else
                    throw;                
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }

        }


        public static string modificarCamion(Camion camion)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"UPDATE Camion 
                                                                        SET patente = @patente, 
                                                                            habilitado = @habilitado,
                                                                            fechaCompra = @fechaCompra,
                                                                            nroVehiculo = @nroVehiculo,
                                                                            marca = @marca,
                                                                            modelo = @modelo
                                                                      WHERE idCamion = @id");
                cmd.Parameters.AddWithValue("@patente", camion.Patente);
                cmd.Parameters.AddWithValue("@habilitado", (camion.Habilitado) ? 1 : 0);
                cmd.Parameters.AddWithValue("@fechaCompra", camion.FechaCompra);
                cmd.Parameters.AddWithValue("@nroVehiculo", camion.NroVehiculo);
                cmd.Parameters.AddWithValue("@marca", camion.Marca.IdMarca);
                cmd.Parameters.AddWithValue("@modelo", camion.Modelo);

                cmd.Parameters.AddWithValue("@id", camion.IdCamion);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                mensaje = "Camion actualizado!";
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

        public static string eliminarCamion(int id)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"DELETE
                                                                     FROM   Camion 
                                                                     WHERE  idCamion = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                mensaje = "Camion eliminado!";
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

        public static LinkedList<Camion> listarCamiones()
        {
            LinkedList<Camion> lista = new LinkedList<Camion>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCamion,
                                                                            c.patente,
                                                                            c.habilitado,
                                                                            c.fechaCompra,
                                                                            c.nroVehiculo,
                                                                            c.marca,
                                                                            c.modelo,
                                                                            m.nombre
                                                                      FROM  Camion c JOIN Marca m ON m.idMarca= c.marca ORDER BY fechaCompra");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = Int32.Parse(dr["marca"].ToString());
                    marca.Nombre = dr["nombre"].ToString();

                    Camion camion = new Camion();
                    camion.IdCamion = (int)dr["idCamion"];
                    camion.Patente = dr["patente"].ToString();
                    camion.Habilitado = Boolean.Parse(dr["habilitado"].ToString());
                    camion.FechaCompra = (DateTime)dr["fechaCompra"];
                    //var nroVehiculo = dr["nroVehiculo"];
                    camion.NroVehiculo = Int32.Parse(dr["nroVehiculo"].ToString());
                    camion.Modelo = dr["modelo"].ToString();
                    camion.Marca = marca;
                    lista.AddLast(camion);
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

        public static LinkedList<Camion> buscarCamionPorPatente(string patente)
        {
            LinkedList<Camion> lista = new LinkedList<Camion>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCamion,
                                                                            c.patente,
                                                                            c.habilitado,
                                                                            c.fechaCompra,
                                                                            c.nroVehiculo,
                                                                            c.marca,
                                                                            c.modelo,
                                                                            m.nombre
                                                                      FROM  Camion c JOIN Marca m ON m.idMarca= c.marca
                                                                     WHERE  c.patente LIKE @patente");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@patente", "%" + patente +"%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = Int32.Parse(dr["marca"].ToString());
                    marca.Nombre = dr["nombre"].ToString();

                    Camion camion = new Camion();
                    camion.IdCamion = (int)dr["idCamion"];
                    camion.Patente = dr["patente"].ToString();
                    camion.Habilitado = Boolean.Parse(dr["habilitado"].ToString());
                    camion.FechaCompra = (DateTime)dr["fechaCompra"];
                    camion.NroVehiculo = Int32.Parse(dr["nroVehiculo"].ToString());
                    camion.Modelo = dr["modelo"].ToString();
                    camion.Marca = marca;
                    lista.AddLast(camion);
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
        public static Camion buscarCamion(int id)
        {
            Camion camion = new Camion();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idCamion,
                                                                            c.patente,
                                                                            c.habilitado,
                                                                            c.fechaCompra,
                                                                            c.nroVehiculo,
                                                                            c.marca,
                                                                            c.modelo
                                                                      FROM  Camion c
                                                                     WHERE  c.idCamion = @id");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = Int32.Parse(dr["marca"].ToString());


                    camion.IdCamion = (int)dr["idCamion"];
                    camion.Patente = dr["patente"].ToString();
                    camion.Habilitado = Boolean.Parse(dr["habilitado"].ToString());
                    camion.FechaCompra = (DateTime)dr["fechaCompra"];
                    camion.NroVehiculo = Int32.Parse(dr["nroVehiculo"].ToString());
                    camion.Modelo = dr["modelo"].ToString();
                    camion.Marca = marca;
                }
                dr.Close();
                return camion;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return camion;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }
    }
}
