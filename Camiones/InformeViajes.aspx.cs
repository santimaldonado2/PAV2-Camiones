using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioDatos;
using Entidades;

namespace Camiones
{
    public partial class InformeViajes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 100;
            //Verifica si el usuario logeado tiene permisos suficiente parala pagina
            //En este caso es 100 (Administrador)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "InformeViajes.aspx";
                Response.Redirect("Login.aspx");
            }
               

            if (!IsPostBack)
            {
                SelectCamion.DataSource = GestorCamiones.listarCamiones();
                SelectCamion.DataValueField = "idCamion";
                SelectCamion.DataTextField = "nroVehiculo";
                SelectCamion.DataBind();
                SelectCamion.Items.Add(new ListItem("Todos", "-1"));
                SelectCamion.SelectedValue = "-1";


                SelectChofer.DataSource = GestorChoferes.listarChoferes();
                SelectChofer.DataValueField = "idChofer";
                SelectChofer.DataTextField = "nombreChofer";
                SelectChofer.DataBind();
                SelectChofer.Items.Add(new ListItem("Todos", "-1"));
                SelectCamion.SelectedValue = "-1";
                CargarGrillaViajes();
            }

        }




        public void CargarGrillaViajes()
        {
            GrillaViajes.DataSource = (from viaje in GestorInformeViajes.buscarViajes(-1, -1, -1,-1)
                                       orderby viaje.distanciaTotal
                                       select viaje);

            GrillaViajes.DataKeyNames = new string[] { "idViaje" };
            GrillaViajes.DataBind();
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            int idCamion, idChofer;
            double distancia_min, distancia_max;
            idCamion = Convert.ToInt32(SelectCamion.SelectedValue);
            idChofer = Convert.ToInt32(SelectChofer.SelectedValue);
            if (Distancia_min.Text != "")
            {
                distancia_min = Convert.ToInt32(Distancia_min.Text);
            }
            else
            {
                distancia_min = -1;
            }

            if (Distancia_max.Text != "")
            {
                distancia_max = Convert.ToInt32(Distancia_max.Text);
            }
            else
            {
                distancia_max = -1;
            }

            GrillaViajes.DataSource = (from viaje in GestorInformeViajes.buscarViajes(idChofer, idCamion , distancia_min,distancia_max)
                                       orderby viaje.distanciaTotal
                                       select viaje);

            GrillaViajes.DataKeyNames = new string[] { "idViaje" };
            GrillaViajes.DataBind();
        }
    }
}
