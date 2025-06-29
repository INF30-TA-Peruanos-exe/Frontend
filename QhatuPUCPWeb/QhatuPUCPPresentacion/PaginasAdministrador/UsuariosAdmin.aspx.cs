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
    public partial class UsuariosAdmin : System.Web.UI.Page
    {
        protected UsuarioWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new UsuarioWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                List<usuario> usuarios = new List<usuario>();

                // Si no hay filtro, cargar todas
                usuarios = client.listarUsuarios().ToList();

                ViewState["Usuarios"] = usuarios; // guardar para filtrado posterior
                rptUsuarios.DataSource = usuarios;
                rptUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar usuarios: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (ViewState["Usuarios"] != null)
            {
                var usuarios = (List<usuario>)ViewState["Usuarios"];
                var filtradas = usuarios
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                rptUsuarios.DataSource = filtradas;
                rptUsuarios.DataBind();
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // o directamente filtrar aquí
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            int id_usuario= Int32.Parse(((LinkButton)sender).CommandArgument);

            usuario update_usuario = client.obtenerUsuario(id_usuario);

            if (update_usuario.estado == estadoUsuario.HABILITADO)
                update_usuario.estado = estadoUsuario.RESTRINGIDO;
            else
                update_usuario.estado = estadoUsuario.HABILITADO;

            client.actualizarUsuario(update_usuario);

            CargarUsuarios();

            //Response.Redirect("/Usuario/EditarUsuario.aspx?id_usuario=" + id_usuario_str);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_usuario = int.Parse(argument);
            if (!client.eliminarUsuario(id_usuario))
            {
                LblError.Text = "Error al eliminar el área";
            }
            else
            {
                CargarUsuarios();
            }
        }

        protected void btnDescargarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamada al método reporteIncidencias
                byte[] reportePDF = client.reporteUsuarios();

                // Validación del contenido
                if (reportePDF != null && reportePDF.Length > 0)
                {
                    // Preparar respuesta para descargar el PDF
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Top_Usuarios.pdf");
                    Response.OutputStream.Write(reportePDF, 0, reportePDF.Length);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    // Mensaje de error si viene vacío
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El reporte está vacío o no se pudo generar.');", true);
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al descargar el reporte: " + ex.Message.Replace("'", "") + "');", true);
            }
        }
    }
}