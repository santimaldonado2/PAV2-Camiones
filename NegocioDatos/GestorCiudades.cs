using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace NegocioDatos
{
    public class GestorCiudades
    {
        public static LinkedList<Ciudad> listarCiudades()
        {
            LinkedList<Ciudad> lista = new LinkedList<Ciudad>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT idCiudad,nombre FROM Ciudad ORDER BY nombre");
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad marca = new Ciudad();
                    marca.IdCiudad = int.Parse(dr["idCiudad"].ToString());
                    marca.NombreCiudad = dr["nombre"].ToString();

                    lista.AddLast(marca);
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

        public static LinkedList<Ciudad> listarCiudadesExcepto(int id)
        {
            LinkedList<Ciudad> lista = new LinkedList<Ciudad>();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT idCiudad,nombre FROM Ciudad WHERE idCiudad <> @idCiudad ORDER BY nombre");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("idCiudad", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Ciudad marca = new Ciudad();
                    marca.IdCiudad = int.Parse(dr["idCiudad"].ToString());
                    marca.NombreCiudad = dr["nombre"].ToString();

                    lista.AddLast(marca);
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

        public static Ciudad getCiudad(int id)
        {
            Ciudad ciudad = new Ciudad();
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, "SELECT idCiudad,nombre FROM Ciudad WHERE idCiudad = @idCiudad");
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("idCiudad", id);
                SqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    
                    ciudad.IdCiudad = int.Parse(dr["idCiudad"].ToString());
                    ciudad.NombreCiudad = dr["nombre"].ToString();
                }
                return ciudad;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return ciudad;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }
    }

}
