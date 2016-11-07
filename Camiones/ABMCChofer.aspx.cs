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
    public partial class ABMCChofer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 50;
            //Verifica si el usuario logeado tiene permisos suficiente para la pagina
            //En este caso es 50(usuario Final)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "ABMCChofer.aspx";
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                    SelectCiudad.DataSource = GestorCiudades.listarCiudades();
                    SelectCiudad.DataValueField = "idCiudad";
                    SelectCiudad.DataTextField = "nombreCiudad";
                    SelectCiudad.DataBind();

                    FechaNacCompareValidator.ValueToCompare = string.Format("{0:dd/MM/yyyy}", DateTime.Today.ToShortDateString());

                    CargarGrillaChoferes();
                    formulario.Visible = false;
            }
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            Chofer chofer = new Chofer();
            Ciudad ciudad = new Ciudad();
            chofer.NombreChofer = NombreChofer.Text;
            chofer.Activo = Activo.Checked;
            chofer.FechaNac = DateTime.ParseExact(FechaNac.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            chofer.FechaNac = Convert.ToDateTime(FechaNac.Text);
            chofer.NroDoc = Int32.Parse(NroDoc.Text);
            ciudad.IdCiudad = Int32.Parse(SelectCiudad.SelectedValue);
            chofer.Ciudad = ciudad;

            if (IdChofer.Text != "")
            {
                chofer.IdChofer = Int32.Parse(IdChofer.Text);
                Mensaje.Text = GestorChoferes.modificarChofer(chofer);
            }
            else
            {
                try
                {
                    Mensaje.Text = GestorChoferes.insertarChofer(chofer);
                }
                catch(Exception ex)
                {
                    Mensaje.Text = ex.Message;
                }
                
            }

            LimpiarCampos();
            panel_grilla.Visible = true;
            formulario.Visible = false;
            CargarGrillaChoferes();
            

        }

        private void LimpiarCampos()
        {
            IdChofer.Text = "";
            NombreChofer.Text = "";
            Activo.Checked = false;
            FechaNac.Text = "";
            NroDoc.Text = "";
            SelectCiudad.SelectedValue = "1";
        }

        public void CargarGrillaChoferes()
        {
            GrillaChoferes.DataSource = (from chofer in GestorChoferes.listarChoferes()
                                         orderby chofer.FechaNac
                                         select chofer);

            GrillaChoferes.DataKeyNames = new string[] { "idChofer" };
            GrillaChoferes.DataBind();
        }
        
        public void CargarDatosChofer(Chofer chofer)
        {
            if (chofer != null)
            {
                    IdChofer.Text = chofer.IdChofer.ToString();
                    NroDoc.Text = chofer.NroDoc.ToString();
                    Activo.Checked = chofer.Activo;
                    FechaNac.Text = string.Format("{0:dd/MM/yyyy}", chofer.FechaNac);
                    NombreChofer.Text = chofer.NombreChofer.ToString();
                    SelectCiudad.SelectedValue = chofer.Ciudad.IdCiudad.ToString();
                
            }
            

        }

        protected void GrillaChoferes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chofer chofer = GestorChoferes.buscarChofer(Int32.Parse(GrillaChoferes.SelectedDataKey.Value.ToString()));
            this.CargarDatosChofer(chofer);
            formulario.Visible = true;
            panel_grilla.Visible = false;
            titulo.InnerText = "Modificar Camión";
            Eliminar.Visible = true;
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            if (IdChofer.Text != "")
            {
                Mensaje.Text = GestorChoferes.eliminarChofer(Int32.Parse(IdChofer.Text));
                CargarGrillaChoferes();
            }
            else { 
                Mensaje.Text = "Debe seleccionar un chofer para eliminar";
            }
            LimpiarCampos();
            panel_grilla.Visible = true;
            formulario.Visible = false;
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            GrillaChoferes.DataSource = (from chofer in GestorChoferes.buscarPorNombre(BuscarNombre.Text)
                                         orderby chofer.NombreChofer
                                         select chofer);

            GrillaChoferes.DataKeyNames = new string[] { "idChofer" };
            GrillaChoferes.DataBind();
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            panel_grilla.Visible = false;
            formulario.Visible = true;
            Eliminar.Visible = false;
            titulo.InnerText = "Nuevo Chofer";
        }

        protected void Cancelar_click(object sender, EventArgs e)
        {
            formulario.Visible = false;
            panel_grilla.Visible = true;
            LimpiarCampos();
        }
    }
}