using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using NegocioDatos;
using System.Globalization;

namespace Camiones
{
    public partial class RegistroPago : System.Web.UI.Page
    {

        public static LinkedList<DetallePago> detalles;
        public static LinkedList<DTEDetallePago> detalles_grilla;

        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 100;
            //Verifica si el usuario logeado tiene permisos suficiente parala pagina
            //En este caso es 100 (Administrador)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "RegistroPago.aspx";
                Response.Redirect("Login.aspx");
            }


            if (!IsPostBack)
            {

                SelectChofer.DataSource = GestorChoferes.listarChoferes();
                SelectChofer.DataValueField = "idChofer";
                SelectChofer.DataTextField = "nombreChofer";
                SelectChofer.DataBind();
                /*
                SelectViaje.DataSource = GestorViaje;
                SelectViaje.DataValueField = "idViaje";
                SelectViaje.DataBind();
                */
                detalles = new LinkedList<DetallePago>();
                detalles_grilla = new LinkedList<DTEDetallePago>();
                MontoTotal.Text = "0.0";

                Registrar.Visible = false;
            }
        }

        public void LimpiarCampos()
        {
            SelectViaje.SelectedValue = SelectViaje.SelectedValue;
            SelectChofer.SelectedValue = SelectChofer.SelectedValue;

            SelectViaje.SelectedIndex = -1;
            SelectChofer.SelectedIndex = -1;
            FechaPago.Text = "";
            MontoTotal.Text = "";
            Monto.Text = "";
            DescuentoAdelanto.Text = "";


        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            PagoChofer pago = new PagoChofer();
            pago.Chofer = GestorChoferes.buscarChofer(Convert.ToInt32(SelectChofer.SelectedValue));
            pago.FechaPago = Convert.ToDateTime(FechaPago.Text);

            GestorPagos.insertarPagoChofer(pago, detalles);


            Response.Redirect("InformeViajes.aspx");
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            DetallePago detallePago = new DetallePago();

            detallePago.idViaje = Convert.ToInt32(SelectViaje.SelectedValue);
            detallePago.monto = Convert.ToDouble(Monto.Text, CultureInfo.InvariantCulture); 
            detallePago.descuentoAdelanto = Convert.ToDouble(DescuentoAdelanto.Text, CultureInfo.InvariantCulture);
           
            detalles.AddLast(detallePago);

            DTEDetallePago dteDetallePago = new DTEDetallePago(detallePago);

            detalles_grilla.AddLast(dteDetallePago);

            GrillaPagos.DataSource = detalles_grilla;
            GrillaPagos.DataBind();

            double monto_total = Convert.ToDouble(MontoTotal.Text) + dteDetallePago.monto;

            MontoTotal.Text = monto_total.ToString();
            LimpiarCampos();
            Registrar.Visible = true;

        }
        
        protected void GrillaPagos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}