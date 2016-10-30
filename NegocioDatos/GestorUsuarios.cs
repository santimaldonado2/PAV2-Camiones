using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace NegocioDatos
{
    public class GestorUsuarios
    {
        public static Usuario getUsuario(string username)
        {
            Usuario usuario = null;
            SqlConnection cn = GestorConexion.abrirConexion();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                SqlCommand cmd = GestorConexion.iniciarComando(cn, @"SELECT u.username,
                                                                            u.password,
                                                                            u.rol
                                                                      FROM  Usuario u
                                                                     WHERE  u.username = @username");
                cmd.Parameters.AddWithValue("@username", username.ToUpper());
                cmd.Transaction = tran;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    usuario = new Usuario();
                    usuario.Username = dr["username"].ToString();
                    usuario.Password = dr["password"].ToString();
                    usuario.Rol = Convert.ToInt32(dr["rol"].ToString());
                }
                dr.Close();
               
                return usuario;
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return usuario;
            }
            finally
            {
                GestorConexion.cerrarConexion(cn);
            }
        }
    }
}
