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
        protected AdministradorWSClient admin_client;
        private static int paginaActual = 1;
        private static int tamanoPagina = 10; // Número de publicaciones por página

        protected void Page_Init(object sender, EventArgs e)
        {
            client = new UsuarioWSClient();
            admin_client = new AdministradorWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                paginaActual = 1;
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                List<usuario> usuarios_total = new List<usuario>();

                // Si no hay filtro, cargar todas
                usuarios_total = client.listarUsuarios().ToList();

                int totalUsuarios = usuarios_total.Count;

                var usuariosPagina = usuarios_total
                    .Skip((paginaActual - 1) * tamanoPagina) // Saltar las publicaciones de las páginas anteriores
                    .Take(tamanoPagina) // Tomar el número de publicaciones para la página actual
                    .ToList();

                ViewState["Usuarios"] = usuarios_total; // guardar para filtrado posterior
                rptUsuarios.DataSource = usuariosPagina;
                rptUsuarios.DataBind();

                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)usuarios_total.Count / tamanoPagina)}";

                btnAnterior.Enabled = paginaActual > 1; // Deshabilitar si es la primera página
                btnSiguiente.Enabled = (paginaActual * tamanoPagina) < usuarios_total.Count; // Deshabilitar si es la última página

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar usuarios: " + ex.Message);
            }
        }
        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarUsuariosFiltradas();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual++;
            CargarUsuariosFiltradas();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            ViewState["FiltroNombre"] = criterio;

            if (ViewState["Usuarios"] != null)
            {
                var usuarios = (List<usuario>)ViewState["Usuarios"];
                var filtradas = usuarios
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                ViewState["UsuariosFiltrados"] = filtradas;
                paginaActual = 1;
                CargarUsuariosFiltradas();
            }
        }
        protected void CargarUsuariosFiltradas()
        {
            var usuarios = ViewState["UsuariosFiltrados"] as List<usuario> ??
                ViewState["Usuarios"] as List<usuario>;

            int total = usuarios.Count;

            var pagina = usuarios
                .Skip((paginaActual - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            rptUsuarios.DataSource = pagina;
            rptUsuarios.DataBind();

            lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)total / tamanoPagina)}";
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = (paginaActual * tamanoPagina) < total;
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

        protected void btnAsignarAdminModal_Click(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(hfIdUsuarioAdmin.Value);
            string claveMaestra = txtClaveMaestra.Text;

            usuario usuario = client.obtenerUsuario(idUsuario);
            if (usuario == null)
            {
                LblError.Text = "Error: No se encontró al usuario.";
                return;
            }

            administrador nuevoAdmin = new administrador
            {
                idUsuario = usuario.idUsuario,
                codigoPUCP = usuario.codigoPUCP,
                contrasena = usuario.contrasena,
                nombre = usuario.nombre,
                nombreUsuario = usuario.nombreUsuario,
                correo = usuario.correo,
                estado = usuario.estado,
                estadoSpecified = true,
                activo = true,
                claveMaestra = claveMaestra
            };


            try
            {
                admin_client.registrarAdministrador(nuevoAdmin);
                // Podrías mostrar un mensaje de éxito o refrescar la tabla
                Response.Redirect(Request.RawUrl); // Recargar la página
            }
            catch (Exception ex)
            {
                LblError.Text = "Error al asignar administrador: " + ex.Message;
            }
        }
    }
}