using QhatuPUCPPresentacion.WebService;
using System;
using System.Web.UI;

namespace QhatuPUCPPresentacion.Perfil
{
    public partial class EditarPublicacion : Page
    {
        private int idPublicacion;
        private PublicacionWSClient publicacionService = new PublicacionWSClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
                return;
            }

            if (!int.TryParse(Request.QueryString["id"], out idPublicacion))
            {
                lblMensaje.Text = "ID de publicación inválido.";
                return;
            }

            if (!IsPostBack)
            {
                publicacion publicacion = publicacionService.obtenerPublicacion(idPublicacion);
                if (publicacion == null || publicacion.usuario.idUsuario != ((usuario)Session["usuario"]).idUsuario)
                {
                    lblMensaje.Text = "No tienes permiso para editar esta publicación.";
                    btnGuardar.Enabled = false;
                    return;
                }

                txtTitulo.Text = publicacion.titulo;
                txtDescripcion.Text = publicacion.descripcion;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                publicacion publicacion = publicacionService.obtenerPublicacion(idPublicacion);
                publicacion.titulo = txtTitulo.Text.Trim();
                publicacion.descripcion = txtDescripcion.Text.Trim();

                // === DEBUG: Imprimir atributos de la publicación antes de enviar ===
                System.Diagnostics.Debug.WriteLine("== DATOS DE LA PUBLICACIÓN ==");
                System.Diagnostics.Debug.WriteLine("ID: " + publicacion.idPublicacion);
                System.Diagnostics.Debug.WriteLine("Título: " + publicacion.titulo);
                System.Diagnostics.Debug.WriteLine("Descripción: " + publicacion.descripcion);
                System.Diagnostics.Debug.WriteLine("Fecha creación: " + publicacion.fechaPublicacion);
                System.Diagnostics.Debug.WriteLine("Estado: " + publicacion.estado);
                System.Diagnostics.Debug.WriteLine("Usuario: " + (publicacion.usuario != null ? publicacion.usuario.idUsuario.ToString() : "null"));
                System.Diagnostics.Debug.WriteLine("Lista de cursos: " + (publicacion.publicacionesCursos != null ? publicacion.publicacionesCursos.Length.ToString() : "null"));
                System.Diagnostics.Debug.WriteLine("Lista de especialidades: " + (publicacion.publicacionesEspecialidades != null ? publicacion.publicacionesEspecialidades.Length.ToString() : "null"));
                System.Diagnostics.Debug.WriteLine("Lista de facultades: " + (publicacion.publicacionesFacultades != null ? publicacion.publicacionesFacultades.Length.ToString() : "null"));
                System.Diagnostics.Debug.WriteLine("========== FIN ==========");

                publicacionService.actualizarPublicacion(publicacion);
                Response.Redirect("~/Perfil/Perfil.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar cambios: " + ex.Message;
            }
        }
    }
}
