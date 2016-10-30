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
                    Chofer.idChofer = (int)dr["idChofer"];
                    Chofer.nombreChofer = dr["nombreChofer"].ToString();
                    Chofer.activo = Boolean.Parse(dr["activo"].ToString());
                    Chofer.FechaNac = (DateTime)dr["fechaNac"];
                    //var nroVehiculo = dr["nroVehiculo"];
                    Chofer.nroDoc = Int32.Parse(dr["nroDoc"].ToString());
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
    }
}
