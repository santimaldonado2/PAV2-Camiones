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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int permisoMinimoPagina = 100;
            //Verifica si el usuario logeado tiene permisos suficiente parala pagina
            //En este caso es 100 (Administrador)
            if (!GestorSeguridad.TienePermisos((Usuario)Session["user"], permisoMinimoPagina))
            {
                Session["Pagina"] = "InformePagos.aspx";
                Response.Redirect("Login.aspx");
            }
            
                        if (!IsPostBack)
                        {
                            SelectChofer.DataSource = GestorChoferes.listarChoferes();
                            SelectChofer.DataValueField = "idChofer";
                            SelectChofer.DataTextField = "nombreChofer";
                            SelectChofer.DataBind();
                            SelectChofer.Items.Add(new ListItem("Todos", "-1"));
                            SelectChofer.SelectedValue = "-1";
                
                            CargarGrillaPagos();
                        }

                    }


        

                    public void CargarGrillaPagos()
                    {
                        DateTime fechaMin = DateTime.ParseExact("01/01/1000", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        fechaMin = DateTime.Parse("01/01/1000");
                        DateTime fechaMax = DateTime.ParseExact("01/01/1000", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        fechaMax = DateTime.Parse("01/01/1000");
                    GrillaPagos.DataSource = (from PagoChofer in GestorInformePagos.buscarPagos(-1,-1,-1, fechaMin, fechaMax)
                                                   orderby PagoChofer.idPago
                                                   select PagoChofer);

                        GrillaPagos.DataKeyNames = new string[] { "idPago" };
                        GrillaPagos.DataBind();
                    }

                    protected void Buscar_Click(object sender, EventArgs e)
                    {
            int idChofer;
            double monto_min, monto_max;
            idChofer = Convert.ToInt32(SelectChofer.SelectedValue);
            DateTime fechaMin, fechaMax;
            

            if (Monto_min.Text != "")
            {
                monto_min = Convert.ToDouble(Monto_min.Text);
            }
            else
            {
                monto_min = -1;
            }

            if (Monto_max.Text != "")
            {
                monto_max = Convert.ToDouble(Monto_max.Text);
            }
            else
            {
                monto_max = -1;
            }

            if (FechaMax.Text != "")
            {
                fechaMax = Convert.ToDateTime(FechaMax.Text);
            }
            else
            {
                fechaMax = DateTime.Parse("01/01/1000");
            }
            if(FechaMin.Text != "")
            {
                fechaMin = Convert.ToDateTime(FechaMin.Text);
            }
            else
            {
                fechaMin = DateTime.Parse("01/01/1000");
            }

            GrillaPagos.DataSource = (from PagoChofer in GestorInformePagos.buscarPagos(idChofer, monto_min, monto_max, fechaMin, fechaMax)
                                       orderby PagoChofer.montoTotal
                                       select PagoChofer);

            GrillaPagos.DataKeyNames = new string[] { "idPago" };
            GrillaPagos.DataBind();

        }
        }
    }
