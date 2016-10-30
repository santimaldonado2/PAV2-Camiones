using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioDatos;
using Entidades;
using System.Text.RegularExpressions;

namespace Camiones
{
    public partial class ABMCCamion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 50;
            //Verifica si el usuario logeado tiene permisos suficiente parala pagina
            //En este caso es 50(usuario Final)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "InformeViajes.aspx";
                Response.Redirect("Login.aspx");
            }
                
            
            if (!IsPostBack)
            {
                SelectMarca.DataSource = GestorMarcas.listarMarcas();
                SelectMarca.DataValueField = "idMarca";
                SelectMarca.DataTextField = "nombre";
                SelectMarca.DataBind();

                FechaCompraCompareValidator.ValueToCompare = string.Format("{0:dd/MM/yyyy}",DateTime.Today.ToShortDateString());

                CargarGrillaCamiones();
                formulario.Visible = false;
            }
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (campos_validos())
            {
                Camion camion = new Camion();
                Marca marca = new Marca();
                camion.Patente = Patente.Text;
                camion.Habilitado = Habilitado.Checked;
                camion.FechaCompra = DateTime.ParseExact(FechaCompra.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                camion.NroVehiculo = Int32.Parse(NroVehiculo.Text);
                marca.IdMarca = Int32.Parse(SelectMarca.SelectedValue);
                camion.Modelo = Modelo.Text;
                camion.Marca = marca;
                try
                {
                    if (IdCamion.Text != "")
                    {
                        camion.IdCamion = Int32.Parse(IdCamion.Text);
                        Mensaje.Text = GestorCamiones.modificarCamion(camion);
                    }
                    else
                    {
                        Mensaje.Text = GestorCamiones.insertarCamion(camion);
                    }

                    CargarGrillaCamiones();
                    LimpiarCampos();
                    panel_grilla.Visible = true;
                    formulario.Visible = false;

                }
                catch (Exception ex)
                {
                    Mensaje.Text = ex.Message;
                }       
            }
            else
                Mensaje.Text = "Datos incorrectos o incompletos!";


        }

        public void CargarGrillaCamiones()
        {
            GrillaCamiones.DataSource = (from camion in GestorCamiones.listarCamiones()
                                       orderby camion.FechaCompra
                                       select camion);

            GrillaCamiones.DataKeyNames = new string[] { "idCamion" };
            GrillaCamiones.DataBind();
        }

        public void CargarDatosCamion(Camion camion)
        {
            IdCamion.Text = camion.IdCamion.ToString();
            Patente.Text = camion.Patente;
            Habilitado.Checked = camion.Habilitado;
            FechaCompra.Text = string.Format("{0:dd/MM/yyyy}",camion.FechaCompra);
            NroVehiculo.Text = camion.NroVehiculo.ToString();
            SelectMarca.SelectedValue = camion.Marca.IdMarca.ToString();
            Modelo.Text = camion.Modelo;
        }

        public void LimpiarCampos()
        {
            IdCamion.Text = "";
            Patente.Text = "";
            Habilitado.Checked = false;
            FechaCompra.Text = "";
            NroVehiculo.Text = "";
            SelectMarca.SelectedValue = "1";
            Modelo.Text = "";
        }

        protected void GrillaCamiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camion camion = GestorCamiones.buscarCamion(Int32.Parse(GrillaCamiones.SelectedDataKey.Value.ToString()));
            this.CargarDatosCamion(camion);
            formulario.Visible = true;
            panel_grilla.Visible = false;
            titulo.InnerText = "Modificar Camión";
            Eliminar.Visible = true;
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            if (IdCamion.Text != "")
            {
                Mensaje.Text = GestorCamiones.eliminarCamion(Int32.Parse(IdCamion.Text));
                CargarGrillaCamiones();
            }
            else
                Mensaje.Text = "Debe seleccionar un camion para eliminar";

            LimpiarCampos();
        }

        protected bool campos_validos()
        {
            
            if (Patente.Text == "" || Patente.Text.Length > 10|| NroVehiculo.Text == "" || FechaCompra.Text == "" || FechaCompra.Text =="")
            {
                return false;
            }
            var col = Regex.Matches(FechaCompra.Text, "^(((((0[1-9])|(1\\d)|(2[0-8]))\\/((0[1-9])|(1[0-2])))|((31\\/((0[13578])|(1[02])))|((29|30)\\/((0[1,3-9])|(1[0-2])))))\\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\\/02\\/(19|20)(([02468][048])|([13579][26]))))$");
            if (col.Count == 0)
                return false;
            else
            {
                if (DateTime.Parse(FechaCompra.Text) >= DateTime.Today)
                    return false;
            }

            return true;
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            GrillaCamiones.DataSource = (from camion in GestorCamiones.buscarCamionPorPatente(BuscarPatente.Text)
                                         orderby camion.FechaCompra
                                         select camion);

            GrillaCamiones.DataKeyNames = new string[] { "idCamion" };
            GrillaCamiones.DataBind();
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            panel_grilla.Visible = false;
            formulario.Visible = true;
            Eliminar.Visible = false;
            titulo.InnerText = "Nuevo Camión";
        }
    }
}