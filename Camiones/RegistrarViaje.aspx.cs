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
    public partial class RegistrarViaje : System.Web.UI.Page
    {
        public static LinkedList<DetalleViaje> detalles;
        public static LinkedList<DTEDetalleViaje> detalles_grilla;

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
                SelectCamion.DataSource = GestorCamiones.listarCamiones();
                SelectCamion.DataValueField = "idCamion";
                SelectCamion.DataTextField = "nroVehiculo";
                SelectCamion.DataBind();

                SelectChofer.DataSource = GestorChoferes.listarChoferes();
                SelectChofer.DataValueField = "idChofer";
                SelectChofer.DataTextField = "nombreChofer";
                SelectChofer.DataBind();

                SelectCiudadOrigen.DataSource = GestorCiudades.listarCiudades();
                SelectCiudadOrigen.DataValueField = "idCiudad";
                SelectCiudadOrigen.DataTextField = "nombreCiudad";
                SelectCiudadOrigen.DataBind();

                cargarCiudadesDestino();

                SelectCliente.DataSource = GestorClientes.listarClientes();
                SelectCliente.DataValueField = "idCliente";
                SelectCliente.DataTextField = "nombreCliente";
                SelectCliente.DataBind();

                detalles = new LinkedList<DetalleViaje>();
                detalles_grilla = new LinkedList<DTEDetalleViaje>();
                DistanciaTotal.Text = "0.0";

                Registrar.Visible = false;
            }
        }

        private void cargarCiudadesDestino()
        {
            SelectCiudadDestino.DataSource = GestorCiudades.listarCiudadesExcepto(Convert.ToInt32(SelectCiudadOrigen.SelectedValue));
            SelectCiudadDestino.DataValueField = "idCiudad";
            SelectCiudadDestino.DataTextField = "nombreCiudad";
            SelectCiudadDestino.DataBind();
        }

        protected void SelectCiudadOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCiudadesDestino();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            DetalleViaje detalleViaje = new DetalleViaje();

            detalleViaje.CiudadDestino = GestorCiudades.getCiudad(Convert.ToInt32(SelectCiudadDestino.SelectedValue));
            detalleViaje.CiudadOrigen = GestorCiudades.getCiudad(Convert.ToInt32(SelectCiudadOrigen.SelectedValue));
            detalleViaje.FechaLlegada = Convert.ToDateTime(FechaDestino.Text);
            detalleViaje.FechaSalida = Convert.ToDateTime(FechaOrigen.Text, CultureInfo.InvariantCulture);
            detalleViaje.Kilogramos = Double.Parse(Kilogramos.Text, CultureInfo.InvariantCulture); ;
            detalleViaje.Distancia = Convert.ToDouble(Distancia.Text, CultureInfo.InvariantCulture);
            detalleViaje.PrecioUnitario = Convert.ToDouble(PrecioUnitario.Text, CultureInfo.InvariantCulture);
            detalleViaje.Cliente = GestorClientes.buscarCliente(Convert.ToInt32(SelectCliente.SelectedValue));

            detalles.AddLast(detalleViaje);

            DTEDetalleViaje dteDetalleViaje = new DTEDetalleViaje(detalleViaje);

            detalles_grilla.AddLast(dteDetalleViaje);

            GrillaDetallesViajes.DataSource = detalles_grilla;
            GrillaDetallesViajes.DataBind();

            double distancia_total = Convert.ToDouble(DistanciaTotal.Text) + dteDetalleViaje.Distancia;

            DistanciaTotal.Text = distancia_total.ToString();
            LimpiarCampos();
            Registrar.Visible = true;
        }

        public void LimpiarCampos()
        {
            SelectCiudadOrigen.SelectedValue = SelectCiudadDestino.SelectedValue;

            SelectCiudadDestino.SelectedIndex = -1;
            cargarCiudadesDestino();
            FechaOrigen.Text = FechaDestino.Text;
            FechaDestino.Text = "";
            SelectCliente.SelectedIndex = -1;
            Kilogramos.Text = "";
            Distancia.Text = "";
            PrecioUnitario.Text = "";
            

        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            Viaje viaje = new Viaje();
            viaje.Chofer = GestorChoferes.buscarChofer(Convert.ToInt32(SelectChofer.SelectedValue));
            viaje.Camion = GestorCamiones.buscarCamion(Convert.ToInt32(SelectCamion.SelectedValue));

            GestorViajes.RegistrarViaje(viaje, detalles);
            

            Response.Redirect("InformeViajes.aspx");
        }
    }
}