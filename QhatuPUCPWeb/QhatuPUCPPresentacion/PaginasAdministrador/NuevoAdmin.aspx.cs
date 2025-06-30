using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.PaginasAdministrador
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected UsuarioWSClient client;
        protected AdministradorWSClient adminWS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            client = new UsuarioWSClient();
            adminWS = new AdministradorWSClient();
        }


        protected void btnAsignar_Command(object sender, CommandEventArgs e)
        {
            int idUsuario = int.Parse(e.CommandArgument.ToString());

            RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;
            TextBox txtClaveMaestra = item.FindControl("txtClaveMaestra") as TextBox;

            string claveMaestra = txtClaveMaestra.Text.Trim();

            // Verificamos si ya es administrador
            administrador adminExistente = adminWS.obtenerAdministrador(idUsuario);
            if (adminExistente != null)
            {
                lblMensaje.Text = "Este usuario ya es administrador.";
                return;
            }

            if (string.IsNullOrEmpty(claveMaestra))
            {
                lblMensaje.Text = "Debe ingresar una clave maestra.";
                return;
            }

            // Si pasa validaciones, lo insertamos
            administrador nuevoAdmin = new administrador
            {
                idUsuario = idUsuario,
                claveMaestra = claveMaestra
            };

            bool exito = adminWS.registrarAdministrador(nuevoAdmin);
            lblMensaje.Text = exito ? "Administrador asignado correctamente." : "Error al asignar administrador.";
        }
    }
}