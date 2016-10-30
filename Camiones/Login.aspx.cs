using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using NegocioDatos;

namespace Camiones
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void iniciar_sesion_Click(object sender, EventArgs e)
        {
            if (Session["Pagina"] == null)
                Session["Pagina"] = "ABMCCamion.aspx";
            Usuario user = GestorUsuarios.getUsuario(usuario.Text);

            if (user != null)
            {
                if (inputPassword.Text == user.Password)
                {
                    Session["user"] = user;
                    
                    Response.Redirect((string)Session["Pagina"]);
                }
                else
                {
                    mensaje.Text = "contraseña incorrecta";
                    Session["user"] = null;
                }
            }
            else
            {
                mensaje.Text = "El usuario ingresado no se encuentra registrado";
                Session["user"] = null;
            }
        }
    }
}