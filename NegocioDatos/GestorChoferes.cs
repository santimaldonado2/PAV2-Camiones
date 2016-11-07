using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorChoferes
    {

        public static string insertarChofer(Chofer chofer)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "INSERT INTO Chofer (nombreChofer,idCiudad,nroDoc,fechaNac,activo) VALUES (@nombreChofer,@ciudad,@nroDoc,@fechaNac,@activo);select Scope_Identity() as ID");
                cmd.Parameters.AddWithValue("@nombreChofer", chofer.NombreChofer);
                cmd.Parameters.AddWithValue("@nroDoc", chofer.NroDoc);
                cmd.Parameters.AddWithValue("@fechaNac", chofer.FechaNac);
                cmd.Parameters.AddWithValue("@activo", (chofer.Activo) ? 1 : 0);
                cmd.Parameters.AddWithValue("@ciudad", chofer.Ciudad.IdCiudad);

                cmd.Transaction = tran;
                chofer.IdChofer = Convert.ToInt32(cmd.ExecuteScalar());
                tran.Commit();
                mensaje = "Chofer insertado!";
                return mensaje;
            }
            catch (SqlException ex)
            {
                
                tran.Rollback();
                mensaje = ex.Message;
                
                if (mensaje.Contains("UK_Nro_Doc"))
                {
                    throw new Exception("El nro de documento que ha ingresado ya se encuentra registrado, modifique los datos y vuelva a intentarlo");
                }
                return mensaje;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }

        }
        
        public static string eliminarChofer(int id)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"DELETE
                                                                     FROM   Chofer 
                                                                     WHERE  idChofer = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                mensaje = "Chofer eliminado!";
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

        public static string modificarChofer(Chofer chofer)
        {
            string mensaje = "";
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"UPDATE Chofer 
                                                                        SET nombreChofer = @nombreChofer,
                                                                            idCiudad = @ciudad,
                                                                            nroDoc = @nroDoc,
                                                                            fechaNac = @fechaNac,
                                                                            activo = @activo
                                                                      WHERE idChofer = @id");
                cmd.Parameters.AddWithValue("@nombreChofer", chofer.NombreChofer);
                cmd.Parameters.AddWithValue("@ciudad", chofer.Ciudad.IdCiudad);
                cmd.Parameters.AddWithValue("@nroDoc", chofer.NroDoc);
                cmd.Parameters.AddWithValue("@fechaNac", chofer.FechaNac);
                cmd.Parameters.AddWithValue("@activo", (chofer.Activo) ? 1 : 0);
                cmd.Parameters.AddWithValue("@id", chofer.IdChofer);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                mensaje = "Chofer actualizado!";
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

        public static LinkedList<Chofer> listarChoferes()
        {
            LinkedList<Chofer> lista = new LinkedList<Chofer>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idChofer,
                                                                            c.nombreChofer,
                                                                            c.idCiudad,
                                                                            ci.nombre,
                                                                            c.nroDoc,
                                                                            c.fechaNac,
                                                                            c.activo
                                                                      FROM  Chofer c JOIN Ciudad ci ON(c.idCiudad=ci.idCiudad)
                                                                      ORDER BY nombreChofer");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Int32.Parse(dr["idCiudad"].ToString());
                    ciudad.NombreCiudad = dr["nombre"].ToString();

                    Chofer Chofer = new Chofer();
                    Chofer.IdChofer = (int)dr["idChofer"];
                    Chofer.NombreChofer = dr["nombreChofer"].ToString();
                    Chofer.Activo = Boolean.Parse(dr["activo"].ToString());
                    Chofer.FechaNac = (DateTime)dr["fechaNac"];
                    Chofer.NroDoc = Int32.Parse(dr["nroDoc"].ToString());
                    Chofer.Ciudad = ciudad;
                    lista.AddLast(Chofer);
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

        public static Chofer buscarChofer(int id)
        {
            Chofer chofer = new Chofer();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idChofer,
                                                                            c.nombreChofer,
                                                                            c.idCiudad,
                                                                            c.nroDoc,
                                                                            c.fechaNac,
                                                                            c.activo
                                                                      FROM  Chofer c
                                                                     WHERE  c.idChofer = @id");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Int32.Parse(dr["idCiudad"].ToString());


                    chofer.IdChofer = (int)dr["idChofer"];
                    chofer.NombreChofer = dr["nombreChofer"].ToString();
                    chofer.Activo = Boolean.Parse(dr["activo"].ToString());
                    chofer.FechaNac = (DateTime)dr["fechaNac"];
                    chofer.NroDoc = Int32.Parse(dr["nroDoc"].ToString());
                    chofer.Ciudad = ciudad;
                }
                dr.Close();
                return chofer;
            }
            catch (SqlException)
            {
                tran.Rollback();
                return chofer;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }
        

        public static LinkedList<Chofer> buscarPorNombre(string nombre)
        {
            LinkedList<Chofer> lista = new LinkedList<Chofer>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT c.idChofer,
                                                                            c.nombreChofer,
                                                                            c.idCiudad,
                                                                            c.nroDoc,
                                                                            c.fechaNac,
                                                                            c.activo,
                                                                            ci.nombre
                                                                      FROM Chofer c JOIN Ciudad ci ON(c.idCiudad=ci.idCiudad)
                                                                      WHERE c.nombreChofer LIKE @nombre");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Int32.Parse(dr["idCiudad"].ToString());
                    ciudad.NombreCiudad = dr["nombre"].ToString();

                    Chofer chofer = new Chofer();
                    chofer.IdChofer = (int)dr["idChofer"];
                    chofer.NombreChofer= dr["nombreChofer"].ToString();
                    chofer.Activo = Boolean.Parse(dr["activo"].ToString());
                    chofer.FechaNac = (DateTime)dr["fechaNac"];
                    chofer.NroDoc = Int32.Parse(dr["nroDoc"].ToString());
                    chofer.Ciudad = ciudad;
                    lista.AddLast(chofer);
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
