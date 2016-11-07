using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using NegocioDatos;
using System.Data.SqlClient;

namespace Camiones
{
    public partial class RegistrarCobro : System.Web.UI.Page
    {
        public static LinkedList<DetalleCobro> detalles;
        public static LinkedList<DTEDetalleCobro> detalles_grilla;

        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 100;
            //Verifica si el usuario logeado tiene permisos suficiente parala pagina
            //En este caso es 100 (Administrador)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "RegistrarViaje.aspx";
                Response.Redirect("Login.aspx");
            }


            if (!IsPostBack)
            {
                SelectCliente.DataSource = GestorClientes.listarClientes();
                SelectCliente.DataValueField = "idCliente";
                SelectCliente.DataTextField = "nombreCliente";
                SelectCliente.DataBind();

                SelectTipoCobro.DataSource = GestorTipoCobros.listarTiposCobros();
                SelectTipoCobro.DataValueField = "idTipoCobro";
                SelectTipoCobro.DataTextField = "nombreTipoCobro";
                SelectTipoCobro.DataBind();

                GrillaDetallesViajes.DataSource = GestorDetalleViajesGrilla.listarDetallesViajes();
                GrillaDetallesViajes.DataBind();

                detalles = new LinkedList<DetalleCobro>(); //la que se va a cargar despues en la bd
                detalles_grilla = new LinkedList<DTEDetalleCobro>(); //la que carga la grid de abajo
                txtMontoTotal.Text = "0.0";

                Registrar.Visible = false;
            }
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            DetalleCobro detalleCobro = new DetalleCobro();
            int id = Int32.Parse(GrillaDetallesViajes.SelectedDataKey.Value.ToString());
            DTEDetalleCobro dteDetalleCobro = new DTEDetalleCobro();
            DetalleViajeParaGrilla dv = new DetalleViajeParaGrilla();
            dv = GestorDetalleViajesGrilla.obtenerDetalleViaje(id);
               
            detalleCobro.idDetalleViaje = Int32.Parse(GrillaDetallesViajes.SelectedDataKey.Value.ToString());
            detalleCobro.monto = dv.Subtotal;           
            detalles.AddLast(detalleCobro);
                    
            dteDetalleCobro.nombreCliente = SelectCliente.SelectedItem.Text;
            dteDetalleCobro.tipoCobro = SelectTipoCobro.SelectedItem.Text;
            dteDetalleCobro.fechaCobro = Convert.ToDateTime(FechaCobro.Text);
            dteDetalleCobro.fechaSalida = dv.FechaSalida;
            dteDetalleCobro.fechaLlegada = dv.FechaLlegada;
            dteDetalleCobro.kilogramos = dv.Kilogramos;
            dteDetalleCobro.precioUnitario = dv.PrecioUnitario;
            dteDetalleCobro.subtotal = dv.Subtotal;
            dteDetalleCobro.distancia = dv.Distancia;

            detalles_grilla.AddLast(dteDetalleCobro);

            gridDetalleCobro.DataSource = detalles_grilla;
            gridDetalleCobro.DataBind();

            double montoTotal = Convert.ToDouble(txtMontoTotal.Text) + dteDetalleCobro.subtotal;

            txtMontoTotal.Text = montoTotal.ToString();
            LimpiarCampos();
            Registrar.Visible = true;
        }

        public void LimpiarCampos()
        {

        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            Cobro cobro = new Cobro();
            cobro.idCliente = Convert.ToInt32(SelectCliente.SelectedValue);
            cobro.idTipoCobro = Convert.ToInt32(SelectTipoCobro.SelectedValue);
            cobro.montoTotal = Convert.ToDouble(txtMontoTotal.Text);

            GestorCobros.RegistrarCobro(cobro, detalles);


            Response.Redirect("InformeViajes.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}