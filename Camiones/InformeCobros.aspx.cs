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
    public partial class InformeCobros : System.Web.UI.Page
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
                SelectCliente.DataSource = GestorClientes.listarClientes();
                SelectCliente.DataValueField = "idCliente";
                SelectCliente.DataTextField = "nombreCliente";
                SelectCliente.DataBind();
                SelectCliente.Items.Add(new ListItem("Todos", "-1"));
                SelectCliente.SelectedValue = "-1";


                SelectCobro.DataSource = GestorCobros.listarCobros();
                SelectCobro.DataValueField = "idCobro";
                SelectCobro.DataTextField = "montoCobro";
                SelectCobro.DataBind();
                SelectCobro.Items.Add(new ListItem("Todos", "-1"));
                SelectCobro.SelectedValue = "-1";
                CargarGrillaCobros();
            }
        }


        public void CargarGrillaCobros()
        {
            GrillaCobros.DataSource = (from viaje in GestorInformeCobros.buscarCobros(-1, -1, -1, -1)
                                       orderby viaje.distanciaTotal
                                       select viaje);

            GrillaCobros.DataKeyNames = new string[] { "idViaje" };
            GrillaCobros.DataBind();
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            int idCamion, idChofer;
            double distancia_min, distancia_max;
            idCamion = Convert.ToInt32(SelectCliente.SelectedValue);
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

            GrillaCobros.DataSource = (from viaje in GestorInformeViajes.buscarViajes(idChofer, idCamion, distancia_min, distancia_max)
                                       orderby viaje.distanciaTotal
                                       select viaje);

            GrillaCobros.DataKeyNames = new string[] { "idViaje" };
            GrillaCobros.DataBind();
        }



    }
}
}