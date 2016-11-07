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


                SelectCobro.DataSource = GestorTipoCobros.listarTiposCobros();
                SelectCobro.DataValueField = "idTipoCobro";
                SelectCobro.DataTextField = "nombreTipoCobro";
                SelectCobro.DataBind();
                SelectCobro.Items.Add(new ListItem("Todos", "-1"));
                SelectCobro.SelectedValue = "-1";

                CargarGrillaCobros();
            }
        }


        public void CargarGrillaCobros()
        {
            DateTime inicio = Convert.ToDateTime("01/01/1990");
            DateTime fin = DateTime.Today;

            GrillaCobros.DataSource = (from cobro in GestorInformeCobros.buscarCobros(-1, -1, inicio, fin)
                                       orderby cobro.fechaCobro
                                       select cobro);

            GrillaCobros.DataKeyNames = new string[] { "idCobro" };
            GrillaCobros.DataBind();
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            int idCliente, idTipoCobro;
            DateTime fechaMin, fechaMax;

            idCliente = Convert.ToInt32(SelectCliente.SelectedValue);
            idTipoCobro = Convert.ToInt32(SelectCobro.SelectedValue);

            if (FechaMin.Text != "")
            {
                fechaMin = DateTime.ParseExact(FechaMin.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                fechaMin = Convert.ToDateTime("01/01/1990");
            }

            if (FechaMax.Text != "")
            {
                fechaMax = DateTime.ParseExact(FechaMax.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                fechaMax = DateTime.Today;
            }

            GrillaCobros.DataSource = (from cobro in GestorInformeCobros.buscarCobros(idCliente, idTipoCobro, fechaMin, fechaMax)
                                       orderby cobro.fechaCobro
                                       select cobro);

            GrillaCobros.DataKeyNames = new string[] { "idCobro" };
            GrillaCobros.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");

        }
    }
}