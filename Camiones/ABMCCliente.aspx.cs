using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Entidades;
using NegocioDatos;
using System.Text.RegularExpressions;

namespace Camiones
{
    public partial class ABMCCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 50;
            //Verifica si el usuario logeado tiene permisos suficiente parala pagina
            //En este caso es 50(usuario Final)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "ABMCCliente.aspx";
                Response.Redirect("Login.aspx");
            }


            if (!IsPostBack)
            {
                SelectCiudad.DataSource = GestorCiudades.listarCiudades();
                SelectCiudad.DataValueField = "IdCiudad";
                SelectCiudad.DataTextField = "NombreCiudad";
                SelectCiudad.DataBind();

                string aaaa = DateTime.Today.ToShortDateString();

                FechaInscripcionCompareValidator.ValueToCompare = string.Format("{0:dd/MM/yyyy}", DateTime.Today.ToShortDateString());



                CargarGrillaClientes();
                formulario.Visible = false;
            }

        }

        public void CargarGrillaClientes()
        {
            GrillaClientes.DataSource = (from cliente in GestorClientes.listarClientes()
                                         orderby cliente.FechaInscripcion
                                         select cliente);

            GrillaClientes.DataKeyNames = new string[] { "IdCliente" };
            GrillaClientes.DataBind();
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (campos_validos())
            {
                Cliente cliente = new Cliente();
                Ciudad ciudad = new Ciudad();
                cliente.NombreCliente = NombreCliente.Text;
                cliente.ClienteFijo = ClienteFijo.Checked;
                cliente.FechaInscripcion = DateTime.ParseExact(FechaInscripcion.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cliente.NroCuit = Int32.Parse(NroCuit.Text);
                ciudad.IdCiudad = Int32.Parse(SelectCiudad.SelectedValue);
                cliente.Ciudad = ciudad;
                try
                {
                    if (IdCliente.Text != "")
                    {
                        cliente.IdCliente = Int32.Parse(IdCliente.Text);
                        Mensaje.InnerText = GestorClientes.modificarCliente(cliente);
                    }
                    else
                    {
                        Mensaje.InnerText = GestorClientes.insertarCliente(cliente);
                    }

                    CargarGrillaClientes();
                    LimpiarCampos();
                    panel_grilla.Visible = true;
                    formulario.Visible = false;

                }
                catch (Exception ex)
                {
                    Mensaje.InnerText = ex.Message;
                }
            }
            else
                Mensaje.InnerText = "Datos incorrectos o incompletos!";
        }

        protected bool campos_validos()
        {

            if (NombreCliente.Text == "" || NroCuit.Text.Length > 11 || NroCuit.Text == "" || FechaInscripcion.Text == "" || FechaInscripcion.Text == "")
            {
                return false;
            }
            var col = Regex.Matches(FechaInscripcion.Text, "^(((((0[1-9])|(1\\d)|(2[0-8]))\\/((0[1-9])|(1[0-2])))|((31\\/((0[13578])|(1[02])))|((29|30)\\/((0[1,3-9])|(1[0-2])))))\\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\\/02\\/(19|20)(([02468][048])|([13579][26]))))$");
            if (col.Count == 0)
                return false;
            else
            {
                if (DateTime.Parse(FechaInscripcion.Text) >= DateTime.Today)
                    return false;
            }

            return true;
        }



        public void LimpiarCampos()
        {
            IdCliente.Text = "";
            NombreCliente.Text = "";
            ClienteFijo.Checked = false;
            FechaInscripcion.Text = "";
            NroCuit.Text = "";
            SelectCiudad.SelectedValue = "1";
        }



        public void CargarDatosCliente(Cliente cliente)
        {
            IdCliente.Text = cliente.IdCliente.ToString();
            NombreCliente.Text = cliente.NombreCliente;
            ClienteFijo.Checked = cliente.ClienteFijo;
            FechaInscripcion.Text = string.Format("{0:dd/MM/yyyy}", cliente.FechaInscripcion);
            NroCuit.Text = cliente.NroCuit.ToString();
            SelectCiudad.SelectedValue = cliente.Ciudad.IdCiudad.ToString();
        }


        protected void GrillaClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente cliente = GestorClientes.buscarCliente(Int32.Parse(GrillaClientes.SelectedDataKey.Value.ToString()));
            this.CargarDatosCliente(cliente);
            formulario.Visible = true;
            panel_grilla.Visible = false;
            titulo.InnerText = "Modificar Cliente";
            Eliminar.Visible = true;
        }


        protected void Eliminar_Click(object sender, EventArgs e)
        {
            if (IdCliente.Text != "")
            {
                Mensaje.InnerText = GestorClientes.eliminarCliente(Int32.Parse(IdCliente.Text));
                CargarGrillaClientes();
            }
            else
                Mensaje.InnerText = "Debe seleccionar un cliente para eliminar";

            LimpiarCampos();
            panel_grilla.Visible = true;
            formulario.Visible = false;

        }


        protected void Buscar_Click(object sender, EventArgs e)
        {
            GrillaClientes.DataSource = (from cliente in GestorClientes.buscarClientePorNombre(BuscarNombre.Text)
                                         orderby cliente.FechaInscripcion
                                         select cliente);

            GrillaClientes.DataKeyNames = new string[] { "IdCliente" };
            GrillaClientes.DataBind();
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            panel_grilla.Visible = false;
            formulario.Visible = true;
            Eliminar.Visible = false;
            titulo.InnerText = "Nuevo Cliente";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");

        }
    }
}