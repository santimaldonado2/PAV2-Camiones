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
                                                                            c.nroDoc,
                                                                            c.fechaNac,
                                                                            c.activo
                                                                      FROM  Chofer c
                                                                      ORDER BY nombreChofer");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Chofer Chofer = new Chofer();
                    Chofer.IdChofer = (int)dr["idChofer"];
                    Chofer.NombreChofer = dr["nombreChofer"].ToString();
                    Chofer.Activo = Boolean.Parse(dr["activo"].ToString());
                    Chofer.FechaNac = (DateTime)dr["fechaNac"];
                    //var nroVehiculo = dr["nroVehiculo"];
                    Chofer.NroDoc = Int32.Parse(dr["nroDoc"].ToString());
                    lista.AddLast(Chofer);
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
    }
}
